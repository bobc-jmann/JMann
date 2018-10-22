Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Data
Imports System.Data.SqlClient

Partial Class TargetsForecasts
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

					End Select
				End If
			Case "GET"
				hf("UserID") = Session("vUserID")
				Region.SelectedIndex = -1
				Year.SelectedIndex = 0
			Case "HEAD"
		End Select
	End Sub

	Protected Sub grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
		If Not CType(sender, ASPxGridView).IsNewRowEditing Then
			If e.Column.FieldName = "Week" Then
				e.Editor.ReadOnly = True
			Else
				e.Editor.ReadOnly = False
			End If
		End If
	End Sub

	Protected Sub grid_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
		If Region.SelectedIndex = -1 Then
			e.Visible = False
		End If
	End Sub
End Class
