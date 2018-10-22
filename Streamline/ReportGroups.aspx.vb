Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Data
Imports System.Data.SqlClient

Partial Class ReportGroups
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

	End Sub

	Protected Sub grdMain_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
	End Sub

	Protected Sub tbStoreID_SelectedIndexChanged(sender As Object, e As EventArgs)
		grdMain.DataBind()
	End Sub

End Class
