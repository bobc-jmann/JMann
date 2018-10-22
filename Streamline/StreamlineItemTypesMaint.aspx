<%@ Page Title="Streamline Item Types Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="StreamlineItemTypesMaint.aspx.vb" Inherits="StreamlineItemTypesMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Streamline Item Types Maintenance" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxCheckbox runat="server" ID="ckShowInactiveItems" Text="Show Inactive Items" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
            <tr>
                <td style="width: 70%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" 
							DataSourceID="dsItemTypes" 
                            KeyFieldName="ItemTypeID" Width="100%" EnableRowsCache="false" 
							SettingsBehavior-ConfirmDelete="True"
							EnableViewState="false">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="40" />
                            <dx:GridViewDataCheckColumn FieldName="Active" HeaderStyle-HorizontalAlign="Center" Width="60" />
                            <dx:GridViewDataColumn FieldName="ItemType" Width="400" /> 
                            <dx:GridViewDataColumn FieldName="QtyPerMailing" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="MailTypeID" Caption="Mail Type" HeaderStyle-Wrap="True">
                                <PropertiesComboBox DataSourceID="dsMailTypes" ValueType="System.Int32" ValueField="MailTypeID" TextField="MailType" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="CharityID" Caption="Charity" HeaderStyle-Wrap="True">
                                <PropertiesComboBox DataSourceID="dsCharities" ValueType="System.Int32" ValueField="CharityID" TextField="CharityAbbr" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="LeadTime" Caption="Lead Time (in weeks)" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" /> 
                       </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="200" AlwaysShowPager="True" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsItemTypes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        InsertCommand="Streamline.spItemTypes_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="Streamline.spItemTypes_Update"
                        UpdateCommandType="StoredProcedure">
                        <InsertParameters>
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="ItemType" Type="String" />
                            <asp:Parameter Name="QtyPerMailing" Type="Decimal" />
                            <asp:Parameter Name="MailTypeID" Type="Int32" />
                            <asp:Parameter Name="CharityID" Type="Int32" />
                            <asp:Parameter Name="LeadTime" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ItemTypeID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="ItemType" Type="String" />
                            <asp:Parameter Name="QtyPerMailing" Type="Decimal" />
                            <asp:Parameter Name="MailTypeID" Type="Int32" />
                            <asp:Parameter Name="CharityID" Type="Int32" />
                            <asp:Parameter Name="LeadTime" Type="Int32" />
                       </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsMailTypes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT MailTypeID, MailType FROM Streamline.MailTypes 
                            UNION SELECT 0 AS MailTypeID, 'All' AS MailType 
                            UNION SELECT -1 AS MailTypeID, 'None' AS MailType 
                            ORDER BY MailType">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsCharities" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT CharityID, CharityAbbr FROM tblCharities WHERE Active = 1 UNION SELECT 0 AS CharityID, 'All' AS CharityAbbr ORDER BY CharityAbbr">
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
