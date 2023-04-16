<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Servicing.aspx.cs" Inherits="MetaPOS.Admin.SaleBundle.View.Servicing" EnableEventValidation="false" %>

<%@ Import Namespace="System.Activities.Statements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Servicing Point
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Css/select2.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Css/servicing.css?v=0.001") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SaleBundle/Content/Servicing-responsive.css?v=0.000") %>" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">


        <div id="divHeaderPanel" class="margin-top-10">
            <label class="title"><%=Resources.Language.Title_servicing %></label>

        </div>

    

    <div>
        <div class="section add-service">
            <div class="row">
                <div class="col-md-6 col-sm-12 col-xs-12">

                    <div class="form-group padding-top-thirty">
                        <label class="col-md-4"><%=Resources.Language.Lbl_servicing_customer %></label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group padding-top-thirty disNone">
                        <label class="col-md-4"><%=Resources.Language.Lbl_servicing_servicingID %></label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtServiceId" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group padding-top-thirty">
                        <label class="col-md-4 lang-servicing-product-name"><%=Resources.Language.Lbl_servicing_product_name %></label>
                        <div class="col-md-8">
                            <asp:Label runat="server" ID="lblProdId" CssClass="disNone"></asp:Label>
                            <asp:TextBox runat="server" ID="txtProdName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group padding-top-thirty">
                        <label class="col-md-4"><%=Resources.Language.Lbl_servicing_IMEI %></label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtImei" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group padding-top-thirty">
                        <label class="col-md-4"><%=Resources.Language.Lbl_servicing_description %></label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="col-md-6 col-sm-12 col-xs-12">

                    <div class="form-group padding-top-thirty">
                        <label class="col-md-4"><%=Resources.Language.Lbl_servicing_supplier %></label>
                        <div class="col-md-8">
                            <asp:Label runat="server" ID="lblSupplier" CssClass="disNone"></asp:Label>
                            <asp:TextBox runat="server" ID="txtSupplier" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group padding-top-thirty">
                        <label class="col-md-4"><%=Resources.Language.Lbl_servicing_delivery_date %></label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtDeliveryDate" CssClass="form-control datepickerCSS"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group padding-top-thirty">
                        <label class="col-md-4"><%=Resources.Language.Lbl_servicing_total_amount %></label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtTotalAmt" CssClass="form-control float-number-validate"></asp:TextBox>
                        </div>
                    </div>


                    <div class="form-group padding-top-thirty">
                        <label class="col-md-4"><%=Resources.Language.Lbl_servicing_pay_cost %></label>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtPayCost" CssClass="form-control float-number-validate"></asp:TextBox>
                        </div>
                    </div>

                    <div class="padding-top-thirty">
                        <div class="pull-right padding-right-15">
                            <input id="btnResetService" type="button" class="btn btn-default" value="<%=Resources.Language.Btn_servicing_reset %>" />
                            <input id="btnSaveService" type="button" class="btn btn-primary" value="<%=Resources.Language.Btn_servicing_save %>" />
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>


    <div class="section">
        <div class="row">

            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="gridHeader">
                    <div class="col-md-6 col-sm-4 col-xs-4 gridTitle two-line-grid-title text-left">
                        <label><%=Resources.Language.Lbl_servicing_servicing_report %></label>
                    </div>
                    <div class="col-md-6 col-sm-8 col-xs-8 gridTitle form-inline">
                        <asp:Panel ID="PanelSearchStock" runat="server">
                            <div class="form-group stock-search-field float-left">
                                <asp:TextBox ID="txtFrom" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="txtSearch_OnTextChanged" placeholder="<%$Resources:Language, Lbl_servicing_date_form %>"></asp:TextBox>
                            </div>
                            <div class="form-group stock-search-field float-left margin-left-7">
                                <asp:TextBox ID="txtTo" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="txtSearch_OnTextChanged" placeholder="<%$Resources:Language, Lbl_servicing_date_to %>"></asp:TextBox>
                            </div>
                            <div class="form-group stock-search-field float-left">
                                <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtSearch_OnTextChanged" placeholder="<%$Resources:Language, Lbl_servicing_search %>"></asp:TextBox>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="scroll">
                <div class="col-md-12">
                    <asp:GridView ID="grdServiceing" runat="server" AutoGenerateColumns="false" DataKeyNames="serviceId"
                        PageSize="10" OnPageIndexChanging="grdServiceing_OnPageIndexChanging" AllowPaging="True"
                        CssClass="mGrid gBox" OnRowCommand="grdService_OnRowCommand" ViewStateMode="Enabled"  RowStyle-Wrap="false"
                         EmptyDataText="No Record found" ShowFooter="true">

                        <Columns>
                            <asp:TemplateField HeaderText="SL" Visible="false">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_servicing_servicingID %>" SortExpression="serviceId">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("serviceId") %>' ID="serviceId"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("serviceId") %>' ID="serviceId"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CustomerID" SortExpression="customerId" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("customerId") %>' ID="TextBox1"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("customerId") %>' ID="customerId"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="cusName" HeaderText="<%$Resources:Language, Lbl_servicing_cus_name %>" SortExpression="cusName"></asp:BoundField>
                            <asp:BoundField DataField="prodId" HeaderText="prodId" SortExpression="prodId" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="prodName" HeaderText="<%$Resources:Language, Lbl_servicing_product %>" SortExpression="prodName"></asp:BoundField>
                            <asp:BoundField DataField="imei" HeaderText="<%$Resources:Language, Lbl_servicing_IMEI %>" SortExpression="imei"></asp:BoundField>
                            <asp:BoundField DataField="supplierId" HeaderText="SupplierId" SortExpression="supplierId" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="supplier" HeaderText="<%$Resources:Language, Lbl_servicing_supplier %>" SortExpression="supplier" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="entryDate" HeaderText="<%$Resources:Language, Lbl_servicing_received %>" SortExpression="entryDate" DataFormatString="{0:d-MMM-yyyy}" ItemStyle-Width="15%"></asp:BoundField>
                            <asp:BoundField DataField="deliveryDate" HeaderText="<%$Resources:Language, Lbl_servicing_delivery %>" SortExpression="deliveryDate" DataFormatString="{0:d-MMM-yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="description" HeaderText="<%$Resources:Language, Lbl_servicing_description %>" SortExpression="description" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_servicing_total %>" SortExpression="total">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("totalAmt") %>' ID="TextBox2"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("totalAmt") %>' ID="lblTotalAmt"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_servicing_paid %>" SortExpression="paidAmt">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# Bind("paidAmt") %>' ID="TextBox3"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("paidAmt") %>' ID="lblPaidAmt"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="updateDate" HeaderText="updateDate" SortExpression="updateDate" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="roleID" HeaderText="roleID" SortExpression="roleID" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_servicing_status %>" SortExpression="active">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="service-status" ID="lblStatus" Text='<%# (Eval("active").ToString() == "0")? "Processing" : String.Format("<span class=color-green>Delivered</span>") %>' CausesValidation="false" CommandName="StatusChange" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField ItemStyle-CssClass="action-column text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnServicePay" runat="server" CssClass="btn btn-design" CausesValidation="False" CommandName="ServicingPay" Text="<span class='glyphicon glyphicon-usd'></span>" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" data-toggle="tooltip" data-placement="top" title="Pay " data-trigger="hover"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="action-column text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnGrdView" runat="server" CssClass="btn btn-design" CausesValidation="False" CommandName="View" Text="<span class='glyphicon glyphicon-adjust'></span>" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" data-toggle="tooltip" data-placement="top" title="View " data-trigger="hover"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="action-column text-center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnGrdPrint" runat="server" CssClass="btn btn-design" CausesValidation="False" CommandName="ReceiptPrint" Text="<span class='fa fa-print'></span>" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" data-toggle="tooltip" data-placement="top" title="Print " data-trigger="hover"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:GridView>
                </div>
            </div>



            <!-- Detailview -->
            <div class="modal fade" id="serviceDetailView" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h3 class="modal-title" id="myModalLabel">
                                <b><%=Resources.Language.Lbl_servicing_Servicing_id %></b>
                                <asp:Label ID="lblServceId" runat="server"></asp:Label></h3>
                        </div>
                        <div class="modal-body">
                            <asp:DetailsView ID="dltServiceView" runat="server" CssClass="mGrid gBox gCus mDtl dtlSize" AutoGenerateRows="False" DataKeyNames="Id">
                                <Fields>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="serviceId" HeaderText="<%$Resources:Language, Lbl_servicing_servicingID %>" SortExpression="serviceId"></asp:BoundField>
                                    <asp:BoundField DataField="customerId" HeaderText="<%$Resources:Language, Lbl_servicing_customerID %>" SortExpression="customerId"></asp:BoundField>
                                    <asp:BoundField DataField="prodId" HeaderText="prodId" SortExpression="prodId" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="prodName" HeaderText="<%$Resources:Language, Lbl_servicing_product_name %>" SortExpression="prodName"></asp:BoundField>
                                    <asp:BoundField DataField="imei" HeaderText="<%$Resources:Language, Lbl_servicing_IMEI %>" SortExpression="imei"></asp:BoundField>
                                    <asp:BoundField DataField="supplierId" HeaderText="supplierId" SortExpression="supplierId" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="supplier" HeaderText="<%$Resources:Language, Lbl_servicing_supplier %>" SortExpression="supplier"></asp:BoundField>
                                    <asp:BoundField DataField="description" HeaderText="<%$Resources:Language, Lbl_servicing_description %>" SortExpression="description"></asp:BoundField>
                                    <asp:BoundField DataField="paidAmt" HeaderText="<%$Resources:Language, Lbl_servicing_paid%>" SortExpression="paidAmt"></asp:BoundField>
                                    <asp:BoundField DataField="totalAmt" HeaderText="<%$Resources:Language, Lbl_servicing_total_amount %>" SortExpression="totalAmt"></asp:BoundField>
                                    <asp:BoundField DataField="entryDate" HeaderText="<%$Resources:Language, Lbl_servicing_received %>" SortExpression="entryDate" DataFormatString="{0:d-MMM-yyyy}"></asp:BoundField>
                                    <asp:BoundField DataField="deliveryDate" HeaderText="<%$Resources:Language, Lbl_servicing_delivery %>" SortExpression="deliveryDate" DataFormatString="{0:d-MMM-yyyy}"></asp:BoundField>
                                    <asp:BoundField DataField="updateDate" HeaderText="updateDate" SortExpression="updateDate" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="roleID" HeaderText="roleID" SortExpression="roleID" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="active" HeaderText="active" SortExpression="active" Visible="false"></asp:BoundField>
                                </Fields>
                            </asp:DetailsView>
                          <%--  <asp:SqlDataSource runat="server" ID="dsServiceDetailsView" ConnectionString='<%$ ConnectionStrings:localhost %>' SelectCommand="SELECT * FROM [ServicingInfo] WHERE ([serviceId] = @serviceId)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblServceId" PropertyName="Text" Name="serviceId" Type="String"></asp:ControlParameter>
                                </SelectParameters>
                            </asp:SqlDataSource> --%>

                        </div>
                        <div class="modal-footer" style="border: none">
                            <button type="button" class="btn btn-default btn-close" data-dismiss="modal"><%=Resources.Language.Btn_servicing_close %></button>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Gridview -->
            <div class="modal fade" id="serviceStatusChage" tabindex="-1" role="dialog" aria-labelledby="ServiceStatus">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h3 class="modal-title" id="ServiceStatus">
                                <b><%=Resources.Language.Lbl_servicing_Servicing_id %></b>
                                <asp:Label ID="lblServiceIdStatus" runat="server"></asp:Label></h3>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlServiceStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Processing</asp:ListItem>
                                            <asp:ListItem Value="1">Deliveried</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" Text="<%$Resources:Language, Btn_servicing_change_status %>" ID="btnStatusChage" OnClick="btnStatusChage_OnClick" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="border: none">
                            <button type="button" class="btn btn-default" data-dismiss="modal"><%=Resources.Language.Btn_servicing_close %></button>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Servic Pay  -->
            <div class="modal fade" id="servicePayModal" tabindex="-1" role="dialog" aria-labelledby="ServicePay">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h3 class="modal-title" id="ServicePay">
                                <b><%=Resources.Language.Lbl_servicing_customer_id %></b>
                                <asp:Label ID="lblCusServicePay" runat="server"></asp:Label></h3>
                            <asp:Label runat="server" ID="lblDueAmt" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblCusServiceId" Visible="false"></asp:Label>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-6">
                                    <b><%=Resources.Language.Lbl_servicing_customer_payment %></b>
                                    <br />
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtServicePayAmt" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-primary" Text="<%$Resources:Language, Btn_servicing_pay_now %>" ID="btnServicePay" OnClick="btnServicePay_OnClick" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer" style="border: none">
                            <button type="button" class="btn btn-default" data-dismiss="modal"><%=Resources.Language.Btn_servicing_close %></button>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>



    <script src='<%= ResolveUrl("~/Admin/SaleBundle/Script/Servicing.js?v=0.003") %>'></script>
    <script src='<%= ResolveUrl("~/Admin/SaleBundle/Script/sale-customer-upsert.js?v=0.003") %>'></script>
    <script src="<%= ResolveUrl("~/Js/select2.min.js?v=0.033") %>"></script>


    <script>
        $(function () {
            $("#" + "<%= ddlCustomer.ClientID %>").select2({
                placeholder: "<% =Resources.Language.Lbl_servicing_select_a_customer %>",
                allowClear: true
            });
        });
    </script>
    <script>
        activeModule = "sale";
    </script>
</asp:Content>
