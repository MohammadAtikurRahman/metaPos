using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Infrastructure.Data
{
    public class SalesProfitModel : SearchDto
    {
        private SqlOperation sqlOperation = new SqlOperation();

        public DataTable ProfitAmount()
        {
            string query = "SELECT DISTINCT BillNo, prodId,SUM((sPrice-bPrice)* CAST(qty as decimal)) as balance,min(commission) as commission, min(bPrice) as bPrice FROM StockStatusInfo where ((status='sale' AND isPackage='false') OR (status='salePackage' AND isPackage='true')) AND (entryDate >='" + From + "' AND entryDate <='" + To + "') AND BillNo !='' " + storeAccessParameter + " GROUP BY BillNo,prodId";
            return sqlOperation.getDataTable(query);
        }



        public DataTable ReturnAmount()
        {
            string query = "SELECT DISTINCT BillNo,SUM((sPrice-bPrice)*CAST(qty as decimal)) as balance FROM StockStatusInfo where ((status='saleReturn' AND isPackage='false' AND searchType='product') OR (status='saleReturn' AND isPackage='true' AND searchType='salePackage') OR (status='saleReturn' AND isPackage='false' AND searchType='service')) AND(entryDate >='" + From + "' AND entryDate <='" + To + "')  AND BillNo !='' " + storeAccessParameter + " GROUP BY BillNo";
            return sqlOperation.getDataTable(query);
        }



        public DataTable DiscountAmount()
        {
            string query = "SELECT distinct billNo, discAmt FROM SaleInfo WHERE status='1' AND (CAST(entryDate AS DATE) >='" + From + "' AND CAST(entryDate AS DATE) <='" + To + "') " + storeAccessParameter + " ";
            return sqlOperation.getDataTable(query);
        }

        

        public DataTable SupplierCommission()
        {
            string query = "SELECT DISTINCT BillNo, prodId,SUM((sPrice-bPrice)* CAST(qty as decimal)) as balance,min(commission) as commission, min(bPrice) as bPrice FROM StockStatusInfo where ((status='sale' AND isPackage='false') OR (status='salePackage' AND isPackage='true')) AND (entryDate >='" + From + "' AND entryDate <='" + To + "') AND BillNo !='' " + storeAccessParameter + " GROUP BY BillNo,prodId";
            return sqlOperation.getDataTable(query);
        }
    }
}
