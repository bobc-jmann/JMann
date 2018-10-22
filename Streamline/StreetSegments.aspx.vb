Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DevExpress.Utils


Partial Class MissingStreetSegments
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Dim sql As String = ""

		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Dim PostBackControlID As String = ""
		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					Try
						PostBackControlID = GetPostBackControl(Me.Page).ID
					Catch ex As Exception

					End Try

					Select Case PostBackControlID
						Case Else

					End Select
				End If
			Case "GET"

				sql = "SELECT RegionID, RegionDesc FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc"
				Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
				Dim dt As DataTable = New DataTable()
				da.Fill(dt)
				ddlDriverLocations.DataSource = dt
				ddlDriverLocations.DataTextField = "RegionDesc"
				ddlDriverLocations.DataValueField = "RegionID"
				ddlDriverLocations.DataBind()
				ddlDriverLocations.Items.Insert(0, New ListItem("<All Driver Locations>", "0"))
				If Session("UserRegionDefault") <> 0 Then
					ddlDriverLocations.SelectedValue = Session("UserRegionDefault")
				End If

				' Load Routes
				sql = "SELECT RouteID, RouteCode FROM tblRoutes WHERE Active = 1 ORDER BY RouteCode"
				da = New SqlDataAdapter(sql, vConnStr)
				dt = New DataTable()
				da.Fill(dt)
				ddlRoutes.DataSource = dt
				ddlRoutes.DataTextField = "RouteCode"
				ddlRoutes.DataValueField = "RouteID"
				ddlRoutes.DataBind()
				ddlRoutes.Items.Insert(0, New ListItem("<All Routes>", "0"))
		End Select

		LoadMissingSegments()
	End Sub

	Private Sub LoadMissingSegments()

		Dim sql As String = "SELECT MissingSegmentID, RegionCode, SectionID, SectionCode, RouteID, RouteCode, " & _
				"StreetName, Zip5, MinSN, MaxSN, City, " & _
				"Resolution, FromStreetName, ToStreetName, Comment, ModifiedOn, ModifiedBy " & _
			"FROM MapInfo.dbo.MissingSegments AS MS " & _
			"INNER JOIN tlkRegions AS RG ON RG.RegionID = MS.RegionID " & _
			"WHERE (" & ddlDriverLocations.SelectedValue & " = 0 OR " & ddlDriverLocations.SelectedValue & " = MS.RegionID) " & _
				"AND (" & ddlRoutes.SelectedValue & " = 0 OR " & ddlRoutes.SelectedValue & " = MS.RouteID) " & _
			"ORDER BY RouteCode, SectionCode, Zip5, StreetName"

		dsMissingSegments.SelectCommand = sql
		grdMissingSegments.DataBind()
	End Sub

	Protected Sub grdStreetNameLike_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridCustomCallbackEventArgs)
		grdStreetNameSearch.DataBind()
	End Sub

	Function RunFindMissingSegments() As Integer
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spFindMissingSegments"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.CommandTimeout = 150
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "Specials, RunFindMissingSegments")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "Specials, RunFindMissingSegments")
		Finally
			conn.Close()
		End Try

		Return errorID
	End Function

	Function RunCreateNewRules() As Integer
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spCreateNewSegmentAddressRules"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.CommandTimeout = 150
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "StreetSegments, RunCreateNewRules")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try

		Return errorID
	End Function

	Function RunUpdateGeocoderStreets() As Integer
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spUpdateGeocoderStreets"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.CommandTimeout = 150
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "StreetSegments, RunUpdateGeocoderStreets")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "StreetSegments, RunUpdateGeocoderStreets")
		Finally
			conn.Close()
		End Try

		Return errorID
	End Function



End Class
