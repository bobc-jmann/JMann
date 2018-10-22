<%@ Page Title="Other Production Costs" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="OtherProductionCosts.aspx.vb" Inherits="OtherProductionCosts" %>

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
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Other Production Costs" runat="server"></dx:ASPxLabel></td>
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
					<dx:LayoutItem Caption="Effective Date">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
								<dx:ASPxDateEdit ID="dtEffectiveDate" runat="server" 
									Width="100px" AutoPostBack="true">
									<TimeSectionProperties>
										<TimeEditProperties>
											<ClearButton Visibility="Auto">
											</ClearButton>
										</TimeEditProperties>
									</TimeSectionProperties>
									<ClearButton Visibility="Auto">
									</ClearButton>
								</dx:ASPxDateEdit>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
						<CaptionSettings VerticalAlign="Middle" />
					</dx:LayoutItem>
					<dx:LayoutItem Caption="Show All Data">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
								<dx:ASPxCheckBox ID="ckShowAllData" runat="server" 
									Checked="false" AutoPostBack="true">
								</dx:ASPxCheckBox>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
						<CaptionSettings VerticalAlign="Middle" />
					</dx:LayoutItem>
				</Items>
			</dx:LayoutGroup>
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsOtherProductionCosts" 
							KeyFieldName="ID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
							<Columns>
								<dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="90"
									ShowEditButton="true" ShowDeleteButton="true">
								</dx:GridViewCommandColumn>
								<dx:GridViewDataColumn FieldName="ID" Visible="false" /> 
								<dx:GridViewDataColumn FieldName="Category" Visible="false"  /> 
								<dx:GridViewDataComboBoxColumn FieldName="CostTypeID" Caption="Cost Type" Width="200px">
									<PropertiesComboBox DataSourceID="dsCostTypes" ValueType="System.Int32" 
										ValueField="ID" TextField="Description" IncrementalFilteringMode="StartsWith" />
								</dx:GridViewDataComboBoxColumn>
								<dx:GridViewDataTextColumn FieldName="Cost" Width="100px">
									<PropertiesTextEdit ClientInstanceName="Price" DisplayFormatString="#,0.0000" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataDateColumn FieldName="ValidFrom" HeaderStyle-HorizontalAlign="Center" /> 
								<dx:GridViewDataDateColumn FieldName="ValidTo" HeaderStyle-HorizontalAlign="Center" /> 
							</Columns>
							<GroupSummary>
								<dx:ASPxSummaryItem FieldName="Cost" ShowInGroupFooterColumn="Cost" 
									SummaryType="Sum" DisplayFormat="Total: {0:n4}" />
							</GroupSummary>
							<SettingsEditing Mode="Inline" />
							<SettingsPager PageSize="200" AlwaysShowPager="True" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
							<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
						</dx:ASPxGridView>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
		</Items>
	</dx:aspxformlayout>

	<asp:SqlDataSource ID="dsOtherProductionCosts" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="spOtherProductionCosts_Select"
		SelectCommandType="StoredProcedure"
		InsertCommand="spOtherProductionCosts_Insert"
		InsertCommandType="StoredProcedure"
		UpdateCommand="spOtherProductionCosts_Update"
		UpdateCommandType="StoredProcedure"
		DeleteCommand="spOtherProductionCosts_Delete"
		DeleteCommandType="StoredProcedure">
		<SelectParameters>
			<asp:ControlParameter ControlID="fl$dtEffectiveDate" Name="effectiveDate" PropertyName="Value" Type="DateTime" />
			<asp:ControlParameter ControlID="fl$ckShowAllData" Name="showAllData" PropertyName="Checked" Type="Boolean" />
		</SelectParameters>
		<InsertParameters>
			<asp:Parameter Name="CostTypeID" Type="Int32" />
			<asp:Parameter Name="Cost" Type="Decimal" />
			<asp:Parameter Name="ValidFrom" Type="DateTime" />
			<asp:Parameter Name="ValidTo" Type="DateTime" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="ID" Type="Int32" />
			<asp:Parameter Name="CostTypeID" Type="Int32" />
			<asp:Parameter Name="Cost" Type="Decimal" />
			<asp:Parameter Name="ValidFrom" Type="DateTime" />
			<asp:Parameter Name="ValidTo" Type="DateTime" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</UpdateParameters>
		<DeleteParameters>
			<asp:Parameter Name="ID" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</DeleteParameters>
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="dsCostTypes" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT ID, [Description] FROM tlkOtherProductionCostTypes ORDER BY SortOrder">
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
