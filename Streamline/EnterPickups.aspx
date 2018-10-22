<%@ Page Title="Enter Pickups" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="EnterPickups.aspx.vb" Inherits="EnterPickups" %>

<!DOCTYPE html>
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
	    .parm1 { width: 25%; text-align: right; font-weight:bold }
	    .parm2 { width: 60%; text-align: right; font-weight:bold }
	    .parm3 { width: 15%; text-align: right; font-weight:bold }
	    }
	</style> 
    <script type="text/javascript">
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }

        function OnQuantityValidation(s, e) {
            var qty = e.value;
            if (!qty)
                return;
            if (qty < 0 || qty > 100)  {
                e.isValid = false;
                e.errorText = "Quantity must be between 0 and 100.";
            }
        }

        // <![CDATA[
        function OnDriverChangedUnknown(cmbDriverUnknown) {
        	if (cmbDriverUnknown.GetValue() == 0) {
        		btnCreatePickupsForUnknownAddresses.SetEnabled(false);
        	}
        	else {
        		btnCreatePickupsForUnknownAddresses.SetEnabled(true);
        	}
        }
    	// This functionality is really no longer needed because we send all of the addresses
		//  to the tablets so they exist in PickupScheduleDetail and can be handled as if scheduled.
        function OnDriverChangedUnscheduled(cmbDriverUnscheduled) {
        	if (cmbDriverUnscheduled.GetValue() == 0) {
        		btnCreateUnscheduledSelectedAsPickups.SetEnabled(false);
        	}
        	else {
        		btnCreateUnscheduledSelectedAsPickups.SetEnabled(true);
        	}
        }
        function OnDriverChangedScheduled(cmbDriverScheduled) {
        	if (cmbDriverScheduled.GetValue() == 0) {
        		btnCreateScheduledSelectedAsPickups.SetEnabled(false);
        	}
        	else {
        		btnCreateScheduledSelectedAsPickups.SetEnabled(true);
        	}
        }
        // ]]> 
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Enter Pickups" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="50%">
             <tr>
                <td>Lookup Pickup Schedules:
                    <dx:ASPxRadioButtonList ID="rbLookupType" runat="server" AutoPostBack="true">
                        <Items>
                            <dx:ListEditItem Text="by Pickup Date" Value="byPickupDate" />
                            <dx:ListEditItem Text="by Pickup Cycle" Value="byPickupCycle" />
                            <dx:ListEditItem Text="by Route" Value="byRoute" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">Driver Location:
				    <asp:DropDownList ID="ddlDriverLocations" runat="server" ToolTip="Driver Location" Width="100%" AutoPostBack="True">
					</asp:DropDownList>
                    <asp:HiddenField ID="hfSQL_ddlDriverLocations" runat="server" Value=""></asp:HiddenField>
                    <asp:SqlDataSource ID="dsDriverLocations" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
				</td>
				<td style="width: 50%">
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

                    <asp:Label ID="lblPickupCycle" runat="server" Text="Pickup Cycle:"></asp:Label>
   				    <asp:DropDownList ID="ddlPickupCycles" runat="server" ToolTip="Pickup Cycles" Width="100%" AutoPostBack="True">
					</asp:DropDownList>
                    <asp:TextBox ID="txtSQL_ddlPickupCycles" runat="server" Visible="false"></asp:TextBox>
                    <asp:SqlDataSource ID="dsPickupCycles" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>

                    <asp:Label ID="lblRoutes" runat="server" Text="Routes:"></asp:Label>
   				    <asp:DropDownList ID="ddlRoutes" runat="server" ToolTip="Pickup Cycles" Width="100%" AutoPostBack="True">
					</asp:DropDownList>
                    <asp:TextBox ID="txtSQL_ddlRoutes" runat="server" Visible="false"></asp:TextBox>
                    <asp:SqlDataSource ID="dsRoutes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
			    </td>
            </tr>
        </table>

        <table id="tblPickupSchedules" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">
                   <dx:ASPxGridView ID="grdPickupSchedules" KeyFieldName="PickupScheduleID" runat="server" DataSourceID="dsPickupSchedules" Width="100%" EnableRowsCache="false">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="30" Caption=" " ShowClearFilterButton="true"/>
                            <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="History" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="PickupCycleAbbr" VisibleIndex="1" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="ScheduleType" VisibleIndex="2" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="MailingDate" VisibleIndex="4" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="PickupDate" VisibleIndex="5" HeaderStyle-Wrap="True"  />
                            <dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="6" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataTextColumn FieldName="CntMail" VisibleIndex="11" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntEmail" VisibleIndex="12" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntBag" VisibleIndex="13" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntPostcard" VisibleIndex="14" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntEmailNR" Caption=" Cnt Non-Route Email" VisibleIndex="14" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntPutOuts" VisibleIndex="15" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntPickupsDrivers" VisibleIndex="21" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CntPickupsAddresses" VisibleIndex="22" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SoftCarts" VisibleIndex="31" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#.00" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HardCarts" VisibleIndex="32" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#.00" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="TotalCarts" VisibleIndex="33" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                <PropertiesTextEdit DisplayFormatString="#.00" />               
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="10">
                        </SettingsPager>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowFilterRow="True" ShowGroupPanel="False" ShowFooter="True" />
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="grdPickupScheduleSections" OnInit="grdPickupScheduleSections_Init" KeyFieldName="SectionID" runat="server" DataSourceID="dsSections" Width="100%" OnBeforePerformDataSelect="grdPickupScheduleSections_DataSelect">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="SectionID" Visible="false" />
                                        <dx:GridViewDataColumn FieldName="PickupScheduleSectionID" Visible="false" />
                                        <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="false" /> 
                                        <dx:GridViewDataColumn FieldName="PickupCycleAbbr" Visible="false" /> 
                                        <dx:GridViewDataColumn FieldName="SectionDesc" Visible="false" /> 
                                        <dx:GridViewDataColumn FieldName="PickupDate" Visible="false" /> 
                                        <dx:GridViewDataColumn FieldName="SectionCode" VisibleIndex="1" HeaderStyle-Wrap="True"/>
                                        <dx:GridViewDataColumn FieldName="ScheduleType" VisibleIndex="2" HeaderStyle-Wrap="True"/>
                                        <dx:GridViewDataTextColumn FieldName="CntMail" VisibleIndex="11" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CntEmail" VisibleIndex="12" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CntBag" VisibleIndex="13" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CntPostcard" VisibleIndex="14" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CntEmailNR" Caption=" Cnt Non-Route Email" VisibleIndex="14" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CntPutOuts" VisibleIndex="15" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CntPickupsDrivers" VisibleIndex="21" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CntPickupsAddresses" VisibleIndex="22" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SoftCarts" VisibleIndex="31" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#.00" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="HardCarts" VisibleIndex="32" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#.00" />               
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="TotalCarts" VisibleIndex="33" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" >
                                            <PropertiesTextEdit DisplayFormatString="#.00" />               
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior ColumnResizeMode="Control" />
                                        <SettingsPager PageSize="10">
                                    </SettingsPager>
                                    <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                                    <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                                    <Templates>
                                        <DetailRow>
                                            <div style="padding: 3px 3px 2px 3px">
                                                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                                                    <TabPages>
                                                        <dx:TabPage Text="Pickup Sections" runat="server" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <dx:ASPxGridView ID="grdPickupsSections" OnInit="grdPickupsSections_Init" runat="server" DataSourceID="dsPickupsSections" KeyFieldName="PickupsSectionID" Width="100%" OnBeforePerformDataSelect="grdSectionID_DataSelect">
                                                                        <Settings HorizontalScrollBarMode="Visible" />
                                                                        <Columns>
                                                                            <dx:GridViewCommandColumn Name="grdCommands" VisibleIndex="0" Width="60" Caption=" " ShowEditButton="true" ShowDeleteButton="true"/>
                                                                            <dx:GridViewDataColumn FieldName="PickupsSectionID" Visible="false" />
                                                                            <dx:GridViewDataColumn FieldName="PickupScheduleSectionID" Visible="false" />
                                                                            <dx:GridViewDataColumn FieldName="Role" VisibleIndex="1" EditFormSettings-Visible="True"/>
                                                                            <dx:GridViewDataComboBoxColumn FieldName="DriverID" VisibleIndex="2" Caption="Driver" EditFormSettings-Visible="False" HeaderStyle-Wrap="True">
                                                                                <PropertiesComboBox DataSourceID="dsDrivers" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith">
                                                                                </PropertiesComboBox>
                                                                            </dx:GridViewDataComboBoxColumn>
                                                                            <dx:GridViewDataColumn FieldName="OldFirstName" VisibleIndex="3" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" />
                                                                            <dx:GridViewDataColumn FieldName="OldLastName" VisibleIndex="4" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" />
                                                                            <dx:GridViewDataComboBoxColumn FieldName="TruckID" VisibleIndex="6" Caption="Truck" EditFormSettings-Visible="False" HeaderStyle-Wrap="True">
                                                                                <PropertiesComboBox DataSourceID="dsTrucks" TextField="TruckNumber" ValueField="truckID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith">
                                                                                </PropertiesComboBox>
                                                                            </dx:GridViewDataComboBoxColumn>
                                                                            <dx:GridViewDataColumn FieldName="OldTruck" VisibleIndex="7" EditFormSettings-Visible="True"/>
                                                                            <dx:GridViewDataTextColumn FieldName="CntPickupsDriver" VisibleIndex="11" EditFormSettings-Visible="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right">
                                                                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="CntPickupsAddresses" VisibleIndex="12" EditFormSettings-Visible="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right">
                                                                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewBandColumn Caption="Entered on Tablet" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="bold">
                                                                                <Columns>
                                                                                    <dx:GridViewDataTextColumn FieldName="SoftCarts" VisibleIndex="21" EditFormSettings-Visible="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right">
                                                                                        <PropertiesTextEdit DisplayFormatString="#.00" />               
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="HardCarts" VisibleIndex="22" EditFormSettings-Visible="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right">
                                                                                        <PropertiesTextEdit DisplayFormatString="#.00" />               
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="TotalCarts" VisibleIndex="23" EditFormSettings-Visible="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right">
                                                                                        <PropertiesTextEdit DisplayFormatString="#.00" />  
                                                                                    </dx:GridViewDataTextColumn>
                                                                                </Columns>             
                                                                            </dx:GridViewBandColumn>
                                                                            <dx:GridViewDataColumn FieldName="Weather" VisibleIndex="31" EditFormSettings-Visible="True"/>
                                                                            <dx:GridViewDataColumn FieldName="MilesToRoute" VisibleIndex="32" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right"/>
                                                                            <dx:GridViewDataColumn FieldName="MilesOnRoute" VisibleIndex="33" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right"/>
                                                                            <dx:GridViewDataColumn FieldName="MilesFromRoute" VisibleIndex="34" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right"/>
                                                                            <dx:GridViewDataColumn FieldName="DidReRoute" VisibleIndex="35" EditFormSettings-Visible="True"/>
                                                                            <dx:GridViewDataColumn FieldName="Comment" VisibleIndex="36" EditFormSettings-Visible="True"/>
                                                                            <dx:GridViewDataColumn FieldName="DeviceName" VisibleIndex="37" EditFormSettings-Visible="False"/>
                                                                        </Columns>
                                                                        <SettingsEditing EditFormColumnCount="5"/> 
                                                                    </dx:ASPxGridView>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="btnUnknownPop" Text="Create Pickups for Unknown Addresses" OnClick="btnUnknownPopup" runat="server" />
                                                                           </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Text="Pickup Addresses" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <dx:ASPxGridView ID="grdPickupsAddresses"  runat="server" DataSourceID="dsPickupsAddresses" 
                                                                            KeyFieldName="PickupsAddressID" Width="100%" OnBeforePerformDataSelect="grdSectionID_DataSelect" 
                                                                            Settings-VerticalScrollBarMode="Hidden" OnInit="grdPickupsAddresses_Init" OnRowDeleted="grdPickupsAddresses_RowDeleted">
                                                                        <Columns>
                                                                            <dx:GridViewCommandColumn Name="grdCommands" VisibleIndex="0" Width="60" Caption=" " ShowEditButton="true" ShowDeleteButton="true" ShowClearFilterButton="true"/>
                                                                            <dx:GridViewDataColumn FieldName="PickupsAddressID" Visible="false" />
                                                                            <dx:GridViewDataColumn FieldName="AddressID" Visible="false" />
                                                                            <dx:GridViewDataColumn FieldName="StreetAddress" VisibleIndex="1" EditFormSettings-Visible="False"/>
                                                                            <dx:GridViewDataColumn FieldName="StreetName" VisibleIndex="2" EditFormSettings-Visible="False"/>
                                                                            <dx:GridViewDataColumn FieldName="City" VisibleIndex="3" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataColumn FieldName="Zip5" VisibleIndex="4" EditFormSettings-Visible="False"/>
                                                                            <dx:GridViewDataColumn FieldName="Bags" VisibleIndex="5" EditFormSettings-Visible="True"/>
                                                                            <dx:GridViewDataColumn FieldName="Boxes" VisibleIndex="6" EditFormSettings-Visible="True"/>
                                                                            <dx:GridViewDataColumn FieldName="Items" VisibleIndex="7" EditFormSettings-Visible="True"/>
                                                                            <dx:GridViewDataColumn FieldName="Grade" VisibleIndex="8" EditFormSettings-Visible="False"/>
                                                                            <dx:GridViewDataColumn FieldName="Time" VisibleIndex="9" EditFormSettings-Visible="False"/>
                                                                            <dx:GridViewDataColumn FieldName="DriverName" VisibleIndex="11" EditFormSettings-Visible="False"/>
                                                                            <dx:GridViewDataColumn FieldName="DeviceName" VisibleIndex="12" EditFormSettings-Visible="False"/>
                                                                        </Columns>
                                                                        <SettingsEditing EditFormColumnCount="3"/> 
                                                                        <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                                                                    </dx:ASPxGridView>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="btnUnscheduledPop" Text="Create Pickups for Unscheduled Addresses" OnClick="btnUnscheduledPopup" runat="server" />
                                                                           </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Text="Contact Addresses" runat="server" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl ID="ContentControl1" runat="server">
                                                                    <dx:ASPxGridView ID="grdMailAddresses" OnInit="grdMailAddresses_Init" ClientInstanceName="grdMailAddressesClient" runat="server" DataSourceID="dsContactAddresses" KeyFieldName="AddressID" Width="100%" OnBeforePerformDataSelect="grdSectionID_DataSelect" Settings-VerticalScrollBarMode="Auto">
                                                                        <Settings HorizontalScrollBarMode="Visible" />
                                                                        <Columns>
                                                                            <dx:GridViewCommandColumn Name="grdCommands" Caption=" " VisibleIndex="0" ShowSelectCheckbox="true" HeaderStyle-HorizontalAlign="Center" Width="60" ShowClearFilterButton="true" ShowEditButton="True"/>
                                                                            <dx:GridViewDataColumn FieldName="AddressID" Visible="false" />
                                                                            <dx:GridViewDataColumn FieldName="PickupScheduleDetailID" Visible="false" /> 
                                                                            <dx:GridViewDataColumn FieldName="StreetAddress" VisibleIndex="1" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataColumn FieldName="StreetName" VisibleIndex="2" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataColumn FieldName="City" VisibleIndex="3" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataColumn FieldName="Zip5" VisibleIndex="4" Width="50" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataComboBoxColumn FieldName="SectionID" VisibleIndex="5" Caption="Route-Section" EditFormSettings-Visible="False">
                                                                                <PropertiesComboBox DataSourceID="dsRouteSection" TextField="RouteSection" ValueField="SectionID" ValueType="System.Int32" />
                                                                            </dx:GridViewDataComboBoxColumn>
                                                                            <dx:GridViewDataColumn FieldName="ContactTypes" VisibleIndex="6" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataComboBoxColumn FieldName="StatusID" VisibleIndex="7" Caption="Status" EditFormSettings-Visible="False">
                                                                                <PropertiesComboBox DataSourceID="dsStatus" TextField="Status" ValueField="StatusID" ValueType="System.Int32" />                                                                            </dx:GridViewDataComboBoxColumn>
                                                                            <dx:GridViewDataColumn FieldName="Mail_OK" VisibleIndex="8" Width="50" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataColumn FieldName="Email_OK" VisibleIndex="9" Width="50" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataColumn FieldName="Bag_OK" VisibleIndex="10" Width="50" EditFormSettings-Visible="False" />
                                                                            <dx:GridViewDataDateColumn FieldName="DeliveryDate" VisibleIndex="11" Width="75" EditFormSettings-Visible="True" />
                                                                        </Columns>
                                                                        <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                                                                        <SettingsPager PageSize ="100" />
                                                                    </dx:ASPxGridView>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxButton ID="btnScheduledPop" Text="Create Pickups for Scheduled Addresses" OnClick="btnScheduledPopup" runat="server" />
                                                                            </td>
                                                                            <td>
                                                                                <dx:ASPxButton ID="btnAddressesDeselectAll" Text="Deselect All" OnClick="btnAddressesDeselectAll" runat="server" AutoPostBack="True" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                </dx:ASPxPageControl>
                                            </div>
                                        </DetailRow>
                                    </Templates>
                                    <SettingsDetail ShowDetailRow="true" />
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsPickupSchedules" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="hfPickupSchedulesSelectCommand" runat="server" Value=""></asp:HiddenField>
 
                    <asp:SqlDataSource ID="dsSections" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT DISTINCT PSS.PickupScheduleSectionID, PSS.PickupScheduleID, PSS.SectionID, PSS.SectionCode, ST.ScheduleType,
	                                PSS.CntMail, PSS.CntEmail, PSS.CntBag, PSS.CntPostcard, PSS.CntEmailNR, PSS.CntPutOuts, PSS.CntPickupsDrivers, PSS.CntPickupsAddresses,
	                                PSS.SoftCarts, PSS.HardCarts, PSS.TotalCarts
                                FROM tblPickupScheduleSections AS PSS
                                INNER JOIN tlkScheduleTypes AS ST ON ST.ScheduleTypeID = PSS.ScheduleTypeID
                                WHERE PSS.PickupScheduleID = @PickupScheduleID">
                            <SelectParameters>
                            <asp:SessionParameter Name="PickupScheduleID" SessionField="PickupScheduleID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsContactAddresses" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PSD.AddressID, PSD.PickupScheduleDetailID, StreetAddress, StreetName, StreetNumber, City, Zip5, A.SectionID, A.StatusID, A.Mail_OK, A.Email_OK, A.Bag_OK, dbo.ufnContactTypes(PSD.Mail, PSD.Email, PSD.Bag, PSD.Postcard) AS ContactTypes, PSD.DeliveryDate FROM tblPickupScheduleDetail PSD INNER JOIN tblAddresses A ON A.AddressID = PSD.AddressID WHERE @History = 0 AND PSD.PickupScheduleID = @PickupScheduleID AND A.SectionID = @SectionID AND PSD.EmailNR = 0 AND PSD.MailNR = 0"
                        UpdateCommand="UPDATE tblPickupScheduleDetail SET DeliveryDate = @deliveryDate WHERE PickupScheduleDetailID = @PickupScheduleDetailID">
                        <SelectParameters>
                            <asp:SessionParameter Name="SectionID" SessionField="SectionID" Type="Int32" />
                            <asp:SessionParameter Name="PickupScheduleID" SessionField="PickupScheduleID" Type="Int32" />
                            <asp:SessionParameter Name="History" SessionField="History" Type="Boolean" />
                        </SelectParameters>
						<UpdateParameters>
                            <asp:Parameter Name="PickupScheduleDetailID" Type="Int32" />
                            <asp:Parameter Name="DeliveryDate" Type="DateTime" />
						</UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPickupsSections" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PSEC.PickupsSectionID, PSEC.PickupScheduleSectionID, PSEC.[Role], PSEC.DriverID, PSEC.TruckID, 
	                                CASE WHEN D.DriverID IS NULL THEN PSEC.FirstName ELSE '' END AS OldFirstName, 
	                                CASE WHEN D.DriverID IS NULL THEN PSEC.LastName ELSE '' END AS OldLastName, 
	                                CASE WHEN T.TruckID IS NULL THEN PSEC.Truck ELSE '' END AS OldTruck,  
	                                CASE WHEN DA.DriverAssignmentID IS NULL THEN PickUp ELSE DA.CntPickupsDriver END AS CntPickupsDriver, 
	                                CASE WHEN DA.DriverAssignmentID IS NULL THEN PickUp ELSE DA.CntPickupsAddresses END AS CntPickupsAddresses, 
	                                CASE WHEN DA.DriverAssignmentID IS NULL THEN 0 ELSE DA.SoftCarts END AS SoftCarts, 
	                                CASE WHEN DA.DriverAssignmentID IS NULL THEN 0 ELSE DA.HardCarts END AS HardCarts, 
	                                CASE WHEN DA.DriverAssignmentID IS NULL THEN 0 ELSE DA.TotalCarts END AS TotalCarts, 
	                                PSEC.Weather, PSEC.MilesToRoute, PSEC.MilesOnRoute, PSEC.MilesFromRoute, 
                                    PSEC.DidReRoute, PSEC.DeviceName, PSEC.Comment 
                                FROM tblPickupsSections PSEC
                                LEFT OUTER JOIN tblDrivers AS D ON D.DriverID = PSEC.DriverID
                                LEFT OUTER JOIN tblTrucks AS T ON T.TruckID = PSEC.TruckID
                                LEFT OUTER JOIN tblDriverAssignments AS DA ON DA.PickupScheduleID = PSEC.PickupScheduleID
	                                AND DA.SectionID = PSEC.SectionID
	                                AND DA.DriverID = PSEC.DriverID
                                WHERE PSEC.PickupScheduleID = @PickupScheduleID AND PSEC.SectionID = @SectionID 
                                ORDER BY [Role], CASE WHEN D.DriverID IS NULL THEN PSEC.LastName ELSE D.LastName END, 
                                    CASE WHEN D.DriverID IS NULL THEN PSEC.FirstName ELSE D.FirstName END"
                        UpdateCommand="UPDATE [tblPickupsSections] SET [Role] = @Role, [FirstName] = @FirstName, [LastName] = @LastName, 
                                    [Truck] = @Truck, [Weather] = @Weather, 
                                    [MilesToRoute] = @MilesToRoute, [MilesOnRoute] = @MilesOnRoute, [MilesFromRoute] = @MilesFromRoute, 
                                    [DidReRoute] = @DidReRoute, [Comment] = REPLACE(@Comment, '''', '''''')
                                WHERE [PickupsSectionID] = @PickupsSectionID"
                        DeleteCommand="spPickupsSectionsDelete"
						DeleteCommandType="StoredProcedure" >
                        <SelectParameters>
                            <asp:SessionParameter Name="SectionID" SessionField="SectionID" Type="Int32" />
                            <asp:SessionParameter Name="PickupScheduleID" SessionField="PickupScheduleID" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PickupsSectionID" Type="Int32" />
                            <asp:Parameter Name="Role" Type="String" />
                            <asp:Parameter Name="FirstName" Type="String" />
                            <asp:Parameter Name="LastName" Type="String" />
                            <asp:Parameter Name="Truck" Type="String" />
                            <asp:Parameter Name="Weather" Type="String" />
                            <asp:Parameter Name="MilesToRoute" Type="Int32" />
                            <asp:Parameter Name="MilesOnRoute" Type="Int32" />
                            <asp:Parameter Name="MilesFromRoute" Type="Int32" />
                            <asp:Parameter Name="DidReRoute" Type="Boolean" />
                            <asp:Parameter Name="Comment" Type="String" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="PickupsSectionID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPickupsAddresses" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PA.PickupsAddressID, PA.AddressID, StreetAddress, StreetName, StreetNumber, City, Zip5, 
                                        Bags, Boxes, Items, Grade, CONVERT (VARCHAR(5), EndTime, 108) AS [Time], PA.DeviceName, 
                                        CASE WHEN D.FirstName IS NULL THEN '' ELSE D.FirstName + ' ' END + 
                                            CASE WHEN D.LastName IS NULL THEN '' ELSE D.LastName END AS DriverName  
                                    FROM tblPickupsAddresses PA 
                                    INNER JOIN tblPickupsSections PSEC ON PSEC.PickupsSectionID = PA.PickupsSectionID 
                                    LEFT OUTER JOIN tblAddresses A ON A.AddressID = PA.AddressID
                                    LEFT OUTER JOIN tblDrivers AS D ON D.DriverID = PSEC.DriverID
                                    WHERE PSEC.PickupScheduleID = @PickupScheduleID AND PSEC.SectionID = @SectionID"
                        UpdateCommand="UPDATE [tblPickupsAddresses] SET [Bags] = @Bags, [Boxes] = @Boxes, [Items] = @Items WHERE [PickupsAddressID] = @PickupsAddressID"
                        DeleteCommand="spPickupsManualDelete"
                        DeleteCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="SectionID" SessionField="SectionID" Type="Int32" />
                            <asp:SessionParameter Name="PickupScheduleID" SessionField="PickupScheduleID" Type="Int32" />
                            <asp:SessionParameter Name="History" SessionField="History" Type="Boolean" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PickupsAddressID" Type="Int32" />
                            <asp:Parameter Name="Bags" Type="Int32" />
                            <asp:Parameter Name="Boxes" Type="Int32" />
                            <asp:Parameter Name="Items" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="PickupsAddressID" Type="Int32" />
                        </DeleteParameters>
                   </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRouteSection" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [SectionID], R.RouteCode + '-' + S.SectionCode AS [RouteSection] FROM [tblSections] S INNER JOIN [tblRoutes] R ON R.RouteID = S.RouteID ORDER BY R.RouteCode, S.SectionCode">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsStatus" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [StatusID], [Status] FROM [tlkStatuses] ORDER BY [StatusID]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDrivers" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [DriverID], CASE WHEN FirstName IS NULL THEN '' ELSE FirstName + ' ' END + 
                                    CASE WHEN LastName IS NULL THEN '' ELSE LastName END AS DriverName 
                                FROM [tblDrivers] ORDER BY [LastName], [FirstName]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsTrucks" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [TruckID], [TruckNumber] FROM [tblTrucks] ORDER BY [TruckNumber]">
                    </asp:SqlDataSource>

              </td>
            </tr>
         </table>   
    </div>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblPickupDateMin" runat="server" Text="Earliest Pickup Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtEarliestPickupDate" ClientInstanceName="dtEarliestPickupDate"
                        Width="80%" ToolTip="Earliest Pickup Date" EditFormat="Custom" 
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
            </tr>
        </table>
    </div>

    <dx:ASPxPopupControl ID="popUnknown" ClientInstanceName="popUnknown" HeaderText="Create Pickups for Unknown Addresses" FooterText="" Width="350px" Height="100px" EncodeHtml="false" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AllowResize="true" runat="server" CloseAction="OuterMouseClick" AllowDragging="True" ShowFooter="true">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table>
                    <tr>
                        <td>
                            <dx:AspxLabel ID="lblDriverUnknown" runat="server" Text="Driver:" width="150"/>
                        </td>
                        <td>
                            <dx:ASPxComboBox runat="server" ID="cmbDriverUnknown" 
                                    DataSourceID="dsDrivers" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" 
                                    IncrementalFilteringMode="StartsWith">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDriverChangedUnknown(s); }" /> 
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:AspxLabel ID="lblUnknownAddressPickups" runat="server" Text="Pickups to Create:" width="150"/>
                        </td>
                        <td>
                            <dx:AspxTextbox ID="txtUnknown" runat="server" Width="30" Text="0" ValidationSettings-ErrorTextPosition="Bottom" >
                                <MaskSettings Mask="###" />
                                <ClientSideEvents Validation="OnQuantityValidation" />
                                <ValidationSettings>
                                    <RequiredField IsRequired="true" ErrorText="A quantity is required." />
                                </ValidationSettings>
                            </dx:AspxTextbox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <dx:ASPxButton ID="btnCreatePickupsForUnknownAddresses" Text="Create Pickups for Unknown Addresses" 
                                ClientInstanceName="btnCreatePickupsForUnknownAddresses"
                                OnClick="btnCreatePickupsForUnknownAddressesOK" runat="server" AutoPostBack="True" ClientEnabled="False" />
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popUnscheduled" ClientInstanceName="popUnscheduled" HeaderText="Create Pickups for Unscheduled Addresses" FooterText="" Width="800px" Height="300px" EncodeHtml="false" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AllowResize="true" runat="server" CloseAction="OuterMouseClick" AllowDragging="True" ShowFooter="true">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:AspxLabel ID="lblDriverUnscheduled" runat="server" Text="Driver:" width="150"/>
                <dx:ASPxComboBox runat="server" ID="cmbDriverUnscheduled" 
                        DataSourceID="dsDrivers" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" 
                        IncrementalFilteringMode="StartsWith">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDriverChangedUnscheduled(s); }" /> 
                </dx:ASPxComboBox>
                 <dx:ASPxGridView ID="grdUnscheduledAddresses" ClientInstanceName="grdUnscheduledAddressesClient" runat="server" DataSourceID="dsUnscheduledAddresses" KeyFieldName="AddressID" Width="100%" Settings-VerticalScrollBarMode="Auto">
                    <Columns>
                        <dx:GridViewCommandColumn Name="grdCommands" VisibleIndex="0" ShowSelectCheckbox="true" HeaderStyle-HorizontalAlign="Center" Width="60" ShowClearFilterButton="true"/>
                        <dx:GridViewDataColumn FieldName="AddressID" Visible="false" />
                        <dx:GridViewDataColumn FieldName="StreetAddress" VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="StreetName" VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="City" VisibleIndex="3" />
                        <dx:GridViewDataColumn FieldName="Zip" VisibleIndex="4" />
                        <dx:GridViewDataComboBoxColumn FieldName="SectionID" VisibleIndex="5" Caption="Route-Section" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" EditFormCaptionStyle-HorizontalAlign="Right" EditFormSettings-Caption="Route-Section">
                            <PropertiesComboBox DataSourceID="dsRouteSection" TextField="RouteSection" ValueField="SectionID" ValueType="System.Int32" />
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="StatusID" VisibleIndex="6" Caption="Status" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" EditFormCaptionStyle-HorizontalAlign="Right" EditFormSettings-Caption="Status">
                            <PropertiesComboBox DataSourceID="dsStatus" TextField="Status" ValueField="StatusID" ValueType="System.Int32" />
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn FieldName="Mail_OK" VisibleIndex="8" />
                        <dx:GridViewDataColumn FieldName="Email_OK" VisibleIndex="9" />
                        <dx:GridViewDataColumn FieldName="Bag_OK" VisibleIndex="10" />
                    </Columns>
                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                    <SettingsPager PageSize ="500" />
                </dx:ASPxGridView>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnCreateUnscheduledSelectedAsPickups" Text="Save Selected as Pickups"
								ClientInstanceName="btnCreateUnscheduledSelectedAsPickups"
                                OnClick="btnCreateUnscheduledSelectedAsPickupsOK" runat="server" AutoPostBack="True">
                                <ClientSideEvents Click="function(s, e) { popUnscheduled.Hide(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnUnscheduleAddressesDeselectAll" Text="Deselect All" 
                                OnClick="btnUnscheduledAddressesDeselectAll" runat="server" AutoPostBack="True" />
                        </td>
                        <td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

	<dx:ASPxPopupControl ID="popScheduled" ClientInstanceName="popScheduled" HeaderText="Create Pickups for Unknown Addresses" FooterText="" Width="350px" Height="100px" EncodeHtml="false" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AllowResize="true" runat="server" CloseAction="OuterMouseClick" AllowDragging="True" ShowFooter="true">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table>
                    <tr>
                        <td>
                            <dx:AspxLabel ID="lblDriverScheduled" runat="server" Text="Driver:" width="150"/>
                        </td>
                        <td>
                            <dx:ASPxComboBox runat="server" ID="cmbDriverScheduled" 
                                    DataSourceID="dsDrivers" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" 
                                    IncrementalFilteringMode="StartsWith">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDriverChangedScheduled(s); }" /> 
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <dx:ASPxButton ID="btnCreateScheduledSelectedAsPickups" Text="Create Pickups for Scheduled Addresses" 
                                ClientInstanceName="btnCreateScheduledSelectedAsPickups"
                                OnClick="btnCreatePickupsForScheduledAddressesOK" runat="server" AutoPostBack="True" ClientEnabled="False">
                                <ClientSideEvents Click="function(s, e) { popScheduled.Hide(); }" />
							</dx:ASPxButton>
                         </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <asp:SqlDataSource ID="dsUnscheduledAddresses" runat="server"
        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
        SelectCommand="SELECT A.AddressID, A.StreetAddress, A.StreetName, A.StreetNumber, A.City, A.Zip, A.SectionID, A.StatusID, A.Mail_OK, A.Email_OK, A.Bag_OK FROM tblAddresses AS A WHERE @History = 0 AND A.SectionID = @SectionID AND NOT EXISTS (SELECT * FROM tblPickupScheduleDetail AS PSD WHERE PSD.PickupScheduleID = @PickupScheduleID AND PSD.AddressID = A.AddressID) UNION SELECT A.AddressID, A.StreetAddress, A.StreetName, A.StreetNumber, A.City, A.Zip, A.SectionID, A.StatusID, A.Mail_OK, A.Email_OK, A.Bag_OK FROM tblAddresses A WHERE @History = 1 AND A.SectionID = @SectionID ORDER BY City, StreetName, StreetNumber">
        <SelectParameters>
            <asp:SessionParameter Name="SectionID" SessionField="SectionID" Type="Int32" />
            <asp:SessionParameter Name="PickupScheduleID" SessionField="PickupScheduleID" Type="Int32" />
            <asp:SessionParameter Name="History" SessionField="History" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>

    </form>
</body>
</html>
