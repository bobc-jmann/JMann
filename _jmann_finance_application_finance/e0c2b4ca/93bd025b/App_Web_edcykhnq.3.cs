#pragma checksum "D:\JMann\Finance\Application\Finance\sys_ReportServer.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A96FC67D2FB8A4273FF004DD738FC3D0C67D2470"

#line 1 "D:\JMann\Finance\Application\Finance\sys_ReportServer.aspx.cs"
using System;
using System.Data;
using System.Reflection;
using System.Xml;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Web.UI;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using jmann.ReportServer;

namespace jmann

{
   public partial class sys_ReportServer : System.Web.UI.Page 
   { 
	  protected void Page_Load(object sender, EventArgs e)
      { 
            if ((! IsPostBack))
            {  rptViewer.ProcessingMode = ProcessingMode.Remote;
            
            // string parms = Request.QueryString("PARMS");
                rptViewer.ServerReport.ReportServerCredentials = new jmann.ReportServer.ReportServerConnection.MyReportServerCredentials();
                rptViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
                rptViewer.ServerReport.ReportPath = (string)Request["RPTPATH"];
                rptViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerURL"]);
            //    rptViewer.ServerReport.Refresh();
           //     string[] p = parms.Split(new char [] {"|"});
           //     if (p(0) !=  "")
           //     {
           //         Generic.List paramList = new Generic.List(Of ReportParameter);
           //         ;
           //     }

            }

      }
    }
        /// <summary>
    /// This IReportServerConnection2 implementation is specified in the project's Web.config file. At run time, the ReportViewer control uses
    /// the connection information specified in this class instead of the controls' public properties, and does not store the information
    /// in the ASP.NET session.
    /// </summary>
//    [Serializable]
//    public sealed class MyReportServerConnection : IReportServerConnection2
//    {
//        public WindowsIdentity ImpersonationUser
//        {
//            get
//            {
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.
//                return null;
//            }
//        }

//        public ICredentials NetworkCredentials
//        {
//            get
//            {
                // Read the user information from the web.config file. By reading the information on demand instead of 
                // storing it, the credentials will not be stored in session, reducing the vulnerable surface area to the
                // web.config file, which can be secured with an ACL.

                // User name
 //               string userName = ConfigurationManager.AppSettings["MyReportViewerUser"];

//                if (string.IsNullOrEmpty(userName))
//                    throw new InvalidOperationException("Please specify the user name in the project's Web.config file.");

                // Password
//                string password = ConfigurationManager.AppSettings["MyReportViewerPassword"];

//                if (string.IsNullOrEmpty(password))
//                    throw new InvalidOperationException("Please specify the password in the project's Web.config file");

                // Domain
//                string domain = ConfigurationManager.AppSettings["MyReportViewerDomain"];

//                if (string.IsNullOrEmpty(domain))
//                    throw new InvalidOperationException("Please specify the domain in the project's Web.config file");

//                return new NetworkCredential(userName, password, domain);
//            }
//        }

//        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
//        {
//            authCookie = null;
//            userName = null;
//            password = null;
//            authority = null;

            // Not using form credentials
//            return false;
//        }

//        public Uri ReportServerUrl
//        {
//            get
//            {
//                string url = ConfigurationManager.AppSettings["MyReportServerUrl"];

//                if (string.IsNullOrEmpty(url))
//                    throw new InvalidOperationException("Please specify the report server URL in the project's Web.config file");

//                return new Uri(url);
//            }
//        }

//        public int Timeout
//        {
//            get
//            {
//                return 60000; // 60 seconds
//            }
//        }

//        public IEnumerable<Cookie> Cookies
//        {
//            get
//            {
//                // No custom cookies
//                return null;
//            }
//        }

//        public IEnumerable<string> Headers
//        {
//            get
//            {
                // No custom headers
//                return null;
//            }
//        }
//    }
}


#line default
#line hidden
