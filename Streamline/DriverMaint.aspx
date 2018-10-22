<%@ Page Title="Driver Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="DriverMaint.aspx.vb" Inherits="DriverMaint" %>

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
            var height = Math.max(0, document.documentElement.clientHeight) - 110;
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Driver Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckShowInactiveDrivers" Text="Show Inactive Drivers" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" 
							DataSourceID="dsDrivers" 
                            KeyFieldName="DriverID" Width="100%" EnableRowsCache="false" 
							SettingsBehavior-ConfirmDelete="True"
							EnableViewState="false">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="40" />
                            <dx:GridViewDataColumn FieldName="Active" HeaderStyle-HorizontalAlign="Center" Width="50" /> 
                            <dx:GridViewDataColumn FieldName="FirstName" /> 
                            <dx:GridViewDataColumn FieldName="LastName" /> 
							<dx:GridViewDataCheckColumn FieldName="Driver" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" />
							<dx:GridViewDataCheckColumn FieldName="Bagger" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" />
                            <dx:GridViewDataComboBoxColumn FieldName="TabletID" Caption="Tablet">
                                <PropertiesComboBox DataSourceID="dsTablets" ValueType="System.Int32" ValueField="TabletID" TextField="TabletName" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="PhoneID" Caption="Phone">
                                <PropertiesComboBox DataSourceID="dsPhones" ValueType="System.Int32" ValueField="PhoneID" TextField="PhoneNumber" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="TruckID" Caption="Truck">
                                <PropertiesComboBox DataSourceID="dsTrucks" ValueType="System.Int32" ValueField="TruckID" TextField="TruckNumber" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="DeviceDescription" Caption="Geotab Device" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="DriverLocationID" Caption="Region">
                                <PropertiesComboBox DataSourceID="dsRegions" ValueType="System.Int32" ValueField="RegionID" TextField="RegionCode" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="DriverID" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" /> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="200" AlwaysShowPager="True" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsDrivers" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        InsertCommand="INSERT INTO tblDrivers (FirstName, LastName, Driver, Bagger, TabletID, PhoneID, TruckID, DriverLocationID, Active) VALUES (@FirstName, @LastName, @Driver, @Bagger, @TabletID, @PhoneID, @TruckID, @DriverLocationID, @Active)"
                        UpdateCommand="UPDATE tblDrivers SET FirstName = @FirstName, LastName = @LastName, Driver = @Driver, Bagger = @Bagger, TabletID = @TabletID, PhoneID = @PhoneID, TruckID = @TruckID, DriverLocationID = @DriverLocationID, Active = @Active WHERE DriverID = @DriverID">
                        <InsertParameters>
                            <asp:Parameter Name="FirstName" Type="String" />
                            <asp:Parameter Name="LastName" Type="String" />
                            <asp:Parameter Name="Driver" Type="Boolean" />
                            <asp:Parameter Name="Bagger" Type="Boolean" />
                            <asp:Parameter Name="TabletID" Type="Int32" />
                            <asp:Parameter Name="PhoneID" Type="Int32" />
                            <asp:Parameter Name="TruckID" Type="Int32" />
                            <asp:Parameter Name="DriverLocationID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="DriverID" Type="Int32" />
                            <asp:Parameter Name="FirstName" Type="String" />
                            <asp:Parameter Name="LastName" Type="String" />
                            <asp:Parameter Name="Driver" Type="Boolean" />
                            <asp:Parameter Name="Bagger" Type="Boolean" />
                            <asp:Parameter Name="TabletID" Type="Int32" />
                            <asp:Parameter Name="PhoneID" Type="Int32" />
                            <asp:Parameter Name="TruckID" Type="Int32" />
                            <asp:Parameter Name="DriverLocationID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                       </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsTablets" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT TabletID, TabletName FROM tblTablets WHERE Active = 1 UNION SELECT 0 AS TabletID, '' AS TabletName ORDER BY TabletName">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPhones" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PhoneID, PhoneNumber FROM tblPhones WHERE Active = 1 UNION SELECT 0 AS PhoneID, '' AS PhoneNumber ORDER BY PhoneNumber">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsTrucks" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT TruckID, TruckNumber FROM tblTrucks WHERE Active = 1 UNION SELECT 0 AS TruckID, '' AS TruckNumber ORDER BY TruckNumber">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRegions" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 0%" />
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
