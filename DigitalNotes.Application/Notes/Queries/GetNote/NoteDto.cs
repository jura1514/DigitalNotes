namespace DigitalNotes.Application.Notes.Queries.GetNote;

public record NoteDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Content { get; init; }
    public required string CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}