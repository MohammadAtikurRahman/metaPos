using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;
using MetaPOS.Api.Models;

namespace MetaPOS.Api.Service
{
    public class ProductService : Product
    {

        private CommonFunction commonFunction = new CommonFunction();

        // // GET: products
        public List<object> ProductList()
        {
            var ProductData = new List<object>();

            try
            {
                ProductModel productModel = new ProductModel();
                string host = HttpContext.Current.Request.Url.Host;
                var subdomain = host;
                host.ToLower();
                host.Replace("https://", string.Empty);
                host.Replace("http://", string.Empty);
                if (host.Contains('.'))
                {
                   subdomain = host.Split('.')[0];
                }
                productModel.shopName = subdomain;
                var dtProduct = productModel.ProductDataTableModel();
                //var products = new List<object>();
                for (int i = 0; i < dtProduct.Rows.Count; i++)
                {
                    ProductData.Add(new Product()
                    {
                        productId = dtProduct.Rows[i]["prodID"].ToString(),
                        prodName = dtProduct.Rows[i]["prodName"].ToString(),
                        salePrice = dtProduct.Rows[i]["sPrice"].ToString(),
                        qty = dtProduct.Rows[i]["stockQty"].ToString()
                    });
                }


                ProductData.Add(ProductData);

            }
            catch (Exception)
            {
                ProductData.Add("No data was fount!");
                //statusData.Add(new DataStatus() { status = "403" });
            }

            return ProductData;
        }



        // GET: Search products with prodName
        public List<object> ProductList(string productName)
        {
            var ProductData = new List<object>();

            try
            {
                ProductModel productModel = new ProductModel();
                string host = HttpContext.Current.Request.Url.Host;
                var subdomain = host;
                host.ToLower();
                host.Replace("https://", string.Empty);
                host.Replace("http://", string.Empty);
                if (host.Contains('.'))
                {
                    subdomain = host.Split('.')[0];
                }
                productModel.shopName = subdomain;
                var dtProduct = productModel.ProductDataTableModel();
                //var products = new List<object>();
                for (int i = 0; i < dtProduct.Rows.Count; i++)
                {
                    if (productName == dtProduct.Rows[i]["prodName"].ToString())
                    {
                        ProductData.Add(new Product()
                        {
                            productId = dtProduct.Rows[i]["prodID"].ToString(),
                            prodName = dtProduct.Rows[i]["prodName"].ToString(),
                            salePrice = dtProduct.Rows[i]["sPrice"].ToString(),
                            qty = dtProduct.Rows[i]["stockQty"].ToString()
                        });
                    }
                }



                ProductData.Add(ProductData);

            }
            catch (Exception)
            {
                ProductData.Add("No data was fount!");
                //statusData.Add(new DataStatus() { status = "403" });
            }

            return ProductData;
        }



