Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class I_LIFE_PRG_LI_INDV_POLY_PREM
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    'Protected STRPAGE_TITLE As String
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

    ' additional premium variable
    Dim dblAdd_Prem_Amt As Double = 0
    Dim dblAdd_Prem_Rate As Double = 0
    Dim dblAdd_Rate_Per As Integer = 0
    Dim dblAdd_Prem_SA_LC As Double
    Dim dblAdd_Prem_SA_FC As Double

    Dim dblTotal_Add_Prem_LC As Double = 0
    Dim dblTotal_Add_Prem_FC As Double = 0

    Dim dblTotal_Prem_LC As Double = 0
    Dim dblTotal_Prem_FC As Double = 0

    Dim dblAnnual_Basic_Prem_LC As Double = 0
    Dim dblAnnual_Basic_Prem_FC As Double = 0

    Dim dblTmp_Amt As Double = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_POLICY_PREM_INFO"

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


        If Not (Page.IsPostBack) Then
            Me.lblMsg.Text = "Status:"

            Me.optPrem_Applied_SA.Checked = True
            Me.optPrem_Applied_Prem.Checked = False
            Me.txtPrem_Rate_Applied_On.Text = "S"
            Me.txtPrem_Is_SA_From_PremNum.Text = "N"
            'Call gnProc_DDL_Get(Me.cboPrem_Is_SA_From_Prem, RTrim(Me.txtPrem_Is_SA_From_PremNum.Text))

            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False
            Call gnProc_Populate_Box("IL_CODE_LIST", "017", Me.cboPrem_SA_Currency)
            Me.cboPrem_MOP_Type.Items.Clear()
            Call gnProc_Populate_Box("IL_MOP_FACTOR_LIST", "IND", Me.cboPrem_MOP_Type)

            If Trim(strF_ID) <> "" Then
                Me.txtFileNum.Text = RTrim(strF_ID)
                Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                If oAL.Item(0) = "TRUE" Then
                    '    'Retrieve the record
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
                    Me.txtQuote_Num.Text = oAL.Item(3)
                    Me.txtPolNum.Text = oAL.Item(4)
                    Me.txtProductClass.Text = oAL.Item(5)
                    Me.txtProduct_Num.Text = oAL.Item(6)
                    Me.txtPlan_Num.Text = oAL.Item(7)
                    Me.txtCover_Num.Text = oAL.Item(8)
                    Me.txtDOB.Text = oAL.Item(9)
                    Me.txtDOB_ANB.Text = oAL.Item(10)
                    Me.cmdNext.Enabled = True

                    If UCase(oAL.Item(18).ToString) = "A" Then
                        Me.cmdNew_ASP.Visible = False
                        'Me.cmdSave_ASP.Visible = False
                        Me.cmdDelete_ASP.Visible = False
                        Me.cmdPrint_ASP.Visible = False
                    End If

                    If Me.txtProduct_Num.Text = "E003" Then
                        cboPrem_Life_Cover.SelectedValue = "Y"
                        cboPrem_Bonus_YN.SelectedValue = "Y"
                        cboPrem_Allocation_YN.SelectedValue = "N"
                    End If


                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If

            Call gnProc_Populate_Box("IL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPrem_Rate_Code)

            If Trim(strF_ID) <> "" Then
                strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
            End If
            'If Trim(strQ_ID) <> "" Then
            '    Me.txtQuote_Num.Text = RTrim(strQ_ID)
            'End If
            'If Trim(strP_ID) <> "" Then
            '    Me.txtPolNum.Text = RTrim(strP_ID)
            'End If

            If Trim(Me.txtProduct_Num.Text) = "E001" Then
                Me.lblPrem_School_Term.Enabled = True
                Me.txtPrem_School_Term.Enabled = True
                Me.txtPrem_School_Term_Name.Enabled = True
                Me.lblPrem_Sch_Fee_Prd.Enabled = False
                Me.txtPrem_Sch_Fee_Prd.Enabled = False
            Else
                Me.lblPrem_School_Term.Enabled = False
                Me.txtPrem_School_Term.Enabled = False
                Me.txtPrem_School_Term_Name.Enabled = False
                Me.lblPrem_Sch_Fee_Prd.Enabled = False
                Me.txtPrem_Sch_Fee_Prd.Enabled = False
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

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call Proc_DoSave()
        Me.txtAction.Text = ""

    End Sub

    Private Sub DoGet_SelectedItem(ByVal pvDDL_Control As DropDownList, ByVal pvCtr_Value As TextBox, ByVal pvCtr_Text As TextBox, Optional ByVal pvCtr_Label As Label = Nothing)
        Try
            If pvDDL_Control.SelectedIndex = -1 Or pvDDL_Control.SelectedIndex = 0 Or _
            pvDDL_Control.SelectedItem.Value = "" Or pvDDL_Control.SelectedItem.Value = "*" Then
                pvCtr_Value.Text = ""
                pvCtr_Text.Text = ""
            Else
                pvCtr_Value.Text = pvDDL_Control.SelectedItem.Value
                pvCtr_Text.Text = pvDDL_Control.SelectedItem.Text
            End If
        Catch ex As Exception
            If pvCtr_Label IsNot Nothing Then
                If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                    pvCtr_Label.Text = "Error. Reason: " & ex.Message.ToString
                End If
            End If
        End Try

    End Sub

    Protected Sub DoProc_Currency_Code_Change()
        Me.lblMsg.Text = ""
        Me.txtPrem_Start_Date.Text = Trim(Me.txtPrem_Start_Date.Text)
        If RTrim(Me.txtPrem_Start_Date.Text) = "" Or Len(Trim(Me.txtPrem_Start_Date.Text)) <> 10 Then
            Me.cboPrem_SA_Currency.SelectedIndex = -1
            Me.lblMsg.Text = "Sorry. You must enter the TRANSACTION START DATE before the " & Me.lblPrem_SA_Currency.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim strMyYear As String = ""
        Dim strMyMth As String = ""
        Dim strMyDay As String = ""

        Dim strMyDte As String = ""
        Dim strMyDteX As String = ""

        Dim dteStart As Date = Now

        Dim mydteX As String = ""
        Dim mydte As Date = Now

        'Validate date
        myarrData = Split(Me.txtPrem_Start_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.cboPrem_SA_Currency.SelectedIndex = -1
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblPrem_Start_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.cboPrem_SA_Currency.SelectedIndex = -1
            Me.lblMsg.Text = "Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If
        Me.txtPrem_Start_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")


        Call DoGet_SelectedItem(Me.cboPrem_SA_Currency, Me.txtPrem_SA_CurrencyCode, Me.txtPrem_SA_CurrencyName, Me.lblMsg)
        If Trim(Me.txtPrem_SA_CurrencyCode.Text) = "" Then
            Me.txtPrem_Exchange_Rate.Text = "0.00"
        Else
            Dim myRetValue As String = "0"
            myRetValue = MOD_GEN.gnGET_RATE("GET_IL_EXCHANGE_RATE", "IND", Me.txtPrem_SA_CurrencyCode.Text, Format(dteStart, "MM/dd/yyyy"), RTrim(""), RTrim(""), Me.lblMsg)
            If Left(LTrim(myRetValue), 3) = "ERR" Then
                'Me.cboPrem_SA_Currency.SelectedIndex = -1
                Me.txtPrem_Exchange_Rate.Text = "0.00"
            Else
                Me.txtPrem_Exchange_Rate.Text = myRetValue.ToString
            End If
        End If


    End Sub


    Protected Sub DoProc_Premium_Code_Change()
        Call DoGet_SelectedItem(Me.cboPrem_Rate_Code, Me.txtPrem_Rate_Code, Me.txtPrem_Rate_CodeName, Me.lblMsg)
        If Trim(Me.txtPrem_Rate_Code.Text) = "" Then
            Me.txtPrem_Rate.Text = "0.00"
            Me.txtPrem_Rate_Per.Text = "0"
        Else
            Dim myRetValue As String = "0"
            Dim myTerm As String = ""
            myTerm = Me.txtPrem_Period_Yr.Text
            Select Case UCase(Me.txtProduct_Num.Text)
                Case "P005"
                    myTerm = "1"
                Case "F001", "F002"
                    myTerm = "1"
            End Select

            'Response.Write("<BR/>Rate Code: " & Me.txtPrem_Rate_Code.Text)
            'Response.Write("<BR/>Product Code: " & Me.txtProduct_Num.Text)
            'Response.Write("<BR/>Period: " & myTerm)
            'Response.Write("<BR/>Age: " & Me.txtDOB_ANB.Text)

            myRetValue = MOD_GEN.gnGET_RATE("GET_IL_PREMIUM_RATE", "I", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Me.txtDOB_ANB.Text, Me.lblMsg, Me.txtPrem_Rate_Per)
            If Left(LTrim(myRetValue), 3) = "ERR" Then
                Me.cboPrem_Rate_Code.SelectedIndex = -1
                Me.txtPrem_Rate.Text = "0.00"
                Me.txtPrem_Rate_Per.Text = "0"
            Else
                Me.txtPrem_Rate.Text = myRetValue.ToString
            End If
        End If

    End Sub

    Protected Sub DoProc_Rate_Type_Change()
        'Me.lblPrem_Rate_Applied_On.Enabled = False
        'Me.optPrem_Applied_SA.Enabled = False
        'Me.optPrem_Applied_SA.Checked = False
        'Me.optPrem_Applied_Prem.Enabled = False
        'Me.optPrem_Applied_Prem.Checked = False

        Call DoGet_SelectedItem(Me.cboPrem_Rate_Type, Me.txtPrem_Rate_TypeNum, Me.txtPrem_Rate_TypeName, Me.lblMsg)
        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "F" Then
            Me.lblPrem_Fixed_Rate.Enabled = True
            Me.txtPrem_Fixed_Rate.Enabled = True
            Me.lblPrem_Fixed_Rate_Per.Enabled = True
            Me.cboPrem_Fixed_Rate_Per.Enabled = True
            Me.lblPrem_Rate_Code.Enabled = False
            Me.cboPrem_Rate_Code.Enabled = False
            'Me.txtPrem_Rate_Code.Enabled = False
            'Me.lblPrem_Rate.Enabled = False
            'Me.txtPrem_Rate.Enabled = False
            'Me.txtPrem_Rate_Per.Enabled = False
        ElseIf Trim(Me.txtPrem_Rate_TypeNum.Text) = "N" Then
            Me.lblPrem_Fixed_Rate.Enabled = False
            Me.txtPrem_Fixed_Rate.Enabled = False
            Me.lblPrem_Fixed_Rate_Per.Enabled = False
            Me.cboPrem_Fixed_Rate_Per.Enabled = False
            Me.lblPrem_Rate_Code.Enabled = False
            Me.cboPrem_Rate_Code.Enabled = False
            'Me.txtPrem_Rate_Code.Enabled = False
            'Me.lblPrem_Rate.Enabled = False
            'Me.txtPrem_Rate.Enabled = False
            'Me.txtPrem_Rate_Per.Enabled = False
        ElseIf Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
            Me.lblPrem_Fixed_Rate.Enabled = False
            Me.txtPrem_Fixed_Rate.Enabled = False
            Me.lblPrem_Fixed_Rate_Per.Enabled = False
            Me.cboPrem_Fixed_Rate_Per.Enabled = False
            Me.lblPrem_Rate_Code.Enabled = True
            Me.cboPrem_Rate_Code.Enabled = True
            'Me.txtPrem_Rate_Code.Enabled = True
            'Me.lblPrem_Rate.Enabled = True
            'Me.txtPrem_Rate.Enabled = True
            'Me.txtPrem_Rate_Per.Enabled = True
            'Me.lblPrem_Rate_Applied_On.Enabled = True
            'Me.optPrem_Applied_SA.Enabled = True
            'Me.optPrem_Applied_SA.Checked = True
            'Me.optPrem_Applied_Prem.Enabled = True
            'Me.optPrem_Applied_Prem.Checked = True

        End If

    End Sub

    Protected Sub DoProc_MOP_Change()
        Call DoGet_SelectedItem(Me.cboPrem_MOP_Type, Me.txtPrem_MOP_Type, Me.txtPrem_MOP_TypeName, Me.lblMsg)
        If Trim(Me.txtPrem_MOP_Type.Text) = "" Then
            Me.txtPrem_MOP_Rate.Text = "0.00"
            Me.txtPrem_MOP_TypeName.Text = ""
            Me.lblMsg.Text = "Missing " & Me.lblPrem_MOP_Type.Text
        Else
            Dim myRetValue As String = "0"
            myRetValue = MOD_GEN.gnGET_RATE("GET_IL_MOP_FACTOR", "IND", Me.txtPrem_MOP_Type.Text, "", "", "", Me.lblMsg, Me.txtPrem_MOP_TypeName)
            If Left(LTrim(myRetValue), 3) = "ERR" Then
                Me.cboPrem_MOP_Type.SelectedIndex = -1
                Me.txtPrem_MOP_Rate.Text = "0.00"
                Me.txtPrem_MOP_TypeName.Text = ""
            Else
                Me.txtPrem_MOP_Rate.Text = myRetValue.ToString
            End If
        End If

    End Sub

    Private Sub Proc_DoSave()

        'New fields
        'TBIL_POL_PRM_LIFE_COVER                     Is  there Life Cover   Y/N     (Char 3)
        'TBIL_POL_SA_FROM_PRM                         Is SA Calculated from Premium Y/N     (Char 3)
        'TBIL_POL_PRM_RT_TAB_FIX		 Premium rate -  F – Fixed rate  T  - Table rate (Char 5)
        'TBIL_POL_PRM_RT_FIXED		Fixed Rate to use (Numeric 4 decimal)
        'TBIL_POL_PRM_RT+FIX_PER		Fixed rate Per (100, 1000)   (Numeric no decimal)

        Dim strMyYear As String = ""
        Dim strMyMth As String = ""
        Dim strMyDay As String = ""

        Dim strMyDte As String = ""
        Dim strMyDteX As String = ""

        Dim dteStart As Date = Now
        Dim dteEnd As Date = Now

        Dim dteStart_RW As Date = Now
        Dim dteEnd_RW As Date = Now

        Dim mydteX As String = ""
        Dim mydte As Date = Now

        Dim dteDOB As Date = Now

        Dim lngDOB_ANB As Integer = 0
        Dim Dte_Proposal As Date = Now
        Dim Dte_Commence As Date = Now
        Dim Dte_DOB As Date = Now
        Dim Dte_Maturity As Date = Now

        Dim myTmp_RecStatus = CType(Session("myTmp_RecStatus"), String)

        Me.lblMsg.Text = ""

        If Me.txtFileNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtQuote_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'If Me.txtPolNum.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblPolNum.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        If Me.txtProductClass.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProduct.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtProduct_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProduct.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        Me.txtPrem_Period_Yr.Text = Trim(Me.txtPrem_Period_Yr.Text)
        If Me.txtPrem_Period_Yr.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPrem_Period_Yr.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Not IsNumeric(Me.txtPrem_Period_Yr.Text) Then
            Me.lblMsg.Text = "Invalid " & Me.lblPrem_Period_Yr.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtPrem_Start_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblPrem_Start_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)
        Me.txtPrem_Start_Date.Text = Trim(strMyDte)

        If RTrim(Me.txtPrem_Start_Date.Text) = "" Or Len(Trim(Me.txtPrem_Start_Date.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblPrem_Start_Date.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtPrem_Start_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblPrem_Start_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)

        blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
        If blnStatusX = False Then
            Me.lblMsg.Text = "Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If
        Me.txtPrem_Start_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")

        dteEnd = DateAdd(DateInterval.Year, Val(Me.txtPrem_Period_Yr.Text), dteStart)
        Me.txtPrem_End_Date.Text = Format(dteEnd, "dd/MM/yyyy")

        'VALIDATION for DATA NOT OLD START - first pass
        If myTmp_RecStatus <> "OLD" Then
            'Call gnGET_SelectedItem(Me.cboPrem_MOP_Type, Me.txtPrem_MOP_Type, Me.txtPrem_MOP_TypeName, Me.lblMsg)
            Call DoProc_MOP_Change()
            If Me.txtPrem_MOP_Type.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblPrem_MOP_Type.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            If Trim(Me.txtPrem_Start_Date.Text) <> "" Then
                Call DoProc_Currency_Code_Change()
            End If


            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_MOP_Rate)
            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Exchange_Rate)



            Select Case UCase(Me.txtProduct_Num.Text)
                Case "F001", "F002"
                    If Val(Me.txtPrem_Enrollee_Num.Text) = 0 Then
                        Me.lblMsg.Text = "Missing " & Me.lblPrem_Enrollee_Num.Text
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                        Exit Sub
                    End If
                Case Else
                    Me.txtPrem_Enrollee_Num.Text = "0"
            End Select

            Call gnGET_SelectedItem(Me.cboPrem_Allocation_YN, Me.txtPrem_Allocation_YN, Me.txtPrem_Allocation_YN_Name, Me.lblMsg)
            If Me.txtPrem_Allocation_YN.Text = "" Then
                'txtPrem_Allocation_YN.Text = "N"
                Me.lblMsg.Text = "Missing " & Me.lblPrem_Allocation_YN.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If


            Call gnGET_SelectedItem(Me.cboPrem_Bonus_YN, Me.txtPrem_Bonus_YN, Me.txtPrem_Bonus_YN_Name, Me.lblMsg)
            If Me.txtPrem_Bonus_YN.Text = "" Then
                'txtPrem_Bonus_YN.Text = "N"
                Me.lblMsg.Text = "Missing " & Me.lblPrem_Bonus_YN.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call gnGET_SelectedItem(Me.cboPrem_Rate_Type, Me.txtPrem_Rate_TypeNum, Me.txtPrem_Rate_TypeName, Me.lblMsg)
            If Me.txtPrem_Rate_TypeNum.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblPrem_Rate_Type.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "N" Then
                Me.txtPrem_Fixed_Rate.Text = "0"
                Me.txtPrem_Fixed_Rate_PerNum.Text = "1000"
                Me.txtPrem_Rate.Text = "0"
                Me.txtPrem_Rate_Per.Text = "1000"
            End If

            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
                Me.txtPrem_Fixed_Rate.Text = "0.00"
                Me.txtPrem_Fixed_Rate_PerNum.Text = "1000"
            End If
            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Fixed_Rate)
            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "F" And Val(Me.txtPrem_Fixed_Rate.Text) = 0 Then
                Me.lblMsg.Text = "Missing Fixed Rate. Please enter valid fixed rate..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call gnGET_SelectedItem(Me.cboPrem_Fixed_Rate_Per, Me.txtPrem_Fixed_Rate_PerNum, Me.txtPrem_Fixed_Rate_PerName, Me.lblMsg)
            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "F" And Val(Me.txtPrem_Fixed_Rate_PerNum.Text) = 0 Then
                Me.lblMsg.Text = "Missing " & Me.lblPrem_Fixed_Rate_Per.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

        End If '        'VALIDATION for DATA NOT OLD END- first pass

        Call gnGET_SelectedItem(Me.cboPrem_Life_Cover, Me.txtPrem_Life_CoverNum, Me.txtPrem_Life_CoverName, Me.lblMsg)
        If Me.txtPrem_Life_CoverNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPrem_Life_Cover.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        'Call gnGET_SelectedItem(Me.cboPrem_Rate_Code, Me.txtPrem_Rate_Code, Me.txtPrem_Rate_CodeName, Me.lblMsg)
        'Call DoProc_Premium_Code_Change()
        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
            Call DoProc_Premium_Code_Change()
        End If
        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Me.txtPrem_Rate_Code.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblPrem_Rate_Code.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Val(Me.txtPrem_Rate.Text) = 0 Then
            Me.lblMsg.Text = "Missing Premium Rate. Please select valid premium rate code..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Val(Me.txtPrem_Rate_Per.Text) = 0 Then
            Me.lblMsg.Text = "Missing Premium Rate per e.g 100 or 1000. Please select valid premium rate code..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If



        If Me.optPrem_Applied_SA.Checked = True Then
            Me.txtPrem_Rate_Applied_On.Text = "S"
            Me.txtPrem_Is_SA_From_PremNum.Text = "N"
        ElseIf Me.optPrem_Applied_Prem.Checked = True Then
            Me.txtPrem_Rate_Applied_On.Text = "P"
            Me.txtPrem_Is_SA_From_PremNum.Text = "Y"
        Else
            Me.txtPrem_Rate_Applied_On.Text = "S"
            Me.txtPrem_Is_SA_From_PremNum.Text = "N"
        End If
        'Call gnProc_DDL_Get(Me.cboPrem_Is_SA_From_Prem, RTrim(Me.txtPrem_Is_SA_From_PremNum.Text))

        'Call gnGET_SelectedItem(Me.cboPrem_Is_SA_From_Prem, Me.txtPrem_Is_SA_From_PremNum, Me.txtPrem_Is_SA_From_PremName, Me.lblMsg)
        'If Me.txtPrem_Is_SA_From_PremNum.Text = "" Then
        '    'txtPrem_Is_SA_From_PremNum.Text = "N"
        '    Me.lblMsg.Text = "Missing " & Me.lblPrem_Is_SA_From_Prem.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        Dim pvAMT As Double = 0
        pvAMT = 0

        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Ann_Contrib_LC)
        If Me.optPrem_Applied_Prem.Checked = True And Val(Me.txtPrem_Ann_Contrib_LC.Text) = 0 Then
            Me.lblMsg.Text = "Missing Premium Amount. Please enter valid premium amount..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Me.txtPrem_Ann_Contrib_FC.Text = Me.txtPrem_Ann_Contrib_LC.Text
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Ann_Contrib_FC)

        pvAMT = CType(Trim(Me.txtPrem_Ann_Contrib_LC.Text), Double)
        If pvAMT <> 0 Then
            Me.txtPrem_Mth_Contrib_LC.Text = Format(pvAMT / 12, "###########0.00")
            Me.txtPrem_Mth_Contrib_FC.Text = Me.txtPrem_Mth_Contrib_LC.Text
        Else
            txtPrem_Mth_Contrib_LC.Text = "0"
            Me.txtPrem_Mth_Contrib_FC.Text = Me.txtPrem_Mth_Contrib_LC.Text
        End If

        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Mth_Contrib_LC)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Mth_Contrib_FC)


        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_SA_LC)
        If Me.optPrem_Applied_SA.Checked = True And Val(Me.txtPrem_SA_LC.Text) = 0 Then
            Me.lblMsg.Text = "Missing Sum Assured. Please enter valid Sum Assured..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Me.txtPrem_SA_FC.Text = Me.txtPrem_SA_LC.Text
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_SA_FC)

        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Life_Cover_SA_LC)
        If Me.txtPrem_Life_CoverNum.Text = "N" Then
            Me.txtPrem_Life_CoverNum.Text = "N"
        Else
            If Val(Me.txtPrem_Life_Cover_SA_LC.Text) = 0 And Trim(Me.txtPrem_Rate_Applied_On.Text) = "S" Then
                Me.lblMsg.Text = "Missing Life Cover SA... - xxx"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        End If
        If Val(Me.txtPrem_Life_Cover_SA_LC.Text) = 0 And Trim(Me.txtPrem_Rate_Applied_On.Text) = "S" Then
            Me.lblMsg.Text = "Missing Life Cover SA..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Me.txtPrem_Life_Cover_SA_FC.Text = Me.txtPrem_Life_Cover_SA_LC.Text
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Life_Cover_SA_FC)


        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Free_Cover_Lmt_LC)
        Me.txtPrem_Free_Cover_Lmt_FC.Text = Me.txtPrem_Free_Cover_Lmt_LC.Text
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Free_Cover_Lmt_FC)

        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Rate)

        Me.txtPrem_No_Instal.Text = Me.txtPrem_No_Instal.Text
        If Not IsNumeric(Trim(Me.txtPrem_No_Instal.Text)) Then
            Me.txtPrem_No_Instal.Text = "0"
        End If

        If Trim(Me.txtProductClass.Text) = "P" And Val(Me.txtPrem_SA_LC.Text) <> 0 Then
            Me.txtPrem_Free_LiveCover_Lmt_LC.Text = Me.txtPrem_SA_LC.Text
        End If
        If Trim(Me.txtProductClass.Text) = "P" And Val(Me.txtPrem_SA_FC.Text) <> 0 Then
            Me.txtPrem_Free_LiveCover_Lmt_FC.Text = Me.txtPrem_SA_FC.Text
        End If

        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Free_LiveCover_Lmt_LC)
        Me.txtPrem_Free_LiveCover_Lmt_FC.Text = Me.txtPrem_Free_LiveCover_Lmt_LC.Text
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Free_LiveCover_Lmt_FC)


        'VALIDATION for DATA NOT OLD START
        If myTmp_RecStatus <> "OLD" Then

            Call gnGET_SelectedItem(Me.cboPrem_School_Term, Me.txtPrem_School_Term, Me.txtPrem_School_Term_Name, Me.lblMsg)
            Select Case Trim(Me.txtProduct_Num.Text)
                Case "E001"
                    If Trim(Me.txtPrem_School_Term.Text) = "" Then
                        Me.lblPrem_School_Term.Enabled = True
                        Me.cboPrem_School_Term.Enabled = True
                        Me.lblMsg.Text = "Missing " & Me.lblPrem_School_Term.Text
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                        Exit Sub
                    End If
                    'If Val(Me.txtPrem_Sch_Fee_Prd.Text) = 0 Then
                    '    Me.lblPrem_Sch_Fee_Prd.Enabled = True
                    '    Me.txtPrem_Sch_Fee_Prd.Enabled = True
                    '    Me.lblMsg.Text = "Missing " & Me.lblPrem_Sch_Fee_Prd.Text
                    '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    '    Exit Sub
                    'End If
                Case Else
                    'Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Sch_Fee_Prd)

            End Select

            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Discount_Rate)
            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Loading_Rate)

            Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Payable)

            Dim PremiumInstallmet As Double
            PremiumInstallmet = 0
            If Trim(cboPrem_MOP_Type.SelectedValue = "W") Then
                PremiumInstallmet = CDbl(txtPrem_Ann_Contrib_LC.Text) / 52
            ElseIf Trim(cboPrem_MOP_Type.SelectedValue = "M") Then
                PremiumInstallmet = CDbl(txtPrem_Mth_Contrib_LC.Text)
            ElseIf Trim(cboPrem_MOP_Type.SelectedValue = "Q") Then
                PremiumInstallmet = CDbl(txtPrem_Ann_Contrib_LC.Text) / 4
            ElseIf Trim(cboPrem_MOP_Type.SelectedValue = "H") Then
                PremiumInstallmet = CDbl(txtPrem_Ann_Contrib_LC.Text) / 2
            ElseIf Trim(cboPrem_MOP_Type.SelectedValue = "S") Then
                PremiumInstallmet = CDbl(txtPrem_Ann_Contrib_LC.Text)
            ElseIf Trim(cboPrem_MOP_Type.SelectedValue = "A") Then
                PremiumInstallmet = CDbl(txtPrem_Ann_Contrib_LC.Text)
            End If

            If IsNumeric(txtDepositAmount.Text) Then
                If Val(txtDepositAmount.Text) < PremiumInstallmet Then
                    Me.lblMsg.Text = "Deposit Amount must not be less than premium installment"
                    txtDepositAmount.Focus()
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                End If
            Else
                Me.lblMsg.Text = "Deposit Amount must be numeric"
                txtDepositAmount.Focus()
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If


        End If 'VALIDATION for DATA NOT OLD END

            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "N" Then
                Me.txtPrem_Fixed_Rate.Text = "0.00"
                Me.txtPrem_Fixed_Rate_PerNum.Text = "0"
                Me.txtPrem_Rate.Text = "0.00"
                Me.txtPrem_Rate_Per.Text = "0"
            End If

            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
                Me.txtPrem_Fixed_Rate.Text = "0.00"
                Me.txtPrem_Fixed_Rate_PerNum.Text = "1000"
            End If

            '============================================


            'Me.lblMsg.Text = "About to submit data... "
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

            'If RTrim(txtNum.Text) = "" Then
            '    Me.txtNum.Text = MOD_GEN.gnGet_Serial_Und("GET_SN_IL_UNDW", Trim(strP_ID), Trim(Me.txtGroupNum.Text), "XXXX", "XXXX", "")
            '    If Trim(txtNum.Text) = "" Or Trim(Me.txtNum.Text) = "0" Or Trim(Me.txtNum.Text) = "*" Then
            '        Me.txtNum.Text = ""
            '        Me.lblMessage.Text = "Sorry!. Unable to get the next record id. Please contact your service provider..."
            '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            '        Me.lblMessage.Text = "Status:"
            '        Exit Sub
            '    ElseIf Trim(Me.txtNum.Text) = "PARAM_ERR" Then
            '        Me.txtNum.Text = ""
            '        Me.lblMessage.Text = "Sorry!. Unable to get the next record id - INVALID PARAMETER(S) - " & Trim(strP_ID)
            '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            '        Me.lblMessage.Text = "Status:"
            '        Exit Sub
            '    ElseIf Trim(Me.txtNum.Text) = "DB_ERR" Then
            '        Me.txtNum.Text = ""
            '        Me.lblMessage.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
            '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            '        Me.lblMessage.Text = "Status:"
            '        Exit Sub
            '    ElseIf Trim(Me.txtNum.Text) = "ERR_ERR" Then
            '        Me.txtNum.Text = ""
            '        Me.lblMessage.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
            '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            '        Me.lblMessage.Text = "Status:"
            '        Exit Sub
            '    End If

            'End If


            '******************************************************
            ' START CODES - CALCULATE SUM ASSURED FROM PREMIUM
            '******************************************************

            Select Case Trim(Me.txtPrem_Rate_Applied_On.Text)
                Case "P"
                    Select Case UCase(Me.txtProduct_Num.Text)
                        Case "F001", "F002"
                        Case Else
                            If Val(Trim(Me.txtPrem_SA_LC.Text)) = 0 And Val(Trim(Me.txtPrem_SA_FC.Text)) = 0 And _
                               Val(Trim(Me.txtPrem_Life_Cover_SA_LC.Text)) = 0 And Val(Trim(Me.txtPrem_Life_Cover_SA_FC.Text)) = 0 Then
                                Call Proc_DoCalc_Prem()
                            End If
                    End Select
            End Select

            '******************************************************
            ' END CODES - CALCULATE SUM ASSURED FROM PREMIUM
            '******************************************************

            Dim myUserIDX As String = ""
            Try
                myUserIDX = CType(Session("MyUserIDX"), String)
            Catch ex As Exception
                myUserIDX = ""
            End Try


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
                Exit Sub
            End Try


            strTable = strTableName

            strSQL = ""
            strSQL = "SELECT TOP 1 * FROM " & strTable
            strSQL = strSQL & " WHERE TBIL_POL_PRM_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
            'If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
            '    strSQL = strSQL & " AND TBIL_POL_PRM_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
            'End If


            Dim objDA As System.Data.OleDb.OleDbDataAdapter
            objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
            'or
            'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

            Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
            m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

            Dim obj_DT As New System.Data.DataTable
            'Dim m_rwContact As System.Data.DataRow
            Dim intC As Integer = 0


            Try

                objDA.Fill(obj_DT)

                If obj_DT.Rows.Count = 0 Then
                    '   Creating a new record

                    Dim drNewRow As System.Data.DataRow
                    drNewRow = obj_DT.NewRow()

                    drNewRow("TBIL_POL_PRM_MDLE") = RTrim("I")

                    drNewRow("TBIL_POL_PRM_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    drNewRow("TBIL_POL_PRM_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    'drNewRow("TBIL_POL_PRM_POLY_NO") = RTrim(Me.txtPolNum.Text)

                    drNewRow("TBIL_POL_PRM_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)
                    drNewRow("TBIL_POL_PRM_PLAN_CD") = RTrim(Me.txtPlan_Num.Text)
                    'drNewRow("TBIL_POL_PRM_COVER_CD") = RTrim(Me.txtCover_Num.Text)

                    drNewRow("TBIL_POL_PRM_PERIOD_YRS") = Val(Trim(Me.txtPrem_Period_Yr.Text))

                    If Trim(Me.txtPrem_Start_Date.Text) <> "" Then
                        drNewRow("TBIL_POL_PRM_FROM") = dteStart
                    End If
                    If Trim(Me.txtPrem_End_Date.Text) <> "" Then
                        drNewRow("TBIL_POL_PRM_TO") = dteEnd
                    End If

                    drNewRow("TBIL_POL_PRM_MODE_PAYT") = RTrim(Me.txtPrem_MOP_Type.Text)
                    drNewRow("TBIL_POL_PRM_MOP_RATE") = RTrim(Me.txtPrem_MOP_Rate.Text)

                    drNewRow("TBIL_POL_PRM_SA_CURRCY") = Trim(Me.txtPrem_SA_CurrencyCode.Text)
                    drNewRow("TBIL_POL_PRM_EXCHG_RATE") = RTrim(Me.txtPrem_Exchange_Rate.Text)

                    drNewRow("TBIL_POL_PRM_ANN_CONTRIB_LC") = Trim(Me.txtPrem_Ann_Contrib_LC.Text)
                    drNewRow("TBIL_POL_PRM_ANN_CONTRIB_FC") = Trim(Me.txtPrem_Ann_Contrib_FC.Text)
                    drNewRow("TBIL_POL_PRM_MTH_CONTRIB_LC") = Trim(Me.txtPrem_Mth_Contrib_LC.Text)
                    drNewRow("TBIL_POL_PRM_MTH_CONTRIB_FC") = Trim(Me.txtPrem_Mth_Contrib_FC.Text)


                    'new fields
                    drNewRow("TBIL_POL_PRM_LIFE_COVER") = Trim(Me.txtPrem_Life_CoverNum.Text)
                    drNewRow("TBIL_POL_SA_FROM_PRM") = Trim(Me.txtPrem_Is_SA_From_PremNum.Text)
                    drNewRow("TBIL_POL_PRM_RT_TAB_FIX") = Trim(Me.txtPrem_Rate_TypeNum.Text)
                    drNewRow("TBIL_POL_PRM_RT_FIXED") = Trim(Me.txtPrem_Fixed_Rate.Text)
                    drNewRow("TBIL_POL_PRM_RT_FIX_PER") = Trim(Me.txtPrem_Fixed_Rate_PerNum.Text)


                    drNewRow("TBIL_POL_PRM_SA_LC") = Trim(Me.txtPrem_SA_LC.Text)
                    drNewRow("TBIL_POL_PRM_SA_FC") = Trim(Me.txtPrem_SA_FC.Text)

                    drNewRow("TBIL_POL_PRM_LIFE_COVER_SA_LC") = Trim(Me.txtPrem_Life_Cover_SA_LC.Text)
                    drNewRow("TBIL_POL_PRM_LIFE_COVER_SA_FC") = Trim(Me.txtPrem_Life_Cover_SA_FC.Text)

                    drNewRow("TBIL_POL_PRM_FREE_COV_LMT_LC") = Trim(Me.txtPrem_Free_Cover_Lmt_LC.Text)
                    drNewRow("TBIL_POL_PRM_FREE_COV_LMT_FC") = Trim(Me.txtPrem_Free_Cover_Lmt_FC.Text)

                    drNewRow("TBIL_POL_PRM_RATE_CD") = RTrim(Me.txtPrem_Rate_Code.Text)
                    drNewRow("TBIL_POL_PRM_RATE") = RTrim(Me.txtPrem_Rate.Text)
                    drNewRow("TBIL_POL_PRM_RATE_PER") = RTrim(Me.txtPrem_Rate_Per.Text)

                    drNewRow("TBIL_POL_PRM_NO_INSTAL") = RTrim(Me.txtPrem_No_Instal.Text)

                    drNewRow("TBIL_POL_PRM_FREE_LIFECOVER_LMT_LC") = Trim(Me.txtPrem_Free_LiveCover_Lmt_LC.Text)
                    drNewRow("TBIL_POL_PRM_FREE_LIFECOVER_LMT_FC") = Trim(Me.txtPrem_Free_LiveCover_Lmt_FC.Text)

                    drNewRow("TBIL_POL_PRM_RT_APPLIED_ON") = Trim(Me.txtPrem_Rate_Applied_On.Text)

                    'new fields
                    drNewRow("TBIL_POL_PRM_RT_BONUS_CD") = Trim(Me.txtPrem_Bonus_YN.Text)
                    drNewRow("TBIL_POL_PRM_ALLOC_CD") = Trim(Me.txtPrem_Allocation_YN.Text)
                    drNewRow("TBIL_POL_PRM_SCH_TERM") = Trim(Me.txtPrem_School_Term.Text)
                    'drNewRow("TBIL_POL_PRM_FEE_PRD") = Val(Me.txtPrem_Sch_Fee_Prd.Text)

                    drNewRow("TBIL_POL_PRM_DISCNT_PCENT") = RTrim(Me.txtPrem_Discount_Rate.Text)
                    drNewRow("TBIL_POL_PRM_LOAD_PCENT") = RTrim(Me.txtPrem_Loading_Rate.Text)
                    drNewRow("TBIL_POL_PRM_ENROL_NO") = Val(txtPrem_Enrollee_Num.Text)
                drNewRow("TBIL_POL_PRM_DEPOSIT_AMT") = Val(txtDepositAmount.Text)


                    drNewRow("TBIL_POL_PRM_FLAG") = "A"
                    drNewRow("TBIL_POL_PRM_OPERID") = CType(myUserIDX, String)
                    drNewRow("TBIL_POL_PRM_KEYDTE") = Now

                    obj_DT.Rows.Add(drNewRow)
                    'obj_DT.AcceptChanges()
                    intC = objDA.Update(obj_DT)

                    drNewRow = Nothing

                    Me.lblMsg.Text = "New Record Saved to Database Successfully."

                Else
                    '   Update existing record

                    'm_rwContact = m_dtContacts.Rows(0)
                    'm_rwContact("ContactName") = "Bob Brown"
                    'm_rwContact.AcceptChanges()
                    'm_dtContacts.AcceptChanges()
                    'Dim intC As Integer = m_daDataAdapter.Update(m_dtContacts)


                    With obj_DT
                        .Rows(0)("TBIL_POL_PRM_FILE_NO") = RTrim(Me.txtFileNum.Text)
                        .Rows(0)("TBIL_POL_PRM_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                        '.Rows(0)("TBIL_POL_PRM_POLY_NO") = RTrim(Me.txtPolNum.Text)

                        .Rows(0)("TBIL_POL_PRM_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)
                        .Rows(0)("TBIL_POL_PRM_PLAN_CD") = RTrim(Me.txtPlan_Num.Text)
                        '.Rows(0)("TBIL_POL_PRM_COVER_CD") = RTrim(Me.txtCover_Num.Text)

                        .Rows(0)("TBIL_POL_PRM_PERIOD_YRS") = Val(Trim(Me.txtPrem_Period_Yr.Text))

                        If Trim(Me.txtPrem_Start_Date.Text) <> "" Then
                            .Rows(0)("TBIL_POL_PRM_FROM") = dteStart
                        End If
                        If Trim(Me.txtPrem_End_Date.Text) <> "" Then
                            .Rows(0)("TBIL_POL_PRM_TO") = dteEnd
                        End If

                        .Rows(0)("TBIL_POL_PRM_SA_CURRCY") = Trim(Me.txtPrem_SA_CurrencyCode.Text)
                        .Rows(0)("TBIL_POL_PRM_MOP_RATE") = RTrim(Me.txtPrem_MOP_Rate.Text)
                        .Rows(0)("TBIL_POL_PRM_EXCHG_RATE") = RTrim(Me.txtPrem_Exchange_Rate.Text)

                        .Rows(0)("TBIL_POL_PRM_ANN_CONTRIB_LC") = Trim(Me.txtPrem_Ann_Contrib_LC.Text)
                        .Rows(0)("TBIL_POL_PRM_ANN_CONTRIB_FC") = Trim(Me.txtPrem_Ann_Contrib_FC.Text)
                        .Rows(0)("TBIL_POL_PRM_MTH_CONTRIB_LC") = Trim(Me.txtPrem_Mth_Contrib_LC.Text)
                        .Rows(0)("TBIL_POL_PRM_MTH_CONTRIB_FC") = Trim(Me.txtPrem_Mth_Contrib_FC.Text)

                        'new fields
                        .Rows(0)("TBIL_POL_PRM_LIFE_COVER") = Trim(Me.txtPrem_Life_CoverNum.Text)
                        .Rows(0)("TBIL_POL_SA_FROM_PRM") = Trim(Me.txtPrem_Is_SA_From_PremNum.Text)
                        .Rows(0)("TBIL_POL_PRM_RT_TAB_FIX") = Trim(Me.txtPrem_Rate_TypeNum.Text)
                        .Rows(0)("TBIL_POL_PRM_RT_FIXED") = Trim(Me.txtPrem_Fixed_Rate.Text)
                        .Rows(0)("TBIL_POL_PRM_RT_FIX_PER") = Trim(Me.txtPrem_Fixed_Rate_PerNum.Text)

                        .Rows(0)("TBIL_POL_PRM_SA_LC") = Trim(Me.txtPrem_SA_LC.Text)
                        .Rows(0)("TBIL_POL_PRM_SA_FC") = Trim(Me.txtPrem_SA_FC.Text)

                        .Rows(0)("TBIL_POL_PRM_LIFE_COVER_SA_LC") = Trim(Me.txtPrem_Life_Cover_SA_LC.Text)
                        .Rows(0)("TBIL_POL_PRM_LIFE_COVER_SA_FC") = Trim(Me.txtPrem_Life_Cover_SA_FC.Text)

                        .Rows(0)("TBIL_POL_PRM_FREE_COV_LMT_LC") = Trim(Me.txtPrem_Free_Cover_Lmt_LC.Text)
                        .Rows(0)("TBIL_POL_PRM_FREE_COV_LMT_FC") = Trim(Me.txtPrem_Free_Cover_Lmt_FC.Text)

                        .Rows(0)("TBIL_POL_PRM_MODE_PAYT") = RTrim(Me.txtPrem_MOP_Type.Text)
                        .Rows(0)("TBIL_POL_PRM_RATE_CD") = RTrim(Me.txtPrem_Rate_Code.Text)
                        .Rows(0)("TBIL_POL_PRM_RATE") = RTrim(Me.txtPrem_Rate.Text)
                        .Rows(0)("TBIL_POL_PRM_RATE_PER") = RTrim(Me.txtPrem_Rate_Per.Text)

                        .Rows(0)("TBIL_POL_PRM_NO_INSTAL") = RTrim(Me.txtPrem_No_Instal.Text)

                        .Rows(0)("TBIL_POL_PRM_FREE_LIFECOVER_LMT_LC") = Trim(Me.txtPrem_Free_LiveCover_Lmt_LC.Text)
                        .Rows(0)("TBIL_POL_PRM_FREE_LIFECOVER_LMT_FC") = Trim(Me.txtPrem_Free_LiveCover_Lmt_FC.Text)

                        .Rows(0)("TBIL_POL_PRM_RT_APPLIED_ON") = Trim(Me.txtPrem_Rate_Applied_On.Text)

                        'new fields
                        .Rows(0)("TBIL_POL_PRM_RT_BONUS_CD") = Trim(Me.txtPrem_Bonus_YN.Text)
                        .Rows(0)("TBIL_POL_PRM_ALLOC_CD") = Trim(Me.txtPrem_Allocation_YN.Text)
                        .Rows(0)("TBIL_POL_PRM_SCH_TERM") = Trim(Me.txtPrem_School_Term.Text)
                        '.Rows(0)("TBIL_POL_PRM_FEE_PRD") = Val(Me.txtPrem_Sch_Fee_Prd.Text)

                        .Rows(0)("TBIL_POL_PRM_DISCNT_PCENT") = RTrim(Me.txtPrem_Discount_Rate.Text)
                        .Rows(0)("TBIL_POL_PRM_LOAD_PCENT") = RTrim(Me.txtPrem_Loading_Rate.Text)
                        .Rows(0)("TBIL_POL_PRM_ENROL_NO") = Val(txtPrem_Enrollee_Num.Text)
                    .Rows(0)("TBIL_POL_PRM_DEPOSIT_AMT") = Val(txtDepositAmount.Text)
                        .Rows(0)("TBIL_POL_PRM_FLAG") = "C"
                        '.Rows(0)("TBIL_POL_PRM_OPERID") = CType(myUserIDX, String)
                        '.Rows(0)("TBIL_POL_PRM_KEYDTE") = Now
                    End With

                    'obj_DT.AcceptChanges()
                    intC = objDA.Update(obj_DT)

                    Me.lblMsg.Text = "Record Saved to Database Successfully."

                End If


                'Dim dataSet As System.Data.DataSet = New System.Data.DataSet

                'm_daDataAdapter.Fill(dataSet, m_Tbl)
                '' Insert Code to modify data in DataSet here 
                ''   ...
                ''   ...

                ''m_cbCommandBuilder.GetInsertCommand()

                'm_cbCommandBuilder.GetUpdateCommand()

                ''m_cbCommandBuilder.GetDeleteCommand()

                '' Without the OleDbCommandBuilder this line would fail.
                'm_daDataAdapter.Update(dataSet, m_Tbl)


                '' If there is existing data, update it.
                'If m_dtContacts.Rows.Count <> 0 Then
                '    m_dtContacts.Rows(m_rowPosition)("ContactName") = strContactName
                '    m_dtContacts.Rows(m_rowPosition)("State") = strState
                '    m_daDataAdapter.Update(m_dtContacts)
                'Else
                '    '   Creating New Record
                '    Dim drNewRow As System.Data.DataRow = m_dtContacts.NewRow()
                '    drNewRow("ContactName") = strContactName
                '    drNewRow("State") = strState
                '    m_dtContacts.Rows.Add(drNewRow)
                '    m_daDataAdapter.Update(m_dtContacts)
                'End If


                ''To access the first row of your DataTable like this:
                'm_rwContact = m_dtContacts.Rows(0)

                ''To reference the value of a column, you can pass the column name to the DataRow like this:
                '' Change the value of the column.
                'm_rwContact("ContactName") = "Bob Brown"

                ''   or
                '' Get the value of the column.
                'strContactName = m_rwContact("ContactName")


                ''Deleting Record
                '' If there is data, delete the current row.
                'If m_dtContacts.Rows.Count <> 0 Then
                '    m_dtContacts.Rows(m_rowPosition).Delete()
                '    m_daDataAdapter.Update(m_dtContacts)
                'End If


            Catch ex As Exception
                Me.lblMsg.Text = ex.Message.ToString
                Exit Sub
            End Try

            obj_DT.Dispose()
            obj_DT = Nothing

            m_cbCommandBuilder.Dispose()
            m_cbCommandBuilder = Nothing

            If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
                objDA.SelectCommand.Connection.Close()
            End If
            objDA.Dispose()
            objDA = Nothing

            If objOLEConn.State = ConnectionState.Open Then
                objOLEConn.Close()
            End If
            objOLEConn = Nothing



            Me.cmdNext.Enabled = True

            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

            'Call DoNew()


    End Sub


    Private Sub Proc_DoCalc_Prem()

        '******************************************************
        ' START CODES - CALCULATE SUM ASSURED FROM PREMIUM
        '******************************************************


        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        Dim strHas_Life_Cover As String
        Dim strRate_Type As String

        Dim dblSum_Assured_LC As Double = 0
        Dim dblSum_Assured_FC As Double = 0

        Dim dblLife_Cover_SA_LC As Double = 0
        Dim dblLife_Cover_SA_FC As Double = 0

        Dim dblFree_Life_Cover_Lmt_LC As Double = 0
        Dim dblFree_Life_Cover_Lmt_FC As Double = 0

        Dim dblAnnual_Contrib_LC As Double = 0
        Dim dblAnnual_Contrib_FC As Double = 0

        Dim dblPrem_Rate As Double = 0
        Dim dblRate_Per As Integer = 0
        Dim dblTerm As Double = 0

        Dim dblMOP_Rate As Double = 0
        Dim dblMOP_Per As Double = 0
        Dim dblMOP_Contrib_LC As Double = 0
        Dim dblMOP_Contrib_FC As Double = 0

        Dim dblMOP_Fee As Double = 0
        Dim dblMOP_Prem_LC As Double = 0
        Dim dblMOP_Prem_FC As Double = 0



        strHas_Life_Cover = Trim(Me.txtPrem_Life_CoverNum.Text)
        'txtPrem_Rate_TypeNum.Text = "T"
        strRate_Type = Trim(Me.txtPrem_Rate_TypeNum.Text)
        'Response.Write("<br />Rate Type: " & strRate_Type)


        dblFree_Life_Cover_Lmt_LC = Val(Me.txtPrem_Free_LiveCover_Lmt_LC.Text)
        dblFree_Life_Cover_Lmt_FC = Val(Me.txtPrem_Free_LiveCover_Lmt_FC.Text)

        dblPrem_Rate = 0
        dblRate_Per = 0

        dblAnnual_Contrib_LC = Val(Me.txtPrem_Ann_Contrib_LC.Text)
        dblAnnual_Contrib_FC = Val(Me.txtPrem_Ann_Contrib_FC.Text)

        dblAnnual_Basic_Prem_LC = 0
        dblAnnual_Basic_Prem_FC = 0

        dblTotal_Add_Prem_LC = 0
        dblTotal_Add_Prem_FC = 0


        strREC_ID = Trim(strF_ID)
        strREC_ID = Trim(Me.txtFileNum.Text)
        strQ_ID = Trim(Me.txtQuote_Num.Text)
        strP_ID = Trim(Me.txtPolNum.Text)

        dblMOP_Rate = 0
        dblMOP_Per = 0

        Dim myRetValue As String = "0"
        myRetValue = MOD_GEN.gnGET_RATE("GET_IL_MOP_FACTOR", "IND", Me.txtPrem_MOP_Type.Text, "", "", "", Me.lblMsg, Nothing, Me.txtPrem_MOP_Per)
        If Left(LTrim(myRetValue), 3) = "ERR" Then
            dblMOP_Rate = 0
        Else
            dblMOP_Rate = Val(myRetValue)
            dblMOP_Per = Val(Me.txtPrem_MOP_Per.Text)
        End If


        '******************************************************
        ' START CODES - Education Endowment
        '******************************************************
        Select Case Trim(Me.txtProduct_Num.Text)
            Case "E001"

                dblTmp_Amt = 0

                Select Case Trim(Me.txtPrem_Rate_Applied_On.Text)
                    Case "S"
                        Select Case UCase(Trim(strRate_Type))
                            Case "N"
                                dblAnnual_Basic_Prem_LC = 0
                                dblAnnual_Basic_Prem_FC = 0

                                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                                'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                                dblPrem_Rate = 0
                                dblRate_Per = 0

                            Case "F"
                                'If TBIL_POL_PRM_RT_TAB_FIX of Policy file Premium information = ‘F’ 
                                'use the fixed rate instead of Table rate. 
                                'Use the Fixed Rate per Go to (e0)

                                '* Rat = eTBIL_POL_PRM_RT_FIXED
                                '*Rate per = TBIL_POL_PRM_RT_FIX_PER

                                dblPrem_Rate = 0
                                dblRate_Per = 0
                                dblTmp_Amt = 0

                                If IsNumeric(Trim(Me.txtPrem_Fixed_Rate.Text)) Then
                                    dblPrem_Rate = CType(Trim(Me.txtPrem_Fixed_Rate.Text), Double)
                                End If
                                If IsNumeric(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text)) Then
                                    dblRate_Per = CType(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text), Integer)
                                End If

                                'If Val(dblFree_Life_Cover_Lmt_LC) > 0 And Val(Me.txtPrem_Sch_Fee_Prd.Text) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                '    dblTmp_Amt = (dblFree_Life_Cover_Lmt_LC / (3 * Val(Me.txtPrem_Sch_Fee_Prd.Text))) / dblRate_Per * dblPrem_Rate
                                'End If
                                'change the calculation mode
                                If Val(dblFree_Life_Cover_Lmt_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                    dblTmp_Amt = (dblFree_Life_Cover_Lmt_LC / dblRate_Per) * dblPrem_Rate
                                End If

                                dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                                dblAnnual_Basic_Prem_LC = dblTmp_Amt
                                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblTmp_Amt
                                'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                            Case "T"
                                'GET RATE FROM TABLE USING THE FOLLOWING
                                '   TBIL_POL_PRM_RATE_CD
                                '   TBIL_POL_PRM_PERIOD_YR
                                '   TBIL_POLY_ASSRD_AGE

                                '*Rate   -  TBIL_PRM_RT_RATE 
                                'Rate per   - TBIL_PRM_RT_PER.

                                'Calculate Annual Basic Premium as Follows.
                                '	Annual Basic Prem = S.A multiplied by rate divided by Rate per.
                                'Move Annual Basic Prem. Calculated to 
                                '   TBIL_POL_PRM_DTL_BASIC_PRM_LC
                                '   TBIL_POL_PRM_DTL_BASIC_PRM_FC

                                dblPrem_Rate = 0
                                dblRate_Per = 0
                                dblTmp_Amt = 0


                                If IsNumeric(Trim(Me.txtPrem_Rate.Text)) Then
                                    dblPrem_Rate = CType(Trim(Me.txtPrem_Rate.Text), Double)
                                End If
                                If IsNumeric(Trim(Me.txtPrem_Rate_Per.Text)) Then
                                    dblRate_Per = CType(Trim(Me.txtPrem_Rate_Per.Text), Integer)
                                End If

                                If Val(dblFree_Life_Cover_Lmt_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                    dblTmp_Amt = (dblFree_Life_Cover_Lmt_LC / dblRate_Per) * dblPrem_Rate
                                End If

                                dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                                dblAnnual_Basic_Prem_LC = dblTmp_Amt
                                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblTmp_Amt
                                'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                        End Select


                    Case "P"
                        '(TBIL_POL_PRM_ANN_CONTRIB_LC less any additional covers premium Times (3 times TBIL_POL_PRM_FEE_PRD)) divided by Table Rate  Times Rate Per.

                        'Move result to TBIL_POL_PRM_DTL_SA_LC
                        '               TBIL_POL_PRM_DTL_LIFE_COVER_SA_LC()

                        'Move TBIL_POL_PRM_ANN_CONTRIB less Total Additional cover premiums to  TBIL_POL_PRM_DTL_BASIC_PRM_LC


                        Call Proc_DoCalc_AddPrem(objOLEConn)

                        dblTmp_Amt = dblAnnual_Contrib_LC - dblTotal_Add_Prem_LC

                        Select Case UCase(Trim(strRate_Type))
                            Case "N"
                                dblAnnual_Basic_Prem_LC = 0
                                dblAnnual_Basic_Prem_FC = 0

                                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                                'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                                dblPrem_Rate = 0
                                dblRate_Per = 0

                            Case "F"
                                'If TBIL_POL_PRM_RT_TAB_FIX of Policy file Premium information = ‘F’ 
                                'use the fixed rate instead of Table rate. 
                                'Use the Fixed Rate per Go to (e0)

                                '* Rat = eTBIL_POL_PRM_RT_FIXED
                                '*Rate per = TBIL_POL_PRM_RT_FIX_PER

                                dblPrem_Rate = 0
                                dblRate_Per = 0
                                dblSum_Assured_LC = 0
                                dblSum_Assured_FC = 0


                                If IsNumeric(Trim(Me.txtPrem_Fixed_Rate.Text)) Then
                                    dblPrem_Rate = CType(Trim(Me.txtPrem_Fixed_Rate.Text), Double)
                                End If
                                If IsNumeric(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text)) Then
                                    dblRate_Per = CType(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text), Integer)
                                End If

                                If Val(dblTmp_Amt) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                    'dblSum_Assured_LC = ((dblTmp_Amt / dblMOP_Rate) * (3 * Val(Me.txtPrem_Sch_Fee_Prd.Text))) / dblPrem_Rate * dblRate_Per
                                    dblSum_Assured_LC = (dblTmp_Amt / dblMOP_Rate) / dblPrem_Rate * dblRate_Per
                                End If

                                dblSum_Assured_LC = Format(dblSum_Assured_LC, "###########0.00")
                                dblSum_Assured_FC = dblSum_Assured_LC

                                'Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                                'Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString
                                Me.txtPrem_SA_LC.Text = dblSum_Assured_LC.ToString
                                Me.txtPrem_SA_FC.Text = dblSum_Assured_FC.ToString

                                'txtCalc_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                                'txtCalc_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString
                                txtPrem_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                                txtPrem_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString

                                dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                                dblAnnual_Basic_Prem_LC = dblTmp_Amt
                                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblTmp_Amt
                                'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                            Case "T"
                                'GET RATE FROM TABLE USING THE FOLLOWING
                                '   TBIL_POL_PRM_RATE_CD
                                '   TBIL_POL_PRM_PERIOD_YR
                                '   TBIL_POLY_ASSRD_AGE

                                '*Rate   -  TBIL_PRM_RT_RATE 
                                'Rate per   - TBIL_PRM_RT_PER.

                                'Calculate Annual Basic Premium as Follows.
                                '	Annual Basic Prem = S.A multiplied by rate divided by Rate per.
                                'Move Annual Basic Prem. Calculated to 
                                '   TBIL_POL_PRM_DTL_BASIC_PRM_LC
                                '   TBIL_POL_PRM_DTL_BASIC_PRM_FC

                                dblPrem_Rate = 0
                                dblRate_Per = 0
                                dblSum_Assured_LC = 0
                                dblSum_Assured_FC = 0


                                If IsNumeric(Trim(Me.txtPrem_Rate.Text)) Then
                                    dblPrem_Rate = CType(Trim(Me.txtPrem_Rate.Text), Double)
                                End If
                                If IsNumeric(Trim(Me.txtPrem_Rate_Per.Text)) Then
                                    dblRate_Per = CType(Trim(Me.txtPrem_Rate_Per.Text), Integer)
                                End If

                                If Val(dblTmp_Amt) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                    'dblSum_Assured_LC = ((dblTmp_Amt / dblMOP_Rate) * (3 * Val(Me.txtPrem_Sch_Fee_Prd.Text))) / dblPrem_Rate * dblRate_Per
                                    dblSum_Assured_LC = (dblTmp_Amt / dblMOP_Rate) / dblPrem_Rate * dblRate_Per
                                End If

                                dblSum_Assured_LC = Format(dblSum_Assured_LC, "###########0.00")
                                dblSum_Assured_FC = dblSum_Assured_LC

                                'Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                                'Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString
                                Me.txtPrem_SA_LC.Text = dblSum_Assured_LC.ToString
                                Me.txtPrem_SA_FC.Text = dblSum_Assured_FC.ToString

                                'txtCalc_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                                'txtCalc_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString
                                txtPrem_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                                txtPrem_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString

                                dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                                dblAnnual_Basic_Prem_LC = dblTmp_Amt
                                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblTmp_Amt
                                'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                            Case Else
                                '*Rate   -  0 
                                'Rate per   - 0.

                        End Select

                End Select

                GoTo Skip_C001

        End Select

        '******************************************************
        ' END CODES - Education Endowment
        '******************************************************



        '******************************************************
        ' START CODES - CALCULATE SUM ASSURED FROM PREMIUM
        '******************************************************


        Select Case Trim(Me.txtPrem_Rate_Applied_On.Text)
            Case "P"
                'Sum Assured =
                '(Annual Contribution – Additional Cover Premium) X Rate per  / Premium Rate

                'Before using the Annual Contribution Check if there are Additional covers. If there are, then sum the Additional cover premium and subtract it from the Annual Contribution before applying the table rate.

                'Move Sum Assured calculated to TBIL_POL_PRM_DTL_SA_LC
                '                TBIL_POL_PRM_DTL_LIFE_COVER_SA_LC()
                'Move (Annual Contribution – Additional Cover Premium)
                '             To TBIL_POL_PRM_DTL_BASIC_PRM_LC

                dblTotal_Add_Prem_LC = 0
                Call Proc_DoCalc_AddPrem(objOLEConn)

                If Val(dblAnnual_Contrib_LC) > 0 And dblMOP_Rate <> 0 Then
                    dblTmp_Amt = (dblAnnual_Contrib_LC / dblMOP_Rate) - dblTotal_Add_Prem_LC
                End If

                'Response.Write("<br />Annual Contribution: " & dblAnnual_Contrib_LC)
                'Response.Write("<br />Bal Contribution: " & dblTmp_Amt)
                'Response.Write("<br />Rate Type: " & strRate_Type)


                Select Case UCase(Trim(strRate_Type))
                    Case "N"
                        dblAnnual_Basic_Prem_LC = 0
                        dblAnnual_Basic_Prem_FC = 0

                        'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                        'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                        dblPrem_Rate = 0
                        dblRate_Per = 0

                    Case "F"
                        'If TBIL_POL_PRM_RT_TAB_FIX of Policy file Premium information = ‘F’ 
                        'use the fixed rate instead of Table rate. 
                        'Use the Fixed Rate per Go to (e0)

                        '* Rat = eTBIL_POL_PRM_RT_FIXED
                        '*Rate per = TBIL_POL_PRM_RT_FIX_PER

                        dblPrem_Rate = 0
                        dblRate_Per = 0
                        dblSum_Assured_LC = 0
                        dblSum_Assured_FC = 0


                        If IsNumeric(Trim(Me.txtPrem_Fixed_Rate.Text)) Then
                            dblPrem_Rate = CType(Trim(Me.txtPrem_Fixed_Rate.Text), Double)
                        End If
                        If IsNumeric(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text)) Then
                            dblRate_Per = CType(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text), Integer)
                        End If

                        If Val(dblTmp_Amt) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblSum_Assured_LC = dblTmp_Amt * dblRate_Per / dblPrem_Rate
                        End If

                        dblSum_Assured_LC = Format(dblSum_Assured_LC, "###########0.00")
                        dblSum_Assured_FC = dblSum_Assured_LC

                        'Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                        'Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString
                        Me.txtPrem_SA_LC.Text = dblSum_Assured_LC.ToString
                        Me.txtPrem_SA_FC.Text = dblSum_Assured_FC.ToString


                        'txtCalc_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                        'txtCalc_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString
                        txtPrem_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                        txtPrem_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString

                        dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                        dblAnnual_Basic_Prem_LC = dblTmp_Amt
                        'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblTmp_Amt
                        'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                    Case "T"
                        'GET RATE FROM TABLE USING THE FOLLOWING
                        '   TBIL_POL_PRM_RATE_CD
                        '   TBIL_POL_PRM_PERIOD_YR
                        '   TBIL_POLY_ASSRD_AGE

                        '*Rate   -  TBIL_PRM_RT_RATE 
                        'Rate per   - TBIL_PRM_RT_PER.

                        'Calculate Annual Basic Premium as Follows.
                        '	Annual Basic Prem = S.A multiplied by rate divided by Rate per.
                        'Move Annual Basic Prem. Calculated to 
                        '   TBIL_POL_PRM_DTL_BASIC_PRM_LC
                        '   TBIL_POL_PRM_DTL_BASIC_PRM_FC

                        dblPrem_Rate = 0
                        dblRate_Per = 0
                        dblSum_Assured_LC = 0
                        dblSum_Assured_FC = 0


                        If IsNumeric(Trim(Me.txtPrem_Rate.Text)) Then
                            dblPrem_Rate = CType(Trim(Me.txtPrem_Rate.Text), Double)
                        End If
                        If IsNumeric(Trim(Me.txtPrem_Rate_Per.Text)) Then
                            dblRate_Per = CType(Trim(Me.txtPrem_Rate_Per.Text), Integer)
                        End If

                        If Val(dblTmp_Amt) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblSum_Assured_LC = dblTmp_Amt * dblRate_Per / dblPrem_Rate
                        End If

                        dblSum_Assured_LC = Format(dblSum_Assured_LC, "###########0.00")
                        dblSum_Assured_FC = dblSum_Assured_LC

                        'Response.Write("Sum Assured: " & dblSum_Assured_LC)

                        'Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                        'Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString
                        Me.txtPrem_SA_LC.Text = dblSum_Assured_LC.ToString
                        Me.txtPrem_SA_FC.Text = dblSum_Assured_FC.ToString

                        'txtCalc_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                        'txtCalc_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString
                        txtPrem_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                        txtPrem_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString

                        dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                        dblAnnual_Basic_Prem_LC = dblTmp_Amt
                        'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblTmp_Amt
                        'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                    Case Else
                        '*Rate   -  0 
                        'Rate per   - 0.

                End Select

                GoTo Skip_C001

        End Select


        '******************************************************
        ' END CODES - CALCULATE SUM ASSURED FROM PREMIUM
        '******************************************************

