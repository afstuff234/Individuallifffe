<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WCustomPage.aspx.vb" Inherits="WCustomPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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

<body style="background-color:Aqua">

<form id="Form1" runat="server">

    <table border="0" align="center" style="background-color: White; width:50%; border-style: ridge; height: 200px;">
        <tr style="background-color:Red;">
            <td><h2>System Error.</h2></td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr>
            <td>
                An unexpected problem has occurred. <br />
                Performing your action again in a few moments will probably solve the problem.  <br />
                If it persists, we suggest you close this window and re-launch it again.            
            </td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
        <tr style="background-color:Silver;">
            <td><h4>Please See your system administrator</h4></td>
        </tr>
        <tr>
            <td><asp:Label id="lblError" text="Error..." runat="server"></asp:Label></td>
        </tr>
        
        <tr>
            <td><input type="button" name="cmdClose" id="cmdClose" value="Close" runat="server" onclick="return cmdClose_onclick()" /></td>
        </tr>
    </table>
</form>
</body>
</html>
