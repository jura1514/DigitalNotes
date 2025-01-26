using DigitalNotes.Application.Notes.Queries.GetNote;
using DigitalNotes.Domain.NoteAggregate.Interfaces;

namespace DigitalNotes.Application.Notes.Queries.GetNotes;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, NotesDto>
{
    private readonly INoteReadOnlyRepository _noteReadOnlyRepository;

    public GetNotesQueryHandler(INoteReadOnlyRepository noteReadOnlyRepository)
    {
        _noteReadOnlyRepository = noteReadOnlyRepository;
    }

    public async Task<NotesDto> Handle(GetNotesQuery query, CancellationToken cancellationToken)
    {
        var notes = await _noteReadOnlyRepository.GetPaginatedAsync(query.CreatedBy, query.PageNumber, query.PageSize,
            query.NoteNameQuery, cancellationToken);

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
            await _noteReadOnlyRepository.GetTotalCountAsync(query.CreatedBy, query.NoteNameQuery, cancellationToken);

        return new NotesDto(totalCount, list);
    }
}