Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports CustodianLife.Data
Partial Class I_LIFE_PRG_LI_REQ_BENE_BANK
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    'Protected BufferStr As String

    Protected strStatus As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strF_ID As String
    Protected strQ_ID As String
    Protected strP_ID As String
    Protected strClaim_ID As String

    Protected strP_TYPE As String
    Protected strP_DESC As String
    Protected str_Rec As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""

    Dim myarrData() As String

    Dim strErrMsg As String
    Shared _rtnMessage As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            strF_ID = CType(Request.QueryString("optfileid"), String)
        Catch ex As Exception
            strF_ID = ""
        End Try


        If Not (Page.IsPostBack) Then
            'Call Proc_DoNew()

            Me.lblMsg.Text = "Status:"
            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False


            Try
                strP_TYPE = CType(Request.QueryString("optid"), String)
                strP_DESC = CType(Request.QueryString("optd"), String)
                str_Rec = CType(Request.QueryString("idd"), String)
                strClaim_ID = CType(Request.QueryString("optclaimid"), String)
                Session("strClaim_ID") = strClaim_ID
            Catch ex As Exception
                strP_TYPE = "ERR_TYPE"
                strP_DESC = "ERR_DESC"
            End Try

            If IsNothing(str_Rec) Then
                'brand new record
                txtAction.Text = "New"
            Else
                'get record from an already existing list
                GetBenefDetailsByNumber(str_Rec)
            End If

            If strClaim_ID = "Death" Then
                'change relevant labels to depict correct description
                lblNormName.Text = "Norm. Name"
                lblNormAge.Text = "Norm. Age"
                txtBenefGuardian.Visible = False
                txtPayoutShareAmt.Visible = False
                txtPayoutSharePCent.Visible = False
                lblBenefGardian.Visible = False
                lblBenefShare.Visible = False
                GridView1.Visible = False
                GetBenefDetailsByNumber(strClaim_ID)

            End If


            If Trim(strF_ID) <> "" Then
                Me.txtFileNum.Text = RTrim(strF_ID)
                Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                If oAL.Item(0) = "TRUE" Then
                    Me.txtQuote_Num.Text = oAL.Item(3)
                    Me.txtPolNum.Text = oAL.Item(4)
                    Me.cmdNext.Enabled = True

                    If UCase(oAL.Item(18).ToString) = "A" Then
                        Me.cmdPrint_ASP.Visible = False
                    End If
                    txtClaimNo.Text = CType(Session("ClaimsNo"), String)

                    Call Proc_DataBind_BankRec()
                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If
        End If
    End Sub
    Private Sub Proc_DataBind_BankRec()
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try

        'Try
        strSQL = "SPIL_CLAIM_BENEF_SEARCH_BY_CLAIMNO"

        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
        objDA.SelectCommand.CommandType = CommandType.StoredProcedure
        objDA.SelectCommand.Parameters.Clear()

        objDA.SelectCommand.Parameters.Add("pRefNo", OleDbType.VarChar, 50).Value = Trim(txtClaimNo.Text)
        objDA.SelectCommand.Parameters.Add("pRefType", OleDbType.VarChar, 10).Value = "CLA"
        objDA.SelectCommand.Parameters.Add("pDebug", OleDbType.VarChar, 1).Value = 0

        Dim objDS As DataSet = New DataSet()
        objDA.Fill(objDS)
        With GridView1
            .DataSource = objDS
            .DataBind()
        End With

        objDS.Dispose()

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()

        objDS = Nothing
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub
    Sub ClaerAllFields()
        txtPolNum.Text = ""
        '     txtClaimsNo.Text = ""
        txtBankAccountName.Text = String.Empty
        txtBankAccountNo.Text = String.Empty
        txtBankName.Text = String.Empty
        txtSortCode.Text = String.Empty
        cmbAccountType.SelectedIndex = 0



    End Sub
    

    'Private Function AddNewBankInfo(ByVal fileNumber As String, ByVal proposalNumber As String, ByVal polNumber As String, ByVal claimNo As String, _
    '                                 ByVal bankAccountNo As String, ByVal bankAccountName As String, ByVal bankName As String, ByVal bankSortCode As String, _
    '                                 ByVal bankAccountType As String, ByVal settlementStatus As String, ByVal benefName As String, ByVal GuardianName As String, _
    '                                 ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String) As String

    '    Dim mystrConn As String = CType(Session("connstr"), String)
    '    Dim conn As OleDbConnection
    '    conn = New OleDbConnection(mystrConn)
    '    Dim cmd As OleDbCommand = New OleDbCommand()
    '    cmd.Connection = conn
    '    cmd.CommandText = "SPIL_INS_BENEFICIARY_BANK_DETAILS"
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_FILE_NO", fileNumber)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_PROPOSAL_NO", proposalNumber)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_POLY_NO", polNumber)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_CLM_NO", claimNo)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_ACCOUNT_NO", bankAccountNo)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_ACCOUNT_NAME", bankAccountName)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_NAME", bankName)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_SORT_CODE", bankSortCode)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_ACCOUNT_TYPE", bankAccountType)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_SETTELEMENT_STATUS", settlementStatus)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENEFICIARY_NAME", benefName)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_GUARDIAN_NAME", GuardianName)

    '    cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FLAG", flag)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_KEYDTE", dDate)
    '    cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_OPERID", userId)

    '    Try
    '        conn.Open()
    '        Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
    '        adapter.SelectCommand = cmd

    '        Dim ds As DataSet = New DataSet()
    '        adapter.Fill(ds)
    '        conn.Close()

    '        Dim dt As DataTable = ds.Tables(0)
    '        For Each dr As DataRow In dt.Rows
    '            Dim msg = dr("Msg").ToString()
    '            If msg = 1 Then
    '                '_rtnMessage = "Entry Successful, with CLAIM NUMBER: " + claimNo + " generated!"
    '                _rtnMessage = "Entry Successful!"
    '            Else
    '                _rtnMessage = "Entry failed, record already exist!"
    '            End If
    '        Next
    '    Catch ex As Exception
    '        _rtnMessage = "Entry failed! " + ex.Message.ToString()
    '    End Try


    '    Return _rtnMessage

    'End Function

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Dim str() As String
        Dim flag As String
        strClaim_ID = CType(Session("strClaim_ID"), String)
        'Checking fields for empty values
        If txtPolNum.Text = "" Then
            lblMsg.Text = ""
        End If

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim rc As ClaimBeneficiaryRepository = New ClaimBeneficiaryRepository
        If txtAction.Text = "New" Then
            If strClaim_ID = "Death" Then
                flag = "N" 'the norminee flag code
            Else
                flag = "A"
            End If
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)


            lblMsg.Text = rc.AddNewBankInfo_Repo(txtFileNum.Text.ToString.Trim(), txtQuote_Num.Text.ToString.Trim(), _
                                          txtPolNum.Text.Trim(), txtClaimNo.Text.Trim(), _
                                          txtBankAccountNo.Text.Trim(), txtBankAccountName.Text.Trim() _
                                          , txtBankName.Text.Trim(), txtSortCode.Text.Trim(), cmbAccountType.SelectedValue.Trim(), _
                                          String.Empty, txtBeneficiaryName.Text.Trim(), txtAge.Text, txtPayoutSharePCent.Text, txtPayoutShareAmt.Text, _
                                          txtPayOut.Text, txtBenefGuardian.Text.Trim, _
                                          flag, dateAdded, operatorId, mystrConn)

            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        Else
            If strClaim_ID = "Death" Then
                flag = "N" 'the norminee flag code
            Else
                flag = "C"
            End If
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)
            lblMsg.Text = rc.AddNewBankInfo_Repo(txtFileNum.Text.ToString.Trim(), txtQuote_Num.Text.ToString.Trim(), _
                                          txtPolNum.Text.Trim(), txtClaimNo.Text.Trim(), _
                                          txtBankAccountNo.Text.Trim(), txtBankAccountName.Text.Trim() _
                                          , txtBankName.Text.Trim(), txtSortCode.Text.Trim(), cmbAccountType.SelectedValue.Trim(), _
                                          String.Empty, txtBeneficiaryName.Text.Trim(), txtAge.Text, txtPayoutSharePCent.Text, txtPayoutShareAmt.Text, _
                                          txtPayOut.Text, txtBenefGuardian.Text.Trim, _
                                          flag, dateAdded, operatorId, mystrConn)
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If
        rc = Nothing


        Proc_DataBind_ClaimsRec()
        'InitializeClaimFields()

    End Sub

    Protected Sub cmdDelItem_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelItem_ASP.Click
        Dim str() As String



        If txtAction.Text = "Delete" Then
            Dim mystrConn As String = CType(Session("connstr"), String)
            Dim rc As ClaimBeneficiaryRepository = New ClaimBeneficiaryRepository
            Dim flag As String = "D"
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)

            lblMsg.Text = rc.AddNewBankInfo_Repo(txtFileNum.Text.ToString.Trim(), txtQuote_Num.Text.ToString.Trim(), _
                                          txtPolNum.Text.Trim(), txtClaimNo.Text.Trim(), _
                                          txtBankAccountNo.Text.Trim(), txtBankAccountName.Text.Trim() _
                                          , txtBankName.Text.Trim(), txtSortCode.Text.Trim(), cmbAccountType.SelectedValue.Trim(), _
                                          String.Empty, txtBeneficiaryName.Text.Trim(), txtAge.Text, txtPayoutSharePCent.Text, txtPayoutShareAmt.Text, _
                                          txtPayOut.Text, txtBenefGuardian.Text.Trim, _
                                          flag, dateAdded, operatorId, mystrConn)
            rc = Nothing
        End If

    End Sub

    Private Sub Proc_DataBind_ClaimsRec()
        'txtTotalPremPaid.Text = 0.0
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try

        'Try
        strSQL = "SPIL_SEARCH_FROM_BENEFICIARYBANK"

        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
        objDA.SelectCommand.CommandType = CommandType.StoredProcedure
        objDA.SelectCommand.Parameters.Clear()

        objDA.SelectCommand.Parameters.Add("pRefNo", OleDbType.VarChar, 50).Value = Trim(txtClaimNo.Text)
        objDA.SelectCommand.Parameters.Add("pBenefName", OleDbType.VarChar, 10).Value = " "
        objDA.SelectCommand.Parameters.Add("pDebug", OleDbType.VarChar, 1).Value = 0

        Dim objDS As DataSet = New DataSet()
        objDA.Fill(objDS)
        With GridView1
            .DataSource = objDS
            .DataBind()
        End With

        objDS.Dispose()

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()

        objDS = Nothing
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub

    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Dim pvURL As String = ""
        pvURL = "PRG_LI_REQ_ENTRY.aspx?optfileid=" & Trim(Me.txtFileNum.Text)

        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        pvURL = pvURL & "&optclaimid=" & Trim(Me.txtClaimNo.Text)
        Response.Redirect(pvURL)
    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim pvURL As String = "#?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub
    Private Function GetBenefDetailsByNumber(ByVal strParam As String) As String

        Dim RefNumber As String = String.Empty
        Dim BenefName As String = String.Empty
        Dim NormineeSwitch As Integer = 0
        Dim strParams As String() = strParam.Split(",")

        If strParams.Length > 1 Then
            RefNumber = strParams(0)
            BenefName = strParams(1)
        Else
            NormineeSwitch = 1
            RefNumber = CType(Request.QueryString("optclaimkey"), String)
            BenefName = String.Empty
        End If
        'Dim rtnString As String

        If txtClaims_No_sv.Text = "" Or txtClaims_No_sv.Text.Trim.Length = 0 Then
            str_Rec = Nothing
            Dim mystrConn As String = CType(Session("connstr"), String)
            Dim conn As OleDbConnection
            conn = New OleDbConnection(mystrConn)
            Dim cmd As OleDbCommand = New OleDbCommand()
            cmd.Connection = conn
            cmd.CommandText = "SPIL_SEARCH_FROM_BENEFICIARYBANK"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("pRefNo", RefNumber)
            cmd.Parameters.AddWithValue("@pBenefName", BenefName)
            cmd.Parameters.AddWithValue("pDebug", NormineeSwitch)


            Try
                conn.Open()
                Dim objOledr As OleDbDataReader
                objOledr = cmd.ExecuteReader()
                If (objOledr.Read()) Then
                    strErrMsg = "true"

                    txtPolNum.Text = RTrim(CType(objOledr("TBIL_CLM_BENE_BANK_POLY_NO") & vbNullString, String))
                    txtBankAccountName.Text = CType(objOledr("TBIL_CLM_BENE_BANK_ACCOUNT_NAME") & vbNullString, String)
                    txtBankAccountNo.Text = CType(objOledr("TBIL_CLM_BENE_BANK_ACCOUNT_NO") & vbNullString, String)
                    txtBankName.Text = CType(objOledr("TBIL_CLM_BENE_BANK_NAME") & vbNullString, String)
                    txtBenefGuardian.Text = CType(objOledr("TBIL_CLM_BENE_BANK_GUARDIAN_NAME") & vbNullString, String)
                    txtBeneficiaryName.Text = CType(objOledr("TBIL_CLM_BENEFICIARY_NAME") & vbNullString, String)
                    txtFileNum.Text = CType(objOledr("TBIL_CLM_BENE_BANK_FILE_NO") & vbNullString, String)
                    txtQuote_Num.Text = CType(objOledr("TBIL_CLM_BENE_BANK_PROPOSAL_NO") & vbNullString, String)
                    txtSortCode.Text = CType(objOledr("TBIL_CLM_BENE_BANK_SORT_CODE") & vbNullString, String)
                    txtClaimNo.Text = CType(objOledr("TBIL_CLM_BENE_BANK_CLM_NO") & vbNullString, String)
                    txtAge.Text = CType(objOledr("TBIL_CLM_BENEFICIARY_AGE") & vbNullString, String)
                    txtPayOut.Text = CType(objOledr("TBIL_CLM_BENE_BANK_PAYOUT") & vbNullString, String)
                    txtPayoutSharePCent.Text = CType(objOledr("TBIL_CLM_BENEFICIARY_SHARE_PCNT") & vbNullString, String)
                    txtPayoutShareAmt.Text = CType(objOledr("TBIL_CLM_BENEFICIARY_SHARE") & vbNullString, String)
                    cmbAccountType.SelectedItem.Value = CType(objOledr("TBIL_CLM_BENE_BANK_ACCOUNT_TYPE") & vbNullString, String)

                    txtPayoutShareAmt.Text = FormatNumber((Convert.ToDecimal(txtPayOut.Text) * Convert.ToDecimal(txtPayoutSharePCent.Text)) / 100, 2)
                    txtClaims_No_sv.Text = txtClaimNo.Text

                    Session("ClaimsNo") = txtClaimNo.Text.Trim()

                    _rtnMessage = "Claims record retrieved!"

                Else
                    _rtnMessage = "Unable to retrieve record. Invalid CLAIM NUMBER!"
                End If
                conn.Close()
            Catch ex As Exception
                _rtnMessage = "Error retrieving data! " + ex.Message
            End Try
            ' strStatus = Proc_DoOpenRecord(RTrim("POL"), Me.txtPolNum.Text, RTrim("0"))
            ' Me.cmdNext.Enabled = True
        End If
        Proc_DataBind_BankRec()
        Return _rtnMessage
    End Function

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
