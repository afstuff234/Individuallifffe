$(document).ready(function() {
    $("#txtPolicyNumber").blur(function(e) {
        e.preventDefault();
        if ($("#txtPolicyNumber").val() != "") {
            console.log('RetrieveAssuredCode init');
            RetrieveAssuredCode();
        }
    })
    $('#cboSearch').change(function(e) {
        e.preventDefault();
        if ($("#cboSearch").val() != "* Select Insured *") {
            GetPolicyNoFromDpList();
        }
        else {
            alert("Please select insured")
            InitializeClientControls()
            $("#txtPolicyNumber").val("")
        }
    });

    $('#chkConfirmWaiver').change(function(e) {
        e.preventDefault();
        if ($(this).is(":checked")) {
            // it is checked
            GetCoverCodes();
        }
        else {
            $('#drpWaiverCodes').hide();
            $('#lblWaiverCode').hide();
            $('#lblWaiverEffDate').hide();
            $('#txtWaiverEffectiveDate').hide();
            $('#lblWaiverEffFormat').hide();
        }
    });

    $('#drpWaiverCodes').change(function(e) {
        e.preventDefault();
        if ($('#drpWaiverCodes').val() != "Select") {
            VerifyAdditionalPolicyCover();
        }
    });

    $('#txtWaiverEffectiveDate').blur(function(e) {
        e.preventDefault();
        if ($('#txtWaiverEffectiveDate').val() != "") {
            var res = CheckDate($('#txtWaiverEffectiveDate').val());
            if (res == true) {
                $('#lblMsg').text("");
                return true
            }
            else {
                $('#lblMsg').text("Not a valid Waiver effective date format");
                $('#txtWaiverEffectiveDate').focus();
                return false;
            }
        }
    });
});



