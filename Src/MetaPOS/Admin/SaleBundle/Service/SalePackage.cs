using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SalePackage
    {
        StockModel stockModel = new StockModel();
        PackageModel packageModel = new PackageModel();
        CommonFunction commonFunction = new CommonFunction();

        public string getPackageDataList(string prodId)
        {

            var dtItemList = packageModel.getPackageDataListModelByPackId(prodId);

            return commonFunction.serializeDatatableToJson(dtItemList);
        }

        public string getEmptyCylinderQty(string code)
        {
            try
            {
                string[] splitText = new string[] { };
                string prodId = "";
                string gasQty = "0", cylinderQty = "0", packageQtyList = "";
                decimal emptyQty = 0;

                splitText = code.Split(';');
                int arrayCount = splitText.Length - 1;
                DataTable dtStock;
                for (int i = 0; i < arrayCount; i++)
                {
                    prodId = splitText[i];

                    dtStock = stockModel.getItemStockDataListModelByProdID(prodId);

                    if (dtStock.Rows.Count > 0)
                        //lblTest.Text = dsQtyInfo.Tables[0].Rows.Count.ToString();
                        packageQtyList += dtStock.Rows[0]["qty"]+ ";";
                }

                string[] splitQty = packageQtyList.Split(';');
                if (splitQty.Length > 1)
                {
                    gasQty = splitQty[0];
                    cylinderQty = splitQty[1];
                }

                if (gasQty == "")
                    gasQty = "0";
                if (cylinderQty == "")
                    cylinderQty = "0";

                decimal getGasQty = Convert.ToDecimal(gasQty);
                decimal getCylinderQty = Convert.ToDecimal(cylinderQty);


                if (getGasQty > getCylinderQty)
                {
                    emptyQty = getGasQty - getCylinderQty;
                }
                else
                {
                    emptyQty = getCylinderQty - getGasQty;
                }


                return emptyQty.ToString();
            }
            catch (Exception)
            {
                return "0";
            }

        }

        public string getPackageDataListAddToCart(string PackId)
        {
            var packageModel = new PackageModel();
            var dtPackage = packageModel.getPackageDataListModelByPackId(PackId);
            return commonFunction.serializeDatatableToJson(dtPackage);
        }
    }
}