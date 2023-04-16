<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MetaPOS.Admin.AnalyticBundle.View.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Dashboard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <link href="../../../Css/Analytic.css" rel="stylesheet" />
    <link href="../../../Css/Dashboard.css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/AnalyticBundle/Content/Dashboard-responsive.css?v=0.001") %>" rel="stylesheet" />
    <style>
        h3 {
            margin-top: 10px !important;
            margin-bottom: 0 !important;
        }
    </style>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <asp:Label ID="lblTest2" runat="server" Text=""></asp:Label>
    <script>

        // Sales Report in Area Chart
        google.load('visualization', '1', { packages: ['corechart'] });
        google.setOnLoadCallback(drawChart);


        function drawChart() {
            var options = {
                //title: 'Sales Report',
                hAxis: { title: 'Sales Graph Chart', titleTextStyle: { color: '#333' } },
                vAxis: { minValue: 0 },
                legend: 'none',
            };

            $.ajax({
                url: "<%= ResolveUrl("~/Admin/AnalyticBundle/View/Dashboard.aspx/getSaleData") %>",
                data: '',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.length > 1) {
                        var data = google.visualization.arrayToDataTable(response.d);
                        //var chart = new google.visualization.AreaChart($("#chart")[0]);
                        var chart = new google.visualization.AreaChart($("#chart1")[0]);
                        chart.draw(data, options);
                    }
                },
                failure: function(response) {
                    console.log(response);
                },
                error: function(response) {
                    console.log(response);
                }
            });
        }
        $(window).on("throttledresize", function (event) {
            var options = {
                width: '100%',
                height: '100%'
            };

            var data = google.visualization.arrayToDataTable([]);
            drawChart(data, options);
        });


    </script>

    <script>
        // Top Product Report in Pie Chart 
        google.load('visualization', '1', { packages: ['corechart'] });
        google.setOnLoadCallback(drawChart);


        function drawChart() {
            var options = {
                //title: '',
                hAxis: { title: 'Top Products Graph Chart', titleTextStyle: { color: '#333' } },
                vAxis: { minValue: 0 },
                legend: 'none',
                is3D:true,
            };

            $.ajax({
                url: "<%= ResolveUrl("~/Admin/AnalyticBundle/View/Dashboard.aspx/getTopCategory") %>",
                data: '',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    var data = google.visualization.arrayToDataTable(response.d);
                    //var chart = new google.visualization.PieChart($("#pieChart")[0]);
                    var chart = new google.visualization.PieChart($("#chart_div")[0]);
                    chart.draw(data, options);
                },
                failure: function(response) {
                    console.log(response);
                },
                error: function(response) {
                    console.log(response);
                }
            });
        }

        $(window).on("throttledresize", function (event) {
            var options = {
                width: '100%',
                height: '100%'
            };

            var data = google.visualization.arrayToDataTable([]);
            drawChart(data, options);
        });

    </script>

    <div class="analytic-reprot dashboard">

        <!-- Summary -->
        <div class="row topCardMargin">
            <div class="col-md-3 col-sm-6 col-xs-6">
                <div class="card">
                    <div class="content-summary">
                        <div class="row">
                            <div class="col-xs-2">
                                <div class="icon-big icon-warning text-center">
                                    <%--<span class="taka icon-sale">৳</span>--%>
                                    <i class="fa fa-money  fa-4x icon-sale"></i>
                                </div>
                            </div>
                            <div class="col-xs-10">
                                <div class="numbers">
                                    <p class="sale-amount"> <%=Resources.Language.today_sale %> </p>
                                    <span class="taka-icon">৳ </span>
                                    <asp:Label ID="lblTotalSaleAmt" runat="server" Text="0.00"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="footer-summary">
                            <hr>
                            <div class="stats">
                                <i class="fa fa-refresh update-status">  <%=Resources.Language.updated %>  </i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-3 col-sm-6 col-xs-6">
                <div class="card">
                    <div class="content-summary">
                        <div class="row">
                            <div class="col-xs-2">
                                <div class="icon-big icon-warning text-center">
                                    <%--<span class="taka icon-paid">৳</span>--%>
                                    <i class="fa fa-file-text-o  fa-4x icon-paid"></i>
                                </div>
                            </div>
                            <div class="col-xs-10">
                                <div class="numbers">
                                    <p class="new-invice"><%=Resources.Language.new_invoie %></p>
                                    <asp:Label ID="lblNewInvoice" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="footer-summary">
                            <hr>
                            <div class="stats">
                                <i class="fa fa-refresh update-status"> <%=Resources.Language.updated %></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-3 col-sm-6 col-xs-6">
                <div class="card">
                    <div class="content-summary">
                        <div class="row">
                            <div class="col-xs-2">
                                <div class="icon-big icon-warning text-center">
                                    <i class="fa fa-database fa-4x icon-qty"></i>
                                </div>
                            </div>
                            <div class="col-xs-10">
                                <div class="numbers">
                                    <p class="sale-item"><%=Resources.Language.sale_qty %></p>
                                    <span></span>
                                    <asp:Label ID="lblProdQty" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="footer-summary">
                            <hr>
                            <div class="stats">
                                <i class="fa fa-refresh update-status"> <%=Resources.Language.updated %></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-3 col-sm-6 col-xs-6">
                <div class="card">
                    <div class="content-summary">
                        <div class="row">
                            <div class="col-xs-2">
                                <div class="icon-big icon-warning text-center">
                                    <i class="fa fa-users  fa-4x icon-customer"></i>
                                </div>
                            </div>
                            <div class="col-xs-10">
                                <div class="numbers">
                                    <p class="new-customer"><%=Resources.Language.new_customer %></p>
                                    <span></span>
                                    <asp:Label ID="lblNewCustomer" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="footer-summary">
                            <hr>
                            <div class="stats ">
                                <i class="fa fa-refresh update-status"> <%=Resources.Language.updated %></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <!-- Sales and Paid Chart -->
            <div class="col-md-6 col-sm-12 col-xs-12">
                <div class="section">
                    <h3 class="title"><%=Resources.Language.Lbl_last %> <asp:Label ID="lblMonthCount" runat="server" Text="0" CssClass="lblMonthCount">0 </asp:Label> <%=Resources.Language.month_sales %></h3>
                   <%-- <div id="chart" class="chart-report"></div>--%>
                    <div id="chart">
                        <div id="chart1">

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12">
                <div class="section">
                    <h3 class="title last-month-popular-product"><%=Resources.Language.this_month_popular %> <span runat="server" id="lblProductDate"></span></h3>
                    <%--<div id="pieChart" class="chart-report" style="width: 100%; height: 100%;"></div>--%>
                    <div id="chart_wrap">
                        <div id="chart_div">

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script>
        activeModule = "default";
    </script>

</asp:Content>