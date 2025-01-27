namespace DigitalNotes.Infrastructure.Data.Entities;

public class Outbox
{
    public Guid Id { get; init; }
    public required string EventType { get; init; }
    public required string EventData { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime ProcessedAt { get; private set; }
    public bool Processed { get; private set; }

    public void Process()
    {
        Processed = true;
        ProcessedAt = DateTime.UtcNow;
    }
}