Imports DevExpress.Web
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DataUtil

Partial Class DriverLog
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Dim sql As String
		Dim da As SqlDataAdapter
		Dim dt As DataTable
		Dim PostBackControlID As String = ""
		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					Try
						PostBackControlID = GetPostBackControl(Me.Page).ID
					Catch ex As Exception

					End Try

					Select Case PostBackControlID
						Case "ddlDriverLocations"
							sql = "SELECT DriverID, DriverName FROM tblDrivers " & _
									"WHERE DriverLocationID = " & ddlDriverLocations.SelectedValue & " " & _
									"ORDER BY DriverName"

							da = New SqlDataAdapter(sql, vConnStr)
							dt = New DataTable()
							da.Fill(dt)
							ddlDrivers.DataSource = dt
							ddlDrivers.DataTextField = "DriverName"
							ddlDrivers.DataValueField = "DriverID"
							ddlDrivers.DataBind()
							ddlDrivers.Items.Insert(0, New ListItem("Select Driver", "0"))
						Case "ddlDrivers"
							grdPickupDates.DataBind()
						Case Else

					End Select
				End If
			Case "GET"
				dtEarliestPickupDate.Value = DateAdd(DateInterval.Month, -2, Now)

				sql = "SELECT RegionID, RegionDesc FROM tlkRegions " & _
					"WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc"
				da = New SqlDataAdapter(sql, vConnStr)
				dt = New DataTable()
				da.Fill(dt)
				ddlDriverLocations.DataSource = dt
				ddlDriverLocations.DataTextField = "RegionDesc"
				ddlDriverLocations.DataValueField = "RegionID"
				ddlDriverLocations.DataBind()
				ddlDriverLocations.Items.Insert(0, New ListItem("Select Driver Location", "0"))
				If Session("UserRegionDefault") <> 0 Then
					ddlDriverLocations.SelectedValue = Session("UserRegionDefault")
				End If
		End Select
	End Sub

	Protected Sub grdPickupDate_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("PickupDate") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
	End Sub

	Protected Sub grdDriverLog_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)
		e.NewValues("StartTime") = CDate(Session("PickupDate"))
		e.NewValues("EndTime") = CDate(Session("PickupDate"))
	End Sub

	Protected Sub btnDriverLogReport_Click(ByVal sender As Object, ByVal e As EventArgs)
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Driver Log Report")
	End Sub

End Class
