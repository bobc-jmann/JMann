Imports DevExpress.Web
Imports DevExpress.Data
Imports System.Data
Imports System.Data.SqlClient
Imports DataUtil

Partial Class MailSchedulePlanner
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

		If Session("MailSchedulerSpecialDelete") = "Visible" Then
			fl.FindItemByFieldName("SpecialDelete").Visible = True
			ckSpecialDelete.Visible = True
		Else
			fl.FindItemByFieldName("SpecialDelete").Visible = False
			ckSpecialDelete.Visible = False
		End If
	End Sub

	Protected Sub grdMain_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
		Dim MasterID() As String = Split(Session("MasterID"), "|")
		Session("YearWeek") = MasterID(0)
		Session("RegionID") = MasterID(1)
	End Sub

	Protected Sub grdDetail_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("DetailID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
		Dim MasterID() As String = Split(Session("DetailID"), "|")
		Session("YearWeek") = MasterID(0)
		Session("SectionID") = MasterID(1)
	End Sub

	Protected Sub tbYearWeek_SelectedIndexChanged(sender As Object, e As EventArgs)
		grdMain.DataBind()
	End Sub

	Protected Sub grdMain_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
		If e.Column.FieldName = "MailCartsToSchedule" Or e.Column.FieldName = "ForecastNonRouteMailCarts" Then
			e.Editor.ReadOnly = False
		Else
			e.Editor.ReadOnly = True
		End If
	End Sub

	Protected Sub grdMain_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		Dim forecastMailAllSections As Decimal = CDec(grdMain.GetRowValues(e.VisibleIndex, "ForecastMailCartsAllSections"))
		If e.NewValues("MailCartsToSchedule") > forecastMailAllSections Then
			e.RowError = "Cannot schedule more than the total Forecast Mail for All Sections."
			Return
		End If

		Dim daysToEndOfMailingWeek As Integer = GetDaysToEndOfMailingWeek()

		If daysToEndOfMailingWeek < 0 Then
			e.RowError = "Error reading Calendar"
		ElseIf daysToEndOfMailingWeek < 17 Then
			e.RowError = "This week has already been scheduled and cannot be modified."
		End If
	End Sub

	Protected Sub grdMain_HtmlFooterCellPrepared(sender As Object, e As ASPxGridViewTableFooterCellEventArgs)
		Dim fieldName As String = CType(e.Column, GridViewDataColumn).FieldName
		If fieldName = "InventoryAdjustmentDesired" Or _
			fieldName = "ForecastMailCartsDesired" Or _
			fieldName = "MailCartsToSchedule" Or _
			fieldName = "ForecastInventoryChange" Then
			e.Cell.ForeColor = Drawing.Color.Blue
		End If
	End Sub

	Protected Sub grdDetail_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
		If e.Column.FieldName = "Schedule" Then
			e.Editor.ReadOnly = False
		Else
			e.Editor.ReadOnly = True
		End If
	End Sub

	Protected Sub grdDetail_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
		Dim daysToEndOfMailingWeek As Integer = GetDaysToEndOfMailingWeek()

		If daysToEndOfMailingWeek < 0 And ckSpecialDelete.Checked <> True Then
			e.RowError = "Error reading Calendar"
		ElseIf daysToEndOfMailingWeek < 17 And ckSpecialDelete.Checked <> True Then
			e.RowError = "This week has already been scheduled and cannot be modified."
		End If
	End Sub

	Dim staleCnt As Integer
	Dim newCnt As Integer
	Dim scheduleCnt As Integer
	Dim forecastCarts As Decimal
	Dim forecastPCB As Decimal
	Dim forecastPCO As Decimal
	Dim forecastTPO As Decimal
	Dim groupForecastPCB As Decimal
	Dim groupForecastPCO As Decimal
	Dim groupForecastTPO As Decimal
	Dim allForecastCarts As Decimal
	Dim allForecastPCB As Decimal
	Dim allForecastPCO As Decimal

	Protected Sub grdDetail_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs)
		Try
			Dim fieldName As String = (CType(e.Item, ASPxSummaryItem)).FieldName
			Dim curRow As Integer = e.RowHandle

			Dim grdDetail As ASPxGridView = TryCast(grdMain.FindDetailRowTemplateControl(grdMain.FocusedRowIndex, "grdDetail"), ASPxGridView)

			If IsNothing(grdDetail) Then
				Return
			End If

			Dim summary As ASPxSummaryItem = e.Item

			'If e.IsGroupSummary Then
			'	' Initialization.
			'	If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
			'		If fieldName = "ForecastPCB" Then
			'			groupForecastPCB = 0
			'		End If
			'		If fieldName = "ForecastPCO" Then
			'			groupForecastPCO = 0
			'		End If
			'		If fieldName = "ForecastTPO" Then
			'			groupForecastTPO = 0
			'		End If
			'	End If
			'	' Calculation.
			'	If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
			'		If fieldName = "ForecastPCB" Then
			'			If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
			'				groupForecastPCB += e.FieldValue
			'			End If
			'		End If
			'		If fieldName = "ForecastPCO" Then
			'			If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
			'				groupForecastPCO += e.FieldValue
			'			End If
			'		End If
			'		If fieldName = "ForecastTPO" Then
			'			If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
			'				groupForecastTPO += e.FieldValue
			'			End If
			'		End If
			'	End If
			'	' Finalization.
			'	If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
			'		If fieldName = "ForecastPCB" Then
			'			e.TotalValue = groupForecastPCB
			'		End If
			'		If fieldName = "ForecastPCO" Then
			'			e.TotalValue = groupForecastPCO
			'		End If
			'		If fieldName = "ForecastTPO" Then
			'			e.TotalValue = groupForecastTPO
			'		End If
			'	End If

			'	Return
			'End If

			If fieldName = "HistoricalCartsPerK" Then
				Dim historicalCartsSummary As ASPxSummaryItem = (TryCast(sender, ASPxGridView)).TotalSummary("HistoricalCarts")
				Dim historicalPCBSummary As ASPxSummaryItem = (TryCast(sender, ASPxGridView)).TotalSummary("HistoricalPCB")
				Dim historicalPCOSummary As ASPxSummaryItem = (TryCast(sender, ASPxGridView)).TotalSummary("HistoricalPCO")
				Dim historicalCarts As Decimal = Convert.ToDecimal((CType(sender, ASPxGridView)).GetTotalSummaryValue(historicalCartsSummary))
				Dim historicalPCB As Decimal = Convert.ToDecimal((CType(sender, ASPxGridView)).GetTotalSummaryValue(historicalPCBSummary))
				Dim historicalPCO As Decimal = Convert.ToDecimal((CType(sender, ASPxGridView)).GetTotalSummaryValue(historicalPCOSummary))
				e.TotalValue = historicalCarts / ((historicalPCB + historicalPCO) / 1000)
			End If

			If fieldName = "ForecastCartsPerK" Then
				curRow = curRow
			End If



			' Initialization.
			If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
				If fieldName = "Stale" Then
					staleCnt = 0
				End If
				If fieldName = "New" Then
					newCnt = 0
				End If
				If fieldName = "Schedule" Then
					scheduleCnt = 0
				End If
				If fieldName = "ForecastCarts" Then
					forecastCarts = 0
				End If
				If fieldName = "ForecastCartsNotSeasonallyAdjusted" Then
					forecastCarts = 0
				End If
				If fieldName = "ForecastPCB" Then
					forecastPCB = 0
				End If
				If fieldName = "ForecastPCO" Then
					forecastPCO = 0
				End If
				If fieldName = "ForecastTPO" Then
					forecastTPO = 0
				End If
				If fieldName = "ForecastCartsPerK" Then
					If summary.Tag = "Sch" Then
						forecastCarts = 0
						forecastPCB = 0
						forecastPCO = 0
					Else
						allForecastCarts = 0
						allForecastPCB = 0
						allForecastPCO = 0
					End If
				End If
			End If
			' Calculation.
			If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
				If fieldName = "Stale" Then
					staleCnt += Convert.ToInt32(e.FieldValue)
				End If
				If fieldName = "New" Then
					newCnt += Convert.ToInt32(e.FieldValue)
				End If
				If fieldName = "Schedule" Then
					scheduleCnt += Convert.ToInt32(e.FieldValue)
				End If
				If fieldName = "ForecastCarts" Then
					If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
						forecastCarts += e.FieldValue
					End If
				End If
				If fieldName = "ForecastCartsNotSeasonallyAdjusted" Then
					If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
						forecastCarts += e.FieldValue
					End If
				End If
				If fieldName = "ForecastPCB" Then
					If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
						forecastPCB += e.FieldValue
					End If
				End If
				If fieldName = "ForecastPCO" Then
					If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
						forecastPCO += e.FieldValue
					End If
				End If
				If fieldName = "ForecastTPO" Then
					If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
						forecastTPO += e.FieldValue
					End If
				End If
				If fieldName = "ForecastCartsPerK" Then
					If summary.Tag = "Sch" Then
						If Convert.ToBoolean(grdDetail.GetRowValues(curRow, "Schedule")) Then
							forecastCarts += Convert.ToDecimal(grdDetail.GetRowValues(curRow, "ForecastCarts"))
							forecastPCB += Convert.ToDecimal(grdDetail.GetRowValues(curRow, "ForecastPCB"))
							forecastPCO += Convert.ToDecimal(grdDetail.GetRowValues(curRow, "ForecastPCO"))
						End If
					Else
						allForecastCarts += Convert.ToDecimal(grdDetail.GetRowValues(curRow, "ForecastCarts"))
						allForecastPCB += Convert.ToDecimal(grdDetail.GetRowValues(curRow, "ForecastPCB"))
						allForecastPCO += Convert.ToDecimal(grdDetail.GetRowValues(curRow, "ForecastPCO"))
					End If
				End If
			End If
			' Finalization.
			If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
				If fieldName = "Stale" Then
					e.TotalValue = staleCnt
				End If
				If fieldName = "New" Then
					e.TotalValue = newCnt
				End If
				If fieldName = "Schedule" Then
					e.TotalValue = scheduleCnt
				End If
				If fieldName = "ForecastCarts" Then
					e.TotalValue = forecastCarts
				End If
				If fieldName = "ForecastCartsNotSeasonallyAdjusted" Then
					e.TotalValue = forecastCarts
				End If
				If fieldName = "ForecastPCB" Then
					e.TotalValue = forecastPCB
				End If
				If fieldName = "ForecastPCO" Then
					e.TotalValue = forecastPCO
				End If
				If fieldName = "ForecastTPO" Then
					e.TotalValue = forecastTPO
				End If
				If fieldName = "ForecastCartsPerK" Then
					If summary.Tag = "Sch" Then
						e.TotalValue = forecastCarts / ((forecastPCB + forecastPCO) / 1000)
					Else
						e.TotalValue = allForecastCarts / ((allForecastPCB + allForecastPCO) / 1000)
					End If
				End If
			End If

		Catch ex As Exception
			ex = ex
		End Try

	End Sub

	Private Function GetDaysToEndOfMailingWeek() As Integer
		Dim daysToEndOfMailingWeek As Integer = 0
		Dim sql As String
		Dim rsql As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
		sql = "SELECT TOP (1) DATEDIFF(DAY, SYSDATETIME(), CalendarYearWeekEndingDate) AS DaysToEndOfMailingWeek " & _
			"FROM Calendar " & _
			"WHERE CalendarYearWeekNumber = " & tbYearWeek.Value.ToString()

		If Not SqlQueryOpen(connSQL, rsql, sql, "Notify Web User") Then
			SqlQueryClose(connSQL, rsql)
			Return -1
		End If

		If rsql.Read() Then
			daysToEndOfMailingWeek = CInt(rsql("DaysToEndOfMailingWeek").ToString)
		End If
		SqlQueryClose(connSQL, rsql)

		Return daysToEndOfMailingWeek
	End Function
End Class
