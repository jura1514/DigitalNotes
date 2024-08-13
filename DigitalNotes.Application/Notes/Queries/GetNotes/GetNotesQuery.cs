namespace DigitalNotes.Application.Notes.Queries.GetNotes;

public class GetNotesQuery : IRequest<NotesDto>
{
    public required string CreatedBy { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string? NoteNameQuery { get; init; }
}