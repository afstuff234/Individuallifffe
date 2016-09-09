Imports System.Data
Imports System.Data.OleDb
Partial Class I_LIFE_RPT_LI_OUT_PREMIUM_NOTF
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Dim rParams As String() = {"nw", "nw", "new"}

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("* Select Insured *")
            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()
        End If
    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        initializeFields()
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtPolicyNo.Text = ""
            Else
                txtPolicyNo.Text = Me.cboSearch.SelectedItem.Value
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub
    Private Sub initializeFields()
        txtPolicyNo.Text = String.Empty
    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        If (txtPolicyNo.Text = "") Then
            lblMsg.Text = "Policy Number must not be empty"
            Exit Sub
        End If

        rParams(0) = "rptOutstandingPremLetter"
        rParams(1) = "pPolicyNo="
        rParams(2) = txtPolicyNo.Text + "&"
        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub
End Class
