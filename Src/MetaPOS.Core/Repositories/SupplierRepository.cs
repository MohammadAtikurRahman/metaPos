using MetaPOS.Core.Interfaces;
using MetaPOS.Entities.Dto;
using MetaPOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Repositories
{
    public class SupplierRepository : ISupplier
    {
        public string Add(SupplierDto supplier)
        {
            throw new NotImplementedException();
        }

        public string Delete(SupplierDto supplier)
        {
            throw new NotImplementedException();
        }

        public bool GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetData()
        {
            throw new NotImplementedException();
        }

        public DataTable GetData(SupplierDto supplier, SqlConnection con)
        {
            var supplierModel = new SupplierModel();
            supplierModel.From = supplier.From;
            supplierModel.To = supplier.To;
            supplierModel.storeAccessParameter = supplier.storeAccessParameter;
            return supplierModel.GetSupplierRecivedAmount();
        }

        public DataTable GetData(SupplierDto supplier)
        {
            var supplierModel = new SupplierModel();
            supplierModel.From = supplier.From;
            supplierModel.To = supplier.To;
            supplierModel.storeAccessParameter = supplier.storeAccessParameter;
            return supplierModel.GetSupplierRecivedAmount();
        }

        public DataTable GetDataCondition(string condition)
        {
            throw new NotImplementedException();
        }

        public string Update(SupplierDto supplier)
        {
            throw new NotImplementedException();
        }
    }
}
