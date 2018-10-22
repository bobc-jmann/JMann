Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DataUtil

Partial Class OptimumMailDeliveryDaysMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

    End Sub

End Class
