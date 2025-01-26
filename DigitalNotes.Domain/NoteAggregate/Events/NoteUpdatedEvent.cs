using DigitalNotes.Domain.Common;

namespace DigitalNotes.Domain.NoteAggregate.Events;

public sealed record NoteUpdatedEvent(
    Guid Id,
    string Title,
    string Content,
    DateTime UpdatedAt
) : IDomainEvent;