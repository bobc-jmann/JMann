Imports DevExpress.Web
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Diagnostics
Imports DataUtil

Partial Class UnsubscribeEmailAddress
	Inherits System.Web.UI.Page

	Private chk As ASPxCheckBox

	Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		'http://localhost:10674/Streamline/UnsubscribeEmailAddress.aspx?emailaddress=rcomyn@comcast.net&addressid=4174426

		Select Case Request.HttpMethod
			Case "POST"
			Case "GET"
				Dim emailAddress As String = "x"
				Dim addressID As Integer = 0

				emailAddress = Request.QueryString("emailaddress")
				addressID = CInt(Request.QueryString("addressid"))

				Dim sql As String = "UPDATE tblAddressEmails " & _
					"SET Unsubscribed = 1 " & _
					"WHERE Email = '" & emailAddress & "' " & _
						"AND AddressID = " & addressID

				Dim cnt As Integer = SqlNonQuery(sql)

				If cnt > 0 Then
					content.InnerHtml = "You have been successfully unsubscribed."
				Else
					content.InnerHtml = "Error unsubscribing. Please call the number on your email."
				End If
			Case "HEAD"
		End Select


	End Sub

End Class
