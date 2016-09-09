<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CRViewer.aspx.vb" Inherits="I_LIFE_CRViewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function cmdCloseX_onclick() {

        }

// ]]>
    </script>
</head>

<body onload="<%= FirstMsg %>" style="background-color: White;">
    <form id="form1" runat="server">

    <div>
        <table align="center" border="0" width="90%" style="background-color: #DBEAF5;">
            <tr>
                <td align="left" colspan="2" valign="top">
                    <table align="center" border="0" width="100%">
                        <tr>
                            <td align="left" valign="top" style="height: 20px;">
                                <span style="font-size:large; text-transform: uppercase;"><%=STRCOMP_NAME%>&nbsp;</span>
                            </td>
                            <td align="right" valign="top">
                                <span style="font-size:small; text-transform:capitalize;">&nbsp;<%=STRUSER_LOGIN_NAME%>&nbsp;</span>
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>           
    </div>

    <div>
        <table align="center" border="0" width="90%" style="background-color: #DBEAF5;">
            <tr>
                <td align="left"><asp:Label ID="lblMessage" Text="Status..." runat="server" ForeColor="#FF8040"></asp:Label>
                </td>
                <td align="right"colspan="1">&nbsp;<input id="cmdCloseX" type="button" style="font-weight:bold; font-size:medium;" value="Close Page ..." runat="server" onclick="return cmdCloseX_onclick()" />&nbsp;
                </td>
            </tr>

            <tr>
                <td align="left" colspan="2" style="width: 100%;">
                    Page:&nbsp;&nbsp;<asp:TextBox ID="txtPageNumber" Text="1" runat="server" Width="38px"></asp:TextBox>
                    &nbsp; of &nbsp;&nbsp;<asp:TextBox ID="txtTotalPageNumber" Text="" Enabled="false" runat="server" Width="38px"></asp:TextBox>&nbsp;&nbsp;
                    | &nbsp; First Page &nbsp;<asp:ImageButton id="btnFirstPage" ImageAlign="AbsBottom" runat="server" imageUrl="../images/first_page.gif" ToolTip="First Page"/>&nbsp;
                    | &nbsp; Next &nbsp;<asp:ImageButton id="btnNextPage" ImageAlign="AbsBottom" runat="server" imageUrl="../images/next_page.gif" ToolTip="Next Page"/>&nbsp;
                    | &nbsp; Previous &nbsp;<asp:ImageButton id="btnPrevPage" ImageAlign="AbsBottom" runat="server" imageUrl="../images/prev_page.gif" ToolTip="Previous Page"/>&nbsp;
                    | &nbsp; Last Page &nbsp;<asp:ImageButton id="btnLastPage" ImageAlign="AbsBottom" runat="server" imageUrl="../images/last_page.gif" ToolTip="Last Page"/>&nbsp;&nbsp;
                </td>
            </tr>

        </table>    
        
        <table align="center" border="0" width="90%" style="background-color:White;">
            <tr>
                <td align="left" colspan="2" valign="top" style=" border: 1px solid #c0c0c0; height: 600px;">
                    <table border="0" width="100%">
                        <tr>
                            <td align="left">&nbsp; &nbsp;
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" Height="100%" Width="100%"                       
                                    AutoDataBind="true" />&nbsp; &nbsp;
                            </td>
                        </tr>                    
                    </table>                
                </td>
            </tr>
        </table>

        <table align="center" width="90%" style="background-color: #DBEAF5;">
            <tr>
                <td align="left" colspan="2"><asp:Label ID="lblReportPath" Text="Report Path..." Visible="false" runat="server"></asp:Label>
                </td>
            </tr>    
            <tr>
                <td align="left" colspan="2">Copyright &copy; &nbsp; <%=STRCOMP_NAME%>
                </td>
            </tr>

        </table>    

    
    </div>
    </form>
</body>
</html>
