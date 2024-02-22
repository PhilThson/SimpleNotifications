using Microsoft.AspNetCore.SignalR;
using NotificationHub.Helpers;

namespace NotificationHub;

public class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims.FirstOrDefault(c => c.Type == Constants.UserIdClaim)?.Value;
    }
}

