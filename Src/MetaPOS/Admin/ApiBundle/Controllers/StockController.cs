using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MetaPOS.Api.Service;
using MetaPOS.Api.Entity;

namespace MetaPOS.Admin.ApiBundle.Controllers
{
    public class StockController : ApiController
    {
        // GET: products
        [System.Web.Http.HttpGet]
       // [System.Web.Http.ActionName("Product")]
        public IHttpActionResult AllProduct()
        {
            var productService = new ProductService();
            var products = productService.ProductList();
            return Ok(products);
        }

        // GET: Search products with prodName   ?q=productName
        [System.Web.Http.HttpGet]
        public IHttpActionResult AllProduct(string q)
        {
            var productService = new ProductService();
            var product = productService.ProductList(q);
            return Ok(product);
        }



        [System.Web.Http.HttpPost]
        public IHttpActionResult ProductLog(SoldProduct value)
        {
            var productService = new ProductService();
            var saleProduct = productService.ProductListPost(value);
            return Ok(saleProduct);
        }





        [System.Web.Http.HttpPut]
        public IHttpActionResult ProductUpdateResult(SoldProduct value)
        {
            return null;
        }



    }
}