        ��  ��                  p�      �����e                 
    <link rel="stylesheet" type="text/css" href="Styles/jquery.tooltip.css" />
    <script src="Scripts/gridview-addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">
        var addEditTitle = "Register Record";
        var urlAndMethod = "GridViewAddEdit_RegisterRecord.aspx/GetRegisterRecord";

        $(function () {
            InitializeDatePicker("input[id$=TxtDate]");            
            InitializeDeleteConfirmation();
            InitializeAddEditRecord();
            InitializeToolTip();
        });
 
        function setURL(itemId) {
            alert(itemId);
            var txtURL = "AddEdit_RegisterRecords.aspx";
            alert(txtURL);
            window.Open(txtURL,'_self',false) ;
        };
        function clearFields() {
	        $("#").val(ConvertNullToString(msg.d.OverringCount));
        };


</script>
<style type="text/css">
    .style2
    {
        height: 29px;
    }
    .style3
    {
        width: 349px;
    }
    .style4
    {
        height: 29px;
        width: 349px;
    }
    .style5
    {
        width: 47px;
    }
    .style6
    {
        height: 29px;
        width: 47px;
    }
    .style7
    {
        width: 180px;text-align: right;
    }
    .style7bold
    {
        width: 180px;text-align: right;font-weight: bold;
    }
    .style8
    {
        height: 29px;
        width: 116px;
    }
</style>

    </div> 
    <div id="deleteConfirmationDialog"></div>
    <div id="deleteErrorDialog" title="An error occured during item deletion!"></div>

<!--  <a id="showAddNewRecord1" href="#"><img src="Images/Add.gif" alt="Add New RegisterRecord" style="border: none;" /></a>&nbsp;<a id="showAddNewRecord2" href="#">Add New RegisterRecord</a>
    <br /><br />
    <div id="divAddEditRecord" class="ui-widget-content" style="display: none; width: 600px; padding: 0px 10px 20px 10px;">
        <h3 id="h3AddEditRecord" class="ui-widget-header">Add New RegisterRecord</h3>
        <table>
	        <tr id="trPrimaryKey">
		        <td>Register Record ID:</td>
                <td></td>
		        <td class="style5"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7">Cashier Register No.:</td>
                <td></td>
		        <td class="style7"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Total </td>
                <td style="font-weight: bold">Currency</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="TotalCurrency"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
            <tr>
		        <td class="style7">Cash in Drawer</td>
                <td>(from ZClerk)</td>
		        <td class="style7"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Total</td>
                <td style="font-weight: bold">Charges</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="TotalCharges"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>


   	        <tr>
		        <td class="style7">Charge in Drawer</td>
                <td>(from ZClerk)</td>
		        <td class="style7"></td>
                <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Employee</td>
                <td style="font-weight: bold">Register Total</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="EmployeeRegisterTotal"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7">Register Total</td>
                <td>(from ZClerk)</td>
		        <td class="style7"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Errors in Cash</td>
                <td style="font-weight: bold">-Over/Short</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="CashError"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Errors in Charges</td>
                <td style="font-weight: bold">-Over/Short</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="ChargeError"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
            <tr>
		        <td class="style7bold" style="font-size: 10pt">Total</td>
                <td style="font-weight: bold">Errors +/-</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="TotalError"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7">Overring:</td>
                <td></td>
		        <td class="style7">
    <script src="Scripts/addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            InitializeDatePicker("input[id$=TxtDate]");
            refresh_totals();
        });
        // Treat "Enter" key as tab, except if focus is on "Update Record" or "Add Record"
        $(document).ready(function () {
            $("input").not($(":button")).keypress(function (evt) {
                if (evt.keyCode == 13) {
                    iname = $(this).val();
                    if (iname !== 'Add Record' && iname !== 'Update Record') {
                        var fields = $(this).parents('form:eq(0),body').find('button,input,textarea,select');
                        var index = fields.index(this);
                        if (index > -1 && (index + 1) < fields.length) {
                            fields.eq(index + 1).focus();
                        }
                        return false;
                    }
                }
            });

            // use this section for key up  on Charges
            $('#ContentPlaceHolder1_TxtAtmCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtAtmTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtVisaCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtVisaTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMastercardCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMastercardTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtDiscoverCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtDiscoverTotal').keyup(function () { refresh_totals() });
        });

        function refresh_totals() {
            var sumCCCount = 0;
            var sumCCTotal = 0;
            var ctAtm = document.getElementById("ContentPlaceHolder1_TxtAtmCount").value;
            var amtAtm = document.getElementById("ContentPlaceHolder1_TxtAtmTotal").value;
            var ctVisa = document.getElementById("ContentPlaceHolder1_TxtVisaCount").value;
            var amtVisa = document.getElementById("ContentPlaceHolder1_TxtVisaTotal").value;
            var ctMastercard = document.getElementById("ContentPlaceHolder1_TxtMastercardCount").value;
            var amtMastercard = document.getElementById("ContentPlaceHolder1_TxtMastercardTotal").value;
            var ctDiscover = document.getElementById("ContentPlaceHolder1_TxtDiscoverCount").value;
            var amtDiscover = document.getElementById("ContentPlaceHolder1_TxtDiscoverTotal").value;

            // Calculate CreditCard Count
            if (!isNaN(ctAtm) && ctAtm.length != 0) {
                sumCCCount += parseFloat(ctAtm);
            }
            if (!isNaN(ctVisa) && ctVisa.length != 0) {
                sumCCCount += parseFloat(ctVisa);
            }
            if (!isNaN(ctMastercard) && ctMastercard.length != 0) {
                sumCCCount += parseFloat(ctMastercard);
            }
            if (!isNaN(ctDiscover) && ctDiscover.length != 0) {
                sumCCCount += parseFloat(ctDiscover);
            }

            // Calculate CreditCard Total
            if (!isNaN(amtAtm) && amtAtm.length != 0) {
                sumCCTotal += parseFloat(amtAtm);
            }
            if (!isNaN(amtVisa) && amtVisa.length != 0) {
                sumCCTotal += parseFloat(amtVisa);
            }
            if (!isNaN(amtMastercard) && amtMastercard.length != 0) {
                sumCCTotal += parseFloat(amtMastercard);
            }
            if (!isNaN(amtDiscover) && amtDiscover.length != 0) {
                sumCCTotal += parseFloat(amtDiscover);
            }

            document.getElementById("CCCount").innerHTML = sumCCCount.toFixed(0);
            document.getElementById("CCTotal").innerHTML = sumCCTotal.toFixed(2);
        }


    </script>
