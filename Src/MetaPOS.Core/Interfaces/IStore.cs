using MetaPOS.Entities.RecordAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Interfaces
{
    public interface IStore
    {
        bool GetById(int Id);
        DataTable GetData();
        DataTable GetDataCondition(string condition);
        string Add(Store category);
        string Update(Store category);
        string Delete(Store category);
        DataTable getBranchRoleList(int roleId);
    }
}
