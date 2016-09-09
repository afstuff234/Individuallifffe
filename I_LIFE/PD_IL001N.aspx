<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PD_IL001N.aspx.vb" Inherits="I_LIFE_PD_IL001N" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Page</title>
    <link rel="Stylesheet" href="../SS_ILIFE_02.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/JS_DOC.js"></script>
</head>

<body onload="<%= FirstMsg %>">
    <form id="form1" runat="server">
    
    <div align="center" class="mydiv">

    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%" class="tablemax">

    <!-- main banner -->
	<tr>
	   <td colspan="2" valign="top">
		<table border="0" cellSpacing="0" cellPadding="0" width="100%" bgcolor="#f1f1f1" style="border-bottom: #a4a4a4 1px solid; padding-left: 10px; padding-right: 10px; padding-top: 8px; padding-bottom: 0px;">
			<tr>
			    <td colspan="1" valign="top" align="left">
			        <img alt="Logo" src="../Images/CAILogoN.jpg" 
                        style="border: 1px solid #c0c0c0; height: 120px; width: 250px;" />
			    </td>
			    <td colspan="1" valign="top" align="right" style="font-family: Trebuchet MS; font-size: 9pt; font-weight: bold; padding-top: 1px;">
			            <a href="http://www.google.com" target="_blank">Google</a> |
			            <a href="http://www.yahoo.com" target="_blank">Yahoo</a> |
			            <a href="http://www.gmail.com" target="_blank">GMail</a> |
			            <a href="http://www.facebook.com" target="_blank">Facebook</a> |
			            <a href="http://www.twitter.com" target="_blank">Twitter</a>			        
			     </td>
			</tr>
			<tr>
			    <td align="left" colspan="2">
			        <div>
    			        <span style="font-size:large;">&nbsp;<%=STRCOMP_NAME%></span>
			        </div>
			    </td>
			</tr>
		</table>
	   </td>
	</tr>
	
    <!-- sub banner 2 -->
	<tr>
	   <td colspan="2" valign="top">
		<table border="0" cellSpacing="0" cellPadding="0" width="100%" style="border-bottom: #a4a4a4 1px solid; padding-left: 10px; padding-right: 10px; padding-top: 8px; padding-bottom: 0px;">
			<tr>
			    <td valign="top" align="left" style="height: 30px;">
			        <span style="font-size: medium;">Policy Document/Schedule Prints</span>
				    <div style="display: none; font-size: 8pt; padding-top: 2px; padding-bottom: 10px;">
					    <a href="#">Home</a> |
					    <a href="#">Previous</a> |
	    				<a href="#">Next</a> |
	    				&nbsp;<b><a href="javascript:window.close();">Log out...</a></b>
		    		</div>                    
			    </td>
			    <td valign="top" align="right">
			        <span style="font-size: small;">ABS Individual Life Application</span>
				    <div style="display: none; font-size: 8pt; padding-top: 2px; padding-bottom: 10px;">
					    Version 1.0.1
				    </div>			        
			    </td>
			</tr>
		</table>
	   </td>
	</tr>

    <!-- contents -->
	<tr>
	   <td colspan="2" valign="top" style=" height: 600px;">
		<table border="0" cellSpacing="0" cellPadding="0" width="100%"  bgcolor="#f1f1f1" style="border-bottom: #a4a4a4 1px solid; padding-left: 10px; padding-right: 10px; padding-top: 8px; padding-bottom: 0px;">

	                <tr>
                        <td align="right" colspan="1" valign="top">    
                            Find Insured Name:
                        </td>    
                        <td align="left" colspan="1" valign="top">    
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" runat="server"></asp:DropDownList>
                        </td>	                
	                </tr>

            <tr style="display:block;">
                <td align="right" colspan="2" valign="top">&nbsp;<%=PageLinks%></td>                           
            </tr>

            <tr>
                <td align="left" colspan="2">
                    <hr />
                </td>
            </tr>

            <tr>
                <td align="left" colspan="2" valign="top">
                    <asp:Label ID="lblMsg" Text="Status..." Font-Bold="false" ForeColor="Red" runat="server"></asp:Label>
                </td>
            </tr>

			<tr>
			    <td colspan="2" valign="top">&nbsp;</td>
			</tr>

			<tr>
			                    <td align="right" valign="top"><asp:Label ID="lblPro_Pol_Num" Text="Policy Number:" runat="server"></asp:Label></td>
			                    <td align="left" valign="top"><asp:TextBox ID="txtPro_Pol_Num" runat="server" Width="250px"></asp:TextBox>                                    
                                    &nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtPrem_Rate_Code" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
			</tr>
			<tr>
			    <td align="left" colspan="1"></td>
			    <td align="left" colspan="1">
                    <asp:Button ID="BUT_OK" Font-Bold="true" Text="View/Print Policy Schedule" runat="server" Width="220px" />			    </td>
			</tr>
			<tr>
			    <td align="left" colspan="2">
			        <div id="div_doc" runat="server"></div>
			    </td>
			</tr>
			<tr>
			    <td colspan="2" valign="top">&nbsp;</td>
			</tr>

            <tr>
                <td align="left" colspan="2">
                    <hr />
                </td>
            </tr>

            <tr>
                <td align="right" colspan="2" valign="top">&nbsp;<%=PageLinks%></td>                           
            </tr>

			<tr>
			    <td colspan="2" valign="top">&nbsp;</td>
			</tr>

		</table>
	   </td>
	</tr>

    <!-- footer -->
	<tr>
	   <td colspan="2" valign="top">
		<table border="0" cellSpacing="0" cellPadding="0" width="100%" bgcolor="#f1f1f1" style="border-bottom: #a4a4a4 1px solid; padding-left: 10px; padding-right: 10px; padding-top: 8px; padding-bottom: 0px;">
			<tr>
			    <td colspan="2" valign="top" align="left">
			        Copyright &copy; 2014. &nbsp; Allright reserve &nbsp; <%=STRCOMP_NAME%> &nbsp;
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
