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

Partial Class SpecialsGrading
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sql As String = ""

        'DCM - added for security
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

                ckShowAllSpecials.Checked = False
            Case "HEAD"

        End Select

        LoadSpecials()
    End Sub

    Private Sub LoadSpecials()
        If (ddlDriverLocations.SelectedValue = "0") Then
            Return
        End If

		dsDriver.SelectCommand = "SELECT D.DriverID, D.DriverName " & _
			"FROM tblDrivers AS D " & _
			"WHERE D.DriverLocationID = " & ddlDriverLocations.SelectedValue & " " & _
				"AND D.Active = 1 " & _
			"ORDER BY D.DriverName"
		Dim driverID_column = TryCast(grdSpecialsNotGraded.DataColumns("DriverID"), GridViewDataComboBoxColumn)
		driverID_column.PropertiesComboBox.RequireDataBinding()
		dsDriver.DataBind()

        Dim sql As String = "SELECT DISTINCT SP.PickupID, SP.DriverLocation, RG.RegionCode, SP.PickupDate, " & _
                 "A.City, A.StreetName, A.StreetNumber, A.StreetAddress, SUBSTRING(A.ZIP, 1, 5) AS Zip, " & _
                 "SP.ItemBags, SP.ItemBoxes, SP.ItemOther, DriverID, " & _
                 "COALESCE(CASE WHEN SP.EndTime < '20000101' THEN SP.PickupDate ELSE SP.EndTime END, SP.PickupDate) AS EndTime, SP.Grade, SP.Status " & _
             "FROM tblSpecials SP " & _
             "INNER JOIN tblAddresses A ON SP.AddressID = A.AddressID " & _
             "INNER JOIN tlkRegions AS RG ON SP.DriverLocation = RG.RegionDesc " & _
             "WHERE "
        If Not ckShowAllSpecials.Checked Then
            sql += "SP.Status = 'SCHEDULED' AND "
        End If
        sql += "SP.PickupDate = '" & Format(dtPickupDate.Value, "yyyyMMdd") & "' " & _
                 "AND SP.DriverLocation = '" & ddlDriverLocations.SelectedItem.Text & "' " & _
             "ORDER BY A.City, A.StreetName, A.StreetNumber"

        dsSpecialsNotGraded.SelectCommand = sql
        grdSpecialsNotGraded.DataBind()

    End Sub

    Protected Sub btnRunSpecialsNotGradedReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSpecialsNotGradedReport.Click
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
        rs.Close()

        Dim parms As String = "&PARMS=startDate~" & DateAdd(DateInterval.Day, -5, dtPickupDate.Value) & _
            "|endDate~" & dtPickupDate.Value & "|driverLocationCode~" & locationCode
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Not Graded" & parms)

		SqlQueryClose(connSQL, rs)
	End Sub

End Class
