using DigitalNotes.Application.Notes.Queries.GetNote;

namespace DigitalNotes.Application.Notes.Queries.GetNotes;

public class GetNotesQuery : IRequest<IReadOnlyCollection<NoteDto>>
{
    public required string CreatedBy { get; init; }
    public int LastRowNumber { get; init; }
    
    public string? NoteNameQuery { get; init; }
}