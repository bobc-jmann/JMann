<%@ Page Title="Uncommitted Changes" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="UncommittedChanges.aspx.vb" Inherits="UncommittedChanges" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Uncommitted Changes" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table id="tblMain" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 40%">
                    <dx:ASPxGridView ID="grdMain" KeyFieldName="RouteID" runat="server" DataSourceID="dsRoutes" 
						Width="100%" EnableRowsCache="false"
						OnCustomButtonInitialize="grdMain_CustomButtonInitialize">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" width="150" ShowClearFilterButton="true">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="cbCreate" Text="Create" Visibility="AllDataRows" />
                                    <dx:GridViewCommandColumnCustomButton ID="cbCommit" Text="Commit" Visibility="AllDataRows" />
                                    <dx:GridViewCommandColumnCustomButton ID="cbCancel" Text="Cancel" Visibility="AllDataRows" />
                                </CustomButtons>
							</dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="RouteID" Visible="false" /> 
                            <dx:GridViewDataColumn FieldName="RegionCode" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" /> 
                            <dx:GridViewDataColumn FieldName="RouteCode" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" /> 
                            <dx:GridViewDataColumn FieldName="UncommittedChanges" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" /> 
                        </Columns>
                        <SettingsBehavior ColumnResizeMode="Control" />
                            <SettingsPager PageSize="1000">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="False" ShowFooter="True" />
                    </dx:ASPxGridView>

                    <asp:SqlDataSource ID="dsRoutes" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>"
                        SelectCommand="SELECT RG.RegionCode, R.RouteID, R.RouteCode, R.UncommittedChanges
							FROM tblRoutes AS R
							OUTER APPLY (SELECT TOP (1) RG.RegionCode
								FROM tlkRegions AS RG
								INNER JOIN tblPickupCycleDriverLocations AS PCDL ON PCDL.RegionID = RG.RegionID
									AND PCDL.PrimaryRegion = 1
								INNER JOIN tblPickupCycles AS PC ON PC.PickupCycleID = PCDL.PickupCycleID
								INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.PickupCycleTemplateID = PC.PickupCycleTemplateID
								WHERE PCTD.RouteID = R.RouteID) AS RG
							ORDER BY RegionCode, RouteCode">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 70%" />
           </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
