<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Analytic.aspx.cs" Inherits="MetaPOS.Admin.AnalyticBundle.View.Analytic" %>

<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Analytic Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <link href="../../../Css/Analytic.css" rel="stylesheet" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/AnalyticBundle/Content/Analytic-responsive.css?v=0.001") %>" />

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblStoreAccessParameters" runat="server" Text=""></asp:Label>
    <script>
        $(document).ready(function () {

            //var graphType = $('.active').find('input').attr("value");
            drawChart('day');

            $("button").click(function () {
                var name = $(this).toggleClass("active").val();
            

                drawChart(name);

                $('contentBody_optionDateSearchType button').addClass('active');
            });



        });

        function optionDateSearchType_onClick() {

            drawChart('day');

        };







        google.load('visualization', '1', { packages: ['corechart'] });
        google.setOnLoadCallback(drawChart);


        function drawChart(dateSearchType) {

            if (dateSearchType == undefined || dateSearchType == Event)
                dateSearchType = 'day';


            var options = {
                title: 'Sale Reports',
                vAxis: { title: 'Cups' },
                hAxis: { title: dateSearchType },
                seriesType: 'bars',
                series: { 5: { type: 'line' } }
            };


            //$("#optionDateSearchType .active input").attr("value");
            //var dateTo = document.getElementById("contentBody_txtTo").value;
            //console.log(dateTo);
            var dateFrom = $('#contentBody_txtFrom').val();
            var dateTo = $('#contentBody_txtTo').val();
            var storeId = $('#contentBody_ddlStoreList').val();

            

            var analyticObj = {
                "dateSearchType": dateSearchType,
                "dateForm": dateFrom,
                "dateTo": dateTo,
                "storeId": storeId
            };


            $.ajax({
                url: "<%= ResolveUrl("~/Admin/AnalyticBundle/View/Analytic.aspx/getSaleData") %>",
                data: "{'analyticObj':'" + JSON.stringify(analyticObj) + "'}",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = google.visualization.arrayToDataTable(response.d);
                    var chart = new google.visualization.ComboChart($("#chart")[0]);
                    chart.draw(data, options);
                },
                failure: function (response) {
                    alert(response);
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
    </script>

    <div class="analytic-reprot">

        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
                <div class="section">
                    <h2 class="sectionBreadcrumb lang-reports-analytic-reports">Analytic Reports</h2>
                    <asp:Label runat="server" ID="Label1"></asp:Label>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="section">
                    <div class="form-inline">
                        <div class="sale-report-search">

                            <div class="form-group">
                                <label class="lang-reports-store">Store</label>
                                <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control" onchange="optionDateSearchType_onClick()"></asp:DropDownList>
                            </div>

                            <div class="form-group disNone">
                                <label>Category</label>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" onclick="optionDateSearchType_onClick()"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label class="lang-reports-from">From</label>
                                <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control datepickerCSS" onchange="optionDateSearchType_onClick()"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label class="lang-reports-to">To</label>
                                <asp:TextBox ID="txtTo" runat="server" CssClass="form-control  datepickerCSS"
                                    onchange="optionDateSearchType_onClick()"></asp:TextBox>
                            </div>


                            <div class="btn-group" data-toggle="buttons-radio" id="optionDateSearchType" runat="server">
                                <button class="btn active" value="day">Day</button>
                                <button class="btn disNone" value="week">Week</button>
                                <button class="btn" value="month">Month</button>
                                <button class="btn" value="year">Year</button>
                            </div>


                        </div>
                    </div>
                    <div id="chart" class="chart-report"></div>
                </div>
            </div>
        </div>
    </div>

    <script>
        activeModule = "report";
    </script>

</asp:Content>
