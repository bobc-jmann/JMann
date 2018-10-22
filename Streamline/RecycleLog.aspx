<%@ Page Title="Recycle Log" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="RecycleLog.aspx.vb" Inherits="RecycleLog" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Recycle Log" runat="server"></dx:ASPxLabel></td>
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
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsRecycleLog" KeyFieldName="RecycleLogID" Width="100%" EnableRowsCache="false" >
                        <Columns>
                            <dx:GridViewDataColumn FieldName="RecycleLogID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="RecycleDate" Width="75" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="SectionID" Width="100" Caption="Route-Section" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesComboBox DataSourceID="dsRouteSections" ValueType="System.Int32" ValueField="SectionID" TextField="Route-Section" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="StartCurrent" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="StartBackup" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="StartTotal" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="NumDonors" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="NumLowMail" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="NumDonorsLowMail" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="ProjSizeSizeChangePercent" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="ProjSizeNonDonorPercent" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="NewSize" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="NewSizeFactor"  Width="125" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="EndCurrent" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="EndBackup" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="EndTotal" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="NumberOfTimesScheduled" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="FirstMailingDate" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataColumn FieldName="LastMailingDate" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                        </Columns>
                        <SettingsPager PageSize="100" />
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="False" ShowFooter="True" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsRecycleLog" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRouteSections" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT SectionID, RouteCode + '-' + SectionCode AS [Route-Section] 
                            FROM tblSections AS S
                            INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID
                            ORDER BY RouteCode + '-' + SectionCode">
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
            </tr>
        </table>     
    </div>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblPickupDateMin" runat="server" Text="Earliest Pickup Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtEarliestRecycleDate"
                        Width="80%" ToolTip="Earliest Recycle Date" EditFormat="Custom" 
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

    </form>
</body>
</html>
