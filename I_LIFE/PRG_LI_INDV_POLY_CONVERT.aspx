<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_POLY_CONVERT.aspx.vb" Inherits="I_LIFE_PRG_LI_INDV_POLY_CONVERT" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Convert Proposal to Policy</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js"></script>
</head>

<body onload="<%= FirstMsg %>">
    <form id="Form1" runat="server">
    
    <!-- start banner -->
    <div id="div_banner" align="center">    
        <uc1:UC_BANT ID="UC_BANT1" runat="server" />    
    </div>

    <div id="div_content" align="center">
        <table id="tbl_content" align="center">
        <tr>
            <td align="center" valign="top" class="td_menu_new">
	            <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
	                <tr>
                        <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" runat="server"></asp:DropDownList>
                        </td>	                
	                </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%></td>
                    </tr>
                    
                    <tr>
                        <td align="left" colspan="2" valign="top">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                        &nbsp;
                                    </td>
                                    <td align="right" colspan="1" valign="top">&nbsp;
                                        <a href="PRG_ANNUITY_PROP_POLICY.aspx?menu=AN_QUOTE" class="a_sub_menu">Return to Menu</a>&nbsp;                            
                                    </td>                           
                                    
                                    <td align="right" colspan="1" valign="top" style="display:none;">    
                                        &nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox></td>
                                </tr>
                            </table>                            
                        </td>                           
                    </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top">&nbsp;
                            <asp:Label ID="lblMsg" Text="Status..." ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblPro_Pol_Num" Text="Proposal Number:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtPro_Pol_Num" Width="250px" runat="server"></asp:TextBox>
                            &nbsp;<asp:Button ID="cmdFileNum" Enabled="true" Font-Bold="true" Text="Get Record" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblFileNum" Text="File Number:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;
                            <asp:TextBox ID="txtFileNum" Enabled="true" Font-Bold="true" ForeColor="Red" Width="250px" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title">Policy Information</td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblPol_Num" Text="Policy Number:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtPol_Num" Enabled="false" Font-Bold="true" Font-Size="Large" ForeColor="Red" Width="350px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblAssuredName" Text="Assured Name:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtAssured_Name" Enabled="False" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblProduct_Num" Text="Product:" runat="server"></asp:Label>
                        </td>    
                        <td align="left" colspan="1">&nbsp;
                            <asp:TextBox ID="txtProduct_Num" Enabled="false" Width="80px" runat="server"></asp:TextBox>&nbsp;
                            <asp:TextBox ID="txtProduct_Name" Enabled="false" Font-Bold="true" Width="260px" runat="server"></asp:TextBox>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td align="left" colspan="2" valign="top" class="myMenu_Title">Receipt Information</td>
                    </tr>

                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblTrans_Date" Text="Receipt Date:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtTrans_Date" MaxLength="10" Width="200px" runat="server"></asp:TextBox>
                            &nbsp;<asp:Label ID="lblTrans_Date_Format" Font-Bold="true" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblTrans_Num" Text="Reference No:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtTrans_Num" MaxLength="15" Width="200px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblTrans_Amt" Text="Transaction Amount:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="1">&nbsp;                     
                            <asp:TextBox ID="txtTrans_Amt" MaxLength="15" Width="200px" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2"><hr /></td>
                    </tr>

                    <tr>
                        <td align="left" colspan="2" class="td_remarks">
                            <span>
                                Please note that after the proposal data conversion, you will not be allowed to modify the data again.<br />
                                Changes or alteration to policy information will only be done through ENDORSEMENT module.
                            </span>
                            <br />
                            <asp:CheckBox ID="chkAccept" AutoPostBack="true" Enabled="false" Checked="false" Font-Bold="true" Font-Size="Large" Text="Accept to continue with proposal data conversion..." runat="server" />
                            <br />
                            &nbsp;<asp:Label ID="lblPWD" Enabled="false" Text="Password/Access Code:" runat="server"></asp:Label>&nbsp;<asp:TextBox ID="txtPWD" Enabled="false" TextMode="Password" runat="server"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td align="center" colspan="2">&nbsp;
                            <asp:Button ID="BUT_OK" Enabled="false" Font-Bold="true" Font-Size="Large" Text="Convert to Policy" runat="server" />
                        </td>
                    </tr>
                    

                    <tr style="display: none;">
                        <td align="left" colspan="2" valign="top">&nbsp;
                            U/W Year:&nbsp;<asp:TextBox ID="txtYear" Enabled="false" Width="80px" runat="server"></asp:TextBox>&nbsp;
                            Branch:&nbsp;<asp:TextBox ID="txtBraNum" Enabled="false" Width="80px" runat="server"></asp:TextBox>&nbsp;
                            Product Class:&nbsp;<asp:TextBox ID="txtProductClass" Enabled="false" Width="80px" runat="server"></asp:TextBox>&nbsp;
                            Effective Date&nbsp;<asp:TextBox ID="txtPol_Eff_Date" Enabled="false" runat="server"></asp:TextBox>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td align="right" colspan="2" valign="top">&nbsp;
                            <a href="PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE" class="a_sub_menu">Return to Menu</a>&nbsp;
                        </td>                           
                    </tr>
                    
                    <tr>
                        <td colspan="2">&nbsp;</td>
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
