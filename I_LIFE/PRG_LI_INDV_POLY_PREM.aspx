<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_POLY_PREM.aspx.vb" Inherits="I_LIFE_PRG_LI_INDV_POLY_PREM" %>

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
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" Enabled="false" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
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
                    <td nowrap class="myheader">Premium Information</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                                            <tr>
                                                <td align="left" colspan="4" valign="top">
                                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                    <asp:Label ID="lblOriginator" ForeColor="Red" Font-Size="Small" runat="server" CssClass="RecordOriginator"></asp:Label>
                                                </td>
                                            </tr>

                            <tr>
                                                <td nowrap align="left" valign="top"><asp:CheckBox ID="chkFileNum" Visible="false" AutoPostBack="false" Text="" runat="server" />
                                                    &nbsp;<asp:Label ID="lblFileNum" Enabled="true" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNum" Enabled="false" Width="200px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Button ID="cmdFileNum" Visible="false" Enabled="false" Text="Get Record" runat="server" />
                                                    &nbsp;<asp:TextBox ID="txtRecNo" Visible="false" Enabled="false" MaxLength="18" Width="40" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="true" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                            </tr>
                        
                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblQuote_Num" Enabled="true" Text="Proposal No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="lblPlan_Num" Enabled="true" Text="Plan/Cover Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:TextBox ID="txtPlan_Num" Visible="true" Enabled="false" MaxLength="10" Width="80" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtCover_Num" Visible="true" Enabled="false" MaxLength="10" Width="80px" runat="server"></asp:TextBox></td>
                                                
                            </tr>


                                    <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblDOB" Enabled="true" Text="Date of Birth:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtDOB" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblDOB_Format" Enabled="false" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblDOB_ANB" Enabled="true" Text="Age (ANB):" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtDOB_ANB" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>


                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Risk Coverage/Payment Information</td>
                                    </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblPrem_Period_Yr" Text="Duration/Term:" runat="server"></asp:Label></td>
                                                <td align="left" colspan="1" valign="top"><asp:TextBox ID="txtPrem_Period_Yr" runat="server"></asp:TextBox>
                                                </td>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblProduct" Enabled="true" Text="Product Category/Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:TextBox ID="txtProductClass" Visible="true" Enabled="false" MaxLength="10" Width="80" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="true" Enabled="false" MaxLength="10" Width="80px" runat="server"></asp:TextBox></td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblPrem_Start_Date" Text="Commencement Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Start_Date" MaxLength="10" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblPrem_Start_Date_Format" Visible="true" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:Label ID="lblPrem_End_Date" Enabled="true" Text="Expiry Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_End_Date" MaxLength="10" Enabled="false" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblPrem_End_Date_Format" Visible="true" Enabled="false" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_MOP_Type" Text="Mode of Payment:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboPrem_MOP_Type" Width="150px" AutoPostBack="true" runat="server" OnTextChanged="DoProc_MOP_Change">
                                        <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                        <asp:ListItem Value="D">Daily</asp:ListItem>
                                        <asp:ListItem Value="M">Monthly</asp:ListItem>
                                        <asp:ListItem Value="W">Weekly</asp:ListItem>
                                        <asp:ListItem Value="Q">Quarterly</asp:ListItem>
                                        <asp:ListItem Value="H">Half Yearly</asp:ListItem>
                                        <asp:ListItem Value="A">Annual</asp:ListItem>
                                        <asp:ListItem Value="S">Single</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_MOP_Type" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_MOP_TypeName" Visible="false" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox></td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_MOP_Rate" Enabled="true" Text="Mod of Payment Rate:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_MOP_Rate" Enabled="false" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_MOP_Per" Visible="false" Enabled="false" MaxLength="5" Width="40px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            
                            <tr>
                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Sum Assured/Premium Information</td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_SA_Currency" Text="Sum Assured Currency:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboPrem_SA_Currency" Width="150px" runat="server" AutoPostBack="true" OnTextChanged="DoProc_Currency_Code_Change">
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_SA_CurrencyCode" Visible="false" MaxLength="5" Width="40px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtPrem_SA_CurrencyName" Visible="false" Width="40px" runat="server"></asp:TextBox></td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Exchange_Rate" Enabled="false" Text="Exchange Rate:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Exchange_Rate" Enabled="false" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Life_Cover" Text="Any Life Cover:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboPrem_Life_Cover" Width="80px" runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Life_CoverNum" Visible="false" Enabled="false" MaxLength="1" Width="20px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Life_CoverName" Visible="false" Enabled="false" MaxLength="1" Width="20px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Enrollee_Num" Text="No of Enrollee:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Enrollee_Num"  MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblPrem_Enrollee_Num_Rmks" ForeColor="Red" Text="Applicable to Funeral Product" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_Allocation_YN" Text="Is Allocation Applicable:" ToolTip="Is Allocation applicable to this product..." runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboPrem_Allocation_YN" Width="80px" ToolTip="Is Allocation applicable to this product..." runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Allocation_YN" Visible="false" Enabled="false" MaxLength="1" Width="20px"  runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Allocation_YN_Name" Visible="false" Enabled="false" MaxLength="1" Width="20px"  runat="server"></asp:TextBox>
                                </td>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_Bonus_YN" Text="Is Bonus Applicable:" ToolTip="Is Allocation applicable to this product..." runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboPrem_Bonus_YN" Width="80px" ToolTip="Is Bonus applicable to this product..." runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Bonus_YN" Visible="true" Enabled="false" MaxLength="1" Width="20px"  runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Bonus_YN_Name" Visible="false" Enabled="false" MaxLength="1" Width="20px"  runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Rate_Type" Text="Premium Rate Type:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboPrem_Rate_Type" AutoPostBack="true" Width="100px" runat="server" OnTextChanged="DoProc_Rate_Type_Change">
                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                        <asp:ListItem Value="F">Fixed Rate</asp:ListItem>
                                        <asp:ListItem Value="N">No Rate</asp:ListItem>
                                        <asp:ListItem Value="T">Table Rate</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_TypeNum" Visible="false" Enabled="false" MaxLength="1" Width="20px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_TypeName" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" colspan="2"><asp:Label ID="lblPrem_Fixed_Rate" Enabled="false" Text="Fixed Rate:" runat="server"></asp:Label>
                                    &nbsp;&nbsp;<asp:TextBox ID="txtPrem_Fixed_Rate" Enabled="false" MaxLength="8" Width="60px"  runat="server"></asp:TextBox>
                                    &nbsp;&nbsp;<asp:Label ID="lblPrem_Fixed_Rate_Per" Enabled="false" Text="Fixed Rate Per:" runat="server"></asp:Label>
                                    &nbsp;
                                    <asp:DropDownList ID="cboPrem_Fixed_Rate_Per" Enabled="false" Width="100px" runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                        <asp:ListItem Value="100">Per 100</asp:ListItem>
                                        <asp:ListItem Value="1000">Per 1000</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Fixed_Rate_PerNum" Visible="false" Enabled="false" MaxLength="5" Width="20px" ToolTip="" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Fixed_Rate_PerName" Visible="false" Enabled="false" Width="20px" ToolTip="" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblPrem_Rate_Code" Enabled="false" Text="Premium Rate Code:" runat="server"></asp:Label></td> 
                                <td align="left" valign="top" colspan="3">
                                    <asp:DropDownList ID="cboPrem_Rate_Code" Enabled="false" Width="450px" runat="server" AutoPostBack="true" OnTextChanged="DoProc_Premium_Code_Change">
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_Code" Visible="true" Enabled="false" Width="40px" runat="server">
                                    &nbsp;</asp:TextBox>&nbsp;<asp:TextBox ID="txtPrem_Rate_CodeName" Visible="false" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblPrem_Rate" Enabled="false" Text="Premium Rate:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:TextBox ID="txtPrem_Rate" Enabled="false" Width="90px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:Label ID="lblPrem_Rate_Per" Enabled="false" Text="Rate Per:" runat="server"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_Per" Visible="true" Enabled="false" MaxLength="5" Width="60px" ToolTip="" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" >
                                    <asp:Label ID="lblDepositAmount" Enabled="False" Text="Deposit Amount:" 
                                        runat="server"></asp:Label>
                                    </td>
                                <td align="left" valign="top" ><asp:TextBox ID="txtDepositAmount" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" colspan="4"><hr /></td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_Rate_Applied_On" Text="Applied Rate on:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:RadioButton ID="optPrem_Applied_SA" Text="Sum Assured" GroupName="optSaving_Type" runat="server" />
                                    &nbsp;<asp:RadioButton ID="optPrem_Applied_Prem" Text="Premium:" GroupName="optSaving_Type" runat="server" />
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_Applied_On" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox>
                                </td>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_Is_SA_From_Prem" Enabled="false" Text="Is SA Calc. From Prem:" ToolTip="Is Sum Assured(SA) Calculation from Premium..." runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:DropDownList ID="cboPrem_Is_SA_From_Prem" Enabled="false" Width="80px" ToolTip="Is Sum Assured(SA) Calculation from Premium..." runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                        <asp:ListItem Value="N">NO</asp:ListItem>
                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Is_SA_From_PremNum" Visible="false" Enabled="false" MaxLength="1" Width="20px"  runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Is_SA_From_PremName" Visible="false" Enabled="false" MaxLength="1" Width="20px"  runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Ann_Contrib_LC" ToolTip="LC=Local Currency" Text="Annual Contribution LC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Ann_Contrib_LC" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Ann_Contrib_FC" ToolTip="FC=Foreign Currency" Text="Annual Contribution FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Ann_Contrib_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Mth_Contrib_LC" ToolTip="LC=Local Currency" Text="Monthly Contribution LC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Mth_Contrib_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Mth_Contrib_FC" ToolTip="FC=Foreign Currency" Text="Monthly Contribution FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Mth_Contrib_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top" colspan="4"><hr /></td>
                            </tr>
                            
                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_SA_LC" ToolTip="LC=Local Currency" Text="Sum Assured LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_SA_LC" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_SA_FC" ToolTip="FC=Foreign Currency" Text="Sum Assured FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_SA_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Life_Cover_SA_LC" ToolTip="LC=Local Currency" Text="Life Cover SA LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Life_Cover_SA_LC" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Life_Cover_SA_FC" ToolTip="FC=Foreign Currency" Text="Life Cover SA FC:" runat="server"></asp:Label></td>
                                <td nowrap align="left" valign="top"><asp:TextBox ID="txtPrem_Life_Cover_SA_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_Free_Cover_Lmt_LC" Enabled="false" ToolTip="LC=Local Currency" Text="Free Cover Limit LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Free_Cover_Lmt_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_Free_Cover_Lmt_FC" Enabled="false" ToolTip="FC=Foreign Currency" Text="Free Cover Limit FC:" runat="server"></asp:Label></td>
                                <td nowrap align="left" valign="top"><asp:TextBox ID="txtPrem_Free_Cover_Lmt_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_Free_LiveCover_Lmt_LC" Enabled="false" ToolTip="LC=Local Currency" Text="Free Life Cover Limit LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Free_LiveCover_Lmt_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblPrem_Free_LiveCover_Lmt_FC" Enabled="false" ToolTip="FC=Foreign Currency" Text="Free Life Cover Limit FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Free_LiveCover_Lmt_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top" colspan="4"><hr /></td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_School_Term" Enabled="false" Text="School Term" runat="server"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="cboPrem_School_Term" Enabled="false" Width="120px" ToolTip="Select School Term..." runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                        <asp:ListItem Value="P">Primary</asp:ListItem>
                                        <asp:ListItem Value="S">Secondary</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_School_Term" Enabled="false" Visible="false" MaxLength="3" Width="20px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_School_Term_Name" Enabled="false" Visible="false" Width="20px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Sch_Fee_Prd" Enabled="false" Text="School Fee Period" runat="server"></asp:Label></td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtPrem_Sch_Fee_Prd" Enabled="false" Width="80px" runat="server"></asp:TextBox>
                                </td>
                            </tr>


                            <tr>
                                <td align="left" valign="top" colspan="4"><hr /></td>
                            </tr>

                            <tr style="display: none;">
                                <td align="left" valign="top"><asp:Label ID="lblPrem_No_Instal" Enabled="true" Text="Premium No Install:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtPrem_No_Instal" Enabled="true" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr style="display: none;">
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Discount_Rate" Text="Discount Percent %" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Discount_Rate" MaxLength="7" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Loading_Rate" Text="Loading Percent %:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Loading_Rate" MaxLength="7" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr style="display: none;">
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Payable" Enabled="false" Text="Premium Payable:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtPrem_Payable" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
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
