using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleProduct
    {
        private Admin.Model.StockModel stock = new Model.StockModel();
        CommonFunction commonFunction = new CommonFunction();




        public void loadProduct()
        {
        }





        public void removeProduct()
        {
        }





        public string getStockDataList(string prodCode)
        {
            var items = new Dictionary<string, string>();

            
            var dtItemList = stock.getStockDataTableModelByProdCode(prodCode);

            return commonFunction.serializeDatatableToJson(dtItemList);
        }




        public string getStockDataListByProductId(string prodId)
        {
            var items = new Dictionary<string, string>();


            var dtItemList = stock.getStockDataListModelByProdID(prodId);

            return commonFunction.serializeDatatableToJson(dtItemList);
        }











        public string getSaleInfoDataList(string billNo, string prodId)
        {
            var saleModel = new SaleModel();
            var commonFunction = new CommonFunction();

            DataTable dtSale = saleModel.getSaleInfoDataListModel(prodId,billNo);
            return commonFunction.serializeDatatableToJson(dtSale);
        }



        public string getProductDataListSoldItem(string billNo, string prodId)
        {
            var saleModel = new SaleModel();
            var dtSoldItemList = saleModel.getProductDataListSoldItemModel(prodId, billNo);
            return commonFunction.serializeDatatableToJson(dtSoldItemList);
        }

        public string getProductDataListNewItem(string prodId)
        {
            var saleModel = new SaleModel();
            var dtNewItemList = saleModel.getProductDataListNewItemModel(prodId);
            return commonFunction.serializeDatatableToJson(dtNewItemList);
        }

        public string getPackageDataList(string billNo,string prodCode)
        {
            var items = new Dictionary<string, string>();

            var dtItemList = stock.getPackageDataTableModel(prodCode);
            var packageData = commonFunction.serializeDatatableToJson(dtItemList);

            var saleProduct = new SaleProduct();
            var saleData = saleProduct.getSaleInfoDataList(billNo, prodCode);
            var dicPackageData = new Dictionary<string, string>();
            dicPackageData.Add("saleData", saleData);
            dicPackageData.Add("packageData", packageData);

            var multiSerialize = commonFunction.serializeDictionayToJson(dicPackageData);
            return multiSerialize;
        }
    }


}