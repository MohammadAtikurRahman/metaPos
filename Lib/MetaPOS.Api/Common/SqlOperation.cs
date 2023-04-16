using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using MetaPOS.Api.Common;


namespace MetaPOS.Api.Common
{


    public class SqlOperation
    {


        public string conString { get; set; }
        private SqlConnection vcon;


        


        public string updateConnectionString()
        {
            try
            {
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

                if (HttpContext.Current.Session["roleId"].ToString() == "")
                    return "";

                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                return "Operation Successful.";
            }
            catch (Exception ex)
            {
                vcon.Close();
                return "Sorry! Operation Failed." + ex;
            }
        }

        //for api data excution in stockStatusInfo
        public string executeQueryWithoutRole(string query)
        {
            try
            {
                updateConnectionString();

                //if (HttpContext.Current.Session["roleId"].ToString() == "")
                //    return "";

                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                return "Operation Successful.";
            }
            catch (Exception ex)
            {
                vcon.Close();
                return "Sorry! Operation Failed." + ex;
            }
        }



        public string executeQueryWithoutAuth(string query)
        {
            try
            {
                if (updateConnectionString() == "error")
                    return "Sorry! Operation Failed.";

                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                return "Operation Successful.";
            }
            catch (Exception ex)
            {
                vcon.Close();
                return "Sorry! Operation Failed." + ex;
            }
        }

        public bool fireQueryWithoutAuth(string query)
        {
            try
            {
                if (updateConnectionString() == "error")
                    return false;

                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                return true;
            }
            catch (Exception)
            {
                vcon.Close();
                return false;
            }
        }


        public string executeQueryScalar(string query)
        {
            try
            {
                updateConnectionString();

                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                var Id = cmd.ExecuteScalar();
                vcon.Close();
                return Id.ToString();
            }
            catch (Exception)
            {
                vcon.Close();
                return "";
            }
        }




        public bool fireQuery(string query)
        {
            try
            {
                updateConnectionString();

                if (HttpContext.Current.Session["roleId"].ToString() == "")
                    return false;

                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                return true;
            }
            catch(Exception)
            {
                vcon.Close();
                return false;
            }
        }





        public DataSet getDataSet(string query)
        {
            try
            {
                updateConnectionString();

                var adp = new SqlDataAdapter(query, vcon);
                var ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }
        }





        public DataTable getDataTable(string query)
        {
            try
            {
                updateConnectionString();

                var adp = new SqlDataAdapter(query, vcon);
                var dt = new DataTable();
                adp.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return  new DataTable();
            }
        }



        

        public int countDataRows(string query)
        {
            try
            {
                updateConnectionString();

                var adp = new SqlDataAdapter(query, vcon);
                var dt = new DataTable();
                adp.Fill(dt);
                return dt.Rows.Count;
            }
            catch (Exception)
            {
                return 0;
            }
        }





        public SqlDataReader getDataReader(string query)
        {
            try
            {
                updateConnectionString();

                var cmd = new SqlCommand(query, vcon);
                vcon.Open();
                var reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string executeQueryWithLog(string query)
        {
            try
            {
                if (HttpContext.Current.Session["roleId"].ToString() == "")
                    return "";

                vcon.Open();
                var cmd = new SqlCommand(query, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                return "True|Operation Successful.| Query:" + query;
            }
            catch (Exception ex)
            {
                vcon.Close();
                return "False|Sorry! Operation Failed. | Error: " + ex + " | Query:" + query;
            }
        }



        public string getConnectionStringName()
        {

            string conString = "";
            try
            {
                conString = HttpContext.Current.Session["conString"].ToString();
            }
            catch (Exception)
            {
                conString = "dbPOS";
            }
            return conString;
        }

        public static string getConnectionStringNameStatic()
        {

            string conString = "";
            try
            {
                conString = HttpContext.Current.Session["conString"].ToString();
            }
            catch (Exception)
            {
                conString = "dbPOS";
            }
            return conString;
        }
    }


}