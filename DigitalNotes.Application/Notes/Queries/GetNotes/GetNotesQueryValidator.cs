namespace DigitalNotes.Application.Notes.Queries.GetNotes;

public class GetNotesQueryValidator : AbstractValidator<GetNotesQuery>
{
    public GetNotesQueryValidator()
    {
        RuleFor(v => v.CreatedBy).NotEmpty();
    }
}