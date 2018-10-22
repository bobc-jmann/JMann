Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Data
Imports System.Data.SqlClient

Partial Class CartsWorked
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					Dim PostBackControlID As String = ""
					Try
						PostBackControlID = GetPostBackControl(Me.Page).ID
					Catch ex As Exception

					End Try
					Select Case PostBackControlID
						Case "dtOldestDateWorked"
							hfOldestDateWorked.Value = dtOldestDateWorked.Value
							Store.DataBind()
					End Select
				End If
			Case "GET"
				hf("UserID") = Session("vUserID")
				dtOldestDateWorked.Value = DateAdd(DateInterval.Month, -2, Now)
				hfOldestDateWorked.Value = dtOldestDateWorked.Value
				Store.SelectedIndex = -1
			Case "HEAD"
		End Select
	End Sub

	Protected Sub grd_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grid.RowValidating
		Dim financeLocationID As Integer = Store.Value
		Dim dateWorked As Date = e.NewValues("DateWorked")

		If IsNothing(e.NewValues("DateWorked")) Or IsDBNull(e.NewValues("DateWorked")) Then
			e.RowError = "Please enter a Date Worked."
			Return
		End If
		If IsNothing(e.NewValues("CartsWorkedSoft")) Or IsDBNull(e.NewValues("CartsWorkedSoft")) Then
			e.RowError = "Please enter a value for Soft Carts."
			Return
		End If
		If IsNothing(e.NewValues("CartsWorkedHard")) Or IsDBNull(e.NewValues("CartsWorkedHard")) Then
			e.RowError = "Please enter a value for Hard Carts."
			Return
		End If

		' If the Inventory for this date or any future date has been approved, changes are not allowed.
		Dim approved As Boolean = IsInventoryApproved(dateWorked, 0, "", financeLocationID)
		Try
			If approved Then
				If e.OldValues("CartsWorkedSoft") <> e.NewValues("CartsWorkedSoft") Then
					e.RowError = "The Ending Inventory for this Location and Pickup Date has been approved so Soft Carts cannot be changed."
				End If
				If e.OldValues("CartsWorkedHard") <> e.NewValues("CartsWorkedHard") Then
					e.RowError = "The Ending Inventory for this Location and Pickup Date has been approved so Hard Carts cannot be changed."
				End If
			End If
		Catch
		End Try
	End Sub

	Protected Sub grid_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
		If Store.SelectedIndex = -1 Then
			e.Visible = False
		End If
	End Sub
End Class
