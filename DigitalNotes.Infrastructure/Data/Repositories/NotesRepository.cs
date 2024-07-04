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

    public Task<int?> GetLastRowNumberAsync(string createdBy, CancellationToken cancellationToken)
    {
        return _digitalNotesDbContext.NotesView
            .Where(nw => nw.CreatedBy == createdBy)
            .Select(nw => nw.RowNumber)
            .MaxAsync(cancellationToken);
    }

    public Task<List<NoteView>> GetPaginatedAsync(string createdBy, int lastRowNumber, int pageSize,
        string? noteNameQuery, CancellationToken cancellationToken)
    {
        var query = GetLastCreatedOrUpdatedNotesQuery(createdBy);

        if (!string.IsNullOrEmpty(noteNameQuery))
            query = query.Where(nw => nw.Title.Contains(noteNameQuery));

        return query
            .Where(nw => nw.RowNumber <= lastRowNumber)
            .Take(pageSize).ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _digitalNotesDbContext.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<NoteView> GetLastCreatedOrUpdatedNotesQuery(string createdBy)
    {
        return _digitalNotesDbContext.NotesView
            .Where(nw => nw.CreatedBy == createdBy)
            .OrderByDescending(nw => nw.UpdatedAt ?? nw.CreatedAt);
    }
}