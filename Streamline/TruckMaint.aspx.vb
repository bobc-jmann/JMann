Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DataUtil

Partial Class TruckMaint
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

        dsTrucks.SelectCommand =
            "SELECT TruckID, TruckNumber, Truck, Vehicle, DriverLocationID, Active, DeviceID " &
            "FROM tblTrucks " &
            "WHERE DriverLocationID IN " & Session("userRegionsList") & " " &
            "ORDER BY TruckNumber"

        dsRegions.SelectCommand =
            "SELECT RegionID, RegionCode " &
            "FROM tlkRegions " &
            "WHERE RegionID IN " & Session("userRegionsList") & " " &
            "ORDER BY RegionCode"

        dsDevices.SelectCommand =
            "SELECT DeviceID, DeviceDescription " &
            "FROM GeotabDevices " &
            "WHERE DriverLocationID IN " & Session("userRegionsList") & " " &
            "UNION " &
            "SELECT 0 AS DeviceID, '' AS DeviceDescription " &
            "ORDER BY DeviceDescription"

        Dim regionID_column = TryCast(grid.DataColumns("DriverLocationID"), GridViewDataComboBoxColumn)
        regionID_column.PropertiesComboBox.RequireDataBinding()
        dsRegions.DataBind()

        Dim deviceID_column = TryCast(grid.DataColumns("DeviceID"), GridViewDataComboBoxColumn)
        deviceID_column.PropertiesComboBox.RequireDataBinding()
        dsDevices.DataBind()

        grid.DataBind()
    End Sub

	Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
		e.NewValues("Truck") = False
		e.NewValues("Vehicle") = False
		e.NewValues("Active") = True
	End Sub

	Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grid.RowValidating
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim cnt As Integer = 0

		Dim truckID As Integer = 0
		If Not IsNothing(e.Keys.Values(0)) Then
			truckID = e.Keys.Values(0)
		End If

        sql = "SELECT COUNT(*) AS Cnt " &
            "FROM tblTrucks " &
            "WHERE TruckNumber = '" & e.NewValues("TruckNumber") & "' "

        If truckID > 0 Then
			sql += "AND TruckID <> " & truckID
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
			e.RowError = "This Truck Number is already in use in another Truck entry."
			Return
		End If

		If Not e.NewValues("Truck") And Not e.NewValues("Vehicle") Then
			e.RowError = "Entry must be a Truck or a Vehicle."
			Return
		End If

		If e.NewValues("Truck") And e.NewValues("Vehicle") Then
			e.RowError = "Entry cannot be a Truck and a Vehicle."
			Return
		End If

		If e.NewValues("DriverLocationID") = 0 Then
            e.RowError = "A Truck/Vehicle must have a Region."
            Return
		End If
	End Sub

End Class
