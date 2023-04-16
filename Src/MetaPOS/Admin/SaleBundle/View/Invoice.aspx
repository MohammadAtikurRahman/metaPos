<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="MetaPOS.Admin.SaleBundle.View.Invoice" EnableEventValidation="false" %>

<%@ Register Src="~/Admin/Controller/CustomerOpt.ascx" TagPrefix="uc1" TagName="CustomerOpt" %>
<%@ Register Src="~/Admin/Controller/StaffOpt.ascx" TagPrefix="uc1" TagName="StaffOpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Invoice - metaPOS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Css/bootstrap-toggle.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Css/custom-search-theme.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Css/select2.min.css?v=0.011") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Css/Invoice.css?v=0.017") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Css/Print-Style.css?v=0.011") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SaleBundle/Content/invoice.css?v=0.013") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SaleBundle/Content/invoice-responsive.css?v=0.004") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server" defaultbutton="btnProtect">
    <asp:Panel ID="pnlMainBody" runat="server" DefaultButton="btnProtect">
        <asp:Button runat="server" ID="btnProtect" Visible="false"></asp:Button>
        <div>
            <asp:Label runat="server" ID="lblTempAttr"></asp:Label>
            <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hidfieldSessionId" runat="server" />
            <asp:HiddenField ID="lblBillNoHidden" runat="server" />
        </div>

        <div class="flex-container" id="invoiceBody">
            <div class="row">
                <div class="col-lg-8 col-md-8 col-sm-7 col-xs-12 same-height">

                    <div class="section-next-previous">
                        <div class="sellFBlock">
                            <asp:Label ID="lblErrorPrint" runat="server" Text=""></asp:Label>
                            <div class="billLbl form-inline billMar" runat="server" visible="false">
                                <!-- Customer Name -->
                                <span class="disBlock">
                                    <asp:Label ID="titleCusID" runat="server" Text="" CssClass="lblHead"><i class="fa fa-user" aria-hidden="true"></i></asp:Label>
                                    <asp:Label ID="lblHeaderCusName" CssClass="" runat="server" Text=": .... / ...."></asp:Label>
                                </span>
                            </div>
                            <div class="billLbl form-inline billMar" style="padding-left: 30px;" runat="server" visible="false">
                                <asp:Panel ID="pnlCheckCusID" runat="server" DefaultButton="btnCheckCusDetails">
                                    <div class="disNone">
                                        <asp:Label ID="Label3" runat="server" Text=":" CssClass="colon"></asp:Label>
                                    </div>
                                    <!-- Total Due -->
                                    <span class="disBlock">
                                        <asp:Label ID="titleCusDue" runat="server" Text="" CssClass="lblHead"><i class="fa fa-sort-amount-asc" aria-hidden="true"></i></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text=":" CssClass="colon"></asp:Label>
                                    </span>
                                    <!-- Bill No -->
                                    <asp:Label ID="lblBillingNoTag" runat="server" Text="" CssClass="lblHead"><i class="fa fa-file-text" aria-hidden="true"></i></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text=":" CssClass="colon"></asp:Label>
                                </asp:Panel>
                            </div>

                            <div class="col-md-12 col-sm-12 col-xs-12 billLbl section form-inline">
                                <div class="col-md-7 col-sm-7 col-xs-12">
                                    <div class="bill-no">
                                        <div class="form-group" runat="server" id="divInvoice">
                                            <i class="fa fa-shopping-cart" aria-hidden="true"></i>
                                            <asp:Label ID="lblBillingNo" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group billing-invoice" id="dateEdited">
                                        <!-- Date Editable -->
                                        <div id="divDate" class="divDate">
                                            <i class="fa fa-calendar " aria-hidden="true"></i>
                                            <asp:Label ID="lblBillingDate" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtBillingDate" runat="server" CssClass="BillingDate" disabled></asp:TextBox>
                                        </div>

                                        <div id="divEditDate">
                                            <input type="button" id="btnEditDate" class="btn btn-sm btn-default btn-edit-date" value="<%=Resources.Language.Lbl_invoice_Edit %>" />
                                            <input type="button" id="btnUpdateDate" class="btn btn-sm btn-default btn-edit-date disNone" value="<%=Resources.Language.updated %>" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5 col-sm-5 col-xs-12 searchBox">
                                    <asp:Panel ID="pnlSearchBillingNo" runat="server" CssClass="text-right ">
                                        <div class="form-group form-inline invoice-search col-xs-12 searchBox">
                                            <asp:TextBox ID="txtBillingNoSearch" CssClass="form-control invoice-search-billing" runat="server" placeholder="<%$Resources:Language, Lbl_invoice_search___ %>"></asp:TextBox>
                                            <input id="btnNew" class="btn btn-info btn-sm btnMimpCustom btn-new-invoice" type="button" value="<%=Resources.Language.Lbl_invoice_new_invoice %>" />
                                        </div>
                                        <div class="form-group">
                                        </div>
                                        <div class="form-group disNone">
                                            <!-- next -->
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 section2">
                        <asp:Label runat="server" ID="lblStockProblemId"></asp:Label>
                        <div class="headerDesign">
                            <div class="section3">
                                <div id="divSaleUpdateTitle" class="disNone">
                                    <p class="sale-product-title"><%=Resources.Language.Lbl_invoice_sales_product %></p>
                                </div>
                                <div class="dealer section3 form-inline disNone" runat="server" id="divCusTypeInvoice">
                                    <asp:RadioButtonList ID="rblCusType" runat="server" CssClass="radio-inline">
                                    </asp:RadioButtonList>
                                </div>
                                <div class="dealer section3 form-inline disNone" runat="server" id="divSearchBy">
                                    <asp:RadioButtonList ID="rblSearchIn" runat="server" CssClass="radio-inline">
                                    </asp:RadioButtonList>
                                </div>

                                <asp:Panel ID="pnlScan" runat="server">
                                    <div class="input-group add-product" id="divSearchBox">
                                        <asp:TextBox ID="txtSearchNameCode" CssClass="addProductName form-control" runat="server" aria-describedby="btnAddToCart" placeholder="<%$Resources:Language, Lbl_invoice_search_product_by %>" TabIndex="1"></asp:TextBox>
                                        <asp:HiddenField ID="hfProductDetails" runat="server" />
                                        <span class="input-group-btn">
                                            <button type="button" id="btnAddToCart" class="btn btn-default inp-group add-to-cart">
                                                <img src="../Img/plus.png" /></button>
                                            <button type="button" id="btnLoadDefaultProduct" class="disNone btn btn-default cart-default-loader"><%=Resources.Language.Lbl_invoice_autoloader %></button>
                                        </span>
                                    </div>
                                </asp:Panel>

                                <div class="form-inline form-group divFieldControl disNone" runat="server" id="divFieldControl">
                                    <div class="divIMEI" runat="server" id="divIMEI">
                                        <asp:DropDownList runat="server" ID="ddlIMEI" CssClass="form-control" multiple="multiple" />
                                    </div>
                                    <div id="divField" runat="server">
                                        <div class="form-inline divAttributeControl" runat="server" id="divAttributeControl">
                                        </div>
                                    </div>
                                    <button id="btnAddAttr" type="button" class="btn btn-default btn-sm"><%=Resources.Language.Lbl_invoice_add_imei %></button>
                                </div>


                                <table class="table table-responsive dynamicContentUnit mainItem">
                                    <thead id="staticTheadRow">
                                        <tr class="dynamicContentUnit">
                                            <th class="disNone"></th>
                                            <th class="disNone"></th>
                                            <th class="disNone"></th>
                                            <th class="disNone"></th>
                                            <th><%=Resources.Language.Lbl_invoice_sl %></th>
                                            <th><%=Resources.Language.Lbl_invoice_Details %></th>
                                            <th><%=Resources.Language.lbl_invoice_price %></th>
                                            <th><%=Resources.Language.Lbl_invoice_qty %></th>
                                            <th id="thSerialNo" class="disNone"><%=Resources.Language.Lbl_invoice_serial %></th>
                                            <th id="thReturn" class="disNone"><%=Resources.Language.Lbl_invoice_return %></th>
                                            <th><%=Resources.Language.Lbl_invoice_total %></th>
                                            <th></th>
                                            <th class="disNone"></th>
                                            <th class="disNone"></th>
                                            <th class="disNone"></th>
                                            <th class="disNone"></th>
                                        </tr>
                                    </thead>
                                    <tbody id="divShoppingList" runat="server" class="mainItemF">
                                        <tr class="dynamicContentUnit">
                                          
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-5 col-xs-12 sale-padding-top same-height">
                    <div class="alert alert-info alert-dismissible" role="alert" runat="server" id="divOfferDetails" visible="False">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <asp:Label runat="server" ID="lblOfferDetails" Text="Offer Report"></asp:Label>
                    </div>

                    <div class="headerDesign">
                        <div class="" id="section-print-custom-div">
                            <div class="">
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs" role="tablist" id="tabinSales">
                                    <li role="presentation" class="active "><a href="#account" aria-controls="account" role="tab" data-toggle="tab" class="accounts"><%=Resources.Language.Lbl_invoice_account %></a></li>
                                    <li role="presentation" class="disNone"><a href="#customer" aria-controls="customer" role="tab" data-toggle="tab">
                                        <asp:Label ID="lblTabsCustomer" CssClass="lang-customers" runat="server" Text="New customer"></asp:Label></a></li>
                                    <li role="presentation" id="tabMiscCost" class="disNone"><a href="#miscCost" aria-controls="refferal" role="tab" data-toggle="tab" class="lang-misc"><%=Resources.Language.Lbl_invoice_mise_costtab %></a></li>
                                    <li role="presentation"><a href="#refferal" aria-controls="refferal" role="tab" data-toggle="tab" id="liRefferal" class="lang-referal-tab"><%=Resources.Language.Lbl_invoice_refferal %></a></li>
                                    <li role="presentation"><a href="#comment" aria-controls="comment" role="tab" data-toggle="tab" class="lang-notes"><%=Resources.Language.Lbl_Invoice_notes %></a></li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content sale-tab">
                                    <div role="tabpanel" class="tab-pane active" id="account">
                                        <asp:Panel runat="server">
                                            <asp:Panel runat="server">
                                                <div class="section3 section-control min-right-side-height">
                                                    <div class="mainItem">
                                                        <div class="form-horizontal salePadding">
                                                            <div class="form-group customer-search">
                                                                <div class="input-group div-select2">
                                                                    <asp:DropDownList ID="ddlCustomer" runat="server" TabIndex="4"></asp:DropDownList>
                                                                    <button type="button" class="input-group-addon" id="btnCustomerModalSale"><i class="fa fa-plus"></i></button>
                                                                </div>
                                                            </div>
                                                            <div class="form-group div-cart-total">
                                                                <asp:Label ID="lblNetAmt" CssClass="lbl col-sm-5 col-xs-4 control-label lang-cart-total padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_cart_total %> "></asp:Label>
                                                                <div class="col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtNetAmt" CssClass="form-control txtboxBg TxtboxToLabel padding-zero" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group disNone" id="divMisc">
                                                                <asp:Label ID="Label19" CssClass="lbl col-md-5 col-sm-5 col-xs-4 control-label lang-misc-cost padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_mise_cost %>"></asp:Label>
                                                                <div class="col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="lblMiscAmt" CssClass="form-control txtboxBg TxtboxToLabel padding-zero" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group form-horizontal disNone" id="divVat">
                                                                <asp:Label ID="Label20" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label lang-vat padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_vat %>"></asp:Label>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-2-persent" style="padding-right: 0px;">
                                                                    <asp:TextBox ID="txtVatAmt" CssClass="form-control " runat="server" Text="" TabIndex="5"></asp:TextBox>

                                                                    <label id="lblVatAmtError" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>
                                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-2 padding-2-persent padding-zero-five ">
                                                                    <asp:Label runat="server" ID="Label13" CssClass="disNone display-disc-type"></asp:Label>
                                                                    <div>
                                                                        <asp:DropDownList ID="ddlVatAmt" runat="server" CssClass="form-control" Style="padding-left: 0px; padding-right: 0px; border-radius: 4px;">
                                                                            <asp:ListItem Value="Tk" Selected="True" Text="<%$Resources:Language, Lbl_invoice_taka %>"></asp:ListItem>
                                                                            <asp:ListItem Value="%">%</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                            

                                                            <div class="form-group form-horizontal disNone" id="divDiscount">
                                                                <asp:Label ID="lblDiscAmt" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label lang-discount padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_discount %>"></asp:Label>
                                                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6 padding-2-persent" style="padding-right: 0px;">
                                                                    <asp:TextBox ID="txtDiscAmt" CssClass="form-control" runat="server" Value="" Text="" TabIndex="6"></asp:TextBox>
                                                                    <label id="lblDiscAmtError" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>

                                                                <div class="col-lg-3 col-md-3 col-sm-3 col-xs-2 padding-2-persent padding-zero-five">
                                                                    <asp:Label runat="server" ID="lblDiscType" CssClass="disNone display-disc-type"></asp:Label>
                                                                    <div id="divDiscountOption">
                                                                        <asp:DropDownList ID="ddlDiscType" runat="server" Style="padding-left: 0px; padding-right: 0px; border-radius: 4px;" CssClass="form-control">
                                                                            <asp:ListItem Value="" Selected="True" Text="<%$Resources:Language, Lbl_invoice_taka %>">  </asp:ListItem>
                                                                            <asp:ListItem Value="%">%</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group disNone" id="divMoreDisc">
                                                                <asp:Label runat="server" CssClass="lbl col-lg-5 col-md-5 col-sm-5  col-xs-6 control-label lang-more-discount" ID="labelExtraDisc" Text="<%$Resources:Language, Lbl_invoice_more_discount %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-6">
                                                                    <asp:TextBox ID="txtExtraDiscount" CssClass="form-control txtboxBg TxtboxToLabel" Font-Bold="true" runat="server" Text="0.00" Enabled="false"></asp:TextBox>

                                                                </div>
                                                            </div>

                                                            <div class="form-group padding-2-persent disNone" id="divInterestAmt">
                                                                <asp:Label runat="server" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label lang-interest" ID="lblInterestAmtTxt" Text="<%$Resources:Language, Lbl_invoice_intarest %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:Label runat="server" ID="lblInterestAmt" CssClass="form-control txtboxBg TxtboxToLabel" Text="0.00"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="distance-border"></div>
                                                            </div>

                                                            <div class="form-group" id="divGrossAmt">

                                                                <!-- disNone -->
                                                                <asp:Label ID="lblGrossAmt" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label lang-total padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_total %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtGrossAmt" CssClass="form-control txtboxBg TxtboxToLabel" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group disNone" id="divPreviousDue">
                                                                <asp:Label ID="Label21" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label lang-previous-balance padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_pre_bal %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtPreviousDue" CssClass="form-control txtboxBg TxtboxToLabel padding-zero" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                                                    <label id="lblPreviousGiftAmtError" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group disNone" id="divReturnTotal">
                                                                <asp:Label ID="Label8" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-6 control-label lang-return-amount padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_return %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-6">
                                                                    <asp:TextBox ID="txtReturnTotal" CssClass="form-control txtboxBg TxtboxToLabel padding-zero" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                                                    <label id="lblReturnTotalError" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group disNone" id="divReturnPaid">
                                                                <asp:Label ID="Label22" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-6 control-label lang-return-paid padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_paid %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-6">
                                                                    <asp:TextBox ID="txtReturnPaid" CssClass="form-control txtboxBg TxtboxToLabel padding-zero" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                                                    <label id="lblReturnPaidError" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group disNone" id="divTotalBal">

                                                                <div class="form-group">
                                                                    <div class="distance-border" style="margin-bottom: 5px;"></div>
                                                                </div>

                                                                <asp:Label ID="lblGiftAmt" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label lang-total-balance padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_total_bal %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtGiftAmt" CssClass="form-control txtboxBg TxtboxToLabel padding-zero" runat="server" Enabled="false" Text="0.00"></asp:TextBox>
                                                                    <label id="lblGiftAmtError" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group" id="divSaleBy">
                                                                <asp:Label ID="Label6" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label lang-sales-person padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_sales_person %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-8">
                                                                    <span id="divSaleByLabel" class="disNone sale-person-label">
                                                                        <asp:Label runat="server" ID="lblSaleBy"></asp:Label>
                                                                        <asp:Label runat="server" ID="lblSaleById" CssClass="disNone"></asp:Label>
                                                                    </span>
                                                                    <asp:DropDownList ID="ddlStaff" CssClass="form-control disNone" runat="server"></asp:DropDownList>
                                                                </div>
                                                            </div>


                                                            <div class="form-group padding-2-persent disNone" id="divReferredBy">
                                                                <asp:Label ID="Label14" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label padding-2-persent lang-referal referrelTextBox" runat="server" Text="<%$Resources:Language, Lbl_invoice_referrel %>"></asp:Label>
                                                                <div class="padding-right-0 referrel-div col-lg-5 col-md-5 col-sm-5 col-xs-7">
                                                                    <asp:DropDownList ID="ddlReferredBy" CssClass="form-control referrelTextBox" runat="server"></asp:DropDownList>
                                                                </div>
                                                                <div class="padding-0 col-lg-2 col-md-2 col-sm-2 col-xs-1">
                                                                    <input type="button" class="btn btn-default btnQuickPay" id="btnAddReferedBy" value="+" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group disNone" id="divPayCard">
                                                                <!-- disNone -->
                                                                <asp:Label ID="lblPayCard" CssClass="lbl col-sm-5 control-label lang-pay-card" runat="server" Text="<%$Resources:Language, Lbl_invoice_pay_card %> "></asp:Label>
                                                                <div class="col-sm-7">
                                                                    <asp:TextBox ID="txtPayCard" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <asp:Label ID="lblPayCardError" CssClass="crStyle" runat="server" Text="Invalid!" Visible="false"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group padding-2-persent disNone" id="divToken">
                                                                <asp:Label ID="lblToekn" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 padding-2-persent control-label lang-token" runat="server" Text="<%$Resources:Language, Lbl_invoice_token %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtToken" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                                                                    <label id="lblToeknError" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>
                                                            </div>



                                                            <div class="form-group padding-2-persent disNone" id="divPayDate">
                                                                <!-- visible= false -->
                                                                <asp:Label ID="lblPayDate" CssClass="lbl col-lg-5 col-md-5 col-sm-5 col-xs-4 control-label padding-2-persent lang-pay-date" runat="server" Text="<%$Resources:Language, Lbl_invoice_pay_date %>"></asp:Label>
                                                                <div class="col-lg-7 col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtPayDate" CssClass="form-control datepickerCSS" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            <hr />

                                                            <div class="form-group">
                                                                <div class="col-xs-4 col-sm-5 col-md-5 col-lg-5 padding-right-0">
                                                                    <asp:Label ID="lblPayCash" CssClass="lbl control-label lang-pay-type disNone" runat="server" Text="<%$Resources:Language, Lbl_invoice_cart_payment %>"></asp:Label>
                                                                    <asp:DropDownList ID="ddlPayType" CssClass="form-control ddlPayType" runat="server">
                                                                        <asp:ListItem Value="0" Selected="True" Text="<%$Resources:Language, Lbl_invoice_Cash %>">  </asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_invoice_card %>">  </asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="<%$Resources:Language, Lbl_invoice_cheque %>"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="<%$Resources:Language, Lbl_invoice_mobile_bank %>"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="<%$Resources:Language, Lbl_invoice_EMI_plan %>"></asp:ListItem>
                                                                        <asp:ListItem Value="5" Text="<%$Resources:Language, Lbl_invoice_cash_on_delivary %>"></asp:ListItem>

                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-xs-6 col-sm-5 col-md-5 col-lg-5 padding-left-right-5 payCashTxtBox" id="divPayCashOneTextbox">
                                                                    <asp:TextBox ID="txtPayCash" CssClass="form-control pay-cash float-number-validate" runat="server" placeholder="0.00" TabIndex="7"></asp:TextBox>

                                                                    <label id="lblPayCashError" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>
                                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 btnPadding-0" id="divPayOne">
                                                                    <input type="button" class="btn btn-default padding-btn-6 btnQuickPay" id="btnQuickPayOne" value="Pay" />
                                                                </div>
                                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 padding-0 disNone" id="divAddButton">
                                                                    <input type="button" class="btn btn-default padding-btn-6" id="btnAddPay" value="+" />
                                                                </div>
                                                            </div>



                                                            <div class="form-group disNone" id="divPaymentTwo">
                                                                <div class="col-xs-4 col-sm-5 col-md-5 col-lg-5 padding-right-0">
                                                                    <asp:DropDownList ID="ddlPayTypeTwo" CssClass="form-control ddlPayType" runat="server">
                                                                        <asp:ListItem Value="0" Selected="True"  Text="<%$Resources:Language, Lbl_invoice_Cash %>"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_invoice_card %>"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="<%$Resources:Language, Lbl_invoice_cheque %>"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="<%$Resources:Language, Lbl_invoice_mobile_bank %>"></asp:ListItem>
                                                                        <%--<asp:ListItem Value="4">Deposit</asp:ListItem>--%>
                                                                        <asp:ListItem Value="4" Text="<%$Resources:Language, Lbl_invoice_cash_on_delivary %>"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-xs-6 col-sm-5 col-md-5 col-lg-5 padding-left-right-5 payCashTxtBox" id="divPayCashTowTextbox">
                                                                    <asp:TextBox ID="txtPayTwo" CssClass="form-control pay-cash" runat="server" placeholder="0.00"></asp:TextBox>
                                                                    <label id="lblPayCashErrorTwo" class="crStyle" style="display: none;">Invalid!</label>
                                                                </div>
                                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1">
                                                                    <input type="button" class="btn btn-default btnQuickPay lang-pay" id="btnQuickPayTwo" value="Pay" />
                                                                </div>
                                                                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 padding-0">
                                                                    <input type="button" class="btn btn-default padding-btn-6 btnRemovePay" id="btnRemovePayTwo" value="X" />
                                                                </div>
                                                            </div>



                                                            <div class="disNone" id="MaturityDate">
                                                                <div class="form-group padding-left-46">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                        <asp:TextBox ID="txtMaturityDate" runat="server" CssClass="form-control datepickerCSS lang-maturity-date" placeholder="<%$Resources:Language, Lbl_invoice_Maturity_date  %>"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                        <asp:DropDownList ID="ddlBankName" CssClass="form-control" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                        <asp:TextBox ID="txtCheckNo" runat="server" CssClass="form-control" placeholder="<%$Resources:Language, Lbl_invoice_cheque_no  %>"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group padding-left-46 disNone" id="divCardType">
                                                                <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                    <asp:DropDownList ID="ddlCardType" CssClass="form-control" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                    <asp:TextBox ID="txtCardNo" runat="server" CssClass="form-control" placeholder="<%$Resources:Language, Lbl_invoice_Card_no  %>"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            <div class="form-group padding-left-46 disNone" id="divMobileBankType">
                                                                <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                    <asp:DropDownList ID="ddlMobileBankType" CssClass="form-control" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                    <asp:TextBox ID="txtTrxId" runat="server" CssClass="form-control" placeholder="Trx Id"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group padding-left-46 disNone" id="divEmiType">
                                                                <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                    <asp:DropDownList ID="ddlEmiType" CssClass="form-control" runat="server">
                                                                        <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_invoice_select_a_emi_plan %>"></asp:ListItem>
                                                                        <asp:ListItem Value="3month" Text="<%$Resources:Language, Lbl_invoice_3month %>"></asp:ListItem>
                                                                        <asp:ListItem Value="6month" Text="<%$Resources:Language, Lbl_invoice_6month %>"></asp:ListItem>
                                                                        <asp:ListItem Value="9month" Text="<%$Resources:Language,  Lbl_invoice_9month%>"></asp:ListItem>
                                                                        <asp:ListItem Value="12month" Text="<%$Resources:Language,  Lbl_invoice_12month%>"></asp:ListItem>
                                                                        <asp:ListItem Value="18month" Text="<%$Resources:Language,  Lbl_invoice_18month%>"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12 col-xs-12 padding-2-persent">
                                                                    <asp:TextBox ID="emiCardNo" runat="server" CssClass="form-control" placeholder="Card No"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            <div class="form-group disNone" id="divInstalmentAmtShow">
                                                                <asp:Label ID="Label27" CssClass="lbl col-md-5 col-sm-5 col-xs-4 padding-2-persent control-label" runat="server" Text="Paymentable"></asp:Label>
                                                                <div class="col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtInstalmentAmount" CssClass="form-control" disabled="disabled" runat="server" Text=""></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div id="divInstalment" class="disNone">
                                                                <hr />

                                                                <div class="form-group " id="divInstalmentStartDate">
                                                                    <asp:Label ID="Label15" CssClass="lbl col-md-5 col-sm-5 col-xs-4 padding-2-persent control-label lang-next-start-date" runat="server" Text="<%$Resources:Language, Lbl_invoice_start_date %>"></asp:Label>
                                                                    <div class="col-md-7 col-sm-7 col-xs-8">
                                                                        <asp:TextBox ID="txtStartPymentDate" CssClass="form-control datepickerCSS" runat="server" Text=""></asp:TextBox>
                                                                    </div>
                                                                </div>


                                                                <div class="form-group" id="divInstalmentPayDate">
                                                                    <asp:Label ID="Label24" CssClass="lbl col-md-5 col-sm-5 col-xs-4 padding-2-persent control-label lang-next-pay-date" runat="server" Text="<%$Resources:Language, Lbl_invoice_next_date %>"></asp:Label>
                                                                    <div class="col-md-7 col-sm-7 col-xs-8">
                                                                        <div class="col-sm-12 col-xs-9 padding-zero" style="padding-right: 0px;">
                                                                            <asp:TextBox ID="txtCustomerRemainder" CssClass="form-control datepickerCSS" runat="server" Text=""></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-12 col-sm-12 col-xs-3 padding-left-0 padding-three-zero">
                                                                            <select class="ddlDateSeletor width-100-persent" id="ddlDateSeletor">
                                                                                <option value="0">--<%=Resources.Language.Lbl_invoice_ins_select %>--</option>
                                                                                <option value="7"><%=Resources.Language.Lbl_invoice_7days %> </option>
                                                                                <option value="15"><%=Resources.Language.Lbl_invoice_15days %> </option>
                                                                                <option value="1m"><%=Resources.Language.Lbl_invoice_1month %> </option>
                                                                                <option value="3m"><%=Resources.Language.Lbl_invoice_3month %></option>
                                                                                <option value="6m"><%=Resources.Language.Lbl_invoice_6month %></option>
                                                                                <option value="1y"><%=Resources.Language.Lbl_invoice_1year %></option>
                                                                            </select>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group" id="divInstallmentNumber">
                                                                    <asp:Label runat="server" CssClass="lbl col-md-5 col-sm-5 col-xs-4 padding-2-persent control-label lang-installment-number" Text="<%$Resources:Language, Lbl_invoice_ins_number %> "></asp:Label>
                                                                    <div class="col-md-7 col-sm-7 col-xs-8">
                                                                        <asp:TextBox runat="server" ID="txtInstalmentNumber" CssClass="form-control int-number-validate"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label runat="server" CssClass="lbl col-md-5 col-sm-5 col-xs-4 padding-2-persent control-label lang-installment-amount" Text="<%$Resources:Language, Lbl_invoice_installment %>"></asp:Label>
                                                                    <div class="col-md-7 col-sm-7 col-xs-8">
                                                                        <asp:TextBox runat="server" ID="txtDownPayment" CssClass="form-control float-number-validate"></asp:TextBox>
                                                                    </div>
                                                                    <hr />
                                                                </div>
                                                            </div>



                                                            <div class="form-group disNone" id="divInterestRate">
                                                                <hr />

                                                                <div class="padding-left-36">
                                                                    <asp:RadioButtonList ID="rblInterestType" runat="server" CssClass="interest-type">
                                                                        <%--  --%>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <asp:Label ID="Label25" CssClass="lbl col-md-5 col-sm-5 col-xs-4 control-label lang-interest-rate" runat="server" Text="<%$Resources:Language, Lbl_invoice_intarest_rate %>"></asp:Label>
                                                                <div class="col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtInterestRate" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                                                </div>
                                                            </div>



                                                            <div class="form-group" id="divDueChage">
                                                                <asp:Label ID="lblDueChage" CssClass="lbl col-md-5 col-sm-5 col-xs-4 control-label padding-2-persent lang-current-balance" runat="server" Text=""></asp:Label>
                                                                <div class="col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:TextBox ID="txtDueChange" CssClass="form-control txtboxBg TxtboxToLabel padding-zero" Enabled="False" runat="server" Text="0.00"></asp:TextBox>
                                                                    <div class="disNone" id="divExtraDiscount">
                                                                        <input type="button" class="btn-extra-discount btn btn-warning printBtnDesign lang-add-as-disc" id="btnExtraDiscount" value="<%=Resources.Language.Lbl_invoice_add_as_discount %>" />
                                                                    </div>
                                                                </div>

                                                            </div>



                                                            <div class="PaperSizeOption form-group disNone" id="divDisplayPaperSize">
                                                                <asp:Label ID="Label23" CssClass="lbl col-md-5 col-sm-5 col-xs-4 control-label lang-paper-size padding-2-persent" runat="server" Text="<%$Resources:Language, Lbl_invoice_paper_size  %>"></asp:Label>
                                                                <div class="col-md-7 col-sm-7 col-xs-8">
                                                                    <asp:RadioButtonList ID="rblPaperSizeOption" runat="server" CssClass="radio-inline">
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>


                                                            <div class="form-group" id="divAdvance">

                                                                <div class="col-md-7 col-md-offset-5 padding-left-36">
                                                                    <input type="checkbox" id="btnAdvance" name="feature" value="Pay Advance" />
                                                                    <label for="btnAdvance" class="lang-pay-advance"><%=Resources.Language.Lbl_invoice_pay_advanced %> </label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="saleButton">
                                                        <span class="saleButtonAlign">
                                                            <input type="button" class="btn btn-info btn-md btnAddCustom printBtnDesign lang-save" value="<%=Resources.Language.Lbl_invoice_save %>" id="btnSaveInvoice" tabindex="7" />
                                                            <input type="button" class="btn btn-warning printBtnDesign btnPrintCustom disNone" value="<%=Resources.Language.Lbl_invoice_draft %>" id="btnDraftInvoice" />
                                                            <input type="button" class="btn btn-primary printBtnDesign lang-update disNone" value="<%=Resources.Language.Lbl_invoice_confirm_sale %>" id="btnConfirmSale" />
                                                            <input type="button" class="btn btn-primary printBtnDesign lang-update disNone" value="<%=Resources.Language.Lbl_invoice_update %>" id="btnUpdateInvice" />
                                                            <input type="button" class="btn btn-danger lang-suspend printBtnDesign disNone" value="<%=Resources.Language.Lbl_invoice_suspended %>" id="btnSuspend" />
                                                            <input type="button" onclick="printInvoiceDiv();" value="<%=Resources.Language.Lbl_invoice_print %>" id="btnPrintInvoice" class="btn btn-warning printBtnDesign btnPrintCustom lang-print-invoice" />
                                                            <input type="button" onclick="printGodownDiv();" value="<%=Resources.Language.Lbl_invoice_challan %>" id="btnPrintChallan" class="btn btn-warning printBtnDesign btnPrintCustom lang-print-challan disNone" />
                                                            <input type="button" onclick="printReceiptDiv();" value="<%=Resources.Language.Lbl_invoice_receipt %>" id="btnPrintReceipt" class="btn btn-warning printBtnDesign btnPrintCustom lang-print-receipt disNone" />
                                                            <input type="button" onclick="printTokenRcptDiv();" value="<%=Resources.Language.Lbl_invoice_token_rcpt %>" id="btnPrintTokenRcpt" class="btn btn-warning printBtnDesign btnPrintCustom lang-print-receipt disNone" />
                                                        </span>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </div>




                                    <div role="tabpanel" class="tab-pane" id="miscCost">
                                        <div class="section3">
                                            <div class="mainItem">
                                                <div class="form-horizontal" onkeydown=" return (event.keyCode != 13) ">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label16" CssClass="lbl col-sm-4 control-label lang-loading" runat="server" Text="<%$Resources:Language, Lbl_invoice_loading %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtLoadingCost" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" CssClass="lbl col-sm-4 control-label lang-unloading" runat="server" Text="<%$Resources:Language, Lbl_invoice_unloading %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtUnloading" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <asp:Label ID="Label17" CssClass="lbl col-sm-4 control-label lang-shipping" runat="server" Text="<%$Resources:Language, Lbl_invoice_shipping %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtShippingCost" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <asp:Label ID="Label18" CssClass="lbl col-sm-4 control-label lang-carrying" runat="server" Text="<%$Resources:Language, Lbl_invoice_carrying %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCarryingCost" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <asp:Label ID="Label26" CssClass="lbl col-sm-4 control-label lang-service" runat="server" Text="<%$Resources:Language, Lbl_invoice__mise_service %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtServiceCharge" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div role="tabpanel" class="tab-pane disNone" id="customer">
                                        <div class="section3">
                                            <div class="mainItem">
                                                <div class="form-horizontal" onkeydown=" return (event.keyCode != 13) ">
                                                    <div class="form-group" id="divCusID" runat="server" visible="false">
                                                        <asp:Label ID="Label11" CssClass="lbl col-sm-4 control-label static-label" runat="server" Text="ID"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:Label ID="lblCusID" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group" id="divCusDue">
                                                        <asp:Label ID="Label12" CssClass="lbl col-sm-4 control-label static-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_due %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                        </div>
                                                    </div>

                                                    <div class="form-group" id="divCusName">
                                                        <asp:Label ID="lblCusName" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_full_name %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCusName" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group" id="divCusPhone">
                                                        <asp:Label ID="lblCusPhone" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_phone %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCusPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtCusPhone" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{11,11}$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group" id="divCusPhone2" runat="server">
                                                        <asp:Label ID="Label7" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_phone2 %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCusPhone2" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCusPhone2" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{11,11}$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_email %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCusEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revCusEmail" runat="server" ControlToValidate="txtCusEmail" ErrorMessage="Invalid!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group" id="divBloodGroup">
                                                        <asp:Label ID="Label9" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_blood_group %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" ID="ddlBloodGroup" CssClass="form-control">
                                                                <asp:ListItem Value="0">None</asp:ListItem>
                                                                <asp:ListItem Value="1">A+</asp:ListItem>
                                                                <asp:ListItem Value="2">A–</asp:ListItem>
                                                                <asp:ListItem Value="3">B+</asp:ListItem>
                                                                <asp:ListItem Value="4">B-</asp:ListItem>
                                                                <asp:ListItem Value="5">O+</asp:ListItem>
                                                                <asp:ListItem Value="6">O-</asp:ListItem>
                                                                <asp:ListItem Value="7">AB+</asp:ListItem>
                                                                <asp:ListItem Value="8">AB-</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group" runat="server" id="divMemberId">
                                                        <asp:Label ID="Label10" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_membar_id %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtMemberId" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <asp:Label ID="lblCusAddress" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_address %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtCusAddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div role="tabpanel" class="tab-pane" id="refferal">
                                        <div class="section3">
                                            <div class="mainItem">
                                                <div class="form-horizontal" onkeydown="return (event.keyCode != 13)">

                                                    <div class="form-group">
                                                        <asp:Label ID="lblRefName" CssClass="lbl col-sm-4 control-label lang-referral-name" runat="server" Text="<%$Resources:Language, Lbl_invoice_full_name %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtRefName" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="lblRefPhone" CssClass="lbl col-sm-4 control-label lang-referral-phone" runat="server" Text="<%$Resources:Language, Lbl_invoice_phone %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtRefPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRefPhone" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{11,11}$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="lblRefAddress" CssClass="lbl col-sm-4 control-label lang-referrel-address" runat="server" Text="<%$Resources:Language, Lbl_invoice_address %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtRefAddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="comment">
                                        <div class="section3">
                                            <div class="mainItem">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblComment" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_invoice_write_notes %>"></asp:Label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtComment" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="8"></asp:TextBox>
                                                            <%-- <asp:RegularExpressionValidator ID="revComment" runat="server" ControlToValidate="txtComment" ErrorMessage="Invalid!" ValidationExpression="^[0-9]{11,11}$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>



        <!-- Due modal -->
        <div class="modal fade" id="CusDueModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title" id="myModalLabel">
                            <asp:Label ID="lblCustomerName" runat="server" Text="Name: NULL"></asp:Label>
                            /
                            <asp:Label ID="lbl" runat="server" Text="ID: "></asp:Label><asp:Label ID="lblCustomerId" runat="server"></asp:Label></h3>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="grdCusDue"
                            runat="server"
                            CssClass="mDtl dtlSize"
                            AutoGenerateColumns="False"
                            EmptyDataText="NO DUE AVAILABLE">
                            <Columns>
                                <asp:TemplateField HeaderText="Invoice No" SortExpression="billNo">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# Bind("billNo") %>' ID="TextBox1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("billNo") %>' ID="lblInvoice"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="giftAmt" HeaderText="Due Amount" SortExpression="giftAmt"></asp:BoundField>
                                <asp:BoundField DataField="entryDate" HeaderText="Entry Date" SortExpression="entryDate" DataFormatString="{0:d}"></asp:BoundField>
                                <asp:TemplateField HeaderText="Pay Now">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPay" runat="server" CssClass="btnPay" CommandName="Select"><i class="fa fa-shopping-cart" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--<asp:SqlDataSource runat="server" ID="dsGrdCusDue" ConnectionString='<%$ ConnectionStrings:dbPOS %>' SelectCommand="SELECT DISTINCT [billNo], [cusID], [giftAmt], [entryDate] FROM [SaleInfo] WHERE ([cusID] = @cusID) AND ([giftAmt] > 0)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblCusID" Name="cusID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>--%>
                    </div>
                    <div class="modal-footer" style="border: none">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" tabindex="-1" role="dialog" id="mymodal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title"><%=Resources.Language.Lbl_invoice_please_confirm %></h4>
                    </div>
                    <div class="modal-body">
                        <p><%=Resources.Language.Lbl_invoice_suspend_notice %> </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal"><%=Resources.Language.Btn_invoice_modal_close %>  </button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

        <!-- Custom Search Design theme -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="overlay overlay-scale" id="divSpecialSearch">
                    <div class="col-md-10 col-md-offset-2 col-sm-10 col-sm-offset-2 col-xs-12">
                        <%--col-sm-offset-4   col-xs-offset-3--%>
                        <button type="button" class="overlay-close col-xs-1">Close</button>

                        <div class="serach-overlay col-xs-11">
                            <div class="form-group search-option" id="divSearchOptionPanel">
                                <input type="checkbox" id="chkSearchOption" runat="server" data-toggle="toggle" data-on="<%$Resources:Language, Lbl_invoice_invoice %>" data-off="<%$Resources:Language, Lbl_invoice_customer %>" data-onstyle="warning" data-offstyle="success" />
                            </div>
                            <div class="form-group ">
                                <asp:TextBox ID="txtBillingNo" runat="server" CssClass="form-control searchbox-design" placeholder="<%$Resources:Language, Lbl_invoice_search___ %>"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <input type="button" class="btn btn-lg btnSearchCustom btn-sale-search" value="<%=Resources.Language.Btn_invoice_search_customer_or_invoice %>" id="btnSearch" />
                            </div>
                        </div>

                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <!-- 

        /********************* Add A New Customer ***********************************/
        -->

    <!-- Cutomer Add -->
    <uc1:CustomerOpt runat="server" ID="CustomerOpt" />



    <!-- Delete Action Modal -->
    <div class="modal fade" id="suspendInvoice" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog " role="document">
            <div class="modal-content suspend-invoice">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title"><%=Resources.Language.Lbl_invoice_are_you_sure_want_to_susspend %> </h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label><%=Resources.Language.Lbl_invoice_invoice_return %> </label>
                        <label id="lblGrandTotalSusspend">0</label>
                    </div>
                    <div class="form-group">
                        <label><%=Resources.Language.Lbl_invoice_total_due_advance %> </label>
                        <label id="lblBalanceSusspend">0</label>
                    </div>
                    <div class="form-group">
                        <label><%=Resources.Language.Lbl_invoice_max_return %>  </label>
                        <label id="lblReturnAmtSusspend">0</label>
                        <input type="text" class="form-control" id="txtSuspendReturn" />
                    </div>

                </div>
                <div class="modal-footer">
                    <a href="JavaScript:void(0);" id="btnSuspendPopUp" class="btn btn-danger"><%=Resources.Language.Btn_invoice_modal_suspend %></a>
                    <a href="JavaScript:void(0);" id="btnLoadNewPopUp" class="btn btn-primary"><%=Resources.Language.Btn_invoice_modal_suspend_as_new_invoice %></a>
                    <a href="JavaScript:void(0);" data-dismiss="modal" aria-hidden="true" class="btn btn-default"><%=Resources.Language.Btn_invoice_modal_close %></a>
                </div>
            </div>
        </div>
    </div>



    <!-- Subscription Action Modal -->
    <div class="modal fade expiry-modal" id="expiryModal" tabindex="-1" role="dialog" aria-labelledby="expiryModalLebel">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><%=Resources.Language.Lbl_invoice_notification %></h4>
                </div>
                <div class="modal-body">

                    <b><%=Resources.Language.Lbl_invoice_subs_exp %></b>
                    <p><%=Resources.Language.Lbl_invoice_pay_subs %></p>

                </div>
                <div class="modal-footer">
                    <a href="/admin/subscription" class="btn btn-primary"><%=Resources.Language.Lbl_invoice_Subscription_now %></a>
                </div>
            </div>
        </div>
    </div>


    <!-- Staff/refered by -->
    <uc1:StaffOpt runat="server" ID="StaffOpt" />



    <asp:HiddenField ID="lblGenBillNo" runat="server" />



    <script src="<%= ResolveUrl("~/Js/bootstrap-toggle.min.js") %>"></script>

    <script src="<%= ResolveUrl("~/Js/Search/classie.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Js/Search/custom-search.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Js/select2.min.js") %>"></script>

    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-accounting.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-main.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-print.js?v=1.1.11") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-item-search.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-package.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-invoice-search.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-imei.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-service.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-offer.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-variant.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-product.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-stock.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-upsert.js?v=1.1.11") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-validation.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-customer-upsert.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-invoice-suspend.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-promotion.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-discount.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-date-edit.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-next-pay-date.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-installment.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-reset.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-advance.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-cart.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-expiration.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-tabindex.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-suspend-new-invoice.js?v=1.1.10") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/CustomerBundle/Script/customer-upsert.js?v=1.1.10") %>"></script>
    

    <script>
        // Intialize global variable      
        var iCounter = 0;

        $(window).on("load", function () {
            activeModule = "sale";
        });

        var Select_a_customer = "<% =Resources.Language.Lbl_invoice_select_a_customer %>";
    </script>
    
  
    
    



</asp:Content>


