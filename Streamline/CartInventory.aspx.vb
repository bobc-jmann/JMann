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

Partial Class CartInventory
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Dim PostBackControlID As String = ""
		Select Case Request.HttpMethod
			Case "POST"
				If Not IsPostBack Then
					dsGridMain.DataBind()
				End If
			Case "GET"
				dtInventoryDate.Value = DateAdd("d", -1, Today)

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

		''''''''''''''''''''''''''''''''''''''
		If dtInventoryDate.Value < "9/8/2015" Then
			dtInventoryDate.Value = "9/8/2015"
		End If
		''''''''''''''''''''''''''''''''''''''

		gridTransfers.DataBind()
	End Sub

	Protected Sub gridMain_OnHtmlDataCellPrepared(ByVal g As ASPxGridView, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
		If e.DataColumn.FieldName = "SoftCarts" Or e.DataColumn.FieldName = "HardCarts" Or e.DataColumn.FieldName = "TotalCarts" Or e.DataColumn.FieldName = "EmptyCarts" Then
			Dim lineTypeID As Integer = CInt(g.GetRowValues(e.VisibleIndex, "LineTypeID"))
			Select Case lineTypeID
				Case 1, 9, 11
					e.Cell.ForeColor = Color.DarkBlue
				Case 2, 3, 4, 5, 12
					e.Cell.ForeColor = Color.Green
				Case 6, 7, 8
					e.Cell.ForeColor = Color.Purple
				Case 10
					e.Cell.ForeColor = Color.Red
			End Select
		End If
	End Sub

	Protected Sub gridMain_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
		If e.VisibleIndex < 0 Then
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

		Dim lineTypeID As Integer = CInt(gridMain.GetRowValues(visibleIndex, "LineTypeID").ToString())
		Dim transferID As String = gridMain.GetRowValues(visibleIndex, "TransferID").ToString()
		Select Case lineTypeID
			Case 4, 5, 8, 10, 13
				Return True
			Case 7
				If buttonType = "Edit" Then
					Return True
				End If
				If buttonType = "Delete" And transferID = "" Then
					Return True
				End If
		End Select
		Return False
	End Function

	Protected Sub gridMain_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex < 0 Then
			Return
		End If

		Dim visible As Boolean

		Select Case e.ButtonID
			Case "cbApprove"
				visible = CustomCommandButtonVisibleCriteria(e.VisibleIndex, "Approve")
				gridTransfers.DataBind()
			Case "cbUnapprove"
				visible = CustomCommandButtonVisibleCriteria(e.VisibleIndex, "Unapprove")
				gridTransfers.DataBind()
		End Select

		If visible Then
			e.Visible = DefaultBoolean.True
		Else
			e.Visible = DefaultBoolean.False
		End If
	End Sub

	Private Function CustomCommandButtonVisibleCriteria(ByVal visibleIndex As Integer, ByVal buttonType As String) As Boolean
		Dim specialUser As Boolean = False
		If Session("vUserGroup") = "ADMIN" Or _
				Session("vUserGroup") = "MANAGEMENT" Then
			specialUser = True
		End If

		Dim sql As String
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim lineTypeID As Integer = CInt(gridMain.GetRowValues(visibleIndex, "LineTypeID").ToString())
		Dim lastEditedBy As String = gridMain.GetRowValues(visibleIndex, "LastEditedBy").ToString()
		Dim locationID As Integer = CInt(gridMain.GetRowValues(visibleIndex, "LocationID").ToString())
		Dim inventoryDate As Date = dtInventoryDate.Value
		Dim cnt As Integer = 0

		If lineTypeID = 11 Then
			If lastEditedBy = "" And buttonType = "Approve" Then
				' If any dates in the last seven days have not been approved, do not allow approve.
				sql = "SELECT COUNT(*) AS Cnt " & _
						"FROM Carts.Inventory " & _
						"WHERE LocationID = " & locationID & " " & _
							"AND InventoryDate BETWEEN '" & DateAdd("d", -7, Format(inventoryDate, "MM/dd/yyyy")) & "' " & _
								"AND '" & DateAdd("d", -1, Format(inventoryDate, "MM/dd/yyyy")) & "' " & _
							"AND LineTypeID = 11 " & _
							"AND LastEditedByID IS NULL"
				If Not SqlQueryOpen(connSQL, rsql, sql, "Notify Web User") Then
					Return False
				End If

				If rsql.Read() Then
					cnt = CInt(rsql("Cnt").ToString)
				End If
				SqlQueryClose(connSQL, rsql)
				If cnt > 0 Then
					Return False
				End If
				Return True
			ElseIf lastEditedBy <> "" And buttonType = "Unapprove" Then
				' If any dates > this date have been approved, do not allow unapprove.
				Dim approved As Boolean = IsInventoryApproved(DateAdd("d", 1, inventoryDate), locationID, )
				If Not approved Or specialUser = True Then
					Return True
				End If
			End If
		End If
		Return False
	End Function

	Protected Sub gridMain_CustomButtonBallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles gridMain.CustomButtonCallback
		If e.ButtonID = "cbApprove" Or e.ButtonID = "cbUnapprove" Then
			Dim approve As Boolean = True
			If e.ButtonID = "cbUnapprove" Then
				approve = False
			End If

			Dim inventoryID As Integer = CInt(gridMain.GetRowValues(e.VisibleIndex, "InventoryID"))

			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spCartInventory_Approve"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@inventoryID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int, inventoryID))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@approve", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit, approve))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@currentUserID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, Session("vUserID")))
			Dim errorID As Integer = 0
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			Try
				conn.Open()
				myCmd.ExecuteNonQuery()
				errorID = myCmd.Parameters("@RETURN_VALUE").Value
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "")
			Finally
				conn.Close()
			End Try

			gridMain.DataBind()
		End If
	End Sub

	Protected Sub gridMain_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
		Dim lineTypeID As Integer = 0
		If e.VisibleIndex > 0 Then
			lineTypeID = gridMain.GetRowValues(e.VisibleIndex, "LineTypeID")
		End If

		If e.Column.FieldName = "LineTypeID" Then
			Dim sql As String = "SELECT LineTypeID, LineTypeDesc " & _
				"FROM Carts.LineTypes "
			If lineTypeID = 0 Then
				sql += "WHERE LineTypeID IN (5, 7, 8, 10, 13) "
			End If
			sql += "ORDER BY SortCode"
			dsLineType.SelectCommand = sql
			dsLineType.DataBind()
		End If

		Dim lineTypes = New List(Of Integer) From {4, 5, 6, 8, 10}
		If e.Column.FieldName = "CharityID" And lineTypes.Contains(lineTypeID) Then
			e.Editor.ClientEnabled = False
			e.Editor.ReadOnly = True
			Return
		End If

		lineTypes = New List(Of Integer) From {4, 5, 6, 10}
		If e.Column.FieldName = "SecondaryLocationID" And lineTypes.Contains(lineTypeID) Then
			e.Editor.ClientEnabled = False
			e.Editor.ReadOnly = True
			Return
		End If

		lineTypes = New List(Of Integer) From {0, 4, 10}
		If e.Column.FieldName = "EditReasonID" And Not lineTypes.Contains(lineTypeID) Then
			e.Editor.ClientEnabled = False
			e.Editor.ReadOnly = True
			Return
		End If

		e.Editor.ClientEnabled = True
		e.Editor.ReadOnly = False
	End Sub

	Protected Sub gridMain_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles gridMain.RowValidating
		If IsNothing(e.NewValues("LineTypeID")) Then
			e.RowError = "Please select a Line Type."
			Return
		End If

		If Not e.IsNewRow And e.OldValues("LineTypeID") <> e.NewValues("LineTypeID") Then
			e.RowError = "You cannot change the Line Type while editing."
			Return
		End If

		If e.NewValues("LineTypeID") = 4 And (IsNothing(e.NewValues("SecondaryLocationID")) Or e.NewValues("SecondaryLocationID") = 0) Then
			e.RowError = "Please enter a Location from which the transfer was received."
			Return
		End If

		Dim lineTypes = New List(Of Integer) From {4, 10, 13}
		If Not lineTypes.Contains(e.NewValues("LineTypeID")) And _
			Not (IsNothing(e.NewValues("EditReasonID")) Or e.NewValues("EditReasonID") = 0) Then
			e.RowError = "Please do not enter an Edit Reason for this Line Type."
			Return
		End If

		If e.NewValues("LineTypeID") = 5 And (IsNothing(e.NewValues("SecondaryLocationID")) Or e.NewValues("SecondaryLocationID") = 0) Then
			e.RowError = "Please enter a Location from which the purchase was received."
			Return
		End If
		If e.NewValues("LineTypeID") = 5 And Not IsNothing(e.NewValues("CharityID")) And e.NewValues("CharityID") > 0 Then
			e.RowError = "Please do not enter a Charity for the Purchase Received."
			Return
		End If
		If e.NewValues("LineTypeID") = 7 And (IsNothing(e.NewValues("SecondaryLocationID")) Or e.NewValues("SecondaryLocationID") = 0) Then
			e.RowError = "Please enter a Location to which the transfer was shipped."
			Return
		End If
		'If e.NewValues("LineTypeID") = 7 And (IsNothing(e.NewValues("CharityID")) Or e.NewValues("CharityID") = 0) Then
		'	e.RowError = "Please enter a Charity for the Transfer Shipped."
		'	Return
		'End If
		If e.NewValues("LineTypeID") = 8 And (IsNothing(e.NewValues("SecondaryLocationID")) Or e.NewValues("SecondaryLocationID") = 0) Then
			e.RowError = "Please enter a Location to which the transfer was shipped."
			Return
		End If
		If e.NewValues("LineTypeID") = 8 And Not IsNothing(e.NewValues("CharityID")) And e.NewValues("CharityID") > 0 Then
			e.RowError = "Please do not enter a Charity for a Transfer Sold."
			Return
		End If
		If e.NewValues("LineTypeID") = 10 And (Not IsNothing(e.NewValues("SecondaryLocationID")) And e.NewValues("SecondaryLocationID") > 0) Then
			e.RowError = "Please do not enter a Location for Adjustments."
			Return
		End If
		If e.NewValues("LineTypeID") = 10 And Not IsNothing(e.NewValues("CharityID")) And e.NewValues("CharityID") > 0 Then
			e.RowError = "Please do not enter a Charity for Adjustments."
			Return
		End If
		If e.NewValues("LineTypeID") = 13 And (Not IsNothing(e.NewValues("SecondaryLocationID")) And e.NewValues("SecondaryLocationID") > 0) Then
			e.RowError = "Please do not enter a Location for Empty Carts."
			Return
		End If
		If e.NewValues("LineTypeID") = 13 And Not IsNothing(e.NewValues("CharityID")) And e.NewValues("CharityID") > 0 Then
			e.RowError = "Please do not enter a Charity for Empty Carts."
			Return
		End If
		If e.NewValues("LineTypeID") = 13 And Not IsNothing(e.NewValues("HardCarts")) And e.NewValues("HardCarts") <> 0 Then
			e.RowError = "Please do not enter Hard Carts for Empty Carts."
			Return
		End If
		If e.NewValues("LineTypeID") = 13 And Not IsNothing(e.NewValues("SoftCarts")) And e.NewValues("SoftCarts") <> 0 Then
			e.RowError = "Please do not enter Soft Carts for Empty Carts."
			Return
		End If

		Dim outsideLocation As Boolean = False
		Select Case e.NewValues("LineTypeID")
			Case 4, 5, 7, 8
				Dim sql As String
				Dim rsql As SqlDataReader = Nothing
				Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
				sql = "SELECT OutsideLocation FROM tblLocations	WHERE LocationID = " & e.NewValues("SecondaryLocationID")

				If Not SqlQueryOpen(connSQL, rsql, sql, "Notify Web User") Then
					e.RowError = "Error reading Cart Locations"
					Return
				End If

				If rsql.Read() Then
					outsideLocation = CBool(rsql("OutsideLocation").ToString)
				End If
				SqlQueryClose(connSQL, rsql)
		End Select

		Select Case e.NewValues("LineTypeID")
			Case 4
				If outsideLocation Then
					e.RowError = "Transfers Received require a Location within the company."
					Return
				End If
			Case 5
				If Not outsideLocation Then
					e.RowError = "Purchases Received require a Location outside the company."
					Return
				End If
			Case 7
				If outsideLocation Then
					e.RowError = "Transfers Shipped require a Location within the company."
					Return
				End If
			Case 8
				If Not outsideLocation Then
					e.RowError = "Transfers Sold require a Location outside the company."
					Return
				End If
		End Select


		If e.NewValues("LineTypeID") = 4 And (IsNothing(e.NewValues("EditReasonID")) Or e.NewValues("EditReasonID") = 0) Then
			e.RowError = "Please select a reason for the adjustment."
			Return
		End If
		If e.NewValues("LineTypeID") = 10 And (IsNothing(e.NewValues("EditReasonID")) Or e.NewValues("EditReasonID") = 0) Then
			e.RowError = "Please select a reason for the adjustment."
			Return
		End If

		If e.NewValues("LineTypeID") = 7 And _
			Not e.IsNewRow And Not IsDBNull(gridMain.GetRowValues(e.VisibleIndex, "TransferID")) Then
			If e.OldValues("SoftCarts") <> e.NewValues("SoftCarts") Then
				e.RowError = "This shipment has been received and Soft Carts cannot be changed."
			End If
			If e.OldValues("HardCarts") <> e.NewValues("HardCarts") Then
				e.RowError = "This shipment has been received and Hard Carts cannot be changed."
			End If
			If e.OldValues("SecondaryLocationID") <> e.NewValues("SecondaryLocationID") Then
				e.RowError = "This shipment has been received and the Receiving Location cannot be changed."
			End If
		End If

	End Sub

	Protected Sub gridTransfers_CustomButtonInitialize(sender As Object, e As ASPxGridViewCustomButtonEventArgs)
		If e.VisibleIndex = -1 Or IsNothing(gridMain.GetRowValues(0, "Approved")) Then
			Return
		End If

		Dim approved As Boolean = CBool(gridMain.GetRowValues(0, "Approved").ToString())
		If approved Then
			e.Visible = DefaultBoolean.False
		Else
			e.Visible = DefaultBoolean.True
		End If
	End Sub

	Protected Sub gridTransfers_CustomButtonBallback(sender As Object, e As ASPxGridViewCustomButtonCallbackEventArgs) Handles gridTransfers.CustomButtonCallback
		If e.ButtonID = "cbReceive" Then
			Dim inventoryID As Integer = CInt(gridTransfers.GetRowValues(e.VisibleIndex, "InventoryID"))
			Dim softCarts As Decimal = CDec(gridTransfers.GetRowValues(e.VisibleIndex, "SoftCarts"))
			Dim hardCarts As Decimal = CDec(gridTransfers.GetRowValues(e.VisibleIndex, "HardCarts"))
			Dim emptyCarts As Decimal = CDec(gridTransfers.GetRowValues(e.VisibleIndex, "EmptyCarts"))
			Dim secondaryLocationID As Integer = CInt(gridTransfers.GetRowValues(e.VisibleIndex, "LocationID"))
			Dim notes As String = TryCast(gridTransfers.GetRowValues(e.VisibleIndex, "Notes"), String)
			Dim editReasonID As Integer = 0
			If Len(notes) > 0 Then
				editReasonID = 11 ' Note Added
			End If
			'Dim charityID As Integer = CInt(gridTransfers.GetRowValues(e.VisibleIndex, "CharityID"))

			Dim conn As SqlConnection = New SqlConnection(vConnStr)
			Dim myCmd As SqlCommand = New SqlCommand()
			Dim token As System.Guid = System.Guid.Empty
			myCmd.Connection = conn
			myCmd.CommandText = "spCartInventory_Receive"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@inventoryID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int, inventoryID))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@locationID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int, ddlLocations.SelectedValue))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@inventoryDate", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Date, dtInventoryDate.Value))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@softCarts", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal, softCarts))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@hardCarts", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal, hardCarts))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@emptyCarts", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal, emptyCarts))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@secondaryLocationID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int, secondaryLocationID))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@editReasonID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int, editReasonID))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@notes", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, notes, 255))
			'myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@charityID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int, charityID))
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@currentUserID", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, Session("vUserID")))
			Dim errorID As Integer = 0
			myCmd.Parameters.Add(DataUtil.CreateParameterSQL("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

			Try
				conn.Open()
				myCmd.ExecuteNonQuery()
				errorID = myCmd.Parameters("@RETURN_VALUE").Value
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "")
			Finally
				conn.Close()
			End Try

			gridMain.DataBind()
			gridTransfers.DataBind()
		End If
	End Sub

	Protected Sub cmdCartInventory_Click(sender As Object, e As System.EventArgs) Handles cmdCartInventory.Click
		Dim parms As String = "&PARMS=inventoryDate~" & dtInventoryDate.Value
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Cart Inventory" & parms)
	End Sub

	Protected Sub cmdInterRegionTransfers_Click(sender As Object, e As System.EventArgs) Handles cmdInterRegionTransfers.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Inter-Region Cart Transfers")
	End Sub
End Class
