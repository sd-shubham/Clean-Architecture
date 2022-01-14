using App.Application.Dtos;
using App.Application.Interfaces;
using App.Domain.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace App.Api.Services
{
    internal class EmailService : IMailService
    {
        // configure mailsettings and mailrequest according to your need.
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> options) => _mailSettings = options.Value; 
        public async Task SendMailAsync(MailRequest mailRequset)
        {
            MimeMessage email = new()
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = mailRequset.Subject,
            };
            email.To.Add(MailboxAddress.Parse(mailRequset.ToEmail));
            BodyBuilder builder = new();
            if(mailRequset.Attachments is not null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequset.Attachments)
                {
                    if(file.Length > 0)
                    {
                        using(var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequset.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);


        }
    }
}
