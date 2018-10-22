Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DataUtil

Partial Class MailStatusesMaint
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		dsMailStatuses.SelectCommand =
			"SELECT MS.StatusID, MS.[Status], MS.Active, MS.NonRouteOK " & _
			"FROM tlkStatuses AS MS "

		If Not ckShowInactiveMailStatuses.Checked Then
			dsMailStatuses.SelectCommand += "WHERE MS.Active = 1 "
		End If

		dsMailStatuses.SelectCommand += "ORDER BY MS.[Status]"

		grid.DataBind()
	End Sub

	Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
		e.NewValues("Active") = True
		e.NewValues("NonRouteOK") = False
	End Sub

	Protected Sub gridAddresses_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grid.RowValidating
		If e.OldValues("Status") = "MAILABLE" Then
			If e.OldValues("Status") = "MAILABLE" And e.NewValues("Status") <> "MAILABLE" Then
				e.RowError = "The Status MAILABLE cannot be changed"
			End If
			If Not e.NewValues("Active") Then
				e.RowError = "MAILABLE must always be Active"
			End If
			If Not e.NewValues("NonRouteOK") Then
				e.RowError = "MAILABLE is always OK for Non-Route"
			End If
		End If
	End Sub
End Class
