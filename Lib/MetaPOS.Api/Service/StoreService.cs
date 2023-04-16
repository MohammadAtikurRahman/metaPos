using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaPOS.Api.Common;
using MetaPOS.Api.Entity;
using MetaPOS.Api.Models;

namespace MetaPOS.Api.Service
{
    public class StoreService
    {
        private CommonFunction commonFunction = new CommonFunction();

        public string roleid { get; set; }

        public string shopname { get; set; }

        public List<DataStatus> StoreList()
        {
            var dataStatus = new List<DataStatus>();
            try
            {
                if (!commonFunction.CheckConnectionString(shopname))
                {
                    dataStatus.Add(new DataStatus(){status = "400"});
                    return dataStatus;
                }

                var roleList = commonFunction.getRoleIdByBranchID(roleid, shopname);

                var roleModel = new RoleModel();
                var dtStoreList = roleModel.getStoreList(roleList, shopname);
                var storeList = new List<object>();
                for (int i = 0; i < dtStoreList.Rows.Count; i++)
                {
                    storeList.Add(new Store() { id = dtStoreList.Rows[i]["Id"].ToString(), name = dtStoreList.Rows[i]["name"].ToString() });
                }

                dataStatus.Add(new DataStatus() { status = "200", data = storeList });
            }
            catch (Exception)
            {
                dataStatus.Add(new DataStatus() { status = "404" });
            }
            return dataStatus;
        }


    }
}
