Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class I_LIFE_RPT_LI_DEFINITE_CERT
    Inherits System.Web.UI.Page
    Dim ErrorInd As String
    Protected FirstMsg As String
    Protected STRMENU_TITLE As String
    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean
    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new", "new", "new", "new", "new"}
    Protected PageLinks As String
    Dim strREC_ID As String
    Protected strOPT As String = "0"
    Protected BufferStr As String
    Dim li As ListItem

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadSupervisors()
            LoadReinsurance()
        End If
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

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        txtPolNum.Text = ""
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
            Else
                txtPolNum.Text = Me.cboSearch.SelectedItem.Value
                GetPolicyDetails(cboSearch.SelectedValue.Trim())
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
            lblMsg.Visible = True
        End Try
    End Sub

    Private Sub GetPolicyDetails(ByVal PolicyNo As String)
        lblMsg.Text = ""
        lblMsg.Visible = False
        DoNew()
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
                txtAssuredName.Text = objOLEReader("TBIL_INSRD_SURNAME") & " " & objOLEReader("TBIL_INSRD_FIRSTNAME")
                txtPolicyClass.Text = objOLEReader("TBIL_PRDCT_DTL_DESC")
                txtStartDate.Text = Format(objOLEReader("TBIL_POL_PRM_FROM"), "dd/MM/yyyy")
                txtEndDate.Text = Format(objOLEReader("TBIL_POL_PRM_TO"), "dd/MM/yyyy")
            Else
                lblMsg.Text = txtPolNum.Text & " is not a valid policy number"
                lblMsg.Visible = True
                txtPolNum.Text = ""
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "');"
                txtPolNum.Focus()
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

    Private Sub LoadSupervisors()
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
            strSQL = ""
            strSQL = strSQL + "SELECT SEC_USER_NAME, SEC_USER_LOGIN FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_ROLE='Supervisor'"
            objOLEComm.CommandText = strSQL
            strSQL = strSQL + " AND SEC_USER_FLAG NOT IN('D')"
            objOLEComm.CommandType = CommandType.Text
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            li = New ListItem
            li.Text = "Select"
            li.Value = "Select"
            cboSupervisor.Items.Add(li)
            While (objOLEReader.Read())
                li = New ListItem
                li.Text = objOLEReader("SEC_USER_NAME")
                li.Value = objOLEReader("SEC_USER_LOGIN")
                cboSupervisor.Items.Add(li)
            End While
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
    Private Sub LoadReinsurance()
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
            strSQL = ""
            strSQL = strSQL + "SELECT TBGL_REC_ID, TBGL_DESC FROM TBGL_REINSURANCE"
            strSQL = strSQL + " WHERE TBGL_FLAG NOT IN('D')"
            objOLEComm.CommandText = strSQL
            objOLEComm.CommandType = CommandType.Text
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            li = New ListItem
            li.Text = "Select"
            li.Value = "0"
            cboReinsurance.Items.Add(li)
            While (objOLEReader.Read())
                li = New ListItem
                li.Text = objOLEReader("TBGL_DESC")
                li.Value = objOLEReader("TBGL_REC_ID")
                cboReinsurance.Items.Add(li)
            End While
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

    Protected Sub cmdFileNum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileNum.Click
        Try
            If txtPolNum.Text = "" Then
                Me.lblMsg.Text = "Policy number must not be empty"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                lblMsg.Visible = True
                Exit Sub
            Else
                GetPolicyDetails(txtPolNum.Text)
            End If
        Catch ex As Exception
            lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub cmdPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        ErrorInd = ""
        lblMsg.Text = ""
        ValidateControls(ErrorInd)
        If ErrorInd = "Y" Then
            Exit Sub
        End If

        Dim myUserIDX As String = ""
        Try
            myUserIDX = CType(Session("MyUserIDX"), String)
        Catch ex As Exception
            myUserIDX = ""
        End Try


        Dim myResult As Boolean
        myResult = DetermineReInsurance(txtPolNum.Text)
        If myResult = False Then
            lblMsg.Text = "Policy holder cannot be Reassured"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        End If

        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = radReportType.SelectedValue
        rParams(1) = "pPolicyNo="
        rParams(2) = txtPolNum.Text + "&"
        rParams(3) = "pLoginId="
        rParams(4) = myUserIDX + "&"
        rParams(5) = "SupervisorId="
        rParams(6) = cboSupervisor.SelectedValue + "&"
        rParams(7) = "pReInsId="
        rParams(8) = cboReinsurance.SelectedValue + "&"
        rParams(9) = url

        Session("ReportParams") = rParams

        Response.Redirect("../PrintView.aspx")
    End Sub


    Private Function DetermineReInsurance(ByVal policyno As String) As Boolean
        Dim result As Boolean
        result = False
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
            Exit Function
        End Try


        Try
            strSQL = ""
            strSQL = "SELECT DET.[TBIL_POLY_POLICY_NO], PREM.[TBIL_POL_PRM_SA_LC]"
            strSQL = strSQL & " FROM [TBIL_POLICY_DET] AS DET"
            strSQL = strSQL & " INNER JOIN TBIL_POLICY_PREM_INFO AS PREM ON DET.TBIL_POLY_POLICY_NO= PREM.TBIL_POL_PRM_POLY_NO"
            strSQL = strSQL & " WHERE DET.[TBIL_POLY_POLICY_NO] = '" & policyno & "'"
            strSQL = strSQL & " AND DET.[TBIL_POLY_FLAG] <> 'D'"
            strSQL = strSQL & " AND PREM.[TBIL_POL_PRM_SA_LC] > DET.[TBIL_POLY_RETENTION]"

            objOLEComm.Connection = objOLEConn
            objOLEComm.CommandText = strSQL
            objOLEComm.CommandType = CommandType.Text
            Dim objOLEReader As OleDbDataReader = objOLEComm.ExecuteReader()
            If objOLEReader.HasRows = True Then
                result = True
            End If
        Catch ex As Exception
            Me.lblMsg.Text = ex.Message.ToString
            lblMsg.Visible = True
            Exit Function
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
        Return result
    End Function

    Private Sub DoNew()
        txtAssuredName.Text = ""
        txtPolicyClass.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""
        cboReinsurance.SelectedIndex = 0
        cboSupervisor.SelectedIndex = 0
    End Sub

    Private Sub ValidateControls(ByRef ErrorInd As String)
        If txtPolNum.Text = String.Empty Then
            lblMsg.Text = "Policy number must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If txtPolicyClass.Text = String.Empty Then
            lblMsg.Text = "Policy class must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If txtAssuredName.Text = String.Empty Then
            lblMsg.Text = "Assured name must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If txtStartDate.Text = String.Empty Then
            lblMsg.Text = "Policy Start date must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If txtEndDate.Text = String.Empty Then
            lblMsg.Text = "Policy End date must not be empty"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If cboSupervisor.SelectedIndex = 0 Then
            lblMsg.Text = "Please select a Supervisor"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If radReportType.SelectedValue = "" Then
            lblMsg.Text = "Please select a report type"
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            lblMsg.Visible = True
            ErrorInd = "Y"
            Exit Sub
        End If
        If radReportType.SelectedValue = "rptDefiniteCertLetter" Then
            If cboReinsurance.SelectedIndex = 0 Then
                lblMsg.Text = "Please select a Reinsurance firm"
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                lblMsg.Visible = True
                ErrorInd = "Y"
                Exit Sub
            End If
        End If
    End Sub

    Protected Sub cmdNew_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew_ASP.Click
        DoNew()
        txtPolNum.Text = ""
    End Sub
End Class
