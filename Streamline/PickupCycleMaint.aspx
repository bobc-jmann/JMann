<%@ Page Title="Pickup Cycle Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="PickupCycleMaint.aspx.vb" Inherits="PickupCycleMaint" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html>
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
    <script type="text/javascript">
    	function OnChanging(s, e) {
    		e.reloadContentOnCallback = true;
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Pickup Cycle Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table id="tblPickupCycles" runat="server" style="visibility: visible">
            <tr>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckShowInactivePickupCycles" Text="Show Inactive Pickup Cycles" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="5">
                    <dx:ASPxGridView ID="grdPickupCycles" KeyFieldName="PickupCycleID" runat="server" DataSourceID="dsPickupCycles" 
                            Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True"
							OnRowValidating="grdPickupCycles_RowValidating">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Width="40" ShowEditButton="true"/>
                            <dx:GridViewDataColumn FieldName="PickupCycleID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="Active" VisibleIndex="1" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Width="60" />
                            <dx:GridViewDataColumn FieldName="PickupCycleAbbr" VisibleIndex="2" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="PickupCycleDesc" VisibleIndex="3" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataComboBoxColumn FieldName="PermitID" VisibleIndex="4" Caption="Charity (Permit)" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" >
                                <PropertiesComboBox DataSourceID="dsPermits" TextField="Charity-Permit" ValueField="PermitID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="PickupCycleTemplateID" VisibleIndex="5" Caption="Template" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" >
                                <PropertiesComboBox DataSourceID="dsPickupCycleTemplates" TextField="PickupCycleTemplateCode" ValueField="PickupCycleTemplateID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="LastWeekScheduled" VisibleIndex="6" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="LastDayScheduled" VisibleIndex="7" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="CardBagCode" VisibleIndex="8" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" />
							<dx:GridViewDataDateColumn FieldName="InitialLastPickupDate" Caption="Initial Last Pickup Date">
								<PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" />
							</dx:GridViewDataDateColumn>
                        </Columns>
                        <SettingsEditing EditFormColumnCount="4"/>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="100">
                        </SettingsPager>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <Templates>
                            <DetailRow>
                                <div style="padding: 3px 3px 2px 3px">
                                    <dx:ASPxPageControl runat="server" ID="pageControl2" Width="30%" EnableCallBacks="true">
                                        <TabPages>
                                            <dx:TabPage Text="Regions" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl2" runat="server">
														<dx:ASPxGridView ID="grdPickupCycleDriverLocations" KeyFieldName="PickupCycleDriverLocationID" 
																runat="server" DataSourceID="dsPickupCycleDriverLocations" Width="30%" 
																OnBeforePerformDataSelect="grdPickupCycles_DataSelect" SettingsBehavior-ConfirmDelete="True"
																OnRowValidating="grdPickupCycleDriverLocations_RowValidating"
																OnRowDeleting="grdPickupCycleDriverLocations_RowDeleting">
															<ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
															<Columns>
                                                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Caption=" " ShowEditButton="true">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete"/>
                                                                    </CustomButtons>
                                                                </dx:GridViewCommandColumn>
																<dx:GridViewDataColumn FieldName="PickupCycleDriverLocationID" Visible="False" /> 
																<dx:GridViewDataComboBoxColumn FieldName="RegionID" VisibleIndex="1" Caption="Region" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" >
																	<PropertiesComboBox DataSourceID="dsRegions" TextField="RegionCode" ValueField="RegionID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
																	</PropertiesComboBox>
																</dx:GridViewDataComboBoxColumn>
																<dx:GridViewDataCheckColumn FieldName="PrimaryRegion" VisibleIndex="2" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" />
															</Columns>  
														</dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage Text="Dates to Skip" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl1" runat="server">
														<dx:ASPxGridView ID="grdPickupCycleDatesToSkip" KeyFieldName="PickupDatesToSkipID" 
																runat="server" DataSourceID="dsPickupCycleDatesToSkip" Width="30%" 
																OnBeforePerformDataSelect="grdPickupCycles_DataSelect" SettingsBehavior-ConfirmDelete="True">
															<Columns>
                                                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Caption=" " ShowEditButton="true" ShowDeleteButton="true"/>
																<dx:GridViewDataColumn FieldName="PickupDatesToSkipID" Visible="False" /> 
																<dx:GridViewDataColumn FieldName="PickupCycleID" Visible="False" /> 
																<dx:GridViewDataDateColumn FieldName="DateToSkip" Caption="Dates to Skip">
																	<PropertiesDateEdit DisplayFormatString="MM/dd/yyyy" />
																</dx:GridViewDataDateColumn>
															</Columns>  
														</dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                        </TabPages>
                                        <ClientSideEvents ActiveTabChanging="OnChanging" />
                                    </dx:ASPxPageControl>
                                </div>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsPickupCycles" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        InsertCommandType="StoredProcedure"
                        InsertCommand="spPickupCycles_Insert"
                        UpdateCommandType="StoredProcedure"
                        UpdateCommand="spPickupCycles_Update"
                        DeleteCommandType="StoredProcedure"
                        DeleteCommand="spPickupCycles_Delete">
                          <UpdateParameters>
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="PickupCycleID" Type="Int32" />
                            <asp:Parameter Name="PickupCycleAbbr" Type="String" />
                            <asp:Parameter Name="PickupCycleDesc" Type="String" />
                            <asp:Parameter Name="PermitID" Type="Int32" />
                            <asp:Parameter Name="PickupCycleTemplateID" Type="Int32" />
                            <asp:Parameter Name="LastWeekScheduled" Type="Int32" />
                            <asp:Parameter Name="LastDayScheduled" Type="Int32" />
                            <asp:Parameter Name="CardBagCode" Type="String" />
                            <asp:Parameter Name="InitialLastPickupDate" Type="DateTime" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="PickupCycleAbbr" Type="String" />
                            <asp:Parameter Name="PickupCycleDesc" Type="String" />
                            <asp:Parameter Name="PermitID" Type="Int32" />
                            <asp:Parameter Name="PickupCycleTemplateID" Type="Int32" />
                            <asp:Parameter Name="LastWeekScheduled" Type="Int32" />
                            <asp:Parameter Name="LastDayScheduled" Type="Int32" />
                            <asp:Parameter Name="CardBagCode" Type="String" />
                            <asp:Parameter Name="InitialLastPickupDate" Type="DateTime" />
                        </InsertParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="PickupCycleID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPickupCycleDatesToSkip" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PickupDatesToSkipID, PickupCycleID, DateToSkip
                            FROM tblPickupCycleDatesToSkip
                            WHERE PickupCycleID = @PickupCycleID
                            ORDER BY DateToSkip"
                        InsertCommand="INSERT INTO tblPickupCycleDatesToSkip (PickupCycleID, DateToSkip) VALUES (@PickupCycleID, @DateToSkip)"
                        UpdateCommand="UPDATE tblPickupCycleDatesToSkip SET DateToSkip = @DateToSkip WHERE PickupDatesToSkipID = @PickupDatesToSkipID"
                        DeleteCommand="DELETE FROM tblPickupCycleDatesToSkip WHERE PickupDatesToSkipID = @PickupDatesToSkipID">
						<SelectParameters>
                            <asp:SessionParameter Name="PickupCycleID" SessionField="PickupCycleID" Type="Int32" />
						</SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PickupDatesToSkipID" Type="Int32" />
 							<asp:Parameter Name="DateToSkip" Type="DateTime" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="PickupCycleID" SessionField="PickupCycleID" Type="Int32" />
							<asp:Parameter Name="DateToSkip" Type="DateTime" />
                        </InsertParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="PickupDatesToSkipID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPermits" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT P.PermitID, C.CharityAbbr + ' (' + PermitNbr + ')' AS [Charity-Permit]
                            FROM tblPermits AS P
                            INNER JOIN tblCharities AS C ON C.CharityID = P.CharityID
							WHERE P.Active = 1
                            ORDER BY C.CharityAbbr, P.PermitNbr">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPickupCycleTemplates" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [PickupCycleTemplateID], [PickupCycleTemplateCode] FROM [tblPickupCycleTemplates] ORDER BY [PickupCycleTemplateCode]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRegions" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPickupCycleDriverLocations" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [PickupCycleDriverLocationID], [PickupCycleID], R.[RegionID], [PrimaryRegion] FROM [tblPickupCycleDriverLocations] AS PCDL INNER JOIN [tlkRegions] AS R ON PCDL.RegionID = R.RegionID WHERE [PickupCycleID] = @PickupCycleID ORDER BY R.RegionCode"
                        UpdateCommandType="StoredProcedure"
                        UpdateCommand="spPickupCycleDriverLocations_Update"
                        InsertCommandType="StoredProcedure"
                        InsertCommand="spPickupCycleDriverLocations_Insert"
                        DeleteCommand="DELETE FROM [tblPickupCycleDriverLocations] WHERE [PickupCycleDriverLocationID] = @PickupCycleDriverLocationID">
                        <SelectParameters>
                            <asp:SessionParameter Name="PickupCycleID" SessionField="PickupCycleID" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PickupCycleDriverLocationID" Type="Int32" />
                            <asp:SessionParameter Name="PickupCycleID" SessionField="PickupCycleID" Type="Int32" />
                            <asp:Parameter Name="RegionID" Type="Int32" />
                            <asp:Parameter Name="PrimaryRegion" Type="Boolean" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="PickupCycleID" SessionField="PickupCycleID" Type="Int32" />
                            <asp:Parameter Name="RegionID" Type="Int32" />
                            <asp:Parameter Name="PrimaryRegion" Type="Boolean" />
                        </InsertParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="PickpCycleDriverLocationID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
