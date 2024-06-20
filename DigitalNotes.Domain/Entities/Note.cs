namespace DigitalNotes.Domain.Entities;

public class Note
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required string CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
}