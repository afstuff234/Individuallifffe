Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class I_LIFE_PRG_LI_INV_PLUS
    Inherits System.Web.UI.Page


    '            FirstMsg = "vbscript:call DoPopReport('" & ReportPage & "', 800, 600)"

    '    Protected TPlateColumn As New TemplateColumn()

    '            MasterTable = "SELECT * FROM TBIL_POLICY_DET Where TBIL_POLICY_TYPE = 'WL' AND TBIL_POLICY_FLAG = 'IL'"
    '            MasterTable += "order By TBIL_POLY_POLICY_NO"

    '            KeyField = "TBIL_POLY_POLICY_NO"
    '            KeyTextField = "TBIL_POLY_POLICY_NO"

    '<asp:datagrid id="DataGrid1" runat="server" CssClass="GridStyle" width="100%" AllowPaging="True" AutoGenerateColumns="False">
    '	<SelectedItemStyle CssClass="GridSelItem"></SelectedItemStyle>
    '	<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
    '	<ItemStyle CssClass="GridItem"></ItemStyle>
    '	<HeaderStyle CssClass="GridHeader"></HeaderStyle>
    '	<FooterStyle CssClass="GridFooter"></FooterStyle>
    '	<Columns>
    '		<asp:TemplateColumn>
    '			<HeaderTemplate>
    '				<B>Policy No</B>
    '			</HeaderTemplate>
    '			<ItemTemplate>
    '				<asp:CheckBox id="Chk1" runat="server"></asp:CheckBox>
    '				<asp:Label ID="Key1" Visible="False" Runat="server" text="<%# DataBinder.Eval(Container.DataItem, KeyField) %>" />
    '				<a href="<%=A1%><%# DataBinder.Eval(Container.DataItem, KeyField) %><%=Z1%>">
    '				<%# DataBinder.Eval(Container.DataItem, KeyTextField) %>
    '				</a>
    '			</ItemTemplate>
    '		</asp:TemplateColumn>
    '	</Columns>
    '	<PagerStyle CssClass="GridPager" Mode="NumericPages"></PagerStyle>
    '</asp:datagrid>

    Protected FirstMsg As String
    Protected PageLinks As String

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    'Protected BufferStr As String

    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strP_ID As String
    Protected strQ_ID As String

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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strTableName = "TBIL_POLICY_DET"

        STRMENU_TITLE = "Proposal Screen"
        'STRMENU_TITLE = "Investment Plus Proposal"

        Try
            strP_ID = CType(Request.QueryString("optpolid"), String)
            'strP_DESC = CType(Request.QueryString("optd"), String)
        Catch ex As Exception
            strP_ID = ""
        End Try
        Try
            strQ_ID = CType(Request.QueryString("optquotid"), String)
        Catch ex As Exception
            strQ_ID = ""
        End Try


        ' Load data for the DropDownList control only once, when the 
        ' page is first loaded.
        If Not (Page.IsPostBack) Then
            Me.cmdPrev.Enabled = False
            Me.cmdNext.Enabled = False
            If Trim(strP_ID) <> "" Then
                Me.txtPolNum.Text = RTrim(strP_ID)
            End If
            If Trim(strQ_ID) <> "" Then
                Me.txtQuote_Num.Text = RTrim(strQ_ID)
                Me.cmdNext.Enabled = True
            End If

            Call DoProc_CreateDataSource("IL_PRODUCT_CAT_LIST", Trim("IND"), Me.cboProductClass)
            'Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Trim("101"), Me.cboProduct)
            Call gnProc_Populate_Box("IL_CODE_LIST", "003", Me.cboBranch)
            Call gnProc_Populate_Box("IL_CODE_LIST", "005", Me.cboDepartment)
            Call gnProc_Populate_Box("IL_CODE_LIST", "009", Me.cboReligion)
            Call gnProc_Populate_Box("IL_CODE_LIST", "013", Me.cboBenefry_Relationship)
            Call gnProc_Populate_Box("IL_CODE_LIST", "014", Me.cboGender)
            Call gnProc_Populate_Box("IL_CODE_LIST", "015", Me.cboMaritalStatus)

            Me.lblMsg.Text = "Status:"
        End If


        If Me.txtAction.Text = "New" Then
            Me.txtQuote_Num.Text = ""
            'Call DoNew()
            'Call Proc_OpenRecord(Me.txtNum.Text)
            Me.txtAction.Text = ""
            Me.cboProduct.SelectedIndex = 0
            Me.txtProduct_Num.Text = ""
            Me.lblMsg.Text = "New Entry..."
        End If

        If Me.txtAction.Text = "Save" Then
            Call DoSave()
            Me.txtAction.Text = ""

        End If

        'If Me.txtAction.Text = "Delete" Then
        'Call DoDelete()
        'Me.txtAction.Text = ""
        'End If

        'If Me.txtAction.Text = "Delete_Item" Then
        '    'Call DoDelItem()
        '    Me.txtAction.Text = ""
        'End If

    End Sub



    'Private Function CreateDataSource(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvField_Text As String, ByVal pvField_Value As String) As ICollection
    Private Sub DoProc_CreateDataSource(ByVal pvCODE As String, ByVal pvTransType As String, ByVal pvcboDDList As DropDownList)

        pvcboDDList.Items.Clear()

        Dim pvField_Text As String = "MyFld_Text"
        Dim pvField_Value As String = "MyFld_Value"

        ' Create a table to store data for the DropDownList control.
        Dim obj_dt As DataTable = New DataTable()
        Dim obj_dr As DataRow
        Dim obj_dv As DataView

        ' Define the columns of the table.
        obj_dt.Columns.Add(New DataColumn(pvField_Text, GetType(String)))
        obj_dt.Columns.Add(New DataColumn(pvField_Value, GetType(String)))


        obj_dr = obj_dt.NewRow()
        obj_dr(pvField_Text) = "*** Select product ***"
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
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_CAT_CD"

            Case "IL_PRODUCT_DET_LIST"
                strTable = strTableName
                strTable = RTrim("TBIL_PRODUCT_DETL")
                strSQL = ""
                strSQL = strSQL & "SELECT TBIL_PRDCT_DTL_CODE AS MyFld_Value, TBIL_PRDCT_DTL_DESC AS MyFld_Text"
                strSQL = strSQL & " FROM " & strTable
                strSQL = strSQL & " WHERE TBIL_PRDCT_DTL_CAT = '" & RTrim(pvTransType) & "'"
                strSQL = strSQL & " AND TBIL_PRDCT_DTL_MDLE IN('IND','I')"
                strSQL = strSQL & " ORDER BY TBIL_PRDCT_DESC"

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
        Try
            If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Or _
               Me.cboProductClass.SelectedItem.Value = "" Or Me.cboProductClass.SelectedItem.Value = "*" Then
                Me.txtProductClass.Text = ""
                Me.lblMsg.Text = "Missing or Invalid " & Me.lblProductClass.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            Else
                strTmp_Value = Me.cboProductClass.SelectedItem.Value
                myarrData = Split(strTmp_Value, "=")
                If myarrData.Count <> 2 Then
                    Me.lblMsg.Text = "Missing or Invalid " & Me.lblProductClass.Text
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                End If
                Me.txtProductClass.Text = myarrData(1)
                Me.txtProduct_Num.Text = ""
                Call DoProc_CreateDataSource("IL_PRODUCT_DET_LIST", Me.txtProductClass.Text, Me.cboProduct)
            End If
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub DoProc_Branch_Change()
        Try
            If Me.cboBranch.SelectedIndex = -1 Or Me.cboBranch.SelectedIndex = 0 Or _
               Me.cboBranch.SelectedItem.Value = "" Or Me.cboBranch.SelectedItem.Value = "*" Then
                Me.txtBraNum.Text = ""
            Else
                Me.txtBraNum.Text = Me.cboBranch.SelectedItem.Value
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DoProc_Branch_Refresh()
        Call gnProc_Populate_Box("IL_CODE_LIST", "003", Me.cboBranch)

    End Sub

    Protected Sub DoProc_Dept_Change()
        Try
            If Me.cboDepartment.SelectedIndex = -1 Or Me.cboDepartment.SelectedIndex = 0 Or _
               Me.cboDepartment.SelectedItem.Value = "" Or Me.cboDepartment.SelectedItem.Value = "*" Then
                Me.txtDeptNum.Text = ""
            Else
                Me.txtDeptNum.Text = Me.cboDepartment.SelectedItem.Value
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DoProc_Dept_Refresh()
        Call gnProc_Populate_Box("IL_CODE_LIST", "005", Me.cboDepartment)

    End Sub

    Protected Sub DoProc_Assured_Change()
        Try
            If Me.cboAssured.SelectedIndex = -1 Or Me.cboAssured.SelectedIndex = 0 Or _
            Me.cboAssured.SelectedItem.Value = "" Or Me.cboAssured.SelectedItem.Value = "*" Then
                Me.txtAssured_Num.Text = ""
                Me.txtAssured_Name.Text = ""
            Else
                Me.txtAssured_Num.Text = Me.cboAssured.SelectedItem.Value
                Me.txtAssured_Name.Text = Me.cboAssured.SelectedItem.Text
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub DoProc_Assured_Search()
        If RTrim(Me.txtAssured_Search.Text) <> "" Then
            Call gnProc_Populate_Box("IL_ASSURED_LIST", "001", Me.cboAssured, RTrim(Me.txtAssured_Search.Text))
        End If
    End Sub

    Protected Sub DoProc_Agcy_Change()

    End Sub

    Protected Sub DoProc_Agcy_Search()

    End Sub

    Private Sub DoSave()

        Try
            If Me.cboProductClass.SelectedIndex = -1 Or Me.cboProductClass.SelectedIndex = 0 Or _
               Me.cboProductClass.SelectedItem.Value = "" Or Me.cboProductClass.SelectedItem.Value = "*" Then
                Me.txtProductClass.Text = ""
                Me.lblMsg.Text = "Missing or Invalid " & Me.lblProductClass.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            Else
                strTmp_Value = Me.cboProductClass.SelectedItem.Value
                myarrData = Split(strTmp_Value, "=")
                If myarrData.Count <> 2 Then
                    Me.lblMsg.Text = "Missing or Invalid " & Me.lblProductClass.Text
                    FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                    Exit Sub
                End If
                Me.txtProductClass.Text = myarrData(1)
            End If
        Catch ex As Exception

        End Try


        Try
            If Me.cboProduct.SelectedIndex = -1 Or Me.cboProduct.SelectedIndex = 0 Or _
               Me.cboProduct.SelectedItem.Value = "" Or Me.cboProduct.SelectedItem.Value = "*" Then
                Me.txtProduct_Num.Text = ""
                Me.lblMsg.Text = "Missing or Invalid " & Me.lblProduct_Num.Text
                FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"
                Exit Sub
            Else
                strTmp_Value = Me.cboProduct.SelectedItem.Value
                Me.txtProduct_Num.Text = RTrim(strTmp_Value)
            End If
        Catch ex As Exception

        End Try


        If Me.txtProduct_Num.Text = "" Then
            Me.lblMsg.Text = "Missing product code..."
        Else
            Me.lblMsg.Text = "Product OK..."
        End If

        If Trim(Me.txtQuote_Num.Text) = "" Then
            Me.txtQuote_Num.Text = "Q/2013/09/0000001"
        End If


        If Trim(Me.txtPolNum.Text) = "" Then
            Me.txtPolNum.Text = "P/2013/09/0000001"
        End If

        Me.lblMsg.Text = "About to submit data... "
        FirstMsg = "Javascript:alert('" & Me.lblMsg.Text & "')"

        Me.cmdNext.Enabled = True
    End Sub

    Private Function CreateDataSource_Demo(ByVal pvLV_Text As String, ByVal pvLV_Value As String) As ICollection

        ' Create a table to store data for the DropDownList control.
        Dim dt As DataTable = New DataTable()

        ' Define the columns of the table.
        dt.Columns.Add(New DataColumn(pvLV_Text, GetType(String)))
        dt.Columns.Add(New DataColumn(pvLV_Value, GetType(String)))

        ' Populate the table with sample values.
        dt.Rows.Add(CreateRow("Select product", "0", dt))
        dt.Rows.Add(CreateRow("Investment Plus", "A001", dt))
        dt.Rows.Add(CreateRow("Personal Provident", "A002", dt))
        dt.Rows.Add(CreateRow("Capital Builder", "A003", dt))
        dt.Rows.Add(CreateRow("Dollar Linked", "A004", dt))
        dt.Rows.Add(CreateRow("Unit Link Plan", "A005", dt))
        dt.Rows.Add(CreateRow("Esusu Shield", "A006", dt))

        ' Create a DataView from the DataTable to act as the data source
        ' for the DropDownList control.
        Dim dv As DataView = New DataView(dt)
        Return dv

    End Function

    Private Function CreateRow(ByVal Text As String, ByVal Value As String, ByVal dt As DataTable) As DataRow

        ' Create a DataRow using the DataTable defined in the 
        ' CreateDataSource method.
        Dim dr As DataRow = dt.NewRow()

        ' This DataRow contains the ColorTextField and ColorValueField 
        ' fields, as defined in the CreateDataSource method. Set the 
        ' fields with the appropriate value. Remember that column 0 
        ' is defined as ColorTextField, and column 1 is defined as 
        ' ColorValueField.
        dr(0) = Text
        dr(1) = Value

        Return dr

    End Function


    '    Sub BindGrid()
    '        Dim Conn As OleDbConnection = New OleDbConnection(ConnString)
    '        Dim myDA As OleDbDataAdapter = New OleDbDataAdapter(MasterTable, Conn)

    '        Dim custDS As DataSet = New DataSet()
    '        myDA.Fill(custDS)

    '        TPlateColumn = CType(Me.DataGrid1.Columns.Item(0), TemplateColumn)
    '        Me.DataGrid1.Columns.Clear()
    '        '             'field  'caption
    '        Call NewTemplateColumn("", "Proposal No")
    '        Call NewColumn("PropDate", "Proposal Date", "{0:dd-MMM-yyyy}")
    '        Call NewColumn("Surname", "Client Name", "")
    '        Call NewColumn("SumAssured", "Sum Assured", "{0:n2}")
    '        Call NewColumn("Duration", "Term", "")
    '        Call NewColumn("PremPayable", "Premium Payable", "{0:n2}")
    '        'Call NewColumn("Status", "Status", "")

    '        Me.DataGrid1.DataSource = custDS
    '        Me.DataGrid1.DataBind()
    '    End Sub
    '    Sub BindPolicy()
    '        Dim Conn As OleDbConnection = New OleDbConnection(ConnString)
    '        Dim myDA As OleDbDataAdapter = New OleDbDataAdapter(MasterTable, Conn)

    '        Dim custDS As DataSet = New DataSet()
    '        myDA.Fill(custDS)

    '        TPlateColumn = CType(Me.DataGrid1.Columns.Item(0), TemplateColumn)
    '        Me.DataGrid1.Columns.Clear()
    '        '             'field  'caption
    '        Call NewTemplateColumn("", "Policy No")
    '        Call NewColumn("CommencementDate", "Commencement Date", "{0:dd-MMM-yyyy}")
    '        Call NewColumn("Surname", "Client Name", "")
    '        Call NewColumn("SumAssured", "Sum Assured", "{0:n2}")
    '        Call NewColumn("Duration", "Term", "")
    '        Call NewColumn("PremPayable", "Premium Payable", "{0:n2}")
    '        'Call NewColumn("Status", "Status", "")

    '        Me.DataGrid1.DataSource = custDS
    '        Me.DataGrid1.DataBind()
    '    End Sub

    '    Private Sub NewTemplateColumn(ByVal FieldName As String, ByVal HeaderCaption As String)
    '        With TPlateColumn
    '            .HeaderText = HeaderCaption
    '        End With
    '        Me.DataGrid1.Columns.Add(TPlateColumn)
    '    End Sub

    '    Private Sub NewColumn(ByVal FieldName As String, ByVal HeaderCaption As String, ByVal myFormat As String)
    '        Dim NumberColumn As New BoundColumn()

    '        With NumberColumn
    '            .HeaderText = HeaderCaption
    '            .DataField = FieldName
    '            .DataFormatString = myFormat
    '        End With
    '        Me.DataGrid1.Columns.Add(NumberColumn)
    '    End Sub




    '    Private Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '        DataGrid1.CurrentPageIndex = e.NewPageIndex
    '        BindGrid()
    '    End Sub


    'Private Sub cmdApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdApply.Click
    '    'Me.Hidden1.Text = Val(Session("PropID"))

    '    Call PUpdate_Date()

    'End Sub


    Private Sub PUpdate_Date()
        '        Dim lngANB As Integer
        '        Dim Dte1 As Date, Dte2 As Date, Dte3 As Date

        '        If Me.txtCommenceDate.Text = "" Or _
        '           Me.txtDateOfBirth.Text = "" Then
        '            GoTo PUpdate_Date1
        '        End If
        '        lngANB = Val(DateDiff("yyyy", Me.txtPropDate.Text, Me.txtDateOfBirth.Text))
        '        If lngANB < 0 Then
        '            lngANB = lngANB * -1
        '        End If
        '        Dte1 = CDate(Me.txtPropDate.Text)
        '        Dte1 = CDate(Me.txtCommenceDate.Text)
        '        Dte2 = CDate(Me.txtDateOfBirth.Text)
        '        If Dte1.Month > Dte2.Month Then
        '            lngANB = lngANB + 1
        '        End If
        '        Me.txtAge.Text = Trim(Str(lngANB))
        '        Dte3 = CDate(Me.txtCommenceDate.Text)
        '        Me.txtMaturityDate.Text = CStr(DateAdd("yyyy", Val(Me.txtDuration.Text), Dte3))

        '        Call PUpdate_Prem()

        'PUpdate_Date1:

    End Sub


    Private Sub PUpdate_Prem()

        '        Dim mySQL As String, myTable As String
        '        Dim myIntModeOfPay As Integer = 0
        '        Dim myIntTerm As Integer = 0
        '        Dim myStrModeOfPay As String
        '        Dim myStrAge As String

        '        Dim mystrPremTermYear As String = 0 '"45"
        '        Dim mystrProfitYN As String = 0 '"PROFIT_LTD"
        '        Dim premtotal As Single = 0
        '        Dim mySgnSA As Single = 0 'Sum Assured
        '        Dim mySgnPremRate As Single = 0 '14.18 'Premium Rate
        '        Dim mySgnFreqRate As Single = 0 '9.0 'Frequency Rate
        '        Dim mySgnStampRate As Single = 0 '1.5 'Stamp Duty Rate
        '        Dim mySgnStampFee As Single = 0
        '        Dim mySgnPolicyFee As Single = 0 '250.0
        '        Dim mySgnAnnualPrem As Single = 0
        '        Dim mySgnBasicPrem As Single = 0

        '        myStrAge = Me.txtAge.Text
        '        myIntTerm = (Me.txtDuration.Text)
        '        mySgnSA = (Me.txtSumAssured.Text)
        '        mySgnPolicyFee = (Me.txtPolicyFee.Text)

        '        myStrModeOfPay = Me.cbPaymentFreqency.SelectedItem.Value
        '        If myStrModeOfPay = "Single" Then 'Single
        '            myIntModeOfPay = 1
        '        ElseIf myStrModeOfPay = "Annually" Then 'Annual
        '            myIntModeOfPay = 1
        '        ElseIf myStrModeOfPay = "Half-Year" Then 'HalfYear
        '            myIntModeOfPay = 2
        '        ElseIf myStrModeOfPay = "Quarterly" Then 'Quarterly
        '            myIntModeOfPay = 4
        '        ElseIf myStrModeOfPay = "Monthly" Then 'Monthly
        '            myIntModeOfPay = 12
        '        End If

        '        If Me.cbPremTermYear.SelectedItem.Value = "LP" Then
        '            mystrPremTermYear = Me.txtPremTermYear.Text
        '        Else
        '            mystrPremTermYear = "0"
        '        End If
        '        If Me.cbPremTermYear.SelectedItem.Value = "LP" And _
        '           Me.cbPolicyPlan.SelectedItem.Value = "WP" Then
        '            mystrProfitYN = "PROFIT_LTD"
        '        End If
        '        If Me.cbPremTermYear.SelectedItem.Value = "LP" And _
        '           Me.cbPolicyPlan.SelectedItem.Value = "NP" Then
        '            mystrProfitYN = "NOPROFIT_LTD"
        '        End If
        '        If Me.cbPremTermYear.SelectedItem.Value = "UP" And _
        '           Me.cbPolicyPlan.SelectedItem.Value = "WP" Then
        '            mystrProfitYN = "PROFIT_ULTD"
        '        End If
        '        If Me.cbPremTermYear.SelectedItem.Value = "UP" And _
        '           Me.cbPolicyPlan.SelectedItem.Value = "NP" Then
        '            mystrProfitYN = "NOPROFIT_ULTD"
        '        End If

        '        Dim ConnX As OleDbConnection = New OleDbConnection(ConnString)
        '        Dim CmddX As OleDbCommand
        '        Dim objDR As OleDbDataReader
        '        Dim ansX As Integer = 0

        '        myTable = "Undstampduty1"
        '        mySQL = "SELECT * FROM " & myTable
        '        'mySQL += " WHERE TransType = '" & mystrProfitYN & "'"
        '        'mySQL += " AND Age = '" & myStrAge & "'"
        '        'mySQL += " AND PremTermYear = '" & mystrPremTermYear & "'"

        '        ConnX.Open()
        '        CmddX = New OleDbCommand(mySQL, ConnX)
        '        CmddX.CommandType = CommandType.Text
        '        objDR = CmddX.ExecuteReader(CommandBehavior.CloseConnection)
        '        If (objDR.Read()) Then
        '            mySgnStampRate = CType(objDR("Stampduty"), Single)
        '        Else
        '            'mySgnPremRate = 0
        '        End If
        '        objDR.Close()
        '        CmddX.Dispose()
        '        'ConnX.Open()
        '        'CmddX = New OleDbCommand("mktTHISProposal", ConnX)
        '        'CmddX.CommandType = CommandType.StoredProcedure
        '        'CmddX.Parameters.Clear()
        '        'CmddX.Parameters.Add("myCode", OleDbType.VarChar, 50).Value = Me.txtPropNo.Text
        '        'CmddX.Parameters.Add("myCode1", OleDbType.VarChar, 50).Value = ""
        '        'ansX += CmddX.ExecuteNonQuery()

        '        'To get Whole life premium rate
        '        myTable = "LifWholeLifePremRate"
        '        mySQL = "SELECT * FROM " & myTable
        '        mySQL += " WHERE TransType = '" & mystrProfitYN & "'"
        '        mySQL += " AND Age = '" & myStrAge & "'"
        '        mySQL += " AND PremTermYear = '" & mystrPremTermYear & "'"

        '        ConnX.Open()
        '        CmddX = New OleDbCommand(mySQL, ConnX)
        '        CmddX.CommandType = CommandType.Text
        '        objDR = CmddX.ExecuteReader(CommandBehavior.CloseConnection)
        '        If (objDR.Read()) Then
        '            mySgnPremRate = CType(objDR("PremRate"), Single)
        '        Else
        '            mySgnPremRate = 0
        '        End If
        '        objDR.Close()
        '        CmddX.Dispose()

        '        'To get Whole life premium rate
        '        myTable = "LifBasicRate"
        '        mySQL = "SELECT * FROM " & myTable
        '        mySQL += " WHERE Code = '" & myStrModeOfPay & "'"

        '        ConnX.Open()
        '        CmddX = New OleDbCommand(mySQL, ConnX)
        '        CmddX.CommandType = CommandType.Text
        '        objDR = CmddX.ExecuteReader(CommandBehavior.CloseConnection)
        '        If (objDR.Read()) Then
        '            mySgnFreqRate = CType(objDR("BasicRate"), Single)
        '        Else
        '            mySgnFreqRate = 0
        '        End If

        '        objDR.Close()
        '        CmddX.Dispose()

        '        ConnX.Dispose()
        '        ConnX.Close()
        '        Me.txtPremRate.Text = (mySgnPremRate)
        '        Me.txtFreqRate.Text = CStr(mySgnFreqRate)
        '        Me.txtStampDuty.Text = (mySgnStampRate)


        '        mySgnStampFee = ((mySgnSA / 1000) * mySgnStampRate)
        '        Me.txtStampDutyFee.Text = CStr(mySgnStampFee)
        '        mySgnAnnualPrem = ((mySgnSA / 1000) * mySgnPremRate) + (mySgnStampFee + mySgnPolicyFee)
        '        Me.txtAnnualPrem.Text = (mySgnAnnualPrem)

        '        If myStrModeOfPay = "Monthly" Or _
        '           myStrModeOfPay = "Quarterly" Or _
        '           myStrModeOfPay = "Half-Year" Then
        '            mySgnBasicPrem = (mySgnAnnualPrem * mySgnFreqRate) / 100
        '            'myBasicExtMed = (mySgnAnnualPrem * mySgnFreqRate) / 100
        '        Else
        '            mySgnBasicPrem = mySgnAnnualPrem
        '            'myBasicExtMed = myAnnExtMed
        '        End If
        '        premtotal = mySgnBasicPrem

        '        Call ExtraPremValue()

        '        Me.txtPremPayable.Text = (Me.txtExtrPremVal.Text + premtotal)

    End Sub

    Private Sub ExtraPremValue()

        '    Dim mySQL As String, myTable As String
        '    Dim myIntModeOfPay As Integer = 0
        '    Dim myIntTerm As Integer = 0
        '    Dim myStrModeOfPay As String
        '    Dim myStrAge As String
        '    Dim mySgnFreqRate As Single = 0
        '    Dim mystrPremRate As Single = 0
        '    Dim annExtPre As Single = 0
        '    Dim myAnnExtMed As Single = 0
        '    Dim myBasicExtMed As Single = 0
        '    Dim mySgnAnnualPrem As Single = 0
        '    Dim mySgnBasicPrem As Single = 0
        '    Dim mySgnSA As Single = 0 'Sum Assured
        '    mySgnSA = (Me.txtSumAssured.Text)

        '    myStrModeOfPay = Me.cbPaymentFreqency.SelectedItem.Value
        '    If myStrModeOfPay = "Single" Then 'Single
        '        myIntModeOfPay = 1
        '    ElseIf myStrModeOfPay = "Annually" Then 'Annual
        '        myIntModeOfPay = 1
        '    ElseIf myStrModeOfPay = "Half-Year" Then 'HalfYear
        '        myIntModeOfPay = 2
        '    ElseIf myStrModeOfPay = "Quarterly" Then 'Quarterly
        '        myIntModeOfPay = 4
        '    ElseIf myStrModeOfPay = "Monthly" Then 'Monthly
        '        myIntModeOfPay = 12
        '    End If
        '    If Me.cbStateHealth.SelectedItem.Value = "Sub-StardardLife" Then
        '        mystrPremRate = Me.txtExtrPremRate.Text
        '    Else
        '        mystrPremRate = "0"
        '    End If
        '    'If Me.cbStateHealth.SelectedItem.Value = "StardardLife" Then
        '    '    mystrPremRate = Me.txtExtrPremRate.Text
        '    '    mystrPremRate = "0"
        '    'Else
        '    '    mystrPremRate = "0"
        '    'End If
        '    Dim ConnX As OleDbConnection = New OleDbConnection(ConnString)
        '    Dim CmddX As OleDbCommand
        '    Dim objDR As OleDbDataReader
        '    Dim ansX As Integer = 0

        '    myTable = "LifBasicRate"
        '    mySQL = "SELECT * FROM " & myTable
        '    mySQL += " WHERE Code = '" & myStrModeOfPay & "'"

        '    ConnX.Open()
        '    CmddX = New OleDbCommand(mySQL, ConnX)
        '    CmddX.CommandType = CommandType.Text
        '    objDR = CmddX.ExecuteReader(CommandBehavior.CloseConnection)
        '    If (objDR.Read()) Then
        '        mySgnFreqRate = CType(objDR("BasicRate"), Single)
        '    Else
        '        mySgnFreqRate = 0
        '    End If

        '    objDR.Close()
        '    CmddX.Dispose()

        '    ConnX.Dispose()
        '    ConnX.Close()
        '    myAnnExtMed = (mySgnSA / 1000) * mystrPremRate

        '    If myStrModeOfPay = "Monthly" Or _
        '               myStrModeOfPay = "Quarterly" Or _
        '               myStrModeOfPay = "Half-Year" Then
        '        'mySgnBasicPrem = (myAnnExtMed * mySgnFreqRate) / 100
        '        myBasicExtMed = (myAnnExtMed * mySgnFreqRate) / 100
        '    Else
        '        'mySgnBasicPrem = mySgnAnnualPrem
        '        myBasicExtMed = myAnnExtMed
        '    End If
        '    Me.txtExtrPremVal.Text = (myBasicExtMed)
        '    'Me.txtPremPayable.Text += (myBasicExtMed)

    End Sub

End Class
