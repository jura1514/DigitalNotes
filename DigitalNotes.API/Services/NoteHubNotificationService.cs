using DigitalNotes.API.Hubs;
using DigitalNotes.Infrastructure.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DigitalNotes.API.Services;

public class NoteHubNotificationService : INoteHubNotificationService
{
    private readonly IHubContext<NoteHub, INoteHub> _hubContext;

    public NoteHubNotificationService(IHubContext<NoteHub, INoteHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyReadOnlyNoteUpdated(string createdBy)
    {
        return _hubContext.Clients.Group(createdBy).SendNoteReadOnlySynced();
    }
}