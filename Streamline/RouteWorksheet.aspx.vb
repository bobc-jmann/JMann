Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DevExpress.XtraPrinting
Imports DataUtil

Partial Class RouteWorksheet
    Inherits System.Web.UI.Page
    Private UnselectDate As Boolean = False
    Private MonthChanged As Boolean = False

    Private grdSectionAddressesPageSize As Integer = 20

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					Dim PostBackControlID As String = ""
					Try
						PostBackControlID = GetPostBackControl(Me.Page).ID
					Catch ex As Exception
						ja(ex.Message)
						Return
					End Try

					Select Case PostBackControlID
						Case "ddlRegions"
						Case "ddlRoutes"
						Case "ddlSections"
						Case "ddlDrivers"
						Case "ckShowAllAddresses"
							LoadAddresses()
						Case "btnDriverLog"
						Case "btnSendTexts"
						Case "btnSendTextsConfirm"
						Case "UpMain"
						Case "btnSection"
						Case "btnBreak"
						Case "btnLunch"
						Case "grid"
							LoadAddresses()
						Case "grdDriverLog"
						Case "btnSaveComments"
						Case Else
							LoadComboBoxes()
							LoadAddresses()
					End Select
				End If
			Case "GET"
				dtPickupDate.Value = Today
				LoadComboBoxes()
				grdDriverLog.ClientVisible = False
			Case "HEAD"
				'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
		End Select
    End Sub

	Private Shared Function SelectRoutes(ByVal pickupDate As Date, ByVal regionID As Integer) As String
		Dim sql As String
		sql = "SELECT PS.RouteID, PS.RouteCode + ' (' + CASE WHEN C2.CharityAbbr IS NULL THEN C.CharityAbbr ELSE C2.CharityAbbr END + ')' AS RouteCharity " & _
			"FROM tblPickupCycles AS PC " & _
			"INNER JOIN tblPickupSchedule AS PS ON PS.PickupCycleID = PC.PickupCycleID " & _
			"INNER JOIN tblPermits AS P ON P.PermitID = PC.PermitID " & _
			"INNER JOIN tblCharities AS C ON C.CharityID = P.CharityID " & _
			"INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.PickupCycleTemplatesDetailID = PS.PickupCycleTemplatesDetailID " & _
			"LEFT OUTER JOIN tblCharities AS C2 ON C2.CharityID = PCTD.CharityID " & _
			"CROSS APPLY (SELECT DISTINCT LocationID AS RegionID FROM tblDriverAssignments AS DA " & _
				"WHERE DA.PickupScheduleID = PS.PickupScheduleID AND DA.SectionID > 0) AS R " & _
			"WHERE PS.PickupDate = '" & Format(pickupDate, "yyyyMMdd") & "' "

		If regionID > 0 Then
			sql += "AND R.RegionID = " & regionID & " "
		End If

		sql += "UNION " & _
			"SELECT DISTINCT 0 AS RouteID, 'Specials' AS RoutePickupCycle " & _
			"FROM tblDriverAssignments AS DA " & _
			"WHERE DA.PickupDate = '" & Format(pickupDate, "yyyyMMdd") & "' " & _
				"AND DA.SectionID = 0 "

		If regionID > 0 Then
			sql += "AND DA.LocationID = " & regionID & " "
		End If

		sql += "ORDER BY RouteCharity"

		Return (sql)
	End Function

	Public Class Routes
		Public RouteID As Integer
		Public RoutePickupCycle As String
	End Class

	Protected Sub ddlRegions_SelectedIndexChanged(sender As Object, e As EventArgs)
		Dim da As SqlDataAdapter = New SqlDataAdapter(SelectRoutes(dtPickupDate.Value, ddlRegions.SelectedValue), vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)
		ddlRoutes.DataSource = dt
		ddlRoutes.DataTextField = "RouteCharity"
		ddlRoutes.DataValueField = "RouteID"
		ddlRoutes.DataBind()
		ddlRoutes.Items.Insert(0, New ListItem("Select Route & Charity", "-1"))
		ddlRoutes.SelectedValue = -1

		ddlSections.Items.Clear()
		ddlDrivers.Items.Clear()
		'lblRoute.Text = ""
	End Sub

	Public Function SelectRouteSections(ByVal pickupDate As Date, ByVal routeID As Integer) As String
		Dim sql As String
		If ddlRoutes.SelectedValue = 0 Then
			sql = "SELECT 0 AS SectionID, 0 AS RouteID, 'Specials' AS SectionCode, 'Specials' AS RouteCode "
		Else
			sql = "SELECT PSS.SectionID, PSS.SectionCode, PS.RouteID, PS.RouteCode " & _
					"FROM tblPickupSchedule AS PS " & _
					"INNER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleID = PS.PickupScheduleID " & _
					"WHERE PS.PickupDate = '" & Format(pickupDate, "yyyyMMdd") & "' " & _
						"AND PS.RouteID = " & routeID & " "
		End If

		sql += "ORDER BY SectionCode"

		Return (sql)
	End Function

	Protected Sub ddlRoutes_SelectedIndexChanged(sender As Object, e As EventArgs)
		ResetRoutes()
	End Sub

	Sub ResetRoutes()
		Dim sql As String = SelectRouteSections(dtPickupDate.Value, ddlRoutes.SelectedValue)
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)
		ddlSections.DataSource = dt
		ddlSections.DataTextField = "SectionCode"
		ddlSections.DataValueField = "SectionID"
		ddlSections.DataBind()
		If ddlRoutes.SelectedValue = 0 Then
			ddlSections.SelectedValue = 0
			LoadComments()
		Else
			ddlSections.Items.Insert(0, New ListItem("Select Section", "-1"))
			ddlSections.SelectedValue = -1
		End If

		Try
			If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
				Return
			End If

			'If rsql.Read() Then
			'	lblRoute.Text = rsql("RouteCode").ToString()
			'End If
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

		If ddlRoutes.SelectedValue = 0 Then
			hfPickupScheduleSectionID("hidden_value") = 0
		Else
			hfPickupScheduleSectionID("hidden_value") = -1
			ddlDrivers.Items.Clear()
		End If

		ClearLogFields()
	End Sub

	Protected Sub ddlSections_SelectedIndexChanged(sender As Object, e As EventArgs)
		ckShowAllAddresses.Checked = False
		LoadComments()
		LoadAddresses()
		grdDriverLog.DataBind()
	End Sub

	Protected Sub btnSaveComments_Click(sender As Object, e As EventArgs)
		If Not IsNothing(txtSectionComments.Text) Then
			txtSectionComments.Text.Replace(" '", "''")
		End If

		Dim sql As String = "UPDATE tblPickupScheduleSections " & _
			"SET Comments = '" & Replace(txtSectionComments.Text, "'", "''") & "' " & _
			"WHERE PickupScheduleSectionID = " & hfPickupScheduleSectionID("hidden_value")

		Try
			SqlNonQuery(sql)
		Catch ex As Exception
			wf("Route Worksheet|Save Comments Error|:" & sql & ":")
		Finally

		End Try
	End Sub


	Private Sub LoadComboBoxes()
		' Regions
		Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT RegionID, RegionCode FROM tlkRegions ORDER BY RegionCode", vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)
		ddlRegions.DataSource = dt
		ddlRegions.DataTextField = "RegionCode"
		ddlRegions.DataValueField = "RegionID"
		ddlRegions.DataBind()
		ddlRegions.Items.Insert(0, New ListItem("All", "0"))
		ddlRegions.SelectedValue = 0

		' Routes
		da = New SqlDataAdapter(SelectRoutes(dtPickupDate.Value, ddlRegions.SelectedValue), vConnStr)
		dt = New DataTable()
		da.Fill(dt)
		ddlRoutes.DataSource = dt
		ddlRoutes.DataTextField = "RouteCharity"
		ddlRoutes.DataValueField = "RouteID"
		ddlRoutes.DataBind()
		ddlRoutes.Items.Insert(0, New ListItem("Select Route & Charity", "-1"))
		ddlRoutes.SelectedValue = -1

		' RouteSections
		da = New SqlDataAdapter(SelectRouteSections(dtPickupDate.Value, ddlRoutes.SelectedValue), vConnStr)
		dt = New DataTable()
		da.Fill(dt)
		'If dt.Rows.Count > 0 Then
		'	lblRoute.Text = dt.Rows(0).Item("Routecode").ToString
		'	If ddlRoutes.SelectedValue <> -1 And lblRoute.Text = "Specials" Then
		'		lblRoute.Text = ""
		'	End If
		'End If
		ddlSections.DataSource = dt
		ddlSections.DataTextField = "SectionCode"
		ddlSections.DataValueField = "SectionID"
		ddlSections.DataBind()
		If ddlRoutes.SelectedValue = 0 Then
			ddlSections.SelectedValue = 0
		Else
			ddlSections.Items.Insert(0, New ListItem("Select Section", "-1"))
			ddlSections.SelectedValue = -1
		End If

		If ddlSections.SelectedValue >= 0 Then
			divButtons.Style.Add("display", "block")
		Else
			divButtons.Style.Add("display", "none")
		End If
		If (ddlSections.SelectedValue > 0) Then
			divComments.Style.Add("display", "block")
		Else
			divComments.Style.Add("display", "none")
		End If
	End Sub

	Private Sub LoadAddresses()
		If ddlRoutes.SelectedValue = "" Or ddlSections.SelectedValue Is Nothing Then
			Return
		End If

        Dim sql As String =
            "SELECT PSD.PickupScheduleDetailID, PSD.Confirmed, PSD.Missed, PSD.RedTagged, A.DoNotRedTag, PSD.Comments, " &
                "A.StreetAddress, A.StreetName, A.City, A.Zip, ST.Status, A.LastDonationDate, A.MailingsSinceLastDonation, " &
                "A.Donations1Yr, A.Donations3Yr, " &
                "(SELECT COUNT(*) FROM tSysTextMessageLog AS TML WHERE TML.PickupScheduleDetailID = PSD.PickupScheduleDetailID) AS TextsSent " &
            "FROM tblPickupSchedule AS PS " &
            "INNER JOIN tblPickupScheduleDetail AS PSD ON PSD.PickupScheduleID = PS.PickupScheduleID " &
            "INNER JOIN tblAddresses AS A ON A.AddressID = PSD.AddressID " &
            "INNER JOIN tlkStatuses AS ST ON ST.StatusID = A.StatusID " &
            "WHERE PS.PickupDate = '" & Format(dtPickupDate.Value, "yyyyMMdd") & "' " &
                "AND PS.RouteID = " & ddlRoutes.SelectedValue & " " &
                "AND PSD.SectionID = " & ddlSections.SelectedValue & " " &
                "AND PSD.EmailNR = 0 " &
                "AND PSD.MailNR = 0 "

        If Not ckShowAllAddresses.Checked Then
			sql += "AND (PSD.Confirmed = 1 OR PSD.Missed = 1 OR PSD.RedTagged = 1 OR A.DoNotRedTag = 1 " & _
				"OR (PSD.Comments IS NOT NULL AND PSD.Comments <> '')) "
		End If

		sql += "ORDER BY A.StreetName, A.StreetAddress"
		dsAddresses.SelectCommand = sql
		grid.DataBind()
	End Sub

	Protected Sub grdLog_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("PickupScheduleDetailID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
	End Sub

	Sub LoadComments()
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim cnt As Integer = 0
		Dim textAddress As String = ""

		Dim sectionID As Integer = ddlSections.SelectedValue
		Dim routeID As Integer = ddlRoutes.SelectedValue
		Dim driverCnt As Integer = 0

		If ddlRoutes.SelectedValue = 0 Then
			sql = "SELECT DISTINCT '' AS Comments, 0 AS PickupScheduleSectionID, " & _
				"COALESCE(D.DriverID, 0) AS DriverID, COALESCE(D.DriverName, '&ltNot Assigned&gt') AS DriverName " & _
			"FROM tblDriverAssignments AS DA " & _
			"INNER JOIN tblDrivers AS D ON D.DriverID = DA.DriverID " & _
			"WHERE DA.PickupDate = '" & Format(dtPickupDate.Value, "yyyyMMdd") & "' " & _
				"AND DA.SectionID = 0 "
		Else
			sql = "SELECT COALESCE(PSS.Comments, '') AS Comments, PSS.PickupScheduleSectionID, " & _
					"COALESCE(D.DriverID, 0) AS DriverID, COALESCE(D.DriverName, '&ltNot Assigned&gt') AS DriverName " & _
				"FROM tblPickupScheduleSections AS PSS " & _
				"INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSS.PickupScheduleID " & _
				"LEFT OUTER JOIN tblDriverAssignments AS DA ON DA.PickupScheduleID = PS.PickupScheduleID " & _
					"AND DA.SectionID = PSS.SectionID " & _
				"LEFT OUTER JOIN tblDrivers AS D ON D.DriverID = DA.DriverID " & _
				"WHERE PS.RouteID = " & routeID & " " & _
					"AND PS.PickupDate = '" & Format(dtPickupDate.Value, "yyyyMMdd") & "' " & _
					"AND PSS.SectionID = " & sectionID & " "
		End If

		If ddlRegions.SelectedValue > 0 Then
			sql += "AND DA.LocationID = " & ddlRegions.SelectedValue & " "
		End If

		sql += "ORDER BY DriverName"

		Try
			If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
				Return
			End If

			ddlDrivers.Items.Clear()

			While rsql.Read()
				driverCnt += 1

				If driverCnt = 1 Then
					txtSectionComments.Text = rsql("Comments")
					hfPickupScheduleSectionID.Set("hidden_value", CInt(rsql("PickupScheduleSectionID")))
				End If

				Dim listItem As ListItem = New ListItem
				listItem.Value = rsql("DriverID")
				listItem.Text = rsql("DriverName")
				ddlDrivers.Items.Add(listItem)
			End While
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

		If driverCnt = 0 Then
			lblDrivers.Text = ""
		ElseIf driverCnt = 1 Then
			lblDrivers.Text = "Driver:"
		Else
			lblDrivers.Text = "Drivers:"
		End If

		If ddlSections.SelectedValue >= 0 Then
			divButtons.Style.Add("display", "block")
		Else
			divButtons.Style.Add("display", "none")
		End If
		If (ddlSections.SelectedValue > 0) Then
			divComments.Style.Add("display", "block")
		Else
			divComments.Style.Add("display", "none")
		End If

		PopulateLogEntries()
	End Sub

	Private Sub UpdateSections(ByVal sectionID As Integer, ByVal addressIDs As String, ByVal updateStatusIDs As Boolean)
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spAddressSection_Update"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@sectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, sectionID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@addressIDs", System.Data.ParameterDirection.Input, System.Data.DbType.String, addressIDs))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@updateStatusIDs", System.Data.ParameterDirection.Input, System.Data.DbType.String, updateStatusIDs))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, Session("vUserID")))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "RouteWorksheet, UpdateSections")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "RouteWorksheet, UpdateSections")
		Finally
			conn.Close()
		End Try

	End Sub

	Protected Sub btnSendTexts_Click(sender As Object, e As EventArgs)
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim cnt As Integer = 0
		Dim textAddress As String = ""
		Dim result As String = ""

		Dim pickupScheduleDetailIDs As String = ""
		Dim idList As List(Of Object) = grid.GetSelectedFieldValues("PickupScheduleDetailID")
		Dim addressList As List(Of Object) = grid.GetSelectedFieldValues("StreetAddress")
		Dim doNotRedTagList As List(Of Object) = grid.GetSelectedFieldValues("DoNotRedTag")
		Dim commentsList As List(Of Object) = grid.GetSelectedFieldValues("Comments")
		Dim messageCntPerDriver As Integer = idList.Count

		For i As Integer = 0 To ddlDrivers.Items.Count - 1
			Dim driverID As Integer = ddlDrivers.Items(i).Value
			Dim driverName As String = ddlDrivers.Items(i).Text

			'driverID = 71 ' RCC

			sql = "SELECT P.PhoneNumber + '@' + P.TextingDomain AS TextAddress " & _
				"FROM tblDrivers AS D " & _
				"INNER JOIN tblPhones AS P ON P.PhoneID = D.PhoneID " & _
				"WHERE D.DriverID = " & driverID

			Try
				If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
					Return
				End If
				rsql.Read()
				textAddress = rsql("TextAddress")
				SqlQueryClose(connSQL, rsql)
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			End Try

			If messageCntPerDriver = 0 Then
				lblSendTexts.Text = "Please select at least one Address."
				divBtnSendTextConfirm.Style.Add("display", "block")
				Return
			End If

			'Dim vSubject As String = lblRoute.Text & "-" & ddlSections.SelectedItem.Text
			Dim vSubject As String = ddlRoutes.SelectedItem.Text & "-" & ddlSections.SelectedItem.Text
			Dim vPriority As String = ""

			For j As Integer = 0 To messageCntPerDriver - 1
				Dim vBody As String = addressList(j)
				If doNotRedTagList(j) Then
					vBody += " - " & "Do Not Red Tag"
				End If
				If Not IsDBNull(commentsList(j)) AndAlso commentsList(j) <> "" Then
					vBody += " - " & commentsList(j)
				End If

				result = vbSendLogText(CInt(idList(j)), _
						CInt(Session("vUserID")), Session("vUserName"), Session("vUserEmail"),
							textAddress, vSubject, vBody, vPriority, "HTML", ddlDrivers.Items(i).Text)
			Next
		Next

		grid.Selection.UnselectAll()
		LoadAddresses()

		Dim totalMessageCnt As Integer = messageCntPerDriver * ddlDrivers.Items.Count
		If result <> "Your email has been sent." Then
			lblSendTexts.Text = result
		Else
			lblSendTexts.Text = totalMessageCnt & " text message(s) sent."
		End If

		divBtnSendText.Style.Add("display", "none")
		divBtnSendTextConfirm.Style.Add("display", "block")
	End Sub

	Protected Sub btnSendTextsConfirm_Click(sender As Object, e As EventArgs)
		lblSendTexts.Text = ""
		divBtnSendTextConfirm.Style.Add("display", "none")
	End Sub

	Protected Sub grdDriverLog_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)
		e.NewValues("DriverID") = ddlDrivers.SelectedValue
		e.NewValues("PickupDate") = dtPickupDate.Value
		e.NewValues("StartTime") = dtPickupDate.Value
		e.NewValues("EndTime") = dtPickupDate.Value
	End Sub

	Public Class LogResult
		Public lblStart As String
		Public lblEnd As String
	End Class

	Protected Sub btnSection_Click(sender As Object, e As EventArgs)
		Dim pickupDate As Date = dtPickupDate.Value
		Dim sectionID As Integer = ddlSections.SelectedValue
		Dim driverID As Integer = ddlDrivers.SelectedValue
		Dim cr As LogResult = New LogResult
		If ddlRoutes.SelectedValue = 0 Then
			If btnSection.Text = "Start Special" Then
				UpdateDriverLog(7, pickupDate, sectionID, driverID, True, cr)
				lblSectionStart.Text = cr.lblStart
				lblSectionEnd.Text = cr.lblEnd
				btnSection.Text = "End Special"
			Else
				UpdateDriverLog(7, pickupDate, sectionID, driverID, False, cr)
				lblSectionEnd.Text = cr.lblEnd
				btnSection.Text = "Start Special"
			End If
		Else
			If btnSection.Text = "Start Section" Then
				UpdateDriverLog(1, pickupDate, sectionID, driverID, True, cr)
				lblSectionStart.Text = cr.lblStart
				lblSectionEnd.Text = cr.lblEnd
				btnSection.Text = "End Section"
			Else
				UpdateDriverLog(1, pickupDate, sectionID, driverID, False, cr)
				lblSectionEnd.Text = cr.lblEnd
				btnSection.Text = "Start Section"
			End If
		End If

	End Sub

	Protected Sub btnBreak_Click(sender As Object, e As EventArgs)
		Dim pickupDate As Date = dtPickupDate.Value
		Dim sectionID As Integer = ddlSections.SelectedValue
		Dim driverID As Integer = ddlDrivers.SelectedValue
		Dim cr As LogResult = New LogResult
		If btnBreak.Text = "Start Break" Then
			UpdateDriverLog(4, pickupDate, sectionID, driverID, True, cr)
			lblBreakStart.Text = cr.lblStart
			lblBreakEnd.Text = cr.lblEnd
			btnBreak.Text = "End Break"
		Else
			UpdateDriverLog(4, pickupDate, sectionID, driverID, False, cr)
			lblBreakEnd.Text = cr.lblEnd
			btnBreak.Text = "Start Section"
		End If

	End Sub

	Protected Sub btnLunch_Click(sender As Object, e As EventArgs)
		Dim pickupDate As Date = dtPickupDate.Value
		Dim sectionID As Integer = ddlSections.SelectedValue
		Dim driverID As Integer = ddlDrivers.SelectedValue
		Dim cr As LogResult = New LogResult
		If btnLunch.Text = "Start Lunch" Then
			UpdateDriverLog(5, pickupDate, sectionID, driverID, True, cr)
			lblLunchStart.Text = cr.lblStart
			lblLunchEnd.Text = cr.lblEnd
			btnLunch.Text = "End Lunch"
		Else
			UpdateDriverLog(5, pickupDate, sectionID, driverID, False, cr)
			lblLunchEnd.Text = cr.lblEnd
			btnLunch.Text = "Start Lunch"
		End If

	End Sub

	Public Sub UpdateDriverLog(ByVal actionID As Integer, ByVal pickupDate As Date, ByVal sectionID As Integer, _
			ByVal driverID As Integer, ByVal start As Boolean, ByRef cr As LogResult)

		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim driverLogID As Integer = 0
		Dim driverAssignmentID As Integer = 0
		Dim startTime As DateTime? = Nothing
		Dim endTime As DateTime? = Nothing
		Dim comments As String = ""

		If Not start Then
			' Find the record to update.
			sql = "SELECT TOP (1) DL.DriverLogID, DL.DriverAssignmentID, DL.StartTime, DL.EndTime, DL.Comments " & _
				"FROM tblDriverLog AS DL " & _
				"LEFT OUTER JOIN tblDriverAssignments AS DA ON DA.DriverAssignmentID = DL.DriverAssignmentID " & _
				"WHERE DL.ActionID = " & actionID & " " & _
					"AND DL.PickupDate = '" & Format(pickupDate, "yyyyMMdd") & "' " & _
					"AND DL.DriverID = " & driverID & " " & _
					"AND DL.EndTime IS NULL "

			If actionID = 1 Then
				sql += "AND DA.SectionID = " & sectionID & " "
			End If
			sql += "ORDER BY DL.DriverLogID DESC"

			Try
				If Not SqlQueryOpen(connSQL, rsql, sql, "") Then
					Return
				End If

				If rsql.Read() Then
					driverLogID = CInt(rsql("DriverLogID"))
					If Not rsql("DriverAssignmentID").ToString() = "" Then
						driverAssignmentID = CInt(rsql("DriverAssignmentID").ToString())
					End If
					If Not rsql("StartTime").ToString() = "" Then
						startTime = CDate(rsql("StartTime").ToString())
					End If
					If Not rsql("EndTime").ToString() = "" Then
						endTime = CDate(rsql("EndTime").ToString())
					End If
					If Not rsql("Comments").ToString() = "" Then
						comments = rsql("Comments").ToString()
					End If
				End If
				SqlQueryClose(connSQL, rsql)
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "")
			End Try
		End If

		If start Then
			startTime = DateTime.Now
			cr.lblStart = Format(startTime, "HH:mm")
			cr.lblEnd = ""
		Else
			endTime = DateTime.Now
			cr.lblEnd = Format(endTime, "HH:mm")
		End If

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = connSQL

		If driverLogID = 0 Then
			myCmd.CommandText = "spDriverLog_Insert"
			If actionID = 1 Then
				sql = "SELECT DriverAssignmentID " & _
					"FROM tblDriverAssignments " & _
					"WHERE PickupDate = '" & Format(pickupDate, "yyyyMMdd") & "' " & _
						"AND SectionID = " & sectionID & " " & _
						"AND DriverID = " & driverID
				Try
					If Not SqlQueryOpen(connSQL, rsql, sql, "") Then
						Return
					End If

					If rsql.Read() Then
						driverAssignmentID = CInt(rsql("DriverAssignmentID").ToString())
					End If
					SqlQueryClose(connSQL, rsql)
				Catch ex As Exception
					LogProgramError(ex.Message, "", ex.StackTrace, "")
				End Try

			End If
		Else
			myCmd.CommandText = "spDriverLog_Update"
			myCmd.Parameters.Add(DataUtil.CreateParameter("@DriverLogID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, driverLogID))
		End If
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@driverID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, driverID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupDate", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, pickupDate))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@actionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, actionID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@driverAssignmentID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(driverAssignmentID = 0, System.DBNull.Value, driverAssignmentID)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@startTime", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(IsNothing(startTime), System.DBNull.Value, startTime)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@endTime", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(IsNothing(endTime), System.DBNull.Value, endTime)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@comments", System.Data.ParameterDirection.Input, System.Data.DbType.String, comments.Replace("'", "''")))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			connSQL.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "RouteWorksheet, UpdateDriverLog")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "", "RouteWorksheet, UpdateDriverLog")
		Finally
			connSQL.Close()
		End Try

		grdDriverLog.DataBind()
		Return
	End Sub

	Protected Sub btnDriverLog_Click(sender As Object, e As EventArgs)
		If btnDriverLog.Text = "Show Driver Log" Then
			btnDriverLog.Text = "Hide Driver Log"
			grdDriverLog.ClientVisible = True
		Else
			btnDriverLog.Text = "Show Driver Log"
			grdDriverLog.ClientVisible = False
		End If
	End Sub

	Protected Sub ddlDrivers_SelectedIndexChanged(sender As Object, e As EventArgs)
		PopulateLogEntries()
		grdDriverLog.DataBind()
	End Sub

	Public Sub PopulateLogEntries()
		If ddlDrivers.SelectedValue = 0 Then
			Return
		End If

		' Does this driver have any pending or completed log entries?
		Dim pickupDate As Date = dtPickupDate.Value
		Dim sectionID As Integer = ddlSections.SelectedValue
		Dim driverID As Integer = ddlDrivers.SelectedValue
		Dim cr As LogResult = New LogResult

		' Section
		If ddlRoutes.SelectedValue = 0 Then
			FindDriverLogEntries(7, pickupDate, sectionID, driverID, cr)
			If (cr.lblStart = "" And cr.lblEnd = "") Or cr.lblEnd <> "" Then
				btnSection.Text = "Start Special"
			Else
				btnSection.Text = "End Special"
			End If
			lblSectionStart.Text = cr.lblStart
			lblSectionEnd.Text = cr.lblEnd
		Else
			FindDriverLogEntries(1, pickupDate, sectionID, driverID, cr)
			If (cr.lblStart = "" And cr.lblEnd = "") Or cr.lblEnd <> "" Then
				btnSection.Text = "Start Section"
			Else
				btnSection.Text = "End Section"
			End If
			lblSectionStart.Text = cr.lblStart
			lblSectionEnd.Text = cr.lblEnd
		End If

		' Break
		FindDriverLogEntries(4, pickupDate, sectionID, driverID, cr)
		If (cr.lblStart = "" And cr.lblEnd = "") Or cr.lblEnd <> "" Then
			btnBreak.Text = "Start Break"
		Else
			btnBreak.Text = "End Break"
		End If
		lblBreakStart.Text = cr.lblStart
		lblBreakEnd.Text = cr.lblEnd

		' Lunch
		FindDriverLogEntries(5, pickupDate, sectionID, driverID, cr)
		If (cr.lblStart = "" And cr.lblEnd = "") Or cr.lblEnd <> "" Then
			btnLunch.Text = "Start Lunch"
		Else
			btnLunch.Text = "End Lunch"
		End If
		lblLunchStart.Text = cr.lblStart
		lblLunchEnd.Text = cr.lblEnd
	End Sub

	Sub FindDriverLogEntries(ByVal actionID As Integer, ByVal pickupDate As Date, _
			ByVal sectionID As Integer, ByVal driverID As Integer, ByRef cr As LogResult)
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim driverLogID As Integer = 0
		Dim startTime As DateTime? = Nothing
		Dim endTime As DateTime? = Nothing

		cr.lblStart = ""
		cr.lblEnd = ""

		' Is there a record for the actionID?
		sql = "SELECT TOP (1) DL.DriverLogID, DL.StartTime, DL.EndTime " & _
			"FROM tblDriverLog AS DL " & _
			"LEFT OUTER JOIN tblDriverAssignments AS DA ON DA.DriverAssignmentID = DL.DriverAssignmentID " & _
			"WHERE DL.ActionID = " & actionID & " " & _
				"AND DL.PickupDate = '" & Format(pickupDate, "yyyyMMdd") & "' " & _
				"AND DL.DriverID = " & driverID & " "

		If actionID = 1 Then
			sql += "AND DA.SectionID = " & sectionID & " "
		End If
		sql += "ORDER BY DL.StartTime DESC "

		Try
			If Not SqlQueryOpen(connSQL, rsql, sql, "") Then
				Return
			End If

			If rsql.Read() Then
				driverLogID = CInt(rsql("DriverLogID"))
				If Not rsql("StartTime").ToString() = "" Then
					startTime = CDate(rsql("StartTime").ToString())
					cr.lblStart = Format(startTime, "HH:mm")
				End If
				If Not rsql("EndTime").ToString() = "" Then
					endTime = CDate(rsql("EndTime").ToString())
					cr.lblEnd = Format(endTime, "HH:mm")
				End If
			End If
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "")
		End Try

	End Sub

	Sub ClearLogFields()
		lblSectionStart.Text = ""
		lblSectionEnd.Text = ""
		lblBreakStart.Text = ""
		lblBreakEnd.Text = ""
		lblLunchStart.Text = ""
		lblLunchEnd.Text = ""
	End Sub

	Protected Sub grdDriverLog_RowUpdated(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatedEventArgs)
		PopulateLogEntries()
	End Sub

	Protected Sub grdDriverLog_RowDeleted(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletedEventArgs)
		PopulateLogEntries()
	End Sub


End Class

