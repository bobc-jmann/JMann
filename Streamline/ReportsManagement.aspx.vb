Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Web.Services

Partial Class ReportsManagement
    Inherits System.Web.UI.Page
    Private UnselectDate As Boolean = False
    Private MonthChanged As Boolean = False

    Private gridPageSize As Integer = 5
    Private gridPageSize2 As Integer = 15

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
        Select Case Request.HttpMethod
            Case "POST"
            Case "GET"
				If Session("ProductionUpdate") = "Visible" Then
					cmdProductionUpdate.Visible = True
				Else
					cmdProductionUpdate.Visible = False
				End If

				dtStartDate.Value = IIf(Day(Today) > 10, _
					DateValue(Month(Today) & "/1/" & Year(Today)), _
					DateValue(Month(DateAdd(DateInterval.Day, -1, DateValue(Month(Today) & "/1/" & Year(Today)))) & "/1/" & Year(DateAdd(DateInterval.Day, -1, DateValue(Month(Today) & "/1/" & Year(Today))))))
                dtEndDate.Value = IIf(Day(Today) > 10, _
                    DateAdd(DateInterval.Day, -1, DateValue(Month(DateAdd(DateInterval.Month, 1, DateValue(Month(Today) & "/1/" & Year(Today)))) & "/1/" & Year(DateAdd(DateInterval.Month, 1, DateValue(Month(Today) & "/1/" & Year(Today)))))), _
                    DateAdd(DateInterval.Day, -1, DateValue(Month(Today) & "/1/" & Year(Today))))
            Case "HEAD"
        End Select
    End Sub

    Sub Arrow_Left(s As Object, e As EventArgs)
        dtStartDate.Value = DateAdd(DateInterval.Month, -1, dtStartDate.Value)
        dtEndDate.Value = DateAdd(DateInterval.Month, -1, dtEndDate.Value)
        dtEndDate.Value = DateSerial(Year(dtEndDate.Value), Month(dtEndDate.Value) + 1, 0)  'Insure it's the last day of the month
    End Sub

    Sub Arrow_Right(s As Object, e As EventArgs)
        dtStartDate.Value = DateAdd(DateInterval.Month, 1, dtStartDate.Value)
        dtEndDate.Value = DateAdd(DateInterval.Month, 1, dtEndDate.Value)
        dtEndDate.Value = DateSerial(Year(dtEndDate.Value), Month(dtEndDate.Value) + 1, 0)  'Insure it's the last day of the month
    End Sub

    Protected Sub cmdSpecialsNotGradedReport_Click(sender As Object, e As System.EventArgs) Handles cmdSpecialsNotGradedReport.Click
        Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
            "|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy") & "|driverLocationCode~All Locations"
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Not Graded" & parms)
	End Sub

	Protected Sub cmdContainerPickupsNotGradedReport_Click(sender As Object, e As System.EventArgs) Handles cmdContainerPickupsNotGradedReport.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy") & "|driverLocationCode~All Locations"
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Container Pickups Not Graded" & parms)
	End Sub

	Protected Sub btnRunMissingBaggerInformationReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMissingBaggerInformation.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy") & "|driverLocation~All Locations"
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Bagger Information" & parms)
	End Sub

	Protected Sub btnRunMissingDriverInformationReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMissingDriverInformation.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy") & "|driverLocation~All Locations"
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Driver Information" & parms)
	End Sub

	Protected Sub btnRunMissingPickupsReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMissingPickups.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy") & "|driverLocation~All Locations"
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Pickups from Tablets" & parms)
	End Sub

	Protected Sub btnRunMissingDriversForPickupsReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMissingDriversForPickups.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy") & "|driverLocation~0"
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Drivers for Pickups" & parms)
	End Sub

	Protected Sub btnRunMissingProductionDataReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMissingDailyRouteData.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy") & "|driverLocation~All Locations"
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Daily Route Data" & parms)
	End Sub

	Protected Sub cmdRegionalCartInventorySummary_Click(sender As Object, e As System.EventArgs) Handles cmdRegionalCartInventorySummary.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Regional Cart Inventory Summary 2")
	End Sub

	Protected Sub cmdProductionWeekly_Click(sender As Object, e As System.EventArgs) Handles cmdProductionWeekly.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Report - Weekly")
	End Sub

	Protected Sub cmdProductionSummary_Click(sender As Object, e As System.EventArgs) Handles cmdProductionSummary.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Report - Summary" & parms)
	End Sub

	Protected Sub cmdProductionDetail_Click(sender As Object, e As System.EventArgs) Handles cmdProductionDetail.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Report - Detail" & parms)
	End Sub

	Protected Sub cmdProductionBaggers_Click(sender As Object, e As System.EventArgs) Handles cmdProductionBaggers.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Report - Baggers" & parms)
	End Sub

	Protected Sub cmdProductionDrivers_Click(sender As Object, e As System.EventArgs) Handles cmdProductionDrivers.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Report - Drivers" & parms)
	End Sub

	Protected Sub cmdProductionEmail_Click(sender As Object, e As System.EventArgs) Handles cmdProductionEmail.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Report - Email" & parms)
	End Sub

	Protected Sub cmdPickupVsPutOutAnalysis_Click(sender As Object, e As System.EventArgs) Handles cmdPickupVsPutOutAnalysis.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Pickup vs Put Out Analysis" & parms)
	End Sub

	Protected Sub cmdProductionAnalysis_Click(sender As Object, e As System.EventArgs) Handles cmdProductionAnalysis.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Analysis" & parms)
	End Sub

	Protected Sub cmdProductionComparison_Click(sender As Object, e As System.EventArgs) Handles cmdProductionComparison.Click
		Dim parms As String = "&PARMS=startDate~01/01/2014" & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Comparison" & parms)
	End Sub

	Protected Sub cmdSeasonalityAnalysis_Click(sender As Object, e As System.EventArgs) Handles cmdSeasonalityAnalysis.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Seasonality Analysis")
	End Sub

	Protected Sub cmdMissedPickupsReport_Click(sender As Object, e As System.EventArgs) Handles cmdInventoryAnalysis.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Inventory Analysis")
	End Sub

	Protected Sub cmdMailDaysOnHandAnalysis_Click(sender As Object, e As System.EventArgs) Handles cmdMailDaysOnHandAnalysis.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Mail Days On Hand Analysis")
	End Sub

	Protected Sub cmdMonthlySummary_Click(sender As Object, e As System.EventArgs) Handles cmdMonthlySummary.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Monthly Summary Report")
	End Sub


	Protected Sub cmdProductionCartRecap_Click(sender As Object, e As System.EventArgs) Handles cmdProductionCartRecap.Click
		Dim parms As String = "&PARMS=startDate~" & Format(dtStartDate.Value, "MM/dd/yyyy") & _
			"|endDate~" & Format(dtEndDate.Value, "MM/dd/yyyy")
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Production Report - Cart Recap" & parms)
	End Sub

	Protected Sub cmdPostageDeposit_Click(sender As Object, e As System.EventArgs) Handles cmdPostageDeposit.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Postage Deposit")
	End Sub

	Protected Sub cmdPostageBilling_Click(sender As Object, e As System.EventArgs) Handles cmdPostageBilling.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Postage Billing")
	End Sub

	Protected Sub cmdPostageBillingDetailByAddress_Click(sender As Object, e As System.EventArgs) Handles cmdPostageBillingDetailByAddress.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Postage Billing Detail by Address")
	End Sub

	Protected Sub cmdPostageBillingByPermit_Click(sender As Object, e As System.EventArgs) Handles cmdPostageBillingByPermit.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Postage Billing by Permit")
	End Sub

	Protected Sub cmdProductionUpdate_Click(sender As Object, e As System.EventArgs) Handles cmdProductionUpdate.Click
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		' This also runs spProductionUpdate
		myCmd.CommandText = "spProductionReportWeeklyGenerate"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure
		myCmd.CommandTimeout = 480	'8 min

		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@reportDate", System.Data.ParameterDirection.Input, SqlDbType.Date, CDate(dtEndDate.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "ReportsManagement, cmdProductionUpdate_Click")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "ReportsManagement, cmdProductionUpdate_Click")
		Finally
			conn.Close()
		End Try

		ja("Update Complete")
	End Sub
End Class
