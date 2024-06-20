using DigitalNotes.Infrastructure.Data.Repositories;

namespace DigitalNotes.Application.Notes.Commands.UpdateNote;

internal class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private readonly INotesRepository _notesRepository;

    public UpdateNoteCommandHandler(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }


    public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _notesRepository.GetAsync(request.Id, cancellationToken, false);

        if (note is null)
            throw new InvalidOperationException($"Note record with {request.Id} not found.");

        note.Title = request.Title;
        note.Content = request.Content;
        note.UpdatedAt = DateTime.UtcNow;
        
        await _notesRepository.SaveChangesAsync(cancellationToken);
    }
}