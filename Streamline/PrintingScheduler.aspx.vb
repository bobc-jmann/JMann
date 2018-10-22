Imports System.Data
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

Partial Class PrintingScheduler
    Inherits System.Web.UI.Page

    Private vDoIt As Boolean
	Private sprm As SqlParameter
	Private cntNotPrinted As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'DCM - added for security
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
                        Case "ckDoNotShowApprovedForExport"
                        Case "ckDoNotShowPrinted"
                        Case Else
                            LoadJobs()
                    End Select
                End If

            Case "GET"
                dtEarliestPickupDate.Value = DateAdd(DateInterval.Month, -2, Now)

                ckDoNotShowPrinted.Checked = False
                LoadJobs()
 
            Case "HEAD"
                'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
        End Select

		If cntNotPrinted > vMaxMailInPrintScheduler Then
			lblTooMany.Visible = True
		Else
			lblTooMany.Visible = False
		End If
    End Sub

    Private Sub LoadJobs()
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

        Dim whereClause As String = ""
        If ckDoNotShowApprovedForExport.Checked Then
            whereClause += "WHERE ApprovedForExport = 0 "
        End If
        If ckDoNotShowPrinted.Checked Then
            If whereClause = "" Then
                whereClause = "WHERE "
            Else
                whereClause += "AND "
            End If
            whereClause += "Printed = 0 "
        End If
        If whereClause = "" Then
            whereClause = "WHERE "
        Else
            whereClause += "AND "
        End If
        whereClause += "ScheduleType IN ('MAIL', 'MAG', 'MAP') "
        whereClause += "AND PickupDate >= '" & Format(dtEarliestPickupDate.Value, "MM/dd/yyyy") & "' "
        hfGridMainSelectCommand.Value = "SELECT * FROM qryScheduler " & whereClause & _
                        "ORDER BY PickupDate, PrintJobCategory, RouteCode DESC"
        dsGridMain.SelectCommand = hfGridMainSelectCommand.Value
        gridMain.DataBind()
    End Sub

    Protected Sub ckDoNotShowApprovedForExport_CheckedChanged(sender As Object, e As EventArgs) Handles ckDoNotShowApprovedForExport.CheckedChanged
        LoadJobs()
    End Sub

    Protected Sub ckDoNotShowPrinted_CheckedChanged(sender As Object, e As EventArgs) Handles ckDoNotShowPrinted.CheckedChanged
        LoadJobs()
    End Sub

    Protected Sub gridMain_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles gridMain.RowValidating
		'Dim sql As String
		'Dim rsql As SqlDataReader = Nothing
		'Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		'Dim vToday As Date = vbDate()
		'Dim exportedCount As Integer
		'Dim x As Integer = gridMain.FocusedRowIndex

		'      sql = "SELECT COUNT(*) FROM tblScheduledPickupSectionsForExport	WHERE PickupScheduleID = " & gridMain.GetRowValues(x, "PickupScheduleID").ToString & " AND Exported = 1"
		'      exportedCount = SQLExecuteScalar(sql, "Notify User")

		'      If exportedCount > 0 And (e.NewValues("ApprovedForExport") <> e.OldValues("ApprovedForExport")) Then
		'          e.RowError = "Cannot change Approved for Export on a job that has been exported."
		'      End If
		'      If exportedCount > 0 And (e.NewValues("MailingDate") <> e.OldValues("MailingDate")) Then
		'          e.RowError = "Cannot change Mailing Date on a job that has been exported and sent to tablets."
		'      End If
		'      If e.OldValues("MailingDate") <> e.NewValues("MailingDate") And Not e.NewValues("MailingDate") > vToday Then
		'          e.RowError = "Mailing Date must be later than today."
		'      End If
		'      If Not e.NewValues("MailingDate") < e.NewValues("PickupDate") Then
		'          e.RowError = "Mailing Date must be earlier than Pickup Date."
		'      End If

		'      If e.NewValues("AllMail") And Not e.NewValues("Printed") Then
		'          e.RowError = "You cannot select 'All Mail' without also selecting 'Printed'."
		'      End If
		'      If e.NewValues("AllPostcard") And Not e.NewValues("Printed") Then
		'          e.RowError = "You cannot select 'All Postcard' without also selecting 'Printed'."
		'      End If

		'      If e.NewValues("AllMail") And e.NewValues("AllPostcard") Then
		'          e.RowError = "A print job cannot be 'All Mail' and 'All Postcard'."
		'      End If


		'If e.NewValues("ApprovedForExport") AndAlso Not e.OldValues("ApprovedForExport") AndAlso cntNotPrinted > vMaxMailInPrintScheduler Then
		'	e.RowError = "Print queue full."
		'End If
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
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim vToday As Date = Today()
        Dim vPickupDate, vMailDate As Date
        e.Result = "OK"
        x = gridMain.FocusedRowIndex
        Dim vals As Object = gridMain.GetRowValues(x, New String() {"PickupScheduleID"})
        s = "select * from qryScheduler where PickupScheduleID=" & vals.ToString
        Session("tempVal") = vals.ToString
		If Not SqlQueryOpen(connSQL, rs, s, "Notify Web User") Then
			Return
		End If
		While rs.Read
			vPickupDate = rs("PickupDate")
			vMailDate = rs("MailingDate")
		End While
        If Not ismt(vMailDate) Then
            If vToday >= vMailDate Then
                e.Result = "Mailing date must be earlier than today."
            End If
        End If
        If Not ismt(vPickupDate) Then
            If vToday >= vPickupDate Then
                e.Result = "Pickup date must be earlier than today."
            End If
        End If
		SqlQueryClose(connSQL, rs)
	End Sub

    Sub vbDeleteAfterCheck(s As Object, e As CallbackEventArgs)
        If Not ismt(Session("tempVal")) Then
            Dim vKey = Session("tempVal")

			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim cmd As SqlCommand = New SqlCommand()
			cmd.Connection = conn
			cmd.CommandText = "spScheduleDelete"
			cmd.CommandType = System.Data.CommandType.StoredProcedure

			cmd.Parameters.Add(DataUtil.CreateParameter("@PickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, vKey))
			Dim errorID As Integer = 0
			cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			Try
				conn.Open()
				cmd.ExecuteNonQuery()
				errorID = cmd.Parameters("@RETURN_VALUE").Value
				If errorID > 0 Then
					vbHandleProgramError(errorID, "PrintingScheduler, vbDeleteAfterCheck")
				End If
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "PrintingSchedule, vbDeleteAfterCheck")
			Finally
				conn.Close()
			End Try

			e.Result = errorID
			Session("tempVal") = Nothing
        End If
    End Sub

    Sub vbReschAfterCheck(s As Object, e As CallbackEventArgs)
        If Not ismt(Session("tempVal")) Then
            Dim vKey = Session("tempVal")

			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim cmd As SqlCommand = New SqlCommand()
			cmd.Connection = conn
			cmd.CommandText = "spScheduleBuild"
			cmd.CommandType = System.Data.CommandType.StoredProcedure

			cmd.Parameters.Add(DataUtil.CreateParameter("@PickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, vKey))
			Dim errorID As Integer = 0
			cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			Try
				conn.Open()
				cmd.ExecuteNonQuery()
				errorID = cmd.Parameters("@RETURN_VALUE").Value
				If errorID > 0 Then
					vbHandleProgramError(errorID, "PrintingScheduler, vbReschAfterCheck")
				End If
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "PrintingScheduler, vbReschAfterCheck")
			Finally
				conn.Close()
			End Try

			e.Result = errorID
            Session("tempVal") = Nothing
        End If
    End Sub


	Protected Sub btnRunExportedUnprintedPrintJobsReport_Click(ByVal sender As Object, ByVal e As EventArgs)
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Exported Unprinted Print Jobs")
	End Sub

	Protected Sub btnMailCountsByMailingDate_Click(ByVal sender As Object, ByVal e As EventArgs)
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Mail Counts By Mailing Date")
	End Sub

	Protected Sub btnScheduleMailCounts_Click(ByVal sender As Object, ByVal e As EventArgs)
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Schedule Mail Counts")
	End Sub
End Class
