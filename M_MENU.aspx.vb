
Partial Class M_MENU
    Inherits System.Web.UI.Page

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not (Page.IsPostBack) Then
            Me.LNK_HOME.Text = "Home"
            Me.LNK_IL.Text = "Individual Life"
            Me.LNK_GL.Text = "Group Life"
            Me.LNK_ANNUITY.Text = "Annuity"
            Me.LNK_ACC.Text = "Accounts"
            Me.LNK_ADMIN.Text = "Administration"

        End If

        If Me.txtAction.Text = "Log_Out" Then
            Call DoProc_LogOut()
            Me.txtAction.Text = ""
            Response.Redirect("LoginP.aspx")
            Exit Sub
        End If


        'Put user code to initialize the page here
        Dim mKey As String
        Try
            mKey = Page.Request.QueryString("menu").ToString
            'mkey options = MKT UND CLM REIN CRCO ACC ADMIN
        Catch
            mKey = "HOME"
        End Try

        STRMENU_TITLE = "+++ Home Page +++ "
        BufferStr = ""
        Call DoProc_Menu(mKey)

    End Sub

    Private Sub DoProc_Menu(ByVal pvMenu As String)
        Select Case pvMenu.ToUpper
            Case "HOME"
                STRMENU_TITLE = "+++ Home Page +++ "
                'AddMenuItem("", "Welcome to ABS Web Accounts Manager", "MainM.aspx?menu=HOME")
                BufferStr += "<tr>"
                BufferStr += "<td align='center' valign='top'>"
                BufferStr += "&nbsp;<img alt='Image' src='Images/Discussion.jpg' style='width: 850px; height: 500px' />&nbsp;"
                BufferStr += "</td>"
                BufferStr += "</tr>"
            Case "GEN"
                STRMENU_TITLE = "+++ Parameters Menu +++ "
                AddMenuItem("", "Company Data Setup", "javascript:jsDoPopNew_Full('General/GEN110.aspx?TTYPE=COY')")
                AddMenuItem("", "Server Parameters Setup", "")
                AddMenuItem("", "", "") 'blank link
            Case "SEC"
                STRMENU_TITLE = "+++ Security Menu +++ "
                AddMenuItem("", "Administrator Password Change", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Create New User", "")
                AddMenuItem("", "User Password Change", "")
                AddMenuItem("", "", "") 'blank link
        End Select


    End Sub

    Private Sub AddMenuItem(ByVal LeadItem As String, ByVal MenuItemText As String, ByVal LinkUrl As String)
        Dim myURL As String
        myURL = LinkUrl
        If Trim(myURL) = "" Then
            myURL = "'#'"
        End If

        BufferStr += "<tr>"
        If LeadItem.Trim() = "" Then
            BufferStr += "<td align='left' valign='top'>&nbsp;</td>"
        Else
            BufferStr += "<td align='left' valign='top'>&nbsp;"
            BufferStr += "" & LeadItem & "</td>"
        End If

        If MenuItemText.Trim = "" Then
            BufferStr += "<td align='left' valign='top'>&nbsp;</td>"
        Else
            BufferStr += "<td align='left' valign='top' style='height: 17px;'>&nbsp;"
            BufferStr += "<a href=" & myURL & " style='height: 17px; font-family: Trebuchet MS; font-size:large; font-weight:normal;' >" & MenuItemText & "</a>"
            BufferStr += "</td>"
        End If
        BufferStr += "</tr>"
    End Sub

    Protected Sub DoProc_LogOut()

        Dim strSess As String = "STFID"
        Dim intC As Integer = 0
        Dim intCX As Integer = 0
        Dim MyArray(15) As String

        intC = 0
        intCX = 0
        Try
            'Session("STFID") = RTrim(Me.txtNum.Text)
            'Session("STFID") = RTrim("")

            'Session.Keys
            'Session.Count
            'LOOP THROUGH THE SESSION OBJECT
            '*******************************

            'For intC = 0 To Session.Count - 1
            'Response.Write("<br />" & "Item " & intC & " - Key: " & Session.Keys(intC).ToString & " - Value: : " & Session.Item(intC).ToString)
            '
            'Next

            'SAMPLE SESSION DATA
            '*******************
            ''Item 0 - Key: ActiveSess - Value: : 1
            ''Item 1 - Key: StartdDate - Value: : 06/14/2013 7:01:55 PM
            ''Item 2 - Key: connstr - Value: : Provider=SQLOLEDB;Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 3 - Key: connstr_SQL - Value: : Data Source=ABS-PC;Initial Catalog=ABS;User ID=SA;Password=;
            ''Item 4 - Key: CL_COMP_NAME - Value: : CUSTODIAN AND ALLIED INSURANCE PLC
            ''Item(5 - Key) : MyUserIDX(-Value) : ADM()
            ''Item(6 - Key) : MyUserName(-Value) : System(Administrator)
            ''Item 7 - Key: MyUserLevelX - Value: : 0
            ''Item(8 - Key) : MyUserIDX_NIM(-Value) : ADM()
            ''Item(9 - Key) : MyUserName_NIM(-Value) : System(Administrator)
            ''Item 10 - Key: MyUserLevelX_NIM - Value: : 0
            ''Item 11 - Key: MyLogonTime - Value: : 06/14/2013 7:02:05 PM
            ''Item(12 - Key) : MyUserID(-Value) : ADM()


            'For Each strS As String In Session.Keys
            '    '    ' ...
            '    'Response.Write("<br /> Session ID: " & Session.SessionID & " - Key: " & strSess.ToString & " - Value: " & Session.Item(strSess).ToString)

            '    '    If UCase(strSess) = UCase("connstr") Or _
            '    '      UCase(strSess) = UCase("connstr_SQL") Or _
            '    '      UCase(strSess) = UCase("CL_COMP_NAME") Then
            '    '    Else
            '    '        'Session.Remove(strSess)
            '    '    End If
            '    strSess = strS
            '    Response.Write("<br />" & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
            'Next

            For intCX = 0 To Session.Count - 1

                strSess = Session.Keys(intCX).ToString

                If UCase(strSess) = UCase("connstr") Or _
                  UCase(strSess) = UCase("connstr_SQL") Or _
                  UCase(strSess) = UCase("CL_COMP_NAME") Or _
                  UCase(strSess) = UCase("ActiveSess") Or _
                  UCase(strSess) = UCase("StartdDate") Then
                Else
                    intC = intC + 1
                    MyArray(intC) = strSess
                    'Response.Write("<br />" & "Item " & intC & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)

                End If

            Next

            'Response.Write("<br />" & "Item ubound(): " & UBound(MyArray).ToString)
            'Response.Write("<br />" & "Item Length: " & MyArray.Length)

            For intCX = 1 To intC

                strSess = MyArray(intCX).ToString

                Response.Write("<br />" & "Removing session Item " & intCX & " - Key: " & strSess.ToString & " - Value: : " & Session.Item(strSess).ToString)
                Session.Remove(strSess.ToString)
                'Session.Contents.Remove(strSess)

            Next

            'Session.Clear()

        Catch ex As Exception
            Response.Write("<br /> Error has Occured at key: " & strSess.ToString & " Reason: " & ex.Message.ToString)
            'Exit Try
        End Try


    End Sub


    Private Sub LNK_HOME_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNK_HOME.Click
        Response.Redirect("M_MENU.aspx?menu=HOME")
    End Sub

    Private Sub LNK_IL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNK_IL.Click
        Response.Redirect("~/I_LIFE/MENU_IL.aspx?menu=HOME")
    End Sub

    Private Sub LNK_GL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNK_GL.Click
        Response.Redirect("~/G_LIFE/MENU_GL.aspx?menu=HOME")
    End Sub

    Private Sub LNK_ANNUITY_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNK_ANNUITY.Click
        Response.Redirect("~/Annuity/MENU_AN.aspx?menu=HOME")
    End Sub

    Private Sub LNK_ACC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNK_ACC.Click
        Response.Redirect("~/ACCT/MENU_ACC.aspx?menu=home")
    End Sub

    Private Sub LNK_ADMIN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNK_ADMIN.Click
        Response.Redirect("~/SEC/MGL_SEC.aspx?menu=home")

    End Sub

End Class
