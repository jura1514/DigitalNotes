using DigitalNotes.Application.Notes.Queries.GetNote;
using DigitalNotes.Infrastructure.Data.Repositories;

namespace DigitalNotes.Application.Notes.Queries.GetNotes;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, IReadOnlyCollection<NoteDto>>
{
    private readonly INotesRepository _notesRepository;

    public GetNotesQueryHandler(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }

    public async Task<IReadOnlyCollection<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var notes =
            await _notesRepository.GetPaginatedAsync(request.CreatedBy, request.LastRowNumber, 10,
                request.NoteNameQuery, cancellationToken);

        var list = new List<NoteDto>();
        foreach (var noteEntity in notes)
        {
            list.Add(new NoteDto
            {
                RowNumber = noteEntity.RowNumber,
                Id = noteEntity.Id,
                Title = noteEntity.Title,
                Content = noteEntity.Content,
                CreatedAt = noteEntity.CreatedAt,
                CreatedBy = noteEntity.CreatedBy
            });
        }

        return list;
    }
}