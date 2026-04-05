using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Domain;

[Index(nameof(Name), IsUnique=true)]
public class UserEntity
{
    [Key]
    public int Id { get; init; }
	[Required]
	public string? Name { get; init; }
	public string? Data { get; init; }
	public string? PasswordHash { get; init; }

	private UserEntity() {}
	public UserEntity(User user, string passwordHash) : this()
	{
		Name = user.Name;
		Data = user.Data;
		PasswordHash = passwordHash;
	}
}