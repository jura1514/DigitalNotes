namespace DigitalNotes.Domain.NoteAggregate;

internal sealed record NoteCreatedEvent(
    Guid Id,
    string Title,
    string Content,
    string CreatedBy
);