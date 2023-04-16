using MetaPOS.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface IRole
    {
        bool GetById(int Id);
        DataTable GetData();
        DataTable GetDataCondition(string condition);
        string Add(Role category);
        string Update(Role category);
        string Delete(Role category);
    }
}
