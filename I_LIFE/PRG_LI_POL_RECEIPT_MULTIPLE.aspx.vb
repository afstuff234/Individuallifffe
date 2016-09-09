Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports CustodianLife.Data
Imports CustodianLife.Model
Imports CustodianLife.Repositories
Imports System.Xml.Serialization
Imports System.Xml
Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Partial Class I_LIFE_PRG_LI_POL_RECEIPT_MULTIPLE
    Inherits System.Web.UI.Page
    Dim rcRepo As ReceiptsRepository
    Dim indLifeEnq As IndLifeCodesRepository
    Dim prodEnq As ProductDetailsRepository
    Dim prodCatRepo As ProductCatRepository
    Dim polinfo As PolicyInfo
    Dim updateFlag As Boolean
    Dim strKey As String
    Dim strSchKey As String
    Dim newReceiptNo As String
    Dim newSerialNum As String
    Dim Rceipt As CustodianLife.Model.Receipts

    Dim Save_Amount As Decimal = 0
    Dim Cum_Detail_Amount As Decimal = 0
    Dim swEntry As String = "H"
    Dim detailEdit As String = "N"
    Dim TotTransAmt As Decimal = 0
    Dim TransAmt As Decimal = 0
    Protected publicMsgs As String = String.Empty
    Protected FirstMsg As String = String.Empty
    Protected strF_ID As String
    Protected strQ_ID As String
    Protected strP_ID As String
    Protected strStatus As String
    Dim strSQL As String
    Protected strP_TYPE As String
    Protected strP_DESC As String
    Dim Err As String
    Dim strTmp_Value As String = ""
    Dim myarrData() As String
    Protected STRMENU_TITLE As String = String.Empty

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtAssuredAddress.Attributes.Add("disabled", "disabled")
        txtMOP.Attributes.Add("disabled", "disabled")
        txtMOPDesc.Attributes.Add("disabled", "disabled")
        txtTransDesc2.Attributes.Add("disabled", "disabled")
        txtAgentCode.Attributes.Add("disabled", "disabled")
        txtPolRegularContrib.Attributes.Add("disabled", "disabled")
        txtInsuredCode.Attributes.Add("disabled", "disabled")
        txtAgentName.Attributes.Add("disabled", "disabled")
        txtMainAcctDebitDesc.Attributes.Add("disabled", "disabled")
        'txtMainAcctCreditDesc.Attributes.Add("disabled", "disabled")
        txtSubAcctDebitDesc.Attributes.Add("disabled", "disabled")
        'txtSubAcctCreditDesc.Attributes.Add("disabled", "disabled")
        txtAssuredName.Attributes.Add("disabled", "disabled")
        txtAgentName.Attributes.Add("disabled", "disabled")

        STRMENU_TITLE = "Proposal Screen"
        'STRMENU_TITLE = "Investment Plus Proposal"

        Try
            strF_ID = CType(Request.QueryString("optfileid"), String)
        Catch ex As Exception
            strF_ID = ""
        End Try

        Try
            strQ_ID = CType(Request.QueryString("optquotid"), String)
        Catch ex As Exception
            strQ_ID = ""
        End Try

        Try
            strP_ID = CType(Request.QueryString("optpolid"), String)
        Catch ex As Exception
            strP_ID = ""
        End Try


        Try
            strF_ID = CType(Request.QueryString("optfileid"), String)
        Catch ex As Exception
            strF_ID = ""
        End Try

        Try
            strQ_ID = CType(Request.QueryString("optquotid"), String)
        Catch ex As Exception
            strQ_ID = ""
        End Try

        Try
            strP_ID = CType(Request.QueryString("optpolid"), String)
        Catch ex As Exception
            strP_ID = ""
        End Try


        If Not (Page.IsPostBack) Then
            Me.lblMsg.Text = "Status:"


            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False
            'Call gnProc_Populate_Box("IL_CODE_LIST", "017", Me.cboPrem_SA_Currency)
            'Call gnProc_Populate_Box("IL_MOP_FACTOR_LIST", "IND", Me.cboPrem_MOP_Type)

            If Trim(strF_ID) <> "" Then
                Me.txtFileNo.Text = RTrim(strF_ID)
                Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                If oAL.Item(0) = "TRUE" Then
                    '    'Retrieve the record
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
                    Me.txtQuote_Num.Text = oAL.Item(3)
                    Me.txtPolicyNo.Text = oAL.Item(4)
                    Me.txtProductClass.Text = oAL.Item(5)
                    Me.txtProduct.Text = oAL.Item(6)
                    Me.txtPlan_Num.Text = oAL.Item(7)
                    Me.txtCover_Num.Text = oAL.Item(8)
                    Me.cmdNext.Enabled = True

                    If UCase(oAL.Item(18).ToString) = "A" Then
                        Me.cmdNew_ASP.Visible = False
                        'Me.cmdSave_ASP.Visible = False
                        Me.cmdDelete_ASP.Visible = False
                        Me.cmdPrint_ASP.Visible = False
                    End If

                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If


            If txtPolicyNo.Text.Trim.Length > 0 Then
                cmbTransType.SelectedValue = "RN"
                cmbTransType.Enabled = False
            Else
                cmbTransType.SelectedValue = "NB"
                cmbTransType.Enabled = False

            End If
            If Trim(strF_ID) <> "" Then
                Proc_DoOpenRecord()

            End If
        End If

        If Me.txtAction.Text = "Save" Then
            'Call Proc_DoSave()
            Me.txtAction.Text = ""

        End If

        If Me.txtAction.Text = "Delete" Then
            'Call Proc_DoDelete()
            Me.txtAction.Text = ""
        End If


        If Not Page.IsPostBack Then
            rcRepo = New ReceiptsRepository
            indLifeEnq = New IndLifeCodesRepository
            prodEnq = New ProductDetailsRepository
            prodCatRepo = New ProductCatRepository
            Session("rcRepo") = rcRepo
            updateFlag = False
            Session("updateFlag") = updateFlag
            swEntry = "H"
            Session("swEntry") = swEntry

            strKey = Request.QueryString("idd")
            Session("rcId") = strKey

            'Company code value to be filled from login
            'txtCompanyCode.Text = "001"
            '  txtEntryDate.Text = Now.Date.ToString()
            txtEntryDate.Text = Format(Now, "dd/MM/yyyy")
            txtEntryDate.ReadOnly = True
            lblError.Visible = False

            Call gnProc_Populate_Box("IL_CODE_LIST", "003", cmbBranchCode)
            Call gnProc_Populate_Box("IL_CODE_LIST", "017", cmbCurrencyType)
            'SetComboBinding(cmbBranchCode, indLifeEnq.GetById("L02", "003"), "CodeItem_CodeLongDesc", "CodeItem")
            'SetComboBinding(cmbCurrencyType, indLifeEnq.GetById("L02", "017"), "CodeItem_CodeLongDesc", "CodeItem")
            'SetComboBinding(cboProductClass, prodCatRepo.ProductCategories(), "PrdtCode_Desc", "ProductCatCode")
            txtSubAcctDebit.Text = "000000"
            cmbBranchCode.SelectedValue = "1501"
            txtBranchCode.Text = cmbBranchCode.SelectedValue
            txtCurrencyCode.Text = cmbCurrencyType.SelectedValue
            If strKey IsNot Nothing Then
                strKey = CType(Session("rcId"), String)
                rcRepo = CType(Session("rcRepo"), ReceiptsRepository)
                Rceipt = rcRepo.GetById(strKey)

                Session("Rceipt") = Rceipt
                fillValues()
            Else
                rcRepo = CType(Session("rcRepo"), ReceiptsRepository)
            End If

        Else 'post back
            txtPolRegularContrib.Text = txtPolRegularContribH.Value
            txtMainAcctDebitDesc.Text = txtMainAcctDebitDescH.Value
            Me.Validate()
            If (Not Me.IsValid) Then
                Dim msg As String
                ' Loop through all validation controls to see which 
                ' generated the error(s).
                Dim oValidator As IValidator
                For Each oValidator In Validators
                    If oValidator.IsValid = False Then
                        msg = msg & "\n" & oValidator.ErrorMessage
                    End If
                Next

                lblError.Text = msg
                lblError.Visible = True
                publicMsgs = "javascript:alert('" + msg + "')"
            End If
        End If

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        'Dim msg As String = String.Empty
        'lblError.Text = ""
        'Dim Err = ""
        'ValidateFields(Err)
        'If Err = "Y" Then
        '    Exit Sub
        'End If
        'Try
        '    If Me.IsValid Then

        '        'this routine will persist only one object. 
        '        '1. The Receipt object

        '        updateFlag = CType(Session("updateFlag"), Boolean)
        '        If Not updateFlag Then 'if new record

        '            'create a new instance of the Receipt object
        '            Rceipt = New CustodianLife.Model.Receipts()
        '            rcRepo = New ReceiptsRepository()
        '            lblError.Visible = False
        '            txtAgentCode.Enabled = True
        '            Rceipt.AgentCode = txtAgentCode.Text
        '            'txtAgentCode.Enabled = False

        '            Rceipt.TotalAmountFC = CType(txtReceiptAmtFC.Text, Decimal)
        '            Rceipt.TotalAmountLC = CType(txtReceiptAmtLC.Text, Decimal)

        '            Rceipt.BankCode = String.Empty 'txtBankGLCode.Text
        '            Rceipt.BatchNo = CType(txtBatchNo.Text, Int32)
        '            Rceipt.BranchCode = cmbBranchCode.SelectedValue.ToString
        '            If Trim(txtChequeDate.Text).Length() > 0 Then
        '                Rceipt.ChequeDate = ValidDate(txtChequeDate.Text)
        '            Else
        '                Rceipt.ChequeDate = #1/1/2014#
        '            End If

        '            Rceipt.ChequeInwardNo = txtChequeNo.Text
        '            Rceipt.ChequeTellerNo = txtTellerNo.Text
        '            Rceipt.CommissionApplicable = cmbCommissions.SelectedValue
        '            Rceipt.CompanyCode = "001"

        '            Rceipt.CurrencyType = cmbReceiptType.SelectedValue
        '            Rceipt.EntryDate = Now.Date
        '            Rceipt.InsuredCode = txtInsuredCode.Text

        '            Rceipt.MainAccountCredit = String.Empty ' txtMainAcctCredit.Text
        '            Rceipt.MainAccountDebit = txtMainAcctDebit.Text
        '            Rceipt.PayeeName = txtPayeeName.Text
        '            Rceipt.PolicyPaymentMode = cmbMode.SelectedValue
        '            Rceipt.PolicyRegularContribution = CType(txtPolRegularContrib.Text, Decimal)

        '            Rceipt.PolicyPaymentMode = txtMOP.Text
        '            Rceipt.ReceiptType = cmbReceiptType.SelectedValue
        '            Rceipt.ReferenceNo = txtReceiptRefNo.Text

        '            Dim docMonth As String = Right(txtBatchNo.Text, 2)
        '            Dim docYear As String = Left(txtBatchNo.Text, 4)

        '            'get new serial number
        '            newSerialNum = rcRepo.GetNextSerialNumber("L01", "001", docMonth, docYear, " ", "12", "11")


        '            'get new receipt number
        '            newReceiptNo = rcRepo.GetNextSerialNumber("RCN", "002", "001", docYear, "IL-BR-", "12", "11")
        '            txtReceiptNo.Text = Trim(newReceiptNo)
        '            txtSerialNo.Text = newSerialNum
        '            Rceipt.SerialNo = CType(newSerialNum, Int64)
        '            Rceipt.SubAccountCredit = String.Empty ' txtSubAcctCredit.Text
        '            Rceipt.SubAccountDebit = txtSubAcctDebit.Text
        '            Rceipt.DocNo = Trim(newReceiptNo)

        '            Rceipt.TempTransNo = txtTempReceiptNo.Text
        '            Rceipt.TranDescription1 = txtTransDesc1.Text
        '            Rceipt.TranDescription2 = txtTransDesc2.Text
        '            Rceipt.TransDate = ValidDate(txtEffectiveDate.Text)
        '            Rceipt.TransMode = cmbMode.SelectedValue
        '            Rceipt.TransType = cmbTransType.SelectedValue
        '            Rceipt.CurrencyType = cmbCurrencyType.SelectedValue
        '            Rceipt.LedgerTypeCredit = "T"
        '            Rceipt.FileNo = txtFileNo.Text.Trim()
        '            Rceipt.ProductCode = txtProduct.Text.Trim()
        '            Rceipt.Flag = "A"
        '            Rceipt.OperId = "001"
        '            Rceipt.ProcDate = txtBatchNo.Text.Trim
        '            Rceipt.TempPolicyNo = String.Empty ' txtTempPolNo.Text.Trim()
        '            Rceipt.TempInsuredName = String.Empty ' txtTempInsName.Text.Trim()
        '            Rceipt.TempProdCode = cboProduct.SelectedValue
        '            Rceipt.TempProdCat = cboProductClass.SelectedValue

        '            rcRepo.Save(Rceipt)
        '            Session("Rceipt") = Rceipt
        '            msg = "Save Operation Successful"
        '            lblError.Text = msg
        '            lblError.Visible = True
        '            publicMsgs = "javascript:alert('" + msg + "')"

        '        Else
        '            Rceipt = CType(Session("Rceipt"), CustodianLife.Model.Receipts)
        '            rcRepo = CType(Session("rcRepo"), ReceiptsRepository)

        '            txtAgentCode.Enabled = True
        '            Rceipt.AgentCode = txtAgentCode.Text
        '            txtAgentCode.Enabled = False
        '            Rceipt.TotalAmountFC = CType(txtReceiptAmtFC.Text, Decimal)
        '            Rceipt.TotalAmountLC = CType(txtReceiptAmtLC.Text, Decimal)

        '            Rceipt.BankCode = String.Empty ' txtBankGLCode.Text
        '            Rceipt.BatchNo = CType(txtBatchNo.Text, Int32)
        '            Rceipt.BranchCode = cmbBranchCode.SelectedValue.ToString
        '            If Trim(txtChequeDate.Text).Length() > 0 Then
        '                Rceipt.ChequeDate = ValidDate(txtChequeDate.Text)
        '            Else
        '                Rceipt.ChequeDate = #1/1/2014#
        '            End If


        '            Rceipt.ChequeInwardNo = txtChequeNo.Text
        '            Rceipt.ChequeTellerNo = txtTellerNo.Text
        '            Rceipt.CommissionApplicable = cmbCommissions.SelectedValue
        '            Rceipt.CompanyCode = "001" 'txtCompanyCode.Text

        '            Rceipt.CurrencyType = cmbCurrencyType.SelectedValue
        '            '  Rceipt.EntryDate = CType(txtEntryDate.Text, Date)
        '            Rceipt.InsuredCode = txtInsuredCode.Text


        '            Rceipt.MainAccountCredit = String.Empty 'txtMainAcctCredit.Text
        '            Rceipt.MainAccountDebit = txtMainAcctDebit.Text
        '            Rceipt.PayeeName = txtPayeeName.Text
        '            Rceipt.PolicyPaymentMode = cmbMode.SelectedValue
        '            Rceipt.PolicyRegularContribution = CType(txtPolRegularContrib.Text, Decimal)

        '            Rceipt.PolicyPaymentMode = txtMOP.Text
        '            Rceipt.ReceiptType = cmbReceiptType.SelectedValue
        '            Rceipt.ReferenceNo = txtReceiptRefNo.Text
        '            Rceipt.SubAccountCredit = String.Empty 'txtSubAcctCredit.Text
        '            Rceipt.SubAccountDebit = txtSubAcctDebit.Text

        '            Rceipt.TempTransNo = txtTempReceiptNo.Text
        '            Rceipt.TranDescription1 = txtTransDesc1.Text
        '            Rceipt.TranDescription2 = txtTransDesc2.Text
        '            Rceipt.TransDate = ValidDate(txtEffectiveDate.Text)
        '            Rceipt.TransMode = cmbMode.SelectedValue
        '            Rceipt.TransType = cmbTransType.SelectedValue
        '            Rceipt.CurrencyType = cmbCurrencyType.SelectedValue
        '            Rceipt.LedgerTypeCredit = "T"
        '            Rceipt.FileNo = txtFileNo.Text.Trim()
        '            Rceipt.ProductCode = txtProduct.Text.Trim()
        '            Rceipt.Flag = "A"
        '            Rceipt.OperId = "001"
        '            Rceipt.ProcDate = txtBatchNo.Text.Trim
        '            Rceipt.TempPolicyNo = String.Empty 'txtTempPolNo.Text.Trim()
        '            Rceipt.TempInsuredName = String.Empty 'txtTempInsName.Text.Trim()
        '            Rceipt.TempProdCode = cboProduct.SelectedValue
        '            Rceipt.TempProdCat = cboProductClass.SelectedValue

        '            rcRepo.Save(Rceipt)
        '            msg = "Save Operation Successful"
        '            lblError.Text = msg
        '            lblError.Visible = True
        '            publicMsgs = "javascript:alert('" + msg + "')"
        '        End If
        '        'If cmbReceiptType.SelectedValue = "P" Or cmbReceiptType.SelectedValue = "D" Then
        '        '    If (txtInsuredCode.Text = "" Or txtAgentCode.Text = "" Or txtPolRegularContrib.Text = "0.00" Or txtMOP.Text = "") Then
        '        '        rcRepo.UpdateUnCompletedRec(txtReceiptRefNo.Text)
        '        '    End If
        '        'End If
        '        initializeFields()
        '        txtReceiptNo.Enabled = False
        '    End If
        'Catch ex As Exception
        '    msg = ex.Message
        '    lblError.Text = msg
        '    lblError.Visible = True
        '    publicMsgs = "javascript:alert('" + msg + "')"

        'End Try



        lblError.Text = ""
        Dim msg As String = String.Empty
        Dim docMonth As String = String.Empty
        Dim docYear As String = String.Empty
        Dim Err = ""
        ValidateFields(Err)
        If Err = "Y" Then
            Exit Sub
        End If
        Try
            If Me.IsValid Then

                'this routine will persist only one object. 
                '1. The GlTrans object

                updateFlag = CType(Session("updateFlag"), Boolean)


                If Not updateFlag Then 'if new record

                    'create a new instance of the GLTrans object
                    Rceipt = New CustodianLife.Model.Receipts()
                    rcRepo = New ReceiptsRepository()
                    lblError.Visible = False

                    With Rceipt
                        .BatchNo = CType(txtBatchNo.Text, Integer)
                        .BranchCode = cmbBranchCode.SelectedValue.ToString()

                        'If Trim(txtChequeDate.Text).Length > 0 Then
                        '    .ChequeDate = ValidDate(txtChequeDate.Text)
                        'Else
                        .ChequeDate = #1/1/2014# 'initial value for all date fields
                        'End If

                        '.ChequeTellerNo = txtChequeNo.Text
                        .PayeeName = txtPayeeName.Text
                        .CompanyCode = "001"
                        .CurrencyType = cmbCurrencyType.SelectedValue.ToString()
                        .DocNo = txtReceiptNo.Text
                        .EntryDate = Now.Date
                        .PolicyPaymentMode = cmbMode.SelectedValue
                        .PolicyRegularContribution = CType(txtPolRegularContrib.Text, Decimal)

                        .PolicyPaymentMode = txtMOPH.Value
                        .ReceiptType = cmbReceiptType.SelectedValue
                        .ReferenceNo = txtQuote_Num.Text.Trim()

                        .ProductCode = txtProduct.Text.Trim()
                        .InsuredCode = txtInsuredCodeH.Value
                        .AgentCode = txtAgentCodeH.Value
                        .CommissionApplicable = cmbCommissions.SelectedValue

                        .MainAccountDebit = txtMainAcctDebit.Text
                        .SubAccountDebit = txtSubAcctDebit.Text
                        .LedgerTypeCredit = "T"

                        .FileNo = txtFileNo.Text.Trim()
                        .ProposalNo = txtQuote_Num.Text.Trim()
                        .PolicyNo = txtPolicyNo.Text.Trim()
                        .ProcDate = txtBatchNo.Text.Trim

                        .TotalAmountLC = txtReceiptAmtLC.Text
                        .DetailDocNo = txtDetailDocNo.Text.Trim()
                        If Trim(txtDetailDate.Text.Trim()).Length > 0 Then
                            .DetailTransDate = ValidDate(txtDetailDate.Text.Trim())
                        Else
                            .DetailTransDate = #1/1/2014# 'initial value for all date fields
                        End If

                        .DetailTransAmountLC = txtTransAmt.Text.Trim()
                        .Remarks = cmbRemarks.SelectedValue.Trim
                        .DetailTransStatus = cmbTransDetailStatus.SelectedValue.Trim


                        .OperId = "001"
                        .PostStatus = "U" 'Unposted
                        .ApprovalStatus = "N"
                        .Flag = "A"

                        .TellerNo = txtTellerNo.Text
                        .ProductClass = txtProductClass.Text
                        .ProductCode = txtProduct.Text
                        .ProductPlanNo = txtPlan_Num.Text
                        .ProductCoverNo = txtCover_Num.Text

                        If Trim(txtEffectiveDate.Text).Length > 0 Then
                            .TransDate = ValidDate(txtEffectiveDate.Text)
                        Else
                            .TransDate = #1/1/2014#
                        End If

                        Rceipt.TranDescription1 = txtTransDesc1.Text
                        Rceipt.TranDescription2 = txtTransDesc2.Text

                        .TransId = "H" 'header
                        .TransType = cmbTransType.SelectedValue.ToString()
                        Save_Amount = CType(txtReceiptAmtLC.Text, Decimal)
                        Session("Save_Amount") = Save_Amount

                        Cum_Detail_Amount = 0
                        Session("Cum_Detail_Amount") = Cum_Detail_Amount

                        docMonth = Right(txtBatchNo.Text, 2)
                        docYear = Left(txtBatchNo.Text, 4)
                        'get new serial number
                        'L02(other receipts) , 001 -- for main serial number 
                        'L02(other receipts), 002 -- for Sub serial number 
                        If .TransId = "H" Then
                            newSerialNum = rcRepo.GetNextSerialNumber("L01", "001", docMonth, docYear, " ", "12", "11")

                            'prgKey = Session("prgKey")

                            .TransType = cmbTransType.SelectedValue
                            'get new receipt number
                            newReceiptNo = rcRepo.GetNextSerialNumber("RCN", "002", "001", docYear, "IL-BR-", "12", "11")

                            txtReceiptNo.Text = newReceiptNo
                            txtSerialNo.Text = newSerialNum
                            .SerialNo = CType(txtSerialNo.Text, Long)
                            .DocNo = Trim(txtReceiptNo.Text)

                        End If
                        .TransMode = cmbMode.SelectedValue.ToString

                        'initialize details part
                        .DetailTransStatus = String.Empty 'D
                        .SubSerialNo = 0 'D

                        'save
                        rcRepo.Save(Rceipt)
                        grdData.DataBind()
                        Session("Rceipt") = Rceipt
                        updateFlag = True
                        Session("updateFlag") = updateFlag
                        detailEdit = "N"
                        Session("detailEdit") = detailEdit
                        ' initializeDetailFields()
                    End With
                Else
                    swEntry = CType(Session("swEntry"), String)
                    If swEntry = "D" Then
                        Rceipt = New CustodianLife.Model.Receipts() 'a new object but filled with old values with the only changes from the detail part
                        detailEdit = "N"
                        Session("detailEdit") = detailEdit ' detail records in new rec mode

                    Else
                        detailEdit = Session("detailEdit") ' detail records in edit mode
                        Rceipt = CType(Session("Rceipt"), CustodianLife.Model.Receipts)
                    End If
                    rcRepo = CType(Session("rcRepo"), ReceiptsRepository)


                    With Rceipt
                        .BatchNo = CType(txtBatchNo.Text, Integer)
                        .BranchCode = cmbBranchCode.SelectedValue.ToString()

                        'If Trim(txtChequeDate.Text).Length > 0 Then
                        '    .ChequeDate = ValidDate(txtChequeDate.Text)
                        'Else
                        .ChequeDate = #1/1/2014# 'initial value for all date fields
                        'End If
                        .TellerNo = txtTellerNo.Text
                        .ProductClass = txtProductClass.Text
                        .ProductCode = txtProduct.Text
                        .ProductPlanNo = txtPlan_Num.Text
                        .ProductCoverNo = txtCover_Num.Text

                        '.ChequeTellerNo = txtChequeNo.Text
                        .PayeeName = txtPayeeName.Text
                        .CompanyCode = "001"
                        .CurrencyType = cmbCurrencyType.SelectedValue.ToString()
                        .DocNo = txtReceiptNo.Text
                        .EntryDate = Now.Date
                        .PolicyPaymentMode = cmbMode.SelectedValue
                        .PolicyRegularContribution = CType(txtPolRegularContrib.Text, Decimal)

                        .PolicyPaymentMode = txtMOPH.Value 'txtMOP.Text
                        .ReceiptType = cmbReceiptType.SelectedValue
                        .ReferenceNo = txtQuote_Num.Text.Trim()

                        .ProductCode = txtProduct.Text.Trim()
                        .InsuredCode = txtInsuredCodeH.Value
                        .AgentCode = txtAgentCodeH.Value
                        .CommissionApplicable = cmbCommissions.SelectedValue

                        .MainAccountDebit = txtMainAcctDebit.Text
                        .SubAccountDebit = txtSubAcctDebit.Text
                        .LedgerTypeCredit = "T"

                        .FileNo = txtFileNo.Text.Trim()
                        .ProposalNo = txtQuote_Num.Text.Trim()
                        .PolicyNo = txtPolicyNo.Text.Trim()
                        .ProcDate = txtBatchNo.Text.Trim

                        .TotalAmountLC = txtReceiptAmtLC.Text.Trim
                        .DetailDocNo = txtDetailDocNo.Text.Trim()
                        If Trim(txtDetailDate.Text.Trim()).Length > 0 Then
                            .DetailTransDate = ValidDate(txtDetailDate.Text.Trim())
                        Else
                            .DetailTransDate = #1/1/2014# 'initial value for all date fields
                        End If
                        .DetailTransAmountLC = txtTransAmt.Text.Trim()
                        .Remarks = cmbRemarks.SelectedValue.Trim

                        .OperId = "001"
                        .PostStatus = "U" 'Unposted
                        .ApprovalStatus = "N"
                        .Flag = "A"
                        docMonth = Right(txtBatchNo.Text, 2)
                        docYear = Left(txtBatchNo.Text, 4)


                        .TellerNo = txtTellerNo.Text

                        If Trim(txtEffectiveDate.Text).Length > 0 Then
                            .TransDate = ValidDate(txtEffectiveDate.Text)

                        Else
                            .TransDate = #1/1/2014#
                        End If
                        Rceipt.TranDescription1 = txtTransDesc1.Text
                        Rceipt.TranDescription2 = txtTransDesc2.Text
                        .TransId = "D" 'Detail
                        .SerialNo = CType(txtSerialNo.Text, Long)
                        .DocNo = Trim(txtReceiptNo.Text)
                        .TransMode = cmbMode.SelectedValue.ToString
                        swEntry = CType(Session("swEntry"), String)
                        If swEntry = "H" Or swEntry = "D" Then
                            swEntry = "D"
                            Session("swEntry") = swEntry
                            'get sub serial number
                            'L02, 002 -- for Sub serial number 
                            detailEdit = CType(Session("detailEdit"), String)
                            If detailEdit = "N" Then
                                Dim newSerialSubNum As String = rcRepo.GetNextSerialNumber("L02", "002", docMonth, docYear, " ", "12", "11")
                                txtSubSerialNo.Text = newSerialSubNum
                                .SubSerialNo = CType(txtSubSerialNo.Text, Integer)
                            End If
                        End If

                        .TransType = cmbTransType.SelectedValue

                        Cum_Detail_Amount = Cum_Detail_Amount + .DetailTransAmountLC
                        Session("Cum_Detail_Amount") = Cum_Detail_Amount

                        ' If .PostStatus = "U" And .ApprovalStatus = "N" Then  'Unposted and Not Approved -- change is possible

                        rcRepo.Save(Rceipt)
                        msg = "Save Operation Successful"

                        lblError.Text = msg
                        lblError.Visible = True
                        publicMsgs = "javascript:alert('" + msg + "')"

                        'End If
                        grdData.DataBind()

                        Session("Rceipt") = Rceipt
                        initializeDetailFields()
                    End With


                End If
                ' initializeFields()


            End If
        Catch ex As Exception
            msg = ex.Message
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"

        End Try
        strF_ID = "Enable the Next Button"
        cmdNext.Enabled = True
    End Sub
    Protected Sub initializeDetailFields()

        cmbTransDetailStatus.SelectedIndex = 0
        txtTransAmt.Text = String.Empty
        txtDetailDate.Text = String.Empty
        'txtReceiptRefNo1.Text = String.Empty

        txtSubSerialNo.Text = 0
        txtDetailDocNo.Text = String.Empty

        txtTransAmt.Text = 0
        cmbRemarks.SelectedIndex = 0

        detailEdit = "N"
        Session("detailEdit") = detailEdit
        swEntry = "D"
        Session("swEntry") = swEntry
    End Sub

    Private Sub ValidateDetailFields(ByRef ErrorInd)
        Dim msg
        If cmbMode.SelectedIndex = 0 Then
            msg = "Please select receipt mode"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            cmbMode.Focus()
            Exit Sub
        End If
    End Sub
    Private Sub ValidateFields(ByRef ErrorInd)
        Dim msg
        'If txtReceiptNo.Text = "" Then
        '    msg = "Receipt number must not be empty"
        '    ErrorInd = "Y"
        '    lblError.Text = msg
        '    lblError.Visible = True
        '    publicMsgs = "javascript:alert('" + msg + "')"
        '    txtReceiptNo.Focus()
        '    Exit Sub
        'End If
        If txtBatchNo.Text = "" Then
            msg = "Batch date must not be empty"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            txtBatchNo.Focus()
            Exit Sub
        End If
        If txtEffectiveDate.Text = "" Then
            msg = "Effective date must not be empty"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            txtEffectiveDate.Focus()
            Exit Sub
        End If

        Dim str() As String
        str = DoDate_Process(txtEffectiveDate.Text, txtEffectiveDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Effective date, ")
            msg = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblError.Text = msg
            publicMsgs = "javascript:alert('" + msg + "')"
            lblError.Visible = True
            txtEffectiveDate.Focus()
            ErrorInd = "Y"
            Exit Sub
        Else
            txtEffectiveDate.Text = str(2).ToString()
        End If

        If cmbReceiptType.SelectedIndex = 0 Then
            msg = "Please select receipt type"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            cmbReceiptType.Focus()
            Exit Sub
        End If
        If txtPolicyNo.Text = "" Then
            ' msg = lblRefNo.Text & " must not be empty"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            txtPolicyNo.Focus()
            Exit Sub
        End If
        'If cmbReceiptType.SelectedValue = "P" Or cmbReceiptType.SelectedValue = "D" Then
        '    If txtInsuredCode.Text = "" Then
        '        msg = "Insured code must not be empty, Please contact technical dept to update record"
        '        ErrorInd = "Y"
        '        lblError.Text = msg
        '        lblError.Visible = True
        '        publicMsgs = "javascript:alert('" + msg + "')"
        '        txtInsuredCode.Focus()
        '        Exit Sub
        '    End If
        'End If
        If cmbMode.SelectedValue = "T" Then
            If txtTellerNo.Text = "" Then
                msg = "Teller no must not be empty"
                ErrorInd = "Y"
                lblError.Text = msg
                lblError.Visible = True
                publicMsgs = "javascript:alert('" + msg + "')"
                txtTellerNo.Focus()
                Exit Sub
            End If
        End If

        'If cmbMode.SelectedValue = "Q" Then
        '    If txtChequeNo.Text = "" Then
        '        msg = "Cheque no must not be empty"
        '        ErrorInd = "Y"
        '        lblError.Text = msg
        '        lblError.Visible = True
        '        publicMsgs = "javascript:alert('" + msg + "')"
        '        txtChequeNo.Focus()
        '        Exit Sub
        '    End If
        '    If txtChequeDate.Text = "" Then
        '        msg = "Cheque date must not be empty"
        '        ErrorInd = "Y"
        '        lblError.Text = msg
        '        lblError.Visible = True
        '        publicMsgs = "javascript:alert('" + msg + "')"
        '        txtChequeDate.Focus()
        '        Exit Sub
        '    End If
        'End If
        If txtPayeeName.Text = "" Then
            msg = "Payee name must not be empty"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            txtPayeeName.Focus()
            Exit Sub
        End If
        If cmbReceiptType.SelectedValue = "P" Or cmbReceiptType.SelectedValue = "D" Then
            If txtTransDesc1.Text = "" Then
                msg = "Trans desc 1 must not be empty"
                ErrorInd = "Y"
                lblError.Text = msg
                lblError.Visible = True
                publicMsgs = "javascript:alert('" + msg + "')"
                txtTransDesc1.Focus()
                Exit Sub
            End If
        End If
        If cmbCommissions.SelectedIndex = 0 Then
            msg = "Please select commission applicable"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            cmbCommissions.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtReceiptAmtLC.Text) Then
            msg = "Receipt Amount LC must be numeric"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            txtReceiptAmtLC.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtReceiptAmtFC.Text) Then
            msg = "Receipt Amount FC must be numeric"
            ErrorInd = "Y"
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
            txtReceiptAmtFC.Focus()
            Exit Sub
        End If

    End Sub

    Private Sub fillValues()

       
        If Rceipt IsNot Nothing Then
            'txtReceiptNo.Enabled = True

            txtAgentCode.Text = Rceipt.AgentCode
            'txtReceiptAmtFC.Text = Math.Round(Rceipt.TotalAmountFC, 2).ToString()
            'txtReceiptAmtLC.Text = Math.Round(Rceipt.TotalAmountLC, 2).ToString()

            txtReceiptAmtFC.Text = Format(Rceipt.TotalAmountFC, "Standard")
            txtReceiptAmtLC.Text = Format(Rceipt.TotalAmountLC, "Standard")

            'txtBankGLCode.Text = Rceipt.BankCode
            txtBatchNo.Text = Rceipt.BatchNo
            cmbBranchCode.SelectedValue = Rceipt.BranchCode
            'txtChequeDate.Text = ValidDateFromDB(Rceipt.ChequeDate)


            'txtChequeNo.Text = Rceipt.ChequeInwardNo
            txtTellerNo.Text = Rceipt.ChequeTellerNo
            cmbCommissions.SelectedValue = Rceipt.CommissionApplicable
            'txtCompanyCode.Text = Rceipt.CompanyCode

            cmbCurrencyType.SelectedValue = Rceipt.CurrencyType
            txtEntryDate.Text = ValidDateFromDB(Rceipt.EntryDate)
            txtInsuredCode.Text = Rceipt.InsuredCode

            'txtMainAcctCredit.Text = Rceipt.MainAccountCredit
            txtMainAcctDebit.Text = Rceipt.MainAccountDebit
            txtPayeeName.Text = Rceipt.PayeeName
            cmbMode.SelectedValue = Rceipt.PolicyPaymentMode
            'txtPolRegularContrib.Text = Math.Round(Rceipt.PolicyRegularContribution, 2)
            txtPolRegularContrib.Text = Format(Rceipt.PolicyRegularContribution, "Standard")


            txtMOP.Text = Rceipt.PolicyPaymentMode
            cmbReceiptType.SelectedValue = Rceipt.ReceiptType
            txtQuote_Num.Text = Rceipt.ReferenceNo
            txtReceiptNo.Text = Rceipt.DocNo
            Err = ""
            txtSerialNo.Text = Rceipt.SerialNo
            If Rceipt.SubAccountCredit = "" Then
                '    txtSubAcctCredit.Text = "000000"
            Else
                '   txtSubAcctCredit.Text = Rceipt.SubAccountCredit
            End If
            If Rceipt.SubAccountDebit = "" Then
                txtSubAcctDebit.Text = "000000"
            Else
                txtSubAcctDebit.Text = Rceipt.SubAccountDebit
            End If

            If txtMainAcctDebit.Text <> "" Then
                GetAcctDescriptionDR()
            End If

            'If txtMainAcctCredit.Text <> "" Then
            '    GetAcctDescriptionCR()
            'End If

            'txtTempReceiptNo.Text = Rceipt.TempTransNo
            txtTransDesc1.Text = Rceipt.TranDescription1
            txtTransDesc2.Text = Rceipt.TranDescription2
            txtEffectiveDate.Text = ValidDateFromDB(Rceipt.TransDate)
            cmbMode.SelectedValue = Rceipt.TransMode
            cmbTransType.SelectedValue = Rceipt.TransType
            cmbCurrencyType.SelectedValue = Rceipt.CurrencyType

            txtBranchCode.Text = cmbBranchCode.SelectedValue
            txtCurrencyCode.Text = cmbCurrencyType.Text
            txtReceiptCode.Text = cmbReceiptType.SelectedValue
            txtMode.Text = cmbMode.SelectedValue
            txtPolicyNo.Text = Rceipt.PolicyNo
            txtTellerNo.Text = Rceipt.TellerNo
            txtProductClass.Text = Rceipt.ProductClass
            txtProduct.Text = Rceipt.ProductCode

            txtPlan_Num.Text = Rceipt.ProductPlanNo
            txtCover_Num.Text = Rceipt.ProductCoverNo

            txtDetailDocNo.Text = Rceipt.DetailDocNo
            txtDetailDate.Text = Rceipt.DetailTransDate
            txtTransAmt.Text = Rceipt.DetailTransAmountLC
            cmbTransDetailStatus.SelectedValue = Rceipt.DetailTransStatus
            cmbRemarks.SelectedValue = Rceipt.Remarks
            cmbMode.SelectedValue = Rceipt.TransMode




            txtFileNo.Text = Rceipt.FileNo
            txtProduct.Text = Rceipt.ProductCode
            updateFlag = True
            Session("updateFlag") = updateFlag

            detailEdit = "Y"
            Session("detailEdit") = detailEdit
            grdData.DataBind()

            If (txtReceiptNo.Text <> "" And (cmbReceiptType.SelectedValue = "P" Or cmbReceiptType.SelectedValue = "D")) Then
                GetPolicyInfos(Err)
                If Err = "Y" Then
                    Exit Sub
                End If
            End If

        End If
        GetPolicyInfos(Err)
        GridView1.DataBind()

    End Sub
    Private Sub GetPolicyInfos(ByRef Errors)
        Dim dt As DataSet = New DataSet()
        Dim recRep As New ReceiptsRepository()
        dt = recRep.GetPolicyInfoDataSet(txtPolicyNo.Text, cmbReceiptType.SelectedValue)
        If dt.Tables(0).Rows().Count <> 0 Then
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("TBIL_POLY_ASSRD_CD")) Then _
                                           txtInsuredCodeH.Value = dt.Tables(0).Rows(0).Item("TBIL_POLY_ASSRD_CD")
            txtInsuredCode.Text = txtInsuredCodeH.Value

            If Not IsDBNull(dt.Tables(0).Rows(0).Item("TBIL_POLY_AGCY_CODE")) Then _
                                            txtAgentCodeH.Value = dt.Tables(0).Rows(0).Item("TBIL_POLY_AGCY_CODE")

            txtAgentCode.Text = txtAgentCodeH.Value
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("Insured_Name")) Then _
                                            txtAssuredName.Text = dt.Tables(0).Rows(0).Item("Insured_Name")
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("Insured_Address")) Then _
                                            txtAssuredAddress.Text = dt.Tables(0).Rows(0).Item("Insured_Address")
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("Insured_Name")) Then _
                                            txtPayeeName.Text = dt.Tables(0).Rows(0).Item("Insured_Name")
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("TBIL_POL_PRM_DTL_MOP_PRM_LC")) Then
                txtPolRegularContrib.Text = Format(dt.Tables(0).Rows(0).Item("TBIL_POL_PRM_DTL_MOP_PRM_LC"), "Standard")
                txtPolRegularContribH.Value = txtPolRegularContrib.Text
            End If
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("Agent_Name")) Then _
                                            txtAgentName.Text = dt.Tables(0).Rows(0).Item("Agent_Name")
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("Payment_Mode")) Then _
                                            txtMOPH.Value = dt.Tables(0).Rows(0).Item("Payment_Mode")
            txtMOP.Text = txtMOPH.Value
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("Payment_Mode_Desc")) Then _
                                            txtMOPDesc.Text = dt.Tables(0).Rows(0).Item("Payment_Mode_Desc")
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("File_No")) Then _
                                            txtFileNo.Text = dt.Tables(0).Rows(0).Item("File_No")
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("Product_Code")) Then _
                                            txtProduct.Text = dt.Tables(0).Rows(0).Item("Product_Code")
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("TBIL_POLICY_EFF_DT")) Then _
                                            txtPolicyEffDate.Text = dt.Tables(0).Rows(0).Item("TBIL_POLICY_EFF_DT")
            'txtEffectiveDate.Text = txtPolicyEffDate.Text

            'If cmbReceiptType.SelectedValue = "P" Or cmbReceiptType.SelectedValue = "D" Then
            '    If (txtInsuredCode.Text = "" Or txtAgentCode.Text = "" Or txtPolRegularContrib.Text = "0.00" Or txtMOP.Text = "") Then
            '        Dim message = "Please contact technical department, record not completed for " & lblRefNo.Text & "no " & txtPolicyNo.Text
            '        ErrorInd = "Y"
            '        publicMsgs = "javascript:alert('" + message + "')"
            '        txtPolicyNo.Focus()
            '        Exit Sub
            '    End If
            'End If
        End If

    End Sub
    Private Sub GetAcctDescriptionDR()
        Dim dt As DataSet = New DataSet()
        Dim recRep As New ReceiptsRepository()
        dt = recRep.GetAccountChartDetailsDataSet(txtSubAcctDebit.Text, txtMainAcctDebit.Text)
        If dt.Tables(0).Rows().Count <> 0 Then
            txtMainAcctDebitDesc.Text = dt.Tables(0).Rows(0).Item("sMainDesc")
            txtSubAcctDebitDesc.Text = dt.Tables(0).Rows(0).Item("sSubDesc")
        End If
    End Sub
    Protected Sub initializeFields()
        'txtReceiptNo.Enabled = False
        txtPolRegularContribH.Value = "0.00"
        txtMainAcctDebitDescH.Value = ""
        txtAgentCode.Text = String.Empty
        txtReceiptAmtFC.Text = String.Empty
        txtReceiptAmtLC.Text = String.Empty

        'txtBankGLCode.Text = String.Empty
        txtBatchNo.Text = String.Empty
        'cmbBranchCode.SelectedIndex = 0
        cmbBranchCode.Text = "1501"
        'txtChequeDate.Text = String.Empty


        'txtChequeNo.Text = String.Empty
        txtTellerNo.Text = String.Empty
        cmbCommissions.SelectedIndex = 0
        'txtCompanyCode.Text = "001"

        cmbReceiptType.SelectedIndex = 0
        'cmbReceiptCode.Text = String.Empty
        txtPolicyNo.Text = String.Empty
        ' txtEntryDate.Text = String.Empty
        txtInsuredCode.Text = String.Empty

        'txtMainAcctCredit.Text = String.Empty
        txtMainAcctDebit.Text = String.Empty
        txtPayeeName.Text = String.Empty
        cmbMode.SelectedIndex = 0
        txtPolRegularContrib.Text = String.Empty

        txtMOP.Text = String.Empty
        cmbReceiptType.SelectedIndex = 0
        txtPolicyNo.Text = String.Empty
        txtSerialNo.Text = String.Empty
        'txtSubAcctCredit.Text = String.Empty
        txtSubAcctDebit.Text = String.Empty

        'txtTempReceiptNo.Text = String.Empty
        txtTransDesc1.Text = String.Empty
        txtTransDesc2.Text = String.Empty
        txtEffectiveDate.Text = String.Empty
        cmbTransType.SelectedIndex = 0
        cmbCurrencyType.SelectedIndex = 0

        'txtReceiptNo.Text = String.Empty
        txtSubAcctDebitDesc.Text = String.Empty
        'txtSubAcctCreditDesc.Text = String.Empty

        txtMode.Text = String.Empty
        'txtBranchCode.Text = String.Empty
        txtReceiptCode.Text = String.Empty
        'txtCurrencyCode.Text = String.Empty
        txtMainAcctDebitDesc.Text = String.Empty
        txtAssuredName.Text = String.Empty
        txtAgentName.Text = String.Empty
        txtAssuredAddress.Text = String.Empty
        txtMOPDesc.Text = String.Empty
        cmbBranchCode.SelectedValue = "1501"
        txtBranchCode.Text = cmbBranchCode.SelectedValue
        txtCurrencyCode.Text = cmbCurrencyType.SelectedValue

        'txtTempInsName.Text = String.Empty
        'txtTempPolNo.Text = String.Empty
        cboProductClass.SelectedIndex = 0
        cboProduct.SelectedIndex = 0
        txtProductClass.Text = cboProductClass.SelectedValue
        txtProduct.Text = cboProduct.SelectedValue
        txtEntryDate.Text = Format(Now, "dd/MM/yyyy")
        txtPlan_Num.Text = String.Empty
        txtCover_Num.Text = String.Empty

        'notFound.Visible = True
        updateFlag = False
        Session("updateFlag") = updateFlag 'ready for a new record
        detailEdit = "N"
        Session("detailEdit") = detailEdit
        swEntry = "H"
        Session("swEntry") = swEntry


        ' grdData.DataBind()

    End Sub
    Private Sub SetComboBinding(ByVal toBind As ListControl, ByVal dataSource As Object, ByVal displayMember As String, ByVal valueMember As String)
        toBind.DataTextField = displayMember
        toBind.DataValueField = valueMember
        toBind.DataSource = dataSource
        toBind.DataBind()
    End Sub

    Protected Sub cmdDelete_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelete_ASP.Click
        Dim msg As String = String.Empty
        Rceipt = CType(Session("Rceipt"), CustodianLife.Model.Receipts)
        rcRepo = CType(Session("rcRepo"), ReceiptsRepository)
        Try
            rcRepo.Delete(Rceipt)
        Catch ex As Exception
            msg = ex.Message
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
        End Try
        initializeFields()
        txtReceiptNo.Enabled = False
    End Sub
    Private Function ValidDate(ByVal DateValue As String) As DateTime
        Dim dateparts() As String = DateValue.Split(Microsoft.VisualBasic.ChrW(47))
        Dim strDateTest As String = dateparts(1) & "/" & dateparts(0) & "/" & dateparts(2)
        Dim dateIn As Date = Format(CDate(strDateTest), "MM/dd/yyyy")
        Return dateIn
    End Function
    Private Function ValidDateFromDB(ByVal DateValue As Date) As String
        Dim dateparts() As String = DateValue.Date.ToString.Split(Microsoft.VisualBasic.ChrW(47))
        Dim strDateTest As String = dateparts(1) & "/" & dateparts(0) & "/" & Left(dateparts(2), 4)
        Return strDateTest
    End Function

    <System.Web.Services.WebMethod()> _
