<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RPT_GRP_QuotationSlip.aspx.vb" Inherits="I_LIFE_RPT_GRP_QuotationSlip" %>
<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quotation Slip</title>
     <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
    <script src="../jquery.min.js" type="text/javascript"></script>

    <script src="../Script/jquery-1.11.0.js" type="text/javascript"></script>
    <script src="../jquery.simplemodal.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript"> </script>
    
    <style type="text/css">
        .style1
        {
            width: 1181px;
        }
        .style2
        {
            width: 119px;
        }
    </style>
</head>
<body onload="<%= FirstMsg %>">
    <form id="PRG_LI_REVIVE_POLICY" runat="server">
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
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;"><%=STRMENU_TITLE%></td>
                            <td align="left" colspan="1" valign="top" style="display:none;">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>
                            </td>
                            <td align="right" colspan="1" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<%--<asp:Button ID="cmdSearch" Text="Search" runat="server" />--%><asp:Button
                                    ID="cmdSearch" runat="server" Text="Search" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" Width="150px" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True">
                                    <asp:ListItem>* Select Insured *</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="left" colspan="4" valign="top">
                                            <asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=IL_CLAIM')">Go to Menu</a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                        
                    </table>                    
                </td>
            </tr>
        </table>
    </div>
    <div id="div_content" align="center">
     <table class="tbl_cont">
                <tr>
                    <td nowrap class="myheader"><b style="mso-bidi-font-weight:normal">
                        <span style="font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-ansi-language:EN-US;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">Print Quotation Slip</span></b>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                    <tr>
                        <td colspan="4">
                            <center>
                                <asp:Label ID="lblMsg" runat="server" Font-Size="13pt" ForeColor="#FF3300"></asp:Label></center>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="style2">
                            &nbsp;</td>
                        <td align="left" valign="top">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="style2">
                            <asp:Label ID="Label2" runat="server" Text="Proposal Number: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtProposalNo" runat="server" Width="269px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="style2">
                            <asp:Label ID="Label3" runat="server" Text="File Number:"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtFileNo" runat="server" Width="270px"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            &nbsp;</td>
                        <td align="left" valign="top">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="style2">
                            <asp:Label ID="Label5" runat="server" Text="Batch Number: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtBatchNo" runat="server"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            &nbsp;</td>
                        <td align="left" valign="top">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" class="style2">
                            &nbsp;</td>
                        <td align="right" valign="top">
                            <asp:button id="cmdPrint_ASP" CssClass="cmd_butt" runat="server" 
                                                text="Print"></asp:button>
                        </td>
                        <td align="left" valign="top">
                            &nbsp;</td>
                        <td align="left" valign="top">
                            &nbsp;</td>
                    </tr>
                    </table>
                    </td>                                                                                    
                </tr>
        </table>
    </div>
    
 <div id='confirm'>
        <div class='header'><span>Confirm</span></div>
        <div class='message'></div>
        <div class='buttons'>
            <div class='no simplemodal-close'>No</div><div class='yes'>Yes</div>
        </div>
    </div>
<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td colspan="4" class="style1">                                                        
                            <uc2:UC_FOOT ID="UC_FOOT1" runat="server" />                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>    

    </form></body>
</html>
