using System;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.Print
{


    public partial class ReceiptInvoiceLoader : System.Web.UI.Page
    {


        private ReceiptInvoice obReceiptInvoice = new ReceiptInvoice();





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var value = obReceiptInvoice.ReceiptPrint();
                dvReport.InnerHtml = value;
            }
        }


    }


}