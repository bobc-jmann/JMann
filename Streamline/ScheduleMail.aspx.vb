Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common
Imports DataUtil

Partial Class ScheduleMail
    Inherits System.Web.UI.Page

    Private Const ScheduleTypeID As Integer = 1    ' Mail
    Private vDoIt As Boolean

    Sub On_sql_gridMain_Updating(ByVal source As Object, ByVal e As SqlDataSourceCommandEventArgs)
        Dim command As DbCommand
        Dim connection As DbConnection

        command = e.Command
        connection = command.Connection
        connection.Open()
        command.CommandTimeout = 360
    End Sub

    Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.PreRender
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
						myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduleTypeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(ScheduleTypeID)))

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
			"INNER JOIN tblPickupCycleTemplates AS PCT ON PC.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
			"WHERE PC.Active = 1 " & _
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
			gvcol.VisibleIndex = 0
			gvcol.Width = 100
			g.Columns.Insert(0, gvcol)
			g.ClientSideEvents.CustomButtonClick = "function(s,e) { if(e.buttonID=='delBtn') { cbCheckDates.PerformCallback(); }; if(e.buttonID=='resBtn') { cbCheckDatesResch.PerformCallback(); } }"
		End If
	End Sub

	Protected Sub gridMain_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles gridMain.RowValidating
		Dim sql As String
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim vToday As Date = Today()
		Dim exportedCount As Integer
		Dim x As Integer = gridMain.FocusedRowIndex

		sql = "SELECT COUNT(*) FROM tblScheduledPickupSectionsForExport	WHERE PickupScheduleID = " & gridMain.GetRowValues(x, "PickupScheduleID").ToString & " AND Exported = 1"
		exportedCount = SQLExecuteScalar(sql, "Notify User")
		If exportedCount > 0 Then
			e.RowError = "Cannot change a job that has been exported and sent to tablets."
		End If

		If e.OldValues("Exported") = True And (e.NewValues("ApprovedForExport") <> e.OldValues("ApprovedForExport")) Then
			e.RowError = "Cannot change Approved for Export on a job that has been exported."
		End If
		If e.OldValues("MailingDate") <> e.NewValues("MailingDate") And Not e.NewValues("MailingDate") > vToday Then
			e.RowError = "Mailing Date must be later than today."
		End If
		If e.OldValues("PickupDate") <> e.NewValues("PickupDate") And Not e.NewValues("PickupDate") > vToday Then
			e.RowError = "Pickup Date must be later than today."
		End If
		If Not e.NewValues("MailingDate") < e.NewValues("PickupDate") Then
			e.RowError = "Mailing Date must be earlier than Pickup Date."
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
			"WHERE PC.PickupCycleID = " & v
		Dim rsql As SqlDataReader = Nothing
		Dim connSql As SqlConnection = New SqlConnection(vConnStr)
		Try
			If Not SqlQueryOpen(connSql, rsql, sql, "Notify User") Then
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
			Dim vDate = g.GetRowValues(e.VisibleIndex, e.DataColumn.FieldName)
			If Not IsDBNull(vDate) Then
				vDoIt = False
				x = SQLExecuteScalar("SELECT COUNT(*) AS Cnt FROM tlkHolidays WHERE HolidayDate='" & vDate & "'", "")
				If Not ismt(x) Then
					vDoIt = True
				End If
				If Weekday(vDate) = 1 Then vDoIt = True
				If vDoIt Then
					e.Cell.ForeColor = Drawing.Color.Red
				End If
				If Weekday(vDate) = 7 Then
					e.Cell.ForeColor = Drawing.Color.Blue
				End If
			End If
		End If
	End Sub

	Sub vbCheckDates(s As Object, e As CallbackEventArgs)
		Dim x As Integer
		Dim vToday As Date = Today()
		Dim vPickupDate, vMailDate As Date
		e.Result = "OK"
		x = gridMain.FocusedRowIndex

		Dim sql As String = "SELECT PickupDate, MailingDate FROM qryScheduler WHERE PickupScheduleID = " & gridMain.GetRowValues(x, "PickupScheduleID").ToString
		Dim rsql As SqlDataReader = Nothing
		Dim connSql As SqlConnection = New SqlConnection(vConnStr)
		Try
			If Not SqlQueryOpen(connSql, rsql, sql, "Notify User") Then
				Return
			End If
			While rsql.Read()
				vPickupDate = rsql("PickupDate")
				vMailDate = rsql("MailingDate")
			End While
			SqlQueryClose(connSql, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

		If Not ismt(vMailDate) Then
			If vToday >= vMailDate Then
				e.Result = "Mailing date must be later than today."
			End If
		End If
		If Not ismt(vPickupDate) Then
			If vToday >= vPickupDate Then
				e.Result = "Pickup date must be later than today."
			End If
		End If
	End Sub

	Sub vbDeleteAfterCheck(s As Object, e As CallbackEventArgs)
		Dim x As Integer = gridMain.FocusedRowIndex
		Dim productionScheduleID As Integer = CInt(gridMain.GetRowValues(x, "PickupScheduleID"))
		If ismt(productionScheduleID) Then
			Return
		End If
		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spScheduleDelete"
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

	Sub vbReschAfterCheck(s As Object, e As CallbackEventArgs)
		Dim x As Integer = gridMain.FocusedRowIndex
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
End Class
