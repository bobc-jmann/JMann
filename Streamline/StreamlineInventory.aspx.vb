Imports System.Data.SqlClient
Imports System.Drawing
Imports DevExpress.Web
Imports DataUtil

Partial Class StreamlineInventory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()

        gridItems.JSProperties("cpIsUpdated") = False
        gridTransactions.JSProperties("cpIsUpdated") = False

        Dim PostBackControlID As String = ""
        Select Case Request.HttpMethod
            Case "POST"
                gridItems.DataBind()

            Case "GET"
                dtEarliestTransactionDate.Value = DateAdd(DateInterval.Month, -2, Now)

            Case "HEAD"
        End Select

        Dim sql As String
        sql = "SELECT T.InventoryTransactionID, T.TransactionDate, T.ItemID, IT.ItemType, " &
                "T.TransactionTypeID, TT.TransactionType, T.Quantity, T.Notes, " &
                "C.CharityAbbr AS Charity, PS.RouteCode AS Route, PS.MailingDate, U.Username AS LastEditedBy " &
            "FROM Streamline.InventoryTransactions AS T " &
            "INNER JOIN Streamline.TransactionTypes AS TT ON TT.TransactionTypeID = T.TransactionTypeID " &
            "INNER JOIN Streamline.Items AS I ON I.ItemID = T.ItemID " &
            "INNER JOIN Streamline.ItemTypes AS IT ON IT.ItemTypeID = I.ItemTypeID " &
            "LEFT OUTER JOIN dbo.tblPickupSchedule AS PS ON PS.PickupScheduleID = T.PickupScheduleID " &
            "LEFT OUTER JOIN dbo.tblCharities AS C ON C.CharityID = PS.CharityID " &
            "LEFT OUTER JOIN users.users AS U ON U.UserID = T.LastEditedBy " &
            "WHERE T.TransactionDate >= '" & Format(dtEarliestTransactionDate.Value, "MM/dd/yyyy") & "' " &
            "ORDER BY T.TransactionDate DESC, TT.TransactionType"
        dsGridTransactions.SelectCommand = sql
        gridTransactions.DataBind()
    End Sub

    Protected Sub gridTransactions_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
        If e.VisibleIndex < 0 Then
            Return
        End If

        Select Case e.ButtonType
            Case ColumnCommandButtonType.Delete
                e.Visible = CommandButtonVisibleCriteria(e.VisibleIndex, "Delete")
            Case ColumnCommandButtonType.Edit
                e.Visible = CommandButtonVisibleCriteria(e.VisibleIndex, "Edit")
        End Select
    End Sub

    Private Function CommandButtonVisibleCriteria(ByVal visibleIndex As Integer, ByVal buttonType As String) As Boolean
        Dim TransactionTypeID As Integer = CInt(gridTransactions.GetRowValues(visibleIndex, "TransactionTypeID").ToString())
        Select Case TransactionTypeID
            Case 1, 3, 4, 5
                Return True
        End Select
        Return False
    End Function

    Protected Sub gridTransactions_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
        Dim lineTypeID As Integer = 0
        If e.VisibleIndex > 0 Then
            lineTypeID = gridTransactions.GetRowValues(e.VisibleIndex, "TransactionTypeID")
        End If

        If e.Column.FieldName = "TransactionTypeID" Then
            Dim sql As String = "SELECT TransactionTypeID, TransactionType " &
                "FROM Streamline.TransactionTypes "
            If lineTypeID = 0 Then
                sql += "WHERE TransactionTypeID IN (1, 3, 4, 5) "
            End If
            sql += "ORDER BY SortCode"
            dsTransactionTypes.SelectCommand = sql
            dsTransactionTypes.DataBind()
        End If

        e.Editor.ClientEnabled = True
        e.Editor.ReadOnly = False
    End Sub

    Protected Sub gridTransactions_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles gridTransactions.RowValidating
        If IsNothing(e.NewValues("ItemID")) Then
            e.RowError = "Please select an Item."
            Return
        End If

        If IsNothing(e.NewValues("TransactionTypeID")) Then
            e.RowError = "Please select a Transaction Type."
            Return
        End If

        If Not e.IsNewRow And e.OldValues("TransactionTypeID") <> e.NewValues("TransactionTypeID") Then
            e.RowError = "You cannot change the Transaction Type while editing."
            Return
        End If

        ' Manufacture
        If e.NewValues("TransactionTypeID") = 5 Then
            Dim sql As String = ""
            Dim rsql As SqlDataReader = Nothing
            Dim connSQL As SqlConnection = New SqlConnection(vConnStr)
            Dim cnt As Integer = 0

            sql = "SELECT COUNT(*) AS Cnt " &
                "FROM Streamline.BillsOfMaterial AS BOM " &
                "INNER JOIN Streamline.Items AS I ON I.ItemtypeID = BOM.FinishedItemTypeID " &
                "WHERE I.ItemID = '" & e.NewValues("ItemID") & "'"

            Try
                If Not SqlQueryOpen(connSQL, rsql, sql, "Notify User") Then
                    Return
                End If
                rsql.Read()
                cnt = rsql("Cnt")
                SqlQueryClose(connSQL, rsql)
            Catch ex As Exception
                LogProgramError(ex.Message, "", ex.StackTrace, "Notify User")
            End Try

            If cnt = 0 Then
                e.RowError = "Only Items with a Bill of Materials can be manufactured."
                Return
            End If
        End If
    End Sub

    Protected Sub gridItems_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.FieldName = "EstimatedRunoutDate" Then
            Dim estimatedRunoutDate As Object = gridItems.GetRowValues(e.VisibleIndex, "EstimatedRunoutDate")
            If Not IsDate(estimatedRunoutDate) Then
                Return
            End If
            Dim leadTimeInDays As Integer = gridItems.GetRowValues(e.VisibleIndex, "LeadTime") * 7
            Dim DaysToRunoutDate = DateDiff(DateInterval.Day, Now(), estimatedRunoutDate)
            If DaysToRunoutDate < leadTimeInDays Then
                e.Cell.ForeColor = Color.Red
            ElseIf DaysToRunoutDate < leadTimeInDays + 14 Then
                e.Cell.ForeColor = Color.Blue
            End If
        End If
    End Sub
    Protected Sub btnInventoryUsage_Click(sender As Object, e As System.EventArgs)
        Client_NewWindow("ReportServer.aspx?RPTPATH=/Non-Linked Reports/Streamline Inventory Usage")
    End Sub

End Class
