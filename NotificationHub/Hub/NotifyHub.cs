using Microsoft.AspNetCore.SignalR;
using NotificationHub.Models;

namespace NotificationHub;

public class NotifyHub : Hub<INotifyClient>
{
    private readonly NotificationsRegistry _registry;
    private readonly ILogger<NotifyHub> _logger;

    public NotifyHub(NotificationsRegistry registry, ILogger<NotifyHub> logger)
    {
        _registry = registry;
        _logger = logger;
    }

    public async Task SendNotification(string userId, string message)
    {
        var user = _registry.GetUser(userId) ??
            throw new ApplicationException("No user connected!");

        //TODO: model should conatin sender and receiver

        _registry.AddNotification(message, user);
        await Clients.Client(user.ConnectionId).ReceiveNotification(message);
    }

    public override Task OnConnectedAsync()
    {
        var newUser = new User(Context.ConnectionId, Context.UserIdentifier ?? "(no name)");
        _registry.AddUser(newUser);
        _logger.LogInformation("{User} connected!", newUser);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var user = _registry.GetUser(Context.ConnectionId);
        if (user != null)
        {
            _registry.RemoveUser(user);
            _logger.LogInformation("{User} disconnected!", user);
        }
        return base.OnDisconnectedAsync(exception);
    }
}
