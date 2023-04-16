using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace MetaPOS.Admin.InventoryBundle.Service
{


    public class Return
    {
        private ReturnModel returnModel = new ReturnModel();
        private CommonFunction commonFunction = new CommonFunction();


        public string productCode { get; set; }
        public string storeId { get; set; }
        public decimal buyPrice { get; set; }



        public Dictionary<string, string> getReturnOrDamageItemData()
        {
            
            var dicReturnItem = new Dictionary<string, string>();

            var dt = returnModel.getItemDataModel(productCode, storeId);
            if (dt.Rows.Count > 0)
            {
                string qty = "0", piece = "0";
                var qtyString = dt.Rows[0]["stockQty"].ToString();
                if (qtyString.Contains('.'))
                {
                    qty = qtyString.Split('.')[0];
                    piece = qtyString.Split('.')[1];
                }
                else
                {
                    qty = dt.Rows[0]["stockQty"].ToString();
                    piece = "0";
                }

                dicReturnItem.Add("qty", qty);
                dicReturnItem.Add("piece", piece);
                dicReturnItem.Add("bPrice", dt.Rows[0]["bPrice"].ToString());
                dicReturnItem.Add("catName", dt.Rows[0]["catName"].ToString());
                dicReturnItem.Add("imei", dt.Rows[0]["imei"].ToString());
                dicReturnItem.Add("prodID", dt.Rows[0]["prodID"].ToString());
                dicReturnItem.Add("supCompany", dt.Rows[0]["supCompany"].ToString());
            }

            return dicReturnItem;
        }





        public string getItemPriceData(string prodCode, decimal bPrice, string qtywithPiece)
        {
            int ratio = returnModel.getItemRatioDataModel(prodCode);

            decimal qty = 0, piece = 0, totalPricePrice = 0;
            if (ratio != 0)
            {
                string[] splitQtyWithPiece = qtywithPiece.Split('.');
                foreach (var item in splitQtyWithPiece)
                {
                    qty = Convert.ToDecimal(splitQtyWithPiece[0]);
                    piece = Convert.ToDecimal(splitQtyWithPiece[1]);
                }
                totalPricePrice = (bPrice * piece) / ratio;
            }
            else
            {
                qty = Convert.ToDecimal(qtywithPiece);
            }

            decimal totalQtyPrice = bPrice * qty;
            decimal totalPrice = totalPricePrice + totalQtyPrice;

            return totalPrice.ToString("00");
        }


    }


}