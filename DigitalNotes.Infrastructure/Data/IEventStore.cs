using DigitalNotes.Infrastructure.Data.Entities;

namespace DigitalNotes.Infrastructure.Data;

internal interface IEventStore
{
    Task<List<EventStoreEntity>> AddEventsAsync(Guid aggregateId, IEnumerable<object> events, long? expectedVersion,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<object>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default);
}