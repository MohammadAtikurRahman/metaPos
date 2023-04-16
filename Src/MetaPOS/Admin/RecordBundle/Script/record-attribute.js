
var storeAttributeData = "";

function eventChange() {
    activeModule = "record";

    checkPagePermission("Attribute");

    displayDataTableUisngFieldData();

    loadFieldList();

    $('#attributeName').keypress(function (event) {
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

    $('#ddlField').change(function (event) {
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


function displayDataTableUisngFieldData() {

    var jsonDataField = {
        "select": "Id,field",
        "from": "FieldInfo",
        "where": {
            "active": $("#ddlActiveStatus").val()
        },
        "column": "id",
        "dir": "desc"
    };


    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/RecordBundle/View/Record.aspx/getRecordInfoListAction",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonDataField) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            storeAttributeData = JSON.parse(data.d);

            displayDataTableAttribute();
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




function displayDataTableAttribute() {

    var jsonData = {
        "select": "Id,fieldId,attributeName",
        "from": "AttributeInfo",

        "where": {
            "active": $("#ddlActiveStatus").val()
        },
        "column": "id",
        "dir": "desc"
    };



    $.ajax({
        "type": "Post",
        "url": baseUrl + "Admin/AppBundle/View/Operation.aspx/getDataListAction",
        "data": "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        "contentType": "application/json; charset=utf-8",
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

            // operation mode off for sub branch
            var dynamicCss = "";
            if (branchType == "sub")
                dynamicCss = "disNone";

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
                        "className": "fieldName",
                        "width": "15%",
                        "targets": [1]
                    },
                    {
                        "className": "attributeName",
                        "width": "20%",
                        "targets": [2]
                    },
                    {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action " + dynamicCss,
                        "width": "30%",
                        "targets": [3]
                    },
                    {
                        "className": "fieldId disNone",
                        "width": "0%",
                        "targets": [4]
                    }
                ],
                "columns": [
                    {
                        "title": ID,
                        "data": "Id"
                    },
                    {
                        "title": Field_Name,
                        //"data": "fieldId"
                        "data": function (attrData) {
                            var objAttr = storeAttributeData.find(function (obj) { return (obj.Id === attrData.fieldId); });
                            if (JSON.stringify(objAttr) === undefined || null) {
                                return 'N/A';
                            }
                            return objAttr.field;
                        }
                    },
                    {
                        "title": Attribute_Name,
                        "data": "attributeName"
                    },

                    {
                        "title": Action,
                        "data": null
                    },
                    {
                        "title": "fieldId",
                        "data": "fieldId"
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
                               "columns": [0,1, 2]
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

                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Attribute List</b></p>');
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
                                       "columns": [1, 2]
                                   }
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1, 2]
                                   }
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1, 2]
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
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;<span class='lang-record-add-attribute'>" + Add_attribute + "</span>");
        $("#btnUpdate").hide();
        $("#btnSave").show();
        $('#formModal').modal("show");

        $('#lblActionText').text('save');
        setTimeout(function () { $('input[name="attributeName"]').focus(); }, 1000);

        loadFieldList();
    });





// Load Field list
function loadFieldList() {
    var jsonData = {
        "select": "Id,field",
        "from": "FieldInfo",
        "where": {
            "active": true
        },
        "column": "field",
        "dir": "asc"
    };

    // Reset field list
    $("#ddlField").empty();
    $("#ddlField").append("<option value='0'>" + Select_a_Field + "</option>");

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
                    $("#ddlField").append("<option value='" + item.Id + "'>" + item.field + "</option>");
                });
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





// Trigger Update Modal
$(document).on("click",
    "#btnUpdateModal",
    function () {

        resetFormValue();
        $("#formModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;" + Edit_attribute + "");
        $("#btnSave").hide();
        $("#btnUpdate").show();

        $('#lblActionText').text('update');
        setTimeout(function () { $('input[name="attributeName"]').focus(); }, 1000);

        $('#ddlField').val($(this).parent().parent().find(".fieldId").html());
        $('#attributeName').val($(this).parent().parent().find(".attributeName").html());


        var id = $(this).parent().parent().find(".id").html();
        var attributeName = $(this).parent().parent().find(".attributeName").html();
        $('#formModal').data('id', id).data('attributeName', attributeName).modal("show");
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
    displayDataTableUisngFieldData();
});





// Reset form values
function resetFormValue() {
    $('#ddlField,#attributeName').val("");
}





// Save Action
$(document).on("click",
    "#btnSave",
    function () {

        if (validateForm() === false)
            return;

        var findjsonData = {
            "select": "attributeName",
            "from": "AttributeInfo",
            "where": {
                "attributeName": $('#attributeName').val()

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
                    showModalOutput("Attribute name is exist! Try different name.", "warning");
                }
                else {
                    saveActionAttribute();
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





function saveActionAttribute() {

    var jsonData = {
        "from": "AttributeInfo",
        "values":
            {
                "fieldId": $('#ddlField').val(),
                "attributeName": $('#attributeName').val(),
                "active": '1',
                "entryDate": currentDatetimeGlobal,
                "updateDate": currentDatetimeGlobal,
                "roleId": roleIdGlobal

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
            displayDataTableUisngFieldData();
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
            "select": "attributeName",
            "from": "AttributeInfo",
            "where": {
                "attributeName": $('#attributeName').val()
            }
        };

        $.ajax({
            url: baseUrl + "Admin/AppBundle/View/Operation.aspx/findDataAction",
            dataType: "json",
            data: "{ 'jsonStrData': '" + JSON.stringify(findjsonData) + "' }",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if ($('#attributeName').val() === $('#formModal').data('attributeName')) {
                    updateAction();
                }
                else if (data.d === "success") {
                    showModalOutput("Attribute name is exist! Try different name.", "warning");
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
        "from": "AttributeInfo",
        "set": {
            "attributeName": $('#attributeName').val(),
            "fieldId": $('#ddlField').val(),
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

            displayDataTableUisngFieldData();
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
            "from": "AttributeInfo",
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

                displayDataTableUisngFieldData();
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
            "from": "AttributeInfo",
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

                displayDataTableUisngFieldData();
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

    var attributeName = $('#attributeName').val();
    var fieldName = $('#ddlField').val();

    if (fieldName === '0') {
        validate = false;
        showModalOutput("Please select a field", "warning");
    }

    if (attributeName === "") {
        validate = false;
        showModalOutput("Please enter an attribute name!", "warning");
        //$('.validateName').append();

    }

    return validate;
}
