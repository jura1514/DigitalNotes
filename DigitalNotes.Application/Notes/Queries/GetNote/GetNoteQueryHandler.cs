using DigitalNotes.Infrastructure.Data.Repositories;

namespace DigitalNotes.Application.Notes.Queries.GetNote;

internal class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, NoteDto>
{
    private readonly INotesRepository _notesRepository;

    public GetNoteQueryHandler(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }

    public async Task<NoteDto> Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        var noteEntity = await _notesRepository.GetAsync(request.Id, cancellationToken);

        if (noteEntity is null)
            throw new ArgumentNullException($"{nameof(noteEntity)}", "Note is not found.");

        return new NoteDto
        {
            Id = noteEntity.Id,
            Title = noteEntity.Title,
            Content = noteEntity.Content,
            CreatedAt = noteEntity.CreatedAt,
            CreatedBy = noteEntity.CreatedBy
        };
    }
}