<%@ Page Title="" Language="C#" MasterPageFile="~/Shop/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="MetaPOS.Shop.Product" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title runat="server" id="title">Products</title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" runat="server">

    <div class="product" runat="server">
        <div class="product-listing">
            <div class="container">

                <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>

                <div class="spec top-padding">
                    <h3>Our Products</h3>
                    <div class="ser-t">
                        <b></b>
                        <span><i></i></span>
                        <b class="line"></b>
                    </div>
                </div>

                <div class="search-section">
                    <div class="row">
                        <div class="cateogry col-md-3 ">
                            <ul class="">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DataList ID="lvCatagory" runat="server" CssClass="">
                                            <HeaderTemplate>
                                                <li>
                                                    <h4><i class="fa fa-tasks" aria-hidden="true"></i>
                                                        <span style="padding-left: 5px">Categories</span>
                                                    </h4>
                                                </li>
                                                <li class="category-item"><a href="Product">All Category</a></li>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--<div class="list-group">
                                            <a href="Product.aspx?cat=<%# Eval("Id") %>" class="list-group-item"><%# Eval("catName") %></a>
                                        </div>--%>
                                                <li class="category-item"><a href="Product?cat=<%# Eval("Id") %>" class=""><%# Eval("catName") %></a></li>
                                            </ItemTemplate>

                                        </asp:DataList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </ul>
                        </div>
                        <div class="col-md-9">
                            <div class="top-search col-md-12">
                                <div class="col-md-3">
                                    <div class="pos-search-number">
                                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                            <asp:ListItem Value="6">Show 6</asp:ListItem>
                                            <asp:ListItem Value="12">Show 12 </asp:ListItem>
                                            <asp:ListItem Value="24">Show 24 </asp:ListItem>
                                            <asp:ListItem Value="600">Show All</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3 col-md-offset-6">
                                    <div class="cat-search">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search...." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <asp:DataList ID="lvProduct" runat="server" CssClass="" RepeatDirection="Horizontal" RepeatColumns="4" RepeatLayout="Flow" onerror="imgError(this)">
                                <ItemTemplate>
                                    <div class="col-xs-6 col-sm-3 col-md-4 single-product">
                                        <asp:ImageButton ID="btnViewDetailsImage" runat="server" ImageUrl='<%# string.Format("~/Img/Product/{0}", Eval("image")) %>' CssClass="img-responsive product-img" ImageAlign="Middle" AlternateText="Photo" onerror="imgError(this)" />
                                        <asp:Label runat="server" ID="lblProductName" CssClass="lblSingle" Text=""><%# Eval("ProdTitle") %></asp:Label>
                                        <asp:Label runat="server" ID="lblPrice" CssClass="price"><%#string.Format(new System.Globalization.CultureInfo("bn-BD"), "{0:C}", Eval("sPrice")) %></asp:Label>
                                        <span style="display: none">
                                            <asp:Label runat="server" ID="lblIdTest"><%# Eval("Id") %></asp:Label>
                                            <asp:Label runat="server" ID="lblCatName"><%# Eval("catName") %></asp:Label>
                                            <asp:Label runat="server" ID="lblDescription"><%# Eval("shortDescr") %></asp:Label>
                                            <asp:Label runat="server" ID="lblSupCom"><%# Eval("supCompany") %></asp:Label>
                                            <asp:Label runat="server" ID="lblQulity"><%# Eval("qty") %></asp:Label>
                                            <asp:Label runat="server" ID="lblImage"><%# Eval("image") %></asp:Label>
                                        </span>
                                        <%--<asp:Button ID="btnAddToCart" CssClass="btn btn-primary" runat="server" Text="Add to Cart" />--%>
                                        <span class="add">
                                            <asp:Button ID="btnViewDetails" runat="server" CssClass="btn btn-danger my-cart-btn my-cart-b" Text="View Deatils" />
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>

                            <div class="error-mgs hide">
                                <asp:Label CssClass="error" runat="server" ID="lblMsg" Text="There have no products found."></asp:Label>
                            </div>

                            <asp:Label ID="lblEmpty" Text="" runat="server"></asp:Label>

                            <div class="row">
                                <!-- Paging start -->
                                <%--<asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <div class="col-md-12 paging-control">
                                        <div class="btn-group" role="group">
                                            <asp:LinkButton ID="btnFirst" runat="server" CssClass="btn btn-info btn-pagination-custom" OnClick="btnFirst_Click"> << First</asp:LinkButton>
                                            <asp:LinkButton ID="btnPrevious" runat="server" CssClass="btn btn-info btn-pagination-custom" OnClick="btnPrevious_Click"> < Previous</asp:LinkButton>
                                            <asp:LinkButton ID="btnNext" runat="server" CssClass="btn btn-info btn-pagination-custom" OnClick="btnNext_Click"> Next > </asp:LinkButton>
                                            <asp:LinkButton ID="btnLast" runat="server" CssClass="btn btn-info btn-pagination-custom" OnClick="btnLast_Click"> Last >> </asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>--%>

                                <div class="pagination-custom col-md-12">
                                    <div class="col-md-3 show-number">
                                        <asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-9 push-right">
                                        <table class="pagination-table">
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td colspan="5">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="center">
                                                                <asp:LinkButton ID="lnkFirst" runat="server" CssClass="btn btn-info btn-pagination-custom" OnClick="lnkFirst_Click">First</asp:LinkButton></td>
                                                            <td valign="top" align="center">
                                                                <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="btn btn-info btn-pagination-custom" OnClick="lnkPrevious_Click">Prev</asp:LinkButton>

                                                            </td>
                                                            <td>
                                                                <asp:DataList ID="DataListPaging" runat="server" RepeatDirection="Horizontal"
                                                                              OnItemCommand="DataListPaging_ItemCommand"
                                                                              OnItemDataBound="DataListPaging_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="Pagingbtn" Width="30" runat="server" CssClass="btn btn-info btn-pagination-custom" CommandArgument='<%# Eval("PageIndex") %>' CommandName="newpage" Text='<%# Eval("PageText") %> '></asp:LinkButton>&nbsp;
                                                                    </ItemTemplate>
                                                                </asp:DataList>&nbsp;
                                                            </td>
                                                            <td width="30" valign="top" align="center">
                                                                <asp:LinkButton ID="lnkNext" runat="server" CssClass="btn btn-info btn-pagination-custom" OnClick="lnkNext_Click">Next</asp:LinkButton>
                                                            </td>
                                                            <td width="30" valign="top" align="center">
                                                                <asp:LinkButton ID="lnkLast" runat="server" CssClass="btn btn-info btn-pagination-custom" OnClick="lnkLast_Click">Last</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="modal fade" id="ProductView" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-5 modal-left">
                                        <asp:Image ID="imgProdModal" runat="server" CssClass="img-responsive img-product" AlternateText="Product Image" />
                                    </div>
                                    <div class="col-md-7 modal-right">
                                        <h3><span id="ProductName"></span></h3>
                                        <hr />
                                        <h4><span id="Price"></span></h4>

                                        <div class="addToCart row" runat="server" visible="false">
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtQtyAddToCart" runat="server" TextMode="Number" CssClass="form-control" Text="1"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:Button ID="addToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary" OnClick="addToCart_Click" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>

                                        <b class="quick-border">Quick Overview:</b>
                                        <p><span id="Description"></span></p>
                                        <p class="qty"><b>Stock : </b><span id="qty"></span></p>
                                        <p><b>Supplier : </b><span id="supCom"></span></p>
                                        <p><b>Catagory : </b><span id="Catagory"></span></p>

                                        <div class="callToAction">
                                            <p>Call to Order</p>
                                            <asp:Label runat="server" ID="lblTempNumber" Visible="false"></asp:Label>
                                            <a href="#" runat="server" id="callToNumber">
                                                <i class="fa fa-phone"></i>
                                                <asp:Label runat="server" CssClass="contactMobile" ID="lblContactMobile"></asp:Label>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>