Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.Internal
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.Services
Imports DataUtil

Partial Class NMPurchaseOrders
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
						Case "ckShowCompletedPurchaseOrders"
							hfShowCompletedPurchaseOrders.Value = ckShowCompletedPurchaseOrders.Checked
							navigateComboBox.DataBind()
						Case "dtOldestPurchaseOrderDate"
							hfOldestPurchaseOrderDate.Value = dtOldestPurchaseOrderDate.Value
							navigateComboBox.DataBind()
					End Select
				End If
			Case "GET"
				hf("InsertMode") = False
				hf("UserID") = Session("vUserID")
				hfPurchaseOrderNum.Value = ""
				dtOldestPurchaseOrderDate.Value = DateAdd(DateInterval.Month, -2, Now)
				hfOldestPurchaseOrderDate.Value = dtOldestPurchaseOrderDate.Value
				hfShowCompletedPurchaseOrders.Value = False
				navigateComboBox.SelectedIndex = -1
				ClearForm()
				grid.Visible = False
			Case "HEAD"
		End Select
	End Sub

	Protected Sub navigateComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		PopulateForm()
	End Sub

	Protected Sub newButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		ClearForm()
		hf("InsertMode") = True
		flPO.FindItemOrGroupByName("Controls").Visible = False
		newButton.Visible = False
		updateButton.Visible = True
		cancelButton.Visible = True
		deleteButton.Visible = False
		grid.Visible = False

		tbVendorID.SelectedIndex = 0
		tbStoreID.SelectedIndex = 0

		tbPurchaseOrderNum.Text = ""

		dsPurchaseOrderHeaders.SelectParameters("PurchaseOrderNum").DefaultValue = ""
	End Sub

	Protected Sub updateButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		If Trim(tbOrderedBy.Text) = "" Then
			errorMessageLabel.Text = "Please enter an Ordered By"
			errorMessageLabel.Visible = True
			Return
		End If
		If Trim(tbVendorID.SelectedIndex) = 0 Then
			errorMessageLabel.Text = "Please select a Vendor"
			errorMessageLabel.Visible = True
			Return
		End If
		If Trim(tbStoreID.SelectedIndex) = 0 Then
			errorMessageLabel.Text = "Please select a Ship To"
			errorMessageLabel.Visible = True
			Return
		End If
		Try
			If hf("InsertMode") Then
				dsPurchaseOrderHeaders.Insert()

			Else
				dsPurchaseOrderHeaders.UpdateParameters("PurchaseOrderNum").DefaultValue = hfPurchaseOrderNum.Value
				dsPurchaseOrderHeaders.Update()
			End If
		Catch ex As Exception
			errorMessageLabel.Text = ex.Message
			errorMessageLabel.Visible = True
		End Try

		hf("InsertMode") = False
		flPO.FindItemOrGroupByName("Controls").Visible = True
		navigateComboBox.DataBind()
		navigateComboBox.Visible = True
		grid.Visible = True

		newButton.Visible = True
		If tbStatus.Text = "Open" Then
			deleteButton.Visible = True
		Else
			deleteButton.Visible = False
		End If

		PopulateForm()
		navigateComboBox.SelectedIndex = navigateComboBox.Items.IndexOfText(CInt(hfPurchaseOrderNum.Value))
		grid.DataBind()
	End Sub

	Sub On_Inserted(ByVal sender As Object, ByVal e As SqlDataSourceStatusEventArgs)
		Dim command As DbCommand
		command = e.Command

		hfPurchaseOrderNum.Value = command.Parameters("@PurchaseOrderNum").Value.ToString()
		tbPurchaseOrderNum.Text = hfPurchaseOrderNum.Value
	End Sub

	Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		hf("InsertMode") = False
		flPO.FindItemOrGroupByName("Controls").Visible = True
		navigateComboBox.DataBind()
		navigateComboBox.SelectedIndex = navigateComboBox.Items.IndexOfValue(hfPurchaseOrderNum.Value)
		PopulateForm()
	End Sub

	Protected Sub deleteButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		If Not hf("delete") Then
			Return
		End If
		dsPurchaseOrderHeaders.DeleteParameters("PurchaseOrderNum").DefaultValue = hfPurchaseOrderNum.Value
		dsPurchaseOrderHeaders.Delete()

		dsPurchaseOrderHeaders.SelectParameters("PurchaseOrderNum").DefaultValue = hfPurchaseOrderNum.Value
		Dim item As DevExpress.Web.ListEditItem = navigateComboBox.Items(navigateComboBox.SelectedIndex)
		navigateComboBox.Items.Remove(item)
		navigateComboBox.SelectedIndex = -1

		hfPurchaseOrderNum.Value = ""
		flPO.DataBind()
		grid.DataBind()
		tbVendorID.SelectedIndex = 0
		tbStoreID.SelectedIndex = 0
		tbPurchaseOrderNum.Text = ""
		ClearForm()

		deleteButton.Visible = False
		updateButton.Visible = False
		cancelButton.Visible = False
	End Sub

	Private Sub PopulateForm()
		grid.Visible = True
		ClearForm()
		If Not IsNothing(navigateComboBox.Value) Then
			dsPurchaseOrderHeaders.SelectParameters("PurchaseOrderNum").DefaultValue = navigateComboBox.Value.ToString()
			flPO.DataBind()
			hfPurchaseOrderNum.Value = navigateComboBox.Value.ToString()
		End If

		newButton.Visible = True
		flPO.DataBind()

		If grid.VisibleRowCount > 0 Then
			tbVendorID.ReadOnly = True
			tbVendorID.BackColor = Drawing.Color.WhiteSmoke
		End If

		If tbStatus.Text = "Open" Then
			deleteButton.Visible = True
			updateButton.Visible = True
			cancelButton.Visible = True
		Else
			deleteButton.Visible = False
			updateButton.Visible = False
			cancelButton.Visible = False
			tbStoreID.ReadOnly = True
			tbStoreID.BackColor = Drawing.Color.WhiteSmoke
		End If
	End Sub

	Private Sub ClearForm()
		tbPurchaseOrderNum.Text = ""
		tbStatus.Text = ""
		tbVendorID.ReadOnly = False
		tbVendorID.BackColor = Drawing.Color.White
		tbStoreID.ReadOnly = False
		tbStoreID.BackColor = Drawing.Color.White
		tbPurchaseOrderDate.Value = Today
		tbVendorOrderNum.Text = ""
		tbOrderedBy.Text = ""
		tbShipVia.Text = ""
		tbFOB.Text = ""
		tbShippingTerms.Text = ""
		tbSubtotal.Text = ""
		tbTaxRate.Text = ""
		tbTax.Text = ""
		tbShippingHandling.Text = ""
		tbOther.Text = ""
		tbTotal.Text = ""
		tbNotes.Text = ""
		errorMessageLabel.Visible = False
	End Sub

	Protected Sub grid_DataBound(sender As Object, e As EventArgs)
		Dim buttonVisibility As Boolean = False
		If tbStatus.Text = "Open" Then
			buttonVisibility = True
		End If

		Dim g As ASPxGridView = CType(sender, ASPxGridView)
		CType(g.Columns("cmd"), GridViewCommandColumn).ShowNewButtonInHeader = buttonVisibility
	End Sub

	Protected Sub grid_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex = -1 Then
			Return
		End If

		Dim buttonVisibility As Boolean = False
		If tbStatus.Text = "Open" Then
			buttonVisibility = True
		End If

		Select Case e.ButtonType
			Case ColumnCommandButtonType.Delete
				e.Visible = buttonVisibility
			Case ColumnCommandButtonType.Edit
				e.Visible = buttonVisibility
		End Select
	End Sub

	Protected Sub grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
		If e.Column.FieldName = "ItemNumber" And grid.IsNewRowEditing Then
			e.Editor.Focus()
		End If
		If e.Column.FieldName = "AvgCost" Or _
			e.Column.FieldName = "OnHand" Or _
			e.Column.FieldName = "OnOrder" Then
			e.Editor.ClientEnabled = False
			e.Editor.Visible = False
		End If
	End Sub

	Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		If IsNothing(e.NewValues("ItemNumber")) Then
			e.RowError = "Please select an Item"
			Return
		End If
		If IsNothing(e.NewValues("QtyOrdered")) Then
			e.RowError = "Please enter a Quantity Ordered"
			Return
		End If
		If IsNothing(e.NewValues("UnitCost")) Then
			e.RowError = "Please enter a Unit Cost"
			Return
		End If
	End Sub

	<WebMethod()> _
	Public Shared Function GetTaxRate(ByVal VendorID As Integer) As Decimal
		Dim taxRate As Decimal = 0
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)

		sql = "SELECT TaxRate " & _
			"FROM NewMerch.Vendors " & _
			"WHERE VendorID = " & VendorID

		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() AndAlso rsql("TaxRate").ToString() <> "" Then
			taxRate = CDec(rsql("TaxRate"))
		End If
		SqlQueryClose(conn, rsql)

		Return taxRate
	End Function

	Public Class ItemInfo
		Public ItemNumber As String = ""
		Public Description As String = ""
		Public CurrentCost As Decimal = 0
		Public StandardPrice As Decimal = 0
		Public QtyOrdered As Integer = 0
	End Class

	<WebMethod()> _
	Public Shared Function GetItemInfo(ByVal PurchaseOrderNum As Integer, _
				ByVal ItemNumber As String, ByVal qtyOrdered As Integer) As ItemInfo
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim ii As ItemInfo = New ItemInfo

		sql = "SELECT ItemDescription, CurrentCost, StandardPrice " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE ItemNumber = '" & Trim(ItemNumber) & "'"

		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			If rsql("ItemDescription").ToString() <> "" Then
				ii.Description = rsql("ItemDescription")
			End If
			If rsql("CurrentCost").ToString() <> "" Then
				ii.CurrentCost = CDec(rsql("CurrentCost"))
			End If
			If rsql("StandardPrice").ToString() <> "" Then
				ii.StandardPrice = CDec(rsql("StandardPrice"))
			End If
		End If
		SqlQueryClose(conn, rsql)

		ii.ItemNumber = ItemNumber
		ii.QtyOrdered = qtyOrdered

		Return ii
	End Function

	<WebMethod()> _
	Public Shared Function ValidateNewInventoryItem(ByVal itemNumber As String, ByVal upc As String, _
				ByVal labelTypeID As Integer, ByVal itemDescription As String, ByVal currentCost As Decimal, _
				ByVal subDepartmentID As Integer, ByVal standardPrice As Decimal, ByVal qtyOrdered As Integer) As String

		If Trim(itemNumber) = "" Then
			Return ("Please enter an Item Number")
		End If
		If Trim(upc) = "" Then
			Return ("Please enter a UPC")
		End If
		If Mid(Trim(upc), 1, 4) = "0000" And labelTypeID = 0 Then
			Return ("Please select a Label Type")
		End If
		If Trim(itemDescription) = "" Then
			Return ("Please enter an Item Description")
		End If
		If subDepartmentID = 0 Then
			Return ("Please select a Sub-Department")
		End If
		If qtyOrdered = 0 Then
			Return ("Please enter a Qty Ordered")
		End If

		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		sql = "SELECT 1 " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE ItemNumber = '" & Trim(itemNumber) & "'"
		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			Return ("The Item Number entered already exists")
		End If
		SqlQueryClose(conn, rsql)

		sql = "SELECT 1 " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE UPC = '" & Trim(upc) & "'"
		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			Return ("The UPC entered already exists")
		End If
		SqlQueryClose(conn, rsql)

		Return "" 'OK
	End Function

	<WebMethod()> _
	Public Shared Function InsertNewInventoryItem(ByVal itemNumber As String, ByVal upc As String, _
				ByVal labelTypeID As Integer, ByVal itemDescription As String, ByVal labelDescription As String,
				ByVal currentCost As Decimal, ByVal subDepartmentID As Integer, ByVal standardPrice As Decimal, _
				ByVal vendorID As Integer, ByVal inventoryActive As Boolean, ByVal storeID As Integer,
				ByVal userID As Integer) As String

		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim cmd As SqlCommand = New SqlCommand()
		cmd.Connection = conn
		cmd.CommandText = "NewMerch.spInventoryItems_Insert"
		cmd.CommandType = System.Data.CommandType.StoredProcedure

		cmd.Parameters.Add(DataUtil.CreateParameter("@itemNumber", System.Data.ParameterDirection.Input, System.Data.DbType.String, Trim(itemNumber)))
		cmd.Parameters.Add(DataUtil.CreateParameter("@itemDescription", System.Data.ParameterDirection.Input, System.Data.DbType.String, itemDescription))
		cmd.Parameters.Add(DataUtil.CreateParameter("@labelDescription", System.Data.ParameterDirection.Input, System.Data.DbType.String, labelDescription))
		cmd.Parameters.Add(DataUtil.CreateParameter("@upc", System.Data.ParameterDirection.Input, System.Data.DbType.String, Trim(upc)))
		cmd.Parameters.Add(DataUtil.CreateParameter("@labelTypeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, labelTypeID))
		cmd.Parameters.Add(DataUtil.CreateParameter("@vendorID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, vendorID))
		cmd.Parameters.Add(DataUtil.CreateParameter("@currentCost", System.Data.ParameterDirection.Input, System.Data.DbType.Decimal, currentCost))
		cmd.Parameters.Add(DataUtil.CreateParameter("@standardPrice", System.Data.ParameterDirection.Input, System.Data.DbType.Decimal, standardPrice))
		cmd.Parameters.Add(DataUtil.CreateParameter("@categoryID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 9))
		cmd.Parameters.Add(DataUtil.CreateParameter("@departmentID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 12))
		cmd.Parameters.Add(DataUtil.CreateParameter("@subDepartmentID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, subDepartmentID))
		cmd.Parameters.Add(DataUtil.CreateParameter("@active", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, True))
		cmd.Parameters.Add(DataUtil.CreateParameter("@notes", System.Data.ParameterDirection.Input, System.Data.DbType.String, ""))
		cmd.Parameters.Add(DataUtil.CreateParameter("@inventoryActive", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, inventoryActive))
		cmd.Parameters.Add(DataUtil.CreateParameter("@storeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, storeID))
		cmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, userID))
		Dim errorID As Integer = 0
		cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			cmd.ExecuteNonQuery()
			errorID = cmd.Parameters("@RETURN_VALUE").Value
			upc = cmd.Parameters("@upc").Value.ToString()
			If errorID > 0 Then
				vbHandleProgramError(errorID, "NMPurchaseOrder, InsertNewInventoryItem")
				Return ("Error inserting new item into database")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "NMPurchaseOrder, InsertNewInventoryItem")
		Finally
			conn.Close()
		End Try

		Return "" 'OK
	End Function

	Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		dsPurchaseOrderItems.DeleteParameters.Clear()
		dsPurchaseOrderItems.DeleteParameters.Add("PurchaseOrderNum", navigateComboBox.Value)
		dsPurchaseOrderItems.DeleteParameters.Add("StoreID", tbStoreID.Value)
		dsPurchaseOrderItems.DeleteParameters.Add("LineNumber", e.Values("LineNumber"))
		dsPurchaseOrderItems.DeleteParameters.Add("ItemNumber", e.Values("ItemNumber"))
		dsPurchaseOrderItems.DeleteParameters.Add("QtyOrdered", e.Values("QtyOrdered"))
		dsPurchaseOrderItems.DeleteParameters.Add("CurrentUserID", Session("vUserID"))
		dsPurchaseOrderItems.Delete()
		e.Cancel = True
	End Sub

	Protected Sub ItemNumber_ItemsRequestedByFilterCondition(source As Object, e As ListEditItemsRequestedByFilterConditionEventArgs)
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		dsInventoryItemsByVendor.SelectCommand = "SELECT ItemNumber AS Item, ItemNumber, ItemDescription " & _
			"FROM (SELECT ItemNumber AS Item, ItemNumber, ItemDescription, VendorID, Active, " & _
			"ROW_NUMBER() OVER(ORDER BY T.ItemNumber) AS [rn] FROM NewMerch.InventoryItems AS T " & _
				"WHERE ((ItemNumber) LIKE @filter) AND VendorID = @vendorID AND Active = 1) AS ST " & _
					"WHERE ST.[rn] BETWEEN @startIndex and @endIndex "
		If e.Filter = "" Then
		dsInventoryItemsByVendor.SelectCommand += "UNION " & _
		"SELECT '<New Item>' AS Item, '<New Item>' AS ItemNumber, '' AS ItemDescription"
		End If

		Dim endIndex = e.EndIndex + 1
		If e.Filter = "" Then
			endIndex = e.BeginIndex + 100
		End If

		dsInventoryItemsByVendor.SelectParameters.Clear()
		dsInventoryItemsByVendor.SelectParameters.Add("filter", TypeCode.String, String.Format("{0}%", e.Filter))
		dsInventoryItemsByVendor.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString())
		dsInventoryItemsByVendor.SelectParameters.Add("endIndex", TypeCode.Int64, endIndex.ToString())
		dsInventoryItemsByVendor.SelectParameters.Add("VendorID", TypeCode.Int32, tbVendorID.Value.ToString())
		comboBox.DataSource = dsInventoryItemsByVendor
		comboBox.DataBind()
	End Sub

	Protected Sub ItemNumber_ItemRequestedByValue(source As Object, e As ListEditItemRequestedByValueEventArgs)
		Dim value As Long = 0
		If e.Value Is Nothing OrElse (Not Int64.TryParse(e.Value.ToString(), value)) Then
			Return
		End If
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		If e.Value.ToString() = "<New Item>" Then
			dsInventoryItemsByVendor.SelectCommand = "SELECT '<New Item>' AS Item, '<New Item>' AS ItemNumber, '' AS ItemDescription"
			dsInventoryItemsByVendor.SelectParameters.Clear()
		Else
			dsInventoryItemsByVendor.SelectCommand = "SELECT ItemNumber AS Item, ItemNumber, ItemDescription " & _
				"FROM NewMerch.InventoryItems " & _
				"WHERE ItemNumber = @ItemNumber " & _
				"ORDER BY ItemNumber"
			dsInventoryItemsByVendor.SelectParameters.Clear()
			dsInventoryItemsByVendor.SelectParameters.Add("ItemNumber", TypeCode.String, e.Value.ToString())
		End If

		comboBox.DataSource = dsInventoryItemsByVendor
		comboBox.DataBind()
	End Sub

	<WebMethod()> _
	Public Shared Function GenerateUpc(ByVal userID As Integer) As String
		Dim upc As String = ""
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim cmd As SqlCommand = New SqlCommand()
		cmd.Connection = conn
		cmd.CommandText = "NewMerch.spInventoryItems_GenerateBarcode"
		cmd.CommandType = System.Data.CommandType.StoredProcedure

		cmd.Parameters.Add(DataUtil.CreateParameterSQL("@upc", System.Data.ParameterDirection.Output, SqlDbType.VarChar, upc, 12))
		cmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, userID))
		Dim errorID As Integer = 0
		cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			cmd.ExecuteNonQuery()
			errorID = cmd.Parameters("@RETURN_VALUE").Value
			upc = cmd.Parameters("@upc").Value.ToString()
			If errorID > 0 Then
				vbHandleProgramError(errorID, "NMPurchaseOrder, GenerateUpc")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "NMPurchaseOrder, GenerateUpc")
		Finally
			conn.Close()
		End Try

		Return upc
	End Function

End Class
