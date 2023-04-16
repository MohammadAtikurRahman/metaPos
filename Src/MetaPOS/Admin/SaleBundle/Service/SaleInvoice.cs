using System;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.SaleBundle.Service
{


    public class SaleInvoice
    {


        private CommonFunction commonFunction = new CommonFunction();
        private SaleModel saleModel = new SaleModel();





        public string getInvoiceDataListSerialize(string searchTxt)
        {
            var dt = saleModel.getSaleInfoDataListModel(searchTxt);

            if (dt.Rows.Count <= 0)
                return "";

            return commonFunction.serializeDatatableToJson(dt);
        }





        public string getItemSaleDataList(string billNo)
        {
            var dt = saleModel.getSaleInfoDataListModel(billNo);

            if (dt.Rows.Count <= 0)
                return "";

            return commonFunction.serializeDatatableToJson(dt);
        }





        public string genearateBillNoInfo()
        {
            return saleModel.generateSaleId();
        }





        public void getLastBillNo(string billNo)
        {
            DataSet ds = saleModel.getLastBillNoModel(billNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string lastID = ds.Tables[0].Rows[0][0].ToString();
                setInvoiceBillNo(lastID);
            }
        }





        public void setInvoiceBillNo(string billNo)
        {
            ShortInvoice.printBillingNo = billNo;
            ReceiptInvoice.printBillingNo = billNo;
            GodownInvoice.printBillingNo = billNo;
        }





        public string getNextBillNo()
        {
            try
            {
                string nextBillNoRequire = "";
                DataSet ds = saleModel.getInsertedLastBillNoDesc();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    nextBillNoRequire = commonFunction.nextIdGenerator(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    nextBillNoRequire = "AA00001";
                }

                return nextBillNoRequire;
            }
            catch (Exception)
            {
                commonFunction.pageout();
            }
            return "";
        }



        public string changeSaleInfoStatus(string billNo)
        {
            var transactionQuery = "";
            var saleModel = new SaleModel();
            transactionQuery += "BEGIN ";
            saleModel.billNo = billNo;
            transactionQuery += saleModel.changeSaleInfoStatusModel();
            transactionQuery += "END ";

            return transactionQuery;
        }





        public string getSaleLastID()
        {
            var saleModel = new SaleModel();
            return saleModel.getSaleLastIDStoreWise();
        }

    }


}