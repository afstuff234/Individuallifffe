Imports System.Data
Imports System.Data.OleDb

Partial Class I_LIFE_PRG_LI_CUST_CLASS
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String

    Protected strP_ID As String
    Protected strP_TYPE As String
    Protected strP_DESC As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strErrMsg As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_INS_CLASS"

        Try
            strP_TYPE = CType(Request.QueryString("optid"), String)
            strP_DESC = CType(Request.QueryString("optd"), String)
        Catch ex As Exception
            strP_TYPE = "ERR_TYPE"
            strP_DESC = "ERR_DESC"
        End Try

        STRPAGE_TITLE = "Master Codes Setup - " & strP_DESC

        If Trim(strP_TYPE) = "ERR_TYPE" Or Trim(strP_TYPE) = "" Then
            strP_TYPE = ""
        End If

        strP_ID = "L01"
        Me.txtCustType.Text = RTrim(strP_TYPE)
        Me.txtCustType.Text = RTrim("001")

        If Not (Page.IsPostBack) Then
            Call Proc_Populate_Box("IL_INS_CLASS_LIST", Trim(Me.txtCustType.Text), Me.cboTransList)
            Call Proc_DataBind()
            Call DoNew()
            'Me.lblMessage.Text = strSQL
            Me.txtCustNum.Enabled = True
            Me.txtCustNum.Focus()
        End If

        If Me.txtAction.Text = "New" Then
            Call DoNew()
            'Call Proc_OpenRecord(Me.txtNum.Text)
            Me.txtAction.Text = ""
            Me.txtCustNum.Enabled = True
            Me.txtCustNum.Focus()
        End If

        If Me.txtAction.Text = "Save" Then
            'Call DoSave()
            'Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete" Then
            Call DoDelete()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete_Item" Then
            'Call DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call DoSave()
        Me.txtAction.Text = ""

    End Sub


    Protected Sub txtCustNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustNum.TextChanged
        If RTrim(Me.txtCustNum.Text) <> "" Then
            textMessage.Text = RTrim(Me.txtCustNum.Text)
            strREC_ID = RTrim(Me.txtCustNum.Text)
            strErrMsg = Proc_OpenRecord(Me.txtCustNum.Text)
        End If

    End Sub

    Protected Sub DoNew()
        Call Proc_DDL_Get(Me.cboTransList, RTrim("*"))

        'Scan through textboxes on page or form
        'Dim ctrl As Control
        'For Each ctrl In Page.Controls
        '    If TypeOf ctrl Is HtmlForm Then
        '        Dim subctrl As Control
        '        For Each subctrl In ctrl.Controls
        '            If TypeOf subctrl Is System.Web.UI.WebControls.TextBox Then
        '                CType(subctrl, TextBox).Text = ""
        '            End If
        '        Next
        '    End If
        'Next

        With Me
            .txtCustType.Enabled = False
            .txtCustNum.ReadOnly = False
            .txtCustNum.Enabled = True
            .txtCustNum.Text = ""
            .txtRecNo.Text = "0"
            .txtRecNo.Enabled = False

            .txtCustName.Text = ""
            .txtShortName.Text = ""

            .cmdDelete_ASP.Enabled = False
            .textMessage.Text = "Status: New Entry..."
        End With
        strREC_ID = ""

    End Sub

    Protected Sub Proc_Populate_Box(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboList As DropDownList)
        'Populate box with codes
        Select Case UCase(Trim(pvCODE))
            Case "IL_INS_CLASS_LIST"
                strTable = strTableName
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_INS_CLASS_CD AS MyFld_Value, TBIL_INS_CLASS_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INS_CLASS_TYPE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " ORDER BY TBIL_INS_CLASS_TYPE, TBIL_INS_CLASS_DESC"
                'pvcboList.Items.Clear()
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")
        End Select

    End Sub


    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TBIL_INS_CLASS_REC_ID, TBIL_INS_CLASS_TYPE, TBIL_INS_CLASS_CD, TBIL_INS_CLASS_DESC"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_INS_CLASS_TYPE = '" & RTrim(Me.txtCustType.Text) & "'"
        strSQL = strSQL & " ORDER BY TBIL_INS_CLASS_TYPE, TBIL_INS_CLASS_CD"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        objOLEConn.Open()

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
        objDS = Nothing
        objDA = Nothing
        'objOLECmd.Dispose()
        'objOLECmd = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        Dim P As Integer = 0
        Dim C As Integer = 0

        C = 0
        For P = 0 To Me.GridView1.Rows.Count - 1
            C = C + 1
        Next
        If C >= 1 Then
            Me.cmdDelete_ASP.Enabled = True
        End If

    End Sub

    Private Sub DoSave()
        textMessage.Text = ""
        Dim strMyVal As String

        strMyVal = RTrim(Me.txtCustType.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.textMessage.Text = "Missing/Invalid " & Me.lblCustType.Text
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtCustNum.Text) = "" Or RTrim(Me.txtCustNum.Text) = "*" Then
            Me.textMessage.Text = "Missing/Invalid " & Me.lblCustNum.Text
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtCustName.Text) = "" Or RTrim(Me.txtCustName.Text) = "*" Then
            Me.textMessage.Text = "Missing/Invalid " & Me.lblCustName.Text
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        Dim intC As Long = 0
        Dim strmyFields As String
        Dim strmyParams As String

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.textMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try

        strREC_ID = Trim(Me.txtCustNum.Text)
        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 TBIL_INS_CLASS_CD FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_INS_CLASS_DESC = '" & RTrim(Me.txtCustName.Text) & "'"
        strSQL = strSQL & " AND TBIL_INS_CLASS_TYPE = '" & RTrim(Me.txtCustType.Text) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtCustNum.Text) <> Trim(chk_objOLEDR("TBIL_INS_CLASS_CD") & vbNullString) Then
                Me.textMessage.Text = "Warning!. The code description you enter already exist..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("TBIL_INS_CLASS_CD") & vbNullString)
                chk_objOLECmd = Nothing
                chk_objOLEDR = Nothing
                If objOLEConn.State = ConnectionState.Open Then
                    objOLEConn.Close()
                End If
                objOLEConn = Nothing
                Exit Sub
            End If
        End If

        chk_objOLECmd = Nothing
        chk_objOLEDR = Nothing

        Try
            'open connection to database
            objOLEConn.Close()
        Catch ex As Exception
            'Me.textMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Me.textMessage.Text = ex.Message.ToString
            objOLEConn = Nothing
            Exit Sub
        End Try


        objOLEConn.ConnectionString = mystrCONN
        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            'Me.textMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        strmyFields = ""
        strmyParams = ""

        strSQL = ""
        strSQL = "SELECT TOP 1 TBIL_INS_CLASS_CD FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_INS_CLASS_CD = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_INS_CLASS_TYPE = '" & RTrim(Me.txtCustType.Text) & "'"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim mySQLDS As New SqlDataSource

        Dim objOLEDR As OleDbDataReader = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            'Save existing record

            If objOLEDR.IsClosed = False Then
                objOLEDR.Close()
            End If

            strSQL = ""
            strSQL = strSQL & "UPDATE " & strTable & " SET"
            strSQL = strSQL & " TBIL_INS_CLASS_TYPE = '" & RTrim(Me.txtCustType.Text) & "'"
            strSQL = strSQL & ",TBIL_INS_CLASS_CD = '" & RTrim(Me.txtCustNum.Text) & "'"
            strSQL = strSQL & ",TBIL_INS_CLASS_DESC = '" & RTrim(Me.txtCustName.Text) & "'"
            strSQL = strSQL & ",TBIL_INS_CLASS_SHRT_DESC = '" & Left(RTrim(Me.txtShortName.Text), 18) & "'"
            strSQL = strSQL & ",TBIL_INS_CLASS_FLAG = '" & RTrim("C") & "'"
            strSQL = strSQL & " WHERE TBIL_INS_CLASS_CD = '" & RTrim(Me.txtCustNum.Text) & "'"
            strSQL = strSQL & " AND TBIL_INS_CLASS_TYPE = '" & RTrim(Me.txtCustType.Text) & "'"

            Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()
            objOLECmd2.Dispose()
            objOLECmd2 = Nothing

            Me.textMessage.Text = "Record Saved to Database Successfully."
        Else
            'Save new record

            'Specify the database fields
            strmyFields = ""
            strmyFields = strmyFields & "TBIL_INS_CLASS_TYPE,TBIL_INS_CLASS_CD,TBIL_INS_CLASS_DESC,TBIL_INS_CLASS_SHRT_DESC"
            strmyFields = strmyFields & ",TBIL_INS_CLASS_FLAG,TBIL_INS_CLASS_KEYDTE,TBIL_INS_CLASS_OPERID"


            Dim arrFlds As Array
            arrFlds = Split(strmyFields, ",")

            'Specify the field parameters, same as database fields, but prefix it with the @ sign
            strmyParams = ""
            'strmyParams = strmyParams & "@TBIL_INS_CLASS_TYPE,@TBIL_INS_CLASS_CD,@TBIL_INS_CLASS_DESC,@TBIL_INS_CLASS_SHRT_DESC"
            'strmyParams = strmyParams & ",@TBIL_INS_CLASS_FLAG,@TBIL_INS_CLASS_KEYDTE,@TBIL_INS_CLASS_OPERID"
            strmyParams = ""


            For intC = 0 To UBound(arrFlds)
                strmyParams = strmyParams & "@" & arrFlds(intC)
                If intC < UBound(arrFlds) Then
                    strmyParams = strmyParams & ","
                End If
            Next

            mySQLDS.ConnectionString = CType(Session("connstr_SQL"), String)

            With mySQLDS
                .InsertCommandType = SqlDataSourceCommandType.Text
                .InsertCommand = "INSERT INTO " & strTable & "(" & strmyFields & ")" & " VALUES(" & strmyParams & ")"

                .InsertParameters.Add("TBIL_INS_CLASS_TYPE", RTrim(Me.txtCustType.Text))
                .InsertParameters.Add("TBIL_INS_CLASS_CD", RTrim(Me.txtCustNum.Text))
                .InsertParameters.Add("TBIL_INS_CLASS_DESC", RTrim(Me.txtCustName.Text))
                .InsertParameters.Add("TBIL_INS_CLASS_SHRT_DESC", Left(RTrim(Me.txtShortName.Text), 18))

                .InsertParameters.Add("TBIL_INS_CLASS_FLAG", RTrim("A"))
                .InsertParameters.Add("TBIL_INS_CLASS_KEYDTE", CType(Format(Now, "MM/dd/yyyy"), Date))
                .InsertParameters.Add("TBIL_INS_CLASS_OPERID", RTrim(myUserIDX))
            End With


            'Try
            intC = mySQLDS.Insert()
            Me.textMessage.Text = "New Record Saved to Database Successfully."
            'Catch ex As Exception
            'Me.textMessage.Text = "Error!. Record not save. See your system administrator..."

            'End Try


        End If

        mySQLDS = Nothing

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


        FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
        'Me.textMessage.Text = ""

        Call Proc_Populate_Box("IL_INS_CLASS_LIST", Trim(Me.txtCustType.Text), Me.cboTransList)
        Call Proc_DataBind()

        DoNew()

        Me.txtCustName.Enabled = True
        Me.txtCustName.Focus()

    End Sub

    Protected Sub DoDelete()

        Dim strMyVal As String
        strMyVal = RTrim(Me.txtCustType.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.textMessage.Text = "Missing/Invalid " & Me.lblCustType.Text
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtCustNum.Text) = "" Then
            Me.textMessage.Text = "Missing " & Me.lblCustNum.Text
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        strTable = strTableName

        strREC_ID = Trim(Me.txtCustNum.Text)

        strSQL = "SELECT TBIL_INS_CLASS_CD FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_INS_CLASS_CD = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_INS_CLASS_TYPE = '" & RTrim(Me.txtCustType.Text) & "'"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        'open connection to database
        objOLEConn.Open()

        strOPT = "NEW"
        FirstMsg = ""

        Dim objOLEDR As OleDbDataReader = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strOPT = "OLD"
        End If

        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        Select Case RTrim(strOPT)
            Case "OLD"
                'Delete record
                'Me.textMessage.Text = "Deleting record... "
                strSQL = ""
                strSQL = "DELETE FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INS_CLASS_CD = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND TBIL_INS_CLASS_TYPE = '" & RTrim(Me.txtCustType.Text) & "'"

                Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
                objOLECmd2.Connection = objOLEConn
                objOLECmd2.CommandType = CommandType.Text
                objOLECmd2.CommandText = strSQL
                intC = objOLECmd2.ExecuteNonQuery()
                objOLECmd2.Dispose()
                objOLECmd2 = Nothing
            Case Else
        End Select

        'Try
        'Catch ex As Exception
        'End Try

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        If intC >= 1 Then
            Me.textMessage.Text = "Record deleted successfully."
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "');"
        Else
            Me.textMessage.Text = "Sorry!. Record not deleted..."
            FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "');"
        End If
        'Me.textMessage.Text = ""

        Call Proc_Populate_Box("IL_INS_CLASS_LIST", Trim(Me.txtCustType.Text), Me.cboTransList)
        Call Proc_DataBind()

        Me.cmdDelete_ASP.Enabled = False

        Call DoNew()
        Me.txtCustName.Enabled = True
        Me.txtCustName.Focus()

    End Sub

    Protected Sub cboTransList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTransList.SelectedIndexChanged
        Me.txtCustNum.Text = RTrim(Me.cboTransList.SelectedItem.Value)
        If RTrim(Me.txtCustNum.Text) = "*" Or RTrim(Me.txtCustNum.Text) = "" Or RTrim(Me.txtCustNum.Text) = "0" Then
            Me.txtCustNum.Text = ""
            Call DoNew()
            Exit Sub
        End If

        If RTrim(Me.txtCustNum.Text) <> "" Then
            textMessage.Text = RTrim(Me.txtCustNum.Text)
            strREC_ID = RTrim(Me.txtCustNum.Text)
            strErrMsg = Proc_OpenRecord(Me.txtCustNum.Text)
        End If

    End Sub

    Protected Sub cboGLACCCODE_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGLAccCode.TextChanged
        Me.txtGLAccCode.Text = RTrim(Me.cboGLAccCode.SelectedItem.Value)
        If RTrim(Me.txtGLAccCode.Text) = "*" Then
            Me.txtGLAccCode.Text = ""
            Exit Sub
        End If

    End Sub

    Private Function Proc_OpenRecord(ByVal strRefNo As String) As String

        On Error GoTo myRtn_Err

        strErrMsg = "false"

        textMessage.Text = ""
        If Trim(strRefNo) = "" Then
            Proc_OpenRecord = strErrMsg
            Return Proc_OpenRecord
        End If

        strREC_ID = Trim(strRefNo)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 TRN.*"
        strSQL = strSQL & " FROM " & strTable & " AS TRN"
        strSQL = strSQL & " WHERE TRN.TBIL_INS_CLASS_CD = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TRN.TBIL_INS_CLASS_TYPE = '" & RTrim(Me.txtCustType.Text) & "'"
        'strSQL = strSQL & " AND TBIL_INS_CLASS_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader

        'open connection to database
        objOLEConn.Open()

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_INS_CLASS_REC_ID") & vbNullString, String))
            Me.txtCustType.Text = RTrim(CType(objOLEDR("TBIL_INS_CLASS_TYPE") & vbNullString, String))
            Me.txtCustNum.Text = RTrim(CType(objOLEDR("TBIL_INS_CLASS_CD") & vbNullString, String))
            Me.txtCustName.Text = RTrim(CType(objOLEDR("TBIL_INS_CLASS_DESC") & vbNullString, String))
            Me.txtShortName.Text = RTrim(CType(objOLEDR("TBIL_INS_CLASS_SHRT_DESC") & vbNullString, String))

            'Me.txtGLAccCode.Text = RTrim(CType(objOLEDR("TBIL_CUST_CAT_CNTRL_ACCT") & vbNullString, String))
            'Call Proc_DDL_Get(Me.cboGLAccCode, RTrim(Me.txtGLAccCode.Text))


            Call DisableBox(Me.txtCustNum)
            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True
        Else
            'Me.txtCustNum.Text = ""
            Me.cmdDelete_ASP.Enabled = False
            strErrMsg = "Status: New Entry..."
            Me.txtCustName.Enabled = True
            Me.txtCustName.Focus()
        End If

        ' dispose of open objects
        objOLECmd.Dispose()

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If

        GoTo MyRtn_Ok

