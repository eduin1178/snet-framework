namespace SNET.Framework.Domain.Primitives;

public abstract class Entity
{
    private Entity()
    {

    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Guid Id { get; private set;}
}
