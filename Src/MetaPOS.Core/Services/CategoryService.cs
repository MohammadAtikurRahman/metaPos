using MetaPOS.Core.Interfaces;
using MetaPOS.Entities.ProductAggregate;
using MetaPOS.Infrastructure;
using MetaPOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Services
{
    public class CategoryService
    {
        public bool isCategoryExists(Category category)
        {
            var categoryModel = new CategoryModel();
            var dtCategory = categoryModel.GetCategoryId(category.Id);
            if (dtCategory.Rows.Count > 0)
                return true;

            return false;
        }
    }
}
