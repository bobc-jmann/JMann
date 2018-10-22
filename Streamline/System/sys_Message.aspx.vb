Imports System.Diagnostics

Partial Class sys_Message
    Inherits System.Web.UI.Page

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        vbLoadAppVars()
        Dim a() As String = Split(Request.QueryString("MSG"), "|")
		ph_Message1.Text = a(0)
		If a.Count > 1 Then
			ph_Message2.Text = a(1)
			If a.Count > 2 Then
				ph_Message3.Text = a(2)
				If a.Count > 3 Then
					ph_Message4.Text = a(3)
					If a.Count > 4 Then
						ph_Message5.Text = a(4)
						If a.Count > 5 Then
							ph_Message6.Text = a(5)
						End If
					End If
				End If
			End If
		End If
    End Sub

End Class
