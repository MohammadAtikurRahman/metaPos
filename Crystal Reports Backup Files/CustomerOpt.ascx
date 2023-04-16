<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerOpt.ascx.cs" Inherits="MetaPOS.Admin.Controller.CustomerOpt" %>


<div class="modal fade" id="modalCustomer" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="modal-title lang-add-new-customer" id="myModalLabel">Add New Customer</h4>
                <label id="lblId" class="disNone"></label>
                <label id="lblActionPage" class="disNone"></label>
                <label id="lblActionText" class="disNone"></label>
            </div>
            <div class="modal-body">

                <div id="msgOutput" class="text-left"></div>

                <div class="form-group group-section">
                    <label class="col-md-4 col-sm-4 col-xs-4 lang-customer-type">Cusotmer Type</label>
                    <div class="col-md-8 col-sm-8 col-xs-8 rblCusType">

                        <label class="radio-inline">
                            <input type="radio" name="customerType" value="0" checked><span class="lang-customer-retail">Retailer</span>
                        </label>
                        <label class="radio-inline">
                            <input type="radio" name="customerType" value="1"><span class="lang-customer-wholesale">Wholesaler</span>
                        </label>

                    </div>
                </div>

                <div class="form-group group-section disNone" id="divAccountNo">
                    <label class="col-md-4 col-sm-4 col-xs-4 lang-account-no">Account No</label>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <input type="text" class="form-control" id="txtAccountNo" name="txtAccountNo" />
                    </div>
                </div>

                <div class="form-group group-section">
                    <label class="col-md-4 col-sm-4 col-xs-4 lang-customer-name">Name</label>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <input type="text" class="form-control char-input-validate" id="txtCustomerName" name="txtCustomerName" />
                    </div>
                </div>

                <div class="form-group group-section">
                    <div class="col-md-4 col-sm-4 col-xs-4">
                        <label class="lang-customer-phone">Phone</label>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <input type="text" class="form-control int-number-validate" id="txtCustomerPhone" name="txtCustomerPhone" />
                    </div>
                </div>
                <div class="form-group group-section disNone" id="divCusAge">
                    <div class="col-md-4 col-sm-4 col-xs-4">
                        <label class="lang-customer-age">Age</label>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <input type="text" class="form-control" id="txtCusAge" name="txtCusAge" />
                    </div>
                </div>
                <div class="form-group group-section">
                    <div class="col-md-4 col-sm-4 col-xs-4">
                        <label class="lang-customer-address">Address</label>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <input type="text" class="form-control" id="txtCustomerAddress" name="txtCustomerAddress" />
                    </div>
                </div>
                <div class="form-group group-section">
                    <div class="col-md-4 col-sm-4 col-xs-4">
                        <label class="lang-email-email">Email</label>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <input class="form-control" type="text" id="txtCustomerEmail" name="txtCustomerEmail" />
                    </div>
                </div>
                <div class="form-group group-section disNone" id="divCusDesignation">
                    <div class="col-md-4 col-sm-4 col-xs-4">
                        <label class="lang-customer-designation">Designation</label>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <input type="text" class="form-control" id="txtCusDesignation" name="txtCusDesignation" />
                    </div>
                </div>
                <div class="form-group group-section disNone" runat="server" id="divBloodGroup">
                    <asp:Label ID="Label9" CssClass="lbl col-sm-4 col-sm-4 col-xs-4 control-label" runat="server" Text="Blood Group"></asp:Label>
                    <div class="col-sm-8 col-sm-8 col-xs-8">
                        <asp:DropDownList runat="server" ID="ddlBloodGroup" CssClass="form-control">
                            <asp:ListItem Value="0">None</asp:ListItem>
                            <asp:ListItem Value="A+">A+</asp:ListItem>
                            <asp:ListItem Value="A–">A–</asp:ListItem>
                            <asp:ListItem Value="B+">B+</asp:ListItem>
                            <asp:ListItem Value="B-">B-</asp:ListItem>
                            <asp:ListItem Value="O+">O+</asp:ListItem>
                            <asp:ListItem Value="O-">O-</asp:ListItem>
                            <asp:ListItem Value="AB+">AB+</asp:ListItem>
                            <asp:ListItem Value="AB-">AB-</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                
                <div class="form-group group-section disNone" id="divSex">
                    <asp:Label ID="Label1" CssClass="lbl col-sm-4 col-sm-4 col-xs-4 control-label" runat="server" Text="Sex"></asp:Label>
                    <div class="col-sm-8 col-sm-8 col-xs-8">
                        <asp:DropDownList runat="server" ID="ddlSex" CssClass="form-control">
                            <asp:ListItem Value="Male">Male</asp:ListItem>
                            <asp:ListItem Value="Female">Female</asp:ListItem>
                            <asp:ListItem Value="Others">Others</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group group-section">
                    <div class="col-md-4 col-sm-4 col-xs-4">
                        <label class="lang-customer-notes">Notes</label>
                    </div>
                    <div class="col-md-8 col-sm-8 col-xs-8">
                        <textarea id="txtCustomerNotes" class="form-control"></textarea>
                    </div>
                </div>
                <div class="form-group disNone" id="divInstallmentStatus">
                    <div class="col-md-4 col-sm-4 col-xs-4"></div>
                    <div class="col-md-8 col-sm-8 col-xs-8 installment-status">
                        <input type="checkbox" id="chkInstallmentStatus" value="Pay with installment" />
                        <label for="chkInstallmentStatus" class="lang-customer-pay-with-installment">Pay with installment</label>
                    </div>
                </div>
            </div>
            <div style="clear: both"></div>
            <div class="modal-footer" runat="server" id="modalFooter">
                <button type="button" class="btn btnCustomize btn-close" data-dismiss="modal" id="btnCloseCusModal">Close</button>
                <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom btn-save" id="btnSaveCustomer">Save</button>
                <button type="button" class="btn btn-info btn-sm btnResize btnAddCustom btn-update" id="btnUpdateCustomer">Update</button>
            </div>
        </div>
    </div>
</div>
