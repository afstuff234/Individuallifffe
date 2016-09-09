<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_REQ_BENE_BANK.aspx.vb" Inherits="I_LIFE_PRG_LI_REQ_BENE_BANK" %>
<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>

<%@ Register Src="../UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Individual Life Module</title>
	        <link href="css/general.css" rel="stylesheet" type="text/css" />

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
								<td align="right" colspan="2" valign="top">&nbsp;&nbsp;Status:&nbsp;<asp:TextBox ID="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:TextBox>&nbsp;&nbsp;Find:&nbsp;
								<input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
									onfocus="if (this.value == 'Search...') {this.value = '';}"
									onblur="if (this.value == '') {this.value = 'Search...';}" />
									&nbsp;&nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
									&nbsp;&nbsp;<asp:DropDownList ID="cboSearch" runat="server" Height="26px"
										Width="150px">
									</asp:DropDownList>
								</td>
							</tr>

							<tr style="display: none;">
								<td align="left" colspan="4" valign="top">
									<hr />
								</td>
							</tr>

							<tr>
								<td align="center" colspan="4" valign="top" style="height: 26px">&nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go to Menu</a>
									&nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
									&nbsp;&nbsp;<asp:Button ID="cmdNew_ASP" CssClass="cmd_butt" runat="server" Text="New Data" OnClientClick="JSNew_ASP();"></asp:Button>
									&nbsp;&nbsp;<asp:Button ID="cmdSave_ASP" CssClass="cmd_butt" runat="server" Text="Save Data"></asp:Button>
									&nbsp;&nbsp;<asp:Button ID="cmdDelItem_ASP" CssClass="cmd_butt" Enabled="false" Font-Bold="true" Text="Delete Item" OnClientClick="JSDelItem_ASP()" runat="server" />
									&nbsp;&nbsp;<asp:Button ID="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" Text="Print"></asp:Button>
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
					<td nowrap class="myheader">Beneficiary Bank Information</td>
				</tr>
				<tr>
					<td align="center" valign="top" class="td_menu">
						<table align="center" border="0" class="tbl_menu_new">
							<tr>
								<td align="left" colspan="4" valign="top">
									<asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                    <asp:Label ID="lblOriginator" ForeColor="Red" Font-Size="Small" 
                                        runat="server" CssClass="RecordOriginator"></asp:Label>
								</td>
							</tr>

							<tr>
								<td nowrap align="right" valign="top">
									<asp:Label ID="lblFileNum" Enabled="true" Text="File No:" runat="server"></asp:Label></td>
								<td align="left" valign="top" colspan="1">
									<asp:TextBox ID="txtFileNum" Enabled="false" Width="250px" runat="server" ></asp:TextBox></td>
								<td align="right" valign="top">
									<asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="true" runat="server"></asp:Label></td>
								<td align="left" valign="top" colspan="1">
									<asp:TextBox ID="txtPolNum" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
							</tr>

							<tr>
								<td align="right" valign="top">
									<asp:Label ID="lblQuote_Num" Enabled="true" Text="Proposal No:" runat="server"></asp:Label></td>
								<td align="left" valign="top" colspan="1">
									<asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
								<td align="right" valign="top" colspan="1">
									<asp:Label ID="lblClaim"  Text="Claim No:" Enabled="false" runat="server"></asp:Label></td>
								<td align="left" valign="top" colspan="1">
									<asp:TextBox ID="txtClaimNo" Enabled="false" runat="server" ></asp:TextBox>
									<asp:TextBox ID="txtClaims_No_sv" Enabled="false" runat="server" CssClass=popupOffset ></asp:TextBox>
								</td>
							</tr>

							<tr>
								<td nowrap align="right" valign="top">
									&nbsp;</td>
								<td align="left" valign="top" colspan="3">
									&nbsp;</td>
							</tr>

                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                BANK ACCOUNT DETAILS</td>
                        </tr>
                       
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Account No:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtBankAccountNo" runat="server" Width=271px></asp:TextBox>
                               
                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label3" runat="server" Text="Account Name:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtBankAccountName" runat="server" Width=250px ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label7" runat="server" Text="Bank Name:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtBankName" runat="server" Width=271px ></asp:TextBox>

                            </td>
                            <td align="left" valign="top" class="style3">
                                <asp:Label ID="Label8" runat="server" Text="Sort Code:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtSortCode" runat="server" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <asp:Label ID="Label2" runat="server" Text="Account Type:"></asp:Label>
                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:DropDownList ID="cmbAccountType" runat="server" 
                                    TabIndex="12" Width="131px">
                                    <asp:ListItem Value="Current">Current</asp:ListItem>
                                    <asp:ListItem Value="Savings">Savings</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td align="left" valign="top" class="style3">
                             <asp:Label ID="lblNormAge" runat="server" Text="Benef. Age:"></asp:Label>
