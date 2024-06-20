namespace DigitalNotes.Application.Notes.Commands.CreateNote;

internal class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.CreatedBy).NotEmpty();
    }
}