<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Expiry.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.Expiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Expiry Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Expiry.css?v=0.002") %>" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Expiry-responsive.css?v=0.001") %>" />
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="warningQty">

        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
                <div class="section">
                    <h2 class="sectionBreadcrumb lang-inventory-expiry">Expiry </h2>
                    <asp:Label runat="server" ID="lblTest"></asp:Label>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="">
                    <div class="col-md-12 col-sm-12 col-xs-12 gridTitle text-left">
                        <label class="lang-inventory-expired-product-history">Expired Product History</label>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 gridTitle text-right form-inline">
                        <div class="form-group float-left margin-left-7">
                            <asp:CheckBox runat="server" ID="chkListExpiry" Text="All Preview" CssClass="expiry-preview-all" AutoPostBack="true" OnCheckedChanged="chkListExpiry_CheckedChanged"/>
                        </div>
                        <div class="form-group float-left margin-left-7">
                            <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control datepickerCSS" AutoPostBack="true" OnTextChanged="txtSearch_action"></asp:TextBox>
                        </div>
                            <span>to</span>
                        <div class="form-group float-left margin-left-7">
                            <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control datepickerCSS" AutoPostBack="true" OnTextChanged="txtSearch_action"></asp:TextBox>
                        </div>

                        <div class="form-group stock-search-field float-left margin-left-7">
                            <asp:DropDownList ID="ddlSupplierList" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtSearch_action"></asp:DropDownList>
                        </div>

                        <div class="form-group stock-search-field float-left margin-left-7">
                            <asp:DropDownList ID="ddlCatagoryList" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtSearch_action"></asp:DropDownList>
                        </div>


                        <div class="form-group stock-search-field float-left margin-left-7">
                            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtSearch_action" placeholder="Search..."></asp:TextBox>
                        </div>

                        <div class="form-group float-left margin-left-7">
                            <asp:LinkButton runat="server" ID="btnExpiry" OnClick="btnPrintExpiry_OnClick"><i class="fa fa-2x fa-print"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="scroll">
                    <asp:GridView ID="grdExpiryQty" runat="server" AutoGenerateColumns="False" CssClass="mGrid gBox scrollBar" PageSize="10"
                        DataKeyNames="Id" AllowPaging="true" OnRowDataBound="grdExpiryQty_OnRowDataBound" OnPageIndexChanging="grdExpiryQty_OnPageIndexChanging" EmptyDataText="No Record found">
                        <Columns>
                            <asp:TemplateField HeaderText="SL" ControlStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="Product Name" SortExpression="prodName">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("prodName") %>' ID="TextBox1"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("prodName") %>' ID="Label1"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="30%"></ControlStyle>
                            </asp:TemplateField>

                            <asp:BoundField DataField="catName" HeaderText="Category Name" SortExpression="catName" ItemStyle-Width="25%"></asp:BoundField>
                            <asp:BoundField DataField="qty" HeaderText="Current Qty" SortExpression="qty" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:TemplateField HeaderText="Expiry" SortExpression="expiryDate">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='' ID="lblExpiryDay"></asp:Label>
                                    <asp:Label runat="server" Text='<%# Bind("expiryDate") %>' ID="lblExpiryDate" Visible="False"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:GridView>

                </div>
            </div>
        </div>

    </div>


    <script>

        //init date formet
        //$(".hasDatepicker").datepicker({ dateFormat: 'yy-MMMM-dd' });

        activeModule = "inventory";
    </script>
</asp:Content>
