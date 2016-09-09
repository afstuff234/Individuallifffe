
Partial Class WNoAccess
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strval = Request.QueryString("strERR")
            Me.lblmsg.Text = strval
        Catch ex As Exception
            Me.lblmsg.Text = ""

        End Try

    End Sub
End Class
