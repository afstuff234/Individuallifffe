Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class PRG_LI_CLM_WAIVER
    Inherits System.Web.UI.Page
    Dim WaiverEffectiveDate As DateTime
    Dim PolicyEndDate As DateTime
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
        txtAssuredName.Attributes.Add("disabled", "disabled")
        txtProdDesc.Attributes.Add("disabled", "disabled")
        txtPolicyStartDate.Attributes.Add("disabled", "disabled")
        txtPolicyEndDate.Attributes.Add("disabled", "disabled")
        txtAssuredCode.Attributes.Add("disabled", "disabled")
        txtPolicyProCode.Attributes.Add("disabled", "disabled")
        strTableName = "TBIL_POLICY_DET"
        If Not IsPostBack Then
        Else
            txtAssuredName.Text = HidAssuredName.Value
            txtProdDesc.Text = HidProdDesc.Value
            txtPolicyStartDate.Text = HidPolStartDate.Value
            txtPolicyEndDate.Text = HidPolEndDate.Value
            txtPolicyProCode.Text = HidPolicyProCode.Value
            txtAssuredCode.Text = HidAssuredCode.Value
        End If
    End Sub
    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtPolicyNumber.Text = ""
                Me.txtPolicyProCode.Text = ""
                Me.txtAssuredCode.Text = ""
                'Me.txtSearch.Value = ""
            Else
                'Me.txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                'strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtPolicyNumber.Text, RTrim("0"))
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub
    Protected Sub cmdSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch1.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            'Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
            cboSearch.Items.Clear()
            cboSearch.Items.Add("* Select Insured *")
            Dim dt As DataTable = GET_INSURED(txtSearch.Value.Trim()).Tables(0)
            cboSearch.DataSource = dt
            cboSearch.DataValueField = "TBIL_POLY_POLICY_NO"
            cboSearch.DataTextField = "MyFld_Text"
            cboSearch.DataBind()
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
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtAssuredCode.Text = String.Empty) Then
            lblMsg.Text = "Please enter a assurance code"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyProCode.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy product code"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyStartDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy start date"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolicyEndDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter policy end date"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtWaiverEffectiveDate.Text = String.Empty) Then
            lblMsg.Text = "Please enter waiver effective date"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (drpWaiverCodes.Text = "Select") Then
            lblMsg.Text = "Please select a waiver code"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (drpWaiverCodes.Text = "Select") Then
            lblMsg.Text = "Please select a waiver code"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolyStatus.Value = "") Then
            lblMsg.Text = "Waiver cannot be process because status is null"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        If (Not IsDate(txtPolicyEndDate.Text)) Then
            lblMsg.Text = "Policy end date is not valid"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        Else
            PolicyEndDate = CDate(txtPolicyEndDate.Text)
        End If

        If (PolicyEndDate < Now) Then
            lblMsg.Text = "Policy End Date must be greater than today"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (txtPolyStatus.Value <> "A") Then
            lblMsg.Text = "Policy status must be Active"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (Not IsDate(txtWaiverEffectiveDate.Text)) Then
            lblMsg.Text = "Please enter a valid waiver effective date"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If (chkConfirmWaiver.Checked = False) Then
            lblMsg.Text = "Please confirm WAIVER"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If

        Dim str() As String
        str = DoDate_Process(txtWaiverEffectiveDate.Text, txtWaiverEffectiveDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Waiver Effective Date, ")
            lblMsg.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            txtWaiverEffectiveDate.Focus()
            ErrorInd = "Y"
            Exit Sub
        Else
            txtWaiverEffectiveDate.Text = str(2).ToString()
        End If

        Dim PolicyStartYear = Year(Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyStartDate.Text)))
        Dim PolicyEndYear = Year(Convert.ToDateTime(DoConvertToDbDateFormat(txtPolicyEndDate.Text)))
        Dim WaiverYear = Year(Convert.ToDateTime(DoConvertToDbDateFormat(txtWaiverEffectiveDate.Text)))

        If WaiverYear < PolicyStartYear Or WaiverYear > PolicyEndYear Then
            lblMsg.Text = "Waiver effective date must be within policy year"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
    End Sub

    Private Sub initializeFields()
        txtPolicyNumber.Text = String.Empty
        txtAssuredCode.Text = String.Empty
        txtAssuredName.Text = String.Empty
        txtPolicyProCode.Text = String.Empty
        drpWaiverCodes.SelectedIndex = 0
        txtProdDesc.Text = String.Empty
        txtPolicyStartDate.Text = String.Empty
        txtPolicyEndDate.Text = String.Empty
        txtWaiverEffectiveDate.Text = String.Empty
        chkConfirmWaiver.Checked = False
        HidAssuredName.Value = String.Empty
        HidProdDesc.Value = String.Empty
        HidPolStartDate.Value = String.Empty
        HidPolEndDate.Value = String.Empty
        HidPolicyProCode.Value = String.Empty
        HidAssuredCode.Value = String.Empty
    End Sub
    <System.Web.Services.WebMethod()> _
Public Shared Function GetPolicyPerInfo(ByVal _policyNo As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetPolicyPerInfo1(_policyNo)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCoverCodes() As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetCoverCodes()
            Dim i As Integer
            i = 8
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function
    <System.Web.Services.WebMethod()> _
Public Shared Function VerifyAdditionalCover(ByVal _WaiverCodes As String, ByVal _PolicyNumber As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.VerifyAdditionalCover(_WaiverCodes, _PolicyNumber)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function
    <System.Web.Services.WebMethod()> _
Public Shared Function GetEffectedWaiverDsc(ByVal waiverCode As String) As String
        Dim codeinfo As String = String.Empty
        Dim waiverRepo As New PRG_LI_CLM_WAIVER_REPOSITORY()
        Try
            codeinfo = waiverRepo.GetEffectedWaiverDsc(waiverCode)
            Return codeinfo
        Finally
            If codeinfo = "<NewDataSet />" Then
                Throw New Exception()
            End If
        End Try
    End Function
   

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
        'strSQL = strSQL & "AND TBIL_POLY_POLICY_NO='" & RTrim(txtPolicyProCode.Text) & "'"

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
                    .Rows(0)("TBIL_POLY_WAIVER_DT") = Convert.ToDateTime(DoConvertToDbDateFormat(txtWaiverEffectiveDate.Text))
                    .Rows(0)("TBIL_POLY_STATUS") = "W"
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
    End Sub

    Protected Sub cmdPrint_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint_ASP.Click
        Response.Redirect("PRG_LI_CLM_WAIVER_RPT.aspx")
    End Sub
End Class
