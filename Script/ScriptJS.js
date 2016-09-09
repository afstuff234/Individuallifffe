//"₦"

//var mynav = navigator.userAgent.toLowerCase();

var myWin;
var popUpDate;
var popUpCode;
var popUpHelp;

var iw;
var ih;

var icordX;
var icordY;

var Pcnt;
var myobj;
var myprm;

var str_browser_ver = navigator.appVersion;
var str_browser_name = navigator.appName;

//    var button = document.getElementById(/* button client id */);
//    button.click();

//i. In your code file (assuming you are using C# and .NET 2.0 or later) add the following Interface to your Page Class to make it look like
//public partial class Default : System.Web.UI.Page, IPostBackEventHandler{}
//ii. This should add (using Tab-Tab) this function to your code file:
//public void RaisePostBackEvent(string eventArgument) { }
//iii. In your onclick event in Javascript write the following code:
// var pageId = '<%=  Page.ClientID %>';
// __doPostBack(pageId, argumentString);

//This will call the 'RaisePostBackEvent' method in your code file with the 'eventArgument' as the 'argumentString' you passed from the Javascript. Now, you can call any other event you like.

function ShowPopup_Message(strMSG) {
    try {
        alert(strMSG);
    }
    catch (ex) {
        alert("Error has occured!. Reason: " + ex.message);
    }
}

function MyForm_Load() {
    //document.Form1.autocomplete = "off";
    document.forms["Form1"].autocomplete = "off";
    //alert("In LOGIN form load. \nAuto Complete is turn off...");

    //alert("Browser Name: \t" + str_browser_name + "\n\nBrowser Version: \t" + str_browser_ver);

}

function MyFrm_Load_Life() {
    //document.location.href = "m_menu.aspx?menu=home";
    document.location.href = "LoginP.aspx";
}


function SelControl_OnChg(objSelect_Ctrl, objValue_Ctrl, objText_Ctrl) {
    try {

        // onchange="SelControl_OnChange()"
        // var myf=document.forms['Form1'];myf.txtCustType.value=myf.selCustType.options[myf.selCustType.selectedIndex].value;

        //objFrm = document.forms['Form1'];
        ////objFrm.txtCustType.value = objFrm.selCustType.selectedIndex;
        //objFrm.txtCustType.value = objFrm.selCustType.options[objFrm.selCustType.selectedIndex].value;

        if ((objSelect_Ctrl.type == "select-one") && (objValue_Ctrl.type == "text")) {
            objValue_Ctrl.value = objSelect_Ctrl.options[objSelect_Ctrl.selectedIndex].value;
        }
        if ((objSelect_Ctrl.type == "select-one") && (objText_Ctrl.type == "text")) {
            objText_Ctrl.value = objSelect_Ctrl.options[objSelect_Ctrl.selectedIndex].text;
        }
    }
    catch (ex) {
        alert("Error has occured!. Reason: " + ex.message);
    }
}


