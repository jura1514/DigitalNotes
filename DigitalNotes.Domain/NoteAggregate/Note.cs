using DigitalNotes.Domain.Common;
using DigitalNotes.Domain.NoteAggregate.Events;

namespace DigitalNotes.Domain.NoteAggregate;

public class Note : EventDrivenAggregateBase
{
    public string Title { get; private set; }
    public string Content { get; private set; }

    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private bool IsDeleted { get; set; }

    /// <summary>
    /// Used when reconstructing the aggregate by replaying events
    /// </summary>
    private Note()
    {
        Title = string.Empty;
        Content = string.Empty;
        CreatedBy = string.Empty;
    }

    public Note(Guid id, string title, string content, string createdBy)
    {
        Title = title;
        Content = content;
        CreatedBy = createdBy;
        RegisterEvent(new NoteCreatedEvent(id, title, content, createdBy, DateTime.UtcNow));
    }

    public void Update(string newTitle, string newContent)
    {
        ThrowIfDeleted();
        RegisterEvent(new NoteUpdatedEvent(Id, newTitle, newContent, DateTime.UtcNow));
    }

    public void Delete()
    {
        ThrowIfDeleted();
        RegisterEvent(new NoteDeletedEvent(Id));
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case NoteCreatedEvent e:
                Id = e.Id;
                Title = e.Title;
                Content = e.Content;
                CreatedBy = e.CreatedBy;
                CreatedAt = e.CreatedAt;
                IsDeleted = false;
                break;

            case NoteUpdatedEvent e:
                Title = e.Title;
                Content = e.Content;
                UpdatedAt = e.UpdatedAt;
                break;

            case NoteDeletedEvent e:
                IsDeleted = true;
                break;

            default:
                throw new InvalidOperationException($"Unsupported event type: {@event.GetType().Name}");
        }
    }

    private void ThrowIfDeleted()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot update a deleted note.");
    }
}