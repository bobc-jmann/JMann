#pragma checksum "D:\JMann\Finance\Application\Finance\ThriftOS_Update.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "922AFC2509A50BB4362543FBE058F9E67313A9D6"

#line 1 "D:\JMann\Finance\Application\Finance\ThriftOS_Update.aspx.cs"
using System;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using jmann.BusinessObject;
using System.Web.Services;

namespace jmann
{
    public partial class ThriftOS_Update : System.Web.UI.Page
    {
        private string CURRENT_LOCATION = "0";	// create variable to hold current location
        protected void Page_Load(object sender, EventArgs e)
        {
            CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
            if (!IsPostBack)
            {
                fill_location();				// populate location dropdown
            }
        }

        private void fill_location()
        {
            DAL dal = new DAL();	// instantiate the DAL

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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblDone.Visible = false;
            lblError.Visible = false;

            if (ddlLocation.SelectedIndex == 0 || TxtDate.Text == "")
            {
                lblNoLocationOrDate.Visible = true;
                dsEmployeeIssues.SelectParameters["locationID"].DefaultValue = "0";
                dsEmployeeIssues.SelectParameters["date"].DefaultValue = "1/1/2000";
                return;
            }
            else
            {
                lblNoLocationOrDate.Visible = false;
                dsEmployeeIssues.SelectParameters["locationID"].DefaultValue = ddlLocation.SelectedValue;
                dsEmployeeIssues.SelectParameters["date"].DefaultValue = TxtDate.Text;
            }
			try
			{
				GridView1.DataBind();
			}
			catch (SqlException ex)
			{
				lblError.Visible = true;
				lblError.Text = ex.Message.ToString();
				return;
			}

			if (GridView1.Rows.Count > 0)
			{
				GridView1.Visible = true;
				gridMessage1.Visible = true;
				gridMessage2.Visible = true;
			}
			else
			{
				GridView1.Visible = false;
				gridMessage1.Visible = false;
				gridMessage2.Visible = false;

                try
                {
                    SqlConnection conn = jmann.DataLayer.Dbase.GetConnection();
                    SqlCommand cmd = new SqlCommand("spThriftOS_Update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@locationID", SqlDbType.Int).Value = ddlLocation.SelectedValue;
                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = TxtDate.Text;
                    SqlParameter retval = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                    retval.Direction = ParameterDirection.ReturnValue;
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();
                    int returnvalue = (int)cmd.Parameters["@RETURN_VALUE"].Value;
                    conn.Close();

                    if (returnvalue == 0)
                        lblDone.Text = TxtDate.Text + " update complete!";
                    else
                        lblDone.Text = TxtDate.Text + " has been updated previously!";

                    lblDone.Visible = true;

                }
                catch (SqlException ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.Message.ToString();
                }
			}
        }
    }
}


#line default
#line hidden
