namespace Demo.EmailNotifications.Entities
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string UserName { get; set; }
        public string APIKey { get; set; }
    }
}
