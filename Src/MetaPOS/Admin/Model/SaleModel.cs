using System;
using System.Data;
using System.Web;
using MetaPOS.Admin.DataAccess;
using DataTable = System.Data.DataTable;


namespace MetaPOS.Admin.Model
{


    public class SaleModel
    {


        private SqlOperation sqlOperation = new SqlOperation();
        private CommonFunction commonFunction = new CommonFunction();

        // Initialize global info
       // private string tblName = "SaleInfo";

        // Initialize database parameter
        public string billNo { get; set; }
        public string roleId { get; set; }
        public string cusID { get; set; }
        public string prodID { get; set; }
        public string qty { get; set; }

        public decimal netAmt { get; set; }
        public decimal discAmt { get; set; }
        public decimal vatAmt { get; set; }
        public decimal grossAmt { get; set; }
        public string payMethod { get; set; }
        public decimal payCash { get; set; }
        public string payCard { get; set; }
        public decimal giftAmt { get; set; }
        public decimal return_ { get; set; }
        public decimal balance { get; set; }
        public decimal sPrice { get; set; }
        public string discType { get; set; }
        public string comment { get; set; }
        public decimal currentCash { get; set; }
        public int branchId { get; set; }
        public int groupId { get; set; }
        public int salesPersonId { get; set; }
        public int referredBy { get; set; }
        public int cardId { get; set; }
        public int bankId { get; set; }
        public string warranty { get; set; }
        public string token { get; set; }
        public int CusType { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string BankName { get; set; }
        public string checkNo { get; set; }
        //using sale page & Quotation page 
        public decimal loadingCost { get; set; }
        public decimal unloadingCost { get; set; }
        public decimal shippingCost { get; set; }
        public decimal carryingCost { get; set; }
        public string salePersonType { get; set; }
        public decimal additionalDue { get; set; }
        public string status { get; set; }
        public string returnQty { get; set; }
        public decimal returnAmt { get; set; }

        public decimal PreviousDue { get; set; }

        public int interestRate { get; set; }
        public decimal interestAmt { get; set; }

        public string searchType { get; set; }

        public string storeId { get; set; }

        public string imei { get; set; }

        public bool isAutoSalesPerson { get; set; }
        public decimal serviceCharge { get; set; }

        public decimal extraDiscount { get; set; }

        public string refName { get; set; }
        public string refPhone { get; set; }
        public string refAddress { get; set; }
        public string vatType { get; set; }
        public decimal miscCost { get; set; }


        public string storeAccessParameters { get; set; }
        public DateTime To { get; set; }
        public DateTime From { get; set; }
        public string serialNo { get; set; }
        public string  invoiceType { get; set; }





        public SaleModel()
        {
            netAmt = 0;
            grossAmt = 0;
            CusType = 0;
            status = "Sold";
            PreviousDue = 0;
            entryDate = commonFunction.GetCurrentTime();
        }



        public string updateSaleDataModel()
        {
            string querySale = "UPDATE SaleInfo SET roleId='" + roleId
                                        + "',prodID='" + prodID
                                        + "',qty='" + qty
                                        + "',netAmt= '" + netAmt
                                        + "',discAmt='" + discAmt
                                        + "',vatAmt = '" + vatAmt
                                        + "',grossAmt='" + grossAmt
                                        + "',payMethod='" + payMethod
                                        + "',payCash='" + payCash
                                        + "',payCard='" + payCard
                                        + "',giftAmt='" + giftAmt
                                        + "',return_='" + return_
                                        + "',balance='" + balance
                                        + "',sPrice='" + sPrice
                                        + "',discType='" + discType
                                        + "',comment='" + comment
                                        + "',currentCash='" + currentCash
                                        + "',branchId='" + branchId
                                        + "',groupId='" + groupId
                                        + "',salesPersonId='" + salesPersonId
                                        + "',referredBy='" + referredBy
                                        + "',cardId='" + cardId
                                        + "',bankId='" + bankId
                                        + "',warranty='" + warranty
                                        + "',token='" + token
                                        + "',CusType='" + CusType
                                        + "',maturityDate='" + MaturityDate
                                        + "',checkNo='" + checkNo
                                        + "',loadingCost ='" + loadingCost
                                        + "',shippingCost= '" + shippingCost
                                        + "',carryingCost = '" + carryingCost
                                        + "',serviceCharge = '" + serviceCharge
                                        + "',returnQty = '" + returnQty
                                        + "',returnAmt = '" + returnAmt
                                        + "',previousDue = '" + PreviousDue
                                        + "',imei = '" + imei
                                        + "',extraDiscount = extraDiscount +'" + extraDiscount
                                        + "',refName = '" + refName
                                        + "',refPhone = '" + refPhone
                                        + "',refAddress = '" + refAddress
                                        + "' WHERE billNo = '" + billNo
                                        + "' AND prodId='" + prodID + "' ";
            return querySale;
        }


        public void createSale()
        {
            string query = "INSERT INTO SaleInfo ( " +
                    "billNo,                             " +
                    "roleId,                       " +
                    "cusId," +
                    "prodId," +
                    "qty, " +
                    "netAmt, " +
                    "discAmt, " +
                    "vatAmt, " +
                    "grossAmt, " +
                    "payMethod, " +
                    "invoiceType,"+
                    "payCash, " +
                    "payCard, " +
                    "giftAmt, " +
                    "return_," +
                    "balance, " +
                    "entryDate, " +
                    "sPrice, " +
                    "discType, " +
                    "comment, " +
                    "currentCash, " +
                    "branchId, " +
                    "groupId, " +
                    "salesPersonId, " +
                    "cardId, " +
                    "bankId, " +
                    "warranty, " +
                    "token, " +
                    "cusType, " +
                    "maturityDate, " +
                    "checkNo, " +
                    "loadingCost, " +
                    "shippingCost, " +
                    "carryingCost, " +
                    "unloadingCost, " +
                    "salePersonType, " +
                    "status," +
                    "previousDue," +
                    "vatType) " +
                    "VALUES ('" +
                    billNo + "', '" +
                    HttpContext.Current.Session["roleId"] + "', '" +
                    cusID + "', '" +
                    prodID + "', '" +
                    qty + "', '" +
                    netAmt + "', '" +
                    discAmt + "', '" +
                    vatAmt + "', '" +
                    grossAmt + "', '" +
                    payMethod + "', '" +
                    invoiceType + "', '" +
                    payCash + "', '" +
                    payCard + "', '" +
                    giftAmt + "', '" +
                    return_ + "', '" +
                    balance + "', '" +
                    entryDate.ToString("dd-MMM-yyyy") + "', '" +
                    sPrice + "',  '" +
                    discType + "', '" +
                    comment + "', '" +
                    currentCash + "', '" +
                    HttpContext.Current.Session["branchId"] + "', '" +
                    HttpContext.Current.Session["groupId"] + "', '" +
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
                    '0' + "','" +
                    '1' + "','" +
                    PreviousDue + "','" +
                    vatType + "') ";

            sqlOperation.executeQuery(query);
        }





        public dynamic getSale(string getConditinalParameter)
        {
            string query = "SELECT * FROM SaleInfo WHERE " + getConditinalParameter + "";
            return sqlOperation.getDataSet(query);
        }





        public void updateSale()
        {
            string query = "UPDATE SaleInfo SET billNo = '" + billNo
                    + "', roleId = '" + HttpContext.Current.Session["roleId"]
                    + "', cusID = '" + cusID
                    + "', prodID = '" + prodID
                    + "', qty = '" + qty
                    + "', netAmt = '" + netAmt
                    + "', discAmt = '" + discAmt
                    + "', vatAmt = '" + vatAmt
                    + "', grossAmt = '" + grossAmt
                    + "', payMethod = '" + payMethod
                    + "', payCash = '" + payCash
                    + "', payCard = '" + payCard
                    + "', giftAmt = '" + giftAmt
                    + "', return_ = '" + return_
                    + "', balance = '" + balance
                    + "', entryDate = '" + commonFunction.GetCurrentTime().ToString("dd-MMM-yyyy")
                    + "', sPrice = '" + sPrice
                    + "', discType = '" + discType
                    + "', comment = '" + comment
                    + "', currentCash = '" + currentCash
                    + "', branchId = '" + HttpContext.Current.Session["branchId"]
                    + "', groupId = '" + HttpContext.Current.Session["groupId"]
                    + "', salesPersonId = '" + salesPersonId
                    + "', cardId = '" + cardId
                    + "', bankId = '" + bankId
                    + "', warranty = '" + warranty
                    + "', token = '" + token
                    + "', CusType = '" + CusType + "' ";

            sqlOperation.executeQuery(query);
        }





        public void deleteSale()
        {
            sqlOperation.executeQuery("DELETE FROM SaleInfo WHERE billNo = '" + billNo + "'");
        }





        public dynamic listSale()
        {
            string query = "SELECT * FROM SaleInfo WHERE billNo = '" + billNo + "'";
            return sqlOperation.getDataSet(query);
        }





        // Create sale Id / billNo
        public string generateSaleId()
        {
            string lastSaleId = commonFunction.getSaleLastID();

            try
            {
                billNo = commonFunction.nextIdGenerator(lastSaleId);
            }
            catch
            {
                billNo = "AA00001";
            }

            return billNo;
        }








        public string getSaleLastIDStoreWise()
        {
            var dtSaleId = sqlOperation.getDataTable("SELECT billNo FROM [SaleInfo] where storeId = '" + HttpContext.Current.Session["storeId"] + "' ORDER BY billNo DESC");
            if (dtSaleId.Rows.Count > 0)
                return dtSaleId.Rows[0][0].ToString();
            else
                return "";
        }



        public DataSet getLastBillNoModel(string billNo)
        {
            var dslastBill =
                sqlOperation.getDataSet("SELECT billNo FROM [SaleInfo] WHERE billNo < '" + billNo +
                                        "' ORDER BY billNo DESC");
            return dslastBill;
        }





        public DataSet getInsertedLastBillNoDesc()
        {
            var ds = sqlOperation.getDataSet("SELECT billNo FROM [SaleInfo] ORDER BY billNo DESC");
            return ds;
        }





        public string saveSaleDataListModel()
        {

            string query = "INSERT INTO SaleInfo (" +
                    "billNo," +
                    "roleId," +
                    "cusId," +
                    "prodId," +
                    "qty," +
                    "serialNo,"+
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
                    prodID + "', '" + 
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

            return query;
        }


        public string saveSlipDataModel()
        {

            string query = "INSERT INTO SlipInfo " +
                           "(billNo, roleId," +
                           "cusId," +
                           "prodId," +
                           "qty," +
                           "serialNo,"+
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
                           prodID + "','" + 
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

            return query;
        }







        //public bool saveStockStatusInfoModel()
        //{
        //    string query = "INSERT INTO StockStatusInfo VALUES ('"+prodID"','"+prod+"')"
        //}

        public DataTable getSaleInfoDataListModel(string billNO)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM SaleInfo WHERE billNo= '" + billNO + "'");
            return dt;
        }





