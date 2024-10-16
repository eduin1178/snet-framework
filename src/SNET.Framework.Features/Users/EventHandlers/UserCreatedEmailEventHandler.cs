using MediatR;
using SNET.Framework.Domain.DomainEvents.Users;
using SNET.Framework.Domain.Notifications.Email;

namespace SNET.Framework.Features.Users.EventHandlers;

public class UserCreatedEmailEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IEmailNotifications _emailNotifications;
    private readonly EmailNotificationSettings _emailNotificationSettings;

    public UserCreatedEmailEventHandler(IEmailNotifications emailNotifications,
        EmailNotificationSettings emailNotificationSettings)
    {
        _emailNotifications = emailNotifications;
        _emailNotificationSettings = emailNotificationSettings;
    }

    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var email = new MailModel
        {
            Content = "Welcome to our platform",
            From = new EmailAddress
            {
                Addres = _emailNotificationSettings.EmailFrom,
                DisplayName = _emailNotificationSettings.EmailName
            },
            To = new List<EmailAddress>
            {
                new EmailAddress
                {
                    Addres = notification.Email,
                    DisplayName = $"{notification.FirstName} {notification.LastName}"
                }
            },
            Subject = "Welcome to our platform",
            IsBodyHtml = true,
        };

        _emailNotifications.SendEmailAsync(email);

        return Task.CompletedTask;
    }
}
