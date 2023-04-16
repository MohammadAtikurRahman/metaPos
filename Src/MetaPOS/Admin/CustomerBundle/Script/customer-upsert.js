

function change() {
    if (columnAccess[0]["isCusDesignation"] == "1") {
        $('#divCusDesignation').removeClass("disNone");
    }

    if (columnAccess[0]["displayInstalment"] == "1") {
        $('#divAccountNo').removeClass("disNone");
        $('#divInstallmentStatus').removeClass("disNone");
    }


    if (columnAccess[0]["businessType"].trim() == "diagnostic") {
        $('#divCusAge').removeClass("disNone");
    }
    console.log("blood:", columnAccess[0]["bloodGroup"]);
    if (columnAccess[0]["bloodGroup"] == "1") {
        $('#contentBody_CustomerOpt_divBloodGroup').removeClass("disNone");
    }

    if (columnAccess[0]["businessType"].trim() == "diagnostic") {
        $('#divSex').removeClass("disNone");
    }


    $('#txtCustomerName,#txtCustomerPhone,#txtCustomerAddress,#txtCustomerEmail,#txtCustomerNotes').keypress(function (event) {

        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {

            var action = $('#lblActionText').text();
            if (action == 'save') {
                $(".btn-save").trigger("click");
            }
            else {
                $(".btn-update").trigger("click");
            }

            event.preventDefault();
        }

    });


    $('#txtPaymentAmt,#txtPaymentDueDate').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnCustomerPayment").trigger("click");
            event.preventDefault();
        }
    });


    $('#txtOpeningDueAmt,#txtOpeningDueDate').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnCustomerOpeningDue").trigger("click");
            event.preventDefault();
        }
    });

}





// Trigger Save Modal
var cusCounter = 0;
$(document).on("click",
    "#btnSaveCustomer",
    function (e) {

        var name = $.trim($('#txtCustomerName').val());
        var phone = $.trim($('#txtCustomerPhone').val());

        if (name == null || name == "") {
            showModalOutput("Name field required!", "warning");
            return;
        }

        if (columnAccess[0]["isCustomerRequired"] == "1") {
            if (phone.length != 11) {
                showModalOutput("Phone number is not valid", "warning");
                return;
            }
        }

        if (cusCounter == 0) {
            upsertCustomerInfo();
            cusCounter++;
        }


    });


$(document).on("click",
    "#btnCustomerModal",
    function () {
        resetCustomerForm();

        $('#lblTitle').text('পরবর্তি কাস্টমার');
        $('#btnSaveCustomer').show();
        $('#btnUpdateCustomer').hide();

        $('#lblActionText').text('save');
        setTimeout(function () { $('input[name="txtCustomerName"]').focus(); }, 1000);

        // Set Page 
        $('#lblActionPage').text("customer");

        change();
        
        $('#modalCustomer').modal('show');
    });




$(document).on("click",
    "#viewCustomer",
    function () {
        var result = viewCustomer();
        $('#modalLoadResult').html(result);

        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();

        $('#lblTitle').text(Customer_Details);
        $('#lblCustomerType').text(data['CusType'] == "0" ? "Retail" : "Wholesale");
        $('#lblCusName').text(data['name']);
        $('#lblCusPhone').text(data['phone']);
        $('#lblCusAddress').text(data['address']);
        $('#lblCusEmail').text(data['mailInfo']);
        $('#lblCusNotes').text(data['notes']);
        $('#lblAccountNo').text(data['AccountNo']);
        $('#lblCusDesignation').text(data['designation']);

        if (columnAccess[0]["isCusDesignation"] == "1") {
            $('#divCusDesignation').removeClass("disNone");
        }

        // view label text for translate
        $('#lblCusType').text(CustomerType);
        $('#lblCustomerName').text(Name);
        $('#lblCustomerPhone').text(Phone);
        $('#lblCustomerAddress').text(Address);
        $('#lblCustomerEmail').text(Email);
        $('#lblCustomerNote').text(Notes);
        $('#lblCustomerDesignation').text(Designation);
        $('.btnCloseCustomer').text(BtnClose);
        
        

        $('#customerModal').modal('show');
    });





