<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_CANCEL_PROCESS.aspx.vb" Inherits="I_LIFE_PRG_LI_CANCEL_PROCESS" %>
<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Policy Cancellation</title>
     <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
    <script src="../jquery.min.js" type="text/javascript"></script>

    <script src="../Script/jquery-1.11.0.js" type="text/javascript"></script>
    <script src="../jquery.simplemodal.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript"> </script>
    
    <style type="text/css">
  /*      #lblPaidUpEffDate
 {
 	display:none;
 }
 #txtCancelDate
 {
 	display:none;
 }
 #lblPaidUpEffFormat
 {
 	display:none;
 }*/
        .style1
        {
            width: 1181px;
        }
    </style>
</head>
<body onload="<%= FirstMsg %>">
    <form id="form1" runat="server">
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
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" runat="server" 
                                                text="Print"></asp:button>
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
                    <td nowrap class="myheader">&nbsp;Policy Cancellation Process</td>
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
                        <td align="left" valign="top">
                            <asp:Label ID="Label2" runat="server" Text="Policy Number: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyNumber" runat="server" Width="221px" 
                                AutoPostBack="True"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label3" runat="server" Text="Assured Code:"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtAssuredCode" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="Label4" runat="server" Text="Assured Name:"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtAssuredName" runat="server" Width="270px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label5" runat="server" Text="Product Code: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyProCode" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="Label6" runat="server" Text="Product Name: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtProdDesc" runat="server" Width="270px" style="height: 22px" 
                                Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label7" runat="server" Text="Policy Start Date: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyStartDate" runat="server" Enabled="False"></asp:TextBox>
                            <asp:Label ID="lblPaidUpEffFormat0" runat="server" Text="dd/mm/yyyy" 
                                Visible="False"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:Label ID="Label8" runat="server" Text="Policy End Date: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPolicyEndDate" runat="server" Enabled="False"></asp:TextBox>
                            <asp:Label ID="Label12" runat="server" Text="dd/mm/yyyy"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label13" runat="server" Text="Premium Paid: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPremiumPaid" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <td  align="left" valign="top">
                            <asp:Label ID="Label16" runat="server" Text="Basic Commission Paid: "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBasicCommPaid" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:Label ID="Label14" runat="server" Text="Agent Code: "></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtAgentCode" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <td  align="left" valign="top">
                            <asp:Label ID="Label18" runat="server" Text="Agent Name: "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAgentName" runat="server" Enabled="False" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:HiddenField ID="HidPolyStatus" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCancelPolicy" runat="server" Text="Cancel Policy?" 
                                AutoPostBack="True" />
                        </td>
                        <td  align="left" valign="top">
                            <asp:Label ID="lblPaidUpEffDate" runat="server" Text="Effective Cancel Date: " 
                                Visible="False"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCancelDate" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="lblPaidUpEffFormat" runat="server" Text="dd/mm/yyyy" 
                                Visible="False"></asp:Label>
                        </td>
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
    
      <script language="javascript" type="text/javascript">
          function CheckDate(my) {
              var returnMsg;
              var d = new Date();
              var userdate = new Date(my)
              // var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(18|20)\d{2}$/; //mm/dd/yyyy
              var date_regex = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/
              if (!(date_regex.test(my))) {
                  returnMsg = false;
              }
              else {
                  returnMsg = true;
              }
              return returnMsg;
          }
          
          
          $('#txtCancelDate').blur(function(e) {
              e.preventDefault();
              if ($('#txtCancelDate').val() != "") {
                  var res = CheckDate($('#txtCancelDate').val());
                  if (res == true) {
                      $('#lblMsg').text("");
                      return true
                  }
                  else {
                      alert("Not a valid policy cancellation date format")
                      $('#lblMsg').text("Not a valid Paid Up date format");
                      $('#txtCancelDate').focus();
                      return false;
                  }
              }
          });
    </script>
</html>
