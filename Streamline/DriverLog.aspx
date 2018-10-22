<%@ Page Title="Driver Log" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="DriverLog.aspx.vb" Inherits="DriverLog" %>

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
        var visibleIndex;
        var grid;
        function OnCustomButtonClick(s, e) {
            visibleIndex = e.visibleIndex;
            grid = s;
            s.GetRowValues(e.visibleIndex, "PrimaryRegion", OnGetRowValues);
        }
        function OnGetRowValues(values) {
            if (values) {
                alert("Cannot delete Primary Region");
            }
            else {
                if (confirm("Are you sure you want to delete this Region?")) {
                    grid.DeleteRow(visibleIndex);
                }
            }
        }
    </script>
</head>

<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="glob" runat="server">
        <ClientSideEvents ControlsInitialized="function (s, e) { jsCallHideLoadingPanel(); }" />
    </dx:ASPxGlobalEvents>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Driver Log" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table id="tblSearch" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 20%">Driver Location:
				    <asp:DropDownList ID="ddlDriverLocations" runat="server" ToolTip="Driver Location" Width="100%" AutoPostBack="True" />
 				</td>
                <td style="width: 10%"></td>
                <td style="width: 20%">
                    <dx:ASPxButton runat="server" ID="btnDriverLogReport" Text="Driver Log Report" OnClick="btnDriverLogReport_Click" AutoPostBack="True" Width="90%"></dx:ASPxButton>
                </td>
                <td style="width: 50%"></td>
			</tr>
			<tr>
                <td>Select Driver:
				    <asp:DropDownList ID="ddlDrivers" runat="server" ToolTip="Driver" Width="100%" AutoPostBack="True" />
                </td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
		</table>
	</div>
	<br />
    <div id="divMain" class="specials" runat="server">
        <table id="tblMain" runat="server" style="visibility: visible">
			<tr>
                <td style="width: 50%">
                    <dx:ASPxGridView ID="grdPickupDates" KeyFieldName="PickupDate" runat="server" DataSourceID="dsPickupDates" 
                            Width="100%" EnableRowsCache="false" >
                        <Columns>
                            <dx:GridViewDataColumn FieldName="PickupDate" VisibleIndex="1" HeaderStyle-HorizontalAlign="Left" />
                        </Columns>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="100">
                        </SettingsPager>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="grdDriverLog" KeyFieldName="DriverLogID" 
                                        runat="server" DataSourceID="dsDriverLog" Width="100%" 
                                        OnBeforePerformDataSelect="grdPickupDate_DataSelect" 
										SettingsBehavior-ConfirmDelete="True"
										OnInitNewRow="grdDriverLog_InitNewRow">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Caption=" " Width="60" ShowEditButton="true" ShowDeleteButton="true"/>
                                        <dx:GridViewDataColumn FieldName="DriverLogID" Visible="False" /> 
                                        <dx:GridViewDataColumn FieldName="DriverID" Visible="False" /> 
                                        <dx:GridViewDataColumn FieldName="PickupDate" Visible="False" /> 
                                        <dx:GridViewDataComboBoxColumn FieldName="ActionID" VisibleIndex="1" Caption="Action" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Left" Width="50" >
                                            <PropertiesComboBox DataSourceID="dsDriverActions" TextField="DriverAction" ValueField="DriverActionID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="DriverAssignmentID" VisibleIndex="2" Caption="Route-Section" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2" HeaderStyle-HorizontalAlign="Left" Width="100">
                                            <PropertiesComboBox DataSourceID="dsDriverAssignments" TextField="RouteSection" ValueField="DriverAssignmentID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
										<dx:GridViewDataTimeEditColumn FieldName="StartTime" VisibleIndex="3" EditFormSettings-VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="40" CellStyle-HorizontalAlign="Center">
											<PropertiesTimeEdit EditFormatString="HH:mm" DisplayFormatString="HH:mm">
											</PropertiesTimeEdit>
										</dx:GridViewDataTimeEditColumn>
										<dx:GridViewDataTimeEditColumn FieldName="EndTime" VisibleIndex="4" EditFormSettings-VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="40" CellStyle-HorizontalAlign="Center">
											<PropertiesTimeEdit EditFormatString="HH:mm" DisplayFormatString="HH:mm">
											</PropertiesTimeEdit>
										</dx:GridViewDataTimeEditColumn>
										<dx:GridViewDataColumn FieldName="Comments" VisibleIndex="5" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="5" HeaderStyle-HorizontalAlign="Left" EditFormSettings-ColumnSpan="2" />
                                    </Columns>
                                    <SettingsCommandButton>
                                        <DeleteButton Text="Del"/>
                                    </SettingsCommandButton>  
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" />
						<SettingsEditing EditFormColumnCount="2" />
                </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsPickupDates" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT DISTINCT PickupDate FROM tblDriverAssignments WHERE DriverID = @DriverID AND PickupDate >= @EarliestPickupDate ORDER BY PickupDate DESC">
						<SelectParameters>
							<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
							<asp:ControlParameter name="EarliestPickupDate" controlid="dtEarliestPickupDate" propertyname="Value" />
						</SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDriverLog" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommandType="StoredProcedure"
                        SelectCommand="spDriverLog_Select"
                        InsertCommandType="StoredProcedure"
                        InsertCommand="spDriverLog_Insert"
                        UpdateCommandType="StoredProcedure"
                        UpdateCommand="spDriverLog_Update"
                        DeleteCommandType="StoredProcedure"
                        DeleteCommand="spDriverLog_Delete">
						<SelectParameters>
							<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
                            <asp:SessionParameter Name="PickupDate" SessionField="PickupDate" Type="DateTime" />
						</SelectParameters>
                           <UpdateParameters>
                            <asp:Parameter Name="DriverLogID" Type="Int32" />
							<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
                            <asp:SessionParameter Name="PickupDate" SessionField="PickupDate" Type="DateTime" />
                            <asp:Parameter Name="ActionID" Type="Int32" />
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                            <asp:Parameter Name="StartTime" Type="DateTime" />
                            <asp:Parameter Name="EndTime" Type="DateTime" />
                            <asp:Parameter Name="Comments" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <InsertParameters>
							<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
                            <asp:SessionParameter Name="PickupDate" SessionField="PickupDate" Type="DateTime" />
                            <asp:Parameter Name="ActionID" Type="Int32" />
                            <asp:Parameter Name="DriverAssignmentID" Type="Int32" />
                            <asp:Parameter Name="StartTime" Type="DateTime" />
                            <asp:Parameter Name="EndTime" Type="DateTime" />
                            <asp:Parameter Name="Comments" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="DriverLogID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDriverActions" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT DriverActionID, DriverAction FROM tblDriverActions ORDER BY DriverAction">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDriverAssignments" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT DriverAssignmentID, RouteCode + '-' + SectionCode AS RouteSection FROM tblDriverAssignments WHERE DriverID = @DriverID AND PickupDate = @PickupDate AND SectionID > 0 ORDER BY RouteSection">
						<SelectParameters>
							<asp:ControlParameter name="DriverID" controlid="ddlDrivers" propertyname="SelectedValue" />
                            <asp:SessionParameter Name="PickupDate" SessionField="PickupDate" Type="DateTime" />
						</SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td style="width: 50%"></td>
            </tr>
        </table>     
    </div>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblPickupDateMin" runat="server" Text="Earliest Pickup Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtEarliestPickupDate" ClientInstanceName="dtEarliestPickupDate"
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
    </div>
    </form>
</body>
</html>
