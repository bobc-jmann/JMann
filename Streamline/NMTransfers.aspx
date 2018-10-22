<%@ Page Title="Transfers" Language="VB" Theme="Youthful" AutoEventWireup="true" CodeFile="NMTransfers.aspx.vb" Inherits="NMTranfers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script>
    	function ConfirmDelete(s, e) {
    		if (confirm("Confirm delete?")) {
    			dirtyEditors = {};
    			hf.Set('delete', true);
    		}
    		else
    			hf.Set('delete', false);
    	}
    </script>
    <script type="text/javascript">
    	var dirtyEditors = {};
    	function OnControlsInitialized(s, e) {
    		var controls = ASPxClientControl.GetControlCollection().elements;
    		for (var ctrlName in controls) {
    			var ctrl = controls[ctrlName];
    			if (typeof ctrl.ValueChanged === "undefined") continue;
    			if (ctrlName == "flT_navigateComboBox_DDD_L") continue;
    			if (ctrlName == "flT_navigateComboBox") continue;
    			if (ctrlName == "flT_dtOldestTransferDate") continue;
    			if (ctrlName.substring(0, 9) == "flT_grid") continue;
    			if (ctrlName.substring(0, 10) == "popNewItem") continue;

    			ctrl.cpInitialValue = ctrl.GetValue();
    			ctrl.cpName = ctrlName;
    			ctrl.ValueChanged.AddHandler(function (s, e) {
    				var newValue = s.GetValue();
    				if (newValue == s.cpInitialValue)
    					delete dirtyEditors[s.cpName];
    				else
    					dirtyEditors[s.cpName] = newValue;
    			});
    		}
    		// Create listener for beforeunload
    		window.addEventListener("beforeunload", function (e) {
    			if (Object.keys(dirtyEditors).length > 0)
    				return ("You have made changes on this page that you have not yet confirmed. If you navigate away from this page you will lose your unsaved changes");
    		});
    	}
    	function ClearDirtyEditors(s, e) {
    		dirtyEditors = {};
    		// If the user changes the selection and elects to stay on the selected page because they have edited a field,
    		//  the event causing the change will occur on postback before the update. So change it back.
    		if (s.name == "flT_updateButton" && !hf.Get("InsertMode")) {
    			var TransferNum = document.getElementById('hfTransferNum');
    			navigateComboBox.SetValue(TransferNum.value);
    		}
		}

     	function OnItemNumberSelectedIndexChanged(s, e) {
    		var ItemNumber = s.GetValue();
    		var TransferNum = 0;
    		if (!(navigateComboBox === undefined))
    			TransferNum = navigateComboBox.GetValue();

    		// if add new item, open the popup
    		if (ItemNumber == "<New Item>") {
    			// Clear form
    			puItemNumber.SetText("");
    			puUPC.SetText("");
    			puLabelTypeID.SetValue(0);
    			puItemDescription.SetText("");
    			puCurrentCost.SetText("");
    			puSubDepartmentID.SetValue(0);
    			puStandardPrice.SetText("");
    			puQtyTransferred.SetText("");
    			// Clear Editors
    			var itemDescriptionEditor = grid.GetEditor('ItemDescription');
    			itemDescriptionEditor.SetText("");
    			var unitCostEditor = grid.GetEditor('UnitCost');
    			unitCostEditor.SetText("");
    			var priceEditor = grid.GetEditor('Price');
    			priceEditor.SetText("");
  				var qtyEditor = grid.GetEditor('QtyTransferred');
   				qtyEditor.SetText("");
    		} else {
    			PageMethods.GetItemInfo(TransferNum, ItemNumber, 0, OnItemNumberSuccess, OnItemNumberError);
    		}
	   	}

    	function OnItemNumberSuccess(result) {
     		var itemDescriptionEditor = grid.GetEditor('ItemDescription');
    		itemDescriptionEditor.SetText(result.Description);

   			var unitCostEditor = grid.GetEditor('UnitCost');
   			unitCostEditor.SetText(result.CurrentCost);

   			var priceEditor = grid.GetEditor('Price');
   			priceEditor.SetText(result.StandardPrice);

   			if (result.QtyTransferred > 0) {
   				var itemEditor = grid.GetEditor('ItemNumber');
   				itemEditor.SetText(result.ItemNumber);

   				var qtyEditor = grid.GetEditor('QtyTransferred');
   				qtyEditor.SetText(result.QtyTransferred);
   			}
    	} 

    	function OnItemNumberError() {
    		alert('ItemNumber Error');
    	} 

    	var currentCommand = '';
    	var addNewRow = false;

    	function OnLineItemValueChanged(s, e) {
    		currentCommand = e.command;
    		if (e.command == "ADDNEWROW")
    			addNewRow = true;
    		if (e.command == "STARTEDIT" || e.command == "ADDNEWROW" || e.command == "DELETEROW")
    			return;
   			var qtyTransferredEditor = grid.GetEditor('QtyTransferred');
    		var unitCostEditor = grid.GetEditor('UnitCost');
    	}

    	function OnEndCallback(s, e) {
    		if (currentCommand == 'UPDATEEDIT' && addNewRow)
    			grid.AddNewRow();
    		if (currentCommand != 'UPDATEEDIT' && currentCommand != 'ADDNEWROW')
    			addNewRow = false;
    	}

    	function OnFromStoreSelectedIndexChanged(s, e) {
    		hf.Set("FromStoreChanged", true);
    		grid.Refresh();
    	}

    	function OnToStoreSelectedIndexChanged(s, e) {
    		hf.Set("ToStoreChanged", true);
    		grid.Refresh();
		}
    </script>
