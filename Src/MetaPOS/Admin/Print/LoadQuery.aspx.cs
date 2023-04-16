using System;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Print
{


    public partial class LoadQuery : BasePage
    {


        private CommonFunction commonFunction = new CommonFunction();
        private SqlConnection vcon;

        protected void Page_Load(object sender, EventArgs e)
        {
            var constr = GlobalVariable.getConnectionStringName();
            vcon = new SqlConnection(ConfigurationManager.ConnectionStrings[constr].ToString());

            if (Session["email"] == null)
            {
                var obj = new DataAccess.CommonFunction();
                obj.logout();
            }


            // receive query string and page name
            if ( Session["pageName"].ToString() == "Customer")
                fnCustomer(Session["reportQury"].ToString());

            if (Session["pageName"].ToString() == "CustomerList")
                fnCustomerList(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Supplier")
                fnSupplier(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Staff")
                fnStaff(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "ExpenseReport")
                fnExpenseReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Category")
                fnCategoryInfo(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Particular" &&
                     Session["reportQury"] == "SELECT * FROM CashCatInfo WHERE active='1' ORDER BY cashType ASC")
                fnCashCatInfo(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Stock")
                fnStock(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Image")
                fnImage(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Return")
                fnStockReturn(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Damage")
                fnStockReturn(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "PurchaseReport")
                fnPurshase(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "SlipReport")
                fnSlip(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Bank")
                fnBank(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Banking")
                fnBankingReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "SupplierList")
                fnSupplierListReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Size")
                fnSizeReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "StaffList")
                fnStaffListReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "OfferList")
                fnOfferList(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "EcommerceList")
                fnEcommerceList(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Cancel")
                fnECancelList(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "TransReport")
                fnTransReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "CashReport")
                fnCashReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "PaymentReceipt")
                fnPaymentReceipt(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "Package")
                fnPackage(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "StockStatus")
                fnStockStatus(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "SaleStatus")
                fnSaleStatus(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "paymentReport")
                fnCusPaymentReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "serviceReport")
                fnServiceReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "PurchaseItemReport")
                fnPurchaseReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "CustomerLedgerReport")
                fnCustomerLedgerReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "WarrantyReport")
                fnWarrantyReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "WarningReport")
                fnWarningReport(Session["reportQury"].ToString());

            else if (Session["pageName"].ToString() == "ExpiryReport")
                fnExpiryReport(Session["reportQury"].ToString());
        }

        private void fnWarrantyReport(string query)
        {
            SqlCommand cmd = new SqlCommand(query, vcon);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "WarrantyReport");
            //
            ReportDocument report = new ReportDocument();
            report.Load(Server.MapPath("crWarrantyReport.rpt"));
            report.SetDataSource(ds.Tables["WarrantyReport"]);

            //

            CrystalReportViewer1.ReportSource = report;
            CrystalReportViewer1.DataBind();

        }



        private void fnPurchaseReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "StockStatusInfoWtihCatSup");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crPurchaseReport.rpt"));
                report.SetDataSource(ds.Tables["StockStatusInfoWtihCatSup"]);
                //

                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                string reportName = Session["reportName"].ToString();
                crParameterDiscreteValue.Value = reportName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reportName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                //crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //

                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }

        private void fnServiceReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "ServiceInfo");




                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crServiceReceiptReport.rpt"));
                report.SetDataSource(ds.Tables["ServiceInfo"]);

                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                string comName = Session["comName"].ToString();
                crParameterDiscreteValue.Value = comName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["comName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                //crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnCusPaymentReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CusPayReport");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCusPaymentReport.rpt"));
                report.SetDataSource(ds.Tables["CusPayReport"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnStockStatus(string query)
        {
            SqlCommand cmd = new SqlCommand(query, vcon);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "StockStatus");
            //
            ReportDocument report = new ReportDocument();
            report.Load(Server.MapPath("crStockStatusReport.rpt"));
            report.SetDataSource(ds.Tables["StockStatus"]);

            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

            string ReportType = Session["ReportType"].ToString();
            crParameterDiscreteValue.Value = ReportType;
            crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["ReportType"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

            //
            CrystalReportViewer1.ReportSource = report;
            CrystalReportViewer1.DataBind();
        }





        private void fnSaleStatus(string query)
        {
            SqlCommand cmd = new SqlCommand(query, vcon);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "SaleStatus");
            //
            ReportDocument report = new ReportDocument();
            report.Load(Server.MapPath("crSaleStatusReport.rpt"));
            report.SetDataSource(ds.Tables["SaleStatus"]);

            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

            string ReportType = Session["ReportType"].ToString();
            crParameterDiscreteValue.Value = ReportType;
            crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["ReportType"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

            //
            CrystalReportViewer1.ReportSource = report;
            CrystalReportViewer1.DataBind();
        }





        private void fnPaymentReceipt(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "PaymentReceipt");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crPaymentReceipt.rpt"));
                report.SetDataSource(ds.Tables["PaymentReceipt"]);
                //


                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                string payment = Session["payment"].ToString();
                crParameterDiscreteValue.Value = payment;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["payment"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                //crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                string comName = Session["comName"].ToString();
                crParameterDiscreteValue.Value = comName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["comName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                //crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnTransReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "TransReport");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crTransaction.rpt"));
                report.SetDataSource(ds.Tables["TransReport"]);
                //

                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                string type = Session["Type"].ToString();
                crParameterDiscreteValue.Value = type;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["type"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                string reportName = Session["reportName"].ToString();
                crParameterDiscreteValue.Value = reportName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reportName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                //crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //

                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnCashReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CashReportInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCashReport.rpt"));
                report.SetDataSource(ds.Tables["CashReportInfo"]);
                //

                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                string reportName = Session["reportName"].ToString();
                crParameterDiscreteValue.Value = reportName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reportName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                //crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //

                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnECancelList(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "StockStatusInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCancelList.rpt"));
                report.SetDataSource(ds.Tables["StockStatusInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnOfferList(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "OfferInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crOfferList.rpt"));
                report.SetDataSource(ds.Tables["OfferInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnEcommerceList(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Ecommerce");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crEcommerceList.rpt"));
                report.SetDataSource(ds.Tables["Ecommerce"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnCustomer(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CustomerInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCustomer.rpt"));
                report.SetDataSource(ds.Tables["CustomerInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnCustomerList(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CustomerInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCustomerList.rpt"));
                report.SetDataSource(ds.Tables["CustomerInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnSupplier(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "SupplierInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crSupplier.rpt"));
                report.SetDataSource(ds.Tables["SupplierInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnStaff(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "StaffInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crStaff.rpt"));
                report.SetDataSource(ds.Tables["StaffInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnSupplierListReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "SupplierInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crSupplierListReport.rpt"));
                report.SetDataSource(ds.Tables["SupplierInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnExpenseReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CashReportInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crExpenseReport.rpt"));
                report.SetDataSource(ds.Tables["CashReportInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnCategoryInfo(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CategoryInfo");


                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCategoryInfo.rpt"));
                report.SetDataSource(ds.Tables["CategoryInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();

                //lblErrorMsg.Text = Login.pageName + " 203 " + Login.query;
            }
            catch (Exception)
            {
            }
        }





        private void fnCashCatInfo(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CashCatInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCashCatInfo.rpt"));
                report.SetDataSource(ds.Tables["CashCatInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnStock(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "StockInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crStock.rpt"));
                report.SetDataSource(ds.Tables["StockInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnImage(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Barcode");
                //
                ReportDocument report = new ReportDocument();


                var printBarcodeFor = commonFunction.findSettingItemValueDataTable("printBarcodeFor");

                if (printBarcodeFor == "Product")
                {
                    report.Load(Server.MapPath("crProdImage.rpt"));
                }
                else if (printBarcodeFor == "Category")
                {
                    report.Load(Server.MapPath("crProdImageByCategory.rpt"));
                }
                else if (printBarcodeFor == "Company")
                {
                    report.Load(Server.MapPath("crProductImageByCompany.rpt"));

                }
                else if (printBarcodeFor == "Sku")
                {
                    report.Load(Server.MapPath("crProductImageBySku.rpt"));
                }


                report.SetDataSource(ds.Tables["Barcode"]);


                //string url1 = "http://" + HttpContext.Current.Request.Url.Authority;

                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                // a = "http://localhost:4350/", "http://aanshaa.com/";

                //string hostUrl = HttpContext.Current.Request.Url.Host;
                //if (hostUrl == "localhost")
                //   hostUrl = "http://localhost:4355/";
                //else
                //    hostUrl = "http://" + hostUrl+"/";

                //string url = hostUrl==""? Session["urlPath"].ToString() : hostUrl;
                string url =Session["urlPath"].ToString();
                crParameterDiscreteValue.Value = url;

                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["url"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                string comName = Session["comName"].ToString();
                crParameterDiscreteValue.Value = comName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["comName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnStockReturn(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "StockStatusInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crReturn.rpt"));
                report.SetDataSource(ds.Tables["StockStatusInfo"]);

                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                string reportName = Session["pageName"] + " Report";
                crParameterDiscreteValue.Value = reportName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reportName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnPurshase(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CashReportInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCashReport.rpt"));
                report.SetDataSource(ds.Tables["CashReportInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnSlip(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "SlipInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crSlipReport.rpt"));
                report.SetDataSource(ds.Tables["SlipInfo"]);
                // work here SlipReport
                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                string reportName = Session["reportName"].ToString();
                crParameterDiscreteValue.Value = reportName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["reportName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                //crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //

                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnBank(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "BankNameInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crBankInfo.rpt"));
                report.SetDataSource(ds.Tables["BankNameInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnBankingReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CashReportInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crBankingReport.rpt"));
                report.SetDataSource(ds.Tables["CashReportInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnSizeReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "SizeInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crSizeReport.rpt"));
                report.SetDataSource(ds.Tables["SizeInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnStaffListReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "StaffInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crStaffReport.rpt"));
                report.SetDataSource(ds.Tables["StaffInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnPackage(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "PackageInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crPackageInfo.rpt"));
                report.SetDataSource(ds.Tables["PackageInfo"]);
                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }


        private void fnCustomerLedgerReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "CustomerLedgerReport");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crCustomerLedgerReport.rpt"));
                report.SetDataSource(ds.Tables["CustomerLedgerReport"]);
                //

                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                string comName = Session["comName"].ToString();
                crParameterDiscreteValue.Value = comName;
                crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["comName"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;
                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }


        private void fnWarningReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "warningReport");

                //
                ReportDocument report = new ReportDocument();

                report.Load(Server.MapPath("crWarningReport.rpt"));
                report.SetDataSource(ds.Tables["warningReport"]);

                //ParameterFieldDefinitions crParameterFieldDefinitions;
                //ParameterFieldDefinition crParameterFieldDefinition;
                //ParameterValues crParameterValues = new ParameterValues();
                //ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                //string comName = Session["comName"].ToString();
                //crParameterDiscreteValue.Value = comName;
                //crParameterFieldDefinitions = report.DataDefinition.ParameterFields;
                //crParameterFieldDefinition = crParameterFieldDefinitions["comName"];
                //crParameterValues = crParameterFieldDefinition.CurrentValues;
                ////crParameterValues.Clear();
                //crParameterValues.Add(crParameterDiscreteValue);
                //crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                //
                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();
            }
            catch (Exception)
            {
            }
        }





        private void fnExpiryReport(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "expiryReport");

                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crExpiryReport.rpt"));
                report.SetDataSource(ds.Tables["expiryReport"]);

                CrystalReportViewer1.ReportSource = report;
                CrystalReportViewer1.DataBind();

            }
            catch (Exception)
            {

            }
        }



        public void scriptMessage(string msg)
        {
            string title = "Notification Area";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), title, "alert('" + msg + "');", true);
        }


    }


}