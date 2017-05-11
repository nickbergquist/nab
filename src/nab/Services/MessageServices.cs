using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using nab.Settings;
using System;
using System.Net;
using System.Threading.Tasks;

namespace nab.Services
{
    public class MessageServices : IEmailService
    {
        private readonly EmailSettings es;

        public MessageServices(IOptions<EmailSettings> emailSettings)
        {
            this.es = emailSettings.Value;
        }

        public async Task SendEmailAsync(string senderName, string senderEmail, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Clear();
                emailMessage.From.Add(new MailboxAddress(senderName, senderEmail));

                emailMessage.To.Clear();
                emailMessage.To.Add(new MailboxAddress(es.ToName, es.ToAddress));

                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    //client.LocalDomain = es.LocalDomain;

                    await client.ConnectAsync(es.MailServerAddress, Convert.ToInt32(es.MailServerPort), SecureSocketOptions.Auto).ConfigureAwait(false);
                    await client.AuthenticateAsync(new NetworkCredential(es.UserId, es.UserPassword));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
