 var myWin_N;
 var popUpDate_N;
 var popUpCode_N;
 var popUpHelp_N = null;

var iw_N;
var ih_N;

var icordX_N;
var icordY_N;

var Pcnt_N;
var myobj_N;
var myprm_N;

var ctrNAME_FORM_N;
var ctrNAME_VALUE_N;
var ctrNAME_TEXT_N;
var strParam_N;

var obj_sel_N;
var obj_txt_N;
var obj_val_N;

var currPrefs = new Array();
var my_args;
var my_form;
var my_returnedData;

//javascript:void(0);

      //var myform = window.document.forms[0];
      //for (var i = 0; i < myform.elements.length; i++) {
      ////for (var i = 0; i < window.document.forms[0].elements.length; i++) {
      //  //document.write("\nItem: " + i + " " + window.document.forms[0].elements[i].type);     
      //  document.write("\nItem: " + i + " " + myform.elements[i].type);     
      //}

function ShowPopup_Msg(strMSG) {
    try {
        alert(strMSG);
    }
    catch (ex) {
        alert("Error has occured!. Reason: " + ex.message);
    }
}

    function Sel_Func_Init() {
	    if (window.dialogArguments) {
		    my_args = window.dialogArguments;
//    		my_form = document.Form1;
//	    	if (my_args["array_id"]) {
//	    	    my_form.txtCustID.value = my_args["array_id"];
//		    }
//	    	if (my_args["array_name"]) {
//	    	    my_form.txtCustName.value = my_args["array_name"];
//		    }
        }		    
    }


function Sel_Func_Open(fvQRY_TYPE, PopPage, fvFRM_Name, fvCTR_Val, fvCTR_Txt)
{

    ctrNAME_FORM_N = fvFRM_Name;
    ctrNAME_VALUE_N = fvCTR_Val;
    ctrNAME_TEXT_N = fvCTR_Txt;

    strParam_N = PopPage
    strParam_N = strParam_N + "&QRY_TYPE=" + fvQRY_TYPE;
    strParam_N = strParam_N + "&FRM_NAME=" + fvFRM_Name;
    strParam_N = strParam_N + "&CTR_VAL=" + fvCTR_Val;
    strParam_N = strParam_N + "&CTR_TXT=" + fvCTR_Txt;
    
   // alert("Parameter Received...\nForm ID: " + ctrNAME_FORM_N + "\nControl Value Name: " + ctrNAME_VALUE_N  + "\nControl Text Name: " + ctrNAME_TEXT_N);

    iw_N = 1000;
    ih_N = 550;

    icordX_N = (screen.width - iw_N) / 2;
    icordY_N = (screen.height - ih_N) / 2;
    icordY_N = ((screen.height - ih_N) / 2 ) - 50;
    if (icordY_N < 1) {
       icordY_N = 50;
    }

    var str_browser_ver = navigator.appVersion;
    var str_browser_name = navigator.appName;

    //document.getElementById(ctrNAME_TEXT_N).value = str_browser_name;
    //alert("Browser Name: " + str_browser_name);

    //str_browser_name = str_browser_name + "_XXX";


    var sharedObject = {};
    sharedObject.array_id = "001";
    sharedObject.array_name = "001-Name";

    var retValue = false;
    
        if (window.showModalDialog && str_browser_name == "Microsoft Internet Explorer") {
           retValue = window.showModalDialog(strParam_N, sharedObject,
           "dialogHeight:" + ih_N + "px; " +
           "dialogWidth:" + iw_N + "px; " +
           "dialogLeft:" + icordX_N + "px; " +
           "dialogTop:" + icordY_N + "px; " +
           "help:yes; center:yes; resizable:yes; status:yes");
           if (retValue) {
              Sel_Func_UpdateFields(retValue.array_id, retValue.array_name, ctrNAME_FORM_N, ctrNAME_VALUE_N, ctrNAME_TEXT_N);
           }
        }
        else {
           popUpHelp_N = window.open(strParam_N, "", "width=" + iw_N + ",height=" + ih_N + ",left=" + icordX_N + ",top=" + icordY_N + ",resizable=1,channelmode=1,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no,modal=yes,alwaysRaised=yes");
           //popUpHelp_N = window.open(PopPage, "fraDetails", "width=" + iw_N + ",height=" + ih_N + ",left=" + icordX_N + ",top=" + icordY_N + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no");
           //popUpHelp_N.dialogArguments = sharedObject;
           popUpHelp_N.focus;
        }
    
}


