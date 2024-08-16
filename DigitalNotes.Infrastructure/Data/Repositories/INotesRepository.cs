using DigitalNotes.Domain.Entities;

namespace DigitalNotes.Infrastructure.Data.Repositories;

public interface INotesRepository
{
    Task<Note> AddAsync(Note entity);
    Task<Note?> GetAsync(Guid id, CancellationToken cancellationToken, bool isNoTracking = true);
    Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<int> GetTotalCountAsync(string createdBy, string? noteNameQuery, CancellationToken cancellationToken);

    Task<List<NoteView>> GetPaginatedAsync(string createdBy, int pageNumber, int pageSize, string? noteNameQuery,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}