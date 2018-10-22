Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.Internal
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Data.Common
Imports DataUtil

Partial Class NMPhysicalInventory
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
						Case "tbStoreID"
							grid.DataBind()
						Case "ckShowInactiveInventoryItems"
							hfShowInactiveInventoryItems.Value = ckShowInactiveInventoryItems.Checked
							navigateComboBox.DataBind()
					End Select
				End If
			Case "GET"
				hf("InsertMode") = False
				hf("UserID") = Session("vUserID")
				hfStoreID.Value = 0
				hfItemNumber.Value = ""
				hfShowInactiveInventoryItems.Value = False
				navigateVendorBox.DataBind()
				navigateVendorBox.SelectedIndex = 0
				hfVendorID.Value = 0
				dsInventoryItems_SelectList.SelectCommand = "SELECT TOP (100) ItemNumber AS Item, ItemNumber, ItemDescription " & _
					"FROM [NewMerch].InventoryItems " & _
					"WHERE Active = 1 " & _
					"ORDER BY Item"
				navigateComboBox.DataSource = dsInventoryItems_SelectList
				navigateComboBox.DataBind()
				navigateComboBox.SelectedIndex = -1
			Case "HEAD"
		End Select
	End Sub

	Protected Sub navigateComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		' When using multiple columns in a combo box, extra events are generated when they shouldn't be
		If navigateComboBox.Text = hfItemNumber.Value Then
			Return
		End If

		hfItemNumber.Value = navigateComboBox.Value
		ClearForm()
		grid.DataBind()
	End Sub

	Protected Sub navigateVendorBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		hfVendorID.Value = navigateVendorBox.Value.ToString()
		dsInventoryItems_SelectList.DataBind()

		If navigateComboBox.SelectedIndex > 0 Then
			hfItemNumber.Value = navigateComboBox.Value
			ClearForm()
			grid.DataBind()
		End If
	End Sub

	Private Sub ClearForm()
		tbStoreID.Value = CInt(hfStoreID.Value)
		tbStoreID.DataBind()
		tbInventoryDate.Value = Now
		tbQtyActual.Text = ""
		tbNotes.Text = ""
		errorMessageLabel.Visible = False
		grid.DataBind()
	End Sub

	Protected Sub updateButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		errorMessageLabel.Visible = False

		If navigateComboBox.Text = "" Then
			errorMessageLabel.Text = "Please select an Item"
			errorMessageLabel.Visible = True
			Return
		End If
		If tbStoreID.SelectedIndex <= 0 Then
			errorMessageLabel.Text = "Please select a Store"
			errorMessageLabel.Visible = True
			Return
		End If
		If tbInventoryDate.Value < Now.AddDays(-7) Then
			errorMessageLabel.Text = "Please select a Date within the last week"
			errorMessageLabel.Visible = True
			Return
		End If
		If tbQtyActual.Text = "" Then
			errorMessageLabel.Text = "Please enter an Actual Quantity"
			errorMessageLabel.Visible = True
			Return
		End If

		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim cmd As SqlCommand = New SqlCommand()
		cmd.Connection = conn
		cmd.CommandText = "NewMerch.spPhysicalInventory_Insert"
		cmd.CommandType = System.Data.CommandType.StoredProcedure

		cmd.Parameters.Add(DataUtil.CreateParameter("@storeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, tbStoreID.Value))
		cmd.Parameters.Add(DataUtil.CreateParameter("@itemNumber", System.Data.ParameterDirection.Input, System.Data.DbType.String, navigateComboBox.Text))
		cmd.Parameters.Add(DataUtil.CreateParameter("@inventoryDate", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, tbInventoryDate.Value))
		cmd.Parameters.Add(DataUtil.CreateParameter("@qtyActual", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, tbQtyActual.Text))
		cmd.Parameters.Add(DataUtil.CreateParameter("@notes", System.Data.ParameterDirection.Input, System.Data.DbType.String, tbNotes.Text))
		cmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, Session("vUserID")))
		Dim errorID As Integer = 0
		cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			cmd.ExecuteNonQuery()
			errorID = cmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "NMPhysicalInventory, updateButton_Click")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "NMPhysicalInventory, updateButton_Click")
		Finally
			conn.Close()
		End Try

		ClearForm()
		tbSearchByUPC.Text = ""
	End Sub

	Protected Sub grid_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		Dim buttonVisibility As Boolean = IIf(grid.GetRowValues(e.VisibleIndex, "Posted"), False, True)

		Select Case e.ButtonType
			Case ColumnCommandButtonType.Delete
				e.Visible = buttonVisibility
			Case ColumnCommandButtonType.Edit
				e.Visible = buttonVisibility
		End Select
	End Sub

	Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		If CDate(e.NewValues("InventoryDate")) < Now.AddDays(-7) Then
			e.RowError = "Please select a Date within the last week"
			Return
		End If
		If IsNothing(e.NewValues("QtyActual")) Then
			e.RowError = "Please enter an Actual Quantity"
			Return
		End If
	End Sub

	Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		Dim physicalInventoryID As Integer = e.Values("PhysicalInventoryID")
		dsPhysicalInventory.DeleteParameters.Clear()
		dsPhysicalInventory.DeleteParameters.Add("PhysicalInventoryID", physicalInventoryID)
		dsPhysicalInventory.DeleteParameters.Add("CurrentUserID", Session("vUserID"))
		dsPhysicalInventory.Delete()
		dsPhysicalInventory.DataBind()
		e.Cancel = True
	End Sub

	Class ItemVendor
		Public ItemNumber As String
		Public VendorID As Integer
	End Class

	<WebMethod()> _
	Public Shared Function SearchByUPC(ByVal upc As String) As ItemVendor
		Dim iv As ItemVendor = New ItemVendor
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)

		sql = "SELECT ItemNumber, VendorID " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE UPC = '" & upc & "'"

		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			iv.ItemNumber = rsql("ItemNumber")
			iv.VendorID = rsql("VendorID")
		End If
		SqlQueryClose(conn, rsql)

		Return iv
	End Function

	Protected Sub navigateComboBox_ItemsRequestedByFilterCondition(source As Object, e As ListEditItemsRequestedByFilterConditionEventArgs)
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		dsInventoryItems_SelectList.SelectCommand = "SELECT ItemNumber AS Item, ItemNumber, ItemDescription " & _
			"FROM (SELECT ItemNumber AS Item, ItemNumber, ItemDescription, VendorID, Active, " & _
			"ROW_NUMBER() OVER(ORDER BY T.ItemNumber) AS [rn] FROM NewMerch.InventoryItems AS T " & _
				"WHERE ((ItemNumber) LIKE @filter) " & _
					"AND (@vendorID = 0 OR VendorID = @vendorID) " & _
					"AND (@showInactive = 1 OR Active = 1)) AS ST " & _
						"WHERE ST.[rn] BETWEEN @startIndex and @endIndex "

		Dim endIndex = e.EndIndex + 1
		If e.Filter = "" Then
			endIndex = e.BeginIndex + 100
		End If

		dsInventoryItems_SelectList.SelectParameters.Clear()
		dsInventoryItems_SelectList.SelectParameters.Add("filter", TypeCode.String, String.Format("{0}%", e.Filter))
		dsInventoryItems_SelectList.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString())
		dsInventoryItems_SelectList.SelectParameters.Add("endIndex", TypeCode.Int64, endIndex.ToString())
		dsInventoryItems_SelectList.SelectParameters.Add("VendorID", TypeCode.Int32, hfVendorID.Value.ToString())
		dsInventoryItems_SelectList.SelectParameters.Add("ShowInactive", TypeCode.Int32, IIf(hfShowInactiveInventoryItems.Value, 1, 0))
		comboBox.DataSource = dsInventoryItems_SelectList
		comboBox.DataBind()
	End Sub

	Protected Sub navigateComboBox_ItemRequestedByValue(source As Object, e As ListEditItemRequestedByValueEventArgs)
		Dim value As Long = 0
		If e.Value Is Nothing OrElse (Not Int64.TryParse(e.Value.ToString(), value)) Then
			Return
		End If
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		dsInventoryItems_SelectList.SelectCommand = "SELECT ItemNumber AS Item, ItemNumber, ItemDescription " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE ItemNumber = @ItemNumber " & _
			"ORDER BY ItemNumber"

		dsInventoryItems_SelectList.SelectParameters.Clear()
		dsInventoryItems_SelectList.SelectParameters.Add("ItemNumber", TypeCode.String, e.Value.ToString())
		comboBox.DataSource = dsInventoryItems_SelectList
		comboBox.DataBind()
	End Sub

End Class
