using Microsoft.AspNetCore.SignalR;

namespace NotificationHub;

public class NotifyHub : Hub
{
    public async Task SendNotification(string connectionId, string message)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"ConnectionId: {Context.ConnectionId}");
        Console.WriteLine($"UserId: {Context.UserIdentifier}");
        Console.WriteLine($"User: {Context.User.Identity.Name}");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
