Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class I_LIFE_PRG_LI_INDV_POLY_FUNERAL
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

    Dim dblSum_Assured_LC As Double = 0
    Dim dblSum_Assured_FC As Double = 0

    Dim dblAnnual_Contrib_LC As Double = 0
    Dim dblAnnual_Contrib_FC As Double = 0

    Dim dblPrem_Rate As Double = 0
    Dim dblRate_Per As Integer = 0
    Dim dblTerm As Double = 0

    Dim dblMOP_Rate As Double = 0
    Dim dblMOP_Per As Double = 0
    Dim dblMOP_Contrib_LC As Double = 0
    Dim dblMOP_Contrib_FC As Double = 0

    Dim dblMOP_Fee As Double = 0
    Dim dblMOP_Prem_LC As Double = 0
    Dim dblMOP_Prem_FC As Double = 0

    Dim dblTmp_Amt As Double = 0
    Dim dblTmp_AmtX As Double = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        strTableName = "TBIL_FUNERAL_SA_TAB"

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

            'Call gnProc_Populate_Box("IL_CODE_LIST", "015", Me.cboGender)
            'Call gnProc_Populate_Box("IL_CODE_LIST", "013", Me.cboBenef_Relationship)
            Call gnProc_Populate_Box("IL_CODE_LIST", "015", Me.cboBenef_Relationship)

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
                    Me.txtDOB.Text = oAL.Item(9)
                    'Me.txtDOB_ANB.Text = oAL.Item(10)
                    Me.txtDOB_ANB_Prm.Text = oAL.Item(10)
                    'Me.txtBenef_Cover_ID.Text = oAL.Item(11)
                    Me.txtPrem_Rate_TypeNum.Text = oAL.Item(12)
                    Me.txtPrem_Rate_Applied_On.Text = oAL.Item(13)
                    Me.txtPrem_Rate_Code.Text = oAL.Item(14)
                    Me.txtPrem_Enrollee_Num.Text = oAL.Item(15)
                    Me.txtPrem_MOP_Type.Text = oAL.Item(16)
                    Me.txtPrem_Ann_Contrib_LC_Prm.Text = oAL.Item(17)
                    Me.cmdNext.Enabled = True

                    If UCase(oAL.Item(18).ToString) = "A" Then
                        'Me.cmdNew_ASP.Visible = False
                        'Me.cmdSave_ASP.Visible = False
                        ''Me.cmdDelete_ASP.Visible = False
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

        If Me.txtAction.Text = "Delete" Then
            'Call DoDelete()
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Delete_Item" Then
            Call Proc_DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call Proc_DoSave()
        Me.txtAction.Text = ""

    End Sub

    Protected Sub DoProc_Cover_ID_Change()

    End Sub

    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT *"
        strSQL = strSQL & " FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(strF_ID) & "'"
        strSQL = strSQL & " AND TBIL_FUN_PROP_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " ORDER BY TBIL_FUN_SNO"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            objOLEConn = Nothing
        End Try

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
        strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_FUN_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_FUN_REC_ID = " & Val(RTrim(Me.txtRecNo.Text)) & ""

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
        strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(strREC_ID) & "'"
        strSQL = strSQL & " AND TBIL_FUN_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        strSQL = strSQL & " AND TBIL_FUN_REC_ID = " & Val(RTrim(Me.txtRecNo.Text)) & ""

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

        '                   subctrl.ID = "txtPrem_Rate" Or _
        '                   subctrl.ID = "txtPrem_Rate_Per" Or _

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
                           subctrl.ID = "txtDOB" Or _
                           subctrl.ID = "txtPrem_Ann_Contrib_LC_Prm" Or _
                           subctrl.ID = "txtDOB_ANB_Prm" Or _
                           subctrl.ID = "txtPrem_Rate_TypeNum" Or _
                           subctrl.ID = "txtPrem_Rate_Applied_On" Or _
                           subctrl.ID = "txtPrem_Rate_Code" Or _
                           subctrl.ID = "txtPrem_Enrollee_Num" Or _
                           subctrl.ID = "txtPrem_MOP_Type" Or _
                           subctrl.ID = "txtPrem_MOP_Per" Or _
                           subctrl.ID = "xyz_123" Then
                            'Control(ID) : txtAction
                            'Control(ID) : txtFileNum
                            'Control(ID) : txtPolNum
                            'Control(ID) : txtQuote_Num
                            'Control(ID) : txtRecNo
                            'Control(ID) : txtBenef_SN
                            'Control(ID) : txtBenef_Type
                            'Control(ID) : txtBenef_TypeName
                            'Control(ID) : txtBenef_Category
                            'Control(ID) : txtBenef_CategoryName
                            'Control(ID) : txtBenef_Name
                            'Control(ID) : txtBenef_Percentage
                            'Control(ID) : txtPrem_SA_LC
                            'Control(ID) : txtDOB_ANB
                            'Control(ID) : txtBenef_Relationship
                            'Control(ID) : txtBenef_RelationshipName
                            'Control(ID) : txtBenef_Address
                            'Control(ID) : txtPrem_Ann_Contrib_LC
                        Else
                            'Response.Write("<br> Control ID: " & subctrl.ID)
                            CType(subctrl, TextBox).Text = ""
                        End If
                    End If
                    If TypeOf subctrl Is System.Web.UI.WebControls.DropDownList Then
                        CType(subctrl, DropDownList).SelectedIndex = -1
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
            Me.lblMsg.Text = "Missing " & Me.lblProduct.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Me.txtProduct_Num.Text = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblProduct.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call MOD_GEN.gnGET_SelectedItem(Me.cboBenef_Type, Me.txtBenef_Type, Me.txtBenef_TypeName, Me.lblMsg)
        If Trim(Me.txtBenef_Type.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblBenef_Type.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Call MOD_GEN.gnGET_SelectedItem(Me.cboBenef_Category, Me.txtBenef_Category, Me.txtBenef_CategoryName, Me.lblMsg)
        If Trim(Me.txtBenef_Category.Text) = "" Then
            Me.lblMsg.Text = "Missing " & Me.lblBenef_Category.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtBenef_Name.Text) = "" Or Trim(Me.txtBenef_Name.Text) = "." Or Trim(Me.txtBenef_Name.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_Name.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Me.txtDOB_ANB.Text = Trim(Me.txtDOB_ANB.Text)
        Call MOD_GEN.gnInitialize_Numeric(Me.txtDOB_ANB)

        'Call MOD_GEN.gnInitialize_Numeric(Me.txtBenef_Percentage)
        'If Val(Me.txtBenef_Percentage.Text) <= 0 Then
        '    Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_Percentage.Text
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    Exit Sub
        'End If


        Call MOD_GEN.gnGET_SelectedItem(Me.cboBenef_Relationship, Me.txtBenef_Relationship, Me.txtBenef_RelationshipName, Me.lblMsg)
        If Trim(Me.txtBenef_Relationship.Text) = "" Or Trim(Me.txtBenef_Relationship.Text) = "." Or Trim(Me.txtBenef_Relationship.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_Relationship.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If


        If Trim(Me.txtBenef_Address.Text) = "" Or Trim(Me.txtBenef_Address.Text) = "." Or Trim(Me.txtBenef_Address.Text) = "*" Then
            Me.lblMsg.Text = "Missing or invalid " & Me.lblBenef_Address.Text
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Me.txtPrem_Ann_Contrib_LC.Text = Me.txtPrem_Ann_Contrib_LC.Text
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_Ann_Contrib_LC)
        'If Trim(Me.txtPrem_Ann_Contrib_LC.Text) = "" Or Trim(Me.txtPrem_Ann_Contrib_LC.Text) = "." Or Trim(Me.txtPrem_Ann_Contrib_LC.Text) = "*" Then
        'Me.lblMsg.Text = "Missing or invalid " & Me.lblPrem_Ann_Contrib_LC.Text
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        'Exit Sub
        'End If


        Me.txtPrem_SA_LC.Text = Me.txtPrem_SA_LC.Text
        Call MOD_GEN.gnInitialize_Numeric(Me.txtPrem_SA_LC)

        If Val(Trim(Me.txtPrem_Ann_Contrib_LC.Text)) = 0 And Val(Trim(Me.txtPrem_SA_LC.Text)) = 0 Then
            Me.lblMsg.Text = "Missing Value - Annual Contribution or Sum Assured..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

Proc_Skip_ANB:


        'Me.lblMsg.Text = "About to submit data... "
        'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"



        If Trim(txtBenef_Cover_ID.Text) = "" Then
            Me.txtBenef_Cover_ID.Text = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_IL"), RTrim("FUN_COVER_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))
        End If
        If Trim(txtBenef_Cover_ID.Text) = "" Or Trim(Me.txtBenef_Cover_ID.Text) = "0" Or Trim(Me.txtBenef_Cover_ID.Text) = "*" Then
            'Me.txtFileNum.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next COVER NO. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtBenef_Cover_ID.Text) = "PARAM_ERR" Then
            Me.txtBenef_Cover_ID.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get the next COVER NO - INVALID PARAMETER(S) ..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtBenef_Cover_ID.Text) = "DB_ERR" Then
            Me.txtBenef_Cover_ID.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to connect to database. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        ElseIf Trim(Me.txtBenef_Cover_ID.Text) = "ERR_ERR" Then
            Me.txtBenef_Cover_ID.Text = ""
            Me.lblMsg.Text = "Sorry!. Unable to get connection object. Please contact your service provider..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            'Me.lblMsg.Text = "Status:"
            Exit Sub
        End If

        If Trim(txtBenef_SN.Text) = "" Then
            Me.txtBenef_SN.Text = MOD_GEN.gnGet_Serial_No(RTrim("GET_SN_IL"), RTrim("FUN_SN"), Trim(Me.txtFileNum.Text), Trim(Me.txtQuote_Num.Text))
        End If

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try



        Dim myRetValue As String = "0"
        dblMOP_Rate = 0
        dblMOP_Per = 0

        myRetValue = MOD_GEN.gnGET_RATE("GET_IL_MOP_FACTOR", "IND", Me.txtPrem_MOP_Type.Text, "", "", "", Me.lblMsg, Nothing, Me.txtPrem_MOP_Per)
        If Left(LTrim(myRetValue), 3) = "ERR" Then
            dblMOP_Rate = 0
        Else
            dblMOP_Rate = Val(myRetValue)
            dblMOP_Per = Val(Me.txtPrem_MOP_Per.Text)
        End If

        Dim myTerm As String = ""
        'myTerm = Me.txtPrem_Period_Yr.Text
        myTerm = Me.txtBenef_Type.Text

        myRetValue = MOD_GEN.gnGET_RATE("GET_IL_PREMIUM_RATE", "IND", Me.txtPrem_Rate_Code.Text, Me.txtProduct_Num.Text, myTerm, Me.txtDOB_ANB_Prm.Text, Me.lblMsg, Me.txtPrem_Rate_Per)

        'MsgBox(myRetValue)

        If Left(LTrim(myRetValue), 3) = "ERR" Then
            'Me.cboPrem_Rate_Code.SelectedIndex = -1
            Me.txtPrem_Rate.Text = "0.00"
            Me.txtPrem_Rate_Per.Text = "0"
        Else
            Me.txtPrem_Rate.Text = myRetValue.ToString
        End If

        dblPrem_Rate = Val(Me.txtPrem_Rate.Text)
        dblRate_Per = Val(txtPrem_Rate_Per.Text)

        dblAnnual_Contrib_LC = 0
        dblAnnual_Contrib_FC = 0

        dblSum_Assured_LC = 0
        dblSum_Assured_FC = 0
        dblTmp_Amt = 0
        dblTmp_AmtX = 0



        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "S" Then
            dblSum_Assured_LC = Val(Me.txtPrem_SA_LC.Text)
            dblSum_Assured_FC = dblSum_Assured_LC
            If Val(dblSum_Assured_LC) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
                dblTmp_Amt = dblSum_Assured_LC * dblPrem_Rate / dblRate_Per
            End If
            dblAnnual_Contrib_LC = dblTmp_Amt
            Me.txtPrem_Ann_Contrib_LC.Text = Format(dblAnnual_Contrib_LC, "###########0.00")
        End If


        'If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "P" Then
        '    dblAnnual_Contrib_LC = Val(Me.txtPrem_Ann_Contrib_LC.Text)
        '    dblAnnual_Contrib_LC = Val(Me.txtPrem_Ann_Contrib_LC_Prm.Text)
        '    dblAnnual_Contrib_FC = dblAnnual_Contrib_LC

        '    If Val(dblAnnual_Contrib_LC) > 0 And Val(Me.txtPrem_Enrollee_Num.Text) <> 0 Then
        '        dblTmp_Amt = dblAnnual_Contrib_LC / Val(Me.txtPrem_Enrollee_Num.Text)
        '        Me.txtPrem_Ann_Contrib_LC.Text = dblTmp_Amt.ToString
        '    Else
        '        dblTmp_Amt = 0
        '    End If

        '    If Val(dblTmp_Amt) > 0 And dblMOP_Rate <> 0 Then
        '        dblTmp_AmtX = (dblTmp_Amt / dblMOP_Rate)
        '    Else
        '        dblTmp_AmtX = 0
        '    End If

        '    If Val(dblTmp_AmtX) > 0 And Val(dblPrem_Rate) <> 0 And Val(dblRate_Per) <> 0 Then
        '        dblTmp_Amt = dblTmp_AmtX * dblRate_Per / dblPrem_Rate
        '    Else
        '        dblTmp_Amt = 0
        '    End If

        '    Me.txtPrem_SA_LC.Text = Format(dblTmp_Amt, "###########0.00")
        'End If



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
        strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(txtFileNum.Text) & "'"
        'strSQL = strSQL & " AND TBIL_FUN_PROP_NO = '" & RTrim(txtQuote_Num.Text) & "'"
        'If Val(LTrim(RTrim(Me.txtRecNo.Text))) <> 0 Then
        strSQL = strSQL & " AND TBIL_FUN_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"
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

                drNewRow("TBIL_FUN_FILE_NO") = RTrim(Me.txtFileNum.Text)
                drNewRow("TBIL_FUN_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                'drNewRow("TBIL_FUN_POLY_NO") = RTrim(Me.txtPolNum.Text)

                drNewRow("TBIL_FUN_MDLE") = RTrim("I")
                '
                drNewRow("TBIL_FUN_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)

                drNewRow("TBIL_FUN_COVER_ID") = Val(Me.txtBenef_Cover_ID.Text)
                drNewRow("TBIL_FUN_SNO") = Val(Me.txtBenef_SN.Text)
                drNewRow("TBIL_FUN_PERSON_COVERD") = RTrim(Me.txtBenef_Type.Text)
                drNewRow("TBIL_FUN_RELATION") = RTrim(Me.txtBenef_Category.Text)


                drNewRow("TBIL_FUN_AGE") = Val(Me.txtDOB_ANB.Text)
                drNewRow("TBIL_FUN_SEX_COVERD") = RTrim(Me.txtBenef_Relationship.Text)
                drNewRow("TBIL_FUN_NAME_COVERD") = RTrim(Me.txtBenef_Name.Text)
                drNewRow("TBIL_FUN_ADDRES_COVERD") = Trim(Me.txtBenef_Address.Text)

                'drNewRow("TBIL_POL_BENF_PCENT") = Val(Me.txtBenef_Percentage.Text)
                drNewRow("TBIL_FUN_ANN_CONTRIB") = Trim(Me.txtPrem_Ann_Contrib_LC.Text)
                'If Trim(Me.txtBenef_DOB.Text) <> "" Then
                'drNewRow("TBIL_FUN_SA") = dteDOB
                'End If

                drNewRow("TBIL_FUN_SA") = Trim(Me.txtPrem_SA_LC.Text)

                drNewRow("TBIL_FUN_PREM_RATE") = Val(Me.txtPrem_Rate.Text)
                drNewRow("TBIL_FUN_PREM_PER") = Val(Me.txtPrem_Rate_Per.Text)


                drNewRow("TBIL_FUN_TAG") = ""
                drNewRow("TBIL_FUN_FLAG") = "A"
                drNewRow("TBIL_FUN_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_FUN_KEYDTE") = Now

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
                    .Rows(0)("TBIL_FUN_FILE_NO") = RTrim(Me.txtFileNum.Text)
                    .Rows(0)("TBIL_FUN_PROP_NO") = RTrim(Me.txtQuote_Num.Text)
                    '.Rows(0)("TBIL_FUN_POLY_NO") = RTrim(Me.txtPolNum.Text)

                    .Rows(0)("TBIL_FUN_MDLE") = RTrim("I")

                    .Rows(0)("TBIL_FUN_PRDCT_CD") = RTrim(Me.txtProduct_Num.Text)

                    .Rows(0)("TBIL_FUN_COVER_ID") = Val(Me.txtBenef_Cover_ID.Text)
                    .Rows(0)("TBIL_FUN_SNO") = Val(Me.txtBenef_SN.Text)
                    .Rows(0)("TBIL_FUN_PERSON_COVERD") = RTrim(Me.txtBenef_Type.Text)
                    .Rows(0)("TBIL_FUN_RELATION") = RTrim(Me.txtBenef_Category.Text)


                    .Rows(0)("TBIL_FUN_AGE") = Val(Me.txtDOB_ANB.Text)
                    .Rows(0)("TBIL_FUN_SEX_COVERD") = RTrim(Me.txtBenef_Relationship.Text)
                    .Rows(0)("TBIL_FUN_NAME_COVERD") = RTrim(Me.txtBenef_Name.Text)
                    .Rows(0)("TBIL_FUN_ADDRES_COVERD") = Trim(Me.txtBenef_Address.Text)

                    '.Rows(0)("TBIL_POL_BENF_PCENT") = Val(Me.txtBenef_Percentage.Text)
                    .Rows(0)("TBIL_FUN_ANN_CONTRIB") = RTrim(Me.txtPrem_Ann_Contrib_LC.Text)
                    'If Trim(Me.txtBenef_DOB.Text) <> "" Then
                    '.Rows(0)("TBIL_FUN_SA") = dteDOB
                    'End If

                    .Rows(0)("TBIL_FUN_SA") = RTrim(Me.txtPrem_SA_LC.Text)

                    .Rows(0)("TBIL_FUN_PREM_RATE") = Val(Me.txtPrem_Rate.Text)
                    .Rows(0)("TBIL_FUN_PREM_PER") = Val(Me.txtPrem_Rate_Per.Text)

                    .Rows(0)("TBIL_FUN_FLAG") = "C"
                    '.Rows(0)("TBIL_FUN_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_FUN_KEYDTE") = Now
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



        mystrCONN = CType(Session("connstr"), String)
        objOLEConn = New OleDbConnection()
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



        '**********************************************************
        ' FUNERAL START CODES
        '**********************************************************
        'strREC_ID = Me.txtFileNum.Text
        'Dim myTmp_Contrib As Double = 0
        'Dim myTmp_SA As Double = 0
        'Dim myTmp_Amt As Double = 0

        'Select Case Trim(Me.txtProduct_Num.Text)
        '    Case "F001", "F002"
        '        strTable = strTableName
        '        strTable = "TBIL_FUNERAL_SA_TAB"
        '        strSQL = ""
        '        strSQL = strSQL & "SELECT SUM(TBIL_FUN_ANN_CONTRIB) AS TOT_CONTRIB, SUM(TBIL_FUN_SA) AS TOT_SA"
        '        strSQL = strSQL & " FROM " & strTable & ""
        '        strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(strREC_ID) & "'"
        '        strSQL = strSQL & " AND TBIL_FUN_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"
        '        'strSQL = strSQL & " AND TBIL_FUN_POLY_NO = '" & RTrim(strP_ID) & "'"

        '        Dim objFun_Cmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        '        'objFun_Cmd.CommandTimeout = 180
        '        objFun_Cmd.CommandType = CommandType.Text
        '        'objFun_Cmd.CommandType = CommandType.StoredProcedure
        '        'objFun_Cmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        '        'objFun_Cmd.Parameters.Add("p01", OleDbType.VarChar, 40).Value = strREC_ID
        '        'objFun_Cmd.Parameters.Add("p01", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        '        Dim intFun As Integer = 0

        '        Dim objFun_DR As OleDbDataReader

        '        objFun_DR = objFun_Cmd.ExecuteReader()
        '        If (objFun_DR.Read()) Then
        '            myTmp_Contrib = Val(objFun_DR("TOT_CONTRIB") & vbNullString)
        '            myTmp_SA = Val(objFun_DR("TOT_SA") & vbNullString)
        '        Else
        '            myTmp_Contrib = Val(0)
        '            myTmp_SA = Val(0)
        '        End If

        '        objFun_DR.Close()
        '        objFun_Cmd.Dispose()

        '        objFun_DR = Nothing
        '        objFun_Cmd = Nothing

        '        'for SA
        '        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "S" Then
        '            If Val(myTmp_SA) > 0 Then
        '                myTmp_Amt = myTmp_SA
        '                strSQL = ""
        '                strSQL = strSQL & " UPDATE TBIL_POLICY_PREM_INFO"
        '                strSQL = strSQL & " SET TBIL_POL_PRM_SA_LC = " & Val(myTmp_Amt)
        '                strSQL = strSQL & " ,TBIL_POL_PRM_SA_FC = " & Val(myTmp_Amt)
        '                strSQL = strSQL & " ,TBIL_POL_PRM_LIFE_COVER_SA_LC = " & Val(myTmp_Amt)
        '                strSQL = strSQL & " ,TBIL_POL_PRM_LIFE_COVER_SA_FC = " & Val(myTmp_Amt)
        '                strSQL = strSQL & " WHERE TBIL_POL_PRM_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
        '                strSQL = strSQL & " AND TBIL_POL_PRM_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

        '                objFun_Cmd = New OleDbCommand(strSQL, objOLEConn)
        '                'objFun_Cmd.CommandTimeout = 180
        '                objFun_Cmd.CommandType = CommandType.Text
        '                intFun = objFun_Cmd.ExecuteNonQuery()

        '                objFun_Cmd.Dispose()
        '                objFun_Cmd = Nothing

        '            End If
        '        End If

        '        'for premium
        '        If Trim(Me.txtPrem_Rate_TypeNum.Text) = "T" And Trim(Me.txtPrem_Rate_Applied_On.Text) = "P" Then
        '            If Val(myTmp_Contrib) > 0 Then
        '                myTmp_Amt = myTmp_Contrib
        '                strSQL = ""
        '                strSQL = strSQL & " UPDATE TBIL_POLICY_PREM_INFO"
        '                strSQL = strSQL & " SET TBIL_POL_PRM_ANN_CONTRIB_LC = " & Val(myTmp_Amt)
        '                strSQL = strSQL & " ,TBIL_POL_PRM_ANN_CONTRIB_FC = " & Val(myTmp_Amt)
        '                strSQL = strSQL & " WHERE TBIL_POL_PRM_FILE_NO = '" & RTrim(Me.txtFileNum.Text) & "'"
        '                strSQL = strSQL & " AND TBIL_POL_PRM_PROP_NO = '" & RTrim(Me.txtQuote_Num.Text) & "'"

        '                objFun_Cmd = New OleDbCommand(strSQL, objOLEConn)
        '                'objFun_Cmd.CommandTimeout = 180
        '                objFun_Cmd.CommandType = CommandType.Text
        '                intFun = objFun_Cmd.ExecuteNonQuery()

        '                objFun_Cmd.Dispose()
        '                objFun_Cmd = Nothing

        '            End If
        '        End If

        'End Select

        '**********************************************************
        ' FUNERAL END CODES
        '**********************************************************

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
        strSQL = strSQL & "SELECT TOP 1 FUN_TBL.*"
        strSQL = strSQL & " FROM " & strTable & " AS FUN_TBL"
        strSQL = strSQL & " WHERE FUN_TBL.TBIL_FUN_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND FUN_TBL.TBIL_FUN_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"

        strSQL = "SPIL_GET_POLICY_FUNERAL"

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

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_FUN_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_FUN_REC_ID") & vbNullString, String))

            Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_FUN_PROP_NO") & vbNullString, String))
            Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_FUN_POLY_NO") & vbNullString, String))

            'Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_FUN_PRDCT_CD") & vbNullString, String))

            Me.txtBenef_Cover_ID.Text = RTrim(CType(objOLEDR("TBIL_FUN_COVER_ID") & vbNullString, String))
            Me.txtBenef_SN.Text = RTrim(CType(objOLEDR("TBIL_FUN_SNO") & vbNullString, String))

            Me.txtBenef_Type.Text = RTrim(CType(objOLEDR("TBIL_FUN_PERSON_COVERD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBenef_Type, RTrim(Me.txtBenef_Type.Text))

            Me.txtBenef_Category.Text = RTrim(CType(objOLEDR("TBIL_FUN_RELATION") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBenef_Category, RTrim(Me.txtBenef_Category.Text))

            Me.txtBenef_Name.Text = RTrim(CType(objOLEDR("TBIL_FUN_NAME_COVERD") & vbNullString, String))
            'Me.txtBenef_Percentage.Text = Val(objOLEDR("TBIL_POL_BENF_PCENT") & vbNullString)

            'If IsDate(objOLEDR("TBIL_POL_BENF_BDATE")) Then
            'Me.txtBenef_DOB.Text = Format(CType(objOLEDR("TBIL_POL_BENF_BDATE"), DateTime), "dd/MM/yyyy")
            'End If
            Me.txtDOB_ANB.Text = Val(objOLEDR("TBIL_FUN_AGE") & vbNullString)

            Me.txtBenef_Relationship.Text = RTrim(CType(objOLEDR("TBIL_FUN_SEX_COVERD") & vbNullString, String))
            Call gnProc_DDL_Get(Me.cboBenef_Relationship, RTrim(Me.txtBenef_Relationship.Text))

            Me.txtBenef_Address.Text = RTrim(CType(objOLEDR("TBIL_FUN_ADDRES_COVERD") & vbNullString, String))
            Me.txtPrem_Ann_Contrib_LC.Text = RTrim(CType(objOLEDR("TBIL_FUN_ANN_CONTRIB") & vbNullString, String))
            Me.txtPrem_SA_LC.Text = RTrim(CType(objOLEDR("TBIL_FUN_SA") & vbNullString, String))

            Me.txtPrem_Rate.Text = RTrim(CType(objOLEDR("TBIL_FUN_PREM_RATE") & vbNullString, String))
            Me.txtPrem_Rate_Per.Text = RTrim(CType(objOLEDR("TBIL_FUN_PREM_PER") & vbNullString, String))

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
        Dim pvURL As String = "prg_li_indv_poly_prem.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim pvURL As String = ""
        pvURL = "prg_li_indv_poly_add_cover.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        pvURL = "prg_li_indv_poly_benefry.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        Select Case Trim(Me.txtProduct_Num.Text)
            Case "F001", "F002"
                pvURL = "prg_li_indv_poly_benefry.aspx?optfileid=" & Trim(Me.txtFileNum.Text)
        End Select
        pvURL = pvURL & "&optpolid=" & Trim(Me.txtPolNum.Text)
        pvURL = pvURL & "&optquotid=" & Trim(Me.txtQuote_Num.Text)
        Response.Redirect(pvURL)

    End Sub

End Class
