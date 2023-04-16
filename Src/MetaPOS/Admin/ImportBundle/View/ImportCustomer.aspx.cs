using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using MetaPOS.Admin.DataAccess;
using MetaPOS.Admin.Model;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace MetaPOS.Admin.ImportBundle.View
{


    public partial class ImportCustomer : System.Web.UI.Page
    {


        private Controller.CommonController objCommController = new Controller.CommonController();

        private CommonFunction objCommonFun = new CommonFunction();
        private CustomerModel objCustomerModel = new CustomerModel();





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("CustomerImport"))
                {
                    objCommonFun.pageout();
                }
            }
        }





        public void scriptMessage(string msg)
        {
            string title = "Notification Area";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), title, "alert('" + msg + "');", true);
        }





        protected void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                scriptMessage("Please select a file");
                return;
            }

            // check maximum file size 1M 
            HttpPostedFile file = FileUpload1.PostedFile;
            int iFileSize = file.ContentLength;
            if (iFileSize > 1048576)
            {
                scriptMessage("File size less then 1 MB");
                return;
            }

            if ((file == null) && (file.ContentLength <= 0))
            {
                scriptMessage("Please select a file");
                return;
            }

            string path = Server.MapPath("~/Files/");

            // Save the uploaded Excel file.
            string filePath = path + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);

            // Open the Excel file in Read Mode using OpenXml.
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false))
            { 

                //Read the first Sheets from Excel file.
                Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                //Get the Worksheet instance.
                Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;


                //Fetch all the rows present in the Worksheet.
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();


                //Create a new DataTable.
                DataTable dt = new DataTable();

                // create datatable for gridview
                DataTable dtGrd = new DataTable();

                //Loop through the Worksheet rows.
                foreach (Row row in rows)
                {
                    //Use the first row to add columns to DataTable
                    if (row.RowIndex.Value == 1)
                    {
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            dt.Columns.Add(GetValue(doc, cell));
                        }
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                            i++;
                        }
                    }
                }

                int phoneCounter = 0;//, emailCounter = 0, percentange = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dbPhone = "";
                    string name = dt.Rows[i][0].ToString();
                    string address = dt.Rows[i][1].ToString();
                    string phone = dt.Rows[i][2].ToString();
                    string phone2 = dt.Rows[i][3].ToString();
                    string email = dt.Rows[i][4].ToString();

                    // Customer phone exists check db
                    bool phoneExists = false;
                    string originalPhone = "0" + phone;
                    Dictionary<string, string> dicCusPhone = new Dictionary<string, string>();
                    dicCusPhone.Add("phone", originalPhone);
                    string getConditionalControlParmeter = objCommController.getConditinalParameter(dicCusPhone);

                    DataSet dsCusPhone = objCustomerModel.getCustomerByCondition(getConditionalControlParmeter);
                    if (dsCusPhone.Tables[0].Rows.Count > 0)
                    {
                        for (int cusDbPhone = 0; cusDbPhone < dsCusPhone.Tables[0].Rows.Count; cusDbPhone++)
                        {
                            dbPhone = dsCusPhone.Tables[0].Rows[cusDbPhone][3].ToString();
                            if (dbPhone != "" && dbPhone == originalPhone)
                            {
                                phoneExists = true;
                                break;
                            }
                        }
                    }

                    // Phone counter
                    if ((phone.Length.ToString() == "10" && phone != "[@#]" && phoneExists == false))
                    {
                        bool find = false;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i != j && phone == dt.Rows[j][2].ToString())
                            {
                                find = true;
                                break;
                            }
                        }


                        if (find == false)
                        {
                            // Add a fist char 0
                            phone = "0" + phone;

                            objCustomerModel.nextCusID = objCustomerModel.generateCustomerId();
                            objCustomerModel.name = name.Replace("[@#]", "").Replace("'", "");
                            objCustomerModel.address = address.Replace("[@#]", "").Replace("'", "");
                            objCustomerModel.phone = phone.Replace("[@#]", "");
                            objCustomerModel.phone2 = phone2.Replace("[@#]", "");
                            objCustomerModel.mailInfo = email.Replace("[@#]", "");

                            objCustomerModel.createCustomer();
                            phoneCounter++;
                        }
                    }


                    // Email counter
                    //if (email != "[@#]")
                    //{
                    //    bool find = false;
                    //    for (int k = 0; k < dt.Rows.Count; k++)
                    //    {
                    //        if (i != k && email == dt.Rows[k][4].ToString())
                    //        {
                    //            find = true;
                    //            break;
                    //        }
                    //    }
                    //    if (find == false)
                    //        emailCounter++;
                    //}
                }


                // Process bar
                string maxWith = dt.Rows.Count.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "updateProgress",
                    "updateProgress('" + phoneCounter + "', '" + maxWith + "');", true);

                lblCounter.Text = "Successfully inserted = " + phoneCounter + " rows. ";
            }

            // Delete uploading file 
            System.IO.File.Delete(filePath);
        }





        private string GetValue(DocumentFormat.OpenXml.Packaging.SpreadsheetDocument doc, DocumentFormat.OpenXml.Spreadsheet.Cell cell)
        {
            string value = cell.CellValue.InnerText;


            if (cell.DataType != null && cell.DataType.Value == DocumentFormat.OpenXml.Spreadsheet.CellValues.SharedString)
            {
                return
                    doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value))
                        .InnerText;
            }
            return value;
        }


    }


}