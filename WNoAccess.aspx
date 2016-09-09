<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WNoAccess.aspx.vb" Inherits="WNoAccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Custom Error</title>
    <link rel="Stylesheet" type="text/css" href="SS_ILIFE.css" />
</head>
<body>

<form id="Form1" runat="server">

    <br />
    <table align="center" border="0" style="background-color: White; width:50%; border-style: ridge; height: 200px;">
        <tr bgcolor="red">
            <td><h3>No Access or Access denied</h3></td>
        </tr>
        <tr>
            <td><p style="font-family: Trebuchet MS; font-size:medium;"><asp:Label id="lblmsg" Text="Status" runat="server"></asp:Label></p></td>
        </tr>
        <tr style="background-color:Silver;">
            <td><h4>Please See your system administrator</h4></td>
        </tr>
        <tr>
            <td><input type="button" visible="false" name="cmdClose" ID="cmdClose" value="Close" runat="server" /></td>
        </tr>
    </table>
</form>
</body>
</html>
