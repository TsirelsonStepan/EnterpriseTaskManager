using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Domain.Entity;

public class TaskEntity
{
	[Key]
    public string TaskGuid { get; init; } = Guid.NewGuid().ToString();
	[Required]
	public string TaskName { get; init; } = null!;
	[Required][AllowedValues("Pending", "Completed")]
	public string TaskStatus { get; init; } = null!;
	public string? TaskDescrition { get; init; }
	public string? TaskDeadline { get; init; }
	
	[Required]
	public string TaskOwnerUserGuid { get; init; } = null!;
	[Required]
	public UserEntity TaskOwnerUser { get; init; } = null!;
	
	private TaskEntity() {}

	public TaskEntity(string name, UserEntity user, string status, string? description = null, string? deadline = null) : this()
	{
		TaskName = name;
		TaskOwnerUser = user;
		TaskOwnerUserGuid = user.UserGuid;
		TaskStatus = status;
		TaskDescrition = description;
        TaskDeadline = deadline;
	}
}