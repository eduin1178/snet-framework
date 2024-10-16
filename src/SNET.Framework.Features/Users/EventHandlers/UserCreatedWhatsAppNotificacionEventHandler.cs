using MediatR;
using Microsoft.Extensions.Logging;
using SNET.Framework.Domain.DomainEvents.Users;

namespace SNET.Framework.Features.Users.EventHandlers;

internal class UserCreatedWhatsAppNotificacionEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly ILogger<UserCreatedWhatsAppNotificacionEventHandler> _logger;

    public UserCreatedWhatsAppNotificacionEventHandler(ILogger<UserCreatedWhatsAppNotificacionEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User created: {notification.UserId}");

        return Task.CompletedTask;
    }
}
