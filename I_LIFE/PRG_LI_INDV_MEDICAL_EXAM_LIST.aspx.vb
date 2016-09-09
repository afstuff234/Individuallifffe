
Imports System.Data
Imports System.Data.OleDb

Partial Class I_LIFE_PRG_LI_INDV_MEDICAL_EXAM_LIST
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
    Protected strQ_ID As String
    Protected strP_ID As String

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

    ' additional premium variable
    Dim dblAdd_Prem_Amt As Double = 0
    Dim dblAdd_Prem_Rate As Double = 0
    Dim dblAdd_Rate_Per As Integer = 0
    Dim dblAdd_Prem_SA_LC As Double
    Dim dblAdd_Prem_SA_FC As Double

    Dim dblTotal_Add_Prem_LC As Double = 0
    Dim dblTotal_Add_Prem_FC As Double = 0

    Dim dblTotal_Prem_LC As Double = 0
    Dim dblTotal_Prem_FC As Double = 0

    Dim dblAnnual_Basic_Prem_LC As Double = 0
    Dim dblAnnual_Basic_Prem_FC As Double = 0

    Dim dblTmp_Amt As Double = 0


    Dim rParams As String() = {"nw", "nw", "nw", "nw", "nw", "new"}



    Public Sub GET_POLICYDATE_BY_FILENO(ByVal fileNumber As String)
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_IND_POLICYDATE_BY_FILENO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_POL_PRM_POLY_NO", fileNumber)

        Try
            conn.Open()
            Dim objOledr As OleDbDataReader
            objOledr = cmd.ExecuteReader()
            If (objOledr.Read()) Then
                Dim newStartDate As Date = Convert.ToDateTime(objOledr("TBIL_POL_PRM_FROM"))
                Dim newEndDate As Date = Convert.ToDateTime(objOledr("TBIL_POL_PRM_TO"))

                newStartDate = newStartDate.AddYears(1)
                newEndDate = newEndDate.AddYears(1)
                txtPrem_Start_Date.Text = newStartDate.ToString("dd/MM/yyyy")
                txtPrem_End_Date.Text = newEndDate.ToString("dd/MM/yyyy")

            End If

            conn.Close()
        Catch ex As Exception
            strErrMsg = "Error retrieving data! " + ex.Message
        End Try
    End Sub

    'Dim rParams As String() = {"nw", "nw", "nw", "nw", "nw", "nw", "nw", "nw", "new", "new"}


    Public Sub GET_SUPERVISORS()
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim sqlQry As String = "SELECT SEC_USER_REC_ID, SEC_USER_NAME FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_ROLE = 'SUPERVISOR'"
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = sqlQry
        cmd.CommandType = CommandType.Text

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()

            Dim dt As DataTable = ds.Tables(0)
            Dim dr As DataRow = dt.NewRow()

            dr("SEC_USER_REC_ID") = 0
            dr("SEC_USER_NAME") = "-- Select --"
            dt.Rows.InsertAt(dr, 0)

            cboSupervisor.DataSource = dt
            cboSupervisor.DataValueField = "SEC_USER_REC_ID"
            cboSupervisor.DataTextField = "SEC_USER_NAME"
            cboSupervisor.DataBind()

        Catch ex As Exception
            strErrMsg = "Error retrieving data! " + ex.Message
        End Try
    End Sub

    Public Sub GET_ASSOCIATE_COMPANY()
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim sqlQry As String = "SELECT TBGL_REC_ID, TBGL_DESC FROM TBGL_REINSURANCE WHERE TBGL_COMPANY_TYPE = 'A'"
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = sqlQry
        cmd.CommandType = CommandType.Text

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()

            Dim dt As DataTable = ds.Tables(0)
            Dim dr As DataRow = dt.NewRow()

            dr("TBGL_REC_ID") = 0
            dr("TBGL_DESC") = "-- Select --"
            dt.Rows.InsertAt(dr, 0)

            cboAssCompany.DataSource = dt
            cboAssCompany.DataValueField = "TBGL_REC_ID"
            cboAssCompany.DataTextField = "TBGL_DESC"
            cboAssCompany.DataBind()

        Catch ex As Exception
            strErrMsg = "Error retrieving data! " + ex.Message
        End Try
    End Sub


    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        'FIRST CHECK FOR DATE VALUES BEFORE SENDING DATA
        Dim sDate As String
        Dim eDate As String
        If txtPrem_End_Date.Text <> "" Or txtPrem_Start_Date.Text <> "" Then
            sDate = DoConvertToDbDateFormat(txtPrem_Start_Date.Text)
            eDate = DoConvertToDbDateFormat(txtPrem_End_Date.Text)

        Else
            FirstMsg = "javascript:alert('Start or end date cannot be empty!');"
            Exit Sub
        End If

        If cboAssCompany.SelectedIndex <> 0 Or cboSupervisor.SelectedIndex <> 0 Then

            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
            rParams(0) = "rptIND_MEDICAL_UNDER_CLASS_TEST"
            rParams(1) = "pSTART_DATE="
            rParams(2) = sDate + "&"
            rParams(3) = "pEND_DATE="
            rParams(4) = eDate + "&"
            'rParams(5) = "pASSOCIATE_REC_ID="
            'rParams(6) = cboAssCompany.SelectedValue.ToString() + "&"
            'rParams(7) = "pSUPERVISOR="
            'rParams(8) = cboSupervisor.SelectedValue.ToString() + "&"
            rParams(5) = url
        Else
            FirstMsg = "javascript:alert('Select Supervisor or Associated company!');"
            Exit Sub
        End If



        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
    End Sub

    'Protected Sub rBtnOption_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rBtnOption.SelectedIndexChanged


    '    Dim sDate, eDate

    '    If txtPrem_End_Date.Text <> "" Or txtPrem_Start_Date.Text <> "" Then
    '        sDate = DoConvertToDbDateFormat(txtPrem_Start_Date.Text)
    '        eDate = DoConvertToDbDateFormat(txtPrem_End_Date.Text)

    '    Else
    '        FirstMsg = "javascript:alert('Start or end date cannot be empty!');"
    '        Exit Sub
    '    End If

    '    Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
    '    rParams(0) = "rptIND_MEDICAL_UNDER_CLASS_TEST_LIST"
    '    rParams(1) = "pSTART_DATE="
    '    rParams(2) = CType((sDate + "&"), String)
    '    rParams(3) = "pEND_DATE="
    '    rParams(4) = CType((eDate + "&"), String)
    '    rParams(5) = "pASSOCIATE_REC_ID="
    '    rParams(6) = cboAssCompany.SelectedValue.ToString() + "&"
    '    rParams(7) = "pSUPERVISOR="
    '    rParams(8) = cboSupervisor.SelectedValue.ToString() + "&"
    '    rParams(9) = url

    '    Session("ReportParams") = rParams
    '    Response.Redirect("../PrintView.aspx")
    'End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not IsPostBack) Then
            GET_SUPERVISORS()
            GET_ASSOCIATE_COMPANY()
        End If

    End Sub
End Class
