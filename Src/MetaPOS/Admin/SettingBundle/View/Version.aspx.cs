using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MetaPOS.Admin.SaleBundle.Service;


namespace MetaPOS.Admin.SettingBundle.View
{


    public partial class Version : System.Web.UI.Page
    {


        private Admin.DataAccess.SqlOperation objSql = new Admin.DataAccess.SqlOperation();
        private Admin.DataAccess.CommonFunction objCommonFun = new Admin.DataAccess.CommonFunction();
        private DataSet ds;


        string query = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!objCommonFun.accessChecker("Version"))
            {
                DataAccess.CommonFunction obj = new DataAccess.CommonFunction();
                obj.pageout();
            }
            checkVersion();
        }





        private void checkVersion()
        {
            ds =
                objSql.getDataSet("SELECT version,entryDate FROM RoleInfo WHERE (branchId = '" + Session["branchId"] +
                                  "' OR groupId = '" + Session["groupId"] + "' )");
            lblVersion.Text = ds.Tables[0].Rows[0][0].ToString();
            lblVersionDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0][1]).ToString("dd-MMM-yyyy");
        }


        private string versionUpdate()
        {
            Session["stockProblmemId"] += "// Start ";

            int threadCounter = 0;

            var dtSaleBilNo = objSql.getDataTable("select billNo from SaleInfo where prodId !=''  or billNo !=''");
            Session["stockProblmemId"] += "// Rows:" + dtSaleBilNo.Rows.Count + "//";
            for (int i = 0; i < dtSaleBilNo.Rows.Count; i++)
            {
                var dtSaleProductId = objSql.getDataTable("SELECT * FROM SaleInfo WHERE BillNo = '" + dtSaleBilNo.Rows[i]["billNo"] + "'");

                for (int j = 0; j < dtSaleProductId.Rows.Count; j++)
                {

                    query = "BEGIN TRANSACTION "
                       + "UPDATE StockStatusInfo SET qty = (select SUM(CAST(qty as decimal)) as qty FROM SaleInfo where prodID='" + dtSaleProductId.Rows[j]["prodId"] + "' and billNo='" + dtSaleBilNo.Rows[i]["billNo"] + "') where prodID='" + dtSaleProductId.Rows[j]["prodId"] + "' and billNo='" + dtSaleBilNo.Rows[i]["billNo"] + "' and status='sale' "
                   + "COMMIT";
                    objSql.fireQuery(query);

                }

                threadCounter++;
                Session["stockProblmemId"] += threadCounter + ",";


            }

            Session["stockProblmemId"] += "// Finshed sale to stockstatus";

            var saleVersion = new SaleVersion();

            decimal adjustQty = 0;
            string problemId = "";


            var dtStockProduct = objSql.getDataTable("SELECT distinct prodID,qty FROM StockInfo where prodID != '' or qty !=''");
            Session["stockProblmemId"] += "// Start Stock Match: " + dtStockProduct.Rows.Count + "||";
            for (int i = 0; i < dtStockProduct.Rows.Count; i++)
            {
                var prodId = dtStockProduct.Rows[i]["prodID"].ToString();
                decimal totalQtyDb = saleVersion.StockStatusTotalQty(prodId);

                decimal prodStock = Convert.ToDecimal(dtStockProduct.Rows[i]["qty"].ToString());

                adjustQty = prodStock - totalQtyDb;

                if (adjustQty != 0 && prodStock > 0 && prodStock != totalQtyDb)
                {
                    query = "BEGIN TRANSACTION "
                            + "INSERT StockStatusInfo (prodID,prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleID,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId,isPackage,engineNumber,cecishNumber,transceiverId,searchType,purchaseCode) "
                            + "(SELECT top 1 sale.prodID, stock.prodCode,stock.prodName,stock.prodDescr,stock.supCompany,stock.catName,'" + adjustQty + "',stock.bPrice,stock.sPrice,stock.weight,stock.size,stock.discount,stock.stockTotal,'stock',getdate(),getdate(),stock.entryQty,stock.title,sale.roleID,sale.billNo,sale.branchId,sale.groupId,stock.fieldAttribute,stock.tax,stock.sku,stock.lastQty,'',stock.prodCode,'','','',stock.commission,stock.dealerPrice,stock.createdFor,stock.unitId,'false',stock.engineNumber,stock.cecishNumber,'',sale.searchType,stock.purchaseDate  FROM SaleInfo sale LEFT JOIN StockInfo stock ON sale.prodID = stock.prodID where sale.prodId='" + prodId + "') "
                       + "COMMIT";
                    objSql.executeQuery(query);

                    Session["stockProblmemId"] += i + ",";
                }
            }

            Session["stockProblmemId"] += "//Finished Stock Match";
            return problemId;
        }






        [WebMethod]
        public static string updateSystemVersion()
        {
            SaleVersion saleVersion = new SaleVersion();
            return saleVersion.versionUpdagrading();

        }







    }


}