Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Data
Imports System.Data.SqlClient

Partial Class DriverBaggerCosts
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		grid.DataBind()

	End Sub

	Protected Sub btnCreateYearMonth_Click(sender As Object, e As EventArgs)
		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim cmd As SqlCommand = New SqlCommand()
		cmd.Connection = conn
		cmd.CommandText = "spDriverBaggerCosts_Insert"
		cmd.CommandType = System.Data.CommandType.StoredProcedure

		cmd.Parameters.Add(DataUtil.CreateParameter("@yearMonth", System.Data.ParameterDirection.Input, DbType.Int32, yearMonth.Value))
		cmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, DbType.Int32, Session("vUserID")))
		Dim errorID As Integer = 0
		cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			cmd.ExecuteNonQuery()
			errorID = cmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "DriverBaggerCosts, btnCreateYearMonth_Click")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try

		grid.DataBind()
	End Sub
End Class
