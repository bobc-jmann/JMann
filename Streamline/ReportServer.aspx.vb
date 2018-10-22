Imports Microsoft.Reporting.WebForms
Imports System.Windows.Forms

Partial Class ReportServer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (reportDisplayed.Value <> "True" And screenHeight.Value <> "") Then
            rptViewer.ProcessingMode = ProcessingMode.Remote
            rptViewer.ServerReport.ReportServerCredentials = New ReportServerConnection.MyReportServerCredentials()

            rptViewer.ServerReport.ReportPath = Request.QueryString("RPTPATH")
            rptViewer.ServerReport.ReportServerUrl = New Uri(ConfigurationManager.AppSettings("ReportServerURL"))

            rptViewer.Height = screenHeight.Value - 50
            rptViewer.Visible = True

            Dim p() As String = Split(Request.QueryString("PARMS"), "|")
            If p(0) <> "" Then
                Dim paramList As New Generic.List(Of ReportParameter)
                For i As Integer = 0 To p.Length - 1
                    ' Split parameter name from value(s)
                    Dim pa As String() = Split(p(i), "~")
                    ' Split values into an array
                    Dim pam() As String = Split(pa(1), ",")
                    paramList.Add(New ReportParameter(pa(0), pam))
                Next

                rptViewer.ServerReport.SetParameters(paramList)
            End If

            rptViewer.ServerReport.Refresh()
            reportDisplayed.Value = "True"
            loading.Visible = False
        End If
    End Sub
End Class
