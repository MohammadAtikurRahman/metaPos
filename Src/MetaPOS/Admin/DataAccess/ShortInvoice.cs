using System;
using System.Data;
using System.Web;


namespace MetaPOS.Admin.DataAccess
{


    public class ShortInvoice
    {


        private CommonFunction obCommonFunction = new CommonFunction();

        private SqlOperation objSql = new SqlOperation();
        //private DataSet ds, dsPreBill;
        private DataTable dt;

        private string isPaidWatermark = "";//, query = "";

        public static string isCatagoryProduct = "", isPaymentHistory = "", isUnicode = "", printBillNo = "", printChangeAmt, isMiscCost = "", isVat = "", isDisc = "";

        public static string printBillingNo = "";

        // Small Invoice
        public string ShortInvoicePrint()
        {
            string printQuery = "";
            string content = "";

            isCatagoryProduct = obCommonFunction.findSettingItemValue(19);
            isMiscCost = obCommonFunction.findSettingItemValueDataTable("isMiscCost");
            isVat = obCommonFunction.findSettingItemValueDataTable("isVatSale");
            isDisc = obCommonFunction.findSettingItemValueDataTable("displayDiscountInSale");



            try
            {

                printQuery = @"SELECT DISTINCT sale.prodID, sale.qty, sale.sPrice, sale.netAmt, sale.discAmt,
                                sale.vatAmt, sale.grossAmt, sale.payMethod,sale.billNo,sale.cusID,
                                sale.payCash, sale.payCard, sale.giftAmt, sale.return_, sale.balance, 
                                sale.entryDate, sale.sPrice, sale.currentCash, sale.comment,
                                sale.loadingCost,sale.shippingCost,sale.carryingCost,sale.unloadingCost,sale.serviceCharge,
                                (sale.loadingCost+sale.shippingCost+sale.carryingCost+sale.unloadingCost+sale.serviceCharge) as miscCost,
                                sale.salePersonType,sale.returnQty,sale.isAutoSalesPerson,sale.extraDiscount,
                                (select top 1 status from SlipInfo where billNo = '" + printBillingNo + @"' order by Id desc ) as status,
                                service.name as serviceName, service.type as serviceType,
                                ci.name, ci.phone,ci.address,ci.mailInfo,ci.totalDue,ci.openingDue,   
                                si.prodName AS prodName, si.sPrice,si.catName,si.warranty, si.unitId,si.locationId,
                                location.name as locationName,
                                staff.name AS staffName,
                                bi.branchName, bi.branchAddress, bi.branchPhone, 
                                bi.branchMobile, bi.invoiceFooterNote, bi.branchLogoPath,
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
                                LEFT JOIN BranchInfo AS bi ON '" + HttpContext.Current.Session["storeId"] + "' = bi.storeId " +
                             " WHERE sale.billNo = '" + printBillingNo + "' and stockstatus.billNo = '" + printBillingNo + "' AND stockstatus.status !='saleReturn'";


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
                    //    paymentBy = "Cheque";
                    //else if (row["payMethod"].ToString() == "3")
                    //    paymentBy = "Bkash";
                    //else if (row["payMethod"].ToString() == "4")
                    //    paymentBy = "Deposit";
                    //else if (row["payMethod"].ToString() == "5")
                    //    paymentBy = "Cash On Delivery";



                    var advancedDisplay = obCommonFunction.findSettingItemValue(22);

                    //
                    if (count == 0)
                    {
                        content += @"<html><header><style>
		                            @import url('https://fonts.googleapis.com/css?family=Roboto+Condensed:300,400|Work+Sans:300,500');
                                     body{
                                          display: flex;
                                          min-height: 100vh;
                                          color: #525252;
                                          font-family: 'Work Sans', sans-serif;
                                          font-weight: 300;
                                          line-height: 1.25;
                                          color:#000;
                                          font-weight: 600;
                                          }
                                    .address p{
                                               padding:0;
                                               margin:0;
                                                margin: 0 auto; 
                                                font-size:12px;
                                                color:#000;
                                        }
                                    
	                            </style></header><body>";
                        var smallPaperWidthMilimeter = obCommonFunction.findSettingItemValueDataTable("smallPrintPaperWidth");
                        var smallPaperWidthPixel = Convert.ToDecimal(smallPaperWidthMilimeter == "" ? "0" : smallPaperWidthMilimeter) * 3.77M;
                        //<img src=" + folderPath + row["branchLogoPath"] + " style='width:120px;padding: 20px 0 0 15px; left:0;top:0;'/>" +
                        content += @"<div class='mainEmailBody' style='width: " + smallPaperWidthPixel + "px" + @";  height: auto; overflow: hidden; background: #fff; padding:0 5px 0 5px; font-family: 'Work Sans', sans-serif; font-weight: 600;'>
                                    
                                        <div class='brand-name' style='width:95%; text-align:center; float:left'>
                                            <h2 style='margin: 0 auto;font-size: 18px;font-weight: 900;'>" +
                                   row["branchName"] + "</h2> " +
                                   "<span class='address''>" + row["branchAddress"] +
                                   "</span><span style='width: 60%; margin: 0 auto; font-size:12px;color:#000;'>" +
                                   row["branchPhone"] +
                                   ", " + row["branchMobile"] +
                                   @"</span></div>";


                        content +=
                            @"<div class='invoiceTitle invoiceText' style='text-transform: uppercase ;color: #000;font-size: 12px;padding: 4px 0px 0px 0;float: left; margin: 4px 0 8px 0;  width:100%'> 
                                             <span style='border:1px solid #000;padding: 3px 10px;margin-left: 35%;'>Invoice</span>
                                 </div>";

                        content +=
                            @"<div class='' style='color: #000;font-size: 12px;padding: 0px 0px;float: left; margin: 4px 0 8px 0;  width:50%; float:left;'>" +
                            "<span style='margin:0;'> Cus ID: " + row["cusID"] +
                            "</span><br/><span style='margin:0;'> Name: " + row["name"] +
                            //"</span><br/><span style='margin:0;'> Address: " + row["address"] +
                            "</span><br/><span style='margin:0;'> Phone: " + row["phone"] +
                            "</span><br/><span style='margin:0;'> Email: " + row["mailInfo"] + "</span>" +
                            "</div>" +
                            @"<div class='invoiceTitle invoiceText' style='color: #000;font-size: 12px;padding: 0px 0px;float: left; margin: 4px 0 8px 0;  width:50%; float:left;'> " +
                            "<span style='margin:0'> Invoice No: " + printBillingNo +
                            "</span><br/><span style='margin:0;'>Date: " +
                            Convert.ToDateTime(row["entryDate"]).ToString("dd-MMM-yyyy") +
                            "</span><br/><span style='margin:0;'> Time: " +
                            obCommonFunction.GetCurrentTime().ToShortTimeString() +
                            "</span><br/><span style='margin:0;'>Sold By: " + row["staffName"] + "</span>" +
                            //"</span><br/><span style='margin:0;'> Payment By: " + paymentBy +
                            "</div>";

                        content += @"<div class='' style='text-transform: uppercase ;color: #000;font-size: 11px;padding: 0px 0px;float: left; margin: 4px 0 0px 0; width:95%; background: #eee; '> 
                                        <div class='pDetails' style='width:27%; height: 5px; background: #eee; float: left;  text-align: left; padding: 4px 0px 10px 2px'> Product </div>
                                        <div class='pQuantity' style='width:10%; height: 5px; background: #eee; float: left; padding: 4px 0px 10px 0'> Qty </div>";
                        //if (Convert.ToDecimal(row["returnQty"]) > 0)
                            content += "<div class='pQuantity' style='width:17%; height: 5px; background: #eee; float: left; padding: 4px 0px 10px 0'> Return </div>";

                        content += @"<div class='pUprice' style='width: 16%;height: 5px;background: #eee;float: left;text-align: right; padding: 4px 0px 10px 10px;color:#000;'> Price </div>
                                                        <div class='pTotal' style='width:22%;height: 5px; background: #eee; float: left ; padding: 3px 2px 10px 10px;color:#000;'> Total </div>
                                                    </div>";

                    }

                    content += "<table style='width:95%; font-size:11px; >" +
                               "<tr ><td style='text-align: left;padding-left: 8px;font-size: 11px;display:none '>";
                    ++count;
                    //"<tr ><td style='width: 4%;text-align: left;padding-left: 8px;font-size: 11px; '>" + ++count;
                    if (isCatagoryProduct == "0")
                    {
                        if (row["searchType"].ToString() == "salePackage")
                        {
                            content +=
                                "</td><td style='width:24%;float:left;padding-left: 2px;font-size: 11px;color:#000;'>" +
                                row["prodPackName"];
                        }
                        else if (row["searchType"].ToString() == "service")
                        {
                            content +=
                                "</td><td style='width:24%;float:left;padding-left: 2px;font-size: 11px;color:#000;'>" +
                                row["serviceName"];
                        }
                        else
                        {
                            content +=
                                "</td><td style='width:24%;float:left;padding-left: 2px;font-size: 11px;color:#000;'>" +
                                row["prodName"];
                        }
                    }
                    else
                    {
                        content +=
                            "</td><td style='width:90%;float:left;padding-left: 2px;font-size: 11px;color:#000;'>" +
                            row["CategoryName"];
                    }

                    content += "</td><td style='width: 15%;text-align:left;font-size: 11px;float: left;'>" + row["qty"];

                    //if (Convert.ToDecimal(row["returnQty"]) > 0)
                        content += "</td><td style='width: 15%;text-align:left;font-size: 11px;float: left;'>" + row["returnQty"];

                    content += "</td><td style='width: 18%;text-align:right;font-size: 11px; color:#000;float: left;'>" +
                               row["sPrice"] +
                               "</td><td style='width: 22%;text-align:right; padding-right: 5px;font-size: 11px;color:#000;float: left;'>" +
                               (Convert.ToDecimal(row["qty"]) * Convert.ToDecimal(row["sPrice"])-(Convert.ToDecimal(row["returnQty"]) * Convert.ToDecimal(row["sPrice"]))) +
                               "</td></tr></table>";


                    if (count == dt.Rows.Count)
                    {
                        content += "<div style='width:100%; height:120px; '>";
                        content += "<div style='width:20%;float:left;font-size:11px;'>" + row["comment"] + "</div>";

                        content +=
                            "<div style='width: 75%;float: right;margin-right: 5px;'><div class='ItemDetails' style='width: 100%; font-size:11px;color: #000;text-align: center;padding-top:3px;'>" +
                            "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Total </div>" +
                            "<div class='pTotal' style='width:45%; float: left; text-align:right;'>" + row["netAmt"] +
                            "</div></div>";

                        if (isMiscCost == "1")
                        {
                            content +=
                                "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                                "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Misc Cost </div>" +
                                "<div class='pTotal' style='width:45%; float: left;  text-align:right;'>" +
                                row["miscCost"] +
                                "</div></div>";
                        }

                        if (isVat == "1")
                        {
                            content +=
                                "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                                "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Vat. </div>" +
                                "<div class='pTotal' style='width:45%; float: left;  text-align:right;'>" +
                                row["vatAmt"] +
                                "</div></div>";
                        }

                        if (obCommonFunction.findSettingItemValueDataTable("displayDiscountInSale") == "1")
                        {
                            content +=
                                "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                                "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Discount </div>" +
                                "<div class='pTotal' style='width:45%; float: left;  text-align:right;'>" +
                                row["discAmt"] +
                                "</div></div>";

                        }

                        content +=
                            "<div class='vertical-border' style='border-bottom: 1px dotted #ccc; padding-top: 8px; width: 80%; float: right; margin-right: 10px;'></div>";

                        content +=
                            "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                            "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Grand Total </div>" +
                            "<div class='pTotal' style='width:45%; float: left;  text-align:right;'>" + row["grossAmt"] +
                            "</div></div>";


                        var previousDue = 0M;

                        if (row["status"].ToString() == "Sold")
                        {
                            previousDue = (Convert.ToDecimal(row["cusCurrentDue"]) + Convert.ToDecimal(row["payCash"])) -
                                          Convert.ToDecimal(row["grossAmt"]);
                        }
                        else
                        {
                            previousDue = (Convert.ToDecimal(row["cusCurrentDue"]) + Convert.ToDecimal(row["payCash"]));
                        }
                        previousDue += Convert.ToDecimal(row["extraDiscount"]);
                        
                        content +=
                            "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                            "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Pre Balance </div>" +
                            "<div class='pTotal' style='width:45%; float: left; text-align:right;'>" + previousDue
                            +
                            "</div></div>";


                        if (Convert.ToDecimal(row["currentCash"]) >= 0)
                        {
                            decimal totalPay = Convert.ToDecimal(row["payCash"]);

                            content +=
                                "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                                "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Pay </div>" +
                                "<div class='pTotal' style='width:45%; float: left; text-align:right;'>" + totalPay
                                +
                                "</div></div>";
                        }



                        if (Convert.ToDecimal(row["return_"]) > 0)
                        {
                            content +=
                                "<div class='ItemDetails' style='width: 100%;font-size:11px; font-size:11px; color: #000;text-align: center;'>" +
                                "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Change </div>" +
                                "<div class='pTotal' style='width:45%;  float: left; text-align:right;'>" +
                                row["return_"] +
                                "</div></div> ";
                        }
                        else
                        {
                            var due = Convert.ToDecimal(row["cusCurrentDue"]);
                            due = due < 0 ? 0 : due;

                            content +=
                                "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                                "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Due </div>" +
                                "<div class='pTotal' style='width:45%;  float: left; text-align:right;'>" + due +
                                "</div>" +
                                "</div> ";
                        }


                        content += "</div>";


                        isPaidWatermark = obCommonFunction.findSettingItemValue(18);
                        if (isPaidWatermark == "1" && Convert.ToInt32(row["giftAmt"]) == 0)
                        {
                            content +=
                                "<h1 style='padding: 10px 25px 10px 15px;width: 60px;height: 30px;position: absolute;text-transform: uppercase;text-align: center;left: 10%;top: 15%;-ms-transform: rotate(30deg);-webkit-transform: rotate(30deg);transform: rotate(30deg);'>Paid</h1>";
                            //content +=
                            //    "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                            //    "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Paid </div>" +
                            //    "<div class='pTotal' style='width:45%; float: left;  text-align:right;'>" + row["payCash"] +
                            //    "</div></div>";


                            if (Convert.ToDecimal(row["currentCash"]) >= 0)
                            {
                                decimal totalPay = Convert.ToDecimal(row["currentCash"]) +
                                                   Convert.ToDecimal(row["return_"]);

                                content +=
                                    "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                                    "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Pay </div>" +
                                    "<div class='pTotal' style='width:45%; float: left; text-align:right;'>" +
                                    totalPay
                                    +
                                    "</div></div>";
                            }
                            else
                            {
                                content +=
                                    "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                                    "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Return </div>" +
                                    "<div class='pTotal' style='width:45%; float: left; text-align:right;'>" +
                                    row["returnAmt"] +
                                    "</div></div>";
                            }

                            if (Convert.ToDecimal(row["return_"]) > 0)
                            {
                                content +=
                                    "<div class='ItemDetails' style='width: 100%;font-size:11px; font-size:11px; color: #000;text-align: center;'>" +
                                    "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Change </div>" +
                                    "<div class='pTotal' style='width:45%;  float: left; text-align:right;'>" +
                                    row["return_"] +
                                    "</div></div> ";
                            }
                            else
                            {

                                content +=
                                    "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                                    "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Due </div>" +
                                    "<div class='pTotal' style='width:45%;  float: left; text-align:right;'>" +
                                    row["cusCurrentDue"] +
                                    "</div>" +
                                    "</div> ";
                            }


                            //if (Convert.ToDecimal(row["returnAmt"]) > 0)
                            //{
                            //    content +=
                            //        "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                            //        "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Return </div>" +
                            //        "<div class='pTotal' style='width:45%;  float: left; text-align:right;'>" + row["returnAmt"] +
                            //        "</div>" +
                            //        "</div> ";
                            //}

                            printChangeAmt = obCommonFunction.findSettingItemValueDataTable("printChangeAmt");
                            if (printChangeAmt == "1")
                            {
                                //if (Convert.ToDecimal(row["return_"]) > 0)
                                //{
                                //    content +=
                                //        "<div class='ItemDetails' style='width: 100%;font-size:11px; font-size:11px; color: #000;text-align: center;'>" +
                                //        "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Change </div>" +
                                //        "<div class='pTotal' style='width:45%;  float: left; text-align:right;'>" +
                                //        row["return_"] +
                                //        "</div></div> ";
                                //}
                            }

                            //content +=
                            //    "<div class='ItemDetails' style='width: 100%; font-size:11px; color: #000;text-align: center;'>" +
                            //    "<div class='pUprice' style='width: 50%;float: left;text-align: right;'> Payment By </div>" +
                            //    "<div class='pTotal' style='width:45%;  float: left;  text-align:right;'>" + paymentBy +
                            //    "</div></div></div> ";


                            content += "</div>";

                            isPaidWatermark = obCommonFunction.findSettingItemValue(18);
                            if (isPaidWatermark == "1" && Convert.ToInt32(row["giftAmt"]) == 0)
                            {
                                content +=
                                    "<h1 style='padding: 10px 25px 10px 15px;width: 60px;height: 30px;position: absolute;text-transform: uppercase;text-align: center;left: 10%;top: 15%;-ms-transform: rotate(30deg);-webkit-transform: rotate(30deg);transform: rotate(30deg);'>Paid</h1>";
                            }
                            //content += "<div style='padding-top: 60px; font-size:12px;margin-top: 15px;'>" +
                            //            "<div style='width:18%;float:left;border-top:1px solid  #000;margin-left:10px;text-align:center;'>Customer Sign</div>" +
                            //            "<div style='width:18%;float:right;border-top:1px solid  #000;margin-right:20px;text-align:center;'>Manager Sign </div>" +
                            //            "</div>";

                            content += @"<div class='warningsms' style='width: 100%;padding-top: 140px;font-size:11px;padding: 0px;margin: 0px auto -10px;text-align: center;'>
                             <p style='padding-top: 115px;'>" + row["invoiceFooterNote"] +
                                       "</p></div>";

                            content += "<div style='width:100%'>" +
                                       "<div style='width:100%;float:left;margin:0 auto; font-size:11px; text-align:center;padding: 0px 0 10px 0;'>Developed by MetaKave</div>" +
                                       "</div>";

                            content += "</div></body></html>";
                        }

                    }
                }

                return content;
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


    }


}