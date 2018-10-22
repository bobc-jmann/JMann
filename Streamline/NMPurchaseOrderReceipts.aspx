<%@ Page Title="Purchase Order Receipts" Language="VB" Theme="Youthful" AutoEventWireup="true" CodeFile="NMPurchaseOrderReceipts.aspx.vb" Inherits="NMPurchaseOrderReceipts" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    		if (s.name == "flPO_newButton")
    			navigateComboBox.SetValue(null);
    		// If the user changes the selection and elects to stay on the selected page because they have edited a field,
    		//  the event causing the change will occur on postback before the update. So change it back.
    		if (s.name == "flPO_updateButton") {
    			var purchaseOrderNum = document.getElementById('hfPurchaseOrderNum');
    			navigateComboBox.SetValue(purchaseOrderNum.value);
    		}
		}

     	function InsertNewReceipt() {
    		var PurchaseOrderNum;
    		if (navigateComboBox === undefined) {
    			return;
    		}
    		PurchaseOrderNum = navigateComboBox.GetValue();
    		var currentUserID = hf.Get("CurrentUserID");
    		PageMethods.InsertNewReceiptRow(PurchaseOrderNum, currentUserID, OnInsertNewReceiptSuccess, OnInsertNewReceiptError);
    	}

     	function OnInsertNewReceiptSuccess(missingPricesCnt) {
     		if (missingPricesCnt > 0) {
     			var message = "This Purchase Order cannot be received because "
     			if (missingPricesCnt == 1) {
     				message += "1 item is missing a price."
     			} else {
     				message += missingPricesCnt + " items are missing prices."
     			}
     			alert(message);
     			return;
     		}
     		if (missingPricesCnt == -1) {
     			alert("There are no items to receive");
     			return;
     		}
     		tbStatus.SetText("Received");
     		grid.Refresh();
    	}

    	function OnInsertNewReceiptError() {
    		alert('InsertNewReceipt Error');
    	}


     	function OnReceiptValueChanged(s, e) {
    		if (e.command == "STARTEDIT" || e.command == "ADDNEWROW" || e.command == "REFRESH" ||
				e.command == "SHOWDETAILROW" || e.command == "HIDEDETAILROW" || e.command == "DELETEROW")
    			return;
    		var subtotalEditor = grid.GetEditor('Subtotal');
    		var taxRateEditor = grid.GetEditor('TaxRate');
    		var taxEditor = grid.GetEditor('Tax');
    		var shippingHandlingEditor = grid.GetEditor('ShippingHandling');
    		var otherEditor = grid.GetEditor('Other');
    		var totalEditor = grid.GetEditor('Total');
    		taxEditor.SetText((Number(taxRateEditor.GetText()) * Number(subtotalEditor.GetText()) / 100).toFixed(2));
    		totalEditor.SetText(Number(subtotalEditor.GetText()) + Number(taxEditor.GetText()) +
				Number(shippingHandlingEditor.GetText()) + Number(otherEditor.GetText()));
    	}
    	function OnReceiptItemValueChanged(s, e) {
    		if (e.command == "STARTEDIT" || e.command == "ADDNEWROW")
    			return;
    		var qtyOrderedEditor = gridDetail.GetEditor('QtyOrdered');
    		var qtyReceivedEditor = gridDetail.GetEditor('QtyReceived');
    		var qtyReturnedEditor = gridDetail.GetEditor('QtyReturned');
    		var qtyStockedEditor = gridDetail.GetEditor('QtyStocked');
    		var qtyBackorderedEditor = gridDetail.GetEditor('QtyBackordered');
    		var backorders = tbBackorders.GetValue();
    		qtyStockedEditor.SetText(qtyReceivedEditor.GetText() - qtyReturnedEditor.GetText());
    		if (backorders) {
    			qtyBackorderedEditor.SetText(qtyOrderedEditor.GetText() - qtyStockedEditor.GetText());
    		}
    		var unitCostEditor = gridDetail.GetEditor('UnitCost');
    		var amtReceivedEditor = gridDetail.GetEditor('AmtReceived');
    		var amtReceivedPrev = amtReceivedEditor.GetText();
    		if (isNaN(amtReceivedPrev))
    			amtReceivedPrev = 0;
    		amtReceivedEditor.SetText((qtyReceivedEditor.GetText() * unitCostEditor.GetText()).toFixed(2));
    		var amtReceivedPost = amtReceivedEditor.GetText();
    		if (isNaN(amtReceivedPost))
    			amtReceivedPost = 0;
    	}

    	function EndCallback(s, e) {
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
							Text="Purchase Order Receipts" runat="server"></dx:ASPxLabel>
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
				<dx:LayoutGroup ShowCaption="False" ColCount="5" ColSpan="6" Name="Controls">
					<Items>
						<dx:LayoutItem Name="navigateComboBox" Caption="Select Purchase Order" HorizontalAlign="Center">
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
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
									<dx:ASPxButton ID="updateButton" runat="server" OnClick="updateButton_Click" Text="Update" Visible="False" Width="150px">
										<ClientSideEvents Click="function(s, e) { ClearDirtyEditors(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
									<dx:ASPxButton ID="cancelButton" runat="server" OnClick="cancelButton_Click" Text="Cancel" Visible="False" Width="150px">
										<ClientSideEvents Click="function(s, e) { ClearDirtyEditors(s, e); }" />
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
									<dx:ASPxDateEdit ID="tbPurchaseOrderDate" runat="server"  Width="100px"
										ReadOnly="true"  BackColor="WhiteSmoke">
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
						<dx:LayoutItem FieldName="Backorders" ColSpan="2" ShowCaption="True">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
									<dx:ASPxCheckBox ID="tbBackorders" ClientInstanceName="tbBackorders" 
										runat="server" Width="100px" Style="display: none">
									</dx:ASPxCheckBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:EmptyLayoutItem ColSpan="2">
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
									<dx:ASPxComboBox ID="tbVendorID" runat="server" 
										DataSourceID="dsVendors_SelectList" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="VendorName" ValueField="VendorID" Width="200px"
										ReadOnly="true"  BackColor="WhiteSmoke">
										<ClearButton Visibility="Auto" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="StoreID" Caption="Ship To" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
									<dx:ASPxComboBox ID="tbStoreID" runat="server"
										DataSourceID="dsStores" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="StoreName" ValueField="StoreID" Width="120px"
										ReadOnly="true"  BackColor="WhiteSmoke">
										<ClearButton Visibility="Auto" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Status">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
									<dx:ASPxTextBox ID="tbStatus" ClientInstanceName="tbStatus" runat="server" Checked="false" 
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
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
									<dx:ASPxTextBox ID="tbVendorOrderNum" runat="server" Width="150px"
										ReadOnly="true"  BackColor="WhiteSmoke">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="OrderedBy">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
									<dx:ASPxTextBox ID="tbOrderedBy" runat="server" Width="150px"
										ReadOnly="true"  BackColor="WhiteSmoke">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="ShipVia">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
									<dx:ASPxTextBox ID="tbShipVia" runat="server" Width="150px"
										ReadOnly="true"  BackColor="WhiteSmoke">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="FOB">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
									<dx:ASPxTextBox ID="tbFOB" runat="server" Width="150px"
										ReadOnly="true"  BackColor="WhiteSmoke">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings Location="Top" />
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="ShippingTerms" ColSpan="2" HorizontalAlign="Left">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
									<dx:ASPxTextBox ID="tbShippingTerms" runat="server" Width="250px"
										ReadOnly="true"  BackColor="WhiteSmoke">
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
									<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsPurchaseOrderReceipts" 
										KeyFieldName="ReceiptNum" Width="100%" EnableRowsCache="true" SettingsBehavior-ConfirmDelete="True"
										OnRowDeleting="grid_RowDeleting"
										OnRowValidating="grid_RowValidating">
										<SettingsBehavior ConfirmDelete="True" />
										<Columns>
											<dx:GridViewCommandColumn Width="80" ShowEditButton="true" ShowDeleteButton="true">
												<HeaderCaptionTemplate>
													<dx:ASPxHyperLink ID="btnNew" runat="server" Text="New">
														<ClientSideEvents Click="function (s, e) { InsertNewReceipt();}" />
													</dx:ASPxHyperLink>
												</HeaderCaptionTemplate>
											</dx:GridViewCommandColumn>
											<dx:GridViewDataColumn FieldName="PurchaseOrderNum" Visible="False" /> 
											<dx:GridViewDataTextColumn FieldName="ReceiptNum" VisibleIndex="1" ReadOnly="true" Width="60px">
												<PropertiesTextEdit ClientInstanceName="ReceiptNum" DisplayFormatString="#" Width="100%" />
												<HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataDateColumn FieldName="ReceiptDate" VisibleIndex="2" Width="80px" /> 
											<dx:GridViewDataColumn FieldName="InvoiceNum" VisibleIndex="3" Width="60" /> 
											<dx:GridViewDataDateColumn FieldName="InvoiceDate" VisibleIndex="4" Width="80px" /> 
											<dx:GridViewDataTextColumn FieldName="InvoiceAmt" VisibleIndex="5" Width="100px">
												<PropertiesTextEdit ClientInstanceName="InvoiceAmt" DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="Subtotal" VisibleIndex="6" ReadOnly="true" Width="100px">
												<PropertiesTextEdit ClientInstanceName="Subtotal" DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="TaxRate" VisibleIndex="7" Width="100px">
												<PropertiesTextEdit ClientInstanceName="TaxRate" DisplayFormatString="#,0.00" Width="100%">
													<ClientSideEvents ValueChanged="OnReceiptValueChanged" />
												</PropertiesTextEdit>
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="Tax" VisibleIndex="8" ReadOnly="true" Width="100px">
												<PropertiesTextEdit ClientInstanceName="Tax" DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="ShippingHandling" VisibleIndex="9" Width="100px">
												<PropertiesTextEdit ClientInstanceName="ShippingHandling" DisplayFormatString="#,0.00" Width="100%">
													<ClientSideEvents ValueChanged="OnReceiptValueChanged" />
												</PropertiesTextEdit>
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="Other" VisibleIndex="10" Width="100px">
												<PropertiesTextEdit ClientInstanceName="Other" DisplayFormatString="#,0.00" Width="100%">
													<ClientSideEvents ValueChanged="OnReceiptValueChanged" />
												</PropertiesTextEdit>
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="Total" VisibleIndex="11" ReadOnly="true" Width="100px">
												<PropertiesTextEdit ClientInstanceName="Total" DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
										</Columns>
										<TotalSummary>
											<dx:ASPxSummaryItem FieldName="Subtotal" ShowInGroupFooterColumn="Subtotal" SummaryType="Sum" DisplayFormat="#,0.00" />
											<dx:ASPxSummaryItem FieldName="Tax" ShowInGroupFooterColumn="Tax" SummaryType="Sum" DisplayFormat="#,0.00" />
											<dx:ASPxSummaryItem FieldName="ShippingHandling" ShowInGroupFooterColumn="ShippingHandling" SummaryType="Sum" DisplayFormat="#,0.00" />
											<dx:ASPxSummaryItem FieldName="Other" ShowInGroupFooterColumn="Other" SummaryType="Sum" DisplayFormat="#,0.00" />
											<dx:ASPxSummaryItem FieldName="Total" ShowInGroupFooterColumn="Total" SummaryType="Sum" DisplayFormat="#,0.00" />
										</TotalSummary>
										<SettingsPager PageSize="500" AlwaysShowPager="True" />
										<SettingsEditing Mode="Inline" />
										<ClientSideEvents BeginCallback="OnReceiptValueChanged" />
										<SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
										<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
										<Templates>
											<DetailRow>
												<dx:ASPxGridView ID="gridDetail" ClientInstanceName="gridDetail" 
													KeyFieldName="ReceiptNum;LineNumber" runat="server" 
													DataSourceID="dsPurchaseOrderReceiptItems" Width="100%" 
													OnBeforePerformDataSelect="grid_DataSelect">
													<Columns>
														<dx:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowEditButton="true"/>
														<dx:GridViewDataColumn FieldName="PurchaseOrderNum" Visible="False" /> 
														<dx:GridViewDataColumn FieldName="ReceiptNum" Visible="False" /> 
														<dx:GridViewDataColumn FieldName="LineNumber" Visible="False" /> 
														<dx:GridViewDataTextColumn FieldName="ItemNumber" VisibleIndex="1" Width="100px">
															<PropertiesTextEdit ClientInstanceName="ItemNumber" Width="100%">
															</PropertiesTextEdit>
															<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="Price" Caption="Eco-Price" VisibleIndex="2" Width="100px">
															<PropertiesTextEdit ClientInstanceName="Price" DisplayFormatString="#,0.00" Width="100%">
															</PropertiesTextEdit>
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="QtyOrdered" VisibleIndex="3" ReadOnly="true" Width="100px">
															<PropertiesTextEdit ClientInstanceName="QtyOrdered" DisplayFormatString="#,0" Width="100%" />
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="QtyReceived" VisibleIndex="4" Width="100px">
															<PropertiesTextEdit ClientInstanceName="QtyReceived" DisplayFormatString="#,0" Width="100%">
																<ClientSideEvents ValueChanged="OnReceiptItemValueChanged" />
															</PropertiesTextEdit>
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="QtyReturned" VisibleIndex="5" Width="100px">
															<PropertiesTextEdit ClientInstanceName="QtyReturned" DisplayFormatString="#,0" Width="100%">
																<ClientSideEvents ValueChanged="OnReceiptItemValueChanged" />
															</PropertiesTextEdit>
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="QtyStocked" VisibleIndex="6" ReadOnly="true" Width="100px">
															<PropertiesTextEdit ClientInstanceName="Total" DisplayFormatString="#,0" Width="100%" />
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="QtyBackordered" VisibleIndex="7" Width="100px">
															<PropertiesTextEdit ClientInstanceName="QtyBackordered" DisplayFormatString="#,0" Width="100%">
																<ClientSideEvents ValueChanged="OnReceiptItemValueChanged" />
															</PropertiesTextEdit>
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="UnitCost" VisibleIndex="8" Width="100px">
															<PropertiesTextEdit ClientInstanceName="UnitCost" DisplayFormatString="#,0.0000" Width="100%">
																<ClientSideEvents ValueChanged="OnReceiptItemValueChanged" />
															</PropertiesTextEdit>
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
														</dx:GridViewDataTextColumn>
														<dx:GridViewDataTextColumn FieldName="AmtReceived" VisibleIndex="9" ReadOnly="true" Width="100px">
															<PropertiesTextEdit ClientInstanceName="AmtReceived" DisplayFormatString="#,0.00" Width="100%" />
															<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
															<EditCellStyle BackColor="WhiteSmoke" />
															<CellStyle BackColor="WhiteSmoke" />
														</dx:GridViewDataTextColumn>
													</Columns>  
													<SettingsPager PageSize="100" />
													<SettingsEditing Mode="Inline" />
													<ClientSideEvents EndCallback="EndCallback" />
											   </dx:ASPxGridView>
											</DetailRow>
										</Templates>
										<SettingsDetail ShowDetailRow="true" />
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
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" Direction="Output" PropertyName="Text" Type="Int32" />
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
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Text" Type="Int32" />
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
				<asp:ControlParameter ControlID="flPO$navigateComboBox" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </DeleteParameters>
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsVendors_SelectList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spVendors_SelectList"
			SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsStores" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS StoreID, '<Select Store>' AS StoreName UNION SELECT StoreID, StoreName FROM Corporate.Stores ORDER BY StoreName">
        </asp:SqlDataSource>


		<asp:SqlDataSource ID="dsPurchaseOrderReceipts" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spPurchaseOrderReceipts_Select"
			SelectCommandType="StoredProcedure"
            InsertCommand="NewMerch.spPurchaseOrderReceipts_Insert"
			InsertCommandType="StoredProcedure"
            UpdateCommand="NewMerch.spPurchaseOrderReceipts_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spPurchaseOrderReceipts_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
			</SelectParameters>
			<InsertParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="ReceiptNum" Direction="Output" Type="Int32" />
				<asp:Parameter Name="ReceiptDate" Type="DateTime" />
				<asp:Parameter Name="InvoiceNum" Type="String" />
				<asp:Parameter Name="InvoiceDate" Type="DateTime" />
				<asp:Parameter Name="InvoiceAmt" Type="Decimal" />
				<asp:Parameter Name="TaxRate" Type="Decimal" />
				<asp:Parameter Name="Tax" Type="Decimal" />
				<asp:Parameter Name="ShippingHandling" Type="Decimal" />
				<asp:Parameter Name="Other" Type="Decimal" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</InsertParameters>
			<UpdateParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="ReceiptNum" Type="Int32" />
				<asp:Parameter Name="ReceiptDate" Type="DateTime" />
				<asp:Parameter Name="InvoiceNum" Type="String" />
				<asp:Parameter Name="InvoiceDate" Type="DateTime" />
				<asp:Parameter Name="InvoiceAmt" Type="Decimal" />
				<asp:Parameter Name="TaxRate" Type="Decimal" />
				<asp:Parameter Name="Tax" Type="Decimal" />
				<asp:Parameter Name="ShippingHandling" Type="Decimal" />
				<asp:Parameter Name="Other" Type="Decimal" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</UpdateParameters>
            <DeleteParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="ReceiptNum" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </DeleteParameters>
		</asp:SqlDataSource>

		<asp:SqlDataSource ID="dsPurchaseOrderReceiptItems" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spPurchaseOrderReceiptItems_Select"
			SelectCommandType="StoredProcedure"
            UpdateCommand="NewMerch.spPurchaseOrderReceiptItems_Update"
			UpdateCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
                <asp:SessionParameter Name="ReceiptNum" SessionField="ReceiptNum" Type="Int32" />
 			</SelectParameters>
			<UpdateParameters>
				<asp:ControlParameter ControlID="hfPurchaseOrderNum" Name="PurchaseOrderNum" PropertyName="Value" Type="Int32" />
                <asp:SessionParameter Name="ReceiptNum" SessionField="ReceiptNum" Type="Int32" />
				<asp:Parameter Name="LineNumber" Type="Int32" />
				<asp:Parameter Name="Price" Type="Decimal" />
				<asp:Parameter Name="QtyReceived" Type="Decimal" />
				<asp:Parameter Name="QtyReturned" Type="Decimal" />
				<asp:Parameter Name="QtyBackordered" Type="Decimal" />
				<asp:Parameter Name="UnitCost" Type="Decimal" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</UpdateParameters>
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
