using MetaPOS.Entities.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPOS.Infrastructure.Data
{
    public class CategoryModel
    {

        SqlOperation sqlOperation = new SqlOperation();

        public string AddCategory(Category category)
        {
            string query = "INSERT INTO CategoryInfo " +
                "(catname,entrydate,updatedate,roleId,active) VALUES('" +
                category.CategoryName + "','" + category.EnteryDate + "','" +
                category.UpdateDate + "','" + category.RoleId + "','" +
                category.Active + "')";
            return sqlOperation.executeQuery(query);
        }



        public string UpdateCategory(Category category)
        {
            string query = "UPDATE CategoryInfo SET catName='" + category.CategoryName + "', entrydate='" + category.UpdateDate + "' where Id='" + category.Id + "'";
            return sqlOperation.executeQuery(query);
        }


        public DataTable GetCategoryId(int id)
        {
            string query = "SELECT * FROM CategoryInfo WHERE Id='" + id + "'";
            return sqlOperation.getDataTable(query);
        }
    }
}
