
Partial Class I_LIFE_RPT_LI_AGENCY_CODES
    Inherits System.Web.UI.Page
    Dim rParams As String() = {"nw", "nw"}
    Protected FirstMsg As String

    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = "rptAgencyCodes"
        rParams(1) = url
        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub

    Protected Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Response.Redirect("~/I_LIFE/MENU_IL.aspx?menu=IL_UND")
    End Sub
End Class
