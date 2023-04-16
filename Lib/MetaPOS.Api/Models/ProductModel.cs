using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MetaPOS.Admin.DataAccess;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;


namespace MetaPOS.Api.Models
{

    public class ProductModel : ProductsEntiyes
    {

        private CommonFunction commonFunction = new CommonFunction();
        SqlOperation sqlOperation = new SqlOperation();
        public string shopName { get; set; }
        public string productId { get; set; }
        public string prodName { get; set; }
        public int storeId { get; set; }
        public string sPrice { get; set; }

        


        public DataTable ProductDataTableModel()
        {
            sqlOperation.conString = shopName;
            string query = "SELECT StockInfo.prodID,StockInfo.prodName,StockInfo.sPrice, qtyManagement.stockQty FROM StockInfo INNER JOIN qtyManagement ON StockInfo.prodID=qtyManagement.productId";
            return sqlOperation.getDataTable(query);
        }






        public DataTable getStockDataListModelByProdID(string prodId)
        {
            sqlOperation.conString = shopName;
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM StockInfo WHERE prodId = '" + prodId + "'");
            return dt;
        }



        public string productSeleDataModel()
        {
            sqlOperation.conString = shopName;
            // string billNo = "AA00056";
            //string cusID = "000045";
            //string query = "INSERT INTO SaleInfo (prodID,billNo,cusID,comment) VALUES('" + productId + "','" + billNo + "','" + cusID + "','" + prodName + "')";
            //return sqlOperation.executeQueryScalar(query);




            string query = "INSERT INTO StockStatusInfo (" +
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
                        "invoiceType," +
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
                        productId + "',N'" +
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
                        roleId + "','" +// HttpContext.Current.Session["roleId"] + "','" +
                        billNo + "','" +
                        branchId + "','" +// HttpContext.Current.Session["branchId"] + "','" +
                        groupId + "','" +//HttpContext.Current.Session["groupId"] + "','" +
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
                        balanceQty + "','" +
                        salesPersonId + "','" +
                        referredBy + "') ";
            return sqlOperation.executeQueryWithoutRole(query);

        }





        public string saveSaleInfoData()
        {
            sqlOperation.conString = shopName;
            string query = "INSERT INTO SaleInfo (" +
                    "billNo," +
                    "roleId," +
                    "cusId," +
                    "prodId," +
                    "qty," +
                    "serialNo," +
                    "invoiceType," +
                    "netAmt," +
                    "discAmt," +
                    "vatAmt," +
                    "grossAmt," +
                    "payMethod," +
                    "payCash," +
                    "payCard," +
                    "giftAmt," +
                    "return_," +
                    "balance," +
                    "entryDate," +
                    "sPrice," +
                    "discType," +
                    "comment," +
                    "currentCash," +
                    "branchId," +
                    "groupId," +
                    "salesPersonId," +
                    "cardId," +
                    "bankId," +
                    "warranty," +
                    "token," +
                    "cusType," +
                    "maturityDate," +
                    "checkNo," +
                    "loadingCost," +
                    "shippingCost," +
                    "carryingCost," +
                    "unloadingCost," +
                    "salePersonType," +
                    "status," +
                    "additionalDue," +
                    "returnQty," +
                    "returnAmt," +
                    "previousDue," +
                    "interestRate," +
                    "interestAmt, " +
                    "searchType," +
                    "imei," +
                    "isAutoSalesPerson," +
                    "storeId," +
                    "serviceCharge," +
                    "extraDiscount," +
                    "refName," +
                    "refPhone," +
                    "refAddress," +
                    "vatType," +
                    "referredBy) " +
                    "VALUES ('" +
                    billNo + "', '" +
                    roleId + "', '" +
                    cusID + "', '" +
                    productId + "', '" +
                    qty + "','" +
                    serialNo + "', '" +
                    invoiceType + "', '" +
                    netAmt + "', '" +
                    discAmt + "', '" +
                    vatAmt + "', '" +
                    grossAmt + "', '" +
                    payMethod + "', '" +
                    payCash + "', '" +
                    payCard + "', '" +
                    giftAmt + "', '" +
                    return_ + "', '" +
                    balance + "', '" +
                    entryDate + "', '" +
                    sPrice + "',  '" +
                    discType + "', '" +
                    comment + "', '" +
                    currentCash + "', '" +
                    branchId + "', '" +
                    groupId + "', '" +
                    salesPersonId + "','" +
                    cardId + "','" +
                    bankId + "','" +
                    warranty + "', '" +
                    token + "',  '" +
                    CusType + "','" +
                    MaturityDate + "','" +
                    checkNo + "','" +
                    loadingCost + "','" +
                    shippingCost + "','" +
                    carryingCost + "','" +
                    unloadingCost + "','" +
                    salePersonType + "','1','" +
                    additionalDue + "','" +
                    returnQty + "','" +
                    returnAmt + "','" +
                    PreviousDue + "','" +
                    interestRate + "','" +
                    interestAmt + "','" +
                    searchType + "','" +
                    imei + "','" +
                    isAutoSalesPerson + "','" +
                    storeId + "','" +
                    serviceCharge + "','" +
                    extraDiscount + "','" +
                    refName + "','" +
                    refPhone + "','" +
                    refAddress + "','" +
                    vatType + "','" +
                    referredBy + "') ";

            //return query;
            return sqlOperation.executeQueryWithoutRole(query);
        }



