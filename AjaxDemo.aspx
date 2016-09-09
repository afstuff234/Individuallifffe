<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AjaxDemo.aspx.vb" Inherits="AjaxDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server"  GridLines="None" CellPadding="2" 
                    DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True"
                    ForeColor="#333333" AutoGenerateColumns="False"  PageSize="5"
                    DataKeyNames="EmployeeID" Width="800px">
                    <Columns>
                        <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" 
                            InsertVisible="False" ReadOnly="True" SortExpression="EmployeeID" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                            SortExpression="LastName" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                            SortExpression="FirstName" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="BirthDate" DataFormatString="{0:dd MMM yyyy}" 
                            HeaderText="BirthDate" SortExpression="Birth of Date" />
                        <asp:BoundField DataField="Address" HeaderText="Address Line" 
                            SortExpression="Address" />
                        <asp:BoundField DataField="HomePhone" HeaderText="Home Phone" 
                            SortExpression="HomePhone" />
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"                    
                    SelectCommand="SELECT [EmployeeID], [LastName], [FirstName], [Title], [BirthDate], [Address], [HomePhone] FROM [Employees] ORDER BY [EmployeeID]" 
                    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
                </asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                Getting employee data. Please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>

