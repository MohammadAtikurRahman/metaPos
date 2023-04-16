using System;
using System.Collections;
using System.Data;
using MetaPOS.Shop;

namespace MetaPOS.Shop.Controller
{


    public partial class Slider : System.Web.UI.UserControl
    {


        private Model.Shop objWebModel = new Model.Shop();

        private ArrayList ImgArray = new ArrayList();
        private DataSet ds;

        private string ImgList = "", ImgListWithoutLastSemi = "", imagePath = "", folderPath = "";





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadImage();
            }
        }





        public void loadImage()
        {
            ds = objWebModel.getWeb();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ImgList = ds.Tables[0].Rows[0][2].ToString();
                if (ImgList != "" && ds.Tables[0].Rows.Count > 0)
                    ImgListWithoutLastSemi = ImgList.Substring(0, ImgList.Length - 1);

                else
                    pnlSlider.Visible = false;

                folderPath = ("~/Img/Slider/");

                string[] ImgUrls = ImgListWithoutLastSemi.Split(';');


                foreach (string ImgUrl in ImgUrls)
                {
                    imagePath = folderPath + ImgUrl;
                    ImgArray.Add(imagePath);
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("imgView");
                dt.Columns.Add("Id");

                for (int i = 0; i < ImgArray.Count; i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i]["imgView"] = ImgArray[i].ToString();
                    dt.Rows[i]["Id"] = i.ToString();
                }


                rQuotes.DataSource = dt;
                rQuotes.DataBind();
            }
        }





        public void imgSeperate()
        {
            ds = objWebModel.getWeb();
            string imgUrlList = ds.Tables[0].Rows[0][3].ToString();
            string[] imgUrls = imgUrlList.Split(';');

            string folderPath = Server.MapPath("~/Img/Slider/");

            foreach (var imgUrl in imgUrls)
            {
                ImgArray.Add(folderPath + imgUrl);
            }

            //GridView1.DataSource = ImgArray;
            //GridView1.DataBind();
        }


    }


}