using SNET.Framework.Domain.Primitives;

namespace SNET.Framework.Domain.DomainEvents.Users;

public record UserCreatedDomainEvent(Guid Id, Guid UserId, string FirstName, string LastName) : IDomainEvent;
