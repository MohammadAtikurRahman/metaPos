using MetaPOS.Core.Interfaces;
using MetaPOS.Core.Services;
using MetaPOS.Entities.RecordAggregate;
using MetaPOS.Entities.UserAggregate;
using MetaPOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Repositories
{
    public class StoreRepository : IStore
    {
        public string Add(Store category)
        {
            throw new NotImplementedException();
        }

        public string Delete(Store category)
        {
            throw new NotImplementedException();
        }

        public bool GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetData()
        {
            var storeModel = new StoreModel();
            return storeModel.getStore();
        }

        public DataTable GetDataCondition(string condition)
        {
            var storeModel = new StoreModel();
            return storeModel.getStoreListDataCondition(condition);
        }

        public string Update(Store store)
        {
            throw new NotImplementedException();
        }


        public DataTable getBranchRoleList(int roleId)
        {
            var roleModel = new RoleModel();
            roleModel.RoleId = roleId;
            return roleModel.getBranchRoleList();
        }
    }
}
