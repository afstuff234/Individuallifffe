<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INV_PLUS.aspx.vb" Inherits="I_LIFE_PRG_LI_INV_PLUS" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

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

    <div>
    
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    
    </div>

    <!-- start banner -->
    <div id="div_banner" align="center">
    </div>
    
    <!-- start header -->
    <div id="div_header" align="center">
        <table id="tbl_header" align="center">
            <tr>
                <td align="left" valign="top" class="myMenu_Title_02">
                    <table border="0" width="100%">
                        <tr>
                            <td align="left" valign="top"><%=STRMENU_TITLE%></td>
                            <td align="center" valign="top">
                                &nbsp;<a href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=IL_QUOTE')">Previous Menu</a>&nbsp;
                            </td>    
                            <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>
    	                        &nbsp;&nbsp;Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;&nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;&nbsp;<asp:DropDownList ID="cboSearch" runat="server" Height="26px" 
                                    Width="150px"></asp:DropDownList>
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
            <caption>Proposal Header</caption>
                <tr>
                    <td align="center" valign="top" class="td_menu_hedaer">
                        <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
                                            <tr>
                                                <td align="left" colspan="4" valign="top">
                                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblFileNum" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNum" Enabled="true" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" Enabled="false" runat="server"></asp:TextBox></td>
                            </tr>
                        
                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblQuote_Num" Text="Quotation No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtQuote_Num" Enabled="false" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="Label1" Text="Transaction Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtTrans_Date" EnableViewState="true" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="butCal" Visible="true"  runat="server" OnClientClick="OpenModal_Cal(this.form,'txtTrans_Date','txtTrans_Date')" 
                                                        ImageUrl="~/Images/Calendar.bmp" Height="17" />
                                                </td>
                            </tr>

                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblProductClass" Text="Product Category:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="2">                                                    
                                                    <asp:DropDownList ID="cboProductClass" AutoPostBack="true" CssClass="selProduct" runat="server" OnTextChanged="DoProc_ProductClass_Change"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProductClass" runat="server" MaxLength="5" Width="80"></asp:TextBox>
                                                    &nbsp;
                                                </td>                                            
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblProduct_Num" Text="Product Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="2">                                                    
                                                    <asp:DropDownList id="cboProduct" AutoPostBack="true" CssClass="selProduct" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtProduct_Num" Enabled="true" EnableViewState="true" runat="server" MaxLength="10" Width="80"></asp:TextBox>
                                                    &nbsp;
                                                </td>
                                            </tr>


                                            <tr>
                                                <td align="left" colspan="4" valign="top">&nbsp;</td>
                                            </tr>    
                        </table>
                    </td>                                                                                    
                </tr>
        </table>
        
        <asp:TabContainer ID="TabContainer1" runat="server" Width="1000px" 
            ScrollBars="Both" ActiveTabIndex="0">
            
            <asp:TabPanel ID="TabPanel1" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Proposal Tab">
                <ContentTemplate>
