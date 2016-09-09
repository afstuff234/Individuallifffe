<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_LAPSE_PROCESS.aspx.vb" Inherits="I_LIFE_PRG_LI_LAPSE_PROCESS" %>
<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Policy Lapse Processing</title>
     <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
    <script src="../jquery.min.js" type="text/javascript"></script>

    <script src="../Script/jquery-1.11.0.js" type="text/javascript"></script>
    <script src="../jquery.simplemodal.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript"> </script>
    
     <script src="../calendar_eu.js" type="text/javascript"></script>

    <link href="../calendar.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .style1
        {
            width: 1181px;
        }
          .linkStyle
        {
        	background-color:red;
            color:#fff;
        	
        }
        .style2
        {
            width: 109px;
        }
    </style>
</head>
<body onload="<%= FirstMsg %>">
    <form id="PRG_PAIDUP_PROCESS" runat="server">
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
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;"><%=STRMENU_TITLE%></td>
                            <td align="left" colspan="1" valign="top" style="display:none;">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>
                            </td>
                            <td align="right" colspan="1" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<%--<asp:Button ID="cmdSearch" Text="Search" runat="server" />--%><asp:Button
                                    ID="cmdSearch" runat="server" Text="Search" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" Width="150px" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True">
                                    <asp:ListItem>* Select Insured *</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=IL_CLAIM')">Go to Menu</a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdPrintAll" 
                                                CssClass="cmd_butt" runat="server" Text="Print List" 
                                                OnClientClick="return PrintLapsePolicy()" />
                             &nbsp;&nbsp;<asp:Button ID="cmdUpdate" CssClass="cmd_butt" runat="server" Text="Make Policy Lapse" 
                                                OnClientClick="return ClientUpdateLapse()" Width="156px" />
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" runat="server" 
                                                text="Print Lapse" Height="35px"></asp:button>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                        
                    </table>                    
                </td>
            </tr>
        </table>
    </div>
    <div id="div_content" align="center">
     <table class="tbl_cont">
                <tr>
                    <td nowrap class="myheader">&nbsp;Policy Lapse Process</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new"">
                    <tr>
                        <td colspan="4">
                            <center>
                                <asp:Label ID="lblMsg" runat="server" Font-Size="13pt" ForeColor="#FF3300"></asp:Label></center>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" class="style2">
                            <asp:Label ID="Label1" runat="server" Text="Policy Number"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyNo" runat="server" AutoPostBack="True" Width="287px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="4" style="height:400px!important;">
                                      <asp:GridView ID="GrdLapsePolicy" runat="server" AllowPaging="True" 
                                          AutoGenerateColumns="False">
                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="10" />
                                    
                             <Columns>
                                 
                                 <asp:TemplateField> 
                                     <ItemTemplate>
                                         <asp:CheckBox ID="chkPolicyNo" runat="server" />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="POLICY NO" HeaderText="POLICY NUMBER" />
                                 <asp:BoundField DataField="ASSURED CODE" HeaderText="ASSURED CODE" />
                                 <asp:BoundField DataField="ASSURED NAME" HeaderText="ASSURED NAME" 
                                     SortExpression="ASSURED NAME" >
                                 <ItemStyle Width="120px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="PRODUCT CODE" HeaderText="PRODUCT CODE" 
                                     SortExpression="PRODUCT CODE" />
                                 <asp:BoundField DataField="PRODUCT NAME" HeaderText="PRODUCT NAME" 
                                     SortExpression="PRODUCT NAME" />
                                 <asp:BoundField DataField="START DATE" HeaderText="START DATE" 
                                     SortExpression="START DATE" >
                                 <ItemStyle Width="50px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="END DATE" HeaderText="END DATE" 
                                     SortExpression="END DATE" >
                                 <ItemStyle Width="50px" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="LAST PREMIUM PAID DATE" 
                                     HeaderText="LAST PREMIUM PAID DATE" 
                                     SortExpression="LAST PREMIUM PAID DATE" >
                                     <ItemStyle Width="50px" />
                                 </asp:BoundField>
                             
                                 
                    </Columns>
                      <SelectedRowStyle BackColor="Silver" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                  <HeaderStyle BackColor="ControlDarkDark" CssClass="CartListHead" Font-Bold="True"
                      ForeColor="White" />
                  <RowStyle BackColor="Control" Font-Names="Trebuchet MS" Font-Size="Small " ForeColor="black" />
                  <AlternatingRowStyle BackColor="white" Font-Names="Trebuchet MS" Font-Size="Small "
                      ForeColor="black" />
                            </asp:GridView></td>
                    </tr>
                    </table>
                    </td>                                                                                    
                </tr>
        </table>
    </div>
    
 <div id='confirm'>
        <div class='header'><span>Confirm</span></div>
        <div class='message'></div>
        <div class='buttons'>
            <div class='no simplemodal-close'>No</div><div class='yes'>Yes</div>
        </div>
    </div>
<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td colspan="4" class="style1">                                                        
                            <uc2:UC_FOOT ID="UC_FOOT1" runat="server" />                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>    

    </form></body>
    <script type="text/javascript">
        function ClientUpdateLapse() {
            var res = confirm("Are you sure you want to Process lapse for this policy?")
            if (res) {

                var res1 = confirm("Blank and Last Premium paid date less than a year will not be processed")
                if (res1) {
                    return true
                }
                else {
                return false
                }
            }
            else {
                return false;
            }
        }
        function PrintLapsePolicy() {
            var res = confirm("Are you sure you want to print this lapse policy?")
            if (res) {
                return true
            }
            else {
                return false;
            }
        }
        
    </script>
</html>
