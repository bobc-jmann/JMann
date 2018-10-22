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

Partial Class SpecialAssignments
    Inherits System.Web.UI.Page

    Public googleCoords As String = ""
    Public specialAddressesCoords As String = ""

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

                    Dim parameter As String = Request("__EVENTARGUMENT")
                    Dim selectedDriverID As Integer = 0

                    If parameter = "Draw" Then
                        Dim a = drivers.GetSelectedFieldValues("DriverID")
                        selectedDriverID = a(0)
                        hf("selectedDriverID") = selectedDriverID
                        DrawMap(selectedDriverID)
                        LoadDrivers()
                    End If

                    Select Case PostBackControlID

                    End Select

                End If

            Case "GET"
                dtPickupDate.Value = DateAdd(DateInterval.Day, -1, Now)

                Dim sql As String = "SELECT RegionID, RegionDesc FROM tlkRegions WHERE RegionID IN " & Session("userRegionsList") & " ORDER BY RegionDesc"
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

            Case "HEAD"
                'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
        End Select

    End Sub

    Protected Sub ddlDriverLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDriverLocations.SelectedIndexChanged
        LoadDrivers()
        DrawMap(-1)
    End Sub
    Protected Sub dtPickupDate_DateChanged(sender As Object, e As EventArgs) Handles dtPickupDate.DateChanged
        LoadDrivers()
        DrawMap(-1)
    End Sub

    Protected Sub drivers_CustomCallback1(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        Dim selectedDriverID As Integer = drivers.GetRowValues(e.Parameters, "DriverID")
        drivers.Selection.UnselectAll()
        'Hidden fields do not work, so we select the row that was clicked.
        drivers.Selection.SelectRow(e.Parameters)
        'DrawMap(selectedDriverID)
    End Sub

    Public Sub LoadDrivers()
        Dim sql As String = "SELECT D.DriverID, D.DriverName, DA.CntSections, SP.CntSpecials " &
            "FROM tblDrivers AS D " &
            "OUTER APPLY (SELECT COUNT(*) AS CntSections " &
                "FROM tblDriverAssignments AS DA " &
                "WHERE DA.DriverID = D.DriverID " &
                    "AND DA.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " &
                    "AND DA.SectionID > 0) AS DA " &
            "OUTER APPLY (SELECT COUNT(*) AS CntSpecials " &
                "FROM tblSpecials AS SP " &
                "WHERE SP.DriverID = D.DriverID " &
                    "AND SP.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "') AS SP " &
            "WHERE DriverLocationID = " & ddlDriverLocations.SelectedValue & " " &
                "AND D.Active = 1 " &
            "ORDER BY DriverName"

        dsDrivers.SelectCommand = sql
        drivers.DataBind()

        'Get total number of Specials
        Dim rs As SqlDataReader = Nothing
        Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

        sql = "SELECT COUNT(*) AS Cnt " &
            "FROM tblSpecials " &
            "WHERE DriverLocationID = " & ddlDriverLocations.SelectedValue & " " &
                "AND PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' "

        If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
            Return
        End If
        rs.Read()
        totalSpecials.Text = "Total Specials: " & rs("Cnt")
        SqlQueryClose(connSQL, rs)

        'Get number of unassigned Specials
        Dim uSpecials As Integer = 0
        sql = "SELECT COUNT(*) AS Cnt " &
            "FROM tblSpecials AS SP " &
            "WHERE SP.DriverLocationID = " & ddlDriverLocations.SelectedValue & " " &
                "AND SP.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " &
                "AND SP.DriverID = 0"

        If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
            Return
        End If
        rs.Read()
        uSpecials = CInt(rs("Cnt"))
        If uSpecials > 0 Then
            unassignedSpecials.Text = "Unassigned Specials: " & rs("Cnt")
            unassignedSpecials.Visible = True
        Else
            unassignedSpecials.Visible = False
        End If
        SqlQueryClose(connSQL, rs)

    End Sub

    Sub BuildSections(ByVal selectedDriverID As Integer)
        If selectedDriverID <= 0 Then
            Return
        End If

        Dim sql As String = ""
        Dim rsql As SqlDataReader = Nothing
        Dim concaveHull As String = ""
        Dim sectionCode As String = ""
        Dim routeCode As String = ""
        Dim driverID As Integer = 0

        Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

        sql = "SELECT R.RouteCode, DA.SectionCode, DA.DriverID, SG.ConcaveHull.ToString() AS ConcaveHull " &
            "FROM tblDriverAssignments AS DA " &
            "INNER JOIN tblSectionsGeography AS SG ON SG.SectionID = DA.SectionID " &
            "INNER JOIN tblRoutes AS R ON R.RouteID = DA.RouteID " &
            "WHERE DA.PickupDate = '" & Format(dtPickupDate.Value, "MM/dd/yyyy") & "' " &
                "AND DA.LocationID = " & ddlDriverLocations.SelectedValue & " " &
                "AND DA.SectionID > 0 " &
                "AND SG.ConcaveHull.ToString() <> 'GEOMETRYCOLLECTION EMPTY'"

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
            driverID = rsql("DriverID")

            If googleCoords <> "" Then
                googleCoords += ", "
            End If
            googleCoords += ConvertPolygonToGoogleCoords(selectedDriverID, routeCode, sectionCode, driverID, concaveHull, 0)
        End While
        SqlQueryClose(connSQL, rsql)
    End Sub


    Sub BuildSpecialsAddresses(ByVal selectedDriverID As Integer)
        If selectedDriverID <= 0 Then
            Return
        End If

        Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
        Dim rsql As SqlDataReader = Nothing

        'Get Selected DriverName
        Dim rs As SqlDataReader = Nothing

        Dim sql As String = "SELECT DriverName " &
            "FROM tblDrivers " &
            "WHERE DriverID = " & selectedDriverID

        If Not SqlQueryOpen(connSQL, rs, sql, "Notify Web User") Then
            Return
        End If
        rs.Read()
        Dim selectedDriverName As String = rs("DriverName")
        SqlQueryClose(connSQL, rs)


        Dim parms As List(Of QueryParms) = New List(Of QueryParms)
        Dim p As QueryParms = New QueryParms
        p.parmName = "@regionID"
        p.dbType = SqlDbType.Int
        p.value = ddlDriverLocations.SelectedValue
        parms.Add(p)
        p = New QueryParms
        p.parmName = "@pickupDate"
        p.dbType = SqlDbType.Date
        p.value = dtPickupDate.Value
        parms.Add(p)

        Try
            rsql = GetReader(connSQL, vConnStr, "spMappingAddressesSpecials", parms)
            If IsNothing(rsql) Then
                Return
            End If
            While rsql.Read()
                If specialAddressesCoords <> "" Then
                    specialAddressesCoords += ", "
                End If

                Dim label As String = rsql("StreetAddress")
                If rsql("DriverName") <> "" Then
                    label += " (" & rsql("DriverName") & ")"
                End If

                Dim image As String = ""
                Dim driverID As Integer = CInt(rsql("DriverID"))
                Select Case driverID
                    Case -1
                        image = "building-land.png"
                    Case 0
                        image = "ylw-pushpin.png"
                    Case Else
                        If driverID = selectedDriverID Then
                            image = "green-dot.png"
                        Else
                            image = "ltblue-dot.png"
                        End If
                End Select

                specialAddressesCoords += "[" & rsql("Lat") & "," & rsql("Long") & ",""" &
                    selectedDriverName & """,""" & label & """,""" & image & """," & rsql("AddressID") & "]"
            End While
            SqlQueryClose(connSQL, rsql)
        Catch ex As Exception
            LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
        End Try
    End Sub


    Public Function ConvertPolygonToGoogleCoords(ByVal selectedDriverID As Integer, ByVal routeCode As String,
            ByVal sectionCode As String, ByVal driverID As Integer, ByVal polygon As String, i As Integer) As String
        Dim s As String = ""
        Dim sqlPoints() As String
        Dim sqlPoly() As String
        Dim longLat() As String
        Dim polyCoords As String = ""
        Dim googleCoords As String = ""

        Dim color As String = ""
        color = SectionColor(selectedDriverID, sectionCode, driverID, i)

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

    Public Function SectionColor(ByVal selectedDriverID As Integer, ByVal SectionCode As String,
                                                ByVal driverID As Integer, ByVal i As Integer) As String
        Dim color As String = ""
        Dim colorNum As String = ""

        If SectionCode = "" Then
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

        If selectedDriverID <> driverID Then
            color = "grey"
        End If

        Return color
    End Function

    Public Sub DrawMap(ByVal selectedDriverID As Integer)
        If selectedDriverID = 0 Then
            Return 'No driver selected
        End If

        googleCoords = ""
        googleCoordinates.Value = ""
        specialAddressesCoords = ""
        specialAddressesCoordinates.Value = ""

        'Selected driver's sections should have their proper color.
        'Other sections in the Region should be gray.
        BuildSections(selectedDriverID)
        'Selected driver's Specials should have a marker green-dot.
        'Specials not assigned should be have a marker ylw-pushpin.
        'Specials assigned to someone else should have marker ltblue-dot.
        'Selected Specials should have marker red.
        'All markers should have address & current driver initials.
        BuildSpecialsAddresses(selectedDriverID)


        googleCoordinates.Value = "[ " & googleCoords & " ]"
        specialAddressesCoordinates.Value = "[ " & specialAddressesCoords & " ]"

    End Sub

    <WebMethod()>
    Public Shared Function UpdateSpecials(ByVal selectedDriverID As Integer, ByVal addressIDs As String) As Integer
        Dim conn As SqlConnection = New SqlConnection(vConnStr)

        Dim sql As String = "UPDATE tblSpecials " &
            "SET DriverID = " & selectedDriverID & " " &
            "WHERE AddressID IN (" & addressIDs & ")"

        Dim retValue As Integer = SqlNonQuery(sql)
        If retValue >= 0 Then
            Return 0
        End If
        Return -1   'error
    End Function

End Class
