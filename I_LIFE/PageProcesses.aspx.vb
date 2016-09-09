
Imports System.Data.OleDb
Imports System.Data

Partial Class I_LIFE_PageProcesses
    Inherits System.Web.UI.Page

    Private _rtnMessage As String
    Private _xmlMessage as String


     <System.Web.Services.WebMethod()> _
    Public  Function CHECK_IF_CLAIM_EXIST(ByVal claimNumber As String) As String
        Dim mystrConn As String = CType(Session("connstr"), String)
        Dim conn1 As OleDbConnection
        conn1 = New OleDbConnection(mystrConn)
        Dim cmd1 As OleDbCommand = New OleDbCommand()
        cmd1.Connection = conn1
        cmd1.CommandText = "SPIL_GET_CLAIM_PAID"
        cmd1.CommandType = CommandType.StoredProcedure
        cmd1.Parameters.AddWithValue("@pCLAIM_NUMBER", claimNumber)

        Try
            conn1.Open()
            Dim objOledr1 As OleDbDataReader
            objOledr1 = cmd1.ExecuteReader()
            If (objOledr1.Read()) Then
                Dim s As String = CType(objOledr1("TBIL_CLM_PAID_CLM_NO") & vbNullString, String)

            Else
                _rtnMessage = "empty data"
            End If
        Catch ex As Exception
            _rtnMessage = "Error retrieving data! " + ex.Message
        End Try

        Return _rtnMessage

    End Function


End Class
