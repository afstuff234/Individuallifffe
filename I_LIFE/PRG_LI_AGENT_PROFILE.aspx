<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_AGENT_PROFILE.aspx.vb" Inherits="I_LIFE_PRG_LI_AGENT_PROFILE" %>

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
        $(document).ready(function() {
            // $('#cbosearchBy').on('focusout', function(e) {

            $('#cbosearchBy').change(function(e) {
                e.preventDefault();
                var myval = $('#cbosearchBy').val();
                if (myval == "dob") {
                    $('#lblValue').text('Date of Birth');
                }
                else if (myval == "name") {
                    $('#lblValue').text('Find by Agent Name');
                }
                else if (myval == "eplatform") {
                    $('#lblValue').text('Agent e Platform code');
                }
                else if (myval == "mobile") {
                    $('#lblValue').text('Agent Mobile No');
                }
                //return false;
            });

        });
        
        
        
    </script>

    <style type="text/css">
        .RecordOriginator
        {
            margin-left:100px;
        }
        
        .SearchFont
        {
            font-size:10px;
             padding:0px;
        }
        
         .SearchRow
        {
         height:10px !important;
         padding:0px;
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
                            <td align="left" colspan="2" valign="top" style="color: Red; font-weight: bold;"><%=STRMENU_TITLE%>&nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>
                            </td>
                            <td align="right" colspan="1" valign="top">    
                               <table border="0">
                              <tr class="SearchRow"><td class="SearchFont">Date of birth</td><td>
                                  <asp:TextBox ID="txtDobSearch" runat="server" Height="10px"></asp:TextBox></td></tr>
                              <tr class="SearchRow"><td class="SearchFont">Agent Name</td><td class="style2">
                                  <asp:TextBox ID="txtAgtNameSearch" runat="server" Height="10px"></asp:TextBox></td></tr>
                              <tr class="SearchRow"><td class="SearchFont">Agent Code - E-platform </td><td>
                                  <asp:TextBox ID="txtEplatformSearch" 
                                      runat="server" Height="10px"></asp:TextBox></td></tr>
                              <tr class="SearchRow"><td class="SearchFont">Agent Mobile No</td><td>
                                  <asp:TextBox ID="txtMobileSearch" runat="server" Height="10px"></asp:TextBox></td></tr>
                              </table></td>
                            <td align="left" colspan="1" valign="top">  
                              <asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" 
                                    runat="server" AppendDataBoundItems="True">
                                    <asp:ListItem Value="*">* Select Agent*</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                                    <tr>
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
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
                    <td nowrap class="myheader">Agent Profile</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                                            <tr>
                                                <td align="left" colspan="4" valign="top">
                                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                    <asp:Label ID="lblOriginator" ForeColor="Red" Font-Size="Small" runat="server" CssClass="RecordOriginator"></asp:Label>
                                                </td>
                                                <td align="right" colspan="4" valign="top">
                                                    <a id="PageAnchor_Return_Link" runat="server" class="a_return_menu" href="#" 
                                                        onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=il_code_cust')">Returns 
                                                    to Previous Page</a></td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" colspan="8" valign="top" class="myMenu_Title">basic Info.</td>
                                            </tr>

                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:Label ID="lblRecNo" 
                                                        Text="Rec. No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtRecNo" 
                                                        Enabled="false" Width="119px" runat="server"></asp:TextBox>
                                                    </td>
                                                <td nowrap align="left" valign="top" colspan="5">
                                                    <asp:Label ID="lblCustID" 
                                                        Text="Record ID:" Enabled="False" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" >
                                                   <asp:TextBox ID="txtCustId" Width="119px"
                                                        runat="server" Enabled="False"></asp:TextBox>
                                                    
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top" ><asp:Label ID="lblSetupdate" 
                                                        Text="Setup date" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtSetupDate" 
                                                        Enabled="false" Width="119px" runat="server"></asp:TextBox>
                                                    </td>
                                                <td nowrap align="left" valign="top" colspan="5"><asp:Label ID="lblActivatnDate" 
                                                        Text="Activation date:" Enabled="False" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" >
                                                   <asp:TextBox ID="txtActivatnDate" Width="119px"
                                                        runat="server"></asp:TextBox>
                                                        <%--<input type="date" id="txtActivatnDate" />--%>
                                                    &nbsp;<asp:Label ID="lblTrans_Date_Format" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                                    
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top" >
                                                    <asp:Label ID="lblAgtCodeEPlat" Text="Agent Code - E-platform:" 
                                                        runat="server"></asp:Label></td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtAgtCodeEPlat" 
                                                        runat="server" Width="119px"></asp:TextBox>
                                                    &nbsp;</td>
                                                <td align="left" valign="top" colspan="5"><asp:CheckBox id="chkNum" runat="server" AutoPostBack="true" class="chk_Butt" />
                                                    <asp:Label ID="lblAgentABSCode" 
                                                        Text="Agt Code - ABS:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" >
                                                    <asp:TextBox ID="txtAgentABSCode" Width="119px" 
                                                        Enabled="true" runat="server" AutoPostBack="True"></asp:TextBox>
                                                </td>
                            </tr>

                            <tr>
                                                <td align="left" valign="top" ><asp:Label ID="lblTitle" Text="Title:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top">                                                    
                                                    <asp:DropDownList ID="cboTitle" CssClass="selProduct" 
                                                        runat="server" Width="119px">
                                                        <asp:ListItem Value="*">Select</asp:ListItem>
                                                        <asp:ListItem>Mr</asp:ListItem>
                                                        <asp:ListItem>Mrs</asp:ListItem>
                                                        <asp:ListItem>Ms</asp:ListItem>
                                                        <asp:ListItem>Dr</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td nowrap align="left" valign="top" colspan="5"><asp:Label ID="lblAgentName" 
                                                        Text="Agent Name:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" >
                                                    <asp:TextBox ID="txtAgentName" Width="242px" Enabled="true" runat="server"></asp:TextBox>
                                                </td>    
                            </tr>
                        

                                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblDOB" Text="Date of Birth:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" >                                                    
                                                    <asp:TextBox ID="txtDOB" Width="119px" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblTrans_Date_Format0" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                                    
                                                    </td>
                                                <td align="left" valign="top" colspan="5" ><asp:Label ID="lblGender" Text="Gender:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1" >                                                    
                                                    <asp:DropDownList id="cboGender" CssClass="selProduct" 
                                                        runat="server"></asp:DropDownList>
                                                    &nbsp;&nbsp;</td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left" valign="top" ><asp:Label ID="lblMaritalStatus" Text="Marital Status:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList ID="cboMaritalStatus" AutoPostBack="false" 
                                                        CssClass="selProduct" runat="server" Width="119px"></asp:DropDownList>
                                                    &nbsp;&nbsp;</td>                                            
                                                <td align="left" valign="top" colspan="5">&nbsp;</td>
                                                <td align="left" valign="top" colspan="1" >                                                    
                                                    &nbsp;&nbsp;</td>                                            
                                            </tr>

                                    <tr>
                                        <td align="left" valign="top" ><asp:Label ID="lblAgtHomeAdd" Text="Agent - Home Address:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:TextBox ID="txtAgtHomeAdd" 
                                                 runat="server" TextMode="MultiLine" Width="220px"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="top" colspan="5"><asp:Label ID="lblAgtOffAdd" 
                                                Text="Agent - Office Address:" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:TextBox ID="txtAgtOffAdd" 
                                                runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                            </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" ><asp:Label ID="lblAgtMobNo1" Text="Agent Mobile No. 1:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:TextBox ID="txtAgtMobNo1" 
                                                runat="server" Width="119px"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="top" colspan="5"><asp:Label ID="lblAgtMobNo2" 
                                                Text="Agent Mobile No. 2:" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:TextBox ID="txtAgtMobNo2" 
                                              runat="server" Width="150px"></asp:TextBox>
                                            </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" >
                                            <asp:Label ID="lblAgtLandLnHom" runat="server" Text="Agent - Landline - Home"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:TextBox ID="txtAgtLandLnHom"  runat="server" Width="119px"></asp:TextBox>
                                            </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:Label ID="lblAgtLandLnOff" runat="server" Text="Agent - Landline - Office"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:TextBox ID="txtAgtLandLnOff"  runat="server" Width="150px"></asp:TextBox>
                                            </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" >
                                            <asp:Label ID="lblAgtEmailId" runat="server" Text="Agent - Email Id"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:TextBox ID="txtAgtEmailId" runat="server" Width="220px" ></asp:TextBox>
                                            </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:Label ID="lblMobility" runat="server" Text="Mobility"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:DropDownList id="cboMobility" 
                                                CssClass="selProduct" runat="server">
                                                        <asp:ListItem Value="*">Select</asp:ListItem>
                                                        <asp:ListItem>Two Wheeler</asp:ListItem>
                                                        <asp:ListItem>Four Wheeler</asp:ListItem>
                                                        <asp:ListItem>Public Transport</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </td>
                                    </tr>

                                    <tr>
                                        <td align="left" colspan="8" valign="top" class="myMenu_Title">hierarchy mapping&nbsp;</td>
                                    </tr>

                                    <tr style="display:none">
                                        <td align="left" valign="top" ><asp:Label ID="lblBranchCode" 
                                                Text="Branch Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboBranchCode" Width="220px" runat="server"></asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="top" colspan="5"><asp:Label ID="lblBranchName" 
                                                Text="Branch Name:" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboBranchName" Width="220px" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" >
                                            <asp:Label ID="lblDesignaton" 
                                                Text="Designation:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboDesignation" Width="220px" runat="server">
                                                <asp:ListItem Value="*">Select</asp:ListItem>
                                                <asp:ListItem Value="0">Agent</asp:ListItem>
                                                <asp:ListItem Value="1">Unit Manager</asp:ListItem>
                                                <asp:ListItem Value="2">Agency Manager</asp:ListItem>
                                                <asp:ListItem Value="3">Senior Agency Manager</asp:ListItem>
                                                <asp:ListItem Value="4">Regional Manager</asp:ListItem>
                                                <asp:ListItem Value="5">Zonal Head</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                        <td align="left" valign="top" colspan="5">
                                            &nbsp;</td>
                                        <td align="left" valign="top" colspan="1">
                                            &nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" ><asp:Label ID="lblRegionCode" 
                                                Text="Region Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1" >
                                            <asp:TextBox ID="txtRegionCode" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                                            </td>
                                        <td align="left" valign="top" colspan="5" >
                                            <asp:Label ID="lblRegionCode0" Text="Region Name:" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1" >
                                            <asp:DropDownList ID="cboRegionName" Width="220px" runat="server" 
                                                AutoPostBack="True"></asp:DropDownList>
                                            </td>
                                    </tr>

                                     <tr class="tr_frame_02">
                                        <td align="left" valign="top" >
                                            <asp:Label ID="lblSupervisorSearch" 
                                                Text="Search for Supervisor:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="7"><asp:TextBox ID="txtSupervisor_Search" 
                                                runat="server"></asp:TextBox>
                                            &nbsp;<asp:Button ID="cmdSupervisor_Search" Text="Search..." runat="server" 
                                               />
                                            &nbsp;<asp:DropDownList ID="cboSupervisor_Search" AutoPostBack="True" Width="300px" 
                                                runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="*">* Select Supervisor *</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
<tr>
                                        <td align="left" valign="top" ><asp:Label ID="lblSupervisorCode" 
                                                Text="Supervisor Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="7">
                                            <asp:TextBox ID="txtSupervisor_Code" 
                                                AutoPostBack="true" runat="server" Enabled="False" 
                                               ></asp:TextBox>
                                            &nbsp;<asp:Label ID="lblSupervisorName" Text="Full Name:" runat="server"></asp:Label>
                                            &nbsp;<asp:TextBox ID="txtSupervisor_Name" Enabled="False" runat="server" 
                                                Width="250px"></asp:TextBox>
                                          <%--  &nbsp;<input type="button" id="cmdAssured_Setup" name="cmdAssured_Setup" value="Setup" onclick="javascript:jsDoPopNew_Full('PRG_LI_CUST_DTL.aspx?optid=001&optd=Customer_Details&popup=YES')" />
                                            &nbsp;<input type="button" id="cmdAssured_Browse" name="cmdAssured_Browse" value="Browse..." onclick="javascript:Sel_Func_Open('INS','../WebForm3.aspx?popup=YES','Form1','txtAssured_Num','txtAssured_Name')" />--%>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td nowrap align="left" valign="top" >
                                            <asp:Label ID="lblLvlMgrName1" Text="Level 1 Manager Name:" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboLvlMgrName1" Width="220px" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:Label ID="lblLvlMgrName2" Text="Level 2 Manager Name:" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboLvlMgrName2" Width="220px" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                    </tr>

                                    <tr>
                                        <td nowrap align="left" valign="top" >
                                                    <asp:Label ID="lblLvlMgrName3" 
                                                        Text="Level 3 Manager Name:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboLvlMgrName3" Width="220px" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                                </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:Label ID="lblLvlMgrName4" 
                                                        Enabled="False" Text="Level 4 Manager Name:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboLvlMgrName4" Width="220px" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                                </td>
                                    </tr>

                                    <tr>
                                                <td align="left" valign="top" ><asp:Label ID="lblLvlMgrName5" 
                                                        Text="Level 5 Manager Name:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboLvlMgrName5" Width="220px" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>    
                                                <td align="left" valign="top" colspan="5">
                                                    &nbsp;</td>
                                                <td align="left" valign="top" colspan="1">
                                                    &nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td align="left" colspan="8" valign="top" class="myMenu_Title">
                                            qualification/experience&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblQualification" Text="Highest Qualification:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboQualification" Width="220px" runat="server">
                                                <asp:ListItem Value="*">Select</asp:ListItem>
                                                <asp:ListItem>SSCE</asp:ListItem>
                                                <asp:ListItem>OND</asp:ListItem>
                                                <asp:ListItem>HND</asp:ListItem>
                                                <asp:ListItem>Graduate</asp:ListItem>
                                                <asp:ListItem> Post Graduate</asp:ListItem>
                                                <asp:ListItem>Ph.D</asp:ListItem>
                                                <asp:ListItem>Professional Certifications</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;</td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:Label ID="lblProfile" runat="server" Text="Profile"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboProfile" Width="220px" runat="server">
                                                <asp:ListItem Value="*">Select</asp:ListItem>
                                                <asp:ListItem>Homemaker</asp:ListItem>
                                                <asp:ListItem>Student</asp:ListItem>
                                                <asp:ListItem>Salaried - Private</asp:ListItem>
                                                <asp:ListItem>Salaried - Government</asp:ListItem>
                                                <asp:ListItem>Self Employed</asp:ListItem>
                                                <asp:ListItem>Retired</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" ><asp:Label ID="lblWorkStatus" Text="work Status:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1"><asp:DropDownList ID="cboWorkStatus" 
                                                Width="220px" runat="server">
                                            <asp:ListItem Value="*">Select</asp:ListItem>
                                            <asp:ListItem>Full Time</asp:ListItem>
                                            <asp:ListItem>Part Time</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;</td>
                                        <td align="left" valign="top" colspan="5">
                                            &nbsp;</td>
                                        <td align="left" valign="top" colspan="1">
                                            &nbsp;</td>
                                    </tr>

                            <tr>
                                        <td align="left" colspan="8" valign="top" class="myMenu_Title">license and 
                                            affilations</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" ><asp:Label ID="lblNaicomLicense" Text="NAICOM License:" 
                                                runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboNaicomLicense" Width="119px" runat="server">
                                                        <asp:ListItem Value="*">Select</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem>No</asp:ListItem>
                                                    </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top"colspan="5">
                                            <asp:Label ID="lblNaicomLicenseNo" 
                                                Text="NAICOM License No:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtNaicomLicenseNo" Enabled="true" 
                                                MaxLength="25" Width="150px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" >
                                            <asp:Label ID="lblNaicomLicenseIssueDt" 
                                                Text="NAICOM LC Issue Date:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtNaicomLicenseIssueDt" Visible="true" Enabled="true" 
                                                MaxLength="25" Width="119px" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:Label ID="lblNaicomLicenseExpDt" 
                                                Text="NAICOM LC Expiry Date:" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtNaicomLicenseExpDt" Visible="true" Enabled="true" 
                                                MaxLength="25" Width="142px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" >
                                            <asp:Label ID="lblNaicomLicensePrinName" runat="server" 
                                                Text="NAICOM LC Principal Name"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtNaicomLicensePrinName" Enabled="true" 
                                                MaxLength="25" Width="220px" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" colspan="5">
                                            &nbsp;</td>
                                        <td align="left" valign="top" colspan="1">
                                                    &nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" >
                                                    <asp:Label ID="lblCIINLicense" runat="server" Text="CIIN License:"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboCIINLicense" Width="119px" runat="server">
                                                        <asp:ListItem Value="*">Select</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem>No</asp:ListItem>
                                                    </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top" colspan="5">
                                                    <asp:Label ID="lblCIINLicenseNo" runat="server" Text="CIIN License No"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtCIINLicenseNo" Visible="true" Enabled="true" 
                                                MaxLength="25" Width="150px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" >
                                            <asp:Label ID="lblArianLicense" runat="server" Text="ARIAN License:"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:DropDownList ID="cboArianLicense" Width="119px" runat="server">
                                                        <asp:ListItem Value="*">Select</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem>No</asp:ListItem>
                                                    </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top" colspan="5">
                                            <asp:Label ID="lblArianLicenseNo" runat="server" Text="ARIAN License No."></asp:Label>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtArianLicenseNo" Visible="true" Enabled="true" 
                                                MaxLength="25" Width="150px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="8" valign="top" class="myMenu_Title">bank account details&nbsp;</td>

                                    </tr>

                                    <tr>
                                                <td align="left" valign="top" ><asp:Label ID="lblBnkAcctNo" Text="Bank Account No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtBnkAcctNo" Visible="true" Enabled="true"  Width="119px" 
                                                        runat="server"></asp:TextBox>
                                                </td>    
                                                 <td align="left" colspan="5" valign="top"><asp:Label ID="lblSortCode" 
                                                         Text="SORT Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtSortCode" Visible="true" Enabled="true"  
                                                Width="150px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top"><asp:Label ID="lblBnkAcctHoldName" 
                                                Text="Bank Account Holder Name:" runat="server"></asp:Label></td>
                                        <td align="left" colspan="1" valign="top">
                                                    <asp:TextBox ID="txtBnkAcctHoldName" Visible="true" Enabled="true"  
                                                Width="220px" runat="server"></asp:TextBox>
                                                </td>
                                        <td align="left" colspan="5" valign="top"><asp:Label ID="lblBnkAcctStatus" 
                                                Text="Bank Account Status:" runat="server"></asp:Label></td>
                                        <td align="left" colspan="1" valign="top">
                                                    <asp:DropDownList ID="cboBnkAcctStatus" Width="150px" runat="server">
                                                        <asp:ListItem Value="*">Select</asp:ListItem>
                                                        <asp:ListItem>Savings</asp:ListItem>
                                                        <asp:ListItem>Current</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblBnkName" 
                                                Text="Bank Name:" runat="server"></asp:Label></td>
                                        <td align="left" colspan="1" valign="top">
                                                    <asp:TextBox ID="txtBnkName" Visible="true" Enabled="true"  
                                                Width="220px" runat="server"></asp:TextBox>
                                                </td>
                                        <td align="left" colspan="5" valign="top">
                                            <asp:Label ID="lblBVN" 
                                                Text="BVN:" runat="server"></asp:Label></td>
                                        <td align="left" colspan="1" valign="top">
                                                    <asp:TextBox ID="txtBVN" Visible="true" Enabled="true"  
                                                Width="150px" runat="server"></asp:TextBox>
                                                </td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblCommMode" 
                                                Text="Commission Mode:" runat="server"></asp:Label></td>
                                        <td align="left" colspan="1" valign="top">
                                                    <asp:DropDownList ID="cboCommMode" Width="119px" runat="server">
                                                        <asp:ListItem Value="*">Select</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem>No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                        <td align="left" colspan="5" valign="top">&nbsp;</td>
                                        <td align="left" colspan="1" valign="top">
                                                    &nbsp;</td>
                                    </tr>
 <tr>
                                        <td align="left" colspan="8" valign="top" class="myMenu_Title">other reference&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td align="left" valign="top" >
                                            <asp:Label ID="lblOldAgentNo" 
                                                Text="Old Agent Code:" runat="server"></asp:Label></td>
                                        <td align="left" valign="top" colspan="1">
                                                    <asp:TextBox ID="txtOldAgentCode" Visible="true" Enabled="False"  Width="119px" 
                                                        runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" colspan="5">&nbsp;</td>
                                        <td align="left" valign="top" colspan="1">
                                            &nbsp;</td>
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
