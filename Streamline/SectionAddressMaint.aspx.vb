Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DevExpress.XtraPrinting
Imports DataUtil

Partial Class SectionAddressMaint
    Inherits System.Web.UI.Page
    Private UnselectDate As Boolean = False
    Private MonthChanged As Boolean = False

    Private grdSectionAddressesPageSize As Integer = 20

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
						Case "btnSelectSectionAddresses"
						Case "btnSelectAddressesToAdd"
						Case "ckActiveTemplates"
							LoadTemplates()
						Case "ckInactiveTemplates"
							LoadTemplates()
						Case "ckInactiveSections"
							hfShowInactiveSections.Value = ckInactiveSections.Value
							LoadTemplates()
						Case Else
							LoadComboBoxes()
							LoadSectionAddresses()
							LoadAddressesToAdd()
					End Select
				End If

			Case "GET"
				ckActiveTemplates.Checked = True
				ckInactiveTemplates.Checked = False
				ckInactiveSections.Checked = False
				hfShowInactiveSections.Value = False

				If Session("SectionAddressesAddAddresses") = "Update" Then
					btnSelectedAddressesAdd.Enabled = True
				Else
					btnSelectedAddressesAdd.Enabled = False
				End If

				LoadTemplates()
				hfUncommittedChanges.Value = 0
			Case "HEAD"
				'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
		End Select

		If hfUncommittedChanges.Value = 1 Then
			lblUcommittedChanges.Text = "This route has Uncommitted Changes"
		Else
			lblUcommittedChanges.Text = ""
		End If
    End Sub

    Private Shared Function SelectRoutes(ByVal templateID As Integer) As String
        Dim sql As String
        If templateID = 99 Then
			sql = "SELECT DISTINCT R.RouteID, R.RouteCode " & _
				"FROM tblRoutes R " &
				"LEFT JOIN tblPickupCycleTemplatesDetail PCTD ON PCTD.RouteID = R.RouteID " &
				"WHERE PCTD.PickupCycleTemplateID IS NULL " & _
				"ORDER BY R.RouteCode"
        Else
			sql = "SELECT DISTINCT R.RouteID, R.RouteCode " & _
				"FROM tblRoutes R " &
				"LEFT JOIN tblPickupCycleTemplatesDetail PCTD ON PCTD.RouteID = R.RouteID " &
				"WHERE PCTD.PickupCycleTemplateID = " & templateID & " " & _
				"ORDER BY R.RouteCode"
        End If

        Return (sql)
    End Function

    Public Class Route
        Public RouteId As Integer
        Public RouteCode As String
	End Class

    <WebMethod()> _
    Public Shared Function GetRoutes(ByVal templateID As Integer) As Route()
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim result As Boolean = False
        Dim r As System.Data.SqlClient.SqlDataReader = Nothing
        Dim routes As List(Of Route) = New List(Of Route)
        Dim sql As String = SelectRoutes(templateID)

        Try
            r = DataUtil.GetReader(conn, vConnStr, System.Data.CommandType.Text, sql, 90)
            While r.Read()
                Dim rte As Route = New Route
                rte.RouteId = CInt(r.Item("RouteID").ToString)
                rte.RouteCode = r.Item("RouteCode").ToString
				routes.Add(rte)
            End While
        Catch ex As Exception
            routes = Nothing
        Finally
            r.Close()
            conn.Close()
        End Try

        Return routes.ToArray()
    End Function

	Private Shared Function SelectSections(ByVal routeID As Integer, ByVal showInactiveSections As Boolean) As String
		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim r As System.Data.SqlClient.SqlDataReader = Nothing
		Dim uncommittedChanges As Boolean = False
		Dim sql As String = "SELECT UncommittedChanges " & _
			"FROM tblRoutes " & _
			"WHERE RouteID = " & routeID

		Try
			r = DataUtil.GetReader(conn, vConnStr, System.Data.CommandType.Text, sql, 90)
			While r.Read()
				uncommittedChanges = CBool(r.Item("UncommittedChanges").ToString)
			End While
		Catch ex As Exception
			uncommittedChanges = False
		Finally
			r.Close()
			conn.Close()
		End Try

		sql = "SELECT S.SectionID, S.SectionCode, R.UncommittedChanges " & _
			"FROM tblSections S " &
			"INNER JOIN tblRoutes R ON R.RouteID = S.RouteID "

		If uncommittedChanges Then
			sql += "WHERE S.UncommittedRouteID = " & routeID & " "
		Else
			sql += "WHERE S.RouteID = " & routeID & " "
		End If

		If Not showInactiveSections Then
			sql += "AND S.Active = 1 "
		End If
		sql += "ORDER BY S.SectionCode"
		Return (sql)
	End Function

    Public Class Section
        Public SectionId As Integer
		Public SectionCode As String
		Public UncommittedChanges As Boolean
	End Class

	<WebMethod()> _
	Public Shared Function GetSections(ByVal routeID As Integer, ByVal showInactiveSections As Boolean) As Section()
		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim result As Boolean = False
		Dim r As System.Data.SqlClient.SqlDataReader = Nothing
		Dim sections As List(Of Section) = New List(Of Section)
		Dim sql As String = SelectSections(routeID, showInactiveSections)

		Try
			r = DataUtil.GetReader(conn, vConnStr, System.Data.CommandType.Text, sql, 90)
			While r.Read()
				Dim sec As Section = New Section
				sec.SectionId = CInt(r.Item("SectionID").ToString)
				sec.SectionCode = r.Item("SectionCode").ToString
				sec.UncommittedChanges = CBool(r.Item("UncommittedChanges").ToString)
				sections.Add(sec)
			End While
		Catch ex As Exception
			sections = Nothing
		Finally
			r.Close()
			conn.Close()
		End Try

		Return sections.ToArray()
	End Function

    <WebMethod()> _
    Public Shared Function GetOtherRouteTemplates(ByVal templateID As Integer, ByVal routeID As Integer) As String
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim result As String = ""
        Dim r As System.Data.SqlClient.SqlDataReader = Nothing
        Dim sections As List(Of Section) = New List(Of Section)
        Dim sql As String = "SELECT PCT.PickupCycleTemplateCode " & _
            "FROM tblPickupCycleTemplates AS PCT " & _
            "INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
            "INNER JOIN tblRoutes AS R ON R.RouteID = PCTD.RouteID " & _
            "WHERE PCT.PickupCycleTemplateID <> " & templateID & " " & _
                "AND R.RouteID = " & routeID
        Try
            r = DataUtil.GetReader(conn, vConnStr, System.Data.CommandType.Text, sql, 90)
            While r.Read()
                If result = "" Then
                    result = "Other Template(s) with this Route: "
                Else
                    result += ", "
                End If
                result += r.Item("PickupCycleTemplateCode").ToString
            End While
        Catch ex As Exception
            result = Nothing
        Finally
            r.Close()
            conn.Close()
        End Try

        Return result
    End Function

	Private Shared Function SelectSectionAddresses(ByVal sectionID As Integer, ByVal uc As Boolean) As String
		Dim sql As String = "SELECT AddressID, StreetAddress, StreetName, City, Zip, ST.Status, status_ AS AccuZipStatus, crrt, Mail_OK, Email_OK, Bag_OK " & _
			"FROM tblAddresses A " & _
			"LEFT OUTER JOIN tlkStatuses ST ON ST.StatusID = A.StatusID "

		If uc Then
			sql += "WHERE A.UncommittedSectionID = " & sectionID & " "
		Else
			sql += "WHERE A.SectionID = " & sectionID & " "
		End If

		sql += "ORDER BY City, StreetName, StreetNumberValue"
		Return (sql)
	End Function

    Protected Sub ddlSections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSections.SelectedIndexChanged
        grdSectionAddresses.PageIndex = 0
        DeselectSectionAddresses()
        LoadSectionAddresses()
    End Sub

	Private Function SelectAddressesToAdd(ByVal city As String, ByVal street As String, ByVal zip As String, ByVal crrt As String) As String
		Dim sql As String = "SELECT AddressID, StreetAddress, StreetName, City, Zip, ST.Status, status_ AS AccuZipStatus, crrt, Mail_OK, Email_OK, Bag_OK " & _
			"FROM tblAddresses A "

		If hfUncommittedChanges.Value = 0 Then
			sql += "LEFT OUTER JOIN tblSections S ON S.SectionID = A.SectionID " & _
			"LEFT OUTER JOIN tblRoutes R ON R.RouteID = S.RouteID " & _
			"LEFT OUTER JOIN tlkStatuses ST ON ST.StatusID = A.StatusID " & _
			"WHERE A.SectionID IS NULL "
		Else
			sql += "LEFT OUTER JOIN tblSections S ON S.SectionID = A.UncommittedSectionID " & _
			"LEFT OUTER JOIN tblRoutes R ON R.RouteID = S.UncommittedRouteID " & _
			"LEFT OUTER JOIN tlkStatuses ST ON ST.StatusID = A.StatusID " & _
			"WHERE A.UncommittedSectionID IS NULL "
		End If

		If Trim(city) <> "" Then
			sql += "AND A.City LIKE '" & city & "%' "
		End If
		If Trim(street) <> "" Then
			If IsStreetNumber(street) Then
				sql += "AND A.StreetAddress LIKE '" & street & "%' "
			Else
				sql += "AND A.StreetName LIKE '" & street & "%' "

			End If
		End If
		If Trim(crrt) <> "" Then
			sql += "AND crrt LIKE '" & crrt & "%' "
		End If
		If Trim(zip) <> "" Then
			sql += "AND SUBSTRING(A.Zip, 1, 5) LIKE '" & zip & "%' "
		End If
		sql += "ORDER BY City, StreetName, StreetNumberValue"
		Return (sql)
	End Function

	Private Function SelectAddressesToAddInfo(ByVal city As String, ByVal street As String, ByVal zip As String, ByVal crrt As String) As String
		Dim status As String = ""
		If ckStatusGroup.Checked Then
			status = "ST.[Status] "
		Else
			status = "'All' "
		End If

		Dim sql As String = "SELECT DISTINCT A.StreetName, " & _
			"MIN(A.StreetNumber) OVER(PARTITION BY StreetName, " & status & ") AS MinStreetNumber, " & _
			"MAX(A.StreetNumber) OVER(PARTITION BY StreetName, " & status & ") AS MaxStreetNumber, " & _
			status & "AS [Status], " & _
			"COUNT(A.StreetNumber) OVER(PARTITION BY StreetName, " & status & ") AS [Count] " & _
			"FROM tblAddresses AS A "

		If hfUncommittedChanges.Value = 0 Then
			sql += "LEFT OUTER JOIN tblSections S ON S.SectionID = A.SectionID " & _
			"LEFT OUTER JOIN tblRoutes R ON R.RouteID = S.RouteID " & _
			"LEFT OUTER JOIN tlkStatuses ST ON ST.StatusID = A.StatusID " & _
			"WHERE A.SectionID IS NULL "
		Else
			sql += "LEFT OUTER JOIN tblSections S ON S.SectionID = A.UncommittedSectionID " & _
			"LEFT OUTER JOIN tblRoutes R ON R.RouteID = S.UncommittedRouteID " & _
			"LEFT OUTER JOIN tlkStatuses ST ON ST.StatusID = A.StatusID " & _
			"WHERE A.UncommittedSectionID IS NULL "
		End If

		If Trim(city) <> "" Then
			sql &= "AND A.City LIKE '" & city & "%' "
		End If
		If Trim(street) <> "" Then
			If IsStreetNumber(street) Then
				sql &= "AND A.StreetAddress LIKE '" & street & "%' "
			Else
				sql &= "AND A.StreetName LIKE '" & street & "%' "

			End If
		End If
		If Trim(crrt) <> "" Then
			sql += "AND crrt LIKE '" & crrt & "%' "
		End If
		If Trim(zip) <> "" Then
			sql &= "AND SUBSTRING(A.Zip, 1, 5) LIKE '" & zip & "%' "
		End If
		sql &= "ORDER BY A.StreetName, [Status] "
		Return (sql)
	End Function

    Private Sub LoadTemplates()
        txtSQL_ddlTemplates.Text = "SELECT PickupCycleTemplateID, PickupCycleTemplateCode " & _
            "FROM tblPickupcycleTemplates "

        If Not ckActiveTemplates.Checked And Not ckInactiveTemplates.Checked Then
            ckActiveTemplates.Checked = True ' Don't let user uncheck both boxes
        End If

        If ckActiveTemplates.Checked And Not ckInactiveTemplates.Checked Then
            txtSQL_ddlTemplates.Text &= "WHERE Active = 1 "
        ElseIf Not ckActiveTemplates.Checked And ckInactiveTemplates.Checked Then
            txtSQL_ddlTemplates.Text &= "WHERE Active = 0 "
        End If

        txtSQL_ddlTemplates.Text &= "ORDER BY PickupCycleTemplateCode"

        Dim da As SqlDataAdapter = New SqlDataAdapter(txtSQL_ddlTemplates.Text, vConnStr)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        ddlTemplates.DataSource = dt
        ddlTemplates.DataTextField = "PickupCycleTemplateCode"
        ddlTemplates.DataValueField = "PickupCycleTemplateID"
        ddlTemplates.DataBind()
        ddlTemplates.Items.Insert(0, New ListItem("Select Template", "0"))

        ddlTemplatesMove.DataSource = dt
        ddlTemplatesMove.DataTextField = "PickupCycleTemplateCode"
        ddlTemplatesMove.DataValueField = "PickupCycleTemplateID"
        ddlTemplatesMove.DataBind()
        ddlTemplatesMove.Items.Insert(0, New ListItem("Select Template", "0"))

        ddlTemplatesMoveSection.DataSource = dt
        ddlTemplatesMoveSection.DataTextField = "PickupCycleTemplateCode"
        ddlTemplatesMoveSection.DataValueField = "PickupCycleTemplateID"
        ddlTemplatesMoveSection.DataBind()
        ddlTemplatesMoveSection.Items.Insert(0, New ListItem("Select Template", "0"))

        If ckInactiveTemplates.Checked Then
            ddlTemplates.Items.Insert(1, New ListItem("<Routes w/o Template>", "99"))
            ddlTemplatesMove.Items.Insert(1, New ListItem("<Routes w/o Template>", "99"))
            ddlTemplatesMoveSection.Items.Insert(1, New ListItem("<Routes w/o Template>", "99"))
        End If
    End Sub

    Private Sub LoadComboBoxes()
        ' Routes
        Dim da As SqlDataAdapter = New SqlDataAdapter(SelectRoutes(ddlTemplates.SelectedValue), vConnStr)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        ddlRoutes.DataSource = dt
        ddlRoutes.DataTextField = "RouteCode"
        ddlRoutes.DataValueField = "RouteID"
        ddlRoutes.DataBind()
        ddlRoutes.Items.Insert(0, New ListItem("Select Route", "0"))
        ddlRoutes.SelectedValue = hfSelectedRouteID.Value

        ' Sections
		da = New SqlDataAdapter(SelectSections(ddlRoutes.SelectedValue, ckInactiveTemplates.Value), vConnStr)
        dt = New DataTable()
        da.Fill(dt)
        ddlSections.DataSource = dt
        ddlSections.DataTextField = "SectionCode"
        ddlSections.DataValueField = "SectionID"
        ddlSections.DataBind()
        ddlSections.Items.Insert(0, New ListItem("Select Section", "0"))
        ddlSections.SelectedValue = hfSelectedSectionID.Value

        ' Status
        da = New SqlDataAdapter("SELECT StatusID, Status FROM tlkStatuses ORDER BY Status", vConnStr)
        dt = New DataTable()
        da.Fill(dt)
        ddlStatus.DataSource = dt
        ddlStatus.DataTextField = "Status"
        ddlStatus.DataValueField = "StatusID"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Select Status", "0"))
        If hfSelectedStatus.Value = "" Then hfSelectedStatus.Value = "0"
        ddlStatus.SelectedValue = hfSelectedStatus.Value

        ' Status Addresses to Add
        da = New SqlDataAdapter("SELECT StatusID, Status FROM tlkStatuses ORDER BY Status", vConnStr)
        dt = New DataTable()
        da.Fill(dt)
        ddlStatusAdd.DataSource = dt
        ddlStatusAdd.DataTextField = "Status"
        ddlStatusAdd.DataValueField = "StatusID"
        ddlStatusAdd.DataBind()
        ddlStatusAdd.Items.Insert(0, New ListItem("Select Status", "0"))
        If hfSelectedStatusAdd.Value = "" Then hfSelectedStatusAdd.Value = "0"
        ddlStatusAdd.SelectedValue = hfSelectedStatusAdd.Value

        tblSectionAddressParameters.Attributes.Add("style", "visibility:visible")

        ' RoutesMove
        da = New SqlDataAdapter(SelectRoutes(ddlTemplatesMove.SelectedValue), vConnStr)
        dt = New DataTable()
        da.Fill(dt)
        ddlRoutesMove.DataSource = dt
        ddlRoutesMove.DataTextField = "RouteCode"
        ddlRoutesMove.DataValueField = "RouteID"
        ddlRoutesMove.DataBind()
        ddlRoutesMove.Items.Insert(0, New ListItem("Select Route", "0"))
        Try
            ddlRoutesMove.SelectedValue = hfSelectedRouteIDMove.Value
        Catch ex As Exception
        End Try

        ' SectionsMove
		da = New SqlDataAdapter(SelectSections(ddlRoutesMove.SelectedValue, ckInactiveSections.Value), vConnStr)
        dt = New DataTable()
        da.Fill(dt)
        ddlSectionsMove.DataSource = dt
        ddlSectionsMove.DataTextField = "SectionCode"
        ddlSectionsMove.DataValueField = "SectionID"
        ddlSectionsMove.DataBind()
        ddlSectionsMove.Items.Insert(0, New ListItem("Select Section", "0"))
        Try
            ddlSectionsMove.SelectedValue = hfSelectedSectionIDMove.Value
        Catch ex As Exception
        End Try

        ' RoutesMoveSection
        da = New SqlDataAdapter(SelectRoutes(ddlTemplatesMoveSection.SelectedValue), vConnStr)
        dt = New DataTable()
        da.Fill(dt)
        ddlRoutesMoveSection.DataSource = dt
        ddlRoutesMoveSection.DataTextField = "RouteCode"
        ddlRoutesMoveSection.DataValueField = "RouteID"
        ddlRoutesMoveSection.DataBind()
        ddlRoutesMoveSection.Items.Insert(0, New ListItem("Select Route", "0"))
        Try
            ddlRoutesMoveSection.SelectedValue = hfSelectedRouteIDMoveSection.Value
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadSectionAddresses()
        Dim selectionList As List(Of Object) = grdSectionAddresses.GetSelectedFieldValues("AddressID")
        selCountSectionAddresses.InnerHtml = selectionList.Count

        If ddlSections.SelectedValue = "" Or ddlSections.SelectedValue = "0" Then
            Return
        End If

		Dim uc As Boolean = False
		If hfUncommittedChanges.Value = 1 Then
			uc = True
		End If

        'tblSectionAddresses.Attributes.Add("style", "visibility:visible")
        With dsSectionAddresses
            .ConnectionString = vConnStr
			.SelectCommand = SelectSectionAddresses(ddlSections.SelectedValue, uc)
        End With
        grdSectionAddresses.DataBind()
        'If grdSectionAddresses.VisibleRowCount = 0 Then
        '    tblSectionAddresses.Attributes.Add("style", "visibility:hidden")
        'End If
    End Sub

    Private Sub LoadAddressesToAdd()
        If txtAddressCity.Text = "" AndAlso txtAddressStreet.Text = "" AndAlso txtAddressZip.Text = "" Then
            Return
        End If

        Dim selectionList As List(Of Object) = grdAddressesToAdd.GetSelectedFieldValues("AddressID")
        selCountAddressesToAdd.InnerHtml = selectionList.Count

        tblAddressesToAdd.Attributes.Add("style", "visibility:visible")
        tblAddressesToAddInfo.Attributes.Add("style", "visibility:visible")

        With dsAddressesToAdd
            .ConnectionString = vConnStr
			.SelectCommand = SelectAddressesToAdd(txtAddressCity.Text, txtAddressStreet.Text, txtAddressZip.Text, txtAddressCRRT.Text)
        End With
        grdAddressesToAdd.DataBind()

        With dsAddressesToAddInfo
            .ConnectionString = vConnStr
			.SelectCommand = SelectAddressesToAddInfo(txtAddressCity.Text, txtAddressStreet.Text, txtAddressZip.Text, txtAddressCRRT.Text)
        End With
        grdAddressesToAddInfo.DataBind()

		If grdAddressesToAdd.VisibleRowCount = 0 Then
			tblAddressesToAdd.Attributes.Add("style", "visibility:hidden")
			tblAddressesToAddInfo.Attributes.Add("style", "visibility:hidden")
		End If
    End Sub

	Protected Sub btnSectionAddressesDeselect_Click(sender As Object, e As EventArgs) Handles btnSectionAddressesDeselect.Click
		DeselectSectionAddresses()
	End Sub

    Private Sub DeselectSectionAddresses()
        grdSectionAddresses.Selection.UnselectAll()
        txtSectionAddressCity.Text = ""
		txtSectionAddressStreet.Text = ""
		txtSectionAddressCRRT.Text = ""
	End Sub

    Private Sub DeselectAddressesToAdd()
        grdAddressesToAdd.Selection.UnselectAll()
    End Sub

    Private Sub SelectAddressesToAdd()
        For i As Integer = 0 To grdAddressesToAdd.VisibleRowCount - 1
            grdAddressesToAdd.Selection.SelectRow(i)
        Next
    End Sub

    Protected Sub btnSectionAddressesSelect_Click(sender As Object, e As EventArgs) Handles btnSectionAddressesSelect.Click
        PopulateSectionAddressCheckboxes()
    End Sub

    Protected Sub btnSelectAddressesToAdd_Click(sender As Object, e As EventArgs) Handles btnSelectAddressesToAdd.Click
        btnSelectedAddressesAdd.Visible = True
        grdAddressesToAdd.PageIndex = 0
        DeselectAddressesToAdd()


        Dim removeList As List(Of Object) = grdAddressesToAdd.GetSelectedFieldValues("AddressID")

        LoadAddressesToAdd()
        SelectAddressesToAdd()

        tblAddressesToAddParameters.Attributes.Add("style", "visibility:visible")
    End Sub

    Private Sub PopulateSectionAddressCheckboxes()
		If txtSectionAddressCity.Text = "" AndAlso txtSectionAddressStreet.Text = "" AndAlso txtSectionAddressCRRT.Text = "" Then
			Return
		End If

		Dim city As String = ""
		Dim street As String = ""
		Dim crrt As String = ""
		Try
			For i As Integer = 0 To grdSectionAddresses.VisibleRowCount - 1
				city = grdSectionAddresses.GetRowValues(i, "City").ToString()
				street = grdSectionAddresses.GetRowValues(i, "StreetAddress").ToString()
				crrt = grdSectionAddresses.GetRowValues(i, "crrt").ToString()
				Dim cOK As Boolean = False
				Dim sOK As Boolean = False
				Dim rOK As Boolean = False
				If txtSectionAddressCity.Text.Trim() = "" Then
					cOK = True
				Else
					If InStr(city.Trim().ToUpper(), txtSectionAddressCity.Text.Trim().ToUpper()) > 0 Then
						cOK = True
					End If
				End If
				If txtSectionAddressStreet.Text.Trim() = "" Then
					sOK = True
				Else
					If InStr(street.Trim().ToUpper(), txtSectionAddressStreet.Text.Trim().ToUpper()) > 0 Then
						sOK = True
					End If
				End If
				If txtSectionAddressCRRT.Text.Trim() = "" Then
					rOK = True
				Else
					If InStr(crrt.Trim().ToUpper(), txtSectionAddressCRRT.Text.Trim().ToUpper()) > 0 Then
						rOK = True
					End If
				End If
				If cOK And sOK And rOK Then
					grdSectionAddresses.Selection.SelectRow(i)
				End If
			Next
		Catch ex As Exception

		End Try
	End Sub

    Private Sub PopulateAddressesToAddCheckboxes()
        If txtAddressCity.Text = "" AndAlso txtAddressStreet.Text = "" Then
            Return
        End If

        For i As Integer = 0 To grdAddressesToAdd.VisibleRowCount - 1
            Dim city As String = grdAddressesToAdd.GetRowValues(i, "City").ToString()
            Dim street As String = grdAddressesToAdd.GetRowValues(i, "StreetAddress").ToString()
			Dim crrt As String = grdSectionAddresses.GetRowValues(i, "crrt").ToString()
			Dim cOK As Boolean = False
            Dim sOK As Boolean = False
			Dim rOK As Boolean = False
			If txtAddressCity.Text.Trim() = "" Then
				cOK = True
			Else
				If InStr(city.Trim().ToUpper(), txtAddressCity.Text.Trim().ToUpper()) > 0 Then
					cOK = True
				End If
			End If
            If txtAddressStreet.Text.Trim() = "" Then
                sOK = True
            Else
                If InStr(street.Trim().ToUpper(), txtAddressStreet.Text.Trim().ToUpper()) > 0 Then
                    sOK = True
                End If
            End If
			If txtSectionAddressCRRT.Text.Trim() = "" Then
				rOK = True
			Else
				If InStr(crrt.Trim().ToUpper(), txtAddressCRRT.Text.Trim().ToUpper()) > 0 Then
					rOK = True
				End If
			End If
			If cOK And sOK And rOK Then
				grdAddressesToAdd.Selection.SelectRow(i)
			End If
        Next
    End Sub

    Protected Sub grdSectionAddresses_DataBound(sender As Object, e As EventArgs) Handles grdSectionAddresses.DataBound
        PopulateSectionAddressCheckboxes()
    End Sub

    Protected Sub grdAddressesToAdd_DataBound(sender As Object, e As EventArgs) Handles grdAddressesToAdd.DataBound
        PopulateSectionAddressCheckboxes()
    End Sub

    Sub OnSectionAddressesDataBound(g As ASPxGridView, e As EventArgs)
        If g.Columns.Count > 0 And (g.Columns("grdCommands") Is Nothing) Then
            Dim gvcol As New DevExpress.Web.GridViewCommandColumn("Select")
            gvcol.Name = "grdCommands"
            gvcol.ShowSelectCheckbox = True
            gvcol.VisibleIndex = 0
            gvcol.Index = 0
            gvcol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			gvcol.Width = 60
			gvcol.ShowClearFilterButton = True
            g.Columns.Insert(0, gvcol)
        End If
        If Not g.Columns("AddressID") Is Nothing Then g.Columns("AddressID").Visible = False
        If Not g.Columns("StreetAddress") Is Nothing Then
            g.Columns("StreetAddress").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("StreetName") Is Nothing Then
            g.Columns("StreetName").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("City") Is Nothing Then
            g.Columns("City").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("Zip") Is Nothing Then
            g.Columns("Zip").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("Status") Is Nothing Then
            g.Columns("Status").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
		If Not g.Columns("AccuZipStatus") Is Nothing Then
			g.Columns("AccuZipStatus").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("AccuZipStatus").HeaderStyle.Wrap = True
		End If
		If Not g.Columns("crrt") Is Nothing Then
			g.Columns("crrt").Caption = "CRRT"
			g.Columns("crrt").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("crrt").HeaderStyle.Wrap = True
		End If
		If Not g.Columns("Mail_OK") Is Nothing Then
			g.Columns("Mail_OK").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("Mail_OK").Caption = "OK to Mail"
		End If
		If Not g.Columns("Email_OK") Is Nothing Then
			g.Columns("Email_OK").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("Email_OK").Caption = "OK to Email"
		End If
		If Not g.Columns("Bag_OK") Is Nothing Then
			g.Columns("Bag_OK").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("Bag_OK").Caption = "OK to Bag"
		End If
	End Sub

    Sub OnAddressesToAddDataBound(g As ASPxGridView, e As EventArgs)
        If g.Columns.Count > 0 And (g.Columns("grdCommands") Is Nothing) Then
            Dim gvcol As New DevExpress.Web.GridViewCommandColumn("Select")
            gvcol.Name = "grdCommands"
            gvcol.ShowSelectCheckbox = True
            gvcol.VisibleIndex = 0
            gvcol.Index = 0
            gvcol.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            gvcol.Width = 60
            g.Columns.Insert(0, gvcol)
        End If
        If Not g.Columns("AddressID") Is Nothing Then g.Columns("AddressID").Visible = False
        If Not g.Columns("StreetAddress") Is Nothing Then
            g.Columns("StreetAddress").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("StreetName") Is Nothing Then
            g.Columns("StreetName").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("City") Is Nothing Then
            g.Columns("City").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("Zip") Is Nothing Then
            g.Columns("Zip").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("Status") Is Nothing Then
            g.Columns("Status").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        End If
        If Not g.Columns("AccuZipStatus") Is Nothing Then
            g.Columns("AccuZipStatus").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            g.Columns("AccuZipStatus").HeaderStyle.Wrap = True
        End If
		If Not g.Columns("crrt") Is Nothing Then
			g.Columns("crrt").Caption = "CRRT"
			g.Columns("crrt").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("crrt").HeaderStyle.Wrap = True
		End If
		If Not g.Columns("Mail_OK") Is Nothing Then
			g.Columns("Mail_OK").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("Mail_OK").Caption = "OK to Mail"
		End If
		If Not g.Columns("Email_OK") Is Nothing Then
			g.Columns("Email_OK").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("Email_OK").Caption = "OK to Email"
		End If
		If Not g.Columns("Bag_OK") Is Nothing Then
			g.Columns("Bag_OK").HeaderStyle.HorizontalAlign = HorizontalAlign.Center
			g.Columns("Bag_OK").Caption = "OK to Bag"
		End If
	End Sub

    Protected Sub btnRemoveSelectedAddresses_Click(sender As Object, e As EventArgs) Handles btnRemoveSelectedAddresses.Click
        Dim addressIDs As String = ""
        Dim removeList As List(Of Object) = grdSectionAddresses.GetSelectedFieldValues("AddressID")

        If removeList.Count = 0 Then
            ja("Please select at least one Address to Remove.")
            Return
        End If

        For Each item As Object In removeList
            If Len(addressIDs) > 0 Then
                addressIDs += ", "
            End If
            addressIDs += item.ToString()
        Next

		UpdateSections(0, addressIDs, False)

		DeselectSectionAddresses()
		LoadSectionAddresses()
	End Sub

    Protected Sub btnMoveSelectedAddresses_Click(sender As Object, e As EventArgs) Handles btnMoveSelectedAddresses.Click
        If IsNothing(ddlSectionsMove.SelectedValue) OrElse ddlSectionsMove.SelectedValue = 0 Then
            ja("Please select a Template, Route, and Section to which to move the selected Addresses.")
            Return
        End If

		Dim addressIDs As String = ""
        Dim moveList As List(Of Object) = grdSectionAddresses.GetSelectedFieldValues("AddressID")
        If moveList.Count = 0 Then
            ja("Please select at least one Address to Move.")
            Return
        End If

		Dim cntInMoveToSection As Integer = 0
		If hfUncommittedChanges.Value = "1" Then
			SQLExecuteScalar("SELECT COUNT(*) FROM tblAddresses WHERE UncommittedSectionID = " & ddlSectionsMove.SelectedValue, "Notify User")
		Else
			SQLExecuteScalar("SELECT COUNT(*) FROM tblAddresses WHERE SectionID = " & ddlSectionsMove.SelectedValue, "Notify User")
		End If

        If moveList.Count + cntInMoveToSection > vMaxAddressesPerSection Then
            ja("A Section cannot have more than " & vMaxAddressesPerSection & " addresses.")
            Return
		End If

		Dim sql As String = "SELECT COUNT(*) " & _
			"FROM tblRoutes AS R " & _
			"INNER JOIN tblSections AS S ON S.RouteID = R.RouteID " & _
			"WHERE S.SectionID = " & ddlSectionsMove.SelectedValue & " " & _
				"AND UncommittedChanges = 1"
		Dim cnt As Integer = SQLExecuteScalar(sql, "Notify User")

		If hfUncommittedChanges.Value = 1 And cnt = 0 Then
			Dim s As String = "You are processing Uncommitted Changes for " & ddlRoutes.SelectedItem.Text & ". " & _
					"You cannot move those addresses to a Route that is NOT processing Uncommitted Changes."
			ja(s)
			Return
		End If
		If hfUncommittedChanges.Value = 0 And cnt = 1 Then
			Dim s As String = "You are NOT processing Uncommitted Changes for " & ddlRoutes.SelectedItem.Text & ". " & _
					"You cannot move those addresses to a Route that IS processing Uncommitted Changes."
			ja(s)
			Return
		End If

		If moveList.Count + cntInMoveToSection > vWarnAddressesPerSection Then
			ja("This Section will now have more than " & vWarnAddressesPerSection & " addresses. Please proceed with caution.")
		End If

		For Each item As Object In moveList
			If Len(addressIDs) > 0 Then
				addressIDs += ", "
			End If
			addressIDs += item.ToString()
		Next

		UpdateSections(ddlSectionsMove.SelectedValue, addressIDs, False)

		For i As Integer = 0 To grdSectionAddresses.VisibleRowCount - 1
			grdSectionAddresses.Selection.UnselectRow(i)
		Next
		DeselectSectionAddresses()
		LoadSectionAddresses()
    End Sub

    Protected Sub btnMoveSelectedSection_Click(sender As Object, e As EventArgs) Handles btnMoveSelectedSection.Click
        If IsNothing(ddlRoutesMoveSection.SelectedValue) OrElse ddlRoutesMoveSection.SelectedValue = 0 Then
            ja("Please select a Template and Route to which to move the selected Section.")
            Return
        End If

        If IsNothing(ddlSections.SelectedValue) OrElse ddlSections.SelectedValue = 0 Then
            ja("Please select a Template, Route, and Section above from which to move the selected Section.")
            Return
        End If

		Dim sql As String = "SELECT COUNT(*) " & _
			"FROM tblRoutes " & _
			"WHERE RouteID = " & ddlRoutesMoveSection.SelectedValue & " " & _
				"AND UncommittedChanges = 1"
		Dim cnt As Integer = SQLExecuteScalar(sql, "Notify User")

		If hfUncommittedChanges.Value = 1 And cnt = 0 Then
			Dim s As String = "You are processing Uncommitted Changes for " & ddlRoutes.SelectedItem.Text & ". " & _
					"You cannot move a Section to a Route that is NOT processing Uncommitted Changes."
			ja(s)
			Return
		End If
		If hfUncommittedChanges.Value = 0 And cnt = 1 Then
			Dim s As String = "You are NOT processing Uncommitted Changes for " & ddlRoutes.SelectedItem.Text & ". " & _
					"You cannot move a Section to a Route that IS processing Uncommitted Changes."
			ja(s)
			Return
		End If

		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spSectionRoute_Update"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@sectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, ddlSections.SelectedValue))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@routeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, ddlRoutesMoveSection.SelectedValue))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.CommandTimeout = 150
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "SectionAddressMaint, btnMoveSelectedSection_Click")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "SectionAddressMaint, btnMoveSelectedSection_Click")
		Finally
			conn.Close()
		End Try

		ddlSections.SelectedValue = "0"
		hfSelectedSectionID.Value = "0"
		ddlRoutes.SelectedValue = "0"
		hfSelectedRouteID.Value = "0"
		LoadSectionAddresses()
	End Sub

    Protected Sub btnChangeStatusSelectedAddress_Click(sender As Object, e As EventArgs) Handles btnChangeStatusSelectedAddress.Click
        If IsNothing(ddlStatus.SelectedValue) OrElse ddlStatus.SelectedValue = 0 Then
            ja("Please select a Status to which to change the selected Addresses.")
            Return
        End If

        Dim addressIDs As String = ""
        Dim changeList As List(Of Object) = grdSectionAddresses.GetSelectedFieldValues("AddressID")
        If changeList.Count = 0 Then
            ja("Please select at least one Address to Change.")
            Return
        End If

        For Each item As Object In changeList
            If Len(addressIDs) > 0 Then
                addressIDs += ", "
            End If
            addressIDs += item.ToString()
        Next

        UpdateStatuses(ddlStatus.SelectedValue, addressIDs)
   
        For i As Integer = 0 To grdSectionAddresses.VisibleRowCount - 1
            grdSectionAddresses.Selection.UnselectRow(i)
        Next
        DeselectSectionAddresses()
        LoadSectionAddresses()
    End Sub

    Protected Sub btnSelectedAddressesAdd_Click(sender As Object, e As EventArgs) Handles btnSelectedAddressesAdd.Click
        Dim addressIDs As String = ""
        Dim addList As List(Of Object) = grdAddressesToAdd.GetSelectedFieldValues("AddressID")

        If addList.Count = 0 Then
            ja("Please select at least one Address to Add.")
            Return
        End If

        If grdSectionAddresses.VisibleRowCount + addList.Count > vMaxAddressesPerSection Then
            ja("A Section cannot have more than " & vMaxAddressesPerSection & " addresses.")
            Return
        End If

        If grdSectionAddresses.VisibleRowCount + addList.Count > vWarnAddressesPerSection Then
            ja("This Section will now have more than " & vWarnAddressesPerSection & " addresses. Please proceed with caution.")
        End If


        For Each item As Object In addList
            If Len(addressIDs) > 0 Then
                addressIDs += ", "
            End If
            addressIDs += item.ToString()
        Next

        UpdateSections(ddlSections.SelectedValue, addressIDs, True)

        For i As Integer = 0 To grdAddressesToAdd.VisibleRowCount - 1
            grdAddressesToAdd.Selection.UnselectRow(i)
        Next
        txtAddressCity.Text = ""
        txtAddressStreet.Text = ""
        tblAddressesToAdd.Attributes.Add("style", "visibility:hidden")
        btnSelectedAddressesAdd.Visible = False

        LoadSectionAddresses()
    End Sub

    Protected Sub btnAddAddressesDeselect_Click(sender As Object, e As EventArgs) Handles btnAddressesToAddDeselect.Click
        DeselectAddressesToAdd()
    End Sub

    Protected Sub btnAddAddressesSelect_Click(sender As Object, e As EventArgs) Handles btnAddressesToAddSelect.Click
        PopulateAddressesToAddCheckboxesToAdd()
    End Sub

    Protected Sub btnChangeStatusAddAddress_Click(sender As Object, e As EventArgs) Handles btnChangeStatusSelectedAddressesToAdd.Click
        If IsNothing(ddlStatusAdd.SelectedValue) OrElse ddlStatusAdd.SelectedValue = 0 Then
            ja("Please select a Status to which to change the selected Addresses.")
            Return
        End If

        Dim addressIDs As String = ""
        Dim changeList As List(Of Object) = grdAddressesToAdd.GetSelectedFieldValues("AddressID")
        If changeList.Count = 0 Then
            ja("Please select at least one Address to Change.")
            Return
        End If

        For Each item As Object In changeList
            If Len(addressIDs) > 0 Then
                addressIDs += ", "
            End If
            addressIDs += item.ToString()
        Next

        UpdateStatuses(ddlStatusAdd.SelectedValue, addressIDs)

        For i As Integer = 0 To grdAddressesToAdd.VisibleRowCount - 1
            grdAddressesToAdd.Selection.UnselectRow(i)
        Next
        DeselectAddressesToAdd()
        LoadAddressesToAdd()
    End Sub

    Private Sub PopulateAddressesToAddCheckboxesToAdd()
        If txtAddressCity.Text = "" AndAlso txtAddressStreet.Text = "" Then
            Return
        End If

        For i As Integer = 0 To grdAddressesToAdd.VisibleRowCount - 1
            Dim city As String = grdAddressesToAdd.GetRowValues(i, "City").ToString()
			Dim street As String = grdAddressesToAdd.GetRowValues(i, "StreetName").ToString()
			Dim crrt As String = grdAddressesToAdd.GetRowValues(i, "crrt").ToString()
			Dim cOK As Boolean = False
            Dim sOK As Boolean = False
			Dim rOK As Boolean = False
			If txtAddressesToAddCity.Text.Trim() = "" Then
				cOK = True
			Else
				If InStr(city.Trim().ToUpper(), txtAddressesToAddCity.Text.Trim().ToUpper()) > 0 Then
					cOK = True
				End If
			End If
            If txtAddressesToAddStreet.Text.Trim() = "" Then
                sOK = True
            Else
                If InStr(street.Trim().ToUpper(), txtAddressesToAddStreet.Text.Trim().ToUpper()) > 0 Then
                    sOK = True
                End If
            End If
			If txtAddressesToAddCRRT.Text.Trim() = "" Then
				rOK = True
			Else
				If InStr(crrt.Trim().ToUpper(), txtAddressesToAddCRRT.Text.Trim().ToUpper()) > 0 Then
					rOK = True
				End If
			End If
			If cOK And sOK And rOK Then
				grdAddressesToAdd.Selection.SelectRow(i)
			End If
        Next
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim options As DevExpress.XtraPrinting.XlsExportOptions = New XlsExportOptions()
        options.SheetName = "Addresses To Add Info"
        addressesToAddInfoExporter.WriteXlsToResponse(options)
    End Sub

    Private Sub UpdateStatuses(ByVal statusID As Integer, ByVal addressIDs As String)
        Dim conn As SqlConnection = New SqlConnection(vConnStr)

        Dim myCmd As SqlCommand = New SqlCommand()
        myCmd.Connection = conn
        myCmd.CommandText = "spAddressStatus_Update"
        myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@statusID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, statusID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@addressIDs", System.Data.ParameterDirection.Input, System.Data.DbType.String, addressIDs))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, Session("vUserID")))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.CommandTimeout = 150
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "SectionAddressMaint, UpdateStatuses")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "SectionAddressMaint, UpdateStatuses")
		Finally
			conn.Close()
		End Try

	End Sub

	Private Sub UpdateSections(ByVal sectionID As Integer, ByVal addressIDs As String, ByVal updateStatusIDs As Boolean)
		Dim uc As Boolean = False
		If hfUncommittedChanges.Value = 1 Then
			uc = True
		End If

		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spAddressSection_Update"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@sectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, sectionID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@uncommittedChanges", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, uc))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@addressIDs", System.Data.ParameterDirection.Input, System.Data.DbType.String, addressIDs))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@updateStatusIDs", System.Data.ParameterDirection.Input, System.Data.DbType.String, updateStatusIDs))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, Session("vUserID")))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.CommandTimeout = 150
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "SectionAddressMaint, UpdateSections")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "SectionAddressMaint, UpdateSections")
		Finally
			conn.Close()
		End Try

	End Sub

End Class
