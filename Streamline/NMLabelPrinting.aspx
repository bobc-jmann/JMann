<%@ Page Title="Label Printing" Language="VB" Theme="Youthful" AutoEventWireup="true" CodeFile="NMLabelPrinting.aspx.vb" Inherits="NMLabelPrinting" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
    	function OnItemNumberSelectedIndexChanged(s, e) {
    		var ItemNumber = s.GetValue();
    		var storeID = tbStoreID.GetValue();
    		PageMethods.GetItemInfo(ItemNumber, storeID, OnItemNumberSuccess, OnItemNumberError);
    	}

    	function OnItemNumberSuccess(result) {
    		var itemDescriptionEditor = grid.GetEditor('ItemDescription');
    		itemDescriptionEditor.SetText(result.Description);

    		var upcEditor = grid.GetEditor('UPC');
    		upcEditor.SetText(result.UPC);

    		var labelTypeEditor = grid.GetEditor('LabelTypeID');
    		labelTypeEditor.SetValue(result.LabelTypeID);

    		var priceEditor = grid.GetEditor('Price');
    		priceEditor.SetText(result.Price);

    		var qtyToPrint = grid.GetEditor('QtyToPrint');
    		qtyToPrint.SetText(1);
		}

    	function OnItemNumberError(result) {
    		alert('ItemNumber Error');
    	}
    	         
    	function OnUpcSelectedIndexChanged(s, e) {
    		var upc = s.GetValue();
    		var storeID = tbStoreID.GetValue();
    		PageMethods.GetUpcInfo(upc, storeID, OnUpcSuccess, OnUpcError);
    	}

    	function OnUpcSuccess(result) {
    		var itemNumberEditor = grid.GetEditor('ItemNumber');
    		itemNumberEditor.SetText(result.ItemNumber);

    		var itemDescriptionEditor = grid.GetEditor('ItemDescription');
    		itemDescriptionEditor.SetText(result.Description);

     		var labelTypeEditor = grid.GetEditor('LabelTypeID');
    		labelTypeEditor.SetValue(result.LabelTypeID);

    		var priceEditor = grid.GetEditor('Price');
    		priceEditor.SetText(result.Price);

    		var qtyToPrint = grid.GetEditor('QtyToPrint');
    		qtyToPrint.SetText(1);
		}

    	function OnUpcError(result) {
    		alert('ItemNumber Error');
    	}

    	function LaunchQZ_Click(s, e) {
    		launchQZ();
    	}

    	function StartConnection_Click(s, e) {
    		startConnection();
    	}

    	function EndConnection_Click(s, e) {
    		endConnection();
    	}

    	function CustomPrint(s, e) {
    		if(e.buttonID == 'cbPrint') {
    			grid.GetRowValues(e.visibleIndex, 'StoreID;ItemNumber;LabelTypeID;QtyToPrint;LabelPrintingID', printZPL);
    		}
    		if(e.buttonID == 'cbPrintAll') {
    			for (var i = 0; i < grid.GetVisibleRowsOnPage(); i++) {
    				grid.GetRowValues(i, 'StoreID;ItemNumber;LabelTypeID;QtyToPrint;LabelPrintingID', printZPL);
    			}
    		}
    	}


	</script>
</head>

<!-- QZ Required scripts -->
<script type="text/javascript" src="js/dependencies/sha-256.min.js"></script>
<script type="text/javascript" src="js/dependencies/rsvp-3.1.0.min.js"></script>
<script type="text/javascript" src="js/qz-tray.js"></script>
<script type="text/javascript" src="js/additional/jquery-1.11.3.min.js"></script>
<script type="text/javascript" src="js/additional/bootstrap.min.js"></script>

