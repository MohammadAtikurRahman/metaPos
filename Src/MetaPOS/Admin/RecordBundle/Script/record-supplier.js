// On page load
function eventChange() {

    activeModule = "record";

    checkPagePermission("Supplier");

    displayDataTable();


    $('#txtCompany,#supplierCode,#contactName,#contactDesignation,#contactPhone,#email,#discount').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var action = $('#lblActionText').text();
            if (action == 'save') {
                $("#btnSave").trigger("click");
            }
            else {
                $("#btnUpdate").trigger("click");
            }

            event.preventDefault();
        }
    });

}

function Generator() { };
Generator.prototype.rand = Math.floor(Math.random() * 26) + Date.now();
Generator.prototype.getId = function () {
    return this.rand++;
};



// Display datatable list
function displayDataTable() {

    var jsonData = {
        "select": "Id,supId,supCompany,supCode,conName,conPhone,mailinfo as email,conTitle as designation, address, discount ",
        "from": "SupplierInfo",
        "where": {
            "active": $("#ddlActiveStatus").val()
        },
        "column": "id",
        "dir": "desc"
    };
    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data) {

            var actionEditHtml =
                "<a id='btnUpdateModal' href='javascript:void(0)' class='glyIconPosition' data-toggle='modal' data-backdrop='static'>" +
                    "<span class='glyphicon glyphicon-pencil'>" +
                    "</span></a>";

            var actionHtml;
            if ($("#ddlActiveStatus").val() === "1") {
                actionHtml = actionEditHtml +
                    "<a id='btnDeleteModal' href='javascript:void(0)' class='glyIconPosition' data-toggle='modal' data-backdrop='static'>" +
                    "<span class='glyphicon glyphicon-trash'>" +
                    "</span></a>";
            }
            else {
                actionHtml =
                    "<a id='btnRestoreModal' href='javascript:void(0)' class='glyIconPosition' data-toggle='modal' data-backdrop='static'>" +
                    "<span class='glyphicon glyphicon-retweet'>" +
                    "</span></a>";
            }

            $('#dataListTable').DataTable().destroy();
            $('#dataListTable').empty();

            // operation mode off for sub branch
            var dynamicCss = "";
            if (branchType == "sub")
                dynamicCss = "disNone";

            var table = $('#dataListTable').DataTable({
                "aaData": JSON.parse(data.d),
                "order": [[1, "desc"]],
                "columnDefs": [
                    {
                        "width": "5%",
                        "className": "id",
                        "targets": [0]
                    },
                    {
                        "width": "0%",
                        "className": "supId",
                        "targets": [1]
                    },
                    {
                        "className": "supCompany",
                        "width": "20%",
                        "targets": [2]
                    },
                    {
                        "className": "supCode",
                        "width": "15%",
                        "targets": [3]
                    },
                    {
                        "className": "conName",
                        "width": "20%",
                        "targets": [4]
                    },
                    {
                        "className": "conPhone",
                        "width": "20%",
                        "targets": [5]
                    },
                    {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action " + dynamicCss,
                        "width": "15%",
                        "targets": [6]
                    },
                    {
                        "width": "0%",
                        "className": "designation disNone",
                        "targets": [7]
                    },
                    {
                        "width": "0%",
                        "className": "email disNone",
                        "targets": [8]
                    },
                    {
                        "width": "0%",
                        "className": "discount disNone",
                        "targets": [9]
                    },
                    {
                        "width": "0%",
                        "className": "address disNone",
                        "targets": [10]
                    }
                ],
                "columns": [
                    {
                        "title": "",
                        "data": "Id"
                    },
                    {
                        "title": ID,
                        "data": "supId"
                    },
                    {
                        "title": Company_name,
                        "data": "supCompany"
                    },
                    {
                        "title": Supplier_code,
                        "data": "supCode"
                    },
                    {
                        "title": Contact_name,
                        "data": "conName"
                    },
                    {
                        "title": Contact_phone,
                        "data": "conPhone"
                    },
                    {
                        "title": Action,
                        "data": null
                    },
                    {
                        "title": Designation,
                        "data": "designation"
                    },
                    {
                        "title": Email,
                        "data": "email"
                    },
                    {
                        "title": Discount,
                        "data": "discount"
                    },
                    {
                        "title": Address,
                        "data": "address"
                    }
                ],
                "lengthMenu": [
                    [10, 25, 50, 100, -1],
                    [10, 25, 50, 100, "All"]
                ],
                "pageLength": 10,
                "paging": true,
                "searching": true
            });

            var buttons = new $.fn.dataTable.Buttons(table,
               {
                   "buttons": [
                       {
                           "extend": 'print',
                           "exportOptions": {
                               "columns": [0,1, 2,3,4]
                           },
                           "text": '',
                           "autoPrint":true,
                           "className": 'glyphicon glyphicon-print datatable-button',
                           "customize": function (win) {

                               $('h1').addClass('disNone');
                               $(win.document.body).find('h1').addClass('disNone').css('font-size', '9px');

                               $(win.document.body)
                                   .css('text-align', 'center');

                               var companyName = $('#contentBody_lblHiddenCompanyName').val();
                               var companyAddress = $('#contentBody_lblHiddenCompanyAddress').val();
                               var companyPhone = $('#contentBody_lblHiddenCompanyPhone').val();


                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Supplier List</b></p>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; padding-bottom: 5; margin: 0">' + companyPhone + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; margin: 0; margin-bottom: 5">' + companyAddress + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 25; margin-top: 5">' + companyName + '</h3>');


                           }
                       },
                       {
                           "extend": 'collection',
                           "exportOptions": {
                               "columns": [1, 2,3,4]
                           },
                           "text": '',
                           "className": 'glyphicon glyphicon-export datatable-button',
                           "buttons": [
                               {
                                   "extend": 'pdf',
                                   "exportOptions": {
                                       "columns": [1, 2,3,4]
                                   },
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1, 2,3,4]
                                   },
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1, 2,3,4]
                                   },
                               },
                           ]
                       }
                   ]
               }).container().appendTo($('#filterPanel'));
        },
        error: function(data) {
            showMessage(data.responseText, "Error");
        },
        failure: function(data) {
            showMessage(data.responseText, "Error");
        }
    });

}





