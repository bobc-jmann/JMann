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

Partial Class NMInventoryMaint
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
						Case "ckShowInactiveInventoryItems"
							hfShowInactiveInventoryItems.Value = ckShowInactiveInventoryItems.Checked
							navigateComboBox.DataBind()
					End Select
				End If
			Case "GET"
				hf("InsertMode") = False
				hf("UserID") = Session("vUserID")
				hf("Search") = False
				hfItemNumber.Value = ""
				hfUPC.Value = ""
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

		PopulateForm()
	End Sub

	Protected Sub navigateVendorBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		hfVendorID.Value = navigateVendorBox.Value.ToString()
		dsInventoryItems_SelectList.SelectCommand = "SELECT TOP (100) ItemNumber AS Item, ItemNumber, ItemDescription " & _
			"FROM [NewMerch].InventoryItems " & _
			"WHERE Active = 1 "
		If hfVendorID.Value <> "0" Then
			dsInventoryItems_SelectList.SelectCommand &= "AND VendorID = " & hfVendorID.Value & " "
		End If
		dsInventoryItems_SelectList.SelectCommand &= "ORDER BY Item"
		navigateComboBox.DataSource = dsInventoryItems_SelectList
		navigateComboBox.DataBind()
		navigateComboBox.SelectedIndex = -1

		If Not IsNothing(navigateComboBox.Value) Then
			PopulateForm()
		End If
	End Sub

	Protected Sub newButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		ClearForm()
		hf("InsertMode") = True
		flInventoryItems.FindItemOrGroupByName("Controls").Visible = False

		newButton.Visible = False
		updateButton.Visible = True
		cancelButton.Visible = True
		deleteButton.Visible = False
		grid.Visible = False

		If hfVendorID.Value <> "" Then
			tbVendorID.SelectedIndex = tbVendorID.Items.IndexOfValue(CInt(hfVendorID.Value))
		Else
			tbVendorID.Value = 0
			dsVendors_SelectList.DataBind()
		End If
		tbCategoryID.SelectedIndex = tbCategoryID.Items.IndexOfText("PINK")
		tbDepartmentID.SelectedIndex = tbDepartmentID.Items.IndexOfText("NEW ITEMS")
		tbSubDepartmentID.SelectedIndex = 0
		tbActive.Checked = True
		btnGenerateUPC.Visible = True
		tbLabelTypeID.Visible = True
		CType(flInventoryItems.Items(7), LayoutItem).ShowCaption = DevExpress.Utils.DefaultBoolean.True
		tbLabelTypeID.SelectedIndex = 0
		tbUPC.ReadOnly = False
		tbUPC.BackColor = Drawing.Color.White
	End Sub

	Protected Sub updateButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		If Trim(tbItemNumber.Text) = "" Then
			errorMessageLabel.Text = "Please enter an Item Number"
			errorMessageLabel.Visible = True
			Return
		End If
		If Trim(tbItemDescription.Text) = "" Then
			errorMessageLabel.Text = "Please enter an Item Description"
			errorMessageLabel.Visible = True
			Return
		End If
		If Mid(Trim(tbUPC.Text), 1, 4) = "0000" And Not tbLabelTypeID.SelectedIndex > 0 Then
			errorMessageLabel.Text = "Please select a Label Type"
			errorMessageLabel.Visible = True
			Return
		End If
		If Trim(tbVendorID.SelectedIndex) = 0 Then
			errorMessageLabel.Text = "Please select a Vendor"
			errorMessageLabel.Visible = True
			Return
		End If
		If Trim(tbCategoryID.SelectedIndex) = 0 Then
			errorMessageLabel.Text = "Please select a Category"
			errorMessageLabel.Visible = True
			Return
		End If
		If Trim(tbDepartmentID.SelectedIndex) = 0 Then
			errorMessageLabel.Text = "Please select a Department"
			errorMessageLabel.Visible = True
			Return
		End If
		If Trim(tbSubDepartmentID.SelectedIndex) = 0 Then
			errorMessageLabel.Text = "Please select a Sub Department"
			errorMessageLabel.Visible = True
			Return
		End If

		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		sql = "SELECT 1 " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE ItemNumber = '" & Trim(tbItemNumber.Text) & "' "
		If Not hf("InsertMode") Then
			' Update, exclude currently selected ItemNumber
			sql += "AND ItemNumber <> '" & hfItemNumber.Value & "'"
		End If
		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			errorMessageLabel.Text = "The Item Number entered already exists"
			errorMessageLabel.Visible = True
			Return
		End If
		SqlQueryClose(conn, rsql)

		sql = "SELECT 1 " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE UPC = '" & Trim(tbUPC.Text) & "' "
		If Not hf("InsertMode") Then
			' Update, exclude currently selected ItemNumber
			sql += "AND UPC <> '" & hfUPC.Value & "'"
		End If
		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			errorMessageLabel.Text = "The UPC entered already exists"
			errorMessageLabel.Visible = True
			Return
		End If
		SqlQueryClose(conn, rsql)

		Try
			If hf("InsertMode") Then
				dsInventoryItems.Insert()
			Else
				dsInventoryItems.UpdateParameters("ItemNumber").DefaultValue = hfItemNumber.Value
				dsInventoryItems.Update()
			End If
		Catch ex As Exception
			errorMessageLabel.Text = ex.Message
			errorMessageLabel.Visible = True
		End Try

		If IsNothing(navigateComboBox.Value) Then
			navigateVendorBox.DataBind()
			navigateVendorBox.SelectedIndex = navigateVendorBox.Items.IndexOfText(tbVendorID.Text)
			hfVendorID.Value = tbVendorID.Value
			navigateComboBox.DataBind()
			navigateComboBox.Value = Trim(tbItemNumber.Text)
		End If
		If hf("InsertMode") Then
			hf("InsertMode") = False
			PopulateForm()
		End If
		flInventoryItems.FindItemOrGroupByName("Controls").Visible = True
		navigateComboBox.DataBind()

		newButton.Visible = True
		deleteButton.Visible = True
		grid.Visible = True
		grid.DataBind()
	End Sub

	Sub On_Inserted(ByVal sender As Object, ByVal e As SqlDataSourceStatusEventArgs)
		Dim command As DbCommand
		command = e.Command

		hfItemNumber.Value = Trim(tbItemNumber.Text)
		hfUPC.Value = Trim(tbUPC.Text)
	End Sub

	Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		hf("InsertMode") = False
		flInventoryItems.FindItemOrGroupByName("Controls").Visible = True
		navigateComboBox.DataBind()
		navigateComboBox.Value = hfItemNumber.Value
		PopulateForm()
	End Sub

	Protected Sub deleteButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		If Not hf("delete") Then
			Return
		End If
		ClearForm()
		dsInventoryItems.DeleteParameters("ItemNumber").DefaultValue = hfItemNumber.Value
		Try
			dsInventoryItems.Delete()
		Catch
			ja("This item cannot be deleted.")
			Return
		End Try


		dsInventoryItems.SelectParameters("ItemNumber").DefaultValue = hfItemNumber.Value
		navigateComboBox.DataBind()
		navigateComboBox.SelectedIndex = -1
		hfItemNumber.Value = ""
		hfUPC.Value = ""
		flInventoryItems.DataBind()
		grid.DataBind()

		deleteButton.Visible = False
		updateButton.Visible = False
		cancelButton.Visible = False
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
				vbHandleProgramError(errorID, "NMInventoryMaint, GenerateUpc")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "NMInventoryMaint, GenerateUpc")
		Finally
			conn.Close()
		End Try

		Return upc
	End Function

	Private Sub PopulateForm()
		ClearForm()

		If hf("Search") Then
			hfVendorID.Value = navigateVendorBox.Value
			navigateComboBox.DataBind()
			hf("Search") = False
		End If

		If Not IsNothing(navigateComboBox.Value) Then
			dsInventoryItems.SelectParameters("ItemNumber").DefaultValue = navigateComboBox.Value.ToString()
			hfItemNumber.Value = navigateComboBox.Value.ToString()
			flInventoryItems.DataBind()
			hfUPC.Value = Trim(tbUPC.Text)
		End If

		newButton.Visible = True
		If IsNothing(navigateComboBox.Value) Then
			deleteButton.Visible = False
			updateButton.Visible = False
			cancelButton.Visible = False
		Else
			deleteButton.Visible = True
			updateButton.Visible = True
			cancelButton.Visible = True
		End If
		If Trim(tbUPC.Text) = "" And Not IsNothing(navigateComboBox.Value) Then
			btnGenerateUPC.Visible = True
			tbLabelTypeID.Visible = True
			CType(flInventoryItems.Items(7), LayoutItem).ShowCaption = DevExpress.Utils.DefaultBoolean.True
			tbUPC.ReadOnly = False
			tbUPC.BackColor = Drawing.Color.White
		Else
			btnGenerateUPC.Visible = False
			If Mid(Trim(tbUPC.Text), 1, 4) = "0000" Then
				tbLabelTypeID.Visible = True
				CType(flInventoryItems.Items(7), LayoutItem).ShowCaption = DevExpress.Utils.DefaultBoolean.True
			Else
				tbLabelTypeID.Visible = False
				tbLabelTypeID.SelectedIndex = 0
				CType(flInventoryItems.Items(7), LayoutItem).ShowCaption = DevExpress.Utils.DefaultBoolean.False
			End If
		End If
	End Sub

	Private Sub ClearForm()
		tbItemNumber.Text = ""
		tbItemDescription.Text = ""
		tbLabelDescription.Text = ""
		tbUPC.Text = ""
		tbUPC.ReadOnly = False
		tbUPC.BackColor = Drawing.Color.White
		tbVendorID.Text = ""
		tbCurrentCost.Text = ""
		tbStandardPrice.Text = ""
		tbCategoryID.Text = ""
		tbDepartmentID.Text = ""
		tbSubDepartmentID.Text = ""
		tbActive.Checked = False
		tbNotes.Text = ""
		errorMessageLabel.Visible = False
	End Sub

	Protected Sub grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
		If e.Column.FieldName = "AvgCost" Or _
			e.Column.FieldName = "OnHand" Or _
			e.Column.FieldName = "OnOrder" Then
			e.Editor.ClientEnabled = False
			e.Editor.Visible = False
		End If
	End Sub

	Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		If IsNothing(e.NewValues("StoreID")) Then
			e.RowError = "Please select a Store"
			Return
		End If
		If e.IsNewRow Then
			Dim sql As String = ""
			Dim rsql As SqlDataReader = Nothing
			Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)

			sql = "SELECT 1 " & _
				"FROM NewMerch.InventoryByLocation " & _
				"WHERE ItemNumber = '" & navigateComboBox.Text & "' " & _
					"AND StoreID = " & e.NewValues("StoreID")
			If Not SqlQueryOpen(conn, rsql, sql, "") Then
				e.RowError = "SqlQueryOpen Error on InventoryByLocation"
				Return
			End If
			If rsql.Read() Then
				e.RowError = "An entry for the selected Store already exists"
			End If
			SqlQueryClose(conn, rsql)
		End If
	End Sub

	Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		dsInventoryByLocation.DeleteParameters.Clear()
		dsInventoryByLocation.DeleteParameters.Add("ItemNumber", hfItemNumber.Value)
		dsInventoryByLocation.DeleteParameters.Add("StoreID", e.Values("StoreID"))
		dsInventoryByLocation.DeleteParameters.Add("CurrentUserID", Session("vUserID"))
		dsInventoryByLocation.Delete()
		dsInventoryByLocation.DataBind()
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
