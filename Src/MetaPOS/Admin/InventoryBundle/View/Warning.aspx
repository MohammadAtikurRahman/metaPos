<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Warning.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.Warning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Warning Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Warning.css") %>" type="text/css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Warning-responsive.css") %>" type="text/css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="warningQty">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 marginTop-10">
                <div class="section">
                    <h2 class="sectionBreadcrumb"><%=Resources.Language.Title_warning %></h2>
                    <asp:Label runat="server" ID="lblTest"></asp:Label>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="">
                    <div class="col-md-12 col-sm-12 col-xs-12 gridTitle text-left">
                        <label><%=Resources.Language.Lbl_warning_warning_stock_history %></label>
                    </div>
                    <div class="col-md-12 col-sm-12 col-xs-12 gridTitle text-right form-inline">

                        <div class="form-group stock-search-field float-left margin-left-7">
                            <asp:DropDownList ID="ddlSupplierList" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtSearch_TextChanged"></asp:DropDownList>
                        </div>
                        <div class="form-group stock-search-field float-left margin-left-7">
                            <asp:DropDownList ID="ddlCatagoryList" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtSearch_TextChanged"></asp:DropDownList>
                        </div>

                        <div class="form-group stock-search-field float-left margin-left-7">
                            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" placeholder="<%$Resources:Language,Lbl_warning_search %>"></asp:TextBox>
                        </div>

                        <div class="form-group float-left margin-left-7">
                            <asp:LinkButton runat="server" ID="btnPrintWarning" OnClick="btnPrintWarning_OnClick"><i class="fa fa-2x fa-print"></i></asp:LinkButton>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-12 col-sm-12">
                <div class="scroll">
                    <asp:GridView ID="grdWarningQty" runat="server" AutoGenerateColumns="False" CssClass="mGrid gBox scrollBar"
                        DataKeyNames="Id" AllowPaging="true"
                         PageSize="10" OnPageIndexChanging="grdWarningQty_OnPageIndexChanging"
                         EmptyDataText="No Records Found">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Language,Lbl_warning_sl%>" ControlStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,Lbl_warning_product%>" SortExpression="prodName">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("prodName") %>' ID="TextBox1"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("prodName") %>' ID="Label1"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="30%"></ControlStyle>
                            </asp:TemplateField>

                            <asp:BoundField DataField="catName" HeaderText="<%$Resources:Language,Lbl_warning_category%>" SortExpression="catName" ItemStyle-Width="25%"></asp:BoundField>
                            <asp:BoundField DataField="stockqty" HeaderText="<%$Resources:Language,Lbl_warning_current_qty%>" SortExpression="stockqty" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="warningQty" HeaderText="<%$Resources:Language,Lbl_warning_warning_qty%>" SortExpression="warningQty" ItemStyle-Width="15%"></asp:BoundField>

                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:GridView>

                </div>
            </div>
        </div>

    </div>

    <script>
        activeModule = "inventory";
    </script>

</asp:Content>
