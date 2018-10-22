#pragma checksum "D:\JMann\Finance\Application\Finance\MasterPage.master.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "656BB541412FC651F4744C15E51F1E82EA3A254C"

#line 1 "D:\JMann\Finance\Application\Finance\MasterPage.master.cs"
using System;
using System.Web.UI.WebControls; 
using jmann.DataLayer; 
using jmann.BusinessObject;
using System.Configuration;

namespace jmann
{
    public static class MyGlobals
    {
        public static string userName;
    }

    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string connectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
            connectionString = rootWebConfig.ConnectionStrings.ConnectionStrings["App-MainConnectionString"].ConnectionString;

            string un = System.Web.HttpContext.Current.Request.QueryString["user"];
            if (un != "" && un != null)
                MyGlobals.userName = un;
			// Enabled for everyone 3/11/15
            //if (MyGlobals.userName == "admin" || MyGlobals.userName == "kGouveia" || MyGlobals.userName == "gZabala")
            //    TOS.Visible = true;
        }

        protected void RunDailyRegisterByLocation_Click(Object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.open('sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/RegisterReportByLocation','_blank');</script>");
        }

 
     protected void RunCashierReport_Click(Object sender, EventArgs e)
        {
    //      Client_NewWindow("sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/CashierReport");
  //          Response.Redirect("~/sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/CashierReport");
           Response.Write("<script type='text/javascript'>window.open('sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/CashierReport','_blank');</script>");
        }

        protected void RunFinanceSummary_Click(Object sender, EventArgs e)
        {
    //      Client_NewWindow("sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/CashierReport");
  //          Response.Redirect("~/sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/CashierReport");
           Response.Write("<script type='text/javascript'>window.open('sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/FinanceSummary','_blank');</script>");
        }
        protected void RunWeeklySalesComparison_Click(Object sender, EventArgs e)
        {
           Response.Write("<script type='text/javascript'>window.open('sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/WeeklySalesComparison','_blank');</script>");
        }
        protected void RunCartReport_Click(Object sender, EventArgs e)
        {
           Response.Write("<script type='text/javascript'>window.open('sys_ReportServer.aspx?RPTPATH=/Finance/Production Reporting/CartReport','_blank');</script>");
        }
/*        public void Client_NewWindow(string sURL)
        {
          string str;
          string nl = System.Environment.NewLine;
//          System.Web.UI.Page P = CType(System.Web.HttpContext.Current.Handler, System.Web.UI.Page);
          System.Web.UI.Page P = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler; 
           str = nl + "<script language=javascript>"+ nl;
          if (sURL.Length >  0)
          {
            str = str+ "    window.open('"+ sURL+ "','_blank');"+ nl;
            str = str+ "</script>"+ nl;
            P.ClientScript.RegisterStartupScript(P.GetType, "", str);
          }

         }
*/
    }
}


#line default
#line hidden
