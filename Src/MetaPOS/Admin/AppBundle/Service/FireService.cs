using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MetaPOS.Admin.DataAccess;


namespace MetaPOS.Admin.AppBundle.Service
{
    public class FireService
    {
        //private SqlConnection vcon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbPOS"].ToString());
        private CommonService commonService = new CommonService();
        private static string conString = GlobalVariable.getConnectionStringName();
        private static SqlConnection vcon = new SqlConnection(ConfigurationManager.ConnectionStrings[conString].ToString());


        public string updateConnectionString()
        {
            try
            {
                conString = GlobalVariable.getConnectionStringName();
                vcon = new SqlConnection(ConfigurationManager.ConnectionStrings[conString].ToString());
                return conString;
            }
            catch (Exception ex)
            {
                var d = ex;
                return "error";
            }
        }

        public string executeQuery(string query)
        {
            try
            {
                updateConnectionString();

                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                return "success";
            }
            catch(Exception ex)
            {
                vcon.Close();
                return "Sorry! Operation failed. " + query + " " + ex;
            }
        }





        public string countDataRows(string query)
        {
            try
            {
                updateConnectionString();

                var adp = new SqlDataAdapter(query, vcon);
                var dt = new DataTable();
                adp.Fill(dt);
                return dt.Rows.Count > 0 ? "success" : "Sorry! Data not found. " + query;
            }
            catch(Exception ex)
            {
                return "Sorry! Operation failed. " + query + " " + ex;
            }
        }



        public string getDataTable(string query)
        {
            try
            {
                updateConnectionString();

                var adp = new SqlDataAdapter(query, vcon);
                var dt = new DataTable();
                adp.Fill(dt);
                //return dt.Rows.Count > 0 ? commonService.serializeDatatableToJson(dt) : "Sorry! Data not found. " + query;
                return commonService.serializeDatatableToJson(dt);
            }
            catch(Exception ex)
            {
                return "Sorry! Operation failed. " + query + " " +  ex;
            }
        }





    }
}