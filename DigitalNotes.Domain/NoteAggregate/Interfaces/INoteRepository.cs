using DigitalNotes.Domain.Common;

namespace DigitalNotes.Domain.NoteAggregate.Interfaces;

public interface INoteRepository : IEventDrivenRepository<Note>;