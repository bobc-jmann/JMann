Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web.UI
Imports DevExpress.Utils
Imports DevExpress.Web.ASPxHtmlEditor
Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports DataUtil


Imports System.Data
Imports System.Data.Sql
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DevExpress.XtraPrinting

Imports DevExpress.Web.Internal

Partial Class Mapping
    Inherits System.Web.UI.Page

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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
						Case "ddlMapType"
							ResetDropDownLists()
						Case "ddlTemplates"
							LoadRoutes()
						Case "ddlRoutes"
							LoadSections()
						Case "ddlSections"
							DisplaySectionOptions()
						Case "ddlTemplateUpdate"
							LoadRoutesUpdate()
						Case "ddlRouteUpdate"
							LoadSectionsUpdate()
						Case "ckActiveTemplates"
							LoadTemplates()
						Case "ckInactiveTemplates"
							LoadTemplates()
						Case "ckActiveRoutes"
							LoadRoutes()
						Case "ckActiveRoutes"
							LoadRoutes()
						Case "ckActiveSections"
							LoadSections()
						Case "ckActiveSections"
							LoadSections()
						Case "rbUpdateList"
							If rbUpdateList.SelectedIndex > 0 Then
								ddlTemplateUpdate.SelectedValue = ddlTemplates.SelectedValue
								LoadRoutesUpdate()
								ddlRouteUpdate.SelectedValue = 0
							End If
							'Case "ckNonRouteAddressesZip"
							'	If ckNonRouteAddressesZip.Checked Then
							'		ckNonRouteAddressesNearby.Checked = False
							'	End If
							'Case "ckNonRouteAddressesNearby"
							'	If ckNonRouteAddressesNearby.Checked Then
							'		ckNonRouteAddressesZip.Checked = False
							'	End If
					End Select
				End If

				If ddlMapType.SelectedValue = 0 Then
					tblDrawMap.Style.Add("display", "none")
				Else
					tblDrawMap.Style.Add("display", "block")
				End If

				tblRegion.Style.Add("display", "none")
				tblCity.Style.Add("display", "none")
				tblTemplate.Style.Add("display", "none")

				If ddlMapType.SelectedValue = 1 Then
					tblRegion.Style.Add("display", "block")
				End If

				If ddlMapType.SelectedValue = 2 Then
					tblCity.Style.Add("display", "block")
				End If

				If ddlMapType.SelectedValue = 3 Then
					tblTemplate.Style.Add("display", "block")
					DisplaySectionOptions()
				End If

				ResizeMap(cbMapSizes.SelectedIndex)

			Case "GET"
				userID.Value = Session("vUserID")
				ResizeMap(0)

				If Session("MappingUpdate") = "Update" Then
					mappingUpdate.Value = True
				Else
					mappingUpdate.Value = False
				End If

				ddlMapType.Items.Insert(0, New ListItem("<Select Map Type>", "0"))
				ddlMapType.Items.Insert(1, New ListItem("Region", "1"))
				ddlMapType.Items.Insert(2, New ListItem("City", "2"))
				ddlMapType.Items.Insert(3, New ListItem("Template, Route, Section", "3"))
				'ddlMapType.Items.Insert(4, New ListItem("Zip5, Carrier Route", "4"))

				Dim sql As String = "SELECT RegionID, RegionCode FROM tlkRegions ORDER BY RegionCode"
				Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
				Dim dt As DataTable = New DataTable()
				da.Fill(dt)
				ddlRegion.DataSource = dt
				ddlRegion.DataTextField = "RegionCode"
				ddlRegion.DataValueField = "RegionID"
				ddlRegion.DataBind()
				ddlRegion.Items.Insert(0, New ListItem("<Select Region>", "0"))

				sql = "SELECT DISTINCT City FROM tlkDesignatedCityFromRoute ORDER BY City"
				da = New SqlDataAdapter(sql, vConnStr)
				dt = New DataTable()
				da.Fill(dt)
				ddlCity.DataSource = dt
				ddlCity.DataTextField = "City"
				ddlCity.DataValueField = "City"
				ddlCity.DataBind()
				ddlCity.Items.Insert(0, New ListItem("<Select City>", "0"))

				sql = "SELECT StatusID, [Status] FROM tlkStatuses ORDER BY [Status]"
				da = New SqlDataAdapter(sql, vConnStr)
				dt = New DataTable()
				da.Fill(dt)
				ddlStatusesUpdate.DataSource = dt
				ddlStatusesUpdate.DataTextField = "Status"
				ddlStatusesUpdate.DataValueField = "StatusID"
				ddlStatusesUpdate.DataBind()
				ddlStatusesUpdate.Items.Insert(0, New ListItem("<Select Status>", "0"))

				ckNonRouteAddressesNearby.Checked = True
				rbAddressStatuses.SelectedIndex = 1
				rbUpdateList.SelectedIndex = 0
				txtNearbyDistance.Text = "0.5"

				hfUncommittedChanges.Value = 0
				hfMapTitle.Value = ""
			Case "HEAD"
				'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
		End Select

		If hfUncommittedChanges.Value = 1 Then
			lblUcommittedChanges.Text = "This route has Uncommitted Changes"
		Else
			lblUcommittedChanges.Text = ""
		End If
	End Sub

	Private Sub ResetDropDownLists()
		Select Case ddlMapType.SelectedValue
			Case 1
				ddlRegion.SelectedValue = "0"
			Case 2
				ddlCity.SelectedValue = "0"
			Case 3
				LoadTemplates()

				ckActiveTemplates.Checked = True
				ckInactiveTemplates.Checked = False
				ckActiveRoutes.Checked = True
				ckInactiveRoutes.Checked = False
				ckActiveSections.Checked = True
				ckInactiveSections.Checked = False

				ddlTemplates.SelectedValue = "0"
				ddlRoutes.Items.Clear()
				ddlSections.Items.Clear()
			Case 4
				ddlRegion.SelectedValue = "0"
		End Select
	End Sub

	Private Sub LoadTemplates()
		Dim sql As String = "SELECT PickupCycleTemplateID, PickupCycleTemplateCode " & _
			"FROM tblPickupcycleTemplates "

		If Not ckActiveTemplates.Checked And Not ckInactiveTemplates.Checked Then
			ckActiveTemplates.Checked = True ' Don't let user uncheck both boxes
		End If

		If ckActiveTemplates.Checked And Not ckInactiveTemplates.Checked Then
			sql &= "WHERE Active = 1 "
		ElseIf Not ckActiveTemplates.Checked And ckInactiveTemplates.Checked Then
			sql &= "WHERE Active = 0 "
		End If

		sql &= "ORDER BY PickupCycleTemplateCode"

		Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)
		ddlTemplates.Items.Clear()
		ddlTemplates.DataSource = dt
		ddlTemplates.DataTextField = "PickupCycleTemplateCode"
		ddlTemplates.DataValueField = "PickupCycleTemplateID"
		ddlTemplates.DataBind()
		ddlTemplates.Items.Insert(0, New ListItem("<Select Template>", "0"))

		ddlTemplateUpdate.Items.Clear()
		ddlTemplateUpdate.DataSource = dt
		ddlTemplateUpdate.DataTextField = "PickupCycleTemplateCode"
		ddlTemplateUpdate.DataValueField = "PickupCycleTemplateID"
		ddlTemplateUpdate.DataBind()
		ddlTemplateUpdate.Items.Insert(0, New ListItem("<Select Template>", "0"))

		If ckInactiveTemplates.Checked Then
			ddlTemplates.Items.Insert(1, New ListItem("<Routes w/o Template>", "99"))
		End If
	End Sub

	Private Sub LoadRoutes()
		If ddlTemplates.SelectedValue = 0 Then
			Return
		End If

		Dim sql As String = "SELECT DISTINCT R.RouteID, R.RouteCode " & _
			"FROM tblRoutes AS R " & _
			"INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.RouteID = R.RouteID " & _
			"INNER JOIN tblPickupCycleTemplates AS PCT ON PCT.PickupCycleTemplateID = PCTD.PickupCycleTemplateID " & _
			"WHERE PCT.PickupCycleTemplateID = " & ddlTemplates.SelectedValue & " "

		If Not ckActiveRoutes.Checked And Not ckInactiveRoutes.Checked Then
			ckActiveRoutes.Checked = True ' Don't let user uncheck both boxes
		End If

		If ckActiveRoutes.Checked And Not ckInactiveRoutes.Checked Then
			sql &= "AND R.Active = 1 "
		ElseIf Not ckActiveRoutes.Checked And ckInactiveRoutes.Checked Then
			sql &= "AND R.Active = 0 "
		End If

		sql &= "ORDER BY R.RouteCode"

		Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)
		ddlRoutes.Items.Clear()
		ddlRoutes.DataSource = dt
		ddlRoutes.DataTextField = "RouteCode"
		ddlRoutes.DataValueField = "RouteID"
		ddlRoutes.DataBind()
		ddlRoutes.Items.Insert(0, New ListItem("<Select Route>", "0"))

		ddlSections.Items.Clear()
		DisplaySectionOptions()
	End Sub

	Private Sub LoadRoutesUpdate()
		If ddlTemplateUpdate.SelectedValue = 0 Then
			Return
		End If

		Dim sql As String = "SELECT R.RouteID, R.RouteCode " & _
			"FROM tblRoutes AS R " & _
			"INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.RouteID = R.RouteID " & _
			"INNER JOIN tblPickupCycleTemplates AS PCT ON PCT.PickupCycleTemplateID = PCTD.PickupCycleTemplateID " & _
			"WHERE PCT.PickupCycleTemplateID = " & ddlTemplateUpdate.SelectedValue & " "

		If Not ckActiveRoutes.Checked And Not ckInactiveRoutes.Checked Then
			ckActiveRoutes.Checked = True ' Don't let user uncheck both boxes
		End If

		If ckActiveRoutes.Checked And Not ckInactiveRoutes.Checked Then
			sql &= "AND R.Active = 1 "
		ElseIf Not ckActiveRoutes.Checked And ckInactiveRoutes.Checked Then
			sql &= "AND R.Active = 0 "
		End If

		sql &= "ORDER BY R.RouteCode"

		Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)
		ddlRouteUpdate.Items.Clear()
		ddlRouteUpdate.DataSource = dt
		ddlRouteUpdate.DataTextField = "RouteCode"
		ddlRouteUpdate.DataValueField = "RouteID"
		ddlRouteUpdate.DataBind()
		ddlRouteUpdate.Items.Insert(0, New ListItem("<Select Route>", "0"))

		ddlSectionUpdate.Items.Clear()
	End Sub

	Private Sub LoadSections()
		If ddlRoutes.SelectedValue = 0 Then
			Return
		End If

		Dim sql As String = "SELECT COUNT(*) " & _
			"FROM tblRoutes " & _
			"WHERE RouteID = " & ddlRoutes.SelectedValue & " " & _
				"AND UncommittedChanges = 1"
		Dim cnt As Integer = SQLExecuteScalar(sql, "Notify User")

		If cnt = 1 Then
			lblUcommittedChanges.Text = "This route has Uncommitted Changes"
			hfUncommittedChanges.Value = 1
		Else
			lblUcommittedChanges.Text = ""
			hfUncommittedChanges.Value = 0
		End If

		sql = "SELECT S.SectionID, S.SectionCode " & _
			"FROM tblSections AS S "

		If hfUncommittedChanges.Value = 0 Then
			sql += "INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID "
		Else
			sql += "INNER JOIN tblRoutes AS R ON R.RouteID = S.UncommittedRouteID "
		End If
		sql += "WHERE R.RouteID = " & ddlRoutes.SelectedValue & " "

		If Not ckActiveSections.Checked And Not ckInactiveSections.Checked Then
			ckActiveSections.Checked = True	' Don't let user uncheck both boxes
		End If

		If ckActiveSections.Checked And Not ckInactiveSections.Checked Then
			sql &= "AND S.Active = 1 "
		ElseIf Not ckActiveSections.Checked And ckInactiveSections.Checked Then
			sql &= "AND S.Active = 0 "
		End If

		sql &= "ORDER BY S.SectionCode"

		Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)
		ddlSections.Items.Clear()
		ddlSections.DataSource = dt
		ddlSections.DataTextField = "SectionCode"
		ddlSections.DataValueField = "SectionID"
		ddlSections.DataBind()
		ddlSections.Items.Insert(0, New ListItem("<Select Section>", "0"))

		ddlRouteUpdate.SelectedIndex = 0
		lblMessage.Text = ""
	End Sub

	Private Sub LoadSectionsUpdate()
		If ddlRoutes.SelectedValue = 0 Then
			Return
		End If

		Dim sql As String = "SELECT COUNT(*) " & _
			"FROM tblRoutes AS R " & _
			"WHERE RouteID = " & ddlRouteUpdate.SelectedValue & " " & _
				"AND UncommittedChanges = 1"
		Dim cnt As Integer = SQLExecuteScalar(sql, "Notify User")

		If hfUncommittedChanges.Value = 1 And cnt = 0 Then
			lblMessage.Text = "You are processing Uncommitted Changes for " & ddlRoutes.SelectedItem.Text & ". " & _
					"You cannot move those addresses to a Route that is NOT processing Uncommitted Changes."
			Return
		End If
		If hfUncommittedChanges.Value = 0 And cnt = 1 Then
			lblMessage.Text = "You are NOT processing Uncommitted Changes for " & ddlRoutes.SelectedItem.Text & ". " & _
					"You cannot move those addresses to a Route that IS processing Uncommitted Changes."
			Return
		End If
		lblMessage.Text = ""

		sql = "SELECT S.SectionID, S.SectionCode " & _
			"FROM tblSections AS S "

		If hfUncommittedChanges.Value = 0 Then
			sql += "INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID "
		Else
			sql += "INNER JOIN tblRoutes AS R ON R.RouteID = S.UncommittedRouteID "
		End If
		sql += "WHERE R.RouteID = " & ddlRouteUpdate.SelectedValue & " "

		If Not ckActiveSections.Checked And Not ckInactiveSections.Checked Then
			ckActiveSections.Checked = True	' Don't let user uncheck both boxes
		End If

		If ckActiveSections.Checked And Not ckInactiveSections.Checked Then
			sql &= "AND S.Active = 1 "
		ElseIf Not ckActiveSections.Checked And ckInactiveSections.Checked Then
			sql &= "AND S.Active = 0 "
		End If

		sql &= "ORDER BY S.SectionCode"

		Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
		Dim dt As DataTable = New DataTable()
		da.Fill(dt)
		ddlSectionUpdate.Items.Clear()
		ddlSectionUpdate.DataSource = dt
		ddlSectionUpdate.DataTextField = "SectionCode"
		ddlSectionUpdate.DataValueField = "SectionID"
		ddlSectionUpdate.DataBind()
		ddlSectionUpdate.Items.Insert(0, New ListItem("<Select Section>", "0"))
	End Sub

	Sub DisplaySectionOptions()
		If ddlSections.Items.Count = 0 OrElse ddlSections.SelectedValue = 0 Then
			tblAddressStatuses.Style.Add("display", "none")
			tblCkNonRoute.Style.Add("display", "none")
			tblUpdate.Style.Add("display", "none")
			tblStatusesUpdate.Style.Add("display", "none")
			tblTemplateUpdate.Style.Add("display", "none")
			tblRouteUpdate.Style.Add("display", "none")
			tblSectionUpdate.Style.Add("display", "none")
			tblUpdateButtons.Style.Add("display", "none")
		Else
			tblAddressStatuses.Style.Add("display", "block")
			tblCkNonRoute.Style.Add("display", "block")
			If Session("MappingUpdate") = "Update" Then
				tblUpdate.Style.Add("display", "block")
				If rbUpdateList.SelectedIndex = 0 Then
					tblStatusesUpdate.Style.Add("display", "block")
					tblTemplateUpdate.Style.Add("display", "none")
					tblRouteUpdate.Style.Add("display", "none")
					tblSectionUpdate.Style.Add("display", "none")
				Else
					tblTemplateUpdate.Style.Add("display", "block")
					tblRouteUpdate.Style.Add("display", "block")
					tblSectionUpdate.Style.Add("display", "block")
					tblStatusesUpdate.Style.Add("display", "none")
				End If
				tblUpdateButtons.Style.Add("display", "block")
			End If
		End If
	End Sub

	Sub BuildRoutes(ByVal mapType As String)
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim concaveHull As String = ""
		Dim googleCoords As String = ""
		Dim routeCode As String = ""

		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		googleCoordinates.Value = ""
		sql = "SELECT DISTINCT R.RouteCode, RG.ConcaveHull.ToString() AS ConcaveHull " & _
			"FROM tblRoutes AS R " & _
			"INNER JOIN tblRoutesGeography AS RG ON RG.RouteID = R.RouteID "

		If mapType <> "City" Then
			sql += "INNER JOIN tblPickupCycleTemplatesDetail AS PCTD ON PCTD.RouteID = R.RouteID " & _
				"INNER JOIN tblPickupCycleTemplates AS PCT ON PCT.PickupCycleTemplateID = PCTD.PickupCycleTemplateID "
			If mapType = "Region" Then
				sql += "INNER JOIN tblPickupCycles AS PC ON PC.PickupCycleTemplateID = PCT.PickupCycleTemplateID " & _
					"INNER JOIN tblPickupCycleDriverLocations AS PCDL ON PCDL.PickupCycleID = PC.PickupCycleID "
			End If
		Else
			sql += "INNER JOIN tlkAllRoutesInCities AS DCR ON DCR.RouteID = R.RouteID "
		End If

		If mapType <> "City" Then
			sql += "WHERE PCT.Active = 1 "
			If mapType = "Region" Then
				sql += "AND PC.Active = 1 " & _
					"AND PCDL.PrimaryRegion = 1 " & _
					"AND PCDL.RegionID = " & ddlRegion.SelectedValue & " "
			Else
				sql += "AND PCT.PickupCycleTemplateID = " & ddlTemplates.SelectedValue & " "
			End If
		Else
			sql += "WHERE DCR.City = '" & ddlCity.SelectedValue & "' "
		End If

		sql += "AND ConcaveHull.ToString() <> 'GEOMETRYCOLLECTION EMPTY'"

		Try
			If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
				Return
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			ja(ex.Message)
			Return
		End Try

		Dim i As Integer = 0
		While rsql.Read()
			routeCode = rsql("RouteCode")
			concaveHull = rsql("ConcaveHull")

			If googleCoords <> "" Then
				googleCoords += ", "
			End If
			googleCoords += ConvertPolygonToGoogleCoords(routeCode, "", concaveHull, i)
			i += 1
		End While

		googleCoordinates.Value = "[ " & googleCoords & " ]"
		SqlQueryClose(connSQL, rsql)
	End Sub

	Sub BuildSections()
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim concaveHull As String = ""
		Dim googleCoords As String = ""
		Dim sectionCode As String = ""
		Dim routeCode As String = ""

		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim routeID As Integer = ddlRoutes.SelectedValue
		Dim sectionID As Integer = ddlSections.SelectedValue
		Dim showActiveSections As Boolean = ckActiveSections.Checked
		Dim showInactiveSections As Boolean = ckInactiveSections.Checked

		googleCoordinates.Value = ""
		If routeID > 0 Or sectionID > 0 Then
			sql = "SELECT R.RouteCode, S.SectionCode, SG.ConcaveHull.ToString() AS ConcaveHull " & _
				"FROM tblSections AS S " & _
				"INNER JOIN tblSectionsGeography AS SG ON SG.SectionID = S.SectionID "

			If hfUncommittedChanges.Value = 0 Then
				sql += "INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID "
			Else
				sql += "INNER JOIN tblRoutes AS R ON R.RouteID = S.UncommittedRouteID "
			End If

			If sectionID > 0 Then
				sql += "WHERE S.SectionID = " & sectionID & " "
			Else
				sql += "WHERE R.RouteID = " & routeID & " "
				If showActiveSections And Not showInactiveSections Then
					sql += "AND S.Active = 1 "
				End If
				If Not showActiveSections And showInactiveSections Then
					sql += "AND S.Active = 0 "
				End If
				If Not showActiveSections And Not showInactiveSections Then
					sql += "AND S.Active IS NULL "
				End If
			End If

			sql += "AND SG.ConcaveHull.ToString() <> 'GEOMETRYCOLLECTION EMPTY'"

			Try
				If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
					Return
				End If
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
				ja(ex.Message)
				Return
			End Try
			While rsql.Read()
				concaveHull = rsql("ConcaveHull")
				sectionCode = rsql("SectionCode")
				routeCode = rsql("RouteCode")

				If googleCoords <> "" Then
					googleCoords += ", "
				End If
				googleCoords += ConvertPolygonToGoogleCoords(routeCode, sectionCode, concaveHull, 0)
			End While

			googleCoordinates.Value = "[ " & googleCoords & " ]"
			SqlQueryClose(connSQL, rsql)
		End If
	End Sub

	Sub BuildNearbySections()
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim rsql As SqlDataReader = Nothing
		Dim nearbySectionsCoords As String = ""
		Dim concaveHull As String = ""
		Dim sectionCode As String = ""
		Dim routeCode As String = ""

		nearbySectionsCoordinates.Value = ""
		If ddlSections.SelectedValue = 0 Then
			Return	' No nearby Sections unless one section is selected
		End If

		Dim parms As List(Of QueryParms) = New List(Of QueryParms)
		Dim p As QueryParms = New QueryParms
		p.parmName = "@sectionID"
		p.dbType = SqlDbType.Int
		p.value = ddlSections.SelectedValue
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@bufferSize"
		p.dbType = SqlDbType.Float
		p.value = Int(CDbl(txtNearbyDistance.Text) * 1609.344)
		parms.Add(p)

		Try
			rsql = GetReader(connSQL, vConnStr, "spNearbySections", parms)
			If IsNothing(rsql) Then
				Return
			End If
			While rsql.Read()
				concaveHull = rsql("ConcaveHull")
				sectionCode = rsql("SectionCode")
				routeCode = rsql("RouteCode")

				If nearbySectionsCoords <> "" Then
					nearbySectionsCoords += ", "
				End If

				nearbySectionsCoords += ConvertPolygonToGoogleCoords(routeCode, sectionCode, concaveHull, 0)
			End While
			If nearbySectionsCoords <> "" Then
				nearbySectionsCoordinates.Value = "[ " & nearbySectionsCoords & " ]"
			Else
				nearbySectionsCoordinates.Value = ""
			End If
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try

	End Sub

	Public sectionAddressesCoords As String = ""

	Sub BuildSectionAddresses(ByVal ddlSectionsIndex As Integer, ByVal firstSection As Boolean)
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim rsql As SqlDataReader = Nothing

		If firstSection Then
			sectionAddressesCoords = ""
			sectionAddressesCoordinates.Value = ""
		End If

		Dim color As String = ""
		color = SectionColor(ddlSections.Items(ddlSectionsIndex).Text, 0)

		Dim uc As Boolean = False
		If hfUncommittedChanges.Value = 1 Then
			uc = True
		End If

		Dim parms As List(Of QueryParms) = New List(Of QueryParms)
		Dim p As QueryParms = New QueryParms
		p.parmName = "@sectionID"
		p.dbType = SqlDbType.Int
		p.value = ddlSections.Items(ddlSectionsIndex).Value
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@uncommittedChanges"
		p.dbType = SqlDbType.Bit
		p.value = uc
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@statuses"
		p.dbType = SqlDbType.VarChar
		p.value = rbAddressStatuses.Value
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@addressTypes"
		p.dbType = SqlDbType.Int
		p.value = 2
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@bufferSize"
		p.dbType = SqlDbType.Int
		p.value = 0
		parms.Add(p)
		p = New QueryParms

		Try
			rsql = GetReader(connSQL, vConnStr, "spMappingAddresses", parms)
			If IsNothing(rsql) Then
				Return
			End If
			While rsql.Read()
				If sectionAddressesCoords <> "" Then
					sectionAddressesCoords += ", "
				End If

				sectionAddressesCoords += "[" & rsql("Lat") & "," & rsql("Long") & ",""" & color & """]"
				'sectionAddressesCoords += "[" & rsql("Lat") & "," & rsql("Long") & ",""" & rsql("StreetAddress") & " (" &
				'	rsql("Status") & ")"",""" & rsql("Status") & """,""" & rsql("AddressID") & """,""" & color & """]"
			End While
			sectionAddressesCoordinates.Value = "[ " & sectionAddressesCoords & " ]"
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try
	End Sub

	Sub BuildNonRouteAddressMarkers()
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim rsql As SqlDataReader = Nothing
		Dim nonRouteCoords As String = ""

		nonRouteCoordinates.Value = ""

		' I think we always want this to occur.
		'If Not ckNonRouteAddressesNearby.Checked And Not ckNonRouteAddressesZip.Checked Then
		'	Return
		'End If

		Dim uc As Boolean = False
		If hfUncommittedChanges.Value = 1 Then
			uc = True
		End If

		Dim parms As List(Of QueryParms) = New List(Of QueryParms)
		Dim p As QueryParms = New QueryParms
		p.parmName = "@sectionID"
		p.dbType = SqlDbType.Int
		p.value = ddlSections.SelectedValue
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@uncommittedChanges"
		p.dbType = SqlDbType.Bit
		p.value = uc
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@statuses"
		p.dbType = SqlDbType.VarChar
		p.value = rbAddressStatuses.Value
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@addressTypes"
		p.dbType = SqlDbType.Int
		p.value = 3
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@bufferSize"
		p.dbType = SqlDbType.Float
		'If ckNonRouteAddressesNearby.Checked Then
		p.value = Int(CDbl(txtNearbyDistance.Text) * 1609.344)
		'Else
		'p.value = 0	' By Zip
		'End If
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@selectedStatus"
		p.dbType = SqlDbType.Int
		p.value = ddlStatusesUpdate.SelectedValue
		parms.Add(p)

		Try
			rsql = GetReader(connSQL, vConnStr, "spMappingAddresses", parms)
			If IsNothing(rsql) Then
				Return
			End If
			While rsql.Read()
				If nonRouteCoords <> "" Then
					nonRouteCoords += ", "
				End If

				nonRouteCoords += "[" & rsql("Lat") & "," & rsql("Long") & ",""" & rsql("StreetAddress") & " (" &
					rsql("Status") & ")"",""" & rsql("StatusID") & """,""" & rsql("AddressID") & """]"
			End While
			nonRouteCoordinates.Value = "[ " & nonRouteCoords & " ]"
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try
	End Sub

	Sub BuildCarrierRoutes()
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim crrt As String = ""
		Dim wkt As String = ""
		Dim carrierRouteCoords As String = ""

		carrierRouteCoordinates.Value = ""

		Dim parms As List(Of QueryParms) = New List(Of QueryParms)
		Dim p As QueryParms = New QueryParms
		p.parmName = "@sectionID"
		p.dbType = SqlDbType.Int
		p.value = ddlSections.SelectedValue
		parms.Add(p)
		p = New QueryParms
		p.parmName = "@bufferSize"
		p.dbType = SqlDbType.Float
		p.value = Int(CDbl(txtNearbyDistance.Text) * 1609.344)
		parms.Add(p)

		Try
			rsql = GetReader(connSQL, vConnStr, "spMappingCarrierRoutes", parms)
			If IsNothing(rsql) Then
				Return
			End If
			While rsql.Read()
				crrt = rsql("crrt")
				wkt = rsql("wkt")

				If carrierRouteCoords <> "" Then
					carrierRouteCoords += ", "
				End If
				carrierRouteCoords += ConvertPolygonToGoogleCoords(crrt, "crrt", wkt, 0)
			End While
			carrierRouteCoordinates.Value = "[ " & carrierRouteCoords & " ]"
			SqlQueryClose(connSQL, rsql)
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		End Try
	End Sub

	Public Function ConvertPolygonToGoogleCoords(ByVal routeCode As String, _
								ByVal sectionCode As String, ByVal polygon As String, i As Integer) As String
		'Convert POLYGON ((Long[0] Lat[0], Long[1] Lat[1], Long[n] Lat[n])) 
		'   to [[Route-Section, color], [Lat[0], [Long[0]], [Lat[1], [Long[1]], [Lat[n], [Long[n]]]
		'POLYGON ((-121.42 38.4206, -121.42 38.4207, -121.42 38.4213, -121.42 38.4228, -121.432 38.4299, -121.433 38.4302, -121.443 38.431399999999996, -121.446 38.4302, -121.447 38.4274, -121.442 38.4169, -121.441 38.4165, -121.437 38.4165, -121.436 38.4166, -121.428 38.4183, -121.42 38.4206))
		'Convert MULTIPOLYGON (((Long[m0] Lat[m0], Long[m1] Lat[m1], Long[mn] Lat[mn])),((Long[n0] Lat[n0], Long[n1] Lat[n1], Long[nn] Lat[nn])) 
		'   to [[Route-Section, color], [Lat[m0], [Long[m0]], [Lat[m1], [Long[m1]], [Lat[mn], [Long[mn]], [[Route-Section, color], [Lat[n0], [Long[n0]], [Lat[n1], [Long[n1]], [Lat[nn], [Long[nn]]]
		'MULTIPOLYGON (((-121.447 38.4129, -121.446 38.4129, -121.445 38.4129, -121.444 38.4129, -121.443 38.4129, -121.443 38.413, -121.443 38.4132, -121.443 38.4133, -121.443 38.4135, -121.443 38.4136, -121.443 38.4138, -121.443 38.4139, -121.443 38.4141, -121.443 38.4142, -121.443 38.4143, -121.443 38.4145, -121.443 38.4146, -121.443 38.4148, -121.443 38.4149, -121.443 38.4151, -121.443 38.4152, -121.442 38.4166, -121.442 38.4167, -121.442 38.4169, -121.442 38.4172, -121.442 38.4178, -121.442 38.4197, -121.441 38.4205, -121.443 38.4199, -121.444 38.4213, -121.444 38.4214, -121.444 38.4215, -121.444 38.4232, -121.445 38.4213, -121.447 38.4212, -121.448 38.421, -121.448 38.4209, -121.448 38.4208, -121.448 38.4206, -121.448 38.4205, -121.448 38.4204, -121.448 38.4197, -121.448 38.4187, -121.448 38.4186, -121.448 38.4185, -121.448 38.4183, -121.448 38.4182, -121.448 38.4181, -121.448 38.4178, -121.448 38.4174, -121.448 38.4173, -121.448 38.4171, -121.448 38.4167, -121.448 38.4158, -121.449 38.4147, -121.449 38.4139, -121.448 38.4137, -121.447 38.4129)), ((-121.436 38.4094, -121.435 38.41, -121.433 38.4117, -121.432 38.4104, -121.431 38.4098, -121.429 38.4108, -121.428 38.4108, -121.427 38.4107, -121.425 38.4123, -121.424 38.4123, -121.423 38.4106, -121.422 38.4106, -121.421 38.4106, -121.419 38.4113, -121.418 38.4109, -121.418 38.411, -121.418 38.4113, -121.418 38.412, -121.418 38.4121, -121.418 38.4122, -121.418 38.4123, -121.418 38.4125, -121.418 38.4128, -121.418 38.413, -121.418 38.4132, -121.418 38.4133, -121.418 38.4134, -121.418 38.4135, -121.418 38.4136, -121.418 38.4137, -121.418 38.4138, -121.418 38.414, -121.418 38.4149, -121.418 38.415, -121.418 38.4151, -121.418 38.4158, -121.419 38.4172, -121.419 38.4173, -121.419 38.4174, -121.419 38.4175, -121.419 38.4176, -121.419 38.4177, -121.419 38.4178, -121.419 38.4179, -121.419 38.418, -121.419 38.4181, -121.419 38.4182, -121.419 38.4183, -121.418 38.4197, -121.419 38.4197, -121.42 38.4197, -121.421 38.4199, -121.422 38.42, -121.423 38.4199, -121.424 38.4193, -121.426 38.418, -121.428 38.4174, -121.429 38.4174, -121.43 38.4174, -121.431 38.4173, -121.432 38.4173, -121.434 38.4168, -121.435 38.418, -121.435 38.4179, -121.435 38.4178, -121.435 38.4177, -121.435 38.4176, -121.436 38.4157, -121.436 38.4156, -121.436 38.4153, -121.436 38.4151, -121.436 38.415, -121.436 38.4148, -121.436 38.4147, -121.436 38.4145, -121.436 38.4135, -121.436 38.4134, -121.436 38.4133, -121.436 38.4132, -121.436 38.4131, -121.436 38.4129, -121.436 38.4128, -121.436 38.4127, -121.436 38.4125, -121.436 38.4123, -121.436 38.4121, -121.436 38.412, -121.436 38.4118, -121.436 38.4116, -121.436 38.4115, -121.436 38.4113, -121.436 38.4105, -121.436 38.4104, -121.436 38.4103, -121.436 38.4102, -121.436 38.4101, -121.436 38.41, -121.436 38.4098, -121.436 38.4094)))
		Dim s As String = ""
		Dim sqlPoints() As String
		Dim sqlPoly() As String
		Dim longLat() As String
		Dim polyCoords As String = ""
		Dim googleCoords As String = ""

		Dim color As String = ""
		color = SectionColor(sectionCode, i)

		s = polygon.Replace("MULTIPOLYGON (((", "")
		s = s.Replace("POLYGON ((", "")
		sqlPoly = Split(s, ")),")
		For Each poly As String In sqlPoly
			s = poly.Replace("(", "")
			s = s.Replace(")", "")
			sqlPoints = Split(s, ",")

			If sectionCode = "" Or sectionCode = "crrt" Then
				polyCoords = "[""" & routeCode & """, """ & color & """]"
			Else
				polyCoords = "[""" & routeCode & "-" & sectionCode & """, """ & color & """]"
			End If
			For Each point As String In sqlPoints
				longLat = Split(LTrim(point))
				polyCoords += ", [" & longLat(1) & ", " & longLat(0) & "]"
			Next
			If googleCoords <> "" Then
				googleCoords += ", "
			End If
			googleCoords += "[" & polyCoords & "]"
		Next

		Return googleCoords
	End Function

	Public Function SectionColor(ByVal SectionCode As String, ByVal i As Integer) As String
		Dim color As String = ""
		Dim colorNum As String = ""

		If SectionCode = "" Or SectionCode = "crrt" Then
			colorNum = (i Mod 6) + 1
		Else
			Dim c As Integer = NumericOnly(SectionCode)
			If c = 99 Then
				colorNum = 99
			Else
				colorNum = (c Mod 6)
			End If
		End If

		Select Case colorNum
			Case "1"
				color = "orange"
			Case "2"
				color = "green"
			Case "3"
				color = "red"
			Case "4"
				color = "blue"
			Case "5"
				color = "purple"
			Case "0" ' 6
				color = "yellow"
			Case Else
				color = "grey"
		End Select

		Return color
	End Function

	Sub BuildMapTitle()
		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim routeCode As String = ""

		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim routeID As Integer = ddlRoutes.SelectedValue
		Dim title As String = ""

		If routeID > 0 Then
			sql = "SELECT R.RouteCode, CL.LastChangeDate, CL.NextPickupDate " & _
				"FROM tblRoutes AS R " & _
				"LEFT OUTER JOIN tblRouteChangeLog AS CL ON CL.RouteID = R.RouteID " & _
				"WHERE R.RouteID = " & routeID

			Try
				If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
					Return
				End If
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
				ja(ex.Message)
				Return
			End Try
			If rsql.Read() Then
				title = "<div><big><b>" & rsql("RouteCode").ToString()
				Dim nextPickupDate As String = rsql("NextPickupDate").ToString()
				If nextPickupDate <> "" Then
					title &= " (Effective: " & Format(CDate(nextPickupDate), "MM/dd/yyyy") & ")"
				Else
					Dim lastChangeDate As String = rsql("LastChangeDate").ToString()
					If lastChangeDate <> "" Then
						title &= " (Last Changed: " & Format(CDate(lastChangeDate), "MM/dd/yyyy") & ")"
					End If
				End If
				title &= "</b></big></div>"
				hfMapTitle.Value = title
			End If
		End If
	End Sub

	Protected Sub btnDrawMap_Click(sender As Object, e As EventArgs) Handles btnDrawMap.Click
		If ddlMapType.SelectedValue = 0 Then
			ja("Please select a Map Type")
			Return
		End If
		nearbySectionsCoordinates.Value = ""
		sectionAddressesCoordinates.Value = ""
		nonRouteCoordinates.Value = ""
		hfMapTitle.Value = ""
		If ddlMapType.SelectedValue = 1 Then
			If ddlRegion.SelectedValue = 0 Then
				ja("Please select a Region")
				Return
			End If
			BuildRoutes("Region")
		End If
		If ddlMapType.SelectedValue = 2 Then
			If ddlCity.SelectedValue = "0" Then
				ja("Please select a City")
				Return
			End If
			BuildRoutes("City")
		End If
		If ddlMapType.SelectedValue = 3 Then
			If ddlTemplates.SelectedValue = "0" Then
				ja("Please select a Template")
				Return
			End If
			If ddlRoutes.Items.Count = 0 OrElse ddlRoutes.SelectedValue = "0" Then
				BuildRoutes("Template")
			ElseIf ddlRoutes.SelectedValue > "0" And ddlSections.SelectedValue = "0" Then
				BuildSections()
				For i As Integer = 1 To ddlSections.Items.Count - 1
					Dim firstSection As Boolean = True
					If i > 1 Then
						firstSection = False
					End If
					BuildSectionAddresses(i, firstSection)
				Next
				BuildMapTitle()
			Else
				BuildSections()
				BuildNearbySections()
				If ddlSections.SelectedValue > 0 Then
					BuildSectionAddresses(ddlSections.SelectedIndex, True)
				End If
				BuildNonRouteAddressMarkers()
				BuildCarrierRoutes()
			End If
		End If
	End Sub

	Protected Sub NearbyDistance_Validation(ByVal sender As Object, ByVal e As ValidationEventArgs)
		If CommonUtils.IsNullValue(e.Value) OrElse (CStr(e.Value) = "") Then
			Return
		End If
		Dim strNearbyDistance As String = (CStr(e.Value)).TrimStart("0"c)
		If strNearbyDistance.Length = 0 Then
			Return
		End If
		Dim distance As Decimal = 0
		If (Not Decimal.TryParse(strNearbyDistance, distance)) OrElse strNearbyDistance > 5 Then
			e.IsValid = False
		End If
	End Sub

	<WebMethod()> _
	Public Shared Function UpdateStatuses(ByVal statusID As Integer, ByVal addressIDs As String) As Integer
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim sql As String = "UPDATE tblAddresses " & _
			"SET StatusID = " & statusID & " " & _
			"WHERE AddressID IN (" & addressIDs & ")"

		Dim retValue As Integer = SqlNonQuery(sql)
		If retValue >= 0 Then
			Return statusID
		End If
		Return -1	'error
	End Function

	<WebMethod()> _
	Public Shared Function UpdateSection(ByVal sectionID As Integer, ByVal addressIDs As String, ByVal userID As Integer, ByVal uc As Boolean) As Integer
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spAddressSection_Update"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@sectionID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, sectionID))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@uncommittedChanges", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, uc))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@addressIDs", System.Data.ParameterDirection.Input, System.Data.DbType.String, addressIDs))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@updateStatusIDs", System.Data.ParameterDirection.Input, System.Data.DbType.String, 1))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@statusID", System.Data.ParameterDirection.Input, System.Data.DbType.String, 0))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, userID))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "Mapping, UpdateSection")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try

		If errorID = 0 Then
			Return sectionID
		End If
		Return -1	'error
	End Function

	Protected Sub cbMapSizes_SelectedIndexChanged(sender As Object, e As EventArgs)
		ResizeMap(cbMapSizes.Value)
	End Sub

	Private Sub ResizeMap(ByVal id As Integer)
		Dim pixelsPerInch As Double = 90.0
		Dim topMargin As Double = 0.75
		Dim sideMargin As Double = 0.75

		Select Case id
			Case 0
				map_canvas.Style.Add("width", "100%")
				map_canvas.Style.Add("height", "100%")
			Case 1
				map_canvas.Style.Add("width", ((8.5 - sideMargin) * pixelsPerInch) & "px")
				map_canvas.Style.Add("height", ((11.0 - topMargin) * pixelsPerInch) & "px")
			Case 2
				map_canvas.Style.Add("width", ((11.0 - sideMargin) * pixelsPerInch) & "px")
				map_canvas.Style.Add("height", ((18.0 - topMargin) * pixelsPerInch) & "px")
			Case 3
				map_canvas.Style.Add("width", ((11.0 - sideMargin) * pixelsPerInch) & "px")
				map_canvas.Style.Add("height", ((8.5 - topMargin) * pixelsPerInch) & "px")
			Case 4
				map_canvas.Style.Add("width", ((18.0 - sideMargin) * pixelsPerInch) & "px")
				map_canvas.Style.Add("height", ((11.0 - topMargin) * pixelsPerInch) & "px")
		End Select

		cbMapSizes.SelectedIndex = id
		hfMapSize.Value = id	 ' Need this for zoom level
	End Sub
  
End Class
