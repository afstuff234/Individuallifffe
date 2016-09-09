<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_MEDICAL_EXAM_LIST.aspx.vb"
    Inherits="I_LIFE_PRG_LI_INDV_MEDICAL_EXAM_LIST" %>

<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>
<%@ Register Src="../UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Medical Underwriting Req.</title>
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
<<<<<<< HEAD
        .style2
        {            width: 273px;
        }
=======
>>>>>>> origin/master
        .style3
        {
        }
        .style4
        {
            width: 212px;
        }
        .style5
        {
            width: 262px;
        }
        .style6
        {
            width: 152px;
        }
        .style7
        {
            width: 126px;
        }
        .style8
        {
            height: 24px;
        }
        .style9
        {
            width: 148px;
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
                        <tr style="display: none;">
                            <td align="left" colspan="2" valign="top">
                                <%=STRMENU_TITLE%>
                            </td>
                            <td align="right" colspan="2" valign="top">
                                <%--&nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;&nbsp;--%>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td align="left" colspan="4" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="style1">
                                &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go
                                    to Menu</a>
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
                    <span id="Label2">Medical Examination List</span></td>
            </tr>
            <tr>
                <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="">
                            <td nowrap align="left" valign="top" class="style6">
                                                <asp:Label ID="Label4" Text="Start Date:" runat="server"></asp:Label>
                            </td>
                            <td nowrap align="left" valign="top" class="style7">
                                                <asp:TextBox ID="txtPrem_Start_Date" MaxLength="10" runat="server" 
                                                    Enabled="True"></asp:TextBox>
                                                <asp:Label ID="Label5" Visible="true" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                            <td nowrap align="right" valign="top" class="style3">
                                                <asp:Label ID="Label6" Enabled="true" Text="Expiry Date:" runat="server"></asp:Label>
                            </td>
                            <td nowrap align="left" valign="top" class="style3">
                                                <asp:TextBox ID="txtPrem_End_Date" MaxLength="10" runat="server"></asp:TextBox>
                                                <asp:Label ID="Label7" Visible="true" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="">
                            <td nowrap align="left" valign="top" class="style6">
                                                <asp:Label ID="Label9" runat="server" Text="Doc. Supervisor:"></asp:Label>
                            </td>
                            <td nowrap align="left" valign="top" class="style7">
                                                <asp:DropDownList ID="cboSupervisor" runat="server" 
                                    Height="26px" Width="250px">
                                                </asp:DropDownList>
                            </td>
                            <td nowrap align="right" valign="top" class="style3">
                                                <asp:Label ID="Label8" runat="server" Text="Associate Company:"></asp:Label>
                            </td>
                            <td nowrap align="left" valign="top" class="style3">
                                                <asp:DropDownList ID="cboAssCompany" runat="server" 
                                    Height="26px" Width="250px">
                                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="center" valign="top" class="style8" colspan="4">
                                <asp:Button ID="btnGo" runat="server" Height="25px" Text="View/Print Report" Width="165px"
                                    Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style4" colspan="2">
                                &nbsp;
                                &nbsp;
                                &nbsp;
                            </td>
                            <td align="left" valign="top" class="style3" colspan="2">
                                &nbsp;</td>
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
