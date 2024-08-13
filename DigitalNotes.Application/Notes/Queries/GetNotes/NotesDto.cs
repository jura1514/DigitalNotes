using DigitalNotes.Application.Notes.Queries.GetNote;

namespace DigitalNotes.Application.Notes.Queries.GetNotes;

public record NotesDto(int TotalCount, IReadOnlyCollection<NoteDto> Notes);