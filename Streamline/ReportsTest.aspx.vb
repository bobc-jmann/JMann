Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Web.Services

Partial Class ReportsTest
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Select Case Request.HttpMethod
			Case "POST"
			Case "GET"
			Case "HEAD"
		End Select
	End Sub

	Protected Sub btnScheduleCalendar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScheduleCalendar.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Schedule Calendar Test2")
	End Sub

End Class
