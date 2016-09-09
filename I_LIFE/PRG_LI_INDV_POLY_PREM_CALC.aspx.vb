Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class I_LIFE_PRG_LI_INDV_POLY_PREM_CALC
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

    Dim dblSum_Assured_LC As Double = 0
    Dim dblSum_Assured_FC As Double = 0

    ' additional premium variable
    Dim dblAdd_Prem_Amt As Double = 0
    Dim dblAdd_Prem_Rate As Double = 0
    Dim dblAdd_Rate_Per As Integer = 0
    Dim dblAdd_Prem_SA_LC As Double
    Dim dblAdd_Prem_SA_FC As Double

    Dim strProduct_Num As String = ""
    Dim strCover_Num As String = ""
    Dim intAFAB_Rec_ID As Integer = 0
    Dim strAFAB_SW As String = "N"
    Dim strAFAB_Num As String = ""
    Dim dblAFAB_Value As Double = 0

    Dim dblTotal_Add_Prem_LC As Double = 0
    Dim dblTotal_Add_Prem_FC As Double = 0

    Dim dblTotal_Prem_LC As Double = 0
    Dim dblTotal_Prem_FC As Double = 0

    Dim dblAnnual_Basic_Prem_LC As Double = 0
    Dim dblAnnual_Basic_Prem_FC As Double = 0

    Dim dblMOP_Basic_Prem_LC As Double = 0
    Dim dblMOP_Basic_Prem_FC As Double = 0

    Dim dblNet_Prem_LC As Double = 0
    Dim dblNet_Prem_FC As Double = 0

    Dim dblTmp_Amt As Double = 0
    Dim dblTmp_AmtX As Double = 0


    Dim strDoc_Item_Flag As String = ""
    Dim strDoc_Item_SQL As String = ""




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_POLICY_PREM_DETAILS"

        Try
            strP_TYPE = CType(Request.QueryString("optid"), String)
        Catch ex As Exception
            strP_TYPE = "NEW"
        End Try

        STRMENU_TITLE = "Proposal Screen"
        'STRMENU_TITLE = "Investment Plus Proposal"
        Select Case UCase(strP_TYPE)
            Case "NEW"
                STRMENU_TITLE = "New Proposal"
            Case "CHG"
                STRMENU_TITLE = "Change Mode"
            Case "DEL"
                STRMENU_TITLE = "Delete Mode"
            Case Else
                strP_TYPE = "NEW"
                STRMENU_TITLE = "New Proposal"
        End Select


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

        'strF_ID = "6004025"

        ' Load data for the DropDownList control only once, when the 
        ' page is first loaded.
        If Not (Page.IsPostBack) Then
            'Call Proc_DoNew()
            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False

            Me.cboProduct.Items.Clear()

            'Call DoProc_CreateDataSource("IL_PRODUCT_CAT_LIST", Trim("IND"), Me.cboProductClass)
            'Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Trim("101"), Me.cboProduct)

            'Call gnProc_Populate_Box("IL_CODE_LIST", "001", Me.cboNationality)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "003", Me.cboBranch)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "005", Me.cboDepartment)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "007", Me.cboOccupationClass)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "008", Me.cboOccupation)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "009", Me.cboReligion)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "013", Me.cboRelation)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "015", Me.cboGender)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "020", Me.cboMaritalStatus)

            If Trim(strF_ID) <> "" Then
                Me.txtFileNum.Text = RTrim(strF_ID)
                Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD("GET_POLICY_BY_FILE_NO", RTrim(strF_ID), RTrim(""), RTrim(""))
                If oAL.Item(0) = "TRUE" Then
                    '    'Retrieve the record
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
                    Me.txtQuote_Num.Text = oAL.Item(3)
                    'strQ_ID = Me.txtQuote_Num.Text
                    Me.txtPolNum.Text = oAL.Item(4)
                    'strP_ID = Me.txtPolNum.Text

                    Me.txtProductClass.Text = oAL.Item(5)
                    Me.txtProduct_Num.Text = oAL.Item(6)
                    Me.txtPlan_Num.Text = oAL.Item(7)
                    Me.txtCover_Num.Text = oAL.Item(8)
                    Me.txtDOB.Text = oAL.Item(9)
                    Me.txtDOB_ANB.Text = oAL.Item(10)
                    Me.txtTrans_Date.Text = oAL.Item(11)
                    Me.txtPrem_Enrollee_Num.Text = oAL.Item(15)
                    Me.txtPrem_Ann_Contrib_LC_Prm.Text = oAL.Item(17)
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

            Call DoProc_CreateDataSource("IL_PRODUCT_CAT_LIST", Trim("IND"), Me.cboProductClass)
            Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)
            Call gnProc_Populate_Box("IL_COVER_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
            Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)

            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("IND=") & RTrim(Me.txtProductClass.Text))
            Call gnProc_DDL_Get(Me.cboProduct, RTrim(Me.txtProduct_Num.Text))
            Call gnProc_DDL_Get(Me.cboCover_Name, RTrim(Me.txtCover_Num.Text))
            Call gnProc_DDL_Get(Me.cboPlan_Name, RTrim(Me.txtPlan_Num.Text))

            Call gnProc_Populate_Box("IL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPrem_Rate_Code)

            If Trim(strF_ID) <> "" Then
                'Call gnProc_Populate_Box("IL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPrem_Rate_Code)
                strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
            End If


            'If Trim(strQ_ID) <> "" Then
            '    Me.txtQuote_Num.Text = RTrim(strQ_ID)
            'End If
            'If Trim(strP_ID) <> "" Then
            '    Me.txtPolNum.Text = RTrim(strP_ID)
            'End If

            Me.lblMsg.Text = "Status:"

        End If


        If Me.txtAction.Text = "New" Then
            Me.txtQuote_Num.Text = ""
            Call Proc_DoNew()
            Me.txtAction.Text = ""
            Me.lblMsg.Text = "New Entry..."
        End If

        If Me.txtAction.Text = "Save" Then
            'strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
            'Call Proc_DoSave()
            Me.txtAction.Text = ""
            'Me.cmdSave_ASP.Enabled = False
        End If

        If Me.txtAction.Text = "Delete" Then
            Call Proc_DoDelete()
            Me.txtAction.Text = ""
        End If

        'If Me.txtAction.Text = "Delete_Item" Then
        '    'Call Proc_DoDelItem()
        '    Me.txtAction.Text = ""
        'End If

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
        Call Proc_DoSave()
        Me.txtAction.Text = ""
        Me.cmdSave_ASP.Enabled = False

    End Sub

    'Private Function CreateDataSource(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvField_Text As String, ByVal pvField_Value As String) As ICollection
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
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD"

            Case "IL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','I')"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"

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

    Protected Sub DoProc_Rate_Type_Change()

    End Sub

    Protected Sub DoProc_Premium_Code_Change()

    End Sub
    Private Sub Proc_DoDelete()

    End Sub

    Private Sub Proc_DoNew()

    End Sub

    Private Sub Proc_DoSave()

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
            Me.lblMsg.Text = "Missing " & Me.lblProductClass.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtProduct_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProduct_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


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


        'Call Proc_DoCalc_Prem()


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
        strSQL = strSQL & " WHERE TBIL_POL_PRM_DTL_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_PRM_DTL_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        'If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
        'strSQL = strSQL & " AND TBIL_POL_PRM_DTL_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        'End If


        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        ''or
        ''objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        ''Dim m_rwContact As System.Data.DataRow
        Dim intC As Integer = 0


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '        '   Creating a new record

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                '
                drNewRow("TBIL_POL_PRM_DTL_MDLE") = RTrim("I")

                drNewRow("TBIL_POL_PRM_DTL_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_POL_PRM_DTL_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                'drNewRow("TBIL_POL_PRM_DTL_POLY_NO") = RTrim(Me.txtPolNum.Text)

                drNewRow("TBIL_POL_PRM_DTL_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)

                drNewRow("TBIL_POL_PRM_DTL_SA_LC") = Me.txtCalc_SA_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_SA_FC") = Me.txtCalc_SA_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_LIFE_COVER_SA_LC") = Me.txtCalc_Life_Cover_SA_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_LIFE_COVER_SA_FC") = Me.txtCalc_Life_Cover_SA_FC.Text


                drNewRow("TBIL_POL_PRM_DTL_BASIC_PRM_LC") = Me.txtCalc_Ann_Basic_Prem_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_BASIC_PRM_FC") = Me.txtCalc_Ann_Basic_Prem_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_ADDPREM_LC") = Me.txtCalc_Add_Prem_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_ADDPREM_FC") = Me.txtCalc_Add_Prem_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_LOADING_LC") = Me.txtCalc_Prem_Loading_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_LOADING_FC") = Me.txtCalc_Prem_Loading_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_DISCNT_LC") = Me.txtCalc_Prem_Disc_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_DISCNT_FC") = Me.txtCalc_Prem_Disc_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_CHG_LC") = Me.txtCalc_Add_Charges_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_CHG_FC") = Me.txtCalc_Add_Charges_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_MOP_PRM_LC") = Val(Me.txtCalc_MOP_Prem_LC.Text)
                drNewRow("TBIL_POL_PRM_DTL_MOP_PRM_FC") = Val(Me.txtCalc_MOP_Prem_FC.Text)

                drNewRow("TBIL_POL_PRM_DTL_TOT_PRM_LC") = Me.txtCalc_Total_Prem_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_TOT_PRM_FC") = Me.txtCalc_Total_Prem_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_ANN_PREM_LC") = Me.txtCalc_Ann_Prem_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_ANN_PREM_FC") = Me.txtCalc_Ann_Prem_FC.Text

                ' next
                drNewRow("TBIL_POL_PRM_DTL_MOP_CONTRB_LC") = txtCalc_MOP_Contrib_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_MOP_CONTRB_FC") = txtCalc_MOP_Contrib_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_ANN_CONTRB_LC") = Me.txtCalc_Ann_Contrib_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_ANN_CONTRB_FC") = Me.txtCalc_Ann_Contrib_FC.Text

                drNewRow("TBIL_POL_PRM_DTL_FIRST_PRM_LC") = txtCalc_First_Prem_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_FIRST_PRM_FC") = txtCalc_First_Prem_FC.Text


                drNewRow("TBIL_POL_PRM_DTL_NET_PRM_LC") = txtCalc_Net_Prem_LC.Text
                drNewRow("TBIL_POL_PRM_DTL_NET_PRM_FC") = txtCalc_Net_Prem_FC.Text


                drNewRow("TBIL_POL_PRM_DTL_FLAG") = RTrim("A")
                drNewRow("TBIL_POL_PRM_DTL_OPERID") = RTrim(myUserIDX)
                drNewRow("TBIL_POL_PRM_DTL_KEYDTE") = Format(Now, "MM/dd/yyyy")


                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMsg.Text = "New Record Saved to Database Successfully."

            Else
                '        '   Update existing record

                '        'm_rwContact = m_dtContacts.Rows(0)
                '        'm_rwContact("ContactName") = "Bob Brown"
                '        'm_rwContact.AcceptChanges()
                '        'm_dtContacts.AcceptChanges()
                '        'Dim intC As Integer = m_daDataAdapter.Update(m_dtContacts)


                With obj_DT
                    .Rows(0)("TBIL_POL_PRM_DTL_MDLE") = RTrim("I")

                    .Rows(0)("TBIL_POL_PRM_DTL_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_POL_PRM_DTL_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    .Rows(0)("TBIL_POL_PRM_DTL_POLY_NO") = RTrim(Me.txtPolNum.Text)

                    .Rows(0)("TBIL_POL_PRM_DTL_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)

                    .Rows(0)("TBIL_POL_PRM_DTL_SA_LC") = Me.txtCalc_SA_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_SA_FC") = Me.txtCalc_SA_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_LIFE_COVER_SA_LC") = Me.txtCalc_Life_Cover_SA_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_LIFE_COVER_SA_FC") = Me.txtCalc_Life_Cover_SA_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_BASIC_PRM_LC") = Me.txtCalc_Ann_Basic_Prem_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_BASIC_PRM_FC") = Me.txtCalc_Ann_Basic_Prem_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_ADDPREM_LC") = Me.txtCalc_Add_Prem_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_ADDPREM_FC") = Me.txtCalc_Add_Prem_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_LOADING_LC") = Me.txtCalc_Prem_Loading_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_LOADING_FC") = Me.txtCalc_Prem_Loading_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_DISCNT_LC") = Me.txtCalc_Prem_Disc_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_DISCNT_FC") = Me.txtCalc_Prem_Disc_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_CHG_LC") = Me.txtCalc_Add_Charges_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_CHG_FC") = Me.txtCalc_Add_Charges_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_MOP_FACTOR") = RTrim(Me.txtCalc_MOP_Factor_LC.Text)
                    .Rows(0)("TBIL_POL_PRM_DTL_MOP_PRM_LC") = Val(Me.txtCalc_MOP_Prem_LC.Text)
                    .Rows(0)("TBIL_POL_PRM_DTL_MOP_PRM_FC") = Val(Me.txtCalc_MOP_Prem_FC.Text)

                    .Rows(0)("TBIL_POL_PRM_DTL_TOT_PRM_LC") = Me.txtCalc_Total_Prem_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_TOT_PRM_FC") = Me.txtCalc_Total_Prem_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_ANN_PREM_LC") = Me.txtCalc_Ann_Prem_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_ANN_PREM_FC") = Me.txtCalc_Ann_Prem_FC.Text


                    ' next
                    .Rows(0)("TBIL_POL_PRM_DTL_MOP_CONTRB_LC") = txtCalc_MOP_Contrib_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_MOP_CONTRB_FC") = txtCalc_MOP_Contrib_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_ANN_CONTRB_LC") = Me.txtCalc_Ann_Contrib_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_ANN_CONTRB_FC") = Me.txtCalc_Ann_Contrib_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_FIRST_PRM_LC") = txtCalc_First_Prem_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_FIRST_PRM_FC") = txtCalc_First_Prem_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_NET_PRM_LC") = txtCalc_Net_Prem_LC.Text
                    .Rows(0)("TBIL_POL_PRM_DTL_NET_PRM_FC") = txtCalc_Net_Prem_FC.Text

                    .Rows(0)("TBIL_POL_PRM_DTL_FLAG") = RTrim("C")
                    '.Rows(0)("TBIL_POL_PRM_DTL_OPERID") = RTrim(myUserIDX)
                    '.Rows(0)("TBIL_POL_PRM_DTL_KEYDTE") = Format(Now, "MM/dd/yyyy")

                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMsg.Text = "Record Saved to Database Successfully."

            End If


            ''==========================================================================


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

        'Me.cmdNext.Enabled = True


        objOLEConn = New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            'Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            GoTo Proc_DoSave_End
            'Exit Sub
        End Try

        'Check for LIFE TERM HARVEST and AFAB
        If UCase(Trim(Me.txtProduct_Num.Text)) = "E003" And UCase(Trim(strAFAB_Num)) = "E003-4" And UCase(Trim(strAFAB_SW)) = "Y" Then
            'dblAFAB_Value
            strSQL = ""
            strSQL = strSQL & " update TBIL_POLICY_ADD_PREM"
            strSQL = strSQL & " set TBIL_POL_ADD_SA_LC = '" & Val(dblAFAB_Value) & "'"
            strSQL = strSQL & " ,TBIL_POL_ADD_SA_FC = '" & Val(dblAFAB_Value) & "'"
            strSQL = strSQL & " where TBIL_POL_ADD_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
            strSQL = strSQL & " and TBIL_POL_ADD_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
            strSQL = strSQL & " and TBIL_POL_ADD_REC_ID = '" & Val(intAFAB_Rec_ID) & "'"
            strSQL = strSQL & " and TBIL_POL_ADD_PRDCT_CD = '" & RTrim(Me.txtProduct_Num.Text) & "'"
            strSQL = strSQL & " and TBIL_POL_ADD_COVER_CD = '" & RTrim(strAFAB_Num) & "'"

            Dim objOLECmd_AFAB As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
            objOLECmd_AFAB.CommandType = CommandType.Text
            'objOLECmd_AFAB.CommandType = CommandType.StoredProcedure
            'objOLECmd_AFAB.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
            'objOLECmd_AFAB.Parameters.Add("p01", OleDbType.VarChar, 30).Value = strREC_ID
            'objOLECmd_AFAB.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

            Dim myC As Integer = 0
            myC = objOLECmd_AFAB.ExecuteNonQuery

            objOLECmd_AFAB.Dispose()
            objOLECmd_AFAB = Nothing
        Else
        End If


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

