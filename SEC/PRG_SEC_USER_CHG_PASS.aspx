<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_SEC_USER_CHG_PASS.aspx.vb" Inherits="SEC_PRG_SEC_USER_CHG_PASS" %>
<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>
<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Details Setup Page</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
   </script>
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
        .style2
        {
            height: 45px;
        }
        .style3
        {
            /* background-color: Black; */
  /* background-color: #4682B4; */
        background-color: #FAEBD7; /* background-color: #B0E0E6; */;
            background-color: #DBEAF5; /* color: #ffffff; */;
            font-family: Trebuchet MS, "Arial Narrow", "Times New Roman", Georgia, Times, serif;
            font-size: large;
            font-weight: bold; /* height: 17px; */;
            margin: 0px;
            padding: 5px 3px;
            text-align: left;
            text-decoration: blink, line-through;
            text-transform: capitalize;
            height: 11px;
        }
        </style>
        <script>
            function confirmLogout() {
                var result = confirm("Are you sure you want to logout?")
                if (result) {
                    return true;
                }
                else {
                    return false;
                }
            }
        
        </script>
</head>
<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">                      
        
        <uc1:UC_BANT ID="UC_BANT1" runat="server" />
        
    </div>
<div id="div_content" align="center">

        <table id="tbl_content" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="4" class="style3">
                    <table align="center" border="0">
                        <tr>
                            <td align="left" colspan="2" valign="baseline" class="style2">&nbsp;<asp:button 
                                    id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Change"></asp:button>
                                &nbsp;
                        	                    </td>
    	                    <td align="right" colspan="2" valign="baseline" class="style2">&nbsp;
                                </td>
                        </tr>
                    
                    </table>
                </td>
            </tr>


            <tr>
                <td align="center" colspan="4" valign="top" class="td_menu">
                    <table align="center" border="0" cellpadding="1" cellspacing="1"  class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="5" class="myheader"><%=STRPAGE_TITLE%></td>
                        </tr>
                	    <tr>
                	        <td align="left" nowrap colspan="3"><asp:Label id="lblMessage" Text="Staus:" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top" colspan="2"> <asp:LinkButton ID="LnkBut_LogOff" runat="server" Text="LOG OFF" OnClientClick="return confirmLogout();"></asp:LinkButton>
                           
                                &nbsp;<%=PageLinks%>&nbsp;
                            </td>
    	               	</tr>


                		<tr>
    		                <td align="right" nowrap class="style1">
                                New                                 <asp:Label ID="lblPassword" 
                                    Text="Password:" runat="server"></asp:Label></td>
                		    <td align="left" nowrap colspan="4" class="style1"><asp:textbox id="txtPassword" 
                                    MaxLength="11" Width="200px" runat="server" EnableViewState="true" 
                                    TextMode="Password" ></asp:textbox>
                		        </td>
    		            </tr>
                    
                		<tr>
    		                <td align="right" nowrap class="style1">
                                <asp:Label ID="lblConPassword" 
                                    Text="Confirm Password:" runat="server"></asp:Label></td>
                		    <td align="left" nowrap colspan="4" class="style1"><asp:textbox id="txtConPassword" 
                                    MaxLength="11" Width="200px" runat="server" EnableViewState="true" 
                                    TextMode="Password" ></asp:textbox>
                		        </td>
    		            </tr>
                    
                		<tr style="display:none;">
    		                <td align="right" nowrap class="style1">
                                <asp:Label ID="lblCustEmail6" Text="Password Expiry Days:" 
                                    runat="server"></asp:Label></td>
                		    <td align="left" nowrap class="style1">
                                <asp:textbox id="txtPassExpDays" MaxLength="11" Width="47px" 
                                    runat="server" EnableViewState="true" Enabled="False" ></asp:textbox>
                		        <asp:Label ID="lblCustEmail7" 
                                    Text="Password Expiry Date:" runat="server"></asp:Label>&nbsp;<asp:textbox id="txtPassExpDate" MaxLength="11" 
                                    Width="99px" runat="server" EnableViewState="true" Enabled="False" 
                                    Height="19px" ></asp:textbox>
                		        </td>
                		    <td align="left" nowrap colspan="2" class="style1">&nbsp;</td>
                		    <td align="left" nowrap class="style1">&nbsp;</td>
    		            </tr>
                    
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
    		
                    </table>
                
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>

        </table>
    
    </div>

<!-- footer -->
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
