using DigitalNotes.Domain.Common;
using DigitalNotes.Domain.NoteAggregate;
using DigitalNotes.Domain.NoteAggregate.Interfaces;

namespace DigitalNotes.Infrastructure.Data.Repositories;

internal class NoteRepository : INoteRepository
{
    private readonly IEventDrivenRepository<Note> _eventDrivenRepository;

    public NoteRepository(IEventDrivenRepository<Note> eventDrivenRepository)
    {
        _eventDrivenRepository = eventDrivenRepository;
    }

    public Task<Note> GetByIdAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        return _eventDrivenRepository.GetByIdAsync(aggregateId, cancellationToken);
    }

    public Task SaveAsync(Note aggregate, CancellationToken cancellationToken = default)
    {
        return _eventDrivenRepository.SaveAsync(aggregate, cancellationToken);
    }
}