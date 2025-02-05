using DigitalNotes.Domain.Common;
using DigitalNotes.Domain.NoteAggregate;
using DigitalNotes.Domain.NoteAggregate.Events;
using DigitalNotes.Infrastructure.Data;
using DigitalNotes.Infrastructure.Interfaces;

namespace DigitalNotes.Infrastructure.Events;

internal class NoteReadOnlyEventHandler : IEventHandler<IDomainEvent>
{
    private readonly DigitalNotesDbContext _context;
    private readonly INoteHubNotificationService _noteHubNotificationService;

    public NoteReadOnlyEventHandler(DigitalNotesDbContext context,
        INoteHubNotificationService noteHubNotificationService)
    {
        _context = context;
        _noteHubNotificationService = noteHubNotificationService;
    }

    public async Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        string? createdBy = null;

        switch (domainEvent)
        {
            case NoteCreatedEvent e:
                await Handle(e, cancellationToken);
                createdBy = await SetCreatedBy(e);
                break;
            case NoteUpdatedEvent e:
                await Handle(e, cancellationToken);
                createdBy = await SetCreatedBy(e);
                break;
            case NoteDeletedEvent e:
                createdBy = await SetCreatedBy(e);
                await Handle(e, cancellationToken);
                break;
        }

        if (createdBy == null) return;
        await _noteHubNotificationService.NotifyReadOnlyNoteUpdated(createdBy);
        return;

        Task<string?> SetCreatedBy(IDomainEvent e)
        {
            return _context.NotesReadOnly.Where(n => n.Id == e.Id).Select(n => n.CreatedBy)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }

    private async Task Handle(NoteCreatedEvent @event, CancellationToken cancellationToken)
    {
        _context.NotesReadOnly.Add(new NoteReadOnly
        {
            Id = @event.Id,
            Title = @event.Title,
            Content = @event.Content,
            CreatedBy = @event.CreatedBy,
            CreatedAt = @event.CreatedAt
        });
        await _context.SaveChangesAsync(cancellationToken);
    }

    private Task<int> Handle(NoteUpdatedEvent @event, CancellationToken cancellationToken)
    {
        return _context.NotesReadOnly.Where(e => e.Id == @event.Id)
            .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(e => e.Content, @event.Content).SetProperty(e => e.Title, @event.Title)
                        .SetProperty(e => e.UpdatedAt, @event.UpdatedAt),
                cancellationToken: cancellationToken);
    }

    private Task<int> Handle(NoteDeletedEvent @event, CancellationToken cancellationToken)
    {
        return _context.NotesReadOnly.Where(e => e.Id == @event.Id)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}