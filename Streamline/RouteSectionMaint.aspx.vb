Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports DataUtil

Partial Class RouteSectionMaint
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
						Case "ckShowOnlyActiveRoutes"
							If ckShowOnlyActiveRoutes.Checked Then
								ckShowOnlyActiveRoutes.Checked = False
							End If
						Case "ckShowMagRoutes"
							If ckShowMagRoutes.Checked Then
								ckShowMapRoutes.Checked = False
							End If
						Case "ckShowMapRoutes"
							If ckShowMapRoutes.Checked Then
								ckShowMagRoutes.Checked = False
							End If
                        Case "ckShowOnlyCurrentSections"
                            If ckShowOnlyCurrentSections.Checked Then
                                ckShowOnlyCurrentSections.Checked = False
                            End If
                        Case Else
                    End Select
                    LoadRoutes()
                End If
            Case "GET"
				ckShowOnlyActiveRoutes.Checked = True
				ckShowMagRoutes.Checked = False
                ckShowMapRoutes.Checked = False
                ckShowOnlyCurrentSections.Checked = True
                LoadRoutes()
        End Select
    End Sub

    Private Sub LoadRoutes()
		Dim sql As String = "SELECT R.Active, R.RouteID, R.RouteCode, R.RouteDesc, " & _
				"R.Notes, R.UncommittedChanges, R.MapLastUpdated " & _
			"FROM tblRoutes R WHERE 1 = 1 "

		If ckShowOnlyActiveRoutes.Checked Then
			sql += "AND R.Active = 1 "
		End If

		If ckShowMagRoutes.Checked Then
			sql += "AND EXISTS (SELECT * FROM tblSections S WHERE S.RouteID = R.RouteID AND S.MAG = 1 "
			If ckShowOnlyCurrentSections.Checked Then
				sql += " AND S.Active = 1) "
			Else
				sql += ") "
			End If
		ElseIf ckShowMapRoutes.Checked Then
			sql += "AND EXISTS (SELECT * FROM tblSections S WHERE S.RouteID = R.RouteID AND S.MAP = 1 "
			If ckShowOnlyCurrentSections.Checked Then
				sql += " AND S.Active = 1) "
			Else
				sql += ") "
			End If
		End If

		sql += " ORDER BY R.RouteCode"
		dsRoutes.SelectCommand = sql
		grdRoutes.DataBind()

		If ckShowOnlyCurrentSections.Checked Then
			Session("ShowOnlyCurrentSections") = True
		Else
			Session("ShowOnlyCurrentSections") = False
		End If

		If Not IsNothing(grdSections) Then
			grdSections.DataBind()
		End If

	End Sub

    Protected Sub grdRoutes_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("RouteID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

	Protected Sub grdRoutes_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)
		e.NewValues("Active") = True
	End Sub

	Protected Sub grdSections_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)
		e.NewValues("QualityRating") = "High"
		e.NewValues("Active") = True
	End Sub

	Protected Sub gridRoutes_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grdRoutes.RowValidating
		If e.NewValues("RouteCode") = "" Then
			e.RowError = "Route Code cannot be blank."
			Return
		End If

	End Sub

	Protected Sub grdSections_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		If Len(e.NewValues("SectionCode")) > 4 Then
			e.RowError = "Section Code cannot be longer than four characters."
			Return
		End If
		If e.NewValues("SectionCode") = "" Then
			e.RowError = "Section Code cannot be blank."
			Return
		End If
		If e.NewValues("MAG") And e.NewValues("MAP") Then
			e.RowError = "Section cannot be MAG and MAP at the same time."
			Return
		End If
		If Not IsNothing(e.NewValues("alpha")) And (e.NewValues("alpha") <= 0 Or e.NewValues("alpha") >= 0.1) Then
			e.RowError = "Alpha must be greater than zero and less than 0.1."
			Return
		End If

		Dim sql As String = ""
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		Dim cnt As Integer = 0

		Dim sectionID As Integer = 0
		If Not IsNothing(e.Keys.Values(0)) Then
			sectionID = e.Keys.Values(0)
		End If

		sql = "SELECT COUNT(*) AS Cnt " & _
			"FROM tblSections AS S " & _
			"INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID " & _
			"WHERE R.RouteID = " & Session("RouteID") & " " & _
				"AND SectionCode = '" & e.NewValues("SectionCode") & "' "

		If sectionID > 0 Then
			sql += "AND SectionID <> " & sectionID
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
			e.RowError = "This Section Code is a duplicate of an already existing Section Code for this Route."
			Return
		End If

		If e.NewValues("MAG") Then
			sql = "SELECT COUNT(*) AS Cnt " & _
				"FROM tblSections AS S " & _
				"WHERE S.RouteID = " & Session("RouteID") & " " & _
					"AND MAP = 1"

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
				e.RowError = "Cannot make a MAG Section in a Route that has a MAP Section. Reset All Sections first."
				Return
			End If
		End If

		If e.NewValues("MAP") Then
			sql = "SELECT COUNT(*) AS Cnt " & _
				"FROM tblSections AS S " & _
				"WHERE S.RouteID = " & Session("RouteID") & " " & _
					"AND MAG = 1"

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
				e.RowError = "Cannot make a MAP Section in a Route that has a MAG Section. Reset All Sections first."
				Return
			End If
		End If

		If e.IsNewRow Then
			Dim uncommittedChanges As Boolean = False
			sql = "SELECT UncommittedChanges " & _
				"FROM tblRoutes " & _
				"WHERE RouteID = " & Session("RouteID")

			Try
				If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
					Return
				End If
				rsql.Read()
				uncommittedChanges = CBool(rsql("UncommittedChanges"))
				SqlQueryClose(connSQL, rsql)
			Catch ex As Exception
				LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
			End Try

			If uncommittedChanges Then
				e.RowError = "Cannot create new Sections for a Route that has Uncommitted Changes."
				Return
			End If
		End If

	End Sub

	Private grdSections As ASPxGridView

	Protected Sub grdSections_Init(sender As Object, e As EventArgs)
		grdSections = CType(sender, ASPxGridView)
	End Sub

	Protected Sub ResetAllSections_Click(sender As Object, e As EventArgs)
		UpdateMagMap(False, False)
	End Sub

	Protected Sub MapAllSections_Click(sender As Object, e As EventArgs)
		UpdateMagMap(False, True)
	End Sub

	Private Sub UpdateMagMap(ByVal mag As Boolean, ByVal map As Boolean)
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "spSectionsMagMap_Update"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@routeID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("RouteID"))))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@mag", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, mag))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@map", System.Data.ParameterDirection.Input, System.Data.DbType.Boolean, map))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, CInt(Session("vUserID"))))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "RouteSectionMaint, UpdateMagMap")
			End If
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "RouteSectionMaint, UpdateMagMap")
		Finally
			conn.Close()
		End Try

		grdSections.DataBind()
	End Sub

	Protected Sub dsSections_Updating(ByVal sender As Object, ByVal e As SqlDataSourceCommandEventArgs) Handles dsSections.Updating
		e.Command.CommandTimeout = 300
	End Sub
End Class
