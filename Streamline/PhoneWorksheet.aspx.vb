Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DataUtil

Partial Class PhoneWorksheet
    Inherits System.Web.UI.Page

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
                        'LogProgramError(ex.Message, ex.StackTrace, "Notify User")
                    End Try
                    Select Case PostBackControlID

                    End Select
                End If

            Case "GET"
                LoadDailySheet()
            Case "HEAD"
                'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
        End Select
    End Sub

    Private Sub LoadDailySheet()
		Dim sql As String
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		lblDate.Text = Format(Now, "MM/dd/yyyy")
		lblOperator.Text = Session("vUserName")

		sql = "SELECT RollOverVoiceMail, VoiceMailReturned, WebSpecialsReturned, EmailsProcessed, TotalCallsReceived, CourtesyCallsMade, ReRouteCallsMade, OtherComplaints, " & _
				"(SELECT COUNT(*) FROM tblSpecials WHERE CreatedBy = '" & Session("vUserName") & "' AND CreatedOn >= '" & lblDate.Text & _
				"' AND CreatedOn < '" & Format(DateAdd(DateInterval.Day, 1, CDate(lblDate.Text)), "MM/dd/yyyy") & "') AS Specials, " & _
				"(SELECT COUNT(*) FROM tSysConfirmMissLog WHERE UserName = '" & Session("vUserName") & "' AND Confirmed = 1 AND LogDate >= '" & lblDate.Text & _
				"' AND LogDate < '" & Format(DateAdd(DateInterval.Day, 1, CDate(lblDate.Text)), "MM/dd/yyyy") & "') AS Confirms, " & _
				"(SELECT COUNT(*) FROM tSysConfirmMissLog WHERE UserName = '" & Session("vUserName") & "' AND Missed = 1 AND LogDate >= '" & lblDate.Text & _
				"' AND LogDate < '" & Format(DateAdd(DateInterval.Day, 1, CDate(lblDate.Text)), "MM/dd/yyyy") & "') AS Misses, " & _
				"Comments " & _
			"FROM tblPhoneSheetsForSpecials " & _
			"WHERE Date = '" & lblDate.Text & "' " & _
				"AND UserID = " & Session("vUserID")

		If Not SqlQueryOpen(connSQL, rsql, sql, "Notify Web User") Then
			Return
		End If

		If rsql.Read() Then
			txtROVM.Text = rsql("RollOverVoiceMail")
			txtVMR.Text = rsql("VoiceMailReturned")
			txtWSR.Text = rsql("WebSpecialsReturned")
			txtEP.Text = rsql("EmailsProcessed")
			txtTCR.Text = rsql("TotalCallsReceived")
			txtCCM.Text = rsql("CourtesyCallsMade")
			txtRRCM.Text = rsql("ReRouteCallsMade")
			txtOC.Text = rsql("OtherComplaints")
			txtSpecials.Text = rsql("Specials")
			txtConfirms.Text = rsql("Confirms")
			txtMisses.Text = rsql("Misses")
			txtComments.Text = rsql("Comments")
		Else
			txtROVM.Text = "0"
			txtVMR.Text = "0"
			txtWSR.Text = "0"
			txtEP.Text = "0"
			txtTCR.Text = "0"
			txtCCM.Text = "0"
			txtRRCM.Text = "0"
			txtOC.Text = "0"
			txtSpecials.Text = "0"
			txtConfirms.Text = "0"
			txtMisses.Text = "0"
			txtComments.Text = ""
		End If

		SqlQueryClose(connSQL, rsql)
	End Sub

    Private Sub SaveDailySheet()
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim myCmd As SqlCommand = New SqlCommand()
        myCmd.Connection = conn
        myCmd.CommandText = "spPhoneSheetsForSpecialsSave"
        myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@Date", System.Data.ParameterDirection.Input, System.Data.DbType.Date, CDate(lblDate.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@UserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RollOverVoiceMail", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(txtROVM.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@VoiceMailReturned", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(txtVMR.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@WebSpecialsReturned", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(txtWSR.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@EmailsProcessed", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(txtEP.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@TotalCallsReceived", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(txtTCR.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@CourtesyCallsMade", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(txtCCM.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@ReRouteCallsMade", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(txtRRCM.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@OtherComplaints", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(txtOC.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@Comments", System.Data.ParameterDirection.Input, System.Data.DbType.String, Replace(txtComments.Text, "'", "''")))

		myCmd.Parameters.Add(DataUtil.CreateParameter("@return_value", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@error_id", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))

        Try
            conn.Open()
            myCmd.ExecuteNonQuery()
            Dim error_id As Integer = myCmd.Parameters("@error_id").Value
            If error_id > 0 Then
				vbHandleProgramError(error_id, "PhoneWorksheet, SaveDailySheet")
            End If
        Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "PhoneWorksheet, SaveDailySheet")
        Finally
            conn.Close()
        End Try

    End Sub

    Protected Sub btnROVM_Click(sender As Object, e As EventArgs) Handles btnROVM.Click
        txtROVM.Text = CInt(txtROVM.Text) + 1
        SaveDailySheet()
    End Sub

    Protected Sub txtROVM_TextChanged(sender As Object, e As EventArgs) Handles txtROVM.TextChanged
        SaveDailySheet()
    End Sub

    Protected Sub btnVMR_Click(sender As Object, e As EventArgs) Handles btnVMR.Click
        txtVMR.Text = CInt(txtVMR.Text) + 1
        SaveDailySheet()
    End Sub

    Protected Sub txtVMR_TextChanged(sender As Object, e As EventArgs) Handles txtVMR.TextChanged
        SaveDailySheet()
    End Sub

	Protected Sub btnWSR_Click(sender As Object, e As EventArgs) Handles btnWSR.Click
		txtWSR.Text = CInt(txtWSR.Text) + 1
		SaveDailySheet()
	End Sub

	Protected Sub txtEP_TextChanged(sender As Object, e As EventArgs) Handles txtEP.TextChanged
		SaveDailySheet()
	End Sub

	Protected Sub btnEP_Click(sender As Object, e As EventArgs) Handles btnEP.Click
		txtEP.Text = CInt(txtEP.Text) + 1
		SaveDailySheet()
	End Sub

	Protected Sub txtWSR_TextChanged(sender As Object, e As EventArgs) Handles txtWSR.TextChanged
		SaveDailySheet()
	End Sub

    Protected Sub btnTCR_Click(sender As Object, e As EventArgs) Handles btnTCR.Click
        txtTCR.Text = CInt(txtTCR.Text) + 1
        SaveDailySheet()
    End Sub

    Protected Sub txtTCR_TextChanged(sender As Object, e As EventArgs) Handles txtTCR.TextChanged
        SaveDailySheet()
    End Sub

    Protected Sub btnCCM_Click(sender As Object, e As EventArgs) Handles btnCCM.Click
        txtCCM.Text = CInt(txtCCM.Text) + 1
        SaveDailySheet()
    End Sub

    Protected Sub txtCCM_TextChanged(sender As Object, e As EventArgs) Handles txtCCM.TextChanged
        SaveDailySheet()
    End Sub

    Protected Sub btnRRCM_Click(sender As Object, e As EventArgs) Handles btnRRCM.Click
        txtRRCM.Text = CInt(txtRRCM.Text) + 1
        SaveDailySheet()
    End Sub

    Protected Sub txtRRCM_TextChanged(sender As Object, e As EventArgs) Handles txtRRCM.TextChanged
        SaveDailySheet()
    End Sub

    Protected Sub btnOC_Click(sender As Object, e As EventArgs) Handles btnOC.Click
        txtOC.Text = CInt(txtOC.Text) + 1
        SaveDailySheet()
    End Sub

    Protected Sub txtOC_TextChanged(sender As Object, e As EventArgs) Handles txtOC.TextChanged
        SaveDailySheet()
    End Sub

    Protected Sub txtComments_TextChanged(sender As Object, e As EventArgs) Handles txtComments.TextChanged
        SaveDailySheet()
    End Sub

End Class
