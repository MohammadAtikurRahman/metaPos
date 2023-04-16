using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MetaPOS.Api.Service;

namespace MetaPOS.Admin.ApiBundle.Controllers
{
    public class AccountingController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult Summary(DateTime startDate, DateTime endDate, string storeid,string shopname)
        {
            var summaryService = new AccountSummaryService();
            summaryService.storeid = storeid;
            summaryService.startdate = startDate;
            summaryService.enddate = endDate;
            summaryService.shopname = shopname;
            var accounts = summaryService.getAccountSummary();
            return Ok(accounts);
        }
    }
}