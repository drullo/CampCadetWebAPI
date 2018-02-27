using System.Collections.Generic;
using System.Net.Mail;

namespace CampCadetWebAPI.Models
{
    public class EMail
    {
        public string Server { get; set; }
        public int SMTPPort { get; set; }
        public bool UseSSL { get; set; }
        public string Subject { get; set; }
        public EmailSender Sender { get; set; }
        public EmailRecipients Recipients { get; set; }
        public EmailContent Content { get; set; }
        public MailPriority Priority { get; set; }

        public EMail()
        {
            SMTPPort = 25;
            Priority = MailPriority.Normal; // Normal = 0, High = 2

            Sender = new EmailSender();

            Recipients = new EmailRecipients
            {
                To = new List<string>(),
                CC = new List<string>(),
                BCC = new List<string>()
            };

            Content = new EmailContent();
        }
    }

    public class EmailSender
    {
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class EmailRecipients
    {
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
    }

    public class EmailContent
    {
        public string Text { get; set; }
        public string HTML { get; set; }
    }
}