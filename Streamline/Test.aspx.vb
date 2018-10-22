Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.Web

Partial Class Test
	Inherits Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	Public Sub fileManager_CustomThumbnail(ByVal sender As Object, ByVal e As FileManagerThumbnailCreateEventArgs)
		Select Case (CType(e.File, FileManagerFile)).Extension
			Case ".txt"
				e.ThumbnailImage.Url = "Resources/filetypeThumbnails/txt.png"
			Case ".doc"
				e.ThumbnailImage.Url = "Resources/filetypeThumbnails/doc.png"
			Case ".xls"
				e.ThumbnailImage.Url = "Resources/filetypeThumbnails/xls-121.png"
			Case ".xlsx"
				e.ThumbnailImage.Url = "Resources/filetypeThumbnails/Excel2013FileIcon.png"
			Case ".xlsm"
				e.ThumbnailImage.Url = "Resources/filetypeThumbnails/xlsm-1758.png"
			Case ".bat"
				e.ThumbnailImage.Url = "Resources/filetypeThumbnails/exec.png"
		End Select
	End Sub
End Class