</td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtAge" runat="server" >0</asp:TextBox></td>
                        </tr>
                        <tr>
                        <td align="left" valign="top" class="style3">
                               <asp:Label ID="lblNormName" runat="server" Text="Beneficiary Name:"></asp:Label>

                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtBeneficiaryName" runat="server" Width=271px ></asp:TextBox>
                            </td>                        

                        <td align="left" valign="top" class="style3">
                               <asp:Label ID="lblBenefGardian" runat="server" Text="Benef. Guardian:"></asp:Label>

                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtBenefGuardian" runat="server" Width=271px ></asp:TextBox>

                            </td>                        

                            </tr>

                        <tr>
                        <td align="left" valign="top" class="style3">
                               <asp:Label ID="Label9" runat="server" Text="Total Payout:"></asp:Label>

                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPayOut" runat="server" Width="271px" Enabled="false">0.00</asp:TextBox>
                            </td>                        

                        <td align="left" valign="top" class="style3">
                               <asp:Label ID="lblBenefShare" runat="server" Text="Benef. Share:"></asp:Label>

                            </td>
                            <td align="left" valign="top" class="style2">
                                <asp:TextBox ID="txtPayoutSharePCent" runat="server" Width="100px" Enabled="false">0.00</asp:TextBox>
                                <asp:TextBox ID="txtPayoutShareAmt" runat="server" Width="171px" Enabled="false">0.00</asp:TextBox>

                            </td>                        

                            </tr>
							<tr>
								<td colspan="4">
									<hr />
								</td>
							</tr>


							<tr>
								<td align="center" colspan="4" valign="top">
												<asp:GridView ID="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl"
													DataKeyNames="TBIL_CLM_BENE_BANK_CLM_NO" HorizontalAlign="Left"
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
                                                        <asp:HyperLinkField DataTextField="TBIL_CLM_BENE_BANK_CLM_NO" DataNavigateUrlFields="TBIL_CLM_BENE_BANK_CLM_NO,TBIL_CLM_BENEFICIARY_NAME"
                                                            DataNavigateUrlFormatString="PRG_LI_REQ_BENE_BANK.aspx?idd={0},{1}" ItemStyle-Width="70px"
                                                            HeaderText="Claim No" ItemStyle-CssClass="DocNum">
                                                            <ItemStyle CssClass="DocNum"></ItemStyle>
                                                        </asp:HyperLinkField>


														<asp:BoundField ReadOnly="true" DataField="TBIL_CLM_BENEFICIARY_NAME" HeaderText="Beneficiary Name" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_CLM_BENEFICIARY_AGE" HeaderText="Age" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" DataFormatString="{0:dd MMM yyyy}" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_CLM_BENEFICIARY_SHARE_PCNT" HeaderText="Share %" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_CLM_BENEFICIARY_SHARE" HeaderText="Share Amt" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
														<asp:BoundField ReadOnly="true" DataField="TBIL_CLM_BENE_BANK_GUARDIAN_NAME" HeaderText="Guardian Name" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
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
			</table>
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
</body>
</html>
