namespace DigitalNotes.Domain.NoteAggregate;

internal sealed record NoteUpdatedEvent(Guid Id, string Title, string Content);