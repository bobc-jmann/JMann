<%@ Page Title="Menu Group Maintenance" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ZipGroupsMaint.aspx.vb" Inherits="ZipGroupsMaint" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Zip Groups Maintenance" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
	<div class="explanation" style="font-family: 'Times New Roman'; font-size: medium;" >
		&nbsp;&nbsp;&nbsp;&nbsp;1. Zips in _DAILY will get scheduled every day (except Holidays).<br />
		&nbsp;&nbsp;&nbsp;&nbsp;2. Zips in _INACTIVE will not get scheduled unless there is a Pickup Schedule in that zip on that day.<br />
		&nbsp;&nbsp;&nbsp;&nbsp;3. Zips in other Zip Groups will get scheduled on the weekdays selected (except Holidays)<br />
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;and when there is a Pickup Schedule in any zip in that group on that day.
	</div>
    <br />
    <div id="divMain" class="specials" runat="server">
		<dx:ASPxHiddenField runat="server" ID="hfDetailGrid" />
        <table id="tblMain" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 60%">
                    <dx:ASPxGridView ID="grdMain" KeyFieldName="ZipGroupID" runat="server" DataSourceID="dsZipGroups" Width="100%" 
							EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True"
							OnCommandButtonInitialize="grdMain_CommandButtonInitialize">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="100" ShowNewButtonInHeader="True" ShowDeleteButton="true" ShowEditButton="true"/>
                            <dx:GridViewDataColumn FieldName="ZipGroupID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="ZipGroup" Width="150" VisibleIndex="1" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" EditFormSettings-ColumnSpan="5" />
                            <dx:GridViewDataCheckColumn FieldName="ServiceMonday" Caption="Monday Service" VisibleIndex="2" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormCaptionStyle-Wrap="True" />
                            <dx:GridViewDataCheckColumn FieldName="ServiceTuesday" Caption="Tuesday Service" VisibleIndex="3" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormCaptionStyle-Wrap="True" />
                            <dx:GridViewDataCheckColumn FieldName="ServiceWednesday" Caption="Wednesday Service" VisibleIndex="4" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormCaptionStyle-Wrap="True" />
                            <dx:GridViewDataCheckColumn FieldName="ServiceThursday" Caption="Thursday Service" VisibleIndex="5" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormCaptionStyle-Wrap="True" />
                            <dx:GridViewDataCheckColumn FieldName="ServiceFriday" Caption="Friday Service" VisibleIndex="6" EditFormSettings-Visible="True" EditFormSettings-VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" EditFormCaptionStyle-Wrap="True" />
                        </Columns>
                        <SettingsEditing EditFormColumnCount="5"/>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="100">
                        </SettingsPager>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="true" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="grdDetail" KeyFieldName="ZipGroupDetailID" runat="server" 
									DataSourceID="dsZipGroupDetail" Width="50%" 
									OnBeforePerformDataSelect="grdMain_DataSelect"
									OnLoad="grdDetail_Load"
									OnCustomButtonInitialize="grdDetail_CustomButtonInitialize"
									OnCustomButtonCallback="grdDetail_CustomButtonCallback">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0" Caption=" ">
											<CustomButtons>
												<dx:GridViewCommandColumnCustomButton ID="cbMoveToDaily" Text="Move to _DAILY" Visibility="AllDataRows" />
												<dx:GridViewCommandColumnCustomButton ID="cbMoveToInactive" Text="Move to _INACTIVE" Visibility="AllDataRows" />
											</CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataColumn FieldName="ZipGroupDetailID" Visible="False" /> 
                                        <dx:GridViewDataColumn FieldName="Zip5" VisibleIndex="1" Caption="Zip5" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" /> 
                                    </Columns>  
                                    <SettingsPager PageSize="1000" />
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsZipGroups" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="spZipGroups_SelectAll"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="spZipGroups_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="spZipGroups_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="spZipGroups_Delete"
                        DeleteCommandType="StoredProcedure">
                        <InsertParameters>
                            <asp:Parameter Name="ZipGroup" Type="String" />
                            <asp:Parameter Name="ServiceMonday" Type="Boolean" />
                            <asp:Parameter Name="ServiceTuesday" Type="Boolean" />
                            <asp:Parameter Name="ServiceWednesday" Type="Boolean" />
                            <asp:Parameter Name="ServiceThursday" Type="Boolean" />
                            <asp:Parameter Name="ServiceFriday" Type="Boolean" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ZipGroupID" Type="Int32" />
                            <asp:Parameter Name="ZipGroup" Type="String" />
                            <asp:Parameter Name="ServiceMonday" Type="Boolean" />
                            <asp:Parameter Name="ServiceTuesday" Type="Boolean" />
                            <asp:Parameter Name="ServiceWednesday" Type="Boolean" />
                            <asp:Parameter Name="ServiceThursday" Type="Boolean" />
                            <asp:Parameter Name="ServiceFriday" Type="Boolean" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="ZipGroupID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsZipGroupDetail" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="spZipGroupDetail_SelectGroup"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="spZipGroupDetail_Insert"
                        InsertCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="ZipGroupID" SessionField="MasterID" Type="Int32" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:SessionParameter Name="ZipGroupID" SessionField="MasterID" Type="Int32" />
                            <asp:Parameter Name="Zip5" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="ZipGroupDetailID" Type="Int32" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                </td>
                <td style="width: 40%"></td>
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
