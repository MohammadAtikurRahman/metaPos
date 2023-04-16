using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace MetaPOS.Admin.Print
{


    public partial class PrintQuery : System.Web.UI.Page
    {


        private SqlConnection vcon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbPOS"].ToString());





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fnSale();
            }

            if (Session["pageName"].ToString() == "paymentReport")
                fnCusPaymentReport(Session["reportQury"].ToString());
        }





        private void fnSale()
        {
            try
            {
                string query = "SELECT * FROM [TempPrintSaleInfo]";
                SqlCommand cmd = new SqlCommand(query, vcon);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "TempPrintSaleInfo");
                //
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("crSale.rpt"));
                report.SetDataSource(ds.Tables["TempPrintSaleInfo"]);
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


    }


}