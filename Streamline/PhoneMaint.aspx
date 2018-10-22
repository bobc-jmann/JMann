<%@ Page Title="Phone Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="PhoneMaint.aspx.vb" Inherits="PhoneMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Phone Maintenance" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" 
						DataSourceID="dsPhones" KeyFieldName="PhoneID" Width="100%" 
						EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True"
						EnableViewState="false">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="40" />
                            <dx:GridViewDataColumn FieldName="PhoneID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="Active" HeaderStyle-HorizontalAlign="Center" Width="50" /> 
                            <dx:GridViewDataColumn FieldName="PhoneNumber" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="TextingDomain" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="DirectConnectNumber" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="DriverLocationID" Caption="Region">
                                <PropertiesComboBox DataSourceID="dsRegions" ValueType="System.Int32" ValueField="RegionID" TextField="RegionCode" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="200" AlwaysShowPager="True" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsPhones" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        InsertCommand="INSERT INTO tblPhones (PhoneNumber, TextingDomain, DirectConnectNumber, DriverLocationID, Active) VALUES (@PhoneNumber, @TextingDomain, @DirectConnectNumber, @DriverLocationID, @Active)"
                        UpdateCommand="UPDATE tblPhones SET PhoneNumber = @PhoneNumber, TextingDomain = @TextingDomain, DirectConnectNumber = @DirectConnectNumber, DriverLocationID = @DriverLocationID, Active = @Active WHERE PhoneID = @PhoneID">
                        <InsertParameters>
                            <asp:Parameter Name="PhoneNumber" Type="String" />
                            <asp:Parameter Name="TextingDomain" Type="String" />
                            <asp:Parameter Name="DirectConnectNumber" Type="String" />
                            <asp:Parameter Name="DriverLocationID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PhoneID" Type="Int32" />
                            <asp:Parameter Name="PhoneNumber" Type="String" />
                            <asp:Parameter Name="TextingDomain" Type="String" />
                            <asp:Parameter Name="DirectConnectNumber" Type="String" />
                            <asp:Parameter Name="DriverLocationID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRegions" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
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
