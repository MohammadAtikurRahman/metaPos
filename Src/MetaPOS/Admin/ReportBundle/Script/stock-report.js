
$(function () {

    var url = location.href;
    var baseUrl = url.substring(0, url.indexOf('/', 14)) + "/";

    // get using url
    let searchParams = new URLSearchParams(window.location.search)
    let category = searchParams.get('category')
    let supplier = searchParams.get('supplier')
    let store = searchParams.get('store')

    $.ajax({
        url: baseUrl + "Admin/ReportBundle/View/StockReport.aspx/getStockReportAction",
        data: JSON.stringify({ "category": category, "supplier": supplier, 'store': store }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "[]") {
                let isBoss = confirm("No product found. Are you want to close?");
                if (isBoss)
                    window.top.close();
                return;
            }

            var stockData = JSON.parse(data.d);
            console.log("stockData:", stockData);
            $('#CompanyName').text(stockData[0].branchname);
            $('#CompanyAddress').text(stockData[0].branchaddress);
            var phone = stockData[0].branchphone != "" ? "#" + stockData[0].branchphone + "," : "";
            $('#CompanyPnoneNumber').text(phone + stockData[0].branchmobile);
            $('#StoreName').text(stockData[0].storename);

            var table = "";

            var index = 1, buyPriceFooter = 0, salePriceFooter = 0, wholeSalePriceFooter = 0, buyTotal = 0, buyTotalFooter = 0;
            var qtyTotalFooter = 0, saleTotal = 0, saleTotalFooter = 0, wholeSaleTotal = 0, wholeSaleTotalFooter = 0;

            $.each(stockData, function () {
                console.log("prodId", this['prodId']);

                var qty = this["stockqty"];
                qtyTotalFooter += parseFloat(qty);

                var buyPrice = this["bprice"];
                buyPriceFooter += parseFloat(buyPrice);

                var salePrice = this["sprice"];
                salePriceFooter += parseFloat(salePrice);

                var wholeSalePrice = this["dealerPrice"];
                wholeSalePriceFooter += parseFloat(wholeSalePrice);

                buyTotal = parseFloat(this["bprice"]) * parseFloat(this["stockqty"]);
                buyTotalFooter += buyTotal;

                saleTotal = parseFloat(this["sprice"]) * parseFloat(this["stockqty"]);
                saleTotalFooter += saleTotal;

                wholeSaleTotal = parseFloat(this["dealerPrice"]) * parseFloat(this["stockqty"]);
                wholeSaleTotalFooter += wholeSaleTotal;




                table += "<tr>" +
                "<th>" + index + "</th>" +
                "<td>" + this['prodId'] + "</td>" +
                "<td>" + this['prodName'] + "</td>" +
                "<td>" + qty + "</td>" +
                "<td>" + buyPrice + "</td>" +
                "<td>" + salePrice + "</td>" +
                "<td>" + wholeSalePrice + "</td>" +
                "<td>" + buyTotal + "</td>" +
                "<td>" + saleTotal + "</td>" +
                "<td>" + wholeSaleTotal + "</td>" +
            "</tr>";
                index++;
            });
            $('#tbodyStockReport').append(table);


            var tableFooter = "<tr>" +
					            "<th></th>" +
                                "<th></th>" +
                                "<th>Total</th>" +
                                "<th>" + qtyTotalFooter.toFixed(2) + "</th>" +
                                "<th>" + buyPriceFooter.toFixed(2) + "</th>" +
                                "<th>" + salePriceFooter.toFixed(2) + "</th>" +
                                "<th>" + wholeSalePriceFooter.toFixed(2) + "</th>" +
                                "<th>" + buyTotalFooter.toFixed(2) + "</th>" +
                                "<th>" + saleTotalFooter.toFixed(2) + "</th>" +
                                "<th>" + wholeSaleTotalFooter.toFixed(2) + "</th>" +
				            "</tr>";

            $('#tFooterStockReport').append(tableFooter);
            window.print();


        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });

})