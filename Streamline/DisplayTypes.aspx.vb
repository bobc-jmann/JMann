Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Data
Imports System.Data.SqlClient

Partial Class DisplayTypes
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

	End Sub

End Class
