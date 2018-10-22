<%@ Page Title="Optimum Mail Delivery Days Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="OptimumMailDeliveryDaysMaint.aspx.vb" Inherits="OptimumMailDeliveryDaysMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Optimum Mail Delivery Days Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 40%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsOptimumMailDeliveryDays" 
                            KeyFieldName="OptimumMailDeliveryDaysID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn  Width="40" ShowEditButton="True" />
                            <dx:GridViewDataColumn FieldName="OptimumMailDeliveryDaysID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="PickupDay" EditFormSettings-Visible="False" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="OptimumDeliveryDay" EditFormSettings-Visible="True"  HeaderStyle-HorizontalAlign="Center" >
                                <PropertiesComboBox DataSourceID="dsOptimum" TextField="PickupDay" ValueField="PickupDay" ValueType="System.String" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                        </Columns>
                        <SettingsEditing Mode="EditForm" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsOptimumMailDeliveryDays" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT OptimumMailDeliveryDaysID, PickupDay, OptimumDeliveryDay FROM tblOptimumMailDeliveryDays ORDER BY OptimumMailDeliveryDaysID"
                        UpdateCommand="UPDATE tblOptimumMailDeliveryDays SET OptimumDeliveryDay = @OptimumDeliveryDay WHERE OptimumMailDeliveryDaysID = @OptimumMailDeliveryDaysID">
                        <UpdateParameters>
                            <asp:Parameter Name="OptimumMailDeliveryDaysID" Type="Int32" />
                            <asp:Parameter Name="PickupDay" Type="String" />
                            <asp:Parameter Name="OptimumDeliveryDay" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsOptimum" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT 'Monday' AS PickupDay, 2 AS SortCode
                            UNION
                            SELECT 'Tuesday' AS PickupDay, 3 AS SortCode
                            UNION
                            SELECT 'Wednesday' AS PickupDay, 4 AS SortCode
                            UNION
                            SELECT 'Thursday' AS PickupDay, 5 AS SortCode
                            UNION
                            SELECT 'Friday' AS PickupDay, 6 AS SortCode
                            UNION
                            SELECT 'Saturday' AS PickupDay, 7 AS SortCode
                            ORDER BY SortCode">
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 60%" />
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
