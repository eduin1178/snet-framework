namespace SNET.Framework.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();
    public AggregateRoot(Guid id) : base(id)
    {

    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
