Imports DevExpress.Web
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DataUtil

Partial Class PickupCycleMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

        Select Case Request.HttpMethod
            Case "POST"
                If IsPostBack Then
                   
                End If
            Case "GET"
                ckShowInactivePickupCycles.Checked = False
        End Select
        dsRegions.SelectCommand =
            "SELECT RegionID, RegionCode " & _
            "FROM tlkRegions " & _
            "ORDER BY RegionCode"

		Dim sql As String =
		   "SELECT DISTINCT PC.Active, PC.PickupCycleID, PickupCycleAbbr, PickupCycleDesc, " & _
				"PermitID, PickupCycleTemplateID, LastWeekScheduled, LastDayScheduled, CardBagCode, InitialLastPickupDate " & _
			"FROM tblPickupCycles AS PC " & _
			"LEFT OUTER JOIN tblPickupCycleDriverLocations AS PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
			"WHERE (PCDL.RegionID IN " & Session("userRegionsList") & " OR PCDL.RegionID IS NULL) "

        If Not ckShowInactivePickupCycles.Checked Then
            sql += "And Active = 1 "
        End If

        sql += "ORDER BY PickupCycleAbbr"

        dsPickupCycles.SelectCommand = Sql
        grdPickupCycles.DataBind()
    End Sub

    Protected Sub grdPickupCycles_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("PickupCycleID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

	Protected Sub grdPickupCycles_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		If IsNothing(e.NewValues("PickupCycleAbbr")) OrElse Trim(e.NewValues("PickupCycleAbbr")) = "" Then
			e.RowError = "Please enter a Pickup Cycle Abbreviation."
			Return
		End If
		If IsNothing(e.NewValues("PickupCycleDesc")) OrElse Trim(e.NewValues("PickupCycleDesc")) = "" Then
			e.RowError = "Please enter a Pickup Cycle Description."
			Return
		End If
		If IsNothing(e.NewValues("PermitID")) Then
			e.RowError = "Please select a Permit."
			Return
		End If
		If IsNothing(e.NewValues("PickupCycleTemplateID")) Then
			e.RowError = "Please select a Pickup Cycle Template."
			Return
		End If
		If IsNothing(e.NewValues("LastWeekScheduled")) Then
			e.RowError = "Please enter a Last Week Scheduled."
			Return
		End If
		If IsNothing(e.NewValues("LastDayScheduled")) Then
			e.RowError = "Please select a Last Day Scheduled."
			Return
		End If
		If IsNothing(e.NewValues("CardBagCode")) OrElse Trim(e.NewValues("CardBagCode")) = "" Then
			e.RowError = "Please enter a Card Bag Code."
			Return
		End If
		If IsNothing(e.NewValues("InitialLastPickupDate")) Then
			e.RowError = "Please enter an Initial Last PickupDate."
			Return
		End If
	End Sub

	Protected Sub grdPickupCycleDriverLocations_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		Dim sql As String = ""
		Dim cnt As Integer = 0

		cnt = SQLExecuteScalar("SELECT COUNT(*) FROM tblPickupCycleDriverLocations " & _
			"WHERE PickupCycleID = " & Session("PickupCycleID") & " AND PrimaryRegion = 1", "Notify User")

		If e.OldValues("PrimaryRegion") Then
			cnt -= 1
		End If
		If e.NewValues("PrimaryRegion") Then
			cnt += 1
		End If

		If cnt = 0 Then
			e.RowError = "There must be one Primary Region."
			Return
		ElseIf cnt > 1 Then
			sql = "UPDATE tblPickupCycleDriverLocations " & _
				"SET PrimaryRegion = 0 " & _
				"WHERE PickupCycleID = " & Session("PickupCycleID") & " "
			If Not IsNothing(e.Keys.Values(0)) Then
				sql += "AND PickupCycleDriverLocationID <> " & e.Keys.Values(0)
			End If

			SqlNonQuery(sql)
		End If

	End Sub

    Protected Sub grdPickupCycleDriverLocations_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        If e.Values("PrimaryRegion") Then
            e.Cancel = True
        End If
    End Sub

    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grdPickupCycles.InitNewRow
        e.NewValues("Active") = True
    End Sub

End Class
