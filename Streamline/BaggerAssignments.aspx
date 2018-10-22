<%@ Page Title="Bagger Assignments" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="BaggerAssignments.aspx.vb" Inherits="BaggerAssignments" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<script type="text/javascript">
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }

        function grid_EndCallback(s, e) {
            var pickupDate = dtPickupDate.GetDate();
            var driverLocationID = document.getElementById("ddlBaggerLocations").value;
            PageMethods.GetMissingInfo(pickupDate, driverLocationID, OnMissingInfoSuccess, OnMissingInfoError);
        }

        function OnMissingInfoSuccess(result) {
            if (result.Drivers > 0)
                document.getElementById("lblMissingBaggers").innerText = result.Baggers + " record(s) do not have Baggers assigned.";
            else
                document.getElementById("lblMissingBaggers").innerText = "";
        } // end OnMissingInfoSuccess

        function OnMissingInfoError(result) {
            var routeSections = document.getElementById("ddlBaggerLocations").value;
            alert('GetMissingInfo Error');
        } // end OnMissingInfoError

        function OnListBoxSaveClose(s, e) {
            var baggerID = cmbBaggers.GetValue();
            if (baggerID == null) {
                alert("Please select a Bagger.");
                return;
            }
            var pickupDate = dtPickupDate.GetDate();
            var routeSections = checkComboBox.GetText();
            PageMethods.SaveRouteSectionSelections(pickupDate, baggerID, routeSections, OnSaveRouteSectionSelectionsSuccess, OnSaveRouteSectionSelectionsError);
            checkComboBox.HideDropDown();
        }

        function OnSaveRouteSectionSelectionsSuccess(result) {
 
        } // end OnSaveRouteSectionSelectionsSuccess

        function OnSaveRouteSectionSelectionsError(result) {
            //alert('SaveRouteSectionSelections Error');
        } // end OnSaveRouteSectionSelectionsError

        function OnBaggerSelectedIndexChanged(s, e) {
            var baggerID = s.GetValue();
            PageMethods.GetBaggerInfo(baggerID, OnGetBaggerInfoSuccess, OnGetBaggerInfoError);
        }

        function OnGetBaggerInfoSuccess(result) {
            var d3 = grdBaggerAssignments.GetEditor('BaggerVehicleID');
            d3.SetText(result[2]);
        } // end OnGetBaggerInfoSuccess

        function OnGetBaggerInfoError(result) {
            alert('GetBaggerInfo Error');
        } // end OnGetBaggerInfoError


        // <![CDATA[
        var textSeparator = ";";
        function OnListBoxSelectionChanged(listBox, args) {
            UpdateText();
        }
        function UpdateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(GetSelectedItemsText(selectedItems));
        }
        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateText(); // for remove non-existing texts
        }
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
        // ]]>

	</script>
    <script type="text/javascript">
            function OnInit(s, e) {
                AdjustSize();
            }
            function OnEndCallback(s, e) {
                AdjustSize();
                if (s.cpServerMessage != null)
                    alert(s.cpServerMessage);
            }
            function OnControlsInitialized(s, e) {
                ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                    AdjustSize();
                });
            }
            function AdjustSize() {
                var height = Math.max(0, document.documentElement.clientHeight) - 200;
                grdBaggerAssignments.SetHeight(height);
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Bagger Assignments" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="100%">
            <tr>
                <td style="width: 20%">Bagger Location:
				    <asp:DropDownList ID="ddlBaggerLocations" runat="server" ToolTip="Bagger Location" Width="100%" AutoPostBack="True" />
 				</td>
                <td style="width: 20%"></td>
                <td style="width: 20%">
                    <dx:ASPxButton runat="server" ID="btnMissingBaggerInformation" Text="Missing Bagger Information" OnClick="btnRunMissingBaggerInformationReport_Click" AutoPostBack="True" Width="90%"></dx:ASPxButton>
                </td>
                <td style="width: 40%"></td>
            </tr>
            <tr>
 				<td style="width: 20%">
                    <asp:Label ID="lblPickupDate" runat="server" Text="Pickup Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtPickupDate" ClientInstanceName="dtPickupDate"
                        Width="100%" ToolTip="Pickup Date" EditFormat="Custom" 
                        EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                        ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                        ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                        runat="server" AutoPostBack="True">
                        <ValidationSettings SetFocusOnError="True">
                            <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                            <RegularExpression ErrorText="Invalid date"></RegularExpression>
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td></td>
                <td></td>
                <td></td>
           </tr>
           <tr>
                <td>Select Bagger:
                    <dx:ASPxComboBox ID="cmbBaggers" ClientInstanceName="cmbBaggers" Width="100%" ValueField="DriverID" TextField="DriverName" IncrementalFilteringMode="StartsWith"  ValueType="System.Int32" DataSourceID="dsBagger" runat="server" AutoPostBack="False">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { checkComboBox.SetText('');}" />
                    </dx:ASPxComboBox>
                </td>
                <td>Select Sections:
                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ddlSections" Width="90%" runat="server" AnimationType="None">
                        <DropDownWindowTemplate>
                            <dx:ASPxListBox Width="100%" ID="listBox" DataSourceID="dsSections" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" runat="server" ValueType="System.Int32" ValueField="SectionID" TextField="Route-Section">
                                <Border BorderStyle="None" />
                                <BorderBottom BorderStyle="Solid" BorderWidth="1px" />                                   
                                <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
                            </dx:ASPxListBox>
                            <table style="width: 100%" cellspacing="0" cellpadding="4">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnListBoxSaveClose" AutoPostBack="True" runat="server" Text="Save">
                                            <ClientSideEvents Click="OnListBoxSaveClose" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnListBoxClose" AutoPostBack="False" runat="server" Text="Close">
                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DropDownWindowTemplate>
                        <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues" />
                    </dx:ASPxDropDownEdit>
                    <asp:SqlDataSource ID="dsSections" runat="server" ProviderName="System.Data.SqlClient"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                </td>
			</tr>
        </table>
        <table>
           <tr>
                <td></td>
 				<td>
                     <asp:Label ID="lblMissingBaggers" runat="server" Text="" Forecolor="DarkMagenta" Font-Names="Arial" Font-Size="Medium" ></asp:Label>
                </td>
            </tr>               
        </table>

        <table id="tblBaggerAssignments" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 60%">
                    <dx:ASPxGridView ID="grdBaggerAssignments" KeyFieldName="DriverAssignmentID" runat="server" 
                            DataSourceID="dsBaggerAssignments" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False" 
                            Settings-VerticalScrollBarMode="Auto" OnStartRowEditing="grdBaggerAssignments_StartRowEditing" 
                            OnRowDeleting="grdBaggerAssignments_RowDeleting" EnableViewState="false"
                            OnCommandButtonInitialize="grdBaggerAssignments_CommandButtonInitialize"
                            OnCustomButtonInitialize="grdBaggerAssignments_CustomButtonInitialize">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowEditButton="true" ShowDeleteButton="true">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="cbAddBagger" Text="Add" Visibility="AllDataRows"/>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="DriverAssignmentID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="PickupScheduleSectionID" Visible="False" />
                            <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="False" />
                            <dx:GridViewDataColumn FieldName="PickupsSectionExists" Visible="False" />
                            <dx:GridViewDataColumn FieldName="NonTabletBagPickup" VisibleIndex="1" EditFormSettings-Visible="False" Caption="NT" Width="30" />
                            <dx:GridViewDataColumn FieldName="RouteID" Visible="False" />
                            <dx:GridViewDataColumn FieldName="RouteCode" Visible="False" />
                            <dx:GridViewDataColumn FieldName="RouteDesc" Visible="False" />
                            <dx:GridViewDataColumn FieldName="SectionID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="SectionCode" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="SectionDesc" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="DriverID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="Route-Section" VisibleIndex="4" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <EditFormSettings Visible="False"></EditFormSettings>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="BaggerDisplayID" VisibleIndex="6" EditFormSettings-Visible="False" Caption="Bagger" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesComboBox DataSourceID="dsBagger" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
						            <ClientSideEvents SelectedIndexChanged="OnDriverSelectedIndexChanged" />
                                </PropertiesComboBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="BaggerID" Visible="False"  EditFormSettings-Visible="True" Caption="Bagger" HeaderStyle-HorizontalAlign="Center" >
                                <PropertiesComboBox DataSourceID="dsBaggerEdit" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
						            <ClientSideEvents SelectedIndexChanged="OnBaggerSelectedIndexChanged" />
                                </PropertiesComboBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="BaggerVehicleID" VisibleIndex="9" Caption="Vehicle" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesComboBox DataSourceID="dsVehicles" TextField="TruckNumber" ValueField="BaggerVehicleID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
                                </PropertiesComboBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="CntTotalAddresses" Caption="Total Addresses" VisibleIndex="10" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True"  EditFormSettings-Visible="False">
                                <PropertiesTextEdit ClientInstanceName="CntTotalAddresses" DisplayFormatString="#,#" />
							</dx:GridViewDataTextColumn>  
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="BaggerID" ShowInGroupFooterColumn="BaggerID" SummaryType="Count" />
                            <dx:ASPxSummaryItem FieldName="CntTotalAddresses" ShowInGroupFooterColumn="CntTotalAddresses" SummaryType="Sum" />
                        </TotalSummary>
                        <SettingsEditing EditFormColumnCount="1" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="100" />
                        <ClientSideEvents EndCallback="grid_EndCallback" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                        <SettingsCommandButton>
                            <DeleteButton Text="Del"/>
                        </SettingsCommandButton>
                   </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsBaggerAssignments" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        UpdateCommand="spBaggerAssignments_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="spDriverAssignments_Delete"
                        DeleteCommandType="StoredProcedure">
                        <UpdateParameters>
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                            <asp:Parameter Name="BaggerID" Type="Int32" />
                            <asp:Parameter Name="BaggerVehicleID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                        </DeleteParameters> 
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="hfBaggerAssignmentsSelectCommand" runat="server" Value=""></asp:HiddenField>
                    <asp:SqlDataSource ID="dsBagger" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsBaggerEdit" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsVehicles" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 40%" />
             </tr>
        </table>
      
    </div>
    </form>
</body>
</html>
