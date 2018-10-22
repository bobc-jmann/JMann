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

Partial Class RouteMerge
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Select Case Request.HttpMethod
			Case "POST"

			Case "GET"
				LoadRoutes()
		End Select
	End Sub

	Private Sub LoadRoutes()
		Dim sql As String = "SELECT R.RouteID, R.RouteCode " & _
			"FROM tblRoutes AS R " & _
			"ORDER BY R.RouteCode"
		Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)

		ddlRoutesPrimary.Items.Clear()
		ddlRoutesPrimary.DataSource = dt
		ddlRoutesPrimary.DataTextField = "RouteCode"
		ddlRoutesPrimary.DataValueField = "RouteID"
		ddlRoutesPrimary.DataBind()
		ddlRoutesPrimary.Items.Insert(0, New ListItem("<Select Route>", "0"))

		ddlRoutesMerge.Items.Clear()
		ddlRoutesMerge.DataSource = dt
		ddlRoutesMerge.DataTextField = "RouteCode"
		ddlRoutesMerge.DataValueField = "RouteID"
		ddlRoutesMerge.DataBind()
		ddlRoutesMerge.Items.Insert(0, New ListItem("<Select Route>", "0"))
	End Sub

	Protected Sub btnMerge_Click(sender As Object, e As EventArgs) Handles btnMerge.Click
		If ddlRoutesPrimary.SelectedValue = 0 Then
			ja("Please select a Primary Route.")
			Return
		End If

		If ddlRoutesMerge.SelectedValue = 0 Then
			ja("Please select a Route to be Merged and Deleted.")
			Return
		End If

		If ddlRoutesPrimary.SelectedValue = ddlRoutesMerge.SelectedValue Then
			ja("The Primary Route and the Route to be Merged and Deleted cannot be the same Route.")
			Return
		End If

		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spRoutesDeleteDuplicate"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@primaryRouteID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, ddlRoutesPrimary.SelectedValue))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@toBeDeletedRouteID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, ddlRoutesMerge.SelectedValue))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "RouteMerge, btnMerge_Click")
			Else
				ja("The routes merged successfully!")
			End If

		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "RouteMerge, btnMerge_Click")
		Finally
			conn.Close()
		End Try

		LoadRoutes()
	End Sub

End Class
