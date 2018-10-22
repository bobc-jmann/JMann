Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web.UI
Imports DevExpress.Utils
Imports DevExpress.Web.ASPxHtmlEditor
Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports DataUtil


Imports System.Data
Imports System.Data.Sql
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DevExpress.XtraPrinting

Imports DevExpress.Web.Internal

Partial Class MappingPickupsDriver
	Inherits System.Web.UI.Page

	Public googleCoords As String = ""
	Public sectionAddressesCoords As String = ""
	Public driverCoords As String = ""

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					Dim PostBackControlID As String = ""
					Try
						PostBackControlID = GetPostBackControl(Me.Page).ID
					Catch ex As Exception

					End Try
					Select Case PostBackControlID

					End Select
					LoadDriverAssignments()

					googleCoordinates.Value = ""
					sectionAddressesCoordinates.Value = ""
					driverCoordinates.Value = ""

				End If

			Case "GET"
				dtPickupDateFrom.Value = DateAdd(DateInterval.Day, -1, Now)
				dtPickupDateTo.Value = DateAdd(DateInterval.Day, -1, Now)
				ckShowDriverLogs.Checked = False

				Dim sql As String = "SELECT RegionID, RegionDesc FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc"
				Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
				Dim dt As DataTable = New DataTable()
				da.Fill(dt)
				ddlDriverLocations.DataSource = dt
				ddlDriverLocations.DataTextField = "RegionDesc"
				ddlDriverLocations.DataValueField = "RegionID"
				ddlDriverLocations.DataBind()
				ddlDriverLocations.Items.Insert(0, New ListItem("All Locations", "ALL"))
				ddlDriverLocations.Items.Insert(0, New ListItem("Select Driver Location", "0"))
				If Session("UserRegionDefault") <> 0 Then
					ddlDriverLocations.SelectedValue = Session("UserRegionDefault")
				End If
			Case "HEAD"
				'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
		End Select

	End Sub

	Protected Sub ddlDriverLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriverLocations.SelectedIndexChanged
		LoadDriverAssignments()
	End Sub

	Protected Sub dtPickupDateFrom_DateChanged(sender As Object, e As EventArgs) Handles dtPickupDateFrom.DateChanged
		LoadDriverAssignments()
	End Sub

	Protected Sub dtPickupDateTo_DateChanged(sender As Object, e As EventArgs) Handles dtPickupDateTo.DateChanged
		LoadDriverAssignments()
	End Sub

	Private Sub LoadDriverAssignments()
		Dim sql As String = "SELECT DA.DriverAssignmentID, PS.PickupDate, " & _
				"RG.RegionID, RG.RegionCode, PS.RouteID, PS.RouteCode, DA.SectionID, DA.SectionCode, " & _
				"D.DriverName, DA.CntPickupsDriver, DA.CntPickupsAddresses, Da.SoftCarts, DA.HardCarts, DA.TotalCarts, " & _
				"PSS.CntPutOuts, " & _
				"CASE WHEN PSS.CntPutOuts = 0 THEN 0 ELSE (CAST(DA.CntPickupsDriver AS FLOAT) / PSS.CntPutOuts) END AS PickupPercent, " & _
				"CASE WHEN PSS.CntPutOuts = 0 THEN 0 ELSE ((CAST(DA.TotalCarts AS FLOAT) * 1000) / PSS.CntPutOuts) END AS CartsPer1000 " & _
			"FROM tblPickupSchedule AS PS " & _
			"INNER JOIN tblDriverAssignments AS DA ON DA.PickupScheduleID = PS.PickupScheduleID " & _
			"INNER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleID = PS.PickupScheduleID " & _
				"AND PSS.SectionID = DA.SectionID " & _
			"INNER JOIN tblDrivers AS D ON D.DriverID = DA.DriverID " & _
			"INNER JOIN tlkRegions AS RG ON RG.RegionID = DA.LocationID " & _
			"WHERE PS.PickupDate BETWEEN '" & _
				Format(dtPickupDateFrom.Value, "yyyyMMdd") & "' AND '" & Format(dtPickupDateTo.Value, "yyyyMMdd") & "' " & _
				"AND PS.ScheduleTypeID IN (1, 4, 5) " & _
				"AND DA.SectionID > 0 " & _
				"AND DA.CntPickupsDriver > 0 " & _
				"AND DA.LocationID = " & ddlDriverLocations.SelectedValue & " " & _
			"ORDER BY PickupDate DESC, RegionCode, RouteCode, SectionCode, DriverName"

		dsDriverAssignments.SelectCommand = sql
		grdDriverAssignments.DataBind()
	End Sub

	Sub BuildSections(ByVal sectionID As Integer)
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim concaveHull As String = ""
		Dim sectionCode As String = ""
		Dim routeCode As String = ""

		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		sql = "SELECT R.RouteCode, S.SectionCode, SG.ConcaveHull.ToString() AS ConcaveHull " & _
				"FROM tblSections AS S " & _
				"INNER JOIN tblSectionsGeography AS SG ON SG.SectionID = S.SectionID " & _
				"INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID " & _
				"WHERE S.SectionID = " & sectionID & " " & _
					"AND SG.ConcaveHull.ToString() <> 'GEOMETRYCOLLECTION EMPTY'"

		Try
			If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
				Return
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			ja(ex.Message)
			Return
		End Try
		While rsql.Read()
			concaveHull = rsql("ConcaveHull")
			sectionCode = rsql("SectionCode")
			routeCode = rsql("RouteCode")

			If googleCoords <> "" Then
				googleCoords += ", "
			End If
			googleCoords += ConvertPolygonToGoogleCoords(routeCode, sectionCode, concaveHull, 0)
		End While
		SqlQueryClose(connSQL, rsql)
	End Sub


	Sub BuildSectionAddresses(ByVal driverAssignmentID As Integer)
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim rsql As SqlDataReader = Nothing

		Dim sectionID As Integer = grdDriverAssignments.GetRowValuesByKeyValue(driverAssignmentID, "SectionID")
		Dim sectionCode As String = grdDriverAssignments.GetRowValuesByKeyValue(driverAssignmentID, "SectionCode")

		BuildSections(sectionID)

		Dim color As String = ""
		color = SectionColor(sectionCode, 0)

		Dim parms As List(Of QueryParms) = New List(Of QueryParms)
		Dim p As QueryParms = New QueryParms
		p.parmName = "@driverAssignmentID"
		p.dbType = SqlDbType.Int
		p.value = driverAssignmentID
		parms.Add(p)

		Try
			rsql = GetReader(connSQL, vConnStr, "spMappingAddressesPickups", parms)
			If IsNothing(rsql) Then
				Return
			End If
			While rsql.Read()
				If sectionAddressesCoords <> "" Then
					sectionAddressesCoords += ", "
				End If

				Dim streetAddress As String = rsql("StreetAddress")
				Dim pickup As Integer = rsql("Pickup")
				If pickup = 0 Then
					streetAddress = ""	' Do not send address text if it is not going to be displayed.
				End If

				sectionAddressesCoords += "[" & rsql("Lat") & "," & rsql("Long") & ",""" & _
					color & """,""" & streetAddress & """," & pickup & "]"
			End While
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try
	End Sub


	Public Function ConvertPolygonToGoogleCoords(ByVal routeCode As String, _
								ByVal sectionCode As String, ByVal polygon As String, i As Integer) As String
		Dim s As String = ""
		Dim sqlPoints() As String
		Dim sqlPoly() As String
		Dim longLat() As String
		Dim polyCoords As String = ""
		Dim googleCoords As String = ""

		Dim color As String = ""
		color = SectionColor(sectionCode, i)

		s = polygon.Replace("MULTIPOLYGON (((", "")
		s = s.Replace("POLYGON ((", "")
		sqlPoly = Split(s, ")),")
		For Each poly As String In sqlPoly
			s = poly.Replace("(", "")
			s = s.Replace(")", "")
			sqlPoints = Split(s, ",")

			If sectionCode = "" Or sectionCode = "crrt" Then
				polyCoords = "[""" & routeCode & """, """ & color & """]"
			Else
				polyCoords = "[""" & routeCode & "-" & sectionCode & """, """ & color & """]"
			End If
			For Each point As String In sqlPoints
				longLat = Split(LTrim(point))
				polyCoords += ", [" & longLat(1) & ", " & longLat(0) & "]"
			Next
			If googleCoords <> "" Then
				googleCoords += ", "
			End If
			googleCoords += "[" & polyCoords & "]"
		Next

		Return googleCoords
	End Function

	Public Function SectionColor(ByVal SectionCode As String, ByVal i As Integer) As String
		Dim color As String = ""
		Dim colorNum As String = ""

		If SectionCode = "" Then
			colorNum = (i Mod 6) + 1
		Else
			Dim c As Integer = NumericOnly(SectionCode)
			If c = 99 Then
				colorNum = 99
			Else
				colorNum = (c Mod 6)
			End If
		End If

		Select Case colorNum
			Case "1"
				color = "orange"
			Case "2"
				color = "green"
			Case "3"
				color = "red"
			Case "4"
				color = "blue"
			Case "5"
				color = "purple"
			Case "0" ' 6
				color = "yellow"
			Case Else
				color = "grey"
		End Select

		Return color
	End Function

	Protected Sub btnDrawMap_Click(sender As Object, e As EventArgs) Handles btnDrawMap.Click
		Dim changeList As List(Of Object) = grdDriverAssignments.GetSelectedFieldValues("DriverAssignmentID")
		If changeList.Count = 0 Then
			ja("Please select at least one Section to draw.")
			Return
		End If

		googleCoords = ""
		googleCoordinates.Value = ""
		sectionAddressesCoords = ""
		sectionAddressesCoordinates.Value = ""

		For Each item As Object In changeList
			BuildSectionAddresses(item)
		Next

		If ckShowDriverLogs.Checked Then
			BuildDriverPositions(changeList)
		End If

		googleCoordinates.Value = "[ " & googleCoords & " ]"
		sectionAddressesCoordinates.Value = "[ " & sectionAddressesCoords & " ]"
		If driverCoords.Length > 0 Then
			driverCoordinates.Value = "[ " & driverCoords & " ]"
		End If
	End Sub

	Sub BuildDriverPositions(ByVal changeList As List(Of Object))
		Dim driverAssignmentIDs As String = ""
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim rsql As SqlDataReader = Nothing

		For Each item As Object In changeList
			If driverAssignmentIDs <> "" Then
				driverAssignmentIDs &= ", "
			End If
			driverAssignmentIDs += item.ToString()
		Next

		Dim parms As List(Of QueryParms) = New List(Of QueryParms)
		Dim p As QueryParms = New QueryParms
		p.parmName = "@driverAssignmentIDs"
		p.dbType = SqlDbType.VarChar
		p.size = 255
		p.value = driverAssignmentIDs
		parms.Add(p)

		Dim currentDriver As Integer = 0
		Dim currentDate As Date = #1/1/1990#

		Try
			rsql = GetReader(connSQL, vConnStr, "spMappingDriverLogs", parms)
			If IsNothing(rsql) Then
				Return
			End If

			While rsql.Read()
				Dim driverID As Integer = CInt(rsql("DriverID"))
				Dim dDate As Date = CDate(rsql("Date"))
				If driverID <> currentDriver Or dDate <> currentDate Then
					If driverCoords <> "" Then
						driverCoords += "],"
					End If
					driverCoords += "["
					currentDriver = driverID
					currentDate = dDate
				Else
					driverCoords += ", "
				End If

				driverCoords += "[" & rsql("Lat").ToString() & "," & rsql("Long").ToString() & "]"
			End While
			If driverCoords <> "" Then
				driverCoords += "]"
			End If
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try
	End Sub



End Class
