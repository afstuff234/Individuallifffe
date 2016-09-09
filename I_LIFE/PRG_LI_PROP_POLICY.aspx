<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_PROP_POLICY.aspx.vb" Inherits="I_LIFE_PRG_LI_PROP_POLICY" %>

<%@ Register src="../UC_BAN.ascx" tagname="UC_BAN" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Individual Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
</head>
<body style="background-color: White;">
    <form id="Form1" runat="server">
    <!-- start banner -->
    <div id="div_banner" align="center">
        
        <uc1:UC_BAN ID="UC_BAN1" runat="server" />
        
    </div>
    
    <!-- start header -->
    <div id="div_header" align="center">
            <asp:Panel ID="menuPanel" CssClass="menuPanel" runat="server">&nbsp;&nbsp;
                &nbsp;<asp:LinkButton ID="LNK_QUOTE" Enabled="true" Font-Bold="true" runat="server" Text="Proposal Entry" PostBackUrl="PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE"></asp:LinkButton>&nbsp;
                &nbsp;<asp:LinkButton ID="LNK_DOC" Visible="false" Enabled="true" Font-Bold="true" runat="server" Text="Policy Document" PostBackUrl="PRG_LI_PROP_POLICY.aspx?menu=IL_DOCUMENT"></asp:LinkButton>&nbsp;
                &nbsp;<asp:LinkButton ID="LNK_DOCX" Enabled="true" Font-Bold="true" runat="server" Text="Policy Document" PostBackUrl="PD_IL001.aspx?opt=I001"></asp:LinkButton>&nbsp;
                &nbsp;<a class="HREF_MENU2" href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=HOME')">Return to Main Menu</a>&nbsp;
        </asp:Panel>
    </div>
    
    <div id="div_content" align="center">
        <table border="1" id="tbl_content" align="center">
        <tr>
            <td align="center" valign="top" class="td_menu">
	            <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
                    <tr style="display: none;">
                        <td align="right" valign="top">
                            &nbsp;<asp:Label ID="lblAction" ForeColor="LightGray" Text="Status:" runat="server"></asp:Label>
                            &nbsp;<asp:textbox id="txtAction" ForeColor="LightGray" Text="" Visible="true" runat="server" EnableViewState="False" Width="30px"></asp:textbox>
                        </td>
                    </tr>

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
            <td align="center" valign="top">
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
