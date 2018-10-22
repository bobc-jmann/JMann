<%@ Page Title="Streamline Items Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="StreamlineItemsMaint.aspx.vb" Inherits="StreamlineItemsMaint" %>

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
            var height = Math.max(0, document.documentElement.clientHeight) - 130;
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Streamline Items Maintenance" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxCheckbox runat="server" ID="ckShowInactiveItems" Text="Show Inactive Items" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" 
							DataSourceID="dsItems" 
                            KeyFieldName="ItemID" Width="100%" EnableRowsCache="false" 
							SettingsBehavior-ConfirmDelete="True"
						    OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared" 
							EnableViewState="false">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="40" />
                            <dx:GridViewDataCheckColumn FieldName="Active" HeaderStyle-HorizontalAlign="Center" Width="60" />
                            <dx:GridViewDataColumn FieldName="ItemCode" /> 
                            <dx:GridViewDataColumn FieldName="ItemDescription" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="ItemTypeID" Caption="Item Type" HeaderStyle-Wrap="True">
                                <PropertiesComboBox DataSourceID="dsItemTypes" ValueType="System.Int32" ValueField="ItemTypeID" TextField="ItemType" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="OnHand" Width="80" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right">
                                <PropertiesTextEdit DisplayFormatString="#,##0" />
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn FieldName="EstimatedRunoutDate" Width="100" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="LeadTime" Caption="Lead Time (in weeks)" Width="100" 
                                HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
                  </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="200" AlwaysShowPager="True" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsItems" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        InsertCommand="Streamline.spItems_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="Streamline.spItems_Update"
                        UpdateCommandType="StoredProcedure">
                        <InsertParameters>
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="ItemCode" Type="String" />
                            <asp:Parameter Name="ItemDescription" Type="String" />
                            <asp:Parameter Name="ItemTypeID" Type="Int32" />
                            <asp:Parameter Name="OnHand" Type="Int32" />
                            <asp:Parameter Name="EstimatedRunoutDate" Type="DateTime" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ItemID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="ItemCode" Type="String" />
                            <asp:Parameter Name="ItemDescription" Type="String" />
                            <asp:Parameter Name="ItemTypeID" Type="Int32" />
                            <asp:Parameter Name="OnHand" Type="Int32" />
                            <asp:Parameter Name="EstimatedRunoutDate" Type="DateTime" />
                       </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsItemTypes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT ItemTypeID, ItemType FROM Streamline.ItemTypes ORDER BY ItemType">
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
