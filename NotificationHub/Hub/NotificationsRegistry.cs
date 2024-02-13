﻿using NotificationHub.Models;

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

	public User? GetUser(string connectionId)
	{
		return _users.FirstOrDefault(u => u.ConnectionId == connectionId);
	}

	public void AddNotification(string message, User user)
	{
		_notifications.Add(new UserNotification(message, user));
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

