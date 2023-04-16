<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="StockOpt.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.StockOpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">

    <% if (Session["pageName"].ToString() == "unit")
       { %>
        Unit Stock 
    <% }
       else
       { %>
        Bulk Stock
    <% } %>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    
    <div class="stockOpt-header">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="section">
                    <h2 class="sectionBreadcrumb">
                        <asp:Label ID="lblStockTitle" runat="server" Text="Purchase"></asp:Label>
                    </h2>
                    <asp:Label runat="server" ID="lblTest" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    
    <div>
        
    </div>

</asp:Content>
