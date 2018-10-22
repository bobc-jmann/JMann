<%@ Page Title="Non-Tablet Bag Pickups" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="NonTabletBagPickups.aspx.vb" Inherits="NonTabletBagPickups" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script>
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }
    </script>
    <script type="text/javascript">
        function OnInit(s, e) {
            AdjustSize();
        }
        function OnEndCallback(s, e) {
            AdjustSize();
        }
        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight) - 100;
            grid.SetHeight(height);
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Non-Tablet Bag Pickups" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" 
                            DataSourceID="dsNonTabletBagPickups" KeyFieldName="PickupID" Width="100%" EnableRowsCache="false" 
                            SettingsBehavior-ConfirmDelete="True" OnRowValidating="grid_RowValidating">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="80" ShowClearFilterButton="true" ShowEditButton="true" ShowDeleteButton="true"/>
                            <dx:GridViewDataColumn FieldName="PickupID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="PickupScheduleSectionID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="DriverAssignmentID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="PickupDate" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="PickupCycleID" Caption="Pickup Cycle">
                                <PropertiesComboBox DataSourceID="dsPickupCycles" ValueType="System.Int32" ValueField="PickupCycleID" TextField="PickupCycleAbbr" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="SectionID" Caption="Route-Section">
                                <PropertiesComboBox DataSourceID="dsRouteSection" ValueType="System.Int32" ValueField="SectionID" TextField="Route-Section" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="BaggerID" Caption="Bagger">
                                <PropertiesComboBox DataSourceID="dsBaggers" ValueType="System.Int32" ValueField="DriverID" TextField="DriverName" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="CntPutOutsBagger" Caption="Bagger Put Outs" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="DriverID" Caption="Driver">
                                <PropertiesComboBox DataSourceID="dsDriver" ValueType="System.Int32" ValueField="DriverID" TextField="DriverName" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="PutOuts" Caption="Put Outs" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="Pickups" Caption="Pickups" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataTextColumn FieldName="SoftCarts" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesTextEdit ClientInstanceName="SoftCarts" DisplayFormatString="#.00" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HardCarts" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesTextEdit ClientInstanceName="HardCarts" DisplayFormatString="#.00" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="TotalCarts" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesTextEdit ClientInstanceName="TotalCarts" DisplayFormatString="#.00" />
                                <EditFormSettings Visible="False"></EditFormSettings>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="LocationUnloadedID" VisibleIndex="31" EditFormSettings-Visible="True" Caption="Location Unloaded" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesComboBox DataSourceID="dsLocations" TextField="LocationAbbr" ValueField="LocationID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" />						            
                                 <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                        </Columns>
                        <SettingsPager PageSize="50" />
                        <SettingsEditing EditFormColumnCount="3" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" ShowFilterRow="True" ShowFilterRowMenu="True"  />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsNonTabletBagPickups" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="spNonTabletBagPickups_Select"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="spNonTabletBagPickups_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="spNonTabletBagPickups_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="spNonTabletBagPickups_Delete"
                        DeleteCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="EarliestPickupDate" SessionField="EarliestPickupDate" Type="DateTime" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:Parameter Name="PickupCycleID" Type="Int32" />
                            <asp:Parameter Name="PickupDate" Type="DateTime" />
                            <asp:Parameter Name="SectionID" Type="Int32" />
                            <asp:Parameter Name="BaggerID" Type="Int32" />
                            <asp:Parameter Name="CntPutOutsBagger" Type="Int32" />
                            <asp:Parameter Name="DriverID" Type="Int32" />
                            <asp:Parameter Name="PutOuts" Type="Int32" />
                            <asp:Parameter Name="Pickups" Type="Int32" />
                            <asp:Parameter Name="SoftCarts" Type="Double" />
                            <asp:Parameter Name="HardCarts" Type="Double" />
                            <asp:Parameter Name="LocationUnloadedID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PickupID" Type="Int32" />
                            <asp:Parameter Name="PickupScheduleID" Type="Int32" />
                            <asp:Parameter Name="PickupScheduleSectionID" Type="Int32" />
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                            <asp:Parameter Name="PickupCycleID" Type="Int32" />
                            <asp:Parameter Name="PickupDate" Type="DateTime" />
                            <asp:Parameter Name="SectionID" Type="Int32" />
                            <asp:Parameter Name="BaggerID" Type="Int32" />
                            <asp:Parameter Name="CntPutOutsBagger" Type="Int32" />
                            <asp:Parameter Name="DriverID" Type="Int32" />
                            <asp:Parameter Name="PutOuts" Type="Int32" />
                            <asp:Parameter Name="Pickups" Type="Int32" />
                            <asp:Parameter Name="SoftCarts" Type="Double" />
                            <asp:Parameter Name="HardCarts" Type="Double" />
                            <asp:Parameter Name="LocationUnloadedID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="PickupID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRouteSection" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT SectionID, RouteCode + '-' + SectionCode AS [Route-Section] 
                            FROM tblSections AS S
                            INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID
                            LEFT OUTER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.RouteID = R.RouteID
                            LEFT OUTER JOIN tblPickupCycleTemplates AS PCT ON PCT.PickupCycleTemplateID = PCTD.PickupCycleTemplateID
                            WHERE (PCT.ScheduleTypeID IS NULL OR PCT.ScheduleTypeID = 3)
                            ORDER BY [Route-Section]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsBaggers" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT D.DriverID, D.DriverName + ' (' + RG.RegionCode + ')' AS DriverName
                            FROM tblDrivers AS D 
                            INNER JOIN tlkRegions AS RG ON RG.RegionID = D.DriverLocationID
							WHERE D.Active = 1
								AND D.Bagger = 1
                             ORDER BY DriverName">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDriver" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT D.DriverID, D.DriverName + ' (' + RG.RegionCode + ')' AS DriverName
                            FROM tblDrivers AS D 
                            INNER JOIN tlkRegions AS RG ON RG.RegionID = D.DriverLocationID
							WHERE D.Active = 1
								AND D.Driver = 1
                            ORDER BY DriverName">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPickupCycles" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PickupCycleID, PickupCycleAbbr FROM tblPickupCycles ORDER BY PickupCycleAbbr">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsLocations" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
						SelectCommand="SELECT LocationID, LocationAbbr FROM tblLocations ORDER BY LocationAbbr">
                    </asp:SqlDataSource>
                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
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
    </form>
</body>
</html>
