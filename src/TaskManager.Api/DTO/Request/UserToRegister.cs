using TaskManager.Api.Domain;

namespace TaskManager.Api.DTO.Request;

public class UserToRegister
{
	public User UserData { get; init; }
	public required string Password { get; init; }

	public UserToRegister(User user, string password)
	{
		UserData = user;
		Password = password;
	}
}