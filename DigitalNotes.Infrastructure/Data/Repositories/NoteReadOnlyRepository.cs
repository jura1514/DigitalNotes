using DigitalNotes.Domain.NoteAggregate;
using DigitalNotes.Domain.NoteAggregate.Interfaces;

namespace DigitalNotes.Infrastructure.Data.Repositories;

internal class NoteReadOnlyRepository : INoteReadOnlyRepository
{
    private readonly DigitalNotesDbContext _digitalNotesDbContext;

    public NoteReadOnlyRepository(DigitalNotesDbContext digitalNotesDbContext)
    {
        _digitalNotesDbContext = digitalNotesDbContext;
    }

    public Task<NoteReadOnly> GetNoteByIdAsync(Guid id, CancellationToken cancelationToken = default)
    {
        return _digitalNotesDbContext.NotesReadOnly.SingleAsync(nw => nw.Id == id, cancelationToken);
    }

    public Task<int> GetTotalCountAsync(string createdBy, string? noteNameQuery, CancellationToken cancellationToken)
    {
        return GetFilteredNotesQuery(createdBy, noteNameQuery)
            .CountAsync(cancellationToken);
    }

    public Task<List<NoteReadOnly>> GetPaginatedAsync(string createdBy, int pageNumber, int pageSize,
        string? noteNameQuery, CancellationToken cancellationToken)
    {
        return GetFilteredNotesQuery(createdBy, noteNameQuery)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    private IQueryable<NoteReadOnly> GetFilteredNotesQuery(string createdBy, string? containsTitle)
    {
        var query = _digitalNotesDbContext.NotesReadOnly
            .Where(nw => nw.CreatedBy == createdBy);

        if (!string.IsNullOrEmpty(containsTitle))
            query = query.Where(nw => nw.Title.Contains(containsTitle));

        return query.OrderByDescending(nw => nw.UpdatedAt ?? nw.CreatedAt);
    }
}