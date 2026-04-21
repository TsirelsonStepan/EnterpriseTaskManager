using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using TaskManager.Api.Service;
using TaskManager.Api.Domain.Dto;
using TaskManager.Api.Domain.Model;

namespace TaskManager.Api.Controller;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly TasksService _tasksService;

    public TasksController(TasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [Authorize]
    [HttpGet("{taskGuid}")]
    public async Task<ActionResult<TaskDtoResponse>> GetTask([FromRoute] string taskGuid)
    {
        string guid = GetCurrentUserGuid();
        TaskModel taskModel = await _tasksService.ReadTask(taskGuid);
        if (taskModel.TaskOwnerUserGuid != guid) throw new UnauthorizedAccessException();
        //FIX: mapping
        return Ok(new TaskDtoResponse()
        {
            Guid = (taskModel.TaskGuid ?? throw new NullReferenceException("The task isn't registered in DB and doesn't have guid")).ToString(),
            Name = taskModel.TaskName,
            Status = taskModel.TaskStatus.ToString(),
            Description = taskModel.TaskDescription,
            Deadline = taskModel.TaskDeadline.ToString()
        });
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<TaskDtoResponse>>> GetTasks()
    {
        string username = GetCurrentUserName();
        List<TaskModel> taskModels = await _tasksService.GetTasksByUsername(username);
        List<TaskDtoResponse> taskDtos = [];
        foreach (TaskModel taskModel in taskModels)
        {
            //FIX: mapping
            taskDtos.Add(new TaskDtoResponse()
            {
                Guid = (taskModel.TaskGuid ?? throw new NullReferenceException("The task isn't registered in DB and doesn't have guid")).ToString(),
                Name = taskModel.TaskName,
                Status = taskModel.TaskStatus.ToString(),
                Description = taskModel.TaskDescription,
                Deadline = taskModel.TaskDeadline.ToString()
            });
        }
        return Ok(taskDtos);
    }

    [Authorize]
    [HttpPost]
    public async Task<StatusCodeResult> AddTask([FromBody] TaskDtoRequest taskDto)
    {
        string guid = GetCurrentUserGuid();
        TaskModel taskModel = new(
            name: taskDto.Name,
            status: (TaskModel.ETaskStatus)Enum.Parse(typeof(TaskModel.ETaskStatus), taskDto.Status),//FIX: very ugly
            ownerGuid: guid,
            description: taskDto.Description,
            deadline: taskDto.Deadline == null ? null : DateTime.Parse(taskDto.Deadline)//FIX: also very ugly
        );

        await _tasksService.AddTask(taskModel);
        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<StatusCodeResult> RemoveTask([FromBody] TaskDtoRequest taskDto)
    {
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost]
    public async Task<StatusCodeResult> UpdateTask([FromBody] TaskDtoRequest taskDto)
    {
        throw new NotImplementedException();
    }

    //helper
    private string GetCurrentUserName() => (HttpContext.User.FindFirst(ClaimTypes.Name) ?? throw new UnauthorizedAccessException()).Value;
    private string GetCurrentUserGuid() => (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException()).Value;
}