function sel_change(sel_ctrl) {
    if (sel_ctrl.type.indexOf("select") > 0) {
        switch (sel_ctrl.selectedIndex) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}


// <select id="cboProduct" class="selProduct" runat="server"
//  onchange="javascript:setSelected(this,this.value,this.form.txtProduct_Num)">
// </select>
// &nbsp;<asp:TextBox ID="txtProduct_Num" Enabled="true" EnableViewState="true" runat="server" MaxLength="10" Width="80"></asp:TextBox>

//onchange="javascript:setSelected(this,this.value,this.form.txtProduct_Num)">                
// Utility function to set a SELECT element to one value
function setSelected(obj_select, str_value, objText_Ctrl) {

    for (var i = 0; i < obj_select.options.length; i++) {
        if (str_value == "0" || str_value == "*") {
            break;
            return false;
        }
        if (obj_select.options[i].value == str_value) {
            obj_select.selectedIndex = i;
            objText_Ctrl.value = obj_select.options[obj_select.selectedIndex].value;
            objText_Ctrl.value = str_value;
            break;
        }
    }
    return true;
}


function func_mytoday() {
    var mydays = new Array("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
    var mytoday = new Date();
    return mydays[mytoday.getDay()];
}

function saveCookie(myvalue, myday) {
    var myexpires = "";

    if (mydays) {
        var date = new Date();
        date.setTime(date.getTime() + (mydays * 24 * 60 * 60 * 1000));
        myexpires = "; expires = " + date.toGMTString();
    }

    var mycookie = "cookieName = " + myvalue + myexpires + "; path=/";
    document.cookie = mycookie;

}

function readCookie() {
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        var x = c.charAt(0);
        var y = c.substring(1, c.length);
    }
}


function JSDO_RETURN(pURL) {
    try {
        myprm = confirm("*** WARNING: You are About to Close this Page *** " +
         "\n\n\t+++ OK TO CLOSE THIS PAGE ? +++");
        if (myprm == true) {
            window.location.href = pURL;
        }
    }
    catch (ex_err) {
        alert("Error has occured. Reason: " + ex_err.message);
    }
}

function JSDO_LOG_OUT() {

    myprm = confirm("*** DO YOU WANT TO LOG OUT NOW ?");
    if (myprm == true) {
        document.forms["Form1"].elements["txtAction"].value = "Log_Out";
        document.forms["Form1"].submit();
    }
    else {
        Pcnt = 0;
        //alert("Log Out Cancelled!");
    }
}

function openwin(WinName) {
    window.open(WinName, "_blank", "resizable=no," +
          "scrollbars=yes,toolbar=no,location=no,directories=no,status=no," +
          "menubar=no,maximize=no,width=420,height=300,top=100,left=300");
}

function windowTitle(sWinMsg) {
    parent.document.title = sWinMsg;
}

function JSNew_ASP() {

    Pcnt = 0;

    //alert("Welcome to new button...");

    var myform = window.document.forms["Form1"];
    //for (i = 0; i < document.forms["Form1"].length; i++) {
    //  myobj = document.forms["Form1"].elements[i];
    for (i = 0; i < myform.elements.length; i++) {
        myobj = myform.elements[i];
        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id + "\nStatus: " + myobj.Checked);
        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id);      
        if (myobj.id == "cmdNew_ASP" || myobj.id.substring(24, 34) == "cmdNew_ASP") {
            Pcnt = Pcnt + 1;
        }

    }

    if (Pcnt > 0) {
        myprm = confirm("THIS FORM/PAGE CONTENT WILL BE CLEARED?" +
	        "\nNOTE: Any unsave data will be discarded." +
	        "\n\nOK to clear the content of this form/page?");
        if (myprm == true) {
            document.forms["Form1"].elements["txtAction"].value = "New";
            document.forms["Form1"].submit();
        }
        else {
            Pcnt = 0;
            //alert("Current page record retained!");
        }
    }
}

function JSSave_ASP() {

    Pcnt = 0;

    //id=TabContainer1_TabPanel1_cmdSave_ASP

    //    var myform = window.document.forms[0];
    //    for (var i = 0; i < myform.elements.length; i++) {
    //  	  myobj = myform.elements[i];
    //      alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id);
    ////    if (myform.elements[i].type == "text") {
    ////        myform.elements[i].value = ""
    //    }

    //alert("save button clicked...");

    //        obj_sel = document.forms(0).elements["TabContainer1_TabPanel1_" + pvobj_sel];

    //  eval('var myform = document.' + formName + ';');

    var myform;
    myform = document.forms['Form1'];
    if (!myform) {
        myform = window.document.forms["Form1"];
    }
    if (!myform) {
        myform = document.Form1;
    }
    if (!myform) {
        myform = document.forms(0);
    }
    if (!myform) {
        alert('Unable to obtain the page Form. Operation cancelled...');
        return false;
    }

    //for (i = 0; i < document.forms["Form1"].length; i++) {
    //  myobj = document.forms["Form1"].elements[i];
    for (i = 0; i < myform.elements.length; i++) {
        myobj = myform.elements[i];
        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id + "\nStatus: " + myobj.Checked);
        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id);
        if (myobj.id == "cmdSave" || myobj.id == "cmdSave_ASP" || myobj.id == "cmdSave_Next" ||
           myobj.id.substring(24, 34) == "cmdNew_ASP" ||
           myobj.id.substring(24, 31) == "cmdSave" ||
           myobj.id.substring(24, 35) == "cmdSave_ASP" ||
           myobj.id.substring(24, 37) == "cmdDelete_ASP" ||
           myobj.id.substring(24, 36) == "cmdPrint_ASP" ||
           myobj.id.substring(24, 36) == "cmdSave_Next") {
            //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id);
            Pcnt = Pcnt + 1;
        }
        //else{alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id + "\nSave_ASP: " + myobj.id.substring(24,34));}
    }

    if (Pcnt > 0) {
        //myprm = confirm("Are you sure you want to save this record to database ?");
        myprm = confirm("Are you sure you want to save this data ?");
        //alert("My value: " + myprm);

        if (myprm == true) {
            //document.forms["Form1"].elements["txtAction"].value = "Save";
            //document.forms["Form1"].submit();
            myform.elements["txtAction"].value = "Save";
            myform.submit();
        }
        else {
            Pcnt = 0;
            //alert("Current page record not save!");
        }
    }
}



