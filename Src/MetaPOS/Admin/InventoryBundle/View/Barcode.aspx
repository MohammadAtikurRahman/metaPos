<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Barcode.aspx.cs" Inherits="MetaPOS.Admin.InventoryBundle.View.Barcode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Barcode
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <a href="<%= ResolveUrl("~/Admin/InventoryBundle/Content/barcode.css") %>"></a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    
     <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section stock-back-section">
                <h2 class="sectionBreadcrumb sectionBreadcrumb-stock" id="lblHeaderTitle" runat="server"><%=Resources.Language.Title_barcode_barcode_print %></h2>


            </div>
        </div>
    </div>

    <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>

    <div class="barcode">
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <div class="section" style="padding: 40px 30px">
                    <div class="form-group form-inline">
                        <asp:TextBox ID="txtBarcode" runat="server" CssClass="form-control" placeholder="<%$Resources:Language, Lbl_barcode_enter_your_product_code %>"></asp:TextBox>
                        <asp:Button ID="btnBarcodePrint" runat="server" CssClass="btn btn-info" Text="<%$Resources:Language, Lbl_barcode_print %>" OnClick="btnBarcodePrint_OnClick" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script>
        activeModule = "inventory";
    </script>
    
    <script src="<%= ResolveUrl("~/Admin/InventoryBundle/Script/barcode.js") %>"></script>

</asp:Content>
