<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="StockBulkOpt.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.StockBulkOpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Product
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/StockOpt.css?v=0.003") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/wwwroot/css/designer.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Css/bootstrap-tagsinput.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Css/select2.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/StockBulkOpt-responsive.css") %>" rel="stylesheet" type="text/css" />
    <script src='<%= ResolveUrl("~/Js/bootstrap-tagsinput.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Admin/InventoryBundle/Script/stock-loader.js") %>' type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Admin/InventoryBundle/Script/variant-search.js?v=0.015") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/InventoryBundle/Script/stock-variant.js?v=0.015") %>"></script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server" onkeydown="return (event.keyCode!=13)">

    <div class="stock-operation">
        <div class="row">
            <div class="purchaseMarginTop col-md-12 col-sm-12 col-xs-12">
                <div class="section">
                    <h2 class="sectionBreadcrumb">
                        <asp:Label ID="lblStockTitle" CssClass="lang-purchase" runat="server" Text="<%$Resources:Language, Title_Purchase_page  %>"></asp:Label>
                    </h2>
                    <asp:Label runat="server" ID="lblTest" Text="" BackColor="red"></asp:Label>
                </div>
            </div>
        </div>

        <input type="hidden" id="txtImgDb" runat="server" />
        <input type="hidden" id="txtImgFileName" runat="server" />

        <div class="row">
            <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleStockField section">
                    <h2 class="sectionHeading lang-add-update-product"><%=Resources.Language.Lbl_purchase_add_update_product %></h2>
                    <asp:Label runat="server" ID="lblTestMessage"></asp:Label>
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="form-group">
                            <asp:Panel ID="pnlScan" runat="server" DefaultButton="btnLoadProductDetails">
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearchNameCode" CssClass="form-control btnEditOpt" runat="server" aria-describedby="btnLoadProductDetails" placeholder="<%$Resources:Language, Lbl_invoice_search_product_by  %>"></asp:TextBox>
                                        <asp:HiddenField ID="hfProductDetails" runat="server" />
                                        <asp:Button ID="Button1" Text="Submit" runat="server" OnClick="Button1_Click" Visible="false" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="btnLoadProductDetails" runat="server" CssClass="btn btn-default inp-group" OnClick="btnLoadProductDetails_Click" ImageUrl="~/Img/search.png" />
                                        </span>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <asp:Label runat="server" ID="lblProdID" Visible="false"></asp:Label>

                        <div class="form-group" id="divWarehouse" runat="server">
                            <asp:Label ID="Label13" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-store" runat="server" Text="<%$Resources:Language, Lbl_purchase_store  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList runat="server" ID="ddlStoreList" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStoreList_OnSelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" id="divLocation" runat="server" visible="false">
                            <asp:Label ID="Label24" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-location" runat="server" Text="<%$Resources:Language, Lbl_purchase_location  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList runat="server" ID="ddlLocationList" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="divManufacturer">
                            <asp:Label ID="Label10" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-manufacturer-brand" runat="server" Text="<%$Resources:Language, Lbl_purchase_manufacturer_brand  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList ID="ddlManufacturer" CssClass=" form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblSup" CssClass="lbl col-xs-4 col-sm-4 control-stock-label" runat="server" Text="<%$Resources:Language, Lbl_purchase_supplier_vendor  %> "></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList ID="ddlSup" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblCat" CssClass="lbl col-xs-4 col-sm-4 control-stock-label" runat="server" Text="<%$Resources:Language, Lbl_purchase_category  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList ID="ddlCat" CssClass=" form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <%--this text control from aspx.cs --%>
                        <div class="form-group">
                            <asp:Label ID="Label1" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-product-name" runat="server" Text=""></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtProduct" CssClass="form-control" runat="server"></asp:TextBox>
                                <div id="divVariantControlForUpdate" runat="server" visible="false">
                                    <asp:Label ID="lblVaiantUpdateLabel" CssClass="control-stock-label" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divBarcodeGenerator">
                            <asp:Label ID="lblScanCode" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-product-code" runat="server" Text="<%$Resources:Language, Lbl_purchase_product_code  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtScanCode" CssClass="form-control" runat="server" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:ImageButton runat="server" ID="btnGenerateBarCode" CssClass="btn btn-default inp-group" OnClick="btnGenerateBarCode_Click" ImageUrl="~/Img/generate.png" />
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div id="divUnit" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblUnit" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-unit" runat="server" Text="<%$Resources:Language, Lbl_purchase_unit  %>"></asp:Label>
                                <div class="col-xs-8 col-sm-8">
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-add-exist-qty" Text="<%$Resources:Language, Lbl_purchase_add_qty_exists  %> "></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtQty" CssClass="form-control float-number-validate" runat="server" OnTextChanged="unitTotalPurchaseAmt_click" AutoPostBack="True"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revQty" runat="server" ControlToValidate="txtQty" ErrorMessage="Invalid!" ValidationExpression="^\d+(\.\d{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>

                                    <span class="input-group-addon ">+&nbsp;<asp:Label runat="server" ID="lblCurrentQty" CssClass="inp-group" Text="0" /></span>
                                </div>
                            </div>
                        </div>

                        <div id="divFreeQty" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label251" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-free-qty" runat="server" Text="<%$Resources:Language, Lbl_purchase_free_qty  %>"></asp:Label>
                                <div class="col-xs-8 col-sm-8">
                                    <asp:TextBox ID="txtFreeQty" CssClass="form-control" runat="server" Enabled="false" Text="0"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divWaringinQty">
                            <asp:Label ID="lblAlertQty" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-warning-qty" runat="server" Text="<%$Resources:Language, Lbl_purchase_warning_qty  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="">
                                    <asp:TextBox ID="txtWarningQty" CssClass="form-control float-number-validate" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="reWarningQty" runat="server" ControlToValidate="txtWarningQty" ErrorMessage="Invalid!" ValidationExpression="^([-+]?[0-9]{0,6})$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>


                        <div class="form-group" id="divCostPrice" runat="server">
                            <asp:Label ID="lblPriceBuy" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-set-cost-price" runat="server" Text="<%$Resources:Language, Lbl_purchase_buy_price  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBPrice" CssClass="form-control float-number-validate" runat="server" Text="" aria-describedby="btnBPriceEdit" OnTextChanged="unitTotalPurchaseAmt_click"></asp:TextBox> <%-- AutoPostBack="True"--%>
                                    <asp:Label runat="server" ID="lblBPrice" CssClass="disNone" Text="0" />
                                    <asp:RegularExpressionValidator ID="revBPrice" runat="server" ControlToValidate="txtBPrice" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{1,10}(?:\.[0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                    <span class="input-group-btn">
                                        <asp:ImageButton runat="server" ID="btnBPriceEdit" CssClass="btn btn-default inp-group" OnClick="btnBPriceEdit_Click" ImageUrl="~/Img/edit.png" /></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" id="divCompanyPrice" runat="server">
                            <asp:Label ID="Label22" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-set-company-price" runat="server" Text="<%$Resources:Language, Lbl_purchase_company_price  %> "></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtCompanyPrice" CssClass="form-control float-number-validate" runat="server" Text="" aria-describedby="btnBPriceEdit"></asp:TextBox>
                                    <asp:Label runat="server" ID="Label23" CssClass="disNone" Text="0" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCompanyPrice" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{1,10}(?:\.[0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                    <span class="input-group-btn">
                                        <asp:ImageButton runat="server" ID="ImageButton1" CssClass="btn btn-default inp-group" OnClick="btnCompanyPrice_Click" ImageUrl="~/Img/edit.png" /></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divDealerPrice" visible="false">
                            <asp:Label ID="lblDPrice" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-wholesale-price" runat="server" Text="<%$Resources:Language, Lbl_purchase_wholesale_price  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtDealerPrice" CssClass="form-control float-number-validate" runat="server" Text=""></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:DropDownList ID="ddlSetBuyPrice" runat="server" CssClass="form-control margin-right-40" AutoPostBack="true" OnSelectedIndexChanged="ddlSetBuyPrice_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblSPrice" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-sale-price" runat="server" Text="<%$Resources:Language, Lbl_purchase_retail_price  %> "></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSPrice" CssClass="form-control float-number-validate" runat="server" Text="" aria-describedby="btnSPriceEdt"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revSPrice" runat="server" ControlToValidate="txtSPrice" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{1,10}(?:\.[0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                    <span class="input-group-btn">
                                        <asp:ImageButton runat="server" ID="btnSPriceEdt" CssClass="btn btn-default inp-group" OnClick="btnSPriceEdt_Click" ImageUrl="~/Img/edit.png" /></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divUnitPurchaseAmt" visible="False">
                            <asp:Label ID="Label14" runat="server" CssClass="lbl col-xs-4 col-md-4 control-stock-label lang-total-purchase-amount" Text="<%$Resources:Language, Lbl_purchase_total_purchase_amount  %>"></asp:Label>
                            <div class="col-xs-8 col-md-8 bulk-stock">
                                <div class="input-group">
                                    <asp:TextBox ID="txtUnitTotalPurchaseAmt" runat="server" Enabled="false" CssClass="lang-total-stock-amount float-number-validate form-control" placeholder="Total Stock Amount"></asp:TextBox>
                                    <span class="input-group-btn"></span>
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="btnUnitEditPurAmt" CssClass="btn" runat="server" OnClick="btnUnitEditPurAmt_OnClick"><span class="glyphicon glyphicon-pencil" aria-hidden="true" ></span></asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divSize">
                            <asp:Label ID="Label11" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-size" runat="server" Text="<%$Resources:Language, Lbl_purchase_warranty%>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtSize" CssClass="form-control float-number-validate" runat="server" Text=""></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" id="divWarranty" runat="server">
                            <asp:Label ID="Label2" CssClass="lbl col-xs-4 col-sm-4 control-stock-label padding-top-10 lang-warranty" runat="server" Text="<%$Resources:Language, Lbl_purchase_warranty%>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="form-inline">
                                    <div class="input-group col-xs-4 padding-top-10" style="float:left;">
                                        <asp:DropDownList ID="ddlWarrantyYear" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_purchase_year%>"></asp:ListItem>
                                            <asp:ListItem Value="1">1 Y</asp:ListItem>
                                            <asp:ListItem Value="2">2 Y</asp:ListItem>
                                            <asp:ListItem Value="3">3 Y</asp:ListItem>
                                            <asp:ListItem Value="4">4 Y</asp:ListItem>
                                            <asp:ListItem Value="5">5 Y</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="input-group col-xs-4 padding-top-10" style="float:left;">
                                        <asp:DropDownList ID="ddlWarrantyMonth" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_purchase_month%>"></asp:ListItem>
                                            <asp:ListItem Value="1">1 M</asp:ListItem>
                                            <asp:ListItem Value="2">2 M</asp:ListItem>
                                            <asp:ListItem Value="3">3 M</asp:ListItem>
                                            <asp:ListItem Value="4">4 M</asp:ListItem>
                                            <asp:ListItem Value="5">5 M</asp:ListItem>
                                            <asp:ListItem Value="6">6 M</asp:ListItem>
                                            <asp:ListItem Value="7">7 M</asp:ListItem>
                                            <asp:ListItem Value="8">8 M</asp:ListItem>
                                            <asp:ListItem Value="9">9 M</asp:ListItem>
                                            <asp:ListItem Value="10">10 M</asp:ListItem>
                                            <asp:ListItem Value="11">11 M</asp:ListItem>
                                            <asp:ListItem Value="12">12 M</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="input-group col-xs-4 padding-top-10">
                                        <asp:DropDownList ID="ddlWarrantyDays" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_purchase_day%>"></asp:ListItem>
                                            <asp:ListItem Value="1">1 D</asp:ListItem>
                                            <asp:ListItem Value="2">2 D</asp:ListItem>
                                            <asp:ListItem Value="3">3 D</asp:ListItem>
                                            <asp:ListItem Value="4">4 D</asp:ListItem>
                                            <asp:ListItem Value="5">5 D</asp:ListItem>
                                            <asp:ListItem Value="6">6 D</asp:ListItem>
                                            <asp:ListItem Value="7">7 D</asp:ListItem>
                                            <asp:ListItem Value="8">8 D</asp:ListItem>
                                            <asp:ListItem Value="9">9 D</asp:ListItem>
                                            <asp:ListItem Value="10">10 D</asp:ListItem>
                                            <asp:ListItem Value="11">11 D</asp:ListItem>
                                            <asp:ListItem Value="12">12 D</asp:ListItem>
                                            <asp:ListItem Value="13">13 D</asp:ListItem>
                                            <asp:ListItem Value="14">14 D</asp:ListItem>
                                            <asp:ListItem Value="15">15 D</asp:ListItem>
                                            <asp:ListItem Value="16">16 D</asp:ListItem>
                                            <asp:ListItem Value="17">17 D</asp:ListItem>
                                            <asp:ListItem Value="18">18 D</asp:ListItem>
                                            <asp:ListItem Value="19">19 D</asp:ListItem>
                                            <asp:ListItem Value="20">20 D</asp:ListItem>
                                            <asp:ListItem Value="21">21 D</asp:ListItem>
                                            <asp:ListItem Value="22">22 D</asp:ListItem>
                                            <asp:ListItem Value="23">23 D</asp:ListItem>
                                            <asp:ListItem Value="24">24 D</asp:ListItem>
                                            <asp:ListItem Value="25">25 D</asp:ListItem>
                                            <asp:ListItem Value="26">26 D</asp:ListItem>
                                            <asp:ListItem Value="27">27 D</asp:ListItem>
                                            <asp:ListItem Value="28">28 D</asp:ListItem>
                                            <asp:ListItem Value="29">29 D</asp:ListItem>
                                            <asp:ListItem Value="30">30 D</asp:ListItem>
                                            <asp:ListItem Value="30">31 D</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" id="divImei" runat="server">
                            <asp:Label ID="lblImei" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-imei" Text="<%$Resources:Language, Lbl_purchase_IMEI  %>" runat="server"></asp:Label>
                            <div class="col-xs-8 col-md-8">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnImeiChecker">
                                    <div class="input-group">
                                        <input runat="server" type="text" data-role="tagsinput" value="" id="txtIMEI" onkeydown="return (event.keyCode!=13)" />
                                        <span class="input-group-btn ">
                                            <asp:CheckBox runat="server" ID="chkIMEIEnable" CssClass="chkImeiDesign" />
                                        </span>
                                    </div>
                                    
                                </asp:Panel>
                                <asp:Button ID="btnImeiChecker" CssClass="disNone" runat="server" Text="Button"  OnClick="btnImeiChecker_OnClick"/>
                            </div>
                        </div>

                        <div class="form-group" id="divSku" runat="server">
                            <asp:Label ID="lblSku" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-sku" runat="server" Text="<%$Resources:Language, Lbl_purchase_SKU  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtSku" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divEngineNumber" runat="server">
                            <asp:Label ID="Label20" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-engine-number" runat="server" Text="<%$Resources:Language, Lbl_purchase_engine_number  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtEngineNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divCecishNumber" runat="server">
                            <asp:Label ID="Label21" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-cecish-number" runat="server" Text="<%$Resources:Language, Lbl_purchase_cecish_number  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtCecishNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divTax" runat="server">
                            <asp:Label ID="lblTax" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-tax" runat="server" Text="<%$Resources:Language, Lbl_purchase_tax  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtTax" CssClass="form-control float-number-validate" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revTax" runat="server" ControlToValidate="txtTax" ErrorMessage="Invalid!" ValidationExpression="^([0-9]{0,3})$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="form-group" id="divRecivedDate" runat="server">
                            <asp:Label ID="Label5" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-received-date" runat="server" Text="<%$Resources:Language, Lbl_purchase_recived_date  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtRecivedDate" CssClass="form-control datepickerCSS " runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divExpireDate" runat="server">
                            <asp:Label ID="Label6" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-expiry-date" runat="server" Text="<%$Resources:Language, Lbl_purchase_Expiry_date  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtExpiryDate" CssClass="form-control datepickerCSS " runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divBatchNo" runat="server">
                            <asp:Label ID="Label7" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-batch-no" runat="server" Text="<%$Resources:Language, Lbl_purchase_Batch_no  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtBatchNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group" id="divSerialNo" runat="server">
                            <asp:Label ID="Label8" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-serial-no" runat="server" Text="<%$Resources:Language, Lbl_purchase_serial_no  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtSerialNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group" id="divSupplierCom" runat="server">
                            <asp:Label ID="lbl34" CssClass="lbl col-xs-4 col-sm-4 control-stock-label float-number-validate lang-supplier-payment" runat="server" Text="<%$Resources:Language, Lbl_purchase_supplier_payment  %>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                
                            </div>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                   <asp:TextBox ID="txtSuplierCommission" runat="server" CssClass="form-control float-number-validate"></asp:TextBox>
                                    <span class="input-group-addon ">%</span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" id="divShipmentStatus" runat="server">
                            <asp:Label ID="Label9" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-shipment-status" runat="server" Text="<%$Resources:Language, Lbl_purchase_shipment_status  %>"></asp:Label>
                            <div class="col-xs-4 col-sm-8">
                                <asp:DropDownList ID="ddlShipmentStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group" id="div1" runat="server">
                            <asp:Label ID="Label12" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-notes " runat="server" Text="<%$Resources:Language, Lbl_purchase_notes%>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox runat="server" ID="txtNotes" CssClass="form-control" placeholder="<%$Resources:Language, Lbl_purchase_notes%>" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group " id="divUploader" runat="server">
                            <asp:Label ID="lblFileUpload" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-upload-file" runat="server" Text="<%$Resources:Language, Lbl_purchase_Upload_File  %>"></asp:Label>

                            <div class="col-xs-8 col-sm-6">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileuploader" onchange="readURL(this)" OnClientClick=" ImageUpload(this) " />
                                <%--<asp:FileUpload ID="FileUpload1" onchange="if (confirm('Upload ' + this.value + '?')) this.form.submit();" runat="server" Width="200"/>--%>
                                <input type="button" id="btnUpload" value="Upload" class="disNone" />
                            </div>
                            <div class="col-sm-2">
                                <img id="Image1" class="pull-right img-responsive" runat="server" />
                            </div>
                        </div>


                        <div class="form-group" id="divVariantControl" runat="server">
                            <asp:Label ID="Label26" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-type" Text="<%$Resources:Language, Lbl_purchase_product_type  %>" runat="server"></asp:Label>
                            <div class="col-xs-8 col-md-8">
                                <div class="">
                                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="<%$Resources:Language, Lbl_purchase_single_product  %>" Value="0" />
                                        <asp:ListItem Text="<%$Resources:Language, Lbl_purchase_varient_product  %>" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hiddenAttributeJosnValue" runat="server" />
                        <asp:HiddenField ID="hiddenFieldJosnValue" runat="server" />


                        <div class="form-group disNone" id="divVarient" runat="server">
                            <div class="col-xs-12 col-md-12">

                                <div class="varient-header">
                                    <div class="form-group col-xs-6 col-md-6">
                                        <label class="lang-inventory-field te">Field</label>
                                    </div>
                                    <div class="form-group col-xs-6 col-md-6">
                                        <label class="lang-inventory-attribute">Attribute</label>
                                    </div>
                                </div>


                                <div id="varientArea">
                                </div>

                                <div class="form-group btn-add-new-variant">
                                    <input type="button" class="btn btn-default " value="Add New" id="btnAddNewVarient" />
                                </div>
                            </div>
                        </div>

                        <asp:Label ID="lblSelectedFieldRecord" runat="server" Text=" " Visible="false"></asp:Label>

                        <div id="divFieldList" class="form-control col-xs-6 disNone" runat="server">
                            <!-- Dynamic content -->

                        </div>

                        <div id="divAttributeList" class="form-control col-xs-6" runat="server">
                            <!-- Dynamic content -->
                        </div>


                        <div class="form-group">

                            <asp:Button ID="btnAddToPurchaseCart" ValidationGroup="reqAddStock" CssClass="btn btn-info btn-sm CRBtnDesign btnResize e67e22 btnAddOpt" runat="server" Text="<%$Resources:Language, Btn_purchase_add  %>" OnClick="AddToPurchaseCart" />
                            <asp:Button ID="btnReset" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnReset" runat="server" Text="<%$Resources:Language, Btn_purchase_clear%>" OnClick="btnReset_Click" />

                        </div>

                    </asp:Panel>

                </div>
            </div>

            <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal" runat="server" id="divImport">
                <div class="section">
                <div class="row import">
                    <div class="col-md-8">
                        <div class="form-inline data-import-box">
                            <h2 class="sectionHeading lang-import-products"><%=Resources.Language.Lbl_purchase_import_new_products %></h2>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="upload-section">
                                        <div class="form-group select-branch">
                                        </div>
                                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="btn btn-default btn-file btn-custom-uploader" />
                                        <%--<asp:Button ID="btnImportFile" ValidationGroup="import" runat="server" Text="<%$Resources:Language, Lbl_purchase_import_file  %>" CssClass="btn btn-info btn-sm CRBtnDesign btnResize e67e22 btn-import margin-write-20" OnClick="btnImportFile_Click" />--%>

                                        <asp:Button ID="btnImportFileToCart" ValidationGroup="import" runat="server" Text="<%$Resources:Language, Btn_purchase_import_file  %>" CssClass="btn btn-info btn-sm CRBtnDesign btnResize e67e22 btn-import margin-write-20" OnClick="ImportFileToCart" />
                                        <br />

                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="fileUpload" runat="server" ErrorMessage="File Required!" ForeColor="Red" Display="Dynamic" ValidationGroup="import"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="fileUpload" runat="server" Display="Dynamic" ForeColor="Red" ErrorMessage="Please select a valid file" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$" ValidationGroup="import"></asp:RegularExpressionValidator>


                                        <asp:Label runat="server" ID="lblmsg" CssClass="import-msg"></asp:Label>
                                        <asp:Label runat="server" ID="lblTrackRows"></asp:Label>
                                        <asp:Label ID="lblImport" runat="server" Text="List of wrong cell row number: " CssClass="import-msg error" Visible="false"></asp:Label>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnImportFileToCart" />
                                </Triggers>
                            </asp:UpdatePanel>


                            <%--<asp:Button ID="btnSampleDownload" runat="server" Text="Download Sample" CssClass="btn btn-primary btn-sm CRBtnDesign " />--%>
                        </div>
                    </div>

                    <div class="col-md-4 disNone">
                        <div class="form-inline data-import-box">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnImportSample" runat="server" Text="<%$Resources:Language, Lbl_purchase_download_simple  %>" CssClass="btn btn-info btn-sm CRBtnDesign" OnClick="btnImportSample_OnClick" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnImportSample" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                </div>
            </div>

            <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal" runat="server" id="divProductList">
                <div class="section">
                    <div class="row">
                        <div class="col-md-4">
                            <h2 class="sectionHeading lang-product-list"><%=Resources.Language.Lbl_purchase_product_list %> </h2>
                        </div>

                    </div>

                    <div class="staticContentUnit mainItem">
                        <span runat="server" visible="false"><%=Resources.Language.Lbl_invoice_sl %></span>
                        <span class="lang-product-title"><%=Resources.Language.Lbl_purchase_product_title %></span>
                        <span class="lang-price"><%=Resources.Language.Lbl_purchase_product_price %></span>
                        <span class="lang-qty"><%=Resources.Language.Lbl_purchase_product_qty %></span>
                        <span class="lang-total-price"><%=Resources.Language.Lbl_purchase_product_total_price %></span>
                    </div>
                    <div id="divStockProductList" runat="server" class="mainItemF">
                        <!-- Dynamic content -->
                    </div>

                    <div class="row">
                        <div class="">
                            <div class="save-supply">
                                <div class="purchase-payment col-md-12">
                                    <div class="purchase-amount">
                                        <asp:Label ID="lblStockAmout" runat="server" CssClass="col-xs-4 col-md-5 padding-zero lang-total-purchase-amount" Text="<%$Resources:Language, Lbl_purchase_total_purchase_amount  %>"></asp:Label>
                                        <div class="col-xs-8 col-md-7 bulk-stock">
                                            <div class="input-group totalAmountMargin">
                                                <asp:TextBox ID="txtStockAmount" runat="server" Enabled="false" CssClass="PurchaseTxtBox float-number-validate form-control" placeholder="Total Stock Amount"></asp:TextBox>
                                                <span class="input-group-btn"></span>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="btnEditPurAmt" CssClass="btn" runat="server" OnClick="btnEditPurAmt_Click"><span class="glyphicon glyphicon-pencil" aria-hidden="true" ></span></asp:LinkButton>
                                                    <asp:LinkButton ID="btnUpdatePurAmt" CssClass="btn" runat="server" Visible="false"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">                                                                         
                                        <asp:Label ID="Label16" runat="server" CssClass="col-xs-4 col-md-5 lang-supplier-commission" Text="<%$Resources:Language, Lbl_purchase_supplier_commission%>"></asp:Label> <%--Text="Supplier Payment"--%>
                                        <div class="col-xs-8 col-md-7 bulk-stock">
                                            <asp:TextBox ID="txtPurchasePayment" runat="server" CssClass="form-control float-number-validate schedule-amt width-purchase-adjustment" placeholder=""></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group purchase-code">
                                        <asp:Label ID="Label4" runat="server" CssClass="col-xs-4 col-md-5 lang-purchase-code" Text="<%$Resources:Language, Lbl_purchase_purchase_code%>"></asp:Label>
                                        <div class="col-xs-8 col-md-7 ">
                                            <div class="input-group bulk-stock">
                                                <asp:TextBox ID="txtPurchaseCode" runat="server" CssClass="PurchaseTxtBox form-control" Enabled="False"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="btnEditParchaseCode" CssClass="btn" runat="server" OnClick="btnEditParchaseCode_OnClick"><span class="glyphicon glyphicon-pencil" aria-hidden="true" ></span></asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <asp:Label runat="server" ID="labelPurDate" CssClass="col-xs-4 col-md-5 lang-purchase-date" Text="<%$Resources:Language, Lbl_purchase_purchase_date%>"></asp:Label>
                                        <div class="col-xs-8 col-md-7 bulk-stock">
                                            <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="form-control datepickerCSS width-purchase-adjustment"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group comment">
                                        <asp:Label ID="Label3" runat="server" CssClass="col-xs-4 col-md-5 lang-purchase-comment" Text="<%$Resources:Language, Lbl_purchase_purchase_comment%>"></asp:Label>
                                        <div class="col-xs-8 col-md-7 bulk-stock">
                                            <div class="">
                                                <asp:TextBox ID="txtComment" runat="server" CssClass="form-control width-purchase-adjustment" TextMode="MultiLine" placeholder="<%$Resources:Language, Lbl_purchase_purchase_notes%>" Columns="28"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="schedule-payment col-xs-12 col-md-12" runat="server" id="divSchedulePayment">
                                    <div class="disNone">
                                        <asp:CheckBox runat="server" ID="chkScheduePyament" CssClass="lang-schedule-payment" Text="<%$Resources:Language, Lbl_purchase_Schedule_Payment%>" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="col-xs-4 col-md-5 lang-schedule-payment" Text="<%$Resources:Language, Lbl_purchase_Schedule_Payment%>"></asp:Label>
                                        <div class="col-xs-8 col-md-7 bulk-stock">
                                            <div class="input-group form-inline">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtSchedulePayment" runat="server" CssClass="form-control float-number-validate purchase-amt width-purchase-adjustment" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtSchedulePaymentDate" runat="server" CssClass="form-control datepickerCSS  purchase-amt width-purchase-adjustment" placeholder=""></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server" CssClass="col-xs-4 col-md-5 lang-schedule-comment" Text="<%$Resources:Language, Lbl_purchase_Schedule_Payment%>"></asp:Label>
                                        <div class="col-xs-8 col-md-7 bulk-stock">
                                            <div class="">
                                                <asp:TextBox ID="txtScheduleComment" runat="server" CssClass="form-control width-purchase-adjustment" TextMode="MultiLine" placeholder="<%$Resources:Language, Lbl_purchase_Schedule_comment%>" Columns="28"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="supplier-received col-md-12" id="divSupplierReceived" runat="server">
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="label18" CssClass="col-xs-4 col-md-5 total-received" Text="<%$Resources:Language, Lbl_purchase_track_received %>"></asp:Label>
                                        <div class="col-xs-8 col-md-7 bulk-stock">
                                            <asp:TextBox ID="txtSupplierReceived" runat="server" CssClass="form-control width-purchase-adjustment"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label19" runat="server" CssClass="col-xs-4 col-md-5 lang-track-received-comment" Text="<%$Resources:Language, Lbl_purchase_track_received_comment%>"></asp:Label>
                                        <div class="col-xs-8 col-md-7 bulk-stock">
                                            <asp:TextBox ID="txtSupplierReceivedComment" runat="server" CssClass="form-control width-purchase-adjustment" TextMode="MultiLine" placeholder="<%$Resources:Language, Lbl_purchase_write_track_received_notes %>" Columns="28"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="multi-stock-button saveAll-cencel-btn">
                                    <asp:Button ID="btnSaveProductCartToDatabase" runat="server" Text="<%$Resources:Language, Btn_purchase_save_all%>" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnAddOpt stock-btn-saveAll" OnClick="SaveProductCartToDatabase" />
                                    <asp:Button ID="btnCancelMultiStock" runat="server" Text="<%$Resources:Language, Btn_purchase_cancel%>" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnReset" OnClick="btnCancelMultiStock_Click" />
                                </div>
                            </div>
                        </div>

                        <!-- MultiStock Save button -->

                    </div>
                </div>
            </div>
        </div>
    </div>


    <script src='<%= ResolveUrl("~/Js/bootstrap-tagsinput.min.js") %>'></script>
    <script src="<%= ResolveUrl("~/Js/select2.min.js?v=0.154") %>"></script>
    <script src='<%= ResolveUrl("~/Admin/InventoryBundle/Script/stock-loader.js?v=0.002") %>'></script>
    <script src='<%= ResolveUrl("~/Admin/InventoryBundle/Script/supplier-commission.js") %>'></script>

    <script type="text/javascript">


        var prm = Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(FileUploader);


        // File upload function
        function FileUploader() {

            $('#<%= FileUpload1.ClientID %>').change("click", function () {

                // validation checking
                var productCode = document.getElementById('<%= txtScanCode.ClientID %>');
                var prodName = document.getElementById('<%= txtProduct.ClientID %>');
                if (productCode.value.length == 0 || prodName.value.length == 0) {
                    alert('Product Name and code  is required');
                    $('#<%= Image1.ClientID %>').attr('src', null);

                    return;
                }

                $('#btnUpload').trigger('click');
            });


            $("#btnUpload").click(function (evt) {
                var fileUpload = $("#<%= FileUpload1.ClientID %>").get(0);
                var files = fileUpload.files;

                var originalImage = files[0].name;
                var extension = originalImage.substr((originalImage.lastIndexOf('.') + 1));

                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }

                var hasImageId = document.getElementById("<%= txtImgFileName.ClientID %>").value;

                if (hasImageId == '') {
                    // generate image id
                    var date = new Date();
                    var components = [
                        date.getYear(),
                        date.getMonth(),
                        date.getDate(),
                        date.getHours(),
                        date.getMinutes(),
                        date.getSeconds(),
                        date.getMilliseconds()
                    ];

                    var id = components.join("");
                }
                else {
                    id = hasImageId;
                }

                var hiddenControl = '<%= txtImgFileName.ClientID %>';

                var imgWithExtension = "";
                imgWithExtension = id + "." + extension;


                document.getElementById(hiddenControl).value = id;
                var ImageForDB = '<%= txtImgDb.ClientID %>';
                document.getElementById(ImageForDB).value = imgWithExtension;



                $.ajax({
                    url: "Controller/FileUploadHandler.ashx?id=" + id + "",
                    type: "POST",
                    data: data,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        //alert(result);
                    },
                    failure: function (response) {
                        console.log(response);
                    },
                    error: function (response) {
                        console.log(response);
                    }
                });

                evt.preventDefault();
            });
        }


        // Image upoload with ajax
        $(document).ready(function () {

            // file upload function calling
            FileUploader();

            $(document).on("keyup paste", function () {

                if ($(document.activeElement).attr("id") == "<%= txtSearchNameCode.ClientID %>") {

                    $("[id$=txtSearchNameCode]").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%= ResolveUrl("~/Admin/SaleBundle/View/Invoice.aspx/getSearchProductListAction") %>',
                                data: "{ 'search': '" + request.term + "', 'itemType': '0'}",
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    response($.map(data.d, function (item) {

                                        return {
                                            label: item.split('<>')[0] + item.split('<>')[1] + ' [' + item.split('<>')[2] + ']',
                                            val: item.split('<>')[2]
                                        };
                                    }));
                                },
                                error: function (data) {
                                    showMessage(data.responseText, "Error");
                                },
                                failure: function (data) {
                                    showMessage(data.responseText, "Error");
                                }
                            });
                        },
                        select: function (e, i) {
                            $("[id$=hfProductDetails]").val(i.item.val);
                        },
                        minLength: 1
                    });
                }
            });


            $(function () {
                $("input").val();
            });

        });


        //File upload get File name -->
        function getFileName() {
            var varfile = document.getElementById("contentBody_fuProductImg");
            document.getElementById("contentBody_filename").value = varfile.value;
        }


        $(function () {
            $("input").val();
        });

        prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            var script = document.createElement('script');
            script.src = '../Js/bootstrap-tagsinput.min.js';
            script.type = 'text/javascript';
            var head = document.getElementsByTagName("head")[0];
            head.appendChild(script);

        });

        prm.add_endRequest(function (s, e) {
            var script = document.createElement('script');
            script.src = 'InventoryBundle/Script/stock-variant.js';
            script.type = 'text/javascript';
            var head = document.getElementsByTagName("head")[0];
            head.appendChild(script);
        });


        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%= Image1.ClientID %>').attr('src', e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
                }
            }


            function unitPurchsaeTotalAmount() {
                var qty = $('#contentBody_txtQty').val();
                var bPrice = $('#contentBody_txtBPrice').val();
                if (qty == "")
                    qty = 0;
                if (bPrice == "")
                    bPrice = 0;

                $('#txtUnitTotalPurchaseAmt').val(parseFloat(qty) * parseFloat(bPrice));
            }

            $('<%= FileUpload1.ClientID %>').change(function () {
            readURL(this);
        });
    </script>


    <script>

        $('#contentBody_lblImei').keypress(function () {
            console.log("Test");
        });

        activeModule = "inventory";
    </script>
</asp:Content>
