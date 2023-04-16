<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Subscription.aspx.cs" Inherits="MetaPOS.Admin.SubscriptionBundle.View.Subscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Subscription
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SubscriptionBundle/Content/subscription.css?v=0.002") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SubscriptionBundle/Content/subscription-responsive.css?v=0.002") %>" rel="stylesheet" />

    <!-- Latest compiled and minified CSS -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="subscription">
        <div class="row">

            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
                <div id="divHeaderPanel">
                    <label class="title">Subscription</label>
                    <asp:Label runat="server" ID="lblMessage" Text=""></asp:Label>
                    <div class="pull-right form-inline ">

                        <p class="balance">
                            Balance:
                        <asp:Label ID="lblBalance" runat="server" Text="..." CssClass=" text-balance balance-text"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>


            <div class="col-md-6 col-md-offset-3 col-xs-12">
                <div class="section form-horizontal subscription-section-min-height">
                    <div class="ReturnfieldHeight2 ">
                        <h2 class="sectionHeading">Add Balance </h2>
                        <asp:Panel ID="pnlCode" runat="server">


                            <div class="form-group">
                                <asp:Panel runat="server" ID="Panel4">

                                    <div class="padding-bottom-20">
                                        <div class="col-md-8  col-md-offset-2 padding-bottom-20">
                                            <p class="text-center label-control transection-wallet-title">
                                                Total Due: <b>
                                                    <asp:Label runat="server" ID="lblDueAmout" Text="..."></asp:Label></b>
                                            </p>
                                            <p class="text-center label-control transection-wallet-title">
                                                Current Balance: <b>
                                                    <asp:Label runat="server" ID="lblCurrentBalance" Text="..."></asp:Label></b>
                                            </p>
                                            <p class="text-center label-control transection-wallet-title">
                                                Due Date: <b>
                                                    <asp:Label ID="lblNextExpiryDate" runat="server" Text="Next Expire Date" CssClass=""></asp:Label></b>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="col-md-8 col-md-offset-2 col-xs-8 col-xs-offset-2">

                                        <div class="form-group">
                                            <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server" Text="0.0"></asp:TextBox>

                                        </div>
                                        <div class="form-group margin-right-minus-30">
                                            <asp:Button ID="btnPayment" runat="server" CssClass="btn btn-info btn-md btnAddCustom printBtnDesign CRBtnDesign" Text="Add Balance" OnClick="btnPayment_OnClick" />
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

                        </asp:Panel>
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
                                <label>Transection History</label>
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

                    <div class="subscription-list scrollBar" >

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


    <script>
        activeModule = "settings";

    </script>

    <script src="<%= ResolveUrl("~/Admin/SubscriptionBundle/Script/Subscription.js?v0.006") %>"></script>

</asp:Content>
