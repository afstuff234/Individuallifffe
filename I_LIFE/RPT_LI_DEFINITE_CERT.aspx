<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RPT_LI_DEFINITE_CERT.aspx.vb" Inherits="I_LIFE_RPT_LI_DEFINITE_CERT" %>
<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Individual Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
    <script src="../jquery.min.js" type="text/javascript"></script>
    <script src="../jquery.simplemodal.js" type="text/javascript"></script>
        
    <script language="javascript" type="text/javascript">


//            function confirm(message, callback) {
//                var modalWindow = document.getElementById("confirm");
//                console.log(modalWindow);
//                $(modalWindow).modal({
//                    closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
//                    position: ["20%", ],
//                    overlayId: 'confirm-overlay',
//                    containerId: 'confirm-container',
//                    onShow: function(dialog) {
//                        var modal = this;

//                        $('.message', dialog.data[0]).append(message);

//                        $('.yes', dialog.data[0]).click(function() {
//                            modal.close(); $.modal.close();
//                            callback(); // HERE IS THE CALLBACK
//                            return true;
//                        });
//                    }
//                });
//            }
//            function doSomething() {
                //$('#txtRecStatusTemp').attr('value', 'OLD');
                //do nothing

//            }

            // calling jquery functions once document is ready
            $(document).ready(function() {
            });
        
        
        
    </script>

    <style type="text/css">
        .style2
        {
            width: 173px;
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
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;"><%=STRMENU_TITLE%></td>
                            <td align="right" colspan="1" valign="top" style="display:none;">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>
                            </td>
                            <td align="right" colspan="1" valign="top">    
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" 
                                    runat="server" AppendDataBoundItems="True">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                    <td nowrap class="myheader">Print Definite Certificate</td>
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
                                                <td align="left" valign="top" class="style2"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolNum" Width="259px" 
                                                        runat="server"></asp:TextBox>
                                                    <asp:Button ID="cmdFileNum" Text="Get Record" runat="server" />
                                                    &nbsp;</td>
                                                <td nowrap align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">&nbsp;</td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top" class="style2"><asp:Label ID="lblCover_Num" 
                                                        Text="Policy Class:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtPolicyClass" MaxLength="10" 
                                                        runat="server" Enabled="False" Width="225px"></asp:TextBox>
                                                    &nbsp;</td>
                                                <td align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top">&nbsp;</td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top" class="style2">
                                                    <asp:Label ID="lblTrans_Date" Text="Assured Name:" 
                                                        runat="server"></asp:Label></td>
                                                <td align="left" valign="top"><asp:TextBox ID="txtAssuredName" Enabled="False" 
                                                        MaxLength="4" Width="250px" runat="server"></asp:TextBox>
                                                </td>
                                                <td nowrap align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">
                                                    &nbsp;&nbsp;</td>    
                            </tr>
                        

                                            <tr>
                                                <td align="left" valign="top" class="style2"><asp:Label ID="lblUWYear" 
                                                        Text="Start Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:TextBox ID="txtStartDate" Enabled="False" MaxLength="4" Width="125px" 
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    &nbsp;&nbsp;</td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" valign="top" class="style2"><asp:Label ID="lblProductClass" 
                                                        Text="End Date:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:TextBox ID="txtEndDate" Enabled="False" MaxLength="4" Width="125px" 
                                                        runat="server"></asp:TextBox>
                                                    </td>                                            
                                                <td align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    &nbsp;&nbsp;</td>                                            
                                            </tr>

                                            <tr>
                                                <td align="left" valign="top" class="style2">
                                                    <asp:Label ID="lblCover_Num0" 
                                                        Text="Supervisor:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboSupervisor" runat="server">
                                                    </asp:DropDownList>
                                                    </td>                                            
                                                <td align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    &nbsp;</td>                                            
                                            </tr>

                                            <tr>
                                                <td align="left" valign="top" class="style2">
                                                    <asp:Label ID="lblCover_Num2" 
                                                        Text="Reinsurance:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboReinsurance" runat="server">
                                                    </asp:DropDownList>
                                                    </td>                                            
                                                <td align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    &nbsp;</td>                                            
                                            </tr>

                                            <tr>
                                                <td align="left" valign="top" class="style2">
                                                    <asp:Label ID="lblCover_Num1" 
                                                        Text="Report Type:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:RadioButtonList ID="radReportType" runat="server">
                                                        <asp:ListItem Value="rptDefiniteCert">Definite Cert.</asp:ListItem>
                                                        <asp:ListItem Value="rptDefiniteCertLetter">Definite Cert. Letter</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    </td>                                            
                                                <td align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    &nbsp;</td>                                            
                                            </tr>

                                            <tr>
                                                <td align="left" valign="top" class="style2">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    &nbsp;</td>                                            
                                                <td align="left" valign="top">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:button id="cmdPrint" CssClass="cmd_butt" runat="server" text="Print"></asp:button>
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
