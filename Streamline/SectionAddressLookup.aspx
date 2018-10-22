<%@ Page Title="Section Address Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="SectionAddressLookup.aspx.vb" Inherits="SectionAddressLookup" %>

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
        #tblSectionAddressParameters tr:nth-child(odd) { background-color: #E0E0E0; border-color: #E0E0E0; border: 0px; margin: 0px; padding: 0px; border-collapse: collapse }
        #tblSectionAddressParameters tr:nth-child(even) { background-color: #F0F0F0; border-color: #F0F0F0; Border: 0px; margin: 0px; padding: 0px;  border-collapse: collapse }
        #tblAddressesToAddParameters tr:nth-child(odd) { background-color: #E0E0E0; border-color: #E0E0E0; border: 0px; margin: 0px; padding: 0px; border-collapse: collapse }
        #tblAddressesToAddParameters tr:nth-child(even) { background-color: #F0F0F0; border-color: #F0F0F0; Border: 0px; margin: 0px; padding: 0px;  border-collapse: collapse }
	    .parm1 { width: 40%; text-align: right; font-weight:bold }
	    .parm2 { width: 40%; text-align: right; font-weight:bold }
	    .parm3 { width: 20%; text-align: right; font-weight:bold }
	</style> 
    <script>
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }
    </script>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script>
        function OnTemplateChanged()
        {
            var templateID = document.getElementById('otherRouteTemplates').innerHTML = "";
            var templateID = document.getElementById('ddlTemplates').value;
            if (templateID == 0) { //if there is no Template selected
                document.getElementById('ddlRoutes').options.length = 0;
                var opt = document.createElement("option");
                opt.text = 'Select Template';
                opt.value = 0;
                document.getElementById('ddlRoutes').options.add(opt);
            }
            else {
                PageMethods.GetRoutes(templateID, OnRouteSuccess, OnRouteError);
            }
        } // end TemplateChanged

        function OnRouteSuccess(result)
        {
            //remove previous Routes
            document.getElementById('ddlRoutes').options.length = 0;
            var opt = document.createElement("option");
            opt.text = 'Select Route';
            opt.value = 0;
            document.getElementById('ddlRoutes').options.add(opt);
            for (var i = 0; i < result.length; i++)
            {
                opt = document.createElement("option");
                opt.text = result[i].RouteCode;
                opt.value = result[i].RouteId;
                document.getElementById('ddlRoutes').options.add(opt);
            }
            OnRouteChanged();
        } // end OnRouteSuccess
 
        function OnRouteError(result)
        {
            alert('GetRoute Error');
        } // end OnRouteError

        function OnRouteChanged() {
            var templateID = document.getElementById('otherRouteTemplates').innerHTML = "";
            var routeID = document.getElementById('ddlRoutes').value;
            if (routeID == 0) { //if there is no Route selected
                document.getElementById('ddlSections').options.length = 0;
                var opt = document.createElement("option");
                opt.text = 'Select Section';
                opt.value = 0;
                document.getElementById('ddlSections').options.add(opt);
            }
            else {
                document.getElementById('hfSelectedRouteID').value = routeID
                PageMethods.GetSections(routeID, OnSectionSuccess, OnSectionError);
                var templateID = document.getElementById('ddlTemplates').value;
                PageMethods.GetOtherRouteTemplates(templateID, routeID, OnOtherRouteTemplatesSuccess, OnOtherRouteTemplatesError);
            }
        } // end RouteChanged

        function OnSectionSuccess(result) {
            //remove previous Sections
            document.getElementById('ddlSections').options.length = 0;
            var opt = document.createElement("option");
            opt.text = 'Select Section';
            opt.value = 0;
            document.getElementById('ddlSections').options.add(opt);
            for (var i = 0; i < result.length; i++) {
                opt = document.createElement("option");
                opt.text = result[i].SectionCode;
                opt.value = result[i].SectionId;
                document.getElementById('ddlSections').options.add(opt);
            }
        } // end OnSectionSuccess

        function OnSectionError(result) {
            alert('GetSection Error');
        } // end OnSectionError

  
        function OnSectionChanged() {
            document.getElementById('hfSelectedSectionID').value = document.getElementById('ddlSections').value;
        } // end SectionChanged


        function OnOtherRouteTemplatesSuccess(result) {
            document.getElementById('otherRouteTemplates').innerHTML = result;
        } // end OnOtherRouteTemplatesSuccess

        function OnOtherRouteTemplatesError(result) {
            alert('GetOtherRouteTemplates Error');
        } // end OnOtherRouteTempatesError

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Section-Address Lookup" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="50%">
            <tr>
                <td style="width: 34%">
                    <dx:ASPxCheckbox runat="server" ID="ckActiveTemplates" Text="Show Active Templates" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
                <td style="width: 33%">
                    <dx:ASPxCheckbox runat="server" ID="ckInactiveTemplates" Text="Show Inactive Templates" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
                <td style="width: 33%">
                    <dx:ASPxCheckbox runat="server" ID="ckAddressesNotOnRoutes" Text="Show Addresses Not on Routes" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
        </table>
        <table id="tblTemplates" runat="server" width="50%">
            <tr>
                <td style="width: 34%">Template:
				    <asp:DropDownList ID="ddlTemplates" runat="server" ToolTip="Pickup Cycle Template Code" Width="100%" OnChange="OnTemplateChanged();">
					</asp:DropDownList>
                    <asp:TextBox ID="txtSQL_ddlTemplates" runat="server" Visible="false"></asp:TextBox>
                    <asp:SqlDataSource ID="dsSqlTemplates" runat="server"></asp:SqlDataSource>
 				</td>
                <td style="width: 33%">Route:
				    <asp:DropDownList ID="ddlRoutes" runat="server" ToolTip="Route Code" Width="100%" OnChange="OnRouteChanged();">
					</asp:DropDownList>
                    <asp:HiddenField ID="hfSelectedRouteID" runat="server" Value=""></asp:HiddenField>
 				</td>
                <td style="width: 33%">Section:
				    <asp:DropDownList ID="ddlSections" runat="server" ToolTip="Section Code" Width="100%" OnChange="OnSectionChanged();" AutoPostBack="true">
					</asp:DropDownList>
                    <asp:HiddenField ID="hfSelectedSectionID" runat="server" Value=""></asp:HiddenField>
 				</td>
            </tr>
            <tr>
                <td colspan="3">
                    <p><asp:Label ID="otherRouteTemplates" runat="server" Visible="true" ForeColor="Magenta"></asp:Label></p>
                </td>
            </tr>
        </table>

        <table id="tblSectionAddresses" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">
                    <dx:ASPxGridView OnDataBound="OnSectionAddressesDataBound" ID="grdSectionAddresses" KeyFieldName="AddressID" ClientInstanceName="grdSectionAddresses" runat="server" DataSourceID="dsSectionAddresses" Width="100%">
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="10">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="False" ShowFooter="True" />
                     </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsSectionAddresses" runat="server"></asp:SqlDataSource>
                </td>
            </tr>
        </table>       

    </div>
    </form>
</body>
</html>
