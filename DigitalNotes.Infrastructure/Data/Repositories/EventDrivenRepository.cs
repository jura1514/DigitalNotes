using DigitalNotes.Domain.Common;

namespace DigitalNotes.Infrastructure.Data.Repositories;

internal class EventDrivenRepository<TEventDrivenAggregate> : IEventDrivenRepository<TEventDrivenAggregate>
    where TEventDrivenAggregate : EventDrivenAggregateBase
{
    private readonly IEventStore _eventStore;

    public EventDrivenRepository(IEventStore eventStore)
    {
        _eventStore = eventStore;
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
        var uncommittedEvents = aggregate.GetUncommittedEvents();

        if (!uncommittedEvents.Any())
            return;

        await _eventStore.SaveEventsAsync(aggregate.Id, uncommittedEvents, aggregate.Version, cancellationToken);
        aggregate.ClearUncommittedEvents();
    }
}