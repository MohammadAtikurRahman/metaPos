﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Return.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.Return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Return Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Return.css") %>" rel="stylesheet" />
     <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Return-responsive.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Css/bootstrap-tagsinput.css") %>" rel="stylesheet" />
    <script src="<%= ResolveUrl("~/Js/bootstrap-tagsinput.min.js") %>"></script>

    <script>
        $(function () {
            $("input").val();
        });
    </script>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">


    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 marginTop-10">
            <div class="section">
                <h2 class="sectionBreadcrumb"><%=Resources.Language.Title_Return_page %></h2>
                <asp:Label runat="server" ID="lblTest"></asp:Label>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalStockReturn" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title">
                        <span class="glyphicon glyphicon-user glyIconPosition"></span>
                        <asp:Label ID="lblModalTitle" runat="server" Text="Label"></asp:Label>
                    </h3>
                </div>
                <div class="modal-body">
                    <asp:DetailsView ID="dtlStockStatus" runat="server"
                        CssClass="mDtl dtlSize" AllowPaging="True" AutoGenerateRows="False" 
                        DataKeyNames="Id">
                        <Fields>
                            <asp:TemplateField HeaderText="Id" InsertVisible="False" SortExpression="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_product %>" SortExpression="prodName">
                                <ItemTemplate>
                                    <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("prodName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_code %>" SortExpression="prodCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblProdCode" runat="server" Text='<%# Bind("prodCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_supplier %>" SortExpression="supCompany">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupCompany" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_imei %>" SortExpression="catName">
                                <ItemTemplate>
                                    <asp:Label ID="lblCatName" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_category %>" SortExpression="imei">
                                <ItemTemplate>
                                    <asp:Label ID="lblImei" runat="server" Text='<%# Bind("imei") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_qty %>" SortExpression="qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("qty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" <%$Resources:Language, Lbl_return_price_buy %>" SortExpression="bPrice">
                                <ItemTemplate>
                                    <asp:Label ID="lblBPrice" runat="server" Text='<%# Bind("bPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_price_sell %>" SortExpression="sPrice">
                                <ItemTemplate>
                                    <asp:Label ID="lblSPrice" runat="server" Text='<%# Bind("sPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_Size %>" SortExpression="size">
                                <ItemTemplate>
                                    <asp:Label ID="lblSize" runat="server" Text='<%# Bind("size") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_returned_date %>" SortExpression="size">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusDate" runat="server" Text='<%# Bind("statusDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:DetailsView>
                </div>

                <div style="clear: both"></div>
                <div class="modal-footer" runat="server" id="modalFooter">
                    <button type="submit" class="btn btn-lg btnCustomize btnCloseCustom" data-dismiss="modal"><%=Resources.Language.Btn_return_close %></button>
                </div>
            </div>
        </div>
    </div>

    <div class="row headerDesign">
        <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
            <div class="section">
                <h2 class="sectionHeading"><%=Resources.Language.Lbl_return_back_to_return %></h2>
                <asp:Panel ID="pnlCode" runat="server" DefaultButton="btnCheck">
                    <div class="form-group">
                        <asp:Label ID="lblCode" CssClass="lbl col-sm-3 col-xs-3 control-label" runat="server" Text="<%$Resources:Language, Lbl_return_product_code %>"></asp:Label>
                        <div class="col-sm-9 col-xs-9">
                            <asp:TextBox ID="txtCode" CssClass="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Panel runat="server" ID="pnlStore">
                            <asp:Label ID="Label3" CssClass="lbl col-sm-3 col-xs-3 control-label" runat="server" Text="<%$Resources:Language, Lbl_return_store %>"></asp:Label>
                            <div class="col-sm-9 col-xs-9">
                                <asp:DropDownList runat="server" ID="ddlStore" CssClass="form-control" />
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnCheck" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnSearchCustom " runat="server" Text="<%$Resources:Language, Btn_return_check %>" OnClick="btnCheck_Click" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlQty" runat="server" DefaultButton="btnBackToReturn">
                    <div class="form-group">
                        <asp:Label ID="lblQty" CssClass="lbl col-sm-3 col-xs-3 control-label lang-inventory-qty" runat="server" Text="<%$Resources:Language, Lbl_return_qty %>"></asp:Label>
                        <div class="col-sm-9 col-xs-9">
                            <asp:TextBox ID="txtQty" CssClass="form-control float-number-validate" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqQty" ValidationGroup="reqEditDtlStock" runat="server" ControlToValidate="txtQty" ErrorMessage="Required!" Display="Dynamic" CssClass="crStyle"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revQty" runat="server" ControlToValidate="txtQty" ErrorMessage="Invalid!" ValidationExpression="(^\d{0,20}([.]\d{0,20})?$)" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div id="divPiece" runat="server" visible="false">
                        <div class="form-group">
                            <asp:Label ID="Label2" CssClass="lbl col-sm-3 col-xs-3 control-label" runat="server" Text="Piece"></asp:Label>
                            <div class="col-sm-9 col-xs-9">
                                <asp:TextBox ID="txtPiece" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Panel runat="server" ID="pnlReturnImei" Visible="false">
                            <asp:Label ID="Label1" CssClass="lbl col-sm-3 col-xs-3 control-label lang-inventory-imei" runat="server" Text="IMEI"></asp:Label>
                            <div class="col-sm-9 col-xs-9">
                                <div class="input-group">
                                    <input runat="server" type="text" data-role="tagsinput" value="" id="txtReturnImei" class="form-control" />
                                </div>
                                <%--<asp:TextBox runat="server" ID="txtReturnImei" CssClass="form-control"></asp:TextBox>--%>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="form-group padding-bottom-10">
                        <asp:Button ID="btnBackToReturn" CssClass="btn btn-info btn-default  CRBtnDesign blockDesign btnLimpCustom btnAddOpt" ValidationGroup="reqEditDtlStock" runat="server" Text="<%$Resources:Language, Btn_return_back_to_return %>" OnClick="btnBackToReturn_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
            <div class="section">
                <h2 class="sectionHeading"><%=Resources.Language.Lbl_return_search_by_date %></h2>
                <asp:Panel ID="pnlDate" runat="server" DefaultButton="btnSearch">
                    <div class="form-group">
                        <asp:Label ID="lblSup" CssClass="lbl col-sm-3 col-xs-3 control-label" runat="server" Text="<%$Resources:Language, Btn_return_supplier%>"></asp:Label>
                        <div class="col-sm-9 col-xs-9">
                            <asp:DropDownList ID="ddlSup" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCat" CssClass="lbl col-sm-3 col-xs-3 control-label" runat="server" Text="<%$Resources:Language, Lbl_return_category%>"></asp:Label>
                        <div class="col-sm-9 col-xs-9">
                            <asp:DropDownList ID="ddlCat" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblFrom" CssClass="lbl col-sm-3 col-xs-3 control-label" runat="server" Text="<%$Resources:Language, Lbl_return_from %>"></asp:Label>
                        <div class="col-sm-9 col-xs-9">
                            <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control datepickerCSS" AutoPostBack="true" OnTextChanged="btnSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblTo" CssClass="lbl col-sm-3 col-xs-3 control-label" runat="server" Text="<%$Resources:Language, Lbl_return_to %>"></asp:Label>
                        <div class="col-sm-9 col-xs-9">
                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control datepickerCSS" AutoPostBack="true" OnTextChanged="btnSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group padding-bottom-25" ValidationGroup="validGroup">
                        <asp:Button ID="btnSearch" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnSearchCustom" runat="server" Text="<%$Resources:Language, Btn_return_search %>" OnClick="btnSearch_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <%--<div class="row gridTopButton">
        <div class="btn-group btnGroupDesign" role="group">
            <asp:Button ID="btnPrint" CssClass="btn btn-primary btnPrintCustom" runat="server" Text="Print" OnClick="btnPrint_Click" />
        </div>
    </div>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-xs-4 gridTitle text-left">
                    <label class="lang-inventory-stock-report"><%=Resources.Language.Lbl_return_stock_report %></label>
                </div>
                <div class="col-xs-8 gridTitle text-right form-inline">
                    <div class="form-group">
                        <asp:LinkButton runat="server" ID="btnPrint" ValidationGroup="validGroup" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:GridView ID="grdStockStatus" OnRowDataBound="grdStockStatus_RowDataBound"
                OnSelectedIndexChanged="grdStockStatus_SelectedIndexChanged" OnRowEditing="grdStockStatus_RowEditing"
                ShowFooter="true" AllowPaging="true" PageSize="10" runat="server" OnPageIndexChanging="grdStockStatus_OnPageIndexChanging"
                CssClass="mGrid gBox scrollBar" AutoGenerateColumns="False" DataKeyNames="Id"
                EmptyDataText="There are no data records to display." ViewStateMode="Enabled">
                <Columns>
                    <asp:TemplateField HeaderText="Id" SortExpression="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_sl %>">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_prod_id %>" SortExpression="prodID" Visible="false">
                        <EditItemTemplate>
                            <asp:Label ID="lblProdID" runat="server" Text='<%# Bind("prodID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProdID" runat="server" Text='<%# Bind("prodID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_product %>" SortExpression="prodName">
                        <ItemTemplate>
                            <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("prodName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_code %>" SortExpression="prodCode">
                        <ItemTemplate>
                            <asp:Label ID="lblProdCode" runat="server" Text='<%# Bind("prodCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Imei" SortExpression="imei" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblImei" runat="server" Text='<%# Bind("imei") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supplier" SortExpression="supCompany" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblSupCompany" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" SortExpression="catName" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblCatName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCatName" runat="server" Text="Total : "></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_qty %>" SortExpression="qty">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("qty") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblQtyFooter" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_price_buy %>" SortExpression="bPrice">
                        <ItemTemplate>
                            <asp:Label ID="lblBPrice" runat="server" Text='<%# Bind("bPrice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_stock_total %>" SortExpression="stockTotal">
                        <ItemTemplate>
                            <asp:Label ID="lblStockTotal" runat="server" Text='<%# Bind("stockTotal") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblStockTotal" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_status_date %>" SortExpression="statusDate" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusDate" runat="server" Text='<%# Bind("statusDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Store" SortExpression="storeId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblStoreId" runat="server" Text='<%# Bind("storeId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_view %>" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdView" runat="server" CssClass="btn btn-design " CausesValidation="False" CommandName="Select" Text="<span class='glyphicon glyphicon-adjust'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_restore %>" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdRetrieve" runat="server" OnClientClick=" return confirm('Do you want to Restore the selected record ?') " CssClass="btn btn-design btnDeleteOpt" CausesValidation="False" CommandName="Edit" Text="<span class='glyphicon glyphicon-retweet'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
        </div>
    </div>

    <script>
        activeModule = "inventory";


        prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            var script = document.createElement('script');
            script.src = '../../../../Js/bootstrap-tagsinput.min.js';
            script.type = 'text/javascript';
            var head = document.getElementsByTagName("head")[0];
            head.appendChild(script);
        });


    </script>

</asp:Content>