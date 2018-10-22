Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services

Partial Class PhoneSummary
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
                            LoadPhoneSheets()
                    End Select
                End If

            Case "GET"
                dtDateMin.Value = DateAdd(DateInterval.Month, -2, Now)
                LoadPhoneSheets()
            Case "HEAD"
                'Response.Redirect(ConfigurationManager.AppSettings.Item(My.Computer.Name & "_DefaultContent").ToString)
        End Select
    End Sub

    Private Shared Function SelectPhoneSheets(ByVal joinWhereClause As String) As String
		Dim sql As String = "SELECT CAST(PS.[Date] AS varchar) + CAST(PS.UserID AS varchar) AS UserDate, PS.UserID, PS.[Date], U.Username AS Operator, " & _
				"PS.RollOverVoiceMail, PS.VoiceMailReturned, PS.WebSpecialsReturned, PS.EmailsProcessed, PS.TotalCallsReceived, PS.CourtesyCallsMade, PS.ReRouteCallsMade, PS.OtherComplaints,  " & _
				"(SELECT COUNT(*) FROM tblSpecials S WHERE CreatedBy = " & _
					"(SELECT Username FROM users.Users U WHERE UserID = PS.UserID) AND S.CreatedOn >= PS.[Date] " & _
					"AND S.CreatedOn < DATEADD(d, 1, PS.[Date])) AS SpecialsCreated, " & _
				"(SELECT COUNT(*) FROM tSysConfirmMissLog LE WHERE UserName = " & _
					"(SELECT Username FROM users.Users U WHERE UserID = PS.UserID) AND LE.LogDate >= PS.[Date] " & _
					"AND LE.LogDate < DATEADD(d, 1, PS.[Date]) AND LE.Confirmed = 1) AS Confirms, " & _
				"(SELECT COUNT(*) FROM tSysConfirmMissLog LE WHERE UserName = " & _
					"(SELECT Username FROM users.Users U WHERE UserID = PS.UserID) AND LE.LogDate >= PS.[Date] " & _
					"AND LE.LogDate < DATEADD(d, 1, PS.[Date]) AND LE.Missed = 1) AS Misses, " & _
				"PS.Comments " & _
			"FROM tblPhoneSheetsForSpecials PS INNER JOIN users.Users U ON U.UserID = PS.UserID " & _
			joinWhereClause & " " & _
			"ORDER BY PS.[Date] DESC, U.Username "
        Return (sql)
    End Function

    Private Sub LoadPhoneSheets()
        hfPhoneSheetsSelectCommand.Value = SelectPhoneSheets("WHERE PS.[Date] >= '" & Format(dtDateMin.Value, "MM/dd/yyyy") & "' ")
        dsPhoneSheets.SelectCommand = hfPhoneSheetsSelectCommand.Value
        grdPhoneSheets.DataBind()
    End Sub

	Protected Sub btnPhoneSheets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPhoneSheets.Click
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Phone Sheets")
	End Sub

End Class
