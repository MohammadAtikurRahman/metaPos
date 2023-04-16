<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="SubscriptionNew.aspx.cs" Inherits="MetaPOS.Admin.SubscriptionBundle.View.Subscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Subscription
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div id="divHeaderPanel">
                <asp:Label runat="server" ID="lblTest1" Text=""></asp:Label>
                <label class="title"><%=Resources.Language.Title_subscription %></label>
            </div>
        </div>
        <br/>
        <div class="col-md-offset-2 col-lg-offset-2">
            <div class="center-align">
                <b><h3>যে ভাবে আপনি সাবস্ক্রিপশন ফি পেমেন্ট করবেন</h3></b>
                <p>এখানে আমরা বিকাশ দিয়ে কীভাবে পেমেন্ট করবেন তা দেখিয়েছি ।</p>
                <p>এই ধাপ গুলো অনুসরন করুন:</p>
            </div>
            <br/>
            <div class="center-align">
                <p>ধাপ ১: বিকাশ পেমেন্টের জন্য ডায়াল করুন *247# ।</p>
                <p>ধাপ ২: "পেমেন্ট" অপশন সিলেক্ট করুন ।</p>
                <p>ধাপ ৩: মার্চেন্ট বিকাশ নাম্বার টি দিন (মার্চেন্ট নাম্বারঃ 01924572887) ।</p>
                <p>ধাপ ৪: যত টাকা ব্যালেন্স লোড করতে চান, সেই টাকা ইনপুট করুন ।</p>
                <p>ধাপ ৫: বেফারেন্স নিসাবে আপনার দোকান নামটি দিন: My Shop ।</p>
                <p>ধাপ ৬: কাউন্টার নাম্বার দিন : 1 ।</p>
                <p>ধাপ ৭: এরপর আপনার পিন নাম্বার দিন ।</p>
                <p>সবশেষে আপনি একটি কনফার্মেশন এস.এম.এস. পাবেন বিকাশ থেকে তাহলে আপনার পেমেন্ট টি সফলভাবে সম্পাদন হয়েছে ।</p>
            </div>
        </div>
    </div>
    <script>
       activeModule = "settings";
    </script>
</asp:Content>

