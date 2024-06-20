using DigitalNotes.Domain.Entities;
using DigitalNotes.Infrastructure.Data.Repositories;

namespace DigitalNotes.Application.Notes.Commands.CreateNote;

internal class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly INotesRepository _notesRepository;

    public CreateNoteCommandHandler(INotesRepository notesRepository)
    {
        _notesRepository = notesRepository;
    }


    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Note
        {
            Title = request.Title!,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };
        await _notesRepository.AddAsync(entity);
        await _notesRepository.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}