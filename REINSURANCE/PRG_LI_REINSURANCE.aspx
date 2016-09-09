<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_REINSURANCE.aspx.vb"
    Inherits="REINSURANCE_PRG_LI_REINSURANCE" %>

<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>
<%@ Register Src="../UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="UC_BAN" Src="~/UC_BAN.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <title>RE-INSURANCE Details Setup Page</title>

    <link rel="stylesheet" type="text/css" href="../Cal/calendar.css" />
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />

    <style type="text/css">
        .style2
        {
            width: 107px;
        }
    </style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">
    <!-- start banner -->
    <div id="div_banner" align="center">
        <uc1:UC_BAN ID="UC_BAN1" Visible="true" runat="server" />
    </div>
    <!-- content -->
    <div id="div_content" align="center">
        <table id="tbl_content" align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="4" class="tbl_buttons">
                    <table align="center" border="1">
                        <tr>
                            <td align="left" colspan="2" valign="baseline">
                                <asp:Button ID="cmdNew_ASP" CssClass="cmd_butt" runat="server" Text="New Data" OnClientClick="JSNew_ASP()">
                                </asp:Button>
                                &nbsp;<asp:Button ID="cmdSave_ASP" CssClass="cmd_butt" runat="server" Text="Save Data">
                                </asp:Button>
                                &nbsp;<asp:Button ID="cmdDelete_ASP" CssClass="cmd_butt" runat="server" Text="Delete Data"
                                    OnClientClick="JSDelete_ASP()"></asp:Button>
                                &nbsp;<asp:Button ID="cmdPrint_ASP" Enabled="false" CssClass="cmd_butt" Text="Print"
                                    runat="server" />
                                &nbsp;&nbsp;Status: &nbsp;<asp:TextBox ID="txtAction" Visible="true" runat="server"
                                    EnableViewState="False" Width="50px"></asp:TextBox>&nbsp;
                                <!-- <A HREF="<%= request.ApplicationPath %>/Setup/UCD001.aspx" TARGET="fraDetails">Previous Menu</A>&nbsp;| -->
                                <!-- &nbsp;|&nbsp;<a href="javascript:window.close()" style="font-weight:bold;">Close Page</a>&nbsp;| -->
                            </td>
                            <td align="right" colspan="2" valign="baseline">
                                Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}" onblur="if (this.value == '') {this.value = 'Search...';}"></input>&nbsp;
                                <asp:Button ID="cmdSearch" Text="Search" runat="server" />
                                <br />
                                <asp:DropDownList ID="cbo_PfaName" CssClass="comboBx" runat="server" Style="margin-left: 0px"
                                    Width="250px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <style>
                .comboBx
                {
                    margin-top: 10px;
                }
                .style3
                {
                    width: 279px;
                }
                .style4
                {
                    width: 143px;
                }
            </style>
            <tr>
                <td align="center" colspan="4" valign="top" class="td_menu">
                    <table align="center" border="0" cellpadding="1" cellspacing="1" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" class="myMenu_Title">
                                <%=STRPAGE_TITLE%>
                                <asp:Label ID="Label1" runat="server" Text="+++ REINSURANCE DATA ENTRY +++"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" nowrap colspan="4">
                                <asp:Label ID="lblMessage" Text="Staus:" runat="server" Font-Size="Small" ForeColor="Red"
                                    Font-Bold="True"></asp:Label>
                                &nbsp;<a id="PageAnchor_Return_Link" runat="server" class="a_return_menu" href="#"
                                    style="float: right;" visible="False">Returns to Previous Page</a> &nbsp;<%=PageLinks%>&nbsp;
                                <%--onclick="javascript:JSDO_RETURN('MENU_IL.aspx?menu=il_code_cust')"--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" valign="top">
                                <table align="center" style="background-color: White; width: 95%;">
                                    <tr>
                                        <td align="left" colspan="4" valign="top">
                                            <asp:GridView ID="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl" DataKeyNames="TBGL_REC_ID"
                                                HorizontalAlign="Left" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="false"
                                                PageSize="10" PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                                PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next" PagerSettings-PreviousPageText="Previous"
                                                PagerSettings-LastPageText="Last" EmptyDataText="No data available..." GridLines="Both"
                                                ShowFooter="True">
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
                                                    <asp:BoundField ReadOnly="true" DataField="TBGL_REC_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left"
                                                        ConvertEmptyStringToNull="true" />
                                                    <%--<asp:BoundField readonly="true" DataField="TBGL_ID" HeaderText="Ref.ID" HeaderStyle-HorizontalAlign="Left" convertemptystringtonull="true" />--%>
                                                    <asp:BoundField ReadOnly="true" DataField="TBGL_CODE" HeaderText="Re-Ins. Code" HeaderStyle-HorizontalAlign="Left"
                                                        ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ReadOnly="true" DataField="TBGL_FULL_NAME" HeaderText="Re-Ins. Name"
                                                        HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ReadOnly="true" DataField="TBGL_PHONE_NUM" HeaderText="Phone No"
                                                        HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap class="style4">
                                <asp:Label ID="lblCustID" Enabled="false" Text="Record ID:" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td align="left" nowrap class="style3">
                                <asp:TextBox ID="txtPfaID" Enabled="false" MaxLength="3" Width="100px" runat="server"
                                    EnableViewState="true"></asp:TextBox>
                            </td>
                            <td align="left" nowrap class="style2">
                                <asp:Label ID="lblTBGL_CODE" Enabled="False" Text="Re-Ins. Code:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtTBGL_CODE" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="">
                            <td align="right" nowrap class="style4">
                                <asp:Label ID="lblTBGL_DESC1" Enabled="False" Text="Reinsurance Module:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap class="style3">
                                <asp:DropDownList ID="ddnReinsuranceMdl" runat="server" Width="250px">
                                    <asp:ListItem>--Select Item --</asp:ListItem>
                                    <asp:ListItem Value="I">Individual Life</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap class="style2">
                                <asp:Label ID="lblCompanyType" Enabled="False" Text="Company Type:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap>
                                <asp:DropDownList ID="ddnCompanyType" runat="server" Width="250px">
                                    <asp:ListItem>--Select Item --</asp:ListItem>
                                    <asp:ListItem Value="R">REINSURANCE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap valign="top" class="style4">
                                <asp:Label ID="lblTBGL_DESC" Enabled="False" Text="Reinsurance Name:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap class="style3">
                                <asp:TextBox ID="txtTBGL_DESC" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap valign="top" class="style2">
                                <asp:Label ID="lblTBGL_DESC0" Enabled="False" Text="Reinsurance Short Name:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap valign="top">
                                <asp:TextBox ID="txtTBGL_SHRT_DESC" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap valign="top" class="style4">
                                <asp:Label ID="lblTBGL_BRANCH0" Enabled="False" Text="Address 1:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap class="style3">
                                <asp:TextBox ID="txtTBGL_ADRES1" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap valign="top" class="style2">
                                <asp:Label ID="lblTBGL_BRANCH1" Enabled="False" Text="Address 2:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtTBGL_ADRES2" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap class="style4">
                                <asp:Label ID="lblTBGL_BRANCH2" Enabled="False" Text="Phone 1:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap class="style3">
                                <asp:TextBox ID="txtTBGL_PHONE1" runat="server" Width="250px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap class="style2">
                                <asp:Label ID="lblTBGL_BRANCH3" Enabled="False" Text="Phone 2:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtTBGL_PHONE2" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap class="style4">
                                <asp:Label ID="lblTBGL_BRANCH4" Enabled="False" Text="Email 1:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap class="style3">
                                <asp:TextBox ID="txtTBGL_EMAIL1" runat="server" Width="250px"></asp:TextBox>
                            </td>
                            <td align="left" nowrap class="style2">
                                <asp:Label ID="lblTBGL_BRANCH5" Enabled="False" Text="Email 2:" runat="server"></asp:Label>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtTBGL_EMAIL2" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap colspan="2">
                                &nbsp;
                            </td>
                            <td align="left" nowrap colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
    </div>
    <!-- footer -->
    <div id="div_footer" align="center">
        <table id="tbl_footer" align="center">
            <tr>
                <td valign="top">
                    <table align="center" border="0" class="footer" style="background-color: Black;">
                        <tr>
                            <td>
                                <uc2:UC_FOOT ID="UC_FOOT" runat="server" />
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
