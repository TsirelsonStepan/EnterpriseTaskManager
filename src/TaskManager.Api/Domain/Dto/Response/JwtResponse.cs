namespace TaskManager.Api.Domain.Dto;

public class JwtResponse
{
    public string AccessToken { get; init; }

    public JwtResponse(string token)
    {
        AccessToken = token;
    }
}