Public Shared Function PaymentsPeriodCover(ByVal _polnum As String, ByVal _mop As String, ByVal _effdate As String, ByVal _contrib As String, ByVal _amtpaid As String) As String
        Dim paycover As String = String.Empty
        Dim rRepo As New ReceiptsRepository()

        Try
            paycover = rRepo.GetPaymentCover(_polnum, _mop, _effdate, _contrib, CDbl(_amtpaid))
            Return paycover
        Finally
            If paycover = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetBranchInformation(ByVal _branchcode As String) As String
        Dim branchinfo As String = String.Empty
        Dim recRepo As New ReceiptsRepository()
        'Dim crit As String = 

        Try
            branchinfo = recRepo.GetBranchInfo(_branchcode)
            Return branchinfo
        Finally
            If branchinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function

    <System.Web.Services.WebMethod()> _
     Public Shared Function GetCurrencyInformation(ByVal _currencycode As String) As String
        Dim currencycode As String = String.Empty
        Dim recRepo As New ReceiptsRepository()
        'Dim crit As String = 

        Try
            currencycode = recRepo.GetCurrencyType(_currencycode)
            Return currencycode
        Finally
            If currencycode = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPolicyInformation(ByVal _polnum As String, ByVal _type As String) As String
        Dim polinfos As String = String.Empty
        Dim recRepo As New ReceiptsRepository()
        'Dim crit As String = 

        Try
            polinfos = recRepo.GetPolicyInfo(_polnum, _type)
            Return polinfos
        Finally
            If polinfos = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetAccountChartDetails(ByVal _accountsubcode As String, ByVal _accountmaincode As String) As String
        Dim acodes As String = String.Empty
        Dim recRepo As New ReceiptsRepository()
        'Dim crit As String = 

        Try
            acodes = recRepo.GetAccountChartDetails(_accountsubcode, _accountmaincode)
            Return acodes
        Finally
            If acodes = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try

    End Function

    <System.Web.Services.WebMethod()> _
     Public Shared Function GetProductByCatCodeClient(ByVal _catCode As String) As String
        Dim ProductDetails As String = String.Empty
        Dim prodEnq As New ProductDetailsRepository()
        'Dim crit As String = 

        Try
            ProductDetails = prodEnq.GetProductByCatCodeClient(_catCode)
            Return ProductDetails
        Finally
            If ProductDetails = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function

    Protected Sub txtReceiptAmtLC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReceiptAmtLC.TextChanged
        If ((txtReceiptAmtLC.Text <> "") And IsNumeric(txtReceiptAmtLC.Text)) Then
            txtReceiptAmtLC.Text = Format(txtReceiptAmtLC.Text, "Standard")
            txtReceiptAmtFC.Text = txtReceiptAmtLC.Text
            txtTransDesc2.Text = ""
            If cmbReceiptType.SelectedValue = "P" Or cmbReceiptType.SelectedValue = "D" Then
                Dim dt As DataSet = New DataSet()
                Dim recRep1 As New ReceiptsRepository()
                If txtPolRegularContrib.Text = "" Or txtPolRegularContrib.Text = "0.00" Then
                    'lblError.Text = "Regular Contrib cannot be 0 or empty, contact technical dept"
                    'lblError.Visible = True
                    'publicMsgs = "javascript:alert('" + lblError.Text + "')"
                    'Exit Sub
                Else
                    dt = recRep1.GetPaymentCoverDataSet(txtPolicyNo.Text, txtMOPH.Value, txtPolicyEffDate.Text, CDbl(txtPolRegularContrib.Text), CDbl(txtReceiptAmtLC.Text))
                    If dt.Tables(0).Rows().Count <> 0 Then
                        If Not IsDBNull(dt.Tables(0).Rows(0).Item("sPeriodsCoverRange")) Then _
                                                        txtTransDesc2.Text = dt.Tables(0).Rows(0).Item("sPeriodsCoverRange")
                    Else
                        lblError.Text = "Load Periods Cover Not Found. Parameters Empty or Invalid. Please Re-Confirm"
                        lblError.Visible = True
                        publicMsgs = "javascript:alert('" + lblError.Text + "')"
                        Exit Sub
                    End If
                End If
            End If

        End If
        GetPolicyInfos(Err)
        txtDetailDocNo.Text = txtTellerNo.Text
        cmbMode.SelectedValue = "T"
        txtDetailDate.Text = txtEffectiveDate.Text
        txtTransAmt.Text = txtReceiptAmtLC.Text
    End Sub

    Protected Sub chkReceiptNo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkReceiptNo.CheckedChanged
        If chkReceiptNo.Checked Then
            txtReceiptNo.Enabled = True
        Else
            txtReceiptNo.Enabled = False
        End If
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        txtReceiptNo.Text = String.Empty
        initializeFields()
        txtReceiptNo.Enabled = False
    End Sub

    Protected Sub txtReceiptNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReceiptNo.TextChanged
        If txtReceiptNo.Text <> "" Then
            initializeFields()
            lblError.Text = ""
            rcRepo = CType(Session("rcRepo"), ReceiptsRepository)
            Rceipt = rcRepo.GetByReceiptNo(Trim(txtReceiptNo.Text)).Item(0)
            'Rceipt = rcRepo.GetById("Receipt", txtReceiptNo.Text.Trim, 1)
            Session("Rceipt") = Rceipt
            If Rceipt IsNot Nothing Then
                'txtReceiptNo.Enabled = True
                txtReceiptNo.Enabled = False
                chkReceiptNo.Checked = False
                txtAgentCode.Text = Rceipt.AgentCode
                'txtReceiptAmtFC.Text = Math.Round(Rceipt.TotalAmountFC, 2).ToString()
                'txtReceiptAmtLC.Text = Math.Round(Rceipt.TotalAmountLC, 2).ToString()

                txtReceiptAmtFC.Text = Format(Rceipt.TotalAmountFC, "Standard")
                txtReceiptAmtLC.Text = Format(Rceipt.TotalAmountLC, "Standard")

                'txtBankGLCode.Text = Rceipt.BankCode
                txtBatchNo.Text = Rceipt.BatchNo
                cmbBranchCode.SelectedValue = Rceipt.BranchCode
                'txtChequeDate.Text = ValidDateFromDB(Rceipt.ChequeDate)


                'txtChequeNo.Text = Rceipt.ChequeInwardNo
                txtTellerNo.Text = Rceipt.TellerNo
                cmbCommissions.SelectedValue = Rceipt.CommissionApplicable
                'txtCompanyCode.Text = Rceipt.CompanyCode

                cmbCurrencyType.SelectedValue = Rceipt.CurrencyType
                txtEntryDate.Text = ValidDateFromDB(Rceipt.EntryDate)
                txtInsuredCode.Text = Rceipt.InsuredCode

                'txtMainAcctCredit.Text = Rceipt.MainAccountCredit
                txtMainAcctDebit.Text = Rceipt.MainAccountDebit
                txtPayeeName.Text = Rceipt.PayeeName
                'cmbMode.SelectedValue = Rceipt.PolicyPaymentMode
                'cmbMode.Text = Rceipt.PolicyPaymentMode
                ' txtPolRegularContrib.Text = Math.Round(Rceipt.PolicyRegularContribution, 2)
                txtPolRegularContrib.Text = Format(Rceipt.PolicyRegularContribution, "Standard")

                txtMOP.Text = Rceipt.PolicyPaymentMode
                cmbReceiptType.SelectedValue = Rceipt.ReceiptType
                txtQuote_Num.Text = Rceipt.ReferenceNo
                txtReceiptNo.Text = Rceipt.DocNo


                txtSerialNo.Text = Rceipt.SerialNo
                'txtSubAcctCredit.Text = Rceipt.SubAccountCredit
                'txtSubAcctDebit.Text = Rceipt.SubAccountDebit

                'If Rceipt.SubAccountCredit = "" Then
                '    txtSubAcctCredit.Text = "000000"
                'Else
                '    txtSubAcctCredit.Text = Rceipt.SubAccountCredit
                'End If
                If Rceipt.SubAccountDebit = "" Then
                    txtSubAcctDebit.Text = "000000"
                Else
                    txtSubAcctDebit.Text = Rceipt.SubAccountDebit
                End If

                'txtTempReceiptNo.Text = Rceipt.TempTransNo
                txtTransDesc1.Text = Rceipt.TranDescription1
                txtTransDesc2.Text = Rceipt.TranDescription2
                txtEffectiveDate.Text = ValidDateFromDB(Rceipt.TransDate)
                cmbMode.SelectedValue = Rceipt.TransMode
                cmbTransType.SelectedValue = Rceipt.TransType
                cmbCurrencyType.SelectedValue = Rceipt.CurrencyType

                txtPlan_Num.Text = Rceipt.ProductPlanNo
                txtCover_Num.Text = Rceipt.ProductCoverNo


                txtBranchCode.Text = cmbBranchCode.SelectedValue
                txtCurrencyCode.Text = cmbCurrencyType.SelectedValue
                txtReceiptCode.Text = cmbReceiptType.SelectedValue
                txtMode.Text = cmbMode.SelectedValue

                txtFileNo.Text = Rceipt.FileNo
                txtProduct.Text = Rceipt.ProductCode
                'txtTempPolNo.Text = Rceipt.TempPolicyNo
                'txtTempInsName.Text = Rceipt.TempInsuredName
                cboProductClass.SelectedValue = Rceipt.TempProdCat
                txtProductClass.Text = cboProductClass.SelectedValue
                prodEnq = New ProductDetailsRepository
                SetComboBinding(cboProduct, prodEnq.GetProductByCatCode(cboProductClass.SelectedValue), "ProductDesc", "ProductCode")
                cboProduct.SelectedValue = Rceipt.TempProdCode
                txtProduct.Text = cboProduct.SelectedValue
                'If txtTempInsName.Text = "" Then
                '    notFound.Visible = False
                'End If

                updateFlag = True
                Session("updateFlag") = updateFlag

                If txtMainAcctDebit.Text <> "" Then
                    GetAcctDescriptionDR()
                End If

                'If txtMainAcctCredit.Text <> "" Then
                '    GetAcctDescriptionCR()
                'End If
                Err = ""
                If (txtReceiptNo.Text <> "" And (cmbReceiptType.SelectedValue = "P" Or cmbReceiptType.SelectedValue = "D")) Then
                    GetPolicyInfos(Err)
                    If Err = "Y" Then
                        Exit Sub
                    End If
                End If
            End If
        End If

        fillValues()
    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim pvURL As String = ""
        Dim msg As String = String.Empty
        TotTransAmt = Session("TotTransAmt")




        pvURL = "prg_li_indv_poly_benefry.aspx?optfileid=" & Trim(Me.txtFileNo.Text)
        Select Case Trim(Me.txtProduct.Text)
            Case "F001", "F002"
                pvURL = "prg_li_indv_poly_funeral.aspx?optfileid=" & Trim(Me.txtFileNo.Text)
        End Select
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolicyNo.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)

        If Convert.ToDouble(txtReceiptAmtLC.Text) = Convert.ToDouble(TotTransAmt) Then
            Response.Redirect(pvURL)
        Else
            msg = "The Total Receipt Amount MUST be equal to the Detailed Analysis. Please Rectify..."
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
        End If

    End Sub

    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Dim pvURL As String = "prg_li_indv_poly_prem.aspx?optfileid=" & Trim(Me.txtFileNo.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolicyNo.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)
    End Sub
    Private Function Proc_DoOpenRecord() As String
        GetPolicyInfos(Err)
        Proc_DataBind()
        Return String.Empty
    End Function

    Private Sub Proc_DataBind()
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
        strSQL = "SPIL_GET_RECEIPT_HISTORY"

        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
        objDA.SelectCommand.CommandType = CommandType.StoredProcedure
        objDA.SelectCommand.Parameters.Clear()

        objDA.SelectCommand.Parameters.Add("p01", OleDbType.VarChar, 50).Value = "D"
        objDA.SelectCommand.Parameters.Add("p02", OleDbType.VarChar, 50).Value = Trim(txtQuote_Num.Text)

        Dim objDS As DataSet = New DataSet()
        objDA.Fill(objDS)
        With GridView1
            .DataSource = objDS
            .DataBind()
        End With

        'Execute this if no payment history found with proposal no.
        If GridView1.Rows.Count = 0 Then
            objDA = New OleDbDataAdapter(strSQL, objOLEConn)
            objDA.SelectCommand.CommandType = CommandType.StoredProcedure
            objDA.SelectCommand.Parameters.Clear()
            objDA.SelectCommand.Parameters.Add("p01", OleDbType.VarChar, 50).Value = "P"
            objDA.SelectCommand.Parameters.Add("p02", OleDbType.VarChar, 50).Value = Trim(txtQuote_Num.Text)
            objDA.Fill(objDS)
            With GridView1
                .DataSource = objDS
                .DataBind()
            End With
        End If

        If GridView1.Rows.Count = 0 Then
            objDA = New OleDbDataAdapter(strSQL, objOLEConn)
            objDA.SelectCommand.CommandType = CommandType.StoredProcedure
            objDA.SelectCommand.Parameters.Clear()
            objDA.SelectCommand.Parameters.Add("p01", OleDbType.VarChar, 50).Value = "P"
            objDA.SelectCommand.Parameters.Add("p02", OleDbType.VarChar, 50).Value = Trim(txtPolicyNo.Text)
            objDA.Fill(objDS)
            With GridView1
                .DataSource = objDS
                .DataBind()
            End With
        End If
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
        Dim TotalPremPaid = 0
        If GridView1.Rows.Count > 0 Then
            For P = 0 To Me.GridView1.Rows.Count - 1
                TotalPremPaid += GridView1.Rows(0).Cells(3).Text
            Next
        End If
    End Sub

    Protected Sub butSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSaveDetail.Click

    End Sub

    Protected Sub grdData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdData.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lblPrice As Label = CType(e.Row.FindControl("lblTransAmt"), Label)
            TransAmt = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DetailTransAmountLC")))
            TotTransAmt = (TotTransAmt + TransAmt)

            Session("TotTransAmt") = TotTransAmt
        End If
        If (e.Row.RowType = DataControlRowType.Footer) Then
            Dim lblTotal As Label = CType(e.Row.FindControl("lbltxtTotal"), Label)
            lblTotal.Text = Math.Round(TotTransAmt, 2).ToString
        End If
    End Sub

    Protected Sub grdData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdData.SelectedIndexChanged

    End Sub

    Protected Sub butNewDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNewDetail.Click
        GetPolicyInfos(Err)
        initializeDetailFields()

    End Sub

    Protected Sub butDeleteDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDeleteDetail.Click
        Dim msg As String = String.Empty
        Rceipt = CType(Session("Rceipt"), CustodianLife.Model.Receipts)
        rcRepo = CType(Session("rcRepo"), ReceiptsRepository)
        Try
            rcRepo.Delete(Rceipt)
            msg = "Delete Successful"
            lblError.Text = msg
            grdData.DataBind()

        Catch ex As Exception
            msg = ex.Message
            lblError.Text = msg
            lblError.Visible = True
            publicMsgs = "javascript:alert('" + msg + "')"
        End Try
        initializeDetailFields()


    End Sub
End Class
