Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DataUtil

Partial Class StreamlineItemTypesMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

        Select Case Request.HttpMethod
            Case "POST"
                If IsPostBack Then

                End If
            Case "GET"
                ckShowInactiveItems.Checked = False
        End Select

        Dim sql As String =
            "SELECT ItemTypeID, Active, ItemType, QtyPerMailing, MailTypeID, CharityID, LeadTime " &
            "FROM Streamline.ItemTypes "

        If Not ckShowInactiveItems.Checked Then
            sql += "WHERE Active = 1 "
        End If

        sql += "ORDER BY ItemType"

        dsItemTypes.SelectCommand = sql
        grid.DataBind()
    End Sub

    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
        e.NewValues("Active") = True
    End Sub

End Class
