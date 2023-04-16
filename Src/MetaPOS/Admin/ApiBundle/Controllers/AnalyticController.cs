using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MetaPOS.Api.Service;

namespace MetaPOS.Admin.ApiBundle.Controllers
{
    public class AnalyticController : ApiController
    {
        // GET: Sale
        [System.Web.Http.HttpGet]
        public IHttpActionResult Report(DateTime startDate, DateTime endDate, string storeid, string shopname)
        {
            var analyticService = new AnalyticService();
            var sale = analyticService.AnalyticReport(startDate, endDate, storeid, shopname);
            return Ok(sale);
        }

    }
}