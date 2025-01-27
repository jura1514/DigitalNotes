using DigitalNotes.Domain.Common;
using DigitalNotes.Infrastructure.Data.Entities;

namespace DigitalNotes.Infrastructure.Data.Repositories;

internal class EventDrivenRepository<TEventDrivenAggregate> : IEventDrivenRepository<TEventDrivenAggregate>
    where TEventDrivenAggregate : EventDrivenAggregateBase
{
    private readonly IEventStore _eventStore;
    private readonly DigitalNotesDbContext _dbContext;

    public EventDrivenRepository(IEventStore eventStore, DigitalNotesDbContext dbContext)
    {
        _eventStore = eventStore;
        _dbContext = dbContext;
    }

    public async Task<TEventDrivenAggregate> GetByIdAsync(Guid aggregateId,
        CancellationToken cancellationToken = default)
    {
        var events = await _eventStore.GetEventsAsync(aggregateId, cancellationToken);
        if (!events.Any())
            throw new InvalidOperationException($"No events found for aggregate with ID {aggregateId}");

        var aggregate =
            (TEventDrivenAggregate)Activator.CreateInstance(typeof(TEventDrivenAggregate), nonPublic: true)!;

        foreach (var @event in events)
        {
            aggregate.ApplyEvent(@event);
        }

        return aggregate;
    }

    public async Task SaveAsync(TEventDrivenAggregate aggregate, CancellationToken cancellationToken = default)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(
            async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var uncommittedEvents = aggregate.GetUncommittedEvents();

                    if (!uncommittedEvents.Any())
                        return;

                    var addedEvents = await _eventStore.AddEventsAsync(aggregate.Id, uncommittedEvents,
                        aggregate.Version, cancellationToken);

                    var outboxEntries = addedEvents.Select(@event => new Outbox
                    {
                        Id = Guid.NewGuid(),
                        EventType = @event.EventType,
                        EventData = @event.EventData,
                        CreatedAt = @event.CreatedAt
                    }).ToList();

                    await _dbContext.Outbox.AddRangeAsync(outboxEntries, cancellationToken);

                    // commit transaction
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    // clear events since they are committed
                    aggregate.ClearUncommittedEvents();
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
    }
}