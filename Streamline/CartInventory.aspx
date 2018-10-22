<%@ Page Title="Cart Inventory" Language="VB" Theme="Youthful" AutoEventWireup="false" CodeFile="CartInventory.aspx.vb" Inherits="CartInventory" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
		.DevExButton {
			font-family: Tahoma;
			font-size: 8pt;
			color: #3C3A49;
			background-color: #ECEDF0;
			height: 25px;
			border-style: solid;
			border-color: #A9ACB5;
		}
	</style>
    <title></title>
    <script>
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
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
    <div style="position:relative;top:0;left:0">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Cart Inventory" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
	<br />
	<div>
		<table>
			<tr>
				<td style="width: 20%">Cart Location:
					<asp:DropDownList ID="ddlLocations" runat="server" ToolTip="Inventory Location" Width="100%" AutoPostBack="True" />
 				</td>
   	            <td style="width:2%" />
   	            <td colspan="4" style="font-size:medium"><span style="color:darkblue">Ending Inventory</span> = 
					   <span style="color:darkblue">Beginning Inventory</span> + 
					   <span style="color:green">Carts Added</span> - 
					   <span style="color:purple">Carts Removed</span>
				</td>
 			</tr>
			<tr>
 				<td style="width: 20%">
					<asp:Label ID="lblInventoryDate" runat="server" Text="Inventory Date:"></asp:Label>
					<dx:ASPxDateEdit ID="dtInventoryDate" ClientInstanceName="dtInventoryDate"
						Width="100%" ToolTip="Inventory Date" EditFormat="Custom" 
						EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
						ValidationSettings-RegularExpression-ErrorText="Invalid date" 
						ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
						runat="server" AutoPostBack="True">
						<ValidationSettings SetFocusOnError="True">
							<ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
							<RegularExpression ErrorText="Invalid date"></RegularExpression>
						</ValidationSettings>
					</dx:ASPxDateEdit>
				</td>
   	            <td style="width:2%" />
   	            <td style="width:18%" />
                <td style="width:20%">
					<asp:Button ID="cmdCartInventory" runat="server" Text="Cart Inventory Report" width="250px"/>
    	        </td>
                <td style="width:20%">
					<asp:Button ID="cmdInterRegionTransfers" runat="server" Text="Inter-Region Transfers Report" width="250px"/>
    	        </td>
   	            <td style="width:20%" />
			</tr>
		</table>
	</div>
	<br />
 	<asp:UpdatePanel ID="upMain" runat="server">
 	<ContentTemplate>
		<div id="divSearch" class="specials" runat="server" >

			<asp:Label ID="lblInventory" runat="server" Text="Inventory:" Font-Size="14"></asp:Label>
			<table id="tblInventory" runat="server" style="visibility: visible">
				<tr>
					<td style="width: 100%">
						<dx:ASPxGridView KeyFieldName="InventoryID" ID="gridMain" EnableRowsCache="true"
							ClientInstanceName="gridMain" runat="server"
							DataSourceID="dsGridMain" Width="80%"
							OnHtmlDataCellPrepared="gridMain_OnHtmlDataCellPrepared"
							OnCommandButtonInitialize="gridMain_CommandButtonInitialize"
							OnCustomButtonInitialize="gridMain_CustomButtonInitialize"
							OnCellEditorInitialize="gridMain_CellEditorInitialize"
							EnableCallBacks="False" >
							<Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" Width="80" Caption=" " CellStyle-HorizontalAlign="Left" ShowNewButtonInHeader="true" ShowDeleteButton="true" ShowEditButton="true">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="cbApprove" Text="Approve" Visibility="AllDataRows"/>
                                        <dx:GridViewCommandColumnCustomButton ID="cbUnapprove" Text="Unapprove" Visibility="AllDataRows"/>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
								<dx:GridViewDataColumn FieldName="InventoryID" Visible="false" /> 
								<dx:GridViewDataColumn FieldName="LocationID" Visible="False" /> 
								<dx:GridViewDataColumn FieldName="InventoryDate" Visible="False" /> 
								<dx:GridViewDataColumn FieldName="TransferID" Visible="False" /> 
								<dx:GridViewDataColumn FieldName="Approved" Visible="False" /> 
								<dx:GridViewDataComboBoxColumn FieldName="LineTypeID" Visible="False" EditFormSettings-Visible="True" Caption="Line Type" HeaderStyle-HorizontalAlign="Left">
									<PropertiesComboBox DataSourceID="dsLineType" TextField="LineTypeDesc" ValueField="LineTypeID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
									</PropertiesComboBox>
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
								</dx:GridViewDataComboBoxColumn>
								<dx:GridViewDataColumn FieldName="LineTypeDesc" Caption=" " Width="175" VisibleIndex="1" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
		                        <dx:GridViewDataTextColumn FieldName="SoftCarts" Caption="Soft" VisibleIndex="2" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="True">
		                            <PropertiesTextEdit DisplayFormatString="#,##0.00" />
								</dx:GridViewDataTextColumn>
		                        <dx:GridViewDataTextColumn FieldName="HardCarts" Caption="Hard" VisibleIndex="3" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="True">
		                            <PropertiesTextEdit DisplayFormatString="#,##0.00" />
								</dx:GridViewDataTextColumn>
		                        <dx:GridViewDataTextColumn FieldName="TotalCarts" Caption="Total" VisibleIndex="4" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False">
		                            <PropertiesTextEdit DisplayFormatString="#,##0.00" />
								</dx:GridViewDataTextColumn>
		                        <dx:GridViewDataTextColumn FieldName="EmptyCarts" Caption="Empty" VisibleIndex="5" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="True">
		                            <PropertiesTextEdit DisplayFormatString="#,##0.00" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataComboBoxColumn FieldName="SecondaryLocationID" Width="75" VisibleIndex="6" Caption="Location" HeaderStyle-HorizontalAlign="Left">
									<PropertiesComboBox DataSourceID="dsSecondaryLocation" TextField="LocationAbbr" ValueField="LocationID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
									</PropertiesComboBox>
								</dx:GridViewDataComboBoxColumn>
  								<dx:GridViewDataComboBoxColumn FieldName="CharityID" VisibleIndex="7" Visible="false" EditFormSettings-Visible="False" Caption="Charity" HeaderStyle-HorizontalAlign="Left">
									<PropertiesComboBox DataSourceID="dsCharities" TextField="CharityAbbr" ValueField="CharityID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
									</PropertiesComboBox>
								</dx:GridViewDataComboBoxColumn>
  								<dx:GridViewDataComboBoxColumn FieldName="EditReasonID" VisibleIndex="8" Width="175" EditFormSettings-Visible="True" Caption="Edit Reason" HeaderStyle-HorizontalAlign="Left">
									<PropertiesComboBox DataSourceID="dsEditReasons" TextField="EditReason" ValueField="EditReasonID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
									</PropertiesComboBox>
								</dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataColumn FieldName="Notes" Width="200" VisibleIndex="9" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="True" EditFormSettings-ColumnSpan="2" />
                                <dx:GridViewDataColumn FieldName="LastEditedBy" VisibleIndex="10" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False"/>
							</Columns>
							<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" ConfirmDelete="True" />
							<SettingsBehavior ProcessSelectionChangedOnServer="True" />
							<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
							<SettingsPager PageSize="20" AlwaysShowPager="True" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="300" />
							<SettingsEditing EditFormColumnCount="4" />
                            <SettingsCommandButton>
                                <DeleteButton Text="Del"/>
                            </SettingsCommandButton>
						</dx:ASPxGridView>

						<asp:SqlDataSource ID="dsGridMain" runat="server" 
							ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
							SelectCommand="spCartInventory_Select"
							SelectCommandType="StoredProcedure"
							InsertCommand="spCartInventory_Insert"
							InsertCommandType="StoredProcedure"
							UpdateCommand="spCartInventory_Update"
							UpdateCommandType="StoredProcedure"
							DeleteCommand="spCartInventory_Delete"
							DeleteCommandType="StoredProcedure">
							<SelectParameters>
								<asp:ControlParameter name="LocationID" ControlID="ddlLocations" PropertyName="SelectedValue" Type="Int32" />
								<asp:ControlParameter name="InventoryDate" ControlID="dtInventoryDate" PropertyName="Value" Type="DateTime" />
							</SelectParameters>
							<InsertParameters>
								<asp:Parameter Name="InventoryID" Type="Int32" />
								<asp:ControlParameter name="LocationID" ControlID="ddlLocations" PropertyName="SelectedValue" Type="Int32" />
								<asp:ControlParameter name="InventoryDate" ControlID="dtInventoryDate" PropertyName="Value" Type="DateTime" />
								<asp:Parameter Name="LineTypeID" Type="Int32" />
								<asp:Parameter Name="SoftCarts" Type="Decimal" />
								<asp:Parameter Name="HardCarts" Type="Decimal" />
								<asp:Parameter Name="EmptyCarts" Type="Decimal" />
								<asp:Parameter Name="SecondaryLocationID" Type="Int32" />
								<asp:Parameter Name="EditReasonID" Type="Int32" />
								<asp:Parameter Name="Notes" Type="String" />
								<asp:Parameter Name="CharityID" Type="Int32" />
								<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
							</InsertParameters>
							<UpdateParameters>
								<asp:Parameter Name="InventoryID" Type="Int32" />
								<asp:ControlParameter name="LocationID" ControlID="ddlLocations" PropertyName="SelectedValue" Type="Int32" />
								<asp:ControlParameter name="InventoryDate" ControlID="dtInventoryDate" PropertyName="Value" Type="DateTime" />
								<asp:Parameter Name="LineTypeID" Type="Int32" />
								<asp:Parameter Name="SoftCarts" Type="Decimal" />
								<asp:Parameter Name="HardCarts" Type="Decimal" />
								<asp:Parameter Name="EmptyCarts" Type="Decimal" />
								<asp:Parameter Name="SecondaryLocationID" Type="Int32" />
								<asp:Parameter Name="EditReasonID" Type="Int32" />
								<asp:Parameter Name="Notes" Type="String" />
								<asp:Parameter Name="CharityID" Type="Int32" />
								<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
							</UpdateParameters>
							<DeleteParameters>
								<asp:Parameter Name="InventoryID" Type="Int32" />
								<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
							</DeleteParameters>
						</asp:SqlDataSource>
			
						<asp:SqlDataSource ID="dsLineType" runat="server"
							ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
							SelectCommand="SELECT LineTypeID, LinetypeDesc FROM Carts.LineTypes 
								WHERE LineTypeID IN (4, 5, 7, 8, 10, 13)
								ORDER BY SortCode">
						</asp:SqlDataSource>							

						<asp:SqlDataSource ID="dsSecondaryLocation" runat="server"
							ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
							SelectCommand="SELECT LocationID, LocationAbbr
								FROM tblLocations 
								WHERE InventoryLocation = 1 OR OutsideLocation = 1 
								UNION 
								SELECT 0 AS LocationID, '' AS LocationAbbr
								ORDER BY LocationAbbr">
						</asp:SqlDataSource>							

						<asp:SqlDataSource ID="dsCharities" runat="server"
							ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
							SelectCommand="SELECT CharityID, CharityAbbr 
								FROM tblCharities 
								UNION
								SELECT 0 AS CharityID, '' AS CharityAbbr
								ORDER BY CharityAbbr">
						</asp:SqlDataSource>							

						<asp:SqlDataSource ID="dsEditReasons" runat="server"
							ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
							SelectCommand="SELECT EditReasonID, EditReason, SortCode
								FROM Carts.EditReasons
								UNION
								SELECT 0 AS EditReasonID, '' AS EditReason, '0' AS SortCode
								ORDER BY SortCode">
						</asp:SqlDataSource>							
					</td>
				</tr>
			</table>
		</div> 
		<br />
		<div>
			<asp:Label ID="lblTransfers" runat="server" Text="Transfers in Process:" Font-Size="12"></asp:Label>
			<table id="Table1" runat="server" style="visibility: visible">
				<tr>
					<td style="width: 100%">
						<dx:ASPxGridView KeyFieldName="InventoryID" ID="gridTransfers" EnableRowsCache="true"
							ClientInstanceName="gridTransfers" runat="server"
							DataSourceID="dsGridTransfers" Width="80%"
							OnCustomButtonInitialize="gridTransfers_CustomButtonInitialize"
							EnableCallBacks="false">
							<Columns>
								<dx:GridViewCommandColumn VisibleIndex="0" Width="80" Caption=" " 
										CellStyle-HorizontalAlign="Left">
									<CustomButtons>
										<dx:GridViewCommandColumnCustomButton ID="cbReceive" Text="Receive" Visibility="AllDataRows" />
									</CustomButtons>
								</dx:GridViewCommandColumn>
								<dx:GridViewDataColumn FieldName="InventoryID" Visible="false" /> 
								<dx:GridViewDataColumn FieldName="LocationID" Visible="false" /> 
								<dx:GridViewDataColumn FieldName="LocationAbbr" Caption="Shipping Location " Width="175" VisibleIndex="1" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" />
								<dx:GridViewDataColumn FieldName="InventoryDate" Caption="Date Shipped" VisibleIndex="2" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" /> 
								<dx:GridViewDataColumn FieldName="LineTypeDesc" Visible="false" />
		                        <dx:GridViewDataTextColumn FieldName="SoftCarts" Caption="Soft" VisibleIndex="3" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right">
		                            <PropertiesTextEdit DisplayFormatString="#,##0.00" />
								</dx:GridViewDataTextColumn>
		                        <dx:GridViewDataTextColumn FieldName="HardCarts" Caption="Hard" VisibleIndex="4" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right">
		                            <PropertiesTextEdit DisplayFormatString="#,##0.00" />
								</dx:GridViewDataTextColumn>
		                        <dx:GridViewDataTextColumn FieldName="TotalCarts" Caption="Total" VisibleIndex="5" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right">
		                            <PropertiesTextEdit DisplayFormatString="#,##0.00" />
								</dx:GridViewDataTextColumn>
		                        <dx:GridViewDataTextColumn FieldName="EmptyCarts" Caption="Empty" VisibleIndex="6" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right">
		                            <PropertiesTextEdit DisplayFormatString="#,##0.00" />
								</dx:GridViewDataTextColumn>
  								<dx:GridViewDataComboBoxColumn FieldName="CharityID" VisibleIndex="7" Caption="Location" HeaderStyle-HorizontalAlign="Left">
									<PropertiesComboBox DataSourceID="dsCharities" TextField="CharityAbbr" ValueField="CharityID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
									</PropertiesComboBox>
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
								</dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataColumn FieldName="Notes" Width="300" VisibleIndex="8" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataColumn FieldName="LastEditedBy" VisibleIndex="9" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" />
							</Columns>
							<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" ConfirmDelete="True" />
							<SettingsBehavior ProcessSelectionChangedOnServer="True" />
							<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="false" />
							<SettingsPager PageSize="20" AlwaysShowPager="True" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="150" />
						</dx:ASPxGridView>

						<asp:SqlDataSource ID="dsGridTransfers" runat="server" 
							ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
							SelectCommand="spCartInventory_SelectTransfers"
							SelectCommandType="StoredProcedure">
							<SelectParameters>
								<asp:ControlParameter name="LocationID" ControlID="ddlLocations" PropertyName="SelectedValue" Type="Int32" />
							</SelectParameters>
						</asp:SqlDataSource>
					</td>
				</tr>
			</table>  
		</div>
	</ContentTemplate>
	</asp:UpdatePanel>
	</form>
</body>
</html>
