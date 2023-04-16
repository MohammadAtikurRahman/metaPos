using System;
using System.Web;
using MetaPOS.Admin.Model;
using System.Data;


namespace MetaPOS.Admin.PromotionBundle.Service
{


    public class SmsService
    {



        public string medium { get; set; }

        public string apiKey { get; set; }

        public string senderKey { get; set; }

        public string senderId { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        



        public SmsService()
        {
            var smsConfigModel = new SmsConfigModel();
            var dataList = smsConfigModel.getSmsCofigData();

            if (dataList.Rows.Count > 0)
            {
                medium = dataList.Rows[0]["medium"].ToString();
                apiKey = dataList.Rows[0]["apiKey"].ToString();
                senderId = dataList.Rows[0]["senderId"].ToString();
                username = dataList.Rows[0]["username"].ToString();
                password = dataList.Rows[0]["password"].ToString();
                senderKey = dataList.Rows[0]["senderKey"].ToString();
            }
        }








        public string sendSmsService(string phoneNumber, string msg, double messageCost, int msgCount, string customer)
        {
            string result;

            if (medium == "elitbuzz")
            {
                result = elitbuzzSmsService(phoneNumber, msg, messageCost, msgCount);
                return result;
            }
            else if (medium == "infobip")
            {
                result = infobipSmsService(phoneNumber, msg, messageCost, msgCount);
                return result;
            }
            else if(medium == "default")
            {
                result = defaultSmsService(phoneNumber, msg, messageCost, msgCount, customer);
                return result;
            }
            else if (medium == "modem")
            {
                result = modemSmsService();
                return result;
            }
            

            return "Please select a medium";
        }






        // Elitbuzz
        public string elitbuzzSmsService(string phoneNumber, string msg, double msgCost, int msgCount)
        {
            var elitbuzzModel = new MetaSMS.elitbuzz.ElitbuzzModel();
            elitbuzzModel.apiKey = apiKey;
            elitbuzzModel.senderId = senderId;
            elitbuzzModel.phoneList = phoneNumber;
            elitbuzzModel.message = msg;

            var elitbuzz = new MetaSMS.elitbuzz.Elitbuzz();
            string result = elitbuzz.sendSMSByElitbuzz(elitbuzzModel);

            string[] splitSmsResult = result.Split(':');
            string resultKey = "";
            string resultId = "";
            string successId = "";
            string output = result;

            if (splitSmsResult.Length == 2)
            {
                resultKey = splitSmsResult[0];
                resultId = splitSmsResult[1];
                string[] splitSuccessId = resultId.Split('-');
                string successKey = splitSuccessId[0];
                successId = splitSuccessId[1];
            }

            // Save sms log in db
            if (resultKey == "SMS SUBMITTED")
            {
                var smsLogModel = new SmsLogModel();

                smsLogModel.message = msg;
                smsLogModel.deliveryId = successId;
                smsLogModel.phoneRecord = phoneNumber;
                smsLogModel.medium = medium;
                smsLogModel.msgCounter = msgCount;
                smsLogModel.msgCost = Convert.ToDecimal(msgCost);
                smsLogModel.sentAt = DateTime.Now;
                smsLogModel.roleId = Convert.ToInt32(HttpContext.Current.Session["roleId"]);
                smsLogModel.active = "1";
                smsLogModel.saveSmsLogInfoModel();
                output = "Message send successfuly.";
                //smsLogModel.saveSmsLogInfoModel();
            }

            return output;
        }





        // Infobip
        public string infobipSmsService(string phoneNumber, string msg, double msgCost, int msgCount)
        {
            var infobipModel = new MetaSMS.Infobip.InfobipModel();
            infobipModel.senderId = senderId;
            infobipModel.username = username;
            infobipModel.password = password;
            infobipModel.message = msg;
            infobipModel.phoneNumber = phoneNumber;

            var infobip = new MetaSMS.Infobip.Infobip();
            string resultFromInfobip = infobip.sendSmsByInfobip(infobipModel);

            string[] splitResultFromInfobip = resultFromInfobip.Split(';');

            string messageId = splitResultFromInfobip[0];
            int statusId = int.Parse(splitResultFromInfobip[1]);
            string description = splitResultFromInfobip[2];
            string output = description;

            if (statusId == 7)
            {
                var smsLogModel = new SmsLogModel();

                smsLogModel.message = msg;
                smsLogModel.deliveryId = messageId;
                smsLogModel.phoneRecord = phoneNumber;
                smsLogModel.medium = medium;
                smsLogModel.msgCounter = msgCount;
                smsLogModel.msgCost = Convert.ToDecimal(msgCost);
                smsLogModel.sentAt = DateTime.Now;
                smsLogModel.roleId = Convert.ToInt32(HttpContext.Current.Session["roleId"]);
                smsLogModel.active = "1";
                smsLogModel.saveSmsLogInfoModel();
                output = "Message send successfuly.";
            }

            return output;
        }


        public string defaultSmsService(string phoneNumber, string msg, double msgCost, int msgCount, string customer) 
        {
            var defaultModel = new MetaSMS.Default.DefaultModel();
            defaultModel.senderKey = senderKey; 
            defaultModel.message = msg;
            defaultModel.phoneNumber = phoneNumber;
            defaultModel.receiverType = "customer";
            defaultModel.title = customer;

            var defaultSend = new MetaSMS.Default.Default();
            string result = defaultSend.sendSmsByDefault(defaultModel);

            return result;
        }





        // Modem
        public string modemSmsService()
        {
            return "Medium have not found! ";
        }





        // Get Sms Balance
        public string getSmsBalance()
        {
            string getBalance = "0";

            if(medium == "elitbuzz")
            {
                var elitbuzzbalance = new MetaSMS.elitbuzz.ElitbuzzBalance();
                getBalance = elitbuzzbalance.getSmsBalanceFromElitbuzz(apiKey);
            }
            else if (medium == "infobip")
            {
                var infobipBalance = new MetaSMS.Infobip.InfobipBalance();
                getBalance = infobipBalance.getInfobipBalance();
            }
            else
            {
                var smsConfigModel = new SmsConfigModel();
                var dtSmsData = smsConfigModel.getSmsConfigDataByBranchId();
                if(dtSmsData.Rows.Count > 0)
                    getBalance = dtSmsData.Rows[0]["balance"].ToString();
            }

            return getBalance;
        }





        public string getReSendSms(string Id)
        {
            var smsLogModel = new SmsLogModel();

            var SmsDataList = smsLogModel.getReSendSmsData(Id);
            var message = SmsDataList.Rows[0]["message"].ToString();
            var phoneNumber = SmsDataList.Rows[0]["phoneRecord"].ToString();

            return message + ";" + phoneNumber;
        }



        public decimal getCustomSmsBalance()
        {
            var smsconfigModel = new SmsConfigModel();
            DataTable dtSms = smsconfigModel.getSmsConfigDataByBranchId();

            decimal balance = 0;
            if (dtSms.Rows.Count > 0)
            {
                //if (dtSms.Rows[0]["balance"].ToString() == "")
                balance = Convert.ToDecimal(dtSms.Rows[0]["balance"]);
            }

            return balance;

        }


        internal void updateBalance(decimal smsTotalCost)
        {
            var smsconfigModel = new SmsConfigModel();
            smsconfigModel.updateBalanceModel(smsTotalCost);
        }
    }




}