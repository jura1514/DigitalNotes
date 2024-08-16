namespace DigitalNotes.Application.Notes.Commands.DeleteNote;

internal class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}