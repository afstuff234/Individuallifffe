<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started

        
        ' Change Session Timeout to 20 minutes (if you need to)
        'Session.Timeout = 20
        ' Set a Session Start Time

        '//Ensure that this page expires within 10 minutes...	
        '   Response.Expires = 10        
        '//...or before Jan 1, 2001, which ever comes first.
        '   Response.ExpiresAbsolute = "Jan 1, 2001 13:30:15";

        Dim strCONN As String = "data source=abs-pc;initial catalog=abs_life;user id=sa;password=;"
        Dim myreturn_status As String = ""

        'strCONN = "Data Source=" & gnAPP_SRV_NAME & ";Initial Catalog=" & gnAPP_DB_NAME & ";User Id=" & gnAPP_UID & ";Password=;"
        'Session("connstr") = ConfigurationSettings.AppSettings("APPCONN")
        'Session("connstr") = CType(ConfigurationManager.AppSettings("ABSECONN"), String)

        'Session("connstr") = ConfigurationManager.AppSettings("APPCONN")
        Session("connstr_rpt") = CType(ConfigurationManager.ConnectionStrings("APPCONN_RPT").ToString, String)


        strCONN = gnGET_CONN_STRING()

        Session("connstr") = "Provider=SQLOLEDB;" & strCONN

        Session("connstr_SQL") = strCONN

        myreturn_status = MOD_GEN.gnGET_COMPANY_INFO("001", "001", strCONN)
        Session("CL_COMP_NAME") = gnCOMP_NAME

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>