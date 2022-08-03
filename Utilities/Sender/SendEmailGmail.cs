using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Utilities
{
    public class SendEmailGmail
    {
        public static void Send(string To,string Subject,string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.aliatyabi.ir");
            mail.From = new MailAddress("info@aliatyabi.ir", "فروشگاه اینترنتی");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@aliatyabi.ir", "My@Info@Email");
            //SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}