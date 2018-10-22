<%@ Page Title="Zips Daily Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ZipsDaily.aspx.vb" Inherits="ZipsDaily" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="glob" runat="server">
        <ClientSideEvents ControlsInitialized="function (s, e) { jsCallHideLoadingPanel(); }" />
    </dx:ASPxGlobalEvents>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Zips Daily Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
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
                <td style="width: 20%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" KeyFieldName="ZipsDailyID" runat="server" DataSourceID="dsZipsDaily" 
                        Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                             <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="80" ShowDeleteButton="true"/>
                             <dx:GridViewDataColumn FieldName="ZipsDailyID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="Zip5" VisibleIndex="1" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" />
                        </Columns>
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="100"></SettingsPager>
                        <SettingsEditing Mode="Batch" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsZipsDaily" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT ZipsDailyID, Zip5 FROM tblZipsDaily ORDER BY Zip5"
                        InsertCommandType="StoredProcedure"
                        InsertCommand="spZipsDaily_Insert"
                        UpdateCommandType="StoredProcedure"
                        UpdateCommand="spZipsDaily_Update"
                        DeleteCommandType="StoredProcedure"
                        DeleteCommand="spZipsDaily_Delete">
                        <UpdateParameters>
                            <asp:Parameter Name="ZipsDailyID" Type="Int32" />
                            <asp:Parameter Name="Zip5" Type="Int32" />
                        </UpdateParameters>
                            <InsertParameters>
                            <asp:Parameter Name="Zip5" Type="Int32" />
                        </InsertParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="ZipsDailyID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                </td>
                <td style="width: 80%" />
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>
