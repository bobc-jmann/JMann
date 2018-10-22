<%@ Page Title="Route Worksheet" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="RouteWorksheet.aspx.vb" Inherits="RouteWorksheet" %>

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
		#parameters td.route {
			padding: 50px, 0px, 0px, 100px;

		}
	</style> 
    <script>
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }
    </script>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
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
     		grid.SetHeight(height);
     	}
    </script>
	<script type="text/javascript">

		function OnPickupDateChanged() {
			var pickupDate = dtPickupDate.GetDate();
			var regionID = document.getElementById("ddlRegions").value
			document.getElementById("ddlSections").value = -1
			document.getElementById("ddlDrivers").value = 0
			document.getElementById("ckShowAllAddresses").checked = false
		} 

		function grid_SelectionChanged(s, e) {
			s.GetSelectedFieldValues("StreetAddress", GetAddressesSelectedFieldValuesCallback);
		}

		function GetAddressesSelectedFieldValuesCallback(values) {
			var cnt = grid.GetSelectedRowCount();
			var driverCnt = document.getElementById("ddlDrivers").length;
			cnt = cnt * driverCnt;
			var btnText = "Send ".concat(cnt.toString().concat(" Text"));

			if (cnt > 0 && driverCnt > 0)
				document.getElementById("divBtnSendText").style.display = "block";
			else
				document.getElementById("divBSendText").style.display = "none";

			if (cnt > 1) {
				btnText = btnText.concat("s");
			}
			btnSendTexts.SetText(btnText);
		}

	</script>

	<script type="text/javascript">
		function Grid_BatchEditStartEditing(s, e) {
			var confirmed = s.GetColumnByField("Confirmed");
			if (!e.rowValues.hasOwnProperty(confirmed.index))
				return;
			var cellInfo = e.rowValues[confirmed.index];
			ckConfirmed.SetValue(cellInfo.value);
			if (e.focusedColumn === confirmed)
				if (cellInfo.value)
					ckConfirmed.SetValue(false);
				else
					ckConfirmed.SetValue(true);
			ckConfirmed.SetFocus();

			var missed = s.GetColumnByField("Missed");
			if (!e.rowValues.hasOwnProperty(missed.index))
				return;
			cellInfo = e.rowValues[missed.index];
			ckMissed.SetValue(cellInfo.value);
			if (e.focusedColumn === missed)
				if (cellInfo.value)
					ckMissed.SetValue(false);
				else
					ckMissed.SetValue(true);
			ckMissed.SetFocus();

			var redTagged = s.GetColumnByField("RedTagged");
			if (!e.rowValues.hasOwnProperty(redTagged.index))
				return;
			cellInfo = e.rowValues[redTagged.index];
			ckRedTagged.SetValue(cellInfo.value);
			if (e.focusedColumn === missed)
				if (cellInfo.value)
					ckRedTagged.SetValue(false);
				else
					ckRedTagged.SetValue(true);
			ckRedTagged.SetFocus();

			var comments = s.GetColumnByField("Comments");
			if (!e.rowValues.hasOwnProperty(comments.index))
				return;
			cellInfo = e.rowValues[comments.index];
			txtComments.SetValue(cellInfo.value);
			if (e.focusedColumn === comments)
				txtComments.SetFocus();
		}
		function Grid_BatchEditEndEditing(s, e) {
			var confirmed = s.GetColumnByField("Confirmed");
			if (!e.rowValues.hasOwnProperty(confirmed.index))
				return;
			var cellInfo = e.rowValues[confirmed.index];
			cellInfo.value = ckConfirmed.GetValue();
			cellInfo.text = ckConfirmed.GetText();
			ckConfirmed.SetValue(null);

			var missed = s.GetColumnByField("Missed");
			if (!e.rowValues.hasOwnProperty(missed.index))
				return;
			cellInfo = e.rowValues[missed.index];
			cellInfo.value = ckMissed.GetValue();
			cellInfo.text = ckMissed.GetText();
			ckMissed.SetValue(null);

			var redTagged = s.GetColumnByField("RedTagged");
			if (!e.rowValues.hasOwnProperty(redTagged.index))
				return;
			cellInfo = e.rowValues[redTagged.index];
			cellInfo.value = ckRedTagged.GetValue();
			cellInfo.text = ckRedTagged.GetText();
			ckRedTagged.SetValue(null);

			var comments = s.GetColumnByField("Comments");
			if (!e.rowValues.hasOwnProperty(comments.index))
				return;
			cellInfo = e.rowValues[comments.index];
			cellInfo.value = txtComments.GetValue();
			cellInfo.text = txtComments.GetText();
			txtComments.SetValue(null);
		}
		function OnLostFocus(s, e) {
			grid.UpdateEdit();
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Route Worksheet" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
		<asp:UpdatePanel ID="upMain" runat="server">
 			<ContentTemplate>
				<table id="parameters" style="width: 100%">
					<tr>
						<td style="width: 20%">Pickup Date:
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
								<ClientSideEvents DateChanged="OnPickupDateChanged" />
							</dx:ASPxDateEdit>
						</td>
						<td style="width: 10%">Region:
							<asp:DropDownList ID="ddlRegions" runat="server" ToolTip="Region" Width="100%" 
								OnSelectedIndexChanged="ddlRegions_SelectedIndexChanged" AutoPostBack="true">
							</asp:DropDownList>
						</td>
						<td style="width: 20%">Route & Charity:
							<asp:DropDownList ID="ddlRoutes" runat="server" ToolTip="Route" Width="100%" 
								OnSelectedIndexChanged="ddlRoutes_SelectedIndexChanged" AutoPostBack="true">
							</asp:DropDownList>
						</td>
						<td class="route" style="width: 18%"><br />
							<%--<asp:Label ID="lblRoute" runat="server" Text="" Font-Size="Medium" ForeColor="Magenta" ></asp:Label>--%>
 						</td>
						<td style="width: 12%">Section:
							<asp:DropDownList ID="ddlSections" runat="server" ToolTip="Section Code" Width="100%" 
								OnSelectedIndexChanged="ddlSections_SelectedIndexChanged" AutoPostBack="true" >
							</asp:DropDownList>
 						</td>
						<td style="width: 20%">
							<asp:Label ID="lblDrivers" runat="server" Text="" />
							<br />
							<asp:DropDownList ID="ddlDrivers" runat="server" ToolTip="Driver" Width="100%" 
							 	OnSelectedIndexChanged="ddlDrivers_SelectedIndexChanged" AutoPostBack="True" 
								ForeColor="Magenta" Font-Size="Medium">
							</asp:DropDownList>
 						</td>
					</tr>
					<tr>
						<td colspan="4">
							<dx:ASPxHiddenField runat="server" ClientInstanceName="hfPickupScheduleSectionID" ID="hfPickupScheduleSectionID" />
							<div id="divButtons" runat="server" Style="display: none">
								<div style="float:left; margin-top:5px">
									<asp:Button id="btnSection" runat="server" Text="Start Section" Height="30" 
										OnClick="btnSection_Click">
									</asp:Button>
								</div>
								<div style="float:left; margin-top:6px">
									<asp:Label ID="lblSectionStart" ForeColor="Green" runat="server" Text=""></asp:Label>
									<br />
									<asp:Label ID="lblSectionEnd" ForeColor="Red" runat="server" Text=""></asp:Label>
								</div>

								<div style="float:left; margin-left:10px; margin-top:5px">
									<asp:Button id="btnBreak" runat="server" Text="Start Break" Height="30" 
										OnClick="btnBreak_Click">
									</asp:Button>
								</div>
								<div style="float:left; margin-top:6px">
									<asp:Label ID="lblBreakStart" ForeColor="Green" runat="server" Text=""></asp:Label>
									<br />
									<asp:Label ID="lblBreakEnd" ForeColor="Red" runat="server" Text=""></asp:Label>
								</div>

								<div style="float:left; margin-left:10px; margin-top:5px">
									<asp:Button id="btnLunch" runat="server" Text="Start Lunch" Height="30" 
										OnClick="btnLunch_Click">
									</asp:Button>
								</div>
								<div style="float:left; margin-top:6px">
									<asp:Label ID="lblLunchStart" ForeColor="Green" runat="server" Text=""></asp:Label>
									<br />
									<asp:Label ID="lblLunchEnd" ForeColor="Red" runat="server" Text=""></asp:Label>
								</div>
							</div>
							<div id="divComments" runat="server" Style="display: none">
								<div style="float:left; margin-left:10px">Section Comments:
									<dx:aspxTextBox ID="txtSectionComments" ClientInstanceName="txtSectionComments" runat="server" Width="250px" ></dx:aspxTextBox>
								</div>
								<div style="float:left"><br />&nbsp
									<asp:Button id="btnSaveComments" runat="server" Text="Save Comments" 
										OnClick="btnSaveComments_Click" />
								</div>
							</div>
						</td>
						<td>
							<asp:CheckBox ID="ckShowAllAddresses" runat="server" AutoPostBack="true" Text="Show All Addresses" />
						</td>
						<td>
							<asp:Button id="btnDriverLog" runat="server" Text="Show Driver Log" 
								OnClick="btnDriverLog_Click">
							</asp:Button>
						</td>
					</tr>
					<tr>
						<td>
							<div id="divBtnSendText" runat="server" Style="display: none">
								<dx:ASPxButton ID="btnSendTexts" runat="server" Text="Send Text" OnClick="btnSendTexts_Click" />
							</div>
							<div id="divBtnSendTextConfirm" runat="server" Style="display: none">
								<asp:Label ID="lblSendTexts" runat="server" ForeColor="Magenta" Text=""></asp:Label>
								<dx:ASPxButton ID="btnSendTextsConfirm" runat="server" Text="OK" OnClick="btnSendTextsConfirm_Click" />
    						</div>
						</td>
						<td colspan="2"></td>
						<td colspan="3">
							<dx:ASPxGridView ID="grdDriverLog" ClientInstanceName="grdDriverLog" KeyFieldName="DriverLogID" 
									runat="server" DataSourceID="dsDriverLog" Width="100%"
									SettingsBehavior-ConfirmDelete="True"
									OnInitNewRow="grdDriverLog_InitNewRow"
									OnRowUpdated="grdDriverLog_RowUpdated"
									OnRowDeleted="grdDriverLog_RowDeleted"
									EnableCallBacks="False">
								<Columns>
                                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Caption=" " Width="60" ShowEditButton="true" ShowDeleteButton="true"/>
									<dx:GridViewDataColumn FieldName="DriverLogID" Visible="False" /> 
									<dx:GridViewDataColumn FieldName="DriverID" Visible="False" /> 
									<dx:GridViewDataColumn FieldName="PickupDate" Visible="False" /> 
									<dx:GridViewDataComboBoxColumn FieldName="ActionID" VisibleIndex="1" Caption="Action" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Left" Width="50" >
										<PropertiesComboBox DataSourceID="dsDriverActions" TextField="DriverAction" ValueField="DriverActionID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
										</PropertiesComboBox>
									</dx:GridViewDataComboBoxColumn>
									<dx:GridViewDataComboBoxColumn FieldName="DriverAssignmentID" VisibleIndex="2" Caption="Route-Section" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2" HeaderStyle-HorizontalAlign="Left" Width="100">
										<PropertiesComboBox DataSourceID="dsDriverAssignments" TextField="RouteSection" ValueField="DriverAssignmentID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
										</PropertiesComboBox>
									</dx:GridViewDataComboBoxColumn>
									<dx:GridViewDataTimeEditColumn FieldName="StartTime" VisibleIndex="3" EditFormSettings-VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="40" CellStyle-HorizontalAlign="Center">
										<PropertiesTimeEdit EditFormatString="HH:mm" DisplayFormatString="HH:mm">
										</PropertiesTimeEdit>
									</dx:GridViewDataTimeEditColumn>
									<dx:GridViewDataTimeEditColumn FieldName="EndTime" VisibleIndex="4" EditFormSettings-VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="40" CellStyle-HorizontalAlign="Center">
										<PropertiesTimeEdit EditFormatString="HH:mm" DisplayFormatString="HH:mm">
										</PropertiesTimeEdit>
									</dx:GridViewDataTimeEditColumn>
									<dx:GridViewDataColumn FieldName="Comments" VisibleIndex="5" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="5" HeaderStyle-HorizontalAlign="Left" EditFormSettings-ColumnSpan="2" />
								</Columns>
                                <SettingsCommandButton>
                                    <DeleteButton Text="Del"/>
                                </SettingsCommandButton>  
							</dx:ASPxGridView>

							<asp:SqlDataSource ID="dsDriverLog" runat="server"
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
								SelectCommandType="StoredProcedure"
								SelectCommand="spDriverLog_Select"
								InsertCommandType="StoredProcedure"
								InsertCommand="spDriverLog_Insert"
								UpdateCommandType="StoredProcedure"
								UpdateCommand="spDriverLog_Update"
								DeleteCommandType="StoredProcedure"
								DeleteCommand="spDriverLog_Delete">
								<SelectParameters>
									<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
									<asp:ControlParameter name="PickupDate" controlid="dtPickupDate" propertyname="Value" />
								</SelectParameters>
								<UpdateParameters>
									<asp:Parameter Name="DriverLogID" Type="Int32" />
									<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
									<asp:ControlParameter name="PickupDate" controlid="dtPickupDate" propertyname="Value" />
									<asp:Parameter Name="ActionID" Type="Int32" />
									<asp:Parameter Name="DriverAssignmentID" Type="Int32" />
									<asp:Parameter Name="StartTime" Type="DateTime" />
									<asp:Parameter Name="EndTime" Type="DateTime" />
									<asp:Parameter Name="Comments" Type="String" />
									<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
								</UpdateParameters>
								<InsertParameters>
									<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
									<asp:ControlParameter name="PickupDate" controlid="dtPickupDate" propertyname="Value" />
									<asp:Parameter Name="ActionID" Type="Int32" />
									<asp:Parameter Name="DriverAssignmentID" Type="Int32" />
									<asp:Parameter Name="StartTime" Type="DateTime" />
									<asp:Parameter Name="EndTime" Type="DateTime" />
									<asp:Parameter Name="Comments" Type="String" />
									<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
								</InsertParameters>
								<DeleteParameters>
									<asp:Parameter Name="DriverLogID" Type="Int32" />
									<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
								</DeleteParameters>
							</asp:SqlDataSource>
							<asp:SqlDataSource ID="dsDriverActions" runat="server"
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
								SelectCommand="SELECT DriverActionID, DriverAction FROM tblDriverActions ORDER BY DriverAction">
							</asp:SqlDataSource>
							<asp:SqlDataSource ID="dsDriverAssignments" runat="server"
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
								SelectCommand="SELECT DriverAssignmentID, RouteCode + '-' + SectionCode AS RouteSection FROM tblDriverAssignments WHERE DriverID = @DriverID AND PickupDate = @PickupDate AND SectionID > 0 ORDER BY RouteSection">
								<SelectParameters>
									<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
									<asp:ControlParameter name="PickupDate" controlid="dtPickupDate" propertyname="Value" />
								</SelectParameters>
							</asp:SqlDataSource>
						</td>
					</tr>
				</table>
				<table id="tblGrid" runat="server" style="visibility: visible">
					<tr>
						<td style="width: 100%">
							<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsAddresses" 
								KeyFieldName="PickupScheduleDetailID" Width="100%" SettingsBehavior-ConfirmDelete="True"
								EnableCallBacks="False">
								<Columns>
									<dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="Select" HeaderStyle-HorizontalAlign="Center" Width="50" />
									<dx:GridViewDataColumn FieldName="PickupScheduleDetailID" Visible="false" /> 
									<dx:GridViewDataColumn FieldName="TextsSent" HeaderStyle-Wrap="True" ReadOnly="true" 
										HeaderStyle-HorizontalAlign="Center" Width="40" CellStyle-HorizontalAlign="Center" /> 
									<dx:GridViewDataCheckColumn FieldName="Confirmed" HeaderStyle-HorizontalAlign="Center" Width="62">
										<EditItemTemplate>
											<dx:aspxCheckBox ID="ckConfirmed" runat="server" ClientInstanceName="ckConfirmed" Width="100%">
												<ClientSideEvents LostFocus="OnLostFocus" />
											</dx:aspxCheckBox>
										</EditItemTemplate>
									</dx:GridViewDataCheckColumn>
									<dx:GridViewDataCheckColumn FieldName="Missed" HeaderStyle-HorizontalAlign="Center" Width="62" >
										<EditItemTemplate>
											<dx:aspxCheckBox ID="ckMissed" runat="server" ClientInstanceName="ckMissed" Width="100%">
												<ClientSideEvents LostFocus="OnLostFocus" />
											</dx:aspxCheckBox>
										</EditItemTemplate>
									</dx:GridViewDataCheckColumn>
									<dx:GridViewDataCheckColumn FieldName="RedTagged" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="62" >
										<EditItemTemplate>
											<dx:aspxCheckBox ID="ckRedTagged" runat="server" ClientInstanceName="ckRedTagged" Width="100%">
												<ClientSideEvents LostFocus="OnLostFocus" />
											</dx:aspxCheckBox>
										</EditItemTemplate>
									</dx:GridViewDataCheckColumn>
									<dx:GridViewDataCheckColumn FieldName="DoNotRedTag" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" ReadOnly="true" Width="50" /> 
									<dx:GridViewDataTextColumn FieldName="Comments" HeaderStyle-Wrap="True">
										<EditItemTemplate>
											<dx:ASPxTextBox ID="txtComments" runat="server" ClientInstanceName="txtComments" Width="100%">
												<ClientSideEvents LostFocus="OnLostFocus" />
											</dx:ASPxTextBox>
										</EditItemTemplate>
									</dx:GridViewDataTextColumn>						 
									<dx:GridViewDataColumn FieldName="StreetAddress" HeaderStyle-Wrap="True" ReadOnly="true" /> 
									<dx:GridViewDataColumn FieldName="StreetName" HeaderStyle-Wrap="True" ReadOnly="true" Visible="false"/> 
									<dx:GridViewDataColumn FieldName="City" HeaderStyle-Wrap="True" ReadOnly="true" Width="100" /> 
									<dx:GridViewDataColumn FieldName="Status" HeaderStyle-Wrap="True" ReadOnly="true" Width="60" /> 
									<dx:GridViewDataColumn FieldName="Donations1Yr" Caption="Donations in Last Year" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" Width="60" HeaderStyle-CssClass="gridHeaderFont" /> 
									<dx:GridViewDataColumn FieldName="Donations3Yr" Caption="Donations in Last Three Years" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" Width="60" HeaderStyle-CssClass="gridHeaderFont" /> 
									<dx:GridViewDataColumn FieldName="LastDonationDate" HeaderStyle-Wrap="True" ReadOnly="true" Width="60" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gridHeaderFont" /> 
									<dx:GridViewDataColumn FieldName="MailingsSinceLastDonation" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" Width="60" HeaderStyle-CssClass="gridHeaderFont" /> 
								</Columns>
								<SettingsPager PageSize="1000" AlwaysShowPager="True" />
								<SettingsEditing Mode="Batch" />
								<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
								<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" SelectionChanged="grid_SelectionChanged"
									BatchEditStartEditing="Grid_BatchEditStartEditing" BatchEditEndEditing="Grid_BatchEditEndEditing" />
								<SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
								<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
								<Templates>
									<DetailRow>
										<dx:ASPxGridView ID="grdTexts" KeyFieldName="LogID" 
												runat="server" DataSourceID="dsLog" Width="60%" 
												OnBeforePerformDataSelect="grdLog_DataSelect">
											<Columns>
												<dx:GridViewDataColumn FieldName="LogID" Visible="False" /> 
												<dx:GridViewDataDateColumn FieldName="LogDate" Caption="Date/Time" PropertiesDateEdit-DisplayFormatString="g" /> 
												<dx:GridViewDataColumn FieldName="UserName" /> 
												<dx:GridViewDataColumn FieldName="Subject" /> 
												<dx:GridViewDataColumn FieldName="Body" /> 
												<dx:GridViewDataColumn FieldName="DriverName" /> 
										   </Columns>  
										</dx:ASPxGridView>
									</DetailRow>
								</Templates>
								<SettingsDetail ShowDetailRow="true" />
							</dx:ASPxGridView>

							<asp:SqlDataSource ID="dsAddresses" runat="server"
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
								UpdateCommand="spSpecialsConfirmMiss"
								UpdateCommandType="StoredProcedure">
								<UpdateParameters>
									<asp:Parameter Name="PickupScheduleDetailID" Type="Int32" />
									<asp:Parameter Name="Confirmed" Type="Boolean" />
									<asp:Parameter Name="Missed" Type="Boolean" />
									<asp:Parameter Name="RedTagged" Type="Boolean" />
									<asp:Parameter Name="Comments" Type="String" />
					                <asp:SessionParameter Name="UserID" SessionField="vUserID" Type="Int32" />
					                <asp:SessionParameter Name="UserName" SessionField="vUserName" Type="String" />
								</UpdateParameters>
							</asp:SqlDataSource>
							<asp:SqlDataSource ID="dsRegions" runat="server"
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
							</asp:SqlDataSource>

							<asp:SqlDataSource ID="dsLog" runat="server"
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
								SelectCommand="SELECT LogID, LogDate, UserName, Subject, Body, DriverName FROM tSysTextMessageLog WHERE PickupScheduleDetailID = @PickupScheduleDetailID ORDER BY LogDate">
								<SelectParameters>
									<asp:SessionParameter Name="PickupScheduleDetailID" SessionField="PickupScheduleDetailID" Type="Int32" />
								</SelectParameters>
							</asp:SqlDataSource>

							<asp:SqlDataSource ID="dsPickupScheduleSections" runat="server"
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
								UpdateCommand="UPDATE tblPickupScheduleSections SET Comments = REPLACE(@Comments, '''', '''''') WHERE PickupScheduleSectionID = @PickupScheduleSectionID">
								<UpdateParameters>
									<asp:Parameter Name="PickupScheduleSectionID" Type="Int32" />
									<asp:Parameter Name="Comments" Type="String" />
								</UpdateParameters>
							</asp:SqlDataSource>

							<dx:ASPxGlobalEvents ID="ge" runat="server">
								<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
							</dx:ASPxGlobalEvents>
						</td>
					</tr>
				</table>
			</ContentTemplate>
		</asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