// Trigger Save Modal
$(document).on("click",
    "#btnSaveModal",
    function() {
        resetFormValue();
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;<span class='lang-record-add-supplier'>" + Add_new_supplier + "</span>");
        $("#btnUpdate").hide();
        $("#btnSave").show();

        $('#lblActionText').text("save");
        setTimeout(function () { $('input[name="txtCompany"]').focus(); }, 1000);

        $('#formModal').modal("show");
    });


// Trigger Update Modal
$(document).on("click",
    "#btnUpdateModal",
    function() {
        resetFormValue();
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;" + Edit_supplier + "");
        $("#btnSave").hide();
        $("#btnUpdate").show();

        $('#lblActionText').text("update");
        setTimeout(function () { $('input[name="txtCompany"]').focus(); }, 1000);

        $('#txtCompany').val($(this).parent().parent().find(".supCompany").html());
        $('#supplierCode').val($(this).parent().parent().find(".supCode").html());
        $('#contactName').val($(this).parent().parent().find(".conName").html());
        $('#contactPhone').val($(this).parent().parent().find(".conPhone").html());
        $('#contactDesignation').val($(this).parent().parent().find(".designation").html());
        $('#email').val($(this).parent().parent().find(".email").html());
        $('#discount').val($(this).parent().parent().find(".discount").html());
        $('#address').val($(this).parent().parent().find(".address").html());

        var id = $(this).parent().parent().find(".id").html();
        var name = $(this).parent().parent().find(".name").html();      
        $('#formModal').data('id', id).data('name', name).modal("show");
    });


// Trigger Delete Modal
$(document).on("click",
    "#btnDeleteModal",
    function() {
        var id = $(this).parent().parent().find(".id").html();
        $('#deleteModal').data('id', id).modal("show");
    });


// Trigger Restore Modal
$(document).on("click",
    "#btnRestoreModal",
    function() {
        var id = $(this).parent().parent().find(".id").html();
        $('#restoreModal').data('id', id).modal("show");
    });


// Trigger Active Status 
$('#ddlActiveStatus').change(function() {
    displayDataTable();
});





// Reset form values
function resetFormValue() {
    $('#txtCompany,#supplierCode,#contactName,#contactPhone,#contactDesignation,#discount,#email,#address').val("");
}





// Save Action
$(document).on("click",
    "#btnSave",
    function() {

        if (validateForm() === false)
            return;

        var findjsonData = {
            "select": "supCode",
            "from": "SupplierInfo",
            "where": {
                "supCompany": $('#txtCompany').val()
            },
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
            dataType: "json",
            data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function(data) {

                if (data.d === "success") {
                    showModalOutput(" Company name is already exist! Try different code", "warning");
                }
                else {
                    saveAction();
                }
            },
            failure: function (response) {
                console.log(response);
                alert(response);
            },
            error: function (response) {
                console.log(response);
                alert(response);
            }
        });

    });





