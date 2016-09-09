<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_POLY_ADD_COVER.aspx.vb" Inherits="I_LIFE_PRG_LI_INDV_POLY_ADD_COVER" %>

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
            margin-left:100px;
        }
    </style>
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
                                        <td align="center" colspan="4" valign="top" style="height: 26px">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
	                                        &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
		                		        	&nbsp;&nbsp;<asp:Button ID="cmdDelItem_ASP" CssClass="cmd_butt" Enabled="false" Font-Bold="true" Text="Delete Item" OnClientClick="JSDelItem_ASP()" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" text="Print"></asp:button>
                                            &nbsp;&nbsp;<asp:Button ID="cmdNext" CssClass="cmd_butt" Enabled="false" Text="Next..»" runat="server" />
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
                    <td nowrap class="myheader">Additional Covers Information</td>
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
                                                <td align="left" valign="top"><asp:Label ID="lblDOB" Enabled="false" Text="Date of Birth:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtDOB" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblDOB_Format" Enabled="false" Text="" runat="server"></asp:Label></td>
                                                <td align="right" valign="top" colspan="1"><asp:Label ID="lblDOB_ANB" Enabled="false" Text="Age (ANB):" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtDOB_ANB" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>

                                            <tr>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblProductClass" Text="Product Category:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboProductClass" Enabled="false" AutoPostBack="false" CssClass="selProduct" runat="server" OnTextChanged="DoProc_ProductClass_Change"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="50" runat="server"></asp:TextBox></td>                                            
                                                <td align="right" valign="top"><asp:Label ID="lblProduct_Num" Text="Product Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList id="cboProduct" Enabled="false" AutoPostBack="false" CssClass="selProduct" runat="server" OnTextChanged="DoProc_Product_Change"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProduct_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                                            </tr>

                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Additional Covers Details</td>
                                    </tr>
                                                                        
                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblCover_Num" Text="Cover Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboCover_Name" AutoPostBack="false" CssClass="selProduct" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtCover_Num" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtCover_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                                </td>                                            
                                                <td align="right" valign="top"><asp:Label ID="lblPlan_Num" Visible="false" Text="Plan Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboPlan_Name" Visible="false" AutoPostBack="false" CssClass="selProduct" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtPlan_Num" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtPlan_Name" Visible="false" Enabled="false" Width="20" runat="server"></asp:TextBox>
                                                </td>                                            
                                            </tr>

                            <tr>
                                <td nowrap colspan="4" class="td_frame">
                                    <table align="center" border="1">
                                        <tr class="tr_frame">
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblAdd_Period_Yr" Text="Period Cover (Yrs)" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:Label ID="lblAdd_Start_Date" Text="Start Date" runat="server"></asp:Label>
                                                    <br /><asp:Label ID="lblStart_Date_Format" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top"><asp:Label ID="lblAdd_End_Date" Enabled="false" Text="Expiry Date" runat="server"></asp:Label>
                                                    <br /><asp:Label ID="lblEnd_Date_Format" Enabled="false" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="2"><asp:Label ID="lblAdd_No_Prem_Yr" Text="No of Years Prem is to be Paid" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                                <td align="left" colspan="1" valign="top"><asp:TextBox ID="txtAdd_Period_Yr" MaxLength="3" Width="60px" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtAdd_Start_Date" MaxLength="10" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtAdd_End_Date" MaxLength="10" Enabled="false" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top" colspan="2"><asp:TextBox ID="txtAdd_No_Prem_Yr" MaxLength="3" runat="server"></asp:TextBox></td>
                                        </tr>

                                        <tr class="tr_frame">
                                            <td align="left" valign="top" colspan="1"><asp:Label ID="lblAdd_Prem_Rate_Type" Text="Prem Rate Type:" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblAdd_Prem_Amt" Text="Additional Amount" ToolTip="" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblAdd_Prem_Fixed_Rate" Text="Fixed Rate:" runat="server"></asp:Label></td>
                                            <td align="left" valign="top" colspan="1"><asp:Label ID="lblAdd_Prem_Fixed_Rate_Per" Text="Fixed Rate Per:" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblAdd_Prem_Rate_Applied_On" Text="Applied Rate on:" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboAdd_Prem_Rate_Type" Width="100px" AutoPostBack="true" runat="server" OnTextChanged="DoProc_Premium_RateType_Change">
                                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                                        <asp:ListItem Value="A">Fixed Amount</asp:ListItem>
                                                        <asp:ListItem Value="F">Fixed Rate</asp:ListItem>
                                                        <asp:ListItem Value="R">Table Rate</asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtAdd_Prem_Rate_TypeNum" Visible="false" Enabled="false" MaxLength="1" Width="20px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtAdd_Prem_Rate_TypeName" Visible="false" Enabled="false" MaxLength="1" Width="20px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtAdd_Prem_Amt" MaxLength="15" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtAdd_Prem_Fixed_Rate" MaxLength="8" Width="80px"  runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top" colspan="1">
                                                <asp:DropDownList ID="cboAdd_Prem_Fixed_Rate_Per" Width="120px" runat="server">
                                                    <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                    <asp:ListItem Value="100">Rate Per 100</asp:ListItem>
                                                    <asp:ListItem Value="1000">Rate Per 1000</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtAdd_Prem_Fixed_Rate_PerNum" Visible="false" Enabled="false" MaxLength="5" Width="20px" ToolTip="" runat="server"></asp:TextBox>
                                                &nbsp;<asp:TextBox ID="txtAdd_Prem_Fixed_Rate_PerName" Visible="false" Enabled="false" Width="20px" ToolTip="" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top" colspan="1">
                                                <asp:DropDownList ID="cboAdd_Prem_Rate_Applied_On" Width="120px" runat="server">
                                                    <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                    <asp:ListItem Value="S">Sum Assured</asp:ListItem>
                                                    <asp:ListItem Value="P">Basic Prem</asp:ListItem>
                                                    <asp:ListItem Value="N">Not Applicable</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtAdd_Prem_Rate_Applied_On_Num" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                                &nbsp;<asp:TextBox ID="txtAdd_Prem_Rate_Applied_On_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        
                                        <tr class="tr_frame">
                                            <td align="left" valign="top"><asp:Label ID="lblAdd_SA_LC" Text="Sum Assured LC" ToolTip="LC=Local Currency" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblAdd_SA_FC" Enabled="false" Text="Sum Assured FC" ToolTip="FC=Foreign Currency" runat="server"></asp:Label></td>
                                            <td align="left" valign="top" colspan="2"><asp:Label ID="lblAdd_Prem_Rate_Code" Text="Premium Rate Code" runat="server"></asp:Label></td>
                                            <td align="left" valign="top" colspan="1"><asp:Label ID="lblAdd_Prem_Rate" Enabled="false" Text="Prem Rate" runat="server"></asp:Label></td>
                                        </tr>
                                        
                                        <tr style="font-size: small;">
                                           <td align="left" valign="top"><asp:TextBox ID="txtAdd_SA_LC" MaxLength="15" runat="server"></asp:TextBox></td>
                                           <td align="left" valign="top"><asp:TextBox ID="txtAdd_SA_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top" colspan="2">
                                                <asp:DropDownList ID="cboAdd_Prem_Rate_Code" Width="200px" runat="server" AutoPostBack="true" OnTextChanged="DoProc_Premium_Code_Change">
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtAdd_Prem_Rate_Code" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                                &nbsp;<asp:TextBox ID="txtAdd_Prem_Rate_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtAdd_Prem_Rate" Enabled="false" Width="80px" runat="server"></asp:TextBox>
                                                &nbsp;<asp:TextBox ID="txtAdd_Prem_Rate_Per" Visible="true" Enabled="false" MaxLength="5" Width="60px" ToolTip="" runat="server"></asp:TextBox>
                                            </td>

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
                                                <asp:GridView id="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
                                                    DataKeyNames="TBIL_POL_ADD_REC_ID" HorizontalAlign="Left"
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
                            
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ADD_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ADD_FILE_NO" HeaderText="File Number" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ADD_PRDCT_CD" HeaderText="Product" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ADD_COVER_CD" HeaderText="Cover" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ADD_PREM_AMT" HeaderText="Fixed Amount" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ADD_PREM_FX_RT" HeaderText="Fixed Rate" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ADD_SA_LC" HeaderText="Sum Assured" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_POL_ADD_RATE" HeaderText="Prem Rate" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
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