        // Post: sale product
        public List<DataStatus> ProductListPost(SoldProduct soldProduct )
        {
            CustomerModel customerModel = new CustomerModel();
            ProductModel productModel = new ProductModel();
            var statusData = new List<DataStatus>();
            string host = HttpContext.Current.Request.Url.Host;
            var subdomain = host;
            host.ToLower();
            host.Replace("https://", string.Empty);
            host.Replace("http://", string.Empty);
            if (host.Contains('.'))
            {
                subdomain = host.Split('.')[0];
            }
            
            if (!commonFunction.CheckConnectionString(subdomain))
            {
                statusData.Add(new DataStatus() { status = "404" });
                return statusData;
            }
            string billNo = generateSaleId(subdomain);
            string cusID = customerModel.generateCusId(subdomain);
            productModel.shopName = subdomain;
            var dtProduct = productModel.getStockDataListModelByProdID(soldProduct.productId);
            int storeId = Convert.ToInt32(dtProduct.Rows[0]["warehouse"]);
            int roleId = Convert.ToInt32(dtProduct.Rows[0]["roleID"]);
            int branchId = Convert.ToInt32(dtProduct.Rows[0]["branchId"]);
            int groupId = Convert.ToInt32(dtProduct.Rows[0]["groupId"]);
            try
            { 
                
                productModel.prodName = soldProduct.prodName;
                productModel.productId = soldProduct.productId;
                productModel.sPrice = soldProduct.salePrice;
                productModel.qty = soldProduct.qty;

                //int storeId = Convert.ToInt32(dtProduct.Rows[0]["warehouse"]);

                var lastQty = commonFunction.getLastStockQty(soldProduct.productId, storeId);//HttpContext.Current.Session["storeId"].ToString()
                if (!lastQty.Contains("."))
                {
                    lastQty = lastQty + ".0";
                }

                var balanceQty = commonFunction.calculateQty(soldProduct.productId, lastQty, soldProduct.qty, "-");
                if (!balanceQty.Contains("."))
                {
                    balanceQty = balanceQty + ".0";
                }
                productModel.lastQty = lastQty;
                productModel.balanceQty = balanceQty;
                foreach (DataRow row in dtProduct.Rows)
                {
                    productModel.prodDescr = row["prodDescr"].ToString();
                    productModel.prodCode = row["prodCode"].ToString();
                    //productModel.prodName = row["prodName"].ToString();
                    //productModel.prodDescr = row["prodDescr"].ToString();
                    productModel.supCompany = row["supCompany"].ToString();
                    productModel.catName = row["catName"].ToString();
                   // productModel.qty = soldQty;
                    productModel.serialNo = "";
                    productModel.payType = "Cash";
                    productModel.payDescr = "Cash";
                    productModel.invoiceType = "0";
                    productModel.bPrice = (decimal)row["bprice"];
                    productModel.weight = (decimal)row["weight"];
                    productModel.size = "";
                    productModel.discount = "0";
                    productModel.stockTotal = (decimal)row["stockTotal"];
                    productModel.status = "sale";
                    productModel.entryDate = commonFunction.GetCurrentTime().ToString("MM-dd-yyyy");
                    productModel.entryDate = commonFunction.GetCurrentTime().ToString("MM-dd-yyyy");
                    productModel.entryQty = row["qty"].ToString();
                    productModel.title = row["title"].ToString();
                    productModel.roleId = (int)row["roleID"];
                    productModel.billNo = billNo;
                    productModel.branchId = (int)row["branchId"];
                    productModel.groupId = (int)row["groupId"];
                    productModel.fieldAttribute = "";
                    productModel.tax = row["tax"].ToString();
                    productModel.sku = row["sku"].ToString();
                    productModel.productSource = "";
                    productModel.prodCodes = row["prodCode"].ToString();//prodCodes;
                    productModel.imei = "";//data["imei"].ToString().Replace('×', ',').Replace(" ", "").TrimEnd(',').TrimStart(',');
                    productModel.fieldId = "";
                    productModel.attributeId = "";
                    productModel.supCommission = Convert.ToDecimal(row["commission"]);
                    productModel.dealerPrice = Convert.ToDecimal(row["dealerPrice"]);
                    productModel.createdFor = row["createdFor"].ToString();
                    productModel.unitId = row["unitId"].ToString();
                    productModel.isPackage = false;
                    productModel.engineNumber = row["engineNumber"].ToString();
                    productModel.cecishNumber = row["cecishNumber"].ToString();
                    productModel.searchType = "product";//data["searchType"].ToString();
                    productModel.storeId = storeId;//row["warehouse"].ToString();//HttpContext.Current.Session["storeId"].ToString();
                    productModel.offer = "0";
                    productModel.offerType = "";
                    productModel.isOfferQty = false;
                    productModel.fieldRecord = "0";//data["fieldRecord"].ToString();
                    productModel.attributeRecord = "0"; //data["attributeRecord"].ToString();
                    productModel.salesPersonId = "0";
                    productModel.referredBy = "0";
                }
               
                productModel.productSeleDataModel();
            }
            catch (Exception)
            {
                statusData.Add(new DataStatus() { status = "403" });
            }

            //seleInfo
            try
            {
                //string billNo = generateSaleId();//data["billNo"].Value<string>();

                //same work here
                DataTable dtBal = productModel.getSaleLsitModel(billNo, soldProduct.productId);

                decimal dbBalance = 0;
                if (dtBal.Rows.Count > 0)
                {
                    dbBalance = Convert.ToDecimal(dtBal.Rows[0]["balance"]);
                }

                var payCashOne = Convert.ToDecimal(soldProduct.salePrice);//Convert.ToDecimal(data["payCash"].Value<string>() == "" ? "0" : data["payCash"].Value<string>());
                var payCashTwo = 0.00M;//Convert.ToDecimal(data["payCashTwo"].Value<string>() == "" ? "0" : data["payCashTwo"].Value<string>());
                var payCash = payCashOne + payCashTwo;
                var grossAmt = Convert.ToDecimal(soldProduct.salePrice);//data["grossAmt"].Value<decimal>();
                var preDue = 0.00M;//data["preDue"].Value<decimal>();
                var isAdvanced = false;//data["isAdvance"].Value<bool>();
                var totalDue = grossAmt + preDue;

                if (!isAdvanced && payCash > totalDue)
                {
                    payCash = totalDue;
                }


                productModel.billNo = billNo;//data["billNo"].Value<string>();
                productModel.roleId = roleId;//HttpContext.Current.Session["roleId"].ToString();
                productModel.cusID = cusID;//data["cusId"].Value<string>();
                productModel.productId = soldProduct.productId;//data["prodID"].Value<string>();
                productModel.qty = soldProduct.qty;//data["qty"].Value<string>();
                productModel.serialNo = "";//data["serialNo"].Value<string>();
                productModel.invoiceType = "0";//data["invoiceType"].Value<string>();
                productModel.netAmt = soldProduct.salePrice;//data["netAmt"].Value<decimal>();
                productModel.discAmt = 0.00M;//data["discAmt"].Value<decimal>();
                productModel.vatAmt = 0.00M;//data["vatAmt"].Value<decimal>();
                productModel.grossAmt = grossAmt;//data["grossAmt"].Value<decimal>();
                productModel.payMethod = "0";//data["payMethod"].Value<string>();
                productModel.payCash = payCashOne + payCashTwo;
                productModel.payCard = "0.00";//data["payCard"] == null ? "0" : data["payCard"].ToString();
                productModel.giftAmt = getInvoiceGiftAmt(billNo, grossAmt, payCash, dbBalance);
                productModel.return_ = 0.00M;//data["return_"].Value<decimal>();
                productModel.balance = payCash;
                productModel.entryDate = commonFunction.GetCurrentTime().ToString();
                productModel.sPrice = soldProduct.salePrice;//data["sPrice"].Value<decimal>();
                productModel.discType = "";//data["discType"].Value<string>();
                productModel.comment = "";//data["comment"].Value<string>();
                productModel.currentCash = payCash;//data["currentCash"].Value<decimal>();
                productModel.branchId = branchId;//Convert.ToInt32(HttpContext.Current.Session["barnchId"]);
                productModel.groupId = groupId;//Convert.ToInt32(HttpContext.Current.Session["groupId"]);
                productModel.salesPersonId = "0";//data["salesPersonId"].Value<int>();
                productModel.referredBy = "0";//data["referredBy"].Value<int>();
                productModel.cardId = "0";//data["cardId"] == null ? '0' : data["cardId"].Value<int>();
                productModel.bankId = 0;//data["bankId"] == null ? '0' : data["bankId"].Value<int>();
                productModel.warranty = "";//data["warranty"].Value<string>();
                productModel.token = "";//data["token"] == null ? " " : data["token"].Value<string>();
                productModel.CusType = 0;//data["cusType"] == null ? 0 : data["cusType"].Value<int>();
                productModel.MaturityDate = commonFunction.GetCurrentTime();
                productModel.checkNo = "";//data["checkNo"] == null ? " " : data["checkNo"].Value<string>();
                productModel.loadingCost = 0;//data["loadingCost"].Value<decimal>();
                productModel.unloadingCost = 0;//data["unloadingCost"].Value<decimal>();
                productModel.serviceCharge = 0;//data["serviceCharge"].Value<decimal>();
                productModel.shippingCost = 0;//data["shippingCost"].Value<decimal>();
                productModel.carryingCost = 0;//data["carryingCost"].Value<decimal>();
                productModel.salePersonType = "0";//data["salePersonType"].Value<string>();
                productModel.returnQty = "0.0";//data["returnQty"].Value<string>();
                productModel.returnAmt = 0.00M;//data["returnAmt"].Value<decimal>();
                productModel.entryDate = commonFunction.GetCurrentTime().ToString();//data["entryDate"].Value<DateTime>();
                productModel.PreviousDue = 0.00M;//data["preDue"].Value<decimal>();
                productModel.interestRate = 0;//data["interestRate"].Value<int>();
                productModel.interestAmt = 0.00M;//data["interestAmt"].Value<decimal>();
                productModel.searchType = "product";//data["searchType"].Value<string>();
                productModel.extraDiscount = 0.00M;//data["extraDiscount"].Value<decimal>();
                productModel.imei = "";//data["imei"].ToString().Replace('×', ',').Replace(" ", string.Empty);
                productModel.storeId = storeId;//HttpContext.Current.Session["storeId"].ToString();
                productModel.refName = "0";//data["refName"].Value<string>();
                productModel.refPhone = "0";//data["refPhone"].Value<string>();
                productModel.refAddress = "0";//data["refAddress"].Value<string>();
                productModel.vatType = "TK";//data["vatType"].Value<string>();
                productModel.isAutoSalesPerson = false;

                productModel.saveSaleInfoData();
            }
            catch (Exception)
            {
                statusData.Add(new DataStatus() { status = "403" });
            }

            //slipInfo

            try
            {
                productModel.saveSlipDataModel();

            }
            catch (Exception)
            {

                statusData.Add(new DataStatus() { status = "403" });
            }

            //cashReportInfo
            try
            {
                productModel.cashType = "Invoice";
                productModel.descr = cusID;
                productModel.billNo = billNo;
                productModel.cashIn = 0;
                productModel.cashOut = Convert.ToDecimal(soldProduct.salePrice);//grossAmt + totalDiscAmt; // gross amount + discount amount with extra discount
                productModel.status = "6";
                productModel.payType = "Cash";
                productModel.payDescr = "Cash";
                productModel.saveCustomerCashData();


                if (soldProduct.salePrice!="")
                {
                    // Sales transection
                    //commonFunction.cashTransactionSales(currentCash, 0, "Sales Payment", cusId, billNo, payMethod, "5", "0", dateTime);

                    //if (currentCash > grossAmt + preDue && !isAdvance)
                    //{
                    //    currentCash = grossAmt + preDue;
                    //}

                    productModel.cashType = "Sales Payment";
                    productModel.descr = cusID;
                    productModel.cashIn = Convert.ToDecimal(soldProduct.salePrice);//currentCash;
                    productModel.cashOut = 0;
                    productModel.cashInHand = 0;
                    productModel.entryDate = commonFunction.GetCurrentTime().ToString("MM-dd-yyyy"); //entryDate;
                    productModel.billNo = billNo;
                    productModel.mainDescr = "0";
                    productModel.roleId = roleId;//Convert.ToInt32(HttpContext.Current.Session["roleId"].ToString());
                    productModel.branchId = branchId;//Convert.ToInt32(HttpContext.Current.Session["branchId"].ToString());
                    productModel.groupId = groupId;//Convert.ToInt32(HttpContext.Current.Session["groupId"].ToString());
                    productModel.status = "5";
                    productModel.adjust = '0';
                    productModel.isSchedulePayment = false;
                    productModel.isScheduled = false;
                    productModel.isReceived = true;
                    productModel.trackAmt = 0M;
                    productModel.storeId = storeId;//Convert.ToInt32(HttpContext.Current.Session["storeId"].ToString());
                    productModel.payMethod = "0";
                    productModel.purchaseCode = "";//purchaseCode;
                    productModel.payType = "Cash";
                    productModel.payDescr = "Cash";
                    productModel.cardType = "";
                    productModel.maturityDate = Convert.ToDateTime("01/01/2020");
                    productModel.bankName = "";
                    productModel.checkNo = "";

                    //if (payMethod == "7")
                    //{
                    //    saleCashReport.cardType = cardType;
                    //}
                    //else if (payMethod == "6")
                    //{
                    //    saleCashReport.maturityDate = Convert.ToDateTime(maturityDate);
                    //    saleCashReport.bankName = bankName;
                    //    saleCashReport.checkNo = checkNo;
                    //}
                    //else
                    //{
                    //    saleCashReport.cardType = "";
                    //    saleCashReport.maturityDate = Convert.ToDateTime("01/01/2000");
                    //    saleCashReport.bankName = "";
                    //    saleCashReport.checkNo = "";
                    //}
                    productModel.cashTransactionSales();



                    //Customer Cashin/ payment


                    productModel.cashType = "Payment";
                    productModel.descr = cusID;
                    productModel.billNo = billNo;
                    productModel.cashIn = Convert.ToDecimal(soldProduct.salePrice);//currentCash;
                    productModel.cashOut = 0;
                    productModel.status = "6";
                    productModel.payType = "Cash";
                    productModel.payDescr = "Cash";
                    productModel.saveCustomerCashData();

                }
            }
            catch (Exception)
            {
                statusData.Add(new DataStatus() { status = "403" });
            }

            //qtyManegement
            try
            {
                productModel.updateStockQtyWihoutExecute();

            }
            catch (Exception)
            {

                statusData.Add(new DataStatus() { status = "403" });
            }
            //insert customerrInfo
            try
            {
                productModel.nextCusId = cusID;
                productModel.name = "";
                productModel.phone = "";
                productModel.address = "";
                productModel.mailInfo = "";
                productModel.refName = "";
                productModel.refPhone = "";
                productModel.refAddress = "";
                productModel.designation = "";
                productModel.bloodGroup = "";
                productModel.age = "";
                productModel.sex = "";
                productModel.saveCustomerInfoModel();
            }
            catch (Exception)
            {

                statusData.Add(new DataStatus() { status = "403" });
            }


            return statusData;
        }



        // Create sale Id / billNo
        public string generateSaleId(string subdomain)
        {
            string billNo = string.Empty;
            string lastSaleId = commonFunction.getSaleLastID(subdomain);


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



    }
}
