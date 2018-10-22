<%@ Page Title="Mailing Date Rules Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="MailingDateRulesMaint.aspx.vb" Inherits="MailingDateRulesMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Mailing Date Rules Maintenance" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsMailingDateRules" 
                            KeyFieldName="MailingDateRulesID" Width="100%" EnableRowsCache="false" 
							SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="true" Width="100"
								ShowClearFilterButton="True" />
                            <dx:GridViewDataColumn FieldName="MailingDateRulesID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="City" /> 
                            <dx:GridViewDataColumn FieldName="Zip" HeaderStyle-HorizontalAlign="Right" /> 
                            <dx:GridViewDataColumn FieldName="DeliveryDays" HeaderStyle-HorizontalAlign="Right" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="PostOfficeID" Caption="Post Office" >
                                <PropertiesComboBox DataSourceID="dsPostOffices" TextField="PostOfficeCode" ValueField="PostOfficeID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="PostOfficesToTransfer" HeaderStyle-HorizontalAlign="Right" /> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="True" ShowFooter="True" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsMailingDateRules" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT MailingDateRulesID, City, Zip, DeliveryDays, PostOfficeID, PostOfficesToTransfer FROM tblMailingDateRules ORDER BY City, Zip"
                        InsertCommand="INSERT INTO tblMailingDateRules (City, Zip, DeliveryDays, PostOfficeID, PostOfficesToTransfer) VALUES (UPPER(@City), @Zip, @DeliveryDays, @PostOfficeID, @PostOfficesToTransfer)"
                        UpdateCommand="UPDATE tblMailingDateRules SET City = UPPER(@City), Zip = @Zip, DeliveryDays = @DeliveryDays, PostOfficeID = @PostOfficeID, PostOfficesToTransfer = @PostOfficesToTransfer WHERE MailingDateRulesID = @MailingDateRulesID"
                        DeleteCommand="DELETE FROM tblMailingDateRules WHERE MailingDateRulesID = @MailingDateRulesID">
                        <InsertParameters>
                            <asp:Parameter Name="City" Type="String" />
                            <asp:Parameter Name="Zip" Type="Int32" />
                            <asp:Parameter Name="DeliveryDays" Type="Int32" />
                            <asp:Parameter Name="PostOfficeID" Type="Int32" />
                            <asp:Parameter Name="PostOfficesToTransfer" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="MailingDateRulesID" Type="Int32" />
                            <asp:Parameter Name="City" Type="String" />
                            <asp:Parameter Name="Zip" Type="Int32" />
                            <asp:Parameter Name="DeliveryDays" Type="Int32" />
                            <asp:Parameter Name="PostOfficeID" Type="Int32" />
                            <asp:Parameter Name="PostOfficesToTransfer" Type="Int32" />
						</UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="MailingDateRulesID" Type="Int32" />
						</DeleteParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsPostOffices" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PostOfficeID, PostOfficeCode FROM tblPostOffices ORDER BY PostOfficeCode">
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
