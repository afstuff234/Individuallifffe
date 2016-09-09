Imports System.Web.Configuration


Partial Class WFileNotFound
    Inherits System.Web.UI.Page

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error

    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'MyBase.OnInit(e)
        'AddHandler Me.Load, AddressOf Page_Load
        'AddHandler Me.Error, AddressOf Page_Error


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim CX As Integer = 0

        If Not Page.IsPostBack Then
            'Dim strSvrName As String = Server.MachineName
            Dim objCurrentError As System.Exception
            objCurrentError = New System.Exception
            objCurrentError = Server.GetLastError()

            Dim objContext As HttpContext = HttpContext.Current

            'Me.txtError.Text = Request.QueryString("err")
            'Me.txtError.Text = ErrorPage(Err.LastDllError)
            'Me.lblError.Text = "Last Error Code: " & Err.LastDllError.ToString
            'Me.lblErrURL.Text = "The Error occurred in: " & objContext.Request.Url.ToString

            'Me.lblErrMsg.Text = "Error Message: " & Server.GetLastError.Message.ToString
            'Me.lblErrMsg.Text = "Error Message: " & objCurrentError.Message.ToString()
            'Me.lblErrMsg.Text = "Error Message: " & "URL or resource not found..."
            'Me.lblErrStack.Text = "Error Stack: " & objCurrentError.ToString


            'Dim currentError As Exception = Server.GetLastError()

            ' Write error to log file if not already done by AppException
            'If Not (TypeOf currentError Is AppException) Then
            '    AppException.LogError(currentError.Message.ToString)
            'End If

            ' Show error on screen
            'ShowError(currentError)


            ' Clear error so that it does not buble up to Application Level
            Server.ClearError()
            objCurrentError = Nothing
            objContext = Nothing

        End If




        ''The following code example demonstrates how to use the CustomError class. 

        ''<customErrors mode="RemoteOnly"
        ''  defaultRedirect="customerror.htm">
        ''    <error statusCode="404" redirect="customerror404.htm"/>
        ''</customErrors>

        '' Get the Web application configuration.
        ''Dim myConfiguration As System.Configuration.Configuration = _
        ''WebConfigurationManager.OpenWebConfiguration("/aspnetTest")

        'Dim myConfiguration As System.Configuration.Configuration = _
        'WebConfigurationManager.OpenWebConfiguration("/web.config")

        '' Get the section.
        'Dim myCustomErrors As CustomErrorsSection = _
        ' CType(myConfiguration.GetSection("system.web/customErrors"), CustomErrorsSection)

        'With myCustomErrors

        'End With
        '' Get the collection.
        'Dim myCustomErrorsCollection As CustomErrorCollection = myCustomErrors.Errors
        ''Response.Write("<br>Error Count: " & myCustomErrorsCollection.Count)
        ''For CX = 0 To myCustomErrorsCollection.Count - 1
        ''    Response.Write("<br>Item: " & CX)
        ''Next

        'With myCustomErrorsCollection

        'End With
        ''For Each myCustomError As CustomError In myCustomErrorsCollection
        ''    Response.Write("<br>Error code: " & myCustomError.StatusCode.ToString)
        ''Next

        'With myCustomErrorsCollection

        'End With

        '' Get second error StatusCode.
        ''Dim currentError1 As CustomError
        ''Dim currentStatusCode As Integer
        ''For CX = 0 To myCustomErrorsCollection.Count - 1
        ''    currentError1 = myCustomErrorsCollection.Item(CX)
        ''    currentStatusCode = currentError1.StatusCode
        ''    Response.Write("<br>Error item : " & CX)

        ''Next

        '' Set the second error StatusCode.
        ''currentError1.StatusCode = 404

        ''Response.Write("<br>Error Info. ")

    End Sub

    Public Shared Sub ShowError(ByVal objCurrentErr As Exception)
        Dim objContext As HttpContext = HttpContext.Current

        objContext.Response.Write( _
         "<h2>Error</h2><hr/>" & _
         "An unexpected error has occurred on this page." & _
         "The system administrators have been notified.<br/>" & _
         "<br/><b>The error occurred in:</b>" & _
           "<pre>" & objContext.Request.Url.ToString & "</pre>" & _
         "<br/><b>Error Message:</b>" & _
           "<pre>" & objCurrentErr.Message.ToString & "</pre>" & _
         "<br/><b>Error Stack:</b>" & _
          "<pre>" & objCurrentErr.ToString & "</pre>")
    End Sub

    Public Shared Sub ShowErrorN(ByVal currentError As Exception)
        Dim context As HttpContext = HttpContext.Current

        context.Response.Write( _
         "<link rel=""stylesheet"" href=""/ThePhileVB/Styles/ThePhile.css"">" & _
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
