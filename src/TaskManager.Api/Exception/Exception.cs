namespace TaskManager.Api.Exception;

public class UserNotFoundException : System.Exception
{
    public string Username { get; set; }
    public UserNotFoundException(string username) : base() { Username = username; }
}