using System.Net;
using System.Net.Mail;

namespace CheckServices
{
    class EmailObj
    {
        public bool Ssl { get; set; }
        public string SMTPServer { get; set; }

        public int SMTPPort { get; set; }

        public bool IsHTML { get; set; }

        public string From { get; set; }

        public string Password { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string[] Attachments { get; set; }


        public EmailObj()
        {

        }

        public void SendEmail()
        {
            MailAddress fromMailAddress = new MailAddress(From);
            MailAddress toMailAddress = new MailAddress(To);

            MailMessage message = new MailMessage(fromMailAddress, toMailAddress)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = IsHTML

            };

            if (Attachments.Length >= 0)
            {

                for (int i = 0; i < Attachments.Length; i++)
                {
                    if (!Attachments[i].Equals(""))
                    {
                        Attachment data = new Attachment(Attachments[i]);
                        message.Attachments.Add(data);
                    }
                }
            }

            SmtpClient client = new SmtpClient(SMTPServer, SMTPPort)
            {
                Credentials = new NetworkCredential(From, Password),
                EnableSsl = Ssl
            };

            client.Send(message);

            client.Dispose();

        }



    }
}
