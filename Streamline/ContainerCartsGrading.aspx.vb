Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DataUtil

Partial Class ContainerCartsGrading
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

                ckShowAllPickups.Checked = False
            Case "HEAD"

        End Select

        LoadSpecials()
    End Sub

    Private Sub LoadSpecials()
        If (ddlDriverLocations.SelectedValue = "0") Then
            Return
        End If

        dsDrivers.SelectCommand = "SELECT DriverID, DriverName " & _
            "FROM tblDrivers AS D " & _
            "WHERE D.DriverLocationID = " & ddlDriverLocations.SelectedValue & " " & _
            "ORDER BY D.DriverName"
        dsDrivers.DataBind()

        Dim sql As String = "SELECT DISTINCT CT.PickupID, CT.DriverLocation, RG.RegionCode, CT.PickupDate, " & _
                 "CT.City, CT.StreetName, CT.Address, SUBSTRING(CT.ZIP, 1, 5) AS Zip, " & _
                 "CT.ItemBags, CT.ItemBoxes, CT.ItemOther, CT.DriverID, CT.SoftCarts, CT.HardCarts, " & _
                 "COALESCE(CASE WHEN CT.EndTime < '20000101' THEN CT.PickupDate ELSE CT.EndTime END, CT.PickupDate) AS EndTime, CT.Grade, CT.Status " & _
             "FROM tblContainers CT " & _
             "INNER JOIN tlkRegions AS RG ON CT.DriverLocation = RG.RegionDesc " & _
             "WHERE "
        If Not ckShowAllPickups.Checked Then
            sql += "CT.Status = 'SCHEDULED' AND "
        End If
        sql += "CT.PickupDate = '" & Format(dtPickupDate.Value, "yyyyMMdd") & "' " & _
                 "AND CT.DriverLocation = '" & ddlDriverLocations.SelectedItem.Text & "' " & _
             "ORDER BY CT.City, CT.StreetName, CT.Address"

        dsContainerPickupsNotGraded.SelectCommand = sql
        grdContainerPickupsNotGraded.DataBind()
    End Sub

	Protected Sub btnRunContainerPickupsNotGradedReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnContainerPickupsNotGradedReport.Click
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		If ddlDriverLocations.SelectedValue = "0" Then
			Return
		End If

		Dim sql As String = "SELECT RG.RegionCode FROM tlkRegions AS RG WHERE RG.RegionID = " & ddlDriverLocations.SelectedValue
		If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
			Return
		End If
		rs.Read()
		Dim locationCode As String = rs("RegionCode")
		SqlQueryClose(connSQL, rs)

		Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, dtPickupDate.Value) & _
			"|endDate~" & dtPickupDate.Value & "|driverLocationCode~" & locationCode
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Container Pickups Not Graded" & parms)
	End Sub

End Class
