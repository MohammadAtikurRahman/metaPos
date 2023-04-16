using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Web;


namespace MetaPOS.Admin.DataAccess
{


    public class ElectronicInvoice
    {


        private CommonFunction obCommonFunction = new CommonFunction();

        private SqlOperation objSql = new SqlOperation();
        //private DataSet ds, dsPreBill;
        private DataTable dt;

        private string isPaidWatermark = "";//,query = "",;

        public static string isCatagoryProduct = "", isPaymentHistory = "", isUnicode = "", printBillNo = "";

        // Electronic Invoice
        public void ElectronicInvoicePrint(string email, string invoiceNo)
        {
            string printQuery = "";
            string content = "";

            printQuery = @"SELECT tps.billNo, tps.cusID, tps.prodID, tps.roleID, 
                            sale.qty, sale.sPrice, sale.netAmt, sale.discAmt, sale.vatAmt, sale.grossAmt, sale.payMethod, 
                            sale.payCash, sale.payCard, sale.giftAmt, sale.return_, sale.balance, 
                            sale.entryDate, sale.sPrice, sale.currentCash, sale.comment,
                            ci.name, ci.phone,ci.address,ci.mailInfo,ci.totalDue,   
                            si.prodName AS prodName, si.sPrice,si.catName,
                            staff.name AS staffName,
                            bi.branchName, bi.branchAddress, bi.branchPhone, bi.branchMobile, bi.invoiceFooterNote, bi.branchLogoPath,
                            cat.Id, cat.catName as CategoryName,
                            pack.packageName AS prodName,
                            CONCAT(pack.packageName , si.prodName) AS prodPackName
                            FROM TempPrintSaleInfo as tps
                            LEFT JOIN SaleInfo AS sale ON tps.billNo = sale.billNo AND tps.prodID = sale.prodID
                            LEFT JOIN CustomerInfo AS ci ON tps.cusId = ci.cusId
                            LEFT JOIN stockInfo AS si ON tps.prodId = si.ProdID
                            LEFT JOIN StaffInfo AS staff ON sale.salesPersonId = staff.staffID
                            LEFT JOIN CategoryInfo AS cat ON si.catName = cat.Id
                            LEFT JOIN PackageInfo AS pack ON sale.prodID = pack.Id
                            LEFT JOIN BranchInfo AS bi ON " + HttpContext.Current.Session["invoiceRoleId"] + " = bi.Id";

            dt = objSql.getDataTable(printQuery);


            int count = 0;
            string paymentBy = "", ImgPath = "";
            string folderPath = HttpContext.Current.Server.MapPath("~/Img/Logo/");

            foreach (DataRow row in dt.Rows)
            {
                //check payment method
                if (row["payMethod"].ToString() == "0")
                    paymentBy = "Cash";
                else if (row["payMethod"].ToString() == "1")
                    paymentBy = "Card";
                else if (row["payMethod"].ToString() == "2")
                    paymentBy = "Check";
                else if (row["payMethod"].ToString() == "3")
                    paymentBy = "Bkash";
                else if (row["payMethod"].ToString() == "4")
                    paymentBy = "Deposit";
                else if (row["payMethod"].ToString() == "5")
                    paymentBy = "Cash On Delivery";

                if (count == 0)
                {
                    //Logo
                    DataSet dsImg =
                        objSql.getDataSet("SELECT * FROM BranchInfo WHERE Id = '" +
                                          HttpContext.Current.Session["roleId"] + "'");

                    string imgName = "";

                    if (dsImg.Tables[0].Rows.Count > 0)
                    {
                        imgName = dsImg.Tables[0].Rows[0][10].ToString();
                        ImgPath = "../../Img/Logo/" + imgName;
                    }


                    content += @"<html><header></header><body>";
                    //<img src=" + folderPath + row["branchLogoPath"] + " style='width:120px;padding: 20px 0 0 15px; left:0;top:0;'/>" +
                    content += @"<div class='mainEmailBody' style='width: 95%; height: auto; overflow: hidden;  background: #fff; margin: 0 auto; padding:0 15px 0 20px; font-family: Arial, Helvetica Neue, Helvetica, sans-serif;'>
                                    <div class='mHead' style='width: 100%; height: 120px; '>
                                        <div class='invoiceTitle' style='width:20%;color:#fff;font-weight: bold;font-size: 22px;float: left;position: absolute;'>
                                                <img src='" + ImgPath +
                               "' style='width:120px;padding: 0px 0 0 15px; left:0;top:0;'/>" +
                               @"</div>
                                        <div class='brand-name' style='width:70%; text-align:center; float:left'>
                                            <h2 style='margin: 0 auto;font-size: 32px;font-weight: 900;'>" +
                               row["branchName"] + "</h2>" +
                               row["branchAddress"] +
                               row["branchPhone"] +
                               "," + row["branchMobile"] +
                               @"</div>
                                </div>";

                    content += @"<div style='width: 100% ; height:50px; text-align:center; padding-left: 36%;'>
                                    <div class='invoiceTitle invoiceText' style='text-transform: uppercase;color: #000;font-weight: bold;font-size: 18px;padding: 8px 20px;float: left;margin-top: 5px; border:1px solid #000'> 
                                             Customer Copy
                                         </div>
                                </div>";

                    content +=
                        @"<div class='mContentBody' font-size:14px; style='width: 100%; height: 125px; padding-bottom:5px; margin-top: 5px;'>
                                <div class='invoiceAddress' style='width:46%; font-size:14px;  background: #fff; color:#333;  padding: 0px 12px; float: left; border-right:1px solid #ccc; line-height: 23px' >" +
                        "<p style='margin:0'> Customer ID: " + row["cusID"] +
                        "</p><p style='margin:0'> Name: " + row["name"] +
                        "</p><p style='margin:0'> Address: " + row["address"] +
                        "</p><p style='margin:0'> Phone: " + row["phone"] +
                        "</p><p style='margin:0'> Email: " + row["mailInfo"] +
                        @"</p></div>
                                <div class='invoiceOthers' style='width:28%; background: #fff; font-size:14px; color:#333;  padding: 0px 12px; float: right; text-align: left; line-height: 23px'> 
                                  <p style='margin:0'> Invoice No: " + row["billNo"] +
                        "</p><p style='margin:0'>Invoice Date: " +
                        Convert.ToDateTime(row["entryDate"]).ToString("dd/MMM/yyyy") +
                        "</P><p style='margin:0'> Invoice Time: " +
                        obCommonFunction.GetCurrentTime().ToShortDateString() +
                        "</p><p style='margin:0'> Sold By: " + row["staffName"] +
                        "</p><p style='margin:0'> Payment By: " + paymentBy +
                        "</div>" +
                        "</div>";

                    content +=
                        @"<div class='ItemHead' style='width: 100%; font-size: 15px;  height: 30px; background: #eee; border: 1px solid #ccc; margin-top: 0px; color: #444; font-weight: bold; text-align: center; '>
                            <div class='SLno' style='width:5%; height: 10px; background: #eee; float: left; text-align: left;  padding: 8px 0px 10px 10px'> SL </div>
                            <div class='pDetails' style='width:47%; height: 10px; background: #eee; float: left;  text-align: left; padding: 8px 0px 10px 10px'> Product Details </div>
                            <div class='pQuantity' style='width:8%; height: 10px; background: #eee; float: left; padding: 8px 0px 10px 10px'> Qty </div>
                            <div class='pUprice' style='width: 17%;height: 10px;background: #eee;float: left;text-align: right;padding: 8px 0px 10px 10px;'> Unit Cost </div>
                            <div class='pTotal' style='width:10%; height: 10px; background: #eee; float: Right ; padding: 8px 0px 10px 10px'> Total </div>
                        </div>";
                }

                content +=
                    "<div class='ItemDetails' style='width: 100%;font-size: 14px; height: 24px; background: #f7f7f7; border: 1px solid #UpSertOptInvoice; color: #444;text-align: center; '>" +
                    "<div class='SLno' style='width:5%; height: 10px; float: left; text-align: left; padding: 4px 0px 10px 8px;'>" +
                    ++count +
                    "</div><div class='pDetails' style='width:47%; height: 10px;float: left;  text-align: left; padding: 4px 0px 10px 8px'>" +
                    row["prodPackName"] +
                    "</div><div class='pQuantity' style='width:8%; height: 10px; float: left;  padding: 4px 0px 10px 8px'>" +
                    row["qty"] +
                    "</div><div class='pUprice' style='width: 18%;height: 10px;float: left;text-align: right;padding: 4px 0px 10px 8px;';>" +
                    row["sPrice"] +
                    "</div><div class='pTotal' style='width: 10%;height: 10px;float: Right;text-align: right;padding: 4px 20px 10px 8px;'> " +
                    Convert.ToInt32(row["qty"])*Convert.ToInt32(row["sPrice"]) +
                    "</div></div>";

                if (count == dt.Rows.Count)
                {
                    content +=
                        "<div class='ItemDetails' style='width: 100%; font-size:14px; height: 20px; color: #444;text-align: center;padding-top:10px;'>" +
                        "<div class='pUprice' style='width:81%; height: 20px;  float: left;  padding: 0px 0px 0px 10px; text-align:right;'> Total Amount </div>" +
                        "<div class='pTotal' style='width:16%; height: 20px; float: left; padding: 0px 0px 0px 0px; text-align:right;'>" +
                        row["netAmt"] +
                        "</div>";

                    if (Convert.ToInt32(row["giftAmt"]) == 0)
                    {
                        content +=
                            "<h1 style='border-radius:10px;border:2px solid #000;padding:10px 15px;width:60px;height:30px;text-transform:uppercase;text-align:center;margin-left: 200px;transform: rotate(30deg);'>Paid</h1>";
                    }

                    content += "</div>";

                    isPaidWatermark = obCommonFunction.findSettingItemValue(18);
                    if (isPaidWatermark == "1" && Convert.ToInt32(row["giftAmt"]) == 0)
                    {
                        content +=
                            "<h1 style='border-radius: 10px;border: 2px solid #000;padding: 10px 25px 10px 15px;width: 60px;height: 30px;position: absolute;text-transform: uppercase;text-align: center;left: 40%;top: 40%;-ms-transform: rotate(30deg);-webkit-transform: rotate(30deg);transform: rotate(30deg);'>Paid</h1>";
                    }

                    content +=
                        "<div class='ItemDetails' style='width: 100%; font-size:14px; height: 20px; color: #444;text-align: center;'>" +
                        "<div class='pUprice' style='width:81%; height: 20px;  float: left;  padding: 0px 0px 0px 10px; text-align:right;'> Discount </div>" +
                        "<div class='pTotal' style='width:16%; height: 20px; float: left; padding: 0px 0px 0px 0px; text-align:right;'>" +
                        row["discAmt"] +
                        "</div></div>";

                    content +=
                        "<div class='ItemDetails' style='width: 100%; font-size:14px; height: 20px; color: #444;text-align: center;'>" +
                        "<div class='pUprice' style='width:81%; height: 20px;  float: left;  padding: 0px 0px 0px 10px; text-align:right;'> Received Amount </div>" +
                        "<div class='pTotal' style='width:16%; height: 20px; float: left; padding: 0px 0px 0px 0px; text-align:right;'>" +
                        row["payCash"] +
                        "</div></div>";

                    content +=
                        "<div class='ItemDetails' style='width: 100%; font-size:14px; height: 20px; color: #444;text-align: center;'>" +
                        "<div class='pUprice' style='width:81%; height: 20px;  float: left;  padding: 0px 0px 0px 10px; text-align:right;'> Pay Amount </div>" +
                        "<div class='pTotal' style='width:16%; height: 20px; float: left; padding: 0px 0px 0px 0px; text-align:right;'>" +
                        row["currentCash"] +
                        "</div></div>";

                    content +=
                        "<div class='ItemDetails' style='width: 100%; font-size:14px; height: 20px; color: #444;text-align: center;'>" +
                        "<div class='pUprice' style='width:81%; height: 20px;  float: left;  padding: 0px 0px 0px 10px; text-align:right;'> Due Amount </div>" +
                        "<div class='pTotal' style='width:16%; height: 20px; float: left; padding: 0px 0px 0px 0px; text-align:right;'>" +
                        row["giftAmt"] +
                        "</div></div>";

                    content += "<div style='padding-top: 60px; font-size:14px;'>" +
                               "<div style='width:18%;float:left;border-top:1px solid  #000;margin-left:10px;text-align:center;'>Customer Sign</div>" +
                               "<div style='width:18%;float:right;border-top:1px solid  #000;margin-right:20px;text-align:center;'>Manager Sign </div>" +
                               "</div>";

                    content += @"<div class='warningsms' style='width:98%; font-size:14px; padding: 10px; background: #f5f5f5; margin: 40px auto 20px;; line-height: 23px;'>
                             " + row["invoiceFooterNote"] +
                               "</div>";


                    content += "<div style='width:100%'>" +
                               "<div style='width:100%;float:left;margin:0 auto; font-size:14px; text-align:center;padding: 0px 0 10px 0;'>Software by MetaKave</div>" +
                               "</div>";

                    content += "</div><br/><br/></body></html>";

                    if (string.IsNullOrEmpty(email))
                        return;

                    //Mail Send
                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("support@metaposbd.com");
                    mailMessage.To.Add(new MailAddress(email));

                    string emailBody = content;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = "MetaPOS Invoice";
                    mailMessage.Body = emailBody;

                    var smt = new SmtpClient("mail.metaposbd.com");
                    var ntwordCred = new NetworkCredential("support@metaposbd.com", "rtj#s()k8d#k");
                    smt.Credentials = ntwordCred;

                    try
                    {
                        smt.Send(mailMessage);
                    }
                    catch
                    {
                    }
                }
            }
        }


    }


}