namespace DigitalNotes.Application.Notes.Queries.GetNote;

internal class GetNoteQueryValidator : AbstractValidator<GetNoteQuery>
{
    public GetNoteQueryValidator()
    {
        RuleFor(v => v.Id).NotNull();
    }
}