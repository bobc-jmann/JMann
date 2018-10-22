Imports DevExpress.Web
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DevExpress.Utils

Partial Class ZipGroupsMaint
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

	Protected Sub grdMain_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
	End Sub

	Protected Sub grdMain_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grdMain.InitNewRow
		e.NewValues("ServiceMonday") = False
		e.NewValues("ServiceTuesday") = False
		e.NewValues("ServiceWednesday") = False
		e.NewValues("ServiceThursday") = False
		e.NewValues("ServiceFriday") = False
	End Sub

	Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		Select Case e.ButtonType
			Case ColumnCommandButtonType.Edit
				e.Visible = MainButtonVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex)
			Case ColumnCommandButtonType.Delete
				e.Visible = MainButtonVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex)
		End Select
	End Sub

	Private Function MainButtonVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim row As Object = grid.GetRow(visibleIndex)
		Return CInt((CType(row, DataRowView))("ZipGroupID").ToString()) > 2
	End Function

	Protected Sub grdDetail_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		If e.ButtonID = "cbMoveToDaily" And Not DailyVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
		If e.ButtonID = "cbMoveToInactive" And Not InactiveVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
	End Sub

	Private Function DailyVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim row As Object = grid.GetRow(visibleIndex)
		Return CInt((CType(row, DataRowView))("ZipGroupID").ToString()) <> 1
	End Function

	Private Function InactiveVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim row As Object = grid.GetRow(visibleIndex)
		Return CInt((CType(row, DataRowView))("ZipGroupID").ToString()) <> 2
	End Function

	Protected Sub grdDetail_Load(sender As Object, e As EventArgs)
		hfDetailGrid("instance") = sender
	End Sub

	Protected Sub grdDetail_CustomButtonCallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs)
		Dim dg As ASPxGridView = hfDetailGrid("instance")
		Dim zipGroupDetailID As Integer = dg.GetRowValues(e.VisibleIndex, "ZipGroupDetailID")

		Dim moveTo As Integer = 0
		If e.ButtonID = "cbMoveToDaily" Then
			moveTo = 1
		End If
		If e.ButtonID = "cbMoveToInactive" Then
			moveTo = 2
		End If

		If moveTo > 0 Then
			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spZipGroupDetail_Move"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameter("@zipGroupDetailID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, zipGroupDetailID))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@moveTo", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, moveTo))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
			Dim errorID As Integer = 0
			myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			Try
				conn.Open()
				myCmd.ExecuteNonQuery()
				errorID = myCmd.Parameters("@RETURN_VALUE").Value
				If errorID > 0 Then
					vbHandleProgramError(errorID, "ZipGroupsMaint, grdDetail_CustomButtonCallback")
				End If
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "", "ZipGroupsMaint, grdDetail_CustomButtonCallback")
			Finally
				conn.Close()
			End Try

		End If

		dg.DataBind()
	End Sub
End Class
