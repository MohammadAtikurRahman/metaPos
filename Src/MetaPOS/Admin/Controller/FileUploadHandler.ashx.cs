using System.Web;


namespace MetaPOS.Admin.Controller
{


    /// <summary>
    /// Summary description for FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    string id = HttpContext.Current.Request["Id"].ToString();

                    HttpPostedFile file = files[i];

                    var path = context.Server.MapPath("~/Img/Product/");
                    string fname = path + id + System.IO.Path.GetExtension(file.FileName);

                    // Exising image delete
                    if (System.IO.File.Exists(fname))
                        System.IO.File.Delete(fname);

                    file.SaveAs(fname);
                }
                //context.Response.ContentType = "text/plain";
            }
        }





        public bool IsReusable
        {
            get { return false; }
        }


    }


}