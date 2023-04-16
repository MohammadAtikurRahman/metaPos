<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="PurchaseReport.aspx.cs" Inherits="MetaPOS.Admin.AnalyticBundle.View.PurchaseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Purchase Report

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href='<%= ResolveUrl("../Content/purchase-report.css") %>' />
    <link href="<%= ResolveUrl("~/Admin/ReportBundle/Content/Purchese-report-responsive.css?v=0.002") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div id="divHeaderPanel">
                <label class="title lang-reports-purchase-report">Purchase Report</label>
            </div>
        </div>
    </div>


    <asp:Label runat="server" ID="lblTest"></asp:Label>

    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="gridHeader">
             
                <div class="col-md-12 col-sm-12 col-xs-12 gridTitle form-inline">
                    
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control" AutoPostBack="True"  OnSelectedIndexChanged="ddlStoreList_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList runat="server" ID="ddlUserList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserList_OnSelectedIndexChanged"/>
                    </div>
                    <div class="form-group hidden float-left margin-left-7">
                        <asp:Label runat="server" ID="lblTotalDueBalance" CssClass="control-label text-capitalize text-primary msg-report"></asp:Label>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="ddlSupplierName" CssClass="form-control" runat="server" OnSelectedIndexChanged="btnGrdSearch_OnClick" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:TextBox ID="txtSearchDateFrom" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnGrdSearch_OnClick"></asp:TextBox>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:TextBox ID="txtSearchDateTo" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnGrdSearch_OnClick"></asp:TextBox>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:TextBox runat="server" ID="txtSearch" placeholder="Search..." CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSearch_OnTextChanged"></asp:TextBox>
                    </div>
                    <div class="form-group margin-bottom-10 disNone float-left margin-left-7">
                        <asp:LinkButton runat="server" ID="btnPrint" CssClass="print" OnClick="btnPrint_OnClickick"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                    </div>
                    <asp:Button ID="btnGrdSearch" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnSearchCustom disNone" runat="server" Text="Search" OnClick="btnGrdSearch_OnClick" />
                </div>

            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:GridView ID="grdPurchaseReportInfo"
                ShowFooter="True"
                    OnRowDataBound="grdPurchaseReportInfo_OnRowDataBound"
                    OnSelectedIndexChanged="grdPurchaseReportInfo_OnSelectedIndexChanged"
                    OnPageIndexChanging="grdPurchaseReportInfo_OnPageIndexChanging"
                    OnRowCommand="grdPurchaseReportInfo_OnRowCommand"
                    runat="server"
                    CssClass="mGrid gBox scrollBar"
                    AutoGenerateColumns="False"
                    DataKeyNames="purchaseCode"
                    EmptyDataText="There are no data records to display."
                    AllowSorting="true"
                    ViewStateMode="Enabled"
                    AllowPaging="true">
                <Columns>
                    <asp:TemplateField HeaderText="Code" SortExpression="purchaseCode">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("purchaseCode") %>' ID="TextBox2"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("purchaseCode") %>' ID="lblPurchaseCode"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="supCompany" HeaderText="Supplier" SortExpression="supCompany"></asp:BoundField>
                    <asp:BoundField DataField="qty" HeaderText="Qty" SortExpression="qty"></asp:BoundField>
                    <asp:BoundField DataField="stockTotal" HeaderText="Purchase Amount" SortExpression="stockTotal"></asp:BoundField>
                    <asp:TemplateField HeaderText="Date" SortExpression="statusDate">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("statusDate", "{0:dd-MMM-yyyy}") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("statusDate", "{0:dd-MMM-yyyy}") %>' ID="LabelStatusDate"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Store" SortExpression="storeName">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("storeName") %>' ID="LabelStore"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdPruchaseReport" runat="server" CommandArgument='<%#  ((GridViewRow) Container).RowIndex%>' CssClass="btn btn-design" CausesValidation="False" CommandName="PurchaseReport" Text="<span class='glyphicon glyphicon-print'></span>" title="Print "></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
        </div>
    </div>

    <script>
        activeModule = "report";
    </script>


</asp:Content>
