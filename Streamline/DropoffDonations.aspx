<%@ Page Title="Drop-off Donations" Language="VB" Theme="Youthful" AutoEventWireup="false" CodeFile="DropoffDonations.aspx.vb" Inherits="DropoffDonations" %>

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
   <script type="text/javascript">
   		function OnInit(s, e) {
   			AdjustSize();
   		}
   		function OnControlsInitialized(s, e) {
   			ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
   				AdjustSize();
   			});
   		}
   		function AdjustSize() {
   			var height = Math.max(0, document.documentElement.clientHeight) - 150;
   			gridMain.SetHeight(height);
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Drop-off Donations" runat="server"></dx:ASPxLabel></td>
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
					<asp:DropDownList ID="ddlLocations" runat="server" ToolTip="Cart Location" Width="100%" AutoPostBack="True" />
 				</td>
   	            <td style="width:20%" />
    	        <td style="width:20%" />
    	        <td style="width:20%" />
    	        <td style="width:20%" />
 			</tr>
		</table>
	</div>
	<br />
 	<div id="divSearch" class="specials" runat="server" >

		<table id="tblDonations" runat="server" style="visibility: visible">
			<tr>
				<td style="width: 100%">
					<dx:ASPxGridView KeyFieldName="DropoffDonationID" ID="gridMain" EnableRowsCache="false"
						ClientInstanceName="gridMain" runat="server"
						DataSourceID="dsGridMain" Width="80%"
						OnCommandButtonInitialize="gridMain_CommandButtonInitialize"
						EnableCallBacks="true" >
						<Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="100" Caption=" " CellStyle-HorizontalAlign="Left" ShowNewButtonInHeader="true" ShowDeleteButton="true" ShowEditButton="true"/>
							<dx:GridViewDataColumn FieldName="DropoffDonationID" Visible="false" /> 
							<dx:GridViewDataColumn FieldName="LocationID" Visible="False" /> 
							<dx:GridViewDataColumn FieldName="Approved" Visible="False" /> 
		                    <dx:GridViewDataDateColumn FieldName="DonationDate" VisibleIndex="1" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="True" />
		                    <dx:GridViewDataTextColumn FieldName="SoftCarts" Caption="Soft" VisibleIndex="2" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="True">
		                        <PropertiesTextEdit DisplayFormatString="#,##0.00" />
							</dx:GridViewDataTextColumn>
		                    <dx:GridViewDataTextColumn FieldName="HardCarts" Caption="Hard" VisibleIndex="3" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="True">
		                        <PropertiesTextEdit DisplayFormatString="#,##0.00" />
							</dx:GridViewDataTextColumn>
		                    <dx:GridViewDataTextColumn FieldName="TotalCarts" Caption="Total" VisibleIndex="4" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False">
		                        <PropertiesTextEdit DisplayFormatString="#,##0.00" />
							</dx:GridViewDataTextColumn>
  							<dx:GridViewDataComboBoxColumn FieldName="CharityID" VisibleIndex="6" EditFormSettings-Visible="True" Caption="Charity" HeaderStyle-HorizontalAlign="Left">
								<PropertiesComboBox DataSourceID="dsCharities" TextField="CharityAbbr" ValueField="CharityID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
								</PropertiesComboBox>
								<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							</dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="Notes" Width="300" VisibleIndex="7" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="True" EditFormSettings-ColumnSpan="3" />
                            <dx:GridViewDataColumn FieldName="LastEditedBy" VisibleIndex="8" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False"/>
						</Columns>
						<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" ConfirmDelete="True" />
						<SettingsBehavior ProcessSelectionChangedOnServer="True" />
						<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
						<SettingsPager PageSize="20" AlwaysShowPager="True" />
						<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="300" />
						<SettingsEditing EditFormColumnCount="4" />
						<ClientSideEvents Init="OnInit" />
                        <SettingsCommandButton>
                            <DeleteButton Text="Del"/>
                        </SettingsCommandButton>
					</dx:ASPxGridView>

					<asp:SqlDataSource ID="dsGridMain" runat="server" 
						ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
						SelectCommand="spDropoffDonations_Select"
						SelectCommandType="StoredProcedure"
						InsertCommand="spDropoffDonation_Insert"
						InsertCommandType="StoredProcedure"
						UpdateCommand="spDropoffDonation_Update"
						UpdateCommandType="StoredProcedure"
						DeleteCommand="spDropoffDonation_Delete"
						DeleteCommandType="StoredProcedure">
						<SelectParameters>
							<asp:ControlParameter name="LocationID" ControlID="ddlLocations" PropertyName="SelectedValue" Type="Int32" />
							<asp:ControlParameter name="EarliestDonationDate" ControlID="dtEarliestDropoffDate" PropertyName="Value" Type="DateTime" />
						</SelectParameters>
						<InsertParameters>
							<asp:Parameter Name="DropoffDonationID" Type="Int32" />
							<asp:ControlParameter name="LocationID" ControlID="ddlLocations" PropertyName="SelectedValue" Type="Int32" />
							<asp:Parameter Name="DonationDate" Type="DateTime" />
							<asp:Parameter Name="SoftCarts" Type="Decimal" />
							<asp:Parameter Name="HardCarts" Type="Decimal" />
							<asp:Parameter Name="Notes" Type="String" />
							<asp:Parameter Name="CharityID" Type="Int32" />
							<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
						</InsertParameters>
						<UpdateParameters>
							<asp:Parameter Name="DropoffDonationID" Type="Int32" />
							<asp:ControlParameter name="LocationID" ControlID="ddlLocations" PropertyName="SelectedValue" Type="Int32" />
							<asp:Parameter Name="DonationDate" Type="DateTime" />
							<asp:Parameter Name="SoftCarts" Type="Decimal" />
							<asp:Parameter Name="HardCarts" Type="Decimal" />
							<asp:Parameter Name="Notes" Type="String" />
							<asp:Parameter Name="CharityID" Type="Int32" />
							<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
						</UpdateParameters>
						<DeleteParameters>
							<asp:Parameter Name="DropoffDonationID" Type="Int32" />
							<asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
						</DeleteParameters>
					</asp:SqlDataSource>
			
					<asp:SqlDataSource ID="dsCharities" runat="server"
						ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
						SelectCommand="SELECT CharityID, CharityAbbr 
							FROM tblCharities 
							UNION
							SELECT 0 AS CharityID, '' AS CharityAbbr
							ORDER BY CharityAbbr">
					</asp:SqlDataSource>							

					<dx:ASPxGlobalEvents ID="ge" runat="server">
						<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
					</dx:ASPxGlobalEvents>
				</td>
			</tr>
		</table>
	</div> 
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblDropoffDateMin" runat="server" Text="Earliest Drop-off Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtEarliestDropoffDate" ClientInstanceName="dtEarliestDropoffDate"
                        Width="80%" ToolTip="Earliest Drop-off Date" EditFormat="Custom" 
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
    </div>
	</form>
</body>
</html>
