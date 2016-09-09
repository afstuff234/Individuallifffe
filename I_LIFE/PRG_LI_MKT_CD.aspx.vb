Imports System.Data
Imports System.Data.OleDb

Partial Class I_LIFE_PRG_LI_MKT_CD
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
        strTableName = "TBIL_AGENCY_CD"

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

        Me.txtCustID.Text = RTrim(strP_TYPE)


        If Not (Page.IsPostBack) Then
            Call gnPopulate_DropDownList("IL_INS_AGENCY_TYPE_LIST", Me.cboAgcy_Type, strSQL, "", "(Select item)", "*")
            Call gnPopulate_DropDownList("IL_MKT_REGION_LIST", Me.cboAgcy_Region_Name, strSQL, "", "(Select item)", "*")
            'Call gnProc_Populate_Box("IL_MKT_REGION_LIST_SP", "001", Me.cboAgcy_Region_Name, RTrim("001"))
            Call gnProc_Populate_Box("IL_MKT_AGENCY_LIST_SP", "001", Me.cboAgcy_Agency_List, RTrim("001"))
            Call gnProc_Populate_Box("IL_MKT_UNIT_LIST_SP", "001", Me.cboAgcy_Unit_List, RTrim("001"))
            Call gnProc_Populate_Box("IL_MKT_AGENT_LIST_SP", "001", Me.cboTransList, RTrim("001"))
            Call gnProc_Populate_Box("IL_CODE_LIST", "006", Me.cboLocation)
            Call DoNew()
        End If

        If Me.txtAction.Text = "New" Then
            Call DoNew()
            'Call Proc_OpenRecord(Me.txtNum.Text)
            Me.lblMessage.Text = "New Entry"
            Me.txtAction.Text = ""
            'Me.txtNum.Enabled = True
            'Me.txtNum.Focus()
            'Me.txtCustName.Enabled = True
            'Me.txtCustName.Focus()
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

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call DoSave()
        Me.txtAction.Text = ""

    End Sub

    Protected Sub DoProc_Agcy_Region_Change()
        'Call gnGET_SelectedItem(Me.cboAgcy_Region_Name, Me.txtAgcy_Region_Num, Me.txtAgcy_Region_Name, Me.lblMessage)
        Try

            If cboAgcy_Region_Name.SelectedIndex = -1 Or cboAgcy_Region_Name.SelectedIndex = 0 Or _
                cboAgcy_Region_Name.SelectedItem.Value = "" Or cboAgcy_Region_Name.SelectedItem.Value = "*" Then
                txtAgcy_Region_Num.Text = ""
                'txtAgcy_Region_Name.Text = ""
            Else
                txtAgcy_Region_Num.Text = cboAgcy_Region_Name.SelectedItem.Value
                txtAgcy_Region_Name.Text = cboAgcy_Region_Name.SelectedItem.Text
            End If
        Catch ex As Exception
            Me.lblMessage.Text = "Error. Reason: " & ex.Message.ToString
        End Try

    End Sub

    Protected Sub DoProc_Agcy_Agency_Change()
        Call gnGET_SelectedItem(Me.cboAgcy_Agency_List, Me.txtAgcy_Agcy_Num, Me.txtAgcy_Agcy_Name, Me.lblMessage)
    End Sub

    Protected Sub DoProc_Agcy_Unit_Change()
        Call gnGET_SelectedItem(Me.cboAgcy_Unit_List, Me.txtAgcy_Unit_Num, Me.txtAgcy_Unit_Name, Me.lblMessage)
    End Sub

    Protected Sub DoProc_Agcy_Agent_Change()
        Call gnGET_SelectedItem(Me.cboTransList, Me.txtNum, Me.txtCustName, Me.lblMessage)
        If Trim(Me.txtNum.Text) <> "" Then
            Call Proc_OpenRecord(Me.txtNum.Text)
        End If
    End Sub

    'Protected Sub txtNum_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtNum.TextChanged
    '    If Trim(Me.txtNum.Text) <> "" And Trim(Me.txtCustID.Text) <> "" Then
    '        Call Proc_OpenRecord(Trim(Me.txtNum.Text))
    '    End If

    'End Sub

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

        Call Proc_DDL_Get(Me.cboAgcy_Region_Name, RTrim("*"))
        Call Proc_DDL_Get(Me.cboAgcy_Agency_List, RTrim("*"))
        Call Proc_DDL_Get(Me.cboAgcy_Unit_List, RTrim("*"))

        Call Proc_DDL_Get(Me.cboTransList, RTrim("*"))
        Call Proc_DDL_Get(Me.cboAgcy_Type, RTrim("*"))
        Call Proc_DDL_Get(Me.cboLocation, RTrim("*"))

        'Me.lblShortName.Visible = False
        'Me.txtShortName.Visible = False

        Me.cmdGetRecord.Enabled = False
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

            .txtAgcy_Region_Num.Text = ""
            .txtAgcy_Region_Name.Text = ""

            .txtAgcy_Agcy_Num.Text = ""
            .txtAgcy_Agcy_Name.Text = ""

            .txtAgcy_Unit_Num.Text = ""
            .txtAgcy_Unit_Name.Text = ""

            .txtAgcy_Type.Text = ""
            .txtAgcy_Unit_Name.Text = ""

            .txtCustName.Text = ""
            '.lblTransName.Text = "Trans Name:"
            .txtCustAddr01.Text = ""
            .txtCustAddr02.Text = ""
            .txtCustPhone01.Text = ""
            .txtCustPhone02.Text = ""
            .txtCustEmail01.Text = ""
            .txtCustEmail02.Text = ""

            .cmdDelete_ASP.Enabled = False
            .lblMessage.Text = "Status: New Entry..."
        End With

    End Sub


    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TBIL_AGCY_AGENT_REC_ID, TBIL_AGCY_AGENT_ID"
        strSQL = strSQL & ",TBIL_AGCY_REG_CODE, TBIL_AGCY_REG_NAME"
        strSQL = strSQL & ",TBIL_AGCY_AGCY_CODE, TBIL_AGCY_AGCY_NAME"
        strSQL = strSQL & ",TBIL_AGCY_UNIT_CODE, TBIL_AGCY_UNIT_NAME"
        strSQL = strSQL & ",TBIL_AGCY_AGENT_CD ,TBIL_AGCY_AGENT_NAME"
        strSQL = strSQL & ",RTRIM(ISNULL(TBIL_AGENT_PHONE1,'')) + ' ' + RTRIM(ISNULL(TBIL_AGENT_PHONE2,'')) AS TBIL_AGENT_PHONE_NUM"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_AGCY_AGENT_ID = '" & RTrim(Me.txtCustID.Text) & "'"
        strSQL = strSQL & " AND (TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
        strSQL = strSQL & " ORDER BY TBIL_AGCY_AGENT_ID, RTRIM(ISNULL(TBIL_AGCY_AGENT_NAME,''))"

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

        If RTrim(Me.txtCustID.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCustID.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.txtAgcy_Region_Num.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblAgcy_Region_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        If RTrim(Me.txtAgcy_Region_Name.Text) = "" Then
            'Me.lblMessage.Text = "Missing " & Me.lblAgcy_Region_Name.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        End If

        If RTrim(Me.txtAgcy_Agcy_Num.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblAgcy_Agcy_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        If RTrim(Me.txtAgcy_Agcy_Name.Text) = "" Then
            'Me.lblMessage.Text = "Missing " & Me.lblAgcy_Agcy_Name.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        End If

        If RTrim(Me.txtAgcy_Unit_Num.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblAgcy_Unit_Num.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If
        If RTrim(Me.txtAgcy_Unit_Name.Text) = "" Then
            'Me.lblMessage.Text = "Missing " & Me.lblAgcy_Unit_Name.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        End If


        If RTrim(Me.txtNum.Text) = "" Then
            'Me.lblMessage.Text = "Missing " & Me.lblNum.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        End If

        If RTrim(Me.txtCustName.Text) = "" Or RTrim(Me.txtCustName.Text) = "." Or RTrim(Me.txtCustName.Text) = "*" Then
            Me.lblMessage.Text = "Invalid or Missing " & Me.lblCustName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Try
            If Me.cboAgcy_Type.SelectedIndex = -1 Or Me.cboAgcy_Type.SelectedIndex = 0 Or _
               Me.cboAgcy_Type.SelectedItem.Value = "" Or Me.cboAgcy_Type.SelectedItem.Value = "*" Then
                Me.txtAgcy_Type.Text = ""
                Me.lblMessage.Text = "Missing " & Me.lblAgcy_Type.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub
            Else
                Me.txtAgcy_Type.Text = Me.cboAgcy_Type.SelectedItem.Value
                Me.txtAgcy_Type_Name.Text = Me.cboAgcy_Type.SelectedItem.Text
            End If
        Catch ex As Exception

        End Try



        'Try
        '    If Me.cboLocation.SelectedIndex = -1 Or Me.cboLocation.SelectedIndex = 0 Or _
        '       Me.cboAgcy_Type.SelectedItem.Value = "" Or Me.cboLocation.SelectedItem.Value = "*" Then
        '        Me.txtLoc_Num.Text = ""
        '        Me.lblMessage.Text = "Missing " & Me.lblLoc_Num.Text
        '        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '        Exit Sub
        '    Else
        '        Me.txtLoc_Num.Text = Me.cboLocation.SelectedItem.Value
        '    End If
        'Catch ex As Exception

        'End Try


        If LTrim(RTrim(Me.txtCustPhone01.Text)) = "" Then
            'Me.lblMessage.Text = "Missing " & Me.lblCustPhone01.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtCustPhone01.Text))) And Len(LTrim(RTrim(Me.txtCustPhone01.Text))) = 11 Then
            Else
                Me.lblMessage.Text = "Incorrect or Invalid " & Me.lblCustPhone01.Text
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Exit Sub
            End If
        End If

        If LTrim(RTrim(Me.txtCustPhone02.Text)) = "" Then
            'Me.textMessage.Text = "Missing " & Me.lblCustPhone02.Text
            'FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
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
        strSQL = strSQL & " WHERE TBIL_AGCY_AGENT_NAME = '" & LTrim(Me.txtCustName.Text) & "'"
        'strSQL = strSQL & " AND TBIL_AGENT_ID = '" & RTrim(txtCustID.Text) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtNum.Text) <> Trim(chk_objOLEDR("TBIL_AGCY_AGENT_CD") & vbNullString) Then
                Me.lblMessage.Text = "Warning!. The Account Name you enter already exist..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("TBIL_AGCY_AGENT_CD") & vbNullString)
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
            Me.txtNum.Text = MOD_GEN.gnGet_Serial_Und("GET_SN_IL_MKT", Trim("MKT"), Trim("MKT"), "XXXX", "XXXX", "MR")
            If Trim(txtNum.Text) = "" Or Trim(Me.txtNum.Text) = "0" Or Trim(Me.txtNum.Text) = "*" Then
                Me.txtNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get the next record id. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                Me.lblMessage.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtNum.Text) = "PARAM_ERR" Then
                Me.txtNum.Text = ""
                Me.lblMessage.Text = "Sorry!. Unable to get the next record id - INVALID PARAMETER(S)"
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


        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_AGCY_AGENT_CD = '" & RTrim(txtNum.Text) & "'"
        strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(txtCustID.Text) & "'"
        If Val(Trim(Me.txtRecNo.Text)) <> 0 Then
            strSQL = strSQL & " AND TBIL_AGCY_AGENT_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"

        End If

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

                drNewRow("TBIL_AGCY_AGENT_ID") = RTrim(Me.txtCustID.Text)
                drNewRow("TBIL_AGCY_CD_MDLE") = RTrim("I")

                drNewRow("TBIL_AGCY_REG_CODE") = RTrim(Me.txtAgcy_Region_Num.Text)
                drNewRow("TBIL_AGCY_REG_NAME") = RTrim(Me.txtAgcy_Region_Name.Text)

                drNewRow("TBIL_AGCY_AGCY_CODE") = RTrim(Me.txtAgcy_Agcy_Num.Text)
                drNewRow("TBIL_AGCY_AGCY_NAME") = RTrim(Me.txtAgcy_Agcy_Name.Text)

                drNewRow("TBIL_AGCY_UNIT_CODE") = RTrim(Me.txtAgcy_Unit_Num.Text)
                drNewRow("TBIL_AGCY_UNIT_NAME") = RTrim(Me.txtAgcy_Unit_Name.Text)

                drNewRow("TBIL_AGCY_AGENT_CD") = RTrim(Me.txtNum.Text)
                drNewRow("TBIL_AGCY_AGENT_NAME") = RTrim(Me.txtCustName.Text)

                drNewRow("TBIL_AGCY_TYPE") = RTrim(Me.txtAgcy_Type.Text)
                drNewRow("TBIL_AGCY_TYPE_DESC") = RTrim(Me.txtAgcy_Type_Name.Text)

                drNewRow("TBIL_AGENT_ADRES1") = Left(LTrim(Me.txtCustAddr01.Text), 39)
                drNewRow("TBIL_AGENT_ADRES2") = Left(LTrim(Me.txtCustAddr02.Text), 39)
                drNewRow("TBIL_AGENT_PHONE1") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                drNewRow("TBIL_AGENT_PHONE2") = Left(LTrim(Me.txtCustPhone02.Text), 11)
                drNewRow("TBIL_AGENT_EMAIL1") = Left(LTrim(Me.txtCustEmail01.Text), 49)
                drNewRow("TBIL_AGENT_EMAIL2") = Left(LTrim(Me.txtCustEmail02.Text), 49)

                drNewRow("TBIL_AGENT_FLAG") = "A"
                drNewRow("TBIL_AGENT_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_AGENT_KEYDTE") = Now

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
                    .Rows(0)("TBIL_AGCY_AGENT_ID") = RTrim(Me.txtCustID.Text)
                    .Rows(0)("TBIL_AGCY_CD_MDLE") = RTrim("I")

                    .Rows(0)("TBIL_AGCY_REG_CODE") = RTrim(Me.txtAgcy_Region_Num.Text)
                    .Rows(0)("TBIL_AGCY_REG_NAME") = RTrim(Me.txtAgcy_Region_Name.Text)

                    .Rows(0)("TBIL_AGCY_AGCY_CODE") = RTrim(Me.txtAgcy_Agcy_Num.Text)
                    .Rows(0)("TBIL_AGCY_AGCY_NAME") = RTrim(Me.txtAgcy_Agcy_Name.Text)

                    .Rows(0)("TBIL_AGCY_UNIT_CODE") = RTrim(Me.txtAgcy_Unit_Num.Text)
                    .Rows(0)("TBIL_AGCY_UNIT_NAME") = RTrim(Me.txtAgcy_Unit_Name.Text)

                    .Rows(0)("TBIL_AGCY_AGENT_CD") = RTrim(Me.txtNum.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_NAME") = RTrim(Me.txtCustName.Text)

                    .Rows(0)("TBIL_AGCY_TYPE") = RTrim(Me.txtAgcy_Type.Text)
                    .Rows(0)("TBIL_AGCY_TYPE_DESC") = RTrim(Me.txtAgcy_Type_Name.Text)

                    .Rows(0)("TBIL_AGENT_ADRES1") = Left(LTrim(Me.txtCustAddr01.Text), 39)
                    .Rows(0)("TBIL_AGENT_ADRES2") = Left(LTrim(Me.txtCustAddr02.Text), 39)
                    .Rows(0)("TBIL_AGENT_PHONE1") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                    .Rows(0)("TBIL_AGENT_PHONE2") = Left(LTrim(Me.txtCustPhone02.Text), 11)
                    .Rows(0)("TBIL_AGENT_EMAIL1") = Left(LTrim(Me.txtCustEmail01.Text), 49)
                    .Rows(0)("TBIL_AGENT_EMAIL2") = Left(LTrim(Me.txtCustEmail02.Text), 49)

                    .Rows(0)("TBIL_AGENT_FLAG") = "C"
                    '.Rows(0)("TBIL_AGENT_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_AGENT_KEYDTE") = Now
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

        Me.txtNum.Enabled = False
        Me.chkNum.Enabled = False

        Me.txtSearch.Value = RTrim(Me.txtCustName.Text)
        Call Proc_DataBind()
        Me.txtSearch.Value = ""

        Call DoNew()


        'Me.txtNum.Enabled = True
        'Me.txtNum.Focus()
        Me.txtCustName.Enabled = True
        Me.txtCustName.Focus()

    End Sub


    Protected Sub DoDelete()

        If Trim(Me.txtNum.Text) = "" Then
            Me.lblMessage.Text = "Missing number " & Me.lblNum.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        Dim intC As Integer = 0

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

        strOPT = "NEW"
        FirstMsg = ""

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try

            'Delete record
            'Me.textMessage.Text = "Deleting record... "
            strSQL = ""
            strSQL = "DELETE FROM " & strTable
            strSQL = strSQL & " WHERE TBIL_AGCY_AGENT_CD = '" & RTrim(strREC_ID) & "'"
            strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(txtCustID.Text) & "'"

            objOLECmd2.Connection = objOLEConn
            objOLECmd2.CommandType = CommandType.Text
            objOLECmd2.CommandText = strSQL
            intC = objOLECmd2.ExecuteNonQuery()
            objOLECmd2.Dispose()

        Catch ex As Exception
        End Try

        objOLECmd2 = Nothing

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

        'Me.lblMessage.Text = ""


        Call DoNew()
        Call Proc_DataBind()
        'Me.txtNum.Enabled = True
        'Me.txtNum.Focus()
        Me.txtCustName.Enabled = True
        Me.txtCustName.Focus()

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
                Me.txtNum.Text = Me.GridView1.Rows(P).Cells(10).Text


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

        Me.lblMessage.Text = "Record deleted successfully." & " No of item(s) deleted: " & CStr(C)
        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        'Me.textMessage.Text = ""

        Call DoNew("INIT")
        Call Proc_DataBind()

        Me.lblMessage.Text = "Deleted Item(s): " & myKeyX

        'Me.txtTreatyNum.Enabled = True
        'Me.txtTreatyNum.Focus()

    End Sub

    Protected Sub DoDelete_Record()

        If Trim(Me.txtCustID.Text) = "" Then
            Me.txtNum.Text = "Missing " & Me.lblCustID.Text
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

        Dim intC As Integer = 0

        strREC_ID = Trim(Me.txtNum.Text)
        strTable = strTableName

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

        'Delete record
        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try

            strSQL = ""
            strSQL = "DELETE FROM " & strTable
            strSQL = strSQL & " WHERE TBIL_AGCY_AGENT_CD = '" & RTrim(strREC_ID) & "'"
            strSQL = strSQL & " AND MKT.TBIL_AGCY_AGENT_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
            strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(txtCustID.Text) & "'"

            With objOLECmd2
                .Connection = objOLEConn
                .CommandType = CommandType.Text
                .CommandText = strSQL
            End With
            intC = objOLECmd2.ExecuteNonQuery()
            objOLECmd2.Dispose()

        Catch ex As Exception
        End Try

        objOLECmd2 = Nothing

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
        strSQL = strSQL & "SELECT TOP 1 MKT.*"
        strSQL = strSQL & " FROM " & strTable & " AS MKT"
        strSQL = strSQL & " WHERE MKT.TBIL_AGCY_AGENT_CD = '" & RTrim(strREC_ID) & "'"
        If Val(RTrim(txtRecNo.Text)) <> 0 Then
            strSQL = strSQL & " AND MKT.TBIL_AGCY_AGENT_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        End If
        strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(txtCustID.Text) & "'"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader


        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then

            Me.txtCustID.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_ID") & vbNullString, String))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_REC_ID") & vbNullString, String))

            Me.txtAgcy_Region_Num.Text = RTrim(CType(objOLEDR("TBIL_AGCY_REG_CODE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboAgcy_Region_Name, RTrim(Me.txtAgcy_Region_Num.Text))
            Me.txtAgcy_Region_Name.Text = RTrim(CType(objOLEDR("TBIL_AGCY_REG_NAME") & vbNullString, String))

            Me.txtAgcy_Agcy_Num.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGCY_CODE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboAgcy_Agency_List, RTrim(Me.txtAgcy_Agcy_Num.Text))
            Me.txtAgcy_Agcy_Name.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGCY_NAME") & vbNullString, String))

            Me.txtAgcy_Unit_Num.Text = RTrim(CType(objOLEDR("TBIL_AGCY_UNIT_CODE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboAgcy_Unit_List, RTrim(Me.txtAgcy_Unit_Num.Text))
            Me.txtAgcy_Unit_Name.Text = RTrim(CType(objOLEDR("TBIL_AGCY_UNIT_NAME") & vbNullString, String))

            Me.txtNum.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_CD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboTransList, RTrim(Me.txtNum.Text))
            Me.txtCustName.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))

            Me.txtAgcy_Type.Text = RTrim(CType(objOLEDR("TBIL_AGCY_TYPE") & vbNullString, String))
            Call Proc_DDL_Get(Me.cboAgcy_Type, RTrim(Me.txtAgcy_Type.Text))
            Me.txtAgcy_Type_Name.Text = RTrim(CType(objOLEDR("TBIL_AGCY_TYPE_DESC") & vbNullString, String))


            Me.txtCustAddr01.Text = RTrim(CType(objOLEDR("TBIL_AGENT_ADRES1") & vbNullString, String))
            Me.txtCustAddr02.Text = RTrim(CType(objOLEDR("TBIL_AGENT_ADRES2") & vbNullString, String))
            Me.txtCustPhone01.Text = RTrim(CType(objOLEDR("TBIL_AGENT_PHONE1") & vbNullString, String))
            Me.txtCustPhone02.Text = RTrim(CType(objOLEDR("TBIL_AGENT_PHONE2") & vbNullString, String))
            Me.txtCustEmail01.Text = RTrim(CType(objOLEDR("TBIL_AGENT_EMAIL1") & vbNullString, String))
            Me.txtCustEmail02.Text = RTrim(CType(objOLEDR("TBIL_AGENT_EMAIL2") & vbNullString, String))

            Me.lblNum.Enabled = False
            Call DisableBox(Me.txtNum)
            Me.chkNum.Enabled = False
            Me.cmdGetRecord.Enabled = False
            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True

        Else
            'Me.txtNum.Text = ""
            Me.cmdDelete_ASP.Enabled = False
            strErrMsg = "Status: New Entry..."

            Me.txtCustName.Enabled = True
            Me.txtCustName.Focus()
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

        Me.txtCustID.Text = row.Cells(3).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))

        Me.txtAgcy_Region_Num.Text = row.Cells(4).Text
        Me.txtAgcy_Region_Name.Text = row.Cells(5).Text

        Me.txtAgcy_Agcy_Num.Text = row.Cells(6).Text
        Me.txtAgcy_Agcy_Name.Text = row.Cells(7).Text

        Me.txtAgcy_Unit_Num.Text = row.Cells(8).Text
        Me.txtAgcy_Unit_Name.Text = row.Cells(9).Text

        Me.txtNum.Text = row.Cells(10).Text
        Me.txtCustName.Text = row.Cells(11).Text

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

    Protected Sub chkNum_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkNum.CheckedChanged
        If Me.chkNum.Checked = True Then
            Me.lblNum.Enabled = False
            Me.txtNum.Enabled = False
            Me.cmdGetRecord.Enabled = False
        Else
            Me.lblNum.Enabled = True
            Me.txtNum.Enabled = True
            Me.cmdGetRecord.Enabled = True
        End If
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If Trim(Me.txtSearch.Value) = "" Or Trim(Me.txtSearch.Value) = "." Or Trim(Me.txtSearch.Value) = "*" Then
        Else
            Call Proc_DataBind()
        End If

    End Sub

    Protected Sub cmdGetRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetRecord.Click
        If Trim(Me.txtNum.Text) <> "" Then
            Call Proc_OpenRecord(Trim(Me.txtNum.Text))
        End If
    End Sub

End Class
