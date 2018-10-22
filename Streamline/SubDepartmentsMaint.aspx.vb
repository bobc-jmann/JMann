Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Data
Imports System.Data.SqlClient

Partial Class SubDepartmentsMaint
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

	End Sub

	Protected Sub grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
		If e.Column.FieldName = "ReportGroupID" Then
			Dim combo = CType(e.Editor, ASPxComboBox)
			AddHandler combo.Callback, AddressOf combo_Callback

			Dim grid = e.Column.Grid
			If (Not combo.IsCallback) Then
				Dim departmentID As Integer = -1
				If (Not grid.IsNewRowEditing) Then
					departmentID = CInt(Fix(grid.GetRowValues(e.VisibleIndex, "DepartmentID")))
				End If
				FillReportGroupComboBox(combo, departmentID)
			End If
		End If
	End Sub

	Private Sub combo_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
		Dim departmentID As Integer = -1
		Int32.TryParse(e.Parameter, departmentID)
		FillReportGroupComboBox(TryCast(sender, ASPxComboBox), departmentID)
	End Sub

	Protected Sub FillReportGroupComboBox(ByVal combo As ASPxComboBox, ByVal departmentID As Integer)
		combo.DataSourceID = "dsReportGroupsEditor"
		dsReportGroupsEditor.SelectParameters("DepartmentID").DefaultValue = departmentID.ToString()
		dsReportGroupsEditor.DataBind()
		combo.DataBindItems()

		combo.Items.Insert(0, New ListEditItem("", Nothing)) ' Null Item
	End Sub
End Class
