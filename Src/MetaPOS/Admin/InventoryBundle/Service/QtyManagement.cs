using MetaPOS.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.InventoryBundle.Service
{
    public class QtyManagement
    {
        public string ProductId { get; set; }
        public string StoreId { get; set; }
        public string operationalQty { get; set; }
        public string mainOperationalQty { get; set; }

        public string subtractStockQty()
        {
            string dbStockedQty = getStockQty(ProductId, StoreId);
            int unitRatio = getProductRatio(ProductId);

            // stock Qty and Pieces split
            bool containsStockQty = dbStockedQty.Contains(".");
            int dbStockQty = 0, dbStockPiece = 0;
            if (containsStockQty)
            {
                string[] splitDbQty = dbStockedQty.Split('.');
                if (splitDbQty.Length > 1)
                {
                    dbStockQty = Convert.ToInt32(splitDbQty[0]);
                    dbStockPiece = Convert.ToInt32(splitDbQty[1]);
                }
            }
            else
            {
                dbStockQty = Convert.ToInt32(dbStockedQty);
            }

            bool containsOperationalQty = operationalQty.Contains(".");
            int optQty = 0, optPiece = 0;

            if (containsOperationalQty)
            {
                string[] splitOptQty = operationalQty.Split('.');
                if (splitOptQty.Length > 1)
                {
                    optQty = Convert.ToInt32(splitOptQty[0]);
                    optPiece = Convert.ToInt32(splitOptQty[1]);
                }
            }
            else
            {
                optQty = Convert.ToInt32(operationalQty);
            }

            if (optPiece > dbStockPiece)
            {
                dbStockQty -= 1;
                dbStockPiece += unitRatio;
            }

            var currentQty = dbStockQty - optQty;
            var currentPiece = dbStockPiece - optPiece;

            return currentQty + "." + currentPiece;
        }




        public string addStockQty()
        {
            string dbStockedQty = getStockQty(ProductId, StoreId);
            int unitRatio = getProductRatio(ProductId);

            // stock Qty and Pieces split
            bool containsStockQty = dbStockedQty.Contains(".");
            int dbStockQty = 0, dbStockPiece = 0;
            if (containsStockQty)
            {
                string[] splitDbQty = dbStockedQty.Split('.');
                if (splitDbQty.Length > 1)
                {
                    dbStockQty = Convert.ToInt32(splitDbQty[0]);
                    dbStockPiece = Convert.ToInt32(splitDbQty[1]);
                }
            }
            else
            {
                dbStockQty = Convert.ToInt32(dbStockedQty);
            }

            bool containsOperationalQty = operationalQty.Contains(".");
            int optQty = 0, optPiece = 0;

            if (containsOperationalQty)
            {
                string[] splitOptQty = operationalQty.Split('.');
                if (splitOptQty.Length > 1)
                {
                    optQty = Convert.ToInt32(splitOptQty[0]);
                    optPiece = Convert.ToInt32(splitOptQty[1]);
                }
            }
            else
            {
                optQty = Convert.ToInt32(operationalQty);
            }

            var currentQty = dbStockQty + optQty;
            var currentPiece = dbStockPiece + optPiece;

            if (currentPiece >= unitRatio)
            {
                currentQty += 1;
                currentPiece -= unitRatio;
            }

            return currentQty + "." + currentPiece;
        }



        public string substractQtyFromQty()
        {
            string dbStockedQty = mainOperationalQty;
            int unitRatio = getProductRatio(ProductId);

            // stock Qty and Pieces split
            bool containsStockQty = dbStockedQty.Contains(".");
            int dbStockQty = 0, dbStockPiece = 0;
            if (containsStockQty)
            {
                string[] splitDbQty = dbStockedQty.Split('.');
                if (splitDbQty.Length > 1)
                {
                    dbStockQty = Convert.ToInt32(splitDbQty[0]);
                    dbStockPiece = Convert.ToInt32(splitDbQty[1]);
                }
            }
            else
            {
                dbStockQty = Convert.ToInt32(dbStockedQty);
            }

            bool containsOperationalQty = operationalQty.Contains(".");
            int optQty = 0, optPiece = 0;

            if (containsOperationalQty)
            {
                string[] splitOptQty = operationalQty.Split('.');
                if (splitOptQty.Length > 1)
                {
                    optQty = Convert.ToInt32(splitOptQty[0]);
                    optPiece = Convert.ToInt32(splitOptQty[1]);
                }
            }
            else
            {
                optQty = Convert.ToInt32(operationalQty);
            }

            if (optPiece >= dbStockPiece)
            {
                dbStockQty -= 1;
                dbStockPiece += unitRatio;
            }

            var currentQty = dbStockQty - optQty;
            var currentPiece = dbStockPiece - optPiece;

            return currentQty + "." + currentPiece;
        }



        public string getStockQty(string productId, string storeId)
        {
            var stockModel = new StockModel();
            stockModel.prodId = Convert.ToInt32(productId);
            stockModel.storeId = Convert.ToInt32(storeId);
            var dtStockQtyManagement = stockModel.getStockQtyManagement();
            if (dtStockQtyManagement.Rows.Count > 0)
                return dtStockQtyManagement.Rows[0]["stockQty"].ToString();

            return "0.0";
        }




        public int getProductRatio(string productId)
        {
            int dbRatio = 1;
            var stockModel = new StockModel();
            var dtStock = stockModel.getProductRatioModelByProductId(productId);
            if (dtStock.Rows.Count > 0)
                dbRatio = Convert.ToInt32(dtStock.Rows[0]["unitRatio"]);
            return dbRatio;
        }




        public bool hasProductStockQty(string productId, string storeId)
        {
            var qtyManagementModel = new StockModel();
            qtyManagementModel.prodId = Convert.ToInt32(productId);
            qtyManagementModel.storeId = Convert.ToInt32(storeId);
            var dtStockQtyManagent = qtyManagementModel.getStockQtyManagement();
            if (dtStockQtyManagent.Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}