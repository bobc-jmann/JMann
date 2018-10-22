<%@ Page Title="Mail Scheduler" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ScheduleMail.aspx.vb" Inherits="ScheduleMail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
        #tblSectionAddressParameters tr:nth-child(odd) { background-color: #E0E0E0; border-color: #E0E0E0; border: 0px; margin: 0px; padding: 0px; border-collapse: collapse }
        #tblSectionAddressParameters tr:nth-child(even) { background-color: #F0F0F0; border-color: #F0F0F0; Border: 0px; margin: 0px; padding: 0px;  border-collapse: collapse }
	    .parm1 { width: 25%; text-align: right; font-weight:bold }
	    .parm2 { width: 60%; text-align: right; font-weight:bold }
	    .parm3 { width: 15%; text-align: right; font-weight:bold }
	    }
	</style> 
    <script>
        function jsCallHideLoadingPanel() {
            parent.jsHideLoadingPanel();
        }
    </script>
     <script>
        var jResult = true;
        function jsCheckDatesComplete(s, e) {
            v = e.result
            if (v != "OK") {
                if (v.indexOf("must be later than today") > 0) {
                    alert(v)
                    e.processOnServer = false;
                }
                else {
                    rv = confirm(v)
                    if (rv) { cbDeleteAfterCheck.PerformCallback(); }
                }
            }
            else { if (confirm("Confirm deletion?")) { cbDeleteAfterCheck.PerformCallback(); } }
        }
        function jsCheckDatesReschComplete(s, e) {
            v = e.result
            if (v != "OK") {
                if (v.indexOf("must be later than today") > 0) {
                    alert(v)
                    e.processOnServer = false;
                }
                else {
                    rv = confirm(v)
                    if (rv) { cbReschAfterCheck.PerformCallback(); gridMain.Refresh(); }
                }
            }
            else { cbReschAfterCheck.PerformCallback(); gridMain.Refresh(); }
        }
        function jsConfirmDelete(s, e) {
            if (e.result == "0") {
                gridMain.Refresh();
                alert("Schedule deleted.")
            }
            else {
                alert("Cannot delete a job that has been exported and sent to tablets.")
            }
        }
        function jsConfirmResch(s, e) {
            if (e.result == "0") {
                gridMain.Refresh();
                alert("Reschedule completed.")
            }
            else {
                alert("Cannot reschedule a job that has been exported and sent to tablets.")
            }
        }
        function OnGetRowValues(result) {
            var ds = new Date()
            m = ds.getMonth()
            d = ds.getDate()
            y = ds.getFullYear()
            jToday = m + "/" + d + "/" + y
            var dToday = new Date(jToday)
            if (result.length > 2) {
                var ds = new Date(result[0])
                m = ds.getMonth()
                d = ds.getDate()
                y = ds.getFullYear()
                jMail = m + "/" + d + "/" + y
                var dMail = new Date(jMail)
                var ds = new Date(result[1])
                m = ds.getMonth()
                d = ds.getDate()
                y = ds.getFullYear()
                jPick = m + "/" + d + "/" + y
                var dPick = new Date(jPick)
                var ds = new Date(result[2])
                m = ds.getMonth()
                d = ds.getDate()
                y = ds.getFullYear()
                jProd = m + "/" + d + "/" + y
                var dProd = new Date(jProd)
                if (dToday >= dMail) {
                    alert("Mailing date must be earlier than today.");
                    gridMain.processOnServer = false;
                    return;
                }
                if (dToday >= dPick) {
                    alert("Pickup date must be earlier than today");
                    gridMain.processOnServer = false;
                    return;
                }
                if (dToday > dProd) {
                    rv = confirm("Today is past this Pickup Schedule’s Production Week Date. Are you sure you want to Delete?");
                    gridMain.processOnServer = false;
                    return;
                }
            }
            gridMain.processOnServer = confirm('Confirm deletion??');
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Mail Scheduler" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <table cellpadding="3" border=0 align="center" width="95%">
        <tr>
            <td nowrap align="right">Select Pickup Cycle</td>
            <td>
                <dx:ASPxComboBox Width=100 ID="PickupCycleID" ClientInstanceName="PickupCycleID"  ValueField="PickupCycleID" TextField="PickupCycleAbbr" AutoPostBack="true" DataSourceID="sql_PickupCycles" ShowShadow="true" IncrementalFilteringMode="StartsWith"  ValueType="System.Int32" runat="server" ><columns><dx:ListBoxColumn FieldName="PickupCycleID" Visible="false" /><dx:ListBoxColumn FieldName="PickupCycleAbbr" /></columns></dx:ASPxComboBox>
                <asp:SqlDataSource ID="sql_PickupCycles" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>"></asp:SqlDataSource>
            </td>
            <td>
                <dx:ASPxButton runat="server" ID="btnCreateSchedule" Text="Create Schedule"></dx:ASPxButton>
            </td>
            <td><dx:aspxlabel ID="errMsg" ForeColor="Red" Font-Bold="true" runat="server"></dx:aspxlabel></td>
        </tr>
        <tr>
            <td colspan=20 align="center">
                <table id="tblPickup" runat="server" visible="false" cellpadding="3" style="border:1px solid gainsboro">
                    <tr>
                        <td class="editLbl">Pickup Cycle Abbr</td><td align="left"><dx:ASPxLabel ID="PickupCycleAbbr" CssClass="displayLbl" runat="server"></dx:ASPxLabel></td>
                        <td class="editLbl">Last Week Scheduled</td><td align="left"><dx:ASPxLabel ID="LastWeekScheduled" CssClass="displayLbl" runat="server"></dx:ASPxLabel></td>
                    </tr>
                    <tr>
                        <td class="editLbl">Pickup Cycle Desc</td><td align="left"><dx:ASPxLabel ID="PickupCycleDesc" CssClass="displayLbl" runat="server"></dx:ASPxLabel></td>
                        <td class="editLbl">Last Day Scheduled</td><td align="left"><dx:ASPxLabel ID="LastDayScheduled" CssClass="displayLbl" runat="server"></dx:ASPxLabel></td>
                    </tr>
                    <tr>
                        <td class="editLbl">Pickup Cycle Template</td><td align="left"><dx:ASPxLabel ID="PickupCycleTemplateCode" CssClass="displayLbl" runat="server" ></dx:ASPxLabel></td>
                        <td class="editLbl">Default Schedule Type</td><td align="left"><dx:ASPxLabel ID="ScheduleType" CssClass="displayLbl" runat="server" ></dx:ASPxLabel></td>
                    </tr>
                    <tr>
                        <td class="editLbl">Charity Abbr</td><td align="left"><dx:ASPxLabel ID="CharityAbbr" CssClass="displayLbl" runat="server" ></dx:ASPxLabel></td>
                        <td class="editLbl">Permit Abbr</td><td align="left"><dx:ASPxLabel ID="PermitNbr" CssClass="displayLbl" runat="server" ></dx:ASPxLabel></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan=20>
                <asp:SqlDataSource ID="sql_gridMain" SelectCommand="select * from qryScheduler where PickupCycleID=@PickupCycleID AND ScheduleTypeID IN (1, 4, 5) order by PickupDate desc" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>" 
                    UpdateCommand="spScheduleUpdate"
                    UpdateCommandType="StoredProcedure"
                    OnUpdating="On_sql_gridMain_Updating">
                    <selectparameters><asp:ControlParameter ControlID="PickupCycleID" Name="PickupCycleID" PropertyName="Value" Type="Int32" /></selectparameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ScheduleTypeID" type="Int32" />
                        <asp:Parameter Name="MailingDate" type="DateTime" />
                        <asp:Parameter Name="PickupDate" type="DateTime" />
                        <asp:Parameter Name="PermitID" type="Int32" />
                        <asp:Parameter Name="PrintJobCategory" type="String" />
                        <asp:Parameter Name="ApprovedForExport" type="Boolean" />
                        <asp:Parameter Name="Printed" type="Boolean" />
                        <asp:Parameter Name="PickupScheduleID" type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <dx:ASPxGridView KeyFieldName="PickupScheduleID" OnDataBound="vbHideID" ID="gridMain" 
						OnHtmlDataCellPrepared="vbOnHtmlDataCellPrepared" ClientInstanceName="gridMain" 
						runat="server" DataSourceID="sql_gridMain" Width="100%">
                    <SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" />
                    <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                    <Columns>
                      <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="False" /> 
                      <dx:GridViewDataColumn FieldName="PickupCycleID" Visible="False" /> 
                      <dx:GridViewDataColumn FieldName="PickupCycleAbbr" VisibleIndex="1" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataComboBoxColumn FieldName="ScheduleTypeID" VisibleIndex="2" Caption="Schedule Type" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="True">
                            <PropertiesComboBox DataSourceID="dsScheduleTypes" TextField="ScheduleType" ValueField="ScheduleTypeID" ValueType="System.Int32">
                            </PropertiesComboBox>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                      </dx:GridViewDataComboBoxColumn>
                      <dx:GridViewDataColumn FieldName="PickupCycleWeek" VisibleIndex="3" EditFormSettings-Visible="False" Caption="Week"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataColumn FieldName="PickupCycleDay" VisibleIndex="4" EditFormSettings-Visible="False"  Caption="Day" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="5" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                      <dx:GridViewDataColumn FieldName="MailingDate" VisibleIndex="7" EditFormSettings-Visible="True"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                      <dx:GridViewDataColumn FieldName="PickupDate" VisibleIndex="8" EditFormSettings-Visible="True"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                      <dx:GridViewDataColumn FieldName="PermitID" Visible="False" /> 
                      <dx:GridViewDataColumn FieldName="PrintJobCategory" VisibleIndex="9" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Width="100" />
                      <dx:GridViewDataColumn FieldName="ApprovedForExport" VisibleIndex="10" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Caption="Approved for Export" EditFormSettings-CaptionLocation="Default" />
                      <dx:GridViewDataColumn FieldName="Printed" VisibleIndex="13" EditFormSettings-Visible="True"  HeaderStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataTextColumn FieldName="MailCount" VisibleIndex="21" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                        <PropertiesTextEdit DisplayFormatString="#,##0" />               
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="EmailCount" VisibleIndex="22" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                        <PropertiesTextEdit DisplayFormatString="#,##0" />               
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="BagCount" VisibleIndex="23" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                        <PropertiesTextEdit DisplayFormatString="#,##0" />               
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="PostcardCount" VisibleIndex="24" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                        <PropertiesTextEdit DisplayFormatString="#,##0" />               
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="CntMailNR" Caption="Non-Route Mail Count" VisibleIndex="25" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                        <PropertiesTextEdit DisplayFormatString="#,##0" />               
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="CntEmailNR" Caption="Non-Route Email Count" VisibleIndex="26" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                        <PropertiesTextEdit DisplayFormatString="#,##0" />               
                      </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <EditForm>
                            <table cellpadding=3 cellspacing=3>
                                <tr>
                                    <td nowrap class="editLbl">Pickup Cycle Abbr</td>
                                    <td nowrap class="editLbl">Pickup Type</td>
                                    <td nowrap class="editLbl">Week</td>
                                    <td nowrap class="editLbl">Day</td>
                                    <td nowrap class="editLbl">Route Code</td>
                                    <td nowrap class="editLbl">Mailing Date</td>
                                    <td nowrap class="editLbl">Pickup Date</td>
                                    <td nowrap class="editLbl">Approved for Export</td>
                                    <td nowrap class="editLbl">Exported</td>
                                    <td nowrap class="editLbl">Printed</td>
                                </tr>
                                <tr>
                                    <td><dx:ASPxLabel Wrap="False" Visible=false runat="server" ID="PickupScheduleID" Text='<%# Bind("PickupScheduleID")%>'></dx:ASPxLabel><dx:ASPxLabel Wrap="False" runat="server" ID="PickupCycleAbbr" Text='<%# Bind("PickupCycleAbbr")%>'></dx:ASPxLabel></td>
                                    <td><dx:ASPxComboBox DropDownRows=3 ID="ScheduleTypeID" ClientInstanceName="ScheduleTypeID"  Value='<%# Bind("ScheduleTypeID")%>' ValueField="ScheduleTypeID" TextField="ScheduleType" DataSourceID="dsScheduleTypes" ValueType="System.Int32" runat="server" ><columns><dx:ListBoxColumn FieldName="ScheduleTypeID" Visible="false" /><dx:ListBoxColumn FieldName="ScheduleType" Visible="true" /></columns></dx:ASPxComboBox></td>
                                    <td><dx:ASPxLabel Wrap="False" runat="server" ID="ASPxLabel1" Text='<%# Bind("PickupCycleWeek")%>'></dx:ASPxLabel></td>
                                    <td><dx:ASPxLabel Wrap="False" runat="server" ID="ASPxLabel3" Text='<%# Bind("PickupCycleDay")%>'></dx:ASPxLabel></td>
                                    <td><dx:ASPxLabel Wrap="False" runat="server" ID="ASPxLabel4" Text='<%# Bind("RouteCode")%>'></dx:ASPxLabel></td>
                                    <td><dx:ASPxDateEdit ID="MailingDate" ClientInstanceName="MailingDate" Width="100" Value='<%# Bind("MailingDate")%>' EditFormat="Custom" EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" ValidationSettings-RegularExpression-ErrorText="Invalid date" ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" runat="server" ></dx:ASPxDateEdit></td>
                                    <td><dx:ASPxDateEdit ID="PickupDate" ClientInstanceName="PickupDate" Width="100" Value='<%# Bind("PickupDate")%>' EditFormat="Custom" EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" ValidationSettings-RegularExpression-ErrorText="Invalid date" ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" runat="server" ></dx:ASPxDateEdit></td>                                
                                    <td style="text-align:center"><dx:ASPxCheckbox runat="server" ID="ckApprovedForExport" Value='<%# Bind("ApprovedForExport")%>' ></dx:ASPxCheckbox></td>
                                    <td style="text-align:center"><dx:ASPxCheckbox runat="server" ID="ckExported" Value='<%# Bind("Exported")%>' Enabled="False"></dx:ASPxCheckbox></td>
                                    <td style="text-align:center"><dx:ASPxCheckbox runat="server" ID="ckPrinted" Value='<%# Bind("Printed")%>' ></dx:ASPxCheckbox></td>
                                </tr>
                                <tr>
                                    <td colspan=20 align=right>
                                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                                    </td>
                                </tr>
                            </table>
                        </EditForm>
                    </Templates>
                </dx:ASPxGridView>
                <dx:ASPxCallback runat="server" ID="cbCheckDates" ClientInstanceName="cbCheckDates" OnCallback="vbCheckDates"><ClientSideEvents CallbackComplete="jsCheckDatesComplete" /></dx:ASPxCallback>
                <dx:ASPxCallback runat="server" ID="cbDeleteAfterCheck" ClientInstanceName="cbDeleteAfterCheck" OnCallback="vbDeleteAfterCheck"><ClientSideEvents CallbackComplete="jsConfirmDelete" /></dx:ASPxCallback>
                <dx:ASPxCallback runat="server" ID="cbCheckDatesResch" ClientInstanceName="cbCheckDatesResch" OnCallback="vbCheckDates"><ClientSideEvents CallbackComplete="jsCheckDatesReschComplete" /></dx:ASPxCallback>
                <dx:ASPxCallback runat="server" ID="cbReschAfterCheck" ClientInstanceName="cbReschAfterCheck" OnCallback="vbReschAfterCheck"><ClientSideEvents CallbackComplete="jsConfirmResch" /></dx:ASPxCallback>

                <asp:SqlDataSource ID="dsScheduleTypes" 
                    SelectCommand="SELECT ScheduleTypeID, ScheduleType FROM tlkScheduleTypes ORDER BY ScheduleType" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</form>
</body>
</html>