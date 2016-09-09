
Partial Class I_LIFE_PRG_LI_REVIVE_POLICY_RPT
    Inherits System.Web.UI.Page
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new"}

    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click
        Dim str() As String
        '  Dim reportname As String
        If (txtStartDate.Text = "") Then
            Status.Text = "Waiver effective start date must not be empty"
            Exit Sub
        End If
        If (txtEndDate.Text = "") Then
            Status.Text = "Waiver effective end date must not be empty"
            Exit Sub
        End If



        str = DoDate_Process(txtStartDate.Text, txtStartDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Start date, ")
            Status.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            txtStartDate.Focus()
            Exit Sub
        Else
            txtStartDate.Text = str(2).ToString()
        End If

        str = DoDate_Process(txtEndDate.Text, txtEndDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " End date ")
            Status.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            txtEndDate.Focus()
            Exit Sub
        Else
            txtEndDate.Text = str(2).ToString()
        End If

        Dim startDate1 = Convert.ToDateTime(DoConvertToDbDateFormat(txtStartDate.Text))
        Dim endDate1 = Convert.ToDateTime(DoConvertToDbDateFormat(txtEndDate.Text))

        If startDate1 > endDate1 Then
            Status.Text = "Start Date must not be greater than end date"
            Exit Sub
        End If

        Dim startDate = Format(startDate1, "MM/dd/yyyy")
        Dim endDate = Format(endDate1, "MM/dd/yyyy")


        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = "RPT_LI_REVIVE_POLICY"
        rParams(1) = "pStartDate="
        rParams(2) = startDate + "&"
        rParams(3) = "pEndDate="
        rParams(4) = endDate + "&"
        rParams(5) = url

        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
        ' Response.Redirect("PrintView.aspx")
    End Sub

    Protected Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Response.Redirect("PRG_LI_REVIVE_POLICY.aspx")
    End Sub

End Class
