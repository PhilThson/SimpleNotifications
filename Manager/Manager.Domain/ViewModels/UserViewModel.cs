using Manager.Domain.DTOs;

namespace Manager.Domain.ViewModels;

public class UserViewModel
{
	public UserViewModel(UserDto userDto)
	{
		UserName = userDto.UserId;
		ConnectionId = userDto.ConnectionId;
	}

	public string UserName { get; private set; }
	public string ConnectionId { get; private set; }
}

