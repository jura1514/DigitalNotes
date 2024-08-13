using DigitalNotes.Application.Notes.Queries.GetNote;
using DigitalNotes.Infrastructure.Data.Repositories;

namespace DigitalNotes.Application.Notes.Queries.GetNotes;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, NotesDto>
{
    private readonly INotesRepository _notesRepository;

    public GetNotesQueryHandler(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }

    public async Task<NotesDto> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var notes =
            await _notesRepository.GetPaginatedAsync(request.CreatedBy, request.PageNumber, request.PageSize,
                request.NoteNameQuery, cancellationToken);

        var list = new List<NoteDto>();
        foreach (var noteEntity in notes)
        {
            list.Add(new NoteDto
            {
                Id = noteEntity.Id,
                Title = noteEntity.Title,
                Content = noteEntity.Content,
                CreatedAt = noteEntity.CreatedAt,
                CreatedBy = noteEntity.CreatedBy
            });
        }

        var totalCount =
            await _notesRepository.GetTotalCountAsync(request.CreatedBy, request.NoteNameQuery, cancellationToken);

        return new NotesDto(totalCount, list);
    }
}