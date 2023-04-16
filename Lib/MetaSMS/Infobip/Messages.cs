namespace MetaSMS.Infobip
{
    class Messages
    {
        public string to { get; set; }
        public int smsCount { get; set; }
        public string messageId { get; set; }
        public Status status { get; set; }
        public double balance { get; set; }
        public string currency { get; set; }
    }
}
