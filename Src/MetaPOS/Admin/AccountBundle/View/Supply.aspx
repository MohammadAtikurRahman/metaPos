<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Supply.aspx.cs" Inherits="MetaPOS.Admin.AccountBundle.View.Purchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Supply Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Purchase.css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/AccountBundle/Content/Supply-responsive.css?v=0.001") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div id="divHeaderPanel">
                <label class="title"><%=Resources.Language.Title_supply %></label>
                <asp:Button ID="btnAddSupplier" CssClass="btn btn-info btn-sm btnResize btnAddCustom" runat="server" Text="<%$Resources:Language, Btn_supply_add_supplier %>" OnClick="btnAddSupplier_Click" />
            </div>
        </div>
    </div>


    <div class="row headerDesign">
        <!-- Cash In Panel False -->
        <asp:Panel ID="pnlCashIn" runat="server" DefaultButton="btnCashInAdd">
            <div class="col-md-4 col-sm-6 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading"><%=Resources.Language.Lbl_supply_opening_balance %></h2>
                    <div class="form-group disNone">
                        <asp:Label ID="Label1" CssClass="lbl col-sm-4 col-xs-4 control-label lang-supply-particular" runat="server" Text="Particular Type"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:DropDownList ID="ddlCashInType" CssClass=" form-control" runat="server">
                                <asp:ListItem Value="Supplier Payment lang-supply-supplier-payment">Supplier Payment</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="div1" runat="server">
                        <asp:Label ID="Label2" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_supplier %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:DropDownList ID="ddlCashInTypeSup" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="div2" runat="server">
                        <asp:Label ID="Label3" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_description %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:TextBox ID="txtCashInMainDescr" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_amount %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:TextBox ID="txtCashInAmount" CssClass=" form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCashInAmount" ValidationGroup="cashIn" ErrorMessage="Invalid!" ValidationExpression="^[-+]?[0-9]{1,8}([\.][0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label5" CssClass="lbl col-sm-4 col-xs-4 control-label lang-supply-date" runat="server" Text="<%$Resources:Language, Lbl_supply_date %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:TextBox ID="txtOpeinginDueDate" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group padding-bottom-70">
                        <asp:Button ID="btnCashInAdd" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnAddCustom btnAddOpt" runat="server" ValidationGroup="cashIn" Text="<%$Resources:Language, Btn_supply_add %>" OnClick="btnCashInAdd_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCashOut" runat="server" DefaultButton="btnCashOutAdd">
            <div class="col-md-4 col-sm-6 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading"><%=Resources.Language.Lbl_supply_give_payment %></h2>
                    <div class="form-group disNone">
                        <asp:Label ID="lblCashOutType" CssClass="lbl col-sm-4 col-xs-4 control-label " runat="server" Text="Particular Type"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:DropDownList ID="ddlCashOutType" CssClass=" form-control" runat="server">
                                <asp:ListItem Value="Supplier Payment lang-supply-supplier-payment">Supplier Payment</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutTypeSup" runat="server">
                        <asp:Label ID="lblCashOutTypeSup" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_supplier %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:DropDownList ID="ddlCashOutTypeSup" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutDescriptions" runat="server">
                        <asp:Label ID="lblCashOutMainDescr" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_description %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:TextBox ID="txtCashOutMainDescr" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashOutAmount" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_amount %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:TextBox ID="txtCashOutAmount" CssClass=" form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revCashOutAmount" runat="server" ControlToValidate="txtCashOutAmount" ValidationGroup="cashOut" ErrorMessage="Invalid!" ValidationExpression="^[-+]?[0-9]{1,10}([\.][0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label7" CssClass="lbl col-sm-4 col-xs-4 control-label lang-supply-date" runat="server" Text="<%$Resources:Language, Lbl_supply_date %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:TextBox ID="txtGivePaymentDate" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group padding-bottom-70">
                        <asp:Button ID="btnCashOutAdd" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnAddCustom btnAddOpt" runat="server" ValidationGroup="cashOut" Text="<%$Resources:Language, Btn_supply_add %>" OnClick="btnCashOutAdd_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="cashClear"></div>
        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnCashSearch">
            <div class="col-md-4 col-sm-6 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading"><%=Resources.Language.Lbl_supply_search %></h2>
                    <div class="form-group"  runat="server" id="divStoreList">
                        <asp:Label ID="Label8" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_store %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStoreList_OnSelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group"  runat="server" id="divUserList">
                        <asp:Label ID="Label9" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_user %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:DropDownList runat="server" ID="ddlUserList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserList_OnSelectedIndexChanged"/>
                        </div>
                    </div>
                    <div class="form-group disNone">
                        <asp:Label ID="lblCashSearchType" CssClass="lbl col-sm-4 col-xs-4 control-label " runat="server" Text="Particular Type"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:DropDownList ID="ddlCashSearchType" CssClass=" form-control" runat="server">
                                <asp:ListItem Value="Supplier Payment">Supplier Payment</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divSearchSup" runat="server">
                        <asp:Label ID="lblSearchSup" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_by_supplier %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:DropDownList ID="ddlSearchSup" CssClass="form-control" runat="server" OnSelectedIndexChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSearchDateFrom" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_date_from %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:TextBox ID="txtSearchDateFrom" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSearchDateTo" CssClass="lbl col-sm-4 col-xs-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_supply_date_to %>"></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:TextBox ID="txtSearchDateTo" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="">
                        <asp:Label runat="server" CssClass="lbl col-sm-4 col-xs-4 control-label" Text=""></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:CheckBox runat="server" ID="chkAnyDate" AutoPostBack="True" Text="<%$Resources:Language, Lbl_supply_any_date %>" CssClass="checkbox" />
                        </div>
                    </div>
                    <div class="">
                        <asp:Label runat="server" CssClass="lbl col-sm-4 col-xs-4 control-label" Text=""></asp:Label>
                        <div class="col-sm-8 col-xs-8">
                            <asp:CheckBox runat="server" ID="chkSchedule" AutoPostBack="True" Text="<%$Resources:Language, Lbl_supply_schedule %>" CssClass="checkbox" />
                        </div>
                    </div>
                    <div class="form-group padding-bottom-6">
                        <asp:Button ID="btnCashSearch" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnSearchCustom" runat="server" Text="<%$Resources:Language, Btn_supply_search %>" OnClick="btnCashSearch_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="cashClear"></div>
    </div>
    <div style="clear: both"></div>

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-xs-4 gridTitle text-left">
                    <label><%=Resources.Language.Lbl_supply_supply_report %></label>
                </div>
                <div class="col-xs-8 gridTitle text-right form-inline">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblTotalStock" CssClass="control-label text-capitalize text-primary msg-report" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblTotalDueBalance" CssClass="control-label text-capitalize text-primary msg-report" Visible="False"></asp:Label>
                    </div>
                    <div class="btn-group" role="group">
                        <asp:LinkButton runat="server" ID="btnPrint" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:GridView ID="grdCashReportInfo" OnRowDataBound="grdCashReportInfo_RowDataBound" 
                OnRowCommand="grdCashReportInfo_RowCommand" OnRowDeleted="grdCashReportInfo_RowDeleted" 
                OnRowDeleting="grdCashReportInfo_RowDeleting" CssClass="mGrid gBox scrollBar" ShowFooter="true" 
                OnPageIndexChanging="grdCashReportInfo_OnPageIndexChanging"
                AllowPaging="True" PageSize="10" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" 
                 EmptyDataText="There are no data records to display." 
                ViewStateMode="Enabled" RowStyle-Wrap="true">
                <Columns>
                    <asp:TemplateField HeaderText="Id" SortExpression="Id" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_sl %>">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_source %>" SortExpression="cashType">
                        <ItemTemplate>
                            <asp:Label ID="lblCashType" runat="server" Text='<%# Bind("cashType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_particular%>" SortExpression="descr">
                        <ItemTemplate>
                            <asp:Label ID="lblDescr" runat="server" Text='<%# Bind("descr") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblParticular" runat="server" Text='<%# Bind("Particular") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_p_code %>" SortExpression="purchaseCode">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("purchaseCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_details %>" SortExpression="mainDescr" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblMainDescr" runat="server" Text='<%# Bind("mainDescr") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblMainDescr" runat="server" Text="Total : "></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_p_amount %>" SortExpression="cashIn">
                        <ItemTemplate>
                            <asp:Label ID="lblCashIn" runat="server" Text='<%# Bind("cashIn") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCashIn" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_payment%>" SortExpression="cashOut" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCashOut" runat="server" Text='<%# Bind("cashOut") %>' Visible="False"></asp:Label>
                            <asp:LinkButton ID="btnGrdSchedulePay" runat="server" CssClass="btn btn-design btn-pay-opening-due" CausesValidation="False" CommandName="schedulePayment" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%# Bind("cashOut") %>' data-toggle="tooltip" data-placement="top" title="View Invoice" data-trigger="hover"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCashOut" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash-In-Hand" SortExpression="cashInHand" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCashInHand" runat="server" Text='<%# Bind("cashInHand") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_date %>" SortExpression="entryDate" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Schedule Pay" SortExpression="entryDate" ItemStyle-Width="11%" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblScheduleStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("isScheduled")) == true ? "Yes" : "No" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_store %>" SortExpression="storeName" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblStoreName" runat="server" Text='<%# Bind("storeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_supply_receipt %>">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnRecipt" runat="server" CssClass="btn btn-design" CausesValidation="false" CommandName="Receipt" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<span class="glyphicon glyphicon-print"></span>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" CssClass="btn  btn-design" OnClientClick=" return confirm('Are you sure you want to delete selected record ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
        </div>


        <div class="modal fade" id="schedulePayment" tabindex="-1" role="dialog" aria-labelledby="schedulePayment">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title" id="OpeningDuePayLabel">
                            <asp:Label ID="lblCashId" runat="server" Text="" CssClass="disNone"></asp:Label>
                            <asp:Label ID="lblSupplierId" runat="server" Text="" CssClass="disNone"></asp:Label>
                            <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                        </h3>
                    </div>
                    <div class="modal-body">
                        <div class="openingDuePay ">
                            <div class="form-inline">
                                <div class="form-group col-md-12">
                                    <asp:TextBox ID="txtSchedulePayment" runat="server" CssClass="form-control paydueCustom"></asp:TextBox>
                                    <asp:Button ID="btnShedulePay" runat="server" Text="<%$Resources:Language, Btn_supply_pay_now %>" CssClass="btn btn-primary btnDeleteOpt" OnClick="btnShedulePay_OnClick" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="border: none">
                        <button type="button" class="btn btn-default" data-dismiss="modal"><%=Resources.Language.Btn_supply_close %></button>
                    </div>
                </div>
            </div>
        </div>


    </div>
    
    <script src="<%#ResolveUrl("~/Admin/AccountBundle/Script/account-supply.js?v=0.1") %>"></script>

    <script>
        activeModule = "accounting";
    </script>

</asp:Content>
