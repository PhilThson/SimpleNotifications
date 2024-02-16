using NotificationHub.Models;

namespace NotificationHub;

public class NotificationsRegistry
{
	private List<UserNotification> _notifications;
	private List<User> _users;

	public NotificationsRegistry()
	{
		_notifications = new List<UserNotification>();
		_users = new List<User>();
	}

	public void AddUser(User user)
	{
		_users.Add(user);
	}

	public void RemoveUser(User user)
	{
		_users.Remove(user);
	}

	public User? GetUser(string? userId)
	{
		if (string.IsNullOrEmpty(userId))
		{
			return null;
		}
		return _users.FirstOrDefault(u => u.UserId == userId);
	}

	public void AddNotification(string message, User sender, User recipient)
	{
		_notifications.Add(new UserNotification(message, sender, recipient));
	}

	public List<UserNotification> GetAllNotifications()
	{
		return _notifications;
	}

	public List<User> GetAllUsers()
	{
		return _users;
	}
}