</td>
	        </tr>
            <tr>
                <td></td>
                <td></td>
                <td><b>Count</b></td>
                <td></td>
                <td><b>Amount</b></td>
            </tr>
	        <tr>
		        <td>Atm:</td>
                <td></td>
		        <td></td>
	        </tr>
	        <tr>
		        <td></td>
                <td></td>
                <td><div style="color: #900; font-weight: bold; text-align: right;width: 150px" id="CCCount" >0</div></td>
                <td></td>
                <td><div style="color: #900; font-weight: bold; text-align: right" id="CCTotal" >0.00</div></td>
	        </tr>
	        <tr>
		        <td colspan="2"></td>
		        <td colspan="2">
                    
    <script src="Scripts/addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            InitializeDatePicker("input[id$=TxtDate]");
            refresh_totals();
        });

        // Treat "Enter" key as tab, except if focus is on "Update Record" or "Add Record"
        $(document).ready(function () {
            $("input").not($(":button")).keypress(function (evt) {
                if (evt.keyCode == 13) {
                    iname = $(this).val();
                    if (iname !== 'Add Record' && iname !== 'Update Record') {
                        var fields = $(this).parents('form:eq(0),body').find('button,input,textarea,select');
                        var index = fields.index(this);
                        if (index > -1 && (index + 1) < fields.length) {
                            fields.eq(index + 1).focus();
                        }
                        return false;
                    }
                }
            });
            // use this section for key up  on Charges
            $('#ContentPlaceHolder1_TxtVisa').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMastercard').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtDiscover').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtAtm').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtGiftCertificate').keyup(function () { refresh_totals() });

            // use this section for key up on Currency 
            $('#ContentPlaceHolder1_TxtCurrency').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtCoins').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMiscCash').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtZTapeCash').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtZTapeCharge').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtZTape').keyup(function () { refresh_totals() });


        });

        function refresh_totals() {
            var sumCharges = 0;
            var val1 = document.getElementById("ContentPlaceHolder1_TxtVisa").value;
            var val2 = document.getElementById("ContentPlaceHolder1_TxtMastercard").value;
            var val3 = document.getElementById("ContentPlaceHolder1_TxtDiscover").value;
            var val4 = document.getElementById("ContentPlaceHolder1_TxtAtm").value;
            var val5 = document.getElementById("ContentPlaceHolder1_TxtGiftCertificate").value;

            var sumCurrency = 0;
            var val11 = document.getElementById("ContentPlaceHolder1_TxtCurrency").value;
            var val12 = document.getElementById("ContentPlaceHolder1_TxtCoins").value;
            var val13 = document.getElementById("ContentPlaceHolder1_TxtMiscCash").value;

            var valRegTotal = 0;
            var val20 = document.getElementById("ContentPlaceHolder1_TxtZTape").value;

            var valZTapeCharge = 0;
            var val21 = document.getElementById("ContentPlaceHolder1_TxtZTapeCharge").value;

            var valZTapeCash = 0;
            var val22 = document.getElementById("ContentPlaceHolder1_TxtZTapeCash").value;

            // Calculate total of charges and debits
            if (!isNaN(val1) && val1.length != 0) {
                sumCharges += parseFloat(val1);
            }
            if (!isNaN(val2) && val2.length != 0) {
                sumCharges += parseFloat(val2);
            }
            if (!isNaN(val3) && val3.length != 0) {
                sumCharges += parseFloat(val3);
            }
            if (!isNaN(val4) && val4.length != 0) {
                sumCharges += parseFloat(val4);
            }
            if (!isNaN(val5) && val5.length != 0) {
            	sumCharges += parseFloat(val5);
            }

            document.getElementById("TotalCharges").innerHTML = sumCharges.toFixed(2);

            // Calculate total of currency
            // val5 (Gift Certificate) should be included in currency
            if (!isNaN(val11) && val11.length != 0) {
                sumCurrency += parseFloat(val11);
            }
            if (!isNaN(val12) && val12.length != 0) {
                sumCurrency += parseFloat(val12);
            }
            if (!isNaN(val13) && val13.length != 0) {
                sumCurrency += parseFloat(val13);
            }
            document.getElementById("TotalCurrency").innerHTML = sumCurrency.toFixed(2);

            var empRegTotal = parseFloat(sumCharges) + parseFloat(sumCurrency);
            document.getElementById("EmployeeRegisterTotal").innerHTML = empRegTotal.toFixed(2);

            // Validate ZTape Cash Total
            if (!isNaN(val22) && val22.length != 0) {
                valZTapeCash += parseFloat(val22);
            }
            var cashErr = parseFloat(sumCurrency) - parseFloat(valZTapeCash);
            document.getElementById("CashError").innerHTML = cashErr.toFixed(2);

            // Validate Charge in Drawer Total
            if (!isNaN(val21) && val21.length != 0) {
                valZTapeCharge += parseFloat(val21);
            }
            var chargeErr = parseFloat(sumCharges) - parseFloat(valZTapeCharge);
            document.getElementById("ChargeError").innerHTML = chargeErr.toFixed(2);

            // Validate Register Total
            if (!isNaN(val20) && val20.length != 0) {
                valRegTotal += parseFloat(val20);
            }

            var totalErr = parseFloat(sumCharges) + parseFloat(sumCurrency) - parseFloat(valZTapeCash) - parseFloat(valZTapeCharge);
            document.getElementById("TotalError").innerHTML = totalErr.toFixed(2);

        }
    </script>
