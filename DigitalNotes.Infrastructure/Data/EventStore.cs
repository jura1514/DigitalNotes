using DigitalNotes.Infrastructure.Data.Entities;

namespace DigitalNotes.Infrastructure.Data;

internal class EventStore : IEventStore
{
    private readonly DigitalNotesDbContext _dbContext;

    public EventStore(DigitalNotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<object> events, long? expectedVersion,
        CancellationToken cancellationToken = default)
    {
        var currentVersion = _dbContext.EventStore
            .Where(e => e.AggregateId == aggregateId)
            .OrderByDescending(e => e.Version)
            .Select(e => e.Version)
            .FirstOrDefault();

        if (currentVersion != expectedVersion)
        {
            throw new InvalidOperationException("Concurrency conflict: Version mismatch");
        }

        var newEvents = events.Select((@event, index) => new EventStoreEntity
        {
            EventId = Guid.NewGuid(),
            AggregateId = aggregateId,
            EventType = @event.GetType().AssemblyQualifiedName!,
            EventData = JsonSerializer.Serialize(@event),
            Version = currentVersion + index + 1,
            CreatedAt = DateTime.UtcNow
        });

        await _dbContext.EventStore.AddRangeAsync(newEvents, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<object>> GetEventsAsync(Guid aggregateId,
        CancellationToken cancellationToken = default)
    {
        var eventEntities = await _dbContext.EventStore
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.Version)
            .ToListAsync(cancellationToken: cancellationToken);

        return GetDeserializedEvents(eventEntities).ToList();
    }

    private static IEnumerable<object> GetDeserializedEvents(List<EventStoreEntity> eventEntities)
    {
        foreach (var e in eventEntities)
        {
            var eventType = Type.GetType(e.EventType);
            var @event = eventType != null
                ? JsonSerializer.Deserialize(e.EventData, eventType)
                : throw new InvalidOperationException($"Unknown event type: {e.EventType}");
            yield return @event!;
        }
    }
}