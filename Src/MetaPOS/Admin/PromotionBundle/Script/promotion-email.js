// On page load
function eventChange() {
    activeModule = "promotion";

    displayDataTable();
}


function displayDataTable() {

    var jsonData = {
        "select": "Id,message,msgCounter,msgCost,sentAt,medium",
        "from": "SmsLogInfo",
        "column": "id",
        "dir": "desc"
    };

    $.ajax({
        type: "Post",
        url: baseUrl + "Admin/PromotionBundle/View/EmailLog.aspx/getEmailLogModelList",
        data: "{ 'jsonStrData' : '" + JSON.stringify(jsonData) + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#dataListTable').DataTable().destroy();
            $('#dataListTable').empty();

            $('#dataListTable').DataTable({
                "aaData": JSON.parse(data.d),
                "columnDefs": [
                    {
                        "width": "0%",
                        "className": "Id disNone",
                        "targets": [0]
                    },
                    {
                        
                        "width": "40%",
                        "targets": [1]
                    },
                   
                    {
                        
                        "width": "20%",
                        "targets": [2]
                    },
                    {
                        
                        "width": "20%",
                        "targets": [3]
                    },
                    {
                        
                        "width": "20%",
                        "targets": [4]
                    }
                ],
                "columns": [
                    {
                        "title": "",
                        "data": "Id"
                    },
                    {
                        "title": "Message",
                        "data": "message"
                    },
                    
                    {
                        "title": "Cost",
                        "data": "emailCost"
                    },
                    {
                        "title": "Sent at",
                        "data": "sentAt"
                    },
                    {
                        "title": "Medium",
                        "data": "medium"
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