$(document).on("click",
    "#editCustomer",
    function () {

        change();

        $('#lblActionText').text('update');
        setTimeout(function () { $('input[name="txtCustomerName"]').focus(); }, 1000);

        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();

        $('#lblTitle').text('Edit Customer');
        $('#btnSaveCustomer').hide();
        $('#btnUpdateCustomer').show();

        $('#lblId').text(data['cusID']);
        $("input[name='customerType']:checked").val(data['cusType']);
        $('#txtAccountNo').val(data['AccountNo']);
        $('#txtCustomerName').val(data['name']);
        $('#txtCustomerPhone').val(data['phone']);
        $('#txtCustomerAddress').val(data['address']);
        $('#txtCustomerEmail').val(data['mailInfo']);
        $('#txtCustomerNotes').val(data['notes']);
        $('#txtCusDesignation').val(data['designation']);
        $('#modalCustomer').modal('show');
    });





$(document).on("click", "#btnUpdateCustomer", function () {
    updateCustomerData();
});


$(document).on("click",
    "#addAdvanceCustomer",
    function () {
        $('#lblTitle').text('Add Advance');
        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();
        $('#contentBody_lblCusIdAdvance').text(data["cusID"]);
        $('#advanceModal').modal('show');
    });



$(document).on("click",
    "#btnCustomerAdvance",
    function () {

        if ($('#txtAdvanceAmt').val() == "") {
            showModalOutputAdvance("Amount cann't be empty", "warning");
            return;
        }


        saveCustomerAdvanceAmount();

        $('#advanceModal').modal('hide');


        // reset
        $('#contentBody_lblCusIdAdvance').text("");
        $("txtAdvanceAmt").val();
        $("txtAdvanceDueDate").val();


    });








$(document).on("click",
    "#addOpeningDue",
    function () {

        //var result = addOpeningDueCustomer();
        //$('#modalLoadResult').html(result);


        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();
        $('#contentBody_lblCustomerId').text(data["cusID"]);


        $('#openingDueModal').modal('show');



    });


$(document).on("click",
    "#btnCustomerOpeningDue",
    function () {
        if ($('#txtOpeningDueAmt').val() == "") {
            showModalOutputOpeningDue("Amount cann't be empty", "warning");
            return;
        }

        if ($('#contentBody_lblCustomerId').text() == "" || null) {
            showModalOutputOpeningDue("Customer ID is not valid.", "warning");
            return;
        }

        saveCustomerOpeingDueAmount();

        $('#openingDueModal').modal('hide');

        // reset 
        $('#txtOpeningDueAmt').val("");
        $('#txtOpeningDueDate').val("");
        $('#lblCusId').text("");

    });







/* Add payemtn */
$(document).on("click",
    "#addPayment",
    function () {

        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();

        $('#contentBody_lblPaymentCusId').text(data["cusID"]);
        $('#contentBody_lblPaymentCusName').text(data["name"]);


        $('#paymentModal').modal('show');



    });


$(document).on("click",
    "#btnCustomerPayment",
    function () {

        if ($('#txtPaymentAmt').val() == "") {
            showModalOutputPayment("Amount cann't be empty", "warning");
            return;
        }

        if ($('#contentBody_lblPaymentCusId').text() == "" || null) {
            showModalOutputPayment("Customer ID is not valid.", "warning");
            return;
        }

        saveCustomerPayment();

        $('#paymentModal').modal('hide');


        // reset 
        $('#txtPaymentAmt').val("");
        $('#txtOpeningDueDate').val("");
        $('#contentBody_lblPaymentCusId').text("");


    });




$(document).on("click",
    "#invoiceStatus",
    function () {

        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();


        var cusId = data["cusID"];

        window.location.href = baseUrl + "admin/slip?cusid=" + cusId + "";

    });





