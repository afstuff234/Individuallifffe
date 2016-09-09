<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_BRK_CAT.aspx.vb" Inherits="I_LIFE_PRG_LI_BRK_CAT" %>

<%@ Register src="../UC_BANX.ascx" tagname="UC_BANX" tagprefix="uc1" %>
<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Class Setup Page</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
   </script>
   
</head>
<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">                      
        
        <uc1:UC_BANX ID="UC_BANX1" runat="server" />
        
    </div>


    <!-- content -->
    <div id="div_content" align="center">

        <table id="tbl_content" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="4">
                    <table align="center" border="1" class="tbl_buttons">
                        <tr>
                            <td align="left" colspan="2"><asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP()"></asp:button>
                                &nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                &nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" runat="server" text="Delete Data" OnClientClick="JSDelete_ASP()"></asp:button>
                                &nbsp;<asp:Button ID="cmdPrint_ASP" Enabled="false" CssClass="cmd_butt" runat="server" Text="Print" />
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;
                                <!-- <A HREF="<%= request.ApplicationPath %>/Setup/UCD001.aspx" TARGET="fraDetails">Previous Menu</A>&nbsp;| -->
                                <!-- &nbsp;|&nbsp;<a href="javascript:window.close()" style="font-weight:bold;">Close Page</a>&nbsp;| -->
    	                    </td>
    	                    <td align="right" colspan="2" valign="baseline">Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}"></input>&nbsp;
                                <asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                    </td>
                        </tr>
                    
                    </table>
                </td>
            </tr>

            <tr>
                <td align="center" colspan="4" valign="top" class="td_menu">
                    <table align="center" border="0" cellpadding="1" cellspacing="1"  class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" class="myMenu_Title"><%=STRPAGE_TITLE%></td>
                        </tr>
                	    <tr>
                	        <td align="left" nowrap colspan="3"><asp:Label id="textMessage" Text="Staus:" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                &nbsp;<a class="a_return_menu" href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=il_code_cust')">Returns to Previous Page</a>&nbsp;
                            </td>
    	               	</tr>

                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblRecID" Enabled="false" Text="Record ID:" runat="server"></asp:Label>&nbsp;</td>
            	    	    <td align="left" nowrap colspan="1"><asp:textbox id="txtRecID" Enabled="false" MaxLength="3" Width="50px" runat="server" EnableViewState="true"></asp:textbox></td>
                            <td align="right" valign="top" colspan="1"><asp:Label ID="lblRecNo" Text="Rec. No:" Enabled="false" runat="server"></asp:Label>&nbsp;</td>
                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18"></asp:TextBox>
                            </td>
        	        	</tr>
                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblCustNum" Text="Category Code:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustNum" MaxLength="5" Width="100px" AutoPostBack="true" runat="server" EnableViewState="true"></asp:textbox>
                		        &nbsp;<asp:DropDownList id="cboTransList" Width="350px" AutoPostBack="true" runat="server"></asp:DropDownList>&nbsp;
                            </td>
    		            </tr>
                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustName" Text="Category Description:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="3"><asp:textbox id="txtCustName" MaxLength="40" Width="350px" runat="server" EnableViewState="true" ></asp:textbox>&nbsp;</td>
    		            </tr>
                        <tr>
                            <td align="right" valign="top"><asp:Label ID="lblShortName" Text="Short Description:" runat="server"></asp:Label>&nbsp;</td>                    
                            <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtShortName" MaxLength="18" runat="server" 
                                    Width="200px"></asp:TextBox></td>                    
                        </tr>
                        <tr>
                            <td align="right" valign="top"><asp:Label ID="lblCustType" Text="Category Type:" runat="server"></asp:Label>&nbsp;</td>
                            <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtCustType" Enabled="false" MaxLength="3" runat="server" Width="50px"></asp:TextBox>&nbsp;
                                <select id="selCustType" name="selCustType" class="selScheme" runat="server" onchange="SelControl_OnChg(this.form.selCustType,this.form.txtCustType,this.form.txtCustTypeName)">
                                    <option value="*">None</option>
                                    <option value="01">Cash Customer</option>
                                </select>
                                &nbsp;<asp:TextBox ID="txtCustTypeName" Enabled="false" MaxLength="20" runat="server"></asp:TextBox>
                            </td>                    
                        </tr>

                		<tr>
                		    <td align="right" nowrap><asp:Label ID="lblMainType" runat="server">G/Ledger Account No:</asp:Label>&nbsp;</td>
    		                <td align="left" nowrap colspan="3"><asp:textbox id="txtGLAccCode" Enabled="false" MaxLength="10" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
                                &nbsp;<asp:DropDownList id="cboGLAccCode" Width="450px" AutoPostBack="true" runat="server"></asp:DropDownList>&nbsp;
                            </td>
                		</tr>

                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>

                    
                    <tr>
                    <td align="center" colspan="4" valign="top">
                    <table align="center" style="background-color: White; width: 95%;">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                    DataKeyNames="TBIL_CUST_CAT_REC_ID" HorizontalAlign="Left"
                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="false" PageSize="10"
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
                            
                                        <asp:BoundField readonly="true" DataField="TBIL_CUST_CAT_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_CUST_CAT_ID" HeaderText="RecID" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_CUST_CATEG" HeaderText="Code" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_CUST_CAT_DESC" HeaderText="Trans Description" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="TBIL_CUST_CAT_CNTRL_ACCT" HeaderText="G/L Account" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
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
