namespace TaskManager.Api.Domain;

public class User
{
    public string Name { get; init; }
    public string? Data { get; init; }

    public User(string name, string? data = null)
    {
        Name = name;
        Data = data;
    }
}