function Sel_Func_OK(fvopt) {
    //                window.opener.UpdateFields (forename.value, surname.value);

    var fvVal = document.getElementById("hidCustID").value;
    var fvTxt = document.getElementById("hidCustName").value;
    var fvFRM_Name = document.getElementById("hidFRM_NAME").value;
    var fvCTR_Val = document.getElementById("hidCTR_VAL").value;
    var fvCTR_Txt = document.getElementById("hidCTR_TXT").value;

    ctrNAME_FORM_N = fvFRM_Name;
    ctrNAME_VALUE_N = fvCTR_Val;
    ctrNAME_TEXT_N = fvCTR_Txt;

    var str_browser_ver = navigator.appVersion;
    var str_browser_name = navigator.appName;

    //str_browser_name = str_browser_name + "_XXX";

        if (window.showModalDialog && str_browser_name == "Microsoft Internet Explorer") {
           var sharedObject = {};
           sharedObject.array_id = fvVal;
           sharedObject.array_name = fvTxt;

           window.returnValue = sharedObject;
           window.close();
       }
       else  {
           if (window.opener != null && !window.opener.closed) {
               window.opener.Sel_Func_UpdateFields(fvVal, fvTxt, ctrNAME_FORM_N, ctrNAME_VALUE_N, ctrNAME_TEXT_N);
               //window.opener.document.getElementById(ctrNAME_VALUE_N).value = fvVal;
               //window.opener.document.getElementById(ctrNAME_TEXT_N).value = fvTxt;
               //popUpHelp_N.document.forms[0].ctrNAME_VALUE_N.value = fvVal;
               //popUpHelp_N.document.forms[0].ctrNAME_TEXT_N.value = fvTxt;
               //alert("About to close window...\nSelected ID: " + fvVal + "\nSelected Text: " + fvTxt +
               //  "\nForm Name: " + ctrNAME_FORM_N + "\nField Value Name: " + ctrNAME_VALUE_N + "\nField Text Name: " + ctrNAME_TEXT_N);
               if (popUpHelp_N) {
                   popUpHelp_N.close();
               }
               else {
                   window.close();
               }
           }
           else {
               alert("No parent window open...");
               window.close();
           }
       }
    
    popUpHelp_N = null;
    
}

function Sel_Func_Cancel() {
    window.returnValue = "";
    window.close();
}

function Sel_Func_UpdateFields(newVal, newTxt, fvFRM, fvctr_Val, fvctr_Txt) {

    //alert("Update Parameters...\nNew Value: " + newVal + "\nNew Text: " + newTxt + "\nForm ID: " + fvFRM + "\nControl Value Name: " + fvctr_Val + "\nControl Text Name: " + fvctr_Txt);

    var my_form = null;

    if (document.layers) {
        my_form = document.layers['Form1'];
    }
    else if (document.all) {
        my_form = document.all('Form1');
    }
    else if (document.getElementById) {
        my_form = document.getElementById('Form1');
    }

    if (!my_form) {
        my_form = document.forms['Form1'];
    }
    if (!my_form) {
        my_form = document.Form1;
    }

    if (!my_form) {
        my_form = window.document.forms[0];
    }

    if (!my_form) {
        alert('Unable to obtain the page Form. Operation cancelled...');
        //window.close();
        return true;
    }

    if (my_form.elements[fvctr_Val]) {
        if (my_form.elements[fvctr_Val].type == "text" || my_form.elements[fvctr_Val].type == "hidden") {
            obj_val_N = my_form.elements[fvctr_Val];
            obj_val_N.value = newVal;
        }
    }

    if (my_form.elements[fvctr_Txt]) {
        if (my_form.elements[fvctr_Txt].type == "text" || my_form.elements[fvctr_Txt].type == "hidden") {
            obj_txt_N = my_form.elements[fvctr_Txt];
            obj_txt_N.value = newTxt;
        }
    }

}


