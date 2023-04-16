using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetaPOS.Admin.InventoryBundle.Entities
{
    public class Stock
    {
        // Initialize Database perameter
        public int prodId { get; set; }
        public string prodCode { get; set; }
        public string prodName { get; set; }
        public string prodDescr { get; set; }
        public string supCompany { get; set; }
        public string catName { get; set; }
        public string qty { get; set; }
        public decimal bPrice { get; set; }
        public decimal sPrice { get; set; }
        public decimal weight { get; set; }
        public string size { get; set; }
        public decimal discount { get; set; }
        public decimal stockTotal { get; set; }
        public string status { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime statusDate { get; set; }
        public string entryQty { get; set; }
        public string title { get; set; }
        public string roleId { get; set; }
        public string billNo { get; set; }
        public string branchId { get; set; }
        public string groupId { get; set; }
        public string tax { get; set; }
        public string sku { get; set; }
        public string fieldAttribute { get; set; }
        public string lastQty { get; set; }
        public string productSource { get; set; }
        public string prodCodes { get; set; }

        public string imei { get; set; }
        public string fieldId { get; set; }
        public string attributeId { get; set; }

        public decimal supCommission { get; set; }
        public decimal dealerPrice { get; set; }

        public string createdFor { get; set; }

        public string unitId { get; set; }
        public bool isPackage { get; set; }

        public string cecishNumber { get; set; }
        public string engineNumber { get; set; }

        public string searchType { get; set; }

        public string storeId { get; set; }
        public string returnImei { get; set; }

        public string offer { get; set; }

        public string offerType { get; set; }
        public bool isOfferQty { get; set; }

        public string fieldRecord { get; set; }

        public string attributeRecord { get; set; }

        public string balanceQty { get; set; }
        public string salesPersonId { get; set; }
        public string referredBy { get; set; }
    }
}