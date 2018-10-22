<%@ Page Title="Bracket Standards" Language="VB" Theme="Youthful" AutoEventWireup="false" CodeFile="BracketStandards.aspx.vb" Inherits="BracketStandards" %>

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
        function OnInit(s, e) {
            AdjustSize();
        }
        function OnEndCallback(s, e) {
            AdjustSize();
        }
        function OnControlsInitialized(s, e) {
            ASPxClientUtils.AttachEventToElement(window, "resize", function (evt) {
                AdjustSize();
            });
        }
        function AdjustSize() {
            var height = Math.max(0, document.documentElement.clientHeight) - 150;
            grdMain.SetHeight(height);
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Bracket Standards" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divGrid" class="specials" runat="server">
        <table>
            <tr>
                <td style="width: 50%">Standard Type:
				    <asp:DropDownList ID="ddlStandardTypes" runat="server" ToolTip="Standard Types" Width="100%" AutoPostBack="True" />
 				</td>
                <td style="width: 50%"></td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxCheckbox runat="server" ID="ckShowInactiveBracketStandards" Text="Show Inactive Bracket Standards" AutoPostBack="true"></dx:ASPxCheckbox>
                </td>
            </tr>
         </table>
         <table id="tblGrid" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 30%">
                    <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" runat="server" DataSourceID="dsBracketStandards" KeyFieldName="ID" Width="100%" EnableRowsCache="false" SettingsBehavior-ConfirmDelete="True">
                        <Columns>
							<dx:GridViewDataCheckColumn FieldName="Active" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewCommandColumn VisibleIndex="0" Width="60" ShowNewButtonInHeader="True" ShowDeleteButton="true"/>
                            <dx:GridViewDataColumn FieldName="ID" Visible="false" /> 
                            <dx:GridViewDataComboBoxColumn FieldName="DepartmentID" Caption="Department" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True" >
                                <PropertiesComboBox DataSourceID="dsDepartments" ValueType="System.String" ValueField="DepartmentID" TextField="DepartmentName" IncrementalFilteringMode="StartsWith" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn FieldName="PriceLevel" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True" /> 
                            <dx:GridViewDataTextColumn FieldName="Standard" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True">
								<PropertiesTextEdit DisplayFormatString="0%" />
							</dx:GridViewDataTextColumn> 
                        </Columns>
                        <SettingsEditing Mode="Batch" />
                        <SettingsPager PageSize="100" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />
                        <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                            <Settings ShowFilterRow="False" ShowGroupPanel="False" ShowFooter="True" />
						<GroupSummary>
							<dx:ASPxSummaryItem FieldName="DepartmentID" SummaryType="Count" />
							<dx:ASPxSummaryItem FieldName="Standard" SummaryType="Sum" />
						</GroupSummary>
						<Settings ShowGroupPanel="true" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsBracketStandards" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="Stores.spBracketStandards_SelectAll"
                        SelectCommandType="StoredProcedure"
                        InsertCommand="Stores.spBracketStandards_Insert"
                        InsertCommandType="StoredProcedure"
                        UpdateCommand="Stores.spBracketStandards_Update"
                        UpdateCommandType="StoredProcedure"
                        DeleteCommand="Stores.spBracketStandards_Delete"
                        DeleteCommandType="StoredProcedure">
						<SelectParameters>
 							<asp:ControlParameter ControlID="ddlStandardTypes" Name="StandardTypeID" PropertyName="SelectedValue" Type="Int32"/>
							<asp:ControlParameter name="ShowInactive" ControlID="ckShowInactiveBracketStandards" PropertyName="Value" Type="Boolean" />
						</SelectParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Active" Type="Boolean" />
 							<asp:ControlParameter ControlID="ddlStandardTypes" Name="StandardTypeID" PropertyName="SelectedValue" Type="Int32"/>
                            <asp:Parameter Name="DepartmentID" Type="Int32" />
                            <asp:Parameter Name="PriceLevel" Type="Decimal" />
                            <asp:Parameter Name="Standard" Type="Decimal" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="ID" Type="Int32" />
                            <asp:Parameter Name="Active" Type="Boolean" />
 							<asp:ControlParameter ControlID="ddlStandardTypes" Name="StandardTypeID" PropertyName="SelectedValue" Type="Int32"/>
                            <asp:Parameter Name="DepartmentID" Type="Int32" />
                            <asp:Parameter Name="PriceLevel" Type="Decimal" />
                            <asp:Parameter Name="Standard" Type="Decimal" />
                            <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="ID" Type="Int32" />
                           <asp:SessionParameter Name="CurrentUserID" SessionField="vUserID" Type="Int32" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsStandardTypes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT StandardTypeID, StandardType FROM Stores.BracketStandardTypes ORDER BY StandardType">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDepartments" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-NewMerchConnectionString%>"
                        SelectCommand="SELECT DepartmentID, DepartmentName 
							FROM SysConfig.Departments 
							WHERE SortOrder IS NOT NULL
							ORDER BY SortOrder">
                    </asp:SqlDataSource>

                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                </td>
                <td style="width: 70%" />
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
