

namespace MetaEmail.Elastic
{
    public class ElasticEmailModel
    {
        public string sender { get; set; }
        public string apiKey { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string emailList { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
    }
}
