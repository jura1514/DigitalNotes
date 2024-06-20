namespace DigitalNotes.Application.Notes.Commands.UpdateNote;

public record UpdateNoteCommand : IRequest
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }
}