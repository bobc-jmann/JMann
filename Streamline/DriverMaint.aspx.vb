Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DataUtil

Partial Class DriverMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

        dsDrivers.SelectCommand =
            "SELECT D.DriverID, D.FirstName, D.LastName, D.Driver, D.Bagger, " &
                "D.TabletID, D.PhoneID, D.TruckID, D.DriverLocationID, D.Active, G.DeviceDescription " &
            "FROM tblDrivers AS D " &
            "LEFT OUTER JOIN tblTrucks AS T ON T.TruckID = D.TruckID " &
            "LEFT OUTER JOIN GeotabDevices AS G ON G.DeviceID = T.DeviceID " &
            "WHERE D.DriverLocationID IN " & Session("userRegionsList") & " "

        If Not ckShowInactiveDrivers.Checked Then
            dsDrivers.SelectCommand += "And D.Active = 1 "
        End If

        dsDrivers.SelectCommand += "ORDER BY D.DriverName"

        dsRegions.SelectCommand =
            "SELECT RegionID, RegionCode " &
            "FROM tlkRegions " &
            "WHERE RegionID IN " & Session("userRegionsList") & " " &
            "ORDER BY RegionCode"

        Dim regionID_column = TryCast(grid.DataColumns("DriverLocationID"), GridViewDataComboBoxColumn)
        regionID_column.PropertiesComboBox.RequireDataBinding()
		dsRegions.DataBind()

        grid.DataBind()
    End Sub

    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
		e.NewValues("Driver") = False
		e.NewValues("Bagger") = False
		e.NewValues("Active") = True
	End Sub

	Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grid.RowValidating
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim cnt As Integer = 0

		Dim driverID As Integer = 0
		If Not IsNothing(e.Keys.Values(0)) Then
			driverID = e.Keys.Values(0)
		End If

		sql = "SELECT COUNT(*) AS Cnt " & _
			"FROM tblDrivers " & _
			"WHERE FirstName = '" & e.NewValues("FirstName") & "' " & _
				"AND LastName = '" & e.NewValues("LastName") & "' "

		If driverID > 0 Then
			sql += "AND DriverID <> " & driverID
		End If

		Try
			If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
				Return
			End If
			rsql.Read()
			cnt = rsql("Cnt")
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

		If cnt > 0 Then
			e.RowError = "This First Name and Last Name combination is already in use in another Driver entry."
			Return
		End If

		If Not e.NewValues("Driver") And Not e.NewValues("Bagger") Then
			e.RowError = "Employee must be a Driver, Bagger, or both."
			Return
		End If

		If e.NewValues("DriverLocationID") = 0 Then
			e.RowError = "An Employee must have a Region."
			Return
		End If
	End Sub

End Class
