<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="StockReport.aspx.cs" Inherits="MetaPOS.Admin.ReportBundle.View.StockReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Stock Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/admin/ReportBundle/Content/stock-report.css") %>" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div id="divHeaderPanel" class="profit-title">
                <label class="title"><%=Resources.Language.Title_stockReport %></label>
            </div>
            <asp:Label runat="server" ID="lblTest"></asp:Label>

        </div>
    </div>
    <div class="section section-stock-report">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">

                <div class="col-md-6">
                    <div class="form-group">
                        <label><%=Resources.Language.Lbl_stockReport_category %></label>
                        <asp:DropDownList ID="ddlCatagoryList" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label><%=Resources.Language.Lbl_stockReport_store %></label>
                        <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                </div>

                <div class="col-md-6">

                    <div class="form-group">
                        <label><%=Resources.Language.Lbl_stockReport_supplier %></label>
                        <asp:DropDownList ID="ddlSupplierList" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <input type="button" id="btnSearchStockReport" class="btn btn-info width-handred-persent btn-stock-report" value="<%=Resources.Language.Lbl_stockReport_print %>" />
                    </div>
                </div>



            </div>
        </div>
    </div>

    <script>
        activeModule = "report";

        $('#btnSearchStockReport').click(function () {
            var category = $('#contentBody_ddlCatagoryList').val();
            var supplier = $('#contentBody_ddlSupplierList').val();
            var store = $('#contentBody_ddlStoreList').val();

            window.open('/admin/reportbundle/report/StockReport.html?category=' + category + '&supplier=' + supplier + '&store=' + store + '', '_blank');
        });
    </script>

</asp:Content>
