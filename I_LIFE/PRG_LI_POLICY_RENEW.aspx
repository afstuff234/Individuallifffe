<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_POLICY_RENEW.aspx.vb"
    Inherits="I_LIFE_PRG_LI_POLICY_RENEW" %>

<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>
<%@ Register Src="../UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Renewal Scheme</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>

    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>

    <style type="text/css">
        .style1
        {
            height: 22px;
        }
        .style2
        {
            width: 263px;
        }
        .style3
        {
            width: 141px;
        }
    </style>
</head>
<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">
    <!-- start banner -->
    <div id="div_banner" align="center">
        <uc1:UC_BANT ID="UC_BANT1" runat="server" />
    </div>
    <!-- start header -->
    <div id="div_header" align="center">
        <table id="tbl_header" align="center">
            <tr>
                <td align="left" valign="top" class="myMenu_Title_02">
                    <table border="0" width="100%">
                        <tr>
                            <td align="left" colspan="2" valign="top">
                                <%=STRMENU_TITLE%>
                            </td>
                            <td align="right" colspan="2" valign="top">
                                <%--&nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;&nbsp;--%>
                                Find:&nbsp;<input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}" onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;&nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
                                &nbsp;&nbsp;<asp:DropDownList ID="cboSearch" runat="server" Height="26px" 
                                    Width="150px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td align="left" colspan="4" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" valign="top" class="style1">
                                &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go
                                    to Menu</a> &nbsp;&nbsp;<asp:Button ID="btnRenewPolicy" CssClass="cmd_butt" Text="RENEW POLICY" OnClientClick="return confirm('WARNING: Record will be permanently BE RENEWED! Are you sure you want to renew policy!');" 
                                        runat="server" Font-Bold="True" Width="164px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- start content -->
    <div id="div_content" align="center">
        <table class="tbl_cont" align="center">
            <tr>
                <td nowrap class="myheader">
                    Premium Information
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="left" valign="top" class="style3">
                                <asp:Label ID="lblFileNum" Enabled="true" Text="File No:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style2">
                                <asp:TextBox ID="txtFileNum" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="true" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtPolNum" Width="244px" runat="server"></asp:TextBox>
                                <asp:Button ID="btnGo" runat="server" Height="25px" Text="Go" Width="40px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblQuote_Num" Enabled="true" Text="Proposal No:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style2">
                                <asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblPlan_Num" Text="Plan/Cover Code:" runat="server" 
                                    Visible="False"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtPlan_Num" Visible="False" Enabled="false" MaxLength="10" Width="80"
                                    runat="server"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtCover_Num" Visible="False" Enabled="false" MaxLength="10"
                                    Width="80px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="lblPrem_Start_Date" Text="Start Date:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style2">
                                <asp:TextBox ID="txtPrem_Start_Date" MaxLength="10" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblPrem_Start_Date_Format" Visible="true" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:Label ID="lblPrem_End_Date" Enabled="true" Text="Expiry Date:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtPrem_End_Date" MaxLength="10" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblPrem_Start_Date_Format0" Visible="true" 
                                    Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style3">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" colspan="1" class="style2">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" colspan="1">
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style3">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" colspan="1" class="style2">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" colspan="1">
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
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
                    <table align="center" border="0" class="footer" style="background-color: Black;">
                        <tr>
                            <td colspan="4">
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
