namespace TaskManager.Api.DTO;

public class JwtResponse
{
    public string AccessToken { get; init; }

    public JwtResponse(string token)
    {
        AccessToken = token;
    }
}