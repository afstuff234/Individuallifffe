//TODO: validating numeric entries
$(function() {
    //    alert("working");
    $("#txtBasicSumClaimsLC").keypress(function(e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            alert("Invalid keyboard entry!");
            return false;
        }
    });

    $("#txtBasicSumClaimsFC").keypress(function(e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            alert("Invalid keyboard entry!");
            return false;
        }
        return false;
    });

    $("#txtAdditionalSumClaimsLC").keypress(function(e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            alert("Invalid keyboard entry!");
            return false;
        }
        return false;
    });

    $("#txtAdditionalSumClaimsFC").keypress(function(e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 46 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            alert("Invalid keyboard entry!");
            return false;
        }
        return false;
    });
});


//TODO: function to check if CHECK_IF_CLAIM_EXIST
$(function() {
   
    function getClaimFromClaimPaid() {
        $.ajax({
            type: "POST",
            url: "PageProcesses.aspx/CHECK_IF_CLAIM_EXIST",
            //    data: JSON.stringify({ policyNumber: polNum }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: onSucceccCm(response),
            failure: onFailureCm(response),
            error: onErrorCm(response)
        });
    }
    
    function onSucceccCm(response) {
        console.log(response);
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var admobjects = xml.find("Table");
        $("#lossTypeDdn").append("<option>--Select Loss Type--</option>");
        $.each(admobjects, function() {
            var admobject = $(this);
            var optionValue = "<option value=\"" + $(this).find("TBIL_COD_ITEM").text() + "\">" + $(this).find("TBIL_COD_SHORT_DESC").text() + "</option>";
            $("#lossTypeDdn").append(optionValue);
        });

    }

    function onErrorCm(response) {
        console.log(response);
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var admobjects = xml.find("Table");
        $("#lossTypeDdn").append("<option>--Select Loss Type--</option>");
        $.each(admobjects, function() {
            var admobject = $(this);
            var optionValue = "<option value=\"" + $(this).find("TBIL_COD_ITEM").text() + "\">" + $(this).find("TBIL_COD_SHORT_DESC").text() + "</option>";
            $("#lossTypeDdn").append(optionValue);
        });

    }

    function onFailureCm(response) {
        console.log(response);
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var admobjects = xml.find("Table");
        $("#lossTypeDdn").append("<option>--Select Loss Type--</option>");
        $.each(admobjects, function() {
            var admobject = $(this);
            var optionValue = "<option value=\"" + $(this).find("TBIL_COD_ITEM").text() + "\">" + $(this).find("TBIL_COD_SHORT_DESC").text() + "</option>";
            $("#lossTypeDdn").append(optionValue);
        });

    }

});


//TODO: PRG_LI_CLM_MATURE.aspx queries
$(function() {
    
});