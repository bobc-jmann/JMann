#pragma checksum "D:\JMann\Finance\Application\Finance\AddEdit_RegisterRecord.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4404D654E46956786E3246ED52605219C90FCD54"

#line 1 "D:\JMann\Finance\Application\Finance\AddEdit_RegisterRecord.aspx.cs"
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
    public partial class AddEdit_RegisterRecord : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        private string CURRENT_DATE ;    // create variable to hold current date
        private DataSet dsEmployees = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {   CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
            CURRENT_DATE = Globals.isNull(Session["currdate"], DateTime.Now.ToShortDateString());		// get current date from session, default "0" if null
           

            if (!IsPostBack)
            {
                fill_location();					// populate location dropdown 
                get_employees_all();                   // get list of all employees
		        fill_employee();	                // populate employee dropdown 
                fill_date();                        // load txtDate with last used date                 
                string operation = (string)Request["operation"];

                if (!String.IsNullOrEmpty(operation))
                {
                    if (operation == "update")
                    {
                        LblTitle.Text = "Edit RegisterRecord";
                        BtnUpdateRecord.Visible = true;
                        BtnAddRecord.Visible = false;

                        // retrieve id(s) to be updated
                        string registerRecordID = (string)Request["registerrecordid"];

                        if (!String.IsNullOrEmpty(registerRecordID))
                        {
                            // retrieve record to be updated
                            RegisterRecord objRegisterRecord = jmann.BusinessObject.RegisterRecord.SelectByPrimaryKey(Convert.ToInt32(registerRecordID));

                            if (objRegisterRecord != null)
                            {  // get_employees_all ();
                               // fill_employee();
                                TxtRegisterRecordID.Enabled = false;
                                TxtRegisterRecordID.Text = objRegisterRecord.RegisterRecordID.ToString();
                                ddlEmployee.SelectedValue = objRegisterRecord.EmployeeID.ToString();
                                ddlLocation.SelectedValue = objRegisterRecord.LocationID.ToString();
                                TxtDate.Text = String.Format("{0:M/d/yyyy}",Convert.ToDateTime(objRegisterRecord.Date.ToString()));
   
                                if(objRegisterRecord.Register != null)
                                    TxtRegister.Text = objRegisterRecord.Register.ToString();

                                if(objRegisterRecord.Record != null)
                                    TxtRecord.Text = objRegisterRecord.Record.ToString();

                                if(objRegisterRecord.CustomerCount != null)
                                    TxtCustomerCount.Text = objRegisterRecord.CustomerCount.ToString();

                                if(objRegisterRecord.Coins != null)

                                    TxtCoins.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.Coins.ToString()));
                  
                                if(objRegisterRecord.Currency != null)
                                    TxtCurrency.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.Currency.ToString()));

                                if(objRegisterRecord.MiscCash != null)
                                    TxtMiscCash.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.MiscCash.ToString()));

                                if(objRegisterRecord.Visa != null)
                                    TxtVisa.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.Visa.ToString()));

                                if(objRegisterRecord.Mastercard != null)
                                    TxtMastercard.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.Mastercard.ToString()));

                                if(objRegisterRecord.Discover != null)
                                    TxtDiscover.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.Discover.ToString()));

                                if(objRegisterRecord.Atm != null)
                                    TxtAtm.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.Atm.ToString()));

                                if(objRegisterRecord.GiftCertificate != null)
                                    TxtGiftCertificate.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.GiftCertificate.ToString()));
 
                                if(objRegisterRecord.ZTapeCash != null)
                                    TxtZTapeCash.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.ZTapeCash.ToString()));

                                if(objRegisterRecord.ZTapeCharge != null)
                                    TxtZTapeCharge.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.ZTapeCharge.ToString()));

                                if(objRegisterRecord.ZTapeTotal != null)
                                    TxtZTape.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.ZTapeTotal.ToString()));

                                if(objRegisterRecord.Overring != null)
                                    TxtOverring.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.Overring.ToString()));

                                if(objRegisterRecord.OverringCount != null)
                                    TxtOverringCount.Text = String.Format ("{0:0}",Convert.ToDecimal(objRegisterRecord.OverringCount.ToString()));
 
                                if(objRegisterRecord.ActualCashOverShort != null)
                                    TxtActualCashOverShort.Text = String.Format ("{0:0.00}",Convert.ToDecimal(objRegisterRecord.ActualCashOverShort.ToString()));

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
                RegisterRecord objRegisterRecord;

                if (operation == "update")
                    objRegisterRecord = jmann.BusinessObject.RegisterRecord.SelectByPrimaryKey(Convert.ToInt32(TxtRegisterRecordID.Text));
                else
                {
                    objRegisterRecord = new RegisterRecord();
                }

                if (String.IsNullOrEmpty(TxtRegister.Text))
                    objRegisterRecord.Register = null;
                else
                    objRegisterRecord.Register = Convert.ToInt32(TxtRegister.Text);

                if (String.IsNullOrEmpty(TxtRecord.Text))
                    objRegisterRecord.Record = null;
                else
                    objRegisterRecord.Record = Convert.ToInt32(TxtRecord.Text);

                if (String.IsNullOrEmpty(ddlEmployee.SelectedValue))
                    objRegisterRecord.EmployeeID = null;
                else
                    objRegisterRecord.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);

                if (String.IsNullOrEmpty(ddlLocation.SelectedValue))
                    objRegisterRecord.LocationID = null;
                else
                    objRegisterRecord.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);

                if (String.IsNullOrEmpty(TxtDate.Text))
                    objRegisterRecord.Date = null;
                else
                    objRegisterRecord.Date = Convert.ToDateTime(TxtDate.Text);

                if (String.IsNullOrEmpty(TxtCustomerCount.Text))
                    objRegisterRecord.CustomerCount = null;
                else
                    objRegisterRecord.CustomerCount = Convert.ToInt32(TxtCustomerCount.Text);

                if (String.IsNullOrEmpty(TxtCoins.Text))
                    objRegisterRecord.Coins = null;
                else
                    objRegisterRecord.Coins = Convert.ToDecimal(TxtCoins.Text);

                if (String.IsNullOrEmpty(TxtCurrency.Text))
                    objRegisterRecord.Currency = null;
                else
                    objRegisterRecord.Currency = Convert.ToDecimal(TxtCurrency.Text);

                if (String.IsNullOrEmpty(TxtMiscCash.Text))
                    objRegisterRecord.MiscCash = null;
                else
                    objRegisterRecord.MiscCash = Convert.ToDecimal(TxtMiscCash.Text);

                if (String.IsNullOrEmpty(TxtVisa.Text))
                    objRegisterRecord.Visa = null;
                else
                    objRegisterRecord.Visa = Convert.ToDecimal(TxtVisa.Text);

                if (String.IsNullOrEmpty(TxtMastercard.Text))
                    objRegisterRecord.Mastercard = null;
                else
                    objRegisterRecord.Mastercard = Convert.ToDecimal(TxtMastercard.Text);

                if (String.IsNullOrEmpty(TxtDiscover.Text))
                    objRegisterRecord.Discover = null;
                else
                    objRegisterRecord.Discover = Convert.ToDecimal(TxtDiscover.Text);

                if (String.IsNullOrEmpty(TxtAtm.Text))
                    objRegisterRecord.Atm = null;
                else
                    objRegisterRecord.Atm = Convert.ToDecimal(TxtAtm.Text);

                if (String.IsNullOrEmpty(TxtGiftCertificate.Text))
                    objRegisterRecord.GiftCertificate = null;
                else
                    objRegisterRecord.GiftCertificate = Convert.ToDecimal(TxtGiftCertificate.Text);

                if (String.IsNullOrEmpty(TxtZTapeCash.Text))
                    objRegisterRecord.ZTapeCash = null;
                else
                    objRegisterRecord.ZTapeCash = Convert.ToDecimal(TxtZTapeCash.Text);

                if (String.IsNullOrEmpty(TxtZTapeCharge.Text))
                    objRegisterRecord.ZTapeCharge = null;
                else
                    objRegisterRecord.ZTapeCharge = Convert.ToDecimal(TxtZTapeCharge.Text);    
                                    
                if (String.IsNullOrEmpty(TxtZTape.Text))
                    objRegisterRecord.ZTapeTotal = null;
                else
                    objRegisterRecord.ZTapeTotal = Convert.ToDecimal(TxtZTape.Text);

                if (String.IsNullOrEmpty(TxtOverring.Text))
                    objRegisterRecord.Overring = null;
                else
                    objRegisterRecord.Overring = Convert.ToDecimal(TxtOverring.Text);

                if (String.IsNullOrEmpty(TxtOverringCount.Text))
                    objRegisterRecord.OverringCount = null;
                else
                    objRegisterRecord.OverringCount = Convert.ToInt32(TxtOverringCount.Text);

                if (String.IsNullOrEmpty(TxtActualCashOverShort.Text))
                    objRegisterRecord.ActualCashOverShort = null;
                else
                    objRegisterRecord.ActualCashOverShort = Convert.ToDecimal(TxtActualCashOverShort.Text);

                // the insert method returns the newly created primary key
                int newlyCreatedPrimaryKey;

                if (operation == "update")
                    objRegisterRecord.Update();
                else
                    newlyCreatedPrimaryKey = objRegisterRecord.Insert();

                Response.Redirect("~/AddEdit_RegisterRecord.aspx");
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
	    private void fill_employee()
	    {
		    ddlEmployee.Items.Clear();	// clear employee list
		    if (dsEmployees != null)		// check that employee data set is not null
		    {
			    // get the default view and filter by location
			    DataView dv = dsEmployees.Tables[0].DefaultView;
			    dv.RowFilter = string.Format(@"locationID='{0}'", ddlLocation.SelectedValue);
			
			    // bind the Employee dropdown list to the filtered employee view
			    ddlEmployee.DataSource = dv;
			    ddlEmployee.DataValueField = "employeeID";
			    ddlEmployee.DataTextField = "name";
			    ddlEmployee.DataBind();
		    }
		
	    }
        private void fill_date()
        {
            TxtDate.Text = CURRENT_DATE;
        }

	    private void get_employees_all()
	    {
		    dsEmployees = Session["dsEmployees"] as DataSet;		// get employee data set from session
//		    if (dsEmployees == null)								// if null, doesn't exist yet or session lost
//		    {
			    DAL dal = new DAL();								// instantiate new DAL
			    dsEmployees = dal.EmployeesAll;						// get list of employees (all)
			    Session["dsEmployees"] = dsEmployees;				// store into the session

	    }


	    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
	    {
		    Session["locationID"] = ddlLocation.SelectedValue;		// store the current location value into session
		    get_employees_all();
            fill_employee();										// populate list of employees (based on location)
	    }
        
        protected void txtDate_Changed(object sender, EventArgs e)
	    {
		    Session["currdate"] = TxtDate.Text;		                    // store the current date value into session
	    }

    }
}


#line default
#line hidden
