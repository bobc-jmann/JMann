Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports System.Web.UI
Imports DevExpress.Data
Imports DataUtil

Partial Class BracketPricing
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		Dim sql As String
		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					grdMain.DataBind()
				End If
			Case "GET"
				sql = "WITH D AS " & _
					"( " & _
						"SELECT C.CalendarYearWeekNumber AS YearWeek, C.CalendarWeek AS WeekNum, " & _
							"C.CalendarYearWeekEndingDate AS WeekEndingDate, C.CalendarYearWeekEnding AS WeekText " & _
						"FROM Calendar AS C " & _
						"WHERE C.CalendarYear IN (YEAR(SYSDATETIME()) - 1, YEAR(SYSDATETIME())) " & _
							"AND (C.CalendarYear = YEAR (SYSDATETIME()) - 1 OR (C.CalendarWeek < (SELECT CalendarWeek " & _
								"FROM Calendar AS DW " & _
								"WHERE DW.[date] = CAST(SYSDATETIME() AS DATE)))) " & _
					") " & _
					"SELECT D.YearWeek, D.WeekEndingDate, WeekText " & _
					"FROM D WHERE WeekEndingDate < SYSDATETIME() " & _
					"GROUP BY YearWeek, WeekNum, D.WeekEndingDate, D.WeekText " & _
					"ORDER BY WeekEndingDate DESC"
				Dim da As SqlDataAdapter = New SqlDataAdapter(sql, vConnStr)
				Dim dt As DataTable = New DataTable()
				da.Fill(dt)
				ddlYearWeek.DataSource = dt
				ddlYearWeek.DataTextField = "WeekText"
				ddlYearWeek.DataValueField = "YearWeek"
				ddlYearWeek.DataBind()
				ddlYearWeek.Items.Insert(0, New ListItem("<Select Year-Week>", "0"))

				sql = "SELECT L.FinanceLocationID AS LocationID, L.LocationDesc + ' (' + RG.RegionCode + ')' AS LocationDesc " & _
					"FROM tblLocations AS L " & _
					"INNER JOIN tlkRegions AS RG ON RG.RegionID = L.RegionID " & _
					"WHERE L.FinanceLocationID NOT IN (5, 9) " & _
						"AND L.AccountingDescriptionSortOrder IS NOT NULL " & _
					"ORDER BY LocationDesc"
				da = New SqlDataAdapter(sql, vConnStr)
				dt = New DataTable()
				da.Fill(dt)
				ddlLocations.DataSource = dt
				ddlLocations.DataTextField = "LocationDesc"
				ddlLocations.DataValueField = "LocationID"
				ddlLocations.DataBind()
				ddlLocations.Items.Insert(0, New ListItem("<Select Location>", "0"))
		End Select


		' Insert any missing rows for the YearWeek and LocationID
		If ddlLocations.SelectedValue > 0 And ddlYearWeek.SelectedValue > 0 Then
			sql = "INSERT INTO Stores.BracketPricing (YearWeek, LocationID, DepartmentID, PriceLevelID) " & _
				"SELECT " & ddlYearWeek.SelectedValue & ", " & ddlLocations.SelectedValue & ", BS.DepartmentID, BS.ID " & _
				"FROM Stores.BracketStandards AS BS " & _
				"WHERE NOT EXISTS (SELECT * FROM Stores.BracketPricing AS BP " & _
					"WHERE BP.YearWeek = " & ddlYearWeek.SelectedValue & " " & _
						"AND BP.LocationID = " & ddlLocations.SelectedValue & " " & _
						"AND BP.DepartmentID = BS.DepartmentID " & _
						"AND BP.PriceLevelID = BS.ID) " & _
					"AND BS.Active = 1 " & _
					"AND BS.StandardTypeID = 1"

			SqlNonQuery(sql)
		End If

		grdMain.DataBind()

	End Sub

	Protected Sub grdMain_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
	End Sub

	Protected Sub grdMain_CustomJSProperties(sender As Object, e As ASPxGridViewClientJSPropertiesEventArgs)
		e.Properties("cpIsEditing") = grdMain.IsEditing
	End Sub

	Protected Sub grdMain_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
		Dim box As ASPxTextBox = TryCast(e.Editor, ASPxTextBox)
		If box Is Nothing Then
			Return
		End If
		box.ClientSideEvents.KeyDown = "editor_KeyDown"

		If e.Column.FieldName = "DepartmentName" Or e.Column.FieldName = "PriceLevel" Then
			e.Editor.ClientEnabled = False
		End If
	End Sub

	Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "Stores.spBracketPricingReportGenerate"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@yearWeek", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, ddlYearWeek.SelectedValue))
		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))
		myCmd.CommandTimeout = 180
		btnUpdate.Text = "Update Data for Reports"
		btnUpdate.Enabled = True
		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "BracketPricing, btnUpdate_Click")
			End If
			ja("Update Complete!")
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try
	End Sub

	Protected Sub btnCartsUpdate_Click(sender As Object, e As EventArgs) Handles btnCartsUpdate.Click
		Dim conn As SqlConnection = New SqlConnection(vConnStr)

		Dim myCmd As SqlCommand = New SqlCommand()
		myCmd.Connection = conn
		myCmd.CommandText = "Carts.spCartETL"
		myCmd.CommandType = System.Data.CommandType.StoredProcedure

		Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))
		btnCartsUpdate.Text = "Update Cart Data"
		btnCartsUpdate.Enabled = True
		Try
			conn.Open()
			myCmd.ExecuteNonQuery()
			errorID = myCmd.Parameters("@RETURN_VALUE").Value
			If errorID > 0 Then
				vbHandleProgramError(errorID, "BracketPricing, btnCartsUpdate_Click")
			End If
			ja("Update Complete!")
		Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
		Finally
			conn.Close()
		End Try
	End Sub

End Class
