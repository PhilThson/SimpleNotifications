namespace Manager.Domain.DTOs
{
    public class NotificationDto
	{
        public UserDto Sender { get; set; }
        public UserDto Recipient { get; set; }
        public string Message { get; set; }
    }

    public class UserDto
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}

