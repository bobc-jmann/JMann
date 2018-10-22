<%@ Page Title="Route-Section Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="RouteSectionMaint.aspx.vb" Inherits="RouteSectionMaint" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
        #tblAddressParameters tr:nth-child(odd) { background-color: #E0E0E0; border-color: #E0E0E0; border: 0px; margin: 0px; padding: 0px; border-collapse: collapse }
        #tblAddressParameters tr:nth-child(even) { background-color: #F0F0F0; border-color: #F0F0F0; Border: 0px; margin: 0px; padding: 0px;  border-collapse: collapse }
	    .parm1 { width: 40%; text-align: right; font-weight:bold }
	    .parm2 { width: 40%; text-align: right; font-weight:bold }
	    .parm3 { width: 20%; text-align: right; font-weight:bold }	    }
	</style> 
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Route-Section Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
         <table width="70%">
            <tr>
                <td style="width: 20%"></td>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckShowOnlyActiveRoutes" Text="Show Only Active Routes" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%"></td>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckShowMagRoutes" Text="Show Only MAG Routes" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%"></td>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckShowMapRoutes" Text="Show Only MAP Routes" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%"></td>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckShowOnlyCurrentSections" Text="Show Only Current Sections" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
        </table>
        <table id="tblRouteSections" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 90%">
                    <dx:ASPxGridView ID="grdRoutes" KeyFieldName="RouteID" runat="server" DataSourceID="dsRoutes" Width="70%" 
						EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True" OnInitNewRow="grdRoutes_InitNewRow">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" ShowNewButtonInHeader="true" Width="40" 
								ShowEditButton="true" ShowClearFilterButton="True" />
                            <dx:GridViewDataColumn FieldName="RouteID" Visible="false" /> 
                            <dx:GridViewDataCheckColumn FieldName="Active" VisibleIndex="1" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Width="60" />
                            <dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="2" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="RouteDesc" VisibleIndex="3" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataDateColumn FieldName="MapLastUpdated" VisibleIndex="4" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="4"  HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="Notes" VisibleIndex="6" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataCheckColumn FieldName="UncommittedChanges" VisibleIndex="11" EditFormSettings-Visible="False" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="80" />
                        </Columns>
                        <SettingsEditing EditFormColumnCount="3"/>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="20">
                        </SettingsPager>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="False" ShowFooter="True" />
                        <Templates>
                            <DetailRow>
                                <div style="padding: 3px 3px 2px 3px">
                                    <dx:ASPxPageControl runat="server" ID="pageControl2" Width="60%" EnableCallBacks="true">
                                        <TabPages>
                                            <dx:TabPage Text="Sections" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl2" runat="server">
														<dx:ASPxButton ID="ResetAllSections" runat="server" Text="Remove MAG & MAP from All Sections" OnClick="ResetAllSections_Click" />
														<dx:ASPxButton ID="MapAllSections" runat="server" Text="MAP All Sections" OnClick="MapAllSections_Click" />
														<dx:ASPxGridView ID="grdSections" KeyFieldName="SectionID" runat="server" DataSourceID="dsSections" 
															Width="100%" OnBeforePerformDataSelect="grdRoutes_DataSelect" SettingsBehavior-ConfirmDelete="True"
															OnInit="grdSections_Init" OnRowValidating="grdSections_RowValidating"
															OnInitNewRow="grdSections_InitNewRow">
															<Columns>
                                                                <dx:GridViewCommandColumn VisibleIndex="0" ShowNewButtonInHeader="true" Width="40" ShowEditButton="true"/>
																<dx:GridViewDataColumn FieldName="RouteID" Visible="False" /> 
																<dx:GridViewDataColumn FieldName="SectionID" Visible="False" /> 
																<dx:GridViewDataCheckColumn FieldName="Active" VisibleIndex="1" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" Width="60" />
																<dx:GridViewDataColumn FieldName="SectionCode" VisibleIndex="2" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" />
																<dx:GridViewDataColumn FieldName="SectionDesc" VisibleIndex="3" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" />
																<dx:GridViewDataCheckColumn FieldName="MAG" VisibleIndex="4" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" />
																<dx:GridViewDataCheckColumn FieldName="MAP" VisibleIndex="5" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" />
																<dx:GridViewDataComboBoxColumn FieldName="QualityRating" VisibleIndex="6" Caption="Quality Rating" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" >
																	<PropertiesComboBox DataSourceID="dsQualityRating" TextField="QualityRating" ValueField="QualityRating" ValueType="System.String" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
																	</PropertiesComboBox>
																</dx:GridViewDataComboBoxColumn>
																<dx:GridViewDataColumn FieldName="alpha" VisibleIndex="7" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" />
																<dx:GridViewDataColumn FieldName="Notes" VisibleIndex="8" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" EditFormSettings-ColumnSpan="3" />
															</Columns>  
															<SettingsEditing EditFormColumnCount="3"/>
														</dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage Text="Templates" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl ID="ContentControl3" runat="server">
                                                        <dx:ASPxGridView ID="grdTemplates" runat="server" 
                                                            DataSourceID="dsTemplates" KeyFieldName="PickupCycleTemplateID" Width="40%"
                                                            OnBeforePerformDataSelect="grdRoutes_DataSelect">
                                                            <Columns>
                                                                <dx:GridViewDataColumn FieldName="PickupCycleTemplateCode" VisibleIndex="1" Caption="Template" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" /> 
                                                                <dx:GridViewDataCheckColumn FieldName="Active" VisibleIndex="2" Caption="Active" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                                                                <dx:GridViewDataColumn FieldName="PickupCycleWeek" VisibleIndex="3" Caption="Week" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" /> 
                                                                <dx:GridViewDataColumn FieldName="PickupCycleDay" VisibleIndex="4" Caption="Day" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Right" /> 
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

                    <asp:SqlDataSource ID="dsRoutes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        InsertCommand="spRoutes_Insert"
						InsertCommandType="StoredProcedure"
                        UpdateCommand="spRoutes_Update"
						UpdateCommandType="StoredProcedure">
                        <UpdateParameters>
                            <asp:Parameter Name="RouteID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="RouteCode" Type="String" />
                            <asp:Parameter Name="RouteDesc" Type="String" />
                             <asp:Parameter Name="Notes" Type="String" />
                            <asp:Parameter Name="MapLastUpdated" Type="DateTime" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
						</UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="RouteCode" Type="String" />
                            <asp:Parameter Name="RouteDesc" Type="String" />
                            <asp:Parameter Name="Notes" Type="String" />
                            <asp:Parameter Name="MapLastUpdated" Type="DateTime" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
                         </InsertParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsSections" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="spSections_Select"
						SelectCommandType="StoredProcedure"
                        UpdateCommand="spSections_Update"
						UpdateCommandType="StoredProcedure"
                        InsertCommand="spSections_Insert"
						InsertCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="RouteID" SessionField="RouteID" Type="Int32" />
                            <asp:SessionParameter Name="ShowOnlyCurrentSections" SessionField="ShowOnlyCurrentSections" Type="Boolean" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="SectionID" Type="Int32" />
                            <asp:SessionParameter Name="RouteID" SessionField="RouteID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                            <asp:Parameter Name="SectionCode" Type="String" />
                            <asp:Parameter Name="SectionDesc" Type="String" />
                            <asp:Parameter Name="Notes" Type="String" />
                            <asp:Parameter Name="MAG" Type="Boolean" />
                            <asp:Parameter Name="MAP" Type="Boolean" />
                            <asp:Parameter Name="alpha" Type="Double" />
                            <asp:Parameter Name="QualityRating" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="RouteID" SessionField="RouteID" Type="Int32" />
                            <asp:Parameter Name="SectionCode" Type="String" />
                            <asp:Parameter Name="SectionDesc" Type="String" />
                            <asp:Parameter Name="Active" Type="Int32" />
                            <asp:Parameter Name="Notes" Type="String" />
                            <asp:Parameter Name="MAG" Type="Boolean" />
                            <asp:Parameter Name="MAP" Type="Boolean" />
                            <asp:Parameter Name="alpha" Type="Double" />
                            <asp:Parameter Name="QualityRating" Type="String" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="String" />
                        </InsertParameters>
                   </asp:SqlDataSource>
                   <asp:SqlDataSource ID="dsTemplates" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PCT.PickupCycleTemplateID, PCT.PickupCycleTemplateCode, PCTD.PickupCycleWeek, PCTD.PickupCycleDay, PCT.Active FROM tblPickupCycleTemplates AS PCT INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.PickupCycleTemplateID = PCT.PickupCycleTemplateID WHERE PCTD.RouteID = @RouteID">
                        <SelectParameters>
                            <asp:SessionParameter Name="RouteID" SessionField="RouteID" Type="Int32" />
                        </SelectParameters>
                   </asp:SqlDataSource>
                   <asp:SqlDataSource ID="dsQualityRating" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT 'High' AS QualityRating, 1 AS SortCode
							UNION SELECT 'Medium' AS QualityRating, 2 AS SortCode
							UNION SELECT 'Low' AS QualityRating, 3 AS SortCode
							UNION SELECT 'Very Low' AS QualityRating, 4 AS SortCode
							ORDER BY SortCode">
                        <SelectParameters>
                            <asp:SessionParameter Name="RouteID" SessionField="RouteID" Type="Int32" />
                        </SelectParameters>
                   </asp:SqlDataSource>
				</td>
                <td style="width: 10%"></td>
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