$(document).on("click",
    "#fullLedgerReport",
    function () {

        var result = fullLedgerCustomer();
        $('#modalLoadResult').html(result);

        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();


        $('#contentBody_txtLederCusId').text(data["cusID"]);
        $('#contentBody_txtLederCusId').val(data["cusID"]);


        $('#customerLedgerModal').modal('show');

    });













$(document).on("click",
    "#deleteCustomer",
    function () {

        var result = deleteCustomer();
        $('#modalLoadResult').html(result);

        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();
        $('#lblId').text(data["cusID"]);
        $('#lblCustomerName').text(data['name']);

        // Delete label text for translate
        $('#lblTitle').text(Delete_Customer);
        $('.btnCancelCustomer').text(CancelCustomer);
        $('.btnDeleteCustomer').text(Delete);


        $('#customerModal').modal('show');

    });


$(document).on("click",
    "#btnCustomerDelete",
    function () {

        var cusId = $('#lblId').text();
        delRestoreCustomerData(cusId, "0");

        $('#customerModal').modal("hide");

    });




$(document).on("click",
    "#restoreCustomer",
    function () {

        var result = restoreCustomer();
        $('#modalLoadResult').html(result);

        var table = $('#dataListTable').DataTable();
        var data = table.row($(this).closest('tr')).data();
        $('#lblId').text(data["cusID"]);
        $('#lblCustomerName').text(data['name']);

        $('#lblTitle').text('Restore Customer');

        $('#customerModal').modal('show');

    });


$(document).on("click",
    "#btnCustomerRestore",
    function () {

        var cusId = $('#lblId').text();
        delRestoreCustomerData(cusId, "1");

        $('#customerModal').modal("hide");

    });



function upsertCustomerInfo() {
    var cusId = $.trim($('#lblId').text());
    var accountNo = $('#txtAccountNo').val();
    var name = $.trim($('#txtCustomerName').val());
    var phone = $.trim($('#txtCustomerPhone').val());
    var address = $.trim($('#txtCustomerAddress').val());
    var email = $.trim($('#txtCustomerEmail').val());
    var notes = $('#txtCustomerNotes').val();
    var cusType = $('input[name=customerType]:checked').val();
    var designation = $('#txtCusDesignation').val();
    var bloodGroup = $('#contentBody_CustomerOpt_ddlBloodGroup option:selected').val();
    var age = $('#txtCusAge').val() == "" ? "0" : $('#txtCusAge').val();
    var sex = $('#contentBody_CustomerOpt_ddlSex option:selected').val();

    var installmentStatus = $('#chkInstallmentStatus').is(":checked") == undefined || $('#contentBody_chkInstallmentStatus').is(":checked") == null ? false : true;
    if (columnAccess[0]["displayInstalment"] == "0") {
        installmentStatus = false;
    }

    var customerInfo = {
        "cusId": cusId,
        "name": name,
        "phone": phone,
        "address": address,
        "email": email,
        "designation": designation,
        "notes": notes,
        "cusType": cusType,
        "accountNo": accountNo,
        "installmentStatus": installmentStatus,
        "bloodGroup": bloodGroup,
        "age": age,
        "sex": sex
    };

    var newCustomer = {
        "id": '',
        "name": name,
        "phone": phone,
        "address": address,
        "email": email,
        "designation": designation,
        "notes": notes,
        "cusType": cusType,
        "accountNo": accountNo,
        "installmentStatus": installmentStatus,
        "bloodGroup": bloodGroup,
        "age": age,
        "sex": sex
    };

    console.log("Test cus:", customerInfo, "//newCustomer:" + newCustomer);

    var url = baseUrl + "Admin/CustomerBundle/View/Customer.aspx/SaveCustomerDataAction";

    // 
    var type = $('#lblActionPage').text();
    upsertCustomerAjaxRequest(url, customerInfo, type, newCustomer);
}




