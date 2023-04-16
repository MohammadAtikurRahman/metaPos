using MetaPOS.Core.Interfaces;
using MetaPOS.Core.Services;
using MetaPOS.Entities.MessageAggregate;
using MetaPOS.Entities.ProductAggregate;
using MetaPOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Core.Repositories
{
    public class CategoryRepositoy : ICategory
    {
        public string Add(Category category)
        {
            var categoryModel = new CategoryModel();
            return categoryModel.AddCategory(category);
        }

        public string Update(Category category)
        {
            var categoryService = new CategoryService();
            if (!categoryService.isCategoryExists(category))
            {
                var returnMessage = new ReturnMessage();
                returnMessage.status = "404";
                returnMessage.message = "Category is not found";
            }

            var categoryModel = new CategoryModel();
            return categoryModel.UpdateCategory(category);
        }

        public string Delete(Category category)
        {
            throw new NotImplementedException();
        }

        public bool GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetData()
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataCondition(string condition)
        {
            throw new NotImplementedException();
        }

        
    }
}
