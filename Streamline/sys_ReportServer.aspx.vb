Imports DevExpress.Web
Imports System.Reflection
Imports DevExpress.XtraReports.UI
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraReports.Web

Imports Microsoft.VisualBasic
Imports Microsoft.Reporting.WebForms
Imports System.Net

Partial Class sys_ReportServer
    Inherits System.Web.UI.Page

    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If (Not IsPostBack) Then
            rptViewer.ServerReport.ReportServerCredentials = New ReportServerConnection.MyReportServerCredentials()

            rptViewer.ProcessingMode = ProcessingMode.Remote
            rptViewer.ServerReport.ReportPath = Request.QueryString("RPTPATH")
            rptViewer.ServerReport.ReportServerUrl = New Uri(ConfigurationManager.AppSettings("ReportServerURL"))

            Dim p() As String = Split(Request.QueryString("PARMS"), "|")
            If p(0) <> "" Then
                Dim paramList As New Generic.List(Of ReportParameter)
                For i As Integer = 0 To p.Length - 1
                    Dim pa As String() = Split(p(i), "~")
                    paramList.Add(New ReportParameter(pa(0), pa(1)))
                Next

                rptViewer.ServerReport.SetParameters(paramList)
            End If
            rptViewer.ServerReport.Refresh()
        End If
    End Sub
End Class


