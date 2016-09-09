Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class I_LIFE_PRG_LI_INDV_POLY_LOAD_DISC
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

        strTableName = "TBIL_POLICY_DISCT_LOAD"

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

            Me.cboLoad_Disc_Num.Items.Clear()
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

                    Me.cmdNext.Enabled = True

                    Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)

                    Call gnProc_DDL_Get(Me.cboProductClass, RTrim("IND=") & RTrim(Me.txtProductClass.Text))
                    If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Then
                        Call gnProc_DDL_Get(Me.cboProductClass, RTrim("I=") & RTrim(Me.txtProductClass.Text))
                    End If
                    Call gnProc_DDL_Get(Me.cboProduct, RTrim(Me.txtProduct_Num.Text))

                    If UCase(oAL.Item(18).ToString) = "A" Then
                        'Me.cmdNew_ASP.Visible = False
                        'Me.cmdSave_ASP.Visible = False
                        ''Me.cmdDelete_ASP.Visible = False
                        'Me.cmdDelItem_ASP.Visible = False
                        Me.cmdPrint_ASP.Visible = False
                    End If

                    Call Proc_DataBind()
                    GetSumAssured()
                Else
                    '    'Destroy i.e remove the array list object from memory
                    '    Response.Write("<br/>Status: " & oAL.Item(0))
                    Me.lblMsg.Text = "Status: " & oAL.Item(1)
                End If
                oAL = Nothing
            End If

            Call gnProc_Populate_Box("IL_COVER_LIST", RTrim(Me.txtProduct_Num.Text), Me.cboCover_Name)

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

    Protected Sub DoProc_Load_Disc_Type_Change()
        Call DoGet_SelectedItem(Me.cboLoad_Disc_Type, Me.txtLoad_Disc_Type, Me.txtLoad_Disc_TypeName, Me.lblMsg)

        If Trim(Me.txtLoad_Disc_Type.Text) = "L" Then
            Call gnProc_Populate_Box("IL_CODE_LIST_UND", "004", Me.cboLoad_Disc_Num)
        ElseIf Trim(Me.txtLoad_Disc_Type.Text) = "D" Then
            Call gnProc_Populate_Box("IL_CODE_LIST_UND", "005", Me.cboLoad_Disc_Num)
        Else
            Me.cboLoad_Disc_Num.Items.Clear()
        End If
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


        'Try

        strREC_ID = RTrim(strF_ID)
        strSQL = "SPIL_GET_POLICY_DISCT_LOAD"

        'Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        ''objOLECmd.CommandType = CommandType.Text
        'objOLECmd.CommandType = CommandType.StoredProcedure

        'objOLECmd.Parameters.Clear()
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = "LST"
        'objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 50).Value = strREC_ID
        'objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 50).Value = strQ_ID


        ''Dim objDA As OleDbDataAdapter = New OleDbDataAdapter()
        ''objDA.SelectCommand = objOLECmd

        'Dim objDR As OleDbDataReader = objOLECmd.ExecuteReader()
        'Me.DataGrid1.DataSource = objDR
        'Me.DataGrid1.DataBind()

        'objDR.Close()
        'objDR = Nothing

        'objOLECmd.Dispose()
        'objOLECmd = Nothing



        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT PT.*"
        strSQL = strSQL & " FROM " & strTable & "  AS PT "
        strSQL = strSQL & " WHERE TBIL_POL_DISC_FILE_NO = '" & RTrim(strF_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_DISC_PROP_NO = '" & RTrim(strQ_ID) & "'"
        strSQL = strSQL & " ORDER BY TBIL_POL_DISC_REC_ID"


        strSQL = "SPIL_GET_POLICY_DISCT_LOAD"


        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

        'objDA.SelectCommand.CommandType = CommandType.Text

        objDA.SelectCommand.CommandType = CommandType.StoredProcedure
        objDA.SelectCommand.Parameters.Clear()

        'objDA.SelectCommand.Parameters.Add(New SqlParameter("@CategoryName", SqlDbType.NVarChar, 15))
        'objDA.SelectCommand.Parameters("@CategoryName").Value = SelectCategory.Value
        'objDA.SelectCommand.Parameters.Add(New SqlParameter("@OrdYear", SqlDbType.NVarChar, 4))
        'objDA.SelectCommand.Parameters("@OrdYear").Value = SelectYear.Value

        objDA.SelectCommand.Parameters.Add("p01", OleDbType.VarChar, 50).Value = "LST"
        objDA.SelectCommand.Parameters.Add("p02", OleDbType.VarChar, 50).Value = strREC_ID
        objDA.SelectCommand.Parameters.Add("p03", OleDbType.VarChar, 50).Value = strQ_ID


        'Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        'm_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim objDS As DataSet = New DataSet()
        'objDA.Fill(objDS)
        objDA.Fill(objDS, strTable)


        'Dim objDV As New DataView
        'objDV = objDS.Tables(strTable).DefaultView
        'objDV.Sort = "ACT_REC_NO"
        'Session("myobjDV") = objDV

        'With Me.DataGrid1
        '.DataSource = objDS
        ''.DataSource = 'objDV
        '.DataBind()
        'End With

        With GridView1
            ''.DataSource = 'objDV
            .DataSource = objDS
            '.DataSource = objDS.Tables(strTable).DefaultView
            .DataBind()
        End With

        'With Me.Repeater1
        '.DataSource = objDS
        '.DataBind()
        'End With

        'objDV.Dispose()
        'objDV = Nothing

        'm_cbCommandBuilder.Dispose()
        'm_cbCommandBuilder = Nothing

        objDS.Dispose()

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()

        objDS = Nothing
        objDA = Nothing


        'Catch ex As Exception
        'Me.lblMsg.Text = ex.Message.ToString

        'End Try


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
        strSQL = strSQL & " WHERE TBIL_POL_DISC_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_DISC_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

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
        strSQL = strSQL & " WHERE TBIL_POL_DISC_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_POL_DISC_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_POL_DISC_REC_ID = " & Val(RTrim(Me.txtRecNo.Text)) & ""

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

        If Me.txtProductClass.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProductClass.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

      

        Me.txtLoad_Disc_SN.Text = Trim(Me.txtLoad_Disc_SN.Text)


        Call MOD_GEN.gnGET_SelectedItem(Me.cboLoad_Disc_Type, Me.txtLoad_Disc_Type, Me.txtLoad_Disc_TypeName, Me.lblMsg)
        If Me.txtLoad_Disc_Type.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblLoad_Disc_Type.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call MOD_GEN.gnGET_SelectedItem(Me.cboLoad_Disc_Prem_Rate_Type, Me.txtLoad_Disc_Prem_Rate_TypeNum, Me.txtLoad_Disc_Prem_Rate_TypeName, Me.lblMsg)
        If Me.txtLoad_Disc_Prem_Rate_TypeNum.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblLoad_Disc_Prem_Rate_Type.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Me.txtLoad_Disc_Amt.Text = Trim(Me.txtLoad_Disc_Amt.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtLoad_Disc_Amt)
        If Trim(Me.txtLoad_Disc_Prem_Rate_TypeNum.Text) = "A" And Val(Me.txtLoad_Disc_Amt.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & "Loading or Discount Amount. Please enter valid amount..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtLoad_Disc_Amt.Focus()
            Exit Sub
        End If

        Me.txtLoad_Disc_Percent.Text = Trim(Me.txtLoad_Disc_Percent.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtLoad_Disc_Percent)
        'If Trim(Me.txtLoad_Disc_Prem_Rate_TypeNum.Text) = "F" And Val(Me.txtLoad_Disc_Percent.Text) = 0 Then
        '    Me.lblMsg.Text = "Missing " & "fixed rate or percentage. Please enter valid percentage..."
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If



       


        Me.txtLoad_Disc_SA_LC.Text = Trim(Me.txtLoad_Disc_SA_LC.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtLoad_Disc_SA_LC)

        Me.txtLoad_Disc_SA_FC.Text = Trim(Me.txtLoad_Disc_SA_LC.Text)
        Me.txtLoad_Disc_SA_FC.Text = Trim(Me.txtLoad_Disc_SA_FC.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtLoad_Disc_SA_FC)



        Call MOD_GEN.gnGET_SelectedItem(Me.cboCover_Name, Me.txtCover_Num, Me.txtCover_Name, Me.lblMsg)
        If Me.txtCover_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblCover_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Me.cboCover_Name.Focus()
            Exit Sub
        End If

        Me.txtCover_Rate.Text = Trim(Me.txtCover_Rate.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtCover_Rate)

        'If Trim(Me.txtLoad_Disc_Prem_Rate_TypeNum.Text) = "R" And Val(Me.txtCover_Rate.Text) = 0 Then
        '    Me.lblMsg.Text = "Missing " & "premium rate or percentage. Please enter valid percentage..."
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If

        If Trim(Me.cboLoad_Disc_Prem_Rate_Type.SelectedValue) = "R" And Val(Me.txtCover_Rate.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & "premium rate. Please enter valid rate..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Me.txtCover_Rate.Focus()
            Exit Sub
        End If

        Call MOD_GEN.gnGET_SelectedItem(Me.cboLoad_Disc_Num, Me.txtLoad_Disc_Num, Me.txtLoad_Disc_NumName, Me.lblMsg)
        If Trim(Me.cboLoad_Disc_Prem_Rate_Type.SelectedValue) = "R" And Me.txtLoad_Disc_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProduct_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtProduct_Num.Focus()
            Exit Sub
        End If

        Call MOD_GEN.gnGET_SelectedItem(Me.cboLoad_Disc_Applied_On, Me.txtLoad_Disc_Applied_On, Me.txtLoad_Disc_Applied_OnName, Me.lblMsg)
        If (Trim(Me.cboLoad_Disc_Prem_Rate_Type.SelectedValue) = "R" Or Trim(Me.cboLoad_Disc_Prem_Rate_Type.SelectedValue) = "F") And Me.txtLoad_Disc_Applied_On.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblLoad_Disc_Applied_On.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        If Trim(Me.cboLoad_Disc_Prem_Rate_Type.SelectedValue) = "F" And Val(Me.txtLoad_Disc_Percent.Text) = 0 Then
            Me.lblMsg.Text = "Missing " & "premium percentage. Please enter valid percentage..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Me.txtLoad_Disc_Percent.Focus()
            Exit Sub
        End If


        Call MOD_GEN.gnGET_SelectedItem(Me.cboCover_Rate_Per, Me.txtCover_Rate_Per, Me.txtCover_Rate_PerName, Me.lblMsg)
        If (Trim(Me.cboLoad_Disc_Prem_Rate_Type.SelectedValue) = "R" Or Trim(Me.cboLoad_Disc_Prem_Rate_Type.SelectedValue) = "F") And Me.txtCover_Rate_Per.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblCover_Rate_Per.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        Else
            txtCover_Rate_Per.Text = 100
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
        strSQL = strSQL & " WHERE TBIL_POL_DISC_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        'strSQL = strSQL & " AND TBIL_POL_DISC_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        'If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
        strSQL = strSQL & " AND TBIL_POL_DISC_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
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

                drNewRow("TBIL_POL_DISC_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_POL_DISC_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                'drNewRow("TBIL_POL_DISC_POLY_NO") = RTrim(Me.txtPolNum.Text)

                drNewRow("TBIL_POL_DISC_SNO") = Val(Me.txtLoad_Disc_SN.Text)

                drNewRow("TBIL_POL_DISC_LOAD_TYPE") = RTrim(Me.txtLoad_Disc_Type.Text)
                drNewRow("TBIL_POL_DISC_LOAD_CD") = RTrim(Me.txtLoad_Disc_Num.Text)
                drNewRow("TBIL_POL_DISC_LOAD_RT_AMT_CD") = RTrim(Me.txtLoad_Disc_Prem_Rate_TypeNum.Text)
                drNewRow("TBIL_POL_DISC_LOAD_PCENT") = Val(Me.txtLoad_Disc_Percent.Text)

                drNewRow("TBIL_POL_DISC_LOAD_PREM_AMT") = Val(Me.txtLoad_Disc_Amt.Text)

                drNewRow("TBIL_POL_DISC_APPLIED_ON") = RTrim(Me.txtLoad_Disc_Applied_On.Text)

                drNewRow("TBIL_POL_DISC_COVER_CD") = RTrim(Me.txtCover_Num.Text)
                drNewRow("TBIL_POL_DISC_RATE") = Val(Me.txtCover_Rate.Text)
                drNewRow("TBIL_POL_DISC_RATE_PER") = RTrim(Me.txtCover_Rate_Per.Text)

                drNewRow("TBIL_POL_DISC_LOAD_VALUE_LC") = Val(Trim(Me.txtLoad_Disc_SA_LC.Text))
                drNewRow("TBIL_POL_DISC_LOAD_VALUE_FC") = Val(Trim(Me.txtLoad_Disc_SA_FC.Text))


                drNewRow("TBIL_POL_DISC_LOAD_FLAG") = "A"
                drNewRow("TBIL_POL_DISC_LOAD_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_POL_DISC_LOAD_KEYDTE") = Now

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
                    .Rows(0)("TBIL_POL_DISC_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_POL_DISC_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    '.Rows(0)("TBIL_POL_DISC_POLY_NO") = RTrim(Me.txtPolNum.Text)

                    .Rows(0)("TBIL_POL_DISC_SNO") = Val(Me.txtLoad_Disc_SN.Text)

                    .Rows(0)("TBIL_POL_DISC_LOAD_TYPE") = RTrim(Me.txtLoad_Disc_Type.Text)
                    .Rows(0)("TBIL_POL_DISC_LOAD_CD") = RTrim(Me.txtLoad_Disc_Num.Text)
                    .Rows(0)("TBIL_POL_DISC_LOAD_RT_AMT_CD") = RTrim(Me.txtLoad_Disc_Prem_Rate_TypeNum.Text)
                    .Rows(0)("TBIL_POL_DISC_LOAD_PCENT") = Val(Me.txtLoad_Disc_Percent.Text)

                    .Rows(0)("TBIL_POL_DISC_LOAD_PREM_AMT") = Val(Me.txtLoad_Disc_Amt.Text)

                    .Rows(0)("TBIL_POL_DISC_APPLIED_ON") = RTrim(Me.txtLoad_Disc_Applied_On.Text)

                    .Rows(0)("TBIL_POL_DISC_COVER_CD") = RTrim(Me.txtCover_Num.Text)
                    .Rows(0)("TBIL_POL_DISC_RATE") = Val(Me.txtCover_Rate.Text)

                    .Rows(0)("TBIL_POL_DISC_RATE_PER") = RTrim(Me.txtCover_Rate_Per.Text)

                    .Rows(0)("TBIL_POL_DISC_LOAD_VALUE_LC") = Val(Trim(Me.txtLoad_Disc_SA_LC.Text))
                    .Rows(0)("TBIL_POL_DISC_LOAD_VALUE_FC") = Val(Trim(Me.txtLoad_Disc_SA_LC.Text))

                    .Rows(0)("TBIL_POL_DISC_LOAD_FLAG") = "C"
                    '.Rows(0)("TBIL_POL_DISC_LOAD_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_POL_DISC_LOAD_KEYDTE") = Now

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
        strSQL = strSQL & "SELECT TOP 1 DL_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS DL_TBL"
        strSQL = strSQL & " WHERE DL_TBL.TBIL_POL_DISC_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND DL_TBL.TBIL_POL_DISC_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPIL_GET_POLICY_DISCT_LOAD"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        'objOLECmd.CommandType = CommandType.Text
        objOLECmd.CommandType = CommandType.StoredProcedure
        objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 30).Value = strREC_ID
        objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 20).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_POLY_NO") & vbNullString, String))

            Me.txtLoad_Disc_SN.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_SNO") & vbNullString, String))

            Me.txtLoad_Disc_Type.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_LOAD_TYPE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboLoad_Disc_Type, RTrim(Me.txtLoad_Disc_Type.Text))

            Me.txtLoad_Disc_Prem_Rate_TypeNum.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_LOAD_RT_AMT_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboLoad_Disc_Prem_Rate_Type, RTrim(Me.txtLoad_Disc_Prem_Rate_TypeNum.Text))

            Me.txtLoad_Disc_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_LOAD_CD") & vbNullString, String))
            If Trim(Me.txtLoad_Disc_Type.Text) = "L" Then
                Call gnProc_Populate_Box("IL_CODE_LIST_UND", "004", Me.cboLoad_Disc_Num)
            ElseIf Trim(Me.txtLoad_Disc_Type.Text) = "D" Then
                Call gnProc_Populate_Box("IL_CODE_LIST_UND", "005", Me.cboLoad_Disc_Num)
            Else
                'Me.cboLoad_Disc_Num.Items.Clear()
            End If
            Call gnProc_DDL_Get(Me.cboLoad_Disc_Num, RTrim(Me.txtLoad_Disc_Num.Text))


            Me.txtLoad_Disc_Percent.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_LOAD_PCENT") & vbNullString, String))

            Me.txtLoad_Disc_Amt.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_LOAD_PREM_AMT") & vbNullString, String))

            Me.txtLoad_Disc_Applied_On.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_APPLIED_ON") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboLoad_Disc_Applied_On, RTrim(Me.txtLoad_Disc_Applied_On.Text))

            Me.txtCover_Num.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_COVER_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboCover_Name, RTrim(Me.txtCover_Num.Text))
            Me.txtCover_Rate.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_RATE") & vbNullString, String))

            Me.txtCover_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_RATE_PER") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboCover_Rate_Per, RTrim(Me.txtCover_Rate_Per.Text))


            Me.txtLoad_Disc_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_LOAD_VALUE_LC") & vbNullString, String))
            Me.txtLoad_Disc_SA_FC.Text = RTrim(CType(objOLEDR("TBIL_POL_DISC_LOAD_VALUE_FC") & vbNullString, String))
            lblOriginator.Text = "Record Originally created by " & GetRecordOriginator(RTrim(CType(objOLEDR("TBIL_POL_DISC_LOAD_OPERID") & vbNullString, String)))


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
        Dim pvURL As String = "prg_li_indv_poly_charges.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        '
        Dim pvURL As String = "prg_li_indv_poly_prem_calc.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

    Private Sub GetSumAssured()
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
        strSQL = "SELECT * FROM TBIL_POLICY_PREM_INFO WHERE TBIL_POL_PRM_FILE_NO='" + txtFileNum.Text + "'"

        Try
            Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

            objOLECmd.CommandType = CommandType.Text
            Dim objOLEDR As OleDbDataReader
            objOLEDR = objOLECmd.ExecuteReader()
            If (objOLEDR.Read()) Then
                Me.txtLoad_Disc_SA_LC.Text = Format(objOLEDR("TBIL_POL_PRM_SA_LC"), "Standard")
                Me.txtLoad_Disc_SA_FC.Text = Format(objOLEDR("TBIL_POL_PRM_SA_LC"), "Standard")
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
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        DoNew()
    End Sub

    Private Sub DoNew()
        cboLoad_Disc_Type.SelectedIndex = -1
        txtLoad_Disc_Type.Text = ""
        txtLoad_Disc_TypeName.Text = ""
        cboLoad_Disc_Prem_Rate_Type.SelectedIndex = -1
        txtLoad_Disc_Prem_Rate_TypeNum.Text = ""
        txtLoad_Disc_Prem_Rate_TypeName.Text = ""
        txtLoad_Disc_Percent.Text = ""
        txtLoad_Disc_Amt.Text = ""
        cboLoad_Disc_Num.SelectedIndex = -1
        txtLoad_Disc_Num.Text = ""
        txtLoad_Disc_NumName.Text = ""
        cboLoad_Disc_Applied_On.SelectedIndex = -1
        txtLoad_Disc_Applied_On.Text = ""
        txtLoad_Disc_Applied_OnName.Text = ""
        cboCover_Name.SelectedIndex = -1
        txtCover_Name.Text = ""
        txtCover_Num.Text = ""
        txtCover_Rate.Text = ""
        cboCover_Rate_Per.SelectedIndex = -1
        txtCover_Rate_Per.Text = ""
        txtCover_Rate_PerName.Text = ""
    End Sub

    Protected Sub cboLoad_Disc_Prem_Rate_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLoad_Disc_Prem_Rate_Type.SelectedIndexChanged
        If cboLoad_Disc_Prem_Rate_Type.SelectedValue = "A" Then
            UnlockAll()
            txtLoad_Disc_Percent.Enabled = False
            cboLoad_Disc_Applied_On.Enabled = False
            cboCover_Rate_Per.Enabled = False
            txtCover_Rate.Enabled = False
        ElseIf cboLoad_Disc_Prem_Rate_Type.SelectedValue = "R" Then
            UnlockAll()
            txtLoad_Disc_Percent.Enabled = False
            txtLoad_Disc_Amt.Enabled = False
        ElseIf cboLoad_Disc_Prem_Rate_Type.SelectedValue = "F" Then
            UnlockAll()
            txtCover_Rate.Enabled = False
            txtLoad_Disc_Amt.Enabled = False
        End If
    End Sub

    Private Sub UnlockAll()
        txtLoad_Disc_Percent.Enabled = True
        cboLoad_Disc_Num.Enabled = True
        cboLoad_Disc_Applied_On.Enabled = True
        cboCover_Name.Enabled = True
        cboCover_Rate_Per.Enabled = True
        txtCover_Rate.Enabled = True
        txtLoad_Disc_Percent.Enabled = True
        txtLoad_Disc_Amt.Enabled = True
        txtCover_Rate.Enabled = True
        txtLoad_Disc_Amt.Enabled = True
    End Sub
End Class
