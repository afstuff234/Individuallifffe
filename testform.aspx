<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testform.aspx.vb" Inherits="testform" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Individual Life Module</title>
    <link rel="Stylesheet" href="SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
    <script src="jquery.min.js" type="text/javascript"></script>
    <script src="jquery.simplemodal.js" type="text/javascript"></script>
        
    <script language="javascript" type="text/javascript">

        function doSomething2() {

            alert("Yes!");
        }


            function confirm(message, callback) {
                var modalWindow = document.getElementById("confirm");
                console.log(modalWindow);
                $(modalWindow).modal({
                    closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
                    position: ["20%", ],
                    overlayId: 'confirm-overlay',
                    containerId: 'confirm-container',
                    onShow: function(dialog) {
                        var modal = this;

                        $('.message', dialog.data[0]).append(message);

                        $('.yes', dialog.data[0]).click(function() {
                            modal.close(); $.modal.close();
                            callback(); // HERE IS THE CALLBACK
                            return true;
                        });
                    }
                });
            }
            function doSomething() {
                //mainController.deleteNode(nodeMoveMessage, url);
                //$("butTest").click();

                alert("Yes!");
            }



            $('#cmdSave_ASP').click(function(e) {
                e.preventDefault();

                confirm("Delete elements?", doSomething);
            });

              //  confirm("Delete elements?", doSomething);






            // calling jquery functions once document is ready
            $(document).ready(function() {

        });
        
        
        
    </script>
</head>
<body>
    <form id="form1" runat="server" >
    <asp:Button ID="cmdSave_ASP" runat="server" Text="Save" />
    <asp:Button ID="butTest" runat="server" Text="Test Me!" />
<div id='content'>
		<div id='confirm-dialog'>
			<h3>Confirm Override</h3>
			<p>A modal dialog override of the JavaScript confirm function. Demonstrates the use of the <code>onShow</code> callback as well as how to display a modal dialog confirmation instead of the default JavaScript confirm dialog.</p>
			<input type='button' name='confirm' class='confirm' value='Demo'/> or <a href='#' class='confirm'>Demo</a
            <asp:TextBox ID="txtOldNew" runat="server" ></asp:TextBox>
		</div>
		
		<!-- modal content -->
		<div id='confirm'>
			<div class='header'><span>Confirm</span></div>
			<div class='message'></div>
			<div class='buttons'>
				<div class='no simplemodal-close'>No</div><div class='yes'>Yes</div>
			</div>
		</div>
		<!-- preload the images -->
		<div style='display:none'>
			<img src='img/confirm/header.gif' alt='' />
			<img src='img/confirm/button.gif' alt='' />
		</div>
	</div>    </form>
</body>
</html>
