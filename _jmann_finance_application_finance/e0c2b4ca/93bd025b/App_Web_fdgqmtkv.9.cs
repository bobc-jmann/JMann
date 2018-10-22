#pragma checksum "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4B7BBD175DEC4D9E952A1FE02E833E22AF7BFF51"

#line 1 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx.cs"
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using HtmlPivot; //From the referenced Pivot.dll
using jmann.DataLayer;
using jmann.BusinessObject;

public partial class _Default : System.Web.UI.Page 
{   private string CURRENT_LOCATION = "0";	// create variable to hold current location
    private string PIVOT_DATE = DateTime.Today.ToString("yyyyMMdd");
    protected void Page_Load(object sender, EventArgs e)
    {  CURRENT_LOCATION = Globals.isNull(Session["locationID"], "0");		// get current location from session, default "0" if null
       PIVOT_DATE = Globals.isNull(Session["pivotDate"], DateTime.Today.ToString("yyyyMMdd"));
        //Advanced Pivot 
         if (!IsPostBack)
            {   //TxtDate.Text = Convert.ToString(Session["pivotDate"]).Substring(4,2)  + "/" + Convert.ToString(Session["pivotDate"]).Substring(6,2)  + "/" + Convert.ToString(Session["pivotDate"]).Substring(0,4); //  load session date 
                 TxtDate.Text = PIVOT_DATE.Substring(4,2) + "/" +  PIVOT_DATE.Substring(6,2)  + "/" +  PIVOT_DATE.Substring(0,4);
                fill_location();					// populate location dropdown
                Pivot advPivot = new Pivot(registerRecordForPivot );
        //override default style with css
 //       advPivot.CssTopHeading = "Heading";
 //       advPivot.CssLeftColumn = "LeftColumn";
 //       advPivot.CssItems = "Items";
 //       advPivot.CssTotals = "Totals";
 //       advPivot.CssTable = "Table";
       
               HtmlTable advancedPivot = advPivot.PivotTable("empName", "measureName", new string[] { "measureValue"});
               div1.Controls.Add(advancedPivot);

        //Simple Pivot
//        Pivot pivot = new Pivot(registerRecordForPivot );


//        HtmlTable simplePivot = pivot.PivotTable("empName", "measureName", "measureValue");
//        div2.Controls.Add(simplePivot);
          }
    }
    public DataTable registerRecordForPivot
    {
        get
        {
            string storedProcName = "usp_registerrecord_filtered" ;
            SqlConnection connection = Dbase.GetConnection();
            SqlCommand command = Dbase.GetCommand(storedProcName, connection);
             // parameters
            command.Parameters.AddWithValue("@location_id", CURRENT_LOCATION );
            command.Parameters.AddWithValue("@regDate", PIVOT_DATE );
            DataSet ds = Dbase.GetDbaseDataSet(command);
            DataTable dt = ds.Tables[0];
            return dt;
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
				ddlLocation.SelectedValue = Convert.ToString(CURRENT_LOCATION);
			}
	    }
	    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
	    {
		    Session["locationID"] = ddlLocation.SelectedValue;		// store the current location value into session
//		    fill_employee();																		// populate list of employees (based on location)
	    }
        protected void txtDate_TextChanged(object sender, EventArgs e)
	    {
		    Session["pivotDate"] = Convert.ToDateTime(TxtDate.Text).ToString("yyyyMMdd");		// store the date into session variable
            Response.Redirect(Request.RawUrl);
														
	    }
}

#line default
#line hidden
