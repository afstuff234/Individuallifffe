
Partial Class I_LIFE_PRG_LI_INDV_RECEIPT
    Inherits System.Web.UI.Page
    Protected FirstMsg As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then

        End If
        Try
            txtReceiptNo.Text = CType(Request.QueryString("idd"), String)
        Catch ex As Exception
            ' strF_ID = ""
        End Try
    End Sub

    Protected Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Response.Redirect("PRG_LI_INDV_ENQUIRY.aspx")
    End Sub

    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click
        If Len(Trim(txtReceiptNo.Text)) = 0 Then
            FirstMsg = "javascript:alert('Error! Please enter a valid receipt number')"
        Else
            Session("rcPrintNo") = txtReceiptNo.Text
            Response.Redirect("ReceiptPrint.aspx")
        End If
    End Sub
End Class
