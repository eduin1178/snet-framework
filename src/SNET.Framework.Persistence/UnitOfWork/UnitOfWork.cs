using MediatR;
using Microsoft.Extensions.Logging;
using SNET.Framework.Domain.Primitives;
using SNET.Framework.Domain.UnitOfWork;

namespace SNET.Framework.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApiDbContext _context;
    private readonly ILogger<ApiDbContext> _logger;
    private readonly IPublisher _publisher;
    public UnitOfWork(ApiDbContext context,
        ILogger<ApiDbContext> logger,
        IPublisher publisher)
    {
        _context = context;
        _logger = logger;
        _publisher = publisher;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);

        var events = _context.ChangeTracker.Entries<AggregateRoot>()
               .Select(x => x.Entity)
               .SelectMany(x =>
               {
                   var events = x.DomainEvents;
                   x.ClearDomainEvents();
                   return events;
               }).ToList();

        foreach (var @event in events)
        {
            _logger.LogInformation("New domain event {Event}", @event.GetType().Name);
            await _publisher.Publish(@event);
        }
    }
}
