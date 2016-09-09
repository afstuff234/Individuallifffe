<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_CLM_MATURE.aspx.vb" Inherits="I_LIFE_PRG_LI_CLM_MATURE" %>

<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>
<%@ Register Src="../UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Individual Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>

    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>

    <script src="../jquery.min.js" type="text/javascript"></script>

    <script src="../jquery.simplemodal.js" type="text/javascript"></script>

    <%--<script src="js/JScript.js" type="text/javascript"></script>--%>
    
    <script language="javascript" type="text/javascript">
       
        
    </script>

    <style type="text/css">
        .style1
        {
        }
        .style2
        {
        }
        .style3
        {
        }
        .style4
        {
            height: 22px;
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
                        <tr>
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;">
                                <%=STRMENU_TITLE%>
                            </td>
                            <td align="right" colspan="1" valign="top" style="display: none;">
                                &nbsp;&nbsp;Status:&nbsp;<asp:TextBox ID="txtAction" Visible="true" ForeColor="Gray"
                                    runat="server" EnableViewState="False" Width="50px"></asp:TextBox>
                            </td>
                            <td align="right" colspan="1" valign="top">
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}" onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
                                &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" 
                                    runat="server" AppendDataBoundItems="True" EnableViewState="False">
                                    <asp:ListItem>** Select Insured **</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" valign="top">
                                &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=IL_CLAIM')">Go
                                    to Menu</a> &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdNew_ASP" CssClass="cmd_butt"
                                        runat="server" Text="New Data" OnClientClick="JSNew_ASP();" 
                                    Enabled="False"></asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdSave_ASP" CssClass="cmd_butt" runat="server" 
                                    Text="Save Data" Enabled="False">
                                </asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdDelete_ASP" CssClass="cmd_butt" Enabled="False" runat="server"
                                    Text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdPrint_ASP" CssClass="cmd_butt" runat="server"
                                    Text="Print" PostBackUrl="~/I_LIFE/PRG_LI_CLM_MATURE_RPT.aspx"></asp:Button>
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
                <td nowrap class="myheader">
                    Maturity Claim Process</td>
            </tr>
            <tr>
                <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" valign="top" class="style4">
                                <asp:Label ID="lblMsg0" ForeColor="Red" Font-Size="Small" runat="server">Status:</asp:Label>
                                <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="">
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                POLICY INFO.
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:CheckBox ID="chkClaimNum" AutoPostBack="true" Text="Claim #:" runat="server" />
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtClaimsNo" runat="server" Enabled="False" TabIndex="1" 
                                    AutoPostBack="True"></asp:TextBox>
                                <asp:Button ID="cmdClaimNoGet" Enabled="false" Text="Get Record" runat="server" Style="height: 26px" />
                                <asp:TextBox ID="txtRecNo" Visible="false" Enabled="false" MaxLength="18" Width="40"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:CheckBox ID="chkPolyNum" AutoPostBack="true" Text="Policy #:" runat="server" />
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPolicyNumber" runat="server" Enabled="False" TabIndex="2" 
                                    AutoPostBack="True"></asp:TextBox>
                                <asp:Button ID="cmdPolyNoGet" Enabled="false" Text="Get Record" 
                                    runat="server" />
                                <asp:TextBox ID="txtRecNo0" Visible="false" Enabled="false" MaxLength="18" Width="40px"
                                    runat="server" Height="22px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Under Writing Year:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtUWY" runat="server" Width="80px" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label17" runat="server" Text="Claim Type:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:DropDownList ID="DdnClaimType" runat="server" TabIndex="11" 
                                    Enabled="False">
                                    <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                                    <asp:ListItem Value="1">Full Maturity</asp:ListItem>
                                    <asp:ListItem Value="2">Patial Maturity</asp:ListItem>
                                    <asp:ListItem Value="3">Surrender</asp:ListItem>
                                    <asp:ListItem Value="4">Death</asp:ListItem>
                                    <asp:ListItem Value="5">Critical Illness</asp:ListItem>
                                    <asp:ListItem Value="6">Accident (AFAB)</asp:ListItem>
                                    <asp:ListItem Value="7">Paid Up</asp:ListItem>
                                    <asp:ListItem Value="8">Partial Withdrawal</asp:ListItem>
                                    <asp:ListItem Value="9">Full Withdrawal</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label3" runat="server" Text="Policy Start Date: "></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPolicyStartDate" runat="server" Enabled="False"></asp:TextBox>
                                <asp:ImageButton ID="butCal" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label2" runat="server" Text="Product Code:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtProductCode" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="Label4" runat="server" Text="Policy End Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPolicyEndDate" runat="server" Enabled="False"></asp:TextBox>
                                <asp:ImageButton ID="butCal1" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format1" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style3" rowspan="2">
                                <asp:Label ID="Label16" runat="server" Text="Product Name:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2" rowspan="2">
                                <asp:TextBox ID="txtProductName" runat="server" Enabled="False" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="Label12" runat="server" Text="Claims Paid Module:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:DropDownList ID="DdnSysModule" runat="server" TabIndex="10" 
                                    Enabled="False">
                                    <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                                    <asp:ListItem Value="I">Individual Life</asp:ListItem>
                                    <asp:ListItem Value="G">Group Life</asp:ListItem>
                                    <asp:ListItem Value="A">Annuity</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="4" class="myMenu_Title">
                                CLAIMS INFO.&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="Label18" runat="server" Text="Claims Calculated Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtClaimsCalculatedDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                <asp:ImageButton ID="butCal2" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format2" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style3">
                                &nbsp;</td>
                            <td align="left" valign="top" class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label runat="server" Text="Contribution Claims LC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtContributionClaimsLC" runat="server" TabIndex="5"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label9" runat="server" Text="Sum Assured Claims LC:"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSumAssuredClaimLC" runat="server" TabIndex="7"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label runat="server" Text="Contribution Claims FC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtContributionClaimsFC" runat="server" TabIndex="6"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label10" runat="server" Text="Sum Assured Claims FC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtSumAssuredClaimFC" runat="server" TabIndex="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label19" runat="server" Text="Bonus Claims LC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtBonusClaimsLC" runat="server" TabIndex="6"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label21" runat="server" Text="Loan Balance LC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtLoanBalanceLC" runat="server" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label20" runat="server" Text="Bonus Claims FC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtBonusClaimsFC" runat="server" TabIndex="6"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label22" runat="server" Text="Loan Balance FC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtLoanBalanceFC" runat="server" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1" colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label23" runat="server" Text="Total Claim Amount LC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtTotalClaimAmtLC" runat="server" TabIndex="6"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3">
                                &nbsp;</td>
                            <td align="left" valign="top" class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label24" runat="server" Text="Total Claim Amount FC:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtTotalClaimAmtFC" runat="server" TabIndex="6"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style3" colspan="2">
                                <asp:CheckBox ID="recalcClaimsCbx" AutoPostBack="true" Text="Re-Calculate Claims?" 
                                    runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF3300" 
                                    Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                &nbsp;</td>
                            <td align="left" valign="top" class="style2">
                                &nbsp;</td>
                            <td align="left" valign="top" class="style3">
                                &nbsp;</td>
                            <td align="left" valign="top" class="style2">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id='confirm'>
        <div class='header'>
            <span>Confirm</span></div>
        <div class='message'>
        </div>
        <div class='buttons'>
            <div class='no simplemodal-close'>
                No</div>
            <div class='yes'>
                Yes</div>
        </div>
    </div>
    <div id="customModal">
        
    </div>
    
    <div id="div_footer" align="center">
        <table id="tbl_footer" align="center">
            <tr>
                <td valign="top">
                    <table align="center" border="0" class="footer" style="background-color: Black;">
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

    <script>
        
    </script>

</body>

</html>
