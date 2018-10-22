<%@ Page Title="Display Types" Language="VB" Theme="Youthful" AutoEventWireup="false" CodeFile="DisplayTypes.aspx.vb" Inherits="DisplayTypes" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Display Types" runat="server"></dx:ASPxLabel></td>
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
						<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsDisplayTypes" 
							KeyFieldName="DisplayTypeID" Width="100%" EnableRowsCache="false" 
							SettingsBehavior-ConfirmDelete="True">
							<Columns>
								<dx:GridViewCommandColumn Width="80" ShowNewButtonInHeader="True" Caption=" " ShowDeleteButton="True" />
								<dx:GridViewDataColumn FieldName="DisplayTypeID" Visible="false" />
								<dx:GridViewDataColumn FieldName="Description" /> 
								<dx:GridViewDataTextColumn FieldName="Footage" Caption="Footage" Width="80px">
									<PropertiesTextEdit DisplayFormatString="#,0.00" Width="100%">
									</PropertiesTextEdit>
									<HeaderStyle HorizontalAlign="Right" Wrap="True"></HeaderStyle>
								</dx:GridViewDataTextColumn>
							</Columns>
							<SettingsEditing Mode="Batch" />
							<SettingsPager PageSize="200" AlwaysShowPager="True" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
							<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
						</dx:ASPxGridView>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
		</Items>
	</dx:aspxformlayout>

	<asp:SqlDataSource ID="dsDisplayTypes" runat="server"
		ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
		SelectCommand="Stores.spDisplayTypes_Select"
		SelectCommandType="StoredProcedure"
		InsertCommand="Stores.spDisplayTypes_Insert"
		InsertCommandType="StoredProcedure"
		UpdateCommand="Stores.spDisplayTypes_Update"
		UpdateCommandType="StoredProcedure"
		DeleteCommand="Stores.spDisplayTypes_Delete"
		DeleteCommandType="StoredProcedure">
		<InsertParameters>
			<asp:Parameter Name="Description" Type="String" />
			<asp:Parameter Name="Footage" Type="Decimal" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="DisplayTypeID" Type="Int32" />
			<asp:Parameter Name="Description" Type="String" />
			<asp:Parameter Name="Footage" Type="Decimal" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</UpdateParameters>
		<DeleteParameters>
			<asp:Parameter Name="DisplayTypeID" Type="Int32" />
            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
		</DeleteParameters>
	</asp:SqlDataSource>

	<dx:ASPxGlobalEvents ID="ge" runat="server">
		<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
	</dx:ASPxGlobalEvents>

    </form>
</body>
</html>
