Imports DevExpress.Web
Imports DevExpress.Web.Data

Partial Class ZipsDaily
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
        Select Case Request.HttpMethod
            Case "POST"
                If IsPostBack Then
                    grid.DataBind()
                    grdVerify.DataBind()
                End If
            Case "GET"
                grid.DataBind()
                grdVerify.DataBind()
        End Select

    End Sub
End Class
