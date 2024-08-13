using DigitalNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalNotes.Infrastructure.Data.Repositories;

internal class NotesRepository : INotesRepository
{
    private readonly DigitalNotesDbContext _digitalNotesDbContext;

    public NotesRepository(DigitalNotesDbContext digitalNotesDbContext)
    {
        _digitalNotesDbContext = digitalNotesDbContext;
    }

    public async Task<Note> AddAsync(Note entity)
    {
        return (await _digitalNotesDbContext.AddAsync(entity)).Entity;
    }

    public Task<Note?> GetAsync(Guid id, CancellationToken cancellationToken, bool isNoTracking = true)
    {
        return isNoTracking
            ? _digitalNotesDbContext.Notes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id, cancellationToken)
            : _digitalNotesDbContext.Notes.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public Task<int> GetTotalCountAsync(string createdBy, string? noteNameQuery, CancellationToken cancellationToken)
    {
        return GetFilteredNotesQuery(createdBy, noteNameQuery)
            .CountAsync(cancellationToken);
    }

    public Task<List<NoteView>> GetPaginatedAsync(string createdBy, int pageNumber, int pageSize,
        string? noteNameQuery, CancellationToken cancellationToken)
    {
        return GetFilteredNotesQuery(createdBy, noteNameQuery)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _digitalNotesDbContext.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<NoteView> GetFilteredNotesQuery(string createdBy, string? containsTitle)
    {
        var query = _digitalNotesDbContext.NotesView
            .Where(nw => nw.CreatedBy == createdBy);

        if (!string.IsNullOrEmpty(containsTitle))
            query = query.Where(nw => nw.Title.Contains(containsTitle));

        return query;
    }
}