<style type="text/css">
    .style2
    {
        height: 29px;
    }
    .style3
    {
        width: 349px;
    }
    .style4
    {
        height: 29px;
        width: 349px;
    }
    .style5
    {
        width: 47px;
    }
    .style6
    {
        height: 29px;
        width: 47px;
    }
    .style7
    {
        width: 180px;text-align: right;
    }
    .style7bold
    {
        width: 180px;text-align: right;font-weight: bold;
    }
    .style8
    {
        height: 29px;
        width: 116px;
    }
</style>


                </td>
		        <td class="style5"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7">Employee:</td>
                <td></td>
		        <td class="style7"></td>
		        <td class="style5"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7">Date:</td>
                <td>    </td>
		        <td style="padding-left: 12px"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Total </td>
                <td style="font-weight: bold">Currency</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="TotalCurrency"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
            <tr>
		        <td class="style7">Cash in Drawer</td>
                <td>(from ZClerk)</td>
		        <td class="style7"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Total</td>
                <td style="font-weight: bold">Charges</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="TotalCharges"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>


   	        <tr>
		        <td class="style7">Charge in Drawer</td>
                <td>(from ZClerk)</td>
		        <td class="style7"></td>
                <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Employee</td>
                <td style="font-weight: bold">Register Total</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="EmployeeRegisterTotal"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7">Register Total</td>
                <td>(from ZClerk)</td>
		        <td class="style7"></td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Errors in Cash</td>
                <td style="font-weight: bold">-Over/Short</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="CashError"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7bold">Errors in Charges</td>
                <td style="font-weight: bold">-Over/Short</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="ChargeError"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
            <tr>
		        <td class="style7bold" style="font-size: 10pt">Total</td>
                <td style="font-weight: bold">Errors +/-</td>
				<td><div style="color: #900; font-weight: bold; text-align: right;" id="TotalError"></div></td>
		        <td class="style5">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
		        <td class="style3">&nbsp;</td>
	        </tr>
	        <tr>
		        <td class="style7">Actual Cash</td>
                <td>-Over/Short</td>
		        <td class="style7">
                    &nbsp;<input id="inpCancel" type="button" value="Cancel" onclick="window.location = 'GridViewAddEdit_RegisterRecord.aspx'; return false;" />
                </td>
		        <td>
                    &nbsp;</td>
		        <td>
                    &nbsp;</td>
	        </tr>
        </table> 

    <link rel="stylesheet" type="text/css" href="Styles/jquery.tooltip.css" />
    <script src="Scripts/gridview-addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">
        var addEditTitle = "Employee";
        var urlAndMethod = "GridViewAddEdit_Employee.aspx/GetEmployee";

        $(function () {
            InitializeDeleteConfirmation();
            InitializeAddEditRecord();
            InitializeToolTip();
        });

        function clearFields() {
	        $("#</ br>
    </div> 
    <div id="deleteConfirmationDialog"></div>
    <div id="deleteErrorDialog" title="An error occured during item deletion!"></div>

    <a id="showAddNewRecord1" href="#"><img src="Images/Add.gif" alt="Add New Employee" style="border: none;" /></a>&nbsp;<a id="showAddNewRecord2" href="#">Add New Employee</a>
    <br /><br />
    <div id="divAddEditRecord" class="ui-widget-content" style="display: none; width: 600px; padding: 0px 10px 20px 10px;">
        <h3 id="h3AddEditRecord" class="ui-widget-header">Add New Employee</h3>
        <table>
	        <tr id="trPrimaryKey">
		        <td>Employee ID:</td>
                <td></ br>

    <br /><br />
    <div id="deleteConfirmationDialog"></div>
    <div id="deleteErrorDialog" title="An error occured during item deletion!"></div>
<!--
    <a href="AddEdit_Carts.aspx?operation=add"><img src="Images/Add.gif" alt="Add New Carts" style="border: none;" /></a>&nbsp;
    <link rel="stylesheet" type="text/css" href="Styles/jquery.tooltip.css" />
    <script src="Scripts/gridview-addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            InitializeDatePicker("input[id$=TxtDate]");
        });
    </script>