function OpenModal_Form(pvPAGE_URL, pvVal, pvTxt)
{

 // Use the following code on the FORM object
 //     onSubmit="return false" 

    iw_N = 960;
    ih_N = 640;

    icordX_N = (screen.width - iw_N) / 2;
    icordY_N = (screen.height - ih_N) / 2;
    icordY_N = ((screen.height - ih_N) / 2 ) - 50;
    if (icordY_N < 0) {
       icordY_N = 50;
    }

     // Note: The [currPrefs] array variable should be declared as a global variable

     currPrefs = new Array();

     //currPrefs["array_id"] = "01702";
     //currPrefs["array_name"] = "DEBTOR CONTROL ACCOUNT";
     
//     var prefs  = window.showModalDialog(pvPAGE_URL, currPrefs, 
//        "window.dialogHeight:" + ih_N + "px; " + 
//        "window.dialogWidth:" + iw_N + "px; " + 
//        "window.dialogLeft:" + icordX_N + "px; " +
//        "window.dialogTop:" + icordY_N + "px; " +
//        "help:yes; center:yes; resizable:yes; status:yes");

     var prefs  = window.showModalDialog(pvPAGE_URL, currPrefs, 
        "dialogHeight:" + ih_N + "px; " + 
        "dialogWidth:" + iw_N + "px; " + 
        "dialogLeft:" + icordX_N + "px; " +
        "dialogTop:" + icordY_N + "px; " +
        "help:yes; center:yes; resizable:yes; status:yes");


    // To retrieve any data PASS TO to the dialog box use the following codes
    // -----------------------------------------------------------------------
    //if (window.dialogArguments) {
    //    var args = window.dialogArguments;
    //    var myform_obj = document.form_name;
    //    // check if [args] variable contains valid array index called - array_id
    //    if (args["array_id"]) {
    //        myform_obj.control_name.value = args["array_id"];
    //    }
    // }
     
    // To retrieve any data PASS FROM the dialog box use the following codes
    // -----------------------------------------------------------------------
    //    // check if [prefs] is a valid variable or object and contains valid array index called array_id
	if (prefs && prefs["array_id"] && prefs["array_name"]) {
	    alert("Values From Called Form. \nID: " + prefs["array_id"] + "\nValue: " + prefs["array_name"] );
		if (prefs["array_id"]) {
		    //document.all.txtAssured_Num.innerText = prefs["array_id"];
		    if (document.forms(0).elements["TabContainer1_TabPanel1_" + pvVal].type == "text") {
    		    obj_val = document.forms(0).elements["TabContainer1_TabPanel1_" + pvVal];
	            obj_val.value = prefs["array_id"];
		    }
	        //  Store the received value in the global variable in case you want to access it again but, is is optional
		    currPrefs["array_id"] = prefs["array_id"];
        }
        
		if (prefs["array_name"]) {
		    //document.all.txtAssured_Name.innerText = prefs["array_name"];
		    if (document.forms(0).elements["TabContainer1_TabPanel1_" + pvTxt].type == "text") {
                obj_txt = document.forms(0).elements["TabContainer1_TabPanel1_" + pvTxt];
    	        obj_txt.value = prefs["array_name"];
    	    }    
	        //  Store the received value in the global variable in case you want to access it again but, is is optional
		    currPrefs["array_name"] = prefs["array_name"];
		}
    }	

}

