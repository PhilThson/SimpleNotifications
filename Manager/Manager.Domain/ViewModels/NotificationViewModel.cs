using System.ComponentModel;
using Manager.Domain.DTOs;

namespace Manager.Domain.ViewModels;

public class NotificationViewModel
{
	public NotificationViewModel(NotificationDto dto)
	{
		Sender = dto.Sender.UserId;
		Recipient = dto.Recipient.UserId;
		Message = dto.Message;
	}

	public string Sender { get; set; }
	public string Recipient { get; set; }
	public string Message { get; set; }
}

