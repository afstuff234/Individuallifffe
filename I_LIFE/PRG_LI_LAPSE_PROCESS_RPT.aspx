<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_LAPSE_PROCESS_RPT.aspx.vb" Inherits="I_LIFE_PRG_LI_LAPSE_PROCESS_RPT" %>

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
     <script src="../calendar_eu.js" type="text/javascript"></script>
    <link href="../calendar.css" rel="stylesheet" type="text/css" />
    <title>Lapse Policies Report</title>
</head>
<body>
    <form id="PRG_LI_LAPSE_PROCESS_RPT" runat="server">
      <div>
    </div>
    <div  class="newpage" style="margin-left: 20%!important; margin-right: 20%!important;>
    <table>
    <tr>
    <td>
        <asp:Literal runat="server" Visible="false" ID="litMsgs"></asp:Literal>
        <asp:Label runat="server" ID="Status" Font-Bold="true" ForeColor="Red" Visible="true" Text="Status:"> </asp:Label>
        <asp:Label runat="server" ID="Label1" Font-Bold="true" ForeColor="Red" Visible="false"> </asp:Label>
    </td></tr>
    </table>

     <div class="grid">
            <div class="rounded">
                <div class="top-outer"><div class="top-inner"><div class="top">
                    <h2>PRINT: Lapse Policies Report Listing</h2>
                </div></div></div>
                <div class="mid-outer"><div class="mid-inner">
                <div class="mid">     
                	

                <table class="tbl_menu_new">
			        <tr><td colspan="2" class="myMenu_Title" align="center">&nbsp;</td>
                        </tr>
				    <tr>
					    <td>Lapse Start Date</td>
					    <td><asp:TextBox ID="txtStartDate" runat="server" Width="150px" MaxLength=10 ></asp:TextBox>
					    					<script language="JavaScript" type="text/javascript">
					    					    new tcal({ 'formname': 'PRG_LI_LAPSE_PROCESS_RPT', 'controlname': 'txtStartDate' });</script>dd/mm/yyyy</td>
				    </tr>
    				
				    
				    <tr>
					    <td>Lapse End Date</td>
					    <td><asp:TextBox ID="txtEndDate" runat="server" Width="150px" MaxLength=10 ></asp:TextBox>
					    	<script language="JavaScript" type="text/javascript">
					    	    new tcal({ 'formname': 'PRG_LI_LAPSE_PROCESS_RPT', 'controlname': 'txtEndDate' });</script>dd/mm/yyyy
					    					</td>
				    </tr>

				    
				<tr>
					<td>&nbsp;</td>
					<td>
                        &nbsp;</td>
				</tr>

				<tr>
					<td></td>
					<td>
                        <asp:Button ID="butOK" runat="server" Text="OK" style="height: 26px"/>
                        <asp:Button ID="butClose" runat="server" Text="Close" />
                     </td>
				</tr>

			    </table>
			     </div></div></div>
            <div class="bottom-outer"><div class="bottom-inner">
            <div class="bottom"></div></div></div>                
        </div>      
    </div>

</div>
    </form>
</body>
</html>
