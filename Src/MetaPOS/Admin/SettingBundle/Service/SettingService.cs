using System.Web;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;


namespace MetaPOS.Admin.SettingBundle.Service
{


    public class SettingService
    {


        private SettingModel settingModel = new SettingModel();





        public string updateSettingInfo(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);
            var branchId = data["branchId"].Value<string>();

            if (branchId == "")
            {
                string tempId = HttpContext.Current.Session["roleId"].ToString();
                if (tempId == "0")
                    tempId = HttpContext.Current.Session["roleId"].ToString();

                branchId = tempId;
            }


            settingModel.column = data["column"].Value<string>();
            settingModel.value = data["value"].Value<string>();
            settingModel.roleId = branchId;

            return settingModel.updateSettingInfoModel();
        }




    }


}