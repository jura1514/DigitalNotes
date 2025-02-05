using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DigitalNotes.API.Hubs;

[Authorize]
public class NoteHub : Hub<INoteHub>
{
    public override async Task OnConnectedAsync()
    {
        var email = Context.GetHttpContext()?.GetRouteValue("email")?.ToString();
        if (!string.IsNullOrEmpty(email))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, email);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}