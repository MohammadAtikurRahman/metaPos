using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;


namespace MetaPOS.Admin.DataAccess
{


    public class GodownInvoice
    {


        private CommonFunction objCommonFun = new CommonFunction();

        private SqlOperation objSql = new SqlOperation();
        //private DataSet ds, dsPreBill;
        //private DataTable dt;

        public static string isCatagoryProduct = "", isPaymentHistory = "", isUnicode = "", printBillNo = "";
        //private string query = "", isPaidWatermark = "";

        public static string printBillingNo = "";

        // Property 
        public string BillNo { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }


        // public string GodownInvoicePrint()
//        {
//            try
//            {
//                string printQuery = "", content = "";

//                isCatagoryProduct = objCommonFun.findSettingItemValue(19);
//                isPaymentHistory = objCommonFun.findSettingItemValue(32);
//                //
//                isUnicode = objCommonFun.findSettingItemValue(33);

//                //            printQuery = @"SELECT tps.billNo, tps.cusID, tps.prodID, tps.roleID, 
//                //                            sale.qty, sale.sPrice, sale.netAmt, sale.discAmt, sale.vatAmt, sale.grossAmt, sale.payMethod, 
//                //                            sale.payCash, sale.payCard, sale.giftAmt, sale.return_, sale.balance, 
//                //                            sale.entryDate, sale.sPrice, sale.currentCash, sale.comment,
//                //                            ci.name, ci.phone,ci.address,ci.mailInfo,ci.totalDue,   
//                //                            si.prodName AS prodName, si.sPrice,si.catName,
//                //                            staff.name AS staffName,
//                //                            bi.branchName, bi.branchAddress, bi.branchPhone, bi.branchMobile, bi.invoiceFooterNote, bi.branchLogoPath,
//                //                            cat.Id, cat.catName as CategoryName,
//                //                            pack.packageName AS prodName,
//                //                            CONCAT(pack.packageName , si.prodName) AS prodPackName
//                //                            FROM TempPrintSaleInfo as tps
//                //                            LEFT JOIN SaleInfo AS sale ON tps.billNo = sale.billNo AND tps.prodID = sale.prodID
//                //                            LEFT JOIN CustomerInfo AS ci ON tps.cusId = ci.cusId
//                //                            LEFT JOIN stockInfo AS si ON tps.prodId = si.ProdID
//                //                            LEFT JOIN StaffInfo AS staff ON sale.salesPersonId = staff.staffID
//                //                            LEFT JOIN CategoryInfo AS cat ON si.catName = cat.Id
//                //                            LEFT JOIN PackageInfo AS pack ON sale.prodID = pack.Id
//                //                            LEFT JOIN BranchInfo AS bi ON " + HttpContext.Current.Session["invoiceRoleId"] + " = bi.Id WHERE tps.billNo = '" + printBillingNo + "'";

//                printQuery = @"SELECT sale.qty, sale.sPrice, sale.netAmt, sale.discAmt, sale.vatAmt, sale.grossAmt, sale.payMethod,sale.billNo,sale.cusID,
//                            sale.payCash, sale.payCard, sale.giftAmt, sale.return_, sale.balance, 
//                            sale.entryDate, sale.sPrice, sale.currentCash, sale.comment,
//                            ci.name, ci.phone,ci.address,ci.mailInfo,ci.totalDue,   
//                            si.prodName AS prodName, si.sPrice,si.catName,
//                            staff.name AS staffName,
//                            bi.branchName, bi.branchAddress, bi.branchPhone, bi.branchMobile, bi.invoiceFooterNote, bi.branchLogoPath,
//                            cat.Id, cat.catName as CategoryName,
//                            pack.packageName AS prodName,
//                            CONCAT(pack.packageName , si.prodName) AS prodPackName,
//                            sup.supCompany as supCompany
//                            FROM  SaleInfo AS sale LEFT JOIN CustomerInfo AS ci ON ci.cusId = sale.cusID
//                             LEFT JOIN stockInfo AS si ON si.ProdID = sale.prodID
//                            LEFT JOIN StaffInfo AS staff ON sale.salesPersonId = staff.staffID
//                            LEFT JOIN CategoryInfo AS cat ON si.catName = cat.Id
//                            LEFT JOIN PackageInfo AS pack ON sale.prodID = pack.Id
//                            LEFT JOIN supplierInfo as sup ON si.supCompany = sup.supId
//                            LEFT JOIN BranchInfo AS bi ON " + HttpContext.Current.Session["invoiceRoleId"] + " = bi.Id WHERE sale.billNo = '" + printBillingNo + "'";

//                dt = objSql.getDataTable(printQuery);


//                BillNo = printBillingNo;
//                int count = 0;
//                string paymentBy = "", ImgPath = "";
//                string folderPath = HttpContext.Current.Server.MapPath("../Img/Logo/");

//                foreach (DataRow row in dt.Rows)
//                {
//                    //check payment method
//                    if (row["payMethod"].ToString() == "0")
//                        paymentBy = "Cash";
//                    else if (row["payMethod"].ToString() == "1")
//                        paymentBy = "Card";
//                    else if (row["payMethod"].ToString() == "2")
//                        paymentBy = "Cheque";
//                    else if (row["payMethod"].ToString() == "3")
//                        paymentBy = "Bkash";
//                    else if (row["payMethod"].ToString() == "4")
//                        paymentBy = "Deposit";
//                    else if (row["payMethod"].ToString() == "5")
//                        paymentBy = "Cash On Delivery";

//                    // 
//                    printBillNo = BillNo;

//                    //Advanced amout
//                    decimal Advanced = 0;
//                    if (Convert.ToDecimal(row["payCash"]) > (Convert.ToDecimal(row["netAmt"]) + Convert.ToDecimal(row["discAmt"])))
//                    {
//                        Advanced = Convert.ToDecimal(row["payCash"]) - (Convert.ToDecimal(row["netAmt"]) + Convert.ToDecimal(row["discAmt"]));
//                    }
//                    else
//                    {
//                        Advanced = 0;
//                    }

//                    var advancedDisplay = objCommonFun.findSettingItemValue(22);


//                    if (count == 0)
//                    {
//                        DataSet dsImg = objSql.getDataSet("SELECT * FROM BranchInfo WHERE Id = '" + HttpContext.Current.Session["roleId"] + "'");

//                        string imgName = "";

//                        if (dsImg.Tables[0].Rows.Count > 0 && !String.IsNullOrEmpty(dsImg.Tables[0].Rows[0][10].ToString()))
//                        {
//                            imgName = dsImg.Tables[0].Rows[0][10].ToString();
//                            ImgPath = "../../Img/Logo/" + imgName;
//                        }
//                        else
//                        {
//                            ImgPath = "";
//                        }

//                        string branchPhoneNum = "";
//                        if (row["branchPhone"].ToString() != "" && row["branchMobile"].ToString() != "")
//                            branchPhoneNum = row["branchPhone"].ToString() + ", " + row["branchMobile"].ToString();
//                        else
//                            branchPhoneNum = row["branchPhone"].ToString() + row["branchMobile"].ToString();


//                        content += @"<html lang='bn'><header>
//                            <style type='text/css'>
//                                .address p{margin: 0 0 5px};  
//                            </style>  
//                            </header><body>";
//                        //<img src=" + folderPath + row["branchLogoPath"] + " style='width:120px;padding: 20px 0 0 15px; left:0;top:0;'/>" +
//                        content += @"<div class='mainEmailBody' style='width: 95%;  height: auto; overflow: hidden; background: #fff; margin: 0 auto; padding:0 20px 0 20px;'>
//                                    <div class='mHead' style='width: 100%; overflow: hidden; '>
//                                        <div class='invoiceTitle' style='width:200px;color:#fff;font-size: 22px;float: left;position: absolute;'>
//                                                <img src='" + ImgPath + "' style='width:auto;height: 100px;padding: 20px 0 0 15px; left:0;top:0;'/>" +
//                                        @"</div>
//                                        <div class='brand-name' style='width:100%; text-align:center; float:left'>
//                                            <h2 style='margin: 0 auto;font-size: 32px;font-weight: 900;'>" + row["branchName"] + "</h2> " +
//                                                     "<div class='address' style='width: 60%; margin: 0 auto; '>" +
//                                                    row["branchAddress"] +
//                                                    branchPhoneNum +
//                                            @"</div></div>
//                                </div>";


//                        content += @"<div style='width: 100% ; height:50px; text-align:center; padding-left: 42%;'>
//                                    <div class='invoiceTitle invoiceText' style='text-transform: uppercase;color: #000;font-size: 18px;padding: 8px 20px;float: left;margin-top: 5px; border:1px solid #000'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "ইনভয়েস ";
//                        }
//                        else
//                        {
//                            content += " Godown Invoice ";
//                        };
//                        content += "</div></div>";

//                        content += @"<div class='mContentBody' font-size:14px; style='width: 100%; height: 125px; padding-bottom:5px; margin-top: 5px;'>
//                                <div class='invoiceAddress' style='width:46%; font-size:14px;  background: #fff; color:#333;  padding: 0px 12px; float: left; border-right:1px solid #ccc; line-height: 23px' >";
//                        content += @"<p style='margin:0'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "কাস্টমার নংঃ ";
//                            content += objCommonFun.ConvertToUnicode(CustomerId);
//                        }
//                        else
//                        {
//                            content += "Customer ID: ";
//                            content += row["cusId"];
//                        };

//                        content += "</p><p style='margin:0'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "নামঃ ";
//                        }
//                        else
//                        {
//                            content += "Name: ";
//                        };
//                        content += row["name"] + "</p><p style='margin:0'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "ঠিকানাঃ ";
//                        }
//                        else
//                        {
//                            content += "Address: ";
//                        };
//                        content += row["address"] + "</p><p style='margin:0'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "ফোনঃ ";
//                            content += objCommonFun.ConvertToUnicode(row["phone"]) + "</p><p style='margin:0'>";
//                        }
//                        else
//                        {
//                            content += "Phone: ";
//                            content += row["phone"] + "</p><p style='margin:0'>";
//                        };

//                        if (isUnicode == "1")
//                        {
//                            content += "ইমেইলঃ ";
//                        }
//                        else
//                        {
//                            content += "Email: ";
//                        };
//                        content += row["mailInfo"] + "</p></div>";

//                        content += @"<div class='invoiceOthers' style='width:28%; background: #fff; font-size:14px; color:#333;  padding: 0px 12px; float: left; text-align: left; line-height: 23px'> 
//                                  <p style='margin:0'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "বিল নংঃ ";
//                            content += objCommonFun.ConvertToUnicode(BillNo) + "</p><p style='margin:0'>";
//                        }
//                        else
//                        {
//                            content += "Bill No: ";
//                            content += row["billNo"] + "</p><p style='margin:0'>";
//                        };

//                        if (isUnicode == "1")
//                        {
//                            content += "তারিখঃ ";
//                            content += objCommonFun.ConvertToUnicode(Convert.ToDateTime(row["entryDate"]).Day) + "/" + objCommonFun.ConvertToUnicode(Convert.ToDateTime(row["entryDate"]).Month) + "/" + objCommonFun.ConvertToUnicode(Convert.ToDateTime(row["entryDate"]).Year);
//                            //content += Convert.ToDateTime(row["entryDate"]).ToString("dd/MMM/yyyy", new CultureInfo("bn-BD", false));
//                        }
//                        else
//                        {
//                            content += "Date: ";
//                            content += Convert.ToDateTime(row["entryDate"]).ToString("dd/MMM/yyyy");
//                        };

//                        content += "</P><p style='margin:0'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "সময়ঃ ";
//                            content += objCommonFun.ConvertToUnicode(DateTime.Parse(objCommonFun.GetCurrentTime().ToString("hh:mm tt")).Hour.ToString("00")) + ":" + objCommonFun.ConvertToUnicode(Convert.ToDateTime(objCommonFun.GetCurrentTime().ToString("0:hh:mm tt")).Minute.ToString("00"));
//                            //content += obCommonFunction.ConvertToUnicode(Convert.ToDateTime(row["entryDate"]).Minute);
//                        }
//                        else
//                        {
//                            content += "Time: ";
//                            content += objCommonFun.GetCurrentTime().ToShortTimeString();
//                            //content += DateTime.Now.ToShortTimeString();
//                        };

//                        content += "</p><p style='margin:0'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "বিক্রয়ঃ ";
//                        }
//                        else
//                        {
//                            content += "Sold by: ";
//                        };
//                        content += row["staffName"];
//                        content += "</p><p style='margin:0'>";
//                        if (isUnicode == "1")
//                        {
//                            content += "পেমেন্টের ধরণঃ ";
//                        }
//                        else
//                        {
//                            content += "Payment by: ";
//                        };
//                        content += paymentBy;

//                        content += "</div></div>";

//                        if (isUnicode == "1")
//                        {
//                            content += @"<div class='ItemHead' style='width: 100%; font-size: 15px;  height: 30px; background: #eee; border: 1px solid #ccc; margin-top: 0px; color: #444; text-align: center; '>
//                            <div class='SLno' style='width:5%; height: 10px; background: #eee; float: left; text-align: left;  padding: 8px 0px 10px 10px'> নং </div>
//                              
//                            <div class='pDetails' style='width:70%; height: 10px; background: #eee; float: left;  text-align: left; padding: 8px 0px 10px 10px'> বিবরণ </div>
//                            <div class='pUprice' style='width: 20%;height: 10px;background: #eee;float: left;text-align: left;padding: 8px 0px 10px 10px;'> ক্যাটাগরি </div>
//                            <div class='pTotal' style='width:20%; height: 10px; background: #eee; float: left ;text-align: left; padding: 8px 0px 10px 10px; display:none'> সুপ্লায়ার </div>
//                            <div class='pQuantity' style='width:14%; height: 10px; background: #eee; float: left; padding: 8px 0px 10px 10px'>পরিমান </div>
//
//                        </div>";
//                        }
//                        else
//                        {
//                            content += @"<div class='ItemHead' style='width: 100%; font-size: 15px;  height: 30px; background: #eee; border: 1px solid #ccc; margin-top: 0px; color: #444; text-align: center; '>
//                            <div class='SLno' style='width:5%; height: 10px; background: #eee; float: left; text-align: left;  padding: 8px 0px 10px 10px'> SL </div>
//
//                            <div class='pDetails' style='width:70%; height: 10px; background: #eee; float: left;  text-align: left; padding: 8px 0px 10px 10px'> Product Details </div>
//                            <div class='pUprice' style='width: 20%;height: 10px;background: #eee;float: left;text-align: left;padding: 8px 0px 10px 10px; display:none'> Category </div>
//                            <div class='pTotal' style='width:20%; height: 10px; background: #eee; float: left ;text-align: left; padding: 8px 0px 10px 10px; display:none'> Supplier </div>
//                            <div class='pQuantity' style='width:10%; height: 10px; background: #eee; float: left; padding: 8px 0px 10px 10px'> Qty </div>
//                            
//                        </div>";
//                        };


//                    }

//                    if (isUnicode == "1")
//                    {
//                        content +=
//                            "<table style='width:100%; border-bottom:1px solid #ccc;border-left:1px solid #ccc;border-right:1px solid #ccc;' >" +
//                            "<tr ><td style='width: 5%;text-align: left;padding-left: 10px;font-size: 14px;'>" +
//                            objCommonFun.ConvertToUnicode(++count);
//                    }
//                    else
//                    {
//                        content +=
//                           "<table style='width:100%; border-bottom:1px solid #ccc;border-left:1px solid #ccc;border-right:1px solid #ccc;' >" +
//                           "<tr ><td style='width: 5%;text-align: left;padding-left: 10px;font-size: 14px;'>" + ++count;

//                    }


//                    content += "</td><td style='width:80%;float:left;padding-left: 15px;font-size: 14px;'>" + row["CategoryName"] + " " + row["prodPackName"];


//                    //content += "</td><td style='width:28%;float:left;padding-left: 15px;font-size: 14px;'>" + row["CategoryName"];

//                    //content += "</td><td style='width:22%;float:left;padding-left: 15px;font-size: 14px; display:none'>" + row["supCompany"];


//                    if (isUnicode == "1")
//                    {
//                        content += "</td><td style='width: 12%;text-align:left;font-size: 14px;float:left'>" + objCommonFun.ConvertToUnicode(row["qty"]) +
//                    "</td></tr></table>";
//                    }
//                    else
//                    {
//                        content += "</td><td style='width: 10%;text-align:left;font-size: 14px;float:left '>" + row["qty"] +
//                    "</td></tr></table>";
//                    }

//                    if (count == dt.Rows.Count)
//                    {
//                        int i;
//                        decimal tmpAmt = 0, dsAmt = 0, recivedAmt = 0;
//                        var output = "";
//                        Label[] lblDateTime = new Label[100];
//                        Label[] lblRecivedAmt = new Label[100];

//                        dsPreBill = objCommonFun.prevoiusBillNo(BillNo.ToString());
//                        int countBill = dsPreBill.Tables[0].Rows.Count;


//                        for (i = 0; i < countBill; i++)
//                        {
//                            lblRecivedAmt[i] = new Label();
//                            lblDateTime[i] = new Label();
//                            lblRecivedAmt[i].Text = dsPreBill.Tables[0].Rows[i][2].ToString();
//                            lblDateTime[i].Text = dsPreBill.Tables[0].Rows[i][3].ToString();
//                        }


//                        content += "<div style='width:100%; height:90px;'>";
//                        content += "<div style='width:50%;float:left;padding:10px;font-size:14px;'>" + row["comment"];

//                        //Heder payment history
//                        if (isPaymentHistory == "1" && dt.Rows.Count > 1)
//                        {
//                            content += "<p style='padding: 2px;margin: 0;border-bottom: 1px solid #ccc;width: 50%; padding-top:10px ;'>Payment History</p>";

//                            for (i = 0; i < countBill; i++)
//                            {
//                                dsAmt = Convert.ToDecimal(lblRecivedAmt[i].Text);
//                                recivedAmt = (dsAmt - tmpAmt);
//                                if (Convert.ToDecimal(recivedAmt) > 0)
//                                {
        //                                    content += "<div style='width:25%;float:left;font-size:14px; padding-right: 30px;'>" + Convert.ToDateTime(lblDateTime[i].Text).ToString("dd-MMM-yyyy") + "</div>";
//                                    content += "<div style='width:50%;float:left;font-size:14px; text-align='right' '>" + recivedAmt.ToString() + "</div>";
//                                }
//                                tmpAmt = dsAmt;
//                            }
//                        }

//                        //
//                        content += "</div>";

//                        content += "</div></div>";

//                        isPaidWatermark = objCommonFun.findSettingItemValue(18);
//                        if (isPaidWatermark == "1" && Convert.ToInt32(row["giftAmt"]) == 0)
//                        {
//                            content += "<h1 style='border-radius: 10px;border: 2px solid #000;padding: 10px 25px 10px 15px;width: 60px;height: 30px;position: absolute;text-transform: uppercase;text-align: center;left: 40%;top: 40%;-ms-transform: rotate(30deg);-webkit-transform: rotate(30deg);transform: rotate(30deg);'>Paid</h1>";
//                        }

//                        if (isUnicode == "1")
//                        {
//                            content += "<div style='padding-top: 60px; font-size:14px;margin-top: 15px;'>" +
//                                    "<div style='width:18%;float:left;border-top:1px solid  #000;margin-left:10px;text-align:center;'>ক্রেতার স্বাক্ষর</div>" +
//                                    "<div style='width:18%;float:right;border-top:1px solid  #000;margin-right:20px;text-align:center;'>ম্যানেজার স্বাক্ষর </div>" +
//                                    "</div>";
//                        }
//                        else
//                        {
//                            content += "<div style='padding-top: 60px; font-size:14px;margin-top: 15px;'>" +
//                                    "<div style='width:18%;float:left;border-top:1px solid  #000;margin-left:10px;text-align:center;'>Customer Sign</div>" +
//                                    "<div style='width:18%;float:right;border-top:1px solid  #000;margin-right:20px;text-align:center;'>Manager Sign </div>" +
//                                    "</div>";
//                        }

//                        content += @"<div class='warningsms' style='width:98%; font-size:14px; padding: 10px; background: #f5f5f5; margin: 40px auto 20px;; line-height: 23px;'>
//                             " + row["invoiceFooterNote"] +
//                               "</div>";


//                        content += "<div style='width:100%'>" +
//                                    "<div style='width:100%;float:left;margin:0 auto; font-size:14px; text-align:center;padding: 0px 0 10px 0;'>Developed by MetaKave</div>" +
//                                    "</div>";

//                        content += "</div><br/><br/></body></html>";
//                    }
//                }


//                //objSql.executeQuery("DELETE FROM [TempPrintSaleInfo] WHERE billNo = '" + printBillingNo + "'");

//                return content;
//            }
//            catch (Exception ex)
//            {
//                return ex.ToString();
//            }
//        }
    }


}