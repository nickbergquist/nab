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

                    try
                    {
                        await client.ConnectAsync(es.MailServerAddress, Convert.ToInt32(es.MailServerPort), SecureSocketOptions.Auto).ConfigureAwait(false);
                    }
                    catch (SmtpCommandException ex)
                    {
                        Console.WriteLine("Error trying to connect: {0}", ex.Message);
                        Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);
                        return;
                    }
                    catch (SmtpProtocolException ex)
                    {
                        Console.WriteLine("Protocol error while trying to connect: {0}", ex.Message);
                        return;
                    }

                    if (client.Capabilities.HasFlag(SmtpCapabilities.Authentication))
                    {
                        try
                        {
                            await client.AuthenticateAsync(new NetworkCredential(es.UserId, es.UserPassword));
                        }
                        catch (AuthenticationException ex)
                        {
                            Console.WriteLine("Invalid user name or password: {0}", ex.Message);
                            return;
                        }
                        catch (SmtpCommandException ex)
                        {
                            Console.WriteLine("Error trying to authenticate: {0}", ex.Message);
                            Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);
                            return;
                        }
                        catch (SmtpProtocolException ex)
                        {
                            Console.WriteLine("Protocol error while trying to authenticate: {0}", ex.Message);
                            return;
                        }
                    }

                    try
                    {
                        await client.SendAsync(emailMessage).ConfigureAwait(false);
                    }
                    catch (SmtpCommandException ex)
                    {
                        Console.WriteLine("Error sending message: {0}", ex.Message);
                        Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);

                        switch (ex.ErrorCode)
                        {
                            case SmtpErrorCode.RecipientNotAccepted:
                                Console.WriteLine("\tRecipient not accepted: {0}", ex.Mailbox);
                                break;
                            case SmtpErrorCode.SenderNotAccepted:
                                Console.WriteLine("\tSender not accepted: {0}", ex.Mailbox);
                                break;
                            case SmtpErrorCode.MessageNotAccepted:
                                Console.WriteLine("\tMessage not accepted.");
                                break;
                        }
                    }
                    catch (SmtpProtocolException ex)
                    {
                        Console.WriteLine("Protocol error while sending message: {0}", ex.Message);
                    }

                    await client.DisconnectAsync(true).ConfigureAwait(false);
                

                    //await client.ConnectAsync(es.MailServerAddress, Convert.ToInt32(es.MailServerPort), SecureSocketOptions.Auto).ConfigureAwait(false);
                    //await client.AuthenticateAsync(new NetworkCredential(es.UserId, es.UserPassword));
                    //await client.SendAsync(emailMessage).ConfigureAwait(false);
                    //await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
