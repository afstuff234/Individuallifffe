<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_CODES.aspx.vb" Inherits="I_LIFE_PRG_LI_CODES" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Codes Setup Page</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
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
            <tr style="display: none;">
                <td align="left" valign="top" class="myMenu_Title_02">
                    <table border="0" width="100%">
                        <tr style="display: none;">
                            <td align="right"  colspan="2" valign="top" style="display: none;">
                                &nbsp;<a class="HREF_MENU2" href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=il_code_std')">Returns to Previous Page</a>&nbsp;
                            </td>

       	                    <td align="right" colspan="1" valign="baseline" style="display: none;"><asp:Label ID="lblAction" Text="Status:" runat="server"></asp:Label>
                           	    &nbsp;&nbsp;<asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="False" Width="50px"></asp:textbox></td>

                           	<td align="right" colspan="1" style="display: none;">    
      	                        &nbsp;&nbsp;Find:&nbsp;
                                                              <input type="text" id="txtSearch" name="txtSearch" disabled="disabled" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}"></input>&nbsp;
                                    <asp:Button ID="cmdSearch" Text="Search" Enabled="false" runat="server" />
            	            </td>

                        </tr>
                    </table>
                </td>
            </tr>

        </table>                                                                                                                                    
    </div>

<!-- content -->
<div id="div_content" align="center">

<table id="tbl_content" align="center" border="0" cellpadding="0" cellspacing="0">
    
    <tr>
        <td align="center" colspan="4" valign="top" class="td_menu">
            <table align="center" border="0" cellpadding="1" cellspacing="1"  class="tbl_menu_new">
                <tr>
                    <td align="left" colspan="4" class="myMenu_Title"><%=STRPAGE_TITLE%></td>
                </tr>
                <tr>
                    <td align="left" colspan="3" valign="top"><asp:Label ID="lblMessage" Text="Status:" ForeColor="Red" runat="server"></asp:Label></td>
                    <td align="right" valign="top">
                        &nbsp;<a id="PageAnchor_Return_Link" runat="server" class="a_return_menu" href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=il_code_std')">Returns to Previous Page</a>
                        &nbsp;<%=PageLinks%>
                    </td>
                </tr>    

                <tr>
                    <td align="right" valign="top"><asp:Label ID="lblGroupNum" Text="Group Code:" runat="server"></asp:Label>&nbsp;</td>                    
                    <td align="left" valign="top" colspan="3">
                        <asp:TextBox ID="txtGroupNum" Enabled="false" MaxLength="4" Width="100px" runat="server"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblRecNo" BorderColor="#ff8080" BorderStyle="Solid" BorderWidth="1px" Text="Rec. No:" Enabled="false" runat="server"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td align="right" valign="top"><asp:CheckBox id="chkNum" runat="server" AutoPostBack="true" class="chk_Butt" />
                        &nbsp;<asp:Label ID="lblNum" Text="Trans Code:" runat="server"></asp:Label>&nbsp;
                    </td>                    
                    <td align="left" valign="top" colspan="3">
                        <asp:TextBox ID="txtNum" AutoPostBack="true" MaxLength="4" runat="server" Width="100px" ></asp:TextBox>
                        &nbsp;<asp:DropDownList ID="ddlGroup" AutoPostBack="true" runat="server" Width="300px"></asp:DropDownList>                    </td>                    
                </tr>
                
                <tr>
                    <td align="right" valign="top"><asp:Label ID="lblTransName" Text="Long Description:" runat="server"></asp:Label>&nbsp;</td>                    
                    <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtTransName" MaxLength="95" runat="server" 
                            Width="400px"></asp:TextBox></td>                    
                </tr>

                <tr>
                    <td align="right" valign="top"><asp:Label ID="lblShortName" Text="Short Description:" runat="server"></asp:Label>&nbsp;</td>                    
                    <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtShortName" MaxLength="18" runat="server" 
                            Width="200px"></asp:TextBox></td>                    
                </tr>
          &nbsp;</td>
                </tr>

            	   	<tr>
                        <td  align="right" colspan="1" style="height: 26px">
				        	<asp:Button ID="cmdDelItem_ASP" CssClass="cmd_butt" Enabled="true" Font-Bold="true" Text="Delete Item" OnClientClick="JSDelItem_ASP()" runat="server" />
		        		</td>
                        <td height="26" align="left"><asp:button id="cmdSave" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button></td>
                        <td height="26" align="center"><asp:button id="cmdNew_ASP" CssClass="cmd_butt" Visible="true"  OnClientClick="JSNew_ASP();" runat="server" text="New Data"></asp:button></td>
                        <td height="26" align="center"><asp:button id="cmdDelete_ASP" CssClass="cmd_butt" Visible="false" OnClientClick="JSDelete_ASP()" runat="server" text="Delete Data"></asp:button>
                            &nbsp;&nbsp;<asp:Button ID="cmdDelItem" Enabled="false" CssClass="cmd_butt" Visible="false" Text="Delete Item" runat="server" />
                        </td>    	                            
            	   	</tr>
                    
                    <tr>
                    <td align="center" colspan="4" valign="top">
                    <table align="center" style="background-color: White; width: 95%;">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                    DataKeyNames="TBIL_COD_REC_ID" HorizontalAlign="Left"
                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true" PageSize="10"
                                    PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                    EmptyDataText="No data available..."
                                    GridLines="Both" ShowFooter="True">                        

                        
                                    <PagerStyle CssClass="grd_page_style" />
                                    <HeaderStyle CssClass="grd_header_style" />
                                    <RowStyle CssClass="grd_row_style" />
                                    <SelectedRowStyle CssClass="grd_selrow_style" />
                                    <EditRowStyle CssClass="grd_editrow_style" />
                                    <AlternatingRowStyle CssClass="grd_altrow_style" />
                                    <FooterStyle CssClass="grd_footer_style" />
                    
                                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" 
                                        PreviousPageText="Previous">
                                    </PagerSettings>
                        
                                    <Columns>
                                        <asp:TemplateField>
        			                        <ItemTemplate>
        						                <asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            
                                        <asp:CommandField ShowSelectButton="True" />
                            
                                        <asp:BoundField readonly="true" DataField="TBIL_COD_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_COD_TYP" HeaderText="Rec.ID" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_COD_ITEM" HeaderText="Code" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_COD_LONG_DESC" HeaderText="Trans Description" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                    </Columns>
   
                                </asp:GridView>
                            </td>
                        </tr>
                    
                    </table>
                    </td>
                    </tr>

                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    
                        
            </table>
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
