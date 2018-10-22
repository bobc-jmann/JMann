<%@ Page Title="Driver Assignments" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="DriverAssignments.aspx.vb" Inherits="DriverAssignments" %>

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

        //function grid_EndCallback(s, e) {
        //    var pickupDate = dtPickupDate.GetDate();
        //    var driverLocationID = document.getElementById("ddlDriverLocations").value;
        //    PageMethods.GetMissingInfo(pickupDate, driverLocationID, OnMissingInfoSuccess, OnMissingInfoError);
        //}

        //function OnMissingInfoSuccess(result) {
        //    if (result.Drivers > 0)
        //        document.getElementById("lblMissingDrivers").innerText = result.Drivers + " record(s) do not have Drivers assigned.";
        //    else
        //        document.getElementById("lblMissingDrivers").innerText = "";

        //    if (result.Tablets > 0)
        //        document.getElementById("lblMissingTablets").innerText = result.Tablets + " record(s) do not have Tablets assigned.";
        //    else
        //        document.getElementById("lblMissingTablets").innerText = "";

        //} // end OnMissingInfoSuccess

        //function OnMissingInfoError(result) {
        //    var routeSections = document.getElementById("ddlDriverLocations").value;
        //    alert('GetMissingInfo Error');
        //} // end OnMissingInfoError

        //function OnListBoxSaveClose(s, e) {
        //	var driverID = cmbDrivers.GetValue();
        //	if (driverID == null) {
        //		alert("Please select a Driver.");
        //		return;
        //	}
        //	var pickupDate = dtPickupDate.GetDate();
        //	var routeSections = checkComboBox.GetText();
        //	PageMethods.SaveRouteSectionSelections(pickupDate, driverID, routeSections, OnSaveRouteSectionSelectionsSuccess, OnSaveRouteSectionSelectionsError);
        //	checkComboBox.HideDropDown();
        //}

        //function OnSaveRouteSectionSelectionsSuccess(result) {
			
        //} // end OnSaveRouteSectionSelectionsSuccess

        //function OnSaveRouteSectionSelectionsError(result) {
        //    //alert('SaveRouteSectionSelections Error');
        //} // end OnSaveRouteSectionSelectionsError

        //function OnSpecialsListBoxSaveClose(s, e) {
        //	var driverID = cmbDrivers.GetValue();
        //	if (driverID == null) {
        //		alert("Please select a Driver.");
        //		return;
        //	}
        //	var pickupDate = dtPickupDate.GetDate();
        //	var specials = checkSpecialsComboBox.GetText();
        //	PageMethods.SaveSpecialsSelections(pickupDate, driverID, specials, OnSaveSpecialsSelectionsSuccess, OnSaveSpecialsSelectionsError);
        //	checkSpecialsComboBox.HideDropDown();
		//}

        //function OnSaveSpecialsSelectionsSuccess(result) {
        //} // end OnSaveRouteSectionSelectionsSuccess

        //function OnSaveSpecialsSelectionsError(result) {
        //	//alert('SaveRouteSectionSelections Error');
        //} // end OnSaveRouteSectionSelectionsError

        function OnDriverSelectedIndexChanged(s, e) {
            var driverID = s.GetValue();
            PageMethods.GetDriverInfo(driverID, OnGetDriverInfoSuccess, OnGetDriverInfoError);
        }

        function OnGetDriverInfoSuccess(result) {
            // RCC - 5/2/13 - SetSelectedItem using the IDs did not work so I had to go with the Text.
            var d1 = grdDriverAssignments.GetEditor('TabletID');
            d1.SetText(result[0]);
            var d2 = grdDriverAssignments.GetEditor('PhoneID');
            d2.SetText(result[1]);
            var d3 = grdDriverAssignments.GetEditor('TruckID');
            d3.SetText(result[2]);
        } // end OnGetDriverInfoSuccess

        function OnGetDriverInfoError(result) {
            alert('GetDriverInfo Error');
        } // end OnGetDriverInfoError

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

        // <![CDATA[
        var textSeparator = ";";
        function OnSpecialsListBoxSelectionChanged(listBox, args) {
        	SpecialsUpdateText();
        }
        function SpecialsUpdateText() {
        	var selectedItems = checkSpecialsListBox.GetSelectedItems();
        	checkSpecialsComboBox.SetText(GetSpecialsSelectedItemsText(selectedItems));
		}
        //function SynchronizeSpecialsListBoxValues(dropDown, args) {
        //	checkSpecialsListBox.UnselectAll();
        //	var texts = dropDown.GetText().split(textSeparator);
        //	var values = GetSpecialsValuesByTexts(texts);
        //	checkSpecialsListBox.SelectValues(values);
        //	SpecialsUpdateText(); // for remove non-existing texts
        //}
        function GetSpecialsSelectedItemsText(items) {
        	var texts = [];
        	for (var i = 0; i < items.length; i++)
        		texts.push(items[i].text);
        	return texts.join(textSeparator);
        }
        function GetSpecialsValuesByTexts(texts) {
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
                grdDriverAssignments.SetHeight(height);
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Driver Assignments" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="100%">
            <tr>
                <td style="width: 20%">Driver Location:
				    <dx:ASPxComboBox ID="ddlDriverLocations" runat="server" ToolTip="Driver Location" Width="100%" AutoPostBack="False" 
						DataSourceID="dsDriverLocations" ValueField="RegionID" TextField="RegionDesc" ValueType="System.Int32">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { cmbDrivers.SetText(null); 
							checkComboBox.SetText(''); checkSpecialsComboBox.SetText(''); 
							DriversCallbackPanel.PerformCallback(); 
							SectionsCallbackPanel.PerformCallback(); 
							SpecialsCallbackPanel.PerformCallback(); 
							DriverAssignmentsCallbackPanel.PerformCallback(); 
							MissingCallbackPanel.PerformCallback(); }" />
					</dx:ASPxComboBox>
                    <asp:SqlDataSource ID="dsDriverLocations" runat="server" ProviderName="System.Data.SqlClient"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
  				</td>
                <td style="width: 40%"></td>
                <td style="width: 20%">
                    <dx:ASPxButton runat="server" ID="btnMissingDriverInformation" Text="Missing Driver Information" OnClick="btnRunMissingDriverInformationReport_Click" AutoPostBack="True" Width="90%"></dx:ASPxButton>
                </td>
                <td style="width: 20%"></td>
            </tr>
            <tr>
 				<td style="width: 20%">
                    <asp:Label ID="lblPickupDate" runat="server" Text="Pickup Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtPickupDate" ClientInstanceName="dtPickupDate"
                        Width="100%" ToolTip="Pickup Date" EditFormat="Custom" 
                        EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                        ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                        ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                        runat="server" AutoPostBack="False">
                        <ValidationSettings SetFocusOnError="True">
                            <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                            <RegularExpression ErrorText="Invalid date"></RegularExpression>
                        </ValidationSettings>
                        <ClientSideEvents DateChanged="function(s, e) { 
                            cmbDrivers.SetValue(null); 
							checkComboBox.SetText(''); 
                            checkSpecialsComboBox.SetText(''); 
							DriversCallbackPanel.PerformCallback(); 
							SectionsCallbackPanel.PerformCallback(); 
							SpecialsCallbackPanel.PerformCallback();
							DriverAssignmentsCallbackPanel.PerformCallback();
							MissingCallbackPanel.PerformCallback(); }" />                        
                   </dx:ASPxDateEdit>
                </td>
                <td></td>
                <td></td>
                <td></td>
           </tr>
           <tr>
                <td>Select Driver:
					 <dx:ASPxCallbackPanel runat="server" ID="DriversCallbackPanel" ClientInstanceName="DriversCallbackPanel"
                        Width="100%" OnCallback="DriversCallbackPanel_Callback" >
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent4" runat="server">
								<dx:ASPxComboBox ID="cmbDrivers" ClientInstanceName="cmbDrivers" Width="100%" ValueField="DriverID" TextField="DriverName" 
									IncrementalFilteringMode="StartsWith"  ValueType="System.Int32" DataSourceID="dsDriver" runat="server" AutoPostBack="False">
									<ClientSideEvents SelectedIndexChanged="function(s, e) { checkComboBox.SetText(''); checkSpecialsComboBox.SetText(''); 
										SpecialsCallbackPanel.PerformCallback(); DriverAssignmentsCallbackPanel.PerformCallback(); MissingCallbackPanel.PerformCallback(); }" />
								</dx:ASPxComboBox>
							</dx:PanelContent>
						</PanelCollection>
					</dx:ASPxCallbackPanel>
                </td>
                <td>Select Sections:
					 <dx:ASPxCallbackPanel runat="server" ID="SectionsCallbackPanel" ClientInstanceName="SectionsCallbackPanel"
                        Width="100%" OnCallback="SectionsCallbackPanel_Callback" >
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent5" runat="server">
								<dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ddlSections" Width="90%" runat="server" AnimationType="None">
									<DropDownWindowTemplate>
										<dx:ASPxListBox Width="100%" ID="listBox" DataSourceID="dsSections" ClientInstanceName="checkListBox" 
												SelectionMode="CheckColumn" runat="server"  Height="300"
												ValueType="System.Int32" ValueField="SectionID" TextField="Route-Section">
											<Border BorderStyle="None" />
											<BorderBottom BorderStyle="Solid" BorderWidth="1px" />                                   
											<ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
										</dx:ASPxListBox>
										<table style="width: 100%" cellspacing="0" cellpadding="4">
											<tr>
												<td>
													<dx:ASPxButton ID="btnListBoxSaveClose" AutoPostBack="False" runat="server" Text="Save">
			<%--                                            <ClientSideEvents Click="OnListBoxSaveClose" />--%>
														<ClientSideEvents Click="function(s, e) { checkComboBox.HideDropDown(); DriverAssignmentsCallbackPanel.PerformCallback(); MissingCallbackPanel.PerformCallback(); }" />
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
							</dx:PanelContent>
						</PanelCollection>
					</dx:ASPxCallbackPanel>
                    <asp:SqlDataSource ID="dsSections" runat="server" ProviderName="System.Data.SqlClient"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                </td>
                <td colspan="2">Select Specials:
					 <dx:ASPxCallbackPanel runat="server" ID="SpecialsCallbackPanel" ClientInstanceName="SpecialsCallbackPanel"
                        Width="100%" OnCallback="SpecialsCallbackPanel_Callback" >
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
								<dx:ASPxDropDownEdit ClientInstanceName="checkSpecialsComboBox" ID="ddlSpecials" Width="100%" runat="server" AnimationType="None">
									<DropDownWindowTemplate>
										<dx:ASPxListBox Width="100%" ID="specialsListBox" DataSourceID="dsSpecials" ClientInstanceName="checkSpecialsListBox" 
												SelectionMode="CheckColumn" runat="server" ValueType="System.Int32" ValueField="PickupID" TextField="SpecialText"
												OnDataBound="specialsListBox_DataBound" Height="300">
											<Border BorderStyle="None" />
											<BorderBottom BorderStyle="Solid" BorderWidth="1px" />                                   
											<ClientSideEvents SelectedIndexChanged="OnSpecialsListBoxSelectionChanged" />
										</dx:ASPxListBox>
										<table style="width: 100%" cellspacing="0" cellpadding="4">
											<tr>
												<td>
													<dx:ASPxButton ID="btnSpecialsListBoxSaveClose" AutoPostBack="False" runat="server" Text="Save">
														<%--<ClientSideEvents Click="OnSpecialsListBoxSaveClose" />--%>
														<ClientSideEvents Click="function(s, e) { SpecialsCallbackPanel.PerformCallback(); MissingCallbackPanel.PerformCallback(); }" />
													</dx:ASPxButton>
												</td>
												<td>
													<dx:ASPxButton ID="btnSpecialsListBoxClose" AutoPostBack="False" runat="server" Text="Close">
														<ClientSideEvents Click="function(s, e){ checkSpecialsComboBox.HideDropDown(); }" />
													</dx:ASPxButton>
												</td>
											</tr>
										</table>
									</DropDownWindowTemplate>
									<%--<ClientSideEvents TextChanged="SynchronizeSpecialsListBoxValues" DropDown="SynchronizeSpecialsListBoxValues" />--%>
								</dx:ASPxDropDownEdit>
							</dx:PanelContent>
						</PanelCollection>
					</dx:ASPxCallbackPanel>
                    <asp:SqlDataSource ID="dsSpecials" runat="server" ProviderName="System.Data.SqlClient"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                </td>
			</tr>
        </table>
		<dx:ASPxCallbackPanel runat="server" ID="MissingCallbackPanel" ClientInstanceName="MissingCallbackPanel"
        Width="100%" OnCallback="MissingCallbackPanel_Callback" >
			<PanelCollection>
				<dx:PanelContent ID="PanelContent2" runat="server">
					<table>
					   <tr>
							<td></td>
 							<td>
								 <asp:Label ID="lblMissingDrivers" runat="server" Text="" Forecolor="DarkMagenta" Font-Names="Arial" Font-Size="Medium" ></asp:Label>
							</td>
						</tr>               
						<tr>
							<td></td>
 							<td>
								 <asp:Label ID="lblMissingTablets" runat="server" Text="" Forecolor="DarkMagenta" Font-Names="Arial" Font-Size="Medium" ></asp:Label>
							</td>
						</tr>               
						<tr>
							<td></td>
 							<td>
								 <asp:Label ID="lblMissingSpecials" runat="server" Text="" Forecolor="DarkMagenta" Font-Names="Arial" Font-Size="Medium" ></asp:Label>
							</td>
						</tr>               
					</table>
				</dx:PanelContent>
			</PanelCollection>
		</dx:ASPxCallbackPanel>

        <table id="tblDriverAssignments" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">
					 <dx:ASPxCallbackPanel runat="server" ID="DriverAssignmentsCallbackPanel" ClientInstanceName="DriverAssignmentsCallbackPanel"
                        Width="100%" OnCallback="DriverAssignmentsCallbackPanel_Callback" >
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent3" runat="server">
								<dx:ASPxGridView ID="grdDriverAssignments" ClientInstanceName="grdDriverAssignments" KeyFieldName="DriverAssignmentID" runat="server" 
										DataSourceID="dsDriverAssignments" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False" 
										Settings-VerticalScrollBarMode="Auto" OnStartRowEditing="grdDriverAssignments_StartRowEditing" 
										OnRowDeleting="grdDriverAssignments_RowDeleting" EnableViewState="false"
										OnCommandButtonInitialize="grdDriverAssignments_CommandButtonInitialize"
										OnCustomButtonInitialize="grdDriverAssignments_CustomButtonInitialize">
									<Columns>
										<dx:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowEditButton="true" ShowDeleteButton="true">
											<CustomButtons>
												<dx:GridViewCommandColumnCustomButton ID="cbAddDriver" Text="Add" Visibility="AllDataRows"/>
												<dx:GridViewCommandColumnCustomButton ID="cbAddSpecial" Text="Spec" Visibility="AllDataRows"/>
											</CustomButtons>
										</dx:GridViewCommandColumn>
										<dx:GridViewDataColumn FieldName="DriverAssignmentID" Visible="False" /> 
										<dx:GridViewDataColumn FieldName="PickupScheduleSectionID" Visible="False" />
										<dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="False" />
										<dx:GridViewDataColumn FieldName="PickupsSectionExists" Visible="False" />
										<dx:GridViewDataColumn FieldName="LocationUnloadedID" Visible="False" />
										<dx:GridViewDataColumn FieldName="NonTabletBagPickup" VisibleIndex="1" EditFormSettings-Visible="False" Caption="NT" Width="30" />
										<dx:GridViewDataColumn FieldName="RouteID" Visible="False" />
										<dx:GridViewDataColumn FieldName="RouteCode" Visible="False" />
										<dx:GridViewDataColumn FieldName="RouteDesc" Visible="False" />
										<dx:GridViewDataColumn FieldName="SectionID" Visible="False" /> 
										<dx:GridViewDataColumn FieldName="SectionCode" Visible="False" /> 
										<dx:GridViewDataColumn FieldName="SectionDesc" Visible="False" /> 
										<dx:GridViewDataColumn FieldName="BaggerID" Visible="False" /> 
										<dx:GridViewDataColumn FieldName="Route-Section" VisibleIndex="4" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
											<EditFormSettings Visible="False"></EditFormSettings>
											<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
										</dx:GridViewDataColumn>
										<dx:GridViewDataComboBoxColumn FieldName="DriverDisplayID" VisibleIndex="6" EditFormSettings-Visible="False" Caption="Driver" HeaderStyle-HorizontalAlign="Center">
											<PropertiesComboBox DataSourceID="dsDriver" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
												<ClientSideEvents SelectedIndexChanged="OnDriverSelectedIndexChanged" />
											</PropertiesComboBox>
											<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										</dx:GridViewDataComboBoxColumn>
										<dx:GridViewDataComboBoxColumn FieldName="DriverID" Visible="False"  EditFormSettings-Visible="True" Caption="Driver" HeaderStyle-HorizontalAlign="Center" >
											<PropertiesComboBox DataSourceID="dsDriverEdit" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
												<ClientSideEvents SelectedIndexChanged="OnDriverSelectedIndexChanged" />
											</PropertiesComboBox>
											<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										</dx:GridViewDataComboBoxColumn>
										<dx:GridViewDataComboBoxColumn FieldName="TabletID" VisibleIndex="7" Caption="Tablet" HeaderStyle-HorizontalAlign="Center">
											<PropertiesComboBox DataSourceID="dsTablet" TextField="TabletName" ValueField="TabletID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
											</PropertiesComboBox>
											<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										</dx:GridViewDataComboBoxColumn>
										<dx:GridViewDataComboBoxColumn FieldName="PhoneID" VisibleIndex="8" Caption="Phone" HeaderStyle-HorizontalAlign="Center">
											<PropertiesComboBox DataSourceID="dsPhone" TextField="PhoneNumber" ValueField="PhoneID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
											</PropertiesComboBox>
											<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										</dx:GridViewDataComboBoxColumn>
										<dx:GridViewDataComboBoxColumn FieldName="TruckID" VisibleIndex="9" Caption="Truck" HeaderStyle-HorizontalAlign="Center">
											<PropertiesComboBox DataSourceID="dsTruck" TextField="TruckNumber" ValueField="TruckID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
											</PropertiesComboBox>
											<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										</dx:GridViewDataComboBoxColumn>
										<dx:GridViewDataComboBoxColumn FieldName="DeviceID" VisibleIndex="10" Caption="Geotab Device" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="False">
											<PropertiesComboBox DataSourceID="dsDevices" TextField="DeviceDescription" ValueField="DeviceID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
											</PropertiesComboBox>
											<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										</dx:GridViewDataComboBoxColumn>
										<dx:GridViewDataTextColumn FieldName="CntTotalAddresses" Caption="Total Addresses" VisibleIndex="15" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True"  EditFormSettings-Visible="False">
											<PropertiesTextEdit ClientInstanceName="CntTotalAddresses" DisplayFormatString="#,#" />
										</dx:GridViewDataTextColumn>  
									</Columns>
									<TotalSummary>
										<dx:ASPxSummaryItem FieldName="DriverID" ShowInGroupFooterColumn="DriverID" SummaryType="Count" />
										<dx:ASPxSummaryItem FieldName="CntTotalAddresses" ShowInGroupFooterColumn="CntTotalAddresses" SummaryType="Sum" />
									</TotalSummary>
									<SettingsEditing EditFormColumnCount="5" />
									<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
									<SettingsBehavior ColumnResizeMode="Control" />
									<SettingsPager PageSize="100" />
									<%--<ClientSideEvents EndCallback="grid_EndCallback" />--%>
									<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
									<SettingsCommandButton>
										<DeleteButton Text="Del"/>
									</SettingsCommandButton>
								</dx:ASPxGridView>
							</dx:PanelContent>
						</PanelCollection>
					</dx:ASPxCallbackPanel>
                    <asp:SqlDataSource ID="dsDriverAssignments" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        UpdateCommand="spDriverAssignments_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="spDriverAssignments_Delete"
                        DeleteCommandType="StoredProcedure">
                        <UpdateParameters>
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                            <asp:Parameter Name="DriverID" Type="Int32" />
                            <asp:Parameter Name="TabletID" Type="Int32" />
                            <asp:Parameter Name="PhoneID" Type="Int32" />
                            <asp:Parameter Name="TruckID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                        </DeleteParameters> 
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="hfDriverAssignmentsSelectCommand" runat="server" Value=""></asp:HiddenField>
                    <asp:SqlDataSource ID="dsDriver" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDriverEdit" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsTablet" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPhone" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsTruck" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDevices" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
            </tr>
        </table>
      
    </div>
    </form>
</body>
</html>
