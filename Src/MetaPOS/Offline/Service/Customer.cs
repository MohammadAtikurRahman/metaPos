using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Offline.Service
{
    public class Customer
    {
        public string initializeCustomer()
        {
            var commonFunction = new CommonFunction();
            var customerModel = new CustomerModel();
            customerModel.active = "1";
            customerModel.CusType = "All";

            return customerModel.getCustomerListOfflineSerializeModel();
        }
    }
}