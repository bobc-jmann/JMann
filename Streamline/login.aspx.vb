Imports System.Data.SqlClient
Imports System
Imports DataUtil

Partial Class Login
    Inherits System.Web.UI.Page

	Private vLoginCt As Integer

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load, Me.Load
        If UCase(Request.QueryString("T")) = "LOGOUT" Then
            Session("vUserID") = Nothing
            Session("vUserName") = Nothing
            Session("vUserFirstName") = Nothing
            Session("vUserEmail") = Nothing
            Session("vUserLevel") = Nothing
            Session("vLocationCode") = Nothing
        End If
        If Not IsPostBack Then
            vLoginCt = 0
        End If
    End Sub

    Public Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
		Dim rs As SqlDataReader = Nothing
		Dim connSQL As SqlConnection = New SqlConnection(vConnStr)

		Dim userName, passWord, newPassWord, confirmNewPassWord, userEmail, s As String
        Dim ct As Integer
		lblError.Text = ""
        userName = txtUserName.Text
        passWord = txtPassword.Text
        Select Case UCase(sender.id)
            Case "BTNLOGIN"
                If userName <> "" AndAlso passWord <> "" Then
                    s = "SELECT U.*, RG.RegionID " & _
                        "FROM users.Users U " & _
                        "LEFT OUTER JOIN tlkRegions AS RG ON RG.RegionCode = U.LocationCode " & _
                        "WHERE upper(Username) = upper(@userName) AND upper(Password) = upper(@passWord) AND U.Active = 1"
					connSQL.Open()
					Dim cmdSelect As New SqlCommand(s, connSQL)
                    cmdSelect.Parameters.AddWithValue("@userName", userName)
                    cmdSelect.Parameters.AddWithValue("@passWord", passWord)
                    rs = cmdSelect.ExecuteReader
                    While rs.Read
                        Session("vUserID") = rs("UserID")
                        Session("vUserName") = rs("UserName")
                        Session("vUserFirstName") = rs("UserFirstName")
                        Session("vUserEmail") = rs("EmailAddress")
                        Session("vUserLevel") = rs("UserLevel")
                        Session("vUserGroup") = rs("UserGroup")
                        Session("vLocationCode") = rs("LocationCode").ToString
                        Session("vLocationID") = rs("RegionID").ToString
					End While
					rs.Close()
					connSQL.Close()

					If userName.ToUpper = passWord.ToUpper Then
						lblError.Text = "Your password cannot match your username. Please reset your password."
						rd("login.aspx?T=RESET")
					ElseIf Not ismt(Session("vUserID")) And Not Session("vUserID") = -1 Then
						'rl("<script>window.parent.location='" & vIntranetPage & "?ID=" & Session("vUserID") & "'</script>")

						' RCC - 2/16/17 - testing
						'If Request.QueryString("T") = "ABOUT" Then
						'	rd("AboutIntranet.aspx")
						'Else
						'	rd("Intranet.aspx")
						'End If

						rd("Intranet.aspx")
					Else
						lblError.Text = "Invalid Username or Password"
					End If

                Else
                    lblError.Text = "Please complete all fields"
                End If
                vLoginCt = vLoginCt + 1
                If vLoginCt > 5 Then
                    lblError.Text = "Sorry, too many login attempts.  Try again in a few minutes."
                    btnLogin.Enabled = False
                    Dim cookie = New HttpCookie(userName)
                    cookie.Name = userName
                    cookie.Value = DateTime.Now.AddMinutes(1)
                    Response.Cookies.Add(cookie)
                End If
                If Not Request.Cookies(userName) Is Nothing Then
                    If (CDate(Request.Cookies(userName).Value) < DateTime.Now) And (lblError.Text = "Sorry, too many login attempts.  Try again in a few minutes.") Then
                        lblError.Text = ""
                        btnLogin.Enabled = True
                        vLoginCt = 0
                    End If
                End If
            Case "BTNRESET"
                passWord = txtOldPassword.Text
                newPassWord = txtNewPassword.Text
                confirmNewPassWord = txtConfirmNewPassword.Text
                userName = txtUserName2.Text
                If userName <> "" AndAlso passWord <> "" Then
					connSQL.Open()
					Dim sql As String = "SELECT UserID,UserName,UserFirstName,EmailAddress from users.Users WHERE upper(Username) = upper(@userName) AND upper(Password) = upper(@passWord)"
					Dim cmdSelect As New SqlCommand(sql, connSQL)
                    cmdSelect.Parameters.AddWithValue("@userName", userName)
                    cmdSelect.Parameters.AddWithValue("@passWord", passWord)
					rs = cmdSelect.ExecuteReader
                    While rs.Read
                        Session("vUserID") = rs("UserID")
                        Session("vUserName") = rs("UserName")
                        Session("vUserFirstName") = rs("UserFirstName")
                        Session("vUserEmail") = rs("EmailAddress")
                    End While
					rs.Close()
					connSQL.Close()
                    If Not ismt(Session("vUserID")) And Not Session("vUserID") = -1 Then
                        Dim s1 As String = ""
                        Dim s2 As String = ""
                        Dim vMatch = True
                        If newPassWord.Length <> confirmNewPassWord.Length Then vMatch = False
                        For i = 1 To newPassWord.Length
                            s1 = Mid(newPassWord, i, 1)
                            s2 = Mid(newPassWord, i, 1)
                            If s1 <> s2 Then vMatch = False
                        Next
                        If Not vMatch Then lblError.Text = "The New and Retype New password fields do not match.  Try again." : Exit Sub
						sql = "select count(*) as ct from users.Users where UserID=" & Session("vUserID") & " and upper(Password)='" & UCase(newPassWord) & "'"
						ct = SQLExecuteScalar(sql, "")
                        If ct <> 0 Then lblError.Text = "Sorry, you have used that password before.  Try again." : Exit Sub
                        If newPassWord.Length < 8 Then lblError.Text = "Your password must be at least 8 characters.  Try again." : Exit Sub
                        Dim vFoundNbr As Boolean = False
                        Dim vFoundSpec As Boolean = False
                        Dim vFoundUcase As Boolean = False
                        Dim vSpecChrs As String = "!@#$%^&*()"
                        Dim vNumbers As String = "0123456789"
                        For i As Integer = 1 To newPassWord.Length
							s = Mid(newPassWord, i, 1)
                            If s = s.ToUpper And InStr(vSpecChrs, s) = 0 And InStr(vNumbers, s) = 0 Then vFoundUcase = True
                            If InStr(vNumbers, s) > 0 Then vFoundNbr = True
                            If InStr(vSpecChrs, s) > 0 Then vFoundSpec = True
                        Next
                        If Not vFoundNbr Then lblError.Text = "There must be at least one number in your password." : Exit Sub
                        If Not vFoundSpec Then lblError.Text = "There must be at least one special character !@#$()%^&* in your password." : Exit Sub
                        If Not vFoundUcase Then lblError.Text = "There must be at least one upper case character in your password." : Exit Sub
                        s = "update users.Users set Password='" & newPassWord & "' where UserID=" & Session("vUserID")
						SqlNonQuery(s)
                        lblError.Text = "You have successfully reset your password."
                        rl("<script>window.parent.location='" & vIntranetPage & "'</script>")
                    Else
                        lblError.Text = "Invalid Username or Password"
                    End If
                Else
                    lblError.Text = "Please complete all fields"
                End If
            Case "BTNFORGOT"
                userName = txtUserName3.Text
                userEmail = txtUserEmail.Text
                s = "select count(*) as ct from users.Users where upper(UserName)='" & UCase(userName) & "' and upper(EmailAddress)='" & UCase(userEmail) & "'"
				If Not SqlQueryOpen(connSQL, rs, s, "Notify Web User") Then
					Return
				End If
				ct = 0
                While rs.Read
                    ct = rs("ct")
                End While
				SqlQueryClose(connSQL, rs)
				If ct = 0 Then lblError.Text = "We could not find this username / email combo.  Try again." : Exit Sub
                Dim vQuestion = "", vAnswer = ""
                Dim vUserID As Integer = 0
                Dim vEmail As String = ""
                Dim vUserFirstName As String = ""
                Dim vUserLastName As String = ""
                Dim vBody As String = ""
                Dim vPassword As String = ""
                s = "select * from users.Users where upper(UserName)='" & UCase(userName) & "' and upper(EmailAddress)='" & UCase(userEmail) & "'"
				If Not SqlQueryOpen(connSQL, rs, s, "Notify Web User") Then
					Return
				End If
				While rs.Read
					vQuestion = rs("PWQuestion")
					vUserID = rs("UserID")
					vEmail = rs("EmailAddress")
					vUserFirstName = rs("UserFirstName")
					vUserLastName = rs("UserLastName")
					vPassword = rs("Password")
				End While
				SqlQueryClose(connSQL, rs)
				If ismt(vQuestion) Then lblError.Text = "We could not find a registered question for this account.  Please contact your system administrator." : Exit Sub
                divQA.Visible = True
                txtPasswordQuestion.Text = vQuestion
                vAnswer = txtPasswordAnswer.Text
                If Not ismt(vAnswer) Then
                    s = "select count(*) as ct from users.Users where UserID=" & vUserID & " and upper(PWAnswer)='" & UCase(vAnswer) & "'"
					If Not SqlQueryOpen(connSQL, rs, s, "Notify Web User") Then
						Return
					End If
					ct = 0
                    While rs.Read
                        ct = rs("ct")
                    End While
					SqlQueryClose(connSQL, rs)
					If ct = 0 Then lblError.Text = "Sorry, incorrect answer.  Try again." : Exit Sub
                    vBody = "Dear " & vUserFirstName & " " & vUserLastName & ", <br>"
                    vBody = vBody & "<br>"
                    vBody = vBody & "You have requested your current password.  It can be found on the line below.<br>"
                    vBody = vBody & "<br>"
                    vBody = vBody & vPassword & "<br>"
                    vBody = vBody & "<br>"
                    vBody = vBody & "Go to the " & vAppShortTitle & " website and login with your username and this password.<br>"
                    vBody = vBody & "<br>"
                    vBody = vBody & "Thank you for contacting " & vAppShortTitle & " support.<br>"
                    vBody = vBody & "<br>"
                    vBody = vBody & vAdminEmail & "<br>"
                    vbSendEmail(vAdminName, vAdminEmail, vEmail, "Message from " & vAppShortTitle & " System Administrator", vBody, 0, "HTML")
                    lblError.Text = "An email with your current password has been sent to your registered email address.  Click <a href='Login.aspx'>here</a> to login."
                End If
        End Select
    End Sub

End Class
