Imports System.Data
Imports System.Data.OleDb

Partial Class I_LIFE_PRG_LI_BRK_DTl
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_CUST_DETAIL"

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

        strP_ID = "L01"
        Me.txtCustID.Text = RTrim(strP_TYPE)
        Me.txtCustID.Text = RTrim("001")

        If Not (Page.IsPostBack) Then
            Call Proc_Populate_Box("IL_BRK_MODULE_LIST", Trim("001"), Me.cboCustModule)
            Call Proc_Populate_Box("IL_BRK_CLASS_LIST", Trim("001"), Me.cboCustCateg)
            Call Proc_Populate_Box("IL_BRK_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
            Call Proc_DataBind()
            Call DoNew()
            'Me.lblMessage.Text = strSQL
            Me.txtAction.Text = ""
            'Me.txtCustNum.Enabled = True
            'Me.txtCustNum.Focus()
            Me.txtCustName.Enabled = True
            Me.txtCustName.Focus()
        End If

        If Me.txtAction.Text = "New" Then
            Call DoNew()
            'Call Proc_OpenRecord(Me.txtNum.Text)
            Me.txtAction.Text = ""
            'Me.txtCustNum.Enabled = True
            'Me.txtCustNum.Focus()
            Me.txtCustName.Enabled = True
            Me.txtCustName.Focus()
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

    Protected Sub txtCustNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCustNum.TextChanged
        If RTrim(Me.txtCustNum.Text) <> "" Then
            lblMessage.Text = RTrim(Me.txtCustNum.Text)
            strREC_ID = RTrim(Me.txtCustNum.Text)
            strErrMsg = Proc_OpenRecord(Me.txtCustNum.Text)
        End If

    End Sub

    Protected Sub DoNew()
        Call Proc_DDL_Get(Me.cboCustModule, RTrim("*"))
        Call Proc_DDL_Get(Me.cboCustCateg, RTrim("*"))
        Call Proc_DDL_Get(Me.cboTransList, RTrim("*"))

        Me.cboCustModule.SelectedIndex = 0
        Me.cboCustCateg.SelectedIndex = 0
        Me.cboTransList.SelectedIndex = 0

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

            .txtCustID.Enabled = False
            .chkNum.Checked = True
            .chkNum.Enabled = True

            .txtCustModule.Text = ""
            .txtCustCateg.Text = ""

            .txtCustNum.ReadOnly = False
            .txtCustNum.Enabled = True
            .txtCustNum.Enabled = False
            .txtCustNum.Text = ""
            .cboTransList.Enabled = False

            .txtCustName.Text = ""
            .txtCustAddr01.Text = ""
            .txtCustAddr02.Text = ""
            .txtCustPhone01.Text = ""
            .txtCustPhone02.Text = ""
            .txtCustEmail01.Text = ""
            .txtCustEmail02.Text = ""

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
        strSQL = strSQL & "SELECT TBIL_CUST_REC_ID, TBIL_CUST_ID, TBIL_CUST_CODE"
        strSQL = strSQL & ",RTRIM(ISNULL(TBIL_CUST_DESC,'')) AS TBIL_CUST_FULL_NAME"
        strSQL = strSQL & ",RTRIM(ISNULL(TBIL_CUST_PHONE1,'')) + ' ' + RTRIM(ISNULL(TBIL_CUST_PHONE2,'')) AS TBIL_CUST_PHONE_NUM"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"
        'strSQL = strSQL & " AND TBIL_CUST_MDLE = '" & RTrim(Me.txtCustModule.Text) & "'"
        strSQL = strSQL & " AND (TBIL_CUST_DESC LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
        strSQL = strSQL & " ORDER BY TBIL_CUST_ID, RTRIM(ISNULL(TBIL_CUST_DESC,''))"

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
        lblMessage.Text = ""
        Dim strMyVal As String


        Try
            If Me.cboCustModule.SelectedIndex >= 1 Then
                'Me.txtCustModule.Text = cboCustModule.SelectedValue
                Me.txtCustModule.Text = cboCustModule.SelectedItem.Value
            End If
        Catch ex As Exception
        End Try

        Try
            If Me.cboCustCateg.SelectedIndex >= 1 Then
                'Me.txtCustCateg.Text = cboCustCateg.SelectedValue
                Me.txtCustCateg.Text = cboCustCateg.SelectedItem.Value
            End If
        Catch ex As Exception
        End Try


        strMyVal = RTrim(Me.txtCustID.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustID.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtCustModule.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustModule.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtCustCateg.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustCateg.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        strMyVal = RTrim(Me.txtCustNum.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            'Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        End If

        If Trim(Me.txtCustName.Text) = "" Or RTrim(Me.txtCustName.Text) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustName.Text
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
            If IsNumeric(LTrim(RTrim(Me.txtCustPhone02.Text))) And Len(LTrim(RTrim(Me.txtCustPhone01.Text))) = 7 Then
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
        strSQL = "SELECT TOP 1 TBIL_CUST_CODE FROM " & strTable
        strSQL = strSQL & " WHERE RTRIM(ISNULL(TBIL_CUST_DESC,'')) = '" & RTrim(Me.txtCustName.Text) & "'"
        strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtCustNum.Text) <> Trim(chk_objOLEDR("TBIL_CUST_CODE") & vbNullString) Then
                Me.lblMessage.Text = "Warning!. The code description you enter already exist..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("TBIL_CUST_CODE") & vbNullString)
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


        If Trim(Me.txtCustNum.Text) = "" Then

        End If

        If Trim(Me.txtCustNum.Text) = "" Then
            Me.txtCustNum.Text = MOD_GEN.gnGet_Serial_Und("GET_SN_IL_BRK", Trim("BRK"), Trim("BRK"), "XXXX", "XXXX", "BR")
            If Trim(txtCustNum.Text) = "" Or Trim(Me.txtCustNum.Text) = "0" Or Trim(Me.txtCustNum.Text) = "*" Then
                Me.txtCustNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get the next record id. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtCustNum.Text) = "PARAM_ERR" Then
                Me.txtCustNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get the next record id - INVALID PARAMETER(S)"
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtCustNum.Text) = "DB_ERR" Then
                Me.txtCustNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtCustNum.Text) = "ERR_ERR" Then
                Me.txtCustNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            End If

        End If

        strREC_ID = Trim(Me.txtCustNum.Text)

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


        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_CUST_CODE = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"

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

                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()

                drNewRow("TBIL_CUST_ID") = RTrim(Me.txtCustID.Text)
                drNewRow("TBIL_CUST_CODE") = RTrim(Me.txtCustNum.Text)

                drNewRow("TBIL_CUST_MDLE") = RTrim(Me.txtCustModule.Text)
                drNewRow("TBIL_CUST_CATG") = RTrim(Me.txtCustCateg.Text)

                drNewRow("TBIL_CUST_DESC") = Left(RTrim(Me.txtCustName.Text), 49)

                drNewRow("TBIL_CUST_ADRES1") = Left(LTrim(Me.txtCustAddr01.Text), 39)
                drNewRow("TBIL_CUST_ADRES2") = Left(LTrim(Me.txtCustAddr02.Text), 39)
                drNewRow("TBIL_CUST_PHONE1") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                drNewRow("TBIL_CUST_PHONE2") = Left(LTrim(Me.txtCustPhone02.Text), 11)
                drNewRow("TBIL_CUST_EMAIL1") = Left(LTrim(Me.txtCustEmail01.Text), 49)
                drNewRow("TBIL_CUST_EMAIL2") = Left(LTrim(Me.txtCustEmail02.Text), 49)

                drNewRow("TBIL_CUST_FLAG") = "A"
                drNewRow("TBIL_CUST_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_CUST_KEYDTE") = Now

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
                    .Rows(0)("TBIL_CUST_ID") = RTrim(Me.txtCustID.Text)
                    .Rows(0)("TBIL_CUST_CODE") = RTrim(Me.txtCustNum.Text)

                    .Rows(0)("TBIL_CUST_MDLE") = RTrim(Me.txtCustModule.Text)
                    .Rows(0)("TBIL_CUST_CATG") = RTrim(Me.txtCustCateg.Text)

                    .Rows(0)("TBIL_CUST_DESC") = UCase(Left(RTrim(Me.txtCustName.Text), 49))

                    .Rows(0)("TBIL_CUST_ADRES1") = Left(LTrim(Me.txtCustAddr01.Text), 39)
                    .Rows(0)("TBIL_CUST_ADRES2") = Left(LTrim(Me.txtCustAddr02.Text), 39)
                    .Rows(0)("TBIL_CUST_PHONE1") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                    .Rows(0)("TBIL_CUST_PHONE2") = Left(LTrim(Me.txtCustPhone02.Text), 11)
                    .Rows(0)("TBIL_CUST_EMAIL1") = Left(LTrim(Me.txtCustEmail01.Text), 49)
                    .Rows(0)("TBIL_CUST_EMAIL2") = Left(LTrim(Me.txtCustEmail02.Text), 49)

                    .Rows(0)("TBIL_CUST_FLAG") = "C"
                    '.Rows(0)("TBIL_CUST_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_CUST_KEYDTE") = Now
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

        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        'Me.lblMessage.Text = ""

        Me.txtSearch.Value = RTrim(Me.txtCustName.Text)

        'Call Proc_Populate_Box("IL_BRK_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
        Call Proc_DataBind()
        Me.txtSearch.Value = ""

        DoNew()

        Me.txtCustName.Enabled = True
        Me.txtCustName.Focus()

    End Sub

    Protected Sub DoDelete()

        Dim strMyVal As String
        strMyVal = RTrim(Me.txtCustID.Text)
        If RTrim(strMyVal) = "" Or RTrim(strMyVal) = "*" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblCustID.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtCustNum.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCustNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Long = 0

        strTable = strTableName

        strREC_ID = Trim(Me.txtCustNum.Text)

        strSQL = "SELECT TOP 1 TBIL_CUST_CODE FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_CUST_CODE = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"

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
                strSQL = strSQL & " WHERE TBIL_CUST_CODE = '" & RTrim(strREC_ID) & "'"
                strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"

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
            lblMessage.Text = RTrim(Me.txtCustNum.Text)
            strREC_ID = RTrim(Me.txtCustNum.Text)
            strErrMsg = Proc_OpenRecord(Me.txtCustNum.Text)
        End If

    End Sub


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
        strSQL = strSQL & " WHERE TRN.TBIL_CUST_CODE = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TRN.TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"
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
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_CUST_REC_ID") & vbNullString, String))
            Me.txtCustID.Text = RTrim(CType(objOLEDR("TBIL_CUST_ID") & vbNullString, String))

            Me.txtCustModule.Text = RTrim(CType(objOLEDR("TBIL_CUST_MDLE") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboCustModule, RTrim(Me.txtCustModule.Text))
            Me.txtCustCateg.Text = RTrim(CType(objOLEDR("TBIL_CUST_CATG") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboCustCateg, RTrim(Me.txtCustCateg.Text))

            Me.txtCustNum.Text = RTrim(CType(objOLEDR("TBIL_CUST_CODE") & vbNullString, String))
            Me.txtCustName.Text = RTrim(CType(objOLEDR("TBIL_CUST_DESC") & vbNullString, String))

            Me.txtCustAddr01.Text = RTrim(CType(objOLEDR("TBIL_CUST_ADRES1") & vbNullString, String))
            Me.txtCustAddr02.Text = RTrim(CType(objOLEDR("TBIL_CUST_ADRES2") & vbNullString, String))
            Me.txtCustPhone01.Text = RTrim(CType(objOLEDR("TBIL_CUST_PHONE1") & vbNullString, String))
            Me.txtCustPhone02.Text = RTrim(CType(objOLEDR("TBIL_CUST_PHONE2") & vbNullString, String))
            Me.txtCustEmail01.Text = RTrim(CType(objOLEDR("TBIL_CUST_EMAIL1") & vbNullString, String))
            Me.txtCustEmail02.Text = RTrim(CType(objOLEDR("TBIL_CUST_EMAIL2") & vbNullString, String))

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

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex
        Call Proc_DataBind()
        Me.lblMessage.Text = "Page " & GridView1.PageIndex + 1 & " of " & Me.GridView1.PageCount

    End Sub

    Private Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtRecNo.Text = row.Cells(2).Text

        Me.txtCustID.Text = row.Cells(3).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))

        Me.txtCustNum.Text = row.Cells(4).Text
        Call Proc_DDL_Get(Me.cboTransList, RTrim(Me.txtCustNum.Text))

        Call Proc_OpenRecord(Me.txtCustNum.Text)

        lblMessage.Text = "You selected " & Me.txtCustNum.Text & " / " & Me.txtRecNo.Text & "."

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If Trim(Me.txtSearch.Value) = "" Or Trim(Me.txtSearch.Value) = "." Or Trim(Me.txtSearch.Value) = "*" Then
        Else
            Call Proc_DataBind()
        End If
    End Sub

    Private Sub cboCustModule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustModule.SelectedIndexChanged
        Me.txtCustModule.Text = RTrim(Me.cboCustModule.SelectedItem.Value)
        If RTrim(Me.txtCustModule.Text) = "*" Or RTrim(Me.txtCustModule.Text) = "" Or RTrim(Me.txtCustModule.Text) = "0" Then
            Me.txtCustModule.Text = ""
            Exit Sub
        End If

    End Sub

    Private Sub cboCustCateg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustCateg.SelectedIndexChanged
        Me.txtCustCateg.Text = RTrim(Me.cboCustCateg.SelectedItem.Value)
        If RTrim(Me.txtCustCateg.Text) = "*" Or RTrim(Me.txtCustCateg.Text) = "" Or RTrim(Me.txtCustCateg.Text) = "0" Then
            Me.txtCustCateg.Text = ""
            Exit Sub
        End If

    End Sub

    Protected Sub chkNum_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkNum.CheckedChanged
        If Me.chkNum.Checked = True Then
            Me.txtCustNum.Enabled = False
            Me.cboTransList.Enabled = False
        Else
            Me.txtCustNum.Enabled = True
            Me.cboTransList.Enabled = True
        End If
    End Sub

End Class
