<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="MetaPOS.Admin.SaleBundle.View.Quotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Quotation - metaPOS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SaleBundle/Content/Quotation-responsive.css?v=0.000") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 marginTop-10">
            <div class="section">
                <h2 class="sectionBreadcrumb lang-sales-quotation">Quotation</h2>

                <asp:Label ID="lblTest1" runat="server"></asp:Label>
                <asp:Label ID="lblTest" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-md-12 gridTitle text-left">
                    <label class="lang-sales-quotation-list">Quotation List</label>
                </div>
            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:GridView ID="gvQuotation"
                          runat="server"
                          AutoGenerateColumns="False"
                          DataKeyNames="Id"
                          DataSourceID="dsQuotation"
                          CssClass="mGrid gBox scrollBar"
                          EnableViewState="false"
                          EmptyDataText="There are no data records to display."
                          OnRowDataBound="gvQuotation_RowDataBound"
                          AllowPaging="True"
                          ViewStateMode="Enabled"
                          RowStyle-Wrap="false"
                          OnRowCommand="gvQuotation_OnRowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="SL">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="False"></asp:BoundField>
                    <asp:TemplateField HeaderText="Order Id" SortExpression="orderId">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("orderId") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("orderId") %>' ID="lblOrderID"></asp:Label>
                        </ItemTemplate>

                        <ControlStyle Width="10%"></ControlStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Id" SortExpression="cusID" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("cusID") %>' ID="TextBox2"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("cusID") %>' ID="lblCusId"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="prodId" HeaderText="Product Id" SortExpression="prodId" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="prodName" HeaderText="Product" SortExpression="prodName"></asp:BoundField>
                    <asp:TemplateField HeaderText="Sku" SortExpression="prodSku">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("prodSku") %>' ID="lblSKU"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("prodSku") %>' ID="lblSKU"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="prodQty" HeaderText="Qty" SortExpression="prodQty"></asp:BoundField>
                    <asp:BoundField DataField="name" HeaderText="Customer" SortExpression="name"></asp:BoundField>
                    <asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone"></asp:BoundField>
                    <asp:BoundField DataField="mailinfo" HeaderText="Email" SortExpression="mailinfo"></asp:BoundField>
                    <asp:TemplateField HeaderText="Date" SortExpression="entryDate">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("entryDate", "{0:dd-MMM-yyyy}") %>' ID="Label1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" Visible="false"></asp:BoundField>

                    <asp:TemplateField HeaderText="Branch">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlBrachToQuotation" CssClass="form-control" OnDataBinding="ddlBrachToQuotation_DataBinding"></asp:DropDownList>
                            <%--<asp:SqlDataSource runat="server" ID="dsQuotationBranch" ConnectionString='<%$ ConnectionStrings:dbPOS %>' SelectCommand="SELECT [roleID], [title] FROM [RoleInfo] WHERE active='1'">
                                
                            </asp:SqlDataSource>--%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnConfirm" runat="server" CssClass="btn btn-design glyIconPosition btnAddOpt" CausesValidation="False" Text='<i class="fa fa-check-circle" aria-hidden="true"></i>' CommandName="confirm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cancel">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-design glyIconPosition btnDeleteOpt" CausesValidation="False" Text='<i class="fa fa-times" aria-hidden="true"></i>' CommandName="cancel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
            <asp:SqlDataSource runat="server" ID="dsQuotation"
                               ConnectionString='<%$ ConnectionStrings:dbPOS %>'
                               DeleteCommand="DELETE FROM [QuotationInfo] WHERE [Id] = @Id"
                               SelectCommand="SELECT DISTINCT quot.orderId,quot.Id,quot.orderId,quot.cusId,quot.prodId,quot.prodsku,quot.prodQty,stock.prodName,stock.sku,stock.Qty,cus.name,cus.phone,cus.mailInfo,quot.entryDate,quot.status FROM QuotationInfo AS quot LEFT JOIN CustomerInfo AS cus ON quot.orderId = cus.orderId LEFT JOIN  StockInfo as stock ON quot.prodSku = stock.sku WHERE status='0' AND quot.orderId != 0  ORDER BY quot.Id, quot.orderId DESC">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                </DeleteParameters>
            </asp:SqlDataSource>
        </div>
    </div>

    <script>
        activeModule = "sale";
    </script>


</asp:Content>