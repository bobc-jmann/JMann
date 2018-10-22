<%@ Page Title="Specials Reports" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="SpecialsReports.aspx.vb" Inherits="SpecialsReports" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="DevExpress.XtraReports.v18.1.Web.WebForms, Version=18.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
	<style type="text/css">   
		html { height: 100% }   
		body { height: 100%; margin: 0px; padding: 0px; font-family: Arial; font-size: 12px; }  
	    .auto-style1 {
            width: 104px;
        }
        .auto-style2 {
            width: 161px;
        }
        .auto-style3 {
            width: 120px;
        }
	</style> 
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OnRowClick(s, e) {
            _aspxClearSelection();
            s._selectAllRowsOnPage(false);
            s.SelectRow(e.visibleIndex, true);
        }
        function ToggleCalendar() {
            return false;
        }
    </script>
</head>

<body>
    <form id="Form1" runat="server">
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Specials Reports" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <div id="divSearch" class="specials" runat="server">
        <table>
        <tr><td>
        <table>
    	    <tr>
    	        <td class="auto-style1">
    	            <table class="specials">
    	                <tr>
    	                    <td class="style1">For date: <a id="SelectedPickupDate" runat="server"></a></td>
    	                </tr>
    	                <tr>
    	                    <td>
			                    <asp:Calendar id="calSpecials" runat="server"	Width="180px" Height="180px" Font-Size="X-Small" Font-Names="Tahoma">
				                    <TodayDayStyle BorderStyle="Solid"></TodayDayStyle>
			                    </asp:Calendar>
     	                    </td>
    	                </tr>
    	            </table>
    	        </td>
    	        <td class="auto-style3">
    	            <table class="specials">
    	                <tr>
    	                    <td class="auto-style2">For location: <a id="SelectedLocation" runat="server"></a></td>
    	                </tr>
    	                <tr>
    	                    <td class="auto-style2">
                                <asp:ListBox ID="ddlLocations" runat="server" Width="120" Height="180px" Font-Size="X-Small" Font-Names="Tahoma" SelectionMode="Multiple" AutoPostBack="False"></asp:ListBox>
                            </td>
    	                </tr>
    	            </table>
    	        </td>
     	        <td>
    	            <table class="specials">
      	                <tr>
    	                    <td>
				                <asp:Button ID="cmdSpecialsSheet" runat="server" Text="Specials Sheet" width="250px"/>
    	                    </td>
                        </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdSummaryReport" runat="server" Text="Summary Report" width="250px"/>
    	                    </td>
    	                </tr>
   	                    <tr>
    	                    <td>
				                <asp:Button ID="cmdAddressesForRouting" runat="server" Text="Addresses For Routing Report" width="250px"/>
    	                    </td>
    	                </tr>
   	                    <tr>
    	                    <td>
				                <asp:Button ID="cmdConfirmRedTagReport" runat="server" Text="Confirm and Do Not Red Tag Report" width="250px"/>
    	                    </td>
    	                </tr>
                        <tr><td></td></tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdContainerPickupsSheet" runat="server" Text="Container Pickups Sheet" width="250px"/>
                                <a runat="server" target="_new" id="aContainerPickupsSheet" visible="false" href="#">Show Report</a>
    	                    </td>
                        </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdContainerPickupsForRouting" runat="server" Text="Container Pickups For Routing Report" width="250px"/>
    	                    </td>
    	                </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdDoNotBagSheet" runat="server" Text="Do Not Bag Sheet" width="250px"/>
    	                    </td>
    	                </tr>
    	                <tr>
    	                    <td>
				                <asp:Button ID="cmdDoNotBagSheet_Unscheduled" runat="server" Text="Do Not Bag Sheet - Unscheduled" width="250px"/>
    	                    </td>
    	                </tr>
						<tr>
	  						<td>
								<asp:Button ID="btnScheduleCalendar" runat="server" Text="Schedule Calendar" Width="250px"></asp:Button>
    						</td>
						</tr>
                    </table>
    	        </td>
    	    </tr>
        </table>
        </td></tr>
        </table>
    </div>
    </form>
</body>
</html>
