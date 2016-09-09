
Imports System.Data.OleDb
Imports System.Data
Imports NHibernate.Hql.Ast.ANTLR
Imports System.Runtime.Remoting.Proxies

Partial Class I_LIFE_PRG_LI_CLM_MATURE
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

    Protected strP_TYPE As String
    Protected strP_DESC As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""

    Dim myarrData() As String

    Dim strErrMsg As String

    Dim basicLc As Decimal
    Dim basicFc As Decimal
    Dim addLc As Decimal
    Dim addFc As Decimal
    Dim newDateToDb As Date

    Shared _rtnMessage As String


    Protected Sub chkClaimNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkClaimNum.CheckedChanged
        If chkClaimNum.Checked Then
            txtClaimsNo.Enabled = True
            cmdClaimNoGet.Enabled = True
        Else
            txtClaimsNo.Enabled = False
            cmdClaimNoGet.Enabled = True

        End If
    End Sub

    Protected Sub cmdClaimNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClaimNoGet.Click
        'clear all fields first
        ClearAllFields()
        'by ClaimNo(CLAIMNUM)
        If txtClaimsNo.Text <> "" Then
            lblMsg.Text = GET_CLMS_RPTD_MATURITY("CLAIMNUM", txtClaimsNo.Text.Trim())

            If lblMsg.Text = "Claims paid does not exist!" Then
                FirstMsg = "Javascript:return confirm('" + lblMsg.Text + "')"

                'FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
                recalcClaimsCbx.Visible = True
                Exit Sub
            Else

            End If

        Else
            lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            txtClaimsNo.Focus()
            Exit Sub
        End If

    End Sub

    Public Function GET_CLMS_RPTD_MATURITY(ByVal pQueryType As String, ByVal pQueryValue As String) As String
        'Dim pQUERY_TYPE As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_CLMS_RPTD_MATURITY"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@pQUERY_TYPE", pQueryType)
        cmd.Parameters.AddWithValue("@pQUERY_VALUE", pQueryValue)


        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                strErrMsg = "true"

                txtClaimsNo.Text = RTrim(CType(objOledr("TBIL_CLM_RPTD_CLM_NO") & vbNullString, String))
                txtPolicyNumber.Text = RTrim(CType(objOledr("TBIL_CLM_RPTD_POLY_NO") & vbNullString, String))
                txtUWY.Text = CType(objOledr("TBIL_CLM_RPTD_UNDW_YR") & vbNullString, String).Trim()
                txtProductCode.Text = CType(objOledr("TBIL_CLM_RPTD_PRDCT_CD") & vbNullString, String)
                txtProductName.Text = CType(objOledr("TBIL_PRDCT_DTL_DESC") & vbNullString, String)


                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT")) Then
                    txtPolicyStartDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_TO_DT")) Then
                    txtPolicyEndDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_TO_DT"), DateTime), "dd/MM/yyyy")
                End If

                DdnClaimType.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE") & vbNullString, String).Trim()
                'FirstMsg=CType(objOledr("TBIL_CLM_RPTD_MDLE") & vbNullString, String).Trim()
                DdnSysModule.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_MDLE") & vbNullString, String).Trim()

                Dim sDate As String = CType(CType(Now.ToShortDateString(), Date), String)
                Dim sDate1 = sDate.Split(CType("/", Char))
                txtClaimsCalculatedDate.Text = sDate1(1) + "/" + sDate1(0) + "/" + sDate1(2)

                Dim _rtnMsg = "Claims record retrieved!"
                'If _rtnMsg = "Claims record retrieved!" Then  
                Dim res As String = MOVESELECTDATA_FROM_CLAIMRPTD_TO_CLAIMPAID(RTrim(CType(objOledr("TBIL_CLM_RPTD_MDLE") & vbNullString, String)), _
                                                                 RTrim(CType(objOledr("TBIL_CLM_RPTD_POLY_NO") & vbNullString, String)), _
                                                                 RTrim(CType(objOledr("TBIL_CLM_RPTD_CLM_NO") & vbNullString, String)), _
                                                                 RTrim(CType(objOledr("TBIL_CLM_RPTD_PRDCT_CD") & vbNullString, String)), _
                                                                 RTrim(CType(objOledr("TBIL_CLM_RPTD_UNDW_YR") & vbNullString, String)), _
                                                                 RTrim(CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE") & vbNullString, String)), _
                                                                 Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text)), _
                                                                 Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text)), _
                                                                 Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsCalculatedDate.Text)))


                _rtnMessage = res

                'End If

            Else
                _rtnMessage = "Claims record does not exist!"
            End If
            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try

        Return _rtnMessage
    End Function

    Sub GetCalcClaim(ByVal policyNumber As String)

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_PRG_LI_CLM_MATURE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@pTBIL_CLM_PAID_POLY_NO", policyNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                txtContributionClaimsLC.Text = Format(CType(objOledr("TBIL_CLM_PAID_CONTRIB_AMT_LC"), Decimal), "N2")
                txtContributionClaimsFC.Text = Format(CType(objOledr("TBIL_CLM_PAID_CONTRIB_AMT_LC"), Decimal), "N2")
                txtLoanBalanceLC.Text = Format(CType(objOledr("TBIL_CLM_PAID_LOAN_BAL_AMT_LC"), Decimal), "N2")
                txtLoanBalanceFC.Text = Format(CType(objOledr("TBIL_CLM_PAID_LOAN_BAL_AMT_LC"), Decimal), "N2")
                txtTotalClaimAmtLC.Text = Format(CType(objOledr("TBIL_CLM_PAID_TOT_AMT_LC"), Decimal), "N2")
                txtTotalClaimAmtFC.Text = Format(CType(objOledr("TBIL_CLM_PAID_TOT_AMT_LC"), Decimal), "N2")


                txtSumAssuredClaimLC.Text = Format(CType(objOledr("TBIL_CLM_PAID_SA_AMT_LC"), Decimal), "N2")
                txtSumAssuredClaimFC.Text = Format(CType(objOledr("TBIL_CLM_PAID_SA_AMT_LC"), Decimal), "N2")
                txtBonusClaimsLC.Text = Format(CType(objOledr("TBIL_CLM_PAID_BONUS_AMT_LC"), Decimal), "N2")
                txtBonusClaimsFC.Text = Format(CType(objOledr("TBIL_CLM_PAID_BONUS_AMT_LC"), Decimal), "N2")



            End If
            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try



    End Sub

    Public Function MOVESELECTDATA_FROM_CLAIMRPTD_TO_CLAIMPAID(ByVal systemModule As String, ByVal claimNumber As String, ByVal polyNumber As String, _
                                                                ByVal uwy As String, ByVal prodCode As String, _
                                                               ByVal claimType As String, ByVal polyStartDate As DateTime, _
                                                               ByVal polyEndDate As DateTime, ByVal claimsCalcDate As DateTime) As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn1 As OleDbConnection
        conn1 = New OleDbConnection(mystrConn)
        Dim cmd1 As OleDbCommand = New OleDbCommand()
        cmd1.Connection = conn1
        cmd1.CommandText = "SPIL_GET_CLAIM_PAID"
        cmd1.CommandType = CommandType.StoredProcedure

        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_MDLE", systemModule)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_CLM_NO", claimNumber)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_POLY_NO", polyNumber)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_PRDCT_CD", prodCode)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_UNDW_YR", uwy)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_CLM_TYP", claimType)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_POLY_FROM_DT", polyStartDate)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_POLY_TO_DT", polyEndDate)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_DUE_DATE", claimsCalcDate)
        cmd1.Parameters.AddWithValue("@TBIL_CLM_PAID_CONTRIB_AMT_LC", 0.00)

        Try
            conn1.Open()
            Dim objOledr1 As OleDbDataReader
            objOledr1 = cmd1.ExecuteReader()
            If (objOledr1.Read()) Then

                Dim contribAmtLc As String = CType(objOledr1("TBIL_CLM_PAID_CONTRIB_AMT_LC") & vbNullString, String)

                If contribAmtLc <> "0.00" Then
                    lblMsg.Text = "Maturity already calculated!, click 'Re-Calculate' to recalculate maturity!"
                    FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
                    recalcClaimsCbx.Visible = True

                    GetCalcClaim(CType((objOledr1("TBIL_CLM_PAID_POLY_NO") & vbNullString), String))

                    Exit Function
                Else
                    recalcClaimsCbx.Checked = False
                    recalcClaimsCbx.Visible = False
                    Dim prodCategory As String = CType(objOledr1("TBIL_PRDCT_DTL_CAT") & vbNullString, String).Trim()
                    If prodCategory = "I" Then
                        Call Do_CLM_MATURE_INVESTMENT(CType((objOledr1("TBIL_CLM_PAID_POLY_NO") & vbNullString), String))
                    ElseIf prodCategory = "E" Then
                        Call Do_CLM_MATURE_ENDOWMENT(CType((objOledr1("TBIL_CLM_PAID_POLY_NO") & vbNullString), String), Now.Year, CType(objOledr1("TBIL_CLM_PAID_PRDCT_CD") & vbNullString, String).Trim())
                    End If
                    'lblMsg.Text = "Not a string"
                End If


            Else
                _rtnMessage = "Unable to read data!"
            End If
        Catch ex As Exception
            _rtnMessage = "Error calculating maturity claim! " + ex.Message
        End Try

        Return _rtnMessage

    End Function

    Public Function Do_CLM_MATURE_INVESTMENT(ByVal policyNumber As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn2 As OleDbConnection
        conn2 = New OleDbConnection(mystrConn)
        Dim cmd2 As OleDbCommand = New OleDbCommand()
        cmd2.Connection = conn2
        cmd2.CommandText = "SPIL_PRG_LI_CLM_MATURE_INVESTMENT"
        cmd2.CommandType = CommandType.StoredProcedure

        cmd2.Parameters.AddWithValue("@pPOLICY_NUMBER", policyNumber)
        Try
            conn2.Open()
            Dim objOledr2 As OleDbDataReader
            objOledr2 = cmd2.ExecuteReader()
            If (objOledr2.Read()) Then
                ''CONTRIBUTION_CLAIM LOAN_BALANCE TOTAL_CLAIM_AMOUNT
                If IsNumeric(objOledr2("TBIL_CLM_PAID_CONTRIB_AMT_LC")) Then
                    txtContributionClaimsLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_CONTRIB_AMT_LC") & vbNullString, Decimal), "N2")
                    txtContributionClaimsFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_CONTRIB_AMT_FC") & vbNullString, Decimal), "N2")
                End If

                If IsNumeric(objOledr2("TBIL_CLM_PAID_LOAN_BAL_AMT_LC")) Then
                    txtLoanBalanceLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_LOAN_BAL_AMT_LC") & vbNullString, Decimal), "N2")
                    txtLoanBalanceFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_LOAN_BAL_AMT_FC") & vbNullString, Decimal), "N2")

                End If

                If IsNumeric(objOledr2("TBIL_CLM_PAID_TOT_AMT_LC")) Then
                    txtTotalClaimAmtLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_TOT_AMT_LC") & vbNullString, Decimal), "N2")
                    txtTotalClaimAmtFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_TOT_AMT_FC") & vbNullString, Decimal), "N2")
                End If

                txtSumAssuredClaimLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_SA_AMT_LC"), Decimal), "N2")
                txtSumAssuredClaimFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_SA_AMT_FC"), Decimal), "N2")
                txtBonusClaimsLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_BONUS_AMT_LC"), Decimal), "N2")
                txtBonusClaimsFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_BONUS_AMT_FC"), Decimal), "N2")

                '_rtnMessage = "MATURE_INVESTMENT"
            End If

        Catch ex As Exception
            _rtnMessage = "Error calculating maturity claim! " + ex.Message
        End Try

        Return _rtnMessage
    End Function


    Public Function Do_CLM_MATURE_ENDOWMENT(ByVal policyNumber As String, ByVal bonusYear As Integer, ByVal prodCode As String) As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn2 As OleDbConnection
        conn2 = New OleDbConnection(mystrConn)
        Dim cmd2 As OleDbCommand = New OleDbCommand()
        cmd2.Connection = conn2
        cmd2.CommandText = "SPIL_PRG_LI_CLM_MATURE_ENDOWMENT"
        cmd2.CommandType = CommandType.StoredProcedure

        cmd2.Parameters.AddWithValue("@pPOLICY_NUMBER", policyNumber)
        cmd2.Parameters.AddWithValue("@pPRODUCT_CODE", prodCode)
        cmd2.Parameters.AddWithValue("@pBONUS_RT_YEAR", bonusYear)

        Try
            conn2.Open()
            Dim objOledr2 As OleDbDataReader
            objOledr2 = cmd2.ExecuteReader()
            If (objOledr2.Read()) Then

                If IsNumeric(objOledr2("TBIL_CLM_PAID_CONTRIB_AMT_LC")) Then
                    txtContributionClaimsLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_CONTRIB_AMT_LC") & vbNullString, Decimal), "N2")
                    txtContributionClaimsFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_CONTRIB_AMT_FC") & vbNullString, Decimal), "N2")
                End If

                If IsNumeric(objOledr2("TBIL_CLM_PAID_LOAN_BAL_AMT_LC")) Then
                    txtLoanBalanceLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_LOAN_BAL_AMT_LC") & vbNullString, Decimal), "N2")
                    txtLoanBalanceFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_LOAN_BAL_AMT_FC") & vbNullString, Decimal), "N2")

                End If

                If IsNumeric(objOledr2("TBIL_CLM_PAID_TOT_AMT_LC")) Then
                    txtTotalClaimAmtLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_TOT_AMT_LC") & vbNullString, Decimal), "N2")
                    txtTotalClaimAmtFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_TOT_AMT_FC") & vbNullString, Decimal), "N2")
                End If

                If IsNumeric(objOledr2("TBIL_CLM_PAID_SA_AMT_LC")) Then
                    txtSumAssuredClaimLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_SA_AMT_LC"), Decimal), "N2")
                    txtSumAssuredClaimFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_SA_AMT_FC"), Decimal), "N2")
                End If

                If IsNumeric(objOledr2("TBIL_CLM_PAID_BONUS_AMT_LC")) Then
                    txtBonusClaimsLC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_BONUS_AMT_LC"), Decimal), "N2")
                    txtBonusClaimsFC.Text = Format(CType(objOledr2("TBIL_CLM_PAID_BONUS_AMT_FC"), Decimal), "N2")
                End If
            Else
                _rtnMessage = "Unable to read data!"
                '_rtnMessage = "MATURE_ENDOWMENT"
            End If

        Catch ex As Exception
            _rtnMessage = "Error calculating maturity claim! " + ex.Message
        End Try

        Return _rtnMessage
    End Function
    'method to calculate claims into TBIL_CLAIM_PAID
    Public Function DO_CALC_CLAIMS_INTO_TBIL_CLAIM_PAID() As Boolean



        Return True
    End Function


    Protected Sub txtClaimsNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClaimsNo.TextChanged
        'clear all fields first
        ClearAllFields()
        If txtClaimsNo.Text <> "" Then
            lblMsg.Text = GET_CLMS_RPTD_MATURITY("CLAIMNUM", txtClaimsNo.Text)
            If lblMsg.Text = "Maturity claim paid does not exist!" Then
                FirstMsg = "Javascript:confirm('" + lblMsg.Text + "')"
                Exit Sub
            End If
            'FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
        Else
            lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            txtClaimsNo.Focus()
            Exit Sub
        End If
    End Sub

    Protected Sub txtPolicyNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicyNumber.TextChanged
        'clear all fields first
        ClearAllFields()
        If txtPolicyNumber.Text <> "" Then
            lblMsg.Text = GET_CLMS_RPTD_MATURITY("POLYNUM", txtPolicyNumber.Text)
            If lblMsg.Text = "Claims paid does not exist!" Then
                FirstMsg = "Javascript:return confirm('" + lblMsg.Text + "')"
                'FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
                recalcClaimsCbx.Visible = True
                Exit Sub
            End If
        Else
            lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"

            txtPolicyNumber.Focus()
            Exit Sub
        End If
    End Sub

    Protected Sub chkPolyNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPolyNum.CheckedChanged
        If chkPolyNum.Checked Then
            txtPolicyNumber.Enabled = True
            cmdPolyNoGet.Enabled = True
        Else
            txtPolicyNumber.Enabled = False
            cmdPolyNoGet.Enabled = True
        End If
    End Sub

    Protected Sub cmdPolyNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPolyNoGet.Click

        ClearAllFields()

        'by PolyNo(POLUNUM)
        If txtPolicyNumber.Text <> "" Then
            lblMsg.Text = GET_CLMS_RPTD_MATURITY("POLYNUM", txtPolicyNumber.Text.Trim())
            If lblMsg.Text = "Claims paid does not exist!" Then
                FirstMsg = "Javascript:return confirm('" + lblMsg.Text + "')"
                'FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
                recalcClaimsCbx.Visible = True
                Exit Sub
            End If
        Else
            lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"

            txtPolicyNumber.Focus()
            Exit Sub
        End If
    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged

        ClearAllFields()

        'by PolyNo(POLUNUM)
        lblMsg.Text = GET_CLMS_RPTD_MATURITY("POLYNUM", cboSearch.SelectedValue)
        FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(txtSearch.Value)) <> "" Then

            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()

        End If
    End Sub

    Sub ClearAllFields()
        'txtClaimsNo.Text = ""
        'txtPolicyNumber.Text = ""
        txtPolicyStartDate.Text = ""
        txtPolicyEndDate.Text = ""
        txtProductCode.Text = ""
        txtProductName.Text = ""
        txtUWY.Text = ""
        DdnSysModule.SelectedIndex = 0
        DdnClaimType.SelectedIndex = 0

        txtClaimsCalculatedDate.Text = ""
        txtContributionClaimsLC.Text = ""
        txtContributionClaimsFC.Text = ""
        txtSumAssuredClaimLC.Text = ""
        txtSumAssuredClaimFC.Text = ""
        txtBonusClaimsLC.Text = ""
        txtBonusClaimsFC.Text = ""

        txtTotalClaimAmtLC.Text = ""
        txtTotalClaimAmtFC.Text = ""

    End Sub

    Protected Sub txtClaimsCalculatedDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClaimsCalculatedDate.TextChanged

        'Dim msg = CHECK_IF_CLAIM_EXIST(txtClaimsNo.Text.Trim())
        'lblMsg.Text = msg
        'FirstMsg = "javascript:confirm('" + lblMsg.Text + "')"

    End Sub

    Protected Sub recalcClaimsCbx_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles recalcClaimsCbx.CheckedChanged
        Dim res As String = MOVESELECTDATA_FROM_CLAIMRPTD_TO_CLAIMPAID(RTrim(DdnSysModule.SelectedItem.Value), _
                                                                    RTrim(txtPolicyNumber.Text.Trim()), _
                                                                    RTrim(txtClaimsNo.Text.Trim()), _
                                                                    RTrim(txtProductCode.Text.Trim()), _
                                                                    RTrim(txtUWY.Text), _
                                                                    RTrim(DdnClaimType.SelectedItem.Value), _
                                                                    Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text)), _
                                                                    Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text)), _
                                                                    Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsCalculatedDate.Text)))

    End Sub


    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click

    End Sub
End Class