Proc_DoSave_End:

        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"

        'Call Proc_DataBind()
        'Call Proc_DoNew()


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
        strSQL = strSQL & "SELECT TOP 1 CALC_TBL.*"

        strSQL = strSQL & ",PROD.TBIL_PRDCT_DTL_CAT, PROD.TBIL_PRDCT_DTL_DESC, PROD.TBIL_PRDCT_DTL_MDLE"

        strSQL = strSQL & ",PRM.TBIL_POL_PRM_SA_LC"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_SA_FC"

        strSQL = strSQL & ",TBIL_POL_PRM_LIFE_COVER_SA_LC"
        strSQL = strSQL & ",TBIL_POL_PRM_LIFE_COVER_SA_FC"

        strSQL = strSQL & ",PRM.TBIL_POL_PRM_RT_APPLIED_ON"
        strSQL = strSQL & ",PRM.TBIL_POL_SA_FROM_PRM"

        strSQL = strSQL & ",PRM.TBIL_POL_PRM_RATE_CD"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_RATE"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_RATE_PER"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_PERIOD_YRS"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_ANN_CONTRIB_LC"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_ANN_CONTRIB_FC"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_MODE_PAYT"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_MOP_RATE"

        'Me.txtCalc_MOP_Factor_LC.Text = RTrim(Me.txtPrem_MOP_Type.Text)
        'Me.txtCalc_MOP_Factor_FC.Text = Trim(Me.txtCalc_MOP_Factor_LC.Text)

        strSQL = strSQL & ",PRM.TBIL_POL_PRM_FREE_LIFECOVER_LMT_LC"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_FREE_LIFECOVER_LMT_FC"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_LIFE_COVER"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_RT_TAB_FIX"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_RT_FIXED"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_RT_FIX_PER"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_ANN_CONTRIB_LC"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_ANN_CONTRIB_FC"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_SCH_TERM"
        strSQL = strSQL & ",PRM.TBIL_POL_PRM_FEE_PRD"

        strSQL = strSQL & " FROM " & strTable & " AS CALC_TBL"

        strSQL = strSQL & " LEFT JOIN TBIL_PRODUCT_DETL AS PROD"
        strSQL = strSQL & " ON PROD.TBIL_PRDCT_DTL_CODE = CALC_TBL.TBIL_POL_PRM_DTL_PRDCT_CD"
        strSQL = strSQL & " AND PROD.TBIL_PRDCT_DTL_MDLE IN('IND','I')"

        strSQL = strSQL & " LEFT JOIN TBIL_POLICY_PREM_INFO " & " AS PRM"
        strSQL = strSQL & " ON ( PRM.TBIL_POL_PRM_FILE_NO = CALC_TBL.TBIL_POL_PRM_DTL_FILE_NO "
        strSQL = strSQL & "  AND PRM.TBIL_POL_PRM_PROP_NO = CALC_TBL.TBIL_POL_PRM_DTL_PROP_NO )"

        strSQL = strSQL & " WHERE CALC_TBL.TBIL_POL_PRM_DTL_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND CALC_TBL.TBIL_POL_PRM_DTL_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        strSQL = strSQL & " AND CALC_TBL.TBIL_POL_PRM_DTL_PROP_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND CALC_TBL.TBIL_POL_PRM_DTL_POLY_NO = '" & RTrim(strP_ID) & "'"
        ' TO ALLOW FOR RE-CALCULATION
        strSQL = strSQL & " AND CALC_TBL.TBIL_POL_PRM_DTL_FILE_NO = '" & RTrim("XYZ") & "'"

        'strSQL = "SPIL_GET_POLICY_PREM_INFO"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        'objOLECmd.CommandTimeout = 180
        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.CommandType = CommandType.StoredProcedure
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 30).Value = strREC_ID
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_POLY_NO") & vbNullString, String))

            'Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_PRDCT_CAT_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim(Me.txtProductClass.Text))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("IND=") & RTrim(Me.txtProductClass.Text))
            Call gnProc_DDL_Get(Me.cboProductClass, RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_MDLE") & vbNullString, String)) & RTrim("=") & RTrim(Me.txtProductClass.Text))
            'If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Then
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("I=") & RTrim(Me.txtProductClass.Text))
            'End If

            'Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)
            'Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_PRDCT_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboProduct, RTrim(Me.txtProduct_Num.Text))
            'Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DESC") & vbNullString, String))

            'Call gnProc_Populate_Box("IL_COVER_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
            'Me.txtCover_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_COVER_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboCover_Name, RTrim(Me.txtCover_Num.Text))

            'Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)
            'Me.txtPlan_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_PLAN_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPlan_Name, RTrim(Me.txtPlan_Num.Text))


            'Me.txtPrem_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PERIOD_YRS") & vbNullString, String))

            'If IsDate(objOLEDR("TBIL_POL_PRM_FROM")) Then
            '    Me.txtPrem_Start_Date.Text = Format(CType(objOLEDR("TBIL_POL_PRM_FROM"), DateTime), "dd/MM/yyyy")
            'End If
            'If IsDate(objOLEDR("TBIL_POL_PRM_TO")) Then
            '    Me.txtPrem_End_Date.Text = Format(CType(objOLEDR("TBIL_POL_PRM_TO"), DateTime), "dd/MM/yyyy")
            'End If


            Me.txtCalc_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_SA_LC") & vbNullString, String))
            Me.txtCalc_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_SA_FC") & vbNullString, String))
            If Val(Trim(Me.txtCalc_SA_LC.Text)) = 0 Then
                Me.txtCalc_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_LC") & vbNullString, String))
            End If
            If Val(Trim(Me.txtCalc_SA_FC.Text)) = 0 Then
                Me.txtCalc_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_FC") & vbNullString, String))
            End If


            Me.txtCalc_Life_Cover_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_LIFE_COVER_SA_LC") & vbNullString, String))
            Me.txtCalc_Life_Cover_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_LIFE_COVER_SA_FC") & vbNullString, String))
            If Val(Trim(Me.txtCalc_Life_Cover_SA_LC.Text)) = 0 Then
                Me.txtCalc_Life_Cover_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER_SA_LC") & vbNullString, String))
            End If
            If Val(Trim(Me.txtCalc_Life_Cover_SA_FC.Text)) = 0 Then
                Me.txtCalc_Life_Cover_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER_SA_FC") & vbNullString, String))
            End If

            Me.txtPrem_Free_LiveCover_Lmt_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FREE_LIFECOVER_LMT_LC") & vbNullString, String))
            Me.txtPrem_Free_LiveCover_Lmt_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FREE_LIFECOVER_LMT_FC") & vbNullString, String))

            Me.txtPrem_School_Term.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SCH_TERM") & vbNullString, String))
            Me.txtPrem_Sch_Fee_Prd.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FEE_PRD") & vbNullString, String))

            Me.txtCalc_Ann_Basic_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_BASIC_PRM_LC") & vbNullString, String))
            Me.txtCalc_Ann_Basic_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_BASIC_PRM_FC") & vbNullString, String))

            Me.txtCalc_Add_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_ADDPREM_LC") & vbNullString, String))
            Me.txtCalc_Add_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_ADDPREM_FC") & vbNullString, String))

            Me.txtCalc_Prem_Loading_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_LOADING_LC") & vbNullString, String))
            Me.txtCalc_Prem_Loading_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_LOADING_FC") & vbNullString, String))

            Me.txtCalc_Prem_Disc_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_DISCNT_LC") & vbNullString, String))
            Me.txtCalc_Prem_Disc_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_DISCNT_FC") & vbNullString, String))

            Me.txtCalc_Add_Charges_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_CHG_LC") & vbNullString, String))
            Me.txtCalc_Add_Charges_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_CHG_FC") & vbNullString, String))


            'mop
            Me.txtCalc_MOP_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_MOP_PRM_LC") & vbNullString, String))
            Me.txtCalc_MOP_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_MOP_PRM_FC") & vbNullString, String))

            Me.txtCalc_MOP_Factor_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_MOP_FACTOR") & vbNullString, String))
            Me.txtCalc_MOP_Factor_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_MOP_FACTOR") & vbNullString, String))

            Me.txtCalc_MOP_Contrib_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_MOP_CONTRB_LC") & vbNullString, String))
            Me.txtCalc_MOP_Contrib_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_MOP_CONTRB_FC") & vbNullString, String))

            ' first premium
            Me.txtCalc_First_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_FIRST_PRM_LC") & vbNullString, String))
            Me.txtCalc_First_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_FIRST_PRM_FC") & vbNullString, String))


            Me.txtCalc_Total_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_TOT_PRM_LC") & vbNullString, String))
            Me.txtCalc_Total_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_TOT_PRM_FC") & vbNullString, String))

            Me.txtCalc_Ann_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_ANN_PREM_LC") & vbNullString, String))
            Me.txtCalc_Ann_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_ANN_PREM_FC") & vbNullString, String))

            ' FROM PREMIUM FILE
            '------------------
            'Me.txtCalc_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_LC") & vbNullString, String))
            'Me.txtCalc_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_FC") & vbNullString, String))

            Me.txtPrem_Rate_Code.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Rate_Code, RTrim(Me.txtPrem_Rate_Code.Text))
            Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE") & vbNullString, String))
            Me.txtPrem_Rate_PerNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE_PER") & vbNullString, String))

            Me.txtPrem_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PERIOD_YRS") & vbNullString, String))

            Me.txtCalc_Ann_Contrib_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_ANN_CONTRB_LC") & vbNullString, String))
            Me.txtCalc_Ann_Contrib_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_ANN_CONTRB_FC") & vbNullString, String))
            If Val(Trim(Me.txtCalc_Ann_Contrib_LC.Text)) = 0 Then
                Me.txtCalc_Ann_Contrib_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_LC") & vbNullString, String))
            End If
            If Val(Trim(Me.txtCalc_Ann_Contrib_FC.Text)) = 0 Then
                Me.txtCalc_Ann_Contrib_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_FC") & vbNullString, String))
            End If

            Me.txtCalc_Net_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_NET_PRM_LC") & vbNullString, String))
            Me.txtCalc_Net_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_NET_PRM_FC") & vbNullString, String))


            Me.txtPrem_MOP_Type.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MODE_PAYT") & vbNullString, String))
            Me.txtPrem_MOP_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MOP_RATE") & vbNullString, String))

            Me.txtCalc_MOP_Factor_LC.Text = RTrim(Me.txtPrem_MOP_Type.Text)
            Me.txtCalc_MOP_Factor_FC.Text = Trim(Me.txtCalc_MOP_Factor_LC.Text)

            Me.txtPrem_Life_CoverNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER") & vbNullString, String))
            Me.txtPrem_Rate_TypeNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_TAB_FIX") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPrem_Rate_Type, RTrim(Me.txtPrem_Rate_TypeNum.Text))
            'Call gnGET_SelectedItem(Me.cboPrem_Rate_Type, Me.txtPrem_Rate_TypeNum, Me.txtPrem_Rate_TypeName, Me.lblMsg)


            Me.txtPrem_Fixed_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_FIXED") & vbNullString, String))
            Me.txtPrem_Fixed_Rate_PerNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_FIX_PER") & vbNullString, String))

            Me.txtPrem_Rate_Applied_On.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_APPLIED_ON") & vbNullString, String))
            Me.txtPrem_Is_SA_From_PremNum.Text = RTrim(CType(objOLEDR("TBIL_POL_SA_FROM_PRM") & vbNullString, String))
            lblOriginator.Text = "Record Originally created by " & GetRecordOriginator(RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_OPERID") & vbNullString, String)))


            Select Case Trim(Me.txtPrem_Life_CoverNum.Text)
                Case "N"
                    'Me.txtCalc_Ann_Basic_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_LC") & vbNullString, String))
                    'Me.txtCalc_Ann_Basic_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_FC") & vbNullString, String))

            End Select

            strF_ID = RTrim(Me.txtFileNum.Text)
            'strREC_ID = RTrim(Me.txtFileNum.Text)
            strQ_ID = RTrim(Me.txtQuote_Num.Text)
            strP_ID = RTrim(Me.txtPolNum.Text)


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

            ' dispose of open objects
            objOLECmd.Dispose()

            If objOLEDR.IsClosed = False Then
                objOLEDR.Close()
            End If

        Else

            ' dispose of open objects
            objOLECmd.Dispose()
            objOLECmd = Nothing

            If objOLEDR.IsClosed = False Then
                objOLEDR.Close()
            End If

            strTable = strTableName
            strTable = "TBIL_POLICY_PREM_INFO"

            strSQL = ""
            strSQL = strSQL & "SELECT TOP 1 PRM_TBL.*"

            strSQL = strSQL & ",PROD.TBIL_PRDCT_DTL_CAT, PROD.TBIL_PRDCT_DTL_DESC, PROD.TBIL_PRDCT_DTL_MDLE"

            strSQL = strSQL & " FROM " & strTable & " AS PRM_TBL"

            strSQL = strSQL & " LEFT JOIN TBIL_PRODUCT_DETL AS PROD"
            strSQL = strSQL & " ON PROD.TBIL_PRDCT_DTL_CODE = PRM_TBL.TBIL_POL_PRM_PRDCT_CD"
            strSQL = strSQL & " AND PROD.TBIL_PRDCT_DTL_MDLE IN('IND','I')"

            strSQL = strSQL & " WHERE PRM_TBL.TBIL_POL_PRM_FILE_NO = '" & RTrim(strREC_ID) & "'"
            'If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            '    strSQL = strSQL & " AND PRM_TBL.TBIL_POL_PRM_REC_ID = '" & Val(FVstrRecNo) & "'"
            'End If
            strSQL = strSQL & " AND PRM_TBL.TBIL_POL_PRM_PROP_NO = '" & RTrim(strQ_ID) & "'"
            'strSQL = strSQL & " AND CALC.TBIL_POL_PRM_POLY_NO = '" & RTrim(strP_ID) & "'"

            'strSQL = "SPIL_GET_POLICY_PREM_INFO"



            objOLECmd = New OleDbCommand(strSQL, objOLEConn)
            'objOLECmd.CommandTimeout = 180
            objOLECmd.CommandType = CommandType.Text
            'objOLECmd.CommandType = CommandType.StoredProcedure
            'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
            'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 30).Value = strREC_ID
            'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)


            objOLEDR = objOLECmd.ExecuteReader()
            If (objOLEDR.Read()) Then
                strErrMsg = "true"

                Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FILE_NO") & vbNullString, String))
                'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
                'Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_REC_ID") & vbNullString, String))

                Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PROP_NO") & vbNullString, String))
                Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_POLY_NO") & vbNullString, String))

                'Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_PRDCT_CAT_CD") & vbNullString, String))
                'Call gnProc_DDL_Get(Me.cboProductClass, RTrim(Me.txtProductClass.Text))
                'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("IND=") & RTrim(Me.txtProductClass.Text))
                Call gnProc_DDL_Get(Me.cboProductClass, RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_MDLE") & vbNullString, String)) & RTrim("=") & RTrim(Me.txtProductClass.Text))
                'If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Then
                'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("I=") & RTrim(Me.txtProductClass.Text))
                'End If

                'Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)
                'Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PRDCT_CD") & vbNullString, String))
                Call gnProc_DDL_Get(Me.cboProduct, RTrim(Me.txtProduct_Num.Text))
                'Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DESC") & vbNullString, String))

                'Call gnProc_Populate_Box("IL_COVER_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
                'Me.txtCover_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_COVER_CD") & vbNullString, String))
                Call gnProc_DDL_Get(Me.cboCover_Name, RTrim(Me.txtCover_Num.Text))

                'Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)
                'Me.txtPlan_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_DTL_PLAN_CD") & vbNullString, String))
                Call gnProc_DDL_Get(Me.cboPlan_Name, RTrim(Me.txtPlan_Num.Text))

                'Me.txtPrem_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PERIOD_YRS") & vbNullString, String))

                'If IsDate(objOLEDR("TBIL_POL_PRM_FROM")) Then
                '    Me.txtPrem_Start_Date.Text = Format(CType(objOLEDR("TBIL_POL_PRM_FROM"), DateTime), "dd/MM/yyyy")
                'End If
                'If IsDate(objOLEDR("TBIL_POL_PRM_TO")) Then
                '    Me.txtPrem_End_Date.Text = Format(CType(objOLEDR("TBIL_POL_PRM_TO"), DateTime), "dd/MM/yyyy")
                'End If

                Me.txtCalc_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_LC") & vbNullString, String))
                Me.txtCalc_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SA_FC") & vbNullString, String))


                Me.txtCalc_Life_Cover_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER_SA_LC") & vbNullString, String))
                Me.txtCalc_Life_Cover_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER_SA_LC") & vbNullString, String))

                Me.txtPrem_Free_LiveCover_Lmt_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FREE_LIFECOVER_LMT_LC") & vbNullString, String))
                Me.txtPrem_Free_LiveCover_Lmt_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FREE_LIFECOVER_LMT_FC") & vbNullString, String))


                Me.txtPrem_School_Term.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_SCH_TERM") & vbNullString, String))
                Me.txtPrem_Sch_Fee_Prd.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_FEE_PRD") & vbNullString, String))

                Me.txtPrem_Rate_Code.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE_CD") & vbNullString, String))
                Call gnProc_DDL_Get(Me.cboPrem_Rate_Code, RTrim(Me.txtPrem_Rate_Code.Text))

                Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE") & vbNullString, String))
                Me.txtPrem_Rate_PerNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RATE_PER") & vbNullString, String))

                Me.txtPrem_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_PERIOD_YRS") & vbNullString, String))

                Me.txtCalc_Ann_Contrib_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_LC") & vbNullString, String))
                Me.txtCalc_Ann_Contrib_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_FC") & vbNullString, String))


                Me.txtPrem_MOP_Type.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MODE_PAYT") & vbNullString, String))
                Me.txtPrem_MOP_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_MOP_RATE") & vbNullString, String))

                Me.txtCalc_MOP_Factor_LC.Text = RTrim(Me.txtPrem_MOP_Type.Text)
                Me.txtCalc_MOP_Factor_FC.Text = Trim(Me.txtCalc_MOP_Factor_LC.Text)

                Me.txtPrem_Life_CoverNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_LIFE_COVER") & vbNullString, String))
                Me.txtPrem_Rate_TypeNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_TAB_FIX") & vbNullString, String))
                Call gnProc_DDL_Get(Me.cboPrem_Rate_Type, RTrim(Me.txtPrem_Rate_TypeNum.Text))
                'Call gnGET_SelectedItem(Me.cboPrem_Rate_Type, Me.txtPrem_Rate_TypeNum, Me.txtPrem_Rate_TypeName, Me.lblMsg)

                'Response.Write("<br />Rate Type (From DB): " & Me.txtPrem_Rate_TypeNum.Text)

                Me.txtPrem_Fixed_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_FIXED") & vbNullString, String))
                Me.txtPrem_Fixed_Rate_PerNum.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_FIX_PER") & vbNullString, String))

                Me.txtPrem_Rate_Applied_On.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_RT_APPLIED_ON") & vbNullString, String))
                Me.txtPrem_Is_SA_From_PremNum.Text = RTrim(CType(objOLEDR("TBIL_POL_SA_FROM_PRM") & vbNullString, String))
                lblOriginator.Text = "Record Originally created by " & GetRecordOriginator(RTrim(CType(objOLEDR("TBIL_POL_PRM_OPERID") & vbNullString, String)))

                Select Case Trim(Me.txtPrem_Life_CoverNum.Text)
                    Case "N"
                        Me.txtCalc_Ann_Basic_Prem_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_LC") & vbNullString, String))
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_PRM_ANN_CONTRIB_FC") & vbNullString, String))

                End Select



                '   FROM PREMIUM FILE
                'TBIL_POL_PRM_RATE_CD
                'TBIL_POL_PRM_RATE
                'TBIL_POL_PRM_PERIOD_YRS
                'TBIL_POL_PRM_DTL_ANN_CONTRB_LC
                'TBIL_POL_PRM_DTL_ANN_CONTRB_FC
                'TBIL_POL_PRM_MODE_PAYT
                'TBIL_POL_PRM_MOP_RATE

                'TBIL_POL_PRM_LIFE_COVER
                'TBIL_POL_SA_FROM_PRM
                'TBIL_POL_PRM_RT_TAB_FIX
                'TBIL_POL_PRM_RT_FIXED
                'TBIL_POL_PRM_RT_FIX_PER

                '   FROM POLICY FILE
                'TBIL_POLY_ASSRD_AGE


                strF_ID = RTrim(Me.txtFileNum.Text)
                'strREC_ID = RTrim(Me.txtFileNum.Text)
                strQ_ID = RTrim(Me.txtQuote_Num.Text)
                strP_ID = RTrim(Me.txtPolNum.Text)


            End If


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


            ' dispose of open objects
            objOLECmd.Dispose()

            If objOLEDR.IsClosed = False Then
                objOLEDR.Close()
            End If

        End If


        objOLECmd = Nothing

        objOLEDR = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Call Proc_DoCalc_Prem()

        Return strErrMsg

    End Function

    Private Sub Proc_DoCalc_Prem()

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


        '-----------------------------------

        Dim int_myrc As Integer = 0

        ' remove any previously created document items
        strDoc_Item_SQL = ""
        strDoc_Item_SQL = "DELETE FROM TBIL_POLICY_DOC_ITEMS"
        strDoc_Item_SQL = strDoc_Item_SQL & " where TBIL_POL_ITEM_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " and TBIL_POL_ITEM_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        If Trim(Me.txtPolNum.Text) <> "" Then
            'strDoc_Item_SQL = strDoc_Item_SQL & " and TBIL_POL_ITEM_POLY_NO = '" & RTrim(Me.txtPolNum.Text) & "'"
        End If
        strDoc_Item_SQL = strDoc_Item_SQL & " and TBIL_POL_ITEM_MDLE = '" & RTrim("I") & "'"

        Dim objDoc_Item As OleDbCommand = Nothing
        objDoc_Item = New OleDbCommand(strDoc_Item_SQL, objOLEConn)
        int_myrc = objDoc_Item.ExecuteNonQuery()



        '---------------------------------

        Dim strHas_Life_Cover As String
        Dim strRate_Type As String

        Dim dblTot_Sum_Assured_LC As Double = 0
        Dim dblTot_Sum_Assured_FC As Double = 0

        Dim dblLife_Cover_SA_LC As Double = 0
        Dim dblLife_Cover_SA_FC As Double = 0

        Dim dblFree_Life_Cover_Lmt_LC As Double = 0
        Dim dblFree_Life_Cover_Lmt_FC As Double = 0

        Dim dblAnnual_Contrib_LC As Double = 0
        Dim dblAnnual_Contrib_FC As Double = 0

        Dim dblTot_Annual_Contrib_LC As Double = 0
        Dim dblTot_Annual_Contrib_FC As Double = 0

        Dim dblPrem_Rate As Double = 0
        Dim dblRate_Per As Integer = 0
        Dim dblTerm As Double = 0


        ' loading/discount variables
        Dim dblDisc_Load_Prem_Amt As Double = 0
        Dim dblDisc_Load_Prem_Rate As Double = 0
        Dim dblDisc_Load_Rate_Per As Integer = 0
        Dim dblDisc_Load_SA_LC As Double
        Dim dblDisc_Load_SA_FC As Double

        Dim dblTotal_Disc_Prem_LC As Double = 0
        Dim dblTotal_Load_Prem_LC As Double = 0

        Dim dblTotal_Disc_Prem_FC As Double = 0
        Dim dblTotal_Load_Prem_FC As Double = 0

        Dim dblDisc_Load_Percent As Double = 0

        Dim dblTotal_Disc_Amt_LC As Double = 0
        Dim dblTotal_Load_Amt_LC As Double = 0

        Dim dblCharges_Amt As Double = 0
        Dim dblCharges_Rate As Double = 0
        Dim dblCharges_Rate_Per As Double = 0

        Dim dblTotal_Charges_LC As Double = 0
        Dim dblTotal_Charges_FC As Double = 0

        Dim dblMOP_Rate As Double = 0
        Dim dblMOP_Per As Double = 0
        Dim dblMOP_Contrib_LC As Double = 0
        Dim dblMOP_Contrib_FC As Double = 0

        Dim dblMOP_Fee As Double = 0
        Dim dblMOP_Prem_LC As Double = 0
        Dim dblMOP_Prem_FC As Double = 0

        Dim dblTop_MOP As Double = 0

        Dim dblAnnual_Prem_LC As Double = 0
        Dim dblAnnual_Prem_FC As Double = 0

        Dim dblFirst_Prem_LC As Double = 0
        Dim dblFirst_Prem_FC As Double = 0


        strHas_Life_Cover = Trim(Me.txtPrem_Life_CoverNum.Text)
        'txtPrem_Rate_TypeNum.Text = "T"
        strRate_Type = Trim(Me.txtPrem_Rate_TypeNum.Text)
        'Response.Write("<br />Rate Type: " & strRate_Type)


        dblFree_Life_Cover_Lmt_LC = Val(Me.txtPrem_Free_LiveCover_Lmt_LC.Text)
        dblFree_Life_Cover_Lmt_FC = Val(Me.txtPrem_Free_LiveCover_Lmt_FC.Text)
        dblFree_Life_Cover_Lmt_LC = Val(txtCalc_Life_Cover_SA_LC.Text)
        dblFree_Life_Cover_Lmt_FC = Val(txtCalc_Life_Cover_SA_FC.Text)

        dblPrem_Rate = 0
        dblRate_Per = 0

        dblAnnual_Contrib_LC = Val(Me.txtCalc_Ann_Contrib_LC.Text)
        dblAnnual_Contrib_FC = Val(Me.txtCalc_Ann_Contrib_FC.Text)

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
        myRetValue = MOD_GEN.gnGET_RATE("GET_IL_MOP_FACTOR", "IND", Me.txtCalc_MOP_Factor_LC.Text, "", "", "", Me.lblMsg, Nothing, Me.txtPrem_MOP_Per)
        If Left(LTrim(myRetValue), 3) = "ERR" Then
            dblMOP_Rate = 0
        Else
            dblMOP_Rate = Val(myRetValue)
            dblMOP_Per = Val(Me.txtPrem_MOP_Per.Text)
        End If



        '**********************************************************
        ' FUNERAL START CODES
        '**********************************************************
        'strREC_ID = Me.txtFileNum.Text
        Dim myTmp_Contrib As Double = 0
        Dim myTmp_SA As Double = 0
        Dim myTmp_Amt As Double = 0
        Dim myTmp_Tot_Rate As Double = 0

        Dim intFun As Integer = 0

        dblTot_Sum_Assured_LC = 0
        dblTot_Sum_Assured_FC = 0

        dblTot_Annual_Contrib_LC = 0
        dblTot_Annual_Contrib_FC = 0

        dblTop_MOP = 0

        dblTmp_Amt = 0
        dblTmp_AmtX = 0

        Select Case Trim(Me.txtProduct_Num.Text)
            Case "F001", "F002"
                strTable = strTableName
                strTable = "TBIL_FUNERAL_SA_TAB"
                strSQL = ""

                myTmp_Tot_Rate = 0
                dblTop_MOP = 0

                ' ---------------------
                ' sum the premium rate
                ' ---------------------
                strSQL = ""
                'strSQL = strSQL & "SELECT SUM(TBIL_FUN_PREM_RATE) AS TOT_RATE"
                strSQL = strSQL & "SELECT TBIL_FUN_PREM_RATE"
                strSQL = strSQL & " FROM " & strTable & ""
                strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND TBIL_FUN_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
                'strSQL = strSQL & " AND TBIL_FUN_POLY_NO = '" & RTrim(strP_ID) & "'"

                Dim objFun_Cmd_Rate As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
                'objFun_Cmd.CommandTimeout = 180
                objFun_Cmd_Rate.CommandType = CommandType.Text
                'objFun_Cmd_Rate.CommandType = CommandType.StoredProcedure
                'objFun_Cmd_Rate.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
                'objFun_Cmd_Rate.Parameters.Add("p01", OleDbType.VarChar, 40).Value = strREC_ID
                'objFun_Cmd_Rate.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

                Dim objFun_DR_Rate As OleDbDataReader

                objFun_DR_Rate = objFun_Cmd_Rate.ExecuteReader()
                'If (objFun_DR_Rate.Read()) Then
                'myTmp_Tot_Rate = objFun_DR_Rate("TOT_RATE")
                'Else
                'myTmp_Tot_Rate = Val(0)
                'End If
                Do While objFun_DR_Rate.Read()
                    'Response.Write("<br/>Rate: " & objFun_DR_Rate("TBIL_FUN_PREM_RATE"))
                    myTmp_Tot_Rate = myTmp_Tot_Rate + objFun_DR_Rate("TBIL_FUN_PREM_RATE")
                Loop

                'Response.Write("<br/>Total Rate: " & myTmp_Tot_Rate)

                objFun_DR_Rate.Close()
                objFun_Cmd_Rate.Dispose()

                objFun_DR_Rate = Nothing
                objFun_Cmd_Rate = Nothing


                ' ---------------------
                ' read the funeral data
                ' ---------------------
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_FUN_ANN_CONTRIB, TBIL_FUN_SA, TBIL_FUN_PREM_RATE, TBIL_FUN_PREM_PER"
                strSQL = strSQL & " FROM " & strTable & ""
                strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND TBIL_FUN_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
                'strSQL = strSQL & " AND TBIL_FUN_POLY_NO = '" & RTrim(strP_ID) & "'"

                Dim objFun_Cmd_Read As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
                'objFun_Cmd_Read.CommandTimeout = 180
                objFun_Cmd_Read.CommandType = CommandType.Text
                'objFun_Cmd_Read.CommandType = CommandType.StoredProcedure
                'objFun_Cmd_Read.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
                'objFun_Cmd_Read.Parameters.Add("p01", OleDbType.VarChar, 40).Value = strREC_ID
                'objFun_Cmd_Read.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)


                Dim objFun_DR_Read As OleDbDataReader

                objFun_DR_Read = objFun_Cmd_Read.ExecuteReader()
                Do While objFun_DR_Read.Read

                    dblPrem_Rate = Val(objFun_DR_Read("TBIL_FUN_PREM_RATE") & vbNullString)
                    dblRate_Per = Val(objFun_DR_Read("TBIL_FUN_PREM_PER") & vbNullString)

                    If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "S" Then
                        dblSum_Assured_LC = Val(objFun_DR_Read("TBIL_FUN_SA") & vbNullString)
                        dblSum_Assured_FC = dblSum_Assured_LC
                        If Val(dblSum_Assured_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblTmp_Amt = dblSum_Assured_LC * dblPrem_Rate / dblRate_Per
                        Else
                            dblTmp_Amt = 0
                        End If

                        dblTot_Sum_Assured_LC = dblTot_Sum_Assured_LC + dblSum_Assured_LC
                        dblTot_Annual_Contrib_LC = dblTot_Annual_Contrib_LC + dblTmp_Amt
                    End If


                    If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "P" Then
                        dblAnnual_Contrib_LC = Val(Me.txtPrem_Ann_Contrib_LC_Prm.Text)
                        dblAnnual_Contrib_FC = dblAnnual_Contrib_LC

                        'If Val(dblAnnual_Contrib_LC) > 0 And Val(Me.txtPrem_Enrollee_Num.Text) <> 0 Then
                        '    dblTmp_Amt = dblAnnual_Contrib_LC / Val(Me.txtPrem_Enrollee_Num.Text)
                        'Else
                        '    dblTmp_Amt = 0
                        'End If

                        If Val(dblAnnual_Contrib_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(myTmp_Tot_Rate) <> 0 Then
                            dblTmp_Amt = dblAnnual_Contrib_LC * dblPrem_Rate / myTmp_Tot_Rate
                        Else
                            dblTmp_Amt = 0
                        End If
                        'Response.Write("<br/>Premium Rate: " & dblPrem_Rate)
                        'Response.Write("<br/>Premium: " & dblTmp_Amt)

                        dblTot_Annual_Contrib_LC = dblTot_Annual_Contrib_LC + dblTmp_Amt

                        If Val(dblTmp_Amt) > 0 And dblMOP_Rate <> 0 Then
                            dblTmp_AmtX = (dblTmp_Amt / dblMOP_Rate)
                            dblTop_MOP = dblTop_MOP + dblTmp_AmtX
                        Else
                            dblTmp_AmtX = 0
                        End If
                        'Response.Write("<br/>MOP Contribution " & dblTmp_AmtX)

                        If Val(dblTmp_AmtX) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblTmp_Amt = dblTmp_AmtX * dblRate_Per / dblPrem_Rate
                        Else
                            dblTmp_Amt = 0
                        End If

                        'Response.Write("<br/>Sum Assured: " & dblTmp_Amt)
                        'Response.Write("<hr/>")
                        dblTot_Sum_Assured_LC = dblTot_Sum_Assured_LC + dblTmp_Amt
                    End If

                Loop

                objFun_DR_Read.Close()
                objFun_Cmd_Read.Dispose()

                objFun_DR_Read = Nothing
                objFun_Cmd_Read = Nothing


                myTmp_Contrib = dblTot_Annual_Contrib_LC
                myTmp_SA = dblTot_Sum_Assured_LC

                dblAnnual_Contrib_LC = Format(dblTot_Annual_Contrib_LC, "###########0.00")
                dblAnnual_Contrib_FC = dblAnnual_Contrib_LC
                Me.txtCalc_Ann_Contrib_LC.Text = dblAnnual_Contrib_LC.ToString
                Me.txtCalc_Ann_Contrib_FC.Text = dblAnnual_Contrib_FC.ToString

                dblAnnual_Basic_Prem_LC = dblAnnual_Contrib_LC
                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString
                dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                'XXXXXXXXXXXX
                Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString


                dblSum_Assured_LC = Format(dblTot_Sum_Assured_LC, "###########0.00")
                dblSum_Assured_FC = dblSum_Assured_LC
                Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString

                Me.txtCalc_Life_Cover_SA_LC.Text = Me.txtCalc_SA_LC.Text
                Me.txtCalc_Life_Cover_SA_FC.Text = Me.txtCalc_SA_FC.Text

                If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "S" Then
                    If dblAnnual_Contrib_LC <> 0 And dblMOP_Rate <> 0 And dblMOP_Per <> 0 Then
                        dblMOP_Contrib_LC = dblAnnual_Contrib_LC * dblMOP_Rate / dblMOP_Per
                    Else
                        'dblAnnual_Contrib_LC = 0
                        dblMOP_Contrib_LC = 0
                    End If
                Else
                    dblMOP_Contrib_LC = dblTop_MOP
                End If


                Me.txtCalc_MOP_Contrib_LC.Text = dblMOP_Contrib_LC.ToString
                dblMOP_Contrib_FC = dblMOP_Contrib_LC
                Me.txtCalc_MOP_Contrib_FC.Text = dblMOP_Contrib_FC.ToString


                Dim objFun_Cmd As OleDbCommand = Nothing

                'strSQL = ""
                'strSQL = strSQL & "SELECT SUM(TBIL_FUN_ANN_CONTRIB) AS TOT_CONTRIB, SUM(TBIL_FUN_SA) AS TOT_SA"
                'strSQL = strSQL & " FROM " & strTable & ""
                'strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(strREC_ID) & "'"
                'strSQL = strSQL & " AND TBIL_FUN_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
                ''strSQL = strSQL & " AND TBIL_FUN_POLY_NO = '" & RTrim(strP_ID) & "'"

                'objFun_Cmd = New OleDbCommand(strSQL, objOLEConn)
                ''objFun_Cmd.CommandTimeout = 180
                'objFun_Cmd.CommandType = CommandType.Text
                ''objFun_Cmd.CommandType = CommandType.StoredProcedure
                ''objFun_Cmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
                ''objFun_Cmd.Parameters.Add("p01", OleDbType.VarChar, 40).Value = strREC_ID
                ''objFun_Cmd.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

                'Dim objFun_DR As OleDbDataReader

                'objFun_DR = objFun_Cmd.ExecuteReader()
                'If (objFun_DR.Read()) Then
                '    myTmp_Contrib = Val(objFun_DR("TOT_CONTRIB") & vbNullString)
                '    myTmp_SA = Val(objFun_DR("TOT_SA") & vbNullString)
                'Else
                '    myTmp_Contrib = Val(0)
                '    myTmp_SA = Val(0)
                'End If

                'objFun_DR.Close()
                'objFun_Cmd.Dispose()

                'objFun_DR = Nothing
                'objFun_Cmd = Nothing

                ''for SA
                If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "S" Then
                    If Val(myTmp_SA) > 0 Then
                        myTmp_Amt = myTmp_Contrib
                        strSQL = ""
                        strSQL = strSQL & " UPDATE TBIL_POLICY_PREM_INFO"
                        strSQL = strSQL & " SET TBIL_POL_PRM_ANN_CONTRIB_LC = " & Val(myTmp_Amt)
                        strSQL = strSQL & " ,TBIL_POL_PRM_ANN_CONTRIB_FC = " & Val(myTmp_Amt)
                        strSQL = strSQL & " WHERE TBIL_POL_PRM_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
                        strSQL = strSQL & " AND TBIL_POL_PRM_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

                        'objFun_Cmd = New OleDbCommand(strSQL, objOLEConn)
                        ''objFun_Cmd.CommandTimeout = 180
                        'objFun_Cmd.CommandType = CommandType.Text
                        'intFun = objFun_Cmd.ExecuteNonQuery()

                        'objFun_Cmd.Dispose()
                        'objFun_Cmd = Nothing
                    End If
                End If

                ' ''for premium
                If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "P" Then
                    If Val(myTmp_Contrib) > 0 Then
                        myTmp_Amt = myTmp_SA
                        strSQL = ""
                        strSQL = strSQL & " UPDATE TBIL_POLICY_PREM_INFO"
                        strSQL = strSQL & " SET TBIL_POL_PRM_SA_LC = " & Val(myTmp_Amt)
                        strSQL = strSQL & " ,TBIL_POL_PRM_SA_FC = " & Val(myTmp_Amt)
                        strSQL = strSQL & " ,TBIL_POL_PRM_LIFE_COVER_SA_LC = " & Val(myTmp_Amt)
                        strSQL = strSQL & " ,TBIL_POL_PRM_LIFE_COVER_SA_FC = " & Val(myTmp_Amt)
                        strSQL = strSQL & " WHERE TBIL_POL_PRM_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
                        strSQL = strSQL & " AND TBIL_POL_PRM_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

                        'objFun_Cmd = New OleDbCommand(strSQL, objOLEConn)
                        ''objFun_Cmd.CommandTimeout = 180
                        'objFun_Cmd.CommandType = CommandType.Text
                        'intFun = objFun_Cmd.ExecuteNonQuery()

                        'objFun_Cmd.Dispose()
                        'objFun_Cmd = Nothing
                    End If
                End If

                'dblSum_Assured_LC = myTmp_SA
                'dblSum_Assured_FC = dblSum_Assured_LC
                'Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                'Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString

                'Me.txtCalc_Life_Cover_SA_LC.Text = Me.txtCalc_SA_LC.Text
                'Me.txtCalc_Life_Cover_SA_FC.Text = Me.txtCalc_SA_FC.Text


                'dblAnnual_Contrib_LC = myTmp_Contrib
                'dblAnnual_Contrib_FC = dblAnnual_Contrib_LC
                'Me.txtCalc_Ann_Contrib_LC.Text = dblAnnual_Contrib_LC.ToString
                'Me.txtCalc_Ann_Contrib_FC.Text = dblAnnual_Contrib_FC.ToString

                'dblAnnual_Basic_Prem_LC = dblAnnual_Contrib_LC
                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString
                'dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                'Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                'Response.Write("<br/>Premium: " & dblTot_Annual_Contrib_LC)


                GoTo START_ADD_PREM_RTN


        End Select
        '**********************************************************
        ' FUNERAL END CODES
        '**********************************************************




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

                                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                                Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

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

                                If Val(dblFree_Life_Cover_Lmt_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                    dblTmp_Amt = (dblFree_Life_Cover_Lmt_LC / dblRate_Per) * dblPrem_Rate
                                End If


                                dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                                dblAnnual_Basic_Prem_LC = dblTmp_Amt
                                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblTmp_Amt
                                Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

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
                                If IsNumeric(Trim(Me.txtPrem_Rate_PerNum.Text)) Then
                                    dblRate_Per = CType(Trim(Me.txtPrem_Rate_PerNum.Text), Integer)
                                End If

                                'If Val(dblFree_Life_Cover_Lmt_LC) > 0 And Val(Me.txtPrem_Sch_Fee_Prd.Text) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                '    dblTmp_Amt = (dblFree_Life_Cover_Lmt_LC / (3 * Val(Me.txtPrem_Sch_Fee_Prd.Text))) / dblRate_Per * dblPrem_Rate
                                'End If
                                If Val(dblFree_Life_Cover_Lmt_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                    dblTmp_Amt = (dblFree_Life_Cover_Lmt_LC / dblRate_Per) * dblPrem_Rate
                                End If

                                dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                                dblAnnual_Basic_Prem_LC = dblTmp_Amt
                                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblTmp_Amt
                                Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

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

                                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                                Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

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
                                'change in calculation mode
                                'If Val(dblTmp_Amt) > 0 And Val(Me.txtPrem_Sch_Fee_Prd.Text) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                '    dblSum_Assured_LC = ((dblTmp_Amt / dblMOP_Rate) * (3 * Val(Me.txtPrem_Sch_Fee_Prd.Text))) / dblPrem_Rate * dblRate_Per
                                'End If

                                If Val(dblTmp_Amt) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                    dblSum_Assured_LC = (dblTmp_Amt / dblMOP_Rate) / (dblPrem_Rate * dblRate_Per)
                                End If


                                dblSum_Assured_LC = Format(dblSum_Assured_LC, "###########0.00")
                                dblSum_Assured_FC = dblSum_Assured_LC

                                Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                                Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString

                                txtCalc_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                                txtCalc_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString

                                dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                                dblAnnual_Basic_Prem_LC = dblTmp_Amt
                                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblTmp_Amt
                                Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

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
                                If IsNumeric(Trim(Me.txtPrem_Rate_PerNum.Text)) Then
                                    dblRate_Per = CType(Trim(Me.txtPrem_Rate_PerNum.Text), Integer)
                                End If

                                'change in calculation mode
                                'If Val(dblTmp_Amt) > 0 And Val(Me.txtPrem_Sch_Fee_Prd.Text) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                '    dblSum_Assured_LC = ((dblTmp_Amt / dblMOP_Rate) * (3 * Val(Me.txtPrem_Sch_Fee_Prd.Text))) / dblPrem_Rate * dblRate_Per
                                'End If

                                If Val(dblTmp_Amt) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                                    dblSum_Assured_LC = (dblTmp_Amt / dblMOP_Rate) / (dblPrem_Rate * dblRate_Per)
                                End If


                                dblSum_Assured_LC = Format(dblSum_Assured_LC, "###########0.00")
                                dblSum_Assured_FC = dblSum_Assured_LC

                                Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                                Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString

                                txtCalc_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                                txtCalc_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString

                                dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                                dblAnnual_Basic_Prem_LC = dblTmp_Amt
                                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                                dblAnnual_Basic_Prem_FC = dblTmp_Amt
                                Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                            Case Else
                                '*Rate   -  0 
                                'Rate per   - 0.

                        End Select

                End Select

                GoTo Skip_C001

        End Select

        '******************************************************
        ' END CODES - Education Endowment -- E001
        '******************************************************


        Select Case Trim(Me.txtProduct_Num.Text)
            Case "F001", "F002"

                GoTo Skip_C001
        End Select



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
                dblTmp_Amt = 0
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

                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                        dblAnnual_Contrib_LC = Val(Me.txtCalc_Ann_Contrib_LC.Text)
                        dblSum_Assured_LC = dblAnnual_Contrib_LC * Val(Me.txtPrem_Period_Yr.Text)
                        Me.txtCalc_SA_LC.Text = dblSum_Assured_LC
                        Me.txtCalc_SA_FC.Text = dblSum_Assured_LC

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

                        Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                        Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString

                        txtCalc_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                        txtCalc_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString

                        dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                        dblAnnual_Basic_Prem_LC = dblTmp_Amt
                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblTmp_Amt
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

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
                        If IsNumeric(Trim(Me.txtPrem_Rate_PerNum.Text)) Then
                            dblRate_Per = CType(Trim(Me.txtPrem_Rate_PerNum.Text), Integer)
                        End If

                        If Val(dblTmp_Amt) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblSum_Assured_LC = dblTmp_Amt * dblRate_Per / dblPrem_Rate
                        End If

                        dblSum_Assured_LC = Format(dblSum_Assured_LC, "###########0.00")
                        dblSum_Assured_FC = dblSum_Assured_LC

                        'Response.Write("Sum Assured: " & dblSum_Assured_LC)

                        Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                        Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString

                        txtCalc_Life_Cover_SA_LC.Text = dblSum_Assured_LC.ToString
                        txtCalc_Life_Cover_SA_FC.Text = dblSum_Assured_FC.ToString

                        dblTmp_Amt = Format(dblTmp_Amt, "###########0.00")
                        dblAnnual_Basic_Prem_LC = dblTmp_Amt
                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblTmp_Amt
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                    Case Else
                        '*Rate   -  0 
                        'Rate per   - 0.

                End Select

                GoTo Skip_C001

        End Select

        '******************************************************
        ' END CODES - CALCULATE SUM ASSURED FROM PREMIUM
        '******************************************************


        dblPrem_Rate = 0
        dblRate_Per = 0

        dblSum_Assured_LC = Val(Trim(Me.txtCalc_SA_LC.Text))
        dblSum_Assured_FC = Val(Trim(Me.txtCalc_SA_FC.Text))

        dblSum_Assured_LC = Val(Trim(Me.txtCalc_Life_Cover_SA_LC.Text))
        dblSum_Assured_FC = Val(Trim(Me.txtCalc_Life_Cover_SA_FC.Text))

        dblLife_Cover_SA_LC = Val(Trim(Me.txtCalc_Life_Cover_SA_LC.Text))
        dblLife_Cover_SA_FC = Val(Trim(Me.txtCalc_Life_Cover_SA_FC.Text))


        dblAnnual_Basic_Prem_LC = 0
        dblAnnual_Basic_Prem_FC = 0

        dblTotal_Add_Prem_LC = 0
        dblTotal_Add_Prem_FC = 0



        Select Case UCase(Trim(strHas_Life_Cover))
            Case "N"
                '(e1)	If No Life Cover or Rate Table not found 
                'move Policy Premium Information TBIL_POL_PRM_ANN_CONTRIB_LC to
                'TBIL_POL_PRM_DTL_BASIC_PRM_LC
                'TBIL_POL_PRM_DTL_BASIC_PRM_FC

                If IsNumeric(Trim(Me.txtCalc_Ann_Basic_Prem_LC.Text)) Then
                    dblAnnual_Basic_Prem_LC = CType(Trim(Me.txtCalc_Ann_Basic_Prem_LC.Text), Double)
                End If
                If IsNumeric(Trim(Me.txtCalc_Ann_Basic_Prem_FC.Text)) Then
                    dblAnnual_Basic_Prem_FC = CType(Trim(Me.txtCalc_Ann_Basic_Prem_FC.Text), Double)
                End If

                Select Case UCase(Trim(strRate_Type))
                    Case "N"
                        dblAnnual_Basic_Prem_LC = 0
                        dblAnnual_Basic_Prem_FC = 0

                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                        dblSum_Assured_LC = Val(Me.txtCalc_SA_LC.Text)
                        dblAnnual_Contrib_LC = dblSum_Assured_LC / Val(Me.txtPrem_Period_Yr.Text)
                        Me.txtCalc_Ann_Contrib_LC.Text = dblAnnual_Contrib_LC
                        Me.txtCalc_Ann_Contrib_FC.Text = dblAnnual_Contrib_LC

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

                        If IsNumeric(Trim(Me.txtPrem_Fixed_Rate.Text)) Then
                            dblPrem_Rate = CType(Trim(Me.txtPrem_Fixed_Rate.Text), Double)
                        End If
                        If IsNumeric(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text)) Then
                            dblRate_Per = CType(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text), Integer)
                        End If

                        If Val(dblSum_Assured_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblAnnual_Basic_Prem_LC = dblSum_Assured_LC * dblPrem_Rate / dblRate_Per
                        End If

                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

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

                        If IsNumeric(Trim(Me.txtPrem_Rate.Text)) Then
                            dblPrem_Rate = CType(Trim(Me.txtPrem_Rate.Text), Double)
                        End If
                        If IsNumeric(Trim(Me.txtPrem_Rate_PerNum.Text)) Then
                            dblRate_Per = CType(Trim(Me.txtPrem_Rate_PerNum.Text), Integer)
                        End If

                        If Val(dblSum_Assured_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblAnnual_Basic_Prem_LC = dblSum_Assured_LC * dblPrem_Rate / dblRate_Per
                        End If
                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString


                    Case Else
                        '*Rate   -  0 
                        'Rate per   - 0.

                End Select

            Case "Y"
                Select Case UCase(Trim(strRate_Type))
                    Case "N"
                        dblAnnual_Basic_Prem_LC = 0
                        dblAnnual_Basic_Prem_FC = 0

                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

                    Case "F"
                        'If TBIL_POL_PRM_RT_TAB_FIX of Policy file Premium information = ‘F’ 
                        'use the fixed rate instead of Table rate. 
                        'Use the Fixed Rate per Go to (e0)

                        '* Rat = eTBIL_POL_PRM_RT_FIXED
                        '*Rate per = TBIL_POL_PRM_RT_FIX_PER

                        If IsNumeric(Trim(Me.txtPrem_Fixed_Rate.Text)) Then
                            dblPrem_Rate = CType(Trim(Me.txtPrem_Fixed_Rate.Text), Double)
                        End If
                        If IsNumeric(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text)) Then
                            dblRate_Per = CType(Trim(Me.txtPrem_Fixed_Rate_PerNum.Text), Integer)
                        End If

                        If Val(dblSum_Assured_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblAnnual_Basic_Prem_LC = dblSum_Assured_LC * dblPrem_Rate / dblRate_Per
                        End If
                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString

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


                        If IsNumeric(Trim(Me.txtPrem_Rate.Text)) Then
                            dblPrem_Rate = CType(Trim(Me.txtPrem_Rate.Text), Double)
                        End If
                        If IsNumeric(Trim(Me.txtPrem_Rate_PerNum.Text)) Then
                            dblRate_Per = CType(Trim(Me.txtPrem_Rate_PerNum.Text), Integer)
                        End If

                        If Val(dblSum_Assured_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                            dblAnnual_Basic_Prem_LC = dblSum_Assured_LC * dblPrem_Rate / dblRate_Per
                        End If
                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString

                        dblAnnual_Basic_Prem_FC = dblAnnual_Basic_Prem_LC
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = dblAnnual_Basic_Prem_FC.ToString


                    Case Else
                        '*Rate   -  0 
                        'Rate per   - 0.

                End Select


        End Select


Skip_C001:

        Select Case UCase(Me.txtProduct_Num.Text)
            Case "I005"
                'Investment Plust
                If Val(dblAnnual_Contrib_LC) <> 0 And Val(dblTerm) <> 0 Then
                    dblLife_Cover_SA_LC = 0
                    dblLife_Cover_SA_FC = 0
                    dblLife_Cover_SA_LC = dblAnnual_Contrib_LC * dblTerm
                    dblLife_Cover_SA_FC = dblLife_Cover_SA_LC
                    Me.txtCalc_Life_Cover_SA_LC.Text = dblLife_Cover_SA_LC.ToString
                    Me.txtCalc_Life_Cover_SA_FC.Text = dblLife_Cover_SA_FC.ToString

                    dblAnnual_Basic_Prem_LC = 0
                    dblAnnual_Basic_Prem_FC = 0
                    Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString
                    Me.txtCalc_Ann_Basic_Prem_FC.Text = Me.txtCalc_Ann_Basic_Prem_LC.Text
                End If

            Case "I001"
                ' Capital Builder
                'If Val(dblAnnual_Contrib_LC) <> 0 And Val(dblTerm) <> 0 Then
                'dblLife_Cover_SA_LC = 0
                'dblLife_Cover_SA_FC = 0
                'dblLife_Cover_SA_LC = dblAnnual_Contrib_LC * 10
                'dblLife_Cover_SA_FC = dblLife_Cover_SA_LC
                'Me.txtCalc_Life_Cover_SA_LC.Text = dblLife_Cover_SA_LC.ToString
                'Me.txtCalc_Life_Cover_SA_FC.Text = dblLife_Cover_SA_FC.ToString

                'dblAnnual_Basic_Prem_LC = 0
                'dblAnnual_Basic_Prem_FC = 0
                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString
                'Me.txtCalc_Ann_Basic_Prem_FC.Text = Me.txtCalc_Ann_Basic_Prem_LC.Text
                'End If

            Case "I002"
                'Dollor Link
                'If Val(dblAnnual_Contrib_LC) <> 0 And Val(dblTerm) <> 0 Then
                dblLife_Cover_SA_LC = 0
                dblLife_Cover_SA_FC = 0
                dblLife_Cover_SA_LC = dblAnnual_Contrib_LC * 10
                dblLife_Cover_SA_FC = dblLife_Cover_SA_LC
                Me.txtCalc_Life_Cover_SA_LC.Text = dblLife_Cover_SA_LC.ToString
                Me.txtCalc_Life_Cover_SA_FC.Text = dblLife_Cover_SA_FC.ToString

                dblAnnual_Basic_Prem_LC = 0
                dblAnnual_Basic_Prem_FC = 0
                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString
                Me.txtCalc_Ann_Basic_Prem_FC.Text = Me.txtCalc_Ann_Basic_Prem_LC.Text
                'End If

            Case "I004"
                'Personal provident
                'If Val(dblAnnual_Contrib_LC) <> 0 And Val(dblTerm) <> 0 Then
                dblLife_Cover_SA_LC = 0
                dblLife_Cover_SA_FC = 0
                dblLife_Cover_SA_LC = dblAnnual_Contrib_LC * 10
                If Val(dblLife_Cover_SA_LC) > 500000 Then
                    dblLife_Cover_SA_LC = 500000
                End If

                dblLife_Cover_SA_FC = dblLife_Cover_SA_LC
                Me.txtCalc_Life_Cover_SA_LC.Text = dblLife_Cover_SA_LC.ToString
                Me.txtCalc_Life_Cover_SA_FC.Text = dblLife_Cover_SA_FC.ToString

                dblAnnual_Basic_Prem_LC = 0
                dblAnnual_Basic_Prem_FC = 0
                Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString
                Me.txtCalc_Ann_Basic_Prem_FC.Text = Me.txtCalc_Ann_Basic_Prem_LC.Text

            Case "E003"
                'LIFE TIME HARVEST
                'NEW_AFAB value

                'If Val(dblAnnual_Contrib_LC) <> 0 And Val(dblTerm) <> 0 Then
                'If Val(dblLife_Cover_SA_LC) > 500000 Then
                '    dblLife_Cover_SA_LC = 500000
                'Else
                '    dblSum_Assured_LC = dblLife_Cover_SA_LC
                '    dblSum_Assured_FC = dblSum_Assured_LC
                '    Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                '    Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString
                'End If

                'If Val(dblSum_Assured_LC) > 500000 Then
                '    dblSum_Assured_LC = 500000
                'Else
                'End If

                'dblSum_Assured_FC = dblSum_Assured_LC

                'Me.txtCalc_SA_LC.Text = dblSum_Assured_LC.ToString
                'Me.txtCalc_SA_FC.Text = dblSum_Assured_FC.ToString

                'dblLife_Cover_SA_LC = dblSum_Assured_LC
                'dblLife_Cover_SA_FC = dblLife_Cover_SA_LC

                'Me.txtCalc_Life_Cover_SA_LC.Text = dblLife_Cover_SA_LC.ToString
                'Me.txtCalc_Life_Cover_SA_FC.Text = dblLife_Cover_SA_FC.ToString

                'dblAnnual_Basic_Prem_LC = 0
                'dblAnnual_Basic_Prem_FC = 0
                'Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString
                'Me.txtCalc_Ann_Basic_Prem_FC.Text = Me.txtCalc_Ann_Basic_Prem_LC.Text
                'End If
            Case Else

        End Select


ESU_RTN_START:
        'Esusu start computation
        '-----------------------------

        Select Case UCase(Me.txtProduct_Num.Text)
            Case "I003"
                'ESUSU
            Case Else
                GoTo ESU_RTN_END

        End Select

        strTable = strTableName
        strTable = "TBIL_ESUSU_RATE"

        Dim dblEsu_Rate As Double
        Dim dblEsu_Rate_Per As Double
        dblEsu_Rate = 0
        dblEsu_Rate_Per = 0

        myTmp_Contrib = 0
        Select Case RTrim(Me.txtPrem_MOP_Type.Text)
            Case "D"
                myTmp_Contrib = dblAnnual_Contrib_LC / 365
            Case "M"
                myTmp_Contrib = dblAnnual_Contrib_LC / 12
            Case "W"
                myTmp_Contrib = dblAnnual_Contrib_LC / 52
        End Select

        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 ESU_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS ESU_TBL"
        strSQL = strSQL & " WHERE ESU_TBL.TBIL_ESU_PRDCT_CD = '" & RTrim(Me.txtProduct_Num.Text) & "'"
        strSQL = strSQL & " AND ESU_TBL.TBIL_ESU_MOP = '" & RTrim(Me.txtPrem_MOP_Type.Text) & "'"
        strSQL = strSQL & " AND ESU_TBL.TBIL_ESU_RT_MDLE IN('IND','I')"
        strSQL = strSQL & " AND " & myTmp_Contrib & " BETWEEN ESU_TBL.TBIL_ESU_CONTRIB_START AND ESU_TBL.TBIL_ESU_CONTRIB_END"


        Dim objOLECmd_esu As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        'objOLECmd_esu.CommandTimeout = 180
        objOLECmd_esu.CommandType = CommandType.Text
        'objOLECmd_esu.CommandType = CommandType.StoredProcedure
        'objOLECmd_esu.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd_esu.Parameters.Add("p01", OleDbType.VarChar, 30).Value = strREC_ID
        'objOLECmd_esu.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR_esu As OleDbDataReader

        objOLEDR_esu = objOLECmd_esu.ExecuteReader()

        If (objOLEDR_esu.Read()) Then
            dblEsu_Rate = Val(Trim(CType(objOLEDR_esu("TBIL_ESU_PRM_RATE") & vbNullString, String)))
            dblEsu_Rate_Per = Val(Trim(CType(objOLEDR_esu("TBIL_ESU_PRM_RT_PER") & vbNullString, String)))

            Select Case UCase(Me.txtProduct_Num.Text)
                Case "I003"
                    Me.txtCalc_Life_Cover_SA_LC.Text = Trim(CType(objOLEDR_esu("TBIL_ESU_LIFE_SA") & vbNullString, String))
                    Me.txtCalc_Life_Cover_SA_FC.Text = Me.txtCalc_Life_Cover_SA_LC.Text

                    dblLife_Cover_SA_LC = Val(Me.txtCalc_Life_Cover_SA_LC.Text)
                    If Val(dblLife_Cover_SA_LC) <> 0 And Val(dblEsu_Rate) <> 0 And Val(dblEsu_Rate_Per) <> 0 Then
                        dblAnnual_Basic_Prem_LC = dblLife_Cover_SA_LC * dblEsu_Rate / dblEsu_Rate_Per
                        Me.txtCalc_Ann_Basic_Prem_LC.Text = dblAnnual_Basic_Prem_LC.ToString
                        Me.txtCalc_Ann_Basic_Prem_FC.Text = Me.txtCalc_Ann_Basic_Prem_LC.Text
                    End If

                Case Else

            End Select
        End If


        objOLECmd_esu = Nothing
        objOLEDR_esu = Nothing

ESU_RTN_END:
        'Esusu end computation
        '-----------------------------



START_ADD_PREM_RTN:


        '******************************************
        ' CALCULATE MOP BASIC PREMIUM
        ' dblAnnual_Basic_Prem_LC
        ' dblAnnual_Basic_Prem_FC

        dblTmp_Amt = 0

        Select Case Trim(Me.txtPrem_Rate_Applied_On.Text)
            Case "P"
                If Val(dblAnnual_Basic_Prem_LC) > 0 And Val(dblMOP_Per) <> 0 Then
                    dblTmp_Amt = dblAnnual_Basic_Prem_LC / dblMOP_Per
                End If

                dblMOP_Basic_Prem_LC = dblTmp_Amt
                Me.txtCalc_MOP_Basic_LC.Text = dblMOP_Basic_Prem_LC.ToString

                dblMOP_Basic_Prem_FC = dblMOP_Basic_Prem_LC
                Me.txtCalc_MOP_Basic_FC.Text = dblMOP_Basic_Prem_FC.ToString

            Case Else

                '   compute MOP basic premium
                '   ----------------------------------------
                If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblMOP_Rate) <> 0 And Val(dblMOP_Per) <> 0 Then
                    dblTmp_Amt = dblAnnual_Basic_Prem_LC / dblMOP_Per
                End If
                'Response.Write("<br/>MOP Contribution: " & dblTmp_Amt.ToString)

                dblMOP_Basic_Prem_LC = dblTmp_Amt
                Me.txtCalc_MOP_Basic_LC.Text = dblMOP_Basic_Prem_LC.ToString

                dblMOP_Basic_Prem_FC = dblMOP_Basic_Prem_LC
                Me.txtCalc_MOP_Basic_FC.Text = dblMOP_Basic_Prem_FC.ToString

        End Select



        '***********************************************************************************************
        ' START ADDITIONAL COVERS CODES 
        '***********************************************************************************************


        '====================================
        '(f)	Calculate Additional Premium
        '====================================

        Call Proc_DoCalc_AddPrem(objOLEConn, RTrim("ADD_COV"))


        dblTotal_Prem_LC = dblAnnual_Basic_Prem_LC + dblTotal_Add_Prem_LC
        Me.txtCalc_Total_Prem_LC.Text = dblTotal_Prem_LC.ToString
        dblTotal_Prem_FC = dblAnnual_Basic_Prem_FC + dblTotal_Add_Prem_FC
        Me.txtCalc_Total_Prem_FC.Text = dblTotal_Prem_FC.ToString

        '***********************************************************************************************
        ' END ADDITIONAL COVERS CODES 
        '***********************************************************************************************




        '====================================
        '(i)	Calculate Discount/Loadings
        '====================================

        'Read all records in the Policy Loading/Discount Table TBIL_POLICY_DISCT_LOAD. for the Policy 

        '(i1)	If TBIL_POL_DISCT_LOAD_RT_AMT_CD =   ‘A’      i.e  Use Amount
        '	and TBIL_POL_DISC_LOAD_TYP = ‘D’ (Discount)
        '	Add TBIL_POL_DISC_LOAD_VALUE_LC to Total Discount Amount.
        ' Else if TBIL_POL_DISC_LOAD_TYP = ‘L’ (Loading)
        '	Add TBIL_POL_DISC_LOAD_VALUE_LC to Total Loading Amount

        '(i2)	If TBIL_POL_DISC_LOAD_RT_AMT_CD = ‘R’     (use Rate)
        'and TBIL_POL_DISC_APPLIED_ON = ‘P’ (Basic Prem) then
        ' Discount/Loading Amount =
        'TBIL_POL_PRM_DTL_BASIC_PRM_LC Multiplied by TBIL_POL_DISC_RATE divided by TBIL_POL_DISC_RATE_PER

        '(i3)	if TBIL_POL_DISC_LOAD_RT_AMT_CD = ‘R’ and 
        '	TBIL_POL_DISC_APPLIED_ON =  “T”    (Total Prem)
        '	Then Discount/Load Amount  = 
        '	TBIL_POL_PRM_DTL_TOT_PRM_LC multiplied by TBIL_POL_DISC_RATE Divided by TBIL_POL_DISC_RATE_PER.

        '(i4)	If TBIL_POL_DISC_LOAD_TYP = ‘D’  (Discount)
        'Add Discount/Load Amount to Total Discount Amount else Add   Discount/Load Amount to Total Loading Amount
        'Move Total Loading Amount to TBIL_POL_PRM_DTL_LOADING_LC
        '                TBIL_POL_PRM_DTL_LOADING_FC()
        'Display on screen
        'Move Total Discount Amount to TBIL_POL_PRM_DTL_DISCNT_LC
        '                TBIL_POL_PRM_DTL_DISCNT_FC()
        'Display on screen


        strTable = strTableName
        strTable = "TBIL_POLICY_DISCT_LOAD"


        strSQL = ""
        strSQL = strSQL & "SELECT DL_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS DL_TBL"
        strSQL = strSQL & " WHERE DL_TBL.TBIL_POL_DISC_FILE_NO = '" & RTrim(strREC_ID) & "'"
        'If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
        'strSQL = strSQL & " AND DL_TBL.TBIL_POL_DISC_REC_ID = '" & Val(FVstrRecNo) & "'"
        'End If
        strSQL = strSQL & " AND DL_TBL.TBIL_POL_DISC_PROP_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND DL_TBL.TBIL_POL_DISC_POLY_NO = '" & RTrim(strP_ID) & "'"

        'strSQL = "SPIL_GET_POLICY_DISCT_LOAD"

        Dim objOLECmd_DL As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        'objOLECmd_CHG.CommandTimeout = 180
        objOLECmd_DL.CommandType = CommandType.Text
        'objOLECmd_DL.CommandType = CommandType.StoredProcedure
        'objOLECmd_DL.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd_DL.Parameters.Add("p01", OleDbType.VarChar, 30).Value = strREC_ID
        'objOLECmd_DL.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR_DL As OleDbDataReader

        objOLEDR_DL = objOLECmd_DL.ExecuteReader()

        Do While objOLEDR_DL.Read()

            '   TBIL_POL_DISC_LOAD_RT_AMT_CD
            '   TBIL_POL_DISC_LOAD_PREM_AMT
            '   TBIL_POL_DISC_LOAD_PCENT
            '   TBIL_POL_DISC_APPLIED_ON
            '   TBIL_POL_DISC_COVER_CD
            '   TBIL_POL_DISC_RATE
            '   TBIL_POL_DISC_RATE_PER
            '   TBIL_POL_DISC_LOAD_VALUE_LC

            Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_RT_AMT_CD") & vbNullString, String)))
                Case "A"    'USE FIXED AMOUNT
                    dblTmp_Amt = 0
                    dblDisc_Load_Prem_Amt = 0

                    Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_TYPE") & vbNullString, String)))
                        Case "D"    ' DISCOUNT
                            dblDisc_Load_Prem_Amt = 0
                            dblTmp_Amt = 0
                            If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PREM_AMT") & vbNullString, String)) Then
                                dblDisc_Load_Prem_Amt = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PREM_AMT") & vbNullString, Double)
                            End If
                            dblTmp_Amt = dblDisc_Load_Prem_Amt
                            dblTotal_Disc_Prem_LC = dblTotal_Disc_Prem_LC + dblTmp_Amt

                        Case "L"    ' LOADING
                            dblDisc_Load_Prem_Amt = 0
                            dblTmp_Amt = 0
                            If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PREM_AMT") & vbNullString, String)) Then
                                dblDisc_Load_Prem_Amt = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PREM_AMT") & vbNullString, Double)
                            End If
                            dblTmp_Amt = dblDisc_Load_Prem_Amt
                            dblTotal_Load_Prem_LC = dblTotal_Load_Prem_LC + dblTmp_Amt

                    End Select

                Case "R"    ' USE RATE TABLE
                    dblDisc_Load_Prem_Rate = 0
                    dblDisc_Load_Rate_Per = 0
                    dblDisc_Load_SA_LC = 0
                    dblDisc_Load_SA_FC = 0

                    dblTmp_Amt = 0

                    If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_RATE") & vbNullString, String)) Then
                        dblDisc_Load_Prem_Rate = CType(objOLEDR_DL("TBIL_POL_DISC_RATE") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_RATE_PER") & vbNullString, String)) Then
                        dblDisc_Load_Rate_Per = CType(objOLEDR_DL("TBIL_POL_DISC_RATE_PER") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, String)) Then
                        dblDisc_Load_SA_LC = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, Double)
                    End If
                    dblDisc_Load_SA_FC = dblDisc_Load_SA_LC


                    Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_TYPE") & vbNullString, String)))
                        Case "D"    ' DISCOUNT
                            Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_APPLIED_ON") & vbNullString, String)))
                                Case "S"    ' ON SUM ASSURED
                                    dblTmp_Amt = 0
                                    If Val(dblDisc_Load_SA_LC) <> 0 And Val(dblDisc_Load_Prem_Rate) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblDisc_Load_SA_LC * dblDisc_Load_Prem_Rate / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Disc_Prem_LC = dblTotal_Disc_Prem_LC + dblTmp_Amt
                                Case "P"    'compute discount base on basic premium
                                    dblTmp_Amt = 0
                                    If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblDisc_Load_Prem_Rate) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblDisc_Load_Prem_Rate / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Disc_Prem_LC = dblTotal_Disc_Prem_LC + dblTmp_Amt

                                Case "T"    'compute discount premium base on total premium
                                    dblTmp_Amt = 0
                                    If Val(dblTotal_Prem_LC) <> 0 And Val(dblDisc_Load_Prem_Rate) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblTotal_Prem_LC * dblDisc_Load_Prem_Rate / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Disc_Prem_LC = dblTotal_Disc_Prem_LC + dblTmp_Amt
                            End Select

                        Case "L"    ' LOADING
                            Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_APPLIED_ON") & vbNullString, String)))
                                Case "S"    ' ON SUM ASSURED
                                    dblTmp_Amt = 0
                                    If Val(dblDisc_Load_SA_LC) <> 0 And Val(dblDisc_Load_Prem_Rate) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblDisc_Load_SA_LC * dblDisc_Load_Prem_Rate / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Load_Prem_LC = dblTotal_Load_Prem_LC + dblTmp_Amt
                                Case "P"    'compute discount base on basic premium
                                    dblTmp_Amt = 0
                                    If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblDisc_Load_Prem_Rate) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblDisc_Load_Prem_Rate / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Load_Prem_LC = dblTotal_Load_Prem_LC + dblTmp_Amt

                                Case "T"    'compute discount premium base on total premium
                                    dblTmp_Amt = 0
                                    If Val(dblTotal_Prem_LC) <> 0 And Val(dblDisc_Load_Prem_Rate) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblTotal_Prem_LC * dblDisc_Load_Prem_Rate / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Load_Prem_LC = dblTotal_Load_Prem_LC + dblTmp_Amt
                            End Select

                    End Select

                Case "F"    ' USE Fixed Rate
                    dblDisc_Load_Rate_Per = 0
                    dblDisc_Load_SA_LC = 0
                    dblDisc_Load_SA_FC = 0

                    dblTmp_Amt = 0

                    If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PCENT") & vbNullString, String)) Then
                        dblDisc_Load_Percent = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PCENT") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_RATE_PER") & vbNullString, String)) Then
                        dblDisc_Load_Rate_Per = CType(objOLEDR_DL("TBIL_POL_DISC_RATE_PER") & vbNullString, Double)
                    End If
                    If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, String)) Then
                        dblDisc_Load_SA_LC = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, Double)
                    End If
                    dblDisc_Load_SA_FC = dblDisc_Load_SA_LC


                    Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_TYPE") & vbNullString, String)))
                        Case "D"    ' DISCOUNT
                            Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_APPLIED_ON") & vbNullString, String)))
                                Case "S"    ' ON SUM ASSURED
                                    dblTmp_Amt = 0
                                    If Val(dblDisc_Load_SA_LC) <> 0 And Val(dblDisc_Load_Percent) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblDisc_Load_SA_LC * dblDisc_Load_Percent / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Disc_Prem_LC = dblTotal_Disc_Prem_LC + dblTmp_Amt
                                Case "P"    'compute discount base on basic premium
                                    dblTmp_Amt = 0
                                    If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblDisc_Load_Percent) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblDisc_Load_Percent / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Disc_Prem_LC = dblTotal_Disc_Prem_LC + dblTmp_Amt

                                Case "T"    'compute discount premium base on total premium
                                    dblTmp_Amt = 0
                                    If Val(dblTotal_Prem_LC) <> 0 And Val(dblDisc_Load_Percent) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblTotal_Prem_LC * dblDisc_Load_Percent / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Disc_Prem_LC = dblTotal_Disc_Prem_LC + dblTmp_Amt
                            End Select

                        Case "L"    ' LOADING
                            Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_APPLIED_ON") & vbNullString, String)))
                                Case "S"    ' ON SUM ASSURED
                                    dblTmp_Amt = 0
                                    If Val(dblDisc_Load_SA_LC) <> 0 And Val(dblDisc_Load_Percent) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblDisc_Load_SA_LC * dblDisc_Load_Percent / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Load_Prem_LC = dblTotal_Load_Prem_LC + dblTmp_Amt
                                Case "P"    'compute discount base on basic premium
                                    dblTmp_Amt = 0
                                    If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblDisc_Load_Percent) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblDisc_Load_Percent / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Load_Prem_LC = dblTotal_Load_Prem_LC + dblTmp_Amt

                                Case "T"    'compute discount premium base on total premium
                                    dblTmp_Amt = 0
                                    If Val(dblTotal_Prem_LC) <> 0 And Val(dblDisc_Load_Percent) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
                                        dblTmp_Amt = dblTotal_Prem_LC * dblDisc_Load_Percent / dblDisc_Load_Rate_Per
                                    End If
                                    dblTotal_Load_Prem_LC = dblTotal_Load_Prem_LC + dblTmp_Amt
                            End Select

                    End Select

            End Select

            dblDisc_Load_Percent = 0
            dblTmp_Amt = 0

            'Calculation for fixed rate was not specified in the existing code. Duplicated table rate calculation for fixed rate 
            'and made changes where neccessary.
            ' --- Azeez comment start here
            'If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PCENT") & vbNullString, String)) Then
            '    dblDisc_Load_Percent = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PCENT") & vbNullString, Double)
            'End If

            'Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_TYPE") & vbNullString, String)))
            '    Case "D"    ' DISCOUNT
            '        dblTmp_Amt = 0
            '        If Val(dblTotal_Disc_Prem_LC) <> 0 And Val(dblDisc_Load_Percent) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
            '            dblTmp_Amt = dblTotal_Disc_Prem_LC * dblDisc_Load_Percent / dblDisc_Load_Rate_Per
            '        End If
            '        dblTotal_Disc_Amt_LC = dblTotal_Disc_Amt_LC + dblTmp_Amt

            '    Case "L"    ' LOADING
            '        dblTmp_Amt = 0
            '        If Val(dblTotal_Load_Prem_LC) <> 0 And Val(dblDisc_Load_Percent) <> 0 And Val(dblDisc_Load_Rate_Per) <> 0 Then
            '            dblTmp_Amt = dblTotal_Load_Prem_LC * dblDisc_Load_Percent / dblDisc_Load_Rate_Per
            '        End If
            '        dblTotal_Load_Amt_LC = dblTotal_Load_Amt_LC + dblTmp_Amt
            'End Select
            ' ---Azeez comment ends here
        Loop

        objOLECmd_DL = Nothing
        objOLEDR_DL = Nothing
        'Initially this but Azeez reverse this based on how it was calculated above
        'dblTotal_Load_Prem_LC = dblTotal_Load_Amt_LC
        'dblTotal_Disc_Prem_LC = dblTotal_Disc_Amt_LC

        dblTotal_Load_Amt_LC = dblTotal_Load_Prem_LC
        dblTotal_Disc_Amt_LC = dblTotal_Disc_Prem_LC

        Me.txtCalc_Prem_Loading_LC.Text = dblTotal_Load_Prem_LC.ToString()
        dblTotal_Load_Prem_FC = dblTotal_Load_Prem_LC
        Me.txtCalc_Prem_Loading_FC.Text = dblTotal_Load_Prem_FC.ToString()

        Me.txtCalc_Prem_Disc_LC.Text = dblTotal_Disc_Prem_LC.ToString()
        dblTotal_Disc_Prem_FC = dblTotal_Disc_Prem_LC
        Me.txtCalc_Prem_Disc_FC.Text = dblTotal_Disc_Prem_FC.ToString()


        '====================================
        '(j)	Calculate Charges
        '====================================

        'Read all records on the policy charge details (TBIL_POLICY_CHG_DTLS)
        ' for the policy
        '	If TBIL_POL_CHG_APPLD_ON =  ‘A’    (Direct Amount)
        '	Then Add TBIL_POL_CHG_AMT_LC to Total charges Amount.
        '(j1)	If TBIL_POL_CHG_APPLD_ON = ‘P’ (Basic Prem)
        '	Then charge amount =
        '	TBIL_POL_PRM_DTL_BASIC_PRM_LC multiplied by 
        '	TBIL_POL_CHG_RATE Divided by 100
        '	Add charge Amount to Total charge Amount.
        '(j2)	If TBIL_POL_CHG_APPLD_ON = ‘T’ (Total Prem) then charge Amount =
        '	TBIL_POL_PRM_DTL_TOT_PRM_LC multiplied by 
        '	TBIL_POL_POL_CHG_RATE Divided By 100.
        '	Add charge Amount to Total charge Amount
        '(j3)	Move Total Charge Amount to TBIL_POL_PRM_DTL_ANN_CHG_LC
        '                        TBIL_POL_PRM_DTL_ANN_CHG_FC()
        '	Display on screen

        strTable = strTableName
        strTable = "TBIL_POLICY_CHG_DTLS"


        strSQL = ""
        strSQL = strSQL & "SELECT CHG_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS CHG_TBL"
        strSQL = strSQL & " WHERE CHG_TBL.TBIL_POLY_CHG_FILE_NO = '" & RTrim(strREC_ID) & "'"
        'If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
        'strSQL = strSQL & " AND CHG_TBL.TBIL_POLY_CHG_REC_ID = '" & Val(FVstrRecNo) & "'"
        'End If
        strSQL = strSQL & " AND CHG_TBL.TBIL_POLY_CHG_PROP_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND CHG_TBL.TBIL_POLY_CHG_POLY_NO = '" & RTrim(strP_ID) & "'"

        'strSQL = "SPIL_GET_POLICY_CHG_DTLS"

        Dim objOLECmd_CHG As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        'objOLECmd_CHG.CommandTimeout = 180
        objOLECmd_CHG.CommandType = CommandType.Text
        'objOLECmd_CHG.CommandType = CommandType.StoredProcedure
        'objOLECmd_CHG.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd_CHG.Parameters.Add("p01", OleDbType.VarChar, 30).Value = strREC_ID
        'objOLECmd_CHG.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR_CHG As OleDbDataReader

        objOLEDR_CHG = objOLECmd_CHG.ExecuteReader()

        Do While objOLEDR_CHG.Read()

            'TBIL_POLY_CHG_CD
            'TBIL_POLY_CHG_APPLD_ON
            'TBIL_POLY_CHG_RATE
            'TBIL_POLY_CHG_APPLD_PERIOD
            'TBIL_POLY_CHG_AMT_LC

            dblTmp_Amt = 0
            dblCharges_Amt = 0

            dblCharges_Rate = 0
            dblCharges_Rate_Per = 100

            If IsNumeric(CType(objOLEDR_CHG("TBIL_POLY_CHG_RATE") & vbNullString, String)) Then
                dblCharges_Rate = CType(objOLEDR_CHG("TBIL_POLY_CHG_RATE") & vbNullString, Double)
            End If
            If IsNumeric(CType(objOLEDR_CHG("TBIL_POLY_CHG_RATE_PER") & vbNullString, String)) Then
                dblCharges_Rate_Per = CType(objOLEDR_CHG("TBIL_POLY_CHG_RATE_PER") & vbNullString, Double)
            End If

            Select Case UCase(Trim(CType(objOLEDR_CHG("TBIL_POLY_CHG_APPLD_ON") & vbNullString, String)))
                Case "A"    'USE FIXED AMOUNT
                    dblTmp_Amt = 0
                    dblCharges_Amt = 0
                    If IsNumeric(CType(objOLEDR_CHG("TBIL_POLY_CHG_AMT_LC") & vbNullString, String)) Then
                        dblCharges_Amt = CType(objOLEDR_CHG("TBIL_POLY_CHG_AMT_LC") & vbNullString, Double)
                    End If
                    dblTmp_Amt = dblCharges_Amt
                    dblTotal_Charges_LC = dblTotal_Charges_LC + dblTmp_Amt

                Case "P"    'compute premium base on basic premium
                    dblTmp_Amt = 0
                    If Val(dblAnnual_Basic_Prem_LC) <> 0 And Val(dblCharges_Rate) <> 0 And Val(dblCharges_Rate_Per) <> 0 Then
                        dblTmp_Amt = dblAnnual_Basic_Prem_LC * dblCharges_Rate / dblCharges_Rate_Per
                    End If
                    dblTotal_Charges_LC = dblTotal_Charges_LC + dblTmp_Amt

                Case "T"    'compute premium base on total premium
                    dblTmp_Amt = 0
                    If Val(dblTotal_Prem_LC) <> 0 And Val(dblCharges_Rate) <> 0 And Val(dblCharges_Rate_Per) <> 0 Then
                        dblTmp_Amt = dblTotal_Prem_LC * dblCharges_Rate / dblCharges_Rate_Per
                    End If
                    dblTotal_Charges_LC = dblTotal_Charges_LC + dblTmp_Amt
            End Select

        Loop

        objOLEDR_CHG = Nothing
        objOLECmd_CHG = Nothing

        Me.txtCalc_Add_Charges_LC.Text = dblTotal_Charges_LC.ToString()

        dblTotal_Charges_FC = dblTotal_Charges_LC
        Me.txtCalc_Add_Charges_FC.Text = dblTotal_Charges_FC.ToString()



