Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Word


Partial Class I_LIFE_PD_IL001
    Inherits System.Web.UI.Page

    Protected FirstMsg As String
    Protected PageLinks As String

    Protected strStatus As String

    Protected strF_ID As String
    Protected strP_ID As String
    Protected strQ_ID As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Protected strRptName As String
    Protected strReportFile As String
    Protected strRptTitle As String
    Protected strRptTitle2 As String

    Protected strPolNum As String
    Protected strID As String
    Protected strFT As String

    Protected strProc_Year As String
    Protected strProc_Mth As String
    Protected strProc_Date As String

    Protected STRMENU_TITLE As String
    Protected BufferStr As String
    Dim strErrMsg As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_POLICY_DET"

        '//MonthName(Month(CurrentDate)) + ' ' + ToWords(Year(CurrentDate))
        'left(ToText(Day({SPIL_PS_PRINT;1.TBIL_PS_START_DATE})),2) + ' day of  ' +  'MonthName(Month({SPIL_PS_PRINT;1.TBIL_PS_START_DATE}))

        PageLinks = "<a href='PRG_LI_PROP_POLICY.aspx' class='a_sub_menu'>Return to Menu</a>&nbsp;"

        Dim strOPT As String = ""
        Try
            strOPT = Page.Request.QueryString("opt").ToString
            'strOPT options = I001
        Catch
            strOPT = "PDI_ERR"
        End Try

        Me.txtDocName.Text = "c:\temp\test1.docx"
        'frmDoc.Attributes.Add("src", "http://localhost/docs/test.doc")

        If Not (Page.IsPostBack()) Then
            'Me.txtPro_Pol_Num.Text = "PI/2014/1501/E/E003/I/0000002"
            Call DoProc_CreateDataSource("IL_PRODUCT_CAT_LIST", Trim("I"), Me.cboProductClass)
        End If

        Select Case UCase(Trim(strOPT))
            Case "I001"
                STRMENU_TITLE = UCase("+++ Policy Document +++ ")
                BufferStr = ""
            Case Else
                STRMENU_TITLE = UCase("+++ Policy Document +++ ")
                BufferStr = ""
        End Select


    End Sub

    'Private Function CreateDataSource(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvField_Text As String, ByVal pvField_Value As String) As ICollection
    Private Sub DoProc_CreateDataSource(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboDDList As DropDownList)

        pvcboDDList.Items.Clear()

        Dim pvField_Text As String = "MyFld_Text"
        Dim pvField_Value As String = "MyFld_Value"

        ' Create a table to store data for the DropDownList control.
        Dim obj_dt As System.Data.DataTable = New System.Data.DataTable()
        Dim obj_dr As DataRow
        Dim obj_dv As DataView

        ' Define the columns of the table.
        obj_dt.Columns.Add(New DataColumn(pvField_Text, GetType(String)))
        obj_dt.Columns.Add(New DataColumn(pvField_Value, GetType(String)))


        obj_dr = obj_dt.NewRow()
        obj_dr(pvField_Text) = "*** Select item ***"
        obj_dr(pvField_Value) = "0"

        obj_dt.Rows.Add(obj_dr)

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
            obj_dv = New DataView(obj_dt)
            'Return obj_dv
            Exit Sub
        End Try


        Dim objOLECmd As OleDbCommand
        Dim objOLEDR As OleDbDataReader

        Select Case UCase(Trim(pvCODE))
            Case "IL_PRODUCT_CAT_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT RTRIM(TBIL_PRDCT_CAT_MDLE) + '=' + RTRIM(TBIL_PRDCT_CAT_CD) AS MyFld_Value, TBIL_PRDCT_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_CAT_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE = '" & RTrim("I") & "'"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_DESC"

            Case "GL_PRODUCT_CAT_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_CAT")
                strSQL = ""
                strSQL = strSQL & "SELECT RTRIM(TBIL_PRDCT_CAT_MDLE) + '=' + RTRIM(TBIL_PRDCT_CAT_CD) AS MyFld_Value, TBIL_PRDCT_CAT_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_CAT_MDLE = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " OR TBIL_PRDCT_CAT_MDLE = '" & RTrim("G") & "'"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_DESC"

            Case "IL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','I')"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_DESC"

            Case "GL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','G')"
                'strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_CODE"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DTL_DESC"

        End Select

        objOLECmd = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandType = CommandType.Text
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 50).Value = strREC_ID

        objOLEDR = objOLECmd.ExecuteReader()

        Do While objOLEDR.Read
            obj_dr = obj_dt.NewRow()
            obj_dr(pvField_Text) = RTrim(CType(objOLEDR("MyFld_Text") & vbNullString, String))
            obj_dr(pvField_Value) = RTrim(CType(objOLEDR("MyFld_Value") & vbNullString, String))

            obj_dt.Rows.Add(obj_dr)
        Loop

        obj_dt.AcceptChanges()


        objOLECmd = Nothing
        objOLEDR = Nothing

        Try
            'close connection to database
            objOLEConn.Close()
        Catch ex As Exception
            'Me.textMessage.Text = "Unable to connect to database. Reason: " & ex.Message
            'FirstMsg = "Javascript:alert('" & Me.textMessage.Text & "')"
            'Me.lblMsg.Text = ex.Message.ToString
        End Try

        objOLEConn = Nothing

        ' Create a DataView from the DataTable to act as the data source
        ' for the DropDownList control.
        obj_dv = New DataView(obj_dt)
        obj_dv.Sort = pvField_Value


        pvcboDDList.DataSource = obj_dv
        pvcboDDList.DataTextField = pvField_Text
        pvcboDDList.DataValueField = pvField_Value

        ' Bind the data to the control.
        pvcboDDList.DataBind()

        ' Set the default selected item, if desired.
        pvcboDDList.SelectedIndex = 0

        'Return obj_dv

    End Sub

    Protected Sub DoProc_ProductClass_Change()
        Call gnGET_SelectedItem(Me.cboProductClass, Me.txtProductClassX, Me.txtProductClassX_Name)

    End Sub

    Protected Sub BUT_OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        'Call MyMS_Word_Open()
        'Call MyMS_Word_App()

        If Trim(Me.txtPro_Pol_Num.Text) = "" Then
            Me.lblMsg.Text = "Missing policy number. Please enter valid policy number..."
            FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
            Exit Sub
        Else
            Me.lblMsg.Text = "Status..."
            'ClientScript.RegisterStartupScript(Me.GetType(), "Popup_MSOLE", "MyOpen_MS_Word('" & Me.txtDocName.Text & "');", True)
        End If


        strF_ID = ""
        strF_ID = RTrim(Me.txtPro_Pol_Num.Text)

        Dim strGET_WHAT As String = "GET_POLICY_BY_FILE_NO"
        strGET_WHAT = "GET_POLICY_BY_POLICY_NO"
        Dim oAL As ArrayList = MOD_GEN.gnGET_RECORD(strGET_WHAT, RTrim(strF_ID), RTrim(""), RTrim(""))
        If oAL.Item(0) = "TRUE" Then
            '    'Retrieve the record
            '    Response.Write("<br/>Status: " & oAL.Item(0))
            '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))

            'Me.txtQuote_Num.Text = oAL.Item(3)
            'Me.txtPolNum.Text = oAL.Item(4)
            Me.txtProductClass.Text = oAL.Item(5)
            Me.txtProduct_Num.Text = oAL.Item(6)
            'Me.txtPlan_Num.Text = oAL.Item(7)
            'Me.txtCover_Num.Text = oAL.Item(8)
            Me.txtPrem_Rate_Code.Text = oAL.Item(14)
            oAL = Nothing
        Else
            '    'Destroy i.e remove the array list object from memory
            '    Response.Write("<br/>Status: " & oAL.Item(0))
            Me.lblMsg.Text = "Status: " & oAL.Item(1)
            oAL = Nothing
            Exit Sub
        End If
        oAL = Nothing


        strRptName = "PD_IL001"
        strRptName = ""

        Select Case UCase(Trim(Me.txtProduct_Num.Text))
            'Annuity

            'PI/2014/1104/E/E003/I/0000002
            'Endowment
            Case "E001"
                strRptName = "RPS_IL_EDUCATION_ENDOWMENT"
            Case "E003"
                strRptName = "RPS_IL_LIFE_TIME_HARVEST"

                'Funeral
            Case "F001", "F002"
                strRptName = "RPS_IL_FUNERAL"

                'Investment
            Case "I001"
                strRptName = "RPS_IL_CAPITAL_BUILDER"
                Select Case UCase(Trim(Me.txtPrem_Rate_Code.Text))
                    Case "071"   'with life
                        strRptName = "RPS_IL_CAPITAL_BUILDER_LIFE"
                End Select
            Case "I003"
                strRptName = "RPS_IL_ESUSU"
            Case "I004"
                strRptName = "RPS_IL_PERSONAL_PROVIDENT"
            Case "I005"
                strRptName = "RPS_IL_IPP"

                'Protection
            Case "P001"
                strRptName = "RPS_IL_MORTGAGE"
            Case "P002"
                strRptName = "RPS_IL_TERM_ASSURANCE"
            Case "P004"
                strRptName = "RPS_IL_CREDIT_LIFE"
            Case "P005"
                strRptName = "RPS_IL_WHOLE_LIFE"
            Case "P006"
                strRptName = "RPS_IL_TUITION"

            Case Else
                Me.lblMsg.Text = "Status: Unable to get POLICY DOCUMENT report..."
                Exit Sub
        End Select

        'strRptName = "Report2"
        'strRptName = "Report5"

        If Trim(strRptName) = "" Then
            Me.lblMsg.Text = "Status: Missing POLICY DOCUMENT report..."
            Exit Sub
        End If


        strID = RTrim("Y")
        strPolNum = RTrim(Me.txtPro_Pol_Num.Text)
        strProc_Date = ""

        'strRptTitle = "POLICY SCHEDULE " & gnGET_RPT_YYYYMM(Me.txtRptDate.Text)
        strRptTitle = "POLICY SCHEDULE "
        strRptTitle2 = "Report Title 2"
        strReportFile = "PD_IL001"
        strReportFile = strRptName

        gnCOMP_NAME = UCase(CType(Session("CL_COMP_NAME"), String))

        Dim strReportParam As String = ""
        strReportParam = strReportParam & "&rptparams=" & gnCOMP_NAME & "<*>" & RTrim(strRptTitle) & "<*>" & strRptTitle2
        strReportParam = strReportParam & "&dbparams=" & RTrim(strPolNum)
        strReportParam = strReportParam & "<*>" & RTrim(strID)

        Dim mystrURL As String = ""

        Try

            mystrURL = "window.open('" & "CRViewer.aspx?rptname=" & RTrim(strReportFile) & strReportParam & "','','left=50,top=50,width=1024,height=650,titlebar=yes,z-lock=yes,address=yes,channelmode=1,fullscreen=no,directories=yes,location=yes,toolbar=yes,menubar=yes,status=yes,scrollbars=1,resizable=yes');"
            FirstMsg = "javascript:" & mystrURL
        Catch ex As Exception
            Me.lblMsg.Text = "<br />Unable to connect to report viewer. <br />Reason: " & ex.Message.ToString

        End Try

    End Sub

    Private Sub MyMS_Word_Open()
        Dim myword_app As Microsoft.Office.Interop.Word.Application
        myword_app = New Microsoft.Office.Interop.Word.Application

        Dim missingType As Object = Type.Missing
        Dim sFile As String = "C:\TEMP\TEST1.DOC"
        Dim sFileText As String = ""

        myword_app.Visible = False

        'myword_app.Documents.Open(sFile)
        myword_app.Documents.Open(sFile)
        'sFileText = myword_app.Documents.Open(sFile).Content.Text
        sFileText = myword_app.Documents(sFile).Content.Text
        myword_app.Visible = False

        Me.div_doc.InnerHtml = "Word Document"
        Me.div_doc.InnerHtml = sFileText

        'Close the word document

        'myword_app.Documents.Close(missingType, missingType, missingType)
        myword_app.Documents.Close()
        myword_app.Quit()
        myword_app.Application.Quit()
        myword_app = Nothing

    End Sub

    Private Sub MyMS_Word_App()


        Dim myword_app As Microsoft.Office.Interop.Word.Application
        myword_app = New Microsoft.Office.Interop.Word.Application

        '	To create a new document based on a custom template
        '•	Use the Add method of the Documents collection to create a new document based on Normal.dot.
        'myword_app.Documents.Add(Template:="C:\Test\SampleTemplate.Dot")


        '	How to: Create New Documents
        myword_app.Documents.Add()

        '	How to: Open Existing Documents
        'myword_app.Documents.Open("C:\Test\NewDocument.doc")
        'myword_app.Documents.Open(FileName:="C:\Test\NewDocument.doc", ReadOnly:=True)
        'myword_app.Documents.Open("C:\Afrik\Doc1.docx")


        With myword_app
            .Visible = True
            .Selection.Font.Name = "Arial"
            .Selection.Font.Size = 10
            .Selection.Font.Bold = True
            .Selection.Font.Underline = Word.WdUnderline.wdUnderlineNone
            .Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            .Selection.TypeText("Welcome Office Document Demonstration...")
        End With

        Dim MyTable As Word.Table
        'Dim MyCells As Word.Cells
        Dim MyCell As Word.Cell
        Dim MyCols As Word.Columns

        Dim intRows As Long
        Dim intCols As Long
        Dim intR As Long
        Dim intR1 As Long

        Dim myText As String

        intRows = 0
        intRows = intRows + 7
        intCols = 5

        myText = ""


        'Create table header
        MyTable = myword_app.Selection.Tables.Add(myword_app.Selection.Range, intRows, intCols)
        MyCols = MyTable.Columns
        MyCols(1).Width = 80
        MyCols(2).Width = 80
        MyCols(3).Width = 80
        MyCols(4).Width = 80
        MyCols(5).Width = 80


        MyCell = MyTable.Cell(1, 1)
        MyCell.Select()
        'With MyCell.Range
        '.Font.Bold = True
        '.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
        'End With

        myword_app.Selection.Font.Size = 10
        myword_app.Selection.Font.Bold = True
        myText = "COL-1"
        myword_app.Selection.TypeText(myText)

        MyCell = MyTable.Cell(1, 2)
        MyCell.Select()
        myword_app.Selection.Font.Bold = True
        myText = "Col-2"
        myword_app.Selection.TypeText(myText)

        MyCell = MyTable.Cell(1, 3)
        MyCell.Select()
        myword_app.Selection.Font.Bold = True
        myText = "COL-3"
        myword_app.Selection.TypeText(myText)

        MyCell = MyTable.Cell(1, 4)
        MyCell.Select()
        myword_app.Selection.Font.Bold = True
        myText = "COL-4"
        myword_app.Selection.TypeText(myText)

        MyCell = MyTable.Cell(1, 5)
        MyCell.Select()
        myword_app.Selection.Font.Bold = True
        myText = "COL-5"
        myword_app.Selection.TypeText(myText)

        intR = 2
        intR1 = intR

        For intR = intR1 To intR1 + 5

            For intCols = 1 To 5
                MyCell = MyTable.Cell(intR, intCols)
                MyCell.Select()
                myword_app.Selection.Font.Size = 8
                myword_app.Selection.Font.Bold = False

                myText = "row: " & intR - 1 & " col: " & intCols
                myword_app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                myword_app.Selection.TypeText(myText)

            Next
        Next

        myText = ""
        'myword_app.Selection.GoToNext(Word.WdGoToItem.wdGoToLine)
        myword_app.Selection.GoToNext(Word.WdGoToItem.wdGoToPage)
        myword_app.Selection.Font.Bold = False
        myword_app.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
        'myword_app.Selection.TypeText(myText)



        'Me.Application.ActiveDocument.Save()

        'Me.Application.Documents("C:\Test\NewDocument.doc").Save()

        'Me.Close(Word.WdSaveOptions.wdDoNotSaveChanges)

        MyTable = Nothing
        'MyCells  = Nothing
        MyCell = Nothing
        MyCols = Nothing

        'myword_app.Quit()
        'myword_app.Application.Quit()
        myword_app = Nothing

    End Sub

    Private Sub MyDoc_PDF(ByVal sfile As String)

        Dim documentType As String = String.Empty

        'Check whether FileUpload control has file  
        'If FileUpload1.HasFile Then

        '    '//Calculate size of file to be uploaded
        '    'Dim fileSize As Integer = FileUpload1.PostedFile.ContentLength

        '    Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

        '    'Dim fileExtension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

        '    'provide document type based on it's extension 
        '    Select Case fileExtension
        '        Case ".pdf"
        '            documentType = "application/pdf"
        '            Exit Select
        '        Case ".xls"
        '            documentType = "application/vnd.ms-excel"
        '            Exit Select
        '        Case ".xlsx"
        '            documentType = "application/vnd.ms-excel"
        '            Exit Select
        '        Case ".doc"
        '            documentType = "application/vnd.ms-word"
        '            Exit Select
        '        Case ".docx"
        '            documentType = "application/vnd.ms-word"
        '            Exit Select
        '        Case ".gif"
        '            documentType = "image/gif"
        '            Exit Select
        '        Case ".png"
        '            documentType = "image/png"
        '            Exit Select
        '        Case ".jpg"
        '            documentType = "image/jpg"
        '            Exit Select
        '    End Select


        '    'Dim documentBinary As Byte() = New Byte(fileSize - 1) {}
        '    'FileUpload1.PostedFile.InputStream.Read(documentBinary, 0, fileSize)

        '    'Dim uploadedDocument As New SqlParameter("@DocData", SqlDbType.Binary, fileSize)
        '    'uploadedDocument.Value = documentBinary
        '    'cmd.Parameters.Add(uploadedDocument)

        'End If

        Dim strCONN_STRING As String = "data source=server_name; initial catalog=database_name; user id=database_user_id; password=***;"
        Dim myCon As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(strCONN_STRING)

        Try
            myCon.Open()

        Catch ex As Exception
            Console.Write("Error has occured: Reason: " & ex.Message)
            myCon = Nothing
            Exit Sub
        End Try

        Try
            'Read from binary a file
            Dim fs As FileStream
            fs = New FileStream(sfile, FileMode.Open, FileAccess.Read)
            Dim docByte As Byte() = New Byte(fs.Length - 1) {}
            fs.Read(docByte, 0, System.Convert.ToInt32(fs.Length))
            fs.Close()
            fs.Dispose()


            ' Write into a binary file
            'Dim fStream As FileStream = New FileStream("tmp_" & sfile, FileMode.Create, FileAccess.Write)
            'fStream.Write(docByte, 0, docByte.Length)
            'fStream.Close()
            'fStream.Dispose()


            'store doc as Binary value using SQLParameter
            Dim docfile As New System.Data.SqlClient.SqlParameter
            docfile.SqlDbType = Data.SqlDbType.Binary
            docfile.ParameterName = "fdoc"
            docfile.Value = docByte


            'Insert statement for sql query
            Dim sqltxt As String
            sqltxt = "insert into doc_tbl(fld_file_id, fld_file_data) values('123',@fdoc)"

            Dim sqlcmd As System.Data.SqlClient.SqlCommand
            sqlcmd = New System.Data.SqlClient.SqlCommand(sqltxt, myCon)
            sqlcmd.Parameters.Add(docfile)
            sqlcmd.ExecuteNonQuery()

            sqlcmd.Dispose()
            sqlcmd = Nothing
            Console.Write("File saved successfully - " & sfile)

        Catch ex As Exception
            Console.Write("Error has occured: Reason: " & ex.Message)
        End Try


        'To retrieve it is exactly the same process you use for any other database access:

        Dim fileName As String = Request.QueryString("name").ToString()

        Dim fileData As Byte()
        Using mycom As New System.Data.SqlClient.SqlCommand("SELECT fld_file_id, fld_file_data FROM doc_tbl", myCon)
            Using reader As System.Data.SqlClient.SqlDataReader = mycom.ExecuteReader()
                While reader.Read()
                    fileName = DirectCast(reader("fld_file_id"), String)
                    fileData = DirectCast(reader("fld_file_data"), Byte())

                End While
            End Using
        End Using


        ' Write into a binary file
        'Dim fStream As FileStream = New FileStream("tmp_" & sfile, FileMode.Create, FileAccess.Write)
        'fStream.Write(fileData, 0, fileData.Length)
        'fStream.Close()
        'fStream.Dispose()

        fileData = Nothing

        myCon.Close()
        myCon = Nothing



        'Dim fileName As String = Request.QueryString("name").ToString()

        'Dim strExtenstion As String = "extension of retrieved file"

        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)

        'If (strExtenstion = ".doc" Or strExtenstion = ".docx") Then
        '    Response.ContentType = "application/vnd.ms-word"
        '    Response.AddHeader("content-disposition", "attachment;filename=Tr.doc")
        'ElseIf (strExtenstion = ".xls" Or strExtenstion = ".xlsx") Then
        '    Response.ContentType = "application/vnd.ms-excel"
        '    Response.AddHeader("content-disposition", "attachment;filename=Tr.xls")
        'ElseIf (strExtenstion = ".pdf") Then
        '    Response.ContentType = "application/pdf"
        '    Response.AddHeader("content-disposition", "attachment;filename=Tr.pdf")
        'End If


        'Response.TransmitFile(Server.MapPath("~/Files/" & fileName))

        'Response.Charset = ""
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ''// If you write,
        ''// Response.Write(bytFile1)
        ''// then you will get only 13 byte in bytFile.
        ''Response.BinaryWrite(bytFile)

        'Response.[End]()
        ''Response.End()

    End Sub


    Private Sub MyDoc_File()
        '//Create word document

        Dim mydocument As Document = New Document()
        mydocument.LoadFromFile("..\wordtohtml.doc")

        '//Save doc file to html
        'mydocument.SaveToFile("toHTML.html", FileFormat.Html)
        WordDocViewer("toHTML.html")
    End Sub



    Private Sub WordDocViewer(ByVal fileName As String)
        Try
            System.Diagnostics.Process.Start(fileName)
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If LTrim(RTrim(Me.txtSearch.Value)) = "Search..." Then
        ElseIf LTrim(RTrim(Me.txtSearch.Value)) <> "" Then
            Call gnProc_Populate_Box("IL_ASSURED_HELP_SP", "001", Me.cboSearch, RTrim(Me.txtSearch.Value))
        End If

    End Sub

    Protected Sub cboSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSearch.SelectedIndexChanged
        Try
            If Me.cboSearch.SelectedIndex = -1 Or Me.cboSearch.SelectedIndex = 0 Or _
            Me.cboSearch.SelectedItem.Value = "" Or Me.cboSearch.SelectedItem.Value = "*" Then
                Me.txtFileNum.Text = ""
                'Me.txtQuote_Num.Text = ""
                'Me.txtPolNum.Text = ""
                'Me.txtSearch.Value = ""
            Else
                Me.txtFileNum.Text = Me.cboSearch.SelectedItem.Value
                If LTrim(RTrim(Me.txtFileNum.Text)) <> "" Then
                    strStatus = Proc_DoOpenRecord(RTrim("FIL"), Me.txtFileNum.Text, RTrim("0"))
                End If
            End If
        Catch ex As Exception
            Me.lblMsg.Text = "Error. Reason: " & ex.Message.ToString
        End Try

    End Sub


    Private Function Proc_DoOpenRecord(ByVal FVstrGetType As String, ByVal FVstrRefNum As String, Optional ByVal FVstrRecNo As String = "", Optional ByVal strSearchByWhat As String = "FILE_NUM") As String

        strErrMsg = "false"

        lblMsg.Text = ""
        If Trim(FVstrRefNum) = "" Then
            Return strErrMsg
            Exit Function
        End If

        Dim mystrCONN As String = CType(Session("connstr"), String)
        Dim objOLEConn As New OleDbConnection(mystrCONN)

        Try
            'open connection to database
            objOLEConn.Open()
        Catch ex As Exception
            Me.lblMsg.Text = "Unable to connect to database. Reason: " & ex.Message
            objOLEConn = Nothing
            Return strErrMsg
            Exit Function
        End Try


        strREC_ID = Trim(FVstrRefNum)

        strTable = strTableName
        strSQL = ""
        strSQL = strSQL & "SELECT TOP 1 PT.*"
        strSQL = strSQL & " FROM " & strTable & " AS PT"
        strSQL = strSQL & " WHERE PT.TBIL_POLY_FILE_NO = '" & RTrim(strREC_ID) & "'"
        If Val(LTrim(RTrim(FVstrRecNo))) <> 0 Then
            strSQL = strSQL & " AND PT.TBIL_POLY_REC_ID = '" & Val(FVstrRecNo) & "'"
        End If
        'strSQL = strSQL & " AND PT.TBIL_POLY_PROPSAL_NO = '" & RTrim(strQ_ID) & "'"
        'strSQL = strSQL & " AND PT.TBIL_POLY_POLICY_NO = '" & RTrim(strP_ID) & "'"


        'strSQL = "SPIL_GET_POLICY_DET"

        Dim objOLECmd As OleDbCommand = New OleDbCommand(strSQL, objOLEConn)
        objOLECmd.CommandTimeout = 180
        ''objOLECmd.CommandType = CommandType.Text
        'objOLECmd.CommandType = CommandType.StoredProcedure
        'objOLECmd.Parameters.Add("p01", OleDbType.VarChar, 3).Value = LTrim(RTrim(FVstrGetType))
        'objOLECmd.Parameters.Add("p02", OleDbType.VarChar, 40).Value = strREC_ID
        'objOLECmd.Parameters.Add("p03", OleDbType.VarChar, 18).Value = Val(FVstrRecNo)

        Dim objOLEDR As OleDbDataReader

        objOLEDR = objOLECmd.ExecuteReader()
        If (objOLEDR.Read()) Then
            strErrMsg = "true"

            Me.txtFileNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_FILE_NO") & vbNullString, String))
            'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))
            'Me.txtRecNo.Text = RTrim(CType(objOLEDR("TBIL_POLY_REC_ID") & vbNullString, String))

            'Me.txtQuote_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PROPSAL_NO") & vbNullString, String))
            'Me.txtPolNum.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))
            Me.txtPro_Pol_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_POLICY_NO") & vbNullString, String))

            'Me.txtProductClass.Text = RTrim(CType(objOLEDR("TBIL_PRDCT_DTL_CAT") & vbNullString, String))
            Me.txtProduct_Num.Text = RTrim(CType(objOLEDR("TBIL_POLY_PRDCT_CD") & vbNullString, String))

            strOPT = "2"
            Me.lblMsg.Text = "Status: Policy No: " & Me.txtPro_Pol_Num.Text

        Else

            Me.txtFileNum.Text = RTrim("")
            Me.txtPro_Pol_Num.Text = RTrim("")

            Me.lblMsg.Text = "Status: New Entry..."
            Me.lblMsg.Text = "Sorry!. Unable to get record ..."
            'FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

            strOPT = "1"

        End If


        ' dispose of open objects
        objOLECmd.Dispose()
        objOLECmd = Nothing

        If objOLEDR.IsClosed = False Then
            objOLEDR.Close()
        End If
        objOLEDR = Nothing

        If objOLEConn.State = ConnectionState.Open Then
            objOLEConn.Close()
        End If
        objOLEConn = Nothing

        Return strErrMsg

    End Function

End Class