        public DataTable getSaleInfoDataListModel(string prodId, string billNo)
        {
            return sqlOperation.getDataTable("SELECT * FROM SaleInfo WHERE prodId= '" + prodId + "' AND billNo= '" + billNo + "'");
        }






        public DataTable getItemLsitModel(string searchTxt, string billNo)
        {
            string query =
                "SELECT distinct stock.prodCode,sale.qty,sale.Id as saleId FROM StockInfo as stock LEFT JOIN SaleInfo as sale ON stock.prodId = sale.prodId WHERE sale.prodId= '" +
                searchTxt + "' AND sale.BillNo ='" + billNo + "'";
            DataTable dt = sqlOperation.getDataTable(query);
            return dt;
        }





        public DataTable saleSettingOptionModel()
        {

            return sqlOperation.getDataTable("SELECT * FROM SettingInfo WHERE id='" + HttpContext.Current.Session["roleId"] +
                                          "'");

        }











        public DataTable getInvoiceWithProductLsitModel(string billNo, string prodId)
        {
            var dt =
                sqlOperation.getDataTable("SELECT * FROM SaleInfo WHERE billNo= '" + billNo + "' AND prodId='" + prodId +
                                          "'");
            return dt;
        }





        public bool updateSlipDataModel()
        {
            try
            {
                sqlOperation.executeQuery("UPDATE SlipInfo SET cusId='" +
                                          cusID + "', ProdId='" +
                                          prodID + "', qty='" +
                                          qty + "',netAmt='" +
                                          netAmt + "',discAmt='" +
                                          discAmt + "',vatAmt='" +
                                          vatAmt + "',grossAmt='" +
                                          grossAmt + "',payMethod='" +
                                          payMethod + "',payCash='" +
                                          payCash + "',payCard='" +
                                          payCard + "',giftAmt='" +
                                          giftAmt + "',return_='" +
                                          return_ + "',balance=balance + '" +
                                          balance + "',status='Resold',branchId='" +
                                          branchId + "',groupId='" +
                                          groupId + "' WHERE billNo='" + billNo + "'");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }





        public DataTable getSaleLsitModel(string billNo, string prodId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM SaleInfo WHERE billNo= '" + billNo + "' AND prodId='" + prodId +
                                          "'");
            return dt;
        }





