#pragma checksum "D:\JMann\Finance\Application\Finance\AddEdit_CreditCardRecord.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A0CFCC549E396118F60D1807790CE7A2D39BF633"

#line 1 "D:\JMann\Finance\Application\Finance\AddEdit_CreditCardRecord.aspx.cs"
using System;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;

namespace jmann
{
    public partial class AddEdit_CreditCardRecord : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        private string CURRENT_DATE ;    // create variable to hold current date
        protected void Page_Load(object sender, EventArgs e)
        {   CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
            CURRENT_DATE = Globals.isNull(Session["currdate"], DateTime.Now.ToShortDateString());		// get current date from session, default "0" if null
            if (!IsPostBack)
            {   fill_location();					// populate location dropdown
                string operation = (string)Request["operation"];

                if (!String.IsNullOrEmpty(operation))
                {
                    if (operation == "update")
                    {
                        LblTitle.Text = "Edit CreditCardRecord";
                        BtnUpdateRecord.Visible = true;
                        BtnAddRecord.Visible = false;

                        // retrieve id(s) to be updated
                        string creditCardRecordID = (string)Request["creditcardrecordid"];

                        if (!String.IsNullOrEmpty(creditCardRecordID))
                        {
                            // retrieve record to be updated
                            CreditCardRecord objCreditCardRecord = jmann.BusinessObject.CreditCardRecord.SelectByPrimaryKey(Convert.ToInt32(creditCardRecordID));

                            if (objCreditCardRecord != null)
                            {
                                TxtCreditCardRecordID.Enabled = false;
                                TxtCreditCardRecordID.Text = objCreditCardRecord.CreditCardRecordID.ToString();
                                ddlLocation.SelectedValue = objCreditCardRecord.LocationID.ToString();

                                if(objCreditCardRecord.Date != null)
                                    TxtDate.Text = String.Format("{0:M/d/yyyy}",Convert.ToDateTime(objCreditCardRecord.Date.ToString()));

                                if(objCreditCardRecord.AtmCount != null)
                                    TxtAtmCount.Text = objCreditCardRecord.AtmCount.ToString();

                                if(objCreditCardRecord.AtmTotal != null)
                                    TxtAtmTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objCreditCardRecord.AtmTotal.ToString()));
                                     
                                if(objCreditCardRecord.VisaCount != null)
                                    TxtVisaCount.Text = objCreditCardRecord.VisaCount.ToString();

                                if(objCreditCardRecord.VisaTotal != null)
                                    TxtVisaTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objCreditCardRecord.VisaTotal.ToString()));

                                if(objCreditCardRecord.MastercardCount != null)
                                    TxtMastercardCount.Text = objCreditCardRecord.MastercardCount.ToString();

                                if(objCreditCardRecord.MastercardTotal != null)
                                    TxtMastercardTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objCreditCardRecord.MastercardTotal.ToString()));

                                if(objCreditCardRecord.DiscoverCount != null)
                                    TxtDiscoverCount.Text = objCreditCardRecord.DiscoverCount.ToString();

                                if(objCreditCardRecord.DiscoverTotal != null)
                                    TxtDiscoverTotal.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objCreditCardRecord.DiscoverTotal.ToString()));
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
                CreditCardRecord objCreditCardRecord;

                if (operation == "update")
                    objCreditCardRecord = jmann.BusinessObject.CreditCardRecord.SelectByPrimaryKey(Convert.ToInt32(TxtCreditCardRecordID.Text));
                else
                {
                    objCreditCardRecord = new CreditCardRecord();
                }

                if (String.IsNullOrEmpty(TxtDate.Text))
                    objCreditCardRecord.Date = null;
                else
                    objCreditCardRecord.Date = Convert.ToDateTime(TxtDate.Text);

                if (String.IsNullOrEmpty(ddlLocation.SelectedValue))
                    objCreditCardRecord.LocationID = null;
                else
                    objCreditCardRecord.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);

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

                Response.Redirect("~/GridViewAddEdit_CreditCardRecord.aspx");
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
        private void fill_date()
        {
            TxtDate.Text = CURRENT_DATE;
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
	    {
		    Session["locationID"] = ddlLocation.SelectedValue;		// store the current location value into session
	    }
        protected void txtDate_Changed(object sender, EventArgs e)
	    {
		    Session["currdate"] = TxtDate.Text;		                    // store the current date value into session
	    }
    }
}


#line default
#line hidden
