namespace DigitalNotes.Application.Notes.Commands.DeleteNote;

public record DeleteNoteCommand : IRequest
{
    public Guid Id { get; init; }
}