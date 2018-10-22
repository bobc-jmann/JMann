Imports DevExpress.Web
Imports System.Drawing
Imports DataUtil

Partial Class StreamlineItemsMaint
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
            "SELECT I.ItemID, I.Active, I.ItemCode, I.ItemDescription, I.ItemTypeID, I.OnHand, I.EstimatedRunoutDate, " &
                "IT.ItemType, IT.LeadTime " &
            "FROM Streamline.Items I " &
            "INNER JOIN Streamline.ItemTypes AS IT ON IT.ItemTypeID = I.ItemTypeID "

        If Not ckShowInactiveItems.Checked Then
            sql += "WHERE I.Active = 1 "
        End If

        sql += "ORDER BY ItemType"

        dsItems.SelectCommand = sql
        grid.DataBind()
    End Sub

    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
        e.NewValues("Active") = True
        e.NewValues("OnHand") = 0
    End Sub

    Protected Sub grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.FieldName = "EstimatedRunoutDate" Then
            Dim estimatedRunoutDate As Object = grid.GetRowValues(e.VisibleIndex, "EstimatedRunoutDate")
            If Not IsDate(estimatedRunoutDate) Then
                Return
            End If
            Dim leadTimeInDays As Integer = grid.GetRowValues(e.VisibleIndex, "LeadTime") * 7
            Dim DaysToRunoutDate = DateDiff(DateInterval.Day, Now(), estimatedRunoutDate)
            If DaysToRunoutDate < leadTimeInDays Then
                e.Cell.ForeColor = Color.Red
            ElseIf DaysToRunoutDate < leadTimeInDays + 14 Then
                e.Cell.ForeColor = Color.Blue
            End If
        End If
    End Sub
End Class
