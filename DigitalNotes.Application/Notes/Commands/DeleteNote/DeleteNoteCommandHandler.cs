using DigitalNotes.Infrastructure.Data.Repositories;

namespace DigitalNotes.Application.Notes.Commands.DeleteNote;

internal class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INotesRepository _notesRepository;

    public DeleteNoteCommandHandler(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }

    public Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        return _notesRepository.DeleteAsync(request.Id, cancellationToken);
    }
}