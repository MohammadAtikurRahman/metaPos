<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Token.aspx.cs" Inherits="MetaPOS.Admin.SaleBundle.View.Token" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Token Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../Css/Token.css" type="text/css" />
    <link href="<%= ResolveUrl("~/Admin/SaleBundle/Content/Token-responsive.css?v=0.001") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div class="section">
                <h2 class="sectionBreadcrumb lang-token-token">Token</h2>
                <asp:Label runat="server" ID="lblTest"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-xs-3 gridTitle text-left">
                    <label class="lang-token-token-report">Token Report</label>
                </div>
                <div class="col-xs-9 gridTitle text-right form-inline">
                    <asp:Panel runat="server" ID="PNL" DefaultButton="">
                        <div class="form-group float-left">
                            <asp:TextBox runat="server" ID="txtSearchDateFrom" CssClass="form-control datepickerCSS" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <span class="lang-sales-to float-left">To</span>
                        <div class="form-group float-left">
                            <asp:TextBox runat="server" ID="txtSearchDateTo" CssClass="form-control datepickerCSS" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="form-group float-left margin-left-5">
                            <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control " placeholder="Search..." AutoPostBack="true"></asp:TextBox>
                        </div>
                        <%--<div class="btn-group" role="group">
                            <asp:LinkButton runat="server" ID="btnPrint" CssClass="print"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                        </div>--%>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:GridView ID="grdTokenInfo" OnRowDataBound="grdTokenInfo_RowDataBound" OnRowCommand="grdTokenInfo_RowCommand"
                          OnRowDeleted="grdTokenInfo_RowDeleted" OnRowDeleting="grdTokenInfo_RowDeleting" CssClass="mGrid gBox"
                          ShowFooter="true" AllowPaging="True" runat="server" AutoGenerateColumns="False"  OnPageIndexChanging="grdTokenInfo_OnPageIndexChanging"
                          EmptyDataText="There are no data records to display." ViewStateMode="Enabled" RowStyle-Wrap="true">
                <Columns>
                    <asp:TemplateField HeaderText="SL" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="billNo" HeaderText="Invoice No" SortExpression="billNo" ItemStyle-Width="20%"></asp:BoundField>
                    <asp:BoundField DataField="cusID" HeaderText="Customer ID" SortExpression="cusID" ItemStyle-Width="20%"></asp:BoundField>
                    <asp:TemplateField HeaderText="Token" SortExpression="token">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("token") %>' ID="Label2"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalMessage" runat="server" Text="Total:"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Disc Amount" SortExpression="discAmt">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("discAmt") %>' ID="Label1"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date" SortExpression="entryDate" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
        </div>
    </div>
    
    <script>
        activeModule = "sale";
    </script>

</asp:Content>