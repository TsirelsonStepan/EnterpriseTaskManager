namespace TaskManager.Api.Domain.Dto;

public class TaskDtoRequest
{
    public required string Name { get; init; }
    public required string Status { get; init; }
    public string? Description { get; init; }
    public string? Deadline { get; init; }
}