function InitModal_Cal () {
            var sharedObject = window.dialogArguments;

            //var forename = document.getElementById ("forename");
            //var surname = document.getElementById ("surname");
            //forename.value = sharedObject.forename;
            //surname.value = sharedObject.surname;
        }

function OpenModal_Cal(PopPage, pvFRM, pvVal, pvTxt)
{

 // Use the following code on the FORM object
 //     onSubmit="return false" 

    //alert("Object Parameter-1: " + pvVal);

    ctrNAME_FORM_N = pvFRM;
    ctrNAME_VALUE_N = pvVal;
    ctrNAME_TEXT_N = pvTxt;

    strParam_N = PopPage
    strParam_N = strParam_N + "&FRM_NAME=" + pvFRM;
    strParam_N = strParam_N + "&CTR_VAL=" + pvVal;
    strParam_N = strParam_N + "&CTR_TXT=" + pvTxt;
    
    iw_N = 370;
    ih_N = 340;

    icordX_N = (screen.width - iw_N) / 2;
    icordY_N = (screen.height - ih_N) / 2;
    icordY_N = ((screen.height - ih_N) / 2 ) - 50;
    if (icordY_N < 0) {
       icordY_N = 50;
    }

     var prefs = new Array();
     var myObject = new Object();

     //currPrefs = new Array();

     var sharedObject = {};
     var mytoday = new Date();
     
     sharedObject.array_id = mytoday.getMonth() + "/" + mytoday.getDay() + "/" + mytoday.getYear();
     sharedObject.array_name = mytoday.getMonth() + "/" + mytoday.getDay() + "/" + mytoday.getYear();

     var BrwType = navigator.appName;
     // Note:  navigator.appName may returns
     //   Microsoft Internet Explorer
     //   Netscape
     
     if (BrwType == "Microsoft Internet Explorer") {    
     //if (window.showModalDialog) {
         //     var prefs  = window.showModelessDialog(strParam_N, currPrefs, 
         //        "dialogHeight:" + ih_N + "px; " + 
         //        "dialogWidth:" + iw_N + "px; " + 
         //        "dialogLeft:" + icordX_N + "px; " +
         //        "dialogTop:" + icordY_N + "px; " +
         //        "help:yes; center:yes; resizable:yes; status:yes");

         var retValue = window.showModalDialog(strParam_N, sharedObject,
             "dialogHeight:" + ih_N + "px; " + "dialogWidth:" + iw_N + "px; " +
             "dialogLeft:" + icordX_N + "px; " + "dialogTop:" + icordY_N + "px; " +
             "help:yes; center:yes; resizable:yes; status:yes");

             
         try {
            
             //alert("About to update parent page...");

             var fld_id = document.getElementById(pvVal);

             if (retValue.array_id == undefined) {
             }
             else {
                 if (fld_id.type == "text" || fld_id.type == "hidden") {
                     //alert("Ready to update parent page..." + pvVal);
                     //document.getElementById(pvVal).value = mytoday.getDay() + "/" + mytoday.getMonth() + "/" + mytoday.getFullYear();
                     //document.getElementById(pvVal).innerHTML = retValue.array_id;
                     //document.getElementById('txtDte').value = retValue.array_id;
                     fld_id.value = retValue.array_id;
                 }
             }            
             
             //alert("End of update...");
         }
         catch (ex) {
         }
                 
     }

     else {
         // for similar functionality in Opera, but it's not modal!
         //alert("About to load page...");
         var objModal = window.open(strParam_N, null, "width=" + iw_N + ",height=" + ih_N + ",top=" + icordY_N + ",left=" + icordX_N + ",resizable=1,channelmode=0,status=yes,toolbar=no,menubar=no,scrollbars=yes,location=no,modal=yes,alwaysRaised=yes", null);
         objModal.dialogArguments = sharedObject;
         objModal.focus;
     }

        //prefs = window.returnValue;
    	
}


    function myFunc_OK(fvopt) {
	    my_form = document.Form1;
		//alert("Selected Index: " +  my_form.selScheme_Type.selectedIndex);
		if (my_form.selScheme_Type.selectedIndex == 0 || my_form.selScheme_Type.selectedIndex == -1 )
		{
		    alert("No item selected!. \nYou should select an item from the list box...");
    	    window.returnValue = "";
		    //return false;    
		}
		else
		{
		    window.returnValue = myFunc_GetFormData(fvopt);
           //alert("About to leave the popup page...");		
        	window.close();
        }
		
    }

    function myFunc_OK_Cal(fvopt) {

        var fvFRM_Name = document.getElementById("hidFRM_NAME").value;
        var fvCTR_Val = document.getElementById("hidCTR_VAL").value;
        var fvCTR_Txt = document.getElementById("hidCTR_TXT").value;

        var sharedObject = {};
        
        ctrNAME_FORM_N = fvFRM_Name;
        ctrNAME_VALUE_N = fvCTR_Val;
        ctrNAME_TEXT_N = fvCTR_Txt;
        
        var BrwType = navigator.appName;
        // Note:  navigator.appName may returns
        //   Microsoft Internet Explorer
        //   Netscape

            if (BrwType == "Microsoft Internet Explorer") {   
           //if (window.showModalDialog) {
                //alert("About to pass parameter to parent page... ");
               //alert("About to update parent page... " + document.forms['Form1'].elements["hidDate"].value);
                var fld_id = document.getElementById("hidDate");
                var fld_name = document.getElementById ("hidDate");
                if (fld_id.type == "text" || fld_id.type == "hidden") {
                    
                    sharedObject.array_id = fld_id.value;
                    sharedObject.array_name = fld_name.value;
                    //sharedObject = { array_id: fld_id.value, array_name: fld_name.value };
                    
                    window.returnValue = sharedObject;
                    //alert("About to update parent page... " + document.forms['Form1'].elements["hidDate"].value);
                }    
            }
            else {
                    // if not modal, we cannot use the returnValue property, we need to update the opener window
               //alert("About to update parent page...");
                var fld_id = document.getElementById ("hidDate");
                var fld_name = document.getElementById("hidDate");
                //var ctr_id = window.opener.document.getElementById(fvCTR_Val);
                //if ((fld_id.type == "text" || fld_id.type == "hidden") && (ctr_id.type == "text" || ctr_id.type == "hidden")) {
                if (fld_id.type == "text" || fld_id.type == "hidden") {

                    sharedObject.array_id = fld_id.value;
                    sharedObject.array_name = fld_name.value;
                    //sharedObject = { array_id: fld_id.value, array_name: fld_name.value };

                    //if (window.opener != null && !window.opener.closed) {
                    if (window.opener) {
                        //var txtName = window.opener.document.getElementById("parent_control_name");
                        //txtName.value = document.getElementById("child_control_name").value;
                        window.opener.UpdateFields_Cal(fld_id.value, fld_name.value, fvCTR_Val, fvCTR_Txt);
                        //window.opener.document.getElementById(fvCTR_Val).value = fld_id.value;
                        //window.opener.document.getElementById(fvCTR_Txt).value = fld_name.value;
                    }
                    else {
                        //alert("No open window or missing parent window...");
                    }    
                      
                }

                if (window.opener) {
                    window.opener.returnValue = sharedObject;
                }
                window.returnValue = sharedObject;
                //self.close();
   
            }

            window.close();

    }


    function UpdateFields_Cal(newVal, newTxt, pvVal, pvTxt) {

        var my_form = null;

        if (document.layers) {
            my_form = document.layers['Form1'];
        }
        else if (document.all) {
            my_form = document.all('Form1');
        }
        else if (document.getElementById) {
            my_form = document.getElementById('Form1');
        }

        if (!my_form) {
            my_form = document.forms['Form1'];
        }
        if (!my_form) {
            my_form = document.Form1;
        }

        if (!my_form) {
            my_form = window.document.forms[0];
        }

        if (!my_form) {
            alert('Unable to obtain the page Form. Operation cancelled...');
            //window.close();
            return true;
        }

        if (my_form.elements[pvVal].type == "text" || my_form.elements[pvVal].type == "hidden") {
            obj_val_N = my_form.elements[pvVal];
            obj_val_N.value = newVal;
        }

        if (my_form.elements[pvTxt]) {
            if (my_form.elements[pvTxt].type == "text" || my_form.elements[pvVal].type == "hidden") {
                obj_txt_N = my_form.elements[pvTxt];
                obj_txt_N.value = newTxt;
            }
        }


    }
    
    function myFunc_GetFormData(fvopt) {
	    my_form = document.Form1;
	    my_returnedData = new Array();

        // Harvest values for each type of form element
		my_form.txtCustID.value = my_form.selScheme_Type.options[my_form.selScheme_Type.selectedIndex].value;
		my_returnedData["array_id"] = my_form.txtCustID.value;
		
		my_form.txtCustName.value = my_form.selScheme_Type.options[my_form.selScheme_Type.selectedIndex].text;
		my_returnedData["array_name"] = my_form.txtCustName.value;

        if (fvopt == "OK")
        {
    	    return my_returnedData;
    	}
    	else
    	{
    	    return "";
    	}
   }

    // Handle click of Cancel button
    function myFunc_Cancel() {
	    window.returnValue = "";
    	window.close();
    }

