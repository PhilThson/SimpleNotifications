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

	public string Sender { get; private set; }
	public string Recipient { get; private set; }
	public string Message { get; private set; }
}

