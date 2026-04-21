namespace TaskManager.Api.Domain.Model;

public class UserModel
{
    public string UserName { get; init; }
    public string? UserData { get; init; }
    public string? UserGuid { get; init; }

    public UserModel(string name, string? data = null, string? guid = null)
    {
        UserName = name;
        UserData = data;
        UserGuid = guid;
    }
}