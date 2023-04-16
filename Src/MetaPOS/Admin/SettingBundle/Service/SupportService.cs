using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.SettingBundle.Service
{
    public class SupportService
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public string groupId { get; set; }
        public string value { get; set; }

        public string changePaymentMode()
        {
            try
            {
                var dtBranch = sqlOperation.getDataTable("SELECT * FROM [roleInfo] WHERE groupId='" + groupId + "' ");

                var settingModel = new SettingModel();
                for (int i = 0; i < dtBranch.Rows.Count; i++)
                {
                    settingModel.column = "paymentMode";
                    settingModel.value = value;
                    settingModel.roleId = dtBranch.Rows[i]["roleId"].ToString();
                    settingModel.updateSettingInfoModel();
                }

                return "true|saved successfully.";
            }
            catch (Exception)
            {
                return "false|fail.";
            }

        }
    }
}