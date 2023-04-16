// On page load
function eventChange() {
    activeModule = "promotion";
    displayDataTable();
}


function displayDataTable() {

    var jsonData = {
        "select": "Id,message,phoneRecord,medium,msgCounter,msgCost,sentAt",
        "from": "SmsLogInfo",
        "column": "sentAt",
        "dir": "desc"
    };

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/PromotionBundle/View/SmsLog.aspx/getSmsLogModelList",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var messageData = JSON.parse(data.d);
            //console.log(messageData[0].Id);
            var actionHtml;
            actionHtml = "<a type='button' id = 'btnReSend' class='btn  btn-danger btn-md btnResend'><i>Send again</i></a>";

            

            $('#dataListTable').DataTable().destroy();
            $('#dataListTable').empty();

            $('#dataListTable').DataTable({
                "aaData": JSON.parse(data.d),

                "columnDefs": [
                    {
                        "width": "0%",
                        "className": "id disNone",
                        "targets": [0]
                    },
                    {
                        "width": "20%",
                        "targets": [1]
                    },
                    {
                        "width": "10%",
                        "targets": [2]
                    },
                    {
                        "width": "10%",
                        "targets": [3]
                    },
                    {
                        "width": "10%",
                        "targets": [4]
                    },
                    {
                        "width": "10%",
                        "targets": [5]
                    },
                    {
                        "width": "14%",
                        "targets": [6]
                    },
                    {
                         "width": "16%",
                         "targets": [7]
                     },
                {
                        "orderable": false,
                        "defaultContent": actionHtml,
                        "className": "action",
                        "width": "10%",
                        "targets": [8]
        }

                ],
                "columns": [
                    {
                        "title": "",
                        "data": "Id"
                    },
                    {
                        "title": "SMS",
                        "data": "message"
                    },
                    {
                        "title": "Sent",
                        "data": function (data) {
                            return (data.phoneRecord.length + 1) / 12;
                        }


                    },
                    {
                        "title": "Counter",
                        "data": "msgCounter"
                    },
                    {
                        "title": "Cost",
                        "data": "msgCost"
                    },
                    {
                        "title": "Total",
                        "data": function (data) {
                            return (((data.phoneRecord.length + 1) / 12) * (data.msgCost) * (data.msgCounter)).toFixed(2);
                    }

                    },
                    {
                        "title": "Medium",
                        "data": "medium"
                    },
                    {
                        "title": "Sent at",
                        "data": "sentAt"
                    },
                     {
                         "title": "Action",
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
        },
        error: function (data) {
            showMessage(data.responseText, "Error");
        },
        failure: function (data) {
            showMessage(data.responseText, "Error");
        }
    });

}




$(document).on("click", "#btnReSend", function() {

    var Id = $(this).parent().parent().find(".id").html();
    location.href = '/Admin/PromotionBundle/View/SMS.aspx?id=' + Id + '';


});


