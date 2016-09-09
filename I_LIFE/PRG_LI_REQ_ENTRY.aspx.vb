
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports CustodianLife.Data

Partial Class I_LIFE_PRG_LI_REQ_ENTRY
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
    Protected str_Rec As String
    Protected str_Prv_Rec As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strTmp_Value As String = ""

    Dim myarrData() As String

    Dim strErrMsg As String

    Dim totalPremPaidlc As Decimal
    Dim totalPremPaidfc As Decimal
    Dim premOutsdlc As Decimal
    Dim premOutsdfc As Decimal
    Dim premDuelc As Decimal
    Dim premDuefc As Decimal
    Dim addFc As Decimal
    Dim newDateToDb As Date
    Dim rowID As Long
    Shared _rtnMessage As String

    Dim GenEnd_Date, GenStart_Date, CurrentDate, CoverEndDate, Renew_Date, GracePeriodDate As Date
    Dim Eff_Date As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'load loss type into combobox
        If Not IsPostBack Then
            LoadLossTypeCmb()
        End If

        CurrentDate = Convert.ToDateTime(DoConvertToDbDateFormat(Format(DateTime.Now, "dd/MM/yyyy")))
        If Not (Page.IsPostBack) Then
            Call DoProc_CreateDataSource("IL_PRODUCT_CAT_LIST", Trim("I"), Me.cboProductClass)
            Me.lblMsg.Text = "Status:"
        End If

        strTableName = "TBIL_INS_CLASS"

        Try
            strP_TYPE = CType(Request.QueryString("optid"), String)
            strP_DESC = CType(Request.QueryString("optd"), String)
            str_Rec = CType(Request.QueryString("idd"), String)
            str_Prv_Rec = CType(Request.QueryString("optclaimid"), String)

        Catch ex As Exception
            strP_TYPE = "ERR_TYPE"
            strP_DESC = "ERR_DESC"
        End Try

        STRPAGE_TITLE = "Master Codes Setup - " & strP_DESC

        If Trim(strP_TYPE) = "ERR_TYPE" Or Trim(strP_TYPE) = "" Then
            strP_TYPE = ""
        End If

        strP_ID = "L01"
        'rowID = CType(Session("RowID"), Long)
        If IsNothing(str_Rec) Then
            'brand new record
            ' txtAction.Text = "New"
        Else
            'get record from an already existing list
            GetClaimsDetailsByNumber(str_Rec)

        End If
        If IsNothing(str_Prv_Rec) Then
            'do nothing
        Else
            'get old record when navigating backwards
            GetClaimsDetailsByNumber(str_Prv_Rec)


        End If


        If Me.txtAction.Text = "Save" Then
            'Call DoSave()
            'Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete" Then
            'Call DoDelete()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete_Item" Then
            'Call DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub chkClaimNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkClaimNum.CheckedChanged

        If Me.chkClaimNum.Checked Then
            txtClaimsNo.Enabled = True
            cmdClaimNoGet.Enabled = True

            txtAction.Text = "Save"
        Else
            txtClaimsNo.Enabled = False
            cmdClaimNoGet.Enabled = False

            txtAction.Text = ""
        End If

    End Sub

    'Protected Sub chkPolyNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPolyNum.CheckedChanged

    '    'If chkPolyNum.Checked Then
    '    '    txtPolNum.Enabled = True
    '    '    cmdPolyNoGet.Enabled = True

    '    '    txtAction.Text = "New"
    '    'Else
    '    '    txtPolNum.Enabled = False
    '    '    cmdPolyNoGet.Enabled = False

    '    '    txtAction.Text = ""
    '    'End If

    'End Sub



    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSearch.Click
        'If LTrim(RTrim(txtSearch.Value)) = "Search..." Then
        'ElseIf LTrim(RTrim(txtSearch.Value)) <> "" Then
        '    'Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))

        '    Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)

        '    cboSearch.DataSource = dt
        '    cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
        '    cboSearch.DataTextField = "MyFld_Text"
        '    cboSearch.DataBind()

        'End If


        lblMsg.Text = ""
        cboSearch.Items.Clear()
        Dim MyBirthDate As String
        If Trim(Me.txtSearch.Value) = "Search..." Then

        ElseIf Trim(txtDOBSearch.Value) = "dd/mm/yyyy" And Trim(Me.txtSearch.Value) <> "Search..." Then
            Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, Trim(Me.txtSearch.Value))
        Else
            Dim str() As String
            str = DoDate_Process(txtDOBSearch.Value, txtDOB)
            If (str(2) = Nothing) Then
                Dim errMsg = str(0).Insert(18, " Date of Birth, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                lblMsg.Visible = True
                txtDOB.Focus()
                Exit Sub
            Else
                myarrData = Split(Trim(txtDOBSearch.Value), "/")
                MyBirthDate = myarrData(1) & "/" & myarrData(0) & "/" & myarrData(2)
                Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, Trim(Me.txtSearch.Value), MyBirthDate)
            End If
        End If


    End Sub

    Sub LoadLossTypeCmb()

        'Dim ds As DataSet = GetAllLossTypeCode()
        'Dim dt As DataTable = ds.Tables(0)
        'Dim dr As DataRow = dt.NewRow()

        'dr("TBIL_COD_LONG_DESC") = "-- Selecct --"
        'dr("TBIL_COD_ITEM") = ""
        'dt.Rows.InsertAt(dr, 0)

        'DdnLossType.DataSource = dt
        'DdnLossType.DataTextField = "TBIL_COD_LONG_DESC"
        'DdnLossType.DataValueField = "TBIL_COD_ITEM"
        'DdnLossType.DataBind()

    End Sub

    Sub ClaerAllFields()
        txtPolNum.Text = ""
        txtClaimsNo.Text = ""
        txtNotificationDate.Text = ""
        txtClaimsEffectiveDate.Text = ""
        txtTotalPremPaid.Text = ""
        txtPremOutstanding.Text = ""
        DdnLossType.SelectedIndex = 0
        DdnClaimType.SelectedIndex = 0
        txtProductDec.Text = ""
        



    End Sub
    Sub LoadProductsDescCmb()
        'Dim dsProd As DataSet = GetAllProductCodeList()
        'Dim dt As DataTable = ds.Tables(0)

        'ddnProductDesc.DataSource = dsProd.Tables(0)
        'ddnProductDesc.DataTextField = "TBIL_PRDCT_DTL_DESC"
        'ddnProductDesc.DataValueField = "TBIL_PRDCT_DTL_PLAN_CD"
        'ddnProductDesc.DataBind()

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboSearch.SelectedIndexChanged
        'clear fields
        ClaerAllFields()
        'Try
        '    If cboSearch.SelectedIndex = -1 Or cboSearch.SelectedIndex = 0 Or cboSearch.SelectedItem.Value = "" Or cboSearch.SelectedItem.Value = "*" Then

        '    Else
        '        Dim cboValue As String = cboSearch.SelectedItem.Value
        '        ' strStatus = GetPolicyDetailsByNumber(cboValue.Trim())
        '    End If
        'Catch ex As Exception
        '    lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        'End Try

        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtFileNum.Text = ""
                Me.txtQuote_Num.Text = ""
                Me.txtPolNum.Text = ""
                'Me.txtSearch.Value = ""
            Else
                Me.txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    

    Private Function GetClaimsDetailsByNumber(ByVal RefNumber As String) As String
        Dim RefNo As String = String.Empty
        Dim RefID As Long = 0
        Dim RefArray As String() = RefNumber.Split(",")
        If RefArray.Length > 1 Then
            RefNo = RefArray(0)
            RefID = RefArray(1)
        Else
            RefNo = RefArray(0)
            RefID = CType(Session("RowID"), Long)
        End If

       

        If txtClaims_No_sv.Text = "" Or txtClaims_No_sv.Text.Trim.Length = 0 Then
            str_Rec = Nothing
            Dim mystrConn As String = CType(Session("connstr"), String)
            Dim conn As OleDbConnection
            conn = New OleDbConnection(mystrConn)
            Dim cmd As OleDbCommand = New OleDbCommand()
            cmd.Connection = conn
            cmd.CommandText = "SPIL_CLAIMSNUM_SEARCH_FRM_TABLE"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("pRefNo", RefNo)
            cmd.Parameters.AddWithValue("pRecID", RefID)
            cmd.Parameters.AddWithValue("pRefType", "CLA")
            cmd.Parameters.AddWithValue("pDebug", 0)


            Try
                conn.Open()
                Dim objOledr As OleDbDataReader
                objOledr = cmd.ExecuteReader()
                If (objOledr.Read()) Then
                    strErrMsg = "true"

                    txtPolNum.Text = RTrim(CType(objOledr("TBIL_CLM_RPTD_POLY_NO") & vbNullString, String))
                    txtProductClass.Text = CType(objOledr("TBIL_CLM_RPTD_PRDCT_CD") & vbNullString, String)
                    'txtProductCode0.Text = CType(objOledr("TBIL_PRDCT_DTL_DESC") & vbNullString, String)



                    If IsDate(objOledr("TBIL_CLM_RPTD_NOTIF_DT")) Then
                        txtNotificationDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_NOTIF_DT"), DateTime), "dd/MM/yyyy")
                    End If
                    If IsDate(objOledr("TBIL_CLM_RPTD_LOSS_DT")) Then
                        txtClaimsEffectiveDate.Text = Format(CType(objOledr("TBIL_CLM_RPTD_LOSS_DT"), DateTime), "dd/MM/yyyy")
                    End If

                    txtTotalPremPaid.Text = Format(CType(objOledr("TBIL_CLM_RPTD_TOTAL_PREM_PAID_LC"), Decimal), "N2")

                    DdnClaimType.SelectedItem.Text = CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE") & vbNullString, String)
                    Session("ClaimType") = CType(objOledr("TBIL_CLM_RPTD_CLM_TYPE") & vbNullString, String)
                    DdnLossType.SelectedItem.Text = CType(objOledr("TBIL_CLM_RPTD_LOSS_TYPE") & vbNullString, String)
                    txtProductDec.Text = CType(objOledr("TBIL_CLM_RPTD_DESC") & vbNullString, String)
                    ddnClaimSettle.SelectedItem.Text = CType(objOledr("TBIL_CLM_RPTD_SETTLEMENT_STATUS") & vbNullString, String)
                    txtTotalPaidOut.Text = Format(CType(objOledr("TBIL_CLM_RPTD_TOTAL_PAID_OUT_LC"), Decimal), "N2")
                    txtClaimsNo.Text = CType(objOledr("TBIL_CLM_RPTD_CLM_NO") & vbNullString, String)
                    txtClaims_No_sv.Text = txtClaimsNo.Text

                    Session("SettlementStatus") = CType(objOledr("TBIL_CLM_RPTD_SETTLEMENT_STATUS") & vbNullString, String)
                    Session("LossStatus") = CType(objOledr("TBIL_CLM_RPTD_LOSS_TYPE") & vbNullString, String)
                    Session("LossDate") = Format(CType(objOledr("TBIL_CLM_RPTD_LOSS_DT"), DateTime), "dd/MM/yyyy")
                    Session("RowID") = CType(objOledr("TBIL_CLAIM_REPTED_REC_ID"), Long)
                    Session("EntryDate") = CType(objOledr("TBIL_CLM_RPTD_KEYDTE"), DateTime)
                    Session("ClaimsNo") = txtClaimsNo.Text.Trim()

                    If (txtTotalPremPaid.Text <> "" Or txtTotalPremPaid.Text = "0.00") And txtPremDueH.Value <> String.Empty Then
                        premOutsdlc = Convert.ToDouble(txtPremDueH.Value) - Convert.ToDouble(txtTotalPremPaid.Text)
                        txtPremOutstanding.Text = Format(premOutsdlc.ToString(), "N2")
                    End If
                    txtAction.Text = "Change"
                    _rtnMessage = "Claims record retrieved!"

                Else
                    _rtnMessage = "Unable to retrieve record. Invalid CLAIM NUMBER!"
                End If
                conn.Close()
            Catch ex As Exception
                _rtnMessage = "Error retrieving data! " + ex.Message
            End Try
            strStatus = Proc_DoOpenRecord(RTrim("POL"), Me.txtPolNum.Text, RTrim("0"))
            Me.cmdNext.Enabled = True
        End If
        Return _rtnMessage
    End Function
    'Protected Sub cmdPolyNoGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdPolyNoGet.Click
    '    'If txtPolNum.Text <> "" Then
    '    '    ClearFormControls()
    '    '    lblMsg.Text = GetPolicyDetailsByNumber(txtPolicyNum.Text.Trim())
    '    '    FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
    '    'Else
    '    '    lblMsg.Text = "Policy number field cannot be empty!"
    '    '    FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
    '    'End If
    'End Sub
    Sub ClearFormControls()
        txtNotificationDate.Text = ""
        txtClaimsEffectiveDate.Text = ""
        txtTotalPremPaid.Text = ""
        txtPremOutstanding.Text = ""
        DdnClaimType.SelectedIndex = 0
        DdnLossType.SelectedIndex = 0
        txtProductDec.Text = ""
        txtAction.Text = String.Empty

    End Sub
    Protected Sub cmdClaimNoGet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClaimNoGet.Click
        If txtClaimsNo.Text <> "" Then
            ClearFormControls()
            lblMsg.Text = GetClaimsDetailsByNumber(txtClaimsNo.Text.Trim())
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        Else
            lblMsg.Text = "Claims number field cannot be empty!"
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If
    End Sub


    Private Function AddNewClaimsRequest(ByVal rowID As Integer, ByVal fileNumber As String, ByVal proposalNumber As String, ByVal polNumber As String, ByVal claimNo As String, _
                       ByVal productCode As String, ByVal lossType As String, _
                       ByVal notificationDate As DateTime, ByVal claimEffectiveDate As DateTime, ByVal totalPremPaidlc As Decimal, _
                       ByVal totalPremPaidfc As Decimal, ByVal premDuelc As Decimal, ByVal premDuefc As Decimal, ByVal premOutsdlc As Decimal, ByVal premOutsdfc As Decimal, _
                       ByVal claimDescription As String, ByVal dob As DateTime, ByVal lossStatus As String, ByVal totalPaidOut As Decimal, ByVal settlementStatus As String, _
                       ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_INS_CLAIMSREQUEST"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_REC_ID", rowID)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FILE_NO", fileNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PROPOSAL_NO", proposalNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_NO", polNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_NO", claimNo)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PRDCT_CD", productCode)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_TYPE", lossType)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_NOTIF_DT", notificationDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_DT", claimEffectiveDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_TOTAL_PREM_PAID_LC", totalPremPaidlc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_TOTAL_PREM_PAID_FC", totalPremPaidfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PREMIUM_DUE_LC", premDuelc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PREMIUM_DUE_FC", premDuefc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PREMIUM_OUTSTD_LC", premOutsdlc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PREMIUM_OUTSTD_FC", premOutsdfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_DESC", claimDescription)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_BIRTH_DT", dob)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_TYPE", lossStatus)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_TOTAL_PAID_OUT_LC", totalPaidOut)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_TOTAL_PAID_OUT_FC", totalPaidOut)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLAIM_SETTLEMENT_STATUS", settlementStatus)

        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FLAG", flag)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_KEYDTE", dDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_OPERID", userId)

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd

            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()

            Dim dt As DataTable = ds.Tables(0)
            For Each dr As DataRow In dt.Rows
                Dim msg = dr("Msg").ToString()
                If msg = 1 Then
                    '_rtnMessage = "Entry Successful, with CLAIM NUMBER: " + claimNo + " generated!"
                    _rtnMessage = "Entry Successful!"
                Else
                    _rtnMessage = "Entry failed, record already exist!"
                End If
            Next
        Catch ex As Exception
            _rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try


        Return _rtnMessage

    End Function

    Private Function ChangeClaims(ByVal rowID As Integer, ByVal fileNumber As String, ByVal proposalNumber As String, ByVal polNumber As String, ByVal claimNo As String, _
                       ByVal productCode As String, ByVal lossType As String, _
                       ByVal notificationDate As DateTime, ByVal claimEffectiveDate As DateTime, ByVal totalPremPaidlc As Decimal, _
                       ByVal totalPremPaidfc As Decimal, ByVal premDuelc As Decimal, ByVal premDuefc As Decimal, ByVal premOutsdlc As Decimal, ByVal premOutsdfc As Decimal, _
                       ByVal claimDescription As String, ByVal dob As DateTime, ByVal lossStatus As String, ByVal totalPaidOut As Decimal, ByVal settlementStatus As String, _
                       ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String) As String

        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_INS_CLAIMSREQUEST"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_REC_ID", rowID)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FILE_NO", fileNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PROPOSAL_NO", proposalNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_POLY_NO", polNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_NO", claimNo)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PRDCT_CD", productCode)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLM_TYPE", lossType)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_NOTIF_DT", notificationDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_DT", claimEffectiveDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_TOTAL_PREM_PAID_LC", totalPremPaidlc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_TOTAL_PREM_PAID_FC", totalPremPaidfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PREMIUM_DUE_LC", premDuelc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PREMIUM_DUE_FC", premDuefc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PREMIUM_OUTSTD_LC", premOutsdlc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_PREMIUM_OUTSTD_FC", premOutsdfc)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_DESC", claimDescription)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_BIRTH_DT", dob)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_LOSS_TYPE", lossStatus)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_TOTAL_PAID_OUT_LC", totalPaidOut)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_TOTAL_PAID_OUT_FC", totalPaidOut)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_CLAIM_SETTLEMENT_STATUS", settlementStatus)

        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FLAG", flag)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_KEYDTE", dDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_OPERID", userId)

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd

            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()

            Dim dt As DataTable = ds.Tables(0)
            For Each dr As DataRow In dt.Rows
                Dim msg = dr("Msg").ToString()
                If msg = 1 Then
                    '_rtnMessage = "Entry Successful, with CLAIM NUMBER: " + claimNo + " generated!"
                    _rtnMessage = "Entry Successful!"
                Else
                    _rtnMessage = "Operation Successful!"
                End If
            Next
        Catch ex As Exception
            _rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try


        Return _rtnMessage
    End Function

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click

        Dim str() As String

        'Checking fields for empty values
        If txtPolNum.Text = "" Then
            lblMsg.Text = ""
        End If

        If txtNotificationDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtNotificationDate")
            str = MOD_GEN.DoDate_Process(txtNotificationDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Notification date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtNotificationDate.Focus()
                Exit Sub

            Else
                txtNotificationDate.Text = str(2).ToString()
            End If
        Else
            lblMsg.Text = "Notification Date field is required!"
            FirstMsg = lblMsg.Text
            txtNotificationDate.Focus()
            Exit Sub
        End If

        If txtClaimsEffectiveDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtClaimsEffectiveDate")
            str = MOD_GEN.DoDate_Process(txtClaimsEffectiveDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Claims Effective Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtClaimsEffectiveDate.Focus()
                Exit Sub

            Else
                txtClaimsEffectiveDate.Text = str(2).ToString()
            End If
        Else
            lblMsg.Text = "Claims Effective Date field is required!"
            FirstMsg = lblMsg.Text
            txtClaimsEffectiveDate.Focus()
            Exit Sub
        End If




        Dim newNotifDate As Date = Convert.ToDateTime(DoConvertToDbDateFormat(txtNotificationDate.Text))
        Dim newClaimsEffDate As Date = Convert.ToDateTime(DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text))



        If newNotifDate < Convert.ToDateTime(DoConvertToDbDateFormat(txtCommenceDate.Text)) _
        Or newNotifDate > Convert.ToDateTime(DoConvertToDbDateFormat(txtMaturityDate.Text)) Then
            Dim errMsg = "Notification date should be within policy start and end date!"
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            FirstMsg = errMsg
            txtNotificationDate.Focus()
            Exit Sub
        End If

        If txtNotificationDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtNotificationDate")
            str = MOD_GEN.DoDate_Process(txtNotificationDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Notification date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtNotificationDate.Focus()
                Exit Sub

            Else
                txtNotificationDate.Text = str(2).ToString()
            End If

        End If

        If txtClaimsEffectiveDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtClaimsEffectiveDate")
            str = MOD_GEN.DoDate_Process(txtClaimsEffectiveDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Claims Effective Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtClaimsEffectiveDate.Focus()
                Exit Sub

            Else
                txtClaimsEffectiveDate.Text = str(2).ToString()
            End If

        End If


        If txtTotalPremPaid.Text = "" Or txtTotalPremPaid.Text = "0.00" Then
            lblMsg.Text = "Total Premium Paid LC field is required!"
            txtTotalPremPaid.Focus()
            Exit Sub
        Else
            totalPremPaidlc = Convert.ToDecimal((txtTotalPremPaid.Text).Replace(",", ""))
            totalPremPaidfc = totalPremPaidlc
            txtPremOutstanding.Text = Format(CDbl(txtTotalPremDue.Text) - CDbl(totalPremPaidlc), "Standard")
        End If



        If txtPremOutstanding.Text = "" Then
            lblMsg.Text = "Outstanding Premium field is required!"
            txtPremOutstanding.Focus()
            Exit Sub
        Else
            premOutsdlc = Convert.ToDecimal((txtPremOutstanding.Text).Replace(",", ""))
            premOutsdfc = premOutsdlc
        End If

        'If DdnClaimType.SelectedIndex = 0 Then
        '    lblMsg.Text = "Claims Type field is required!"
        '    DdnClaimType.Focus()
        '    Exit Sub
        'End If

        'If DdnLossType.SelectedIndex = 0 Then
        '    lblMsg.Text = "Loss Type field is required!"
        '    DdnLossType.Focus()
        '    Exit Sub
        'End If


        'If txtProductDec.Text = "" Then
        '    lblMsg.Text = "Product Description field is required!"
        '    txtProductDec.Focus()
        '    Exit Sub
        'End If

        Session("TotalPayout") = txtTotalPaidOut.Text
        If txtAction.Text = "New" Then
            Dim flag As String = "A"
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)
            Dim rc As ReceiptsRepository = New ReceiptsRepository()
            'get new claim number
            Dim newClaimNo As String = rc.GetNextSerialNumber("CLM", "002", "001", dateAdded.Year, "CLM-", "12", "11")
            txtClaimsNo.Text = newClaimNo
            lblMsg.Text = AddNewClaimsRequest(0, txtFileNum.Text.ToString.Trim(), txtQuote_Num.Text.ToString.Trim(), _
                                          txtPolNum.Text.Trim(), txtClaimsNo.Text.Trim(), _
                                          txtProductClass.Text.Trim(), DdnClaimType.SelectedItem.Text.Trim(), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text)), _
                                          Convert.ToDecimal(totalPremPaidlc), Convert.ToDecimal(totalPremPaidlc), _
                                          Convert.ToDecimal(premDuelc), Convert.ToDecimal(premDuelc), premOutsdlc, premOutsdlc, _
                                          Convert.ToString(txtProductDec.Text), Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtDOB.Text)), _
                                          Convert.ToString(DdnLossType.SelectedItem.Text.Trim), Convert.ToDecimal(txtTotalPaidOut.Text.Trim), ddnClaimSettle.SelectedItem.Text.Trim(), flag, dateAdded, operatorId)
            rc = Nothing



            Dim cr As ClaimBeneficiaryRepository = New ClaimBeneficiaryRepository
            Dim mystrConn As String = CType(Session("connstr"), String)

            Dim P As Integer = 0

            'Write dummy bank details for all the beneficiaries (if any)
            If GridView2.Rows.Count > 0 Then
                For P = 0 To Me.GridView2.Rows.Count - 1

                    lblMsg.Text = cr.AddNewBankInfo_Repo(txtFileNum.Text.ToString.Trim(), txtQuote_Num.Text.ToString.Trim(), _
                                                 txtPolNum.Text.Trim(), txtClaimsNo.Text.Trim(), _
                                                 String.Empty, String.Empty _
                                                 , String.Empty, String.Empty, String.Empty, _
                                                 String.Empty, GridView2.Rows(P).Cells(3).Text, GridView2.Rows(P).Cells(5).Text, _
                                                 GridView2.Rows(P).Cells(6).Text, "0", txtTotalPaidOut.Text, GridView2.Rows(P).Cells(8).Text, _
                                                 flag, dateAdded, operatorId, mystrConn)
                Next
            End If

            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        Else
            'Session("SettlementStatus") = CType(objOledr("TBIL_CLM_RPTD_SETTLEMENT_STATUS") & vbNullString, String)
            'Session("LossStatus") = CType(objOledr("TBIL_CLM_RPTD_LOSS_TYPE") & vbNullString, String)
            'Session("LossDate") = Format(CType(objOledr("TBIL_CLM_RPTD_LOSS_DT"), DateTime), "dd/MM/yyyy")
            'Session("RowID") = CType(objOledr("TBIL_CLM_RPTD_REC_ID") & vbNullString, String)

            Dim settlementStatus As String = CType(Session("SettlementStatus"), String)
            Dim LossStatus As String = CType(Session("LossStatus"), String)
            Dim LossDate As DateTime = CType(Session("LossDate"), DateTime)
            Dim rowid As Integer = 0
            Dim dateAdded As DateTime = Now


            Dim claimType As String = CType(Session("ClaimType"), String)
            Dim rc As ReceiptsRepository = New ReceiptsRepository()
            If DdnClaimType.SelectedItem.Text <> claimType Then
                Dim newClaimNo As String = rc.GetNextSerialNumber("CLM", "002", "001", dateAdded.Year, "CLM-", "12", "11")
                txtClaimsNo.Text = newClaimNo
                dateAdded = Now
                rowid = 0
            ElseIf (ddnClaimSettle.SelectedItem.Text <> settlementStatus) Or _
                   (DdnLossType.SelectedItem.Text <> LossStatus) Or (txtClaimsEffectiveDate.Text <> LossDate) Then

                dateAdded = Now
                rowid = 0

            Else
                rowid = CType(Session("RowID"), Integer)
                dateAdded = CType(Session("EntryDate"), DateTime)
            End If
            Dim flag As String = "C"
            Dim operatorId As String = CType(Session("MyUserIDX"), String)
            lblMsg.Text = ChangeClaims(rowid, txtFileNum.Text.ToString.Trim(), txtQuote_Num.Text.ToString.Trim(), _
                                          txtPolNum.Text.Trim(), txtClaimsNo.Text.Trim(), _
                                          txtProductClass.Text.Trim(), DdnClaimType.SelectedItem.Text.Trim(), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text)), _
                                          Convert.ToDecimal(totalPremPaidlc), Convert.ToDecimal(totalPremPaidlc), _
                                          Convert.ToDecimal(premDuelc), Convert.ToDecimal(premDuelc), premOutsdlc, premOutsdlc, _
                                          Convert.ToString(txtProductDec.Text), Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtDOB.Text)), _
                                          Convert.ToString(DdnLossType.SelectedItem.Text.Trim), Convert.ToDecimal(txtTotalPaidOut.Text.Trim), _
                                          ddnClaimSettle.SelectedItem.Text.Trim, flag, dateAdded, operatorId)
            FirstMsg = "javascript:alert('" + lblMsg.Text + "');"
        End If


        Proc_DataBind_ClaimsRec()
        InitializeClaimFields()

    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click

        InitializeClaimFields()
        
    End Sub


    Protected Sub cmdDelete_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete_ASP.Click
        Dim str() As String

        If txtNotificationDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtNotificationDate")
            str = MOD_GEN.DoDate_Process(txtNotificationDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Notification date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtNotificationDate.Focus()
                Exit Sub

            Else
                txtNotificationDate.Text = str(2).ToString()
            End If

        End If

        If txtClaimsEffectiveDate.Text <> "" Then
            Dim ctrlId As Control = FindControl("txtClaimsEffectiveDate")
            str = MOD_GEN.DoDate_Process(txtClaimsEffectiveDate.Text, ctrlId)

            If str(2) = Nothing Then
                Dim errMsg = str(0).Insert(18, " Claims Effective Date, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                FirstMsg = errMsg
                txtClaimsEffectiveDate.Focus()
                Exit Sub

            Else
                txtClaimsEffectiveDate.Text = str(2).ToString()
            End If

        End If


        If txtTotalPremPaid.Text = "" Or txtTotalPremPaid.Text = "0.00" Then
            lblMsg.Text = "Total Premium Paid LC field is required!"
            txtTotalPremPaid.Focus()
            Exit Sub
        Else
            totalPremPaidlc = Convert.ToDecimal((txtTotalPremPaid.Text).Replace(",", ""))
            totalPremPaidfc = totalPremPaidlc

        End If



        If txtPremOutstanding.Text = "" Then
            lblMsg.Text = "Additional Sum Claimed LC field is required!"
            txtPremOutstanding.Focus()
            Exit Sub
        Else
            premOutsdlc = Convert.ToDecimal((txtPremOutstanding.Text).Replace(",", ""))
            premOutsdfc = premOutsdlc
        End If

        'If DdnClaimType.SelectedIndex = 0 Then
        '    lblMsg.Text = "Claims Type field is required!"
        '    DdnClaimType.Focus()
        '    Exit Sub
        'End If

        'If DdnLossType.SelectedIndex = 0 Then
        '    lblMsg.Text = "Loss Type field is required!"
        '    DdnLossType.Focus()
        '    Exit Sub
        'End If


        'If txtProductDec.Text = "" Then
        '    lblMsg.Text = "Product Description field is required!"
        '    txtProductDec.Focus()
        '    Exit Sub
        'End If


        If txtAction.Text = "Delete" Then

            Dim flag As String = "D"
            Dim dateAdded As DateTime = Now
            Dim operatorId As String = CType(Session("MyUserIDX"), String)

            lblMsg.Text = ChangeClaims(0, txtFileNum.Text.ToString.Trim(), txtQuote_Num.Text.ToString.Trim(), _
                                          txtPolNum.Text.Trim(), txtClaimsNo.Text.Trim(), _
                                          txtProductClass.Text.Trim(), DdnClaimType.SelectedItem.Text.Trim(), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtNotificationDate.Text)), _
                                          Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtClaimsEffectiveDate.Text)), _
                                          Convert.ToDecimal(totalPremPaidlc), Convert.ToDecimal(totalPremPaidlc), _
                                          Convert.ToDecimal(premDuelc), Convert.ToDecimal(premDuelc), premOutsdlc, premOutsdlc, _
                                          Convert.ToString(txtProductDec.Text), Convert.ToDateTime(MOD_GEN.DoConvertToDbDateFormat(txtDOB.Text)), _
                                          Convert.ToString(DdnLossType.SelectedItem.Text.Trim), Convert.ToDecimal(txtTotalPaidOut.Text.Trim), ddnClaimSettle.SelectedItem.Text.Trim, flag, dateAdded, operatorId)


        End If
    End Sub


    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click

    End Sub

   

    Protected Sub cmdFileNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileNum.Click
        If LTrim(RTrim(Me.txtFileNum.Text)) <> "" Then
            Me.txtQuote_Num.Text = ""
            Me.txtPolNum.Text = ""
            strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
        Else
            Me.lblMsg.Text = "Please enter a file number"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Me.txtFileNum.Focus()
            Exit Sub
        End If
    End Sub
    Private Function Proc_DoOpenRecord(ByVal FVstrGetType As String, ByVal FVstrRefNum As String, Optional ByVal FVstrRecNo As String = "", Optional ByVal strSearchByWhat As String = "FILE_NUM") As String
        'DoNew()
        strErrMsg = "false"

        lblMsg.Text = ""
        If Trim(FVstrRefNum) = "" Then
            Return strErrMsg
            Exit Function
        End If

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Return strErrMsg
            Exit Function
        End Try


        strSQL = "SPIL_GET_POLICY_ENQUIRY"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        'objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = FVstrRefNum
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        Try
            objOLEDR = objOLECmd.ExecuteReader()
            If (objOLEDR.Read()) Then
                strErrMsg = "true"

                Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_FILE_NO") & vbNullString, String))

                Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PROPSAL_NO") & vbNullString, String))
                Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))

                Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
                Call gnProc_DDL_Get(Me.cboProductClass, RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_MDLE") & vbNullString, String)) & RTrim("=") & RTrim(Me.txtProductClass.Text))
                Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_DESC") & vbNullString, String))


                If IsDate(objOLEDR("TBIL_POLY_PRPSAL_ISSUE_DATE")) Then
                    Me.txtProposalDate.Text = Format(CType(objOLEDR("TBIL_POLY_PRPSAL_ISSUE_DATE"), DateTime), "dd/MM/yyyy")
                End If
                If IsDate(objOLEDR("TBIL_POL_PRM_FROM")) Then
                    Me.txtCommenceDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_FROM"), DateTime), "dd/MM/yyyy")
                    myarrData = Split(Trim(txtCommenceDate.Text), "/")
                    GenStart_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                End If
                If IsDate(objOLEDR("TBIL_POL_PRM_TO")) Then
                    Me.txtMaturityDate.Text = Format(CType(objOLEDR("TBIL_POL_PRM_TO"), DateTime), "dd/MM/yyyy")
                    myarrData = Split(Trim(txtMaturityDate.Text), "/")
                    GenEnd_Date = CDate(Format(Val(myarrData(1)), "00") & "/" & Format(Val(myarrData(0)), "00") & "/" & Format(Val(myarrData(2)), "0000"))
                End If
                Me.txtAssuredName.Text = RTrim(CType(objOLEDR("TBIL_INSRD_NAME") & vbNullString, String))
                Me.txtTelephone.Text = RTrim(CType(objOLEDR("TBIL_INSRD_PHONE_NO") & vbNullString, String))
                Me.txtAddress.Text = RTrim(CType(objOLEDR("TBIL_INSRD_ADDRESS") & vbNullString, String))
                Me.txtEmail.Text = RTrim(CType(objOLEDR("TBIL_INSRD_EMAIL") & vbNullString, String))
                Me.txtDOB.Text = Format(CType(objOLEDR("TBIL_POLY_ASSRD_BDATE"), DateTime), "dd/MM/yyyy") 'RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_BDATE") & vbNullString, String))

                If Not IsDBNull(objOLEDR("TBIL_POL_PRM_SA_LC")) Then _
                            txtSumAssured.Text = Format(objOLEDR("TBIL_POL_PRM_SA_LC"), "Standard")

                If Not IsDBNull(objOLEDR("TBIL_POL_PRM_DTL_MOP_PRM_LC")) Then _
                            txtPremAmt.Text = Format(objOLEDR("TBIL_POL_PRM_DTL_MOP_PRM_LC"), "Standard")

                txtMop.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MODE_PAYT") & vbNullString, String))
                txtTenure.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PERIOD_YRS") & vbNullString, String))

            End If
            Proc_DataBind_ClaimsRec()
            Proc_DataBind_Beneficiary()

            
            txtTotalPremDue.Text = Format(CalculateTotalPremDue(txtMop.Text, CDbl(txtPremAmt.Text), GenStart_Date, CurrentDate), "Standard")
            txtPremDueH.Value = txtTotalPremDue.Text

            Me.cmdNew_ASP.Enabled = True


            strOPT = "2"
            Me.lblMsg.Text = "Status: Record Retrieved"



            Select Case UCase(strP_TYPE)
                Case "NEW"
                    'STRMENU_TITLE = "New Proposal"
                    Me.lblPolNum.Enabled = False
                    Me.txtPolNum.Enabled = False
                    Me.cmdGetPol.Enabled = False
                Case "CHG"
                    'STRMENU_TITLE = "Change Mode"
                    Me.lblMsg.Text = "Sorry!. Unable to get record ..."
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Me.txtFileNum.Text = ""
                    Me.lblPolNum.Enabled = True
                    Me.txtPolNum.Enabled = True
                    Me.txtPolNum.Text = ""
                    Me.cmdGetPol.Enabled = True
                    'Call Proc_DoNew()
                Case "DEL"
                    'STRMENU_TITLE = "Delete Mode"
                    Me.lblMsg.Text = "Sorry!. Unable to get record ..."
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Me.txtFileNum.Text = ""
                    Me.txtPolNum.Text = ""
                    ' Call Proc_DoNew()
                Case Else
                    'strP_TYPE = "NEW"
                    'STRMENU_TITLE = "New Proposal"
            End Select

            strOPT = "1"
            Me.lblMsg.Text = "Status: New Entry..."


        Catch ex As Exception
            Me.lblMsg.Text = ex.Message
            Return strErrMsg
            Exit Function
        End Try

        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        Return strErrMsg

    End Function

    Protected Sub cmdGetPol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetPol.Click
        If Trim(Me.txtPolNum.Text) <> "" Then
            Me.txtFileNum.Text = ""
            Me.txtQuote_Num.Text = ""
            strStatus = Proc_DoOpenRecord(RTrim("POL"), Me.txtPolNum.Text, RTrim("0"))
        Else
            Me.lblMsg.Text = "Please enter a policy number"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Me.txtPolNum.Focus()
            Exit Sub
        End If
    End Sub

    Protected Sub cmdGetPro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetPro.Click
        If Trim(Me.txtQuote_Num.Text) <> "" Then
            Me.txtFileNum.Text = ""
            Me.txtPolNum.Text = ""
            strStatus = Proc_DoOpenRecord(RTrim("QUO"), Me.txtQuote_Num.Text, RTrim("0"))
        Else
            Me.lblMsg.Text = "Please enter a policy number"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Me.txtPolNum.Focus()
            Exit Sub
        End If
    End Sub
    Private Function CalculateTotalPremDue(ByVal mop As String, ByVal PremAmt As Double, ByVal StartDate As Date, ByVal CurrentDate As Date) As Double
        Dim diff As Integer
        Dim TotPremDue As Double
        TotPremDue = 0.0
        If mop = "M" Then
            diff = DateDiff(DateInterval.Month, StartDate, CurrentDate)
            TotPremDue = PremAmt * diff
        ElseIf mop = "Q" Then
            diff = DateDiff(DateInterval.Quarter, StartDate, CurrentDate)
            TotPremDue = PremAmt * diff
        ElseIf mop = "H" Then
            diff = DateDiff(DateInterval.Month, StartDate, CurrentDate)
            diff = diff / 6
            TotPremDue = PremAmt * diff
        ElseIf mop = "Y" Then
            diff = DateDiff(DateInterval.Year, StartDate, CurrentDate)
            TotPremDue = PremAmt * diff
        ElseIf mop = "S" Then
            diff = DateDiff(DateInterval.Year, StartDate, CurrentDate)
            TotPremDue = PremAmt * diff
        ElseIf mop = "A" Then
            diff = DateDiff(DateInterval.Year, StartDate, CurrentDate)
            TotPremDue = PremAmt * diff
        ElseIf mop = "W" Then
            diff = DateDiff(DateInterval.Month, StartDate, CurrentDate)
            TotPremDue = PremAmt * (diff * 4)
        End If
        Return TotPremDue
    End Function
    Private Sub DoProc_CreateDataSource(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboDDList As DropDownList)

        pvcboDDList.Items.Clear()

        Dim pvField_Text As String = "MyFld_Text"
        Dim pvField_Value As String = "MyFld_Value"

        ' Create a table to store data for the DropDownList control.
        Dim obj_dt As DataTable = New DataTable()
        Dim obj_dr As DataRow
        Dim obj_dv As DataView

        ' Define the columns of the table.
        obj_dt.Columns.Add(New DataColumn(pvField_Text, GetType(String)))
        obj_dt.Columns.Add(New DataColumn(pvField_Value, GetType(String)))


        obj_dr = obj_dt.NewRow()
        obj_dr(pvField_Text) = "*** Select item ***"
        obj_dr(pvField_Value) = "0"

        obj_dt.Rows.Add(obj_dr)

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            obj_dv = New DataView(obj_dt)
            'Return obj_dv
            Exit Sub
        End Try


        Dim objOLECmd As OleDbCommand
        Dim objOLEDR As OleDbDataReader

        Select Case UCase(Trim(pvCODE))
            Case "IL_PRODUCT_CAT_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT RTRIM(TBIL_PRDCT_CAT_MDLE) + '=' + RTRIM(TBIL_PRDCT_CAT_CD) AS MyFld_Value, TBIL_PRDCT_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_CAT_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE = '" & RTrim("I") & "'"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_DESC"

            Case "GL_PRODUCT_CAT_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT RTRIM(TBIL_PRDCT_CAT_MDLE) + '=' + RTRIM(TBIL_PRDCT_CAT_CD) AS MyFld_Value, TBIL_PRDCT_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_CAT_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE = '" & RTrim("G") & "'"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_DESC"

            Case "IL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','I')"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_DESC"

            Case "GL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','G')"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_DESC"

        End Select

        objOLECmd = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        objOLEDR = objOLECmd.ExecuteReader()

        Do While objOLEDR.Read
            obj_dr = obj_dt.NewRow()
            obj_dr(pvField_Text) = RTrim(CType(objOLEDR("MyFld_Text") & vbNullString, String))
            obj_dr(pvField_Value) = RTrim(CType(objOLEDR("MyFld_Value") & vbNullString, String))

            obj_dt.Rows.Add(obj_dr)
        Loop

        obj_dt.AcceptChanges()


        objOLECmd = Nothing
        objOLEDR = Nothing

        Try
            'close connection to database
            objOLEConn.Close()
        Catch ex As Exception
            'Me.textMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            'Me.lblMsg.Text = ex.Message.ToString
        End Try

        objOLEConn = Nothing

        ' Create a DataView from the DataTable to act as the data source
        ' for the DropDownList control.
        obj_dv = New DataView(obj_dt)
        obj_dv.Sort = pvField_Value


        pvcboDDList.DataSource = obj_dv
        pvcboDDList.DataTextField = pvField_Text
        pvcboDDList.DataValueField = pvField_Value

        ' Bind the data to the control.
        pvcboDDList.DataBind()

        ' Set the default selected item, if desired.
        pvcboDDList.SelectedIndex = 0

        'Return obj_dv

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
        strSQL = "SPIL_CLAIMSNUM_SEARCH_FRM_TABLE"

        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
        objDA.SelectCommand.CommandType = CommandType.StoredProcedure
        objDA.SelectCommand.Parameters.Clear()

        objDA.SelectCommand.Parameters.Add("pRefNo", OleDbType.VarChar, 50).Value = Trim(txtPolNum.Text)
        objDA.SelectCommand.Parameters.Add("pRecID", OleDbType.BigInt).Value = 0

        objDA.SelectCommand.Parameters.Add("pRefType", OleDbType.VarChar, 10).Value = "POL"
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
        Dim P As Integer = 0
        'Dim C As Integer = 0
        'C = 0
        If GridView1.Rows.Count > 0 Then
            'For P = 0 To Me.GridView1.Rows.Count - 1
            '    TotalPremPaid += GridView1.Rows(0).Cells(3).Text
            'Next
            'txtTotalPremPaid.Text = Format(TotalPremPaid, "Standard")
        End If

    End Sub

    Protected Sub txtTotalPremPaid_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalPremPaid.TextChanged
        If txtTotalPremPaid.Text <> "" Or txtTotalPremPaid.Text = "0.00" Then
            premOutsdlc = Convert.ToDouble(txtTotalPremDue.Text) - Convert.ToDouble(txtTotalPremPaid.Text)
            txtPremOutstanding.Text = Format(premOutsdlc.ToString, "Standard")
        End If
    End Sub
    Private Sub InitializeClaimFields()


        txtAction.Text = "New"


        DoNew()
        DdnLossType.SelectedIndex = 0
        DdnClaimType.SelectedIndex = 0
        txtClaimsEffectiveDate.Text = ""
        txtNotificationDate.Text = ""
        txtProductDec.Text = ""
        txtQuote_Num.Text = String.Empty
        txtFileNum.Text = String.Empty
        'txtPolNum.Text = String.Empty
        txtClaims_No_sv.Text = String.Empty
        txtClaimsNo.Text = String.Empty






    End Sub

    Private Sub DoNew()
        Me.txtCommenceDate.Text = ""
        Me.txtMaturityDate.Text = ""
        Me.txtProposalDate.Text = ""
        Me.txtAssuredName.Text = ""
        Me.txtTelephone.Text = ""
        Me.txtAddress.Text = ""
        Me.txtEffDate.Text = ""
        Me.txtProduct_Name.Text = ""
        Me.txtEmail.Text = ""
        Me.txtSumAssured.Text = ""
        Me.txtPremAmt.Text = ""
        Me.txtMop.Text = ""
        Me.txtTenure.Text = ""
        Me.txtTotalPremDue.Text = ""
        Me.txtTotalPremPaid.Text = ""
        Me.txtPremOutstanding.Text = ""
        cboProductClass.SelectedIndex = -1

        GridView1.DataSource = Nothing
        GridView1.DataBind()

        GridView2.DataSource = Nothing
        GridView2.DataBind()

        Me.lblMsg.Text = ""
    End Sub
    Private Sub Proc_DataBind_Beneficiary()

        strTable = "TBIL_POLICY_BENEFRY"
        strSQL = ""
        strSQL = strSQL & "SELECT *,(select a.TBIL_COD_LONG_DESC from TBIL_LIFE_CODES a where b.TBIL_POL_BENF_RELATN_CD = a.TBIL_COD_ITEM and a.TBIL_COD_TAB_ID =  'L02' and a.TBIL_COD_TYP = '013' ) as RELATIONSHIP "
        strSQL = strSQL & " FROM " & strTable & " b "
        strSQL = strSQL & " WHERE TBIL_POL_BENF_FILE_NO = '" & txtFileNum.Text.Trim() & "'"
        strSQL = strSQL & " AND TBIL_POL_BENF_PROP_NO = '" & txtQuote_Num.Text.Trim() & "'"
        strSQL = strSQL & " ORDER BY TBIL_POL_BENF_COVER_ID, TBIL_POL_BENF_SNO"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try

        Try
            Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

            Dim objDS As DataSet = New DataSet()
            objDA.Fill(objDS, strTable)

            With GridView2
                .DataSource = objDS
                .DataBind()
            End With

            objDS.Dispose()
            objDA.Dispose()

            objDS = Nothing
            objDA = Nothing

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString

        End Try


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub
 
    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim pvURL As String = "PRG_LI_REQ_BENE_BANK.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        pvURL = pvURL & "&optclaimid=" & Trim(Me.DdnClaimType.SelectedItem.Text)
        pvURL = pvURL & "&optclaimkey=" & Trim(Me.txtClaimsNo.Text.Trim)

        Response.Redirect(pvURL)

    End Sub
End Class
