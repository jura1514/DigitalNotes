namespace DigitalNotes.Application.Notes.Queries.GetLastRowNumber;

public class GetLastRowNumberQuery : IRequest<int>
{
    public required string CreatedBy { get; init; }
}