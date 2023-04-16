using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleServicing
    {
        CommonFunction commonFunction = new CommonFunction();
        internal string saveServiceInfoData(string serviceData)
        {
             var data = (JObject)JsonConvert.DeserializeObject(serviceData);

            var servicingModel = new ServicingModel();
            servicingModel.serviceId = genreateNextServiceID();
            servicingModel.cusId = data["customerId"].Value<string>();
            servicingModel.prodId = data["prodId"].Value<string>();
            servicingModel.prodName = data["prodName"].Value<string>();
            servicingModel.imei = data["imei"].Value<string>();
            servicingModel.supplier = data["supplier"].Value<string>();
            servicingModel.supplierId = data["supplierId"].Value<string>();
            servicingModel.deliveryDate = data["deliveryDate"].Value<string>();
            servicingModel.description = data["description"].Value<string>();
            servicingModel.paidAmt = data["paidAmt"].Value<decimal>();
            servicingModel.totalAmt = data["totalAmt"].Value<decimal>();
            servicingModel.entryDate = commonFunction.GetCurrentTime();
            servicingModel.updateDate = commonFunction.GetCurrentTime();
            servicingModel.roleId = HttpContext.Current.Session["roleId"].ToString();
            servicingModel.active = data["active"].Value<string>();

            // cash report info 
            decimal paidAmt = data["paidAmt"].Value<decimal>();
            decimal totalAmt = data["totalAmt"].Value<decimal>();

            string serviceID = commonFunction.nextServiceId();

            string cusID = data["customerId"].Value<string>();
            commonFunction.cashTransactionSales(paidAmt, 0, "Service payment", cusID, cusID, serviceID, "7", "0",
                commonFunction.GetCurrentTime().ToString());
            
            // custoer adjustment
            CustomerModel customerModel = new CustomerModel();

            // get customer due
            var dsCus = customerModel.getCustomerByCondition(" cusID='" + cusID + "'");
            decimal dbCusDue = Convert.ToDecimal(dsCus.Tables[0].Rows[0][10]);
            decimal openingDue = Convert.ToDecimal(dsCus.Tables[0].Rows[0][22]);

            decimal serviceDue = totalAmt - paidAmt;
            customerModel.totalPaid = paidAmt;
            customerModel.totalDue = serviceDue + dbCusDue;
            customerModel.openingDue = openingDue;
            customerModel.cusId = data["customerId"].Value<string>();
            customerModel.updateCustomerInfoModel();

            return servicingModel.saveServiceInfoDataModel(serviceData);
        }

        public string genreateNextServiceID()
        {
            ServicingModel serviceModel = new ServicingModel();
            DataTable dtService = serviceModel.generateNextServiceIdModel();
            
             string nextBillNoRequire = "";
            

            try
            {
                nextBillNoRequire = commonFunction.nextIdGenerator(dtService.Rows[0][0].ToString());
            }
            catch
            {
                nextBillNoRequire = "SS00001";
            }

            return nextBillNoRequire;
        }
    }
}