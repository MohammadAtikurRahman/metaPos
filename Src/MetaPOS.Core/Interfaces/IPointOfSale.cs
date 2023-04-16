using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface IPointOfSale
    {
        bool GetById(int Id);
        DataTable GetData();
        DataTable GetDataCondition(string condition);
    }
}
