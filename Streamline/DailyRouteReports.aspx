<%@ Page Title="Daily Route Reports" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="DailyRouteReports.aspx.vb" Inherits="DailyRouteReports" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<script type="text/javascript">

        function OnMissingInfoSuccess(result) {
            if (result.PickupsDriver > 0)
                document.getElementById("lblMissingPickupsDriver").innerText = result.PickupsDriver + " record(s) do not have Driver Pickups entered.";
            else
            	document.getElementById("lblMissingPickupsDriver").innerText = "";

            if (result.SoftCarts > 0)
            	document.getElementById("lblMissingSoftCarts").innerText = result.SoftCarts + " record(s) do not have Soft Carts entered.";
            else
            	document.getElementById("lblMissingSoftCarts").innerText = "";

            if (result.HardCarts > 0)
            	document.getElementById("lblMissingHardCarts").innerText = result.HardCarts + " record(s) do not have Hard Carts entered.";
            else
            	document.getElementById("lblMissingHardCarts").innerText = "";

            if (result.LocationsUnloaded > 0)
            	document.getElementById("lblMissingLocationsUnloaded").innerText = result.LocationsUnloaded + " record(s) do not have a Location Unloaded entered.";
            else
            	document.getElementById("lblMissingLocationsUnloaded").innerText = "";
		} // end OnMissingInfoSuccess

        function OnMissingInfoError(result) {
            var routeSections = document.getElementById("ddlDriverLocations").value;
            alert('GetMissingInfo Error');
        } // end OnMissingInfoError

	</script>
    <script type="text/javascript">
            function OnInit(s, e) {
                AdjustSize();
            }
            function OnEndCallback(s, e) {
            	var pickupDate = dtPickupDate.GetDate();
            	var driverLocationID = document.getElementById("ddlDriverLocations").value;
            	PageMethods.GetMissingInfo(pickupDate, driverLocationID, OnMissingInfoSuccess, OnMissingInfoError);

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
                var height = Math.max(0, document.documentElement.clientHeight) - 220;
                grdMain.SetHeight(height);
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Daily Route Reports" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="100%">
            <tr>
                <td style="width: 20%">Driver Location:
				    <asp:DropDownList ID="ddlDriverLocations" runat="server" ToolTip="Driver Location" Width="100%" AutoPostBack="True" />
 				</td>
                <td style="width: 40%" />
                <td style="width: 20%">
                    <dx:ASPxButton runat="server" ID="btnDailyReportReport" Text="Daily Route Reports" OnClick="btnRunDailyRouteReport_Click" AutoPostBack="True" Width="90%"></dx:ASPxButton>
                </td>
                <td style="width: 20%">
                    <dx:ASPxButton runat="server" ID="btnMissingDailyRouteData" Text="Missing Daily Route Data" OnClick="btnRunMissingDailyRouteDataReport_Click" AutoPostBack="True" Width="90%"></dx:ASPxButton>
                </td>
            </tr>
            <tr>
 				<td style="width: 20%">
                    <asp:Label ID="lblPickupDate" runat="server" Text="Pickup Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtPickupDate" ClientInstanceName="dtPickupDate"
                        Width="100%" ToolTip="Pickup Date" EditFormat="Custom" 
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
                <td></td>
                 <td>
                    <dx:ASPxButton runat="server" ID="btnMissingPickupsFromTablets" Text="Missing Pickups From Tablets" OnClick="btnRunMissingPickupsFromTabletsReport_Click" AutoPostBack="True" Width="90%"></dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton runat="server" ID="btnMissingPickupsForDrivers" Text="Missing Drivers for Pickups" OnClick="btnMissingDriversForPickups_Click" AutoPostBack="True" Width="90%"></dx:ASPxButton>
                </td>
			</tr>
        </table>
        <table>
            <tr>
                <td></td>
 				<td>
                     <asp:Label ID="lblMissingPickupsDriver" runat="server" Text="" Forecolor="DarkMagenta" Font-Names="Arial" Font-Size="Medium" ></asp:Label>
                </td>
            </tr>               
			<tr>
               <td></td>
 				<td>
                     <asp:Label ID="lblMissingSoftCarts" runat="server" Text="" Forecolor="DarkCyan" Font-Names="Arial" Font-Size="Medium" ></asp:Label>
                </td>
            </tr>               
           <tr>
                <td></td>
 			    <td>
                     <asp:Label ID="lblMissingHardCarts" runat="server" Text="" Forecolor="DarkCyan" Font-Names="Arial" Font-Size="Medium"></asp:Label>
                </td>
            </tr>               
           <tr>
                <td></td>
 			    <td>
                     <asp:Label ID="lblMissingLocationsUnloaded" runat="server" Text="" Forecolor="DarkCyan" Font-Names="Arial" Font-Size="Medium"></asp:Label>
                </td>
            </tr>               
        </table>

        <table id="tblMain" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">

                    <dx:ASPxGridView ID="grdMain" KeyFieldName="DriverAssignmentID" runat="server" 
                            DataSourceID="dsRouteReport" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False" 
                            Settings-VerticalScrollBarMode="Auto" EnableViewState="false"
							OnStartRowEditing="grdDriverAssignments_StartRowEditing" 
                            OnRowDeleting="grdDriverAssignments_RowDeleting" 
                            OnCommandButtonInitialize="grdDriverAssignments_CommandButtonInitialize"
                            OnCustomButtonInitialize="grdDriverAssignments_CustomButtonInitialize">
                       <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " ShowDeleteButton="true">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="cbAddDriver" Text="Add" Visibility="AllDataRows"/>
                                    <dx:GridViewCommandColumnCustomButton ID="cbAddSpecial" Text="Spec" Visibility="AllDataRows"/>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="DriverAssignmentID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="PickupScheduleSectionID" Visible="False" />
                            <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="False" />
                            <dx:GridViewDataColumn FieldName="PickupsSectionExists" Visible="False" />
                            <dx:GridViewDataColumn FieldName="NonTabletBagPickup" VisibleIndex="1" ReadOnly="true" Caption="NT" Width="30" CellStyle-BackColor="WhiteSmoke" />
                            <dx:GridViewDataColumn FieldName="RouteID" Visible="False" />
                            <dx:GridViewDataColumn FieldName="RouteCode" Visible="False" />
                            <dx:GridViewDataColumn FieldName="RouteDesc" Visible="False" />
                            <dx:GridViewDataColumn FieldName="SectionID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="SectionCode" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="SectionDesc" Visible="False" />
							<dx:GridViewDataColumn FieldName="Route-Section" VisibleIndex="4" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-BackColor="WhiteSmoke">
                                <EditFormSettings Visible="False"></EditFormSettings>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="DriverID" VisibleIndex="5" Caption="Driver" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesComboBox DataSourceID="dsDriver" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
                                </PropertiesComboBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="CntPickupsDriver" Caption="Driver Pickups" VisibleIndex="9" ReadOnly="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
                                <PropertiesTextEdit ClientInstanceName="CntPickupsDriver" DisplayFormatString="#,##0" />
							</dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn FieldName="CntPickupsAddresses" Caption="Address Pickups" VisibleIndex="10" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-BackColor="WhiteSmoke" >
                                <PropertiesTextEdit ClientInstanceName="CntPickupsAddresses" DisplayFormatString="#,##0" />
							</dx:GridViewDataTextColumn>  
                            <dx:GridViewDataTextColumn FieldName="SoftCarts" VisibleIndex="21" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesTextEdit ClientInstanceName="SoftCarts" DisplayFormatString="#.00" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HardCarts" VisibleIndex="22" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesTextEdit ClientInstanceName="HardCarts" DisplayFormatString="#.00" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="TotalCarts" VisibleIndex="23" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormSettings-Visible="False" CellStyle-BackColor="WhiteSmoke">
                                <PropertiesTextEdit ClientInstanceName="TotalCarts" DisplayFormatString="#.00" />
                                <EditFormSettings Visible="False"></EditFormSettings>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="LocationUnloadedID" VisibleIndex="31" EditFormSettings-Visible="True" Caption="Location Unloaded" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesComboBox DataSourceID="dsLocations" TextField="LocationAbbr" ValueField="LocationID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" />						            
                                 <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="BaggerID" VisibleIndex="41" Caption="Bagger" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesComboBox DataSourceID="dsBagger" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" IncrementalFilteringMode="StartsWith" >
                                </PropertiesComboBox>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="CntPutOutsBagger" Caption="Bagger Put Outs" VisibleIndex="42" ReadOnly="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" >
                                <PropertiesTextEdit ClientInstanceName="CntPickupsDriver" DisplayFormatString="#,##0" />
							</dx:GridViewDataTextColumn>  
                            <dx:GridViewDataColumn FieldName="CntBag" Width="1" VisibleIndex="51" />
                        </Columns>
                        <TotalSummary>
                            <dx:ASPxSummaryItem FieldName="DriverName" ShowInGroupFooterColumn="DriverName" SummaryType="Count" />
                            <dx:ASPxSummaryItem FieldName="CntPickupsDriver" ShowInGroupFooterColumn="CntPickupsDriver" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="CntPickupsAddresses" ShowInGroupFooterColumn="CntPickupsAddresses" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="SoftCarts" ShowInGroupFooterColumn="SoftCarts" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="HardCarts" ShowInGroupFooterColumn="HardCarts" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="TotalCarts" ShowInGroupFooterColumn="TotalCarts" SummaryType="Sum" />
                        </TotalSummary>
                        <SettingsEditing EditFormColumnCount="4" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="100" />
                        <SettingsEditing Mode="Batch" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                        <SettingsCommandButton>
                            <DeleteButton Text="Del"/>
                        </SettingsCommandButton>
                   </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsRouteReport" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        UpdateCommand="spDailyRouteReport_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="spDriverAssignments_Delete"
                        DeleteCommandType="StoredProcedure">
                        <UpdateParameters>
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                            <asp:Parameter Name="PickupScheduleID" Type="Int32" />
                            <asp:Parameter Name="DriverID" Type="Int32" />
                            <asp:Parameter Name="SectionID" Type="Int32" />
                            <asp:Parameter Name="CntPickupsDriver" Type="Int32" />
                            <asp:Parameter Name="SoftCarts" Type="Double" />
                            <asp:Parameter Name="HardCarts" Type="Double" />
							<asp:Parameter Name="LocationUnloadedID" Type="Int32" />
                            <asp:Parameter Name="BaggerID" Type="Int32" />
                            <asp:Parameter Name="CntPutOutsBagger" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                        </DeleteParameters> 
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDriver" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsBagger" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsLocations" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="hfRouteReportSelectCommand" runat="server" Value=""></asp:HiddenField>
                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
            </tr>
        </table>
      
    </div>
    </form>
</body>
</html>
