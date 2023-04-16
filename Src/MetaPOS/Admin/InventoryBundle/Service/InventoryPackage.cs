using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.Model;
using System.Data;
using MetaPOS.Admin.SaleBundle.Service;


namespace MetaPOS.Admin.InventoryBundle.Service
{
    public class InventoryPackage
    {

        public float getPackageSoldQty(string prodID)
        {
            var packageModel = new PackageModel();
            var saleModel = new SaleModel();
            var saelStock = new SaleStock();

            DataTable dtPackageList = packageModel.getPackageDataListModel();

            float soldQty = 0;
            for (int i = 0; i < dtPackageList.Rows.Count; i++)
            {
                string prodCodes = dtPackageList.Rows[i]["prodCode"].ToString();
                string[] splitProdCode = prodCodes.Split(';');

                for (int j = 0; j < splitProdCode.Length; j++)
                {
                    if (splitProdCode[j] == "")
                        break;

                    if (prodID == splitProdCode[j])
                    {
                        string prodId = dtPackageList.Rows[i]["Id"].ToString();
                        DataTable dtSale = saleModel.getSaleInfoDataListModelByProdId(prodId);

                        if (dtSale.Rows.Count > 0 && dtSale.Rows[0]["qty"].ToString() !="")
                        {
                            soldQty += float.Parse(dtSale.Rows[0]["qty"].ToString());
                        }
                    }
                }
            }

            return soldQty;
        }
    }
}