using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.SaleBundle.Service
{
    public class SaleVersion
    {

        private SqlOperation objSql = new SqlOperation();
        public string versionUpdagrading()
        {
            string query = "";
            HttpContext.Current.Session["stockProblmemId"] += "// Start ";

            int threadCounter = 0;

            var dtSaleBilNo = objSql.getDataTable("select billNo from SaleInfo where prodId !=''  or billNo !=''");
            HttpContext.Current.Session["stockProblmemId"] += "// Rows:" + dtSaleBilNo.Rows.Count + "//";
            for (int i = 0; i < dtSaleBilNo.Rows.Count; i++)
            {
                var dtSaleProductId = objSql.getDataTable("SELECT * FROM SaleInfo WHERE BillNo = '" + dtSaleBilNo.Rows[i]["billNo"] + "'");

                for (int j = 0; j < dtSaleProductId.Rows.Count; j++)
                {

                    query = "BEGIN TRANSACTION "
                       + "UPDATE StockStatusInfo SET qty = (select SUM(CAST(qty as decimal)) as qty FROM SaleInfo where prodID='" + dtSaleProductId.Rows[j]["prodId"] + "' and billNo='" + dtSaleBilNo.Rows[i]["billNo"] + "') where prodID='" + dtSaleProductId.Rows[j]["prodId"] + "' and billNo='" + dtSaleBilNo.Rows[i]["billNo"] + "' and status='sale' "
                   + "COMMIT";
                    objSql.fireQuery(query);

                    threadCounter++;
                   HttpContext.Current.Session["stockProblmemId"] += threadCounter + ",";
                }


            }

           HttpContext.Current.Session["stockProblmemId"] += "// Finshed sale to stockstatus";

            decimal adjustQty = 0;
            string problemId = "";

            var dtStockProduct = objSql.getDataTable("SELECT distinct prodID,qty FROM StockInfo where prodID != '' or qty !=''");
            for (int i = 0; i < dtStockProduct.Rows.Count; i++)
            {
                var prodId = dtStockProduct.Rows[i]["prodID"].ToString();
                decimal totalQtyDb = StockStatusTotalQty(prodId);
                if (prodId == "594")
                    HttpContext.Current.Session["stockProblmemId"] += "594:" + totalQtyDb;

                decimal prodStock = Convert.ToDecimal(dtStockProduct.Rows[i]["qty"].ToString());
                
                adjustQty = prodStock - totalQtyDb;

                if (adjustQty != 0 && prodStock > 0)
                {
                    query = "BEGIN TRANSACTION "
                            + "INSERT StockStatusInfo (prodID,prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleID,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId,isPackage,engineNumber,cecishNumber,transceiverId,searchType,purchaseCode) "
                            + "(SELECT top 1 sale.prodID, stock.prodCode,stock.prodName,stock.prodDescr,stock.supCompany,stock.catName,'" + adjustQty + "',stock.bPrice,stock.sPrice,stock.weight,stock.size,stock.discount,stock.stockTotal,'stock',getdate(),getdate(),stock.entryQty,stock.title,sale.roleID,sale.billNo,sale.branchId,sale.groupId,stock.fieldAttribute,stock.tax,stock.sku,stock.lastQty,'',stock.prodCode,'','','',stock.commission,stock.dealerPrice,stock.createdFor,stock.unitId,'false',stock.engineNumber,stock.cecishNumber,'',sale.searchType,stock.purchaseDate  FROM SaleInfo sale LEFT JOIN StockInfo stock ON sale.prodID = stock.prodID where sale.prodId='" + prodId + "') "
                       + "COMMIT";
                    objSql.executeQuery(query);
                    //HttpContext.Current.Session["stockProblmemId"] += "func 2:" + i;

                    totalQtyDb = StockStatusTotalQty(prodId);
                    if (prodStock != totalQtyDb)
                        problemId += prodId;
                }
            }

            HttpContext.Current.Session["stockProblmemId"] += "//Finished: problem ID:" + problemId;
            return problemId;
        }


        public decimal StockStatusTotalQty(string prodId)
        {
            decimal stock = 0,
                stockReturn = 0,
                sale = 0,
                saleReturn = 0
                ;

            var dtStock = objSql.getDataTable("SELECT SUM(CAST(qty as decimal)) qty FROM StockStatusInfo where status='stock' AND prodID='" + prodId + "'");
            if (dtStock.Rows.Count > 0 && dtStock.Rows[0]["qty"].ToString() != "")
                stock = Convert.ToDecimal(dtStock.Rows[0]["qty"].ToString());

            var dtStockReturn = objSql.getDataTable("SELECT SUM(CAST(qty as decimal)) qty FROM StockStatusInfo where status='stockReturn' AND prodID='" + prodId + "'");
            if (dtStockReturn.Rows.Count > 0 && dtStockReturn.Rows[0]["qty"].ToString() != "")
                stockReturn = Convert.ToDecimal(dtStockReturn.Rows[0]["qty"].ToString());

            var dtSale = objSql.getDataTable("SELECT SUM(CAST(qty as decimal)) qty FROM StockStatusInfo where status='sale' AND prodID='" + prodId + "'");
            if (dtSale.Rows.Count > 0 && dtSale.Rows[0]["qty"].ToString() != "")
                sale = Convert.ToDecimal(dtSale.Rows[0]["qty"].ToString());

            var dtSaleReturn = objSql.getDataTable("SELECT SUM(CAST(qty as decimal)) qty FROM StockStatusInfo where status='saleReturn' AND prodID='" + prodId + "'");
            if (dtSaleReturn.Rows.Count > 0 && dtSaleReturn.Rows[0]["qty"].ToString() != "")
                saleReturn = Convert.ToDecimal(dtSaleReturn.Rows[0]["qty"].ToString());

            return (stock + stockReturn + saleReturn) - sale;
        }

    }
}