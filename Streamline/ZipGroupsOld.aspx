<%@ Page Title="Zip Groups Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ZipGroupsOld.aspx.vb" Inherits="ZipGroupsOld" %>

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
            grdVerify.Refresh();
            AdjustSize();
        }
        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight) - 150;
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Zip Groups Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblVerify" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 60%">
                    <dx:ASPxGridView ID="grdVerify" ClientInstanceName="grdVerify" KeyFieldName="Zip5" runat="server" DataSourceID="dsZipsVerify" 
                        Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="Zip5" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="ZipGroup" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="Message" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" />
                        </Columns>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="100">
                        </SettingsPager>
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsZipsVerify" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="spZipsVerify"
                        SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 40%" />
            </tr>
        </table>

        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 40%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsZipGroups" KeyFieldName="ZipGroupsID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="80" ShowDeleteButton="true"/>
                            <dx:GridViewDataColumn FieldName="ZipGroupsID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="ZipGroup" HeaderStyle-HorizontalAlign="Center" /> 
                            <dx:GridViewDataColumn FieldName="Zip5" /> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsZipGroups" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT ZipGroupsID, ZipGroup, Zip5
                            FROM tblZipGroups
                            ORDER BY ZipGroup, Zip5"
                        InsertCommand="INSERT INTO tblZipGroups (ZipGroup, Zip5) VALUES (@ZipGroup, @Zip5)"
                        UpdateCommand="UPDATE tblZipGroups SET ZipGroup = @ZipGroup, Zip5 = @Zip5 WHERE ZipGroupsID = @ZipGroupsID"
                        DeleteCommand="DELETE FROM tblZipGroups WHERE ZipGroupsID = @ZipGroupsID">
                        <InsertParameters>
                            <asp:Parameter Name="ZipGroup" Type="String" />
                            <asp:Parameter Name="Zip5" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ZipGroupsID" Type="Int32" />
                            <asp:Parameter Name="ZipGroup" Type="String" />
                            <asp:Parameter Name="Zip5" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="ZipGroupsID" Type="Int32" />
                        </DeleteParameters>
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
