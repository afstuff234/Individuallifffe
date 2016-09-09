Imports System.Data
Imports System.Data.OleDb

Partial Class I_LIFE_PRG_LI_REVIVE_POLICY
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
        '  strTableName = "TBIL_POLICY_DET"
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
                If Not IsDBNull(objOLEReader("TBIL_POLY_LAPSE_DT")) Then
                    txtPolLapseDate.Text = Format(objOLEReader("TBIL_POLY_LAPSE_DT"), "dd/MM/yyyy")
                End If
                If Not IsDBNull(objOLEReader("TBIL_POLY_STATUS")) Then
                    HidPolyStatus.Value = objOLEReader("TBIL_POLY_STATUS")
                    If HidPolyStatus.Value <> "L" And HidPolyStatus.Value <> "R" Then
                        lblMsg.Text = "Policy must be lapsed before reactivating"
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
                        lblMsg.Visible = True
                        'ErrorInd = "Y"
                        Exit Sub
                    End If
                    If HidPolyStatus.Value = "R" Then
                        If Not IsDBNull(objOLEReader("REACTIVATE_DT")) Then
                            txtPolReviveDate.Text = Format(objOLEReader("REACTIVATE_DT"), "dd/MM/yyyy")
                            txtPolReviveDate.Visible = True
                            lblReviveFormat.Visible = True
                            lblRevive.Visible = True
                        End If
                        chkRevivePolicy.Checked = True
                        lblMsg.Text = "Policy has already been reactivated"
                        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
                        lblMsg.Visible = True
                    End If
                End If
            Else
                lblMsg.Text = txtPolicyNumber.Text & " is not a valid policy number"
                lblMsg.Visible = True
                txtPolicyNumber.Text = ""
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
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
    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        initializeFields()
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtPolicyNumber.Text = ""
                Me.txtPolicyProCode.Text = ""
                Me.txtAssuredCode.Text = ""
                'Me.txtSearch.Value = ""
            Else
                txtPolicyNumber.Text = Me.cboSearch.SelectedItem.Value
                GetPolicyDetails(cboSearch.SelectedValue.Trim())
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub

    Protected Sub txtPolicyNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolicyNumber.TextChanged
        initializeFields()
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
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            ErrorInd = "Y"
            Exit Sub
        End If
        If (HidPolyStatus.Value = "R") Then
            lblMsg.Text = "Policy has already been reactivated"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtAssuredCode.Text = String.Empty) Then
            lblMsg.Text = "Please enter a assurance code"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyProCode.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy product code"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyStartDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy start date"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyEndDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy end date"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If


        If (Not IsDate(txtPolicyEndDate.Text)) Then
            lblMsg.Text = "Policy end date is not valid"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        Else
            PolicyEndDate = CDate(txtPolicyEndDate.Text)
        End If

        'If (PolicyEndDate < Now) Then
        '    lblMsg.Text = "Policy has already been completed"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If
        'If (chkRevivePolicy.Checked = False) Then
        '    lblMsg.Text = "Please confirm Paid UP"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If

        If (txtPolLapseDate.Text = String.Empty) Then
            lblMsg.Text = "Policy lapse date must not be empty, lapse has not been processed on the policy"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            lblMsg.Visible = True
            ErrorInd = "Y"
            'txtPaidUpEffectiveDate.Visible = True
            'lblPaidUpEffDate.Visible = True
            'lblPaidUpEffFormat.Visible = True
            Exit Sub
        End If

        Dim str() As String
        str = DoDate_Process(txtPolLapseDate.Text, txtPolLapseDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Paid UP effective date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtPolLapseDate.Focus()
            ErrorInd = "Y"
            Exit Sub
        Else
            txtPolLapseDate.Text = str(2).ToString()
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

        str = DoDate_Process(txtPolReviveDate.Text, txtPolReviveDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Policy revive date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            lblMsg.Visible = True
            txtPolReviveDate.Focus()
            ErrorInd = "Y"
            Exit Sub
        Else
            txtPolReviveDate.Text = str(2).ToString()
        End If


        Dim PolicyStartDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text))
        Dim PolEndDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text))
        Dim ReviveDate = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolReviveDate.Text))

        If ReviveDate < PolicyStartDate Or ReviveDate > PolEndDate Then
            lblMsg.Text = "Policy Revive Date must be within policy year"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        'If ReviveDate < PolEndDate Then
        '    lblMsg.Text = "Policy Revive Date must be greater than Policy End Date"
        '    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If

        'If (HidPolyStatus.Value = "") Then
        '    lblMsg.Text = "Policy status is empty or null"
        '    lblMsg.Visible = True
        '    ErrorInd = "Y"
        '    Exit Sub
        'End If
        If (HidPolyStatus.Value = "R") Then
            lblMsg.Text = "Policy has already been reactivated"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            chkRevivePolicy.Checked = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (HidPolyStatus.Value <> "L") Then
            lblMsg.Text = "Policy must be lapsed before reactivating"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
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
        txtPolLapseDate.Text = String.Empty
        txtPolReviveDate.Text = String.Empty
        chkRevivePolicy.Checked = False
        HidPolyStatus.Value = String.Empty
        txtPolReviveDate.Visible = False
        lblReviveFormat.Visible = False
        lblRevive.Visible = False
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
            objOLEConn = Nothing
            lblMsg.Visible = True
            Exit Sub
        End Try



        strTable = strTableName

        strSQL = ""
        strSQL = "SELECT TOP 1 * FROM TBIL_POLICY_DET"
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
                    .Rows(0)("TBIL_POLY_REACTIVATE_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(txtPolReviveDate.Text))
                    .Rows(0)("TBIL_POLY_STATUS") = "R"
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
    Protected Sub chkRevivePolicy_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRevivePolicy.CheckedChanged
        lblMsg.Text = ""
        If txtPolicyNumber.Text = "" Then
            lblMsg.Text = "Please enter a policy Number"
            lblMsg.Visible = True
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
            txtPolicyNumber.Focus()
            chkRevivePolicy.Checked = False
            Exit Sub
        Else
            If (HidPolyStatus.Value = "R") Then
                lblMsg.Text = "Policy has already been reactivated"
                lblMsg.Visible = True
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
                chkRevivePolicy.Checked = True
                ' ErrorInd = "Y"
                Exit Sub
            End If
            If HidPolyStatus.Value <> "L" Then
                lblMsg.Text = "Policy must be lapsed before reactivating"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
                lblMsg.Visible = True
                'ErrorInd = "Y"
                chkRevivePolicy.Checked = False
                Exit Sub
            End If
            If chkRevivePolicy.Checked Then
                txtPolReviveDate.Visible = True
                lblRevive.Visible = True
                lblReviveFormat.Visible = True
            Else
                txtPolReviveDate.Visible = False
                lblRevive.Visible = False
                lblReviveFormat.Visible = False
            End If
        End If
    End Sub
    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        Response.Redirect("PRG_LI_REVIVE_POLICY_RPT.aspx")
    End Sub
End Class
