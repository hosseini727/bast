using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace Raika.Common.SharedInfrastructure.Email
{
    public class EmailService : IEmailService
    {
        private string SMTPServer { get; set; } = "localhost";
        private int SMTPPort { get; set; } = 25;
        private string SMTPUserName { get; set; } = "gust";
        private string SMTPPassword { get; set; } = "gust";

        //public EmailService(string sMTPServer, int sMTPPort, string sMTPUserName, string sMTPPassword)
        //{
        //    SMTPServer = sMTPServer;
        //    SMTPPort = sMTPPort;
        //    SMTPUserName = sMTPUserName;
        //    SMTPPassword = sMTPPassword;
        //}        

        public async Task SendMailAsync(string from, string to, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.Subject = subject;
                message.Body = body;
                SmtpClient smtp = new SmtpClient();
                await smtp.SendMailAsync(message);
            }
            catch (Exception)
            {
                //
                // Logging
                //

            }

        }
        public async Task SendMailAsync(string fromAddress, string fromName, string toAddress, string subject, string body)
        {
            try
            {
                MailAddress to = new MailAddress(toAddress);
                MailAddress from = new MailAddress(fromAddress, fromName, System.Text.Encoding.UTF8);
                MailMessage message = new MailMessage(from, to);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, new ContentType("text/html")));
                message.IsBodyHtml = true;
                NetworkCredential au = new NetworkCredential(SMTPUserName, SMTPPassword);
                SmtpClient smtp = new SmtpClient(SMTPServer, SMTPPort);
                smtp.Credentials = au;
                await smtp.SendMailAsync(message);
            }
            catch (Exception)
            {
                //
                // Logging
                //

            }

        }
    }
}
