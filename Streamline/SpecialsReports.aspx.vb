Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports System.Web.Services

Partial Class SpecialsReports
    Inherits System.Web.UI.Page
    Private UnselectDate As Boolean = False
    Private MonthChanged As Boolean = False

    Private gridPageSize As Integer = 5
    Private gridPageSize2 As Integer = 15

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'validate incoming URI:
        'If Not Request.UrlReferrer = Nothing Then
        '	If Request.UrlReferrer.AbsoluteUri.StartsWith(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_URI").ToString) Then
        '	Else
        '		Response.Redirect(Request.UrlReferrer.AbsoluteUri)
        '		Exit Sub
        '	End If
        'Else
        '	Response.Redirect("DeadEnd.htm")
        '	Exit Sub
        'End If

        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

        Select Case Request.HttpMethod
            Case "POST"
                If IsPostBack Then
                    Dim PostBackControlID As String = ""

                End If
            Case "GET"
                Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc", vConnStr)
                Dim dt As DataTable = New DataTable()
                da.Fill(dt)
                ddlLocations.DataSource = dt
                ddlLocations.DataTextField = "RegionDesc"
                ddlLocations.DataValueField = "RegionID"
                ddlLocations.DataBind()
                If Session("UserRegionDefault") <> 0 Then
                    ddlLocations.SelectedValue = Session("UserRegionDefault")
                End If

                ddlLocations.SelectedValue = Session("vLocationID")
            Case "HEAD"
                'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
        End Select
    End Sub

    Protected Sub cmdSpecialsSheet_Click(sender As Object, e As System.EventArgs) Handles cmdSpecialsSheet.Click
		Dim x As Object
		Dim vParams As String = ""
        x = calSpecials.SelectedDate
        vParams = vParams & "&PARMS=pickupDate~" & x
        x = ""
        For i = 0 To ddlLocations.Items.Count - 1
            If ddlLocations.Items(i).Selected Then
                If x <> "" Then
                    x = x & ","
                End If
                x = x & ddlLocations.Items(i).Value
            End If
        Next
        If x <> "" Then
            vParams = vParams & "|DriverLocation~" & x
        End If
        x = ""
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Sheet" & vParams)
	End Sub

	Protected Sub cmdSummaryReport_Click(sender As Object, e As System.EventArgs) Handles cmdSummaryReport.Click
		Dim x As Object
		Dim vParams As String = ""
		x = calSpecials.SelectedDate
		vParams = vParams & "&PARMS=pickupDate~" & x
		x = ""
		For i = 0 To ddlLocations.Items.Count - 1
			If ddlLocations.Items(i).Selected Then
				If x <> "" Then
					x = x & ","
				End If
				x = x & ddlLocations.Items(i).Value
			End If
		Next
		If x <> "" Then
			vParams = vParams & "|DriverLocation~" & x
		End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Summary" & vParams)
	End Sub

	Protected Sub cmdAddressesForRouting_Click(sender As Object, e As System.EventArgs) Handles cmdAddressesForRouting.Click
		Dim x As Object
		Dim vParams As String = ""
		x = calSpecials.SelectedDate
		vParams = vParams & "&PARMS=pickupDate~" & x
		x = ""
		For i = 0 To ddlLocations.Items.Count - 1
			If ddlLocations.Items(i).Selected Then
				If x <> "" Then
					x = x & ","
				End If
				x = x & ddlLocations.Items(i).Value
			End If
		Next
		If x <> "" Then
			vParams = vParams & "|DriverLocation~" & x
		End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Specials Addresses for Routing" & vParams)
	End Sub

	Protected Sub cmdConfirmRedTagReport_Click(sender As Object, e As System.EventArgs) Handles cmdConfirmRedTagReport.Click
		Dim x As Object
		Dim parms As String = ""
		x = calSpecials.SelectedDate
		parms = parms & "&PARMS=pickupDate~" & x & "|"
		x = ""
		If ddlLocations.Items.Count > 0 Then
			x = ddlLocations.Items(0).Value
		Else
			x = "0"
		End If
		parms = parms & "regionID~" & x
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Confirm and Do Not Red Tag Sheet" & parms)
	End Sub

	Protected Sub cmdContainerPickupsSheet_Click(sender As Object, e As System.EventArgs) Handles cmdContainerPickupsSheet.Click
		Dim x As Object
		Dim vParams As String = ""
		x = calSpecials.SelectedDate
		vParams = vParams & "&PARMS=pickupDate~" & x
		x = ""
		For i = 0 To ddlLocations.Items.Count - 1
			If ddlLocations.Items(i).Selected Then
				If x <> "" Then
					x = x & ","
				End If
				x = x & ddlLocations.Items(i).Value
			End If
		Next
		If x <> "" Then
			vParams = vParams & "|DriverLocation~" & x
		End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Container Pickups Sheet" & vParams)
	End Sub

	Protected Sub cmdContainerPickupsForRouting_Click(sender As Object, e As System.EventArgs) Handles cmdContainerPickupsForRouting.Click
		Dim x As Object
		Dim vParams As String = ""
		x = calSpecials.SelectedDate
		vParams = vParams & "&PARMS=pickupDate~" & x
		x = ""
		For i = 0 To ddlLocations.Items.Count - 1
			If ddlLocations.Items(i).Selected Then
				If x <> "" Then
					x = x & ","
				End If
				x = x & ddlLocations.Items(i).Value
			End If
		Next
		If x <> "" Then
			vParams = vParams & "|DriverLocation~" & x
		End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Container Pickups for Routing" & vParams)
	End Sub

	Protected Sub cmdDoNotBagSheet_Click(sender As Object, e As System.EventArgs) Handles cmdDoNotBagSheet.Click
		Dim x As Object
		Dim vParams As String = ""
		x = calSpecials.SelectedDate
		vParams = vParams & "&PARMS=PickupDate~" & x
		x = ""
		For i = 0 To ddlLocations.Items.Count - 1
			If ddlLocations.Items(i).Selected Then
				If x <> "" Then
					x = x & ","
				End If
				x = x & ddlLocations.Items(i).Value
			End If
		Next
		If x <> "" Then
			vParams = vParams & "|driverLocation~" & x
		End If
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Do Not Bag Sheet" & vParams)
	End Sub

	Protected Sub cmdDoNotBagSheet_Unscheduled_Click(sender As Object, e As System.EventArgs) Handles cmdDoNotBagSheet_Unscheduled.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Do Not Bag Sheet - Unscheduled")
	End Sub

	Protected Sub btnScheduleCalendar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScheduleCalendar.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Schedule Calendar")
	End Sub

End Class
