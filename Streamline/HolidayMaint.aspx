<%@ Page Title="Holiday Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="HolidayMaint.aspx.vb" Inherits="HolidayMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Holiday Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 65%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" 
						DataSourceID="dsHolidays" KeyFieldName="HolidayDate" Width="100%" EnableRowsCache="false" 
						SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="True" />
                            <dx:GridViewDataColumn FieldName="Holiday" HeaderStyle-Wrap="True" Width="200" /> 
                            <dx:GridViewDataDateColumn FieldName="HolidayDate" /> 
                            <dx:GridViewDataDateColumn FieldName="RescheduleToDate" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="SlowDownPeriodBefore" HeaderStyle-Wrap="True" Width="60" /> 
                            <dx:GridViewDataColumn FieldName="SlowDownPeriodAfter" HeaderStyle-Wrap="True" Width="60" /> 
                            <dx:GridViewDataColumn FieldName="SlowDownDays" HeaderStyle-Wrap="True" Width="60" /> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
						<SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsHolidays" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
						SelectCommand="spHolidays_SelectAll"
						SelectCommandType="StoredProcedure"
                        InsertCommand="spHolidays_Insert"
						InsertCommandType="StoredProcedure"
                        UpdateCommand="spHolidays_Update"
						UpdateCommandType="StoredProcedure"
                        DeleteCommand="spHolidays_Delete"
						DeleteCommandType="StoredProcedure">
                        <InsertParameters>
                            <asp:Parameter Name="HolidayDate" Type="DateTime" />
                            <asp:Parameter Name="Holiday" Type="String" />
                            <asp:Parameter Name="RescheduleToDate" Type="DateTime" />
                            <asp:Parameter Name="SlowDownPeriodBefore" Type="Int32" />
                            <asp:Parameter Name="SlowDownPeriodAfter" Type="Int32" />
                            <asp:Parameter Name="SlowDownDays" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
						</InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="HolidayDate" Type="DateTime" />
                            <asp:Parameter Name="Holiday" Type="String" />
                            <asp:Parameter Name="RescheduleToDate" Type="DateTime" />
                            <asp:Parameter Name="SlowDownPeriodBefore" Type="Int32" />
                            <asp:Parameter Name="SlowDownPeriodAfter" Type="Int32" />
                            <asp:Parameter Name="SlowDownDays" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
                      </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="HolidayDate" Type="DateTime" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
                        </DeleteParameters>
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 35%" />
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
