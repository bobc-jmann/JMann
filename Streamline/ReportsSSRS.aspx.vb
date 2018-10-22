Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Web.Services

Partial Class ReportsSSRS
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Client_NewWindow("http://jmann-sql/Reports")
	End Sub

	Protected Sub btnSSRS_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSSRS.Click
		Client_NewWindow("http://jmann-sql/Reports")
	End Sub

End Class
