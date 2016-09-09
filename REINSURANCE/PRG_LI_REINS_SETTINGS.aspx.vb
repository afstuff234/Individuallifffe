Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Partial Class REINSURANCE_PRG_LI_REINS_SETTINGS
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_REINSURANCE_SETTINGS"
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri

        'Try
        '    strP_TYPE = CType(Request.QueryString("i"), String)
        '    strP_DESC = CType(Request.QueryString("optd"), String)
        'Catch ex As Exception
        '    strP_TYPE = "ERR_TYPE"
        '    strP_DESC = "ERR_DESC"
        'End Try

        'Try
        '    strPOP_UP = CType(Request.QueryString("popup"), String)
        'Catch ex As Exception
        '    strPOP_UP = "NO"
        'End Try

        'If UCase(Trim(strPOP_UP)) = "YES" Then
        '    Me.PageAnchor_Return_Link.Visible = False
        '    PageLinks = "<a class='a_return_menu' href='#' onclick='javascript:window.close();'>Click here to CLOSE PAGE...</a>"
        'Else
        '    Me.PageAnchor_Return_Link.Visible = True
        '    PageLinks = ""
        'End If

        'STRPAGE_TITLE = "Reinsurance Setup - " & strP_DESC
        STRPAGE_TITLE = "Reinsurance Setup"

        'If Trim(strP_TYPE) = "ERR_TYPE" Or Trim(strP_TYPE) = "" Then
        '    strP_TYPE = ""
        'End If

        strP_ID = "0"
        'Me.txtGroupNum.Text = RTrim(strP_TYPE)

        If Not (Page.IsPostBack) Then
            Call Proc_DataBind()
            Call DoNew()
            Me.txtAction.Text = ""
            Me.lblMessage.Text = "New Entry"
            txtCompShare.Text = 100
        End If

        If Me.txtAction.Text = "New" Then
            Call DoNew()
            Me.lblMessage.Text = "New Entry"
            Me.txtAction.Text = ""
        End If

        If Me.txtAction.Text = "Save" Then
        End If

        If Me.txtAction.Text = "Delete_Item" Then
            Call DoDelItem()
            Me.txtAction.Text = ""
        End If

    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Call DoSave()
        Me.txtAction.Text = ""
    End Sub
    Protected Sub DoNew(Optional ByVal pvOPT As String = "NEW")
        With Me
            .txtRecNo.Text = "0"
            .txtRecNo.Enabled = False
            .txtRetention.Text = ""
            .txtFreeMedCovLmt.Text = ""
            .txtCompShare.Text = ""
            .txtCommDate.Text = ""
            .cmdDelete_ASP.Enabled = False
            .lblMessage.Text = "Status: New Entry..."
            .txtCommDate.Enabled = True
        End With

    End Sub

    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TBIL_REINS_REC_ID, TBIL_REINS_RETENTION, TBIL_REINS_MED_COV_LMT, TBIL_REINS_COY_SHARE,"
        strSQL = strSQL & " TBIL_REINS_EFF_DATE FROM " & strTable & " "
        strSQL = strSQL & " WHERE TBIL_REINS_FLAG <> 'D'"
        'strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(Me.txtGroupNum.Text) & "'"
        'strSQL = strSQL & " ORDER BY TBIL_COD_TAB_ID, TBIL_COD_TYP, TBIL_COD_ITEM"

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

        If RTrim(Me.txtRetention.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblRetention.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.txtFreeMedCovLmt.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblFreeMedCovLmt.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.txtCompShare.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCompShare.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If RTrim(Me.txtCommDate.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCommDate.Text
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
        strSQL = strSQL & " WHERE TBIL_REINS_REC_ID = '" & RTrim(txtRecNo.Text) & "'"
        'strSQL = strSQL & " AND TBIL_COD_ITEM = '" & RTrim(txtNum.Text) & "'"
        'strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"


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

                drNewRow("TBIL_REINS_RETENTION") = txtRetention.Text
                drNewRow("TBIL_REINS_MED_COV_LMT") = txtFreeMedCovLmt.Text
                drNewRow("TBIL_REINS_COY_SHARE") = txtCompShare.Text
                drNewRow("TBIL_REINS_EFF_DATE") = Convert.ToDateTime(DoConvertToDbDateFormat(txtCommDate.Text))

                drNewRow("TBIL_REINS_FLAG") = "A"
                drNewRow("TBIL_REINS_OPERID") = CType(myUserIDX, String)
                drNewRow("TBIL_REINS_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMessage.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record

                

                With obj_DT
                    .Rows(0)("TBIL_REINS_RETENTION") = txtRetention.Text
                    .Rows(0)("TBIL_REINS_MED_COV_LMT") = txtFreeMedCovLmt.Text
                    .Rows(0)("TBIL_REINS_COY_SHARE") = txtCompShare.Text
                    .Rows(0)("TBIL_REINS_EFF_DATE") = Convert.ToDateTime(DoConvertToDbDateFormat(txtCommDate.Text))

                    .Rows(0)("TBIL_REINS_FLAG") = "C"
                    '.Rows(0)("TBIL_REINS_OPERID") = CType(myUserIDX, String)
                    '.Rows(0)("TBIL_REINS_KEYDTE") = Now
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMessage.Text = "Record Saved to Database Successfully."

            End If
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
        Me.cmdDelete_ASP.Enabled = True
        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        Call Proc_DataBind()
        Call DoNew()

        'Me.txtTransName.Enabled = True
        'Me.txtTransName.Focus()
    End Sub


    Protected Sub DoDelete()

        If Trim(Me.txtRecNo.Text) = "" Then
            Me.lblMessage.Text = "Missing number " & Me.lblRecNo.Text
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

        strREC_ID = Trim(Me.txtRecNo.Text)

        strSQL = "SELECT * " & strTable
        'strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
        strSQL = strSQL & " WHERE TBIL_REINS_REC_ID = '" & RTrim(strREC_ID) & "'"
        'strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

        strOPT = "NEW"
        FirstMsg = ""

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
       
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
                'Only update flag to 'D'
                strSQL = ""
                strSQL = "UPDATE " & strTable
                strSQL = strSQL & " SET TBIL_REINS_FLAG='D'"
                strSQL = strSQL & " WHERE TBIL_REINS_REC_ID = '" & RTrim(strREC_ID) & "'"
                'strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
                'strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

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

        Me.cmdDelete_ASP.Enabled = False

        If intC >= 1 Then
            Me.lblMessage.Text = "Record deleted successfully."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        Else
            Me.lblMessage.Text = "Sorry!. Record not deleted..."
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        End If

        Call DoNew()
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
                Me.txtRecNo.Text = Me.GridView1.Rows(P).Cells(2).Text


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
        Me.lblMessage.Text = "Record deleted successfully." & " No of item(s) deleted: " & CStr(C)
        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "');"
        'Me.textMessage.Text = ""

        Call DoNew("INIT")

        Me.lblMessage.Text = "Deleted Item(s): " & myKeyX

        'Me.txtTreatyNum.Enabled = True
        'Me.txtTreatyNum.Focus()

    End Sub

    Protected Sub DoDelete_Record()

        If Trim(Me.txtRetention.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblRetention.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtRecNo.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblRecNo.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtFreeMedCovLmt.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblFreeMedCovLmt.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtCompShare.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCompShare.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If Trim(Me.txtCommDate.Text) = "" Then
            Me.lblMessage.Text = "Missing " & Me.lblCommDate.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If


        Dim intC As Long = 0

        strREC_ID = Trim(Me.txtRecNo.Text)
        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_REINS_REC_ID = '" & RTrim(strREC_ID) & "'"
        'strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
        'strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

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
                strSQL = "UPDATE " & strTable
                strSQL = strSQL & " SET TBIL_REINS_FLAG = 'D'"
                strSQL = strSQL & " WHERE TBIL_REINS_REC_ID = '" & RTrim(strREC_ID) & "'"
                'strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
                'strSQL = strSQL & " AND TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"

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
        strSQL = strSQL & "SELECT *"
        strSQL = strSQL & " FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_REINS_REC_ID = '" & RTrim(strREC_ID) & "'"
        'strSQL = strSQL & " AND ILCODE.TBIL_COD_TYP = '" & RTrim(txtGroupNum.Text) & "'"
        'strSQL = strSQL & " AND ILCODE.TBIL_COD_TAB_ID = '" & RTrim(strP_ID) & "'"
        'strSQL = strSQL & " AND TBIL_COD_REC_ID = '" & Val(RTrim(txtRecNo.Text)) & "'"


        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Dim objOLEDR As OleDbDataReader


        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then

            Me.txtRetention.Text = Format(RTrim(CType(objOLEDR("TBIL_REINS_RETENTION") & vbNullString, String)), "Standard")
            Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_REINS_REC_ID") & vbNullString, String))
            Me.txtFreeMedCovLmt.Text = Format(RTrim(CType(objOLEDR("TBIL_REINS_MED_COV_LMT") & vbNullString, String)), "Standard")
            Me.txtCompShare.Text = RTrim(CType(objOLEDR("TBIL_REINS_COY_SHARE") & vbNullString, String))

            If Not IsDBNull(objOLEDR("TBIL_REINS_EFF_DATE")) Then _
                Me.txtCommDate.Text = Format(objOLEDR("TBIL_REINS_EFF_DATE"), "dd/MM/yyyy")
            ' Call DisableBox(Me.txtNum)
            strErrMsg = "Status: Data Modification"
            strOPT = "1"
            Me.cmdNew_ASP.Enabled = True
            Me.cmdDelete_ASP.Enabled = True

        Else
            'Me.txtNum.Text = ""
            Me.cmdDelete_ASP.Enabled = False
            strErrMsg = "Status: New Entry..."

            'Me.txtTransName.Enabled = True
            'Me.txtTransName.Focus()
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

        Me.txtRetention.Text = Format(row.Cells(3).Text, "Standard")
        Me.txtFreeMedCovLmt.Text = Format(row.Cells(4).Text, "Standard")
        Me.txtCompShare.Text = row.Cells(5).Text
        Me.txtCommDate.Text = row.Cells(6).Text
        txtCommDate.Enabled = False
        ' Call Proc_OpenRecord(Me.txtRecNo.Text)

        lblMessage.Text = "You selected " & Me.txtRecNo.Text & " / " & Me.txtCommDate.Text & "."

    End Sub

    Protected Sub txtRetention_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRetention.TextChanged
        If txtRetention.Text <> "" Then
            If IsNumeric(txtRetention.Text) Then
                txtRetention.Text = Format(txtRetention.Text, "Standard")
            Else
                Me.lblMessage.Text = "Retention must be numeric"
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                txtRetention.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Protected Sub txtFreeMedCovLmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFreeMedCovLmt.TextChanged
        If txtFreeMedCovLmt.Text <> "" Then
            If IsNumeric(txtFreeMedCovLmt.Text) Then
                txtFreeMedCovLmt.Text = Format(txtFreeMedCovLmt.Text, "Standard")
            Else
                Me.lblMessage.Text = "Free medical cover limit must be numeric"
                FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                txtFreeMedCovLmt.Focus()
                Exit Sub
            End If
        End If
    End Sub

    Protected Sub cmdDelItem_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelItem_ASP.Click
        'Call DoDelItem()
    End Sub

    Protected Sub cmdDelItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDelItem.Click

    End Sub
End Class
