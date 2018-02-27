using CampCadetWebAPI.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;
using static System.Net.Mime.MediaTypeNames;

namespace CampCadetWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailController : ApiController
    {
        [HttpPost]
        [Route("api/email")]
        public IHttpActionResult Post([FromBody] EMail email)
        {
            if (email == null)
                return BadRequest("Required data was not supplied.");

            using (var message = new MailMessage())
            {
                message.Subject = email.Subject;
                message.IsBodyHtml = !string.IsNullOrEmpty(email.Content.HTML);
                message.Priority = email.Priority;

                // If we have both Text and HTML, send a multi-part MIME, otherwise just send a regular email
                if (!string.IsNullOrEmpty(email.Content.Text) && !string.IsNullOrEmpty(email.Content.HTML))
                {
                    var textView = AlternateView.CreateAlternateViewFromString(email.Content.Text, null, Text.Plain);
                    var htmlView = AlternateView.CreateAlternateViewFromString(email.Content.HTML, null, Text.Html);

                    message.AlternateViews.Add(htmlView);
                    message.AlternateViews.Add(textView);
                }
                else if (!string.IsNullOrEmpty(email.Content.Text))
                    message.Body = email.Content.Text;
                else if (!string.IsNullOrEmpty(email.Content.HTML))
                    message.Body = email.Content.HTML;

                // Add the sender
                message.From = string.IsNullOrEmpty(email.Sender.DisplayName) ?
                    new MailAddress(email.Sender.EmailAddress) :
                    new MailAddress(email.Sender.EmailAddress, email.Sender.DisplayName);

                // Add the recipients
                email.Recipients.To
                    .ForEach(r => message.To.Add(new MailAddress(r)));

                // Add the CC
                email.Recipients.CC
                    .ForEach(c => message.CC.Add(new MailAddress(c)));

                // Add the BCC
                email.Recipients.BCC
                    .ForEach(b => message.Bcc.Add(new MailAddress(b)));

                // Configure the mail client
                using (var client = new SmtpClient())
                {
                    client.Host = email.Server;
                    client.Port = email.SMTPPort;
                    client.EnableSsl = email.UseSSL;

                    // Use specific credentials?
                    client.UseDefaultCredentials = string.IsNullOrEmpty(email.Sender.UserName);
                    client.Credentials = client.UseDefaultCredentials ?
                        null :
                        new NetworkCredential(email.Sender.UserName, email.Sender.Password);

                    // Send it
                    client.Send(message);
                }
            }

            return Ok(email);
        }
    }
}