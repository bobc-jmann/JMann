<%@ Page Title="Driver-Bagger Costs" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="DriverBaggerCosts.aspx.vb" Inherits="DriverBaggerCosts" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Driver-Bagger Costs" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
 	<dx:ASPxHiddenField ID="hf" runat="server" />
	<dx:aspxformlayout ID="fl" runat="server" ColCount="2" EnableTheming="True" Theme="DevEx" Width="70%">
		<Items>
			<dx:LayoutGroup ShowCaption="false" ColCount="2" ColSpan="2">
				<Items>
					<dx:LayoutItem Caption="Year-Month">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
								<dx:ASPxComboBox ID="yearMonth" runat="server"
									DataSourceID="dsYearMonth" ValueType="System.String" 
									UseSubmitBehavior="false" AutoPostBack="true"
									DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
									TextField="YearMonth" ValueField="YearMonthNumber"  Width="100px">
								</dx:ASPxComboBox>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
						<CaptionSettings VerticalAlign="Middle" />
					</dx:LayoutItem>
					<dx:LayoutItem ShowCaption="False">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
								<dx:ASPxButton ID="btnCreateYearMonth" runat="server" 
									Text="Create Year-Month" Width="150px" OnClick="btnCreateYearMonth_Click">
								</dx:ASPxButton>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
						<CaptionSettings VerticalAlign="Middle" />
					</dx:LayoutItem>
				</Items>
			</dx:LayoutGroup>
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsDriverBaggerCosts" 
							KeyFieldName="YearMonth;StoreID" Width="100%" EnableRowsCache="false">
							<Columns>
								<dx:GridViewDataComboBoxColumn FieldName="YearMonth" Width="100px" ReadOnly="true">
									<PropertiesComboBox DataSourceID="dsYearMonth" ValueType="System.Int32" 
										ValueField="YearMonthNumber" TextField="YearMonth" IncrementalFilteringMode="StartsWith" />
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataComboBoxColumn>
								<dx:GridViewDataComboBoxColumn FieldName="StoreID" Caption="Store" ReadOnly="true">
									<PropertiesComboBox DataSourceID="dsStores" ValueType="System.Int32" 
										ValueField="LocationID" TextField="AccountingDescription" IncrementalFilteringMode="StartsWith" />
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataComboBoxColumn>
								<dx:GridViewDataTextColumn FieldName="DriverCost" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="BaggerCost" Width="100px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
							</Columns>
							<GroupSummary>
								<dx:ASPxSummaryItem FieldName="Cost" ShowInGroupFooterColumn="Cost" 
									SummaryType="Sum" DisplayFormat="Total: {0:n4}" />
							</GroupSummary>
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

	<asp:SqlDataSource ID="dsDriverBaggerCosts" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="spDriverBaggerCosts_Select"
		SelectCommandType="StoredProcedure"
		UpdateCommand="spDriverBaggerCosts_Update"
		UpdateCommandType="StoredProcedure">
		<SelectParameters>
			<asp:ControlParameter ControlID="fl$YearMonth" Name="YearMonth" PropertyName="Value" Type="Int32" />
		</SelectParameters>
		<UpdateParameters>
			<asp:Parameter Name="YearMonth" Type="Int32" />
			<asp:Parameter Name="StoreID" Type="Int32" />
			<asp:Parameter Name="DriverCost" Type="Decimal" />
			<asp:Parameter Name="BaggerCost" Type="Decimal" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="dsYearMonth" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT YearMonthNumber, YearMonth 
			FROM Calendar
			WHERE [Date] BETWEEN '20150101' AND SYSDATETIME()
			GROUP BY YearMonthNumber, YearMonth
			ORDER BY YearMonthNumber DESC">
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="dsStores" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT LocationID, AccountingDescription
			FROM tblLocations
			WHERE AccountingDescription IS NOT NULL
			ORDER BY AccountingDescriptionSortOrder">
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
