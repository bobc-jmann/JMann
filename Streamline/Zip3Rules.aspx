<%@ Page Title="Zip 3 Rules" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="Zip3Rules.aspx.vb" Inherits="Zip3Rules" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Zip 3 Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 80%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsZip3Rules" KeyFieldName="Zip3ID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="40" />
                            <dx:GridViewDataColumn FieldName="Zip3ID" visible="false" /> 
                            <dx:GridViewDataColumn FieldName="Zip3" /> 
                            <dx:GridViewDataColumn FieldName="Community" /> 
                            <dx:GridViewDataColumn FieldName="MailDaysMax" /> 
                            <dx:GridViewDataColumn FieldName="CloseDays" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="PostOfficeID" Caption="Post Office">
                                <PropertiesComboBox DataSourceID="dsPostOffices" ValueType="System.Int32" ValueField="PostOfficeID" TextField="PostOfficeCode" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataMemoColumn FieldName="Comments" /> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="50" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsZip3Rules" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT Zip3ID, Zip3, Community, MailDaysMax, CloseDays, PostOfficeID, Comments
                            FROM tlkZip3Rules
                            ORDER BY Zip3"
                        InsertCommand="INSERT INTO tlkZip3Rules (Zip3, Community, MailDaysMax, CloseDays, PostOfficeID, Comments) VALUES (@Zip3, @Community, @MailDaysMax, @CloseDays, @PostOfficeID, REPLACE(@Comments, '''', ''''''))"
                        UpdateCommand="UPDATE tlkZip3Rules SET Community = @Community, MailDaysMax = @MailDaysMax, CloseDays = @CloseDays, PostOfficeID = @PostOfficeID, Comments = REPLACE(@Comments, '''', '''''') WHERE Zip3ID = @Zip3ID">
                        <InsertParameters>
                            <asp:Parameter Name="Zip3" Type="Int32" />
                            <asp:Parameter Name="Community" Type="String" />
                            <asp:Parameter Name="MailDaysMax" Type="String" />
                            <asp:Parameter Name="CloseDays" Type="String" />
                            <asp:Parameter Name="PostOfficeID" Type="Int32" />
                            <asp:Parameter Name="Comments" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Zip3ID" Type="Int32" />
                            <asp:Parameter Name="Zip3" Type="Int32" />
                            <asp:Parameter Name="Community" Type="String" />
                            <asp:Parameter Name="MailDaysMax" Type="String" />
                            <asp:Parameter Name="CloseDays" Type="String" />
                            <asp:Parameter Name="PostOfficeID" Type="Int32" />
                            <asp:Parameter Name="Comments" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPostOffices" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PostOfficeID, PostOfficeCode FROM tblPostOffices
                            UNION SELECT NULL AS PostOfficeID, '' AS PostOfficeCode
                            ORDER BY PostOfficeCode">
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 20%" />
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
