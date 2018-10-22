#pragma checksum "D:\JMann\Finance\Application\Finance\AddEdit_ZTapeRecord.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "65D5CE27C95FEA8A08D4DB5593138662072E8C29"

#line 1 "D:\JMann\Finance\Application\Finance\AddEdit_ZTapeRecord.aspx.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace jmann
{
    public partial class AddEdit_ZTapeRecord : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        protected void Page_Load(object sender, EventArgs e)
        {   CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
            if (!IsPostBack)
            {   fill_location();					// populate location dropdown
                string operation = (string)Request["operation"];

                if (!String.IsNullOrEmpty(operation))
                {
                    if (operation == "update")
                    {
                        LblTitle.Text = "Edit ZTapeRecord";
                        BtnUpdateRecord.Visible = true;
                        BtnAddRecord.Visible = false;

                        // retrieve id(s) to be updated
                        string zTapeRecordID = (string)Request["ztaperecordid"];

                        if (!String.IsNullOrEmpty(zTapeRecordID))
                        {
                            // retrieve record to be updated
                            ZTapeRecord objZTapeRecord = jmann.BusinessObject.ZTapeRecord.SelectByPrimaryKey(Convert.ToInt32(zTapeRecordID));

                            if (objZTapeRecord != null)
                            {
                                TxtZTapeRecordID.Enabled = false;
                                TxtZTapeRecordID.Text = objZTapeRecord.ZTapeRecordID.ToString();
                                ddlLocation.SelectedValue = objZTapeRecord.LocationID.ToString();
                                TxtAddComment.Text = objZTapeRecord.AddComment;
                                TxtCashComment.Text = objZTapeRecord.CashComment;

                                if(objZTapeRecord.Date != null)
                                    TxtDate.Text = String.Format("{0:M/d/yyyy}",Convert.ToDateTime(objZTapeRecord.Date.ToString()));

                                if(objZTapeRecord.Register != null)
                                    TxtRegister.Text = objZTapeRecord.Register.ToString();

                                if(objZTapeRecord.AddAdjustment != null)
                                    TxtAddAdjustment.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.AddAdjustment.ToString()));

                                if(objZTapeRecord.CashAdjustment != null)
                                    TxtCashAdjustment.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.CashAdjustment.ToString()));

                                if(objZTapeRecord.Discount1StCount != null)
                                    TxtDiscount1StCount.Text = objZTapeRecord.Discount1StCount.ToString();

                                if(objZTapeRecord.Discount1StTotal != null)
                                    TxtDiscount1StTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.Discount1StTotal.ToString()));

                                if(objZTapeRecord.Discount2StCount != null)
                                    TxtDiscount2StCount.Text = objZTapeRecord.Discount2StCount.ToString();

                                if(objZTapeRecord.Discount2StTotal != null)
                                    TxtDiscount2StTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.Discount2StTotal.ToString()));

                                if(objZTapeRecord.ChargeCount != null)
                                    TxtChargeCount.Text = objZTapeRecord.ChargeCount.ToString();

                                if(objZTapeRecord.ChargeTotal != null)
                                    TxtChargeTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ChargeTotal.ToString()));

                                if(objZTapeRecord.CashCount != null)
                                    TxtCashCount.Text = objZTapeRecord.CashCount.ToString();

                                if(objZTapeRecord.CashTotal != null)
                                    TxtCashTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.CashTotal.ToString()));

                                if(objZTapeRecord.Tax != null)
                                    TxtTax.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.Tax.ToString()));

                                if(objZTapeRecord.Discount1ItCount != null)
                                    TxtDiscount1ItCount.Text = objZTapeRecord.Discount1ItCount.ToString();

                                if(objZTapeRecord.Discount1ItTotal != null)
                                    TxtDiscount1ItTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.Discount1ItTotal.ToString()));

                                if(objZTapeRecord.Discount2ItCount != null)
                                    TxtDiscount2ItCount.Text = objZTapeRecord.Discount2ItCount.ToString();

                                if(objZTapeRecord.Discount2ItTotal != null)
                                    TxtDiscount2ItTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.Discount2ItTotal.ToString()));

                                if(objZTapeRecord.ReturnsCount != null)
                                    TxtReturnsCount.Text = objZTapeRecord.ReturnsCount.ToString();

                                if(objZTapeRecord.ReturnsTotal != null)
                                    TxtReturnsTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ReturnsTotal.ToString()));

                                if(objZTapeRecord.ReturnsTax != null)
                                    TxtReturnsTax.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ReturnsTax.ToString()));

                                if(objZTapeRecord.FurnitureCount != null)
                                    TxtFurnitureCount.Text = objZTapeRecord.FurnitureCount.ToString();

                                if(objZTapeRecord.FurnitureTotal != null)
                                    TxtFurnitureTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.FurnitureTotal.ToString()));

                                if(objZTapeRecord.JewelryCount != null)
                                    TxtJewelryCount.Text = objZTapeRecord.JewelryCount.ToString();

                                if(objZTapeRecord.JewelryTotal != null)
                                    TxtJewelryTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.JewelryTotal.ToString()));

                                if(objZTapeRecord.ElectricalCount != null)
                                    TxtElectricalCount.Text = objZTapeRecord.ElectricalCount.ToString();

                                if(objZTapeRecord.ElectricalTotal != null)
                                    TxtElectricalTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ElectricalTotal.ToString()));

                                if(objZTapeRecord.WomensCount != null)
                                    TxtWomensCount.Text = objZTapeRecord.WomensCount.ToString();

                                if(objZTapeRecord.WomensTotal != null)
                                    TxtWomensTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.WomensTotal.ToString()));

                                if(objZTapeRecord.BinsCount != null)
                                    TxtBinsCount.Text = objZTapeRecord.BinsCount.ToString();

                                if(objZTapeRecord.BinsTotal != null)
                                    TxtBinsTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.BinsTotal.ToString()));

                                if(objZTapeRecord.MiscCount != null)
                                    TxtMiscCount.Text = objZTapeRecord.MiscCount.ToString();

                                if(objZTapeRecord.MiscTotal != null)
                                    TxtMiscTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.MiscTotal.ToString()));

                                if(objZTapeRecord.ShoesCount != null)
                                    TxtShoesCount.Text = objZTapeRecord.ShoesCount.ToString();

                                if(objZTapeRecord.ShoesTotal != null)
                                    TxtShoesTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ShoesTotal.ToString()));

                                if(objZTapeRecord.BoutiqueCount != null)
                                    TxtBoutiqueCount.Text = objZTapeRecord.BoutiqueCount.ToString();

                                if(objZTapeRecord.BoutiqueTotal != null)
                                    TxtBoutiqueTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.BoutiqueTotal.ToString()));

                                if(objZTapeRecord.ChildsCount != null)
                                    TxtChildsCount.Text = objZTapeRecord.ChildsCount.ToString();

                                if(objZTapeRecord.ChildsTotal != null)
                                    TxtChildsTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ChildsTotal.ToString()));

                                if(objZTapeRecord.MensCount != null)
                                    TxtMensCount.Text = objZTapeRecord.MensCount.ToString();

                                if(objZTapeRecord.MensTotal != null)
                                    TxtMensTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.MensTotal.ToString()));

                                if(objZTapeRecord.BooksCount != null)
                                    TxtBooksCount.Text = objZTapeRecord.BooksCount.ToString();

                                if(objZTapeRecord.BooksTotal != null)
                                    TxtBooksTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.BooksTotal.ToString()));

                                if(objZTapeRecord.FurnitureCountDisc != null)
                                    TxtFurnitureCountDisc.Text = objZTapeRecord.FurnitureCountDisc.ToString();

                                if(objZTapeRecord.FurnitureTotalDisc != null)
                                    TxtFurnitureTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.FurnitureTotalDisc.ToString()));

                                if(objZTapeRecord.JewelryCountDisc != null)
                                    TxtJewelryCountDisc.Text = objZTapeRecord.JewelryCountDisc.ToString();

                                if(objZTapeRecord.JewelryTotalDisc != null)
                                    TxtJewelryTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.JewelryTotalDisc.ToString()));

                                if(objZTapeRecord.ElectricalCountDisc != null)
                                    TxtElectricalCountDisc.Text = objZTapeRecord.ElectricalCountDisc.ToString();

                                if(objZTapeRecord.ElectricalTotalDisc != null)
                                    TxtElectricalTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ElectricalTotalDisc.ToString()));

                                if(objZTapeRecord.WomensCountDisc != null)
                                    TxtWomensCountDisc.Text = objZTapeRecord.WomensCountDisc.ToString();

                                if(objZTapeRecord.WomensTotalDisc != null)
                                    TxtWomensTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.WomensTotalDisc.ToString()));

                                if(objZTapeRecord.BinsCountDisc != null)
                                    TxtBinsCountDisc.Text = objZTapeRecord.BinsCountDisc.ToString();

                                if(objZTapeRecord.BinsTotalDisc != null)
                                    TxtBinsTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.BinsTotalDisc.ToString()));

                                if(objZTapeRecord.MiscCountDisc != null)
                                    TxtMiscCountDisc.Text = objZTapeRecord.MiscCountDisc.ToString();

                                if(objZTapeRecord.MiscTotalDisc != null)
                                    TxtMiscTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.MiscTotalDisc.ToString()));

                                if(objZTapeRecord.ShoesCountDisc != null)
                                    TxtShoesCountDisc.Text = objZTapeRecord.ShoesCountDisc.ToString();

                                if(objZTapeRecord.ShoesTotalDisc != null)
                                    TxtShoesTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ShoesTotalDisc.ToString()));

                                if(objZTapeRecord.BoutiqueCountDisc != null)
                                    TxtBoutiqueCountDisc.Text = objZTapeRecord.BoutiqueCountDisc.ToString();

                                if(objZTapeRecord.BoutiqueTotalDisc != null)
                                    TxtBoutiqueTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.BoutiqueTotalDisc.ToString()));

                                if(objZTapeRecord.ChildsCountDisc != null)
                                    TxtChildsCountDisc.Text = objZTapeRecord.ChildsCountDisc.ToString();

                                if(objZTapeRecord.ChildsTotalDisc != null)
                                    TxtChildsTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.ChildsTotalDisc.ToString()));

                                if(objZTapeRecord.MensCountDisc != null)
                                    TxtMensCountDisc.Text = objZTapeRecord.MensCountDisc.ToString();

                                if(objZTapeRecord.MensTotalDisc != null)
                                    TxtMensTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.MensTotalDisc.ToString()));

                                if(objZTapeRecord.BooksCountDisc != null)
                                    TxtBooksCountDisc.Text = objZTapeRecord.BooksCountDisc.ToString();

                                if(objZTapeRecord.BooksTotalDisc != null)
                                    TxtBooksTotalDisc.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.BooksTotalDisc.ToString()));

                                if(objZTapeRecord.CorrectionCount != null)
                                    TxtCorrectionCount.Text = objZTapeRecord.CorrectionCount.ToString();

                                if(objZTapeRecord.CorrectionTotal != null)
                                    TxtCorrectionTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.CorrectionTotal.ToString()));

                                if(objZTapeRecord.VoidCount != null)
                                    TxtVoidCount.Text = objZTapeRecord.VoidCount.ToString();

                                if(objZTapeRecord.VoidTotal != null)
                                    TxtVoidTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.VoidTotal.ToString()));

                                if(objZTapeRecord.AllVoidCount != null)
                                    TxtAllVoidCount.Text = objZTapeRecord.AllVoidCount.ToString();

                                if(objZTapeRecord.AllVoidTotal != null)
                                    TxtAllVoidTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objZTapeRecord.AllVoidTotal.ToString()));

                                if(objZTapeRecord.NewMerchCount != null)
                                    TxtNewMerchCount.Text = objZTapeRecord.NewMerchCount.ToString();

                                if(objZTapeRecord.NewMerchTotal != null)
                                    TxtNewMerchTotal.Text = String.Format("{0:0.00}", Convert.ToDecimal(objZTapeRecord.NewMerchTotal.ToString()));
                            }
                        }
                    }
                    else
                    {
                        // add a record
                        PnlPrimaryKey.Visible = false;
                        BtnUpdateRecord.Visible = false;
                    }
                }
            }
        }

        protected void BtnAddRecord_Click(object sender, EventArgs e)
        {
            AddOrUpdateRecord("add");
        }

        protected void BtnUpdateRecord_Click(object sender, EventArgs e)
        {
            AddOrUpdateRecord("update");
        }

        private void AddOrUpdateRecord(string operation)
        {
            if (IsValid)
            {
                ZTapeRecord objZTapeRecord;

                if (operation == "update")
                    objZTapeRecord = jmann.BusinessObject.ZTapeRecord.SelectByPrimaryKey(Convert.ToInt32(TxtZTapeRecordID.Text));
                else
                {
                    objZTapeRecord = new ZTapeRecord();
                }

                if (String.IsNullOrEmpty(TxtDate.Text))
                    objZTapeRecord.Date = null;
                else
                    objZTapeRecord.Date = Convert.ToDateTime(TxtDate.Text);

                if (String.IsNullOrEmpty(ddlLocation.SelectedValue))
                    objZTapeRecord.LocationID = null;
                else
                    objZTapeRecord.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);

                if (String.IsNullOrEmpty(TxtRegister.Text))
                    objZTapeRecord.Register = null;
                else
                    objZTapeRecord.Register = Convert.ToInt32(TxtRegister.Text);

                if (String.IsNullOrEmpty(TxtAddAdjustment.Text))
                    objZTapeRecord.AddAdjustment = null;
                else
                    objZTapeRecord.AddAdjustment = Convert.ToDecimal(TxtAddAdjustment.Text);

                if (String.IsNullOrEmpty(TxtAddComment.Text))
                    objZTapeRecord.AddComment = null;
                else
                    objZTapeRecord.AddComment = TxtAddComment.Text;

                if (String.IsNullOrEmpty(TxtCashAdjustment.Text))
                    objZTapeRecord.CashAdjustment = null;
                else
                    objZTapeRecord.CashAdjustment = Convert.ToDecimal(TxtCashAdjustment.Text);

                if (String.IsNullOrEmpty(TxtCashComment.Text))
                    objZTapeRecord.CashComment = null;
                else
                    objZTapeRecord.CashComment = TxtCashComment.Text;

                if (String.IsNullOrEmpty(TxtDiscount1StCount.Text))
                    objZTapeRecord.Discount1StCount = null;
                else
                    objZTapeRecord.Discount1StCount = Convert.ToInt32(TxtDiscount1StCount.Text);

                if (String.IsNullOrEmpty(TxtDiscount1StTotal.Text))
                    objZTapeRecord.Discount1StTotal = null;
                else
                    objZTapeRecord.Discount1StTotal = Convert.ToDecimal(TxtDiscount1StTotal.Text);

                if (String.IsNullOrEmpty(TxtDiscount2StCount.Text))
                    objZTapeRecord.Discount2StCount = null;
                else
                    objZTapeRecord.Discount2StCount = Convert.ToInt32(TxtDiscount2StCount.Text);

                if (String.IsNullOrEmpty(TxtDiscount2StTotal.Text))
                    objZTapeRecord.Discount2StTotal = null;
                else
                    objZTapeRecord.Discount2StTotal = Convert.ToDecimal(TxtDiscount2StTotal.Text);

                if (String.IsNullOrEmpty(TxtChargeCount.Text))
                    objZTapeRecord.ChargeCount = null;
                else
                    objZTapeRecord.ChargeCount = Convert.ToInt32(TxtChargeCount.Text);

                if (String.IsNullOrEmpty(TxtChargeTotal.Text))
                    objZTapeRecord.ChargeTotal = null;
                else
                    objZTapeRecord.ChargeTotal = Convert.ToDecimal(TxtChargeTotal.Text);

                if (String.IsNullOrEmpty(TxtCashCount.Text))
                    objZTapeRecord.CashCount = null;
                else
                    objZTapeRecord.CashCount = Convert.ToInt32(TxtCashCount.Text);

                if (String.IsNullOrEmpty(TxtCashTotal.Text))
                    objZTapeRecord.CashTotal = null;
                else
                    objZTapeRecord.CashTotal = Convert.ToDecimal(TxtCashTotal.Text);

                if (String.IsNullOrEmpty(TxtTax.Text))
                    objZTapeRecord.Tax = null;
                else
                    objZTapeRecord.Tax = Convert.ToDecimal(TxtTax.Text);

                if (String.IsNullOrEmpty(TxtDiscount1ItCount.Text))
                    objZTapeRecord.Discount1ItCount = null;
                else
                    objZTapeRecord.Discount1ItCount = Convert.ToInt32(TxtDiscount1ItCount.Text);

                if (String.IsNullOrEmpty(TxtDiscount1ItTotal.Text))
                    objZTapeRecord.Discount1ItTotal = null;
                else
                    objZTapeRecord.Discount1ItTotal = Convert.ToDecimal(TxtDiscount1ItTotal.Text);

                if (String.IsNullOrEmpty(TxtDiscount2ItCount.Text))
                    objZTapeRecord.Discount2ItCount = null;
                else
                    objZTapeRecord.Discount2ItCount = Convert.ToInt32(TxtDiscount2ItCount.Text);

                if (String.IsNullOrEmpty(TxtDiscount2ItTotal.Text))
                    objZTapeRecord.Discount2ItTotal = null;
                else
                    objZTapeRecord.Discount2ItTotal = Convert.ToDecimal(TxtDiscount2ItTotal.Text);

                if (String.IsNullOrEmpty(TxtReturnsCount.Text))
                    objZTapeRecord.ReturnsCount = null;
                else
                    objZTapeRecord.ReturnsCount = Convert.ToInt32(TxtReturnsCount.Text);

                if (String.IsNullOrEmpty(TxtReturnsTotal.Text))
                    objZTapeRecord.ReturnsTotal = null;
                else
                    objZTapeRecord.ReturnsTotal = Convert.ToDecimal(TxtReturnsTotal.Text);

                if (String.IsNullOrEmpty(TxtReturnsTax.Text))
                    objZTapeRecord.ReturnsTax = null;
                else
                    objZTapeRecord.ReturnsTax = Convert.ToDecimal(TxtReturnsTax.Text);

                if (String.IsNullOrEmpty(TxtFurnitureCount.Text))
                    objZTapeRecord.FurnitureCount = null;
                else
                    objZTapeRecord.FurnitureCount = Convert.ToInt32(TxtFurnitureCount.Text);

                if (String.IsNullOrEmpty(TxtFurnitureTotal.Text))
                    objZTapeRecord.FurnitureTotal = null;
                else
                    objZTapeRecord.FurnitureTotal = Convert.ToDecimal(TxtFurnitureTotal.Text);

                if (String.IsNullOrEmpty(TxtJewelryCount.Text))
                    objZTapeRecord.JewelryCount = null;
                else
                    objZTapeRecord.JewelryCount = Convert.ToInt32(TxtJewelryCount.Text);

                if (String.IsNullOrEmpty(TxtJewelryTotal.Text))
                    objZTapeRecord.JewelryTotal = null;
                else
                    objZTapeRecord.JewelryTotal = Convert.ToDecimal(TxtJewelryTotal.Text);

                if (String.IsNullOrEmpty(TxtElectricalCount.Text))
                    objZTapeRecord.ElectricalCount = null;
                else
                    objZTapeRecord.ElectricalCount = Convert.ToInt32(TxtElectricalCount.Text);

                if (String.IsNullOrEmpty(TxtElectricalTotal.Text))
                    objZTapeRecord.ElectricalTotal = null;
                else
                    objZTapeRecord.ElectricalTotal = Convert.ToDecimal(TxtElectricalTotal.Text);

                if (String.IsNullOrEmpty(TxtWomensCount.Text))
                    objZTapeRecord.WomensCount = null;
                else
                    objZTapeRecord.WomensCount = Convert.ToInt32(TxtWomensCount.Text);

                if (String.IsNullOrEmpty(TxtWomensTotal.Text))
                    objZTapeRecord.WomensTotal = null;
                else
                    objZTapeRecord.WomensTotal = Convert.ToDecimal(TxtWomensTotal.Text);

                if (String.IsNullOrEmpty(TxtBinsCount.Text))
                    objZTapeRecord.BinsCount = null;
                else
                    objZTapeRecord.BinsCount = Convert.ToInt32(TxtBinsCount.Text);

                if (String.IsNullOrEmpty(TxtBinsTotal.Text))
                    objZTapeRecord.BinsTotal = null;
                else
                    objZTapeRecord.BinsTotal = Convert.ToDecimal(TxtBinsTotal.Text);

                if (String.IsNullOrEmpty(TxtMiscCount.Text))
                    objZTapeRecord.MiscCount = null;
                else
                    objZTapeRecord.MiscCount = Convert.ToInt32(TxtMiscCount.Text);

                if (String.IsNullOrEmpty(TxtMiscTotal.Text))
                    objZTapeRecord.MiscTotal = null;
                else
                    objZTapeRecord.MiscTotal = Convert.ToDecimal(TxtMiscTotal.Text);

                if (String.IsNullOrEmpty(TxtShoesCount.Text))
                    objZTapeRecord.ShoesCount = null;
                else
                    objZTapeRecord.ShoesCount = Convert.ToInt32(TxtShoesCount.Text);

                if (String.IsNullOrEmpty(TxtShoesTotal.Text))
                    objZTapeRecord.ShoesTotal = null;
                else
                    objZTapeRecord.ShoesTotal = Convert.ToDecimal(TxtShoesTotal.Text);

                if (String.IsNullOrEmpty(TxtBoutiqueCount.Text))
                    objZTapeRecord.BoutiqueCount = null;
                else
                    objZTapeRecord.BoutiqueCount = Convert.ToInt32(TxtBoutiqueCount.Text);

                if (String.IsNullOrEmpty(TxtBoutiqueTotal.Text))
                    objZTapeRecord.BoutiqueTotal = null;
                else
                    objZTapeRecord.BoutiqueTotal = Convert.ToDecimal(TxtBoutiqueTotal.Text);

                if (String.IsNullOrEmpty(TxtChildsCount.Text))
                    objZTapeRecord.ChildsCount = null;
                else
                    objZTapeRecord.ChildsCount = Convert.ToInt32(TxtChildsCount.Text);

                if (String.IsNullOrEmpty(TxtChildsTotal.Text))
                    objZTapeRecord.ChildsTotal = null;
                else
                    objZTapeRecord.ChildsTotal = Convert.ToDecimal(TxtChildsTotal.Text);

                if (String.IsNullOrEmpty(TxtMensCount.Text))
                    objZTapeRecord.MensCount = null;
                else
                    objZTapeRecord.MensCount = Convert.ToInt32(TxtMensCount.Text);

                if (String.IsNullOrEmpty(TxtMensTotal.Text))
                    objZTapeRecord.MensTotal = null;
                else
                    objZTapeRecord.MensTotal = Convert.ToDecimal(TxtMensTotal.Text);

                if (String.IsNullOrEmpty(TxtBooksCount.Text))
                    objZTapeRecord.BooksCount = null;
                else
                    objZTapeRecord.BooksCount = Convert.ToInt32(TxtBooksCount.Text);

                if (String.IsNullOrEmpty(TxtBooksTotal.Text))
                    objZTapeRecord.BooksTotal = null;
                else
                    objZTapeRecord.BooksTotal = Convert.ToDecimal(TxtBooksTotal.Text);

                if (String.IsNullOrEmpty(TxtNewMerchCount.Text))
                    objZTapeRecord.NewMerchCount = null;
                else
                    objZTapeRecord.NewMerchCount = Convert.ToInt32(TxtNewMerchCount.Text);

                if (String.IsNullOrEmpty(TxtNewMerchTotal.Text))
                    objZTapeRecord.NewMerchTotal = null;
                else
                    objZTapeRecord.NewMerchTotal = Convert.ToDecimal(TxtNewMerchTotal.Text);

                if (String.IsNullOrEmpty(TxtFurnitureCountDisc.Text))
                    objZTapeRecord.FurnitureCountDisc = null;
                else
                    objZTapeRecord.FurnitureCountDisc = Convert.ToInt32(TxtFurnitureCountDisc.Text);

                if (String.IsNullOrEmpty(TxtFurnitureTotalDisc.Text))
                    objZTapeRecord.FurnitureTotalDisc = null;
                else
                    objZTapeRecord.FurnitureTotalDisc = Convert.ToDecimal(TxtFurnitureTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtJewelryCountDisc.Text))
                    objZTapeRecord.JewelryCountDisc = null;
                else
                    objZTapeRecord.JewelryCountDisc = Convert.ToInt32(TxtJewelryCountDisc.Text);

                if (String.IsNullOrEmpty(TxtJewelryTotalDisc.Text))
                    objZTapeRecord.JewelryTotalDisc = null;
                else
                    objZTapeRecord.JewelryTotalDisc = Convert.ToDecimal(TxtJewelryTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtElectricalCountDisc.Text))
                    objZTapeRecord.ElectricalCountDisc = null;
                else
                    objZTapeRecord.ElectricalCountDisc = Convert.ToInt32(TxtElectricalCountDisc.Text);

                if (String.IsNullOrEmpty(TxtElectricalTotalDisc.Text))
                    objZTapeRecord.ElectricalTotalDisc = null;
                else
                    objZTapeRecord.ElectricalTotalDisc = Convert.ToDecimal(TxtElectricalTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtWomensCountDisc.Text))
                    objZTapeRecord.WomensCountDisc = null;
                else
                    objZTapeRecord.WomensCountDisc = Convert.ToInt32(TxtWomensCountDisc.Text);

                if (String.IsNullOrEmpty(TxtWomensTotalDisc.Text))
                    objZTapeRecord.WomensTotalDisc = null;
                else
                    objZTapeRecord.WomensTotalDisc = Convert.ToDecimal(TxtWomensTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtBinsCountDisc.Text))
                    objZTapeRecord.BinsCountDisc = null;
                else
                    objZTapeRecord.BinsCountDisc = Convert.ToInt32(TxtBinsCountDisc.Text);

                if (String.IsNullOrEmpty(TxtBinsTotalDisc.Text))
                    objZTapeRecord.BinsTotalDisc = null;
                else
                    objZTapeRecord.BinsTotalDisc = Convert.ToDecimal(TxtBinsTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtMiscCountDisc.Text))
                    objZTapeRecord.MiscCountDisc = null;
                else
                    objZTapeRecord.MiscCountDisc = Convert.ToInt32(TxtMiscCountDisc.Text);

                if (String.IsNullOrEmpty(TxtMiscTotalDisc.Text))
                    objZTapeRecord.MiscTotalDisc = null;
                else
                    objZTapeRecord.MiscTotalDisc = Convert.ToDecimal(TxtMiscTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtShoesCountDisc.Text))
                    objZTapeRecord.ShoesCountDisc = null;
                else
                    objZTapeRecord.ShoesCountDisc = Convert.ToInt32(TxtShoesCountDisc.Text);

                if (String.IsNullOrEmpty(TxtShoesTotalDisc.Text))
                    objZTapeRecord.ShoesTotalDisc = null;
                else
                    objZTapeRecord.ShoesTotalDisc = Convert.ToDecimal(TxtShoesTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtBoutiqueCountDisc.Text))
                    objZTapeRecord.BoutiqueCountDisc = null;
                else
                    objZTapeRecord.BoutiqueCountDisc = Convert.ToInt32(TxtBoutiqueCountDisc.Text);

                if (String.IsNullOrEmpty(TxtBoutiqueTotalDisc.Text))
                    objZTapeRecord.BoutiqueTotalDisc = null;
                else
                    objZTapeRecord.BoutiqueTotalDisc = Convert.ToDecimal(TxtBoutiqueTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtChildsCountDisc.Text))
                    objZTapeRecord.ChildsCountDisc = null;
                else
                    objZTapeRecord.ChildsCountDisc = Convert.ToInt32(TxtChildsCountDisc.Text);

                if (String.IsNullOrEmpty(TxtChildsTotalDisc.Text))
                    objZTapeRecord.ChildsTotalDisc = null;
                else
                    objZTapeRecord.ChildsTotalDisc = Convert.ToDecimal(TxtChildsTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtMensCountDisc.Text))
                    objZTapeRecord.MensCountDisc = null;
                else
                    objZTapeRecord.MensCountDisc = Convert.ToInt32(TxtMensCountDisc.Text);

                if (String.IsNullOrEmpty(TxtMensTotalDisc.Text))
                    objZTapeRecord.MensTotalDisc = null;
                else
                    objZTapeRecord.MensTotalDisc = Convert.ToDecimal(TxtMensTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtBooksCountDisc.Text))
                    objZTapeRecord.BooksCountDisc = null;
                else
                    objZTapeRecord.BooksCountDisc = Convert.ToInt32(TxtBooksCountDisc.Text);

                if (String.IsNullOrEmpty(TxtBooksTotalDisc.Text))
                    objZTapeRecord.BooksTotalDisc = null;
                else
                    objZTapeRecord.BooksTotalDisc = Convert.ToDecimal(TxtBooksTotalDisc.Text);

                if (String.IsNullOrEmpty(TxtCorrectionCount.Text))
                    objZTapeRecord.CorrectionCount = null;
                else
                    objZTapeRecord.CorrectionCount = Convert.ToInt32(TxtCorrectionCount.Text);

                if (String.IsNullOrEmpty(TxtCorrectionTotal.Text))
                    objZTapeRecord.CorrectionTotal = null;
                else
                    objZTapeRecord.CorrectionTotal = Convert.ToDecimal(TxtCorrectionTotal.Text);

                if (String.IsNullOrEmpty(TxtVoidCount.Text))
                    objZTapeRecord.VoidCount = null;
                else
                    objZTapeRecord.VoidCount = Convert.ToInt32(TxtVoidCount.Text);

                if (String.IsNullOrEmpty(TxtVoidTotal.Text))
                    objZTapeRecord.VoidTotal = null;
                else
                    objZTapeRecord.VoidTotal = Convert.ToDecimal(TxtVoidTotal.Text);

                if (String.IsNullOrEmpty(TxtAllVoidCount.Text))
                    objZTapeRecord.AllVoidCount = null;
                else
                    objZTapeRecord.AllVoidCount = Convert.ToInt32(TxtAllVoidCount.Text);

                if (String.IsNullOrEmpty(TxtAllVoidTotal.Text))
                    objZTapeRecord.AllVoidTotal = null;
                else
                    objZTapeRecord.AllVoidTotal = Convert.ToDecimal(TxtAllVoidTotal.Text);

                // the insert method returns the newly created primary key
                int newlyCreatedPrimaryKey;

                if (operation == "update")
                    objZTapeRecord.Update();
                else
                    newlyCreatedPrimaryKey = objZTapeRecord.Insert();

                Response.Redirect("~/GridViewAddEdit_ZTapeRecord.aspx");
            }
        }
        private void fill_location()
	    {
		    DAL dal = new DAL();	// instanciate the DAL

		    // get list of locations and bind to the location dropdown list
		    ddlLocation.DataSource = dal.LocationsAll;
		    ddlLocation.DataTextField = "locationname";
		    ddlLocation.DataValueField = "locationID";
		    ddlLocation.DataBind();
            if (ddlLocation.Items.Count > 0)
			{
				ddlLocation.SelectedValue = CURRENT_LOCATION;
			}
	    }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
	    {
		    Session["locationID"] = ddlLocation.SelectedValue;		// store the current location value into session
	    }
    }
}


#line default
#line hidden
