Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common
Imports DataUtil

Partial Class ScheduleBags
    Inherits System.Web.UI.Page

    Private Const scheduleTypeID As Integer = 3    ' Bags
    Private vDoIt As Boolean

    Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.PreRender
    End Sub

    Sub On_sql_gridMain_Updating(ByVal source As Object, ByVal e As SqlDataSourceCommandEventArgs)
        Dim command As DbCommand
        Dim connection As DbConnection

        command = e.Command
        connection = command.Connection
        connection.Open()
        command.CommandTimeout = 360
    End Sub

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Dim v As System.Web.UI.Control
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		If IsPostBack Then
			v = GetPostBackControl(Me.Page)
			If Not v Is Nothing Then
				Select Case v.ID
					Case "PickupCycleID"
						vbShowPickupData()
					Case "btnCreateSchedule"
						Dim conn As SqlConnection = New SqlConnection(vConnStr)
						Dim myCmd As SqlCommand = New SqlCommand()
						myCmd.Connection = conn
						myCmd.CommandText = "spScheduleBuild"
						myCmd.CommandType = System.Data.CommandType.StoredProcedure

						myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupCycleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(PickupCycleID.Value)))
						myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduleTypeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(scheduleTypeID)))

						Try
							conn.Open()
							myCmd.CommandTimeout = 360
							myCmd.ExecuteNonQuery()
						Catch SqlEx As SqlException
							Dim se As SqlError
							For Each se In SqlEx.Errors
								ja(se.Message)
							Next
						Finally
							conn.Close()
						End Try

						gridMain.DataBind()
						vbShowPickupData()
				End Select
			End If
		End If
		sql_PickupCycles.SelectCommand = "SELECT PC.PickupCycleID, PC.PickupCycleAbbr " & _
			"FROM tblPickupCycles AS PC " & _
			"INNER JOIN tblPickupCycleDriverLocations AS PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"LEFT OUTER JOIN tblPickupCycleTemplates AS PCT ON PC.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
			"WHERE (PCT.ScheduleTypeID <> 2 OR PCT.ScheduleTypeID IS NULL) " & _
				"AND PC.Active = 1 " & _
				"AND PCDL.PrimaryRegion = 1 " & _
				"AND PCDL.RegionID IN " & Session("userRegionsList") & " " & _
			"ORDER BY PickupCycleAbbr"
	End Sub

    Sub vbHideID(g As ASPxGridView, e As EventArgs)
        If Not g.Columns("PickupCycleID") Is Nothing Then g.Columns("PickupCycleID").Visible = False
        If Not g.Columns("PickupScheduleID") Is Nothing Then g.Columns("PickupScheduleID").Visible = False
        g.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm
        If g.Columns.Count > 0 And (g.Columns("grdCommands") Is Nothing) Then
            Dim gvcol As New DevExpress.Web.GridViewCommandColumn("Edit")
            gvcol.Name = "grdCommands"
            gvcol.ShowEditButton = True
            gvcol.HeaderStyle.HorizontalAlign = 2
            Dim delBtn As New GridViewCommandColumnCustomButton
            delBtn.Visibility = GridViewCustomButtonVisibility.AllDataRows
            delBtn.Text = "Del"
            delBtn.ID = "delBtn"
            gvcol.CustomButtons.Add(delBtn)
            Dim resBtn As New GridViewCommandColumnCustomButton
            resBtn.Visibility = GridViewCustomButtonVisibility.AllDataRows
            resBtn.Text = "Resched"
            resBtn.ID = "resBtn"
            gvcol.CustomButtons.Add(resBtn)

            ' If Admin user or Lisa, allow Special Delete
            Dim sql As String
            Dim rsql As SqlDataReader = Nothing
            Dim connSql As SqlConnection = New SqlConnection(vConnStr)
            Dim specialDelete As Boolean = False
            Try
                sql = "SELECT UserGroup FROM users.Users WHERE Username = '" & Session("vUsername") & "'"
                rsql = Nothing
                If Not SqlQueryOpen(connSql, rsql, sql, "Notify Web User") Then
                    Return
                End If
                If rsql.Read() Then
                    If rsql("UserGroup").ToString = "ADMIN" Or Session("vUsername") = "lCecil" Or Session("vUsername") = "kIsaac" Then
                        Dim sdBtn As New GridViewCommandColumnCustomButton
                        sdBtn.Visibility = GridViewCustomButtonVisibility.AllDataRows
                        sdBtn.Text = "SpDel"
                        sdBtn.ID = "sdBtn"
                        gvcol.CustomButtons.Add(sdBtn)
                        specialDelete = True
                    End If
                End If
                SqlQueryClose(connSql, rsql)
            Catch
            End Try

            gvcol.VisibleIndex = 0
            gvcol.Width = 160
            g.Columns.Insert(0, gvcol)
            If specialDelete Then
                g.ClientSideEvents.CustomButtonClick = "function(s,e) { if(e.buttonID=='delBtn') { cbCheckDates.PerformCallback(); }; if(e.buttonID=='resBtn') { cbCheckDatesResch.PerformCallback(); } if(e.buttonID=='sdBtn') { cbSpecialDelete.PerformCallback(); } }"
            Else
                g.ClientSideEvents.CustomButtonClick = "function(s,e) { if(e.buttonID=='delBtn') { cbCheckDates.PerformCallback(); }; if(e.buttonID=='resBtn') { cbCheckDatesResch.PerformCallback(); } }"
            End If
       End If
    End Sub

    Protected Sub gridMain_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles gridMain.RowValidating
        Dim sql As String
        Dim rsql As SqlDataReader = Nothing
        Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim vToday As Date = Today()
        Dim exportedCount As Integer
        Dim x As Integer = gridMain.FocusedRowIndex

        Dim nt As String = CStr(gridMain.GetRowValues(x, "NonTabletBagPickup"))
        If nt = "NT" Then
            e.RowError = "A Non-Tablet Bag Pickup cannot be modified here."
        End If

        sql = "SELECT COUNT(*) FROM tblScheduledPickupSectionsForExport	WHERE PickupScheduleID = " & gridMain.GetRowValues(x, "PickupScheduleID").ToString & " AND Exported = 1"
        exportedCount = SQLExecuteScalar(sql, "Notify User")
        If exportedCount > 0 Then
            e.RowError = "Cannot change a job that has been exported and sent to tablets."
        End If

        If gridMain.GetRowValues(x, "ScheduleTypeID").ToString > "3" Then
            e.RowError = "Cannot edit MAG or MAP schedules from Bag Scheduler."
        End If
        If e.OldValues("Exported") = True And (e.NewValues("ApprovedForExport") <> e.OldValues("ApprovedForExport")) Then
            e.RowError = "Cannot change Approved for Export on a job that has been exported."
        End If
        If Not e.NewValues("PickupDate") > vToday Then
            e.RowError = "Pickup Date must be later than today."
        End If
    End Sub

	Sub vbShowPickupData()
		Dim v As Integer
		v = PickupCycleID.Value
		Dim sql As String = "SELECT PC.PickupCycleAbbr, PC.PickupCycleDesc, PCT.PickupCycleTemplateCode, " & _
				"PC.LastWeekScheduled, PC.LastDayScheduled, PCT.DefaultDaysToSchedule, PT.ScheduleType, P.PermitNbr, C.CharityAbbr " & _
			"FROM tblPickupCycles AS PC " & _
			"LEFT OUTER JOIN tblPickupCycleTemplates AS PCT ON PC.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
			"INNER JOIN tlkScheduleTypes AS PT ON PCT.ScheduleTypeID = PT.ScheduleTypeID " & _
			"INNER JOIN tblPermits AS P ON PC.PermitID = P.PermitID " & _
			"INNER JOIN tblCharities AS C ON P.CharityID = C.CharityID " & _
			"WHERE PC.PickupCycleID = @PickupCycleID"
		Dim rsql As SqlDataReader = Nothing
		Dim connSql As SqlConnection = New SqlConnection(vConnStr)
		Dim parms As List(Of QueryParms) = New List(Of QueryParms)
		Dim p As QueryParms = New QueryParms
		p.parmName = "@PickupCycleID"
		p.dbType = SqlDbType.Int
		p.value = v
		parms.Add(p)
		Try
			If Not SqlQueryOpenWithParms(connSql, rsql, sql, "Notify User", parms) Then
				Return
			End If
			While rsql.Read()
				PickupCycleAbbr.Value = rsql("PickupCycleAbbr")
				PickupCycleDesc.Value = rsql("PickupCycleDesc")
				PickupCycleTemplateCode.Value = rsql("PickupCycleTemplateCode")
				LastWeekScheduled.Value = rsql("LastWeekScheduled")
				LastDayScheduled.Value = rsql("LastDayScheduled")
				ScheduleType.Value = rsql("ScheduleType")
				CharityAbbr.Value = rsql("CharityAbbr")
				PermitNbr.Value = rsql("PermitNbr")
			End While
			SqlQueryClose(connSql, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try
		tblPickup.Visible = True
	End Sub

	Protected Sub vbOnHtmlDataCellPrepared(ByVal g As ASPxGridView, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
		Dim x As Integer
		If e.DataColumn.FieldName = "PickupDate" Or e.DataColumn.FieldName = "MailingDate" Then
			Dim vDt = g.GetRowValues(e.VisibleIndex, e.DataColumn.FieldName)
			If Not IsDBNull(vDt) Then
				vDoIt = False
				x = SQLExecuteScalar("select count(*) as ct from tlkHolidays where HolidayDate='" & vDt & "'", "")
				If Not ismt(x) Then
					vDoIt = True
				End If
				If Weekday(vDt) = 1 Then vDoIt = True
				If vDoIt Then
					e.Cell.ForeColor = Drawing.Color.Red
				End If
				If Weekday(vDt) = 7 Then
					e.Cell.ForeColor = Drawing.Color.Blue
				End If
			End If
		End If
	End Sub

	Sub vbCheckDates(s As Object, e As CallbackEventArgs)
		Dim x As Integer
		Dim vToday As Date = Today()
		Dim vPickupDate As Date = Nothing
		Dim vMailDate As Date = Nothing
		e.Result = "OK"
		x = gridMain.FocusedRowIndex

		Dim nt As String = CStr(gridMain.GetRowValues(x, "NonTabletBagPickup"))
		If nt = "NT" Then
			e.Result = "A Non-Tablet Bag Pickup cannot be modified here."
			Return
		End If

		Dim sql As String = "SELECT PickupDate, MailingDate FROM qryScheduler WHERE PickupScheduleID = " & gridMain.GetRowValues(x, "PickupScheduleID").ToString
		Dim rsql As SqlDataReader = Nothing
		Dim connSql As SqlConnection = New SqlConnection(vConnStr)
		Try
			If Not SqlQueryOpen(connSql, rsql, sql, "Notify User") Then
				Return
			End If
			While rsql.Read()
				If Not IsDBNull(rsql("PickupDate")) Then
					vPickupDate = rsql("PickupDate")
				End If
				If Not IsDBNull(rsql("MailingDate")) Then
					vMailDate = rsql("MailingDate")
				End If
			End While
			SqlQueryClose(connSql, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

		If Not vPickupDate = DateTime.MinValue Then
			If vToday >= vPickupDate Then
				e.Result = "Pickup date must be later than today."
				Return
			End If
		End If
	End Sub

    Sub vbSpecialDelete(s As Object, e As CallbackEventArgs)
        Dim x = gridMain.FocusedRowIndex
        Dim nt As String = CStr(gridMain.GetRowValues(x, "NonTabletBagPickup"))
        If nt = "NT" Then
            e.Result = "A Non-Tablet Bag Pickup cannot be modified here."
            Return
        End If

        e.Result = DeleteProductionSchedule(True)
    End Sub

    Sub vbDeleteAfterCheck(s As Object, e As CallbackEventArgs)
        e.Result = DeleteProductionSchedule(False)
    End Sub

	Function DeleteProductionSchedule(ByVal specialDelete As Boolean) As Integer
		Dim x = gridMain.FocusedRowIndex
		Dim productionScheduleID As Integer = CInt(gridMain.GetRowValues(x, "PickupScheduleID"))
		If ismt(productionScheduleID) Then
			Return 0
		End If
		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spScheduleDelete"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, productionScheduleID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@specialDeletePermission", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, specialDelete))

		Dim errorCnt As Integer = 0
		Try
			conn.Open()
			myCmd.CommandTimeout = 360
			myCmd.ExecuteNonQuery()
		Catch SqlEx As SqlException
			Dim se As SqlError
			errorCnt = SqlEx.Errors.Count
			For Each se In SqlEx.Errors
				ja(se.Message)
			Next
		Finally
			conn.Close()
		End Try

		Return errorCnt
	End Function

	Sub vbReschAfterCheck(s As Object, e As CallbackEventArgs)
		Dim x As Integer = 0

		Dim nt As String = CStr(gridMain.GetRowValues(x, "NonTabletBagPickup"))
		If nt = "NT" Then
			e.Result = "A Non-Tablet Bag Pickup cannot be modified here."
		End If

		Dim productionScheduleID As Integer = CInt(gridMain.GetRowValues(x, "PickupScheduleID"))
		If ismt(productionScheduleID) Then
			Return
		End If
		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spScheduleBuild"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, productionScheduleID))

		Dim errorCnt As Integer = 0
		Try
			conn.Open()
			myCmd.CommandTimeout = 360
			myCmd.ExecuteNonQuery()
		Catch SqlEx As SqlException
			Dim se As SqlError
			errorCnt = SqlEx.Errors.Count
			For Each se In SqlEx.Errors
				ja(se.Message)
			Next
		Finally
			conn.Close()
		End Try

		e.Result = errorCnt
	End Sub

    Protected Sub btnSchedule2Popup(ByVal sender As Object, ByVal e As EventArgs)
        If PickupCycleID.Value = 0 Then
            ja("Please select a Pickup Cycle")
            Return
        End If

        Dim popup As ASPxPopupControl = DirectCast(Page.FindControl("popSchedule2"), ASPxPopupControl)
		Dim sql As String = "SELECT DISTINCT R.RouteID, R.RouteCode " & _
			"FROM tblRoutes AS R " & _
			"INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.RouteID = R.RouteID " & _
			"INNER JOIN tblPickupCycleTemplates AS PCT ON PCT.PickupCycleTemplateID = PCTD.PickupCycleTemplateID " & _
			"INNER JOIN tblPickupCycles AS PC ON PC.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
			"WHERE PickupCycleID = " & PickupCycleID.Value & " " & _
				"AND R.Active = 1 " & _
			"ORDER BY R.RouteCode"

        dsRoutes.SelectCommand = sql
        dsRoutes.DataBind()

        popup.ShowOnPageLoad = True
    End Sub

    Protected Sub btnCreateSchedule2OK(ByVal sender As Object, ByVal e As EventArgs)
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim myCmd As SqlCommand = New SqlCommand()
        myCmd.Connection = conn
        myCmd.CommandText = "spScheduleBuild"
        myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupCycleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(PickupCycleID.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduleTypeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 3))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@specificRouteID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, routeID.Value))

		Try
			conn.Open()
			myCmd.CommandTimeout = 360
			myCmd.ExecuteNonQuery()
		Catch SqlEx As SqlException
			Dim se As SqlError
			For Each se In SqlEx.Errors
				ja(se.Message)
			Next
		Finally
			conn.Close()
		End Try

		gridMain.DataBind()
        vbShowPickupData()

        popSchedule2.ShowOnPageLoad = False
    End Sub

End Class
