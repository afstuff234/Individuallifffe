Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class I_LIFE_PRG_LI_INDV_POLY_PERSNAL
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'TRUNCATE TABLE dbo.TBIL_POLICY_ADD_PREM
        'TRUNCATE TABLE dbo.TBIL_POLICY_BENEFRY
        'TRUNCATE TABLE dbo.TBIL_POLICY_CHG_DTLS
        'TRUNCATE TABLE dbo.TBIL_POLICY_DET
        'TRUNCATE TABLE dbo.TBIL_POLICY_DISCT_LOAD
        'TRUNCATE TABLE dbo.TBIL_POLICY_ILLNESS
        'TRUNCATE TABLE dbo.TBIL_POLICY_MEDIC_EXAM
        'TRUNCATE TABLE dbo.TBIL_POLICY_PREM_DETAILS
        'TRUNCATE TABLE dbo.TBIL_POLICY_PREM_INFO


        strTableName = "TBIL_POLICY_DET"
        Me.cmdGetPol.Enabled = False

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
                Me.cmdGetPol.Enabled = True
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


        ' Load data for the DropDownList control only once, when the 
        ' page is first loaded.
        If Not (Page.IsPostBack) Then
            Call Proc_DoNew()
            Me.cmdPrev.Enabled = False
            Me.cmdNext.Enabled = False
            GetReInsValue()
            Me.txtTrans_Date.Text = Format(Now, "dd/MM/yyyy")
            If Me.txtProStatus.Text = "" Then
                Me.txtProStatus.Text = "P"
                Call gnProc_DDL_Get(Me.cboProStatus, RTrim(Me.txtProStatus.Text))
            End If

            Me.cboProduct.Items.Clear()

            Call DoProc_CreateDataSource("IL_PRODUCT_CAT_LIST", Trim("I"), Me.cboProductClass)
            'Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Trim("101"), Me.cboProduct)

            Call gnProc_Populate_Box("IL_CODE_LIST", "001", Me.cboNationality)
            Call gnProc_Populate_Box("IL_CODE_LIST", "003", Me.cboBranch)
            Call gnProc_Populate_Box("IL_CODE_LIST", "005", Me.cboDepartment)
            Call gnProc_Populate_Box("IL_CODE_LIST", "007", Me.cboOccupationClass)
            Call gnProc_Populate_Box("IL_CODE_LIST", "008", Me.cboOccupation)
            Call gnProc_Populate_Box("IL_CODE_LIST", "009", Me.cboReligion)
            Call gnProc_Populate_Box("IL_CODE_LIST", "013", Me.cboRelation)
            Call gnProc_Populate_Box("IL_CODE_LIST", "014", Me.cboBusSource)
            Call gnProc_Populate_Box("IL_CODE_LIST", "015", Me.cboGender)
            Call gnProc_Populate_Box("IL_CODE_LIST", "020", Me.cboMaritalStatus)

            If Trim(strF_ID) <> "" Then
                Me.txtFileNum.Text = RTrim(strF_ID)
                Me.cmdNext.Enabled = True
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

        txtUWYear.Text = Year(DateTime.Now)

        If Me.txtAction.Text = "New" Then
            Me.txtQuote_Num.Text = ""
            Call Proc_DoNew()
            Me.txtAction.Text = ""
            Me.lblMsg.Text = "New Entry..."
        End If

        If Me.txtAction.Text = "Save" Then
            'Call Proc_DoSave()
            Me.txtAction.Text = ""

        End If

        If Me.txtAction.Text = "Delete" Then
            'Call Proc_DoDelete()
            Me.txtAction.Text = ""
        End If

        'If Me.txtAction.Text = "Delete_Item" Then
        '    'Call Proc_DoDelItem()
        '    Me.txtAction.Text = ""
        'End If

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call Proc_DoSave()
        Me.txtAction.Text = ""

    End Sub

    Protected Sub chkFileNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileNum.CheckedChanged
        If Me.chkFileNum.Checked = True Then
            Me.lblFileNum.Enabled = True
            Me.txtFileNum.Enabled = True
            Me.cmdFileNum.Enabled = True
        Else
            Me.lblFileNum.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.cmdFileNum.Enabled = False
        End If
    End Sub

    Protected Sub cmdFileNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileNum.Click
        If LTrim(RTrim(Me.txtFileNum.Text)) <> "" Then
            strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
        End If

    End Sub

    Protected Sub cmdGetPol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetPol.Click

        If Trim(Me.txtPolNum.Text) <> "" Then
            strStatus = Proc_DoOpenRecord(RTrim("POL"), Me.txtPolNum.Text, RTrim("0"))
        End If

    End Sub

    Private Function CreateDataSource_Demo(ByVal pvLV_Text As String, ByVal pvLV_Value As String) As ICollection

        ' Create a table to store data for the DropDownList control.
        Dim dt As DataTable = New DataTable()

        ' Define the columns of the table.
        dt.Columns.Add(New DataColumn(pvLV_Text, GetType(String)))
        dt.Columns.Add(New DataColumn(pvLV_Value, GetType(String)))

        ' Populate the table with sample values.
        dt.Rows.Add(CreateRow("Select product", "0", dt))
        dt.Rows.Add(CreateRow("Investment Plus", "A001", dt))
        dt.Rows.Add(CreateRow("Personal Provident", "A002", dt))
        dt.Rows.Add(CreateRow("Capital Builder", "A003", dt))
        dt.Rows.Add(CreateRow("Dollar Linked", "A004", dt))
        dt.Rows.Add(CreateRow("Unit Link Plan", "A005", dt))
        dt.Rows.Add(CreateRow("Esusu Shield", "A006", dt))

        ' Create a DataView from the DataTable to act as the data source
        ' for the DropDownList control.
        Dim dv As DataView = New DataView(dt)
        Return dv

    End Function

    Private Function CreateRow(ByVal Text As String, ByVal Value As String, ByVal dt As DataTable) As DataRow

        ' Create a DataRow using the DataTable defined in the 
        ' CreateDataSource method.
        Dim dr As DataRow = dt.NewRow()

        ' This DataRow contains the ColorTextField and ColorValueField 
        ' fields, as defined in the CreateDataSource method. Set the 
        ' fields with the appropriate value. Remember that column 0 
        ' is defined as ColorTextField, and column 1 is defined as 
        ' ColorValueField.
        dr(0) = Text
        dr(1) = Value

        Return dr

    End Function


    '    Sub BindGrid()
    '        Dim Conn As OleDbConnection = New OleDbConnection(ConnString)
    '        Dim myDA As OleDbDataAdapter = New OleDbDataAdapter(MasterTable, Conn)

    '        Dim custDS As DataSet = New DataSet()
    '        myDA.Fill(custDS)

    '        TPlateColumn = CType(Me.DataGrid1.Columns.Item(0), TemplateColumn)
    '        Me.DataGrid1.Columns.Clear()
    '        '             'field  'caption
    '        Call NewTemplateColumn("", "Proposal No")
    '        Call NewColumn("PropDate", "Proposal Date", "{0:dd-MMM-yyyy}")
    '        Call NewColumn("Surname", "Client Name", "")
    '        Call NewColumn("SumAssured", "Sum Assured", "{0:n2}")
    '        Call NewColumn("Duration", "Term", "")
    '        Call NewColumn("PremPayable", "Premium Payable", "{0:n2}")
    '        'Call NewColumn("Status", "Status", "")

    '        Me.DataGrid1.DataSource = custDS
    '        Me.DataGrid1.DataBind()
    '    End Sub
    '    Sub BindPolicy()
    '        Dim Conn As OleDbConnection = New OleDbConnection(ConnString)
    '        Dim myDA As OleDbDataAdapter = New OleDbDataAdapter(MasterTable, Conn)

    '        Dim custDS As DataSet = New DataSet()
    '        myDA.Fill(custDS)

    '        TPlateColumn = CType(Me.DataGrid1.Columns.Item(0), TemplateColumn)
    '        Me.DataGrid1.Columns.Clear()
    '        '             'field  'caption
    '        Call NewTemplateColumn("", "Policy No")
    '        Call NewColumn("CommencementDate", "Commencement Date", "{0:dd-MMM-yyyy}")
    '        Call NewColumn("Surname", "Client Name", "")
    '        Call NewColumn("SumAssured", "Sum Assured", "{0:n2}")
    '        Call NewColumn("Duration", "Term", "")
    '        Call NewColumn("PremPayable", "Premium Payable", "{0:n2}")
    '        'Call NewColumn("Status", "Status", "")

    '        Me.DataGrid1.DataSource = custDS
    '        Me.DataGrid1.DataBind()
    '    End Sub

    '    Private Sub NewTemplateColumn(ByVal FieldName As String, ByVal HeaderCaption As String)
    '        With TPlateColumn
    '            .HeaderText = HeaderCaption
    '        End With
    '        Me.DataGrid1.Columns.Add(TPlateColumn)
    '    End Sub

    '    Private Sub NewColumn(ByVal FieldName As String, ByVal HeaderCaption As String, ByVal myFormat As String)
    '        Dim NumberColumn As New BoundColumn()

    '        With NumberColumn
    '            .HeaderText = HeaderCaption
    '            .DataField = FieldName
    '            .DataFormatString = myFormat
    '        End With
    '        Me.DataGrid1.Columns.Add(NumberColumn)
    '    End Sub




    '    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '        DataGrid1.CurrentPageIndex = e.NewPageIndex
    '        BindGrid()
    '    End Sub


    'Private Sub cmdApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdApply.Click
    '    'Me.Hidden1.Text = Val(Session("PropID"))

    '    Call PUpdate_Date()

    'End Sub


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


    Protected Sub DoProc_Agcy_Change()
        Call DoGet_SelectedItem(Me.cboAgcy_Search, Me.txtAgcyNum, Me.txtAgcyName, Me.lblMsg)
        Me.txtAgcy_Search.Text = ""
    End Sub

    Protected Sub DoProc_Agcy_Search()
        If RTrim(Me.txtAgcy_Search.Text) <> "" Then
            Call gnProc_Populate_Box("IL_MARKETERS_LIST", "001", Me.cboAgcy_Search, RTrim(Me.txtAgcy_Search.Text))
        End If

    End Sub

    Protected Sub DoProc_Assured_Change()
        Call DoGet_SelectedItem(Me.cboAssured_Search, Me.txtAssured_Num, Me.txtAssured_Name, Me.lblMsg)
        Me.txtAssured_Search.Text = ""

    End Sub

    Protected Sub DoProc_Assured_Search()
        If RTrim(Me.txtAssured_Search.Text) <> "" Then
            Call gnProc_Populate_Box("IL_ASSURED_LIST", "001", Me.cboAssured_Search, RTrim(Me.txtAssured_Search.Text))
        End If
    End Sub

    Protected Sub DoProc_Branch_Change()
        Call DoGet_SelectedItem(Me.cboBranch, Me.txtBraNum, Me.txtBraName, Me.lblMsg)

    End Sub

    Protected Sub DoProc_Branch_Refresh()
        Call gnProc_Populate_Box("IL_CODE_LIST", "003", Me.cboBranch)

    End Sub

    Protected Sub DoProc_Broker_Change()
        Call DoGet_SelectedItem(Me.cboBroker_Search, Me.txtBrokerNum, Me.txtBrokerName, Me.lblMsg)
        Me.txtBroker_Search.Text = ""

    End Sub

    Protected Sub DoProc_Broker_Search()
        If RTrim(Me.txtBroker_Search.Text) <> "" Then
            Call gnProc_Populate_Box("IL_BROKERS_LIST", "001", Me.cboBroker_Search, RTrim(Me.txtBroker_Search.Text))
        End If

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


    Protected Sub DoProc_Dept_Change()
        Call DoGet_SelectedItem(Me.cboDepartment, Me.txtDeptNum, Me.txtDeptName, Me.lblMsg)
    End Sub

    Protected Sub DoProc_Dept_Refresh()
        Call gnProc_Populate_Box("IL_CODE_LIST", "005", Me.cboDepartment)

    End Sub

    Protected Sub DoProc_Nationality_Change()
        Call DoGet_SelectedItem(Me.cboNationality, Me.txtNationality, Me.txtNationalityName, Me.lblMsg)

    End Sub

    Protected Sub DoProc_Nationality_Refresh()
        Call gnProc_Populate_Box("IL_CODE_LIST", "001", Me.cboNationality)
    End Sub

    Protected Sub DoProc_ProductClass_Change()
        Try
            If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Or _
               Me.cboProductClass.SelectedItem.Value = "" Or Me.cboProductClass.SelectedItem.Value = "*" Then
                Me.txtProductClass.Text = ""
                Me.txtProduct_Num.Text = ""
                Me.cboProduct.SelectedIndex = -1
                'Me.lblMsg.Text = "Missing or Invalid " & Me.lblProductClass.Text
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            Else
                strTmp_Value = Me.cboProductClass.SelectedItem.Value
                myarrData = Split(strTmp_Value, "=")
                If myarrData.Count <> 2 Then
                    Me.lblMsg.Text = "Missing or Invalid " & Me.lblProductClass.Text
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                End If
                Me.txtProductClass.Text = myarrData(1)
                Me.txtProduct_Num.Text = ""
                Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)

                If cboProduct.Items.Count >= 1 Then
                    cboProduct.SelectedIndex = 1
                    DoProc_Product_Change()
                End If

                If cboCover_Name.Items.Count >= 1 Then
                    cboCover_Name.SelectedIndex = 1
                End If
                If cboPlan_Name.Items.Count >= 1 Then
                    cboPlan_Name.SelectedIndex = 1
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DoProc_Product_Change()
        Call DoGet_SelectedItem(Me.cboProduct, Me.txtProduct_Num, Me.txtProduct_Name, Me.lblMsg)
        Me.txtCover_Num.Text = ""
        Me.txtCover_Name.Text = ""
        Call gnProc_Populate_Box("IL_COVER_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
        Me.txtPlan_Num.Text = ""
        Me.txtPlan_Name.Text = ""
        Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)
        If cboCover_Name.Items.Count >= 1 Then
            cboCover_Name.SelectedIndex = 1
        End If
        If cboPlan_Name.Items.Count >= 1 Then
            cboPlan_Name.SelectedIndex = 1
        End If
    End Sub


    Protected Sub DoProc_OccupationClass_Change()
        Call DoGet_SelectedItem(Me.cboOccupationClass, Me.txtOccupationClass, Me.txtOccupationClassName, Me.lblMsg)
    End Sub


    Protected Sub DoProc_OccupationClass_Refresh()
        Call gnProc_Populate_Box("IL_CODE_LIST", "007", Me.cboOccupationClass)

    End Sub

    Protected Sub DoProc_Occupation_Change()
        Call DoGet_SelectedItem(Me.cboOccupation, Me.txtOccupationNum, Me.txtOccupationName, Me.lblMsg)
    End Sub

    Protected Sub DoProc_Occupation_Refresh()
        Call gnProc_Populate_Box("IL_CODE_LIST", "008", Me.cboOccupation)

    End Sub

    Protected Sub DoProc_Validate_Agency()
        If Trim(Me.txtAgcyNum.Text) = "" Then
            Me.txtAgcyName.Text = ""
        Else
            blnStatus = gnValidate_Codes("AGENCY_UND_LIFE", Me.txtAgcyNum, Me.txtAgcyName)
            If blnStatus = False Then
                Me.lblMsg.Text = "Invalid Agency Code: " & Me.txtAgcyNum.Text
                Me.txtAgcyNum.Text = ""
                Me.txtAgcyName.Text = ""
                'Me.lblMsg.Text = "<script type='text/javascript'>myShowDialogue('" & strParam1 & "','" & strParam2 & "'" & ");</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            End If
        End If

    End Sub

    Protected Sub DoProc_Validate_Assured()
        If Trim(Me.txtAssured_Num.Text) = "" Then
            Me.txtAssured_Name.Text = ""
        Else
            blnStatus = gnValidate_Codes("INSURED_CODE_LIFE", Me.txtAssured_Num, Me.txtAssured_Name)
            If blnStatus = False Then
                Me.lblMsg.Text = "Invalid Assured Code: " & Me.txtAssured_Num.Text
                Me.txtAssured_Num.Text = ""
                Me.txtAssured_Name.Text = ""
                'Me.lblMsg.Text = "<script type='text/javascript'>myShowDialogue('" & strParam1 & "','" & strParam2 & "'" & ");</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            End If
        End If

    End Sub

    Protected Sub DoProc_Validate_Broker()
        If Trim(Me.txtBrokerNum.Text) = "" Then
            Me.txtBrokerName.Text = ""
        Else
            blnStatus = gnValidate_Codes("BROKER_UND_LIFE", Me.txtBrokerNum, Me.txtBrokerName)
            If blnStatus = False Then
                Me.lblMsg.Text = "Invalid Broker Code: " & Me.txtBrokerNum.Text
                Me.txtBrokerNum.Text = ""
                Me.txtBrokerName.Text = ""
                'Me.lblMsg.Text = "<script type='text/javascript'>myShowDialogue('" & strParam1 & "','" & strParam2 & "'" & ");</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMsg.Text & "');", True)
            End If
        End If

    End Sub

    Private Sub Proc_DoDelete()
        Dim xc As Integer = 0

        If Trim(Me.txtFileNum.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtFileNum.Text)))
            If Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblFileNum.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        Next

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtQuote_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtQuote_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtQuote_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblQuote_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        Next

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
        strSQL = strSQL & " WHERE TBIL_POLY_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POLY_PROPSAL_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                '  remove premium file
                objOLECmd2.Dispose()
                objOLECmd2 = Nothing
                strSQL = ""
                strSQL = strSQL & "delete from TBIL_POLICY_PREM_INFO"
                strSQL = strSQL & " WHERE TBIL_POL_PRM_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
                strSQL = strSQL & " AND TBIL_POL_PRM_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
                objOLECmd2 = New OleDbCommand(strSQL, objOLEConn)
                intC = objOLECmd2.ExecuteNonQuery

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
                        CType(subctrl, TextBox).Text = ""
                    End If
                    If TypeOf subctrl Is System.Web.UI.WebControls.DropDownList Then
                        CType(subctrl, DropDownList).SelectedIndex = -1
                    End If
                Next
            End If
        Next

        Me.chkFileNum.Enabled = True
        Me.chkFileNum.Checked = False
        Me.lblFileNum.Enabled = False
        Me.txtFileNum.Enabled = False
        Me.cmdFileNum.Enabled = False

        Me.txtRecNo.Text = "0"
        Me.txtTrans_Date.Text = Format(Now, "dd/MM/yyyy")

        Me.lblQuote_Num.Enabled = True
        Me.txtQuote_Num.Enabled = True

        txtFreeMedCovLmt.Text = "0.00"
        txtRetention.Text = "0.00"
        txtCompShare.Text = "0.00"

        Select Case UCase(strP_TYPE)
            Case "NEW"
                Me.lblPolNum.Enabled = False
                Me.txtPolNum.Enabled = False
                Me.txtPolNum.Text = ""
                Me.cmdGetPol.Enabled = False
            Case "CHG"
                Me.lblPolNum.Enabled = True
                Me.txtPolNum.Enabled = True
                Me.txtPolNum.Text = ""
                Me.cmdGetPol.Enabled = True
            Case "DEL"
                Me.lblPolNum.Enabled = False
                Me.txtPolNum.Enabled = False
                Me.txtPolNum.Text = ""
                Me.cmdGetPol.Enabled = False
        End Select


        'If Me.txtProStatus.Text = "" Then
        Me.txtProStatus.Text = "P"
        Call gnProc_DDL_Get(Me.cboProStatus, RTrim(Me.txtProStatus.Text))
        'End If

        Me.cboProductClass.SelectedIndex = -1
        Me.cboProduct.SelectedIndex = -1
        Me.cboCover_Name.SelectedIndex = -1
        Me.cboPlan_Name.SelectedIndex = -1

        Me.txtProduct_Num.Text = ""

        Me.cmdSave_ASP.Enabled = True
        Me.cmdNext.Enabled = False

        Me.cmdSave_ASP.Visible = True
        Me.cmdDelete_ASP.Visible = True
        Me.cmdPrint_ASP.Visible = True

    End Sub


    Private Sub Proc_DoSave()

        Dim xc As Integer = 0

        Dim myTmp_Chk As String
        Dim myTmp_Ref As String
        Dim myTmp_RecStatus As String = ""
        myTmp_Chk = "N"
        myTmp_Ref = ""

        Dim mystrCONN_Chk As String = ""
        Dim objOLEConn_Chk As OleDbConnection = Nothing
        Dim objOLECmd_Chk As OleDbCommand = Nothing

        Me.txtFileNum.Text = LTrim(RTrim(Me.txtFileNum.Text))
        If Trim(Me.txtFileNum.Text) = "" Then
            'Me.txtFileNum.Text = "F/" & RTrim(Me.txtProduct_Num.Text) & "/" & RTrim("0000001")
            GoTo Proc_Skip_Check
        End If

        '====================================================
        '   START CHECK
        '====================================================

        For xc = 1 To Len(LTrim(RTrim(Me.txtFileNum.Text)))
            If Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblFileNum.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        Next

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
            Exit Sub
        End Try


        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POLY_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
            strSQL = strSQL & " AND TBIL_POLY_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        End If


        objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
        'objOLECmd_Chk.CommandTimeout = 180
        objOLECmd_Chk.CommandType = CommandType.Text
        'objOLECmd_Chk.CommandType = CommandType.StoredProcedure
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 40).Value = strREC_ID
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR_Chk As OleDbDataReader
        objOLEDR_Chk = objOLECmd_Chk.ExecuteReader()
        If (objOLEDR_Chk.Read()) Then
            myTmp_Chk = RTrim(CType(objOLEDR_Chk("TBIL_POLY_REC_ID") & vbNullString, String))
            myTmp_Ref = RTrim(CType(objOLEDR_Chk("TBIL_POLY_PROPSAL_NO") & vbNullString, String))
            myTmp_Ref = RTrim(CType(objOLEDR_Chk("TBIL_POLY_PROPSAL_NO") & vbNullString, String))


            If Val(myTmp_Chk) <> Val(Me.txtRecNo.Text) Then
                myTmp_Chk = "Y"
                Me.lblMsg.Text = "The File No you enter already exist. \nPlease check proposal no: " & myTmp_Ref & ""
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'Me.lblMsg.Text = "Status:"
                'Exit Sub
            Else
                myTmp_Chk = "N"
            End If
        Else
            myTmp_Chk = "N"
        End If


        objOLEDR_Chk = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing

        If Trim(myTmp_Chk) <> "N" Then
            Exit Sub
        End If
        '====================================================
        '   END CHECK
        '====================================================


