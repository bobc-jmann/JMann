<%@ Page Title="Missing Street Segments" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="StreetSegments.aspx.vb" Inherits="MissingStreetSegments" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>

    <script type="text/javascript">
        function OnInit(s, e) {
            AdjustSize();
        }
        function OnEndCallback(s, e) {
            AdjustSize();
            if (s.cpServerMessage != null)
                alert(s.cpServerMessage);
        }
        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight) - 450;
            grdMissingSegments.SetHeight(height);
        }
    </script>

	<script type="text/javascript">
		function GeocoderStreetsSearch(s, e) {
			var uniqueID = document.getElementById("txtUniqueID");
			var zip5 = document.getElementById("txtZip5");
			var streetNameLike = document.getElementById("txtStreetNameLike");

			if (uniqueID.value != "") {
				grdStreetNameSearch.PerformCallback();
			}
			else if (zip5.value != "" && streetNameLike.value != "") {
				grdStreetNameSearch.PerformCallback();
			}
			else {
				alert("Please enter a Unique_ID OR a Zip5 and a Street Name Like.");
			}
			return false;
		}

		function FindMissingSegments(s, e) {
			var btn = document.getElementById("btnFindMissingSegments");
			if (btn.value == "Processing...")
				return false;	// prevent repeated button pushing
			var agree = confirm("Are you sure you want to run Find Missing Segments?");
			if (agree) {
				btn.value = "Processing...";
				return true;
			}
			return false;
		}

		function CreateNewRules(s, e) {
			var btn = document.getElementById("btnCreateNewRules");
			if (btn.value == "Processing...")
				return false;	// prevent repeated button pushing
			var agree = confirm("Are you sure you want to run Create New Rules?");
			if (agree) {
				btn.value = "Processing...";
				return true;
			}
			return false;
		}

		function UpdateGeocoderStreets(s, e) {
			var btn = document.getElementById("btnUpdateGeocoderStreets");
			if (btn.value == "Processing...")
				return false;	// prevent repeated button pushing
			var agree = confirm("Are you sure you want to run Update Geocoder Streets?");
			if (agree) {
				btn.value = "Processing...";
				return true;
			}
			return false;
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
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Street Segments" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table>
            <tr>
                <td style="width: 20%">Driver Location:
				    <asp:DropDownList ID="ddlDriverLocations" runat="server" ToolTip="Driver Location" Width="100%" AutoPostBack="True" />
 				</td>
				<td style="width: 20%">Route:
				    <asp:DropDownList ID="ddlRoutes" runat="server" ToolTip="Route" Width="100%" AutoPostBack="True" />
 				</td>
                <td style="width: 60%"></td>
            </tr>
         </table>
         <table id="tblMissingSegments" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 98%">

                    <dx:ASPxGridView ID="grdMissingSegments" KeyFieldName="MissingSegmentID" runat="server" 
                            DataSourceID="dsMissingSegments" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False" 
                            Settings-VerticalScrollBarMode="Auto" EnableViewState="false">
                       <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowEditButton="true" Width="50" ShowClearFilterButton="True" />
                            <dx:GridViewDataColumn FieldName="MissingSegmentID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="RegionCode" VisibleIndex="1" EditFormSettings-Visible="False" Caption="Region" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="2" EditFormSettings-Visible="False" Caption="Route" Width="80" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="SectionCode" VisibleIndex="3" EditFormSettings-Visible="False" Caption="Section" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="Zip5" VisibleIndex="4" EditFormSettings-Visible="False" Caption="Zip5" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="StreetName" VisibleIndex="5" EditFormSettings-Visible="False" Caption="Street" Width="150" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="MinSN" VisibleIndex="6" EditFormSettings-Visible="False" Caption="Min SN" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="MaxSN" VisibleIndex="7" EditFormSettings-Visible="False" Caption="Max SN" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="City" VisibleIndex="8" EditFormSettings-Visible="False" Caption="City" Width="100" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataComboBoxColumn FieldName="Resolution" VisibleIndex="11" Width="100" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesComboBox DataSourceID="dsResolution" TextField="Resolution" ValueField="Resolution" ValueType="System.String" IncrementalFilteringMode="StartsWith" >
                                </PropertiesComboBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="FromStreetName" VisibleIndex="12" Width="150" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="ToStreetName" VisibleIndex="13" Width="150" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="Comment" VisibleIndex="14" Width="200" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="ModifiedOn" VisibleIndex="21" EditFormSettings-Visible="False" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="ModifiedBy" VisibleIndex="22" EditFormSettings-Visible="False" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                        </Columns>
                        <SettingsEditing EditFormColumnCount="4" />
                        <Settings ShowFilterRow="True" ShowGroupPanel="False" ShowFooter="True" />
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="100" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
					</dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsMissingSegments" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        UpdateCommand="spMissingSegments_Update"
						UpdateCommandType="StoredProcedure">
                        <UpdateParameters>
                            <asp:Parameter Name="MissingSegmentID" Type="Int32" />
                            <asp:Parameter Name="Resolution" Type="String" />
                            <asp:Parameter Name="FromStreetName" Type="String" />
                            <asp:Parameter Name="ToStreetName" Type="String" />
                            <asp:Parameter Name="Comment" Type="String" />
                            <asp:SessionParameter Name="ModifiedBy" SessionField="vUserName" Type="String" />
                        </UpdateParameters>
                     </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsResolution" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
						SelectCommand="SELECT 'MapInfo Error' AS Resolution UNION SELECT 'Missing' AS Resolution UNION SELECT 'SMS Error' AS Resolution">
                    </asp:SqlDataSource>
 
                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 2%"></td>
            </tr>
        </table>
       <table>
            <tr>
                <td style="width: 6%">Unique_ID:
				    <asp:TextBox ID="txtUniqueID" runat="server"></asp:TextBox>
 				</td>
				<td style="width: 6%">Zip5:
				    <asp:TextBox ID="txtZip5" runat="server"></asp:TextBox>
 				</td>
				<td style="width: 20%">Street Name Like:
				    <asp:TextBox ID="txtStreetNameLike" runat="server"></asp:TextBox>
 				</td>
				<td style="width: 20%">
					<asp:Button ID="txtSearch" runat="server" Text="Search" OnClientClick="return GeocoderStreetsSearch()" Width="200px" />
				</td>
                <td style="width: 18%"></td>
				<td style="width: 10%">
					<asp:UpdatePanel ID="UpdatePanelNR" runat="server">
					<ContentTemplate>
					<asp:Button ID="btnCreateNewRules" runat="server" Text="Create New Rules" OnClick="RunCreateNewRules" OnClientClick="return CreateNewRules()" Width="200px" />
					</ContentTemplate>
					</asp:UpdatePanel>
				</td>
				<td style="width: 10%">
					<asp:UpdatePanel ID="UpdatePanelGS" runat="server">
					<ContentTemplate>
					<asp:Button ID="btnUpdateGeocoderStreets" runat="server" Text="Update Geocoder Streets" OnClick="RunUpdateGeocoderStreets" OnClientClick="return UpdateGeocoderStreets()" Width="200px" />
					</ContentTemplate>
					</asp:UpdatePanel>
				</td>
				<td style="width: 10%">
<%--					<asp:UpdatePanel ID="UpdatePanelMS" runat="server">
					<ContentTemplate>
 					<asp:Button ID="btnFindMissingSegments" runat="server" Text="Find Missing Segments" OnClick="RunFindMissingSegments" OnClientClick="return FindMissingSegments()" Width="200px" />
					</ContentTemplate>
					</asp:UpdatePanel>--%>
				</td>
            </tr>
         </table>
         <table id="tblStreetNameSearch" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%">

                    <dx:ASPxGridView ID="grdStreetNameSearch" KeyFieldName="Unique_ID" runat="server" 
							DataSourceID="dsGeocoderStreets" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False"
							ClientInstanceName="grdStreetNameSearch"
							OnCustomCallback="grdStreetNameLike_CustomCallback"
                            Settings-VerticalScrollBarMode="Auto">
                       <Columns>
							<dx:GridViewCommandColumn VisibleIndex="1" Caption=" " ShowEditButton="true" Width="60" />
                            <dx:GridViewDataColumn FieldName="Unique_ID" VisibleIndex="2" Width="60" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="Name" VisibleIndex="11" Width="150" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="FromLeft" VisibleIndex="12" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="ToLeft" VisibleIndex="13" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="FromRight" VisibleIndex="14" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="ToRight" VisibleIndex="15" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="PC_Left" VisibleIndex="16" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="PC_Right" VisibleIndex="17" Width="50" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="CityLeft" VisibleIndex="18" Width="150" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="CityRight" VisibleIndex="19" Width="150" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" />
							<dx:GridViewDataColumn FieldName="Route" VisibleIndex="20" Width="100" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
							<dx:GridViewDataColumn FieldName="Section" VisibleIndex="21" Width="60" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
							<dx:GridViewDataColumn FieldName="Route2" VisibleIndex="22" Width="100" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
							<dx:GridViewDataColumn FieldName="Section2" VisibleIndex="23" Width="60" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataCheckColumn FieldName="ManualUpdate" VisibleIndex="31" Width="60" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                        </Columns>
                        <SettingsEditing EditFormColumnCount="4" />
                        <Settings ShowFilterRow="True" ShowGroupPanel="False" ShowFooter="True" ShowHeaderFilterButton="False" />
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="100" />
 					</dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsGeocoderStreets" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="spTempGS_Select"
						SelectCommandType="StoredProcedure"
                        UpdateCommand="spTempGS_Update"
						UpdateCommandType="StoredProcedure"
						CancelSelectOnNullParameter="False">
						<SelectParameters>
							<asp:ControlParameter Name="uniqueID" ControlID="txtUniqueID" Type="Int32" ConvertEmptyStringToNull="true" />
							<asp:ControlParameter Name="zip5" ControlID="txtZip5" Type="String" ConvertEmptyStringToNull="true" />
							<asp:ControlParameter Name="streetNameLike" ControlID="txtStreetNameLike" Type="String" ConvertEmptyStringToNull="true" />
						</SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Unique_ID" Type="Int32" />
                            <asp:Parameter Name="Route" Type="String" />
                            <asp:Parameter Name="Section" Type="String" />
                            <asp:Parameter Name="Route2" Type="String" />
                            <asp:Parameter Name="Section2" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
				</td>
            </tr>
        </table>
     
    </div>
    </form>
</body>
</html>
