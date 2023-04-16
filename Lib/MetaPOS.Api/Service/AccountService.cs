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
    public class AccountService : Account
    {
        public CommonFunction CommonFunction = new CommonFunction();
        public List<DataStatus> Login()
        {
            var dataStatus = new List<DataStatus>();
            try
            {
                var accountsList = new List<object>();
                if (email == "" || password == "")
                {
                    dataStatus.Add(new DataStatus() { status = "404" });
                    return dataStatus;
                }


                if (!CommonFunction.CheckConnectionString(shopname))
                {
                    dataStatus.Add(new DataStatus() { status = "204" });
                    return dataStatus;
                }


                var accountModel = new AccountModel();
                accountModel.shopname = shopname;
                accountModel.email = email;
                accountModel.password = CommonFunction.Encrypt(password);
                var dtLogin = accountModel.getLoginData();
                if (dtLogin.Rows.Count <= 0)
                {
                    dataStatus.Add(new DataStatus() { status = "400" });
                    return dataStatus;
                }


                for (int i = 0; i < dtLogin.Rows.Count; i++)
                {
                    accountsList.Add(new Account()
                    {
                        shopname = shopname,
                        email = dtLogin.Rows[i]["email"].ToString(),
                        roleId = dtLogin.Rows[i]["roleId"].ToString(),
                        storeId = dtLogin.Rows[i]["storeId"].ToString(),
                        title = dtLogin.Rows[i]["title"].ToString(),
                        branchId = dtLogin.Rows[i]["branchId"].ToString(),
                        activedate = dtLogin.Rows[i]["activeDate"].ToString(),
                        expirydate = dtLogin.Rows[i]["expiryDate"].ToString(),
                        monthlyfee = dtLogin.Rows[i]["monthlyFee"].ToString(),
                        baseurl = dtLogin.Rows[i]["branchWebsite"].ToString()
                    });
                }

                dataStatus.Add(new DataStatus() { status = "200", data = accountsList });

            }
            catch (Exception)
            {
                dataStatus.Add(new DataStatus() { status = "404" });
            }
            return dataStatus;

        }
    }
}
