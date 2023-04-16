using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace MetaPOS.Infrastructure.Services
{
    public class ExecutionService
    {
        public string query { get; set; }
        public SqlConnection connection { get; set; }

        private static SqlConnection vcon = new SqlConnection(ConfigurationManager.ConnectionStrings[HttpContext.Current.Session["conString"].ToString() == "" ? "dbPOS" : HttpContext.Current.Session["conString"].ToString()].ToString());

        public bool updateConnectionString()
        {
            try
            {
                vcon = new SqlConnection(ConfigurationManager.ConnectionStrings[HttpContext.Current.Session["conString"].ToString() == "" ? "dbPOS" : HttpContext.Current.Session["conString"].ToString()].ToString());
                return true;
            }
            catch (Exception ex)
            {
                vcon = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbPOS"].ConnectionString);
                return false;
            }
        }


        public bool execute()
        {
            try
            {
                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();

                return true;
            }
            catch (Exception ex)
            {
                vcon.Close();
                return false;
            }
        }

        public DataTable executeDatatable()
        {
            vcon.Open();
            var adp = new SqlDataAdapter(query, vcon);
            var dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        public DataTable executeDatatableTest()
        {
            connection.Open();
            var adp = new SqlDataAdapter(query, connection);
            var dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }
    }
}
