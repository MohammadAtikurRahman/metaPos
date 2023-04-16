using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.InventoryBundle.Entities;


namespace MetaPOS.Admin.Model
{


    public class StockStatusModel : Stock
    {
        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        // Global Variable
        private string query = "";
        public string serialNo { get; set; }
        public string payType { get; set; }
        public string payDescr { get; set; }
        public string invoiceType{ get; set; }


        // Insert Data StockInfoStatus Table
        public dynamic createStockStatus()
        {
            query = "INSERT INTO StockStatusInfo (prodId, " +
                    "prodCode," +
                    "prodName," +
                    "prodDescr," +
                    "supCompany," +
                    "catName," +
                    "qty," +
                    "serialNo,"+
                    "bPrice," +
                    "payType," +
                    "payDescr," +
                    "invoiceType,"+
                    "sPrice," +
                    "weight," +
                    "size," +
                    "discount," +
                    "stockTotal," +
                    "status," +
                    "entryDate," +
                    "statusDate," +
                    "entryQty," +
                    "title," +
                    "roleId," +
                    "billNo," +
                    "branchId," +
                    "groupId," +
                    "fieldAttribute," +
                    "tax," +
                    "sku," +
                    "lastQty," +
                    "productSource," +
                    "prodCodes," +
                    "imei," +
                    "fieldId," +
                    "attributeId," +
                    "commission," +
                    "dealerprice," +
                    "createdFor," +
                    "engineNumber," +
                    "cecishNumber, " +
                    "transceiverId," +
                    "storeId," +
                    "balanceQty) " +
                    " VALUES ('" +
                    prodId + "',N'" +
                    prodCode + "',N'" +
                    prodName + "',N'" +
                    prodDescr + "',N'" +
                    supCompany + "',N'" +
                    catName + "','" +
                    qty + "','" + 
                    serialNo + "', '" + 
                    bPrice + "','" + 
                    payType + "','" + 
                    payDescr + "','" +
                    invoiceType + "','" +
                    sPrice + "','" +
                    weight + "','" +
                    size + "','" +
                    discount + "','" +
                    stockTotal + "','" +
                    status + "','" +
                    commonFunction.GetCurrentTime().ToString("MM-dd-yyyy") + "','" +//
                    commonFunction.GetCurrentTime().ToString("MM-dd-yyyy") + "','" +
                    entryQty + "',N'" +
                    title + "','" +
                    HttpContext.Current.Session["roleId"] + "','" +
                    billNo + "','" +
                    HttpContext.Current.Session["branchId"] + "','" +
                    HttpContext.Current.Session["groupId"] + "','" +
                    fieldAttribute + "','" +
                    tax + "','" +
                    sku + "','" +
                    lastQty + "','" +
                    productSource + "','" +
                    prodCodes + "','" +
                    imei + "','" +
                    fieldId + "','" +
                    attributeId + "','" +
                    supCommission + "','" +
                    dealerPrice + "','" +
                    createdFor + "','" +
                    engineNumber + "','" +
                    cecishNumber + "'," +
                    HttpContext.Current.Session["roleId"] + ", '" +
                    storeId + "','" +
                    balanceQty + "')";
            return sqlOperation.executeQuery(query);
        }




        //string billNoSaleReturn, int prodIdSaleReturn, string qtySaleReturn, string statusSaleReturn, string searchType
        public string saveStockStatusInfoListForSaleReturn()
        {
            query = "INSERT INTO StockStatusInfo (prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,storeId,searchType,balanceQty)" +
               " SELECT top 1  prodId, prodCode,prodName,prodDescr,supCompany,catName,'" + qty + "',bPrice,sPrice,weight,size,discount,stockTotal,'" + status + "',entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,'" + commonFunction.getLastStockQty(prodId.ToString(), HttpContext.Current.Session["storeId"].ToString()) + "',productSource,prodCodes,'" + returnImei + "',fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,'" + HttpContext.Current.Session["storeId"] + "',searchType,'" + balanceQty + "' from StockStatusInfo where billNo='" + billNo + "' AND prodId ='" + prodId + "'";
            return query;
        }


