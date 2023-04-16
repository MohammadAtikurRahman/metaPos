using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using MetaPOS.Api.Service;

namespace MetaPOS.Admin.ApiBundle.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Login(string email, string password,string shopname)
        {
            var accountService = new AccountService();
            accountService.shopname = shopname;
            accountService.email = email;
            accountService.password = password;
            var login = accountService.Login();
            return Ok(login);
        }



        [HttpGet]
        public IHttpActionResult Storelist(string roleid, string shopname)
        {
            var storeService = new StoreService();
            storeService.roleid = roleid;
            storeService.shopname = shopname;
            var storeList = storeService.StoreList();
            return Ok(storeList);
        }
    }
}