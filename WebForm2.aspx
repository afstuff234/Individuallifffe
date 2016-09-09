<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WebForm2.aspx.vb" Inherits="WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        
        body
        {
        	background-color: #f1f1f1;
        	font-family: Verdana;
        	font-size: medium;
        	font-weight: normal;
        }

        caption {
          background: #4682B4;
          color: #ffffff;
          font-size:large;
          font-weight:normal;
          margin: 1px;
          padding: 5px 3px;
          text-align:left; 
          text-transform: capitalize;
        }
        
        p 
        {
           /* background-color: Maroon; */
          color: #ffffff;
          font-size:large;
          font-weight:bold;
          margin: 1px;
          padding: 5px 3px;
          text-align:left; 
          text-transform:uppercase;
        }
        
        a { text-decoration: none}
        /* a:link { color: #ffffff; } */
        /* a:visited { color: #ffffff;} */
        a:hover { text-decoration:underline;}
        a:active { color: Highlight;}
        
        .td_menu_dynamic
        {
        	background-color: #DCDCDC;
        	border: 1px solid #c0c0c0;
        	/* height: 600px; */
            width: auto;
        }

    </style>
    
    <script language="javascript" type="text/javascript">
    
        //The getElementsByTagName method is equivalent to using the tags method on the all collection. 
        //For example, the following code shows how to retrieve a collection of div elements from the body element, 
        //first using the Dynamic HTML (DHTML) Object Model and then the Document Object Model (DOM). 

        //Using the DHTML Object Model: 
        //  var aDivs = document.body.all.tags("DIV");
        //Using the DOM: 
        //  var aDivs = document.body.getElementsByTagName("DIV");

        //document.getElementsById()
        //document.getElementsByTagName()
        //document.getElementsByName()
        
        //document.all.elementID.style.stylePropertyName
        function MyProc_Init()
        {
        //      e_div.style.visibility = 'visible';
        //      e_div.style.visibility = 'hidden';
        //      document.all.myDIV.style.width = "1024px";
        //      document.all.myDIV.style.border = "solid 2px black"
        //      document.all.myDIV.style.padding = "5px";
        //     var d = td.getElementsByTagName("div").item(0);

        // get all <DIV> tag objects in IE4/W3C DOMs
        //if (document.all) {
        //    var allDivs = document.all.tags("DIV");
        //} else if (document.getElementsByTagName) {
        //    var allDivs = document.getElementsByTagName("DIV")
        //}        

            //var odiv = window.document.getElementById("mydiv_title");
            //odiv.innerHTML = "Welcome to " + "Custodian&reg; and Allied Insurance Plc" + "";

            //if (document.getElementsByName("div_menu_01")) {
            if (document.all.div_menu_01.style) {
            //if (document.getElementById("div_menu_01")) {
                //alert("About to set object style property...");
                //document.all.div_menu_01.style.visibility = "visible";
                //document.all.div_menu_01.style.visibility = "hidden";
                //document.all.div_menu_01.style.display = "";
                document.all.div_menu_01.style.display = "none";
                //document.getElementById("div_menu_01").style.display = "none";
            }

            //if (document.getElementsByName("div_menu_02")) {
            //if (document.all.div_menu_02.style) {
            if (document.getElementById("div_menu_02")) {
                //alert("About to set object style property..");
                //document.all.div_menu_02.style.visibility = "visible";
                //document.all.div_menu_02.style.visibility = "hidden";
                //document.all.div_menu_02.style.display = "";
                //document.all.div_menu_02.style.display = "none";
                document.getElementById("div_menu_02").style.display = "none";
            }

        }
    </script>
</head>
<body onload="MyProc_Init()">
    <form id="form1" runat="server">
    
    <div align="center" style="width: 1000px;">
        <table align="center" border="1" cellpadding="1" cellspacing="2">
            <caption>MENU DEMONSTRATION</caption>
            <tr>
                <!-- menu -->
                <td align="left" valign="top" class="td_menu_dynamic">
                    <p><a href="#" onclick="MyProc_Init(); document.all.div_menu_01.style.display='';">Accounts MENU</a></p>
                    <div id="div_menu_01">
                        <table align="center" border="0" width="100%;">
                            <caption>Accounts MENU Sign-In</caption>
                            <tr>
                                <td valign="top">
                                    <input type="button" id="Mnu_01_cmdNew" name="Mnu_01_cmdNew" value="New Data" />&nbsp;&nbsp;
                                    <input type="button" id="Mnu_01_cmdSave" name="Mnu_01_cmdSave" value="Save Data" />&nbsp;&nbsp;
                                    <input type="button" id="Mnu_01_cmdDelete" name="Mnu_01_cmdDelete" value="Delete Data" />&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top"><hr /></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Account No:&nbsp;
                                    <input type="text" id="Mnu_01_txtName" name="Mnu_01_txtName" value="101-001" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top"><a href="#">Menu-1 Submenu-1</a></td>
                            </tr>
                            <tr>
                                <td valign="top"><a href="#">Menu-1 Submenu-2</a></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <!-- menu -->
                <td align="left" valign="top" class="td_menu_dynamic">
                    <p><a href="#" onclick="MyProc_Init(); document.all.div_menu_02.style.display='';">Email MENU</a></p>
                    <div id="div_menu_02">
                        <table align="center" border="0">
                            <caption>Email MENU Sign-In</caption>
                            <tr>
                                <td valign="top">
                                    <input type="button" id="Mnu_02_cmdNew" name="Mnu_02_cmdNew" value="New Data" />&nbsp;&nbsp;
                                    <input type="button" id="Mnu_02_cmdSave" name="Mnu_02_cmdSave" value="Save Data" />&nbsp;&nbsp;
                                    <input type="button" id="Mnu_02_cmdDelete" name="Mnu_02_cmdDelete" value="Delete Data" />&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td valign="top"><hr /></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Email Address:&nbsp;
                                    <input type="text" id="Mnu_02_txtEmail" name="Mnu_02_txtEmail" value="ola@yahoo.com" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top"><a href="#">Menu-2 Submenu-1</a></td>
                            </tr>
                            <tr>
                                <td valign="top"><a href="#">Menu-2 Submenu-2</a></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
