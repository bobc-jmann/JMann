Imports System.Data
Imports System.Data.Common
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports DataUtil
Imports DevExpress.Utils
Imports System.Web.UI.PageAsyncTask

Partial Class ScheduledMail
	Inherits System.Web.UI.Page

	Private cntNotPrinted As Integer

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Select Case Request.HttpMethod
			Case "POST"

			Case "GET"
				dtEarliestMailingDate.Value = DateAdd(DateInterval.Day, -7, Now)
				hfExport("timerRunning") = False
				hfExport("token") = System.Guid.Empty
				hfExport("PickupScheduleID") = 0
				hfExport("count") = 0

				dtMailingDate.Value = Today
			Case "HEAD"

		End Select

		Dim sql As String = "SELECT COUNT(*) AS Cnt FROM qryScheduler " & _
			"WHERE Printed = 0 AND ApprovedForExport = 1 AND ScheduleTypeID IN (1, 4, 5)"
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
			Return
		End If
		If rsql.Read() Then
			cntNotPrinted = CInt(rsql("Cnt").ToString)
		Else
			cntNotPrinted = 0
		End If
		SqlQueryClose(connSQL, rsql)

		If cntNotPrinted > vMaxMailInPrintScheduler Then
			lblTooMany.Visible = True
		Else
			lblTooMany.Visible = False
		End If
	End Sub

	Protected Sub vbOnHtmlDataCellPrepared(ByVal g As ASPxGridView, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
		Dim x As Integer
		Dim vDoIt As Boolean
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
					e.Cell.ForeColor = Color.Red
				End If
				If Weekday(vDate) = 7 Then
					e.Cell.ForeColor = Color.Blue
				End If
			End If
		End If
	End Sub

	Protected Sub gridMain_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		Dim visible As Boolean = CommandButtonVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex)

		Select Case e.ButtonType
			Case ColumnCommandButtonType.Delete
				e.Visible = visible
			Case ColumnCommandButtonType.Edit
				e.Visible = visible
		End Select
	End Sub

	Protected Sub gridMain_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		Dim visible As Boolean = CommandButtonVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex)

		If e.ButtonID = "cbReschedule" Then
			If visible Then
				e.Visible = DefaultBoolean.True
			Else
				e.Visible = DefaultBoolean.False
			End If
		End If
	End Sub

	Private Function CommandButtonVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim row As Object = grid.GetRow(visibleIndex)
		If IsNothing(row) Then
			Return False
		End If
		Return (CType(row, DataRowView))("ExportedToTablets").ToString() = "0"
	End Function

	Protected Sub gridMain_CustomButtonBallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles gridMain.CustomButtonCallback
		If e.ButtonID = "cbReschedule" Then
			Dim pickupScheduleID As Integer = CInt(gridMain.GetRowValues(e.VisibleIndex, "PickupScheduleID"))
			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spScheduleBuild"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, pickupScheduleID))

			Dim errorCnt As Integer = 0
			Try
				conn.Open()
				myCmd.CommandTimeout = 360
				myCmd.ExecuteNonQuery()
			Catch SqlEx As SqlException
				Dim se As SqlError
				errorCnt = SqlEx.Errors.Count
				For Each se In SqlEx.Errors
					sender.JSProperties("cpRescheduleMessage") = se.Message
				Next
			Finally
				conn.Close()
			End Try

			If errorCnt = 0 Then
				sender.JSProperties("cpRescheduleMessage") = "Reschedule completed."
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

		If e.NewValues("ApprovedForExport") AndAlso Not e.OldValues("ApprovedForExport") AndAlso cntNotPrinted > vMaxMailInPrintScheduler Then
			e.RowError = "Print queue full."
		End If
	End Sub

	Protected Sub btnSelectUnapproved_Click(sender As Object, e As EventArgs)
		For i As Integer = 0 To gridMain.VisibleRowCount - 1
			Dim approved As Boolean = gridMain.GetRowValues(i, "ApprovedForExport")
			Dim exported As Boolean = gridMain.GetRowValues(i, "Exported")
			Dim mailingDate As Date = CDate(gridMain.GetRowValues(i, "MailingDate"))
			If Not approved And Not exported And (mailingDate <= dtMailingDate.Value) Then
				gridMain.Selection.SelectRow(i)
			End If
		Next
	End Sub

	Protected Sub btnApproveSelected_Click(sender As Object, e As EventArgs)
		Dim approveList As List(Of Object) = gridMain.GetSelectedFieldValues("PickupScheduleID")

		If approveList.Count = 0 Then
			ja("Please select at least one Pickup Schedule to approve.")
			Return
		End If

		If cntNotPrinted > vMaxMailInPrintScheduler Then
			Return
		End If
		If approveList.Count + cntNotPrinted > vMaxMailInPrintScheduler Then
			lblTooManySelected.Text = "Too many jobs selected. The print queue has room for " & vMaxMailInPrintScheduler - cntNotPrinted & " jobs."
			lblTooManySelected.Visible = True
			Return
		End If

		For Each pickupScheduleID As Integer In approveList
			Dim sql As String = "UPDATE tblPickupSchedule " & _
				"SET ApprovedForExport = 1 " & _
				"WHERE PickupScheduleID = " & pickupScheduleID
			SqlNonQuery(sql)
		Next

		lblTooManySelected.Visible = False
		gridMain.Selection.UnselectAll()
		gridMain.DataBind()
	End Sub

	Protected Sub btnSelectApprovedAndUnexported_Click(sender As Object, e As EventArgs)
		For i As Integer = 0 To gridMain.VisibleRowCount - 1
			Dim approved As Boolean = gridMain.GetRowValues(i, "ApprovedForExport")
			Dim exported As Boolean = gridMain.GetRowValues(i, "Exported")
			If approved And Not exported Then
				gridMain.Selection.SelectRow(i)
			End If
		Next
	End Sub

	Protected Sub btnDeselectAll_Click(sender As Object, e As EventArgs)
		gridMain.Selection.UnselectAll()
		lblTooManySelected.Visible = False
	End Sub

	Protected Sub Timer1_Tick(sender As Object, e As EventArgs)
		' This subroutine is called when the timer expires.
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		' 1. Is there a PickupScheduleID in process?
		Dim pickupScheduleID As Integer = hfExport("PickupScheduleID")

		' 2. If so, check if processing is complete.
		If pickupScheduleID > 0 Then
			Dim complete As Boolean = False
			sql = "SELECT finish_time FROM AsyncExecResults WHERE token = '" & hfExport("token").ToString() & "'"
			Try
				If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
					Return
				End If
				If rsql.Read() Then
					If IsDBNull(rsql("finish_time")) Then
						SqlQueryClose(connSQL, rsql)
						Return ' still in process
					Else
						complete = True
					End If
				Else
					' Not yet started.
				End If
				SqlQueryClose(connSQL, rsql)
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "")
				Return
			End Try

			' 4. If complete, deselect that row and increment counter.
			If complete Then
				For i As Integer = 0 To gridMain.VisibleRowCount - 1
					Dim psid As String = gridMain.GetRowValues(i, "PickupScheduleID")
					If psid = pickupScheduleID Then
						gridMain.Selection.UnselectRow(i)
						hfExport("token") = System.Guid.Empty
						hfExport("PickupScheduleID") = 0
						hfExport("count") += 1
						pickupScheduleID = 0
						lblProgressBar.Text = hfExport("count") & " Pickup Schedule" & IIf(hfExport("count") > 1, "s", "") & " exported."
						Exit For
					End If
				Next
			End If
		End If

		' 5. Is there another PickupScheduleID to be processed?
		For i As Integer = 0 To gridMain.VisibleRowCount - 1
			Dim approved As Boolean = gridMain.GetRowValues(i, "ApprovedForExport")
			Dim exported As Boolean = gridMain.GetRowValues(i, "Exported")
			Dim selected As Boolean = gridMain.Selection.IsRowSelected(i)
			If approved And Not exported And selected Then
				pickupScheduleID = CInt(gridMain.GetRowValues(i, "PickupScheduleID").ToString())
				Exit For
			End If
		Next

		' 6. If not, terminate process.
		If pickupScheduleID = 0 Then
			Timer1.Enabled = False
			hfExport("timerRunning") = False
			btnExportApprovedAndUnexported.Text = "Export Approved and Unexported"
			btnExportApprovedAndUnexported.BackColor = ColorTranslator.FromHtml("#ECEDF0")
			If hfExport("count") = 0 Then
				lblProgressBar.Text = "Please select at least one Pickup Schedule that is Approved and not Exported."
			Else
				lblProgressBar.Text = "Export Complete. " & _
					hfExport("count") & " Pickup Schedule" & IIf(hfExport("count") > 1, "s", "") & " exported."
			End If
			Return
		End If

		' 7. If so, start export process for that PickupScheduleID.
		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		Dim token As System.Guid = System.Guid.Empty
		myCmd.Connection = conn
		myCmd.CommandText = "usp_AsyncExecInvoke"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@procedureName", System.Data.ParameterDirection.Input, System.Data.SqlDbType.NVarChar, "spScheduledDataForExportBuild"))
		myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@p1", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int, pickupScheduleID))
		myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@n1", System.Data.ParameterDirection.Input, System.Data.SqlDbType.NVarChar, "@PickupScheduleID"))
		myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@token", System.Data.ParameterDirection.Output, System.Data.SqlDbType.UniqueIdentifier, token))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			hfExport("token") = myCmd.Parameters("@token").Value
			hfExport("PickupScheduleID") = pickupScheduleID
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "")
		Finally
			conn.Close()
		End Try

	End Sub

	Protected Sub btnExportApprovedAndUnexported_Click(sender As Object, e As EventArgs)
		If Timer1.Enabled = False Then
			Timer1.Enabled = True
			btnExportApprovedAndUnexported.Text = "Stop Export"
			btnExportApprovedAndUnexported.BackColor = Color.Red
		Else
			Timer1.Enabled = False
			btnExportApprovedAndUnexported.Text = "Export Approved and Unexported"
			btnExportApprovedAndUnexported.BackColor = ColorTranslator.FromHtml("#ECEDF0")
		End If
	End Sub
End Class