myRtn_Err:
        strErrMsg = Err.Number & " - " & Err.Description
MyRtn_Ok:

        objOLECmd = Nothing
        objOLEDR = Nothing
        objOLEConn = Nothing

        textMessage.Text = strErrMsg
        Proc_OpenRecord = strErrMsg
        Return Proc_OpenRecord

    End Function

    Private Sub DisableBox(ByVal objTxtBox As TextBox)
        Dim c As System.Drawing.Color = Drawing.Color.LightGray
        objTxtBox.ReadOnly = True
        objTxtBox.Enabled = False
        'objTxtBox.BackColor = c

    End Sub

    Private Sub Proc_CloseDB(ByVal myOLECmd As OleDbCommand, ByVal myOLEConn As OleDbConnection)
        myOLECmd.Dispose()
        If myOLEConn.State = ConnectionState.Open Then
            myOLEConn.Close()
        End If

    End Sub

    Private Sub Proc_DDL_Get(ByVal pvDDL As DropDownList, ByVal pvRef_Value As String)
        On Error Resume Next
        pvDDL.SelectedIndex = pvDDL.Items.IndexOf(pvDDL.Items.FindByValue(CType(RTrim(pvRef_Value), String)))

    End Sub

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        Me.textMessage.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount

    End Sub

    Private Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(2).Text

        Me.txtCustType.Text = row.Cells(3).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))

        Me.txtCustNum.Text = row.Cells(4).Text
        Call Proc_DDL_Get(Me.cboTransList, RTrim(Me.txtCustNum.Text))

        Call Proc_OpenRecord(Me.txtCustNum.Text)

        textMessage.Text = "You selected " & Me.txtCustNum.Text & " / " & Me.txtRecNo.Text & "."

    End Sub

End Class

