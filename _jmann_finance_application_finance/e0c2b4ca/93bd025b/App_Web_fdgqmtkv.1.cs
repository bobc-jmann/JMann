#pragma checksum "D:\JMann\Finance\Application\Finance\GridViewAddEdit_RegisterRecord.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "638EDBDBDF8457FF31D0C649A20E8A8BFFE344E8"

#line 1 "D:\JMann\Finance\Application\Finance\GridViewAddEdit_RegisterRecord.aspx.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace jmann
{
    public partial class GridViewAddEdit_RegisterRecord : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        private DataSet dsEmployees = null;
        private string currRegId = "";
        private string strURL = "";
        protected void Page_Load(object sender, EventArgs e)
        {   CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
          if (!IsPostBack)
          {
                fill_location();				// populate location dropdown
			    get_employees_all();			// get list of all employees
          }
        }
       
        protected void IBtnEdit_Click(object sender, CommandEventArgs e)
        {   
               currRegId = Convert.ToString(e.CommandArgument);
               strURL = "AddEdit_RegisterRecord.aspx?operation=update&registerrecordid=" + currRegId ;
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
                RegisterRecord objRegisterRecord;

                if (operation == "update")
                    objRegisterRecord = jmann.BusinessObject.RegisterRecord.SelectByPrimaryKey(Convert.ToInt32(HfldRegisterRecordID.Value));
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

                // the insert method returns the newly created primary key
                int newlyCreatedPrimaryKey;

                if (operation == "update")
                    objRegisterRecord.Update();
                else
                    newlyCreatedPrimaryKey = objRegisterRecord.Insert();

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
	    private void get_employees_all()
	    {
		    dsEmployees = Session["dsEmployees"] as DataSet;		// get employee data set from session
		    if (dsEmployees == null)								// if null, doesn't exist yet or session lost
		    {
			    DAL dal = new DAL();								// instantiate new DAL
			    dsEmployees = dal.EmployeesAll;					    // get list of employees (all)
			    Session["dsEmployees"] = dsEmployees;				// store into the session
		    }
	    }


	    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
	    {
		    Session["locationID"] = ddlLocation.SelectedValue;		// store the current location value into session
		    fill_employee();	// populate list of employees (based on location)
	    }
        [WebMethod]
        public static RegisterRecord GetRegisterRecord(string registerRecordID)
        {
            return jmann.BusinessObject.RegisterRecord.SelectByPrimaryKey(Convert.ToInt32(registerRecordID));
        }
    }
}


#line default
#line hidden
