
Partial Class Calendar1
    Inherits System.Web.UI.Page

    Dim strFRM_NAME As String
    Dim strCTR_VAL As String
    Dim strCTR_TXT As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '                    OnSelectionChanged="OnSelectionChanged" >


        Try
            strFRM_NAME = CType(Request.QueryString("FRM_NAME"), String)
        Catch ex As Exception
            strFRM_NAME = "Form1"
        End Try
        Try
            strCTR_VAL = CType(Request.QueryString("CTR_VAL"), String)
        Catch ex As Exception
            strCTR_VAL = "Field_Value"
        End Try
        Try
            strCTR_TXT = CType(Request.QueryString("CTR_TXT"), String)
        Catch ex As Exception
            strCTR_TXT = "Field_Text"
        End Try

        'Response.Write("<br/>Form Name: " & strFRM_NAME)
        'Response.Write("<br/>Field Value Name: " & strCTR_VAL)
        'Response.Write("<br/>Field Text Name: " & strCTR_TXT)

        Me.hidFRM_NAME.Value = strFRM_NAME
        Me.hidCTR_VAL.Value = strCTR_VAL
        Me.hidCTR_TXT.Value = strCTR_TXT

        If Not (Page.IsPostBack) Then
            Cal1.SelectedDate = DateTime.Today
            Cal1.VisibleDate = DateTime.Today
            Me.lblDate.Text = Cal1.SelectedDate
            Me.hidDate.Value = ""
            Me.hidDate.Value = Format(Now, "dd/MM/yyyy")
        Else
            Me.lblDate.Text = Cal1.SelectedDate
            Me.hidDate.Value = Format(Val(Cal1.SelectedDate.Day.ToString()), "00") & "/" & _
                      Format(Val(Cal1.SelectedDate.Month.ToString()), "00") & "/" & _
                      Format(Val(Cal1.SelectedDate.Year.ToString()), "0000")
        End If

        'Me.lblDate.Text = Cal1.SelectedDate
        'Me.hidDate.Value = Format(Val(Cal1.SelectedDate.Day.ToString()), "00") & "/" & _
        '          Format(Val(Cal1.SelectedDate.Month.ToString()), "00") & "/" & _
        '          Format(Val(Cal1.SelectedDate.Year.ToString()), "00")

    End Sub

    'Private Sub Cal1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cal1.SelectionChanged
    '    Me.lblDate.Text = Cal1.SelectedDate

    '    Me.hidDate.Value = Format(Val(Cal1.SelectedDate.Day.ToString()), "00") & "/" & _
    '              Format(Val(Cal1.SelectedDate.Month.ToString()), "00") & "/" & _
    '              Format(Val(Cal1.SelectedDate.Year.ToString()), "00")
    'End Sub

    Sub MySel_Changed()

        'Me.lblDate.Text = "13/09/1969"
        Me.lblDate.Text = Cal1.SelectedDate

        Me.hidDate.Value = Format(Val(Cal1.SelectedDate.Day.ToString()), "00") & "/" & _
                  Format(Val(Cal1.SelectedDate.Month.ToString()), "00") & "/" & _
                  Format(Val(Cal1.SelectedDate.Year.ToString()), "0000")

        'Me.lblDate.Text = "13/09/1969"
    End Sub


End Class
