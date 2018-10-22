Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Data
Imports DevExpress.Utils
Imports System.Data.Sql
Imports System.Data.SqlClient

Partial Class UncommittedChanges
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					grdMain.DataBind()
				End If
			Case "GET"
				grdMain.DataBind()
		End Select
	End Sub

	Protected Sub grdMain_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles grdMain.CustomButtonCallback
		Dim routeID As Integer = grdMain.GetRowValues(e.VisibleIndex, "RouteID")
		Dim mode As String = ""

		If e.ButtonID = "cbCreate" Then
			mode = "Create"
		ElseIf e.ButtonID = "cbCommit" Then
			mode = "Commit"
		ElseIf e.ButtonID = "cbCancel" Then
			mode = "Cancel"
		End If

		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spUncommittedChanges"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure
		myCmd.CommandTimeout = 300	' 5 min

		myCmd.Parameters.Add(DataUtil.CreateParameter("@routeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, routeID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@mode", System.Data.ParameterDirection.Input, System.Data.DbType.String, mode))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "UncommittedChanges, grdMain_CustomButtonCallback")
			Else
				grdMain.DataBind()
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "UncommittedChanges, grdMain_CustomButtonCallback")
		Finally
			conn.Close()
		End Try
	End Sub

	Protected Sub grdMain_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		If e.ButtonID = "cbCreate" And UncommittedChanges(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
		If e.ButtonID = "cbCommit" And Not UncommittedChanges(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
		If e.ButtonID = "cbCancel" And Not UncommittedChanges(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
	End Sub

	Private Function UncommittedChanges(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim row As Object = grid.GetRow(visibleIndex)
		Return (CType(row, DataRowView))("UncommittedChanges").ToString()
	End Function

End Class