        public void deleteSaleDataById(string p)
        {
            sqlOperation.executeQuery("DELETE FROM SaleInfo WHERE Id='" + p + "'");
        }





        public DataTable getSaleInfoDataModelById(string delId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT * FROM SaleInfo WHERE Id='" + delId + "'");
            return dt;
        }



        public DataTable getSaleInfoDataListModelByProdId(string prodId)
        {
            DataTable dt = sqlOperation.getDataTable("SELECT SUM(CAST(qty as float)) as qty FROM SaleInfo WHERE prodId='" + prodId + "'");
            return dt;
        }






        public string changeSaleInfoStatusModel()
        {
            string query = "UPDATE SaleInfo SET status='0' WHERE billNo='" + billNo + "'";
            return query;
        }




        public DataTable getProductDataListSoldItemModel(string prodId, string billNo)
        {
            string query =
                "select top 1 stock.qty as stockQty, stockStatus.qty as stockStatusQty, sale.qty as saleQty, unit.unitRatio FROM StockInfo as stock LEFT JOIN StockStatusInfo as stockStatus ON stock.prodID = stockStatus.prodID LEFT JOIN SaleInfo as sale ON stock.prodId = sale.prodID LEFT JOIN UnitInfo as unit ON stock.UnitId = unit.Id where sale.prodID= '" +
                prodId + "' AND sale.billNo = '" + billNo + "'";
            return sqlOperation.getDataTable(query);
        }




