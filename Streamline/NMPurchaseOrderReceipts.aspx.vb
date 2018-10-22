Imports Microsoft.VisualBasic
Imports System
Imports System.Data.Common
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.Internal
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Data
Imports System.Data.Sql
Imports DataUtil

Partial Class NMPurchaseOrderReceipts
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
						Case Else
							dsPurchaseOrderHeaders.DataBind()
					End Select
				End If
			Case "GET"
				hf("InsertMode") = False
				hfPurchaseOrderNum.Value = ""
				dtOldestPurchaseOrderDate.Value = DateAdd(DateInterval.Month, -2, Now)
				hfOldestPurchaseOrderDate.Value = dtOldestPurchaseOrderDate.Value
				hfShowCompletedPurchaseOrders.Value = False
				navigateComboBox.SelectedIndex = -1
				ClearForm()
				hf("CurrentUserID") = Session("vUserID")
				grid.Visible = False
			Case "HEAD"
		End Select
	End Sub

	Protected Sub navigateComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		' When using multiple columns in a combo box, extra events are generated when they shouldn't be
		If navigateComboBox.Text = hfPurchaseOrderNum.Value Then
			Return
		End If

		PopulateForm()
	End Sub

	Protected Sub grid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("ReceiptNum") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
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

		PopulateForm()
		navigateComboBox.SelectedIndex = navigateComboBox.Items.IndexOfText(tbPurchaseOrderNum.Text)
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
		navigateComboBox.Visible = True
		navigateComboBox.DataBind()
		navigateComboBox.SelectedIndex = navigateComboBox.Items.IndexOfValue(hfPurchaseOrderNum.Value)
		PopulateForm()
	End Sub

	Private Sub PopulateForm()
		grid.Visible = True
		ClearForm()
		If Not IsNothing(navigateComboBox.Value) Then
			dsPurchaseOrderHeaders.SelectParameters("PurchaseOrderNum").DefaultValue = navigateComboBox.Value.ToString()
			flPO.DataBind()
			hfPurchaseOrderNum.Value = navigateComboBox.Value.ToString()
		End If

		updateButton.Visible = True
		cancelButton.Visible = True
	End Sub

	Private Sub ClearForm()
		tbStatus.Text = ""
		tbPurchaseOrderDate.Value = Today
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

	Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		'If IsNothing(e.NewValues("ItemNumber")) Then
		'	e.RowError = "Please select an Item"
		'	Return
		'End If
		'If IsNothing(e.NewValues("QtyOrdered")) Then
		'	e.RowError = "Please enter a Quantity Ordered"
		'	Return
		'End If
		'If IsNothing(e.NewValues("UnitCost")) Then
		'	e.RowError = "Please enter a Unit Cost"
		'	Return
		'End If
	End Sub

	<WebMethod()> _
	Public Shared Function InsertNewReceiptRow(ByVal PurchaseOrderNum As Integer, ByVal currentUserID As Integer) As Integer
		Dim missingPricesCnt As Integer = 0
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim cmd As SqlCommand = New SqlCommand()
		cmd.Connection = conn
		cmd.CommandText = "NewMerch.spPurchaseOrderReceipts_Insert"
		cmd.CommandType = System.Data.CommandType.StoredProcedure

		cmd.Parameters.Add(DataUtil.CreateParameter("@purchaseOrderNum", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, PurchaseOrderNum))
		cmd.Parameters.Add(DataUtil.CreateParameter("@missingPricesCnt", System.Data.ParameterDirection.Output, System.Data.DbType.Int32, missingPricesCnt))
		cmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, currentUserID))
		Dim errorID As Integer = 0
		cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			cmd.ExecuteNonQuery()
			errorID = cmd.Parameters("@RETURN_VALUE").Value
			missingPricesCnt = cmd.Parameters("@missingPricesCnt").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "NMPurchaseOrderReceipts, InsertNewReceiptRow")
			End If

		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify Admin", "NMPurchaseOrderReceipts, InsertNewReceiptRow")
			Throw New Exception(ex.Message)
		Finally
			conn.Close()
		End Try

		Return missingPricesCnt
	End Function


	Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		dsPurchaseOrderReceipts.DeleteParameters.Clear()
		dsPurchaseOrderReceipts.DeleteParameters.Add("PurchaseOrderNum", navigateComboBox.Value)
		dsPurchaseOrderReceipts.DeleteParameters.Add("ReceiptNum", e.Values("ReceiptNum"))
		dsPurchaseOrderReceipts.DeleteParameters.Add("CurrentUserID", Session("vUserID"))
		dsPurchaseOrderReceipts.Delete()
		dsPurchaseOrderReceipts.DataBind()

		flPO.DataBind()	' This actually changes the value of tbStatus.Text. It just doesn't refresh.
		tbStatus.Text = tbStatus.Text ' Try to force refresh. Still doesn't work.

		e.Cancel = True
	End Sub

End Class
