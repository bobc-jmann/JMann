Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.Internal
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Data.Common
Imports DataUtil

Partial Class NMUpdatePrices
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
	End Sub

	Protected Sub btnUpdatePrices_Click(sender As Object, e As EventArgs) Handles btnUpdatePrices.Click
		errorMessageLabel.Text = ""
		errorMessageLabel.Visible = False

		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim cmd As SqlCommand = New SqlCommand()
		cmd.Connection = conn
		cmd.CommandText = "[NewMerchandise].[NewMerch].[spStockKeepingUnits_Update]"
		cmd.CommandType = System.Data.CommandType.StoredProcedure
		cmd.CommandTimeout = 60

		Dim errorID As Integer = 0
		cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			cmd.ExecuteNonQuery()
			errorID = cmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "NMUpdatePrices, btnUpdatePrices_Click")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			errorMessageLabel.Text = "Error creating StockKeepingUnits"
			errorMessageLabel.Visible = True
			conn.Close()
			Return
		Finally
			conn.Close()
		End Try

		cmd.Parameters.Clear()
		cmd.CommandText = "[ThriftOS].[Data].[uspStockKeepingUnitsUpdate]"
		cmd.CommandType = System.Data.CommandType.StoredProcedure
		cmd.CommandTimeout = 600

		cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			cmd.ExecuteNonQuery()
			errorID = cmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "NMUpdatePrices, btnUpdatePrices_Click2")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "NMUpdatePrices, btnUpdatePrices_Click")
			errorMessageLabel.Text = "Error deploying StockKeepingUnits"
			errorMessageLabel.Visible = True
			conn.Close()
			Return
		Finally
			conn.Close()
		End Try

		ja("Update Complete!")
	End Sub
End Class
