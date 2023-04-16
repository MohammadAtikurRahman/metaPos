<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MetaPOS.Admin.InstallmentBundle.View.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Installment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/InstallmentBundle/Content/installment.css?v=0.0013") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/InstallmentBundle/Content/installment-responsive.css?v=0.0002") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/InstallmentBundle/Content/customer.css?v=0.0013") %>" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">


    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12 marginTop-10">
            <div id="divHeaderPanel">
                <label class="title"><%=Resources.Language.Title_installment %></label>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">

            <div class="gridTitle">
                <div id="divSearchPanel" class="col-md-12 col-sm-12 col-xs-12">
                    <div class="col-md-4 col-sm-3 col-xs-3 gridTitle text-left">
                        <label><%=Resources.Language.Lbl_installment_installment_list %></label>
                    </div>
                    <div class="col-md-8 col-sm-9 col-xs-9 gridTitle text-right form-inline" id="filterPanel">
                        <div class="form-group">
                            <input type="checkbox" id="chkWithDueCustomer" value="" class="float-left" /><span class="checkbox-text float-left margin-left-7"><%=Resources.Language.Lbl_installment_all_installment %></span>
                            <input type="text" id="txtFrom" class="form-control datepickerCSS float-left margin-left-7" /> <span class="float-left"><%=Resources.Language.Lbl_installment_to %></span>
                            <input type="text" id="txtTo" class="form-control datepickerCSS float-left" />
                        </div>
                    </div>
                </div>

                <div class="card" id="divListPanel">
                    <table id="dataListTable" class="table table-striped table-bordered" cellspacing="0" width="100%" style="overflow-x: scroll; display: block;">
                        <thead class="scrollBar">
                            <tr>
                                <th></th>
                                <th></th>
                                <th></th>

                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th>Footer</th>
                                <th></th>
                                <th></th>

                            </tr>
                        </tfoot>
                    </table>
                </div>

            </div>
        </div>
    </div>



    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">
                        <label id="lblCustomerId"></label>
                        /
                        <label id="lblCustomerName"></label>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-6"><%=Resources.Language.Lbl_installment_total_paid %></label>
                            <label class="col-md-6">
                                <label id="lblTotalPaid">0.0</label></label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-6"><%=Resources.Language.Lbl_installment_total_balance %></label>
                            <label class="col-md-6">
                                <label id="lblTotalDue">0.00</label></label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-6 lang-installment-pay-date"><%=Resources.Language.Lbl_installment_pay_date %></label>
                            <div class="col-md-6">
                                <input type="text" class="datepickerCSS form-control" id="txtNextPayDate" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-6 lang-installment-pay"><%=Resources.Language.Lbl_installment_pay %></label>
                            <div class="col-md-6">
                                <input type="text" class="form-control" id="txtPay" />
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-close" data-dismiss="modal"><%=Resources.Language.Btn_installment_close %></button>
                    <button type="button" class="btn btn-primary btn-save" id="btnSaveReminder"><%=Resources.Language.Btn_installment_save %></button>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="lblHiddenCompanyName" runat="server" />
    <asp:HiddenField ID="lblHiddenCompanyAddress" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyPhone" runat="server"/>


    <script src="<%= ResolveUrl("~/Admin/InstallmentBundle/Script/installment-main.js?v=0.033") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/InstallmentBundle/Script/installment-customer.js?v=0.033") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/InstallmentBundle/Script/installment-upsert.js?v=0.033") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/InstallmentBundle/Script/installment-message.js?v=0.033") %>"></script>
    <script>
        activeModule = "sale";

        var Bill_no = "<% =Resources.Language.Lbl_installment_bill_no %>";
        var Name = "<% =Resources.Language.Lbl_installment_name %>";
        var Phone = "<% =Resources.Language.Lbl_installment_phone %>";
        var Address = "<% =Resources.Language.Lbl_installment_address %>";
        var Total = "<% =Resources.Language.Lbl_installment_total%>";
        var Total_Paid = "<% =Resources.Language.Lbl_installment_total_paid %>";
        var Due = "<% =Resources.Language.Lbl_installment_due %>";
        var Instalment = "<% =Resources.Language.Lbl_installment_installment %>";
        var Paid = "<% =Resources.Language.Lbl_installment_paid %>";
        var Pay_Date = "<% =Resources.Language.Lbl_installment_pay_date %>";
        var Action = "<% =Resources.Language.Lbl_installment_action %>";

    </script>

</asp:Content>
