<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PRG_LI_POL_RECEIPT_MULTIPLE.aspx.vb" Inherits="I_LIFE_PRG_LI_POL_RECEIPT_MULTIPLE" %>
<%@ Register src="../UC_BANT.ascx" tagname="UC_BANT" tagprefix="uc1" %>

<%@ Register src="../UC_FOOT.ascx" tagname="UC_FOOT" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Individual Life Module</title>
    <link rel="Stylesheet" href="../SS_ILIFE.css" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Script/ScriptJS.js">
    </script>
    <script language="javascript" type="text/javascript" src="../Script/SJS_02.js">
    </script>
        <script language="JavaScript" src="calendar_eu.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js"></script>

   
    <link rel="stylesheet" href="calendar.css" />
    <link href="css/general.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
    <link href="css/rounded.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.11.0.js" type="text/javascript"></script>

    <script src="js/jquery.simplemodal.js" type="text/javascript"></script>
    
    
    
    
    <style>
        .RecordOriginator
        {
            margin-left:100px;
        }
        .style1
        {
            height: 28px;
        }
        .style2
        {
            height: 35px;
        }
    </style>
    
    
    <script type="text/javascript">

        //warn before browser is closed or location is changed
        window.onbeforeunload = confirmExit;
        function confirmExit() {
            window.event.returnValue = 'If you navigate away from this page any unsaved changes will be lost!';
        }

        function cancelEvent(event) {
            window[event] = function() { null }

        }


        // calling jquery functions once document is ready
        $(document).ready(function() {
            var resultValueDR;
            var resultValueCR;
            var resultValue;
            var resultDesc;

            //Refresh screen with post back values
            function checkMode() {
                switch ($('#cmbMode').val()) {
                    case "C":
                        $('#ChequeRow').hide();
                        break;
                    default:
                        $('#ChequeRow').show();
                }

            }


            //Refresh screen with post back values
            function HideShow() {
                switch ($('#HidShowHide').val()) {
                    case "hide":
                        $('#notFound').hide();
                        $('#notFound1').hide();
                        break;
                    default:
                        $('#notFound').show();
                        $('#notFound1').show();
                }

            }

            function GetReceiptType() {

                $("#cmbReceiptType").val($("#txtReceiptCode").val());
            }

            function CheckReceiptType() {
                $('#txtReceiptCode').val($('#cmbReceiptType').val());
                switch ($('#cmbReceiptType').val()) {
                    case "D":
                        $('#lblRefNo').text('Proposal No');

                        break;
                    case "P":
                        $('#lblRefNo').text('Policy No');
                        break;
                    case "X":
                    case "L":
                    case "V":
                    case "R":
                    case "I":
                    case "T":
                    case "S":
                    case "G":
                    case "O":
                        $('#lblMainAcctCR').show();
                        $('#txtMainAcctCredit').show();
                        $('#txtMainAcctCreditDesc').show();
                        $('#MainAccountCreditSearch').show();
                        $('#lblSubAcctCR').show();
                        $('#txtSubAcctCredit').show();
                        $('#txtSubAcctCreditDesc').show();
                        break;
                    default:
                        $('#lblMainAcctCR').hide();
                        $('#txtMainAcctCredit').hide();
                        $('#MainAcctCreditAdd').hide();
                        $('#txtMainAcctCreditDesc').hide();
                        $('#MainAccountCreditSearch').hide();
                        $('#lblSubAcctCR').hide();
                        $('#txtSubAcctCredit').hide();
                        $('#txtSubAcctCreditDesc').hide();
                        $('#SubAccountCreditSearch').hide();
                        $('#SubAcctCreditAdd').hide();
                        $('#lblRefNo').text('Ref No');
                        break;

                }

            }

            checkMode();
            CheckReceiptType();
            HideShow();

            $('#txtReceiptAmtLC').on('focusout', function(e) {
                e.preventDefault();
                //                var currency = $('#txtReceiptAmtLC').val();
                //                var number = Number(currency.replace(/[^0-9\.]+/g, ""));
                //                $("#txtReceiptAmtLC").val(number);
                $('#txtReceiptAmtFC').attr('value', $('#txtReceiptAmtLC').val());
            });

            $('#cmbMode').on('focusout', function(e) {
                e.preventDefault();
                $('#txtMode').val($('#cmbMode').val());
                checkMode();

                //return false;
            });

            $('#cmbCurrencyType').on('focusout', function(e) {
                e.preventDefault();
                $('#txtCurrencyCode').val($('#cmbCurrencyType').val());
            });


            $('#cmbBranchCode').on('focusout', function(e) {
                e.preventDefault();
                $('#txtBranchCode').val($('#cmbBranchCode').val());
            });

            $('#cmbReceiptType, #cmbCommissions').on('focusout', function(e) {
                e.preventDefault();
                CheckReceiptType();
            });


            $('#cboProductClass').on('focusout', function(e) {
                e.preventDefault();
                if ($('#cboProductClass').val() != "0") {
                    $('#txtProductClass').val($('#cboProductClass').val());
                    GetProductClass();
                }
            });

            //FORMAT EFFECTIVE DATE AUTOMATICALLY. Make the slashes jump into place

            $("#txtEffectiveDate").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtEffectiveDate").val() != "") {
                    var effDate = $("#txtEffectiveDate").val();
                    effDateLen = effDate.length
                    if (effDateLen == 8 && $.isNumeric(effDate)) {
                        $("#txtEffectiveDate").val(FormatDateAuto(effDate))
                    }
                    else if (effDateLen != 8 && $.isNumeric(effDate)) {
                        alert("Auto date format allows only 8 digit numbers (ddmmyyyy)");
                        $("#txtEffectiveDate").focus();
                    }
                }
                //return false;
            });

            //Format Cheque date Automatically
            $("#txtChequeDate").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtChequeDate").val() != "") {
                    var effDate = $("#txtChequeDate").val();
                    effDateLen = effDate.length
                    if (effDateLen == 8 && $.isNumeric(effDate)) {
                        $("#txtChequeDate").val(FormatDateAuto(effDate))
                    }
                }
                //return false;
            });

            $("#txtBatchNo").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtBatchNo").val() != "") {
                    var BatchNo = $("#txtBatchNo").val();
                    BatchNoLen = BatchNo.length
                    if ($.isNumeric(BatchNo)) {
                        if (BatchNoLen != 6) {
                            alert("Invalid batch date");
                            $("#txtBatchNo").focus();
                        }
                        else {
                            var lastTwoDigit = BatchNo.substring(4);
                            if (Number(lastTwoDigit) >= 1 && Number(lastTwoDigit) <= 12) {
                            }
                            else {
                                alert("Batch date month part is invalid")
                                $("#txtBatchNo").focus();
                            }
                        }
                    }
                    else {
                        alert("Batch date contains non numeric character")
                        $("#txtBatchNo").focus();
                    }
                }
                //return false;
            });


            //retrieve data on focus loss
            $("#txtMainAcctDebit").on('focusout', function(e) {
                e.preventDefault();
                $("#txtMainAcctDebitDesc").val('')
                $("#txtSubAcctDebitDesc").val('')
                if ($("#txtMainAcctDebit").val() != "" && $("#txtSubAcctDebit").val() != "")
                    LoadChartInfo("txtSubAcctDebit", "txtMainAcctDebit", "Main", "DR");
                $("#txtSubAcctDebit").focus();
                //return false;
            });

            //retrieve data on focus loss
            $("#txtSubAcctDebit").on('focusout', function(e) {
                e.preventDefault();
                $("#txtMainAcctDebitDesc").val('')
                $("#txtSubAcctDebitDesc").val('')
                if ($("#txtSubAcctDebit").val() != "" && $("#txtMainAcctDebit").val() != "")
                    LoadChartInfo("txtSubAcctDebit", "txtMainAcctDebit", "Sub", "DR");
                //return false;
            });


            //retrieve data on focus loss
            $("#txtMainAcctCredit").on('focusout', function(e) {
                $("#txtMainAcctCreditDesc").val('')
                $("#txtSubAcctCreditDesc").val('')
                e.preventDefault();
                if ($("#txtMainAcctCredit").val() != "" && $("#txtSubAcctCredit").val() != "")
                //LoadChartInfo("txtSubAcctDebit", "txtMainAcctCredit", "Main", "CR");
                    LoadChartInfo("txtSubAcctCredit", "txtMainAcctCredit", "Main", "CR");
                $("#txtSubAcctCredit").focus();
                //return false;
            });

            //retrieve data on focus loss
            $("#txtSubAcctCredit").on('focusout', function(e) {
                e.preventDefault();

                if ($("#txtSubAcctCredit").val() != "" && $("#txtMainAcctCredit").val() != "")
                //  LoadChartInfo("txtSubAcctCredit", "txtMainAcctDebit", "Sub", "CR");
                    LoadChartInfo("txtSubAcctCredit", "txtMainAcctCredit", "Sub", "CR");
                //return false;
            });

            //retrieve data on focus loss
            $("#txtReceiptRefNo").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtReceiptRefNo").val() != "" && ($("#lblRefNo").text() == "Proposal No" || $("#lblRefNo").text() == "Policy No")) {
                    document.getElementById('txtInsuredCode').value = "";
                    document.getElementById('txtAgentCode').value = ""; ;
                    document.getElementById('txtAssuredName').value = ""; ;
                    document.getElementById('txtAssuredAddress').value = ""; ;
                    document.getElementById('txtPayeeName').value = "";
                    document.getElementById('txtPolRegularContrib').value = "0.00";
                    document.getElementById('txtPolRegularContribH').value = "0.00";
                    document.getElementById('txtAgentName').value = "";
                    document.getElementById('txtMOP').value = "";
                    document.getElementById('txtMOPDesc').value = "";
                    document.getElementById('txtPolicyEffDate').value = "";
                    document.getElementById('txtFileNo').value = "";
                    document.getElementById('txtProductCode').value = "";
                    document.getElementById('txtTempPolNo').value = $("#txtReceiptRefNo").val()

                    LoadPolicyInfoObject();

                }
                return false;

            });

            //retrieve data on focus loss branches

            $("#txtBranchCode").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtBranchCode").val() != "")
                    LoadBranchInfoObject();
                //return false;
            });

            //retrieve data on focus loss currency

            $("#txtCurrencyCode").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtCurrencyCode").val() != "")
                    LoadCurrencyObject();
                //return false;
            });


            $("#txtReceiptCode").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtReceiptCode").val() != "") {
                    GetReceiptType();
                }
                //return false;
            });

            //Get receipt mode description
            $("#txtMode").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtMode").val() != "") {
                    GetReceiptMode();
                }
                //return false;
            });

            //            $("#cmbReceiptType").on('focusout', function(e) {
            //                e.preventDefault();
            //                if ($("#cmbReceiptType").val() != "")
            //                    $('#txtReceiptCode').val($("#cmbReceiptType").val())

            //                //return false;
            //            });

            $("#txtProductClass").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtProductClass").val() != "") {
                    GetProductClass();
                }
                //return false;
            });

            //            $('#cboProduct').change(function(e) {
            //                e.preventDefault();
            //                $('#txtProduct').val($('#cboProduct').val());
            //            });

            $('#cboProduct').on('focusout', function(e) {
                e.preventDefault();
                $('#txtProduct').val($('#cboProduct').val());
            });

            $("#txtProduct").on('focusout', function(e) {
                e.preventDefault();
                if ($("#txtProduct").val() != "") {
                    GetProduct();
                }
                //return false;
            });


            //call popup to browse the Policies
            $('#imgReceiptRefNo').click(function(e) {
                e.preventDefault();
                var src = "\PolicyBrowse.aspx";
                $.modal('<iframe id="simplemodal-container" src="' + src + '" height="500" width="830" style="border:0">', {
                    closeHTML: "<a  class='modalCloseImg' href='#'></a>",
                    containerCss: {
                        backgroundColor: "#fff",
                        borderColor: "#fff",
                        height: 500,
                        padding: 0,
                        width: 830
                    },
                    appendTo: 'form',
                    persist: true,
                    overlayClose: true,
                    opacity: 30,
                    overlayCss: { backgroundColor: "black" },
                    onClose: function(dialog) {

                        var resultPolicy = $("iframe[src='PolicyBrowse.aspx']").contents().find("#txtValue").val();
                        var resultProposal = $("iframe[src='PolicyBrowse.aspx']").contents().find("#txtValue1").val();
                        //   alert(resultPolicy + "--" + resultProposal);

                        switch ($('#cmbReceiptType').val()) {
                            case "D":
                                if (resultProposal.length > 0)
                                // $('#txtReceiptRefNo').attr('value', resultProposal); // proposal code
                                    document.getElementById('txtReceiptRefNo').value = resultProposal;

                                break;
                            case "P":

                                if (resultPolicy.length > 0)
                                //$('#txtReceiptRefNo').attr('value', resultPolicy); // policy code
                                    document.getElementById('txtReceiptRefNo').value = resultPolicy;
                                break;
                            default:
                                //$('#lblRefNo').text('Ref No');
                                break;
                        }


                        dialog.data.fadeOut('200', function() {
                            dialog.container.slideUp('200', function() {
                                dialog.overlay.fadeOut('200', function() {
                                    $.modal.close();
                                });
                            });
                        });
                    }
                });
            });


            //call print receipt popup 
            $('#butPrint').click(function(e) {
                e.preventDefault();
                //copy content of receipt number to the print dialog receipt number
                $('#txtRecptNo').attr('value', document.getElementById('txtReceiptNo').value)

                $('#PrintDialog').css({ display: true });
                $('#PrintDialog').modal({
                    containerCss: {
                        backgroundColor: "#fff",
                        borderColor: "#fff",
                        height: 400,
                        padding: 5,
                        width: 500
                    },
                    appendTo: 'form',
                    persist: true,
                    overlayClose: false,
                    opacity: 40,
                    overlayCss: { backgroundColor: "black" }

                }
                      );
            });
            //call popup to add to the main account
            $('#MainAcctDebitAdd, #SubAcctDebitAdd,#MainAcctCreditAdd,#SubAcctCreditAdd').click(function(e) {
                e.preventDefault();
                var src = "\ChartOfAccountsEntryBrowse.aspx";
                $.modal('<iframe id="simplemodal-container" src="' + src + '" height="500" width="1100" style="border:0">', {
                    closeHTML: "<a  class='modalCloseImg' href='#'></a>",
                    containerCss: {
                        backgroundColor: "#fff",
                        borderColor: "#fff",
                        height: 500,
                        padding: 0,
                        width: 1100
                    },
                    appendTo: 'form',
                    persist: true,
                    overlayClose: true,
                    opacity: 50,
                    overlayCss: { backgroundColor: "black" },
                    onClose: function(dialog) {

                        dialog.data.fadeOut('200', function() {
                            dialog.container.slideUp('200', function() {
                                dialog.overlay.fadeOut('200', function() {
                                    $.modal.close();
                                });
                            });
                        });
                    }
                });
            });


            //call popup to browse the main account
            $('#MainAccountDebitSearch').click(function(e) {
                e.preventDefault();
                var src = "\AccountChartBrowse.aspx";
                $.modal('<iframe id="simplemodal-container" src="' + src + '" height="500" width="830" style="border:0">', {
                    closeHTML: "<a  class='modalCloseImg' href='#'></a>",
                    containerCss: {
                        backgroundColor: "#fff",
                        borderColor: "#fff",
                        height: 500,
                        padding: 0,
                        width: 830
                    },
                    appendTo: 'form',
                    persist: true,
                    overlayClose: true,
                    opacity: 30,
                    overlayCss: { backgroundColor: "black" },
                    onClose: function(dialog) {

                        var resultValueDR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtValue").val();
                        var resultDescDR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtDesc").val();

                        if (resultValueDR.length > 0)
                        //$('#txtMainAcctDebit').attr('value', resultValueDR); // Main account code
                            $('#txtMainAcctDebit').val(resultValueDR)
                        if (resultDescDR.length > 0)
                            $('#txtMainAcctDebitDesc').attr('value', resultDescDR); // Main account description
                        $('#txtMainAcctDebitDescH').attr('value', resultDescDR); // Main account description hidden field
                        var resultValSubDR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtValue1").val();

                        var resultDescSubDR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtDesc1").val();
                        if (resultValSubDR.length > 0)
                            document.getElementById('txtSubAcctDebit').value = resultValSubDR
                        if (resultDescSubDR.length > 0)
                            $('#txtSubAcctDebitDesc').attr('value', resultDescSubDR);


                        dialog.data.fadeOut('200', function() {
                            dialog.container.slideUp('200', function() {
                                dialog.overlay.fadeOut('200', function() {
                                    $.modal.close();
                                });
                            });
                        });
                    }
                });
            });

            //call popup to browse the sub account
            $('#SubAccountDebitSearch').click(function(e) {
                e.preventDefault();
                //var src = "\AccountChartBrowse.aspx?MainAcct=" + $('#txtMainAcctDebit').val();
                //               var src = "\AccountChartBrowse.aspx?MainAcct=" + $('#txtMainAcctDebit').val();
                var src = "\AccountChartBrowse.aspx";
                $.modal('<iframe id="simplemodal-container" src="' + src + '" height="500" width="830" style="border:0">', {
                    closeHTML: "<a  class='modalCloseImg' href='#'></a>",
                    containerCss: {
                        backgroundColor: "#fff",
                        borderColor: "#fff",
                        height: 500,
                        padding: 0,
                        width: 830
                    },
                    appendTo: 'form',
                    persist: true,
                    overlayClose: true,
                    opacity: 30,
                    overlayCss: { backgroundColor: "black" },
                    onClose: function(dialog) {

                        var resultValSubDR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtValue1").val();

                        var resultDescSubDR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtDesc1").val();
                        if (resultValSubDR.length > 0)
                            document.getElementById('txtSubAcctDebit').value = resultValSubDR
                        if (resultDescSubDR.length > 0)
                            $('#txtSubAcctDebitDesc').attr('value', resultDescSubDR);

                        dialog.data.fadeOut('200', function() {
                            dialog.container.slideUp('200', function() {
                                dialog.overlay.fadeOut('200', function() {
                                    $.modal.close();
                                });
                            });
                        });
                    }

                });
            });

            //call popup to browse
            $('#MainAccountCreditSearch').click(function(e) {
                e.preventDefault();
                var src = "\AccountChartBrowse.aspx";
                $.modal('<iframe id="simplemodal-container" src="' + src + '" height="500" width="830" style="border:0">', {
                    closeHTML: "<a  class='modalCloseImg' href='#'></a>",
                    onOpen: function(dialog) {
                        dialog.overlay.fadeIn('slow', function() {
                            dialog.container.slideDown('100', function() {
                                dialog.data.fadeIn('fast');
                            });
                        });
                    },
                    preventDefault: true,
                    containerCss: {
                        backgroundColor: "#fff",
                        borderColor: "#fff",
                        height: 500,
                        padding: 0,
                        width: 830
                    },
                    appendTo: 'form',
                    persist: true,
                    overlayClose: true,
                    opacity: 30,
                    overlayCss: { backgroundColor: "black" },
                    onClose: function(dialog) {

                        var resultValueCR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtValue").val();
                        var resultDescCR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtDesc").val();
                        if (resultValueCR.length > 0)
                            $('#txtMainAcctCredit').attr('value', resultValueCR);

                        if (resultDescCR.length > 0)
                            $('#txtMainAcctCreditDesc').attr('value', resultDescCR);

                        var resultValSubCR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtValue1").val();
                        var resultDescSubCR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtDesc1").val();

                        if (resultValSubCR.length > 0)
                            $('#txtSubAcctCredit').attr('value', resultValSubCR);

                        if (resultDescSubCR.length > 0)
                            $('#txtSubAcctCreditDesc').attr('value', resultDescSubCR);

                        dialog.data.fadeOut('200', function() {
                            dialog.container.slideUp('200', function() {
                                dialog.overlay.fadeOut('200', function() {
                                    $.modal.close();
                                });
                            });
                        });
                    }
                });
            });

            //call popup to browse the sub account
            $('#SubAccountCreditSearch').click(function(e) {
                e.preventDefault();
                // var src = "\AccountChartBrowse.aspx?MainAcct=" + $('#txtMainAcctCredit').val();
                var src = "\AccountChartBrowse.aspx";
                $.modal('<iframe id="simplemodal-container" src="' + src + '" height="500" width="830" style="border:0">', {
                    closeHTML: "<a  class='modalCloseImg' href='#'></a>", containerCss: {
                        backgroundColor: "#fff",
                        borderColor: "#fff",
                        height: 500,
                        padding: 0,
                        width: 830
                    },
                    appendTo: 'form',
                    persist: true,
                    overlayClose: true,
                    opacity: 30,
                    overlayCss: { backgroundColor: "black" },
                    onClose: function(dialog) {

                        var resultValSubCR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtValue1").val();
                        var resultDescSubCR = $("iframe[src='AccountChartBrowse.aspx']").contents().find("#txtDesc1").val();

                        if (resultValSubCR.length > 0)
                            $('#txtSubAcctCredit').attr('value', resultValSubCR);

                        if (resultDescSubCR.length > 0)
                            $('#txtSubAcctCreditDesc').attr('value', resultDescSubCR);

                        dialog.data.fadeOut('200', function() {
                            dialog.container.slideUp('200', function() {
                                dialog.overlay.fadeOut('200', function() {
                                    $.modal.close();
                                });
                            });
                        });
                    }
                });
            });

            //retrieve data on focus loss
            /* $("#txtTransDesc1").on('focusout', function(e) {
            e.preventDefault();
            if ($("#txtTransDesc1").val() != "")
            LoadPeriodsCover();
            return false;
            });*/

            //retrieve data on focus
            // $("#txtTransDesc1").on('focusout', function(e) {
            //var keyCode = e.keyCode || e.which;
            //trap the TAB key
            //if (keyCode == 9) {
            //    e.preventDefault();
            // call custom function here
            //LoadMOPPremium();
            // return false;
            //   }
            //});


            //loading screen functionality - this part is additional - start
            $("#divTable").ajaxStart(OnAjaxStart);
            $("#divTable").ajaxError(OnAjaxError);
            $("#divTable").ajaxSuccess(OnAjaxSuccess);
            $("#divTable").ajaxStop(OnAjaxStop);
            $("#divTable").ajaxComplete(OnAjaxComplete);
            //loading screen functionality - this part is additional - end



            // ajax call to load account chart information
            function LoadChartInfo(subaccountcode, mainaccountcode, ctype, drcr) {
                console.log(document.getElementById(subaccountcode).value)
                console.log(document.getElementById(mainaccountcode).value)
                $.ajax({
                    type: "POST",
                    url: "PRG_FIN_RECPT_ISSUE.aspx/GetAccountChartDetails",
                    //                data: JSON.stringify({ _accountcode: document.getElementById(accountcode).value, _type: ctype }),
                    data: JSON.stringify({ _accountsubcode: document.getElementById(subaccountcode).value, _accountmaincode: document.getElementById(mainaccountcode).value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        var xmlDoc = $.parseXML(data.d);
                        var xml = $(xmlDoc);
                        var accountcharts = xml.find("Table");
                        retrieve_AccountChartInfoValues(accountcharts, ctype, drcr)
                        if (ctype == "Sub" && drcr == "DR") {
                            $("#txtMainAcctCredit").focus();
                            $("#txtSubAcctDebit").val("000000");
                        }
                        //retrieve_AccountChartInfoValues(accountcharts, drcr)
                    },
                    failure: OnFailure,
                    //error: OnError_LoadChartInfo(ctype, drcr)
                    error: function() {
                        alert('Error!: Account Chart could not be Retrieved. Parameters sent is empty or invalid. Please Re-Confirm' + '<br/>');
                        if (ctype == "Sub" && drcr == "DR") {
                            $("#txtMainAcctDebit").focus();
                            $("#txtSubAcctDebit").val("000000");
                        }
                        else if (ctype == "Sub" && drcr == "CR") {
                            $("#txtMainAcctCredit").focus();
                            $("#txtSubAcctCredit").val("000000");
                        }
                    }
                });
                // this avoids page refresh on button click
                return false;
            }
            // retrieve the values and
            function retrieve_AccountChartInfoValues(accountcharts, ctype, drcr) {
                //debugger;
                $.each(accountcharts, function() {
                    var accountchart = $(this);

                    if (ctype == "Main" && drcr == "DR") {

                        document.getElementById('txtMainAcctDebitDesc').value = $(this).find("sMainDesc").text()
                        document.getElementById('txtMainAcctDebitDescH').value = $(this).find("sMainDesc").text()
                        // document.getElementById('txtSubAcctDebit').value = $(this).find("sSubCode").text()
                        // document.getElementById('txtSubAcctDebitDesc').value = $(this).find("sSubDesc").text()
                        document.getElementById('txtSubAcctDebit').value = $(this).find("sSubCode").text()
                        document.getElementById('txtSubAcctDebitDesc').value = $(this).find("sSubDesc").text()
                    }
                    else if (ctype == "Main" && drcr == "CR") {
                        document.getElementById('txtMainAcctCreditDesc').value = $(this).find("sMainDesc").text()
                        //  document.getElementById('txtSubAcctCredit').value = $(this).find("sSubCode").text()
                        //  document.getElementById('txtSubAcctCreditDesc').value = $(this).find("sSubDesc").text()

                        document.getElementById('txtSubAcctCredit').value = $(this).find("sSubCode").text()
                        document.getElementById('txtSubAcctCreditDesc').value = $(this).find("sSubDesc").text()
                    }
                    else if (ctype == "Sub" && drcr == "DR") {

                        document.getElementById('txtSubAcctDebit').value = $(this).find("sSubCode").text()
                        document.getElementById('txtSubAcctDebitDesc').value = $(this).find("sSubDesc").text()
                        document.getElementById('txtMainAcctDebit').value = $(this).find("sMainCode").text()
                        document.getElementById('txtMainAcctDebitDesc').value = $(this).find("sMainDesc").text()
                        document.getElementById('txtMainAcctDebitDescH').value = $(this).find("sMainDesc").text()

                    }
                    else if (ctype == "Sub" && drcr == "CR") {
                        document.getElementById('txtSubAcctCredit').value = $(this).find("sSubCode").text()
                        document.getElementById('txtSubAcctCreditDesc').value = $(this).find("sSubDesc").text()
                        document.getElementById('txtMainAcctCredit').value = $(this).find("sMainCode").text()
                        document.getElementById('txtMainAcctCreditDesc').value = $(this).find("sMainDesc").text()
                    }

                });
            }


            // ajax call to load policy information
            function LoadBranchInfoObject() {
                $.ajax({
                    type: "POST",
                    url: "PRG_FIN_RECPT_ISSUE.aspx/GetBranchInformation",
                    data: JSON.stringify({ _branchcode: document.getElementById('txtBranchCode').value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_LoadBranchInfoObject,
                    failure: OnFailure,
                    error: OnError_LoadBranchInfoObject
                });
                // this avoids page refresh on button click
                return false;
            }
            function OnSuccess_LoadBranchInfoObject(response) {
                //debugger;

                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var branches = xml.find("Table");
                retrieve_BranchInfoValues(branches);

            }
            // retrieve the values for branch
            function retrieve_BranchInfoValues(branches) {
                //debugger;
                $.each(branches, function() {
                    var branch = $(this);
                    $("#cmbBranchCode").val($(this).find("sCode").text())
                });
            }


            // ajax call to load policy information
            function LoadPolicyInfoObject() {
                $.ajax({
                    type: "POST",
                    url: "PRG_FIN_RECPT_ISSUE.aspx/GetPolicyInformation",
                    data: JSON.stringify({ _polnum: document.getElementById('txtReceiptRefNo').value, _type: document.getElementById('cmbReceiptType').value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_LoadPolicyInfoObject,
                    failure: OnFailure,
                    error: OnError_LoadPolicyInfoObject
                });
                // this avoids page refresh on button click

                return false;
            }
            // on sucess get the xml
            function OnSuccess_LoadPolicyInfoObject(response) {
                //debugger;
                $('#HidShowHide').val("hide");
                HideShow()
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var policyholders = xml.find("Table");
                retrieve_PolicyInfoValues(policyholders);

            }
            // retrieve the values and
            function retrieve_PolicyInfoValues(policyholders) {
                //debugger;
                $.each(policyholders, function() {
                    var policyholder = $(this);
                    document.getElementById('txtInsuredCode').value = $(this).find("TBIL_POLY_ASSRD_CD").text();
                    document.getElementById('txtAgentCode').value = $(this).find("TBIL_POLY_AGCY_CODE").text();
                    document.getElementById('txtAssuredName').value = $(this).find("Insured_Name").text();
                    document.getElementById('txtAssuredAddress').value = $(this).find("Insured_Address").text();
                    document.getElementById('txtPayeeName').value = $(this).find("Insured_Name").text();
                    document.getElementById('txtPolRegularContrib').value = $(this).find("TBIL_POL_PRM_DTL_MOP_PRM_LC").text();
                    document.getElementById('txtPolRegularContribH').value = $(this).find("TBIL_POL_PRM_DTL_MOP_PRM_LC").text();


                    document.getElementById('txtAgentName').value = $(this).find("Agent_Name").text();
                    document.getElementById('txtMOP').value = $(this).find("Payment_Mode").text();
                    document.getElementById('txtMOPDesc').value = $(this).find("Payment_Mode_Desc").text();
                    document.getElementById('txtPolicyEffDate').value = $(this).find("TBIL_POLICY_EFF_DT").text();
                    document.getElementById('txtFileNo').value = $(this).find("File_No").text();
                    document.getElementById('txtProductCode').value = $(this).find("Product_Code").text();

                    //                if ($(this).find("TBIL_POLY_ASSRD_CD").text() == "" || $(this).find("TBIL_POLY_AGCY_CODE").text() == "" || $(this).find("TBIL_POL_PRM_DTL_MOP_PRM_LC").text() == "0.00" || $(this).find("Payment_Mode").text() == "") {
                    //                    alert("Please contact technical department, record not completed for " + policy + " no " + document.getElementById('txtReceiptRefNo').value);
                    //                    $("#txtReceiptRefNo").focus();
                    //                }

                });

            }

            // ajax call to load receipts cover details
            function LoadPeriodsCover() {
                $.ajax({
                    type: "POST",
                    url: "PRG_FIN_RECPT_ISSUE.aspx/PaymentsPeriodCover",
                    data: JSON.stringify({ _polnum: document.getElementById('txtReceiptRefNo').value,
                        _mop: document.getElementById('txtMOP').value,
                        _effdate: document.getElementById('txtPolicyEffDate').value,
                        _contrib: document.getElementById('txtPolRegularContrib').value,
                        _amtpaid: document.getElementById('txtReceiptAmtLC').value
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_LoadPeriodsCover,
                    failure: OnFailure_LoadPeriodsCover,
                    error: OnError_LoadPeriodsCover
                });
                // this avoids page refresh on button click
                return false;
            }
            // on sucess get the xml
            function OnSuccess_LoadPeriodsCover(response) {
                //debugger;
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var coverdets = xml.find("Table");
                retrieve_LoadPeriodsCover(coverdets)

            }
            // retrieve the values and
            function retrieve_LoadPeriodsCover(coverdets) {
                //debugger;
                $.each(coverdets, function() {
                    var coverdet = $(this);

                    document.getElementById('txtTransDesc2').value = $(this).find("sPeriodsCoverRange").text()
                });
            }
            function OnFailure_LoadPeriodsCover(response) {
                //debugger;
                //alert('Failure!!!' + '<br/>' + response.reponseText);
                alert('Failure!: Periods Covered Failed. Parameters sent is empty or invalid. Please Re-Confirm' + '<br/>');
            }

            // function OnError_LoadChartInfo(response) {
            function OnError_LoadChartInfo(ctype, drcr) {
                //debugger;
                //var errorText = response.responseText;
                //alert('Error!!!' + '\n\n' + errorText);
                alert('Error!: Account Chart could not be Retrieved. Parameters sent is empty or invalid. Please Re-Confirm' + '<br/>');
            }
            function OnError_LoadBranchInfoObject(response) {
                //debugger;
                //var errorText = response.responseText;
                //alert('Error!!!' + '\n\n' + errorText);
                alert('Error!: Branch Infomation Not Found. Parameters Empty or Invalid. Please Re-Confirm' + '<br/>');
                $('#txtBranchCode').focus();
            }
            function OnError_LoadPolicyInfoObject(response) {
                $('#HidShowHide').val("show");
                HideShow();
            }
            function OnError_LoadPeriodsCover(response) {
                //debugger;
                //var errorText = response.responseText;
                //alert('Error!!!' + '\n\n' + errorText);
                alert('Error!: Load Periods Cover Not Found. Parameters Empty or Invalid. Please Re-Confirm' + '<br/>');
            }
            function OnError_LoadCurrencyObject(response) {
                //debugger;
                //var errorText = response.responseText;
                //alert('Error!!!' + '\n\n' + errorText);
                alert('Error!: Currency Code Not Found. Parameters Empty or Invalid. Please Re-Confirm' + '<br/>');
                $('#txtCurrencyCode').focus();
            }


            // loading screen functionality functions - this part is additional - start
            function OnAjaxStart() {
                //debugger;
                //alert('Starting...');
                $("#divLoading").css("display", "block");
            }
            function OnFailure(response) {
                //debugger;
                alert('Failure!!!' + '<br/>' + response.reponseText);
            }
            function OnError(response) {
                //debugger;
                var errorText = response.responseText;
                alert('Error!!!' + '\n\n' + errorText);
            }
            function OnAjaxError() {
                //debugger;
                alert('Error!: Invalid Ajax Call');
            }
            function OnAjaxSuccess() {
                //debugger;
                //alert('Sucess!!!');
                $("#divLoading").css("display", "none");
            }
            function OnAjaxStop() {
                //debugger;
                //alert('Stop!!!');
                $("#divLoading").css("display", "none");
            }
            function OnAjaxComplete() {
                //debugger;
                //alert('Completed!!!');
                $("#divLoading").css("display", "none");
            }
            // loading screen functionality functions - this part is additional - end


            // ajax call to load currency type
            function LoadCurrencyObject() {
                $.ajax({
                    type: "POST",
                    url: "PRG_FIN_RECPT_ISSUE.aspx/GetCurrencyInformation",
                    data: JSON.stringify({ _currencycode: document.getElementById('txtCurrencyCode').value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_LoadCurrencyObject,
                    failure: OnFailure,
                    error: OnError_LoadCurrencyObject
                });
                // this avoids page refresh on button click
                return false;
            }
            function OnSuccess_LoadCurrencyObject(response) {
                //debugger;

                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var currencies = xml.find("Table");
                retrieve_LoadCurrencyObject(currencies);

            }
            // retrieve the values for currency
            function retrieve_LoadCurrencyObject(currencies) {
                //debugger;
                $.each(currencies, function() {
                    var currency = $(this);
                    $("#cmbCurrencyType").val($(this).find("TBIL_COD_ITEM").text())
                });
            }


            function GetReceiptMode() {
                receiptmode = $('#txtMode').val()
                if (receiptmode == "C") {
                    $("#cmbMode").val('C');
                }
                else if (receiptmode == "Q") {
                    $("#cmbMode").val('Q');
                }
                else if (receiptmode == "D") {
                    $("#cmbMode").val('D');
                }
                else if (receiptmode == "T") {
                    $("#cmbMode").val('T');
                }
                else if (receiptmode == "C") {
                    $("#cmbMode").val('C');
                }
                else {
                    alert("Receipt mode not found");
                    $("#cmbMode").val(0);
                }
            }

            function GetProductClass() {
                productclass = $('#txtProductClass').val();
                $('#txtProduct').val("");
                $("#cboProductClass").val(productclass);
                $.ajax({
                    type: "POST",
                    url: "PRG_FIN_RECPT_ISSUE.aspx/GetProductByCatCodeClient",
                    data: JSON.stringify({ _catCode: document.getElementById('txtProductClass').value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_GetProductByCatCodeClient,
                    failure: OnFailure,
                    error: OnError_GetProductByCatCodeClient
                });
                // this avoids page refresh on button click
                return false;
            }

            // on sucess get the xml
            function OnSuccess_GetProductByCatCodeClient(response) {
                //debugger;
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var productdets = xml.find("Table");
                retrieve_ProductDetails(productdets);

            }
            // retrieve the values and
            function retrieve_ProductDetails(productdets) {
                //debugger;

                $('#cboProduct').empty();
                $('#cboProduct')
            .append('<option value="0">' + "Select" + '</option>')
                $.each(productdets, function() {
                    var productdet = $(this);
                    //document.getElementById("drpWaiverCodes").value = $(this).find("TBIL_COV_CD").text();
                    $('#cboProduct')
            .append('<option value=' + $(this).find("TBIL_PRDCT_DTL_CODE").text() + '>' +
              $(this).find("TBIL_PRDCT_DTL_CODE").text() + '-' + $(this).find("TBIL_PRDCT_DTL_DESC").text() + '</option>')
                });


            }
            function OnError_GetProductByCatCodeClient(response) {
                alert('Error!: Product Category not found. Please re-try' + '<br/>');
                $('#txtProductClass').focus();
                $('#cboProductClass').val(0)
            }


            function GetProduct() {
                product = $('#txtProduct').val()
                $("#cboProduct").val(product);

            }
            function FormatDateAuto(effDate) {
                var effDateDay = effDate.substring(0, 2);
                var effDateMonth = effDate.substring(2, 4);
                var effDateYear = effDate.substring(4);
                return effDateDay + "/" + effDateMonth + "/" + effDateYear;
            }
        });         //Document ready ends here

       
    </script>    
</head>

<body onload="<%= FirstMsg %>">
    <form id="Form1" name="Form1" runat="server">

    <!-- start banner -->
    <div id="div_banner" align="center">
    
        <uc1:UC_BANT ID="UC_BANT1" runat="server" />
    
    </div>


    <!-- start header -->
    <div id="div_header" align="center">
        <table id="tbl_header" align="center">
            <tr>
                <td align="left" valign="top" class="myMenu_Title_02">
                    <table border="0" width="100%">

                        <tr style="display: none;">
                            <td align="left" colspan="2" valign="top"><%=STRMENU_TITLE%></td>
                            <td align="right" colspan="2" valign="top">    
                                &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" ForeColor="Gray" runat="server" EnableViewState="False" Width="50px"></asp:textbox>&nbsp;&nbsp;Find:&nbsp;
                                <input type="text" id="txtSearch" name="txtSearch" value="Search..." runat="server"
                                    onfocus="if (this.value == 'Search...') {this.value = '';}"
                                    onblur="if (this.value == '') {this.value = 'Search...';}" />
                                &nbsp;&nbsp;<asp:Button ID="cmdSearch" Text="Search" runat="server" />
    	                        &nbsp;&nbsp;<asp:DropDownList ID="cboSearch" runat="server" Height="26px" 
                                    Width="150px"></asp:DropDownList>
                            </td>
                        </tr>

                                    <tr style="display: none;">
                                        <td align="left" colspan="4" valign="top"><hr /></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" valign="top">
                                            &nbsp;&nbsp;<a href="#" onclick="javascript:JSDO_RETURN('PRG_LI_PROP_POLICY.aspx?menu=IL_QUOTE')">Go to Menu</a>
                                            &nbsp;&nbsp;<asp:Button ID="cmdPrev" CssClass="cmd_butt" Enabled="false" Text="«..Previous" runat="server" />
                                            &nbsp;&nbsp;<asp:button id="cmdNew_ASP" Enabled="false" CssClass="cmd_butt" runat="server" text="New Data" OnClientClick="JSNew_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdSave_ASP" CssClass="cmd_butt" runat="server" text="Save Data"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdDelete_ASP" CssClass="cmd_butt" Enabled="false"  runat="server" text="Delete Data" OnClientClick="JSDelete_ASP();"></asp:button>
                                            &nbsp;&nbsp;<asp:button id="cmdPrint_ASP" CssClass="cmd_butt" Enabled="False" runat="server" text="Print"></asp:button>
                                            &nbsp;&nbsp;<asp:Button ID="cmdNext" CssClass="cmd_butt" Enabled="false" Text="Next..»" runat="server" />
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>

                        
                    </table>                    
                </td>
            </tr>
        </table>
    </div>


    <!-- start content -->
    <div id="div_content" align="center">
        <table class="tbl_cont" align="center">
                <tr>
                    <td nowrap class="myheader">Receipt Payment Information</td>
                </tr>
                <tr>
                    <td align="center" valign="top" class="td_menu">
                        <table align="center" border="0" class="tbl_menu_new">
                                            <tr>
                                                <td align="left" colspan="4" valign="top">
                                                    <asp:Label ID="lblMsg" ForeColor="Red" Font-Size="Small" runat="server"></asp:Label>
                                                    <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Red" Visible="false"> </asp:Label>
                                                </td>
                                            </tr>
                             <tr>
                                                <td nowrap align="left" valign="top"><asp:CheckBox ID="chkReceiptNo" runat="server" AutoPostBack="True" Text="Receipt Number" />
                                                    &nbsp;</td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtReceiptNo" runat="server" Width="150px" Enabled="False" AutoPostBack="True"></asp:TextBox>
                                                    &nbsp;&nbsp;</td>
                                                <td align="left" valign="top"><asp:Label ID="lblEntryDate" Text="Entry Date:" Enabled="true" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtEntryDate" runat="server" Width="150px" Enabled="false"></asp:TextBox></td>
                            </tr>                                            

                            <tr>
                                                <td nowrap align="left" valign="top">&nbsp;<asp:Label ID="lblFileNum" Enabled="true" Text="File No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtFileNo" Enabled="false" Width="250px" runat="server"></asp:TextBox>
                                                    &nbsp;&nbsp;</td>
                                                <td align="left" valign="top"><asp:Label ID="lblPolNum" Text="Policy Number:" Enabled="true" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtPolicyNo" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                            </tr>
                        
                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lblQuote_Num" Enabled="true" Text="Proposal No:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtQuote_Num" Enabled="false" Width="250px" runat="server"></asp:TextBox></td>
                                                <td align="left" valign="top"><asp:Label ID="lblPlan_Num" Enabled="true" Text="Plan/Cover Code:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:TextBox ID="txtPlan_Num" Visible="true" Enabled="false" MaxLength="10" Width="80" runat="server"></asp:TextBox>
                                                    &nbsp;<asp:TextBox ID="txtCover_Num" Visible="true" Enabled="false" MaxLength="10" Width="80px" runat="server"></asp:TextBox></td>
                                                
                            </tr>

<tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblProduct" Text="Product Category:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:TextBox ID="txtProductClass" runat="server" Width="36px"></asp:TextBox>
                                            <asp:DropDownList ID="cboProductClass" runat="server" CssClass="selProduct" AppendDataBoundItems="True" Enabled="false"
                                                Width="150px">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                            
                                    </td>
                                <td align="left" valign="top"><asp:Label ID="lblPrem_Exchange_Rate" Enabled="false" Text="Product Code:" runat="server"></asp:Label></td>
                                <td align="left" valign="top">                                     <asp:TextBox ID="txtProduct" runat="server" Enabled="false" Width="36px"></asp:TextBox>
                                            <asp:DropDownList ID="cboProduct" runat="server" CssClass="selProduct" AppendDataBoundItems="True" Enabled="false"
                                                Width="150px">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                </td>
                            </tr>
                                    


                                    <tr>
                                        <td align="left" colspan="4" valign="top" class="myMenu_Title">Risk Coverage/Payment Information</td>
                                    </tr>

<tr>
                                                <td align="left" valign="top"><asp:Label ID="lblDOB" Text="Batch Date:" 
                                                        runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:TextBox ID="txtBatchNo" runat="server" Width="150px"></asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblDOB_Format" Enabled="False" Text="yyyy/mm" 
                                                        runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblBustype" Enabled="true" Text="Business Type:" runat="server"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="3"> <asp:DropDownList ID="cmbTransType" runat="server" Width="150px">
                                                <asp:ListItem Value="NB" Text="New Business" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="RN" Text="Renewal Business" ></asp:ListItem>
                                            </asp:DropDownList>
                                                </td>
                                    </tr>
                            <tr>
                                                <td align="left" valign="top"><asp:Label ID="lbleffDate" Enabled="true" Text="Effective Date:" runat="server"></asp:Label></td>
                                                <td align="left" colspan="1" valign="top">                                                    
                                                    <asp:TextBox ID="txtEffectiveDate" runat="server" MaxLength="10"></asp:TextBox><asp:Label ID="lblEff_Date_Format" Visible="true" Enabled="false" Text="dd/mm/yyyy" runat="server"></asp:Label>
                                                </td>
                                                <td nowrap align="left" valign="top"><asp:Label ID="lblRcpt_Type" Enabled="true" Text="Receipt Type:" runat="server"></asp:Label></td>
                                                <td align="left" valign="top" colspan="1">                                                    
                                                    <asp:TextBox ID="txtReceiptCode" runat="server" Width="18px"></asp:TextBox>
                                            <asp:DropDownList ID="cmbReceiptType" runat="server" Width="126px" >
                                                <asp:ListItem Value="0" Text="Receipt Type"></asp:ListItem>
                                                <asp:ListItem Value="D" Text="D-Premium Deposit"></asp:ListItem>
                                                <asp:ListItem Value="P" Text="P-Regular Premium"></asp:ListItem>
                                                <asp:ListItem Value="U" Text="U-Lumpsum"></asp:ListItem>
                                                <asp:ListItem Value="F" Text="F-FAC"></asp:ListItem>
                                                <asp:ListItem Value="C" Text="C-Co-Insurance"></asp:ListItem>
                                                <asp:ListItem Value="L" Text="L-Lease"></asp:ListItem>
                                                <asp:ListItem Value="X" Text="X-Cheque Exchange"></asp:ListItem>
                                                <asp:ListItem Value="V" Text="V-Dividend"></asp:ListItem>
                                                <asp:ListItem Value="R" Text="R-Refund"></asp:ListItem>
                                                <asp:ListItem Value="Y" Text="Y-Claim Recovery"></asp:ListItem>
                                                <asp:ListItem Value="I" Text="I-Interest Received"></asp:ListItem>
                                                <asp:ListItem Value="T" Text="T-Investment Income"></asp:ListItem>
                                                <asp:ListItem Value="S" Text="S-Share Sale"></asp:ListItem>
                                                <asp:ListItem Value="G" Text="G-Salvage"></asp:ListItem>
                                                <asp:ListItem Value="O" Text="O-Others"></asp:ListItem>
                                            </asp:DropDownList>
                                                 </td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="lblTellerNo" Text="Teller Number:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:TextBox ID="txtTellerNo" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                <td align="left" valign="top"><asp:Label ID="lblCurr_Type" Enabled="true" Text="Currency Type:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"> <asp:TextBox ID="txtCurrencyCode" runat="server" Width="36px"></asp:TextBox>
                                            <asp:DropDownList ID="cmbCurrencyType" runat="server" Width="110px" Height="21px">
                                                <asp:ListItem Value="0" Text="Currency Type"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csValidateCurrencyType" runat="server" ErrorMessage="Please Select the Currency Type">*</asp:CustomValidator>

                                </td>
                            </tr>

                            
                            <tr>
                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Sum Assured/Premium Information</td>
                            </tr>
                            
                            <tr>
                                <td nowrap align="left" valign="top" class="style1"><asp:Label ID="lblReceiptAmt" Text="Receipt Amt LC:" ToolTip="Receipt Amount..." runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1" class="style1">
 <asp:TextBox ID="txtReceiptAmtLC" runat="server" Width="150px" Text="0.00" AutoPostBack="True"></asp:TextBox><%--<asp:RegularExpressionValidator
                                                ID="rvDecimal" runat="server" ErrorMessage="Please Enter a Valid Receipt Amount"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" ControlToValidate="txtReceiptAmtLC">*</asp:RegularExpressionValidator>--%>
                                </td>
                                <td nowrap align="left" valign="top" class="style1">Receipt Amount FC</td>
                                <td align="left" valign="top" colspan="1" class="style1">
                                    <asp:TextBox ID="txtReceiptAmtFC" runat="server" Width="150px" Text="0.00" Enabled="False"></asp:TextBox><%--<asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter a Valid Receipt Amount"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" ControlToValidate="txtReceiptAmtFC">*</asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblPayeeName" Text="Payee Name:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                    <asp:TextBox ID="txtPayeeName" runat="server" Width="270px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblBranch" Enabled="false" Text="Branch:" runat="server"></asp:Label>
                                 </td> 
                                    <td  align="left" valign="top"><asp:TextBox ID="txtBranchCode" runat="server" Width="40px"></asp:TextBox>
                                            <asp:DropDownList ID="cmbBranchCode" runat="server" Width="150px">
                                                <asp:ListItem Value="0" Text="Branch Code"></asp:ListItem>
                                            </asp:DropDownList>
                                </td>
                            </tr>

                           
                            <tr>
                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblTransDesc" Enabled="false" Text="Trans. Desc. 1:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                   <asp:TextBox ID="txtTransDesc1" runat="server" Width="270px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" >
                                    <asp:Label ID="lblDepositAmount" Enabled="False" Text="Deposit Amount:" CssClass="popupOffset"
                                        runat="server"></asp:Label>
                                    </td>
                                <td align="left" valign="top" ><asp:TextBox ID="txtDepositAmount" runat="server" CssClass="popupOffset"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" valign="top" colspan="1"><asp:Label ID="lblTransDesc2" Enabled="false" Text="Trans. Desc. 2:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                   <asp:TextBox ID="txtTransDesc2" runat="server" Width="270px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" >
                                    <asp:Label ID="Label2" Enabled="False" Text="Comm. Applicable?:" 
                                        runat="server"></asp:Label>
                                    </td>
                                <td align="left" valign="top" >                                            <asp:DropDownList ID="cmbCommissions" runat="server" Width="100px">
                                                <asp:ListItem Value="0" Text="Commissions Applicable?"></asp:ListItem>
                                                <asp:ListItem Value="Y" Text="YES" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:CustomValidator ID="csValidateCommissions" runat="server" OnServerValidate="csValidateCommissions_ServerValidate"
                                                ControlToValidate="cmbCommissions" ErrorMessage="Please Select Commission Applicable">*</asp:CustomValidator>--%>
                                       
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td align="left" valign="top" colspan="4"><hr /></td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblpolRegularContrib" ToolTip="LC=Local Currency" Text="Policy Regular Contrib.:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtPolRegularContrib" runat="server" Width="150px" Text="0.00"></asp:TextBox>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblMOP" ToolTip="Mode of Payment" Text="Mode Of Payment:" runat="server"></asp:Label></td>
                                <td align="left" valign="top">             <asp:TextBox ID="txtMOP" runat="server" Width="20px"> </asp:TextBox><asp:TextBox
                                                ID="txtMOPDesc" runat="server" Width="250px" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="left" valign="top"><asp:Label ID="lblMainAcctDebit" ToolTip="Account to Debit" Text="Main Account(Debit):" runat="server"></asp:Label></td>
                                <td align="left" valign="top"> <asp:TextBox ID="txtMainAcctDebit" runat="server" 
                                        Width="150px" Enabled="False">1020020730</asp:TextBox>
                                            &nbsp;</td>
                                </td>
                                <td align="left" valign="top"><asp:Label ID="lblSubAcctDebit" ToolTip="Sub Account Debit" Text="Sub Account(Debit):" runat="server"></asp:Label></td>
                                <td align="left" valign="top">                                      
                                    <asp:TextBox ID="txtSubAcctDebit" runat="server" Width="150px" Enabled="False">000000</asp:TextBox>
                                            &nbsp;</td>
                            </tr>

                            <tr>
                                <td align="left" valign="top" colspan="4"><hr /></td>
                            </tr>
    
                            <tr>
                                <td align="left" colspan="4" valign="top" class="myMenu_Title">Other Technical Information</td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="Label1" Text="Assured Code" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                   <asp:TextBox ID="txtInsuredCode" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                <td align="left" valign="top"><asp:Label ID="Label5" Enabled="false" Text="Assured Name:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtAssuredName" runat="server" Width="270px" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="Label3" Enabled="false" Text="Assured Address:" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                   
                                    <asp:TextBox ID="txtAssuredAddress" runat="server" Width="290px" BorderStyle="None"></asp:TextBox>
                                   
                                    </td>
                                <td align="left" valign="top"></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtSerialNo" runat="server" Width="150px" Enabled="false" CssClass="popupOffset"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="Label6" Text="Agent Code" runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1">
                                  <asp:TextBox ID="txtAgentCode" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                <td align="left" valign="top"><asp:Label ID="Label7" Enabled="false" Text="Agent Name:" runat="server"></asp:Label></td>
                                <td align="left" valign="top"><asp:TextBox ID="txtAgentName" runat="server" Width="270px" Enabled="false" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td nowrap align="left" valign="top"><asp:Label ID="Label4" Text="Main A/C Debit Desc." runat="server"></asp:Label></td>
                                <td align="left" valign="top" colspan="1"> <asp:HiddenField ID="HidShowHide" runat="server" />
                                  <asp:TextBox ID="txtMainAcctDebitDesc" runat="server" Width="300px" Enabled="true"
                                                BorderStyle="None"></asp:TextBox><asp:HiddenField ID="txtPolRegularContribH" runat="server" />
                                    </td>
                                <td align="left" valign="top"><asp:Label ID="Label8" Enabled="false" Text="Sub A/C Desc:" runat="server" CssClass=popupOffset></asp:Label></td>
                                <td align="left" valign="top"><asp:Button ID="butSave" runat="server" Text="Save" OnClick="cmdSave_ASP_Click" />
                                        <asp:Button ID="butNew" runat="server" Text="New Hdr" />
                                        <asp:Button ID="butPrint" runat="server" Text="Print" Visible="True" />
                                        <asp:Button ID="butClose" runat="server" Text="Close" Visible="True" /><asp:TextBox ID="txtSubAcctDebitDesc" runat="server" Width="300px" BorderStyle="None" CssClass=popupOffset></asp:TextBox>

                                             
                                           
                                              <asp:HiddenField ID="txtMainAcctDebitDescH" runat="server" />
                                              <asp:HiddenField ID="txtMOPH" runat="server" />
                                              <asp:HiddenField ID="txtInsuredCodeH" runat="server" />
                                              <asp:HiddenField ID="txtAgentCodeH" runat="server" />
                                              <asp:TextBox ID="txtPolicyEffDate" runat="server" Width="150px" CssClass="popupOffset">
                                            </asp:TextBox>

                                </td>
                            </tr>
                            
                        <tr>
                        <td colspan="4" class="myMenu_Title" align="center">
                                            Transaction Detail Entry
                                        </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="9">
                              <div id="DetailPart">
                                <table class="tbl_menu_new">
                                    
                                    <tr>
                                        
                                        <td>
                                            <asp:Label ID="lblSNO" runat="server" Text="S/NO" CssClass="popupOffset"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRcpt_Mode" runat="server" Text="Receipt Mode"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRef1" runat="server" Text="Ref. No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Trans Date"></asp:Label>
                                        </td>
                                        <td>
                                            
                                            <asp:Label ID="lblTranAmt" runat="server" Text="Trans Amt"></asp:Label>
                                        </td>
                                       
                                        <td>
                                            <asp:Label ID="lblTranStatus" runat="server" Text="Trans Status"></asp:Label>
                                        </td>

                                        <td>
                                            
                                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        
                                        <td>
                                            <asp:TextBox ID="txtSubSerialNo" runat="server" BorderStyle="None" Width="30px" CssClass="popupOffset"></asp:TextBox>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtMode" runat="server" Width="26px"></asp:TextBox>
                                            <asp:DropDownList ID="cmbMode" runat="server" Width="120px">
                                                <asp:ListItem Value="0" Text="Receipt Mode"></asp:ListItem>
                                                <asp:ListItem Value="C" Text="C-Cash"></asp:ListItem>
                                                <asp:ListItem Value="Q" Text="Q-Cheque"></asp:ListItem>
                                                <asp:ListItem Value="D" Text="D-Direct Payment"></asp:ListItem>
                                                <asp:ListItem Value="T" Text="T-Teller"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDetailDocNo" runat="server" Width="90px"></asp:TextBox>
                                        </td>
                                        
                                        <td style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtDetailDate" runat="server" Width="100px" AutoPostBack="False"></asp:TextBox>
                                        </td>
                                        
                                        <td style="white-space: nowrap">
                                            &nbsp;<asp:TextBox ID="txtTransAmt" runat="server" Width="100px" AutoPostBack="False">0.00</asp:TextBox>
                                        </td>
                                        
                                        <td>
                                            <asp:TextBox ID="txtTransTypeCode" runat="server" Width="30" CssClass="popupOffset"></asp:TextBox>
                                            <asp:DropDownList ID="cmbTransDetailStatus" runat="server" Width="150px" AutoPostBack="True">
                                              <asp:ListItem Value="OK" Text="TRANS OK">                                            
                                            </asp:ListItem>
                                             <asp:ListItem Value="RJ" Text="REJECTED">                                            
                                            </asp:ListItem>                                           
                                            </asp:DropDownList>
                                           
                                        </td>
                                        <td>
                                            <%--<asp:RegularExpressionValidator ID="vdamt" runat="server" ErrorMessage="Please Enter a Valid Amount"
                                                ValidationExpression="^(-)?\d+(\.\d\d)?$" ControlToValidate="txtTransAmt">*</asp:RegularExpressionValidator>
                                       --%>
                                                                                
                                            <asp:DropDownList ID="cmbRemarks" runat="server" Width="165px" 
                                                AutoPostBack="True" Height="16px">
                                            <asp:ListItem Value=OK Text="TRANS OK">                                            
                                            </asp:ListItem>
                                             <asp:ListItem Value="UNVERIFIED" Text="AMOUNT NOT VERIFIABLE">                                            
                                            </asp:ListItem>  
                                                        <asp:ListItem Value="BOUNCED" Text="CHEQUE BOUNCED">                                            
                                            </asp:ListItem>                              
                                            </asp:DropDownList>
                                        </td>
                                        
                                    </tr>
                                   
                                    <tr><td></td><td></td><td></td><td></td><td></td><td></td><td><asp:Button ID="butSaveDetail" runat="server" Text="Save" OnClick="cmdSave_ASP_Click" /><asp:Button ID="butNewDetail" runat="server" Text="New" /><asp:Button ID="butDeleteDetail" runat="server" Text="Del" />
                                
                                        </td></tr>
                                </table>
                                
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMainAcctDesc" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="txtSubAcctDesc" runat="server" Width="170px" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="txtRefDesc1" runat="server" Enabled="false" Width="170px"></asp:TextBox>
                                            <asp:TextBox ID="txtRefDesc2" runat="server" Width="170px" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="txtRefDesc3" runat="server" Width="170px" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    
                                </table>
                                
                            </div>
                            <div id="displayDetails">
                                <div class="gridscreen">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                        <asp:Label ID="lblDesc2" runat="server" Text="Receipts Details Entry"></asp:Label>
                                                    </h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid">
                             <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" FooterStyle-Font-Size="11px"
                                                        FooterStyle-Font-Bold="true" FooterStyle-ForeColor="RosyBrown" FooterStyle-Font-Underline="true"
                                                        PageSize="3" DataSourceID="ods1" AllowSorting="True" CssClass="datatable" CellPadding="0"
                                                        BorderWidth="0px" AlternatingRowStyle-BackColor="#CDE4F1" GridLines="None" HeaderStyle-BackColor="#099cc"
                                                        ShowFooter="True">
                                                        <FooterStyle Font-Bold="True" Font-Size="11px" Font-Underline="True" ForeColor="RosyBrown">
                                                        </FooterStyle>
                                                        <PagerStyle CssClass="pager-row" />
                                                        <RowStyle CssClass="row" />
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="7" FirstPageText="«" LastPageText="»" />
                                                        <Columns>
                                                            <asp:HyperLinkField DataTextField="rtId" DataNavigateUrlFields="rtId,CompanyCode,BatchNo,SerialNo,SubSerialNo,TransType"
                                                                DataNavigateUrlFormatString="~/I_LIFE/PRG_LI_POL_RECEIPT_MULTIPLE.aspx?idd={0},{1},{2},{3},{4},{5}&prgKey={5}"
                                                                HeaderText="Id" Visible="false" HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                                                                <HeaderStyle CssClass="first"></HeaderStyle>
                                                                <ItemStyle CssClass="first"></ItemStyle>
                                                            </asp:HyperLinkField>
                                                            <asp:HyperLinkField DataTextField="CompanyCode" DataNavigateUrlFields="rtId,CompanyCode,BatchNo,SerialNo,SubSerialNo,TransType"
                                                                DataNavigateUrlFormatString="~/I_LIFE/PRG_LI_POL_RECEIPT_MULTIPLE.aspx?idd={0},{1},{2},{3},{4},{5}&prgKey={5}"
                                                                HeaderText="Coy" Visible="false" HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                                                                <HeaderStyle CssClass="first"></HeaderStyle>
                                                                <ItemStyle CssClass="first"></ItemStyle>
                                                            </asp:HyperLinkField>
                                                            <asp:HyperLinkField DataTextField="BatchNo" DataNavigateUrlFields="rtId,CompanyCode,BatchNo,SerialNo,SubSerialNo,TransType"
                                                                DataNavigateUrlFormatString="~/I_LIFE/PRG_LI_POL_RECEIPT_MULTIPLE.aspx?idd={0},{1},{2},{3},{4},{5}&prgKey={5}"
                                                                HeaderText="Bat.#" HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                                                                <HeaderStyle CssClass="first"></HeaderStyle>
                                                                <ItemStyle CssClass="first"></ItemStyle>
                                                            </asp:HyperLinkField>
                                                            <asp:HyperLinkField DataTextField="SerialNo" DataNavigateUrlFields="rtId,CompanyCode,BatchNo,SerialNo,SubSerialNo,TransType"
                                                                DataNavigateUrlFormatString="~/I_LIFE/PRG_LI_POL_RECEIPT_MULTIPLE.aspx?idd={0},{1},{2},{3},{4},{5}&prgKey={5}"
                                                                HeaderText="SN" HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                                                                <HeaderStyle CssClass="first"></HeaderStyle>
                                                                <ItemStyle CssClass="first"></ItemStyle>
                                                            </asp:HyperLinkField>
                                                            <asp:HyperLinkField DataTextField="SubSerialNo" DataNavigateUrlFields="rtId,CompanyCode,BatchNo,SerialNo,SubSerialNo,TransType"
                                                                DataNavigateUrlFormatString="~/I_LIFE/PRG_LI_POL_RECEIPT_MULTIPLE.aspx?idd={0},{1},{2},{3},{4},{5}&prgKey={5}"
                                                                HeaderText="SSN" HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                                                                <HeaderStyle CssClass="first"></HeaderStyle>
                                                                <ItemStyle CssClass="first"></ItemStyle>
                                                            </asp:HyperLinkField>
                                                            <asp:BoundField DataField="DocNo" HeaderText="Doc#" />
                                                            <asp:BoundField DataField="TransId" HeaderText="Trans #" />
                                                            <asp:BoundField DataField="TransDate" HeaderText="Trans Dt" DataFormatString="{0:d}" />
                                                            <asp:BoundField DataField="Remarks" HeaderText="Detail Status" />
                                                            <asp:BoundField DataField="DetailDocNo" HeaderText="Det.Doc #" />
                                                            <asp:TemplateField HeaderText="Trans. Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTransAmt" runat="server" DataFormatString="{0:N2}" Text='<%#Eval("DetailTransAmountLC") %>' />
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lbltxtTotal" runat="server" Text="0.00" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                                                        <AlternatingRowStyle BackColor="#CDE4F1" />
                                                    </asp:GridView>
                              
                              
                                                   <asp:ObjectDataSource ID=ods1 runat=server SelectMethod="GetById" 
                                                        TypeName="CustodianLife.Data.ReceiptsRepository" >
                                                       <SelectParameters>
                                                           <asp:Parameter DefaultValue="Receipt" Name="_key" Type="String" />
                                                           <asp:ControlParameter ControlID="txtReceiptNo" Name="_value" 
                                                               PropertyName="Text" Type="String" DefaultValue="Z" />
                                                       </SelectParameters>
                                                   
                                                   </asp:ObjectDataSource>
                           </div>
                                            </div>
                                        </div>
                                        <div class="bottom-outer">
                                            <div class="bottom-inner">
                                                <div class="bottom">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                            <div class="div_grid">
                                    <asp:GridView ID="GridView1" CellPadding="2" runat="server" CssClass="grd_ctrl" DataKeyNames="TBFN_ACCT_DOC_NO"
                                        HorizontalAlign="Left" AutoGenerateColumns="False" AllowPaging="false" AllowSorting="true"
                                        PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                        PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next" PagerSettings-PreviousPageText="Previous"
                                        PagerSettings-LastPageText="Last" EmptyDataText="No data available..." GridLines="Both"
                                        ShowHeader="True" ShowFooter="True">
                                        <PagerStyle CssClass="grd_page_style" />
                                        <HeaderStyle CssClass="grd_header_style" />
                                        <RowStyle CssClass="grd_row_style" />
                                        <SelectedRowStyle CssClass="grd_selrow_style" />
                                        <EditRowStyle CssClass="grd_editrow_style" />
                                        <AlternatingRowStyle CssClass="grd_altrow_style" />
                                        <FooterStyle CssClass="grd_footer_style" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom"
                                            PreviousPageText="Previous"></PagerSettings>
                                        <Columns>
                                            <%--<asp:TemplateField>
        			                                        <ItemTemplate>
        						                                <asp:CheckBox id="chkSel" runat="server" Width="20px"></asp:CheckBox>
                                                            </ItemTemplate>                                                            
                                                        </asp:TemplateField>
                                --%>
                                            <%-- <asp:CommandField ShowSelectButton="True" ItemStyle-Width="50px" />--%>
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_ENTRY_DATE" HeaderText="Entry Date"
                                                ItemStyle-Width="70px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <%--<asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_DOC_NO" HeaderText="Receipt No"
                                                ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />--%>
                                            <asp:HyperLinkField DataTextField="TBFN_ACCT_DOC_NO" DataNavigateUrlFields="TBFN_ACCT_DOC_NO"
                                                DataNavigateUrlFormatString="PRG_LI_INDV_RECEIPT.aspx?idd={0}" ItemStyle-Width="70px"
                                                HeaderText="Receipt No" ItemStyle-CssClass="DocNum">
                                                <ItemStyle CssClass="DocNum"></ItemStyle>
                                            </asp:HyperLinkField>
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_PAYER_PAYEE_NAME" HeaderText="Payee Name"
                                                ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_AMT_LC" HeaderText="Amount"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true"
                                                DataFormatString="{0:N2}" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_TRANS_MODE" HeaderText="Receipt Mode"
                                                ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_RECP_TYP" HeaderText="Receipt Type"
                                                ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_CHQ_TELLER_NO" HeaderText="Ref No"
                                                ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_CHQ_INWARD_DATE" HeaderText="Cheque Date"
                                                ItemStyle-Width="60px" ConvertEmptyStringToNull="true" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_TRANS_DESC1" HeaderText="Trans Desc"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_TRANS_DESC2" HeaderText="Cover Period"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                            <asp:BoundField ReadOnly="true" DataField="TBFN_ACCT_DR_MAIN" HeaderText="Main Acct(Debit)"
                                                ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ConvertEmptyStringToNull="true" />
                                        </Columns>
                                    </asp:GridView>
                               </div>



                            </td>
                        </tr>
                        </table>
        
    </div>
    <div>
    
    </div>
                                                                                
<div id="div_footer" align="center">    

    <table id="tbl_footer" align="center">
        <tr>
            <td valign="top">
                <table align="center" border="0" class="footer" style=" background-color: Black;">
                    <tr>
                        <td colspan="4">                                                        
                            <uc2:UC_FOOT ID="UC_FOOT1" runat="server" />                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>    

    </form>
</body>
</html>
