Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.Internal
Imports System.Data.SqlClient
Imports System.Data.Common
Imports DataUtil

Partial Class MailingDateCalculator
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Select Case Request.HttpMethod
			Case "POST"
			Case "GET"
			Case "HEAD"
		End Select
	End Sub


	Protected Sub btnCalculate_Click(sender As Object, e As EventArgs)
		ClearForm()

		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		If IsNothing(dtPickupDate.Value) Then
			ja("Please select a Pickup Date")
			Return
		End If

		Dim pickupDOW As Integer = Weekday(dtPickupDate.Value)
		tbPickupDOW.Text = WeekdayName(pickupDOW)

		sql = "SELECT OptimumDeliveryDay " & _
			"FROM tblOptimumMailDeliveryDays " & _
			"WHERE PickupDay = '" & tbPickupDOW.Text & "'"
		If Not SqlQueryOpen(conn, rsql, sql, "") Then
			ja("SqlQueryOpen Error on Optimum Delivery Days")
			Return
		End If
		If rsql.Read() Then
			tbOptimumDeliveryDOW.Text = Trim(rsql("OptimumDeliveryDay").ToString())
		Else
			ja("Optimum Delivery Day not found.")
			Return
		End If
		SqlQueryClose(conn, rsql)

		Dim optimumDeliveryDOW As Integer

		Select Case tbOptimumDeliveryDOW.Text
			Case "Sunday"
				optimumDeliveryDOW = 1
			Case "Monday"
				optimumDeliveryDOW = 2
			Case "Tuesday"
				optimumDeliveryDOW = 3
			Case "Wednesday"
				optimumDeliveryDOW = 4
			Case "Thursday"
				optimumDeliveryDOW = 5
			Case "Friday"
				optimumDeliveryDOW = 6
			Case "Saturday"
				optimumDeliveryDOW = 7
		End Select

		Dim daysBeforePickupForOptimumDelivery As Integer = pickupDOW - optimumDeliveryDOW
		If daysBeforePickupForOptimumDelivery <= 0 Then
			daysBeforePickupForOptimumDelivery += 7
		End If

		Dim optimumDeliveryDate As Date = DateAdd(DateInterval.Day, daysBeforePickupForOptimumDelivery * -1, dtPickupDate.Value)

		' Get City and Zip associated with Route
		sql = "SELECT TOP (1) City, Zip5 " & _
			"FROM tlkPrimaryCityZipFromRoute " & _
			"WHERE RouteID = '" & cmbRoutes.SelectedItem.Value & "'"
		If Not SqlQueryOpen(conn, rsql, sql, "") Then
			ja("SqlQueryOpen Error on Primary City Zip From Route")
			Return
		End If
		If rsql.Read() Then
			tbPrimaryCity.Text = rsql("City").ToString()
			tbPrimaryZip5.Text = rsql("Zip5").ToString()
		Else
			ja("City, Zip5 combination not found in tlkPrimaryCityZipFromRoute.")
			Return
		End If
		SqlQueryClose(conn, rsql)

		' Get the Delivery Days and Post Office for this City
		' First try the Zip5
		Dim postOfficeID As Integer
		sql = "SELECT MDR.DeliveryDays, MDR.PostOfficeID, PO.PostOfficeCode " & _
			"FROM tblMailingDateRules AS MDR " & _
			"INNER JOIN tblPostOffices AS PO ON PO.PostOfficeID = MDR.PostOfficeID " & _
			"WHERE City = '" & tbPrimaryCity.Text & "' " & _
				"AND Zip = '" & tbPrimaryZip5.Text & "'"
		If Not SqlQueryOpen(conn, rsql, sql, "") Then
			ja("SqlQueryOpen Error on Primary City Zip From Route")
			Return
		End If
		If rsql.Read() Then
			tbRuleMatchedCity.Text = tbPrimaryCity.Text
			tbRuleMatchedZip.Text = tbPrimaryZip5.Text
			tbDeliveryDays.Text = rsql("DeliveryDays").ToString()
			tbPostOffice.Text = rsql("PostOfficeCode").ToString()
			postOfficeID = CInt(rsql("PostOfficeID").ToString())
		Else
			SqlQueryClose(conn, rsql)
			' Try the Zip3
			sql = "SELECT MDR.DeliveryDays, MDR.PostOfficeID, PO.PostOfficeCode " & _
				"FROM tblMailingDateRules AS MDR " & _
				"INNER JOIN tblPostOffices AS PO ON PO.PostOfficeID = MDR.PostOfficeID " & _
				"WHERE City = '" & tbPrimaryCity.Text & "' " & _
					"AND Zip = '" & Mid(tbPrimaryZip5.Text, 1, 3) & "'"
			If Not SqlQueryOpen(conn, rsql, sql, "") Then
				ja("SqlQueryOpen Error on Primary City Zip From Route 2")
				Return
			End If
			If rsql.Read() Then
				tbRuleMatchedCity.Text = tbPrimaryCity.Text
				tbRuleMatchedZip.Text = Mid(tbPrimaryZip5.Text, 1, 3)
				tbDeliveryDays.Text = rsql("DeliveryDays").ToString()
				tbPostOffice.Text = rsql("PostOfficeCode").ToString()
				postOfficeID = CInt(rsql("PostOfficeID").ToString())
			Else
				ja("Mailing Date Rule not found for City and Zip.")
				Return
			End If
			SqlQueryClose(conn, rsql)
		End If

		' Subtract the Delivery Days from the Delivery Date to get the Mailing Date counting only weekdays
		Dim mailingDate As Date
		sql = "SELECT dbo.ufnDateAdd_NoWeekends(" & tbDeliveryDays.Text & " * -1, " & _
				"'" & Format(optimumDeliveryDate, "MM/dd/yyyy") & "') AS MailingDate"
		If Not SqlQueryOpen(conn, rsql, sql, "") Then
			ja("SqlQueryOpen Error on subtract delivery days")
			Return
		End If
		If rsql.Read() Then
			mailingDate = CDate(rsql("MailingDate").ToString())
			tbTenativeMailingDate.Text = Format(mailingDate, "MM/dd/yyyy")
		Else
			ja("Query Error on subtract delivery days.")
			Return
		End If
		SqlQueryClose(conn, rsql)

		' Subtract the Slow Down Days from the most recent Holiday if applicable to the Delivery Days
		Dim slowDownDays As Integer = 0
		sql = ";WITH Periods AS " & _
			"( " & _
				"SELECT Holiday, HolidayDate, " & _
					"DATEADD(DAY, SlowDownPeriodBefore * -1, HolidayDate) AS PeriodBegin, " & _
					"DATEADD(DAY, SlowDownPeriodAfter, HolidayDate) AS PeriodEnd, " & _
					"SlowDownDays " & _
				"FROM tlkHolidays " & _
				"WHERE HolidayDate > DATEADD(DAY, -60, SYSDATETIME()) " & _
					"AND SlowDownPeriodBefore IS NOT NULL " & _
					"AND SlowDownPeriodAfter IS NOT NULL " & _
					"AND SlowDownDays IS NOT NULL " & _
			") " & _
			"SELECT TOP (1) Holiday, HolidayDate, PeriodBegin, PeriodEnd, SlowDownDays " & _
			"FROM Periods " & _
			"WHERE '" & Format(mailingDate, "MM/dd/yyyy") & "' BETWEEN PeriodBegin AND PeriodEnd " & _
			"ORDER BY HolidayDate"
		If Not SqlQueryOpen(conn, rsql, sql, "") Then
			ja("SqlQueryOpen Error on Holidays")
			Return
		End If
		If rsql.Read() Then
			tbHoliday.Text = rsql("Holiday").ToString
			tbHolidaytDate.Text = Format(CDate(rsql("HolidayDate").ToString()), "MM/dd/yyyy")
			tbSlowDownPeriodStart.Text = Format(CDate(rsql("PeriodBegin").ToString()), "MM/dd/yyyy")
			tbSlowDownPeriodEnd.Text = Format(CDate(rsql("PeriodEnd").ToString()), "MM/dd/yyyy")
			slowDownDays = CInt(rsql("SlowDownDays").ToString())
			tbSlowDownDays.Text = slowDownDays.ToString()
		Else
			slowDownDays = 0
		End If
		SqlQueryClose(conn, rsql)

		mailingDate = DateAdd(DateInterval.Day, slowDownDays * -1, mailingDate)
		tbMailingDateAdjustedForHoliday.Text = Format(mailingDate, "MM/dd/yyyy")

		' Adjust for Weekends and Holidays and Post Office
		sql = "SELECT dbo.ufnAdjustMailingDate('" & Format(mailingDate, "MM/dd/yyyy") & "', " & _
			postOfficeID.ToString() & ") AS MailingDate"
		If Not SqlQueryOpen(conn, rsql, sql, "") Then
			ja("SqlQueryOpen Error ufnAdjustMailingDate")
			Return
		End If
		If rsql.Read() Then
			mailingDate = CDate(rsql("MailingDate").ToString())
		Else
			ja("Query Error on subtract delivery days.")
			Return
		End If
		SqlQueryClose(conn, rsql)

		tbFinalMailingDate.Text = Format(mailingDate, "MM/dd/yyyy")
	End Sub

	Protected Sub ClearForm()
		tbPickupDOW.Text = ""
		tbOptimumDeliveryDOW.Text = ""
		tbPrimaryCity.Text = ""
		tbPrimaryZip5.Text = ""
		tbRuleMatchedCity.Text = ""
		tbRuleMatchedZip.Text = ""
		tbDeliveryDays.Text = ""
		tbPostOffice.Text = ""
		tbTenativeMailingDate.Text = ""
		tbHoliday.Text = ""
		tbHolidaytDate.Text = ""
		tbSlowDownPeriodStart.Text = ""
		tbSlowDownPeriodEnd.Text = ""
		tbSlowDownDays.Text = ""
		tbMailingDateAdjustedForHoliday.Text = ""
		tbFinalMailingDate.Text = ""
	End Sub
End Class
