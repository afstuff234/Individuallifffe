Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class I_LIFE_PRG_LI_CANCEL_PROCESS
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Dim waiverRep As PRG_LI_CLM_WAIVER_REPOSITORY = New PRG_LI_CLM_WAIVER_REPOSITORY()
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Protected strStatus As String
    Dim strREC_ID As String
    Dim strTable As String
    Dim strSQL As String
    Protected strTableName As String
    Dim strErrMsg As String
    Protected blnStatusX As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "TBIL_POLICY_DET"
    End Sub


    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            cboSearch.Items.Clear()
            cboSearch.Items.Add("* Select Insured *")
            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()
        End If
    End Sub
    Private Sub GetPolicyDetails(ByVal PolicyNo As String)
        lblMsg.Text = ""
        ' lblMsg.Visible = False
        initializeFields()
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN
        Dim objOLEComm As OleDbCommand = New OleDbCommand()

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Sub
        End Try


        Try
            objOLEComm.Connection = objOLEConn
            objOLEComm.CommandText = "SPIL_GET_POLICY_INFO"
            objOLEComm.CommandType = CommandType.StoredProcedure
            objOLEComm.Parameters.AddWithValue("@PARAM_POL_NO", PolicyNo)
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()

            If objOLEReader.HasRows = True Then
                objOLEReader.Read()
                txtAssuredCode.Text = objOLEReader("TBIL_POLY_ASSRD_CD")
                txtAssuredName.Text = objOLEReader("TBIL_INSRD_SURNAME") & "" & objOLEReader("TBIL_INSRD_FIRSTNAME")
                txtPolicyProCode.Text = objOLEReader("TBIL_POL_PRM_PRDCT_CD")
                txtProdDesc.Text = objOLEReader("TBIL_PRDCT_DTL_DESC")
                txtPolicyStartDate.Text = Format(objOLEReader("TBIL_POL_PRM_FROM"), "dd/MM/yyyy")
                txtPolicyEndDate.Text = Format(objOLEReader("TBIL_POL_PRM_TO"), "dd/MM/yyyy")
                If Not IsDBNull(objOLEReader("TBIL_POLY_AGCY_CODE")) Then txtAgentCode.Text = objOLEReader("TBIL_POLY_AGCY_CODE")
                If Not IsDBNull(objOLEReader("TBIL_AGCY_AGENT_NAME")) Then txtAgentName.Text = objOLEReader("TBIL_AGCY_AGENT_NAME")

                GetTotalPremium()
                GetAgencyComm()
                If Not IsDBNull(objOLEReader("TBIL_POLY_STATUS")) Then
                    HidPolyStatus.Value = objOLEReader("TBIL_POLY_STATUS")

                    If HidPolyStatus.Value = "C" Then
                        If Not IsDBNull(objOLEReader("CANCEL_DT")) Then
                            txtCancelDate.Text = Format(objOLEReader("CANCEL_DT"), "dd/MM/yyyy")
                            txtCancelDate.Visible = True
                            lblPaidUpEffDate.Visible = True
                            lblPaidUpEffFormat.Visible = True
                        End If
                        chkCancelPolicy.Checked = True
                        lblMsg.Text = "Policy cancellation has already been processed"
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                        'ErrorInd = "Y"
                        lblMsg.Visible = True
                        Exit Sub
                    End If
                End If

                Dim daydiff = GetDayDiff(CDate(txtPolicyStartDate.Text), Now)
                If daydiff > 31 Then
                    lblMsg.Text = "Policy can only be cancelled if not more than a month "
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    lblMsg.Visible = True
                    ' ErrorInd = "Y"
                    Exit Sub
                End If
            Else
                lblMsg.Text = txtPolicyNumber.Text & " is not a valid policy number"
                lblMsg.Visible = True
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
                txtPolicyNumber.Text = ""
                txtPolicyNumber.Focus()
                'ErrorInd = "Y"
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
            Exit Sub
        End Try

        If objOLEComm.Connection.State = ConnectionState.Open Then
            objOLEComm.Connection.Close()
        End If
        '   objOLEComm.Dispose()
        objOLEComm = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing
    End Sub
    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        ' initializeFields()
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtPolicyNumber.Text = ""
                Me.txtPolicyProCode.Text = ""
                Me.txtAssuredCode.Text = ""
                'Me.txtSearch.Value = ""
            Else
                txtPolicyNumber.Text = Me.cboSearch.SelectedItem.Value
                ErrorInd = ""
                GetPolicyDetails(cboSearch.SelectedValue.Trim())
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub
    Protected Sub txtPolicyNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicyNumber.TextChanged
        'initializeFields()
        If txtPolicyNumber.Text <> "" Then
            GetPolicyDetails(txtPolicyNumber.Text.Trim())
        End If
    End Sub

    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        lblMsg.Visible = False
        ErrorInd = ""
        ValidateControls(ErrorInd)
        If ErrorInd = "Y" Then
            Exit Sub
        End If
        Proc_DoSave()
        initializeFields()
    End Sub
    Private Sub ValidateControls(ByRef ErrorInd As String)
        If (txtPolicyNumber.Text = String.Empty) Then
            lblMsg.Text = "Please enter a policy number"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtAssuredCode.Text = String.Empty) Then
            lblMsg.Text = "Please enter a assurance code"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyProCode.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy product code"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyStartDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy start date"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        Dim monthValue As Integer = GetDayDiff(CDate(txtPolicyStartDate.Text), Now)
        If monthValue > 31 Then
            lblMsg.Text = "Policy can only be cancelled if not more than a month"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (chkCancelPolicy.Checked = False) Then
            lblMsg.Text = "Please confirm policy cancellation process"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (txtCancelDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter Paid UP effective date"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        Dim str() As String
        str = DoDate_Process(txtCancelDate.Text, txtCancelDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " policy cancellation effective date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtCancelDate.Focus()
            ErrorInd = "Y"
            Exit Sub
        Else
            txtCancelDate.Text = str(2).ToString()
        End If


        str = DoDate_Process(txtPolicyStartDate.Text, txtPolicyStartDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Policy start date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtPolicyStartDate.Focus()
            ErrorInd = "Y"
            Exit Sub
        Else
            txtPolicyStartDate.Text = str(2).ToString()
        End If


        Dim PolicyStartDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text))
        Dim PolicyEndDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text))
        Dim CancelDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtCancelDate.Text))

        ' If CancelDate < PolicyStartDate Then
        If CancelDate < PolicyStartDate Or CancelDate > PolicyEndDate Then
            ' lblMsg.Text = "Policy cancellation effective date must not be earlier than policy start date"
            lblMsg.Text = "Policy cancellation effective date must be within policy year"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If


        monthValue = GetDayDiff(PolicyStartDate, CancelDate)
        If monthValue > 31 Then
            lblMsg.Text = "Cancellation date must not be more than one (1) month after policy start date"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPremiumPaid.Text = "") Then
            lblMsg.Text = "Premium paid must not empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        'If (txtAgentCode.Text = "") Then
        '    lblMsg.Text = "Agent code must not be empty"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If

        'If (txtAgentName.Text = "") Then
        '    lblMsg.Text = "Agent Name must not be empty"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If

        'If (txtBasicCommPaid.Text = "") Then
        '    lblMsg.Text = "Basic Commission paid must not be empty"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If

        'If (txtOverCommPaid.Text = "") Then
        '    lblMsg.Text = Overriding Commission paid must not be emptyl"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If

        If (HidPolyStatus.Value = "") Then
            lblMsg.Text = "Policy status is empty or null"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (HidPolyStatus.Value = "C") Then
            lblMsg.Text = "Policy cancellation has already been processed"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (HidPolyStatus.Value <> "A") Then
            lblMsg.Text = "Policy status must be Active"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
    End Sub

    Private Sub GetTotalPremium()
        Dim sum As Double = 0.0
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Sub
        End Try

        strSQL = "SELECT  * FROM TBFN_ALLOC_DETAIL"
        strSQL = strSQL & " WHERE TBFN_TRANS_POLY_NO = '" & RTrim(txtPolicyNumber.Text) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        Dim intC As Integer = 0

        Try

            objDA.Fill(obj_DT)
            Dim dr As System.Data.DataRow
            If obj_DT.Rows.Count > 0 Then
                For i = 0 To obj_DT.Rows.Count - 1
                    dr = obj_DT.Rows(i)
                    If Not IsDBNull(dr("TBFN_TRANS_TOT_AMT")) Then
                        sum = sum + dr("TBFN_TRANS_TOT_AMT")
                    End If
                Next
            End If

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
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
        txtPremiumPaid.Text = Format(sum, "Standard")
    End Sub

    Private Sub GetAgencyComm()
        Dim BasicCommTotal As Double = 0.0
        Dim OverridingCommTotal As Double = 0.0
        Dim SumAgcyDtl_Add1 As Double = 0.0
        Dim SumAgcyDtl_Add2 As Double = 0.0
        Dim SumAgcyDtl_Add3 As Double = 0.0
        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection()
        objOLEConn.ConnectionString = mystrCONN

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
            objOLEConn = Nothing
            Exit Sub
        End Try

        strSQL = "SELECT  * FROM TBFN_AGENT_COMM_DETAIL"
        strSQL = strSQL & " WHERE TBFN_COMM_TRANS_POLY_NO = '" & RTrim(txtPolicyNumber.Text) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        Dim intC As Integer = 0

        Try

            objDA.Fill(obj_DT)
            Dim dr As System.Data.DataRow
            If obj_DT.Rows.Count > 0 Then
                For i = 0 To obj_DT.Rows.Count - 1
                    dr = obj_DT.Rows(i)
                    If Not IsDBNull(dr("TBFN_COMM_TRANS_COMM_AMT")) Then
                        BasicCommTotal = BasicCommTotal + dr("TBFN_COMM_TRANS_COMM_AMT")
                        'SumAgcyDtl_Add1 = SumAgcyDtl_Add1 + dr("TBIL_AGCY_DTL_ADD_1")
                        'SumAgcyDtl_Add2 = SumAgcyDtl_Add1 + dr("TBIL_AGCY_DTL_ADD_2")
                        'SumAgcyDtl_Add3 = SumAgcyDtl_Add1 + dr("TBIL_AGCY_DTL_ADD_3")
                    End If
                Next
            End If
            ' OverridingCommTotal = SumAgcyDtl_Add1 + SumAgcyDtl_Add2 + SumAgcyDtl_Add3

        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
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
        txtBasicCommPaid.Text = Format(BasicCommTotal, "Standard")
        '  txtOverCommPaid.Text = Format(OverridingCommTotal, "Standard")
    End Sub

    Private Sub Proc_DoSave()
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
            lblMsg.Visible = True
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            objOLEConn = Nothing
            Exit Sub
        End Try



        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM " & strTable
        strSQL = strSQL & " WHERE TBIL_POLY_POLICY_NO = '" & RTrim(txtPolicyNumber.Text) & "'"

        Dim objDA As System.Data.OleDb.OleDbDataAdapter
        objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)
        Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
        m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

        Dim obj_DT As New System.Data.DataTable
        Dim intC As Integer = 0

        Try

            objDA.Fill(obj_DT)

            If obj_DT.Rows.Count = 0 Then
                '   Creating a new record
                Dim drNewRow As System.Data.DataRow
                drNewRow = obj_DT.NewRow()
                drNewRow = Nothing
                Me.lblMsg.Text = "New Record Saved to Database Successfully."
            Else
                '   Update existing record
                With obj_DT
                    .Rows(0)("TBIL_POLY_CANCEL_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(txtCancelDate.Text))
                    .Rows(0)("TBIL_POLY_STATUS") = "C"
                    .Rows(0)("TBIL_POLY_KEYDTE") = Now
                    .Rows(0)("TBIL_POLY_FLAG") = "A"
                    .Rows(0)("TBIL_POLY_OPERID") = myUserIDX
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMsg.Text = "Record Saved to Database Successfully."

            End If


        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
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
        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        txtPolicyNumber.Text = ""
        lblMsg.Visible = True
    End Sub

    Private Sub initializeFields()
        'txtPolicyNumber.Text = String.Empty
        txtAssuredCode.Text = String.Empty
        txtAssuredName.Text = String.Empty
        txtPolicyProCode.Text = String.Empty
        txtProdDesc.Text = String.Empty
        txtPolicyStartDate.Text = String.Empty
        txtPolicyEndDate.Text = String.Empty
        txtCancelDate.Text = String.Empty
        chkCancelPolicy.Checked = False
        HidPolyStatus.Value = String.Empty
        txtCancelDate.Visible = False
        lblPaidUpEffDate.Visible = False
        lblPaidUpEffFormat.Visible = False
        txtPremiumPaid.Text = String.Empty
        txtAgentCode.Text = String.Empty
        txtAgentName.Text = String.Empty
        txtBasicCommPaid.Text = String.Empty
        'txtOverCommPaid.Text = String.Empty
        txtCancelDate.Visible = False
        lblPaidUpEffDate.Visible = False
        lblPaidUpEffFormat.Visible = False
    End Sub

    Protected Sub chkCancelPolicy_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCancelPolicy.CheckedChanged
        lblMsg.Text = ""
        If txtPolicyNumber.Text = "" Then
            lblMsg.Text = "Please enter a policy Number"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            txtPolicyNumber.Focus()
            chkCancelPolicy.Checked = False
            Exit Sub
        Else
            If (HidPolyStatus.Value = "C") Then
                lblMsg.Text = "Policy cancellation has already been processed"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                lblMsg.Visible = True
                chkCancelPolicy.Checked = True
                'ErrorInd = "Y"
                Exit Sub
            End If

            Dim daydiff = GetDayDiff(CDate(txtPolicyStartDate.Text), Now)
            If daydiff > 31 Then
                lblMsg.Text = "Policy can only be cancelled if not more than a month"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                lblMsg.Visible = True
                ' ErrorInd = "Y"
                chkCancelPolicy.Checked = False
                Exit Sub
            End If
            If chkCancelPolicy.Checked Then
                txtCancelDate.Visible = True
                lblPaidUpEffDate.Visible = True
                lblPaidUpEffFormat.Visible = True
            Else
                txtCancelDate.Visible = False
                lblPaidUpEffDate.Visible = False
                lblPaidUpEffFormat.Visible = False
            End If
        End If
    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        Response.Redirect("PRG_LI_CANCEL_PROCESS_RPT.aspx")
    End Sub

    Private Function GetDayDiff(ByVal PolicyStartDt As DateTime, ByVal higherDate As DateTime) As Integer
        Dim DayValue As Integer = DateDiff("d", PolicyStartDt, higherDate)
        Return DayValue
    End Function

End Class
