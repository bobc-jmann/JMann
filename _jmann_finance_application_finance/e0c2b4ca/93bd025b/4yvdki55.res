        ��  ��                  �$      �����e                 
    <link rel="stylesheet" type="text/css" href="Styles/jquery.tooltip.css" />
    <script src="Scripts/gridview-addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">
        var addEditTitle = "ZTape Record";
        var urlAndMethod = "GridViewAddEdit_ZTapeRecord.aspx/GetZTapeRecord";

        $(function () {
            InitializeDatePicker("input[id$=TxtDate]");
            InitializeDeleteConfirmation();
            InitializeAddEditRecord();
            InitializeToolTip();
        });

        function setURL() {
            var intRecordID = document.getElementById("ContentPlaceHolder1_ZTapeRecordID").value;
            var txtURL = "AddEdit_ZTapeRecord.aspx?operation=update&ztaperecordid="+intRecordID.ToString;
            window.Open(txtURL,"_self") ;
        };
        function clearFields() {
	        $("#
    </div>
    <div id="deleteConfirmationDialog"></div>
    <div id="deleteErrorDialog" title="An error occured during item deletion!"></div>

<!--    <a id="showAddNewRecord1" href="#"><img src="Images/Add.gif" alt="Add New ZTapeRecord" style="border: none;" /></a>&nbsp;<a id="showAddNewRecord2" href="#">Add New ZTapeRecord</a>
    <br /><br />

    <div id="divAddEditRecord" class="ui-widget-content" style="display: none; width: 600px; padding: 0px 10px 20px 10px;">
        <h3 id="h3AddEditRecord" class="ui-widget-header">Add New ZTapeRecord</h3>
        <table>
	        <tr id="trPrimaryKey">
		        <td>ZTape Record ID:</td>
                <td></td>
	        </tr>
            <tr>
                <th id="AddBlank"></th>
                <th id="AddBlank2"></th>
                <th id="AddAdjustment" style="text-align: left">Add Adjustment</th>
                <th id="AddBlank3"> </th>
                <th id="AddComment" style="text-align: left">Comment</th>
            </tr>
            <tr>
		        <td></td>
                <td></td>
                <td headers="AddAdjustment" style="vertical-align:top"></td> 
            </tr>
	        <tr>
                <th id="CashBlank"> </th>
                <th id="CashBlank2"> </th>
                <th id="CashAdjustment" style="text-align: left">Cash Adjustment</th>
                <th id="CashBlank3"> </th>
                <th id="CashComment" style="text-align: left">Comment</th>
            </tr>
            <tr>
		        <td></td>
                <td></td>
                <td headers="CashAdjustment" style="vertical-align:top"></td> 
            </tr>
            <tr>
                <th id="MeasureBlank"> </th>
                <th id="MeasureBlank2"> </th>
                <th id="MeasureCount" style="text-align: left;font-weight: Bold">Count</th>
                <th id="MeasureBlank3"> </th>
                <th id="MeasureAmount" style="text-align: left;font-weight: Bold">Amount</th>
            </tr>
	        <tr>
		        <td>Discount1 St:</td>
                <td></td>
		        <td>
    <link rel="stylesheet" type="text/css" href="Styles/jquery.tooltip.css" />
    <script src="Scripts/gridview-addrecord-script.js" type="text/javascript"></script>
    <script type="text/javascript">
        var addEditTitle = "Location";
        var urlAndMethod = "GridViewAddEdit_Location.aspx/GetLocation";

        $(function () {
            InitializeDeleteConfirmation();
            InitializeAddEditRecord();
            InitializeToolTip();
        });

        function clearFields() {
	        $("#

    <div id="deleteConfirmationDialog"></div>
    <div id="deleteErrorDialog" title="An error occured during item deletion!"></div>

    <a id="showAddNewRecord1" href="#"><img src="Images/Add.gif" alt="Add New Location" style="border: none;" /></a>&nbsp;<a id="showAddNewRecord2" href="#">Add New Location</a>
    <br /><br />
    <div id="divAddEditRecord" class="ui-widget-content" style="display: none; width: 600px; padding: 0px 10px 20px 10px;">
        <h3 id="h3AddEditRecord" class="ui-widget-header">Add New Location</h3>
        <table>
	        <tr id="trPrimaryKey">
		        <td>Location ID:</td>
                <td>
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
            $('#ContentPlaceHolder1_TxtThrownLbs').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtCartsWorkedHard').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtCartsWorkedSoft').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtOnHandHard').keyup(function () { refresh_totals() });
            $('#ContentPlaceHolder1_TxtOnHandSoft').keyup(function () { refresh_totals() });
        });
        function refresh_totals() {
            var wrkThrownCount = 0;
            var wrkCartsTotal = 0;
            var onHandTotal = 0;
            var wrkThrownLbs = document.getElementById("ContentPlaceHolder1_TxtThrownLbs").value;
            if (!isNaN(wrkThrownLbs) && wrkThrownLbs.length != 0) {
                wrkThrownCount += parseFloat(wrkThrownLbs) * parseFloat(1.25);
            }
            var wrkCartsHard = document.getElementById("ContentPlaceHolder1_TxtCartsWorkedHard").value;
            if (!isNaN(wrkCartsHard) && wrkCartsHard.length != 0) {
            	wrkCartsTotal += parseFloat(wrkCartsHard);
            }
            var wrkCartsSoft = document.getElementById("ContentPlaceHolder1_TxtCartsWorkedSoft").value;
            if (!isNaN(wrkCartsSoft) && wrkCartsSoft.length != 0) {
            	wrkCartsTotal += parseFloat(wrkCartsSoft);
            }
            var onHandHard = document.getElementById("ContentPlaceHolder1_TxtOnHandHard").value;
            if (!isNaN(onHandHard) && onHandHard.length != 0) {
            	onHandTotal += parseFloat(onHandHard);
            }
            var onHandSoft = document.getElementById("ContentPlaceHolder1_TxtOnHandSoft").value;
            if (!isNaN(onHandSoft) && onHandSoft.length != 0) {
            	onHandTotal += parseFloat(onHandSoft);
            }
            document.getElementById("ThrownCount").innerHTML = wrkThrownCount.toFixed(0);
             document.getElementById("ContentPlaceHolder1_TxtThrownCount").value = wrkThrownCount.toFixed(0);
             document.getElementById("CartsTotal").innerHTML = wrkCartsTotal.toFixed(0);
             document.getElementById("OnHandTotal").innerHTML = onHandTotal.toFixed(0);
		 }

    </script>
</td>
	        </tr>
	        <tr>
		        <td>Carts Total:</td>
                <td></td>
		        <td><div style="color: #900; font-weight: bold; text-align: right;width: 150px" id="CartsTotal" >0</div></td>
		        <td></td>
	        </tr>
	        <tr>
		        <td>On Hand Hard:</td>
                <td></td>
		        <td></td>
	        </tr>
	        <tr>
		        <td>On Hand Total:</td>
                <td></td>
		        <td><div style="color: #900; font-weight: bold; text-align: right;width: 150px" id="OnHandTotal" >0</div></td>
		        <td></td>
	        </tr>
	        <tr>
		        <td>Hang Total:</td>
                <td></td>
		        <td></td>
	        </tr>
	        <tr>
		        <td>Thrown Count:</td>
                <td></td>
		        <td><div style="color: #900; font-weight: bold; text-align: right;width: 150px" id="ThrownCount" >0</div></td>
		        <td></td>
	        </tr>
	        <tr>
		        <td>Ragged Lbs:</td>
                <td></td>
		        <td> 
<h1 style="text-align: center">General Instructions</h1>
<p style="margin-left: 10px">Use this application for entering Daily Register and ZTape information.<br>
Click on the "Add Record" at the bottom of the page when you have completed entering the information.<br>
Use the <b>TAB</b> key to move from one entry field to the next<br><br>
"View Daily Register List" and "View ZTape Record List" will display multiple records<br>
You can edit records or delete them by using the clicking on the appropriate icons on the row<br>
<b>EDIT Icon= </b> <img src="./Images/Edit.gif" alt="Edit Button"><b>        DELETE Icon= </b><img src="./Images/Delete.png"" alt="Delete Button"><br><br>
Run the "Daily Register by Location" report to view all entries by day and location.<br>
</p>   
 
