Imports DevExpress.Web
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Partial Class BracketStandards
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
		Select Case Request.HttpMethod
			Case "POST"
				If IsPostBack Then
					grdMain.DataBind()
				End If
			Case "GET"
				Dim da As SqlDataAdapter = New SqlDataAdapter(dsStandardTypes.SelectCommand, vConnStr)
				Dim dt As DataTable = New DataTable()
				da.Fill(dt)
				ddlStandardTypes.DataSource = dt
				ddlStandardTypes.DataTextField = "StandardType"
				ddlStandardTypes.DataValueField = "StandardTypeID"
				ddlStandardTypes.DataBind()

				ckShowInactiveBracketStandards.Checked = False

				grdMain.DataBind()
		End Select
	End Sub

	Protected Sub grdMain_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
		Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
	End Sub

	Protected Sub grdMain_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grdMain.InitNewRow
		e.NewValues("Active") = True
	End Sub

End Class
