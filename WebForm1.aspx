<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WebForm1.aspx.vb" Inherits="WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/SJS_02.js">
    </script>

    <style type="text/css">
        .selScheme_Type
        {
            width: 392px;
        }
        .cmdOK, .cmdCancel
        {
            width: 75px;
        }
    </style>

</head>
<body>
    <form id="Form1" name="Form1" action="#" runat="server" onsubmit="return true">
    
    <div align="center" style="background-color: White; margin: 0px auto 0 auto; padding: 0px; width: 1024px;">
        <table align="center" border="0" width="1024px">            
                        <tr>
                            <td colspan="2" valign="top"><hr /></td>
                        </tr>
            <tr>
                <td align="left" valign="top" style=" height:600px; width: 1024px;">
                    <table border="0" align="center">
                        <tr>
                            <td colspan="2" valign="top" style="color: White;" class="HREF_MENU3">
                                <a id="A1" href="#" runat="server">Home</a>&nbsp;
                                <a id="A2" href="#" runat="server">About us</a>&nbsp;
                                <a id="A3" href="#" runat="server">Yahoo</a>&nbsp;
                                <a id="A4" href="#" runat="server">Facebook</a>&nbsp;
                                <a id="A5" href="#" runat="server">Twitter</a>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top"><hr /></td>
                        </tr>

                        <tr>
                            <td valign="top">Account No:</td>
                            <td valign="top">Account Name:</td>
                        </tr>
                        <tr>
                            <td valign="top"><asp:TextBox ID="txtCustID" runat="server" Width="180px"></asp:TextBox></td>
                            <td valign="top"><asp:TextBox ID="txtCustName" runat="server" Width="430px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">Select Account:</td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">
                                <select id="selScheme_Type" name="selScheme_Type" class="selScheme_Type" runat="server" onchange="myFunc_GetFormData('SEL')">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="justify" colspan="2" valign="top">
                                <input type="button" id="cmdOK" name="cmdOK" value="OK..." onclick="myFunc_OK('OK')" class="cmdOK" />
                                &nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="cmdCancel" name="cmdCancel" value="Cancel..." onclick="myFunc_Cancel()" class="cmdCancel" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2" valign="top"><hr /></td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">
                            
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CustomerID" 
                                    DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" 
                                    GridLines="None" Width="700px" ShowFooter="True" OnPageIndexChanged="Proc_SelectedChange">

                                    <RowStyle BackColor="#E3EAEB" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />

                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />

                                        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" HeaderStyle-HorizontalAlign="Left" 
                                            ReadOnly="True" SortExpression="CustomerID" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="CompanyName" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ContactName" HeaderText="ContactName" HeaderStyle-HorizontalAlign="Left" 
                                            SortExpression="ContactName" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone"  
                                            HeaderStyle-HorizontalAlign="Left" >
                                        </asp:BoundField>
                                    </Columns>

                                </asp:GridView>
                            
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </div>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
                SelectCommand="SELECT [CustomerID], [CompanyName], [ContactName], [Phone] FROM [Customers]">
            </asp:SqlDataSource>
    

    </form>
</body>
</html>
