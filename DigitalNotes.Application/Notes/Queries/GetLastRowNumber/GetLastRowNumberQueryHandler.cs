using DigitalNotes.Infrastructure.Data.Repositories;

namespace DigitalNotes.Application.Notes.Queries.GetLastRowNumber;

public class GetLastRowNumberQueryHandler : IRequestHandler<GetLastRowNumberQuery, int>
{
    private readonly INotesRepository _notesRepository;

    public GetLastRowNumberQueryHandler(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }

    public async Task<int> Handle(GetLastRowNumberQuery request, CancellationToken cancellationToken)
    {
        var rowNumber = await _notesRepository.GetLastRowNumberAsync(request.CreatedBy, cancellationToken);
        return rowNumber ?? 0;
    }
}