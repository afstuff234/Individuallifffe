Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO


Partial Class I_LIFE_PRG_LI_INDV_POLY_ADD_COVER
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
    Protected strP_ID As String
    Protected strQ_ID As String

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

        strTableName = "TBIL_POLICY_ADD_PREM"

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

            Call Proc_DoNew()

            Me.lblMsg.Text = "Status:"
            Me.cmdPrev.Enabled = True
            Me.cmdNext.Enabled = False

            Me.cboProduct.Items.Clear()
            Call DoProc_CreateDataSource("IL_PRODUCT_CAT_LIST", Trim("IND"), Me.cboProductClass)

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
                    'Me.txtPlan_Num.Text = oAL.Item(7)
                    'Me.txtCover_Num.Text = oAL.Item(8)
                    Me.txtDOB.Text = oAL.Item(9)
                    Me.txtDOB_ANB.Text = oAL.Item(10)

                    Me.cmdNext.Enabled = True
                    If UCase(oAL.Item(18).ToString) = "A" Then
                        'Me.cmdNew_ASP.Visible = False
                        'Me.cmdSave_ASP.Visible = False
                        'Me.cmdDelete_ASP.Visible = False
                        'Me.cmdDelItem_ASP.Visible = False
                        Me.cmdPrint_ASP.Visible = False
                    End If

                    Call Proc_DataBind()
                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If

            'Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Trim("101"), Me.cboProduct)
            Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)

            Call gnProc_DDL_Get(Me.cboProductClass, Me.txtProductClass.Text & RTrim("=") & RTrim(Me.txtProductClass.Text))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_MDLE") & vbNullString, String)) & RTrim("=") & RTrim(Me.txtProductClass.Text))
            If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Then
                Call gnProc_DDL_Get(Me.cboProductClass, RTrim("I=") & RTrim(Me.txtProductClass.Text))
            End If
            Call gnProc_DDL_Get(Me.cboProduct, RTrim(Me.txtProduct_Num.Text))

            Call gnProc_Populate_Box("IL_COVER_LIST_OTHERS", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
            Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)
            Call gnProc_Populate_Box("IL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboAdd_Prem_Rate_Code)

            If Trim(strF_ID) <> "" Then
                'Call Proc_OpenRecord(Me.txtNum.Text)
            End If
            'If Trim(strQ_ID) <> "" Then
            '    Me.txtQuote_Num.Text = RTrim(strQ_ID)
            'End If
            'If Trim(strP_ID) <> "" Then
            '    Me.txtPolNum.Text = RTrim(strP_ID)
            'End If

        End If


        If Me.txtAction.Text = "New" Then
            Call Proc_DoNew()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Save" Then
            'Call Proc_DoSave()
            Me.txtAction.Text = ""
        End If

        'If Me.txtAction.Text = "Delete" Then
        'Call DoDelete()
        'Me.txtAction.Text = ""
        'End If

        If Me.txtAction.Text = "Delete_Item" Then
            Call Proc_DoDelItem()
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
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE IN('IND','I')"
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

    Protected Sub DoProc_ProductClass_Change()
        Try
            If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Or _
               Me.cboProductClass.SelectedItem.Value = "" Or Me.cboProductClass.SelectedItem.Value = "*" Then
                'Me.txtProductClass.Text = ""
                'Me.txtProduct_Num.Text = ""
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
                'Me.txtProduct_Num.Text = ""
                Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)
            End If
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub DoProc_Premium_Code_Change()

        Call DoGet_SelectedItem(Me.cboAdd_Prem_Rate_Code, Me.txtAdd_Prem_Rate_Code, Me.txtAdd_Prem_Rate_Name, Me.lblMsg)
        If Trim(Me.txtAdd_Prem_Rate_Code.Text) = "" Then
            Me.txtAdd_Prem_Rate.Text = "0.00"
            Me.txtAdd_Prem_Rate_Per.Text = "0"
        Else
            Dim myRetValue As String = "0"
            myRetValue = MOD_GEN.gnGET_RATE("GET_IL_PREMIUM_RATE", "IND", Me.txtAdd_Prem_Rate_Code.Text, Me.txtProduct_Num.Text, Me.txtAdd_Period_Yr.Text, Me.txtDOB_ANB.Text, Me.lblMsg, Me.txtAdd_Prem_Rate_Per)
            If Left(LTrim(myRetValue), 3) = "ERR" Then
                Me.cboAdd_Prem_Rate_Code.SelectedIndex = -1
                Me.txtAdd_Prem_Rate.Text = "0.00"
                Me.txtAdd_Prem_Rate_Per.Text = "0"
            Else
                Me.txtAdd_Prem_Rate.Text = myRetValue.ToString
            End If
        End If


    End Sub

    Protected Sub DoProc_Premium_RateType_Change()
        Call gnGET_SelectedItem(Me.cboAdd_Prem_Rate_Type, Me.txtAdd_Prem_Rate_TypeNum, Me.txtAdd_Prem_Rate_TypeName, Me.lblMsg)
        Select Case UCase(Me.txtAdd_Prem_Rate_TypeNum.Text)
            Case "A"
                Me.txtAdd_Prem_Fixed_Rate.Enabled = False
                Me.txtAdd_Prem_Fixed_Rate_PerNum.Enabled = False
                Me.cboAdd_Prem_Fixed_Rate_Per.Enabled = False
                Me.txtAdd_Prem_Rate.Enabled = False
                Me.txtAdd_Prem_Rate_Per.Enabled = False
                Me.txtAdd_Prem_Rate_Code.Enabled = False
                Me.cboAdd_Prem_Rate_Code.Enabled = False
                txtAdd_Prem_Rate_Applied_On_Num.Text = "N"
                Call gnProc_DDL_Get(Me.cboAdd_Prem_Rate_Applied_On, RTrim(Me.txtAdd_Prem_Rate_Applied_On_Num.Text))
                Me.cboAdd_Prem_Rate_Applied_On.Enabled = False
                'Me.txtAdd_SA_LC.Enabled = False
                'Me.txtAdd_SA_FC.Enabled = False
            Case "F"
                Me.txtAdd_Prem_Fixed_Rate.Enabled = True
                Me.txtAdd_Prem_Fixed_Rate_PerNum.Enabled = True
                Me.cboAdd_Prem_Fixed_Rate_Per.Enabled = True
                Me.txtAdd_Prem_Rate.Enabled = False
                Me.txtAdd_Prem_Rate_Per.Enabled = False
                Me.txtAdd_Prem_Rate_Code.Enabled = False
                Me.cboAdd_Prem_Rate_Code.Enabled = False
                'txtAdd_Prem_Rate_Applied_On_Num.Text = "N"
                'Call gnProc_DDL_Get(Me.cboAdd_Prem_Rate_Applied_On, RTrim(Me.txtAdd_Prem_Rate_Applied_On_Num.Text))
                Me.cboAdd_Prem_Rate_Applied_On.Enabled = True
                Me.txtAdd_SA_LC.Enabled = True
                Me.txtAdd_SA_FC.Enabled = True
            Case "R"
                Me.txtAdd_Prem_Fixed_Rate.Enabled = False
                Me.txtAdd_Prem_Fixed_Rate_PerNum.Enabled = False
                Me.cboAdd_Prem_Fixed_Rate_Per.Enabled = False
                Me.txtAdd_Prem_Rate.Enabled = True
                Me.txtAdd_Prem_Rate_Per.Enabled = True
                Me.txtAdd_Prem_Rate_Code.Enabled = True
                Me.cboAdd_Prem_Rate_Code.Enabled = True
                'txtAdd_Prem_Rate_Applied_On_Num.Text = "N"
                'Call gnProc_DDL_Get(Me.cboAdd_Prem_Rate_Applied_On, RTrim(Me.txtAdd_Prem_Rate_Applied_On_Num.Text))
                Me.cboAdd_Prem_Rate_Applied_On.Enabled = True
                Me.txtAdd_SA_LC.Enabled = True
                Me.txtAdd_SA_FC.Enabled = True
        End Select

    End Sub
    Protected Sub DoProc_Product_Change()
        Call DoGet_SelectedItem(Me.cboProduct, Me.txtProduct_Num, Me.txtProduct_Name, Me.lblMsg)
        Me.txtCover_Num.Text = ""
        Me.txtCover_Name.Text = ""
        Call gnProc_Populate_Box("IL_COVER_LIST_OTHERS", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
        Me.txtPlan_Num.Text = ""
        Me.txtPlan_Name.Text = ""
        Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)
        Me.txtAdd_Prem_Rate_Code.Text = ""
        Call gnProc_Populate_Box("IL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboAdd_Prem_Rate_Code)

    End Sub


    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True


        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try


        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT *"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_POL_ADD_FILE_NO = '" & RTrim(strF_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_ADD_PROP_NO = '" & RTrim(strQ_ID) & "'"
        strSQL = strSQL & " ORDER BY TBIL_POL_ADD_REC_ID"

        Try

            'Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
            'objOLECmd.CommandType = CommandType.Text
            'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
            'Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)
            'objDA.SelectCommand = objOLECmd

            Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

            Dim objDS As DataSet = New DataSet()
            objDA.Fill(objDS, strTable)

            'Dim objDV As New DataView
            'objDV = objDS.Tables(strTable).DefaultView
            'objDV.Sort = "ACT_REC_NO"
            'Session("myobjDV") = objDV

            'With Me.DataGrid1
            '.DataSource = objDS
            '.DataBind()
            'End With

            With GridView1
                .DataSource = objDS
                .DataBind()
            End With

            'With Me.Repeater1
            '.DataSource = objDS
            '.DataBind()
            'End With

            'objDV.Dispose()
            'objDV = Nothing

            objDS.Dispose()
            objDA.Dispose()

            objDS = Nothing
            objDA = Nothing
            'objOLECmd.Dispose()
            'objOLECmd = Nothing


        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString

        End Try


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        objOLEConn = Nothing

        Me.cmdDelItem_ASP.Enabled = False


        Dim P As Integer = 0
        Dim C As Integer = 0

        C = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            C = C + 1
        Next
        If C >= 1 Then
            'Me.cmdDelete_ASP.Enabled = True
            Me.cmdDelItem_ASP.Enabled = True
        End If

        'C = C + 1
        'Me.txtBenef_SN.Text = C.ToString

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
        strSQL = strSQL & " WHERE TBIL_POL_ADD_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_ADD_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

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


    Protected Sub Proc_DoDelItem()

        Dim blnRet As Boolean = False
        Dim P As Integer = 0, C As Integer
        Dim myKey As String = "", myKeyX As String = ""


        For P = 0 To Me.GridView1.Rows.Count - 1
            If CType(Me.GridView1.Rows(P).FindControl("chkSel"), CheckBox).Checked Then
                ' Get the currently selected row using the SelectedRow property.
                Dim row As GridViewRow = GridView1.Rows(P)
                myKeyX = myKeyX & row.Cells(2).Text
                myKeyX = myKeyX & " / "

                myKey = Me.GridView1.Rows(P).Cells(2).Text
                'Me.txtNum.Text = Me.GridView1.Rows(P).Cells(4).Text


                ' Display the required value from the selected row.
                'Me.txtRecNo.Text = row.Cells(2).Text


                'Insert codes to delete selected/checked item(s)

                If Trim(myKey) <> "" Then
                    Me.txtRecNo.Text = myKey
                    Call Proc_DoDelete_Record()
                    C = C + 1
                End If

            End If

        Next

        If C >= 1 Then
            'Me.cmdDelItem_ASP.Enabled = False
            'Me.cmdDelItem.Enabled = False

            Call Proc_DataBind()

            Call Proc_DoNew()

            Me.lblMsg.Text = "Record deleted successfully." & " No of item(s) deleted: " & CStr(C)
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            'Me.textMessage.Text = ""

            Me.lblMsg.Text = "Deleted Item(s): " & myKeyX

        Else
            Me.lblMsg.Text = "Record not deleted ..."

        End If

        'Me.txtTreatyNum.Enabled = True
        'Me.txtTreatyNum.Focus()

    End Sub

    Protected Sub Proc_DoDelete_Record()

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

        If Trim(Me.txtRecNo.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblRecNo.Text
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


        strREC_ID = Trim(Me.txtFileNum.Text)
        strTable = strTableName

        strSQL = ""
        'Delete record
        '==============================================
        strSQL = ""
        strSQL = "DELETE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POL_ADD_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_ADD_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_ADD_REC_ID = " & Val(RTrim(Me.txtRecNo.Text)) & ""

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try
            With objOLECmd2
                .Connection = objOLEConn
                .CommandType = CommandType.Text
                .CommandText = strSQL
            End With
            intC = objOLECmd2.ExecuteNonQuery()

            If intC >= 1 Then
                'Call Proc_DoNew()
                'Me.lblMsg.Text = "Record deleted successfully."
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Else
                'Me.lblMsg.Text = "Sorry!. Record not deleted..."
                'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
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
                           subctrl.ID = "txtDOB" Or _
                           subctrl.ID = "txtDOB_ANB" Or _
                           subctrl.ID = "txtProductClass" Or _
                           subctrl.ID = "txtProduct_Num" Or _
                           subctrl.ID = "xyz_123" Then
                        Else
                            'Response.Write("<br> Control ID: " & subctrl.ID)
                            CType(subctrl, TextBox).Text = ""
                        End If
                    End If
                    If TypeOf subctrl Is System.Web.UI.WebControls.DropDownList Then
                        If subctrl.ID = "cboProductClass" Or _
                           subctrl.ID = "cboProduct" Or _
                           subctrl.ID = "xyz_123" Then
                        Else
                            CType(subctrl, DropDownList).SelectedIndex = -1
                        End If
                    End If
                Next
            End If
        Next

        'Me.chkFileNum.Enabled = True
        'Me.chkFileNum.Checked = False
        'Me.lblFileNum.Enabled = False
        'Me.txtFileNum.Enabled = False
        'Me.cmdFileNum.Enabled = False

        Me.txtRecNo.Text = "0"

        'Me.cboProductClass.SelectedIndex = -1
        'Me.cboProduct.SelectedIndex = -1
        'Me.cboCover_Name.SelectedIndex = -1
        'Me.cboPlan_Name.SelectedIndex = -1

        'Me.txtProduct_Num.Text = ""

        Me.cmdSave_ASP.Enabled = True
        'Me.cmdDelItem_ASP.Enabled = False
        'Me.cmdNext.Enabled = False

    End Sub

    Private Sub Proc_DoSave()

        'New fields


        '        TBIL_POL_ADD_PREM_RT_AMT_CD	R – Use Rate Table  F – Use Fixed Rate   A – Use Amount
        '								(Char 3)
        'TBIL_POL_ADD_PREM_RT_CD	          	Additional  Premium Table Rate Code  (Char 5)
        'TBIL_POL_ADD_PREM_AMT	Additional  Premium  Direct Amount   (Numeric 2 decimal)
        'TBIL_POL_ADD_PREM_FX_RT	          	Additional  Premium Fixed Rate  (Numeric 4 decimal)
        'TBIL_POL_ADD_PREM_FX_RT_PER          	Additional  Premium Fixed Rate Per (100, 1000)                 (Numeric 4 decimal)
        'TBIL_POL_ADD_RATE_APPLY	          	Apply rate on   S – SA    P – Basic Premium  (Char 3)
        'TBIL_POL_ADD_SA_RT_AMT_CD          	R -  if SA % of Basic SA    A – If SA is Amount (Char 3)
        'TBIL_POL_ADD_PCENT_SA             	% If SA percent of Basic SA   (Numeric 2 decimal)


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

        Dim Dte_Current As Date = Now
        Dim Dte_DOB As Date = Now

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

        If Me.txtDOB.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblDOB.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Val(Me.txtDOB_ANB.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & Me.lblDOB_ANB.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


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

        Call MOD_GEN.gnGET_SelectedItem(Me.cboCover_Name, Me.txtCover_Num, Me.txtCover_Name, Me.lblMsg)
        If Me.txtCover_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblCover_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'If Me.txtPlan_Num.Text = "" Then
        'Me.lblMsg.Text = "Missing " & Me.lblPlan_Num.Text
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        'Exit Sub
        'End If


        Me.txtAdd_Period_Yr.Text = Trim(Me.txtAdd_Period_Yr.Text)
        If Me.txtAdd_Period_Yr.Text = "" Or Val(Me.txtAdd_Period_Yr.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & Me.lblAdd_Period_Yr.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Not IsNumeric(Me.txtAdd_Period_Yr.Text) Then
            Me.lblMsg.Text = "Invalid " & Me.lblAdd_Period_Yr.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        If RTrim(Me.txtAdd_Start_Date.Text) = "" Or Len(Trim(Me.txtAdd_Start_Date.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblAdd_Start_Date.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtAdd_Start_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblAdd_Start_Date.Text & ". Expecting full date in ddmmyyyy format ..."
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
            Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If
        Me.txtAdd_Start_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteStart = Format(mydte, "MM/dd/yyyy")



        dteEnd = DateAdd(DateInterval.Year, Val(Me.txtAdd_Period_Yr.Text), dteStart)
        Me.txtAdd_End_Date.Text = Format(dteEnd, "dd/MM/yyyy")


        If RTrim(Me.txtAdd_End_Date.Text) = "" Or Len(Trim(Me.txtAdd_End_Date.Text)) <> 10 Then
            Me.lblMsg.Text = "Missing or Invalid date - " & Me.lblAdd_End_Date.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'Validate date
        myarrData = Split(Me.txtAdd_End_Date.Text, "/")
        If myarrData.Count <> 3 Then
            Me.lblMsg.Text = "Missing or Invalid " & Me.lblAdd_End_Date.Text & ". Expecting full date in ddmmyyyy format ..."
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
            Me.lblMsg.Text = "Incorrect date. Please enter valid date..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            Exit Sub
        End If
        Me.txtAdd_End_Date.Text = RTrim(strMyDte)
        'mydteX = Mid(Me.txtStartDate.Text, 4, 2) & "/" & Left(Me.txtStartDate.Text, 2) & "/" & Right(Me.txtStartDate.Text, 4)
        mydteX = Trim(strMyMth) & "/" & Trim(strMyDay) & "/" & Trim(strMyYear)
        mydte = Format(CDate(mydteX), "MM/dd/yyyy")
        dteEnd = Format(mydte, "MM/dd/yyyy")


        Me.txtAdd_No_Prem_Yr.Text = Trim(Me.txtAdd_No_Prem_Yr.Text)
        If Me.txtAdd_No_Prem_Yr.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblAdd_No_Prem_Yr.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Not IsNumeric(Me.txtAdd_No_Prem_Yr.Text) Then
            Me.lblMsg.Text = "Invalid " & Me.lblAdd_No_Prem_Yr.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        Call gnGET_SelectedItem(Me.cboAdd_Prem_Rate_Type, Me.txtAdd_Prem_Rate_TypeNum, Me.txtAdd_Prem_Rate_TypeName, Me.lblMsg)
        If Me.txtAdd_Prem_Rate_TypeNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblAdd_Prem_Rate_Type.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call MOD_GEN.gnInitialize_Numeric(Me.txtAdd_Prem_Amt)
        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "A" And Val(Me.txtAdd_Prem_Amt.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & "Additional Amount. Please enter valid additional amount..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "A" Then
            Me.txtAdd_Prem_Fixed_Rate.Text = "0"
            Me.txtAdd_Prem_Fixed_Rate_PerNum.Text = "0"
            Me.txtAdd_Prem_Rate.Text = "0"
            Me.txtAdd_Prem_Rate_Per.Text = "0"
            Me.txtAdd_Prem_Rate_Code.Text = ""
            txtAdd_Prem_Rate_Applied_On_Num.Text = "N"
            Call gnProc_DDL_Get(Me.cboAdd_Prem_Rate_Applied_On, RTrim(Me.txtAdd_Prem_Rate_Applied_On_Num.Text))
            'Me.txtAdd_SA_LC.Text = "0"
            'Me.txtAdd_SA_FC.Text = "0"

        End If
        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "F" Then
            Me.txtAdd_Prem_Rate.Text = "0"
            Me.txtAdd_Prem_Rate_Per.Text = "0"
            Me.txtAdd_Prem_Rate_Code.Text = ""
        End If
        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "R" Then
            Me.txtAdd_Prem_Fixed_Rate.Text = "0"
            Me.txtAdd_Prem_Fixed_Rate_PerNum.Text = "0"
        End If

        Call MOD_GEN.gnInitialize_Numeric(Me.txtAdd_Prem_Fixed_Rate)
        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "F" And Val(Me.txtAdd_Prem_Fixed_Rate.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & "Fixed Rate. Please enter valid fixed rate..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call gnGET_SelectedItem(Me.cboAdd_Prem_Fixed_Rate_Per, Me.txtAdd_Prem_Fixed_Rate_PerNum, Me.txtAdd_Prem_Fixed_Rate_PerName, Me.lblMsg)
        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "F" And Me.txtAdd_Prem_Fixed_Rate_PerNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblAdd_Prem_Fixed_Rate_Per.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "F" Or Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "R" Then
            Call gnGET_SelectedItem(Me.cboAdd_Prem_Rate_Applied_On, Me.txtAdd_Prem_Rate_Applied_On_Num, Me.txtAdd_Prem_Rate_Applied_On_Name, Me.lblMsg)
            If Me.txtAdd_Prem_Rate_Applied_On_Num.Text = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblAdd_Prem_Rate_Applied_On.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        End If


        Me.txtAdd_SA_LC.Text = Trim(Me.txtAdd_SA_LC.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtAdd_SA_LC)
        If Val(Me.txtAdd_SA_LC.Text) = 0 Then
            'Me.lblMsg.Text = "Missing " & Me.lblAdd_SA_LC.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
        End If


        Me.txtAdd_SA_FC.Text = Me.txtAdd_SA_LC.Text
        Call MOD_GEN.gnInitialize_Numeric(Me.txtAdd_SA_FC)



        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "R" Then
            Call DoProc_Premium_Code_Change()
            'Call MOD_GEN.gnGET_SelectedItem(Me.cboAdd_Prem_Rate_Code, Me.txtAdd_Prem_Rate_Code, Me.txtAdd_Prem_Rate_Name, Me.lblMsg)
            If Trim(Me.txtAdd_Prem_Rate_Code.Text) = "" Then
                Me.lblMsg.Text = "Missing " & Me.lblAdd_Prem_Rate_Code.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        End If

        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "A" Or Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "R" Then
            Me.txtAdd_Prem_Fixed_Rate_PerNum.Text = "0"
        End If

        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "R" And Val(Me.txtAdd_Prem_Rate.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & "Missing Premium Rate. Please select valid premium rate code..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If Trim(Me.txtAdd_Prem_Rate_TypeNum.Text) = "R" And Val(Me.txtAdd_Prem_Rate_Per.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & "Missing Premium Rate per e.g 100 or 1000 from rate table. Please select valid premium rate code..."
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
        strSQL = strSQL & " WHERE TBIL_POL_ADD_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        'strSQL = strSQL & " AND TBIL_POL_ADD_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        'If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
        strSQL = strSQL & " AND TBIL_POL_ADD_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
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

                drNewRow("TBIL_POL_ADD_MDLE") = "I"

                drNewRow("TBIL_POL_ADD_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_POL_ADD_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                'drNewRow("TBIL_POL_ADD_POLY_NO") = RTrim(Me.txtPolNum.Text)

                drNewRow("TBIL_POL_ADD_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)
                drNewRow("TBIL_POL_ADD_COVER_CD") = RTrim(Me.txtCover_Num.Text)

                If Trim(Me.txtAdd_Start_Date.Text) <> "" Then
                    drNewRow("TBIL_POL_ADD_DT_FROM") = dteStart
                End If
                If Trim(Me.txtAdd_End_Date.Text) <> "" Then
                    drNewRow("TBIL_POL_ADD_DT_TO") = dteEnd
                End If

                drNewRow("TBIL_POL_ADD_PERIOD") = Val(Me.txtAdd_Period_Yr.Text)
                drNewRow("TBIL_POL_ADD_PREM_PAY_YRS") = Val(Me.txtAdd_No_Prem_Yr.Text)

                drNewRow("TBIL_POL_ADD_PREM_RT_AMT_CD") = RTrim(Me.txtAdd_Prem_Rate_TypeNum.Text)
                drNewRow("TBIL_POL_ADD_PREM_AMT") = RTrim(Me.txtAdd_Prem_Amt.Text)
                drNewRow("TBIL_POL_ADD_PREM_FX_RT") = RTrim(Me.txtAdd_Prem_Fixed_Rate.Text)
                drNewRow("TBIL_POL_ADD_PREM_FX_RT_PER") = RTrim(Me.txtAdd_Prem_Fixed_Rate_PerNum.Text)

                drNewRow("TBIL_POL_ADD_SA_LC") = RTrim(Me.txtAdd_SA_LC.Text)
                drNewRow("TBIL_POL_ADD_SA_FC") = RTrim(Me.txtAdd_SA_FC.Text)

                drNewRow("TBIL_POL_ADD_PREM_RT_CD") = RTrim(Me.txtAdd_Prem_Rate_Code.Text)
                drNewRow("TBIL_POL_ADD_RATE") = Trim(Me.txtAdd_Prem_Rate.Text)
                drNewRow("TBIL_POL_ADD_RATE_PER") = Trim(Me.txtAdd_Prem_Rate_Per.Text)


                drNewRow("TBIL_POL_ADD_RATE_APPLY") = Trim(Me.txtAdd_Prem_Rate_Applied_On_Num.Text)
                'drNewRow("TBIL_POL_ADD_SA_RT_AMT_CD") = RTrim(Me.txtAdd_Prem_Rate_Code.Text)

                drNewRow("TBIL_POL_ADD_FLAG") = "A"
                drNewRow("TBIL_POL_ADD_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_POL_ADD_KEYDTE") = Now

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
                    .Rows(0)("TBIL_POL_ADD_MDLE") = "I"

                    .Rows(0)("TBIL_POL_ADD_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_POL_ADD_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    '.Rows(0)("TBIL_POL_ADD_POLY_NO") = RTrim(Me.txtPolNum.Text)

                    .Rows(0)("TBIL_POL_ADD_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)
                    .Rows(0)("TBIL_POL_ADD_COVER_CD") = RTrim(Me.txtCover_Num.Text)

                    If Trim(Me.txtAdd_Start_Date.Text) <> "" Then
                        .Rows(0)("TBIL_POL_ADD_DT_FROM") = dteStart
                    End If
                    If Trim(Me.txtAdd_End_Date.Text) <> "" Then
                        .Rows(0)("TBIL_POL_ADD_DT_TO") = dteEnd
                    End If

                    .Rows(0)("TBIL_POL_ADD_PERIOD") = Val(Me.txtAdd_Period_Yr.Text)
                    .Rows(0)("TBIL_POL_ADD_PREM_PAY_YRS") = Val(Me.txtAdd_No_Prem_Yr.Text)

                    .Rows(0)("TBIL_POL_ADD_PREM_RT_AMT_CD") = RTrim(Me.txtAdd_Prem_Rate_TypeNum.Text)
                    .Rows(0)("TBIL_POL_ADD_PREM_AMT") = RTrim(Me.txtAdd_Prem_Amt.Text)
                    .Rows(0)("TBIL_POL_ADD_PREM_FX_RT") = RTrim(Me.txtAdd_Prem_Fixed_Rate.Text)
                    .Rows(0)("TBIL_POL_ADD_PREM_FX_RT_PER") = RTrim(Me.txtAdd_Prem_Fixed_Rate_PerNum.Text)

                    .Rows(0)("TBIL_POL_ADD_SA_LC") = RTrim(Me.txtAdd_SA_LC.Text)
                    .Rows(0)("TBIL_POL_ADD_SA_FC") = RTrim(Me.txtAdd_SA_FC.Text)

                    .Rows(0)("TBIL_POL_ADD_PREM_RT_CD") = RTrim(Me.txtAdd_Prem_Rate_Code.Text)
                    .Rows(0)("TBIL_POL_ADD_RATE") = Trim(Me.txtAdd_Prem_Rate.Text)
                    .Rows(0)("TBIL_POL_ADD_RATE_PER") = Trim(Me.txtAdd_Prem_Rate_Per.Text)

                    .Rows(0)("TBIL_POL_ADD_RATE_APPLY") = Trim(Me.txtAdd_Prem_Rate_Applied_On_Num.Text)
                    '.Rows(0)("TBIL_POL_ADD_SA_RT_AMT_CD") = RTrim(Me.txtAdd_Prem_Rate_Code.Text)

                    .Rows(0)("TBIL_POL_ADD_FLAG") = "C"
                    '.Rows(0)("TBIL_POL_ADD_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_POL_ADD_KEYDTE") = Now
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

        Call Proc_DataBind()
        Call Proc_DoNew()


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
        strSQL = strSQL & "SELECT TOP 1 ADD_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS ADD_TBL"
        strSQL = strSQL & " WHERE ADD_TBL.TBIL_POL_ADD_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND ADD_TBL.TBIL_POL_ADD_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPIL_GET_POLICY_ADD_PREM"

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

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_POLY_NO") & vbNullString, String))

            Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim(Me.txtProductClass.Text))
            Call gnProc_DDL_Get(Me.cboProductClass, RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_MDLE") & vbNullString, String)) & RTrim("=") & RTrim(Me.txtProductClass.Text))
            'Call gnProc_DDL_Get(Me.cboProductClass, RTrim("IND=") & RTrim(Me.txtProductClass.Text))
            If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Then
                Call gnProc_DDL_Get(Me.cboProductClass, RTrim("I=") & RTrim(Me.txtProductClass.Text))
            End If

            'Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)
            Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PRDCT_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboProduct, RTrim(Me.txtProduct_Num.Text))
            'Me.txtProduct_Name.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DESC") & vbNullString, String))

            'Call gnProc_Populate_Box("IL_COVER_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
            Call gnProc_Populate_Box("IL_COVER_LIST_OTHERS", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)
            Call gnProc_Populate_Box("IL_PLAN_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboPlan_Name)

            Me.txtCover_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_COVER_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboCover_Name, RTrim(Me.txtCover_Num.Text))

            'Me.txtPlan_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PLAN_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboPlan_Name, RTrim(Me.txtPlan_Num.Text))

            If IsDate(objOLEDR("TBIL_POL_ADD_DT_FROM")) Then
                Me.txtAdd_Start_Date.Text = Format(CType(objOLEDR("TBIL_POL_ADD_DT_FROM"), DateTime), "dd/MM/yyyy")
            End If
            If IsDate(objOLEDR("TBIL_POL_ADD_DT_TO")) Then
                Me.txtAdd_End_Date.Text = Format(CType(objOLEDR("TBIL_POL_ADD_DT_TO"), DateTime), "dd/MM/yyyy")
            End If

            Me.txtAdd_Period_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PERIOD") & vbNullString, String))
            Me.txtAdd_No_Prem_Yr.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PREM_PAY_YRS") & vbNullString, String))


            Me.txtAdd_Prem_Rate_TypeNum.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PREM_RT_AMT_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboAdd_Prem_Rate_Type, RTrim(Me.txtAdd_Prem_Rate_TypeNum.Text))
            Me.txtAdd_Prem_Amt.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PREM_AMT") & vbNullString, String))
            Me.txtAdd_Prem_Fixed_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT") & vbNullString, String))
            Me.txtAdd_Prem_Fixed_Rate_PerNum.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PREM_FX_RT_PER") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboAdd_Prem_Fixed_Rate_Per, RTrim(Me.txtAdd_Prem_Fixed_Rate_PerNum.Text))

            Me.txtAdd_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_SA_LC") & vbNullString, String))
            Me.txtAdd_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_SA_FC") & vbNullString, String))

            Call gnProc_Populate_Box("IL_RATE_TYPE_CODE_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboAdd_Prem_Rate_Code)
            Me.txtAdd_Prem_Rate_Code.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_PREM_RT_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboAdd_Prem_Rate_Code, RTrim(Me.txtAdd_Prem_Rate_Code.Text))
            Me.txtAdd_Prem_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_RATE") & vbNullString, String))
            Me.txtAdd_Prem_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_RATE_PER") & vbNullString, String))

            Me.txtAdd_Prem_Rate_Applied_On_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_RATE_APPLY") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboAdd_Prem_Rate_Applied_On, RTrim(Me.txtAdd_Prem_Rate_Applied_On_Num.Text))
            'Me.txtAdd_Prem_Rate_Code.Text = RTrim(CType(objOLEDR("TBIL_POL_ADD_SA_RT_AMT_CD") & vbNullString, String))
            lblOriginator.Text = "Record Originally created by " & GetRecordOriginator(RTrim(CType(objOLEDR("TBIL_POL_ADD_OPERID") & vbNullString, String)))

            Me.lblFileNum.Enabled = False
            'Call DisableBox(Me.txtFileNum)
            'Me.chkFileNum.Enabled = False
            Me.txtFileNum.Enabled = False
            Me.txtQuote_Num.Enabled = False
            Me.txtPolNum.Enabled = False

            Me.cmdNew_ASP.Enabled = True
            'Me.cmdDelete_ASP.Enabled = True
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

            'Me.cmdDelete_ASP.Enabled = False
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

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        lblMsg.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(2).Text

        'Me.txtGroupNum.Text = row.Cells(3).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))

        'Me.txtNum.Text = row.Cells(4).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtNum.Text))

        strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, Val(RTrim(Me.txtRecNo.Text)))

        lblMsg.Text = "You selected " & Me.txtFileNum.Text & " / " & Me.txtRecNo.Text & "."

    End Sub


    'Private Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting

    'End Sub

    'Private Sub GridView1_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Sorted
    '    ' Display the sort expression and sort direction.
    '    Me.lblMessage.Text = "Sorting by " & _
    '      GridView1.SortExpression.ToString() & " in " & GridView1.SortDirection.ToString() & " order."

    'End Sub


    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        Dim pvURL As String = "prg_li_indv_poly_benefry.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim pvURL As String = "prg_li_indv_poly_medic_info.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

End Class
