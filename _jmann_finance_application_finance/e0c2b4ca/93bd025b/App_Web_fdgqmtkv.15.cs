#pragma checksum "D:\JMann\Finance\Application\Finance\GridViewAddEdit_CreditCardRecord.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C55E7C7207643B5EA300C7E958E3CB9A5A4EB9E4"

#line 1 "D:\JMann\Finance\Application\Finance\GridViewAddEdit_CreditCardRecord.aspx.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace jmann
{
    public partial class GridViewAddEdit_CreditCardRecord : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        private string currCCId = "";
        private string strURL = "";
        protected void Page_Load(object sender, EventArgs e)
        {   CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
          if (!IsPostBack)
          {
                fill_location();				// populate location dropdown
          }
        }
       protected void IBtnEdit_Click(object sender, CommandEventArgs e)
        {   
               currCCId = Convert.ToString(e.CommandArgument);
               strURL = "AddEdit_CreditCardRecord.aspx?operation=update&creditcardrecordid=" + currCCId ;
               Response.Redirect(strURL);
        }
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
                CreditCardRecord objCreditCardRecord;

                if (operation == "update")
                    objCreditCardRecord = jmann.BusinessObject.CreditCardRecord.SelectByPrimaryKey(Convert.ToInt32(HfldCreditCardRecordID.Value));
                else
                {
                    objCreditCardRecord = new CreditCardRecord();
                }

                if (String.IsNullOrEmpty(TxtDate.Text))
                    objCreditCardRecord.Date = null;
                else
                    objCreditCardRecord.Date = Convert.ToDateTime(TxtDate.Text);

                if (String.IsNullOrEmpty(DdlLocationID.SelectedValue))
                    objCreditCardRecord.LocationID = null;
                else
                    objCreditCardRecord.LocationID = Convert.ToInt32(DdlLocationID.SelectedValue);

                if (String.IsNullOrEmpty(TxtAtmCount.Text))
                    objCreditCardRecord.AtmCount = null;
                else
                    objCreditCardRecord.AtmCount = Convert.ToInt32(TxtAtmCount.Text);

                if (String.IsNullOrEmpty(TxtAtmTotal.Text))
                    objCreditCardRecord.AtmTotal = null;
                else
                    objCreditCardRecord.AtmTotal = Convert.ToDecimal(TxtAtmTotal.Text);

                if (String.IsNullOrEmpty(TxtVisaCount.Text))
                    objCreditCardRecord.VisaCount = null;
                else
                    objCreditCardRecord.VisaCount = Convert.ToInt32(TxtVisaCount.Text);

                if (String.IsNullOrEmpty(TxtVisaTotal.Text))
                    objCreditCardRecord.VisaTotal = null;
                else
                    objCreditCardRecord.VisaTotal = Convert.ToDecimal(TxtVisaTotal.Text);

                if (String.IsNullOrEmpty(TxtMastercardCount.Text))
                    objCreditCardRecord.MastercardCount = null;
                else
                    objCreditCardRecord.MastercardCount = Convert.ToInt32(TxtMastercardCount.Text);

                if (String.IsNullOrEmpty(TxtMastercardTotal.Text))
                    objCreditCardRecord.MastercardTotal = null;
                else
                    objCreditCardRecord.MastercardTotal = Convert.ToDecimal(TxtMastercardTotal.Text);

                if (String.IsNullOrEmpty(TxtDiscoverCount.Text))
                    objCreditCardRecord.DiscoverCount = null;
                else
                    objCreditCardRecord.DiscoverCount = Convert.ToInt32(TxtDiscoverCount.Text);

                if (String.IsNullOrEmpty(TxtDiscoverTotal.Text))
                    objCreditCardRecord.DiscoverTotal = null;
                else
                    objCreditCardRecord.DiscoverTotal = Convert.ToDecimal(TxtDiscoverTotal.Text);

                // the insert method returns the newly created primary key
                int newlyCreatedPrimaryKey;

                if (operation == "update")
                    objCreditCardRecord.Update();
                else
                    newlyCreatedPrimaryKey = objCreditCardRecord.Insert();

                GridView1.DataBind();
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
        [WebMethod]
        public static CreditCardRecord GetCreditCardRecord(string creditCardRecordID)
        {
            return jmann.BusinessObject.CreditCardRecord.SelectByPrimaryKey(Convert.ToInt32(creditCardRecordID));
        }
    }
}


#line default
#line hidden