function JSDelete_ASP() {
    Pcnt = 0;

    var myform = window.document.forms["Form1"];
    //for (i = 0; i < document.forms["Form1"].length; i++) {
    //  myobj = document.forms["Form1"].elements[i];
    for (i = 0; i < myform.elements.length; i++) {
        myobj = myform.elements[i];
        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id + "\nStatus: " + myobj.Checked);
        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id);
        if (myobj.id == "cmdDelete" || myobj.id == "cmdDelete_ASP" || myobj.id.substring(24, 37) == "cmdDelete_ASP") {
            Pcnt = Pcnt + 1;
        }

    }

    if (Pcnt > 0) {
        myprm = confirm("WARNING: Record will be permanently deleteed from the database!" +
          "\nAre you sure you want to delete this data?");
        if (myprm == true) {
            document.forms["Form1"].elements["txtAction"].value = "Delete";
            document.forms["Form1"].submit;
        }
        else {
            Pcnt = 0;
            alert("Current page record not deleted!");
        }
    }
}


function JSDelItem_ASP() {

    Pcnt = 0;

    //      if V.type = "checkbox" and right(V.id,6) = "chkSel" and (left(V.id,9) = "DataGrid1" or left(V.id,9) = "GridView1") then
    //         'msgbox "Found Control Type: " &  V.type & vbcrLF & "Found Control ID: " & V.id & vbcrLF & "Status: " & V.Checked
    //         if V.Checked = True then
    //            P = P + 1
    //         end if
    //      end if

    var myform = window.document.forms["Form1"];
    //for (i = 0; i < document.forms["Form1"].length; i++) {
    //  myobj = document.forms["Form1"].elements[i];
    for (i = 0; i < myform.elements.length; i++) {
        myobj = myform.elements[i];
        var myobjid = myobj.id;

        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id + "\nStatus: " + myobj.Checked);
        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id + "\nCharacter Extracted: " + myobjid.substring(0,9));
        //if (myobj.id == "chkSel" && myobj.type == "checkbox" && myobj.checked == true && (myobjid.substring(0,9) == "DataGrid1" || myobjid.substring(0,9) == "GridView1")) { 

        //var xx_xx = myobj.type;
        //if (xx_xx == "text" || xx_xx == "radio" || xx_xx == "checkbox" || xx_xx.indexOf("select") > 0) 
        //{
        //    alert("Control is either text box or radio button or check box or drop down select control...");
        //}

        if (myobjid.substring(0, 9) == "GridView1" && myobj.type == "checkbox" && myobj.checked == 1) {
            Pcnt = Pcnt + 1;
        }

        //if (myobj.id == "chkSel" && myobj.type == "checkbox" && myobj.checked == true && (myobjid.substring(0,9) == "GridView1")) { 
        //    Pcnt = Pcnt + 1;
        //}

    }

    if (Pcnt > 0) {
        myprm = confirm("WARNING: Record will be permanently deleteed!" +
          "\nAre you sure you want to delete the selected item(s)?");
        if (myprm == true) {
            document.forms["Form1"].elements["txtAction"].value = "Delete_Item";
            document.forms["Form1"].submit;
        }
    }
    else {
        Pcnt = 0;
        alert("You must select an item to delete. Nothing is selected to Delete...");
    }

}


