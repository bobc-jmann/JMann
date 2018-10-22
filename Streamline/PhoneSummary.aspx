<%@ Page Title="Phone Summary" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="PhoneSummary.aspx.vb" Inherits="PhoneSummary" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Phone Summary" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divMaint" class="specials" runat="server">
         <table id="tblPhoneSheets" runat="server" style="visibility: visible">
            <tr>
    	        <td style="text-align: right">
                    <asp:Button ID="btnPhoneSheets" runat="server" Text="Phone Sheets Report" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="5">
                    <dx:ASPxGridView ID="grdPhoneSheets" KeyFieldName="UserDate" runat="server" DataSourceID="dsPhoneSheets" Width="100%" EnableRowsCache="false">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="60" Caption=" " ShowEditButton="true"/>
                            <dx:GridViewDataColumn FieldName="UserDate" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="UserID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="Date" VisibleIndex="1" EditFormSettings-Visible="False" GroupIndex="0">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Operator" VisibleIndex="2" EditFormSettings-Visible="False" />
                            <dx:GridViewDataColumn FieldName="RollOverVoiceMail" VisibleIndex="3" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="VoiceMailReturned" VisibleIndex="4" EditFormSettings-Visible="True"  HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="WebSpecialsReturned" VisibleIndex="4" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="EmailsProcessed" VisibleIndex="5" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="TotalCallsReceived" VisibleIndex="6" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="CourtesyCallsMade" VisibleIndex="7" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="ReRouteCallsMade" VisibleIndex="8" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="OtherComplaints" VisibleIndex="9" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="SpecialsCreated" VisibleIndex="10" EditFormSettings-Visible="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="Confirms" VisibleIndex="11" EditFormSettings-Visible="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="Misses" VisibleIndex="12" EditFormSettings-Visible="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Right" />
                            <dx:GridViewDataColumn FieldName="Comments" VisibleIndex="13" EditFormSettings-Visible="True" HeaderStyle-Wrap="True" />
                        </Columns>
                        <Settings ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" />
                        <SettingsEditing EditFormColumnCount="6"/>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="10">
                        </SettingsPager>
                        <GroupSummary>
                            <dx:ASPxSummaryItem FieldName="Operator" ShowInGroupFooterColumn="Operator" SummaryType="Count" />
                            <dx:ASPxSummaryItem FieldName="RollOverVoiceMail" ShowInGroupFooterColumn="RollOverVoiceMail" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="VoiceMailReturned" ShowInGroupFooterColumn="VoiceMailReturned" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="WebSpecialsReturned" ShowInGroupFooterColumn="WebSpecialsReturned" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="EmailsProcessed" ShowInGroupFooterColumn="EmailsProcessed" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="TotalCallsReceived" ShowInGroupFooterColumn="TotalCallsReceived" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="CourtesyCallsMade" ShowInGroupFooterColumn="CourtesyCallsMade" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="ReRouteCallsMade" ShowInGroupFooterColumn="ReRouteCallsMade" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="OtherComplaints" ShowInGroupFooterColumn="OtherComplaints" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="SpecialsCreated" ShowInGroupFooterColumn="SpecialsCreated" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="Confirms" ShowInGroupFooterColumn="Confirms" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="Misses" ShowInGroupFooterColumn="Misses" SummaryType="Sum" />
                        </GroupSummary>
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsPhoneSheets" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        UpdateCommand="UPDATE [tblPhoneSheetsForSpecials] SET [RollOverVoiceMail] = @RollOverVoiceMail, [VoiceMailReturned] = @VoiceMailReturned, [WebSpecialsReturned] = @WebSpecialsReturned, [TotalCallsReceived] = @TotalCallsReceived, [CourtesyCallsMade] = @CourtesyCallsMade, [ReRouteCallsMade] = @ReRouteCallsMade, [OtherComplaints] = @OtherComplaints, [Comments] = REPLACE(@Comments, '''', '''''') WHERE [Date] = @Date AND [UserID] = @UserID">
                        <UpdateParameters>
                            <asp:Parameter Name="Date" Type="DateTime" />
                            <asp:Parameter Name="UserID" Type="Int32" />
                            <asp:Parameter Name="RollOverVoiceMail" Type="Int32" />
                            <asp:Parameter Name="VoiceMailReturned" Type="Int32" />
                            <asp:Parameter Name="WebSpecialsReturned" Type="Int32" />
                            <asp:Parameter Name="EmailsProcessed" Type="Int32" />
                            <asp:Parameter Name="TotalCallsReceived" Type="Int32" />
                            <asp:Parameter Name="CourtesyCallsMade" Type="Int32" />
                            <asp:Parameter Name="ReRouteCallsMade" Type="Int32" />
                            <asp:Parameter Name="OtherComplaints" Type="Int32" />
                            <asp:Parameter Name="Comments" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="hfPhoneSheetsSelectCommand" runat="server" Value=""></asp:HiddenField>
                </td>
            </tr>
        </table>       
    </div>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblDateMin" runat="server" Text="Earliest Phone Sheet Date:"></asp:Label>
                    <dx:ASPxDateEdit ID="dtDateMin" ClientInstanceName="dtDateMin"
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