function CloseModal_Form() {  
  window.close();
}

    function getSelected_Change(pvobj_sel, pvobj_txt, pvformName) {

      var myidx;
      var myval;
      var mytxt;
            		
		//obj_sel = document.getElementById(pvobj_sel);
		//obj_sel = document.forms(0).elements["TabContainer1_TabPanel1_selRegion"];
        obj_sel = document.forms(0).elements["TabContainer1$TabPanel1$selRegion"];
        obj_sel = document.forms(0).elements["TabContainer1$TabPanel1$" + pvobj_sel];
        obj_sel = document.forms(0).elements["TabContainer1_TabPanel1_" + pvobj_sel];
        //obj_sel = obj_frm.elements["TabContainer1_TabPanel1_" + pvobj_sel];
        
		//obj_txt = document.getElementById(pvobj_txt);
        //obj_txt = document.forms(0).elements["TabContainer1_TabPanel1_txtRegion_Num"];
        obj_txt = document.forms(0).elements["TabContainer1$TabPanel1$txtRegion_Num"];
        obj_txt = document.forms(0).elements["TabContainer1$TabPanel1$" + pvobj_txt];
        obj_txt = document.forms(0).elements["TabContainer1_TabPanel1_" + pvobj_txt];
        //obj_txt = obj_frm.elements["TabContainer1_TabPanel1_" + pvobj_txt];

          //if (obj_sel && obj_txt)  {
          //   alert("Control Name: " + obj_sel.name + "\nID: " + obj_sel.id + "\nType: " + obj_sel.type +
          //     "\nControl Name: " + obj_txt.name + "\nID: " + obj_txt.id + "\nType: " + obj_txt.type);
          //}
      
      if (obj_sel && obj_txt)  {
         myidx = obj_sel.selectedIndex;

         //myval = document.Form1.control_name.options[myidx].value;
         myval = obj_sel.options[myidx].value;
         mytxt = obj_sel.options[myidx].text;
      
         if (myval != "*") {
           //document.forms(0).elements["TabContainer1$TabPanel1$txtScheme_Type"].value = myval;
           //document.Form1.msg.value = myidx + " - " + myval + " - " + mytxt;
              obj_txt.value = myval;
              //window.alert("You selected: " + myidx + " - " + myval + " - " + mytxt);
         }  
         else {
            window.alert("Please select valid option from the drop down list box...");
         }
         
      }
      else {
        window.alert("Error!. Missing or Invalid object...");
      }
        
    }
   