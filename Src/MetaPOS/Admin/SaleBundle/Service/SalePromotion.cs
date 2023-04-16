using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.PromotionBundle.Service;
using Newtonsoft.Json;

namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SalePromotion
    {
        public string SendInvoiceSms(dynamic data)
        {
            var smsConfigModel = new SmsConfigModel();
            var commonFunction = new CommonFunction();


        var dataList = smsConfigModel.getSmsConfigDataByBranchId();

            var smsService = new SmsService();
            if (dataList.Rows.Count > 0)
            {
                smsService.medium = "elitbuzz";
                //smsService.medium = "default";
                //smsService.senderKey = dataList.Rows[0]["senderKey"].ToString();
                smsService.senderId = dataList.Rows[0]["senderKey"].ToString();
            }
            else
            {
                return "Please set SMS configuration properly.";
            }

            var promotionModel = new PromotionModel();
            var dtTemplate = promotionModel.GetInvoiceTemplateData();
            if (dtTemplate.Rows.Count == 0)
                return "You have no sms template. Please set before sending SMS.";

            var smsTemplate = dtTemplate.Rows[0]["invoiceSmsTemplate"].ToString();
            var ownerNumber = dtTemplate.Rows[0]["ownerNumber"].ToString().Trim();

            string customer="", phone="";
            var cusId = data["cusId"].ToString();
            var saleCustomer = new SaleCustomer();
            var dtCustomer = saleCustomer.getCustomerData(cusId);
            if(dtCustomer.Rows.Count > 0)
            {
                customer = dtCustomer.Rows[0]["name"].ToString();
                phone = dtCustomer.Rows[0]["phone"].ToString();
            }

            // phone is valid
            var mobileNo = phone;
            if (mobileNo.Length == 11)
            {
                phone = mobileNo;
            }
            else
            {
                return "Mobile number is invalid";
            }
           


            var billNo = data["billNo"].ToString();
            var paidAmt = data["payCash"].ToString();
            var dueAmt = data["giftAmt"].ToString();
            var cartAmount = data["netAmt"].ToString();
            var grandAmt = data["grossAmt"].ToString();

            //work here
            var payMethod = data["payMethod"].ToString();
            var bankId = data["brankId"].ToString();
            var cardId = data["cardId"].ToString();
            var payType = Convert.ToString(data["payType"]);
            var payDescr = Convert.ToString(data["payDescr"]);


            BankModel bankModel = new BankModel();
            DataTable dtBank = bankModel.GetBnakNameModel(bankId);
            var dbBankName = "";
            if (dtBank.Rows.Count > 0)
            {
                dbBankName = Convert.ToString(dtBank.Rows[0]["bankName"]);
            }


            CardModel cardModel = new CardModel();
            DataTable dtCard = cardModel.GetCardNameModel(cardId);
            var dbCardName = " ";
            if (dtCard.Rows.Count > 0)
            {
                dbCardName = Convert.ToString(dtCard.Rows[0]["cardName"]);
            }


            var payMedia = "";
            var payDesNo = "";
            if (payMethod == "1")
            {
                if (payType == cardId)
                {
                    payMedia = " Payment method is " + dbCardName + ",";
                }
                payDesNo = "card no " + payDescr;
            }
            else if (payMethod == "2")
            {
                if (payType == bankId)
                {
                    payMedia = " Payment method is " + dbBankName + ",";
                }
                payDesNo = "check no " + payDescr;
            }
            else if (payMethod == "3")
            {
                if (payType == "1")
                {
                    payMedia = " Payment method is bKash,";
                }
                else if (payType == "2")
                {
                    payMedia = " Payment method is Roket,";
                }
                else if (payType == "3")
                {
                    payMedia = " Payment method is Nogod,";
                }
                else if (payType == "4")
                {
                    payMedia = " Payment method is Mcash,";
                }

                payDesNo = "trxID " + payDescr;
            }
            else if (payMethod == "4")
            {
                if (payType == "1")
                {
                    payMedia = " Payment method is EMI(3Months),";
                }
                else if (payType == "2")
                {
                    payMedia = " Payment method is EMI(6Months),";
                }
                else if (payType == "3")
                {
                    payMedia = " Payment method is EMI(9Months),";
                }
                else if (payType == "4")
                {
                    payMedia = " Payment method is EMI(12Months),";
                }
                else if (payType == "5")
                {
                    payMedia = " Payment method is EMI(18Months),";
                }
                payDesNo = "card no " + payDescr;
            }
            else if(payMethod == "5")
            {
                payMedia = " Payment method is Cash on delivery,";
            }

            if (smsTemplate=="")
            {
                smsTemplate = "Dear @customer, Your Invoice number is @billno, Your paid amonut @paid Tk, Your due amount @due Tk.@paymehtod @paydescription Thanks for shopping.";
            }

            var message = smsTemplate.Replace("@billno", billNo).Replace("@paid", paidAmt).Replace("@due", dueAmt).Replace("@customer", customer).Replace("@cartAmt", cartAmount).Replace("@grandAmt", grandAmt).Replace("@paymehtod", payMedia).Replace("@paydescription", payDesNo);
            var messageCount = 1;

            string msgSendInvoice = "", msgSendOwner = "";
            if (commonFunction.findSettingItemValueDataTable("sendInvoiceBySms") == "1")
            {
                var phoneList = "+88" + phone;
                msgSendInvoice = smsService.sendSmsService(phoneList, message, 0, messageCount, customer);
            }

            if (commonFunction.findSettingItemValueDataTable("isSendSmsOwnerNumber") == "1" && ownerNumber.Length == 11)
            {
                var phoneList = "+88" + ownerNumber;
                msgSendOwner = smsService.sendSmsService(phoneList, message, 0, messageCount, customer);
            }


            return msgSendInvoice + "/" + msgSendOwner;
        }
    }
}