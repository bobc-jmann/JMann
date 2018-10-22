<%@ Page Title="Test Reports" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ReportsTest.aspx.vb" Inherits="ReportsTest" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
	    .auto-style1 {
            width: 104px;
        }
        .auto-style2 {
            width: 161px;
        }
        .auto-style3 {
            width: 120px;
        }
	</style> 
</head>

<body>
     <form id="Form1" runat="server">
     <asp:ScriptManager ID="ScriptManager122" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="Scripts/jquery-1.4.1.min.js" />
        </Scripts>
    </asp:ScriptManager>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="General Reports" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
    <div id="divReports" class="specials" runat="server">
        <table>
            <tr>
                <td style="font-weight: bold; font-size: large">Test:</td>
            </tr>
             <tr>
	  	        <td>
                    <asp:Button ID="btnScheduleCalendar" runat="server" Text="Schedule Calendar Test" Width="250px"></asp:Button>
    	        </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
