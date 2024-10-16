using SNET.Framework.Domain.Notifications.Email;
using System.Net.Mail;
using System.Net;

namespace SNET.Framework.Infrastructure.Notifications.Email;

public class SmtpNotifications : IEmailNotifications
{
        private readonly EmailNotificationSettings _setting;
        private readonly SmtpClient _client;
        public bool _mailSent;

        public SmtpNotifications(EmailNotificationSettings setting)
        {
            _setting = setting;
            _client = new SmtpClient
            {
                Host = setting.Host,
                Port = setting.Port,
                Credentials = new NetworkCredential
                {
                    UserName = setting.User,
                    Password = setting.Password,
                },
                EnableSsl = true,
                Timeout = setting.TimeOut
            };
        }

        public async Task SendEmailAsync(MailModel mail)
        {

            var to = new MailAddressCollection();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_setting.EmailFrom, _setting.EmailName),
                Subject = mail.Subject,
                Body = mail.Content,
                IsBodyHtml = mail.IsBodyHtml,
            };

            mail.To.ForEach(x =>
            {
                mailMessage.To.Add(new MailAddress(x.Addres, x.DisplayName));
            });

            if (mail.Files != null && mail.Files.Count() > 0)
            {
                mail.Files.ForEach(x =>
                {
                    mailMessage.Attachments.Add(new Attachment(x));
                });
            }

            await _client.SendMailAsync(mailMessage);


        }

        public async Task SendEmailWithStreamAttachmentAsync(MailModel mail)
        {

            var to = new MailAddressCollection();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_setting.EmailFrom, _setting.EmailName),
                Subject = mail.Subject,
                Body = mail.Content,
                IsBodyHtml = mail.IsBodyHtml,
            };

            mail.To.ForEach(x =>
            {
                mailMessage.To.Add(new MailAddress(x.Addres, x.DisplayName));
            });

            if (mail.Streams != null && mail.Streams.Count() > 0)
            {
                mail.Streams.ForEach(x =>
                {
                    mailMessage.Attachments.Add(new Attachment(x.Stream, x.Name, x.MediaType));
                });
            }


            await _client.SendMailAsync(mailMessage);


        }

        public async Task SendEmailAsync(List<MailModel> mailList)
        {
            foreach (var mail in mailList)
            {

                var to = new MailAddressCollection();

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_setting.EmailFrom, _setting.EmailName),
                    Subject = mail.Subject,
                    Body = mail.Content,
                    IsBodyHtml = mail.IsBodyHtml,
                };

                mail.To.ForEach(x =>
                {
                    mailMessage.To.Add(new MailAddress(x.Addres, x.DisplayName));
                });

                if (mail.Files != null && mail.Files.Count() > 0)
                {
                    mail.Files.ForEach(x =>
                    {
                        mailMessage.Attachments.Add(new Attachment(x));
                    });
                }

                await _client.SendMailAsync(mailMessage);

            }

        }

}
