namespace NotificationHub;

public interface INotifyClient
{
    Task ReceiveNotification(string message);
}
