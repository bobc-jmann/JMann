Imports System.Data
Imports System.Data.Common
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Drawing
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports DataUtil
Imports DevExpress.Utils
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class DropoffDonations
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Dim PostBackControlID As String = ""
		Select Case Request.HttpMethod
			Case "POST"

			Case "GET"
				dtEarliestDropoffDate.Value = DateAdd(DateInterval.Day, -90, Now)

				Dim sql As String = "SELECT LocationID, LocationAbbr " & _
					"FROM tblLocations " & _
					"WHERE RegionID IN " & Session("userRegionsList") & " " & _
						"AND InventoryLocation = 1 " & _
					"ORDER BY LocationAbbr"
				Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
				Dim dt As DataTable = New DataTable()
				da.Fill(dt)
				ddlLocations.DataSource = dt
				ddlLocations.DataTextField = "LocationAbbr"
				ddlLocations.DataValueField = "LocationID"
				ddlLocations.DataBind()
				ddlLocations.Items.Insert(0, New ListItem("<Select Cart Location", "0"))
				If Session("UserRegionDefault") <> 0 Then
					ddlLocations.SelectedValue = Session("UserRegionDefault")
				End If
			Case "HEAD"
		End Select
	End Sub

	Protected Sub gridMain_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		Select Case e.ButtonType
			Case ColumnCommandButtonType.Delete
				e.Visible = CommandButtonVisibleCriteria(e.VisibleIndex, "Delete")
			Case ColumnCommandButtonType.Edit
				e.Visible = CommandButtonVisibleCriteria(e.VisibleIndex, "Edit")
		End Select
	End Sub

	Private Function CommandButtonVisibleCriteria(ByVal visibleIndex As Integer, ByVal buttonType As String) As Boolean
		Dim approved As Boolean = CBool(gridMain.GetRowValues(visibleIndex, "Approved").ToString())

		If approved Then
			Return False
		End If
		Return True
	End Function

	Protected Sub gridMain_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles gridMain.RowValidating
		If IsNothing(e.NewValues("DonationDate")) Then
			e.RowError = "Please select a Donation Date."
			Return
		End If
		If IsNothing(e.NewValues("CharityID")) OrElse e.NewValues("CharityID").ToString = "" Then
			e.RowError = "Please select a Charity."
			Return
		End If

		If e.IsNewRow Then
			Dim approved As Boolean = IsInventoryApproved(e.NewValues("DonationDate"), ddlLocations.SelectedValue, )
			If approved Then
				e.RowError = "The Ending Inventory for this Location and Date has been approved so no changes can be made."
			End If
		End If
	End Sub
End Class
