Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Utils
Imports DataUtil

Partial Class MonitorETL
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
	End Sub

	Protected Sub grdSMS_CustomButtonBallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles grdSMS.CustomButtonCallback
		If e.ButtonID = "cbStartJobSMS" Then
			Dim jobName As String = grdSMS.GetRowValues(e.VisibleIndex, "JobName")

			Dim conn As SqlConnection = New SqlConnection(vConnStr)

			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spAgentJobStart"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameter("@jobName", System.Data.ParameterDirection.Input, System.Data.DbType.String, jobName))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@serverName", System.Data.ParameterDirection.Input, System.Data.DbType.String, "[JMANN-SQL]"))
			Dim errorID As Integer = 0
			myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			Try
				conn.Open()
				myCmd.CommandTimeout = 150
				myCmd.ExecuteNonQuery()
				errorID = myCmd.Parameters("@RETURN_VALUE").Value
				If errorID > 0 Then
					ja("Job is already running")
				Else
					grdSMS.DataBind()
				End If
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			Finally
				conn.Close()
			End Try
		End If
	End Sub

    Protected Sub grdSMS_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
        If e.VisibleIndex = -1 Then
            Return
        End If

        If e.ButtonID = "cbStartJobSMS" And Not VisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
            e.Visible = DefaultBoolean.False
        End If
    End Sub

    Protected Sub grdTOS_CustomButtonBallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles grdTOS.CustomButtonCallback
		If e.ButtonID = "cbStartJobTOS" Then
			Dim jobName As String = grdTOS.GetRowValues(e.VisibleIndex, "JobName")

			Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)

			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spAgentJobStart"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameter("@jobName", System.Data.ParameterDirection.Input, System.Data.DbType.String, jobName))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@serverName", System.Data.ParameterDirection.Input, System.Data.DbType.String, "[JMANN-SQL\THRIFTOS]"))
			Dim errorID As Integer = 0
			myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			Try
				conn.Open()
				myCmd.CommandTimeout = 150
				myCmd.ExecuteNonQuery()
				errorID = myCmd.Parameters("@RETURN_VALUE").Value
				If errorID > 0 Then
					ja("Job is already running")
				Else
					grdTOS.DataBind()
				End If
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			Finally
				conn.Close()
			End Try
		End If
	End Sub

	Protected Sub grdTOS_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		If e.ButtonID = "cbStartJobTOS" And Not VisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
	End Sub

	Private Function VisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim val As Object = grid.GetRowValues(visibleIndex, "Running")
		Return val <> "Yes"
	End Function

End Class