<table class="tbl_cont" align="center"><caption>Proposal Details</caption><tr><td align="center" valign="top" class="td_menu"><table align="center" border="0" cellspacing="0" class="tbl_menu_new"><tr><td align="center" colspan="4" valign="top">&#160;&#160;<asp:button 
        id="cmdPrev" CssClass="cmd_butt"  runat="server" text="«..Previous" 
        Enabled="False"></asp:button>&nbsp;&nbsp;<asp:Button ID="cmdNew_ASP" 
        CssClass="cmd_butt" Text="New Data" runat="server" 
        OnClientClick="JSNew_ASP();" />&#160;&#160;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data" OnClientClick="JSSave_ASP();"></asp:button>&nbsp;&nbsp;<asp:button 
        id="cmdDelete_ASP" CssClass="cmd_butt" runat="server" text="Delete Data" 
        OnClientClick="JSDelete_ASP();"></asp:button>&#160;&#160;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" text="Print"></asp:button>&nbsp;&nbsp;<asp:Button 
        ID="cmdNext" CssClass="cmd_butt" Enabled="False" Text="Next..»" 
        runat="server" /></td></tr><tr><td align="left" colspan="4" valign="top"><hr /></td></tr><tr><td align="left" colspan="4" valign="top" class="myMenu_Title">Business Source Info</td></tr><tr><td align="left" valign="top"><asp:Label ID="lblBraNum" Text="Branch:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="3"><asp:DropDownList ID="cboBranch" Width="220px" runat="server" OnTextChanged="DoProc_Branch_Change"></asp:DropDownList>&#160;<asp:TextBox 
        ID="txtBraNum" MaxLength="4" Enabled="False" Width="40px" runat="server"></asp:TextBox>&nbsp;<input type="button" id="cmdBranch_Setup" name="cmdBranch_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CODES.aspx?optid=003&optd=Branch&popup=YES')" />&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdBranch_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Branch_Refresh" /></td></tr><tr><td align="left" valign="top"><asp:Label ID="lblDeptNum" Text="Department:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="3"><asp:DropDownList ID="cboDepartment" Width="220px" runat="server" OnTextChanged="DoProc_Dept_Change"></asp:DropDownList>&#160;<asp:TextBox 
            ID="txtDeptNum" Enabled="False" runat="server" Width="40px"></asp:TextBox>&nbsp;<input type="button" id="cmdDept_Setup" name="cmdDept_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CODES.aspx?optid=005&optd=Department&popup=YES')" />&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdDept_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Dept_Refresh" /></td></tr><tr><td align="left" colspan="4" valign="top" class="myMenu_Title">Assured/Customer Info</td></tr><tr><td align="left" valign="top"><asp:Label ID="lblAssuredNum" Text="Assured Code:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAssured_Num" MaxLength="30" runat="server"></asp:TextBox>&#160;<asp:Label ID="lblAssuredName" Text="Name:" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAssured_Name" Enabled="False" runat="server" Width="350px"></asp:TextBox>&nbsp;<input type="button" id="cmdAssured_Setup" name="cmdAssured_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CUST_DTL.aspx?optid=001&optd=Customer_Details&popup=YES')" />&nbsp;</td></tr><tr><td align="left" valign="top"><asp:Label ID="lblAssured_Search" Text="Search for Assured:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAssured_Search" runat="server"></asp:TextBox>&#160;<asp:Button ID="cmdAssured_Search" Text="Search..." runat="server" OnClick="DoProc_Assured_Search" />&nbsp;<asp:DropDownList ID="cboAssured" AutoPostBack="True" Width="350px" 
                                                runat="server" OnTextChanged="DoProc_Assured_Change"></asp:DropDownList></td></tr><tr><td align="left" valign="top"><asp:Label ID="lblGender" Text="Gender:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="1"><asp:DropDownList ID="cboGender" Width="150px" runat="server"></asp:DropDownList>&#160;<asp:TextBox 
            ID="txtGender" Enabled="False" MaxLength="4" Width="40px" runat="server"></asp:TextBox></td><td align="left" valign="top" colspan="1"><asp:Label ID="lblMaritalStatus" Text="Marital Status:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="1"><asp:DropDownList ID="cboMaritalStatus" Width="150px" runat="server"></asp:DropDownList>&#160;<asp:TextBox 
            ID="txtMaritalStatus" Enabled="False" Width="40px" runat="server"></asp:TextBox></td></tr><tr><td align="left" valign="top"><asp:Label ID="lblDOB" Text="Date of Birth:" runat="server"></asp:Label></td><td align="left" valign="top"><asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>&#160;dd/MM/yyyy </td><td align="left" valign="top"><asp:Label 
            ID="lblDOB_ANB" Enabled="False" Text="Age (ANB):" runat="server"></asp:Label></td><td align="left" valign="top"><asp:TextBox 
                ID="txtDOB_ANB" Enabled="False" Width="40px" runat="server"></asp:TextBox></td></tr><tr><td align="left" valign="top"><asp:Label ID="lblReligion" Text="Religion/Belief:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="1"><asp:DropDownList ID="cboReligion" Width="150px" runat="server"></asp:DropDownList>&#160;<asp:TextBox 
            ID="txtReligion" Enabled="False" MaxLength="10" Width="40px" runat="server"></asp:TextBox></td><td align="left" valign="top" colspan="1"><asp:Label ID="lblHeight" Text="Height:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="1"><asp:TextBox 
            ID="txtHeight" MaxLength="3" Width="40px" runat="server"></asp:TextBox>&#160;<asp:Label ID="lblWeight" Text="Weight:" runat="server"></asp:Label>&nbsp;<asp:TextBox 
            ID="txtWeight" MaxLength="3" Width="40px" runat="server"></asp:TextBox></td></tr><tr><td 
            align="left" class="myMenu_Title" colspan="4" valign="top">Marketer/Agent/Broker Info</td></tr><tr><td 
            align="left" valign="top"><asp:Label ID="lblAgcyNunm" runat="server" 
            Text="Marketer Code:"></asp:Label></td><td align="left" colspan="3" 
            valign="top"><asp:TextBox ID="txtAgcyNum" runat="server" MaxLength="30"></asp:TextBox>&#160;<asp:Label 
                ID="lblAgcyName" runat="server" Text="Name:"></asp:Label>&#160;&#160;&#160;<asp:TextBox 
                ID="txtAgcyName" runat="server" Enabled="False" Width="350px"></asp:TextBox>&#160;<input 
                id="cmdAgcy_Setup" name="cmdAgcy_Setup" 
                onclick="javascript:jsDoPopNew_Full('PRG_LI_MKT_CD.aspx?optid=001&amp;optd=Marketers_Agency&amp;popup=YES')" 
                type="button" value="Setup" /> </td></tr><tr><td align="left" 
            valign="top" nowrap><asp:Label ID="lblAgcy_Search" 
            Text="Search for Marketer:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="3"><asp:TextBox 
                ID="txtAgcy_Search" runat="server"></asp:TextBox>&#160;<asp:Button ID="Button1" 
                runat="server" OnClick="DoProc_Agcy_Search" Text="Search..."></asp:Button>&nbsp;<asp:DropDownList 
                ID="cboAgcy" runat="server" AutoPostBack="True" 
                OnTextChanged="DoProc_Agcy_Change" Width="350px">
            </asp:DropDownList></td></tr><tr><td align="left" valign="top"><asp:Label 
            ID="lblCustNum" Text="Broker Code:" runat="server"></asp:Label></td><td align="left" valign="top" colspan="3"><asp:TextBox 
                ID="txtCustNum" runat="server" MaxLength="30"></asp:TextBox>&#160;<asp:Label 
                ID="lblCustName" runat="server" Text="Name:"></asp:Label>&#160;&#160;&#160;<asp:TextBox 
                ID="txtCustName" runat="server" Enabled="False" Width="350px"></asp:TextBox>&nbsp;<input 
                id="cmdCust_Setup" name="cmdCust_Setup" 
                onclick="javascript:jsDoPopNew_Full('PRG_LI_BRK_DTL.aspx?optid=001&amp;optd=Brokers_Agents_Details&amp;popup=YES')" 
                type="button" value="Setup" />&nbsp;</td></tr><tr><td align="left" 
            valign="top" colspan="4">&#160;</td></tr></table></td></tr></table>
                
                
        </ContentTemplate>
            
