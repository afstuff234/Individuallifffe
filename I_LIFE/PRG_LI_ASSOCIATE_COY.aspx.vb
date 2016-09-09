
Imports System.Data
Imports System.Data.OleDb

Partial Class I_LIFE_PRG_LI_ASSOCIATE_COY
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

        PageLinks = ""
        'PageLinks = PageLinks & "<a href='javascript:window.close();' runat='server'>Close...</a>"
        PageLinks = "<a href='../I_LIFE/MENU_IL.aspx?menu=IL_REINS' class='a_sub_menu' style='float:right;'>Return to Menu</a>&nbsp;<br/>"

        strTableName = "TBGL_REINSURANCE"
    End Sub

    Private Sub DoSave()
        lblMessage.Text = ""
        Dim strMyVal As String

        If LTrim(RTrim(Me.txtTBGL_CODE.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCECode Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        'If LTrim(RTrim(Me.txtTBGL_CATG.Text)) = "" Then
        '    Me.lblMessage.Text = "Incorrect/Invalid/Empty Category Field!"
        '    FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '    Exit Sub
        'End If

        If LTrim(RTrim(Me.txtTBGL_DESC.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCE Description Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBGL_SHRT_DESC.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCE Short Description Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        'If LTrim(RTrim(Me.txtTBGL_BRANCH.Text)) = "" Then
        '    Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCE Branch Field!"
        '    FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        '    Exit Sub
        'End If

        If LTrim(RTrim(Me.txtTBGL_ADRES1.Text)) = "" Then
            Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCE Address 1 Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If LTrim(RTrim(Me.txtTBGL_PHONE1.Text)) = "" Then
            'Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCE Phone 1 Field!"
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
            txtTBGL_PHONE1.Text = "N/A"
        End If

        If LTrim(RTrim(Me.txtTBGL_PHONE2.Text)) = "" Then
            'Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCE Phone 1 Field!"
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub
            txtTBGL_PHONE2.Text = "N/A"
        End If

        If LTrim(RTrim(Me.txtTBGL_EMAIL1.Text)) = "" Then
            'Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCE Email 1 Field!"
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub

            txtTBGL_EMAIL1.Text = "N/A"
        End If

        If LTrim(RTrim(Me.txtTBGL_EMAIL2.Text)) = "" Then
            'Me.lblMessage.Text = "Incorrect/Invalid/REINSURANCE Email 1 Field!"
            'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            'Exit Sub

            txtTBGL_EMAIL2.Text = "N/A"
        End If


        If ddnCompanyType.SelectedIndex = 0 Then
            Me.lblMessage.Text = "Incorrect/Invalid/Company Type Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        End If

        If ddnReinsuranceMdl.SelectedIndex = 0 Then
            Me.lblMessage.Text = "Incorrect/Invalid/Reinsutance Module Field!"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
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

        strTable = "TBGL_REINSURANCE"

        strSQL = ""
        strSQL = "SELECT TOP 1 TBGL_CODE FROM " & strTable
        strSQL = strSQL & " WHERE TBGL_DESC = '" & RTrim(Me.txtTBGL_DESC.Text) & "'"
        'strSQL = strSQL & " AND TBGL_ID = '" & RTrim(Me.txtCustID.Text) & "'"

        Dim chk_objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        chk_objOLECmd.CommandType = CommandType.Text
        'chk_objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID
        Dim chk_objOLEDR As OleDbDataReader

        chk_objOLEDR = chk_objOLECmd.ExecuteReader()
        If (chk_objOLEDR.Read()) Then
            If Trim(Me.txtTBGL_CODE.Text) <> Trim(chk_objOLEDR("TBGL_CODE") & vbNullString) Then
                Me.lblMessage.Text = "Warning!. The code description you enter already exist..." & _
                  "<br />Please check code: " & RTrim(chk_objOLEDR("TBGL_CODE") & vbNullString)
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


        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBGL_DESC = '" & RTrim(txtTBGL_DESC.Text) & "'"
        'strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(Me.txtCustID.Text) & "'"

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

                'drNewRow("TBIL_CUST_ID") = RTrim(Me.txtCustID.Text)
                drNewRow("TBGL_CODE") = RTrim(Me.txtTBGL_CODE.Text)

                'drNewRow("TBGL_MDLE") = RTrim(Me.txtt.Text)
                'drNewRow("TBGL_CATG") = RTrim(Me.txtTBGL_CATG.Text)

                drNewRow("TBGL_DESC") = Me.txtTBGL_DESC.Text.Trim()
                drNewRow("TBGL_SHRT_DESC") = Me.txtTBGL_SHRT_DESC.Text.Trim()

                drNewRow("TBGL_ADRES1") = Me.txtTBGL_ADRES1.Text.Trim()
                drNewRow("TBGL_ADRES2") = Me.txtTBGL_ADRES2.Text.Trim()
                'drNewRow("TBGL_BRANCH") = Me.txtTBGL_BRANCH.Text.Trim()
                drNewRow("TBGL_COMPANY_TYPE") = ddnCompanyType.SelectedValue
                drNewRow("TBGL_MODULE") = ddnReinsuranceMdl.SelectedValue
                drNewRow("TBGL_PHONE1") = Me.txtTBGL_PHONE1.Text.Trim()
                drNewRow("TBGL_PHONE2") = Me.txtTBGL_PHONE2.Text.Trim()
                drNewRow("TBGL_EMAIL1") = Me.txtTBGL_EMAIL1.Text.Trim()
                drNewRow("TBGL_EMAIL2") = Me.txtTBGL_EMAIL2.Text.Trim()
                drNewRow("TBGL_PERSON_RESP") = Me.txtTBGL_PERSON_RESP.Text.Trim()

                drNewRow("TBGL_FLAG") = "A"
                drNewRow("TBGL_OPERID") = CType(myUserIDX, String)
                drNewRow("TBGL_KEYDTE") = Now

                obj_DT.Rows.Add(drNewRow)
                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                drNewRow = Nothing

                Me.lblMessage.Text = "New Record Saved to Database Successfully."

            Else
                '   Update existing record

                With obj_DT
                    .Rows(0)("TBGL_CODE") = RTrim(Me.txtTBGL_CODE.Text)

                    'drNewRow("TBGL_MDLE") = RTrim(Me.txtt.Text)
                    '.Rows(0)("TBGL_CATG") = RTrim(Me.txtTBGL_CATG.Text)

                    .Rows(0)("TBGL_DESC") = Me.txtTBGL_DESC.Text.Trim()
                    .Rows(0)("TBGL_SHRT_DESC") = Me.txtTBGL_SHRT_DESC.Text.Trim()
                    '.Rows(0)("TBGL_BRANCH") = Me.txtTBGL_BRANCH.Text.Trim()
                    .Rows(0)("TBGL_COMPANY_TYPE") = ddnCompanyType.SelectedValue
                    .Rows(0)("TBGL_MODULE") = ddnReinsuranceMdl.SelectedValue
                    .Rows(0)("TBGL_ADRES1") = Me.txtTBGL_ADRES1.Text.Trim()
                    .Rows(0)("TBGL_ADRES2") = Me.txtTBGL_ADRES2.Text.Trim()
                    .Rows(0)("TBGL_PHONE1") = Me.txtTBGL_PHONE1.Text.Trim()
                    .Rows(0)("TBGL_PHONE2") = Me.txtTBGL_PHONE2.Text.Trim()
                    .Rows(0)("TBGL_EMAIL1") = Me.txtTBGL_EMAIL1.Text.Trim()
                    .Rows(0)("TBGL_EMAIL2") = Me.txtTBGL_EMAIL2.Text.Trim()
                    .Rows(0)("TBGL_PERSON_RESP") = Me.txtTBGL_PERSON_RESP.Text.Trim()

                    .Rows(0)("TBGL_FLAG") = "C"
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

        objDA.Dispose()
        objDA = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing


        FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
        'Me.lblMessage.Text = ""

        Me.txtSearch.Value = RTrim(Me.txtTBGL_DESC.Text)

        'Call Proc_Populate_Box("IL_BRK_DETAIL_LIST", Trim(Me.txtCustID.Text), Me.cboTransList)
        Call Proc_DataBind()
        Me.txtSearch.Value = ""

        'DoNew()

        Me.txtTBGL_DESC.Enabled = True
        Me.txtTBGL_DESC.Focus()

    End Sub

    Private Sub Proc_DataBind()
        'Me.cmdDelItem.Enabled = True

        'Try
        '    Me.txtCustModule.Text = cboCustModule.SelectedValue
        'Catch ex As Exception
        'End Try

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TBGL_REC_ID, TBGL_CODE, RTRIM(ISNULL(TBGL_DESC,'')) AS TBGL_FULL_NAME, " & _
                        " RTRIM(ISNULL(TBGL_PHONE1,'')) + ' ' + RTRIM(ISNULL(TBGL_PHONE2,'')) AS TBGL_PHONE_NUM " & _
                         " FROM " & strTable & " where TBGL_DESC = '" & RTrim(Me.txtTBGL_DESC.Text) & "'" & _
                         " ORDER BY TBGL_DESC, RTRIM(ISNULL(TBGL_DESC,''))"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        objOLEConn.Open()
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

    Function ValidateTextEntries() As Boolean

        Return False
    End Function

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        Call DoSave()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtPfaID.Text = row.Cells(2).Text

        Me.txtTBGL_DESC.Text = row.Cells(4).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
        Dim sPhone As String() = (row.Cells(5).Text).Split(" ")
        Me.txtTBGL_PHONE1.Text = sPhone(0)
        Me.txtTBGL_PHONE2.Text = sPhone(1)
        'Call Proc_DDL_Get(Me.cboTransList, RTrim(Me.txtCustNum.Text))

        'Call Proc_OpenRecord(Me.txtCustNum.Text)

        lblMessage.Text = "You selected " & Me.txtTBGL_DESC.Text & " / " & Me.txtPfaID.Text & "."

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSearch.Click
        'search for registered pfa under ANNUITY
        If txtSearch.Value <> "" Then
            Dim sText As String = txtSearch.Value
            Proc_SearchPfa(sText)
        Else
            Exit Sub
        End If


    End Sub

    Private Sub Proc_SearchPfa(ByVal sVal As String)

        strTable = "TBGL_REINSURANCE"
        strSQL = "SELECT *, RTRIM(ISNULL(TBGL_DESC,'')) AS TBGL_FULL_NAME " _
               + " FROM " & strTable & " where TBGL_DESC like '" & sVal.Trim & "%' or (TBGL_CODE like '" & sVal.Trim & "%') or (TBGL_SHRT_DESC like '" & sVal.Trim & "%') ORDER BY TBGL_DESC"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = objOLEConn
        cmd.CommandText = strSQL
        cmd.CommandType = CommandType.Text

        Try
            objOLEConn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            objOLEConn.Close()

            Dim dt As DataTable = ds.Tables(0)
            Dim dr As DataRow = dt.NewRow()
            dr("TBGL_FULL_NAME") = "-- Selecct --"
            dr("TBGL_CODE") = ""
            dt.Rows.InsertAt(dr, 0)

            cbo_PfaName.DataSource = dt
            cbo_PfaName.DataTextField = "TBGL_FULL_NAME"
            cbo_PfaName.DataValueField = "TBGL_CODE"
            cbo_PfaName.DataBind()

        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()

        End Try

    End Sub

    Protected Sub cbo_PfaName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbo_PfaName.SelectedIndexChanged
        If cbo_PfaName.SelectedIndex <> 0 Then
            Proc_GetPfa(cbo_PfaName.SelectedValue)
            'txtPfa_Search.Text = ""
            cbo_PfaName.SelectedIndex = 0
        End If
    End Sub

    Private Sub Proc_GetPfa(ByVal sVal As String)

        strTable = "TBGL_REINSURANCE"
        strSQL = "SELECT *, RTRIM(ISNULL(TBGL_DESC,'')) AS TBGL_FULL_NAME FROM " & strTable & " where TBGL_CODE = '" & sVal.Trim & "'"

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = objOLEConn
        cmd.CommandText = strSQL
        cmd.CommandType = CommandType.Text

        Try
            objOLEConn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            objOLEConn.Close()

            Dim dt As DataTable = ds.Tables(0)
            For Each dr As DataRow In dt.Rows
                txtPfaID.Text = dr("TBGL_REC_ID").ToString()
                txtTBGL_CODE.Text = dr("TBGL_CODE").ToString()
                txtTBGL_DESC.Text = dr("TBGL_DESC").ToString()
                txtTBGL_SHRT_DESC.Text = dr("TBGL_SHRT_DESC").ToString()
                'txtTBGL_BRANCH.Text = dr("TBGL_BRANCH").ToString()
                ddnCompanyType.SelectedValue = dr("TBGL_COMPANY_TYPE").ToString()
                ddnReinsuranceMdl.SelectedValue = dr("TBGL_MODULE").ToString()
                txtTBGL_ADRES1.Text = dr("TBGL_ADRES1").ToString()
                txtTBGL_ADRES2.Text = dr("TBGL_ADRES2").ToString()
                txtTBGL_PHONE1.Text = dr("TBGL_PHONE1").ToString()
                txtTBGL_PHONE2.Text = dr("TBGL_PHONE2").ToString()
                txtTBGL_EMAIL1.Text = dr("TBGL_EMAIL1").ToString()
                txtTBGL_EMAIL2.Text = dr("TBGL_EMAIL2").ToString()
                txtTBGL_PERSON_RESP.Text = dr("TBGL_PERSON_RESP").ToString()
            Next


        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()

        End Try

    End Sub


    Protected Sub PageAnchor_Return_Link_ServerClick(ByVal sender As Object, ByVal e As EventArgs) Handles PageAnchor_Return_Link.ServerClick
        'me.dispose()
        Dim closeScript As String = "<script language='javascript'> window.close() </script>"
        lblMessage.Text = closeScript

    End Sub

End Class
