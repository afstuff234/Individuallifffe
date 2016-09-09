Imports System.Data
Imports System.Data.OleDb
Partial Class I_LIFE_RPT_GRP_QuotationSlip
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean
    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new", "new", "new", "new"}

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        lblMsg.Text = ""
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("GL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If
    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        lblMsg.Text = ""
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
            Else
                'Dim selectedText = Me.cboSearch.SelectedItem.Text ''''
                'Dim ReturnText = Split(selectedText, "-")
                'txtProposalNo.Text = Trim(ReturnText(2))
                txtFileNo.Text = cboSearch.SelectedValue
                Get_Grp_ProposalNo(txtFileNo.Text)
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
       Clear()
    End Sub

    Protected Sub Clear()
        txtProposalNo.Text = ""
        txtFileNo.Text = ""
        txtBatchNo.Text = ""
        lblMsg.Text = ""
    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        ErrorInd = ""
        lblMsg.Text = ""
        ValidateControls(ErrorInd)
        If ErrorInd = "Y" Then
            Exit Sub
        End If
        ' To avoid document with no proposal number

        'If Session("ProposalNo_Retrieved") <> txtProposalNo.Text Then
        '    Me.lblMsg.Text = "Record not found for Proposal No: " & Me.txtProposalNo.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        blnStatus = Proc_DoGet_Record(Trim(Me.txtProposalNo.Text), Trim(Me.txtFileNo.Text), Trim(txtBatchNo.Text))

        If blnStatus = False Then
            Exit Sub
        End If

        rParams(0) = "rptQuotationSlip"
        rParams(1) = "PARAM_PROP_NUM="
        rParams(2) = txtProposalNo.Text + "&"
        rParams(3) = "PARAM_FILE_NUM="
        rParams(4) = txtFileNo.Text + "&"
        rParams(5) = "PARAM_BATCH_NUM="
        rParams(6) = txtBatchNo.Text + "&"
        rParams(7) = "PARAM_MODULE="
        rParams(8) = "G&"

        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub
    Private Sub ValidateControls(ByRef ErrorInd As String)
        If (txtProposalNo.Text = String.Empty) Then
            lblMsg.Text = "Proposal number must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtFileNo.Text = String.Empty) Then
            lblMsg.Text = "File number must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtBatchNo.Text = String.Empty) Then
            lblMsg.Text = "Batch number must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If Not (IsNumeric(txtBatchNo.Text)) Then
            lblMsg.Text = "Batch number must be numeric"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
    End Sub

    Private Function Proc_DoGet_Record(ByVal pvProNo As String, ByVal pvFileNo As String, ByVal pvQuoNo As String) As Boolean
        lblMsg.Text = ""
        blnStatusX = False

        Dim mystrCONN_Chk As String = ""

        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing
        Dim objOLEDR_Chk As OleDbDataReader

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""


        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            blnStatusX = False
            Return blnStatusX
            Exit Function
        End Try

        Try

            strTable = strTableName

            strSQL = ""
            strSQL = "SPGL_GET_QUOTATION_INVOICE"

            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            ''objOLECmd_Chk.CommandTimeout = 180
            objOLECmd_Chk.CommandType = CommandType.StoredProcedure

            objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 40).Value = LTrim(RTrim(pvProNo))
            objOLECmd_Chk.Parameters.Add("p02", OleDbType.VarChar, 40).Value = LTrim(RTrim(pvFileNo))
            objOLECmd_Chk.Parameters.Add("p03", OleDbType.VarChar, 3).Value = LTrim(RTrim(pvQuoNo))
            objOLECmd_Chk.Parameters.Add("p04", OleDbType.VarChar, 3).Value = RTrim("G")

            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                ' Session("ProposalNo_Retrieved") = Trim(CType(objOLEDR_Chk("TBIL_ANN_POLY_PROPSAL_NO") & vbNullString, String))
                blnStatusX = True
            Else
                blnStatusX = False
                Me.lblMsg.Text = "Record not found for proposal No: " & Me.txtProposalNo.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Function
            End If

        Catch ex As Exception
            blnStatusX = False
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message.ToString()
        End Try
        objOLEDR_Chk = Nothing

        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
        Return blnStatusX
    End Function

    Public Sub Get_Grp_ProposalNo(ByVal fileNo As String)
        lblMsg.Text = ""
        Dim mystrCONN_Chk As String = ""

        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing
        Dim objOLEDR_Chk As OleDbDataReader

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        myTmp_Chk = "N"
        myTmp_Ref = ""


        mystrCONN_Chk = CType(Session("connstr"), String)
        objOLEConn_Chk = New OleDbConnection()
        objOLEConn_Chk.ConnectionString = mystrCONN_Chk

        Try
            'open connection to database
            objOLEConn_Chk.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn_Chk = Nothing
            blnStatusX = False
            Exit Sub
        End Try

        Try
            strTable = strTableName
            strSQL = ""
            strSQL = "SELECT TBIL_POLY_PROPSAL_NO FROM TBIL_GRP_POLICY_DET WHERE TBIL_POLY_FILE_NO='" & fileNo & "'"
            objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
            objOLECmd_Chk.CommandType = CommandType.Text

            objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
            If (objOLEDR_Chk.Read()) Then
                txtProposalNo.Text = Trim(CType(objOLEDR_Chk("TBIL_POLY_PROPSAL_NO") & vbNullString, String))
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message.ToString()
        End Try
        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing
    End Sub
End Class
