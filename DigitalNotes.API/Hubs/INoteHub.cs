namespace DigitalNotes.API.Hubs;

public interface INoteHub
{
    Task SendNoteReadOnlySynced();
}