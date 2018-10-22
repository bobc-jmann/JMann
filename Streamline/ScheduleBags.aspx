<%@ Page Title="Bag Scheduler" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ScheduleBags.aspx.vb" Inherits="ScheduleBags" %>

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
                alert(v)
                e.processOnServer = false;
            }
            else { if (confirm("Confirm deletion?")) { cbDeleteAfterCheck.PerformCallback(); } }
        }
        function jsCheckDatesReschComplete(s, e) {
            v = e.result
            if (v != "OK") {
                alert(v)
                e.processOnServer = false;
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
        function jsConfirmSpecialDelete(s, e) {
            if (e.result.length > 20) {
                alert(e.result);
                return;
            }

            if (e.result == "0") {
                gridMain.Refresh();
                alert("Schedule deleted.")
            }
            else {
                alert("Error deleting Pickup Schedule.")
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
        function OnWeekValidation(s, e) {
            var qty = e.value;
            if (!qty)
                return;
            if (qty < 1 || qty > 20) {
                e.isValid = false;
                e.errorText = "Enter a valid week number.";
            }
        }
        function OnDayValidation(s, e) {
            var qty = e.value;
            if (!qty)
                return;
            if (qty < 1 || qty > 5) {
                e.isValid = false;
                e.errorText = "Enter a valid day number between 1 and 5.";
            }
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Bag Scheduler" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <table cellpadding="3" border=0 align="center" width="95%">
        <tr>
            <td nowrap align="right">Select Pickup Cycle:</td>
            <td>
                <dx:ASPxComboBox Width=100 ID="PickupCycleID" ClientInstanceName="PickupCycleID"  ValueField="PickupCycleID" TextField="PickupCycleAbbr" AutoPostBack="true" DataSourceID="sql_PickupCycles" ShowShadow="true" IncrementalFilteringMode="StartsWith"  ValueType="System.Int32" runat="server" ><columns><dx:ListBoxColumn FieldName="PickupCycleID" Visible="false" /><dx:ListBoxColumn FieldName="PickupCycleAbbr" /></columns></dx:ASPxComboBox>
                <asp:SqlDataSource ID="sql_PickupCycles" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>"></asp:SqlDataSource>
            </td>
            <td>
                <dx:ASPxButton runat="server" ID="btnCreateSchedule" Width="150" Text="Create Schedule"></dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton runat="server" ID="btnCreateSchedule2" Width="150" Text="Create Schedule for specific Route" OnClick="btnSchedule2Popup" Wrap="True"></dx:ASPxButton>
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
                <asp:SqlDataSource ID="sql_gridMain" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>" 
                    SelectCommand="SELECT PS.PickupScheduleID, PC.PickupCycleID, PC.PickupCycleAbbr, PS.Week AS PickupCycleWeek, PS.Day AS PickupCycleDay, PS.RouteCode, PS.MailingDate, PS.PickupDate, PS.PermitID, PS.PrintJobCategory, PS.ApprovedForExport, PS.Exported, CASE WHEN ST.ScheduleTypeID = 5 THEN 4 ELSE ST.ScheduleTypeID END AS ScheduleTypeID, dbo.ufnBagSectionCodesAsString(PS.PickupScheduleID) AS Sections, CntMail AS MailCount, CntEmail AS EmailCount, CntBag AS BagCount, CntPostcard AS PostcardCount, CntMailNR, CntEmailNR, CASE WHEN NTBP.PickupScheduleID IS NULL THEN '' ELSE 'NT' END AS NonTabletBagPickup FROM tblPickupSchedule AS PS INNER JOIN tblPickupCycles AS PC ON PS.PickupCycleID = PC.PickupCycleID INNER JOIN tblRoutes AS R ON R.RouteID = PS.RouteID INNER JOIN tlkScheduleTypes AS ST ON PS.ScheduleTypeID = ST.ScheduleTypeID LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleID = PS.PickupScheduleID WHERE PC.PickupCycleID = @PickupCycleID AND LEN(dbo.ufnBagSectionCodesAsString(PS.PickupScheduleID)) > 0 ORDER BY PickupDate DESC"                    
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
                        <asp:Parameter Name="PickupScheduleID" type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <dx:ASPxGridView KeyFieldName="PickupScheduleID" OnDataBound="vbHideID" ID="gridMain"
                        OnHtmlDataCellPrepared="vbOnHtmlDataCellPrepared" ClientInstanceName="gridMain" runat="server" 
                        DataSourceID="sql_gridMain" Width="100%">
                    <SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" />
                    <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                    <Columns>
                      <dx:GridViewDataColumn FieldName="PickupScheduleID" Visible="False" /> 
                      <dx:GridViewDataColumn FieldName="PickupCycleID" Visible="False" /> 
                      <dx:GridViewDataColumn FieldName="NonTabletBagPickup" Caption="NT" Width="30" VisibleIndex="1" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataColumn FieldName="PickupCycleAbbr" VisibleIndex="2" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" CellStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataComboBoxColumn FieldName="ScheduleTypeID" VisibleIndex="3" Caption="Schedule Type" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="True">
                            <PropertiesComboBox DataSourceID="dsScheduleTypes" TextField="ScheduleType" ValueField="ScheduleTypeID" ValueType="System.Int32" />
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                      </dx:GridViewDataComboBoxColumn>
                      <dx:GridViewDataColumn FieldName="PickupCycleWeek" VisibleIndex="4" EditFormSettings-Visible="False" Caption="Week"  CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataColumn FieldName="PickupCycleDay" VisibleIndex="5" EditFormSettings-Visible="False"  Caption="Day" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="6" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataColumn FieldName="Sections" VisibleIndex="7" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" />
                      <dx:GridViewDataColumn FieldName="MailingDate" Visible="False" VisibleIndex="9" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                      <dx:GridViewDataColumn FieldName="PickupDate" VisibleIndex="10" EditFormSettings-Visible="True"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" />
                      <dx:GridViewDataComboBoxColumn FieldName="PermitID" VisibleIndex="11" Caption="Permit Nbr" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Wrap="True">
                            <PropertiesComboBox DataSourceID="dsPermits" TextField="PermitNbr" ValueField="PermitID" ValueType="System.Int32" />
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                      </dx:GridViewDataComboBoxColumn>
                      <dx:GridViewDataColumn FieldName="PrintJobCategory" Visible="False" /> 
                      <dx:GridViewDataColumn FieldName="ApprovedForExport" VisibleIndex="12" EditFormSettings-Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" Caption="Approved for Export" EditFormSettings-CaptionLocation="Default" />
                      <dx:GridViewDataColumn FieldName="Exported" VisibleIndex="13" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" />
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
                      <dx:GridViewDataTextColumn FieldName="CntEmailNR" Caption="Non-Route Email Count" VisibleIndex="25" EditFormSettings-Visible="False"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
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
                                    <td nowrap class="editLbl">Pickup Date</td>
                                    <td nowrap class="editLbl">Permit Nbr</td>
                                    <td nowrap class="editLbl">Approved for Export</td>
                                    <td nowrap class="editLbl">Exported</td>
                                </tr>
                                <tr>
                                    <td><dx:ASPxLabel Wrap="False" Visible=false runat="server" ID="PickupScheduleID" Text='<%# Bind("PickupScheduleID")%>'></dx:ASPxLabel><dx:ASPxLabel Wrap="False" runat="server" ID="PickupCycleAbbr" Text='<%# Bind("PickupCycleAbbr")%>'></dx:ASPxLabel></td>
                                    <td><dx:ASPxComboBox DropDownRows=3 ID="ScheduleTypeID" ClientInstanceName="ScheduleTypeID" Width="70" Value='<%# Bind("ScheduleTypeID")%>' ValueField="ScheduleTypeID" TextField="ScheduleType" DataSourceID="dsScheduleTypes" ValueType="System.Int32" runat="server" ><columns><dx:ListBoxColumn FieldName="ScheduleTypeID" Visible="false" /><dx:ListBoxColumn FieldName="ScheduleType" Visible="true" /></columns></dx:ASPxComboBox></td>
                                    <td><dx:ASPxLabel Wrap="False" runat="server" ID="ASPxLabel1" Text='<%# Bind("PickupCycleWeek")%>'></dx:ASPxLabel></td>
                                    <td><dx:ASPxLabel Wrap="False" runat="server" ID="ASPxLabel3" Text='<%# Bind("PickupCycleDay")%>'></dx:ASPxLabel></td>
                                    <td><dx:ASPxLabel Wrap="False" runat="server" ID="ASPxLabel4" Text='<%# Bind("RouteCode")%>'></dx:ASPxLabel></td>
                                    <td><dx:ASPxDateEdit ID="PickupDate" ClientInstanceName="PickupDate" Width="100" Value='<%# Bind("PickupDate")%>' EditFormat="Custom" EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" ValidationSettings-RegularExpression-ErrorText="Invalid date" ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" runat="server" ></dx:ASPxDateEdit></td>                                
                                    <td><dx:ASPxComboBox DropDownRows=3 ID="PermitID" ClientInstanceName="PermitID"  Width="70" Value='<%# Bind("PermitID")%>' ValueField="PermitID" TextField="PermitNbr" DataSourceID="dsPermits" ValueType="System.Int32" runat="server" ><columns><dx:ListBoxColumn FieldName="PermitID" Visible="false" /><dx:ListBoxColumn FieldName="PermitNbr" Visible="true" /></columns></dx:ASPxComboBox></td>
                                    <td style="text-align:center"><dx:ASPxCheckbox runat="server" ID="ckApprovedForExport" Value='<%# Bind("ApprovedForExport")%>' ></dx:ASPxCheckbox></td>
                                    <td style="text-align:center"><dx:ASPxCheckbox runat="server" ID="ckExported" Value='<%# Bind("Exported")%>' Enabled="False"></dx:ASPxCheckbox></td>
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
                <dx:ASPxCallback runat="server" ID="cbSpecialDelete" ClientInstanceName="cbSpecialDelete" OnCallback="vbSpecialDelete"><ClientSideEvents CallbackComplete="jsConfirmSpecialDelete" /></dx:ASPxCallback>

                <asp:SqlDataSource ID="dsScheduleTypes" 
                    SelectCommand="SELECT ScheduleTypeID, ScheduleType FROM tlkScheduleTypes ORDER BY ScheduleType" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="dsPermits" 
                    SelectCommand="SELECT PermitID, PermitNbr FROM tblPermits ORDER BY PermitNbr" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>

    <dx:ASPxPopupControl ID="popSchedule2" ClientInstanceName="popSchedule2" HeaderText="Create Pickups for specific Route" FooterText="" Width="280px" Height="100px" EncodeHtml="false" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AllowResize="true" runat="server" CloseAction="OuterMouseClick" AllowDragging="True" ShowFooter="true">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <table cellpadding="3" border=0 align="center" width="95%">
                    <tr>
                        <td nowrap align="right">Route:</td>
                        <td>
                            <dx:ASPxComboBox Width=150 ID="routeID" ClientInstanceName="routeID"  ValueField="RouteID" TextField="RouteCode" 
                                AutoPostBack="true" DataSourceID="dsRoutes" ShowShadow="true" IncrementalFilteringMode="StartsWith"  
                                ValueType="System.Int32" runat="server" >
                                    <columns>
                                        <dx:ListBoxColumn FieldName="RouteID" Visible="false" />
                                        <dx:ListBoxColumn FieldName="RouteCode" />
                                    </columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="dsRoutes" runat="server" ConnectionString="<%$ ConnectionStrings:App-MainConnectionString %>">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <dx:ASPxButton ID="btnCreateSchedulePop" Text="Create Schedule for specified Route" OnClick="btnCreateSchedule2OK" runat="server" AutoPostBack="True" />
                        </td>
                    </tr>
                </table>                   
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</form>
</body>
</html>
