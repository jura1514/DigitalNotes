namespace DigitalNotes.Infrastructure.Data;

internal interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<object> events, long? expectedVersion = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<object>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default);
}