</asp:TabPanel>

            <asp:TabPanel ID="TabPanel2" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Proposal Risk Tab">
                <ContentTemplate>
                <table class="tbl_cont" align="center">
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Risk Information</td>
                                    </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblStart_Date" Text="Start Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtStart_Date" runat="server"></asp:TextBox>
                                                    &nbsp;dd/MM/yyyy
                                                </td>
                                                <td align="right" valign="top"><asp:Label ID="lblDuration" Text="Term/Duration:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtDuration" runat="server"></asp:TextBox>
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblEnd_Date" Text="Expiry Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtEnd_Date" runat="server"></asp:TextBox>
                                                    &nbsp;dd/MM/yyyy
                                                </td>
                                                <td align="right" valign="top"><asp:Label ID="lblDOB_ANB_Risk" Enabled="false" Text="Age (ANB):" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtDOB_ANB_Risk" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>

                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblPayment_Mode" Text="Mode of Payment:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboPayment_Mode" Width="150px" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtPayment_Mode" Enabled="false" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                                </td>    
                                                <td align="right" valign="top" colspan="1"><asp:Label ID="lblPayment_Frequency" Text="Frequency of Payment:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:DropDownList ID="cboPayment_Frequency" Width="150px" runat="server"></asp:DropDownList>
                                                    &nbsp;<asp:TextBox ID="txtPayment_Frequency" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                            </tr>

                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Sum Assured/Premium Information</td>
                                    </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblSaving_Type" Text="Rate Applied one:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="3"><asp:RadioButton ID="optSA" Text="Sum Assured:" GroupName="optSaving_Type" runat="server" />
                                                    &nbsp;<asp:RadioButton ID="optPrem" Text="Premium:" GroupName="optSaving_Type" runat="server" />
                                                    &nbsp;<asp:TextBox ID="txtSaving_Type" Enabled="false" Width="40px" runat="server"></asp:TextBox>
                                                </td>
                            </tr>
                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblSum_Insured" Text="Sum Insured:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtSum_Insured" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right" valign="top"><asp:Label ID="lblPremium" Text="Premium:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtPremium" runat="server"></asp:TextBox>
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblPremRate" Enabled="false" Text="Premium Rate:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtPremRate" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right" valign="top"><asp:Label ID="lblFreqencyRate" Enabled="false" Text="Frequency Rate:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="FreqencyRate" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                            </tr>
                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblPremAnnual" Enabled="false" Text="Annual Premium:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtPremAnnual" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                                                <td align="right" valign="top"><asp:Label ID="lblPrem_Monthly" Enabled="false" Text="Monthly Premium:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Monthly" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblPrem_Payable" Enabled="false" Text="Premium Payable:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtPrem_Payable" Enabled="false" runat="server"></asp:TextBox>
                                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
        
                
        
                
        </ContentTemplate>            
            