START_MOP_RTN:

        Select Case Trim(Me.txtProduct_Num.Text)
            Case "F001", "F002"
                'F001 - FUNERAL POLICY - DIGNITY PLAN
                'F002 - FUNERAL  MULTI INSURED
                GoTo END_MOP_RTN

        End Select
        '************************************************************
        ' START MOP COMPUTATION
        '************************************************************


        '====================================
        '(k)	Determine Mode of Payment Values
        '====================================

        'Use the mode of payment code extracted in (d) above.
        '(TBIL_POL_PRM_DTL_MOP_FACTOR)

        'Use the System Module Code and the Mode of payment Code to access the mode of Payment factor Table (TBIL_MOP_FACTOR)

        'Pick following information.

        'Factor Rate           TBIL_MOP_RATE
        'MOP Divider         TBIL_MOP_DIVIDE
        'Policy fee       	TBIL_MOP_POL_FEE

        '(k1)	Calculate MOP contribution as TBIL_POL_PRM_DTL_ANN_CONTRIB_LC multiplied by TBIL_MOP_RATE divided by TBIL_MOP_DIVIDE.
        '	Move MOP Contribution to TBIL_POL_PRM_DTL_MOP_CONTRB_LC
        '                            TBIL_POL_PRM_DTL_MOP_CONTRB_FC
        '	Display on screen

        '(k2)	If TBIL_POL_PRM_LIFE_COVER = ‘y’
        'Calculate MOP Premium   as TBIL_POL_PRM_DTL_TOT_PRM_LC multiplied by TBIL_MOP_RATE divided by TBIL_MOP_DIVIDE
        'Add TBIL_MOP_POLY_FEE To MOP Premium
        'Move MOP Premium to TBIL_POL_PRM_DTL_MOP_PRM_LC
        '                    TBIL_POL_PRM_DTL_MOP_PRM_FC
        '	Display on screen


        '   Mode of Payment Table
        '   ---------------------
        '   Table Name:
        '       TBIL_MOP_FACTOR
        '   Field Name
        '       TBIL_MOP_TYPE_CD
        '       TBIL_MOP_TYPE_REC_ID
        '       TBIL_MOP_TYPE_DESC
        '       TBIL_MOP_RATE
        '       TBIL_MOP_DIVIDE

        dblMOP_Rate = 0
        dblMOP_Per = 100
        dblMOP_Fee = 0

        dblTmp_Amt = 0

        dblMOP_Contrib_LC = 0
        dblMOP_Contrib_FC = 0
        dblMOP_Prem_LC = 0
        dblMOP_Prem_FC = 0

        myRetValue = "0"
        myRetValue = MOD_GEN.gnGET_RATE("GET_IL_MOP_FACTOR", "IND", Me.txtCalc_MOP_Factor_LC.Text, "", "", "", Me.lblMsg, Nothing, Me.txtPrem_MOP_Per)
        If Left(LTrim(myRetValue), 3) = "ERR" Then
            dblMOP_Rate = 0
        Else
            dblMOP_Rate = Val(myRetValue)
            dblMOP_Per = Val(Me.txtPrem_MOP_Per.Text)
        End If

        'Response.Write("<br/>Annual Contribution: " & dblAnnual_Contrib_LC.ToString)
        'Response.Write("<br/>Applied On: " & Me.txtPrem_Rate_Applied_On.Text)
        'Response.Write("<br/>MOP Type: " & Me.txtCalc_MOP_Factor_LC.Text)
        'Response.Write("<br/>MOP Factor: " & dblMOP_Rate.ToString)

        Select Case Trim(Me.txtPrem_Rate_Applied_On.Text)
            Case "P"
                If Val(dblAnnual_Contrib_LC) > 0 And Val(dblMOP_Per) <> 0 Then
                    dblMOP_Contrib_LC = dblAnnual_Contrib_LC / dblMOP_Per
                End If
                Me.txtCalc_MOP_Contrib_LC.Text = dblMOP_Contrib_LC.ToString

                dblMOP_Contrib_FC = dblMOP_Contrib_LC
                Me.txtCalc_MOP_Contrib_FC.Text = dblMOP_Contrib_FC.ToString

                dblMOP_Prem_LC = dblMOP_Contrib_LC
                Me.txtCalc_MOP_Prem_LC.Text = dblMOP_Prem_LC.ToString

                dblMOP_Prem_FC = dblMOP_Prem_LC
                Me.txtCalc_MOP_Prem_FC.Text = dblMOP_Prem_FC.ToString

            Case Else

                '   compute MOP contribution
                '   ----------------------------------------


                If Val(dblAnnual_Contrib_LC) <> 0 And Val(dblMOP_Rate) <> 0 And Val(dblMOP_Per) <> 0 Then
                    dblTmp_Amt = dblAnnual_Contrib_LC / dblMOP_Per
                End If
                'Response.Write("<br/>MOP Contribution: " & dblTmp_Amt.ToString)

                dblMOP_Contrib_LC = dblTmp_Amt
                Me.txtCalc_MOP_Contrib_LC.Text = dblMOP_Contrib_LC.ToString

                dblMOP_Contrib_FC = dblMOP_Contrib_LC
                Me.txtCalc_MOP_Contrib_FC.Text = dblMOP_Contrib_FC.ToString


                '   computr MOP premium
                '   ----------------------------------------

                dblMOP_Prem_LC = 0
                dblTmp_Amt = 0
                Select Case UCase(Trim(strHas_Life_Cover))
                    Case "Y"
                        If Val(dblTotal_Prem_LC) <> 0 And Val(dblMOP_Rate) <> 0 And Val(dblMOP_Per) <> 0 Then
                            dblTmp_Amt = dblTotal_Prem_LC * dblMOP_Rate / dblMOP_Per
                        End If
                        dblMOP_Prem_LC = dblTmp_Amt
                End Select

                If Val(dblMOP_Prem_LC) > Val(dblMOP_Contrib_LC) Then
                    dblMOP_Contrib_LC = dblMOP_Prem_LC
                    Me.txtCalc_MOP_Contrib_LC.Text = dblMOP_Contrib_LC.ToString
                    dblMOP_Contrib_FC = dblMOP_Contrib_LC
                    Me.txtCalc_MOP_Contrib_FC.Text = dblMOP_Contrib_FC.ToString
                End If

                dblTmp_Amt = dblMOP_Prem_LC + dblMOP_Fee
                dblMOP_Prem_LC = dblTmp_Amt
                Me.txtCalc_MOP_Prem_LC.Text = dblMOP_Prem_LC.ToString

                dblMOP_Prem_FC = dblMOP_Prem_LC
                Me.txtCalc_MOP_Prem_FC.Text = dblMOP_Prem_FC.ToString

        End Select


        '************************************************************
        ' END MOP COMPUTATION
        '************************************************************

        '   compute annual premium for both LC and FC
        '   ----------------------------------------

        '(l)	TBIL_POL_PRM_DTL_ANN_PREM_LC = 
        '            TBIL_POL_PRM_DTL_TOT_PRM_LC + TBIL_POL_PRM_DTL_ANN_CHG_LC
        '          + TBIL_POL_PRM_DTL_LOADING_LC – TBIL_POL_PRM_DTL_DISCNT_LC

        'Move to FC
        'TBIL_POL_PRM_DTL_ANN_PREM_FC = 
        'TBIL_POL_PRM_DT_TOT_PRM_FC + TBIL_POL_PRM_DTL_ANN_CHG_FC +
        'TBIL_POL_PRM_DTL_LOADING_FC – TBIL_POL_PRM_DTL_DISCNT_FC


