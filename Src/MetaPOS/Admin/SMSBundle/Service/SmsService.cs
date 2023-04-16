using MetaPOS.Admin.Model;
using MetaPOS.Admin.SMSBundle.Entity;


namespace MetaPOS.Admin.SMSBundle.Service
{


    public class SMSService
    {



        public object findSMSConfigInfo()
        {
            var smsconfigModel = new SmsConfigModel();
            var dtSMS = smsconfigModel.getSmsConfigDataByBranchId();

            var smsEntity = new SMSEntity();

            if(dtSMS.Rows.Count > 0)
            {
                smsEntity.username = dtSMS.Rows[0]["username"].ToString();
                smsEntity.password = dtSMS.Rows[0]["password"].ToString();
                smsEntity.apiKey = dtSMS.Rows[0]["apiKey"].ToString();
            }
            else
            {
                smsEntity.username = null;
                smsEntity.password = null;
                smsEntity.apiKey = null;
            }

            return smsEntity;
        }


    }


}