using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.CustomerBundle.Service
{
    public class CustomerDelete
    {
        public bool delRestoreCustomerData(string jsonData)
        {
            var data = (JObject) JsonConvert.DeserializeObject(jsonData);
            var customerModel = new CustomerModel();
            customerModel.cusId = data["id"].Value<string>();
            customerModel.active = data["active"].Value<string>();
            return customerModel.delRestoreCustomerDataModel();
        }
    }
}