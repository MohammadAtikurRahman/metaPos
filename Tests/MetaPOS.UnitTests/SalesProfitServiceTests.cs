using MetaPOS.Core.Services.Summary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.UnitTests
{
    [TestClass]
    public class SalesProfitServiceTests
    {
        private SalesProfitService salesProfitService = new SalesProfitService();

        [TestMethod]
        [TestCategory("Sales Profit")]
        [TestProperty("Sales Profit","Security")]
        public void SalesProfit_TotalAmount_IsEqual()
        {
            // Arrange
            salesProfitService.ProfitAmount = 500M;
            salesProfitService.SupplierCommissionAmount = 200M;
            salesProfitService.ReturnAmount = 200M;
            salesProfitService.DiscountAmount = 100M;

            // Act
            var salesProfit = salesProfitService.GetSalesProfitAmount();

            // Assert
            Assert.AreEqual(salesProfit, 400M);
        }
    }
}
