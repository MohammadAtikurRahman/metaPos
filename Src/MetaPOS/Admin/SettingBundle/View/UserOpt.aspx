<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserOpt.aspx.cs" Inherits="MetaPOS.Admin.SettingBundle.View.UserOpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Add / Update User
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SettingBundle/Content/User.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row ">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div id="divHeaderPanel">
                <label class="title" id="dynamicSectionBreadCrumb" runat="server"><%=Resources.Language.Title_addUser %></label>
                <asp:Label runat="server" ID="lblTest" BackColor="red"></asp:Label>
            </div>
        </div>
    </div>

    <div class="section useropt-section">
        <div class="row">
            <div class="col-md-6">

                <div class="form-group">
                    <label><%=Resources.Language.Lbl_addUser_name %> <span class="required">*</span></label>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label><%=Resources.Language.Lbl_addUser_email %> <span class="required">*</span></label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label><%=Resources.Language.Lbl_addUser_password %> <span class="required">*</span></label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <div class="form-group" runat="server" id="divConfirmPassword">
                    <label><%=Resources.Language.Lbl_addUser_confirm_password %> <span class="required">*</span></label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>

                <div class="form-group" runat="server" id="divStoreList" visible="false">
                    <label><%=Resources.Language.Lbl_addUser_store %> <span class="required">*</span></label>
                    <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group" runat="server" id="divBranchLimit" visible="false">
                    <label><%=Resources.Language.Lbl_addUser_branch_limit %> <span class="required">*</span></label>
                    <asp:TextBox ID="txtBranchLimit" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group" runat="server" id="divUserLimit" visible="false">
                    <label><%=Resources.Language.Lbl_addUser_user_limit %> <span class="required">*</span></label>
                    <asp:TextBox ID="txtUserLimit" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group" runat="server" id="divSubscriptionFee" visible="false">
                    <label><%=Resources.Language.Lbl_addUser_subscription_fee %> <span class="required">*</span></label>
                    <asp:TextBox ID="txtSubscriptionFee" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group" runat="server" id="divExpiryDate" visible="false">
                    <label><%=Resources.Language.Lbl_addUser_expiry_date %> <span class="required">*</span></label>
                    <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control datepickerCSS"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Button ID="btnSave" runat="server" Text="<%$Resources:Language, Btn_addUser_save %>" CssClass="btn btn-primary" OnClick="btnSave_Click"/>
                </div>
                
               

            </div>
            <div class="col-md-6">
                <div class="access-page">
                    <div class="row">
                       <div class="col-md-6">
                           <label>Access Page</label>
                       </div>
                       <div class="col-md-6">
                           <a href="~/admin/UserLogs?action=useropt" id="btnUserLogs" runat="server" class="btn btn-info" target="_blank">UserLog</a>  
                       </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Inventory</label><br />
                                <asp:CheckBox runat="server" ID="chkPurchase" Text="Purchase" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkStock" Text="Stock" CssClass="disBlock"/>
                                <asp:CheckBox runat="server" ID="chkPackage" Text="Package" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkWarranty" Text="Warranty" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkReturn" Text="Return" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkDamage" Text="Damage" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkCancel" Text="Cancel" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkWarning" Text="Warning" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkExpiry" Text="Expiry" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkImport" Text="Import" CssClass="disBlock" />
                            </div>

                            <div class="form-group">
                                <label>Sales</label><br />
                                <asp:CheckBox runat="server" ID="chkInvoice" Text="Invoice" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkCustomer" Text="Customer" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkQuotation" Text="Quotation" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkServicing" Text="Servicing" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkDueReminder" Text="DueReminder" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkService" Text="Service" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkToken" Text="Token" CssClass="disBlock" />

                            </div>

                            <div class="form-group">
                                <label>Accounting</label> <br />
                                <asp:CheckBox runat="server" ID="chkSupply" Text="Supply" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkReceive" Text="Receive" CssClass="disBlock" />  
                                <asp:CheckBox runat="server" ID="chkSalary" Text="Salary" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkExpense" Text="Expense" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkBanking" Text="Banking" CssClass="disBlock" /> 
                            </div>

                            <div class="form-group">
                                <label>Promotion</label> <br />
                                <asp:CheckBox runat="server" ID="chkOffer" Text="Offer" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkSMS" Text="SMS" CssClass="disBlock" />  
                            </div>

                        </div>
                        <div class="col-md-6">
                            
                            <div class="form-group">
                                <label>Reports</label> <br />
                                <asp:CheckBox runat="server" ID="chkDashboard" Text="Dashboard" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkSlip" Text="Slip" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkStockReport" Text="StockReport" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkPurchaseReport" Text="PurchaseReport" CssClass="disBlock" />  
                                <asp:CheckBox runat="server" ID="chkInventoryReport" Text="InventoryReport" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkSupplierCommission" Text="SupplierCommission" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkTransaction" Text="Transaction" CssClass="disBlock" />
                                <asp:CheckBox runat="server" ID="chkProfitLoss" Text="ProfitLoss" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkAnalytic" Text="Analytic" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkSummary" Text="Summary" CssClass="disBlock" /> 

                            </div>

                            <div class="form-group">
                                <label>Website</label> <br />
                                <asp:CheckBox runat="server" ID="chkEcommerce" Text="Ecommerce" CssClass="disBlock" /> 
                            </div>

                            <div class="form-group">
                                <label>Records</label><br />
                                <asp:CheckBox runat="server" ID="chkStore" Text="Store" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkManufacturer" Text="Manufacturer" CssClass="disBlock" />  
                                <asp:CheckBox runat="server" ID="chkLocation" Text="Location" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkSupplier" Text="Supplier" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkCategory" Text="Category" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkUnitMeasurement" Text="UnitMeasurement" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkField" Text="Field" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkAttribute" Text="Attribute" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkBank" Text="Bank" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkCard" Text="Card" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkParticular" Text="Particular" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkStaff" Text="Staff" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkServiceType" Text="ServiceType" CssClass="disBlock" /> 
                            </div>

                            <div class="form-group">
                                <label>Setting</label> <br />
                                <asp:CheckBox runat="server" ID="chkOffline" Text="Offline" CssClass="disBlock" /> 
                            </div>

                            <div class="form-group">
                                <label>Operation</label> <br />
                                <asp:CheckBox runat="server" ID="chkAdd" Text="Add" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkEdit" Text="Edit" CssClass="disBlock" /> 
                                <asp:CheckBox runat="server" ID="chkDelete" Text="Delete" CssClass="disBlock" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        activeModule = "settings";
    </script>

</asp:Content>
