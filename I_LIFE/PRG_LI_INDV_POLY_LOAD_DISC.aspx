<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_POLY_LOAD_DISC.aspx.vb" Inherits="I_LIFE_PRG_LI_INDV_POLY_LOAD_DISC" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Individual Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
        
    <style>
        .RecordOriginator
        {
            margin-left: 100px;
        }
    </style>
    <script>
        function ConfirmDoNew() {
            var r = confirm("Are you sure you want to clear screen?")
            if (r) {
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

    <!-- start header -->
    <div id="div_header" align="center">
        <table id="tbl_header" align="center">
            <tr>
                <td align="left" valign="top" class="myMenu_Title_02">
                    <table border="0" width="100%">

                        <tr style="display: none;">
                            <td align="left" colspan="2" valign="top"><%=STRMENU_TITLE%></td>
                            <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;&nbsp;Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;&nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;&nbsp;<asp:DropDownList ID="cboSearch" runat="server" Height="26px" 
                                    Width="150px"></asp:DropDownList>
                            </td>
                        </tr>

                                    <tr style="display: none;">
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="return ConfirmDoNew();"></asp:button>
	                                        &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
		                		        	&nbsp;&nbsp;<asp:Button ID="cmdDelItem_ASP" CssClass="cmd_butt" Enabled="false" Font-Bold="true" Text="Delete Item" OnClientClick="JSDelItem_ASP()" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" text="Print"></asp:button>
                                            &nbsp;&nbsp;<asp:Button ID="cmdNext" CssClass="cmd_butt" Enabled="false" Text="Next..»" runat="server" />
                                            &nbsp;&nbsp;
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
                    <td nowrap class="myheader">Policy Loading and Discount Information</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                    <asp:Label ID="lblOriginator" ForeColor="Red" Font-Size="Small" 
                                        runat="server" CssClass="RecordOriginator"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblFileNum" Enabled="false" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNum" Enabled="false" Width="230px" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                            </tr>
                        
                            <tr>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblQuote_Num" Enabled="false" Text="Proposal No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top" colspan="1"><asp:Label ID="lblRecNo" BorderStyle="Solid" Text="Rec. No:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18"></asp:TextBox>
                                                </td>
                            </tr>

                                            <tr>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblProductClass" Text="Product Category:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboProductClass" Enabled="false" AutoPostBack="false" CssClass="selProduct" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="40" runat="server"></asp:TextBox>
                                                </td>                                            
                                                <td align="left" valign="top"><asp:Label ID="lblProduct_Num" Enabled="false" Text="Product Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList id="cboProduct" Enabled="false" AutoPostBack="false" CssClass="selProduct" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>

                            <tr>
                                <td colspan="4">
                                    <table align="center" border="1" style="width: 100%">
                                        <tr class="tr_frame">
                                            <td align="left" valign="top"><asp:Label ID="lblLoad_Disc_SN" Text="S/No" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblLoad_Disc_Type" Text="Loading/Discount Type" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblLoad_Disc_Prem_Rate_Type" Text="Prem Rate Type:" runat="server"></asp:Label></td>
                                            <td align="left" valign="top" colspan="1"><asp:Label ID="lblLoad_Disc_Percent" Text="Percent %" runat="server"></asp:Label></td>                                            
                                            <td align="left" valign="top"><asp:Label ID="lblLoad_Disc_Amt" Text="Load/Disc Amt:" ToolTip="Loading or Discount Amount" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top"><asp:TextBox ID="txtLoad_Disc_SN" Enabled="false" Width="40px" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboLoad_Disc_Type" Width="120px" runat="server" AutoPostBack="true" OnTextChanged="DoProc_Load_Disc_Type_Change">
                                                    <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                    <asp:ListItem Value="L">Loading</asp:ListItem>
                                                    <asp:ListItem Value="D">Discount</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtLoad_Disc_Type" Enabled="false" Width="30px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtLoad_Disc_TypeName" Visible="false" Enabled="false" Width="30px" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboLoad_Disc_Prem_Rate_Type" Width="120px" runat="server" 
                                                        AutoPostBack="True">
                                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                                        <asp:ListItem Value="A">Fixed Amount</asp:ListItem>
                                                        <asp:ListItem Value="F">Fixed Rate</asp:ListItem>
                                                        <asp:ListItem Value="R">Table Rate</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtLoad_Disc_Prem_Rate_TypeNum" Visible="true" Enabled="false" MaxLength="1" Width="30px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtLoad_Disc_Prem_Rate_TypeName" Visible="false" Enabled="false" MaxLength="1" Width="20px" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtLoad_Disc_Percent" 
                                                    runat="server" Width="60px"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtLoad_Disc_Amt" MaxLength="15" runat="server"></asp:TextBox></td>
                                        </tr>
                                        
                                        <tr class="tr_frame">
                                            <td align="left" valign="top" colspan="2"><asp:Label ID="lblLoad_Disc_Num" Text="Loading/Discount Code" runat="server"></asp:Label>
                                            </td>
                                            <td align="left" valign="top" colspan="2"><asp:Label ID="lblLoad_Disc_SA_LC" Text="Sum Assured LC" ToolTip="LC=Local Currency" runat="server"></asp:Label>
                                            </td>
                                            <td align="left" valign="top" colspan="1"><asp:Label ID="lblLoad_Disc_SA_FC" Enabled="false" Text="Sum Assured FC" ToolTip="FC=Foreign Currency" runat="server"></asp:Label></td>
                                        </tr>
                                        
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top" colspan="2">
                                                <asp:DropDownList ID="cboLoad_Disc_Num" Width="200px" runat="server">
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtLoad_Disc_Num" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtLoad_Disc_NumName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox></td>
                                           <td align="left" valign="top" colspan="2"><asp:TextBox ID="txtLoad_Disc_SA_LC" MaxLength="15" runat="server"></asp:TextBox>
                                           </td>
                                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtLoad_Disc_SA_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox></td>
                                        </tr>
                                        
                                        <tr class="tr_frame">
                                            <td align="left" valign="top" colspan="2"><asp:Label ID="lblLoad_Disc_Applied_On" Text="Loadig/Discount Applied On" ToolTip="" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblCover_Num" Text="Select Applicable Cover" ToolTip="" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblCover_Rate" Text="Rate %" ToolTip="Rate Applicable" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblCover_Rate_Per" Text="Rate Per" ToolTip="" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top" colspan="2">
                                                <asp:DropDownList ID="cboLoad_Disc_Applied_On" Width="130px" runat="server">
                                                    <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                    <asp:ListItem Value="S">Sum Assured</asp:ListItem>
                                                    <asp:ListItem Value="P">Cover Premium</asp:ListItem>
                                                    <asp:ListItem Value="T">Total Premium</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtLoad_Disc_Applied_On" Visible="false" Enabled="false" MaxLength="3" Width="40px" ToolTip="" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtLoad_Disc_Applied_OnName" Visible="false" Enabled="false" Width="40px" ToolTip="" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboCover_Name" Width="200px" runat="server">
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtCover_Num" Visible="false" Enabled="false" MaxLength="10" Width="40px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtCover_Name" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtCover_Rate" Enabled="true" Width="60px" MaxLength="10" ToolTip="Rate Applicable" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboCover_Rate_Per" Width="130px" runat="server">
                                                    <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                    <asp:ListItem Value="100">Rate Per 100</asp:ListItem>
                                                    <asp:ListItem Value="1000">Rate Per 1000</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtCover_Rate_Per" Visible="false" Enabled="false" MaxLength="10" Width="40px" ToolTip="" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtCover_Rate_PerName" Visible="false" Enabled="false" Width="40px" ToolTip="" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                                        <tr>
                                            <td colspan="4"><hr /></td>
                                        </tr>
                    
                            <tr>
                                <td align="center" colspan="4" valign="top">
                                    <table align="center" style="background-color: White; width: 95%;">
                                        <tr>
                                            <td align="left" colspan="4" valign="top">
                                                &nbsp;<asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                                    DataKeyNames="TBIL_POL_DISC_REC_ID" HorizontalAlign="Left"
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
                            
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_DISC_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_DISC_LOAD_TYPE_NAME" HeaderText="L/D Type" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_DISC_LOAD_RT_AMT_CD" HeaderText="Rate Type" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />                                                        
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_DISC_RATE" HeaderText="Rate %" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_DISC_RATE_PER" HeaderText="Rate Per" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />                                                        
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_DISC_LOAD_PREM_AMT" HeaderText="Load/Disc Amt" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />                                                        
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_DISC_APPLIED_NAME" HeaderText="Applied On" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                    </Columns>
   
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
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

    </form>
</body>
</html>
