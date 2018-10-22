<%@ Application Language="VB" %>

<script runat="server">
    
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
	Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
		Dim v As String = ""
		' Code that runs when an unhandled error occurs
		Dim ctx As HttpContext = HttpContext.Current
		Dim err As Exception = ctx.Server.GetLastError()
		Dim ex As Exception = Server.GetLastError
		'If (ex.GetType Is GetType(HttpException)) Then
		If Not ex.InnerException Is Nothing Then
			v = ex.InnerException.GetType.ToString
		Else
			v = ""
		End If
		'HttpSession("vUserName").Current.Response.Redirect("~/system/sys_Error.aspx?EM=" & ex.Message & "&EX=" & v & "&PG=" & ctx.Request.Url.ToString())
		'End If
	End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>