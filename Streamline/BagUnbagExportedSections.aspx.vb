Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Partial Class BagUnbagExportedSections
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                        Case Else
                            LoadGrid()
                    End Select
                End If


            Case "GET"
                hfSQL_ddlPickupCycles.Value =
                    "SELECT DISTINCT PC.PickupCycleID, PC.PickupCycleAbbr " & _
                    "FROM tblPickupCycles AS PC " & _
                    "INNER JOIN tblPickupCycleDriverLocations AS PCDL ON PCDL.PickupCycleID = PC.PickupCycleID " & _
                    "WHERE PCDL.RegionID IN " & Session("userRegionsList") & " " & _
                    "ORDER BY PC.PickupCycleAbbr"
                Dim da As SqlDataAdapter = New SqlDataAdapter(hfSQL_ddlPickupCycles.Value, vConnStr)
                Dim dt As DataTable = New DataTable()
                da.Fill(dt)
                ddlPickupCycles.DataSource = dt
                ddlPickupCycles.DataTextField = "PickupCycleAbbr"
                ddlPickupCycles.DataValueField = "PickupCycleID"
                ddlPickupCycles.DataBind()

                LoadGrid()
            Case "HEAD"
        End Select


    End Sub

    Private Sub LoadGrid()
        dsPickupScheduleSections.SelectCommand =
            "SELECT PSS.PickupScheduleID, S.SectionID, R.RouteCode + '-' + S.SectionCode AS RouteSection, " & _
                "CASE WHEN PSS.CntBag > 0 THEN 1 ELSE 0 END AS Bag " & _
            "FROM tblPickupScheduleSections AS PSS " & _
            "INNER JOIN tblPickupSchedule AS PS ON PS.PickupScheduleID = PSS.PickupScheduleID " & _
            "INNER JOIN tblSections AS S ON S.SectionID = PSS.SectionID " & _
            "INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID " & _
            "INNER JOIN tblScheduledPickupSectionsForExport AS SPSE ON SPSE.PickupScheduleID = PS.PickupScheduleID " & _
                "AND SPSE.SectionID = PSS.SectionID " & _
            "WHERE PS.PickupCycleID = " & ddlPickupCycles.SelectedValue & " " & _
                "AND PS.PickupDate = '" & Format(dtPickupDate.Value, "yyyyMMdd") & "' " & _
                "AND SPSE.Exported = 1"

        grid.DataBind()
    End Sub
End Class
