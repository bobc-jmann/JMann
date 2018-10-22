Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DevExpress.Utils
Imports DataUtil


Partial Class BaggerAssignments
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Dim sql As String = ""

		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Dim PostBackControlID As String = ""
		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					Try
						PostBackControlID = GetPostBackControl(Me.Page).ID
					Catch ex As Exception

					End Try

					Select Case PostBackControlID
						Case Else

					End Select
				End If
			Case "GET"
				dtPickupDate.Value = DateValue(Now)

				sql = "SELECT RegionID, RegionDesc FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc"
				Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
				Dim dt As DataTable = New DataTable()
				da.Fill(dt)
				ddlBaggerLocations.DataSource = dt
				ddlBaggerLocations.DataTextField = "RegionDesc"
				ddlBaggerLocations.DataValueField = "RegionID"
				ddlBaggerLocations.DataBind()
				ddlBaggerLocations.Items.Insert(0, New ListItem("Select Bagger Location", "0"))
				If Session("UserRegionDefault") <> 0 Then
					ddlBaggerLocations.SelectedValue = Session("UserRegionDefault")
				End If

			Case "HEAD"
				'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
		End Select

		dsBagger.SelectCommand = "SELECT DriverID, DriverName FROM [tblDrivers] " & _
			"WHERE (DriverLocationID = '" & ddlBaggerLocations.SelectedValue & "' OR DriverLocationID IS NULL) " & _
				"AND Active = 1 " & _
				"AND Bagger = 1 " & _
			"ORDER BY DriverName"
		Dim baggerID_column = TryCast(grdBaggerAssignments.DataColumns("BaggerDisplayID"), GridViewDataComboBoxColumn)
		baggerID_column.PropertiesComboBox.RequireDataBinding()
		dsBagger.DataBind()
		dsBaggerEdit.SelectCommand = "SELECT DriverID, DriverName FROM [tblDrivers] " & _
			"WHERE (DriverLocationID = '" & ddlBaggerLocations.SelectedValue & "' OR DriverLocationID IS NULL) " & _
				"AND Active = 1 " & _
				"AND Bagger = 1 " & _
			"ORDER BY DriverName"
		Dim baggerEditID_column = TryCast(grdBaggerAssignments.DataColumns("BaggerID"), GridViewDataComboBoxColumn)
		baggerEditID_column.PropertiesComboBox.RequireDataBinding()
		dsBaggerEdit.DataBind()
		cmbBaggers.DataBind()
		dsVehicles.SelectCommand = "SELECT TruckID AS BaggerVehicleID, TruckNumber FROM [tblTrucks] " & _
			"WHERE DriverLocationID = '" & ddlBaggerLocations.SelectedValue & "' OR DriverLocationID IS NULL " & _
				"AND Active = 1 " & _
				"AND Vehicle = 1 " & _
			"ORDER BY TruckNumber"
		Dim baggerVehicleID_column = TryCast(grdBaggerAssignments.DataColumns("BaggerVehicleID"), GridViewDataComboBoxColumn)
		baggerVehicleID_column.PropertiesComboBox.RequireDataBinding()
		dsVehicles.DataBind()

		' Insert rows for Route-Sections into Driver Assignments for current date
		sql = "INSERT INTO tblDriverAssignments (PickupScheduleSectionID, PickupScheduleID, PickupDate, RouteID, RouteCode, RouteDesc, SectionID, SectionCode, SectionDesc) " & _
			"SELECT PSS.PickupScheduleSectionID, PS.PickupScheduleID, PS.PickupDate, PS.RouteID, PS.RouteCode, PS.RouteDesc, PSS.SectionID, PSS.SectionCode, PSS.SectionDesc " & _
			"FROM tblPickupScheduleSections AS PSS " & _
			"INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSS.PickupScheduleID " & _
			"INNER JOIN tblSections AS S ON S.SectionID = PSS.SectionID " & _
			"WHERE PS.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
				"AND S.Active = 1 " & _
				"AND (PSS.CntMail > 0 OR PSS.CntBag > 0 OR PSS.CntPostcard > 0) " & _
				"AND NOT EXISTS (SELECT * FROM tblDriverAssignments DA " & _
					"WHERE DA.PickupScheduleSectionID = PSS.PickupScheduleSectionID " & _
						"AND SectionID > 0) " & _
			"GROUP BY PSS.PickupScheduleSectionID, PS.PickupScheduleID, PS.PickupDate, PS.RouteID, PS.RouteCode, PS.RouteDesc, PSS.SectionID, PSS.SectionCode, PSS.SectionDesc"
		SqlNonQuery(sql, "BaggerAssignments, Page_Load")

		LoadAssignments()
	End Sub

	Private Function SelectAssignments(ByVal whereClause As String) As String
		Dim sql As String = "SELECT CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END AS PreSort, LEN(DA.SectionCode), " & _
				"DA.PickupDate, DA.RouteID, DA.RouteCode, DA.RouteDesc, DA.SectionID, DA.SectionCode, DA.SectionDesc, " & _
				"DA.RouteCode + '-' + DA.SectionCode AS [Route-Section] " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
			"INNER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleSectionID = DA.PickupScheduleSectionID " & _
			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleId = DA.PickupScheduleID " & _
			whereClause & "AND DA.SectionID > 0 AND NTBP.PickupScheduleID IS NULL " & _
			"ORDER BY CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END, DA.RouteCode, LEN(DA.SectionCode), DA.SectionCode"

		dsSections.SelectCommand = sql
		ddlSections.DataBind()

		sql = "SELECT CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END AS PreSort, DA.DriverAssignmentID, DA.PickupScheduleSectionID, DA.PickupScheduleID, " & _
				"DA.RouteID, DA.RouteCode, DA.RouteDesc, DA.SectionID, DA.SectionCode, DA.SectionDesc, " & _
				"DA.RouteCode + '-' + DA.SectionCode AS [Route-Section], DA.DriverID, DA.BaggerID, DA.BaggerID AS BaggerDisplayID, DA.BaggerVehicleID, " & _
				"PSS.CntTotalAddresses, LEN(DA.SectionCode), " & _
				"CASE WHEN NTBP.PickupScheduleID IS NULL THEN '' ELSE 'NT' END AS NonTabletBagPickup, " & _
				"CASE WHEN PSEC.PickupsSectionID IS NULL THEN 0 ELSE 1 END AS PickupsSectionExists " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
			"INNER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleID = DA.PickupScheduleID AND PSS.SectionID = DA.SectionID " & _
			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleId = DA.PickupScheduleID " & _
			"LEFT OUTER JOIN tblPickupsSections AS PSEC ON PSEC.PickupScheduleSectionID = DA.PickupScheduleSectionID " & _
				"AND PSEC.DriverID = DA.DriverID " & _
			whereClause & "AND DA.SectionID > 0 " & _
			"ORDER BY CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END, DA.RouteCode, LEN(DA.SectionCode), DA.SectionCode"

		Return (sql)
	End Function

	Private Sub LoadAssignments()
		If ddlBaggerLocations.SelectedValue = "0" Then
			Return
		End If

		Dim whereClause As String = "WHERE ((DA.BaggerID IS NULL AND PCDL.RegionID = " & ddlBaggerLocations.SelectedValue & ") " & _
			"OR DA.LocationID = " & ddlBaggerLocations.SelectedValue & ") " & _
			"AND PSS.CntBag > 0 " & _
			"AND PS.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' "
		hfBaggerAssignmentsSelectCommand.Value = SelectAssignments(whereClause)

		dsBaggerAssignments.SelectCommand = hfBaggerAssignmentsSelectCommand.Value
		grdBaggerAssignments.DataBind()

		'Fill labels if missing information
		Dim mDrivers As Integer = 0
		Dim mTablets As Integer = 0

		For i As Integer = 0 To grdBaggerAssignments.VisibleRowCount - 1
			If grdBaggerAssignments.GetRowValues(i, "BaggerID").ToString = "" And grdBaggerAssignments.GetRowValues(i, "SectionID") > 0 Then
				mDrivers += 1
			End If
		Next

		If mDrivers > 0 Then
			lblMissingBaggers.Text = mDrivers.ToString + " record(s) do not have Baggers assigned."
		Else
			lblMissingBaggers.Text = ""
		End If

	End Sub

	Public Class MissingInfo
		Public Baggers As Integer = 0
	End Class

	<WebMethod()> _
	Public Shared Function GetMissingInfo(ByVal dtPickupDate As Object, ByVal driverLocationID As Object) As MissingInfo
		Dim sql As String
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim mi As MissingInfo = New MissingInfo

		Dim s1, s2 As String

		s1 = "SELECT COUNT(*) AS Cnt " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"WHERE PCDL.RegionID = " & driverLocationID & " " & _
				"AND DA.PickupDate = '" & Format(dtPickupDate, "MM/dd/yyyy") & "' " & _
				"AND "

		s2 = "SELECT COUNT(*) AS Cnt " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"WHERE PCDL.RegionID = " & driverLocationID & " " & _
				"AND DA.PickupDate = '" & Format(dtPickupDate, "MM/dd/yyyy") & "' " & _
				"AND DA.SectionID = 0 " & _
				"AND "

		Try
			sql = s1 + "BaggerID IS NULL"
			If Not SqlQueryOpen(connSQL, rsql, sql, "Notify Web User") Then
				Return mi
			End If
			rsql.Read()
			mi.Baggers = rsql("Cnt")
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
		End Try

		Return mi
	End Function

	<WebMethod()> _
	Public Shared Function GetBaggerInfo(ByVal baggerID As Integer) As String()
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim ids(2) As String
		Dim sql As String = "SELECT TruckNumber " & _
			"FROM tblDrivers D " & _
			"LEFT OUTER JOIN tblTrucks TR ON TR.TruckID = D.TruckID " & _
			"WHERE DriverID = " & baggerID
		If Not SqlQueryOpen(connSQL, rsql, sql, "Notify Web User") Then
			Return ids
		End If
		If rsql.Read() Then
			Try
				ids(2) = rsql("TruckNumber")
			Catch ex As Exception
				ids(2) = ""
			End Try
		End If
		SqlQueryClose(connSQL, rsql)


		Try

		Catch ex As Exception

		End Try

		Return ids
	End Function


	<WebMethod()> _
	Public Shared Function SaveRouteSectionSelections(ByVal pickupDate As Date, ByVal baggerID As Integer, ByVal routeSectionsText As String) As Integer
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim routeSections() As String = Split(routeSectionsText, ";")

		' Get Bagger Info
		Dim truckID As Integer = Nothing

		Dim sql = "SELECT TruckID FROM tblDrivers WHERE DriverID = " & baggerID
		If Not SqlQueryOpen(connSQL, rsql, sql, "Notify Web User") Then
			Return -1
		End If
		If Not rsql.Read() Then
			Return 1
		End If

		Try
			truckID = rsql("TruckID")
		Catch ex As Exception
			truckID = -1
		End Try
		SqlQueryClose(connSQL, rsql)

		For Each routeSection In routeSections
			Dim pos As Integer = 0
			Dim routeCode As String = ""
			Dim sectionCode As String = ""

			sql = "UPDATE tblDriverAssignments " & _
				"SET [LocationID] = (SELECT DriverLocationID FROM [tblDrivers] WHERE DriverID = " & baggerID & "), " & _
					"BaggerID = " & baggerID

			If truckID <> -1 Then
				sql += ", BaggerVehicleID = " & truckID
			Else
				sql += ", BaggerVehicleID = NULL "
			End If

			sql += ", LocationUnloadedID = CASE " & _
					"WHEN (LocationUnloadedID IS NULL " & _
						"OR (SELECT RegionID FROM tblLocations WHERE LocationID = LocationUnloadedID) <> " & _
							"(SELECT DriverLocationID FROM [tblDrivers] WHERE DriverID = " & baggerID & ")) " & _
					"THEN (SELECT TOP (1) LocationID FROM tblLocations " & _
						"WHERE RegionID = (SELECT DriverLocationID FROM [tblDrivers] " & _
							"WHERE DriverID = " & baggerID & ") AND DefaultLocation = 1) " & _
					"ELSE LocationUnloadedID END "

			sql += " WHERE DriverAssignmentID IN " & _
				"(SELECT DriverAssignmentID FROM tblDriverAssignments DA " & _
					"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID "

			pos = InStrRev(routeSection, "-")
			routeCode = (Mid(routeSection, 1, pos - 1))
			sectionCode = Mid(routeSection, pos + 1)

			sql += "WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
						"AND DA.RouteCode = '" & routeCode & "' " & _
						"AND DA.SectionCode = '" & sectionCode & "')"

			SqlNonQuery(sql)
		Next

		Return 0
	End Function

	Protected Sub ddlBaggerLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBaggerLocations.SelectedIndexChanged
		LoadAssignments()
	End Sub

	Protected Sub dtPickupDate_DateChanged(sender As Object, e As EventArgs) Handles dtPickupDate.DateChanged
		LoadAssignments()
	End Sub

	Protected Sub grdBaggerAssignments_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs)
		Try
			Dim nt As String = CStr(grdBaggerAssignments.GetRowValuesByKeyValue(e.EditingKeyValue, "NonTabletBagPickup"))
			If nt = "NT" Then
				grdBaggerAssignments.JSProperties("cpServerMessage") = "A Non-Tablet Bag Pickup cannot be modified here."
				e.Cancel = True
			End If
		Catch
		End Try
	End Sub

	Protected Sub grdBaggerAssignments_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		Try
			grdBaggerAssignments.JSProperties("cpServerMessage") = Nothing
			If e.Values("NonTabletBagPickup") = "NT" Then
				grdBaggerAssignments.JSProperties("cpServerMessage") = "A Non-Tablet Bag Pickup cannot be modified here."
				e.Cancel = True
			End If
			If Not IsDBNull(e.Values("DriverID")) Then
				grdBaggerAssignments.JSProperties("cpServerMessage") = "Cannot delete because Driver information exists in record."
				e.Cancel = True
			End If
		Catch
		End Try
	End Sub

	Protected Sub grdBaggerAssignments_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles grdBaggerAssignments.CustomButtonCallback
		If e.ButtonID = "cbAddBagger" Then
			Dim sql As String = "INSERT INTO tblDriverAssignments (PickupScheduleSectionID, PickupScheduleID, PickupDate, " & _
					"RouteID, RouteCode, RouteDesc, SectionID, SectionCode, SectionDesc) " & _
				"VALUES (" & grdBaggerAssignments.GetRowValues(e.VisibleIndex, "PickupScheduleSectionID") & ", " & _
					grdBaggerAssignments.GetRowValues(e.VisibleIndex, "PickupScheduleID") & ", '" & _
					dtPickupDate.Value & "', " & _
					grdBaggerAssignments.GetRowValues(e.VisibleIndex, "RouteID") & ", '" & _
					grdBaggerAssignments.GetRowValues(e.VisibleIndex, "RouteCode") & "', '" & _
					grdBaggerAssignments.GetRowValues(e.VisibleIndex, "RouteDesc") & "', " & _
					grdBaggerAssignments.GetRowValues(e.VisibleIndex, "SectionID") & ", '" & _
					grdBaggerAssignments.GetRowValues(e.VisibleIndex, "SectionCode") & "', '" & _
					grdBaggerAssignments.GetRowValues(e.VisibleIndex, "SectionDesc") & "')"
			SqlNonQuery(sql, "BaggerAssignments, grdDriverAssignments_CustomButtonBallback, cbAddBagger")
			LoadAssignments()
		End If
	End Sub

	Protected Sub grdBaggerAssignments_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		Select Case e.ButtonType
			Case ColumnCommandButtonType.Delete
				e.Visible = DeleteButtonVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex)
			Case ColumnCommandButtonType.Edit
				e.Visible = NTVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex)
		End Select
	End Sub

	Private Function DeleteButtonVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim row As Object = grid.GetRow(visibleIndex)
		Return (CType(row, DataRowView))("PickupsSectionExists").ToString() <> "1" And _
			(CType(row, DataRowView))("NonTabletBagPickup").ToString() <> "NT"
	End Function

	Protected Sub grdBaggerAssignments_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		If e.ButtonID = "cbAddBagger" And Not NTVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
	End Sub

	Private Function NTVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim row As Object = grid.GetRow(visibleIndex)
		Return (CType(row, DataRowView))("NonTabletBagPickup").ToString() <> "NT"
	End Function

	Protected Sub btnRunMissingBaggerInformationReport_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, dtPickupDate.Value) & _
			"|endDate~" & dtPickupDate.Value & "|driverLocation~" & ddlBaggerLocations.SelectedItem.Text
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Bagger Information" & parms)
	End Sub

End Class
