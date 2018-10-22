Imports DevExpress.Web

Partial Class PickupCycleTemplateMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
        Select Case Request.HttpMethod
            Case "POST"
                If IsPostBack Then
                    grdMain.DataBind()
                End If
            Case "GET"
                grdMain.DataBind()
        End Select
    End Sub

    Protected Sub grdMain_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("PickupCycleTemplateID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Protected Sub grdMain_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grdMain.InitNewRow
        e.NewValues("Active") = True
        e.NewValues("Recycle") = True
    End Sub
End Class
