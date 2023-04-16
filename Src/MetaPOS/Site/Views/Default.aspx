<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Shared/_Layout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MetaPOS.Site.Views.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Home | metaPOS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Site/Content/asset/css/index.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">

    <!-- HOME AREA START -->
    <!-- my-css -->
    <div class="home-page">
        <div class="header">
            <div class="container">
                <div class="row">
                    <div class="cell col-md-5 col-sm-12 col-xs-12">
                        <div class="heading-content">
                            <div class="h-title">
                                <h1>মেটাপজ</h1>
                                <h2>বিক্রয় ব্যবস্থাপনা সফটওয়্যার</h2>
                            </div>
                            <div class="p-text">
                                <p class="body--large">রি-টেইল, ক্লথ, সু, ইলেক্ট্রনিক্স, রেস্টুরেন্ট, জুয়েলারি, স্যানিটারি, ফার্নিচার ইত্যাদি সব ধরনের ব্যবসা প্রতিষ্ঠানের জন্য এই মেটাপজ সফটওয়্যারটি আপনাকে দিচ্ছে ক্রয়, বিক্রয়, স্টক, লেনদেন, ব্যাকআপ, তথ্য নিরাপত্তা সহ সব ধরনের ব্যবসায়িক পরিচালনার সুবিধা।</p>
                            </div>
                            <a href="http://web.metaposbd.com/signup/" class="btn btn-outline btn-xl go_metaPOS" target="_blank">শুরু করুন </a>
                        </div>
                    </div>
                    <div class="col-md-7 col-xs-12">
                        <div class="header_image">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/metaPOS(2).png") %>" alt="branding_image">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- my-css -->
        <!-- HOME AREA END -->
        <!-- TARGET CLIENT AREA START -->
        <section class="target_client">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12 col-xs-12 text-center">
                        <div class="section_heading">
                            <h1>ব্যবহারকারী ব্যবসা প্রতিষ্ঠানসমূহ</h1>
                            <h2>এই ধরনের প্রতিষ্ঠানগুলো মেটাপজ দ্বারা নিয়ন্ত্রিত হচ্ছে</h2>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <div class="carousel carousel-showmanymoveone slide" id="itemslider">
                            <div class="carousel-inner" style="min-height: 150px;">
                                <div class="item active">
                                    <div class="col-xs-12 col-sm-6 col-md-2">
                                        <div class="box">
                                            <a href="#">
                                                <img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/retail.png") %>" class="img-responsive center-block" title="Retails Shop Solution"></a>
                                            <h4 class="text-center">রিটেইল</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="col-xs-12 col-sm-6 col-md-2">
                                        <div class="box">
                                            <a href="#">
                                                <img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/cloth.png") %>" class="img-responsive center-block" title="Cloth Store Solution"></a>
                                            <h4 class="text-center">ক্লথ স্টোর</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="col-xs-12 col-sm-6 col-md-2">
                                        <div class="box">
                                            <a href="#">
                                                <img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/shoe.png")%>" class="img-responsive center-block" title="Shoe store Solution"></a>
                                            <h4 class="text-center">সু- স্টোর</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="col-xs-12 col-sm-6 col-md-2">
                                        <div class="box">
                                            <a href="#">
                                                <img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/electronic.png")%>" class="img-responsive center-block"></a>
                                            <h4 class="text-center">ইলেক্ট্রনিক শপ</h4>
                                        </div>
                                    </div>
                                </div>
                                <!--
                                                            <div class="item">
                                                                <div class="col-xs-12 col-sm-6 col-md-2">
                                                                    <div class="box">
                                                                        <a href="#"><img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/restorent.png")%>" class="img-responsive center-block"></a>
                                                                        <h4 class="text-center">রেস্টুরেন্ট</h4>
                                                                    </div>
                                                                </div>
                                                            </div>
                            -->
                                <div class="item">
                                    <div class="col-xs-12 col-sm-6 col-md-2">
                                        <div class="box">
                                            <a href="#">
                                                <img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/senetary.png")%>" class="img-responsive center-block"></a>
                                            <h4 class="text-center">স্যানিটারি শপ</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="col-xs-12 col-sm-6 col-md-2">
                                        <div class="box">
                                            <a href="#">
                                                <img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/furniture.png")%>" class="img-responsive center-block"></a>
                                            <h4 class="text-center">ফার্নিচার</h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="item">
                                    <div class="col-xs-12 col-sm-6 col-md-2">
                                        <div class="box">
                                            <a href="#" class="text-center"><i class="fas fa-truck extra"></i></a>
                                            <h4 class="text-center">ডিলারশিপ</h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="slider-control">
                                <a class="left carousel-control" href="#itemslider" data-slide="prev">
                                    <img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/left-arrow.png")%>" alt="Left" class="img-responsive"></a>
                                <a class="right carousel-control" href="#itemslider" data-slide="next">
                                    <img src="<%= ResolveUrl("~/Site/Content/asset/img/target_clients/right-arrow.png")%>" alt="Right" class="img-responsive"></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- TARGET CLIENT AREA END -->
        <!-- FEATIRES AREA START -->
        <section id="main-features" class="main-features bac">
            <div class="container text-center">
                <div class="row">
                    <div class="col-sm-12 col-xs-12 text-center">
                        <div class="section_heading">
                            <h1>ফিচার সমূহ</h1>
                            <div class="com_box">
                                <h2>আপনার ক্লথ স্টোর, জুয়েলারি শপ, ফার্নিচার শো-রুম, মটর বাইক শো-রুম, ইলেক্ট্রনিক্স শো-রুম, </h2>
                                <h2>অটো শো-রুম সহ যে কোন ধরনের বিজনেস মডেল কেন্দ্রিক তৈরি করা হয়েছে।</h2>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4 col-xs-12">
                        <div class="feature-item1">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/stock.png")%>" alt="">
                            <p>ইনভেন্টরি</p>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="feature-item1">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/accounts.png")%>" alt="">
                            <p>একাউন্টিং</p>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="feature-item1">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sale.png")%>" alt="">
                            <p>ক্রয়-বিক্রয়</p>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="feature-item1">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/branch.png")%>"  alt="">
                            <p>মাল্টি ব্রাঞ্চ</p>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="feature-item1">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/e-commerce.png")%>" alt="">
                            <p>ই-কমার্স</p>
                        </div>
                    </div>
                    <div class="col-sm-4 col-xs-12">
                        <div class="feature-item1">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/features/sms.png")%>" alt="">
                            <p>এস এম এস</p>
                        </div>
                    </div>
                </div>
                <div class="row text-center">
                    <div class="col-md-12"><a href="features" class="btn btn-xl explore_all go_features">সবগুলো ফিচার দেখুন</a> </div>
                </div>
            </div>
        </section>
        <!-- FEATIRES AREA END -->
        <!-- PRICING AREA START -->

        <%--<section class="pricing">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <div class="section_heading">
                            <h1>সাবস্ক্রাইব প্লান</h1>
                            <h2>আমাদের চারটি প্লানের মধ্য থেকে বেছে নিন আপনারটি</h2>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-12">
                        <ul class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active need_mar_l"><a href="#monthly" aria-controls="home" role="tab" data-toggle="tab">মাসিক</a></li>
                            <li role="presentation"><a href="#yearly" aria-controls="profile" role="tab" data-toggle="tab">বাৎসরিক</a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane fade in active" id="monthly">
                                <div class="container text-center">
                                    <div class="row pricing_plan">
                                        <div class="col-md-3 col-sm-6 col-xs-12 pricing_plan_box basic">
                                            <h2 class="plan_header">বেসিক</h2>
                                            <p><sup>৳</sup><b>৪৯৯</b><sub>/মাস</sub></p>
                                            <button class="btn btn-success">চলেন শুরু করি</button>
                                            <h2 class="pro_header">পাচ্ছেন ১১ টি সুবিধা</h2>
                                            <div class="feau_item new_item">
                                                <ul>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>২ জন ইউজার</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ৫% ছাড়</li>
                                                </ul>
                                            </div>
                                            <div class="pricing_option">
                                                <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                                            </div>
                                            <div class="support">
                                                <ul>
                                                    <li>সাপোর্টঃ</li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/email.png")%>" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 col-xs-12 pricing_plan_box standard">
                                            <h2 class="plan_header">স্ট্যান্ডার্ড</h2>
                                            <p><sup>৳</sup><b>১,৪৯৯</b><sub>/মাস</sub></p>
                                            <button class="btn btn-success">চলেন শুরু করি</button>
                                            <h2 class="pro_header">পাচ্ছেন ১৮ টি সুবিধা</h2>
                                            <div class="feau_item">
                                                <ul>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>৫ জন ইউজার</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ১০% ছাড়</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফিচার আপগ্রেডেশন সুবিধা</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>কাস্টম ডোমেইন সুবিধা</li>
                                                </ul>
                                            </div>
                                            <div class="pricing_option">
                                                <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                                            </div>
                                            <div class="support">
                                                <ul>
                                                    <li>সাপোর্টঃ</li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/email.png")%>" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/online.png")%>" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 col-xs-12 pricing_plan_box advance">
                                            <h2 class="plan_header">আডভান্স </h2>
                                            <div class="p-responsive">
                                                <p><sup>৳</sup><b>২,৯৯৯</b><sub>/মাস</sub></p>
                                            </div>
                                            <button class="btn btn-success">চলেন শুরু করি</button>
                                            <h2 class="pro_header">পাচ্ছেন ২১ টি সুবিধা</h2>
                                            <div class="feau_item">
                                                <ul>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>১০ জন ইউজার</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ১৫% ছাড়</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফিচার আপগ্রেডেশন সুবিধা</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>কাস্টম ডোমেইন সুবিধা</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>কাস্টমাইজেশন সুবিধা</li>
                                                </ul>
                                            </div>
                                            <div class="pricing_option2">
                                                <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                                            </div>
                                            <div class="support">
                                                <ul>
                                                    <li>সাপোর্টঃ</li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/online.png") %>" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/email.png")%>" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/call.png")%>" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-6 col-xs-12 pricing_plan_box premium">
                                            <h2 class="plan_header">প্রিমিয়াম</h2>
                                            <p><sup>৳</sup><b>৪,৯৯৯</b><sub>/মাস</sub></p>
                                            <button class="btn btn-success">চলেন শুরু করি</button>
                                            <h2 class="pro_header">পাচ্ছেন ২৩ টি সুবিধা</h2>
                                            <div class="feau_item">
                                                <ul>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>আনলিমিটেড ইউজার</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ২০% ছাড়</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি ফিচার আপগ্রেডেশন</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>কাস্টম ডোমেইন সুবিধা</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি কাস্টমাইজেশন</li>
                                                </ul>
                                            </div>
                                            <div class="pricing_option2">
                                                <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                                            </div>
                                            <div class="support">
                                                <ul>
                                                    <li>সাপোর্টঃ</li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/online.png")%>" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/email.png")%>" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/call.png")%>" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/man.png")%>" alt="ম্যান" data-toggle="tooltip" data-placement="bottom" title="ফিজিক্যাল হেল্প"></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane fade" id="yearly">
                                <div class="container text-center">
                                    <div class="row pricing_plan">
                                        <div class="col-sm-3 col-xs-12 pricing_plan_box basic">
                                            <h2 class="plan_header">বেসিক</h2>
                                            <p><sup>৳</sup><b>৪,৯৯৯</b><sub>/বছর</sub></p>
                                            <button class="btn btn-success">চলেন শুরু করি</button>
                                            <h2 class="pro_header">পাচ্ছেন ৫ টি সুবিধা</h2>
                                            <div class="feau_item new_item2">
                                                <ul>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>২ জন ইউজার</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ৫% ছাড়</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>হোস্টিং চার্জ প্রযোজ্য</li>
                                                </ul>
                                            </div>
                                            <div class="pricing_option">
                                                <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                                            </div>
                                            <div class="support">
                                                <ul>
                                                    <li>সাপোর্টঃ</li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/email.png")%>" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-xs-12 pricing_plan_box standard">
                                            <h2 class="plan_header">স্ট্যান্ডার্ড</h2>
                                            <p><sup>৳</sup><b>১৪,৯৯৯</b><sub>/বছর</sub></p>
                                            <button class="btn btn-success">চলেন শুরু করি</button>
                                            <h2 class="pro_header">পাচ্ছেন ৫ টি সুবিধা</h2>
                                            <div class="feau_item">
                                                <ul>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>৫ জন ইউজার</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ১০% ছাড়</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফিচার আপগ্রেডেশন সুবিধা</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>কাস্টম ডোমেইন সুবিধা</li>
                                                </ul>
                                            </div>
                                            <div class="pricing_option">
                                                <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                                            </div>
                                            <div class="support">
                                                <ul>
                                                    <li>সাপোর্টঃ</li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/email.png")%>" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/online.png")%>" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-xs-12 pricing_plan_box advance">
                                            <h2 class="plan_header">আডভান্স </h2>
                                            <p><sup>৳</sup><b>২৯,৯৯৯</b><sub>/বছর</sub></p>
                                            <button class="btn btn-success">চলেন শুরু করি</button>
                                            <h2 class="pro_header">পাচ্ছেন ৫ টি সুবিধা</h2>
                                            <div class="feau_item new_item3">
                                                <ul>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>১০ জন ইউজার</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ১৫% ছাড়</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফিচার আপগ্রেডেশন সুবিধা</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>কাস্টম ডোমেইন সুবিধা</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>কাস্টমাইজেশন সুবিধা</li>
                                                </ul>
                                            </div>
                                            <div class="pricing_option">
                                                <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                                            </div>
                                            <div class="support">
                                                <ul>
                                                    <li>সাপোর্টঃ</li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/online.png")%>" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/email.png")%>" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/call.png")%>" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-xs-12 pricing_plan_box premium">
                                            <h2 class="plan_header">প্রিমিয়াম</h2>
                                            <p><sup>৳</sup><b>৪৯,৯৯৯</b><sub>/বছর</sub></p>
                                            <button class="btn btn-success">চলেন শুরু করি</button>
                                            <h2 class="pro_header">পাচ্ছেন ৫ টি সুবিধা</h2>
                                            <div class="feau_item new_item3">
                                                <ul>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>আনলিমিটেড ইউজার</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ২০% ছাড়</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি ফিচার আপগ্রেডেশন</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>কাস্টম ডোমেইন সুবিধা</li>
                                                    <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি কাস্টমাইজেশন</li>
                                                </ul>
                                            </div>
                                            <div class="pricing_option">
                                                <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                                            </div>
                                            <div class="support">
                                                <ul>
                                                    <li>সাপোর্টঃ</li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/online.png")%>" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/email.png")%>" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/call.png")%>" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
                                                    <li>
                                                        <img src="<%= ResolveUrl("~/Site/Content/asset/img/man.png")%>" alt="ম্যান" data-toggle="tooltip" data-placement="bottom" title="ফিজিক্যাল হেল্প"></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row text-center">
                    <div class="col-md-12"><a href="#" class="btn btn-xl explore_all go_pricing">সবগুলো দেখুন</a> </div>
                </div>
            </div>
        </section>--%>

        <!-- PRICING AREA END -->
        <!-- TRUSTED AREA START -->
        <section id="trusted-by" class="trusted-by bac">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <div class="section_heading">
                            <h1>বিশ্বস্ততা</h1>
                            <h2>আপনার বিজনেসের সমস্ত তথ্যের ১০০% নিরাপত্তার দায়িত্ব মেটাপজের</h2>
                        </div>
                    </div>
                </div>
                <div class="row text-center">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-8 col-xs-12">
                        <div class="trusted_item">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/organigation.png")%>" alt="">
                            <h1>১০০+</h1>
                            <h2>ব্যবসা প্রতিষ্ঠান</h2>
                        </div>
                    </div>
                    <%--<div class="col-sm-4 col-xs-12">
                        <div class="trusted_item">
                            <img src="<%= ResolveUrl("~/Site/Content/asset/img/user.png")%>" alt="">
                            <h1>৫০০০+</h1>
                            <h2>ব্যবহারকারী</h2>
                        </div>
                    </div>--%>
                    <div class="col-sm-2"></div>
                </div>
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <div class="section-heading">
                            <h3>বর্তমান সারা বাংলাদেশে সর্বমোট ১০০+ ব্যবসা প্রতিষ্ঠান মেটা পজ দ্বারা নিয়ন্ত্রিত হচ্ছে</h3>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- TRUSTED AREA END -->
        <!-- VERSION AREA2 START -->
        <div id="my-design" class="my-design">
            <div class="container">
                <div class="row">
                    <div class="section_heading">
                        <h1>সংযোজন সমূহ</h1>
                        <h2>আমাদের সংযোজন সমূহ দেখুন</h2>
                    </div>
                </div>
                <div class="row text-center">
                    <div class="container">
                        <div class="row version-box">
                            <div class="col-md-12">
                                <h2 class="app-name">মেটা পজ</h2>
                                <h2 class="version-name">৬.০.০</h2>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-4 col-xs-12">
                            <h3 class="link add_new ">৭ টি নতুন সংযোজন</h3>
                        </div>
                        <div class="col-md-4 col-xs-12">
                            <h3 class="link updated">১২ টি ফিচার বৃদ্ধি</h3>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                </div>
            </div>
        </div>
        <!-- VERSION AREA2 START -->
        <!-- TESMONIAL AREA START -->
        <section class="client_testimonials">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <div class="section_heading">
                            <h1>মেটাপজ এর ব্যাপারে আমাদের কাস্টমার যা বলে</h1>
                            <h2>দেখুন মেটাপজের সম্পর্কে কাস্টমারদের মতামত</h2>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="carousel slide slider2" data-ride="carousel" id="quote-carousel">
                            <!-- Bottom Carousel Indicators -->
                            <ol class="carousel-indicators">
                                <li data-target="#quote-carousel" data-slide-to="0" class="active">
                                    <img class="img-responsive " src="<%= ResolveUrl("~/Site/Content/asset/img/Clients/01.jpg")%>" alt="">
                                </li>
                                <li data-target="#quote-carousel" data-slide-to="1">
                                    <img class="img-responsive" src="<%= ResolveUrl("~/Site/Content/asset/img/Clients/02.jpg")%>" alt="">
                                </li>
                            </ol>
                            <!-- Carousel Slides / Quotes -->
                            <div class="carousel-inner text-center slider2">
                                <!-- Quote 1 -->
                                <div class="item active">
                                    <blockquote>
                                        <div class="row">
                                            <div class="col-sm-8 col-sm-offset-2">
                                                <p>মেটা-পজ সফটওয়্যারের মাধ্যমে আমার বিসনেস নিয়ন্ত্রণ করাটা এখন অনেক সহজ হয়ে গেছে। আমি এখন অতি সহজেই ঘরে বসে থেকে একাধিক ব্রাঞ্চ নিয়ন্ত্রণ করতে পারছি। </p>
                                                <small>ডিরেক্টর, আনশা জুয়েলার্স</small>
                                            </div>
                                        </div>
                                    </blockquote>
                                </div>
                                <!-- Quote 2 -->
                                <div class="item">
                                    <blockquote>
                                        <div class="row">
                                            <div class="col-sm-8 col-sm-offset-2">
                                                <p>সফটওয়্যারটি আমার বিসনেসকে অনেক স্মার্ট করেছে। সফটওয়ারটি থেকে আমি আমার ক্রয়, বিক্রয় থেকে শুরু করে প্রতিষ্ঠানের সব ধরনের একাউন্টিং সম্পর্কিত কাজ সম্পন্ন করতে পারি। </p>
                                                <small>প্রোপ্রাইটর, তাহমীন ট্রেডার্স</small>
                                            </div>
                                        </div>
                                    </blockquote>
                                </div>
                            </div>
                            <!-- Carousel Buttons Next/Prev -->
                            <a data-slide="prev" href="#quote-carousel" class="left carousel-control"><i class="fa fa-chevron-left"></i></a><a data-slide="next" href="#quote-carousel" class="right carousel-control"><i class="fa fa-chevron-right"></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- TESMONIAL AREA END -->
    </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