        public string saveStockStatusInfoListModel()
        {
            query = "INSERT INTO StockStatusInfo (" +
                        "prodId, " +
                        "prodCode," +
                        "prodName," +
                        "prodDescr," +
                        "supCompany," +
                        "catName," +
                        "qty," +
                        "serialNo," +
                        "bPrice," +
                        "payType," +
                        "payDescr," +
                        "invoiceType,"+
                        "sPrice," +
                        "weight," +
                        "size," +
                        "discount," +
                        "stockTotal," +
                        "status," +
                        "entryDate," +
                        "statusDate," +
                        "entryQty," +
                        "title," +
                        "roleId," +
                        "billNo," +
                        "branchId," +
                        "groupId," +
                        "fieldAttribute," +
                        "tax," +
                        "sku," +
                        "lastQty," +
                        "productSource," +
                        "prodCodes," +
                        "imei," +
                        "fieldId," +
                        "attributeId," +
                        "commission," +
                        "dealerprice," +
                        "createdFor," +
                        "unitId, " +
                        "isPackage," +
                        "engineNumber," +
                        "cecishNumber, " +
                        "searchType," +
                        "storeId," +
                        "offer," +
                        "offerType," +
                        "isOfferQty," +
                        "fieldRecord," +
                        "attributeRecord," +
                        "balanceQty," +
                        "salesPersonId," +
                        "referredBy) " +
                        " VALUES ('" +
                        prodId + "',N'" +
                        prodCode + "',N'" +
                        prodName + "',N'" +
                        prodDescr + "',N'" +
                        supCompany + "',N'" +
                        catName + "','" +
                        qty + "','" +
                        serialNo + "','" + 
                        bPrice + "','" + 
                        payType + "','" + 
                        payDescr + "','" +
                        invoiceType + "','" +
                        sPrice + "','" +
                        weight + "','" +
                        size + "','" +
                        discount + "','" +
                        stockTotal + "','" +
                        status + "','" +
                        entryDate + "','" +
                        entryDate + "','" +
                        entryQty + "',N'" +
                        title + "','" +
                        HttpContext.Current.Session["roleId"] + "','" +
                        billNo + "','" +
                        HttpContext.Current.Session["branchId"] + "','" +
                        HttpContext.Current.Session["groupId"] + "','" +
                        fieldAttribute + "','" +
                        tax + "','" +
                        sku + "','" +
                        lastQty + "','" +
                        productSource + "','" +
                        prodCodes + "','" +
                        imei + "','" +
                        fieldId + "','" +
                        attributeId + "','" +
                        supCommission + "','" +
                        dealerPrice + "','" +
                        createdFor + "','" +
                        unitId + "','" +
                        isPackage + "','" +
                        engineNumber + "','" +
                        cecishNumber + "','" +
                        searchType + "','" +
                        storeId + "','" +
                        offer + "','" +
                        offerType + "','" +
                        isOfferQty + "','" +
                        fieldRecord + "','" +
                        attributeRecord + "','" +
                        balanceQty + "','"+
                        salesPersonId + "','" +
                        referredBy+"') ";
            return query;
        }





        public bool updateStockStatusInfoListModel()
        {
            try
            {
                query = "UPDATE StockStatusInfo SET prodName='" +
                        prodName + "',prodDescr='" +
                        prodDescr + "',supCompany='" +
                        supCompany + "',catName='" +
                        catName + "',qty='" +
                        qty + "',bPrice='" +
                        bPrice + "',sPrice='" +
                        sPrice + "',weight='" +
                        weight + "',size='" +
                        size + "',discount='" +
                        discount + "',stockTotal='" +
                        stockTotal + "',status='Resold' " +
                        "WHERE prodId='" +
                        prodId + "' AND billNo='" +
                        billNo + "'";
                sqlOperation.executeQuery(query);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }





        public DataTable getStockStatusDataListModel(string billNo)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockStatusInfo WHERE BillNo ='" + billNo + "' AND (status='sale' OR status='salePackage') AND (isPackage='0' OR status='salePackage' ) AND isOfferQty='0' ");
        }




