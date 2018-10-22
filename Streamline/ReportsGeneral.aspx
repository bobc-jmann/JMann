<%@ Page Title="General Reports" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ReportsGeneral.aspx.vb" Inherits="ReportsGeneral" %>

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
</head>

<body>
     <form id="Form1" runat="server">
     <asp:ScriptManager ID="ScriptManager122" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="Scripts/jquery-1.4.1.min.js" />
        </Scripts>
    </asp:ScriptManager>
    <div style="position:relative;top:0;left:0;">
        <table runat="server" id="tblMenu" border="0" width="100%" cellpadding=2 cellspacing=0 style="border:0px gainsboro inset">
            <tr id="hdrTR" runat="server" style="background-color:beige;font-size:smaller">
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="General Reports" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
    <div id="divReports" class="specials" runat="server">
        <table>
            <tr>
                <td style="font-weight: bold; font-size: large">Specials:</td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnSpecialsSheet" runat="server" Text="Specials Sheet" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnSpecialsCreated" runat="server" Text="Specials Created" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnSpecialsSummary" runat="server" Text="Specials Summary" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnSpecialsAddressesForRouting" runat="server" Text="Specials Addresses for Routing" Width="250px"></asp:Button>
    	        </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="font-weight: bold; font-size: large">Call Center:</td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnConfirmAndDoNotRedTag" runat="server" Text="Confirm & Do Not Red Tag Sheet" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnConfirmLog" runat="server" Text="Confirm Log" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnMissLog" runat="server" Text="Miss Log" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnRedTagLog" runat="server" Text="Red Tag Log" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnTextMessageLog" runat="server" Text="Text Message Log" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnPhoneSheets" runat="server" Text="Phone Sheets" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnAccuZipSearches" runat="server" Text="AccuZip Searches" Width="250px"></asp:Button>
    	        </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="font-weight: bold; font-size: large">Scheduling:</td>
            </tr>
            <tr>
	  	        <td>
                    <asp:Button ID="btnScheduleCalendar" runat="server" Text="Schedule Calendar" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
	  	        <td>
                    <asp:Button ID="btnScheduleChecker" runat="server" Text="Schedule Checker" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnMailCountsByTemplate" runat="server" Text="Mail Counts By Template" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnPostageReport" runat="server" Text="Postage Report" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnControlSheet" runat="server" Text="Control Sheet - Scheduled" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnControlSheet_Unscheduled" runat="server" Text="Control Sheet - Unscheduled" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnStreetListing" runat="server" Text="Street Listing - Scheduled" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnStreetListingUnscheduled" runat="server" Text="Street Listing - Unscheduled" Width="250px"></asp:Button>
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
                    <asp:Button ID="btnRoutesByCity" runat="server" Text="Routes by City" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnRouteCountsByCity" runat="server" Text="Route Counts by City" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnCarrierRouteAnalysis" runat="server" Text="Carrier Route Analysis" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnHighPostage" runat="server" Text="High Postage" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnMailingDateRules" runat="server" Text="Mailing Date Rules" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnOptimumDeliveryDays" runat="server" Text="Optimum Delivery Days" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnRouteChangeLog" runat="server" Text="Route Change Log" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdMissedPickupsReport" runat="server" Text="Missed Pickups Report" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdCurrentAddressesReport" runat="server" Text="Current Addresses Report" width="250px"/>
    	        </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="font-weight: bold; font-size: large">Lists:</td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnRouteSection" runat="server" Text="Routes & Sections" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnPickupCycles" runat="server" Text="Pickup Cycles" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnPickupCycleTemplates" runat="server" Text="Pickup Cycle Templates" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnAddressesByStatus" runat="server" Text="Addresses by Status" Width="250px"></asp:Button>
    	        </td>
            </tr>
<%--            <tr> // too many addresses for SSRS
    	        <td>
                    <asp:Button ID="btnNonRouteAddresses" runat="server" Text="Non-Route Addresses" Width="250px"></asp:Button>
    	        </td>
            </tr>--%>
        </table>
    </div>
    </form>
</body>
</html>
