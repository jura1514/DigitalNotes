namespace DigitalNotes.Infrastructure.Interfaces;

public interface INoteHubNotificationService
{
    Task NotifyReadOnlyNoteUpdated(string createdBy);
}