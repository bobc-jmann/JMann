<%@ Page Title="Purchase Orders" Language="VB" Theme="Youthful" AutoEventWireup="true" CodeFile="NMPurchaseOrders.aspx.vb" Inherits="NMPurchaseOrders" %>

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
    			if (ctrlName == "flPO_navigateComboBox_DDD_L") continue;
    			if (ctrlName == "flPO_navigateComboBox") continue;
    			if (ctrlName == "flPO_ckShowCompletedPurchaseOrders") continue;
    			if (ctrlName == "flPO_dtOldestPurchaseOrderDate") continue;
    			if (ctrlName.substring(0, 9) == "flPO_grid") continue;
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
    		if (s.name == "flPO_updateButton" && !hf.Get("InsertMode")) {
    			var purchaseOrderNum = document.getElementById('hfPurchaseOrderNum');
    			navigateComboBox.SetValue(purchaseOrderNum.value);
    		}
		}

    	function OnVendorSelectedIndexChanged(s, e) {
    		var VendorID = s.GetValue();
    		PageMethods.GetTaxRate(VendorID, OnVendorSuccess, OnVendorError);
    	}

    	function OnVendorSuccess(result) {
   			tbTaxRate.SetText(result);
   			OnHeaderValueChanged();
    	}

    	function OnVendorError(result) {
    		alert('Vendor Error');
    	}

    	function OnItemNumberSelectedIndexChanged(s, e) {
    		var ItemNumber = s.GetValue();
    		var PurchaseOrderNum = 0;
    		if (!(navigateComboBox === undefined))
    			PurchaseOrderNum = navigateComboBox.GetValue();

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
    			puQtyOrdered.SetText("");
    			// Clear Editors
    			var itemDescriptionEditor = grid.GetEditor('ItemDescription');
    			itemDescriptionEditor.SetText("");
    			var unitCostEditor = grid.GetEditor('UnitCost');
    			unitCostEditor.SetText("");
    			var priceEditor = grid.GetEditor('Price');
    			priceEditor.SetText("");
  				var qtyEditor = grid.GetEditor('QtyOrdered');
   				qtyEditor.SetText("");

    			pni.Show();
    		} else {
    			PageMethods.GetItemInfo(PurchaseOrderNum, ItemNumber, 0, OnItemNumberSuccess, OnItemNumberError);
    		}
	   	}

    	function OnItemNumberSuccess(result) {
    		pni.Hide();

    		var itemDescriptionEditor = grid.GetEditor('ItemDescription');
    		itemDescriptionEditor.SetText(result.Description);

   			var unitCostEditor = grid.GetEditor('UnitCost');
   			unitCostEditor.SetText(result.CurrentCost);

   			var priceEditor = grid.GetEditor('Price');
   			priceEditor.SetText(result.StandardPrice);

   			if (result.QtyOrdered > 0) {
   				var itemEditor = grid.GetEditor('ItemNumber');
   				itemEditor.SetText(result.ItemNumber);

   				var qtyEditor = grid.GetEditor('QtyOrdered');
   				qtyEditor.SetText(result.QtyOrdered);
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
   			var qtyOrderedEditor = grid.GetEditor('QtyOrdered');
    		var unitCostEditor = grid.GetEditor('UnitCost');
    		var amtOrderedEditor = grid.GetEditor('AmtOrdered');
    		var amtOrderedPrev = amtOrderedEditor.GetText();
    		if (isNaN(amtOrderedPrev))
    			amtOrderedPrev = 0;
    		amtOrderedEditor.SetText((qtyOrderedEditor.GetText() * unitCostEditor.GetText()).toFixed(2));
    		var amtOrderedPost = amtOrderedEditor.GetText();
    		if (isNaN(amtOrderedPost))
    			amtOrderedPost = 0;
    		tbSubtotal.SetValue((Number(tbSubtotal.GetValue()) - Number(amtOrderedPrev) + Number(amtOrderedPost)).toFixed(2));
    		OnHeaderValueChanged();
    	}

    	function OnEndCallback(s, e) {
    		if (currentCommand == 'UPDATEEDIT' && addNewRow)
    			grid.AddNewRow();
    		if (currentCommand != 'UPDATEEDIT' && currentCommand != 'ADDNEWROW')
    			addNewRow = false;
    	}

    	function OnHeaderValueChanged() {
    		tbTax.SetValue((Number(tbTaxRate.GetValue()) * Number(tbSubtotal.GetValue()) / 100).toFixed(2));
    		tbTotal.SetValue(Number(tbSubtotal.GetValue()) + Number(tbTax.GetValue()) +
				Number(tbShippingHandling.GetValue()) + Number(tbOther.GetValue()));
    	}
    </script>
	<script type="text/javascript">
		function SetPCVisible(value) {
			var popupControl = GetPopupControl();
			if (value)
				pni.Show();
			else
				pni.Hide();
		}

		function GetPopupControl() {
			return ASPxPopupClientControl;
		}

		function NewItemUpdateButton_Click(s, e) {
			var itemNumber = (!puItemNumber.GetValue() ? "" : puItemNumber.GetValue());
			var upc = (!puUPC.GetValue() ? "" : puUPC.GetValue());
			var labelTypeID = puLabelTypeID.GetValue();
			var itemDescription = (!puItemDescription.GetValue() ? "" : puItemDescription.GetValue());
			var currentCost = (!puCurrentCost.GetValue() ? 0 : puCurrentCost.GetValue());
			var subDepartmentID = puSubDepartmentID.GetValue();
			var standardPrice = (!puStandardPrice.GetValue() ? 0 : puStandardPrice.GetValue());
			var qtyOrdered = (!puQtyOrdered.GetValue() ? 0 : puQtyOrdered.GetValue());
			// Validate entries
			PageMethods.ValidateNewInventoryItem(itemNumber, upc, labelTypeID, itemDescription,
				currentCost, subDepartmentID, standardPrice, qtyOrdered,
				ValidateNewInventoryItemSuccess, ValidateNewInventoryItemError);
		}

		function ValidateNewInventoryItemSuccess(errorMessage) {
			if (errorMessage != "") {
				alert(errorMessage);
				return;
			}
			var itemNumber = (!puItemNumber.GetValue() ? "" : puItemNumber.GetValue());
			var upc = (!puUPC.GetValue() ? "" : puUPC.GetValue());
			var labelTypeID = puLabelTypeID.GetValue();
			var itemDescription = (!puItemDescription.GetValue() ? "" : puItemDescription.GetValue());
			var labelDescription = (!puLabelDescription.GetValue() ? "" : puLabelDescription.GetValue());
			var currentCost = (!puCurrentCost.GetValue() ? 0 : puCurrentCost.GetValue());
			var subDepartmentID = puSubDepartmentID.GetValue();
			var standardPrice = (!puStandardPrice.GetValue() ? 0 : puStandardPrice.GetValue());
			var qtyOrdered = (!puQtyOrdered.GetValue() ? 0 : puQtyOrdered.GetValue());
			var vendorID = tbVendorID.GetValue();
			var inventoryActive = (!puInventoryActive.GetValue() ? false : puInventoryActive.GetValue());
			var storeID = (!puStoreID.GetValue() ? "" : puStoreID.GetValue());
			// Insert new item in database
			PageMethods.InsertNewInventoryItem(itemNumber, upc, labelTypeID, itemDescription, labelDescription,
				currentCost, subDepartmentID, standardPrice, vendorID, inventoryActive, storeID, hf.Get("UserID"),
				InsertNewInventoryItemSuccess, InsertNewInventoryItemError);
		}

		function ValidateNewInventoryItemError(errorMessage) {
			alert('ValidateNewInventoryItem Error');
		}
 
		function InsertNewInventoryItemSuccess(errorMessage) {
			if (errorMessage != "") {
				alert(errorMessage);
				pni.Hide();
				return;
			}
			var PurchaseOrderNum = 0;
			if (!(navigateComboBox === undefined))
				PurchaseOrderNum = navigateComboBox.GetValue();

			var itemNumber = puItemNumber.GetValue();
			var qtyOrdered = puQtyOrdered.GetValue();
			// Fill in the order line
			PageMethods.GetItemInfo(PurchaseOrderNum, itemNumber, qtyOrdered, OnItemNumberSuccess, OnItemNumberError);
		}

		function InsertNewInventoryItemError(errorMessage) {
			alert('InsertNewInventoryItem Error');
		}

		function NewItemCancelButton_Click(s, e) {
			pni.Hide();
		}

		function GenerateUPC(s, e) {
			PageMethods.GenerateUpc(hf.Get("UserID"), GenerateUpcSuccess, GenerateUpcError);
		}

		function GenerateUpcSuccess(upc) {
			puUPC.SetText(upc);
		}

		function GenerateUpcError() {
			alert('GenerateUpc Error');
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
							Text="Purchase Orders" runat="server"></dx:ASPxLabel>
					</td>
				</tr>
			</table>
		</div>
		<br />
		<br />
		<br />
		<div>
			<dx:aspxhiddenfield ID="hf" runat="server" />
			<asp:HiddenField ID="hfPurchaseOrderNum" runat="server" />
			<asp:HiddenField ID="hfShowCompletedPurchaseOrders" runat="server" />
			<asp:HiddenField ID="hfOldestPurchaseOrderDate" runat="server" />
			<dx:aspxformlayout ID="flPO" runat="server" ColCount="6" DataSourceID="dsPurchaseOrderHeaders" 
				EnableTheming="True" Theme="Youthful" Width="100%">
			<Items>
				<dx:LayoutGroup Caption="Controls" ShowCaption="false" ColCount="5" ColSpan="6" Name="Controls">
					<Items>
						<dx:LayoutItem Caption="Select Purchase Order" Name="navigateComboBox" HorizontalAlign="Center">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
									<dx:ASPxComboBox ID="navigateComboBox" runat="server" 
										UseSubmitBehavior="false" AutoPostBack="True" CallbackPageSize="30" 
										ClientInstanceName="navigateComboBox" DataSourceID="dsPurchaseOrderHeaders_SelectList" 
										EnableCallbackMode="True" IncrementalFilteringMode="StartsWith" 
										OnSelectedIndexChanged="navigateComboBox_SelectedIndexChanged" 
										TextFormatString="{0}" ValueField="PurchaseOrderNum" Width="150px">
										<Columns>
											<dx:ListBoxColumn FieldName="PurchaseOrderNum" Caption="Number" Width="60px" />
											<dx:ListBoxColumn FieldName="PurchaseOrderDate" Caption="Date" Width="100px" />
											<dx:ListBoxColumn FieldName="VendorName" Caption="Vendor" Width="250px" />
											<dx:ListBoxColumn FieldName="StoreName" Caption="ShipTo" Width="80px" />
										</Columns>
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem Caption="Show Completed">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxCheckBox ID="ckShowCompletedPurchaseOrders" runat="server" Checked="false" AutoPostBack="true">
									</dx:ASPxCheckBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem Caption="Oldest Order">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxDateEdit ID="dtOldestPurchaseOrderDate" runat="server" Width="100px" AutoPostBack="true">
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
						<dx:LayoutItem ColSpan="2" FieldName="PurchaseOrderDate">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
									<dx:ASPxDateEdit ID="tbPurchaseOrderDate" runat="server"  Width="100px">
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
						<dx:LayoutItem FieldName="PurchaseOrderNum" ColSpan="2" ShowCaption="True">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
									<dx:ASPxTextBox ID="tbPurchaseOrderNum" runat="server" Width="100px" 
										ReadOnly="true" BackColor="WhiteSmoke">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="VendorID" Caption="Vendor" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
									<dx:ASPxComboBox ID="tbVendorID" ClientInstanceName="tbVendorID" runat="server" 
										DataSourceID="dsVendors_SelectList" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="VendorName" ValueField="VendorID" Width="200px">
										<ClearButton Visibility="Auto" />
										<ClientSideEvents ValueChanged="OnVendorSelectedIndexChanged" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="StoreID" Caption="Ship To" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
									<dx:ASPxComboBox ID="tbStoreID" ClientInstanceName="puStoreID" runat="server"
										DataSourceID="dsStores" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="StoreName" ValueField="StoreID" Width="120px">
										<ClearButton Visibility="Auto" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Status">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
									<dx:ASPxTextBox ID="tbStatus" runat="server" Checked="false" 
										ReadOnly="true" BackColor="WhiteSmoke" Width="100px">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem FieldName="VendorOrderNum" Caption="Vendor Order">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
									<dx:ASPxTextBox ID="tbVendorOrderNum" runat="server" Width="150px">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="OrderedBy">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">
									<dx:ASPxTextBox ID="tbOrderedBy" runat="server" Width="150px">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="ShipVia">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
									<dx:ASPxTextBox ID="tbShipVia" runat="server" Width="150px">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="FOB">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
									<dx:ASPxTextBox ID="tbFOB" runat="server" Width="150px">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="ShippingTerms" ColSpan="2" HorizontalAlign="Left">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
									<dx:ASPxTextBox ID="tbShippingTerms" runat="server" Width="250px" >
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Items" ColCount="6" ColSpan="6">
					<Items>
						<dx:LayoutItem ColSpan="6" ShowCaption="False">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer>
									<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsPurchaseOrderItems" 
										KeyFieldName="LineNumber" Width="100%" EnableRowsCache="true"
										OnDataBound="grid_DataBound"
										OnCommandButtonInitialize="grid_CommandButtonInitialize"
										OnCellEditorInitialize="grid_CellEditorInitialize"
										OnRowDeleting="grid_RowDeleting"
										OnRowValidating="grid_RowValidating">
										<Columns>
											<dx:GridViewCommandColumn Caption=" " Name="cmd" 
												ShowNewButtonInHeader="true" Width="80" ShowEditButton="true" ShowDeleteButton="true" />
											<dx:GridViewDataColumn FieldName="PurchaseOrderNum" Visible="False" /> 
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
											<dx:GridViewDataTextColumn FieldName="QtyOrdered" VisibleIndex="4" Width="100px">
												<PropertiesTextEdit ClientInstanceName="QtyOrdered" DisplayFormatString="#,#" Width="100%">
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
											<dx:GridViewDataTextColumn FieldName="AmtOrdered" Caption="Total" VisibleIndex="6" ReadOnly="true" Width="100px">
												<PropertiesTextEdit ClientInstanceName="AmtOrdered" DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
										</Columns>
										<SettingsPager PageSize="500" AlwaysShowPager="True" />
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
						<dx:LayoutItem FieldName="Subtotal" HorizontalAlign="Right">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
									<dx:ASPxSpinEdit ID="tbSubtotal" ClientInstanceName="tbSubtotal" runat="server" Width="100px" 
										ReadOnly="true" BackColor="WhiteSmoke"
										HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="#,0.00">
										<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
										<ClearButton Visibility="Auto" />
										<ClientSideEvents NumberChanged="OnHeaderValueChanged" />
									</dx:ASPxSpinEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="TaxRate" HorizontalAlign="Right">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
									<dx:ASPxSpinEdit ID="tbTaxRate" ClientInstanceName="tbTaxRate" runat="server" Width="100px" 
										HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="#,0.00">
										<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
										<ClearButton Visibility="Auto" />
										<ClientSideEvents NumberChanged="OnHeaderValueChanged" />
									</dx:ASPxSpinEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Tax" HorizontalAlign="Right">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
									<dx:ASPxSpinEdit ID="tbTax" ClientInstanceName="tbTax" runat="server" Width="100px" 
										ReadOnly="true" BackColor="WhiteSmoke"
										HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="#,0.00">
										<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
										<ClearButton Visibility="Auto" />
										<ClientSideEvents NumberChanged="OnHeaderValueChanged" />
									</dx:ASPxSpinEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="ShippingHandling" HorizontalAlign="Right">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
									<dx:ASPxSpinEdit ID="tbShippingHandling" ClientInstanceName="tbShippingHandling" runat="server" Width="100px" 
										HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="#,0.00">
										<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
										<ClearButton Visibility="Auto" />
										<ClientSideEvents NumberChanged="OnHeaderValueChanged" />
									</dx:ASPxSpinEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Other" HorizontalAlign="Right">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
									<dx:ASPxSpinEdit ID="tbOther" ClientInstanceName="tbOther" runat="server" Width="100px"
										HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="#,0.00">
										<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
										<ClearButton Visibility="Auto" />
										<ClientSideEvents NumberChanged="OnHeaderValueChanged" />
									</dx:ASPxSpinEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Total" HorizontalAlign="Right">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">
									<dx:ASPxSpinEdit ID="tbTotal" ClientInstanceName="tbTotal" runat="server" Width="100px" 
										ReadOnly="true" BackColor="WhiteSmoke"
										HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="#,0.00">
										<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
										<ClearButton Visibility="Auto" />
									</dx:ASPxSpinEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
			</Items>
		</dx:aspxformlayout>
			<dx:ASPxLabel runat="server" ID="errorMessageLabel" Visible="false" ForeColor="Red" EnableViewState="false" Font-Size="Medium" />
		</div>
		<br />
		<br />
		<dx:ASPxPopupControl ID="popNewItem" ClientInstanceName="pni" HeaderText="Create New Inventory Item" 
			Width="800px" Height="100px" EncodeHtml="false" 
			PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AllowResize="true" runat="server" 
			CloseAction="OuterMouseClick" AllowDragging="True" PopupElementID="imgButton">
			<ContentCollection>
				<dx:PopupControlContentControl>
					<dx:aspxformlayout ID="newItemPopup" runat="server" ColCount="4" DataSourceID="dsPurchaseOrderHeaders" 
						EnableTheming="True" Theme="Youthful" Width="100%">
						<Items>
							<dx:LayoutItem FieldName="InventoryActive">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer35" runat="server">
										<dx:ASPxCheckBox ID="tbInventoryActive" ClientInstanceName="puInventoryActive"
											runat="server" Checked="false">
											<ClientSideEvents ValueChanged="function (s, e) {}" />
										</dx:ASPxCheckBox>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
							<dx:EmptyLayoutItem ColSpan="2">
							</dx:EmptyLayoutItem>
							<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="150px">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer29" runat="server">
										<dx:ASPxButton ID="btnGenerateUPC" runat="server" VisibleIndex="1"
											Text="Generate UPC" Visible="True" Width="150px" UseSubmitBehavior="false" AutoPostBack="false">
											<ClientSideEvents Click="function(s, e) { GenerateUPC(s, e); }" />
										</dx:ASPxButton>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
							<dx:LayoutItem ColSpan="2" FieldName="ItemNumber">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server">
										<dx:ASPxTextBox ID="tbItemNumber" ClientInstanceName="puItemNumber"  VisibleIndex="2"
											runat="server" Width="100%">
											<ClientSideEvents ValueChanged="function (s, e) {}" />
										</dx:ASPxTextBox>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
							<dx:LayoutItem FieldName="UPC">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server">
										<dx:ASPxTextBox ID="tbUPC" ClientInstanceName="puUPC" VisibleIndex="11"
											runat="server" Width="100px">
											<ClientSideEvents ValueChanged="function (s, e) {}" />
										</dx:ASPxTextBox>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
							<dx:LayoutItem FieldName="LabelTypeID" Caption="Label Type">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server">
										<dx:ASPxComboBox ID="tbLabelTypeID" ClientInstanceName="puLabelTypeID" VisibleIndex="12"
											runat="server" 
											DataSourceID="dsLabelTypes" ValueType="System.Int32"
											DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
											TextField="LabelType" ValueField="LabelTypeID" Width="150px">
											<ClearButton Visibility="Auto">
											</ClearButton>
										</dx:ASPxComboBox>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
							<dx:LayoutItem FieldName="ItemDescription" ColSpan="2">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server">
										<dx:ASPxTextBox ID="tbItemDescription" ClientInstanceName="puItemDescription"  VisibleIndex="3"
											runat="server" Width="100%">
											<ClientSideEvents ValueChanged="function (s, e) {}" />
										</dx:ASPxTextBox>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
							<dx:LayoutItem FieldName="CurrentCost">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer30" runat="server">
										<dx:ASPxSpinEdit ID="tbCurrentCost" ClientInstanceName="puCurrentCost" VisibleIndex="13"
											runat="server" Width="100px" 
											HorizontalAlign="Right" DecimalPlaces="4" DisplayFormatString="#,0.0000">
											<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
											<ClearButton Visibility="Auto" />
											<ClientSideEvents ValueChanged="function (s, e) {}" />
										</dx:ASPxSpinEdit>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
								<CaptionSettings VerticalAlign="Middle" />
							</dx:LayoutItem>
							<dx:LayoutItem FieldName="LabelDescription">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer34" runat="server">
										<dx:ASPxTextBox ID="tbLabelDescription" ClientInstanceName="puLabelDescription" runat="server" Width="100%">
											<ClientSideEvents ValueChanged="function (s, e) {}" />
										</dx:ASPxTextBox>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
							<dx:LayoutItem FieldName="SubDepartmentID" Caption="Sub Department"  ColSpan="2">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer27" runat="server">
										<dx:ASPxComboBox ID="tbSubDepartmentID" ClientInstanceName="puSubDepartmentID" VisibleIndex="4"
											runat="server" 
											DataSourceID="dsSubDepartments" ValueType="System.Int32"
											DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
											TextField="SubDepartmentName" ValueField="SubDepartmentID" Width="100%">
											<ClearButton Visibility="Auto">
											</ClearButton>
										</dx:ASPxComboBox>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>						
							<dx:LayoutItem FieldName="StandardPrice" Caption="Eco Standard Price" ColSpan="2">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer32" runat="server">
										<dx:ASPxSpinEdit ID="tbStandardPrice" ClientInstanceName="puStandardPrice" VisibleIndex="14"
											runat="server" Width="100px" 
											HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="#,0.00">
											<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
											<ClearButton Visibility="Auto" />
											<ClientSideEvents ValueChanged="function (s, e) {}" />
										</dx:ASPxSpinEdit>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
								<CaptionSettings VerticalAlign="Middle" />
							</dx:LayoutItem>
							<dx:LayoutItem FieldName="QtyOrdered" ColSpan="2">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer31" runat="server">
										<dx:ASPxSpinEdit ID="tbQtyOrdered" ClientInstanceName="puQtyOrdered" VisibleIndex="5"
											runat="server" Width="100px" 
											HorizontalAlign="Right" DisplayFormatString="#,0">
											<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
											<ClearButton Visibility="Auto" />
										</dx:ASPxSpinEdit>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
								<CaptionSettings VerticalAlign="Middle" />
							</dx:LayoutItem>
							<dx:EmptyLayoutItem ColSpan="2">
							</dx:EmptyLayoutItem>
							<dx:EmptyLayoutItem ColSpan="2">
							</dx:EmptyLayoutItem>
							<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer28" runat="server">
										<dx:ASPxButton ID="newItemUpdateButton" runat="server" VisibleIndex="21" 
											Text="Update" Visible="True" Width="150px" UseSubmitBehavior="false" AutoPostBack="false">
											<ClientSideEvents Click="function(s, e) { NewItemUpdateButton_Click(s, e); }" />
										</dx:ASPxButton>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
							<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
								<LayoutItemNestedControlCollection>
									<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer33" runat="server">
										<dx:ASPxButton ID="newItemCancelButton" runat="server" VisibleIndex="22" 
											Text="Cancel" Visible="True" Width="150px" UseSubmitBehavior="false" AutoPostBack="false">
											<ClientSideEvents Click="function(s, e) { NewItemCancelButton_Click(s, e); }" />
										</dx:ASPxButton>
									</dx:LayoutItemNestedControlContainer>
								</LayoutItemNestedControlCollection>
							</dx:LayoutItem>
						</Items>
					</dx:aspxformlayout>
					<dx:ASPxLabel runat="server" ID="ASPxLabel1" Visible="false" ForeColor="Red" EnableViewState="false" Font-Size="Medium" />
				</dx:PopupControlContentControl>
			</ContentCollection>
		</dx:ASPxPopupControl>

        <asp:SqlDataSource ID="dsPurchaseOrderHeaders_SelectList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spPurchaseOrderHeaders_SelectList"
			SelectCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfOldestPurchaseOrderDate" Name="OldestPurchaseOrderDate" PropertyName="Value" Type="DateTime" />
				<asp:ControlParameter ControlID="hfShowCompletedPurchaseOrders" Name="ShowCompleted" PropertyName="Value" Type="Boolean" />
			</SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsPurchaseOrderHeaders" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spPurchaseOrderHeaders_Select"
			SelectCommandType="StoredProcedure"
            InsertCommand="NewMerch.spPurchaseOrderHeaders_Insert"
			InsertCommandType="StoredProcedure"
			OnInserted ="On_Inserted"
			UpdateCommand="NewMerch.spPurchaseOrderHeaders_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spPurchaseOrderHeaders_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
			</SelectParameters>
            <InsertParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" Direction="Output" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbVendorID" Name="VendorID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbStoreID" Name="StoreID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbPurchaseOrderDate" Name="PurchaseOrderDate" PropertyName="Value" Type="DateTime" />
				<asp:ControlParameter ControlID="flPO$tbVendorOrderNum" Name="VendorOrderNum" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbOrderedBy" Name="OrderedBy" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbShipVia" Name="ShipVia" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbFOB" Name="FOB" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbShippingTerms" Name="ShippingTerms" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbTaxRate" Name="TaxRate" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flPO$tbTax" Name="Tax" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flPO$tbShippingHandling" Name="ShippingHandling" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flPO$tbOther" Name="Other" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flPO$tbNotes" Name="Notes" PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbVendorID" Name="VendorID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbStoreID" Name="StoreID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbPurchaseOrderDate" Name="PurchaseOrderDate" PropertyName="Value" Type="DateTime" />
				<asp:ControlParameter ControlID="flPO$tbVendorOrderNum" Name="VendorOrderNum" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbOrderedBy" Name="OrderedBy" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbShipVia" Name="ShipVia" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbFOB" Name="FOB" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbShippingTerms" Name="ShippingTerms" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flPO$tbTaxRate" Name="TaxRate" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flPO$tbTax" Name="Tax" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flPO$tbShippingHandling" Name="ShippingHandling" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flPO$tbOther" Name="Other" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flPO$tbNotes" Name="Notes" PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </DeleteParameters>
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsVendors_SelectList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spVendors_SelectList2"
			SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsStores" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS StoreID, '<Select Store>' AS StoreName UNION SELECT StoreID, StoreName FROM Corporate.Stores ORDER BY StoreName">
        </asp:SqlDataSource>


		<asp:SqlDataSource ID="dsPurchaseOrderItems" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spPurchaseOrderItems_Select"
			SelectCommandType="StoredProcedure"
            InsertCommand="NewMerch.spPurchaseOrderItems_Insert"
			InsertCommandType="StoredProcedure"
            UpdateCommand="NewMerch.spPurchaseOrderItems_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spPurchaseOrderItems_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
			</SelectParameters>
			<InsertParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbStoreID" Name="StoreID" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="LineNumber" Direction="Output" Type="Int32" />
				<asp:Parameter Name="ItemNumber" Type="String" />
				<asp:Parameter Name="QtyOrdered" Type="Int32" />
				<asp:Parameter Name="UnitCost" Type="Decimal" />
				<asp:Parameter Name="Price" Type="Decimal" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</InsertParameters>
			<UpdateParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbStoreID" Name="StoreID" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="LineNumber" Type="Int32" />
				<asp:Parameter Name="ItemNumber" Type="String" />
				<asp:Parameter Name="QtyOrdered" Type="Int32" />
				<asp:Parameter Name="UnitCost" Type="Decimal" />
				<asp:Parameter Name="Price" Type="Decimal" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</UpdateParameters>
            <DeleteParameters>
				<asp:Parameter Name="hfPurchaseOrderNum" Type="Int32" />
				<asp:ControlParameter ControlID="flPO$tbStoreID" Name="StoreID" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="LineNumber" Type="Int32" />
				<asp:Parameter Name="ItemNumber" Type="String" />
				<asp:Parameter Name="QtyOrdered" Type="Int32" />
				<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </DeleteParameters>
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsInventoryItemsByVendor" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsLabelTypes" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS LabelTypeID, '<Select Label Type>' AS LabelType UNION SELECT LabelTypeID, LabelType FROM NewMerch.LabelTypes ORDER BY LabelType">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsSubDepartments" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS SubDepartmentID, '<Select SubDepartment>' AS SubDepartmentName UNION SELECT SD.SubDepartmentID, SD.SubDepartmentName FROM SysConfig.SubDepartments AS SD INNER JOIN SysConfig.SubDepartmentAssignments AS SDA ON SDA.SubDepartmentID = SD.SubDepartmentID WHERE SD.SubDepartmentName <> 'NEW ITEMS' AND RTRIM(LTRIM(SD.SubDepartmentName)) <> '' AND SDA.DepartmentID = 12 ORDER BY SubDepartmentName">
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