</h3>
    <table>
        <tr style="vertical-align: top">
            <td>
                <table>
                    <tr>
                        <td class="style120B">Location</td>
  	                    <td class="style90B">
                            
    <link rel="stylesheet" type="text/css" href="Styles/jquery.tooltip.css" />
    <script src="Scripts/gridview-addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">
        var addEditTitle = "Credit Card Record";
        var urlAndMethod = "GridViewAddEdit_CreditCardRecord.aspx/GetCreditCardRecord";

        $(function () {
            InitializeToolTip();
        });

        $(function () {
            InitializeDatePicker("input[id$=TxtDate]");
            InitializeDeleteConfirmation();
            InitializeAddEditRecord();
            InitializeToolTip();
            refresh_totals();
        });

        function clearFields() {
	        $("#
    </div> 

    <div id="deleteConfirmationDialog"></div>
    <div id="deleteErrorDialog" title="An error occured during item deletion!"></div>
<!--
    <a id="showAddNewRecord1" href="#"><img src="Images/Add.gif" alt="Add New CreditCardRecord" style="border: none;" /></a>&nbsp;<a id="showAddNewRecord2" href="#">Add New CreditCardRecord</a>
    <br /><br />
    <div id="divAddEditRecord" class="ui-widget-content" style="display: none; width: 600px; padding: 0px 10px 20px 10px;">
        <h3 id="h3AddEditRecord" class="ui-widget-header">Add New CreditCardRecord</h3>
        <table>
	        <tr id="trPrimaryKey">
		        <td>Credit Card Record ID:</td>
                <td></td>
	        </tr>
        </table>
        <table>
            <tr style="background-color: #05340d;font-size: 12pt ">
              <th >Card</th><th colspan="2">Count</th><th colspan="2">Total</th>
            </tr>
	        <tr>
		        <td>ATM:</td>
                <td  style="width: 100px">

    <script src="Scripts/addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            InitializeDatePicker("input[id$=TxtDate]");
            refresh_totals();
        });

        // Treat "Enter" key as tab, except if focus is on "Update Record" or "Add Record"
        $(document).ready(function () {
            $("input").not($(":button")).keypress(function (evt) {
                if (evt.keyCode == 13) {
                    iname = $(this).val();
                    if (iname !== 'Add Record' && iname !== 'Update Record') {
                        var fields = $(this).parents('form:eq(0),body').find('button,input,textarea,select');
                        var index = fields.index(this);
                        if (index > -1 && (index + 1) < fields.length) {
                            fields.eq(index + 1).focus();
                        }
                        return false;
                    }
                }
            });
            // use this section for key up  on Charges
            $('#ContentPlaceHolder1_TxtTax').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtDiscount1StTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtCashCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtCashTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtChargeCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtChargeTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtCorrectionCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtCorrectionTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtVoidCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtVoidTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtAllVoidCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtAllVoidTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtReturnsCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtReturnsTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtReturnsTax').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtFurnitureCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtFurnitureTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtJewelryCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtJewelryTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtElectricalCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtElectricalTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtWomensCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtWomensTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBinsCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBinsTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMiscCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMiscTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtShoesCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtShoesTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBoutiqueCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBoutiqueTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtChildsCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtChildsTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMensCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMensTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBooksCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBooksTotal').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtNewMerchCount').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtNewMerchTotal').keyup(function () { refresh_totals() });

            $('#ContentPlaceHolder1_TxtFurnitureTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtJewelryTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtElectricalTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtWomensTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBinsTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMiscTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtShoesTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBoutiqueTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtChildsTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtMensTotalDisc').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtBooksTotalDisc').keyup(function () { refresh_totals() });

        });
        function refresh_totals() {
            

            var sumCtGS = 0;
            var sumAmtGS = 0;
            var sumCtNS = 0;
            var sumAmtNS = 0;
            var sumCust = 0;
            var sumAmtDept = 0;
            var sumAmtDeptDisc = 0;
            var sumAmtReturns = 0;
            var sumAmtTax = 0;
            var amtDiscount1St = document.getElementById("ContentPlaceHolder1_TxtDiscount1StTotal").value;
            var ctCash = document.getElementById("ContentPlaceHolder1_TxtCashCount").value;
            var amtCash = document.getElementById("ContentPlaceHolder1_TxtCashTotal").value;
            var ctCharge = document.getElementById("ContentPlaceHolder1_TxtChargeCount").value;
            var amtCharge = document.getElementById("ContentPlaceHolder1_TxtChargeTotal").value;
            var amtTax = document.getElementById("ContentPlaceHolder1_TxtTax").value;
            var ctReturns = document.getElementById("ContentPlaceHolder1_TxtReturnsCount").value;
            var amtReturns = document.getElementById("ContentPlaceHolder1_TxtReturnsTotal").value;
            var amtReturnsTax = document.getElementById("ContentPlaceHolder1_TxtReturnsTax").value;
            var ctCorrection = document.getElementById("ContentPlaceHolder1_TxtCorrectionCount").value;
            var amtCorrection = document.getElementById("ContentPlaceHolder1_TxtCorrectionTotal").value;
            var ctVoid = document.getElementById("ContentPlaceHolder1_TxtVoidCount").value;
            var amtVoid = document.getElementById("ContentPlaceHolder1_TxtVoidTotal").value;
            var ctAllVoid = document.getElementById("ContentPlaceHolder1_TxtAllVoidCount").value;
            var amtAllVoid = document.getElementById("ContentPlaceHolder1_TxtAllVoidTotal").value;

            var ctFurniture = document.getElementById("ContentPlaceHolder1_TxtFurnitureCount").value;
            var amtFurniture = document.getElementById("ContentPlaceHolder1_TxtFurnitureTotal").value;
            var ctJewelry = document.getElementById("ContentPlaceHolder1_TxtJewelryCount").value;
            var amtJewelry = document.getElementById("ContentPlaceHolder1_TxtJewelryTotal").value;
            var ctElectrical = document.getElementById("ContentPlaceHolder1_TxtElectricalCount").value;
            var amtElectrical = document.getElementById("ContentPlaceHolder1_TxtElectricalTotal").value;
            var ctWomens = document.getElementById("ContentPlaceHolder1_TxtWomensCount").value;
            var amtWomens = document.getElementById("ContentPlaceHolder1_TxtWomensTotal").value;
            var ctBins = document.getElementById("ContentPlaceHolder1_TxtBinsCount").value;
            var amtBins = document.getElementById("ContentPlaceHolder1_TxtBinsTotal").value;
            var ctMisc = document.getElementById("ContentPlaceHolder1_TxtMiscCount").value;
            var amtMisc = document.getElementById("ContentPlaceHolder1_TxtMiscTotal").value;
            var ctShoes = document.getElementById("ContentPlaceHolder1_TxtShoesCount").value;
            var amtShoes = document.getElementById("ContentPlaceHolder1_TxtShoesTotal").value;
            var ctBoutique = document.getElementById("ContentPlaceHolder1_TxtBoutiqueCount").value;
            var amtBoutique = document.getElementById("ContentPlaceHolder1_TxtBoutiqueTotal").value;
            var ctChilds = document.getElementById("ContentPlaceHolder1_TxtChildsCount").value;
            var amtChilds = document.getElementById("ContentPlaceHolder1_TxtChildsTotal").value;
            var ctMens = document.getElementById("ContentPlaceHolder1_TxtMensCount").value;
            var amtMens = document.getElementById("ContentPlaceHolder1_TxtMensTotal").value;
            var ctBooks = document.getElementById("ContentPlaceHolder1_TxtBooksCount").value;
            var amtBooks = document.getElementById("ContentPlaceHolder1_TxtBooksTotal").value;
            var ctNewMerch = document.getElementById("ContentPlaceHolder1_TxtNewMerchCount").value;
            var amtNewMerch = document.getElementById("ContentPlaceHolder1_TxtNewMerchTotal").value;

            // Discount amounts
            var amtFurnitureDisc = document.getElementById("ContentPlaceHolder1_TxtFurnitureTotalDisc").value;
            var amtJewelryDisc = document.getElementById("ContentPlaceHolder1_TxtJewelryTotalDisc").value;
            var amtElectricalDisc = document.getElementById("ContentPlaceHolder1_TxtElectricalTotalDisc").value;
            var amtWomensDisc = document.getElementById("ContentPlaceHolder1_TxtWomensTotalDisc").value;
            var amtBinsDisc = document.getElementById("ContentPlaceHolder1_TxtBinsTotalDisc").value;
            var amtMiscDisc = document.getElementById("ContentPlaceHolder1_TxtMiscTotalDisc").value;
            var amtShoesDisc = document.getElementById("ContentPlaceHolder1_TxtShoesTotalDisc").value;
            var amtBoutiqueDisc = document.getElementById("ContentPlaceHolder1_TxtBoutiqueTotalDisc").value;
            var amtChildsDisc = document.getElementById("ContentPlaceHolder1_TxtChildsTotalDisc").value;
            var amtMensDisc = document.getElementById("ContentPlaceHolder1_TxtMensTotalDisc").value;
            var amtBooksDisc = document.getElementById("ContentPlaceHolder1_TxtBooksTotalDisc").value;

            // Store Tax
            if (!isNaN(amtTax) && amtTax.length != 0) {
                sumAmtTax += parseFloat(amtTax);
            }
            // Calculate Customer Count
            if (!isNaN(ctCash) && ctCash.length != 0) {
                sumCust += parseFloat(ctCash);
            }
            if (!isNaN(ctCharge) && ctCharge.length != 0) {
                sumCust += parseFloat(ctCharge);
            }
            document.getElementById("CustCount").innerHTML = sumCust.toFixed(0);

            // Calculate NS total
            if (!isNaN(amtCash) && amtCash.length != 0) {
                sumAmtNS += parseFloat(amtCash);
            }
            if (!isNaN(amtCharge) && amtCharge.length != 0) {
                sumAmtNS += parseFloat(amtCharge);
            }
            document.getElementById("NSTotal").innerHTML = sumAmtNS.toFixed(2);
            document.getElementById("Z_FINANCIAL").innerHTML = sumAmtNS.toFixed(2);

            // Calculate GS Count
            if (!isNaN(ctReturns) && ctReturns.length != 0) {
                sumCtGS += parseFloat(ctReturns);
            }

            if (!isNaN(ctCorrection) && ctCorrection.length != 0) {
                sumCtGS += parseFloat(ctCorrection);
            }

            if (!isNaN(ctVoid) && ctVoid.length != 0) {
                sumCtGS += parseFloat(ctVoid);
            }

            if (!isNaN(ctAllVoid) && ctAllVoid.length != 0) {
                sumCtGS += parseFloat(ctAllVoid);
            }

            if (!isNaN(ctFurniture) && ctFurniture.length != 0) {
                sumCtGS += parseFloat(ctFurniture);
            }
            if (!isNaN(ctJewelry) && ctJewelry.length != 0) {
                sumCtGS += parseFloat(ctJewelry);
            }
            if (!isNaN(ctElectrical) && ctElectrical.length != 0) {
                sumCtGS += parseFloat(ctElectrical);
            }
            if (!isNaN(ctWomens) && ctWomens.length != 0) {
                sumCtGS += parseFloat(ctWomens);
            }
            if (!isNaN(ctBins) && ctBins.length != 0) {
                sumCtGS += parseFloat(ctBins);
            }
            if (!isNaN(ctMisc) && ctMisc.length != 0) {
                sumCtGS += parseFloat(ctMisc);
            }
            if (!isNaN(ctShoes) && ctShoes.length != 0) {
                sumCtGS += parseFloat(ctShoes);
            }
            if (!isNaN(ctBoutique) && ctBoutique.length != 0) {
                sumCtGS += parseFloat(ctBoutique);
            }
            if (!isNaN(ctChilds) && ctChilds.length != 0) {
                sumCtGS += parseFloat(ctChilds);
            }
            if (!isNaN(ctMens) && ctMens.length != 0) {
                sumCtGS += parseFloat(ctMens);
            }
            if (!isNaN(ctBooks) && ctBooks.length != 0) {
                sumCtGS += parseFloat(ctBooks);
            }
            if (!isNaN(ctNewMerch) && ctNewMerch.length != 0) {
                sumCtGS += parseFloat(ctNewMerch);
            }
            document.getElementById("GSCount").innerHTML = sumCtGS.toFixed();

            //  Calculate Gross Sales Amount
            if (!isNaN(amtCash) && amtCash.length != 0) {
                sumAmtGS += parseFloat(amtCash);
            }
            if (!isNaN(amtCharge) && amtCharge.length != 0) {
                sumAmtGS += parseFloat(amtCharge);
            }
            if (!isNaN(amtReturns) && amtReturns.length != 0) {
                sumAmtGS += parseFloat(amtReturns);
            }
            if (!isNaN(amtReturnsTax) && amtReturnsTax.length != 0) {
                sumAmtGS += parseFloat(amtReturnsTax);
            }
            if (!isNaN(amtCorrection) && amtCorrection.length != 0) {
                sumAmtGS += parseFloat(amtCorrection);
            }
            if (!isNaN(amtVoid) && amtVoid.length != 0) {
                sumAmtGS += parseFloat(amtVoid);
            }
            if (!isNaN(amtAllVoid) && amtAllVoid.length != 0) {
                sumAmtGS += parseFloat(amtAllVoid);
            }
            document.getElementById("GSTotal").innerHTML = sumAmtGS.toFixed(2);


            //   Department Totals
            if (!isNaN(amtFurniture) && amtFurniture.length != 0) {
                sumAmtDept += parseFloat(amtFurniture);
            }
            if (!isNaN(amtJewelry) && amtJewelry.length != 0) {
                sumAmtDept += parseFloat(amtJewelry);
            }
            if (!isNaN(amtElectrical) && amtElectrical.length != 0) {
                sumAmtDept += parseFloat(amtElectrical);
            }
            if (!isNaN(amtWomens) && amtWomens.length != 0) {
                sumAmtDept += parseFloat(amtWomens);
            }
            if (!isNaN(amtBins) && amtBins.length != 0) {
                sumAmtDept += parseFloat(amtBins);
            }
            if (!isNaN(amtMisc) && amtMisc.length != 0) {
                sumAmtDept += parseFloat(amtMisc);
            }
            if (!isNaN(amtShoes) && amtShoes.length != 0) {
                sumAmtDept += parseFloat(amtShoes);
            }
            if (!isNaN(amtBoutique) && amtBoutique.length != 0) {
                sumAmtDept += parseFloat(amtBoutique);
            }
            if (!isNaN(amtChilds) && amtChilds.length != 0) {
                sumAmtDept += parseFloat(amtChilds);
            }
            if (!isNaN(amtMens) && amtMens.length != 0) {
                sumAmtDept += parseFloat(amtMens);
            }
            if (!isNaN(amtBooks) && amtBooks.length != 0) {
                sumAmtDept += parseFloat(amtBooks);
            }
            if (!isNaN(amtNewMerch) && amtNewMerch.length != 0) {
                sumAmtDept += parseFloat(amtNewMerch);
            }

            // Department Discount Total
            if (!isNaN(amtFurnitureDisc) && amtFurnitureDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtFurnitureDisc);
            }
            if (!isNaN(amtJewelryDisc) && amtJewelryDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtJewelryDisc);
            }
            if (!isNaN(amtElectricalDisc) && amtElectricalDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtElectricalDisc);
            }
            if (!isNaN(amtWomensDisc) && amtWomensDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtWomensDisc);
            }
            if (!isNaN(amtBinsDisc) && amtBinsDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtBinsDisc);
            }
            if (!isNaN(amtMiscDisc) && amtMiscDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtMiscDisc);
            }
            if (!isNaN(amtShoesDisc) && amtShoesDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtShoesDisc);
            }
            if (!isNaN(amtBoutiqueDisc) && amtBoutiqueDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtBoutiqueDisc);
            }
            if (!isNaN(amtChildsDisc) && amtChildsDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtChildsDisc);
            }
            if (!isNaN(amtMensDisc) && amtMensDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtMensDisc);
            }
            if (!isNaN(amtBooksDisc) && amtBooksDisc.length != 0) {
                sumAmtDeptDisc += parseFloat(amtBooksDisc);
            }

            // Returns Amount
            if (!isNaN(amtReturns) && amtReturns.length != 0) {
                sumAmtReturns += parseFloat(amtReturns);
            }

            document.getElementById("NSCount").innerHTML = sumCtGS - ctReturns;
            
           var sumAmt = sumAmtTax  + sumAmtDept  + sumAmtDeptDisc  - sumAmtNS ;
           document.getElementById("Z_DP_ALL").innerHTML = sumAmt.toFixed (2) ;

        }
    </script>
