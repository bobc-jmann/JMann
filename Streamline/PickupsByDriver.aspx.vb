Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services

Partial Class PickupsByDriver
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sql As String = ""

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
                        Case Else

                    End Select
                End If
            Case "GET"
                dtPickupDate.Value = DateValue(Now)

                sql = "SELECT RegionID, RegionDesc FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc"
                Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
                Dim dt As DataTable = New DataTable()
                da.Fill(dt)
                ddlDriverLocations.DataSource = dt
                ddlDriverLocations.DataTextField = "RegionDesc"
                ddlDriverLocations.DataValueField = "RegionID"
                ddlDriverLocations.DataBind()
                ddlDriverLocations.Items.Insert(0, New ListItem("Select Driver Location", "0"))
                If Session("UserRegionDefault") <> 0 Then
                    ddlDriverLocations.SelectedValue = Session("UserRegionDefault")
                End If

            Case "HEAD"
                'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
        End Select

        dsDriver.SelectCommand = "SELECT DriverID, DriverName FROM [tblDrivers] " & _
            "WHERE (DriverLocationID = '" & ddlDriverLocations.SelectedValue & "' OR DriverLocationID IS NULL) " & _
                "AND Active = 1 " & _
            "UNION " & _
            "SELECT 0 AS DriverID, '<Missing>' AS DriverName " & _
            "ORDER BY DriverName"
        dsDriver.DataBind()
        cmbDrivers.DataBind()

        LoadData()
    End Sub

    Private Sub LoadData()
        If ddlDriverLocations.SelectedValue = "0" Then
            Return
        End If
        If IsNothing(cmbDrivers.SelectedItem) Then
            Return
        End If

        Dim sql As String = ""
        If cmbDrivers.SelectedItem.Value > 0 Then
			sql = _
				"SELECT " & _
				"PA.DeviceName AS TabletName, D.DriverName, " & _
				"PA.PickupsAddressID, A.StreetNumber, A.StreetName, A.City, " & _
				"CONVERT(VARCHAR, PA.StartTime, 8) AS [Time], " & _
				"DA.RouteCode + '-' + DA.SectionCode AS [Route-Section], Bags, Boxes, Items " & _
				"FROM tblDrivers AS DR " & _
				"INNER JOIN tblDriverAssignments AS DA ON DA.DriverID = DR.DriverID " & _
				"LEFT OUTER JOIN tblTablets AS TT ON TT.TabletID = DA.TabletID " & _
				"INNER JOIN tblPickupsAddresses AS PA ON PA.PickupScheduleID = DA.PickupScheduleID " & _
					"AND PA.DeviceName = TT.TabletName " & _
				"INNER JOIN tblPickupsSections AS PSEC ON PSEC.PickupsSectionID = PA.PickupsSectionID " & _
					"AND PSEC.SectionID = DA.SectionID " & _
				"INNER JOIN tblAddresses AS A ON A.AddressID = PA.AddressID " & _
				"INNER JOIN tblDrivers AS D ON D.DriverID = DA.DriverID " & _
				"WHERE DA.DriverID = " & cmbDrivers.SelectedItem.Value & " " & _
					"AND DA.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
					"AND DA.SectionID > 0 " & _
				 "ORDER BY [Time]"
        Else
			sql = _
				"SELECT " & _
				"PA.DeviceName AS TabletName, '' AS DriverName, " & _
				"PA.PickupsAddressID, A.StreetNumber, A.StreetName, A.City, " & _
				"CONVERT(VARCHAR, PA.StartTime, 8) AS [Time], " & _
				"PS.RouteCode + '-' + PSEC.SectionCode AS [Route-Section], Bags, Boxes, Items " & _
				"FROM tblPickupsAddresses AS PA " & _
				"INNER JOIN tblTablets AS TT ON TT.TabletName = PA.DeviceName " & _
				"INNER JOIN tblPickupsSections AS PSEC ON PSEC.PickupsSectionID = PA.PickupsSectionID " & _
				"LEFT OUTER JOIN tblDriverAssignments AS DA ON DA.PickupScheduleID = PA.PickupScheduleID " & _
					"AND DA.TabletID = TT.TabletID AND DA.SectionID = PSEC.SectionID " & _
				"INNER JOIN tblAddresses AS A ON A.AddressID = PA.AddressID " & _
				"INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSEC.PickupScheduleID " & _
				"INNER JOIN tblPickupCycleDriverLocations AS PCDL ON PCDL.PickupCycleID = PS.PickupCycleID " & _
				"WHERE PA.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
					"AND PCDL.RegionID = " & ddlDriverLocations.SelectedValue & " " & _
					"AND DA.DriverAssignmentID IS NULL " & _
				"ORDER BY [Time]"
        End If

        dsPickupsByAddress.SelectCommand = Sql
        grdMain.DataBind()


        '        SELECT 
        '	CASE WHEN TT.TabletName = PA.DeviceName THEN 1 ELSE 0 END AS TabletMatch, PA.DeviceName AS TabletName, 
        '	D.DriverName, PA.PickupsAddressID, A.StreetNumber, A.StreetName, A.City, 
        '	CONVERT(VARCHAR, PA.StartTime, 8) AS [Time], DA.RouteCode + '-' + DA.SectionCode AS [Route-Section], Bags, Boxes, Items 
        'FROM tblDrivers AS DR 
        'INNER JOIN tblDriverAssignments AS DA ON DA.DriverID = DR.DriverID 
        'LEFT OUTER JOIN tblTablets AS TT ON TT.TabletID = DA.TabletID 
        'INNER JOIN tblPickupsAddresses AS PA ON PA.PickupScheduleID = DA.PickupScheduleID AND PA.DeviceName = TT.TabletName 
        'INNER JOIN tblPickupsSections AS PSEC ON PSEC.PickupsSectionID = PA.PickupsSectionID AND PSEC.SectionID = DA.SectionID 
        'INNER JOIN tblAddresses AS A ON A.AddressID = PA.AddressID  
        'INNER JOIN tblDrivers AS D ON D.DriverID = DA.DriverID 
        'WHERE DA.DriverID = 5 AND DA.PickupDate = '06/17/2014' AND DA.SectionID > 0 

        'SELECT 
        '	CASE WHEN TT.TabletName = PA.DeviceName THEN 1 ELSE 0 END AS TabletMatch, PA.DeviceName AS TabletName, 
        '                        '' AS DriverName, PA.PickupsAddressID, A.StreetNumber, A.StreetName, A.City, 
        '	CONVERT(VARCHAR, PA.StartTime, 8) AS [Time], DA.RouteCode + '-' + DA.SectionCode AS [Route-Section], Bags, Boxes, Items 
        'FROM tblPickupsAddresses AS PA 
        'INNER JOIN tblTablets AS TT ON TT.TabletName = PA.DeviceName
        'LEFT OUTER JOIN tblDriverAssignments AS DA ON DA.PickupScheduleID = PA.PickupScheduleID AND DA.TabletID = TT.TabletID 
        'INNER JOIN tblPickupsSections AS PSEC ON PSEC.PickupsSectionID = PA.PickupsSectionID AND PSEC.SectionID = DA.SectionID 
        'INNER JOIN tblAddresses AS A ON A.AddressID = PA.AddressID
        'INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSEC.PickupScheduleID
        'INNER JOIN tblPickupCycleDriverLocations AS PCDL ON PCDL.PickupCycleID = PS.PickupCycleID
        'WHERE PA.PickupDate = '06/17/2014' AND PCDL.RegionID = 1







		sql = "WITH RS AS " & _
			"( " & _
				"SELECT AIDS.AddressID " & _
				"FROM tblDrivers AS DR " & _
				"INNER JOIN tblDriverAssignments AS DA ON DA.DriverID = DR.DriverID " & _
				"CROSS APPLY (SELECT AddressID FROM tblPickupScheduleDetail AS PSD WHERE PSD.PickupScheduleID = DA.PickupScheduleID) AS AIDS " & _
				"WHERE DA.DriverID = " & cmbDrivers.SelectedItem.Value & " " & _
					"AND DA.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " & _
					"AND DA.SectionID > 0 " & _
			") " & _
			"SELECT A.StreetName " & _
			"FROM RS " & _
			"INNER JOIN tblAddresses AS A ON A.AddressID = RS.AddressID " & _
			"GROUP BY A.StreetName " & _
			"ORDER BY A.StreetName"

        dsStreetListing.SelectCommand = sql
        grdStreets.DataBind()

    End Sub

    Protected Sub ddlDriverLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriverLocations.SelectedIndexChanged
        LoadData()
    End Sub

    Protected Sub dtPickupDate_DateChanged(sender As Object, e As EventArgs) Handles dtPickupDate.DateChanged
        LoadData()
    End Sub

    Protected Sub cmbDrivers_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadData()
    End Sub
End Class
