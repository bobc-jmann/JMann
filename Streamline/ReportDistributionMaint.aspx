<%@ Page Title="Report Distribution Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ReportDistributionMaint.aspx.vb" Inherits="ReportDistributionMaint" %>

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
            var height = Math.max(0, document.documentElement.clientHeight) - 70;
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Report Distribution Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 70%" colspan="5">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsDistributionList" KeyFieldName="DistributionListID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="True" Width="50" />
                            <dx:GridViewDataColumn FieldName="DistributionListID" Visible="false" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="UserID" Caption="User">
                                <PropertiesComboBox DataSourceID="dsUsers" ValueType="System.Int32" ValueField="UserID" TextField="Username" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="ReportName" Caption="Report Name">
                                <PropertiesComboBox DataSourceID="dsReports" ValueType="System.String" ValueField="Name" TextField="Name" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="RegionID" Caption="Region">
                                <PropertiesComboBox DataSourceID="dsRegions" ValueType="System.Int32" ValueField="RegionID" TextField="RegionDesc" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsDistributionList" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT DL.DistributionListID, DL.UserID, DL.ReportName, DL.RegionID 
                            FROM [Reports].[DistributionList] AS DL
                            INNER JOIN users.Users AS U ON U.UserID = DL.UserID
                            INNER JOIN tlkRegions AS RG ON RG.RegionID = DL.RegionID
                            ORDER BY U.Username, DL.ReportName, RG.RegionDesc"
                        InsertCommand="INSERT INTO [Reports].[DistributionList] ([UserID], [ReportName], [RegionID]) VALUES (@UserID, @ReportName, @RegionID)"
                        UpdateCommand="UPDATE [Reports].[DistributionList] SET [UserID] = @UserID, [ReportName] = @ReportName, [RegionID] = @RegionID WHERE [DistributionListID] = @DistributionListID"
                        DeleteCommand="DELETE FROM [Reports].[DistributionList] WHERE [DistributionListID] = @DistributionListID">
                        <InsertParameters>
                            <asp:Parameter Name="UserID" Type="Int32" />
                            <asp:Parameter Name="ReportName" Type="String" />
                            <asp:Parameter Name="RegionID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="DistributionListID" Type="Int32" />
                            <asp:Parameter Name="UserID" Type="Int32" />
                            <asp:Parameter Name="ReportName" Type="String" />
                            <asp:Parameter Name="RegionID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="DistributionListID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsUsers" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [UserID], [Username] FROM users.Users WHERE [EmailAddress] IS NOT NULL ORDER BY [Username]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsReports" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT DISTINCT C.[Name] FROM [ReportServer].[dbo].[Catalog] AS C 
                            INNER JOIN [ReportServer].[dbo].[ReportSchedule] AS RS ON RS.ReportID = C.ItemID
                            INNER JOIN [ReportServer].[dbo].[Schedule] AS S ON S.ScheduleID = RS.ScheduleID
                            WHERE C.[Type] = 2 
                                AND SUBSTRING(C.[Path], 1, 11) = '/Streamline' 
                                AND S.EventType = 'TimedSubscription' 
                            ORDER BY C.[Name]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRegions" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [RegionID], [RegionDesc] FROM [tlkRegions] ORDER BY [RegionDesc]">
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
