Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.Internal
Imports System.Data.SqlClient
Imports System.Data.Common
Imports DataUtil

Partial Class NMVendorMaint
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Select Case Request.HttpMethod
			Case "POST"
			Case "GET"
				hf("InsertMode") = False
				hfVendorID.Value = ""
				navigateComboBox.SelectedIndex = -1
			Case "HEAD"
		End Select
	End Sub

	Protected Sub navigateComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		PopulateForm()
	End Sub

	Protected Sub newButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		ClearForm()
		hf("InsertMode") = True
		flVendors.FindItemOrGroupByName("Controls").Visible = False
		navigateComboBox.Visible = False
		newButton.Visible = False
		updateButton.Visible = True
		cancelButton.Visible = True
		deleteButton.Visible = False

		tbBackorders.Checked = True
	End Sub

	Protected Sub updateButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		If Trim(tbVendorName.Text) = "" Then
			errorMessageLabel.Text = "Please enter a Vendor Name"
			errorMessageLabel.Visible = True
			Return
		End If

		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		sql = "SELECT 1 " & _
			"FROM NewMerch.Vendors " & _
			"WHERE VendorName = '" & tbVendorName.Text & "' "
		If Not hf("InsertMode") Then
		' Update, exclude currently selected ItemNumber
		sql += "AND VendorID <> " & navigateComboBox.Value
		End If
		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			errorMessageLabel.Text = "The Vendor Name entered already exists"
			errorMessageLabel.Visible = True
			Return
		End If
		SqlQueryClose(conn, rsql)

		Try
			If hf("InsertMode") Then
				dsVendors.Insert()
			Else
				dsVendors.UpdateParameters("VendorID").DefaultValue = hfVendorID.Value
				dsVendors.Update()
			End If
		Catch ex As Exception
			errorMessageLabel.Text = ex.Message
			errorMessageLabel.Visible = True
		End Try

		hf("InsertMode") = False
		flVendors.FindItemOrGroupByName("Controls").Visible = True
		navigateComboBox.DataBind()
		navigateComboBox.Visible = True

		newButton.Visible = True
		deleteButton.Visible = True

		navigateComboBox.SelectedIndex = navigateComboBox.Items.IndexOfText(tbVendorName.Text)
	End Sub

	Sub On_Inserted(ByVal sender As Object, ByVal e As SqlDataSourceStatusEventArgs)
		Dim command As DbCommand
		command = e.Command

		hfVendorID.Value = CInt(command.Parameters("@VendorID").Value)
	End Sub

	Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		ClearForm()
		hf("InsertMode") = False
		flVendors.FindItemOrGroupByName("Controls").Visible = True
		navigateComboBox.DataBind()
		navigateComboBox.SelectedIndex = navigateComboBox.Items.IndexOfValue(CInt(hfVendorID.Value))
		PopulateForm()
	End Sub

	Protected Sub deleteButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		If Not hf("delete") Then
			Return
		End If
		ClearForm()
		dsVendors.DeleteParameters("VendorID").DefaultValue = hfVendorID.Value
		dsVendors.Delete()

		dsVendors.SelectParameters("VendorID").DefaultValue = hfVendorID.Value
		Dim item As DevExpress.Web.ListEditItem = navigateComboBox.Items(navigateComboBox.SelectedIndex)
		navigateComboBox.Items.Remove(item)
		navigateComboBox.SelectedIndex = -1

		deleteButton.Visible = False
		updateButton.Visible = False
		cancelButton.Visible = False
	End Sub

	Private Sub PopulateForm()
		ClearForm()
		If Not IsNothing(navigateComboBox.Value) Then
			dsVendors.SelectParameters("VendorID").DefaultValue = navigateComboBox.Value.ToString()
			flVendors.DataBind()
			hfVendorID.Value = navigateComboBox.Value.ToString()
		End If

		newButton.Visible = True
		If navigateComboBox.SelectedIndex = -1 Then
			deleteButton.Visible = False
			updateButton.Visible = False
			cancelButton.Visible = False
		Else
			deleteButton.Visible = True
			updateButton.Visible = True
			cancelButton.Visible = True
		End If
	End Sub

	Private Sub ClearForm()
		tbVendorName.Text = ""
		tbAccountNumber.Text = ""
		tbAddressLine1.Text = ""
		tbAddressLine2.Text = ""
		tbCity.Text = ""
		tbState.Text = ""
		tbZip.Text = ""
		tbContact.Text = ""
		tbPhone.Text = ""
		tbAltPhone.Text = ""
		tbFax.Text = ""
		tbEmail.Text = ""
		tbAccountNumber.Text = ""
		tbTerms.Text = ""
		tbTaxRate.Value = ""
		tbBackorders.Checked = False
		tbActive.Checked = True
		tbNotes.Text = ""
		errorMessageLabel.Visible = False
	End Sub
End Class
