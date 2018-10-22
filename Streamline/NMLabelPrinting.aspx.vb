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
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports DevExpress.Utils
Imports System.IO
Imports DataUtil

Partial Class NMLabelPrinting
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		hf("UserID") = Session("vUserID")
	End Sub

	Protected Sub tbStoreID_SelectedIndexChanged(sender As Object, e As EventArgs)
		grid.DataBind()
	End Sub

	Protected Sub tbUPC_TextChanged(sender As Object, e As EventArgs)
		If tbStoreID.Value = 0 Then
			ja("Select a Store")
			Return
		End If
		If tbLabelTypeID.Value = 0 Then
			ja("Select a Label Type")
			Return
		End If

		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "NewMerch.spLabelPrinting_Insert"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@StoreID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, tbStoreID.Value))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@UPC", System.Data.ParameterDirection.Input, System.Data.DbType.String, tbUPC.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@LabelTypeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, tbLabelTypeID.Value))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@QtyToPrint", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 1))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.CommandTimeout = 150
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				ja("UPC not found")
			Else
				tbUPC.Text = ""
				tbUPC.Focus()
				grid.DataBind()
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try
	End Sub

	Protected Sub grid_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex < 0 Then
			Return
		End If

		Dim buttonVisibility As Boolean = True 'IIf(grid.GetRowValues(e.VisibleIndex, "Posted"), False, True)

		Select Case e.ButtonType
			Case ColumnCommandButtonType.Delete
				e.Visible = buttonVisibility
			Case ColumnCommandButtonType.Edit
				e.Visible = buttonVisibility
		End Select
	End Sub

	Protected Sub grid_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex < 0 Then
			e.Visible = DefaultBoolean.False
			Return
		End If

		If e.ButtonID = "cbPrint" And Not PrintVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
		If e.ButtonID = "cbPrintAll" And Not PrintVisibleCriteria(CType(sender, ASPxGridView), e.VisibleIndex) Then
			e.Visible = DefaultBoolean.False
		End If
	End Sub

	Private Function PrintVisibleCriteria(ByVal grid As ASPxGridView, ByVal visibleIndex As Integer) As Boolean
		Dim row As Object = grid.GetRow(visibleIndex)
		If IsNothing(row) Then
			Return False
		End If
		Return (CType(row, DataRowView))("LabelTypeID").ToString() = tbLabelTypeID.Value
	End Function

	Protected Sub grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
		If CType(sender, ASPxGridView).IsNewRowEditing Then
			If e.Column.FieldName = "ItemNumber" _
				Or e.Column.FieldName = "UPC" _
				Or e.Column.FieldName = "LabelTypeID" _
				Or e.Column.FieldName = "QtyToPrint" Then
				e.Editor.ReadOnly = False
			Else
				e.Editor.ReadOnly = True
			End If
		End If
	End Sub

	Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		If IsNothing(e.NewValues("ItemNumber")) Then
			e.RowError = "Please select an Item Number"
			Return
		End If
		If e.NewValues("LabelTypeID") = 0 Then
			e.RowError = "Please select a Label Type"
			Return
		End If
		If IsNothing(e.NewValues("QtyToPrint")) Then
			e.RowError = "Please enter a Quantity to Print"
			Return
		End If
	End Sub

	Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		Dim physicalInventoryID As Integer = e.Values("LabelPrintingID")
		dsLabelPrinting.DeleteParameters.Clear()
		dsLabelPrinting.DeleteParameters.Add("LabelPrintingID", physicalInventoryID)
		dsLabelPrinting.DeleteParameters.Add("CurrentUserID", Session("vUserID"))
		dsLabelPrinting.Delete()
		dsLabelPrinting.DataBind()
		e.Cancel = True
	End Sub

	Public Class ItemInfo
		Public ItemNumber As String = ""
		Public Description As String = ""
		Public UPC As String = ""
		Public LabelTypeID As Integer = 0
		Public Price As Decimal = 0
	End Class

	<WebMethod()> _
	Public Shared Function GetItemInfo(ByVal ItemNumber As String, ByVal storeID As Integer) As ItemInfo
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim ii As ItemInfo = New ItemInfo

		sql = "SELECT CASE WHEN LEN(RTRIM(LTRIM(I.LabelDescription))) > 0 " & _
				"THEN I.LabelDescription ELSE I.ItemDescription END AS ItemDescription, " & _
			"I.UPC, I.LabelTypeID, " & _
			"CASE WHEN IL.Price IS NOT NULL THEN IL.Price ELSE I.StandardPrice END AS Price " & _
			"FROM NewMerch.InventoryItems AS I " & _
			"LEFT OUTER JOIN NewMerch.InventoryByLocation AS IL ON IL.ItemNumber = I.ItemNumber " & _
				"AND IL.StoreID = " & storeID.ToString() & " " & _
			"WHERE I.ItemNumber = '" & ItemNumber & "'"


		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			If rsql("ItemDescription").ToString() <> "" Then
				ii.Description = rsql("ItemDescription")
			End If
			If rsql("UPC").ToString() <> "" Then
				ii.UPC = rsql("UPC")
			End If
			If rsql("LabelTypeID").ToString() <> "" Then
				ii.LabelTypeID = rsql("LabelTypeID")
			End If
			If rsql("Price").ToString() <> "" Then
				ii.Price = CDec(rsql("Price"))
			End If
		End If
		SqlQueryClose(conn, rsql)

		Return ii
	End Function

	<WebMethod()> _
	Public Shared Function GetUpcInfo(ByVal upc As String, ByVal storeID As Integer) As ItemInfo
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim ii As ItemInfo = New ItemInfo

		sql = "SELECT I.ItemNumber, I.ItemDescription, I.LabelTypeID, " & _
			"CASE WHEN IL.Price IS NOT NULL THEN IL.Price ELSE I.StandardPrice END AS Price " & _
			"FROM NewMerch.InventoryItems AS I " & _
			"LEFT OUTER JOIN NewMerch.InventoryByLocation AS IL ON IL.ItemNumber = I.ItemNumber " & _
				"AND IL.StoreID = " & storeID.ToString() & " " & _
			"WHERE I.UPC = '" & upc & "'"

		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			If rsql("ItemNumber").ToString() <> "" Then
				ii.ItemNumber = rsql("ItemNumber")
			End If
			If rsql("ItemDescription").ToString() <> "" Then
				ii.Description = rsql("ItemDescription")
			End If
			If rsql("LabelTypeID").ToString() <> "" Then
				ii.LabelTypeID = rsql("LabelTypeID")
			End If
			If rsql("Price").ToString() <> "" Then
				ii.Price = CDec(rsql("Price"))
			End If
		End If
		SqlQueryClose(conn, rsql)

		Return ii
	End Function

	Protected Sub ItemNumber_ItemsRequestedByFilterCondition(source As Object, e As ListEditItemsRequestedByFilterConditionEventArgs)
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		dsInventoryItemsByVendor.SelectCommand = "SELECT ItemNumber AS Item, ItemNumber, ItemDescription " & _
			"FROM (SELECT ItemNumber AS Item, ItemNumber, ItemDescription, VendorID, Active, " & _
			"ROW_NUMBER() OVER(ORDER BY T.ItemNumber) AS [rn] FROM NewMerch.InventoryItems AS T " & _
				"WHERE ((ItemNumber) LIKE @filter) AND Active = 1) AS ST " & _
					"WHERE ST.[rn] BETWEEN @startIndex and @endIndex "

		Dim endIndex = e.EndIndex + 1
		If e.Filter = "" Then
			endIndex = e.BeginIndex + 100
		End If

		dsInventoryItemsByVendor.SelectParameters.Clear()
		dsInventoryItemsByVendor.SelectParameters.Add("filter", TypeCode.String, String.Format("{0}%", e.Filter))
		dsInventoryItemsByVendor.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString())
		dsInventoryItemsByVendor.SelectParameters.Add("endIndex", TypeCode.Int64, endIndex.ToString())
		comboBox.DataSource = dsInventoryItemsByVendor
		comboBox.DataBind()
	End Sub

	Protected Sub ItemNumber_ItemRequestedByValue(source As Object, e As ListEditItemRequestedByValueEventArgs)
		Dim value As Long = 0
		If e.Value Is Nothing OrElse (Not Int64.TryParse(e.Value.ToString(), value)) Then
			Return
		End If
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		dsInventoryItemsByVendor.SelectCommand = "SELECT ItemNumber AS Item, ItemNumber, ItemDescription " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE ItemNumber = @ItemNumber " & _
			"ORDER BY ItemNumber"

		dsInventoryItemsByVendor.SelectParameters.Clear()
		dsInventoryItemsByVendor.SelectParameters.Add("ItemNumber", TypeCode.String, e.Value.ToString())
		comboBox.DataSource = dsInventoryItemsByVendor
		comboBox.DataBind()
	End Sub

	Protected Sub Upc_ItemRequestedByFilterCondition(source As Object, e As ListEditItemsRequestedByFilterConditionEventArgs)
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		dsInventoryItemsByVendor.SelectCommand = "SELECT UPC AS Item, UPC, ItemDescription " & _
			"FROM (SELECT UPC AS Item, UPC, ItemDescription, VendorID, Active, " & _
			"ROW_NUMBER() OVER(ORDER BY T.UPC) AS [rn] FROM NewMerch.InventoryItems AS T " & _
				"WHERE ((UPC) LIKE @filter) AND Active = 1) AS ST " & _
					"WHERE ST.[rn] BETWEEN @startIndex and @endIndex "

		Dim endIndex = e.EndIndex + 1
		If e.Filter = "" Then
			endIndex = e.BeginIndex + 100
		End If

		dsInventoryItemsByVendor.SelectParameters.Clear()
		dsInventoryItemsByVendor.SelectParameters.Add("filter", TypeCode.String, String.Format("{0}%", e.Filter))
		dsInventoryItemsByVendor.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString())
		dsInventoryItemsByVendor.SelectParameters.Add("endIndex", TypeCode.Int64, endIndex.ToString())
		comboBox.DataSource = dsInventoryItemsByVendor
		comboBox.DataBind()
	End Sub

	Protected Sub Upc_ItemRequestedByValue(source As Object, e As ListEditItemRequestedByValueEventArgs)
		Dim value As Long = 0
		If e.Value Is Nothing OrElse (Not Int64.TryParse(e.Value.ToString(), value)) Then
			Return
		End If
		Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)
		dsInventoryItemsByVendor.SelectCommand = "SELECT UPC AS Item, UPC, ItemDescription " & _
			"FROM NewMerch.InventoryItems " & _
			"WHERE UPC = @upc " & _
			"ORDER BY UPC"

		dsInventoryItemsByVendor.SelectParameters.Clear()
		dsInventoryItemsByVendor.SelectParameters.Add("UPC", TypeCode.String, e.Value.ToString())
		comboBox.DataSource = dsInventoryItemsByVendor
		comboBox.DataBind()
	End Sub

	<WebMethod>
	Public Shared Function GetCertificate() As String
		Dim cert As String = File.ReadAllText(HttpContext.Current.Server.MapPath("digital-certificate.txt"))
		Return cert
	End Function

	<WebMethod>
	Public Shared Function SignMessage(ByVal message As String) As String

		'**********************************************************
		'*           WARNING   WARNING   WARNING                  *
		'**********************************************************													*
		'*                                                        *
		'* This file is intended for demonstration purposes only. *
		'* only.                                                  *
		'*                                                        *
		'* It is the SOLE responsibility of YOU, the programmer   *
		'* to prevent against unauthorized access to any signing  *
		'* functions.                                             *
		'*                                                        *
		'* Organizations that do not protect against un-          *
		'* authorized signing will be black-listed to prevent     *
		'* software piracy.                                       *
		'*                                                        *
		'* -QZ Industries, LLC                                    *
		'*                                                        *
		'**********************************************************

		' Sample key.  Replace with one used for CSR generation
		' How to associate a private key with the X509Certificate2 class in .net
		' On my personal machine, use the openssl in C:\OpenSSL-Win32
		' openssl pkcs12 -export -inkey private-key.pem -in digital-certificate.txt -out private-key.pfx
		' No Password

		Dim KEY As String = HttpContext.Current.Server.MapPath("private-key.pfx")
		Dim PASS As String = ""
		Dim cert As X509Certificate2 = New X509Certificate2(KEY, PASS, X509KeyStorageFlags.MachineKeySet Or X509KeyStorageFlags.PersistKeySet Or X509KeyStorageFlags.Exportable)
		Dim csp As RSACryptoServiceProvider = CType(cert.PrivateKey, RSACryptoServiceProvider)

		Dim data As Byte() = New ASCIIEncoding().GetBytes(message)
		Dim hash As Byte() = New SHA1Managed().ComputeHash(data)

		Dim response As String = Convert.ToBase64String(csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1")))
		Return response
	End Function

	<WebMethod>
	Public Shared Function PrintLabels(ByVal storeID As Integer, ByVal itemNumber As String, _
									   ByVal labelTypeID As Integer, ByVal qtyToPrint As Integer) As String
		Dim price As String = ""
		Dim curDate As String = Format(Now(), "yyyyMMdd")
		Dim itemDescription As String = ""
		Dim upc As String = ""
		Dim department As String = ""
		Dim subDepartment As String = ""
		Dim zpl As String = ""

		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)

		sql = "SELECT COALESCE(II.LabelDescription, II.ItemDescription) AS ItemDescription, " & _
			"CASE WHEN IL.Price IS NOT NULL THEN IL.Price ELSE II.StandardPrice END AS Price, " & _
			"II.UPC, D.DepartmentName, SD.SubDepartmentName, LT.ZPL " & _
			"FROM NewMerch.InventoryItems AS II " & _
			"INNER JOIN SysConfig.Departments AS D ON D.DepartmentID = II.DepartmentID " & _
			"INNER JOIN SysConfig.SubDepartments AS SD ON SD.SubDepartmentID = II.SubDepartmentID " & _
			"INNER JOIN NewMerch.LabelTypes AS LT ON LT.LabelTypeID = " & labelTypeID & " " & _
			"LEFT OUTER JOIN NewMerch.InventoryByLocation AS IL ON IL.ItemNumber = II.ItemNumber " & _
				"AND IL.StoreID = " & storeID.ToString() & " " & _
			"WHERE II.ItemNumber = '" & itemNumber & "'"

		SqlQueryOpen(conn, rsql, sql, "")
		If rsql.Read() Then
			If rsql("ItemDescription").ToString() <> "" Then
				itemDescription = rsql("ItemDescription")
			End If
			If rsql("UPC").ToString() <> "" Then
				upc = rsql("UPC")
			End If
			If rsql("Price").ToString() <> "" Then
				price = CDec(rsql("Price")).ToString("F2")
			End If
			If rsql("DepartmentName").ToString() <> "" Then
				department = rsql("DepartmentName")
			End If
			If rsql("SubDepartmentName").ToString() <> "" Then
				subDepartment = rsql("SubDepartmentName")
			End If
			If rsql("ZPL").ToString() <> "" Then
				zpl = rsql("ZPL")
			End If
		End If
		SqlQueryClose(conn, rsql)

		' If Item Description has [s], change it to just s.
		Dim i As Integer = InStr(itemDescription, "[")
		Dim j As Integer = InStr(itemDescription, "]")
		If i > 0 And j > 0 And j > i Then
			itemDescription = Mid(itemDescription, i + 1, j - i - 1)
		End If

		' If Item Description is too long, shorten it.
		'If Len(itemDescription) > 10 Then
		'	itemDescription = Mid(itemDescription, 1, 10)
		'End If

		Dim barCodeLenOperator As String = "BU"
		If Len(upc) = 13 Then
			barCodeLenOperator = "BE"
		End If

		' Replace Fields in ZPL
		zpl = Replace(zpl, "[PRICE]", price)
		zpl = Replace(zpl, "[DATE]", curDate)
		zpl = Replace(zpl, "[ITEMDESCRIPTION]", itemDescription)
		zpl = Replace(zpl, "[UPC]", upc)
		zpl = Replace(zpl, "[ITEMNUMBER]", itemNumber)
		zpl = Replace(zpl, "[DEPARTMENT]", department)
		zpl = Replace(zpl, "[SUBDEPARTMENT]", subDepartment)
		zpl = Replace(zpl, "[QTY]", qtyToPrint)
		zpl = Replace(zpl, "[BARCODELENOPERATOR]", barCodeLenOperator)

		Return (zpl)
	End Function

	<WebMethod()> _
	Public Shared Function UpdateToPrinted(ByVal labelPrintingID As Integer, ByVal userID As Integer) As String
		Dim conn As SqlConnection = New SqlConnection(NewMerchConnStr)
		Dim cmd As SqlCommand = New SqlCommand()
		cmd.Connection = conn
		cmd.CommandText = "NewMerch.spLabelPrinting_UpdateToPrinted"
		cmd.CommandType = System.Data.CommandType.StoredProcedure

		cmd.Parameters.Add(DataUtil.CreateParameter("@labelPrintingID", System.Data.ParameterDirection.Input, DbType.Int32, labelPrintingID))
		cmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, userID))
		Dim errorID As Integer = 0
		cmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			cmd.ExecuteNonQuery()
			errorID = cmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "NMLabelPrinting, UpdateToPrinted")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			Return (ex.Message)
		Finally
			conn.Close()
		End Try

		Return ""
	End Function

End Class
