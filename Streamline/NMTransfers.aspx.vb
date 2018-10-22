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

Partial Class NMTranfers
	Inherits System.Web.UI.Page

	Public daysToAllowChanges As Integer = 5

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
						Case "dtOldestTransferDate"
							navigateComboBox.DataBind()
					End Select
				End If
			Case "GET"
				hf("InsertMode") = False
				hf("UserID") = Session("vUserID")
				hfTransferNum.Value = ""
				dtOldestTransferDate.Value = DateAdd(DateInterval.Month, -2, Now)
				hfOldestTransferDate.Value = dtOldestTransferDate.Value
				navigateComboBox.SelectedIndex = -1
				ClearForm()
				grid.Visible = False
				hf("FromStoreChanged") = False
				hf("ToStoreChanged") = False
			Case "HEAD"
		End Select
	End Sub

	Protected Sub navigateComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		PopulateForm()
	End Sub

	Protected Sub newButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		ClearForm()
		hf("InsertMode") = True
		flT.FindItemOrGroupByName("Controls").Visible = False
		newButton.Visible = False
		updateButton.Visible = True
		cancelButton.Visible = True
		deleteButton.Visible = False
		grid.Visible = False

		tbFromStoreID.SelectedIndex = 0
		tbToStoreID.SelectedIndex = 0

		tbTransferNum.Text = ""

		dsTransferHeaders.SelectParameters("TransferNum").DefaultValue = ""
	End Sub

	Protected Sub updateButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		If Trim(tbFromStoreID.SelectedIndex) = 0 Then
			errorMessageLabel.Text = "Please select a From Store"
			errorMessageLabel.Visible = True
			Return
		End If
		If Trim(tbToStoreID.SelectedIndex) = 0 Then
			errorMessageLabel.Text = "Please select a To Store"
			errorMessageLabel.Visible = True
			Return
		End If
		Try
			If hf("InsertMode") Then
				dsTransferHeaders.Insert()

			Else
				dsTransferHeaders.UpdateParameters("TransferNum").DefaultValue = hfTransferNum.Value
				dsTransferHeaders.Update()
			End If
		Catch ex As Exception
			errorMessageLabel.Text = ex.Message
			errorMessageLabel.Visible = True
		End Try

		hf("InsertMode") = False
		flT.FindItemOrGroupByName("Controls").Visible = True
		navigateComboBox.DataBind()
		navigateComboBox.Visible = True
		grid.Visible = True

		newButton.Visible = True

		If DateAdd(DateInterval.Day, daysToAllowChanges, tbTransferDate.Value) >= Today Then
			deleteButton.Visible = True
		Else
			deleteButton.Visible = False
		End If

		hf("FromStoreChanged") = False
		hf("ToStoreChanged") = False

		PopulateForm()
		navigateComboBox.SelectedIndex = navigateComboBox.Items.IndexOfText(CInt(hfTransferNum.Value))
		grid.DataBind()
	End Sub

	Sub On_Inserted(ByVal sender As Object, ByVal e As SqlDataSourceStatusEventArgs)
		Dim command As DbCommand
		command = e.Command

		hfTransferNum.Value = command.Parameters("@TransferNum").Value.ToString()
		tbTransferNum.Text = hfTransferNum.Value
	End Sub

	Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		hf("InsertMode") = False
		flT.FindItemOrGroupByName("Controls").Visible = True
		navigateComboBox.DataBind()
		navigateComboBox.SelectedIndex = navigateComboBox.Items.IndexOfValue(hfTransferNum.Value)
		PopulateForm()
	End Sub

	Protected Sub deleteButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		If Not hf("delete") Then
			Return
		End If
		dsTransferHeaders.DeleteParameters("TransferNum").DefaultValue = hfTransferNum.Value
		dsTransferHeaders.DeleteParameters("CurrentUserID").DefaultValue = Session("vUserID")
		dsTransferHeaders.Delete()

		dsTransferHeaders.SelectParameters("TransferNum").DefaultValue = hfTransferNum.Value
		Dim item As DevExpress.Web.ListEditItem = navigateComboBox.Items(navigateComboBox.SelectedIndex)
		navigateComboBox.Items.Remove(item)
		navigateComboBox.SelectedIndex = -1

		hfTransferNum.Value = ""
		flT.DataBind()
		grid.DataBind()
		tbFromStoreID.SelectedIndex = 0
		tbToStoreID.SelectedIndex = 0
		tbTransferNum.Text = ""
		ClearForm()

		deleteButton.Visible = False
		updateButton.Visible = False
		cancelButton.Visible = False
	End Sub

	Private Sub PopulateForm()
		grid.Visible = True
		ClearForm()
		If Not IsNothing(navigateComboBox.Value) Then
			dsTransferHeaders.SelectParameters("TransferNum").DefaultValue = navigateComboBox.Value.ToString()
			flT.DataBind()
			hfTransferNum.Value = navigateComboBox.Value.ToString()
		End If

		newButton.Visible = True
		flT.DataBind()


		If DateAdd(DateInterval.Day, daysToAllowChanges, tbTransferDate.Value) >= Today Then
			deleteButton.Visible = True
			updateButton.Visible = True
			cancelButton.Visible = True
		Else
			deleteButton.Visible = False
			updateButton.Visible = False
			cancelButton.Visible = False
			tbFromStoreID.ReadOnly = True
			tbFromStoreID.BackColor = Drawing.Color.WhiteSmoke
			tbToStoreID.ReadOnly = True
			tbToStoreID.BackColor = Drawing.Color.WhiteSmoke
		End If

	End Sub

	Private Sub ClearForm()
		tbTransferNum.Text = ""
		tbFromStoreID.ReadOnly = False
		tbFromStoreID.BackColor = Drawing.Color.White
		tbToStoreID.ReadOnly = False
		tbToStoreID.BackColor = Drawing.Color.White
		tbTransferDate.Value = Today
		tbNotes.Text = ""
		errorMessageLabel.Visible = False
	End Sub

	Protected Sub grid_DataBound(sender As Object, e As EventArgs)
		Dim buttonVisibility As Boolean = False
		If DateAdd(DateInterval.Day, daysToAllowChanges, tbTransferDate.Value) >= Today Then
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
		If DateAdd(DateInterval.Day, daysToAllowChanges, tbTransferDate.Value) >= Today _
			And hf("FromStoreChanged") = False _
			And hf("ToStoreChanged") = False Then
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
		If IsNothing(e.NewValues("QtyTransferred")) Then
			e.RowError = "Please enter a Quantity to Transfer"
			Return
		End If
	End Sub

	Public Class ItemInfo
		Public ItemNumber As String = ""
		Public Description As String = ""
		Public CurrentCost As Decimal = 0
		Public StandardPrice As Decimal = 0
		Public QtyOrdered As Integer = 0
	End Class

	<WebMethod()> _
	Public Shared Function GetItemInfo(ByVal TransferNum As Integer, _
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

	Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		dsTransferItems.DeleteParameters.Clear()
		dsTransferItems.DeleteParameters.Add("TransferNum", navigateComboBox.Value)
		dsTransferItems.DeleteParameters.Add("FromStoreID", tbFromStoreID.Value)
		dsTransferItems.DeleteParameters.Add("ToStoreID", tbToStoreID.Value)
		dsTransferItems.DeleteParameters.Add("LineNumber", e.Values("LineNumber"))
		dsTransferItems.DeleteParameters.Add("ItemNumber", e.Values("ItemNumber"))
		dsTransferItems.DeleteParameters.Add("QtyTransferred", e.Values("QtyTransferred"))
		dsTransferItems.DeleteParameters.Add("CurrentUserID", Session("vUserID"))
		dsTransferItems.Delete()
		e.Cancel = True
	End Sub

	Protected Sub ItemNumber_ItemsRequestedByFilterCondition(source As Object, e As ListEditItemsRequestedByFilterConditionEventArgs)
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		dsInventoryItems.SelectCommand = "SELECT ItemNumber AS Item, ItemNumber, ItemDescription " & _
			"FROM (SELECT ItemNumber AS Item, ItemNumber, ItemDescription, VendorID, Active, " & _
			"ROW_NUMBER() OVER(ORDER BY T.ItemNumber) AS [rn] FROM NewMerch.InventoryItems AS T " & _
				"WHERE ((ItemNumber) LIKE @filter)) AS ST " & _
					"WHERE ST.[rn] BETWEEN @startIndex and @endIndex "
		If e.Filter = "" Then
			dsInventoryItems.SelectCommand += "UNION " & _
			"SELECT '<New Item>' AS Item, '<New Item>' AS ItemNumber, '' AS ItemDescription"
		End If

		Dim endIndex = e.EndIndex + 1
		If e.Filter = "" Then
			endIndex = e.BeginIndex + 100
		End If

		dsInventoryItems.SelectParameters.Clear()
		dsInventoryItems.SelectParameters.Add("filter", TypeCode.String, String.Format("{0}%", e.Filter))
		dsInventoryItems.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString())
		dsInventoryItems.SelectParameters.Add("endIndex", TypeCode.Int64, endIndex.ToString())
		comboBox.DataSource = dsInventoryItems
		comboBox.DataBind()
	End Sub

	Protected Sub ItemNumber_ItemRequestedByValue(source As Object, e As ListEditItemRequestedByValueEventArgs)
		Dim value As Long = 0
		If e.Value Is Nothing OrElse (Not Int64.TryParse(e.Value.ToString(), value)) Then
			Return
		End If
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		If e.Value.ToString() = "<New Item>" Then
			dsInventoryItems.SelectCommand = "SELECT '<New Item>' AS Item, '<New Item>' AS ItemNumber, '' AS ItemDescription"
			dsInventoryItems.SelectParameters.Clear()
		Else
			dsInventoryItems.SelectCommand = "SELECT ItemNumber AS Item, ItemNumber, ItemDescription " & _
				"FROM NewMerch.InventoryItems " & _
				"WHERE ItemNumber = @ItemNumber " & _
				"ORDER BY ItemNumber"
			dsInventoryItems.SelectParameters.Clear()
			dsInventoryItems.SelectParameters.Add("ItemNumber", TypeCode.String, e.Value.ToString())
		End If

		comboBox.DataSource = dsInventoryItems
		comboBox.DataBind()
	End Sub

End Class
