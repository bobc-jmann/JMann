Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Web.Services
Imports DataUtil

Partial Class Specials
    Inherits System.Web.UI.Page
    Private UnselectDate As Boolean = False
    Private MonthChanged As Boolean = False

    Private gridPageSize As Integer = 5
    Private gridPageSize2 As Integer = 15

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'validate incoming URI:
        'If Not Request.UrlReferrer = Nothing Then
        '	If Request.UrlReferrer.AbsoluteUri.StartsWith(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_URI").ToString) Then
        '	Else
        '		Response.Redirect(Request.UrlReferrer.AbsoluteUri)
        '		Exit Sub
        '	End If
        'Else
        '	Response.Redirect("DeadEnd.htm")
        '	Exit Sub
        'End If

        'Session("vUserName") = ""
		'Response.Redirect("~/Intranet.aspx", True)
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Intranet.aspx") : re()

		If (calSpecials.SelectedDate = "1/1/0001 12:00:00 AM") Then calSpecials.SelectedDate = Today() : calSpecials_SelectionChanged(sender, e)

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
						LogProgramError(ex.Message, PostBackControlID, ex.StackTrace, "Notify User", "Specials, Page_Load")
                    End Try
                End If
            Case "GET"
                'if coming from login form and has proper stuff:
                'If Not Request.UrlReferrer = Nothing Then
                'If Request.UrlReferrer.AbsoluteUri.StartsWith(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_URI").ToString) Then
                'If Request.QueryString.HasKeys Then
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

                    dsDriver.SelectCommand = "SELECT D.DriverID, D.DriverName " & _
                        "FROM tblDrivers AS D " & _
                        "INNER JOIN tlkRegions AS RG ON RG.RegionID = D.DriverLocationID " & _
                        "ORDER BY D.DriverName"
                    ddlDriver.DataBind()
                    hfDriver.Value = dsDriver.SelectCommand

                Catch ex As Exception
					LogProgramError(ex.Message, dsDriver.SelectCommand, ex.StackTrace, "Notify User", "Specials, Page_Load2")
                End Try

                If Session("UserRegionDefault") <> 0 Then
                    ddlLocations.SelectedValue = Session("UserRegionDefault")
                    ddlLocation.SelectedValue = Session("UserRegionDefault")
                End If

                divNotReviewed.Visible = "false"
                cmdSaveDisplay("disabled")
                qryRouteSection.Text = ""
                qryNextScheduledPickup.Text = ""
                qryResults.Text = ""

                SqlDataSourceGrades.SelectCommand = "SELECT [Grade] FROM [tlkGrades] ORDER BY [SortCode]"
                ddlGrade.DataBind()

                SqlDataSourceStatus.SelectCommand = "SELECT [SpecialStatus] FROM [tlkSpecialStatuses] ORDER BY [SortCode]"
                ddlStatus.DataBind()

                ' If a PickupID (pid) was passed in the query string, display that pickup.
                'http://localhost:5338/Streamline/intranet.aspx?pid=291799
                Try
                    qsPickupID.Value = Request.QueryString("pid")
                Catch
                    qsPickupID.Value = ""
                End Try

                If qsPickupID.Value <> "" Then
                    grdSpecials_DisplayRow(qsPickupID.Value)
                End If

            Case "HEAD"
                'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
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
				.SortExpression = "Email"
				.DataField = "Email"
				.HeaderText = "Email"
				.Visible = True
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
                .SortExpression = "SpecialType"
                .DataField = "SpecialType"
                .HeaderText = "Special Type"
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
            hdr.Text = "Specials"
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
            hdr.Text = "Addresses in SMS that match your last search"
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
				.ItemStyle.HorizontalAlign = HorizontalAlign.Center
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
			LogProgramError(ex.Message, sqlSpecials.SelectCommand, ex.StackTrace, "Notify User", "Specials, PrepareSpecialsResults")
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
			LogProgramError(ex.Message, sqlAddresses.SelectCommand, ex.StackTrace, "Notify User", "Specials, PrepareAddressResults")
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
			LogProgramError(ex.Message, sqlNextPickups.SelectCommand, ex.StackTrace, "Notify User", "Specials, PrepareNextPickupResults")
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
        qryResults.Text = txtSpecialsRows.Text & " special" & IIf(txtSpecialsRows.Text = "1", "", "s") & " found"
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
                cmdNewSpecial.Enabled = True
            Else
                cmdShowGrid.Visible = False
            End If
        ElseIf txtAddressesRows.Text <> "" AndAlso CInt(txtAddressesRows.Text) > 0 Then
            grdSpecials.Visible = False
            grdAddresses.Visible = True
            cmdShowGrid.Visible = False
            cmdNewSpecial.Enabled = False
        Else
            grdSpecials.Visible = False
            grdAddresses.Visible = False
            cmdShowGrid.Visible = False
            cmdNewSpecial.Enabled = False
        End If
    End Sub


    Protected Sub cmdShowGrid_Click(sender As Object, e As System.EventArgs) Handles cmdShowGrid.Click
        If cmdShowGrid.Text = "Show Addresses" Then
            cmdShowGrid.Text = "Show Specials"
            grdAddresses.Visible = True
            grdSpecials.Visible = False
            cmdNewSpecial.Enabled = False
        Else
            cmdShowGrid.Text = "Show Addresses"
            grdAddresses.Visible = False
            grdSpecials.Visible = True
            cmdNewSpecial.Enabled = False
        End If
    End Sub

    Protected Sub cmdShowNextPickups_Click(sender As Object, e As System.EventArgs) Handles cmdShowNextPickups.Click
        If cmdShowNextPickups.Text = "Show Next Pickups" Then
            cmdShowNextPickups.Text = "Hide Next Pickups"
            grdNextPickups.Visible = True
            PrepareNextPickupsResults(gridPageSize)
        Else
            cmdShowNextPickups.Text = "Show Next Pickups"
            grdNextPickups.Visible = False
        End If
    End Sub

    Private Function GetWhere(Optional ByVal OnlyCity As Boolean = False, Optional ByVal OnlyStreet As Boolean = False, _
            Optional ByVal OnlyEmail As Boolean = False, Optional ByVal OnlyNotReviewed As Boolean = False) As String
        Dim Where As String = " WHERE "
        Try
            If OnlyCity Or OnlyStreet Or OnlyEmail Or OnlyNotReviewed Then
                If OnlyCity Then
                    If txtCityNameLike.Text.Length > 2 Then
                        Where = Where & "AND City LIKE '" & txtCityNameLike.Text & "%' "
                    End If
                End If
                If OnlyStreet Then
                    If txtStreetNameLike.Text.Length > 0 Then
                        If IsStreetNumber(txtStreetNameLike.Text) Then
                            Where = Where & "AND Address LIKE '" & txtStreetNameLike.Text & "%' "
                        Else
                            Where = Where & "AND StreetName LIKE '" & txtStreetNameLike.Text & "%' "
                        End If
                    End If
                End If
                If OnlyEmail Then
                    If txtEmailAddressLike.Text.Length > 0 Then
                        Where = Where & "AND AE.Email LIKE '" & RemoveEmailPunctuation(txtEmailAddressLike.Text) & "%' "
                    End If
                End If
                If OnlyNotReviewed Then
                    Where = Where & "AND [Status] = 'NOT REVIEWED' "
                End If
            Else
                If SelectedPickupDate.InnerText.Length > 0 Then
                    Where = Where & "(PickupDate = CONVERT(DATETIME, '" & calSpecials.SelectedDate.ToString() & "', 101)) "
                End If
                If txtStreetNameLike.Text.Length > 0 Then
                    If IsStreetNumber(txtStreetNameLike.Text) Then
                        Where = Where & "AND Address LIKE '" & txtStreetNameLike.Text & "%' "
                    Else
                        Where = Where & "AND StreetName LIKE '" & txtStreetNameLike.Text & "%' "
                    End If
                End If
                If txtCityNameLike.Text.Length > 2 Then
                    Where = Where & "AND City Like '" & txtCityNameLike.Text & "%' "
                End If
                If ddlLocations.GetSelectedIndices.Length > 0 Then
                    Dim WhereLocations As String = ""
                    For i As Integer = 0 To ddlLocations.GetSelectedIndices.Length - 1
                        WhereLocations = WhereLocations & " OR " & "OurLocation='" & ddlLocations.Items(ddlLocations.GetSelectedIndices(i)).Text & "'"
                    Next i
                    'remove first or
                    Where = Where & "AND (" & WhereLocations.Substring(4) & ")"
                End If
                If ddlCharities.GetSelectedIndices.Length > 0 Then

                End If
            End If
        Catch ex As Exception
			LogProgramError(ex.Message, Where, ex.StackTrace, "Notify User", "Specials, GetWhere")
        End Try
        If Where.StartsWith(" WHERE AND ") Then Where = " WHERE " & Where.Substring(11)
        If Where.Length = 7 Then Where = "" 'no where statement
        Return Where
    End Function

    Private Sub BuildSql(ByVal Where As String, Optional ByVal OnlyNotReviewed As Boolean = False)
		sqlSpecials.SelectCommand = "SELECT PickupDate, FirstName, LastName, PhoneWork, PhoneHome, S.Email, City, ZIP, [Address], StreetName, " & _
				"CrossStreet, ItemLocation, ItemBags, ItemBoxes, ItemOther, Grid, DriverLocationID, DriverLocation, Comment, Grade, [Status], AddressID, SpecialType, " & _
				"Receipt, Gate, S.CharityID, C.CharityAbbr, " & _
				"CASE WHEN PickupDate IS NULL THEN 'No Pickup Date Scheduled' ELSE CONVERT(varchar, PickupDate, 101) END AS PickupDateDisplay, " & _
				"PickupID, StatusChangedBy, StatusChangedOn, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, ScheduledBy, ScheduledOn, DeviceName, DriverID, " & _
				"PhoneMobile, PhoneOther, LanguagePreference, DialerDate, DialerStatus, DialerComment " & _
			"FROM tblSpecials AS S " & _
			"LEFT OUTER JOIN tblCharities AS C ON C.CharityID = S.CharityID " & _
			Where & _
			"AND (DriverLocationID IN " & Session("userRegionsList") & " " & _
				"OR (DriverLocationID IS NULL OR DriverLocationID = 0))"
        If Not OnlyNotReviewed Then
            sqlSpecials.SelectCommand &= "AND ([Status] <> 'NOT REVIEWED' OR [Status] IS NULL) "
            sqlSpecials.SelectCommand &= " ORDER BY PickupDate DESC"
        Else
            sqlSpecials.SelectCommand &= " ORDER BY PickupDate"
        End If
        sqlSpecials.SelectCommand = Replace(sqlSpecials.SelectCommand, "AE.Email", "S.Email") ' conflict with Email BIT now in address table
        txtSQL_grdSpecials.Text = sqlSpecials.SelectCommand
        If OnlyNotReviewed Then
            sqlAddresses.SelectCommand = "SELECT A.AddressID, AE.Email, A.StreetAddress AS [Address], A.City, A.ZIP, " & _
                    "A.StreetNumber, A.StreetName " & _
                "FROM tblAddresses AS A " & _
                "LEFT OUTER JOIN tblAddressEmails AS AE ON AE.AddressID = A.AddressID " & _
                "WHERE A.AddressID = -1" ' We don't want any addresses here'
            txtSQL_grdAddresses.Text = sqlAddresses.SelectCommand
        Else
            sqlAddresses.SelectCommand = "SELECT A.AddressID, AE.Email, A.StreetAddress AS [Address], A.City, A.ZIP, " & _
                    "A.StreetNumber, A.StreetName " & _
                "FROM tblAddresses AS A " & _
                "LEFT OUTER JOIN tblAddressEmails AS AE ON AE.AddressID = A.AddressID " & Replace(Where, " Address ", " StreetAddress ") & _
                "ORDER BY A.City, A.StreetName, A.StreetNumber"
            txtSQL_grdAddresses.Text = sqlAddresses.SelectCommand
        End If

        ' Get StreetName only if there's a leading StreetNumber
        If txtStreetNameLike.Text.Length > 0 Then
            Dim streetName As String = Trim(ParseStreetNumberAndName().StreetName)

			' This is the new logic. Just going off of what is currently scheduled is not enough
			' now that we are not scheduling so far into the future. RCC - 7/11/16. 
			'This gets existing Pickup Schedules
			'UNION
			'This gets the next unscheduled Pickup for each Route
			sqlNextPickups.SelectCommand = "SELECT DISTINCT StreetName, City, SUBSTRING(ZIP, 1, 5) AS ZIP, " & _
				"CONVERT(varchar, PickupDate, 101) AS PickupDate, PS.RouteCode + '-' + PSS.SectionCode AS RouteSection " & _
			"FROM tblAddresses AS A " & _
			"INNER JOIN tblPickupScheduleDetail AS PSD ON PSD.AddressID = A.AddressID " & _
			"INNER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleSectionID = PSD.PickupScheduleSectionID " & _
			"INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSD.PickupScheduleID " & _
			"WHERE StreetName LIKE '" & streetName & "%' " & _
				"AND PS.PickupDate >= '" & Format(DateValue(Now()), "MM/dd/yyyy") & "' " & _
				"AND PSD.MailNR = 0 " & _
				"AND PSD.EmailNR = 0 " & _
			"UNION " & _
			"SELECT DISTINCT StreetName, City, SUBSTRING(ZIP, 1, 5) AS ZIP, " & _
				"CONVERT(varchar, NP.NextPickupDate, 101) AS PickupDate, PS.RouteCode + '-' + PSS.SectionCode AS RouteSection " & _
			"FROM tblAddresses AS A " & _
			"INNER JOIN tblPickupScheduleDetail AS PSD ON PSD.AddressID = A.AddressID " & _
			"INNER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleSectionID = PSD.PickupScheduleSectionID " & _
			"INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSD.PickupScheduleID " & _
			"CROSS APPLY (SELECT TOP (1) DATEADD(DAY, DATEDIFF(DAY, " & _
					"LEAD(PS2.PickupDate, 1) OVER (ORDER BY PS2.PickupDate DESC), PS2.PickupDate), PS2.PickupDate) AS NextPickupDate " & _
				"FROM tblPickupschedule AS PS2 " & _
				"WHERE (PS2.RouteID = PS.RouteID) " & _
				"ORDER BY PS.PickupDate DESC) AS NP " & _
			"WHERE StreetName LIKE '" & streetName & "%' " & _
				"AND PS.PickupDate >= DATEADD(MONTH, -1, '" & Format(DateValue(Now()), "MM/dd/yyyy") & "') " & _
				"AND PSD.MailNR = 0 " & _
				"AND PSD.EmailNR = 0 "

		Else
			sqlNextPickups.SelectCommand = "SELECT * FROM tblAddresses WHERE AddressID = 0"
		End If
        txtSQL_grdNextPickups.Text = sqlNextPickups.SelectCommand
    End Sub

    Public Class StreetAddress
        Public StreetNumber As String
        Public StreetName As String
    End Class

    Private Function ParseStreetNumberAndName() As StreetAddress
        Dim sa As New StreetAddress
        sa.StreetName = txtStreetNameLike.Text
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

    Protected Sub cmdSearchByDateLocationCharity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchByDateLocationCharity.Click
        Dim pickupDate As Date = CDate(SelectedPickupDate.InnerText)
        Dim i As Integer

		sqlSpecials.SelectCommand = "SELECT PickupDate, FirstName, LastName, PhoneWork, PhoneHome, Email, City, ZIP, [Address], StreetName, " & _
				"CrossStreet, ItemLocation, ItemBags, ItemBoxes, ItemOther, Grid, DriverLocation, Comment, Grade, [Status], AddressID, SpecialType, " & _
				"Receipt, Gate, S.CharityID, C.CharityAbbr, " & _
				"CASE WHEN PickupDate IS NULL THEN 'No Pickup Date Scheduled' ELSE CONVERT(varchar, PickupDate, 101) END AS PickupDateDisplay, " & _
				"PickupID, StatusChangedBy, StatusChangedOn, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, ScheduledBy, ScheduledOn, DeviceName, DriverID, " & _
				"PhoneMobile, PhoneOther, LanguagePreference, DialerDate, DialerStatus, DialerComment " & _
			"FROM  tblSpecials AS S " & _
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

        sqlSpecials.SelectCommand &= "ORDER BY PickupDate DESC"
        txtSQL_grdSpecials.Text = sqlSpecials.SelectCommand

        PopulateSpecialsGrid()
        PrepareSpecialsResults(gridPageSize2)
        grdSpecials.PageIndex = 0
        ckSpecialsOnly.Checked = True
        PrepareQryResultsDisplay()
    End Sub

    Protected Sub cmdSearchByCity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchByCity.Click
        If txtCityNameLike.Text = "" Then
            Return
        End If
        'apply only city criteria
        Dim message As String = ""
        If SearchAddress("SpecialsCity", Session("vUserID"), True, True, False, "", txtCityNameLike.Text, "", message) < 0 Then
            ja(message)
        End If
        Dim where As String = GetWhere(OnlyCity:=True)
        BuildSql(where)
        PopulateSpecialsGrid()
        PopulateAddressesGrid()
        PrepareSpecialsResults(gridPageSize)
        PrepareAddressesResults(gridPageSize)
        grdSpecials.PageIndex = 0
        grdAddresses.PageIndex = 0
        grdNextPickups.PageIndex = 0
        ckSpecialsOnly.Checked = False
        PrepareQryResultsDisplay()
    End Sub

	Protected Sub cmdSearchByStreet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchByStreet.Click
		If txtStreetNameLike.Text = "" Then
			Return
		End If
		'apply only street criteria
		Dim message As String = ""
		If SearchAddress("SpecialsStreet", Session("vUserID"), True, True, False, txtStreetNameLike.Text, "", "", message) < 0 Then
			ja(message)
		End If
		Dim where As String = GetWhere(OnlyStreet:=True)
		BuildSql(where)
		PopulateSpecialsGrid()
		PopulateAddressesGrid()
		PopulateNextPickupsGrid()
		PrepareSpecialsResults(gridPageSize)
		PrepareAddressesResults(gridPageSize)
		grdSpecials.PageIndex = 0
		grdAddresses.PageIndex = 0
		grdNextPickups.PageIndex = 0
		ckSpecialsOnly.Checked = False
		PrepareQryResultsDisplay()
	End Sub

    Public Sub cmdSearchByStreetNameAndCity_Click(sender As Object, e As System.EventArgs) Handles cmdSearchByStreetAndCity.Click
        If txtCityNameLike.Text = "" And txtStreetNameLike.Text = "" Then
            Return
        End If
        'apply both street and city criteria
        Dim message As String = ""
        If SearchAddress("SpecialsStreetCity", Session("vUserID"), True, True, False, _
                         txtStreetNameLike.Text, txtCityNameLike.Text, "", message) < 0 Then
            ja(message)
        End If
        Dim where As String = GetWhere(OnlyCity:=True, OnlyStreet:=True)
        BuildSql(where)
        PopulateSpecialsGrid()
        PopulateAddressesGrid()
        PrepareSpecialsResults(gridPageSize)
        PrepareAddressesResults(gridPageSize)
        grdSpecials.PageIndex = 0
        grdAddresses.PageIndex = 0
        grdNextPickups.PageIndex = 0
        ckSpecialsOnly.Checked = False
        PrepareQryResultsDisplay()
    End Sub

    Protected Sub cmdSearchByEmailAddress_Click(sender As Object, e As System.EventArgs) Handles cmdSearchByEmail.Click
        If txtEmailAddressLike.Text = "" Then
            Return
        End If
        'apply only email criteria
        Dim where As String = GetWhere(OnlyEmail:=True)
        BuildSql(where)
        PopulateSpecialsGrid()
        PopulateAddressesGrid()
        PrepareSpecialsResults(gridPageSize)
        PrepareAddressesResults(gridPageSize)
        grdSpecials.PageIndex = 0
        grdAddresses.PageIndex = 0
        ckSpecialsOnly.Checked = False
        PrepareQryResultsDisplay()
    End Sub

    Protected Sub grdSpecials_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSpecials.RowCommand
        Select Case Convert.ToString(e.CommandName)
            Case "Sort"                 'click on the column header (invoking sort)
            Case "Page"                 'click on the page number given by syskey
            Case "sysrowSelector"       'click on the selected row; use syskey to invoke the editor for this record
                Dim syskey As String = Convert.ToString(grdSpecials.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
                'for this PickupID, populate detail
                grdSpecials_DisplayRow(syskey)
            Case Else
        End Select
    End Sub

    Private Sub grdSpecials_DisplayRow(ByVal pickupID As Integer)
        Dim objConn As New System.Data.SqlClient.SqlConnection
        Dim oDR As System.Data.SqlClient.SqlDataReader = Nothing
		Dim myCommandText As String = "SELECT SP.SpecialType, SP.PickupID, SP.Address, SP.CrossStreet, " & _
				"SP.City, SP.ZIP, SP.AddressID, SP.FirstName, SP.LastName, SP.PhoneHome, SP.PhoneWork, " & _
				"SP.Email, ST.[Status] AS AddressStatus, SP.ItemLocation, SP.ItemBags, SP.ItemBoxes, SP.ItemOther, " & _
				"SP.PromisedBags, SP.PromisedBoxes, SP.PromisedOther, SP.DeviceName, " & _
				"SP.DriverLocation, SP.DriverID, SP.Reminder, SP.DonorSource, SP.Grade, SP.PickupDate, " & _
				"SP.CharityID, SP.CharityAbbr, SP.Receipt, SP.Grid, SP.Gate, SP.Comment, SP.[Status], " & _
				"SP.NotReviewedEmailComment, SP.StartTime, SP.EndTime, SP.ScheduledBy, SP.ScheduledOn, SP.ModifiedBy, SP.ModifiedOn, " & _
				"CASE WHEN A.SectionID IS NOT NULL THEN 'On ' + R.RouteCode + '-' + S.SectionCode " & _
					"WHEN A.NearestSectionID IS NOT NULL THEN FORMAT(A.NearestSectionDistance, '0.00') + ' miles from '  + RNS.RouteCode + '-' + SNS.SectionCode " & _
					"ELSE '' END AS NearestSection, " & _
				"PhoneMobile, PhoneOther, LanguagePreference, DialerDate, DialerStatus, DialerComment " & _
			"FROM tblSpecials AS SP " & _
			"INNER JOIN tblAddresses AS A ON A.AddressID = SP.AddressID " & _
			"LEFT OUTER JOIN tblSections AS S ON S.SectionID = A.SectionID " & _
			"LEFT OUTER JOIN tblRoutes AS R ON R.RouteID = S.RouteID " & _
			"LEFT OUTER JOIN tblSections AS SNS ON SNS.SectionID = A.NearestSectionID " & _
			"LEFT OUTER JOIN tblRoutes AS RNS ON RNS.RouteID = SNS.RouteID " & _
			"LEFT OUTER JOIN tlkStatuses AS ST ON ST.StatusID = A.StatusID " & _
			"WHERE (PickupID = N'" & pickupID & "')"
        Try
            oDR = DataUtil.GetReader(objConn, vConnStr, System.Data.CommandType.Text, myCommandText, 90)
            If oDR.HasRows Then
                Try
                    If oDR.Read Then
						rbSpecialType.Value = oDR.Item("SpecialType").ToString
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
						txtFirstName.Text = oDR.Item("FirstName").ToString
                        txtLastName.Text = oDR.Item("LastName").ToString
                        txtHomePhone.Text = oDR.Item("PhoneHome").ToString
						txtWorkPhone.Text = oDR.Item("PhoneWork").ToString
						txtMobilePhone.Text = oDR.Item("PhoneMobile").ToString
						txtEmail.Text = oDR.Item("Email").ToString
						txtAddressStatus.Text = oDR.Item("AddressStatus").ToString
						txtNearestSection.Text = oDR.Item("NearestSection").ToString
						If oDR.Item("ItemLocation").ToString = "" Then
							ddlItemLocation.Text = " "
						Else
							ddlItemLocation.Text = oDR.Item("ItemLocation").ToString
						End If
						If oDR.Item("LanguagePreference").ToString = "" Then
							ddlLanguagePreference.Text = "Not Specified"
						Else
							ddlLanguagePreference.Text = oDR.Item("LanguagePreference").ToString
						End If
						txtBags.Text = oDR.Item("ItemBags").ToString
                        txtBoxes.Text = oDR.Item("ItemBoxes").ToString
                        txtOther.Text = oDR.Item("ItemOther").ToString
                        txtPromisedBags.Text = oDR.Item("PromisedBags").ToString
                        txtPromisedBoxes.Text = oDR.Item("PromisedBoxes").ToString
                        txtPromisedOther.Text = oDR.Item("PromisedOther").ToString
                        hfDeviceName.Value = oDR.Item("DeviceName").ToString

                        dsDriver.SelectCommand = "SELECT D.DriverID, D.DriverName " & _
                            "FROM tblDrivers AS D " & _
                            "INNER JOIN tlkRegions AS RG ON RG.RegionID = D.DriverLocationID "
                        If oDR.Item("DriverLocation").ToString = "" Then
                            ddlLocation.Text = " "
                        Else
                            ddlLocation.Text = oDR.Item("DriverLocation").ToString
                            dsDriver.SelectCommand &= "WHERE RG.RegionDesc = '" & ddlLocation.Text & "' "
                        End If
                        dsDriver.SelectCommand &= "ORDER BY D.DriverName"
                        ddlDriver.DataBind()
                        hfDriver.Value = dsDriver.SelectCommand

                        If oDR.Item("DriverID").ToString = "" Then
                            ddlDriver.SelectedValue = 0
                        Else
							ddlDriver.SelectedValue = oDR.Item("DriverID").ToString
						End If

                        If oDR.Item("Reminder").ToString = "" Then
                            ckReminder.Checked = False
                        Else
                            ckReminder.Checked = CBool(oDR.Item("Reminder").ToString)
                        End If
                        txtDonorSource.Text = oDR.Item("DonorSource").ToString

                        If oDR.Item("PickupDate").ToString <> "" Then
                            dtPickupDate.Value = CDate(oDR.Item("PickupDate").ToString)
                        Else
                            dtPickupDate.Value = ""
                        End If

                        SetCharity(oDR("CharityID").ToString, oDR("CharityAbbr").ToString)

                        If oDR.Item("Receipt").ToString = "" Then
                            ckReceipt.Checked = False
                        Else
                            ckReceipt.Checked = CBool(oDR.Item("Receipt").ToString)
                        End If

                        txtGrid.Text = oDR.Item("Grid").ToString
						txtGate.Text = oDR.Item("Gate").ToString
						If oDR.Item("AddressStatus").ToString = "GATED" Then
							txtGate.BackColor = System.Drawing.Color.Violet
						Else
							txtGate.BackColor = System.Drawing.Color.White
						End If

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
							If ddlStatus.Text = "NOT REVIEWED" And ConfigurationManager.AppSettings("EmailConfirmationsForWebSpecials") Then
								divNotReviewed.Visible = True
							Else
								divNotReviewed.Visible = False
							End If
						End If
						If oDR.Item("NotReviewedEmailComment").ToString = "" Then
							txtNotReviewed.Text = " "
						Else
							txtNotReviewed.Text = oDR.Item("NotReviewedEmailComment").ToString
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
						lblScheduledOn.Text = oDR.Item("ScheduledOn").ToString
						lblModifiedBy.Text = oDR.Item("ModifiedBy").ToString
						lblModifiedOn.Text = oDR.Item("ModifiedOn").ToString

						hfSyskey.Value = pickupID
						If Not GetScheduledPickup("SPECIAL", pickupID, "PS.PickupDate >= '" & Format(Now, "MM/dd/yyyy") & "' ", "NEXT") Then
							GetScheduledPickup("SPECIAL", pickupID, "PS.PickupDate < '" & Format(Now, "MM/dd/yyyy") & "' ", "PREV")
						End If

						txtDialerDate.Text = oDR.Item("DialerDate").ToString
						txtDialerStatus.Text = oDR.Item("DialerStatus").ToString
						txtDialerComment.Text = oDR.Item("DialerComment").ToString

						cmdSaveDisplay("enabled")
						cmdSave.Text = "Save Old Special"
						grdSpecials.Visible = True
						cmdNewSpecial.Enabled = True
					End If
				Catch ex As Exception
					LogProgramError(ex.Message, myCommandText, ex.StackTrace, "Notify User", "Specials, grdSpecials_DisplayRow")
				End Try
            End If
        Catch ex As Exception
			LogProgramError(ex.Message, myCommandText, ex.StackTrace, "Notify User", "Specials, grdSpecials_DisplayRow2")
        End Try
        oDR.Close()
        objConn.Close()
    End Sub

	Private Function GetScheduledPickup(type As String, syskey As String, dateClause As String, ByVal direction As String) As Boolean
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim objConn As New System.Data.SqlClient.SqlConnection
		Dim oDR As System.Data.SqlClient.SqlDataReader = Nothing
		Dim sql As String = ""
		Dim pickupFound As Boolean = False

		ckConfirmed.Checked = False
		ckMissed.Checked = False
		ckRedTagged.Checked = False
		txtDetailComments.Text = ""
		txtOtherComments.Text = ""

		sql = "SELECT TOP (1) PS.RouteCode + '-' + PSS.SectionCode + ' (' + C.CharityAbbr + ')' AS RouteSection, PS.PickupDate, " & _
			"PSD.PickupScheduleDetailID, PSD.Confirmed, PSD.Missed, PSD.RedTagged, PSD.Comments, PSD.OtherComments, PSD.DeliveryDate, " & _
			"PS.RouteID, PSD.SectionID, PS.PickupScheduleID, PSD.Mail, PSD.Email, PSD.Bag, PSD.Postcard, " & _
			"(SELECT COUNT(*) FROM tSysTextMessageLog AS TML WHERE TML.PickupScheduleDetailID = PSD.PickupScheduleDetailID) AS TextsSent, " & _
			"R.Active, S.Active as sActive "

		If type = "SPECIAL" Then
			sql += "FROM tblSpecials SP " & _
				"INNER JOIN tblAddresses A ON A.AddressID = SP.AddressID "
		Else
			sql += "FROM tblAddresses A "
		End If

		sql += "INNER JOIN tblPickupScheduleDetail AS PSD ON PSD.AddressID = A.AddressID " & _
			"INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSD.PickupScheduleID " & _
			"INNER JOIN tblCharities AS C ON C.CharityID = PS.CharityID " & _
			"INNER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleSectionID = PSD.PickupScheduleSectionID " & _
			"INNER JOIN tblSections AS S ON S.SectionID = A.SectionID " & _
			"INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID "

		If type = "SPECIAL" Then
			sql += "WHERE (SP.PickupID = N'" & syskey & "') " & _
				"AND " & dateClause & " "
		ElseIf type = "ADDRESS" Or type = "NEXT_PICKUPS" Then
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

		sql += "AND PSD.EmailNR = 0 AND PSD.MailNR = 0 " & _
			"AND PSS.CntMail + PSS.CntBag + PSS.CntPostcard > 0 " & _
			"ORDER BY PS.PickupDate"

		If direction = "PREV" Then
			sql += " DESC"
		End If

		Dim oDR2 As System.Data.SqlClient.SqlDataReader = Nothing

		Try
			oDR2 = DataUtil.GetReader(objConn, vConnStr, System.Data.CommandType.Text, sql, 90)
			If oDR2.HasRows Then
				Try
					If oDR2.Read Then
						pickupFound = True
						btnPrevPickup.Visible = True
						qryRouteSection.Text = oDR2.Item("RouteSection").ToString
						If Not CBool(oDR2.Item("Active")) Or CInt(oDR2.Item("sActive")) = 0 Then
							qryRouteSection.Text &= " (Inactive)"
						End If
						Dim pickupDate As Date = CDate(oDR2.Item("PickupDate").ToString)
						If pickupDate >= Today Then
							qryNextScheduledPickup.Text = "Next Scheduled Pickup: " & Format(pickupDate, "MM/dd/yyyy")
						Else
							qryNextScheduledPickup.Text = "Previous Pickup: " & Format(pickupDate, "MM/dd/yyyy")
						End If
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
							ckRedTagged.Visible = False
							lblDetailComments.Visible = False
							txtDetailComments.Visible = False
							lblOtherComments.Visible = False
							txtOtherComments.Visible = False
							btnScheduleDetailSave.Visible = False
							lblDeliveryDate.Visible = False
							calDeliveryDate.Visible = False
							imgMail.Visible = False
							imgEmail.Visible = False
							imgBag.Visible = False
							imgPostcard.Visible = False
						Else
							hfPickupScheduleDetailID.Value = CInt(oDR2.Item("PickupScheduleDetailID").ToString())
							ckConfirmed.Checked = CBool(oDR2.Item("Confirmed").ToString)
							ckMissed.Checked = CBool(oDR2.Item("Missed").ToString)
							ckRedTagged.Checked = CBool(oDR2.Item("RedTagged").ToString)
							txtDetailComments.Text = oDR2.Item("Comments").ToString
							txtOtherComments.Text = oDR2.Item("OtherComments").ToString
							If oDR2.Item("DeliveryDate").ToString() = "" Then
								calDeliveryDate.Value = Nothing
							Else
								calDeliveryDate.Value = CDate(oDR2.Item("DeliveryDate").ToString())
							End If
							ckConfirmed.Visible = True
							ckMissed.Visible = True
							ckRedTagged.Visible = True
							lblDetailComments.Visible = True
							txtDetailComments.Visible = True
							lblOtherComments.Visible = True
							txtOtherComments.Visible = True
							btnScheduleDetailSave.Visible = True
							btnScheduleDetailSaveDisplay("New")
							lblDeliveryDate.Visible = True
							calDeliveryDate.Visible = True
							Dim textsSent As Integer = CInt(oDR2.Item("TextsSent").ToString())
							lblTextsSent.Text = CStr(textsSent) + " text"
							If textsSent <> 1 Then
								lblTextsSent.Text += "s"
							End If
							lblTextsSent.Text += " sent"

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
							dsDrivers.SelectCommand = "SELECT DISTINCT D.DriverID, D.DriverName " & _
								"FROM tblRoutes R " & _
								"INNER JOIN tblPickupCycleTemplatesDetail PCTD ON PCTD.RouteID = R.RouteID " & _
								"INNER JOIN tblPickupCycleTemplates PCT ON PCT.PickupCycleTemplateID = PCTD.PickupCycleTemplateID " & _
								"INNER JOIN tblPickupCycles PC ON PC.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
								"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
								"INNER JOIN tblDrivers D ON D.DriverLocationID = PCDL.RegionID " & _
								"WHERE PCT.Active = 1 " & _
									"AND R.RouteID = " & oDR2.Item("RouteID") & " " & _
									"AND D.Active = 1 " & _
								"ORDER BY D.DriverName"
							ddlDrivers.DataBind()
							hfDrivers.Value = dsDrivers.SelectCommand

							Dim driverNames As String = ""
							Try
								sql = "SELECT DISTINCT D.DriverID, D.DriverName " & _
									"FROM tblDrivers D " & _
									"INNER JOIN tblDriverAssignments DA ON DA.DriverID = D.DriverID " & _
									"WHERE DA.SectionID = " & oDR2.Item("SectionID") & "And DA.PickupScheduleID = " & oDR2.Item("PickupScheduleID") & " " & _
									"ORDER BY D.DriverName"
								If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
									Return False
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
						ckRedTagged.Visible = False
						lblDetailComments.Visible = False
						txtDetailComments.Visible = False
						lblOtherComments.Visible = False
						txtOtherComments.Visible = False
						btnScheduleDetailSave.Visible = False
						lblDeliveryDate.Visible = False
						calDeliveryDate.Visible = False
						imgMail.Visible = False
						imgEmail.Visible = False
						imgBag.Visible = False
						imgPostcard.Visible = False
					End If
				Catch ex As Exception
					LogProgramError(ex.Message, sql, ex.StackTrace, "Notify User", "Specials, GetScheduledPickup")
				End Try
			Else
				btnPrevPickup.Visible = False
				qryRouteSection.Text = ""
				qryNextScheduledPickup.Text = ""
				ckConfirmed.Visible = False
				ckMissed.Visible = False
				ckRedTagged.Visible = False
				lblDetailComments.Visible = False
				txtDetailComments.Visible = False
				lblOtherComments.Visible = False
				txtOtherComments.Visible = False
				btnScheduleDetailSave.Visible = False
				lblDeliveryDate.Visible = False
				calDeliveryDate.Visible = False
				imgMail.Visible = False
				imgEmail.Visible = False
				imgBag.Visible = False
				imgPostcard.Visible = False
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, sql, ex.StackTrace, "Notify User""Specials, GetScheduledPickup2")
		End Try
		oDR2.Close()
		objConn.Close()

		Return pickupFound
	End Function

    Protected Sub btnScheduleDetailSave_Click(sender As Object, e As EventArgs) Handles btnScheduleDetailSave.Click
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim myCmd As SqlCommand = New SqlCommand()
        myCmd.Connection = conn
        myCmd.CommandText = "spSpecialsConfirmMiss"
        myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@confirmed", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, CBool(ckConfirmed.Checked)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@missed", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, CBool(ckMissed.Checked)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@redTagged", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, CBool(ckRedTagged.Checked)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@comments", System.Data.ParameterDirection.Input, System.Data.DbType.String, Replace(txtDetailComments.Text, "'", "''")))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@otherComments", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtOtherComments.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@deliveryDate", System.Data.ParameterDirection.Input, System.Data.DbType.Date, calDeliveryDate.Value))
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
				vbHandleProgramError(error_id, "Specials, btnScheduleDetailSave_Click")
			End If
        Catch ex As Exception
			LogProgramError(ex.Message, myCmd.CommandText, ex.StackTrace, "Notify User", "Specials, btnScheduleDetailSave_Click")
        Finally
            conn.Close()
        End Try

        If ckConfirmed.Checked And rbSpecialType.Value = "WEB" Then
            ddlStatus.SelectedValue = "CONFIRM"
		End If

		btnScheduleDetailSaveDisplay("disabled")

    End Sub

    Protected Sub btnTextDriver_Click(sender As Object, e As EventArgs) Handles btnTextDriver.Click
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		If IsNothing(ddlDrivers.Text) OrElse ddlDrivers.Text = "" Then
			ja("Please select at least one Driver.")
			Return
		End If

		Dim sql As String
        Dim drivers() As String = Split(ddlDrivers.Text, ";")
        For Each driverName In drivers
			sql = "SELECT PhoneNumber, TextingDomain from tblDrivers D " & _
				"INNER JOIN tblPhones P ON P.PhoneID = D.PhoneID " & _
				"WHERE DriverName = '" & driverName & "'"
			'"WHERE DriverName = 'Robert Comyn'"

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
						Dim result As String = vbSendLogText(CInt(hfPickupScheduleDetailID.Value), _
									CInt(Session("vUserID")), Session("vUserName"), Session("vUserEmail"),
										txtAddress, vSubject, vBody, vPriority, "HTML", driverName)
						If result <> "Your email has been sent." Then
							ja(result)
							rs.Close()
							Return
						End If
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
			SqlQueryClose(connSQL, rs)
		Next

		sql = "SELECT COUNT(*) AS TextsSent " & _
			"FROM tSysTextMessageLog AS TML " & _
			"WHERE TML.PickupScheduleDetailID = " & CInt(hfPickupScheduleDetailID.Value)


		Dim rs2 As SqlDataReader = Nothing
		If Not SqlQueryOpen(connSQL, rs2, sql, "Notify Web User") Then
			Return
		End If
		If rs2.Read() Then

			Dim textsSent As Integer = CInt(rs2("TextsSent").ToString())
			lblTextsSent.Text = CStr(textsSent) + " text"
			If textsSent <> 1 Then
				lblTextsSent.Text += "s"
			End If
			lblTextsSent.Text += " sent"
		End If
		SqlQueryClose(connSQL, rs2)

        ja("Text message(s) sent to '" & ddlDrivers.Text & "'")
    End Sub

    Protected Sub btnPrevPickup_Click(sender As Object, e As EventArgs) Handles btnPrevPickup.Click
		GetScheduledPickup(hfType.Value, hfSyskey.Value, "PS.PickupDate < '" & Format(Now, "MM/dd/yyyy") & "' AND PS.PickupDate < '" & hfPrevPickupDate.Value & "' ", "PREV")
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
            Case "Sort"                 'click on the column header (invoking sort)
            Case "Page"                 'click on the page number given by syskey
            Case "sysrowSelector"       'click on the selected row; use syskey to invoke the editor for this record
                Dim syskey As String = Convert.ToString(grdAddresses.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
                'for this AddressID, populate detail
                Dim objConn As New System.Data.SqlClient.SqlConnection
                Dim oDR As System.Data.SqlClient.SqlDataReader = Nothing
				Dim myCommandText As String = "SELECT *, " & _
					"CASE WHEN A.SectionID IS NOT NULL THEN 'On ' + R.RouteCode + '-' + S.SectionCode " & _
						"WHEN A.NearestSectionID IS NOT NULL THEN FORMAT(A.NearestSectionDistance, '0.00') + ' miles from '  + RNS.RouteCode + '-' + SNS.SectionCode " & _
						"ELSE '' END AS NearestSection " & _
					"FROM tblAddresses AS A " & _
					"INNER JOIN tlkStatuses AS ST ON ST.StatusID = A.StatusID " & _
					"LEFT OUTER JOIN tblSections AS S ON S.SectionID = A.SectionID " & _
					"LEFT OUTER JOIN tblRoutes AS R ON R.RouteID = S.RouteID " & _
					"LEFT OUTER JOIN tblSections AS SNS ON SNS.SectionID = A.NearestSectionID " & _
					"LEFT OUTER JOIN tblRoutes AS RNS ON RNS.RouteID = SNS.RouteID " & _
					"WHERE (AddressID = " & syskey & ")"
				Try
					oDR = DataUtil.GetReader(objConn, vConnStr, System.Data.CommandType.Text, myCommandText, 90)
					If oDR.HasRows Then
						Try
							If oDR.Read Then
								rbSpecialType.Value = "PHONE"
								txtAddress.Text = oDR.Item("StreetAddress").ToString
								txtAddress.Enabled = False
								txtPickupID.Text = ""
								txtCrossStreet.Text = ""
								txtCity.Text = oDR.Item("City").ToString
								txtCity.Enabled = False
								txtZIP.Text = oDR.Item("ZIP").ToString
								txtZIP.Enabled = False
								txtAddressID.Text = syskey
								txtFirstName.Text = ""
								txtLastName.Text = ""
								txtHomePhone.Text = ""
								txtWorkPhone.Text = ""
								txtMobilePhone.Text = ""
								txtEmail.Text = grdAddresses.Rows(e.CommandArgument).Cells(3).Text
								If txtEmail.Text = "&nbsp;" Then
									txtEmail.Text = ""
								End If
								txtAddressStatus.Text = oDR.Item("Status").ToString
								txtNearestSection.Text = oDR.Item("NearestSection").ToString
								ddlItemLocation.Text = " "
								ddlLanguagePreference.Text = "Not Specified"
								txtBags.Text = ""
								txtBoxes.Text = ""
								txtOther.Text = ""
								txtPromisedBags.Text = ""
								txtPromisedBoxes.Text = ""
								txtPromisedOther.Text = ""
								hfDeviceName.Value = ""
								ddlDriver.SelectedValue = 0
								ckReminder.Checked = True
								txtDonorSource.Text = ""
								dtPickupDate.Value = ""
								ddlLocation.Text = " "
								ddlCharity.SelectedValue = " "
								ddlCharity.Enabled = True
								ckReceipt.Checked = True
								txtGrid.Text = ""
								txtGate.Text = ""
								If txtAddressStatus.Text = "GATED" Then
									txtGate.BackColor = System.Drawing.Color.Violet
									txtAddressStatus.BackColor = System.Drawing.Color.Violet
								ElseIf txtAddressStatus.Text = "LONG DRIVEWAY" Then
									txtAddressStatus.BackColor = System.Drawing.Color.Violet
								Else
									txtGate.BackColor = System.Drawing.Color.White
									txtAddressStatus.ForeColor = System.Drawing.Color.Black
								End If
								txtComment.Text = ""
								ddlGrade.Text = " "
								ddlStatus.Text = " "
								txtNotReviewed.Text = ""
								divNotReviewed.Visible = False
								lblInitialStatus.Text = " "
								dtStartTime.Value = ""
								dtEndTime.Value = ""
								lblScheduledBy.Text = ""
								lblScheduledOn.Text = ""
								lblModifiedBy.Text = ""
								lblModifiedOn.Text = ""

								txtDialerDate.Text = ""
								txtDialerStatus.Text = ""
								txtDialerComment.Text = ""

								hfSyskey.Value = syskey
								If Not GetScheduledPickup("ADDRESS", syskey, "PS.PickupDate >= '" & Format(Now, "MM/dd/yyyy") & "' ", "NEXT") Then
									GetScheduledPickup("ADDRESS", syskey, "PS.PickupDate < '" & Format(Now, "MM/dd/yyyy") & "' ", "PREV")
								End If

								cmdSave.Text = "Save New Special"
								cmdSaveDisplay("new")
								grdAddresses.Visible = True
							End If
						Catch ex As Exception
							LogProgramError(ex.Message, myCommandText, ex.StackTrace, "Notify User", "Specials, grdAddresses_RowCommand")
						End Try
					End If
				Catch ex As Exception
					LogProgramError(ex.Message, myCommandText, ex.StackTrace, "Notify User", "Specials, grdAddresses_RowCommand")
				End Try
                oDR.Close()
                objConn.Close()

            Case Else
        End Select
    End Sub

    Protected Sub grdNextPickups_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdNextPickups.RowCommand
        Select Case Convert.ToString(e.CommandName)
            Case "Sort"                 'click on the column header (invoking sort)
            Case "Page"                 'click on the page number given by syskey
            Case "sysrowSelector"       'click on the selected row; use syskey to invoke the editor for this record

                Dim row = Convert.ToInt32(e.CommandArgument)
                rbSpecialType.Value = "ROUTE"

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
                txtFirstName.Text = ""
                txtLastName.Text = ""
                txtHomePhone.Text = ""
				txtWorkPhone.Text = ""
				txtMobilePhone.Text = ""
				txtEmail.Text = ""
                If txtEmail.Text = "&nbsp;" Then
                    txtEmail.Text = ""
                End If
				txtAddressStatus.Text = ""
				txtNearestSection.Text = ""
				ddlItemLocation.Text = " "
				ddlLanguagePreference.Text = "Not Specified"
				txtBags.Text = ""
                txtBoxes.Text = ""
                txtOther.Text = ""
                txtPromisedBags.Text = ""
                txtPromisedBoxes.Text = ""
                txtPromisedOther.Text = ""
                hfDeviceName.Value = ""
                ddlDriver.SelectedValue = 0
				ckReminder.Checked = True
                txtDonorSource.Text = ""
                dtPickupDate.Value = CDate(grdNextPickups.Rows(row).Cells(3).Text)
                ddlLocation.Text = " "
                ddlCharity.SelectedValue = " "
                ddlCharity.Enabled = True
                ckReceipt.Checked = True
                txtGrid.Text = ""
                txtGate.Text = ""
				txtGate.BackColor = System.Drawing.Color.White
				txtComment.Text = ""
                ddlGrade.Text = " "
                ddlStatus.Text = " "
                lblInitialStatus.Text = " "
                txtNotReviewed.Text = ""
                divNotReviewed.Visible = False
                dtStartTime.Value = ""
                dtEndTime.Value = ""
				lblScheduledBy.Text = ""
				lblScheduledOn.Text = ""
				lblModifiedBy.Text = ""
				lblModifiedOn.Text = ""

				txtDialerDate.Text = ""
				txtDialerStatus.Text = ""
				txtDialerComment.Text = ""

				hfSyskey.Value = ""
				GetScheduledPickup("NEXT_PICKUPS", row.ToString, "PS.PickupDate >= '" & Format(Now, "MM/dd/yyyy") & "' ", "")

                cmdSave.Text = "Save New Special"
                cmdSaveDisplay("new")

            Case Else
        End Select
    End Sub

    Sub vbAddBlank(ByVal ctrl As DropDownList, e As EventArgs)
        ctrl.Items.Insert(0, " ")
    End Sub

    Sub vbAddBlankZero(ByVal ctrl As DropDownList, e As EventArgs)
        ctrl.Items.Insert(0, New ListItem(" ", "0"))
    End Sub

    Public Function GetStrippedGUID() As String
        Dim MyGuid As String = Space(32)
        Try
            MyGuid = System.Guid.NewGuid.ToString.Replace("-", "").ToUpper
        Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "Specials, GetStrippedGUID")
        End Try
        Return MyGuid
    End Function

    Protected Sub cmdSave_Click(sender As Object, e As System.EventArgs) Handles cmdSave.Click
        SaveSpecial()
    End Sub

	Private Sub SaveSpecial()
		Dim v As String = ""
		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spSpecialsSave"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		Dim pickupID As Integer
		If txtPickupID.Text = "" Then
			pickupID = 0
		Else
			pickupID = CInt(txtPickupID.Text)
		End If
		Dim addressID As Integer
		If txtAddressID.Text = "" Then
			addressID = 0
			Dim cnt As Integer = 0
			Dim message As String = ""
			cnt = SearchAddress("SpecialsNewAddress", Session("vUserID"), False, True, True, _
								txtAddress.Text, txtCity.Text, txtZIP.Text, message, addressID)
			If cnt < 0 Then
				ja(message)
				Return
			ElseIf cnt > 1 Then
				ja("The address entered matches more than one address.")
				Return
			ElseIf cnt = 1 Then
				If message <> "Address Created" Then
					ja("FYI: You entered this as a new address and yet the address matched an address in the database.")
				End If
			End If
		Else
			addressID = CInt(txtAddressID.Text)
		End If

		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupID", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, pickupID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@specialType", System.Data.ParameterDirection.Input, System.Data.DbType.String, rbSpecialType.Value))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@firstName", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtFirstName.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@lastName", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtLastName.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@phoneWork", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtWorkPhone.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@phoneHome", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtHomePhone.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@phoneMobile", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtMobilePhone.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@email", System.Data.ParameterDirection.Input, System.Data.DbType.String, RemoveEmailPunctuation(txtEmail.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@addressID", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, addressID))

		Dim promisedBags As Integer
		If txtPromisedBags.Text = "" Then
			promisedBags = 0
		Else
			promisedBags = CInt(txtPromisedBags.Text)
		End If
		Dim promisedBoxes As Integer
		If txtPromisedBoxes.Text = "" Then
			promisedBoxes = 0
		Else
			promisedBoxes = CInt(txtPromisedBoxes.Text)
		End If
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

		myCmd.Parameters.Add(DataUtil.CreateParameter("@streetAddress", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtAddress.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@city", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtCity.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@state", System.Data.ParameterDirection.Input, System.Data.DbType.String, "CA"))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@zip", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtZIP.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@crossStreet", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtCrossStreet.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@itemLocation", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlItemLocation.Text = " ", DBNull.Value, ddlItemLocation.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@languagePreference", System.Data.ParameterDirection.Input, System.Data.DbType.String, ddlLanguagePreference.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@itemBags", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(bags = 0, DBNull.Value, bags)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@itemBoxes", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(boxes = 0, DBNull.Value, boxes)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@itemOther", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtOther.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@promisedBags", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(promisedBags = 0, DBNull.Value, promisedBags)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@promisedBoxes", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(promisedBoxes = 0, DBNull.Value, promisedBoxes)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@promisedOther", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtPromisedOther.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@deviceName", System.Data.ParameterDirection.Input, System.Data.DbType.String, hfDeviceName.Value))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@driverID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(ddlDriver.SelectedValue)))
		hfDriverID.Value = ddlDriver.SelectedValue
		myCmd.Parameters.Add(DataUtil.CreateParameter("@reminder", System.Data.ParameterDirection.Input, System.Data.DbType.String, ckReminder.Checked))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@donorSource", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtDonorSource.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@receipt", System.Data.ParameterDirection.Input, System.Data.DbType.String, ckReceipt.Checked))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@grid", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtGrid.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@gate", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtGate.Text))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@driverLocation", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlLocation.Text = " ", DBNull.Value, ddlLocation.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@charityID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, IIf(IsNumeric(ddlCharity.SelectedValue), ddlCharity.SelectedValue, DBNull.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupDate", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(IsNothing(dtPickupDate.Value), DBNull.Value, dtPickupDate.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@startTime", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(IsNothing(dtStartTime.Value), DBNull.Value, dtStartTime.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@endTime", System.Data.ParameterDirection.Input, System.Data.DbType.DateTime, IIf(IsNothing(dtEndTime.Value), DBNull.Value, dtEndTime.Value)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@comment", System.Data.ParameterDirection.Input, System.Data.DbType.String, Replace(txtComment.Text, "'", "''")))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@grade", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlGrade.Text = " ", DBNull.Value, ddlGrade.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@status", System.Data.ParameterDirection.Input, System.Data.DbType.String, IIf(ddlStatus.Text = " ", DBNull.Value, ddlStatus.Text)))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@notReviewedEmailComment", System.Data.ParameterDirection.Input, System.Data.DbType.String, txtNotReviewed.Text))

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

		myCmd.Parameters.Add(DataUtil.CreateParameter("@newEmailAddressID", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))
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
				vbHandleProgramError(errorID, "Specials, SaveSpecial")
			End If
			Dim newEmailAddressID As Integer = CInt(myCmd.Parameters("@newEmailAddressID").Value.ToString())
			If newEmailAddressID > 0 Then
				Dim vAddressEmailID As Integer = newEmailAddressID
				v = RemoveEmailPunctuation(txtEmail.Text)
				Dim href As String = "http://sms.ecothrift.com/RCV_EmailConfirm.aspx?ID=" & newEmailAddressID
				Dim vBody As String = "<p>Thank you for providing us with your email address. It will not be shared with anyone under any circumstances.</p>" & _
					"<p>Please click on the link below to confirm your email.</p>" & _
					"<p><a href='" & href & "'>Click here to confirm</a></p>" & _
					"<p>Thank you,</p><p>Streamline Mail Systems</p>"
				Dim vSubject As String = "Confirm Email."
				Dim vPriority As String = ""
				'
				' Make sure v is set to rcomyn@comcast.net until we get the proper text in the email, etc.
				'
				If v <> "igor@zlimen.com" Then
					v = "rcomyn@comcast.net"
				End If
				'3/5/2012 - RCC: Igor, Joel, and I decided not to send confirming emails for the time being.
				'vbSendEmail(vAdminName, vAdminEmail, v, vSubject, vBody, vPriority, "HTML")
				'ja("Email sent.")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, myCmd.CommandText, ex.StackTrace, "Notify User", "Specials, SaveSpecial")
		Finally
			conn.Close()
		End Try

		'Repopulate the grid
		PopulateSpecialsGrid()
		PrepareSpecialsResults(gridPageSize)

		cmdSaveDisplay("enabled")
		cmdSave.Text = "Save Old Special"
		cmdShowGrid.Text = "Show Addresses"
		grdAddresses.Visible = False
		grdSpecials.Visible = True
		cmdNewSpecial.Enabled = True

		dsDriver.SelectCommand = hfDriver.Value
		ddlDriver.DataBind()
		ddlDriver.SelectedValue = hfDriverID.Value
	End Sub

    Protected Sub cmdNewSpecial_Click(sender As Object, e As System.EventArgs) Handles cmdNewSpecial.Click
        txtPickupID.Text = ""
        txtBags.Text = ""
        txtBoxes.Text = ""
        txtOther.Text = ""
        txtPromisedBags.Text = ""
        txtPromisedBoxes.Text = ""
        txtPromisedOther.Text = ""
		txtGate.Text = ""
		If txtAddressStatus.Text = "GATED" Then
			txtGate.BackColor = System.Drawing.Color.Violet
		Else
			txtGate.BackColor = System.Drawing.Color.White
		End If
		hfDeviceName.Value = ""
        ddlDriver.SelectedValue = 0
        dtPickupDate.Value = ""
        dtStartTime.Value = ""
        dtEndTime.Value = ""
        If Not ddlCharity.Enabled Then
            ddlCharity.Text = " "
            ddlCharity.Enabled = True
        End If
        ckReceipt.Checked = True
		ckReminder.Checked = True
        ddlGrade.Text = " "
        If rbSpecialType.Value = "PHONE" Then
            ddlStatus.Text = "SCHEDULED"
        Else
            ddlStatus.Text = "NOT REVIEWED"
        End If
        lblInitialStatus.Text = " "
		lblScheduledBy.Text = ""
		lblScheduledOn.Text = ""
		lblModifiedBy.Text = ""
		lblModifiedOn.Text = ""
		cmdSave.Text = "Save New Special"
        cmdSaveDisplay("new")
        cmdNewSpecial.Enabled = False
    End Sub

    Protected Sub cmdNewAddress_Click(sender As Object, e As System.EventArgs) Handles cmdNewAddress.Click
        ClearDetail()
        ddlCharity.Enabled = True
        cmdSave.Text = "Save New Special"
        cmdSaveDisplay("new")
        cmdNewSpecial.Enabled = False
        qryRouteSection.Text = ""
        qryNextScheduledPickup.Text = ""
    End Sub

    Protected Sub cmdDeleteSpecial_Click(sender As Object, e As System.EventArgs) Handles cmdDeleteSpecial.Click
        Dim conn As SqlConnection = New SqlConnection(vConnStr)

        Dim sql As String = "DELETE FROM [tblSpecials] WHERE [PickupID] = '" & txtPickupID.Text & "'"

        Dim myCmd As New SqlCommand(sql, conn)
        Try
            conn.Open()
            myCmd.ExecuteNonQuery()
            conn.Close()
        Catch ex As Exception
			LogProgramError(ex.Message, sql, ex.StackTrace, "Notify User", "Specials, cmdDeleteSpecial_Click")
        End Try

        'Repopulate the grid
        PopulateSpecialsGrid()
        PrepareSpecialsResults(gridPageSize)

        ClearDetail()
    End Sub

    Protected Sub ClearDetail()
        rbSpecialType.Value = "PHONE"
        txtPickupID.Text = ""
        txtAddress.Text = ""
        txtCrossStreet.Text = ""
        txtCity.Text = ""
        txtZIP.Text = ""
        txtAddressID.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtHomePhone.Text = ""
		txtWorkPhone.Text = ""
		txtMobilePhone.Text = ""
		txtEmail.Text = ""
		txtAddressStatus.Text = ""
		txtNearestSection.Text = ""
		ddlItemLocation.Text = " "
		ddlLanguagePreference.Text = "Not Specified"
		txtBags.Text = ""
        txtBoxes.Text = ""
        txtOther.Text = ""
        txtPromisedBags.Text = ""
        txtPromisedBoxes.Text = ""
        txtPromisedOther.Text = ""
        hfDeviceName.Value = ""
        ddlDriver.SelectedValue = 0
		ckReminder.Checked = True
        txtDonorSource.Text = ""
        dtPickupDate.Value = ""
        ddlLocation.Text = " "
        ddlCharity.Text = " "
        ddlCharity.Enabled = False
        ckReceipt.Checked = True
        txtGrid.Text = ""
		txtGate.Text = ""
		txtGate.BackColor = System.Drawing.Color.White
		txtComment.Text = ""
        ddlGrade.Text = " "
        ddlStatus.Text = " "
        lblInitialStatus.Text = " "
		txtNotReviewed.Text = ""
		txtDialerDate.Text = ""
		txtDialerStatus.Text = ""
		txtDialerComment.Text = ""
        divNotReviewed.Visible = False
		lblScheduledBy.Text = ""
		lblScheduledOn.Text = ""
		lblModifiedBy.Text = ""
		lblModifiedOn.Text = ""
		txtAddress.Enabled = True
        txtCity.Enabled = True
        txtZIP.Enabled = True
        cmdSaveDisplay("disabled")
    End Sub

    Protected Sub cmdUnreviewedSpecials_Click(sender As Object, e As System.EventArgs) Handles cmdUnreviewedSpecials.Click
        'apply only email criteria
        Dim where As String = GetWhere(OnlyNotReviewed:=True)
        BuildSql(where, OnlyNotReviewed:=True)
        PopulateSpecialsGrid()
        PopulateAddressesGrid()
        PrepareSpecialsResults(gridPageSize)
        PrepareAddressesResults(gridPageSize)
        grdSpecials.PageIndex = 0
        grdAddresses.PageIndex = 0
        ckSpecialsOnly.Checked = True
        PrepareQryResultsDisplay()
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

	Private Sub btnScheduleDetailSaveDisplay(ByVal state As String)
		If state = "disabled" Then
			btnScheduleDetailSave.Enabled = False
			btnScheduleDetailSave.BorderColor = Drawing.Color.Gray
		Else
			btnScheduleDetailSave.Enabled = True
			If state = "new" Then
				btnScheduleDetailSave.BorderColor = Drawing.Color.Red
			Else
				btnScheduleDetailSave.BorderColor = Drawing.Color.Gray
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
		sql = "SELECT TOP 1 [AddressID] FROM [tblSpecials] WHERE [PickupID] <> " & pickupID & " AND " & _
				"[PickupDate] = '" & FormatDateTime(pickupDate, DateFormat.ShortDate) & "' AND " & _
				"[Address] = '" & address & "' AND [City] = '" & city & "'"
		Try
			r = DataUtil.GetReader(conn, vConnStr, System.Data.CommandType.Text, sql, 90)
			If r.HasRows Then
				result = True
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, sql, ex.StackTrace, "Notify User", "Specials, CheckDuplicateSpecial")
		Finally
			r.Close()
			conn.Close()
		End Try

		Return result
	End Function

    Protected Sub cmdSpecialsSheet_Click(sender As Object, e As System.EventArgs) Handles cmdSpecialsSheet.Click
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

        Dim website As String = Mid(Request.Url.AbsoluteUri, 1, InStr(Request.Url.AbsoluteUri, "Specials.aspx") - 1)
        vParams &= "|website~" & website

		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Sheet Interactive" & vParams)
    End Sub

    Protected Sub cmdSummaryReport_Click(sender As Object, e As System.EventArgs) Handles cmdSummaryReport.Click
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
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Summary" & vParams)
    End Sub

    Protected Sub cmdAddressesForRouting_Click(sender As Object, e As System.EventArgs) Handles cmdAddressesForRouting.Click
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
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Addresses for Routing" & vParams)
    End Sub

    Protected Sub cmdConfirmRedTagReport_Click(sender As Object, e As System.EventArgs) Handles cmdConfirmRedTagReport.Click
		Dim x As Object
		Dim parms As String = ""
        x = calSpecials.SelectedDate
        parms = parms & "&PARMS=pickupDate~" & x & "|"
        x = ""
        For i = 0 To ddlLocations.Items.Count - 1
            If ddlLocations.Items(i).Selected Then
                x = x & ddlLocations.Items(i).Value
                Exit For
            End If
        Next
        If x = "" Then
            x = "0"
        End If
        parms = parms & "regionID~" & x & "|"
        x = ""
        For i = 0 To ddlCharities.Items.Count - 1
            If ddlCharities.Items(i).Selected Then
                x = x & ddlCharities.Items(i).Value
                Exit For
            End If
        Next
        If x = "" Then
            x = "0"
        End If
        parms = parms & "charityID~" & x
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Confirm and Do Not Red Tag Sheet" & parms)
    End Sub

    Protected Sub cmdSpecialsNotGradedReport_Click(sender As Object, e As System.EventArgs) Handles cmdSpecialsNotGradedReport.Click
        Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, Today) & _
            "|endDate~" & Today
        If IsNothing(ddlLocations.SelectedItem) OrElse (ddlLocations.SelectedItem.Text = "") Then
            parms &= "|driverLocationCode~All Locations"
        Else
            parms &= "|driverLocationCode~" & ddlLocations.SelectedItem.Text
        End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Not Graded" & parms)
    End Sub

    'RCC - 3/28/13 - I changed this from an ASPXButton to an asp:Button because it was not creating a postback
    Protected Sub SelectEmail_Click(sender As Object, e As EventArgs) Handles SelectEmail.Click
        If (gridEmails.GetSelectedFieldValues("Email").Count) > 0 Then
            txtEmail.Text = gridEmails.GetSelectedFieldValues("Email").Item(0).ToString()
            popID.ShowOnPageLoad = False
        End If
    End Sub

    Protected Sub btnNotReviewedConfirm_Click(sender As Object, e As System.EventArgs) Handles btnNotReviewedConfirm.Click
        If ddlLocation.Text = " " Then
            ja("Please select a Location.")
            Return
        End If

        If IsNothing(dtPickupDate.Value) Then
            ja("Please select a Pickup Date.")
            Return
        End If

        Dim body As String = ""

		If ddlCharity.SelectedItem.Text <> "DAV" And ddlCharity.SelectedItem.Text <> "VVA" And ddlCharity.SelectedItem.Text <> "UCP" Then
			ja("Charity must be 'DAV', 'UCP', or 'VVA' to send email confirmation.")
		End If

        body = "Thank you for the pickup you recently scheduled." & Environment.NewLine & Environment.NewLine & _
            "This is a confirmation that we will pick up your items on " & Format(dtPickupDate.Value, "MM/dd/yyyy") & _
            " between 8:00 AM and 5:00 PM from:" & Environment.NewLine & Environment.NewLine & _
            txtAddress.Text & Environment.NewLine & _
            txtCity.Text & ", CA  " & txtZIP.Text & Environment.NewLine & Environment.NewLine

        If txtNotReviewed.Text <> "" Then
            body += txtNotReviewed.Text & Environment.NewLine & Environment.NewLine
        End If

        body += "If anything in this confirmation is incorrect or, if you live in a gated community, " & _
            "please call our office immediately at "

        If ddlCharity.SelectedItem.Text = "DAV" Then
            body += "1-800-238-8387"
		ElseIf ddlCharity.SelectedItem.Text = "VVA" Then
			body += "1-866-241-8387"
		ElseIf ddlCharity.SelectedItem.Text = "UCP" Then
			body += "1-855-228-1190"
		End If

        body += " or just reply to this email." & Environment.NewLine & Environment.NewLine

        body += "Please remember:" & Environment.NewLine
        body += "     1. Place your donation, RAIN or SHINE, marked for us, where it is clearly visible from the street " & _
            "by 8:00 AM on the day of your pickup." & Environment.NewLine
        body += "     2. You do not need to be home." & Environment.NewLine
        body += "     3. The driver will leave a tax receipt at the time of pickup." & Environment.NewLine & Environment.NewLine

        body = AddEmailReminders(body)

        body += "Thank you very much for supporting our Veterans!" & Environment.NewLine & Environment.NewLine

        If ddlCharity.SelectedItem.Text = "DAV" Then
            body += "Disabled American Veterans" & Environment.NewLine & "http://donatedav.org" & Environment.NewLine
        ElseIf ddlCharity.SelectedItem.Text = "VVA" Then
            body += "Vietnam Veterans of America" & Environment.NewLine & "http://givetoveterans.org" & Environment.NewLine
		ElseIf ddlCharity.SelectedItem.Text = "UCP" Then
			body += "United Cerebral Palsy" & Environment.NewLine & "http://www.donateucpgg.org" & Environment.NewLine
		End If

        ddlStatus.Text = "SCHEDULED"
        Dim retValue = ""
        If ddlCharity.SelectedItem.Text = "DAV" Then
            retValue = vbSendEmail("DonateDAV.org", "info@donatedav.org", txtEmail.Text, "Pickup Confirmation", body, "", "Text")
		ElseIf ddlCharity.SelectedItem.Text = "VVA" Then
			retValue = vbSendEmail("GiveToVeterans.org", "info@givetoveterans.org", txtEmail.Text, "Pickup Confirmation", body, "", "Text")
		ElseIf ddlCharity.SelectedItem.Text = "UCP" Then
			retValue = vbSendEmail("DonateUCPGG.org", "info@donateucpgg.org", txtEmail.Text, "Pickup Confirmation", body, "", "Text")
		End If
        If retValue = "Your email has been sent." Then
            If txtComment.Text <> "" Then
                txtComment.Text += Environment.NewLine
            End If
            txtComment.Text += "Email confirmation: " & Format(Now(), "MM/dd/yyyy hh:mm:ss tt")
            SaveSpecial()
            ja("Email sent and Special Saved.")
        Else
            ja("Email NOT sent and Special NOT Saved.")
        End If

    End Sub

    Protected Sub btnNotReviewedChange_Click(sender As Object, e As System.EventArgs) Handles btnNotReviewedChange.Click
        If ddlLocation.Text = " " Then
            ja("Please select a Location.")
            Return
        End If

        If IsNothing(dtPickupDate.Value) Then
            ja("Please select a Pickup Date.")
            Return
        End If

        Dim body As String = ""

		If ddlCharity.SelectedItem.Text <> "DAV" And ddlCharity.SelectedItem.Text <> "VVA" And ddlCharity.SelectedItem.Text <> "UCP" Then
			ja("Charity must be 'DAV', 'UCP', or 'VVA' to send email confirmation.")
		End If

        body = "Thank you for the pickup you recently scheduled for:" & Environment.NewLine & Environment.NewLine & _
            txtAddress.Text & Environment.NewLine & _
            txtCity.Text & ", CA  " & txtZIP.Text & Environment.NewLine & Environment.NewLine

        body += "We do apologize that we do not have a driver in your area on the date you requested but we can schedule " & _
            "a pickup for " & Format(dtPickupDate.Value, "MM/dd/yyyy") & ". Will that be okay? Please advise." & Environment.NewLine & Environment.NewLine

        If txtNotReviewed.Text <> "" Then
            body += txtNotReviewed.Text & Environment.NewLine & Environment.NewLine
        End If

        body += "Your pickup will not be scheduled until you reply to this email or call our office at "

        If ddlCharity.SelectedItem.Text = "DAV" Then
            body += "1-800-238-8387"
		ElseIf ddlCharity.SelectedItem.Text = "VVA" Then
			body += "1-866-241-8387"
		ElseIf ddlCharity.SelectedItem.Text = "UCP" Then
			body += "1-855-228-1190"
		End If

        body += "." & Environment.NewLine & Environment.NewLine

        body += "Thank you very much for supporting our Veterans!" & Environment.NewLine & Environment.NewLine

        If ddlCharity.SelectedItem.Text = "DAV" Then
            body += "Disabled American Veterans" & Environment.NewLine & "http://donatedav.org" & Environment.NewLine
		ElseIf ddlCharity.SelectedItem.Text = "VVA" Then
			body += "Vietnam Veterans of America" & Environment.NewLine & "http://givetoveterans.org" & Environment.NewLine
		ElseIf ddlCharity.SelectedItem.Text = "UCP" Then
			body += "United Cerebral Palsy" & Environment.NewLine & "http://www.donateucpgg.org" & Environment.NewLine
		End If

        ddlStatus.Text = "SCHEDULED"
        Dim retValue = ""
        If ddlCharity.SelectedItem.Text = "DAV" Then
            retValue = vbSendEmail("DonateDAV.org", "info@donatedav.org", txtEmail.Text, "Pickup Change Request", body, "", "Text")
		ElseIf ddlCharity.SelectedItem.Text = "VVA" Then
			retValue = vbSendEmail("GiveToVeterans.org", "info@givetoveterans.org", txtEmail.Text, "Pickup Change Request", body, "", "Text")
		ElseIf ddlCharity.SelectedItem.Text = "UCP" Then
			retValue = vbSendEmail("DonateUCPGG.org", "info@donateucpgg.org", txtEmail.Text, "Pickup Change Request", body, "", "Text")
		End If
        If retValue = "Your email has been sent." Then
            If txtComment.Text <> "" Then
                txtComment.Text += Environment.NewLine
            End If
            txtComment.Text += "Email change: " & Format(Now(), "MM/dd/yyyy hh:mm:ss tt")
            SaveSpecial()
            ja("Email sent and Special Saved.")
        Else
            ja("Email NOT sent and Special NOT Saved.")
        End If

    End Sub

    Protected Sub btnNotReviewedCancel_Click(sender As Object, e As System.EventArgs) Handles btnNotReviewedCancel.Click
        Dim body As String = ""

		If ddlCharity.SelectedItem.Text <> "DAV" And ddlCharity.SelectedItem.Text <> "VVA" And ddlCharity.SelectedItem.Text <> "UCP" Then
			ja("Charity must be 'DAV', 'UCP', or 'VVA' to send email confirmation.")
		End If

        body = "Thank you for the pickup you recently scheduled. We regret that we will be unable to make the pickup." & _
            Environment.NewLine & Environment.NewLine

        If txtNotReviewed.Text <> "" Then
            body += txtNotReviewed.Text & Environment.NewLine & Environment.NewLine
        End If

        body = AddEmailReminders(body)

        body += "Thank you very much for supporting our Veterans!" & Environment.NewLine & Environment.NewLine
        If ddlCharity.SelectedItem.Text = "DAV" Then
            body += "Disabled American Veterans" & Environment.NewLine & "http://donatedav.org" & Environment.NewLine
        ElseIf ddlCharity.SelectedItem.Text = "VVA" Then
            body += "Vietnam Veterans of America" & Environment.NewLine & "http://givetoveterans.org" & Environment.NewLine
		ElseIf ddlCharity.SelectedItem.Text = "UCP" Then
			body += "United Cerebral Palsy" & Environment.NewLine & "http://www.donateucpgg.org" & Environment.NewLine
		End If

        ddlStatus.Text = "CANCELED"
        Dim retValue As String = ""
        If ddlCharity.SelectedItem.Text = "DAV" Then
            retValue = vbSendEmail("DonateDAV.org", "info@donatedav.org", txtEmail.Text, "Pickup Cancellation", body, "", "Text")
        ElseIf ddlCharity.SelectedItem.Text = "VVA" Then
            retValue = vbSendEmail("GiveToVeterans.org", "info@givetoveterans.org", txtEmail.Text, "Pickup Cancellation", body, "", "Text")
		ElseIf ddlCharity.SelectedItem.Text = "UCP" Then
			retValue = vbSendEmail("DonateUCPGG.org", "info@donateucpgg.org", txtEmail.Text, "Pickup Cancellation", body, "", "Text")
		End If

        If retValue = "Your email has been sent." Then
            If txtComment.Text <> "" Then
                txtComment.Text += Environment.NewLine
            End If
            txtComment.Text += "Email cancellation: " & Format(Now(), "MM/dd/yyyy hh:mm:ss tt")
            SaveSpecial()
            ja("Email sent and Special Canceled.")
        Else
            ja("Email NOT sent and Special NOT Canceled.")
        End If

    End Sub

    Private Function AddEmailReminders(ByVal body As String) As String
        body += "As a reminder, we are not able to pickup these types of items:" & Environment.NewLine
		body += "     . Furniture" & Environment.NewLine
        body += "     . Large Appliances (over 25 pounds)" & Environment.NewLine
        body += "     . Carpeting" & Environment.NewLine
        body += "     . Building Materials" & Environment.NewLine
        body += "     . Paints" & Environment.NewLine
        body += "     . Fuels" & Environment.NewLine
        body += "     . Solvents" & Environment.NewLine
        body += "     . Mattresses or Box Springs" & Environment.NewLine
        body += "     . TVs or Computer Monitors" & Environment.NewLine
        body += Environment.NewLine

        Return body
    End Function

End Class
