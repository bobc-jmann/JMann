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


Partial Class DriverAssignments
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

				sql = "SELECT RegionID, RegionDesc FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " " & _
					"UNION " & _
					"SELECT 0 AS RegionID, ' Select Driver Location' AS RegionDesc " & _
					"ORDER BY RegionDesc"
				dsDriverLocations.SelectCommand = sql
				dsDriverLocations.DataBind()

				If Session("UserRegionDefault") <> 0 Then
					ddlDriverLocations.Value = Session("UserRegionDefault")
				Else
					ddlDriverLocations.Value = 0
				End If

			Case "HEAD"
				'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
		End Select

		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		' Insert rows for Route-Sections into Driver Assignments for current date
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spDriverAssignments_InsertRowsForDate"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupDate", System.Data.ParameterDirection.Input, System.Data.DbType.Date, dtPickupDate.Value))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@regionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, ddlDriverLocations.Value))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "DriverAssignments, Page_Load")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try

		dsDriver.SelectCommand = "SELECT DriverID, DriverName FROM [tblDrivers] " & _
			"WHERE (DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL) " & _
				"AND Active = 1 " & _
				"AND Driver = 1 " & _
			"ORDER BY DriverName"
		Dim driverID_column = TryCast(grdDriverAssignments.DataColumns("DriverDisplayID"), GridViewDataComboBoxColumn)
		driverID_column.PropertiesComboBox.RequireDataBinding()
		dsDriver.DataBind()
		dsDriverEdit.SelectCommand = "SELECT DriverID, DriverName FROM [tblDrivers] " & _
			"WHERE (DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL) " & _
				"AND Active = 1 " & _
				"AND Driver = 1 " & _
			"ORDER BY DriverName"
		Dim driverEditID_column = TryCast(grdDriverAssignments.DataColumns("DriverID"), GridViewDataComboBoxColumn)
		driverEditID_column.PropertiesComboBox.RequireDataBinding()
		dsDriverEdit.DataBind()
		cmbDrivers.DataBind()
		dsTablet.SelectCommand = "SELECT TabletID, TabletName FROM [tblTablets] " & _
			"WHERE DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL " & _
				 "AND (Active = 1 OR TabletID = -1) " & _
		   "ORDER BY TabletName"
		Dim tabletID_column = TryCast(grdDriverAssignments.DataColumns("TabletID"), GridViewDataComboBoxColumn)
		tabletID_column.PropertiesComboBox.RequireDataBinding()
		dsTablet.DataBind()
		dsPhone.SelectCommand = "SELECT PhoneID, PhoneNumber FROM [tblPhones] " & _
			"WHERE DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL " & _
				"AND Active = 1 " & _
			"ORDER BY PhoneNumber"
		Dim phoneID_column = TryCast(grdDriverAssignments.DataColumns("PhoneID"), GridViewDataComboBoxColumn)
		phoneID_column.PropertiesComboBox.RequireDataBinding()
		dsPhone.DataBind()
        dsTruck.SelectCommand = "SELECT TruckID, TruckNumber FROM [tblTrucks] " &
            "WHERE DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL " &
                "AND Active = 1 " &
                "AND Truck = 1 " &
            "ORDER BY TruckNumber"
        Dim truckID_column = TryCast(grdDriverAssignments.DataColumns("TruckID"), GridViewDataComboBoxColumn)
        truckID_column.PropertiesComboBox.RequireDataBinding()
        dsTruck.DataBind()
        dsDevices.SelectCommand = "SELECT DeviceID, DeviceDescription FROM GeotabDevices " &
            "WHERE DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL " &
                "AND Active = 1 " &
            "ORDER BY DeviceDescription"
        Dim deviceID_column = TryCast(grdDriverAssignments.DataColumns("DeviceID"), GridViewDataComboBoxColumn)
        deviceID_column.PropertiesComboBox.RequireDataBinding()
        dsDevices.DataBind()

        LoadAssignments()
	End Sub

	Private Function SelectAssignments(ByVal whereClause As String) As String
		Dim sql As String = "SELECT CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END AS PreSort, LEN(DA.SectionCode), " & _
				"DA.PickupDate, DA.RouteID, DA.RouteCode, DA.RouteDesc, DA.SectionID, DA.SectionCode, DA.SectionDesc, " & _
				"DA.RouteCode + '-' + DA.SectionCode AS [Route-Section], DA.LocationUnloadedID " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleId = DA.PickupScheduleID " & _
			whereClause & "AND DA.SectionID > 0 AND NTBP.PickupScheduleID IS NULL " & _
			"UNION " & _
			"SELECT CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END AS PreSort, LEN(DA.SectionCode), " & _
				"DA.PickupDate, DA.RouteID, DA.RouteCode, DA.RouteDesc, DA.SectionID, DA.SectionCode, DA.SectionDesc, " & _
				"RGP.RegionCode + '-Specials' AS [Route-Section], DA.LocationUnloadedID " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"INNER JOIN tblPickupCycleDriverLocations PCDLP ON PCDLP.PickupCycleID = PC.PickupCycleID " & _
			"INNER JOIN tlkRegions RGP ON RGP.RegionID = PCDLP.RegionID " & _
			"LEFT OUTER JOIN tblDrivers AS D ON D.DriverID = " & IIf(IsNothing(cmbDrivers.Value), 0, cmbDrivers.Value) & " " & _
			"LEFT OUTER JOIN tlkRegions RG ON RG.RegionID = DA.LocationID " & _
			"LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleId = DA.PickupScheduleID " & _
			whereClause & "AND DA.SectionID = 0 AND NTBP.PickupScheduleID IS NULL AND PCDLP.PrimaryRegion = 1 " & _
			"ORDER BY CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END, DA.RouteCode, LEN(DA.SectionCode), DA.SectionCode"

		dsSections.SelectCommand = sql
		ddlSections.DataBind()

        sql = "SELECT CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END AS PreSort, DA.DriverAssignmentID, DA.PickupScheduleSectionID, DA.PickupScheduleID, " &
                "DA.RouteID, DA.RouteCode, DA.RouteDesc, DA.SectionID, DA.SectionCode, DA.SectionDesc, " &
                "DA.RouteCode + '-' + DA.SectionCode AS [Route-Section], DA.BaggerID, DA.DriverID, DA.DriverID AS DriverDisplayID, DA.TabletID, DA.PhoneID, DA.TruckID, DA.DeviceID, " &
                "PSS.CntTotalAddresses, LEN(DA.SectionCode), DA.LocationUnloadedID, " &
                "CASE WHEN NTBP.PickupScheduleID IS NULL THEN '' ELSE 'NT' END AS NonTabletBagPickup, " &
                "CASE WHEN PSEC.PickupsSectionID IS NULL THEN 0 ELSE 1 END AS PickupsSectionExists " &
            "FROM tblDriverAssignments DA " &
            "INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " &
            "LEFT OUTER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleID = DA.PickupScheduleID AND PSS.SectionID = DA.SectionID " &
            "INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " &
            "INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " &
            "LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleId = DA.PickupScheduleID " &
            "LEFT OUTER JOIN tblPickupsSections AS PSEC ON PSEC.PickupScheduleSectionID = DA.PickupScheduleSectionID " &
                "AND PSEC.DriverID = DA.DriverID " &
            whereClause & "AND DA.SectionID > 0 " &
            "UNION " &
            "SELECT CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END AS PreSort, DA.DriverAssignmentID, DA.PickupScheduleSectionID, DA.PickupScheduleID, " &
                "DA.RouteID, DA.RouteCode, DA.RouteDesc, DA.SectionID, DA.SectionCode, DA.SectionDesc, " &
                "CASE WHEN RG.RegionCode IS NOT NULL THEN RG.RegionCode ELSE RGP.RegionCode END + '-Specials' AS [Route-Section], " &
                "NULL AS BaggerID, DA.DriverID, DA.DriverID AS DriverDisplayID, DA.TabletID, DA.PhoneID, DA.TruckID, DA.DeviceID, " &
                "PSS.CntTotalAddresses, LEN(DA.SectionCode), DA.LocationUnloadedID, " &
                "CASE WHEN NTBP.PickupScheduleID IS NULL THEN '' ELSE 'NT' END AS NonTabletBagPickup, " &
                "CASE WHEN PSEC.PickupsSectionID IS NULL THEN 0 ELSE 1 END AS PickupsSectionExists " &
            "FROM tblDriverAssignments DA " &
            "INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " &
            "LEFT OUTER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleID = DA.PickupScheduleID AND PSS.SectionID = DA.SectionID " &
            "INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " &
            "INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " &
            "INNER JOIN tblPickupCycleDriverLocations PCDLP ON PCDLP.PickupCycleID = PC.PickupCycleID " &
            "INNER JOIN tlkRegions RGP ON RGP.RegionID = PCDLP.RegionID " &
            "LEFT OUTER JOIN tlkRegions RG ON RG.RegionID = DA.LocationID " &
            "LEFT OUTER JOIN tblNonTabletBagPickups AS NTBP ON NTBP.PickupScheduleId = DA.PickupScheduleID " &
            "LEFT OUTER JOIN tblPickupsSections AS PSEC ON PSEC.PickupScheduleSectionID = DA.PickupScheduleSectionID " &
                "AND PSEC.DriverID = DA.DriverID " &
            whereClause & "AND DA.SectionID = 0 AND PCDLP.PrimaryRegion = 1 " &
            "ORDER BY CASE DA.SectionID WHEN 0 THEN '1' ELSE '2' END, DA.RouteCode, LEN(DA.SectionCode), DA.SectionCode"

        Return (sql)
	End Function

	Private Sub LoadAssignments()
		If ddlDriverLocations.Value = "0" OrElse IsNothing(ddlDriverLocations.Value) Then
			Return
		End If

		Dim whereClause As String = "WHERE ((DA.DriverID IS NULL AND PCDL.RegionID = " & ddlDriverLocations.Value & ") " & _
			"OR DA.LocationID = " & ddlDriverLocations.Value & ") " & _
			"AND PS.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' "
		hfDriverAssignmentsSelectCommand.Value = SelectAssignments(whereClause)

		dsDriverAssignments.SelectCommand = hfDriverAssignmentsSelectCommand.Value
		grdDriverAssignments.DataBind()

		'Get number of unassigned Specials
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim driverID As Integer = cmbDrivers.Value
		Dim mSpecials As Integer = 0
		Dim sql As String = "SELECT COUNT(*) AS Cnt " & _
			"FROM tblSpecials AS SP " & _
			"WHERE SP.DriverLocationID = " & ddlDriverLocations.Value & " " & _
				"AND SP.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
				"AND SP.DriverID = 0"

		If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
			Return
		End If
		rs.Read()
		mSpecials = CInt(rs("Cnt"))
		SqlQueryClose(connSQL, rs)

		'Fill labels if missing information
		Dim mDrivers As Integer = 0
		Dim mTablets As Integer = 0

		For i As Integer = 0 To grdDriverAssignments.VisibleRowCount - 1
			If grdDriverAssignments.GetRowValues(i, "DriverID").ToString = "" And grdDriverAssignments.GetRowValues(i, "SectionID") > 0 Then
				mDrivers += 1
			End If
			If grdDriverAssignments.GetRowValues(i, "TabletID").ToString = "" And grdDriverAssignments.GetRowValues(i, "SectionID") > 0 Then
				mTablets += 1
			End If
		Next

		If mDrivers > 0 Then
			lblMissingDrivers.Text = mDrivers.ToString + " record(s) do not have Drivers assigned."
		Else
			lblMissingDrivers.Text = ""
		End If
		If mTablets > 0 Then
			lblMissingTablets.Text = mTablets.ToString + " record(s) do not have Tablets assigned."
		Else
			lblMissingTablets.Text = ""
		End If
		If mSpecials > 0 Then
			lblMissingSpecials.Text = mSpecials.ToString + " specials(s) do not have Drivers assigned."
		Else
			lblMissingSpecials.Text = ""
		End If


	End Sub

	'Public Class MissingInfo
	'	Public Drivers As Integer = 0
	'	Public Tablets As Integer = 0
	'	Public Specials As Integer = 0
	'End Class

	'<WebMethod()> _
	'Public Shared Function GetMissingInfo(ByVal dtPickupDate As Object, ByVal driverLocationID As Object) As MissingInfo
	'	Dim rs As SqlDataReader = Nothing
	'	Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

	'	Dim mi As MissingInfo = New MissingInfo
	'	Dim sql As String
	'	Dim s1, s2 As String

	'	s1 = "SELECT COUNT(*) AS Cnt " & _
	'		"FROM tblDriverAssignments DA " & _
	'		"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
	'		"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
	'		"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
	'		"WHERE PCDL.RegionID = " & driverLocationID & " " & _
	'			"AND DA.PickupDate = '" & Format(dtPickupDate, "MM/dd/yyyy") & "' " & _
	'			"AND "

	'	s2 = "SELECT COUNT(*) AS Cnt " & _
	'		"FROM tblDriverAssignments DA " & _
	'		"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
	'		"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
	'		"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
	'		"WHERE PCDL.RegionID = " & driverLocationID & " " & _
	'			"AND DA.PickupDate = '" & Format(dtPickupDate, "MM/dd/yyyy") & "' " & _
	'			"AND DA.SectionID = 0 " & _
	'			"AND "

	'	Try
	'		sql = s1 + "DriverID IS NULL"
	'		If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
	'			Return mi
	'		End If
	'		rs.Read()
	'		mi.Drivers = rs("Cnt")
	'		SqlQueryClose(connSQL, rs)
	'	Catch ex As Exception
	'	End Try

	'	Try
	'		sql = s1 + "TabletID IS NULL"
	'		If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
	'			Return mi
	'		End If
	'		rs.Read()
	'		mi.Tablets = rs("Cnt")
	'		SqlQueryClose(connSQL, rs)
	'	Catch ex As Exception
	'	End Try

	'	'Get number of unassigned Specials

	'	Dim mSpecials As Integer = 0
	'	sql = "SELECT COUNT(*) AS Cnt " & _
	'		"FROM tblSpecials AS SP " & _
	'		"WHERE SP.DriverLocationID = " & driverLocationID & " " & _
	'			"AND SP.PickupDate = '" & Format(dtPickupDate, "MM/dd/yyyy") & "' " & _
	'			"AND SP.DriverID = 0"

	'	If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
	'		Return mi
	'	End If
	'	rs.Read()
	'	mSpecials = rs("Cnt")
	'	SqlQueryClose(connSQL, rs)

	'	Return mi
	'End Function

	<WebMethod()> _
	Public Shared Function GetDriverInfo(ByVal driverID As Integer) As String()
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

        Dim ids(3) As String
        Dim sql As String = "SELECT T.TabletName, R.PhoneNumber, TR.TruckNumber, GD.DeviceDescription " &
            "FROM tblDrivers D " &
            "LEFT OUTER JOIN tblTablets T ON T.TabletID = D.TabletID " &
            "LEFT OUTER JOIN tblPhones R ON R.PhoneID = D.PhoneID " &
            "LEFT OUTER JOIN tblTrucks TR ON TR.TruckID = D.TruckID " &
            "LEFT OUTER JOIN GeotabDevices GD ON GD.DeviceID = TR.DeviceID " &
            "WHERE DriverID = " & driverID
        If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
			Return ids
		End If
		If rs.Read() Then
			Try
				ids(0) = rs("TabletName")
			Catch ex As Exception
				ids(0) = ""
			End Try

			Try
				ids(1) = rs("PhoneNumber")
			Catch ex As Exception
				ids(1) = ""
			End Try

            Try
                ids(2) = rs("TruckNumber")
            Catch ex As Exception
                ids(2) = ""
            End Try

            Try
                ids(3) = rs("DeviceDescription")
            Catch ex As Exception
                ids(3) = ""
            End Try
        End If
        SqlQueryClose(connSQL, rs)


		Try
		Catch ex As Exception
		End Try

		Return ids
	End Function

	Protected Sub DriversCallbackPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
		'Dim rs As SqlDataReader = Nothing
		'Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		'' Insert rows for Route-Sections into Driver Assignments for current date
		'Dim conn As SqlConnection = New SqlConnection(vConnStr)

		'Dim myCmd As SqlCommand = New SqlCommand()
		'myCmd.Connection = conn
		'myCmd.CommandText = "spDriverAssignments_InsertRowsForDate"
		'myCmd.CommandType = System.Data.CommandType.StoredProcedure

		'myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupDate", System.Data.ParameterDirection.Input, System.Data.DbType.Date, dtPickupDate.Value))
		'myCmd.Parameters.Add(DataUtil.CreateParameter("@regionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, ddlDriverLocations.Value))
		'Dim errorID As Integer = 0
		'myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		'Try
		'	conn.Open()
		'	myCmd.ExecuteNonQuery()
		'	errorID = myCmd.Parameters("@RETURN_VALUE").Value
		'	If errorID > 0 Then
		'		vbHandleProgramError(errorID, "DriverAssignments, Page_Load")
		'	End If
		'Catch ex As Exception
		'	LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		'Finally
		'	conn.Close()
		'End Try

		'dsDriver.SelectCommand = "SELECT DriverID, DriverName FROM [tblDrivers] " & _
		'	"WHERE (DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL) " & _
		'		"AND Active = 1 " & _
		'		"AND Driver = 1 " & _
		'	"ORDER BY DriverName"
		'Dim driverID_column = TryCast(grdDriverAssignments.DataColumns("DriverDisplayID"), GridViewDataComboBoxColumn)
		'driverID_column.PropertiesComboBox.RequireDataBinding()
		'dsDriver.DataBind()
		'dsDriverEdit.SelectCommand = "SELECT DriverID, DriverName FROM [tblDrivers] " & _
		'	"WHERE (DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL) " & _
		'		"AND Active = 1 " & _
		'		"AND Driver = 1 " & _
		'	"ORDER BY DriverName"
		'Dim driverEditID_column = TryCast(grdDriverAssignments.DataColumns("DriverID"), GridViewDataComboBoxColumn)
		'driverEditID_column.PropertiesComboBox.RequireDataBinding()
		'dsDriverEdit.DataBind()
		'cmbDrivers.DataBind()
		'dsTablet.SelectCommand = "SELECT TabletID, TabletName FROM [tblTablets] " & _
		'	"WHERE DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL " & _
		'		 "AND (Active = 1 OR TabletID = -1) " & _
		'   "ORDER BY TabletName"
		'Dim tabletID_column = TryCast(grdDriverAssignments.DataColumns("TabletID"), GridViewDataComboBoxColumn)
		'tabletID_column.PropertiesComboBox.RequireDataBinding()
		'dsTablet.DataBind()
		'dsPhone.SelectCommand = "SELECT PhoneID, PhoneNumber FROM [tblPhones] " & _
		'	"WHERE DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL " & _
		'		"AND Active = 1 " & _
		'	"ORDER BY PhoneNumber"
		'Dim phoneID_column = TryCast(grdDriverAssignments.DataColumns("PhoneID"), GridViewDataComboBoxColumn)
		'phoneID_column.PropertiesComboBox.RequireDataBinding()
		'dsPhone.DataBind()
		'dsTruck.SelectCommand = "SELECT TruckID, TruckNumber FROM [tblTrucks] " & _
		'	"WHERE DriverLocationID = '" & ddlDriverLocations.Value & "' OR DriverLocationID IS NULL " & _
		'		"AND Active = 1 " & _
		'		"AND Truck = 1 " & _
		'	"ORDER BY TruckNumber"
		'Dim truckID_column = TryCast(grdDriverAssignments.DataColumns("TruckID"), GridViewDataComboBoxColumn)
		'truckID_column.PropertiesComboBox.RequireDataBinding()
		'dsTruck.DataBind()

		'LoadAssignments()
	End Sub

	Protected Sub SectionsCallbackPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
		LoadAssignments()
	End Sub

	Protected Sub DriverAssignmentsCallbackPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim routeSectionsText As String = ddlSections.Value
		Dim driverID As Integer = cmbDrivers.Value
		Dim pickupDate As Date = dtPickupDate.Value

		If IsNothing(routeSectionsText) Then
			Return
		End If

		Dim routeSections() As String = Split(routeSectionsText, ";")

		' Get Driver Info
		Dim tabletID As Integer = Nothing
		Dim PhoneID As Integer = Nothing
        Dim truckID As Integer = Nothing
        Dim deviceID As Integer = Nothing

        Dim sql = "SELECT D.TabletID, D.PhoneID, D.TruckID, GD.DeviceID " &
            "FROM tblDrivers As D " &
            "LEFT OUTER JOIN tblTrucks AS T ON T.TruckID = D.TruckID " &
            "LEFT OUTER JOIN GeotabDevices GD ON GD.DeviceID = T.DeviceID " &
            "WHERE DriverID = " & driverID

        If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
            Return
        End If
        If Not rs.Read() Then
			Return
		End If

		Try
			tabletID = rs("TabletID")
		Catch ex As Exception
			tabletID = -1
		End Try

		Try
			PhoneID = rs("PhoneID")
		Catch ex As Exception
			PhoneID = -1
		End Try

        Try
            truckID = rs("TruckID")
        Catch ex As Exception
            truckID = -1
        End Try

        Try
            deviceID = rs("DeviceID")
        Catch ex As Exception
            deviceID = -1
        End Try
        SqlQueryClose(connSQL, rs)

        For Each routeSection In routeSections
			Dim pos As Integer = 0
			Dim routeCode As String = ""
			Dim sectionCode As String = ""

			sql = "UPDATE tblDriverAssignments " & _
				"SET [LocationID] = (SELECT DriverLocationID FROM [tblDrivers] WHERE DriverID = " & driverID & "), " & _
					"DriverID = " & driverID

			If tabletID <> -1 Then
				sql += ", TabletID = " & tabletID
			Else
				sql += ", TabletID = NULL "
			End If
			If PhoneID <> -1 Then
				sql += ", PhoneID = " & PhoneID
			Else
				sql += ", PhoneID = NULL "
			End If
            If truckID <> -1 Then
                sql += ", TruckID = " & truckID
            Else
                sql += ", TruckID = NULL "
            End If
            If deviceID <> -1 Then
                sql += ", DeviceID = " & deviceID
            Else
                sql += ", DeviceID = NULL "
            End If

            sql += ", LocationUnloadedID = CASE " & _
					"WHEN (LocationUnloadedID IS NULL " & _
						"OR (SELECT RegionID FROM tblLocations WHERE LocationID = LocationUnloadedID) <> " & _
							"(SELECT DriverLocationID FROM [tblDrivers] WHERE DriverID = " & driverID & ")) " & _
					"THEN (SELECT TOP (1) LocationID FROM tblLocations " & _
						"WHERE RegionID = (SELECT DriverLocationID FROM [tblDrivers] " & _
							"WHERE DriverID = " & driverID & ") AND DefaultLocation = 1) " & _
					"ELSE LocationUnloadedID END "

			sql += " WHERE DriverAssignmentID IN " & _
				"(SELECT DriverAssignmentID FROM tblDriverAssignments DA " & _
					"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID "

			If Not routeSection.Contains("Specials") Then
				pos = InStrRev(routeSection, "-")
				routeCode = (Mid(routeSection, 1, pos - 1))
				sectionCode = Mid(routeSection, pos + 1)

				sql += "WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
							"AND DA.RouteCode = '" & routeCode & "' " & _
							"AND DA.SectionCode = '" & sectionCode & "')"
			Else
				sql += "WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
								 "AND DA.SectionID = 0 " & _
								 "AND DA.DriverID = " & driverID & ")"
			End If
			SqlNonQuery(sql)

		Next

		' Is there a Specials row for this Driver for current date?
		Dim specialsRowExists As Boolean = False
		sql = "SELECT * " & _
			"FROM tblDriverAssignments DA " & _
			"INNER JOIN tlkRegions AS RG ON RG.RegionCode = DA.SectionDesc " & _
			"INNER JOIN tblDrivers DL ON DL.DriverLocationID = DA.LocationID " & _
			"WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
				"AND DA.SectionID = 0 " & _
				"AND DA.DriverID = " & driverID

		Try
			If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
				Return
			End If
			If rs.Read() Then
				specialsRowExists = True
			End If
			SqlQueryClose(connSQL, rs)
		Catch ex As Exception

		End Try

		Dim blankDriverAssignmentID As Integer = 0
		If Not specialsRowExists Then
			' Need to create a specials row for this driver. Is there a blank one we can use?
			sql = "SELECT DriverAssignmentID " & _
				"FROM tblDriverAssignments DA " & _
				"INNER JOIN tlkRegions AS RG ON RG.RegionCode = DA.SectionDesc " & _
				"WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
					"AND DA.SectionID = 0 " & _
					"AND DA.DriverID IS NULL " & _
					"AND DA.LocationID = (SELECT DriverLocationID FROM tblDrivers WHERE DriverID = " & driverID & ") "
			Try
				If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
					Return
				End If
				If rs.Read() Then
					blankDriverAssignmentID = rs("DriverAssignmentID")
				End If
				SqlQueryClose(connSQL, rs)
			Catch ex As Exception

			End Try
		End If

		If blankDriverAssignmentID = 0 Then
            ' Insert new records
            sql = "INSERT INTO tblDriverAssignments (PickupScheduleSectionID, PickupScheduleID, PickupDate, " &
                    "RouteID, RouteCode, RouteDesc, SectionID, SectionCode, SectionDesc, " &
                    "LocationID, DriverID, TabletID, PhoneID, TruckID, DeviceID, LocationUnloadedID) " &
                "SELECT TOP 1 DA.PickupScheduleSectionID, DA.PickupScheduleID, DA.PickupDate, DA.RouteID, DA.RouteCode, DA.RouteDesc, 0, ' ', RG.RegionCode, " &
                    "RG.RegionID, " & driverID & ", " & tabletID & ", " & PhoneID & ", " & truckID & ", " & deviceID & ", " &
                    "(SELECT TOP (1) LocationID FROM tblLocations " &
                        "WHERE RegionID = (SELECT DriverLocationID FROM [tblDrivers] " &
                            "WHERE DriverID = " & driverID & ") AND DefaultLocation = 1) " &
                "FROM tblDriverAssignments DA " &
                "INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " &
                "INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " &
                "INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " &
                "INNER JOIN tlkRegions RG ON PCDL.RegionID = RG.RegionID " &
                "INNER JOIN tblDrivers D ON D.DriverLocationID = PCDL.RegionID " &
                "WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " &
                     "AND DA.SectionID > 0 " &
                     "AND D.DriverID = " & driverID
            SqlNonQuery(sql, "DriverAssignments, Page_Load")
		Else
            ' Update the blank record
            sql = "UPDATE tblDriverAssignments " &
                "SET DriverID = " & driverID & ", " &
                    "TabletID = " & tabletID & ", " &
                    "PhoneID = " & PhoneID & ", " &
                    "TruckID = " & truckID & ", " &
                    "DeviceID = " & deviceID & ", " &
                    "LocationUnloadedID = CASE " &
                        "WHEN (LocationUnloadedID IS NULL " &
                            "OR (SELECT RegionID FROM tblLocations WHERE LocationID = LocationUnloadedID) <> " &
                                "(SELECT DriverLocationID FROM [tblDrivers] WHERE DriverID = " & driverID & ")) " &
                        "THEN (SELECT TOP (1) LocationID FROM tblLocations " &
                            "WHERE RegionID = (SELECT DriverLocationID FROM [tblDrivers] " &
                                "WHERE DriverID = " & driverID & ") AND DefaultLocation = 1) " &
                        "ELSE LocationUnloadedID END " &
                "WHERE DriverAssignmentID = " & blankDriverAssignmentID
            SqlNonQuery(sql)
		End If

		LoadAssignments()
	End Sub

	Protected Sub SpecialsCallbackPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim specialsText As String = ddlSpecials.Value
		Dim driverID As Integer = cmbDrivers.Value
		Dim pickupDate As Date = dtPickupDate.Value

		If driverID = 0 Then
			Return
		End If

		Dim specials() As String = Split(specialsText, ";")
		Dim sql As String


		'This is resetting the selected specials to the current driver
		If specialsText <> "" Then
			'Reset selections first.
			sql = "UPDATE tblSpecials " & _
				"SET DriverID = 0 " & _
				"WHERE PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
					"AND DriverID = " & driverID
			SqlNonQuery(sql)


			For Each special In specials
				Dim specialFields() As String = Split(special, ":")
				Dim city As String = Trim(specialFields(0))
				Dim address As String = Trim(specialFields(1))

				sql = "UPDATE tblSpecials " & _
					"SET DriverID = " & driverID & _
					"WHERE City = '" & city & "' " & _
						"AND [Address] = '" & address & "' " & _
						"AND PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "'"
				SqlNonQuery(sql)
			Next		'Populate Specials ListBox

		End If
		'Dim driverID As Integer = cmbDrivers.Value
		sql = "SELECT PickupID, DriverID, City + ' : ' + [Address] + ' : ' + " & _
				"CAST(COALESCE(PromisedBags, 0) + COALESCE(PromisedBoxes, 0) AS varchar) AS SpecialText " & _
			"FROM tblSpecials AS SP " & _
			"WHERE SP.DriverLocationID = " & ddlDriverLocations.Value & " " & _
				"AND SP.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
				"AND SP.DriverID = " & driverID & " " & _
			"UNION " & _
			"SELECT PickupID, DriverID, City + ' : ' + [Address] + ' : ' + " & _
				"CAST(COALESCE(PromisedBags, 0) + COALESCE(PromisedBoxes, 0) AS varchar) AS SpecialText " & _
			"FROM tblSpecials AS SP " & _
			"WHERE SP.DriverLocationID = " & ddlDriverLocations.Value & " " & _
				"AND SP.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
				"AND SP.DriverID = 0 " & _
			"ORDER BY SpecialText"

		dsSpecials.SelectCommand = sql
		ddlSpecials.DataBind()
	End Sub

	Protected Sub MissingCallbackPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
		'Get number of unassigned Specials
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim driverID As Integer = cmbDrivers.Value
		Dim mSpecials As Integer = 0
		Dim sql As String = "SELECT COUNT(*) AS Cnt " & _
			"FROM tblSpecials AS SP " & _
			"WHERE SP.DriverLocationID = " & ddlDriverLocations.Value & " " & _
				"AND SP.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
				"AND SP.DriverID = 0"

		If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
			Return
		End If
		rs.Read()
		mSpecials = CInt(rs("Cnt"))
		SqlQueryClose(connSQL, rs)

		'Fill labels if missing information
		Dim mDrivers As Integer = 0
		Dim mTablets As Integer = 0

		For i As Integer = 0 To grdDriverAssignments.VisibleRowCount - 1
			If grdDriverAssignments.GetRowValues(i, "DriverID").ToString = "" And grdDriverAssignments.GetRowValues(i, "SectionID") > 0 Then
				mDrivers += 1
			End If
			If grdDriverAssignments.GetRowValues(i, "TabletID").ToString = "" And grdDriverAssignments.GetRowValues(i, "SectionID") > 0 Then
				mTablets += 1
			End If
		Next

		If mDrivers > 0 Then
			lblMissingDrivers.Text = mDrivers.ToString + " record(s) do not have Drivers assigned."
		Else
			lblMissingDrivers.Text = ""
		End If
		If mTablets > 0 Then
			lblMissingTablets.Text = mTablets.ToString + " record(s) do not have Tablets assigned."
		Else
			lblMissingTablets.Text = ""
		End If
		If mSpecials > 0 Then
			lblMissingSpecials.Text = mSpecials.ToString + " specials(s) do not have Drivers assigned."
		Else
			lblMissingSpecials.Text = ""
		End If
	End Sub

	Protected Sub specialsListBox_DataBound(sender As Object, e As EventArgs)
		'Mark Specials already assigned to driver
		Dim listBox As ASPxListBox = DirectCast(sender, ASPxListBox)
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim ddlSpecialsText As String = ""

		Dim driverID As Integer = cmbDrivers.Value
		Dim sql As String = "SELECT PickupID, DriverID, City + ' : ' + [Address] + ' : ' + " & _
				"CAST(COALESCE(PromisedBags, 0) + COALESCE(PromisedBoxes, 0) AS varchar) AS SpecialText " & _
			"FROM tblSpecials AS SP " & _
			"WHERE SP.DriverLocationID = " & ddlDriverLocations.Value & " " & _
				"AND SP.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
				"AND SP.DriverID = " & driverID & " " & _
			"ORDER BY SP.City, SP.[Address]"

		If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
			Return
		End If
		While rs.Read()
			For Each item As ListEditItem In listBox.Items
				If item.Text = rs("SpecialText") Then
					item.Selected = True
					If ddlSpecialsText <> "" Then
						ddlSpecialsText &= ";"
					End If
					ddlSpecialsText &= item.Text
					Exit For
				End If
			Next
		End While

		SqlQueryClose(connSQL, rs)

		Try
		Catch ex As Exception
		End Try

		ddlSpecials.Text = ddlSpecialsText
	End Sub

	'<WebMethod()> _
	'Public Shared Function SaveRouteSectionSelections(ByVal pickupDate As Date, ByVal driverID As Integer, ByVal routeSectionsText As String) As Integer
	'	Dim rs As SqlDataReader = Nothing
	'	Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

	'	Dim routeSections() As String = Split(routeSectionsText, ";")

	'	' Get Driver Info
	'	Dim tabletID As Integer = Nothing
	'	Dim PhoneID As Integer = Nothing
	'	Dim truckID As Integer = Nothing

	'	Dim sql = "SELECT TabletID, PhoneID, TruckID FROM tblDrivers WHERE DriverID = " & driverID
	'	If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
	'		Return routeSectionsText
	'	End If
	'	If Not rs.Read() Then
	'		Return 1
	'	End If

	'	Try
	'		tabletID = rs("TabletID")
	'	Catch ex As Exception
	'		tabletID = -1
	'	End Try

	'	Try
	'		PhoneID = rs("PhoneID")
	'	Catch ex As Exception
	'		PhoneID = -1
	'	End Try

	'	Try
	'		truckID = rs("TruckID")
	'	Catch ex As Exception
	'		truckID = -1
	'	End Try
	'	SqlQueryClose(connSQL, rs)

	'	For Each routeSection In routeSections
	'		Dim pos As Integer = 0
	'		Dim routeCode As String = ""
	'		Dim sectionCode As String = ""

	'		sql = "UPDATE tblDriverAssignments " & _
	'			"SET [LocationID] = (SELECT DriverLocationID FROM [tblDrivers] WHERE DriverID = " & driverID & "), " & _
	'				"DriverID = " & driverID

	'		If tabletID <> -1 Then
	'			sql += ", TabletID = " & tabletID
	'		Else
	'			sql += ", TabletID = NULL "
	'		End If
	'		If PhoneID <> -1 Then
	'			sql += ", PhoneID = " & PhoneID
	'		Else
	'			sql += ", PhoneID = NULL "
	'		End If
	'		If truckID <> -1 Then
	'			sql += ", TruckID = " & truckID
	'		Else
	'			sql += ", TruckID = NULL "
	'		End If

	'		sql += ", LocationUnloadedID = CASE " & _
	'				"WHEN (LocationUnloadedID IS NULL " & _
	'					"OR (SELECT RegionID FROM tblLocations WHERE LocationID = LocationUnloadedID) <> " & _
	'						"(SELECT DriverLocationID FROM [tblDrivers] WHERE DriverID = " & driverID & ")) " & _
	'				"THEN (SELECT TOP (1) LocationID FROM tblLocations " & _
	'					"WHERE RegionID = (SELECT DriverLocationID FROM [tblDrivers] " & _
	'						"WHERE DriverID = " & driverID & ") AND DefaultLocation = 1) " & _
	'				"ELSE LocationUnloadedID END "

	'		sql += " WHERE DriverAssignmentID IN " & _
	'			"(SELECT DriverAssignmentID FROM tblDriverAssignments DA " & _
	'				"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID "

	'		If Not routeSection.Contains("Specials") Then
	'			pos = InStrRev(routeSection, "-")
	'			routeCode = (Mid(routeSection, 1, pos - 1))
	'			sectionCode = Mid(routeSection, pos + 1)

	'			sql += "WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
	'						"AND DA.RouteCode = '" & routeCode & "' " & _
	'						"AND DA.SectionCode = '" & sectionCode & "')"
	'		Else
	'			sql += "WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
	'							 "AND DA.SectionID = 0 " & _
	'							 "AND DA.DriverID = " & driverID & ")"
	'		End If
	'		SqlNonQuery(sql)

	'	Next

	'	' Is there a Specials row for this Driver for current date?
	'	Dim specialsRowExists As Boolean = False
	'	sql = "SELECT * " & _
	'		"FROM tblDriverAssignments DA " & _
	'		"INNER JOIN tlkRegions AS RG ON RG.RegionCode = DA.SectionDesc " & _
	'		"INNER JOIN tblDrivers DL ON DL.DriverLocationID = DA.LocationID " & _
	'		"WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
	'			"AND DA.SectionID = 0 " & _
	'			"AND DA.DriverID = " & driverID

	'	Try
	'		If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
	'			Return routeSectionsText
	'		End If
	'		If rs.Read() Then
	'			specialsRowExists = True
	'		End If
	'		SqlQueryClose(connSQL, rs)
	'	Catch ex As Exception

	'	End Try

	'	Dim blankDriverAssignmentID As Integer = 0
	'	If Not specialsRowExists Then
	'		' Need to create a specials row for this driver. Is there a blank one we can use?
	'		sql = "SELECT DriverAssignmentID " & _
	'			"FROM tblDriverAssignments DA " & _
	'			"INNER JOIN tlkRegions AS RG ON RG.RegionCode = DA.SectionDesc " & _
	'			"WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
	'				"AND DA.SectionID = 0 " & _
	'				"AND DA.DriverID IS NULL " & _
	'				"AND DA.LocationID = (SELECT DriverLocationID FROM tblDrivers WHERE DriverID = " & driverID & ") "
	'		Try
	'			If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
	'				Return routeSectionsText
	'			End If
	'			If rs.Read() Then
	'				blankDriverAssignmentID = rs("DriverAssignmentID")
	'			End If
	'			SqlQueryClose(connSQL, rs)
	'		Catch ex As Exception

	'		End Try
	'	End If

	'	If blankDriverAssignmentID = 0 Then
	'		' Insert new records
	'		sql = "INSERT INTO tblDriverAssignments (PickupScheduleSectionID, PickupScheduleID, PickupDate, " & _
	'				"RouteID, RouteCode, RouteDesc, SectionID, SectionCode, SectionDesc, " & _
	'				"LocationID, DriverID, TabletID, PhoneID, TruckID, LocationUnloadedID) " & _
	'			"SELECT TOP 1 DA.PickupScheduleSectionID, DA.PickupScheduleID, DA.PickupDate, DA.RouteID, DA.RouteCode, DA.RouteDesc, 0, ' ', RG.RegionCode, " & _
	'				"RG.RegionID, " & driverID & ", " & tabletID & ", " & PhoneID & ", " & truckID & ", " & _
	'				"(SELECT TOP (1) LocationID FROM tblLocations " & _
	'					"WHERE RegionID = (SELECT DriverLocationID FROM [tblDrivers] " & _
	'						"WHERE DriverID = " & driverID & ") AND DefaultLocation = 1) " & _
	'			"FROM tblDriverAssignments DA " & _
	'			"INNER JOIN tblPickupSchedule PS ON PS.PickupScheduleID = DA.PickupScheduleID " & _
	'			"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
	'			"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
	'			"INNER JOIN tlkRegions RG ON PCDL.RegionID = RG.RegionID " & _
	'			"INNER JOIN tblDrivers D ON D.DriverLocationID = PCDL.RegionID " & _
	'			"WHERE DA.PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
	'				 "AND DA.SectionID > 0 " & _
	'				 "AND D.DriverID = " & driverID
	'		SqlNonQuery(sql, "DriverAssignments, Page_Load")
	'	Else
	'		' Update the blank record
	'		sql = "UPDATE tblDriverAssignments " & _
	'			"SET DriverID = " & driverID & ", " & _
	'				"TabletID = " & tabletID & ", " & _
	'				"PhoneID = " & PhoneID & ", " & _
	'				"TruckID = " & truckID & ", " & _
	'				"LocationUnloadedID = CASE " & _
	'					"WHEN (LocationUnloadedID IS NULL " & _
	'						"OR (SELECT RegionID FROM tblLocations WHERE LocationID = LocationUnloadedID) <> " & _
	'							"(SELECT DriverLocationID FROM [tblDrivers] WHERE DriverID = " & driverID & ")) " & _
	'					"THEN (SELECT TOP (1) LocationID FROM tblLocations " & _
	'						"WHERE RegionID = (SELECT DriverLocationID FROM [tblDrivers] " & _
	'							"WHERE DriverID = " & driverID & ") AND DefaultLocation = 1) " & _
	'					"ELSE LocationUnloadedID END " & _
	'			"WHERE DriverAssignmentID = " & blankDriverAssignmentID
	'		SqlNonQuery(sql)
	'	End If

	'	Return 0
	'End Function

	'<WebMethod()> _
	'Public Shared Function SaveSpecialsSelections(ByVal pickupDate As Date, ByVal driverID As Integer, ByVal specialsText As String) As Integer
	'	Dim rs As SqlDataReader = Nothing
	'	Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

	'	Dim specials() As String = Split(specialsText, ";")
	'	Dim sql As String

	'	'Reset selections first.
	'	sql = "UPDATE tblSpecials " & _
	'		"SET DriverID = 0 " & _
	'		"WHERE PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "' " & _
	'			"AND DriverID = " & driverID
	'	SqlNonQuery(sql)


	'	For Each special In specials
	'		Dim specialFields() As String = Split(special, ":")
	'		Dim city As String = Trim(specialFields(0))
	'		Dim address As String = Trim(specialFields(1))

	'		sql = "UPDATE tblSpecials " & _
	'			"SET DriverID = " & driverID & _
	'			"WHERE City = '" & city & "' " & _
	'				"AND [Address] = '" & address & "' " & _
	'				"AND PickupDate = '" & Format(pickupDate, "MM/dd/yyyy") & "'"
	'		SqlNonQuery(sql)
	'	Next

	'	Return 0
	'End Function

	Protected Sub ddlDriverLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriverLocations.SelectedIndexChanged
		LoadAssignments()
	End Sub

	Protected Sub dtPickupDate_DateChanged(sender As Object, e As EventArgs) Handles dtPickupDate.DateChanged
		LoadAssignments()
	End Sub

	Protected Sub grdDriverAssignments_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs)
		Try
			Dim nt As String = CStr(grdDriverAssignments.GetRowValuesByKeyValue(e.EditingKeyValue, "NonTabletBagPickup"))
			If nt = "NT" Then
				grdDriverAssignments.JSProperties("cpServerMessage") = "A Non-Tablet Bag Pickup cannot be modified here."
				e.Cancel = True
			End If
		Catch
		End Try
	End Sub

	Protected Sub grdDriverAssignments_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		Try
			grdDriverAssignments.JSProperties("cpServerMessage") = Nothing
			If e.Values("NonTabletBagPickup") = "NT" Then
				grdDriverAssignments.JSProperties("cpServerMessage") = "A Non-Tablet Bag Pickup cannot be modified here."
				e.Cancel = True
			End If
			If Not IsDBNull(e.Values("BaggerID")) Then
				grdDriverAssignments.JSProperties("cpServerMessage") = "Cannot delete because Bagger information exists in record."
				e.Cancel = True
			End If
		Catch
		End Try
	End Sub

	Protected Sub grdDriverAssignments_CustomButtonBallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles grdDriverAssignments.CustomButtonCallback
		If e.ButtonID = "cbAddDriver" Then
			Dim sql As String = "INSERT INTO tblDriverAssignments (PickupScheduleSectionID, PickupScheduleID, PickupDate, " & _
					"RouteID, RouteCode, RouteDesc, SectionID, SectionCode, SectionDesc) " & _
				"VALUES (" & IIf(IsDBNull(grdDriverAssignments.GetRowValues(e.VisibleIndex, "PickupScheduleSectionID")), 0, grdDriverAssignments.GetRowValues(e.VisibleIndex, "PickupScheduleSectionID")) & ", " & _
					grdDriverAssignments.GetRowValues(e.VisibleIndex, "PickupScheduleID") & ", '" & _
					dtPickupDate.Value & "', " & _
					IIf(IsDBNull(grdDriverAssignments.GetRowValues(e.VisibleIndex, "RouteID")), 0, grdDriverAssignments.GetRowValues(e.VisibleIndex, "RouteID")) & ", '" & _
					grdDriverAssignments.GetRowValues(e.VisibleIndex, "RouteCode") & "', '" & _
					grdDriverAssignments.GetRowValues(e.VisibleIndex, "RouteDesc") & "', " & _
					grdDriverAssignments.GetRowValues(e.VisibleIndex, "SectionID") & ", '" & _
					grdDriverAssignments.GetRowValues(e.VisibleIndex, "SectionCode") & "', '" & _
					grdDriverAssignments.GetRowValues(e.VisibleIndex, "SectionDesc") & "')"
			SqlNonQuery(sql, "DriverAssignments, grdDriverAssignments_CustomButtonBallback, cbAddDriver")
			LoadAssignments()
		End If
		If e.ButtonID = "cbAddSpecial" Then
			Dim sql As String = "INSERT INTO tblDriverAssignments (PickupScheduleSectionID, PickupScheduleID, PickupDate, " & _
					"RouteID, RouteCode, RouteDesc, SectionID, SectionCode, SectionDesc) " & _
				"VALUES (0, " & grdDriverAssignments.GetRowValues(e.VisibleIndex, "PickupScheduleID") & ", '" & _
					dtPickupDate.Value & "', " & _
					IIf(IsDBNull(grdDriverAssignments.GetRowValues(e.VisibleIndex, "RouteID")), 0, grdDriverAssignments.GetRowValues(e.VisibleIndex, "RouteID")) & ", '" & _
					grdDriverAssignments.GetRowValues(e.VisibleIndex, "RouteCode") & "', '" & _
					grdDriverAssignments.GetRowValues(e.VisibleIndex, "RouteDesc") & "', 0, '', '')"
			SqlNonQuery(sql, "DriverAssignments, grdDriverAssignments_CustomButtonBallback, cbAddSpecial")
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
			Case ColumnCommandButtonType.Edit
				e.Visible = NTVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex)
		End Select
	End Sub

	Private Function DeleteButtonVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim locationUnloadedID As Decimal = IIf(IsDBNull(grid.GetRowValues(visibleIndex, "LocationUnloadedID")), 0, grid.GetRowValues(visibleIndex, "LocationUnloadedID"))
		Dim approved = IsInventoryApproved(dtPickupDate.Value, locationUnloadedID, )
		If approved Then
			Return False
		End If

		Dim val1 As Object = grid.GetRowValues(visibleIndex, "PickupsSectionExists")
		Dim val2 As Object = grid.GetRowValues(visibleIndex, "NonTabletBagPickup")
		Return val1 <> 1 And _
			val2 <> "NT"

		'Dim row As Object = grid.GetRow(visibleIndex)
		'Return (CType(row, DataRowView))("PickupsSectionExists").ToString() <> "1" And _
		'	(CType(row, DataRowView))("NonTabletBagPickup").ToString() <> "NT"
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
		Dim row As Object = grid.GetRow(visibleIndex)
		Return (CType(row, DataRowView))("NonTabletBagPickup").ToString() <> "NT"
	End Function

	Protected Sub btnRunMissingDriverInformationReport_Click(ByVal sender As Object, ByVal e As EventArgs)
		Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, dtPickupDate.Value) & _
			"|endDate~" & dtPickupDate.Value & "|driverLocation~" & ddlDriverLocations.SelectedItem.Text
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Missing Driver Information" & parms)
	End Sub


End Class
