using DigitalNotes.Domain.NoteAggregate;
using DigitalNotes.Domain.NoteAggregate.Interfaces;

namespace DigitalNotes.Application.Notes.Commands.CreateNote;

internal class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly INoteRepository _noteRepository;

    public CreateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Guid> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var note = new Note(Guid.NewGuid(), command.Title!, command.Content!, command.CreatedBy);
        await _noteRepository.SaveAsync(note, cancellationToken);
        return note.Id;
    }
}