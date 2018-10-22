Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services
Imports System.Collections.Generic
Imports DataUtil

Partial Class ScheduleCounts
    Inherits System.Web.UI.Page

    Private vDoIt As Boolean
    Private sprm As SqlParameter
    Private cntNotPrinted As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
        Select Case Request.HttpMethod
            Case "POST"
                If IsPostBack Then
                    LoadJobs()
                End If

            Case "GET"
                dtEarliestPickupDate.Value = DateAdd(DateInterval.Month, -2, Now)
                LoadJobs()

            Case "HEAD"

        End Select

    End Sub

    Private Sub LoadJobs()
        hfGridMainSelectCommand.Value = "SELECT * FROM qryScheduler WHERE PickupDate >= '" & Format(dtEarliestPickupDate.Value, "MM/dd/yyyy") & "' " & _
            "ORDER BY PickupDate DESC, PermitNbr, RouteCode DESC"
        dsGridMain.SelectCommand = hfGridMainSelectCommand.Value
        gridMain.DataBind()
    End Sub

	Protected Sub vbOnHtmlDataCellPrepared(ByVal g As ASPxGridView, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
		Dim x As Integer
		If e.DataColumn.FieldName = "PickupDate" Or e.DataColumn.FieldName = "MailingDate" Then
			Dim vDate = g.GetRowValues(e.VisibleIndex, e.DataColumn.FieldName)
			If Not IsDBNull(vDate) Then
				vDoIt = False
				x = SQLExecuteScalar("SELECT COUNT(*) AS Cnt FROM tlkHolidays WHERE HolidayDate='" & vDate & "'", "")
				If Not ismt(x) Then
					vDoIt = True
				End If
				If Weekday(vDate) = 1 Then vDoIt = True
				If vDoIt Then
					e.Cell.ForeColor = Drawing.Color.Red
				End If
				If Weekday(vDate) = 7 Then
					e.Cell.ForeColor = Drawing.Color.Blue
				End If
			End If
		End If
	End Sub

    
End Class
