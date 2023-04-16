<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Slip.aspx.cs" Inherits="MetaPOS.Admin.Slip" %>

<%@ Import Namespace="System.Activities.Statements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Report- metaPOS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Cancel.css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/ReportBundle/Content/Slip-responsive.css?v=0.001") %>" rel="stylesheet" />

    <script type="text/javascript">

        // Document ready function
        $(document).ready(function () {
            wrapChange();
        });


        // Text wrap
        function wrapChange() {
            $("#contentBody_grdSlip tr").removeAttr("style");
        }

    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div class="section">
                <h2 class="sectionBreadcrumb " id="lblSlipTitle" runat="server"></h2>
                <asp:Label ID="lblTest" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-md-12 col-sm-12 col-xs-12 gridTitle form-inline">

                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStoreList_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList runat="server" ID="ddlUserList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserList_OnSelectedIndexChanged" />
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList runat="server" ID="ddlCusType" AutoPostBack="true" OnSelectedIndexChanged="txtFrom_TextChanged">
                            <asp:ListItem Value="Search All" Selected="True" Text="<%$Resources:Language, Lbl_invoiceReport_all_customer %>"></asp:ListItem>
                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_invoiceReport_consumer %>"></asp:ListItem>
                            <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_invoiceReport_dealer %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList runat="server" ID="ddlReferredBy" AutoPostBack="true" OnSelectedIndexChanged="txtFrom_TextChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList ID="ddlTransection" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlTransection_SelectedIndexChanged">
                            <asp:ListItem Value="1" Selected="True" Text="<%$Resources:Language, Lbl_invoiceReport_invoice_in_group %>"></asp:ListItem>
                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_invoiceReport_invoice_in_detail %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList ID="ddlSlipStatus" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlTransection_SelectedIndexChanged">
                            <asp:ListItem Value="" Selected="True" Text="<%$Resources:Language, Lbl_invoiceReport_select_status %>"></asp:ListItem>
                            <asp:ListItem Value="sale" Text="<%$Resources:Language, Lbl_invoiceReport_sold %>"></asp:ListItem>
                            <asp:ListItem Value="Resold" Text="<%$Resources:Language, Lbl_invoiceReport_resold %>"></asp:ListItem>
                            <asp:ListItem Value="Suspended" Text="<%$Resources:Language, Lbl_invoiceReport_suspended %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:DropDownList ID="ddlDateSearch" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDateSearch_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_invoiceReport_custom_date %>"></asp:ListItem>
                            <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_invoiceReport_any_date %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:TextBox ID="txtFrom" CssClass="tSearch form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="txtFrom_TextChanged"></asp:TextBox>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:TextBox ID="txtTo" CssClass="tSearch form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="txtFrom_TextChanged"></asp:TextBox>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:TextBox ID="txtSearchInvoiceNo" CssClass="form-control" runat="server" placeholder="<%$Resources:Language, Lbl_invoiceReport_search %>" AutoPostBack="true" OnTextChanged="txtFrom_TextChanged"></asp:TextBox>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:TextBox ID="txtSearchProduct" CssClass="form-control" runat="server" placeholder="<%$Resources:Language, Lbl_invoiceReport_search_product %>" AutoPostBack="true" OnTextChanged="txtFrom_TextChanged"></asp:TextBox>
                    </div>
                    <div class="form-group margin-bottom-10 float-left margin-left-7">
                        <asp:LinkButton ID="btnPrint" runat="server" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="scroll">
                <asp:GridView ID="grdSlip"
                    runat="server"
                    OnSelectedIndexChanged="grdSlip_SelectedIndexChanged"
                    AutoGenerateColumns="False"
                    DataKeyNames="Id"
                    CssClass="mGrid gBox scrollBar"
                    EmptyDataText="No data records found."
                    ViewStateMode="Enabled"
                    AllowPaging="True"
                    OnPageIndexChanging="grdSlip_PageIndexChanging"
                    OnRowDataBound="grdSlip_RowDataBound">
                    <Columns>
                        <%--<asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="False" />--%>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_sl %>">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_cus_name %>" SortExpression="cusName" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("cusName") %>' ID="lblCusName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_cus_id %>" SortExpression="cusID">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("cusID") %>' ID="lblCusID"></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_invoice %>" SortExpression="billNo">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("billNo") %>' ID="lblInvoice"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text="<%$Resources:Language, Lbl_invoiceReport_total %>"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_total_net %>" SortExpression="netAmt">
                            <ItemTemplate>
                                <asp:Label ID="lblNetAmt" runat="server" Text='<%# Bind("netAmt") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_total %>" SortExpression="grossAmt">
                            <ItemTemplate>
                                <asp:Label ID="lblGrossAmt" runat="server" Text='<%# Bind("grossAmt") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalGrossAmt" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_disc %>" SortExpression="discAmt">
                            <ItemTemplate>
                                <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("discAmt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_misc %>" SortExpression="miscCost">
                            <ItemTemplate>
                                <asp:Label ID="lblMiscCost" runat="server" Text='<%# Bind("miscCost") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label runat="server" ID="lblTotalMiscCost"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_paid %>" SortExpression="payCash">
                            <ItemTemplate>
                                <asp:Label ID="lblReceivedAmt" runat="server" Text='<%# Bind("payCash") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalReceiveAmt" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_balance %>" SortExpression="giftAmt" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDueAmt" runat="server" Text='<%# Bind("giftAmt") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalDue" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_salesperson %>" SortExpression="name">
                            <ItemTemplate>
                                <asp:Label ID="lblSalesPerson" runat="server" Text='<%# Convert.ToBoolean(Eval("isAutoSalesPerson")) == true?  Eval("title") : Eval("name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_inv_as %>" SortExpression="CusType" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceAs" runat="server" Text='<%# Bind("CusType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="serialNo" HeaderText="<%$Resources:Language, Lbl_invoiceReport_serial_no %>" SortExpression="serialNo" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="entryDate" HeaderText="<%$Resources:Language, Lbl_invoiceReport_invoice_date %>" SortExpression="entryDate" DataFormatString="{0:dd-MMM-yyyy  hh:mm tt}" ItemStyle-Width="16%" />
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_referal_name %>" SortExpression="referalName">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("referalName") %>' ID="TextBox1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("referalName") %>' ID="Label1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_status %>" SortExpression="status">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("status") %>' ID="lblShowStatus"></asp:Label>
                                <asp:Label runat="server" Text='<%# Bind("status") %>' ID="lblStatus" Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="storeName" HeaderText="<%$Resources:Language, Lbl_invoiceReport_store %>" SortExpression="storeName" />
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_invoiceReport_action %>" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDetails" runat="server" CssClass="btn btn-design glyIconPosition" CausesValidation="False" Text="<span class='glyphicon glyphicon-shopping-cart'></span>" CommandName="Select" data-toggle="tooltip" data-placement="top" title="Go Invoice" data-trigger="hover"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Wrap="False" />
                    <RowStyle Wrap="False" />
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>

            </div>
        </div>
    </div>


    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);


        function BeginRequestHandler(sender, args) {

        }


        function EndRequestHandler(sender, args) {
            wrapChange();
        }


        $(function () {
            $('[id*=lsSlipShow]').multiselect({
                includeSelectAllOption: true
            });
        });

    </script>

    <script>
        activeModule = "report";
    </script>


</asp:Content>
