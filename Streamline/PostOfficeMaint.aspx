<%@ Page Title="Post Office Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="PostOfficeMaint.aspx.vb" Inherits="PostOfficeMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Post Office Maintenance" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxCheckbox runat="server" ID="ckShowInactivePostOffices" Text="Show Inactive Post Offices" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
            <tr>
                <td style="width: 70%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsPostOffices" 
                            KeyFieldName="PostOfficeID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="40" />
                            <dx:GridViewDataColumn FieldName="PostOfficeID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="PostOfficeCode" /> 
                            <dx:GridViewDataCheckColumn FieldName="Active" HeaderStyle-HorizontalAlign="Center" Width="50" /> 
                            <dx:GridViewDataCheckColumn FieldName="DeliverMonday" HeaderStyle-HorizontalAlign="Center" Width="70" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataCheckColumn FieldName="DeliverTuesday" HeaderStyle-HorizontalAlign="Center" Width="70" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataCheckColumn FieldName="DeliverWednesday" HeaderStyle-HorizontalAlign="Center" Width="70" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataCheckColumn FieldName="DeliverThursday" HeaderStyle-HorizontalAlign="Center" Width="70" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataCheckColumn FieldName="DeliverFriday" HeaderStyle-HorizontalAlign="Center" Width="70" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataCheckColumn FieldName="UseDayBeforeOnHoliday" HeaderStyle-HorizontalAlign="Center" Width="70" HeaderStyle-Wrap="True" /> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsPostOffices" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        InsertCommand="INSERT INTO tblPostOffices (PostOfficeCode, DeliverMonday, DeliverTuesday, DeliverWednesday, DeliverThursday, DeliverFriday, UseDayBeforeOnHoliday, Active) VALUES (@PostOfficeCode, @DeliverMonday, @DeliverTuesday, @DeliverWednesday, @DeliverThursday, @DeliverFriday, @UseDayBeforeOnHoliday, @Active)"
                        UpdateCommand="UPDATE tblPostOffices SET PostOfficeCode = @PostOfficeCode, DeliverMonday = @DeliverMonday, DeliverTuesday = @DeliverTuesday, DeliverWednesday = @DeliverWednesday, DeliverThursday = @DeliverThursday, DeliverFriday = @DeliverFriday, UseDayBeforeOnHoliday = @UseDayBeforeOnHoliday, Active = @Active WHERE PostOfficeID = @PostOfficeID">
                        <InsertParameters>
                            <asp:Parameter Name="PostOfficeCode" Type="String" />
                            <asp:Parameter Name="DeliverMonday" Type="Boolean" />
                            <asp:Parameter Name="DeliverTuesday" Type="Boolean" />
                            <asp:Parameter Name="DeliverWednesday" Type="Boolean" />
                            <asp:Parameter Name="DeliverThursday" Type="Boolean" />
                            <asp:Parameter Name="DeliverFriday" Type="Boolean" />
                            <asp:Parameter Name="UseDayBeforeOnHoliday" Type="Boolean" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PostOfficeID" Type="Int32" />
                            <asp:Parameter Name="PostOfficeCode" Type="String" />
                            <asp:Parameter Name="DeliverMonday" Type="Boolean" />
                            <asp:Parameter Name="DeliverTuesday" Type="Boolean" />
                            <asp:Parameter Name="DeliverWednesday" Type="Boolean" />
                            <asp:Parameter Name="DeliverThursday" Type="Boolean" />
                            <asp:Parameter Name="DeliverFriday" Type="Boolean" />
                            <asp:Parameter Name="UseDayBeforeOnHoliday" Type="Boolean" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                       </UpdateParameters>
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
