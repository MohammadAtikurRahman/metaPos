


function eventChange() {

    displayDataTableService();


    $('#txtServiceName,#txtWholePrice,#txtRetailPrice').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $("#btnSave").trigger("click");
            event.preventDefault();
        }
    });

}



function displayDataTableService() {

    var active = $('#ddlActiveStatus').val();
    if (active == undefined)
        active = 0;
    $.ajax({
        "type": "Post",
        "url": baseUrl + "Admin/SaleBundle/View/Service.aspx/getServiceDataListAction",
        "data": "{'active':'" + active + "'}",
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
                        "className": "serviceName",
                        "width": "20%",
                        "targets": [1]
                    },
                    {
                        "className": "serviceType",
                        "width": "20%",
                        "targets": [2]
                    },
                    {
                        "className": "wholePrice",
                        "width": "20%",
                        "targets": [3]
                    },
                    {
                        "className": "retailPrice",
                        "width": "20%",
                        "targets": [4]
                    },
                    {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action " + dynamicCss,
                        "width": "20%",
                        "targets": [5]
                    },
                    {
                        "className": "type disNone",
                        "width": "0%",
                        "targets": [6]
                    }
                ],
                "columns": [
                    {
                        "title": ID,
                        "data": "id"
                    },
                    {
                        "title": Service_Name,
                        //"data": "fieldId"
                        "data": "name"
                    },
                    {
                        "title": Service_Type,
                        "data": "typeName"
                    },
                    {
                        "title": Wholesale_Price,
                        "data": "wholePrice"
                    },
                    {
                        "title": Retail_Price,
                        "data": "retailPrice"
                    },

                    {
                        "title": Action,
                        "data": null
                    },
                    {
                        "title": "type",
                        "data": "type"
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

                               $(win.document.body).prepend('<p style="border-bottom: 1px solid #ccc; padding-bottom: 10px; padding-top: 3px;"><b>Service List</b></p>');
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
                                       "columns": [1, 2, 3, 4]
                                   }
                               },
                               {
                                   "extend": 'excel',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 4]
                                   }
                               },
                               {
                                   "extend": 'csv',
                                   "exportOptions": {
                                       "columns": [1, 2, 3, 4]
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
        resetServiceFormValue();
        $("#addServiceFormModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;<span>" + AddService + "</span>");
        $("#btnUpdate").hide();
        $("#btnSave").show();
        $('#addServiceFormModal').modal("show");

        loadServiceType();
    });

// Trigger Update Modal
$(document).on("click",
    "#btnUpdateModal",
    function () {
        // resetFormValue();
        $("#addServiceFormModal .modal-title").html("<i class='fa fa-file-text' aria-hidden='true'></i>&nbsp;" + eiditService + "");
        $("#btnSave").hide();
        $("#btnUpdate").show();

        $('#ddlServiceType').val($(this).parent().parent().find(".type").html());
        $('#txtServiceName').val($(this).parent().parent().find(".serviceName").html());
        $('#txtRetailPrice').val($(this).parent().parent().find(".retailPrice").html());
        $('#txtWholePrice').val($(this).parent().parent().find(".wholePrice").html());


        var id = $(this).parent().parent().find(".id").html();
        $('#lblId').text(id);
        var serviceName = $(this).parent().parent().find(".serviceName").html();
        $('#addServiceFormModal').data('id', id).data('serviceName', serviceName).modal("show");
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
    displayDataTableService();
});


function resetFormValue() {

}



// Delete Action
$(document).on("click",
    "#btnDelete",
    function () {

        var jsonData = {
            "from": "ServiceInfo",
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

                displayDataTableService();
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
            "from": "ServiceInfo",
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

                displayDataTableService();
            },
            error: function (data) {
                showMessage(data.responseText, "Error");
            },
            failure: function (data) {
                showMessage(data.responseText, "Error");
            }
        });

    });




function resetServiceFormValue() {
    $('#ddlServiceType').val("0");
    $('#txtServiceName').val("");
    $('#txtRetailPrice').val("");
    $('#txtWholePrice').val("");
}
