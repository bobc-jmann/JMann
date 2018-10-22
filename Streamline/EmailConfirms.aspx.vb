Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DataUtil

Partial Class EmailConfirms
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		dsEmailConfirms.SelectCommand =
			"SELECT PSD.PickupScheduleDetailID, PS.PickupDate, PC.PickupCycleAbbr, " & _
				"PS.RouteCode, PSS.SectionCode, PSD.Confirmed, PSD.Comments, A.StreetAddress, A.City, A.Zip5 " & _
			"FROM tblAddresses AS A " & _
			"INNER JOIN tblPickupScheduleDetail AS PSD ON PSD.AddressID = A.AddressID " & _
			"INNER JOIN tblPickupScheduleSections AS PSS ON PSS.PickupScheduleSectionID = PSD.PickupScheduleSectionID " & _
			"INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSS.PickupScheduleID " & _
			"INNER JOIN tblPickupCycles AS PC ON PC.PickupCycleID = PS.PickupCycleID " & _
			"WHERE PS.pickupdate >= '" & Format(Now, "yyyyMMdd") & "' " & _
				"AND PSD.Comments IS NOT NULL " & _
				"AND PSD.Comments <> '' " & _
				"AND SUBSTRING(PSD.Comments, 4, 1) = '-' " & _
			"ORDER BY PS.pickupdate, PC.PickupCycleAbbr, PS.RouteCode, PSS.SectionCode"

		grid.DataBind()
	End Sub

End Class