        public string saveSlipDataModel()
        {

            string query = "INSERT INTO SlipInfo " +
                           "(billNo, roleId," +
                           "cusId," +
                           "prodId," +
                           "qty," +
                           "serialNo," +
                           "netAmt," +
                           "discAmt," +
                           "vatAmt," +
                           "grossAmt," +
                           "payMethod," +
                           "payCash," +
                           "payCard," +
                           "giftAmt," +
                           "return_," +
                           "balance," +
                           "entryDate," +
                           "status," +
                           "branchId," +
                           "groupId," +
                           "storeId," +
                           "salesPersonId," +
                           "cusType," +
                           "isAutoSalesPerson," +
                           "miscCost," +
                           "referredBy) " +
                           " VALUES ('" +
                           billNo + "', '" +
                           roleId + "', '" +
                           cusID + "','" +
                           productId + "','" +
                           serialNo + "', '" +
                           qty + "', '" +
                           netAmt + "', '" +
                           discAmt + "', '" +
                           vatAmt + "', '" +
                           grossAmt + "', '" +
                           payMethod + "', '" +
                           payCash + "', '" +
                           payCard + "', '" +
                           giftAmt + "', '" +
                           return_ + "', '" +
                           balance + "', '" +
                           entryDate + "', '" +
                           status + "', '" +
                           branchId + "', '" +
                           groupId + "','" +
                           storeId + "','" +
                           salesPersonId + "','" +
                           CusType + "','" +
                           isAutoSalesPerson + "','" +
                           miscCost + "','" +
                           referredBy + "') ";

            //return query;
            return sqlOperation.executeQueryWithoutRole(query);
        }



        //save customer data 
        public string saveCustomerInfoModel()
        {
            try
            {
                string query = "INSERT INTO CustomerInfo " +
                        "(cusId,name,phone,address,mailInfo,entryDate,updateDate,roleId,branchId,groupId,CusType,notes,accountNo,installmentStatus,designation,bloodGroup,sex,age ) VALUES ('" +
                    nextCusId + "', N'" +
                    name + "','" +
                    phone + "',N'" +
                    address + "','" +
                    mailInfo + "','" +
                    commonFunction.GetCurrentTime() + "','" +
                    commonFunction.GetCurrentTime() + "','" +
                    roleId + "','" +
                    branchId + "','" +
                    groupId + "','" +
                    CusType + "', N'" +
                    nextCusId + "',N'" +
                    nextCusId + "','False','" +
                    designation + "','" +
                    bloodGroup + "','" +
                    sex + "','" +
                     age + "')";

                return sqlOperation.executeQueryWithoutRole(query);
            }
            catch (Exception)
            {
                return "0";
            }
        }


        public string saveCustomerCashData()
        {
            string queryCustCash =
                "INSERT INTO CashReportInfo (cashType,descr,cashIn,cashOut,entryDate,billNo,status,roleId,branchId,groupId,storeId,payMethod,payType,payDescr) VALUES ('" +
                cashType + "','" + descr + "','" + cashIn + "','" + cashOut + "','" + entryDate + "','" + billNo + "','" +
                status + "','" + roleId + "','" + branchId +
                "','" + groupId + "','" + storeId + "','" + payMethod + "','" + payType + "','" + payDescr + "') ";
            //return queryCustCash;
            return sqlOperation.executeQueryWithoutRole(queryCustCash);
        }




        public string cashTransactionSales()
        {
            string query = "INSERT INTO [CashReportInfo] (cashType," +
                                                "descr," +
                                                "cashIn," +
                                                "cashOut," +
                                                "cashInHand," +
                                                "entryDate," +
                                                "billNo," +
                                                "mainDescr," +
                                                "roleID," +
                                                "branchId," +
                                                "groupId," +
                                                "status," +
                                                "adjust," +
                                                "isSchedulePayment," +
                                                "isScheduled," +
                                                "isReceived," +
                                                "trackAmt," +
                                                "storeId," +
                                                "payMethod," +
                                                "purchaseCode," +
                                                "cardType," +
                                                "MaturityDate," +
                                                "BankName," +
                                                "checkNo," +
                                                "payType," +
                                                "payDescr) VALUES ('" +
                                                cashType + "', '" +
                                                descr + "', '" +
                                                cashIn + "', '" +
                                                cashOut + "', '" +
                                                cashInHand + "', '" +
                                                entryDate + "', '" +
                                                billNo + "', '" +
                                                mainDescr + "', '" +
                                                roleId + "', '" +
                                                branchId + "', '" +
                                                groupId + "', '" +
                                                status + "', '" +
                                                adjust + "','" +
                                                isSchedulePayment + "','" +
                                                isScheduled + "','" +
                                                isReceived + "','" +
                                                trackAmt + "','" +
                                                storeId + "','" +
                                                payMethod + "','" +
                                                purchaseCode + "','" +
                                                cardType + "','" +
                                                maturityDate + "','" +
                                                bankName + "','" +
                                                checkNo + "','" +
                                                payType + "','" +
                                                payDescr + "')";

            //return query;
            return sqlOperation.executeQueryWithoutRole(query);
        }





        public string updateStockQtyWihoutExecute()
        {
            string query = "UPDATE QtyManagement SET stockQty='" + balanceQty + "' where productId='" + productId + "' AND storeId='" + storeId + "' ";
            //return query;
            return sqlOperation.executeQueryWithoutRole(query);
        }



        public DataTable getProductRatioModelByProductId(string prodId)
        {
            string query = "SELECT unit.unitRatio,unit.unitName, unit.Id, stock.ProdId, stock.qty  FROM UnitInfo as unit LEFT JOIN StockInfo as stock ON unit.Id = stock.unitId  WHERE stock.prodId = '" +
                prodId + "'";

            return sqlOperation.getDataTable(query);

        }



        public DataTable getSaleLsitModel(string billNo, string prodId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM SaleInfo WHERE billNo= '" + billNo + "' AND prodId='" + prodId +
                                          "'");
            return dt;
        }

    }
}