        public DataTable getProductDataListNewItemModel(string prodId)
        {
            string query = "select top 1 stock.qty as stockQty, unit.unitRatio FROM StockInfo as stock LEFT JOIN UnitInfo as unit ON stock.UnitId = unit.Id where stock.prodID= '" + prodId + "'";
            return sqlOperation.getDataTable(query);

        }




        public DataTable getInvoicePrintDataModel(string billNo, string storeId)
        {
            string printQuery = @"SELECT DISTINCT sale.prodID, sale.qty, sale.sPrice, sale.netAmt, sale.discAmt,
                                sale.vatAmt,sale.vatType, sale.grossAmt, sale.payMethod,sale.billNo,sale.cusID,
                                sale.payCash,sale.balance, sale.payCard, sale.giftAmt, sale.return_, sale.balance, 
                                sale.entryDate, sale.sPrice, sale.currentCash, sale.comment,sale.vatType,
                                sale.loadingCost,sale.shippingCost,sale.carryingCost,sale.unloadingCost,sale.serviceCharge,
                                sale.salePersonType,sale.returnQty,sale.isAutoSalesPerson,sale.extraDiscount,sale.returnAmt,
                                (select top 1 status from SlipInfo where billNo = '" + billNo + @"' order by Id desc ) as status,
                                service.name as serviceName, service.type as serviceType,
                                ci.name, ci.phone,ci.address,ci.mailInfo,ci.totalDue,ci.openingDue,ci.bloodGroup,ci.sex,ci.age,
                                si.prodName AS prodName,si.bPrice, si.sPrice,si.catName,si.warranty, si.prodCode, 
                                si.unitId,si.locationId,si.commission,si.dealerPrice,si.engineNumber,si.cecishNumber,
                                stockstatus.attributeRecord,stockstatus.serialNo,stockstatus.payType,stockstatus.serialNo,
                                unit.unitName,unit.unitRatio,stockstatus.invoiceType,
                                warehouse.name as storeName,
                                location.name as locationName,
                                staff.name AS staffName,
                                bi.branchName, bi.branchAddress, bi.branchPhone, 
                                bi.branchMobile, bi.invoiceFooterNote, bi.branchLogoPath,bi.storeId,
                                cat.Id, cat.catName as CategoryName,
                                sup.supCompany as supCompany,
                                ri.title, stockstatus.searchType,
                                sale.imei, pack.packageName AS packName,
                                CONCAT(pack.packageName , si.prodName) AS prodPackName,
                                (SELECT SUM(cashOut)-SUM(cashIn) FROM CashReportInfo where descr = sale.cusID AND status='6' ) as cusCurrentDue
                                FROM  SaleInfo AS sale
                                LEFT JOIN CustomerInfo AS ci ON ci.cusId = sale.cusID
                                LEFT JOIN stockInfo AS si ON si.ProdID = sale.prodID
                                LEFT JOIN LocationInfo as location ON si.locationId=location.Id
                                LEFT JOIN StaffInfo AS staff ON sale.salesPersonId = staff.staffID 
                                LEFT JOIN RoleInfo as ri ON ri.roleId = sale.salesPersonId
                                LEFT JOIN CategoryInfo AS cat ON si.catName = cat.Id
                                LEFT JOIN PackageInfo AS pack ON sale.prodID = pack.Id
                                LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodID = sale.prodID
                                LEFT JOIN ServiceInfo AS service ON service.Id = stockstatus.prodID
                                LEFT JOIN supplierInfo as sup ON si.supCompany = sup.supId
                                LEFT JOIN unitInfo as unit ON unit.Id = si.unitId
                                LEFT JOIN WarehouseInfo as warehouse ON warehouse.Id = si.warehouse  
                                LEFT JOIN BranchInfo AS bi ON '" + storeId + "' = bi.storeId " +
                             " WHERE sale.billNo = '" + billNo + "' and stockstatus.billNo = '" + billNo + 
                             "' AND stockstatus.status !='saleReturn' AND (isPackage='0' OR stockstatus.status ='salePackage')";

            return sqlOperation.getDataTable(printQuery);
        }




