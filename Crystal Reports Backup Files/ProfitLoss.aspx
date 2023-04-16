<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProfitLoss.aspx.cs" Inherits="MetaPOS.Admin.ReportBundle.View.ProfitLoss" %>

<%@ Import Namespace="System.ComponentModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Profit / Loss
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/ReportBundle/Content/ProfitLoss.css") %>" />
    <link href="<%= ResolveUrl("~/Admin/ReportBundle/Content/transection-report.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/ReportBundle/Content/ProfitLoss-responsive.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="profit-loss">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
                <div id="divHeaderPanel" class="profit-title">
                    <label class="title lang-reports-profit-loss">Profit/Loss</label>
                </div>
                <asp:Label runat="server" ID="lblTest"></asp:Label>

            </div>

            <div class="">
                <div class="col-md-3 col-sm-6 col-xs-6 ">
                    <div class="section profit-loss-design">
                        <h2 runat="server" id="lblRevenue">500.00</h2>
                        <b>Revenue Amount</b>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="section profit-loss-design">
                        <h2 runat="server" id="lblExpense">500.00</h2>
                        <b>Expense Amount</b>
                    </div>

                </div>
                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="section profit-loss-design">
                        <h2 runat="server" id="lblSupplierDue">500.00</h2>
                        <b>Supplier Due Amount</b>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-6">
                    <div class="section profit-loss-design">
                        <h2 runat="server" id="lblProfitLoss">500.00</h2>
                        <b runat="server" id="lblProfitLossTxt">Profit/Loss</b>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="gridHeader">
                    <div class="col-md-3 col-sm-3 col-xs-4 gridTitle text-left">
                        <label class="lang-reports-loss-profit-histroy">Loss/Profit History</label>
                    </div>
                    <div class="col-md-9 col-sm-9 col-xs-8 gridTitle text-right form-inline">

                        <div class="form-group stock-search-field chk-profit-loss float-left">
                            <asp:CheckBox ID="chkAllProfitLoss" runat="server" Text="All Profit/Loss" AutoPostBack="True" OnCheckedChanged="txtSearch_OnTextChanged" />
                        </div>

                        <div class="form-group stock-search-field float-left">
                            <asp:TextBox ID="txtSearchDateFrom" runat="server" CssClass="form-control datepickerCSS" AutoPostBack="True" OnTextChanged="txtSearch_OnTextChanged"></asp:TextBox>
                        </div>
                        <div class="form-group stock-search-field float-left">
                            <asp:TextBox ID="txtSearchDateTo" runat="server" CssClass="form-control datepickerCSS" AutoPostBack="True" OnTextChanged="txtSearch_OnTextChanged"></asp:TextBox>
                        </div>
                    </div>
                </div>

            </div>

            <div class="col-md-12 scroll">

                <asp:GridView ID="grdProfitLoss"
                    CssClass="mGrid gBox"
                    ShowFooter="true" AllowPaging="True" PageSize="10" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="Id" 
                    EmptyDataText="There are no data records to display." ViewStateMode="Enabled" 
                    OnRowDataBound="grdProfitLoss_OnRowDataBound"
                    RowStyle-Wrap="true" AllowSorting="True">
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1  %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="cashType" HeaderText="cashType" SortExpression="cashType"></asp:BoundField>
                        <asp:BoundField DataField="descr" HeaderText="descr" SortExpression="descr" Visible="False"></asp:BoundField>
                        <asp:TemplateField HeaderText="Dated" SortExpression="entryDate">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("entryDate","{0:dd-MMM-yyyy}") %>' ID="Label1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cashin" SortExpression="cashIn">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("cashIn") %>' ID="lblCashin"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label runat="server" ID="lblFooterCashin"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cashout" SortExpression="cashOut">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("cashOut") %>' ID="lblCashout"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label runat="server" ID="lblFooterCashout"></asp:Label>
                            </FooterTemplate>

                        </asp:TemplateField>

                        <asp:BoundField DataField="cashInHand" HeaderText="cashInHand" SortExpression="cashInHand" Visible="False"></asp:BoundField>

                        <asp:BoundField DataField="billNo" HeaderText="BillNo" SortExpression="billNo" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="mainDescr" HeaderText="mainDescr" SortExpression="mainDescr" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="roleID" HeaderText="roleID" SortExpression="roleID" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="branchId" HeaderText="branchId" SortExpression="branchId" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="groupId" HeaderText="groupId" SortExpression="groupId" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" Visible="false"></asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
            </div>

        </div>
    </div>
</asp:Content>
