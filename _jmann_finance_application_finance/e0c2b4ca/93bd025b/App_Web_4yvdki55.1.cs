#pragma checksum "D:\JMann\Finance\Application\Finance\GridViewAddEdit_ZTapeRecord.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4BB52BF4FF29988F2403DDB223A74E4A31823DEC"

#line 1 "D:\JMann\Finance\Application\Finance\GridViewAddEdit_ZTapeRecord.aspx.cs"
using System;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;
using System.Web.UI.WebControls;
namespace jmann
{
    public partial class GridViewAddEdit_ZTapeRecord : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        private string currZTapeId = "";
        private string strURL = "";

        protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            Functions.GridViewRowDataBound(sender, e, 1);
        }

        protected void GridView1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            Functions.GridViewRowCreated(sender, e, 1);
        }

        protected void ObjectDataSource1_Deleted(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
        {
            Functions.ObjectDataSourceDeleted(sender, e, this.Page);
        }
       protected void IBtnEdit_Click(object sender, CommandEventArgs e)
        {   
               currZTapeId = Convert.ToString(e.CommandArgument);
               strURL = "AddEdit_ZTapeRecord.aspx?operation=update&ztaperecordid=" + currZTapeId ;
               Response.Redirect(strURL);
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
                    objZTapeRecord = jmann.BusinessObject.ZTapeRecord.SelectByPrimaryKey(Convert.ToInt32(HfldZTapeRecordID.Value));
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

                GridView1.DataBind();
            }
        }

        [WebMethod]
        public static ZTapeRecord GetZTapeRecord(string zTapeRecordID)
        {
            return jmann.BusinessObject.ZTapeRecord.SelectByPrimaryKey(Convert.ToInt32(zTapeRecordID));
        }
        protected void Page_Load(object sender, EventArgs e)
        { CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
          if (!IsPostBack)
          {
                fill_location();				// populate location dropdown
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
