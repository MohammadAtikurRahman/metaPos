<%@ Page Title="" Language="C#" MasterPageFile="~/Shop/MasterPage.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="MetaPOS.Shop.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title runat="server">About Us</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" runat="server">
    
    <div class="about-us">
        <div class="container">

            <div class="spec top-padding">
                <h3>About Us</h3>
                <div class="ser-t">
                    <b></b>
                    <span><i></i></span>
                    <b class="line"></b>
                </div>
            </div>

            <div class="about-text">
                <p>
                    <asp:Label runat="server" ID="lblAboutText"></asp:Label>
                </p>
            </div>
        </div>
    </div>

</asp:Content>