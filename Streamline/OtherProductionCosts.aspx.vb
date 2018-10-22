Imports DevExpress.Web
Imports DevExpress.Data

Partial Class OtherProductionCosts
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		If dtEffectiveDate.Value < CDate("01/01/2012") Then
			dtEffectiveDate.Value = Today()
		End If

		grid.DataBind()

		If ckShowAllData.Checked Then
			grid.UnGroup(grid.Columns("Category"))
			grid.Settings.ShowGroupPanel = False
			grid.Settings.ShowGroupFooter = GridViewGroupFooterMode.Hidden
		Else
			grid.GroupBy(grid.Columns("Category"))

			grid.Settings.ShowGroupPanel = True
			grid.ExpandAll()
			grid.Settings.ShowGroupFooter = GridViewGroupFooterMode.VisibleIfExpanded
		End If
	End Sub

End Class
