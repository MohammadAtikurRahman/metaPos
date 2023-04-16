﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Offer.aspx.cs" Inherits="MetaPOS.Admin.PromotionBundle.View.Offer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Offer Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link href="<%=ResolveUrl("~/Admin/PromotionBundle/Content/Offer.css?v=0.001") %>" rel="stylesheet" />
    <script type="text/javascript">

        $(document).keyup(function() {

            if ($(document.activeElement).attr("id") == "<% = txtSearchNameCode.ClientID %>") {

                var itemType = $("#contentBody_rblSearchIn input:checked").val();

                $("[id$=txtSearchNameCode]").autocomplete({
                    source: function(request, response) {

                        $.ajax({
                            url: '<%= ResolveUrl("~/Admin/SaleBundle/View/Invoice.aspx/getSearchProductListAction") %>',
                            data: "{ 'search': '" + request.term + "', 'itemType': '" + itemType + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function(data) {
                                response($.map(data.d, function(item) {
                                    return {
                                        label: item.split('<>')[0] + item.split('<>')[1] + ' [' + item.split('<>')[2] + ']',
                                        val: item.split('<>')[2]
                                    };
                                }));
                            },
                            error: function() {
                                alert(response.responseText);
                            },
                            failure: function() {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function(e, i) {
                        $("[id$=hfProductDetails]").val(i.item.val);
                    },
                    minLength: 1
                });
            }
        });

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb"><%=Resources.Language.Title_offer %></h2>
                <asp:Label runat="server" ID="lblTest"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-horizontal">
            <div class="stockfieldHeight singleStockField section-new">
                <h2 class="sectionHeading"><%=Resources.Language.Lbl_offer_set_Offer %></h2>

                <asp:RadioButtonList ID="rblSearchIn" runat="server" CssClass="radio-inline product-package-select">
                    <asp:ListItem Value="0" Selected="True" Text="<%$Resources:Language, Lbl_offer_product %>"></asp:ListItem>
                    <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_offer_package %>"></asp:ListItem>
                </asp:RadioButtonList>

                <asp:Panel ID="pnlSetOffer" runat="server" DefaultButton="btnSetOffer">
                    <div class="form-group">
                        <asp:Panel ID="pnlScan" runat="server" DefaultButton="btnLoadProductDetails">
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearchNameCode" CssClass="form-control" runat="server" aria-describedby="btnLoadProductDetails" placeholder="<%$Resources:Language, Lbl_offer_search_scan_product_by_name %>"></asp:TextBox>
                                    <asp:HiddenField ID="hfProductDetails" runat="server" />
                                    <span class="input-group-btn">
                                        <asp:ImageButton ID="btnLoadProductDetails" runat="server" CssClass="btn btn-default inp-group" OnClick="btnLoadProductDetails_Click" ImageUrl="~/Img/loading.png" />
                                    </span>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-xs-6 section-new-left">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="<%$Resources:Language, Lbl_offer_product_name %>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtProductName" CssClass="form-control" Enabled="false" BackColor="#F0F3F4" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="<%$Resources:Language, Lbl_offer_offer_start %>"></asp:Label>
                            <asp:TextBox ID="txtOfferStart" CssClass="form-control datepickerCSS" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl22" runat="server" Text="<%$Resources:Language, Lbl_offer_offer_on %>"></asp:Label>
                            <asp:DropDownList ID="ddlOfferOn" CssClass=" form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOfferOn_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True" Text="<%$Resources:Language, Lbl_offer_amount %>"></asp:ListItem>
                                <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_offer_qty %>"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label5" runat="server" Text="<%$Resources:Language, Lbl_offer_discount_on %>"></asp:Label>
                            <asp:DropDownList ID="ddlDiscountOn" CssClass=" form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDiscountOn_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True" Text="<%$Resources:Language, Lbl_offer_amount %>"></asp:ListItem>
                                <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_offer_qty %>"></asp:ListItem>
                                <asp:ListItem Value="2" Text="<%$Resources:Language, Lbl_offer_point %>"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-6 section-new-right">
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="<%$Resources:Language, Lbl_offer_product_id %>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtProductID" CssClass="form-control" Enabled="false" BackColor="#F0F3F4" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="<%$Resources:Language, Lbl_offer_offer_end %>"></asp:Label>
                            <asp:TextBox ID="txtOfferEnd" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblOfferOn" runat="server" Text="<%$Resources:Language, Lbl_offer_enter_amount %>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtOfferValue" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblDiscountOn" runat="server" Text="<%$Resources:Language, Lbl_offer_enter_amount %>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtDiscountValue" CssClass="form-control" />
                        </div>

                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnSetOffer" CssClass="btn btn-info btn-sm CRBtnDesign btnResize e67e22 btnAddOpt" runat="server" Text="<%$Resources:Language, Btn_offer_set %>" OnClick="btnSetOffer_Click" />
                        <asp:Button ID="btnReset" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnReset" runat="server" Text="<%$Resources:Language, Btn_offer_clear %>" OnClick="btnReset_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-xs-4 gridTitle text-left">
                    <label><%=Resources.Language.Lbl_offer_offer_list %></label>
                </div>
                <div class="col-xs-8 gridTitle text-right form-inline">
                    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btn">
                        <div class="form-group">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="<%$Resources:Language, Lbl_offer_search %>"></asp:TextBox>
                            <asp:Button ID="btn" runat="server" CssClass="disNone" />
                        </div>
                        <div class="form-group">
                            <asp:LinkButton runat="server" ID="btnPrint" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                        </div>
                    </asp:Panel>
                    
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="scroll">
                <asp:GridView ID="grdOffer" runat="server" CssClass="mGrid gBox" AutoGenerateColumns="False" DataKeyNames="Id"
                               EmptyDataText="No data records found." AllowPaging="true"
                              OnSelectedIndexChanged="grdOffer_SelectedIndexChanged" EnableViewState="false"
                              OnRowDataBound="grdOffer_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_offer_sl %>">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="prodPack" HeaderText="<%$Resources:Language, Lbl_offer_name %>" SortExpression="prodName" ItemStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="prodCode" HeaderText="<%$Resources:Language, Lbl_offer_code %>" SortExpression="prodCode"></asp:BoundField>
                        <asp:BoundField DataField="offerStart" HeaderText="<%$Resources:Language, Lbl_offer_offer_start %>" SortExpression="offerStart" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                        <asp:BoundField DataField="offerEnd" HeaderText="<%$Resources:Language, Lbl_offer_offer_end %>" SortExpression="offerEnd" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                        <asp:BoundField DataField="offerType" HeaderText="<%$Resources:Language, Lbl_offer_offer_on %>" SortExpression="offerType" ItemStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="offerValue" HeaderText="<%$Resources:Language, Lbl_offer_value %>" SortExpression="offerValue" ItemStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="discountType" HeaderText="<%$Resources:Language, Lbl_offer_disc_on %>" SortExpression="discountType" ItemStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="discountValue" HeaderText="<%$Resources:Language, Lbl_offer_value %>" SortExpression="discountValue" ItemStyle-Width="8%"></asp:BoundField>
                        <asp:BoundField DataField="active" HeaderText="<%$Resources:Language, Lbl_offer_active %>" SortExpression="active"></asp:BoundField>
                        <asp:TemplateField HeaderText="Activate" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnGrdView" runat="server" CssClass="btn btn-design" CausesValidation="False" CommandName="Select" Text="<span class='glyphicon glyphicon-refresh'></span>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnGrdDelete" CssClass="btn btn-design" OnClientClick=" return confirm('Are you sure you want to delete selected record ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
            </div>
        </div>
    </div>
    
    
    <script>
        activeModule = "promotion";
    </script>


</asp:Content>