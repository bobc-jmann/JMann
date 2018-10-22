<%@ Page Title="Schedule Counts" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ScheduleCounts.aspx.vb" Inherits="ScheduleCounts" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
    		var height = Math.max(0, document.documentElement.clientHeight) - 150;
    		gridMain.SetHeight(height);
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Schedule Counts" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
          <table id="tblSchedule" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">
                   <dx:ASPxGridView KeyFieldName="PickupScheduleID" ID="gridMain" 
                       OnHtmlDataCellPrepared="vbOnHtmlDataCellPrepared" ClientInstanceName="gridMain" runat="server" 
                       DataSourceID="dsGridMain" Width="100%">
						<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" />
						<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
						<Columns>
							<dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="False" /> 
							<dx:GridViewDataColumn FieldName="PickupCycleID" Visible="False" /> 
							<dx:GridViewDataColumn FieldName="ScheduleTypeID" Visible="False" /> 
							<dx:GridViewDataColumn FieldName="PermitID" Visible="False" /> 
							<dx:GridViewDataColumn FieldName="PickupCycleAbbr" VisibleIndex="1" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" />
							<dx:GridViewDataColumn FieldName="PickupCycleWeek" VisibleIndex="2" Caption="Week"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
							<dx:GridViewDataColumn FieldName="PickupCycleDay" VisibleIndex="3" Caption="Day" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
							<dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
							<dx:GridViewDataColumn FieldName="City" Width="30%" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" />
							<dx:GridViewDataColumn FieldName="MailingDate" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
							<dx:GridViewDataColumn FieldName="PickupDate" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
							<dx:GridViewDataColumn FieldName="PermitNbr" VisibleIndex="9"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" />
							<dx:GridViewDataTextColumn FieldName="MailCount" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								<PropertiesTextEdit ClientInstanceName="MailCount" DisplayFormatString="#,#" />
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="EmailCount" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								<PropertiesTextEdit ClientInstanceName="EmailCount" DisplayFormatString="#,#" />
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="BagCount" VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								<PropertiesTextEdit ClientInstanceName="BagCount" DisplayFormatString="#,#" />
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="PostcardCount" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								<PropertiesTextEdit ClientInstanceName="PostcardCount" DisplayFormatString="#,#" />
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="CntMailNR" Caption="Non-Route Mail Count" VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								<PropertiesTextEdit ClientInstanceName="PostcardCount" DisplayFormatString="#,#" />
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataTextColumn FieldName="CntEmailNR" Caption="Non-Route Email Count" VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								<PropertiesTextEdit ClientInstanceName="PostcardCount" DisplayFormatString="#,#" />
							</dx:GridViewDataTextColumn>
						</Columns>
						<Settings ShowGroupPanel="True" ShowFooter="True" />
						<SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="200" AlwaysShowPager="True" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
						<GroupSummary>
							<dx:ASPxSummaryItem FieldName="MailingDate" SummaryType="Count" DisplayFormat="Routes: {0}" />
							<dx:ASPxSummaryItem FieldName="MailCount" SummaryType="Sum" DisplayFormat="Mail Count: {0:#,0}" />
							<dx:ASPxSummaryItem FieldName="EmailCount" SummaryType="Sum" DisplayFormat="Email Count: {0:#,0}" />
							<dx:ASPxSummaryItem FieldName="BagCount" SummaryType="Sum" DisplayFormat="Bag Count: {0:#,0}" />
							<dx:ASPxSummaryItem FieldName="PostcardCount" SummaryType="Sum" DisplayFormat="Postcard Count: {0:#,0}" />
							<dx:ASPxSummaryItem FieldName="CntMailNR" SummaryType="Sum" DisplayFormat="Non-Route Mail Count: {0:#,0}" />
							<dx:ASPxSummaryItem FieldName="CntEmailNR" SummaryType="Sum" DisplayFormat="Non-Route Email Count: {0:#,0}" />
						</GroupSummary>
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
					</dx:ASPxGridView>

					<asp:SqlDataSource ID="dsGridMain" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>">
					</asp:SqlDataSource>
					<asp:HiddenField ID="hfGridMainSelectCommand" runat="server" Value=""></asp:HiddenField>

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
                    <dx:ASPxDateEdit ID="dtEarliestPickupDate" ClientInstanceName="dtEarliestPickupDate"
                        Width="80%" ToolTip="Earliest Pickup Date" EditFormat="Custom" 
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
