Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Web.Services
Imports DataUtil

Partial Class Containers
    Inherits System.Web.UI.Page
    Private UnselectDate As Boolean = False
    Private MonthChanged As Boolean = False

    Private gridPageSize As Integer = 5
    Private gridPageSize2 As Integer = 15

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Dim s As String = ""
		If (calSpecials.SelectedDate = "1/1/0001 12:00:00 AM") Then calSpecials.SelectedDate = Today() : calSpecials_SelectionChanged(s, e)

        txtSQL_ddlCharities.Text = "SELECT * FROM tblCharities WHERE Active = 1 ORDER BY CharityAbbr"
        txtSQL_ddlLocations.Text = "SELECT * FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc"

        Select Case Request.HttpMethod
            Case "POST"
                If IsPostBack Then
                    Dim PostBackControlID As String = ""
                    Try
						PostBackControlID = GetPostBackControl(Me.Page).ID
                    Catch ex As Exception

                    End Try
                    Select Case PostBackControlID
                        Case "calSpecials"
                            'do this to trigger selectionchanged event; note that it will not actualy clear anything
                            calSpecials.SelectedDates.Clear()
                        Case "cmdSave"

                        Case "grdSpecials"
                            ddlCharity.Items.Clear()
                        Case "grdAddresses"
                            ddlCharity.Items.Clear()
                        Case "grdNextPickups"
                            ddlCharity.Items.Clear()
                        Case "btnScheduleDetailSave"
                            dsDrivers.SelectCommand = hfDrivers.Value
                            ddlDrivers.DataBind()
                        Case Else
                    End Select
                    Try
                        With sqlCharities
                            .ConnectionString = vConnStr
                            .SelectCommand = txtSQL_ddlCharities.Text
                        End With
                        Dim charityID As String = ddlCharity.Text
                        Dim charityAbbr As String = ""
                        If Not IsNothing(ddlCharity.SelectedItem) Then
                            charityAbbr = ddlCharity.SelectedItem.Text
                        End If
                        ddlCharity.DataBind()
                        SetCharity(charityID, charityAbbr)
                        With sqlSpecials
                            .ConnectionString = vConnStr
                            .SelectCommand = txtSQL_grdSpecials.Text
                        End With
                        With sqlAddresses
                            .ConnectionString = vConnStr
                            .SelectCommand = txtSQL_grdAddresses.Text
                        End With
                        With sqlNextPickups
                            .ConnectionString = vConnStr
                            .SelectCommand = txtSQL_grdNextPickups.Text
                        End With
                    Catch ex As Exception
                        LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
                    End Try
                End If
            Case "GET"
                Try
                    With sqlCharities
                        .ConnectionString = vConnStr
                        .SelectCommand = txtSQL_ddlCharities.Text
                    End With
                    With sqlLocations
                        .ConnectionString = vConnStr
                        .SelectCommand = txtSQL_ddlLocations.Text
                    End With
                    ddlLocation.DataBind()
                    ddlLocations.DataBind()
                    With sqlSpecials
                        .ConnectionString = vConnStr
                        .SelectCommand = txtSQL_grdSpecials.Text
                    End With
                    With sqlAddresses
                        .ConnectionString = vConnStr
                        .SelectCommand = txtSQL_grdAddresses.Text
                    End With
                    With sqlNextPickups
                        .ConnectionString = vConnStr
                        .SelectCommand = txtSQL_grdNextPickups.Text
                    End With
                Catch ex As Exception
                    LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
                End Try

                If Session("UserRegionDefault") <> 0 Then
                    ddlLocations.SelectedValue = Session("UserRegionDefault")
                    ddlLocation.SelectedValue = Session("UserRegionDefault")
                End If

                cmdSaveDisplay("disabled")
                qryRouteSection.Text = ""
                qryNextScheduledPickup.Text = ""
                qryResults.Text = ""

            Case "HEAD"

        End Select

    End Sub
    Private Sub PopulateSpecialsGrid()
        grdSpecials.AutoGenerateColumns = False
        If grdSpecials.AutoGenerateColumns = False Then
            grdSpecials.Columns.Clear()
            Dim colAddress As New ButtonField
            With colAddress
                .DataTextField = "Address"
                .HeaderText = "Address"
                .ButtonType = ButtonType.Link
                .SortExpression = "Address"
                .CommandName = "sysrowSelector"
            End With
            grdSpecials.Columns.Add(colAddress)
            Dim OneField As BoundField
            OneField = New BoundField
            With OneField
                .SortExpression = "PickupDateDisplay"
                .DataField = "PickupDateDisplay"
                .HeaderText = "Pickup Date"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "City"
                .DataField = "City"
                .HeaderText = "City"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "ZIP"
                .DataField = "ZIP"
                .HeaderText = "ZIP"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Right
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "PhoneWork"
                .DataField = "PhoneWork"
                .HeaderText = "PhoneWork"
                .Visible = False
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "PhoneHome"
                .DataField = "PhoneHome"
                .HeaderText = "PhoneHome"
                .Visible = False
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "DriverLocation"
                .DataField = "DriverLocation"
                .HeaderText = "Driver Location"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "CharityAbbr"
                .DataField = "CharityAbbr"
                .HeaderText = "Charity"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "Grid"
                .DataField = "Grid"
                .HeaderText = "Grid"
                .Visible = False
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "PickupDate"
                .DataField = "PickupDate"
                .HeaderText = "Pickup Date"
                .Visible = False
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "PickupID"
                .DataField = "PickupID"
                .HeaderText = "PickupID"
                .Visible = False
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdSpecials.Columns.Add(OneField)
        End If
    End Sub

    Protected Sub grdSpecials_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim row As GridViewRow = New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal)
            Dim hdr As TableHeaderCell = New TableHeaderCell()
            hdr.ColumnSpan = grdSpecials.Columns.Count
            hdr.Text = "Container Pickups"
            row.Cells.Add(hdr)
            grdSpecials.Controls(0).Controls.AddAt(0, row)
        End If
    End Sub

    Private Sub PopulateAddressesGrid()
        grdAddresses.AutoGenerateColumns = False
        If grdAddresses.AutoGenerateColumns = False Then
            grdAddresses.Columns.Clear()
            Dim colAddress As New ButtonField
            With colAddress
                .DataTextField = "Address"
                .HeaderText = "Address"
                .ButtonType = ButtonType.Link
                .SortExpression = "Address"
                .CommandName = "sysrowSelector"
            End With
            grdAddresses.Columns.Add(colAddress)
            Dim OneField As BoundField
            OneField = New BoundField
            With OneField
                .SortExpression = "City"
                .DataField = "City"
                .HeaderText = "City"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdAddresses.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "ZIP"
                .DataField = "ZIP"
                .HeaderText = "ZIP"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Right
            End With
            grdAddresses.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "Email"
                .DataField = "Email"
                .HeaderText = "Email"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdAddresses.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "Grid"
                .DataField = "Grid"
                .HeaderText = "Grid"
                .Visible = False
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdAddresses.Columns.Add(OneField)

        End If
    End Sub

    Protected Sub grdAddresses_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim row As GridViewRow = New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal)
            Dim hdr As TableHeaderCell = New TableHeaderCell()
            hdr.ColumnSpan = grdSpecials.Columns.Count
            hdr.Text = "Container Addresses that match your last search"
            row.Cells.Add(hdr)
            grdAddresses.Controls(0).Controls.AddAt(0, row)
        End If
    End Sub

    Private Sub PopulateNextPickupsGrid()
        grdNextPickups.AutoGenerateColumns = False
        If grdNextPickups.AutoGenerateColumns = False Then
            grdNextPickups.Columns.Clear()
            Dim colStreetName As New ButtonField
            With colStreetName
                .DataTextField = "StreetName"
                .HeaderText = "Street Name"
                .ButtonType = ButtonType.Link
                .SortExpression = "StreetName"
                .CommandName = "sysrowSelector"
            End With
            grdNextPickups.Columns.Add(colStreetName)
            Dim OneField As BoundField
            OneField = New BoundField
            With OneField
                .SortExpression = "City"
                .DataField = "City"
                .HeaderText = "City"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdNextPickups.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "ZIP"
                .DataField = "ZIP"
                .HeaderText = "ZIP"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Right
            End With
            grdNextPickups.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "PickupDate"
                .DataField = "PickupDate"
                .HeaderText = "PickupDate"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdNextPickups.Columns.Add(OneField)
            OneField = New BoundField
            With OneField
                .SortExpression = "RouteSection"
                .DataField = "RouteSection"
                .HeaderText = "Route-Section"
                .Visible = True
                .ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End With
            grdNextPickups.Columns.Add(OneField)

        End If
    End Sub

    Protected Sub grdNextPickups_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim row As GridViewRow = New GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal)
            Dim hdr As TableHeaderCell = New TableHeaderCell()
            hdr.ColumnSpan = grdSpecials.Columns.Count
            hdr.Text = "Scheduled Pickups on the Street Name in your last search"
            row.Cells.Add(hdr)
            grdNextPickups.Controls(0).Controls.AddAt(0, row)
        End If
    End Sub

    Private Sub PrepareSpecialsResults(ByVal PageSize As Integer)
        Try
            With grdSpecials
                .PageSize = PageSize
                .DataSourceID = "sqlSpecials"
                .DataBind()     'force the bind now
                .Visible = True
            End With
            Dim myStyle As New Style
            With myStyle
                .Font.Size = 4  'does not stick since it is assigned (nonblank) on page
                .ForeColor = Drawing.Color.Black
            End With
            grdSpecials.ApplyStyle(myStyle)
        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
        End Try
    End Sub

    Private Sub PrepareAddressesResults(ByVal PageSize As Integer)
        Try
            With grdAddresses
                .PageSize = PageSize
                .DataSourceID = "sqlAddresses"
                .DataBind()     'force the bind now
                .Visible = True
            End With
            Dim myStyle As New Style
            With myStyle
                .Font.Size = 4  'does not stick since it is assigned (nonblank) on page
                .ForeColor = Drawing.Color.Black
            End With
            grdAddresses.ApplyStyle(myStyle)
        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
        End Try
    End Sub

    Private Sub PrepareNextPickupsResults(ByVal PageSize As Integer)
        Try
            With grdNextPickups
                .PageSize = 15
                .DataSourceID = "sqlNextPickups"
                .DataBind()     'force the bind now
                .Visible = True
            End With
            Dim myStyle As New Style
            With myStyle
                .Font.Size = 4  'does not stick since it is assigned (nonblank) on page
                .ForeColor = Drawing.Color.Black
            End With
            grdNextPickups.ApplyStyle(myStyle)
        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
        End Try
    End Sub

    Protected Sub sqlSpecials_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles sqlSpecials.Selected
        txtSpecialsRows.Text = e.AffectedRows.ToString
    End Sub

    Protected Sub sqlAddresses_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles sqlAddresses.Selected
        txtAddressesRows.Text = e.AffectedRows.ToString
    End Sub

    Protected Sub sqlNextPickups_Selected(sender As Object, e As SqlDataSourceStatusEventArgs) Handles sqlNextPickups.Selected
        txtNextPickupsRows.Text = e.AffectedRows.ToString
    End Sub

    Private Sub PrepareQryResultsDisplay()
        qryResults.Visible = True
        qryResults.Text = txtSpecialsRows.Text & " pickup" & IIf(txtSpecialsRows.Text = "1", "", "s") & " found"
        If Not ckSpecialsOnly.Checked Then
            qryResults.Text &= "; " & txtAddressesRows.Text & " address" & IIf(txtAddressesRows.Text = "1", "", "es") & " found"
        End If
        qryResults.Text &= "."

        If txtSpecialsRows.Text <> "" AndAlso CInt(txtSpecialsRows.Text) > 0 Then
            grdSpecials.Visible = True
            grdAddresses.Visible = False
            If txtAddressesRows.Text <> "" AndAlso CInt(txtAddressesRows.Text) > 0 Then
                cmdShowGrid.Text = "Show Addresses"
                cmdShowGrid.Visible = True
            Else
                cmdShowGrid.Visible = False
            End If
        ElseIf txtAddressesRows.Text <> "" AndAlso CInt(txtAddressesRows.Text) > 0 Then
            grdSpecials.Visible = False
            grdAddresses.Visible = True
            cmdShowGrid.Visible = False
        Else
            grdSpecials.Visible = False
            grdAddresses.Visible = False
            cmdShowGrid.Visible = False
        End If
    End Sub


    Protected Sub cmdShowGrid_Click(sender As Object, e As System.EventArgs) Handles cmdShowGrid.Click
        If cmdShowGrid.Text = "Show Containers" Then
            cmdShowGrid.Text = "Show Pickupss"
            grdAddresses.Visible = True
            grdSpecials.Visible = False
        Else
            cmdShowGrid.Text = "Show Containers"
            grdAddresses.Visible = False
            grdSpecials.Visible = True
        End If
    End Sub

     Public Class StreetAddress
        Public StreetNumber As String
        Public StreetName As String
    End Class

    Private Function ParseStreetNumberAndName() As StreetAddress
        Dim sa As New StreetAddress
        sa.StreetName = txtAddress.Text
        If IsNumeric(Mid(sa.StreetName, 1, 1)) Then
            Dim firstAlpha As Integer = Len(sa.StreetName)
            For i As Integer = 1 To Len(sa.StreetName)
                If Not IsNumeric(Mid(sa.StreetName, i, 1)) Then
                    firstAlpha = i
                    Exit For
                End If
            Next
            Dim firstSpace As Integer = Len(firstSpace)
            For i As Integer = 1 To Len(sa.StreetName)
                If Mid(sa.StreetName, i, 1) = " " Then
                    firstSpace = i
                    Exit For
                End If
            Next
            If firstAlpha >= firstSpace Then
                sa.StreetNumber = Mid(sa.StreetName, 1, firstSpace - 1)
                sa.StreetName = Mid(sa.StreetName, firstSpace + 1)
            Else
                sa.StreetNumber = ""
            End If
        End If
        Return sa
    End Function

    Protected Sub calSpecials_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calSpecials.SelectionChanged
        If MonthChanged Then
            MonthChanged = False
        Else
            UnselectDate = SelectedPickupDate.InnerText = Format(calSpecials.SelectedDate.Date, "MM/dd/yyyy")
            If UnselectDate Then
                calSpecials.SelectedDates.Clear()
                SelectedPickupDate.InnerText = ""
            Else
                'display selected date:
                SelectedPickupDate.InnerText = Format(calSpecials.SelectedDate.Date, "MM/dd/yyyy")
            End If
        End If
    End Sub

    Protected Sub calSpecials_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calSpecials.VisibleMonthChanged
        MonthChanged = True
    End Sub

	Protected Sub cmdSearchByLocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchByLocation.Click
		Dim pickupDate As Date = CDate(SelectedPickupDate.InnerText)
		Dim i As Integer

		sqlSpecials.SelectCommand = "SELECT PickupDate, City, ZIP, [Address], StreetName, " & _
				"CrossStreet, ItemLocation, ItemBags, ItemBoxes, ItemOther, Grid, DriverLocation, Comment, Grade, [Status], AddressID, " & _
				"Gate, S.CharityID, C.CharityAbbr, SoftCarts, HardCarts, " & _
				"CASE WHEN PickupDate IS NULL THEN 'No Pickup Date Scheduled' ELSE CONVERT(varchar, PickupDate, 101) END AS PickupDateDisplay, " & _
				"PickupID, StatusChangedBy, StatusChangedOn, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, ScheduledBy, ScheduledOn, DeviceName " & _
			"FROM  tblContainers AS S " & _
			"LEFT OUTER JOIN tblCharities AS C ON C.CharityID = S.CharityID " & _
			"WHERE [PickupDate] >= '19900101' "

		Dim locationsSelected As Boolean = False
		For i = 0 To ddlLocations.Items.Count - 1
			If ddlLocations.Items(i).Selected Then
				If Not locationsSelected Then
					sqlSpecials.SelectCommand &= "AND ([DriverLocation] = '" & ddlLocations.Items(i).Text & "' "
					locationsSelected = True
				Else
					sqlSpecials.SelectCommand &= "OR [DriverLocation] = '" & ddlLocations.Items(i).Text & "' "
				End If
			End If
		Next
		If locationsSelected Then
			sqlSpecials.SelectCommand &= ") "
		End If

		sqlSpecials.SelectCommand &= "ORDER BY PickupDate DESC, DriverLocation"
		txtSQL_grdSpecials.Text = sqlSpecials.SelectCommand

		PopulateSpecialsGrid()
		PrepareSpecialsResults(gridPageSize2)
		grdSpecials.PageIndex = 0
		ckSpecialsOnly.Checked = True
		PrepareQryResultsDisplay()
	End Sub

    Protected Sub cmdSearchByDateLocationCharity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchByDateLocationCharity.Click
        Dim pickupDate As Date = CDate(SelectedPickupDate.InnerText)
        Dim i As Integer

        sqlSpecials.SelectCommand = "SELECT PickupDate, City, ZIP, [Address], StreetName, " & _
                "CrossStreet, ItemLocation, ItemBags, ItemBoxes, ItemOther, Grid, DriverLocation, Comment, Grade, [Status], AddressID, " & _
                "Gate, S.CharityID, C.CharityAbbr, DriverID, SoftCarts, HardCarts, " & _
                "CASE WHEN PickupDate IS NULL THEN 'No Pickup Date Scheduled' ELSE CONVERT(varchar, PickupDate, 101) END AS PickupDateDisplay, " & _
                "PickupID, StatusChangedBy, StatusChangedOn, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, ScheduledBy, ScheduledOn, DeviceName " & _
            "FROM  tblContainers AS S " & _
            "LEFT OUTER JOIN tblCharities AS C ON C.CharityID = S.CharityID " & _
            "WHERE [PickupDate] = '" & FormatDateTime(pickupDate, DateFormat.ShortDate) & "' "

        Dim locationsSelected As Boolean = False
        For i = 0 To ddlLocations.Items.Count - 1
            If ddlLocations.Items(i).Selected Then
                If Not locationsSelected Then
                    sqlSpecials.SelectCommand &= "AND ([DriverLocation] = '" & ddlLocations.Items(i).Text & "' "
                    locationsSelected = True
                Else
                    sqlSpecials.SelectCommand &= "OR [DriverLocation] = '" & ddlLocations.Items(i).Text & "' "
                End If
            End If
        Next
        If locationsSelected Then
            sqlSpecials.SelectCommand &= ") "
        End If

        Dim charitiesSelected As Boolean = False
        For i = 0 To ddlCharities.Items.Count - 1
            If ddlCharities.Items(i).Selected Then
                If Not charitiesSelected Then
                    sqlSpecials.SelectCommand &= "AND ([CharityID] = " & ddlCharities.Items(i).Value & " "
                    charitiesSelected = True
                Else
                    sqlSpecials.SelectCommand &= "OR [CharityID] = " & ddlCharities.Items(i).Value & " "
                End If
            End If
        Next
        If charitiesSelected Then
            sqlSpecials.SelectCommand &= ") "
        End If

        sqlSpecials.SelectCommand &= "ORDER BY PickupDate DESC, DriverLocation"
        txtSQL_grdSpecials.Text = sqlSpecials.SelectCommand

        PopulateSpecialsGrid()
        PrepareSpecialsResults(gridPageSize2)
        grdSpecials.PageIndex = 0
        ckSpecialsOnly.Checked = True
        PrepareQryResultsDisplay()
    End Sub

    Protected Sub grdSpecials_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSpecials.RowCommand
        Select Case Convert.ToString(e.CommandName)
            Case "Sort"                 'click on the column header (invoking sort)
            Case "Page"                 'click on the page number given by syskey
            Case "sysrowSelector"       'click on the selected row; use syskey to invoke the editor for this record
                Dim syskey As String = Convert.ToString(grdSpecials.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
                'for this PickupID, populate detail
                Dim objConn As New System.Data.SqlClient.SqlConnection
                Dim oDR As System.Data.SqlClient.SqlDataReader = Nothing
                Dim myCommandText As String = "SELECT * FROM tblContainers WHERE (PickupID = N'" & syskey & "')"
                Try
                    oDR = DataUtil.GetReader(objConn, vConnStr, System.Data.CommandType.Text, myCommandText, 90)
                    If oDR.HasRows Then
                        Try
                            If oDR.Read Then
                                txtPickupID.Text = oDR.Item("PickupID").ToString
                                txtAddress.Text = oDR.Item("Address").ToString
                                txtCrossStreet.Text = oDR.Item("CrossStreet").ToString
                                txtCity.Text = oDR.Item("City").ToString
                                txtZIP.Text = oDR.Item("ZIP").ToString
                                txtAddressID.Text = oDR.Item("AddressID").ToString
                                If txtAddressID.Text <> "" Then
                                    txtAddress.Enabled = False
                                    txtCity.Enabled = False
                                    txtZIP.Enabled = False
                                Else
                                    txtAddress.Enabled = True
                                    txtCity.Enabled = True
                                    txtZIP.Enabled = True
                                End If
                                txtBags.Text = oDR.Item("ItemBags").ToString
                                txtBoxes.Text = oDR.Item("ItemBoxes").ToString
                                txtOther.Text = oDR.Item("ItemOther").ToString
                                txtDeviceName.Text = oDR.Item("DeviceName").ToString
                                txtSoftCarts.Text = oDR.Item("SoftCarts").ToString
                                txtHardCarts.Text = oDR.Item("HardCarts").ToString

                                If oDR.Item("PickupDate").ToString <> "" Then
									dtPickupDate.Value = CDate(oDR.Item("PickupDate").ToString)
									hfPickupDate.Value = dtPickupDate.Value
                                Else
									dtPickupDate.Value = ""
									hfPickupDate.Value = ""
                                End If
                                If oDR.Item("DriverLocation").ToString = "" Then
                                    ddlLocation.Text = " "
                                Else
                                    ddlLocation.Text = oDR.Item("DriverLocation").ToString
                                End If

                                Dim sql As String = "SELECT DriverID, DriverName " & _
                                    "FROM tblDrivers AS D "
                                If ddlLocation.Text <> " " Then
                                    sql += "INNER JOIN tlkRegions AS RG ON RG.RegionID = D.DriverLocationID " & _
                                        "WHERE RG.RegionDesc = '" & ddlLocation.Text & "' "
                                End If
                                sql &= "ORDER BY DriverName"
                                dsDrivers2.SelectCommand = sql
                                ddlDrivers2.DataBind()

                                If oDR.Item("DriverID").ToString <> "" Then
                                    ddlDrivers2.SelectedValue = CInt(oDR.Item("DriverID"))
                                Else
                                    ddlDrivers2.Text = " "
                                End If

                                SetCharity(oDR("CharityID").ToString, oDR("CharityAbbr").ToString)

                                txtGrid.Text = oDR.Item("Grid").ToString
                                txtComment.Text = oDR.Item("Comment").ToString
                                If oDR.Item("Grade").ToString = "" Then
                                    ddlGrade.Text = " "
                                Else
                                    ddlGrade.Text = oDR.Item("Grade").ToString
                                End If
                                If oDR.Item("Status").ToString = "" Then
                                    ddlStatus.Text = " "
                                Else
                                    ddlStatus.Text = oDR.Item("Status").ToString
                                End If
                                If oDR.Item("Status").ToString = "" Then
                                    lblInitialStatus.Text = " "
                                Else
                                    lblInitialStatus.Text = oDR.Item("Status").ToString
                                End If
                                If oDR.Item("StartTime").ToString <> "" Then
                                    dtStartTime.Value = CDate(oDR.Item("StartTime").ToString)
                                Else
                                    dtStartTime.Value = ""
                                End If
                                If oDR.Item("EndTime").ToString <> "" Then
                                    dtEndTime.Value = CDate(oDR.Item("EndTime").ToString)
                                Else
                                    dtEndTime.Value = ""
                                End If

                                lblScheduledBy.Text = oDR.Item("ScheduledBy").ToString

                                hfSyskey.Value = syskey
                                GetScheduledPickup("SPECIAL", syskey, "PS.PickupDate >= '" & Format(Now, "MM/dd/yyyy") & "' ")

                                cmdSaveDisplay("enabled")
                                cmdSave.Text = "Save Old Container"
                                grdSpecials.Visible = True
                                cmdNewContainer.Enabled = True

							End If
                        Catch ex As Exception
                            LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
                        End Try
                    End If
                Catch ex As Exception
                    LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
                End Try
                oDR.Close()
                objConn.Close()

            Case Else
        End Select
    End Sub

    Private Sub GetScheduledPickup(type As String, syskey As String, dateClause As String)
        Dim objConn As New System.Data.SqlClient.SqlConnection
        Dim oDR As System.Data.SqlClient.SqlDataReader = Nothing
        Dim sql As String = ""

        ckConfirmed.Checked = False
        ckMissed.Checked = False
        txtDetailComments.Text = ""

        sql = "SELECT TOP 1 R.RouteCode + '-' + S.SectionCode AS RouteSection, PS.PickupDate, " & _
            "PSD.PickupScheduleDetailID, PSD.Confirmed, PSD.Missed, PSD.Comments, " & _
            "R.RouteID, S.SectionID, PS.PickupScheduleID, PSD.Mail, PSD.Email, PSD.Bag, PSD.Postcard "

        If type = "SPECIAL" Then
            sql += "FROM tblContainers SP " & _
                "LEFT JOIN tblAddresses A ON A.AddressID = SP.AddressID "
        Else
            sql += "FROM tblAddresses A "
        End If

        sql += "INNER JOIN tblSections AS S ON S.SectionID = A.SectionID " & _
            "INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID " & _
            "INNER JOIN tblPickupScheduleDetail AS PSD ON PSD.AddressID = A.AddressID " & _
            "INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSD.PickupScheduleID "

        If type = "SPECIAL" Then
            sql += "WHERE (SP.PickupID = N'" & syskey & "') " & _
                "AND " & dateClause & " "
        ElseIf type = "ADDRESS" Then
            sql += "WHERE (A.AddressID = N'" & syskey & "') " & _
                "AND " & dateClause & " "
        Else
            Dim streetName As String = Convert.ToString(grdNextPickups.DataKeys(syskey).Value)
            Dim city As String = grdNextPickups.Rows(CInt(syskey)).Cells(1).Text
            Dim zip As String = grdNextPickups.Rows(CInt(syskey)).Cells(2).Text
            sql += "WHERE (A.StreetName = '" & streetName & "') " & _
                "AND (A.City = '" & city & "') " & _
                "AND (SUBSTRING(A.Zip, 1, 5) = '" & Mid(zip, 1, 5) & "') " & _
                "AND " & dateClause & " "
        End If

        sql += "ORDER BY PS.PickupDate"

        Dim oDR2 As System.Data.SqlClient.SqlDataReader = Nothing

        Try
            oDR2 = DataUtil.GetReader(objConn, vConnStr, System.Data.CommandType.Text, sql, 90)
            If oDR2.HasRows Then
                Try
                    If oDR2.Read Then
                        btnPrevPickup.Visible = True
                        qryRouteSection.Text = oDR2.Item("RouteSection").ToString
                        Dim pickupDate As Date = CDate(oDR2.Item("PickupDate").ToString)
                        qryNextScheduledPickup.Text = "Next Scheduled Pickup: " & Format(pickupDate, "MM/dd/yyyy")
                        hfPrevPickupDate.Value = Format(pickupDate, "MM/dd/yyyy")
                        If pickupDate = DateValue(Now) Then
                            hfPrevPickupIsToday.Value = "True"
                        Else
                            hfPrevPickupIsToday.Value = "False"
                        End If
                        hfType.Value = type
                        If oDR2.Item("PickupScheduleDetailID").ToString() = "" Then
                            'No PickupScheduleDetailID so we didn't mail to them
                            ckConfirmed.Visible = False
                            ckMissed.Visible = False
                            lblDetailComments.Visible = False
                            txtDetailComments.Visible = False
                            btnScheduleDetailSave.Visible = False
                            imgMail.Visible = False
                            imgEmail.Visible = False
                            imgBag.Visible = False
                            imgPostcard.Visible = False
                        Else
                            hfPickupScheduleDetailID.Value = CInt(oDR2.Item("PickupScheduleDetailID").ToString())
                            ckConfirmed.Checked = CBool(oDR2.Item("Confirmed").ToString)
                            ckMissed.Checked = CBool(oDR2.Item("Missed").ToString)
                            txtDetailComments.Text = oDR2.Item("Comments").ToString
                            ckConfirmed.Visible = True
                            ckMissed.Visible = True
                            lblDetailComments.Visible = True
                            txtDetailComments.Visible = True
                            btnScheduleDetailSave.Visible = True
                            If CBool(oDR2.Item("Mail").ToString) Then
                                imgMail.Visible = True
                            Else
                                imgMail.Visible = False
                            End If
                            If CBool(oDR2.Item("Email").ToString) Then
                                imgEmail.Visible = True
                            Else
                                imgEmail.Visible = False
                            End If
                            If CBool(oDR2.Item("Bag").ToString) Then
                                imgBag.Visible = True
                            Else
                                imgBag.Visible = False
                            End If
                            If CBool(oDR2.Item("Postcard").ToString) Then
                                imgPostcard.Visible = True
                            Else
                                imgPostcard.Visible = False
                            End If

                            'Populate ddlDrivers
                            dsDrivers.SelectCommand = "SELECT DISTINCT D.DriverID, DriverName " & _
                                "FROM tblRoutes R " & _
                                "INNER JOIN tblPickupCycleTemplatesDetail PCTD ON PCTD.RouteID = R.RouteID " & _
                                "INNER JOIN tblPickupCycleTemplates PCT ON PCT.PickupCycleTemplateID = PCTD.PickupCycleTemplateID " & _
                                "INNER JOIN tblPickupCycles PC ON PC.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
                                "INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
                                "INNER JOIN tblDrivers D ON D.DriverLocationID = PCDL.RegionID " & _
                                "WHERE PCT.Active = 1 " & _
                                    "AND R.RouteID = " & oDR2.Item("RouteID") & " " & _
                                    "AND D.Active = 1 " & _
                                "ORDER BY DriverName"
                            ddlDrivers.DataBind()
                            hfDrivers.Value = dsDrivers.SelectCommand

							Dim rs As SqlDataReader = Nothing
							Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
							Dim driverNames As String = ""
                            Try
                                sql = "SELECT DISTINCT D.DriverID, DriverName " & _
                                    "FROM tblDrivers D " & _
                                    "INNER JOIN tblDriverAssignments DA ON DA.DriverID = D.DriverID " & _
                                    "WHERE DA.SectionID = " & oDR2.Item("SectionID") & "And DA.PickupScheduleID = " & oDR2.Item("PickupScheduleID") & " " & _
                                    "ORDER BY DriverName"
								If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
									Return
								End If

								While rs.Read()
									If driverNames <> "" Then
										driverNames += ";"
									End If
									driverNames += rs("DriverName")
								End While
								SqlQueryClose(connSQL, rs)
							Catch ex As Exception
								Client_Alert("Driver Error")
                            End Try
                            ddlDrivers.Text = driverNames
                        End If
                    Else
                        btnPrevPickup.Visible = False
                        ckConfirmed.Visible = False
                        ckMissed.Visible = False
                        lblDetailComments.Visible = False
                        txtDetailComments.Visible = False
                        btnScheduleDetailSave.Visible = False
                        imgMail.Visible = False
                        imgEmail.Visible = False
                        imgBag.Visible = False
                        imgPostcard.Visible = False
                    End If
                Catch ex As Exception
                    LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
                End Try
            Else
                btnPrevPickup.Visible = False
                qryRouteSection.Text = ""
                qryNextScheduledPickup.Text = ""
                ckConfirmed.Visible = False
                ckMissed.Visible = False
                lblDetailComments.Visible = False
                txtDetailComments.Visible = False
                btnScheduleDetailSave.Visible = False
                imgMail.Visible = False
                imgEmail.Visible = False
                imgBag.Visible = False
                imgPostcard.Visible = False
            End If
        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
        End Try
        oDR2.Close()
        objConn.Close()

    End Sub

    Protected Sub btnScheduleDetailSave_Click(sender As Object, e As EventArgs) Handles btnScheduleDetailSave.Click
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim myCmd As SqlCommand = New SqlCommand()
        myCmd.Connection = conn
        myCmd.CommandText = "spSpecialsConfirmMiss"
        myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@confirmed", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, CBool(ckConfirmed.Checked)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@missed", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, CBool(ckMissed.Checked)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@comments", System.Data.ParameterDirection.Input, System.Data.DbType.String, Replace(txtDetailComments.Text, "'", "''")))

		myCmd.Parameters.Add(DataUtil.CreateParameter("@addressID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, txtAddressID.Value))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleDetailID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(hfPickupScheduleDetailID.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@userID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@userName", System.Data.ParameterDirection.Input, System.Data.DbType.String, CStr(Session("vUserName"))))

		myCmd.Parameters.Add(DataUtil.CreateParameter("@return_value", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@error_id", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			Dim error_id As Integer = myCmd.Parameters("@error_id").Value
			If error_id > 0 Then
				vbHandleProgramError(error_id, "Containers, btnScheduleDetailSave_Click")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try

	End Sub

	Protected Sub btnTextDriver_Click(sender As Object, e As EventArgs) Handles btnTextDriver.Click
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		If IsNothing(ddlDrivers.Text) OrElse ddlDrivers.Text = "" Then
			ja("Please select at least one Driver.")
			Return
		End If

		Dim drivers() As String = Split(ddlDrivers.Text, ";")
		For Each driverName In drivers
			Dim sql As String = "SELECT PhoneNumber, TextingDomain from tblDrivers D " & _
				"INNER JOIN tblPhones P ON P.PhoneID = D.PhoneID " & _
				"WHERE DriverName = '" & driverName & "'"
			If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
				Return
			End If
			If rs.Read() Then
				Dim txtAddress As String = rs("PhoneNumber") & "@" & rs("TextingDomain")

				SqlQueryClose(connSQL, rs)
				sql = "SELECT A.StreetAddress, R.RouteCode, S.SectionCode " & _
					"FROM tblPickupScheduleDetail PSD " & _
					"INNER JOIN tblAddresses A ON A.AddressID = PSD.AddressID " & _
					"INNER JOIN tblSections S ON S.SectionID = A.SectionID " & _
					"INNER JOIN tblRoutes R ON R.RouteID = S.RouteID " & _
					"WHERE PSD.PickupScheduleDetailID = " & hfPickupScheduleDetailID.Value
				If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
					Return
				End If
				If rs.Read() Then
					Dim vBody As String = rs("StreetAddress")
					If txtDetailComments.Text <> "" Then
						vBody += " - " + txtDetailComments.Text
					End If
					Dim vSubject As String = rs("RouteCode") & "-" & rs("SectionCode")
					Dim vPriority As String = ""

					If ismt(Session("vUserEmail")) Then
						ja("Cannot send text. Your email address is missing in the user table.")
					Else
						vbSendEmail(Session("vUserName"), Session("vUserEmail"), txtAddress, vSubject, vBody, vPriority, "HTML")

						'Log Text
						Dim conn As SqlConnection = New SqlConnection(vConnStr)
						Dim myCmd As SqlCommand = New SqlCommand()
						myCmd.Connection = conn
						myCmd.CommandText = "spSpecialsLogTextSent"
						myCmd.CommandType = System.Data.CommandType.StoredProcedure

						myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleDetailID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(hfPickupScheduleDetailID.Value)))
						myCmd.Parameters.Add(DataUtil.CreateParameter("@userID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
						myCmd.Parameters.Add(DataUtil.CreateParameter("@userName", System.Data.ParameterDirection.Input, System.Data.DbType.String, CStr(Session("vUserName"))))
						myCmd.Parameters.Add(DataUtil.CreateParameter("@subject", System.Data.ParameterDirection.Input, System.Data.DbType.String, vSubject))
						myCmd.Parameters.Add(DataUtil.CreateParameter("@body", System.Data.ParameterDirection.Input, System.Data.DbType.String, vBody))
						myCmd.Parameters.Add(DataUtil.CreateParameter("@driverName", System.Data.ParameterDirection.Input, System.Data.DbType.String, driverName))
						myCmd.Parameters.Add(DataUtil.CreateParameter("@return_value", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))
						myCmd.Parameters.Add(DataUtil.CreateParameter("@error_id", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))

						Try
							conn.Open()
							myCmd.ExecuteNonQuery()
							Dim error_id As Integer = myCmd.Parameters("@error_id").Value
							If error_id > 0 Then
								vbHandleProgramError(error_id, "Containers, btnTextDriver_Click")
							End If
						Catch ex As Exception
							LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
						Finally
							conn.Close()
						End Try
					End If
				Else
					ja("Section Description not found.")
					SqlQueryClose(connSQL, rs)
					Return
				End If

			Else
				ja("Driver not found. Text not sent.")
				SqlQueryClose(connSQL, rs)
				Return
			End If
			rs.Close()
		Next
		ja("Text message(s) sent to '" & ddlDrivers.Text & "'")
	End Sub

	Protected Sub btnPrevPickup_Click(sender As Object, e As EventArgs) Handles btnPrevPickup.Click
		GetScheduledPickup(hfType.Value, hfSyskey.Value, "PS.PickupDate < '" & Format(Now, "MM/dd/yyyy") & "' AND PS.PickupDate < '" & hfPrevPickupDate.Value & "' ")
	End Sub

	Private Sub SetCharity(ByVal charityID As String, charityAbbr As String)
		If (charityID = "" Or charityID = " ") And (charityAbbr = "" Or charityAbbr = " ") Then Return
		'If CharityID = NULL, then use CharityAbbr (historical Pickup Cycle data) for display 
		'and do not let the user change it.
		If charityID = "" Then
			Dim item As ListItem = ddlCharity.Items.FindByText(charityAbbr)
			If IsNothing(item) Then
				ddlCharity.Items.Add(charityAbbr)
			End If
			ddlCharity.Text = charityAbbr
			If ddlCharity.Text = "" Then
				ddlCharity.Text = " "
				ddlCharity.Enabled = True
			Else
				ddlCharity.Enabled = False
			End If
		Else
			Try
				ddlCharity.SelectedValue = CInt(charityID)
			Catch
			Finally
				ddlCharity.Enabled = True
			End Try
		End If
	End Sub

	Protected Sub grdAddresses_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAddresses.RowCommand
		Select Case Convert.ToString(e.CommandName)
			Case "Sort"					'click on the column header (invoking sort)
			Case "Page"					'click on the page number given by syskey
			Case "sysrowSelector"		'click on the selected row; use syskey to invoke the editor for this record
				Dim syskey As String = Convert.ToString(grdAddresses.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
				'for this AddressID, populate detail
				Dim objConn As New System.Data.SqlClient.SqlConnection
				Dim oDR As System.Data.SqlClient.SqlDataReader = Nothing
				Dim myCommandText As String = "SELECT * FROM tblAddresses WHERE (AddressID = " & syskey & ")"
				Try
					oDR = DataUtil.GetReader(objConn, vConnStr, System.Data.CommandType.Text, myCommandText, 90)
					If oDR.HasRows Then
						Try
							If oDR.Read Then
								txtAddress.Text = oDR.Item("StreetAddress").ToString
								txtAddress.Enabled = False
								txtPickupID.Text = ""
								txtCrossStreet.Text = ""
								txtCity.Text = oDR.Item("City").ToString
								txtCity.Enabled = False
								txtZIP.Text = oDR.Item("ZIP").ToString
								txtZIP.Enabled = False
								txtAddressID.Text = syskey
								txtBags.Text = ""
								txtBoxes.Text = ""
								txtOther.Text = ""
								txtDeviceName.Text = ""
								dtPickupDate.Value = ""
								ddlLocation.Text = " "
								ddlCharity.SelectedValue = " "
								ddlCharity.Enabled = True
								txtGrid.Text = ""
								txtComment.Text = ""
								ddlGrade.Text = " "
								ddlStatus.Text = " "
								lblInitialStatus.Text = " "
								ddlDrivers2.Text = " "
								txtSoftCarts.Text = ""
								txtHardCarts.Text = ""
								dtStartTime.Value = ""
								dtEndTime.Value = ""
								lblScheduledBy.Text = ""

								hfSyskey.Value = syskey
								GetScheduledPickup("ADDRESS", syskey, "PS.PickupDate >= '" & Format(Now, "MM/dd/yyyy") & "' ")

								cmdSave.Text = "Save New Container"
								cmdSaveDisplay("new")
								grdAddresses.Visible = True
							End If
						Catch ex As Exception
							LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
						End Try
					End If
				Catch ex As Exception
					LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
				End Try
				oDR.Close()
				objConn.Close()

			Case Else
		End Select
	End Sub

	Protected Sub grdNextPickups_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdNextPickups.RowCommand
		Select Case Convert.ToString(e.CommandName)
			Case "Sort"					'click on the column header (invoking sort)
			Case "Page"					'click on the page number given by syskey
			Case "sysrowSelector"		'click on the selected row; use syskey to invoke the editor for this record

				Dim row = Convert.ToInt32(e.CommandArgument)

				' Always use the Street Name from the grid and add the Street Number if one was entered in the Search TextBox
				Dim sa As StreetAddress = New StreetAddress
				sa = ParseStreetNumberAndName()
				Dim streetName As String = Convert.ToString(grdNextPickups.DataKeys(row).Value)
				If sa.StreetNumber = "" Then
					txtAddress.Text = streetName
				Else
					txtAddress.Text = sa.StreetNumber & " " & streetName
				End If

				txtAddress.Enabled = False
				txtPickupID.Text = ""
				txtCrossStreet.Text = ""
				txtCity.Text = grdNextPickups.Rows(row).Cells(1).Text
				txtCity.Enabled = False
				txtZIP.Text = grdNextPickups.Rows(row).Cells(2).Text
				txtZIP.Enabled = False
				txtAddressID.Text = ""
				txtBags.Text = ""
				txtBoxes.Text = ""
				txtOther.Text = ""
				txtDeviceName.Text = ""
				dtPickupDate.Value = CDate(grdNextPickups.Rows(row).Cells(3).Text)
				ddlLocation.Text = " "
				ddlCharity.SelectedValue = " "
				ddlCharity.Enabled = True
				txtGrid.Text = ""
				txtComment.Text = ""
				ddlGrade.Text = " "
				ddlStatus.Text = " "
				lblInitialStatus.Text = " "
				ddlDrivers2.Text = " "
				txtSoftCarts.Text = ""
				txtHardCarts.Text = ""
				dtStartTime.Value = ""
				dtEndTime.Value = ""
				lblScheduledBy.Text = ""

				hfSyskey.Value = ""
				GetScheduledPickup("NEXT_PICKUPS", row.ToString, "PS.PickupDate >= '" & Format(Now, "MM/dd/yyyy") & "' ")

				cmdSave.Text = "Save New Pickup"
				cmdSaveDisplay("new")

			Case Else
		End Select
	End Sub

	Sub vbAddBlank(ByVal ctrl As DropDownList, e As EventArgs)
		ctrl.Items.Insert(0, " ")
	End Sub

	Public Function GetStrippedGUID() As String
		Dim MyGuid As String = Space(32)
		Try
			MyGuid = System.Guid.NewGuid.ToString.Replace("-", "").ToUpper
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try
		Return MyGuid
	End Function

	Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
		SaveSpecial()
	End Sub

	Private Sub SaveSpecial()
		' If the Inventory for this date or any future date has been approved, changes are not allowed.
		Dim approved As Boolean = IsInventoryApproved(dtPickupDate.Value, , ddlLocation.Text)

		If approved Then
			ja("CHANGES NOT SAVED: The Ending Inventory for this Location and Date has been approved so no changes can be made.")
			Return
		End If

		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spContainersSave"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		Dim pickupID As Integer
		If txtPickupID.Text = "" Then
			pickupID = 0
		Else
			pickupID = CInt(txtPickupID.Text)
		End If
		Dim addressID As Integer = 0
		'If txtAddressID.Text = "" Then
		'    addressID = 0
		'    Dim cnt As Integer = 0
		'    Dim message As String = ""
		'    cnt = SearchAddress("SpecialsNewAddress", Session("vUserID"), False, True, True, _
		'                        txtAddress.Text, txtCity.Text, txtZIP.Text, message, addressID)
		'    If cnt < 0 Then
		'        ja(message)
		'        Return
		'    ElseIf cnt > 1 Then
		'        ja("The address entered matches more than one address.")
		'        Return
		'    ElseIf cnt = 1 Then
		'        If message <> "Address Created" Then
		'            ja("FYI: You entered this as a new address and yet the address matched an address in the database.")
		'        End If
		'    End If
		'Else
		'    addressID = CInt(txtAddressID.Text)
		'End If

		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupID", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, pickupID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@addressID", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, addressID))

		Dim bags As Integer
		If txtBags.Text = "" Then
			bags = 0
		Else
			bags = CInt(txtBags.Text)
		End If
		Dim boxes As Integer
		If txtBoxes.Text = "" Then
			boxes = 0
		Else
			boxes = CInt(txtBoxes.Text)
		End If
		If dtStartTime.Value < "1/1/1990" Then
			dtStartTime.Value = dtPickupDate.Value
		End If
		If dtEndTime.Value < "1/1/1990" Then
			dtEndTime.Value = dtPickupDate.Value
		End If

		Dim softCarts As Double
		If txtSoftCarts.Text = "" Or Not IsNumeric(txtSoftCarts.Text) Then
			softCarts = 0
		Else
			softCarts = CDbl(txtSoftCarts.Text)
		End If

		Dim hardCarts As Double
		If txtHardCarts.Text = "" Or Not IsNumeric(txtHardCarts.Text) Then
			hardCarts = 0
		Else
			hardCarts = CDbl(txtHardCarts.Text)
		End If

		myCmd.Parameters.Add(DataUtil.CreateParameter("@streetAddress", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtAddress.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@city", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtCity.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@state", System.Data.ParameterDirection.Input, System.Data.DbType.String, "CA"))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@zip", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtZIP.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@crossStreet", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtCrossStreet.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@itemBags", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(bags = 0, DBNull.Value, bags)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@itemBoxes", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(boxes = 0, DBNull.Value, boxes)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@itemOther", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtOther.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@deviceName", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtDeviceName.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@grid", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtGrid.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@driverLocation", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlLocation.Text = " ", DBNull.Value, ddlLocation.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@charityID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(IsNumeric(ddlCharity.SelectedValue), ddlCharity.SelectedValue, DBNull.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupDate", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(IsNothing(dtPickupDate.Value), DBNull.Value, dtPickupDate.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@startTime", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(IsNothing(dtStartTime.Value), DBNull.Value, dtStartTime.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@endTime", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(IsNothing(dtEndTime.Value), DBNull.Value, dtEndTime.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@comment", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtComment.Text.Replace("'", "''")))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@grade", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlGrade.Text = " ", DBNull.Value, ddlGrade.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@status", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlStatus.Text = " ", DBNull.Value, ddlStatus.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@driverID", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlDrivers2.Text = " ", DBNull.Value, ddlDrivers2.SelectedValue)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@softCarts", System.Data.ParameterDirection.Input, System.Data.DbType.String, softCarts))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@hardCarts", System.Data.ParameterDirection.Input, System.Data.DbType.String, hardCarts))

		If txtPickupID.Text = "" Then
			myCmd.Parameters.Add(DataUtil.CreateParameter("@statusChangedOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, DBNull.Value))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@statusChangedBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, DBNull.Value))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@createdOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, Now))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@createdBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, Session("vUserName")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@modifiedOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, DBNull.Value))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@modifiedBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, DBNull.Value))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduledOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(ddlStatus.Text = "SCHEDULED", Now, DBNull.Value)))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduledBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlStatus.Text = "SCHEDULED", Session("vUserName"), DBNull.Value)))
		Else
			If ddlStatus.Text <> lblInitialStatus.Text Then
				myCmd.Parameters.Add(DataUtil.CreateParameter("@statusChangedOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, Now))
				myCmd.Parameters.Add(DataUtil.CreateParameter("@statusChangedBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, Session("vUserName")))
			Else
				myCmd.Parameters.Add(DataUtil.CreateParameter("@statusChangedOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, DBNull.Value))
				myCmd.Parameters.Add(DataUtil.CreateParameter("@statusChangedBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, DBNull.Value))
			End If
			If ddlStatus.Text = "SCHEDULED" And (IsNothing(lblInitialStatus.Text) Or lblInitialStatus.Text <> "SCHEDULED") Then
				myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduledOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, Now))
				myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduledBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, Session("vUserName")))
			Else
				myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduledOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, DBNull.Value))
				myCmd.Parameters.Add(DataUtil.CreateParameter("@scheduledBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, DBNull.Value))
			End If
			myCmd.Parameters.Add(DataUtil.CreateParameter("@createdOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, DBNull.Value))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@createdBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, DBNull.Value))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@modifiedOn", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, Now))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@modifiedBy", System.Data.ParameterDirection.Input, System.Data.DbType.String, Session("vUserName")))
		End If

		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, Session("vUserID")))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			txtPickupID.Text = myCmd.Parameters("@pickupID").Value.ToString()
			txtAddressID.Text = myCmd.Parameters("@addressID").Value.ToString()
			If errorID > 0 Then
				vbHandleProgramError(errorID, "Containers, SaveSpecial")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try

		'Repopulate the grid
		PopulateSpecialsGrid()
		PrepareSpecialsResults(gridPageSize)

		cmdSaveDisplay("enabled")
		cmdSave.Text = "Save Old Pickup"
		cmdShowGrid.Text = "Show Addresses"
		grdAddresses.Visible = False
		grdSpecials.Visible = True
		cmdNewContainer.Enabled = True
	End Sub

    Protected Sub cmdNewContainer_Click(sender As Object, e As System.EventArgs) Handles cmdNewContainer.Click
        txtPickupID.Text = ""
        txtBags.Text = ""
        txtBoxes.Text = ""
        txtOther.Text = ""
        ddlDrivers2.Text = " "
        txtSoftCarts.Text = ""
        txtHardCarts.Text = ""
        txtDeviceName.Text = ""
        dtPickupDate.Value = ""
        dtStartTime.Value = ""
        dtEndTime.Value = ""
        If Not ddlCharity.Enabled Then
            ddlCharity.Text = " "
            ddlCharity.Enabled = True
        End If
        ddlGrade.Text = " "
        lblInitialStatus.Text = " "
        lblScheduledBy.Text = ""
        cmdSave.Text = "Save New Pickup"
        cmdSaveDisplay("new")
        cmdNewContainer.Enabled = False
    End Sub

    Protected Sub cmdNewContainerAddress_Click(sender As Object, e As System.EventArgs) Handles cmdNewContainerAddress.Click
        ClearDetail()
        ddlCharity.Enabled = True
        cmdSave.Text = "Save New Pickup"
        cmdSaveDisplay("new")
        cmdNewContainer.Enabled = False
        qryRouteSection.Text = ""
        qryNextScheduledPickup.Text = ""
    End Sub

    Protected Sub cmdDeleteContainer_Click(sender As Object, e As System.EventArgs) Handles cmdDeleteContainer.Click
        Dim conn As SqlConnection = New SqlConnection(vConnStr)

        Dim sql As String = "DELETE FROM [tblContainers] WHERE [PickupID] = '" & txtPickupID.Text & "'"

        Dim myCmd As New SqlCommand(sql, conn)
        Try
            conn.Open()
            myCmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
        End Try

        'Repopulate the grid
        PopulateSpecialsGrid()
        PrepareSpecialsResults(gridPageSize)

        ClearDetail()
    End Sub

    Protected Sub ClearDetail()
        txtPickupID.Text = ""
        txtAddress.Text = ""
        txtCrossStreet.Text = ""
        txtCity.Text = ""
        txtZIP.Text = ""
        txtAddressID.Text = ""
        txtBags.Text = ""
        txtBoxes.Text = ""
        txtOther.Text = ""
        txtDeviceName.Text = ""
        dtPickupDate.Value = ""
        ddlLocation.Text = " "
        ddlCharity.Text = " "
        ddlCharity.Enabled = False
        txtGrid.Text = ""
        txtComment.Text = ""
        ddlGrade.Text = " "
        ddlStatus.Text = " "
        lblInitialStatus.Text = " "
        lblScheduledBy.Text = ""
        ddlDrivers2.Text = " "
        txtSoftCarts.Text = ""
        txtHardCarts.Text = ""
        txtAddress.Enabled = True
        txtCity.Enabled = True
        txtZIP.Enabled = True
        cmdSaveDisplay("disabled")
    End Sub

    Private Sub cmdSaveDisplay(ByVal state As String)
        If state = "disabled" Then
            cmdSave.Enabled = False
            cmdSave.BorderColor = Drawing.Color.Gray
        Else
            cmdSave.Enabled = True
            If state = "new" Then
                cmdSave.BorderColor = Drawing.Color.Red
            Else
                cmdSave.BorderColor = Drawing.Color.Gray
            End If
        End If
    End Sub

    <WebMethod()> _
    Public Shared Function CheckDuplicateSpecial(ByVal pickupID As Integer, ByVal pickupDate As Date, _
                            ByVal address As String, ByVal city As String) As Boolean

        Dim sql As String
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim result As Boolean = False
        Dim r As System.Data.SqlClient.SqlDataReader = Nothing
        sql = "SELECT TOP 1 [AddressID] FROM [tblContainers] WHERE [PickupID] <> " & pickupID & " AND " & _
                "[PickupDate] = '" & FormatDateTime(pickupDate, DateFormat.ShortDate) & "' AND " & _
                "[Address] = '" & address & "' AND [City] = '" & city & "'"
        Try
            r = DataUtil.GetReader(conn, vConnStr, System.Data.CommandType.Text, sql, 90)
            If r.HasRows Then
                result = True
            End If
        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
        Finally
            r.Close()
            conn.Close()
        End Try

        Return result
    End Function

    Protected Sub cmdContainerPickupsSheet_Click(sender As Object, e As System.EventArgs) Handles cmdContainerPickupsSheet.Click
		Dim x As Object
		Dim vParams As String = ""
        x = calSpecials.SelectedDate
        vParams = vParams & "&PARMS=pickupDate~" & x
        x = ""
        For i = 0 To ddlLocations.Items.Count - 1
            If ddlLocations.Items(i).Selected Then
                If x <> "" Then
                    x = x & ","
                End If
                x = x & ddlLocations.Items(i).Value
            End If
        Next
        If x <> "" Then
            vParams = vParams & "|DriverLocation~" & x
        End If
        x = ""
        For i = 0 To ddlCharities.Items.Count - 1
            If ddlCharities.Items(i).Selected Then
                If x <> "" Then
                    x = x & ","
                End If
                x = x & ddlCharities.Items(i).Value
            End If
        Next
        If x <> "" Then
            vParams = vParams & "|charityIDs~" & x
        End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Container Pickups Sheet" & vParams)
    End Sub

    Protected Sub cmdContainerPickupsForRouting_Click(sender As Object, e As System.EventArgs) Handles cmdContainerPickupsForRouting.Click
		Dim x As Object
		Dim vParams As String = ""
        x = calSpecials.SelectedDate
        vParams = vParams & "&PARMS=pickupDate~" & x
        x = ""
        For i = 0 To ddlLocations.Items.Count - 1
            If ddlLocations.Items(i).Selected Then
                If x <> "" Then
                    x = x & ","
                End If
                x = x & ddlLocations.Items(i).Value
            End If
        Next
        If x <> "" Then
            vParams = vParams & "|DriverLocation~" & x
        End If
        x = ""
        For i = 0 To ddlCharities.Items.Count - 1
            If ddlCharities.Items(i).Selected Then
                If x <> "" Then
                    x = x & ","
                End If
                x = x & ddlCharities.Items(i).Value
            End If
        Next
        If x <> "" Then
            vParams = vParams & "|charityIDs~" & x
        End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Container Pickups for Routing" & vParams)
    End Sub

    Protected Sub cmdContainerPickupsNotGradedReport_Click(sender As Object, e As System.EventArgs) Handles cmdContainerPickupsNotGradedReport.Click
        Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, Today) & _
            "|endDate~" & Today
        If IsNothing(ddlLocations.SelectedItem) OrElse (ddlLocations.SelectedItem.Text = "") Then
            parms &= "|driverLocationCode~All Locations"
        Else
            parms &= "|driverLocationCode~" & ddlLocations.SelectedItem.Text
        End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Container Pickups Not Graded" & parms)
    End Sub

End Class
