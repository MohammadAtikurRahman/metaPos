<%@ Page Title="" Language="C#" MasterPageFile="~/Shop/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MetaPOS.Shop.Default" %>

<%@ Register Src="~/Shop/Controller/Slider.ascx" TagPrefix="uc1" TagName="Slider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title runat="server" id="lblTitle"></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" runat="server">

    <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>

    <!-- Slider -->
    <div class="container-fluid" style="padding-left:0; padding-right:0;">
        <uc1:Slider runat="server" ID="Slider" />
    </div>

    <!-- Featured Product -->
    <div class="container">
        <div class="featured-product">
            <h2 class="product-title text-center">
                <asp:Label ID="lblFeaturedTitle" runat="server" Text="Featured Products"></asp:Label></h2>
            <div class="ser-t">
                <b></b>
                <span><i></i></span>
                <b class="line"></b>
            </div>
            <div class="product-list">
                <div class="row">
                    <asp:DataList ID="dlFeaturedProduct" runat="server" CssClass="" 
                        RepeatDirection="Horizontal" RepeatColumns="4" RepeatLayout="Flow"
                         onerror="imgError(this)" >
                        <ItemTemplate>
                            <div class="col-xs-6 col-sm-3 col-md-3 single-product">

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

                    <%--<div class="error-mgs">
                    <asp:Label CssClass="error" runat="server" ID="lblMsg" Text="There are no data records to found."></asp:Label>
                </div>--%>

                    <asp:Label ID="lblEmpty" Visible='<%#bool.Parse((dlFeaturedProduct.Items.Count==0).ToString())%>' CssClass="empty-message" Text="No featrure product found!" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!-- Featured Product -->
    <div class="container">
        <div class="new-product">
            <h2 class="product-title text-center">
                <asp:Label ID="Label1" runat="server" Text="New Collections"></asp:Label></h2>
            <div class="ser-t">
                <b></b>
                <span><i></i></span>
                <b class="line"></b>
            </div>
            <div class="product-list">
                <div class="row">
                    <asp:DataList ID="dlNewProduct" runat="server" CssClass="" RepeatDirection="Horizontal" RepeatColumns="4" RepeatLayout="Flow" onerror="imgError(this)">
                        <ItemTemplate>
                            <div class="col-xs-6 col-sm-3 col-md-3 single-product">

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

                    <%--<div class="error-mgs">
                        <asp:Label CssClass="error" runat="server" ID="lblMsg" Text="There are no data records to found."></asp:Label>
                    </div>--%>
                    <asp:Label ID="Label3" Visible='<%#bool.Parse((dlNewProduct.Items.Count==0).ToString())%>' CssClass="empty-message" Text="No new collection available!" runat="server"></asp:Label>
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
                                        <a href="#" runat="server" id="callToNumber"><i class="fa fa-phone"></i><asp:Label runat="server" CssClass="contactMobile" ID="lblContactMobile"></asp:Label></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>