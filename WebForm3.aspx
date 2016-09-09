<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WebForm3.aspx.vb" Inherits="WebForm3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Codes List</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/SJS_02.js">
    </script>
    <style type="text/css">
        .selScheme_Type
        {
            width: 392px;
        }
        .cmdOK, .cmdCancel
        {
            width: 75px;
        }
    </style>
</head>
<body style="background: url('') repeat left top; background-color: White;">

    <form id="Form1" name="Form1" action="#" runat="server" >

    <input type="hidden" id="hidFRM_NAME" name="hidFRM_NAME" runat="server" />
    <input type="hidden" id="hidCTR_VAL" name="hidCTR_VAL" runat="server" />
    <input type="hidden" id="hidCTR_TXT" name="hidCTR_TXT" runat="server" />
    
    <input type="hidden" id="hidRecNo" name="hidRecNo" runat="server" />
    <input type="hidden" id="hidCustID" name="hidCustID" runat="server" />
    <input type="hidden" id="hidCustName" name="hidCustName" runat="server" />

    <!-- content -->

<div id="div_header" align="center">    

    <table id="tbl_header" align="center">
        <tr>
            <td valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td colspan="4" align="left" style="background-color: Black; color:White; font-size:larger; height: 20px;">
                           <%=STRPAGE_TITLE%>                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>    

    <!-- content -->
    <div id="div_content" align="center">
    
        <table align="center" border="0" width="1000px" class="tbl_cont"> 
            <tr>
                <td align="left" colspan="4" valign="top" style="width: 1000px;">
                    <table align="center" border="0">
                        <tr>
       	                    <td align="left" colspan="2" valign="baseline"><asp:Label ID="lblAction" Text="Status:" runat="server"></asp:Label>
                   	            &nbsp;&nbsp;<asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="False" Width="50px"></asp:textbox></td>    
                            <td align="right" colspan="2" valign="baseline">&nbsp;&nbsp;Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />&nbsp;
                                <asp:Button ID="cmdSearch" Text="Search" Enabled="true" runat="server" />
            	            </td>
                
                        </tr>
                    </table>
                </td>
            </tr>
                  
            <tr>
                <td align="center" valign="top" colspan="4" style=" height:600px; width: 1000px;">
                    <table align="center" border="0">
                	    <tr>
                	        <td align="left" nowrap colspan="4"><asp:Label id="lblMessage" Text="Staus:" runat="server" Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
    	               	</tr>
                        <tr>
                            <td align="left" valign="top">Ref ID</td>
                            <td align="left" valign="top">Account Number</td>
                            <td align="left" valign="top" colspan="2">Account Name</td>
                        </tr>
                        <tr>
                            <td align="left" valign="top"><asp:TextBox ID="txtRecNo" Enabled="false" runat="server" MaxLength="18" Width="100px"></asp:TextBox></td>
                            <td align="left" valign="top"><asp:TextBox ID="txtCustID" Enabled="false" runat="server" MaxLength="30" Width="150px"></asp:TextBox></td>
                            <td align="left" valign="top"colspan="2"><asp:TextBox ID="txtCustName" Enabled="false" runat="server" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top">
                                <input type="button" id="cmdOK" name="cmdOK"  class="cmdOK" value="OK..." runat="server" 
                                    onclick="Sel_Func_OK('OK')" />
                                &nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="cmdCancel" name="cmdCancel" class="cmdCancel" value="Cancel..."
                                    onclick="Sel_Func_Cancel()" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="4" valign="top"><hr /></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" valign="top">
                            
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                                    GridLines="Both" Width="700px"
                                    DataKeyNames="MyFld_Rec_ID" HorizontalAlign="Left" PageSize="10"
                                    PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                    EmptyDataText="No data available..."
                                    ShowFooter="True">                        

                                    <RowStyle BackColor="#E3EAEB" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />

                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />

                                        <asp:BoundField DataField="MyFld_Rec_ID" HeaderText="Ref.No" HeaderStyle-HorizontalAlign="Left" 
                                            ReadOnly="True">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MyFld_Value" HeaderText="Code" HeaderStyle-HorizontalAlign="Left" 
                                            ReadOnly="True">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MyFld_Text" HeaderText="Code Name" HeaderStyle-HorizontalAlign="Left"
                                            ReadOnly="true" >
                                        </asp:BoundField>
                                    </Columns>

                                </asp:GridView>
                            
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
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td colspan="4" align="left" style="background-color: Black; color:White; font-size:medium; height: 20px;">                                                        
                           Copyright © - All rights reserved.                            
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
