
namespace SNET.Framework.Domain.Notifications.Email;

public class EmailNotificationSettings
{
    public string Host { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string EmailFrom { get; set; }
    public string EmailName { get; set; }
    public int Port { get; set; }
    public int TimeOut { get; set; }
}
