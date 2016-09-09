<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_POLY_FUNERAL.aspx.vb" Inherits="I_LIFE_PRG_LI_INDV_POLY_FUNERAL" %>

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
                    <td nowrap class="myheader">Funeral Information</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblFileNum" Enabled="false" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNum" Enabled="false" Width="230px" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                            </tr>
                        
                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblQuote_Num" Enabled="false" Text="Proposal No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblRecNo" BorderStyle="Solid" Text="Rec. No:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18"></asp:TextBox>
                                                    &nbsp;No of Enrollee:
                                                    &nbsp;<asp:TextBox ID="txtPrem_Enrollee_Num" Visible="true" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                            </tr>
                                    <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblDOB" Enabled="false" Text="Date of Birth:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtDOB" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblDOB_Format" Enabled="false" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblDOB_ANB" Enabled="false" Text="Age (ANB):" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtDOB_ANB_Prm" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;Rate Type:
                                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_TypeNum" Visible="true" Enabled="false" MaxLength="1" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>

                            <tr>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblProduct" Enabled="false" Text="Product Category/Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtProductClass" Visible="true" Enabled="false" MaxLength="10" Width="80" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="true" Enabled="false" MaxLength="10" Width="80px" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblPrem_Rate_Applied_On" Enabled="false" Text="Applied On:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPrem_Rate_Applied_On" Visible="true" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                    &nbsp;Rate Code:
                                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_Code" Visible="true" Enabled="false" Width="60px" runat="server"></asp:TextBox>
                                                </td>                                                
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Ann_Contrib_LC_Prm" Enabled="false" ToolTip="LC=Local Currency" Text="Annual Contribution LC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtPrem_Ann_Contrib_LC_Prm" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Funeral Details</td>
                                    </tr>

                            <tr>
                                <td colspan="4">
                                    <table align="center" border="1" style="width: 100%">
                            <tr>
                                                <td nowrap align="right" valign="top"><asp:Label ID="lblBenef_Cover_ID" Enabled="false" Text="Member/Cover ID:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="4">
                                                    <asp:TextBox ID="txtBenef_Cover_ID" Visible="true" Enabled="false" MaxLength="10" Width="100" runat="server"></asp:TextBox>
                                                </td>
                            </tr>
                                        <tr class="tr_frame">
                                            <td align="left" valign="top"><asp:Label ID="lblBenef_SN" Text="S/No" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenef_Type" Text="Type of Person" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenef_Category" Text="Relation Type" runat="server"></asp:Label></td>
                                            <td align="left" valign="top" colspan="1"><asp:Label ID="lblBenef_Name" Text="Enrollee Name" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenef_Percentage" Text="Age" ToolTip="" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top"><asp:TextBox ID="txtBenef_SN" Enabled="false" Width="100px" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboBenef_Type" Width="100px" runat="server">
                                                    <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                    <asp:ListItem Value="1">Self</asp:ListItem>
                                                    <asp:ListItem Value="2">Spouse</asp:ListItem>
                                                    <asp:ListItem Value="3">Parent</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtBenef_Type" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                                &nbsp;<asp:TextBox ID="txtBenef_TypeName" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboBenef_Category" Width="120px" runat="server">
                                                    <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                    <asp:ListItem Value="1">Self</asp:ListItem>
                                                    <asp:ListItem Value="2">Spouse</asp:ListItem>
                                                    <asp:ListItem Value="3">Mother</asp:ListItem>
                                                    <asp:ListItem Value="4">Father</asp:ListItem>
                                                    <asp:ListItem Value="5">Mother-in-Law</asp:ListItem>
                                                    <asp:ListItem Value="6">Father-in-Law</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtBenef_Category" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBenef_CategoryName" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtBenef_Name" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtDOB_ANB" Enabled="true" Width="40px" runat="server"></asp:TextBox>
                                                &nbsp;<asp:TextBox ID="txtBenef_Percentage" Visible="false" MaxLength="6" Width="40px" ToolTip="" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr class="tr_frame">
                                            <td align="left" valign="top"><asp:Label ID="lblBenef_Relationship" Text="Gender" ToolTip="" runat="server"></asp:Label></td>
                                            <td align="left" valign="top" colspan="2"><asp:Label ID="lblBenef_Address" Text="Enrollee Address" ToolTip="" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblPrem_Ann_Contrib_LC" Text="Annual Contribution" ToolTip="" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblPrem_SA_LC" Text="Sum Assured" ToolTip="" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top"><asp:DropDownList ID="cboBenef_Relationship" Width="120px" runat="server"></asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtBenef_Relationship" Visible="false" Width="20px" ToolTip="" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtBenef_RelationshipName" Visible="false" Width="20px" ToolTip="" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top" colspan="2"><asp:TextBox ID="txtBenef_Address" MaxLength="95" ToolTip="" runat="server" Width="250px"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtPrem_Ann_Contrib_LC" MaxLength="15" ToolTip="" runat="server" Width="140px"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtPrem_SA_LC" MaxLength="15" Width="140px" ToolTip="" runat="server"></asp:TextBox>
                                                &nbsp;
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
                                                    DataKeyNames="TBIL_FUN_REC_ID" HorizontalAlign="Left"
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
                    
                                                    <PagerSettings  FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" 
                                                        PreviousPageText="Previous">
                                                    </PagerSettings>
                        
                                                    <Columns>
                                                        <asp:TemplateField>
        			                                        <ItemTemplate>
        						                                <asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                
                                                        <asp:CommandField ShowSelectButton="True" />
                            
                                                        <asp:BoundField readonly="true" DataField="TBIL_FUN_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_FUN_COVER_ID" HeaderText="Cover ID" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_FUN_NAME_COVERD" HeaderText="Enrollee Name" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_FUN_AGE" HeaderText="Enrollee Age" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_FUN_PREM_RATE" HeaderText="Prem Rate" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_FUN_ANN_CONTRIB" HeaderText="Annual Contrib" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                        <asp:BoundField readonly="true" DataField="TBIL_FUN_SA" HeaderText="Sum Assured" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />
                                                    </Columns>
   
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <!-- hidden fields -->
                            <tr>
                                <td align="left" colspan="4" style="display: none;">
                                            &nbsp;<asp:TextBox ID="txtPrem_Rate" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtPrem_Rate_Per" Visible="true" Enabled="false" MaxLength="5" Width="40px" ToolTip="" runat="server"></asp:TextBox>                                            
                                            &nbsp;<asp:TextBox ID="txtPrem_MOP_Type" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtPrem_MOP_Per" Visible="false" Enabled="false" MaxLength="5" Width="40px" runat="server"></asp:TextBox>

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
