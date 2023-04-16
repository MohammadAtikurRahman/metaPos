// On page load
function eventChange() {

    activeModule = "record";

    checkPagePermission("Bank");

    displayDataTable();

    $('#txtName').keypress(function (event) {
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





// Display datatable list
function displayDataTable() {

    var jsonData = {
        "select": "Id,bankName",
        "from": "BankNameInfo",
        "where": {
            "active": $("#ddlActiveStatus").val(),
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
                        "className": "bankName",
                        "width": "70%",
                        "targets": [1]
                    },
                    {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action ",
                        "width": "30%",
                        "targets": [2]
                    }
                ],
                "columns": [
                    {
                        "title": ID,
                        "data": "Id"
                    },
                    {
                        "title": Bank_name,
                        "data": "bankName"
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
                "searching": true,
            });

            var buttons = new $.fn.dataTable.Buttons(table,
               {
                   "buttons": [
                       {
                           "extend": 'print',
                           "exportOptions": {
                               "columns": [0,1]
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


                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Bank List</b></p>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; padding-bottom: 5; margin: 0">' + companyPhone + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; margin: 0; margin-bottom: 5">' + companyAddress + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 25; margin-top: 5">' + companyName + '</h3>');


                           }
                       },
                       {
                           "extend": 'collection',
                           "exportOptions": {
                               "columns": [1]
                           },
                           "text": '',
                           "className": 'glyphicon glyphicon-export datatable-button',
                           "buttons": [
                               {
                                   "extend": 'pdf',
                                   "exportOptions": {
                                       "columns": [1]
                                   },
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1]
                                   },
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1]
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
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;<span>" + Add_bank + "</span>");
        $("#btnUpdate").hide();
        $("#btnSave").show();

        $('#lblActionText').text('save');
        setTimeout(function () { $('input[name="txtName"]').focus(); }, 1000);

        $('#formModal').modal("show");
    });


// Trigger Update Modal
$(document).on("click",
    "#btnUpdateModal",
    function () {
        resetFormValue();
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;" + Edit_bank_name + "");
        $("#btnSave").hide();
        $("#btnUpdate").show();

        $('#lblActionText').text('update');
        setTimeout(function () { $('input[name="txtName"]').focus(); }, 1000);

        $('#txtName').val($(this).parent().parent().find(".bankName").html());

        var id = $(this).parent().parent().find(".id").html();
        var bankName = $(this).parent().parent().find(".bankName").html();
        $('#formModal').data('id', id).data('bankName', bankName).modal("show");
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
    $('#txtName').val("");
}





// Save Action
$(document).on("click",
    "#btnSave",
    function () {

        if (validateForm() === false)
            return;

        var findjsonData = {
            "select": "bankName",
            "from": "BankNameInfo",
            "where": {
                "bankName": $('#txtName').val(),
                "and": '',
                "roleID": roleIdGlobal
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
                    showModalOutput("Bank name is exist! Try different name.", "warning");
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
        "from": "BankNameInfo",
        "values": {
            "bankName": $('#txtName').val(),
            "descr":'',
            "entryDate": currentDatetimeGlobal,
            "updateDate": currentDatetimeGlobal,
            "roleID": roleIdGlobal,
            "active": '1'
        }
    };

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
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });
}





// Update Action
$(document).on("click",
    "#btnUpdate",
    function () {

        if (validateForm() == false)
            return;

        var findjsonData = {
            "select": "bankName",
            "from": "BankNameInfo",
            "where": {
                "bankName": $("#txtName").val()
            }
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
            dataType: "json",
            data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if ($("#txtName").val() === $('#formModal').data('bankName')) {
                    updateAction();
                }
                else if (data.d === "success") {
                    showModalOutput("Bank name is exist! Try different name.", "warning");
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
        "from": "BankNameInfo",
        "set": {
            "bankName": $('#txtName').val(),
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





// Delete Action
$(document).on("click",
    "#btnDelete",
    function () {

        var jsonData = {
            "from": "BankNameInfo",
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
            "from": "BankNameInfo",
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

    var bankName = $('#txtName').val();

    if (bankName === "") {
        validate = false;
        showModalOutput("Please enter bank name!", "warning");
        //$('.validateName').append();
    }

    return validate;
}