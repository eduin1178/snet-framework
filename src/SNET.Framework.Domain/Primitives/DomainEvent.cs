
namespace SNET.Framework.Domain.Primitives;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
