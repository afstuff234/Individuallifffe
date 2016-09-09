<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_POLY_PREM_CALC.aspx.vb" Inherits="I_LIFE_PRG_LI_INDV_POLY_PREM_CALC" %>

<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Individual Life Premium Calculation</title>
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
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;"><%=STRMENU_TITLE%></td>
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
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" Visible="false" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" Visible="false" CssClass="cmd_butt" Enabled="false"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" Visible="false" CssClass="cmd_butt" Enabled="False" runat="server" text="Print"></asp:button>
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
                    <td nowrap class="myheader">Premium Calculation Information</td>
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
                                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Proposal/Product Info.</td>
                                            </tr>

                            <tr>
                                                <td nowrap align="right" valign="top"><asp:CheckBox ID="chkFileNum" Enabled="false" AutoPostBack="false" Text="" runat="server" />
                                                    &nbsp;<asp:Label ID="lblFileNum" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNum" Enabled="false" Width="230px" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Button ID="cmdFileNum" Visible="false" Enabled="false" Text="Get Record" runat="server" />
                                                    &nbsp;<asp:TextBox ID="txtRecNo" Visible="false" Enabled="false" MaxLength="18" Width="40" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" 
                                                        Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                            </tr>
                        
                            <tr>
                                                <td nowrap align="right" valign="top"><asp:Label ID="lblQuote_Num" Text="Proposal No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top"><asp:Label ID="lblTrans_Date" Enabled="false" Text="Proposal Date:" 
                                                        runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtTrans_Date" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>                                                    
                                                    &nbsp;<asp:ImageButton ID="butCal" Enabled="false" Visible="false"  runat="server" 
                                                        OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                                        ImageUrl="~/Images/Calendar.bmp" Height="17px" Width="18px" />
                                                    &nbsp;<asp:Label ID="lblTrans_Date_Format" Visible="false" Enabled="false" Text="dd/mm/yyyy" runat="server"></asp:Label></td>
                            </tr>

                                    <tr>
                                                <td align="right" valign="top"><asp:Label ID="lblDOB" Enabled="false" Text="Date of Birth:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtDOB" Enabled="false" MaxLength="10" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblDOB_Format" Enabled="false" Text="" runat="server"></asp:Label></td>
                                                <td align="right" valign="top" colspan="1"><asp:Label ID="lblDOB_ANB" Enabled="false" Text="Age (ANB):" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtDOB_ANB" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>
                            

                                            <tr>
                                                <td align="right" valign="top"><asp:Label ID="lblProductClass" Text="Product Category:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboProductClass" Enabled="false" CssClass="selProductX" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProductClassName" Visible="false" Enabled="false" MaxLength="10" Width="20" runat="server"></asp:TextBox></td>
                                                <td align="right" valign="top"><asp:Label ID="lblProduct_Num" Text="Product Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList id="cboProduct" Enabled="false" CssClass="selProductX" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Visible="false" Enabled="false" MaxLength="10" Width="20px" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtProduct_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="right" valign="top"><asp:Label ID="lblCover_Num" Text="Cover Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboCover_Name" Enabled="false" CssClass="selProductX" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtCover_Num" Visible="false" Enabled="true" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtCover_Name" Visible="false" Enabled="false" Width="20px" runat="server"></asp:TextBox></td>                                            
                                                <td align="right" valign="top"><asp:Label ID="lblPlan_Num" Text="Plan Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboPlan_Name" Enabled="false" CssClass="selProductX" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtPlan_Num" Visible="false" Enabled="true" MaxLength="10" Width="20" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="txtPlan_Name" Visible="false" Enabled="false" Width="20" runat="server"></asp:TextBox></td>                                            
                                            </tr>

                                    
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Premium Info</td>
                                    </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_SA_LC" ToolTip="LC=Local Currency" Text="Sum Assured LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_SA_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_SA_FC" ToolTip="FC=Foreign Currency" Text="Sum Assured FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_SA_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_Life_Cover_SA_LC" ToolTip="LC=Local Currency" Text="Life Cover SA LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Life_Cover_SA_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_Life_Cover_SA_FC" ToolTip="FC=Foreign Currency" Text="Life Cover SA FC:" runat="server"></asp:Label></td>
                                <td nowrap align="left" valign="top"><asp:TextBox ID="txtCalc_Life_Cover_SA_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td nowrap align="right" valign="top"><asp:Label ID="lblCalc_Ann_Basic_Prem_LC" ToolTip="LC=Local Currency" Text="Annual Basic Premium LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Ann_Basic_Prem_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td nowrap align="right" valign="top"><asp:Label ID="lblCalc_Ann_Basic_Prem_FC" ToolTip="FC=Foreign Currency" Text="Annual Basic Premium FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Ann_Basic_Prem_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td nowrap align="right" valign="top"><asp:Label ID="lblCalc_MOP_Basic_LC" ToolTip="LC=Local Currency" Text="MOP Basic Premium LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_MOP_Basic_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td nowrap align="right" valign="top"><asp:Label ID="lblCalc_MOP_Basic_FC" ToolTip="FC=Foreign Currency" Text="MOP Basic Premium FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_MOP_Basic_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td nowrap align="right" valign="top"><asp:Label ID="lblCalc_Add_Prem_LC" ToolTip="LC=Local Currency" Text="Addition Cover Prem LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Add_Prem_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td nowrap align="right" valign="top"><asp:Label ID="lblCalc_Add_Prem_FC" ToolTip="FC=Foreign Currency" Text="Additional Cover Prem FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Add_Prem_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="Label1" ToolTip="LC=Local Currency" Text="Premium Loading LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Prem_Loading_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="Label2" ToolTip="FC=Foreign Currency" Text="Premium Loading FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Prem_Loading_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="Label3" ToolTip="LC=Local Currency" Text="Premium Discount LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Prem_Disc_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="Label4" ToolTip="FC=Foreign Currency" Text="Premium Discount FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Prem_Disc_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>


                            <tr>
                                <td align="right" valign="top"><asp:Label ID="Label5" ToolTip="LC=Local Currency" Text="Additional Charges LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Add_Charges_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="Label6" ToolTip="FC=Foreign Currency" Text="Additional Charges FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Add_Charges_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="Label7" ToolTip="LC=Local Currency" Text="MOP Factor LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_MOP_Factor_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="Label8" ToolTip="FC=Foreign Currency" Text="MOP Factor FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_MOP_Factor_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="Label9" ToolTip="LC=Local Currency" Text="MOP Premium LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_MOP_Prem_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="Label10" ToolTip="FC=Foreign Currency" Text="MOP Premium FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_MOP_Prem_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top" colspan="4"><hr /></td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_Total_Prem_LC" Text="Total Premium LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Total_Prem_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_Total_Prem_FC" Text="Total Premium FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Total_Prem_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_Net_Prem_LC" Text="Net Premium LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Net_Prem_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_Net_Prem_FC" Text="Net Premium FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Net_Prem_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td norwap align="right" valign="top"><asp:Label ID="lblCalc_Ann_Prem_LC" ToolTip="LC=Local Currency" Text="Annual Premium LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Ann_Prem_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_Ann_Prem_FC" ToolTip="FC=Foreign Currency" Text="Annual Premium FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Ann_Prem_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td norwap align="right" valign="top"><asp:Label ID="lblCalc_Install_Prem_LC" ToolTip="LC=Local Currency" Text="Instalmental Prem LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Install_Prem_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="Label16" ToolTip="FC=Foreign Currency" Text="Instalmental Prem FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Install_Prem_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td norwap align="right" valign="top"><asp:Label ID="lblCalc_Ann_Contrib_LC" ToolTip="LC=Local Currency" Text="Annual Contribution LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Ann_Contrib_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_Ann_Contrib_FC" ToolTip="FC=Foreign Currency" Text="Annual Contribution FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_Ann_Contrib_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td norwap align="right" valign="top"><asp:Label ID="lblCalc_MOP_Contrib_LC" ToolTip="LC=Local Currency" Text="MOP Contribution LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_MOP_Contrib_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_MOP_Contrib_FC" ToolTip="FC=Foreign Currency" Text="MOP Contribution FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_MOP_Contrib_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td norwap align="right" valign="top"><asp:Label ID="lblCalc_First_Prem_LC" ToolTip="LC=Local Currency" Text="First Premium LC" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_First_Prem_LC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblCalc_First_Prem_FC" ToolTip="FC=Foreign Currency" Text="First Premium FC:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtCalc_First_Prem_FC" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Rate Info</td>
                                    </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblPrem_Period_Yr" Text="Period Cover (Yrs):" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Period_Yr" Enabled="false" Width="60px" runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblPrem_Life_Cover" Text="Any Life Cover:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Life_CoverNum" Visible="true" Enabled="false" MaxLength="1" Width="60px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblPrem_Rate_Type" Text="Premium Rate Type:" runat="server"></asp:Label></td>
                                <td align="left" colspan="3" valign="top">
                                    <asp:DropDownList ID="cboPrem_Rate_Type" Enabled="false" AutoPostBack="false" Width="100px" runat="server">
                                        <asp:ListItem Selected="True" Value="*">(Select)</asp:ListItem>
                                        <asp:ListItem Value="F">Fixed Rate</asp:ListItem>
                                        <asp:ListItem Value="N">No Rate</asp:ListItem>
                                        <asp:ListItem Value="T">Table Rate</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_TypeNum" Visible="true" Enabled="false" MaxLength="1" Width="60px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_TypeName" Visible="false" Enabled="false" Width="100px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblPrem_Rate_Code" Text="Premium Rate Code:" runat="server"></asp:Label></td>
                                <td align="left" colspan="3" valign="top">
                                    <asp:DropDownList ID="cboPrem_Rate_Code" Enabled="false" Width="400px" runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_Code" Visible="true" Enabled="false" Width="60px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblPrem_Rate" Text="Premium Rate:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Rate" Enabled="false" Width="60px" runat="server"></asp:TextBox></td>
                                <td align="right" valign="top"><asp:Label ID="lblPrem_Rate_Per" Text="Rate Per:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Rate_PerNum" Visible="true" Enabled="false" MaxLength="5" Width="60px" ToolTip="" runat="server"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td align="right" valign="top"><asp:Label ID="lblPrem_Fixed_Rate" Text="Fixed Rate:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPrem_Fixed_Rate" Enabled="false" MaxLength="8" Width="60px"  runat="server"></asp:TextBox>
                                </td>
                                <td align="right" valign="top"><asp:Label ID="lblPrem_Fixed_Rate_Per" Text="Fixed Rate Per:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Fixed_Rate_PerNum" Visible="true" Enabled="false" MaxLength="5" Width="60px" ToolTip="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4"><hr /></td>
                            </tr>
                            
                            <tr style="display:none;" >
                                <td nowrap align="left" colspan="4" valign="top">
                                    &nbsp;<asp:TextBox ID="txtPrem_MOP_Type" Visible="true" Enabled="false" MaxLength="4" Width="60px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_MOP_Rate" Enabled="false" Width="60px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_MOP_Per" Enabled="false" Width="60px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Is_SA_From_PremNum" Visible="false" Enabled="false" MaxLength="1" Width="60px"  runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Rate_Applied_On" Visible="false" Enabled="false" Width="60px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Free_LiveCover_Lmt_LC" Enabled="false" MaxLength="15" Width="60px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Free_LiveCover_Lmt_FC" Enabled="false" MaxLength="15" Width="60px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_School_Term" Enabled="false" Visible="false" MaxLength="3" Width="20px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Sch_Fee_Prd" Enabled="false" Width="80px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Enrollee_Num" Visible="true" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="txtPrem_Ann_Contrib_LC_Prm" Enabled="false" MaxLength="15" runat="server"></asp:TextBox>
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
