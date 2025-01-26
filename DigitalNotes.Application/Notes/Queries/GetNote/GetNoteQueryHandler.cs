using DigitalNotes.Domain.NoteAggregate.Interfaces;

namespace DigitalNotes.Application.Notes.Queries.GetNote;

internal class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, NoteDto>
{
    private readonly INoteReadOnlyRepository _noteReadOnlyRepository;

    public GetNoteQueryHandler(INoteReadOnlyRepository noteReadOnlyRepository)
    {
        _noteReadOnlyRepository = noteReadOnlyRepository;
    }


    public async Task<NoteDto> Handle(GetNoteQuery query, CancellationToken cancellationToken)
    {
        var note = await _noteReadOnlyRepository.GetNoteByIdAsync(query.Id, cancellationToken);

        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt,
            CreatedBy = note.CreatedBy
        };
    }
}