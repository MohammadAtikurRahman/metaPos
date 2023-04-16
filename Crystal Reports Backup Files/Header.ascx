<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="MetaPOS.Shop.Controller.Header" %>

<asp:PlaceHolder ID="PlaceHolder1" runat="server">

    <div class="header" runat="server" id="headerControl">
        <div class="container">
            <div class="logo">
                <h1><a href="../shop">
                        <asp:Label ID="lblSiteTitle" CssClass="header-title" runat="server"></asp:Label><span style="color: black;"><asp:Label ID="lblSiteSlogan" runat="server" Text=""></asp:Label></span></a></h1>
            </div>
            <div class="header-ri">
                <ul class="social-top">
                    <!-- <li><a href="#" class="icon facebook" target="_blank"><i class="fa fa-facebook" aria-hidden="true"></i><span></span></a></li>
                        <li><a href="#" class="icon twitter" target="_blank"><i class="fa fa-twitter" aria-hidden="true"></i><span></span></a></li>
                        <li><a href="#" class="icon google-plus" target="_blank"><i class="fa fa-google-plus" aria-hidden="true"></i><span></span></a></li> -->
                </ul>
            </div>
        </div>
    </div>

    <!-- No Website title -->
    <%--<h1 class="no-website text-center"><asp:Label ID="lblNoWebsiteTitle" CssClass="header-title" runat="server"></asp:Label> <br /> <span style="color: black;"><asp:Label ID="lblNoWebsiteSubTitle" runat="server"></asp:Label></span></h1>--%>

</asp:PlaceHolder>