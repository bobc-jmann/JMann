<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sys_GetLongLat.aspx.vb" Inherits="System_sys_GetLongLat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
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
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="Scripts/jquery-1.4.1.min.js" />
        </Scripts>
    </asp:ScriptManager>

    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding="2" cellspacing="0" style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Google Geocoding" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
	<br />
	<br />
	<br />
	<asp:UpdatePanel ID="upMain" runat="server">
 		<ContentTemplate>
			<asp:Timer runat="server" id="Timer1" Interval="1000" OnTick="Timer1_Tick" Enabled="false" />

			<dx:ASPxHiddenField runat="server" ClientInstanceName="hfGeocode" ID="hfGeocode" />

			<asp:Button ID="btnStartGeocoding" runat="server" OnClick="btnStartGeocoding_Click" 
				Text="Start Geocoding" Class="DevExButton" Width="200px">
			</asp:Button>
			<br />
 			<dx:ASPxLabel ID="lblNotes" runat="server" Text="" Font-Size="12" ForeColor="Magenta" />
			<br />
 			<dx:ASPxLabel ID="lblQtyToday" runat="server" Text="" Font-Size="12" ForeColor="Blue" />

			<div id="divSearch" class="specials" runat="server">
				 <table id="tblSchedule" runat="server" style="visibility: visible">
					<tr>
						<td style="width: 100%" colspan="5">
						   <dx:ASPxGridView KeyFieldName="AddressID" ID="gridMain" EnableRowsCache="true"
							   ClientInstanceName="gridMain" runat="server" 
							   DataSourceID="dsGridMain" Width="100%"
							   EnableCallBacks="False">
								<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" ConfirmDelete="True" />
								<Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
								<Columns>
									<dx:GridViewDataColumn FieldName="AddressID" Width="80" VisibleIndex="1" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" EditFormSettings-Visible="False" />
									<dx:GridViewDataColumn FieldName="StreetAddress" VisibleIndex="2" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Left" EditFormSettings-Visible="False" />
									<dx:GridViewDataColumn FieldName="City" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
									<dx:GridViewDataColumn FieldName="State" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
									<dx:GridViewDataColumn FieldName="Zip5" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
								</Columns>
								<SettingsPager PageSize="2500" AlwaysShowPager="True" />
								<Settings VerticalScrollableHeight="0" VerticalScrollBarMode="Visible" />
								<ClientSideEvents Init="OnInit" />
							</dx:ASPxGridView>

							<asp:SqlDataSource ID="dsGridMain" runat="server" 
								ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
								SelectCommand="spGoogleGeocodingSelect"
								SelectCommandType="StoredProcedure">
							</asp:SqlDataSource>
							<asp:HiddenField ID="hfGridMainSelectCommand" runat="server" Value=""></asp:HiddenField>

							<dx:ASPxGlobalEvents ID="ge" runat="server">
								<ClientSideEvents ControlsInitialized="OnControlsInitialized" />
							</dx:ASPxGlobalEvents>
						</td>
					 </tr>
				</table>  
			</div>

		</ContentTemplate>
	</asp:UpdatePanel>
    <br />

    </form>
</body>
</html>
