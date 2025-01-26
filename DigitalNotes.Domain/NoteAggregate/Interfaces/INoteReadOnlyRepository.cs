namespace DigitalNotes.Domain.NoteAggregate.Interfaces;

public interface INoteReadOnlyRepository
{
    Task<NoteReadOnly> GetNoteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(string createdBy, string? noteNameQuery, CancellationToken cancellationToken);

    Task<List<NoteReadOnly>> GetPaginatedAsync(string createdBy, int pageNumber, int pageSize, string? noteNameQuery,
        CancellationToken cancellationToken);
}