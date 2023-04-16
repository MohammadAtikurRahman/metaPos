using System;
using System.Data;
using System.Globalization;
using System.Web;


namespace MetaPOS.Admin.DataAccess
{


    public class ReceiptInvoice
    {


        private CommonFunction objCommonFunction = new CommonFunction();
        private Controller.CommonController objCommonController = new Controller.CommonController();

        private SqlOperation objSql = new SqlOperation();
        //private DataSet ds, dsPreBill;
        private DataTable dt;

        //private string query = "", isPaidWatermark = "";

        public static string isCatagoryProduct = "", isPaymentHistory = "", isUnicode = "", printBillNo = "";
        public static string printBillingNo = "";

        // Invoice print 
        public string ReceiptPrint()
        {
            string printQuery = "";
            string content = "", ImgPath = "";


            printQuery = @"SELECT top 1 sale.billNo,sale.prodID, sale.qty, sale.sPrice, sale.netAmt, sale.discAmt,
                                sale.vatAmt, sale.grossAmt, sale.payMethod,sale.cusID,sale.returnAmt,
                                sale.payCash, sale.payCard, sale.giftAmt, sale.return_, sale.balance, 
                                sale.entryDate, sale.sPrice, sale.currentCash, sale.comment,
                                sale.loadingCost,sale.shippingCost,sale.carryingCost,sale.unloadingCost,sale.salePersonType,sale.returnQty,
                                service.name as serviceName, service.type as serviceType,
                                ci.name, ci.phone,ci.address,ci.mailInfo,ci.totalDue,ci.openingDue,   
                                si.prodName AS prodName, si.sPrice,si.catName,si.warranty, si.unitId,
                                staff.name AS staffName,
                                bi.branchName, bi.branchAddress, bi.branchPhone, 
                                bi.branchMobile, bi.invoiceFooterNote, bi.branchLogoPath,
                                cat.Id, cat.catName as CategoryName,
                                sup.supCompany as supCompany,
                                ri.title, stockstatus.searchType,
                                stockstatus.imei, pack.packageName AS packName,
                                CONCAT(pack.packageName , si.prodName) AS prodPackName,
                                (SELECT SUM(cashOut)-SUM(cashIn) FROM CashReportInfo where descr = sale.cusID AND status='6' ) as cusCurrentDue
                                FROM  SaleInfo AS sale
                                LEFT JOIN CustomerInfo AS ci ON ci.cusId = sale.cusID
                                LEFT JOIN stockInfo AS si ON si.ProdID = sale.prodID
                                LEFT JOIN StaffInfo AS staff ON sale.salesPersonId = staff.staffID
                                LEFT JOIN CategoryInfo AS cat ON si.catName = cat.Id
                                LEFT JOIN PackageInfo AS pack ON sale.prodID = pack.Id
                                LEFT JOIN StockStatusInfo as stockstatus ON stockstatus.prodID = sale.prodID
                                LEFT JOIN ServiceInfo AS service ON service.Id = stockstatus.prodID
                                LEFT JOIN supplierInfo as sup ON si.supCompany = sup.supId
                                LEFT JOIN RoleInfo as ri ON ri.roleId = sale.salesPersonId
                                LEFT JOIN BranchInfo AS bi ON " + HttpContext.Current.Session["storeId"] + " = bi.storeId " +
                             "WHERE sale.billNo = '" + printBillingNo + "' and stockstatus.billNo = '" + printBillingNo +
                             "'";

            dt = objSql.getDataTable(printQuery);


            int count = 0;
            //string paymentBy = "";
            string folderPath = HttpContext.Current.Server.MapPath("~/Img/Logo/");

            foreach (DataRow row in dt.Rows)
            {
                //check payment method

                //if (row["payMethod"].ToString() == "0")
                //    paymentBy = "Cash";
                //else if (row["payMethod"].ToString() == "1")
                //    paymentBy = "Card";
                //else if (row["payMethod"].ToString() == "2")
                //    paymentBy = "Check";
                //else if (row["payMethod"].ToString() == "3")
                //    paymentBy = "Bkash";
                //else if (row["payMethod"].ToString() == "4")
                //    paymentBy = "Deposit";
                //else if (row["payMethod"].ToString() == "5")
                //    paymentBy = "Cash On Delivery";


                if (count == 0)
                {
                    //Logo
                    DataSet dsImg =
                        objSql.getDataSet("SELECT * FROM BranchInfo WHERE Id = '" +
                                          HttpContext.Current.Session["roleId"] + "'");

                    string imgName = "";

                    if (dsImg.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsImg.Tables[0].Rows[0][10].ToString()))
                    {
                        imgName = dsImg.Tables[0].Rows[0][10].ToString();
                        ImgPath = "../../Img/Logo/" + imgName;
                    }
                    else
                    {
                        ImgPath = "";
                    }

                    content += @"<html><header>" +
                               @"<style>
                                    
                                .mainEmailBody { width: 95%;  height: auto; overflow: hidden; background: #fff; margin: 0 auto; padding:0 20px 0 20px; font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}
                                .mHead {width: 100%; overflow: hidden;}
                                .invoiceTitle{width:200px;color:#fff;font-size: 22px;float: left;position: absolute;}
                                .brand-name {width:100%; text-align:center; float:left}
                                .branchName {margin: 0 auto;font-size: 32px;font-weight: 900;}
                                .Invoice {width: 100% ; height:50px; text-align:center; padding-left: 36%;}
                                .invoiceText{text-transform: uppercase;background: url(../../Img/bg_black.png) ;color: #fff;font-size: 18px;padding: 4px 5px;float: left;margin-top: 5px; border:1px solid #000}
                                .mContentBody{width: 100%; height: 40px; padding-bottom:5px; margin-top: 5px; font-size:14px;}
                                .invoiceAddress{width:46%; font-size:14px;  background: #fff; color:#333;  padding: 0px 12px; float: left; line-height: 23px}
                                .invoiceOthers{width:28%; background: #fff; font-size:14px; color:#333;  padding: 0px 12px; float: right; text-align: left; line-height: 23px}
                                .ItemDetails{width: 100%; font-size:14px; color: #444;text-align: center;padding:12px;}
                                .pUprice{width: 35%;height: 20px;float: left;text-align: left;}
                                .pTotal{width:35%; height: 20px; float: left; text-align:right;}
                                .recipt-form{padding-left:10px;}
                                .recipt-form p{margin: none}
                                .border-recipt{border-bottom: 2px dotted #ccc; padding: 0 35px;}

                                    </style>"
                               + "</header><body>";
                    //<img src=" + folderPath + row["branchLogoPath"] + " style='width:120px;padding: 20px 0 0 15px; left:0;top:0;'/>" +
                    content += @"<div class='mainEmailBody' >
                                    <div class='mHead'>
                                        <div class='invoiceTitle'>
                                                <img src='" + ImgPath +
                               "' style='width:120px;padding: 20px 0 0 15px; left:0;top:0;'/>" +
                               @"</div>
                                        <div class='brand-name'>
                                            <h2 class='branchName'>" + row["branchName"] + "</h2> " +
                               "<div style='width: 60%; margin: 0 auto; '>" +
                               row["branchAddress"] +
                               row["branchPhone"] +
                               "," + row["branchMobile"] +
                               @"</div></div>
                                </div>";


                    content += @"<div class='Invoice'>
                                    <div class='invoiceTitle invoiceText'> 
                                             Money Receipt
                                         </div>
                                </div>";

                    content += @"<div class='mContentBody'  >
                                <div class='invoiceAddress' >" +
                               "<p style='margin:0'> Invoice No: " + row["billNo"] +
                               "</p><p style='margin:0'> Customer ID: " + row["cusID"] +
                               @"</p></div>
                                <div class='invoiceOthers' >
                                    <p style='margin:0'>Invoice Date: " +
                               Convert.ToDateTime(row["entryDate"]).ToString("dd/MMM/yyyy") +
                               "</P><p style='margin:0'> Invoice Time: " +
                               Convert.ToDateTime(row["entryDate"]).ToString("hh:mm tt", CultureInfo.InvariantCulture) +
                               "</p></div>" +
                               "</div>";
                }

                content += "<div style='width:100%;'>";

                dynamic receiptPrintAmt = 0M, receiptStatus = "cash";

                receiptPrintAmt = Convert.ToDecimal(row["currentCash"]);
                if (receiptPrintAmt <= 0)
                {
                    receiptStatus = "return";
                    receiptPrintAmt = Convert.ToDecimal(row["returnAmt"]);
                }

                if (receiptStatus == "cash")
                {
                    content += "<div class='recipt-form'>" +
                               @"<p>Recived from <span class='border-recipt'>" + row["name"] +
                               " </span> the amount of taka <span class='border-recipt'>" + receiptPrintAmt +
                               "</span> in word <span class='border-recipt'>" +
                               objCommonController.AmountToWord(Convert.ToInt32(receiptPrintAmt)) +
                               " </span> taka only. </p>" +
                               "</div></div>";
                }
                else if (receiptStatus == "return")
                {
                    content += "<div class='recipt-form'>" +
                               @"<p>Return To <span class='border-recipt'>" + row["name"] +
                               " </span> the amount of taka <span class='border-recipt'>" + receiptPrintAmt +
                               "</span> in word <span class='border-recipt'>" +
                               objCommonController.AmountToWord(Convert.ToInt32(receiptPrintAmt)) +
                               " </span> taka only. </p>" +
                               "</div></div>";
                }

                content += @"<div style='width: 100%;float: Left;margin-right: 0;'>";


                var previousDue = (Convert.ToDecimal(row["cusCurrentDue"]) + Convert.ToDecimal(row["payCash"])) - Convert.ToDecimal(row["grossAmt"]);

                content += @"<div style='width: 50%;float: Left;margin-right: 0; padding-bottom:20px;'>
                            <div class='ItemDetails'>" +
                           "<div class='pUprice' > Total Amount </div>" +
                           "<div class='pTotal' >" + row["netAmt"] + "</div>" +
                           "</div>";


                content += " <div class='ItemDetails'>" +
                           "<div class='pUprice' > Pre Balance </div>" +
                           "<div class='pTotal' >" + previousDue +
                           "</div>" +
                           "</div>";


                content += "<div class='ItemDetails'>" +
                           "<div class='pUprice'> Received </div>" +
                           "<div class='pTotal' >" + row["payCash"] +
                           "</div></div>";


                content += "<div class='ItemDetails'  >" +
                           "<div class='pUprice'  > Pay Amount </div>" +
                           "<div class='pTotal' >" + row["currentCash"] +
                           "</div></div>";

                content += "<div class='ItemDetails' >" +
                           "<div class='pUprice'  > Due Amount </div>" +
                           "<div class='pTotal' >" + row["giftAmt"] +
                           "</div></div> ";


                content += "</div>";

                content += "<div style='padding-top: 60px; font-size:14px;margin-top: 15px;'>" +
                           "<div style='width:18%;float:left;border-top:1px solid  #000;margin-left:10px;text-align:center;'>Customer Sign</div>" +
                           "<div style='width:18%;float:right;border-top:1px solid  #000;margin-right:20px;text-align:center;'>Manager Sign </div>" +
                           "</div>";


                content += "</div><br/><br/></body></html>";

                //content += "<table style='width:100%; border-bottom:1px solid #ccc;border-left:1px solid #ccc;border-right:1px solid #ccc;' >" +
                //    "<tr ><td style='width: 5%;text-align: left;padding-left: 10px;font-size: 14px;'>" + ++count;


                //content += "</td></tr></table>";
            }

            return content;
        }


    }


}