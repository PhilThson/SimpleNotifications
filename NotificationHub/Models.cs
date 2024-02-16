namespace NotificationHub.Models;

public record User(string ConnectionId, string UserId);
public record UserNotification(string Message, User Sender, User Recipient);
