<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="MetaPOS.Admin.Customer" %>

<%@ Register Src="~/Admin/Controller/CustomerOpt.ascx" TagPrefix="uc1" TagName="CustomerOpt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Customers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Customer.css?v=0.002" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/CustomerBundle/Content/Customer.css?v0.0023") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/CustomerBundle/Content/customer-responsive.css?v0.0001") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 marginTop-10">

            <div id="divHeaderPanel">

                <label class="title col-md-6 col-sm-6 col-xs-6"><%=Resources.Language.Title_customers %></label>

                <div class="col-md-6 col-sm-6 col-xs-6  btn-group">
                    <asp:Label runat="server" ID="lblTest"></asp:Label>

                    <div class="form-group">
                        <button class="btn btn-info btn-sm btnResize btnAddCustom margin-left-10" type="button" id="btnCustomerModal"><%=Resources.Language.Btn_customers_new_customer %></button>

                        <asp:Button ID="btnCustomerSummary" runat="server" Text="<%$Resources:Language, Btn_customers_export_summary %>" CssClass="btn btn-default btn-sm btnResize " OnClick="btnCustomerSummary_OnClick" />

                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 header-bottom">
            <div class="gridHeader">
                <div class="col-md-8 col-sm-10 col-xs-10 gridTitle form-inline">
                    <div class="form-group float-left">
                        <asp:DropDownList runat="server" ID="ddlCusType" CssClass="form-control">
                            <asp:ListItem Value="All" Selected="True" Text="<%$Resources:Language, Lbl_customers_all_customer %>"></asp:ListItem>
                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_customers_retail %>"></asp:ListItem>
                            <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_customers_wholesale %>" ></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-group float-left margin-left-7">
                        <asp:DropDownList runat="server" ID="ddlSearchByPayStatus">
                            <asp:ListItem Value="All" Selected="True" Text="<%$Resources:Language, Lbl_customers_pay_status %>"></asp:ListItem>
                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_customers_paid %>"></asp:ListItem>
                            <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_customers_unpaid %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group float-left margin-left-7">
                        <asp:DropDownList runat="server" ID="ddlActiveStatus">
                            <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_customers_active %>"></asp:ListItem>
                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_customers_inactive %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:UpdatePanel ID="upSummery" runat="server">
                        <ContentTemplate>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnCustomerSummary" />
                        </Triggers>
                    </asp:UpdatePanel>




                    <%--<select id="example-getting-started" multiple="multiple">
                        <option value="account">Account</option>
                    </select>--%>
                </div>

                <div class="col-md-4 col-sm-2 col-xs-2 gridTitle text-right form-inline " id="filterPanel">
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="card datatable-grid-design scrollBar" id="divListPanel">
                <table id="dataListTable" class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>




    <!-- Customer modal -->
    <uc1:CustomerOpt runat="server" ID="CustomerOpt" />


    <div class="modal-customer">
        <div class="modal fade" tabindex="-1" role="dialog" id="customerModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">
                            <label id="lblTitle"></label>
                        </h4>
                        <label id="lblId" class="disNone"></label>
                    </div>
                    <div class="modal-body">
                        <div id="msgOutput" class="text-left"></div>
                        <div id="modalLoadResult"></div>

                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </div>






    <div class="modal-customer-ledger">
        <div class="modal fade" tabindex="-1" role="dialog" id="customerLedgerModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">
                            <label id="lblTitleLedger"><%=Resources.Language.Lbl_customers_print_full_ledger %></label></h4>
                        <asp:TextBox ID="txtLederCusId" runat="server" CssClass="disNone"></asp:TextBox>
                    </div>
                    <div class="modal-body">
                        <div id="msgOutputLedger" class="text-left"></div>
                        <div id="modalLedgerResult"></div>



                        <div class="view-customer-ledger">

                            <div class="form-group group-section">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDateTo" runat="server" class="form-control datepickerCSS" placeholder="<%$Resources:Language, Lbl_customers_start_date  %>" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtDateForm" runat="server" class="form-control datepickerCSS" placeholder="<%$Resources:Language, Lbl_customers_end_date  %>" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="modal-footer" runat="server" id="Div1">
                                <button type="button" class="btn btnCustomize" data-dismiss="modal" id="btnCloseCusModal"><%=Resources.Language.Btn_customers_close %></button>
                                <asp:Button runat="server" ID="btnLedgerPrint" OnClick="btnLedgerPrint_OnClick" class="btn btn-info btn-sm btnResize btnAddCustom" Text="<%$Resources:Language, Btn_customers_print %>" />
                            </div>

                        </div>


                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </div>




    <div class="modal-customer-opening-due">
        <div class="modal fade" tabindex="-1" role="dialog" id="openingDueModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title"><%=Resources.Language.Lbl_customers_add_opening_due  %> </h4>
                        <label id="lblCusId"></label>
                        <asp:Label ID="lblCustomerId" runat="server" Text="" CssClass="disNone"></asp:Label>
                    </div>
                    <div class="modal-body">

                        <div id="msgOutputOpeningDue" class="text-left"></div>

                        <div class="view-customer">

                            <div class="form-group group-section">
                                <div class="col-md-4">
                                    <label><%=Resources.Language.Lbl_customers_opening_due_amount %></label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="txtOpeningDueAmt" class="form-control float-number-validate"/>
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <div class="col-md-4">
                                    <label><%=Resources.Language.Lbl_customers_date %></label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="txtOpeningDueDate" class="form-control datepickerCSS"/>
                                </div>
                            </div>

                            <div class="modal-footer" runat="server" id="Div2">
                                <button type="button" class="btn btnCustomize" data-dismiss="modal"><%=Resources.Language.Btn_customers_close %></button>
                                <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnCustomerOpeningDue"><%=Resources.Language.Btn_customers_add %></button>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </div>



    <!-- Advance custom amount -->
    <div class="modal-customer-opening-due">
        <div class="modal fade" tabindex="-1" role="dialog" id="advanceModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="lblTitleAdvance"><%=Resources.Language.Lbl_customers_add_advance %> </h4>
                        <asp:Label ID="lblCusIdAdvance" runat="server" Text="" CssClass="disNone"></asp:Label>
                    </div>
                    <div class="modal-body">

                        <div id="msgOutputAdvance" class="text-left"></div>

                        <div class="view-customer">

                            <div class="form-group group-section">
                                <div class="col-md-4">
                                    <label><%=Resources.Language.Lbl_customers_advance_amount %></label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="txtAdvanceAmt" class="form-control float-number-validate" onkeydown="return (event.keyCode!=13);"/>
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <div class="col-md-4">
                                    <label><%=Resources.Language.Lbl_customers_date%></label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="txtAdvanceDueDate" class="form-control datepickerCSS" onkeydown="return (event.keyCode!=13);">
                                </div>
                            </div>

                            <div class="modal-footer" runat="server" id="Div3">
                                <button type="button" class="btn btnCustomize" data-dismiss="modal"><%=Resources.Language.Btn_customers_close %></button>
                                <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnCustomerAdvance"><%=Resources.Language.Btn_customers_add %></button>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </div>





    <!-- Payment -->

    <!-- Advance custom amount -->
    <div class="modal-customer-opening-due">
        <div class="modal fade" tabindex="-1" role="dialog" id="paymentModal">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title"><%=Resources.Language.Lbl_customers_payment %> :
                                    <asp:Label ID="lblPaymentCusName" runat="server" Text=""></asp:Label>
                        </h4>
                        <asp:Label ID="lblPaymentCusId" runat="server" Text="" CssClass="disNone"></asp:Label>
                    </div>
                    <div class="modal-body">

                        <div id="msgOutputPayment" class="text-left"></div>

                        <div class="view-customer">

                            <div class="form-group group-section">
                                <div class="col-md-4">
                                    <label><%=Resources.Language.Lbl_customers_amount %></label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="txtPaymentAmt" class="form-control float-number-validate" onkeydown="return (event.keyCode!=13);" />
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <div class="col-md-4">
                                    <label><%=Resources.Language.Lbl_customers_date %></label>
                                </div>
                                <div class="col-md-8">
                                    <input type="text" id="txtPaymentDueDate" class="form-control datepickerCSS" onkeydown="return (event.keyCode!=13);" />
                                </div>
                            </div>

                            <div class="modal-footer" runat="server" id="Div4">
                                <button type="button" class="btn btnCustomize" data-dismiss="modal"><%=Resources.Language.Btn_customers_close %></button>
                                <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom" id="btnCustomerPayment"><%=Resources.Language.Btn_customers_add %></button>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </div>



    <asp:HiddenField ID="lblHiddenCompanyName" runat="server" />
    <asp:HiddenField ID="lblHiddenCompanyAddress" runat="server" />
    <asp:HiddenField ID="lblHiddenCompanyPhone" runat="server" />


    <script type="text/javascript" src="<%= ResolveUrl("~/Admin/CustomerBundle/Script/customer-grid-load.js?v0.0094") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/Admin/CustomerBundle/Script/customer-main.js?v0.0094") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/Admin/CustomerBundle/Script/customer-upsert.js?v0.0094") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/Js/lodash.min.js") %>"></script>

    <script>

        function CloseModal() {
            $('#windowTitleDialog').modal('hide');
        }

        $(document).ready(function () {
            $('#example-getting-started').multiselect();
        });

    </script>



    <script>
        activeModule = "sale";

        var ID = "<% =Resources.Language.Lbl_service_id %>";
        var Name = "<% =Resources.Language.Lbl_customers_name %>";
        var Address = "<% =Resources.Language.Lbl_customers_address %>";
        var Phone = "<% =Resources.Language.Lbl_customers_phone %>";
        var Total_Balance= "<% =Resources.Language.Lbl_customers_total_balance %>";
        var Action = "<% =Resources.Language.Lbl_customers_action %>";

        var View = "<% =Resources.Language.Lbl_customers_view %>";
        var Add_Payment = "<% =Resources.Language.Lbl_customers_add_payment %>";
        var Add_Advance = "<% =Resources.Language.Lbl_customers_add_advance %>";
        var Add_Opening_Due = "<% =Resources.Language.Lbl_customers_add_opening_due %>";
        var Invoices = "<% =Resources.Language.Lbl_customers_invoices %>";
        var Full_Ledger = "<% =Resources.Language.Lbl_customers_full_ledger %>";
        var Edit = "<% =Resources.Language.Lbl_customers_edit %>";
        var Delete = "<% =Resources.Language.Lbl_customers_delete %>";
        var Total = "<% =Resources.Language.Lbl_customers_total %>";
        var Email = "<% =Resources.Language.Lbl_customers_email %>"; 

        var Customer_Details = "<% =Resources.Language.Lbl_customers_customer_details %>";
        var Designation = "<% =Resources.Language.Lbl_customers_designation %>";
        var Notes = "<% =Resources.Language.Lbl_customers_notes %>";
        var CustomerType = "<% =Resources.Language.Lbl_customers_customer_type %>";
        var BtnClose = "<% =Resources.Language.Btn_customers_close %>";


        var Delete_Customer = "<% =Resources.Language.Lbl_customers_delete_customer %>";
        var CancelCustomer = "<% =Resources.Language.Btn_customers_cancel %>";


    </script>
</asp:Content>
