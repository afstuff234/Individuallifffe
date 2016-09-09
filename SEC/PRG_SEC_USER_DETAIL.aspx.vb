Imports System.Data
Imports System.Data.OleDb
Partial Class SEC_PRG_SEC_USER_DETAIL
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String

    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strP_ID As String
    Protected strP_TYPE As String
    Protected strP_DESC As String
    Protected strPOP_UP As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strErrMsg As String
    Dim li As ListItem

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "SEC_USER_LIFE_DETAIL"

        STRPAGE_TITLE = "User Details Setup - " & strP_DESC

        strP_ID = "L01"
        Me.txtUserID.Text = RTrim(strP_TYPE)
        Me.txtUserID.Text = RTrim("001")

        If Not (Page.IsPostBack) Then
            'Call Proc_Populate_Box("IL_BRK_MODULE_LIST", Trim("001"), Me.cboWorkGroup)
            'Call Proc_Populate_Box("IL_BRK_CLASS_LIST", Trim("001"), Me.cboRole)
            '    Call Proc_Populate_Box("IL_BRK_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
            txtPassExpDays.Text = 90
            'Me.txtPassExpDate.Text = Format(DateAdd(DateInterval.Day, CInt(txtPassExpDays.Text), DateTime.Now), "dd/MM/yyyy")

            Dim UserRole As String = ""
            Try
                UserRole = CType(Session("MyUserRole"), String)
            Catch ex As Exception
                UserRole = ""
            End Try

            If UserRole <> "Administrator" Then
                cmdSave_ASP.Visible = False
            End If
            chkStatus.Checked = True

            li = New ListItem
            li.Text = "*** Select ***"
            li.Value = "*"
            cboGroup.Items.Add(li)

            li = New ListItem
            li.Text = "Administrators"
            li.Value = "admin"
            cboGroup.Items.Add(li)

            li = New ListItem
            li.Text = "Management"
            li.Value = "mgt"
            cboGroup.Items.Add(li)

            li = New ListItem
            li.Text = "System Users"
            li.Value = "sysuser"
            cboGroup.Items.Add(li)


            cboRole.Items.Add("*** Select ***")
            cboRole.Items.Add("Administrator")
            cboRole.Items.Add("Data Manager")
            cboRole.Items.Add("Officers")
            cboRole.Items.Add("Super User")
            cboRole.Items.Add("Supervisor")
            cboRole.Items.Add("IT Contract Staff")

            Call Proc_DataBind()
            Call DoNew()
            'Me.lblMessage.Text = strSQL
            Me.txtAction.Text = ""
            'Me.txtCustNum.Enabled = True
            'Me.txtCustNum.Focus()
            Me.txtName.Enabled = True
            Me.txtName.Focus()
        End If

        If Me.txtAction.Text = "New" Then
            Call DoNew()
            'Call Proc_OpenRecord(Me.txtNum.Text)
            Me.txtAction.Text = ""
            Me.txtName.Focus()
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
            ''Call DoDelItem()
            Me.txtAction.Text = ""
        End If


    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call DoSave()
        Me.txtAction.Text = ""
    End Sub

    Protected Sub txtLoginName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoginName.TextChanged
        If RTrim(Me.txtLoginName.Text) <> "" Then
            lblMessage.Text = RTrim(Me.txtShortName.Text)
            strREC_ID = RTrim(Me.txtLoginName.Text)
            strErrMsg = Proc_OpenRecord(Me.txtLoginName.Text)
        End If
    End Sub

    Protected Sub DoNew()
        'Call Proc_DDL_Get(Me.cboGroup, RTrim("*"))
        'Call Proc_DDL_Get(Me.cboRole, RTrim("*"))
        ' Call Proc_DDL_Get(Me.cboTransList, RTrim("*"))

        Me.cboGroup.SelectedIndex = 0
        Me.cboRole.SelectedIndex = 0
        ' Me.cboTransList.SelectedIndex = 0

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
            .txtRecNo.Text = "0"
            .txtRecNo.Enabled = False
            .txtUserID.Enabled = False
            .txtGroup.Text = ""
            .txtShortName.Text = ""
            .txtName.Text = ""
            .txtCustPhone01.Text = ""
            .txtCustPhone02.Text = ""
            .txtCustEmail01.Text = ""
            .txtCustEmail02.Text = ""
            txtBranch.Text = ""
            txtLoginName.Text = ""
            txtPassword.Text = ""
            txtConPassword.Text = ""
            .cmdDelete_ASP.Enabled = False
            .lblMessage.Text = "Status: New Entry..."
        End With
        strREC_ID = ""

    End Sub

    Protected Sub Proc_Populate_Box(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboList As DropDownList)
        'Populate box with codes
        pvcboList.Items.Clear()
        Select Case UCase(Trim(pvCODE))
            Case "IL_BRK_MODULE_LIST"
                strTable = strTableName
                strSQL = ""
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_BRK_CLASS_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_CUST_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_CUST_CATEG AS MyFld_Value, TBIL_CUST_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_CUST_CAT_ID = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " ORDER BY TBIL_CUST_CAT_ID, TBIL_CUST_CAT_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_BRK_DETAIL_LIST"
                'Try
                '    Me.txtCustModule.Text = cboCustModule.SelectedValue
                'Catch ex As Exception
                'End Try

                strTable = strTableName
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_CUST_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_CUST_DESC,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_CUST_ID = '" & RTrim(pvTransType) & "'"
                'strSQL = strSQL & " AND TBIL_CUST_MDLE = '" & RTrim(Me.txtCustModule.Text) & "'"
                strSQL = strSQL & " AND (TBIL_CUST_DESC LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
                strSQL = strSQL & " ORDER BY TBIL_CUST_ID, RTRIM(ISNULL(TBIL_CUST_DESC,''))"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")
        End Select

    End Sub


    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        'Try
        '    Me.txtCustModule.Text = cboCustModule.SelectedValue
        'Catch ex As Exception
        'End Try

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT SEC_USER_REC_ID, SEC_USER_ID, SEC_USER_LOGIN"
        strSQL = strSQL & ",RTRIM(ISNULL(SEC_USER_NAME,'')) AS SEC_USER_FULLNAME"
        strSQL = strSQL & ",RTRIM(ISNULL(SEC_USER_PHONE1,'')) + ' ' + RTRIM(ISNULL(SEC_USER_PHONE2,'')) AS SEC_USER_PHONE_NUM"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE SEC_USER_ID = '" & RTrim(Me.txtUserID.Text) & "'"
        strSQL = strSQL & " AND (SEC_USER_NAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
        strSQL = strSQL & " ORDER BY SEC_USER_REC_ID, RTRIM(ISNULL(SEC_USER_NAME,''))"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        objOLEConn.Open()
        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

        Dim objDS As DataSet = New DataSet()
        objDA.Fill(objDS, strTable)

        With GridView1
            .DataSource = objDS
            .DataBind()
        End With
        objDS = Nothing
        objDA = Nothing
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
        lblMessage.Text = ""
        Dim strMyVal As String


        Try
            If Me.cboGroup.SelectedIndex >= 1 Then
                'Me.txtCustModule.Text = cboCustModule.SelectedValue
                Me.txtGroup.Text = cboGroup.SelectedItem.Value
            End If
        Catch ex As Exception
        End Try

        Try
            If Me.cboRole.SelectedIndex >= 1 Then
                'Me.txtCustCateg.Text = cboCustCateg.SelectedValue
                'Me.txtCustCateg.Text = cboRole.SelectedItem.Value
            End If
        Catch ex As Exception
        End Try


        strMyVal = RTrim(Me.txtUserID.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustID.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        If Trim(Me.txtName.Text) = "" Or RTrim(Me.txtName.Text) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblFullName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        strMyVal = RTrim(Me.txtShortName.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblShortName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        strMyVal = RTrim(Me.txtGroup.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblGroup.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        strMyVal = RTrim(Me.cboRole.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblRole.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        strMyVal = RTrim(Me.txtBranch.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblBranch.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtLoginName.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblLoginName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtPassword.Text)
        If RTrim(strMyVal) = "" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblPassword.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtConPassword.Text)
        If RTrim(strMyVal) = "" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblConPassword.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If txtPassword.Text <> txtConPassword.Text Then
            Me.lblMessage.Text = "Password does not matches"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtCustPhone01.Text)) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCustPhone01.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtCustPhone01.Text))) And Len(LTrim(RTrim(Me.txtCustPhone01.Text))) = 11 Then
            Else
                Me.lblMessage.Text = "Incorrect/Invalid " & Me.lblCustPhone01.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub
            End If
        End If

        If LTrim(RTrim(Me.txtCustPhone02.Text)) = "" Then
            'Me.lblMessage.Text = "Missing " & Me.lblCustPhone02.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtCustPhone02.Text))) And Len(LTrim(RTrim(Me.txtCustPhone02.Text))) = 7 Then
            Else
                Me.lblMessage.Text = "Incorrect/Invalid " & Me.lblCustPhone02.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub
            End If
        End If


        If LTrim(RTrim(Me.txtCustEmail01.Text)) = "" Then
        Else
            blnStatus = gnParseEmail_Address(RTrim(Me.txtCustEmail01.Text))
            If blnStatus = False Then
                Me.lblMessage.Text = "Incorrect/Invalid " & Me.lblCustEmail01.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub

            End If
        End If

        If LTrim(RTrim(Me.txtCustEmail02.Text)) = "" Then
        Else
            blnStatus = gnParseEmail_Address(RTrim(Me.txtCustEmail02.Text))
            If blnStatus = False Then
                Me.lblMessage.Text = "Incorrect/Invalid " & Me.lblCustEmail02.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub

            End If
        End If

        If IsNumeric(txtPassExpDays.Text) Then
            If Val(txtPassExpDays.Text) <= 0 Then
                Me.lblMessage.Text = "Password Expiry Days must be greater than zero"
                txtPassExpDays.Focus()
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub
            End If
        Else
            Me.lblMessage.Text = "Password Expiry Days must be numeric"
            txtPassExpDays.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If


        Me.txtPassExpDate.Text = Format(DateAdd(DateInterval.Day, CInt(txtPassExpDays.Text), DateTime.Now), "dd/MM/yyyy")

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        Dim intC As Long = 0

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
        strSQL = "SELECT TOP 1 SEC_USER_LOGIN FROM " & strTable
        strSQL = strSQL & " WHERE SEC_USER_LOGIN = '" & RTrim(Me.txtLoginName.Text) & "'"
        strSQL = strSQL & " AND SEC_USER_ID = '" & RTrim(Me.txtUserID.Text) & "'"
        'strSQL = strSQL & " WHERE RTRIM(ISNULL(TBIL_CUST_DESC,'')) = '" & RTrim(Me.txtName.Text) & "'"
        'strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtUserID.Text) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtLoginName.Text) <> Trim(chk_objOLEDR("SEC_USER_LOGIN") & vbNullString) Then
                Me.lblMessage.Text = "Warning!. The username you entered already exist for " & chk_objOLEDR("SEC_USER_NAME") & " ..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("SEC_USER_LOGIN") & vbNullString)
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
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Me.lblMessage.Text = ex.Message.ToString
            objOLEConn = Nothing
            Exit Sub
        End Try

        strREC_ID = Trim(Me.txtLoginName.Text)

        objOLEConn.ConnectionString = mystrCONN
        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            'Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try

        Dim status As String

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE SEC_USER_LOGIN = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND SEC_USER_ID = '" & RTrim(Me.txtUserID.Text) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        'or
        'objDA.SelectCommand = New System.Data.OleDb.OleDbCommand(strSQL, objOleConn)

        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        'Dim m_rwContact As System.Data.DataRow


        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record

                If chkStatus.Checked = True Then
                    status = "A"
                Else
                    status = "X"
                End If

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("SEC_USER_ID") = RTrim(Me.txtUserID.Text)
                drNewRow("SEC_USER_LOGIN") = RTrim(Me.txtLoginName.Text)
                drNewRow("SEC_USER_NAME") = RTrim(Me.txtName.Text)
                drNewRow("SEC_USER_SHRT_NAME") = RTrim(Me.txtShortName.Text)
                drNewRow("SEC_USER_GROUP_CODE") = RTrim(Me.txtGroup.Text)
                drNewRow("SEC_USER_ROLE") = LTrim(Me.cboRole.Text)
                drNewRow("SEC_USER_BRANCH") = LTrim(Me.txtBranch.Text)
                drNewRow("SEC_USER_PASSWORD") = EncryptNew(LTrim(Me.txtPassword.Text))
                drNewRow("firstpassword") = EncryptNew(LTrim(Me.txtPassword.Text))
                drNewRow("passwordexpirydays") = CInt(txtPassExpDays.Text)
                drNewRow("passwordexpirydate") = Convert.ToDateTime(DoConvertToDbDateFormat(txtPassExpDate.Text))
                drNewRow("SEC_USER_PHONE1") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                drNewRow("SEC_USER_PHONE2") = Left(LTrim(Me.txtCustPhone02.Text), 11)
                drNewRow("SEC_USER_EMAIL1") = Left(LTrim(Me.txtCustEmail01.Text), 49)
                drNewRow("SEC_USER_EMAIL2") = Left(LTrim(Me.txtCustEmail02.Text), 49)

                drNewRow("SEC_USER_FLAG") = status
                drNewRow("SEC_USER_OPERID") = CType(myUserIDX, String)
                drNewRow("SEC_USER_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMessage.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record
                If chkStatus.Checked = True Then
                    status = "C"
                Else
                    status = "X"
                End If
                With obj_DT
                    .Rows(0)("SEC_USER_ID") = RTrim(Me.txtUserID.Text)
                    .Rows(0)("SEC_USER_LOGIN") = RTrim(Me.txtLoginName.Text)
                    .Rows(0)("SEC_USER_NAME") = RTrim(Me.txtName.Text)
                    .Rows(0)("SEC_USER_SHRT_NAME") = RTrim(Me.txtShortName.Text)
                    .Rows(0)("SEC_USER_GROUP_CODE") = RTrim(Me.txtGroup.Text)
                    .Rows(0)("SEC_USER_ROLE") = LTrim(Me.cboRole.Text)
                    .Rows(0)("SEC_USER_BRANCH") = LTrim(Me.txtBranch.Text)
                    .Rows(0)("SEC_USER_PASSWORD") = EncryptNew(LTrim(Me.txtPassword.Text))
                    .Rows(0)("SEC_USER_PHONE1") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                    .Rows(0)("SEC_USER_PHONE2") = Left(LTrim(Me.txtCustPhone02.Text), 11)
                    .Rows(0)("SEC_USER_EMAIL1") = Left(LTrim(Me.txtCustEmail01.Text), 49)
                    .Rows(0)("SEC_USER_EMAIL2") = Left(LTrim(Me.txtCustEmail02.Text), 49)
                    .Rows(0)("SEC_USER_FLAG") = status
                    '.Rows(0)("TBIL_CUST_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_CUST_KEYDTE") = Now
                End With
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMessage.Text = "Record Saved to Database Successfully."

            End If

        Catch ex As Exception
            Me.lblMessage.Text = ex.Message.ToString
            Exit Sub
        End Try

OnlyAdmin:
        m_cbCommandBuilder.Dispose()
        m_cbCommandBuilder = Nothing

        obj_DT.Dispose()
        obj_DT = Nothing

        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        'Me.lblMessage.Text = ""

        Me.txtSearch.Value = RTrim(Me.txtName.Text)

        'Call Proc_Populate_Box("IL_BRK_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
        Call Proc_DataBind()
        Me.txtSearch.Value = ""

        DoNew()

        Me.txtName.Enabled = True
        Me.txtName.Focus()

    End Sub

    Protected Sub DoDelete()

        Dim strMyVal As String
        strMyVal = RTrim(Me.txtUserID.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustID.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtShortName.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblFullName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        strTable = strTableName

        strREC_ID = Trim(Me.txtLoginName.Text)

        strSQL = "SELECT TOP 1 SEC_USER_LOGIN FROM " & strTable
        strSQL = strSQL & " WHERE SEC_USER_LOGIN = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND SEC_USER_ID = '" & RTrim(Me.txtUserID.Text) & "'"

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
                'Me.lblMessage.Text = "Deleting record... "
                strSQL = ""
                strSQL = "DELETE FROM " & strTable
                strSQL = strSQL & " WHERE SEC_USER_LOGIN = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND SEC_USER_ID = '" & RTrim(Me.txtUserID.Text) & "'"

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
            Me.lblMessage.Text = "Record deleted successfully."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        Else
            Me.lblMessage.Text = "Sorry!. Record not deleted..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        End If
        'Me.lblMessage.Text = ""

        'Call Proc_Populate_Box("IL_INS_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
        'Call Proc_DataBind()

        Me.cmdDelete_ASP.Enabled = False

        Call DoNew()
        Me.txtName.Enabled = True
        Me.txtName.Focus()

    End Sub

    'Protected Sub cboTransList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTransList.SelectedIndexChanged
    '    Me.txtCustNum.Text = RTrim(Me.cboTransList.SelectedItem.Value)
    '    If RTrim(Me.txtCustNum.Text) = "*" Or RTrim(Me.txtCustNum.Text) = "" Or RTrim(Me.txtCustNum.Text) = "0" Then
    '        Me.txtCustNum.Text = ""
    '        Call DoNew()
    '        Exit Sub
    '    End If

    '    If RTrim(Me.txtCustNum.Text) <> "" Then
    '        lblMessage.Text = RTrim(Me.txtCustNum.Text)
    '        strREC_ID = RTrim(Me.txtCustNum.Text)
    '        strErrMsg = Proc_OpenRecord(Me.txtCustNum.Text)
    '    End If

    'End Sub

    Private Function Proc_OpenRecord(ByVal strRefNo As String) As String

        On Error GoTo myRtn_Err

        strErrMsg = "false"

        lblMessage.Text = ""
        If Trim(strRefNo) = "" Then
            Proc_OpenRecord = strErrMsg
            Return Proc_OpenRecord
        End If

        strREC_ID = Trim(strRefNo)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 TRN.*"
        strSQL = strSQL & " FROM " & strTable & " AS TRN"
        strSQL = strSQL & " WHERE TRN.SEC_USER_LOGIN = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TRN.SEC_USER_ID = '" & RTrim(Me.txtUserID.Text) & "'"
        'strSQL = strSQL & " AND TBIL_CUST_CLASS_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"

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
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("SEC_USER_REC_ID") & vbNullString, String))
            Me.txtUserID.Text = RTrim(CType(objOLEDR("SEC_USER_ID") & vbNullString, String))
            Me.cboRole.Text = RTrim(CType(objOLEDR("SEC_USER_ROLE") & vbNullString, String))
            Me.txtShortName.Text = RTrim(CType(objOLEDR("SEC_USER_SHRT_NAME") & vbNullString, String))
            Me.txtName.Text = RTrim(CType(objOLEDR("SEC_USER_NAME") & vbNullString, String))
            Me.txtBranch.Text = RTrim(CType(objOLEDR("SEC_USER_BRANCH") & vbNullString, String))
            Me.txtCustPhone01.Text = RTrim(CType(objOLEDR("SEC_USER_PHONE1") & vbNullString, String))
            Me.txtCustPhone02.Text = RTrim(CType(objOLEDR("SEC_USER_PHONE2") & vbNullString, String))
            Me.txtCustEmail01.Text = RTrim(CType(objOLEDR("SEC_USER_EMAIL1") & vbNullString, String))
            Me.txtCustEmail02.Text = RTrim(CType(objOLEDR("SEC_USER_EMAIL2") & vbNullString, String))

            If RTrim(CType(objOLEDR("SEC_USER_FLAG") & vbNullString, String)) = "A" Or RTrim(CType(objOLEDR("SEC_USER_FLAG") & vbNullString, String)) = "C" Then
                chkStatus.Checked = True
            End If



            Me.txtGroup.Text = RTrim(CType(objOLEDR("SEC_USER_GROUP_CODE") & vbNullString, String))

            'Call Proc_DDL_Get(Me.cboGroup, RTrim(Me.txtGroup.Text))
            'This will be done dynamic later
            If txtGroup.Text = "admin" Then
                'cboGroup.Text = "Administrators"
                cboGroup.Text = "admin"
                cboGroup.SelectedValue = "admin"
            ElseIf txtGroup.Text = "mgt" Then
                cboGroup.Text = "mgt"
            ElseIf txtGroup.Text = "sysuser" Then
                cboGroup.Text = "sysuser"
            End If

            Call DisableBox(Me.txtLoginName)
            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True
        Else
            'Me.txtCustNum.Text = ""
            Me.cmdDelete_ASP.Enabled = False
            strErrMsg = "Status: New Entry..."
            Me.txtName.Enabled = True
            Me.txtName.Focus()
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

        lblMessage.Text = strErrMsg
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
        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        Me.lblMessage.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(2).Text

        Me.txtUserID.Text = row.Cells(3).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))

        Me.txtLoginName.Text = row.Cells(4).Text
        'Call Proc_DDL_Get(Me.cboTransList, RTrim(Me.txtCustNum.Text))

        Call Proc_OpenRecord(Me.txtLoginName.Text)

        lblMessage.Text = "You selected " & Me.txtLoginName.Text & " / " & Me.txtRecNo.Text & "."
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If Trim(Me.txtSearch.Value) = "" Or Trim(Me.txtSearch.Value) = "." Or Trim(Me.txtSearch.Value) = "*" Then
        Else
            Call Proc_DataBind()
        End If
    End Sub

    Protected Sub cboGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGroup.SelectedIndexChanged
        Me.txtGroup.Text = RTrim(Me.cboGroup.SelectedItem.Value)
        If RTrim(Me.txtGroup.Text) = "*" Or RTrim(Me.txtGroup.Text) = "" Or RTrim(Me.txtGroup.Text) = "0" Then
            Me.txtGroup.Text = ""
            Exit Sub
        End If
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        txtName.Text = ""
        txtShortName.Text = ""
        txtGroup.Text = ""
        cboGroup.Text = "*"
        cboRole.Text = "*** Select ***"
        txtBranch.Text = ""
        txtCustPhone01.Text = ""
        txtCustPhone02.Text = ""
        txtCustEmail01.Text = ""
        txtCustEmail02.Text = ""
        txtLoginName.Text = ""
        txtPassword.Text = ""
        txtConPassword.Text = ""
        txtLoginName.Enabled = True
        txtLoginName.ReadOnly = False

    End Sub
End Class