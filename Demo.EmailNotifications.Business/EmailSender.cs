using Demo.EmailNotifications.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.EmailNotifications.Business
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<BaseResponse> SendEmailAsync(MailMessage mailMessage)
        {
            try
            {
                if (mailMessage == null)
                    throw new Exception("Please provide valid request.");

                var mimeMessage = new MimeMessage();

                if (string.IsNullOrEmpty(mailMessage.From))
                    mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
                else
                    mimeMessage.From.Add(new MailboxAddress(mailMessage.From));
                if (mailMessage.Cc!=null&& mailMessage.Cc.Any())
                    mimeMessage.Cc.AddRange(mailMessage.Cc.Select(x => new MailboxAddress(x)));
                if (mailMessage.Bcc != null && mailMessage.Bcc.Any())
                    mimeMessage.Bcc.AddRange(mailMessage.Bcc.Select(x => new MailboxAddress(x)));
                mimeMessage.To.AddRange(mailMessage.To.Select(x => new MailboxAddress(x)));
                mimeMessage.Subject = mailMessage.Subject;
                mimeMessage.Body = new TextPart("html")
                {
                    Text = mailMessage.Body
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, true);
                    await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.APIKey);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
