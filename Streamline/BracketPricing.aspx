<%@ Page Title="Bracket Pricing" Language="VB" Theme="Youthful" AutoEventWireup="false" CodeFile="BracketPricing.aspx.vb" Inherits="BracketPricing" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

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
            var height = Math.max(0, document.documentElement.clientHeight) - 150;
            grdMain.SetHeight(height);
        }
    </script>
	<script type="text/javascript">
		var cur_row = -1;
		function grid_RowDblClick(s, e) {
			s.StartEditRow(e.visibleIndex);
		}
		function grid_FocusedRowChanged(s, e) {
			if (s.cpIsEditing)
				s.UpdateEdit();
		}
		function editor_KeyDown(s, e) {

			switch (e.htmlEvent.keyCode) {
				case 13:
					cur_row = grdMain.GetFocusedRowIndex();
					grdMain.UpdateEdit();
					break;
				case 27:
					grdMain.CancelEdit();
					break;
			}
		}
		function grid_EndCallback(s, e) {
			AdjustSize();
			var edit = s.GetEditor(1);
			if (edit) {
				edit.SelectAll();
				edit.SetFocus();
			}

			if (cur_row >= 0) {
				var cr = cur_row + 1;
				cur_row = -1;
				var start_vi = s.GetTopVisibleIndex(),
                    row_count = s.GetVisibleRowsOnPage();
				end_vi = start_vi + (row_count == 0 ? row_count : row_count - 1);
				if (cr > end_vi)
					cr = 0;
				s.SetFocusedRowIndex(cr);
				s.StartEditRow(cr);
			}
		}
		function ButtonUpdate(id) {
			if (id.value == "Processing...")
				return;	// prevent repeated button pushing
			id.value = "Processing...";
			//id.disabled = true; RCC - 9/17/15 - doesn't call the server if disabled
		}
		function ButtonCartsUpdate(id) {
			if (id.value == "Processing...")
				return;	// prevent repeated button pushing
			id.value = "Processing...";
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
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Bracket Pricing" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table>
            <tr>
                <td style="width: 50%">Year-Week:
				    <asp:DropDownList ID="ddlYearWeek" runat="server" ToolTip="Week" Width="100%" AutoPostBack="True" />
 				</td>
                <td style="width: 50%">
					<asp:Button ID="btnUpdate" runat="server" Text="Update Data for Reports" OnClientClick="ButtonUpdate(this);"/>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">Location:
				    <asp:DropDownList ID="ddlLocations" runat="server" ToolTip="Store" Width="100%" AutoPostBack="True" />
 				</td>
                <td style="width: 50%">
					<asp:Button ID="btnCartsUpdate" runat="server" Text="Update Cart Data" OnClientClick="ButtonCartsUpdate(this);"/>
                </td>
            </tr>
        </table>
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 30%">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" DataSourceID="dsBracketPricing" 
						KeyFieldName="ID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True"
						OnCustomJSProperties="grdMain_CustomJSProperties" OnCellEditorInitialize="grdMain_CellEditorInitialize"
						EnableCallBacks="True">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="60" ShowDeleteButton="true"/>
							<dx:GridViewDataColumn FieldName="ID" Visible="false" /> 
                            <dx:GridViewDataTextColumn FieldName="Qty" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True"> 
								<PropertiesTextEdit>
									<ClientSideEvents KeyDown="function(s, e) { KeyDownEventHandler(s, e) }" />
								</PropertiesTextEdit>                        
							</dx:GridViewDataTextColumn>
							<dx:GridViewDataColumn FieldName="PriceLevelID" Visible="false" /> 
							<dx:GridViewDataColumn FieldName="PriceLevel" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" ReadOnly="true" CellStyle-BackColor="WhiteSmoke"/> 
							<dx:GridViewDataColumn FieldName="DepartmentID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="DepartmentName" Caption="Department" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" ReadOnly="true" CellStyle-BackColor="WhiteSmoke"/> 
						</Columns>
						<SettingsBehavior AllowFocusedRow="True" />
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="100" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
						<ClientSideEvents EndCallback="grid_EndCallback" FocusedRowChanged="grid_FocusedRowChanged" />
                        <ClientSideEvents Init="OnInit" />
                            <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
						<GroupSummary>
							<dx:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" />
						</GroupSummary>
						<Settings ShowGroupPanel="true" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsBracketPricing" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="Stores.spBracketPricing_SelectLocation"
                        SelectCommandType="StoredProcedure"
                        UpdateCommand="Stores.spBracketPricing_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="Stores.spBracketPricing_Delete"
                        DeleteCommandType="StoredProcedure">
						<SelectParameters>
							<asp:ControlParameter ControlID="ddlYearWeek" Name="YearWeek" PropertyName="SelectedValue" Type="Int32"/>
							<asp:ControlParameter ControlID="ddlLocations" Name="LocationID" PropertyName="SelectedValue" Type="Int32"/>
						</SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ID" Type="Int32" />
							<asp:ControlParameter ControlID="ddlYearWeek" Name="YearWeek" PropertyName="SelectedValue" Type="Int32"/>
							<asp:ControlParameter ControlID="ddlLocations" Name="LocationID" PropertyName="SelectedValue" Type="Int32"/>
                            <asp:Parameter Name="DepartmentID" Type="Int32" />
                            <asp:Parameter Name="PriceLevelID" Type="Int32" />
                            <asp:Parameter Name="Qty" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="ID" Type="Int32" />
                           <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 70%" />
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
