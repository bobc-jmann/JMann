Partial Class RecycleLog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
        Select Case Request.HttpMethod
            Case "GET"
                dtEarliestRecycleDate.Value = DateAdd(DateInterval.Month, -2, Now)
        End Select

        LoadRecycleLog()
    End Sub

    Private Sub LoadRecycleLog()
        dsRecycleLog.SelectCommand = "SELECT RecycleLogID, RecycleDate, RL.SectionID, StartCurrent, StartBackup, StartTotal, " & _
                "NumDonors, NumLowMail, NumDonorsLowMail, ProjSizeSizeChangePercent, ProjSizeNonDonorPercent, " & _
                "NewSize, NewSizeFactor, EndCurrent, EndBackup, EndTotal, " & _
                "NumberOfTimesScheduled, FirstMailingDate, LastMailingDate " & _
            "FROM tblRecycleLog AS RL " & _
            "INNER JOIN tblSections AS S ON S.SectionID = RL.SectionID " & _
            "INNER JOIN tblRoutes AS R ON R.RouteID = S.RouteID " & _
            "WHERE RecycleDate >= '" & Format(dtEarliestRecycleDate.Value, "yyyyMMdd") & "' " & _
            "ORDER BY RecycleDate DESC, RouteCode + '-' + SectionCode"
        grid.DataBind()
    End Sub

End Class
