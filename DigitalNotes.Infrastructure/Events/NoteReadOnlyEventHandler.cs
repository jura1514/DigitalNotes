using DigitalNotes.Domain.Common;
using DigitalNotes.Domain.NoteAggregate;
using DigitalNotes.Domain.NoteAggregate.Events;
using DigitalNotes.Infrastructure.Data;

namespace DigitalNotes.Infrastructure.Events;

internal class NoteReadOnlyEventHandler : IEventHandler<IDomainEvent>
{
    private readonly DigitalNotesDbContext _context;

    public NoteReadOnlyEventHandler(DigitalNotesDbContext context)
    {
        _context = context;
    }

    public async Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        switch (domainEvent)
        {
            case NoteCreatedEvent e:
                await Handle(e, cancellationToken);
                break;
            case NoteUpdatedEvent e:
                await Handle(e, cancellationToken);
                break;
            case NoteDeletedEvent e:
                await Handle(e, cancellationToken);
                break;
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