        public DataTable getStockStatusQtyOfferDataListModel(string prodId, string billNo)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockStatusInfo WHERE prodId ='" + prodId + "' AND BillNo ='" + billNo + "' AND status='sale' AND isOfferQty='1'");
        }




        public DataTable getStockStatusPointOfferDataListModel(string prodId, string billNo)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockStatusInfo WHERE prodId ='" + prodId + "' AND BillNo ='" + billNo + "' AND status='sale' AND isOfferQty='0'");
        }



        public DataTable getStockStatusDataListModelByBilNoAndProdCode(string billNo, string prodCode)
        {
            return sqlOperation.getDataTable("SELECT * FROM StockStatusInfo WHERE BillNo ='" + billNo + "' AND prodCode='" + prodCode + "'");

        }




        public void deleteStockStatusById(string Id)
        {
            sqlOperation.executeQuery("UPDATE StockStatusInfo SET status='stockReturn' WHERE Id='" + Id + "'");
        }




        public void changeStockSatusInfoModelByBillNo(string billNo)
        {
            sqlOperation.executeQuery("UPDATE StockStatusInfo SET status='stockReturn' WHERE billNo='" + billNo + "'");
        }




        public bool saveStockStatusInfoTransferModel(string Id, string transQty, string shiftTo)
        {

            query =
                "INSERT INTO StockStatusInfo (prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,transceiverId)" +
                "(SELECT prodId,prodCode,prodName,prodDescr,supCompany,catName,'" + transQty + "',bPrice,sPrice,weight,size,discount," + transQty + "*bPrice,'transfer','" +
                commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "','0',title,'" +
                HttpContext.Current.Session["roleId"] + "',''," + HttpContext.Current.Session["branchId"] + "," +
                HttpContext.Current.Session["groupId"] +
                ",fieldAttribute,tax,sku,'0','','',imei,'','',commission,dealerPrice,'" +
                HttpContext.Current.Session["branchId"] +
                "',unitId,'0',engineNumber,cecishNumber,'" + shiftTo + "' FROM StockInfo where Id='" + Id + "')";

            return sqlOperation.fireQuery(query);
        }




        public bool saveStockStatusInfoTransReceivedModel(string Id, string transQty, string shiftTo)
        {
            query =
                "INSERT INTO StockStatusInfo (prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,transceiverId)" +
                "(SELECT prodId,prodCode,prodName,prodDescr,supCompany,catName,'" + transQty +
                "',bPrice,sPrice,weight,size,discount," + transQty + "*bPrice,'receive','" +
                commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "','0',title,'" +
                shiftTo + "',''," + HttpContext.Current.Session["branchId"] + "," +
                HttpContext.Current.Session["groupId"] +
                ",fieldAttribute,tax,sku,'0','','',imei,'','',commission,dealerPrice,'" +
                shiftTo +
                "',unitId,'0',engineNumber,cecishNumber," + HttpContext.Current.Session["roleId"] + " FROM StockInfo where Id='" + Id + "' AND roleId='" +
                shiftTo + "')";
            return sqlOperation.fireQuery(query);
        }




        public bool saveStockStatusInfoByDirectCustomerModel(string billNo, string cusId, string preRoleId, string pay)
        {
            query =
                "INSERT INTO StockStatusInfo (prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,storeId,balanceQty)" +
                " SELECT  prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,'Resold',entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,'" + HttpContext.Current.Session["storeId"] + "','" + balanceQty + "' from StockStatusInfo where billNo='" + billNo +
            "'";
            return sqlOperation.fireQuery(query);
        }



        public string saveStockStatusInfoListForSaleReturn(string billNoSaleReturn, int prodIdSaleReturn, string qtySaleReturn, string statusSaleReturn, string searchType)
        {
            query = "INSERT INTO StockStatusInfo (prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,storeId,searchType,balanceQty)" +
               " SELECT top 1  prodId, prodCode,prodName,prodDescr,supCompany,catName,'" + qtySaleReturn + "',bPrice,sPrice,weight,size,discount,stockTotal,'" + statusSaleReturn + "',entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,'" + commonFunction.getLastStockQty(prodIdSaleReturn.ToString(), HttpContext.Current.Session["storeId"].ToString()) + "',productSource,prodCodes,'" + returnImei + "',fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,'" + HttpContext.Current.Session["storeId"] + "',searchType,'" + balanceQty + "' from StockStatusInfo where billNo='" + billNoSaleReturn + "' AND prodId ='" + prodIdSaleReturn + "'";
            return sqlOperation.executeQuery(query);
        }





        public string saveStockStatusInfoListForSaleReturnQuery(string billNoSaleReturn, int prodIdSaleReturn, string qtySaleReturn, string statusSaleReturn, string searchType)
        {
            query = "INSERT INTO StockStatusInfo (prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,storeId,searchType,balanceQty)" +
               " SELECT top 1  prodId, prodCode,prodName,prodDescr,supCompany,catName,'" + qtySaleReturn + "',bPrice,sPrice,weight,size,discount,stockTotal,'" + statusSaleReturn + "',entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,'" + commonFunction.getLastStockQty(prodIdSaleReturn.ToString(), HttpContext.Current.Session["storeId"].ToString()) + "',productSource,prodCodes,'" + returnImei + "',fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,'" + HttpContext.Current.Session["storeId"] + "',searchType,'" + balanceQty + "' from StockStatusInfo where billNo='" + billNoSaleReturn + "' AND prodId ='" + prodIdSaleReturn + "' ";
            return query;
        }




        public DataTable getStockStatusSaleReturnSumOfQtyModel(string billNoSaleReturn, int prodIdSaleReturn)
        {
            return sqlOperation.getDataTable("SELECT SUM(CAST(qty as decimal)) qtySaleReturn FROM StockStatusInfo WHERE BillNo ='" + billNoSaleReturn + "' AND prodId='" + prodIdSaleReturn + "' AND status='saleReturn'");
        }




        public bool upsertStockstatusTransferModel(string TransId, string TransProdId, string transQty, string status)
        {
            query = "INSERT INTO StockStatusInfo (prodId, prodCode,prodName,prodDescr,supCompany,catName,qty,bPrice,sPrice,weight,size,discount,stockTotal,status,entryDate,statusDate,entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,lastQty,productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,storeId,deliveryStatus,balanceQty)" +
               " SELECT top 1  prodId, prodCode,prodName,prodDescr,supCompany,catName,'" + transQty + "',bPrice,sPrice,weight,size,discount,stockTotal,'" + status + "','" + commonFunction.GetCurrentTime() + "','" + commonFunction.GetCurrentTime() + "',entryQty,title,roleId,billNo,branchId,groupId,fieldAttribute,tax,sku,'" + lastQty + "',productSource,prodCodes,imei,fieldId,attributeId,commission,dealerPrice,createdFor,unitId, isPackage,engineNumber,cecishNumber,'" + TransId + "','0','" + balanceQty + "' from StockStatusInfo where prodId ='" + TransProdId + "'";
            return sqlOperation.fireQuery(query);
        }




        public DataTable getSalesProfitModel(string storeAccessParameters, string from, string to)
        {
            query = "SELECT DISTINCT BillNo, prodId,SUM((sPrice-bPrice)* CAST(qty as decimal)) as balance,min(commission) as commission, min(bPrice) as bPrice FROM StockStatusInfo where ((status='sale' AND isPackage='false') OR (status='salePackage' AND isPackage='true')) AND (entryDate >='" + from + "' AND entryDate <='" + to + "') AND BillNo !='' " + storeAccessParameters + " GROUP BY BillNo,prodId";
            return sqlOperation.getDataTable(query);
        }




        public DataTable getSalesReturnModel(string storeAccessParameters, string from, string to)
        {
            //(sPrice-bPrice)*CAST(qty as decimal)
            query = "SELECT DISTINCT BillNo,SUM((sPrice-bPrice)*CAST(qty as decimal)) as balance FROM StockStatusInfo where ((status='saleReturn' AND isPackage='false' AND searchType='product') OR (status='saleReturn' AND isPackage='true' AND searchType='salePackage') OR (status='saleReturn' AND isPackage='false' AND searchType='service')) AND(entryDate >='" + from + "' AND entryDate <='" + to + "')  AND BillNo !='' " + storeAccessParameters + " GROUP BY BillNo";
            //query = "SELECT SUM(cashin) as balance FROM CashReportInfo WHERE status !='6' AND(entryDate >='" + from + "' AND entryDate <='" + to + "') " + storeAccessParameters + "";
            return sqlOperation.getDataTable(query);
        }




        public DataTable getTotalExpensiveExpensceModel(string storeAccessParameters, string form, string to)
        {
            query = "SELECT SUM(cashOut) as cashOut FROM CashReportInfo WHERE status='2' AND entryDate >= '" + form + "' AND entryDate <= '" + to + "' " + storeAccessParameters + " ";
            return sqlOperation.getDataTable(query);
        }




        public DataTable getTotalSupplierRecivedAmtModel(string storeAccessParameters, string form, string to)
        {
            query = "SELECT SUM(cashIn) as cashIn FROM CashReportInfo WHERE status='3' AND entryDate >= '" + form + "' AND entryDate <= '" + to + "' " + storeAccessParameters + " ";
            return sqlOperation.getDataTable(query);
        }




        public DataTable getTotalSalaryModel(string storeAccessParameters, string form, string to)
        {
            query = "SELECT SUM(cashOut) as cashOut FROM CashReportInfo WHERE status='1' AND entryDate >= '" + form + "' AND entryDate <= '" + to + "' " + storeAccessParameters + " ";
            return sqlOperation.getDataTable(query);
        }




        public DataTable getSalesDiscountModel(string storeAccessParameters, string from, string to)
        {
            query = "SELECT distinct billNo, discAmt FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS DATE) >='" + from + "' AND CAST(entryDate AS DATE) <='" + to + "') " + storeAccessParameters + " ";
            return sqlOperation.getDataTable(query);
        }




        public string changeSuspendStatus(string billNo)
        {
            string query = "UPDATE Stockstatusinfo SET isSuspend='1' where billNo='" + billNo + "' ";
            return query;
        }




        public DataTable getProductIDByIMEI(string IMEI)
        {
            string query = "SELECT * FROM Stockstatusinfo WHERE IMEI LIKE '%" + IMEI + "%' AND storeId='" + HttpContext.Current.Session["storeId"] + "'";
            return sqlOperation.getDataTable(query);
        }


        

        public DataTable CheckTransferProducQtyManagementtModel(string transToStore, string productId)
        {
            string query = "SELECT * FROM QtyManagement where storeId='" + transToStore + "' AND productId='" + productId + "'";
            return sqlOperation.getDataTable(query);
        }





        public bool createTransferStockQtyManagement(string storeId, string prodId, string qty)
        {
            string query = "INSERT INTO QtyManagement (productId,stockQty,storeId,roleId,branchId,groupId,entryDate,updateDate) VALUES ('" + prodId + "','" + qty + "','" + storeId + "','" + HttpContext.Current.Session["roleId"] + "','" + HttpContext.Current.Session["branchId"] + "','" + HttpContext.Current.Session["groupId"] + "','" + DateTime.Now + "','" + DateTime.Now + "')";//entryDate       updateDate
            return sqlOperation.fireQuery(query);
        }




        public bool updateTransferStockQtyQtyManagement(string storeId, string prodId, string qty)
        {
            string query = "UPDATE QtyManagement SET stockQty='" + qty + "' where productId='" + prodId + "' AND storeId='" + storeId + "'";
            return sqlOperation.fireQuery(query);
        }

        
    }


}