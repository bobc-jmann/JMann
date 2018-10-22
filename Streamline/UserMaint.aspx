<%@ Page Title="User Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="UserMaint.aspx.vb" Inherits="UserMaint" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html>
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
    <script type="text/javascript">
        function OnChanging(s, e) {
            e.reloadContentOnCallback = true;
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="User Maintenance" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" DataSourceID="dsUsers" KeyFieldName="UserID" Width="100%" 
                            EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True" 
                            OnRowUpdating="grdMain_RowUpdating" OnRowInserting="grdMain_RowInserting">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Width="40" ShowEditButton="true"/>
                            <dx:GridViewDataColumn FieldName="UserID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="Active" Visible="True" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1">
                                <EditFormSettings VisibleIndex="1" ColumnSpan="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Username" Visible="True" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2">
                                <EditFormSettings VisibleIndex="2" ColumnSpan="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Password" Visible="False" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="3">
                                <EditFormSettings VisibleIndex="3" ColumnSpan="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="EmailAddress" Visible="True" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="4">
                                <EditFormSettings VisibleIndex="4" ColumnSpan="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserFirstName" Caption="First Name" Visible="True" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="5">
                                <EditFormSettings VisibleIndex="5" ColumnSpan="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserLastName" Caption="Last Name" Visible="True" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="6">
                                <EditFormSettings VisibleIndex="6" ColumnSpan="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserLevel" Visible="False" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="7">
                                <EditFormSettings VisibleIndex="7" ColumnSpan="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UserGroup" Visible="False" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="8">
                                <EditFormSettings VisibleIndex="8" ColumnSpan="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataMemoColumn FieldName="Notes" Visible="False" HeaderStyle-HorizontalAlign="Center" /> 
                        </Columns>
                        <SettingsPager PageSize="200" />
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <Templates>
                            <EditForm>
                                <div style="padding: 4px, 4px, 3px, 4px">
                                    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%">
                                        <TabPages>
                                            <dx:TabPage Text="Info" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server" />
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage Text="Notes" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <dx:ASPxMemo runat="server" ID="notesEditor" Text='<%# Eval("Notes")%>' Width="100%" Height="93px" />
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                        </TabPages>
                                    </dx:ASPxPageControl>
                                </div>
                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server" />
                                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server" />
                                </div>
                            </EditForm>
                            <DetailRow>
                                <div style="padding: 3px 3px 2px 3px">
                                    <dx:ASPxPageControl runat="server" ID="pageControl2" Width="60%" EnableCallBacks="true">
                                        <TabPages>
                                            <dx:TabPage Text="Effective Menu Items" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl2" runat="server">
                                                        <dx:ASPxGridView ID="grdEffectiveMenuItems" runat="server" 
                                                            DataSourceID="dsEffectiveMenuItems" KeyFieldName="SystemMenuItemID" Width="100%"
                                                            OnBeforePerformDataSelect="grdEffectiveMenuItems_DataSelect">
                                                            <Columns>
                                                                <dx:GridViewDataColumn FieldName="MenuItemType" VisibleIndex="1" Caption="Menu Item Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                                                <dx:GridViewDataColumn FieldName="MenuItemText" VisibleIndex="2" Caption="Menu Item" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                                            </Columns>
                                                            <SettingsPager PageSize="100" />
                                                        </dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage Text="Menu Groups" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl3" runat="server">
                                                        <dx:ASPxGridView ID="ASPxGridView2" runat="server" 
                                                            DataSourceID="dsUserMenuGroups" KeyFieldName="UserMenuGroupID" Width="100%" 
                                                            OnBeforePerformDataSelect="grdMenuGroups_DataSelect" SettingsBehavior-ConfirmDelete="True">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Width="80" ShowEditButton="true" ShowDeleteButton="true"/>
                                                                <dx:GridViewDataColumn FieldName="UserMenuGroupID" Visible="false" /> 
                                                                <dx:GridViewDataComboBoxColumn FieldName="SystemMenuGroupID" Caption="Menu Group">
                                                                    <PropertiesComboBox DataSourceID="dsSystemMenuGroups" ValueType="System.Int32" ValueField="SystemMenuGroupID" TextField="MenuGroupDesc" IncrementalFilteringMode="StartsWith" />
                                                                </dx:GridViewDataComboBoxColumn>
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage Text="Menu Items" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <dx:ASPxGridView ID="grdUserMenuItems" runat="server" 
                                                            DataSourceID="dsUserMenuItems" KeyFieldName="UserMenuItemID" Width="100%" 
                                                            OnBeforePerformDataSelect="grdMenuItems_DataSelect" SettingsBehavior-ConfirmDelete="True">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Width="80" ShowEditButton="true" ShowDeleteButton="true"/>
                                                                <dx:GridViewDataColumn FieldName="UserMenuItemID" Visible="false" /> 
                                                                <dx:GridViewDataColumn FieldName="MenuItemType" VisibleIndex="1" Caption="Menu Item Type" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                                                <dx:GridViewDataComboBoxColumn FieldName="SystemMenuItemID" VisibleIndex="2" Caption="Menu Item" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                                    <PropertiesComboBox DataSourceID="dsSystemMenuItems" TextFormatString="{1}" ValueField="SystemMenuItemID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
                                                                        <Columns>
                                                                            <dx:ListBoxColumn FieldName="MenuItemType" />
                                                                            <dx:ListBoxColumn FieldName="MenuItemText" />
                                                                        </Columns>
                                                                    </PropertiesComboBox>
                                                                </dx:GridViewDataComboBoxColumn>
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage Text="Regions" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl1" runat="server">
                                                        <dx:ASPxButton ID="CreateAllRegions" runat="server" Text="Create All Regions" OnClick="CreateAllRegions_Click" />
                                                        <dx:ASPxGridView ID="grdRegions" runat="server" OnInitNewRow="grdUserRegions_InitNewRow"
                                                            DataSourceID="dsUserRegions" KeyFieldName="UserRegionID" Width="100%" 
                                                            OnBeforePerformDataSelect="grdRegions_DataSelect" SettingsBehavior-ConfirmDelete="True"
                                                            OnInit="grdRegions_Init">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Width="80" ShowEditButton="true" ShowDeleteButton="true"/>
                                                                <dx:GridViewDataColumn FieldName="UserRegionID" Visible="false" /> 
                                                                <dx:GridViewDataComboBoxColumn FieldName="SystemRegionID" Caption="Region" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
                                                                    <PropertiesComboBox DataSourceID="dsSystemRegions" ValueType="System.Int32" ValueField="SystemRegionID" TextField="RegionCode" IncrementalFilteringMode="StartsWith" />
                                                                </dx:GridViewDataComboBoxColumn>
                                                                <dx:GridViewDataCheckColumn FieldName="DefaultRegion" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                        </TabPages>
                                        <ClientSideEvents ActiveTabChanging="OnChanging" />
                                    </dx:ASPxPageControl>
                                </div>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsUsers" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="users.spUsers_SelectAll"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="users.spUsers_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="users.spUsers_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="users.spUsers_Delete"
                        DeleteCommandType="StoredProcedure">
                        <InsertParameters>
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="Username" Type="String" />
                            <asp:Parameter Name="Password" Type="String" />
                            <asp:Parameter Name="EmailAddress" Type="String" />
                            <asp:Parameter Name="UserFirstName" Type="String" />
                            <asp:Parameter Name="UserLastName" Type="String" />
                            <asp:Parameter Name="UserLevel" Type="Int32" />
                            <asp:Parameter Name="UserGroup" Type="String" />
                            <asp:Parameter Name="Notes" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                       </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="UserID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="Username" Type="String" />
                            <asp:Parameter Name="Password" Type="String" />
                            <asp:Parameter Name="EmailAddress" Type="String" />
                            <asp:Parameter Name="UserFirstName" Type="String" />
                            <asp:Parameter Name="UserLastName" Type="String" />
                            <asp:Parameter Name="UserLevel" Type="Int32" />
                            <asp:Parameter Name="UserGroup" Type="String" />
                            <asp:Parameter Name="Notes" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="UserID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>

                   <asp:SqlDataSource ID="dsEffectiveMenuItems" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="users.spEffectiveMenuItems_SelectUser"
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsUserMenuGroups" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="users.spMenuGroups_SelectUser"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="users.spMenuGroups_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="users.spMenuGroups_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="users.spMenuGroups_Delete"
                        DeleteCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="SystemMenuGroupID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="UserMenuGroupID" Type="Int32" />
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="SystemMenuGroupID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="UserMenuGroupID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsSystemMenuGroups" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="system.spMenuGroups_SelectAll"
                        SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsUserMenuItems" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="users.spMenuItems_SelectUser"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="users.spMenuItems_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="users.spMenuItems_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="users.spMenuItems_Delete"
                        DeleteCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="SystemMenuItemID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="UserMenuItemID" Type="Int32" />
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="SystemMenuItemID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="UserMenuItemID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsSystemMenuItems" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="system.spMenuItems_SelectAll"
                        SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsUserRegions" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="users.spRegions_SelectUser"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="users.spRegions_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="users.spRegions_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="users.spRegions_Delete"
                        DeleteCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="SystemRegionID" Type="Int32" />
                            <asp:Parameter Name="DefaultRegion" Type="Boolean" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="UserRegionID" Type="Int32" />
                            <asp:SessionParameter Name="UserID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="SystemRegionID" Type="Int32" />
                            <asp:Parameter Name="DefaultRegion" Type="Boolean" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="UserRegionID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsSystemRegions" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT RegionID AS SystemRegionID, RegionCode FROM tlkRegions ORDER BY RegionCode">
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
