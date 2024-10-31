using SNET.Framework.Domain.Entities;
using SNET.Framework.Domain.Primitives;

namespace SNET.Framework.Domain.DomainEvents.Users;
public record UserLoginDomainEvent(Guid Id, User User) : DomainEvent(Id);
