namespace DigitalNotes.Application.Notes.Commands.CreateNote;

public record CreateNoteCommand : IRequest<Guid>
{
    public string? Title { get; init; }
    public string? Content { get; init; }
    public required string CreatedBy { get; init; }
}