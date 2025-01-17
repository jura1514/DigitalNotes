namespace DigitalNotes.Infrastructure.Data.Entities;

public class EventStoreEntity
{
    public Guid EventId { get; init; }
    public Guid AggregateId { get; init; }
    public required string EventType { get; init; }
    public required string EventData { get; init; }
    public DateTime CreatedAt { get; init; }
    public long Version { get; init; }
}