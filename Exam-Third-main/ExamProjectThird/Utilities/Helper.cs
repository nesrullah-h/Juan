using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ExamProjectThird.Utilities
{
    public class Helper
    {
        public static void RemoveFile(string root, string folder, string image)
        {
            string path = Path.Combine(root, folder, image);
            if (File.Exists("path"))
            {
                File.Delete(path);
            }

        }
        public enum UserRoles
        {
            SuperAdmin,
            Admin,
            Member,
            Moderator

        }
        public static class Email
        {
            public static void SendMail(string fromEmail, string toEmail, string body, string password, string subject)
            {
                using (var client=new SmtpClient("smtp.googlemail.com",587))
                {
                    client.Credentials = new NetworkCredential(fromEmail,password);
                    client.EnableSsl = true;
                    var msg = new MailMessage(fromEmail, toEmail);
                    msg.Body = body;
                    msg.Subject = subject;

                    client.Send(msg);
                }

               
               
            }

            }
    }
}
