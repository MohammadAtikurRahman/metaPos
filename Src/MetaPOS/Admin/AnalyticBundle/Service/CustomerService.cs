using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.AnalyticBundle.Service
{
    public class CustomerService
    {


        public DataTable getCustomerListByDate(DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var customerModel = new CustomerModel();
            return customerModel.getCustomerListByDateModel(dateTimeFrom, dateTimeTo);
        }


    }
}