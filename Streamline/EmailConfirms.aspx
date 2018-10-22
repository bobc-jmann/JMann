<%@ Page Title="EmailConfirms" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="EmailConfirms.aspx.vb" Inherits="EmailConfirms" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Email Confirms" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsEmailConfirms" 
                            KeyFieldName="PickupScheduleDetailID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="PickupScheduleDetailID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="PickupDate" ReadOnly="true" CellStyle-BackColor="WhiteSmoke" /> 
                            <dx:GridViewDataColumn FieldName="PickupCycleAbbr" Caption="PickupCycle" HeaderStyle-Wrap="True" ReadOnly="true" CellStyle-BackColor="WhiteSmoke" /> 
                            <dx:GridViewDataColumn FieldName="RouteCode" Caption="Route" ReadOnly="true" CellStyle-BackColor="WhiteSmoke" /> 
                            <dx:GridViewDataColumn FieldName="SectionCode" Caption="Section" ReadOnly="true" CellStyle-BackColor="WhiteSmoke" /> 
                            <dx:GridViewDataCheckColumn FieldName="Confirmed" HeaderStyle-HorizontalAlign="Center" Width="70" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="Comments" Width="400" /> 
                            <dx:GridViewDataColumn FieldName="StreetAddress" Width="250" ReadOnly="true" CellStyle-BackColor="WhiteSmoke" /> 
                            <dx:GridViewDataColumn FieldName="City" ReadOnly="true" CellStyle-BackColor="WhiteSmoke" /> 
                            <dx:GridViewDataColumn FieldName="Zip5" Caption="Zip" ReadOnly="true" CellStyle-BackColor="WhiteSmoke" /> 
                       </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsEmailConfirms" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        UpdateCommand="UPDATE tblPickupScheduleDetail SET [Confirmed] = @Confirmed, Comments = REPLACE(@Comments, '''', '''''') WHERE PickupScheduleDetailID = @PickupScheduleDetailID">
                        <UpdateParameters>
                            <asp:Parameter Name="PickupScheduleDetailID" Type="Int32" />
                            <asp:Parameter Name="Confirmed" Type="Boolean" />
                            <asp:Parameter Name="Comments" Type="String" />
                       </UpdateParameters>
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
