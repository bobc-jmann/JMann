<%@ Page Title="Pickup Cycle Template Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="PickupCycleTemplateMaint.aspx.vb" Inherits="PickupCycleTemplateMaint" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Pickup Cycle Template Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">
                    <dx:ASPxGridView ID="grdMain" KeyFieldName="PickupCycleTemplateID" runat="server" DataSourceID="dsPickupCycleTemplates" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Width="40" ShowEditButton="true"/>
                            <dx:GridViewDataColumn FieldName="PickupCycleTemplateID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="PickupCycleTemplateCode" VisibleIndex="1" Caption="Code" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="PickupCycleTemplateDesc" VisibleIndex="2" Caption="Description" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="DefaultDaysToSchedule" VisibleIndex="3" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataComboBoxColumn FieldName="ScheduleTypeID" VisibleIndex="4" Caption="Schedule Type" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" >
                                <PropertiesComboBox DataSourceID="dsScheduleType" TextField="ScheduleType" ValueField="ScheduleTypeID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="NumberOfWeeks" Caption="Weeks in Template" VisibleIndex="6" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataCheckColumn FieldName="Active" VisibleIndex="11" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataCheckColumn FieldName="Recycle" VisibleIndex="12" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="12" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="RecycleLookBackYears" Visible="false" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="21" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="RecycleSizeChangePercent" Visible="false" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="22" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataColumn FieldName="RecycleMinDonorPercent" Visible="false" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="23" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                            <dx:GridViewDataTextColumn FieldName="Mail" VisibleIndex="21" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Postcards" VisibleIndex="22" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Current" VisibleIndex="23" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Backup" VisibleIndex="24" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Other" VisibleIndex="25" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesTextEdit DisplayFormatString="#,##0" />               
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn FieldName="Notes" Visible="false" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="20" EditFormSettings-ColumnSpan="4" HeaderStyle-HorizontalAlign="Center" />
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
                                    <dx:ASPxPageControl runat="server" ID="pageControl2" Width="60%" EnableCallBacks="true">
                                        <TabPages>
                                            <dx:TabPage Text="Template Detail" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl2" runat="server">
														<dx:ASPxGridView ID="grdDetail" KeyFieldName="PickupCycleTemplatesDetailID" runat="server" DataSourceID="dsPickupCycleTemplatesDetail" Width="100%" OnBeforePerformDataSelect="grdMain_DataSelect" SettingsBehavior-ConfirmDelete="True">
															<Columns>
                                                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Caption=" " ShowEditButton="true" ShowDeleteButton="true"/>
																<dx:GridViewDataColumn FieldName="PickupCycleTemplatesDetailID" Visible="False" /> 
																<dx:GridViewDataComboBoxColumn FieldName="RouteID" VisibleIndex="0" Caption="Route" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="0" HeaderStyle-HorizontalAlign="Center" >
																	<PropertiesComboBox DataSourceID="dsRoutes" TextField="RouteCode" ValueField="RouteID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" />
																</dx:GridViewDataComboBoxColumn>
																<dx:GridViewDataComboBoxColumn FieldName="CharityID" VisibleIndex="1" Caption="Charity" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" >
																	<PropertiesComboBox DataSourceID="dsCharities" TextField="CharityAbbr" ValueField="CharityID" ValueType="System.Int32" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" />
																</dx:GridViewDataComboBoxColumn>
																<dx:GridViewDataColumn FieldName="PickupCycleWeek" VisibleIndex="2" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
																<dx:GridViewDataColumn FieldName="PickupCycleDay" VisibleIndex="3" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
																<dx:GridViewDataTextColumn FieldName="Mail" VisibleIndex="3" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
																	<PropertiesTextEdit DisplayFormatString="#,##0" />
																</dx:GridViewDataTextColumn>
																<dx:GridViewDataTextColumn FieldName="Postcards" VisibleIndex="4" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
																	<PropertiesTextEdit DisplayFormatString="#,##0" />               
																</dx:GridViewDataTextColumn>
																<dx:GridViewDataTextColumn FieldName="Current" VisibleIndex="5" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
																	<PropertiesTextEdit DisplayFormatString="#,##0" />               
																</dx:GridViewDataTextColumn>
																<dx:GridViewDataTextColumn FieldName="Backup" VisibleIndex="6" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
																	<PropertiesTextEdit DisplayFormatString="#,##0" />               
																</dx:GridViewDataTextColumn>
																<dx:GridViewDataTextColumn FieldName="Other" VisibleIndex="7" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
																	<PropertiesTextEdit DisplayFormatString="#,##0" />               
																</dx:GridViewDataTextColumn>
															</Columns>  
															<Settings ShowFooter="True" />
															<TotalSummary>
																<dx:ASPxSummaryItem FieldName="Mail" SummaryType="Sum" DisplayFormat="{0:N0}" />
																<dx:ASPxSummaryItem FieldName="Postcards" SummaryType="Sum" DisplayFormat="{0:N0}" />
																<dx:ASPxSummaryItem FieldName="Current" SummaryType="Sum" DisplayFormat="{0:N0}" />
																<dx:ASPxSummaryItem FieldName="Backup" SummaryType="Sum" DisplayFormat="{0:N0}" />
																<dx:ASPxSummaryItem FieldName="Other" SummaryType="Sum" DisplayFormat="{0:N0}" />
															</TotalSummary>
															<SettingsEditing EditFormColumnCount="4"/>
														</dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage Text="Pickup Cycles" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl3" runat="server">
                                                        <dx:ASPxGridView ID="grdPickupCycles" runat="server" 
                                                            DataSourceID="dsPickupCycles" KeyFieldName="PickupCycleID" Width="40%"
                                                            OnBeforePerformDataSelect="grdMain_DataSelect">
                                                            <Columns>
                                                                <dx:GridViewDataColumn FieldName="PickupCycleAbbr" VisibleIndex="1" Caption="Pickup Cycle" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" /> 
                                                                <dx:GridViewDataCheckColumn FieldName="Active" VisibleIndex="2" Caption="Active" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                                            </Columns>
                                                            <SettingsPager PageSize="100" />
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

                    <asp:SqlDataSource ID="dsPickupCycleTemplates" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PickupCycleTemplateID, PickupCycleTemplateCode, PickupCycleTemplateDesc, DefaultDaysToSchedule, ScheduleTypeID, NumberOfWeeks, Notes, Active, Recycle, RecycleLookBackYears, RecycleSizeChangePercent, RecycleMinDonorPercent, CntMail AS Mail, CntPostcard AS Postcards, CntCurrent AS [Current], CntBackup AS [Backup], CntOther AS Other FROM tblPickupCycleTemplates ORDER BY PickupCycleTemplateCode"
                        InsertCommandType="StoredProcedure"
                        InsertCommand="spPickupCycleTemplates_Insert"
                        UpdateCommandType="StoredProcedure"
                        UpdateCommand="spPickupCycleTemplates_Update"
                        DeleteCommandType="StoredProcedure"
                        DeleteCommand="spPickupCycleTemplates_Delete">
                          <UpdateParameters>
                            <asp:Parameter Name="PickupCycleTemplateID" Type="Int32" />
                            <asp:Parameter Name="PickupCycleTemplateCode" Type="String" />
                            <asp:Parameter Name="PickupCycleTemplateDesc" Type="String" />
                            <asp:Parameter Name="DefaultDaysToSchedule" Type="Int32" />
                            <asp:Parameter Name="ScheduleTypeID" Type="Int32" />
                            <asp:Parameter Name="NumberOfWeeks" Type="Int32" />
                            <asp:Parameter Name="Notes" Type="String" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="Recycle" Type="Boolean" />
                            <asp:Parameter Name="RecycleLookBackYears" Type="Int32" />
                            <asp:Parameter Name="RecycleSizeChangePercent" Type="Int32" />
                            <asp:Parameter Name="RecycleMinDonorPercent" Type="Int32" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="PickupCycleTemplateCode" Type="String" />
                            <asp:Parameter Name="PickupCycleTemplateDesc" Type="String" />
                            <asp:Parameter Name="DefaultDaysToSchedule" Type="Int32" />
                            <asp:Parameter Name="ScheduleTypeID" Type="Int32" />
                            <asp:Parameter Name="NumberOfWeeks" Type="Int32" />
                            <asp:Parameter Name="Notes" Type="String" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="Recycle" Type="Boolean" />
                            <asp:Parameter Name="RecycleLookBackYears" Type="Int32" />
                            <asp:Parameter Name="RecycleSizeChangePercent" Type="Int32" />
                            <asp:Parameter Name="RecycleMinDonorPercent" Type="Int32" />
                       </InsertParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="PickupCycleTemplateID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsScheduleType" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT ScheduleTypeID, ScheduleType FROM tlkScheduleTypes ORDER BY ScheduleType">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPickupCycleTemplatesDetail" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PickupCycleTemplatesDetailID, PCTD.RouteID, R.RouteCode, CharityID, PickupCycleWeek, PickupCycleDay, CntMail AS Mail, CntPostcard AS Postcards, CntCurrent AS [Current], CntBackup AS [Backup], CntOther AS Other FROM tblPickupCycleTemplatesDetail AS PCTD INNER JOIN tblRoutes AS R ON R.RouteID = PCTD.RouteID WHERE PickupCycleTemplateID = @PickupCycleTemplateID ORDER BY PickupCycleWeek, PickupCycleDay, R.RouteCode"
                        UpdateCommandType="StoredProcedure"
                        UpdateCommand="spPickupCycleTemplatesDetail_Update"
                        InsertCommandType="StoredProcedure"
                        InsertCommand="spPickupCycleTemplatesDetail_Insert"
                        DeleteCommand="DELETE FROM tblPickupCycleTemplatesDetail WHERE PickupCycleTemplatesDetailID = @PickupCycleTemplatesDetailID">
                        <SelectParameters>
                            <asp:SessionParameter Name="PickupCycleTemplateID" SessionField="PickupCycleTemplateID" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PickupCycleTemplatesDetailID" Type="Int32" />
                            <asp:SessionParameter Name="PickupCycleTemplateID" SessionField="PickupCycleTemplateID" Type="Int32" />
                            <asp:Parameter Name="RouteID" Type="Int32" />
                            <asp:Parameter Name="CharityID" Type="Int32" />
                            <asp:Parameter Name="PickupCycleWeek" Type="Int32" />
                            <asp:Parameter Name="PickupCycleDay" Type="Int32" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="PickupCycleTemplateID" SessionField="PickupCycleTemplateID" Type="Int32" />
                            <asp:Parameter Name="RouteID" Type="Int32" />
                            <asp:Parameter Name="CharityID" Type="Int32" />
                            <asp:Parameter Name="PickupCycleWeek" Type="Int32" />
                            <asp:Parameter Name="PickupCycleDay" Type="Int32" />
                        </InsertParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="PickupCycleTemplateDetailID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsRoutes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT RouteID, RouteCode FROM tblRoutes WHERE Active = 1 ORDER BY RouteCode">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsCharities" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT CharityID, CharityAbbr FROM tblCharities WHERE Active = 1 ORDER BY CharityAbbr">
                    </asp:SqlDataSource>
                   <asp:SqlDataSource ID="dsPickupCycles" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PickupCycleID, PickupCycleAbbr, Active FROM tblPickupCycles WHERE PickupCycleTemplateID = @PickupCycleTemplateID">
                        <SelectParameters>
                            <asp:SessionParameter Name="PickupCycleTemplateID" SessionField="PickupCycleTemplateID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