function upsertCustomerAjaxRequest(url, customerInfo, type, newCustomer) {

    $.ajax({
        url: url,
        data: "{ 'jsonStrData' : '" + JSON.stringify(customerInfo) + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != "0") {
                $('#customerModal').modal("hide");
                showMessage("Customer saved successfully.", "Success");

                newCustomer["cusId"] = data.d;
                // push to localstorage
                //var cusData = [];
                //cusData = JSON.parse(localStorage.getItem('metaposCustomers'));
                //cusData.push(newCustomer);
                //localStorage.setItem('metaposCustomers', JSON.stringify(cusData));

            }
            else {
                showModalOutput("Error! Saving Customer info.", "warning");
                cusId = 0;
                return;
            }

            cusCounter = 0;

            resetCustomerForm();


            if (type == "sale") {
                cusId = data.d;
                getCustomerListSelectedByCusID(cusId);
            }
            else if (type == "customer") {
                loadCustomerDatatable();
            }

            $('#modalCustomer').modal('hide');

        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });

}


function resetCustomerForm() {

    $.trim($('#txtCustomerName').val(""));
    $.trim($('#txtCustomerPhone').val(""));
    $.trim($('#txtCustomerAddress').val(""));
    $.trim($('#txtCustomerEmail').val(""));
    $('#txtCusDesignation').val("");
    $('#txtAccountNo').val("");
    $('#txtCustomerNotes').val("");
    $('#chkInstallmentStatus').attr("checked", false);
    $('#rblCusType').index = 0;
    $('#lblId').text("");

}




/* ====================================
    
    
*/



function addNewOrEditCustomer() {

    var contentHtml = (function () {/*

                    <div class="form-group group-section">
                        <label class="col-md-4">Cusotmer Type</label>
                        <div class="col-md-8 rblCusType">

                            <label class="radio-inline">
                              <input type="radio" name="customerType" value="0" checked>Retail
                            </label>
                            <label class="radio-inline">
                              <input type="radio" name="customerType" value="1">Wholesale
                            </label>

                        </div>
                    </div>

                    <div class="form-group group-section disNone" id="divAccountNo">
                        <label class="col-md-4">Account No</label>
                        <div class="col-md-8">
                            <input type="text" class="form-control" id="txtAccountNo"/>
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <label class="col-md-4">Name</label>
                        <div class="col-md-8">
                            <input type="text" class="form-control char-input-validate lang-customer-name" id="txtCustomerName"/>
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <div class="col-md-4">
                            <label>Phone</label>
                        </div>
                        <div class="col-md-8">
                            <input type="text" class="form-control" id="txtCustomerPhone"/>
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <div class="col-md-4">
                            <label>Address</label>
                        </div>
                        <div class="col-md-8">
                            
                            <input type="text" class="form-control" id="txtCustomerAddress"/>
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <div class="col-md-4">
                            <label>Email</label>
                        </div>
                        <div class="col-md-8">
                            <input type="text" class="form-control" id="txtCustomerEmail"/>
                        </div>
                    </div>

                    <div class="form-group group-section disNone" id="divCusDesignation">
                        <div class="col-md-4">
                            <label>Designation</label>
                        </div>
                        <div class="col-md-8">
                            <input type="text" class="form-control" id="txtCusDesignation"/>
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <div class="col-md-4">
                            <label>Notes</label>
                        </div>
                        <div class="col-md-8">
                            <textarea id="txtCustomerNotes" class="form-control" ></textarea>
                        </div>
                    </div>

                    <div class="form-group disNone" id="divInstallmentStatus">
                        <div class="col-md-4"></div>
                        <div class="col-md-8 installment-status">
                            <div class="checkbox ">
                              <label><input type="checkbox" id="chkInstallmentStatus"  value=""> Pay with installment</label>
                            </div>
                        </div>
                    </div>


                </div>
                <div style="clear: both"></div>
                <div class="modal-footer" runat="server" id="Div1">
                    <button type="button" class="btn btnCustomize" data-dismiss="modal" id="btnCloseCusModal">Close</button>
                    <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnSaveCustomer">Save</button>
                    <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnUpdateCustomer">Update</button>
                </div>       
            */}).toString().match(/[^]*\/\*([^]*)\*\/\}$/)[1];
    return contentHtml;
}


