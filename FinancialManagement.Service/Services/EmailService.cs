using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FinancialManagement.Service.Services
{
    public static class EmailService
    {
        public static async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            string fromEmail = "r@gmail.com";
            string appPassword = "1";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(fromEmail,"רץ כצבי");
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential(fromEmail, appPassword);
                    smtpServer.EnableSsl = true;

                    try
                    {
                        await smtpServer.SendMailAsync(mail);
                        Console.WriteLine("Email sent successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send email. Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
//using System;
//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;

//namespace FinancialManagement.Service.Services
//{
//    public class EmailService
//    {
//        private readonly IConfiguration _configuration;

//        public EmailService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public async Task SendEmailAsync(string toEmail, string subject, string body)
//        {
//            string fromEmail = "razkazvi@gmail.com";
//            string appPassword = _configuration["EmailSettings:Password"];

//            using (MailMessage mail = new MailMessage())
//            {
//                mail.From = new MailAddress(fromEmail, "רץ כצבי");
//                mail.To.Add(toEmail);
//                mail.Subject = subject;
//                mail.Body = body;
//                mail.IsBodyHtml = true; // Assuming HTML content

//                using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
//                {
//                    smtpServer.Port = 587;
//                    smtpServer.Credentials = new NetworkCredential(fromEmail, appPassword);
//                    smtpServer.EnableSsl = true;

//                    try
//                    {
//                        await smtpServer.SendMailAsync(mail);
//                        Console.WriteLine("Email sent successfully!");
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine("Failed to send email. Error: " + ex.Message);
//                    }
//                }
//            }
//        }
//    }
//}
