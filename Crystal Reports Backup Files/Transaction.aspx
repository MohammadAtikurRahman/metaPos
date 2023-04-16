<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="MetaPOS.Admin.AnalyticBundle.View.Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Transaction Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/ReportBundle/Content/transection-report.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/ReportBundle/Content/transaction-report-responsive.css?v=0.001") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div class="section">
                <div class="row">
                    <h2 class="sectionBreadcrumb lang-reports-transaction col-md-8">Transaction</h2>
                    <asp:Label runat="server" ID="lblTotalDueBalance" CssClass="control-label text-capitalize text-primary msg-report label-transaction-total-bal col-md-4"></asp:Label>
                </div>
                <asp:Label runat="server" ID="lblTest"></asp:Label>
            </div>
        </div>

    </div>

    <div class="row headerDesign disNone">
        <asp:Panel ID="pnlCashIn" runat="server" DefaultButton="btnCashInAdd">
            <div class="col-md-4 col-sm-12 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading">Cash In</h2>
                    <div class="form-group">
                        <asp:Label ID="lblCashInType" CssClass="lbl col-sm-4 control-label" runat="server" Text="Particular"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashInType" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlCashInType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashInTypeSup" runat="server" visible="false">
                        <asp:Label ID="lblCashInTypeSup" CssClass="lbl col-sm-4 control-label" runat="server" Text="Supplier"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashInTypeSup" CssClass="form-control" runat="server" OnTextChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashInTypeStaff" runat="server" visible="false">
                        <asp:Label ID="lblCashInTypeStaff" CssClass="lbl col-sm-4 control-label" runat="server" Text="Staff"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashInTypeStaff" CssClass="form-control" runat="server" OnTextChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashInMainDescr" CssClass="lbl col-sm-4 control-label" runat="server" Text="Description"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashInMainDescr" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashInAmount" CssClass="lbl col-sm-4 control-label" runat="server" Text="Amount"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashInAmount" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revCashInAmount" runat="server" ControlToValidate="txtCashInAmount" ErrorMessage="Invalid!" ValidationExpression="^[\-]?[0-9]{1,8}([\.][0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnCashInAdd" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnAddCustom btnAddOpt" runat="server" Text="Add" OnClick="btnCashInAdd_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="cashClear"></div>

        <asp:Panel ID="pnlCashOut" runat="server" DefaultButton="btnCashOutAdd">
            <div class="col-md-4 col-sm-12 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading">Cash out</h2>
                    <div class="form-group">
                        <asp:Label ID="lblCashOutType" CssClass="lbl col-sm-4 control-label " runat="server" Text="Particular"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashOutType" CssClass=" form-control" runat="server" OnSelectedIndexChanged="ddlCashOutType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutTypeSup" runat="server" visible="false">
                        <asp:Label ID="lblCashOutTypeSup" CssClass="lbl col-sm-4 control-label" runat="server" Text="Supplier"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashOutTypeSup" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutTypeStaff" runat="server" visible="false">
                        <asp:Label ID="lblCashOutTypeStaff" CssClass="lbl col-sm-4 control-label" runat="server" Text="Staff"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashOutTypeStaff" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutDescriptions" runat="server" visible="true">
                        <asp:Label ID="lblCashOutMainDescr" CssClass="lbl col-sm-4 control-label" runat="server" Text="Description"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashOutMainDescr" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashOutAmount" CssClass="lbl col-sm-4 control-label" runat="server" Text="Amount"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashOutAmount" CssClass=" form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revCashOutAmount" runat="server" ControlToValidate="txtCashOutAmount" ErrorMessage="Invalid!" ValidationExpression="^[\-]?[0-9]{1,8}([\.][0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnCashOutAdd" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnAddCustom btnAddOpt" runat="server" Text="Add" OnClick="btnCashOutAdd_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

    <div class="row gridTopButton">
        <div class="btn-group btnGroupDesignLeft" role="group">
            <asp:Button ID="btnAddPaticular" CssClass="btn btn-info btnAddCustom btnAddOpt disNone" runat="server" Text="Add Particular" OnClick="btnAddPaticular_Click" />
        </div>
        <%--<div class="btn-group btnGroupDesign" role="group">
            <asp:Button ID="btnPrint" CssClass="btn btn-primary btnPrintCustom" runat="server" Text="Print" OnClick="btnPrint_Click" />
        </div>--%>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div <%--class="gridHeader"--%>>
                <div class="col-md-12 col-sm-12 col-xs-12 gridTitle form-inline transection">

                    <div class="form-group float-left margin-left-7">
                        <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStoreList_OnSelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="form-group float-left margin-left-7">
                        <asp:DropDownList runat="server" ID="ddlUserList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserList_OnSelectedIndexChanged" />
                    </div>
                    <div class="form-group float-left margin-left-7">
                        <asp:Label ID="lblCashSearchType" runat="server" Text="Particular" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlCashSearchType" CssClass=" form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCashSearchType_SelectedIndexChanged">
                            <asp:ListItem Value="SearchAll">Search All</asp:ListItem>
                            <asp:ListItem Value="0">Supply</asp:ListItem>
                            <asp:ListItem Value="5">Sales</asp:ListItem>
                            <asp:ListItem Value="3">Receive</asp:ListItem>
                            <asp:ListItem Value="2">Expense</asp:ListItem>
                            <asp:ListItem Value="1">Salary</asp:ListItem>
                            <asp:ListItem Value="4">Banking</asp:ListItem>
                            <asp:ListItem Value="6">Advance</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group disNone float-left margin-left-7" id="divSearchSup" runat="server" visible="false">
                        <asp:Label ID="Label2" runat="server" Text="Supplier Name"></asp:Label>
                        <asp:DropDownList ID="ddlSearchSup" CssClass="form-control" runat="server" OnSelectedIndexChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="form-group disNone float-left margin-left-7" id="divSearchStaff" runat="server" visible="false">
                        <asp:Label ID="Label1" runat="server" Text="Staff Name"></asp:Label>
                        <asp:DropDownList ID="ddlSearchStaff" CssClass="form-control" runat="server" OnSelectedIndexChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="form-group float-left margin-left-7">
                        <asp:TextBox ID="txtSearchDateFrom" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                    </div>
                    <span class="float-left">To</span>
                    <div class="form-group float-left margin-left-7">
                        <asp:TextBox ID="txtSearchDateTo" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                    </div>
                    <div class="form-group float-left margin-left-7">
                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" AutoPostBack="true" placeholder="Search..." OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                    </div>
                    <div class="form-group float-left margin-left-7">
                        <asp:LinkButton runat="server" ID="btnPrint" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                    </div>
                    <asp:Button ID="btnCashSearch" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnSearchCustom disNone" runat="server" Text="Search" OnClick="btnCashSearch_Click" />
                </div>

            </div>
        </div>


        <div class="col-md-12 scroll">
            <asp:GridView ID="grdCashReportInfo" OnRowDataBound="grdCashReportInfo_RowDataBound" OnRowDeleted="grdCashReportInfo_RowDeleted"
                OnRowDeleting="grdCashReportInfo_RowDeleting" CssClass="mGrid gBox scrollBar"
                ShowFooter="true" AllowPaging="True" PageSize="15" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="Id" OnPageIndexChanging="grdCashReportInfo_OnPageIndexChanging"
                EmptyDataText="There are no data records to display." ViewStateMode="Enabled"
                RowStyle-Wrap="true" OnRowCommand="grdCashReportInfo_RowCommand" AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="Id" SortExpression="Id" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SL" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Source" SortExpression="cashType" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblCashType" runat="server" Text='<%# Bind("cashType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Particular" SortExpression="descr" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblDescr" runat="server" Text='<%# Bind("descr")%>' Visible="False"></asp:Label>
                            <asp:Label ID="lblParticular" runat="server" Text='<%# Bind("Particular")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details" SortExpression="mainDescr" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <%# Eval("billNo").ToString() == "" ? Eval("mainDescr").ToString() : "Invoice No:" %>
                            <asp:Label ID="lblMainDescr" runat="server" Text='<%# Bind("billNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Store" SortExpression="storeName" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblStore" runat="server" Text='<%# Bind("storeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" SortExpression="entryDate" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblMainTotal" runat="server" Text="Total : "></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash In" SortExpression="cashIn" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCashIn" runat="server" Text='<%# Bind("cashIn") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCashIn" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash Out" SortExpression="cashOut" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCashOut" runat="server" Text='<%# Bind("cashOut") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCashOut" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash-In-Hand" SortExpression="cashInHand" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblCashInHand" runat="server" Text='<%# Bind("cashInHand") %>'></asp:Label>
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
    </div>

    <script>
        activeModule = "report";
    </script>

</asp:Content>
