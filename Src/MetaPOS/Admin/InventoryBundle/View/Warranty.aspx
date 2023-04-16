<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Warranty.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.Warranty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Warranty Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Warranty.css" rel="stylesheet" type="text/css" />
    <link href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/Warranty-responsive.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 marginTop-10">
            <div class="section">
                <h2 class="sectionBreadcrumb"><%=Resources.Language.Ttitle_warranty_page %></h2>
                <asp:Label ID="lblTest" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="gridHeader">
                <div class="col-md-4 col-sm-4 col-xs-6 gridTitle text-left">
                    <label><%=Resources.Language.Lbl_warranty_warranty_history %></label>
                </div>
                <div class="col-md-8 col-sm-8 col-xs-6 gridTitle text-right form-inline">
                    
                    <div class="form-group float-left">
                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" placeholder="<%$Resources:Language, Lbl_warranty_search %>" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="form-group float-left margin-left-7">
                        <asp:LinkButton runat="server" ID="btnPrintWarranty" OnClick="btnPrintWarranty_OnClick"><i class="fa fa-2x fa-print"></i></asp:LinkButton>
                    </div>
                    
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="scroll">
                <asp:GridView ID="grdWarranty" runat="server" AutoGenerateColumns="False" 
                    CssClass="mGrid gBox scrollBar"
                    OnRowDataBound="grdWarranty_RowDataBound" PageSize="10"
                    EmptyDataText="There are no data records to display." 
                    OnSelectedIndexChanged="grdWarranty_SelectedIndexChanged"
                    AllowPaging="true" OnPageIndexChanging="grdWarranty_OnPageIndexChanging">
                    <RowStyle Wrap="true" />
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_sl %>">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_invoice %>" SortExpression="billNo">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("billNo") %>' ID="TextBox2"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("billNo") %>' ID="lblInvoice"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product ID" SortExpression="prodID" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("prodID") %>' ID="prodID"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("prodID") %>' ID="prodID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_product %>" SortExpression="prodName">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("prodName") %>' ID="prodName"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("prodName") %>' ID="prodName"></asp:Label>
                            </ItemTemplate>

                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cus ID" SortExpression="cusID" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("cusID") %>' ID="cusID"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("cusID") %>' ID="lblCusID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_name %>" SortExpression="name">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("name") %>' ID="name"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("name") %>' ID="name"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="phone" HeaderText="<%$Resources:Language, Lbl_warranty_phone %>" SortExpression="phone"></asp:BoundField>
                        
                         
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_supplier %>" SortExpression="supCompany">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("supCompany") %>' ID="supCompany"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("supCompany") %>' ID="supCompany"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_IMEI %>" SortExpression="imei">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Bind("imei") %>' ID="imei"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("imei") %>' ID="imei"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="imei" HeaderText="<%$Resources:Language, Lbl_warranty_IMEI %>" SortExpression="imei" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="PurchaseDate" HeaderText="<%$Resources:Language, Lbl_warranty_purchese_date %>" SortExpression="PurchaseDate" DataFormatString="{0:dd-MMM-yyyy}" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="warranty" HeaderText="<%$Resources:Language, Lbl_warranty_warranty %>" SortExpression="warranty"></asp:BoundField>                        
                        <asp:BoundField DataField="SaleDate" HeaderText="<%$Resources:Language, Lbl_warranty_sale_date %>" SortExpression="SaleDate" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_expiry_date %>">
                            <ItemTemplate>
                                <asp:Label runat="server" Text="" ID="lblExpiredDate"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_status %>">
                            <ItemTemplate>
                                <asp:Label runat="server" Text="" ID="lblStatus"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="supID" SortExpression="supID" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("supID") %>' ID="supID"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_warranty_go %>" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDetails" runat="server" CssClass="btn btn-design glyIconPosition" CausesValidation="False" Text="<span class='glyphicon glyphicon-shopping-cart'></span>" CommandName="Select" data-toggle="tooltip" data-placement="top" title="Go Invoice" data-trigger="hover"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Wrap="false" />
                    <RowStyle Wrap="false" />
                    <PagerStyle CssClass="pgr" />
                </asp:GridView>

            </div>
        </div>
    </div>
    
    <script>
        activeModule = "inventory";
    </script>
    

</asp:Content>