<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="MetaPOS.Admin.AnalyticBundle.View.Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Summary Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Admin/AnalyticBundle/Content/Summary.css?v=0.004" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/AnalyticBundle/Content/Summary-responsive.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 summaryMargin-top">
            <div class="section">
                <h2 class="sectionBreadcrumb lang-reports-summary">Summary</h2>
                <asp:Label runat="server" ID="lblTest" Text=""></asp:Label>
                <asp:Label ID="lblDisplay" runat="server" Text="" CssClass="disNone"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row headerDesign">
        <div class="col-md-12">
            <div class="section header-search">
                <div class="col-md-9 col-sm-10 col-xs-11 form-inline">
                    <div class="form-group ddTextBox-all">
                        <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click"></asp:DropDownList>
                    </div>
                    <div class="form-group ddTextBox-all">
                        <asp:TextBox ID="txtFrom" CssClass="form-control datepickerCSS" placeholder="From" runat="server" AutoPostBack="true" OnTextChanged="btnSearch_Click"></asp:TextBox>
                    </div>
                    <div class="form-group ddTextBox-all">
                        <asp:TextBox ID="txtTo" CssClass="form-control datepickerCSS" placeholder="To" runat="server" AutoPostBack="true" OnTextChanged="btnSearch_Click"></asp:TextBox>
                    </div>
                    <div class="form-group margin-top-10 ddTextBox-all">
                        <asp:CheckBox ID="chkAnyDate" runat="server" Text="Any Date" AutoPostBack="True" OnCheckedChanged="btnSearch_Click" />
                        <asp:Button ID="btnSearch" CssClass="btn btn-info btn-sm" runat="server" Text="Search" OnClick="btnSearch_Click" Visible="False" />
                    </div>
                </div>
                <div class="col-md-3 col-sm-2 col-xs-1 form-inline pull-right">
                    <button type="button" name="print" class="btn btn-info printable-button" onclick="printDiv('printarea')"><i class="fa fa-print fa-2x"></i></button>
                </div>
            </div>
        </div>
    </div>

    <%-- Print Area --%>
    <div id="printarea" class="printarea">
        <b><asp:Label ID="lblCompanyName" CssClass="hiddenSummary print-summary-company" runat="server" Text=""></asp:Label></b>
        <asp:Label ID="lblCompanyAddress" CssClass="hiddenSummary print-summary-address" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblCompanyPhone" CssClass="hiddenSummary print-summary-phone" runat="server" Text=""></asp:Label>
        <h3 class="hiddenSummary print-summary-title">Business Summary List</h3>


        <%--Start Second Row Box Design--%>
        <div class="headerDesign2">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 ">
                    <h4 class="lang-reports-sales-sammary summary-section-title">Sales Summary <span class="float-right summery-print-date">Print date:
                        <asp:Label ID="lblDateTime" runat="server" Text="Label"></asp:Label></span></h4>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="summary-box-design">
                        <div class="">
                            <span class="box-body">
                                <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                <asp:Label ID="lblInvoiceQty" runat="server" Text=""></asp:Label>
                            </span>


                            <span class="smytext">Invoice Qty (App.)</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-database" aria-hidden="true"></i>
                                <asp:Label ID="lblSalesQtyTotal" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Item Qty (App.)</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblNetAmount" runat="server" Text="" CssClass=""></asp:Label>
                            </span>
                            <span class="smytext">Net Amount (App.)</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblSalesDisc" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Discount Amount (App.)</span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblTotalMiscCost" runat="server" Text="0"></asp:Label>
                            </span>
                            <span class="smytext">Misc. Cost (App.)</span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblGrossAmt" runat="server" Text="0.00"></asp:Label>
                            </span>
                            <span class="smytext">Gross Amount (App.)</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblRecivedAmt" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Received Amount (App.)</span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblSaleReturnAmt" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Return Amount (App.)</span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblDueAmt" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Due Amount (App.)</span>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <%--End Second Row Box Design--%>


        <%--Start Third Row Box Design--%>
        <div class="headerDesign2 summary">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 ">
                    <h4 class="lang-reports-cash-summary summary-section-title">Cash Summary</h4>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblCashinHand" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Cash Payment</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblCashinCheck" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Check Payment</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblCashinCard" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Card Payment</span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblCashinbKash" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Mobile Banking Payment</span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblCashOnDelivery" runat="server" Text=""></asp:Label>
                            </span>
                            <span class="smytext">Cash On Delivery (COD)</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="headerDesign2 ">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 ">
                    <h4 class="lang-reports-accounts-summary summary-section-title">Accounts Summary</h4>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">

                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblGrossProfit" runat="server" Text="0.00"></asp:Label>
                            </span>
                            <span class="smytext">Total Profit (App.) <a href=".accounts-summary" class="fr" data-toggle="collapse"><i class="fa fa-plus"></i></a></span>
                        </div>

                        <div class="mini-stat-info accounts-summary collapse">
                            <ul>
                                <li>
                                    <span class="">
                                        <p class="fl fs-12">Sales Profit (+):</p>
                                        <asp:Label ID="lblSalesProfit" CssClass="fr fs-12" runat="server" Text=""></asp:Label>
                                    </span>
                                </li>
                                <li>
                                    <span>
                                        <p class="fl fs-12">Commission Amount (+):</p>
                                        <asp:Label ID="lblSupplierCommission" CssClass="fr fs-12" runat="server" Text=""></asp:Label>
                                    </span>
                                </li>
                                <li>
                                    <span>
                                        <p class="fl fs-12">Discount Amount (-):</p>
                                        <asp:Label ID="lblDiscountAmt" CssClass="fr fs-12" runat="server" Text=""></asp:Label>
                                    </span>
                                </li>
                                <li>
                                    <span>
                                        <p class="fl fs-12">Return Amount (-):</p>
                                        <asp:Label ID="lblReturnAmt" CssClass="fr fs-12" runat="server" Text=""></asp:Label>
                                    </span>
                                </li>
                                <li>
                                    <span>
                                        <p class="fl fs-12 ">Total Profit </p>
                                        <asp:Label ID="lblGrossProfitCollapseFooter" CssClass="fr fs-12" runat="server" Text="0.00"></asp:Label>

                                    </span>
                                </li>
                            </ul>

                        </div>

                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblTotalExpense" runat="server" Text="0.00"></asp:Label>
                            </span>
                            <span class="smytext">Total Expenses (App.)</span>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-6 disNone">
                    <div class="mini-stat box-design clearfix">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblTotalSalary" CssClass="bg-color-salary" runat="server" Text="0.00"></asp:Label>
                            </span>
                            <span class="smytext">Total Salary </span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="summary-box-design">
                        <div>
                            <span class="box-body">
                                <i class="fa fa-money" aria-hidden="true"></i>
                                <asp:Label ID="lblNetProfit" runat="server" Text="0.00"></asp:Label>
                            </span>
                            <span class="smytext">Net Income (App.)</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--End Third Row Box Design--%>
    </div>

    <div class="categoryQty disNone" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 ">
                <h4>Category-wise Sales</h4>
                <asp:Label runat="server" ID="lblEmptyMessage" CssClass="empty-message"></asp:Label>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12 disNone">
                <asp:DropDownList ID="ddlQtyList" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>

                <div class="mini-stat box-design clearfix ">
                    <div class="mini-stat-info bgQty">
                        <asp:Label ID="lblQty" runat="server" Text="Label"></asp:Label>
                        <span class="smytext">Sales Qty</span>
                    </div>
                </div>

            </div>

            <div class="" id="dynamicCategoryWiseSales" runat="server">
            </div>
        </div>
    </div>


    <script>
        activeModule = "report";
    </script>


</asp:Content>
