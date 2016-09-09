<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IL_2010.aspx.vb" Inherits="I_LIFE_IL_2010" %>

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
                    <td align="left" valign="top" class="td_menu_hedaer">
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
                                                <td align="left" valign="top"><asp:TextBox ID="txtTrans_Date" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="butCal" Visible="false"  runat="server" OnClientClick="OpenModal_Cal(this.form,'txtTrans_Date','txtTrans_Date')" 
                                                        ImageUrl="~/Images/Calendar.bmp" Height="17" />
                                                </td>
                            </tr>
                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblStart_Date" Text="Start Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtStart_Date" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="lblEnd_Date" Text="Expiry Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtEnd_Date" runat="server"></asp:TextBox></td>
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
                    <table class="tbl_cont" align="center">
                        <caption>Proposal Details</caption>
                        <tr>
                            <td align="left" valign="top" class="td_menu">
                                <table align="center" border="0" cellspacing="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            <asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data" OnClientClick="JSSave_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" text="Print"></asp:button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Business Source Info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblBraNum" Text="Branch:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3">
                                            <asp:DropDownList ID="cboBranch" Width="220px" AutoPostBack="True" runat="server" OnTextChanged="DoProc_Branch_Change"></asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtBraNum" MaxLength="4" Width="40px" runat="server"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdBranch_Setup" name="cmdBranch_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CODES.aspx?optid=003&optd=Branch&popup=YES')" />
                                            &nbsp;&nbsp;<asp:Button ID="cmdBranch_Refresh" Text="Refresh" runat="server" OnClick="DoProc_Branch_Refresh" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblDeptNum" Text="Department:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:DropDownList ID="cboDepartment" Width="220px" runat="server" OnTextChanged="DoProc_Dept_Change"></asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtDeptNum" runat="server" Width="40px"></asp:TextBox>                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Assured Personal Info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblAssuredNum" Text="Assured Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAssured_Num" MaxLength="30" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblAssuredName" Text="Name:" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAssured_Name" Enabled="False" runat="server" Width="350px"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdAssured_Setup" name="cmdAssured_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CUST_DTL.aspx?optid=001&optd=Customer_Details&popup=YES')" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblAssured_Search" Text="Search for Assured:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAssured_Search" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Button ID="cmdAssured_Search" Text="Search..." runat="server" OnClick="DoProc_Assured_Search" />
                                            &nbsp;<asp:DropDownList ID="cboAssured" AutoPostBack="True" Width="350px" 
                                                runat="server" OnTextChanged="DoProc_Assured_Change"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Agent/Broker Info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblAgcyNunm" Text="Marketer Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAgcyNum" MaxLength="30" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblAgcyName" Text="Name:" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAgcyName" Enabled="False" runat="server" Width="350px"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdAgcy_Setup" name="cmdAgcy_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_MKT_CD.aspx?optid=001&optd=Marketers_Agency&popup=YES')" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblAgcy_Search" Text="Search for Marketer:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtAgcy_Search" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Button ID="Button1" Text="Search..." runat="server" OnClick="DoProc_Agcy_Search" />
                                            &nbsp;<asp:DropDownList ID="cboAgcy" AutoPostBack="True" Width="350px" 
                                                runat="server" OnTextChanged="DoProc_Agcy_Change"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblCustNum" Text="Broker Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="3"><asp:TextBox ID="txtCustNum" MaxLength="30" runat="server"></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblCustName" Text="Name:" runat="server"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCustName" Enabled="False" runat="server" Width="350px"></asp:TextBox>
                                            &nbsp;<input type="button" id="cmdCust_Setup" name="cmdCust_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_BRK_DTL.aspx?optid=001&optd=Brokers_Agents_Details&popup=YES')" />
                                        </td>
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

            <asp:TabPanel ID="TabPanel2" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Proposal Risk Tab">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Risk Details</caption>
                        <tr>
                            <td align="left" valign="top" class="td_menu">
                                <table border="0" align="center" cellspacing="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Risk Info</td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Sum Assured/Premium Info</td>
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

            <asp:TabPanel ID="TabPanel3" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Beneficiary Tab">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Beneficiary Details</caption>
                        <tr>
                            <td align="left" valign="top" class="td_menu">
                                <table border="0" align="center" cellspacing="0" class="tbl_menu_new">
                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Beneficiary Info</td>
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

            <asp:TabPanel ID="TabPanel4" runat="server" CssClass="ajax_panel" ScrollBars="Both" HeaderText="Medical Exam Tab">
                <ContentTemplate>
                    <table class="tbl_cont" align="center">
                        <caption>Medical Details</caption>
                        <tr>
                            <td align="left" valign="top" class="td_menu">
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
                            <td align="left" valign="top" class="td_menu">
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
