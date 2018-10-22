Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Data

Partial Class UserMaint
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ismt(Session("vUserName")) Then ja("Please login.") : rd("~/Login.aspx") : re()
    End Sub

    Protected Sub grdMain_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grdMain.InitNewRow
        e.NewValues("Active") = True
    End Sub

    Protected Sub grdEffectiveMenuItems_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Protected Sub grdMenuGroups_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Protected Sub grdMenuItems_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Protected Sub grdRegions_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("MasterID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub
 
    Protected Sub grdUserRegions_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) 'Handles grdUserRegions.InitNewRow
        e.NewValues("DefaultRegion") = False
    End Sub

	Protected Sub grdMain_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
		e.NewValues("Notes") = GetMemoText(CType(sender, ASPxGridView))
	End Sub

    Protected Sub grdMain_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs)
        e.NewValues("Notes") = GetMemoText(CType(sender, ASPxGridView))
    End Sub

    Protected Function GetMemoText(ByVal grdMain As ASPxGridView) As String
        Dim pageControl As ASPxPageControl = TryCast(grdMain.FindEditFormTemplateControl("pageControl"), ASPxPageControl)
        Dim memo As ASPxMemo = TryCast(pageControl.FindControl("notesEditor"), ASPxMemo)
        Return memo.Text
    End Function

    Private grdRegions As ASPxGridView

    Protected Sub grdRegions_Init(ByVal sender As Object, ByVal e As EventArgs)
        grdRegions = CType(sender, ASPxGridView)
    End Sub

    Protected Sub CreateAllRegions_Click(sender As Object, e As EventArgs)
        Dim conn As SqlConnection = New SqlConnection(vConnStr)
        Dim myCmd As SqlCommand = New SqlCommand()
        myCmd.Connection = conn
        myCmd.CommandText = "[users].[spRegions_CreateAll]"
        myCmd.CommandType = System.Data.CommandType.StoredProcedure

		myCmd.Parameters.Add(DataUtil.CreateParameter("@userID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, Session("MasterID")))
		myCmd.Parameters.Add(DataUtil.CreateParameter("@currentUserID", System.Data.ParameterDirection.Input, System.Data.DbType.Int32, Session("vUserID")))
        Dim errorID As Integer = 0
		myCmd.Parameters.Add(DataUtil.CreateParameter("@RETURN_VALUE", System.Data.ParameterDirection.ReturnValue, SqlDbType.Int, errorID))

        Try
            conn.Open()
            myCmd.ExecuteNonQuery()
            errorID = myCmd.Parameters("@RETURN_VALUE").Value
            If errorID > 0 Then
				vbHandleProgramError(errorID, "UserMaint, CreateAllRegions_Click")
            End If
        Catch ex As Exception
			LogProgramError(ex.Message, "", ex.StackTrace, "Notify User", "UserMaint, CreateAllRegions_Click")
        Finally
            conn.Close()
        End Try

        grdRegions.DataBind()
    End Sub
End Class
