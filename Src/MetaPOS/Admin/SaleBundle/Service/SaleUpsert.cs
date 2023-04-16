using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using MetaPOS.Admin.Controller;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.InventoryBundle.Service;
using MetaPOS.Admin.Model;
using MetaPOS.Admin.InstallmentBundle.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MetaPOS.Admin.SaleBundle.Service
{


    public class SaleUpsert
    {


        private CommonFunction commonFunction = new CommonFunction();
        private SaleModel saleModel = new SaleModel();
        private Stock inventoryStock = new Stock();
        private StockStatusModel stockStatusModel = new StockStatusModel();
        private StockModel stockModel = new StockModel();
        private CustomerModel customerModel = new CustomerModel();
        private CommonController objCommonController = new CommonController();
        private SqlOperation sqlOperation = new SqlOperation();




        public string saveSaleInfoList(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            string inputQty = data["qty"].Value<string>();

            string billNo = data["billNo"].Value<string>();
            string prodId = data["prodID"].Value<string>();

            //same work here
            DataTable dtBal = saleModel.getSaleLsitModel(billNo, prodId);

            decimal dbBalance = 0;
            if (dtBal.Rows.Count > 0)
            {
                dbBalance = Convert.ToDecimal(dtBal.Rows[0]["balance"]);
            }

            var payCashOne = Convert.ToDecimal(data["payCash"].Value<string>() == "" ? "0" : data["payCash"].Value<string>());
            var payCashTwo = Convert.ToDecimal(data["payCashTwo"].Value<string>() == "" ? "0" : data["payCashTwo"].Value<string>());
            var payCash = payCashOne + payCashTwo;
            var grossAmt = data["grossAmt"].Value<decimal>();
            var preDue = data["preDue"].Value<decimal>();
            var isAdvanced = data["isAdvance"].Value<bool>();
            var totalDue = grossAmt + preDue;

            if (!isAdvanced && payCash > totalDue)
            {
                payCash = totalDue;
            }


            saleModel.billNo = data["billNo"].Value<string>();
            saleModel.roleId = HttpContext.Current.Session["roleId"].ToString();
            saleModel.cusID = data["cusId"].Value<string>();
            saleModel.prodID = data["prodID"].Value<string>();
            saleModel.qty = data["qty"].Value<string>();
            saleModel.serialNo = data["serialNo"].Value<string>();
            saleModel.invoiceType = data["invoiceType"].Value<string>();
            saleModel.netAmt = data["netAmt"].Value<decimal>();
            saleModel.discAmt = data["discAmt"].Value<decimal>();
            saleModel.vatAmt = data["vatAmt"].Value<decimal>();
            saleModel.grossAmt = data["grossAmt"].Value<decimal>();
            saleModel.payMethod = data["payMethod"].Value<string>();
            saleModel.payCash = payCashOne + payCashTwo;
            saleModel.payCard = data["payCard"] == null ? "0" : data["payCard"].ToString();
            saleModel.giftAmt = getInvoiceGiftAmt(billNo, grossAmt, payCash, dbBalance);
            saleModel.return_ = data["return_"].Value<decimal>();
            saleModel.balance = payCash;
            saleModel.entryDate = commonFunction.GetCurrentTime();
            saleModel.sPrice = data["sPrice"].Value<decimal>();
            saleModel.discType = data["discType"].Value<string>();
            saleModel.comment = data["comment"].Value<string>();
            saleModel.currentCash = data["currentCash"].Value<decimal>();
            saleModel.branchId = Convert.ToInt32(HttpContext.Current.Session["barnchId"]);
            saleModel.groupId = Convert.ToInt32(HttpContext.Current.Session["groupId"]);
            saleModel.salesPersonId = data["salesPersonId"].Value<int>();
            saleModel.referredBy = data["referredBy"].Value<int>();
            saleModel.cardId = data["cardId"] == null ? '0' : data["cardId"].Value<int>();
            saleModel.bankId = data["bankId"] == null ? '0' : data["bankId"].Value<int>();
            saleModel.warranty = data["warranty"].Value<string>();
            saleModel.token = data["token"] == null ? " " : data["token"].Value<string>();
            saleModel.CusType = data["cusType"] == null ? 0 : data["cusType"].Value<int>();
            saleModel.MaturityDate = commonFunction.GetCurrentTime();
            saleModel.checkNo = data["checkNo"] == null ? " " : data["checkNo"].Value<string>();
            saleModel.loadingCost = data["loadingCost"].Value<decimal>();
            saleModel.unloadingCost = data["unloadingCost"].Value<decimal>();
            saleModel.serviceCharge = data["serviceCharge"].Value<decimal>();
            saleModel.shippingCost = data["shippingCost"].Value<decimal>();
            saleModel.carryingCost = data["carryingCost"].Value<decimal>();
            saleModel.salePersonType = data["salePersonType"].Value<string>();
            saleModel.returnQty = data["returnQty"].Value<string>();
            saleModel.returnAmt = data["returnAmt"].Value<decimal>();
            saleModel.entryDate = data["entryDate"].Value<DateTime>();
            saleModel.PreviousDue = data["preDue"].Value<decimal>();
            saleModel.interestRate = data["interestRate"].Value<int>();
            saleModel.interestAmt = data["interestAmt"].Value<decimal>();
            saleModel.searchType = data["searchType"].Value<string>();
            saleModel.extraDiscount = data["extraDiscount"].Value<decimal>();
            saleModel.imei = data["imei"].ToString().Replace('×', ',').Replace(" ", string.Empty);
            saleModel.storeId = HttpContext.Current.Session["storeId"].ToString();
            saleModel.refName = data["refName"].Value<string>();
            saleModel.refPhone = data["refPhone"].Value<string>();
            saleModel.refAddress = data["refAddress"].Value<string>();
            saleModel.vatType = data["vatType"].Value<string>();


            if (commonFunction.findSettingItemValueDataTable("autoSalesPerson") == "1")
            {
                saleModel.isAutoSalesPerson = true;
            }

            var transactionQuery = "";
            transactionQuery += "BEGIN ";
            transactionQuery += saleModel.saveSaleDataListModel();
            transactionQuery += "END ";

            return transactionQuery;
        }





        public decimal getInvoiceGiftAmt(string billNo, decimal grossAmt, decimal payCash, decimal paidAmt)
        {
            decimal giftAmt = 0;

            decimal totalPaid = paidAmt + payCash;
            if (totalPaid < grossAmt)
            {
                giftAmt = grossAmt - totalPaid;
            }

            return giftAmt;
        }





        public string createCashReportData(dynamic data)
        {
            var entryDate = new DateTime();
            try
            {
                entryDate = (DateTime)data["entryDate"];
            }
            catch (Exception)
            {
                entryDate = commonFunction.GetCurrentTime();
            }



            decimal currentCash = Convert.ToDecimal(data["currentCash"]);
            decimal grossAmt = Convert.ToDecimal(data["grossAmt"]);
            decimal paidAmt = Convert.ToDecimal(data["paidAmt"]);
            string billNo = data["billNo"].ToString();
            string payMethod = data["payMethod"].ToString();
            decimal returnAmt = Convert.ToDecimal(data["returnAmt"]);
            decimal paidReturnAmt = Convert.ToDecimal(data["paidReturnAmt"]);
            decimal preDue = Convert.ToDecimal(data["preDue"]);
            string cusId = data["cusId"].ToString();

            decimal totalDiscAmt = Convert.ToDecimal(data["discAmt"]);
            decimal extraDiscount = Convert.ToDecimal(data["extraDiscount"]);
            decimal balance = Convert.ToDecimal(data["balance"]);
            bool isAdvance = Convert.ToBoolean(data["isAdvance"]);
            decimal giftAmt = Convert.ToDecimal(data["giftAmt"]);
            decimal payCashTwo = Convert.ToDecimal(data["payCashTwo"]);
            string payMethodTwo = data["payMethodTwo"].ToString();
            var purchaseCode = "";
            var cardType = data["cardId"].ToString();
            var maturityDate = data["maturityDate"].ToString();
            var bankName = data["brankId"].ToString();
            var checkNo = data["checkNo"].ToString();
            var payType = Convert.ToString(data["payType"]);
            var payDescr = Convert.ToString(data["payDescr"]);




            var cashReportModel = new CashReportModel();
            cashReportModel.payMethod = payMethod;
            cashReportModel.entryDate = entryDate;

            var transactionQuery = "";
            // Save Opt
            if (data["opt"].ToString() == "sale")
            {
                // Customer Cashout
                cashReportModel.cashType = "Invoice";
                cashReportModel.descr = cusId;
                cashReportModel.billNo = billNo;
                cashReportModel.cashIn = 0;
                cashReportModel.cashOut = grossAmt + totalDiscAmt; // gross amount + discount amount with extra discount
                cashReportModel.status = '6';
                cashReportModel.payType = payType;
                cashReportModel.payDescr = payDescr;


                transactionQuery += "BEGIN ";
                transactionQuery += cashReportModel.saveCustomerCashData();
                transactionQuery += "END ";


                if (currentCash != 0)
                {
                    // Sales transection
                    //commonFunction.cashTransactionSales(currentCash, 0, "Sales Payment", cusId, billNo, payMethod, "5", "0", dateTime);

                    if (currentCash > grossAmt + preDue && !isAdvance)
                    {
                        currentCash = grossAmt + preDue;
                    }

                    var saleCashReport = new SaleCashReport();
                    saleCashReport.cashType = "Sales Payment";
                    saleCashReport.descr = cusId;
                    saleCashReport.cashIn = currentCash;
                    saleCashReport.cashOut = 0;
                    saleCashReport.cashInHand = 0;
                    saleCashReport.entryDate = entryDate;
                    saleCashReport.billNo = billNo;
                    saleCashReport.mainDescr = payMethod;
                    saleCashReport.roleId = Convert.ToInt32(HttpContext.Current.Session["roleId"].ToString());
                    saleCashReport.branchId = Convert.ToInt32(HttpContext.Current.Session["branchId"].ToString());
                    saleCashReport.groupId = Convert.ToInt32(HttpContext.Current.Session["groupId"].ToString());
                    saleCashReport.status = '5';
                    saleCashReport.adjust = '0';
                    saleCashReport.isSchedulePayment = false;
                    saleCashReport.isScheduled = false;
                    saleCashReport.isReceived = true;
                    saleCashReport.trackAmt = 0M;
                    saleCashReport.storeId = Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
                    saleCashReport.payMethod = payMethod;
                    saleCashReport.purchaseCode = purchaseCode;
                    saleCashReport.payType = payType;
                    saleCashReport.payDescr = payDescr;

                    if (payMethod == "7")
                    {
                        saleCashReport.cardType = cardType;
                    }
                    else if (payMethod == "6")
                    {
                        saleCashReport.maturityDate = Convert.ToDateTime(maturityDate);
                        saleCashReport.bankName = bankName;
                        saleCashReport.checkNo = checkNo;
                    }
                    else
                    {
                        saleCashReport.cardType = "";
                        saleCashReport.maturityDate = Convert.ToDateTime("01/01/2000");
                        saleCashReport.bankName = "";
                        saleCashReport.checkNo = "";
                    }
                    transactionQuery += "BEGIN ";
                    transactionQuery += saleCashReport.cashTransactionSales();
                    transactionQuery += "END ";


                    //Customer Cashin/ payment


                    cashReportModel.cashType = "Payment";
                    cashReportModel.descr = cusId;
                    cashReportModel.billNo = billNo;
                    cashReportModel.cashIn = currentCash;
                    cashReportModel.cashOut = 0;
                    cashReportModel.status = '6';
                    cashReportModel.payType = payType;
                    cashReportModel.payDescr = payDescr;


                    transactionQuery += "BEGIN ";
                    transactionQuery += cashReportModel.saveCustomerCashData();
                    transactionQuery += "END ";

                    if (payCashTwo > 0)
                    {

                        if (currentCash < grossAmt + preDue)
                        {


                            var exceptPaycashTwo = payCashTwo;
                            var needPayTwo = (grossAmt + preDue) - currentCash;
                            if (payCashTwo > needPayTwo && !isAdvance)
                                exceptPaycashTwo = needPayTwo;


                            //commonFunction.cashTransactionSales(payCashTwo, 0, "Sales Payment", cusId, billNo, payMethodTwo, "5", "0", dateTime);
                            saleCashReport.cashIn = exceptPaycashTwo;
                            saleCashReport.payMethod = payMethodTwo;
                            if (payMethodTwo == "2")
                            {
                                saleCashReport.cardType = cardType;
                            }
                            else if (payMethodTwo == "6")
                            {
                                saleCashReport.maturityDate = Convert.ToDateTime(maturityDate);
                                saleCashReport.bankName = bankName;
                                saleCashReport.checkNo = checkNo;
                            }
                            else
                            {
                                saleCashReport.cardType = "";
                                saleCashReport.maturityDate = Convert.ToDateTime("01/01/2000");
                                saleCashReport.bankName = "";
                                saleCashReport.checkNo = "";
                            }
                            saleCashReport.status = '5';
                            saleCashReport.purchaseCode = purchaseCode;
                            transactionQuery += "BEGIN ";
                            transactionQuery += saleCashReport.cashTransactionSales();
                            transactionQuery += "END ";

                            cashReportModel.cashIn = exceptPaycashTwo;
                            cashReportModel.status = '6';
                            cashReportModel.payType = payType;
                            cashReportModel.payDescr = payDescr;



                            transactionQuery += "BEGIN ";
                            transactionQuery += cashReportModel.saveCustomerCashData();
                            transactionQuery += "END ";
                        }
                    }
                }


                // Discount save in customer
                if (totalDiscAmt > 0)
                {
                    cashReportModel.cashType = "Discount";
                    cashReportModel.descr = cusId;
                    cashReportModel.billNo = billNo;
                    cashReportModel.cashIn = totalDiscAmt;
                    cashReportModel.cashOut = 0;
                    cashReportModel.status = '6';
                    cashReportModel.payType = payType;
                    cashReportModel.payDescr = payDescr;

                    transactionQuery += "BEGIN ";
                    transactionQuery += cashReportModel.saveCustomerCashData();
                    transactionQuery += "END ";

                }

                var currentAdvance = Math.Abs(preDue) - grossAmt;
                if (currentAdvance > 0 && !isAdvance && preDue < 0)
                {
                    // Sales return
                    transactionQuery += "BEGIN ";
                    transactionQuery += commonFunction.cashTransactionSalesData(0, currentAdvance, "Sales Return", cusId, billNo, payMethod, "5", "0", entryDate.ToShortDateString(), payType, payDescr);
                    transactionQuery += "END ";

                    // Cash return
                    cashReportModel.cashType = "Cash Return";
                    cashReportModel.descr = cusId;
                    cashReportModel.billNo = billNo;
                    cashReportModel.cashIn = 0;
                    cashReportModel.cashOut = currentAdvance;
                    cashReportModel.status = '6';
                    cashReportModel.payType = payType;
                    cashReportModel.payDescr = payDescr;

                    transactionQuery += "BEGIN ";
                    transactionQuery += cashReportModel.saveCustomerCashData();
                    transactionQuery += "END ";

                }

            }
            else
            {
                var isTransection = true;
                var isSalesReturn = false;
                returnAmt = returnAmt - paidReturnAmt;
                if (returnAmt > 0)
                {
                    if (preDue > returnAmt)
                    {
                        //Customer return
                        cashReportModel.cashType = "Product Return";
                        cashReportModel.descr = cusId;
                        cashReportModel.billNo = billNo;
                        cashReportModel.cashIn = returnAmt;
                        cashReportModel.cashOut = 0;
                        cashReportModel.status = '6';
                        cashReportModel.payType = payType;
                        cashReportModel.payDescr = payDescr;

                        transactionQuery += "BEGIN ";
                        transactionQuery += cashReportModel.saveCustomerCashData();
                        transactionQuery += "END ";

                        if (currentCash == 0)
                            isTransection = false;
                    }
                    else if (preDue <= returnAmt)
                    {
                        isSalesReturn = true;

                        // Sales return
                        transactionQuery += "BEGIN ";
                        transactionQuery += commonFunction.cashTransactionSalesData(0, returnAmt - preDue, "Sales Return", cusId, billNo, payMethod, "5", "0", entryDate.ToShortDateString(), payType, payDescr);
                        transactionQuery += "END ";


                        //Customer return
                        cashReportModel.cashType = "Product Return";
                        cashReportModel.descr = cusId;
                        cashReportModel.billNo = billNo;
                        cashReportModel.cashIn = returnAmt;
                        cashReportModel.cashOut = 0;
                        cashReportModel.status = '6';
                        cashReportModel.payType = payType;
                        cashReportModel.payDescr = payDescr;

                        transactionQuery += "BEGIN ";
                        transactionQuery += cashReportModel.saveCustomerCashData();
                        transactionQuery += "END ";

                        // Cash return
                        cashReportModel.cashType = "Cash Return";
                        cashReportModel.descr = cusId;
                        cashReportModel.billNo = billNo;
                        cashReportModel.cashIn = 0;
                        cashReportModel.cashOut = returnAmt - preDue;
                        cashReportModel.status = '6';
                        cashReportModel.payType = payType;
                        cashReportModel.payDescr = payDescr;

                        transactionQuery += "BEGIN ";
                        transactionQuery += cashReportModel.saveCustomerCashData();
                        transactionQuery += "END ";

                        isTransection = false;
                    }
                }


                if (currentCash != 0 && isTransection)
                {
                    // Sales transection
                    transactionQuery += "BEGIN ";
                    transactionQuery += commonFunction.cashTransactionSalesData(currentCash, 0, "Due Payment", payMethod, billNo, payMethod, "5", "0", entryDate.ToShortDateString(), payType, payDescr);
                    transactionQuery += "END ";

                    //Customer Cashin/ payment
                    cashReportModel.cashType = "Payment";
                    cashReportModel.descr = cusId;
                    cashReportModel.billNo = billNo;
                    cashReportModel.cashIn = currentCash;
                    cashReportModel.cashOut = 0;
                    cashReportModel.status = '6';
                    cashReportModel.payType = payType;
                    cashReportModel.payDescr = payDescr;

                    transactionQuery += "BEGIN ";
                    transactionQuery += cashReportModel.saveCustomerCashData();
                    transactionQuery += "END ";
                }


                if (extraDiscount > 0)
                {
                    cashReportModel.cashType = "Discount";
                    cashReportModel.descr = cusId;
                    cashReportModel.billNo = billNo;
                    cashReportModel.cashIn = extraDiscount;
                    cashReportModel.cashOut = 0;
                    cashReportModel.status = '6';
                    cashReportModel.payType = payType;
                    cashReportModel.payDescr = payDescr;

                    transactionQuery += "BEGIN ";
                    transactionQuery += cashReportModel.saveCustomerCashData();
                    transactionQuery += "END ";
                }


                if (balance < 0 && !isSalesReturn)
                {
                    var returnBalance = Math.Abs(balance);
                    // Sales return
                    transactionQuery += "BEGIN ";
                    transactionQuery += commonFunction.cashTransactionSalesData(0, returnBalance, "Sales Return", cusId, billNo, payMethod, "5", "0", entryDate.ToShortDateString(), payType, payDescr);
                    transactionQuery += "END ";

                    // Cash return
                    cashReportModel.cashType = "Cash Return";
                    cashReportModel.descr = cusId;
                    cashReportModel.billNo = billNo;
                    cashReportModel.cashIn = 0;
                    cashReportModel.cashOut = returnBalance;
                    cashReportModel.status = '6';
                    cashReportModel.payType = payType;
                    cashReportModel.payDescr = payDescr;

                    transactionQuery += "BEGIN ";
                    transactionQuery += cashReportModel.saveCustomerCashData();
                    transactionQuery += "END ";

                }

            }

            return transactionQuery;
        }





        public string updateSaleDataOperation(dynamic data)
        {
            var transactionQuery = "";


            string billNo = data["billNo"].ToString();
            string prodId = data["prodID"].ToString();

            var dtBal = saleModel.getSaleLsitModel(billNo, prodId);

            decimal dbBalance = 0;
            if (dtBal.Rows.Count > 0)
            {
                dbBalance = Convert.ToDecimal(dtBal.Rows[0]["balance"]);
            }

            decimal payCash = Convert.ToDecimal(data["payCash"].ToString() == "" ? "0" : data["payCash"].ToString());

            var grossAmt = Convert.ToDecimal(data["grossAmt"]);
            var giftAmt = getInvoiceGiftAmt(billNo, grossAmt, payCash, dbBalance);

            // Assaign data
            saleModel.billNo = data["billNo"].ToString();
            saleModel.roleId = HttpContext.Current.Session["roleId"].ToString();
            saleModel.cusID = data["cusId"].ToString();
            saleModel.prodID = data["prodID"].ToString();
            saleModel.qty = data["qty"].ToString();
            saleModel.netAmt = Convert.ToDecimal(data["netAmt"]);
            saleModel.discAmt = Convert.ToDecimal(data["discAmt"]);
            saleModel.vatAmt = Convert.ToDecimal(data["vatAmt"]);
            saleModel.grossAmt = Convert.ToDecimal(data["grossAmt"]);
            saleModel.payMethod = data["payMethod"].ToString();
            saleModel.payCash = payCash;
            saleModel.payCard = data["payCard"] == null ? "0" : data["payCard"].ToString();
            saleModel.giftAmt = giftAmt;
            saleModel.return_ = Convert.ToDecimal(data["return_"]);
            saleModel.balance = (Convert.ToDecimal(data["balance"]) + dbBalance);
            saleModel.entryDate = Convert.ToDateTime(data["entryDate"]);
            saleModel.sPrice = Convert.ToDecimal(data["sPrice"]);
            saleModel.discType = data["discType"].ToString();
            saleModel.comment = data["comment"].ToString();
            saleModel.currentCash = Convert.ToDecimal(data["currentCash"]);
            saleModel.branchId = Convert.ToInt32(HttpContext.Current.Session["barnchId"]);
            saleModel.groupId = Convert.ToInt32(HttpContext.Current.Session["groupId"]);
            saleModel.salesPersonId = Convert.ToInt32(data["salesPersonId"]);
            saleModel.cardId = data["cardId"] == null ? '0' : Convert.ToInt32(data["cardId"]);
            saleModel.bankId = data["bankId"] == null ? '0' : Convert.ToInt32(data["bankId"]);
            saleModel.warranty = data["warranty"].ToString();
            saleModel.token = data["token"] == null ? " " : data["token"].ToString();
            saleModel.CusType = data["cusType"] == null ? 0 : Convert.ToInt32(data["cusType"]);
            saleModel.MaturityDate = commonFunction.GetCurrentTime();
            //MaturityDate = data["maturityDate"] == null ? DateTime.Now : data["maturityDate"].Value<DateTime>();
            saleModel.checkNo = data["checkNo"] == null ? " " : data["checkNo"].ToString();
            saleModel.loadingCost = Convert.ToDecimal(data["loadingCost"]);
            saleModel.shippingCost = Convert.ToDecimal(data["shippingCost"]);
            saleModel.carryingCost = Convert.ToDecimal(data["carryingCost"]);
            saleModel.serviceCharge = Convert.ToDecimal(data["serviceCharge"]);
            saleModel.status = "Resold";
            saleModel.returnQty = data["returnQty"].ToString();
            saleModel.returnAmt = Convert.ToDecimal(data["returnAmt"]);
            saleModel.PreviousDue = Convert.ToDecimal(data["preDue"]);
            saleModel.storeId = HttpContext.Current.Session["storeId"].ToString();
            saleModel.extraDiscount = Convert.ToDecimal(data["extraDiscount"]);
            saleModel.refName = data["refName"].ToString();
            saleModel.refPhone = data["refPhone"].ToString();
            saleModel.refAddress = data["refAddress"].ToString();
            saleModel.referredBy = Convert.ToInt32(data["referredBy"]);
            saleModel.salesPersonId = Convert.ToInt32(data["salesPersonId"]);

            var removeImei = data["removeImei"].ToString();


            // Update SaleInfo
            var saleImei = dtBal.Rows[0]["imei"].ToString();
            if (saleImei != "")
            {
                string[] splitImei = removeImei.Split(',');
                for (int i = 0; i < splitImei.Length - 1; i++)
                {
                    saleImei = saleImei.Replace(splitImei[i].Trim() + ",", "");
                }
                saleModel.imei = saleImei.TrimStart(',').TrimEnd(',').Replace(",,", ",");

            }

            transactionQuery += "BEGIN ";
            transactionQuery += saleModel.updateSaleDataModel();
            transactionQuery += "END ";


            // Sale Return Qty 
            var billNoSaleReturn = data["billNo"].ToString();
            var prodIdSaleReturn = Convert.ToInt32(data["prodID"]);
            var dataReturnQty = data["returnQty"].ToString();

            //Sale Return update for balance qty 
            var lastQty = commonFunction.getLastStockQty(prodId, HttpContext.Current.Session["storeId"].ToString());
            stockStatusModel.balanceQty = commonFunction.calculateQty(prodId, lastQty, dataReturnQty, "+");

            var saleReturnQtyDb = "0";
            var dtSaleReturnQty = stockStatusModel.getStockStatusSaleReturnSumOfQtyModel(billNoSaleReturn, prodIdSaleReturn);
            if (dtSaleReturnQty.Rows.Count > 0 && dtSaleReturnQty.Rows[0][0].ToString() != "")
                saleReturnQtyDb = dtSaleReturnQty.Rows[0][0].ToString();

            if (!dataReturnQty.Contains("."))
            {
                dataReturnQty = dataReturnQty + ".0";
            }

            if (!saleReturnQtyDb.Contains("."))
            {
                saleReturnQtyDb = saleReturnQtyDb + ".0";
            }

            var inputReturnQtyOnly = dataReturnQty.Split('.')[0];
            var inputReturnPieceOnly = dataReturnQty.Split('.')[1];

            var salereturnQtyOnly = saleReturnQtyDb.Split('.')[0];
            var salereturnPieceOnly = saleReturnQtyDb.Split('.')[1];

            var returnQty = Convert.ToDecimal(inputReturnQtyOnly) - Convert.ToDecimal(salereturnQtyOnly);

            var totalPiece = 0;
            if (Convert.ToInt32(inputReturnPieceOnly) < Convert.ToInt32(salereturnPieceOnly))
            {
                var ratio = commonFunction.getRatioByProductId(prodId);
                returnQty -= 1;
                totalPiece = (ratio + Convert.ToInt32(inputReturnPieceOnly)) - Convert.ToInt32(salereturnPieceOnly);

            }
            else
            {
                totalPiece = Convert.ToInt32(inputReturnPieceOnly) - Convert.ToInt32(salereturnPieceOnly);
            }
            string totalReturnQty = returnQty + "." + totalPiece;

            if (Convert.ToDecimal(totalReturnQty) > 0)
            {
                var statusSaleReturn = "saleReturn";
                stockStatusModel.returnImei = removeImei.TrimEnd(',');
                var searchType = data["searchType"].ToString();

                var prodCodes = data["prodCodes"].ToString();
                if (prodCodes == "")
                {
                    stockStatusModel.billNo = billNo;
                    stockStatusModel.prodId = Convert.ToInt32(prodIdSaleReturn);
                    stockStatusModel.status = statusSaleReturn;
                    stockStatusModel.qty = totalReturnQty.ToString();
                    transactionQuery += "BEGIN ";
                    transactionQuery += stockStatusModel.saveStockStatusInfoListForSaleReturn();
                    transactionQuery += "END ";

                }
                else
                {
                    stockStatusModel.billNo = billNo;
                    stockStatusModel.prodId = Convert.ToInt32(prodIdSaleReturn);
                    stockStatusModel.status = "packageReturn";
                    stockStatusModel.qty = totalReturnQty;
                    transactionQuery += "BEGIN ";
                    transactionQuery += stockStatusModel.saveStockStatusInfoListForSaleReturn();
                    transactionQuery += "END ";

                    transactionQuery += packageItemReturn(billNo, prodCodes, totalReturnQty, statusSaleReturn);
                }
                // Sales return qty update
                transactionQuery += "BEGIN ";
                transactionQuery += saleModel.updateSaleDataModel();
                transactionQuery += "END ";

                var saleUpsert = new SaleUpsert();
                transactionQuery += saleUpsert.returnSaleOrderQty(data);
                // comment return
               // return transactionQuery;

            }

            var slipModel = new SlipModel();

            string executeCounter = data["executeCounter"].ToString();
            if (executeCounter == "0")
            {
                slipModel.billNo = data["billNo"].ToString();
                slipModel.status = "Resold";
                slipModel.entryDate = Convert.ToDateTime(data["entryDate"]);
                slipModel.storeId = HttpContext.Current.Session["storeId"].ToString();
                transactionQuery += "BEGIN ";
                transactionQuery += slipModel.saveSlipDataModel();
                transactionQuery += "END ";


                var saleUpsert = new SaleUpsert();
                transactionQuery += saleUpsert.createCashReportData(data);
            }

            // Instalment
            string reminderId = data["reminderId"].ToString();
            var reminderBillNo = data["billNo"].ToString();
            var reminderCustomer = new InstallmentCustomer();

            transactionQuery += reminderCustomer.ChangePayStatus(reminderBillNo, payCash, reminderId);


            return transactionQuery;
        }





        public string packageItemReturn(string billNo, string prodCodes, string totalReturnQty, string statusSaleReturn)
        {
            var splitProdCodes = prodCodes.Split(';');
            var transactionQuery = "";
            for (int i = 0; i < splitProdCodes.Length - 1; i++)
            {
                var prodId = splitProdCodes[i];

                stockStatusModel.billNo = billNo;
                stockStatusModel.prodId = Convert.ToInt32(prodId);
                stockStatusModel.status = statusSaleReturn;
                stockStatusModel.qty = totalReturnQty.ToString();
                transactionQuery += "BEGIN ";
                transactionQuery += stockStatusModel.saveStockStatusInfoListForSaleReturn();
                transactionQuery += "END ";
            }
            return transactionQuery;
        }





        public string updateDueAdjustment(string jsonStrData)
        {
            try
            {
                var returnAmt = "";
                var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

                string cusId = data["cusId"].Value<string>();
                decimal changeAmt = data["changeAmt"].Value<decimal>();
                decimal payCash = data["payCash"].Value<decimal>();
                decimal grossAmt = data["grossAmt"].Value<decimal>();
                decimal returnCartAmt = data["returnAmt"].Value<decimal>();
                decimal paidReturnAmt = data["paidReturnAmt"].Value<decimal>();
                decimal preDue = data["preDue"].Value<decimal>();



                DataSet ds = customerModel.getCustomerByCondition("cusID='" + cusId + "'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    decimal totalPaid = Convert.ToDecimal(ds.Tables[0].Rows[0][8].ToString());
                    decimal openingDue = Convert.ToDecimal(ds.Tables[0].Rows[0][22].ToString());
                    decimal totalDue = preDue - openingDue;

                    //totalPaid += changeAmt;


                    decimal currentDue = payCash - grossAmt;

                    //if (currentDue <= 0)
                    //    return "";


                    if (totalDue > currentDue)
                    {
                        totalDue -= currentDue;
                        returnAmt = totalDue.ToString();
                    }
                    else
                    {
                        openingDue -= (currentDue - totalDue);
                        //openingDue -= (returnCartAmt - paidReturnAmt);
                        totalDue = 0;

                        if (openingDue < 0)
                        {
                            openingDue = 0;
                        }
                        returnAmt = openingDue.ToString();
                    }

                    Dictionary<string, string> dicCustomerFormatedItemData = new Dictionary<string, string>();
                    //dicCustomerFormatedItemData.Add("totalPaid", totalPaid.ToString());
                    dicCustomerFormatedItemData.Add("totalDue", totalDue.ToString());
                    dicCustomerFormatedItemData.Add("openingDue", openingDue.ToString());

                    var getFormatUpdateItemData = objCommonController.getUpdateParameter(dicCustomerFormatedItemData);

                    customerModel.updateCustomer(getFormatUpdateItemData, "cusID='" + cusId + "'");
                }

                return returnAmt;
            }
            catch (Exception)
            {
                return "false";
            }
        }



        public bool updateCustomerAdvance(string jsonStrData)
        {
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            string cusId = data["cusId"].Value<string>();
            decimal advanceAmt = data["advanceAmt"].Value<decimal>();

            bool isSuccess = customerModel.updateCustomerAdvance(cusId, advanceAmt);
            return isSuccess;
        }

        public string saveSaleSlipInfoList(string jsonStrData)
        {
            var transactionQuery = "";
            var data = (JObject)JsonConvert.DeserializeObject(jsonStrData);

            // 
            var slipModel = new SlipModel();
            slipModel.billNo = data["billNo"].ToString();
            slipModel.prodId = data["prodID"].ToString();
            slipModel.status = "Sold";
            slipModel.entryDate = Convert.ToDateTime(data["entryDate"]);
            slipModel.storeId = HttpContext.Current.Session["storeId"].ToString();
            slipModel.referredBy = data["referredBy"].ToString();

            var miscCost = Convert.ToDecimal(data["loadingCost"]) + Convert.ToDecimal(data["unloadingCost"]) +
                           Convert.ToDecimal(data["serviceCharge"]) + Convert.ToDecimal(data["shippingCost"]) +
                           Convert.ToDecimal(data["carryingCost"]);
            slipModel.miscCost = miscCost;


            transactionQuery += "BEGIN ";
            transactionQuery += slipModel.saveSlipDataModel();
            transactionQuery += "END ";

            return transactionQuery;
        }



        public string saveSaleWithSlipData(dynamic data)
        {

            string inputQty = data["qty"].ToString();

            string billNo = data["billNo"].ToString();
            string prodId = data["prodID"].ToString();
            DateTime entryDate = new DateTime();

            try
            {
                entryDate = (DateTime)data["entryDate"];
            }
            catch (Exception)
            {
                entryDate = commonFunction.GetCurrentTime();
            }


            DataTable dtBal = saleModel.getSaleLsitModel(billNo, prodId);

            decimal dbBalance = 0;
            if (dtBal.Rows.Count > 0)
            {
                dbBalance = Convert.ToDecimal(dtBal.Rows[0]["balance"]);
            }

            var payCashOne = Convert.ToDecimal(data["payCash"].ToString() == "" ? "0" : data["payCash"].ToString());
            decimal payCashTwo = Convert.ToDecimal(data["payCashTwo"].ToString() == "" ? "0" : data["payCashTwo"].ToString());
            decimal payCash = payCashOne + payCashTwo;
            decimal grossAmt = Convert.ToDecimal(data["grossAmt"].ToString());
            decimal preDue = Convert.ToDecimal(data["preDue"].ToString());
            var isAdvanced = Convert.ToBoolean(data["isAdvance"]);
            decimal totalDue = grossAmt + preDue;

            if (!isAdvanced && payCash > totalDue)
            {
                payCash = totalDue;
            }

            // get invoice gift amt


            var giftAmt = getInvoiceGiftAmt(billNo, grossAmt, payCash, dbBalance);

            saleModel.billNo = data["billNo"].ToString();
            saleModel.roleId = HttpContext.Current.Session["roleId"].ToString();
            saleModel.cusID = data["cusId"].ToString();
            saleModel.prodID = data["prodID"].ToString();
            saleModel.qty = data["qty"].ToString();
            saleModel.serialNo = data["serialNo"].ToString();
            saleModel.invoiceType = data["invoiceType"].ToString();
            saleModel.netAmt = Convert.ToDecimal(data["netAmt"]);
            saleModel.discAmt = Convert.ToDecimal(data["discAmt"]);
            saleModel.vatAmt = Convert.ToDecimal(data["vatAmt"]);
            saleModel.grossAmt = Convert.ToDecimal(data["grossAmt"]);
            saleModel.payMethod = data["payMethod"].ToString();
            saleModel.payCash = payCashOne + payCashTwo;
            saleModel.payCard = data["payCard"] == null ? "0" : data["payCard"].ToString();
            saleModel.giftAmt = giftAmt;
            saleModel.return_ = Convert.ToDecimal(data["return_"]);
            saleModel.balance = payCash;
            saleModel.entryDate = entryDate;
            saleModel.sPrice = Convert.ToDecimal(data["sPrice"]);
            saleModel.discType = data["discType"].ToString();
            saleModel.comment = data["comment"].ToString();
            saleModel.currentCash = Convert.ToDecimal(data["currentCash"]);
            saleModel.branchId = Convert.ToInt32(HttpContext.Current.Session["barnchId"]);
            saleModel.groupId = Convert.ToInt32(HttpContext.Current.Session["groupId"]);
            saleModel.salesPersonId = Convert.ToInt32(data["salesPersonId"]);
            saleModel.referredBy = Convert.ToInt32(data["referredBy"]);
            saleModel.cardId = data["cardId"] == null ? 0 : Convert.ToInt32(data["cardId"]);
            saleModel.bankId = data["bankId"] == null ? '0' : Convert.ToInt32(data["bankId"]);
            saleModel.warranty = data["warranty"].ToString();
            saleModel.token = data["token"] == null ? " " : data["token"].ToString();
            saleModel.CusType = data["cusType"] == null ? 0 : Convert.ToInt32(data["cusType"]);
            saleModel.MaturityDate = entryDate;
            saleModel.checkNo = data["checkNo"] == null ? "" : data["checkNo"].ToString();
            saleModel.loadingCost = Convert.ToDecimal(data["loadingCost"]);
            saleModel.unloadingCost = Convert.ToDecimal(data["unloadingCost"]);
            saleModel.serviceCharge = Convert.ToDecimal(data["serviceCharge"]);
            saleModel.shippingCost = Convert.ToDecimal(data["shippingCost"]);
            saleModel.carryingCost = Convert.ToDecimal(data["carryingCost"]);
            saleModel.salePersonType = data["salePersonType"].ToString();
            saleModel.returnQty = data["returnQty"].ToString();
            saleModel.returnAmt = Convert.ToDecimal(data["returnAmt"]);
            saleModel.PreviousDue = Convert.ToDecimal(data["preDue"]);
            saleModel.interestRate = Convert.ToInt32(data["interestRate"]);
            saleModel.interestAmt = Convert.ToDecimal(data["interestAmt"]);
            saleModel.searchType = data["searchType"].ToString();
            saleModel.extraDiscount = Convert.ToDecimal(data["extraDiscount"]);
            saleModel.imei = data["imei"].ToString().Replace('×', ',').Replace(" ", string.Empty);
            saleModel.storeId = HttpContext.Current.Session["storeId"].ToString();
            saleModel.refName = data["refName"].ToString();
            saleModel.refPhone = data["refPhone"].ToString();
            saleModel.refAddress = data["refAddress"].ToString();
            saleModel.vatType = data["vatType"].ToString();



            if (commonFunction.findSettingItemValueDataTable("autoSalesPerson") == "1")
            {
                saleModel.isAutoSalesPerson = true;
            }
            
            var transactionQuery = "";
            transactionQuery += "BEGIN ";
            transactionQuery += saleModel.saveSaleDataListModel();
            transactionQuery += "END ";

            string executeCounter = data["executeCounter"].ToString();
            if (executeCounter == "0")
            {

                saleModel.billNo = data["billNo"].ToString();
                saleModel.prodID = data["prodID"].ToString();
                saleModel.status = data["opt"].ToString();
                //saleModel.status = "Sold";
                saleModel.storeId = HttpContext.Current.Session["storeId"].ToString();
                saleModel.referredBy = Convert.ToInt32(data["referredBy"]);

                var miscCost = Convert.ToDecimal(data["loadingCost"]) + Convert.ToDecimal(data["unloadingCost"]) +
                               Convert.ToDecimal(data["serviceCharge"]) + Convert.ToDecimal(data["shippingCost"]) +
                               Convert.ToDecimal(data["carryingCost"]);
                saleModel.miscCost = miscCost;
                saleModel.entryDate = entryDate;

                transactionQuery += "BEGIN ";
                transactionQuery += saleModel.saveSlipDataModel();
                transactionQuery += "END ";
            }

            return transactionQuery;
        }




        public string updateSaleOrderQty(dynamic data)
        {
            string productId = data["prodID"].ToString();
            var qtyManagement = new QtyManagement();
            qtyManagement.ProductId = productId;
            qtyManagement.StoreId = HttpContext.Current.Session["storeId"].ToString();
            qtyManagement.operationalQty = data["qty"].ToString();
            var stockCurrentQty = qtyManagement.subtractStockQty();

            var stockModel = new StockModel();
            stockModel.prodId = Convert.ToInt32(productId);
            stockModel.storeId = Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
            stockModel.qty = stockCurrentQty;

            string transactionQuery = "BEGIN ";
            transactionQuery += stockModel.updateStockQtyWihoutExecute();
            transactionQuery += " END ";

            return transactionQuery;
        }



        public string returnSaleOrderQty(dynamic data)
        {
            string productId = data["prodID"].ToString();
            var qtyManagement = new QtyManagement();
            qtyManagement.ProductId = productId;
            qtyManagement.StoreId = HttpContext.Current.Session["storeId"].ToString();
            qtyManagement.operationalQty = data["returnQty"].ToString();
            var stockCurrentQty = qtyManagement.addStockQty();

            var stockModel = new StockModel();
            stockModel.prodId = Convert.ToInt32(productId);
            stockModel.storeId = Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
            stockModel.qty = stockCurrentQty;

            string transactionQuery = "BEGIN ";
            transactionQuery += stockModel.updateStockQtyWihoutExecute();
            transactionQuery += " END ";

            return transactionQuery;
        }


        public string suspendSaleOrderQty(string billNo)
        {
            var dtStockStatus = stockStatusModel.getStockStatusDataListModel(billNo);
            string queryTrans = "";
            foreach (DataRow data in dtStockStatus.Rows)
            {
                string productId = data["prodID"].ToString();

                var returnQty = returnSaleOrderQty(productId, billNo);
                var saleQty = data["qty"].ToString();

                //var currentSaleQty = Convert.ToDecimal(saleQty) - Convert.ToDecimal(returnQty);
                var qtyManagement = new QtyManagement();
                qtyManagement.ProductId = productId;
                qtyManagement.StoreId = HttpContext.Current.Session["storeId"].ToString();
                qtyManagement.mainOperationalQty = saleQty;
                qtyManagement.operationalQty = returnQty;
                var currentSaleQty = qtyManagement.substractQtyFromQty();

                //var qtyManagement = new QtyManagement();
                qtyManagement.ProductId = productId;
                qtyManagement.StoreId = HttpContext.Current.Session["storeId"].ToString();
                qtyManagement.operationalQty = currentSaleQty.ToString();
                var stockCurrentQty = qtyManagement.addStockQty();

                var stockModel = new StockModel();
                stockModel.prodId = Convert.ToInt32(productId);
                stockModel.storeId = Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
                stockModel.qty = stockCurrentQty;

                queryTrans += "BEGIN ";
                queryTrans += stockModel.updateStockQtyWihoutExecute();
                queryTrans += " END ";
            }

            return queryTrans;
        }


        public string returnSaleOrderQty(string productId, string billNo)
        {
            SaleModel saleModel = new SaleModel();
            saleModel.prodID = productId;
            saleModel.billNo = billNo;
            var dtReturnQty = saleModel.getSaleProductQtyModel();
            int qty = 0, pice = 0;
            if (dtReturnQty.Rows.Count > 0)
            {
                for (int i = 0; i < dtReturnQty.Rows.Count; i++)
                {
                    string stockQty = dtReturnQty.Rows[i][0].ToString();
                    if (stockQty.Contains("."))
                    {
                        string[] splitQty = stockQty.Split('.');
                        qty += Convert.ToInt32(splitQty[0]);
                        pice += Convert.ToInt32(splitQty[1]);
                    }
                    else
                    {
                        qty += Convert.ToInt32(stockQty);
                    }
                }
            }
            return qty + "." + pice;
        }
    }


}