<body>
    <form id="Form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
		</asp:ScriptManager>
 		<dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
		<div style="position:relative;top:0;left:0;">
			<table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
				<tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
					<td>
						<dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" 
							Text="Label Printing" runat="server"></dx:ASPxLabel>
					</td>
				</tr>
			</table>
		</div>
		<br />
		<br />
		<br />

		<asp:UpdatePanel id="updatePanel1" runat="server">
		<ContentTemplate>
		<div>
		<dx:ASPxHiddenField ID="hf" runat="server" />
		<dx:ASPxFormLayout ID="flLP" runat="server" ColCount="6" 
			EnableTheming="True" Theme="Youthful" Width="100%">
			<Items>
				<dx:LayoutGroup Caption="Controls" ShowCaption="False" ColCount="8" ColSpan="6" Name="Controls">
					<Items>
						<dx:EmptyLayoutItem ColSpan="4">
						</dx:EmptyLayoutItem>
						<dx:LayoutItem Caption="Connection" HorizontalAlign="Center" Width="150px" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
									<dx:ASPxLabel runat="server" ID="qzStatus" ClientInstanceName="qzStatus" Font-Size="Medium" />
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
									<dx:ASPxButton ID="btnLaunchQZ" ClientInstanceName="btnLaunchQZ" runat="server" 
										Text="Launch QZ" Visible="True" Width="100px" UseSubmitBehavior="false" AutoPostBack="false">
											<ClientSideEvents Click="function(s, e) { LaunchQZ_Click(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="StoreID" Caption="Store" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
									<dx:ASPxComboBox ID="tbStoreID" runat="server" ClientInstanceName="tbStoreID"									
										DataSourceID="dsStores" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="StoreName" ValueField="StoreID" Width="150px" AutoPostBack="True"
										OnSelectedIndexChanged="tbStoreID_SelectedIndexChanged" SelectedIndex="0">
									<ClearButton Visibility="Auto" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="LabelTypeID" Caption="Label Type" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
									<dx:ASPxComboBox ID="tbLabelTypeID" ClientInstanceName="tbLabelTypeID" runat="server" 
										DataSourceID="dsLabelTypes" ValueType="System.Int32" AutoPostBack="true"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="LabelType" ValueField="LabelTypeID" Width="150px" SelectedIndex="0">
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer33" runat="server">
									<dx:ASPxButton ID="btnConnect" runat="server" 
										Text="Connect" Visible="True" Width="100px" UseSubmitBehavior="false" AutoPostBack="false">
											<ClientSideEvents Click="function(s, e) { StartConnection_Click(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
									<dx:ASPxButton ID="btnDisconnect" runat="server" 
										Text="Disconnect" Visible="True" Width="100px" UseSubmitBehavior="false" AutoPostBack="false">
											<ClientSideEvents Click="function(s, e) { EndConnection_Click(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="UPC" Caption="Create Label Entry for UPC" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
									<dx:ASPxTextBox ID="tbUPC" runat="server"										
										Width="150px" AutoPostBack="True"
										OnTextChanged="tbUPC_TextChanged">
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
			</Items>
		</dx:ASPxFormLayout>
		<dx:ASPxLabel runat="server" ID="errorMessageLabel" Visible="false" ForeColor="Red" EnableViewState="false" Font-Size="Medium" />
		</div>
		<br />
		<br />
		<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsLabelPrinting" 
			KeyFieldName="LabelPrintingID" Width="100%" EnableRowsCache="True" 			
			OnCommandButtonInitialize="grid_CommandButtonInitialize"
            OnCustomButtonInitialize="grid_CustomButtonInitialize"
			OnCellEditorInitialize="grid_CellEditorInitialize"			
			OnRowDeleting="grid_RowDeleting"
			OnRowValidating="grid_RowValidating">
			<ClientSideEvents CustomButtonClick="function(s, e) {
					CustomPrint(s, e);
				}" />			
			<Columns>
                <dx:GridViewCommandColumn Width="110" ShowNewButtonInHeader="true" 
					ShowEditButton="true" ShowDeleteButton="true" Caption=" " ShowClearFilterButton="true">
                   <CustomButtons>
						<dx:GridViewCommandColumnCustomButton ID="cbPrint" Text="Print" Visibility="AllDataRows"/>
						<dx:GridViewCommandColumnCustomButton ID="cbPrintAll" Text="Print All" Visibility="AllDataRows"/>
					</CustomButtons>
 				</dx:GridViewCommandColumn>
                <dx:GridViewDataColumn FieldName="LabelPrintingID" Visible="False" /> 
                <dx:GridViewDataColumn FieldName="StoreID" Visible="False" /> 
                <dx:GridViewDataColumn FieldName="PurchaseOrderNum" Caption="PO" ReadOnly="true">
					<HeaderStyle HorizontalAlign="Right" />
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataColumn> 
                <dx:GridViewDataColumn FieldName="ReceiptNum" Caption="Receipt" ReadOnly="true"> 
					<HeaderStyle HorizontalAlign="Right" />
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataColumn> 
                <dx:GridViewDataColumn FieldName="LineNumber" Caption="Line" ReadOnly="true">
					<HeaderStyle HorizontalAlign="Right" />
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataColumn> 
                <dx:GridViewDataComboBoxColumn FieldName="ItemNumber" Caption="Item Number" ReadOnly="true">
					<PropertiesComboBox Width="100%"
						ValueType="System.String" ValueField="Item" TextField="ItemNumber" 
						EnableCallbackMode="true" CallbackPageSize="10"
						OnItemsRequestedByFilterCondition="ItemNumber_ItemsRequestedByFilterCondition"
						OnItemRequestedByValue="ItemNumber_ItemRequestedByValue"
						IncrementalFilteringMode="StartsWith" >
						<ClearButton Visibility="Auto" />
						<ClientSideEvents SelectedIndexChanged="OnItemNumberSelectedIndexChanged" />
					</PropertiesComboBox>
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataComboBoxColumn> 
                <dx:GridViewDataTextColumn FieldName="ItemDescription" Caption="Printing Description" ReadOnly="true">
					<PropertiesTextEdit ClientInstanceName="ItemDescription" />
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataTextColumn> 
                <dx:GridViewDataComboBoxColumn FieldName="UPC" Caption="UPC" ReadOnly="true">
					<PropertiesComboBox Width="100%"
						ValueType="System.String" ValueField="Item" TextField="UPC" 
						EnableCallbackMode="true" CallbackPageSize="10"
						OnItemsRequestedByFilterCondition="Upc_ItemRequestedByFilterCondition"
						OnItemRequestedByValue="Upc_ItemRequestedByValue"
						IncrementalFilteringMode="StartsWith" >
						<ClearButton Visibility="Auto" />
						<ClientSideEvents SelectedIndexChanged="OnUpcSelectedIndexChanged" />
					</PropertiesComboBox>
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataComboBoxColumn> 
                <dx:GridViewDataComboBoxColumn FieldName="LabelTypeID" Caption="Label Type" ReadOnly="true">
					<PropertiesComboBox DataSourceID="dsLabelTypes" Width="100%" ClientInstanceName="LabelTypeID" 
						ValueType="System.String" ValueField="LabelTypeID" TextField="LabelType" 
						IncrementalFilteringMode="StartsWith" >
						<ClearButton Visibility="Auto" />
					</PropertiesComboBox>
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataComboBoxColumn> 
                <dx:GridViewDataTextColumn FieldName="Price" ReadOnly="true">
					<PropertiesTextEdit ClientInstanceName="Price" DisplayFormatString="#,0.00" />
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataTextColumn> 
                <dx:GridViewDataTextColumn FieldName="QtyToPrint">
                    <PropertiesTextEdit ClientInstanceName="QtyToPrint" DisplayFormatString="#,0" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataCheckColumn FieldName="Printed" ReadOnly="true">
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
 				</dx:GridViewDataCheckColumn>
			</Columns>
			<SettingsPager PageSize="200" AlwaysShowPager="False" />
            <SettingsEditing Mode="Inline" />
			<Settings ShowFilterRow="True" ShowGroupPanel="False" ShowFooter="True" />
		</dx:ASPxGridView>
		</ContentTemplate>
		</asp:UpdatePanel>

        <asp:SqlDataSource ID="dsLabelPrinting" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spLabelPrinting_Select"
			SelectCommandType="StoredProcedure"
            InsertCommand="NewMerch.spLabelPrinting_Insert"
			InsertCommandType="StoredProcedure"
            UpdateCommand="NewMerch.spLabelPrinting_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spLabelPrinting_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="flLP$tbStoreID" Name="StoreID" PropertyName="Value" Type="Int32" />
			</SelectParameters>
            <InsertParameters>
				<asp:ControlParameter ControlID="flLP$tbStoreID" Name="StoreID" PropertyName="Value" Type="Int32" />
				<asp:Parameter Name="PurchaseOrderNum" Type="Int32" />
				<asp:Parameter Name="ReceiptNum" Type="Int32" />
				<asp:Parameter Name="LineNumber" Type="Int32" />
				<asp:Parameter Name="ItemNumber" Type="String" />
				<asp:Parameter Name="LabelTypeID" Type="Int32" />
				<asp:Parameter Name="QtyToPrint" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
				<asp:Parameter Name="LabelPrintingID" Type="Int32" />
				<asp:Parameter Name="StoreID" Type="Int32" />
				<asp:Parameter Name="PurchaseOrderNum" Type="Int32" />
				<asp:Parameter Name="ReceiptNum" Type="Int32" />
				<asp:Parameter Name="LineNumber" Type="Int32" />
				<asp:Parameter Name="ItemNumber" Type="String" />
				<asp:Parameter Name="LabelTypeID" Type="Int32" />
				<asp:Parameter Name="QtyToPrint" Type="Int32" />
				<asp:Parameter Name="Printed" Type="Boolean" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
				<asp:Parameter Name="LabelPrintingID" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </DeleteParameters>
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsStores" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS StoreID, '<Select Store>' AS StoreName UNION SELECT StoreID, StoreName FROM Corporate.Stores ORDER BY StoreName">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsInventoryItemsByVendor" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>">
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsLabelTypes" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS LabelTypeID, '<Select Label Type>' AS LabelType UNION SELECT LabelTypeID, LabelType FROM NewMerch.LabelTypes ORDER BY LabelType">
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


		<script type="text/javascript">
			/*******************************************************
			*
			*						QZ Print
			*
			********************************************************/
			qz.security.setCertificatePromise(PageMethods.GetCertificate);

			qz.security.setSignaturePromise(function (toSign) {
				return function (resolve, reject) {
					PageMethods.SignMessage(toSign, resolve, reject);
				};
			});

			/// Connection ///
			function launchQZ() {
				if (!qz.websocket.isActive()) {
					window.location.assign("qz:launch");
				}
			}

			function startConnection() {
				if (!qz.websocket.isActive()) {
					updateState('Waiting', 'default');

					qz.websocket.connect().then(function () {
						updateState('Active', 'success');
						//findVersion();
					}).catch(function() {
						return qz.websocket.connect({ host: 'localhost.qz.io' });
					}).then(function () {
						updateState('Active', 'success');
					}).catch(handleConnectionError);
				} else {
					displayMessage('An active connection with QZ already exists.', 'alert-warning');
				}
			}

			function endConnection() {
				if (qz.websocket.isActive()) {
					qz.websocket.disconnect().then(function () {
						updateState('Inactive', 'default');
					}).catch(handleConnectionError);
				} else {
					displayMessage('No active connection with QZ exists.', 'alert-warning');
				}
			}

			function printZPL(values) {
				if (tbLabelTypeID.GetSelectedItem().value == values[2]) {
					var config = qz.configs.create("ZDesigner GK420t");

					PageMethods.PrintLabels(values[0], values[1], values[2], values[3], function (data) {
						qz.print(config, [data]).then(printingSuccess(values)).catch(printingError);
					}, printingError);
				}
			}

			function printTest(values) {
				if (tbLabelTypeID.GetSelectedItem().value == values[2]) {
					alert(values[1] + " " + values[2]);
				}
			}

			function printingSuccess(values) {
				PageMethods.UpdateToPrinted(values[4], hf.Get("UserID"), updateToPrintedSuccess, updateToPrintedError);
			}
	
			function printingError(err) {
				alert("Printing Error " + err);
			}

			function updateToPrintedSuccess(err) {
				if (err != "") {
					alert("UpdateToPrinted Error" + err);
				} else {
					grid.Refresh();
				}
			}

			function updateToPrintedError(err) {
				alert("UpdateToPrinted Error " + err);
			}

			/// Page load ///
			$(document).ready(function () {
				startConnection();
			});

			qz.websocket.setClosedCallbacks(function (evt) {
				updateState('Inactive', 'default');
				console.log(evt);
			});

			qz.websocket.setErrorCallbacks(handleConnectionError);

			/// Helpers ///
			function handleConnectionError(err) {
				updateState('Error', 'danger');

				if (err.target != undefined) {
					if (err.target.readyState >= 2) { //if CLOSING or CLOSED
						//displayError("Connection to QZ Tray was closed");
						console.log("Connection to QZ Tray was closed");
					} else {
						//displayError("A connection error occurred, check log for details");
						console.error(err);
					}
				} else {
					//displayError(err);
					console.error(err);
				}
			}

			function updateState(text, css) {
				qzStatus.SetText(text);
				$("#qz-connection").removeClass().addClass('panel panel-' + css);

				if (text === "Inactive" || text === "Error") {
					btnLaunchQZ.SetVisible(true);
				} else {
					btnLaunchQZ.SetVisible(false);
				}
			}

		</script>
    </form>
</body>
</html>
