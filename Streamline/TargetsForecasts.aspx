<%@ Page Title="Targets & Forecasts" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="TargetsForecasts.aspx.vb" Inherits="TargetsForecasts" %>

<!DOCTYPE html>
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
            var height = Math.max(0, document.documentElement.clientHeight) - 160;
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
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding="2" cellspacing="0" style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Targets & Forecasts" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
 	<dx:ASPxHiddenField ID="hf" runat="server" />
	<dx:aspxformlayout ID="fl" runat="server" ColCount="2" EnableTheming="True" Theme="Youthful" Width="70%">
		<Items>
			<dx:LayoutGroup ShowCaption="false" ColCount="2" ColSpan="2">
				<Items>
					<dx:LayoutItem Caption="Region">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
								<dx:ASPxComboBox ID="Region" runat="server"
									DataSourceID="dsRegions" ValueType="System.Int32" 
									UseSubmitBehavior="false" AutoPostBack="true"
									DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
									TextField="RegionCode" ValueField="RegionID"  Width="100px">
								</dx:ASPxComboBox>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
						<CaptionSettings VerticalAlign="Middle" />
					</dx:LayoutItem>
					<dx:LayoutItem Caption="Year">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
								<dx:ASPxComboBox ID="Year" runat="server"
									DataSourceID="dsYears" ValueType="System.Int32" 
									UseSubmitBehavior="false" AutoPostBack="true"
									DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
									TextField="Year" ValueField="Year"  Width="100px">
								</dx:ASPxComboBox>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
						<CaptionSettings VerticalAlign="Middle" />
					</dx:LayoutItem>
				</Items>
			</dx:LayoutGroup>
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsTargetsForecasts" 
							KeyFieldName="YearWeek;RegionID" Width="100%" EnableRowsCache="false" 
							OnCellEditorInitialize="grid_CellEditorInitialize"			
							SettingsBehavior-ConfirmDelete="True">
							<Columns>
								<dx:GridViewDataColumn FieldName="YearWeek" Visible="false" />
								<dx:GridViewDataColumn FieldName="RegionID" Visible="false" />
								<dx:GridViewDataTextColumn FieldName="Week" Width="60px">
									<PropertiesTextEdit DisplayFormatString="#" Width="60%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="TargetInventory" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastCartsWorked" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastBagCarts" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastSpecialsCarts" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastDropoffCarts" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastContainerCarts"  Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastPurchasedCarts" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastNonRoutePickupsPerCart" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ForecastNonRoutePickupsPerK" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
							</Columns>
							<SettingsEditing Mode="Batch" />
							<SettingsPager PageSize="200" AlwaysShowPager="True" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
							<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
						</dx:ASPxGridView>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
		</Items>
	</dx:aspxformlayout>

	<asp:SqlDataSource ID="dsTargetsForecasts" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="Production.spTargetsForecasts_Select"
		SelectCommandType="StoredProcedure"
		InsertCommand="Production.spTargetsForecasts_Insert"
		InsertCommandType="StoredProcedure"
		UpdateCommand="Production.spTargetsForecasts_Update"
		UpdateCommandType="StoredProcedure"
		DeleteCommand="Production.spTargetsForecasts_Delete"
		DeleteCommandType="StoredProcedure">
		<SelectParameters>
			<asp:ControlParameter ControlID="fl$Region" Name="RegionID" PropertyName="Value" Type="Int32" />
			<asp:ControlParameter ControlID="fl$Year" Name="Year" PropertyName="Value" Type="Int32" />
		</SelectParameters>
		<InsertParameters>
			<asp:Parameter Name="YearWeek" Type="Int32" />
			<asp:Parameter Name="TargetInventory" Type="Decimal" />
			<asp:Parameter Name="ForecastCartsWorked" Type="Decimal" />
			<asp:Parameter Name="ForecastBagCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastSpecialsCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastDropoffCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastContainerCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastPurchasedCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastNonRoutePickupsPerCart" Type="Decimal" />
			<asp:Parameter Name="ForecastNonRoutePickupsPerK" Type="Decimal" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="YearWeek" Type="Int32" />
			<asp:Parameter Name="TargetInventory" Type="Decimal" />
			<asp:Parameter Name="ForecastCartsWorked" Type="Decimal" />
			<asp:Parameter Name="ForecastBagCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastSpecialsCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastDropoffCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastContainerCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastPurchasedCarts" Type="Decimal" />
			<asp:Parameter Name="ForecastNonRoutePickupsPerCart" Type="Decimal" />
			<asp:Parameter Name="ForecastNonRoutePickupsPerK" Type="Decimal" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</UpdateParameters>
		<DeleteParameters>
			<asp:ControlParameter ControlID="fl$Region" Name="RegionID" PropertyName="Value" Type="Int32" />
			<asp:ControlParameter ControlID="fl$Year" Name="Year" PropertyName="Value" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</DeleteParameters>
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="dsRegions" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT RegionID, RegionCode
			FROM tlkRegions
			ORDER BY RegionCode">
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="dsYears" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT DISTINCT Year
			FROM Production.TargetsForecasts
			ORDER BY Year DESC">
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
