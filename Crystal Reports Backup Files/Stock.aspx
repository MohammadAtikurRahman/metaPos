<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.Stock" %>

<%@ Import Namespace="System.Activities.Statements" %>
<%@ Import Namespace="System.ComponentModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Stock Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Stock.css?v=0.0010") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Stock-responsive.css?v=0.006") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <script type="text/javascript">
        function EndRequest(sender, args) {
            $("input").val();
        }

        function CheckGrdSelectAll(oCheckbox) {
            var i;
            var grdStock = document.getElementById("<%=grdStock.ClientID %>");
            for (i = 1; i < grdStock.rows.length; i++) {
                grdStock.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }


        function tooltipview() {
            $('[id*=grdStock] tr').each(function () {
                $(this).find("td").each(function () {
                    $(this).tooltip({
                        title: $(this).html(),
                        placement: "right",
                        position: {
                            my: "center bottom-20",
                            at: "center top",
                            using: function (position, feedback) {
                                $(this).css(position);
                                $("<div>")
                                    .addClass("arrow-tooltip")
                                    .addClass(feedback.vertical)
                                    .addClass(feedback.horizontal)
                                    .appendTo(this);
                            }
                        }
                    });
                });
            });
        }

    </script>



    <asp:Label runat="server" ID="lblStoreId" CssClass="disNone"></asp:Label>

    <div class="body-heading disNone">
        <div class="dropdown">
            <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                Shortcuts <span class="caret"></span>
            </button>
            <ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenu1">
                <li>
                    <asp:Button ID="btnAddSupplier" CssClass="btn btn-sm btn-link" runat="server" Text="Add Supplier" OnClick="btnAddSupplier_Click" />
                </li>
                <li>
                    <asp:Button ID="btnAddCategory" CssClass="btn btn-sm btn-link" runat="server" Text="Add Category" OnClick="btnAddCategory_Click" />
                </li>
                <li class="disNone">
                    <asp:Button ID="btnAddProduct" CssClass="btn btn-sm btn-link" runat="server" Text="Add Subcategory" OnClick="btnAddProduct_Click" />
                </li>
                <li class="disNone">
                    <asp:Button ID="btnAddSize" CssClass="btn btn-sm btn-link" runat="server" Text="Add Size" OnClick="btnAddSize_Click" />
                </li>
            </ul>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 stockMargin-top">
            <div class="section stock-back-section">
                <h2 class="sectionBreadcrumb sectionBreadcrumb-stock lang-inventory-stock" id="lblHeaderTitle" runat="server">Stock</h2>

                <a href='javascript:location.reload();' class="printPageHeaderArrow stock-back-button pull-right sectionBreadcrumb sectionBreadcrumb-stock" id="actionRefresh" runat="server" visible="False"><i class="fa fa-2x fa-arrow-left"></i>
                    <h3 class=" stock-back-text" id="lblHeaderTitleInnerAction" runat="server">Stock</h3>
                </a>

            </div>
        </div>
    </div>


    <div class="modal fade" id="modalStock" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title">
                        <span class="glyphicon glyphicon-gift glyIconPosition"></span>
                        <asp:Label ID="lblProductCodeDtl" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblStoreIdDtl" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label>
                    </h3>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblStockTotal" runat="server" Text="0.00" Visible="false"></asp:Label>
                    <asp:DetailsView ID="dtlStock" runat="server" CssClass="mDtl dtlSize" AutoGenerateRows="False" DataKeyNames="Id"
                         OnDataBound="dtlStock_OnDataBound">
                        <Fields>
                            <asp:TemplateField HeaderText="Id" InsertVisible="False" SortExpression="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Image" SortExpression="image">
                                <ItemTemplate>
                                    <asp:Image runat="server" ImageUrl='<%# String.Format("~/Img/Product/{0}", ProcessMyDataItem(Eval("image"))) %>' class="prodViewImg" ID="Image1"></asp:Image>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Code" SortExpression="prodCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblProdCode" runat="server" Text='<%# Bind("prodCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Supplier" SortExpression="supCompany">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupCompany" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Category" SortExpression="catName">
                                <ItemTemplate>
                                    <asp:Label ID="lblCatName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manufacturer" SortExpression="manufacturerName">
                                <ItemTemplate>
                                    <asp:Label ID="lblManufacturer" runat="server" Text='<%# Bind("manufacturerName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product" SortExpression="prodName" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("prodName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Stock Qty" SortExpression="qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("qty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warning Qty" SortExpression="warningQty">
                                <ItemTemplate>
                                    <asp:Label ID="lblWarningQty" runat="server" Text='<%# Bind("warningQty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Buy Price" SortExpression="bPrice" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBPrice" runat="server" Text='<%# Bind("bPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Wholesale Price" SortExpression="dealerPrice">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerPrice" runat="server" Text='<%# Bind("dealerPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Sell Price" SortExpression="sPrice">
                                <ItemTemplate>
                                    <asp:Label ID="lblSPrice" runat="server" Text='<%# Bind("sPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Wholesale Total" SortExpression="wholesaleTotal">
                                <ItemTemplate>
                                    <asp:Label ID="lblWholesaleTotal" runat="server" Text='0.00'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sale Total" SortExpression="saleTotal">
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleTotal" runat="server" Text='0.00'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Buy Total" SortExpression="buyTotal" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblBuyTotal" runat="server" Text='0.00'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Size" SortExpression="size">
                                <ItemTemplate>
                                    <asp:Label ID="lblSize" runat="server" Text='<%# Bind("size") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tax" SortExpression="tax">
                                <ItemTemplate>
                                    <asp:Label ID="lblTax" runat="server" Text='<%# Bind("tax") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SKU" SortExpression="sku">
                                <ItemTemplate>
                                    <asp:Label ID="lblSku" runat="server" Text='<%# Bind("sku") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warranty" SortExpression="warranty">
                                <ItemTemplate>
                                    <asp:Label ID="lblWarranty" runat="server" Text='<%# Bind("warranty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IMEI" SortExpression="imei">
                                <ItemTemplate>
                                    <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("imei") %>' Style="word-wrap: normal; word-break: break-all;"></asp:Label>--%>
                                    <asp:Label ID="lblImei" value="" runat="server" Text='<%# Bind("imei") %>' Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Weight" SortExpression="weight">
                                <ItemTemplate>
                                    <asp:Label ID="lblWeight" runat="server" Text='<%# Bind("weight") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="receivedDate" HeaderText="Received Date"
                                DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="expiryDate" HeaderText="Expiry Date"
                                DataFormatString="{0:dd-MMM-yyyy}" />

                            <asp:TemplateField HeaderText="Batch No" SortExpression="batchNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblBatchNo" runat="server" Text='<%# Bind("batchNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Serial No" SortExpression="serialNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblSerialNo" runat="server" Text='<%# Bind("serialNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shipment Status" SortExpression="shipmentStatus">
                                <ItemTemplate>
                                    <asp:Label ID="lblShipmentStatus" runat="server" Text='<%#  ((string) Eval("shipmentStatus").ToString() == "0") ? "None" : ((string) Eval("shipmentStatus").ToString() == "1") ? "Stocked" : ((string) Eval("shipmentStatus").ToString() == "2") ? "Moving" : ((string) Eval("shipmentStatus").ToString() == "3") ? "Shipped" : " " %>'></asp:Label>
                                    <%--<asp:Label ID="lblShipmentStatus" runat="server" Text='<%#  ((string)Eval("shipmentStatus") == "0") ? "~/images/arrow_yes.png" : ((string)Eval("shipmentStatus") == "1") ? "~/images/edit_msg.png" : "~/images/arrow_down.png" %>')></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit" SortExpression="unitName">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("unitName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commission" SortExpression="commission">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplierCommission" runat="server" Text='<%# Bind("commission") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes" SortExpression="notes">
                                <ItemTemplate>
                                    <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("notes") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:DetailsView>
                    <%--<asp:SqlDataSource ID="dsDtlStock" runat="server" ConnectionString="<%$ ConnectionStrings:dbPOS %>"
                        SelectCommand=" "
                        UpdateCommand="UPDATE [StockInfo] SET [supCompany] = @supCompany, [catName] = @catName, [qty] = @qty, [bPrice] = @bPrice, [sPrice] = @sPrice, [size] = @size, [stockTotal] = @lblStockTotal WHERE [Id] = @Id">
                        <UpdateParameters>
                            <asp:Parameter Name="prodCode" Type="String" />
                            <asp:Parameter Name="prodName" Type="String" />
                            <asp:Parameter Name="prodDescr" Type="String" />
                            <asp:Parameter Name="supCompany" Type="String" />
                            <asp:Parameter Name="catName" Type="String" />
                            <asp:Parameter Name="qty" Type="Int16" />
                            <asp:Parameter Name="bPrice" Type="Decimal" />
                            <asp:Parameter Name="sPrice" Type="Decimal" />
                            <asp:Parameter Name="weight" Type="Decimal" />
                            <asp:Parameter Name="size" Type="String" />
                            <asp:Parameter Name="discount" Type="Decimal" />
                            <asp:Parameter Name="stockTotal" Type="Decimal" />
                            <asp:Parameter DbType="Date" Name="entryDate" />
                            <asp:Parameter Name="Id" Type="Int32" />
                            <asp:ControlParameter ControlID="lblStockTotal" Name="lblStockTotal" PropertyName="Text" Type="String" DefaultValue="0" />
                        </UpdateParameters>
                    </asp:SqlDataSource>--%>
                </div>
                <div style="clear: both"></div>
                <div class="modal-footer" runat="server" id="modalFooter">
                    <button type="submit" class="btn btn-lg btnCloseCustom closeModal" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>



    <div class="row addtSection">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-15">
            <div class="col-md-4 col-sm-12 col-xs-12 text-left form-group">
                <a href="~/admin/bulk-stock?page=unit" id="btnUnitStock" runat="server" class="btn btn-success btnAddCustom btnAddOpt lang-inventory-add-product">Add a Product</a>
                <a href="~/admin/barcode" id="btnPrintbarcodeAnotherPage" runat="server" class="btn btn-info lang-inventory-print-your-barcode" target="_blank">Print Your Barcode</a>
                <asp:LinkButton ID="btnAddSingleStock" runat="server" class="btn btn-success btnAddCustom btnAddOpt  disNone" OnClick="btnAddSingleStock_Click"><i class="fa fa-plus icon-middle" aria-hidden="true"></i> <span class="">Unit Stock</span></asp:LinkButton>
                <asp:LinkButton ID="btnAddMulStock" runat="server" CssClass="btn btn-success btnAddCustom btnAddOpt disNone" OnClick="btnAddMulStock_Click"><i class="fa fa-plus icon-middle" aria-hidden="true"></i> <span class="">Bulk Stock</span></asp:LinkButton>
            </div>
            <div class="col-md-8 col-xs-12 col-sm-12 text-right text-left form-inline">
                <div class="form-group margin-bottom-10">
                    <asp:DropDownList ID="ddlGrdCols" runat="server" OnSelectedIndexChanged="ddlGrdCols_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="-">-- Select --</asp:ListItem>
                        <asp:ListItem Value="0">Select All</asp:ListItem>
                        <asp:ListItem Value="1">Buy Total</asp:ListItem>
                        <asp:ListItem Value="2">Wholesale Total</asp:ListItem>
                        <asp:ListItem Value="3">Sale Total</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="btn-group" role="group">
                    <asp:Button ID="btnShowAllProducts" CssClass="btn btn-primary btnPrintCustom lang-inventory-show-all-products margin-bottom-10 margin-left-0" runat="server" Text="Show all products" OnClick="btnShowAllProducts_OnClick" />
                    <asp:Button ID="btnPrint" CssClass="btn btn-primary btnPrintCustom lang-inventory-print-all-stock margin-bottom-10" runat="server" Text="Print All Stock" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnPrintImage" CssClass="btn btn-primary btnPrintCustom lang-inventory-print-barcode margin-bottom-10" runat="server" Text="Print Barcode" OnClick="btnPrintImage_Click" />

                    <!-- -->
                    <button type="button" id="btnExportToCsvModal" runat="server" class="btn btn-primary btnPrintCustom btn-export lang-inventory-export margin-bottom-10" data-toggle="modal" data-target=".export-to-csv">Export</button>
                    <a href="/admin/import?action=stock" class="btn btn-primary btnPrintCustom btn-export lang-inventory-import">Import</a>
                    <!-- -->

                </div>

            </div>
        </div>
    </div>


    <div style="clear: both"></div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="gridHeader">
                <div class="col-xs-12 col-sm-12 gridTitle two-line-grid-title text-left">
                    <label class="col-xs-6 col-sm-6 lang-inventory-stock-report">Stock Report</label>
                    <div class="col-xs-6 col-sm-6">

                        <div class="float-right form-group prod-loader-counter">
                            <asp:Label ID="lblLodProductCounter" runat="server" Text="Label"></asp:Label>
                            /
                            <asp:Label runat="server" CssClass="" ID="lblloadProductCounter"></asp:Label>

                            <asp:LinkButton ID="btnPrevious" runat="server" CssClass="btn btn-default" OnClick="btnPrevious_OnClick"><i class="fa fa-chevron-left"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnNext" runat="server" CssClass="btn btn-default" OnClick="btnNext_OnClick"><i class="fa fa-chevron-right"></i></asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>

            <div class="">
                <div class="gridTitle form-inline">
                    <asp:Panel ID="PanelSearchStock" CssClass="" runat="server">

                        <div class="form-group stock-search-field">
                            <asp:DropDownList ID="ddlBrachWiseSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="btnSearchDate_Click">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group stock-search-field">
                            <asp:DropDownList ID="ddlWarehouseList" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group stock-search-field">
                            <asp:DropDownList ID="ddlLocationList" runat="server" CssClass="form-control margin-left-4" AutoPostBack="true" OnSelectedIndexChanged="btnSearchDate_Click">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group stock-search-field">
                            <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="btnSearchDate_Click">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group stock-search-field">
                            <asp:DropDownList ID="ddlSupplierList" CssClass="form-control margin-left" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnSearchDate_Click"></asp:DropDownList>
                        </div>
                        <div class="form-group stock-search-field">
                            <asp:DropDownList ID="ddlCatagoryList" CssClass="form-control margin-left-4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnSearchDate_Click"></asp:DropDownList>
                        </div>

                        <div class="form-group stock-search-field">
                            <asp:DropDownList runat="server" ID="ddlStockStatus" CssClass="margin-left-4" AutoPostBack="True" OnSelectedIndexChanged="ddlStockStatus_OnSelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="0">Non-Active</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="form-group stock-search-field">
                            <asp:TextBox ID="txtSearch" CssClass="form-control margin-left-4" runat="server" AutoPostBack="true" OnTextChanged="txtSearch_OnTextChanged" placeholder="Search..."></asp:TextBox>
                        </div>


                    </asp:Panel>
                </div>
            </div>

        </div>


        <!-- Export CSV -->
        <div class="modal fade export-to-csv" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Export to CSV</h4>
                    </div>
                    <div class="modal-body">
                        <asp:RadioButtonList ID="ddlExportCSVOption" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Text=" Summary" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text=" Detail" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="modal-footer">
                        <asp:UpdatePanel ID="UpdatePanelExport" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnExportToCsv" CssClass="btn btn-primary btnPrintCustom" runat="server" Text="Export CSV" OnClick="btnExportToCsv_OnClick" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExportToCsv" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <asp:Label ID="lblTest2" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>





        <div class="col-md-12">
            <div class="scroll">
                <%--EnablePersistedSelection="true"--%>
                <asp:GridView ID="grdStock"
                    ShowFooter="True"
                    OnRowDataBound="grdStock_RowDataBound"
                    OnRowDeleting="grdStock_RowDeleting"
                    OnRowDeleted="grdStock_RowDeleted"
                    OnSelectedIndexChanged="grdStock_SelectedIndexChanged"
                    OnPageIndexChanging="grdStock_PageIndexChanging"
                    OnRowCommand="grdStock_RowCommand"
                    runat="server"
                    CssClass="mGrid gBox scrollBar"
                    AutoGenerateColumns="False"
                    DataKeyNames="Id"
                    EmptyDataText="There are no data records to display."
                    AllowSorting="true"
                    ViewStateMode="Enabled"
                    AllowPaging="False" PageSize="50">
                    <Columns>
                        <asp:TemplateField HeaderText="SL" Visible="false">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <input id="chkGrdSelectAll" type="checkbox" onclick="CheckGrdSelectAll(this)" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkGrdSelect" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Id" SortExpression="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="disNone" />
                            <HeaderStyle CssClass="disNone" />
                            <FooterStyle CssClass="disNone" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Image" SortExpression="image">
                            <ItemTemplate>
                                <asp:Image runat="server" ImageUrl='<%# String.Format("~/Img/Product/{0}", ProcessMyDataItem(Eval("image"))) %>' CssClass="productImg" ID="Image1"></asp:Image>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ID" SortExpression="prodID">
                            <ItemTemplate>
                                <asp:Label ID="lblProdID" runat="server" Text='<%# Bind("prodID") %>' CssClass="grd-product-Id"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Product Name" SortExpression="prodName">
                            <ItemTemplate>
                                <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("prodName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Barcode" SortExpression="prodCode" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblProdCode" runat="server" Text='<%# Bind("prodCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Supplier" SortExpression="supCompany">
                            <ItemTemplate>
                                <asp:Label ID="lblSupCompanyId" runat="server" Text='<%# Bind("supCompanyId") %>'></asp:Label>
                                <asp:Label ID="lblSupCompany" runat="server" Text='<%# Bind("supCompany") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" SortExpression="catName">
                            <ItemTemplate>
                                <asp:Label ID="lblCatName" runat="server" Text='<%# Bind("catName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manufacturer" SortExpression="manufacturerName">
                            <ItemTemplate>
                                <asp:Label ID="lblManufacturer" runat="server" Text='<%# Bind("manufacturerName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Stock Qty" SortExpression="qty" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# String.IsNullOrEmpty(Eval("qty").ToString())? "0" : Eval("qty").ToString()  %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblQtyFooter" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Stock Qty" SortExpression="qty" ItemStyle-CssClass="">
                            <ItemTemplate>
                                <asp:Label ID="lblOrginalQty" runat="server" Text='' CssClass="stock-qty-loader grd-product-qty"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Free Qty" SortExpression="freeQty">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblOfferType" runat="server" Text='<%# Bind("offerType") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblDiscountType" runat="server" Text='<%# Bind("discountType") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblDiscountValue" runat="server" Text='<%# Bind("discountValue") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblOfferValue" runat="server" Text='<%# Bind("offerValue") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblTotalFreeQty" runat="server" Text=''></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Buy Price" SortExpression="bPrice">
                            <ItemTemplate>
                                <asp:Label ID="lblBPrice" runat="server" Text='<%# Bind("bPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Company Price" SortExpression="comPrice">
                            <ItemTemplate>
                                <asp:Label ID="lblComPrice" runat="server" Text='<%# Bind("comPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Wholesale Price" SortExpression="dealerPrice">
                            <ItemTemplate>
                                <asp:Label ID="lblDealerPrice" runat="server" Text='<%# Bind("dealerPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sale Price" SortExpression="sPrice">
                            <ItemTemplate>
                                <asp:Label ID="lblSPrice" runat="server" Text='<%# Bind("sPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Buy Total" SortExpression="buyTotal">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblBuyTotal" runat="server" Text='<%# Convert.ToDecimal(Eval("bPrice"))*Convert.ToDecimal(String.IsNullOrEmpty(Eval("qty").ToString())? "0" : Eval("qty").ToString()) %>'></asp:Label>--%>
                                <asp:Label ID="lblBuyTotal" runat="server" Text=''></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Free Total" SortExpression="freeTotal">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblBuyTotal" runat="server" Text='<%# Convert.ToDecimal(Eval("bPrice"))*Convert.ToDecimal(String.IsNullOrEmpty(Eval("qty").ToString())? "0" : Eval("qty").ToString()) %>'></asp:Label>--%>
                                <asp:Label ID="lblFreeTotal" runat="server" Text=''></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Wholesale Total" SortExpression="wholesaleTotal">
                            <ItemTemplate>
                                <asp:Label ID="lblDealerTotal" runat="server" Text='<%# Convert.ToDecimal(Eval("dealerPrice"))*Convert.ToDecimal(Eval("qty").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Retail Total" SortExpression="soldTotal">
                            <ItemTemplate>
                                <asp:Label ID="lblSellTotal" runat="server" Text='<%# Convert.ToDecimal(Eval("sPrice"))*Convert.ToDecimal(String.IsNullOrEmpty(Eval("qty").ToString())? "0" : Eval("qty").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date" SortExpression="entryDate" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="UnitId" SortExpression="unitId" HeaderStyle-CssClass="disNone" ItemStyle-CssClass="disNone">
                            <ItemTemplate>
                                <asp:Label ID="lblUnitId" runat="server" Text='<%# Bind("unitId") %>' CssClass="grd-unit-id"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Attribute Record" SortExpression="attributeRecord" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAttributeRecord" runat="server" Text='<%# Bind("attributeRecord") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div class="btn-group btn-stock-action">
                                    <button type="button" class="btn btn-sm btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Action <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu btn-stock-dropdown-position">
                                        <li>
                                            <asp:LinkButton ID="btnGrdView" runat="server" CssClass="btn btn-design text-align-left" CausesValidation="False" CommandName="Select" Text="<span class='glyphicon glyphicon-adjust'></span> View" data-toggle="tooltip" data-placement="top" title="View " data-trigger="hover"></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="btnVariantProduct" runat="server" CssClass='<%# ShowButtonVariantProduct(Eval("isParent")) %>' CausesValidation="False" CommandName="Variant" Text="<span class='glyphicon glyphicon-adjust'></span> Variant Product" data-toggle="tooltip" data-placement="top" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" title="Variant Product " data-trigger="hover"></asp:LinkButton></li>
                                        <li class="disNone">
                                            <asp:LinkButton ID="btnAddVariant" runat="server" CssClass="btn btn-design text-align-left" CausesValidation="False" CommandName="addVariant" Text="<span class='glyphicon glyphicon-adjust'></span> Add Variant" data-toggle="tooltip" data-placement="top" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" title="Add Variant " data-trigger="hover"></asp:LinkButton></li>
                                        <li><a href="#">
                                            <asp:LinkButton ID="btnGrdEdit" runat="server" CssClass="btn btn-design text-align-left btnEditOpt" CausesValidation="False" CommandName="Edit" Text="<span class='glyphicon glyphicon-pencil'></span> Edit" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" title="Edit "></asp:LinkButton></li>
                                        <li class="transfer-opt disNone">
                                            <asp:LinkButton ID="btnGrdTransfer" runat="server" CssClass="btn btn-design text-align-left" CausesValidation="False" CommandName="Transfer" Text="<span class='glyphicon glyphicon-transfer'></span> Transfer" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" data-toggle="tooltip" data-placement="top" title="Transfer " data-trigger="hover"></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="btnStockStatus" runat="server" CssClass="btn btn-design text-align-left" CausesValidation="False" CommandName="StockStatus" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" title="Sale Status"><i class="fa fa-gg-circle fa-400px"></i> Stock Status</asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="btnCustomBarcode" runat="server" CssClass="btn btn-design text-align-left" CausesValidation="False" CommandName="customBarcode" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" Text="<span class='glyphicon glyphicon-barcode'></span> Print Custom" data-toggle="tooltip" data-placement="top" title="Print Custom Barcode " data-trigger="hover"></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="btnPrintBarcode" runat="server" CssClass="btn btn-design text-align-left" CausesValidation="False" CommandName="Print" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" title="Print Barcode"><i class="fa fa-barcode fa-400px"></i> Print Barcode</asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="btnGrdDelete" CssClass='<%# ShowButtonDelete(Eval("active")) %>' OnClientClick=" return confirm('Are you sure you want to delete selected record (This product are not found in any store) ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span> Delete" title="Delete Stock"></asp:LinkButton></li>
                                        <li>
                                            <asp:LinkButton ID="btnGrdRestore" CssClass='<%# ShowButtonRestore(Eval("active")) %>' runat="server" CommandArgument="<%#((GridViewRow) Container).RowIndex %>" CausesValidation="False" OnClientClick=" return confirm('Are u want to restore product') " CommandName="Restore" Text="<span class='glyphicon glyphicon-refresh'></span> Restore" title="Restore Stock"></asp:LinkButton></li>
                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />

                </asp:GridView>


            </div>
            <div>
                <asp:Label ID="lblSummary" runat="server" CssClass="footer-summary pull-right" Text="AAA: 1500"></asp:Label>
            </div>
        </div>




        <!-- modal print stock status -->
        <div id="stockBarcodePrintOption" class="modal fade" role="dialog">
            <div class="modal-dialog modal-sm">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Print Barcode</h4>

                        <asp:HiddenField ID="hfProductCode" runat="server" />
                        <asp:HiddenField ID="ProdCodeField" runat="server" />
                        <asp:HiddenField ID="lastQtyField" runat="server" />
                        <asp:HiddenField ID="currentQtyField" runat="server" />

                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">

                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtInputStock" CssClass="form-control" placeholder="Enter qty"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:Button runat="server" ID="btnStockBarcodePrintFromModal" Text="Print" CssClass="btn btn-info" OnClick="btnStockBarcodePrintFromModal_OnClick" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>


        <div id="noBarcodeModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-md">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Print Barcode</h4>
                    </div>
                    <div class="modal-body">
                        <h3><span class="text-warning">Product code length is too short, you can't create barcode!!</span></h3>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>


        <!-- Stock Transfer -->
        <div id="stockTransfer" class="modal fade" role="dialog">
            <div class="modal-dialog ">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Transfer Stock</h4>
                        <asp:Label ID="lblTransFromProdId" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="lblTransFromStoreId" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">

                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnStockTransfer" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div id="div1" runat="server"></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="form-group">
                                <div class="row">
                                    <label class="col-md-6">Product Name:</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblTransferProductName" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-md-6">Available Qty:</label>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblTransferAvailableQty" runat="server" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-md-6">Transfer Qty</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtTransferQty" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <label class="col-md-6">Shift To</label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlShiftTo" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="btnStockTransfer" runat="server" Text="Transfer" CssClass="btn btn-primary transfer" OnClick="btnStockTransfer_OnClick" />
                        </div>
                    </div>
                </div>

            </div>
        </div>


        <!-- Varant child product-->
        <div id="variantProductModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-md">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Variant Product List</h4>
                        <asp:Label runat="server" ID="lblProdIdVariant" Text="" Visible="false"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="dsGridVariant" runat="server" CssClass="mGrid gBox" AutoGenerateColumns="False" 
                            DataSourceID="dsGridVariantProd"
                            OnRowCommand="dsGridVariant_OnRowCommand" OnRowDataBound="dsGridVariant_OnRowDataBound">
                            <Columns>
                                <asp:BoundField DataField="prodID" HeaderText="prodID" SortExpression="prodID"></asp:BoundField>
                                <asp:TemplateField HeaderText="prodName" SortExpression="prodName">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("prodName") %>' ID="lblProdName"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="qty" HeaderText="qty" SortExpression="qty"></asp:BoundField>
                                <asp:BoundField DataField="bPrice" HeaderText="bPrice" SortExpression="bPrice"></asp:BoundField>
                                <asp:BoundField DataField="sPrice" HeaderText="sPrice" SortExpression="sPrice"></asp:BoundField>
                                <asp:TemplateField HeaderText="attributeRecord" SortExpression="attributeRecord" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("attributeRecord") %>' ID="lblAttriRecord"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="fieldRecord" HeaderText="fieldRecord" SortExpression="fieldRecord" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="parentId" HeaderText="parentId" SortExpression="parentId" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="prodName" HeaderText="Parent Product" SortExpression="prodName"></asp:BoundField>
                                <asp:CheckBoxField DataField="isChild" HeaderText="isChild" SortExpression="isChild"></asp:CheckBoxField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource runat="server" ID="dsGridVariantProd" ConnectionString='<%$ ConnectionStrings:dbPOS %>' SelectCommand="SELECT [prodID], [prodName], [qty], [bPrice], [sPrice], [attributeRecord], [fieldRecord], [parentId], [isChild] FROM [StockInfo] WHERE ([parentId] = @parentId)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblProdIdVariant" PropertyName="Text" Name="parentId" Type="String"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>


        <!-- Add New Variant -->
        <div id="AddVariantModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-md">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Add Variant</h4>
                        <asp:Label runat="server" ID="lblAddVariantProductId" Text="" Visible="false"></asp:Label>
                    </div>
                    <div class="modal-body">
                        <h3 runat="server" id="lblProductNameAddVariant"></h3>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlFieldAddVariant" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="A">A</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group ">
                                    <asp:DropDownList ID="ddlAttrAddVariant" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group padding-right-15">
                                <asp:Button ID="btnSaveVariant" runat="server" Text="Add" CssClass="btn btn-default pull-right width-20-persent" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>




        <asp:HiddenField ID="lblProdNameBarcodePrint" runat="server" />
        <asp:HiddenField ID="lblCodeBarcodePrint" runat="server" />
        <asp:HiddenField ID="lblPriceBarcodePrint" runat="server" />

    </div>

    <script>
        function openModal() {
            $('#modalStock').modal('show');
        }

        function stockTransferModal() {
            $('#stockTransfer').modal('show');
        }

        function closeModal() {
            $(".modal").modal("close");
        }


        $(".closeModal").click(function () {
            $(".modal").modal("close");
        });


        function closeBackdrop() {
            $(".modal-backdrop").addClass("modal").removeClass("modal-backdrop");
        }


        function customBarcodePrint() {
            var prodName = $("#contentBody_lblProdNameBarcodePrint").val();
            var prodCode = $("#contentBody_lblCodeBarcodePrint").val();
            var prodPrice = $("#contentBody_lblPriceBarcodePrint").val();

            window.open("" + baseUrl + "Admin/BarcodeTool/barcode.html?name=" + prodName + "&code=" +
                          prodCode + "&price=" + prodPrice + "", '_blank', 'location=yes,height=570,width=520,scrollbars=yes,status=yes');

        }

    </script>


    <%-- <script src="../Js/Bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=contentBody_ddlGrdCols]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>--%>

    <script>
        activeModule = "inventory";
    </script>

    <script src="<%=ResolveUrl("~/Admin/InventoryBundle/Script/main.js?v0.037") %>"></script>

    <script>

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            var head = document.getElementsByTagName("head")[0];
            var script = document.createElement('script');
            script.src = '/Admin/InventoryBundle/Script/main.js?v0.004';
            script.type = 'text/javascript';
            head.appendChild(script);


            $(document).ready(function () {

                var storeId = $('#contentBody_lblStoreId').text();

                LoadQty(storeId);

            });

            

        });





    </script>

</asp:Content>
