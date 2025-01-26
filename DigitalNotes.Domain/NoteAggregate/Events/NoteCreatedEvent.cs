using DigitalNotes.Domain.Common;

namespace DigitalNotes.Domain.NoteAggregate.Events;

public sealed record NoteCreatedEvent(
    Guid Id,
    string Title,
    string Content,
    string CreatedBy,
    DateTime CreatedAt
) : IDomainEvent;