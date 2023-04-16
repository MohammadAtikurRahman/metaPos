<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="SupplierCommission.aspx.cs" Inherits="MetaPOS.Admin.ReportBundle.View.SupplierCommission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Supplier Commission
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="<%= ResolveUrl("~/Admin/ReportBundle/Content/supplier-commission.css") %>" />
    <link type="text/css" rel="stylesheet" href="<%= ResolveUrl("~/Admin/ReportBundle/Content/supplier-commission-responsive.css?v=0.001") %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    
    <div class="row">
        
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div id="divHeaderPanel">
                <asp:Label runat="server" ID="lblCompanyName" CssClass="company-name disNone"></asp:Label>
                <label class="title lang-reports-supplier-commission">Supplier Commission</label>

            </div>
        </div>
        
        <div class="col-md-12">
   
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
                <table id="dataListTable" class="table table-striped table-bordered">
                    <thead>
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

    <script type="text/javascript" src='<%= ResolveUrl("~/Admin/ReportBundle/Script/supplier-commission-report.js?v=0.0035") %>'></script>
    <script>
        activeModule = "report";
    </script>

</asp:Content>
