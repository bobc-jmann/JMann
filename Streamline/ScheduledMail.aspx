<%@ Page Title="Scheduled Mail" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ScheduledMail.aspx.vb" Inherits="ScheduledMail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
		.DevExButton {
			font-family: Tahoma;
			font-size: 8pt;
			color: #3C3A49;
			background-color: #ECEDF0;
			height: 25px;
			border-style: solid;
			border-color: #A9ACB5;
		}
	</style>
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
    		if (s.cpRescheduleMessage != null) {
    			alert(s.cpRescheduleMessage);
    			delete s.cpRescheduleMessage;
    		}
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Scheduled Mail" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
	<br />
	<asp:UpdatePanel ID="upMain" runat="server">
 		<ContentTemplate>
			<asp:Timer runat="server" id="Timer1" Interval="2000" OnTick="Timer1_Tick" Enabled="false" />

			<dx:ASPxHiddenField runat="server" ClientInstanceName="hfExport" ID="hfExport" />

			<asp:Button ID="btnSelectUnapproved" runat="server" OnClick="btnSelectUnapproved_Click" 
				Text="Select Unapproved through Mailing Date:" Class="DevExButton" Width="250px">
			</asp:Button>
			<asp:Button ID="btnApproveSelected" runat="server" OnClick="btnApproveSelected_Click" 
				Text="Approve Selected" Class="DevExButton" Width="120px">
			</asp:Button>
			<asp:Button ID="btnSelectApprovedAndUnexported" runat="server" OnClick="btnSelectApprovedAndUnexported_Click" 
				Text="Select Approved and Unexported" Class="DevExButton" Width="200px">
			</asp:Button>
			<asp:Button ID="btnExportApprovedAndUnexported" runat="server" OnClick="btnExportApprovedAndUnexported_Click" 
				Text="Export Approved and Unexported" Class="DevExButton" Width="200px">
			</asp:Button>
			<asp:Button ID="btnDeselectAll" runat="server" OnClick="btnDeselectAll_Click" 
				Text="Deselect All" Class="DevExButton" Width="120px">
			</asp:Button>
            <dx:ASPxDateEdit ID="dtMailingDate" ClientInstanceName="dtMailingDate"
                Width="250" ToolTip="Pickup Date" EditFormat="Custom" 
                EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                runat="server" AutoPostBack="True">
                <ValidationSettings SetFocusOnError="True">
                    <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                    <RegularExpression ErrorText="Invalid date"></RegularExpression>
                </ValidationSettings>
            </dx:ASPxDateEdit>
            <dx:ASPxLabel ID="lblTooMany" style="font-size:large; color: red" runat="server" Text="Print queue full. System cannot approve more jobs for export until some of the current jobs are printed."></dx:ASPxLabel>
            <dx:ASPxLabel ID="lblTooManySelected" style="font-size:large; color: red" runat="server" Text=""></dx:ASPxLabel>
   			<dx:ASPxLabel ID="lblProgressBar" runat="server" Text="" Font-Size="12" ForeColor="Blue" />

			<div id="divSearch" class="specials" runat="server">
				 <table id="tblSchedule" runat="server" style="visibility: visible">
					<tr>
						<td style="width: 100%" colspan="5">
						   <dx:ASPxGridView KeyFieldName="PickupScheduleID" ID="gridMain" EnableRowsCache="true"
							   OnHtmlDataCellPrepared="vbOnHtmlDataCellPrepared" ClientInstanceName="gridMain" runat="server" 
							   OnCommandButtonInitialize="gridMain_CommandButtonInitialize"
							   OnCustomButtonInitialize="gridMain_CustomButtonInitialize"
							   DataSourceID="dsGridMain" Width="100%"
							   EnableCallBacks="False">
								<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" ConfirmDelete="True" />
								<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
								<Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Width="150" Caption=" " ShowSelectCheckbox="True" ShowDeleteButton="true" ShowEditButton="true">
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="cbReschedule" Text="Resched" Visibility="AllDataRows"/>
                                        </CustomButtons>
                                    </dx:GridViewCommandColumn>
									<dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="False" /> 
									<dx:GridViewDataColumn FieldName="ExportedToTablets" Visible="False" /> 
									<dx:GridViewDataColumn FieldName="PickupCycleAbbr" Width="100" VisibleIndex="1" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
									<dx:GridViewDataColumn FieldName="ScheduleType" VisibleIndex="2" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
									<dx:GridViewDataColumn FieldName="PickupCycleWeek" VisibleIndex="3" Caption="Week"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="False" />
									<dx:GridViewDataColumn FieldName="PickupCycleDay" VisibleIndex="4" Caption="Day" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" EditFormSettings-Visible="False" />
									<dx:GridViewDataColumn FieldName="RouteCode" Width="80" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
									<dx:GridViewDataColumn FieldName="MailingDate" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="True" />
									<dx:GridViewDataColumn FieldName="PickupDate" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="True" />
									<dx:GridViewDataColumn FieldName="PrintJobCategory" Width="100" VisibleIndex="9"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
									<dx:GridViewDataCheckColumn FieldName="ApprovedForExport" VisibleIndex="11"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" />
									<dx:GridViewDataCheckColumn FieldName="Exported" VisibleIndex="13"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="False" />
									<dx:GridViewDataCheckColumn FieldName="Printed" VisibleIndex="14"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="True" />
									<dx:GridViewDataTextColumn FieldName="MailCount" VisibleIndex="20" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" >
										<PropertiesTextEdit ClientInstanceName="MailCount" DisplayFormatString="#,#" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="EmailCount" VisibleIndex="21" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" >
										<PropertiesTextEdit ClientInstanceName="EmailCount" DisplayFormatString="#,#" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="BagCount" VisibleIndex="22" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" >
										<PropertiesTextEdit ClientInstanceName="BagCount" DisplayFormatString="#,#" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="PostcardCount" VisibleIndex="23" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" >
										<PropertiesTextEdit ClientInstanceName="PostcardCount" DisplayFormatString="#,#" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="CntMailNR" Caption="Non-Route Mail Count" VisibleIndex="24" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" >
										<PropertiesTextEdit ClientInstanceName="CntMailNR" DisplayFormatString="#,#" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="CntEmailNR" Caption="Non-Route Email Count" VisibleIndex="25" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" >
										<PropertiesTextEdit ClientInstanceName="CntEmailNR" DisplayFormatString="#,#" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataCheckColumn FieldName="AutoGenerated" Width="30" VisibleIndex="30" Caption="A" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="False" />
									<dx:GridViewDataDateColumn FieldName="AutoGeneratedDate" VisibleIndex="31" Caption="Auto Generated On" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" EditFormSettings-Visible="False" />
								</Columns>
								<Settings ShowGroupPanel="True" ShowFooter="True" />
								<SettingsBehavior ColumnResizeMode="Control" ProcessSelectionChangedOnServer="True" />
								<SettingsPager PageSize="400" AlwaysShowPager="True" />
								<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
								<SettingsEditing EditFormColumnCount="4" />
								<GroupSummary>
									<dx:ASPxSummaryItem FieldName="MailingDate" SummaryType="Count" DisplayFormat="Routes: {0}" />
									<dx:ASPxSummaryItem FieldName="MailCount" SummaryType="Sum" DisplayFormat="Mail Count: {0:#,0}" />
									<dx:ASPxSummaryItem FieldName="EmailCount" SummaryType="Sum" DisplayFormat="Email Count: {0:#,0}" />
									<dx:ASPxSummaryItem FieldName="BagCount" SummaryType="Sum" DisplayFormat="Bag Count: {0:#,0}" />
									<dx:ASPxSummaryItem FieldName="PostcardCount" SummaryType="Sum" DisplayFormat="Postcard Count: {0:#,0}" />
									<dx:ASPxSummaryItem FieldName="CntMailNR" SummaryType="Sum" DisplayFormat="Non-Route Mail Count: {0:#,0}" />
									<dx:ASPxSummaryItem FieldName="CntEmailNR" SummaryType="Sum" DisplayFormat="Non-Route Email Count: {0:#,0}" />
								</GroupSummary>
								<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback"/>
                                <SettingsCommandButton>
                                    <DeleteButton Text="Del"/>
                                </SettingsCommandButton>
							</dx:ASPxGridView>

							<asp:SqlDataSource ID="dsGridMain" runat="server" 
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
								SelectCommand="SELECT * FROM qryScheduler WHERE MailingDate >= @MailingDate AND ScheduleTypeID IN (1, 4, 5) ORDER BY MailingDate DESC, PermitNbr, RouteCode DESC"
								UpdateCommand="spScheduleUpdate"
								UpdateCommandType="StoredProcedure"
								DeleteCommand="spScheduleDelete"
								DeleteCommandType="StoredProcedure">
								<SelectParameters>
									<asp:ControlParameter name="MailingDate" controlid="dtEarliestMailingDate" propertyname="Value" />
								</SelectParameters>
								<UpdateParameters>
									<asp:Parameter Name="PickupScheduleID" Type="Int32" />
									<asp:Parameter Name="MailingDate" Type="DateTime" />
									<asp:Parameter Name="PickupDate" Type="DateTime" />
									<asp:Parameter Name="ApprovedForExport" Type="Boolean" />
									<asp:Parameter Name="Printed" Type="Boolean" />
								</UpdateParameters>
								<DeleteParameters>
									<asp:Parameter Name="PickupScheduleID" Type="Int32" />
								</DeleteParameters>
							</asp:SqlDataSource>
							<asp:HiddenField ID="hfGridMainSelectCommand" runat="server" Value=""></asp:HiddenField>

							<dx:ASPxGlobalEvents ID="ge" runat="server">
								<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
							</dx:ASPxGlobalEvents>
						</td>
					 </tr>
				</table>  
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblMailingDateMin" runat="server" Text="Earliest Mailing Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtEarliestMailingDate" ClientInstanceName="dtEarliestMailingDate"
                        Width="80%" ToolTip="Earliest Mailing Date" EditFormat="Custom" 
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
