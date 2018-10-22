Imports System.Net
Imports System.IO
Imports System

Imports System.Data
Imports System.Data.Common
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing

Imports DataUtil
Imports DevExpress.Utils
Imports System.Web.UI.PageAsyncTask

Partial Class System_sys_GetLongLat
    Inherits System.Web.UI.Page

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Select Case Request.HttpMethod
			Case "POST"

			Case "GET"
				hfGeocode("timerRunning") = False
				hfGeocode("waitUntil") = CDate("1/1/2000")

				' Get date/time of last record updated.
				Dim sql As String = "SELECT TOP (1) LastGeocodingUpdate " & _
					"FROM tblAddresses " & _
					"ORDER BY LastGeocodingUpdate DESC"
				hfGeocode("lastUpdate") = SQLExecuteScalar(sql, "Notify Web User")

				' Get count processed in last 25 hours.
				sql = "SELECT COUNT(*) " & _
					"FROM tblAddresses " & _
					"WHERE DateAdd(hh, -25, SYSDATETIME()) < LastGeocodingUpdate"
				hfGeocode("processed") = SQLExecuteScalar(sql, "Notify Web User")
				lblQtyToday.Text = hfGeocode("processed") & " Address" & IIf(hfGeocode("processed") > 1, "es", "") & _
					" geocoded in last 25 hours (" & Format(hfGeocode("lastUpdate"), "MM/dd/yyyy HH:mm:ss") & ")."
				hfGeocode("pos") = 0
			Case "HEAD"

		End Select

    End Sub

	Protected Sub btnStartGeocoding_Click(sender As Object, e As EventArgs)
		If Timer1.Enabled = False Then
			Timer1.Enabled = True
			btnStartGeocoding.Text = "Stop Geocoding"
			btnStartGeocoding.BackColor = Color.Red
		Else
			Timer1.Enabled = False
			btnStartGeocoding.Text = "Start Geocoding"
			btnStartGeocoding.BackColor = ColorTranslator.FromHtml("#ECEDF0")
			lblNotes.Text = ""
			lblQtyToday.Text = ""
		End If
	End Sub

	Protected Sub Timer1_Tick(sender As Object, e As EventArgs)
		' This subroutine is called when the timer expires.
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim addressID As Integer = 0

		' Is there another AddressID to be processed?
		If hfGeocode("pos") < gridMain.VisibleRowCount Then
			addressID = CInt(gridMain.GetRowValues(hfGeocode("pos"), "AddressID").ToString())
			lblNotes.Text = "AddressID: " & addressID.ToString() & " processed..."
			' Geocode the AddressID.
			Geocode(addressID)
		ElseIf hfGeocode("waitUntil") <> CDate("1/1/2000") AndAlso hfGeocode("waitUntil") < Date.Now() Then
			lblNotes.Text = "Done Waiting: " & Date.Now.ToString()
			' Process another day's worth of geocoding.
			gridMain.DataBind()	' refresh the grid with new addresses
			hfGeocode("pos") = 0
			hfGeocode("processed") = 0
			hfGeocode("waitUntil") = CDate("1/1/2000")
		ElseIf hfGeocode("waitUntil") = CDate("1/1/2000") Then
			lblNotes.Text = "Setting up Wait Until: " & hfGeocode("waitUntil").ToString() & " (" & Date.Now.ToString() & ")"
			' Today's geocoding complete.
			If hfGeocode("processed") = 0 Or hfGeocode("processed") < 2395 Then
				' No processing today or we processed less than the maximum, so we must be done.
				lblQtyToday.Text = "There are no addresses to geocode."
			Else
				' Today's geocoding complete. Set up wait for 24 hours.
				hfGeocode("waitUntil") = DateAdd(DateInterval.Day, 1, Date.Now())
				lblQtyToday.Text = "Geocoding complete for today. Waiting until: " & _
					Format(hfGeocode("waitUntil"), "MM/dd/yyyy HH:mm:ss") & "." & vbCrLf & _
					hfGeocode("processed") & " Address" & IIf(hfGeocode("processed") > 1, "es", "") & _
					" geocoded in last 24 hours (" & Format(hfGeocode("lastUpdate"), "MM/dd/yyyy HH:mm:ss") & ")."
			End If
		Else
			' Waiting
			lblNotes.Text = "Waiting Until: " & hfGeocode("waitUntil") & " (" & Date.Now.ToString() & ")"
		End If
	End Sub

	Public Sub Geocode(ByVal addressID As Integer)

		Dim geocodeMessage As String = GeocodeAddress(addressID, _
						gridMain.GetRowValues(hfGeocode("pos"), "StreetAddress").ToString(),
						gridMain.GetRowValues(hfGeocode("pos"), "City").ToString(),
						gridMain.GetRowValues(hfGeocode("pos"), "State").ToString(),
						gridMain.GetRowValues(hfGeocode("pos"), "Zip5").ToString())

		If geocodeMessage <> "" Then
			' Today's geocoding complete. Set up wait for 24 hours.
			hfGeocode("waitUntil") = DateAdd(DateInterval.Day, 1, Date.Now())
			lblQtyToday.Text = "Error in Geocode: " & geocodeMessage & ", Waiting until: " & _
				Format(hfGeocode("waitUntil"), "MM/dd/yyyy HH:mm:ss") & "." & vbCrLf & _
				hfGeocode("processed") & " Address" & IIf(hfGeocode("processed") > 1, "es", "") & _
				" geocoded in last 24 hours (" & Format(hfGeocode("lastUpdate"), "MM/dd/yyyy HH:mm:ss") & ")."
			Return
		End If

		hfGeocode("lastUpdate") = Date.Now
		hfGeocode("processed") += 1
		hfGeocode("pos") += 1
		lblQtyToday.Text = hfGeocode("processed") & " Address" & IIf(hfGeocode("processed") > 1, "es", "") & _
			" geocoded in last 24 hours (" & Format(hfGeocode("lastUpdate"), "MM/dd/yyyy HH:mm:ss") & ")."
	End Sub

End Class
