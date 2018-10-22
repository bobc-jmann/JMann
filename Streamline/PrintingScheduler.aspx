<%@ Page Title="Printing Scheduler" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="PrintingScheduler.aspx.vb" Inherits="PrintingScheduler" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
        #tblAddressParameters tr:nth-child(odd) { background-color: #E0E0E0; border-color: #E0E0E0; border: 0px; margin: 0px; padding: 0px; border-collapse: collapse }
        #tblAddressParameters tr:nth-child(even) { background-color: #F0F0F0; border-color: #F0F0F0; Border: 0px; margin: 0px; padding: 0px;  border-collapse: collapse }
	    .parm1 { width: 40%; text-align: right; font-weight:bold }
	    .parm2 { width: 40%; text-align: right; font-weight:bold }
	    .parm3 { width: 20%; text-align: right; font-weight:bold }
	</style> 
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Printing Scheduler" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="100%">
            <tr>
                <td style="width: 20%"></td>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckDoNotShowApprovedForExport" Text="Do Not Show 'Approved For Export' Jobs" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
                <td>
                    <dx:ASPxButton runat="server" ID="btnRunExportedUnprintedPrintJobsReport" Width="250" Text="Exported Unprinted Print Jobs" OnClick="btnRunExportedUnprintedPrintJobsReport_Click" AutoPostBack="True"></dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td style="width: 20%"></td>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckDoNotShowPrinted" Text="Do Not Show 'Printed' Jobs" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
                <td>
                    <dx:ASPxButton runat="server" ID="btnMailCountsByMailingDate" Width="250" Text="Mail Counts by Mailing Date" OnClick="btnMailCountsByMailingDate_Click" AutoPostBack="True"></dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td style="width: 20%"></td>
                <td></td>
                <td>
                    <dx:ASPxButton runat="server" ID="btnScheduleMailCounts" Width="250" Text="Schedule Mail Counts" OnClick="btnScheduleMailCounts_Click" AutoPostBack="True"></dx:ASPxButton>
                </td>
            </tr>
            <tr>
               <td style="width: 20%"></td>
               <td colspan="2">
                    <asp:Label ID="lblTooMany" style="font-size:large; color: red" runat="server" Text="Print queue full. System cannot approve more jobs for export until some of the current jobs are printed."></asp:Label>
                </td>
             </tr>
        </table>
        <table id="tblSchedule" runat="server" style="visibility: visible">
            <tr>
				<td style="width: 100%" colspan="5">
					<dx:ASPxGridView KeyFieldName="PickupScheduleID" ID="gridMain" 
					   OnHtmlDataCellPrepared="vbOnHtmlDataCellPrepared" ClientInstanceName="gridMain" 
					   runat="server" DataSourceID="dsGridMain" Width="100%">
						<Columns>
							  <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="False" /> 
							  <dx:GridViewDataColumn FieldName="PickupCycleID" Visible="False" /> 
							  <dx:GridViewDataColumn FieldName="ScheduleTypeID" Visible="False" /> 
							  <dx:GridViewDataColumn FieldName="PermitID" Visible="False" /> 
							  <dx:GridViewDataColumn FieldName="PickupCycleAbbr" Width="7%" VisibleIndex="1" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" />
							  <dx:GridViewDataColumn FieldName="PickupCycleWeek" VisibleIndex="2" EditFormSettings-Visible="False" Caption="Week"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
							  <dx:GridViewDataColumn FieldName="PickupCycleDay" VisibleIndex="3" EditFormSettings-Visible="False"  Caption="Day" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
							  <dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="4" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
							  <dx:GridViewDataColumn FieldName="City" Width="20%" VisibleIndex="5" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" />
							  <dx:GridViewDataColumn FieldName="MailingDate" VisibleIndex="7" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
							  <dx:GridViewDataColumn FieldName="PickupDate" VisibleIndex="8" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
							  <dx:GridViewDataColumn FieldName="PrintJobCategory" VisibleIndex="9" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="100" />
							  <dx:GridViewDataTextColumn FieldName="MailCount" VisibleIndex="9" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								   <PropertiesTextEdit ClientInstanceName="MailCount" DisplayFormatString="#,#" />
							  </dx:GridViewDataTextColumn>
							  <dx:GridViewDataTextColumn FieldName="PostcardCount" VisibleIndex="10" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								   <PropertiesTextEdit ClientInstanceName="PostcardCount" DisplayFormatString="#,#" />
							  </dx:GridViewDataTextColumn>
							  <dx:GridViewDataTextColumn FieldName="CntMailNR" Caption="Mail NR Count" VisibleIndex="11" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
								   <PropertiesTextEdit ClientInstanceName="CntMailNR" DisplayFormatString="#,#" />
							  </dx:GridViewDataTextColumn>
							  <dx:GridViewDataColumn FieldName="ApprovedForExport" VisibleIndex="12" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Caption="App'd for Export" EditFormSettings-CaptionLocation="Default" />
							  <dx:GridViewDataColumn FieldName="Exported" VisibleIndex="13" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" />
							  <dx:GridViewDataColumn FieldName="Printed" VisibleIndex="14" EditFormSettings-Visible="True"  HeaderStyle-HorizontalAlign="Center" />
							  <dx:GridViewDataColumn FieldName="AllMail" VisibleIndex="15" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
							  <dx:GridViewDataColumn FieldName="AllPostcard" VisibleIndex="16" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
						</Columns>
						<SettingsEditing Mode="Batch" />
						<Settings ShowGroupPanel="True" ShowFooter="True" />
						<SettingsBehavior ColumnResizeMode="Control" />
							<SettingsPager PageSize="20">
						</SettingsPager>
						<GroupSummary>
							<dx:ASPxSummaryItem FieldName="MailingDate" SummaryType="Count" DisplayFormat="Routes: {0}" />
							<dx:ASPxSummaryItem FieldName="MailCount" SummaryType="Sum" DisplayFormat="Mail Count: {0:#,0}" />
							<dx:ASPxSummaryItem FieldName="PostcardCount" SummaryType="Sum" DisplayFormat="Postcard Count: {0:#,0}" />
							<dx:ASPxSummaryItem FieldName="CntMailNR" SummaryType="Sum" DisplayFormat="Mail NR Count: {0:#,0}" />
						</GroupSummary>
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
					</dx:ASPxGridView>

					<asp:SqlDataSource ID="dsGridMain" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>" 
						UpdateCommand="spScheduleUpdate"
						UpdateCommandType="StoredProcedure">
						<UpdateParameters>
							<asp:Parameter Name="ScheduleTypeID" type="Int32" />
							<asp:Parameter Name="MailingDate" type="DateTime" />
							<asp:Parameter Name="PickupDate" type="DateTime" />
							<asp:Parameter Name="PermitID" type="Int32" />
							<asp:Parameter Name="PrintJobCategory" type="String" />
							<asp:Parameter Name="ApprovedForExport" type="Boolean" />
							<asp:Parameter Name="Printed" type="Boolean" />
							<asp:Parameter Name="AllMail" type="Boolean" />
							<asp:Parameter Name="AllPostcard" type="Boolean" />
							<asp:Parameter Name="PickupScheduleID" type="Int32" />
						</UpdateParameters>
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
