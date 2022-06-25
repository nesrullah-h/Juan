using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ExamProject.Services.EmailServices
{
    public static class Email
    {
        public static void SendMail(string fromMail ,string toMail,string password,string body,string subject)
        {
            using (var client = new SmtpClient("smtp.googlemail.com",587))
            {
                client.Credentials = new System.Net.NetworkCredential("tural.memmedzade025@gmail.com", password);
                var message = new MailMessage(fromMail,toMail);
                client.EnableSsl = true;
                message.Body = body;
                message.Subject = subject;
                client.Send(message);
            }
        }
    }
}
