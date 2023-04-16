// On page load
function eventChange() {

    activeModule = "record";

    checkPagePermission("Card");

    displayDataTableUsingBank();

    loadBankList();

    $('#cardName,#cardDiscount').keypress(function (event) {
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

    $('#ddlBank').keypress(function (event) {
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


var storeBankData = "";
function displayDataTableUsingBank() {

    var jsonDataBank = {
        "select": "Id,bankName",
        "from": "BankNameInfo",
        "where": {
            "active": $("#ddlActiveStatus").val()
        },
        "column": "id",
        "dir": "desc"
    };

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonDataBank) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            storeBankData = JSON.parse(data.d);


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





// Display datatable list
function displayDataTable() {

    var jsonData = {
        "select": "Id,bankId,cardName,cardDisc",
        "from": "CardTypeInfo",

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
                "order": [[2, "asc"]],
                "columnDefs": [
                    {
                        "width": "5%",
                        "className": "id",
                        "targets": [0]
                    },
                    {
                        "className": "bankName",
                        "width": "20%",
                        "targets": [1]
                    },
                    {
                        "className": "cardName",
                        "width": "30%",
                        "targets": [2]
                    },
                    {
                        "className": "cardDisc",
                        "width": "20%",
                        "targets": [3]
                    },
                    {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action ",
                        "width": "30%",
                        "targets": [4]
                    },
                    {
                        "className": "bankId disNone",
                        "width": "0%",
                        "targets": [5]
                    }
                ],
                "columns": [
                    {
                        "title": "",
                        "data": "Id"
                    },
                    {
                        "title": bank_name,
                        "data": function (bankData) {

                            var objBank = storeBankData.find(function (obj) { return obj.Id == bankData.bankId; });

                            if (JSON.stringify(objBank) === undefined || null) {
                                return 'N/A';
                            }
                            return objBank.bankName;
                        }
                    },
                    {
                        "title": card_name,
                        "data": "cardName"
                    },
                    {
                        "title": card_discount,
                        "data": "cardDisc"
                    },
                    {
                        "title": action,
                        "data": null
                    },
                    {
                        "title": "BankId",
                        "data": "bankId"
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
                               "columns": [0,1, 2, 3]
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

                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Card List</b></p>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; padding-bottom: 5; margin: 0">' + companyPhone + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 15; margin: 0; margin-bottom: 5">' + companyAddress + '</h3>');
                               $(win.document.body).prepend('<h3 style="text-align: center; font-size: 25; margin-top: 5">' + companyName + '</h3>');

                           }
                       },
                       {
                           "extend": 'collection',

                           "text": '',
                           "className": 'glyphicon glyphicon-export datatable-button',
                           "buttons": [
                               {
                                   "extend": 'pdf',
                                   "exportOptions": {
                                       "columns": [1, 2, 3]
                                   }
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1, 2, 3]
                                   }
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1, 2, 3]
                                   }
                               }
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
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;<span class='lang-record-add-card'>" + add_card + "</span>");
        $("#btnUpdate").hide();
        $("#btnSave").show();
        $('#formModal').modal("show");

        $('#lblActionText').text('save');
        setTimeout(function () { $('input[name="cardName"]').focus(); }, 1000);
    });





// Load bank list
function loadBankList() {
    var jsonData = {
        "select": "Id,bankName",
        "from": "BankNameInfo",
        "where": {
            "active": true
        },
        "column": "bankName",
        "dir": "asc"
    };

    // Reset bank list
    $("#ddlBank").empty();//.append("<option selected='selected' value='0'>" + Select_a_bank + "</option>");
    $("#ddlBank").append("<option selected='selected' value='0'>" + Select_a_bank + "</option>");

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
           
            var dataList = $.parseJSON(data.d);

            $.each(dataList,
                function (index, item) {
                    $("#ddlBank").append("<option value='" + item.Id + "'>" + item.bankName + "</option>");
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





// Trigger Update Modal
$(document).on("click",
    "#btnUpdateModal",
    function () {
        resetFormValue();

        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;" + Edit_card + "");
        $("#btnSave").hide();
        $("#btnUpdate").show();

        $('#lblActionText').text('update');
        setTimeout(function () { $('input[name="cardName"]').focus(); }, 1000);

        $('#ddlBank').val($(this).parent().parent().find(".bankId").html());
        $('#cardName').val($(this).parent().parent().find(".cardName").html());
        $('#cardDiscount').val($(this).parent().parent().find(".cardDisc").html());

        var id = $(this).parent().parent().find(".id").html();
        var cardName = $(this).parent().parent().find(".cardName").html();
        $('#formModal').data('id', id).data('cardName', cardName).modal("show");
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
    $('#ddlBank,#cardName,#cardDiscount').val("");
}





// Save Action
$(document).on("click",
    "#btnSave",
    function () {

        if (validateForm() === false)
            return;

        var findjsonData = {
            "select": "cardName",
            "from": "CardTypeInfo",
            "where": {

                "cardName": $('#cardName').val(),
                "and": '',
                "bankId": $('#ddlBank').val()
            }
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
            dataType: "json",
            data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.d === "success") {
                    showModalOutput("Card name is exist with same bank! Try different card name.", "warning");
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
        "from": "CardTypeInfo",
        "values":
            {
                "cardName": $('#cardName').val(),
                "bankId": $('#ddlBank').val(),
                "cardDisc": $('#cardDiscount').val(),
                "entryDate": currentDatetimeGlobal,
                "updateDate": currentDatetimeGlobal,
                "roleId": roleIdGlobal,
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

        if (validateForm() === false)
            return;

        var findjsonData = {
            "select": "cardName,bankId",
            "from": "CardNameInfo",
            "where": {
                "cardName": $('#cardName').val(),
                "and": '',
                "bankId": $('#ddlBank').val()
            }
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
            dataType: "json",
            data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.d === "success") {
                    showModalOutput("Card name is exist! Try different name.", "warning");
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
        "from": "CardTypeInfo",
        "set": {
            "cardName": $('#cardName').val(),
            "cardDisc": $('#cardDiscount').val()
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
            "from": "CardTypeInfo",
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
            "from": "CardTypeInfo",
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

    var cardName = $('#cardName').val();
    var bankName = $('#ddlBank').val();
    var cardDisc = $('#cardDiscount').val();

    if (bankName === '0' || bankName === null) {
        validate = false;
        showModalOutput("Please select a bank name", "warning");
    }
    if (cardDisc === '' || null) {
        validate = false;
        showModalOutput("Please enter a card discount!", "warning");
    }
    if (cardName === "") {
        validate = false;
        showModalOutput("Please enter a card name!", "warning");
        //$('.validateName').append();

    }

    return validate;
}
