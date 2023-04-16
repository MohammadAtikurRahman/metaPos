<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Shared/_Layout.Master" AutoEventWireup="true" CodeBehind="Pricing.aspx.cs" Inherits="MetaPOS.Site.Views.Pricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Pricing | metaPOS
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Site/Content/asset/css/pricing.css") %>" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">

    <!-- TOP NAVIGATION END -->
    <section id="breadcrumb" class="breadcrumb">
        <div class="container">
            <div class="row text-center">
                <div class="col-sm-12">
                    <h1>মেটাপজ সাবস্ক্রাইব প্লান</h1>
                </div>
            </div>
        </div>
    </section>

    <!-- PRICING AREA START -->
    <section class="pricing">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <div class="section_heading">
                        <h1>খুব সহজে আপনার ব্যবসা শুরু করুন</h1>
                        <h2>সাথে পাচ্ছেন ফ্রি কাস্টমার সাপোর্ট</h2>
                    </div>
                </div>
            </div>
            <div class="row">
               <%--<div class="col-md-12 col-xs-12">--%>
                    <%-- <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active need_mar_l"><a href="#monthly" aria-controls="home" role="tab" data-toggle="tab">মাসিক</a></li>
                        <li role="presentation"><a href="#yearly" aria-controls="profile" role="tab" data-toggle="tab">বাৎসরিক</a></li>
                    </ul>--%>
                    <!-- Tab panes -->
                    <div class="col-md-2"></div>
                     <div class="col-md-4 pricing_plan_box basic text-center">
                        <h2 class="plan_header">বেসিক</h2>
                         <h2 class="pro_header">পাচ্ছেন ২৩ টি সুবিধা</h2>
                        <div class="feau_item new_item">
                            <ul>
                                <li><i class="fa fa-check ext" aria-hidden="true"></i>৫ জন ইউজার</li>
                                <li><i class="fa fa-check ext" aria-hidden="true"></i>ফ্রি সার্বক্ষণিক সাপোর্ট</li>
                                <li><i class="fa fa-check ext" aria-hidden="true"></i>পরবর্তী ব্রাঞ্চ এর জন্য ১০% ছাড়</li>
                                <li><i class="fa fa-check ext" aria-hidden="true"></i>ফিচার আপগ্রেডেশন সুবিধা</li>
                            </ul>
                        </div>
                        <div class="pricing_option">
                            <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                        </div>
                        <input type="button" class="btn btn-success btn-basic-demo" value="চলেন শুরু করি"/>
                        <div class="support">
                            <ul>
                                <li>সাপোর্টঃ</li>
                                <li>
                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                <li>
                                    <img src="/Site/Content/asset/img/online.png" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                <li>
                                    <img src="/Site/Content/asset/img/call.png" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
                                 <li>
                                    <img src="/Site/Content/asset/img/liveChat1.png" alt="চ্যাট" data-toggle="tooltip" data-placement="bottom" title="লাইভ চ্যাট হেল্প"></li>
                            </ul>
                        </div>
                    </div>
                     <div class="col-md-4 pricing_plan_box standard text-center">
                        <h2 class="plan_header">এন্টারপ্রাইজ</h2>
                        <h2 class="customization-benefit pro_header">পাচ্ছেন কাস্টমাইজেশন অনুযায়ী সুবিধা</h2>
                        <div class="feau_item new_item">
                            <ul>
                                <li>
                                    আপনার ব্যবসা প্রতিষ্ঠান অনুযায়ী কাস্টমাইজেশন করা হবে।
                                </li>
                            </ul>
                        </div>
                        <div class="pricing_option">
                            <p>* কাস্টমাইজেশন ও হোস্টিং চার্জ প্রযোজ্য</p>
                        </div>
                        <input type="button" class="btn btn-success btn-enterprise-contact" value="যোগাযোগ করুন"/>
                        <div class="support">
                            <ul>
                                <li>সাপোর্টঃ</li>
                                <li>
                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                <li>
                                    <img src="/Site/Content/asset/img/online.png" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                <li>
                                    <img src="/Site/Content/asset/img/call.png" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
                                <li>
                                    <img src="/Site/Content/asset/img/liveChat1.png" alt="চ্যাট" data-toggle="tooltip" data-placement="bottom" title="লাইভ চ্যাট হেল্প"></li>
                                <li>
                                    <img src="/Site/Content/asset/img/man.png" alt="ম্যান" data-toggle="tooltip" data-placement="bottom" title="ফিজিক্যাল হেল্প"></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-2"></div>

                    <%--<div class="tab-content">
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
                                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
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
                                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/online.png" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
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
                                                    <img src="/Site/Content/asset/img/online.png" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/call.png" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
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
                                                    <img src="/Site/Content/asset/img/online.png" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/call.png" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/man.png" alt="ম্যান" data-toggle="tooltip" data-placement="bottom" title="ফিজিক্যাল হেল্প"></li>
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
                                                    <img src="asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
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
                                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/online.png" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
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
                                                    <img src="/Site/Content/asset/img/online.png" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/call.png" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
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
                                                    <img src="/Site/Content/asset/img/online.png" alt="অনলাইন" data-toggle="tooltip" data-placement="bottom" title="অনলাইন হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/email.png" alt="ইমেইল" data-toggle="tooltip" data-placement="bottom" title="ই-মেইল হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/call.png" alt="কল" data-toggle="tooltip" data-placement="bottom" title="কল হেল্প"></li>
                                                <li>
                                                    <img src="/Site/Content/asset/img/man.png" alt="ম্যান" data-toggle="tooltip" data-placement="bottom" title="ফিজিক্যাল হেল্প"></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
            </div>
        </div>
    </section>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
