<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_POLY_MEDIC_INFO.aspx.vb"
    Inherits="I_LIFE_PRG_LI_INDV_POLY_MEDIC_INFO" %>

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

    <style>
        .RecordOriginator
        {
            margin-left: 100px;
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
                            <td align="left" colspan="2" valign="top">
                                <%=STRMENU_TITLE%>
                            </td>
                            <td align="right" colspan="2" valign="top">
                                &nbsp;&nbsp;Status:&nbsp;<asp:TextBox ID="txtAction" Visible="true" ForeColor="Gray"
                                    runat="server" EnableViewState="False" Width="50px"></asp:TextBox>&nbsp;&nbsp;Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}" onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;&nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
                                &nbsp;&nbsp;<asp:DropDownList ID="cboSearch" runat="server" Height="26px" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td align="left" colspan="4" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4" valign="top" style="height: 26px">
                                &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go
                                    to Menu</a> &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false"
                                        Text="«..Previous" runat="server" />
                                &nbsp;&nbsp;<asp:Button ID="cmdNew_ASP" CssClass="cmd_butt" runat="server" Text="New Data"
                                    OnClientClick="JSNew_ASP();"></asp:Button>
                                &nbsp;&nbsp;<asp:Button ID="cmdSave_ASP" CssClass="cmd_butt" runat="server" Text="Save Data">
                                </asp:Button>
                                &nbsp;&nbsp;<asp:Button ID="cmdDelItem_ASP" CssClass="cmd_butt" Enabled="false" Font-Bold="true"
                                    Text="Delete Item" OnClientClick="JSDelItem_ASP()" runat="server" />
                                &nbsp;&nbsp;<asp:Button ID="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server"
                                    Text="Print"></asp:Button>
                                &nbsp;&nbsp;<asp:Button ID="cmdNext" CssClass="cmd_butt" Enabled="false" Text="Next..»"
                                    runat="server" />
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
                    Medical Examination Information
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                <asp:Label ID="lblOriginator" ForeColor="Red" Font-Size="Small" runat="server" CssClass="RecordOriginator"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="lblFileNum" Enabled="false" Text="File No:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtFileNum" Enabled="false" Width="230px" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                <asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="false" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtPolNum" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="lblQuote_Num" Enabled="false" Text="Proposal No:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" colspan="1">
                                <asp:Label ID="lblRecNo" BorderStyle="Solid" Text="Rec. No:" Enabled="false" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtMed_Exam_CodeName" Visible="false" Enabled="false" Width="40px"
                                    runat="server"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtMed_Clinic_CodeName" Visible="false" Enabled="false" Width="40px"
                                    runat="server"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtMed_Status_Name" Visible="false" Enabled="false" Width="40px"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                Medical Examination Details
                            </td>
                        </tr>
                        <tr>
                            <td nowrap colspan="4" class="td_frame">
                                <table align="center" border="1">
                                    <tr class="tr_frame">
                                        <td nowrap align="left" valign="top">
                                            <asp:Label ID="lblMed_Exam_Code" Text="Medical Exam. Test Type" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMed_Exam_Date" Text="Test Date" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblMed_Exam_Date_Format" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMed_Clinic_Code" Text="Clinic/Hospital Name" runat="server"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblMed_Sttus" Text="Status" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="font-size: small;">
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="cboMed_Exam_code" Width="200px" runat="server">
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtMed_Exam_Code" Visible="false" Enabled="false" Width="40px"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtMed_Exam_Date" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboMed_Clinic_Code" Width="200px" runat="server">
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtMed_Clinic_Code" Visible="false" MaxLength="10" Width="40px"
                                                runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:DropDownList ID="cboMed_Status" Width="120px" runat="server">
                                                <asp:ListItem Selected="True" Value="*">(Select item)</asp:ListItem>
                                                <asp:ListItem Value="P">Pending</asp:ListItem>
                                                <asp:ListItem Value="S">Successful</asp:ListItem>
                                                <asp:ListItem Value="U">Unsuccessful</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtMed_Status_Code" Visible="false" Enabled="false" Width="40px"
                                                runat="server"></asp:TextBox>
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
                            <td align="center" colspan="4" valign="top">
                                <table align="center" style="background-color: White; width: 95%;">
                                    <tr>
                                        <td align="left" colspan="4" valign="top">
                                            <asp:GridView ID="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl" DataKeyNames="TBIL_POL_MED_REC_ID"
                                                HorizontalAlign="Left" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true"
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
                                                    <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MED_REC_ID" HeaderText="Ref.No"
                                                        HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MED_FILE_NO" HeaderText="File No"
                                                        HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ReadOnly="true" DataField="TBIL_MED_EXAM_NAME" HeaderText="Test Type"
                                                        HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ReadOnly="true" DataField="TBIL_POL_MED_DATE" HeaderText="Test Date"
                                                        HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" DataFormatString="{0:dd MMM yyyy}" />
                                                    <asp:BoundField ReadOnly="true" DataField="TBIL_MED_CLINIC_NAME" HeaderText="Clinic/Hospital Name"
                                                        HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                                    <asp:BoundField ReadOnly="true" DataField="TBIL_MED_STATUS_NAME" HeaderText="Status"
                                                        HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
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
