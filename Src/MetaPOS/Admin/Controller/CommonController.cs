using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using MetaPOS.Admin.DataAccess;
using System.IO;
using MetaPOS.Admin.Model;
using DataTable = System.Data.DataTable;


namespace MetaPOS.Admin.Controller
{


    public class CommonController
    {


        protected string output = "";



        public string getUserAccessParametersByGroupId(string branchId)
        {
            var objSql = new SqlOperation();

            var dtUserAccessParameters =
                objSql.getDataTable("SELECT * FROM RoleInfo WHERE roleId='" + branchId + "' OR branchId ='" + branchId + "' ");

           
            var userAccessParameters = "";
            for (int count = 0; count < dtUserAccessParameters.Rows.Count; count++)
            {
                if (count == 0)
                    userAccessParameters = " ( ";

                if (count == dtUserAccessParameters.Rows.Count - 1)
                    userAccessParameters += "roleId = '" + dtUserAccessParameters.Rows[count][0] +
                                            "' ) ";
                else
                    userAccessParameters += "roleId = '" + dtUserAccessParameters.Rows[count][0] +
                                            "' OR ";
            }

            return userAccessParameters;
        }



        public string getCreateParameter(Dictionary<string, string> dicData)
        {
            output = "";
            foreach (KeyValuePair<string, string> item in dicData)
            {
                output += item.Value + ',';
            }

            return output.Remove(output.Length - 2);
        }





        public string getUpdateParameter(Dictionary<string, string> dictData)
        {
            output = "";

            foreach (KeyValuePair<string, string> item in dictData)
            {
                output += item.Key + "='" + item.Value + "', ";
            }

            return output.Remove(output.Length - 2);
        }





        public string getConditinalParameter(Dictionary<string, string> dictData)
        {
            output = "";

            foreach (KeyValuePair<string, string> item in dictData)
            {
                if (item.Key == "")
                    output += item.Value + " AND ";
                else
                    output += item.Key + "='" + item.Value + "' AND ";
            }

            return output.Remove(output.Length - 4);
        }





        public string getMultiStockUpdateParameter(Dictionary<string, string> dictData, string len)
        {
            output = "";
            int lenCounter = len.Length;


            foreach (KeyValuePair<string, string> item in dictData)
            {
                string collumn = item.Key.Substring((Convert.ToInt32(item.Key.Length) - lenCounter),
                    Convert.ToInt32(item.Key.Length));
                int lenNumber = Convert.ToInt32(collumn);
                int collNumber = Convert.ToInt32(collumn);


                output += item.Key.Substring(0, Convert.ToInt32(item.Key.Length) - lenCounter) + "='" + item.Value +
                          "', ";
            }

            return output.Remove(output.Length - 2);
        }





        public string getMultiStockConditinalParameter(Dictionary<string, string> dictData, string len)
        {
            output = "";
            int lenCounter = len.Length;

            foreach (KeyValuePair<string, string> item in dictData)
            {
                output += item.Key.Substring(0, Convert.ToInt32(item.Key.Length) - lenCounter) + "='" + item.Value +
                          "'AND ";
            }

            return output.Remove(output.Length - 4);
        }





        // Number to word conversation
        public string AmountToWord(int number)
        {
            if (number == 0) return "Zero";

            if (number == -2147483648)
                return
                    "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";

            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }

            string[] words0 =
            {
                "", "One ", "Two ", "Three ", "Four ",
                "Five ", "Six ", "Seven ", "Eight ", "Nine "
            };

            string[] words1 =
            {
                "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
                "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen "
            };

            string[] words2 =
            {
                "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
                "Seventy ", "Eighty ", "Ninety "
            };

            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs

            //You can increase as per your need.

            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }

            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;

                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens

                if (h > 0) sb.Append(words0[h] + "Hundred ");

                if (u > 0 || t > 0)
                {
                    if (h == 0)
                        sb.Append("");
                    else if (h > 0 || i == 0) sb.Append("and ");


                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }

                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }





        public string getDomainPartOnly()
        {
            string url = HttpContext.Current.Request.Url.Authority;

            try
            {
                if (url.Substring(0, 11) == "http://www.")
                {
                    url = url.Substring(11, url.Length - 11);
                }
                else if (url.Substring(0, 4) == "www.")
                {
                    url = url.Substring(4, url.Length - 4);
                }
                else
                {
                    url = HttpContext.Current.Request.Url.Host;
                }
            }
            catch (Exception)
            {
                url = HttpContext.Current.Request.Url.Host;
            }

            return url;
        }





       

    }


}