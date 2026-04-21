using System;

namespace TaskManager.Api.Domain.Model;

public class TaskModel
{
    public string? TaskGuid { get; init; }
    public string TaskName { get; init; }
    public ETaskStatus TaskStatus { get; init; }
    public string TaskOwnerUserGuid { get; init; }

	public string? TaskDescription { get; init; }
	public DateTime? TaskDeadline { get; init; }

    public TaskModel(string name, ETaskStatus status, string ownerGuid, string? guid = null, string? description = null, DateTime? deadline = null)
    {
        TaskName = name;
        TaskStatus = status;
        TaskOwnerUserGuid = ownerGuid;
        TaskGuid = guid;
        TaskDescription = description;
        TaskDeadline = deadline;
    }
    public enum ETaskStatus
    {
        Pending, Completed
    }
}