</asp:TabPanel>

            <asp:TabPanel ID="TabPanel3" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Beneficiary Tab">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <tr>
                            <td align="center" valign="top" class="td_menu">
                            <table align="center" border="0" cellspacing="0" class="tbl_menu_new">

                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Beneficiary Information</td>
                                    </tr>

                            <tr>
                                <td colspan="4">
                                    <table align="center" border="1" style="width: 100%">
                                        <tr style="background-color: Maroon; color: White; font-size: medium; font-weight: bold;">
                                            <td align="left" valign="top"><asp:Label ID="lblBenefry_SN" Text="S/No" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenefry_Name" Text="Beneficiary Name" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenefry_Category" Text="Beneficiary Category" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenefry_DOB" Text="Date of Birth" ToolTip="Data of Birth" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenefry_Relationship" Text="Relationship" ToolTip="" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenefry_Percentage" Text="Percentage" ToolTip="" runat="server"></asp:Label></td>
                                            <td align="left" valign="top"><asp:Label ID="lblBenefry_GuardianName" Text="Guardian Name" ToolTip="" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="font-size: small;">
                                            <td align="left" valign="top"><asp:TextBox ID="txtBenefry_SN" Enabled="false" Width="40px" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtBenefry_Name" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtBenefry_Category" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtBenefry_DOB" Width="100px" ToolTip="Data of Birth(dd/mm/yyyy)" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:DropDownList ID="cboBenefry_Relationship" Width="120px" runat="server"></asp:DropDownList>
                                                &nbsp;<asp:TextBox ID="txtBenefry_Relationship" Visible="false" Width="40px" ToolTip="" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtBenefry_Percentage" Width="40px" ToolTip="" runat="server"></asp:TextBox></td>
                                            <td align="left" valign="top"><asp:TextBox ID="txtBenefry_GuardianName" ToolTip="" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </table>                                        
                                </td>
                            </tr>                                
                            </table>
                            </td>
                        </tr>
                    </table>
                
                
        </ContentTemplate>            
            
</asp:TabPanel>

            <asp:TabPanel ID="TabPanel4" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Medical Exam Tab">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Medical Details</caption>
                        <tr>
                            <td align="center" valign="top" class="td_menu">
                                <table border="0" align="center" cellspacing="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Medical Info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>        
                        
                
        </ContentTemplate>            
            
</asp:TabPanel>

            <asp:TabPanel ID="TabPanel5" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Discount/Loading Tab">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Discount/Loading Details</caption>
                        <tr>
                            <td align="center" valign="top" class="td_menu">
                                <table border="0" align="center" cellspacing="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Discount/Loading Info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>        
                        
                
        </ContentTemplate>            
                
            
</asp:TabPanel>

            <asp:TabPanel ID="TabPanel6" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Miscellaneous Tab">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Miscellaneous Details</caption>
                        <tr>
                            <td align="left" valign="top" class="td_menu">
                                <table border="0" align="center" cellspacing="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Miscellaneous Info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>        
                        
                
        </ContentTemplate>            

            
</asp:TabPanel>

        </asp:TabContainer>
        
    </div>

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
