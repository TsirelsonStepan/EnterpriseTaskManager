using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using TaskManager.Api.Exception;
using TaskManager.Api.Infrastructure;
using TaskManager.Api.Domain.Model;
using TaskManager.Api.Domain.Entity;

namespace TaskManager.Api.Service;

public interface ITasksService
{
    public Task<List<TaskModel>> GetTasksByUsername(string username);
    public Task<TaskModel> ReadTask(string guid);
    public Task AddTask(TaskModel taskModel);
    public Task RemoveTask(TaskModel taskModel);
    public Task UpdateTask(TaskModel taskModel);
}

public class TasksService : ITasksService
{
    readonly TaskManagerDbContext _context;

    public TasksService(TaskManagerDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskModel>> GetTasksByUsername(string username)
    {
        List<TaskEntity> taskEntities = await _context.Tasks.Where(t => t.TaskOwnerUser != null && t.TaskOwnerUser.UserName == username).ToListAsync();
        List<TaskModel> taskModels = [];
        foreach (TaskEntity taskEntity in taskEntities)
        {
            //FIX: mapper
            taskModels.Add(new
            (
                guid: taskEntity.TaskGuid,
                name: taskEntity.TaskName,
                status: (TaskModel.ETaskStatus)Enum.Parse(typeof(TaskModel.ETaskStatus), taskEntity.TaskStatus),//FIX: very ugly
                ownerGuid: taskEntity.TaskOwnerUserGuid,
                description: taskEntity.TaskDescrition,
                deadline: taskEntity.TaskDeadline == null ? null : DateTime.Parse(taskEntity.TaskDeadline)//FIX: also very ugly                
            ));
        }
        return taskModels;
    }

    public async Task<TaskModel> ReadTask(string guid)
    {
        TaskEntity taskEntity = await GetTaskById(guid);
        //FIX: mapper
        TaskModel taskModel = new
        (
            name: taskEntity.TaskName,
            status: (TaskModel.ETaskStatus)Enum.Parse(typeof(TaskModel.ETaskStatus), taskEntity.TaskStatus),//FIX: very ugly
            ownerGuid: taskEntity.TaskOwnerUserGuid,
            guid: taskEntity.TaskGuid,
            description: taskEntity.TaskDescrition,
            deadline: taskEntity.TaskDeadline == null ? null : DateTime.Parse(taskEntity.TaskDeadline)//FIX: also very ugly
        );
        return taskModel;
    }

    public async Task AddTask(TaskModel taskModel)
    {
        UserEntity userEntity = await GetUserById(taskModel.TaskOwnerUserGuid);
        //FIX: mapping
        TaskEntity taskEntity = new
        (
            name: taskModel.TaskName,
            user: userEntity,
            status: taskModel.TaskStatus.ToString(),
            description: taskModel.TaskDescription,
            deadline: taskModel.TaskDeadline.ToString()
        );
        _context.Tasks.Add(taskEntity);
        await _context.SaveChangesAsync();
    }

    public Task RemoveTask(TaskModel taskModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTask(TaskModel taskModel)
    {
        throw new NotImplementedException();
    }

    //helper
    private async Task<UserEntity> GetUserById(string guid) => await _context.Users.FindAsync(guid) ?? throw new EntityNotFoundException($"User with guid of '{guid}' was not found");
    //helper
    private async Task<TaskEntity> GetTaskById(string guid) => await _context.Tasks.FindAsync(guid) ?? throw new EntityNotFoundException($"Task corresponding to guid '{guid}' was not found");
}