function RetrieveAssuredCode() {
    console.log('RetrieveAssuredCode init1');
    //$('#lblMsg').text("");
    InitializeClientControls()
    var policyNo = $("#txtPolicyNumber").val();
    //alert("This is the class code: " + document.getElementById('txtClassCod').value + " and Item code :" + document.getElementById('txtTrlblMsgNum').value);
    $.ajax({
        type: "POST",
        url: "PRG_LI_CLM_WAIVER.aspx/GetPolicyPerInfo",
        data: JSON.stringify({ _policyNo: policyNo }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_RetrieveAssuredCode,
        failure: OnFailure_RetrieveAssuredCode,
        error: OnError_RetrieveAssuredCode
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_RetrieveAssuredCode(response) {
    //debugger;

    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_AdminCodeInfoValues(admobjects);

}
// retrieve the values for branch
function retrieve_AdminCodeInfoValues(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        $('#txtAssuredCode').val($(this).find("TBIL_POLY_ASSRD_CD").text())
        $('#HidAssuredCode').val($(this).find("TBIL_POLY_ASSRD_CD").text())
        $('#txtPolyStatus').val($(this).find("TBIL_POLY_STATUS").text())
        $('#txtAssuredName').
                val($(this).find("TBIL_INSRD_SURNAME").text() + '  ' + $(this).find("TBIL_INSRD_FIRSTNAME").text())
        $('#HidAssuredName').
                val($(this).find("TBIL_INSRD_SURNAME").text() + '  ' + $(this).find("TBIL_INSRD_FIRSTNAME").text())
        $('#txtPolicyProCode').val($(this).find("TBIL_POL_PRM_PRDCT_CD").text())
        $('#HidPolicyProCode').val($(this).find("TBIL_POL_PRM_PRDCT_CD").text())
        
        $('#txtPolicyStartDate').val(formatDate($(this).find("TBIL_POL_PRM_FROM").text()));
        $('#HidPolStartDate').val(formatDate($(this).find("TBIL_POL_PRM_FROM").text()));
        
        $('#txtPolicyEndDate').val(formatDate($(this).find("TBIL_POL_PRM_TO").text()));
        $('#HidPolEndDate').val(formatDate($(this).find("TBIL_POL_PRM_TO").text()));
        $('#txtProdDesc').val($(this).find("TBIL_PRDCT_DTL_DESC").text())
        $('#HidProdDesc').val($(this).find("TBIL_PRDCT_DTL_DESC").text())
        var status = $('#txtPolyStatus').val();
        if (status == "W") {
            $("#chkConfirmWaiver").prop("checked", true);
            $('#drpWaiverCodes').show();
            $('#lblWaiverCode').show();
            $('#lblWaiverEffDate').show();
            $('#txtWaiverEffectiveDate').show();
            $('#lblWaiverEffFormat').show();
            $('#txtWaiverEffectiveDate').val(formatDate($(this).find("WAIVER_DT").text()));
            $('#lblMsg').text("Waiver has already been processed");
            GetCoverCodes()
            var waiverCode = $(this).find("WAIVERCODE").text();
            Get_Effected_Waiver_Dsc(waiverCode)
           // $('#drpWaiverCodes').select($(this).find("WAIVERCODE").text());
        }
        else {
            $('#drpWaiverCodes').hide();
            $('#lblWaiverCode').hide();
            $('#lblWaiverEffDate').hide();
            $('#txtWaiverEffectiveDate').hide();
            $('#lblWaiverEffFormat').hide();
            $('#lblMsg').text("");
        }
    });
    //RetrieveInsuredDetails();
   // RetrieveProductCode();
}
function OnError_RetrieveAssuredCode(response) {
    //debugger;
    var errorText = response.responseText;
    $("#txtPolicyNumber").focus();
    alert('Error! Policy Number does not exist');
   
}

function OnFailure_RetrieveAssuredCode(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}
function GetCoverCodes() {
    $.ajax({
        type: "POST",
        url: "PRG_LI_CLM_WAIVER.aspx/GetCoverCodes",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_GetCoverCodes,
        failure: OnFailure_GetCoverCodes,
        error: OnError_GetCoverCodes
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_GetCoverCodes(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_GetCoverCodes(admobjects);

}
// retrieve the values for branch
function retrieve_GetCoverCodes(admobjects) {
    //debugger;
    $('#drpWaiverCodes').empty();
    $('#drpWaiverCodes')
            .append('<option value="Select">' + "Select" + '</option>')
    $.each(admobjects, function() {
        var admobject = $(this);
        //document.getElementById("drpWaiverCodes").value = $(this).find("TBIL_COV_CD").text();
       $('#drpWaiverCodes')
            .append('<option value=' + $(this).find("TBIL_COV_CD").text() + '>' +
             $(this).find("TBIL_COV_DESC").text() + ' ; ' + $(this).find("TBIL_COV_CD").text() + '</option>')
    });
    $('#drpWaiverCodes').show();
    $('#lblWaiverCode').show();
}
function OnError_GetCoverCodes(response) {
    //debugger;
    var errorText = response.responseText;
    alert(errorText)
}

function OnFailure_GetCoverCodes(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}
function CheckDate(my) {
    var returnMsg;
      var d = new Date();
      var userdate = new Date(my)
      // var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(18|20)\d{2}$/; //mm/dd/yyyy
      var date_regex = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/
      if (!(date_regex.test(my))) {
          returnMsg = false;
          }
          else {
              returnMsg = true;
          }
          return returnMsg;  
  }

  function CheckPolicyEndDate(my) {
      var returnMsg;
      var d = new Date();
      var userdate = new Date(my)
      // var date_regex = /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(18|20)\d{2}$/; //mm/dd/yyyy
      var date_regex = /^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/
      if (!(date_regex.test(my))) {
          returnMsg = "Invalid";
      }
      else {
          if (userdate <= d) {
              returnMsg = "Invalid1";
          }
          else {
              returnMsg = "Valid";
          }
      }
      return returnMsg;
  }

function ValidateOnClient() {
    if ($("#txtPolicyNumber").val() == "") {
        $("#lblMsg").text("Please enter a policy number");
        return false;
    }
    else if ($("#txtAssuredCode").val() == "") {
        $("#lblMsg").text("Please enter a assurance code");
        return false;
    }
    else if ($("#txtPolicyProCode").val() == "") {
        $("#lblMsg").text("Please enter policy product code");
        return false;
    }
    else if ($("#txtPolicyStartDate").val() == "") {
        $("#lblMsg").text("Please enter policy start date");
        return false;
    }

    else if (CheckDate($('#txtPolicyStartDate').val())==false) {
                $('#lblMsg').text("Not a valid Policy start date format");
                $('#txtPolicyStartDate').focus();
                return false;
    }
    else if ($("#txtPolicyEndDate").val() == "") {
        $("#lblMsg").text("Please enter policy end date");
        return false;
    }
    else if (CheckPolicyEndDate($('#txtPolicyEndDate').val()) == "Invalid") {
        $('#lblMsg').text("Not a valid policy end date format");
        $('#txtPolicyEndDate').focus();
        return false;
    }
    else if (CheckPolicyEndDate($('#txtPolicyEndDate').val()) == "Invalid1") {
    $('#lblMsg').text("Policy end date must be a future date");
        $('#txtPolicyEndDate').focus();
        return false;
    }
    else if (!$('#chkConfirmWaiver').is(":checked")) {
        // $("#lblMsg").text("");
        $("#lblMsg").text("Please confirm waiver");
        return false
    }
    else if ($("#drpWaiverCodes").val() == "Select") {
        $("#lblMsg").text("Please select a waiver code");
        return false;
    }
    else if ($("#txtWaiverEffectiveDate").val() == "") {
        $("#lblMsg").text("Please enter waiver effective date");
        return false;
    }
    else if (CheckDate($('#txtWaiverEffectiveDate').val()) == false) {
        $('#lblMsg').text("Not a valid waiver effective date format");
        $('#txtPolicyStartDate').focus();
        return false;
    }
    else if ($("#txtPolyStatus").val() == "") {
        $("#lblMsg").text("Waiver cannot be process because status is null");
        return false;
    }
    else if ($("#txtPolyStatus").val() == "W") {
        $("#lblMsg").text("Waiver has already been processed");
        return false;
    }
    else if (getYear($("#txtWaiverEffectiveDate").val()) < getYear($("#txtPolicyStartDate").val())
     || getYear($("#txtWaiverEffectiveDate").val()) > getYear($("#txtPolicyEndDate").val()) ){
       $("#lblMsg").text("Waiver effective date must be within policy year");
        return false;
       }
    else {
        return true;
        $('#drpWaiverCodes').hide();
        $('#lblWaiverCode').hide();
        $('#lblWaiverEffDate').hide();
        $('#txtWaiverEffectiveDate').hide();
        $('#lblWaiverEffFormat').hide();
        InitializeClientControls() 
    }
}

function GetPolicyNoFromDpList() {
    console.log('GetPolicyNoFromDpList init');
    var details = $('#cboSearch').val();
    $('#txtPolicyNumber').val(details)
   // $('#txtAssuredCode').focus()
    RetrieveAssuredCode();
}

function VerifyAdditionalPolicyCover() {
    var _WaiverCodes = $("#drpWaiverCodes").val();
    var _PolicyNumber = $("#txtPolicyNumber").val();
    $.ajax({
        type: "POST",
        url: "PRG_LI_CLM_WAIVER.aspx/VerifyAdditionalCover",
        data: JSON.stringify({ _WaiverCodes: _WaiverCodes, _PolicyNumber: _PolicyNumber }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_VerifyAdditionalPolicyCover,
        failure: OnFailure_VerifyAdditionalPolicyCover,
        error: OnError_VerifyAdditionalPolicyCover
    });
    // this avoids page refresh on button click
    return false;
}
function OnSuccess_VerifyAdditionalPolicyCover(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_VerifyAdditionalPolicyCover(admobjects);
}
// retrieve the values for branch
function retrieve_VerifyAdditionalPolicyCover(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        var MSG = $(this).find("MSG").text()
        if (MSG == "0") {
            alert("Waiver code not found in Additional policy cover table, Waiver not applicable");
            $('#lblWaiverEffDate').hide();
            $('#txtWaiverEffectiveDate').hide();
            $('#lblWaiverEffFormat').hide();
        }
        else {
            $('#lblWaiverEffDate').show()
            $('#txtWaiverEffectiveDate').show()
            $('#lblWaiverEffFormat').show()
        }
    });

}
function OnError_VerifyAdditionalPolicyCover(response) {
    //debugger;
    var errorText = response.responseText;

}

function OnFailure_VerifyAdditionalPolicyCover(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}

function InitializeClientControls() {
    //$('#lblMsg').text("");
    //$('#lblMsg').text("");
    $('#txtAssuredCode').val("");
    $('#txtAssuredName').val("");
    $('#drpWaiverCodes').empty();
    $('#txtPolicyProCode').val("");
    $('#txtProdDesc').val("");
    $('#txtPolicyStartDate').val("");
    $('#txtPolicyEndDate').val("");
    $('#txtWaiverEffectiveDate').val("");
    $('#txtPolyStatus').val("");
    $("#chkConfirmWaiver").prop("checked", false);
    $('#drpWaiverCodes').hide();
    $('#lblWaiverCode').hide();
    $('#lblWaiverEffDate').hide();
    $('#txtWaiverEffectiveDate').hide();
    $('#lblWaiverEffFormat').hide();
    $('#HidAssuredName').val("");
    $('#HidPolStartDate').val("");     
    $('#HidPolEndDate').val("");
    $('#HidProdDesc').val("");
}


function formatDate(dateValue) {
    var dateValue_array = dateValue.split('T');
    var final_dateValue_array = dateValue_array[0].split('-');
    var formattedDate = final_dateValue_array[2] + '/' +
                 final_dateValue_array[1] + '/' + final_dateValue_array[0];
    return formattedDate
}

function getYear(dateValue) {
    var dateValue_array = dateValue.split('/');
    var myYear = Number(dateValue_array[2]);
    return myYear
}


function Get_Effected_Waiver_Dsc(waiverCode) {
    $.ajax({
        type: "POST",
        url: "PRG_LI_CLM_WAIVER.aspx/GetEffectedWaiverDsc",
        data: JSON.stringify({ waiverCode: waiverCode }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess_Effected_Waiver_Dsc,
        failure: OnFailure_Effected_Waiver_Dsc,
        error: OnError_Effected_Waiver_Dsc
    });
    // this avoids page refresh on button click
   return false;
}
function OnSuccess_Effected_Waiver_Dsc(response) {
    //debugger;
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    var admobjects = xml.find("Table");
    retrieve_Effected_Waiver_Dsc(admobjects);

}
// retrieve the values for branch
function retrieve_Effected_Waiver_Dsc(admobjects) {
    //debugger;
    $.each(admobjects, function() {
        var admobject = $(this);
        $('#HidWaiverDesc').val($(this).find("TBIL_COV_DESC").text())
        $('#HidWaiverCode').val($(this).find("TBIL_COV_CD").text())
        WaiverDes = $('#HidWaiverDesc').val().trim()
        WaiverCod = $('#HidWaiverCode').val().trim()
        $('#drpWaiverCodes').val(WaiverCod.trim());
    });
}
function OnError_Effected_Waiver_Dsc(response) {
    //debugger;
    var errorText = response.responseText;
}

function OnFailure_Effected_Waiver_Dsc(response) {
    //debugger;
    var errorText = response.responseText;
    alert('Failure!!!' + '<br/>' + errorText);
}
