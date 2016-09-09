<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Calendar1.aspx.vb" Inherits="Calendar1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendar</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="Script/SJS_02.js">
    </script>

    <script language="javascript" type="text/javascript">
       
        function MyCheck()
        {
            alert("On select change event...");
            
            var myform = window.document.forms[0];
            for (var i = 0; i < myform.elements.length; i++) {
          	  myobj = myform.elements[i];
                alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id);
                if (myform.elements[i].type == "text") {
                    myform.elements[i].value = ""
                }
                if (myform.elements[i].type == "select-one" || 
                    myform.elements[i].type == "checkbox" || 
                    myform.elements[i].type == "submit") {
                }
            }

        }
        
    </script>
    
    <style type="text/css">
        .cls_cmdOK
        {
            width: 90px;
        }
        .cls_cmdCancel
        {
            width: 90px;
        }
        .cls_hidDate
        {
            width: 100px;
        }
    </style>
</head>

<body onclick="InitModal_Cal();">
    <form id="Form1" name="Form1" action="#" runat="server" onsubmit="return false">
    
    <input type="hidden" id="hidFRM_NAME" name="hidFRM_NAME" runat="server" />
    <input type="hidden" id="hidCTR_VAL" name="hidCTR_VAL" runat="server" />
    <input type="hidden" id="hidCTR_TXT" name="hidCTR_TXT" runat="server" />
    
    <div align="center">
    <table align="center" border="0" class="tbl_cal">
        <tr>
            <td align="left" colspan="2" valign="top">&nbsp;</td>            
        </tr>    
        <tr>
            <td align="center" colspan="2" valign="top">
                <asp:Calendar id="Cal1" name="Cal1" runat="server" 
                    SelectionMode="DayWeekMonth"
                    Font-Name="Verdana;Arial" Font-Size="12px"
                    Height="120px" Width="270px"
                    TodayDayStyle-Font-Bold="True"
                    DayHeaderStyle-Font-Bold="True"
                    OtherMonthDayStyle-ForeColor="gray"
                    TitleStyle-BackColor="#3366ff"
                    TitleStyle-ForeColor="white"
                    TitleStyle-Font-Bold="True"
                    SelectedDayStyle-BackColor="#ffcc66"
                    SelectedDayStyle-Font-Bold="True"
                    
                    PrevMonthText="<img src='images/prev_page.gif' />"
                    NextMonthText="<img src='images/next_page.gif' />"
                    NextPrevFormat="CustomText"
                    
                    NextPrevStyle-ForeColor="white"
                    NextPrevStyle-Font-Size="10px"
                    SelectorStyle-BackColor="#99ccff"
                    SelectorStyle-ForeColor="navy"
                    SelectorStyle-Font-Size="9px"

                    SelectWeekText = "wk"
                    SelectMonthText = "month"
                    Visible="true" OnSelectionChanged="MySel_Changed">

                    <SelectedDayStyle BackColor="#FFCC66" Font-Bold="True"></SelectedDayStyle>
                    <SelectorStyle BackColor="#99CCFF" Font-Size="9px" ForeColor="Navy"></SelectorStyle>
                    <TodayDayStyle Font-Bold="True"></TodayDayStyle>
                    <OtherMonthDayStyle ForeColor="Gray"></OtherMonthDayStyle>
                    <NextPrevStyle Font-Size="10px" ForeColor="White"></NextPrevStyle>
                    <DayHeaderStyle Font-Bold="True"></DayHeaderStyle>
                    <TitleStyle BackColor="#3366FF" Font-Bold="True" ForeColor="White"></TitleStyle>

                </asp:Calendar>
            </td>
        </tr>

        <tr>
            <td align="left" colspan="2" valign="top"><hr />
            </td>            
        </tr>    
        <tr>
            <td align="center" colspan="2" valign="top">
                <input type="button" id="cmdOK" name="cmdOK" value="OK" class="cls_cmdOK" onclick="myFunc_OK_Cal('OK')" />
                &nbsp;&nbsp;<input type="button" id="cmdCancel" name="cmdCancel" value="Cancel" class="cls_cmdCancel" onclick="myFunc_Cancel()" />
            </td>            
        </tr>    

        <tr>
            <td align="left" colspan="2" valign="top"><hr />
            </td>            
        </tr>    
        <tr>
            <td align="left" colspan="2" valign="top">
                &nbsp;&nbsp;<input type="text" id="hidDate" name="hidDate" runat="server" disabled="disabled" class="cls_hidDate" />
                &nbsp;&nbsp;<asp:Label ID="lblDate" runat="server" Width="100px"></asp:Label>
            </td>            
        </tr>    

     </table>
    </div>
    </form>
</body>
</html>
