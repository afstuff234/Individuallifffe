<%@ Page Language="VB" AutoEventWireup="false" CodeFile="M_MENU.aspx.vb" Inherits="M_MENU" %>

<%@ Register src="UC_BANM.ascx" tagname="UC_BANM" tagprefix="uc1" %>

<%@ Register src="UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Life Application Module</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>

</head>
<body>
    <form id="Form1" runat="server">
    <!-- start banner -->
    <div id="div_banner" align="center">
                
        <uc1:UC_BANM ID="UC_BANM1" runat="server" />
                
    </div>
    
    <!-- start header -->
    <div id="div_header" align="center">
        <asp:Panel ID="menuPanel_main" CssClass="menuPanel_main" runat="server">&nbsp;&nbsp;
            <asp:LinkButton ID="LNK_HOME" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_IL" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_GL" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_ANNUITY" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_ACC" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LNK_ADMIN" Enabled="true" runat="server"></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="LnkBut_LogOff" runat="server" Text="LOG OFF" OnClientClick="javascript:JSDO_LOG_OUT();"></asp:LinkButton>

            <div style="display: none;">
                <asp:Label ID="lblAction" ForeColor="LightGray" Text="Status:" runat="server"></asp:Label>
                &nbsp;<asp:textbox id="txtAction" ForeColor="LightGray" Text="" Visible="true" runat="server" EnableViewState="False" Width="30px"></asp:textbox>
            </div>
        </asp:Panel>
    </div>
    
    <div =id="div_content" align="center">
        <table id="tbl_content" align="center">
        <tr>
            <td align="left" valign="top" class="td_menu_new">
	            <table id="tbl_menu_new" align="center" border="0" cellspacing="0"
                    style="background-color: White; border: 1px solid #c0c0c0; width: 75%;">
                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%></td>
                    </tr>
                    <%=BufferStr%>
                    <tr>
                        <td align="left" colspan="2" valign="top">&nbsp;</td>
                    </tr>
				</table>
			</td>
        </tr>
        </table>
    </div>

<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td>
                                
                            <uc2:UC_FOOT ID="UC_FOOT1" runat="server" />
                                
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>    

    </form>
</body>
</html>
