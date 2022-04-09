using DnsClient;
using System.Net;
using System.Net.Mail;

namespace flights.Areas.Identity.Data
{
    public class EmailHelper
    {
        public bool ValidateMXRecord(string mail)
        {
            //var lookup = new LookupClient(IPAddress.Parse("8.8.4.4"), IPAddress.Parse("8.8.8.8"));
            //lookup.Timeout = TimeSpan.FromSeconds(5);
            //var result = lookup.Query(mail, QueryType.MX);

            //var records = result.Answers;

            //if (records.Any())
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            try
            {
                var addr = new System.Net.Mail.MailAddress(mail);
                return addr.Address == mail;
            }
            catch
            {
                return false;
            }
        }
        public bool SendEmail(string userEmail, string body,string subject)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("flightsiti@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));
           bool tf= ValidateMXRecord(userEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("flightsiti@gmail.com", "123@Aabc");

            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
