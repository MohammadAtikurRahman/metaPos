<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Salary.aspx.cs" Inherits="MetaPOS.Admin.AccountBundle.View.Salary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Salary Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Transaction.css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/AccountBundle/Content/Salary-responsive.css?v=0.000") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div class="section">
                <h2 class="sectionBreadcrumb lang-accounting-salary">Salary</h2>
                <asp:Label runat="server" ID="lblTest"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row headerDesign">
        <asp:Panel ID="pnlCashOut" runat="server" DefaultButton="btnCashOutAdd">
            <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading lang-accounting-add-salary">Add Salary</h2>
                    <div class="form-group disNone">
                        <asp:Label ID="lblCashOutType" CssClass="lbl col-sm-4 control-label " runat="server" Text="Particular Type"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashOutType" CssClass=" form-control" runat="server">
                                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutTypeStaff" runat="server">
                        <asp:Label ID="lblCashOutTypeStaff" CssClass="lbl col-sm-4 control-label lang-accounting-select-staff" runat="server" Text="Select Staff"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashOutTypeStaff" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutDescriptions" runat="server">
                        <asp:Label ID="lblCashOutMainDescr" CssClass="lbl col-sm-4 control-label lang-accounting-details-of-salary" runat="server" Text="Details of Salary"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashOutMainDescr" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashOutAmount" CssClass="lbl col-sm-4 control-label lang-accounting-salary-amount" runat="server" Text="Salary Amount"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashOutAmount" CssClass=" form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revCashOutAmount" runat="server" ControlToValidate="txtCashOutAmount" ErrorMessage="Invalid!" ValidationExpression="^[\-]?[0-9]{1,8}([\.][0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" CssClass="lbl col-sm-4 control-label lang-accountig-salary-date" runat="server" Text="Salary Date"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSalaryDate" CssClass="form-control datepickerCSS" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="padding-bottom-74">
                        <asp:Button ID="btnCashOutAdd" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnAddCustom btnAddOpt" runat="server" Text="Add" OnClick="btnCashOutAdd_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="cashClear"></div>
        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnCashSearch">
            <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading lang-accounting-search">Search</h2>
                    <div class="form-group" id="divStoreList" runat="server">
                        <asp:Label ID="Label2" CssClass="lbl col-sm-4 control-label lang-accounting-store" runat="server" Text="Store"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStoreList_OnSelectedIndexChangedlStoreList_OnSelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group"  runat="server" id="divUserList">
                        <asp:Label ID="Label9" CssClass="lbl col-sm-4 control-label lang-accounting-user" runat="server" Text="User"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlUserList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserList_OnSelectedIndexChanged"/>
                        </div>
                    </div>
                    <div class="form-group disNone">
                        <asp:Label ID="lblCashSearchType" CssClass="lbl col-sm-4 control-label " runat="server" Text="Particular Type"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashSearchType" CssClass=" form-control" runat="server">
                                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divSearchStaff" runat="server">
                        <asp:Label ID="lblSearchStaff" CssClass="lbl col-sm-4 control-label lang-accounting-by-btaff" runat="server" Text="By Staff"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSearchStaff" CssClass="form-control" runat="server" OnSelectedIndexChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSearchDateFrom" CssClass="lbl col-sm-4 control-label lang-accounting-date-from" runat="server" Text="Date From"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSearchDateFrom" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSearchDateTo" CssClass="lbl col-sm-4 control-label lang-accounting-date-to" runat="server" Text="Date To"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSearchDateTo" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="padding-bottom-45">
                        <asp:Button ID="btnCashSearch" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnSearchCustom" runat="server" Text="Search" OnClick="btnCashSearch_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="cashClear"></div>
    </div>
    <div style="clear: both"></div>
    <div class="row gridTopButton">
        <div class="btn-group btnGroupDesignLeft" role="group">
            <asp:Button ID="btnAddPaticular" CssClass="btn btn-info btnAddCustom btnAddOpt" runat="server" Text="Add Staff" OnClick="btnAddPaticular_Click" />
        </div>
        <div class="btn-group btnGroupDesign disNone" role="group">
            <asp:Button ID="btnPrint" CssClass="btn btn-primary btnPrintCustom" runat="server" Text="Print" OnClick="btnPrint_Click" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-xs-4 gridTitle text-left lang-accounting-salary-report">
                    <label>Salary Report</label>
                </div>
                <div class="col-xs-8 gridTitle text-right form-inline">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblTotalDueBalance" CssClass="control-label text-capitalize text-primary msg-report"></asp:Label>
                    </div>
                    <div class="btn-group" role="group">           
                        <asp:LinkButton runat="server" ID="btnPrintSalary" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:GridView ID="grdCashReportInfo" OnRowDataBound="grdCashReportInfo_RowDataBound" 
                OnRowDeleted="grdCashReportInfo_RowDeleted" OnRowDeleting="grdCashReportInfo_RowDeleting" 
                CssClass="mGrid gBox" ShowFooter="true" AllowPaging="True" PageSize="10" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="Id"  OnPageIndexChanging="grdCashReportInfo_OnPageIndexChanging"
                EmptyDataText="There are no data records to display." ViewStateMode="Enabled" RowStyle-Wrap="true" AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="Id" SortExpression="Id" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SL">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Source" SortExpression="cashType">
                        <ItemTemplate>
                            <asp:Label ID="lblCashType" runat="server" Text='<%# Bind("cashType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Particular" SortExpression="descr">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Particular") %>'></asp:Label>
                            <%--<asp:Label ID="lblDescr" runat="server" Text='<%# Bind("descr")%>'></asp:Label>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details" SortExpression="mainDescr" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="lblMainDescr" runat="server" Text='<%# Bind("mainDescr") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblMainDescr" runat="server" Text="Total : "></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash-In" SortExpression="cashIn" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCashIn" runat="server" Text='<%# Bind("cashIn") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCashIn" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash-Out" SortExpression="cashOut"  ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCashOut" runat="server" Text='<%# Bind("cashOut") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Date" SortExpression="entryDate"  ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Store" SortExpression="storeName"  ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblStoreName" runat="server" Text='<%# Bind("storeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" CssClass="btn btn-design btnDeleteOpt" OnClientClick=" return confirm('Are you sure you want to delete selected record ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>

        </div>
    </div>
    
    <script>
        activeModule = "accounting";
    </script>

</asp:Content>