END_MOP_RTN:


        dblTmp_Amt = (dblTotal_Prem_LC + dblTotal_Charges_LC + dblTotal_Load_Prem_LC) - dblTotal_Disc_Prem_LC
        dblAnnual_Prem_LC = dblTmp_Amt
        Me.txtCalc_Ann_Prem_LC.Text = dblAnnual_Prem_LC.ToString

        dblAnnual_Prem_FC = dblTmp_Amt
        Me.txtCalc_Ann_Prem_FC.Text = dblAnnual_Prem_FC.ToString


        '   compute first premium for both LC and FC
        '   ----------------------------------------

        'TBIL_POL_PRM_DTL_FIRST_PRM_LC   =
        'TBIL_POL_PRM_DTL_MOP_LC +
        'TBIL_POL_PRM_DTL_ANN_CHG_LC  +
        'TBIL_POL_PRM_DTL_LOADING_LC  - TBIL_POL_PRM_DTL_DISCNT_LC

        dblTmp_Amt = (dblMOP_Prem_LC + dblTotal_Charges_LC + dblTotal_Load_Prem_LC) - dblTotal_Disc_Prem_LC

        dblFirst_Prem_LC = dblTmp_Amt
        Me.txtCalc_First_Prem_LC.Text = dblFirst_Prem_LC.ToString

        dblFirst_Prem_FC = dblTmp_Amt
        Me.txtCalc_First_Prem_FC.Text = dblFirst_Prem_FC.ToString


        ' ================================================
        '(a)	 Calculate Net Premium
        'Calculate Net Premiun as:
        'TBIL_POL_PRM_DTL_NET_PRM_LC =
        '(TBIL_POL_PRM_DTL_BASIC_ANN_PRM_LC +  TBIL_POL_PRM_DTL_ADDPREM_LC  + TBIL_POL_PRM_DTL_LOADING_LC) -  TBIL_POL_PRM_DTL_DISCNT_LC -  TBIL_POL_PRM_DTL_CHG_LC

        'Move TBIL_POL_PRM_DTL_NET_PRM_LC to TBIL_POL_PRM_DTL_NET_PRM_FC

        dblNet_Prem_LC = (dblAnnual_Basic_Prem_LC + dblTotal_Add_Prem_LC + dblTotal_Load_Prem_LC) - (dblTotal_Disc_Prem_LC + dblTotal_Charges_LC)
        dblNet_Prem_FC = dblNet_Prem_LC
        Me.txtCalc_Net_Prem_LC.Text = dblNet_Prem_LC.ToString
        Me.txtCalc_Net_Prem_FC.Text = dblNet_Prem_FC.ToString


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


    End Sub


    Private Sub Proc_DoCalc_AddPrem(ByVal pv_objOLEConn As OleDbConnection, Optional ByVal pvCover_Flag As String = "")

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


        Dim dblItem_SA As Double
        Dim dblMy_Amt As Double

        dblItem_SA = 0
        dblMy_Amt = 0

        strDoc_Item_Flag = "N"

        dblAdd_Prem_Amt = 0
        dblTotal_Add_Prem_LC = 0

        strCover_Num = ""
        strProduct_Num = ""

        strAFAB_SW = "N"
        strAFAB_Num = ""
        dblAFAB_Value = 0


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

            '   TBIL_POL_ADD_PRDCT_CD
            '   TBIL_POL_ADD_COVER_CD
            strProduct_Num = RTrim(CType(objOLEDR("TBIL_POL_ADD_PRDCT_CD") & vbNullString, String))
            strCover_Num = RTrim(CType(objOLEDR("TBIL_POL_ADD_COVER_CD") & vbNullString, String))

            dblItem_SA = 0

            If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, String)) Then
                dblItem_SA = CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, Double)
            End If


            Select Case UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_RT_AMT_CD") & vbNullString, String)))
                Case "A"
                    dblAdd_Prem_Amt = 0
                    dblTmp_Amt = 0
                    If IsNumeric(CType(objOLEDR("TBIL_POL_ADD_PREM_AMT") & vbNullString, String)) Then
                        dblAdd_Prem_Amt = CType(objOLEDR("TBIL_POL_ADD_PREM_AMT") & vbNullString, Double)
                    End If
                    dblTmp_Amt = dblAdd_Prem_Amt
                    dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                    dblMy_Amt = dblAdd_Prem_Amt

                Case "F"
                    dblAdd_Prem_Amt = 0
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

                            'Check for LIFE TERM HARVEST and AFAB
                            If UCase(Trim(Me.txtProduct_Num.Text)) = "E003" And UCase(Trim(strCover_Num)) = "E003-4" Then
                                intAFAB_Rec_ID = CInt(CType(objOLEDR("TBIL_POL_ADD_REC_ID") & vbNullString, String))
                                strAFAB_SW = "Y"
                                strAFAB_Num = RTrim(strCover_Num)
                                If Val(dblSum_Assured_LC) >= 500000 Then
                                    dblAFAB_Value = 500000
                                Else
                                    dblAFAB_Value = dblSum_Assured_LC
                                End If
                                dblItem_SA = dblAFAB_Value
                            End If


                        Case "S"    'compute additional premium base on sum assured
                            If Val(dblAdd_Prem_SA_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAdd_Prem_SA_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                    End Select

                    dblMy_Amt = dblTmp_Amt

                Case "R"    'compute additional premium using the rate table
                    dblAdd_Prem_Amt = 0
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

                            'Check for LIFE TERM HARVEST and AFAB
                            If UCase(Trim(Me.txtProduct_Num.Text)) = "E003" And UCase(Trim(strCover_Num)) = "E003-4" Then
                                intAFAB_Rec_ID = CInt(CType(objOLEDR("TBIL_POL_ADD_REC_ID") & vbNullString, String))
                                strAFAB_SW = "Y"
                                strAFAB_Num = RTrim(strCover_Num)
                                If Val(dblSum_Assured_LC) >= 500000 Then
                                    dblAFAB_Value = 500000
                                Else
                                    dblAFAB_Value = dblSum_Assured_LC
                                End If
                                dblItem_SA = dblAFAB_Value
                            End If

                        Case "S"    'compute additional premium base on sum assured
                            If Val(dblAdd_Prem_SA_LC) <> 0 And Val(dblAdd_Prem_Rate) <> 0 And Val(dblAdd_Rate_Per) <> 0 Then
                                dblTmp_Amt = dblAdd_Prem_SA_LC * dblAdd_Prem_Rate / dblAdd_Rate_Per
                            End If
                            dblTotal_Add_Prem_LC = dblTotal_Add_Prem_LC + dblTmp_Amt

                    End Select

                    dblMy_Amt = dblTmp_Amt


            End Select


            'Response.Write("<br/>Rate Type: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_RT_AMT_CD") & vbNullString, String))))
            'Response.Write("<br/>Additional Amount: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_AMT") & vbNullString, String))))
            'Response.Write("<br/>Fixed Rate: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_RATE_APPLY") & vbNullString, String))))
            'Response.Write("<br/>Fixed Rate: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT") & vbNullString, String))))
            'Response.Write("<br/>fixed Rate Per: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT_PER") & vbNullString, String))))
            'Response.Write("<br/>Table Rate: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_RATE") & vbNullString, String))))
            'Response.Write("<br/>Table Rate Per: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_RATE_PER") & vbNullString, String))))
            'Response.Write("<br/>Sum Assured: " & UCase(Trim(CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, String))))

            If UCase(RTrim(pvCover_Flag)) = "ADD_COV" Then
                'Call Proc_DoSave_DocItem("ADD_COV", strCover_Num, dblTmp_Amt, pv_objOLEConn)
                Call Proc_DoSave_DocItem("ADD_COV", strCover_Num, dblItem_SA, dblMy_Amt, pv_objOLEConn)
            End If

        Loop

        objOLECmd = Nothing
        objOLEDR = Nothing


        If UCase(RTrim(pvCover_Flag)) = "ADD_COV" And UCase(Trim(strDoc_Item_Flag)) <> "Y" Then
            strCover_Num = RTrim(Me.txtCover_Num.Text)
            strCover_Num = ""
            Call Proc_DoSave_DocItem("ADD_COV", strCover_Num, 0, 0, pv_objOLEConn)
        End If



        Me.txtCalc_Add_Prem_LC.Text = dblTotal_Add_Prem_LC.ToString()
        dblTotal_Add_Prem_FC = dblTotal_Add_Prem_LC
        Me.txtCalc_Add_Prem_FC.Text = dblTotal_Add_Prem_FC.ToString()


        '***********************************************************************************************
        ' END ADDITIONAL COVERS CODES 
        '***********************************************************************************************

    End Sub

    Private Sub Proc_DoSave_DocItem(ByVal pvRecType As String, ByVal pvCoverNum As String, ByVal pvTransAmt As Double, ByVal pvPremAmt As Double, ByVal pv_objOLEConn As OleDbConnection)

        strDoc_Item_Flag = "Y"

        Dim int_myrc As Integer = 0

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try

        ' CREATE DOCUMENT ITEMS RECORD
        strDoc_Item_SQL = ""
        strDoc_Item_SQL = "INSERT INTO TBIL_POLICY_DOC_ITEMS (TBIL_POL_ITEM_MDLE"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,TBIL_POL_ITEM_FILE_NO, TBIL_POL_ITEM_PROP_NO"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,TBIL_POL_ITEM_REC_TYPE, TBIL_POL_ITEM_POLY_NO, TBIL_POL_ITEM_PRDCT_CD"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,TBIL_POL_ITEM_COVER_CD, TBIL_POL_ITEM_PLAN_CD"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,TBIL_POL_ITEM_AMT, TBIL_POL_ITEM_PREM"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,TBIL_POL_ITEM_FLAG, TBIL_POL_ITEM_OPERID, TBIL_POL_ITEM_KEYDTE"
        strDoc_Item_SQL = strDoc_Item_SQL & " )"
        strDoc_Item_SQL = strDoc_Item_SQL & " VALUES(" & RTrim("'I'")
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim(Me.txtFileNum.Text) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim(Me.txtQuote_Num.Text) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim(pvRecType) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim(Me.txtPolNum.Text) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim(Me.txtProduct_Num.Text) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim(pvCoverNum) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim(Me.txtPlan_Num.Text) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & Val(pvTransAmt) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & Val(pvPremAmt) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim("A") & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & RTrim(myUserIDX) & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " ,'" & Format(Now, "MM/dd/yyyy") & "'"
        strDoc_Item_SQL = strDoc_Item_SQL & " )"

        Dim objDoc_Item As OleDbCommand = Nothing
        objDoc_Item = New OleDbCommand(strDoc_Item_SQL, pv_objOLEConn)
        int_myrc = objDoc_Item.ExecuteNonQuery()

    End Sub

    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Dim pvURL As String = "prg_li_indv_poly_load_disc.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        '
        Dim pvURL As String = "prg_li_indv_poly_prem_calc.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        'Response.Redirect(pvURL)

    End Sub


    '    Private Sub Proc_DoCalc_Loading(ByVal pv_objOLEConn As OleDbConnection)
    '        Dim dblDisc_Load_Prem_Amt As Double = 0
    '        Dim dblDisc_Load_Prem_Rate As Double = 0
    '        Dim dblDisc_Load_Rate_Per As Integer = 0
    '        Dim dblDisc_Load_SA_LC As Double = 0
    '        Dim dblDisc_Load_SA_FC As Double = 0
    '        Dim dblDisc_Load_Percent As Double = 0
    '        Dim dblTotal_Load_Amt_LC As Double = 0
    '        Dim dblTotal_Load_Prem_LC As Double = 0
    '        Dim dblTotal_Load_Prem_FC As Double = 0

    '        Dim my_Rate_Type As String = ""
    '        Dim my_Applied_On As String = ""
    '        Dim my_Load_Amt As Double = 0

    '        dblTotal_Load_Amt_LC = 0
    '        'dblTotal_Disc_Amt_LC = 0

    '        strTable = strTableName
    '        strTable = "TBIL_POLICY_DISCT_LOAD"

    '        strSQL = ""
    '        strSQL = strSQL & "SELECT DL_TBL.*"
    '        strSQL = strSQL & " FROM " & strTable & " AS DL_TBL"
    '        strSQL = strSQL & " WHERE DL_TBL.TBIL_POL_DISC_FILE_NO = '" & RTrim(strREC_ID) & "'"
    '        strSQL = strSQL & " AND DL_TBL.TBIL_POL_DISC_PROP_NO = '" & RTrim(strQ_ID) & "'"
    '        strSQL = strSQL & " AND DL_TBL.TBIL_POL_DISC_LOAD_TYPE IN('L')"

    '        Dim objOLECmd_DL As OleDbCommand = New OleDbCommand(strSQL, pv_objOLEConn)
    '        objOLECmd_DL.CommandType = CommandType.Text
    '        Dim objOLEDR_DL As OleDbDataReader

    '        objOLEDR_DL = objOLECmd_DL.ExecuteReader()

    '        Do While objOLEDR_DL.Read()
    '            dblDisc_Load_Prem_Amt = 0

    '            dblDisc_Load_Prem_Rate = 0
    '            dblDisc_Load_Rate_Per = 0
    '            dblDisc_Load_SA_LC = 0
    '            dblDisc_Load_SA_FC = 0

    '            dblTmp_Amt = 0
    '            dblTmp_AmtX = 0

    '            dblDisc_Load_Percent = 0

    '            my_Load_Amt = 0

    '            strCover_Num = CType(objOLEDR_DL("TBIL_POL_DISC_COVER_CD") & vbNullString, String)
    '            my_Rate_Type = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_RT_AMT_CD") & vbNullString, String)
    '            my_Applied_On = CType(objOLEDR_DL("TBIL_POL_DISC_APPLIED_ON") & vbNullString, String)


    '            If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PCENT") & vbNullString, String)) Then
    '                dblDisc_Load_Percent = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_PCENT") & vbNullString, Double)
    '            End If

    '            If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_RATE") & vbNullString, String)) Then
    '                dblDisc_Load_Prem_Rate = CType(objOLEDR_DL("TBIL_POL_DISC_RATE") & vbNullString, Double)
    '            End If
    '            If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_RATE_PER") & vbNullString, String)) Then
    '                dblDisc_Load_Rate_Per = CType(objOLEDR_DL("TBIL_POL_DISC_RATE_PER") & vbNullString, Double)
    '            End If

    '            If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, String)) Then
    '                my_Load_Amt = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, Double)
    '            End If


    '            Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_RT_AMT_CD") & vbNullString, String)))
    '                Case "A"    'USE FIXED AMOUNT
    '                    If IsNumeric(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, String)) Then
    '                        dblTmp_Amt = CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, Double)
    '                    End If
    '                    dblTmp_Amt = 0
    '                    dblTmp_AmtX = 0
    '                    dblTmp_Amt = Proc_DoLoading(pv_objOLEConn, my_Rate_Type, my_Applied_On, my_Load_Amt, dblDisc_Load_Prem_Rate, dblDisc_Load_Rate_Per, dblDisc_Load_Percent)
    '                    'dblTmp_AmtX = Proc_DoLoading_Add_Member(pv_objOLEConn, my_Rate_Type, my_Applied_On, my_Load_Amt, dblDisc_Load_Prem_Rate, dblDisc_Load_Rate_Per, dblDisc_Load_Percent, strCover_Num)
    '                    dblTotal_Load_Amt_LC = dblTotal_Load_Amt_LC + dblTmp_Amt
    '            End Select

    '                Case "F"    'USE FIXED RATE
    '            Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_TYPE") & vbNullString, String)))
    '                Case "D"    ' DISCOUNT
    '                Case "L"    ' LOADING
    '                    dblTmp_Amt = 0
    '                    dblTmp_AmtX = 0
    '                    dblTmp_Amt = Proc_DoLoading(pv_objOLEConn, my_Rate_Type, my_Applied_On, my_Load_Amt, dblDisc_Load_Prem_Rate, dblDisc_Load_Rate_Per, dblDisc_Load_Percent)
    '                    'dblTmp_AmtX = Proc_DoLoading_Add_Member(pv_objOLEConn, my_Rate_Type, my_Applied_On, my_Load_Amt, dblDisc_Load_Prem_Rate, dblDisc_Load_Rate_Per, dblDisc_Load_Percent, strCover_Num)
    '                    dblTotal_Load_Amt_LC = dblTotal_Load_Amt_LC + (dblTmp_Amt + dblTmp_AmtX)
    '            End Select

    '                Case "R"    'USE RATE RATE
    '            Select Case UCase(Trim(CType(objOLEDR_DL("TBIL_POL_DISC_LOAD_TYPE") & vbNullString, String)))
    '                Case "D"    ' DISCOUNT
    '                Case "L"    ' LOADING
    '                    dblTmp_Amt = 0
    '                    dblTmp_AmtX = 0
    '                    dblTmp_Amt = Proc_DoLoading(pv_objOLEConn, my_Rate_Type, my_Applied_On, my_Load_Amt, dblDisc_Load_Prem_Rate, dblDisc_Load_Rate_Per, dblDisc_Load_Percent)
    '                    'dblTmp_AmtX = Proc_DoLoading_Add_Member(pv_objOLEConn, my_Rate_Type, my_Applied_On, my_Load_Amt, dblDisc_Load_Prem_Rate, dblDisc_Load_Rate_Per, dblDisc_Load_Percent, strCover_Num)
    '                    'dblTotal_Load_Amt_LC = dblTotal_Load_Amt_LC + (dblTmp_Amt + dblTmp_AmtX)
    '                    dblTotal_Load_Amt_LC = dblTotal_Load_Amt_LC + dblTmp_Amt
    '            End Select

    '            End Select
    '        Loop

    '        objOLECmd_DL = Nothing
    '        objOLEDR_DL = Nothing

    '        dblTotal_Load_Prem_LC = dblTotal_Load_Amt_LC

    '        Me.txtCalc_Prem_Loading_LC.Text = dblTotal_Load_Prem_LC.ToString()
    '        dblTotal_Load_Prem_FC = dblTotal_Load_Prem_LC
    '        Me.txtCalc_Prem_Loading_FC.Text = dblTotal_Load_Prem_FC.ToString()

    '    End Sub

    '    Private Function Proc_DoLoading(ByVal pv_objOLEConn As OleDbConnection, ByVal pvRate_Type As String, ByVal pvApplied_On As String, ByVal pvLoad_Amt As Double, ByVal pvLoad_Rate As Double, ByVal pvLoad_Rate_Per As Integer, ByVal pvLoad_Percent As Double) As Double

    '        'strTable = strTableName
    '        'strTable = "TBIL_GRP_POLICY_MEMBERS"

    '        Dim myRID As String = ""
    '        Dim mySN As String = ""

    '        Dim myLoad_Amt As Double = 0

    '        Dim myResult As Double = 0
    '        Dim mySA_Amt As Double = 0
    '        Dim myAmt As Double = 0
    '        Dim myFixed_Amt As Double = 0
    '        Dim myPercent As Double = 0
    '        Dim myRate As Double = 0
    '        Dim myPer As Double = 0

    '        Dim myole_cmd As OleDbCommand
    '        Dim mystr As String = ""

    '        Dim myCNT_T As Integer = 0
    '        Dim myCNT As Integer = 0
    '        Dim myC As Integer = 0

    '        Dim oAL_ID As New ArrayList
    '        Dim oAL_Rec As New ArrayList

    '        myFixed_Amt = Val(pvLoad_Amt)
    '        myPercent = Val(pvLoad_Percent)
    '        myRate = Val(pvLoad_Rate)
    '        myPer = Val(pvLoad_Rate_Per)

    '        myLoad_Amt = 0
    '        myCNT_T = 0
    '        myCNT = 0
    '        Select Case UCase(pvRate_Type)
    '            Case "A"
    '                myResult = Val(myFixed_Amt)
    '            Case "F"
    '                Select Case UCase(pvApplied_On)
    '                    Case "S"    'Sum Assured
    '                        If mySA_Amt <> 0 And myPercent <> 0 And myPer <> 0 Then
    '                            myResult = mySA_Amt * myPercent / myPer
    '                        End If
    '                    Case "P"    'Basic Premium
    '                        If myAmt <> 0 And myPercent <> 0 And myPer <> 0 Then
    '                            myResult = myAmt * myPercent / myPer
    '                        End If
    '                    Case "T"    'Total Premium
    '                        ' NO CALCULATION SPECIFICATION
    '                End Select
    '            Case "R"
    '                Select Case UCase(pvApplied_On)
    '                    Case "S"    'Sum Assured
    '                        If mySA_Amt <> 0 And myRate <> 0 And myPer <> 0 Then
    '                            myResult = mySA_Amt * myRate / myPer
    '                        End If
    '                    Case "P"    'Basic Premium
    '                        If myAmt <> 0 And myRate <> 0 And myPer <> 0 Then
    '                            myResult = myAmt * myRate / myPer
    '                        End If
    '                    Case "T"    'Total Premium
    '                        ' NO CALCULATION SPECIFICATION
    '                End Select
    '        End Select



    '        myLoad_Amt = myLoad_Amt + myResult

    '        myCNT_T = myCNT_T + 1
    '        oAL_ID.Insert(myCNT_T - 1, myRID)
    '        oAL_Rec.Insert(myCNT_T - 1, myResult.ToString)
    '        If myCNT_T <= 0 Then
    '            GoTo Proc_End_Rtn
    '        End If

    '        myCNT = 0
    'Proc_End_Rtn:

    '        'myole_cmd.Dispose()
    '        myole_cmd = Nothing

    '        oAL_ID.Clear()
    '        oAL_ID = Nothing
    '        oAL_Rec.Clear()
    '        oAL_Rec = Nothing

    '        Return myLoad_Amt

    '    End Function

End Class
