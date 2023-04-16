using System.Collections.Generic;
using System.Data;
using MetaPOS.Admin.Model;


namespace MetaPOS.Admin.SaleBundle.Service
{


    public class SaleSearch
    {


        private StockModel stock = new StockModel();
        private PackageModel packageModel = new PackageModel();





        public string[] getSearchProductList(string searchVal, string searchFor)
        {
            var originalSearchVal = searchVal;
            if (searchFor == "3")
            {
                var saleStockStatus = new SaleStockStatus();
                var prodId = saleStockStatus.getProductIDByIMEI(searchVal);

                searchVal = "";
                var dtStock = stock.getStockDataListModelByProdID(prodId);
                if (dtStock.Rows.Count > 0)
                    searchVal = dtStock.Rows[0]["prodName"].ToString();

                searchFor = "0";
            }

            var items = new List<string>();
            DataTable dtItemList;

            if (searchVal.Contains("["))
            {
                var searchValSplit = searchVal.Split('[');
                searchVal = searchValSplit[0];
            }

            var attributeRecord = "";
            if (searchFor == "0")
            {
                dtItemList = stock.getSearchProductListModel(searchVal);

            }
            else if (searchFor == "1")
            {

                dtItemList = packageModel.getSearchPackageListModel(searchVal);
            }
            else
            {
                dtItemList = stock.getSearchServiceListModel(searchVal);
            }

            if (dtItemList.Rows.Count == 0)
            {
                if (originalSearchVal.Contains("]"))
                {
                    var splitSearchVal = originalSearchVal.Split(']');
                    searchVal = splitSearchVal[1];
                }
                else if (originalSearchVal.Contains("\""))
                {
                    var splitSearchVal = originalSearchVal.Split('\"');
                    searchVal = splitSearchVal[1];
                }

                if (searchFor == "0")
                {
                    dtItemList = stock.getSearchProductListModel(searchVal);

                }
                else if (searchFor == "1")
                {

                    dtItemList = packageModel.getSearchPackageListModel(searchVal);
                }
                else
                {
                    dtItemList = stock.getSearchServiceListModel(searchVal);
                }
            }

            var listAttrValue = new List<string>();
            for (int i = 0; i < dtItemList.Rows.Count; i++)
            {
                if (dtItemList.Columns.Contains("attributeRecord"))
                    attributeRecord = dtItemList.Rows[i]["attributeRecord"].ToString();

                var attrValue = "";

                if (attributeRecord != "" && attributeRecord != "0")
                {
                    if (attributeRecord.Contains(","))
                    {
                        var attrSplit = attributeRecord.Split(',');
                        for (int j = 0; j < attrSplit.Length; j++)
                        {
                            var dtAttr = stock.getAttrValue(attrSplit[j]);
                            if (dtAttr.Rows.Count > 0)
                            {
                                attrValue += " - " + dtAttr.Rows[0]["attributeName"];
                            }
                        }
                        listAttrValue.Add(attrValue);
                    }
                    else
                    {
                        var dtAttr = stock.getAttrValue(attributeRecord);
                        if (dtAttr.Rows.Count > 0)
                        {
                            listAttrValue.Add(" - " + dtAttr.Rows[0]["attributeName"]);
                        }
                    }


                }
                else
                {
                    // for empty
                    listAttrValue.Add("");
                }
            }



            var attCounter = 0;
            foreach (DataRow row in dtItemList.Rows)
            {
                items.Add(string.Format("{0}<>{1}<>{2}", row["name"], listAttrValue[attCounter], row["code"]));
                attCounter++;
            }

            return items.ToArray();
        }


    }


}