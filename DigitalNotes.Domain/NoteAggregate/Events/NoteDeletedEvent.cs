using DigitalNotes.Domain.Common;

namespace DigitalNotes.Domain.NoteAggregate.Events;

public sealed record NoteDeletedEvent(Guid Id) : IDomainEvent;