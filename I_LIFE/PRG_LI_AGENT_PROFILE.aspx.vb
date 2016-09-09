Imports System.Data
Imports System.Data.OleDb
Partial Class I_LIFE_PRG_LI_AGENT_PROFILE
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
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
    Dim strStatus As String
    Dim myarrData() As String
    Dim MySearchValue As String
    Dim li As ListItem




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

        Me.txtCustId.Text = RTrim(strP_TYPE)


        If Not (Page.IsPostBack) Then
            'Call gnPopulate_DropDownList("IL_INS_AGENCY_TYPE_LIST", Me.cboAgcy_Type, strSQL, "", "(Select item)", "*")
            'Call gnPopulate_DropDownList("IL_MKT_REGION_LIST", Me.cboAgcy_Region_Name, strSQL, "", "(Select item)", "*")
            'Call gnProc_Populate_Box("IL_MKT_AGENCY_LIST_SP", "001", Me.cboAgcy_Agency_List, RTrim("001"))
            'Call gnProc_Populate_Box("IL_MKT_UNIT_LIST_SP", "001", Me.cboAgcy_Unit_List, RTrim("001"))
            'Call gnProc_Populate_Box("IL_MKT_AGENT_LIST_SP", "001", Me.cboTransList, RTrim("001"))
            'Call gnProc_Populate_Box("IL_CODE_LIST", "006", Me.cboLocation)
            txtCustId.Text = "001"
            txtSetupDate.Text = Format(Now, "dd/MM/yyyy")
            Call gnProc_Populate_Box("IL_CODE_LIST", "015", Me.cboGender)
            Call gnProc_Populate_Box("IL_CODE_LIST", "020", Me.cboMaritalStatus)
            Call gnPopulate_DropDownList("IL_MKT_REGION_LIST", Me.cboRegionName, strSQL, "", "(Select item)", "*")

            'Call LoadLevelManger("1", "", Me.cboLvlMgrName1)
            'Call LoadLevelManger("2", "", Me.cboLvlMgrName2)
            'Call LoadLevelManger("3", "", Me.cboLvlMgrName3)
            'Call LoadLevelManger("4", "", Me.cboLvlMgrName4)
            'Call LoadLevelManger("5", "", Me.cboLvlMgrName5)

            Call DoNew()
        End If

        If Me.txtAction.Text = "New" Then
            Call DoNew()
            'Call Proc_OpenRecord(Me.txtAgentABSCode.Text)
            Me.lblMsg.Text = "New Entry"
            Me.txtAction.Text = ""
        End If
        If Me.txtAction.Text = "Delete_Item" Then
            Call DoDelItem()
            Me.txtAction.Text = ""
        End If
    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call DoSave()
        Me.txtAction.Text = ""
    End Sub
    Protected Sub cboRegionName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRegionName.SelectedIndexChanged
        Try

            If cboRegionName.SelectedIndex = -1 Or cboRegionName.SelectedIndex = 0 Or _
                cboRegionName.SelectedItem.Value = "" Or cboRegionName.SelectedItem.Value = "*" Then
                txtRegionCode.Text = ""
                'txtAgcy_Region_Name.Text = ""
            Else
                txtRegionCode.Text = cboRegionName.SelectedItem.Value
                'txtAgcy_Region_Name.Text = cboRegionName.SelectedItem.Text
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub DoNew(Optional ByVal pvOPT As String = "NEW")

        Call Proc_DDL_Get(Me.cboTitle, RTrim("*"))
        Call Proc_DDL_Get(Me.cboGender, RTrim("*"))
        Call Proc_DDL_Get(Me.cboMaritalStatus, RTrim("*"))

        Call Proc_DDL_Get(Me.cboMobility, RTrim("*"))
        Call Proc_DDL_Get(Me.cboDesignation, RTrim("*"))
        Call Proc_DDL_Get(Me.cboRegionName, RTrim("*"))


      ClearManagerLevels()
        cboSupervisor_Search.Items.Clear()
        cboSupervisor_Search.Items.Add("* Select Supervisor *")

        Call Proc_DDL_Get(Me.cboLvlMgrName1, RTrim("*"))
        Call Proc_DDL_Get(Me.cboLvlMgrName2, RTrim("*"))
        Call Proc_DDL_Get(Me.cboLvlMgrName3, RTrim("*"))
        Call Proc_DDL_Get(Me.cboLvlMgrName4, RTrim("*"))
        Call Proc_DDL_Get(Me.cboLvlMgrName5, RTrim("*"))
        Call Proc_DDL_Get(Me.cboSupervisor_Search, RTrim("*"))

        Call Proc_DDL_Get(Me.cboQualification, RTrim("*"))
        Call Proc_DDL_Get(Me.cboProfile, RTrim("*"))
        Call Proc_DDL_Get(Me.cboWorkStatus, RTrim("*"))
        Call Proc_DDL_Get(Me.cboNaicomLicense, RTrim("*"))
        Call Proc_DDL_Get(Me.cboCIINLicense, RTrim("*"))
        Call Proc_DDL_Get(Me.cboArianLicense, RTrim("*"))
        Call Proc_DDL_Get(Me.cboBnkAcctStatus, RTrim("*"))
        Call Proc_DDL_Get(Me.cboCommMode, RTrim("*"))


        'Me.lblShortName.Visible = False
        'Me.txtShortName.Visible = False

        ' Me.cmdGetRecord.Enabled = False
        With Me
            .chkNum.Checked = True
            .chkNum.Enabled = True
            .lblAgentABSCode.Enabled = True
            '.lblAgentABSCode.Text = "Trans Code:"
            .txtAgentABSCode.ReadOnly = False
            .txtAgentABSCode.Enabled = False
            .txtOldAgentCode.Text = ""
            .txtAgentABSCode.Text = ""
            .txtRecNo.Text = "0"
            .txtRecNo.Enabled = False

            .txtActivatnDate.Text = ""
            .txtAgtCodeEPlat.Text = ""

            .txtAgentName.Text = ""
            .txtDOB.Text = ""

            .txtAgtHomeAdd.Text = ""
            .txtAgtOffAdd.Text = ""

            .txtAgtMobNo1.Text = ""
            .txtAgtMobNo2.Text = ""

            .txtAgtLandLnHom.Text = ""
            '.lblTransName.Text = "Trans Name:"
            .txtAgtLandLnOff.Text = ""
            .txtAgtEmailId.Text = ""
            .txtRegionCode.Text = ""
            .txtSupervisor_Search.Text = ""
            .txtSupervisor_Code.Text = ""
            .txtSupervisor_Name.Text = ""

            .txtNaicomLicenseNo.Text = ""
            .txtNaicomLicenseIssueDt.Text = ""
            .txtNaicomLicenseExpDt.Text = ""
            .txtNaicomLicensePrinName.Text = ""
            .txtCIINLicenseNo.Text = ""
            .txtArianLicenseNo.Text = ""


            .txtBnkAcctNo.Text = ""
            .txtSortCode.Text = ""
            .txtBnkAcctHoldName.Text = ""
            .txtBnkName.Text = ""
            .txtBVN.Text = ""

            .cmdDelete_ASP.Enabled = False
            .lblMsg.Text = "Status: New Entry..."
        End With

    End Sub
    Private Sub Proc_DataBind()
        '    strTable = strTableName
        '    strSQL = ""
        '    strSQL = strSQL & "SELECT AG.TBIL_AGENT_REC_ID, AG.TBIL_AGENT_ID, AG.TBIL_AGENT_MGR_CD"
        '    strSQL = strSQL & ",RTRIM(ISNULL(AG.TBIL_AGENT_MGR_NAME,'')) AS TBIL_AGENT_MGR_NAME"
        '    strSQL = strSQL & ",(SELECT TOP 1 TBIL_AGENT_MGR_NAME WHERE AG.TBIL_AGENT_MGR_CD=AG.TBIL_AGENT_SUPERVISOR_CODE) AS SUPERVISOR"
        '    strSQL = strSQL & " FROM " & strTable & " "
        '    strSQL = strSQL & " WHERE AG.TBIL_AGENT_ID = '" & RTrim(Me.txtCustID.Text) & "'"
        '    strSQL = strSQL & " AND (AG.TBIL_AGENT_MGR_NAME LIKE '%" & RTrim(Me.txtSearch.Value) & "%')"
        '    strSQL = strSQL & " ORDER BY AG.TBIL_AGENT_ID, RTRIM(ISNULL(AG.TBIL_AGENT_MGR_NAME,''))"

        '    Dim mystrCONN As String = CType(Session("connstr"), String)
        '    Dim objOLEConn As New OleDbConnection(mystrCONN)

        '    'open connection to database
        '    objOLEConn.Open()


        '    Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

        '    Dim objDS As DataSet = New DataSet()
        '    objDA.Fill(objDS, strTable)

        '    With GridView1
        '        .DataSource = objDS
        '        .DataBind()
        '    End With
        '    objDS = Nothing
        '    objDA = Nothing
        '    If objOLEConn.State = ConnectionState.Open Then
        '        objOLEConn.Close()
        '    End If
        '    objOLEConn = Nothing


        '    Dim P As Integer = 0
        '    Dim C As Integer = 0

        '    C = 0
        '    For P = 0 To Me.GridView1.Rows.Count - 1
        '        C = C + 1
        '    Next
        '    If C >= 1 Then
        '        Me.cmdDelete_ASP.Enabled = True
        '    End If

    End Sub

    Private Sub DoSave()

        Dim str() As String
        str = DoDate_Process(Trim(Me.txtActivatnDate.Text), txtActivatnDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Activation Date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtActivatnDate.Focus()
            Exit Sub
        End If

        str = DoDate_Process(Trim(Me.txtDOB.Text), txtDOB)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Date of Birth, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtDOB.Focus()
            Exit Sub
        End If





        If RTrim(Me.cboTitle.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblTitle.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            cboTitle.Focus()
            Exit Sub
        End If
        If RTrim(Me.txtAgentName.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblAgentName.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            txtAgentName.Focus()
            Exit Sub
        End If
        If RTrim(Me.cboGender.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblGender.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            cboGender.Focus()
            Exit Sub
        End If
        If RTrim(Me.cboMaritalStatus.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblMaritalStatus.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            cboMaritalStatus.Focus()
            Exit Sub
        End If
        'If RTrim(Me.txtAgtHomeAdd.Text) = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblAgtHomeAdd.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    txtAgtHomeAdd.Focus()
        '    Exit Sub
        'End If
        If LTrim(RTrim(Me.txtAgtMobNo1.Text)) = "" Then
            'Me.lblMsg.Text = "Missing " & Me.lblCustPhone01.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtAgtMobNo1.Text))) And Len(LTrim(RTrim(Me.txtAgtMobNo1.Text))) = 11 Then
            Else
                Me.lblMsg.Text = "Incorrect or Invalid " & Me.lblAgtMobNo1.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtAgtMobNo1.Focus()
                Exit Sub
            End If
        End If


        If LTrim(RTrim(Me.txtAgtMobNo2.Text)) = "" Then
            'Me.lblMsg.Text = "Missing " & Me.lblCustPhone01.Text
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtAgtMobNo2.Text))) And Len(LTrim(RTrim(Me.txtAgtMobNo2.Text))) = 11 Then
            Else
                Me.lblMsg.Text = "Incorrect or Invalid " & Me.lblAgtMobNo2.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtAgtMobNo1.Focus()
                Exit Sub
            End If
        End If


        If LTrim(RTrim(Me.txtAgtLandLnHom.Text)) = "" Then
            'Me.textMessage.Text = "Missing " & Me.lblCustPhone02.Text
            'FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            'Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtAgtLandLnHom.Text))) And Len(LTrim(RTrim(Me.txtAgtLandLnHom.Text))) = 7 Then
            Else
                Me.lblMsg.Text = "Incorrect/Invalid " & Me.lblAgtLandLnHom.Text
                txtAgtLandLnHom.Focus()
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        End If

        If LTrim(RTrim(Me.txtAgtLandLnOff.Text)) = "" Then
            'Me.textMessage.Text = "Missing " & Me.lblCustPhone02.Text
            'FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            'Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtAgtLandLnOff.Text))) And Len(LTrim(RTrim(Me.txtAgtLandLnOff.Text))) = 7 Then
            Else
                Me.lblMsg.Text = "Incorrect/Invalid " & Me.lblAgtLandLnOff.Text
                txtAgtLandLnOff.Focus()
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        End If

        If RTrim(Me.cboMobility.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblMobility.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            cboMobility.Focus()
            Exit Sub
        End If
        If LTrim(RTrim(Me.txtAgtEmailId.Text)) = "" Then
        Else
            blnStatus = gnParseEmail_Address(RTrim(Me.txtAgtEmailId.Text))
            If blnStatus = False Then
                Me.lblMsg.Text = "Incorrect/Invalid " & Me.lblAgtEmailId.Text
                txtAgtEmailId.Focus()
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub

            End If
        End If


        If RTrim(Me.cboDesignation.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblDesignaton.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            cboDesignation.Focus()
            Exit Sub
        End If

        If Trim(cboDesignation.SelectedItem.Text) <> "Zonal Head" Then
            If txtSupervisor_Code.Text = "" Then
                Me.lblMsg.Text = "Missing/Invalid " & Me.lblSupervisorCode.Text
                cboSupervisor_Search.Focus()
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            End If
        End If

        If RTrim(Me.txtRegionCode.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblRegionCode.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            cboRegionName.Focus()
            Exit Sub
        End If

        If RTrim(Me.cboQualification.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblMobility.Text
            cboQualification.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        'If RTrim(Me.txtSupervisor_Code.Text) = "" Then
        '    Me.lblMsg.Text = "Missing " & Me.lblSupervisorCode.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If
        If RTrim(Me.cboProfile.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblProfile.Text
            cboProfile.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If RTrim(Me.cboWorkStatus.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblWorkStatus.Text
            cboWorkStatus.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.cboNaicomLicense.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblNaicomLicense.Text
            cboNaicomLicense.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.cboNaicomLicense.SelectedItem.Value) = "Yes" And txtNaicomLicenseNo.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblNaicomLicenseNo.Text
            cboNaicomLicense.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.cboNaicomLicense.SelectedItem.Value) = "Yes" And txtNaicomLicenseIssueDt.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblNaicomLicenseIssueDt.Text
            txtNaicomLicenseIssueDt.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        Else
            txtNaicomLicenseIssueDt.Text = #1/1/2014#
        End If

        If RTrim(Me.cboNaicomLicense.SelectedItem.Value) = "Yes" And txtNaicomLicenseExpDt.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblNaicomLicenseExpDt.Text
            txtNaicomLicenseExpDt.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        Else
            txtNaicomLicenseExpDt.Text = #1/1/2014#
        End If

        If RTrim(Me.cboCIINLicense.SelectedItem.Value) = "Yes" And txtCIINLicenseNo.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblCIINLicenseNo.Text
            txtCIINLicenseNo.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.cboArianLicense.SelectedItem.Value) = "Yes" And txtArianLicenseNo.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblCIINLicenseNo.Text
            txtArianLicenseNo.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        If RTrim(Me.txtBnkAcctNo.Text) = "" Then
            Me.lblMsg.Text = "Invalid or Missing " & Me.lblBnkAcctNo.Text
            txtBnkAcctNo.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        Else
            If IsNumeric(LTrim(RTrim(Me.txtBnkAcctNo.Text))) And Len(LTrim(RTrim(Me.txtBnkAcctNo.Text))) = 10 Then
            Else
                Me.lblMsg.Text = "Incorrect or Invalid " & Me.lblBnkAcctNo.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtBnkAcctNo.Focus()
                Exit Sub
            End If
        End If
        If RTrim(Me.txtBnkAcctHoldName.Text) = "" Then
            Me.lblMsg.Text = "Invalid or Missing " & Me.lblBnkAcctHoldName.Text
            txtBnkAcctHoldName.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.cboBnkAcctStatus.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblBnkAcctStatus.Text
            cboBnkAcctStatus.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If
        If RTrim(Me.cboCommMode.SelectedItem.Value) = "*" Then
            Me.lblMsg.Text = "Missing " & Me.lblCommMode.Text
            cboCommMode.Focus()
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        If RTrim(Me.txtBVN.Text) = "" Then
        Else
            If IsNumeric(LTrim(RTrim(Me.txtBVN.Text))) And Len(LTrim(RTrim(Me.txtBVN.Text))) = 11 Then
            Else
                Me.lblMsg.Text = "Incorrect or Invalid " & Me.lblBVN.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                txtBVN.Focus()
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
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try

        Dim lvlMrgCode1, lvlMrgCode2, lvlMrgCode3, lvlMrgCode4, lvlMrgCode5 As String
        Dim lvlMrgName1, lvlMrgName2, lvlMrgName3, lvlMrgName4, lvlMrgName5 As String

        If cboLvlMgrName1.SelectedItem.Value = "Select" Then
            lvlMrgCode1 = Nothing
            lvlMrgName1 = Nothing
        Else
            lvlMrgCode1 = cboLvlMgrName1.SelectedItem.Value
            lvlMrgName1 = cboLvlMgrName1.SelectedItem.Text
        End If

        If cboLvlMgrName2.SelectedItem.Value = "Select" Then
            lvlMrgCode2 = Nothing
            lvlMrgName2 = Nothing
        Else
            lvlMrgCode2 = cboLvlMgrName2.SelectedItem.Value
            lvlMrgName2 = cboLvlMgrName2.SelectedItem.Text
        End If
        If cboLvlMgrName3.SelectedItem.Value = "Select" Then
            lvlMrgCode3 = Nothing
            lvlMrgName3 = Nothing
        Else
            lvlMrgCode3 = cboLvlMgrName3.SelectedItem.Value
            lvlMrgName3 = cboLvlMgrName3.SelectedItem.Text
        End If
        If cboLvlMgrName4.SelectedItem.Value = "Select" Then
            lvlMrgCode4 = Nothing
            lvlMrgName4 = Nothing
        Else
            lvlMrgCode4 = cboLvlMgrName4.SelectedItem.Value
            lvlMrgName4 = cboLvlMgrName4.SelectedItem.Text
        End If
        If cboLvlMgrName5.SelectedItem.Value = "Select" Then
            lvlMrgCode5 = Nothing
            lvlMrgName5 = Nothing
        Else
            lvlMrgCode5 = cboLvlMgrName5.SelectedItem.Value
            lvlMrgName5 = cboLvlMgrName5.SelectedItem.Text
        End If






        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_AGCY_AGENT_NAME = '" & LTrim(Me.txtAgentName.Text) & "'"
        'strSQL = strSQL & " AND TBIL_AGENT_ID = '" & RTrim(txtCustID.Text) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtAgentABSCode.Text) <> Trim(chk_objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString) Then
                Me.lblMsg.Text = "Warning!. The Account Name you enter already exist..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString)
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
            'Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
            Exit Sub
        End Try

        If RTrim(txtAgentABSCode.Text) = "" Then
            Me.txtAgentABSCode.Text = MOD_GEN.gnGet_Serial_Und("GET_SN_IL_AGT", Trim("AGT"), Trim("AGT"), "XXXX", "XXXX", cboDesignation.SelectedItem.Value & "00")
            txtOldAgentCode.Text = MOD_GEN.gnGet_Serial_Und("GET_SN_IL_MKT", Trim("MKT"), Trim("MKT"), "XXXX", "XXXX", "MR")
            If Trim(txtAgentABSCode.Text) = "" Or Trim(Me.txtAgentABSCode.Text) = "0" Or Trim(Me.txtAgentABSCode.Text) = "*" Then
                Me.txtAgentABSCode.Text = ""
                Me.lblMsg.Text = "Sorry!. Unable to get the next record id. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Me.lblMsg.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtAgentABSCode.Text) = "PARAM_ERR" Then
                Me.txtAgentABSCode.Text = ""
                Me.lblMsg.Text = "Sorry!. Unable to get the next record id - INVALID PARAMETER(S)"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Me.lblMsg.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtAgentABSCode.Text) = "DB_ERR" Then
                Me.txtAgentABSCode.Text = ""
                Me.lblMsg.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Me.lblMsg.Text = "Status:"
                Exit Sub
            ElseIf Trim(Me.txtAgentABSCode.Text) = "ERR_ERR" Then
                Me.txtAgentABSCode.Text = ""
                Me.lblMsg.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Me.lblMsg.Text = "Status:"
                Exit Sub
            End If

        End If


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

        If Trim(cboDesignation.SelectedItem.Text) = "Zonal Head" Then
            txtSupervisor_Code.Text = txtAgentABSCode.Text
            txtSupervisor_Name.Text = txtAgentName.Text
        End If
        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_AGCY_NEW_AGENT_CD = '" & RTrim(txtAgentABSCode.Text) & "'"
        'strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(txtCustID.Text) & "'"
        'If Val(Trim(Me.txtRecNo.Text)) <> 0 Then
        '    strSQL = strSQL & " AND TBIL_AGCY_AGENT_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"

        'End If

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)


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

                drNewRow("TBIL_AGCY_AGENT_ID") = "001"  'RTrim(Me.txtCustID.Text)
                drNewRow("TBIL_AGCY_CD_MDLE") = RTrim("I")

                drNewRow("TBIL_AGCY_AGENT_CD") = Trim(Me.txtOldAgentCode.Text)
                drNewRow("TBIL_AGCY_NEW_AGENT_CD") = Trim(Me.txtAgentABSCode.Text)

                drNewRow("TBIL_AGCY_AGENT_NAME") = Trim(Me.txtAgentName.Text)
                drNewRow("TBIL_AGENT_ADRES1") = Trim(Me.txtAgtHomeAdd.Text)
                drNewRow("TBIL_AGENT_PHONE1") = Trim(Me.txtAgtMobNo1.Text)

                drNewRow("TBIL_AGENT_PHONE2") = Trim(Me.txtAgtMobNo2.Text)
                drNewRow("TBIL_AGENT_OFF_ADRES") = Trim(Me.txtAgtOffAdd.Text)
                drNewRow("TBIL_AGENT_LANDLN_HOME") = Trim(Me.txtAgtLandLnHom.Text)
                drNewRow("TBIL_AGENT_LANDLN_OFF") = Trim(Me.txtAgtLandLnOff.Text)


                drNewRow("TBIL_AGENT_EMAIL1") = Trim(Me.txtAgtEmailId.Text)
                drNewRow("TBIL_AGCY_AGENT_VEHICLE_TYPE") = Trim(Me.cboMobility.Text)
                drNewRow("TBIL_AGCY_SETUP_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(RTrim(Me.txtSetupDate.Text)))
                drNewRow("TBIL_AGCY_ACTV_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(RTrim(Me.txtActivatnDate.Text)))
                drNewRow("TBIL_AGCY_EPLAT_AGT_CODE") = RTrim(Me.txtAgtCodeEPlat.Text)
                drNewRow("TBIL_AGCY_AGENT_TITLE") = RTrim(Me.cboTitle.SelectedItem.Value)
                drNewRow("TBIL_AGCY_DESIGNATN") = RTrim(Me.cboDesignation.SelectedItem.Value)

                drNewRow("TBIL_AGCY_AGENT_DOB") = Convert.ToDateTime(DoConvertToDbDateFormat(RTrim(Me.txtDOB.Text)))
                drNewRow("TBIL_AGCY_AGENT_GENDER") = RTrim(Me.cboGender.SelectedItem.Value)

                drNewRow("TBIL_AGCY_REG_CODE") = RTrim(Me.cboRegionName.SelectedItem.Value)
                drNewRow("TBIL_AGCY_REG_NAME") = RTrim(Me.cboRegionName.SelectedItem.Text)

                drNewRow("TBIL_AGCY_AGENT_SUPERVISOR_CD") = RTrim(Me.txtSupervisor_Code.Text)
                drNewRow("TBIL_AGCY_AGENT_LV1_MGR_CD") = lvlMrgCode1
                drNewRow("TBIL_AGCY_AGENT_LV1_MGR_NAME") = lvlMrgName1

                drNewRow("TBIL_AGCY_AGENT_LV2_MGR_CD") = lvlMrgCode2
                drNewRow("TBIL_AGCY_AGENT_LV2_MGR_NAME") = lvlMrgName2

                drNewRow("TBIL_AGCY_AGENT_LV3_MGR_CD") = lvlMrgCode3
                drNewRow("TBIL_AGCY_AGENT_LV3_MGR_NAME") = lvlMrgName3

                drNewRow("TBIL_AGCY_AGENT_LV4_MGR_CD") = lvlMrgCode4
                drNewRow("TBIL_AGCY_AGENT_LV4_MGR_NAME") = lvlMrgName4

                drNewRow("TBIL_AGCY_AGENT_LV5_MGR_CD") = lvlMrgCode5
                drNewRow("TBIL_AGCY_AGENT_LV5_MGR_NAME") = lvlMrgName5

                drNewRow("TBIL_AGCY_AGENT_MARITAL_STATUS") = RTrim(Me.cboMaritalStatus.SelectedItem.Value)
                drNewRow("TBIL_AGCY_AGENT_QUALIFICATION") = RTrim(Me.cboQualification.SelectedItem.Text)

                drNewRow("TBIL_AGCY_AGENT_PROFILE") = LTrim(Me.cboProfile.SelectedItem.Value)
                drNewRow("TBIL_AGCY_AGENT_WORK_STATUS") = LTrim(Me.cboWorkStatus.SelectedItem.Value)
                'drNewRow("TBIL_AGCY_AGENT_VEHICLE_TYPE") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                drNewRow("TBIL_AGCY_AGENT_NAICOM_LICENSE") = Me.cboNaicomLicense.SelectedItem.Value
                drNewRow("TBIL_AGCY_AGENT_NAICOM_LICENSE_NO") = Trim(Me.txtNaicomLicenseNo.Text)

                drNewRow("TBIL_AGCY_AGENT_CIIN_LICENSE") = Me.cboCIINLicense.SelectedItem.Value
                drNewRow("TBIL_AGCY_AGENT_CIIN_LICENSE_NO") = Trim(Me.txtCIINLicenseNo.Text)
                drNewRow("TBIL_AGCY_AGENT_ARIAN_LICENSE") = Me.cboArianLicense.SelectedItem.Value
                drNewRow("TBIL_AGCY_AGENT_ARIAN_LICENSE_NO") = Trim(Me.txtArianLicenseNo.Text)

                drNewRow("TBIL_AGCY_AGENT_ISSUE_DATE") = Convert.ToDateTime(DoConvertToDbDateFormat(txtNaicomLicenseIssueDt.Text))
                drNewRow("TBIL_AGCY_AGENT_PRINCIPAL_NAME") = Trim(Me.txtNaicomLicensePrinName.Text)
                drNewRow("TBIL_AGCY_AGENT_NAICOM_LICENSE_EXP_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(txtNaicomLicenseExpDt.Text))
                drNewRow("TBIL_AGCY_AGENT_BANK_ACCT") = Trim(Me.txtBnkAcctNo.Text)
                drNewRow("TBIL_AGCY_AGENT_BANK_ACCT_HOLD_NAME") = RTrim(Me.txtBnkAcctHoldName.Text)
                drNewRow("TBIL_AGCY_AGENT_ACCT_STATUS") = RTrim(Me.cboBnkAcctStatus.SelectedItem.Value)
                drNewRow("TBIL_AGCY_AGENT_BANK_NAME") = Trim(Me.txtBnkName.Text)
                drNewRow("TBIL_AGCY_AGENT_BVN") = RTrim(Me.txtBVN.Text)
                drNewRow("TBIL_AGCY_AGENT_COMM_MODE") = RTrim(Me.cboCommMode.SelectedItem.Value)
                drNewRow("TBIL_AGCY_AGENT_BANK_ACCT_SORT_CODE") = RTrim(txtSortCode.Text)
                drNewRow("TBIL_AGENT_FLAG") = "A"
                drNewRow("TBIL_AGENT_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_AGENT_KEYDTE") = Now

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
                    .Rows(0)("TBIL_AGCY_AGENT_ID") = "001"  'RTrim(Me.txtCustID.Text)
                    .Rows(0)("TBIL_AGCY_CD_MDLE") = RTrim("I")


                    .Rows(0)("TBIL_AGCY_AGENT_CD") = Trim(Me.txtOldAgentCode.Text)
                    .Rows(0)("TBIL_AGCY_NEW_AGENT_CD") = Trim(Me.txtAgentABSCode.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_NAME") = Trim(Me.txtAgentName.Text)
                    .Rows(0)("TBIL_AGENT_ADRES1") = Trim(Me.txtAgtHomeAdd.Text)
                    .Rows(0)("TBIL_AGENT_PHONE1") = Trim(Me.txtAgtMobNo1.Text)

                    .Rows(0)("TBIL_AGENT_PHONE2") = Trim(Me.txtAgtMobNo2.Text)
                    .Rows(0)("TBIL_AGENT_OFF_ADRES") = Trim(Me.txtAgtOffAdd.Text)
                    .Rows(0)("TBIL_AGENT_LANDLN_HOME") = Trim(Me.txtAgtLandLnHom.Text)
                    .Rows(0)("TBIL_AGENT_LANDLN_OFF") = Trim(Me.txtAgtLandLnOff.Text)

                    .Rows(0)("TBIL_AGENT_EMAIL1") = Trim(Me.txtAgtEmailId.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_VEHICLE_TYPE") = Trim(Me.cboMobility.Text)
                    .Rows(0)("TBIL_AGCY_SETUP_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(RTrim(Me.txtSetupDate.Text)))
                    .Rows(0)("TBIL_AGCY_ACTV_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(RTrim(Me.txtActivatnDate.Text)))
                    .Rows(0)("TBIL_AGCY_EPLAT_AGT_CODE") = RTrim(Me.txtAgtCodeEPlat.Text)

                    .Rows(0)("TBIL_AGCY_AGENT_TITLE") = RTrim(Me.cboTitle.SelectedItem.Value)
                    .Rows(0)("TBIL_AGCY_DESIGNATN") = RTrim(Me.cboDesignation.SelectedItem.Value)

                    .Rows(0)("TBIL_AGCY_AGENT_DOB") = Convert.ToDateTime(DoConvertToDbDateFormat(RTrim(Me.txtDOB.Text)))
                    .Rows(0)("TBIL_AGCY_AGENT_GENDER") = RTrim(Me.cboGender.SelectedItem.Value)

                    .Rows(0)("TBIL_AGCY_REG_CODE") = RTrim(Me.cboRegionName.SelectedItem.Value)
                    .Rows(0)("TBIL_AGCY_REG_NAME") = RTrim(Me.cboRegionName.SelectedItem.Text)

                    .Rows(0)("TBIL_AGCY_AGENT_SUPERVISOR_CD") = RTrim(Me.txtSupervisor_Code.Text)

                    .Rows(0)("TBIL_AGCY_AGENT_LV1_MGR_CD") = lvlMrgCode1
                    .Rows(0)("TBIL_AGCY_AGENT_LV1_MGR_NAME") = lvlMrgName1

                    .Rows(0)("TBIL_AGCY_AGENT_LV2_MGR_CD") = lvlMrgCode2
                    .Rows(0)("TBIL_AGCY_AGENT_LV2_MGR_NAME") = lvlMrgName2

                    .Rows(0)("TBIL_AGCY_AGENT_LV3_MGR_CD") = lvlMrgCode3
                    .Rows(0)("TBIL_AGCY_AGENT_LV3_MGR_NAME") = lvlMrgName3

                    .Rows(0)("TBIL_AGCY_AGENT_LV4_MGR_CD") = lvlMrgCode4
                    .Rows(0)("TBIL_AGCY_AGENT_LV4_MGR_NAME") = lvlMrgName4

                    .Rows(0)("TBIL_AGCY_AGENT_LV5_MGR_CD") = lvlMrgCode5
                    .Rows(0)("TBIL_AGCY_AGENT_LV5_MGR_NAME") = lvlMrgName5

                    .Rows(0)("TBIL_AGCY_AGENT_MARITAL_STATUS") = RTrim(Me.cboMaritalStatus.SelectedItem.Value)
                    .Rows(0)("TBIL_AGCY_AGENT_QUALIFICATION") = RTrim(Me.cboQualification.SelectedItem.Text)

                    .Rows(0)("TBIL_AGCY_AGENT_PROFILE") = LTrim(Me.cboProfile.SelectedItem.Value)
                    .Rows(0)("TBIL_AGCY_AGENT_WORK_STATUS") = LTrim(Me.cboWorkStatus.SelectedItem.Value)
                    ' .Rows(0)("TBIL_AGCY_AGENT_VEHICLE_TYPE") = Left(LTrim(Me.txtCustPhone01.Text), 11)
                    .Rows(0)("TBIL_AGCY_AGENT_NAICOM_LICENSE") = Me.cboNaicomLicense.SelectedItem.Value
                    .Rows(0)("TBIL_AGCY_AGENT_NAICOM_LICENSE_NO") = Trim(Me.txtNaicomLicenseNo.Text)

                    .Rows(0)("TBIL_AGCY_AGENT_CIIN_LICENSE") = Me.cboCIINLicense.SelectedItem.Value
                    .Rows(0)("TBIL_AGCY_AGENT_CIIN_LICENSE_NO") = Trim(Me.txtCIINLicenseNo.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_ARIAN_LICENSE") = Me.cboArianLicense.SelectedItem.Value
                    .Rows(0)("TBIL_AGCY_AGENT_ARIAN_LICENSE_NO") = Trim(Me.txtArianLicenseNo.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_ISSUE_DATE") = Convert.ToDateTime(DoConvertToDbDateFormat(txtNaicomLicenseIssueDt.Text))
                    .Rows(0)("TBIL_AGCY_AGENT_PRINCIPAL_NAME") = Trim(Me.txtNaicomLicensePrinName.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_NAICOM_LICENSE_EXP_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(txtNaicomLicenseExpDt.Text))
                    .Rows(0)("TBIL_AGCY_AGENT_BANK_ACCT") = Trim(Me.txtBnkAcctNo.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_BANK_ACCT_HOLD_NAME") = RTrim(Me.txtBnkAcctHoldName.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_ACCT_STATUS") = RTrim(Me.cboBnkAcctStatus.SelectedItem.Value)
                    .Rows(0)("TBIL_AGCY_AGENT_BANK_NAME") = Trim(Me.txtBnkName.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_BVN") = RTrim(Me.txtBVN.Text)
                    .Rows(0)("TBIL_AGCY_AGENT_COMM_MODE") = RTrim(Me.cboCommMode.SelectedItem.Value)
                    .Rows(0)("TBIL_AGCY_AGENT_BANK_ACCT_SORT_CODE") = RTrim(txtSortCode.Text)
                    .Rows(0)("TBIL_AGENT_FLAG") = "C"
                    '.Rows(0)("TBIL_AGENT_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_AGENT_KEYDTE") = Now
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
        'Me.txtAgentABSCode.Enabled = False

        'DoProc_SaveRecord = Me.lblMsg.Text
        'Return Me.lblMsg.Text

        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        'Me.textMessage.Text = ""

        Me.txtAgentABSCode.Enabled = False
        Me.chkNum.Enabled = False

        ' Me.txtSearch.Value = RTrim(Me.txtAgentName.Text)
        Call Proc_DataBind()
        'Me.txtSearch.Value = ""
        Call DoNew()
        'Me.txtAgentABSCode.Enabled = True
        'Me.txtAgentABSCode.Focus()
        Me.txtAgentName.Enabled = True
        Me.txtAgentName.Focus()
    End Sub
    Protected Sub DoDelete()

        If Trim(Me.txtAgentABSCode.Text) = "" Then
            Me.lblMsg.Text = "Missing number " & Me.lblAgentABSCode.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim intC As Integer = 0

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

        strREC_ID = Trim(Me.txtAgentABSCode.Text)

        strOPT = "NEW"
        FirstMsg = ""

        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try

            'Delete record
            'Me.textMessage.Text = "Deleting record... "
            strSQL = ""
            strSQL = "DELETE FROM " & strTable
            strSQL = strSQL & " WHERE TBIL_AGCY_NEW_AGENT_CD = '" & RTrim(strREC_ID) & "'"
            strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(txtCustId.Text) & "'"

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
            Me.lblMsg.Text = "Record deleted successfully."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        Else
            Me.lblMsg.Text = "Sorry!. Record not deleted..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        End If

        'Me.lblMsg.Text = ""


        Call DoNew()
        Call Proc_DataBind()
        'Me.txtAgentABSCode.Enabled = True
        'Me.txtAgentABSCode.Focus()
        Me.txtAgentName.Enabled = True
        Me.txtAgentName.Focus()

    End Sub


    Protected Sub DoDelItem()

        'Dim blnRet As Boolean = False
        'Dim P As Integer = 0, C As Integer
        'Dim myKey As String = "", myKeyX As String = ""


        'For P = 0 To Me.GridView1.Rows.Count - 1
        '    If CType(Me.GridView1.Rows(P).FindControl("chkSel"), CheckBox).Checked Then
        '        ' Get the currently selected row using the SelectedRow property.
        '        Dim row As GridViewRow = GridView1.Rows(P)
        '        myKeyX = myKeyX & row.Cells(2).Text
        '        myKeyX = myKeyX & " / "

        '        myKey = Me.GridView1.Rows(P).Cells(2).Text
        '        Me.txtAgentABSCode.Text = Me.GridView1.Rows(P).Cells(10).Text


        '        'Insert codes to delete selected/checked item(s)

        '        If Trim(myKey) <> "" Then
        '            Me.txtRecNo.Text = myKey
        '            Call DoDelete_Record()
        '            C = C + 1
        '        End If

        '    End If

        'Next

        'Me.cmdDelete_ASP.Enabled = False
        ''Me.cmdDelItem.Enabled = False

        'Me.lblMsg.Text = "Record deleted successfully." & " No of item(s) deleted: " & CStr(C)
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        ''Me.textMessage.Text = ""

        'Call DoNew("INIT")
        'Call Proc_DataBind()

        'Me.lblMsg.Text = "Deleted Item(s): " & myKeyX

        ''Me.txtTreatyNum.Enabled = True
        ''Me.txtTreatyNum.Focus()

    End Sub

    Protected Sub DoDelete_Record()

        If Trim(Me.txtCustId.Text) = "" Then
            Me.txtAgentABSCode.Text = "Missing " & Me.lblCustID.Text
            FirstMsg = "Javascript:alert('" & Me.txtAgentABSCode.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtAgentABSCode.Text) = "" Then
            Me.txtAgentABSCode.Text = "Missing " & Me.lblAgentABSCode.Text
            FirstMsg = "Javascript:alert('" & Me.txtAgentABSCode.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtRecNo.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblRecNo.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim intC As Integer = 0

        strREC_ID = Trim(Me.txtAgentABSCode.Text)
        strTable = strTableName

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

        'Delete record
        Dim objOLECmd2 As OleDbCommand = New OleDbCommand()

        Try

            strSQL = ""
            strSQL = "DELETE FROM " & strTable
            strSQL = strSQL & " WHERE TBIL_AGCY_NEW_AGENT_CD = '" & RTrim(strREC_ID) & "'"
            strSQL = strSQL & " AND MKT.TBIL_AGCY_AGENT_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
            strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(txtCustId.Text) & "'"

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

        lblMsg.Text = ""
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
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Proc_OpenRecord = strErrMsg
            Return Proc_OpenRecord
            Exit Function
        End Try


        strREC_ID = Trim(strRefNo)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 MKT.*"
        strSQL = strSQL & ",(SELECT TOP 1 TBIL_AGCY_AGENT_NAME WHERE TBIL_AGCY_NEW_AGENT_CD=MKT.TBIL_AGCY_AGENT_SUPERVISOR_CD) AS SUPERVISOR"
        strSQL = strSQL & " FROM " & strTable & " AS MKT"
        strSQL = strSQL & " WHERE MKT.TBIL_AGCY_NEW_AGENT_CD = '" & RTrim(strREC_ID) & "'"
        'If Val(RTrim(txtRecNo.Text)) <> 0 Then
        '    strSQL = strSQL & " AND MKT.TBIL_AGCY_AGENT_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
        'End If
        'strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(txtCustId.Text) & "'"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader


        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then

            Me.txtCustId.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_ID") & vbNullString, String))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_REC_ID") & vbNullString, String))
            Me.txtOldAgentCode.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_CD") & vbNullString, String))
            Me.txtAgentABSCode.Text = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))

            Me.txtAgentName.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))

            Me.txtAgtHomeAdd.Text = RTrim(CType(objOLEDR("TBIL_AGENT_ADRES1") & vbNullString, String))
            Me.txtAgtMobNo1.Text = RTrim(CType(objOLEDR("TBIL_AGENT_PHONE1") & vbNullString, String))
            Me.txtAgtMobNo2.Text = RTrim(CType(objOLEDR("TBIL_AGENT_PHONE2") & vbNullString, String))
            Me.txtAgtEmailId.Text = RTrim(CType(objOLEDR("TBIL_AGENT_EMAIL1") & vbNullString, String))

            Me.txtAgtMobNo2.Text = RTrim(CType(objOLEDR("TBIL_AGENT_PHONE2") & vbNullString, String))
            Me.txtAgtOffAdd.Text = RTrim(CType(objOLEDR("TBIL_AGENT_OFF_ADRES") & vbNullString, String))
            Me.txtAgtLandLnHom.Text = RTrim(CType(objOLEDR("TBIL_AGENT_LANDLN_HOME") & vbNullString, String))
            Me.txtAgtLandLnOff.Text = RTrim(CType(objOLEDR("TBIL_AGENT_LANDLN_OFF") & vbNullString, String))

            If IsDate(objOLEDR("TBIL_AGCY_SETUP_DT")) Then
                Me.txtSetupDate.Text = Format(objOLEDR("TBIL_AGCY_SETUP_DT"), "dd/MM/yyyy")
            End If

            If IsDate(objOLEDR("TBIL_AGCY_ACTV_DT")) Then
                Me.txtActivatnDate.Text = Format(objOLEDR("TBIL_AGCY_ACTV_DT"), "dd/MM/yyyy")
            End If
            Me.txtAgtCodeEPlat.Text = RTrim(CType(objOLEDR("TBIL_AGCY_EPLAT_AGT_CODE") & vbNullString, String))

            Dim title = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_TITLE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboTitle, RTrim(title))

            Dim designation = RTrim(CType(objOLEDR("TBIL_AGCY_DESIGNATN") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboDesignation, RTrim(designation))


            txtRegionCode.Text = RTrim(CType(objOLEDR("TBIL_AGCY_REG_CODE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboRegionName, RTrim(txtRegionCode.Text))

            If IsDate(objOLEDR("TBIL_AGCY_AGENT_DOB")) Then
                Me.txtDOB.Text = Format(objOLEDR("TBIL_AGCY_AGENT_DOB"), "dd/MM/yyyy")
            End If

            Dim gender = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_GENDER") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboGender, RTrim(gender))


            txtSupervisor_Code.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))

            txtSupervisor_Name.Text = RTrim(CType(objOLEDR("SUPERVISOR") & vbNullString, String))

            li = New ListItem()
            li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV1_MGR_NAME") & vbNullString, String))
            li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV1_MGR_CD") & vbNullString, String))

            If li.Value <> "" Then
                Me.cboLvlMgrName1.Items.Add(li)
                Me.cboLvlMgrName1.SelectedValue = li.Value
            End If

            li = New ListItem()
            li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV2_MGR_NAME") & vbNullString, String))
            li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV2_MGR_CD") & vbNullString, String))
            If li.Value <> "" Then
                Me.cboLvlMgrName2.Items.Add(li)
                Me.cboLvlMgrName2.SelectedValue = li.Value
            End If

            li = New ListItem()
            li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV3_MGR_NAME") & vbNullString, String))
            li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV3_MGR_CD") & vbNullString, String))
            If li.Value <> "" Then
                Me.cboLvlMgrName3.Items.Add(li)
                Me.cboLvlMgrName3.SelectedValue = li.Value
            End If

            li = New ListItem()
            li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV4_MGR_NAME") & vbNullString, String))
            li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV4_MGR_CD") & vbNullString, String))
            If li.Value <> "" Then
                Me.cboLvlMgrName4.Items.Add(li)
                Me.cboLvlMgrName4.SelectedValue = li.Value
            End If

            li = New ListItem()
            li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV5_MGR_NAME") & vbNullString, String))
            li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV5_MGR_CD") & vbNullString, String))
            If li.Value <> "" Then
                Me.cboLvlMgrName5.Items.Add(li)
                Me.cboLvlMgrName5.SelectedValue = li.Value
            End If

            'Dim lvMgrCode1 = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV1_MGR_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboLvlMgrName1, RTrim(lvMgrCode1))
            'Dim lvMgrCode2 = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV2_MGR_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboLvlMgrName2, RTrim(lvMgrCode2))
            'Dim lvMgrCode3 = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV3_MGR_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboLvlMgrName3, RTrim(lvMgrCode3))
            'Dim lvMgrCode4 = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV4_MGR_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboLvlMgrName4, RTrim(lvMgrCode4))
            'Dim lvMgrCode5 = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_LV5_MGR_CD") & vbNullString, String))
            'Call gnProc_DDL_Get(Me.cboLvlMgrName5, RTrim(lvMgrCode5))

            Dim marita_status = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_MARITAL_STATUS") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboMaritalStatus, RTrim(marita_status))

            Dim qualification = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_QUALIFICATION") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboQualification, RTrim(qualification))

            Dim profile = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_PROFILE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboProfile, RTrim(profile))

            Dim work_status = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_WORK_STATUS") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboWorkStatus, RTrim(work_status))

            Dim vehicle_type = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_VEHICLE_TYPE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboMobility, RTrim(vehicle_type))

            Dim naicom_license = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAICOM_LICENSE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboNaicomLicense, RTrim(naicom_license))
            Me.txtNaicomLicenseNo.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAICOM_LICENSE_NO") & vbNullString, String))

            Dim ciin_license = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_CIIN_LICENSE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboCIINLicense, RTrim(ciin_license))
            Me.txtCIINLicenseNo.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_CIIN_LICENSE_NO") & vbNullString, String))

            Dim arian_license = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_ARIAN_LICENSE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboArianLicense, RTrim(arian_license))
            Me.txtArianLicenseNo.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_ARIAN_LICENSE_NO") & vbNullString, String))

            If IsDate(objOLEDR("TBIL_AGCY_AGENT_ISSUE_DATE")) Then
                Me.txtNaicomLicenseIssueDt.Text = Format(objOLEDR("TBIL_AGCY_AGENT_ISSUE_DATE"), "dd/MM/yyyy")
            End If

            Me.txtNaicomLicensePrinName.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_PRINCIPAL_NAME") & vbNullString, String))

            If IsDate(objOLEDR("TBIL_AGCY_AGENT_NAICOM_LICENSE_EXP_DT")) Then
                Me.txtNaicomLicenseExpDt.Text = Format(objOLEDR("TBIL_AGCY_AGENT_NAICOM_LICENSE_EXP_DT"), "dd/MM/yyyy")
            End If
            Me.txtBnkAcctNo.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_BANK_ACCT") & vbNullString, String))
            Me.txtBnkAcctHoldName.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_BANK_ACCT_HOLD_NAME") & vbNullString, String))

            Dim bnk_acct_status = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_ACCT_STATUS") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBnkAcctStatus, RTrim(bnk_acct_status))


            Me.txtBnkName.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_BANK_NAME") & vbNullString, String))
            Me.txtBVN.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_BVN") & vbNullString, String))

            Dim comm_mode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_COMM_MODE") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboCommMode, RTrim(comm_mode))
            Me.txtSortCode.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_BANK_ACCT_SORT_CODE") & vbNullString, String))


            Me.lblAgentABSCode.Enabled = False
            Call DisableBox(Me.txtAgentABSCode)
            Me.chkNum.Enabled = False
            ' Me.cmdGetRecord.Enabled = False
            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True

        Else
            'Me.txtAgentABSCode.Text = ""
            Me.cmdDelete_ASP.Enabled = False
            strErrMsg = "Status: New Entry..."

            Me.txtAgentName.Enabled = True
            Me.txtAgentName.Focus()
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

        Me.lblMsg.Text = strErrMsg
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
    Protected Sub chkNum_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNum.CheckedChanged
        If Me.chkNum.Checked = True Then
            Me.lblAgentABSCode.Enabled = False
            Me.txtAgentABSCode.Enabled = False
            'Me.cmdGetRecord.Enabled = False
        Else
            Me.lblAgentABSCode.Enabled = True
            Me.txtAgentABSCode.Enabled = True
            ' Me.cmdGetRecord.Enabled = True
        End If
    End Sub
    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        MySearchValue = ""
        If LTrim(RTrim(Me.txtDobSearch.Text)) <> "" Then
            'MySearchValue = txtDobSearch.Text
            Dim str() As String
            str = DoDate_Process(Trim(Me.txtDobSearch.Text), txtDobSearch)
            If (str(2) = Nothing) Then
                Dim errMsg = str(0).Insert(18, " Date of Birth, ")
                lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
                lblMsg.Visible = True
                txtDobSearch.Focus()
                Exit Sub
            Else
                'myarrData = Split(Trim(txtSearch.Value), "/")
                MySearchValue = DoConvertToDbDateFormat(Trim(Me.txtDobSearch.Text))
            End If
        End If

        cboSearch.Items.Clear()
        cboSearch.Items.Add("* Select Agent *")
        Dim dt As DataTable = SearchAgent(MySearchValue, Trim(txtAgtNameSearch.Text), Trim(txtEplatformSearch.Text), _
                                          Trim(txtMobileSearch.Text)).Tables(0)
        cboSearch.DataSource = dt
        cboSearch.DataValueField = "MyFieldValue"
        cboSearch.DataTextField = "MyFiledText"
        cboSearch.DataBind()
    End Sub
    Private Function SearchAgent(ByVal dob As String, ByVal name As String, ByVal eplatform As String, ByVal mobile As String) As DataSet
        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT AG.TBIL_AGCY_NEW_AGENT_CD as MyFieldValue, AG.TBIL_AGCY_AGENT_NAME as MyFiledText"
        strSQL = strSQL & " FROM " & strTable & " as AG "


        If dob <> "" And name = "" And eplatform = "" And mobile = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"

        ElseIf name <> "" And dob = "" And eplatform = "" And mobile = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"

        ElseIf eplatform <> "" And dob = "" And name = "" And mobile = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"

        ElseIf mobile <> "" And dob = "" And name = "" And eplatform = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"
            '1st test end

        ElseIf dob <> "" And name <> "" And eplatform = "" And mobile = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"

        ElseIf dob <> "" And eplatform <> "" And name = "" And mobile = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"

        ElseIf dob <> "" And mobile <> "" And name = "" And eplatform = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"


        ElseIf name <> "" And eplatform <> "" And dob = "" And mobile = "" Then
            strSQL = strSQL & " WHERE  AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"

        ElseIf name <> "" And mobile <> "" And dob = "" And eplatform = "" Then
            strSQL = strSQL & " WHERE  AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
            strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"


        ElseIf eplatform <> "" And mobile <> "" And dob = "" And name = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"
            '2nd test end
 
        ElseIf dob <> "" And name <> "" And eplatform <> "" And mobile = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"

        ElseIf dob <> "" And name <> "" And mobile <> "" And eplatform = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
            strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"

        ElseIf dob <> "" And eplatform <> "" And mobile <> "" And name = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"

        ElseIf name <> "" And eplatform <> "" And mobile <> "" And dob = "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"
            '3rd test end

        ElseIf dob <> "" And name <> "" And eplatform <> "" And mobile <> "" Then
            strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
            strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"
            strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"
        End If
        strSQL = strSQL & " ORDER BY RTRIM(ISNULL(AG.TBIL_AGCY_AGENT_NAME,''))"



        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        objOLEConn.Open()

        Try
            Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

            Dim objDS As DataSet = New DataSet()
            objDA.Fill(objDS, strTable)
            Return objDS
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try
        Return Nothing
    End Function


    'Private Function SearchAgent(ByVal dob As String, ByVal name As String, ByVal eplatform As String, ByVal mobile As String) As DataSet
    '    strTable = strTableName
    '    strSQL = ""
    '    strSQL = strSQL & "SELECT AG.TBIL_AGCY_NEW_AGENT_CD as MyFieldValue, AG.TBIL_AGCY_AGENT_NAME as MyFiledText"
    '    strSQL = strSQL & " FROM " & strTable & " as AG "
    '    strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_DOB = '" & Convert.ToDateTime(RTrim(dob)) & "'"

    '    If name <> "" And eplatform = "" And mobile = "" Then
    '        strSQL = strSQL & " AND AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"

    '    ElseIf eplatform <> "" And name = "" And mobile = "" Then
    '        strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"

    '    ElseIf mobile <> "" And name = "" And eplatform = "" Then
    '        strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"

    '    ElseIf name <> "" And eplatform <> "" And mobile = "" Then
    '        strSQL = strSQL & " AND AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
    '        strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"

    '    ElseIf name <> "" And mobile <> "" And eplatform = "" Then
    '        strSQL = strSQL & " AND AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
    '        strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"

    '    ElseIf eplatform <> "" And mobile <> "" And name = "" Then
    '        strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"
    '        strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"
    '    ElseIf name <> "" And eplatform <> "" And mobile <> "" Then
    '        strSQL = strSQL & " AND AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(name) & "%'"
    '        strSQL = strSQL & " AND AG.TBIL_AGCY_EPLAT_AGT_CODE = '" & RTrim(eplatform) & "'"
    '        strSQL = strSQL & " AND AG.TBIL_AGENT_PHONE1 = '" & RTrim(mobile) & "'"
    '    End If
    '    strSQL = strSQL & " ORDER BY RTRIM(ISNULL(AG.TBIL_AGCY_AGENT_NAME,''))"



    '    Dim mystrCONN As String = CType(Session("connstr"), String)
    '    Dim objOLEConn As New OleDbConnection(mystrCONN)

    '    'open connection to database
    '    objOLEConn.Open()

    '    Try
    '        Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

    '        Dim objDS As DataSet = New DataSet()
    '        objDA.Fill(objDS, strTable)
    '        Return objDS
    '    Catch ex As Exception
    '        '_rtnMessage = "Entry failed! " + ex.Message.ToString()
    '    End Try
    '    Return Nothing
    'End Function

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
            Else
                ClearManagerLevels()
                strStatus = Proc_OpenRecord(Me.cboSearch.SelectedItem.Value)
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        DoNew()
    End Sub

    Private Function SearchSupervisor(ByVal design_code As String, ByVal searchval As String) As DataSet
        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT AG.TBIL_AGCY_NEW_AGENT_CD as MyFieldValue, AG.TBIL_AGCY_AGENT_NAME as MyFiledText"
        strSQL = strSQL & " FROM " & strTable & " as AG "
        strSQL = strSQL & " WHERE AG.TBIL_AGCY_AGENT_NAME LIKE '%" & RTrim(searchval) & "%'"
        strSQL = strSQL & " AND TBIL_AGCY_DESIGNATN = '" & design_code & "'"
        strSQL = strSQL & " ORDER BY RTRIM(ISNULL(AG.TBIL_AGCY_AGENT_NAME,''))"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        objOLEConn.Open()

        Try
            Dim objDA As OleDbDataAdapter = New OleDbDataAdapter(strSQL, objOLEConn)

            Dim objDS As DataSet = New DataSet()
            objDA.Fill(objDS, strTable)
            Return objDS
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try
        Return Nothing
    End Function

    Protected Sub cmdSupervisor_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSupervisor_Search.Click
        If LTrim(RTrim(Me.txtSupervisor_Search.Text)) = "" Then
        Else
            If cboDesignation.SelectedIndex = 0 Then
                lblMsg.Text = "Please select designation"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
                cboDesignation.Focus()
            End If

            Dim immediate_supervisor = Val(cboDesignation.SelectedValue) + 1

            MySearchValue = Me.txtSupervisor_Search.Text
            cboSupervisor_Search.Items.Clear()
            cboSupervisor_Search.Items.Add("* Select Supervisor *")
            Dim dt As DataTable = SearchSupervisor(immediate_supervisor, MySearchValue).Tables(0)
            cboSupervisor_Search.DataSource = dt
            cboSupervisor_Search.DataValueField = "MyFieldValue"
            cboSupervisor_Search.DataTextField = "MyFiledText"
            cboSupervisor_Search.DataBind()
        End If
    End Sub

    Private Sub SearchManagerLevelName(ByVal searchval As String)
        '  Dim level As String
        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 AG.*"
        strSQL = strSQL & " FROM " & strTable & " as AG "
        strSQL = strSQL & " WHERE AG.TBIL_AGCY_NEW_AGENT_CD = '" & RTrim(searchval) & "'"
        'strSQL = strSQL & " ORDER BY RTRIM(ISNULL(AG.TBIL_AGCY_AGENT_NAME,''))"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If

        'open connection to database
        objOLEConn.Open()

        Try
            Dim objOLEDR As OleDbDataReader
            Dim objOLEComm As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
            objOLEDR = objOLEComm.ExecuteReader()
            If objOLEDR.Read = True Then
                Dim designation = RTrim(CType(objOLEDR("TBIL_AGCY_DESIGNATN") & vbNullString, String))
                Dim MgrCode = ""
                Dim AgtCode = ""

                If designation = "5" Then
                    AgtCode = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
                    objOLEDR.Close()
                    LoadLevelManger("5", AgtCode, cboLvlMgrName5)
                    'objOLEComm.Connection=CL
                ElseIf designation = "4" Then
                    AgtCode = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
                    MgrCode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))
                    objOLEDR.Close()
                    LoadLevelManger("4", AgtCode, cboLvlMgrName4)
                    SearchManagerLevelName(MgrCode)
                ElseIf designation = "3" Then
                    AgtCode = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
                    MgrCode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))
                    objOLEDR.Close()
                    LoadLevelManger("3", AgtCode, cboLvlMgrName3)
                    SearchManagerLevelName(MgrCode)
                ElseIf designation = "2" Then
                    AgtCode = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
                    MgrCode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))
                    objOLEDR.Close()
                    LoadLevelManger("2", AgtCode, cboLvlMgrName2)
                    SearchManagerLevelName(MgrCode)
                ElseIf designation = "1" Then
                    AgtCode = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
                    MgrCode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))
                    objOLEDR.Close()
                    LoadLevelManger("1", AgtCode, cboLvlMgrName1)
                    SearchManagerLevelName(MgrCode)
                End If
            End If
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try
        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
    End Sub


    'Private Sub SearchManagerLevelName(ByVal searchval As String)
    '    '  Dim level As String
    '    strTable = strTableName
    '    strSQL = ""
    '    strSQL = strSQL & "SELECT TOP 1 AG.*"
    '    strSQL = strSQL & " FROM " & strTable & " as AG "
    '    strSQL = strSQL & " WHERE AG.TBIL_AGCY_NEW_AGENT_CD = '" & RTrim(searchval) & "'"
    '    'strSQL = strSQL & " ORDER BY RTRIM(ISNULL(AG.TBIL_AGCY_AGENT_NAME,''))"

    '    Dim mystrCONN As String = CType(Session("connstr"), String)
    '    Dim objOLEConn As New OleDbConnection(mystrCONN)

    '    If objOLEConn.State = ConnectionState.Open Then
    '        objOLEConn.Close()
    '    End If

    '    'open connection to database
    '    objOLEConn.Open()

    '    Try
    '        Dim objOLEDR As OleDbDataReader
    '        Dim objOLEComm As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
    '        objOLEDR = objOLEComm.ExecuteReader()
    '        If objOLEDR.Read = True Then
    '            Dim designation = RTrim(CType(objOLEDR("TBIL_AGCY_DESIGNATN") & vbNullString, String))
    '            Dim MgrCode = ""

    '            If designation = "5" Then
    '                li = New ListItem
    '                li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))
    '                li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                cboLvlMgrName5.Items.Add(li)
    '                cboLvlMgrName5.SelectedValue = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                objOLEDR.Close()
    '                'objOLEComm.Connection=CL
    '            ElseIf designation = "4" Then
    '                li = New ListItem
    '                li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))
    '                li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                cboLvlMgrName4.Items.Add(li)
    '                cboLvlMgrName4.SelectedValue = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                MgrCode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))
    '                objOLEDR.Close()
    '                SearchManagerLevelName(MgrCode)
    '            ElseIf designation = "3" Then
    '                li = New ListItem
    '                li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))
    '                li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                cboLvlMgrName3.Items.Add(li)
    '                cboLvlMgrName3.SelectedValue = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                MgrCode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))
    '                objOLEDR.Close()
    '                SearchManagerLevelName(MgrCode)
    '            ElseIf designation = "2" Then
    '                li = New ListItem
    '                li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))
    '                li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                cboLvlMgrName2.Items.Add(li)
    '                cboLvlMgrName2.SelectedValue = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                MgrCode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))
    '                objOLEDR.Close()
    '                SearchManagerLevelName(MgrCode)
    '            ElseIf designation = "1" Then
    '                li = New ListItem
    '                li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))
    '                li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                cboLvlMgrName1.Items.Add(li)
    '                cboLvlMgrName1.SelectedValue = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
    '                MgrCode = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_SUPERVISOR_CD") & vbNullString, String))
    '                objOLEDR.Close()
    '                SearchManagerLevelName(MgrCode)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        '_rtnMessage = "Entry failed! " + ex.Message.ToString()
    '    End Try
    '    If objOLEConn.State = ConnectionState.Open Then
    '        objOLEConn.Close()
    '    End If
    'End Sub
    Protected Sub cboSupervisor_Search_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSupervisor_Search.SelectedIndexChanged
        cboLvlMgrName1.Items.Clear()
        cboLvlMgrName1.Items.Add("Select")
        cboLvlMgrName2.Items.Clear()
        cboLvlMgrName2.Items.Add("Select")
        cboLvlMgrName3.Items.Clear()
        cboLvlMgrName3.Items.Add("Select")
        cboLvlMgrName4.Items.Clear()
        cboLvlMgrName4.Items.Add("Select")
        cboLvlMgrName5.Items.Clear()
        cboLvlMgrName5.Items.Add("Select")

        Try
            If Me.cboSupervisor_Search.SelectedIndex = -1 Or Me.cboSupervisor_Search.SelectedIndex = 0 Or _
            Me.cboSupervisor_Search.SelectedItem.Value = "" Or Me.cboSupervisor_Search.SelectedItem.Value = "*" Then
            Else
                txtSupervisor_Code.Text = Me.cboSupervisor_Search.SelectedItem.Value
                txtSupervisor_Name.Text = Me.cboSupervisor_Search.SelectedItem.Text
                SearchManagerLevelName(Me.cboSupervisor_Search.SelectedItem.Value)
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtAgentABSCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgentABSCode.TextChanged
        If Trim(Me.txtAgentABSCode.Text) <> "" Then
            Call Proc_OpenRecord(Trim(Me.txtAgentABSCode.Text))
        End If
    End Sub

    Private Sub LoadLevelManger(ByVal DesignationCode As String, ByVal SupervisorCode As String, ByVal drpLevelManager As DropDownList)
        '  Dim level As String
        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT AG.*"
        strSQL = strSQL & " FROM " & strTable & " as AG "
        strSQL = strSQL & " WHERE AG.TBIL_AGCY_DESIGNATN = '" & RTrim(DesignationCode) & "'"
        'strSQL = strSQL & " ORDER BY RTRIM(ISNULL(AG.TBIL_AGCY_AGENT_NAME,''))"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If

        'open connection to database
        objOLEConn.Open()
        Dim objOLEDR As OleDbDataReader
        Dim objOLEComm As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        Try
            objOLEDR = objOLEComm.ExecuteReader()
            While objOLEDR.Read = True
                li = New ListItem
                li.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))
                li.Value = RTrim(CType(objOLEDR("TBIL_AGCY_NEW_AGENT_CD") & vbNullString, String))
                drpLevelManager.Items.Add(li)
            End While
            drpLevelManager.SelectedValue = SupervisorCode
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()
        End Try

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
    End Sub

    Private Sub ClearManagerLevels()
        cboLvlMgrName1.Items.Clear()
        cboLvlMgrName1.Items.Add("Select")
        cboLvlMgrName2.Items.Clear()
        cboLvlMgrName2.Items.Add("Select")
        cboLvlMgrName3.Items.Clear()
        cboLvlMgrName3.Items.Add("Select")
        cboLvlMgrName4.Items.Clear()
        cboLvlMgrName4.Items.Add("Select")
        cboLvlMgrName5.Items.Clear()
        cboLvlMgrName5.Items.Add("Select")
        cboSupervisor_Search.Items.Clear()
    End Sub
End Class
