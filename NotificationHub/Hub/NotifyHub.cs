using Microsoft.AspNetCore.SignalR;
using NotificationHub.Models;

namespace NotificationHub;

public class NotifyHub : Hub<INotifyClient>
{
    private readonly NotificationsRegistry _registry;

    public NotifyHub(NotificationsRegistry registry)
    {
        _registry = registry;
    }

    public async Task SendNotification(string connectionId, string message)
    {
        var user = _registry.GetUser(Context.ConnectionId) ??
            throw new ApplicationException("First connect to hub!");

        _registry.AddNotification(message, user);
        await Clients.Client(connectionId).ReceiveNotification(message);
    }

    public override Task OnConnectedAsync()
    {
        var newUser = new User(Context.ConnectionId, Context.UserIdentifier ?? "(no name)");
        _registry.AddUser(newUser);
        Console.WriteLine($"User: {newUser}");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var user = _registry.GetUser(Context.ConnectionId);
        if (user != null)
        {
            _registry.RemoveUser(user);
        }
        return base.OnDisconnectedAsync(exception);
    }
}
