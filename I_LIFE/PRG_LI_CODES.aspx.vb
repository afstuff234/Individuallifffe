Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class I_LIFE_PRG_LI_CODES
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String
    Protected PageTitle As String
    Protected STRPAGE_TITLE As String

    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strP_ID As String
    Protected strP_TYPE As String
    Protected strP_DESC As String
    Protected strPOP_UP As String


    Dim strREC_ID As String
    Dim strOPT As String

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim mydteX As String
    Dim mydte As Date
    Dim dteStart As Date
    Dim dteEnd As Date

    Dim strErrMsg As String

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        ' set the theme name at design time
        '<%@ Page Theme="theme_name" ... %>

        ' set the theme name at run time
        'Page.Theme = "theme name"

        ' retrieve the theme name
        'Dim mypage_theme As String = Page.Theme

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        'If Not (Page.IsPostBack) Then
        '    Dim mydir As DirectoryInfo = Nothing
        '    'mydir = New DirectoryInfo(Server.MapPath("I_LIFE"))
        '    mydir = New DirectoryInfo("H:\ABS-WEB\ABSW_LIFE\ABSW_LIFE\")
        '    With Me.ddlGroup
        '        .DataTextField = "Name"
        '        .DataSource = mydir.GetDirectories()
        '        '.DataSource = mydir.GetFiles()
        '        .DataBind()
        '    End With
        'End If

        'Transfer to the currect page. This cause the page to refresh.
        'If Not (Page.IsPostBack) Then
        '    Server.Transfer(Request.FilePath)
        'End If


        strTableName = "TBIL_LIFE_CODES"

        Try
            strP_TYPE = CType(Request.QueryString("optid"), String)
            strP_DESC = CType(Request.QueryString("optd"), String)
        Catch ex As Exception
            strP_TYPE = "ERR_TYPE"
            strP_DESC = "ERR_DESC"
        End Try

        Try
            strPOP_UP = CType(Request.QueryString("popup"), String)
        Catch ex As Exception
            strPOP_UP = "NO"
        End Try

        If UCase(Trim(strPOP_UP)) = "YES" Then
            Me.PageAnchor_Return_Link.Visible = False
            PageLinks = "<a class='a_return_menu' href='#' onclick='javascript:window.close();'>Click here to CLOSE PAGE...</a>"
        Else
            Me.PageAnchor_Return_Link.Visible = True
            PageLinks = ""
        End If

        STRPAGE_TITLE = "Master Codes Setup - " & strP_DESC

        If Trim(strP_TYPE) = "ERR_TYPE" Or Trim(strP_TYPE) = "" Then
            strP_TYPE = ""
        End If

        strP_ID = "L02"
        Me.txtGroupNum.Text = RTrim(strP_TYPE)

        If Not (Page.IsPostBack) Then
            Call Proc_Populate_Box("IL_CODE_LIST", Trim(Me.txtGroupNum.Text), Me.ddlGroup)
            Call Proc_DataBind()
            Call DoNew()
            Me.txtAction.Text = ""
            Me.lblMessage.Text = "New Entry"
            'Me.txtNum.Enabled = True
            'Me.txtNum.Focus()
            Me.txtTransName.Enabled = True
            Me.txtTransName.Focus()
        End If

        If Me.txtAction.Text = "New" Then
            Call DoNew()
            'Call Proc_OpenRecord(Me.txtNum.Text)
            Me.lblMessage.Text = "New Entry"
            Me.txtAction.Text = ""
            'Me.txtNum.Enabled = True
            'Me.txtNum.Focus()
            Me.txtTransName.Enabled = True
            Me.txtTransName.Focus()
        End If

        If Me.txtAction.Text = "Save" Then
            'Call DoSave()
            'Me.txtAction.Text = ""
        End If

        'If Me.txtAction.Text = "Delete" Then
        'Call DoDelete()
        'Me.txtAction.Text = ""
        'End If

        If Me.txtAction.Text = "Delete_Item" Then
            Call DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Call DoSave()
        Me.txtAction.Text = ""

    End Sub

    Protected Sub txtNum_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum.TextChanged
        If Trim(Me.txtNum.Text) <> "" And Trim(Me.txtGroupNum.Text) <> "" Then
            Call Proc_OpenRecord(Trim(Me.txtNum.Text))
        End If

    End Sub

    Protected Sub DoNew(Optional ByVal pvOPT As String = "NEW")

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

        'Call Proc_DDL_Get(Me.ddlGroup, RTrim("*"))

        'Me.lblShortName.Visible = False
        'Me.txtShortName.Visible = False

        With Me
            .chkNum.Checked = True
            .chkNum.Enabled = True
            .lblNum.Enabled = True
            '.lblNum.Text = "Trans Code:"
            .txtNum.ReadOnly = False
            .txtNum.Enabled = False
            .txtNum.Text = ""
            .txtRecNo.Text = "0"
            .txtRecNo.Enabled = False


            .txtTransName.Text = ""
            '.lblTransName.Text = "Trans Name:"
            .txtShortName.Text = ""

            .cmdDelete_ASP.Enabled = False
            .lblMessage.Text = "Status: New Entry..."
        End With

    End Sub

    Protected Sub Proc_Populate_Box(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboList As DropDownList)
        'Populate box with codes
        Select Case UCase(Trim(pvCODE))
            Case "IL_CODE_LIST"
                strTable = strTableName
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_COD_ITEM AS MyFld_Value, TBIL_COD_LONG_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"
                strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " ORDER BY TBIL_COD_TAB_ID, TBIL_COD_TYP, TBIL_COD_LONG_DESC"
                'pvcboList.Items.Clear()
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")
        End Select

    End Sub


    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TBIL_COD_REC_ID, TBIL_COD_TYP, TBIL_COD_ITEM, TBIL_COD_LONG_DESC"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"
        strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(Me.txtGroupNum.Text) & "'"
        strSQL = strSQL & " ORDER BY TBIL_COD_TAB_ID, TBIL_COD_TYP, TBIL_COD_ITEM"

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

        If RTrim(Me.txtGroupNum.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblGroupNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.txtNum.Text) = "" Then
            'Me.lblMessage.Text = "Missing " & Me.lblNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        End If

        If RTrim(Me.txtTransName.Text) = "" Or RTrim(Me.txtTransName.Text) = "." Or RTrim(Me.txtTransName.Text) = "*" Then
            Me.lblMessage.Text = "Invalid or Missing " & Me.lblTransName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
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
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
        strSQL = strSQL & " AND TBIL_COD_LONG_DESC = '" & LTrim(Me.txtTransName.Text) & "'"
        strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtNum.Text) <> Trim(chk_objOLEDR("TBIL_COD_ITEM") & vbNullString) Then
                Me.lblMessage.Text = "Warning!. The code description you enter already exist..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("TBIL_COD_ITEM") & vbNullString)
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
            'Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            Me.lblMessage.Text = ex.Message.ToString
            objOLEConn = Nothing
            Exit Sub
        End Try


        If RTrim(txtNum.Text) = "" Then
            Me.txtNum.Text = MOD_GEN.gnGet_Serial_Und("GET_SN_IL_UNDW", Trim(strP_ID), Trim(Me.txtGroupNum.Text), "XXXX", "XXXX", "")
            If Trim(txtNum.Text) = "" Or Trim(Me.txtNum.Text) = "0" Or Trim(Me.txtNum.Text) = "*" Then
                Me.txtNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get the next record id. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtNum.Text) = "PARAM_ERR" Then
                Me.txtNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get the next record id - INVALID PARAMETER(S) - " & Trim(strP_ID)
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtNum.Text) = "DB_ERR" Then
                Me.txtNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtNum.Text) = "ERR_ERR" Then
                Me.txtNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            End If

        End If


        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try


        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
        strSQL = strSQL & " AND TBIL_COD_ITEM = '" & RTrim(txtNum.Text) & "'"
        strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"


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

                drNewRow("TBIL_COD_TAB_ID") = RTrim(strP_ID)
                drNewRow("TBIL_COD_TYP") = RTrim(Me.txtGroupNum.Text)
                drNewRow("TBIL_COD_ITEM") = RTrim(Me.txtNum.Text)

                drNewRow("TBIL_COD_LONG_DESC") = RTrim(Me.txtTransName.Text)
                drNewRow("TBIL_COD_SHORT_DESC") = Left(LTrim(Me.txtShortName.Text), 18)

                drNewRow("TBIL_COD_FLAG") = "A"
                drNewRow("TBIL_COD_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_COD_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMessage.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record

                'm_rwContact = m_dtContacts.Rows(0)
                'm_rwContact("ContactName") = "Bob Brown"
                'm_rwContact.AcceptChanges()
                'm_dtContacts.AcceptChanges()
                'Dim intC As Integer = m_daDataAdapter.Update(m_dtContacts)


                With obj_DT
                    .Rows(0)("TBIL_COD_TAB_ID") = RTrim(strP_ID)
                    .Rows(0)("TBIL_COD_TYP") = RTrim(Me.txtGroupNum.Text)
                    .Rows(0)("TBIL_COD_ITEM") = RTrim(Me.txtNum.Text)

                    .Rows(0)("TBIL_COD_LONG_DESC") = RTrim(Me.txtTransName.Text)
                    .Rows(0)("TBIL_COD_SHORT_DESC") = Left(LTrim(Me.txtShortName.Text), 18)

                    .Rows(0)("TBIL_COD_FLAG") = "C"
                    '.Rows(0)("TBIL_COD_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_COD_KEYDTE") = Now
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMessage.Text = "Record Saved to Database Successfully."

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
            Me.lblMessage.Text = ex.Message.ToString
            Exit Sub
        End Try

        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        obj_DT.Dispose()
        obj_DT = Nothing

        If objDA.SelectCommand.Connection.State = ConnectionState.Open Then
            objDA.SelectCommand.Connection.Close()
        End If
        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        'Call DoProc_TransList("BRANCH")

        Me.cmdDelete_ASP.Enabled = True
        'Me.txtNum.Enabled = False

        'DoProc_SaveRecord = Me.lblMessage.Text
        'Return Me.lblMessage.Text

        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        'Me.textMessage.Text = ""

        'Me.txtNum.Text = ""


        Call Proc_DataBind()
        Call Proc_Populate_Box("IL_CODE_LIST", Trim(Me.txtGroupNum.Text), Me.ddlGroup)
        Call DoNew()

        'Me.txtNum.Enabled = True
        'Me.txtNum.Focus()
        Me.txtTransName.Enabled = True
        Me.txtTransName.Focus()

    End Sub


    Protected Sub DoDelete()

        If Trim(Me.txtNum.Text) = "" Then
            Me.lblMessage.Text = "Missing number " & Me.lblNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Exit Sub
        End Try


        strTable = strTableName

        strREC_ID = Trim(Me.txtNum.Text)

        strSQL = "SELECT TOP 1 TBIL_COD_ITEM FROM " & strTable
        strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
        strSQL = strSQL & " WHERE TBIL_COD_ITEM = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

        strOPT = "NEW"
        FirstMsg = ""

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

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
                strSQL = strSQL & " WHERE TBIL_COD_ITEM = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
                strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

                Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
                objOLECmd2.Connection = objOLEConn
                objOLECmd2.CommandType = CommandType.Text
                objOLECmd2.CommandText = strSQL
                intC = objOLECmd2.ExecuteNonQuery()
                objOLECmd2.Dispose()
                objOLECmd2 = Nothing

                Call Proc_Populate_Box("IL_CODE_LIST", Trim(Me.txtGroupNum.Text), Me.ddlGroup)
            Case Else
        End Select

        'Try
        'Catch ex As Exception
        'End Try

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        'Session("TRANS_ID") = RTrim("")

        'Call DoProc_TransList("BRANCH")

        Me.cmdDelete_ASP.Enabled = False

        If intC >= 1 Then
            Me.lblMessage.Text = "Record deleted successfully."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        Else
            Me.lblMessage.Text = "Sorry!. Record not deleted..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        End If

        'Me.textMessage.Text = ""


        Call DoNew()
        'Me.txtNum.Enabled = True
        'Me.txtNum.Focus()

    End Sub


    Protected Sub DoDelItem()

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
                Me.txtNum.Text = Me.GridView1.Rows(P).Cells(4).Text


                'Insert codes to delete selected/checked item(s)

                If Trim(myKey) <> "" Then
                    Me.txtRecNo.Text = myKey
                    Call DoDelete_Record()
                    C = C + 1
                End If

            End If

        Next

        Me.cmdDelete_ASP.Enabled = False
        'Me.cmdDelItem.Enabled = False

        Call Proc_DataBind()
        Call Proc_Populate_Box("IL_CODE_LIST", Trim(Me.txtGroupNum.Text), Me.ddlGroup)

        Me.lblMessage.Text = "Record deleted successfully." & " No of item(s) deleted: " & CStr(C)
        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        'Me.textMessage.Text = ""

        Call DoNew("INIT")

        Me.lblMessage.Text = "Deleted Item(s): " & myKeyX

        'Me.txtTreatyNum.Enabled = True
        'Me.txtTreatyNum.Focus()

    End Sub

    Protected Sub DoDelete_Record()

        If Trim(Me.txtGroupNum.Text) = "" Then
            Me.txtNum.Text = "Missing " & Me.lblGroupNum.Text
            FirstMsg = "Javascript:alert('" & Me.txtNum.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtNum.Text) = "" Then
            Me.txtNum.Text = "Missing " & Me.lblNum.Text
            FirstMsg = "Javascript:alert('" & Me.txtNum.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtRecNo.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblRecNo.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        strREC_ID = Trim(Me.txtNum.Text)
        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 TBIL_COD_ITEM FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_COD_ITEM = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
        strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        'open connection to database
        objOLEConn.Open()

        Dim objOLEDR As OleDbDataReader = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strOPT = "OLD"
        Else
            strOPT = "NEW"
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


                'Delete from claim table
                '==============================================
                strSQL = ""
                strSQL = "DELETE FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_COD_ITEM = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
                strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

                Dim objOLECmd2 As OleDbCommand = New OleDbCommand()
                With objOLECmd2
                    .Connection = objOLEConn
                    .CommandType = CommandType.Text
                    .CommandText = strSQL
                End With
                intC = objOLECmd2.ExecuteNonQuery()
                objOLECmd2.Dispose()
                objOLECmd2 = Nothing


        End Select

        'Try
        'Catch ex As Exception
        'End Try

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub


    Private Function Proc_OpenRecord(ByVal strRefNo As String, Optional ByVal strSearchByWhat As String = "MY_TRANS_NUM") As String

        strErrMsg = "false"

        lblMessage.Text = ""
        If Trim(strRefNo) = "" Then
            Proc_OpenRecord = strErrMsg
            Return Proc_OpenRecord
        End If

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Proc_OpenRecord = strErrMsg
            Return Proc_OpenRecord
            Exit Function
        End Try


        strREC_ID = Trim(strRefNo)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 ILCODE.*"
        strSQL = strSQL & " FROM " & strTable & " AS ILCODE"
        strSQL = strSQL & " WHERE ILCODE.TBIL_COD_ITEM = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND ILCODE.TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
        strSQL = strSQL & " AND ILCODE.TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"
        'strSQL = strSQL & " AND TBIL_COD_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"


        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader


        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then

            Me.txtGroupNum.Text = RTrim(CType(objOLEDR("TBIL_COD_TYP") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_COD_REC_ID") & vbNullString, String))

            Me.txtNum.Text = RTrim(CType(objOLEDR("TBIL_COD_ITEM") & vbNullString, String))
            Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtNum.Text))
            Me.txtTransName.Text = RTrim(CType(objOLEDR("TBIL_COD_LONG_DESC") & vbNullString, String))
            Me.txtShortName.Text = RTrim(CType(objOLEDR("TBIL_COD_SHORT_DESC") & vbNullString, String))


            Me.lblNum.Enabled = False
            Call DisableBox(Me.txtNum)
            Me.chkNum.Enabled = False
            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True

        Else
            'Me.txtNum.Text = ""
            Me.cmdDelete_ASP.Enabled = False
            strErrMsg = "Status: New Entry..."

            Me.txtTransName.Enabled = True
            Me.txtTransName.Focus()
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

        Me.lblMessage.Text = strErrMsg
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

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        lblMessage.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(2).Text

        Me.txtGroupNum.Text = row.Cells(3).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))

        Me.txtNum.Text = row.Cells(4).Text
        Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtNum.Text))

        Call Proc_OpenRecord(Me.txtNum.Text)

        lblMessage.Text = "You selected " & Me.txtNum.Text & " / " & Me.txtRecNo.Text & "."

    End Sub


    'Private Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting

    'End Sub

    'Private Sub GridView1_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.Sorted
    '    ' Display the sort expression and sort direction.
    '    Me.lblMessage.Text = "Sorting by " & _
    '      GridView1.SortExpression.ToString() & " in " & GridView1.SortDirection.ToString() & " order."

    'End Sub


    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlGroup.SelectedIndexChanged


        'Me.txtGroupNum.Text = RTrim(Me.ddlGroup.SelectedItem.Value)
        Me.txtNum.Text = RTrim(Me.ddlGroup.SelectedItem.Value)
        If RTrim(Me.txtNum.Text) = "" Or RTrim(Me.txtNum.Text) = "*" Then
            Me.txtNum.Text = ""
            Me.txtTransName.Text = ""
            Exit Sub
        Else
            'Call Proc_DataBind()
            'Call DoNew()
            Me.txtRecNo.Text = "0"
            Call Proc_OpenRecord(Me.txtNum.Text)
        End If

    End Sub


    Protected Sub chkNum_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkNum.CheckedChanged
        If Me.chkNum.Checked = True Then
            Me.txtNum.Enabled = False
        Else
            txtNum.Enabled = True
        End If
    End Sub

End Class
