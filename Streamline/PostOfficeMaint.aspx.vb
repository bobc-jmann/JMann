Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DataUtil

Partial Class PostOfficeMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

        dsPostOffices.SelectCommand =
            "SELECT PO.PostOfficeID, PO.PostOfficeCode, PO.DeliverMonday, PO.DeliverTuesday, PO.DeliverWednesday, " & _
                "PO.DeliverThursday, PO.DeliverFriday, PO.UseDayBeforeOnHoliday, PO.Active " & _
            "FROM tblPostOffices AS PO " 

        If Not ckShowInactivePostOffices.Checked Then
            dsPostOffices.SelectCommand += "WHERE PO.Active = 1 "
        End If

        dsPostOffices.SelectCommand += "ORDER BY PO.PostOfficeCode"

        grid.DataBind()
    End Sub

    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
        e.NewValues("Active") = True
        e.NewValues("DeliverMonday") = False
        e.NewValues("DeliverTuesday") = False
        e.NewValues("DeliverWednesday") = False
        e.NewValues("DeliverThursday") = False
        e.NewValues("DeliverFriday") = False
        e.NewValues("UseDayBeforeOnHoliday") = False
    End Sub

    Protected Sub gridAddresses_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grid.RowValidating
        Dim sql As String = ""
        Dim rsql As SqlDataReader = Nothing
        Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
        Dim cnt As Integer = 0

        Dim postOfficeID As Integer = 0
        If Not IsNothing(e.Keys.Values(0)) Then
            postOfficeID = e.Keys.Values(0)
        End If

        sql = "SELECT COUNT(*) AS Cnt " & _
            "FROM tblPostOffices " & _
            "WHERE PostOfficeCode = '" & e.NewValues("PostOfficeCode") & "' " 

        If postOfficeID > 0 Then
            sql += "AND PostOfficeID <> " & postOfficeID
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
            e.RowError = "This Post Office Code is already in use in another Post Office."
            Return
        End If
    End Sub

End Class