</head>

<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
		<dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
        </dx:ASPxGlobalEvents>
		<div style="position:relative;top:0;left:0;">
			<table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
				<tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
					<td>
						<dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" 
							Text="Transfers" runat="server"></dx:ASPxLabel>
					</td>
				</tr>
			</table>
		</div>
		<br />
		<br />
		<br />
		<div>
			<dx:aspxhiddenfield ID="hf" runat="server" />
			<asp:HiddenField ID="hfTransferNum" runat="server" />
			<asp:HiddenField ID="hfOldestTransferDate" runat="server" />
			<dx:aspxformlayout ID="flT" runat="server" ColCount="6" DataSourceID="dsTransferHeaders" 
				EnableTheming="True" Theme="Youthful" Width="100%">
			<Items>
				<dx:LayoutGroup Caption="Controls" ShowCaption="false" ColCount="5" ColSpan="6" Name="Controls">
					<Items>
						<dx:LayoutItem Caption="Select Transfer" Name="navigateComboBox" HorizontalAlign="Center">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
									<dx:ASPxComboBox ID="navigateComboBox" runat="server" 
										UseSubmitBehavior="false" AutoPostBack="True" CallbackPageSize="30" 
										ClientInstanceName="navigateComboBox" DataSourceID="dsTransferHeaders_SelectList" 
										EnableCallbackMode="True" IncrementalFilteringMode="StartsWith" 
										OnSelectedIndexChanged="navigateComboBox_SelectedIndexChanged" 
										TextFormatString="{0}" ValueField="TransferNum" Width="150px">
										<Columns>
											<dx:ListBoxColumn FieldName="TransferNum" Caption="Number" Width="60px" />
											<dx:ListBoxColumn FieldName="TransferDate" Caption="Date" Width="100px" />
											<dx:ListBoxColumn FieldName="FromStoreName" Caption="From Store" Width="80px" />
											<dx:ListBoxColumn FieldName="ToStoreNAme" Caption="To Store" Width="80px" />
										</Columns>
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem />
						<dx:LayoutItem Caption="Oldest Transfer">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxDateEdit ID="dtOldestTransferDate" runat="server" Width="100px" AutoPostBack="true">
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
						<dx:EmptyLayoutItem ColSpan="2">
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup ShowCaption="false" ColCount="5" ColSpan="6">
					<Items>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
									<dx:ASPxButton ID="newButton" runat="server" OnClick="newButton_Click" 
										Text="New" Width="150px" UseSubmitBehavior="false">
										<ClientSideEvents Click="function(s, e) { ClearDirtyEditors(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
									<dx:ASPxButton ID="updateButton" runat="server" OnClick="updateButton_Click" 
										Text="Update" Visible="False" Width="150px" UseSubmitBehavior="false">
										<ClientSideEvents Click="function(s, e) { ClearDirtyEditors(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
									<dx:ASPxButton ID="cancelButton" runat="server" OnClick="cancelButton_Click" 
										Text="Cancel" Visible="False" Width="150px" UseSubmitBehavior="false">
										<ClientSideEvents Click="function(s, e) { ClearDirtyEditors(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
									<dx:ASPxButton ID="deleteButton" runat="server" OnClick="deleteButton_Click" 
										Text="Delete" Visible="False" Width="150px" UseSubmitBehavior="false">
										<ClientSideEvents Click="function(s, e) { ConfirmDelete(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Header" ColCount="6" ColSpan="6">
					<Items>
						<dx:EmptyLayoutItem ColSpan="4">
						</dx:EmptyLayoutItem>
						<dx:LayoutItem ColSpan="2" FieldName="TransferDate">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
									<dx:ASPxDateEdit ID="tbTransferDate" runat="server"  Width="100px">
										<TimeSectionProperties>
											<TimeEditProperties>
												<ClearButton Visibility="Auto">
												</ClearButton>
											</TimeEditProperties>
										</TimeSectionProperties>
										<ClientSideEvents ValueChanged="function (s, e) {}" />
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxDateEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:EmptyLayoutItem ColSpan="4">
						</dx:EmptyLayoutItem>
						<dx:LayoutItem FieldName="TransferNum" ColSpan="2" ShowCaption="True">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
									<dx:ASPxTextBox ID="tbTransferNum" runat="server" Width="100px" 
										ReadOnly="true" BackColor="WhiteSmoke">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="FromStoreID" Caption="From Store" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
									<dx:ASPxComboBox ID="tbFromStoreID" ClientInstanceName="tbFromStoreID" runat="server" 
										DataSourceID="dsFromStores" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="StoreName" ValueField="StoreID" Width="200px">
										<ClearButton Visibility="Auto" />
										<ClientSideEvents ValueChanged="OnFromStoreSelectedIndexChanged" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="ToStoreID" Caption="To Store" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer36" runat="server">
									<dx:ASPxComboBox ID="tbToStoreID" ClientInstanceName="tbToStoreID" runat="server" 
										DataSourceID="dsToStores" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="StoreNAme" ValueField="StoreID" Width="200px">
										<ClearButton Visibility="Auto" />
										<ClientSideEvents ValueChanged="OnToStoreSelectedIndexChanged" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem ColSpan="2">
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Items" ColCount="6" ColSpan="6">
					<Items>
						<dx:LayoutItem ColSpan="6" ShowCaption="False">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer>
									<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsTransferItems" 
										KeyFieldName="LineNumber" Width="100%" EnableRowsCache="true"
										OnDataBound="grid_DataBound"
										OnCommandButtonInitialize="grid_CommandButtonInitialize"
										OnCellEditorInitialize="grid_CellEditorInitialize"
										OnRowDeleting="grid_RowDeleting"
										OnRowValidating="grid_RowValidating">
										<Columns>
											<dx:GridViewCommandColumn Caption=" " Name="cmd" 
												ShowNewButtonInHeader="true" Width="80" ShowEditButton="true" ShowDeleteButton="true" />
											<dx:GridViewDataColumn FieldName="TransferNum" Visible="False" /> 
											<dx:GridViewDataColumn FieldName="LineNumber" Visible="False" /> 
											<dx:GridViewDataComboBoxColumn FieldName="ItemNumber" Caption="Item" VisibleIndex="1" Width="120px">
												<PropertiesComboBox Width="100%"
													ValueType="System.String" ValueField="Item" TextField="ItemNumber" ClientInstanceName="ItemNumber" 
													EnableCallbackMode="true" CallbackPageSize="10"
													OnItemsRequestedByFilterCondition="ItemNumber_ItemsRequestedByFilterCondition"
													OnItemRequestedByValue="ItemNumber_ItemRequestedByValue"
													IncrementalFilteringMode="StartsWith" >
													<ClearButton Visibility="Auto" />
													<ClientSideEvents SelectedIndexChanged="OnItemNumberSelectedIndexChanged" />
												</PropertiesComboBox>
											</dx:GridViewDataComboBoxColumn>
											<dx:GridViewDataTextColumn FieldName="ItemDescription" Caption="Description" VisibleIndex="2">
												<PropertiesTextEdit ClientInstanceName="ItemDescription">
												</PropertiesTextEdit>
												<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="Price" Caption="Eco-Price" VisibleIndex="3" Width="100px">
												<PropertiesTextEdit ClientInstanceName="Price" DisplayFormatString="#,0.00" Width="100%">
												</PropertiesTextEdit>
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="QtyTransferred" VisibleIndex="4" Width="100px">
												<PropertiesTextEdit ClientInstanceName="QtyTransferred" DisplayFormatString="#,#" Width="100%">
													<ClientSideEvents ValueChanged="OnLineItemValueChanged" />
												</PropertiesTextEdit>
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="UnitCost" VisibleIndex="5" Width="100px">
												<PropertiesTextEdit ClientInstanceName="UnitCost" DisplayFormatString="#,0.0000" Width="100%">
													<ClientSideEvents ValueChanged="OnLineItemValueChanged" />
												</PropertiesTextEdit>
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataTextColumn>
										</Columns>
										<SettingsPager PageSize="500" AlwaysShowPager="False" />
										<SettingsEditing Mode="Inline" />
										<ClientSideEvents BeginCallback="OnLineItemValueChanged" EndCallback="OnEndCallback" />
									</dx:ASPxGridView>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Totals" ColCount="2" ColSpan="6">
					<Items>
						<dx:LayoutItem FieldName="Notes" Caption="Other Comments or Special Instructions" RowSpan="6" Height="100%">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
									<dx:ASPxMemo ID="tbNotes" runat="server" Height="120px" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxMemo>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
			</Items>
		</dx:aspxformlayout>
			<dx:ASPxLabel runat="server" ID="errorMessageLabel" Visible="false" ForeColor="Red" EnableViewState="false" Font-Size="Medium" />
		</div>

        <asp:SqlDataSource ID="dsTransferHeaders_SelectList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spTransferHeaders_SelectList"
			SelectCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfOldestTransferDate" Name="OldestTransferDate" PropertyName="Value" Type="DateTime" />
			</SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsTransferHeaders" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spTransferHeaders_Select"
			SelectCommandType="StoredProcedure"
            InsertCommand="NewMerch.spTransferHeaders_Insert"
			InsertCommandType="StoredProcedure"
			OnInserted ="On_Inserted"
			UpdateCommand="NewMerch.spTransferHeaders_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spTransferHeaders_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfTransferNum" Name="TransferNum" PropertyName="Value" Type="Int32" />
			</SelectParameters>
            <InsertParameters>
				<asp:ControlParameter ControlID="hfTransferNum" Name="TransferNum" Direction="Output" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbFromStoreID" Name="FromStoreID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbToStoreID" Name="ToStoreID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbTransferDate" Name="TransferDate" PropertyName="Value" Type="DateTime" />
				<asp:ControlParameter ControlID="flT$tbNotes" Name="Notes" PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
				<asp:ControlParameter ControlID="hfTransferNum" Name="TransferNum" Direction="Input" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbFromStoreID" Name="FromStoreID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbToStoreID" Name="ToStoreID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbTransferDate" Name="TransferDate" PropertyName="Value" Type="DateTime" />
				<asp:ControlParameter ControlID="flT$tbNotes" Name="Notes" PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
				<asp:ControlParameter ControlID="hfTransferNum" Name="TransferNum" PropertyName="Value" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </DeleteParameters>
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsFromStores" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS StoreID, '<Select Store>' AS StoreName UNION SELECT StoreID, StoreName FROM Corporate.Stores ORDER BY StoreName">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsToStores" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS StoreID, '<Select Store>' AS StoreName UNION SELECT StoreID, StoreName FROM Corporate.Stores ORDER BY StoreName">
        </asp:SqlDataSource>


		<asp:SqlDataSource ID="dsTransferItems" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spTransferItems_Select"
			SelectCommandType="StoredProcedure"
            InsertCommand="NewMerch.spTransferItems_Insert"
			InsertCommandType="StoredProcedure"
            UpdateCommand="NewMerch.spTransferItems_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spTransferItems_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfTransferNum" Name="TransferNum" PropertyName="Value" Type="Int32" />
			</SelectParameters>
			<InsertParameters>
				<asp:ControlParameter ControlID="hfTransferNum" Name="TransferNum" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbFromStoreID" Name="FromStoreID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbToStoreID" Name="ToStoreID" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="LineNumber" Direction="Output" Type="Int32" />
				<asp:Parameter Name="ItemNumber" Type="String" />
				<asp:Parameter Name="QtyTransferred" Type="Int32" />
				<asp:Parameter Name="UnitCost" Type="Decimal" />
				<asp:Parameter Name="Price" Type="Decimal" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</InsertParameters>
			<UpdateParameters>
				<asp:ControlParameter ControlID="hfTransferNum" Name="TransferNum" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbFromStoreID" Name="FromStoreID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flT$tbToStoreID" Name="ToStoreID" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="LineNumber" Direction="Output" Type="Int32" />
				<asp:Parameter Name="ItemNumber" Type="String" />
				<asp:Parameter Name="QtyTransferred" Type="Int32" />
				<asp:Parameter Name="UnitCost" Type="Decimal" />
				<asp:Parameter Name="Price" Type="Decimal" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</UpdateParameters>
 		</asp:SqlDataSource>

        <asp:SqlDataSource ID="dsInventoryItems" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>">
        </asp:SqlDataSource>

		<script type="text/javascript">
			var __oldDoPostBack = __doPostBack;
		 	__doPostBack = CatchExplorerError;

		 	function CatchExplorerError (eventTarget, eventArgument)
		 	{
		 		try
		 		{
		 			return __oldDoPostBack (eventTarget, eventArgument);
		 		} 
		 		catch (ex)
		 		{
		 			// don't want to mask a genuine error
		 			// lets just restrict this to our 'Unspecified' one
		 			if (ex.message.indexOf('Unspecified') == -1)
		 			{
		 				throw ex;
		 			}
		 		}
		 	}
		</script>
    </form>
</body>
</html>
