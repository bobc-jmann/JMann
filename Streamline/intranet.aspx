<%@ Page Language="VB" AutoEventWireup="false" CodeFile="intranet.aspx.vb" Inherits="sys_ContentFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head2" runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="<%=vKeywords%>" />
    <meta name="description" content="" />
    <meta name="verify-v1" content="rE9TcrHs0b3LXBwnrjd2WxfIHj3gNYeSndEK8ojVMn0=" />
    <meta name="robots" content="index,follow" />
    <title><%=vAppTitle %></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body 
        {
            min-height: 90%;     
            height: 90%;     
            width: 95%;
        }
    </style>
<script type="text/javascript" language="javascript">

</script>

    <script type="text/javascript" src="/Streamline/Scripts/__AppJavascript.js"></script>
    <script type ="text/javascript">
        function adjustSize() {
            var contentIFrame = ASPxSplitter1.GetPaneByName("ContentPane").GetContentIFrame();
            var contentIFrameWindow = contentIFrame.contentWindow;
            if (typeof contentIFrameWindow.gridMain != 'undefined') {
                if (ASPxSplitter1.GetPane(0).IsCollapsed()) { v = 60 } else { v = 220 }
                if (contentIFrameWindow.gridMain.GetHeight() > (document.documentElement.clientHeight - 60)) {
                    var height = Math.max(0, document.documentElement.clientHeight);
                    contentIFrameWindow.gridMain.SetHeight(height - 60);
                }
                else {
                    contentIFrameWindow.gridMain.AdjustControl()
                }
                var width = Math.max(0, document.documentElement.clientWidth);
                contentIFrameWindow.gridMain.SetWidth(width - v);
            }
        }
        function jsHideLoadingPanel() {
            LoadingPanel.Hide();
        }

    </script>
    <script type="text/javascript">
        function TestForPopupBlocker() {
            var msg1 = 'Your browser is ';
            var msg2 = ', version ';
            var browserType = document.getElementById('browserType').value;
            var browserVersion = document.getElementById('browserVersion').value;
            var msg = msg1.concat(browserType, msg2, browserVersion, '.\n');
            var pop = window.open('http://www.google.com');
            if (!pop || pop.closed || typeof pop.closed=='undefined' || pop.location=='about:blank') {
                pop.close();
                alert(msg.concat('Popups are disabled.'));
            }
            else {
                pop.close();
                alert(msg.concat('Popups are enabled.'));
            }
        }
    </script>
