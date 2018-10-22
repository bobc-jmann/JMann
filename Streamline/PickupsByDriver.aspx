<%@ Page Title="Pickups by Driver" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="PickupsByDriver.aspx.vb" Inherits="PickupsByDriver" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="/Streamline/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
	</style> 
	<script type="text/javascript">
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
            var height = Math.max(0, document.documentElement.clientHeight) - 200;
            pageControl.SetHeight(height);
            grdMain.SetHeight(height - 50);
            try {
                grdStreets.SetHeight(height - 50);
            }
            catch (err) { }
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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Pickups by Driver" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table width="20%">
            <tr>
                <td style="width: 100%">Driver Location:
				    <asp:DropDownList ID="ddlDriverLocations" runat="server" ToolTip="Driver Location" Width="100%" AutoPostBack="True" />
 				</td>
            </tr>
            <tr>
 				<td style="width: 100%">
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
           </tr>
           <tr>
                <td>Select Driver:
                    <dx:ASPxComboBox ID="cmbDrivers" ClientInstanceName="cmbDrivers" Width="100%" ValueField="DriverID" TextField="DriverName" IncrementalFilteringMode="StartsWith" OnSelectedIndexChanged="cmbDrivers_SelectedIndexChanged"  ValueType="System.Int32" DataSourceID="dsDriver" runat="server" AutoPostBack="True" />
                </td>
           </tr>
        </table>

        <table id="tblMain" runat="server" style="visibility: visible">
            <tr>
                <td style="width: 100%">
                    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                        <TabPages>
                            <dx:TabPage Text="Pickups by Driver" runat="server" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl ID="cc" runat="server">
                                        <dx:ASPxGridView ID="grdMain" ClientInstanceName="grdMain" ClientID="grdMain" KeyFieldName="PickupsAddressID" runat="server" 
                                            DataSourceID="dsPickupsByAddress" Width="100%" EnableRowsCache="False" AutoGenerateColumns="False" 
                                            Settings-VerticalScrollBarMode="Auto">
                                            <Columns>
                                                <dx:GridViewCommandColumn VisibleIndex="0" Width="40" ShowClearFilterButton="true"/>
                                                <dx:GridViewDataColumn FieldName="PickupsAddressID" Visible="false" />
                                                <dx:GridViewDataColumn FieldName="TabletName" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="DriverName" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="StreetNumber" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="StreetName" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="City" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Time" VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Route-Section" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn FieldName="Bags" VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn FieldName="Boxes" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn FieldName="Items" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="False" ShowFooter="True" />
                                            <SettingsBehavior ColumnResizeMode="Control" />
                                            <SettingsPager PageSize="100">
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Route-Section(s) Street Listing" runat="server" Visible="true">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl2" runat="server">
                                        <dx:ASPxGridView ID="grdStreets" ClientInstanceName="grdStreets" ClientID="grdStreets" KeyFieldName="StreetName" runat="server" 
                                            DataSourceID="dsStreetListing" Width="40%" EnableRowsCache="False" AutoGenerateColumns="False" 
                                            Settings-VerticalScrollBarMode="Auto">
                                            <Columns>
                                                <dx:GridViewCommandColumn VisibleIndex="0" Width="40" ShowClearFilterButton="true"/>
                                                <dx:GridViewDataColumn FieldName="StreetName" VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="True">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="False" ShowFooter="True" />
                                            <SettingsBehavior ColumnResizeMode="Control" />
                                            <SettingsPager PageSize="100">
                                            </SettingsPager>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                        <ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
                   </dx:ASPxPageControl>

                    <asp:SqlDataSource ID="dsPickupsByAddress" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsStreetListing" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDriver" runat="server"
                        ConnectionString="<%$ ConnectionStrings:App-MainConnectionString%>">
                    </asp:SqlDataSource>
                   <dx:ASPxGlobalEvents ID="ge" runat="server">
                        <ClientSideEvents ControlsInitialized="OnControlsInitialized" />
                    </dx:ASPxGlobalEvents>
                 </td>
                <td style="width: 20%"></td>
            </tr>
        </table>
      
    </div>
    </form>
</body>
</html>
