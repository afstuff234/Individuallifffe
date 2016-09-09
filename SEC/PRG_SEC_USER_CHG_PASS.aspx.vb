Imports System.Data
Imports System.Data.OleDb
Partial Class SEC_PRG_SEC_USER_CHG_PASS
    Inherits System.Web.UI.Page
    Protected FirstMsg As String
    Protected PageLinks As String
    Protected STRPAGE_TITLE As String

    Protected blnStatus As Boolean
    Protected blnStatusX As Boolean

    Protected strP_ID As String
    Protected strP_TYPE As String
    Protected strP_DESC As String
    Protected strPOP_UP As String

    Protected myTType As String

    Dim strREC_ID As String
    Protected strOPT As String = "0"

    Protected strTableName As String
    Dim strTable As String
    Dim strSQL As String

    Dim strErrMsg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTableName = "SEC_USER_LIFE_DETAIL"

        STRPAGE_TITLE = "User Password Change - " & strP_DESC
        If Not (Page.IsPostBack) Then
            Call DoNew()
            txtPassExpDays.Text = 90
            Me.txtPassExpDate.Text = Format(DateAdd(DateInterval.Day, CInt(txtPassExpDays.Text), DateTime.Now), "dd/MM/yyyy")
        End If
    End Sub
    Protected Sub cmdSave_ASP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave_ASP.Click
        If txtPassword.Text = "" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblPassword.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            txtPassword.Focus()
            Exit Sub
        End If
        If txtConPassword.Text = "" Then
            Me.lblMessage.Text = "Missing/Invalid " & Me.lblConPassword.Text
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            txtConPassword.Focus()
            Exit Sub
        End If
        If txtPassword.Text <> txtConPassword.Text Then
            Me.lblMessage.Text = "Password does not match"
            FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            Exit Sub
        Else
            Dim myUserIDX As String = ""
            Try
                myUserIDX = CType(Session("MyUserIDX"), String)
            Catch ex As Exception
                myUserIDX = ""
            End Try

            Dim intC As Long = 0

            Dim mystrCONN As String = CType(Session("connstr"), String)
            Dim objOLEConn As New OleDbConnection()
            objOLEConn.ConnectionString = mystrCONN

            Try
                'open connection to database
                objOLEConn.Open()
            Catch ex As Exception
                Me.lblMessage.Text = "Unable to connect to database. Reason: " & ex.Message
                'FirstMsg = "Javascript:alert('" & Me.txtMsg.Text & "')"
                objOLEConn = Nothing
                Exit Sub
            End Try

            strTable = strTableName

            strREC_ID = Trim(myUserIDX)

            strSQL = ""
            strSQL = "SELECT TOP 1 * FROM " & strTable
            strSQL = strSQL & " WHERE SEC_USER_LOGIN = '" & RTrim(strREC_ID) & "'"

            Dim objDA As System.Data.OleDb.OleDbDataAdapter
            objDA = New System.Data.OleDb.OleDbDataAdapter(strSQL, objOLEConn)


            Dim m_cbCommandBuilder As System.Data.OleDb.OleDbCommandBuilder
            m_cbCommandBuilder = New System.Data.OleDb.OleDbCommandBuilder(objDA)

            Dim obj_DT As New System.Data.DataTable

            Try

                objDA.Fill(obj_DT)

                If obj_DT.Rows.Count > 0 Then
                    '   Creating a new record

                    'Dim drNewRow As System.Data.DataRow
                    'drNewRow = obj_DT.NewRow()
                    'drNewRow("SEC_USER_PASSWORD") = EncryptNew(LTrim(Me.txtPassword.Text))
                    'drNewRow("passwordexpirydays") = CInt(txtPassExpDays.Text)
                    'drNewRow("passwordexpirydate") = Convert.ToDateTime(DoConvertToDbDateFormat(txtPassExpDate.Text))

                    'obj_DT.Rows.Add(drNewRow)
                    'intC = objDA.Update(obj_DT)
                    'drNewRow = Nothing
                    'Me.lblMessage.Text = "New Record Saved to Database Successfully."

                    'Else
                    '   Update existing record
                    With obj_DT
                        .Rows(0)("SEC_USER_PASSWORD") = EncryptNew(LTrim(Me.txtPassword.Text))
                        .Rows(0)("passwordexpirydays") = CInt(txtPassExpDays.Text)
                        .Rows(0)("passwordexpirydate") = Convert.ToDateTime(DoConvertToDbDateFormat(txtPassExpDate.Text))
                    End With
                    intC = objDA.Update(obj_DT)

                    Me.lblMessage.Text = "Password changed successfully, please kindly re login."

                End If

            Catch ex As Exception
                Me.lblMessage.Text = ex.Message.ToString
                Exit Sub
            End Try

            m_cbCommandBuilder.Dispose()
            m_cbCommandBuilder = Nothing

            obj_DT.Dispose()
            obj_DT = Nothing

            objDA.Dispose()
            objDA = Nothing

            If objOLEConn.State = ConnectionState.Open Then
                objOLEConn.Close()
            End If
            objOLEConn = Nothing
            ' FirstMsg = "Javascript:alert('" & Me.lblMessage.Text & "')"
            MsgBox(Me.lblMessage.Text, 0, "Change Password")
            DoNew()
            Response.Redirect("~/LoginP.aspx")
        End If
    End Sub
    Private Sub DoNew()
        txtPassword.Text = ""
        txtConPassword.Text = ""
    End Sub

    Protected Sub LnkBut_LogOff_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkBut_LogOff.Click
        Session.RemoveAll()
        Session.Abandon()
        Response.Redirect("~/LoginP.aspx")
    End Sub
End Class
