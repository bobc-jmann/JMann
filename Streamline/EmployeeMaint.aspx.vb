Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Web.UI
Imports System.IO
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports DevExpress.Web.Demos
Imports System.Web.Services

Partial Class EmployeeMaint
    Inherits System.Web.UI.Page
    Private newValues As OrderedDictionary
    Private currentPhotoImage As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
    End Sub

    Protected Sub grdMain_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grdMain.InitNewRow
        e.NewValues("EmergencyNumber") = "(916) 388-5900"
    End Sub

    Protected Sub grdMain_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs)
        Me.newValues = e.NewValues
        PopulateNewValues()
    End Sub

    Protected Sub grdMain_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
        Me.newValues = e.NewValues
        PopulateNewValues()
    End Sub

    Private Sub PopulateNewValues()
        Dim formLayout As ASPxFormLayout = CType(grdMain.FindEditFormTemplateControl("formLayout"), ASPxFormLayout)
        formLayout.ForEach(AddressOf ProcessItem)
    End Sub

    Private Sub ProcessItem(ByVal item As LayoutItemBase)
        Dim layoutItem As LayoutItem = TryCast(item, LayoutItem)
        If layoutItem IsNot Nothing Then
            Dim editBase As ASPxEditBase = TryCast(layoutItem.GetNestedControl(), ASPxEditBase)
            If editBase IsNot Nothing Then
                Me.newValues(layoutItem.FieldName) = editBase.Value
            End If
            If layoutItem.FieldName = "PhotoImage" Then
                ' aspxBinaryImage is not an editor so we have to populate the field with the current image from the Session variable
                Me.newValues(layoutItem.FieldName) = Session("uploadedFileData")
            End If
        End If
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs)
		Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Employee Badge")
    End Sub

    Protected Sub uplImage_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs)
        Session("uploadedFileData") = e.UploadedFile.FileBytes
    End Sub

    Protected Sub callbackPanel_Callback(ByVal sender As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase)
        Dim panel As ASPxCallbackPanel = CType(sender, ASPxCallbackPanel)
        Dim bImage As ASPxBinaryImage = CType(panel.FindControl("previewImage"), ASPxBinaryImage)
        bImage.ContentBytes = CType(Session("uploadedFileData"), Byte())
        bImage.Visible = True
    End Sub

    Protected Sub grdMain_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs)
        ' The Session variable keeps the current image.
        Session("uploadedFileData") = grdMain.GetRowValuesByKeyValue(e.EditingKeyValue, "PhotoImage")
    End Sub
End Class
