namespace DigitalNotes.Domain.Entities;

public class NoteView
{
    public int? RowNumber { get; init; }
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Content { get; init; }
    public required string CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}