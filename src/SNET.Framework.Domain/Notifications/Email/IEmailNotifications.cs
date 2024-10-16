namespace SNET.Framework.Domain.Notifications.Email;

public interface IEmailNotifications
{
    Task SendEmailAsync(MailModel mail);
    Task SendEmailAsync(List<MailModel> mailList);
    Task SendEmailWithStreamAttachmentAsync(MailModel mail);
}
