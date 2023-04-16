<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="InventoryReport.aspx.cs" Inherits="MetaPOS.Admin.AnalyticBundle.View.InventoryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Inventory Report    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/ReportBundle/Content/inventory-report.css?v=0.001")%>" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/ReportBundle/Content/inventory-report-responsive.css?v=0.004")%>" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div id="divHeaderPanel">
                <label class="title"><%=Resources.Language.Title_inventoryReport %></label>
            </div>
        </div>

        <div class="col-md-12">
            <div class="gridHeader desplay-block">
                <div class="col-md-12 col-sm-12 col-xs-12 form-control">
                    <asp:RadioButtonList ID="rblSearchByType" name="rblSearchByType" runat="server" CssClass="search-type"></asp:RadioButtonList>
                </div>
            </div>
            <div class="gridHeader desplay-block">
                <div class="col-md-12 col-sm-12 col-xs-12 gridTitle form-inline" id="filterPanel">
                    <div class="form-group margin-bottom-10 float-left">
                        <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left">
                        <asp:DropDownList runat="server" ID="ddlUserList" CssClass="form-control" />
                    </div>
                    <div class="form-group margin-bottom-10 float-left">
                        <select name="ddlCategoryWiseReport" id="ddlCategoryWiseReport" class="form-control">
                        </select>
                    </div>
                    <div class="form-group margin-bottom-10 float-left">
                        <select name="ddlStatusFilter" id="ddlStatusFilter" class="form-control">
                            <option value=""><%=Resources.Language.Lbl_inventoryReport_select_status %></option>
                            <option value="stock"><%=Resources.Language.Lbl_inventoryReport_stocked %></option>
                            <option value="stockTransfer"><%=Resources.Language.Lbl_inventoryReport_transferred %></option>
                            <option value="stockReceive"><%=Resources.Language.Lbl_inventoryReport_received %></option>
                            <option value="StockReturn"><%=Resources.Language.Lbl_inventoryReport_returned %></option>
                            <option value="Damage"><%=Resources.Language.Lbl_inventoryReport_damaged %></option>
                            <option value="sale"><%=Resources.Language.Lbl_inventoryReport_sold %></option>
                            <option value="saleReturn"><%=Resources.Language.Lbl_inventoryReport_sold_back %></option>

                        </select>
                    </div>
                    <div class="form-group margin-bottom-10 float-left">
                        <input type="text" class="datepickerCSS form-control" id="txtDateFrom" />
                    </div>
                    <div class="form-group margin-bottom-10 float-left">
                        <input type="text" class="datepickerCSS form-control" id="txtDateTo" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">

        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="card" id="divListPanel">
                <table id="dataListTable" class="table table-striped table-bordered" cellspacing="0" width="100%" style="overflow-x: scroll; display: block;">
                    <thead class="scrollBar">
                        <tr>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>
    
    
    <asp:HiddenField ID="lblHiddenCompanyName" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyAddress" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyPhone" runat="server"/>


    <script type="text/javascript" src='<%= ResolveUrl("~/Admin/ReportBundle/Script/inventory-report.js?v=0.0033") %>'></script>


    <script>
        activeModule = "report";

        var ID = "<% =Resources.Language.Lbl_inventoryReport_id %>";
        var ItemName = "<% =Resources.Language.Lbl_inventoryReport_item_name %>";
        var Product_code = "<% =Resources.Language.Lbl_inventoryReport_product_code %>";
        var Details = "<% =Resources.Language.Lbl_inventoryReport_details %>";
        var Attributes = "<% =Resources.Language.Lbl_inventoryReport_attribute %>";
        var Qty = "<% =Resources.Language.Lbl_inventoryReport_qty %>";
        var _LastStock = "<% =Resources.Language.Lbl_inventoryReport_last_stock %>";
        var _BalanceStock = "<% =Resources.Language.Lbl_inventoryReport_balance_stock %>";
        var _Date = "<% =Resources.Language.Lbl_inventoryReport_date %>";
        var _Status = "<% =Resources.Language.Lbl_inventoryReport_status %>";
        var _Store = "<% =Resources.Language.Lbl_inventoryReport_store %>";
        var _SelectCategory = "<% =Resources.Language.Lbl_inventoryReport_select_category %>";
        var _BuyPrice = "<% =Resources.Language.Lbl_inventoryReport_buyPrice %>";
        var _InventoryAmount = "<% =Resources.Language.Lbl_inventoryReport_inventoryAmount %>";


    </script>
</asp:Content>
