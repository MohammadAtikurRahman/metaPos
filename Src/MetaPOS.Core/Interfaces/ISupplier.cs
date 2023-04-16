using MetaPOS.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface ISupplier
    {
        bool GetById(int Id);
        DataTable GetData();
        DataTable GetDataCondition(string condition);
        DataTable GetData(SupplierDto supplier);
        DataTable GetData(SupplierDto supplier, SqlConnection con);
        string Add(SupplierDto supplier);
        string Update(SupplierDto supplier);
        string Delete(SupplierDto supplier);
    }
}
