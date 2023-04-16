<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Purchase.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.Purchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Purchase
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/StockOpt.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/wwwroot/css/designer.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Css/bootstrap-tagsinput.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Css/select2.min.css") %>" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/StockBulkOpt-responsive.css") %>" rel="stylesheet" type="text/css" />
    <script src='<%= ResolveUrl("~/Js/bootstrap-tagsinput.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Admin/InventoryBundle/Script/stock-loader.js") %>' type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Admin/InventoryBundle/Script/variant-search.js?v=0.015") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/InventoryBundle/Script/stock-variant.js?v=0.015") %>"></script>



    <script>
        $(function () {
            $("#tabs").tabs();
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="stock-operation">
        <div class="row">
            <div class="purchaseMarginTop col-md-12 col-sm-12 col-xs-12">
                <div class="section">
                    <h2 class="sectionBreadcrumb">
                        <asp:Label ID="lblStockTitle" CssClass="lang-purchase" runat="server" Text="Product"></asp:Label>
                    </h2>
                    <asp:Label runat="server" ID="lblTest" Text="" BackColor="red"></asp:Label>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="section-purchase">
                        <h2 class="sectionHeading lang-add-update-product">Add / UPDATE Product</h2>

                        <div class="input-group search-purchase">
                            <asp:TextBox ID="txtSearchNameCode" CssClass="form-control btnEditOpt" runat="server" aria-describedby="btnLoadProductDetails" placeholder="Search/Scan Product by Name/Code"></asp:TextBox>
                            <asp:HiddenField ID="hfProductDetails" runat="server" />
                            <span class="input-group-btn">
                                <asp:ImageButton ID="btnLoadProductDetails" runat="server" CssClass="btn btn-default inp-group" OnClick="btnLoadProductDetails_Click" ImageUrl="~/Img/search.png" />
                            </span>
                        </div>


                        <asp:LinkButton ID="MainTab" runat="server" OnClick="MainTab_Click" CssClass="initial">Main Input</asp:LinkButton>
                        <asp:LinkButton ID="OthersTab" runat="server" OnClick="OthersTab_Click" CssClass="initial">Other Input</asp:LinkButton>
                        <asp:LinkButton ID="DynamicTab" runat="server" OnClick="DynamicTab_Click" CssClass="initial">Dynamic Input</asp:LinkButton>

                        <div class="">
                            <asp:MultiView ID="MainView" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
                                                <h3>
                                                    <span>View 1 </span>
                                                </h3>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
                                                <h3>View 2
                                                </h3>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                        <tr>
                                            <td>
                                                <h3>View 3
                                                </h3>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                        </div>



                        <div class="form-group" id="divWarehouse" runat="server">
                            <asp:Label ID="Label13" CssClass="lbl col-xs-4 col-sm-4 lang-store" runat="server" Text="Store <span class='required'>*</span>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList runat="server" ID="ddlStoreList" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group" id="divLocation" runat="server" visible="false">
                            <asp:Label ID="Label24" CssClass="lbl col-xs-4 col-sm-4 lang-location" runat="server" Text="Location"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList runat="server" ID="ddlLocationList" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divManufacturer">
                            <asp:Label ID="Label10" CssClass="lbl col-xs-4 col-sm-4 lang-manufacturer-brand" runat="server" Text="Manufacturer/Brand"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList ID="ddlManufacturer" CssClass=" form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblSup" CssClass="lbl col-xs-4 col-sm-4 lang-supplier-vendor" runat="server" Text="Supplier / Vendor <span class='required'>*</span>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList ID="ddlSup" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblCat" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-category" runat="server" Text="Category <span class='required'>*</span>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:DropDownList ID="ddlCat" CssClass=" form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label1" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-product-name" runat="server" Text="Product Name <span class='required'>*</span>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtProduct" CssClass="form-control" runat="server"></asp:TextBox>
                                <div id="divVariantControlForUpdate" runat="server" visible="false">
                                    <asp:Label ID="lblVaiantUpdateLabel" CssClass="control-stock-label" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divBarcodeGenerator">
                            <asp:Label ID="lblScanCode" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-product-code" runat="server" Text="Product Code <span class='required'>*</span>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtScanCode" CssClass="form-control" runat="server" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:ImageButton runat="server" ID="btnGenerateBarCode" CssClass="btn btn-default inp-group" OnClick="btnLoadProductDetails_Click" ImageUrl="~/Img/generate.png" />
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div id="divUnit" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblUnit" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-unit" runat="server" Text="Unit"></asp:Label>
                                <div class="col-xs-8 col-sm-8">
                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-add-exist-qty" Text="Add Qty + Exists <span class='required'>*</span>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtQty" CssClass="form-control float-number-validate" runat="server" AutoPostBack="True"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revQty" runat="server" ControlToValidate="txtQty" ErrorMessage="Invalid!" ValidationExpression="^\d+(\.\d{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>

                                    <span class="input-group-addon ">+&nbsp;<asp:Label runat="server" ID="lblCurrentQty" CssClass="inp-group" Text="0" /></span>
                                </div>
                            </div>
                        </div>

                        <div id="divFreeQty" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label251" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-free-qty" runat="server" Text="Free Qty"></asp:Label>
                                <div class="col-xs-8 col-sm-8">
                                    <asp:TextBox ID="txtFreeQty" CssClass="form-control" runat="server" Enabled="false" Text="0"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divWaringinQty">
                            <asp:Label ID="lblAlertQty" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-warning-qty" runat="server" Text="Warning Qty"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="">
                                    <asp:TextBox ID="txtWarningQty" CssClass="form-control float-number-validate" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="reWarningQty" runat="server" ControlToValidate="txtWarningQty" ErrorMessage="Invalid!" ValidationExpression="^([-+]?[0-9]{0,6})$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" id="divCostPrice" runat="server">
                            <asp:Label ID="lblPriceBuy" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-set-cost-price" runat="server" Text="Buy Price <span class='required'>*</span>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtBPrice" CssClass="form-control float-number-validate" runat="server" Text="" aria-describedby="btnBPriceEdit" AutoPostBack="True"></asp:TextBox>
                                    <asp:Label runat="server" ID="lblBPrice" CssClass="disNone" Text="0" />
                                    <asp:RegularExpressionValidator ID="revBPrice" runat="server" ControlToValidate="txtBPrice" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{1,10}(?:\.[0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                    <span class="input-group-btn">
                                        <asp:ImageButton runat="server" ID="btnBPriceEdit" CssClass="btn btn-default inp-group" ImageUrl="~/Img/edit.png" /></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" id="divCompanyPrice" runat="server">
                            <asp:Label ID="Label22" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-set-company-price" runat="server" Text="Company Price "></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtCompanyPrice" CssClass="form-control float-number-validate" runat="server" Text="" aria-describedby="btnBPriceEdit"></asp:TextBox>
                                    <asp:Label runat="server" ID="Label23" CssClass="disNone" Text="0" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCompanyPrice" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{1,10}(?:\.[0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                    <span class="input-group-btn">
                                        <asp:ImageButton runat="server" ID="ImageButton1" CssClass="btn btn-default inp-group" ImageUrl="~/Img/edit.png" /></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divDealerPrice" visible="false">
                            <asp:Label ID="lblDPrice" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-wholesale-price" runat="server" Text="Wholesale Price"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtDealerPrice" CssClass="form-control float-number-validate" runat="server" Text=""></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:DropDownList ID="ddlSetBuyPrice" runat="server" CssClass="form-control margin-right-40" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblSPrice" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-sale-price" runat="server" Text="Retail Price <span class='required'>*</span>"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSPrice" CssClass="form-control float-number-validate" runat="server" Text="" aria-describedby="btnSPriceEdt"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revSPrice" runat="server" ControlToValidate="txtSPrice" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{1,10}(?:\.[0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                    <span class="input-group-btn">
                                        <asp:ImageButton runat="server" ID="btnSPriceEdt" CssClass="btn btn-default inp-group" ImageUrl="~/Img/edit.png" /></span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divUnitPurchaseAmt" visible="False">
                            <asp:Label ID="Label14" runat="server" CssClass="lbl col-xs-4 col-md-4 control-stock-label lang-total-purchase-amount" Text="Total Purchase Amount"></asp:Label>
                            <div class="col-xs-8 col-md-8 bulk-stock">
                                <div class="input-group">
                                    <asp:TextBox ID="txtUnitTotalPurchaseAmt" runat="server" Enabled="false" CssClass="lang-total-stock-amount float-number-validate form-control" placeholder="Total Stock Amount"></asp:TextBox>
                                    <span class="input-group-btn"></span>
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="btnUnitEditPurAmt" CssClass="btn" runat="server"><span class="glyphicon glyphicon-pencil" aria-hidden="true" ></span></asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divSize">
                            <asp:Label ID="Label11" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-size" runat="server" Text="Size"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtSize" CssClass="form-control float-number-validate" runat="server" Text=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divWarranty" runat="server">
                            <asp:Label ID="Label2" CssClass="lbl col-xs-4 col-sm-4 control-stock-label padding-top-10 lang-warranty" runat="server" Text="Warranty"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <div class="form-inline">
                                    <div class="input-group col-xs-4 padding-top-10" style="float: left;">
                                        <asp:DropDownList ID="ddlWarrantyYear" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Year</asp:ListItem>
                                            <asp:ListItem Value="1">1 Y</asp:ListItem>
                                            <asp:ListItem Value="2">2 Y</asp:ListItem>
                                            <asp:ListItem Value="3">3 Y</asp:ListItem>
                                            <asp:ListItem Value="4">4 Y</asp:ListItem>
                                            <asp:ListItem Value="5">5 Y</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="input-group col-xs-4 padding-top-10" style="float: left;">
                                        <asp:DropDownList ID="ddlWarrantyMonth" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Month</asp:ListItem>
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
                                            <asp:ListItem Value="0">Day</asp:ListItem>
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
                            <asp:Label ID="lblImei" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-imei" Text="IMEI <span class='required'>*</span>" runat="server"></asp:Label>
                            <div class="col-xs-8 col-md-8">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnImeiChecker">
                                    <div class="input-group">
                                        <input runat="server" type="text" data-role="tagsinput" value="" id="txtIMEI" onkeydown="return (event.keyCode!=13)" />
                                        <span class="input-group-btn ">
                                            <asp:CheckBox runat="server" ID="chkIMEIEnable" CssClass="chkImeiDesign" />
                                        </span>
                                    </div>

                                </asp:Panel>
                                <asp:Button ID="btnImeiChecker" CssClass="disNone" runat="server" Text="Button" />
                            </div>
                        </div>

                        <div class="form-group" id="divSku" runat="server">
                            <asp:Label ID="lblSku" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-sku" runat="server" Text="SKU"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtSku" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divEngineNumber" runat="server">
                            <asp:Label ID="Label20" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-engine-number" runat="server" Text="Engine Number"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtEngineNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divCecishNumber" runat="server">
                            <asp:Label ID="Label21" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-cecish-number" runat="server" Text="Cecish Number"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtCecishNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divTax" runat="server">
                            <asp:Label ID="lblTax" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-tax" runat="server" Text="Tax"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtTax" CssClass="form-control float-number-validate" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revTax" runat="server" ControlToValidate="txtTax" ErrorMessage="Invalid!" ValidationExpression="^([0-9]{0,3})$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="form-group" id="divRecivedDate" runat="server">
                            <asp:Label ID="Label5" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-received-date" runat="server" Text="Received Date"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtRecivedDate" CssClass="form-control datepickerCSS " runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divExpireDate" runat="server">
                            <asp:Label ID="Label6" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-expiry-date" runat="server" Text="Expiry Date"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtExpiryDate" CssClass="form-control datepickerCSS " runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divBatchNo" runat="server">
                            <asp:Label ID="Label7" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-batch-no" runat="server" Text="Batch No"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtBatchNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divSerialNo" runat="server">
                            <asp:Label ID="Label8" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-serial-no" runat="server" Text="Serial No"></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox ID="txtSerialNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group" id="divSupplierCom" runat="server">
                            <asp:Label ID="lbl34" CssClass="lbl col-xs-4 col-sm-4 control-stock-label float-number-validate lang-supplier-payment" runat="server" Text="Supplier Payment"></asp:Label>
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
                            <asp:Label ID="Label9" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-shipment-status" runat="server" Text="Shipment Status"></asp:Label>
                            <div class="col-xs-4 col-sm-8">
                                <asp:DropDownList ID="ddlShipmentStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group" id="div1" runat="server">
                            <asp:Label ID="Label12" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-notes " runat="server" Text="Notes "></asp:Label>
                            <div class="col-xs-8 col-sm-8">
                                <asp:TextBox runat="server" ID="txtNotes" CssClass="form-control" placeholder="Notes" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group " id="divUploader" runat="server">
                            <asp:Label ID="lblFileUpload" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-upload-file" runat="server" Text="Upload File"></asp:Label>

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
                            <asp:Label ID="Label26" CssClass="lbl col-xs-4 col-sm-4 control-stock-label lang-type" Text="Type" runat="server"></asp:Label>
                            <div class="col-xs-8 col-md-8">
                                <div class="">
                                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Single" Value="0" />
                                        <asp:ListItem Text="Varient" Value="1"></asp:ListItem>
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


                        <div class="form-group">
                            <asp:Button ID="btnAddToPurchaseCart" ValidationGroup="reqAddStock" CssClass="btn btn-info btn-sm CRBtnDesign btnResize e67e22 btnAddOpt" runat="server" Text="Add" />
                            <asp:Button ID="btnReset" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnReset" runat="server" Text="Clear" />
                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        activeModule = "inventory";
    </script>

</asp:Content>
