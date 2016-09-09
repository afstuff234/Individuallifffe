<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RPT_LI_AGENCY_DET.aspx.vb" Inherits="I_LIFE_RPT_LI_AGENCY_DET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <link rel="stylesheet" href="calendar.css" />
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
    <link href="css/rounded.css" rel="stylesheet" type="text/css" />

    <script src="jquery-1.11.0.js" type="text/javascript"></script>

    <script src="jquery.simplemodal.js" type="text/javascript"></script>

    <script language="JavaScript" src="calendar_eu.js" type="text/javascript"></script>
    <title>Tech Reports</title>
    <style type="text/css">
        .style1
        {
            height: 31px;
        }
        .style2
        {
            width: 84px;
        }
    </style>
</head>
<body onload="<%= FirstMsg %>">
   <form id="LifeReportsPrint" runat="server">
    <div>
    </div>
    <div class="newpage" style="margin-left: 20%!important; margin-right: 20%!important;
        width: 400px;">
        <table align="center" width="400">
            <tr>
                <td>
                    <asp:Literal runat="server" Visible="false" ID="litMsgs"></asp:Literal>
                    <asp:Label runat="server" ID="Status" Font-Bold="true" ForeColor="Red" Visible="true"
                        Text="Status:"> </asp:Label>
                    <asp:Label runat="server" ID="Label1" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="grid" style="width: 400px!important;">
            <div class="rounded">
                <div class="top-outer">
                    <div class="top-inner">
                        <div class="top">
                            <h2>
                                PRINT: Agency Detail Report</h2>
                        </div>
                    </div>
                </div>
                <div class="mid-outer">
                    <div class="mid-inner">
                        <div class="mid">
                             <table class="tbl_menu_new" style="width:400px !important">
                                <tr>
                                    <td class="style1" align="center">
                                        <asp:Label ID="lblDesc1" runat="server" Text="List Print" Visible="False"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <p>
                                            &nbsp;
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="butOK" runat="server" Text="OK" Style="height: 26px" />
                                        <asp:Button ID="butClose" runat="server" Text="Close" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="bottom-outer">
                    <div class="bottom-inner">
                        <div class="bottom">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