function viewCustomer() {

    var contentHtml = (function () {/*
    <div class="view-customer">
        <div class="form-group group-section">
            <div class="col-md-4">
                <label id="lblCusType">Customer Type</label>
            </div>
            <div class="col-md-8">
                <label id="lblCustomerType"></label>
            </div>
        </div>

        <div class="form-group group-section  disNone">
            <div class="col-md-4">
                <label>Account No</label>
            </div>
            <div class="col-md-8">
                <label id="lblAccountNo"></label>
            </div>
        </div>

        <div class="form-group group-section">
            <div class="col-md-4">
                <label id="lblCustomerName">Name</label>
            </div>
            <div class="col-md-8">
                <label id="lblCusName"></label>
            </div>
        </div>

        <div class="form-group group-section">
            <div class="col-md-4">
                <label id="lblCustomerPhone">Phone</label>
            </div>
            <div class="col-md-8">
                <label id="lblCusPhone"></label>
            </div>
        </div>

        <div class="form-group group-section">
            <div class="col-md-4">
                <label id="lblCustomerAddress">Address</label>
            </div>
            <div class="col-md-8">
                <label id="lblCusAddress"></label>
            </div>
        </div>

        <div class="form-group group-section">
            <div class="col-md-4">
                <label id="lblCustomerEmail">Email</label>
            </div>
            <div class="col-md-8">
                <label id="lblCusEmail"></label>
            </div>
        </div>

        <div class="form-group group-section disNone" id="divCusDesignation">
            <div class="col-md-4">
                <label id="lblCustomerDesignation">Designation</label>
            </div>
            <div class="col-md-8">
                <label id="lblCusDesignation"></label>
            </div>
        </div>

        <div class="form-group group-section">
            <div class="col-md-4">
                <label id="lblCustomerNote">Note</label>
            </div>
            <div class="col-md-8">
                <label id="lblCusNotes"></label>
            </div>
        </div>

        <div class="modal-footer" runat="server" id="Div1">
            <button type="button" class="btn btnCustomize btnCloseCustomer" data-dismiss="modal" id="btnCloseCusModal">Close</button>
        </div> 

    </div>
*/}).toString().match(/[^]*\/\*([^]*)\*\/\}$/)[1];

    return contentHtml;
}





function deleteCustomer() {

    var contentHtml = (function () {/*  
    <div class="view-customer">
        
        <div class="form-group group-section">
            <p id="CustomerDeleteText"> Are you sure want to delete : <b> <label id="lblCustomerName"></label> </b></p> 
        </div>
        
        <div class="modal-footer" runat="server" id="Div1">
            <button type="button" class="btn btnCustomize btnCancelCustomer" data-dismiss="modal" id="btnCloseCusModal">Cancel</button>
            <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom btnDeleteCustomer" id="btnCustomerDelete">Delete</button>
        </div> 

    </div>
*/}).toString().match(/[^]*\/\*([^]*)\*\/\}$/)[1];

    return contentHtml;

}


function restoreCustomer() {

    var contentHtml = (function () {/*  
    <div class="view-customer">
        
        <div class="form-group group-section">
            <p> Are you sure want to restore : <b> <label id="lblCustomerName"></label> </b></p> 
        </div>
        
        <div class="modal-footer" runat="server" id="Div1">
            <button type="button" class="btn btnCustomize" data-dismiss="modal" id="btnCloseCusModal">Cancel</button>
            <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnCustomerRestore">Restore</button>
        </div> 

    </div>
*/}).toString().match(/[^]*\/\*([^]*)\*\/\}$/)[1];

    return contentHtml;

}