Skip_C001:


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        '******************************************************
        ' END CODES - CALCULATE SUM ASSURED FROM PREMIUM
        '******************************************************

    End Sub


    Private Sub Proc_DoCalc_AddPrem(ByVal pv_objOLEConn As OleDbConnection)
        '***********************************************************************************************
        ' START ADDITIONAL COVERS CODES 
        '***********************************************************************************************

        '====================================
        '(f)	Calculate Additional Premium
        '====================================

        'Read Records from the Additional Policy Cover Table (TBIL_POLICY_ADD_PREM) for the policy
        '(f1)	If TBIL_POL_ADD_PREM_RT_AMT_CD  = ‘A’    i.e Use Amount, 
        'Add  TBIL_POL_ADD_PREM_AMT to Total Additional Premium.

        '(f2)	If TBIL_POL_ADD_PREM_RT_AMT_CD = ‘F’   fixed rate  
        'and TBIL_POL_ADD_RATE_APPLY =    ‘P’      apply fixed rate on premium
        'Compute Additional Premium = TBIL_POL_DTL_BASIC_PREM_ LC
        ' 	Multiplied by TBIL_POL_ADD_PREM_FX_RATE  Divided by TBIL_POL_ADD_PREM_FX_RT_PER.
        'Add Additional Premium to Total Additional Premium

        '(f3)	if TBIL_POL_ADD_PREM_RT_AMT_CD = ‘R’    i.e use Rate Table
        '	Use TBIL_POL_ADD_MDLE and TBIL_POL_ADD_PREM_RT_CD.
        '	Product Code,  Policy-Tenor, and Assured Age to read  the Premium Rate Table (TBIL_PREM_RATE)
        '	Pick – Premium Rate   TBIL_PRM_RT_RATE
        '		-Rate Per    TBIL_PRM_RT_PER from the Table.
        'Compute Additional Premium as
        '	TBIL_POL_ADD_SA_LC  multiplied by 
        '	TBIL_PRM_RT_RATE divided by
        '                TBIL_PRM_RT_PER()
        '	Add Additional Premium to Total Additional Premium
        '(g)	Do the above additional Premium Routine for all additional records for the Policy.

        '(h)	Move the Total Additional Premium to
        '                TBIL_POL_PRM_DTL_ADDPREM_LC()
        '                TBIL_POL_PRM_DTL_ADDPREM_FC()

        'Display on screen

        'Compute TBIL_POL_PRM_DTL_TOT_PRM_LC = 
        'TBIL_PRM_DTL_BASIC_PRM_LC + TBIL_POL_PRM_DTL_ADDPREM_LC.

        'Move to FC
        'Display on screen

        dblAdd_Prem_Amt = 0
        dblTotal_Add_Prem_LC = 0

        strTable = strTableName
        strTable = "TBIL_POLICY_ADD_PREM"


        strSQL = ""
        strSQL = strSQL & "SELECT ADD_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS ADD_TBL"
        strSQL = strSQL & " WHERE ADD_TBL.TBIL_POL_ADD_FILE_NO = '" & RTrim(strREC_ID) & "'"
        'If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
        'strSQL = strSQL & " AND ADD_TBL.TBIL_POL_ADD_REC_ID = '" & Val(FVstrRecNo) & "'"
        'End If
        strSQL = strSQL & " AND ADD_TBL.TBIL_POL_ADD_PROP_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND ADD_TBL.TBIL_POL_ADD_POLY_NO = '" & RTrim(strP_ID) & "'"

        'strSQL = "SPIL_GET_POLICY_ADD_PREM"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, pv_objOLEConn)
        'objOLECmd.CommandTimeout = 180
        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.CommandType = CommandType.StoredProcedure
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 30).Value = strREC_ID
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()

        Do While objOLEDR.Read()

            Select Case UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_RT_AMT_CD") & vbNullString, String)))
                Case "A"
                    dblAdd_Prem_Amt = 0
                    dblTmp_Amt = 0
                    If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_PREM_AMT") & vbNullString, String)) Then
                        dblAdd_Prem_Amt = CType(objOLEDR("TBIL_POL_ADD_PREM_AMT") & vbNullString, Double)
                    End If
                    dblTmp_Amt = dblAdd_Prem_Amt
                    dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                Case "F"
                    dblTmp_Amt = 0
                    dblAdd_Prem_Rate = 0
                    dblAdd_Rate_Per = 0
                    dblAdd_Prem_SA_LC = 0
                    dblAdd_Prem_SA_FC = 0

                    If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT") & vbNullString, String)) Then
                        dblAdd_Prem_Rate = CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT_PER") & vbNullString, String)) Then
                        dblAdd_Rate_Per = CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT_PER") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, String)) Then
                        dblAdd_Prem_SA_LC = CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, Double)
                    End If
                    dblAdd_Prem_SA_FC = dblAdd_Prem_SA_LC


                    Select Case Trim(CType(objOLEDR("TBIL_POL_ADD_RATE_APPLY") & vbNullString, String))
                        Case "P"    'compute additional premium base on basic premium
                            If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                        Case "S"    'compute additional premium base on sum assured
                            If Val(dblAdd_Prem_SA_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAdd_Prem_SA_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt
                    End Select

                Case "R"    'compute additional premium using the rate table
                    dblTmp_Amt = 0
                    dblAdd_Prem_Rate = 0
                    dblAdd_Rate_Per = 0
                    dblAdd_Prem_SA_LC = 0
                    dblAdd_Prem_SA_FC = 0

                    If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_RATE") & vbNullString, String)) Then
                        dblAdd_Prem_Rate = CType(objOLEDR("TBIL_POL_ADD_RATE") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_RATE_PER") & vbNullString, String)) Then
                        dblAdd_Rate_Per = CType(objOLEDR("TBIL_POL_ADD_RATE_PER") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, String)) Then
                        dblAdd_Prem_SA_LC = CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, Double)
                    End If
                    dblAdd_Prem_SA_FC = dblAdd_Prem_SA_LC

                    Select Case Trim(CType(objOLEDR("TBIL_POL_ADD_RATE_APPLY") & vbNullString, String))
                        Case "P"    'compute additional premium base on basic premium
                            If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                        Case "S"    'compute additional premium base on sum assured
                            If Val(dblAdd_Prem_SA_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAdd_Prem_SA_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt
                    End Select

            End Select

            'Response.Write("<br/>Rate Type: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_RT_AMT_CD") & vbNullString, String))))
            'Response.Write("<br/>Additional Amount: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_AMT") & vbNullString, String))))
            'Response.Write("<br/>Fixed Rate: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_RATE_APPLY") & vbNullString, String))))
            'Response.Write("<br/>Fixed Rate: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT") & vbNullString, String))))
            'Response.Write("<br/>fixed Rate Per: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT_PER") & vbNullString, String))))
            'Response.Write("<br/>Table Rate: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_RATE") & vbNullString, String))))
            'Response.Write("<br/>Table Rate Per: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_RATE_PER") & vbNullString, String))))
            'Response.Write("<br/>Sum Assured: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, String))))

        Loop

        objOLECmd = Nothing
        objOLEDR = Nothing


        'Me.txtCalc_Add_Prem_LC.Text = dblTotal_Add_Prem_LC.ToString()
        dblTotal_Add_Prem_FC = dblTotal_Add_Prem_LC
        'Me.txtCalc_Add_Prem_FC.Text = dblTotal_Add_Prem_FC.ToString()


        '***********************************************************************************************
        ' END ADDITIONAL COVERS CODES 
        '***********************************************************************************************

    End Sub


    Private Sub Proc_DoDelete()

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strREC_ID = Trim(Me.txtFileNum.Text)

        'Delete record
        'Me.textMessage.Text = "Deleting record... "
        strSQL = ""
        strSQL = "DELETE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_PRM_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_PRM_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                Call Proc_DoNew()
                Me.lblMsg.Text = "Record deleted successfully."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Else
                Me.lblMsg.Text = "Sorry!. Record not deleted..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            End If

        Catch ex As Exception
            Me.lblMsg.Text = "Error has occured. Reason: " & ex.Message
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

        End Try


        objOLECmd2.Dispose()
        objOLECmd2 = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        'Me.txtNum.Enabled = True
        'Me.txtNum.Focus()

    End Sub

    Private Sub Proc_DoNew()

        'Call Proc_DDL_Get(Me.cboransList, RTrim("*"))

        'Scan through textboxes on page or form
        'Try
        'Catch ex As Exception

        'End Try

        Dim ctrl As Control
        For Each ctrl In Page.Controls
            If TypeOf ctrl Is HtmlForm Then
                Dim subctrl As Control
                For Each subctrl In ctrl.Controls
                    If TypeOf subctrl Is System.Web.UI.WebControls.TextBox Then
                        If subctrl.ID = "txtFileNum" Or _
                           subctrl.ID = "txtQuote_Num" Or _
                           subctrl.ID = "txtPolNum" Or _
                           subctrl.ID = "txtProductClass" Or _
                           subctrl.ID = "txtProduct_Num" Or _
                           subctrl.ID = "txtPlan_Num" Or _
                           subctrl.ID = "txtCover_Num" Or _
                           subctrl.ID = "txtDOB" Or _
                           subctrl.ID = "txtDOB_ANB" Or _
                           subctrl.ID = "xyz_123" Then
                        Else
                            'Response.Write("<br> Control ID: " & subctrl.ID)
                            'CType(subctrl, TextBox).Text = ""
                        End If
                    End If
                    If TypeOf subctrl Is System.Web.UI.WebControls.DropDownList Then
                        'CType(subctrl, DropDownList).SelectedIndex = -1
                    End If
                Next
            End If
        Next

        Me.chkFileNum.Enabled = True
        Me.chkFileNum.Checked = False
        Me.lblFileNum.Enabled = False
        Me.txtFileNum.Enabled = False
        Me.cmdFileNum.Enabled = False

        Me.cmdSave_ASP.Enabled = True
        Me.cmdNext.Enabled = False

    End Sub

    Private Function Proc_DoOpenRecord(ByVal FVstrGetType As String, ByVal FVstrRefNum As String, Optional ByVal FVstrRecNo As String = "", Optional ByVal strSearchByWhat As String = "FILE_NUM") As String

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


        strREC_ID = Trim(FVstrRefNum)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 PRM_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS PRM_TBL"
        strSQL = strSQL & " WHERE PRM_TBL.TBIL_POL_PRM_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND PRM_TBL.TBIL_POL_PRM_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPIL_GET_POLICY_PREM_INFO"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        'objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_POLY_NO") & vbNullString, String))

            'Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
            ''Call gnProc_DDL_Get(Me.cboProductClass, RTrim(Me.txtProductClass.Text))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("IND=") & RTrim(Me.txtProductClass.Text))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_MDLE") & vbNullString, String)) & RTrim("=") & RTrim(Me.txtProductClass.Text))
            'If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Then
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("I=") & RTrim(Me.txtProductClass.Text))
            'End If

            'Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)

            'Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PRDCT_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboProduct, RTrim(Me.txtProduct_Num.Text))
            'Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DESC") & vbNullString, String))
            'Call gnProc_Populate_Box("IL_COVER_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
            'Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)

            'Me.txtCover_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_COVER_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboCover_Name, RTrim(Me.txtCover_Num.Text))

            'Me.txtPlan_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PLAN_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboPlan_Name, RTrim(Me.txtPlan_Num.Text))

            Me.txtPrem_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PERIOD_YRS") & vbNullString, String))

            If IsDate(objOLEDR("TBIL_POL_PRM_FROM")) Then
                Me.txtPrem_Start_Date.Text = Format(CType(objOLEDR("TBIL_POL_PRM_FROM"), DateTime), "dd/MM/yyyy")
            End If
            If IsDate(objOLEDR("TBIL_POL_PRM_TO")) Then
                Me.txtPrem_End_Date.Text = Format(CType(objOLEDR("TBIL_POL_PRM_TO"), DateTime), "dd/MM/yyyy")
            End If

            Me.txtPrem_MOP_Type.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MODE_PAYT") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_MOP_Type, RTrim(Me.txtPrem_MOP_Type.Text))

            Me.txtPrem_SA_CurrencyCode.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_CURRCY") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_SA_Currency, RTrim(Me.txtPrem_SA_CurrencyCode.Text))
            Me.txtPrem_Exchange_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_EXCHG_RATE") & vbNullString, String))

            Me.txtPrem_Life_CoverNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Life_Cover, RTrim(Me.txtPrem_Life_CoverNum.Text))

            Me.txtPrem_MOP_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MOP_RATE") & vbNullString, String))
            Me.txtPrem_Exchange_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_EXCHG_RATE") & vbNullString, String))

            Me.txtPrem_Rate_TypeNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_TAB_FIX") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Rate_Type, RTrim(Me.txtPrem_Rate_TypeNum.Text))

            Me.txtPrem_Fixed_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_FIXED") & vbNullString, String))

            'Me.lblPrem_Rate_Applied_On.Enabled = False
            'Me.optPrem_Applied_SA.Enabled = False
            'Me.optPrem_Applied_SA.Checked = False
            'Me.optPrem_Applied_Prem.Enabled = False
            'Me.optPrem_Applied_Prem.Checked = False

            If Trim(Me.txtPrem_Rate_TypeNum.Text) = "F" Then
                Me.lblPrem_Fixed_Rate.Enabled = True
                Me.txtPrem_Fixed_Rate.Enabled = True
                Me.lblPrem_Fixed_Rate_Per.Enabled = True
                Me.cboPrem_Fixed_Rate_Per.Enabled = True
                Me.lblPrem_Rate_Code.Enabled = False
                Me.cboPrem_Rate_Code.Enabled = False
                'Me.txtPrem_Rate_Code.Enabled = False
                'Me.lblPrem_Rate.Enabled = False
                'Me.txtPrem_Rate.Enabled = False
                'Me.txtPrem_Rate_Per.Enabled = False
            ElseIf Trim(Me.txtPrem_Rate_TypeNum.Text) = "N" Then
                Me.lblPrem_Fixed_Rate.Enabled = False
                Me.txtPrem_Fixed_Rate.Enabled = False
                Me.lblPrem_Fixed_Rate_Per.Enabled = False
                Me.cboPrem_Fixed_Rate_Per.Enabled = False
                Me.lblPrem_Rate_Code.Enabled = False
                Me.cboPrem_Rate_Code.Enabled = False
                'Me.txtPrem_Rate_Code.Enabled = False
                'Me.lblPrem_Rate.Enabled = False
                'Me.txtPrem_Rate.Enabled = False
                'Me.txtPrem_Rate_Per.Enabled = False
            ElseIf Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" Then
                Me.lblPrem_Fixed_Rate.Enabled = False
                Me.txtPrem_Fixed_Rate.Enabled = False
                Me.lblPrem_Fixed_Rate_Per.Enabled = False
                Me.cboPrem_Fixed_Rate_Per.Enabled = False
                Me.lblPrem_Rate_Code.Enabled = True
                Me.cboPrem_Rate_Code.Enabled = True
                'Me.txtPrem_Rate_Code.Enabled = True
                'Me.lblPrem_Rate.Enabled = True
                'Me.txtPrem_Rate.Enabled = True
                'Me.txtPrem_Rate_Per.Enabled = True
                'Me.lblPrem_Rate_Applied_On.Enabled = True
                'Me.optPrem_Applied_SA.Enabled = True
                'Me.optPrem_Applied_SA.Checked = True
                'Me.optPrem_Applied_Prem.Enabled = True
                'Me.optPrem_Applied_Prem.Checked = True
            End If



            Me.txtPrem_Fixed_Rate_PerNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_FIX_PER") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Fixed_Rate_Per, RTrim(Me.txtPrem_Fixed_Rate_PerNum.Text))

            Me.txtPrem_Rate_Code.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Rate_Code, RTrim(Me.txtPrem_Rate_Code.Text))
            Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE") & vbNullString, String))
            Me.txtPrem_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE_PER") & vbNullString, String))

            Me.txtPrem_Rate_Applied_On.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_APPLIED_ON") & vbNullString, String))
            If Trim(Me.txtPrem_Rate_Applied_On.Text) = "S" Then
                Me.optPrem_Applied_SA.Checked = True
                Me.optPrem_Applied_Prem.Checked = False
            ElseIf Trim(Me.txtPrem_Rate_Applied_On.Text) = "P" Then
                Me.optPrem_Applied_SA.Checked = False
                Me.optPrem_Applied_Prem.Checked = True
            End If

            Me.txtPrem_Is_SA_From_PremNum.Text = RTrim(CType(objOLEDR("TBIL_POL_SA_FROM_PRM") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Is_SA_From_Prem, RTrim(Me.txtPrem_Is_SA_From_PremNum.Text))

            Me.txtPrem_Ann_Contrib_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_LC") & vbNullString, String))
            Me.txtPrem_Ann_Contrib_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_FC") & vbNullString, String))

            Me.txtDepositAmount.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DEPOSIT_AMT") & vbNullString, String))


            Me.txtPrem_Mth_Contrib_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MTH_CONTRIB_LC") & vbNullString, String))
            Me.txtPrem_Mth_Contrib_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MTH_CONTRIB_FC") & vbNullString, String))


            Me.txtPrem_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_LC") & vbNullString, String))
            Me.txtPrem_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_FC") & vbNullString, String))

            Me.txtPrem_Life_Cover_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER_SA_LC") & vbNullString, String))
            Me.txtPrem_Life_Cover_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER_SA_FC") & vbNullString, String))
            '

            Me.txtPrem_Free_Cover_Lmt_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FREE_COV_LMT_LC") & vbNullString, String))
            Me.txtPrem_Free_Cover_Lmt_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FREE_COV_LMT_FC") & vbNullString, String))


            Me.txtPrem_No_Instal.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_NO_INSTAL") & vbNullString, String))

            Me.txtPrem_Free_LiveCover_Lmt_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FREE_LIFECOVER_LMT_LC") & vbNullString, String))
            Me.txtPrem_Free_LiveCover_Lmt_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FREE_LIFECOVER_LMT_FC") & vbNullString, String))

            Me.txtPrem_Bonus_YN.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_BONUS_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Bonus_YN, RTrim(Me.txtPrem_Bonus_YN.Text))

            Me.txtPrem_Allocation_YN.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ALLOC_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Allocation_YN, RTrim(Me.txtPrem_Allocation_YN.Text))

            Me.txtPrem_School_Term.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SCH_TERM") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_School_Term, RTrim(Me.txtPrem_School_Term.Text))
            'Me.txtPrem_Sch_Fee_Prd.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FEE_PRD") & vbNullString, String))

            Me.txtPrem_Discount_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DISCNT_PCENT") & vbNullString, String))
            Me.txtPrem_Loading_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LOAD_PCENT") & vbNullString, String))

            Me.txtPrem_Enrollee_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ENROL_NO") & vbNullString, String))

            lblOriginator.Text = "Record Originally created by " & GetRecordOriginator(RTrim(CType(objOLEDR("TBIL_POL_PRM_OPERID") & vbNullString, String)))

            Me.lblFileNum.Enabled = False
            'Call DisableBox(Me.txtFileNum)
            Me.chkFileNum.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.txtQuote_Num.Enabled = False
            Me.txtPolNum.Enabled = False

            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True
            Me.cmdNext.Enabled = True

            'If RTrim(CType(objOLEDR("TBIL_POLY_PROPSL_ACCPT_STATUS") & vbNullString, String)) = "A" Then
            '    Me.chkFileNum.Enabled = False
            '    Me.lblFileNum.Enabled = False
            '    Me.txtFileNum.Enabled = False
            '    Me.cmdFileNum.Enabled = False
            '    Me.cmdSave_ASP.Enabled = False
            '    Me.cmdDelete_ASP.Enabled = False
            'End If

            strOPT = "2"
            Me.lblMsg.Text = "Status: Data Modification"

        Else
            'Me.lblFileNum.Enabled = True
            'Call DisableBox(Me.txtFileNum)
            'Me.chkFileNum.Enabled = True
            'Me.chkFileNum.Checked = False
            'Me.txtFileNum.Enabled = True
            'Me.txtQuote_Num.Enabled = True
            'Me.txtPolNum.Enabled = True

            Me.cmdDelete_ASP.Enabled = False
            Me.cmdNext.Enabled = False

            strOPT = "1"
            Me.lblMsg.Text = "Status: New Entry..."

        End If


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

    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Dim pvURL As String = "prg_li_indv_poly_persnal.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim pvURL As String = ""
        pvURL = "prg_li_indv_poly_benefry.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        Select Case Trim(Me.txtProduct_Num.Text)
            Case "F001", "F002"
                pvURL = "prg_li_indv_poly_funeral.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        End Select
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

End Class
