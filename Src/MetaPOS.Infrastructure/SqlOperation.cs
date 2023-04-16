using MetaPOS.Infrastructure.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace MetaPOS.Infrastructure
{
    public class SqlOperation
    {

        public string executeQuery(string query)
        {
            try
            {
                var messageService = new MessageService();

                var AccountService = new LoggingService();
                if (!AccountService.isLoggedIn())
                {
                    messageService.status = "440";
                    messageService.message = "Session timeout";
                    return JsonConvert.SerializeObject(messageService);
                }

                var connection = new ExecutionService();
                if (!connection.updateConnectionString())
                {
                    messageService.status = "421";
                    messageService.message = "Connection is not valid";
                    return JsonConvert.SerializeObject(messageService);
                }

                connection.query = query;
                if (!connection.execute())
                {
                    messageService.status = "400";
                    messageService.message = "Error has occurred!";
                    return JsonConvert.SerializeObject(messageService);
                }


                messageService.status = "200";
                messageService.message = "Data Saved Sucessfully";
                return JsonConvert.SerializeObject(messageService);
            }
            catch (Exception ex)
            {
                var messageService = new MessageService();
                messageService.status = "400";
                messageService.message = "Sorry! Operation Failed." + ex;
                return JsonConvert.SerializeObject(messageService);
            }
        }




        public string executeQueryWithoutAuth(string query)
        {
            try
            {
                var messageService = new MessageService();
                var connection = new ExecutionService();
                if (!connection.updateConnectionString())
                {
                    messageService.status = "421";
                    messageService.message = "Connection is not valid";
                    return JsonConvert.SerializeObject(messageService);
                }

                connection.query = query;
                if (!connection.execute())
                {
                    messageService.status = "400";
                    messageService.message = "Error has occurred!";
                    return JsonConvert.SerializeObject(messageService);
                }


                messageService.status = "200";
                messageService.message = "Data Saved Sucessfully";
                return JsonConvert.SerializeObject(messageService);
            }
            catch (Exception ex)
            {
                var messageService = new MessageService();
                messageService.status = "400";
                messageService.message = "Sorry! Operation Failed." + ex;
                return JsonConvert.SerializeObject(messageService);
            }
        }

        public DataTable getDataTable(string query)
        {
            try
            {
                var connection = new ExecutionService();
                connection.query = query;
                connection.updateConnectionString();
                return connection.executeDatatable();
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }


        public DataTable getDataTable(string query, SqlConnection connectionStr)
        {
            try
            {
                var connection = new ExecutionService();
                connection.query = query;
                connection.connection = connectionStr;
                return connection.executeDatatableTest();
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }

    }
}
