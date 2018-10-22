Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Web.Services

Partial Class ReportsGeneral
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
        Select Case Request.HttpMethod
            Case "POST"
            Case "GET"
            Case "HEAD"
        End Select
    End Sub

    Protected Sub btnConfirmAndDoNotRedTag_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirmAndDoNotRedTag.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Confirm and Do Not Red Tag Sheet")
	End Sub

	Protected Sub btnSpecialsSheet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSpecialsSheet.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Sheet")
	End Sub

	Protected Sub btnSpecialsCreated_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSpecialsCreated.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Created")
	End Sub

	Protected Sub btnSpecialsSummary_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSpecialsSummary.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Summary")
	End Sub

	Protected Sub btnSpecialsAddressesForRouting_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSpecialsAddressesForRouting.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Addresses for Routing")
	End Sub

	Protected Sub btnConfirmLog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirmLog.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Confirm Log")
	End Sub

	Protected Sub btnMissLog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMissLog.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Miss Log")
	End Sub

	Protected Sub btnRedTAgLog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRedTagLog.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Red Tag Log")
	End Sub

	Protected Sub btntextMessageLog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTextMessageLog.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Text Message Log")
	End Sub

	Protected Sub btnPhoneSheets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPhoneSheets.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Phone Sheets")
	End Sub

	Protected Sub btnAccuZipSearches_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAccuZipSearches.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/AccuZip Searches Report")
	End Sub

	Protected Sub btnScheduleCalendar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScheduleCalendar.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Schedule Calendar")
	End Sub

	Protected Sub btnScheduleChecker_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScheduleChecker.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Schedule Checker")
	End Sub

	Protected Sub btnMailCountsByTemplate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMailCountsByTemplate.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Mail Counts by Template")
	End Sub

	Protected Sub btnPostageReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPostageReport.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Postage Report")
	End Sub

	Protected Sub btnControlSheet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnControlSheet.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Control Sheet")
	End Sub

	Protected Sub btnControlSheet_Unscheduled_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnControlSheet_Unscheduled.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Control Sheet - Unscheduled")
	End Sub

	Protected Sub btnStreetListing_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStreetListing.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Street Listing")
	End Sub

	Protected Sub btnStreetListingUnscheduled_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStreetListingUnscheduled.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Street Listing - Unscheduled")
	End Sub

	Protected Sub cmdDoNotBagSheet_Click(sender As Object, e As System.EventArgs) Handles cmdDoNotBagSheet.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Do Not Bag Sheet")
	End Sub

	Protected Sub cmdDoNotBagSheet_Unscheduled_Click(sender As Object, e As System.EventArgs) Handles cmdDoNotBagSheet_Unscheduled.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Do Not Bag Sheet - Unscheduled")
	End Sub
	Protected Sub btnRoutesByCity_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRoutesByCity.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Routes by City")
	End Sub

	Protected Sub btnRouteCountssByCity_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRouteCountsByCity.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Route Counts by City")
	End Sub

	Protected Sub btnCarrierRouteAnalysis_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCarrierRouteAnalysis.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Carrier Route Analysis")
	End Sub

	Protected Sub btnHighPostage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHighPostage.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/High Postage")
	End Sub

	Protected Sub btnMailingDateRules_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMailingDateRules.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Mailing Date Rules")
	End Sub

	Protected Sub btnOptimumDeliveryDays_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOptimumDeliveryDays.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Optimum Delivery Days")
	End Sub

	Protected Sub btnRouteChangeLog_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRouteChangeLog.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Route Change Log")
	End Sub

	Protected Sub cmdMissedPickupsReport_Click(sender As Object, e As System.EventArgs) Handles cmdMissedPickupsReport.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missed Pickups Report")
	End Sub

	Protected Sub cmdCurrentAddressesReport_Click(sender As Object, e As System.EventArgs) Handles cmdCurrentAddressesReport.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Current Addresses Report")
	End Sub

	Protected Sub btnRouteSection_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRouteSection.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/RouteSectionReport")
	End Sub

	Protected Sub btnPickupCycles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPickupCycles.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/PickupCyclesReport")
	End Sub

	Protected Sub btnPickupCycleTemplates_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPickupCycleTemplates.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/PickupCycleTemplates")
	End Sub

	Protected Sub btnAddressesByStatus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddressesByStatus.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Addresses by Status")
	End Sub

	'Protected Sub btnNonRouteAddresses_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNonRouteAddresses.Click
	'	Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Scheduing/Non-Route Addresses")
	'End Sub

End Class
