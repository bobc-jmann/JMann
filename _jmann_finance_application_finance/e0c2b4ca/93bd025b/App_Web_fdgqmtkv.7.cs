#pragma checksum "D:\JMann\Finance\Application\Finance\GridViewAddEdit_Employee.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F39FD2F3E5CBA2EE09DE4A2B8845FDDE530F5D3F"

#line 1 "D:\JMann\Finance\Application\Finance\GridViewAddEdit_Employee.aspx.cs"
using System;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;

namespace jmann
{
    public partial class GridViewAddEdit_Employee : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        protected void Page_Load(object sender, EventArgs e)
        {   CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
          if (!IsPostBack)
          {
                fill_location();				// populate location dropdown
	//		    get_employees_all();			// get list of all employees
          }
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
                Employee objEmployee;

                if (operation == "update")
                    objEmployee = jmann.BusinessObject.Employee.SelectByPrimaryKey(Convert.ToInt32(HfldEmployeeID.Value));
                else
                {
                    objEmployee = new Employee();
                }

                objEmployee.Expired = CbxExpired.Checked;

                if (String.IsNullOrEmpty(TxtDepartment.Text))
                    objEmployee.Department = null;
                else
                    objEmployee.Department = Convert.ToInt32(TxtDepartment.Text);

                if (String.IsNullOrEmpty(TxtName.Text))
                    objEmployee.Name = null;
                else
                    objEmployee.Name = TxtName.Text;

  //              if (String.IsNullOrEmpty(TxtLocationID.Text))
  //                  objEmployee.LocationID = null;
  //              else
  //                  objEmployee.LocationID = Convert.ToInt32(TxtLocationID.Text);

                objEmployee.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);

                if (String.IsNullOrEmpty(TxtLastName.Text))
                    objEmployee.LastName = null;
                else
                    objEmployee.LastName = TxtLastName.Text;

                if (String.IsNullOrEmpty(TxtFirstName.Text))
                    objEmployee.FirstName = null;
                else
                    objEmployee.FirstName = TxtFirstName.Text;

                // the insert method returns the newly created primary key
                int newlyCreatedPrimaryKey;

                if (operation == "update")
                    objEmployee.Update();
                else
                    newlyCreatedPrimaryKey = objEmployee.Insert();

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
        public static Employee GetEmployee(string employeeID)
        {
            return jmann.BusinessObject.Employee.SelectByPrimaryKey(Convert.ToInt32(employeeID));
        }
    }
}


#line default
#line hidden
