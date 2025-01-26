using DigitalNotes.Domain.NoteAggregate.Interfaces;

namespace DigitalNotes.Application.Notes.Commands.DeleteNote;

internal class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INoteRepository _noteRepository;

    public DeleteNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(command.Id, cancellationToken);
        note.Delete();
        await _noteRepository.SaveAsync(note, cancellationToken);
    }
}