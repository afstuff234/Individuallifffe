
Partial Class I_LIFE_PRG_LI_PROP_POLICY
    Inherits System.Web.UI.Page

    Protected PageLinks As String

    'Protected STRPAGE_TITLE As String
    Protected STRMENU_TITLE As String
    Protected BufferStr As String

    Protected STRUSER_ID As String
    Protected STRUSER_NAME As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        Dim mKey As String = ""
        Try
            mKey = Page.Request.QueryString("menu").ToString
            'mkey options = MKT UND CLM REIN CRCO ACC ADMIN
        Catch
            mKey = "IL_QUOTE"
            'mKey = ""
        End Try

        'Try
        '    STRUSER_ID = CType(Session("MyUserIDX"), String).ToString
        '    STRUSER_NAME = CType(Session("MyUserName"), String).ToString
        'Catch ex As Exception
        '    'UNLO=USER NOT LOG ON
        '    STRUSER_ID = "UNLO"
        '    STRUSER_NAME = "*** USER NOT LOGGED ON ***"
        '    'Response.Redirect("~/Default.aspx")
        'End Try

        'If Trim(STRUSER_ID) = "" Or Trim(STRUSER_ID) = "UNLO" Then
        '    'Response.Redirect("~/LoginP.aspx")
        '    'Response.Redirect("~/LoginP.aspx?Goto=" & Page.Request.Path)
        '    'Exit Sub
        'End If

        'Dim oAL As ArrayList = MOD_GEN.gnGET_USER_INFO("001", RTrim(STRUSER_ID))
        'If oAL.Item(0) = "TRUE" Then
        '    'Destroy i.e remove the array list object from memory
        '    Response.Write("<br/>Status: " & oAL.Item(0))
        '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
        '    Response.Write("<br/>Item 2 value: " & oAL.Item(2))
        '    Response.Write("<br/>Item 3 value: " & oAL.Item(3))
        '    oAL = Nothing
        '    'Response.Redirect("~/LoginP.aspx?Goto=" & Page.Request.Path)
        '    'Exit Sub
        'Else
        '    Response.Write("<br/>Status: " & oAL.Item(0))
        '    Response.Write("<br/>Item 1 value: " & oAL.Item(1))
        '    Response.Write("<br/>Item 2 value: " & oAL.Item(2))
        '    oAL = Nothing
        '    'Response.Redirect("~/LoginP.aspx?Goto=" & Page.Request.Path)
        '    'Exit Sub
        'End If

        'If Me.txtAction.Text = "Log_Out" Then
        '    Call DoProc_LogOut()
        '    Me.txtAction.Text = ""
        '    'Response.Redirect("LoginP.aspx")
        '    'Response.Redirect(Request.ApplicationPath & "~/Default.aspx")
        '    Response.Redirect(Request.ApplicationPath & "~/m_menu.aspx?menu=home")
        '    Exit Sub
        'End If

        STRMENU_TITLE = "+++ Indivisual Life Proposal Menu +++ "
        BufferStr = ""

        'Me.LNK_CODE.Enabled = True
        'Me.LNK_QUOTE.Enabled = True
        'Me.LNK_UND.Enabled = True
        'Me.LNK_ENDORSE.Enabled = True
        'Me.LNK_PROCESS.Enabled = True
        'Me.LNK_CLP.Enabled = True
        'Me.LNK_REINS.Enabled = True

        'Me.LNK_CODE.BackColor = Drawing.Color.White
        'Me.LNK_QUOTE.BackColor = Drawing.Color.White
        'Me.LNK_UND.BackColor = Drawing.Color.White
        'Me.LNK_ENDORSE.BackColor = Drawing.Color.White
        'Me.LNK_PROCESS.BackColor = Drawing.Color.White
        'Me.LNK_CLP.BackColor = Drawing.Color.White
        'Me.LNK_REINS.BackColor = Drawing.Color.White

        Call DoProc_Menu(mKey)

    End Sub

    Private Sub DoProc_Menu(ByVal pvMenu As String)
        Select Case pvMenu.ToUpper

            Case "IL_QUOTE"
                STRMENU_TITLE = "+++ Proposal Entry Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Proposal Entry", "New Proposal", "PRG_LI_INDV_POLY_PERSNAL.aspx?optid=NEW")
                AddMenuItem("", "Change Proposal", "PRG_LI_INDV_POLY_PERSNAL.aspx?optid=CHG")
                'AddMenuItem("", "Delete Proposal", "PRG_LI_INDV_POLY_PERSNAL.aspx?optid=DEL")
                AddMenuItem("", "Delete Proposal", "")
                'AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Convert Proposal to Policy", "PRG_LI_INDV_POLY_CONVERT.aspx")
                'AddMenuItem("", "", "") 'blank link
                'AddMenuItem("", "Change Policy - Endorsement", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")


            Case "IL_DOCUMENT"
                STRMENU_TITLE = "+++ Proposal Document Menu +++ "
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("Proposal Document", "Investment Product", "PD_IL001.aspx?opt=I001")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Protection", "PD_IL001.aspx?opt=I001")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Endowment", "PD_IL001.aspx?opt=I001")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Funeral", "PD_IL001.aspx?opt=I001")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "Annuity", "")
                AddMenuItem("", "", "") 'blank link
                AddMenuItem("", "UNDER_LINE", "") 'blank link
                AddMenuItem("", "Returns to Previous Page", "menu_il.aspx?menu=home")

        End Select

    End Sub

    Private Sub AddMenuItem(ByVal LeadItem As String, ByVal MenuItemText As String, ByVal LinkUrl As String)
        Dim myURL As String
        myURL = LinkUrl
        If Trim(myURL) = "" Then
            myURL = "'#'"
        Else
            myURL = "'" & myURL & "'"
        End If

        BufferStr += "<tr>"
        If LeadItem.Trim() = "" Then
            BufferStr += "<td nowrap align='left' valign='top'>&nbsp;&nbsp;</td>"
        Else
            BufferStr += "<td nowrap align='left' valign='top' class='a_sub_menu'>&nbsp;"
            BufferStr += "<img alt='Menu Image' src='../Images/ballred.gif' class='MY_IMG_LINK' />&nbsp;"
            BufferStr += LeadItem & "&nbsp;</td>"
        End If

        If MenuItemText.Trim = "" Then
            BufferStr += "<td nowrap align='left' valign='top'>&nbsp;</td>"
        ElseIf MenuItemText.Trim = "UNDER_LINE" Then
            BufferStr += "<td nowrap align='left' valign='top' class='td_under_line'>&nbsp;</td>"
        ElseIf MenuItemText.Trim = "Returns to Previous Page" Then
            BufferStr += "<td nowrap align='left' valign='top' class='td_return_menu'>"
            BufferStr += "<a href=" & myURL & " valign='top' class='a_sub_menu'>" & MenuItemText & "</a>"
            BufferStr += "</td>"
            'a_return_menu
        Else
            BufferStr += "<td nowrap align='left' valign='top' class='td_sub_menu2'>"
            BufferStr += "<img alt='Menu Image' src='../Images/ballredx.gif' class='MY_IMG_LINK' />&nbsp;"
            BufferStr += "<a href=" & myURL & " valign='top' class='a_sub_menu2'>" & MenuItemText & "</a>"
            BufferStr += "</td>"
            'td_sub_menu2
            'a_sub_menu2

            'td_under_line

        End If
        BufferStr += "</tr>"
    End Sub

    Protected Sub DoProc_LogOut()

        Dim strSess As String = ""
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


End Class
