Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Drawing
Imports DevExpress.XtraReports.UI
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.Services

Partial Class Finance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		'rd("~/Login.aspx") : re()
		'Session.Abandon()
		' RCC - 2/16/17 testing
		'Session("vUserName") = Nothing
		'rd("~/Login.aspx?T=ABOUT")

		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					Dim PostBackControlID As String = ""
					Try
						'PostBackControlID = GetPostBackControl.ID
					Catch ex As Exception
					End Try

					Select Case PostBackControlID
						Case Else

					End Select
				End If
			Case "GET"
				If InStr(vConnStr, "_Training") > 0 Then
					hfURL.Value = "http://finance_training.ecothrift.com/default.aspx?user="
				Else
					hfURL.Value = "http://finance.ecothrift.com/default.aspx?user="
					'hfURL.Value = "http://localhost:9869/Finance/default.aspx?user="
				End If
				hfUserName.Value = Session("vUserName")
			Case "HEAD"

		End Select

    End Sub
End Class