Proc_Skip_Check:

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

        Dim Dte_System As Date = Now

        Dim myYear As String = ""

        Dim xx As Integer = 0

        'Validate date
        myarrData = Split(Me.txtTrans_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblTrans_Date.Text & ". Expecting full date in ddmmyyyy format ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        strMyDay = myarrData(0)
        strMyMth = myarrData(1)
        strMyYear = myarrData(2)

        strMyDay = CType(Format(Val(strMyDay), "00"), String)
        strMyMth = CType(Format(Val(strMyMth), "00"), String)
        strMyYear = CType(Format(Val(strMyYear), "0000"), String)
        If Val(strMyYear) < 1999 Then
            Me.lblMsg.Text = "Error. Proposal year date is less than 1999 ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)
        Me.txtTrans_Date.Text = Trim(strMyDte)

        If RTrim(Me.txtTrans_Date.Text) = "" Or Len(Trim(Me.txtTrans_Date.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - Proposal Date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtTrans_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblTrans_Date.Text & ". Expecting full date in ddmmyyyy format ..."
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
        Me.txtTrans_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        Dte_Proposal = Format(mydte, "MM/dd/yyyy")

        'Dte_Proposal = Now
        'Dte_Commence = Now
        Dte_Commence = Dte_Proposal

        If Trim(Me.txtUWYear.Text) = "" Then
            Me.txtUWYear.Text = Format(Dte_Proposal, "yyyy")
        End If
        If Not IsNumeric(Trim(Me.txtUWYear.Text)) Then
            Me.lblMsg.Text = "Invalid" & Me.lblUWYear.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Val(Trim(Me.txtUWYear.Text)) < 1990 Or Len(Trim(Me.txtUWYear.Text)) < 4 Then
            Me.lblMsg.Text = "Underwiting year less than 1990 ... "
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If txtRetention.Text <> "" Then
            If Not IsNumeric(txtRetention.Text) Then
                Me.lblMsg.Text = "Retention must be numeric"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtRetention.Focus()
                Exit Sub
            End If
        Else
            'Me.lblMsg.Text = "Retention not be empty"
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'txtRetention.Focus()
            'Exit Sub
            txtRetention.Text = "0.00"
        End If

        If txtFreeMedCovLmt.Text <> "" Then
            If Not IsNumeric(txtFreeMedCovLmt.Text) Then
                Me.lblMsg.Text = "Free medical cover limit must be numeric"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtFreeMedCovLmt.Focus()
                Exit Sub
            End If
        Else
            'Me.lblMsg.Text = "Free medical cover limit not be empty"
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'txtFreeMedCovLmt.Focus()
            'Exit Sub
            txtFreeMedCovLmt.Text = "0.00"
        End If

        If txtCompShare.Text <> "" Then
            If Not IsNumeric(txtCompShare.Text) Then
                Me.lblMsg.Text = "CIA share must be numeric"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtCompShare.Focus()
                Exit Sub
            End If
        Else
            'Me.lblMsg.Text = "CIA share must not be empty"
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'txtCompShare.Focus()
            'Exit Sub
            txtCompShare.Text = "0.00"
        End If



        Me.txtQuote_Num.Text = LTrim(RTrim(Me.txtQuote_Num.Text))
        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblQuote_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtQuote_Num.Text)))
            If Mid(LTrim(RTrim(Me.txtQuote_Num.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtQuote_Num.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblQuote_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        Next


        '====================================================
        '   START CHECK
        '====================================================

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
            Exit Sub
        End Try


        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POLY_PROPSAL_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
            strSQL = strSQL & " AND TBIL_POLY_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        End If


        objOLECmd_Chk = New OleDbCommand(strSQL, objOLEConn_Chk)
        'objOLECmd_Chk.CommandTimeout = 180
        objOLECmd_Chk.CommandType = CommandType.Text
        'objOLECmd_Chk.CommandType = CommandType.StoredProcedure
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 40).Value = strREC_ID
        'objOLECmd_Chk.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR_Pro As OleDbDataReader
        objOLEDR_Pro = objOLECmd_Chk.ExecuteReader()
        If (objOLEDR_Pro.Read()) Then
            myTmp_Chk = RTrim(CType(objOLEDR_Pro("TBIL_POLY_REC_ID") & vbNullString, String))
            myTmp_Ref = RTrim(CType(objOLEDR_Pro("TBIL_POLY_FILE_NO") & vbNullString, String))
            myTmp_RecStatus = Trim(CType(objOLEDR_Pro("TBIL_POLY_OLD_NEW") & vbNullString, String))

            'keep this record status for the whole browse session
            Session("myTmp_RecStatus") = myTmp_RecStatus

            If Val(myTmp_Chk) <> Val(Me.txtRecNo.Text) Then
                myTmp_Chk = "Y"
                Me.lblMsg.Text = "The Proposal No you entered already exist. \nPlease check file no: " & myTmp_Ref & ""
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                'Me.lblMsg.Text = "Status:"
                'Exit Sub
            Else
                myTmp_Chk = "N"
            End If
        Else

            'Dim message As String = "Is this an Old (Premia) Policy?"
            ''ClientScript.RegisterStartupScript(GetType(Page), "MessagePopUp", "alert('Your Massage');", True)

            ''ClientScript.RegisterStartupScript(Me.GetType, "ff", "alert('foo');", False)
            'ClientScript.RegisterStartupScript(GetType(Page), "Popup", "confirm('" + (message + "', doSomething);"), True)

            If chkPremiaRec.Checked Then
                myTmp_RecStatus = "OLD"
                Session("myTmp_RecStatus") = myTmp_RecStatus
            End If
            myTmp_Chk = "N"
        End If


        objOLEDR_Pro = Nothing
        objOLECmd_Chk.Dispose()
        objOLECmd_Chk = Nothing

        If objOLEConn_Chk.State = ConnectionState.Open Then
            objOLEConn_Chk.Close()
        End If
        objOLEConn_Chk = Nothing

        If Trim(myTmp_Chk) <> "N" Then
            Exit Sub
        End If
        '====================================================
        '   END CHECK
        '====================================================

        If Not Trim(myTmp_RecStatus) = "OLD" Then
            Call DoGet_SelectedItem(Me.cboBusSource, Me.txtBusSource, Me.txtBusSourceName, Me.lblMsg)
            If Me.txtBusSource.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblBusSource.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If


            Try
                If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Or _
                   Me.cboProductClass.SelectedItem.Value = "" Or Me.cboProductClass.SelectedItem.Value = "*" Then
                    Me.txtProductClass.Text = ""
                    Me.txtProduct_Num.Text = ""
                    Me.lblMsg.Text = "Missing or Invalid " & Me.lblProductClass.Text
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                Else
                    strTmp_Value = Me.cboProductClass.SelectedItem.Value
                    myarrData = Split(strTmp_Value, "=")
                    If myarrData.Count <> 2 Then
                        Me.lblMsg.Text = "Missing or Invalid " & Me.lblProductClass.Text
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                        Exit Sub
                    End If
                    Me.txtProductClass.Text = myarrData(1)
                End If
            Catch ex As Exception

            End Try

            If Me.txtProductClass.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblProductClass.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Try
                If Me.cboProduct.SelectedIndex = -1 Or Me.cboProduct.SelectedIndex = 0 Or _
                   Me.cboProduct.SelectedItem.Value = "" Or Me.cboProduct.SelectedItem.Value = "*" Then
                    Me.txtProduct_Num.Text = ""
                    Me.lblMsg.Text = "Missing or Invalid " & Me.lblProduct_Num.Text
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                Else
                    strTmp_Value = Me.cboProduct.SelectedItem.Value
                    Me.txtProduct_Num.Text = RTrim(strTmp_Value)
                End If
            Catch ex As Exception

            End Try


            If Me.txtProduct_Num.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblProduct_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call DoGet_SelectedItem(Me.cboCover_Name, Me.txtCover_Num, Me.txtCover_Name, Me.lblMsg)
            If Me.txtCover_Num.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblCover_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
            Call DoGet_SelectedItem(Me.cboPlan_Name, Me.txtPlan_Num, Me.txtPlan_Name, Me.lblMsg)
            If Me.txtPlan_Num.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblPlan_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            'If Me.txtAgcyNum.Text = "" Then
            '    Me.lblMsg.Text = "Missing " & Me.lblAgcyNum.Text
            '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            '    Exit Sub
            'End If

            If Trim(Me.txtAgcyNum.Text) = "" And Trim(Me.txtBrokerNum.Text) = "" Then
                Me.lblMsg.Text = "Missing Broker Code or Marketer Code. Please enter valid Agency Code... "
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            'If Me.txtBrokerNum.Text = "" Then
            'Me.lblMsg.Text = "Missing " & Me.lblBrokerNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
            'End If

            If Me.txtAssured_Num.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblAssuredNum.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If


            Call DoGet_SelectedItem(Me.cboNationality, Me.txtNationality, Me.txtNationalityName, Me.lblMsg)
            If Me.txtNationality.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblNationality.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call DoGet_SelectedItem(Me.cboOccupationClass, Me.txtOccupationClass, Me.txtOccupationClassName, Me.lblMsg)
            If Me.txtOccupationClass.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblOccupationClass.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call DoGet_SelectedItem(Me.cboOccupation, Me.txtOccupationNum, Me.txtOccupationName, Me.lblMsg)
            If Me.txtOccupationNum.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblOccupation.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

            Call DoGet_SelectedItem(Me.cboGender, Me.txtGender, Me.txtGenderName, Me.lblMsg)
            If Me.txtGender.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblGender.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
            Call DoGet_SelectedItem(Me.cboMaritalStatus, Me.txtMaritalStatus, Me.txtMaritalStatusName, Me.lblMsg)
            If Me.txtMaritalStatus.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblMaritalStatus.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If

        End If ' test for old end if


        'Validate date
        myarrData = Split(Me.txtDOB.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblDOB.Text & ". Expecting full date in ddmmyyyy format ..."
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
        Me.txtDOB.Text = Trim(strMyDte)

        If RTrim(Me.txtDOB.Text) = "" Or Len(Trim(Me.txtDOB.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - Date of Birth..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtDOB.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblDOB.Text & ". Expecting full date in ddmmyyyy format ..."
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
        Me.txtDOB.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteDOB = Format(mydte, "MM/dd/yyyy")


        Dte_DOB = dteDOB
        Dte_System = Now

        lngDOB_ANB = Val(DateDiff("yyyy", Dte_Proposal, Dte_DOB))
        lngDOB_ANB = Val(DateDiff("yyyy", Dte_System, Dte_DOB))
        If lngDOB_ANB < 0 Then
            lngDOB_ANB = lngDOB_ANB * -1
        End If

        If Dte_System.Month > Dte_DOB.Month Then
            lngDOB_ANB = lngDOB_ANB + 1
        End If
        Me.txtDOB_ANB.Text = Trim(Str(lngDOB_ANB))
        'Dte_Maturity = CStr(DateAdd("yyyy", Val(Me.txtDuration.Text), Dte_Commence))


        Call DoGet_SelectedItem(Me.cboDOB_Proof, Me.txtDOB_Proof, Me.txtDOB_ProofName, Me.lblMsg)
        'If Me.txtDOB_Proof.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblDOB_Proof.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        Call DoGet_SelectedItem(Me.cboReligion, Me.txtReligion, Me.txtReligionName, Me.lblMsg)
        If Me.txtReligion.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblReligion.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call DoGet_SelectedItem(Me.cboRelation, Me.txtRelation, Me.txtRelationName, Me.lblMsg)
        'If Me.txtRelation.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblRelation.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        Call DoGet_SelectedItem(Me.cboHeight, Me.txtHeight_Type, Me.txtHeight_TypeName, Me.lblMsg)
        'If Me.txtHeight_Type.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblHeight.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        Call DoGet_SelectedItem(Me.cboWeight, Me.txtWeight_Type, Me.txtWeight_TypeName, Me.lblMsg)
        'If Me.txtWeight_Type.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblWeight.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        Call DoGet_SelectedItem(Me.cboBranch, Me.txtBraNum, Me.txtBraName, Me.lblMsg)
        If Me.txtBraNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblBraNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call DoGet_SelectedItem(Me.cboDepartment, Me.txtDeptNum, Me.txtDeptName, Me.lblMsg)
        If Me.txtDeptNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblDeptNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If



        If Trim(Me.txtProStatus.Text) = "" Then
            Me.txtProStatus.Text = "P"
            Call gnProc_DDL_Get(Me.cboProStatus, RTrim(Me.txtProStatus.Text))
        End If

        'Call DoGet_SelectedItem(Me.cboProStatus, Me.txtProStatus, Me.txtProStatusName, Me.lblMsg)
        'If Me.txtProStatus.Text = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblProStatus.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If


        'Me.lblMsg.Text = "About to submit data... "
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"



        Me.txtFileNum.Text = LTrim(RTrim(Me.txtFileNum.Text))
        If Me.chkFileNum.Checked = True And Me.txtFileNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Me.chkFileNum.Checked = True And Me.txtFileNum.Text = "*" Then
            Me.lblMsg.Text = "Invalid " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.chkFileNum.Checked = True And Me.txtFileNum.Text = "." Then
            Me.lblMsg.Text = "Invalid " & Me.lblFileNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        For xc = 1 To Len(LTrim(RTrim(Me.txtFileNum.Text)))
            If Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ";" Or Mid(LTrim(RTrim(Me.txtFileNum.Text)), xc, 1) = ":" Then
                Me.lblMsg.Text = "Invalid character found in input field - " & Me.lblFileNum.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        Next

        myYear = Trim(Me.txtUWYear.Text)


        If Trim(Me.txtProStatus.Text) = "" Then
            Me.txtProStatus.Text = "P"
        End If


        'Me.txtFileNum.Enabled = True
        'Me.txtFileNum.ReadOnly = False

        If Trim(txtFileNum.Text) = "" Then
            Me.txtFileNum.Text = MOD_GEN.gnGet_Serial_File_Proposal_Policy(RTrim("GET_SN_IL_FIL_PRO_POL"), RTrim("FIL"), Trim(myYear), RTrim("IL"), RTrim(Me.txtBraNum.Text), RTrim(Me.txtProductClass.Text), RTrim(Me.txtProduct_Num.Text), RTrim("I"), RTrim(""), RTrim(""))
        End If

        If Trim(txtFileNum.Text) = "" Or Trim(Me.txtFileNum.Text) = "0" Or Trim(Me.txtFileNum.Text) = "*" Then
            'Me.txtFileNum.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next FILE NO. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtFileNum.Text) = "PARAM_ERR" Then
            Me.txtFileNum.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next FILE NO - INVALID PARAMETER(S) ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtFileNum.Text) = "DB_ERR" Then
            Me.txtFileNum.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtFileNum.Text) = "ERR_ERR" Then
            Me.txtFileNum.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        End If


        Me.txtQuote_Num.Text = LTrim(RTrim(Me.txtQuote_Num.Text))
        If Trim(Me.txtQuote_Num.Text) = "" Then
            'Me.txtQuote_Num.Text = "Q/" & RTrim(Me.txtProduct_Num.Text) & "/" & RTrim("0000001")
        End If

        If Trim(txtQuote_Num.Text) = "" Then
            Me.txtQuote_Num.Text = MOD_GEN.gnGet_Serial_File_Proposal_Policy(RTrim("GET_SN_IL_FIL_PRO_POL"), RTrim("PRO"), Trim(myYear), RTrim("IL"), RTrim(Me.txtBraNum.Text), RTrim(Me.txtProductClass.Text), RTrim(Me.txtProduct_Num.Text), RTrim("I"), RTrim(""), RTrim(""))
        End If

        If Trim(txtQuote_Num.Text) = "" Or Trim(Me.txtQuote_Num.Text) = "0" Or Trim(Me.txtQuote_Num.Text) = "*" Then
            Me.txtQuote_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next QUOTATION NO. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtQuote_Num.Text) = "PARAM_ERR" Then
            Me.txtQuote_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next QUOTATION NO - INVALID PARAMETER(S)..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtQuote_Num.Text) = "DB_ERR" Then
            Me.txtQuote_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtQuote_Num.Text) = "ERR_ERR" Then
            Me.txtQuote_Num.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        End If


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
        strSQL = strSQL & " WHERE TBIL_POLY_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
            strSQL = strSQL & " AND TBIL_POLY_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        End If

        'Dim objOLETran As OleDbTransaction
        ' Start a local transaction.
        'objOLETran = objOLEConn.BeginTransaction


        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        'objDA.SelectCommand.Connection = objOLEConn
        'objDA.SelectCommand.Transaction = objOLETran
        'objDA.SelectCommand.CommandType = CommandType.Text
        'objDA.SelectCommand.CommandText = strSQL
        'or
        'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        'Dim m_rwContact As System.Data.DataRow
        Dim intC As Integer = 0


        'TBIL_POLY_PRPSAL_ISSUE_DATE()
        'TBIL_POLICY_ISSUE_DT()
        'TBIL_POLICY_EFF_DT()
        'TBIL_POLY_ASSRD_BDATE()
        'TBIL_POLY_ASSRD_AGE()

        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("TBIL_POLY_MDLE") = RTrim("I")

                drNewRow("TBIL_POLY_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_POLY_PROPSAL_NO") = RTrim(Me.txtQuote_Num.Text)
                'drNewRow("TBIL_POLY_POLICY_NO") = RTrim(Me.txtPolNum.Text)

                drNewRow("TBIL_POLY_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)
                drNewRow("TBIL_POLY_PLAN_CD") = RTrim(Me.txtPlan_Num.Text)
                drNewRow("TBIL_POLY_COVER_CD") = RTrim(Me.txtCover_Num.Text)

                drNewRow("TBIL_POLY_UNDW_YR") = Me.txtUWYear.Text
                drNewRow("TBIL_POLY_BRANCH_CD") = RTrim(Me.txtBraNum.Text)
                drNewRow("TBIL_POLY_DEPT_CD") = RTrim(Me.txtDeptNum.Text)

                drNewRow("TBIL_POLY_PRPSAL_ISSUE_DATE") = Dte_Proposal
                'drNewRow("TBIL_POLY_PRPSAL_RECD_DT") = Dte_Proposal
                'drNewRow("TBIL_POLY_PROPSL_ACCPT_DT") = Dte_Proposal
                drNewRow("TBIL_POLY_PROPSL_ACCPT_STATUS") = Me.txtProStatus.Text

                'drNewRow("TBIL_POLICY_ISSUE_DT") = Dte_Proposal
                'drNewRow("TBIL_POLICY_EFF_DT") = Dte_Proposal

                drNewRow("TBIL_POLY_CUST_CODE") = RTrim(Me.txtBrokerNum.Text)
                drNewRow("TBIL_POLY_SOURC_BIZ") = RTrim(Me.txtBusSource.Text)
                drNewRow("TBIL_POLY_AGCY_CODE") = RTrim(Me.txtAgcyNum.Text)
                drNewRow("TBIL_POLY_ASSRD_CD") = RTrim(Me.txtAssured_Num.Text)

                If Trim(Me.txtDOB.Text) <> "" Then
                    drNewRow("TBIL_POLY_ASSRD_BDATE") = dteDOB
                    'drNewRow("TBIL_POLY_ASSRD_AGE") = Trim(Me.txtDOB_ANB.Text)
                End If

                drNewRow("TBIL_POLY_ASSRD_HEIGHT") = Trim(Me.txtHeight.Text)
                drNewRow("TBIL_POLY_ASSRD_WEIGHT") = Trim(Me.txtWeight.Text)
                drNewRow("TBIL_POLY_ASSRD_HEIGHT_TYP") = Trim(Me.txtHeight_Type.Text)
                drNewRow("TBIL_POLY_ASSRD_WEIGHT_TYP") = Trim(Me.txtWeight_Type.Text)

                drNewRow("TBIL_POLY_AGE_PROOF") = Trim(Me.txtDOB_Proof.Text)
                drNewRow("TBIL_POLY_ASSRD_AGE") = Val(Trim(Me.txtDOB_ANB.Text))

                drNewRow("TBIL_POLY_ASSRD_NATIONLTY") = RTrim(Me.txtNationality.Text)
                drNewRow("TBIL_POLY_GENDER") = RTrim(Me.txtGender.Text)
                drNewRow("TBIL_POLY_MARITAL") = RTrim(Me.txtMaritalStatus.Text)

                drNewRow("TBIL_POLY_OCCUTN_CLASS") = RTrim(Me.txtOccupationClass.Text)
                drNewRow("TBIL_POLY_OCCUPATN") = RTrim(Me.txtOccupationNum.Text)

                drNewRow("TBIL_POLY_RELIGN_CD") = RTrim(Me.txtReligion.Text)
                drNewRow("TBIL_POLY_ASSRD_RELATN") = RTrim(Me.txtRelation.Text)

                drNewRow("TBIL_POLY_ASSRD_PLACE_BIRTH") = RTrim(Me.txtDOB_Place.Text)

                drNewRow("TBIL_POLY_MED_COVER_LMT") = txtFreeMedCovLmt.Text
                drNewRow("TBIL_POLY_RETENTION") = txtRetention.Text
                drNewRow("TBIL_POLY_COMP_SHARE") = Val(Trim(txtCompShare.Text))

                drNewRow("TBIL_POLY_FLAG") = "A"
                drNewRow("TBIL_POLY_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_POLY_KEYDTE") = Now

                drNewRow("TBIL_POLY_REF_NEW") = RTrim(txtOtherRefNo.Text)
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
                    .Rows(0)("TBIL_POLY_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_POLY_PROPSAL_NO") = RTrim(Me.txtQuote_Num.Text)
                    '.Rows(0)("TBIL_POLY_POLICY_NO") = RTrim(Me.txtPolNum.Text)

                    .Rows(0)("TBIL_POLY_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)
                    .Rows(0)("TBIL_POLY_PLAN_CD") = RTrim(Me.txtPlan_Num.Text)
                    .Rows(0)("TBIL_POLY_COVER_CD") = RTrim(Me.txtCover_Num.Text)

                    .Rows(0)("TBIL_POLY_UNDW_YR") = Me.txtUWYear.Text
                    .Rows(0)("TBIL_POLY_BRANCH_CD") = RTrim(Me.txtBraNum.Text)
                    .Rows(0)("TBIL_POLY_DEPT_CD") = RTrim(Me.txtDeptNum.Text)

                    .Rows(0)("TBIL_POLY_PRPSAL_ISSUE_DATE") = Dte_Proposal
                    '.Rows(0)("TBIL_POLY_PRPSAL_RECD_DT") = Dte_Proposal
                    '.Rows(0)("TBIL_POLY_PROPSL_ACCPT_DT") = Dte_Proposal
                    .Rows(0)("TBIL_POLY_PROPSL_ACCPT_STATUS") = Me.txtProStatus.Text

                    '.Rows(0)("TBIL_POLICY_ISSUE_DT") = Dte_Proposal
                    '.Rows(0)("TBIL_POLICY_EFF_DT") = Dte_Proposal

                    .Rows(0)("TBIL_POLY_CUST_CODE") = RTrim(Me.txtBrokerNum.Text)
                    .Rows(0)("TBIL_POLY_SOURC_BIZ") = RTrim(Me.txtBusSource.Text)
                    .Rows(0)("TBIL_POLY_AGCY_CODE") = RTrim(Me.txtAgcyNum.Text)
                    .Rows(0)("TBIL_POLY_ASSRD_CD") = RTrim(Me.txtAssured_Num.Text)


                    If Trim(Me.txtDOB.Text) <> "" Then
                        .Rows(0)("TBIL_POLY_ASSRD_BDATE") = dteDOB
                        '.Rows(0)("TBIL_POLY_ASSRD_AGE") = Trim(Me.txtDOB_ANB.Text)
                    End If

                    .Rows(0)("TBIL_POLY_ASSRD_HEIGHT") = Trim(Me.txtHeight.Text)
                    .Rows(0)("TBIL_POLY_ASSRD_WEIGHT") = Trim(Me.txtWeight.Text)
                    .Rows(0)("TBIL_POLY_ASSRD_HEIGHT_TYP") = Trim(Me.txtHeight_Type.Text)
                    .Rows(0)("TBIL_POLY_ASSRD_WEIGHT_TYP") = Trim(Me.txtWeight_Type.Text)

                    .Rows(0)("TBIL_POLY_AGE_PROOF") = Trim(Me.txtDOB_Proof.Text)
                    .Rows(0)("TBIL_POLY_ASSRD_AGE") = Val(Trim(Me.txtDOB_ANB.Text))

                    .Rows(0)("TBIL_POLY_ASSRD_NATIONLTY") = RTrim(Me.txtNationality.Text)
                    .Rows(0)("TBIL_POLY_GENDER") = RTrim(Me.txtGender.Text)
                    .Rows(0)("TBIL_POLY_MARITAL") = RTrim(Me.txtMaritalStatus.Text)

                    .Rows(0)("TBIL_POLY_OCCUTN_CLASS") = RTrim(Me.txtOccupationClass.Text)
                    .Rows(0)("TBIL_POLY_OCCUPATN") = RTrim(Me.txtOccupationNum.Text)

                    .Rows(0)("TBIL_POLY_RELIGN_CD") = RTrim(Me.txtReligion.Text)
                    .Rows(0)("TBIL_POLY_ASSRD_RELATN") = RTrim(Me.txtRelation.Text)

                    .Rows(0)("TBIL_POLY_ASSRD_PLACE_BIRTH") = RTrim(Me.txtDOB_Place.Text)

                    .Rows(0)("TBIL_POLY_MED_COVER_LMT") = txtFreeMedCovLmt.Text
                    .Rows(0)("TBIL_POLY_RETENTION") = txtRetention.Text
                    .Rows(0)("TBIL_POLY_COMP_SHARE") = Val(Trim(txtCompShare.Text))

                    .Rows(0)("TBIL_POLY_FLAG") = "C"
                    .Rows(0)("TBIL_POLY_FLAG") = "C"
                    .Rows(0)("TBIL_POLY_REF_NEW") = RTrim(Me.txtOtherRefNo.Text)

                    '.Rows(0)("TBIL_POLY_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_POLY_KEYDTE") = Now

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

    Private Sub PUpdate_Date()
        '        Dim lngANB As Integer
        '        Dim Dte1 As Date, Dte2 As Date, Dte3 As Date

        '        If Me.txtCommenceDate.Text = "" Or _
        '           Me.txtDateOfBirth.Text = "" Then
        '            GoTo PUpdate_Date1
        '        End If
        '        lngANB = Val(DateDiff("yyyy", Me.txtPropDate.Text, Me.txtDateOfBirth.Text))
        '        If lngANB < 0 Then
        '            lngANB = lngANB * -1
        '        End If
        '        Dte1 = CDate(Me.txtPropDate.Text)
        '        Dte1 = CDate(Me.txtCommenceDate.Text)
        '        Dte2 = CDate(Me.txtDateOfBirth.Text)
        '        If Dte1.Month > Dte2.Month Then
        '            lngANB = lngANB + 1
        '        End If
        '        Me.txtAge.Text = Trim(Str(lngANB))
        '        Dte3 = CDate(Me.txtCommenceDate.Text)
        '        Me.txtMaturityDate.Text = CStr(DateAdd("yyyy", Val(Me.txtDuration.Text), Dte3))

        '        Call PUpdate_Prem()

PUpdate_Date1:

    End Sub


    Private Sub PUpdate_Prem()

        '        Dim mySQL As String, myTable As String
        '        Dim myIntModeOfPay As Integer = 0
        '        Dim myIntTerm As Integer = 0
        '        Dim myStrModeOfPay As String
        '        Dim myStrAge As String

        '        Dim mystrPremTermYear As String = 0 '"45"
        '        Dim mystrProfitYN As String = 0 '"PROFIT_LTD"
        '        Dim premtotal As Single = 0
        '        Dim mySgnSA As Single = 0 'Sum Assured
        '        Dim mySgnPremRate As Single = 0 '14.18 'Premium Rate
        '        Dim mySgnFreqRate As Single = 0 '9.0 'Frequency Rate
        '        Dim mySgnStampRate As Single = 0 '1.5 'Stamp Duty Rate
        '        Dim mySgnStampFee As Single = 0
        '        Dim mySgnPolicyFee As Single = 0 '250.0
        '        Dim mySgnAnnualPrem As Single = 0
        '        Dim mySgnBasicPrem As Single = 0

        '        myStrAge = Me.txtAge.Text
        '        myIntTerm = (Me.txtDuration.Text)
        '        mySgnSA = (Me.txtSumAssured.Text)
        '        mySgnPolicyFee = (Me.txtPolicyFee.Text)

        '        myStrModeOfPay = Me.cbPaymentFreqency.SelectedItem.Value
        '        If myStrModeOfPay = "Single" Then 'Single
        '            myIntModeOfPay = 1
        '        ElseIf myStrModeOfPay = "Annually" Then 'Annual
        '            myIntModeOfPay = 1
        '        ElseIf myStrModeOfPay = "Half-Year" Then 'HalfYear
        '            myIntModeOfPay = 2
        '        ElseIf myStrModeOfPay = "Quarterly" Then 'Quarterly
        '            myIntModeOfPay = 4
        '        ElseIf myStrModeOfPay = "Monthly" Then 'Monthly
        '            myIntModeOfPay = 12
        '        End If

        '        If Me.cbPremTermYear.SelectedItem.Value = "LP" Then
        '            mystrPremTermYear = Me.txtPremTermYear.Text
        '        Else
        '            mystrPremTermYear = "0"
        '        End If
        '        If Me.cbPremTermYear.SelectedItem.Value = "LP" And _
        '           Me.cbPolicyPlan.SelectedItem.Value = "WP" Then
        '            mystrProfitYN = "PROFIT_LTD"
        '        End If
        '        If Me.cbPremTermYear.SelectedItem.Value = "LP" And _
        '           Me.cbPolicyPlan.SelectedItem.Value = "NP" Then
        '            mystrProfitYN = "NOPROFIT_LTD"
        '        End If
        '        If Me.cbPremTermYear.SelectedItem.Value = "UP" And _
        '           Me.cbPolicyPlan.SelectedItem.Value = "WP" Then
        '            mystrProfitYN = "PROFIT_ULTD"
        '        End If
        '        If Me.cbPremTermYear.SelectedItem.Value = "UP" And _
        '           Me.cbPolicyPlan.SelectedItem.Value = "NP" Then
        '            mystrProfitYN = "NOPROFIT_ULTD"
        '        End If

        '        Dim ConnX As OleDbConnection = New OleDbConnection(ConnString)
        '        Dim CmddX As OleDbCommand
        '        Dim objDR As OleDbDataReader
        '        Dim ansX As Integer = 0

        '        myTable = "Undstampduty1"
        '        mySQL = "SELECT * FROM " & myTable
        '        'mySQL += " WHERE TransType = '" & mystrProfitYN & "'"
        '        'mySQL += " AND Age = '" & myStrAge & "'"
        '        'mySQL += " AND PremTermYear = '" & mystrPremTermYear & "'"

        '        ConnX.Open()
        '        CmddX = New OleDbCommand(mySQL, ConnX)
        '        CmddX.CommandType = CommandType.Text
        '        objDR = CmddX.ExecuteReader(CommandBehavior.CloseConnection)
        '        If (objDR.Read()) Then
        '            mySgnStampRate = CType(objDR("Stampduty"), Single)
        '        Else
        '            'mySgnPremRate = 0
        '        End If
        '        objDR.Close()
        '        CmddX.Dispose()
        '        'ConnX.Open()
        '        'CmddX = New OleDbCommand("mktTHISProposal", ConnX)
        '        'CmddX.CommandType = CommandType.StoredProcedure
        '        'CmddX.Parameters.Clear()
        '        'CmddX.Parameters.Add("myCode", OleDbType.VarChar, 50).Value = Me.txtPropNo.Text
        '        'CmddX.Parameters.Add("myCode1", OleDbType.VarChar, 50).Value = ""
        '        'ansX += CmddX.ExecuteNonQuery()

        '        'To get Whole life premium rate
        '        myTable = "LifWholeLifePremRate"
        '        mySQL = "SELECT * FROM " & myTable
        '        mySQL += " WHERE TransType = '" & mystrProfitYN & "'"
        '        mySQL += " AND Age = '" & myStrAge & "'"
        '        mySQL += " AND PremTermYear = '" & mystrPremTermYear & "'"

        '        ConnX.Open()
        '        CmddX = New OleDbCommand(mySQL, ConnX)
        '        CmddX.CommandType = CommandType.Text
        '        objDR = CmddX.ExecuteReader(CommandBehavior.CloseConnection)
        '        If (objDR.Read()) Then
        '            mySgnPremRate = CType(objDR("PremRate"), Single)
        '        Else
        '            mySgnPremRate = 0
        '        End If
        '        objDR.Close()
        '        CmddX.Dispose()

        '        'To get Whole life premium rate
        '        myTable = "LifBasicRate"
        '        mySQL = "SELECT * FROM " & myTable
        '        mySQL += " WHERE Code = '" & myStrModeOfPay & "'"

        '        ConnX.Open()
        '        CmddX = New OleDbCommand(mySQL, ConnX)
        '        CmddX.CommandType = CommandType.Text
        '        objDR = CmddX.ExecuteReader(CommandBehavior.CloseConnection)
        '        If (objDR.Read()) Then
        '            mySgnFreqRate = CType(objDR("BasicRate"), Single)
        '        Else
        '            mySgnFreqRate = 0
        '        End If

        '        objDR.Close()
        '        CmddX.Dispose()

        '        ConnX.Dispose()
        '        ConnX.Close()
        '        Me.txtPremRate.Text = (mySgnPremRate)
        '        Me.txtFreqRate.Text = CStr(mySgnFreqRate)
        '        Me.txtStampDuty.Text = (mySgnStampRate)


        '        mySgnStampFee = ((mySgnSA / 1000) * mySgnStampRate)
        '        Me.txtStampDutyFee.Text = CStr(mySgnStampFee)
        '        mySgnAnnualPrem = ((mySgnSA / 1000) * mySgnPremRate) + (mySgnStampFee + mySgnPolicyFee)
        '        Me.txtAnnualPrem.Text = (mySgnAnnualPrem)

        '        If myStrModeOfPay = "Monthly" Or _
        '           myStrModeOfPay = "Quarterly" Or _
        '           myStrModeOfPay = "Half-Year" Then
        '            mySgnBasicPrem = (mySgnAnnualPrem * mySgnFreqRate) / 100
        '            'myBasicExtMed = (mySgnAnnualPrem * mySgnFreqRate) / 100
        '        Else
        '            mySgnBasicPrem = mySgnAnnualPrem
        '            'myBasicExtMed = myAnnExtMed
        '        End If
        '        premtotal = mySgnBasicPrem

        '        Call ExtraPremValue()

        '        Me.txtPremPayable.Text = (Me.txtExtrPremVal.Text + premtotal)

    End Sub

    Private Sub ExtraPremValue()

        '    Dim mySQL As String, myTable As String
        '    Dim myIntModeOfPay As Integer = 0
        '    Dim myIntTerm As Integer = 0
        '    Dim myStrModeOfPay As String
        '    Dim myStrAge As String
        '    Dim mySgnFreqRate As Single = 0
        '    Dim mystrPremRate As Single = 0
        '    Dim annExtPre As Single = 0
        '    Dim myAnnExtMed As Single = 0
        '    Dim myBasicExtMed As Single = 0
        '    Dim mySgnAnnualPrem As Single = 0
        '    Dim mySgnBasicPrem As Single = 0
        '    Dim mySgnSA As Single = 0 'Sum Assured
        '    mySgnSA = (Me.txtSumAssured.Text)

        '    myStrModeOfPay = Me.cbPaymentFreqency.SelectedItem.Value
        '    If myStrModeOfPay = "Single" Then 'Single
        '        myIntModeOfPay = 1
        '    ElseIf myStrModeOfPay = "Annually" Then 'Annual
        '        myIntModeOfPay = 1
        '    ElseIf myStrModeOfPay = "Half-Year" Then 'HalfYear
        '        myIntModeOfPay = 2
        '    ElseIf myStrModeOfPay = "Quarterly" Then 'Quarterly
        '        myIntModeOfPay = 4
        '    ElseIf myStrModeOfPay = "Monthly" Then 'Monthly
        '        myIntModeOfPay = 12
        '    End If
        '    If Me.cbStateHealth.SelectedItem.Value = "Sub-StardardLife" Then
        '        mystrPremRate = Me.txtExtrPremRate.Text
        '    Else
        '        mystrPremRate = "0"
        '    End If
        '    'If Me.cbStateHealth.SelectedItem.Value = "StardardLife" Then
        '    '    mystrPremRate = Me.txtExtrPremRate.Text
        '    '    mystrPremRate = "0"
        '    'Else
        '    '    mystrPremRate = "0"
        '    'End If
        '    Dim ConnX As OleDbConnection = New OleDbConnection(ConnString)
        '    Dim CmddX As OleDbCommand
        '    Dim objDR As OleDbDataReader
        '    Dim ansX As Integer = 0

        '    myTable = "LifBasicRate"
        '    mySQL = "SELECT * FROM " & myTable
        '    mySQL += " WHERE Code = '" & myStrModeOfPay & "'"

        '    ConnX.Open()
        '    CmddX = New OleDbCommand(mySQL, ConnX)
        '    CmddX.CommandType = CommandType.Text
        '    objDR = CmddX.ExecuteReader(CommandBehavior.CloseConnection)
        '    If (objDR.Read()) Then
        '        mySgnFreqRate = CType(objDR("BasicRate"), Single)
        '    Else
        '        mySgnFreqRate = 0
        '    End If

        '    objDR.Close()
        '    CmddX.Dispose()

        '    ConnX.Dispose()
        '    ConnX.Close()
        '    myAnnExtMed = (mySgnSA / 1000) * mystrPremRate

        '    If myStrModeOfPay = "Monthly" Or _
        '               myStrModeOfPay = "Quarterly" Or _
        '               myStrModeOfPay = "Half-Year" Then
        '        'mySgnBasicPrem = (myAnnExtMed * mySgnFreqRate) / 100
        '        myBasicExtMed = (myAnnExtMed * mySgnFreqRate) / 100
        '    Else
        '        'mySgnBasicPrem = mySgnAnnualPrem
        '        myBasicExtMed = myAnnExtMed
        '    End If
        '    Me.txtExtrPremVal.Text = (myBasicExtMed)
        '    'Me.txtPremPayable.Text += (myBasicExtMed)

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
        strSQL = strSQL & "SELECT TOP 1 PT.*"
        strSQL = strSQL & " FROM " & strTable & " AS PT"
        strSQL = strSQL & " WHERE PT.TBIL_POLY_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND PT.TBIL_POLY_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPIL_GET_POLICY_DET"

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

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POLY_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PROPSAL_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))

            If IsDate(objOLEDR("TBIL_POLY_PRPSAL_ISSUE_DATE")) Then
                Me.txtTrans_Date.Text = Format(CType(objOLEDR("TBIL_POLY_PRPSAL_ISSUE_DATE"), DateTime), "dd/MM/yyyy")
            End If

            Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim(Me.txtProductClass.Text))
            'TBIL_PRDCT_DTL_MDLE
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("IND=") & RTrim(Me.txtProductClass.Text))
            Call gnProc_DDL_Get(Me.cboProductClass, RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_MDLE") & vbNullString, String)) & RTrim("=") & RTrim(Me.txtProductClass.Text))
            'If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Then
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("I=") & RTrim(Me.txtProductClass.Text))
            'End If

            Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)
            Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PRDCT_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboProduct, RTrim(Me.txtProduct_Num.Text))
            Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_DESC") & vbNullString, String))

            Call gnProc_Populate_Box("IL_COVER_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
            Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)

            Me.txtCover_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_COVER_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboCover_Name, RTrim(Me.txtCover_Num.Text))

            Me.txtPlan_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PLAN_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboPlan_Name, RTrim(Me.txtPlan_Num.Text))

            Me.txtUWYear.Text = RTrim(CType(objOLEDR("TBIL_POLY_UNDW_YR") & vbNullString, String))

            Me.txtBrokerNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_CUST_CODE") & vbNullString, String))
            Me.txtBrokerName.Text = RTrim(CType(objOLEDR("TBIL_CUST_DESC") & vbNullString, String))

            Me.txtBusSource.Text = RTrim(CType(objOLEDR("TBIL_POLY_SOURC_BIZ") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBusSource, RTrim(Me.txtBusSource.Text))

            Me.txtAgcyNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_AGCY_CODE") & vbNullString, String))
            Me.txtAgcyName.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))

            Me.txtAssured_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_CD") & vbNullString, String))
            Me.txtAssured_Name.Text = RTrim(CType(objOLEDR("TBIL_INSRD_NAME") & vbNullString, String))

            Me.txtNationality.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_NATIONLTY") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboNationality, RTrim(Me.txtNationality.Text))

            Me.txtOccupationClass.Text = RTrim(CType(objOLEDR("TBIL_POLY_OCCUTN_CLASS") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboOccupationClass, RTrim(Me.txtOccupationClass.Text))
            Me.txtOccupationNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_OCCUPATN") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboOccupation, RTrim(Me.txtOccupationNum.Text))

            Me.txtGender.Text = RTrim(CType(objOLEDR("TBIL_POLY_GENDER") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboGender, RTrim(Me.txtGender.Text))
            Me.txtMaritalStatus.Text = RTrim(CType(objOLEDR("TBIL_POLY_MARITAL") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboMaritalStatus, RTrim(Me.txtMaritalStatus.Text))

            Me.txtBraNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_BRANCH_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBranch, RTrim(Me.txtBraNum.Text))
            Me.txtDeptNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_DEPT_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboDepartment, RTrim(Me.txtDeptNum.Text))

            Me.txtHeight.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_HEIGHT") & vbNullString, String))
            Me.txtWeight.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_WEIGHT") & vbNullString, String))
            Me.txtHeight_Type.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_HEIGHT_TYP") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboHeight, RTrim(Me.txtHeight_Type.Text))
            Me.txtWeight_Type.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_WEIGHT_TYP") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboWeight, RTrim(Me.txtWeight_Type.Text))


            If IsDate(objOLEDR("TBIL_POLY_ASSRD_BDATE")) Then
                Me.txtDOB.Text = Format(CType(objOLEDR("TBIL_POLY_ASSRD_BDATE"), DateTime), "dd/MM/yyyy")
            End If
            Me.txtDOB_ANB.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_AGE") & vbNullString, String))

            Me.txtDOB_Proof.Text = RTrim(CType(objOLEDR("TBIL_POLY_AGE_PROOF") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboDOB_Proof, RTrim(Me.txtDOB_Proof.Text))

            Me.txtDOB_Place.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_PLACE_BIRTH") & vbNullString, String))
            Me.txtReligion.Text = RTrim(CType(objOLEDR("TBIL_POLY_RELIGN_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboReligion, RTrim(Me.txtReligion.Text))

            Me.txtRelation.Text = RTrim(CType(objOLEDR("TBIL_POLY_ASSRD_RELATN") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboRelation, RTrim(Me.txtRelation.Text))

            Me.txtProStatus.Text = RTrim(CType(objOLEDR("TBIL_POLY_PROPSL_ACCPT_STATUS") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboProStatus, RTrim(Me.txtProStatus.Text))

            Me.txtOtherRefNo.Text = RTrim(CType(objOLEDR("TBIL_POLY_REF_NEW") & vbNullString, String))


            'Me.txtFreeMedCovLmt.Text = Format(RTrim(CType(objOLEDR("TBIL_POLY_MED_COVER_LMT") & vbNullString, String)), "Standard")
            'Me.txtRetention.Text = Format(RTrim(CType(objOLEDR("TBIL_POLY_RETENTION") & vbNullString, String)), "Standard")
            'Me._txtCompShare.Text = RTrim(CType(objOLEDR("TBIL_POLY_COMP_SHARE") & vbNullString, String))

            If Not IsDBNull(objOLEDR("TBIL_POLY_MED_COVER_LMT")) Then
                txtFreeMedCovLmt.Text = Format(objOLEDR("TBIL_POLY_MED_COVER_LMT"), "Standard")
            Else
                txtFreeMedCovLmt.Text = "0.00"
            End If


            If Not IsDBNull(objOLEDR("TBIL_POLY_RETENTION")) Then
                txtRetention.Text = Format(objOLEDR("TBIL_POLY_RETENTION"), "Standard")
            Else
                txtRetention.Text = "0.00"
            End If

            If Not IsDBNull(objOLEDR("TBIL_POLY_COMP_SHARE")) Then
                txtCompShare.Text = objOLEDR("TBIL_POLY_COMP_SHARE")
            Else
                txtCompShare.Text = "0.00"
            End If

            lblOriginator.Text = "Record Originally created by " & GetRecordOriginator(RTrim(CType(objOLEDR("TBIL_POLY_OPERID") & vbNullString, String)))

            Me.lblFileNum.Enabled = False
            'Call DisableBox(Me.txtFileNum)
            Me.chkFileNum.Enabled = False
            Me.cmdFileNum.Enabled = False

            Me.txtFileNum.Enabled = False
            Me.txtQuote_Num.Enabled = False

            Me.cmdGetPol.Enabled = False
            Me.lblPlan_Num.Enabled = False
            Me.txtPolNum.Enabled = False

            Me.cmdNew_ASP.Enabled = True
            Me.cmdSave_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True
            Me.cmdNext.Enabled = True

            If Trim(Me.txtProStatus.Text) = "A" Then
                Me.chkFileNum.Enabled = False
                Me.lblFileNum.Enabled = False
                Me.txtFileNum.Enabled = False
                Me.cmdFileNum.Enabled = False
                'Me.cmdSave_ASP.Enabled = False
                Me.cmdDelete_ASP.Enabled = False

                'Me.cmdSave_ASP.Visible = False
                Me.cmdDelete_ASP.Visible = False
                Me.cmdPrint_ASP.Visible = False
            End If

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
                    Call Proc_DoNew()
                Case "DEL"
                    'STRMENU_TITLE = "Delete Mode"
                    Me.lblMsg.Text = "Sorry!. Unable to get record ..."
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Me.txtFileNum.Text = ""
                    Me.txtPolNum.Text = ""
                    Call Proc_DoNew()
                Case Else
                    'strP_TYPE = "NEW"
                    'STRMENU_TITLE = "New Proposal"
            End Select

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

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim pvURL As String = "prg_li_indv_poly_prem.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
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

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        lblOriginator.Text = ""
        txtUWYear.Text = Year(DateTime.Now)
    End Sub


    Protected Sub txtRetention_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRetention.TextChanged
        If txtRetention.Text <> "" Then
            If IsNumeric(txtRetention.Text) Then
                txtRetention.Text = Format(txtRetention.Text, "Standard")
                txtFreeMedCovLmt.Focus()
            Else
                Me.lblMsg.Text = "Retention must be numeric"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtRetention.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Protected Sub txtFreeMedCovLmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFreeMedCovLmt.TextChanged
        If txtFreeMedCovLmt.Text <> "" Then
            If IsNumeric(txtFreeMedCovLmt.Text) Then
                txtFreeMedCovLmt.Text = Format(txtFreeMedCovLmt.Text, "Standard")
                txtCompShare.Focus()
            Else
                Me.lblMsg.Text = "Free medical cover limit must be numeric"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtFreeMedCovLmt.Focus()
                Exit Sub
            End If
        End If
    End Sub
    Private Sub GetReInsValue()
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
        strSQL = ""
        strSQL = strSQL & "SELECT *"
        strSQL = strSQL & " FROM TBIL_REINSURANCE_SETTINGS"
        strSQL = strSQL & " WHERE YEAR(TBIL_REINS_EFF_DATE) = '" & Year(DateTime.Now) & "'"
        strSQL = strSQL & " AND TBIL_REINS_FLAG <> 'D'"
        strSQL = strSQL & " ORDER BY TBIL_REINS_REC_ID DESC"





        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader


        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then

            Me.txtRetention.Text = Format(RTrim(CType(objOLEDR("TBIL_REINS_RETENTION") & vbNullString, String)), "Standard")
            Me.txtFreeMedCovLmt.Text = Format(RTrim(CType(objOLEDR("TBIL_REINS_MED_COV_LMT") & vbNullString, String)), "Standard")
            Me.txtCompShare.Text = Format(RTrim(CType(objOLEDR("TBIL_REINS_COY_SHARE") & vbNullString, String)), "Standard")
        Else
            objOLECmd.Dispose()
            objOLECmd = Nothing
            objOLEDR.Close()
            objOLEDR = Nothing

            strSQL = ""
            strSQL = strSQL & "SELECT TOP 1 *"
            strSQL = strSQL & " FROM TBIL_REINSURANCE_SETTINGS"
            strSQL = strSQL & " WHERE TBIL_REINS_FLAG <> 'D'"
            strSQL = strSQL & " ORDER BY TBIL_REINS_REC_ID DESC"


            objOLECmd = New OleDbCommand(strSQL, objOLEConn)
            objOLECmd.CommandType = CommandType.Text
            objOLEDR = objOLECmd.ExecuteReader()
            If (objOLEDR.Read()) Then
                Me.txtRetention.Text = Format(RTrim(CType(objOLEDR("TBIL_REINS_RETENTION") & vbNullString, String)), "Standard")
                Me.txtFreeMedCovLmt.Text = Format(RTrim(CType(objOLEDR("TBIL_REINS_MED_COV_LMT") & vbNullString, String)), "Standard")
                Me.txtCompShare.Text = Format(RTrim(CType(objOLEDR("TBIL_REINS_COY_SHARE") & vbNullString, String)), "Standard")
            Else
                Me.txtRetention.Text = "0.00"
                Me.txtFreeMedCovLmt.Text = "0.00"
                Me.txtCompShare.Text = "0.00"
            End If

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
    End Sub



End Class
