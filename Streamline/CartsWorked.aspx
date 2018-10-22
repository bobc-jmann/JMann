<%@ Page Title="Carts Worked" Language="VB" Theme="Youthful" AutoEventWireup="false" CodeFile="CartsWorked.aspx.vb" Inherits="CartsWorked" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Carts Worked" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
	<asp:HiddenField ID="hfOldestDateWorked" runat="server" />
 	<dx:ASPxHiddenField ID="hf" runat="server" />
	<dx:aspxformlayout ID="fl" runat="server" ColCount="2" EnableTheming="True" Theme="Youthful" Width="90%">
		<Items>
			<dx:LayoutGroup ShowCaption="false" ColCount="2" ColSpan="2">
				<Items>
					<dx:LayoutItem Caption="Store">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
								<dx:ASPxComboBox ID="Store" runat="server"
									DataSourceID="dsStores" ValueType="System.String" 
									UseSubmitBehavior="false" AutoPostBack="true"
									DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
									TextField="StoreDescription" ValueField="FinanceLocationID"  Width="200px">
								</dx:ASPxComboBox>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
						<CaptionSettings VerticalAlign="Middle" />
					</dx:LayoutItem>
					<dx:LayoutItem Caption="Oldest Date Worked">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
								<dx:ASPxDateEdit ID="dtOldestDateWorked" runat="server" Width="100px" AutoPostBack="true">
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
				</Items>
			</dx:LayoutGroup>
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsCartsWorked" 
							KeyFieldName="CartsWorkedID" Width="100%" EnableRowsCache="false" 
							SettingsBehavior-ConfirmDelete="True" OnCommandButtonInitialize="grid_CommandButtonInitialize">
							<Columns>
								<dx:GridViewCommandColumn Width="80" ShowNewButtonInHeader="True" Caption=" " />
								<dx:GridViewDataColumn FieldName="CartsWorkedID" Visible="false" />
								<dx:GridViewDataDateColumn FieldName="DateWorked" Width="80px" /> 
								<dx:GridViewDataTextColumn FieldName="CartsWorkedSoft" Caption="Soft Carts" Width="100px">
									<PropertiesTextEdit DisplayFormatString="N2" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="CartsWorkedHard" Caption="Hard Carts" Width="100px">
									<PropertiesTextEdit DisplayFormatString="N2" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="HangMen" Width="100px">
									<PropertiesTextEdit DisplayFormatString="N0" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="HangWomen" Width="100px">
									<PropertiesTextEdit DisplayFormatString="N0" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="HangChild" Width="100px">
									<PropertiesTextEdit DisplayFormatString="N0" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="HangTotal" Width="100px">
									<PropertiesTextEdit DisplayFormatString="N0" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="ThrownLbs" Width="100px">
									<PropertiesTextEdit DisplayFormatString="N0" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataTextColumn FieldName="RaggedLbs" Width="100px">
									<PropertiesTextEdit DisplayFormatString="N0" Width="100%">
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

	<asp:SqlDataSource ID="dsCartsWorked" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="Stores.spCartsWorked_Select"
		SelectCommandType="StoredProcedure"
		InsertCommand="Stores.spCartsWorked_Insert"
		InsertCommandType="StoredProcedure"
		UpdateCommand="Stores.spCartsWorked_Update"
		UpdateCommandType="StoredProcedure"
		DeleteCommand="Stores.spCartsWorked_Delete"
		DeleteCommandType="StoredProcedure">
		<SelectParameters>
			<asp:ControlParameter ControlID="fl$Store" Name="FinanceLocationID" PropertyName="Value" Type="Int32" />
			<asp:ControlParameter ControlID="hfOldestDateWorked" Name="OldestCartWorkedDate" PropertyName="Value" Type="DateTime" />
		</SelectParameters>
		<InsertParameters>
			<asp:Parameter Name="DateWorked" Type="DateTime" />
			<asp:ControlParameter ControlID="fl$Store" Name="FinanceLocationID" PropertyName="Value" Type="Int32" />
			<asp:Parameter Name="CartsWorkedSoft" Type="Decimal" />
			<asp:Parameter Name="CartsWorkedHard" Type="Decimal" />
			<asp:Parameter Name="HangMen" Type="Int32" />
			<asp:Parameter Name="HangWomen" Type="Int32" />
			<asp:Parameter Name="HangChild" Type="Int32" />
			<asp:Parameter Name="HangTotal" Type="Int32" />
			<asp:Parameter Name="ThrownLbs" Type="Int32" />
			<asp:Parameter Name="RaggedLbs" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="CartsWorkedID" Type="Int32" />
			<asp:Parameter Name="DateWorked" Type="DateTime" />
			<asp:ControlParameter ControlID="fl$Store" Name="FinanceLocationID" PropertyName="Value" Type="Int32" />
			<asp:Parameter Name="CartsWorkedSoft" Type="Decimal" />
			<asp:Parameter Name="CartsWorkedHard" Type="Decimal" />
			<asp:Parameter Name="HangMen" Type="Int32" />
			<asp:Parameter Name="HangWomen" Type="Int32" />
			<asp:Parameter Name="HangChild" Type="Int32" />
			<asp:Parameter Name="HangTotal" Type="Int32" />
			<asp:Parameter Name="ThrownLbs" Type="Int32" />
			<asp:Parameter Name="RaggedLbs" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</UpdateParameters>
		<DeleteParameters>
			<asp:Parameter Name="CartsWorkedID" Type="Int32" />
		</DeleteParameters>
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="dsStores" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT FinanceLocationID, StoreDescription
			FROM qryStores
			ORDER BY StoreDescription">
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
