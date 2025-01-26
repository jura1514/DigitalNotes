using DigitalNotes.Domain.Common;
using DigitalNotes.Infrastructure.Events;

namespace DigitalNotes.Infrastructure.Data.Repositories;

internal class EventDrivenRepository<TEventDrivenAggregate> : IEventDrivenRepository<TEventDrivenAggregate>
    where TEventDrivenAggregate : EventDrivenAggregateBase
{
    private readonly IEventStore _eventStore;
    private readonly DigitalNotesDbContext _dbContext;
    private readonly IEventDispatcher _dispatcher;

    public EventDrivenRepository(IEventStore eventStore, DigitalNotesDbContext dbContext, IEventDispatcher dispatcher)
    {
        _eventStore = eventStore;
        _dbContext = dbContext;
        _dispatcher = dispatcher;
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

                    // TODO: use transactional outbox pattern
                    await _eventStore.SaveEventsAsync(aggregate.Id, uncommittedEvents, aggregate.Version,
                        cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    // Publish events to update the read model
                    foreach (var @event in uncommittedEvents)
                    {
                        await _dispatcher.PublishAsync(@event, cancellationToken);
                    }

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