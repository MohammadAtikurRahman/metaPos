using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MetaPOS.Api.Service;

namespace MetaPOS.Admin.ApiBundle.Controllers
{
    public class SaleController : ApiController
    {
        // GET: Sale
        [System.Web.Http.HttpGet]
        public IHttpActionResult Invoice(DateTime startDate, DateTime endDate, string storeid,string shopname)
        {
            var saleService = new SaleService();
            var sale = saleService.SaleSummary(startDate, endDate, storeid, shopname);
            return Ok(sale);
        }

        

        [System.Web.Http.HttpGet]
        public IHttpActionResult SaleGraphChart(string storeId, string type, string shopname)
        {
            var saleService = new SaleService();
            if (type == "area")
            {
                var sale = saleService.SaleAreaChart(storeId, shopname);
                return Ok(sale);
            }
            else if (type == "pie")
            {
                var sale = saleService.SalePieChart(storeId, shopname);
                return Ok(sale);
            }

            return Ok();
        }


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("SaleAreaChart")]
        //public IHttpActionResult SaleAreaChart(string roleId)
        //{
        //    var saleService = new SaleService();
        //    var sale = saleService.SaleAreaChart(roleId);

        //    //var sale = @"[{'id':'1','date':'Jan/2019','SaleAmt':'2500'},{'id':'2','date':'Feb/2019','SaleAmt':'8000'},{'id':'3','date':'Mar/2019','SaleAmt':'200'},{'id':'4','date':'Apr/2019','SaleAmt':'4700'},{'id':'5','date':'May/2019','SaleAmt':'5000'}]";
        //    return Ok(sale);
        //}


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("SalePieChart")]
        //public IHttpActionResult SalePieChart(string roleId)
        //{
        //    var saleService = new SaleService();
        //    var sale = saleService.SalePieChart("");

        //    return Ok(sale);
        //}
    }
}