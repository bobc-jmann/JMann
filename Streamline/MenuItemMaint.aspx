<%@ Page Title="Menu Item Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="MenuItemMaint.aspx.vb" Inherits="MenuItemMaint" %>

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
            grdMain.SetHeight(height);
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Menu Item Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 70%">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" DataSourceID="dsMenuItems" KeyFieldName="SystemMenuItemID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="50" ShowNewButtonInHeader="True" ShowDeleteButton="true"/>
                            <dx:GridViewDataColumn FieldName="SystemMenuItemID" Visible="false" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="MenuItemType" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
                                <PropertiesComboBox DataSourceID="dsMenuItemTypes" ValueType="System.String" ValueField="MenuItemType" TextField="MenuItemType" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="MenuItemName" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="MenuItemText" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="SortCode" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="200" />
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="0" />
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="grdDetail" KeyFieldName="UserID" runat="server" DataSourceID="dsMenuItemUsers" Width="60%" OnBeforePerformDataSelect="grdMain_DataSelect" SettingsBehavior-ConfirmDelete="True">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="UserID" Visible="False" /> 
                                        <dx:GridViewDataColumn FieldName="Username" VisibleIndex="1" Caption="Username" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                        <dx:GridViewDataColumn FieldName="UserFirstName" VisibleIndex="2" Caption="First Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                        <dx:GridViewDataColumn FieldName="UserLastName" VisibleIndex="3" Caption="Last Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                    </Columns>  
                                    <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="200" />
                                    <SettingsPager PageSize="100" />
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                            <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsMenuItems" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="system.spMenuItems_SelectAll"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="system.spMenuItems_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="system.spMenuItems_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="system.spMenuItems_Delete"
                        DeleteCommandType="StoredProcedure">
                        <InsertParameters>
                            <asp:Parameter Name="MenuItemName" Type="String" />
                            <asp:Parameter Name="MenuItemText" Type="String" />
                            <asp:Parameter Name="MenuItemType" Type="String" />
                            <asp:Parameter Name="SortCode" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="SystemMenuItemID" Type="Int32" />
                            <asp:Parameter Name="MenuItemName" Type="String" />
                            <asp:Parameter Name="MenuItemText" Type="String" />
                            <asp:Parameter Name="MenuItemType" Type="String" />
                            <asp:Parameter Name="SortCode" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="SystemMenuItemID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsMenuItemUsers" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="system.spMenuItems_SelectUsers"
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="SystemMenuItemID" SessionField="MasterID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsMenuItemTypes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT MenuItemType FROM system.MenuItemTypes ORDER BY MenuItemType">
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 30%" />
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
