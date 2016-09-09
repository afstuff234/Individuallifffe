<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFileNotFound.aspx.vb" Inherits="WFileNotFound" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Custom Error</title>

<script language="javascript" type="text/javascript">
// <!CDATA[

function cmdClose_onclick() {
	    //window.close();
	    //self.close;
	    window.history.back(1);
}

// ]]>
</script>

</head>
<body>
<form id="Form1" style="height:200px; width:700px" runat="server">
    <table align="center" border="0" style="width:80%">
        <tr>
            <td></td>
        </tr>
        <tr style="background-color:Red">
            <td><h2>Error 404 - File or resource Not Found</h2></td>
        </tr>
        <tr>
            <td><h3>The resource you are looking for has been removed, had its name changed, or is temporarily unavailable.</h3></td>
        </tr>

        <tr>
            <td></td>
        </tr>
        <tr style="background-color:Silver;">
            <td><h4>Please See your system administrator</h4></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td><input type="button" name="cmdClose" ID="cmdClose" value="Close" runat="server" onclick="return cmdClose_onclick()" /></td>
        </tr>
    </table>
</form>
</body>
</html>

