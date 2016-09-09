Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.OleDb
Public Class PRG_LI_CLM_WAIVER_REPOSITORY
    Dim mystr_conn As String = ""
    Dim conn As OleDbConnection = Nothing
    Dim cmd As OleDbCommand = New OleDbCommand()
    Public Function GetPolicyPerInfo1(ByVal _policyNo As String) As String
        Return GetDataSet(_policyNo).GetXml()
    End Function
    Private Function GetDataSet(ByVal _policy As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_POLICY_INFO"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_POL_NO", _policy)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function
    Public Function GetCoverCodes() As String
        Return GetCoverCodesDataSet().GetXml()
    End Function
    Public Function GetCoverCodesDataSet() As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_COVERCODES"
        cmd.CommandType = CommandType.StoredProcedure
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        conn.Dispose()
        Return Ds
    End Function
    Public Function VerifyAdditionalCover(ByVal _WaiverCodes As String, ByVal _PolicyNumber As String) As String
        Return VerifyAdditionalCoverDataSet(_WaiverCodes, _PolicyNumber).GetXml()
    End Function
    Private Function VerifyAdditionalCoverDataSet(ByVal _WaiverCodes As String, ByVal _PolicyNumber As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_ADDITIONAL_COVER"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_COVER_CODE", _WaiverCodes)
        cmd.Parameters.AddWithValue("@PARAM_POL_ADD_POLY_NO", _PolicyNumber)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function

    Public Function GetEffectedWaiverDsc(ByVal waiverCode As String) As String
        Return GetEffectedWaiverDscDataSet(waiverCode).GetXml()
    End Function
    Private Function GetEffectedWaiverDscDataSet(ByVal waiverCode As String) As DataSet
        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conn = New OleDbConnection(mystr_conn)
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_EFFECTED_COVER_DESC"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_COVER_CD", waiverCode)
        Dim Adapter As OleDbDataAdapter = New OleDbDataAdapter()
        Adapter.SelectCommand = cmd
        conn.Open()
        Dim Ds As DataSet = New DataSet()
        Adapter.Fill(Ds)
        conn.Close()
        Return Ds
    End Function
End Class
