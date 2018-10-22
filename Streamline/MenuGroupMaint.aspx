<%@ Page Title="Menu Group Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="MenuGroupMaint.aspx.vb" Inherits="MenuGroupMaint" %>

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
</head>

<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="glob" runat="server">
        <ClientSideEvents ControlsInitialized="function (s, e) { jsCallHideLoadingPanel(); }" />
    </dx:ASPxGlobalEvents>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Menu Group Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divMain" class="specials" runat="server">
        <table id="tblMain" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 50%">
                    <dx:ASPxGridView ID="grdMain" KeyFieldName="SystemMenuGroupID" runat="server" DataSourceID="dsMenuGroups" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="120" ShowNewButtonInHeader="True" ShowDeleteButton="true" ShowEditButton="true"/>
                            <dx:GridViewDataColumn FieldName="SystemMenuGroupID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="MenuGroupDesc" VisibleIndex="1" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" />
                        </Columns>
                        <SettingsEditing EditFormColumnCount="2"/>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="100">
                        </SettingsPager>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="grdDetail" KeyFieldName="SystemMenuGroupItemID" runat="server" DataSourceID="dsMenuGroupItems" Width="100%" OnBeforePerformDataSelect="grdMain_DataSelect" SettingsBehavior-ConfirmDelete="True">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Caption=" " ShowEditButton="true" ShowDeleteButton="true"/>
                                        <dx:GridViewDataColumn FieldName="SystemMenuGroupItemID" Visible="False" /> 
                                        <dx:GridViewDataColumn FieldName="MenuItemType" VisibleIndex="1" Caption="Menu Item Type" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                        <dx:GridViewDataComboBoxColumn FieldName="SystemMenuItemID" VisibleIndex="2" Caption="Menu Item" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                            <PropertiesComboBox DataSourceID="dsMenuItems" TextFormatString="{1}" ValueField="SystemMenuItemID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" DropDownRows="20">
                                                <Columns>
                                                    <dx:ListBoxColumn FieldName="MenuItemType" />
                                                    <dx:ListBoxColumn FieldName="MenuItemText" />
                                                </Columns>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                    </Columns>  
                                    <SettingsPager PageSize="100" />
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsMenuGroups" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="system.spMenuGroups_SelectAll"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="system.spMenuGroups_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="system.spMenuGroups_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="system.spMenuGroups_Delete"
                        DeleteCommandType="StoredProcedure">
                        <InsertParameters>
                            <asp:Parameter Name="MenuGroupDesc" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="SystemMenuGroupID" Type="Int32" />
                            <asp:Parameter Name="MenuGroupDesc" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="SystemMenuGroupID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsMenuGroupItems" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="system.spMenuGroupItems_SelectGroup"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="system.spMenuGroupItems_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="system.spMenuGroupItems_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="system.spMenuGroupItems_Delete"
                        DeleteCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="SystemMenuGroupID" SessionField="MasterID" Type="Int32" />
                       </SelectParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="SystemMenuGroupID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="SystemMenuItemID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="SystemMenuGroupItemID" Type="Int32" />
                            <asp:SessionParameter Name="SystemMenuGroupID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="SystemMenuItemID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="SystemMenuGroupItemID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsMenuItems" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="system.spMenuItems_SelectAll"
                        SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
               </td>
                <td style="width: 50%"></td>
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
