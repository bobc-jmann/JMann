Imports DevExpress.Web
Imports DevExpress.Web.Data

Partial Class CharityMaint
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
        Session("CharityID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

	Protected Sub grdMain_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grdMain.InitNewRow
		e.NewValues("Email_OK") = False
		e.NewValues("Active") = True
	End Sub

    Protected Sub grdDetail_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)
        e.NewValues("Active") = True
    End Sub

    Protected Sub grdDetail_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
        e.NewValues("PermitNotes") = GetMemoText(CType(sender, ASPxGridView))
    End Sub

    Protected Sub grdDetail_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs)
        e.NewValues("PermitNotes") = GetMemoText(CType(sender, ASPxGridView))
    End Sub

    Protected Function GetMemoText(ByVal grdDetail As ASPxGridView) As String
        Dim pageControl As ASPxPageControl = TryCast(grdDetail.FindEditFormTemplateControl("pageControl"), ASPxPageControl)
        Dim memo As ASPxMemo = TryCast(pageControl.FindControl("notesEditor"), ASPxMemo)
        Return memo.Text
	End Function

	Protected Sub dsCharities_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles dsCharities.Selecting
		e.Command.CommandTimeout = 120
	End Sub
End Class
