// On page load
function eventChange() {

    activeModule = "record";

    checkPagePermission("Staff");

    displayDataTable();


    $('#txtName,#phone,#birthday').keypress(function (event) {
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

    $('#staffSexStatus,#ddlDepartment,#ddlStore').change(function (event) {
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


$(function () {

    // staff birthdate 
    $('#birthday').val(moment().format("DD-MMM-YYYY"));

    loadStaffData();

    $("#contentBody_ddlReferredBy").select2({
        placeholder: "Select a referrel",
        allowClear: true
    });
});





// Display datatable list
function displayDataTable() {

    var jsonData = {
        "select": "staff.Id,staff.name,staff.phone,staff.dob as birthday,staff.sex,staff.address,staff.department,staff.storeId, store.name as storeName",
        "from": "StaffInfo as staff LEFT JOIN WarehouseInfo as store On staff.storeId = store.Id ",
        "where": {
            "staff.active": $("#ddlActiveStatus").val()
        },
        "column": "staff.id",
        "dir": "desc"
    };
    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

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

            var table = $('#dataListTable').DataTable({
                "aaData": JSON.parse(data.d),
                "order": [[1, "asc"]],
                "columnDefs": [
                    {
                        "width": "5%",
                        "className": "id",
                        "targets": [0]
                    },
                    {
                        "className": "name",
                        "width": "25%",
                        "targets": [1]
                    },
                    {
                        "className": "phone",
                        "width": "10%",
                        "targets": [2]
                    },
                    {
                        "className": "sex",
                        "width": "10%",
                        "targets": [3]
                    },
                    {
                        "className": "address",
                        "width": "25%",
                        "targets": [4]
                    },
                     {
                         "className": "department disNone",
                         "width": "0%",
                         "targets": [5]
                     },
                      {
                          "className": "storeId disNone",
                          "width": "0%",
                          "targets": [6]
                      },
                      {
                          "className": "storeName",
                          "width": "15%",
                          "targets": [7]
                      },
                       {
                           "className": "birthday disNone",
                           "width": "15%",
                           "targets": [8]
                       },
                        {
                            "className": "address disNone",
                            "width": "10%",
                            "targets": [9]
                        },
                    {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action",
                        "width": "15%",
                        "targets": [10]
                    }
                ],
                "columns": [
                    {
                        "title": ID,
                        "data": "Id"
                    },
                    {
                        "title": StaffName,
                        "data": "name"
                    },
                    {
                        "title": Phone,
                        "data": "phone"
                    },
                    {
                        "title": Gender,
                        "data": "sex"
                    },
                    {
                        "title": Address,
                        "data": "address"
                    },
                    {
                        "title": "department",
                        "data": "department"
                    },
                    {
                        "title": "storeId",
                        "data": "storeId"
                    },
                    {
                        "title": Store_name,
                        "data": "storeName"
                    },
                    {
                        "title": "Birth Date",
                        "data": "birthday",
                        "render": function(birthday) {
                            return moment(birthday).format("DD-MMM-YYYY");
                        }
                    },
                    {
                        "title": "Address",
                        "data": "address"
                    },
                    {
                        "title": Action,
                        "data": null
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
                                "columns": [0,1, 2, 3, 4]
                            },
                            "text": '',
                            "autoPrint": true,
                            "className": 'glyphicon glyphicon-print datatable-button',
                            "customize": function (win) {

                                $('h1').addClass('disNone');
                                $(win.document.body).find('h1').addClass('disNone').css('font-size', '9px');

                                $(win.document.body)
                                    .css('text-align', 'center');

                                var companyName = $('#contentBody_lblHiddenCompanyName').val();
                                var companyAddress = $('#contentBody_lblHiddenCompanyAddress').val();
                                var companyPhone = $('#contentBody_lblHiddenCompanyPhone').val();

                                $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Staff List</b></p>');
                                $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; padding-bottom: 5; margin: 0">' + companyPhone + '</h3>');
                                $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; margin: 0; margin-bottom: 5">' + companyAddress + '</h3>');
                                $(win.document.body).prepend('<h3 style="text-align: center; font-size: 25; margin-top: 5">' + companyName + '</h3>');


                            }
                        },
                        {
                            "extend": 'collection',
                            "exportOptions": {
                                "columns": [1, 2, 3, 4]
                            },
                            "text": '',
                            "className": 'glyphicon glyphicon-export datatable-button',
                            "buttons": [
                                {
                                    "extend": 'pdf',
                                    "exportOptions": {
                                        "columns": [1, 2, 3, 4]                    
                                    },
                                },
                                {
                                    "extend": 'excel',
                                    "exportOptions": {
                                        "columns": [1, 2, 3, 4]
                                    },
                                },
                                {
                                    "extend": 'csv',
                                    "exportOptions": {
                                        "columns": [1, 2, 3, 4]
                                    },
                                },
                            ]
                        }
                    ]
                }).container().appendTo($('#filterPanel'));
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });

}





// Trigger Save Modal
$(document).on("click",
    "#btnSaveModal",
    function () {
        resetFormValue();
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;<span class='lang-record-add-new-staff'>" + Add_staff + "</span>");
        $("#btnUpdate").hide();
        $("#btnSave").show();

        $('#lblActionText').text('save');
        setTimeout(function () { $('input[name="txtName"]').focus(); }, 1000);

        $('#formModal').modal("show");

        // load store
        var selectedStore = $('#contentBody_lblStoreId').text();
        loadStore(selectedStore);
    });


// Trigger Update Modal
$(document).on("click",
    "#btnUpdateModal",
    function () {
        resetFormValue();
        // load store
        var storeId = $(this).parent().parent().find(".storeId").html();
        loadStore(storeId);

        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;" + Edit_staff + "");
        $("#btnSave").hide();
        $("#btnUpdate").show();

        $('#lblActionText').text('update');
        setTimeout(function () { $('input[name="txtName"]').focus(); }, 1000);

        $('#txtName').val($(this).parent().parent().find(".name").html());
        $('#phone').val($(this).parent().parent().find(".phone").html());
        $('#ddlDepartment').val($(this).parent().parent().find(".department").html()).change();
        //$('#ddlStore').val($(this).parent().parent().find(".storeId").html());
        $('#birthday').val($(this).parent().parent().find(".birthday").html());
        $('#address').val($(this).parent().parent().find(".address").html());

        var id = $(this).parent().parent().find(".id").html();
        var name = $(this).parent().parent().find(".name").html();
        $('#formModal').data('id', id).data('name', name).modal("show");
    });


// Trigger Delete Modal
$(document).on("click",
    "#btnDeleteModal",
    function () {
        var id = $(this).parent().parent().find(".id").html();
        $('#deleteModal').data('id', id).modal("show");
    });


// Trigger Restore Modal
$(document).on("click",
    "#btnRestoreModal",
    function () {
        var id = $(this).parent().parent().find(".id").html();
        $('#restoreModal').data('id', id).modal("show");
    });


// Trigger Active Status 
$('#ddlActiveStatus').change(function () {
    displayDataTable();
});





// Reset form values
function resetFormValue() {
    $('#txtName,#phone,#address').val("");
    $('#birthday').val(moment().format("DD-MMM-YYYY"));

}





// Save Action
$(document).on("click",
    "#btnSave",
    function () {

        console.log("AA 1:", $('#contentBody_StaffOpt_lblStoreId').text());

        if (validateForm() === false)
            return;

        var findjsonData = {
            "select": "name",
            "from": "StaffInfo",
            "where": {
                "name": $('#txtName').val()
            },
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
            dataType: "json",
            data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.d === "success") {
                    showModalOutput("Staff name is exist! Try different name.", "warning");
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
        "from": "StaffInfo",

        "values":
            {
                "staffID": GenRandom.Job(),
                "name": $('#txtName').val(),
                "phone": $('#phone').val(),
                "dob": $('#birthday').val(),
                "address": $('#address').val(),
                "sex": $('#staffSexStatus').val(),
                "entryDate": currentDatetimeGlobal,
                "updateDate": currentDatetimeGlobal,
                "roleId": roleIdGlobal,
                "active": '1',
                "department": $('#ddlDepartment').val(),
                "storeId": $('#ddlStore').val() == null ? $('#contentBody_StaffOpt_lblStoreId').text() : $('#ddlStore').val()
            }

    };

    console.log("jsonData:", jsonData);

    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/saveDataAction",
        dataType: "json",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#formModal").modal("hide");

            showOutput(data.d);

            resetFormValue();

            displayDataTable();

            loadStaffData();

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
}





// Update Action
$(document).on("click",
    "#btnUpdate",
    function () {

        if (validateForm() === false)
            return;

        var findjsonData = {
            "select": "name",
            "from": "StaffInfo",
            "where": {
                "name": $('#txtName').val()
            },
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
            dataType: "json",
            data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if ($('#txtName').val() === $('#formModal').data('name')) {
                    updateAction();
                }
                if (data.d === "success") {
                    showModalOutput("Staff name is exist! Try different name.", "warning");
                }
                else {
                    updateAction();
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





function updateAction() {

    var jsonData = {
        "from": "StaffInfo",
        "set": {
            "name": $('#txtName').val(),
            "phone": $('#phone').val(),
            "department": $('#ddlDepartment').val(),
            "storeId": $('#ddlStore').val(),
            "address": $('#address').val(),
            "dob": $('#birthday').val(),
            "sex": $('#staffSexStatus').val()
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
        success: function (data) {

            $("#formModal").modal("hide");

            showOutput(data.d);

            resetFormValue();

            displayDataTable();
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}





// Delete Action
$(document).on("click",
    "#btnDelete",
    function () {

        var jsonData = {
            "from": "StaffInfo",
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
            success: function (data) {

                $("#deleteModal").modal("hide");

                showOutput(data.d);

                displayDataTable();
            },
            error: function (data) {
                showMessage(data.responseText, "Error");
            },
            failure: function (data) {
                showMessage(data.responseText, "Error");
            }
        });

    });


// Restore Action
$(document).on("click",
    "#btnRestore",
    function () {

        var jsonData = {
            "from": "StaffInfo",
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
            success: function (data) {

                $("#restoreModal").modal("hide");

                showOutput(data.d);

                displayDataTable();
            },
            error: function (data) {
                showMessage(data.responseText, "Error");
            },
            failure: function (data) {
                showMessage(data.responseText, "Error");
            }
        });

    });





// Validate Form
function validateForm() {

    var validate = true;
    var name = $('#txtName').val();
    var phone = $('#phone').val();
    var regPhone = /^(?:\+88|01)?\d{11}$/;

    //if (!regPhone.test(phone)) {
    //    validate = false;
    //    showModalOutput("Invalid number! phone number must contains 11 digits ", "warning");
    //}
    if (name === "") {
        validate = false;
        showModalOutput("Name field required!", "warning");
        //$('.validateName').append();
    }

    return validate;
}


// Store load

function loadStore(selectedStore) {

    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/loadStoreDataAction",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var ddlStore = $('#ddlStore');
            ddlStore.empty();

            $.each(data.d, function () {
                if (selectedStore == "")
                    selectedStore = this['Value'];

                ddlStore.append($("<option></option>").val(this['Value']).html(this['Text']));
            });


            ddlStore.val(selectedStore).change();

            var divStore = $('#divStore');
            var userRight = $('#contentBody_lblUserRight').text();
            console.log("userRight:", userRight, selectedStore);
            if (userRight.trim() != 'Branch') {
                divStore.addClass('disNone');
            }
            else {
                divStore.removeClass('disNone');
            }

        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}




// Trigger Save Modal
$(document).on("click",
    "#btnAddReferedBy",
    function () {
        resetFormValue();
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;Add New Referrel");
        $("#btnUpdate").hide();
        $("#btnSave").show();
        $('#ddlDepartment').val(1);
        $('#divDepartment').addClass('disNone');

        $('#formModal').modal("show");

        // load store
        var selectedStore = $('#contentBody_lblStoreId').text();
        loadStore(selectedStore);
    });



function loadStaffData() {

    $.ajax({
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/loadStaffDataAction",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var ddlStaff = $('#contentBody_ddlReferredBy');
            ddlStaff.empty().append('<option selected="selected" value="0">Select a Referrel</option>');;

            $.each(data.d, function () {
                ddlStaff.append($("<option></option>").val(this['Value']).html(this['Text']));
            });


        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}