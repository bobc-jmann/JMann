<%@ Page Title="Mail Schedule Planner" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="MailSchedulePlanner.aspx.vb" Inherits="MailSchedulePlanner" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style>
		.dxgvFocusedRow_DevEx td.dxgv {
			background: #FFF none;
		}

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
            var height = Math.max(0, document.documentElement.clientHeight) - 160;
            grdMain.SetHeight(height);
        }

        function OnEndCallbackDetail(s, e) {
        	grdMain.Refresh();
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
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding="2" cellspacing="0" style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Mail Schedule Planner" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
 	<dx:ASPxHiddenField ID="hf" runat="server" />
	<dx:aspxformlayout ID="fl" runat="server" ColCount="2" EnableTheming="True" Theme="DevEx" Width="100%">
		<Items>
			<dx:LayoutGroup Caption="Controls" ShowCaption="False" ColCount="2" ColSpan="2" Name="Controls">
				<Items>
					<dx:LayoutItem FieldName="YearWeek" Caption="Mailing Week" ColSpan="1">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
								<dx:ASPxComboBox ID="tbYearWeek" runat="server"										
									DataSourceID="dsYearWeek" ValueType="System.Int32"
									DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
									TextField="CalendarYearWeekEnding" ValueField="YearWeek" Width="230px" AutoPostBack="True"
									OnSelectedIndexChanged="tbYearWeek_SelectedIndexChanged" SelectedIndex="0">
								<ClearButton Visibility="Auto" />
								</dx:ASPxComboBox>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
					</dx:LayoutItem>
					<dx:LayoutItem FieldName="SpecialDelete" Caption="Allow deletion of scheduled Routes" Visible="false" ColSpan="1">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
								<dx:ASPxCheckBox ID="ckSpecialDelete" Visible =" false" runat="server" />									
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
					</dx:LayoutItem>
				</Items>
			</dx:LayoutGroup>
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" DataSourceID="dsMailSchedulePlanning" 
							KeyFieldName="YearWeek;RegionID" Width="100%" EnableRowsCache="false"
							OnCellEditorInitialize="grdMain_CellEditorInitialize"
							OnHtmlFooterCellPrepared="grdMain_HtmlFooterCellPrepared"
							OnRowValidating="grdMain_RowValidating">
							<Columns>
								<dx:GridViewDataColumn FieldName="YearWeek" Visible="false" />
								<dx:GridViewDataComboBoxColumn FieldName="RegionID" Caption="Region" Width="50px" FixedStyle="Left">
									<PropertiesComboBox Width="100%"
										ValueType="System.Int32" ValueField="RegionID" TextField="RegionCode" DataSourceID="dsRegions" 
										EnableCallbackMode="true" CallbackPageSize="20"
										IncrementalFilteringMode="StartsWith" >
										<ClearButton Visibility="Auto" />
									</PropertiesComboBox>
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataComboBoxColumn>
								<dx:GridViewDataTextColumn FieldName="CurrentCartInventory" 
									Caption="<b>A</b><br />Current Cart Inventory" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataDateColumn FieldName="InventoryDate" ReadOnly="true" Width="80px">
									<PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" HorizontalAlign="Center" />
								</dx:GridViewDataDateColumn>
								<dx:GridViewDataTextColumn FieldName="TargetForecastYearWeek" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="AlreadyScheduledAdjustment" 
									Caption="<b>B</b><br />Already Scheduled Adjustment" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="TargetInventory" 
									Caption="<b>C</b><br />Target Inventory" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="InventoryAdjustmentDesired" 
									Caption="<b>D</b><br />Inventory Adjustment Desired<br />C - (A + B)" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" ForeColor="Blue" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="HistoricalMailCarts" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastCartsWorked" 
									Caption="<b>E</b><br />Forecast Carts Worked" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastNonMailCarts" 
									Caption="<b>F</b><br />Forecast Non-Mail Carts" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastMailCartsDesired" 
									Caption="<b>G</b><br />Forecast Mail Carts Desired<br />D + E - F" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" ForeColor="Blue" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastMailCartsAllSections" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastMailCartsNotSeasonallyAdjusted" Caption="Forecast Mail Carts All Sections Not Seasonally Adjusted" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="MailCartsToSchedule" 
									Caption="<b>H</b><br >Mail Carts to Schedule" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle ForeColor="Blue" />
									<CellStyle ForeColor="Blue" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastMailCarts" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastNonRouteMailCarts" 
									Caption="<b>I</b><br />Forecast Non-Route Mail Carts" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="NRMailMaxDistance" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastInventoryChange" 
									Caption="<b>J</b><br />Forecast Inventory Change<br />(F + H + I) - E" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" ForeColor="Blue" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ActualMailCarts" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastActualMailCarts" 
									Caption= "Actual to Forecast Variance" ReadOnly="true" Width="70px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataDateColumn FieldName="MinPickupDate" ReadOnly="true" Width="80px">
									<PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" HorizontalAlign="Center" />
								</dx:GridViewDataDateColumn>
								<dx:GridViewDataDateColumn FieldName="MaxPickupDate" ReadOnly="true" Width="80px">
									<PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" Width="100%" />
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" HorizontalAlign="Center" />
								</dx:GridViewDataDateColumn>
								<dx:GridViewDataColumn FieldName="MinPickupWeekDay" ReadOnly="true" Width="80px">
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" HorizontalAlign="Center" />
								</dx:GridViewDataColumn>
								<dx:GridViewDataColumn FieldName="MaxPickupWeekDay" ReadOnly="true" Width="80px">
									<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" HorizontalAlign="Center" />
								</dx:GridViewDataColumn>
							</Columns>
							<TotalSummary>
								<dx:ASPxSummaryItem FieldName="CurrentCartInventory" ShowInGroupFooterColumn="CurrentCartInventory" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="AlreadyScheduledAdjustment" ShowInGroupFooterColumn="AlreadyScheduledAdjustment" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="TargetInventory" ShowInGroupFooterColumn="TargetInventory" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="InventoryAdjustmentDesired" ShowInGroupFooterColumn="InventoryAdjustmentDesired" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="HistoricalMailCarts" ShowInGroupFooterColumn="HistoricalMailCarts" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastCartsWorked" ShowInGroupFooterColumn="ForecastCartsWorked" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastNonMailCarts" ShowInGroupFooterColumn="ForecastNonMailCarts" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastMailCartsDesired" ShowInGroupFooterColumn="ForecastMailCartsDesired" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastMailCartsAllSections" ShowInGroupFooterColumn="ForecastMailCartsAllSections" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="MailCartsToSchedule" ShowInGroupFooterColumn="MailCartsToSchedule" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastMailCarts" ShowInGroupFooterColumn="ForecastMailCarts" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastMailCartsNotSeasonallyAdjusted" ShowInGroupFooterColumn="ForecastMailCartsNotSeasonallyAdjusted" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastNonRouteMailCarts" ShowInGroupFooterColumn="ForecastNonRouteMailCarts" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastInventoryChange" ShowInGroupFooterColumn="ForecastInventoryChange" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ActualMailCarts" ShowInGroupFooterColumn="ActualMailCarts" SummaryType="Sum" DisplayFormat="{0:n2}" />
								<dx:ASPxSummaryItem FieldName="ForecastActualMailCarts" ShowInGroupFooterColumn="ForecastActualMailCarts" SummaryType="Sum" DisplayFormat="{0:n2}" />
							</TotalSummary>
							<Settings ShowGroupPanel="True" />
							<SettingsEditing Mode="Batch" />
							<SettingsPager PageSize="200" AlwaysShowPager="false" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" HorizontalScrollBarMode="Visible" />
							<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
							<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
							<SettingsBehavior AllowFocusedRow="True" />
							<SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
 							<Templates>
								<DetailRow>
									<dx:ASPxGridView ID="grdDetail" KeyFieldName="YearWeek;SectionID" runat="server" 
											DataSourceID="dsMailSchedulePlanningDetail" Width="95%" OnBeforePerformDataSelect="grdMain_DataSelect" 
											OnCellEditorInitialize="grdDetail_CellEditorInitialize"
											OnCustomSummaryCalculate="grdDetail_CustomSummaryCalculate"
											OnRowValidating="grdDetail_RowValidating">
										<Columns>
											<dx:GridViewDataColumn FieldName="YearWeek" Visible="false" />
											<dx:GridViewDataColumn FieldName="RegionID" Visible="false" />
											<dx:GridViewDataColumn FieldName="SectionID" Visible="false" />
											<dx:GridViewDataTextColumn FieldName="RouteSection" Caption="Route-Section"  
												ReadOnly="true" Width="120px" FixedStyle="Left" VisibleIndex="1">
												<PropertiesTextEdit Width="100%" />
												<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="Charity" ReadOnly="true" Width="50px" VisibleIndex="1">
												<PropertiesTextEdit Width="100%" />
												<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataDateColumn FieldName="MailingDate" ReadOnly="true" Width="80px" VisibleIndex="2">
												<PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" Width="100%" />
												<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" HorizontalAlign="Center" />
											</dx:GridViewDataDateColumn>
											<dx:GridViewDataDateColumn FieldName="PickupDate" ReadOnly="true" Width="80px" VisibleIndex="3" GroupIndex="1">
												<PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" Width="100%" />
												<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" HorizontalAlign="Center" />
											</dx:GridViewDataDateColumn>
											<dx:GridViewDataTextColumn FieldName="HistoricalCarts" ReadOnly="true" Width="70px" VisibleIndex="4">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="HistoricalPCB" ReadOnly="true" Width="70px" VisibleIndex="5">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="HistoricalPCO" ReadOnly="true" Width="70px" VisibleIndex="11">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="HistoricalTPO" ReadOnly="true" Width="70px" VisibleIndex="12">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="HistoricalCartsPerK" ReadOnly="true" Width="70px" VisibleIndex="13">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="QualityRating" Caption="Quality Rating"
												ReadOnly="true" Width="60px" VisibleIndex="14">
												<PropertiesTextEdit Width="100%" />
												<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastRating" ReadOnly="true" Width="70px" VisibleIndex="15">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataCheckColumn FieldName="Stale" ReadOnly="true" Width="40px" VisibleIndex="21">
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataCheckColumn>
											<dx:GridViewDataCheckColumn FieldName="New" ReadOnly="true" Width="40px" VisibleIndex="22">
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataCheckColumn>
											<dx:GridViewDataCheckColumn FieldName="Schedule" Width="50px" VisibleIndex="23" GroupIndex="0">
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataCheckColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastCarts" ReadOnly="true" Width="70px" VisibleIndex="24">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastCartsNotSeasonallyAdjusted" ReadOnly="true" Width="70px" VisibleIndex="25">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastPCB" ReadOnly="true" Width="70px" VisibleIndex="26">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastPCO" ReadOnly="true" Width="70px" VisibleIndex="31">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastTPO" ReadOnly="true" Width="70px" VisibleIndex="32">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastCartsPerK" ReadOnly="true" Width="70px" VisibleIndex="33">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ActualCarts" ReadOnly="true" Width="70px" VisibleIndex="34">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ActualPCB" ReadOnly="true" Width="70px" VisibleIndex="35">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ActualPCO" ReadOnly="true" Width="70px" VisibleIndex="41">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ActualTPO" ReadOnly="true" Width="70px" VisibleIndex="42">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ActualCartsPerK" ReadOnly="true" Width="70px" VisibleIndex="43">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastCarts_NR" ReadOnly="true" Width="70px" VisibleIndex="44">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastPO_NR" ReadOnly="true" Width="70px" VisibleIndex="45">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ForecastCartsPerK_NR" ReadOnly="true" Width="70px" VisibleIndex="51">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ActualCarts_NR" ReadOnly="true" Width="70px" VisibleIndex="52">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ActualPO_NR" ReadOnly="true" Width="70px" VisibleIndex="53">
												<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ActualCartsPerK_NR" ReadOnly="true" Width="70px" VisibleIndex="54">
												<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
										</Columns>
										<SettingsBehavior AutoExpandAllGroups="True" />
										<GroupSummary>
											<dx:ASPxSummaryItem FieldName="ForecastPCB" ShowInGroupFooterColumn="ForecastPCB" SummaryType="Sum" DisplayFormat="All={0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastPCO" ShowInGroupFooterColumn="ForecastPCO" SummaryType="Sum" DisplayFormat="All={0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastTPO" ShowInGroupFooterColumn="ForecastTPO" SummaryType="Sum" DisplayFormat="All={0:n0}" />
										</GroupSummary>
										<TotalSummary>
											<dx:ASPxSummaryItem FieldName="HistoricalCarts" ShowInGroupFooterColumn="HistoricalCarts" SummaryType="Sum" DisplayFormat="{0:n2}" />
											<dx:ASPxSummaryItem FieldName="HistoricalPCB" ShowInGroupFooterColumn="HistoricalPCB" SummaryType="Sum" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="HistoricalPCO" ShowInGroupFooterColumn="HistoricalPCO" SummaryType="Sum" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="HistoricalTPO" ShowInGroupFooterColumn="HistoricalTPO" SummaryType="Sum" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="HistoricalCartsPerK" ShowInGroupFooterColumn="HistoricalCartsPerK" SummaryType="Custom" DisplayFormat="{0:n2}" />
											<dx:ASPxSummaryItem FieldName="Stale" ShowInGroupFooterColumn="Stale" SummaryType="Custom" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="New" ShowInGroupFooterColumn="New" SummaryType="Custom" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="Schedule" ShowInGroupFooterColumn="Schedule" SummaryType="Custom" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastCarts" ShowInGroupFooterColumn="ForecastCarts" SummaryType="Custom" DisplayFormat="Sch={0:n2}" />
											<dx:ASPxSummaryItem FieldName="ForecastCarts" ShowInGroupFooterColumn="ForecastCarts" SummaryType="Sum" DisplayFormat="All={0:n2}" />
											<dx:ASPxSummaryItem FieldName="ForecastCartsNotSeasonallyAdjusted" ShowInGroupFooterColumn="ForecastCartsNotSeasonallyAdjusted" SummaryType="Custom" DisplayFormat="Sch={0:n2}" />
											<dx:ASPxSummaryItem FieldName="ForecastCartsNotSeasonallyAdjusted" ShowInGroupFooterColumn="ForecastCartsNotSeasonallyAdjusted" SummaryType="Sum" DisplayFormat="All={0:n2}" />
											<dx:ASPxSummaryItem FieldName="ForecastPCB" ShowInGroupFooterColumn="ForecastPCB" SummaryType="Custom" DisplayFormat="Sch={0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastPCB" ShowInGroupFooterColumn="ForecastPCB" SummaryType="Sum" DisplayFormat="All={0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastPCO" ShowInGroupFooterColumn="ForecastPCO" SummaryType="Custom" DisplayFormat="Sch={0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastPCO" ShowInGroupFooterColumn="ForecastPCO" SummaryType="Sum" DisplayFormat="All={0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastTPO" ShowInGroupFooterColumn="ForecastTPO" SummaryType="Custom" DisplayFormat="Sch={0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastTPO" ShowInGroupFooterColumn="ForecastTPO" SummaryType="Sum" DisplayFormat="All={0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastCartsPerK" ShowInGroupFooterColumn="ForecastCartsPerK" SummaryType="Custom" DisplayFormat="Sch={0:n2}" Tag="Sch" />
											<dx:ASPxSummaryItem FieldName="ForecastCartsPerK" ShowInGroupFooterColumn="ForecastCartsPerK" SummaryType="Custom" DisplayFormat="All={0:n2}" Tag ="All" />
											<dx:ASPxSummaryItem FieldName="ActualCarts" ShowInGroupFooterColumn="ActualTPOCarts" SummaryType="Sum" DisplayFormat="{0:n2}" />
											<dx:ASPxSummaryItem FieldName="ActualPCB" ShowInGroupFooterColumn="ActualPCB" SummaryType="Sum" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="ActualPCO" ShowInGroupFooterColumn="ActualPCO" SummaryType="Sum" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="ActualTPO" ShowInGroupFooterColumn="ActualTPO" SummaryType="Sum" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="ForecastCarts_NR" ShowInGroupFooterColumn="ForecastCarts_NR" SummaryType="Sum" DisplayFormat="{0:n2}" />
											<dx:ASPxSummaryItem FieldName="ForecastPO_NR" ShowInGroupFooterColumn="ForecastPO_NR" SummaryType="Sum" DisplayFormat="{0:n0}" />
											<dx:ASPxSummaryItem FieldName="ActualCarts_NR" ShowInGroupFooterColumn="ActualCarts_NR" SummaryType="Sum" DisplayFormat="{0:n2}" />
											<dx:ASPxSummaryItem FieldName="ActualPO_NR" ShowInGroupFooterColumn="ActualPO_NR" SummaryType="Sum" DisplayFormat="{0:n0}" />
										</TotalSummary>
										<SettingsBehavior ProcessSelectionChangedOnServer="true" />
										<SettingsPager PageSize="200" AlwaysShowPager="true" />
										<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="300" HorizontalScrollBarMode="Visible" ShowGroupedColumns="True" />
										<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowGroupFooter="VisibleIfExpanded" ShowFooter="True" />
										<SettingsEditing Mode="Batch" />
										<ClientSideEvents EndCallback="OnEndCallbackDetail" />
										<SettingsBehavior AllowFocusedRow="True" />
										<SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
 										<Templates>
											<DetailRow>
												<dx:ASPxGridView ID="grdHistoricalDetail" KeyFieldName="YearWeek;SectionID;PickupScheduleID" runat="server" 
													DataSourceID="dsMailSchedulePlanningHistoricalDetail" Width="95%" OnBeforePerformDataSelect="grdDetail_DataSelect">
													<Columns>
														<dx:GridViewDataColumn FieldName="YearWeek" Visible="false" />
														<dx:GridViewDataColumn FieldName="RegionID" Visible="false" />
														<dx:GridViewDataColumn FieldName="SectionID" Visible="false" />
														<dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="false" />
														<dx:GridViewDataTextColumn FieldName="Section" Caption="Section"  
															ReadOnly="true" Width="200px" FixedStyle="Left" VisibleIndex="1">
															<PropertiesTextEdit Width="100%" />
															<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="Charity" ReadOnly="true" Width="50px" VisibleIndex="1">
															<PropertiesTextEdit Width="100%" />
															<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataDateColumn FieldName="PickupDate" ReadOnly="true" Width="80px" VisibleIndex="3">
															<PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" Width="100%" />
															<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" HorizontalAlign="Center" />
														</dx:GridViewDataDateColumn>
														<dx:GridViewDataTextColumn FieldName="HistoricalCarts" ReadOnly="true" Width="70px" VisibleIndex="4">
															<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="HistoricalPCB" ReadOnly="true" Width="70px" VisibleIndex="5">
															<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="HistoricalPCO" ReadOnly="true" Width="70px" VisibleIndex="11">
															<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="HistoricalTPO" ReadOnly="true" Width="70px" VisibleIndex="12">
															<PropertiesTextEdit DisplayFormatString="#,#" Width="100%" />
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="HistoricalCartsPerK" ReadOnly="true" Width="70px" VisibleIndex="13">
															<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%" />
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
													</Columns>
													<SettingsBehavior ProcessSelectionChangedOnServer="true" />
													<SettingsPager PageSize="200" AlwaysShowPager="true" />
													<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="300" HorizontalScrollBarMode="Visible" />
													<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowGroupFooter="VisibleIfExpanded" ShowFooter="True" />
												</dx:ASPxGridView>
											</DetailRow>
										</Templates>
										<SettingsDetail ShowDetailRow="true" />
									</dx:ASPxGridView>
								</DetailRow>
							</Templates>
							<SettingsDetail ShowDetailRow="true" />
						</dx:ASPxGridView>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
		</Items>
	</dx:aspxformlayout>

    <asp:SqlDataSource ID="dsYearWeek" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT DISTINCT SP.YearWeek, C.CalendarYearWeekEnding
			FROM Production.SchedulePlanning AS SP
		    INNER JOIN Calendar AS C ON C.CalendarYearWeekNumber = SP.YearWeek
			ORDER BY YearWeek DESC">
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsMailSchedulePlanning" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="Production.spMailSchedulePlanning_SelectAll"
		SelectCommandType="StoredProcedure"
		UpdateCommand="Production.spMailSchedulePlanning_Update"
		UpdateCommandType="StoredProcedure">
		<SelectParameters>
			<asp:ControlParameter ControlID="fl$tbYearWeek" Name="YearWeek" PropertyName="Value" Type="Int32" />
		</SelectParameters>
		<UpdateParameters>
			<asp:ControlParameter ControlID="fl$tbYearWeek" Name="YearWeek" PropertyName="Value" Type="Int32" />
            <asp:Parameter Name="RegionID" Type="Int32" />
            <asp:Parameter Name="MailCartsToSchedule" Type="Decimal" />
            <asp:Parameter Name="ForecastNonRouteMailCarts" Type="Decimal" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</UpdateParameters>
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsMailSchedulePlanningDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
        SelectCommand="Production.spMailSchedulePlanningDetail_Select"
        SelectCommandType="StoredProcedure"
        UpdateCommand="Production.spMailSchedulePlanningDetail_Update"
        UpdateCommandType="StoredProcedure">
		<SelectParameters>
            <asp:SessionParameter Name="YearWeek" SessionField="YearWeek" Type="Int32" />
            <asp:SessionParameter Name="RegionID" SessionField="RegionID" Type="Int32" />
       </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="YearWeek" Type="Int32" />
            <asp:Parameter Name="RegionID" Type="Int32" />
            <asp:Parameter Name="SectionID" Type="Int32" />
            <asp:Parameter Name="Schedule" Type="Boolean" />
			<asp:ControlParameter ControlID="fl$ckSpecialDelete" Name="SpecialDelete" PropertyName="Checked" Type="Boolean" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
       </UpdateParameters>
    </asp:SqlDataSource>

	<asp:SqlDataSource ID="dsMailSchedulePlanningHistoricalDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
        SelectCommand="Production.spMailSchedulePlanningHistoricalDetail_Select"
        SelectCommandType="StoredProcedure">
		<SelectParameters>
            <asp:SessionParameter Name="YearWeek" SessionField="YearWeek" Type="Int32" />
            <asp:SessionParameter Name="SectionID" SessionField="SectionID" Type="Int32" />
       </SelectParameters>
    </asp:SqlDataSource>

	<asp:SqlDataSource ID="dsRegions" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT RegionID, RegionCode
			FROM tlkRegions
			ORDER BY RegionCode">
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
