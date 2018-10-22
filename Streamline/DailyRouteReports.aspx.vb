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

Partial Class DailyRouteReports
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
				ddlDriverLocations.DataSource = dt
				ddlDriverLocations.DataTextField = "RegionDesc"
				ddlDriverLocations.DataValueField = "RegionID"
				ddlDriverLocations.DataBind()
				ddlDriverLocations.Items.Insert(0, New ListItem("Select Driver Location", "0"))
				If Session("UserRegionDefault") <> 0 Then
					ddlDriverLocations.SelectedValue = Session("UserRegionDefault")
				End If

			Case "HEAD"
				'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
		End Select

		sql = "SELECT LocationID, LocationAbbr " & _
			"FROM tblLocations " & _
			"WHERE RegionID = " & ddlDriverLocations.SelectedValue & " " & _
				"AND InventoryLocation = 1 " & _
				"AND Active = 1"
		dsLocations.SelectCommand = sql
		Dim locationID_column = TryCast(grdMain.DataColumns("LocationUnloadedID"), GridViewDataComboBoxColumn)
		locationID_column.PropertiesComboBox.RequireDataBinding()
		dsLocations.DataBind()
		'grdMain.DataBind()

		dsDriver.SelectCommand = "SELECT 0 AS DriverID, '' AS DriverName UNION " & _
			"SELECT DriverID, DriverName FROM [tblDrivers] " & _
			"WHERE (DriverLocationID = '" & ddlDriverLocations.SelectedValue & "' OR DriverLocationID IS NULL) " & _
				"AND Active = 1 " & _
				"AND Driver = 1 " & _
			"ORDER BY DriverName"
		Dim driverID_column = TryCast(grdMain.DataColumns("DriverID"), GridViewDataComboBoxColumn)
		driverID_column.PropertiesComboBox.RequireDataBinding()
		dsDriver.DataBind()

		dsBagger.SelectCommand = "SELECT 0 AS DriverID, '' AS DriverName UNION " & _
			"SELECT DriverID, DriverName FROM [tblDrivers] " & _
			"WHERE (DriverLocationID = '" & ddlDriverLocations.SelectedValue & "' OR DriverLocationID IS NULL) " & _
				"AND Active = 1 " & _
				"AND Bagger = 1 " & _
			"ORDER BY DriverName"
		Dim baggerID_column = TryCast(grdMain.DataColumns("BaggerID"), GridViewDataComboBoxColumn)
		baggerID_column.PropertiesComboBox.RequireDataBinding()
		dsBagger.DataBind()

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
					"WHERE DA.PickupScheduleSectionID = PSS.PickupScheduleSectionID) " & _
			"GROUP BY PSS.PickupScheduleSectionID, PS.PickupScheduleID, PS.PickupDate, PS.RouteID, PS.RouteCode, PS.RouteDesc, PSS.SectionID, PSS.SectionCode, PSS.SectionDesc"
		SqlNonQuery(sql, "DailyRouteReports, Page_Load")

		LoadAssignments()
	End Sub

	Private Function SelectAssignments(ByVal whereClause As String) As String

		Dim sql As String = "SELECT CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END AS PreSort, DA.DriverAssignmentID, DA.PickupScheduleSectionID, " & _
				"DA.PickupScheduleID, DA.RouteID, DA.RouteCode, DA.RouteDesc, DA.SectionID, DA.SectionCode, DA.SectionDesc, DA.LocationUnloadedID, " & _
				"DA.TabletID, DA.PhoneID, DA.TruckID, DA.BaggerID, DA.CntPutOutsBagger, " & _
				"DA.RouteCode + '-' + DA.SectionCode AS [Route-Section], DA.DriverID, D.DriverName, " & _
				"DA.CntPickupsDriver, DA.CntPickupsAddresses, DA.SoftCarts, DA.HardCarts, DA.TotalCarts, LEN(DA.SectionCode), " & _
				"CASE WHEN NTBP.PickupScheduleID IS NULL THEN '' ELSE 'NT' END AS NonTabletBagPickup, " & _
				"CASE WHEN PSEC.PickupsSectionID IS NULL THEN 0 ELSE 1 END AS PickupsSectionExists, " & _
				"PSS.CntBag " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
			"LEFT OUTER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleID = DA.PickupScheduleID " & _
				"AND PSS.SectionID = DA.SectionID " & _
			"LEFT OUTER JOIN tblDrivers AS D ON D.DriverID = DA.DriverID " & _
			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleId = DA.PickupScheduleID " & _
			"LEFT OUTER JOIN tblPickupsSections AS PSEC ON PSEC.PickupScheduleSectionID = DA.PickupScheduleSectionID " & _
				"AND PSEC.DriverID = DA.DriverID " & _
			whereClause & "AND DA.SectionID > 0 " & _
			"UNION " & _
			"SELECT CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END AS PreSort, DA.DriverAssignmentID, DA.PickupScheduleSectionID, " & _
				"DA.PickupScheduleID, DA.RouteID, DA.RouteCode, DA.RouteDesc, DA.SectionID, DA.SectionCode, DA.SectionDesc, DA.LocationUnloadedID, " & _
				"DA.TabletID, DA.PhoneID, DA.TruckID, DA.BaggerID, DA.CntPutOutsBagger, " & _
				"RGP.RegionCode + '-Specials' AS [Route-Section], DA.DriverID, D.DriverName, " & _
				"DA.CntPickupsDriver, DA.CntPickupsAddresses, DA.SoftCarts, DA.HardCarts, DA.TotalCarts, LEN(DA.SectionCode), " & _
				"CASE WHEN NTBP.PickupScheduleID IS NULL THEN '' ELSE 'NT' END AS NonTabletBagPickup, " & _
				"CASE WHEN PSEC.PickupsSectionID IS NULL THEN 0 ELSE 1 END AS PickupsSectionExists, " & _
				"0 AS CntBag " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
			"LEFT OUTER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleID = DA.PickupScheduleID " & _
				"AND PSS.SectionID = DA.SectionID " & _
			"LEFT OUTER JOIN tblDrivers AS D ON D.DriverID = DA.DriverID " & _
			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDLP ON PCDLP.PickupCycleID = PC.PickupCycleID " & _
			"INNER JOIN tlkRegions RGP ON RGP.RegionID = PCDLP.RegionID " & _
			"LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleId = DA.PickupScheduleID " & _
			"LEFT OUTER JOIN tblPickupsSections AS PSEC ON PSEC.PickupScheduleSectionID = DA.PickupScheduleSectionID " & _
				"AND PSEC.DriverID = DA.DriverID " & _
			whereClause & "AND DA.SectionID = 0 " & _
				"AND PCDLP.PrimaryRegion = 1 " & _
			"ORDER BY CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END, DA.RouteCode, LEN(DA.SectionCode), DA.SectionCode"

		Return (sql)
	End Function

	Private Sub LoadAssignments()
		If ddlDriverLocations.SelectedValue = "0" Then
			Return
		End If

		Dim whereClause As String = "WHERE ((DA.DriverID IS NULL AND PCDL.RegionID = " & ddlDriverLocations.SelectedValue & ") " & _
			"OR DA.LocationID = " & ddlDriverLocations.SelectedValue & ") " & _
			"AND PS.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' "
		hfRouteReportSelectCommand.Value = SelectAssignments(whereClause)

		dsRouteReport.SelectCommand = hfRouteReportSelectCommand.Value
		grdMain.DataBind()

		'Fill labels if missing information
		Dim mPickupsDriver As Integer = 0
		Dim mSoftCarts As Integer = 0
		Dim mHardCarts As Integer = 0
		Dim mLocationsUnloaded As Integer = 0

		For i As Integer = 0 To grdMain.VisibleRowCount - 1
			If grdMain.GetRowValues(i, "CntPickupsDriver").ToString = "" Then
				mPickupsDriver += 1
			End If
			If grdMain.GetRowValues(i, "SoftCarts").ToString = "" Then
				mSoftCarts += 1
			End If
			If grdMain.GetRowValues(i, "HardCarts").ToString = "" Then
				mHardCarts += 1
			End If
			If grdMain.GetRowValues(i, "LocationUnloadedID").ToString = "" Then
				mLocationsUnloaded += 1
			End If
		Next

		If mPickupsDriver > 0 Then
			lblMissingPickupsDriver.Text = mPickupsDriver.ToString + " record(s) do not have Driver Pickups entered."
		Else
			lblMissingPickupsDriver.Text = ""
		End If
		If mSoftCarts > 0 And DateValue(Now) > dtPickupDate.Value Then
			lblMissingSoftCarts.Text = mSoftCarts.ToString + " record(s) do not have Soft Carts entered."
		Else
			lblMissingSoftCarts.Text = ""
		End If
		If mHardCarts > 0 And DateValue(Now) > dtPickupDate.Value Then
			lblMissingHardCarts.Text = mHardCarts.ToString + " record(s) do not have Hard Carts entered."
		Else
			lblMissingHardCarts.Text = ""
		End If
		If mLocationsUnloaded > 0 And DateValue(Now) > dtPickupDate.Value Then
			lblMissingLocationsUnloaded.Text = mLocationsUnloaded.ToString + " record(s) do not have a Location Unloaded entered."
		Else
			lblMissingLocationsUnloaded.Text = ""
		End If

	End Sub

	Public Class MissingInfo
		Public PickupsDriver As Integer = 0
		Public SoftCarts As Integer = 0
		Public HardCarts As Integer = 0
		Public LocationsUnloaded As Integer = 0
	End Class

	<WebMethod()> _
	Public Shared Function GetMissingInfo(ByVal dtPickupDate As Object, ByVal driverLocationID As Object) As MissingInfo
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim mi As MissingInfo = New MissingInfo
		Dim sql As String
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
			sql = s1 + "CntPickupsDriver IS NULL"
			If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
				Return mi
			End If
			rs.Read()
			mi.PickupsDriver = rs("Cnt")
			SqlQueryClose(connSQL, rs)
		Catch ex As Exception
		End Try

		Try
			If DateValue(Now) > dtPickupDate Then
				sql = s1 + "SoftCarts IS NULL"
				If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
					Return mi
				End If
				rs.Read()
				mi.SoftCarts = rs("Cnt")
				SqlQueryClose(connSQL, rs)
				sql = s2 + "SoftCarts IS NULL"
				If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
					Return mi
				End If
				rs.Read()
				mi.SoftCarts += rs("Cnt")
				SqlQueryClose(connSQL, rs)
			Else
				mi.SoftCarts = 0
			End If
		Catch ex As Exception
		End Try

		Try
			If DateValue(Now) > dtPickupDate Then
				sql = s1 + "HardCarts IS NULL"
				If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
					Return mi
				End If
				rs.Read()
				mi.HardCarts = rs("Cnt")
				SqlQueryClose(connSQL, rs)
				sql = s2 + "HardCarts IS NULL"
				If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
					Return mi
				End If
				rs.Read()
				mi.HardCarts += rs("Cnt")
				SqlQueryClose(connSQL, rs)
			Else
				mi.HardCarts = 0
			End If
		Catch ex As Exception
		End Try

		Try
			If DateValue(Now) > dtPickupDate Then
				sql = s1 + "LocationUnloadedID IS NULL"
				If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
					Return mi
				End If
				rs.Read()
				mi.LocationsUnloaded = rs("Cnt")
				SqlQueryClose(connSQL, rs)
			Else
				mi.LocationsUnloaded = 0
			End If
		Catch ex As Exception
		End Try

		Return mi
	End Function

	Protected Sub ddlDriverLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriverLocations.SelectedIndexChanged
		LoadAssignments()
	End Sub

	Protected Sub dtPickupDate_DateChanged(sender As Object, e As EventArgs) Handles dtPickupDate.DateChanged
		LoadAssignments()
	End Sub

	Protected Sub grdMain_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grdMain.RowValidating
		If IsNothing(e.NewValues("LocationUnloadedID")) Or IsDBNull(e.NewValues("LocationUnloadedID")) Then
			e.RowError = "Please enter a Location Unloaded."
			Return
		End If

		If e.NewValues("CntBag") = 0 Then
			If Not IsDBNull(e.NewValues("BaggerID")) AndAlso e.NewValues("BaggerID") > 0 Then
				e.RowError = "No Bagger is allowed on a Section without bags."
				Return
			End If
			If Not IsDBNull(e.NewValues("CntPutOutsBagger")) AndAlso e.NewValues("CntPutOutsBagger") > 0 Then
				e.RowError = "No Bagger Put Outs are allowed on a Section without bags."
				Return
			End If
		End If

		If e.NewValues("NonTabletBagPickup") = "NT" _
			AndAlso e.OldValues("SoftCarts") <> e.NewValues("SoftCarts") _
			AndAlso e.OldValues("HardCarts") <> e.NewValues("HardCarts") _
			AndAlso e.OldValues("LocationUnloadedID") <> e.NewValues("LocationUnloadedID") Then

			e.RowError = "A Non-Tablet Bag entry must be edited in Non-Tablet Bag Pickups."
			Return
		End If

		' If the Inventory for this date or any future date has been approved, changes are not allowed.
		Dim approvedOld As Boolean = False
		If Not IsDBNull(e.OldValues("LocationUnloadedID")) Then
			approvedOld = IsInventoryApproved(dtPickupDate.Value, e.OldValues("LocationUnloadedID"), )
		End If
		Dim approvedNew As Boolean = IsInventoryApproved(dtPickupDate.Value, e.NewValues("LocationUnloadedID"), )
		Try
			If approvedOld Or approvedNew Then
				If e.OldValues("LocationUnloadedID") <> e.NewValues("LocationUnloadedID") And approvedOld Then
					e.RowError = "The Ending Inventory for this Location and Pickup Date has been approved so the Location Unloaded cannot be changed."
				End If
				If e.OldValues("LocationUnloadedID") <> e.NewValues("LocationUnloadedID") And approvedNew Then
					e.RowError = "The Ending Inventory for the Location you have entered and Pickup Date has been approved so the Location Unloaded cannot be changed."
				End If
				If e.OldValues("SoftCarts").ToString = "" OrElse (e.OldValues("SoftCarts") <> e.NewValues("SoftCarts")) Then
					e.RowError = "The Ending Inventory for this Location and Pickup Date has been approved so Soft Carts cannot be changed."
				End If
				If e.OldValues("HardCarts").ToString = "" OrElse (e.OldValues("HardCarts") <> e.NewValues("HardCarts")) Then
					e.RowError = "The Ending Inventory for this Location and Pickup Date has been approved so Hard Carts cannot be changed."
				End If
			End If
		Catch ex As Exception
			ex = ex
		End Try
	End Sub

	Protected Sub grdDriverAssignments_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs)
		Try
			Dim nt As String = CStr(grdMain.GetRowValuesByKeyValue(e.EditingKeyValue, "NonTabletBagPickup"))
			If nt = "NT" Then
				grdMain.JSProperties("cpServerMessage") = "A Non-Tablet Bag Pickup cannot be modified here."
				e.Cancel = True
			End If
		Catch
		End Try
	End Sub

	Protected Sub grdDriverAssignments_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		Try
			If e.Values("NonTabletBagPickup") = "NT" Then
				grdMain.JSProperties("cpServerMessage") = "A Non-Tablet Bag Pickup cannot be modified here."
				e.Cancel = True
			End If
		Catch
		End Try
	End Sub

	Protected Sub grdDriverAssignments_CustomButtonBallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles grdMain.CustomButtonCallback
		If e.ButtonID = "cbAddDriver" Then
			Dim sql As String = "INSERT INTO tblDriverAssignments (PickupScheduleSectionID, PickupScheduleID, PickupDate, " & _
					"RouteID, RouteCode, RouteDesc, SectionID, SectionCode, SectionDesc) " & _
				"VALUES (" & grdMain.GetRowValues(e.VisibleIndex, "PickupScheduleSectionID") & ", " & _
					grdMain.GetRowValues(e.VisibleIndex, "PickupScheduleID") & ", '" & _
					dtPickupDate.Value & "', " & _
					grdMain.GetRowValues(e.VisibleIndex, "RouteID") & ", '" & _
					grdMain.GetRowValues(e.VisibleIndex, "RouteCode") & "', '" & _
					grdMain.GetRowValues(e.VisibleIndex, "RouteDesc") & "', " & _
					grdMain.GetRowValues(e.VisibleIndex, "SectionID") & ", '" & _
					grdMain.GetRowValues(e.VisibleIndex, "SectionCode") & "', '" & _
					grdMain.GetRowValues(e.VisibleIndex, "SectionDesc") & "')"
			SqlNonQuery(sql, "DailyRouteReports, grdDriverAssignments_CustomButtonBallback, cbAddDriver")
			LoadAssignments()
		End If
		If e.ButtonID = "cbAddSpecial" Then
			Dim sql As String = "INSERT INTO tblDriverAssignments (PickupScheduleSectionID, PickupScheduleID, PickupDate, " & _
					"RouteID, RouteCode, RouteDesc, SectionID, SectionCode, SectionDesc) " & _
				"VALUES (0, " & grdMain.GetRowValues(e.VisibleIndex, "PickupScheduleID") & ", '" & _
					dtPickupDate.Value & "', " & _
					grdMain.GetRowValues(e.VisibleIndex, "RouteID") & ", '" & _
					grdMain.GetRowValues(e.VisibleIndex, "RouteCode") & "', '" & _
					grdMain.GetRowValues(e.VisibleIndex, "RouteDesc") & "', 0, '', '')"
			SqlNonQuery(sql, "DailyRouteReports, grdDriverAssignments_CustomButtonBallback, cbAddSpecial")
			LoadAssignments()
		End If
	End Sub

	Protected Sub grdDriverAssignments_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		Select Case e.ButtonType
			Case ColumnCommandButtonType.Delete
				e.Visible = DeleteButtonVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex)
		End Select
	End Sub

	Private Function DeleteButtonVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim softCarts As Decimal = IIf(IsDBNull(grid.GetRowValues(visibleIndex, "SoftCarts")), 0, grid.GetRowValues(visibleIndex, "SoftCarts"))
		Dim hardCarts As Decimal = IIf(IsDBNull(grid.GetRowValues(visibleIndex, "HardCarts")), 0, grid.GetRowValues(visibleIndex, "HardCarts"))
		Dim locationUnloadedID As Decimal = IIf(IsDBNull(grid.GetRowValues(visibleIndex, "LocationUnloadedID")), 0, grid.GetRowValues(visibleIndex, "LocationUnloadedID"))

		If locationUnloadedID = 2 Then
			locationUnloadedID = locationUnloadedID
		End If


		Dim approved = IsInventoryApproved(dtPickupDate.Value, locationUnloadedID, )
		If approved And (softCarts > 0 Or hardCarts > 0) Then
			Return False
		End If

		Dim val1 As Object = grid.GetRowValues(visibleIndex, "PickupsSectionExists")
		Dim val2 As Object = grid.GetRowValues(visibleIndex, "NonTabletBagPickup")
		Return val1 <> 1 And _
			val2 <> "NT"
	End Function

	Protected Sub grdDriverAssignments_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		If e.ButtonID = "cbAddDriver" And Not NTVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
		If e.ButtonID = "cbAddSpecial" And Not NTVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
	End Sub

	Private Function NTVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim val As Object = grid.GetRowValues(visibleIndex, "NonTabletBagPickup")
		Return val <> "NT"
	End Function

	Protected Sub btnRunDailyRouteReport_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim parms As String = "&PARMS=startDate~" & dtPickupDate.Value & _
			"|endDate~" & dtPickupDate.Value & "|driverLocation~" & ddlDriverLocations.SelectedItem.Text
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Daily Route Report" & parms)
	End Sub

	Protected Sub btnRunMissingPickupsFromTabletsReport_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, dtPickupDate.Value) & _
			"|endDate~" & dtPickupDate.Value & "|driverLocation~" & ddlDriverLocations.SelectedItem.Text
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Pickups from Tablets" & parms)
	End Sub

	Protected Sub btnMissingDriversForPickups_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, dtPickupDate.Value) & _
			"|endDate~" & dtPickupDate.Value & "|driverLocation~" & ddlDriverLocations.SelectedItem.Value
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Drivers for Pickups" & parms)
	End Sub

	Protected Sub btnRunMissingDailyRouteDataReport_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, dtPickupDate.Value) & _
			"|endDate~" & dtPickupDate.Value & "|driverLocation~" & ddlDriverLocations.SelectedItem.Text
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Daily Route Data" & parms)
	End Sub
End Class
