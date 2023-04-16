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
    public class MiscCostServiceTests
    {
        private MiscCostService miscCostService = new MiscCostService();

        [TestMethod]
        public void MiscCost_TotalAmount_IsEqual()
        {
            // Arrange
            miscCostService.LoadingCost = "100";
            miscCostService.UnloadingCost = "100";
            miscCostService.CarryingCost = "100";
            miscCostService.ShippingCost = "100";
            miscCostService.ServiceCharge = "100";

            // Act 
            var okResult = miscCostService.TotalMiscCostAmount();

            // Assert
            Assert.AreEqual(okResult, 500M);
        }

        [TestMethod]
        public void MiscCost_TotalAmount_IsNotEqual()
        {
            // Arrange
            miscCostService.LoadingCost = "100";
            miscCostService.UnloadingCost = "100";
            miscCostService.CarryingCost = "100";
            miscCostService.ShippingCost = "100";
            miscCostService.ServiceCharge = "100";

            // Act 
            var okResult = miscCostService.TotalMiscCostAmount();

            // Assert
            Assert.AreNotEqual(okResult, 550M);
        }


        [TestMethod]
        public void MiscCost_OneParameterIsNull_TotalAmount_IsEqual ()
        {
            // Arrange
            miscCostService.LoadingCost = "";
            miscCostService.UnloadingCost = "100";
            miscCostService.CarryingCost = "100";
            miscCostService.ShippingCost = "100";
            miscCostService.ServiceCharge = "100";

            // Act 
            var okResult = miscCostService.TotalMiscCostAmount();

            // Assert
            Assert.AreEqual(okResult, 400M);
        }


        [TestMethod]
        public void MiscCost_OneParamIsChar_TotalAmount_IsEqual()
        {
            // Arrange
            miscCostService.LoadingCost = "Abc";
            miscCostService.UnloadingCost = "100";
            miscCostService.CarryingCost = "100";
            miscCostService.ShippingCost = "100";
            miscCostService.ServiceCharge = "100";

            // Act 
            var okResult = miscCostService.TotalMiscCostAmount();

            // Assert
            Assert.AreEqual(okResult, 400M);
        }


    }
}
