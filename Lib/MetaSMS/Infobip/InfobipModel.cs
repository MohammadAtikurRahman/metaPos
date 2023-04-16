namespace MetaSMS.Infobip
{
 public class InfobipModel
    {
        public string apiKey { get; set; }
        public string senderId { get; set; }
        public string username { set; get; }
        public string password { set; get; }
        public string phoneNumber { set; get; }
        public string message { set; get; }
        public string description { set; get; }
        public int id { get; set; }
        public object status { get; set; }
        public string messages { set; get; }
        
    }
}
