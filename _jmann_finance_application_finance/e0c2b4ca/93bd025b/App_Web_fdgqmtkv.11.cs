#pragma checksum "D:\JMann\Finance\Application\Finance\GridView_Carts.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54ED9353D7E93D3EA7069C198D994C5C962E2CE0"

#line 1 "D:\JMann\Finance\Application\Finance\GridView_Carts.aspx.cs"
using System;
using System.Web.UI;
using jmann.BusinessObject;
using System.Web.Services;

namespace jmann
{
    public partial class GridView_Carts : System.Web.UI.Page
    {   private string CURRENT_LOCATION = "0";	// create variable to hold current location
        protected void Page_Load(object sender, EventArgs e)
        {   CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
          if (!IsPostBack)
          {
                fill_location();				// populate location dropdown
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
