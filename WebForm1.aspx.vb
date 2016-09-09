
Partial Class WebForm1
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            Dim mylst_item As ListItem

            Me.selScheme_Type.Items.Clear()
            Me.selScheme_Type.Multiple = True

            mylst_item = New ListItem
            mylst_item.Value = "*"
            mylst_item.Text = "Select Product"
            Me.selScheme_Type.Items.Add(mylst_item)

            mylst_item = New ListItem
            mylst_item.Value = "A"
            mylst_item.Text = "Scheme A"
            Me.selScheme_Type.Items.Add(mylst_item)

            mylst_item = New ListItem
            mylst_item.Value = "B"
            mylst_item.Text = "Scheme B"
            Me.selScheme_Type.Items.Add(mylst_item)

            mylst_item = New ListItem
            mylst_item.Value = "C"
            mylst_item.Text = "Scheme C"
            Me.selScheme_Type.Items.Add(mylst_item)

            mylst_item = New ListItem
            mylst_item.Value = "D"
            mylst_item.Text = "Scheme D"
            Me.selScheme_Type.Items.Add(mylst_item)

            mylst_item = New ListItem
            mylst_item.Value = "E"
            mylst_item.Text = "Scheme E"
            Me.selScheme_Type.Items.Add(mylst_item)

            mylst_item = New ListItem
            mylst_item.Value = "F"
            mylst_item.Text = "Scheme F"
            Me.selScheme_Type.Items.Add(mylst_item)

            mylst_item = New ListItem
            mylst_item.Value = "G"
            mylst_item.Text = "Scheme G"
            Me.selScheme_Type.Items.Add(mylst_item)
        End If

    End Sub

    Protected Sub Proc_SelectedChange()

        Me.txtCustID.Text = "AC-001"
        Me.txtCustName.Text = "New Account"
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        'Dim row As GridViewRow = GridView1.Rows(e.NewSelectedIndex)

        GridView1.PageIndex = e.NewPageIndex

    End Sub


    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Me.txtCustID.Text = Now.ToString
        ' Get the currently selected row using the SelectedRow property.
        Dim row As GridViewRow = GridView1.SelectedRow

        ' Display the required value from the selected row.
        Me.txtCustID.Text = row.Cells(1).Text

        Me.txtCustName.Text = row.Cells(2).Text
        'Call Proc_DDL_Get(Me.ddlGroup, RTrim(Me.txtGroupNum.Text))


    End Sub
End Class