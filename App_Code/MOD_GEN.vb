Imports Microsoft.VisualBasic
'Imports CrystalDecisions.CrystalReports
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine

Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Web.Mail
Imports System.IO
Imports System.Data
Imports System.Collections

Public Module MOD_GEN


    Public gnSQL As String
    Public gnTable As String

    Public gnCOMP_NAME As String
    Public gnComp_Addr1 As String
    Public gnComp_Addr2 As String
    Public gnComp_RegNum As String
    Public gnComp_TelNum As String
    Public gnComp_TelNum2 As String
    Public gnComp_TelNum3 As String

    Public gnCONN_STRING As String
    Public gnCONN_STRING_SQL As String

    Public Const gnAPP_REPORT_PATH As String = "h:\abs-web\webreports\"
    Public gnAPP_RPT_PATH As String


    Public Function DecryptNew(ByVal icText As String) As String
        Dim icLen As Integer
        Dim i As Long
        Dim icNewText As String
        Dim icChar As String

        icChar = ""
        icNewText = ""

        icLen = Len(icText)
        For i = 1 To icLen
            icChar = Mid(icText, i, 1)
            Select Case Asc(icChar)
                Case 192 To 217
                    icChar = Chr(Asc(icChar) - 127)
                Case 218 To 243
                    icChar = Chr(Asc(icChar) - 121)
                Case 244 To 253
                    icChar = Chr(Asc(icChar) - 196)
                Case 32
                    icChar = Chr(32)
            End Select
            icNewText = icNewText + icChar
        Next
        DecryptNew = icNewText
    End Function

    Public Function EncryptNew(ByVal icText As String) As String
        Dim icLen As Integer
        Dim icNewText As String
        Dim icChar As String
        Dim i As Long

        icChar = ""
        icNewText = ""

        icLen = Len(icText)
        For i = 1 To icLen
            icChar = Mid(icText, i, 1)
            Select Case Asc(icChar)
                Case 65 To 90
                    icChar = Chr(Asc(icChar) + 127)
                Case 97 To 122
                    icChar = Chr(Asc(icChar) + 121)
                Case 48 To 57
                    icChar = Chr(Asc(icChar) + 196)
                Case 32
                    icChar = Chr(32)
            End Select
            icNewText = icNewText + icChar
        Next
        EncryptNew = icNewText
    End Function


    Public Function gnApp_Path(Optional ByVal strType As String = "PHYSICAL_PATH") As String
        Dim strPhysical_Virtual_Path As String = "ERR"

        If RTrim(strType) = "PHYSICAL_PATH" Then
            '   Gets the physical drive path of the application directory for the application hosted in the current application domain.
            strPhysical_Virtual_Path = HttpRuntime.AppDomainAppPath
        ElseIf RTrim(strType) = "VIRTUAL_PATH" Then
            '   Gets the virtual path of the directory that contains the application hosted in the current application domain.
            strPhysical_Virtual_Path = HttpRuntime.AppDomainAppVirtualPath
        End If

        Return strPhysical_Virtual_Path

    End Function


    Public Sub gnASPNET_MsgBox(ByVal Message As String)

        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)

        System.Web.HttpContext.Current.Response.Write("alert(""" & Message & """)" & vbCrLf)

        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")

    End Sub


    Public Function gnGET_COMPANY_INFO(ByVal pvREC_ID As String, ByVal pvREC_NUM As String, ByVal pvCONN_STRING As String) As String

        gnCOMP_NAME = "AFRIK COMPUTERS LIMITED"
        gnComp_Addr1 = "76 Isolo Road, Mushin"
        gnComp_Addr2 = "Lagos, Nigeria."
        gnComp_RegNum = "RCXXXXX"
        gnComp_TelNum = "+2348023206554"
        gnComp_TelNum2 = "+2348024582289"
        gnComp_TelNum3 = ""

        gnCOMP_NAME = "Custodian and Allied Insurance Plc"
        gnCOMP_NAME = "Custodian Life Assurance Limited"
        'gnComp_Addr1 = "14B Keffi Street, South West, Ikoyi"
        'gnComp_Addr2 = "Lagos, Nigeria."
        'gnComp_RegNum = "RCXXXXX"
        'gnComp_TelNum = "270704-8"
        'gnComp_TelNum2 = "070CUSTODIAN"
        'gnComp_TelNum3 = ""


        gnTable = "ABSCOMPTAB"
        gnSQL = ""
        gnSQL = "SELECT TOP 1 * FROM " & gnTable
        gnSQL = gnSQL & " WHERE COMP_NUM = '" & RTrim(pvREC_NUM) & "'"
        gnSQL = gnSQL & " AND COMP_ID = '" & RTrim(pvREC_ID) & "'"

        Dim myreturn_SW As String
        myreturn_SW = "FALSE"

        Dim mystrCONN As String = CType("Provider=SQLOLEDB;" & pvCONN_STRING, String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(gnSQL, objOLEConn)

        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            objOLECmd = Nothing
            objOLEConn = Nothing

            myreturn_SW = "FALSE"
            Return myreturn_SW
            Exit Function
        End Try

        Dim objOLEDR As OleDbDataReader

        Try
            objOLEDR = objOLECmd.ExecuteReader()
            If (objOLEDR.Read()) Then
                'Save existing record

                gnCOMP_NAME = RTrim(objOLEDR("comp_name") & vbNullString).ToString
                gnComp_Addr1 = RTrim(objOLEDR("comp_addr1") & vbNullString).ToString
                gnComp_Addr2 = RTrim(objOLEDR("comp_addr2") & vbNullString).ToString

                myreturn_SW = gnCOMP_NAME
            End If

        Catch ex As Exception
            objOLEDR = Nothing
            objOLECmd = Nothing
            objOLEConn = Nothing

            myreturn_SW = "FALSE"
            Return myreturn_SW
            Exit Function

        End Try


        objOLEDR = Nothing

        objOLECmd.Dispose()
        objOLECmd = Nothing


        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Return myreturn_SW

    End Function

    Public Function gnGET_CONN_STRING(Optional ByVal pvApp_Type As String = "ABS_ALL") As String

        Dim mystrCONN_STRING As String = ""
        Dim mystrAPP_PATH As String = ""
        Dim mystrCONN_PATH As String = ""

        '   Gets the physical drive path of the application directory for the application hosted in the current application domain.
        mystrAPP_PATH = HttpRuntime.AppDomainAppPath
        If Right(RTrim(mystrAPP_PATH), 1) <> "\" Then
            mystrAPP_PATH = mystrAPP_PATH & "\"
        End If

        '   Gets the virtual path of the directory that contains the application hosted in the current application domain.
        'mystrAPP_PATH = HttpRuntime.AppDomainAppVirtualPath
        mystrCONN_PATH = "App_Data\LAppDB.txt"
        If Trim(pvApp_Type) = "ABS_RPT" Then
            mystrCONN_PATH = "App_Data\dbrpt.txt"

        End If

        Dim objFile As New System.IO.StreamReader(mystrAPP_PATH & mystrCONN_PATH)
        'mystrCONN_STRING = objFile.ReadToEnd()
        mystrCONN_STRING = objFile.ReadLine()
        objFile.Close()
        objFile.Dispose()
        objFile = Nothing

        'gnGET_CONN_STRING = mystrCONN_STRING

        Return mystrCONN_STRING

    End Function



    Public Function gnGET_REPORT_PATH() As String

        gnAPP_RPT_PATH = gnAPP_REPORT_PATH
        gnAPP_RPT_PATH = System.Configuration.ConfigurationManager.AppSettings("CR_RPT_PATH")
        Return gnAPP_RPT_PATH

    End Function

    Public Function gnGET_MS_ACCESS_PATH() As String

        Dim myMS_DB_PATH As String
        myMS_DB_PATH = ""

        '      "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyData.mdb;Jet OLEDB:Database Locking Mode=1;Mode=16" providerName="System.Data.OleDb" />
        'myMS_DB_PATH = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\AFRIK\RPT-DB\STAPP_DB_RPT.mdb;User ID=Admin;"

        '   for ODBC datasource
        'Dsn=ABS_ACCT_DSN_RPT;uid=Admin;pwd=xyz

        '   for OLE DB
        'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\AFRIK\RPT-DB\STAPP_DB_RPT.mdb;User ID=Admin;Password=xyz;OLE DB Services=-13;Encrypt Password=False;Mask Password=False;Jet OLEDB:Database Locking Mode=1;Mode=16
        'Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\AFRIK\RPT-DB\STAPP_DB_RPT.mdb;User ID=Admin;Password=xyz;OLE DB Services=-13;Encrypt Password=False;Mask Password=False;Jet OLEDB:Database Locking Mode=1;Mode=16


        'myMS_DB_PATH = ConfigurationSettings.AppSettings("APPCONN")
        myMS_DB_PATH = System.Configuration.ConfigurationManager.AppSettings("MS_DB_PATH")

        Return myMS_DB_PATH

    End Function


    Public Function gnGET_RATE(ByVal pvstr_GET_WHAT As String, ByVal pvstr_MODULE As String, ByVal pvstr_RATE_CODE As String, ByVal pvstr_PRODUCT_REF_CODE As String, ByVal pvstr_PERIOD As String, ByVal pvstr_AGE As String, Optional ByVal pvCtr_Label As Label = Nothing, Optional ByVal pvRef_Misc As TextBox = Nothing, Optional ByVal pvRef_Misc_02 As TextBox = Nothing) As String

        Dim mystr_conn As String = ""
        Dim mystr_Table As String = ""
        Dim mystr_SQL As String = ""
        Dim mystr_Key As String = ""

        Dim myint_C As Integer = 0
        Dim myRetValue As String = "0"

        'Dim myobj_dt As New System.Data.DataTable
        'Dim myobj_ds As New System.Data.DataSet

        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn

        Dim myole_CONN As OleDbConnection = Nothing
        myole_CONN = New OleDbConnection(mystr_conn)

        Try
            ' Open connection
            myole_CONN.Open()

        Catch ex As Exception
            myole_CONN = Nothing

            If pvCtr_Label IsNot Nothing Then
                If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                    pvCtr_Label.Text = "Error. " & ex.Message.ToString
                End If
            End If

            Return "ERR_CON"
            Exit Function
        End Try


        mystr_SQL = ""
        mystr_SQL = ""

        Dim myole_CMD As OleDbCommand = New OleDb.OleDbCommand()
        myole_CMD.Connection = myole_CONN


        Select Case Trim(pvstr_GET_WHAT)
            Case "GET_IL_PREMIUM_RATE"
                mystr_SQL = "SPIL_GET_PREM_RATE"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim(pvstr_MODULE)
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = RTrim(pvstr_RATE_CODE)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = RTrim(pvstr_PRODUCT_REF_CODE)
                myole_CMD.Parameters.Add("@p04", OleDbType.VarChar, 4).Value = RTrim(pvstr_PERIOD)
                myole_CMD.Parameters.Add("@p05", OleDbType.VarChar, 4).Value = RTrim(pvstr_AGE)
            Case "GET_AN_PREMIUM_RATE"
                mystr_SQL = "SPAN_GET_PREM_RATE"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim(pvstr_MODULE)
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = RTrim(pvstr_RATE_CODE)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = RTrim(pvstr_PRODUCT_REF_CODE)
                myole_CMD.Parameters.Add("@p04", OleDbType.VarChar, 4).Value = RTrim(pvstr_PERIOD)
                myole_CMD.Parameters.Add("@p05", OleDbType.VarChar, 4).Value = RTrim(pvstr_AGE)
            Case "GET_IL_EXCHANGE_RATE"
                mystr_SQL = "SPIL_GET_EXCHANGE_RATE"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim(pvstr_MODULE)
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = RTrim(pvstr_RATE_CODE)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = RTrim(pvstr_PRODUCT_REF_CODE)

            Case "GET_IL_MOP_FACTOR"
                mystr_SQL = "SPIL_GET_MOP_FACTOR"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim(pvstr_MODULE)
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = RTrim(pvstr_RATE_CODE)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = RTrim(pvstr_PRODUCT_REF_CODE)

            Case Else
                myole_CMD = Nothing
                myole_CONN = Nothing

                If pvCtr_Label IsNot Nothing Then
                    If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                        pvCtr_Label.Text = "Error. Invalid parameter: " & pvstr_GET_WHAT.ToString
                    End If
                End If
                Return "ERR_PARAM"
                Exit Function

        End Select

        ' execute the command to obtain the resultant dataset
        Dim myole_DR As OleDbDataReader

        Try

            myole_DR = myole_CMD.ExecuteReader()

            ' with the new data reader parse values and place into the return variable
            If (myole_DR.Read()) Then
                'Me.UserCode.Text = Me.UserName.Text & " - " & oleDR("pwd_code").ToString & vbNullString
                pvCtr_Label.Text = string.Empty
                Select Case Trim(pvstr_GET_WHAT)
                    Case "GET_AN_PREMIUM_RATE"
                        myRetValue = RTrim(myole_DR("TBIL_PRM_RT_RATE") & vbNullString).ToString
                        If pvRef_Misc Is Nothing Then
                        Else
                            pvRef_Misc.Text = RTrim(CType(myole_DR("TBIL_PRM_RT_PER") & vbNullString, String))
                        End If
                    Case "GET_IL_PREMIUM_RATE"
                        myRetValue = RTrim(myole_DR("TBIL_PRM_RT_RATE") & vbNullString).ToString
                        If pvRef_Misc Is Nothing Then
                        Else
                            pvRef_Misc.Text = RTrim(CType(myole_DR("TBIL_PRM_RT_PER") & vbNullString, String))
                        End If

                    Case "GET_IL_EXCHANGE_RATE"
                        myRetValue = RTrim(myole_DR("TBIL_EXCH_RATE") & vbNullString).ToString

                    Case "GET_IL_MOP_FACTOR"
                        myRetValue = RTrim(myole_DR("TBIL_MOP_RATE") & vbNullString).ToString
                        If pvRef_Misc Is Nothing Then
                        Else
                            pvRef_Misc.Text = RTrim(myole_DR("TBIL_MOP_TYPE_DESC") & vbNullString).ToString
                        End If
                        If pvRef_Misc_02 Is Nothing Then
                        Else
                            pvRef_Misc_02.Text = RTrim(myole_DR("TBIL_MOP_DIVIDE") & vbNullString).ToString
                        End If

                    Case Else
                        myRetValue = "ERR_PARAM"
                        If pvCtr_Label IsNot Nothing Then
                            If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                                pvCtr_Label.Text = "Error. Invalid parameter: " & pvstr_GET_WHAT.ToString
                            End If
                        End If
                End Select

                myole_DR.Close()
                myole_CMD.Dispose()
            Else
                myRetValue = "ERR_RNF"
                If pvCtr_Label IsNot Nothing Then
                    If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                        pvCtr_Label.Text = "Record not found for parameters supplied...E.G. 'duration/term' and 'age'"
                    End If
                End If
            End If

        Catch ex As Exception
            '   'Throw ex
            myRetValue = "ERR"
            If pvCtr_Label IsNot Nothing Then
                If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                    pvCtr_Label.Text = "Error. " & ex.Message.ToString
                End If
            End If
        Finally
        End Try


        'myole_DA.Dispose()

        Try
            ' Close connection
            myole_CONN.Close()

        Catch ex As Exception

        End Try

        'myobj_ds = Nothing
        'myole_DA = Nothing

        myole_DR = Nothing
        myole_CMD = Nothing
        myole_CONN = Nothing

        Return myRetValue

    End Function

    Public Function gnGET_RECORD(ByVal pvstr_GET_WHAT As String, ByVal pvstr_Key01 As String, ByVal pvstr_Key02 As String, ByVal pvstr_Key03 As String) As ICollection

        Dim myobj_AL As New ArrayList()
        'oAL = gnValidat_Codes(mystrID, RTrim(pvPol), objOLEConn)
        'If oAL.Item(0) = "TRUE" Then
        '    'Destroy i.e remove the array list object from memory
        '    oAL = Nothing
        '    GoTo gnGet_Loop_Start
        'Else
        '    oAL = Nothing
        'End If

        Dim mystr_conn As String = ""
        Dim mystr_Table As String = ""
        Dim mystr_SQL As String = ""
        Dim mystr_Key As String = ""

        Dim myint_C As Integer = 0

        'Dim myobj_dt As New System.Data.DataTable
        'Dim myobj_ds As New System.Data.DataSet

        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn

        Dim myole_CONN As OleDbConnection = Nothing
        myole_CONN = New OleDbConnection(mystr_conn)

        Try
            ' Open connection
            myole_CONN.Open()

        Catch ex As Exception
            myobj_AL.Insert(0, "ERROR")
            myobj_AL.Insert(1, RTrim(ex.Message.ToString))
            myobj_AL.Insert(2, RTrim("Please contact your System Administrator"))

            myole_CONN = Nothing

            Return myobj_AL
            Exit Function
        End Try

        mystr_Key = " P1=" & pvstr_Key01
        If Trim(pvstr_Key02) <> "" Then
            mystr_Key = mystr_Key & " P2=" & pvstr_Key02
        End If
        If Trim(pvstr_Key03) <> "" Then
            mystr_Key = mystr_Key & " P3=" & pvstr_Key03
        End If

        ' Select the data from table.

        mystr_SQL = ""
        'Select Case Trim(pvstr_GET_WHAT)
        '    Case "GET_POLICY_BY_FILE_NO"
        '        mystr_Table = "TBIL_POLICY_DET"
        '        mystr_SQL = mystr_SQL & "select top 1 * from " & mystr_Table
        '        mystr_SQL = mystr_SQL & " where TBIL_POLY_FILE_NO = '" & RTrim(pvstr_Key01) & "'"
        'End Select

        'Dim myole_DA As OleDbDataAdapter
        'myole_DA = New OleDbDataAdapter(mystr_SQL, myole_CONN)

        'myole_DA.Fill(myobj_dt)
        ''myole_DA.Fill(myobj_ds)

        'myint_C = myobj_dt.Rows.Count
        'For myint_C = 0 To myobj_dt.Rows.Count - 1
        '    Console.Write("<br/>Row: " & myint_C & " Col: 1" & myobj_dt.Rows(myint_C)(0).ToString)
        'Next

        mystr_SQL = ""

        Dim myole_CMD As OleDbCommand = New OleDb.OleDbCommand()
        myole_CMD.Connection = myole_CONN

        Select Case Trim(pvstr_GET_WHAT)
            Case "GET_POLICY_BY_FILE_NO"
                mystr_SQL = "SPIL_GET_POLICY_DET"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim("FIL")
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 40).Value = RTrim(pvstr_Key01)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 18).Value = RTrim("0")

            Case "GET_ANNUITY_BY_FILE_NO"
                mystr_SQL = "SPAN_GET_POLICY_DET"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim("FIL")
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 40).Value = RTrim(pvstr_Key01)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 18).Value = RTrim("0")
            Case "GET_POLICY_BY_QUOTATION_NO"
                mystr_SQL = "SPIL_GET_POLICY_DET"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim("QUO")
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 40).Value = RTrim(pvstr_Key01)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 18).Value = RTrim("0")
            Case "GET_POLICY_BY_POLICY_NO"
                mystr_SQL = "SPIL_GET_POLICY_DET"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim("POL")
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 40).Value = RTrim(pvstr_Key01)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 18).Value = RTrim("0")

                '   FOR GROUP LIFE
            Case "GET_GL_POLICY_BY_FILE_NO"
                mystr_SQL = "SPGL_GET_POLICY_DET"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim("FIL")
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 40).Value = RTrim(pvstr_Key01)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 18).Value = RTrim("0")
            Case "GET_GL_POLICY_BY_QUOTATION_NO"
                mystr_SQL = "SPGL_GET_POLICY_DET"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim("QUO")
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 40).Value = RTrim(pvstr_Key01)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 18).Value = RTrim("0")
            Case "GET_GL_POLICY_BY_POLICY_NO"
                mystr_SQL = "SPGL_GET_POLICY_DET"
                myole_CMD.CommandType = CommandType.StoredProcedure
                myole_CMD.CommandText = mystr_SQL
                myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim("POL")
                myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 40).Value = RTrim(pvstr_Key01)
                myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 18).Value = RTrim("0")

            Case Else
                myobj_AL.Insert(0, "ERROR")
                myobj_AL.Insert(1, RTrim("Invalid parameter: " & pvstr_GET_WHAT.ToString))
                myobj_AL.Insert(2, RTrim("Please contact your System Administrator"))

                myole_CMD = Nothing
                myole_CONN = Nothing

                Return myobj_AL
                Exit Function

        End Select

        ' execute the command to obtain the resultant dataset
        Dim myole_DR As OleDbDataReader

        Try

            myole_DR = myole_CMD.ExecuteReader()

            ' with the new data reader parse values and place into the return variable
            If (myole_DR.Read()) Then
                'Me.UserCode.Text = Me.UserName.Text & " - " & oleDR("pwd_code").ToString & vbNullString

                myobj_AL.Insert(0, "TRUE")
                Select Case Trim(pvstr_GET_WHAT)
                    Case "GET_POLICY_BY_FILE_NO", "GET_POLICY_BY_QUOTATION_NO", "GET_POLICY_BY_POLICY_NO"
                        With myobj_AL
                            .Insert(1, RTrim(myole_DR("TBIL_POLY_REC_ID").ToString & vbNullString))
                            .Insert(2, RTrim(myole_DR("TBIL_POLY_FILE_NO").ToString & vbNullString))
                            .Insert(3, RTrim(myole_DR("TBIL_POLY_PROPSAL_NO").ToString & vbNullString))
                            .Insert(4, RTrim(myole_DR("TBIL_POLY_POLICY_NO").ToString & vbNullString))
                            .Insert(5, RTrim(myole_DR("TBIL_PRDCT_DTL_CAT").ToString & vbNullString))
                            .Insert(6, RTrim(myole_DR("TBIL_POLY_PRDCT_CD").ToString & vbNullString))
                            .Insert(7, RTrim(myole_DR("TBIL_POLY_PLAN_CD").ToString & vbNullString))
                            .Insert(8, RTrim(myole_DR("TBIL_POLY_COVER_CD").ToString & vbNullString))

                            If IsDate(myole_DR("TBIL_POLY_ASSRD_BDATE")) Then
                                .Insert(9, Format(CType(myole_DR("TBIL_POLY_ASSRD_BDATE"), DateTime), "dd/MM/yyyy"))
                            Else
                                .Insert(9, "")
                            End If
                            .Insert(10, RTrim(myole_DR("TBIL_POLY_ASSRD_AGE").ToString & vbNullString))

                            If IsDate(myole_DR("TBIL_POLY_PRPSAL_ISSUE_DATE")) Then
                                .Insert(11, Format(CType(myole_DR("TBIL_POLY_PRPSAL_ISSUE_DATE"), DateTime), "dd/MM/yyyy"))
                            Else
                                .Insert(11, "")
                            End If
                            .Insert(11, RTrim(myole_DR("TBIL_FUN_COVER_ID").ToString & vbNullString))
                            .Insert(12, RTrim(myole_DR("TBIL_POL_PRM_RT_TAB_FIX").ToString & vbNullString))
                            .Insert(13, RTrim(myole_DR("TBIL_POL_PRM_RT_APPLIED_ON").ToString & vbNullString))
                            .Insert(14, RTrim(myole_DR("TBIL_POL_PRM_RATE_CD").ToString & vbNullString))
                            .Insert(15, RTrim(myole_DR("TBIL_POL_PRM_ENROL_NO").ToString & vbNullString))
                            .Insert(16, RTrim(myole_DR("TBIL_POL_PRM_MODE_PAYT").ToString & vbNullString))
                            .Insert(17, RTrim(myole_DR("TBIL_POL_PRM_ANN_CONTRIB_LC").ToString & vbNullString))

                            .Insert(18, RTrim(myole_DR("TBIL_POLY_PROPSL_ACCPT_STATUS").ToString & vbNullString))

                        End With

                    Case "GET_ANNUITY_BY_FILE_NO", "GET_ANNUITY_BY_QUOTATION_NO", "GET_ANNUITY_BY_POLICY_NO"
                        With myobj_AL
                            .Insert(1, RTrim(myole_DR("TBIL_ANN_POLY_REC_ID").ToString & vbNullString))
                            .Insert(2, RTrim(myole_DR("TBIL_ANN_POLY_FILE_NO").ToString & vbNullString))
                            .Insert(3, RTrim(myole_DR("TBIL_ANN_POLY_PROPSAL_NO").ToString & vbNullString))
                            .Insert(4, RTrim(myole_DR("TBIL_ANN_POLY_POLICY_NO").ToString & vbNullString))
                            .Insert(5, RTrim(myole_DR("TBIL_PRDCT_DTL_CAT").ToString & vbNullString))
                            .Insert(6, RTrim(myole_DR("TBIL_ANN_POLY_PRDCT_CD").ToString & vbNullString))
                            .Insert(7, RTrim(myole_DR("TBIL_ANN_POLY_PLAN_CD").ToString & vbNullString))
                            .Insert(8, RTrim(myole_DR("TBIL_ANN_POLY_COVER_CD").ToString & vbNullString))

                            If IsDate(myole_DR("TBIL_ANN_POLY_ASSRD_BDATE")) Then
                                .Insert(9, Format(CType(myole_DR("TBIL_ANN_POLY_ASSRD_BDATE"), DateTime), "dd/MM/yyyy"))
                            Else
                                .Insert(9, "")
                            End If
                            .Insert(10, RTrim(myole_DR("TBIL_ANN_POLY_ASSRD_AGE").ToString & vbNullString))

                            If IsDate(myole_DR("TBIL_ANN_POLY_PRPSAL_ISSUE_DATE")) Then
                                .Insert(11, Format(CType(myole_DR("TBIL_ANN_POLY_PRPSAL_ISSUE_DATE"), DateTime), "dd/MM/yyyy"))
                            Else
                                .Insert(11, "")
                            End If
                            .Insert(11, RTrim(myole_DR("TBIL_FUN_COVER_ID").ToString & vbNullString))
                            .Insert(12, RTrim(myole_DR("TBIL_ANN_POL_PRM_RT_TAB_FIX").ToString & vbNullString))
                            .Insert(13, RTrim(myole_DR("TBIL_ANN_POL_PRM_RT_APPLIED_ON").ToString & vbNullString))
                            .Insert(14, RTrim(myole_DR("TBIL_ANN_POL_PRM_RATE_CD").ToString & vbNullString))
                            .Insert(15, RTrim(myole_DR("TBIL_ANN_POL_PRM_ENROL_NO").ToString & vbNullString))
                            .Insert(16, RTrim(myole_DR("TBIL_ANN_POL_PRM_MODE_PAYT").ToString & vbNullString))
                            '.Insert(17, RTrim(myole_DR("TBIL_ANN_POL_PRM_ANN_CONTRIB_LC").ToString & vbNullString))

                            .Insert(18, RTrim(myole_DR("TBIL_ANN_POLY_PROPSL_ACCPT_STATUS").ToString & vbNullString))

                        End With


                    Case "GET_GL_POLICY_BY_FILE_NO", "GET_GL_POLICY_BY_QUOTATION_NO", "GET_GL_POLICY_BY_POLICY_NO"
                        With myobj_AL
                            .Insert(1, RTrim(myole_DR("TBIL_POLY_REC_ID").ToString & vbNullString))
                            .Insert(2, RTrim(myole_DR("TBIL_POLY_FILE_NO").ToString & vbNullString))
                            .Insert(3, RTrim(myole_DR("TBIL_POLY_PROPSAL_NO").ToString & vbNullString))
                            .Insert(4, RTrim(myole_DR("TBIL_POLY_POLICY_NO").ToString & vbNullString))
                            .Insert(5, RTrim(myole_DR("TBIL_PRDCT_DTL_CAT").ToString & vbNullString))
                            .Insert(6, RTrim(myole_DR("TBIL_POLY_PRDCT_CD").ToString & vbNullString))
                            .Insert(7, RTrim(myole_DR("TBIL_POLY_PLAN_CD").ToString & vbNullString))
                            .Insert(8, RTrim(myole_DR("TBIL_POLY_COVER_CD").ToString & vbNullString))

                            If IsDate(myole_DR("TBIL_POLY_ASSRD_BDATE")) Then
                                .Insert(9, Format(CType(myole_DR("TBIL_POLY_ASSRD_BDATE"), DateTime), "dd/MM/yyyy"))
                            Else
                                .Insert(9, "")
                            End If
                            .Insert(10, RTrim(myole_DR("TBIL_POLY_ASSRD_AGE").ToString & vbNullString))

                            If IsDate(myole_DR("TBIL_POLY_PRPSAL_ISSUE_DATE")) Then
                                .Insert(11, Format(CType(myole_DR("TBIL_POLY_PRPSAL_ISSUE_DATE"), DateTime), "dd/MM/yyyy"))
                            Else
                                .Insert(11, "")
                            End If
                            .Insert(11, RTrim(myole_DR("TBIL_FUN_COVER_ID").ToString & vbNullString))
                            .Insert(12, RTrim(myole_DR("TBIL_POL_PRM_RT_TAB_FIX").ToString & vbNullString))
                            .Insert(13, RTrim(myole_DR("TBIL_POL_PRM_RT_APPLIED_ON").ToString & vbNullString))
                            .Insert(14, RTrim(myole_DR("TBIL_POL_PRM_RATE_CD").ToString & vbNullString))
                            .Insert(15, RTrim(myole_DR("TBIL_POL_PRM_ENROL_NO").ToString & vbNullString))
                            .Insert(16, RTrim(myole_DR("TBIL_POL_PRM_MODE_PAYT").ToString & vbNullString))
                            .Insert(17, RTrim(myole_DR("TBIL_POL_PRM_ANN_CONTRIB_LC").ToString & vbNullString))

                            .Insert(18, RTrim(myole_DR("TBIL_POLY_PROPSL_ACCPT_STATUS").ToString & vbNullString))

                        End With

                    Case Else
                        myobj_AL.Insert(0, "ERROR")
                        myobj_AL.Insert(1, RTrim("Invalid parameter: " & pvstr_GET_WHAT.ToString))
                        myobj_AL.Insert(2, RTrim("Please contact your System Administrator"))
                End Select

                myole_DR.Close()
                myole_CMD.Dispose()
            Else
                myobj_AL.Insert(0, "FALSE")
                myobj_AL.Insert(1, RTrim("Sorry. The system cannot find record with IDs: " & mystr_Key))
                myobj_AL.Insert(2, RTrim("Please enter valid record id..."))
            End If

        Catch ex As Exception
            '   'Throw ex
            myobj_AL.Insert(0, "ERROR")
            myobj_AL.Insert(1, RTrim(ex.Message.ToString))
            myobj_AL.Insert(2, RTrim("Please contact your System Administrator"))
        Finally
        End Try


        'myole_DA.Dispose()

        Try
            ' Close connection
            myole_CONN.Close()

        Catch ex As Exception

        End Try

        'myobj_ds = Nothing
        'myole_DA = Nothing

        myole_DR = Nothing
        myole_CMD = Nothing
        myole_CONN = Nothing

        'Return myobj_dt
        Return myobj_AL

    End Function

    Public Function gnGET_RPT_YYYYMM(ByVal pvRpt_Date As String) As String
        Dim myStrMTH As String
        myStrMTH = ""
        myStrMTH = Right(RTrim(pvRpt_Date), 2)

        Select Case Right(RTrim(pvRpt_Date), 2)
            Case "01"
                myStrMTH = "JANUARY"
            Case "02"
                myStrMTH = "FEBRUARY"
            Case "03"
                myStrMTH = "MARCH"
            Case "04"
                myStrMTH = "APRIL"
            Case "05"
                myStrMTH = "MAY"
            Case "06"
                myStrMTH = "JUNE"
            Case "07"
                myStrMTH = "JULY"
            Case "08"
                myStrMTH = "AUGUST"
            Case "09"
                myStrMTH = "SEPTEMBER"
            Case "10"
                myStrMTH = "OCTOBER"
            Case "11"
                myStrMTH = "NOVEMBER"
            Case "12"
                myStrMTH = "DECEMBER"

        End Select

        Return myStrMTH & ", " & Left(LTrim(pvRpt_Date), 4)


    End Function


    Public Sub gnGET_SelectedItem(ByVal pvDDL_Control As DropDownList, ByVal pvCtr_Value As TextBox, ByVal pvCtr_Text As TextBox, Optional ByVal pvCtr_Label As Label = Nothing)
        Try

            If pvDDL_Control.SelectedIndex = -1 Or pvDDL_Control.SelectedIndex = 0 Or _
                pvDDL_Control.SelectedItem.Value = "" Or pvDDL_Control.SelectedItem.Value = "*" Then
                pvCtr_Value.Text = ""
                pvCtr_Text.Text = ""
            Else
                pvCtr_Value.Text = pvDDL_Control.SelectedItem.Value
                pvCtr_Text.Text = pvDDL_Control.SelectedItem.Text
            End If
        Catch ex As Exception
            If pvCtr_Label IsNot Nothing Then
                If TypeOf pvCtr_Label Is System.Web.UI.WebControls.Label Then
                    pvCtr_Label.Text = "Error. Reason: " & ex.Message.ToString
                End If
            End If
        End Try

    End Sub


    Public Function gnGet_Serial_File_Proposal_Policy(ByVal pvCODE As String, ByVal pvRef_ID As String, ByVal pvRef_Year As String, _
                                                      ByVal pvRef_Type As String, ByVal pvRef_Branch As String, _
                                                      ByVal pvRef_Class As String, ByVal pvRef_Risk As String, _
                                                      ByVal pvRef_Unit As String, ByVal pvRef_Prefix As String, ByVal pvRef_Surfix As String) As String

        Dim pvSerialNum As String
        Dim pvSQL As String

        Dim intC As Integer = 0
        pvSerialNum = "0"
        pvSQL = ""

        pvSerialNum = "ERR_ERR"

        Select Case UCase(RTrim(pvCODE))
            Case "GET_SN_IL_FIL_PRO_POL"
                Select Case RTrim(pvRef_ID)
                    Case "FIL", "PRO", "POL"
                        pvSQL = "SPIL_GET_UNDW_POL_SN"
                    Case Else
                        pvSerialNum = "PARAM_ERR"
                        Return pvSerialNum
                        Exit Function
                End Select

            Case "GET_SN_AN_FIL_PRO_POL"
                Select Case RTrim(pvRef_ID)
                    Case "FIL", "PRO", "POL"
                        pvSQL = "SPAN_GET_UNDW_POL_SN"
                    Case Else
                        pvSerialNum = "PARAM_ERR"
                        Return pvSerialNum
                        Exit Function
                End Select

            Case "GET_SN_GL_FIL_PRO_POL"
                Select Case RTrim(pvRef_ID)
                    Case "FIL", "PRO", "POL"
                        pvSQL = "SPGL_GET_UNDW_POL_SN"
                    Case Else
                        pvSerialNum = "PARAM_ERR"
                        Return pvSerialNum
                        Exit Function
                End Select

            Case Else
                pvSerialNum = "PARAM_ERR"
                Return pvSerialNum
                Exit Function

        End Select

        Dim mystrCONN As String
        'mystrCONN = CType(ConfigurationManager.AppSettings("APPCONN"), String)

        mystrCONN = gnGET_CONN_STRING()
        mystrCONN = "Provider=SQLOLEDB;" & mystrCONN

        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            objOLEConn = Nothing
            Return "DB_ERR"
            Exit Function

        End Try


        Dim objOLECmd As OleDbCommand = New OleDbCommand(pvSQL, objOLEConn)
        objOLECmd.CommandType = Data.CommandType.StoredProcedure
        objOLECmd.Parameters.Clear()

        Select Case pvRef_ID
            Case "FIL", "PRO", "POL"
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 5).Value = RTrim(pvRef_ID)
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 10).Value = RTrim(pvRef_Year)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 5).Value = RTrim(pvRef_Type)
                objOLECmd.Parameters.Add("p04", OleDbType.VarChar, 5).Value = RTrim(pvRef_Branch)
                objOLECmd.Parameters.Add("p05", OleDbType.VarChar, 5).Value = RTrim(pvRef_Class)
                objOLECmd.Parameters.Add("p06", OleDbType.VarChar, 10).Value = RTrim(pvRef_Risk)
                objOLECmd.Parameters.Add("p07", OleDbType.VarChar, 5).Value = RTrim(pvRef_Unit)
                objOLECmd.Parameters.Add("p08", OleDbType.VarChar, 20).Value = RTrim(pvRef_Prefix)
                objOLECmd.Parameters.Add("p09", OleDbType.VarChar, 20).Value = RTrim(pvRef_Surfix)
                objOLECmd.Parameters.Add("p10", OleDbType.VarChar, 50).Direction = Data.ParameterDirection.Output
                objOLECmd.Parameters.Add("p11", OleDbType.VarChar, 50).Direction = Data.ParameterDirection.Output

            Case Else
                objOLECmd = Nothing
                pvSerialNum = "PARAM_ERR"
                Return pvSerialNum
                Exit Function
        End Select


        'Try
        Select Case RTrim(pvRef_ID)
            Case "FIL", "PRO", "POL"
                intC = objOLECmd.ExecuteNonQuery()
                'pvSerialNum = CType(objOLECmd.Parameters("p10").Value & vbNullString, String)
                'Call gnASPNET_MsgBox("Serial No: " & pvSerialNum)
                pvSerialNum = CType(objOLECmd.Parameters("p11").Value & vbNullString, String)
                'Call gnASPNET_MsgBox("Ref Code: " & pvSerialNum)
                GoTo gnGet_SN_End
        End Select
        'Catch ex As Exception

        ' dispose of open objects
        'objOLECmd.Dispose()

        'objOLECmd = Nothing


        If objOLEConn.State = Data.ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        pvSerialNum = "ERR_ERR"
        Return pvSerialNum
        Exit Function
        'End Try


gnGet_SN_End:

        ' dispose of open objects
        objOLECmd.Dispose()


        If objOLEConn.State = Data.ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Return pvSerialNum

    End Function

    Public Function gnGet_Serial_No(ByVal pvCODE As String, ByVal pvRef_Type As String, _
                                    ByVal pvRef_Code_A As String, _
                                    ByVal pvRef_Code_B As String) As String

        Dim pvSerialNum As String
        Dim pvSQL As String

        Dim intC As Integer = 0
        pvSerialNum = "0"
        pvSQL = ""

        pvSerialNum = "ERR_ERR"

        Select Case UCase(RTrim(pvCODE))
            Case "GET_SN_IL"
                Select Case RTrim(pvRef_Type)
                    Case "BENEF_SN", "FUN_SN", "FUN_COVER_SN"
                        pvSQL = "SPIL_GET_UNDW_GEN_CNT"
                    Case Else
                        pvSerialNum = "PARAM_ERR"
                        Return pvSerialNum
                        Exit Function
                End Select

            Case Else
                pvSerialNum = "PARAM_ERR"
                Return pvSerialNum
                Exit Function

        End Select

        Dim mystrCONN As String
        'mystrCONN = CType(ConfigurationManager.AppSettings("APPCONN"), String)

        mystrCONN = gnGET_CONN_STRING()
        mystrCONN = "Provider=SQLOLEDB;" & mystrCONN

        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            objOLEConn = Nothing
            Return "DB_ERR"
            Exit Function

        End Try


        Dim objOLECmd As OleDbCommand = New OleDbCommand(pvSQL, objOLEConn)
        objOLECmd.CommandType = Data.CommandType.StoredProcedure
        objOLECmd.Parameters.Clear()

        Select Case pvRef_Type
            Case "BENEF_SN", "FUN_SN", "FUN_COVER_SN"
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 20).Value = RTrim(pvRef_Type)
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = RTrim(pvRef_Code_A)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 40).Value = RTrim(pvRef_Code_B)
                objOLECmd.Parameters.Add("p04", OleDbType.VarChar, 50).Direction = Data.ParameterDirection.Output
                objOLECmd.Parameters.Add("p05", OleDbType.VarChar, 50).Direction = Data.ParameterDirection.Output
            Case Else
                objOLECmd = Nothing
                pvSerialNum = "PARAM_ERR"
                Return pvSerialNum
                Exit Function
        End Select


        'Try
        Select Case RTrim(pvRef_Type)
            Case "BENEF_SN", "FUN_SN", "FUN_COVER_SN"
                intC = objOLECmd.ExecuteNonQuery()
                'pvSerialNum = CType(objOLECmd.Parameters("p10").Value & vbNullString, String)
                'Call gnASPNET_MsgBox("Serial No: " & pvSerialNum)
                pvSerialNum = CType(objOLECmd.Parameters("p04").Value & vbNullString, String)
                'Call gnASPNET_MsgBox("Ref Code: " & pvSerialNum)
                GoTo gnGet_SN_End
        End Select
        'Catch ex As Exception

        ' dispose of open objects
        'objOLECmd.Dispose()

        'objOLECmd = Nothing


        If objOLEConn.State = Data.ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        pvSerialNum = "ERR_ERR"
        Return pvSerialNum
        Exit Function
        'End Try


gnGet_SN_End:

        ' dispose of open objects
        objOLECmd.Dispose()


        If objOLEConn.State = Data.ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Return pvSerialNum

    End Function

    Public Function gnGet_Serial_Und(ByVal pvCODE As String, ByVal pvRef_Code_ID As String, ByVal pvRef_Code_Type As String, ByVal pvRef_Branch As String, ByVal pvRef_Period As String, ByVal pvRef_Prefix As String) As String

        Dim pvSerialNum As String
        Dim pvSQL As String

        Dim intC As Integer = 0
        pvSerialNum = "0"
        pvSQL = ""

        Select Case RTrim(pvCODE)
            Case "GET_SN_IL_UNDW"
                Select Case RTrim(pvRef_Code_ID)
                    Case "L02"
                        pvSQL = "SPIL_GET_UNDW_SN"
                    Case Else
                        pvSerialNum = "PARAM_ERR"
                        Return pvSerialNum
                        Exit Function
                End Select

            Case "GET_SN_IL_MKT"
                pvSQL = "SPIL_GET_MKT_SN"
            Case "GET_SN_IL_INS"
                pvSQL = "SPIL_GET_INS_SN"
            Case "GET_SN_IL_BRK"
                pvSQL = "SPIL_GET_BRK_SN"
            Case "GET_SN_IL_AGT"
                pvSQL = "SPIL_GET_AGT_SN"

            Case Else
                pvSerialNum = "PARAM_ERR"
                Return pvSerialNum
                Exit Function

        End Select

        Dim mystrCONN As String
        'mystrCONN = CType(ConfigurationManager.AppSettings("APPCONN"), String)

        mystrCONN = gnGET_CONN_STRING()
        mystrCONN = "Provider=SQLOLEDB;" & mystrCONN

        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Try
            'open connection to database
            objOLEConn.Open()

        Catch ex As Exception
            objOLEConn = Nothing
            Return "DB_ERR"
            Exit Function

        End Try


        Dim objOLECmd As OleDbCommand = New OleDbCommand(pvSQL, objOLEConn)
        objOLECmd.CommandType = Data.CommandType.StoredProcedure
        objOLECmd.Parameters.Clear()

        Select Case pvCODE
            Case "GET_SN_IL_UNDW"
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 5).Value = RTrim(pvRef_Code_ID)
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 5).Value = RTrim(pvRef_Code_Type)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 5).Value = RTrim(pvRef_Branch)
                objOLECmd.Parameters.Add("p04", OleDbType.VarChar, 10).Value = RTrim(pvRef_Period)
                objOLECmd.Parameters.Add("p05", OleDbType.VarChar, 5).Value = RTrim(pvRef_Prefix)
                objOLECmd.Parameters.Add("p06", OleDbType.VarChar, 20).Direction = Data.ParameterDirection.Output
                objOLECmd.Parameters.Add("p07", OleDbType.VarChar, 20).Direction = Data.ParameterDirection.Output

            Case "GET_SN_IL_MKT", "GET_SN_IL_INS", "GET_SN_IL_BRK", "GET_SN_IL_AGT"
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 5).Value = RTrim(pvRef_Code_ID)
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 5).Value = RTrim(pvRef_Code_Type)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 5).Value = RTrim(pvRef_Branch)
                objOLECmd.Parameters.Add("p04", OleDbType.VarChar, 10).Value = RTrim(pvRef_Period)
                objOLECmd.Parameters.Add("p05", OleDbType.VarChar, 5).Value = RTrim(pvRef_Prefix)
                objOLECmd.Parameters.Add("p06", OleDbType.VarChar, 20).Direction = Data.ParameterDirection.Output
                objOLECmd.Parameters.Add("p07", OleDbType.VarChar, 20).Direction = Data.ParameterDirection.Output

            Case Else
                objOLECmd = Nothing
                pvSerialNum = "PARAM_ERR"
                Return pvSerialNum
                Exit Function
        End Select


        Try
            Select Case RTrim(pvCODE)
                Case "GET_SN_IL_UNDW"
                    intC = objOLECmd.ExecuteNonQuery()
                    pvSerialNum = CType(objOLECmd.Parameters("p07").Value & vbNullString, String)
                    GoTo gnGet_SN_End
                Case "GET_SN_IL_MKT", "GET_SN_IL_INS", "GET_SN_IL_BRK", "GET_SN_IL_AGT"
                    intC = objOLECmd.ExecuteNonQuery()
                    pvSerialNum = CType(objOLECmd.Parameters("p07").Value & vbNullString, String)
                    GoTo gnGet_SN_End
            End Select
        Catch ex As Exception

            ' dispose of open objects
            'objOLECmd.Dispose()
            objOLECmd = Nothing


            If objOLEConn.State = Data.ConnectionState.Open Then
                objOLEConn.Close()
            End If
            objOLEConn = Nothing

            pvSerialNum = "ERR_ERR"
            Return pvSerialNum
            Exit Function
        End Try


        Try
            Dim objOLEDR As OleDbDataReader
            objOLEDR = objOLECmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)

            If (objOLEDR.Read()) Then
                Select Case RTrim(pvCODE)
                    Case "GET_INSURED_SN"
                        pvSerialNum = CType(objOLEDR("autoinsrd_rec_num") & vbNullString, String)
                    Case "GET_POLICY_SN"
                        pvSerialNum = CType(objOLEDR("autopol_serial_num") & vbNullString, String)
                End Select
            End If

            If objOLEDR.IsClosed = False Then
                objOLEDR.Close()
            End If
            objOLEDR = Nothing

        Catch ex As Exception
            pvSerialNum = "ERR_ERR"

            objOLECmd = Nothing

            If objOLEConn.State = Data.ConnectionState.Open Then
                objOLEConn.Close()
            End If
            objOLEConn = Nothing

            Return pvSerialNum
            Exit Function

        End Try


gnGet_SN_End:

        ' dispose of open objects
        objOLECmd.Dispose()


        If objOLEConn.State = Data.ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Return pvSerialNum

    End Function

    Public Function gnGET_USER_INFO(ByVal pvstr_COMP_ID As String, ByVal pvstr_User_ID As String) As ICollection

        Dim myobj_AL As New ArrayList()
        'oAL = gnValidat_Codes(mystrID, RTrim(pvPol), objOLEConn)
        'If oAL.Item(0) = "TRUE" Then
        '    'Destroy i.e remove the array list object from memory
        '    oAL = Nothing
        '    GoTo gnGet_Loop_Start
        'Else
        '    oAL = Nothing
        'End If

        Dim mystr_conn As String = ""
        Dim mystr_Table As String = ""
        Dim mystr_SQL As String = ""

        Dim myint_C As Integer = 0

        'Dim myobj_dt As New System.Data.DataTable
        'Dim myobj_ds As New System.Data.DataSet

        mystr_conn = gnGET_CONN_STRING()
        mystr_conn = "Provider=SQLOLEDB;" & mystr_conn

        Dim myole_CONN As OleDbConnection = Nothing
        myole_CONN = New OleDbConnection(mystr_conn)

        Try
            ' Open connection
            myole_CONN.Open()

        Catch ex As Exception
            myobj_AL.Insert(0, "ERROR")
            myobj_AL.Insert(1, RTrim(ex.Message.ToString))
            myobj_AL.Insert(2, RTrim("Please contact your System Administrator"))
            Return myobj_AL
            Exit Function
        End Try

        ' Select the data from table.

        mystr_SQL = ""
        mystr_SQL = mystr_SQL & "select top 1 * from " & mystr_Table
        mystr_SQL = mystr_SQL & " where pwd_id = '" & RTrim(pvstr_User_ID) & "'"
        mystr_SQL = mystr_SQL & " and pwd_group_id = '" & RTrim(pvstr_COMP_ID) & "'"

        'Dim myole_DA As OleDbDataAdapter
        'myole_DA = New OleDbDataAdapter(mystr_SQL, myole_CONN)

        'myole_DA.Fill(myobj_dt)
        ''myole_DA.Fill(myobj_ds)

        'myint_C = myobj_dt.Rows.Count
        'For myint_C = 0 To myobj_dt.Rows.Count - 1
        '    Console.Write("<br/>Row: " & myint_C & " Col: 1" & myobj_dt.Rows(myint_C)(0).ToString)
        'Next

        mystr_SQL = "SPIL_GET_USER_INFO"
        Dim myole_CMD As New OleDbCommand(mystr_SQL, myole_CONN)

        myole_CMD.CommandType = CommandType.StoredProcedure
        myole_CMD.Parameters.Add("@p01", OleDbType.VarChar, 3).Value = RTrim(pvstr_COMP_ID)
        myole_CMD.Parameters.Add("@p02", OleDbType.VarChar, 10).Value = RTrim(pvstr_User_ID)
        myole_CMD.Parameters.Add("@p03", OleDbType.VarChar, 10).Value = RTrim(pvstr_User_ID)

        ' execute the command to obtain the resultant dataset
        Dim myole_DR As OleDbDataReader

        Try

            myole_DR = myole_CMD.ExecuteReader()

            ' with the new data reader parse values and place into the return variable
            If (myole_DR.Read()) Then
                'Me.UserCode.Text = Me.UserName.Text & " - " & oleDR("pwd_code").ToString & vbNullString

                myobj_AL.Insert(0, "TRUE")
                With myobj_AL
                    .Insert(1, RTrim(myole_DR("pwd_user_name").ToString & vbNullString))
                    .Insert(2, RTrim(myole_DR("pwd_user_type").ToString & vbNullString))
                    .Insert(3, RTrim(myole_DR("pwd_user_level").ToString & vbNullString))
                End With

                myole_DR.Close()
                myole_CMD.Dispose()
            Else
                myobj_AL.Insert(0, "FALSE")
                myobj_AL.Insert(1, RTrim("Sorry. The system cannot find your information in the database..."))
                myobj_AL.Insert(2, RTrim("Try to login with a valid user id or contact your System Administrator"))
            End If

        Catch ex As Exception
            '   'Throw ex
            myobj_AL.Insert(0, "ERROR")
            myobj_AL.Insert(1, RTrim(ex.Message.ToString))
            myobj_AL.Insert(2, RTrim("Please contact your System Administrator"))
        Finally
        End Try


        'myole_DA.Dispose()

        Try
            ' Close connection
            myole_CONN.Close()

        Catch ex As Exception

        End Try

        'myobj_ds = Nothing
        'myole_DA = Nothing

        myole_DR = Nothing
        myole_CMD = Nothing
        myole_CONN = Nothing

        'Return myobj_dt
        Return myobj_AL

    End Function

    Public Sub gnInitialize_Numeric(ByVal pvCtr_TextBox As TextBox)
        If pvCtr_TextBox IsNot Nothing Then
            If TypeOf pvCtr_TextBox Is System.Web.UI.WebControls.TextBox Then
                pvCtr_TextBox.Text = Trim(pvCtr_TextBox.Text)
                If Trim(pvCtr_TextBox.Text) = "" Then
                    pvCtr_TextBox.Text = "0"
                End If
                If Not IsNumeric(Trim(pvCtr_TextBox.Text)) Then
                    pvCtr_TextBox.Text = "0"
                End If
            End If
        End If

    End Sub

    Public Function gnParseEmail_Address(ByVal emailString As String) As Boolean

        Dim pvblnStatus As Boolean = False

        Dim emailRegEx As New Regex("(\S+)@([^\.\s]+)(?:\.([^\.\s]+))+")
        Dim om As Match = emailRegEx.Match(emailString)
        If om.Success Then
            'Dim output As String = ""
            'output &= "User name: " & m.Groups(1).Value & vbCrLf
            'For i As Integer = 2 To m.Groups.Count - 1
            '    Dim g As Group = m.Groups(i)
            '    For Each c As Capture In g.Captures
            '        output &= "Address part: " & c.Value & vbCrLf
            '    Next
            'Next
            pvblnStatus = True
            'FirstMsg = "javascript:alert('E-mail address OK...');"
        Else
            pvblnStatus = False
            'FirstMsg = "javascript:alert('Error!. Invalid e-mail address...');"
        End If

        Return pvblnStatus

    End Function


    Public Sub gnPopulate_DropDownList(ByVal pvCODE As String, ByVal pvcboDDList As DropDownList, Optional ByVal pvSQL As String = "", _
                                       Optional ByVal pvWhere As String = "", Optional ByVal pvDefault_Text As String = "** Select item **", Optional ByVal pvDefault_Value As String = "*", Optional ByVal pvWhere2 As String = "")

        Dim pvListItem = New ListItem
        Dim pvstrTableName As String = ""
        Dim pvstrSQL As String = ""

        pvcboDDList.Items.Clear()

        pvstrSQL = pvSQL

        Select Case RTrim(pvCODE)
            Case "IL_INS_MODULE_LIST", "IL_BRK_MODULE_LIST"
                'Populate box with app module
                pvcboDDList.Items.Clear()

                pvListItem = New ListItem("Individual Life", "IND")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Group Life", "GRP")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Annuity", "ANN")
                pvcboDDList.Items.Add(pvListItem)

                pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                pvListItem = Nothing
                Exit Sub

            Case "IL_INS_AGENCY_TYPE_LIST"
                'Populate box with app module
                pvcboDDList.Items.Clear()

                'pvListItem = New ListItem("Agency Manager", "M")
                'pvcboDDList.Items.Add(pvListItem)
                'pvListItem = New ListItem("Unit Manager", "U")
                'pvcboDDList.Items.Add(pvListItem)
                'pvListItem = New ListItem("Existing Agent Directly Under Agency Manager", "A")
                'pvcboDDList.Items.Add(pvListItem)
                'pvListItem = New ListItem("Existing Agent Directly Under Unit Manager", "B")
                'pvcboDDList.Items.Add(pvListItem)
                'pvListItem = New ListItem("New Agent Directly Under Agency Manager", "C")
                'pvcboDDList.Items.Add(pvListItem)
                'pvListItem = New ListItem("New Agent Directly Under Unit Manager", "D")
                'pvcboDDList.Items.Add(pvListItem)

                ' NEW AGENTS TYPE
                'Types of Agents
                'T – Agency Controller
                'R – Regional Manager
                'S – Senior Agency Manager
                'M - Agency manager
                'U – Unit Manager
                'N – Senior Sales Exec
                'L –  Sales Exec

                pvListItem = New ListItem("Agency Controller", "T")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Regional Manager", "R")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Senior Agency Manager", "S")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Agency Manager", "M")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Unit Manager", "U")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Senior Sales Executive", "N")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Sales Executive", "L")
                pvcboDDList.Items.Add(pvListItem)

                pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                pvListItem = Nothing
                Exit Sub

            Case "IL_MKT_REGION_LIST"
                'Populate box with app module
                pvcboDDList.Items.Clear()

                pvListItem = New ListItem("REGION 1", "001")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("REGION 2", "002")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("REGION 3", "003")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("N/A", "000")
                pvcboDDList.Items.Add(pvListItem)

                pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                pvListItem = Nothing
                Exit Sub

            Case "COA_MAIN_TYPE"
                'Populate box with Main account type
                pvcboDDList.Items.Clear()

                pvListItem = New ListItem("Asset", "A")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Capital", "C")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Expenses", "E")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Liability", "L")
                pvcboDDList.Items.Add(pvListItem)
                pvListItem = New ListItem("Revenue", "R")
                pvcboDDList.Items.Add(pvListItem)

                pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                pvListItem = Nothing
                Exit Sub

        End Select


        pvcboDDList.Items.Clear()

        'Get connection string
        Dim mystrCONN As String = ""
        'mystrCONN = CType(ConfigurationManager.ConnectionStrings("ABSECONN").ToString, String)
        'mystrCONN = CType(ConfigurationManager.AppSettings("APPCONN"), String)
        mystrCONN = CType(gnCONN_STRING, String)

        mystrCONN = "Provider=SQLOLEDB;" & gnGET_CONN_STRING()

        'System.Web.HttpContext.Current.Response.Write("Connection-1: " & mystrCONN & "<br />" & "Connection-2: " & gnCONN_STRING)
        'Call gnASPNET_MsgBox("connection string-1: " & gnCONN_STRING)
        'Call gnASPNET_MsgBox("connection string-2: " & mystrCONN)

        Dim objOLEConn As New OleDbConnection(mystrCONN)

        'open connection to database
        Try
            objOLEConn.Open()
        Catch ex As Exception
            objOLEConn = Nothing
            Call gnASPNET_MsgBox("Cannot establish connection to database now.  Reason: " & ex.Message)
            Exit Sub
        End Try


        Dim objOLECmd As OleDbCommand = New OleDbCommand(pvstrSQL, objOLEConn)
        Select Case UCase(RTrim(pvCODE))
            Case "IL_ASSURED_HELP_SP"
                objOLECmd.Parameters.Clear()
                objOLECmd.CommandType = Data.CommandType.StoredProcedure
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = RTrim("I")
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 50).Value = RTrim(pvWhere2)
            Case "AN_ASSURED_HELP_SP"
                objOLECmd.Parameters.Clear()
                objOLECmd.CommandType = Data.CommandType.StoredProcedure
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = RTrim("A")
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
            Case "GL_ASSURED_HELP_SP"
                objOLECmd.Parameters.Clear()
                objOLECmd.CommandType = Data.CommandType.StoredProcedure
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = RTrim("G")
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 50).Value = RTrim(pvWhere)

            Case "IL_MKT_REGION_LIST_SP"
                objOLECmd.Parameters.Clear()
                objOLECmd.CommandType = Data.CommandType.StoredProcedure
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = RTrim("I")
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
            Case "IL_MKT_AGENCY_LIST_SP"
                objOLECmd.Parameters.Clear()
                objOLECmd.CommandType = Data.CommandType.StoredProcedure
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = RTrim("I")
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
            Case "IL_MKT_UNIT_LIST_SP"
                objOLECmd.Parameters.Clear()
                objOLECmd.CommandType = Data.CommandType.StoredProcedure
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = RTrim("I")
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
            Case "IL_MKT_AGENT_LIST_SP"
                objOLECmd.Parameters.Clear()
                objOLECmd.CommandType = Data.CommandType.StoredProcedure
                objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = RTrim("I")
                objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
                objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 50).Value = RTrim(pvWhere)
            Case Else
                objOLECmd.Parameters.Clear()
                objOLECmd.CommandType = Data.CommandType.Text
                'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        End Select

        Dim pvlngCnt As Integer = 0
        pvlngCnt = 0

        Dim objOLEDR As OleDbDataReader = objOLECmd.ExecuteReader()

        Do While objOLEDR.Read
            Select Case RTrim(pvCODE)
                Case "XXX"
                Case Else
                    pvListItem = New ListItem
                    pvListItem.Value = RTrim(CType(objOLEDR("MyFld_Value") & vbNullString, String))
                    pvListItem.Text = RTrim(CType(objOLEDR("MyFld_Text") & vbNullString, String))
                    pvcboDDList.Items.Add(pvListItem)
                    pvlngCnt = pvlngCnt + 1
            End Select
        Loop

        Select Case RTrim(pvCODE)
            Case "XXX"
                If Val(pvlngCnt) = 0 Then
                Else
                    pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                End If

            Case "INSURED_CODE", "IL_ASSURED_LIST", "GL_ASSURED_LIST", "IL_ASSURED_HELP_SP"
                If Val(pvlngCnt) = 0 Then
                    pvcboDDList.Items.Insert(0, New ListItem("* Name not found *", pvDefault_Value))
                Else
                    pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                End If

            Case "IL_MARKETERS_LIST"
                If Val(pvlngCnt) = 0 Then
                    pvcboDDList.Items.Insert(0, New ListItem("* Name not Found *", pvDefault_Value))
                Else
                    pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                End If

            Case "IL_BROKERS_LIST"
                If Val(pvlngCnt) = 0 Then
                    pvcboDDList.Items.Insert(0, New ListItem("* Broker Name not Found *", pvDefault_Value))
                Else
                    pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                End If

            Case "ACCOUNT_SEARCH", "ACCOUNT_SEARCH_NEW"
                If Val(pvlngCnt) = 0 Then
                    pvcboDDList.Items.Insert(0, New ListItem("* Account Name not Found *", pvDefault_Value))
                Else
                    pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
                End If

            Case Else
                pvcboDDList.Items.Insert(0, New ListItem(pvDefault_Text, pvDefault_Value))
        End Select

        pvListItem = Nothing

        ' dispose of open objects
        objOLECmd.Dispose()
        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        If objOLEConn.State = Data.ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

    End Sub


    Public Sub gnProc_DDL_Get(ByVal pvDDL As DropDownList, ByVal pvRef_Value As String)

        'On Error Resume Next
        Try
            pvDDL.SelectedIndex = pvDDL.Items.IndexOf(pvDDL.Items.FindByValue(CType(RTrim(pvRef_Value), String)))
        Catch ex As Exception

        End Try

    End Sub


    Public Sub gnProc_Populate_Box(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboList As DropDownList, Optional ByVal pvSearchValue As String = "", Optional ByVal pvSearchValue2 As String = "")
        'Populate box with codes

        Dim strTable As String = ""
        Dim strTableName As String = ""
        Dim strSQL As String = ""

        pvcboList.Items.Clear()

        Select Case UCase(Trim(pvCODE))

            Case "IL_CODE_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_LIFE_CODES")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_COD_ITEM AS MyFld_Value, TBIL_COD_LONG_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_COD_TAB_ID = '" & RTrim("L02") & "'"
                strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " ORDER BY TBIL_COD_LONG_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_CODE_LIST_UND"
                strTable = strTableName
                strTable = RTrim("TBIL_LIFE_CODES")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_COD_ITEM AS MyFld_Value, TBIL_COD_LONG_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_COD_TAB_ID = '" & RTrim("L01") & "'"
                strSQL = strSQL & " AND TBIL_COD_TYP = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " ORDER BY TBIL_COD_LONG_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_PRODUCT_CAT_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_CAT_CD AS MyFld_Value, TBIL_PRDCT_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_CAT_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE IN('I')"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD, TBIL_PRDCT_CAT_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "GL_PRODUCT_CAT_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_CAT_CD AS MyFld_Value, TBIL_PRDCT_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_CAT_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE IN('G')"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD, TBIL_PRDCT_CAT_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','I')"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "GL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('GRP','G')"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_COVER_LIST", "IL_COVER_LIST_OTHERS"
                strTable = strTableName
                strTable = RTrim("TBIL_COVER_DET")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_COV_CD AS MyFld_Value, TBIL_COV_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_COV_PRDCT_CD = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_COV_MDLE_CD IN('IND','I')"
                Select Case UCase(Trim(pvCODE))
                    Case "IL_COVER_LIST"
                        strSQL = strSQL & " AND TBIL_COV_TYPE IN('B')"
                    Case Else
                        strSQL = strSQL & " AND TBIL_COV_TYPE NOT IN('B')"
                End Select

                strSQL = strSQL & " ORDER BY TBIL_COV_PRDCT_CD, TBIL_COV_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "GL_COVER_LIST", "GL_COVER_LIST_OTHERS"
                strTable = strTableName
                strTable = RTrim("TBIL_COVER_DET")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_COV_CD AS MyFld_Value, TBIL_COV_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_COV_PRDCT_CD = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_COV_MDLE_CD IN('GRP','G')"
                Select Case UCase(Trim(pvCODE))
                    Case "GL_COVER_LIST"
                        strSQL = strSQL & " AND TBIL_COV_TYPE IN('B')"
                    Case Else
                        strSQL = strSQL & " AND TBIL_COV_TYPE NOT IN('B')"
                End Select

                strSQL = strSQL & " ORDER BY TBIL_COV_PRDCT_CD, TBIL_COV_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")


            Case "IL_PLAN_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PLAN_MAST")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PLAN_CD AS MyFld_Value, TBIL_PLAN_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PLAN_PRDCT_CD = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PLAN_MDLE_CD  IN('IND','I')"
                strSQL = strSQL & " ORDER BY TBIL_PLAN_PRDCT_CD, TBIL_PLAN_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "GL_PLAN_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PLAN_MAST")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PLAN_CD AS MyFld_Value, TBIL_PLAN_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PLAN_PRDCT_CD = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PLAN_MDLE_CD  IN('GRP','G')"
                strSQL = strSQL & " ORDER BY TBIL_PLAN_PRDCT_CD, TBIL_PLAN_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "AN_RATE_TYPE_CODE_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_RATE_TYPE_CODES")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_RATE_TYP_CODE AS MyFld_Value, TBIL_RATE_TYP_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_RATE_TYP_PRDCT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_RATE_TYP_MDLE IN('ANN','A')"
                strSQL = strSQL & " ORDER BY TBIL_RATE_TYP_PRDCT, TBIL_RATE_TYP_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_RATE_TYPE_CODE_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_RATE_TYPE_CODES")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_RATE_TYP_CODE AS MyFld_Value, TBIL_RATE_TYP_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_RATE_TYP_PRDCT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_RATE_TYP_MDLE IN('IND','I')"
                strSQL = strSQL & " ORDER BY TBIL_RATE_TYP_PRDCT, TBIL_RATE_TYP_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "GL_RATE_TYPE_CODE_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_RATE_TYPE_CODES")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_RATE_TYP_CODE AS MyFld_Value, TBIL_RATE_TYP_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_RATE_TYP_PRDCT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_RATE_TYP_MDLE IN('GRP','G')"
                strSQL = strSQL & " ORDER BY TBIL_RATE_TYP_PRDCT, TBIL_RATE_TYP_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_ASSURED_LIST"
                strTable = "TBIL_INS_DETAIL"
                strSQL = ""
                strSQL = "SELECT TBIL_INSRD_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INSRD_ID = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_INSRD_MDLE IN ('IND','I')"
                strSQL = strSQL & " AND (TBIL_INSRD_SURNAME LIKE '" & RTrim(pvSearchValue) & "%'"
                strSQL = strSQL & " OR TBIL_INSRD_FIRSTNAME LIKE '" & RTrim(pvSearchValue) & "%')"
                strSQL = strSQL & " ORDER BY RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,''))"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "*** select insured name ***", "*")

            Case "AN_ASSURED_LIST"
                strTable = "TBIL_INS_DETAIL"
                strSQL = ""
                strSQL = "SELECT TBIL_INSRD_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INSRD_ID = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_INSRD_MDLE IN ('ANN','A')"
                strSQL = strSQL & " AND (TBIL_INSRD_SURNAME LIKE '" & RTrim(pvSearchValue) & "%'"
                strSQL = strSQL & " OR TBIL_INSRD_FIRSTNAME LIKE '" & RTrim(pvSearchValue) & "%')"
                strSQL = strSQL & " ORDER BY RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,''))"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "*** select insured name ***", "*")


            Case "GL_ASSURED_LIST"
                strTable = "TBIL_INS_DETAIL"
                strSQL = ""
                strSQL = "SELECT TBIL_INSRD_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_INSRD_ID = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_INSRD_MDLE IN ('GRP','G')"
                strSQL = strSQL & " AND (TBIL_INSRD_SURNAME LIKE '" & RTrim(pvSearchValue) & "%'"
                strSQL = strSQL & " OR TBIL_INSRD_FIRSTNAME LIKE '" & RTrim(pvSearchValue) & "%')"
                strSQL = strSQL & " ORDER BY RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' + RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,''))"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "*** select insured name ***", "*")

            Case "IL_ASSURED_HELP_SP"
                strTable = "TBIL_POLICY_DET"
                strSQL = ""
                strSQL = RTrim("SPIL_GET_INSURED")
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, RTrim(pvSearchValue), "* select insured *", "*", pvSearchValue2)

            Case "AN_ASSURED_HELP_SP"
                strTable = "TBIL_ANN_POLICY_DET"
                strSQL = ""
                '  strSQL = RTrim("SPIL_GET_ANN_POLICY")
                strSQL = RTrim("SPAN_GET_INSURED")
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, RTrim(pvSearchValue), "* select insured *", "*")


            Case "GL_ASSURED_HELP_SP"
                strTable = "TBIL_GRP_POLICY_DET"
                strSQL = ""
                strSQL = RTrim("SPGL_GET_INSURED")
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, RTrim(pvSearchValue), "* select insured *", "*")


            Case "IL_MARKETERS_LIST"
                strTable = "TBIL_AGENCY_CD"
                strSQL = ""
                strSQL = "SELECT TBIL_AGCY_AGENT_CD AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_AGCY_AGENT_NAME,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_AGCY_AGENT_NAME LIKE '" & RTrim(pvSearchValue) & "%'"
                strSQL = strSQL & " AND TBIL_AGCY_AGENT_ID = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_AGCY_CD_MDLE IN('IND','I')"
                strSQL = strSQL & " ORDER BY TBIL_AGCY_AGENT_NAME"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "*** marketer name ***", "*")

            Case "IL_MKT_REGION_LIST_SP"
                strTable = "TBIL_AGENCY_CD"
                strSQL = ""
                strSQL = RTrim("SPIL_GET_MKT_REGION")
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, RTrim(pvSearchValue), "* select region *", "*")

            Case "IL_MKT_AGENCY_LIST_SP"
                strTable = "TBIL_AGENCY_CD"
                strSQL = ""
                strSQL = RTrim("SPIL_GET_MKT_AGENCY")
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, RTrim(pvSearchValue), "* select agency *", "*")

            Case "IL_MKT_UNIT_LIST_SP"
                strTable = "TBIL_AGENCY_CD"
                strSQL = ""
                strSQL = RTrim("SPIL_GET_MKT_UNIT")
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, RTrim(pvSearchValue), "* select unit *", "*")

            Case "IL_MKT_AGENT_LIST_SP"
                strTable = "TBIL_AGENCY_CD"
                strSQL = ""
                strSQL = RTrim("SPIL_GET_MKT_AGENT")
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, RTrim(pvSearchValue), "* select agent *", "*")

            Case "IL_BROKERS_LIST"
                strTable = "TBIL_CUST_DETAIL"
                strSQL = ""
                strSQL = "SELECT TBIL_CUST_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_CUST_DESC,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_CUST_DESC LIKE '" & RTrim(pvSearchValue) & "%'"
                strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_CUST_MDLE IN('IND','I')"
                strSQL = strSQL & " ORDER BY TBIL_CUST_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "*** select broker name ***", "*")

            Case "AN_BROKERS_LIST"
                strTable = "TBIL_CUST_DETAIL"
                strSQL = ""
                strSQL = "SELECT TBIL_CUST_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_CUST_DESC,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_CUST_DESC LIKE '" & RTrim(pvSearchValue) & "%'"
                strSQL = strSQL & " AND TBIL_CUST_ID = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_CUST_MDLE IN('ANN','A')"
                strSQL = strSQL & " ORDER BY TBIL_CUST_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "*** select broker name ***", "*")


            Case "IL_BROKERS_LIST_GL"
                strTable = "TBIL_CUST_DETAIL"
                strSQL = ""
                strSQL = "SELECT TBIL_CUST_CODE AS MyFld_Value"
                strSQL = strSQL & ",RTRIM(ISNULL(TBIL_CUST_DESC,'')) AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_CUST_ID = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_CUST_MDLE = '" & RTrim("GRP") & "'"
                strSQL = strSQL & " AND TBIL_CUST_DESC LIKE '" & RTrim(pvSearchValue) & "%'"
                strSQL = strSQL & " ORDER BY TBIL_CUST_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "*** select broker name ***", "*")

            Case "IL_MOP_FACTOR_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_MOP_FACTOR")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_MOP_TYPE_CD AS MyFld_Value, TBIL_MOP_TYPE_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                'strSQL = strSQL & " AND TBIL_MOP_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " ORDER BY TBIL_MOP_TYPE_DESC"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

            Case "IL_FUNERAL_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_FUNERAL_SA_TAB")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_FUN_COVER_ID AS MyFld_Value, TBIL_FUN_NAME_COVERD AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_FUN_FILE_NO = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & pvSearchValue
                strSQL = strSQL & " ORDER BY TBIL_FUN_COVER_ID"
                Call gnPopulate_DropDownList(pvCODE, pvcboList, strSQL, "", "(Select item)", "*")

        End Select

    End Sub


    Public Function gnTest_TransDate(ByVal MyFunc_Date As String) As Boolean

        On Error GoTo MyTestDate_Err1

        Dim pvbln As Boolean

        gnTest_TransDate = False
        pvbln = False

        'If Len(MyFunc_Date) = 10 And Mid(MyFunc_Date, 3, 1) = "/" And Mid(MyFunc_Date, 6, 1) = "/" Then
        'Else
        '    Return pvbln
        '    Exit Function
        'End If

        If (Len(MyFunc_Date) = 10) And _
           (Mid(MyFunc_Date, 3, 1) = "-" Or Mid(MyFunc_Date, 3, 1) = "/") And _
           (Mid(MyFunc_Date, 6, 1) = "-" Or Mid(MyFunc_Date, 6, 1) = "/") Then
        Else
            Return pvbln
            Exit Function
        End If

        Dim strDteMsg As String = "Invalid Date"
        Dim strDteErr As String = "0"
        Dim DteTst As Date

        Dim strDte_Start As String
        Dim strDte_End As String

        Dim strDteYY As String
        Dim strDteMM As String
        Dim strDteDD As String

        strDteMsg = ""
        strDteErr = "0"

        strDteMsg = ""
        strDteErr = "0"

        'MsgBox _
        ' "Left Xter. :" & Left(MyFunc_Date, 2) & vbCrLf & _
        ' "Mid Xter. :" & Mid(MyFunc_Date, 4, 2) & vbCrLf & _
        ' "Right Xter. :" & Right(MyFunc_Date, 4)

        'If MyFunc_Date = "__/__/____" Or _
        '   MyFunc_Date = "" Then
        '    MyTestDate_Trans = True
        '    Exit Function
        'End If

        strDteDD = Left(MyFunc_Date, 2)
        strDteMM = Mid(MyFunc_Date, 4, 2)
        strDteYY = Right(MyFunc_Date, 4)

        strDteDD = Trim(strDteDD)
        strDteMM = Trim(strDteMM)
        strDteYY = Trim(strDteYY)

        'If strDteDD = "" And _
        '   strDteMM = "" And _
        '   strDteYY = "" Then
        '    MyTestDate_Trans = True
        '    Exit Function
        'End If

        'If Val(Left(MyFunc_Date, 2)) = 0 And _
        '   Val(Mid(MyFunc_Date, 4, 2)) = 0 And _
        '   Val(Right(MyFunc_Date, 4)) = 0 Then
        '   MyTestDate_Trans = True
        '   Exit Function
        'End If

        If Trim(strDteDD) < "01" Or _
           Trim(strDteDD) > "31" Then
            strDteMsg = _
              "  -> Day < 01 or Day > 31 ..." & vbCrLf
            strDteErr = "1"
            'MsgBox "Day date error..."
        End If
        If Trim(strDteMM) < "01" Or _
           Trim(strDteMM) > "12" Then
            strDteMsg = strDteMsg & _
              "  -> Month < 01 or Month > 12 ..." & vbCrLf
            strDteErr = "1"
            'MsgBox "Month date error..."
        End If


        'If strDteYY < "1990" Then
        '   strDteMsg = strDteMsg & _
        '     "  -> Year < 1990..." & vbCrLf
        '   strDteErr = "1"
        '   'MsgBox "Year date error..." & Year(Now)
        'End If
        If Len(Trim(strDteYY)) < 4 Then
            strDteMsg = strDteMsg & _
              "  -> Year = 0 digit or Year < 4 digits..." & vbCrLf
            strDteErr = "1"
            'MsgBox "Year date error..." & Year(Now)
        End If


        strDte_Start = ""
        strDte_End = ""
        strDte_Start = MyFunc_Date
        strDte_End = MyFunc_Date


        'Get the first day of a month
        '----------------------------
        'strDte_Start = DateSerial( _
        '  Format(Val(strDteYY), "0000"), _
        '  Format(Val(strDteMM), "00"), _
        '  Format(Val(1), "00"))

        'Get the end day of a month
        '--------------------------
        'strDte_End = DateSerial( _
        '  Format(Val(strDteYY), "0000"), _
        '  Format(Val(strDteMM) + 1, "00"), _
        '  Format(Val(0), "00"))


        'If Val(strDteDD) > Val(Mid(strDte_End, 4, 2)) Then
        '   strDteMsg = strDteMsg & _
        '     "  -> Invalid day in month. Month <" & strDteMM & ">" & _
        '     " ends in <" & Mid(strDte_End, 4, 2) & ">" & _
        '     ". Full Date: " & strDte_End & vbCrLf
        '   strDteErr = "1"
        '   'MsgBox "Day date error..."
        'End If


        Select Case Trim(strDteMM)
            Case "01", "03", "05", "07", "08", "10", "12"
                If Val(strDteDD) > 31 Then
                    strDteMsg = strDteMsg & _
                    "  -> Invalid day in month. Month <" & strDteMM & ">" & _
                    " ends in <" & " 31 " & ">" & _
                    ". Full Date: " & strDte_End & vbCrLf
                    strDteErr = "1"
                End If

            Case "02"
                'If (Val(strDteYY) \ 4) = 0 Then
                If (Val(strDteYY) Mod 4) = 0 Then
                    If Val(strDteDD) > 29 Then
                        strDteMsg = strDteMsg & _
                            "  -> Invalid day in month. Month <" & strDteMM & ">" & _
                            " ends in <" & " 29 " & ">" & _
                            ". Full Date: " & strDte_End & vbCrLf
                        strDteErr = "1"
                    End If
                Else
                    If Val(strDteDD) > 28 Then
                        strDteMsg = strDteMsg & _
                            "  -> Invalid day in month. Month <" & strDteMM & ">" & _
                            " ends in <" & " 28 " & ">" & _
                            ". Full Date: " & strDte_End & vbCrLf
                        strDteErr = "1"
                    End If

                End If

            Case "04", "06", "09", "11"
                If Val(strDteDD) > 30 Then
                    strDteMsg = strDteMsg & _
                    "  -> Invalid day in month. Month <" & strDteMM & ">" & _
                    " ends in <" & " 30 " & ">" & _
                    ". Full Date: " & strDte_End & vbCrLf
                    strDteErr = "1"
                End If
        End Select


MyTestDate_01:
        If strDteErr <> "0" Then
            GoTo MyTestDate_Msg
        End If

        gnTest_TransDate = True
        pvbln = True

        Return pvbln
        Exit Function

MyTestDate_Msg:

        'Call gnASPNET_MsgBox(strDteMsg)
        'Call gnASPNET_MsgBox("Invalid date...")
        'Call gnASPNET_MsgBox_VB(strDteMsg)

        gnTest_TransDate = False
        pvbln = False

        'MsgBox( _
        '  "Error: Incorrect or Incomplete date... " & vbCrLf & vbCrLf & _
        '  "Check the following:" & vbCrLf & vbCrLf & _
        '  strDteMsg & vbCrLf & vbCrLf & _
        '  "Please enter correct date or pick from <DatePicker>.", , "Date" & " - " & MyFunc_Date)

        Return pvbln
        Exit Function

MyTestDate_Err1:
        gnTest_TransDate = False
        pvbln = False

        'If Err.Number = 13 Then
        '    MsgBox("Error: Invalid date...", , "Incorrect date")
        'Else
        '    MsgBox("Error: " & Err.Description & _
        '      "(" & Err.Number & ")", vbCritical, "Error " & " - " & MyFunc_Date)
        'End If

        Return pvbln

    End Function

    Public Function gnValidate_Codes(ByVal pvCODE As String, ByVal pvRef_Value As TextBox, ByVal pvRef_Text As TextBox, _
                                     Optional ByVal pvRef_Misc As TextBox = Nothing, _
                                     Optional ByVal pvRef_Misc2 As TextBox = Nothing, Optional ByVal pvRef_Misc3 As TextBox = Nothing, _
                                     Optional ByVal pvRef_Misc4 As TextBox = Nothing, Optional ByVal pvRef_Misc5 As TextBox = Nothing, _
                                     Optional ByVal pvRef_Misc6 As TextBox = Nothing, Optional ByVal pvRef_Misc7 As TextBox = Nothing, _
                                     Optional ByVal pvRef_Misc8 As TextBox = Nothing, Optional ByVal pvRef_Misc9 As TextBox = Nothing) As Boolean

        Dim pvbln As Boolean
        pvbln = False

        Select Case TypeName(pvRef_Value)
            Case "TextBox", "MaskEdBox", "RichTextBox"
            Case "ImageCombo"
            Case Else
                'MsgBox("Error: Invalid argument in control passed to function." & _
                '  vbCrLf & "Parameter text: " & pvCODE & vbCrLf & _
                '  "Expecting a <TextBox> or <MaskEdBox> or <RichTextBox> or " & _
                '  "<ImageCombo> or " & vbCrLf & "a control with a <.Text> " & _
                '  "property", vbCritical, "Error")
                Return pvbln
                Exit Function
        End Select

        Select Case TypeName(pvRef_Text)
            Case "TextBox", "MaskEdBox", "RichTextBox"
            Case "ImageCombo"
            Case Else
                'MsgBox("Error: Invalid argument in control passed to function." & _
                '  vbCrLf & "Parameter text" & pvCODE & vbCrLf & _
                '  "Expecting a <TextBox> or <MaskEdBox> or <RichTextBox> or " & _
                '  "<ImageCombo> or " & vbCrLf & "a control with a <.Text> " & _
                '  "property", vbCritical, "Error")
                Return pvbln
                Exit Function
        End Select

        Select Case RTrim(pvCODE)
            Case "INSURED_CODE"
                gnTable = "ABSINSRDTAB"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT INSRD.CTINSRD_NUM,INSRD.CTINSRD_LONG_DESCR,CTINSRD_OTHERNAME"
                gnSQL = gnSQL & " ,INSRD.CTINSRD_ADDR1,INSRD.CTINSRD_ADDR2,INSRD.CTINSRD_ADDR3"
                gnSQL = gnSQL & " ,INSRD.CTINSRD_OCCUP,INSRD.CTINSRD_TEL_NUM1,INSRD.CTINSRD_TEL_NUM2"
                gnSQL = gnSQL & " ,INSRD.CTINSRD_TYPE,INSRD.CTINSRD_GSM_NUM,INSRD.CTINSRD_EMAIL_NUM"
                gnSQL = gnSQL & " FROM " & gnTable & " AS INSRD"
                gnSQL = gnSQL & " WHERE CTINSRD_NUM = '" & RTrim(pvRef_Value.Text) & "'"
                gnSQL = gnSQL & " AND CTINSRD_ID IN('001')"

            Case "INSURED_CODE_LIFE"
                gnTable = "TBIL_INS_DETAIL"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT TOP 1 INSRD.TBIL_INSRD_CODE, INSRD.TBIL_INSRD_SURNAME, INSRD.TBIL_INSRD_FIRSTNAME"
                gnSQL = gnSQL & " ,INSRD.TBIL_INSRD_ADRES1, INSRD.TBIL_INSRD_ADRES2"
                gnSQL = gnSQL & " ,INSRD.TBIL_INSRD_PHONE1, INSRD.TBIL_INSRD_PHONE2"
                gnSQL = gnSQL & " ,INSRD.TBIL_INSRD_EMAIL1"
                gnSQL = gnSQL & " FROM " & gnTable & " AS INSRD"
                gnSQL = gnSQL & " WHERE INSRD.TBIL_INSRD_CODE = '" & RTrim(pvRef_Value.Text) & "'"
                gnSQL = gnSQL & " AND INSRD.TBIL_INSRD_ID IN('001')"

            Case "POLICY"
                gnTable = "ABSPOLYTAB"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT PT.PT_POL_NUM,PT.PT_INS_NAME,PT.PT_INS_OTHERNAME,PT.PT_INS_TYPE,PT.PT_INS_NUM"
                gnSQL = gnSQL & ",PT.PT_RISK_NUM,PT.PT_SUBRISK_NUM,PT.PT_BRA_NUM,PT.PT_LOC_NUM,PT_AGCY_NUM"
                gnSQL = gnSQL & ",PT.PT_START_DATE,PT.PT_END_DATE,PT.PT_RW_START_DATE,PT.PT_RW_END_DATE"
                gnSQL = gnSQL & " FROM " & gnTable & " AS PT"
                gnSQL = gnSQL & " WHERE PT.PT_POL_NUM = '" & RTrim(pvRef_Value.Text) & "'"
                gnSQL = gnSQL & " AND PT.PT_POL_ID IN('001')"

            Case "AGENCY_UND", "AGENCY_CODE"
                gnTable = "ABSAGNTTAB"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT AGCY.CTAGCY_NUM,AGCY.CTAGCY_NAME,AGCY.CTAGCY_TYPE"
                gnSQL = gnSQL & " ,AGCY.CTAGCY_MOBILE_NUM"
                gnSQL = gnSQL & " FROM " & gnTable & " AS AGCY"
                gnSQL = gnSQL & " WHERE CTAGCY_NUM = '" & RTrim(pvRef_Value.Text) & "'"
                Select Case RTrim(pvCODE)
                    Case "AGENCY_UND"
                        gnSQL = gnSQL & " AND CTAGCY_ID IN('001')"
                        gnSQL = gnSQL & " AND RIGHT(RTRIM(CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                    Case "AGENCY_CODE"
                        'gnSQL = gnSQL & " AND CTAGCY_ID IN('001')"
                        'gnSQL = gnSQL & " AND RIGHT(RTRIM(CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                End Select

            Case "AGENCY_UND_LIFE", "AGENCY_CODE_LIFE"
                gnTable = "TBIL_AGENCY_CD"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT TOP 1 AGCY.TBIL_AGCY_AGENT_CD, AGCY.TBIL_AGCY_AGENT_NAME, AGCY.TBIL_AGCY_TYPE"
                gnSQL = gnSQL & " FROM " & gnTable & " AS AGCY"
                gnSQL = gnSQL & " WHERE AGCY.TBIL_AGCY_AGENT_CD = '" & RTrim(pvRef_Value.Text) & "'"
                Select Case RTrim(pvCODE)
                    Case "AGENCY_UND_LIFE"
                        gnSQL = gnSQL & " AND AGCY.TBIL_AGCY_AGENT_ID IN('001')"
                        gnSQL = gnSQL & " AND AGCY.TBIL_AGCY_CD_MDLE IN('IND','I')"
                        'gnSQL = gnSQL & " AND RIGHT(RTRIM(CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                    Case "AGENCY_CODE_LIFE"
                        gnSQL = gnSQL & " AND AGCY.TBIL_AGCY_AGENT_ID IN('001')"
                        gnSQL = gnSQL & " AND AGCY.TBIL_AGCY_CD_MDLE IN('IND','I')"
                        'gnSQL = gnSQL & " AND RIGHT(RTRIM(CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                End Select

            Case "BROKER_UND_ANN", "BROKER_CODE_LIFE"
                gnTable = "TBIL_CUST_DETAIL"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT TOP 1 BRK.TBIL_CUST_CODE, BRK.TBIL_CUST_DESC, BRK.TBIL_CUST_CATG"
                gnSQL = gnSQL & " FROM " & gnTable & " AS BRK"
                gnSQL = gnSQL & " WHERE BRK.TBIL_CUST_CODE = '" & RTrim(pvRef_Value.Text) & "'"
                Select Case RTrim(pvCODE)
                    Case "BROKER_UND_ANN"
                        gnSQL = gnSQL & " AND BRK.TBIL_CUST_ID IN('001')"
                        gnSQL = gnSQL & " AND BRK.TBIL_CUST_MDLE IN('ANN','A')"
                        'gnSQL = gnSQL & " AND RIGHT(RTRIM(CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                    Case "BROKER_CODE_LIFE"
                        gnSQL = gnSQL & " AND BRK.TBIL_CUST_ID IN('001')"
                        gnSQL = gnSQL & " AND BRK.TBIL_CUST_MDLE IN('IND','I')"
                        'gnSQL = gnSQL & " AND RIGHT(RTRIM(CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                End Select

            Case "BROKER_UND_LIFE", "BROKER_CODE_LIFE"
                gnTable = "TBIL_CUST_DETAIL"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT TOP 1 BRK.TBIL_CUST_CODE, BRK.TBIL_CUST_DESC, BRK.TBIL_CUST_CATG"
                gnSQL = gnSQL & " FROM " & gnTable & " AS BRK"
                gnSQL = gnSQL & " WHERE BRK.TBIL_CUST_CODE = '" & RTrim(pvRef_Value.Text) & "'"
                Select Case RTrim(pvCODE)
                    Case "BROKER_UND_LIFE"
                        gnSQL = gnSQL & " AND BRK.TBIL_CUST_ID IN('001')"
                        gnSQL = gnSQL & " AND BRK.TBIL_CUST_MDLE IN('IND','I')"
                        'gnSQL = gnSQL & " AND RIGHT(RTRIM(CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                    Case "BROKER_CODE_LIFE"
                        gnSQL = gnSQL & " AND BRK.TBIL_CUST_ID IN('001')"
                        gnSQL = gnSQL & " AND BRK.TBIL_CUST_MDLE IN('IND','I')"
                        'gnSQL = gnSQL & " AND RIGHT(RTRIM(CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                End Select

            Case "ACCOUNT_ACC_UND"
                gnTable = "ABSAGNTTAB"
                gnSQL = ""
                If IsNumeric(Left(pvRef_Value.Text, 1)) Then
                    gnSQL = ""
                    gnSQL = gnSQL & "SELECT COA.COA_LONG_DESCR AS ACCOUNT_NAME"
                    gnSQL = gnSQL & ",COA.COA_LEDGER_TYPE AS LEDGER_TYPE"
                    gnSQL = gnSQL & " FROM " & RTrim("ABSGLCODES") & " AS COA"
                    gnSQL = gnSQL & " WHERE COA.COA_NUM = '" & RTrim(pvRef_Value.Text) & "'"
                    gnSQL = gnSQL & " AND COA.COA_ID = '" & RTrim("005") & "'"
                Else
                    gnSQL = ""
                    gnSQL = gnSQL & "SELECT AGCY.CTAGCY_NAME AS ACCOUNT_NAME"
                    gnSQL = gnSQL & ",AGCY.CTAGCY_TYPE AS LEDGER_TYPE,AGCY.CTAGCY_MOBILE_NUM"
                    gnSQL = gnSQL & "  FROM " & gnTable & " AS AGCY"
                    gnSQL = gnSQL & " WHERE AGCY.CTAGCY_NUM = '" & RTrim(pvRef_Value.Text) & "'"
                    gnSQL = gnSQL & " AND RIGHT(RTRIM(AGCY.CTAGCY_NUM),3) NOT IN('/SO','/FO')"
                    'gnSQL = gnSQL & " AND AGCY.CTAGCY_ID IN('001')"
                End If
            Case "REF_CODE"
                gnTable = "ABSDBCRTAB"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT DC.DNCN_AGCY_NUM,DC.DNCN_BILLING_DATE,DC.DNCN_POL_NUM"
                gnSQL = gnSQL & ",DC.DNCN_BATCH_NUM,DC.DNCN_INS_NUM,DC.DNCN_RISK_NUM,DNCN_SUBRISK_NUM"
                gnSQL = gnSQL & ",RTRIM(PT.PT_INS_NAME) + ' ' + RTRIM(PT.PT_INS_OTHERNAME) AS INSURED_NAME"
                gnSQL = gnSQL & " FROM " & RTrim(gnTable) & " AS DC"
                gnSQL = gnSQL & " LEFT JOIN " & RTrim("ABSPOLYTAB") & " AS PT"
                gnSQL = gnSQL & " ON (DC.DNCN_POL_NUM = PT.PT_POL_NUM)"
                gnSQL = gnSQL & " WHERE DC.DNCN_TRANS_NUM = '" & RTrim(pvRef_Value.Text) & "'"
            Case "SECTOR_CODE"
                gnTable = "ABSBUSECTAB"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT BSEC.CTBS_NUM,BSEC.CTBS_LONG_DESCR FROM " & gnTable & " AS BSEC"
                gnSQL = gnSQL & " WHERE CTBS_NUM = '" & Val(RTrim(pvRef_Value.Text)) & "'"
            Case "SUBRISK_CODE"
                gnTable = "ABSSUBRISK"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT SUBRISK.CTSUBRISK_NUM,SUBRISK.CTSUBRISK_DESCR"
                gnSQL = gnSQL & " ,SUBRISK.CTSUBRISK_CLASS FROM " & gnTable & " AS SUBRISK"
                gnSQL = gnSQL & " WHERE CTSUBRISK_NUM = '" & RTrim(pvRef_Value.Text) & "'"
            Case "BRANCH_CODE"
                gnTable = "ABSBRNCHTAB"
                gnSQL = ""
                gnSQL = gnSQL & "SELECT BRA.CTBRA_NUM,BRA.CTBRA_NAME"
                gnSQL = gnSQL & " ,BRA.CTLOC_NUM FROM " & gnTable & " AS BRA"
                gnSQL = gnSQL & " WHERE CTBRA_NUM = '" & RTrim(pvRef_Value.Text) & "'"
                gnSQL = gnSQL & " AND CTBRA_ID IN('015')"
        End Select

        Dim mystrCONN As String = CType(ConfigurationManager.AppSettings("APPCONN"), String)
        mystrCONN = "Provider=SQLOLEDB;" & gnGET_CONN_STRING()

        Dim objOLEConn As New OleDbConnection(mystrCONN)
        Dim objOLECmd As OleDbCommand = New OleDbCommand(gnSQL, objOLEConn)

        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        Select Case RTrim(pvCODE)
            Case "XXX"
            Case Else
                objOLECmd.CommandType = Data.CommandType.Text
        End Select

        'Try
        'open connection to database
        objOLEConn.Open()

        Dim objOLEDR As OleDbDataReader
        objOLEDR = objOLECmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
        If (objOLEDR.Read()) Then
            Select Case RTrim(pvCODE)
                Case "INSURED_CODE"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("CTINSRD_LONG_DESCR") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        pvRef_Misc.Text = RTrim(CType(objOLEDR("CTINSRD_OTHERNAME") & vbNullString, String))
                    End If
                    If pvRef_Misc2 Is Nothing Then
                    Else
                        pvRef_Misc2.Text = RTrim(CType(objOLEDR("CTINSRD_ADDR1") & vbNullString, String))
                    End If
                    If pvRef_Misc3 Is Nothing Then
                    Else
                        pvRef_Misc3.Text = RTrim(CType(objOLEDR("CTINSRD_ADDR2") & vbNullString, String))
                    End If
                    If pvRef_Misc4 Is Nothing Then
                    Else
                        pvRef_Misc4.Text = RTrim(CType(objOLEDR("CTINSRD_OCCUP") & vbNullString, String))
                    End If
                    If pvRef_Misc5 Is Nothing Then
                    Else
                        pvRef_Misc5.Text = RTrim(CType(objOLEDR("CTINSRD_TEL_NUM1") & vbNullString, String))
                    End If
                    If pvRef_Misc6 Is Nothing Then
                    Else
                        pvRef_Misc6.Text = RTrim(CType(objOLEDR("CTINSRD_GSM_NUM") & vbNullString, String))
                    End If
                    If pvRef_Misc7 Is Nothing Then
                    Else
                        pvRef_Misc7.Text = RTrim(CType(objOLEDR("CTINSRD_EMAIL_NUM") & vbNullString, String))
                    End If
                    If pvRef_Misc8 Is Nothing Then
                    Else
                        pvRef_Misc8.Text = RTrim(CType(objOLEDR("CTINSRD_TYPE") & vbNullString, String))
                    End If
                    pvbln = True

                Case "INSURED_CODE_LIFE"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("TBIL_INSRD_SURNAME") & vbNullString, String)) & _
                      " " & RTrim(CType(objOLEDR("TBIL_INSRD_FIRSTNAME") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        'pvRef_Misc.Text = RTrim(CType(objOLEDR("TBIL_INSRD_FIRSTNAME") & vbNullString, String))
                    End If


                Case "POLICY"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("PT_INS_NAME") & vbNullString, String)) & " " & RTrim(CType(objOLEDR("PT_INS_OTHERNAME") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        pvRef_Misc.Text = RTrim(CType(objOLEDR("PT_INS_NUM") & vbNullString, String))
                    End If
                    If pvRef_Misc2 Is Nothing Then
                    Else
                        pvRef_Misc2.Text = RTrim(CType(objOLEDR("PT_RISK_NUM") & vbNullString, String))
                    End If
                    If pvRef_Misc3 Is Nothing Then
                    Else
                        pvRef_Misc3.Text = RTrim(CType(objOLEDR("PT_SUBRISK_NUM") & vbNullString, String))
                    End If
                    If pvRef_Misc4 Is Nothing Then
                    Else
                        If pvRef_Misc4.Text = "" Then
                            If Not IsDBNull(objOLEDR("PT_START_DATE")) Then
                                pvRef_Misc4.Text = Format(CType(objOLEDR("PT_START_DATE"), Date), "MM/dd/yyyy")
                            End If
                        End If
                    End If
                    If pvRef_Misc5 Is Nothing Then
                    Else
                        If pvRef_Misc5.Text = "" Then
                            If Not IsDBNull(objOLEDR("PT_END_DATE")) Then
                                pvRef_Misc5.Text = Format(CType(objOLEDR("PT_END_DATE"), Date), "MM/dd/yyyy")
                            End If
                        End If
                    End If
                    If pvRef_Misc6 Is Nothing Then
                    Else
                        If pvRef_Misc6.Text = "" Then
                            If Not IsDBNull(objOLEDR("PT_RW_START_DATE")) Then
                                pvRef_Misc6.Text = Format(CType(objOLEDR("PT_RW_START_DATE"), Date), "MM/dd/yyyy")
                            End If
                        End If
                    End If
                    If pvRef_Misc7 Is Nothing Then
                    Else
                        If pvRef_Misc.Text <> "" Then
                            pvRef_Misc7.Text = RTrim(CType(objOLEDR("PT_AGCY_NUM") & vbNullString, String))
                        End If
                    End If
                    pvbln = True

                Case "AGENCY_UND", "AGENCY_CODE"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("CTAGCY_NAME") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        pvRef_Misc.Text = RTrim(CType(objOLEDR("CTAGCY_TYPE") & vbNullString, String))
                    End If
                    If pvRef_Misc2 Is Nothing Then
                    Else
                        pvRef_Misc2.Text = RTrim(CType(objOLEDR("CTAGCY_MOBILE_NUM") & vbNullString, String))
                    End If
                    pvbln = True

                Case "AGENCY_UND_LIFE", "AGENCY_CODE_LIFE"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("TBIL_AGCY_AGENT_NAME") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        'pvRef_Misc.Text = RTrim(CType(objOLEDR("TBIL_AGCY_TYPE") & vbNullString, String))
                    End If
                    If pvRef_Misc2 Is Nothing Then
                    Else
                        'pvRef_Misc2.Text = RTrim(CType(objOLEDR("CTAGCY_MOBILE_NUM") & vbNullString, String))
                    End If
                    pvbln = True

                Case "BROKER_UND_LIFE", "BROKER_CODE_LIFE"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("TBIL_CUST_DESC") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        pvRef_Misc.Text = RTrim(CType(objOLEDR("TBIL_CUST_CATG") & vbNullString, String))
                    End If
                    If pvRef_Misc2 Is Nothing Then
                    Else
                        'pvRef_Misc2.Text = RTrim(CType(objOLEDR("CTAGCY_MOBILE_NUM") & vbNullString, String))
                    End If
                    pvbln = True

                Case "ACCOUNT_ACC_UND"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("ACCOUNT_NAME") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        pvRef_Misc.Text = RTrim(CType(objOLEDR("LEDGER_TYPE") & vbNullString, String))
                    End If
                    pvbln = True

                Case "REF_CODE"
                    If pvRef_Misc Is Nothing Then
                    Else
                        If pvRef_Misc.Text <> RTrim(CType(objOLEDR("DNCN_AGCY_NUM") & vbNullString, String)) Then
                        Else
                            pvRef_Text.Text = CType(objOLEDR("DNCN_REF_DATE") & vbNullString, String)
                            If pvRef_Misc2 Is Nothing Then
                            Else
                                pvRef_Misc2.Text = RTrim(CType(objOLEDR("DNCN_POL_NUM") & vbNullString, String))
                            End If
                            If pvRef_Misc3 Is Nothing Then
                            Else
                                pvRef_Misc3.Text = RTrim(CType(objOLEDR("INSURED_NAME") & vbNullString, String))
                            End If
                            If pvRef_Misc4 Is Nothing Then
                            Else
                                pvRef_Misc4.Text = RTrim(CType(objOLEDR("DNCN_INS_NUM") & vbNullString, String))
                            End If
                            If pvRef_Misc5 Is Nothing Then
                            Else
                                pvRef_Misc5.Text = RTrim(CType(objOLEDR("DNCN_RISK_NUM") & vbNullString, String))
                            End If
                            If pvRef_Misc6 Is Nothing Then
                            Else
                                pvRef_Misc6.Text = RTrim(CType(objOLEDR("DNCN_SUBRISK_NUM") & vbNullString, String))
                            End If
                            pvbln = True

                        End If
                    End If

                Case "SECTOR_CODE"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("CTBS_LONG_DESCR") & vbNullString, String))
                    pvbln = True

                Case "SUBRISK_CODE"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("CTSUBRISK_DESCR") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        pvRef_Misc.Text = RTrim(CType(objOLEDR("CTSUBRISK_CLASS") & vbNullString, String))
                    End If
                    pvbln = True

                Case "BRANCH_CODE"
                    pvRef_Text.Text = RTrim(CType(objOLEDR("CTBRA_NAME") & vbNullString, String))
                    If pvRef_Misc Is Nothing Then
                    Else
                        pvRef_Misc.Text = RTrim(CType(objOLEDR("CTLOC_NUM") & vbNullString, String))
                    End If
                    pvbln = True
            End Select

            If objOLEDR.IsClosed = False Then
                objOLEDR.Close()
            End If
            objOLEDR = Nothing
            pvbln = True
        Else
            If objOLEDR.IsClosed = False Then
                objOLEDR.Close()
            End If
            objOLEDR = Nothing
            'MsgBox("Error!. " & "Invalid code. Please enter correct/valid code", MsgBoxStyle.Critical, "Error")
        End If

        'Catch ex As Exception
        'Throw ex
        'MsgBox("Error!. " & Err.Description, MsgBoxStyle.Critical, "Error")
        'Finally

        'End Try

        ' dispose of open objects
        objOLECmd.Dispose()

        If objOLEConn.State = Data.ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Return pvbln

    End Function


    Public Sub PickDate(ByVal PickButton As HtmlInputButton, ByVal DateTextBox As TextBox)
        'Dim g As System.Drawing.SystemColors
        With DateTextBox
            .ReadOnly = True
            '.BackColor = g.Info
            PickButton.Attributes.Add("onClick", "javascript:OpenCalendar('" & .ClientID & "')")
            'PickButton.Attributes.Add("style", "cursor: Hand")
        End With
    End Sub

    Public Function GET_INSURED(ByVal sValue As String) As DataSet

        Dim mystrConn As String = CType("Provider=SQLOLEDB;" + gnGET_CONN_STRING(), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_GET_INSURED"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@PARAM_01", sValue)
        cmd.Parameters.AddWithValue("@PARAM_02", sValue)
        cmd.Parameters.AddWithValue("@PARAM_TYPE", "IND")

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()
            Return ds
        Catch ex As Exception
            '_rtnMessage = "Entry failed! " + ex.Message.ToString()

        End Try
        Return Nothing

    End Function

    Function DoDate_Process(ByVal dateValue As String, ByVal ctrlId As Control) As String()
        Dim rtnMsg(3) As String
        Dim rtnMsg_ As String = Nothing

        'Checking fields for empty values
        If dateValue = "" Then
            rtnMsg_ = " Field is required!"
            rtnMsg(0) = rtnMsg_
            rtnMsg(1) = ctrlId.ID
            Return rtnMsg
        Else
            'Validate date
            Dim myarrData = Split(dateValue, "/")
            'If myarrData.Count <> 3 Then
            If myarrData.Length <> 3 Then
                rtnMsg_ = " Expecting full date in ddmmyyyy format ..."
                rtnMsg_ = "Javascript:alert('" & rtnMsg_ & "')"
                rtnMsg(0) = rtnMsg_
                rtnMsg(1) = ctrlId.ID
                Return rtnMsg
                'Exit Function
            End If
            Dim strMyDay = myarrData(0)
            Dim strMyMth = myarrData(1)
            Dim strMyYear = myarrData(2)

            strMyDay = CType(Format(Val(strMyDay), "00"), String)
            strMyMth = CType(Format(Val(strMyMth), "00"), String)
            strMyYear = CType(Format(Val(strMyYear), "0000"), String)

            'If Val(strMyYear) < 1999 Then
            '    rtnMsg_ = " year part is less than 1999 ..."
            '    rtnMsg_ = "Javascript:alert('" & rtnMsg_ & "')"
            '    rtnMsg(0) = rtnMsg_
            '    rtnMsg(1) = ctrlId.ID
            '    Return rtnMsg
            '    'Exit Function
            'End If

            Dim strMyDte = Trim(strMyDay) & "/" & Trim(strMyMth) & "/" & Trim(strMyYear)
            'dateValue = Trim(strMyDte)


            Dim blnStatusX = MOD_GEN.gnTest_TransDate(strMyDte)
            If blnStatusX = False Then
                rtnMsg_ = " is not a valid date..."
                rtnMsg_ = "Javascript:alert('" & rtnMsg_ & "');"
                rtnMsg(0) = rtnMsg_
                rtnMsg(1) = ctrlId.ID
                Return rtnMsg
                'Exit Function
            Else
                rtnMsg(2) = CType(strMyDte, String)
                Return rtnMsg
            End If
            dateValue = RTrim(strMyDte)
            'Exit Sub
        End If



        Return rtnMsg
    End Function

    Public Function DoConvertToDbDateFormat(ByVal dateValue As String) As String
        Dim dDate = dateValue.Split(CType("/", Char))
        Dim newDate = dDate(2) + "-" + dDate(1) + "-" + dDate(0)
        Return newDate
    End Function

    Public Function GetRecordOriginator(ByVal UserId As String) As String
        Dim result As String = ""
        Dim objOLEDR As OleDbDataReader
        Dim mystrConn As String = CType("Provider=SQLOLEDB;" + gnGET_CONN_STRING(), String)
        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SELECT * FROM SEC_USER_LIFE_DETAIL WHERE SEC_USER_LOGIN='" + UserId + "'"
        cmd.CommandType = CommandType.Text
        Try
            conn.Open()
            objOLEDR = cmd.ExecuteReader()
            If (objOLEDR.Read()) Then
                result = RTrim(CType(objOLEDR("SEC_USER_NAME") & vbNullString, String))
                conn.Close()
            End If
        Catch ex As Exception
        End Try
        Return result
    End Function

    Public Function removeDateSeperators(ByVal dte As String) As String
        'split to constituents
        Dim dparts() As String
        Dim dy, myday, mt, mth As String
        dparts = Split(Trim(dte), "/")

        If (dparts.Length <> 3) Then
            Return "ERROR: Invalid Date Format!"
        Else
            dy = dparts(0)
            myday = dparts(0)
            mt = dparts(1)
            mth = dparts(1)
            If (mt.Length = 1) Then
                mth = "0" + mt

                If (dy.Length = 1) Then
                    myday = "0" + dy
                End If
            End If
        End If
        Dim ky = dparts(2) + mth + myday
        Return ky
    End Function

End Module
