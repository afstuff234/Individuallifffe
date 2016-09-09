
Partial Class AjaxAccordion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function GetContentFillerText() As String
        Return _
            "ASP.NET AJAX is a free framework for building a new generation of richer, more interactive, highly personalized cross-browser web applications.  " & _
            "This new web development technology from Microsoft integrates cross-browser client script libraries with the ASP.NET 2.0 server-based development framework.  " & _
            "In addition, ASP.NET AJAX offers you the same type of development platform for client-based web pages that ASP.NET offers for server-based pages.  " & _
            "And because ASP.NET AJAX is an extension of ASP.NET, it is fully integrated with server-based services. ASP.NET AJAX makes it possible to easily take advantage of AJAX techniques on the web and enables you to create ASP.NET pages with a rich, responsive UI and server communication.  " & _
            "However, AJAX isn't just for ASP.NET.  " & _
            "You can take advantage of the rich client framework to easily build client-centric web applications that integrate with any backend data provider and run on most modern browsers.  "
    End Function

    Protected Sub Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Label1.Text = "You clicked the " + ((Control) sender).ID + " at " + DateTime.Now.ToLongTimeString() + "."
        Label1.Text = "You clicked the " + sender.ID + " at " + DateTime.Now.ToLongTimeString() + "."
    End Sub

End Class
