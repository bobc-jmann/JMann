<%@ Page Title="Bag/Unbag Exported Sections" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="BagUnbagExportedSections.aspx.vb" Inherits="BagUnbagExportedSections" %>

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
            var height = Math.max(0, document.documentElement.clientHeight) - 400;
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Bag/Unbag Exported Sections" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="50%">
            <tr>
                <td style="width: 50%">Pickup Cycles:
				    <asp:DropDownList ID="ddlPickupCycles" runat="server" ToolTip="Pickup Cycle" Width="100%" AutoPostBack="True">
					</asp:DropDownList>
                    <asp:HiddenField ID="hfSQL_ddlPickupCycles" runat="server" Value=""></asp:HiddenField>
                    <asp:SqlDataSource ID="dsPickupCycles" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
 				</td>
				<td style="width: 50%">
                    <asp:Label ID="lblPickupDate" runat="server" Text="Pickup Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtPickupDate" ClientInstanceName="dtPickupDate"
                        Width="100%" ToolTip="Pickup Date" EditFormat="Custom" 
                        EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                        ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                        ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                        runat="server" AutoPostBack="True">
                        <ValidationSettings SetFocusOnError="True">
                            <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                            <RegularExpression ErrorText="Invalid date"></RegularExpression>
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
            </tr>
        </table>
    </div>

    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 30%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsPickupScheduleSections" KeyFieldName="PickupScheduleID;SectionID" Width="100%" EnableRowsCache="false">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="SectionID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="RouteSection" Caption="Route-Section" HeaderStyle-HorizontalAlign="Center" /> 
                            <dx:GridViewDataCheckColumn FieldName="Bag" HeaderStyle-HorizontalAlign="Center"  /> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsPickupScheduleSections" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        UpdateCommand="spBagUnbagExportedSections"
                        UpdateCommandType="StoredProcedure">
                        <UpdateParameters>
                            <asp:Parameter Name="PickupScheduleID" Type="Int32" />
                            <asp:Parameter Name="SectionID" Type="Int32" />
                            <asp:Parameter Name="Bag" Type="Boolean" />
                       </UpdateParameters>
                    </asp:SqlDataSource>
 
                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 70%" />
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
