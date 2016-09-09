<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_INDV_ENQUIRY.aspx.vb"
    Inherits="I_LIFE_PRG_LI_INDV_ENQUIRY" %>

<%@ Register Src="../UC_BANT.ascx" TagName="UC_BANT" TagPrefix="uc1" %>
<%@ Register Src="../UC_FOOT.ascx" TagName="UC_FOOT" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Policy Enquiry</title>
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
            //call print receipt popup 
            $('#butPrint').click(function(e) {
                e.preventDefault();
                //copy content of receipt number to the print dialog receipt number
                $('#txtRecptNo').attr('value', document.getElementById('txtReceiptNo').value)

                $('#PrintDialog').css({ display: true });
                $('#PrintDialog').modal({
                    containerCss: {
                        backgroundColor: "#fff",
                        borderColor: "#fff",
                        height: 400,
                        padding: 5,
                        width: 500
                    },
                    appendTo: 'form',
                    persist: true,
                    overlayClose: false,
                    opacity: 40,
                    overlayCss: { backgroundColor: "black" }

                }
                      );
            });
        });
        
        
        
    </script>

    <style type="text/css">
        .style2
        {
            width: 173px;
        }
        .RecordOriginator
        {
            margin-left: 100px;
        }
        .style3
        {
            height: 22px;
        }
        .style4
        {
            height: 43px;
        }
        .style5
        {
            width: 173px;
            height: 28px;
        }
        .style6
        {
            height: 28px;
        }
        .DocNum
        {
            text-decoration: underline;
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
                                &nbsp;&nbsp;&nbsp;
                                Date of Birth:&nbsp;
                                <input type="text" id="txtDob" name="txtSearch" value="dd/mm/yyyy" 
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
                            <td align="center" colspan="4" valign="top" class="style4">
                                &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go
                                    to Menu</a> &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdNew_ASP" CssClass="cmd_butt"
                                        runat="server" Text="New Data"></asp:Button>
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
                <td nowrap class="myheader">
                    Policy Enquiry
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="td_menu">
                    <table align="center" border="0" class="tbl_menu_new">
                        <tr>
                            <td align="left" colspan="4" valign="top" class="style3">
                                <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
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
                                <asp:Label ID="lblFileNum" Text="File No:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtFileNum" Width="150px" runat="server"></asp:TextBox>
                                &nbsp;<asp:Button ID="cmdFileNum" Text="Go" runat="server" />
                                &nbsp;<asp:TextBox ID="txtRecNo" Visible="false" Enabled="false" MaxLength="18" Width="40"
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
                                <asp:TextBox ID="txtAddress" MaxLength="10" runat="server" Enabled="False" TextMode="MultiLine"
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
                            <td align="left" valign="top" colspan="1">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                Premium Info
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label1" Text="Sum Assured:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtSumAssured" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="Label2" Text="Premium Amount:" Enabled="False" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtPremAmt" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label3" Text="Mode of Payment:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtMop" MaxLength="10" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="Label4" Text="Policy Term:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTenure" Width="80px" Enabled="False" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label5" Text="Proposal Date:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtProposalDate" Enabled="False" MaxLength="4" Width="80" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="Label6" Text="Commencement Date" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtCommenceDate" MaxLength="10" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                                &nbsp;&nbsp;<asp:TextBox ID="txtEffDate" MaxLength="10" runat="server" Enabled="False"
                                    Visible="False" Width="58px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label19" Text="Maturity Date:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtMaturityDate" Enabled="False" MaxLength="4" Width="80" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="lblProductClass" Text="Payment Cover Period:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtCoverPeriod" MaxLength="10" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style5">
                                <asp:Label ID="lblProduct_Num" Text="Renewal Date:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtRenewalDate" MaxLength="10" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style6">
                                <asp:Label ID="lblProduct_Num4" Text="Grace Period:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtGracePeriod" MaxLength="10" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style5">
                                <asp:Label ID="lblProduct_Num0" Text="Policy Status:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtPolStatus" Enabled="false" Width="80px" runat="server"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style6">
                                <asp:Label ID="lblProduct_Num1" Text="Total Premium Due:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtTotalPremDue" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style5">
                                <asp:Label ID="lblProduct_Num2" Text="Total Premium Paid:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtTotalPremPaid" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" class="style6">
                                <asp:Label ID="lblProduct_Num3" Text="Premium Outstanding:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1" class="style6">
                                <asp:TextBox ID="txtPremOutstanding" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                broker/marketer info
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label7" Text="Broker/Marketer Code:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtMarketerCode" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="Label20" Text="Broker/Marketer Name:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtMarketerName" Width="200px" Enabled="false" runat="server"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label11" Text="Contact No:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtMarketerPhone" Enabled="False" MaxLength="4" Width="80" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap align="left" valign="top">
                                <asp:Label ID="Label12" Text="Email Id" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="1">
                                <asp:TextBox ID="txtMarketerEmail" MaxLength="10" runat="server" Enabled="False"
                                    Width="200px"></asp:TextBox>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style2">
                                <asp:Label ID="Label9" Text="Address:" runat="server"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtMarketerAddress" MaxLength="10" runat="server" Enabled="False"
                                    Width="239px" TextMode="MultiLine"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top" class="myMenu_Title">
                                PREMIUM PAYMENT HISTORY
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="4">
                                <div class="div_grid">
                                    <asp:GridView ID="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl" DataKeyNames="TBFN_ACCT_DOC_NO"
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
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_ENTRY_DATE" HeaderText="Entry Date"
                                                ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <%--<asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_DOC_NO" HeaderText="Receipt No"
                                                ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />--%>
                                            <asp:HyperLinkField DataTextField="TBFN_ACCT_DOC_NO" DataNavigateUrlFields="TBFN_ACCT_DOC_NO"
                                                DataNavigateUrlFormatString="PRG_LI_INDV_RECEIPT.aspx?idd={0}" ItemStyle-Width="70px"
                                                HeaderText="Receipt No" ItemStyle-CssClass="DocNum">
                                                <ItemStyle CssClass="DocNum"></ItemStyle>
                                            </asp:HyperLinkField>
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_PAYER_PAYEE_NAME" HeaderText="Payee Name"
                                                ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_AMT_LC" HeaderText="Amount"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true"
                                                DataFormatString="{0:N2}" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_TRANS_MODE" HeaderText="Receipt Mode"
                                                ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_RECP_TYP" HeaderText="Receipt Type"
                                                ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_CHQ_TELLER_NO" HeaderText="Ref No"
                                                ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_CHQ_INWARD_DATE" HeaderText="Cheque Date"
                                                ItemStyle-Width="60px" ConvertEmptyStringToNull="true" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_TRANS_DESC1" HeaderText="Trans Desc"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_TRANS_DESC2" HeaderText="Cover Period"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_DR_MAIN" HeaderText="Main Acct(Debit)"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr class="SearchRow">
                            <td align="left" valign="top" class="SearchFont">
                                Receipt Mode:</td>
                            <td align="left" valign="top" colspan="3" class="SearchFont">
                                T=Teller, C=Cash, Q=Cheque, D=Direct Payment</td>
                        </tr>
                        <tr class="SearchRow">
                            <td align="left" valign="top" class="SearchFont">
                                Receipt Type:</td>
                            <td align="left" valign="top" colspan="3" class="SearchFont">
                                P=Regular Premium, D=Premium Deposit</td>
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
</body>
</html>
