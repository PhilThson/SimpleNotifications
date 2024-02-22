namespace Manager.Domain.DTOs
{
    public class NotificationDto
	{
        public UserDto Sender { get; set; }
        public UserDto Recipient { get; set; }
        public string Message { get; set; }
    }
}

