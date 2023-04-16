using MetaPOS.Entities.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface ICategory
    {
        bool GetById(int Id);
        DataTable GetData();
        DataTable GetDataCondition(string condition);
        string Add(Category category);
        string Update(Category category);
        string Delete(Category category);
    }
}
