<%@ Page Title="Vendor Maintenance" Language="VB" Theme="Youthful" AutoEventWireup="true" CodeFile="NMInventoryMaint.aspx.vb" Inherits="NMInventoryMaint" %>

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
    			if (ctrlName == "flInventoryItems_navigateComboBox_DDD_L") continue;
    			if (ctrlName == "flInventoryItems_navigateComboBox") continue;
    			if (ctrlName == "flInventoryItems_navigateVendorBox_DDD_L") continue;
    			if (ctrlName == "flInventoryItems_navigateVendorBox") continue;
    			if (ctrlName == "flInventoryItems_ckShowInactiveInventoryItems") continue;
    			if (ctrlName == "flInventoryItems_tbSearchByUPC") continue;
    			if (ctrlName.substring(0, 4) == "grid") continue;

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
     		if (s.name == "flInventoryItems_updateButton" && !hf.Get("InsertMode")) {
    			var itemNumber = document.getElementById('hfItemNumber');
    			navigateComboBox.SetValue(itemNumber.value);
    		}
		}

    	function SearchByUPC_Click(s, e) {
    		var upc = flInventoryItems_tbSearchByUPC.GetValue();
   			PageMethods.SearchByUPC(upc, OnSearchByUpcSuccess, OnSearchByUpcError);
    	}

    	function SearchByUPC(s, e) {
    		if (e.htmlEvent.keyCode == 13) {
    			var upc = s.GetValue();
    			PageMethods.SearchByUPC(upc, OnSearchByUpcSuccess, OnSearchByUpcError);
    		}
    	}

    	function OnSearchByUpcSuccess(iv) {
    		navigateVendorBox.SetValue(iv.VendorID);
    		navigateComboBox.SetValue(iv.ItemNumber);
    		hf.Set('Search', true);
    		__doPostBack();
    	}

    	function OnSearchByUpcError() {
    		alert('SearchByUpc Error');
    	}

    	function GenerateUPC(s, e) {
    		PageMethods.GenerateUpc(hf.Get("UserID"), GenerateUpcSuccess, GenerateUpcError);
    	}

    	function GenerateUpcSuccess(upc) {
    		tbUPC.SetText(upc);
    		dirtyEditors["flInventoryItems_tbUPC"] = tbUPC.GetText();
    	}

    	function GenerateUpcError(result) {
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
							Text="Item Maintenance" runat="server"></dx:ASPxLabel>
					</td>
				</tr>
			</table>
		</div>
		<br />
		<br />
		<br />
		<div>
		<dx:ASPxHiddenField ID="hf" runat="server" />
		<asp:HiddenField ID="hfItemNumber" runat="server" />
		<asp:HiddenField ID="hfUPC" runat="server" />
		<asp:HiddenField ID="hfVendorID" runat="server" />
		<asp:HiddenField ID="hfShowInactiveInventoryItems" runat="server" />
		<dx:ASPxFormLayout ID="flInventoryItems" runat="server" ColCount="4" 
			DataSourceID="dsInventoryItems" EnableTheming="True" Theme="Youthful" Width="100%">
			<Items>
				<dx:LayoutGroup Caption="Controls" ShowCaption="False" ColCount="4" ColSpan="4" Name="Controls">
					<Items>
						<dx:LayoutItem ColSpan="2" Caption="Search by UPC Code" Name="SearchByUPC">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
									<dx:ASPxTextBox ID="tbSearchByUPC" runat="server" Width="100%">
										<ClientSideEvents KeyPress="function(s, e) { SearchByUPC(s, e); }" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px" Name="Search">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
									<dx:ASPxButton ID="btnSearch" runat="server" 
										Text="Search" Width="150px" UseSubmitBehavior="false">
										<ClientSideEvents Click="function(s, e) { SearchByUPC_Click(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem Caption="Show Inactive Inventory Items">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
									<dx:ASPxCheckBox ID="ckShowInactiveInventoryItems" runat="server" Checked="false" AutoPostBack="true">
									</dx:ASPxCheckBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem Caption="Select Item" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
									<dx:ASPxComboBox ID="navigateComboBox" ClientInstanceName="navigateComboBox" runat="server"
										ValueType="System.String" ValueField="ItemNumber" 
										EnableCallbackMode="true" CallbackPageSize="10"
										OnItemsRequestedByFilterCondition="navigateComboBox_ItemsRequestedByFilterCondition"
										OnItemRequestedByValue="navigateComboBox_ItemRequestedByValue"
										UseSubmitBehavior="false" AutoPostBack="true" SelectedIndex="-1"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										OnSelectedIndexChanged="navigateComboBox_SelectedIndexChanged"
										TextFormatString="{0}" Width="100%">
										<Columns>
											<dx:ListBoxColumn FieldName="ItemNumber" Caption="Item Number" Width="150px" />
											<dx:ListBoxColumn FieldName="ItemDescription" Caption="Item Description" Width="200px" />
										</Columns>
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem Caption="Select Vendor" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
									<dx:ASPxComboBox ID="navigateVendorBox" ClientInstanceName="navigateVendorBox" runat="server"
										DataSourceID="dsVendors_NavigateList" ValueType="System.String" 
										UseSubmitBehavior="false" AutoPostBack="true" SelectedIndex="0"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										OnSelectedIndexChanged="navigateVendorBox_SelectedIndexChanged"
										TextField="VendorName" ValueField="VendorID"  Width="100%">
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup ShowCaption="false" ColCount="5" ColSpan="4">
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
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
									<dx:ASPxButton ID="updateButton" runat="server" OnClick="updateButton_Click" 
										Text="Update" Visible="False" Width="150px" UseSubmitBehavior="false">
										<ClientSideEvents Click="function(s, e) { ClearDirtyEditors(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
									<dx:ASPxButton ID="cancelButton" runat="server" OnClick="cancelButton_Click" 
										Text="Cancel" Visible="False" Width="150px" UseSubmitBehavior="false">
										<ClientSideEvents Click="function(s, e) { ClearDirtyEditors(s, e); }" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
									<dx:ASPxButton ID="deleteButton" runat="server" OnClick="deleteButton_Click" 
										Text="Delete" Visible="False" Width="150px" UseSubmitBehavior="false">
										<ClientSideEvents Click="ConfirmDelete" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:EmptyLayoutItem ColSpan="2">
				</dx:EmptyLayoutItem>
				<dx:LayoutItem FieldName="Active">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
							<dx:ASPxCheckBox ID="tbActive" runat="server" Checked="false">
								<ClientSideEvents ValueChanged="function (s, e) {}" />
							</dx:ASPxCheckBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="150px">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
							<dx:ASPxButton ID="btnGenerateUPC" runat="server" 
								Text="Generate UPC" Visible="False" Width="150px" UseSubmitBehavior="false" AutoPostBack="false">
								<ClientSideEvents Click="function(s, e) { GenerateUPC(s, e); }" />
							</dx:ASPxButton>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutItem ColSpan="2" FieldName="ItemNumber">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer runat="server">
							<dx:ASPxTextBox ID="tbItemNumber" runat="server" Width="100%">
								<ClientSideEvents ValueChanged="function (s, e) {}" />
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutItem FieldName="UPC">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer runat="server">
							<dx:ASPxTextBox ID="tbUPC" ClientInstanceName="tbUPC" runat="server" Width="100px">
								<ClientSideEvents ValueChanged="function (s, e) {}" />
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutItem FieldName="LabelTypeID" Caption="Label Type">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
							<dx:ASPxComboBox ID="tbLabelTypeID" runat="server" 
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
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
							<dx:ASPxTextBox ID="tbItemDescription" runat="server" Width="100%">
								<ClientSideEvents ValueChanged="function (s, e) {}" />
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutItem FieldName="CurrentCost">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
							<dx:ASPxSpinEdit ID="tbCurrentCost" ClientInstanceName="tbCurrentCost" runat="server" Width="100px" 
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
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
							<dx:ASPxTextBox ID="tbLabelDescription" runat="server" Width="100%">
								<ClientSideEvents ValueChanged="function (s, e) {}" />
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutItem FieldName="VendorID" Caption="Vendor" ColSpan="2">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer runat="server">
							<dx:ASPxComboBox ID="tbVendorID" runat="server" 
								DataSourceID="dsVendors_SelectList" ValueType="System.Int32"
								DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
								TextField="VendorName" ValueField="VendorID" Width="100%">
								<ClearButton Visibility="Auto">
								</ClearButton>
							</dx:ASPxComboBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutItem FieldName="StandardPrice" Caption="Eco Standard Price" ColSpan="2">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
							<dx:ASPxSpinEdit ID="tbStandardPrice" ClientInstanceName="tbStandardPrice" runat="server" Width="100px" 
								HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="#,0.00">
								<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
								<ClearButton Visibility="Auto" />
								<ClientSideEvents ValueChanged="function (s, e) {}" />
							</dx:ASPxSpinEdit>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
					<CaptionSettings VerticalAlign="Middle" />
				</dx:LayoutItem>
				<dx:LayoutGroup Caption="Groups" ColSpan="2">
					<Items>
						<dx:LayoutItem FieldName="CategoryID" Caption="Category">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxComboBox ID="tbCategoryID" runat="server" 
										DataSourceID="dsCategories" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="CategoryName" ValueField="CategoryID" Width="100%">
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="DepartmentID" Caption="Department">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxComboBox ID="tbDepartmentID" runat="server" 
										DataSourceID="dsDepartments" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="DepartmentName" ValueField="DepartmentID" Width="100%">
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="SubDepartmentID" Caption="Sub Department">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxComboBox ID="tbSubDepartmentID" runat="server" 
										DataSourceID="dsSubDepartments" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="SubDepartmentName" ValueField="SubDepartmentID" Width="100%">
										<ClearButton Visibility="Auto">
										</ClearButton>
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Notes" ColSpan="2">
					<Items>
						<dx:LayoutItem FieldName="Notes" ShowCaption="False">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxMemo ID="tbNotes" runat="server" Height="71px" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxMemo>
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
        <asp:SqlDataSource ID="dsInventoryItems_SelectList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsVendors_NavigateList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spVendors_SelectList3"
			SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsInventoryItems" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spInventoryItems_Select"
			SelectCommandType="StoredProcedure"
            InsertCommand="NewMerch.spInventoryItems_Insert"
			InsertCommandType="StoredProcedure"
			OnInserted ="On_Inserted"
            UpdateCommand="NewMerch.spInventoryItems_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spInventoryItems_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfItemNumber" Name="ItemNumber" PropertyName="Value" Type="String" />
			</SelectParameters>
            <InsertParameters>
				<asp:ControlParameter ControlID="flInventoryItems$tbItemNumber" Name="ItemNumber" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbItemDescription" Name="ItemDescription" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbLabelDescription" Name="LabelDescription" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbUPC" Name="UPC" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbLabelTypeID" Name="LabelTypeID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbVendorID" Name="VendorID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbCurrentCost" Name="CurrentCost" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flInventoryItems$tbStandardPrice" Name="StandardPrice" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flInventoryItems$tbCategoryID" Name="CategoryID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbDepartmentID" Name="DepartmentID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbSubDepartmentID" Name="SubDepartmentID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbActive" Name="Active" PropertyName="Checked" Type="Boolean" />
				<asp:ControlParameter ControlID="flInventoryItems$tbNotes" Name="Notes" PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
				<asp:ControlParameter ControlID="hfItemNumber" Name="OldItemNumber" PropertyName="Value" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbItemNumber" Name="ItemNumber" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbItemDescription" Name="ItemDescription" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbLabelDescription" Name="LabelDescription" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbUPC" Name="UPC" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flInventoryItems$tbLabelTypeID" Name="LabelTypeID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbVendorID" Name="VendorID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbCurrentCost" Name="CurrentCost" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flInventoryItems$tbStandardPrice" Name="StandardPrice" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flInventoryItems$tbCategoryID" Name="CategoryID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbDepartmentID" Name="DepartmentID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbSubDepartmentID" Name="SubDepartmentID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flInventoryItems$tbActive" Name="Active" PropertyName="Checked" Type="Boolean" />
				<asp:ControlParameter ControlID="flInventoryItems$tbNotes" Name="Notes" PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
				<asp:ControlParameter ControlID="hfItemNumber" Name="ItemNumber" PropertyName="Value" Type="String" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </DeleteParameters>
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsVendors_SelectList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spVendors_SelectList2"
			SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsLabelTypes" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS LabelTypeID, '<Select Label Type>' AS LabelType UNION SELECT LabelTypeID, LabelType FROM NewMerch.LabelTypes ORDER BY LabelType">
        </asp:SqlDataSource>
         <asp:SqlDataSource ID="dsCategories" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS CategoryID, '<Select Category>' AS CategoryName UNION SELECT CategoryID, CategoryName FROM SysConfig.Categories ORDER BY CategoryName">
        </asp:SqlDataSource>
       <asp:SqlDataSource ID="dsDepartments" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS DepartmentID, '<Select Department>' AS DepartmentName UNION SELECT DepartmentID, DepartmentName FROM SysConfig.Departments WHERE DepartmentActive = 1 ORDER BY DepartmentName">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsSubDepartments" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT 0 AS SubDepartmentID, '<Select SubDepartment>' AS SubDepartmentName UNION SELECT SD.SubDepartmentID, SD.SubDepartmentName FROM SysConfig.SubDepartments AS SD INNER JOIN SysConfig.SubDepartmentAssignments AS SDA ON SDA.SubDepartmentID = SD.SubDepartmentID WHERE SD.SubDepartmentName <> 'NEW ITEMS' AND RTRIM(LTRIM(SD.SubDepartmentName)) <> '' AND SDA.DepartmentID = @departmentID ORDER BY SubDepartmentName">
			<SelectParameters>
				<asp:ControlParameter ControlID="flInventoryItems$tbDepartmentID" Name="DepartmentID" PropertyName="Value" Type="Int32" />
			</SelectParameters>
        </asp:SqlDataSource>

		<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsInventoryByLocation" 
			KeyFieldName="StoreID" Width="100%" EnableRowsCache="true" SettingsBehavior-ConfirmDelete="True"
			OnCellEditorInitialize="grid_CellEditorInitialize"
			OnRowDeleting="grid_RowDeleting"
			OnRowValidating="grid_RowValidating">
			<Columns>
                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" Width="80" ShowEditButton="true" ShowDeleteButton="true"/>
                <dx:GridViewDataColumn FieldName="ItemNumber" Visible="False" /> 
				<dx:GridViewDataComboBoxColumn FieldName="StoreID" Caption="Store">
					<PropertiesComboBox DataSourceID="dsStores" ValueType="System.Int32" ValueField="StoreID" TextField="StoreName" 
						IncrementalFilteringMode="StartsWith" />
				</dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="Price" Caption="Eco-Price" VisibleIndex="3">
                    <PropertiesTextEdit ClientInstanceName="Price" DisplayFormatString="#,#.00" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="QtyBreak1" VisibleIndex="4">
                    <PropertiesTextEdit ClientInstanceName="QtyBreak1" DisplayFormatString="#,#" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PriceBreak1" VisibleIndex="5">
                    <PropertiesTextEdit ClientInstanceName="PriceBreak1" DisplayFormatString="#,#.00" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="QtyBreak2" VisibleIndex="6">
                    <PropertiesTextEdit ClientInstanceName="QtyBreak2" DisplayFormatString="#,#" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PriceBreak2" VisibleIndex="7">
                    <PropertiesTextEdit ClientInstanceName="PriceBreak2" DisplayFormatString="#,#.00" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AvgCost" VisibleIndex="8" ReadOnly="true" CellStyle-BackColor="WhiteSmoke">
                    <PropertiesTextEdit ClientInstanceName="AvgCost" DisplayFormatString="#,0.0000" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="OnHand" VisibleIndex="9" ReadOnly="true" CellStyle-BackColor="WhiteSmoke">
                    <PropertiesTextEdit ClientInstanceName="OnHand" DisplayFormatString="#,0" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="OnOrder" VisibleIndex="10" ReadOnly="true" CellStyle-BackColor="WhiteSmoke">
                    <PropertiesTextEdit ClientInstanceName="OnOrder" DisplayFormatString="#,0" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ReorderPoint" VisibleIndex="11">
                    <PropertiesTextEdit ClientInstanceName="ReorderPoint" DisplayFormatString="#,#" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>
			</Columns>
			<TotalSummary>
				<dx:ASPxSummaryItem FieldName="OnHand" ShowInGroupFooterColumn="OnHand" SummaryType="Sum" DisplayFormat="#,0" />
				<dx:ASPxSummaryItem FieldName="OnOrder" ShowInGroupFooterColumn="OnOrder" SummaryType="Sum" DisplayFormat="#,0" />
			</TotalSummary>
			<SettingsPager PageSize="200" AlwaysShowPager="False" />
            <SettingsEditing Mode="Inline" />
			<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
		</dx:ASPxGridView>

		<asp:SqlDataSource ID="dsInventoryByLocation" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spInventoryByLocation_Select"
			SelectCommandType="StoredProcedure"
            InsertCommand="NewMerch.spInventoryByLocation_Insert"
			InsertCommandType="StoredProcedure"
            UpdateCommand="NewMerch.spInventoryByLocation_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spInventoryByLocation_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfItemNumber" Name="ItemNumber" PropertyName="Value" Type="String" />
			</SelectParameters>
			<InsertParameters>
				<asp:ControlParameter ControlID="hfItemNumber" Name="ItemNumber" PropertyName="Value" Type="String" />
				<asp:Parameter Name="StoreID" Type="Int32" />
				<asp:Parameter Name="Price" Type="Decimal" />
				<asp:Parameter Name="QtyBreak1" Type="Int32" />
				<asp:Parameter Name="PriceBreak1" Type="Decimal" />
				<asp:Parameter Name="QtyBreak2" Type="Int32" />
				<asp:Parameter Name="PriceBreak2" Type="Decimal" />
				<asp:Parameter Name="ReorderPoint" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</InsertParameters>
			<UpdateParameters>
				<asp:ControlParameter ControlID="hfItemNumber" Name="ItemNumber" PropertyName="Value" Type="String" />
				<asp:Parameter Name="StoreID" Type="Int32" />
				<asp:Parameter Name="Price" Type="Decimal" />
				<asp:Parameter Name="QtyBreak1" Type="Int32" />
				<asp:Parameter Name="PriceBreak1" Type="Decimal" />
				<asp:Parameter Name="QtyBreak2" Type="Int32" />
				<asp:Parameter Name="PriceBreak2" Type="Decimal" />
				<asp:Parameter Name="ReorderPoint" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</UpdateParameters>
            <DeleteParameters>
				<asp:ControlParameter ControlID="hfItemNumber" Name="ItemNumber" PropertyName="Value" Type="String" />
				<asp:Parameter Name="StoreID" Type="Int32" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
           </DeleteParameters>
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dsStores" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT StoreID, StoreName FROM Corporate.Stores ORDER BY StoreName">
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
