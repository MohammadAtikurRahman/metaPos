<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Shared/_Layout.Master" AutoEventWireup="true" CodeBehind="Features.aspx.cs" Inherits="MetaPOS.Site.Views.Features" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Features | Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Site/Content/asset/css/features.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">

    <!-- TOP NAVIGATION END -->
    <section id="breadcrumb" class="breadcrumb">
        <div class="container">
            <div class="row text-center">
                <div class="col-sm-12 col-xs-12">
                    <h1>মেটাপজ ফিচারসমূহ</h1>
                </div>
            </div>
        </div>

    </section>
    <!-- FEATIRES AREA START -->
    <section id="main-features" class="main-features bac">
        <div class="container text-center">
            <div class="row">
                <div class="col-sm-4 col-xs-12">
                    <div class="feature-item1">
                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/stock.png") %>" alt="">
                        <p>স্টক ম্যানেজমেন্ট</p>
                    </div>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <div class="feature-item1">
                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/accounts.png") %>" alt="">
                        <p>একাউন্ট ম্যানেজমেন্ট</p>
                    </div>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <div class="feature-item1">
                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sale.png") %>" alt="">
                        <p>ক্রয়-বিক্রয় ম্যানেজমেন্ট</p>
                    </div>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <div class="feature-item1">
                        <img src="<%=ResolveUrl("~/Site/Content/asset/img/features/branch.png") %>" alt="">
                        <p>মাল্টি ব্রাঞ্চ ম্যানেজমেন্ট</p>
                    </div>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <div class="feature-item1">
                        <img src="<%=ResolveUrl("~/Site/Content/asset/img/features/e-commerce.png") %>" alt="">
                        <p>ই-কমার্স ইন্সটলেশন</p>
                    </div>
                </div>
                <div class="col-sm-4 col-xs-12">
                    <div class="feature-item1">
                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sms.png") %>" alt="">
                        <p>এস এম এস</p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- FEATIRES AREA END -->
    <!-- FEATURES EXPLORAL AREA START -->
    <section class="expl_features">
        <div class="container">
            <div class="row feature_bottom">
                <div class="col-sm-5 ">
                    <h1>ইনভেন্টরি:</h1>
                    <p class="features_des">আপনার প্রোডাক্টের সব ধরনের ট্র্যাকিং সহজ করতে মেটাপজের এই ইনভেন্টরি মডিউল। যেখানে আপনি পাচ্ছেন স্টক ম্যনেজমেন্ট সহ নিচে উল্লেখিত সুবিধা গুলো</p>
                    <ul>
                        <li><i class="fa fa-check" aria-hidden="true"></i>প্রোডাক্ট ফিচার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>রিটার্ন ফিচার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ড্যামেজ ফিচার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ক্যানসেল ফিচার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ওয়ার্নিং ফিচার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ওয়ারেন্টি ফিচার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>অফার ফিচার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>প্যকেজ ফিচার</li>
                    </ul>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-6">
                    <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sc/stock.png") %>" alt="" class="img-responsive">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <img src="<%=ResolveUrl("~/Site/Content/asset/img/features/sc/sales.png") %>" alt="" class="img-responsive">
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <h1>সেলস:</h1>
                    <p class="features_des">ব্যবসার সবচেয়ে কঠিন কাজ হল সেলস ম্যনেজ করা।এই কঠিন কাজকে সহজ করে দিয়েছে মেটাপজের সেলস মডিউল। সহজেই প্রতিষ্ঠানের সেলস ম্যানেজ করুন</p>
                    <ul>
                        <li><i class="fa fa-check" aria-hidden="true"></i>সেলস ফিচার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>কাস্টমার পেমেন্ট হিষ্ট্রি</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>স্লিপ হিষ্ট্রি</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>কোটেশন ফিচার</li>
                    </ul>
                </div>
            </div>
            <div class="row feature_bottom">
                <div class="col-sm-5">
                    <h1>অ্যাকাউন্টস:</h1>
                    <p class="features_des">ব্যবসা মানেই হিসাব। আর সেই হিসাবকে সহজ করতেই মেটাপজের অ্যাকাউন্টস মডিউল, যেখানে আপনি আপনার প্রয়োজনীয় সব রিপোর্টই পাচ্ছেন</p>
                    <ul>
                        <li><i class="fa fa-check" aria-hidden="true"></i>পারচেজ রিপোর্ট</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>এক্সপেন্স রিপোর্ট</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>স্যালারি রিপোর্ট</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ব্যাংক স্টেটমেন্ট লেজার</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ট্রানজেকশন রিপোর্ট</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ব্যালেন্স শিট</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>সামারি রিপোর্ট</li>
                    </ul>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-6">
                    <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sc/accounts.png") %>" alt="" class="img-responsive">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sc/summary.png") %>" alt="" class="img-responsive">
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <h1>গ্রাফ ও চার্ট মডিউল:</h1>
                    <p class="features_des">মেটাপজের এই মডিউলে আপনি খুব সহজেই আপনার প্রতিষ্ঠানের অগ্রগতি বুজতে পারবেন গ্রাফ ও চার্ট আকারে</p>
                    <ul>
                        <li><i class="fa fa-check" aria-hidden="true"></i>প্রতিদিনের সেলস এর চার্ট</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>প্রফিট অংশ দেখার সুবিধা</li>
                    </ul>
                </div>
            </div>
            <div class="row feature_bottom">
                <div class="col-sm-5">
                    <h1>ই-কমার্স মডিউল:</h1>
                    <p class="features_des">মেটাপজ আপনাকে দিচ্ছে একটি ই-কমার্স সাইট ওপেন করার সুবিধা, যেখানে আপনি আপনার প্রতিষ্ঠানের টপ সেলস প্রোডাক্টগুলো দেখাতে পারবেন, তার সাথে পারবেন ক্যাটাগরি অনুযায়ী প্রোডাক্ট দেখানোর সুযোগ।</p>
                    <ul>
                        <li><i class="fa fa-check" aria-hidden="true"></i>টপ সেলস প্রোডাক্ট সমূহ দেখানোর সুবিধা</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ক্যটাগরি অনুযায়ী প্রোডাক্টের বিস্তারিত অ্যাড করার সুযোগ</li>
                    </ul>
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-6">
                    <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sc/ecommerce.png") %>" alt="" class="img-responsive">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sc/sms.png") %>" alt="" class="img-responsive">
                </div>
                <div class="col-sm-1"></div>
                <div class="col-sm-5">
                    <h1>প্রমোশন মডিউল:</h1>
                    <p class="features_des">আগের কাস্টমার যদি আবার এসে প্রোডাক্ট কেনে তাহলে অসুবিধা কোথায়?, মেটাপজের একটি চমৎকার অপশন হল প্রমোশন। যেখানে আপনি আপনার আগের কাস্টমারকে নানাবিদ নোটিফিকেশন পাঠাতে পারবেন</p>
                    <ul>
                        <li><i class="fa fa-check" aria-hidden="true"></i>এসএমএস পাঠানোর সুবিধা</li>
                        <li><i class="fa fa-check" aria-hidden="true"></i>ফেসবুক প্রমোশন করার সুবিধা</li>
                    </ul>
                </div>
            </div>
        </div>
    </section>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
