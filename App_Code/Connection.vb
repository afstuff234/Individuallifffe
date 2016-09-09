Imports Microsoft.VisualBasic

Public Class Connection
    Function SetConnectionString() As String
        Dim conString As String = Nothing
        'Dim mystr_conn = gnGET_CONN_STRING()
        'Dim type mystr_conn = "Provider=SQLOLEDB;" & mystr_conn
        conString = ConfigurationManager.ConnectionStrings("CLAIMSREQUEST").ConnectionString

        Return conString
    End Function
End Class
