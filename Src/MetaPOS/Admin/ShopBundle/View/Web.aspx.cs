using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MetaPOS.Admin.ShopBundle.View
{


    public partial class Web : System.Web.UI.Page
    {


        private Admin.DataAccess.CommonFunction objCommonFun = new Admin.DataAccess.CommonFunction();
        private Admin.Model.WebModel objWebModel = new Admin.Model.WebModel();

        //private string fileExten = "", savedFileName = "", roleId = "";
        private DataSet ds, dsLoad;
        private string msg = "";


        public enum MessageType
        {


            Success,
            Error,
            Info,
            Warning


        };


        private ArrayList ImgArray = new ArrayList();





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!objCommonFun.accessChecker("Web"))
                {
                    var obj = new DataAccess.CommonFunction();
                    obj.pageout();
                }
                

                // Get Web Info
                ds = objWebModel.getWeb();
                if (ds.Tables[0].Rows.Count > 0)
                    getWebInfo();
            }

            // Load Slider Image 
            loadImage();
        }





        public void getWebInfo()
        {
            txtWebName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtWebSlogan.Text = ds.Tables[0].Rows[0][2].ToString();
            txtContact.Text = ds.Tables[0].Rows[0][4].ToString();
            txtAddress.Text = ds.Tables[0].Rows[0][5].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();
            txtAbout.Text = ds.Tables[0].Rows[0][7].ToString();
            txtMapShareLink.Text = ds.Tables[0].Rows[0][11].ToString();
            ddlFeaturedProduct.SelectedValue = ds.Tables[0].Rows[0][12].ToString();
            ddlNewProduct.SelectedValue = ds.Tables[0].Rows[0][13].ToString();
        }





        public void loadImage()
        {
            dsLoad = objWebModel.getWeb();
            if (dsLoad.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ImgData");
                string ImgList = dsLoad.Tables[0].Rows[0][3].ToString().TrimEnd(';');
                if (ImgList != "")
                {
                    string folderPath = "~/Img/Slider/";
                    string imagePath = "";
                    string[] ImgUrls = ImgList.Split(';');
                    foreach (string ImgUrl in ImgUrls)
                    {
                        imagePath = folderPath + ImgUrl;
                        ImgArray.Add(imagePath);
                        dt.Rows.Add(imagePath);
                    }

                    gvGalleryList.DataSource = dt;
                    gvGalleryList.DataBind();
                }
            }
        }





        public void scriptMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Notification Board",
                "showMessage('" + message + "','" + type + "');", true);
        }





        protected void btnUpdateWebInfo_Click(object sender, EventArgs e)
        {
            if (objCommonFun.IsEmpty(txtWebName.Text))
            {
                scriptMessage("Website name required!", MessageType.Warning);
                return;
            }

            objWebModel.websiteName = txtWebName.Text;
            objWebModel.websiteSlogan = txtWebSlogan.Text;
            objWebModel.contact = txtContact.Text;
            objWebModel.address = txtAddress.Text;
            objWebModel.email = txtEmail.Text;
            objWebModel.details = txtAbout.Text;
            objWebModel.googleMapShareLink = txtMapShareLink.Text;
            objWebModel.displayNew = ddlNewProduct.SelectedValue;
            objWebModel.displayFeatured = ddlFeaturedProduct.SelectedValue;

            if (objWebModel.IsUpdateOrInsert())
            {
                // Update
                msg = objWebModel.updateWeb();
                scriptMessage(msg, MessageType.Success);
            }
            else
            {
                // Insert
                msg = objWebModel.createWeb();
                scriptMessage(msg, MessageType.Success);
            }
        }





        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                HttpFileCollection fileCollection = Request.Files;

                string folderPath = Server.MapPath("~/Img/Slider/");
                string roleId = Session["roleId"].ToString();
                string ImgList = "";
                int i = 0, increment = 0;// counter = 0,
                ;


                ds = objWebModel.getWeb();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ImgDBurlList = ds.Tables[0].Rows[0][3].ToString().TrimEnd(';');
                    string[] imgSplits = ImgDBurlList.Split(';');
                    if (ImgDBurlList != "")
                    {
                        int lengCount = Convert.ToInt32(ImgDBurlList.Length);
                        string lastValue = ImgDBurlList.Substring(lengCount - 5, 1);

                        ImgList += ds.Tables[0].Rows[0][3].ToString();
                        increment = Convert.ToInt32(lastValue) + 1;
                    }
                }


                for (i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile uploadFile = fileCollection[i];

                    string Rename = roleId + "-" + increment;
                    string fileName = Rename + Path.GetExtension(uploadFile.FileName);

                    if (Path.GetExtension(uploadFile.FileName) == "")
                    {
                        scriptMessage("Please select an image must to upload!", MessageType.Warning);
                        return;
                    }


                    if ((File.Exists(folderPath + Rename)))
                        File.Delete(folderPath + Rename);
                    uploadFile.SaveAs(folderPath + fileName);
                    ImgList += fileName + ";";
                    increment++;
                }


                //foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                //{
                //    string Rename = roleId + "-" + i;
                //    string fileName = Rename + Path.GetExtension(postedFile.FileName);
                //    if ((File.Exists(folderPath + Rename)))
                //        File.Delete(folderPath + Rename);
                //    postedFile.SaveAs(folderPath + fileName);
                //    ImgList += fileName + ";";
                //    i++;
                //}

                objWebModel.sliderImgName = ImgList;
                objWebModel.ImageUpload();

                loadImage();

                scriptMessage(string.Format("{0} files have been uploaded successfully.", fileCollection.Count),
                    MessageType.Success);


                //roleId = Session["roelID"].ToString();

                //string folderPath = Server.MapPath(@"Img/SliderImg/");

                //if (!Directory.Exists(folderPath))
                //    Directory.CreateDirectory(folderPath);

                //HttpFileCollection hfc = Request.Files;
                //for (int i = 0; i < hfc.Count; i++)
                //{

                //    HttpPostedFile hpf = hfc[i];
                //    if (hpf.ContentLength > 0)
                //    {
                //        hpf.SaveAs(Server.MapPath("MyFiles") + "\\" + System.IO.Path.GetFileName(hpf.FileName));
                //    }

                //    //HttpPostedFile hpf = hfc[i];
                //    //if (hpf.ContentLength > 0)
                //    //{
                //    //    fileExten = Path.GetExtension(hpf.FileName);
                //    //    savedFileName = i + fileExten;
                //    //    hpf.SaveAs(folderPath + "\\" + savedFileName);
                //    //}
                //}
            }
            catch (Exception)
            {
                scriptMessage("Please update your website information before!", MessageType.Warning);
            }
        }





        protected void gvGalleryList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow row;
            GridView grid = sender as GridView;

            switch (e.CommandName)
            {
                case "Cancel":
                    index = Convert.ToInt32(e.CommandArgument);
                    row = grid.Rows[index];
                    lblTest.Text = (row.FindControl("lblImgSlider") as Label).Text;
                    break;
            }
        }





        protected void gvGalleryList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        }





        protected void gvGalleryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ImgName = "";
            string folderPath = Server.MapPath("~/Img/Slider/");

            string ImgUrl = (gvGalleryList.SelectedRow.FindControl("lblImgSlider") as Label).Text.TrimEnd(';');
            if (ImgUrl != "")
            {
                ImgName = ImgUrl.Substring(13) + ";";

                if ((File.Exists(folderPath + ImgName)))
                    File.Delete(folderPath + ImgName);

                ds = objWebModel.getWeb();
                string ImgUrlList = ds.Tables[0].Rows[0][3].ToString();
                string imgFinalUrl = ImgUrlList.Replace(ImgName, "");

                objWebModel.sliderImgName = imgFinalUrl;
                objWebModel.ImageUpload();
                scriptMessage("Delete Successfully", MessageType.Success);

                if ((File.Exists(folderPath + ImgName.TrimEnd(';'))))
                    File.Delete(folderPath + ImgName.TrimEnd(';'));
            }

            loadImage();
        }


    }


}