<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="MetaPOS.Admin.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Payment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SubscriptionBundle/Content/payment.css?v=0.002") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div>

        <div class="payment">
            <div class="row">

                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div id="divHeaderPanel">
                        <label class="title">Payment</label>
                    </div>
                </div>

                <div class="col-md-12 ">
                    <div class="ReturnfieldHeight2 section section-payment">
                        <div class="">
                            <div class="gridHeader">
                                <div class="col-xs-4 gridTitle text-left">
                                    <label>Payment list</label>
                                </div>
                                <div class="col-xs-8 gridTitle text-right form-inline">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlPaymentStatus" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="txtSearch_OnTextChanged">
                                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                                            <asp:ListItem Value="0">Pending</asp:ListItem>
                                            <asp:ListItem Value="1">Accepted</asp:ListItem>
                                            <asp:ListItem Value="2">Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" CssClass="form-control" placeholder="Search..." OnTextChanged="txtSearch_OnTextChanged"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="payment-list">
                            <asp:GridView ID="grdPayment" CssClass="mGrid gBox mGrid-payment " runat="server"
                                AutoGenerateColumns="False" DataSourceID="dsPayment"
                                OnRowDataBound="grdPayment_OnRowDataBound"
                                OnRowCommand="grdPayment_OnRowCommand" DataKeyNames="Id">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id" InsertVisible="False" SortExpression="Id" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("Id") %>' ID="lblId"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="roleId" SortExpression="roleId" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("roleId") %>' ID="lblBranchId"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Branch" SortExpression="branchName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("branchName") %>' ID="lblBranchName"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Trans ID" SortExpression="invoiceNo">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("invoiceNo") %>' ID="lblTransectionId"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="details" HeaderText="Details" SortExpression="details"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Fee" SortExpression="fee" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("fee") %>' ID="lblFee"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="amount" SortExpression="amount">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("amount") %>' ID="lblAmount"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" Visible="False"></asp:BoundField>

                                    <asp:BoundField DataField="active" HeaderText="active" SortExpression="active" Visible="False"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Date" SortExpression="createDate">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Bind("createDate","{0:dd-MMM-yyyy}") %>' ID="Label1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="updateDate" HeaderText="updateDate" SortExpression="updateDate" Visible="False"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <ul style="list-style: none">
                                                <li>
                                                    <asp:LinkButton ID="btnGrdView" runat="server" CssClass="btn btn-design text-align-left btn-grd-submit" CausesValidation="False" CommandName="payment" Text="<span class='glyphicon glyphicon-adjust'></span> Check" data-toggle="tooltip" data-placement="top" title="View " data-trigger="hover" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"></asp:LinkButton></li>
                                            </ul>
                                            <asp:Label runat="server" Text='<%# Bind("status") %>' ID="lblStatus" Visible="False"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource runat="server" ID="dsPayment" ConnectionString='<%$ ConnectionStrings:dbPOS %>' SelectCommand=""></asp:SqlDataSource>
                        </div>
                    </div>
                </div>




                <!-- Payment -->
                <div id="ConfirmPayment" class="modal fade" role="dialog">

                    <div class="modal-dialog ">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Confirm Payment</h4>
                                <asp:Label ID="lblPaymentId" runat="server" Text="Label" Visible="false"></asp:Label>
                            </div>
                            <div class="modal-body">
                                <div class="container-fluid">

                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConfirmPayment" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div id="div1" runat="server"></div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-6">Service Name:</label>
                                            <div class="col-md-6">
                                                <asp:Label ID="Label1" runat="server" Text="Monthly Fee"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-6">Branch Name:</label>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblBranchId" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:Label ID="lblBranchName" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-6">Transection ID:</label>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblTransectionID" runat="server" Text="AGADAS"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-6">Payment</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <label class="col-md-6">Status</label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1">Accepted</asp:ListItem>
                                                    <asp:ListItem Value="2">Rejected</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Button ID="btnConfirmPayment" runat="server" Text="Confirm " CssClass="btn btn-primary transfer pull-right" OnClick="btnConfirmPayment_OnClick" />
                                </div>
                            </div>
                        </div>

                    </div>


                </div>

            </div>
        </div>

    </div>


    <script src="<%= ResolveUrl("~/Admin/SubscriptionBundle/Script/Payment.js?v0.002") %>"></script>

    <script>
        activeModule = "settings";

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            var head = document.getElementsByTagName("head")[0];
            var script = document.createElement('script');
            script.src = '/Admin/SubscriptionBundle/Script/Payment.js';
            script.type = 'text/javascript';
            head.appendChild(script);
        });
    </script>

</asp:Content>
