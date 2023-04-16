 using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;


namespace MetaPOS.Admin.ImportBundle.View
{


    public partial class ImportInvoice : Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
        }





        public void scriptMessage(string msg)
        {
            string title = "Notification Area";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), title, "alert('" + msg + "');", true);
        }





        protected void btnImportInvoice_OnClick(object sender, EventArgs e)
        {
            importInvoice();
        }





        private void importInvoice()
        {
            try
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Invoice.xls"));
                Response.ContentType = "application/ms-excel";

                string[] fileEntries = Directory.GetFiles(@"E:\Projects\@Extras\ImportDAT\Invoices");

                // Initialize column name
                Response.Write("invoiceId");
                Response.Write("\t" + "customerName");
                Response.Write("\t" + "customerPhone");
                Response.Write("\t" + "customerPhone2");
                Response.Write("\t" + "customerEmail");
                Response.Write("\t" + "invoiceDate");
                Response.Write("\t" + "payMethod");
                Response.Write("\t" + "netAmt");
                Response.Write("\t" + "payCash");
                Response.Write("\t" + "item1");
                Response.Write("\t" + "qty1");
                Response.Write("\t" + "sku1");
                Response.Write("\t" + "price1");
                Response.Write("\n");

                foreach (string fileName in fileEntries)
                {
                    var objInput =
                        new StreamReader(
                            @"E:\Projects\@Extras\ImportDAT\Invoices\" + Path.GetFileName(fileName),
                            System.Text.Encoding.Default);

                    string contents = objInput.ReadToEnd().Trim();
                    string[] split = Regex.Split(contents, "&", RegexOptions.None);

                    Response.Write(Path.GetFileNameWithoutExtension(fileName));

                    // Processing static information
                    foreach (string keyval in split)
                    {
                        string[] keyvalSplit = keyval.Split('=');

                        if (keyvalSplit[0] == "Customer")
                        {
                            string[] fileEntriesTemp = Directory.GetFiles(@"E:\Projects\@Extras\ImportDAT\Customers");

                            foreach (string fileNameTemp in fileEntriesTemp)
                            {
                                if (Path.GetFileName(fileNameTemp) == keyvalSplit[1] + ".dat")
                                {
                                    var objInputTemp =
                                        new StreamReader(
                                            @"E:\Projects\@Extras\ImportDAT\Customers\" + Path.GetFileName(fileNameTemp),
                                            System.Text.Encoding.Default);

                                    string contentsTemp = objInputTemp.ReadToEnd().Trim();
                                    string[] splitTemp = Regex.Split(contentsTemp, "&", RegexOptions.None);
                                    string strTemp = "";

                                    foreach (string keyvalTemp in splitTemp)
                                    {
                                        string[] keyvalSplitTemp = keyvalTemp.Split('=');

                                        if (keyvalSplitTemp[0] == "ContactFirst" || keyvalSplitTemp[0] == "Phone" ||
                                            keyvalSplitTemp[0] == "Phone2" || keyvalSplitTemp[0] == "Email")
                                        {
                                            strTemp = "\t";

                                            Response.Write(strTemp +
                                                           keyvalSplitTemp[1].Replace("%20", " ")
                                                               .Replace("%2C", " -")
                                                               .Replace("%40", "@"));                                         
                                        }
                                    }
                                }
                            }
                        }
                        else if (keyvalSplit[0] == "PaymentTerms" || keyvalSplit[0] == "Date")
                        {
                            Response.Write("\t" + keyvalSplit[1]);
                        }
                        else if (keyvalSplit[0] == "Total" || keyvalSplit[0] == "AmountPaid")
                        {
                            Response.Write("\t" + (Convert.ToDouble(keyvalSplit[1])/100));
                        }
                    }

                    // Processing dynamic product information
                    foreach (string keyval in split)
                    {
                        string[] keyvalSplit = keyval.Split('=');

                        if (keyvalSplit[0].Substring(0, 4) == "Item")
                        {
                            int length = keyvalSplit[0].Length;

                            if (length > 11 && keyvalSplit[0].Substring(length - 11, 11) == "Description")
                            {
                                Response.Write("\t" +
                                               keyvalSplit[1].Replace("%20", " ")
                                                   .Replace("%2C", " -")
                                                   .Replace("%40", "@"));
                            }
                            else if (length > 4 && keyvalSplit[0].Substring(length - 4, 4) == "Code")
                            {
                                Response.Write("\t" +
                                               keyvalSplit[1].Replace("%20", " ")
                                                   .Replace("%2C", " -")
                                                   .Replace("%40", "@"));
                            }
                            else if (length > 3 && keyvalSplit[0].Substring(length - 3, 3) == "Qty")
                            {
                                Response.Write("\t" +
                                               keyvalSplit[1].Replace("%20", " ")
                                                   .Replace("%2C", " -")
                                                   .Replace("%40", "@"));
                            }
                            else if (length > 9 && keyvalSplit[0].Substring(length - 9, 9) == "UnitValue")
                            {
                                Response.Write("\t" + (Convert.ToDouble(keyvalSplit[1])/100));
                            }
                        }
                    }

                    Response.Write("\n");
                }

                Response.End();
            }
            catch (Exception ex)
            {
                scriptMessage(ex.ToString());
            }
        }


    }


}