namespace DigitalNotes.Application.Notes.Queries.GetLastRowNumber;

public class GetLastRowNumberQueryValidator : AbstractValidator<GetLastRowNumberQuery>
{
    public GetLastRowNumberQueryValidator()
    {
        RuleFor(v => v.CreatedBy).NotEmpty();
    }
}