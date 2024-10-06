using MediatR;

namespace SNET.Framework.Domain.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init;}
}
