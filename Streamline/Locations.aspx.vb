Partial Class Locations
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

	End Sub

	Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
		e.NewValues("Active") = True
		e.NewValues("DefaultLocation") = False
		e.NewValues("OutsideLocation") = False
		e.NewValues("InventoryLocation") = False
	End Sub
End Class
