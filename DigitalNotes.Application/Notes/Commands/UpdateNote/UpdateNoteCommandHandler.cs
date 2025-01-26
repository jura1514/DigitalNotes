using DigitalNotes.Domain.NoteAggregate.Interfaces;

namespace DigitalNotes.Application.Notes.Commands.UpdateNote;

internal class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private readonly INoteRepository _noteRepository;

    public UpdateNoteCommandHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(command.Id, cancellationToken);
        note.Update(command.Title, command.Content);
        await _noteRepository.SaveAsync(note, cancellationToken);
    }
}