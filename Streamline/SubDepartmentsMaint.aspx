<%@ Page Title="Carts Worked" Language="VB" Theme="Youthful" AutoEventWireup="false" CodeFile="SubDepartmentsMaint.aspx.vb" Inherits="SubDepartmentsMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="SubDepartments Maint" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
 	<dx:ASPxHiddenField ID="hf" runat="server" />
	<dx:aspxformlayout ID="fl" runat="server" ColCount="2" EnableTheming="True" Theme="Youthful" Width="40%">
		<Items>
			<dx:LayoutItem ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer>
						<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsSubDepartments" 
							KeyFieldName="ID" Width="100%" EnableRowsCache="false" 
							SettingsBehavior-ConfirmDelete="True"  OnCellEditorInitialize="grid_CellEditorInitialize">
							<Columns>
								<dx:GridViewCommandColumn Width="80" Caption=" " ShowEditButton="True" />
								<dx:GridViewDataColumn FieldName="ID" Visible="false" />
								<dx:GridViewDataComboBoxColumn FieldName="DepartmentID" Caption="Department" ReadOnly="true" Width="120px">
									<PropertiesComboBox Width="100%"
										ValueType="System.Int32" ValueField="DepartmentID" TextField="DepartmentName" DataSourceID="dsDepartments" >
									</PropertiesComboBox>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataComboBoxColumn>
								<dx:GridViewDataTextColumn FieldName="SubDepartmentName" Caption="SubDepartment" ReadOnly="true" Width="150px">
									<PropertiesTextEdit ClientInstanceName="SubDepartmentName" Width="100%" />
									<HeaderStyle HorizontalAlign="Left" Wrap="True"></HeaderStyle>
									<EditCellStyle BackColor="WhiteSmoke" />
									<CellStyle BackColor="WhiteSmoke" />
								</dx:GridViewDataTextColumn>
								<dx:GridViewDataComboBoxColumn FieldName="CartType" Caption="Cart Type" Width="60px">
									<PropertiesComboBox Width="100%"
										ValueType="System.String" ValueField="CartType" TextField="CartType" DataSourceID="dsCartTypes" 
										EnableCallbackMode="true" CallbackPageSize="10"
										IncrementalFilteringMode="StartsWith" >
										<ClearButton Visibility="Auto" />
									</PropertiesComboBox>
								</dx:GridViewDataComboBoxColumn>
								<dx:GridViewDataComboBoxColumn FieldName="ReportGroupID" Caption="Report Group" Width="180px">
									<PropertiesComboBox Width="100%"
										ValueType="System.Int32" ValueField="ReportGroupID" TextField="Description" DataSourceID="dsReportGroups" 
										EnableCallbackMode="true" CallbackPageSize="20" DropDownStyle="DropDown"
										IncrementalFilteringMode="StartsWith" >
										<ClearButton Visibility="Auto" />
									</PropertiesComboBox>
								</dx:GridViewDataComboBoxColumn>
							</Columns>
							<SettingsEditing Mode="Inline" />
							<SettingsPager PageSize="200" AlwaysShowPager="True" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
							<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
						</dx:ASPxGridView>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
		</Items>
	</dx:aspxformlayout>

	<asp:SqlDataSource ID="dsSubDepartments" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
		SelectCommand="SysConfig.spSubDepartmentAssignments_Select"
		SelectCommandType="StoredProcedure"
		UpdateCommand="SysConfig.spSubDepartmentAssignments_Update"
		UpdateCommandType="StoredProcedure" >
		<UpdateParameters>
			<asp:Parameter Name="ID" Type="Int32" />
			<asp:Parameter Name="CartType" Type="String" />
			<asp:Parameter Name="ReportGroupID" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</UpdateParameters>
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsCartTypes" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT 'Hard' AS CartType UNION SELECT 'Soft' AS CartType">
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsReportGroups" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="Stores.spReportGroups_SelectAll"
		SelectCommandType="StoredProcedure">
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsReportGroupsEditor" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="Stores.spReportGroups_Select"
		SelectCommandType="StoredProcedure">
		<SelectParameters>
			<asp:Parameter Name="DepartmentID" Type="Int32" />
		</SelectParameters>
	</asp:SqlDataSource>

	<asp:SqlDataSource ID="dsDepartments" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="SELECT DepartmentID, DepartmentName
			FROM [JMANN-SQL\THRIFTOS].NewMerchandise.SysConfig.Departments
			WHERE DepartmentActive = 1
			ORDER BY DepartmentName">
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
