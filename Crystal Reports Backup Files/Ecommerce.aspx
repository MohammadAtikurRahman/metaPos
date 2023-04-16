<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Ecommerce.aspx.cs" Inherits="MetaPOS.Admin.ShopBundle.View.Ecommerce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Ecommerce Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Offer.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).on("keyup paste", function() {

            if ($(document.activeElement).attr("id") == "<% = txtSearchNameCode.ClientID %>") {

                $("[id$=txtSearchNameCode]").autocomplete({
                    source: function(request, response) {
                        $.ajax({
                            url: '<%= ResolveUrl("~/Admin/Stock.aspx/GetProducts") %>',
                            data: "{ 'prefix': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function(data) {
                                response($.map(data.d, function(item) {
                                    return {
                                        label: item.split(',')[0] + ' (' + item.split(',')[1] + ')',
                                        val: item.split(',')[1]
                                    };
                                }));
                            },
                            error: function(response) {
                                alert(response.responseText);
                            },
                            failure: function(response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function(e, i) {
                        $("[id$=hfProductDetails]").val(i.item.val);
                    },
                    minLength: 1
                });
            }
        });

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">Ecommerce</h2>
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 form-horizontal">
            <div class="stockfieldHeight singleStockField section-new">
                <h2 class="sectionHeading">Add Ecommerce Product</h2>
                <asp:Panel ID="pnlSet" runat="server" DefaultButton="btnSet">
                    <div class="form-group">
                        <asp:Panel ID="pnlScan" runat="server" DefaultButton="record.css?v=0.001">
                            <div class="col-sm-12">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSearchNameCode" CssClass="form-control" runat="server" aria-describedby="record.css?v=0.001" placeholder="Search/Scan Product by Name/Code"></asp:TextBox>
                                    <asp:HiddenField ID="hfProductDetails" runat="server" />
                                    <span class="input-group-btn">
                                        <asp:ImageButton ID="record.css?v=0.001" runat="server" CssClass="btn btn-default inp-group" OnClick="record.css?v=0.001_Click" ImageUrl="~/Img/loading.png" />
                                    </span>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-xs-6 section-new-left">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Product Name"></asp:Label>
                            <asp:TextBox runat="server" ID="txtProductName" CssClass="form-control" Enabled="false" BackColor="#F0F3F4" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label6" runat="server" Text="Product Title"></asp:Label>
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label7" runat="server" Text="Features"></asp:Label>
                            <asp:TextBox runat="server" ID="txtFeatures" Rows="3" CssClass="form-control" TextMode="MultiLine" />
                        </div>
                    </div>
                    <div class="col-xs-6 section-new-right">
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Product Code"></asp:Label>
                            <asp:TextBox runat="server" ID="txtProductCode" CssClass="form-control" Enabled="false" BackColor="#F0F3F4" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Set Image"></asp:Label>
                            <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
                            <asp:Label runat="server" ID="lblFileName" Text="" CssClass="disNone"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label8" runat="server" Text="Descriptions"></asp:Label>
                            <asp:TextBox runat="server" ID="txtDescription" Rows="3" CssClass="form-control" TextMode="MultiLine" />
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">

                            <asp:CheckBox ID="chkFeatured" CssClass="checkbox-inline" runat="server" Text=" Featured Product" />

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSet" CssClass="btn btn-info btn-sm CRBtnDesign btnResize e67e22 btnAddOpt" runat="server" Text="Set" OnClick="btnSet_Click" />
                                    <asp:Label runat="server" ID="lblTest" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSet" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:Button ID="btnReset" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnReset" runat="server" Text="Clear" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-xs-4 gridTitle text-left">
                    <label>Ecommerce Products</label>
                </div>
                <div class="col-xs-8 gridTitle text-right form-inline">
                    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btn">
                        <div class="form-group">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                            <asp:Button ID="btn" runat="server" CssClass="disNone" />
                        </div>
                        <div class="form-group">
                            <asp:LinkButton runat="server" ID="btnPrint" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="scroll">
                <asp:GridView ID="grdEcommerce" runat="server" CssClass="mGrid gBox" AutoGenerateColumns="False" DataKeyNames="Id" OnRowDeleted="grdEcommerce_RowDeleted"
                              DataSourceID="dsEcommerce" EmptyDataText="No data records found." AllowPaging="true" EnableViewState="false"
                              PageSize="10">
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="prodName" HeaderText="Product Name" SortExpression="prodName" ItemStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="prodCode" HeaderText="Code" SortExpression="prodCode" ItemStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="prodTitle" HeaderText="Title" SortExpression="prodTitle" ItemStyle-Width="25%"></asp:BoundField>
                        <asp:BoundField DataField="sPrice" HeaderText="Price" SortExpression="sPrice" ItemStyle-Width="20%"></asp:BoundField>
                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnGrdDelete" CssClass="btn btn-design btnDeleteOpt" OnClientClick=" return confirm('Are you sure you want to delete selected record ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                    <AlternatingRowStyle CssClass="alt" />
                </asp:GridView>
                <asp:SqlDataSource runat="server" ID="dsEcommerce"
                                   ConnectionString='<%$ ConnectionStrings:dbPOS %>'
                                   DeleteCommand="DELETE FROM Ecommerce WHERE Id = @Id"
                                   SelectCommand="SELECT * FROM Ecommerce as ecom LEFT JOIN StockInfo as si 
                                    ON ecom.prodCode = si.prodCode"
                                   CancelSelectOnNullParameter="false">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                    </DeleteParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
    
    <script>
        activeModule = "website";
    </script>

</asp:Content>