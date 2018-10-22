<%@ Page Title="Phone Worksheet" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="PhoneWorksheet.aspx.vb" Inherits="PhoneWorksheet" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
        #tblAddressParameters tr:nth-child(odd) { background-color: #E0E0E0; border-color: #E0E0E0; border: 0px; margin: 0px; padding: 0px; border-collapse: collapse }
        #tblAddressParameters tr:nth-child(even) { background-color: #F0F0F0; border-color: #F0F0F0; Border: 0px; margin: 0px; padding: 0px;  border-collapse: collapse }
	    .parm1 { width: 40%; text-align: right; font-weight:bold }
	    .parm2 { width: 40%; text-align: right; font-weight:bold }
	    .parm3 { width: 20%; text-align: right; font-weight:bold }	    }
	</style> 
    <script>
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }
    </script>
</head>

<body>
    <form id="Form1" runat="server">
        <%  %>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="Scripts/jquery-1.4.1.min.js" />
        </Scripts>
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="glob" runat="server">
        <ClientSideEvents ControlsInitialized="function (s, e) { jsCallHideLoadingPanel(); }" />
    </dx:ASPxGlobalEvents>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Phone Worksheet" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divMaint" class="specials" runat="server">
        <dx:ASPxLabel ID="lblDateLabel" Text="Date:" runat="server"  Font-Size="Medium" />
        <dx:ASPxLabel ID="lblDate" runat="server"  Font-Size="Medium" />
        <br />
        <br />
        <dx:ASPxLabel ID="lblOperatorLabel" Text="Operator:" runat="server" Font-Size="Medium" />
        <dx:ASPxLabel ID="lblOperator" runat="server" Font-Size="Medium" />
        <br />
        <br />
        <br />
 
        <table id="tblPhoneSheets" runat="server" style="visibility: visible" width="20%">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnROVM" runat="server" Text="Roll Over Voice Mail" Width="180px" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtROVM" runat="server" Width="40px" HorizontalAlign="Right" AutoPostBack="True" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnVMR" runat="server" Text="Voice Mail Returned" Width="180px" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtVMR" runat="server" Width="40px" HorizontalAlign="Right" AutoPostBack="True" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnWSR" runat="server" Text="Web Specials Returned" Width="180px" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtWSR" runat="server" Width="40px" HorizontalAlign="Right" style="height: 19px" AutoPostBack="True" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnEP" runat="server" Text="Emails Processed" Width="180px" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtEP" runat="server" Width="40px" HorizontalAlign="Right" style="height: 19px" AutoPostBack="True" HelpTextStyle-Wrap="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnTCR" runat="server" Text="Total Calls Received" Width="180px" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtTCR" runat="server" Width="40px" HorizontalAlign="Right" style="height: 19px" AutoPostBack="True" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnCCM" runat="server" Text="Courtesy Calls" Width="180px" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtCCM" runat="server" Width="40px" HorizontalAlign="Right" AutoPostBack="True" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnRRCM" runat="server" Text="Re-Route Calls Made" Width="180px" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtRRCM" runat="server" Width="40px" HorizontalAlign="Right" AutoPostBack="True" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnOC" runat="server" Text="Other Complaints" Width="180px" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtOC" runat="server" Width="40px" HorizontalAlign="Right" AutoPostBack="True" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSpecials" runat="server" Text="Specials Created" Width="180px" Enabled="False" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtSpecials" runat="server" Width="40px" HorizontalAlign="Right" Enabled="False" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnConfirms" runat="server" Text="Confirmations Handled" Width="180px" Enabled="False" />
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtConfirms" runat="server" Width="40px" HorizontalAlign="Right" Enabled="False" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnMisses" runat="server" Text="Misses Handled" Width="180px" Enabled="False" HelpTextStyle-Wrap="True"/>
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtMisses" runat="server" Width="40px" HorizontalAlign="Right" Enabled="False" HelpTextStyle-Wrap="True"/>
                </td>
            </tr>
        </table>
        
        <br />       
        <dx:ASPxLabel ID="lblComments" Text="Comments:" runat="server" Font-Size="Medium" />
        <dx:ASPxTextBox ID="txtComments" runat="server" Width="100%" AutoPostBack="True" />

        <asp:SqlDataSource ID="dsDailySheet" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
        </asp:SqlDataSource>

    </div>
 
    </form>
</body>
</html>
