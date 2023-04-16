using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.CustomerBundle.Service
{
    public class CustomerSelectData
    {
        public string getCustomerDataList(string jsonData)
        {
            var commonFunction = new CommonFunction();
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);
            var getCustomerList = new CustomerModel();
            getCustomerList.CusType = data["cusType"].Value<string>();
            getCustomerList.PayStatus = data["payStatus"].Value<string>();
            getCustomerList.active = data["activeStatus"].Value<string>();
            getCustomerList.parameterAccess = " AND (roleId='" + HttpContext.Current.Session["roleId"] + "' OR branchId='" + HttpContext.Current.Session["roleId"] + "')";

            if (commonFunction.findSettingItemValueDataTable("isSeparateStore") == "1")

                getCustomerList.parameterAccess = HttpContext.Current.Session["roleIdAccessStoreWise"].ToString();
            else
                getCustomerList.parameterAccess = HttpContext.Current.Session["userAccessParameters"].ToString();

             return getCustomerList.getCustomerListSerializeModel();
        }
    }
}