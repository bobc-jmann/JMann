<%@ Page Title="Vendor Maintenance" Language="VB" Theme="Youthful" AutoEventWireup="true" CodeFile="NMVendorMaint.aspx.vb" Inherits="NMVendorMaint" %>

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
    			if (ctrlName == "flVendors_navigateComboBox_DDD_L") continue;
    			if (ctrlName == "flVendors_navigateComboBox") continue;

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
    		if (s.name == "flVendors_updateButton" && !hf.Get("InsertMode")) {
    			var vendorID = document.getElementById('hfVendorID');
    			navigateComboBox.SetValue(vendorID.value);
    		}
		}
    </script>
</head>

<body>
    <form id="Form1" runat="server">
		<dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
        </dx:ASPxGlobalEvents>
		<div style="position:relative;top:0;left:0;">
			<table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
				<tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
					<td>
						<dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" 
							Text="Vendor Maintenance" runat="server"></dx:ASPxLabel>
					</td>
				</tr>
			</table>
		</div>
		<br />
		<br />
		<br />
		<dx:ASPxHiddenField ID="hf" runat="server" />
		<asp:HiddenField ID="hfVendorID" runat="server" />
		<dx:ASPxFormLayout ID="flVendors" runat="server" ColCount="4" DataSourceID="dsVendors" 
			EnableTheming="True" Theme="Youthful" Width="100%"
			OnBeforeUnload="">
			<Items>
				<dx:LayoutGroup Caption="Controls" ShowCaption="False" ColCount="4" ColSpan="4" Name="Controls">
					<Items>
						<dx:LayoutItem Caption="Select Vendor" ColSpan="2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
									<dx:ASPxComboBox ID="navigateComboBox" ClientInstanceName="navigateComboBox" runat="server" 
										DataSourceID="dsVendors_SelectList" ValueType="System.Int32" 
										UseSubmitBehavior="false" AutoPostBack="true" SelectedIndex="0"
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
										OnSelectedIndexChanged="navigateComboBox_SelectedIndexChanged"
										TextField="VendorName" ValueField="VendorID" Width="300px">
									</dx:ASPxComboBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem ColSpan="2">
						</dx:EmptyLayoutItem>
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
										<ClientSideEvents Click="ConfirmDelete" />
									</dx:ASPxButton>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:EmptyLayoutItem>
						</dx:EmptyLayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutItem FieldName="VendorName" ColSpan="2">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer runat="server">
							<dx:ASPxTextBox ID="tbVendorName" runat="server" Width="100%">
								<ClientSideEvents ValueChanged="function (s, e) {}" />
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutItem FieldName="AccountNumber" ColSpan="2">
					<LayoutItemNestedControlCollection>
						<dx:LayoutItemNestedControlContainer runat="server">
							<dx:ASPxTextBox ID="tbAccountNumber" runat="server" Width="100%">
								<ClientSideEvents ValueChanged="function (s, e) {}" />
							</dx:ASPxTextBox>
						</dx:LayoutItemNestedControlContainer>
					</LayoutItemNestedControlCollection>
				</dx:LayoutItem>
				<dx:LayoutGroup Caption="Address Info" ColCount="3" ColSpan="2">
					<Items>
						<dx:LayoutItem ColSpan="3" FieldName="AddressLine1" Caption="Line1">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbAddressLine1" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem ColSpan="3" FieldName="AddressLine2" Caption="Line2">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbAddressLine2" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="City" Caption="C/S/Z">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbCity" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="State" ShowCaption="False">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbState" runat="server" Width="30px">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Zip" ShowCaption="False">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbZip" runat="server" Width="80px">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Contact Info" ColSpan="2">
					<Items>
						<dx:LayoutItem FieldName="Contact">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbContact" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Phone">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbPhone" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="AltPhone">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbAltPhone" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Fax">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbFax" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Email">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbEmail" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Other Info" ColSpan="2">
					<Items>
						<dx:LayoutItem FieldName="Terms">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxTextBox ID="tbTerms" runat="server" Width="100%">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxTextBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="TaxRate">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer runat="server">
									<dx:ASPxSpinEdit ID="tbTaxRate" runat="server" Number="0" Width="50px">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxSpinEdit>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Backorders">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
									<dx:ASPxCheckBox ID="tbBackorders" runat="server" Checked="false">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxCheckBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
						<dx:LayoutItem FieldName="Active">
							<LayoutItemNestedControlCollection>
								<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
									<dx:ASPxCheckBox ID="tbActive" runat="server" Checked="true">
										<ClientSideEvents ValueChanged="function (s, e) {}" />
									</dx:ASPxCheckBox>
								</dx:LayoutItemNestedControlContainer>
							</LayoutItemNestedControlCollection>
						</dx:LayoutItem>
					</Items>
				</dx:LayoutGroup>
				<dx:LayoutGroup Caption="Notes" ColSpan="2">
					<Items>
						<dx:LayoutItem Caption="" FieldName="Notes" Height="100px" ShowCaption="False" Width="100%">
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
		<br />
		<br />
		<asp:SqlDataSource ID="dsVendors_SelectList" runat="server"
			ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
			SelectCommand="NewMerch.spVendors_SelectList"
			SelectCommandType="StoredProcedure">
		</asp:SqlDataSource>

		<asp:SqlDataSource ID="dsVendors" runat="server"
			ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
			SelectCommand="NewMerch.spVendors_Select"
			SelectCommandType="StoredProcedure"
			InsertCommand="NewMerch.spVendors_Insert"
			InsertCommandType="StoredProcedure"
			OnInserted ="On_Inserted"
			UpdateCommand="NewMerch.spVendors_Update"
			UpdateCommandType="StoredProcedure"
			DeleteCommand="NewMerch.spVendors_Delete"
			DeleteCommandType="StoredProcedure">
			<SelectParameters>
				<asp:ControlParameter ControlID="hfVendorID" Name="VendorID" PropertyName="Value" Type="Int32" />
			</SelectParameters>
			<InsertParameters>
				<asp:ControlParameter ControlID="hfVendorID" Name="VendorID" Direction="Output" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flVendors$tbVendorName" Name="VendorName" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbAddressLine1" Name="AddressLine1" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbAddressLine2" Name="AddressLine2" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbCity" Name="City" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbState" Name="State" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbZip" Name="Zip" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbContact" Name="Contact" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbPhone" Name="Phone" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbAltPhone" Name="AltPhone" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbFax" Name="Fax" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbEmail" Name="Email" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbAccountNumber" Name="AccountNumber" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbTerms" Name="Terms" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbTaxRate" Name="TaxRate" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flVendors$tbBackorders" Name="Backorders" PropertyName="Checked" Type="Boolean" />
				<asp:ControlParameter ControlID="flVendors$tbActive" Name="Active" PropertyName="Checked" Type="Boolean" />
				<asp:ControlParameter ControlID="flVendors$tbNotes" Name="Notes" PropertyName="Text" Type="String" />
				<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</InsertParameters>
			<UpdateParameters>
				<asp:ControlParameter ControlID="hfVendorID" Name="VendorID" PropertyName="Value" Type="Int32" />
				<asp:ControlParameter ControlID="flVendors$tbVendorName" Name="VendorName" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbAddressLine1" Name="AddressLine1" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbAddressLine2" Name="AddressLine2" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbCity" Name="City" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbState" Name="State" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbZip" Name="Zip" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbContact" Name="Contact" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbPhone" Name="Phone" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbAltPhone" Name="AltPhone" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbFax" Name="Fax" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbEmail" Name="Email" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbAccountNumber" Name="AccountNumber" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbTerms" Name="Terms" PropertyName="Text" Type="String" />
				<asp:ControlParameter ControlID="flVendors$tbTaxRate" Name="TaxRate" PropertyName="Value" Type="Decimal" />
				<asp:ControlParameter ControlID="flVendors$tbBackorders" Name="Backorders" PropertyName="Checked" Type="Boolean" />
				<asp:ControlParameter ControlID="flVendors$tbActive" Name="Active" PropertyName="Checked" Type="Boolean" />
				<asp:ControlParameter ControlID="flVendors$tbNotes" Name="Notes" PropertyName="Text" Type="String" />
				<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</UpdateParameters>
			<DeleteParameters>
				<asp:ControlParameter ControlID="hfVendorID" Name="VendorID" PropertyName="Value" Type="Int32" />
				<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
			</DeleteParameters>
		</asp:SqlDataSource>

		<script type="text/javascript">
			var __oldDoPostBack = __doPostBack;
			__doPostBack = CatchExplorerError;

			function CatchExplorerError(eventTarget, eventArgument) {
				try {
					return __oldDoPostBack(eventTarget, eventArgument);
				}
				catch (ex) {
					// don't want to mask a genuine error
					// lets just restrict this to our 'Unspecified' one
					if (ex.message.indexOf('Unspecified') == -1) {
						throw ex;
					}
				}
			}
		</script>
	</form>
</body>
</html>
