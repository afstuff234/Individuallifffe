
'Imports CrystalDecisions.ReportAppServer.ClientDoc
'Imports CrystalDecisions.ReportAppServer.Controllers
'Imports CrystalDecisions.ReportAppServer.DataDefModel
'Imports CrystalDecisions.ReportAppServer.CommonObjectModel

Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Reporting
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions
Imports System.Data.SqlClient
Imports System.Data


Partial Class I_LIFE_CRViewer
    Inherits System.Web.UI.Page

    Protected FirstMsg As String

    Protected STRCOMP_NAME As String

    Protected STRUSER_LOGIN_ID As String
    Protected STRUSER_LOGIN_NAME As String


    Protected strDB_Srv As String
    Protected strDB_Name As String
    Protected strDB_UID As String
    Protected strDB_PWD As String

    Protected strRptParams As String
    Protected strDBParams As String

    Protected arrDB(15) As String
    Protected arrRPT(15) As String
    Protected intDB As Integer = 0
    Protected intRPT As Integer = 0

    Protected strReportPath As String
    Protected strRptName As String

    Protected WithEvents crdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            STRCOMP_NAME = CType(Session("CL_COMP_NAME"), String).ToString
        Catch ex As Exception
            'STRCOMP_NAME = gnComp_Name
            STRCOMP_NAME = "ABC COMPANY LTD"
            STRCOMP_NAME = "Custodian Life Assurance Limited"
        End Try

        Try
            STRUSER_LOGIN_ID = CType(Session("MyUserIDX"), String).ToString
            STRUSER_LOGIN_NAME = CType(Session("MyUserName"), String).ToString
        Catch ex As Exception
            'UNLO=USER NOT LOG ON
            STRUSER_LOGIN_ID = "UNLO"
            STRUSER_LOGIN_NAME = "*** USER NOT LOGGED ON ***"
            'Response.Redirect("~/Default.aspx")
        End Try


        Me.lblMessage.Text = ""

        If Not (Page.IsPostBack) Then
            Call DoProc_Init()
        End If

        Try
            strRptName = Page.Request.QueryString("rptname").ToString
            'strRptName = "Rpt_Polx"

            'strRptName = "r" & strRptName & ".rpt"
            strRptName = strRptName & ".rpt"
        Catch
            strRptName = "rptMissing.aspx"
            'strRptName = "rnom1505n.rpt"
            Me.lblMessage.Text = "Missing or Invalid report name..." & ""
            Exit Sub
        End Try


        Try
            strRptParams = Page.Request.QueryString("rptparams").ToString
            Call DoLoad_RPT_Info(strRptParams)
        Catch
            strRptParams = "Statement of Financial Position"
            strRptParams = "*** Missing Report Title ***"
            Me.lblMessage.Text = Me.lblMessage.Text & "<br>" & strRptParams
        End Try


        Try
            strDBParams = Page.Request.QueryString("dbparams").ToString
            Call DoLoad_DB_Info(strDBParams)
        Catch
            strDBParams = "*** Missing Report Date ***"
            Me.lblMessage.Text = Me.lblMessage.Text & "<br>" & strDBParams
        End Try



        If Not IsPostBack Then
            Call MyShow_Report()
            Session("mycrdoc") = crdoc

        Else
            Try
                crdoc = CType(Session("mycrdoc"), CrystalDecisions.CrystalReports.Engine.ReportDocument)
                'Dim crdoc_new As CrystalDecisions.CrystalReports.Engine.ReportDocument
                'crdoc_new = CType(Session("mycrdoc"), CrystalDecisions.CrystalReports.Engine.ReportDocument)
                'Session("mycrdoc") = crdoc_new

                Me.CrystalReportViewer1.ReportSource = crdoc
                'CrystalReportViewer1.ReportSource = crdoc_new
                Call DoConfig_Report_ParametersInfo()
                'Me.CrystalReportViewer1.DataBind()
                'Call MyShow_Report()

                Session("mycrdoc") = crdoc

            Catch ex As Exception
                Response.Write("<br>Error has occured. <br />Reason:" & ex.Message.ToString)

            End Try

        End If


        If Not (Page.IsPostBack) Then
            Try
                Me.CrystalReportViewer1.ShowLastPage()
                Me.txtTotalPageNumber.Text = Me.txtPageNumber.Text
                Me.CrystalReportViewer1.ShowFirstPage()
            Catch ex As Exception

            End Try
        End If

        '   User name
        'Dim userName As String = _
        '    ConfigurationManager.AppSettings("MyReportViewerUser")

        'If (String.IsNullOrEmpty(userName)) Then
        '    Throw New Exception("Missing user name from web.config file")
        'End If

    End Sub

    Protected Sub cmdCloseX_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseX.ServerClick

        'Try
        '    oConInfo = Nothing
        'Catch ex As Exception

        'End Try

        Try
            crdoc.Close()
        Catch ex As Exception
        End Try

        Try
            crdoc.Dispose()
        Catch ex As Exception
        End Try

        Try
            crdoc = Nothing
        Catch ex As Exception
        End Try

        'Try
        '    Me.CrystalReportViewer1.Dispose()
        'Catch ex As Exception

        'End Try

        Dim mystrURL As String = ""
        Try
            'mystrURL = "alert('About to close page ...'); window.close();"
            mystrURL = "window.close();"
            '    'FirstMsg = "javascript:window.close();" & mystrURL
            FirstMsg = "javascript:" & mystrURL

        Catch ex As Exception
        End Try


        'Me.lblMessage.Text = "About to close page..."
        'FirstMsg = "Javascript:alert('About to close page...')"
        'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "ShowPopup_Message('" & Me.lblMessage.Text & "');", True)
        'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "<script language=""JavaScript"">alert('" & Me.lblMessage.Text & "');</script>", True)
        'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_Validation", "<script language=""JavaScript"">alert('About to close page...');</script>", True)

    End Sub

    Private Sub DoProc_Init()
        Dim intRC As Integer
        intRC = 0
        For intRC = 0 To 15 - 1
            arrRPT(intRC) = ""
            arrDB(intRC) = ""
        Next

    End Sub

    Private Sub DoLoad_RPT_Info(ByVal pvParam As String)

        Dim intRC As Integer
        intRC = 0

        'Response.Write("<hr />Report parameter start...")

        arrRPT = Split(pvParam, "<*>")
        intRC = 0
        For intRC = 0 To UBound(arrRPT)
            'Response.Write("<br />Report Parameter " & intRC + 1 & " : " & arrRPT(intRC))
        Next

        'Response.Write("<br />End...")

    End Sub

    Private Sub DoLoad_DB_Info(ByVal pvParam As String)

        Dim intRC As Integer
        intRC = 0

        'Response.Write("<hr />Database parameter start...")
        arrDB = Split(pvParam, "<*>")
        intRC = 0
        For intRC = 0 To UBound(arrDB)
            'Response.Write("<br />Database Parameter " & intRC + 1 & " : " & arrDB(intRC))
        Next

        'Response.Write("<br />End...")

    End Sub

    Private Sub MyDownload(ByVal sDL_FileName As String)
        ' codes ok - to preview document

        Dim sFileName As String = sDL_FileName

        Dim objclient As New System.Net.WebClient()
        Dim objbuffer As [Byte]() = objclient.DownloadData(sFileName)
        If objbuffer IsNot Nothing Then
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-length", objbuffer.Length.ToString())
            Response.BinaryWrite(objbuffer)
            Response.End()
        End If
        objclient = Nothing
        objbuffer = Nothing


        ' code ok - provide options to [open], [save] and [save-open] document
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment;filename=" & sDL_FileName)
        ''Response.TransmitFile(Server.MapPath("~/Files/" & sFileName))
        'Response.TransmitFile(sFileName)
        'Response.End()


        ' // using System.IO
        Dim oStream As New System.IO.MemoryStream

        'Response.ContentType = strConType
        'Response.AddHeader("content-length", oStream.Length.ToString())
        ''Response.TransmitFile(Server.MapPath("E:\Export_Doc\" & sDocName))
        'Response.TransmitFile(sFileName)

        'Try
        '    Response.BinaryWrite(oStream.ToArray())
        '    Response.End()
        'Catch err As Exception
        '    Response.Write("< BR > Download error!. < BR >")
        '    Response.Write(err.Message.ToString & "< BR >")
        'End Try



    End Sub


    Private Sub MyExport_Rtn(ByVal pvFileName As String)

        Call MyShow_Report("EXP_RPT")

        Dim pvFormatType As CrystalDecisions.Shared.ExportFormatType
        'reportDoc.Load(report);
        'reportDoc.DataSourceConnections[0].SetConnection("server", "database", "user", "pwd");
        'reportDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, path);

        If (pvFileName) = "" Then
            pvFileName = "PDF_FILE.pdf"
            pvFormatType = ExportFormatType.PortableDocFormat
        End If


        Try

            'crdoc.SetDatabaseLogon(strDB_UID, strDB_PWD, strDB_Srv, strDB_Name, True)

            crdoc = New CrystalDecisions.CrystalReports.Engine.ReportDocument
            crdoc.Load(strReportPath)
            'crdoc.Refresh()

            crdoc.DataSourceConnections.Clear()
            crdoc.DataSourceConnections(0).SetConnection(CStr(strDB_Srv), CStr(strDB_Name), CStr(strDB_UID), CStr(strDB_PWD))


            With crdoc
                '    .SetParameterValue(0, gnCOMP_NAME)
                '    .SetParameterValue(1, gnComp_Addr1)
                '    .SetParameterValue(2, gnComp_TelNum)
                '    '.SetParameterValue(3, gnComp_WebSite)
                '    '.SetParameterValue(4, strReportTitle)
                '    .SetParameterValue(5, arrDB(1))
                '    .SetParameterValue(6, arrDB(2))

                .SetParameterValue(0, arrDB(0))
                .SetParameterValue(1, arrDB(1))

            End With

            crdoc.ExportToDisk(pvFormatType, pvFileName)

            Response.Redirect(pvFileName)

        Catch ex As Exception
            Response.Write("<br>Report File: " & strReportPath)
            Response.Write("<br>Error has occured. <br />Reason:" & ex.Message.ToString)
            crdoc = Nothing
            Exit Sub

        End Try


    End Sub

    Private Sub MyShow_Report(Optional ByVal pvCODE As String = "")

        strReportPath = gnGET_REPORT_PATH() & strRptName
        'Response.Write("<br>Report File: " & strReportPath)

        crdoc = New CrystalDecisions.CrystalReports.Engine.ReportDocument

        Try

            If System.IO.File.Exists(strReportPath) = False Then
                Me.lblMessage.Text = "Unable to open Report or Document: " & strReportPath
                'FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
                FirstMsg = "Javascript:alert('" & "Unable to open Report or Document: " & strRptName & "')"
                Exit Sub
            End If

            With crdoc
                .Load(strReportPath)
                '.Refresh()

            End With

        Catch ex As Exception
            Response.Write("<br>Report File: " & strReportPath)
            Response.Write("<br>Error has occured. <br />Reason:" & ex.Message.ToString)
            crdoc = Nothing
            Exit Sub
        End Try


        ' strDB_Srv = "YUSUF-pc"
        strDB_Srv = "(LOCAL)"
        strDB_Name = "abs"
        strDB_Name = "ABS_LIFE"
        strDB_UID = "sa"
        strDB_PWD = ""

        '  strDB_Srv = "abs-pc"
        '  strDB_Name = "abs"
        ' strDB_UID = "sa"
        ' strDB_PWD = ""

        strDB_Srv = "ABS_ACCT_DSN_RPT"
        strDB_Name = "C:\AFRIK\RPT-DB\STAPP_DB_RPT"
        strDB_UID = "Admin"
        strDB_PWD = ""

        Dim strmyCONN = "Data source=" & strDB_Srv & ";Initial catalog=" & strDB_Name & ";User ID=" & strDB_UID & ";Password=" & strDB_PWD & ";"


    


        strmyCONN = RTrim(gnGET_CONN_STRING())
        '    strmyCONN = "Provider=SQLOLEDB;" & CType(Session("connstr_rpt"), String).ToString
        '****strmyCONN = CType(Session("connstr_rpt"), String).ToString
        'Response.Write("<br /><br />Database Connection String-1: " & strmyCONN)

        ' MsgBox(gnGET_CONN_STRING)
        '  MsgBox(strmyCONN)


        Dim myarrData() As String
        Dim mystrData As String
        myarrData = Split(RTrim(strmyCONN), ";")
        For Each mystrData In myarrData
            Dim myintpos As Integer = InStr(1, mystrData, "=", CompareMethod.Text)
            If myintpos >= 1 Then
                'Response.Write("<br />Searching for '=' in [ " & mystrData & " ]. Found in position : " & myintpos & ". [ Remaining text: " & Mid(mystrData, myintpos + 1) & " ]")
                If UCase(Mid(mystrData, 1, myintpos - 1)) = "DATA SOURCE" Then
                    strDB_Srv = UCase(Mid(mystrData, myintpos + 1))
                ElseIf UCase(Mid(mystrData, 1, myintpos - 1)) = "INITIAL CATALOG" Then
                    strDB_Name = Mid(mystrData, myintpos + 1)
                ElseIf UCase(Mid(mystrData, 1, myintpos - 1)) = "USER ID" Then
                    strDB_UID = Mid(mystrData, myintpos + 1)
                ElseIf UCase(Mid(mystrData, 1, myintpos - 1)) = "PASSWORD" Then
                    strDB_PWD = Mid(mystrData, myintpos + 1)
                End If
            End If
        Next

        'strDB_Srv = UCase("CUSTODIA-HDPO9M")
        ''strDB_Srv = UCase("192.168.10.18")
        'strDB_Name = "ABS_LIFE"
        'strDB_UID = "sa"
        'strDB_PWD = ""

            If Trim(strDB_PWD) = "" Then
                'strDB_PWD = " "
            End If
            strmyCONN = "Data Source=" & strDB_Srv & ";Initial Catalog=" & strDB_Name & ";User ID=" & strDB_UID & ";Password=" & strDB_PWD & ";"
        'Response.Write("<br /><br />Database Connection String-2: " & strmyCONN)


            If Trim(pvCODE) = "EXP_RPT" Then
            'Exit Sub
            End If


            Dim oConInfo As New CrystalDecisions.Shared.ConnectionInfo
            With oConInfo
                .AllowCustomConnection = True
                '.Type = ConnectionInfoType.SQL
                .Type = ConnectionInfoType.CRQE

                '.IntegratedSecurity = False

                .ServerName = strDB_Srv
                .DatabaseName = strDB_Name
                .UserID = strDB_UID
                .Password = strDB_PWD

            End With


            'Table Locatiom:ABSWT_BS_PL

            'Default server name: ABS_ACCT_DSN_RPT
            'Default database name: C:\AFRIK\RPT-DB\STAPP_DB_RPT
            'Default user id: admin
            'Default password: 
            'Integrated security: False
            'Default database type: CRQE 


            'crdoc.DataSourceConnections.Clear()
            'crdoc.DataSourceConnections(0).SetConnection(strDB_Srv, strDB_Name, strDB_UID, strDB_PWD)
            'crdoc.DataSourceConnections("old_DB_Srv", "old_DB_Name").SetConnection(CStr(strDB_Srv), CStr(strDB_Name), CStr(strDB_UID), CStr(strDB_PWD))

            '   FOR INTEGRATED SECURITY
            'crdoc.DataSourceConnections(0).SetConnection(strDB_Srv, strDB_Name, True)

            'crdoc.SetDatabaseLogon("user", "password", "server", "database")
            'crdoc.SetDatabaseLogon(strDB_UID, strDB_PWD, strDB_Srv, strDB_Name, True)


            Dim myTableName As String = "ABSWT_BS_PL"
            'myTableName = crdoc.Database.Tables.Item(0).Location

            'Response.Write("<br>Table Locatiom: " & myTableName)


            'Dim myLogOnInfo As New TableLogOnInfo()
            'myLogOnInfo = crdoc.Database.Tables.Item(myTableName).LogOnInfo
            'myLogOnInfo = crdoc.Database.Tables.Item(0).LogOnInfo

            'With myLogOnInfo
            '    Response.Write("<br />")
            '    Response.Write("<br />Table Name: " & .TableName)
            '    Response.Write("<br />Report Name: " & .ReportName)
            '    Response.Write("<br />Server Name: " & .ConnectionInfo.ServerName)
            '    Response.Write("<br />Database Name: " & .ConnectionInfo.DatabaseName)
            '    Response.Write("<br />UserID Name: " & .ConnectionInfo.UserID)
            '    Response.Write("<br />Password Name: " & .ConnectionInfo.Password)
            '    Response.Write("<br />Password Name: " & .ConnectionInfo.IntegratedSecurity)
            '    Response.Write("<br />Connection Type: " & .ConnectionInfo.Type)
            '    Response.Write("<br />")

            'End With

            'Dim myConnectionInfo As New ConnectionInfo()
            'myConnectionInfo = crdoc.Database.Tables.Item(tableName).LogOnInfo.ConnectionInfo
            'myConnectionInfo = myLogOnInfo.ConnectionInfo

            'With myConnectionInfo
            '    Response.Write("<br />")
            '    Response.Write("<br />Default server name: " & .ServerName)
            '    Response.Write("<br />Default database name: " & .DatabaseName)
            '    Response.Write("<br />Default user id: " & .UserID)
            '    Response.Write("<br />Default password: " & .Password)
            '    Response.Write("<br />Integrated security: " & .IntegratedSecurity)
            '    Response.Write("<br />Default database type: " & .Type.ToString())
            '    Response.Write("<br />")
            'End With


            'myConnectionInfo.ServerName = "ABS_ACCT_DSN_RPT"
            'myConnectionInfo.DatabaseName = "C:\AFRIK\RPT-DB\STAPP_DB_RPT"
            'myConnectionInfo.UserID = "Admin"
            'myConnectionInfo.Password = ""
            'myConnectionInfo.IntegratedSecurity = False
            'myConnectionInfo.Type = ConnectionInfoType.CRQE

            'myLogOnInfo.ConnectionInfo = myConnectionInfo
            'myLogOnInfo.TableName = myTableName
            'myLogOnInfo.ReportName = strRptName

            'crdoc.Database.Tables.Item(0).Location = myTableName
            'crdoc.Database.Tables.Item(myTableName).ApplyLogOnInfo(myLogOnInfo)

            'myLogOnInfo = Nothing
            'myConnectionInfo = Nothing

            '========================================================
            '
            'Using rd As New ReportDocument
            '    rd.Load("C:\Temp\CrystalReports\InternalAccountReport.rpt")
            '    rd.ApplyNewServer("serverName or DSN", "databaseUsername", "databasePassword")
            '    rd.ApplyParameters("AccountNumber=038PQRX922;", True)
            '    rd.ExportToDisk(ExportFormatType.PortableDocFormat, "c:\temp\test.pdf")
            '    rd.Close()
            'End Using

            'System.Diagnostics.Process.Start("c:\temp\test.pdf")

            '========================================================
            Dim intC As Integer = 0
            Dim intC2 As Integer = 0

            Dim cr_ds_conn As CrystalDecisions.Shared.DataSourceConnections = crdoc.DataSourceConnections
            Dim cr_iconinfo As CrystalDecisions.Shared.IConnectionInfo
            For intC = 0 To cr_ds_conn.Count - 1
                cr_iconinfo = cr_ds_conn.Item(intC)
                With cr_iconinfo
                    'Response.Write("<br />Default server name: " & .ServerName)
                    'Response.Write("<br />Default database name: " & .DatabaseName)
                    'Response.Write("<br />Default user id: " & .UserID)
                    'Response.Write("<br />Default password: " & .Password)
                    'Response.Write("<br />Integrated security: " & .IntegratedSecurity)
                    'Response.Write("<br />Default database type: " & .Type.ToString())


                    '.SetConnection("ABS_ACCT_DSN_RPT", "C:\AFRIK\RPT-DB\STAPP_DB_RPT", "Admin", "")
                    '.SetConnection("C:\AFRIK\RPT-DB", "STAPP_DB_RPT.MDB", "admin", "")
                    '.SetConnection("ABS_ACCT_DSN_RPT", "C:\AFRIK\RPT-DB\STAPP_DB_RPT.MDB", False)
                    '.SetLogon(gnAPP_UID, gnAPP_PWD)

                    ' for Windows Integrated security
                    '.SetConnection(CStr(strDB_Srv), CStr(strDB_Name), True)

                    'do this for each distinct database connection, rather than for table
                    ' for SQL Server
                    'Select Case UCase(cr_iconinfo.ServerName.ToString)
                    '    Case strDB_Srv
                    '        'Response.Write("<br/>Setting user name and password")
                    '        'Response.Write("<br/>Old Server name: " & cr_iconinfo.ServerName.ToString)
                    '        'Response.Write("<br/>New Server name: " & strDB_Srv.ToString)

                    '        cr_iconinfo.SetLogon(CStr(strDB_UID), CStr(strDB_PWD))

                    '    Case Else
                    '        'Response.Write("<br/>Setting server name, database name, user name and password")
                    '        'Response.Write("<br/>Old Server name: " & cr_iconinfo.ServerName.ToString)
                    '        'Response.Write("<br/>New Server name: " & strDB_Srv.ToString)

                    .SetConnection(CStr(strDB_Srv), CStr(strDB_Name), CStr(strDB_UID), CStr(strDB_PWD))

                    '        ' ok
                    '        ' Change the server name and database in main reports
                    '        'crdoc.DataSourceConnections(cr_iconinfo.ServerName, cr_iconinfo.DatabaseName).SetConnection(CStr(strDB_Srv), CStr(strDB_Name), CStr(strDB_UID), CStr(strDB_PWD))
                    'End Select


                End With
            Next


        'crdoc.SetDatabaseLogon(strDB_UID, strDB_PWD, strDB_Srv, strDB_Name, True)

            'crdoc.SetDataSource(gnGet_DataSet("abswt_bs_pl", "IFRS_BS"))

        'Me.CrystalReportViewer1.ReportSource = strReportPath

        '*****
        Call SetCrystalLogin(strDB_Srv, strDB_Name, strDB_UID, strDB_PWD, crdoc)
        '*****
        'Call SetCrystalLogin(CStr(strDB_Srv), CStr(strDB_Name), CStr(strDB_UID), CStr(strDB_PWD), crdoc)

        'Me.CrystalReportViewer1.ReportSource = crdoc

        Call SetDBLogon_Info(oConInfo, crdoc)

        crdoc.Refresh()

        Me.CrystalReportViewer1.ReportSource = crdoc
        Call SetDBTableLogon(oConInfo, Me.CrystalReportViewer1)

            'Me.CrystalReportViewer1.ReportSource = crdoc
            'Call SetDBLogon_Info(oConInfo, crdoc)


        'crdoc.Refresh()

            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.ServerName = CStr(strDB_Srv)
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.DatabaseName = CStr(strDB_Name)
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.UserID = CStr(strDB_UID)
            'Me.CrystalReportViewer1.LogOnInfo.Item(0).ConnectionInfo.Password = CStr(strDB_PWD)

            'Call SetDBTableLogon(oConInfo)

            'Me.CrystalReportViewer1.SelectionFormula = "{ABSWT_BS_PL.WTBS_PL_PROG_ID} = '" & RTrim("IFRS_BS") & "'"

            'Response.Write("<br>")
            'Response.Write("<br>Parameter Info.")

            '' Get the Parameter Field definitions from the report object.
            'Dim PFDs As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinitions
            'PFDs = crdoc.DataDefinition.ParameterFields

            '' Create a Parameter Field Defintion to use with the API.
            'Dim pf As CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition
            'Dim pvalues As ParameterValues
            'Dim pdv As ParameterDiscreteValue


            '' Select a specific parameter and get a pointer to its value list.
            'For intC = 0 To PFDs.Count - 1
            '    Response.Write("<br>Field Name: " & PFDs.Item(intC).ParameterFieldName)
            '    Response.Write("<br>Prompt Text: " & PFDs.Item(intC).PromptText)

            '    pf = PFDs.Item(intC)
            '    pvalues = pf.CurrentValues


            '    For intC2 = 0 To pvalues.Count - 1
            '        Dim pvalue As ParameterValue = pvalues.Item(intC2)
            '        Response.Write("<br>Default Value " & intC2 & ": " & pvalue.Description)

            '    Next

            '    ' Create a new parameter value.
            '    pdv = New ParameterDiscreteValue
            '    pdv.Value = "This Is A Param!!! - " & intC

            '    ' Add the new parameter value to the list of values.
            '    pvalues.Add(pdv)

            '    ' pass the updated list of values to the report.
            '    pf.ApplyCurrentValues(pvalues)


            'Next


            intDB = 0
            intRPT = 0
            intC = 0

        Call DoConfig_Report_ParametersInfo()

    End Sub

    Private Sub DoConfig_Report_ParametersInfo()

        intDB = 0
        intRPT = 0

        Dim intC As Integer = 0
        intC = 0

        Dim ParamFlds As CrystalDecisions.Shared.ParameterFields
        'ParamFlds = crdoc.ParameterFields
        ParamFlds = Me.CrystalReportViewer1.ParameterFieldInfo


        For intC = 0 To ParamFlds.Count - 1

            'Response.Write("<br>Parameter Type: " & ParamFlds.Item(intC).ParameterType.ToString)
            'Response.Write("<br>Field Name: " & ParamFlds.Item(intC).ParameterFieldName)
            'Response.Write("<br>Name: " & ParamFlds.Item(intC).Name)

            Select Case ParamFlds.Item(intC).ParameterType.ToString
                Case "ReportParameter"      '0
                    intRPT = intRPT + 1
                    Select Case ParamFlds.Item(intC).Name
                        Case "crCompName"
                            Call MyReport_Param(ParamFlds.Item(intC).Name, arrRPT(intRPT - 1), ParamFlds)
                        Case "crCompAddr1"
                        Case "crCompAddr2"
                        Case "crRegNum"
                        Case "crReportTitle"
                            Me.lblMessage.Text = arrRPT(intRPT - 1)
                            Call MyReport_Param(ParamFlds.Item(intC).Name, arrRPT(intRPT - 1), ParamFlds)
                        Case "crReportTitle2"
                            Call MyReport_Param(ParamFlds.Item(intC).Name, arrRPT(intRPT - 1), ParamFlds)
                        Case Else
                    End Select

                Case "StoreProcedureParameter"      '1
                    intDB = intDB + 1
                    'Response.Write("<br>Database Parameter: " & intDB.ToString & " - Name: " & ParamFlds.Item(intC).Name & " - New Value: " & arrDB(intDB - 1))
                    Call MyReport_Param(ParamFlds.Item(intC).Name, arrDB(intDB - 1), ParamFlds)
                Case "QueryParameter"       '2
                Case "ConnectionParameter"  '3
                Case Else

            End Select

        Next


        'Setting Report Parameter field info with parameter collection object
        Me.CrystalReportViewer1.ParameterFieldInfo = ParamFlds

        ' export the document to the temporary file.
        'crdoc.Export()


        With Me.CrystalReportViewer1
            .EnableDatabaseLogonPrompt = False
            .EnableParameterPrompt = False
            .ReuseParameterValuesOnRefresh = True

            .DisplayGroupTree = True
            .DisplayPage = True
            '.HasRefreshButton = True
            .HasPrintButton = True

            .EnableViewState = True

            .HasCrystalLogo = False
            '.Zoom(100)

            '.RefreshReport()
            .DataBind()

        End With


        ParamFlds = Nothing

        'crdoc.Close()
        'crdoc = Nothing

    End Sub

    Protected Sub MyShow_Report_New(ByVal doc As ReportDocument, ByVal fileName As String, ByVal strProcedureName As String)

        Dim crTableLogOnInfo As TableLogOnInfo = New TableLogOnInfo()
        Dim crConnectionInfo As CrystalDecisions.Shared.ConnectionInfo = New CrystalDecisions.Shared.ConnectionInfo()
        Dim crDatabase As CrystalDecisions.CrystalReports.Engine.Database
        Dim crTables As CrystalDecisions.CrystalReports.Engine.Tables

        doc = New ReportDocument()
        doc.Load(fileName)


        crConnectionInfo.ServerName = "HP-PC"
        crConnectionInfo.DatabaseName = "ProgrammingSamples"
        crConnectionInfo.UserID = "kefanyagei"
        crConnectionInfo.Password = "kefa@#12"
        crConnectionInfo.Type = ConnectionInfoType.SQL
        crConnectionInfo.IntegratedSecurity = False

        crDatabase = doc.Database
        crTables = crDatabase.Tables

        Dim conn As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("ConnectionString").ToString())

        Dim cmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("dbo." + strProcedureName, conn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim adpt As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter(cmd)

        Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
        adpt.Fill(dataSet, "Customers")

        For Each crTable As CrystalDecisions.CrystalReports.Engine.Table In crTables

            crTableLogOnInfo = crTable.LogOnInfo
            crTableLogOnInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogOnInfo)

        Next

        doc.SetDataSource(dataSet.Tables(0))
        CrystalReportViewer1.ReportSource = doc

    End Sub

    Private Sub MyCR_Sort_Rtn(ByVal myCR_Doc As ReportDocument)
        Dim mySortFields As SortFields = myCR_Doc.DataDefinition.SortFields
        Dim firstSortField As SortField = mySortFields(0)
        firstSortField.SortDirection = [Shared].SortDirection.AscendingOrder
        'firstSortField.SortDirection = [Shared].SortDirection.DescendingOrder
        Me.CrystalReportViewer1.ReportSource = myCR_Doc

    End Sub

    Private Sub MyReport_Param(ByVal Param_Name As String, ByVal Param_Value As String, ByVal rptPrm_Fields As CrystalDecisions.Shared.ParameterFields)

        Try
            Dim rptPrm_Fld As CrystalDecisions.Shared.ParameterField
            rptPrm_Fld = New CrystalDecisions.Shared.ParameterField
            rptPrm_Fld.ParameterFieldName = Param_Name

            Dim rptDiscrete_Val As CrystalDecisions.Shared.ParameterDiscreteValue
            rptDiscrete_Val = New CrystalDecisions.Shared.ParameterDiscreteValue
            rptDiscrete_Val.Value = Param_Value

            rptPrm_Fld.CurrentValues.Add(rptDiscrete_Val)
            rptPrm_Fields.Add(rptPrm_Fld)

            rptDiscrete_Val = Nothing
            rptPrm_Fld = Nothing

            'Me.lblMessage.Text = Me.lblMessage.Text & "<BR /> Setting report parameters successful - " & Param_Name & " - " & Param_Value & "<BR />"
            Exit Sub

        Catch ex As Exception
            Response.Write("<br/>Error while setting report parameter: " & Param_Name & " - " & Param_Value & "<br />" & "Reason: " & ex.Message.ToString & "<br />")
            'Me.lblMsg.Text = Me.lblMsg.Text & "<BR /> Error while setting report parameter: " & Param_Name & " - " & Param_Value & "<br />" & "Reason: " & ex.Message.ToString & "<br />"
            Exit Sub
        End Try

    End Sub

    Private Sub ModifyReport()

        '    'Imports CrystalDecisions.ReportAppServer.ClientDoc
        '    'Imports CrystalDecisions.ReportAppServer.Controllers
        '    'Imports CrystalDecisions.ReportAppServer.DataDefModel


        '    Dim myReportClientDocument As ISCDReportClientDocument = _
        '        hierarchicalGroupingReport.ReportClientDocument
        '    Dim myTables As Tables = _
        '        myReportClientDocument.DatabaseController.Database.Tables
        '    Dim myTable As CrystalDecisions.ReportAppServer.DataDefModel.Table
        '    myTable = CType(myTables(0), CrystalDecisions.ReportAppServer.DataDefModel.Table)
        '    Dim myFields As Fields = myTable.DataFields

        '    Dim myField As ISCRField
        '    For Each myField In myFields
        '        Dim myResultFieldController As ResultFieldController = _
        '            myReportClientDocument.DataDefController.ResultFieldController
        '        Try
        '            myResultFieldController.Remove(myField)
        '            Exit For
        '        Catch
        '        End Try
        '    Next
    End Sub


    Public Shared Sub SetCrystalLogin(ByVal sServer As String, ByVal sCompanyDB As String, ByVal sUser As String, ByVal sPassword As String, _
   ByRef oRpt As CrystalDecisions.CrystalReports.Engine.ReportDocument)

        Dim oDB As CrystalDecisions.CrystalReports.Engine.Database = oRpt.Database
        Dim oTables As CrystalDecisions.CrystalReports.Engine.Tables = oDB.Tables
        Dim oLogonInfo As CrystalDecisions.Shared.TableLogOnInfo

        Dim oConnectInfo As CrystalDecisions.Shared.ConnectionInfo = New CrystalDecisions.Shared.ConnectionInfo()

        oConnectInfo.DatabaseName = sCompanyDB
        oConnectInfo.ServerName = sServer
        oConnectInfo.UserID = sUser
        oConnectInfo.Password = sPassword

        ' Set the logon credentials for all tables
        For Each oTable As CrystalDecisions.CrystalReports.Engine.Table In oTables
            oLogonInfo = oTable.LogOnInfo
            oLogonInfo.ConnectionInfo = oConnectInfo
            oTable.ApplyLogOnInfo(oLogonInfo)
        Next

        ' Check for subreports
        Dim oSections As CrystalDecisions.CrystalReports.Engine.Sections
        Dim oSection As CrystalDecisions.CrystalReports.Engine.Section
        Dim oRptObjs As CrystalDecisions.CrystalReports.Engine.ReportObjects
        Dim oRptObj As CrystalDecisions.CrystalReports.Engine.ReportObject
        Dim oSubRptObj As CrystalDecisions.CrystalReports.Engine.SubreportObject
        Dim oSubRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        oSections = oRpt.ReportDefinition.Sections
        For Each oSection In oSections
            oRptObjs = oSection.ReportObjects
            For Each oRptObj In oRptObjs

                If oRptObj.Kind = CrystalDecisions.Shared.ReportObjectKind.SubreportObject Then

                    ' This is a subreport so set the logon credentials for this report's tables
                    oSubRptObj = CType(oRptObj, CrystalDecisions.CrystalReports.Engine.SubreportObject)
                    ' Open the subreport
                    oSubRpt = oSubRptObj.OpenSubreport(oSubRptObj.SubreportName)

                    oDB = oSubRpt.Database
                    oTables = oDB.Tables

                    For Each oTable As CrystalDecisions.CrystalReports.Engine.Table In oTables
                        oLogonInfo = oTable.LogOnInfo
                        oLogonInfo.ConnectionInfo = oConnectInfo
                        oTable.ApplyLogOnInfo(oLogonInfo)
                    Next

                End If

            Next
        Next
        oRpt.Refresh()

    End Sub

    Private Sub SetDBTableLogon(ByVal objConnInfo As CrystalDecisions.Shared.ConnectionInfo, ByVal objCRV1 As CrystalDecisions.Web.CrystalReportViewer)

        On Error GoTo Err_Rtn

        Dim myTableLogOnInfos As CrystalDecisions.Shared.TableLogOnInfos = Nothing

        myTableLogOnInfos = New CrystalDecisions.Shared.TableLogOnInfos
        myTableLogOnInfos = objCRV1.LogOnInfo

        'objCRV1.LogOnInfo.Clear()

        Dim myTableLogOnInfoN As CrystalDecisions.Shared.TableLogOnInfo
        For Each myTableLogOnInfoN In myTableLogOnInfos
            'Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo()
            myTableLogOnInfoN.ConnectionInfo = objConnInfo

            'myTableLogOnInfos.Add(myTableLogOnInfoN)

        Next
        'objCRV1.LogOnInfo = myTableLogOnInfos
        Exit Sub

Err_Rtn:
        Response.Write("<br />*** Table logon error. <br />Error: " & Err.Number & " - " & Err.Description & "<br>")
        Err.Clear()

    End Sub


    Private Sub SetDBLogon_Info(ByVal myConnectionInfo As CrystalDecisions.Shared.ConnectionInfo, ByVal myReportDocument As CrystalDecisions.CrystalReports.Engine.ReportDocument)
        On Error GoTo Err_Rtn

        'default
        '====================================================
        'Table Name: ABSSP_POLYTAB_LIST 
        'Table Location: Proc(ABSSP_POLYTAB_LIST)

        'Table LogonInfo Name: Proc(ABSSP_POLYTAB_LIST)

        '====================================================
        '"ABSWEPAY"."dbo"."ABSSP_POLYTAB_LIST";1 '', ''

        'Table Name: ABSSP_POLYTAB_LIST
        'Table Location : Proc(ABSSP_POLYTAB_LIST)

        '{CALL "ABSWEPAY"."dbo"."ABSSP_POLYTAB_LIST";1(NULL, NULL)}

        'new
        'Table Name: ABSSP_POLYTAB_LIST;1 
        'Table Location: ABSSP_POLYTAB_LIST;1

        Dim myTables As CrystalDecisions.CrystalReports.Engine.Tables = myReportDocument.Database.Tables

        For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
            Dim myTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo = myTable.LogOnInfo

            'Response.Write("<br />Default Value")
            'Response.Write("<br />Table Name: " & myTable.Name)
            'Response.Write("<br />Table Location: " & myTable.Location)
            'Response.Write("<br />Table LogonInfo Name: " & myTableLogonInfo.TableName)

            'myConnectionInfo.ServerName = CStr(Trim(strDB_Srv))
            'myConnectionInfo.DatabaseName = CStr(Trim(strDB_Name))
            'myConnectionInfo.Password = CStr(Trim(strDB_PWD))
            'myConnectionInfo.UserID = CStr(Trim(strDB_UID))

            'myConnectionInfo.Type = ConnectionInfoType.SQL
            'myConnectionInfo.Type = ConnectionInfoType.CRQE

            myTableLogonInfo.ConnectionInfo = myConnectionInfo

            'myTableLogonInfo.ConnectionInfo.ServerName = myConnectionInfo.ServerName ' //physical server name
            'myTableLogonInfo.ConnectionInfo.DatabaseName = myConnectionInfo.DatabaseName
            'myTableLogonInfo.ConnectionInfo.UserID = myConnectionInfo.UserID
            'myTableLogonInfo.ConnectionInfo.Password = myConnectionInfo.Password

            'myTableLogonInfo.ConnectionInfo.IntegratedSecurity = myConnectionInfo.IntegratedSecurity
            'myTableLogonInfo.ConnectionInfo.AllowCustomConnection = myConnectionInfo.AllowCustomConnection


            'ok
            'myTableLogonInfo.TableName = myTable.Name
            'myTableLogonInfo.TableName = "ABSSP_POLYTAB_LIST;1"

            'ok
            'myTable.Location = "ABSWEPAY.dbo.ABSSP_POLYTAB_LIST;1"

            'ok
            'myTable.Location = "ABSWEPAY.dbo.ABSSP_POLYTAB_LIST"

            'ok
            'myTable.Location = "ABSSP_POLYTAB_LIST"
            'myTable.Location = myTable.Name

            myTable.Location = myTable.Name

            'myTable.Location = "SPIL_PS_PRINT"
            'myTable.Location = "SPIL_PS_PRINT;1"
            'myTable.Location = "ABS_LIFE.dbo.SPIL_PS_PRINT"
            'myTable.Location = "ABS_LIFE.dbo.SPIL_PS_PRINT;1"

            'Response.Write("<hr/><br />New Value")
            'Response.Write("<br />Table Name: " & myTable.Name)
            'Response.Write("<br />Table Location: " & myTable.Location)
            'Response.Write("<br />Table LogonInfo Name: " & myTableLogonInfo.TableName)

            'myTable.Location = "{CALL ABSWEPAY.dbo.ABSSP_POLYTAB_LIST;1('NULL, NULL)}"
            'myTable.Location = "{CALL ABSWEPAY.dbo.ABSSP_POLYTAB_LIST;1('01/01/2012, '08/08/2012')}"

            myTable.ApplyLogOnInfo(myTableLogonInfo)


            'myTable.LogOnInfo.ConnectionInfo.DatabaseName = myTableLogonInfo.ConnectionInfo.DatabaseName
            'myTable.LogOnInfo.ConnectionInfo.ServerName = myTableLogonInfo.ConnectionInfo.ServerName
            'myTable.LogOnInfo.ConnectionInfo.UserID = myTableLogonInfo.ConnectionInfo.UserID
            'myTable.LogOnInfo.ConnectionInfo.Password = myTableLogonInfo.ConnectionInfo.Password

        Next
        Exit Sub

