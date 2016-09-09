
Imports System.Data.OleDb
Imports System.Data
'Imports Microsoft.Office.Interop.Word

Partial Class I_LIFE_PRG_LI_CLM_PART_MATURE
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


    Protected Sub cmdClaimNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClaimNoGet.Click
        'clear all fields first
        ClearAllFields()
        'by ClaimNo(CLAIMNUM)
        If txtClaimsNo.Text <> "" Then
            lblMsg.Text = SPIL_PRG_LI_PART_MATURITY_GET_CLAIMS_RPTD(txtClaimsNo.Text.Trim())

            If lblMsg.Text = "Claims paid does not exist!" Then
                FirstMsg = "Javascript:alert('" + lblMsg.Text + "'. Claims has not been reported)"

                '    recalcClaimsCbx.Visible = True
                '    Exit Sub
                'Else
                FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"

            End If

        Else
            lblMsg.Text = "Enter Claim #, to retrieve reported claims!"
            FirstMsg = "Javascript:alert('" + lblMsg.Text + "')"
            txtClaimsNo.Focus()
            Exit Sub
        End If
    End Sub

    Public Function SPIL_PRG_LI_PART_MATURITY_GET_CLAIMS_RPTD(ByVal pQueryValue As String) As String
        'Dim pQUERY_TYPE As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_PRG_LI_PART_MATURITY_GET_CLAIMS"
        cmd.CommandType = CommandType.StoredProcedure
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
                txtAssuredName.Text = CType(objOledr("ASSURED NAME") & vbNullString, String)

                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT")) Then
                    txtPolicyStartDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_FROM_DT"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOledr("TBIL_CLM_RPTD_POLY_TO_DT")) Then
                    txtPolicyEndDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_POLY_TO_DT"), DateTime), "dd/MM/yyyy")
                End If

                DdnClaimType.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE") & vbNullString, String).Trim()
                DdnSysModule.SelectedValue = CType(objOledr("TBIL_CLM_RPTD_MDLE") & vbNullString, String).Trim()

                If DdnClaimType.SelectedValue = 2 Then
                    _rtnMessage = MOVEDATA_FROM_CLAIMRPTD_TO_CLAIMPAID(RTrim(CType(objOledr("TBIL_CLM_RPTD_MDLE") & vbNullString, String)), _
                                                           RTrim(CType(objOledr("TBIL_CLM_RPTD_POLY_NO") & vbNullString, String)), _
                                                           RTrim(CType(objOledr("TBIL_CLM_RPTD_CLM_NO") & vbNullString, String)), _
                                                           RTrim(CType(objOledr("TBIL_CLM_RPTD_PRDCT_CD") & vbNullString, String)), _
                                                           RTrim(CType(objOledr("TBIL_CLM_RPTD_UNDW_YR") & vbNullString, String)), _
                                                           RTrim(CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE") & vbNullString, String)), _
                                                           Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyStartDate.Text)), _
                                                           Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtPolicyEndDate.Text)))
                    rbnPayOptions.Enabled = True

                Else
                    lblMsg.Text = "PARTIAL MATURITY CALCULATION IS NOT APPLICABLE TO THIS CLAIM TYPE!"
                    FirstMsg = "javascript:alert('" + lblMsg.Text + " Click button CALCULATE CLAIMS!');"
                    rbnPayOptions.Enabled = False
                    Exit Function
                End If





                If _rtnMessage = "Claims not calculated!" Then
                    lblMsg.Text = _rtnMessage
                    FirstMsg = "javascript:alert('" + lblMsg.Text + " Click button CALCULATE CLAIMS!');"

                    'lblPartialPayment.Visible = True
                    'rbnPayOptions.Visible = True
                    'btnCalcClaims.Visible = True
                    'btnReCalcClaims.Visible = False

                ElseIf _rtnMessage = "Re-calculate claims!" Then
                    lblMsg.Text = _rtnMessage
                    FirstMsg = "javascript:alert('" + lblMsg.Text + " Click button CALCULATE CLAIMS!');"

                    'lblPartialPayment.Visible = True
                    'rbnPayOptions.Visible = True
                    'btnCalcClaims.Visible = False
                    'btnReCalcClaims.Visible = True
                ElseIf _rtnMessage = "PARTIAL MATURITY CALCULATION IS NOT APPLICABLE TO THIS CLAIM TYPE!" Then
                    lblMsg.Text = _rtnMessage
                    FirstMsg = "javascript:alert('" + lblMsg.Text + " Click button CALCULATE CLAIMS!');"
                    Exit Function
                End If
            Else
                _rtnMessage = "Claims record does not exist!"
            End If
            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try

        Return _rtnMessage
    End Function
    Public Function MOVEDATA_FROM_CLAIMRPTD_TO_CLAIMPAID(ByVal systemModule As String, ByVal claimNumber As String, ByVal polyNumber As String, _
                                                               ByVal uwy As String, ByVal prodCode As String, _
                                                              ByVal claimType As String, ByVal polyStartDate As DateTime, _
                                                              ByVal polyEndDate As DateTime) As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn1 As OleDbConnection
        conn1 = New OleDbConnection(mystrConn)
        Dim cmd1 As OleDbCommand = New OleDbCommand()
        cmd1.Connection = conn1
        cmd1.CommandText = "SPIL_MOVEDATA_FROM_CLAIMRPTD_TO_CLAIMPAID"
        cmd1.CommandType = CommandType.StoredProcedure

        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_MDLE", systemModule)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_CLM_NO", claimNumber)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_POLY_NO", polyNumber)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_PRDCT_CD", prodCode)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_UNDW_YR", uwy)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_CLM_TYP", claimType)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_POLY_FROM_DT", polyStartDate)
        cmd1.Parameters.AddWithValue("@pTBIL_CLM_PAID_POLY_TO_DT", polyEndDate)

        Try
            conn1.Open()
            Dim objOledr1 As OleDbDataReader
            objOledr1 = cmd1.ExecuteReader()
            If (objOledr1.Read()) Then

                'Dim claimType_ As String
                'claimType_ = CType((objOledr1("TBIL_CLM_PAID_CLM_TYP") & vbNullString), String)
                'If claimType_ = "2" Then

                Dim paidAmtLc As String = CType((objOledr1("TBIL_CLM_PAID_AMT_LC") & vbNullString), String)

                If paidAmtLc <> "0.00" Then
                    _rtnMessage = "Re-calculate claims!"
                    Exit Function
                Else
                    _rtnMessage = "Claims not calculated!"
                End If

                'Else
                '    _rtnMessage = "PARTIAL MATURITY CALCULATION IS NOT APPLICABLE TO THIS CLAIM TYPE!"
                '    'Exit Function
                'End If




                'Else
                '    _rtnMessage = "Unable to read data!"
            End If
            conn1.Close()
        Catch ex As Exception
            _rtnMessage = "Error calculating maturity claim! " + ex.Message
        End Try

        Return _rtnMessage

    End Function

    Public Function GET_CALCULATED_CLAIMS(ByVal polyNumber As String, ByVal claimNumber As String, ByVal prodCode As String, _
                                              ByVal payPlan As Int16, ByVal polyStartDate As DateTime, ByVal polyEndDate As DateTime) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_CALC_PART_CLAIM_OR_REJECT_CLAIM"
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.AddWithValue("@pPOLICY_NUMBER", polyNumber)
        cmd.Parameters.AddWithValue("@pCLAIM_NUMBER", claimNumber)
        cmd.Parameters.AddWithValue("@pPRODUCT_CODE", prodCode)
        cmd.Parameters.AddWithValue("@pPARTIAL_PAYMENT", payPlan)
        cmd.Parameters.AddWithValue("@pPOLICY_START_DATE", polyStartDate)
        cmd.Parameters.AddWithValue("@pPOLICY_END_DATE", polyEndDate)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()

            Dim dt As DataTable = New DataTable()
            dt.Load(objOledr)
            For Each dr As DataRow In dt.Rows
                Dim colExist As Boolean = dr.Table.Columns.Contains("MSG")
                If colExist = True Then

                    txtTotalSumAssured.Text = ""
                    txtTotalClaimAmtLC.Text = ""
                    txtPartialSumAssuredPaid.Text = ""

                    btnCalcClaims.Visible = False
                    btnReCalcClaims.Visible = False

                    _rtnMessage = CType(dr("MSG"), String)

                ElseIf colExist = False Then
                    '_rtnMessage = "MSG column does not exist!"
                    txtTotalSumAssured.Text = ""
                    txtTotalClaimAmtLC.Text = ""
                    txtPartialSumAssuredPaid.Text = ""

                    txtTotalSumAssured.Text = Format(CType(dr("TBIL_POL_PRM_SA_LC"), Decimal), "N2")
                    txtTotalClaimAmtLC.Text = Format(CType(dr("TBIL_CLM_PAID_TOT_AMT_LC"), Decimal), "N2")
                    txtPartialSumAssuredPaid.Text = Format(CType(dr("TBIL_CLM_PAID_SA_AMT_LC"), Decimal), "N2")

                    btnReCalcClaims.Visible = True

                    If rbnPayOptions.SelectedIndex = 0 Then
                        _rtnMessage = "1ST PARTIAL PAYMENT CALCULATED!"
                    ElseIf rbnPayOptions.SelectedIndex = 1 Then
                        _rtnMessage = "2ND PARTIAL PAYMENT CALCULATED!"
                    End If

                End If
            Next


            conn.Close()
        Catch ex As Exception
            _rtnMessage = "Error calculating maturity claim! " + ex.Message
        End Try
        Return _rtnMessage

    End Function



    Sub ClearAllFields()
        lblMsg.Text = ""
        'txtClaimsNo.Text = ""
        txtPolicyNumber.Text = ""
        txtPolicyStartDate.Text = ""
        txtPolicyEndDate.Text = ""
        txtProductCode.Text = ""
        txtProductName.Text = ""
        txtAssuredName.Text = ""
        txtUWY.Text = ""
        DdnSysModule.SelectedIndex = 0
        DdnClaimType.SelectedIndex = 0

        txtTotalClaimAmtLC.Text = ""

    End Sub

    Protected Sub btnCalcClaims_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcClaims.Click
        ''CALL METHOD TO CALCULATE CLAIM

        If rbnPayOptions.SelectedValue = 1 Then
            lblMsg.Text = ""
            lblMsg.Text = GET_CALCULATED_CLAIMS(txtPolicyNumber.Text.Trim(), txtClaimsNo.Text.Trim(), txtProductCode.Text.Trim(), CType(rbnPayOptions.SelectedValue.ToString(), Short), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text.Trim())), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text.Trim())))
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        ElseIf rbnPayOptions.SelectedValue = 2 Then
            lblMsg.Text = ""
            lblMsg.Text = GET_CALCULATED_CLAIMS(txtPolicyNumber.Text.Trim(), txtClaimsNo.Text.Trim(), txtProductCode.Text.Trim(), CType(rbnPayOptions.SelectedValue.ToString(), Short), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text.Trim())), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text.Trim())))
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If

    End Sub

    Protected Sub btnReCalcClaims_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReCalcClaims.Click
        ''CALL METHOD TO RE-CALCULATE CLAIM
        If rbnPayOptions.SelectedValue = 1 Then
            'lblMsg.Text = ""
            lblMsg.Text = GET_CALCULATED_CLAIMS(txtPolicyNumber.Text.Trim(), txtClaimsNo.Text.Trim(), txtProductCode.Text.Trim(), CType(rbnPayOptions.SelectedValue.ToString(), Short), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text.Trim())), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text.Trim())))
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        ElseIf rbnPayOptions.SelectedValue = 2 Then
            'lblMsg.Text = ""
            lblMsg.Text = GET_CALCULATED_CLAIMS(txtPolicyNumber.Text.Trim(), txtClaimsNo.Text.Trim(), txtProductCode.Text.Trim(), CType(rbnPayOptions.SelectedValue.ToString(), Short), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text.Trim())), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text.Trim())))
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If


    End Sub

    Protected Sub rbnPayOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnPayOptions.SelectedIndexChanged
        lblMsg.Text = ""
        If rbnPayOptions.SelectedValue = 1 Then
            'lblMsg.Text = ""
            lblMsg.Text = GET_CALCULATED_CLAIMS(txtPolicyNumber.Text.Trim(), txtClaimsNo.Text.Trim(), txtProductCode.Text.Trim(), CType(rbnPayOptions.SelectedValue.ToString(), Short), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text.Trim())), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text.Trim())))
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        ElseIf rbnPayOptions.SelectedValue = 2 Then
            'lblMsg.Text = ""
            lblMsg.Text = GET_CALCULATED_CLAIMS(txtPolicyNumber.Text.Trim(), txtClaimsNo.Text.Trim(), txtProductCode.Text.Trim(), CType(rbnPayOptions.SelectedValue.ToString(), Short), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text.Trim())), Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text.Trim())))
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If

    End Sub



End Class
