<%@ Page Title="Route Merge" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="RouteMerge.aspx.vb" Inherits="RouteMerge" %>

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
	    .parm3 { width: 20%; text-align: right; font-weight:bold }
	</style> 
    <script>
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }

        function ConfirmMergeRoutes() {
            var agree = confirm("Are you sure you want to Merge the selected Routes?");
            if (agree)
                return true;
            else
                return false;
        } // end ConfirmMergeAddresses
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Route Merge" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="30%">
            <tr>
                <td class="parm1">Select Primary Route:</td>
                <td class="parm2">
					<asp:DropDownList ID="ddlRoutesPrimary" runat="server" ToolTip="Route Code" Width="100%" AutoPostBack="True">
					</asp:DropDownList>
                </td>
            </tr>           
        </table>

        <br />

        <table width="30%">
            <tr>
                <td class="parm1">Select Primary Route:</td>
                <td class="parm2">
					<asp:DropDownList ID="ddlRoutesMerge" runat="server" ToolTip="Route Code" Width="100%" AutoPostBack="True">
					</asp:DropDownList>
                </td>
            </tr>           
        </table>

         <br />

        <table width="30%">
            <tr>
                <td class="parm1" colspan="2">All of the information for the "Route to be Merged and Deleted" will be merged into the "Primary Route" and the "Route to be Merged and Deleted" will be deleted from the database.</td>
                <td class="parm3">
                    <asp:Button ID="btnMerge" runat="server" Text="Merge" OnClientClick="return ConfirmMergeRoutes();" Width="100%" />
                </td>
            </tr>           
        </table>
       
    </div>
    </form>
</body>
</html>
