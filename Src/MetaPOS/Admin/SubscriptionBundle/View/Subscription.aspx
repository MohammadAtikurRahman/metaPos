<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="MetaPOS.Admin.SubscriptionBundle.View.Subscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Subscription
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SubscriptionBundle/Content/subscription.css?v=0.002") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SubscriptionBundle/Content/subscription-responsive.css?v=0.002") %>" rel="stylesheet" />

    <!-- Latest compiled and minified CSS -->

    <script>
        function ShowPopup(title, body) {
            $("#MyPopupSubscription .modal-title").html(title);
            $("#MyPopupSubscription .modal-body").html(body);
            $("#MyPopupSubscription").modal("show");
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="subscription">
        <div class="row">

            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
                <div id="divHeaderPanel">
                    <%--<label class="title">Subscription</label>--%>
                    <asp:Label runat="server" ID="lblTest" Text=""></asp:Label>
                    <label class="title"><%=Resources.Language.Title_subscription %></label>
                    <asp:Label runat="server" ID="lblMessage" Text=""></asp:Label>
                    <div class="pull-right form-inline ">

                        <p class="balance">
                            <%=Resources.Language.Lbl_subscription_balance %>
                            <asp:Label ID="lblBalance" runat="server" Text="..." CssClass=" text-balance balance-text"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>


            <div class="col-md-6 col-md-offset-3 col-xs-12 section">
                <div class="form-horizontal ">
                    <div class="ReturnfieldHeight2 ">
                        <h2 class="sectionHeading"><%=Resources.Language.Lbl_subscription_add_balance %> </h2>
                        <asp:Panel ID="pnlCode" runat="server">
                            <div class="col-md-8 col-md-offset-2">
                                <div class="">
                                    <label><%=Resources.Language.Lbl_subscription_amount %> </label>
                                    <asp:TextBox runat="server" ID="txtPaymentAmount" CssClass="form-control" placeholder="<%$Resources:Language, Lbl_subscription_payment_amount  %>"></asp:TextBox>

                                    <asp:Panel runat="server" ID="Panel4">
                                            <div class="col-md-8  col-md-offset-2 due-section">
                                                <p class="text-center label-control transection-wallet-title">
                                                    <%=Resources.Language.Lbl_subscription_total_due %> <b>
                                                        <asp:Label runat="server" ID="lblDueAmout" Text="..."></asp:Label></b>
                                                </p>
                                                <p class="text-center label-control transection-wallet-title">
                                                    <%=Resources.Language.Lbl_subscription_current_balance %> <b>
                                                        <asp:Label runat="server" ID="lblCurrentBalance" Text="..."></asp:Label></b>
                                                </p>
                                                <p class="text-center label-control transection-wallet-title">
                                                    <%=Resources.Language.Lbl_subscription_due_date %><b>
                                                        <asp:Label ID="lblNextExpiryDate" runat="server" Text="Next Expire Date" CssClass=""></asp:Label></b>
                                                </p>
                                            </div>
                                    </asp:Panel>

                                </div>

                                <div class="form-group" id="divCheckedSendEmail">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" value="" /><%=Resources.Language.Lbl_subscription_i_want_to_receive_invoice_in_email %></label>
                                    </div>
                                </div>
                                <div class="form-group disNone" id="divEmailOption">

                                    <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" placeholder="<%$Resources:Language, Lbl_subscription_enter_email  %>"></asp:TextBox>
                                </div>

                                <div class="form-group" id="divTerms">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" value="" /><%=Resources.Language.Lbl_subscription_i_confirm_that_i_am_by_traking_this_box %> <a href="/privacy.html" target="_blank"><%=Resources.Language.Lbl_subscription_in_terms_of_your_payment_and_service %></a> <%=Resources.Language.Lbl_subscription_agreed %></label>
                                    </div>
                                </div>

                                <div class="form-group margin-right-minus-30">
                                    <asp:Button ID="btnPayment" runat="server" CssClass="btn btn-info btn-md btnAddCustom printBtnDesign CRBtnDesign" Text="<%$Resources:Language, Btn_subscription_add_balance %>" OnClick="btnPayment_OnClick" disabled="true" />
                                </div>

                            </div>
                        </asp:Panel>
                    </div>


                    <div class="qrcode-metapos disNone">
                        <div class="row">
                            <p class="col-md-6">সহজ ভাবে বিকাশ অ্যাপ দিয়ে পেমেন্ট করুন</p>
                            <div class="col-md-6">
                                <img src="../../../Img/metapos_marchant_qrcode.png" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>


        <div class="col-md-6 disNone">
            <div class="ReturnfieldHeight2 section subscription-section-min-height">
                <h2 class="sectionHeading">আপনার চলতি মাসের বিল সমূহ</h2>


            </div>
        </div>


        <div class="col-md-12 ">

            <div class="ReturnfieldHeight2 section section-subscription">


                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div id="divSearchPanel">
                        <div class="col-md-4 col-sm-4 col-xs-4 gridTitle text-left">
                            <label><%=Resources.Language.Lbl_subscription_transection_history %></label>
                        </div>
                        <div class="col-md-8 col-sm-8 col-xs-8 gridTitle text-right form-inline" id="filterPanel">
                            <%--<div class="form-group">
                                    <select name="ddlActiveStatus" id="ddlActiveStatus" class="form-control">
                                        <option value="1">Active</option>
                                        <option value="0">Non-Active</option>
                                    </select>
                                </div>--%>
                        </div>
                    </div>
                </div>

                <div class="subscription-list scrollBar">

                    <table id="dataListTable" class="table table-striped table-bordered" cellspacing="0" width="100%">
                        <thead>
                            <tr>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>

    </div>



    <!-- Modal -->
    <div class="modal fade" id="PaymentConfirmModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="exampleModalLabel">metaPOS Payment</h4>
                </div>
                <div class="modal-body">
                    <iframe></iframe>
                </div>

            </div>
        </div>
    </div>


    <!-- Modal Popup -->
    <div id="MyPopupSubscription" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Popup -->

    <script src="<%= ResolveUrl("~/Admin/SubscriptionBundle/Script/Subscription.js?v0.014") %>"></script>
    <script>
        activeModule = "settings";

        var InvoiceNo = "<% =Resources.Language.Lbl_subscription_invoice_no %>";
        var Title = "<% =Resources.Language.Lbl_subscription_title %>";
        var Description = "<% =Resources.Language.Lbl_subscription_description %>";
        var CashIn = "<% =Resources.Language.Lbl_subscription_cash_in %>";//payment
        var CashOut = "<% =Resources.Language.Lbl_subscription_cash_out %>";//Fee
        var Type = "<% =Resources.Language.Lbl_subscription_type %>";
        var Status = "<% =Resources.Language.Lbl_subscription_status %>";
        var sDate = "<% =Resources.Language.Lbl_subscription_date %>";
    </script>


</asp:Content>
