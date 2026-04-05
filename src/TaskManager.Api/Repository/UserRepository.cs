using System.Threading.Tasks;

using TaskManager.Api.Domain;
using TaskManager.Api.Infrastructure;
using TaskManager.Api.Exception;
using System;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.Repository;

public class UserRepository
{
    readonly TaskManagerDbContext _context;

    public UserRepository(TaskManagerDbContext context)
    {
        _context = context;
    }

    //Crud
    public async Task CreateUser(User newUser, string passwordHash)
    {
        UserEntity UserToAdd = new(newUser, passwordHash);
        _context.Users.Add(UserToAdd);
        await _context.SaveChangesAsync();
    }

    //cRud
    public async Task<UserEntity> ReadUser(int id)
    {
        UserEntity user = await _context.Users.FindAsync(id) ?? throw new UserNotFoundException($"userId: {id}");
        return user;
    }

    //crUd
    public async Task UpdateUser(UserEntity userToUpdate)
    {
        throw new NotImplementedException();
    } 

    //cruD
    public async Task DeleteUser(UserEntity userToDelete)
    {
        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();
    }

    //Search
    public async Task<UserEntity> SearchByUsername(string username)
    {
        UserEntity userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Name == username) ?? throw new UserNotFoundException(username);
        return userEntity;
    }
}