Imports DevExpress.Web
Imports DevExpress.Web.Data

Partial Class TabletMaint
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		dsTablets.SelectCommand =
			"SELECT TabletID, TabletName, DriverLocationID, Active " & _
			"FROM tblTablets " & _
			"WHERE DriverLocationID IN " & Session("userRegionsList") & " " & _
			"ORDER BY TabletName"

		dsRegions.SelectCommand =
			"SELECT RegionID, RegionCode " & _
			"FROM tlkRegions " & _
			"WHERE RegionID IN " & Session("userRegionsList") & " " & _
			"ORDER BY RegionCode"
		Dim regionID_column = TryCast(grid.DataColumns("DriverLocationID"), GridViewDataComboBoxColumn)
		regionID_column.PropertiesComboBox.RequireDataBinding()
		dsRegions.DataBind()

		grid.DataBind()
	End Sub

	Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
		e.NewValues("Active") = True
	End Sub
End Class
