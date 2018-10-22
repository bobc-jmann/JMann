<%@ Page Title="Section Address Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="SectionAddressMaint.aspx.vb" Inherits="SectionAddressMaint" %>

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
            var showInactiveSections = document.getElementById('hfShowInactiveSections').value;
            if (routeID == 0) { //if there is no Route selected
                document.getElementById('ddlSections').options.length = 0;
                var opt = document.createElement("option");
                opt.text = 'Select Section';
                opt.value = 0;
                document.getElementById('ddlSections').options.add(opt);
            }
            else {
                document.getElementById('hfSelectedRouteID').value = routeID
                PageMethods.GetSections(routeID, showInactiveSections, OnSectionSuccess, OnSectionError);
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
            var uncommittedChanges;
            document.getElementById('ddlSections').options.add(opt);
            for (var i = 0; i < result.length; i++) {
                opt = document.createElement("option");
                opt.text = result[i].SectionCode;
                opt.value = result[i].SectionId;
                uncommittedChanges = result[i].UncommittedChanges;
                document.getElementById('ddlSections').options.add(opt);
            }
            if (uncommittedChanges) {
            	document.getElementById('lblUcommittedChanges').innerHTML = "This route has Uncommitted Changes";
            	document.getElementById('hfUncommittedChanges').value = '1';
            }
            else {
            	document.getElementById('lblUcommittedChanges').innerHTML = "";
            	document.getElementById('hfUncommittedChanges').value = '0';
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


        function OnTemplateMoveChanged() {
            var templateID = document.getElementById('ddlTemplatesMove').value;
            if (templateID == 0) { //if there is no Template selected
                document.getElementById('ddlRoutesMove').options.length = 0;
                var opt = document.createElement("option");
                opt.text = 'Select a Template';
                opt.value = 0;
                document.getElementById('ddlRoutesMove').options.add(opt);
            }
            else {
                PageMethods.GetRoutes(templateID, OnRouteMoveSuccess, OnRouteMoveError);
            }
        } // end TemplateMoveChanged

        function OnRouteMoveSuccess(result) {
            //remove previous Routes
            document.getElementById('ddlRoutesMove').options.length = 0;
            var opt = document.createElement("option");
            opt.text = 'Select Route';
            opt.value = 0;
            document.getElementById('ddlRoutesMove').options.add(opt);
            for (var i = 0; i < result.length; i++) {
                opt = document.createElement("option");
                opt.text = result[i].RouteCode;
                opt.value = result[i].RouteId;
                document.getElementById('ddlRoutesMove').options.add(opt);
            }
            OnRouteMoveChanged();
        } // end OnRouteMoveSuccess

        function OnRouteMoveError(result) {
            alert('GetRoute Error');
        } // end OnRouteMoveError

        function OnRouteMoveChanged() {
            var routeID = document.getElementById('ddlRoutesMove').value;
            if (routeID == 0) { //if there is no Route selected
                document.getElementById('ddlSectionsMove').options.length = 0;
                var opt = document.createElement("option");
                opt.text = 'Select a Section';
                opt.value = 0;
                document.getElementById('ddlSectionsMove').options.add(opt);
            }
            else {
                document.getElementById('hfSelectedRouteIDMove').value = routeID
                PageMethods.GetSections(routeID, false, OnSectionMoveSuccess, OnSectionMoveError);
            }
        } // end RouteMoveChanged

        function OnSectionMoveSuccess(result) {
            //remove previous Sections
            document.getElementById('ddlSectionsMove').options.length = 0;
            var opt = document.createElement("option");
            opt.text = 'Select Section';
            opt.value = 0;
            document.getElementById('ddlSectionsMove').options.add(opt);
            for (var i = 0; i < result.length; i++) {
                opt = document.createElement("option");
                opt.text = result[i].SectionCode;
                opt.value = result[i].SectionId;
                document.getElementById('ddlSectionsMove').options.add(opt);
            }
        } // end OnSectionMoveSuccess

        function OnSectionMoveError(result) {
            alert('GetSection Error');
        } // end OnSectionMoveError

        function OnTemplateMoveSectionChanged() {
            var templateID = document.getElementById('ddlTemplatesMoveSection').value;
            if (templateID == 0) { //if there is no Template selected
                document.getElementById('ddlRoutesMoveSection').options.length = 0;
                var opt = document.createElement("option");
                opt.text = 'Select a Template';
                opt.value = 0;
                document.getElementById('ddlRoutesMoveSection').options.add(opt);
            }
            else {
                PageMethods.GetRoutes(templateID, OnRouteMoveSectionSuccess, OnRouteMoveSectionError);
            }
        } // end TemplateMoveSectionChanged

        function OnRouteMoveSectionSuccess(result) {
            //remove previous Routes
            document.getElementById('ddlRoutesMoveSection').options.length = 0;
            var opt = document.createElement("option");
            opt.text = 'Select Route';
            opt.value = 0;
            document.getElementById('ddlRoutesMoveSection').options.add(opt);
            for (var i = 0; i < result.length; i++) {
                opt = document.createElement("option");
                opt.text = result[i].RouteCode;
                opt.value = result[i].RouteId;
                document.getElementById('ddlRoutesMoveSection').options.add(opt);
            }
            OnRouteMoveChanged();
        } // end OnRouteMoveSectionSuccess

        function OnRouteMoveSectionError(result) {
            alert('GetRoute Error');
        } // end OnRouteMoveSectionError

        function OnStatusChanged() {
            document.getElementById('hfSelectedStatus').value = document.getElementById('ddlStatus').value;
        } // end OnStatusChanged

        function OnStatusChangedAdd() {
            document.getElementById('hfSelectedStatusAdd').value = document.getElementById('ddlStatusAdd').value;
        } // end OnStatusAddChanged

        function OnSectionMoveChanged() {
            document.getElementById('hfSelectedSectionIDMove').value = document.getElementById('ddlSectionsMove').value;
        } // end SectionMoveChanged

        function OnRouteMoveSectionChanged() {
            document.getElementById('hfSelectedRouteIDMoveSection').value = document.getElementById('ddlRoutesMoveSection').value;
        } // end SectionMoveChanged

        function grdSectionAddresses_SelectionChanged(s, e) {
            s.GetSelectedFieldValues("StreetAddress", GetSectionAddressesSelectedFieldValuesCallback);
        } // end grdSectionAddresses_SelectionChanged

        function GetSectionAddressesSelectedFieldValuesCallback(values) {
            document.getElementById("selCountSectionAddresses").innerHTML = grdSectionAddresses.GetSelectedRowCount();
        } // end GetSectionAddressesSelectedFieldValuesCallback

        function grdAddressesToAdd_SelectionChanged(s, e) {
            s.GetSelectedFieldValues("StreetAddress", GetAddressesToAddSelectedFieldValuesCallback);
        } // end grdAddressesToAdd_SelectionChanged

        function GetAddressesToAddSelectedFieldValuesCallback(values) {
            document.getElementById("selCountAddressesToAdd").innerHTML = grdAddressesToAdd.GetSelectedRowCount();
        } // end GetAddressesToAddSelectedFieldValuesCallback

        function ConfirmRemoveSelectedAddresses() {
            var agree = confirm("Are you sure you want to Remove the selected Addresses?");
            if (agree)
                return true;
            else
                return false;
        } // end ConfirmRemoveSelectedAddresses

        function ConfirmMoveSelectedAddresses() {
            var agree = confirm("Are you sure you want to Move the selected Addresses?");
            if (agree)
                return true;
            else
                return false;
        } // end ConfirmMoveSelectedAddresses

        function ConfirmMoveSelectedSection() {
            var agree = confirm("Are you sure you want to Move the selected Section?");
            if (agree)
                return true;
            else
                return false;
        } // end ConfirmMoveSelectedSection

        function ConfirmChangeStatusSelectedAddresses() {
            var agree = confirm("Are you sure you want to Change the Status of the selected Addresses?");
            if (agree)
                return true;
            else
                return false;
        } // end ConfirmChangeStatusSelectedAddresses

        function ConfirmAddSelectedAddresses() {
            var agree = confirm("Are you sure you want to Add the selected Addresses?");
            if (agree)
                return true;
            else
                return false;
        } // end ConfirmAddSelectedAddresses

        function ConfirmChangeStatusSelectedAddressesToAdd() {
            var agree = confirm("Are you sure you want to Change the Status of the selected Addresses?");
            if (agree)
                return true;
            else
                return false;
        } // end ConfirmChangeStatusSelectedAddressesAdd

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Section-Address Maintenance" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxCheckbox runat="server" ID="ckInactiveSections" Text="Show Inactive Sections" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
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
                    <asp:HiddenField ID="hfShowInactiveSections" runat="server" Value=""></asp:HiddenField>
 				</td>
            </tr>
            <tr>
                <td colspan="3">
                    <p><asp:Label ID="otherRouteTemplates" runat="server" Visible="true" ForeColor="Magenta"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <p><asp:Label ID="lblUcommittedChanges" runat="server" Visible="true" ForeColor="Blue" Text=""></asp:Label></p>
					<asp:HiddenField ID="hfUncommittedChanges" runat="server" />
                </td>
            </tr>
        </table>

        <table id="tblSectionAddresses" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">
                    <dx:ASPxGridView OnDataBound="OnSectionAddressesDataBound" ID="grdSectionAddresses" KeyFieldName="AddressID" 
							ClientInstanceName="grdSectionAddresses" runat="server" DataSourceID="dsSectionAddresses" Width="100%">
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager AlwaysShowPager="True">
                               <PageSizeItemSettings Visible="true" />
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="False" ShowFooter="True" />
                        <ClientSideEvents SelectionChanged="grdSectionAddresses_SelectionChanged" />
                     </dx:ASPxGridView>

                     <asp:SqlDataSource ID="dsSectionAddresses" runat="server"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    Selected count: <span id="selCountSectionAddresses" runat="server" style="font-weight: bold">0</span>
                </td>
            </tr>
        </table>
        <table width="100%" id="tblSectionAddressParameters" runat="server" style="visibility: hidden">
            <tr>
                <td class="parm1">Deselect All Addresses:</td>
                <td class="parm2">

                </td>
                <td class="parm3">
                    <asp:Button ID="btnSectionAddressesDeselect" runat="server" Text="Deselect" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="parm1">Select Addresses:</td>
                <td class="parm2">
                    <table>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblSectionAddressCity" runat="server" Text="In City:" ></asp:Label>
                                <asp:TextBox ID="txtSectionAddressCity" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblSectionAddressStreet" runat="server" Text="On Street:"></asp:Label>
                                <asp:TextBox ID="txtSectionAddressStreet" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 10%">
                                <asp:Label ID="lblSectionAddressCRRT" runat="server" Text="On CRRT:"></asp:Label>
                                <asp:TextBox ID="txtSectionAddressCRRT" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="parm3">
                    <asp:Button ID="btnSectionAddressesSelect" runat="server" Text="Select" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="parm1">Remove Selected Addresses:</td>
                <td class="parm2">
                </td>
                <td class="parm3">
                    <asp:Button ID="btnRemoveSelectedAddresses" runat="server" Text="Remove" OnClientClick="return ConfirmRemoveSelectedAddresses();" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="parm1">Move Selected Addresses to:</td>
                <td class="parm2">
                    <table>
                        <tr>
                            <td style="width: 33%">
                                <asp:Label ID="lblTemplatesMove" runat="server" Text="Pickup Cycle Template:"></asp:Label>
				                <asp:DropDownList ID="ddlTemplatesMove" runat="server" ToolTip="Pickup Cycle Template Code" Width="100%" OnChange="OnTemplateMoveChanged();" OnClientClick="return ConfirmMoveSelectedAddresses();">
					            </asp:DropDownList>
                                <asp:SqlDataSource ID="dsSqlTemplatesMove" runat="server"></asp:SqlDataSource>
 				            </td>
                            <td style="width: 33%">
                                <asp:Label ID="lblRoutesMove" runat="server" Text="Route:"></asp:Label>
				                <asp:DropDownList ID="ddlRoutesMove" runat="server" ToolTip="Route Code" Width="100%" OnChange="OnRouteMoveChanged();">
					            </asp:DropDownList>
                                <asp:HiddenField ID="hfSelectedRouteIDMove" runat="server" Value=""></asp:HiddenField>
			                </td>
                            <td style="width: 33%">
                                <asp:Label ID="lblSectionsMove" runat="server" Text="Section:"></asp:Label>
				                <asp:DropDownList ID="ddlSectionsMove" runat="server" ToolTip="Section Code" Width="100%" OnChange="OnSectionMoveChanged();" AutoPostBack="True">
					            </asp:DropDownList>
                                <asp:HiddenField ID="hfSelectedSectionIDMove" runat="server" Value=""></asp:HiddenField>
 				            </td>
                        </tr>
                    </table>
                </td>
                <td class="parm3">
                    <asp:Button ID="btnMoveSelectedAddresses" runat="server" Text="Move" OnClientClick="return ConfirmMoveSelectedAddresses();" Width="100%" />
                </td>
            </tr>
                        <tr>
                <td class="parm1">Move Current Section to:</td>
                <td class="parm2">
                    <table>
                        <tr>
                            <td style="width: 33%">
                                <asp:Label ID="lblTemplateMoveSection" runat="server" Text="Pickup Cycle Template:"></asp:Label>
				                <asp:DropDownList ID="ddlTemplatesMoveSection" runat="server" ToolTip="Pickup Cycle Template Code" Width="100%" OnChange="OnTemplateMoveSectionChanged();" OnClientClick="return ConfirmMoveSelectedSection();">
					            </asp:DropDownList>
                                <asp:SqlDataSource ID="dsSqlTemplatesMoveSection" runat="server"></asp:SqlDataSource>
 				            </td>
                            <td style="width: 33%">
                                <asp:Label ID="lblTemplatesMoveSection" runat="server" Text="Route:"></asp:Label>
				                <asp:DropDownList ID="ddlRoutesMoveSection" runat="server" ToolTip="Route Code" Width="100%" OnChange="OnRouteMoveSectionChanged();" AutoPostBack="True">
					            </asp:DropDownList>
                                <asp:HiddenField ID="hfSelectedRouteIDMoveSection" runat="server" Value=""></asp:HiddenField>
			                </td>
                       </tr>
                    </table>
                </td>
                <td class="parm3">
                    <asp:Button ID="btnMoveSelectedSection" runat="server" Text="Move" OnClientClick="return ConfirmMoveSelectedSection();" Width="100%" />
                </td>
            </tr>

            <tr>
                <td class="parm1">Change Status of Selected Addresses:</td>
                <td class="parm2">
				    <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Status" Width="33%" OnChange="OnStatusChanged();">
					</asp:DropDownList>
                    <asp:HiddenField ID="hfSelectedStatus" runat="server" Value=""></asp:HiddenField>
 				</td>
                <td class="parm3">
                    <asp:Button ID="btnChangeStatusSelectedAddress" runat="server" Text="Change" OnClientClick="return ConfirmChangeStatusSelectedAddresses();" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="parm1">Select Addresses to Add:</td>
                <td class="parm2">
                    <table>
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblAddressCity" runat="server" Text="In City:"></asp:Label>
                                <asp:TextBox ID="txtAddressCity" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="lblAddressStreet" runat="server" Text="On Street:"></asp:Label>
                                <asp:TextBox ID="txtAddressStreet" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 15%">
                                <asp:Label ID="lblAddressCRRT" runat="server" Text="On CRRT:"></asp:Label>
                                <asp:TextBox ID="txtAddressCRRT" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 15%">
                                <asp:Label ID="lblAddressZip" runat="server" Text="In Zip:"></asp:Label>
                                <asp:TextBox ID="txtAddressZip" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 10%; text-align: center">
                                <asp:Label ID="lbStatus" runat="server" Text="Status Counts"></asp:Label>
                                <asp:CheckBox ID="ckStatusGroup" runat="server" Width="100%"></asp:CheckBox>
                            </td>
                        </tr>
                    </table>
 				</td>
                <td class="parm3">
                    <asp:Button ID="btnSelectAddressesToAdd" runat="server" Text="Select" Width="100%" />
                </td>
            </tr>
        </table>
       
        <table id="tblAddressesToAdd" runat="server" style="visibility: hidden">
            <tr>
                <td style="width: 100%" colspan="5">
                    <dx:ASPxGridView OnDataBound="OnAddressesToAddDataBound" ID="grdAddressesToAdd" KeyFieldName="AddressID" ClientInstanceName="grdAddressesToAdd" runat="server" DataSourceID="dsAddressesToAdd" Width="100%">
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager>
                               <PageSizeItemSettings Visible="true" />
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="Virtual" ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <ClientSideEvents SelectionChanged="grdAddressesToAdd_SelectionChanged" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsAddressesToAdd" runat="server"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    Selected count: <span id="selCountAddressesToAdd" runat="server" style="font-weight: bold">0</span>
                </td>
             </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSelectedAddressesAdd" runat="server" Text="Add Selected Addresses" OnClientClick="return ConfirmAddSelectedAddresses();" />
                </td>
            </tr>
        </table>

        <table width="100%" id="tblAddressesToAddParameters" runat="server" style="visibility: hidden">
            <tr>
                <td class="parm1">Deselect All Addresses:</td>
                <td class="parm2">

                </td>
                <td class="parm3">
                    <asp:Button ID="btnAddressesToAddDeselect" runat="server" Text="Deselect" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="parm1">Select Addresses:</td>
                <td class="parm2">
                    <table>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="lblAddressesToAddCity" runat="server" Text="In City:" ></asp:Label>
                                <asp:TextBox ID="txtAddressesToAddCity" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblAddressesToAddStreet" runat="server" Text="On Street:"></asp:Label>
                                <asp:TextBox ID="txtAddressesToAddStreet" runat="server" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width: 10%">
                                <asp:Label ID="lblAddressesToAddCRRT" runat="server" Text="On CRRT:"></asp:Label>
                                <asp:TextBox ID="txtAddressesToAddCRRT" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="parm3">
                    <asp:Button ID="btnAddressesToAddSelect" runat="server" Text="Select" Width="100%" />
                </td>
            </tr>
 
            <tr>
                <td class="parm1">Change Status of Selected Addresses:</td>
                <td class="parm2">
				    <asp:DropDownList ID="ddlStatusAdd" runat="server" ToolTip="Status" Width="33%" OnChange="OnStatusChangedAdd();">
					</asp:DropDownList>
                    <asp:HiddenField ID="hfSelectedStatusAdd" runat="server" Value=""></asp:HiddenField>
 				</td>
                <td class="parm3">
                    <asp:Button ID="btnChangeStatusSelectedAddressesToAdd" runat="server" Text="Change" OnClientClick="return ConfirmChangeStatusSelectedAddressesToAdd();" Width="100%" />
                </td>
            </tr>
         </table>
 
        <table id="tblAddressesToAddInfo" runat="server" style="visibility: hidden">
            <tr>
                <td style="width: 60%">
                    <dx:ASPxGridView ID="grdAddressesToAddInfo" KeyFieldName="StreetName" ClientInstanceName="grdAddressesToAddInfo" runat="server" DataSourceID="dsAddressesToAddInfo" Width="100%">
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager>
                               <PageSizeItemSettings Visible="true" />
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Auto" ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsAddressesToAddInfo" runat="server"></asp:SqlDataSource>
                    <dx:ASPxGridViewExporter ID="addressesToAddInfoExporter" GridViewID="grdAddressesToAddInfo" runat="server">
                    </dx:ASPxGridViewExporter>
                </td>
                <td style="width: 40%"></td>
             </tr>
             <tr>
                 <td>
                    <dx:ASPxButton ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export Addresses To Add Info">
                    </dx:ASPxButton>
                 </td>
             </tr>
        </table>

    </div>
    </form>
</body>
</html>
