using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.Domain.Entity;

[Index(nameof(UserName), IsUnique=true)]
public class UserEntity
{
	[Key]
    public string UserGuid { get; init; } = Guid.NewGuid().ToString();
	[Required]
	public string UserName { get; init; } = null!;
	public string? UserData { get; init; }
	public string? PasswordHash { get; init; }

	private UserEntity() {}
	
	public UserEntity(string name, string passwordHash) : this()
	{
		UserName = name;
		PasswordHash = passwordHash;
	}
}