</head>
<%--<% 	On Error Resume Next : conn.Open() : rs.Close() : On Error GoTo 0%>--%>
<body background="<%=vAppBackground %>" bgcolor="<%=vAppBodyBackColor %>" style="width:100%" >

    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True"></dx:ASPxLoadingPanel>
    <div>
    <asp:HiddenField ID="browserType" runat="server" />
    <asp:HiddenField ID="browserVersion" runat="server" />
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td valign="top" width="100%">
                <dx:ASPxSplitter EnableTheming="True" ID="ASPxSplitter1" runat="server" FullscreenMode="True" SeparatorSize="10">
                    <ClientSideEvents PaneResizeCompleted="function(s,e) { adjustSize() }" PaneResized="function(s,e) { adjustSize() }" /> 
                    <Panes>
                        <dx:SplitterPane Visible="true" Enabled="true" Name="MenuPane" ShowCollapseBackwardButton="true" ScrollBars="Auto" ShowCollapseForwardButton="false" MinSize=" 0px" size="200px" MaxSize="200px">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                    <div class="title">
                                        <h1><%=vCompanyName %></h1>
                                        <h2>Intranet</h2>
                                        <h3><i>CONFIDENTIAL</i></h3>
                                        <br />
                                        <%
                                            If InStr(vConnStr, "_Training") > 0 Then
                                                rl("TRAINING<br>")
                                            End If
                                            Dim vLogType = "logout"
                                            If Not ismt(Session("vUserName")) Then
                                                rl("<font size='1'>User: " & Session("vUserName") & "</font><br><br>")
                                                vLogType = "logout"
                                            Else
                                                vLogType = "login"
                                            End If
                                        %>
                                        <font size="1">
                                        &nbsp;[ <a class="nav" target="aContentPane" href="AboutIntranet.aspx">home</a> ] [ <a class="nav" target="aContentPane" href="aboutintranet.aspx">about</a> ] [ 
                                        <a class="nav" target="_top" href="login.aspx?T=<%=vLogType %>"><%=vLogType %></a> ]
                                      	</font>
                                        <br />
                                        <br />
                                    </div>
                                    <center>
                                        <dx:ASPxNavBar ID="ASPxNavBar1" Target="aContentPane" runat="server" ClientIDMode="AutoID" BackColor="skyblue" AllowSelectItem="true" Width="155">
                                            <Groups>
                                                <dx:NavBarGroup Name="navFinance" Visible="false" Expanded="false" Text="Finance" NavigateUrl="Finance.aspx">
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navStores" Visible="false" Expanded="false" Text="Stores">
                                                    <Items>
                                                        <dx:NavBarItem Name="navBracketStandards" Visible="false" Text="Bracket Standards" NavigateUrl="BracketStandards.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navBracketPricing" Visible="false" Text="Bracket Pricing" NavigateUrl="BracketPricing.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navBracketReport" Visible="false" Text="Bracket Report" NavigateUrl="ReportServer.aspx?RPTPATH=/Non-Linked Reports/Bracket Pricing"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navBracketCharts" Visible="false" Text="Bracket Charts" NavigateUrl="ReportServer.aspx?RPTPATH=/Non-Linked Reports/Bracket Pricing Charts"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDropoffDonations" Visible="false" Text="Drop-off Donations" NavigateUrl="DropoffDonations.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navCartsWorked" Visible="false" Text="Carts Worked" NavigateUrl="CartsWorked.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navCartInventory" Visible="false" Text="Cart Inventory" NavigateUrl="CartInventory.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navSalesByCartAnalysis" Visible="false" Text="Sales by Cart" NavigateUrl="ReportServer.aspx?RPTPATH=/Non-Linked Reports/Sales by Cart Analysis"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navFinanceSummary" Visible="false" Text="Finance Summary" NavigateUrl="ReportServer.aspx?RPTPATH=/Non-Linked Reports/Finance Summary"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDisplayTypes" Visible="false" Text="Display Types" NavigateUrl="DisplayTypes.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navReportGroups" Visible="false" Text="Report Groups" NavigateUrl="ReportGroups.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navSubDepartmentsMaint" Visible="false" Text="Sub Departments" NavigateUrl="SubDepartmentsMaint.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navSubDepartmentComparison" Visible="false" Text="Sub Dept Comparison" NavigateUrl="ReportServer.aspx?RPTPATH=/Non-Linked Reports/Sub Department Comparison"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navBestReport" Visible="false" Text="Best Report" NavigateUrl="ReportServer.aspx?RPTPATH=/Non-Linked Reports/Best Report"></dx:NavBarItem>
                                                 </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navNM" Visible="false" Expanded="false" Text="New Merchandise">
                                                    <Items>
                                                        <dx:NavBarItem Name="navNMPurchaseOrders" Visible="false" Text="Purchase Orders" NavigateUrl="NMPurchaseOrders.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMPurchaseOrderReceipts" Visible="false" Text="Purchase Order Receipts" NavigateUrl="NMPurchaseOrderReceipts.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMTransfers" Visible="false" Text="Transfers" NavigateUrl="NMtransfers.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMLabelPrinting" Visible="false" Text="Label Printing" NavigateUrl="NMLabelPrinting.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMInventory" Visible="false" Text="Inventory Maint" NavigateUrl="NMInventoryMaint.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMVendors" Visible="false" Text="Vendor Maint" NavigateUrl="NMVendorMaint.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMPhysicalInventory" Visible="false" Text="Physical Inventory" NavigateUrl="NMPhysicalInventory.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMStolenDamagedItems" Visible="false" Text="Stolen or Damaged Items" NavigateUrl="NMStolenDamagedItems.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMReports" Visible="false" Text="Reports" NavigateUrl="NMReports.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNMUpdatePrices" Visible="false" Text="Update Prices" NavigateUrl="NMUpdatePrices.aspx"></dx:NavBarItem>
                                                  </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navScheduling" Visible="false" Expanded="false" Text="Scheduling">
                                                    <Items>
                                                        <dx:NavBarItem Name="navScheduleMail" Visible="false" Text="Mail Scheduler" NavigateUrl="ScheduleMail.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navScheduledMail" Visible="false" Text="Scheduled Mail" NavigateUrl="ScheduledMail.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navScheduleEmail" Visible="false" Text="Email Scheduler" NavigateUrl="ScheduleEmail.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navScheduleBags" Visible="false" Text="Bag Scheduler" NavigateUrl="ScheduleBags.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navBagUnbagExportedSections" Visible="false" Text="Bag/Unbag Sections" NavigateUrl="BagUnbagExportedSections.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navPrintingScheduler" Visible="false" Text="Printing Scheduler" NavigateUrl="PrintingScheduler.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navScheduleCounts" Visible="false" Text="Schedule Counts" NavigateUrl="ScheduleCounts.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navBaggerAssignments" Visible="false" Text="Bagger Assignments" NavigateUrl="BaggerAssignments.aspx"></dx:NavBarItem>           
                                                        <dx:NavBarItem Name="navDriverAssignments" Visible="false" Text="Driver Assignments" NavigateUrl="DriverAssignments.aspx"></dx:NavBarItem>           
                                                        <dx:NavBarItem Name="navSpecialAssignments" Visible="false" Text="Special Assignments" NavigateUrl="SpecialAssignments.aspx"></dx:NavBarItem>           
                                                        <dx:NavBarItem Name="navDailyRouteReports" Visible="false" Text="Daily Route Reports" NavigateUrl="DailyRouteReports.aspx"></dx:NavBarItem>           
                                                        <dx:NavBarItem Name="navEnterPickups" Visible="false" Text="Enter Pickups" NavigateUrl="EnterPickups.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navNonTabletBagPickups" Visible="false" Text="Non-Tablet Bag" NavigateUrl="NonTabletBagPickups.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navMailingDateCalculator" Visible="false" Text="Mail Date Calculator" NavigateUrl="MailingDateCalculator.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navTargetsForecasts" Visible="false" Text="Targets & Forecasts" NavigateUrl="TargetsForecasts.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navMailSchedulePlanner" Visible="false" Text="Schedule Planner" NavigateUrl="MailSchedulePlanner.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navStreamlineInventory" Visible="false" Text="Streamline Inventory" NavigateUrl="StreamlineInventory.aspx"></dx:NavBarItem>
                                                   </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navSpecialsMenu" Visible="false" Expanded="false"  Text="Specials" >
                                                    <Items>
                                                        <dx:NavBarItem Name="navSpecials" Visible="false" Text="Specials" NavigateUrl="Specials.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navSpecialsGrading" Visible="false" Text="Specials Grading" NavigateUrl="SpecialsGrading.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navPhoneWorksheet" Visible="false" Text="Phone Worksheet" NavigateUrl="PhoneWorksheet.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navPhoneSummary" Visible="false" Text="Phone Summary" NavigateUrl="PhoneSummary.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navRouteWorksheet" Visible="false" Text="Route Worksheet" NavigateUrl="RouteWorksheet.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navEmailConfirms" Visible="false" Text="Email Confirms" NavigateUrl="EmailConfirms.aspx"></dx:NavBarItem>
                                                    </Items>                       
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navContainersMenu" Visible="false" Expanded="false"  Text="Containers" >
                                                    <Items>
                                                        <dx:NavBarItem Name="navContainers" Visible="false" Text="Containers" NavigateUrl="Containers.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navContainerCartsGrading" Visible="false" Text="Container Grading" NavigateUrl="ContainerCartsGrading.aspx"></dx:NavBarItem>
                                                    </Items>                       
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navLookups" Visible="false" Expanded="false" Text="Lookups">
                                                    <Items>
                                                        <dx:NavBarItem Name="navLookupsPickupSchedules" Visible="false" Text="Pickup Schedules" NavigateUrl="PickupScheduleLookup.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navLookupsAddresses" Visible="false" Text="Addresses" NavigateUrl="AddressLookup.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navLookupsSectionAddresses" Visible="false" Text="Sections-Addresses" NavigateUrl="SectionAddressLookup.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navLookupsPickupsByDriver" Visible="false" Text="Pickups by Driver" NavigateUrl="PickupsByDriver.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navMapping" Visible="false" Text="Mapping" NavigateUrl="Mapping.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navMappingPickupsDriver" Visible="false" Text="Map Pickups Driver" NavigateUrl="MappingPickupsDriver.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navMappingPickupsRegion" Visible="false" Text="Map Pickups Region" NavigateUrl="MappingPickupsRegion.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navStreetSegments" Visible="false" Text="Street Segments" NavigateUrl="StreetSegments.aspx" ></dx:NavBarItem>
                                                   </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navData" Visible="false" Expanded="false" Text="Data">
                                                    <Items> 
                                                        <dx:NavBarItem Name="navDataCharities" Visible="false" Text="Charities" NavigateUrl="CharityMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataPickupCycles" Visible="false" Text="Pickup Cycles" NavigateUrl="PickupCycleMaint.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataPickupCycleTemplates" Visible="false" Text="Pickup Cycle Templates" NavigateUrl="PickupCycleTemplateMaint.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataRoutesSections" Visible="false" Text="Routes-Sections" NavigateUrl="RouteSectionMaint.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataRouteMerge" Visible="false" Text="Route Merge" NavigateUrl="RouteMerge.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataZip3Rules" Visible="false" Text="Zip3 Rules" NavigateUrl="Zip3Rules.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataRecycleLog" Visible="false" Text="Recycle Log" NavigateUrl="RecycleLog.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataSectionAddresses" Visible="false" Text="Sections-Addresses" NavigateUrl="SectionAddressMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataAddresses2" Visible="false" Text="Addresses" NavigateUrl="AddressMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataAddressBulkEntry" Visible="false" Text="Address Bulk Entry" NavigateUrl="AddressBulkEntry.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataAddressMerge" Visible="false" Text="Address Merge" NavigateUrl="AddressMerge.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navAddressValidator" Visible="false" Text="Address Validator" NavigateUrl="AddressValidation.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataDrivers" Visible="false" Text="Drivers/Baggers" NavigateUrl="DriverMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataDriverLog" Visible="false" Text="Driver Log" NavigateUrl="DriverLog.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataTablets" Visible="false" Text="Tablets" NavigateUrl="TabletMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataPhones" Visible="false" Text="Phones" NavigateUrl="PhoneMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataTrucks" Visible="false" Text="Trucks/Vehicles" NavigateUrl="TruckMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataGeotabDevices" Visible="false" Text="Geotab Devices" NavigateUrl="GeotabDeviceMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataReportDistribution" Visible="false" Text="Report Distribution" NavigateUrl="ReportDistributionMaint.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navDataZipGroups" Visible="false" Text="Zip Groups" NavigateUrl="ZipGroupsMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navHolidays" Visible="false" Text="Holidays" NavigateUrl="HolidayMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navPostOffices" Visible="false" Text="Post Offices" NavigateUrl="PostOfficeMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navMailingDateRules" Visible="false" Text="Mailing Date Rules" NavigateUrl="MailingDateRulesMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navOptimumMailDeliveryDays" Visible="false" Text="Optimum Delivery Days" NavigateUrl="OptimumMailDeliveryDaysMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navUncommittedChanges" Visible="false" Text="Uncommitted Changes" NavigateUrl="UncommittedChanges.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navMailStatuses" Visible="false" Text="Mail Statuses" NavigateUrl="MailStatusesMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navStreamlineItemsMaint" Visible="false" Text="Streamline Items Maint" NavigateUrl="StreamlineItemsMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navStreamlineItemTypesMaint" Visible="false" Text="Streamline Item Types Maint" NavigateUrl="StreamlineItemTypesMaint.aspx"></dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navReports" Visible="false" Expanded="false" Text="Reports">
                                                    <Items>
                                                        <dx:NavBarItem Name="navReportsGeneral" Visible="false" Text="General Reports" NavigateUrl="ReportsGeneral.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navSpecialsReports" Visible="false" Text="Specials Reports" NavigateUrl="SpecialsReports.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navReportsStoreManager" Visible="false" Text="Store Manager Reports" NavigateUrl="ReportsStoreManager.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navReportsManagement" Visible="false" Text="Management Reports" NavigateUrl="ReportsManagement.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navReportsSSRS" Visible="false" Text="SSRS Reports" NavigateUrl="ReportsSSRS.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navReportsTest" Visible="false" Text="Test Reports" NavigateUrl="ReportsTest.aspx"></dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navReportSettings" Visible="false" Expanded="false" Text="Report Settings">
                                                    <Items>
                                                        <dx:NavBarItem Name="navDriverBaggerCosts" Visible="false" Text="Driver-Bagger Costs" NavigateUrl="DriverBaggerCosts.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navOtherProductionCosts" Visible="false" Text="Other Production Costs" NavigateUrl="OtherProductionCosts.aspx"></dx:NavBarItem>						
                                                     </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Name="navSystem" Visible="false" Expanded="false" Text="System">
                                                    <Items>
                                                        <dx:NavBarItem Name="navSysGetLatLong" Visible="false" Text="Geocoding" NavigateUrl="system/sys_GetLongLat.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navUsers" Visible="false" Text="Users" NavigateUrl="UserMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navSystemMenuGroups" Visible="false" Text="Menu Groups" NavigateUrl="MenuGroupMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navSystemMenuItems" Visible="false" Text="Menu Items" NavigateUrl="MenuItemMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navEmployees" Visible="false" Text="Employees" NavigateUrl="EmployeeMaint.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navLocations" Visible="false" Text="Locations" NavigateUrl="Locations.aspx" ></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navMonitorETL" Visible="false" Text="ETL Monitor" NavigateUrl="MonitorETL.aspx"></dx:NavBarItem>
                                                        <dx:NavBarItem Name="navTest" Visible="false" Text="Test" NavigateUrl="Test.aspx"></dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                            </Groups>
                                            <GroupContentStyle BackColor="White" />
                                        </dx:ASPxNavBar>

                           <!--                 <ClientSideEvents ItemClick="function(s, e) { if (e.item.group.name=='navData') LoadingPanel.Show();}"></ClientSideEvents>
       move inside navbar                        -->
                                        <asp:Button ID="btnCompatibilityCheck" runat="server" Text="Compatibility Check" OnClientClick="TestForPopupBlocker()"/>
                                        <asp:Button ID="btnPerformanceCounters" runat="server" Text="Performance Counters" OnClick="btnPerformanceCounters_Click"/>
                                    </center>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane Name="ContentPane" ContentUrlIFrameName="aContentPane" ContentUrl="AboutIntranet.aspx" ScrollBars="Auto">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl2" ClientIDMode="AutoID" runat="server"></dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                </dx:ASPxSplitter>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
