Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services

Partial Class SectionAddressLookup
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
                        Case "ckAddressesNotOnRoutes"
                            ShowSectionAddressParametersTable()
                        Case Else
                            LoadComboBoxes()
                            LoadSectionAddresses()
                    End Select
                End If

            Case "GET"
                ckActiveTemplates.Checked = True
                ckInactiveTemplates.Checked = False

                LoadTemplates()

            Case "HEAD"
                'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
        End Select
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

    Private Shared Function SelectSections(ByVal routeID As Integer) As String
		Dim sql As String = "SELECT S.SectionID, S.SectionCode " & _
			"FROM tblSections S " &
			"WHERE S.RouteID = " & routeID & " " & _
				"AND S.Active = 1 " & _
			"ORDER BY S.SectionCode"
        Return (sql)
    End Function

    Public Class Section
        Public SectionId As Integer
        Public SectionCode As String
    End Class

    <WebMethod()> _
    Public Shared Function GetSections(ByVal routeID As Integer) As Section()
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim result As Boolean = False
        Dim r As System.Data.SqlClient.SqlDataReader = Nothing
        Dim sections As List(Of Section) = New List(Of Section)
        Dim sql As String = SelectSections(routeID)

        Try
            r = DataUtil.GetReader(conn, vConnStr, System.Data.CommandType.Text, sql, 90)
            While r.Read()
                Dim sec As Section = New Section
                sec.SectionId = CInt(r.Item("SectionID").ToString)
                sec.SectionCode = r.Item("SectionCode").ToString
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

    Private Shared Function SelectSectionAddresses(ByVal sectionID As Integer) As String
		Dim sql As String = "SELECT AddressID, StreetAddress, StreetName, City, Zip, ST.Status, status_ AS AccuZipStatus, crrt, Mail_OK, Email_OK, Bag_OK " & _
			"FROM tblAddresses A " & _
			"LEFT OUTER JOIN tlkStatuses ST ON ST.StatusID = A.StatusID " & _
			"WHERE A.SectionID = " & sectionID & " " & _
			"ORDER BY City, StreetName, StreetAddress"
        Return (sql)
    End Function

    Protected Sub ddlSections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSections.SelectedIndexChanged
        grdSectionAddresses.PageIndex = 0
        LoadSectionAddresses()
    End Sub

    Private Function SelectAddressesToAdd(ByVal city As String, ByVal street As String, ByVal zip As String) As String
		Dim sql As String = "SELECT AddressID, StreetAddress, StreetName, City, Zip, ST.Status, status_ AS AccuZipStatus, crrt, Mail_OK, Email_OK, Bag_OK " & _
			"FROM tblAddresses A " & _
			"LEFT OUTER JOIN tblSections S ON S.SectionID = A.SectionID " & _
			"LEFT OUTER JOIN tblRoutes R ON R.RouteID = S.RouteID " & _
			"LEFT OUTER JOIN tlkStatuses ST ON ST.StatusID = A.StatusID " & _
			"WHERE (A.SectionID IS NULL') "
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
        If Trim(zip) <> "" Then
            sql += "AND SUBSTRING(A.Zip, 1, 5) LIKE '" & zip & "%' "
        End If
        sql += "ORDER BY City, StreetName, StreetAddress"
        Return (sql)
    End Function

    Private Function SelectAddressesToAddInfo(ByVal city As String, ByVal street As String, ByVal zip As String) As String
		Dim sql As String = "SELECT DISTINCT StreetName, " & _
				"MIN(StreetNumber) OVER(PARTITION BY StreetName) AS MinStreetNumber, " & _
				"MAX(StreetNumber) OVER(PARTITION BY StreetName) AS MaxStreetNumber " & _
			"FROM tblAddresses AS A " & _
			"LEFT OUTER JOIN tblSections S ON S.SectionID = A.SectionID " & _
			"LEFT OUTER JOIN tblRoutes R ON R.RouteID = S.RouteID " & _
			"LEFT OUTER JOIN tlkStatuses ST ON ST.StatusID = A.StatusID " & _
			"WHERE A.SectionID IS NULL "
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
        If Trim(zip) <> "" Then
            sql += "AND SUBSTRING(A.Zip, 1, 5) LIKE '" & zip & "%' "
        End If
        sql += "ORDER BY StreetName"
        Return (sql)
    End Function

    Private Sub LoadTemplates()
        txtSQL_ddlTemplates.Text = "SELECT PickupCycleTemplateID, PickupCycleTemplateCode " & _
            "FROM tblPickupcycleTemplates "

        ckAddressesNotOnRoutes.Checked = False
        tblSectionAddresses.Attributes.Add("style", "display:inline")
        tblTemplates.Attributes.Add("style", "display:inline")
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

        If ckInactiveTemplates.Checked Then
            ddlTemplates.Items.Insert(1, New ListItem("<Routes w/o Template>", "99"))
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
        da = New SqlDataAdapter(SelectSections(ddlRoutes.SelectedValue), vConnStr)
        dt = New DataTable()
        da.Fill(dt)
        ddlSections.DataSource = dt
        ddlSections.DataTextField = "SectionCode"
        ddlSections.DataValueField = "SectionID"
        ddlSections.DataBind()
        ddlSections.Items.Insert(0, New ListItem("Select Section", "0"))
        ddlSections.SelectedValue = hfSelectedSectionID.Value
    End Sub

    Private Sub LoadSectionAddresses()
        If ckAddressesNotOnRoutes.Checked Then
            ckActiveTemplates.Checked = False
            ckInactiveTemplates.Checked = False
            Return
        End If

        With dsSectionAddresses
            .ConnectionString = vConnStr
            .SelectCommand = SelectSectionAddresses(ddlSections.SelectedValue)
        End With
        grdSectionAddresses.DataBind()
    End Sub

    Private Sub ShowSectionAddressParametersTable()
        If ckAddressesNotOnRoutes.Checked = False Then
            If ckInactiveTemplates.Checked = False Then
                ckActiveTemplates.Checked = True
            End If
            tblSectionAddresses.Attributes.Add("style", "display:inline")
            tblTemplates.Attributes.Add("style", "display:inline")
            LoadComboBoxes()
            Return
        End If
        ckActiveTemplates.Checked = False
        ckInactiveTemplates.Checked = False

        tblSectionAddresses.Attributes.Add("style", "display:none")
        tblTemplates.Attributes.Add("style", "display:none")
    End Sub

    Sub OnSectionAddressesDataBound(g As ASPxGridView, e As EventArgs)
        If g.Columns.Count > 0 And (g.Columns("grdCommands") Is Nothing) Then
            Dim gvcol As New DevExpress.Web.GridViewCommandColumn(" ")
            gvcol.Name = "grdCommands"
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

End Class
