using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NotificationHub.Models;

namespace NotificationHub;

[Authorize]
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
        var sender = _registry.GetUser(Context.UserIdentifier) ??
            throw new ApplicationException("User not connected.");

        var recipient = _registry.GetUser(userId) ??
            throw new ApplicationException("Recipient not found.");

        _registry.AddNotification(message, sender, recipient);
        await Clients.Client(recipient.ConnectionId).ReceiveNotification(message);
    }

    public override async Task OnConnectedAsync()
    {
        var newUser = new User(Context.ConnectionId, Context.UserIdentifier ?? "(no name)");
        _registry.AddUser(newUser);
        _logger.LogInformation("{User} connected!", newUser);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = _registry.GetUser(Context.UserIdentifier);
        if (user != null)
        {
            _registry.RemoveUser(user);
            _logger.LogInformation("{User} disconnected!", user);
        }
        await base.OnDisconnectedAsync(exception);
    }
}
