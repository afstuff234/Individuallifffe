<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_CLM_WAIVER.aspx.vb" Inherits="PRG_LI_CLM_WAIVER" enableEventValidation="false" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Waiver Processing</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
    <script src="../jquery.min.js" type="text/javascript"></script>

    <script src="../Script/jquery-1.11.0.js" type="text/javascript"></script>
    <script src="../jquery.simplemodal.js" type="text/javascript"></script>
    <script src="../Script/WaiverScript.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
// calling jquery functions once document is ready
    </script>
    <style type="text/css">
        #drpWaiverCodes
 {
 	display:none;
 }
 #lblWaiverCode
 {
 	display:none;
 }
 #lblWaiverEffDate
 {
 	display:none;
 }
 #txtWaiverEffectiveDate
 {
 	display:none;
 }
 
 #lblWaiverEffFormat
 {
 	display:none;
 }
    </style>

    </head>

<body onload="<%= FirstMsg %>">
    <form id="form1" runat="server">
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
                                    ID="cmdSearch1" runat="server" Text="Search" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" Width="150px" runat="server" 
                                    AppendDataBoundItems="True">
                                    <asp:ListItem>* Select Insured *</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=IL_CLAIM')">Go to Menu</a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data" OnClientClick="return ValidateOnClient()"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" runat="server" 
                                                text="Print"></asp:button>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
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
                    <td nowrap class="myheader">Waiver of Premium Processing</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new"">
                    <tr>
                        <td colspan="4">
                            <center>
                                <asp:Label ID="lblMsg" runat="server" Font-Size="13pt" ForeColor="#FF3300"></asp:Label></center>
                            <center>
                                <div id="ans" style="text-align: center; width: 500px; color: Red;">
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label2" runat="server" Text="Policy Number: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyNumber" runat="server" Width="221px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label3" runat="server" Text="Assured Code:"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtAssuredCode" runat="server"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="Label4" runat="server" Text="Assured Name:"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtAssuredName" runat="server" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label5" runat="server" Text="Policy Product Code: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyProCode" runat="server"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="Label6" runat="server" Text="Product Description: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtProdDesc" runat="server" Width="270px" style="height: 22px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label7" runat="server" Text="Policy Start Date: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyStartDate" runat="server"></asp:TextBox>
                            <asp:Label ID="Label13" runat="server" Text="dd/mm/yyyy"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="Label8" runat="server" Text="Policy End Date: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyEndDate" runat="server"></asp:TextBox>
                            <asp:Label ID="Label14" runat="server" Text="dd/mm/yyyy"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            &nbsp;</td>
                        <td>
                            <asp:CheckBox ID="chkConfirmWaiver" runat="server" Text="WAIVER?" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="lblWaiverCode" runat="server" Text="Waiver Cover: "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpWaiverCodes" runat="server" Height="25px" Width="307px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="lblWaiverEffDate" runat="server" Text="Waiver Effective Date: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtWaiverEffectiveDate" runat="server"></asp:TextBox>
                            <asp:Label ID="lblWaiverEffFormat" runat="server" Text="dd/mm/yyyy"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:HiddenField ID="txtPolyStatus" runat="server" />
                            <asp:HiddenField ID="HidWaiverDesc" runat="server" />
                            <asp:HiddenField ID="HidWaiverCode" runat="server" />
                        </td>
                        <td>
                            &nbsp;
                            <asp:HiddenField ID="HidAssuredName" runat="server" />
                            <asp:HiddenField ID="HidProdDesc" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="HidPolStartDate" runat="server" />
                            <asp:HiddenField ID="HidPolEndDate" runat="server" />
                            </td>
                        <td>
                            <asp:HiddenField ID="HidAssuredCode" runat="server" />
                            <asp:HiddenField ID="HidPolicyProCode" runat="server" />
                            </td>
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
                        <td colspan="4">                                                        
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
