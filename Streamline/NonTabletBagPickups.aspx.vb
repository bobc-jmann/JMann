Partial Class NonTabletBagPickups
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
        Select Case Request.HttpMethod
            Case "POST"
            Case "GET"
                dtEarliestPickupDate.Value = DateAdd(DateInterval.Month, -2, Now)
            Case "HEAD"
        End Select

        Session("EarliestPickupDate") = dtEarliestPickupDate.Value
    End Sub

    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
        e.NewValues("Active") = True
    End Sub

    Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles grid.RowValidating
        If CInt(e.NewValues("PutOuts")) < 0 Then
            e.RowError = "Put Outs cannot be less than zero."
        End If
        If CInt(e.NewValues("PutOuts")) = 0 Then
            e.RowError = "Put Outs cannot be zero."
        End If
    End Sub

End Class
