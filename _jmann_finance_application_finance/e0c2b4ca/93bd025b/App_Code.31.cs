#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Helper\ReportServerConnection.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A9A129DB02AE74C9AC162ACA4F68826096D160BE"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Helper\ReportServerConnection.cs"
using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

namespace jmann.ReportServer 
{
  public class ReportServerConnection
  {    

    [Serializable]
    public sealed class MyReportServerCredentials : 
      IReportServerCredentials
    {
      public WindowsIdentity ImpersonationUser
      {
          get
          {
            // Use the default Windows user.  Credentials will be
            // provided by the NetworkCredentials property.
            return null;
          }
      }

      public ICredentials NetworkCredentials
      {
          get
          {
            // Read the user information from the Web.config file.  
            // By reading the information on demand instead of 
            // storing it, the credentials will not be stored in 
            // session, reducing the vulnerable surface area to the
            // Web.config file, which can be secured with an ACL.

            // User name
            string userName = 
                ConfigurationManager.AppSettings
                    ["ReportViewerUser"];

            if (string.IsNullOrEmpty(userName))
                throw new Exception(
                    "Missing user name from web.config file");

            // Password
            string password = 
                ConfigurationManager.AppSettings
                    ["ReportViewerPassword"];

            if (string.IsNullOrEmpty(password))
                throw new Exception(
                    "Missing password from web.config file");

            // Domain
            string domain = 
                ConfigurationManager.AppSettings
                    ["ReportViewerDomain"];

            if (string.IsNullOrEmpty(domain))
                throw new Exception(
                    "Missing domain from web.config file");

            return new NetworkCredential(userName, password, domain);
            }
         }

      public bool GetFormsCredentials(out Cookie authCookie, 
                out string userName, out string password, 
                out string authority)
      {
        authCookie = null;
        userName = null;
        password = null;
        authority = null;

        // Not using form credentials
        return false;
      }
   }
  }
}

#line default
#line hidden
