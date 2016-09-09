Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class I_LIFE_PRG_PAIDUP_PROCESS
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Dim PolicyEndDate As DateTime
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
    Private Sub GetPolicyDetails(ByVal PolicyNo As String, ByRef ErrorInd As String)
        lblMsg.Text = ""
        lblMsg.Visible = False
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
            objOLEConn = Nothing
            lblMsg.Visible = True
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


                'If (Not IsDate(txtPolicyEndDate.Text)) Then
                '    lblMsg.Text = "Policy end date is not valid"
                '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                '    lblMsg.Visible = True
                '    ErrorInd = "Y"
                '    Exit Sub
                'Else
                '    PolicyEndDate = CDate(txtPolicyEndDate.Text)
                'End If
                ErrorInd = ""
                GetLastPaidDate(txtPolicyNumber.Text.Trim(), ErrorInd)
                If ErrorInd = "Y" Then Exit Sub
                PolicyEndDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text))
                If (PolicyEndDate < Now) Then
                    lblMsg.Text = "Policy has already been matured"
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    lblMsg.Visible = True
                    ErrorInd = "Y"
                    Exit Sub
                End If

                If Not IsDBNull(objOLEReader("TBIL_POLY_STATUS")) Then
                    HidPolyStatus.Value = objOLEReader("TBIL_POLY_STATUS")
                    If HidPolyStatus.Value = "P" Then
                        If Not IsDBNull(objOLEReader("PAIDUP_DT")) Then
                            txtPaidUpEffectiveDate.Text = Format(objOLEReader("PAIDUP_DT"), "dd/MM/yyyy")
                            txtPaidUpEffectiveDate.Visible = True
                            lblPaidUpEffDate.Visible = True
                            lblPaidUpEffFormat.Visible = True
                        End If
                        chkPaidUp.Checked = True
                        lblMsg.Text = "Paid UP has already been processed"
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                        lblMsg.Visible = True
                        ErrorInd = "Y"
                        Exit Sub
                    End If
                End If

            Else
                lblMsg.Text = txtPolicyNumber.Text & " is not a valid policy number"
                lblMsg.Visible = True
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
                txtPolicyNumber.Text = ""
                txtPolicyNumber.Focus()
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

    Private Sub GetLastPaidDate(ByVal PolicyNo As String, ByRef ErrorInd As String)
        lblMsg.Text = ""
        lblMsg.Visible = False
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
            objOLEConn = Nothing
            lblMsg.Visible = True
            Exit Sub
        End Try


        Try
            objOLEComm.Connection = objOLEConn
            objOLEComm.CommandText = "SPIL_GET_LAST_PREMIUM_PAID_DATE"
            objOLEComm.CommandType = CommandType.StoredProcedure
            objOLEComm.Parameters.AddWithValue("@PARAM_POL_NO", PolicyNo)
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            If objOLEReader.Read() = True Then
                If Not IsDBNull(objOLEReader("LAST_PREMIUM_PAID_DATE")) Then
                    txtPremPaidDate.Text = Format(objOLEReader("LAST_PREMIUM_PAID_DATE"), "dd/MM/yyyy")

                    'Dim Yeardiff = GetYearDiff(CDate(txtPremPaidDate.Text), CDate(txtPolicyStartDate.Text))
                    Dim daydiff = GetYearDiff(CDate(txtPremPaidDate.Text), CDate(txtPolicyStartDate.Text))

                    ' If Yeardiff <= 2 Then
                    '730 days approximately 2 yrs 
                    If daydiff <= 730 Then
                        lblMsg.Text = "Premium Paid Should be more than two (2) years before Paid Up Can be Processed"
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                        lblMsg.Visible = True
                        ErrorInd = "Y"
                        Exit Sub
                    End If
                End If

                If Not IsDBNull(objOLEReader("Message")) Then
                    lblMsg.Text = objOLEReader("Message")
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    lblMsg.Visible = True
                    ErrorInd = "Y"
                    Exit Sub
                End If
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
        initializeFields()
        ErrorInd = ""
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtPolicyNumber.Text = ""
                Me.txtPolicyProCode.Text = ""
                Me.txtAssuredCode.Text = ""
                'Me.txtSearch.Value = ""
            Else
                txtPolicyNumber.Text = Me.cboSearch.SelectedItem.Value
                GetPolicyDetails(cboSearch.SelectedValue.Trim(), ErrorInd)
                If ErrorInd = "Y" Then
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub
    Protected Sub txtPolicyNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicyNumber.TextChanged
        initializeFields()
        ErrorInd = ""
        If txtPolicyNumber.Text <> "" Then
            GetPolicyDetails(txtPolicyNumber.Text.Trim(), ErrorInd)
            If ErrorInd = "Y" Then
                Exit Sub
            End If
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
            lblMsg.Text = "policy start date must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyEndDate.Text = String.Empty) Then
            lblMsg.Text = "policy end date must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (txtPremPaidDate.Text = String.Empty) Then
            lblMsg.Text = "Last premium paid date must not be empty"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ErrorInd = "Y"
            Exit Sub
        End If

        'Dim Yeardiff As Integer = GetYearDiff(CDate(txtPremPaidDate.Text), CDate(txtPolicyStartDate.Text))
        Dim daydiff = GetYearDiff(CDate(txtPremPaidDate.Text), CDate(txtPolicyStartDate.Text))

        ' If Yeardiff <= 2 Then
        '730 days approximately 2 yrs 
        If daydiff <= 730 Then
            lblMsg.Text = "Premium Paid Should be more than two (2) years before Paid Up Can be Processed"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            ErrorInd = "Y"
            lblMsg.Visible = True
            Exit Sub
        End If

        If (chkPaidUp.Checked = False) Then
            lblMsg.Text = "Please confirm Paid UP"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (txtPaidUpEffectiveDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter Paid UP effective date"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            'txtPaidUpEffectiveDate.Visible = True
            'lblPaidUpEffDate.Visible = True
            'lblPaidUpEffFormat.Visible = True
            Exit Sub
        End If

        Dim str() As String
        str = DoDate_Process(txtPaidUpEffectiveDate.Text, txtPaidUpEffectiveDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Paid UP effective date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtPaidUpEffectiveDate.Focus()
            ErrorInd = "Y"
            Exit Sub
        Else
            txtPaidUpEffectiveDate.Text = str(2).ToString()
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

        str = DoDate_Process(txtPolicyEndDate.Text, txtPolicyEndDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Policy end date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtPolicyEndDate.Focus()
            ErrorInd = "Y"
            Exit Sub
        Else
            txtPolicyEndDate.Text = str(2).ToString()
        End If



        Dim PolicyStartDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text))
        PolicyEndDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text))
        Dim PaidUpDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPaidUpEffectiveDate.Text))

        If (PolicyEndDate < Now) Then
            lblMsg.Text = "Policy has already been matured"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If PaidUpDate < PolicyStartDate Or PaidUpDate > PolicyEndDate Then
            lblMsg.Text = "Paid UP effective date must be within policy year"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (HidPolyStatus.Value = "") Then
            lblMsg.Text = "Policy status is empty or null"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (HidPolyStatus.Value = "W") Then
            lblMsg.Text = "Policy has been waived"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (HidPolyStatus.Value = "P") Then
            lblMsg.Text = "Paid UP has already been processed"
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
    Private Sub initializeFields()
        'txtPolicyNumber.Text = String.Empty
        txtAssuredCode.Text = String.Empty
        txtAssuredName.Text = String.Empty
        txtPolicyProCode.Text = String.Empty
        txtProdDesc.Text = String.Empty
        txtPolicyStartDate.Text = String.Empty
        txtPolicyEndDate.Text = String.Empty
        txtPaidUpEffectiveDate.Text = String.Empty
        chkPaidUp.Checked = False
        HidPolyStatus.Value = String.Empty
        txtPaidUpEffectiveDate.Visible = False
        lblPaidUpEffDate.Visible = False
        lblPaidUpEffFormat.Visible = False
        txtPremPaidDate.Text = String.Empty
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
            'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
            lblMsg.Visible = True
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
                    .Rows(0)("TBIL_POLY_PAIDUP_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(txtPaidUpEffectiveDate.Text))
                    .Rows(0)("TBIL_POLY_STATUS") = "P"
                    .Rows(0)("TBIL_POLY_KEYDTE") = Now
                    .Rows(0)("TBIL_POLY_FLAG") = "A"
                    .Rows(0)("TBIL_POLY_OPERID") = myUserIDX
                End With

                'obj_DT.AcceptChanges()
                intC = objDA.Update(obj_DT)

                Me.lblMsg.Text = "Record Saved to Database Successfully."
                lblMsg.Visible = True
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

    Protected Sub chkPaidUp_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPaidUp.CheckedChanged
        lblMsg.Text = ""
        If txtPolicyNumber.Text = "" Then
            lblMsg.Text = "Please enter a policy Number"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            txtPolicyNumber.Focus()
            chkPaidUp.Checked = False
            Exit Sub
        End If

        If (HidPolyStatus.Value = "P") Then
            lblMsg.Text = "Paid UP has already been processed"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            chkPaidUp.Checked = True
            '  ErrorInd = "Y"
            Exit Sub
        End If

        'If (Not IsDate(txtPolicyEndDate.Text)) Then
        '    lblMsg.Text = "Policy end date is not valid"
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
        '    lblMsg.Visible = True
        '    '  ErrorInd = "Y"
        '    Exit Sub
        'Else
        '    PolicyEndDate = CDate(txtPolicyEndDate.Text)
        'End If

        If PolicyEndDate <> Nothing Then
            If (PolicyEndDate < Now) Then
                lblMsg.Text = "Policy has already been matured"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                lblMsg.Visible = True
                chkPaidUp.Checked = False
                ' ErrorInd = "Y"
                Exit Sub
            End If
        End If

        If (txtPremPaidDate.Text = "") Then
            lblMsg.Text = "Last premium paid date must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            chkPaidUp.Checked = False
            ' ErrorInd = "Y"
            Exit Sub
        End If

        ' Dim Yeardiff As Integer = GetYearDiff(CDate(txtPremPaidDate.Text), CDate(txtPolicyStartDate.Text))
        Dim daydiff = GetYearDiff(CDate(txtPremPaidDate.Text), CDate(txtPolicyStartDate.Text))

        ' If Yeardiff <= 2 Then
        '730 days approximately 2 yrs 
        If daydiff <= 730 Then
            lblMsg.Text = "Premium Paid Should be more than two (2) years before Paid Up Can be Processed"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            chkPaidUp.Checked = False
            lblMsg.Visible = True
            Exit Sub
        End If

        If chkPaidUp.Checked Then
            txtPaidUpEffectiveDate.Visible = True
            lblPaidUpEffDate.Visible = True
            lblPaidUpEffFormat.Visible = True
        Else
            txtPaidUpEffectiveDate.Visible = False
            lblPaidUpEffDate.Visible = False
            lblPaidUpEffFormat.Visible = False
        End If
    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        Response.Redirect("PRG_PAIDUP_PROCESS_RPT.aspx")
    End Sub

    Public Function GetYearDiff(ByVal PremiumPaidDate As Date, ByVal StartDate As Date) As Integer
        Dim Yeardiff As Integer = DateDiff("d", StartDate, PremiumPaidDate)
        Return Yeardiff
    End Function
End Class
