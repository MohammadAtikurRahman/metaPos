<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menubar.ascx.cs" Inherits="MetaPOS.Shop.Controller.Menubar" %>

<div class="container text-center">
    <div class="nav-top">
        <nav class="navbar navbar-default">
            <div class="navbar-header nav_2">
                <button type="button" class="navbar-toggle collapsed navbar-toggle1" data-toggle="collapse" data-target="#bs-megadropdown-tabs">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
            </div>
            <div class="collapse navbar-collapse" id="bs-megadropdown-tabs">
                <ul class="nav navbar-nav" id="web-menu">
                    <li><a href="../shop" class="hyper "><span>Home</span></a></li>
                    <li><a href="../shop/product" class="hyper" onclick=""><span>Products</span></a></li>
                    <li><a href="../shop/about" class="hyper"><span>About Us</span></a></li>
                    <li><a href="../shop/contact" class="hyper"><span>Contact Us</span></a></li>
                </ul>
            </div>
        </nav>
        <div class="clearfix"></div>
    </div>
</div>