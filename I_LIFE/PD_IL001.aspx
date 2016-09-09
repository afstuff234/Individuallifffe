<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PD_IL001.aspx.vb" Inherits="I_LIFE_PD_IL001" %>

<%@ Register src="../UC_BAN.ascx" tagname="UC_BAN" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/JS_DOC.js"></script>
    
</head>

<body onload="<%= FirstMsg %>">
    <form id="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">        
        <uc1:UC_BAN ID="UC_BAN1" Visible="true" runat="server" />        
    </div>

    <div id="div_content" align="center">
        <table id="tbl_content" align="center">
        <tr>
            <td align="center" valign="top" colspan="2" class="td_menu">
	            <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
                                            <tr style="display: none;">
                                                <td align="right" colspan="4" valign="top"><asp:Label ID="lblProductClass" Text="Filter by Product Category:" runat="server"></asp:Label>
                                                    &nbsp;<asp:DropDownList ID="cboProductClass" AutoPostBack="true" CssClass="selProduct" runat="server" OnTextChanged="DoProc_ProductClass_Change"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProductClassX" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProductClassX_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                                            </tr>

	                <tr>
                        <td align="right" colspan="4" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="500px" runat="server"></asp:DropDownList>
                        </td>	                
	                </tr>

                    <tr>
                        <td align="left" colspan="4" valign="top" class="myMenu_Title"><%=STRMENU_TITLE%></td>
                    </tr>

                    <tr>
                        <td colspan="4"><hr /></td>
                    </tr>

                    <tr>
                        <td align="right" colspan="4" valign="top">&nbsp;<%=PageLinks%></td>                           
                    </tr>

                    <tr>
                        <td align="left" colspan="4" valign="top">&nbsp;
                            <asp:Label ID="lblMsg" Text="Status..." Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="right" colspan="1" valign="top">&nbsp;
                            <asp:Label ID="lblPro_Pol_Num" Text="Policy Number:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="3" valign="top">&nbsp;                     
                            <asp:TextBox ID="txtPro_Pol_Num" Width="250px" runat="server"></asp:TextBox>
                            &nbsp;<asp:Button ID="BUT_OK" Font-Bold="true" 
                                Text="View/Print Policy Schedule" runat="server" Width="220px" />
                            &nbsp;<asp:TextBox ID="txtFileNum" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtPrem_Rate_Code" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td align="right" colspan="4" valign="top">&nbsp;</td>
                    </tr>

                    <tr>
                        <td align="right" colspan="4" valign="top">&nbsp;</td>
                    </tr>


                    <tr>
                        <td colspan="4"><hr /></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4" valign="top">&nbsp;<%=PageLinks%></td>                           
                    </tr>
                    <tr>
                        <td colspan="4"><hr /></td>
                    </tr>

                    <tr style="display: none;">
                        <td align="right" colspan="4" valign="top">&nbsp;</td>
                    </tr>

                    <tr style="display: none;">
                        <td align="right" colspan="1" valign="top">&nbsp;
                            <asp:Label ID="lblDocName" Text="Document File Name:" runat="server"></asp:Label>
                        </td>
                        <td align="left" colspan="3" valign="top">&nbsp;                     
                            <asp:TextBox ID="txtDocName" Enabled="false" Text="c:\\temp\\test1.docx" Width="250px" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr style="display: none;">
                        <td align="left" colspan="4" valign="top" style=" height: 500px; width: 100%;">
                            <div id="div_doc" runat="server" title="Policy Document">
                                Document text here...
                            </div>
                            <iframe id="frmDoc" name="frmDoc" runat="server" style=" margin: 1px auto; width:95%;height:600px"></iframe>

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
            <td align="center" colspan="2" valign="top">
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

    </form>
</body>
</html>
