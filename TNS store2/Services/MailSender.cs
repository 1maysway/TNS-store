using MimeKit;
using System.Net.Mail;

namespace TNS_store2.Services
{
    public class MailSender : IMailSender
    {
        public async Task sendAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("TNS store", ""));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                //await client.ConnectAsync(" smtp.mailgun.org", 587, false);
                //await client.AuthenticateAsync("", "");
                //await client.SendAsync(emailMessage);
                //await client.DisconnectAsync(true);
            }
        }
    }
}
