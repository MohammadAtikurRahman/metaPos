
let db;
let dbReq = indexedDB.open('metaPOSDatabase', 1);

dbReq.onsuccess = function(event) {
        db = event.target.result;
        getAndDisplay(db);
}


function displayCustmer(customers) {

    var i = 0;
    var tableRow = "";
    var Customer = customers;
    if (Customer == null) {
        tableRow += "<tr><td colspan='5' class='text-center'>" + "No Customer Data found" + "</td></tr>";
        $('#offlineSlip').append(tableRow);
        return;
    }
    var length = Customer.length;

    for (i = 0; i < length; i++) {


        tableRow += "<tr>" +
            "<td>" + customers[i].text.cusID + "</td>" +
            "<td>" + customers[i].text.name + "</td>" +
            "<td>" + customers[i].text.phone + "</td>" +
            "<td >" + customers[i].text.address + "</td>" +
            // "<td><button class='btn btn-primary btn-outline-accent' onclick='app.submitToSale(" + i + ")'><i class=\"material-icons\">sync</i> Action </button></td>" +
            "<td class='disNone'> " +
            '<div class="dropdown">' +
            ' <button class="btn btn-primary btn-outline-accent dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" onclick="app.submitToSale(" + i + ")">Action</button>' +
            ' <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
            ' <a class="dropdown-item btnViewCusotmer" href="javascript:void(0)">View</a>' +
            '<a class="dropdown-item" href="#">Edit</a>' +
            ' <a class="dropdown-item" href="#">Stock Status</a>' +
            ' <a class="dropdown-item" href="#">Print Barcode</a>' +
            ' <a class="dropdown-item" href="#">Print Custom</a>' +
            ' <a class="dropdown-item" href="#">Delete</a>' +
            '</div>' +
            '</div>' +
            "</td>" +
            "</tr>";
    }


    $('#offlineSlip').append(tableRow);

    var rowCount = $('#tblSlip tr').length;
    $('.lblTotalCartQty').attr("totalqty", rowCount);
    $(".lblTotalCartQty").html(rowCount);


}



function getAndDisplay(db) {
    let tx = db.transaction(['metaposCustomers'], 'readonly');
    let store = tx.objectStore('metaposCustomers');

    let req = store.openCursor();
    let allCustomers = [];

    req.onsuccess = function(event) {
        let
        cursor = event.target.result;
        if (cursor != null) {
            allCustomers.push(cursor.value);
            cursor.continue();
        }
        else {
            displayCustmer(allCustomers);
        }
    };
    req.onerror = function(event) {
        alert('error in cursor request ' + event.target.errorCode);
    };
}

