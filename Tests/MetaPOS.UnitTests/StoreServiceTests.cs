using MetaPOS.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.UnitTests
{
    [TestClass]
    public class StoreServiceTests
    {
        private StoreService storeService = new StoreService();

        [TestMethod]
        public void Store_Conditional_DataIsNotEmpty()
        {
            // 
            var dataTable = new DataTable();

            // Arrage
            var columnSpec = new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "Name"
            };
            dataTable.Columns.Add(columnSpec);

            // Act 
            var okResult = storeService.getConditionalData(dataTable);

            // Assert
            Assert.IsNotNull(okResult);

        }

    }
}
