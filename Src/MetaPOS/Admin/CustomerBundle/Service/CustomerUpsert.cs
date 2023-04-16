using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared.Json;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.CustomerBundle.Service
{


    public class CustomerUpsert
    {


        public List<ListItem> getCustomerList()
        {
            CustomerModel objCustomerModel = new CustomerModel();
            return objCustomerModel.getCustomerModel();
        }

        public List<ListItem> searchCustomerList(string searchTxt)
        {
            CustomerModel objCustomerModel = new CustomerModel();
            return objCustomerModel.SearchCustomerModel(searchTxt);

        }





        public string saveCustomerInfo(string jsonStrData)
        {
            var customerModel = new CustomerModel();
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            customerModel.name = data["name"].Value<string>();
            customerModel.phone = data["phone"].Value<string>();
            customerModel.address = data["address"].Value<string>();
            customerModel.mailInfo = data["email"].Value<string>();
            customerModel.notes = data["notes"].Value<string>();
            customerModel.CusType = data["cusType"].Value<string>();
            customerModel.accountNo = data["accountNo"].Value<string>();
            customerModel.installmentStatus = data["installmentStatus"].Value<bool>();
            customerModel.designation = data["designation"].Value<string>();
            customerModel.age = data["age"].Value<int>();
            customerModel.sex = data["sex"].Value<string>();
            customerModel.bloodGroup = data["bloodGroup"].Value<string>();
            
            return customerModel.saveCustomerInfoModel();
        }





        public string updateCustomerInfo(string jsonStrData)
        { 
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);
            return data["cusId"].Value<string>();
        }




        private List<decimal> updateCustomerDueAdjustment(decimal grossAmt, decimal paycash, decimal dbCusDue, decimal openingDue, decimal saleReturnAmt)
        {
            // Return Amt
            decimal currentDue = 0;
            if (saleReturnAmt > 0)
            {
                if (saleReturnAmt <= dbCusDue)
                {
                    currentDue = dbCusDue - saleReturnAmt;
                }
                else
                {
                    openingDue = (dbCusDue + openingDue) - saleReturnAmt;
                    currentDue = 0;

                    if (openingDue < 0)
                        openingDue = 0;
                }
            }
            else
            {
                currentDue = dbCusDue;
            }



            if (paycash < 0)
            {
                paycash = paycash;
                openingDue = 0;
                currentDue = 0;
            }
            else if (paycash < currentDue)
            {
                currentDue -= paycash;
            }
            else if (paycash <= openingDue + currentDue)
            {
                openingDue = (currentDue + openingDue) - paycash;
                currentDue = 0;
            }



            var updateCustomerDueAdjust = new List<decimal>();
            updateCustomerDueAdjust.Add(currentDue);
            updateCustomerDueAdjust.Add(openingDue);
            updateCustomerDueAdjust.Add(paycash);

            return updateCustomerDueAdjust;
        }





        private List<decimal> saveCustomerDueAdjustment(decimal grossAmt, decimal paycash, decimal dbDue, decimal openingDue)
        {
            decimal currentDue = 0;
            if (paycash < grossAmt + dbDue)
            {
                currentDue = (grossAmt + dbDue) - paycash;
            }
            else if (paycash <= grossAmt + openingDue + dbDue)
            {
                openingDue = (grossAmt + dbDue + openingDue) - paycash;
                currentDue = 0;
            }

            var saveCustomerDueAdjust = new List<decimal>();
            saveCustomerDueAdjust.Add(currentDue);
            saveCustomerDueAdjust.Add(openingDue);

            return saveCustomerDueAdjust;
        }



        public string changeCustomerUpdateInfo(string cusId)
        {
            var customerModel = new CustomerModel();
            return customerModel.getCustomerDataSerilize(cusId);
        }

        public bool updateCustomerInfoData(string jsonData)
        {
            var customerModel = new CustomerModel();
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);

            customerModel.cusId = data["id"].Value<string>();
            customerModel.name = data["name"].Value<string>();
            customerModel.phone = data["phone"].Value<string>();
            customerModel.address = data["address"].Value<string>();
            customerModel.notes = data["notes"].Value<string>();
            customerModel.CusType = data["cusType"].Value<string>();
            customerModel.mailInfo = data["mailinfo"].Value<string>();
            customerModel.accountNo = data["accountNo"].Value<string>();

            return customerModel.updateCustomerDataModel();
        }

    }


}