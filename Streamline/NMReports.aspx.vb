Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Web.Services

Partial Class NMReports
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Select Case Request.HttpMethod
			Case "POST"
			Case "GET"
			Case "HEAD"
		End Select
	End Sub

	Protected Sub btnInventoryItems_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInventoryItems.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Inventory Items List")
	End Sub

	Protected Sub btnInventorySalesByStoreByVendor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInventorySalesByStoreByVendor.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Inventory Sales by Store by Vendor")
	End Sub

	Protected Sub btnInventorySalesByItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInventorySalesByItem.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Inventory Sales by Item")
	End Sub

	Protected Sub btnNonInventorySales_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNonInventorySales.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Non-Inventory Sales")
	End Sub

	Protected Sub btnVendorAnalysis_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVendorAnalysis.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Vendor Analysis")
	End Sub

	Protected Sub btnReorderReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReorderReport.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Reorder Report")
	End Sub

	Protected Sub btnTransfersReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransfersReport.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Transfers Report")
	End Sub

	Protected Sub btnStolenDamagedReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStolenDamaged.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Stolen or Damaged Report")
	End Sub

End Class
