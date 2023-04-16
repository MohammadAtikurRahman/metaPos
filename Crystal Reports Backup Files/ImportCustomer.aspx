<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="ImportCustomer.aspx.cs" Inherits="MetaPOS.Admin.ImportBundle.View.ImportCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Import Customer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="customer-import">
        <div class="row">
            <div class="col-md-12">
                <h3>Customer Data Import</h3>
                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSaveToDatabase">
                    <h4>
                        <asp:Label runat="server" ID="lblCounter"></asp:Label>
                    </h4>
                    <div class="fileToDatabase form-inline">
                        <div class="form-group">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-default btn-file" AllowMultiple="true" />
                        </div>
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSaveToDatabase" CssClass="btn btn-primary" runat="server" Text="Save To Database" OnClick="btnSaveToDatabase_Click" OnClientClick=" return confirm('Are you sure you want to execute import?') " />

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSaveToDatabase" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel4" runat="server" CssClass="col-md-6" Visible="false">
                            <div class="progress">
                                <div id="processBar" class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="40"
                                     aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                    <asp:Label ID="lblPercentantce" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <hr />
            </div>
        </div>
    </div>
</asp:Content>