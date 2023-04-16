<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="MetaPOS.Admin.SettingBundle.View.Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Configuration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SettingBundle/Content/Setting.css?v=0.007") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SettingBundle/Content/Setting-responsive.css?v=0.007") %>" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div class="section">
                <h2 class="sectionBreadcrumb">Configuration</h2>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">

            <div class="section setting-section-height">

                <div class="tabbable tabs-left">
                    <ul class="nav nav-tabs setting-nav-tabs">
                        <li role="presentation" class="active"><a href="#general" aria-controls="general" role="tab" data-toggle="tab">General</a></li>
                        <li role="presentation"><a href="#sale" aria-controls="sale" role="tab" data-toggle="tab">Invoice</a></li>
                        <li role="presentation"><a href="#invoicePrint" aria-controls="invoicePrint" role="tab" data-toggle="tab">Invoice Print</a></li>
                        <li role="presentation"><a href="#customer" aria-controls="customer" role="tab" data-toggle="tab">Customer</a></li>
                        <li role="presentation"><a href="#stock" aria-controls="stock" role="tab" data-toggle="tab">Inventory</a></li>
                        <li role="presentation"><a href="#purchase" aria-controls="purchase" role="tab" data-toggle="tab">Purchase</a></li>
                        <li role="presentation"><a href="#ecommerce" aria-controls="ecommerce" role="tab" data-toggle="tab">Ecommerce</a></li>
                        <li role="presentation"><a href="#promotion" aria-controls="promotion" role="tab" data-toggle="tab">Promotion</a></li>
                        <li role="presentation"><a href="#report" aria-controls="report" role="tab" data-toggle="tab">Report</a></li>
                    </ul>
                </div>


                <div class="tab-content">

                    <div role="tabpanel" class="tab-pane active" id="general">


                        <div class="setting-section">
                            <asp:Label ID="Label41" CssClass="lbl  control-label setting-section-title" runat="server" Text="Time Zone"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                        <p></p>
                                    </div>
                                    <div id="divTimeZone" class="setting-action">
                                        <asp:RadioButtonList ID="timeZone" runat="server" CssClass="">
                                            <asp:ListItem Value="Bangladesh Standard Time">Bangladesh Standard Time</asp:ListItem>
                                            <asp:ListItem Value="Arabian Standard Time">Arabian Standard Time</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label116" CssClass="addons-price">50</asp:Label></b>

                                </div>
                            </div>

                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label95" CssClass="lbl control-label setting-section-title" runat="server" Text="D Branding"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                        <p>on your custom Develoment branding logo </p>
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="dbranding" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label118" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>

                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label111" CssClass="control-label setting-section-title" runat="server" Text="Separate Store"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isSeparateStore" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label119" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>

                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label238" CssClass="control-label setting-section-title" runat="server" Text="Language"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:DropDownList ID="language" runat="server" CssClass="form-control ddl-section">
                                            <asp:ListItem Value="en">English</asp:ListItem>
                                            <asp:ListItem Value="bn">Bangla</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section"> Free </b>
                                    <b class="addons-price-section  disNone">৳<asp:Label runat="server" ID="Label239" CssClass="addons-price">0</asp:Label></b>
                                </div>
                            </div>
                        </div>
                        
                        <div class="setting-section">
                            <asp:Label ID="Label241" CssClass="control-label setting-section-title" runat="server" Text="Business Type"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:DropDownList ID="businessType" runat="server" CssClass="form-control ddl-section">
                                            <asp:ListItem Value="retail">Retail Shop</asp:ListItem>
                                            <asp:ListItem Value="gadget">Gadget & Accessories</asp:ListItem>
                                            <asp:ListItem Value="fashion">Fashion & Beauty</asp:ListItem>
                                            <asp:ListItem Value="electronics">Electronics & Appliances</asp:ListItem>
                                            <asp:ListItem Value="sports">Sports & Accessories</asp:ListItem>
                                            <asp:ListItem Value="food">Food & Groceres</asp:ListItem>
                                            <asp:ListItem Value="resturent">Resturent</asp:ListItem>
                                            <asp:ListItem Value="pharma">Pharmacy</asp:ListItem>
                                            <asp:ListItem Value="diagnostic">Diagnostic Center</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section"> Free </b>
                                    <b class="addons-price-section  disNone">৳<asp:Label runat="server" ID="Label242" CssClass="addons-price">0</asp:Label></b>
                                </div>
                            </div>
                        </div>

                    </div>




                    <div role="tabpanel" class="tab-pane" id="sale">

                        <div class="setting-section">
                            <asp:Label ID="Label117" CssClass="control-label setting-section-title" runat="server" Text="Invoice Paper Size"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:RadioButtonList ID="SalesPrintPageType" runat="server">
                                            <asp:ListItem Value="0">Small</asp:ListItem>
                                            <asp:ListItem Value="1">Full</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label120" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>




                        <div class="setting-section">
                            <asp:Label ID="Label4" CssClass="control-label setting-section-title" runat="server" Text="Invoice Paper Size Option"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                        are you want to show invoice option in invoice page
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayPaperSizeSelectOption" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label121" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>



                        <div class="setting-section">
                            <asp:Label ID="Label77" CssClass="control-label setting-section-title" runat="server" Text="Save & Print at a Time"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="printWithAutoSave" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label122" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label22" CssClass="control-label setting-section-title" runat="server" Text="Print without Previewing"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="SalesPrintingTime" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label123" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label11" CssClass="control-label setting-section-title" runat="server" Text="Edit Invoice Date"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="billDateEdit" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label125" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>



                        <div class="setting-section">
                            <asp:Label ID="Label14" CssClass="control-label setting-section-title" runat="server" Text="Edit Product Unit Price"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="salesProductEditable" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label126" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div runat="server" visible="False">
                            <asp:Label ID="Label7" CssClass="lbl col-sm-3 control-label" runat="server" Text="Invoice Card Discount"></asp:Label>
                            <asp:RadioButtonList ID="cardDiscount" runat="server" CssClass="radio-inline">
                                <asp:ListItem Value="0.00">0.0</asp:ListItem>
                                <asp:ListItem Value="1.60">1.6</asp:ListItem>
                                <asp:ListItem Value="10.00">10.0</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label2" CssClass="control-label setting-section-title" runat="server" Text="Show Money Reciept"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="receipt" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label127" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label17" CssClass="control-label setting-section-title" runat="server" Text="Show Payment History"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="printPayHistory" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label130" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label8" CssClass="control-label setting-section-title" runat="server" Text="Print Invoice in"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:RadioButtonList ID="isUnicode" runat="server" CssClass="radio-inline">
                                            <asp:ListItem Value="0">English</asp:ListItem>
                                            <asp:ListItem Value="1">Bangla</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label132" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label133" CssClass="control-label setting-section-title" runat="server" Text="Show Pay Date"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="paydate" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label134" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


