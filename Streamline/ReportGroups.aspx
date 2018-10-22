<%@ Page Title="Carts Worked" Language="VB" Theme="Youthful" AutoEventWireup="false" CodeFile="ReportGroups.aspx.vb" Inherits="ReportGroups" %>

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
            grdMain.SetHeight(height);
        }

        function OnEndCallbackDetail(s, e) {
        	grdMain.Refresh();
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Report Groups" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
 	<dx:ASPxHiddenField ID="hf" runat="server" />
	<dx:aspxformlayout ID="fl" runat="server" ColCount="2" EnableTheming="True" Theme="Youthful" Width="40%">
		<Items>
			<dx:LayoutGroup Caption="Controls" ShowCaption="False" ColCount="2" ColSpan="2" Name="Controls">
				<Items>
					<dx:LayoutItem FieldName="StoreID" Caption="Store" ColSpan="2">
						<LayoutItemNestedControlCollection>
							<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
								<dx:ASPxComboBox ID="tbStoreID" runat="server"										
									DataSourceID="dsStores" ValueType="System.Int32"
									DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
									TextField="StoreName" ValueField="StoreID" Width="150px" AutoPostBack="True"
									OnSelectedIndexChanged="tbStoreID_SelectedIndexChanged" SelectedIndex="0">
								<ClearButton Visibility="Auto" />
								</dx:ASPxComboBox>
							</dx:LayoutItemNestedControlContainer>
						</LayoutItemNestedControlCollection>
					</dx:LayoutItem>
				</Items>
			</dx:LayoutGroup>
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" DataSourceID="dsReportGroups" 
							KeyFieldName="ReportGroupID" Width="100%" EnableRowsCache="false" 
							SettingsBehavior-ConfirmDelete="True">
							<Columns>
								<dx:GridViewCommandColumn Width="80" ShowNewButtonInHeader="True" Caption=" " ShowDeleteButton="True" />
								<dx:GridViewDataColumn FieldName="ReportGroupID" Visible="false" />
								<dx:GridViewDataComboBoxColumn FieldName="DepartmentID" Caption="Department" Width="120px">
									<PropertiesComboBox Width="100%"
										ValueType="System.String" ValueField="DepartmentID" TextField="DepartmentName" DataSourceID="dsDepartments" 
										EnableCallbackMode="true" CallbackPageSize="20"
										IncrementalFilteringMode="StartsWith" >
										<ClearButton Visibility="Auto" />
									</PropertiesComboBox>
								</dx:GridViewDataComboBoxColumn>
								<dx:GridViewDataColumn FieldName="Description" /> 
								<dx:GridViewDataTextColumn FieldName="Footage" Caption="Footage"  ReadOnly="true" Width="80px">
									<PropertiesTextEdit ClientInstanceName="Footer" DisplayFormatString="#,0.00" Width="100%" />
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
							</Columns>
							<SettingsEditing Mode="Batch" />
							<SettingsPager PageSize="200" AlwaysShowPager="True" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
							<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
							<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
							<SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
 							<Templates>
								<DetailRow>
									<dx:ASPxGridView ID="grdDetail" KeyFieldName="ReportGroupDetailID" runat="server" 
										DataSourceID="dsReportGroupDetail" Width="100%" OnBeforePerformDataSelect="grdMain_DataSelect" 
										SettingsBehavior-ConfirmDelete="True">
										<Columns>
											<dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Caption=" " ShowDeleteButton="true"/>
											<dx:GridViewDataColumn FieldName="ReportGroupDetailID" Visible="False" /> 
											<dx:GridViewDataColumn FieldName="ReportGroupID" Visible="False" /> 
											<dx:GridViewDataComboBoxColumn FieldName="DisplayTypeID" Caption="Display Type" Width="120px">
												<PropertiesComboBox Width="100%"
													ValueType="System.String" ValueField="DisplayTypeID" TextField="Description" DataSourceID="dsDisplayTypes" 
													EnableCallbackMode="true" CallbackPageSize="20"
													IncrementalFilteringMode="StartsWith" >
													<ClearButton Visibility="Auto" />
												</PropertiesComboBox>
											</dx:GridViewDataComboBoxColumn>
											<dx:GridViewDataTextColumn FieldName="Qty" Caption="Qty" Width="80px">
												<PropertiesTextEdit ClientInstanceName="Qty" DisplayFormatString="#,0" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
											</dx:GridViewDataTextColumn>
											<dx:GridViewDataTextColumn FieldName="Footage" Caption="Footage" ReadOnly="true" Width="80px">
												<PropertiesTextEdit ClientInstanceName="Footage" DisplayFormatString="#,0.00" Width="100%" />
												<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
												<EditCellStyle BackColor="WhiteSmoke" />
												<CellStyle BackColor="WhiteSmoke" />
											</dx:GridViewDataTextColumn>
										</Columns>  
										<SettingsPager PageSize="100" />
										<SettingsEditing Mode="Batch" />
										<ClientSideEvents EndCallback="OnEndCallbackDetail" />
									</dx:ASPxGridView>
								</DetailRow>
							</Templates>
							<SettingsDetail ShowDetailRow="true" />
						</dx:ASPxGridView>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
		</Items>
	</dx:aspxformlayout>

    <asp:SqlDataSource ID="dsStores" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
        SelectCommand="SELECT 0 AS StoreID, '<Select Store>' AS StoreName 
			UNION 
			SELECT ThriftOSStoreID AS StoreID, LocationAbbr AS StoreName
			FROM tblLocations
			WHERE ThriftOSStoreID IS NOT NULL
				AND Active = 1 
			ORDER BY StoreName">
    </asp:SqlDataSource>

	<asp:SqlDataSource ID="dsReportGroups" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="Stores.spReportGroups_SelectAll"
		SelectCommandType="StoredProcedure"
		InsertCommand="Stores.spReportGroups_Insert"
		InsertCommandType="StoredProcedure"
		UpdateCommand="Stores.spReportGroups_Update"
		UpdateCommandType="StoredProcedure"
		DeleteCommand="Stores.spReportGroups_Delete"
		DeleteCommandType="StoredProcedure">
		<SelectParameters>
			<asp:ControlParameter ControlID="fl$tbStoreID" Name="ThriftOSStoreID" PropertyName="Value" Type="Int32" />
		</SelectParameters>
		<InsertParameters>
			<asp:Parameter Name="DepartmentID" Type="Int32" />
			<asp:Parameter Name="Description" Type="String" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="ReportGroupID" Type="Int32" />
			<asp:Parameter Name="DepartmentID" Type="Int32" />
			<asp:Parameter Name="Description" Type="String" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</UpdateParameters>
		<DeleteParameters>
			<asp:Parameter Name="ReportGroupID" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</DeleteParameters>
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsDepartments" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
		SelectCommand="SELECT DepartmentID, DepartmentName 
			FROM SysConfig.Departments
			ORDER BY DepartmentName">
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsReportGroupDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
        SelectCommand="Stores.spReportGroupDetail_Select"
        SelectCommandType="StoredProcedure"
        InsertCommand="Stores.spReportGroupDetail_Insert"
        InsertCommandType="StoredProcedure"
        UpdateCommand="Stores.spReportGroupDetail_Update"
        UpdateCommandType="StoredProcedure"
        DeleteCommand="Stores.spReportGroupDetail_Delete"
        DeleteCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="ReportGroupID" SessionField="MasterID" Type="Int32" />
			<asp:ControlParameter ControlID="fl$tbStoreID" Name="ThriftOSStoreID" PropertyName="Value" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:SessionParameter Name="ReportGroupID" SessionField="MasterID" Type="Int32" />
			<asp:ControlParameter ControlID="fl$tbStoreID" Name="ThriftOSStoreID" PropertyName="Value" Type="Int32" />
            <asp:Parameter Name="DisplayTypeID" Type="Int32" />
            <asp:Parameter Name="Qty" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="ReportGroupDetailID" Type="Int32" />
            <asp:SessionParameter Name="ReportGroupID" SessionField="MasterID" Type="Int32" />
			<asp:ControlParameter ControlID="fl$tbStoreID" Name="ThriftOSStoreID" PropertyName="Value" Type="Int32" />
            <asp:Parameter Name="DisplayTypeID" Type="Int32" />
            <asp:Parameter Name="Qty" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
       </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="ReportGroupDetailID" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>

	<asp:SqlDataSource ID="dsDisplayTypes" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT DisplayTypeID, [Description] 
			FROM Stores.DisplayTypes
			ORDER BY [Description]">
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
