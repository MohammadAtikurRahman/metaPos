using MetaPOS.Core.Repositories;
using MetaPOS.Core.Services;
using MetaPOS.Entities.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MetaPOS.UnitTests
{
    [TestClass]
    public class SupplierServiceTests
    {
        //SqlConnection vcon = MetaPOS.Admin.DataAccess.DefaultSqlConnection.vcon;
        private SupplierService supplierService = new SupplierService();

        [TestMethod]
        public void Supplier_Recived_AmountIsEmpty()
        {

            // Arrange
            var amount = "";

            // Act
            var okResult = supplierService.HasSupplierRecivedAmount(amount);

            // Assert
            var expect = false;
            Assert.AreEqual(expect, okResult);
        }


        [TestMethod]
        public void Supplier_Recived_AmountIsNotEmpty()
        {
            // Arrange
            var amount = "120";

            // Act 
            var okResult = supplierService.HasSupplierRecivedAmount(amount);

            // Assert
            var expect = true;
            Assert.AreEqual(expect, okResult);
        }


        [TestMethod]
        public void Supplier_Recived_AmountIsDecimal()
        {
            // Arrage 
            var amount = "100";

            // Act
            var okResult = supplierService.ConvertToDecimalSupplierRecivedAmount(amount);

            // Assert
            Assert.AreEqual(okResult, 100M);
        }


        [TestMethod]
        public void Supplier_Recived_AmountIsNotDecimal()
        {
            // Arrage
            var amount = "ABCd";

            // Act
            var okResult = supplierService.ConvertToDecimalSupplierRecivedAmount(amount);

            // Assert
            Assert.AreEqual(okResult, -1M);
        }


        [TestMethod]
        public void Get_Supplier_Recived_AmountIsGraterThenZero()
        {

            
            // Arrange
            string storeId = "74";
            var supplierReposity = new SupplierRepository();
            SupplierDto supplierDto = new SupplierDto();

            // setup
            supplierDto.From = DateTime.Now.AddDays(-30);
            supplierDto.To = DateTime.Now.AddDays(2);
            supplierDto.storeAccessParameter = " AND storeId='" + storeId + "'";


            // Act
            DataTable dt = supplierReposity.GetData(supplierDto);
            var totalRows = dt.Rows.Count;
            //var totalRows = 0;

            // Assert
            Assert.AreNotEqual(totalRows, 0);

        }
    }
}
