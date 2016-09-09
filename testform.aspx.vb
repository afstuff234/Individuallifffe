
Partial Class testform
    Inherits System.Web.UI.Page

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
    End Sub

    Protected Sub butTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTest.Click
        'ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Your Massage');", True)


        Dim message As String = "Is this an Old (Premia) Policy?"
        'ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Your Massage');", True)
        'ClientScript.RegisterStartupScript(GetType(Page), "Popup", "confirm('" + (message + "', doSomething);"), True)

        'ClientScript.RegisterStartupScript(Me.GetType, "ff", "alert('foo');", False)
        ClientScript.RegisterStartupScript(GetType(Page), "Popup", "confirm('" + (message + "', doSomething);"), True)
        'ClientScript.RegisterStartupScript(GetType(Page), "Popup", "<script type='text/javascript'>confirm('Delete elementsyyy?', doSomething);</script>", False)
        'ClientScript.RegisterStartupScript(GetType(Page), "Popup", "<script type='text/javascript'>doSomething();</script>", False)

    End Sub
End Class
