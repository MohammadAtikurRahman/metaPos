using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MetaPOS.Api.Service;

namespace MetaPOS.Admin.ApiBundle.Controllers
{
    public class LocationWaseSaleController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IHttpActionResult Report(string prodID, string code,string shopName)
        {
            var importProductService = new ImportProductService();
            var sale = importProductService.importProductApiReport(prodID, code, shopName);
            return Ok(sale);
        }

    }
}
