<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_SEC_USER_DETAIL.aspx.vb" Inherits="SEC_PRG_SEC_USER_DETAIL" %>
<%--<%@ Register src="../UC_BANX.ascx" tagname="UC_BANX" tagprefix="uc1" %>
--%>
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
   /* background-color: #FAEBD7; */
        background-color: #B0E0E6; /* color: #ffffff; */ /* font-family: Trebuchet MS, "Arial Narrow", "Times New Roman", Georgia, Times, serif; */;
            font-size: large;
            font-weight: bold; /* height: 17px; */;
            margin: 0px;
            padding: 5px 3px;
            text-align: left; /* text-decoration: blink, line-through; */;
            text-transform: uppercase;
            height: 38px;
        }
    </style>
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
                <td align="center" colspan="4" class="myMenu_Title_02">
                    <table align="center" border="0">
                        <tr>
                            <td align="left" colspan="2" valign="baseline" class="style2"><asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP()"></asp:button>
                                &nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                &nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" runat="server" text="Delete Data" OnClientClick="JSDelete_ASP()"></asp:button>
                                &nbsp;<asp:Button ID="cmdPrint_ASP" Enabled="false" CssClass="cmd_butt" Text="Print" runat="server" />
                                &nbsp;&nbsp;Status:
                                &nbsp;<asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;
                        	                    </td>
    	                    <td align="right" colspan="2" valign="baseline" class="style2">Find:&nbsp;
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
                            <td align="left" colspan="5" class="myheader"><%=STRPAGE_TITLE%></td>
                        </tr>
                	    <tr>
                	        <td align="left" nowrap colspan="3"><asp:Label id="lblMessage" Text="Staus:" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top" colspan="2">
                            &nbsp;<a id="PageAnchor_Return_Link" runat="server" class="a_return_menu" href="#" onclick="javascript:JSDO_RETURN('MGL_SEC.aspx?menu=SEC')">Returns to Previous Page</a>
                                &nbsp;<%=PageLinks%>&nbsp;
                            </td>
    	               	</tr>

                    <tr>
                    <td align="center" colspan="5" valign="top">
                    <table align="center" style="background-color: White; width: 95%;">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                    DataKeyNames="SEC_USER_REC_ID" HorizontalAlign="Left"
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
                            
                                        <asp:BoundField readonly="true" DataField="SEC_USER_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="SEC_USER_ID" HeaderText="Ref.ID" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="SEC_USER_LOGIN" HeaderText="Username" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="SEC_USER_FULLNAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                        <asp:BoundField readonly="true" DataField="SEC_USER_PHONE_NUM" HeaderText="Phone No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                    </Columns>
   
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    </td>
                    </tr>

                    <tr>
                        <td colspan="5"><hr /></td>
                    </tr>


                		<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblCustID" Enabled="false" Text="Record ID:" runat="server"></asp:Label>&nbsp;</td>
            	    	    <td align="left" nowrap colspan="4"><asp:textbox id="txtUserID" Enabled="false" 
                                    MaxLength="3" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
            	    	        &nbsp;<asp:Label ID="lblRecNo" Text="Rec. No:" Enabled="false" runat="server"></asp:Label>
            	    	        &nbsp;<asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18"></asp:TextBox>
                            </td>
        	        	</tr>
                		<tr>
    	                    <td align="right" nowrap>&nbsp;<asp:Label ID="lblFullName" Text="FullName:" 
                                    runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="4">&nbsp;<asp:textbox id="txtName" MaxLength="49" 
                                    Width="450px" runat="server" EnableViewState="true" ></asp:textbox>
                            </td>
    		            </tr>
                		<tr>
    		                <td align="right" nowrap>
                                <asp:Label ID="lblShortName" Text="Short Name :" 
                                    runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="4"><asp:textbox id="txtShortName" MaxLength="10" 
                                    Width="100px" AutoPostBack="true" runat="server" EnableViewState="true"></asp:textbox>
                		        </td>
    		            </tr>
        	        	<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblGroup" Text="Group:" runat="server"></asp:Label>&nbsp;</td>
        	        	    <td align="left" nowrap colspan="4"><asp:textbox id="txtGroup" Enabled="false" 
                                    MaxLength="3" Width="100px" runat="server" EnableViewState="true"></asp:textbox>
                		        &nbsp;<asp:DropDownList id="cboGroup" Visible="true" Width="200px" 
                                    AutoPostBack="True" runat="server"></asp:DropDownList>
                            </td>
        	        	</tr>
        	        	<tr>
    	                    <td align="right" nowrap><asp:Label ID="lblRole" Text="Role:" runat="server"></asp:Label>&nbsp;</td>
        	        	    <td align="left" nowrap colspan="4">&nbsp;<asp:DropDownList 
                                    id="cboRole" Visible="true" Width="200px" runat="server"></asp:DropDownList>
                            </td>
        	        	</tr>

                		<tr>
    		                <td align="right" nowrap>
                                <asp:Label ID="lblBranch" Text="Branch:" 
                                    runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="4"><asp:textbox id="txtBranch" MaxLength="39" 
                                    Width="400px" runat="server" EnableViewState="true" ></asp:textbox></td>
    		            </tr>
                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustPhone01" Text="GSM/Mobile No:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="4"><asp:textbox id="txtCustPhone01" MaxLength="11" Width="200px" runat="server" EnableViewState="true" ></asp:textbox>
                		        &nbsp;<asp:Label ID="lblCustPhone02" Text="Land Line No:" runat="server"></asp:Label>
                		        &nbsp;<asp:textbox id="txtCustPhone02" MaxLength="11" Width="102px" 
                                    runat="server" EnableViewState="true" ></asp:textbox>
                		    </td>
    		            </tr>

                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustEmail01" Text="Email Address - 1:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="4"><asp:textbox id="txtCustEmail01" MaxLength="49" Width="450px" runat="server" EnableViewState="true" ></asp:textbox></td>
    		            </tr>

                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustEmail02" Text="Email Address - 2:" runat="server"></asp:Label>&nbsp;</td>
                		    <td align="left" nowrap colspan="4"><asp:textbox id="txtCustEmail02" MaxLength="49" Width="450px" runat="server" EnableViewState="true" ></asp:textbox></td>
    		            </tr>
                    
                		<tr>
    		                <td align="right" nowrap><asp:Label ID="lblCustEmail6" Text="Password Expiry Days:" 
                                    runat="server"></asp:Label></td>
                		    <td align="left" nowrap>
                                <asp:textbox id="txtPassExpDays" MaxLength="11" Width="79px" 
                                    runat="server" EnableViewState="true" ></asp:textbox>
                		    </td>
                		    <td align="left" nowrap colspan="2" align="right">
                                <asp:Label ID="lblCustEmail7" 
                                    Text="Password Expiry Date:" runat="server" Visible="False"></asp:Label></td>
                		    <td align="left" nowrap>
                                <asp:textbox id="txtPassExpDate" MaxLength="11" 
                                    Width="99px" runat="server" EnableViewState="true" Enabled="False" 
                                    Height="19px" Visible="False" ></asp:textbox>
                		    </td>
    		            </tr>
                    
                		<tr>
    		                <td align="right" nowrap>
                                <asp:Label ID="lblCustEmail8" Text="Status:" 
                                    runat="server"></asp:Label></td>
                		    <td align="left" nowrap>
                                <asp:CheckBox ID="chkStatus" runat="server" />
                		    </td>
                		    <td align="left" nowrap colspan="2" align="right">
                                <asp:Label ID="lblCustEmail9" Text="Uncheck to deactivate user" 
                                    runat="server"></asp:Label></td>
                		    <td align="left" nowrap>&nbsp;</td>
    		            </tr>
                    
                		<tr>
    		                <td align="left" nowrap colspan="5" class="myMenu_Title"><asp:Label ID="lblCustEmail5" 
                                    Text="Login Detail:" runat="server"></asp:Label></td>
    		            </tr>
                    
                		<tr>
    		                <td align="right" nowrap class="style1">
                                <asp:Label ID="lblLoginName" 
                                    Text="Username:" runat="server"></asp:Label></td>
                		    <td align="left" nowrap colspan="4" class="style1">
                                <asp:textbox id="txtLoginName" 
                                    MaxLength="11" Width="200px" runat="server" EnableViewState="true" 
                                    AutoPostBack="True" ></asp:textbox>
                		        </td>
    		            </tr>
                    
                		<tr>
    		                <td align="right" nowrap class="style1">
                                <asp:Label ID="lblPassword" 
                                    Text="Password:" runat="server"></asp:Label></td>
                		    <td align="left" nowrap colspan="4" class="style1"><asp:textbox id="txtPassword" 
                                   Width="200px" runat="server" EnableViewState="true" 
                                    TextMode="Password" ></asp:textbox>
                		        </td>
    		            </tr>
                    
                		<tr>
    		                <td align="right" nowrap class="style1">
                                <asp:Label ID="lblConPassword" 
                                    Text="Confirm Password:" runat="server"></asp:Label></td>
                		    <td align="left" nowrap colspan="4" class="style1"><asp:textbox id="txtConPassword" 
                                  Width="200px" runat="server" EnableViewState="true" 
                                    TextMode="Password" ></asp:textbox>
                		        </td>
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