<%--                        <div class="setting-section">
                            <asp:Label ID="Label31" CssClass="control-label setting-section-title" runat="server" Text="Print by Special Date"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="cusDateWiseSearch" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label40" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>--%>


                        <div class="setting-section">
                            <asp:Label ID="Label29" CssClass="control-label setting-section-title" runat="server" Text="Enable Qty Search"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isSearchQty" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label135" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label49" CssClass="control-label setting-section-title" runat="server" Text="Enable Misc. Cost"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isMiscCost" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label136" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label50" CssClass="control-label setting-section-title" runat="server" Text="Enable Godown Invoice"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:RadioButtonList ID="isGodownInvoice" runat="server" CssClass="">
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                            <asp:ListItem Value="1">One Invoice</asp:ListItem>
                                            <asp:ListItem Value="2">Two Invoice</asp:ListItem>
                                            <asp:ListItem Value="3">Three Invoice</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label137" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label51" CssClass="control-label setting-section-title" runat="server" Text="Enable Cart Supplier"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isCartSupplier" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label138" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label52" CssClass="control-label setting-section-title" runat="server" Text="Enable Cart Category"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isCartCategory" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label139" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label53" CssClass="control-label setting-section-title" runat="server" Text="Display Cart Buy Price"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayCartBuyPrice" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label140" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label64" CssClass="control-label setting-section-title" runat="server" Text="Display Cart Wholesale Price"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayCartWholesalePrice" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label141" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label65" CssClass="control-label setting-section-title" runat="server" Text="Display Cart Retail Price"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayCartRetailPrice" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label142" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label66" CssClass="control-label setting-section-title" runat="server" Text="Action Invoice"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isGoAction" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label143" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label56" CssClass="control-label setting-section-title" runat="server" Text="Save Auto Sales Person"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="autoSalesPerson" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label144" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label62" CssClass="control-label setting-section-title" runat="server" Text="Display discount"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayDiscountInSale" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label145" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label63" CssClass="control-label setting-section-title" runat="server" Text="Display Total Qty"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayCartTotalQty" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label146" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label67" CssClass="control-label setting-section-title" runat="server" Text="Display VAT"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isVatSale" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label147" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label81" CssClass="control-label setting-section-title" runat="server" Text="Display Interest"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayInterest" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label150" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label84" CssClass="control-label setting-section-title" runat="server" Text="Display Guarantor"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayGuarantor" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label151" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label88" CssClass="control-label setting-section-title" runat="server" Text="Display instalment send message"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayInstallmentMessage" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label153" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label104" CssClass="control-label setting-section-title" runat="server" Text="Product Search By"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="searchBy" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label154" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label21" CssClass="control-label setting-section-title" runat="server" Text="Active Service"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayService" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label155" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label92" CssClass="control-label setting-section-title" runat="server" Text="Directly misc. cost save to expense"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="saveMiscCostToExpense" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label156" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label107" CssClass="control-label setting-section-title" runat="server" Text="After search stay product name"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="stayAfterSearchProduct" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label157" CssClass="addons-price">15</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label108" CssClass="control-label setting-section-title" runat="server" Text="Multi payment"></asp:Label>
                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isMultipay" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label158" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label74" CssClass="control-label setting-section-title" runat="server" Text="Display Extra Discount"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayExtraDiscount" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label220" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label231" CssClass="control-label setting-section-title" runat="server" Text="Display Referal Option"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isReferredBy" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label232" CssClass="addons-price">30</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label233" CssClass="control-label setting-section-title" runat="server" Text="Auto Select Search By"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:RadioButtonList ID="autoSelectSearchBy" runat="server" CssClass="">
                                            <asp:ListItem Value="0">Product</asp:ListItem>
                                            <asp:ListItem Value="1">Package</asp:ListItem>
                                            <asp:ListItem Value="2">Service</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label234" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label87" CssClass="control-label setting-section-title" runat="server" Text="Display Draft / Quotation"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayDraft" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section">৳<asp:Label runat="server" ID="Label240" CssClass="addons-price">30</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label13" CssClass="control-label setting-section-title" runat="server" Text="Token"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="token" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label178" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                    </div>




                    <div role="tabpanel" class="tab-pane" id="invoicePrint">

                        <div class="setting-section">
                            <asp:Label ID="Label110" CssClass="control-label setting-section-title" runat="server" Text="Try Beta Invoice Print"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isBetaInvoicePrint" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label159" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label82" CssClass="control-label setting-section-title" runat="server" Text="Print IMEI"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isPrintImei" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label160" CssClass="addons-price">140</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label54" CssClass="control-label setting-section-title" runat="server" Text="Print Warranty Date"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isPrintWarranty" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label161" CssClass="addons-price">140</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label55" CssClass="control-label setting-section-title" runat="server" Text="Print Supplier"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isPrintSupplier" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label162" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label57" CssClass="control-label setting-section-title" runat="server" Text="Print Category"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isPrintCategory" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label163" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label58" CssClass="control-label setting-section-title" runat="server" Text="Print Change Amount"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="printChangeAmt" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label164" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label71" CssClass="control-label setting-section-title" runat="server" Text="Print Return Qty"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="printReturnQty" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label165" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label72" CssClass="control-label setting-section-title" runat="server" Text="Invoice in Word"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isInvoiceWord" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label166" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label73" CssClass="control-label setting-section-title" runat="server" Text="Print VAT"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isVatPrint" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label167" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label76" CssClass="control-label setting-section-title" runat="server" Text="Display Logo Invoice Print"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayLogoPrint" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label168" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label98" CssClass="control-label setting-section-title" runat="server" Text="Display Brand Name - Print"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayBrandNamePrint" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label169" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label99" CssClass="control-label setting-section-title" runat="server" Text="Display Discount - Print"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayDiscountPrint" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label170" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label106" CssClass="control-label setting-section-title" runat="server" Text="Display Discount - Print"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <div class="form-group">
                                            <asp:TextBox ID="smallPrintPaperWidth" runat="server" CssClass="form-control"></asp:TextBox>
                                            <div class="btn-group-recomendation">
                                                <input id="btn25mm" value="25" type="button" class="btn btn-default" />
                                                <input id="btn50mm" value="50" type="button" class="btn btn-default" />
                                                <input id="btn80mm" value="80" type="button" class="btn btn-default" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label171" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label10" CssClass="control-label setting-section-title" runat="server" Text="Show Paid Watermark"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isPaidWatermark" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label128" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label16" CssClass="control-label setting-section-title" runat="server" Text="Show Paid Watermark"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:RadioButtonList ID="isCategoryProduct" runat="server" CssClass="">
                                            <asp:ListItem Value="1">Category</asp:ListItem>
                                            <asp:ListItem Value="0">Product</asp:ListItem>
                                            <asp:ListItem Value="2">Category and Product</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label129" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>
                        
                        <div class="setting-section">
                            <asp:Label ID="Label245" CssClass="control-label setting-section-title" runat="server" Text="Print by Serial No"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isDisplaySerialNo" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label124" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>
                        
                        

                        
                        <%--
                            
                            
                            <div class="setting-section">
                            <asp:Label ID="Label31" CssClass="control-label setting-section-title" runat="server" Text="Print by Special Date"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isSerialNo" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label40" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>
                            
                            
                            
                            
                            
                            
                            
                            
                            --%>
                        
                        

                        
                        <div class="setting-section">
                            <asp:Label ID="Label31" CssClass="control-label setting-section-title" runat="server" Text="Print by Special Date"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="cusDateWiseSearch" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label40" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>

                    </div>




                    <div role="tabpanel" class="tab-pane" id="stock">

                        <div class="setting-section">
                            <asp:Label ID="Label102" CssClass="control-label setting-section-title" runat="server" Text="Barcode Size"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:RadioButtonList ID="barCodeType" runat="server">
                                            <asp:ListItem Value="0">Standard</asp:ListItem>
                                            <asp:ListItem Value="1">Medium</asp:ListItem>
                                            <asp:ListItem Value="2">Small</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label172" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label3" CssClass="control-label setting-section-title" runat="server" Text="Show Barcode Generator"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="stockBarCodeScanning" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label173" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label19" CssClass="control-label setting-section-title" runat="server" Text="Allow Same Product Name"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isExistProductName" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label181" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label36" CssClass="control-label setting-section-title" runat="server" Text="Show Image Upload"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="upload" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label185" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <%--<div class="setting-section">
                            <asp:Label ID="Label37" CssClass="control-label setting-section-title" runat="server" Text="Show Shipment Status"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="shipment" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label186" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>--%>


                        <div class="setting-section">
                            <asp:Label ID="Label42" CssClass="control-label setting-section-title" runat="server" Text="Recived Date enable"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isRecivedDate" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label189" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label43" CssClass="control-label setting-section-title" runat="server" Text="Expiry Date enable"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isExpiryDate" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label190" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label46" CssClass="control-label setting-section-title" runat="server" Text="Batch no enable"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isBatchNo" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label193" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label47" CssClass="control-label setting-section-title" runat="server" Text="Show Store list"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isWarehouse" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label194" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label48" CssClass="control-label setting-section-title" runat="server" Text="Purchase Payment"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isPurchasePayment" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label195" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label60" CssClass="control-label setting-section-title" runat="server" Text="Display Buy Price"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayBuyPrice" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label196" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label61" CssClass="control-label setting-section-title" runat="server" Text="Gas Cylinder Avaiable"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isGasCylinderAvail" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label197" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label78" CssClass="control-label setting-section-title" runat="server" Text="Enable Transfer"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayTransfer" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label198" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label86" CssClass="control-label setting-section-title" runat="server" Text="Print Barcode For"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <asp:RadioButtonList ID="printBarcodeFor" runat="server">
                                            <asp:ListItem Value="Product">Product Name</asp:ListItem>
                                            <asp:ListItem Value="Category">Category Name</asp:ListItem>
                                            <asp:ListItem Value="Company">Company Name</asp:ListItem>
                                            <asp:ListItem Value="Sku">SKU</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label199" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label83" CssClass="control-label setting-section-title" runat="server" Text="Display Company Price"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayComPrice" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label200" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label94" CssClass="control-label setting-section-title" runat="server" Text="Show Buy Price For User"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayBuyPriceForUser" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label202" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label96" CssClass="control-label setting-section-title" runat="server" Text="Active Delivery Status"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isDeliveryStatus" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label203" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label97" CssClass="control-label setting-section-title" runat="server" Text="Display Print All Stock For User"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayPrintAllStock" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label204" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label100" CssClass="control-label setting-section-title" runat="server" Text="No Barcode Generate"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="noBarcode" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label205" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label115" CssClass="control-label setting-section-title" runat="server" Text="Show barcode in Stock"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isBarcodeShowInStock" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label230" CssClass="addons-price">0.0</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label237" CssClass="control-label setting-section-title" runat="server" Text="Load default products to Invoice Cart"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <asp:TextBox ID="CartDefaultProduct" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-4 text-right">
                                </div>
                            </div>
                        </div>

                    </div>



                    <div role="tabpanel" class="tab-pane" id="purchase">

                        <div class="setting-section">
                            <asp:Label ID="Label101" CssClass="control-label setting-section-title" runat="server" Text="Display Supplier Schedule Payment"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displaySchedulePayment" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label206" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label68" CssClass="control-label setting-section-title" runat="server" Text="Display Supplier Track Received"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displaySupplierReceived" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label207" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label69" CssClass="control-label setting-section-title" runat="server" Text="Auto Generate barcode type"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="generateBardCodeType" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label208" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label70" CssClass="control-label setting-section-title" runat="server" Text="Display Variant"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayVariant" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label209" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label35" CssClass="control-label setting-section-title" runat="server" Text="Show Manufacturer"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="manufacturer" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label184" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label15" CssClass="control-label setting-section-title" runat="server" Text="Show Warning Qty"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isWarningQty" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label179" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label44" CssClass="control-label setting-section-title" runat="server" Text="Serial no enable"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isSerialNo" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label191" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label39" CssClass="control-label setting-section-title" runat="server" Text="Show Unit Measurement"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isUnit" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label188" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label6" CssClass="control-label setting-section-title" runat="server" Text="Show Warranty"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="warranty" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label176" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label45" CssClass="control-label setting-section-title" runat="server" Text="Supplier Commission enable"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isSupplierCom" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label192" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label12" CssClass="control-label setting-section-title" runat="server" Text="Show IMEI"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="imei" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label177" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label75" CssClass="control-label setting-section-title" runat="server" Text="Engine Number"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isEngineNumber" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label148" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label80" CssClass="control-label setting-section-title" runat="server" Text="Cecish Number"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isCecishNumber" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label149" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label5" CssClass="control-label setting-section-title" runat="server" Text="Show Sku"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="sku" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label174" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label18" CssClass="control-label setting-section-title" runat="server" Text="Show Wholesale Option"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isWholeSeller" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label180" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label93" CssClass="control-label setting-section-title" runat="server" Text="Show Location"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayLocation" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label201" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label9" CssClass="control-label setting-section-title" runat="server" Text="Show Tax"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="tax" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label175" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label38" CssClass="control-label setting-section-title" runat="server" Text="Show Product Size"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="size" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label187" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label37" CssClass="control-label setting-section-title" runat="server" Text="Show Shipment Status"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="shipment" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label186" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label32" CssClass="control-label setting-section-title" runat="server" Text="Import Products"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isImport" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label182" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label34" CssClass="control-label setting-section-title" runat="server" Text="Custom Field Attribute"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="customField" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label183" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>

                    </div>




                    <div role="tabpanel" class="tab-pane" id="ecommerce">

                        <div class="setting-section">
                            <asp:Label ID="Label109" CssClass="control-label setting-section-title" runat="server" Text="Show Quotation Option"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="quotation" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label210" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label23" CssClass="control-label setting-section-title" runat="server" Text="Show Ecommerce API"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="apiEcomm" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label211" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label24" CssClass="control-label setting-section-title" runat="server" Text="Show Quotation Notification"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="quotationNotification" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label212" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>

                    </div>




                    <div role="tabpanel" class="tab-pane" id="customer">

                        <div class="setting-section">
                            <asp:Label ID="Label28" CssClass="control-label setting-section-title" runat="server" Text="Show Blood Group"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="bloodGroup" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label213" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label25" CssClass="control-label setting-section-title" runat="server" Text="Show Additional Contact"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="phone2" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label214" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label26" CssClass="control-label setting-section-title" runat="server" Text="Show Member Id"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="membershipId" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label215" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label27" CssClass="control-label setting-section-title" runat="server" Text="Enable Opening Due"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isOpeningDue" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label216" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label33" CssClass="control-label setting-section-title" runat="server" Text="Enable Due Adjustment"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isDueAdjustment" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label217" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label59" CssClass="control-label setting-section-title" runat="server" Text="Show Advance"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isAdvanced" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label218" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label20" CssClass="control-label setting-section-title" runat="server" Text="Customer Validation Required"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isCustomerRequired" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label219" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>



                        <div class="setting-section">
                            <asp:Label ID="Label85" CssClass="control-label setting-section-title" runat="server" Text="Display Search Customer"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displaySearchCustomer" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label221" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label90" CssClass="control-label setting-section-title" runat="server" Text="Display Add Customer"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayAddCustomer" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label222" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label91" CssClass="control-label setting-section-title" runat="server" Text="Display Designation"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isCusDesignation" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label223" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label235" CssClass="control-label setting-section-title" runat="server" Text="Customer Summary Export"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isCustomerSummaryExport" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label236" CssClass="addons-price">00</asp:Label></b>
                                </div>
                            </div>
                        </div>

                        <div class="setting-section">
                            <asp:Label ID="Label89" CssClass="control-label setting-section-title" runat="server" Text="Enable Instalment"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayInstalment" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label152" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                    </div>




                    <div role="tabpanel" class="tab-pane" id="promotion">

                        <div class="setting-section">
                            <asp:Label ID="Label112" CssClass="control-label setting-section-title" runat="server" Text="Send Invoice To Customer By SMS"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="sendInvoiceBySms" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label224" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label79" CssClass="control-label setting-section-title" runat="server" Text="Send Invoice to business owner By SMS"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isSendSmsOwnerNumber" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label225" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label243" CssClass="control-label setting-section-title" runat="server" Text="Send Invoice Mail"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="sMail" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label244" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label30" CssClass="control-label setting-section-title" runat="server" Text="Show Offer"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="offer" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label131" CssClass="addons-price">50</asp:Label></b>
                                </div>
                            </div>
                        </div>

                    </div>




                    <div role="tabpanel" class="tab-pane" id="report">

                        <div class="setting-section">
                            <asp:Label ID="Label113" CssClass="control-label setting-section-title" runat="server" Text="Display Delivery Status"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="displayDeliveryStatus" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label226" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label103" CssClass="control-label setting-section-title" runat="server" Text="Invoice wise Search Product"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="invoiceWiseSearchProduct" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label227" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label105" CssClass="control-label setting-section-title" runat="server" Text="Show Last Qty"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isLastQty" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label228" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                        <div class="setting-section">
                            <asp:Label ID="Label114" CssClass="control-label setting-section-title" runat="server" Text="Show Balance Qty"></asp:Label>

                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="setting-desc">
                                    </div>
                                    <div class="setting-action">
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="isBalanceQty" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <b class="addons-price-section ">৳<asp:Label runat="server" ID="Label229" CssClass="addons-price">40</asp:Label></b>
                                </div>
                            </div>
                        </div>


                    </div>




                </div>

            </div>

        </div>
    </div>


    <script src="<%= ResolveUrl("~/Admin/SettingBundle/Script/setting-main.js?v=0.012") %>"></script>
    
    <asp:HiddenField ID="lblHiddenBranchId" runat="server"/>

    <script>

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            var head = document.getElementsByTagName("head")[0];
            var script = document.createElement('script');
            script.src = '/Admin/SettingBundle/Script/setting-main.js?v=0.012';
            script.type = 'text/javascript';
            head.appendChild(script);


        });

    </script>


    <script>
        activeModule = "settings";
    </script>


</asp:Content>
