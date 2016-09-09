
Partial Class I_LIFE_PRG_LI_CLM_PART_MATURE_RPT
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new"}


    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click
        If pFilterOption.SelectedIndex <> 0 Then


            'rParams(0) = rblTransType.SelectedValue.Trim

            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
            rParams(0) = "PRG_LI_CLM_PART_MATURE"
            rParams(1) = "pTBIL_CLM_PAID_POLY_NO="
            rParams(2) = txtPolyNumber.Text.Trim + "&"
            rParams(3) = "pTBIL_CLM_FIRST_SECOND_PAYMENT="
            rParams(4) = pPayOption.SelectedValue + "&"
            rParams(5) = url
            'rParams(4) = endDate.Trim + "&"
            'rParams(5) = "pFilterOption="
            'rParams(6) = pFilterOption.SelectedValue + "&"

            Session("ReportParams") = rParams
            Response.Redirect("~/PrintView.aspx")

        Else
            Dim errMsg = "Javascript:alert('Select a filter option!');"
            Label1.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            FirstMsg = errMsg
            pFilterOption.Focus()
            Exit Sub
        End If
    End Sub


End Class