function cmdDelete_HTML_OnClick() {

    Pcnt = 0;
    var V;

    //for (each V in document.forms["Form1"]) {
    //  if (V.id == "cmdDelete_ASP") {
    //     Pcnt = Pcnt + 1;
    //  }
    //}

    for (i = 0; i < document.forms["Form1"].length; i++) {
        myobj = document.forms["Form1"].elements[i];
        //alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id + "\nStatus: " + myobj.Checked);
        alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id);
        if (myobj.id == "cmdDelete_HTML") {
            Pcnt = Pcnt + 1;
        }

    }

    if (Pcnt > 0) {
        myprm = confirm("WARNING: Record will be permanently deleteed!" +
          "\nAre you sure you want to delete this data?");
        if (myprm == true) {
            document.forms["Form1"].elements["txtAction"].value = "Delete";
            document.forms["Form1"].submit;
        }
        else {
            Pcnt = 0;
            alert("Current page record not deleted!");
        }
    }
}


function doSave_JS() {
    myprm = confirm("Are you sure you want to save data?");
    if (myprm == true) {
        //var myfield;
        //myfield = document.forms[0].txtAction;
        //myfield = document.forms[0].elements["txtAction"];
        //myfield.value = "Saving...";
        //alert("Saving Record...");
        document.forms["Form1"].elements["txtAction"].value = "Save";
        //document.forms["Form1"].reset;
        document.forms["Form1"].submit();
        //document.forms["Form1"].cmdSave.click();
        //document.forms["Form1"].handleEvent();
        //document.forms["Form1"].txtAction.select()
        //alert("Record saved successfully...");
    }
    else {
        //alert("Current record not save!");
    }
}



function myOpenPage(sPopURL) {

    var myWinx;

    iw = 960;
    ih = 500;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;

    myWinx = window.open(sPopURL, "", "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no,modal=yes,alwaysRaised=yes");
    //myWinx = window.open(sPopURL, "fraDetails", "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    myWinx.focus;

}

function jsDoPopNewN(PopPage) {

    iw = 950;
    ih = 550;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;
    if (icordY < 0) {
        icordY = 50;
    }

    myWin = window.open(PopPage, "", "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no,modal=yes,alwaysRaised=yes");
    //myWin = window.open(PopPage, "fraDetails", "width=" + iw + ",height=" + ih & ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    myWin.focus;

}

function jsMyPopUpN(PopPage) {

    iw = 960;
    ih = 600;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;
    if (icordY < 0) {
        icordY = 50;
    }

    myWin = window.open(PopPage, null, "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no,modal=yes,alwaysRaised=yes");
    //myWin = window.open(PopPage, "fraDetails", "width=" + iw + ",height=" + ih & ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    myWin.focus;

}

function jsDoPopNew_Small(PopPage) {

    iw = 800;
    ih = 400;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;
    if (icordY < 0) {
        icordY = 50;
    }

    myWin = window.open(PopPage, "", "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no,modal=yes,alwaysRaised=yes");
    //myWin = window.open(PopPage, "fraDetails", "width=" + iw + ",height=" + ih & ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    myWin.focus;

}

function jsDoPopNew_Big(PopPage) {

    iw = 960;
    ih = 500;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;

    myWin = window.open(PopPage, "", "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no,modal=yes,alwaysRaised=yes");
    //myWin = window.open(PopPage, "fraDetails", "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    myWin.focus;

}

function jsDoPopNew_Full(PopPage) {

    iw = 960;
    ih = 500;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;

    //myWin = window.open(PopPage, null, "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    //myWin = window.open(PopPage, null, "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=1,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    myWin = window.open(PopPage, "", "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=1,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    myWin.focus;

}

function DoPopNewN(PopPage, PageWidth, PageHeight) {

    iw = PageWidth;
    ih = PageHeight;

    //iw = 850;
    //ih = 500;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;
    if (icordY < 0) {
        icordY = 50;
    }

    myWin = window.open(PopPage, null, "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no,modal=yes,alwaysRaised=yes");
    //myWin = window.open(PopPage, "fraDetails", "width=" + iw + ",height=" + ih & ",left=" + icordX + ",top=" + icordY + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
    myWin.focus;

}

