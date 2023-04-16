using System;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Print
{


    public partial class InvoiceLoader : System.Web.UI.Page
    {
        private CommonFunction objCommonFun = new CommonFunction();
        private ShortInvoice objShortInvoice = new ShortInvoice();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string value = "";
                var invoiceLoadType = Request["type"];
                var billNo = Request["billNo"];
                ShortInvoice.printBillingNo = billNo;

                if (invoiceLoadType == "3")
                {
                    //MainInvoice.invoiceLoadType = "3";
                    var receiptInvoice = new ReceiptInvoice();
                    value = receiptInvoice.ReceiptPrint();
                }

                dvReport.InnerHtml = value;
            }
        }


    }


}