        public bool updateSaleInfoByDirectCustomerModel(string billNo, string cusId, string preRoleId, string pay)
        {
            string query = "UPDATE SaleInfo SET paycash=" + pay + ", giftAmt = giftAmt-" + pay + ", balance=balance+" + pay + " where billNo='" + billNo + "'";
            return sqlOperation.fireQuery(query);
        }





        public DataTable getsaleSummeryStoreWiseModel()
        {
            string query =
                "select SUM(netAmt) as netAmt, SUM(discAmt) AS discAmt, SUM(grossAmt) as grossAmt, " +
                "SUM(balance) as balance, SUM(giftAmt) as giftAmt,SUM(loadingCost) as loadingCost,SUM(carryingCost) as carryingCost," +
                "SUM(unloadingCost) as unloadingCost,SUM(shippingCost) as shippingCost,SUM(serviceCharge) as serviceCharge FROM " +
                "(SELECT DISTINCT BillNo,netAmt,discAmt,grossAmt,balance,giftAmt,loadingCost,carryingCost,unloadingCost,shippingCost,serviceCharge FROM SaleInfo " +
                "WHERE status='1' AND (CAST(entryDate AS date) >= '" + From.ToString("MM-dd-yyyy") + "' AND CAST(entryDate AS date) <= '" + To.ToString("MM-dd-yyyy") + "') " + storeAccessParameters + ") as sale";

            return sqlOperation.getDataTable(query);
        }



        public DataTable getSaleRecordInfoModel()
        {
            string querySale =
                "SELECT SUM(grossAmt),(SUM(CAST(qty as decimal))-SUM(CAST(returnQty as decimal))) as qty FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS date) >= '" +
                From.ToShortDateString() + "' AND CAST(entryDate AS date) <= '" + To.AddDays(1).ToShortDateString() + "') " +
                storeAccessParameters + " GROUP BY billNo";

            return sqlOperation.getDataTable(querySale);
        }

        public DataTable getSalePackageReturnModel()
        {
            var queryPackage = "Select SUM(CAST(qty as decimal)) as returnQty from Stockstatusinfo where status='packageReturn' AND prodId='" + prodID + "' AND BillNo='" + billNo + "'";
            return sqlOperation.getDataTable(queryPackage);
        }


        public DataTable getSalePackageQtyModel()
        {
            var queryPackage = "Select SUM(CAST(qty as decimal)) as qty from Stockstatusinfo where status='salePackage' AND prodId='" + prodID + "' AND BillNo='" + billNo + "'";
            return sqlOperation.getDataTable(queryPackage);
        }

        public DataTable getSaleProductQtyModel()
        {
            var queryPackage = "Select qty from Stockstatusinfo where status='saleReturn' AND prodId='" + prodID + "' AND BillNo='" + billNo + "'";
            return sqlOperation.getDataTable(queryPackage);
        }

        public bool deleteDraftModel()
        {
            string queryDraft = "BEGIN TRANSACTION " +
                                "BEGIN DELETE SlipInfo Where billNo = '" + billNo + "' END " +
                                "BEGIN DELETE saleinfo Where billNo = '" + billNo + "' END " +
                                "BEGIN DELETE StockStatusInfo WHERE BillNO='"+billNo+"' END " +
                                "COMMIT";
            return sqlOperation.fireQuery(queryDraft);
        }
    }


}