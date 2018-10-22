<%@ Page Title="Streamline Inventory" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="StreamlineInventory.aspx.vb" Inherits="StreamlineInventory" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Streamline Inventory" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
	<br />
 	<asp:UpdatePanel ID="upItems" runat="server">
 	<ContentTemplate>
		<div id="divSearch" class="specials" runat="server" >
			<asp:Label ID="lblItems" runat="server" Text="Inventory Items:" Font-Size="14"></asp:Label>
			<table id="Table1" runat="server" style="visibility: visible">
				<tr>
					<td style="width: 100%">
						<dx:ASPxGridView KeyFieldName="ItemID" ID="gridItems" EnableRowsCache="true"
							ClientInstanceName="gridItems" runat="server"
							DataSourceID="dsGridItems" Width="100%"
						    OnHtmlDataCellPrepared="gridItems_HtmlDataCellPrepared" 
							EnableCallBacks="false">
							<Columns>
								<dx:GridViewDataColumn FieldName="ItemCode" Width="175" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" />
								<dx:GridViewDataColumn FieldName="ItemDescription" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" /> 
								<dx:GridViewDataColumn FieldName="ItemType" />
		                        <dx:GridViewDataTextColumn FieldName="OnHand" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right">
		                            <PropertiesTextEdit DisplayFormatString="#,##0" />
								</dx:GridViewDataTextColumn>
                                <dx:GridViewDataColumn FieldName="EstimatedRunoutDate" Width="100" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" />
                                <dx:GridViewDataColumn FieldName="LeadTime" Caption="Lead Time (in weeks)" Width="100" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" />
							</Columns>
							<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" />
							<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="false" />
							<SettingsPager PageSize="100" AlwaysShowPager="True" />
							<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="250" />
						</dx:ASPxGridView>

						<asp:SqlDataSource ID="dsGridItems" runat="server" 
							ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
							SelectCommand="SELECT I.ItemID, I.ItemCode, I.ItemDescription, IT.ItemType, I.OnHand, I.EstimatedRunoutDate, IT.LeadTime
                                FROM Streamline.Items AS I
                                INNER JOIN Streamline.ItemTypes AS IT ON IT.ItemTypeID = I.ItemTypeID
                                WHERE I.Active = 1
                                ORDER BY ItemType, ItemCode">
						</asp:SqlDataSource>
					</td>
				</tr>
			</table>  
		</div>
    </ContentTemplate>
	</asp:UpdatePanel>
	<table id="tblReports" runat="server" style="visibility: visible">
        <tr>
            <td style="width: 80%">
	            <asp:Label ID="lblInventoryTransactions" runat="server" Text="Inventory Transactions:" Font-Size="14"></asp:Label>
	        </td>
            <td style="width: 20%">
                <dx:ASPxButton runat="server" ID="btnInventoryUsage" Text="Streamline Inventory Usage" OnClick="btnInventoryUsage_Click" AutoPostBack="True" Width="90%"></dx:ASPxButton>
            </td>
       </tr>
    </table>
	<table id="tblInventoryTransactions" runat="server" style="visibility: visible">
		<tr>
			<td style="width: 100%">
				<dx:ASPxGridView KeyFieldName="InventoryTransactionID" ID="gridTransactions" EnableRowsCache="true"
					ClientInstanceName="gridTransactions" runat="server"
					DataSourceID="dsGridTransactions" Width="100%"
					OnCommandButtonInitialize="gridTransactions_CommandButtonInitialize"
					OnCellEditorInitialize="gridTransactions_CellEditorInitialize"
					EnableCallBacks="True" >
					<Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Width="60" Caption=" " CellStyle-HorizontalAlign="Left" ShowNewButtonInHeader="true" ShowDeleteButton="true" ShowEditButton="true" />
						<dx:GridViewDataComboBoxColumn FieldName="TransactionTypeID" Visible="False" EditFormSettings-Visible="True" Caption="Transaction Type" HeaderStyle-HorizontalAlign="Left">
							<PropertiesComboBox DataSourceID="dsTransactionTypes" TextField="TransactionType" ValueField="TransactionTypeID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
							</PropertiesComboBox>
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						</dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataColumn FieldName="TransactionDate" Caption="Date" Width="100" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="True" />
						<dx:GridViewDataColumn FieldName="TransactionType" Caption=" " Width="80" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
						<dx:GridViewDataComboBoxColumn FieldName="ItemID" Caption="Item" HeaderStyle-HorizontalAlign="Left">
							<PropertiesComboBox DataSourceID="dsItems" TextField="ItemCode" ValueField="ItemID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
							</PropertiesComboBox>
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						</dx:GridViewDataComboBoxColumn>
						<dx:GridViewDataColumn FieldName="ItemType" Caption=" " Width="175" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
		                <dx:GridViewDataTextColumn FieldName="Quantity" Caption="Qty" Width="80" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="True">
		                    <PropertiesTextEdit DisplayFormatString="#,##0" />
						</dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Notes" Width="200" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="True" EditFormSettings-ColumnSpan="4" />
                        <dx:GridViewDataColumn FieldName="Route" Width="200" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
                        <dx:GridViewDataColumn FieldName="Charity" Width="70" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
                        <dx:GridViewDataColumn FieldName="MailingDate" Width="100" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
                        <dx:GridViewDataColumn FieldName="LastEditedBy" Width="80" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False"/>
					</Columns>
					<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" ConfirmDelete="True" />
					<SettingsBehavior ProcessSelectionChangedOnServer="True" />
					<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
					<SettingsPager PageSize="500" AlwaysShowPager="True" />
					<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="340" />
					<SettingsEditing EditFormColumnCount="4" />
                    <SettingsCommandButton>
                        <DeleteButton Text="Del"/>
                    </SettingsCommandButton>
                    <ClientSideEvents EndCallback="function OnEndCallback(s,e) { gridItems.PerformCallback(); }" />
				</dx:ASPxGridView>

				<asp:SqlDataSource ID="dsGridTransactions" runat="server" 
					ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
					InsertCommand="Streamline.spInventoryTransactions_Insert"
					InsertCommandType="StoredProcedure"
					UpdateCommand="Streamline.spInventoryTransactions_Update"
					UpdateCommandType="StoredProcedure"
					DeleteCommand="Streamline.spInventoryTransactions_Delete"
					DeleteCommandType="StoredProcedure">
					<InsertParameters>
						<asp:Parameter Name="TransactionDate" Type="DateTime" />
						<asp:Parameter Name="ItemID" Type="Int32" />
						<asp:Parameter Name="TransactionTypeID" Type="Int32" />
						<asp:Parameter Name="Quantity" Type="Int32" />
						<asp:Parameter Name="Notes" Type="String" />
						<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
					</InsertParameters>
					<UpdateParameters>
						<asp:Parameter Name="InventoryTransactionID" Type="Int32" />
						<asp:Parameter Name="TransactionDate" Type="DateTime" />
						<asp:Parameter Name="ItemID" Type="Int32" />
						<asp:Parameter Name="TransactionTypeID" Type="Int32" />
						<asp:Parameter Name="Quantity" Type="Int32" />
						<asp:Parameter Name="Notes" Type="String" />
						<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
					</UpdateParameters>
					<DeleteParameters>
						<asp:Parameter Name="InventoryTransactionID" Type="Int32" />
						<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
					</DeleteParameters>
				</asp:SqlDataSource>
			
				<asp:SqlDataSource ID="dsTransactionTypes" runat="server"
					ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
					SelectCommand="SELECT TransactionTypeID, TransactionType FROM Streamline.TransactionTypes 
						WHERE TransactionTypeID IN (1, 3, 4, 5)
						ORDER BY SortCode">
				</asp:SqlDataSource>							

				<asp:SqlDataSource ID="dsItems" runat="server"
					ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
					SelectCommand="SELECT ItemID, ItemCode
						FROM Streamline.Items 
						WHERE Active = 1
						ORDER BY ItemCode">
				</asp:SqlDataSource>							
			</td>
		</tr>
	</table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblTransactionDateMin" runat="server" Text="Earliest Transaction Date:"></asp:Label>
                <dx:ASPxDateEdit ID="dtEarliestTransactionDate" ClientInstanceName="dtEarliestTransactionDate"
                    Width="80%" ToolTip="Earliest Pickup Date" EditFormat="Custom" 
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
        </tr>
    </table>
	</form>
</body>
</html>
