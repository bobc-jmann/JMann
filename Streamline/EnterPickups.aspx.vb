Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services

Partial Class EnterPickups
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

        'Dim sql As String = "SELECT PS.PickupScheduleID, 0 AS History, PC.PickupCycleAbbr, PT.ScheduleType, PS.MailingDate, PS.PickupDate, PS.RouteCode, " & _
        '"(SELECT  FROM tblPickupScheduleDetail PSD " & _
        '    "WHERE PSD.PickupScheduleID = PS.PickupScheduleID AND PSD.Mail = 1) AS MailCount, " & _
        '"(SELECT COUNT(*) FROM tblPickupScheduleDetail PSD " & _
        '    "WHERE PSD.PickupScheduleID = PS.PickupScheduleID AND PSD.Email = 1) AS EmailCount, " & _
        '"(SELECT COUNT(*) FROM tblPickupScheduleDetail PSD " & _
        '    "WHERE PSD.PickupScheduleID = PS.PickupScheduleID AND PSD.Bag = 1) AS BagCount, " & _
        '"(SELECT COUNT(*) FROM tblPickupsAddresses PA " & _
        '    "INNER JOIN tblPickupsSections PSEC ON PSEC.PickupsSectionID = PA.PickupsSectionID " & _
        '    "WHERE PSEC.PickupScheduleID = PS.PickupScheduleID) AS PickupCount " & _
        '"FROM tblPickupSchedule PS " & _
        '"INNER JOIN tblPickupCycles PC ON PC.PickupCycleID = PS.PickupCycleID " & _
        '"INNER JOIN tblPickupCycleDriverLocations PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
        '"INNER JOIN tlkScheduleTypes PT ON PT.ScheduleTypeID = PS.ScheduleTypeID " & _
        'whereClause & _
        '"ORDER BY PS.PickupDate DESC, PC.PickupCycleAbbr"

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

    Protected Sub btnAddressesDeselectAll(ByVal sender As Object, ByVal e As EventArgs)
        grdMailAddresses.Selection.UnselectAll()
    End Sub

	Protected Sub btnScheduledPopup(ByVal sender As Object, ByVal e As EventArgs)
		Dim popup As ASPxPopupControl = DirectCast(Page.FindControl("popScheduled"), ASPxPopupControl)
		cmbDriverScheduled.Value = ""
		popup.ShowOnPageLoad = True
	End Sub

	Protected Sub btnCreatePickupsForScheduledAddressesOK(ByVal sender As Object, ByVal e As EventArgs)
		Dim addPickupList As List(Of Object) = grdMailAddresses.GetSelectedFieldValues("AddressID")
		Dim addPickupDetailIDList As List(Of Object) = grdMailAddresses.GetSelectedFieldValues("PickupScheduleDetailID")
		Dim driverID As Integer = CInt(cmbDriverScheduled.Value)

		If addPickupList.Count = 0 Then
			ja("Please select at least one Address to Add as a Pickup.")
			Return
		End If

		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Try
			conn.Open()
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

		Dim error_id As Integer = 0

		For i As Integer = 0 To addPickupList.Count - 1

			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spPickupsManualCreate"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleSectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("PickupScheduleSectionID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("PickupScheduleID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@sectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("SectionID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleDetailID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, addPickupDetailIDList.Item(i)))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@driverID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, driverID))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@userName", System.Data.ParameterDirection.Input, System.Data.DbType.String, Session("vUserName")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@addressID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, addPickupList.Item(i)))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@return_value", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@error_id", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))

			Try
				myCmd.ExecuteNonQuery()
				error_id = myCmd.Parameters.Item("@error_id").Value
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			End Try

			vbHandleProgramError(error_id, "EnterPickups, btnCreatePickupsForScheduledAddressesOK")
		Next

		conn.Close()
		grdMailAddresses.Selection.UnselectAll()
		' 12/14/15 - RCC - I have no idea why the addresses won't unselect here and yet it works with the button.
		' No big deal for now. I've spent enough time on it.
		'grdMailAddresses.DataBind()
		grdPickupScheduleSections.DataBind()
		grdPickupSchedules.DataBind()
		grdPickupsSections.DataBind()

	End Sub

	Protected Sub btnUnknownPopup(ByVal sender As Object, ByVal e As EventArgs)
		Dim popup As ASPxPopupControl = DirectCast(Page.FindControl("popUnknown"), ASPxPopupControl)
		cmbDriverUnknown.Value = ""
		popup.ShowOnPageLoad = True
	End Sub

	Protected Sub btnCreatePickupsForUnknownAddressesOK(ByVal sender As Object, ByVal e As EventArgs)
		Dim qty As Integer = CInt(txtUnknown.Text)
		Dim driverID As Integer = CInt(cmbDriverUnknown.Value)


		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Try
			conn.Open()
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

		Dim error_id As Integer = 0

		For i As Integer = 1 To qty
			Dim addressid As Integer = 1
			If qty = 0 Then
				addressid = 0	' Just create the section record the first time through if necessary
			End If

			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spPickupsManualCreate"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleSectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("PickupScheduleSectionID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("PickupScheduleID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@sectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("SectionID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleDetailID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 0))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@driverID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, driverID))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@userName", System.Data.ParameterDirection.Input, System.Data.DbType.String, Session("vUserName")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@addressID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, 1))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@return_value", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@error_id", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))

			Try
				myCmd.ExecuteNonQuery()
				error_id = myCmd.Parameters.Item("@error_id").Value
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			End Try

			vbHandleProgramError(error_id, "EnterPickups, btnCreatePickupsForUnknownAddressesOK")
		Next

		conn.Close()
		grdMailAddresses.Selection.UnselectAll()

		grdPickupsSections.DataBind()
		grdPickupScheduleSections.DataBind()
		grdPickupSchedules.DataBind()

		popUnknown.ShowOnPageLoad = False
	End Sub

	Protected Sub btnUnscheduledPopup(ByVal sender As Object, ByVal e As EventArgs)
		Dim popup As ASPxPopupControl = DirectCast(Page.FindControl("popUnscheduled"), ASPxPopupControl)
		cmbDriverUnscheduled.Value = ""
		popup.ShowOnPageLoad = True
	End Sub

	Protected Sub btnUnscheduledAddressesDeselectAll(ByVal sender As Object, ByVal e As EventArgs)
		grdUnscheduledAddresses.Selection.UnselectAll()
	End Sub

	Protected Sub btnCreateUnscheduledSelectedAsPickupsOK(ByVal sender As Object, ByVal e As EventArgs)
		Dim addPickupList As List(Of Object) = grdUnscheduledAddresses.GetSelectedFieldValues("AddressID")
		Dim driverID As Integer = CInt(cmbDriverUnscheduled.Value)

		If addPickupList.Count = 0 Then
			ja("Please select at least one Address to Add as a Pickup.")
			Return
		End If

		Dim conn As SqlConnection = New SqlConnection(vConnStr)
		Try
			conn.Open()
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

		Dim error_id As Integer = 0

		For i As Integer = 0 To addPickupList.Count - 1

			Dim myCmd As SqlCommand = New SqlCommand()
			myCmd.Connection = conn
			myCmd.CommandText = "spPickupsManualCreate"
			myCmd.CommandType = System.Data.CommandType.StoredProcedure

			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleSectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("PickupScheduleSectionID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("PickupScheduleID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@sectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, grdPickupsSections.GetMasterRowFieldValues("SectionID")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@pickupScheduleDetailID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, -1))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@driverID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, driverID))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@userName", System.Data.ParameterDirection.Input, System.Data.DbType.String, Session("vUserName")))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@addressID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, addPickupList.Item(i)))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@return_value", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))
			myCmd.Parameters.Add(DataUtil.CreateParameter("@error_id", System.Data.ParameterDirection.InputOutput, System.Data.DbType.Int32, 0))

			Try
				myCmd.ExecuteNonQuery()
				error_id = myCmd.Parameters.Item("@error_id").Value
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			End Try

			vbHandleProgramError(error_id, "EnterPickups, btnCreateUnscheduledSelectedAsPickupsOK")
		Next

		conn.Close()
		grdUnscheduledAddresses.Selection.UnselectAll()

		grdMailAddresses.DataBind()
		grdPickupScheduleSections.DataBind()
		grdPickupSchedules.DataBind()
		grdPickupsSections.DataBind()
		grdUnscheduledAddresses.DataBind()

		popUnknown.ShowOnPageLoad = False
	End Sub

    Protected Sub grdPickupsAddresses_RowDeleted(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletedEventArgs)
        grdMailAddresses.DataBind()
        grdPickupScheduleSections.DataBind()
        grdPickupSchedules.DataBind()
        grdPickupsSections.DataBind()
        grdUnscheduledAddresses.DataBind()
    End Sub
End Class
