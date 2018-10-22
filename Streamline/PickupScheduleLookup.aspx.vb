Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services

Partial Class PickupScheduleLookup
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
                    lblPickupDate.Visible = False
                    dtPickupDate.Visible = False
                    lblPickupCycle.Visible = False
                    ddlPickupCycles.Visible = False
                    lblRoutes.Visible = False
                    ddlRoutes.Visible = False

                    If rbLookupType.Value = "byPickupDate" Then
                        grdPickupSchedules.Settings.ShowFilterRow = False
                        grdPickupSchedules.Settings.ShowFilterRowMenu = False
                    ElseIf rbLookupType.Value = "byPickupCycle" Then
                        grdPickupSchedules.Settings.ShowFilterRow = True
                        grdPickupSchedules.Settings.ShowFilterRowMenu = True
                    Else
                        grdPickupSchedules.Settings.ShowFilterRow = True
                        grdPickupSchedules.Settings.ShowFilterRowMenu = True
                    End If

                    If ddlDriverLocations.SelectedValue <> "ALL" Then
                        If rbLookupType.Value = "byPickupDate" Then
                            lblPickupDate.Visible = True
                            dtPickupDate.Visible = True
                        ElseIf rbLookupType.Value = "byPickupCycle" Then
                            lblPickupCycle.Visible = True
                            ddlPickupCycles.Visible = True
                        Else
                            lblRoutes.Visible = True
                            ddlRoutes.Visible = True
                        End If
                    End If

                    Select Case PostBackControlID
                        Case "ddlPickupCycles"

                        Case "ddlRoutes"

                        Case Else
                            LoadPickupSchedules()
                            LoadPickupCycles()
                            LoadRoutes()
                    End Select
                End If


            Case "GET"
                dtEarliestPickupDate.Value = DateAdd(DateInterval.Month, -2, Now)

                hfSQL_ddlDriverLocations.Value = "SELECT RegionID, RegionDesc FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc"
                Dim da As SqlDataAdapter = New SqlDataAdapter(hfSQL_ddlDriverLocations.Value, vConnStr)
                Dim dt As DataTable = New DataTable()
                da.Fill(dt)
                ddlDriverLocations.DataSource = dt
                ddlDriverLocations.DataTextField = "RegionDesc"
                ddlDriverLocations.DataValueField = "RegionID"
                ddlDriverLocations.DataBind()
                ddlDriverLocations.Items.Insert(0, New ListItem("All Locations", "ALL"))
                ddlDriverLocations.Items.Insert(0, New ListItem("Select Driver Location", "0"))
                If Session("UserRegionDefault") <> 0 Then
                    ddlDriverLocations.SelectedValue = Session("UserRegionDefault")
                End If

                rbLookupType.Value = "byPickupDate"

                lblPickupDate.Visible = False
                dtPickupDate.Visible = False
                lblPickupCycle.Visible = False
                ddlPickupCycles.Visible = False
                lblRoutes.Visible = False
                ddlRoutes.Visible = False

                LoadPickupCycles()
                LoadRoutes()
            Case "HEAD"
        End Select
    End Sub

    Private Shared Function SelectPickupSchedules(ByVal whereClause As String) As String
        Dim sql As String = "SELECT PS.PickupScheduleID, 0 AS History, PC.PickupCycleAbbr, PT.ScheduleType, " & _
                "PS.MailingDate, PS.PickupDate, PS.RouteCode, CntMail, CntEmail, CntBag, CntPostcard, CntEmailNR, CntPutOuts, " & _
                "(SELECT SUM(CntPickupsDrivers) FROM tblPickupScheduleSections AS PSS " & _
                    "WHERE PSS.PickupscheduleID = PS.PickupScheduleID) AS CntPickupsDrivers, " & _
                "(SELECT SUM(CntPickupsAddresses) FROM tblPickupScheduleSections AS PSS " & _
                    "WHERE PSS.PickupscheduleID = PS.PickupScheduleID) AS CntPickupsAddresses, " & _
                "(SELECT SUM(SoftCarts) FROM tblPickupScheduleSections AS PSS " & _
                    "WHERE PSS.PickupscheduleID = PS.PickupScheduleID) AS SoftCarts, " & _
                "(SELECT SUM(HardCarts) FROM tblPickupScheduleSections AS PSS " & _
                    "WHERE PSS.PickupscheduleID = PS.PickupScheduleID) AS HardCarts, " & _
                "(SELECT SUM(TotalCarts) FROM tblPickupScheduleSections AS PSS " & _
                    "WHERE PSS.PickupscheduleID = PS.PickupScheduleID) AS TotalCarts " & _
            "FROM tblPickupSchedule PS " & _
            "INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
            "INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
            "INNER JOIN tlkScheduleTypes PT ON PT.ScheduleTypeID = PS.ScheduleTypeID " & _
            whereClause & _
            "ORDER BY PS.PickupDate DESC, PC.PickupCycleAbbr"

		Return (sql)
    End Function

    Protected Sub grdPickupScheduleSections_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("PickupScheduleID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Session("History") = (TryCast(sender, ASPxGridView)).GetMasterRowFieldValues("History")
    End Sub

    Protected Sub grdSectionID_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("SectionID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Private Sub LoadPickupSchedules()
        Dim whereClause As String = "WHERE "

        If ddlDriverLocations.SelectedValue = "0" Then
            Return
        End If
        If ddlDriverLocations.SelectedValue <> "ALL" Then
            whereClause += "PCDL.RegionID = '" & ddlDriverLocations.SelectedValue & "' AND "
        End If
        If rbLookupType.Value = "byPickupDate" Then
            If dtPickupDate.Value < CDate("1/1/1990") Then
                Return
            Else
                whereClause += "PS.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' "
            End If
        ElseIf rbLookupType.Value = "byPickupCycle" Then
            If ddlPickupCycles.SelectedValue = "0" Then
                Return
            Else
                whereClause += "PS.PickupCycleID = " & ddlPickupCycles.SelectedValue & " "
            End If
        ElseIf rbLookupType.Value = "byRoute" Then
            If ddlRoutes.SelectedValue = "0" Then
                Return
            Else
                whereClause += "PS.RouteCode = '" & ddlRoutes.SelectedItem.Text & "' "
            End If
        End If

        whereClause += "AND PS.PickupDate >= '" & Format(dtEarliestPickupDate.Value, "MM/dd/yyyy") & "' "

        hfPickupSchedulesSelectCommand.Value = SelectPickupSchedules(whereClause)
        dsPickupSchedules.SelectCommand = hfPickupSchedulesSelectCommand.Value
        grdPickupSchedules.DataBind()

    End Sub


    Protected Sub ddlDriverLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriverLocations.SelectedIndexChanged
        If ddlDriverLocations.SelectedValue = "0" Then
            ddlPickupCycles.Items.Clear()
            ddlRoutes.Items.Clear()
            Return
        End If

        txtSQL_ddlPickupCycles.Text = "SELECT DISTINCT PC.[PickupCycleID], PC.[PickupCycleAbbr] FROM tblPickupCycles PC "
        If ddlDriverLocations.SelectedValue <> "ALL" Then
            txtSQL_ddlPickupCycles.Text += "INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
                "WHERE PCDL.RegionID = '" & ddlDriverLocations.SelectedValue & "' "
        End If
        txtSQL_ddlPickupCycles.Text += "ORDER BY PC.PickupCycleAbbr"
        LoadPickupCycles()

        txtSQL_ddlRoutes.Text = "SELECT DISTINCT R.[RouteID], R.[RouteCode] FROM tblRoutes R "
        If ddlDriverLocations.SelectedValue <> "ALL" Then
            txtSQL_ddlRoutes.Text += "INNER JOIN tblPickupCycleTemplatesDetail PCTD ON PCTD.RouteID = R.RouteID " & _
                "INNER JOIN tblPickupCycles PC ON PC.PickupCycleTemplateID = PCTD.PickupCycleTemplateID " & _
                "INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
                "WHERE PCDL.RegionID = '" & ddlDriverLocations.SelectedValue & "' "
        End If
        txtSQL_ddlRoutes.Text += "ORDER BY R.RouteCode"
        LoadRoutes()

        LoadPickupSchedules()
    End Sub

    Protected Sub dtPickupDate_DateChanged(sender As Object, e As EventArgs) Handles dtPickupDate.DateChanged
        LoadPickupSchedules()
    End Sub

    Protected Sub ddlPickupCycles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPickupCycles.SelectedIndexChanged
        LoadPickupSchedules()
    End Sub

    Protected Sub ddlRoutes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoutes.SelectedIndexChanged
        LoadPickupSchedules()
    End Sub

    Private Sub LoadPickupCycles()
        If txtSQL_ddlPickupCycles.Text = "" Then
            txtSQL_ddlPickupCycles.Text = "SELECT DISTINCT PC.[PickupCycleID], PC.[PickupCycleAbbr] FROM tblPickupCycles PC WHERE PickupCycleID = 0"
        End If
        Dim da As SqlDataAdapter = New SqlDataAdapter(txtSQL_ddlPickupCycles.Text, vConnStr)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        ddlPickupCycles.DataSource = dt
        ddlPickupCycles.DataTextField = "PickupCycleAbbr"
        ddlPickupCycles.DataValueField = "PickupCycleID"
        ddlPickupCycles.DataBind()
        ddlPickupCycles.Items.Insert(0, New ListItem("Select Pickup Cycle", "0"))
    End Sub

    Private Sub LoadRoutes()
        If txtSQL_ddlRoutes.Text = "" Then
            txtSQL_ddlRoutes.Text = "SELECT DISTINCT R.[RouteID], R.[RouteCode] FROM tblRoutes R WHERE RouteID = 0"
        End If
        Dim da As SqlDataAdapter = New SqlDataAdapter(txtSQL_ddlRoutes.Text, vConnStr)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        ddlRoutes.DataSource = dt
        ddlRoutes.DataTextField = "RouteCode"
        ddlRoutes.DataValueField = "RouteID"
        ddlRoutes.DataBind()
        ddlRoutes.Items.Insert(0, New ListItem("Select Route", "0"))
    End Sub

    Private grdMailAddresses As ASPxGridView
    Private grdPickupsAddresses As ASPxGridView
    Private grdPickupScheduleSections As ASPxGridView
    Private grdPickupsSections As ASPxGridView

    Protected Sub grdPickupsAddresses_Init(ByVal sender As Object, ByVal e As EventArgs)
        grdPickupsAddresses = CType(sender, ASPxGridView)
    End Sub

    Protected Sub grdMailAddresses_Init(ByVal sender As Object, ByVal e As EventArgs)
        grdMailAddresses = CType(sender, ASPxGridView)
    End Sub

    Protected Sub grdPickupScheduleSections_Init(ByVal sender As Object, ByVal e As EventArgs)
        grdPickupScheduleSections = CType(sender, ASPxGridView)
    End Sub

    Protected Sub grdPickupsSections_Init(ByVal sender As Object, ByVal e As EventArgs)
        grdPickupsSections = CType(sender, ASPxGridView)
    End Sub
End Class