function addAdvanceCustomer() {

    var contentHtml = (function () {/*  
    <div class="view-customer">
        
        <div class="form-group group-section">
            <div class="col-md-4">
                <label>Advance Amount</label>
            </div>
            <div class="col-md-8">
                <input type="text" id="txtAdvanceAmt" class="form-control">
            </div>
        </div>
        
        <div class="modal-footer" runat="server" id="Div1">
            <button type="button" class="btn btnCustomize" data-dismiss="modal" id="btnCloseCusModal">Close</button>
            <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnCustomerAdvance">Add</button>
        </div> 

    </div>
*/}).toString().match(/[^]*\/\*([^]*)\*\/\}$/)[1];

    return contentHtml;

}




function addOpeningDueCustomer() {

    var contentHtml = (function () {/*  
    <div class="view-customer">
        
        <div class="form-group group-section">
            <div class="col-md-4">
                <label>Opening Due Amount</label>
            </div>
            <div class="col-md-8">
                <input type="text" id="txtOpeningDueAmt" class="form-control">
            </div>
        </div>

        <div class="form-group group-section">
            <div class="col-md-4">
                <label>Date</label>
            </div>
            <div class="col-md-8">
                <input type="text" id="txtOpeningDueDate" class="form-control datepickerCSS">
            </div>
        </div>
        
        <div class="modal-footer" runat="server" id="Div1">
            <button type="button" class="btn btnCustomize" data-dismiss="modal" id="btnCloseCusModal">Close</button>
            <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnCustomerOpeningDue">Add</button>
        </div> 

    </div>
*/}).toString().match(/[^]*\/\*([^]*)\*\/\}$/)[1];

    return contentHtml;

}





function invoiceStatus() {

    var contentHtml = (function () {/*  
    <div class="view-customer-ledger">
        
        <div class="form-group group-section">
            <div class="col-md-6">
                <input type="text" id="txtDateTo" class="form-control">
            </div>
            <div class="col-md-6">
                <input type="text" id="txtDateForm" class="form-control">
            </div>
        </div>
        
        <div class="modal-footer" runat="server" id="Div1">
            <button type="button" class="btn btnCustomize" data-dismiss="modal" id="btnCloseCusModal">Close</button>
            <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnCustomerOpeningDue">Add</button>
        </div> 

    </div>
*/}).toString().match(/[^]*\/\*([^]*)\*\/\}$/)[1];

    return contentHtml;

}



function fullLedgerCustomer() {

    var contentHtml = (function () {/*  
    <div class="view-customer-ledger">
        
        <div class="form-group group-section">
            <div class="col-md-6">
                <input type="text" id="txtDateTo" class="form-control datepickerCSS" placeholder="Start Date">
            </div>
            <div class="col-md-6">
                <input type="text" id="txtDateForm" class="form-control datepickerCSS"  placeholder="End Date">
            </div>
        </div>
        
        <div class="modal-footer" runat="server" id="Div1">
            <button type="button" class="btn btnCustomize" data-dismiss="modal" id="btnCloseCusModal">Close</button>
            <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnLedgerPrint">Print</button>
        </div> 

    </div>
*/}).toString().match(/[^]*\/\*([^]*)\*\/\}$/)[1];

    return contentHtml;

}




function saveCustomerAdvanceAmount() {

    var jsonData = {
        "id": $('#contentBody_lblCusIdAdvance').text(),
        "amount": $('#txtAdvanceAmt').val() == "" ? 0 : $('#txtAdvanceAmt').val(),
        "date": $('#txtAdvanceDueDate').val()
    };


    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/saveCustomerAdvanceAmountAction",
        data: "{ 'jsonData' : '" + JSON.stringify(jsonData) + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == true) {
                showMessage("Advance amount saved successfully", "Success");
            }
            else {
                showMessage("Advance amount not saved", "Warning");
            }

            //reset
            $('#txtAdvanceAmt').val("");
            $('#txtAdvanceDueDate').val("");

            loadCustomerDatatable();
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });

}








