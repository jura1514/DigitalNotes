namespace DigitalNotes.Application.Notes.Queries.GetNote;

public class GetNoteQuery : IRequest<NoteDto>
{
    public required Guid Id { get; init; }
}