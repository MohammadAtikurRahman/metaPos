<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Support.aspx.cs" Inherits="MetaPOS.Admin.SettingBundle.View.Support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Technical Support
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SettingBundle/Content/support.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <script type="text/javascript">
        function updateProgress(percentage, maxNumber) {
            //document.getElementById('processBar').style.width = percentage + "%";
            $('#processBar').attr('aria-valuenow', maxNumber).css('width', percentage).attr('valuemax', maxNumber);
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">Technical Support</h2>
                <hr />
                <asp:Label ID="lblTest" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <!--- Clear Data -->
    <div class="select-to-clear mGridTitle">
        <h4 class="">Check to clear data</h4>
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <asp:CheckBox ID="chkInventoryAll" runat="server" Text="" OnCheckedChanged="chkInventoryAll_CheckedChanged" AutoPostBack="true" /><b>Inventory</b><br />
                    <asp:CheckBox ID="chkStock" runat="server" Text="Stock" />
                    <br />
                    <asp:CheckBox ID="chkStockStatus" runat="server" Text="StockStatus" />
                    <br />
                    <asp:CheckBox ID="chkPackage" runat="server" Text="Package" CssClass="form-group" /><br />
                    <asp:CheckBox ID="chkReturn" runat="server" Text="Return" /><br />
                    <asp:CheckBox ID="chkDamage" runat="server" Text="Damage" /><br />
                </div>

                <div class="form-group">
                    <asp:CheckBox ID="chkSalesAll" runat="server" Text="" OnCheckedChanged="chkSalesAll_CheckedChanged" AutoPostBack="true" /><b>Sales</b><br />
                    <asp:CheckBox ID="chkInvoice" runat="server" Text="Invoice" /><br />
                    <asp:CheckBox ID="chkCustomer" runat="server" Text="Customer" /><br />
                    <asp:CheckBox ID="chkService" runat="server" Text="Service" /><br />
                    <asp:CheckBox ID="chkQuotation" runat="server" Text="Quotation" /><br />
                    <asp:CheckBox ID="chkServcing" runat="server" Text="Servicing" /><br />
                    <asp:CheckBox ID="chkInstallment" runat="server" Text="Installment" /><br />
                    <asp:CheckBox ID="chkToken" runat="server" Text="Token" /><br />
                </div>
            </div>

            <div class="col-md-2">
                <div class="form-group">
                    <asp:CheckBox ID="chkAccountingAll" runat="server" Text="" OnCheckedChanged="chkAccountingAll_CheckedChanged" AutoPostBack="true" /><b>Accounting</b><br />
                    <asp:CheckBox ID="chkSupply" runat="server" Text="Supply" /><br />
                    <asp:CheckBox ID="chkReceive" runat="server" Text="Receive" /><br />
                    <asp:CheckBox ID="chkExpense" runat="server" Text="Expense" /><br />
                    <asp:CheckBox ID="chkSalary" runat="server" Text="Salary" /><br />
                    <asp:CheckBox ID="chkBanking" runat="server" Text="Banking" /><br />
                </div>

                <div class="form-group">
                    <asp:CheckBox ID="chkReportAll" runat="server" Text="" OnCheckedChanged="chkReportAll_CheckedChanged" AutoPostBack="true" /><b>Report</b><br />
                    <asp:CheckBox ID="chkPurchaseReport" runat="server" Text="Purchase" /><br />
                    <%--<asp:CheckBox ID="chkInvoiceReport" runat="server" Text="Invoice" /><br />--%>
                    <asp:CheckBox ID="chkSlip" runat="server" Text="Invoice" /><br />
                    <asp:CheckBox ID="chkInventoryReport" runat="server" Text="Inventory" /><br />
                    <asp:CheckBox ID="chkTransactionReport" runat="server" Text="Transaction" /><br />
                </div>
            </div>

            <div class="col-md-2">
                <div class="form-group">
                    <asp:CheckBox ID="chkRecordAll" runat="server" Text="" OnCheckedChanged="chkRecordAll_CheckedChanged" AutoPostBack="true" /><b>Records</b><br />
                    <asp:CheckBox ID="chkStore" runat="server" Text="Store" /><br />
                    <asp:CheckBox ID="chkLocation" runat="server" Text="Location" /><br />
                    <asp:CheckBox ID="chkManufacturer" runat="server" Text="Manufacturer" /><br />
                    <asp:CheckBox ID="chkSupplier" runat="server" Text="Supplier" /><br />
                    <asp:CheckBox ID="chkCategory" runat="server" Text="Category" /><br />
                    <asp:CheckBox ID="chkMeasurement" runat="server" Text="Measurement" /><br />
                    <asp:CheckBox ID="chkField" runat="server" Text="Feild" /><br />
                    <asp:CheckBox ID="chkAttribute" runat="server" Text="Attribute" /><br />
                    <asp:CheckBox ID="chkBank" runat="server" Text="Bank" /><br />
                    <asp:CheckBox ID="chkCard" runat="server" Text="Card" /><br />
                    <asp:CheckBox ID="chkParticular" runat="server" Text="Particular" /><br />
                    <asp:CheckBox ID="chkStaff" runat="server" Text="Staff" /><br />
                    <asp:CheckBox ID="chkServiceType" runat="server" Text="Service Type" /><br />
                </div>
            </div>

            <div class="col-md-2">
                <div class="form-group">
                    <b>User</b><br />
                    <asp:CheckBox ID="chkBranch" runat="server" Text="Branch" /><br />
                    <asp:CheckBox ID="chkSubscription" runat="server" Text="Subscription" /><br />
                    <asp:CheckBox ID="chkSmsConfig" runat="server" Text="SMS Config" /><br />
                </div>
            </div>

            <div class="col-md-12">
                <asp:Button ID="btnDeleteData" runat="server" Text="Clear Data" CssClass="btn btn-primary" OnClick="btnDeleteData_Click" OnClientClick=" return confirm('Are you sure you want to delete those selected data?')" />
            </div>

        </div>

        <br />

        <div class="">
            <h3>Setting</h3>
            <div class="setting-section">
                <asp:Label ID="Label41" CssClass="lbl  control-label setting-section-title" runat="server" Text="Portwallet Payment Mode"></asp:Label>

                <div class="row">
                    <div class="col-sm-8">
                        <div class="setting-desc">
                            <p></p>
                        </div>
                        <div id="divPaymentMode" class="setting-action">
                            <asp:RadioButtonList ID="paymentMode" runat="server" CssClass="" OnSelectedIndexChanged="paymentMode_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="SandBox"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Payment"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <b />
        <div class="developer-section">
            <h3>For Developer</h3>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtQuery" runat="server" CssClass="form-control" placeholder="Enter query"></asp:TextBox>
                        <asp:Button ID="btnExecute" runat="server" Text="Execute" OnClick="btnExecute_Click" CssClass="btn btn-primary" />
                    </div>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <div class="col-md-6">
                </div>

                <div class="col-md-12">
                    <div style="height: 400px; width: 800px; overflow: auto;">
                        <asp:GridView ID="grdDeveloper" runat="server"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        activeModule = "settings";
    </script>

</asp:Content>
