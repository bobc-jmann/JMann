<%@ Page Title="SpecialsGrading" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="SpecialsGrading.aspx.vb" Inherits="SpecialsGrading" %>

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
	    .parm3 { width: 20%; text-align: right; font-weight:bold }
	</style> 
	<script type="text/javascript">
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Specials Grading" runat="server"></dx:ASPxLabel></td>
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
                <td style="width: 10%"></td>
                <td style="width: 30%"></td>
                <td style="width: 40%">
                    <dx:ASPxButton runat="server" ID="btnSpecialsNotGradedReport" Text="Specials Not Graded Report" OnClick="btnRunSpecialsNotGradedReport_Click" AutoPostBack="True" Width="60%"></dx:ASPxButton>
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
                    <dx:ASPxCheckbox runat="server" ID="ckShowAllSpecials" Text="Show All Specials" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
                <td></td>
          </tr>
        </table>
        <br />
        <table id="tblSpecialsNotGraded" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%" colspan="5">

                    <dx:ASPxGridView ID="grdSpecialsNotGraded" KeyFieldName="PickupID" runat="server" 
						DataSourceID="dsSpecialsNotGraded" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False" 
						Settings-VerticalScrollBarMode="Auto" Settings-VerticalScrollableHeight="300" EnableViewState="false">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="40" ShowEditButton="true"/>
                            <dx:GridViewDataColumn FieldName="PickupID" Visible="False" /> 
                            <dx:GridViewDataColumn FieldName="City" VisibleIndex="1" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <EditFormSettings Visible="False"></EditFormSettings>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="StreetAddress" VisibleIndex="2" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" MinWidth="200">
                                <EditFormSettings Visible="False"></EditFormSettings>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Zip" VisibleIndex="3" EditFormSettings-Visible="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <EditFormSettings Visible="False"></EditFormSettings>
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemBags" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesTextEdit ClientInstanceName="ItemBags" DisplayFormatString="#" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemBoxes" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesTextEdit ClientInstanceName="ItemBoxes" DisplayFormatString="#" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ItemOther" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesTextEdit ClientInstanceName="ItemOther" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="DriverID" VisibleIndex="7" Caption="Driver" HeaderStyle-HorizontalAlign="Center" MinWidth="100">
                                <PropertiesComboBox DataSourceID="dsDriver" TextField="DriverName" ValueField="DriverID" ValueType="System.Int32" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTimeEditColumn FieldName="EndTime" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                <PropertiesTimeEdit ClientInstanceName="EndTime" DisplayFormatString="HH:mm" EditFormatString="HH:mm" />
                                <HeaderStyle HorizontalAlign="Center" Wrap="True"></HeaderStyle>
                            </dx:GridViewDataTimeEditColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="Grade" VisibleIndex="9" Caption="Grade" HeaderStyle-HorizontalAlign="Center" MinWidth="100">
                                <PropertiesComboBox DataSourceID="dsGrade" TextField="Grade" ValueField="Grade" ValueType="System.String" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="Status" VisibleIndex="10" Caption="Status" HeaderStyle-HorizontalAlign="Center" MinWidth="100">
                                <PropertiesComboBox DataSourceID="dsStatus" TextField="SpecialStatus" ValueField="SpecialStatus" ValueType="System.String" />
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </dx:GridViewDataComboBoxColumn>
                       </Columns>
                        <SettingsEditing EditFormColumnCount="6" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <SettingsBehavior ColumnResizeMode="Control" />
                        <SettingsPager PageSize="100" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsSpecialsNotGraded" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        UpdateCommand="spSpecialsGrading_Update"
                        UpdateCommandType="StoredProcedure">
                        <UpdateParameters>
                            <asp:Parameter Name="PickupID" Type="Int32" />
                            <asp:Parameter Name="ItemBags" Type="Int32" />
                            <asp:Parameter Name="ItemBoxes" Type="Int32" />
                            <asp:Parameter Name="ItemOther" Type="String" />
                            <asp:Parameter Name="DriverID" Type="Int32" />
                            <asp:Parameter Name="EndTime" Type="DateTime" />
                            <asp:Parameter Name="Grade" Type="String" />
                            <asp:Parameter Name="Status" Type="String" />
                            <asp:SessionParameter Name="Username" SessionField="vUserName" Type="String" />
                         </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsGrade" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [Grade] FROM [tlkGrades] ORDER BY [Grade]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsStatus" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT [SpecialStatus] FROM [tlkSpecialStatuses] ORDER BY [SortCode]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDriver" runat="server" ProviderName="System.Data.SqlClient"
						SelectCommand="SELECT D.DriverID, D.DriverName FROM tblDrivers AS D WHERE D.Active = 1 ORDER BY D.DriverName"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>                    
                    <asp:HiddenField ID="hfDriver" runat="server" Value=""></asp:HiddenField>
                </td>
            </tr>
        </table>
      
    </div>
    </form>
</body>
</html>
