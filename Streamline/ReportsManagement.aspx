<%@ Page Title="Management Reports" Language="VB" Theme="DevEx" AutoEventWireup="false" CodeFile="ReportsManagement.aspx.vb" Inherits="ReportsManagement" %>

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
                <td><dx:ASPxLabel Wrap="False" Font-Size="X-Large" ID="lblPageTitle" Text="Management Reports" runat="server"></dx:ASPxLabel></td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
    <div id="divReports" class="specials" runat="server">
    	<table class="specials">
            <tr>
                <td style="width: 25px">
                    <dx:ASPxButton ID="btnArrow_Left" AutoPostBack="true" OnClick="Arrow_Left" Visible="true" Border-BorderStyle="None" Border-BorderColor="White" runat="server">
                        <Image Url="~/resources/images/Arrow_Left_Blue.png" Height="25" Width="25"></Image>
                        <Border BorderStyle="None" BorderWidth="0" />
                        <FocusRectPaddings PaddingBottom="0" Padding="0" />
                    </dx:ASPxButton>
                </td>
                <td style="width: 200px">Start Date:
                    <dx:ASPxDateEdit ID="dtStartDate" ClientInstanceName="dtStartDate" 
                        Width="100%" ToolTip="Start Date" EditFormat="Custom" 
                        EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                        ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                        ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                        runat="server" Date="1/1/0001 12:00:00 AM">
                        <ValidationSettings SetFocusOnError="True">
                            <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                            <RegularExpression ErrorText="Invalid date"></RegularExpression>
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
				</td>
                <td style="width: 200px">End Date:
                    <dx:ASPxDateEdit ID="dtEndDate" ClientInstanceName="dtEndDate" 
                        Width="100%" ToolTip="End Date" EditFormat="Custom" 
                        EditFormatString="MM/dd/yyyy" ValidationSettings-SetFocusOnError="True" 
                        ValidationSettings-RegularExpression-ErrorText="Invalid date" 
                        ValidationSettings-ErrorImage-Url="~/Resources/Images/iconError.png" 
                        runat="server" Date="1/1/0001 12:00:00 AM">
                        <ValidationSettings SetFocusOnError="True">
                            <ErrorImage Url="~/Resources/Images/iconError.png"></ErrorImage>
                            <RegularExpression ErrorText="Invalid date"></RegularExpression>
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
				</td>
                <td style="width: 25px">
                    <dx:ASPxButton ID="btnArrow_Right" AutoPostBack="true" OnClick="Arrow_Right" Visible="true" Border-BorderStyle="None" Border-BorderColor="White" runat="server">
                        <Image Url="~/resources/images/Arrow_Right_Blue.png" Height="25" Width="25"></Image>
                        <Border BorderStyle="None" BorderWidth="0" />
                        <FocusRectPaddings PaddingBottom="0" Padding="0" />
                    </dx:ASPxButton>
                </td>
             </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="font-weight: bold; font-size: large">Missing Information:</td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnMissingBaggerInformation" runat="server" Text="Missing Bagger Information" Width="250px"></asp:Button>
   	            </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnMissingDriverInformation" runat="server" Text="Missing Driver Information" Width="250px"></asp:Button>
   	            </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnMissingPickups" runat="server" Text="Missing Pickups from Tablets" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnMissingDriversForPickups" runat="server" Text="Missing Drivers for Pickups" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
                    <asp:Button ID="btnMissingDailyRouteData" runat="server" Text="Missing Daily Route Data" Width="250px"></asp:Button>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdSpecialsNotGradedReport" runat="server" Text="Specials Not Graded" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdContainerPickupsNotGradedReport" runat="server" Text="Container Pickups Not Graded" width="250px"/>
    	        </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="font-weight: bold; font-size: large">Production:</td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdRegionalCartInventorySummary" runat="server" Text="Regional Cart Inventory Summary" width="250px"/>
    	        </td>
            </tr>
			<tr>
    	        <td>
				    <asp:Button ID="cmdProductionWeekly" runat="server" Text="Production - Weekly" width="250px"/>
				    <asp:Button ID="cmdProductionUpdate" runat="server" Text="Update Production for Reports" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdProductionSummary" runat="server" Text="Production - Summary" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdProductionDetail" runat="server" Text="Production - Detail" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdProductionBaggers" runat="server" Text="Production - Baggers" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdProductionDrivers" runat="server" Text="Production - Drivers" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdProductionEmail" runat="server" Text="Production - Email" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdPickupVsPutOutAnalysis" runat="server" Text="Pickup vs Put Out Analysis" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdProductionAnalysis" runat="server" Text="Production Analysis" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdProductionComparison" runat="server" Text="Production Comparison" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdSeasonalityAnalysis" runat="server" Text="Seasonality Analysis" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdInventoryAnalysis" runat="server" Text="Inventory Analysis" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdMailDaysOnHandAnalysis" runat="server" Text="Mail Days on Hand Analysis" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdMonthlySummary" runat="server" Text="Monthly Summary" width="250px"/>
    	        </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td style="font-weight: bold; font-size: large">Accounting:</td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdProductionCartRecap" runat="server" Text="Production - Cart Recap" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdPostageDeposit" runat="server" Text="Postage Deposit" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdPostageBilling" runat="server" Text="Postage Billing" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdPostageBillingDetailByAddress" runat="server" Text="Postage Billing Detail by Address" width="250px"/>
    	        </td>
            </tr>
            <tr>
    	        <td>
				    <asp:Button ID="cmdPostageBillingByPermit" runat="server" Text="Postage Billing by Permit" width="250px"/>
    	        </td>
            </tr>
        </table>

    </div>
    </form>
</body>
</html>