function saveCustomerOpeingDueAmount() {

    var jsonData = {
        "id": $('#contentBody_lblCustomerId').text(),
        "amount": $('#txtOpeningDueAmt').val(),
        "date": $('#txtOpeningDueDate').val()
    };



    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/saveCustomerOpeingDueAmountAction",
        data: "{ 'jsonData' : '" + JSON.stringify(jsonData) + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == true) {
                showMessage("Opeing due amount saved successfully", "Success");
            }
            else {
                showMessage("Opeing due amount not saved", "Warning");
            }

            //reset
            $('#txtOpeningDueAmt').val("");
            $('#txtOpeningDueDate').val("");

            loadCustomerDatatable();
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });

}



function saveCustomerPayment() {

    var jsonData = {
        "id": $('#contentBody_lblPaymentCusId').text(),
        "payment": $('#txtPaymentAmt').val(),
        "date": $('#txtPaymentDueDate').val()
    };



    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/saveCustomerPaymentAmountAction",
        data: "{ 'jsonData' : '" + JSON.stringify(jsonData) + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var jsonPaymentData = data.d;

            var splitPaymentData = jsonPaymentData.split('|');
            var message = splitPaymentData[1];
            if (splitPaymentData[0] == "true") {
                showMessage(message, "Success");
            }
            else {
                showMessage(message, "Warning");
            }

            console.log("jsonPaymentData:", jsonPaymentData);


            //reset
            $('#txtPaymentAmt').val("");
            $('#txtPaymentDueDate').val("");

            loadCustomerDatatable();
        }
    });

}




function updateCustomerData() {

    var cusId = $('#lblId').text();
    if (cusId == "" || cusId == null) {
        showMessage("Customer ID is invalid.", "Warning");
        $('#customerModal').modal("hide");
        return;
    }

    var jsonData = {
        "id": cusId,
        "cusType": $("input[name='customerType']:checked").val(),
        "name": $('#txtCustomerName').val(),
        "address": $('#txtCustomerAddress').val(),
        "mailinfo": $('#txtCustomerEmail').val(),
        "notes": $('#txtCustomerNotes').val(),
        "phone": $('#txtCustomerPhone').val(),
        "accountNo": $('#txtAccountNo').val()
    };





    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/updateCustomerInfoDataAction",
        data: "{ 'jsonData' : '" + JSON.stringify(jsonData) + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == true) {
                showMessage("Customer Updated successfully", "Success");
            }
            else {
                showMessage("Customer Updated not successfully", "Warning");
            }

            resetCustomerForm();
            loadCustomerDatatable();

            $('#modalCustomer').modal('hide');
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });

}






function delRestoreCustomerData(cusId, active) {

    if (cusId == "" || cusId == null) {
        showMessage("Customer Id is not valid.", "Warning");
        $('#customerModal').modal("hide");
        return;
    }

    var jsonData = {
        "id": cusId,
        "active": active
    };


    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/delRestoreCustomerDataAction",
        data: "{ 'jsonData' : '" + JSON.stringify(jsonData) + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == true) {

                $('#customerModal').modal("hide");

                loadCustomerDatatable();

                showMessage("Customer Deleted successfully.", "Success");



            }
            else {
                $('#customerModal').modal("hide");
                showMessage("Customer is not deleted.", "Warning");

            }

        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });


}






function printCustomerLedger() {

    var cusId = $('#lblId').text();


    var jsonData = {
        "id": cusId
    };


    $.ajax({
        url: baseUrl + "Admin/CustomerBundle/View/Customer.aspx/fullCustomerLedgerPrintAction",
        data: "{ 'jsonData' : '" + JSON.stringify(jsonData) + "' }",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            window.location = baseUrl + "Admin/Print/LoadQuery.aspx";


        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });


}





/**** SET Focus **/


$("#txtAdvanceAmt").keypress(function () {
    //$('#btnCustomerAdvance').focus();
});