Imports DevExpress.Web
Imports DevExpress.Web.Data

Partial Class PhoneMaint
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		dsPhones.SelectCommand =
			"SELECT PhoneID, PhoneNumber, TextingDomain, DirectConnectNumber, DriverLocationID, Active " & _
			"FROM tblPhones " & _
			"WHERE DriverLocationID IN " & Session("userRegionsList") & " " & _
			"ORDER BY PhoneNumber"

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