Err_Rtn:
        Response.Write("<br />*** Database logon error. <br />Error: " & Err.Number & " - " & Err.Description & "<br>")
        Err.Clear()
    End Sub

    Private Sub SetLogOnInfo(ByVal myReportViewer As CrystalDecisions.Web.CrystalReportViewer, ByVal userID As String, ByVal reportName As String, _
    ByVal password As String, ByVal databaseName As String, ByVal serverName As String, ByVal tableName As String)


        '   METHOD -1
        Dim logOnInfo As New TableLogOnInfo()
        logOnInfo = crdoc.Database.Tables.Item(tableName).LogOnInfo

        logOnInfo.ConnectionInfo.ServerName = serverName
        logOnInfo.ConnectionInfo.DatabaseName = databaseName
        logOnInfo.ConnectionInfo.UserID = userID
        logOnInfo.ConnectionInfo.Password = password
        logOnInfo.TableName = tableName
        crdoc.Database.Tables.Item(tableName).ApplyLogOnInfo(logOnInfo)


        '   METHOD -2
        Dim myLogOnInfo As New TableLogOnInfo()
        myLogOnInfo = crdoc.Database.Tables.Item(tableName).LogOnInfo

        Dim myConnectionInfo As New ConnectionInfo()
        'myConnectionInfo = crdoc.Database.Tables.Item(tableName).LogOnInfo.ConnectionInfo
        myConnectionInfo = myLogOnInfo.ConnectionInfo

        myConnectionInfo.DatabaseName = databaseName
        myConnectionInfo.ServerName = serverName
        myConnectionInfo.Password = password
        myConnectionInfo.UserID = userID
        crdoc.Database.Tables.Item(tableName).ApplyLogOnInfo(myLogOnInfo)


        '   METHOD -3
        Dim myLogOnInfoN As New TableLogOnInfo()

        '   OPTION -1
        myLogOnInfoN = crdoc.Database.Tables.Item(tableName).LogOnInfo
        '   OR USE OPTION -2
        myLogOnInfoN = myReportViewer.LogOnInfo(1)
        '   OR USE OPTION -3
        myLogOnInfoN = myReportViewer.LogOnInfo(tableName, reportName)

        Dim myIConnectionInfo As IConnectionInfo
        myIConnectionInfo = myLogOnInfoN.ConnectionInfo
        myIConnectionInfo.SetConnection(serverName, databaseName, True)
        myIConnectionInfo.SetLogon(userID, password)
        myLogOnInfo.TableName = tableName
        myLogOnInfo.ReportName = reportName

        Dim myTableLogOnInfos As New TableLogOnInfos
        myTableLogOnInfos.Add(myLogOnInfoN)
        myReportViewer.LogOnInfo = myTableLogOnInfos

    End Sub

    Private Function VerifyLogOnInfo(ByVal myReportViewer As CrystalDecisions.Web.CrystalReportViewer) As Boolean
        Dim result As Boolean = True
        Dim i As Integer
        For i = 1 To myReportViewer.LogOnInfo.Count
            Dim myLogOnInfo As TableLogOnInfo = myReportViewer.LogOnInfo(i)
            Dim myIConnectionInfo As IConnectionInfo = myLogOnInfo.ConnectionInfo
            If (myIConnectionInfo.UserID.Length = 0 Or myIConnectionInfo.Password.Length = 0 _
            Or myIConnectionInfo.ServerName.Length = 0 _
            Or myIConnectionInfo.DatabaseName.Length = 0) Then
                result = False
                Exit For
            End If
        Next
        VerifyLogOnInfo = result
    End Function


    Protected Sub btnFirstPage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnFirstPage.Click
        Me.txtPageNumber.Text = "1"
        Me.CrystalReportViewer1.ShowFirstPage()
    End Sub

    Protected Sub btnPrevPage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPrevPage.Click
        'Me.CrystalReportViewer1.ShowPreviousPage()
        If Val(Me.txtPageNumber.Text) = 0 Then
            Me.txtPageNumber.Text = 0
        End If

        If Val(Me.txtPageNumber.Text) <= Val(1) Then
            Me.txtPageNumber.Text = "1"
        Else
            Me.txtPageNumber.Text = Val(Me.txtPageNumber.Text) - 1
        End If
        Me.CrystalReportViewer1.ShowNthPage(Convert.ToInt32(Me.txtPageNumber.Text))
    End Sub

    Protected Sub btnNextPage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNextPage.Click
        'Me.CrystalReportViewer1.ShowNextPage()
        If Val(Me.txtPageNumber.Text) = 0 Then
            Me.txtPageNumber.Text = 0
        End If

        If Val(Me.txtPageNumber.Text) >= Val(Me.txtTotalPageNumber.Text) Then
            Me.txtPageNumber.Text = Val(Me.txtTotalPageNumber.Text).ToString
        Else
            Me.txtPageNumber.Text = Val(Me.txtPageNumber.Text) + 1
        End If
        Me.CrystalReportViewer1.ShowNthPage(Convert.ToInt32(Me.txtPageNumber.Text))
    End Sub

    Protected Sub btnLastPage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLastPage.Click
        Me.CrystalReportViewer1.ShowLastPage()
    End Sub

    Protected Sub CrystalReportViewer1_Navigate(ByVal source As Object, ByVal e As CrystalDecisions.Web.NavigateEventArgs) Handles CrystalReportViewer1.Navigate
        Dim intPage_CPN As Integer = e.CurrentPageNumber()
        Dim intPage_NPN As Integer = e.NewPageNumber()

        Me.txtPageNumber.Text = e.NewPageNumber.ToString

        'Me.CrystalReportViewer1.ShowNthPage(e.NewPageNumber)

        Me.lblMessage.Text = "New Page Number: " & intPage_NPN.ToString

    End Sub


    Protected Sub CrystalReportViewer1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles CrystalReportViewer1.Init

    End Sub
End Class
