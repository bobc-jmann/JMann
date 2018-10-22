Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DataUtil

Partial Class MailingDateRulesMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

    End Sub

    Protected Sub gridAddresses_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grid.RowValidating
        Dim sql As String = ""
        Dim rsql As SqlDataReader = Nothing
        Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
        Dim cnt As Integer = 0

        Dim mailingDateRulesID As Integer = 0
        If Not IsNothing(e.Keys.Values(0)) Then
            mailingDateRulesID = e.Keys.Values(0)
        End If

		sql = "SELECT COUNT(*) AS Cnt " & _
			"FROM tblMailingDateRules " & _
			"WHERE City = '" & e.NewValues("City") & "' " & _
				"AND Zip = " & e.NewValues("Zip")

        If mailingDateRulesID > 0 Then
            sql += "AND MailingDateRulesID <> " & mailingDateRulesID
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
			e.RowError = "This City, Zip combination already has an entry."
            Return
        End If

		sql = "SELECT COUNT(*) AS Cnt " & _
			"FROM tlkPrimaryCityZipFromRoute " & _
			"WHERE City = '" & e.NewValues("City") & "' " & _
				"AND (CASE WHEN LEN('" & e.NewValues("Zip") & "') = 3 AND SUBSTRING(Zip5, 1, 3) = '" & e.NewValues("Zip") & "' THEN 1 " & _
					"WHEN Zip5 = '" & e.NewValues("Zip") & "' THEN 1 ELSE 0 END) > 0 "

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

		If cnt = 0 Then
			e.RowError = "This City, Zip3 or City, Zip5 combination does not exist as a Primary City and Zip on a Route."
			Return
			End If

		If IsNothing(e.NewValues("Zip")) Then
			e.RowError = "Please enter a Zip3 or a Zip5."
			Return
		ElseIf Not (e.NewValues("Zip") >= 900 And e.NewValues("Zip") <= 99999) Then
			e.RowError = "Zip3 must be between 900 and 99999."
			Return
			End If

        If IsNothing(e.NewValues("DeliveryDays")) Then
            e.RowError = "Please enter Delivery Days."
            Return
        ElseIf Not (e.NewValues("DeliveryDays") >= 1 And e.NewValues("DeliveryDays") <= 20) Then
            e.RowError = "Delivery Days must be between 1 and 20."
            Return
			End If

        If IsNothing(e.NewValues("PostOfficeID")) Then
            e.RowError = "Please choose a Post Office."
            Return
			End If

	End Sub

End Class