function OpenCalendar(idname) {
    iw = 300;
    ih = 270;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;
    if (icordY < 0) {
        icordY = 50;
    }

    popUpDate = window.open('/abswund/Calendar.aspx?formname=' + document.forms[0].name +
  '&id=' + idname + '&selected=' + document.forms[0].elements[idname].value,
				'popupcal', "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",status=no");

    popUpDate.focus;
}

function DoPopCodes(popString) {

    iw = 750;
    ih = 450;

    icordX = (screen.width - iw) / 2;
    icordY = (screen.height - ih) / 2;
    icordY = ((screen.height - ih) / 2) - 50;
    if (icordY < 0) {
        icordY = 50;
    }

    popUpCode = window.open('/abswund/PopupList.aspx?formname=' + document.forms[0].name +
	             popString, 'Childe', "width=" + iw + ",height=" + ih + ",left=" + icordX + ",top=" + icordY + ",status=yes");
    popUpCode.focus;
}

function SetDate(formName, id, newDate) {
    eval('var theform = document.' + formName + ';');
    popUpDate.close();
    theform.elements[id].value = newDate;
}

function SetCode(formName, CodeID, NameID, NewCode, NewName, PostBack) {
    eval('var theform = document.' + formName + ';');
    popUpCode.close();
    if (CodeID != '') { theform.elements[CodeID].value = NewCode };
    if (NameID != '') { theform.elements[NameID].value = NewName };
    if (PostBack != '') { __doPostBack(PostBack, '') };
}


/*
this function is used to verify whether the 'Enter' key is pressed.
*/

function keydownfn(e, evntHdlrName, formName) {
    var nav4;
    var keyPressed;

    //check if the brower is Netscape Navigator 4 or not
    nav4 = window.Event ? true : false;

    //if browser is Navigator 4, the key pressed is called <event object>.which else it's called <event object>.keyCode
    keyPressed = nav4 ? e.which : e.keyCode;

    if (keyPressed == 13) {
        if (formName == "") formName = 0;
        document.forms[formName].submit();
    }
    return true;
}

function MyFunc_KeyPress(event, href) {
    // SEE ABOVE FOR HTML TAG
    var keyCode;
    if (typeof (event.keyCode) != "undefined") {
        keyCode = event.keyCode;
    }
    else {
        keyCode = event.which;
    }

    if (keyCode == 13) {
        window.location = href;
    }
}

function myOpen_Frame(surl) {
    var myurl = null;

    if (surl != "") {
        myurl = surl;
        parent.frames["content_frame"].location = myurl;
    }
    else {
        alert("You must pass in valid URL...");
    }

}


function jumpToPage(obj) {
    var url = obj.options[obj.selectedIndex].value;
    if (url == "none") {
        alert("You must select a item from this list...");
    }
    else {
        parent.frames["content_frame"].location = url;
    }
}


function myHelp(sPG) {

    //window.showModalDialog("URL"[, arguments] [, features])
    //window.showModelessDialog("URL"[, arguments] [, features])
    //    

    //The first parameter is the URL name of the page to open.
    //
    //The second parameter is the optional parameters to pass to the dialog window.
    //To retrieve the argument parameters from the dialog window use the following:
    //	var returnFunc = window.dialogArguments
    //
    //The third parameter is optional. The IE Dialog Box Window Features are:
    //
    //Feature	 Type	 	Default	 Description
    //=====================================================
    //center	 Boolean	 yes	 Whether to center dialog box (overridden by dialogLeft and/or dialogTop).
    //dialogHeight	 Length		 varies	 Outer height of the dialog box window. IE4 default length unit is em; IE5 is pixel (px).
    //dialogLeft	 Integer	 varies	 Pixel offset of dialog box from left edge of screen.
    //dialogTop	 Integer	 varies	 Pixel offset of dialog box from top edge of screen.
    //dialogWidth	 Length		 varies	 Outer width of the dialog box window. IE4 default length unit is em; IE5 is pixel (px).
    //help		 Boolean	 yes	 Display Help icon in title bar.
    //resizable	 Boolean	 no	 Dialog box is resizable (IE5+ only).
    //status	 Boolean	 varies	 Display statusbar at window bottom (IE5+ only). Default is yes for untrusted dialog box; no for trusted dialog box
    //

    var defaultData = "xyz";

    //window.showHelp();
    var odlg = window.showModalDialog(sPG, defaultData, "dialogHeight:650px; dialogWidth:960px; help:yes; center:yes; resizable:yes; status:yes");
    //odlg.document.forms[0].txtName.value = "Message from Dialog boz";

    //// var returnFunc = window.dialogArguments;

    //window.showModelessDialog();

}

function myMsg() {

    var odiv = window.document.getElementById("mydiv_title");
    odiv.innerHTML = "Welcome to " + "Custodian&reg; and Allied Insurance Plc" + "";

    var otbl = window.document.getElementById("mytbl_subitem");

    //Create and insert table row
    var newRow;
    newRow = otbl.insertRow();

    //var myTR = document.createElement("TR");

    // Note: parameter of 0 inserts at first cell position

    //Create and insert table column
    var newCell = newRow.insertCell(0);
    newCell.innerHTML = "Custodian is a company committed to providing quality insurance services...";
    //newCell.style.backgroundColor = "salmon";
    //newCell.style.fontName = "Courier";
    //newCell.style.fontSize = "20";

}


//  ActiveXObject
//var dict = new ActiveXObject("Scripting.Dictionary");
//var objRef = new ActiveXObject(appName.className[, remoteServerName])
//
//For example, to obtain a reference to an Excel worksheet, use this constructor:
//var mySheet = new ActiveXObject("Excel.Sheet");

//This JScript syntax is the equivalent of the VBScript CreateObject() method.
//For more details, visit http://msdn.microsoft.com/scripting/jscript/doc/jsobjActiveXObject.htm.
//
//
//      appName         className
//      -------------------------
//      Excel           Application
//      Excel           Sheet
//      Word            Application


// To open a word document with javascript
function openWord(strFilePath) {
    var yourSite;
    yourSite = "http://www.yoursite.com";
    openWordDocPath(yourSite + strFilePath);
}

function openWordDocPath(strLocation) {
    var objWord;
    objWord = new ActiveXObject("Word.Application");
    objWord.Visible = true;
    objWord.Documents.Open(strLocation);
}

// The following code segment lets the user open a Word document directly:
function openWordDoc() {
    var pause = 0;
    var wdDialogFileOpen = 80;
    var wdApp = new ActiveXObject("Word.Application");
    var dialog = wdApp.Dialogs(wdDialogFileOpen);
    var button = dialog.Show(pause);

}

// To open an excel document with javascript
function openExcel(strFilePath) {
    var yourSite;
    yourSite = "http://www.yoursite.com";
    openExcelDocPath(yourSite + strFilePath, false);
}

function openExcelDocPath(strLocation, boolReadOnly) {
    var objExcel;
    objExcel = new ActiveXObject("Excel.Application");
    objExcel.Visible = true;
    objExcel.Workbooks.Open(strLocation, false, boolReadOnly);
}

// The following code segment lets the user open a Word document directly:
function openExcelDoc() {
    var pause = 0;
    var wdDialogFileOpen = 80;
    var wdApp = new ActiveXObject("Excel.Application");
    var dialog = wdApp.Dialogs(wdDialogFileOpen);
    var button = dialog.Show(pause);
}

//ActiveXObject FileSystemObject

//var fsObj = new ActiveXObject("Scripting.FileSystemObject")

function saveLocalData(theData) {
    var fsObj = new ActiveXObject("Scripting.FileSystemObject");
    var theFile = fsObj.CreateTextFile("c:\\mytext.txt", true);
    theFile.WriteLine(theData);
    theFile.Close();
}

//function buttonOver() {
//alert("buttonOver() called.");
//this.className = "active";
//var currentTab = this.title;
//}
//function buttonOut() {
//alert("buttonOut() called.");
//this.className = "";
//}

//var theForm = document.forms['Form1'];
//if (!theForm) {
//    theForm = document.Form1;
//}
//if (!theForm) {
//    alert('Unable to obtain the page Form. Operation cancelled...');
//}


// document.forms['form1'].textCtrl.disabled = true;

// <script language="javascript">
//   window.keydown = myfunc();
//   function myfunc()
//   {
//   vat mykeycode = window.event.keyCode;
//   var elementType = window.event.srcElement.type;
//   window.event.srcElement.type == "password";
//   var buttonVal = window.event.srcElement.value;
//   }
// </script>

//<label>Make / Model</label> 
//<select name="makeid" id="makeid" size="1" style="width:128px;" 
//  onchange="javascript:selVal_(this.value)">
//  <option value="0" title="Any Make">Any Make</option>
//  <option value="01" title="Toyota">Toyota</option>
//  <option value="02" title="Honda">Honda</option>
//  <option value="03" title="Nissan">Nissan</option>
//</select>

//for ( var idx = 0; idx < mySelectList.options.length; )
//    {
//        mySelectList.options[0] = null;
//        mySelectList.options[0].value == 'CR_NULL';
//        idx++;
//    }

// REMOVE FROM LIST BOX
//for ( var idx = 0; idx < mySelectList.options.length; )
//   {
//       if ( mySelectList.options[idx].selected )
//           mySelectList.options[idx] = null;
//       else
//           idx++;
//   }
//

//		document.getElementById('ad-box-iframe').height = myheight ;
//		document.getElementById('ad-box-iframe').width = mywidth ;

// GET NEW INSERTED RECORD IDENTITY VALUE
// --------------------------------------
//DECLARE @CurrUser int

//INSERT INTO table1(field1, AddedDate) VALUES (@CurrUser, GETDATE())

//SET @CurrUser = @@IDENTITY
//IF @@ERROR > 0
//      BEGIN
//      RAISERROR ('Insert of User failed', 16, 1)
//      RETURN 99
//      END
//    END

//input type='button'  value='Pay Now!'  name='pay' alt="Pay "  onClick="if (doValidateCard(this.form)) {return doSubmit(this.form)}else{alert('The card is not valid for this transaction')};"


//parts=window.location.hash.split("=")

//document.body.className = document.body.className.replace('no-js', 'js');
//document.body.offsetHeight;


//    document.Form1.autocomplete = "off";

//    window.location.href = sURL;
//    window.location.replace( sURL );
//    window.location.reload( false );

//		var e_div = document.createElement("DIV");
//		e_div.id = 'mydiv';
//      e_div.style.visibility = 'visible';
//      e_div.style.visibility = 'hidden';
//		document.body.appendChild(e_div);

//  if (typeof(d_date) == 'number')
//  if (typeof(d_date) == 'string')

//try {
//  var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
//  document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));  
//}
//catch(e){};

//    var e = document.createElement('script');
//    e.setAttribute('language', 'javascript');
//    e.setAttribute('type', 'text/javascript');
//    e.setAttribute('src',
//       (("https:" == document.location.protocol) ? "https://s3.amazonaws.com/" : "http://") + "static.chartbeat.com/js/chartbeat.js");
//    document.body.appendChild(e);
//
//    document.body.removeChild(document.getElementById("ripple"))

// var dcw = document.documentElement.clientWidth;
// var dch = document.documentElement.clientHeight;
// var bcw = document.body.clientWidth;
// var bch = document.body.clientHeight


// Using the [this] keyword
// ------------------------
//onlick="myfunc(this)"  - pass in the parameter as object 
//onlick="myfunc(this.value)" - pass in the parameter as value
//onlick="myfunc(this.form)" - pass in the form as object
//onlick="myfunc(this.form.control_name)" - pass in the form control_name as object
//onlick="myfunc(this.form.control_name.value)" - pass in the value of form control_name
// example
//File to be uploaded:
//<INPUT TYPE="file" SIZE=40 NAME="fileToGo">
//<INPUT TYPE=”button” VALUE="View Value" onClick="alert(this.form.fileToGo.value)">


// SCAN THROUGH A FORM ELEMENTS
// ============================
//The following code fragment shows the form.elements[] property at work in a
//for repeat loop that looks at every element in a form to set the contents of text
//fields to an empty string. 
//The script cannot simply barge through the form and set every element’s content to 
//an empty string because some elements may be buttons, which don’t have a value property
//that you can set to an empty string.
//

//To find out how many FORM objects are in the current document, use
//  var frm_cnt = document.forms.length;

// SCAN THROUGH A FORM AND ITS TAG
// -------------------------------
// EXAMPLE-1
// ---------
//theForm = document.forms['form1'];
//    for ( idx = 0; idx < theForm.elements.length; ++idx )
//        alert ( theForm.elements[idx].name + " - " + theForm.elements[idx].value );

// EXAMPLE-2
// -------
//    var myform = window.document.forms[0];
//    for (var i = 0; i < myform.elements.length; i++) {
//  	  myobj = myform.elements[i];
//        alert("Control Type: " + myobj.type + "\nControl ID: " + myobj.id);
//        if (myform.elements[i].type == "text") {
//            myform.elements[i].value = ""
//        }
//        if (myform.elements[i].type == "select-one" || 
//            myform.elements[i].type == "checkbox" || 
//            myform.elements[i].type == "submit") {
//        }
//    }


// SCAN THROUGH A TABLE TAG ROWS AND CELLS
// ----------------------------------------
//var count=0;
//    for (i=0; i < document.all.oTable.rows.length; i++) {
//        for (j=0; j < document.all.oTable.rows(i).cells.length; j++) {
//            document.all.oTable.rows(i).cells(j).innerText = count;
//            count++;
//        }
//    }


//Table 42-4 FileSystemObject Property and Methods
//================================================
//Property                      					 Description
//--------------------------------------------------------------
//Drives						                     Returns a collection of (disk) Drive objects (Drive object has 15 properties)

//Method						                     Description
//---------------------------------------------------------------
//BuildPath(path, name)             				 Appends name to existing path
//CopyFile(src, dest[,overwrite])       	 		 Copies file at src path to dest path, optionally to automatically overwrite existing dest file of same name
//CopyFolder(src, dest[,overwrite])         		 Copies directory at src path to dest path, optionally to automatically overwrite existing dest directory of same name
//CreateFolder(path)                				 Creates folder with name specified in path
//CreateTextFile(path[,overwrite[, unicode]])   	 Returns TextStream object after opening an empty file at path, optionally to overwrite existing file at path and optionally to save characters in Unicode (instead of ASCII)
//DeleteFile(path[, force])             			 Deletes file at path, optionally to force deletion of read-only file
//DeleteFolder(path[, force])               		 Deletes directory at path, optionally to force deletion of read-only directory
//DriveExists(drivespec)                			 Returns true if specified drive exists on client
//FileExists(filespec)              				 Returns true if specified file exists
//FolderExists(folderspec)              			 Returns true if specified directory exists
//GetAbsolutePathName(pathspec)             		 Returns full path based on parameters supplied in pathspec
//GetBaseName(filespec)             				 Returns base name of rightmost item in filespec but without file extension
//GetDrive(drivespec)				                 Returns Drive object referenced by drivespec (for example, c:\)
//GetDriveName(path)                				 Returns name of the drive for a given path
//GetExtensionName(path)                			 Returns file extension for rightmost item in the path
//GetFile(filespec) 				                 Returns File object (a File object has 12 properties and 4 methods of its own)
//GetFileName(filespec)             				 Returns the full filename of rightmost item in pathspec
//GetFileVersion(filespec)			                 Returns version number associated with a file
//GetFolder(folderspec)             				 Returns Folder object (a Folder object has 15 properties and 4 methods of its own)
//GetParentFolderName(path)             			 Returns name of parent directory of path
//GetSpecialFolder(type)                			 Returns Folder object of type 0 (Windows), 1(Windows\System), or 2 (Windows\Temp)
//GetTempName()			    		                 Returns a nonsense name for use as a temp filename
//MoveFile(src, dest)               				 Moves src file(s) to dest
//MoveFolder(src, dest)			                	 Moves src folder(s) to dest
//OpenTextFile(path[, iomode[,create[, format]]])	 Returns a TextStream object after opening a file at path for mode (ForReading, ForWriting, ForAppending); optionally to create file if not existing; optionally to treat characters as Unicode (TristateTrue), ASCII (TristateFalse), or system default (TristateUseDefault)

