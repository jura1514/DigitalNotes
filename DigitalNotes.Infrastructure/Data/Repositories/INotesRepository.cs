using DigitalNotes.Domain.Entities;

namespace DigitalNotes.Infrastructure.Data.Repositories;

public interface INotesRepository
{
    Task<Note> AddAsync(Note entity);
    Task<Note?> GetAsync(Guid id, CancellationToken cancellationToken, bool isNoTracking = true);

    Task<int?> GetLastRowNumberAsync(string createdBy, CancellationToken cancellationToken);

    Task<List<NoteView>> GetPaginatedAsync(string createdBy, int lastRowNumber, int pageSize,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}