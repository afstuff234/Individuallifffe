
Partial Class WCustomPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            'Dim strSvrName As String = Server.MachineName
            Dim currentError As Exception
            ' currentError = Server.GetLastError()
            currentError = CType(Session("err"), Exception)


            'Me.txtError.Text = Request.QueryString("err")
            'Me.txtError.Text = ErrorPage(Err.LastDllError)
            'Me.lblError.Text = "Last Error Code: " & Err.LastDllError.ToString
            'Me.lblError.Text = "Last Error Code: " & currentError.Message.ToString()
            'Me.lblErrMsg.Text = "Error Message: " & currentError.Message.ToString
            'Me.lblErrStack.Text = "Error Stack: " & currentError.ToString

            ' Show error on screen
            'ShowError(currentError)

            Server.ClearError()

        End If

    End Sub

    Public Shared Sub ShowError(ByVal currentError As Exception)
        Dim context As HttpContext = HttpContext.Current

        context.Response.Write( _
         "<h2>Error</h2><hr/>" & _
         "An unexpected error has occurred on this page." & _
         "The system administrators have been notified.<br/>" & _
         "<br/><b>The error occurred in:</b>" & _
           "<pre>" & context.Request.Url.ToString & "</pre>" & _
        "<br/><b>Error Message:</b>" & _
         "<pre>" & currentError.Message.ToString & "</pre>" & _
        "<br/><b>Error Stack:</b>" & _
         "<pre>" & currentError.ToString & "</pre>")
    End Sub

End Class