</h3>
           <table>
           <tr style="vertical-align: top">
              <td style="width: 240px"><b>Financial</b></td>
              <td style="width: 200px"><b>Department</b></td>
              <td style="width: 120px"></td>
              <td style="width: 200px;display: none">
                 
              </td>
           </tr>
           <tr style="vertical-align: top">
              <td>     <!--  This is the first column -->
                 <table>
                   <tr>
                      <td class="style120B">Location</td>
  		              <td class="style90B">
                         </td>
                    </tr>
                    <tr>
                       <td class="style120B"><b>GS</b></td>
                       <td><div style="color: #900; font-weight: bold; text-align: right" id="GSCount">0</div></td>
                       <td></td>
                    </tr>
                    <tr>
                       <td></td>
                       <td><div style="color: #900; font-weight: bold; text-align: right" id="GSTotal" >0.00</div></td>
                       <td></td>
                    </tr>
	                <tr>
		               <td><b>Tax1</b></td>
		               <td></td>
	                </tr>
                     
                    <tr>
                       <td><b>NS</b></td>
                       <td><div style="color: #900; font-weight: bold; text-align: right;" id="NSCount">0</div></td>
                       <td></td>
                    </tr>
                    <tr>
                       <td></td>
                       <td><div style="color: #900; font-weight: bold; text-align: right;" id="NSTotal">0.00</div></td>
                       <td></td>
                    </tr>
	                <tr>
		               <td><b>1Disc-RT</b></td>
		               <td></td>
                    </tr>
                    <tr>
                       <td><b>Total Cust</b></td>
                       <td><div style="color: #900; font-weight: bold; text-align: right;" id="CustCount">0</div></td>
                       <td></td>
                    </tr>	                <tr>
		               <td><b>Cash</b></td>
		               <td></td>
	                </tr>
                 </table>
              </td>
              <td>   
 <!--  This is the second column -->
                  <table>
                    <tr>
                       <td></td>
                       <td><b>Full</b></td>
                       <td></td>
                    </tr>
	               <tr>
		              <td><b>Bins</b></td>
		              <td></td>
	               </tr>
                 </table> 
             </td>
             <td>    <!--  This is the third column -->         
                 <table>
                    <tr>
                        <td><b>Discount</b></td>
						<td></td>
                    </tr>
                    <tr>
		               <td></td>
                    </tr>
                  </table>
              </td>
              <td>    <!--  This is the fourth column -->
                  <table>
                   <tr>
                       <td><b>Add +/-</b></td>
                       <td></td> 
                       <td></td> 
                  </tr>
                   <tr>
                       <td style="vertical-align: top"><b>Z_FINANCIAL</b></td>
                       <td><div style="color: #900; font-weight: bold; text-align: right" id="Z_FINANCIAL">0</div></td>
                       <td></td> 
                   </tr>
                   <tr>
                       <td style="vertical-align: top"><b>Z_DP_ALL</b></td>
                       <td><div style="color: #900; font-weight: bold; text-align: right" id="Z_DP_ALL">0</div></td>
                       <td></td> 
                   </tr>
                   <tr>
                       <td></td>
                       <td></td>
                   </tr>
                 </table>              
              </td>
           </tr>
           </table>
    
    
    
           <table>
	         <tr  style="display:none">
		        <td>Discount2 St:</td>
                <td></td>
		        <td>