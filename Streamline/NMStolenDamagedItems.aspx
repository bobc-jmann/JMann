<%@ Page Title="Stolen or Damaged Items" Language="VB" Theme="Youthful" AutoEventWireup="true" CodeFile="NMStolenDamagedItems.aspx.vb" Inherits="NMStolenDamagedItems" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
     	function SearchByUPC_Click(s, e) {
    		var upc = flSD_tbSearchByUPC.GetValue();
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
    		__doPostBack();
    	}

    	function OnSearchByUpcError() {
    		alert('SearchByUpc Error');
    	}

    	function OnStoreIDChanged(s, e) {
    		var storeID = document.getElementById("hfStoreID");
    		storeID.value = tbStoreID.GetValue();
    	}

      </script>
</head>

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
							Text="Stolen or Damaged Items" runat="server"></dx:ASPxLabel>
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
		<asp:HiddenField ID="hfStoreID" runat="server" />
		<asp:HiddenField ID="hfVendorID" runat="server" />
		<asp:HiddenField ID="hfShowInactiveInventoryItems" runat="server" />
		<dx:ASPxFormLayout ID="flSD" runat="server" ColCount="4" 
			EnableTheming="True" Theme="Youthful" Width="100%">
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
										UseSubmitBehavior="false" AutoPostBack="true" SelectedIndex="-1"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										OnSelectedIndexChanged="navigateVendorBox_SelectedIndexChanged"
										TextField="VendorName" ValueField="VendorID"  Width="100%">
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup ShowCaption="False" ColCount="2" ColSpan="2">
					<Items>
						<dx:LayoutItem FieldName="StoreID" Caption="Store" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
									<dx:ASPxComboBox ID="tbStoreID" ClientInstanceName="tbStoreID" runat="server"										
										DataSourceID="dsStores" ValueType="System.Int32"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="StoreName" ValueField="StoreID" Width="200px" AutoPostBack="True">
										<ClearButton Visibility="Auto" />
										<ClientSideEvents ValueChanged="function(s, e) { OnStoreIDChanged(s, e); }" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Reason" Caption="Reason">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
									<dx:ASPxComboBox ID="tbReason" ClientInstanceName="tbReason" runat="server"										
										DataSourceID="dsReasons" ValueType="System.String"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										TextField="Reason" ValueField="Reason" Width="200px" AutoPostBack="True">
										<ClearButton Visibility="Auto" />
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem Caption="Date/Time">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
									<dx:ASPxDateEdit ID="tbDate" runat="server" Width="200px"
										EditFormat="DateTime" EditFormatString="MM/dd/yyyy hh:mm tt">
										<TimeSectionProperties Visible="true">
											<TimeEditProperties EditFormatString="hh:mm tt">
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
						<dx:LayoutItem FieldName="Qty" Caption="Quantity">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
									<dx:ASPxSpinEdit ID="tbQty" ClientInstanceName="tbQty" runat="server" Width="200px" 
										HorizontalAlign="Left" DisplayFormatString="#,#">
										<SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
										<ClearButton Visibility="Auto" />
									</dx:ASPxSpinEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
							<CaptionSettings VerticalAlign="Middle" />
						</dx:LayoutItem>
						<dx:LayoutItem HorizontalAlign="Center" ShowCaption="False" Width="150px">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
									<dx:ASPxButton ID="updateButton" runat="server" OnClick="updateButton_Click" 
										Text="Update" Visible="True" Width="150px" UseSubmitBehavior="false">
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Notes" ColCount="2" ColSpan="2" Width="300px">
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
		<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsStolenDamaged" 
			KeyFieldName="StolenDamagedID" Width="100%" EnableRowsCache="true" SettingsBehavior-ConfirmDelete="True"
			OnCommandButtonInitialize="grid_CommandButtonInitialize"
			OnRowDeleting="grid_RowDeleting"
			OnRowValidating="grid_RowValidating">
			<Columns>
                <dx:GridViewCommandColumn Width="80" ShowEditButton="true" ShowDeleteButton="true" Caption=" " />
                <dx:GridViewDataColumn FieldName="StolenDamagedID" Visible="False" /> 
                <dx:GridViewDataColumn FieldName="StoreID" Visible="False" /> 
                <dx:GridViewDataColumn FieldName="StoreName" Caption="Store" ReadOnly="true">
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataColumn> 
                <dx:GridViewDataColumn FieldName="ItemNumber" ReadOnly="true"> 
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataColumn> 
                <dx:GridViewDataColumn FieldName="ItemDescription" ReadOnly="true">
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataColumn> 
                <dx:GridViewDataDateColumn FieldName="Date" Caption="Date/Time" >
					<PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="MM/dd/yyyy hh:mm tt" EditFormatString="MM/dd/yyyy hh:mm tt">
						<TimeSectionProperties Visible="true">
							<TimeEditProperties EditFormatString="hh:mm tt">
								<ClearButton Visibility="Auto">
								</ClearButton>
							</TimeEditProperties>
						</TimeSectionProperties>
					</PropertiesDateEdit>
				</dx:GridViewDataDateColumn> 
                <dx:GridViewDataTextColumn FieldName="Qty" Caption="Actual">
                    <PropertiesTextEdit ClientInstanceName="Qty" DisplayFormatString="#,0" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
				</dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Reason" Caption="Reason" ReadOnly="true">
                    <PropertiesTextEdit ClientInstanceName="Reason" />
                    <HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
				</dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AvgCost" Caption="Cost" ReadOnly="true">
                    <PropertiesTextEdit ClientInstanceName="AvgCost" DisplayFormatString="#,#.00" />
                    <HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataMemoColumn FieldName="Notes" /> 
				<dx:GridViewDataCheckColumn FieldName="Posted" ReadOnly="true">
					<EditCellStyle BackColor="WhiteSmoke" />
					<CellStyle BackColor="WhiteSmoke" />
 				</dx:GridViewDataCheckColumn>
			</Columns>
			<SettingsPager PageSize="200" AlwaysShowPager="False" />
            <SettingsEditing Mode="Inline" />
			<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
		</dx:ASPxGridView>
        <asp:SqlDataSource ID="dsInventoryItems_SelectList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsVendors_NavigateList" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spVendors_SelectList3"
			SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="dsStolenDamaged" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="NewMerch.spStolenDamaged_Select"
			SelectCommandType="StoredProcedure"
            UpdateCommand="NewMerch.spStolenDamaged_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spStolenDamaged_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfStoreID" Name="StoreID" PropertyName="Value" Type="Int32" />
			</SelectParameters>
            <UpdateParameters>
				<asp:Parameter Name="StolenDamagedID" Type="Int32" />
				<asp:Parameter Name="StoreID" Type="Int32" />
				<asp:Parameter Name="ItemNumber" Type="String" />
				<asp:Parameter Name="Date" Type="DateTime" />
				<asp:Parameter Name="Qty" Type="Int32" />
				<asp:Parameter Name="Reason" Type="String" />
				<asp:Parameter Name="Notes" Type="String" />
                <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
				<asp:Parameter Name="StolenDamagedID" Type="Int32" />
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
			<SelectParameters>
				<asp:ControlParameter ControlID="hfItemNumber" Name="ItemNumber" PropertyName="Value" Type="String" />
			</SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="dsReasons" runat="server"
            ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
            SelectCommand="SELECT '<Select Reason>' AS Reason
				UNION SELECT 'Damaged' AS Reason
 				UNION SELECT 'Expired' AS Reason 
				UNION SELECT 'Stolen' AS Reason">
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
