<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_REQ_ENTRY.aspx.vb"
    Inherits="I_LIFE_PRG_LI_REQ_ENTRY" %>

<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>
<%@ Register Src="../UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Individual Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
        <link href="css/general.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>

    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>

    <script src="../jquery.min.js" type="text/javascript"></script>

    <script src="../jquery.simplemodal.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
       
        
    </script>

    <style type="text/css">
        .style1
        {
            width: 135px;
        }
        .style2
        {
        }
        .style3
        {
            width: 162px;
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
                                    runat="server" EnableViewState="False" Width="50px" Text="New"></asp:TextBox>
                            </td>
                            <td align="right" colspan="1" valign="top">
                                &nbsp;&nbsp;Find Insured Name:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}" onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;&nbsp;&nbsp;
                                Date of Birth:&nbsp;
                                <input type="text" id="txtDOBSearch" name="txtSearch" value="dd/mm/yyyy" 
                                    runat="server" size="10"  onfocus="if (this.value == 'dd/mm/yyyy') {this.value = '';}" onblur="if (this.value == '') {this.value = 'dd/mm/yyyy';}" />
                                &nbsp;
                                <asp:Button ID="cmdSearch" Text="Search" runat="server" />
                                
                                &nbsp;<asp:DropDownList ID="cboSearch" AutoPostBack="true" Width="150px" runat="server">
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
                                        runat="server" Text="New Data" OnClientClick="JSNew_ASP();"></asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdSave_ASP" CssClass="cmd_butt" runat="server" Text="Save Data">
                                </asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false" runat="server"
                                    Text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:Button>
                                &nbsp;
                                <asp:Button ID="cmdPrint_ASP" CssClass="cmd_butt" runat="server"
                                    Text="Print" PostBackUrl="~/I_LIFE/PRG_LI_REQ_ENTRY_RPT.aspx"></asp:Button>
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
                <td nowrap class="myheader">
                   Claims Request Entry

                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" valign="top" class="style3">
                                <asp:Label ID="Label16" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                <asp:Label ID="lblOriginator" ForeColor="Red" Font-Size="Small" runat="server" CssClass="RecordOriginator"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                Policy Info
                            </td>
                        </tr>
                        <tr>
                                <td align="left" valign="top" class="style2">
                                <asp:CheckBox ID="chkClaimNum" AutoPostBack="true" Text="Claim #:" runat="server" />
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtClaimsNo" runat="server" Enabled="False" TabIndex="1"></asp:TextBox>
                                &nbsp;<asp:Button ID="cmdClaimNoGet" Text="Go" runat="server" /><asp:TextBox ID="txtClaims_No_sv" runat="server"  CssClass=popupOffset ></asp:TextBox>
                                &nbsp;</td>
                            

                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="lblFileNum" Text="File No:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtFileNum" Width="150px" runat="server"></asp:TextBox>
                                &nbsp;<asp:Button ID="cmdFileNum" Text="Go" runat="server" />
                                &nbsp;<asp:TextBox ID="TextBox1" Visible="false" Enabled="false" MaxLength="18" Width="40"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtPolNum" Width="200px" runat="server"></asp:TextBox>
                                &nbsp;<asp:Button ID="cmdGetPol" Text="Go" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="lblTrans_Date" Text="Assured Name:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtAssuredName" MaxLength="10" runat="server" Enabled="False" Width="239px"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblQuote_Num" Text="Proposal No:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtQuote_Num" Width="200px" Enabled="true" runat="server"></asp:TextBox>
                                &nbsp;
                                <asp:Button ID="cmdGetPro" Text="Go" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="lblProductClass0" Text="Product Category:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:DropDownList ID="cboProductClass" AutoPostBack="true" CssClass="selProduct"
                                    runat="server" Width="239px" Enabled="False">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtProductClass" Visible="false" Enabled="false" MaxLength="10"
                                    Width="20" runat="server"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblTrans_Date1" Text="Product Name:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtProduct_Name" Enabled="false" Width="200px" runat="server" Height="22px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="lblCover_Num" Text="Assured Telephone:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtTelephone" MaxLength="10" runat="server" Enabled="False" Width="239px"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblCover_Num0" Text="Assured Email:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtEmail" MaxLength="10" runat="server" Enabled="False" Width="239px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="lblPlan_Num" Text="Assured Address:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtAddress"  runat="server" Enabled="False" TextMode="MultiLine"
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="Label24" Text="DOB:" runat="server"></asp:Label>

                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtDOB" MaxLength="10" runat="server"  Width="150px" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                Premium Info
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label17" Text="Sum Assured:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtSumAssured" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="Label18" Text="Premium Amount:" Enabled="False" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtPremAmt" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label19" Text="Mode of Payment:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtMop" MaxLength="10" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="Label20" Text="Policy Term:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTenure" Width="80px" Enabled="False" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label21" Text="Proposal Date:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtProposalDate" Enabled="False" MaxLength="4" Width="80" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="Label22" Text="Commencement Date" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtCommenceDate" MaxLength="10" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                &nbsp;&nbsp;<asp:TextBox ID="txtEffDate" MaxLength="10" runat="server" Enabled="False"
                                    Visible="False" Width="58px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label23" Text="Maturity Date:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtMaturityDate" Enabled="False" MaxLength="4" Width="80" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="lblProduct_Num1" Text="Total Premium Due:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtTotalPremDue" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style5">
                                <asp:Label ID="lblProduct_Num2" Text="Total Premium Paid:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtTotalPremPaid" Width="150px" runat="server" AutoPostBack="true"></asp:TextBox><%--<asp:RegularExpressionValidator
                                                ID="rvDecimal" runat="server" ErrorMessage="Please Enter a Valid Amount"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" ControlToValidate="txtTotalPremPaid">*</asp:RegularExpressionValidator>--%>
                            </td>
                            <td align="left" valign="top" class="style6">
                                <asp:Label ID="lblProduct_Num3" Text="Premium Outstanding:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtPremOutstanding" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                                                    <td align="left" valign="top" class="style5">
                                <asp:Label ID="Label1" Text="Total Benefit Paid Out:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtTotalPaidOut" Width="150px" runat="server">0.00</asp:TextBox>
                            </td>
                            <td><asp:HiddenField ID="txtPremDueH" runat="server" /></td>

                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                Norminee/Beneficiary
                            </td>
                        </tr>

                        

                        <tr>
								<td align="center" colspan="4" valign="top">
												<asp:GridView ID="GridView2" CellPadding="2" runat="server" CssClass="grd_ctrl"
													DataKeyNames="TBIL_POL_BENF_REC_ID" HorizontalAlign="Left"
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

													<PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom"
														PreviousPageText="Previous"></PagerSettings>

													<Columns>
														<asp:TemplateField>
															<ItemTemplate>
																<asp:CheckBox ID="chkSel" runat="server"></asp:CheckBox>
															</ItemTemplate>
														</asp:TemplateField>

														<asp:CommandField ShowSelectButton="True" />

														<asp:BoundField ReadOnly="true" DataField="TBIL_POL_BENF_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_POL_BENF_NAME" HeaderText="Beneficiary Name" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_POL_BENF_BDATE" HeaderText="Date of Birth" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" DataFormatString="{0:dd MMM yyyy}" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_POL_BENF_AGE" HeaderText="Age" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_POL_BENF_PCENT" HeaderText="PCent %" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
														<asp:BoundField ReadOnly="true" DataField="RELATIONSHIP" HeaderText="Relationship" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_POL_BENF_GURDN_NM" HeaderText="Guardian Name" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
													</Columns>

												</asp:GridView>
									<table align="center" style="background-color: White; width: 95%;">
										<tr>
											<td align="left" colspan="4" valign="top">
												&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:Label ID="lblMsg0" ForeColor="Red" Font-Size="Small" runat="server">Status:</asp:Label>
                                <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                CLAIMS PROCESSING</td>
                        </tr>
                       
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label5" runat="server" Text="Notification Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtNotificationDate" runat="server" TabIndex="3"></asp:TextBox>
                                <asp:ImageButton ID="butCal0" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format0" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label6" runat="server" Text="Claims Effective Date:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtClaimsEffectiveDate" runat="server" TabIndex="4"></asp:TextBox>
                                <asp:ImageButton ID="butCal2" runat="server" OnClientClick="OpenModal_Cal('../Calendar1.aspx?popup=YES',this.form.name,'txtTrans_Date','txtTrans_Date')"
                                    ImageUrl="~/I_LIFE/img/cal.gif" Height="17" Visible="False" />
                                <asp:Label ID="lblTrans_Date_Format2" Text="dd/mm/yyyy" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label13" runat="server" Text="Claims Request Type:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:DropDownList ID="DdnClaimType" runat="server" TabIndex="11">
                                    <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                                    <asp:ListItem Value="1">Death</asp:ListItem>
                                    <asp:ListItem Value="2">Maturity</asp:ListItem>
                                    <asp:ListItem Value="3">Surrender</asp:ListItem>
                                    <asp:ListItem Value="4">Partial Maturity</asp:ListItem>
                                    <asp:ListItem Value="5">Annuity</asp:ListItem>
                                    <asp:ListItem Value="6">Partial Withdrawal</asp:ListItem>
                                    <asp:ListItem Value="7">Loan Against Policy</asp:ListItem>
                                    <asp:ListItem Value="8">Policy Refund</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label14" runat="server" Text="Status:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:DropDownList ID="DdnLossType" runat="server" 
                                    TabIndex="12" Width="131px">
                                    <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                    <asp:ListItem Value="2">Rejected</asp:ListItem>
                                    <asp:ListItem Value="3">On Hold</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label15" runat="server" Text="Claims Description:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2" rowspan="2">
                                <asp:TextBox ID="txtProductDec" runat="server" Height="59px" TextMode="MultiLine"
                                    Width="271px" TabIndex="13"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="lblSettlement" runat="server" Text="Settlemment Status:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                               <asp:DropDownList ID="ddnClaimSettle" runat="server" 
                                    TabIndex="12" Width="131px">
                                    <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                                    <asp:ListItem Value="Full Settlement">Full Settlement</asp:ListItem>
                                    <asp:ListItem Value="Partial Settlement">Partial Settlement</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" class="style2">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" class="style2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                CLAIMS SETTLEMENT HISTORY
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <div class="div_grid">
                                    <asp:GridView ID="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl" DataKeyNames="TBIL_CLM_RPTD_CLM_NO"
                                        HorizontalAlign="Left" AutoGenerateColumns="False" AllowPaging="false" AllowSorting="true"
                                        PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                        PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next" PagerSettings-PreviousPageText="Previous"
                                        PagerSettings-LastPageText="Last" EmptyDataText="No data available..." GridLines="Both"
                                        ShowHeader="True" ShowFooter="True">
                                        <PagerStyle CssClass="grd_page_style" />
                                        <HeaderStyle CssClass="grd_header_style" />
                                        <RowStyle CssClass="grd_row_style" />
                                        <SelectedRowStyle CssClass="grd_selrow_style" />
                                        <EditRowStyle CssClass="grd_editrow_style" />
                                        <AlternatingRowStyle CssClass="grd_altrow_style" />
                                        <FooterStyle CssClass="grd_footer_style" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom"
                                            PreviousPageText="Previous"></PagerSettings>
                                        <Columns>
                                            <%--<asp:TemplateField>
        			                                        <ItemTemplate>
        						                                <asp:CheckBox id="chkSel" runat="server" Width="20px"></asp:CheckBox>
                                                            </ItemTemplate>                                                            
                                                        </asp:TemplateField>
                                --%>
                                            <%-- <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />--%>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_CLM_RPTD_KEYDTE" HeaderText="Entry Date"
                                                ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <%--<asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_DOC_NO" HeaderText="Receipt No"
                                                ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />--%>
                                            <asp:HyperLinkField DataTextField="TBIL_CLAIM_REPTED_REC_ID" DataNavigateUrlFields="TBIL_CLAIM_REPTED_REC_ID,TBIL_CLM_RPTD_CLM_NO"
                                                DataNavigateUrlFormatString="PRG_LI_REQ_ENTRY.aspx?idd={1},{0}" ItemStyle-Width="70px"
                                                HeaderText="Rec ID" ItemStyle-CssClass="DocNum">
                                                <ItemStyle CssClass="DocNum"></ItemStyle>
                                            </asp:HyperLinkField>

                                            <asp:HyperLinkField DataTextField="TBIL_CLM_RPTD_CLM_NO" DataNavigateUrlFields="TBIL_CLAIM_REPTED_REC_ID,TBIL_CLM_RPTD_CLM_NO"
                                                DataNavigateUrlFormatString="PRG_LI_REQ_ENTRY.aspx?idd={1},{0}" ItemStyle-Width="70px"
                                                HeaderText="Claim No" ItemStyle-CssClass="DocNum">
                                                <ItemStyle CssClass="DocNum"></ItemStyle>
                                            </asp:HyperLinkField>
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_CLM_RPTD_NOTIF_DT" HeaderText="Notif. Date"
                                                ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" DataFormatString="{0:dd/MM/yyyy}"  />
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_CLM_RPTD_LOSS_DT" HeaderText="Eff. Date"
                                                ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" DataFormatString="{0:dd/MM/yyyy}"  />
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_CLM_RPTD_CLM_TYPE" HeaderText="Claim Type"
                                                ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                                
                                            <asp:BoundField ReadOnly="true" DataField="TBIL_CLM_RPTD_DESC" HeaderText="Description"
                                                ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true"/>
                                                
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
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
        $("#txtBasicSumClaimsLC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })

        $("#txtBasicSumClaimsFC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })

        $("#txtAdditionalSumClaimsLC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })

        $("#txtAdditionalSumClaimsFC").keypress(function(e) {
            //if the letter is not digit then display error and don't type anything
            if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                alert("Invalid keyboard entry!");
                return false;
            }
        })
    </script>

</body>
</html>
