Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DataUtil

Partial Class GeotabDeviceMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

        dsDevices.SelectCommand =
            "SELECT DeviceID, DeviceSerialNumber, DeviceDescription, DriverLocationID, Active " &
            "FROM GeotabDevices " &
            "WHERE DriverLocationID IN " & Session("userRegionsList") & " " &
            "ORDER BY DeviceDescription"

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
        e.NewValues("Active") = True
    End Sub

    Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grid.RowValidating
        Dim sql As String = ""
        Dim rsql As SqlDataReader = Nothing
        Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
        Dim cnt As Integer = 0

        Dim deviceID As Integer = 0
        If Not IsNothing(e.Keys.Values(0)) Then
            deviceID = e.Keys.Values(0)
        End If

        sql = "SELECT COUNT(*) AS Cnt " &
            "FROM GeotabDevices " &
            "WHERE DeviceDescription = '" & e.NewValues("DeviceDescription") & "' "

        If deviceID > 0 Then
            sql += "AND DeviceID <> " & deviceID
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
            e.RowError = "This Device Description is already in use in another Device entry."
            Return
        End If

        If e.NewValues("DriverLocationID") = 0 Then
            e.RowError = "A Device must have a Region."
            Return
        End If
    End Sub

End Class
