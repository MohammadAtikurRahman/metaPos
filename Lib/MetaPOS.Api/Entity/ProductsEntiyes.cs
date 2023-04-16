using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Api.Entity
{
   public class ProductsEntiyes
    {
       //for stockStatusInfo
        public string prodCode { get; set; }
        public string prodDescr { get; set; }
        public string supCompany { get; set; }
        public string catName { get; set; }
        public string qty { get; set; }
        public string serialNo { get; set; }
        public decimal bPrice { get; set; }
        public string payDescr { get; set; }
        public string payType { get; set; }
        public string invoiceType { get; set; }
        public decimal weight { get; set; }
        public string size { get; set; }
        public string discount { get; set; }
        public decimal stockTotal { get; set; }
        public string status { get; set; }
        public string entryQty { get; set; }
        public string title { get; set; }
        public int roleId { get; set; }
        public int branchId { get; set; }
        public int groupId { get; set; }
        public string billNo { get; set; }
        public string fieldAttribute { get; set; }
        public string tax { get; set; }
        public string sku { get; set; }
        public string lastQty { get; set; }
        public string productSource { get; set; }
        public string prodCodes { get; set; }
        public string imei { get; set; }
        public string fieldId { get; set; }
        public string attributeId { get; set; }
        public decimal supCommission { get; set; }
        public decimal dealerPrice { get; set; }
        public string createdFor { get; set; }
        public string engineNumber { get; set; }
        public string cecishNumber { get; set; }
        public string balanceQty { get; set; }
        public string entryDate { get; set; }
        // public string prodId { get; set; }
        public string unitId { get; set; }
        public bool isPackage { get; set; }
        public string searchType { get; set; }
        public string offer { get; set; }
        public string offerType { get; set; }
        public bool isOfferQty { get; set; }
        public string fieldRecord { get; set; }
        public string attributeRecord { get; set; }
        public string salesPersonId { get; set; }
        public string referredBy { get; set; }

        //for seleInfo
        public string cusID { get; set; }
        public string netAmt { get; set; }
        public decimal discAmt { get; set; }
        public decimal vatAmt { get; set; }
        public decimal grossAmt { get; set; }
        public string payMethod { get; set; }
        public decimal payCash { get; set; }
        public string payCard { get; set; }
        public decimal giftAmt { get; set; }
        public decimal return_ { get; set; }
        public decimal balance { get; set; }
        public decimal sPrice { get; set; }
        public string discType { get; set; }
        public string comment { get; set; }
        public decimal currentCash { get; set; }
        public string cardId { get; set; }
        public int bankId { get; set; }
        public string warranty { get; set; }
        public string token { get; set; }
        public int CusType { get; set; }
        public string BankName { get; set; }
        public string checkNo { get; set; }
        public decimal loadingCost { get; set; }
        public decimal unloadingCost { get; set; }
        public decimal shippingCost { get; set; }
        public decimal carryingCost { get; set; }
        public string salePersonType { get; set; }
        public decimal additionalDue { get; set; }
        public string returnQty { get; set; }
        public decimal returnAmt { get; set; }
        public string refName { get; set; }
        public string refPhone { get; set; }
        public string refAddress { get; set; }
        public string vatType { get; set; }
        public decimal miscCost { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal PreviousDue { get; set; }
        public int interestRate { get; set; }
        public decimal interestAmt { get; set; }
        public decimal extraDiscount { get; set; }
        public decimal serviceCharge { get; set; }
        public bool isAutoSalesPerson { get; set; }

       //cashReportInfo
        public string cashType { get; set; }
        public string descr { get; set; }
        public decimal cashIn { get; set; }
        public decimal cashOut { get; set; }

        public decimal cashInHand { get; set; }
        public string mainDescr { get; set; }
        public char adjust { get; set; }
        public bool isSchedulePayment { get; set; }
        public bool isScheduled { get; set; }
        public bool isReceived { get; set; }
        public decimal trackAmt { get; set; }
        public int storeId { get; set; }
        public string purchaseCode { get; set; }
        public string cardType { get; set; }
        public DateTime maturityDate { get; set; }
        public string bankName { get; set; }
       



       //for customerInfo
        public string nextCusId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string mailInfo { get; set; }
        public string designation { get; set; }
        public string bloodGroup { get; set; }
        public string sex { get; set; }
        public string age { get; set; }

    }
}
