<%@ Page Title="Charity Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="CharityMaint.aspx.vb" Inherits="CharityMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Charity Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table id="tblMain" runat="server" style="visibility: visible">
			<tr>
				<td>
					Inserting, Activating, or Deactivating a Charity may take up to a minute 
					because an Address Status record has to be created or deleted for every address.
				</td>
			</tr>
            <tr>
                <td style="width: 100%" colspan="5">
                    <dx:ASPxGridView ID="grdMain" KeyFieldName="CharityID" runat="server" DataSourceID="dsCharities" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" width="40" ShowEditButton="true"/>
                            <dx:GridViewDataColumn FieldName="CharityID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="Active" VisibleIndex="1" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1"  HeaderStyle-HorizontalAlign="Center" Width="60" /> 
                            <dx:GridViewDataColumn FieldName="CharityAbbr" VisibleIndex="2" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="CharityDescription" VisibleIndex="3" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="Email_OK" Caption="OK to Email" VisibleIndex="4" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="4"  HeaderStyle-HorizontalAlign="Center" Width="60" /> 
                        </Columns>
                        <SettingsEditing EditFormColumnCount="4"/>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="100">
                        </SettingsPager>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="grdDetail" OnInitNewRow="grdDetail_InitNewRow" KeyFieldName="PermitID" runat="server" 
                                        DataSourceID="dsPermits" Width="100%" OnBeforePerformDataSelect="grdMain_DataSelect" 
                                        SettingsBehavior-ConfirmDelete="True" OnRowUpdating="grdDetail_RowUpdating" OnRowInserting="grdDetail_RowInserting">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Width="40" ShowEditButton="true"/>
                                        <dx:GridViewDataColumn FieldName="PermitID" Visible="False" /> 
                                        <dx:GridViewDataColumn FieldName="Active" Visible="True" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1">
                                            <EditFormSettings VisibleIndex="1" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="PermitAbbr" Visible="True" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2"> 
                                            <EditFormSettings VisibleIndex="2" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="PermitName" Visible="True" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="3"> 
                                            <EditFormSettings VisibleIndex="3" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="PermitNbr" Visible="True" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="4"> 
                                            <EditFormSettings VisibleIndex="4" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="PermitDesc" Visible="True" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="5"> 
                                            <EditFormSettings VisibleIndex="5" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="GhostNumber" Visible="False" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="6"> 
                                            <EditFormSettings VisibleIndex="6" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="HolderName" Visible="False" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="7"> 
                                            <EditFormSettings VisibleIndex="7" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="HolderAddress" Visible="False" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="8"> 
                                            <EditFormSettings VisibleIndex="8" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="HolderCityStateZIP" Visible="False" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="9"> 
                                            <EditFormSettings VisibleIndex="9" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="MailingAgentName" Visible="False" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="10"> 
                                            <EditFormSettings VisibleIndex="10" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="MailingAgentAddress" Visible="False" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="11"> 
                                            <EditFormSettings VisibleIndex="11" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="MailingAgentCityStateZIP" Visible="False" HeaderStyle-HorizontalAlign="Center" Width="50" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="12"> 
                                            <EditFormSettings VisibleIndex="12" ColumnSpan="1" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataMemoColumn FieldName="PermitNotes" Visible="False" HeaderStyle-HorizontalAlign="Center" Width="50" /> 
                                    </Columns>  
                                    <SettingsEditing EditFormColumnCount="3"/>
                                    <Templates>
                                        <EditForm>
                                            <div style="padding: 4px, 4px, 3px, 4px">
                                                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%">
                                                    <TabPages>
                                                        <dx:TabPage Text="Info" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server" />
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Text="Notes" Visible="true">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <dx:ASPxMemo runat="server" ID="notesEditor" Text='<%# Eval("PermitNotes")%>' Width="100%" Height="93px" />
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                </dx:ASPxPageControl>
                                            </div>
                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server" />
                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server" />
                                            </div>
                                        </EditForm>
                                    </Templates>
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsCharities" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT CharityID, CharityAbbr, CharityDescription, Email_OK, Active FROM tblCharities ORDER BY CharityAbbr"
                        InsertCommandType="StoredProcedure"
                        InsertCommand="spCharities_Insert"
                        UpdateCommandType="StoredProcedure"
                        UpdateCommand="spCharities_Update">
                        <UpdateParameters>
                            <asp:Parameter Name="CharityID" Type="Int32" />
                            <asp:Parameter Name="CharityAbbr" Type="String" />
                            <asp:Parameter Name="CharityDescription" Type="String" />
                            <asp:Parameter Name="Email_OK" Type="Boolean" />
							<asp:Parameter Name="Active" Type="Boolean" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="CharityAbbr" Type="String" />
                            <asp:Parameter Name="CharityDescription" Type="String" />
                            <asp:Parameter Name="Email_OK" Type="Boolean" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                        </InsertParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsPermits" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT PermitID, PermitAbbr, PermitName, PermitNbr, PermitDesc, GhostNumber, HolderName, HolderAddress, HolderCityStateZIP, MailingAgentName, MailingAgentAddress, MailingAgentCityStateZIP, PermitNotes, Active
                            FROM tblPermits 
                            WHERE CharityID = @CharityID 
                            ORDER BY PermitAbbr"
                        UpdateCommandType="StoredProcedure"
                        UpdateCommand="spPermits_Update"
                        InsertCommandType="StoredProcedure"
                        InsertCommand="spPermits_Insert">
                        <SelectParameters>
                            <asp:SessionParameter Name="CharityID" SessionField="CharityID" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="PermitID" Type="Int32" />
                            <asp:SessionParameter Name="CharityID" SessionField="CharityID" Type="Int32" />
                            <asp:Parameter Name="PermitAbbr" Type="String" />
                            <asp:Parameter Name="PermitName" Type="String" />
                            <asp:Parameter Name="PermitNbr" Type="String" />
                            <asp:Parameter Name="PermitDesc" Type="String" />
                            <asp:Parameter Name="GhostNumber" Type="String" />
                            <asp:Parameter Name="HolderName" Type="String" />
                            <asp:Parameter Name="HolderAddress" Type="String" />
                            <asp:Parameter Name="HolderCityStateZIP" Type="String" />
                            <asp:Parameter Name="MailingAgentName" Type="String" />
                            <asp:Parameter Name="MailingAgentAddress" Type="String" />
                            <asp:Parameter Name="MailingAgentCityStateZIP" Type="String" />
                            <asp:Parameter Name="PermitNotes" Type="String" />
                            <asp:Parameter Name="Active" Type="Boolean" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="CharityID" SessionField="CharityID" Type="Int32" />
                            <asp:Parameter Name="PermitAbbr" Type="String" />
                            <asp:Parameter Name="PermitName" Type="String" />
                            <asp:Parameter Name="PermitNbr" Type="String" />
                            <asp:Parameter Name="PermitDesc" Type="String" />
                            <asp:Parameter Name="GhostNumber" Type="String" />
                            <asp:Parameter Name="HolderName" Type="String" />
                            <asp:Parameter Name="HolderAddress" Type="String" />
                            <asp:Parameter Name="HolderCityStateZIP" Type="String" />
                            <asp:Parameter Name="MailingAgentName" Type="String" />
                            <asp:Parameter Name="MailingAgentAddress" Type="String" />
                            <asp:Parameter Name="MailingAgentCityStateZIP" Type="String" />
                            <asp:Parameter Name="PermitNotes" Type="String" />
                            <asp:Parameter Name="Active" Type="Boolean" />
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
