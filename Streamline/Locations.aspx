<%@ Page Title="Locations" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="Locations.aspx.vb" Inherits="Locations" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Locations" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsLocations" 
						KeyFieldName="LocationID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="40" />
                            <dx:GridViewDataColumn FieldName="LocationID" Visible="false" /> 
                            <dx:GridViewDataCheckColumn FieldName="Active" HeaderStyle-HorizontalAlign="Center" Width="50" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="RegionID" Caption="Region"  Width="50">
                                <PropertiesComboBox DataSourceID="dsRegions" ValueType="System.Int32" ValueField="RegionID" TextField="RegionCode" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="LocationAbbr" Width="100" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="LocationDesc" /> 
                            <dx:GridViewDataCheckColumn FieldName="DefaultLocation" Width="70" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataCheckColumn FieldName="OutsideLocation" Width="70" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataCheckColumn FieldName="InventoryLocation" Width="70" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="FinanceLocationID" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" Width="50" /> 
                            <dx:GridViewDataColumn FieldName="ThriftOSStoreID" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" Width="50" /> 
                            <dx:GridViewDataColumn FieldName="AccountingDescription" Caption="Accounting Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="AccountingDescriptionSortOrder" Caption="Accounting Sort Order" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" Width="75" /> 
                        </Columns>
						<SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="50" />
                        <Settings VerticalScrollableHeight="0" VerticalScrollBarMode="Auto" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsLocations" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
						SelectCommandType="StoredProcedure"
						SelectCommand="spLocations_SelectAll"
						InsertCommandType="StoredProcedure"
                        InsertCommand="spLocations_Insert"
						UpdateCommandType="StoredProcedure"
                        UpdateCommand="spLocations_Update">
                        <InsertParameters>
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="RegionID" Type="Int32" />
                            <asp:Parameter Name="LocationAbbr" Type="String" />
                            <asp:Parameter Name="LocationDesc" Type="String" />
                            <asp:Parameter Name="DefaultLocation" Type="Boolean" />
                            <asp:Parameter Name="OutsideLocation" Type="Boolean" />
                            <asp:Parameter Name="InventoryLocation" Type="Boolean" />
                            <asp:Parameter Name="FinanceLocationID" Type="Int32" />
                            <asp:Parameter Name="ThriftOSStoreID" Type="Int32" />
                            <asp:Parameter Name="AccountingDescription" Type="String" />
                            <asp:Parameter Name="AccountingDescriptionSortOrder" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="LocationID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="RegionID" Type="Int32" />
                            <asp:Parameter Name="LocationAbbr" Type="String" />
                            <asp:Parameter Name="LocationDesc" Type="String" />
                            <asp:Parameter Name="DefaultLocation" Type="Boolean" />
                            <asp:Parameter Name="OutsideLocation" Type="Boolean" />
                            <asp:Parameter Name="InventoryLocation" Type="Boolean" />
                            <asp:Parameter Name="FinanceLocationID" Type="Int32" />
                            <asp:Parameter Name="ThriftOSStoreID" Type="Int32" />
                            <asp:Parameter Name="AccountingDescription" Type="String" />
                            <asp:Parameter Name="AccountingDescriptionSortOrder" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRegions" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
						SelectCommand="SELECT RegionID, RegionCode FROM tlkRegions ORDER BY RegionCode">
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
