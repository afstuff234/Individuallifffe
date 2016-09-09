
Partial Class I_LIFE_PRG_LI_INDV_REPORTS
    Inherits System.Web.UI.Page
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles butOK.Click
        Dim reportname As String

        Dim sStartDate As String = ConvertMyDate(txtStartDate.Text)
        Dim sEndDate As String = ConvertMyDate(txtEndDate.Text)


        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        reportname = rblTransType.SelectedValue.Trim
        rParams(0) = rblTransType.SelectedValue.Trim
        rParams(1) = "pStartDate="
        rParams(2) = sStartDate.Trim + "&"
        rParams(3) = "pEndDate="
        rParams(4) = sEndDate.Trim + "&"
        rParams(5) = url


        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub
    Public Function ConvertMyDate(ByVal inDate As String) As String
        Dim myDateArray As Array = inDate.Split("/")
        Dim myNewDate As String = myDateArray(1) & "/" & myDateArray(0) & "/" & myDateArray(2)
        ConvertMyDate = myNewDate
    End Function

    Protected Sub butClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles butClose.Click
        'Response.Redirect("MENU_IL.aspx")
        Response.Redirect("MENU_IL.aspx?menu=IL_UND")
    End Sub
End Class