function saveAction() {

    var jsonData = {
        "from": "SupplierInfo",
        "values":
            {
                "supID": GenRandom.Job(),
                "supCompany": $('#txtCompany').val(),
                "supPhone": "",
                "conName": $('#contactName').val(),
                "conTitle": $('#contactDesignation').val(),
                "conPhone": $('#contactPhone').val(),
                "address": $('#address').val(),
                "fax": "",
                "mailInfo": $('#email').val(),
                "payMethod_": "",
                "discount": $('#discount').val(),
                "entryDate": currentDatetimeGlobal,
                "updateDate": currentDatetimeGlobal,
                "roleId": roleIdGlobal,
                "active": '1',
                "supCode": $('#supplierCode').val()
            }
           
};

$.ajax({
    url: baseUrl + "Admin/AppBundle/View/Operation.aspx/saveDataAction",
    dataType: "json",
    data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
    type: "POST",
    contentType: "application/json; charset=utf-8",
    success: function(data) {

        $("#formModal").modal("hide");

        showOutput(data.d);

        resetFormValue();

        displayDataTable();
    },
    error: function(data) {
        showMessage(data.responseText, "Error");
    },
    failure: function(data) {
        showMessage(data.responseText, "Error");
    }
});
}





// Update Action
$(document).on("click",
    "#btnUpdate",
    function() {

        if (validateForm() === false)
            return;


        updateAction();

        //var findjsonData = {
        //    "select": "supCompany",
        //    "from": "SupplierInfo",
        //    "where": {
        //        "supCompany": $('#txtCompany').val()
        //    },
        //};

        //$.ajax({
        //    url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
        //    dataType: "json",
        //    data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
        //    type: "POST",
        //    contentType: "application/json; charset=utf-8",
        //    success: function(data) {

        //        if (data.d === "success") {
        //            showModalOutput("Supplier name is already exist! Try different name", "warning");
        //        }
        //        else {
                    
        //        }
        //    },
        //    error: function(data) {
        //        console.log(data.responseText, "Error");
        //    },
        //    failure: function(data) {
        //        console.log(data.responseText, "Error");
        //    }
        //});

    });





function updateAction() {

    var jsonData = {
        "from": "SupplierInfo",
        "set": {
            "supCompany": $('#txtCompany').val(),
            "supCode": $('#supplierCode').val(),
            "conName": $('#contactName').val(),
            "conPhone": $('#contactPhone').val(),
            "conTitle": $('#contactDesignation').val(),
            "address": $('#address').val(),
            "mailInfo": $('#email').val(),
            "discount": $('#discount').val()
        },
        "where": {
            Id: $('#formModal').data('id')
        }
    };

    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/updateDataAction",
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function(data) {

            $("#formModal").modal("hide");

            showOutput(data.d);

            resetFormValue();

            displayDataTable();
        },
        error: function(data) {
            showMessage(data.responseText, "Error");
        },
        failure: function(data) {
            showMessage(data.responseText, "Error");
        }
    });
}





// Delete Action
$(document).on("click",
    "#btnDelete",
    function() {

        var jsonData = {
            "from": "SupplierInfo",
            "set": {
                "active": 0
            },
            "where": {
                "Id": $('#deleteModal').data('id')
            }
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/deleteDataAction",
            dataType: "json",
            data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function(data) {

                $("#deleteModal").modal("hide");

                showOutput(data.d);

                displayDataTable();
            },
            error: function(data) {
                showMessage(data.responseText, "Error");
            },
            failure: function(data) {
                showMessage(data.responseText, "Error");
            }
        });

    });


// Restore Action
$(document).on("click",
    "#btnRestore",
    function() {

        var jsonData = {
            "from": "SupplierInfo",
            "set": {
                "active": 1
            },
            "where": {
                "Id": $('#restoreModal').data('id')
            }
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/restoreDataAction",
            dataType: "json",
            data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function(data) {

                $("#restoreModal").modal("hide");

                showOutput(data.d);

                displayDataTable();
            },
            error: function(data) {
                showMessage(data.responseText, "Error");
            },
            failure: function(data) {
                showMessage(data.responseText, "Error");
            }
        });

    });





// Validate Form
function validateForm() {

    var validate = true;
    var name = $('#txtCompany').val();
    var phone = $('#contactPhone').val();
    var regPhone = /^(?:\+88|01)?\d{11}$/;

    if (phone != "") {
        if (!regPhone.test(phone)) {
            validate = false;
            showModalOutput("Invalid number! phone number must contains 11 digits ", "warning");
        }
    }
    if (name === "") {
        validate = false;
        showModalOutput("Company field required!", "warning");
        
    }

    return validate;
}