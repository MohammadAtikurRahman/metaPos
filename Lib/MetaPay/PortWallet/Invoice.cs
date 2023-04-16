using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaPay.PortWallet.Entities;
using MetaPay.PortWallet.RequestHandler;


namespace MetaPay.PortWallet
{
    public class Invoice
    {
        public Dictionary<string, string> _dicOrderData { get; set; }
        public Dictionary<string, string> _dicProductData { get; set; }
        public Dictionary<string, string> _dicCustomerData { get; set; }
        public Dictionary<string, string> _dicAddressData { get; set; }

        public string CreateInvoice()
        {
            var invoiceReq = new InvoiceRequest();
            invoiceReq.dicOrderData = _dicOrderData;
            invoiceReq.dicProductData = _dicProductData;
            invoiceReq.dicCustomerData = _dicCustomerData;
            invoiceReq.dicAddressData = _dicAddressData;
            var url = "https://api-sandbox.portwallet.com/payment/v2/invoice";
           return invoiceReq.invoicePostRequst(url);
        }
    }
}
